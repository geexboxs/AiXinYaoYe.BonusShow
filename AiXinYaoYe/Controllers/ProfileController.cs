using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AiXinYaoYe.Config;
using AiXinYaoYe.Database;
using AiXinYaoYe.WeChat;
using Microsoft.Web.Infrastructure;
using Newtonsoft.Json;

namespace AiXinYaoYe.Controllers
{
    public class ProfileController : Controller
    {
#if DEBUG
        public string OpenId = "123";
#else
        public string OpenId = "";
#endif
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(OpenId))
            {
                GetOpenId();
            }
            var userProfile = AiXinYaoYeDb.GetUserProfile(OpenId);
            if (userProfile == null)
            {
                return RedirectToAction("Connect");
            }

            return View(userProfile);
        }

        public ActionResult Connect()
        {
            return View();
        }

        public ActionResult Save(string hybh)
        {
            if (string.IsNullOrEmpty(OpenId))
            {
                GetOpenId();
            }

            var msg = "error";
            try
            {
                var userInfo = AiXinYaoYeDb.GetUserProfileByHybh(hybh);
                if (userInfo == null)
                {
                    msg = "会员编号错误，请重新输入";
                }
                else
                {
                    AiXinYaoYeDb.AddOpenId(hybh, OpenId);
                    msg = "success";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Json(new {msg});
        }

        public void GetOpenId()
        {
            if (string.IsNullOrEmpty(Request.QueryString["code"]))
            {
                string redirect_uri = HttpUtility.UrlEncode(Request.Url.ToString());
                WXMolde data = new WXMolde();
                data.SetValue("appid", WXConfig.APPID);
                data.SetValue("redirect_uri", redirect_uri);
                data.SetValue("response_type","code");
                data.SetValue("scope", "snsapi_userinfo");
                data.SetValue("state", "STATE" + "#wechat_redirect");
                string url = "https://open.weixin.qq.com/connect/oauth2/authorize?" + data.ToUrl();
                try
                {
                    Response.Redirect(url);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            else
            {
                string code = Request.QueryString["code"];
                try
                {
                    WXMolde data = new WXMolde();
                    data.SetValue("appid",WXConfig.APPID);
                    data.SetValue("secret",WXConfig.APPSECRET);
                    data.SetValue("code",code);
                    data.SetValue("grant_type","authrization_code");
                    string url = "https://api.weixin.qq.com/sns/oauth2/access_token?" + data.ToUrl();
                    HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                    request.Method = "GET";
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    Stream respStream = response.GetResponseStream();

                    StreamReader respStreamReader = new StreamReader(respStream, Encoding.UTF8);
                    var res = respStreamReader.ReadToEnd();
                    var userInfo = JsonConvert.DeserializeObject<WXUserInfoModel>(res);
                    OpenId = userInfo.Openid;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        
    }
}
