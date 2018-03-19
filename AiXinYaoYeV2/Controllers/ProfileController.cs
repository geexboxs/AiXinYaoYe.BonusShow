using System;
using System.Web;
using AiXinYaoYeV2.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Senparc.Weixin.Helpers.Extensions;
using Senparc.Weixin.MP.AdvancedAPIs;
using OAuthScope = Senparc.Weixin.MP.OAuthScope;

namespace AiXinYaoYeV2.Controllers
{
    public class ProfileController : Controller
    {
        private readonly WXConfig _wxConfig;
        private ILogger _logger;
        private readonly AiXinYaoYeDb _aiXinYaoYeDb;

        public ProfileController(WXConfig wxConfig, ILogger<ProfileController> logger,AiXinYaoYeDb aiXinYaoYeDb)
        {
            _wxConfig = wxConfig;
            _logger = logger;
            _aiXinYaoYeDb = aiXinYaoYeDb;
        }
        public IActionResult Index(params string[] @params)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("openid")))
            {
                GetOpenId();
            }
            this._logger.LogWarning(1111, HttpContext.Session.GetString("openid"));

            //var userProfile = AiXinYaoYeDb.GetUserProfile(HttpContext.Session.GetString("openid"));
            //if (userProfile == null)
            //{
            //    return RedirectToAction("Connect");
            //}
            return View(new UserProfile(){Balance = 100,Bonus = 101,CardNum = "12312312312",Name = "hehe"});
        }

        public IActionResult Connect()
        {
            return View();
        }

        public IActionResult Save(string hybh)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("openid")))
            {
                GetOpenId();
            }

            var msg = "error";
            try
            {
                var userInfo = _aiXinYaoYeDb.GetUserProfileByHybh(hybh);
                if (userInfo == null)
                {
                    msg = "会员编号错误，请重新输入";
                }
                else
                {
                    _aiXinYaoYeDb.AddOpenId(hybh, HttpContext.Session.GetString("openid"));
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
            if (string.IsNullOrEmpty(Request.Query["code"]))
            {
                string redirect_uri = Request.GetEncodedUrl();
                var authorizeUrl = OAuthApi.GetAuthorizeUrl(WXConfig.APPID, redirect_uri,"state",OAuthScope.snsapi_userinfo);
                //WXMolde data = new WXMolde();
                //data.SetValue("appid", WXConfig.APPID);
                //data.SetValue("redirect_uri", redirect_uri);
                //data.SetValue("response_type","code");
                //data.SetValue("scope", "snsapi_userinfo");
                //data.SetValue("state", "STATE" + "#wechat_redirect");
                //string url = "https://open.weixin.qq.com/connect/oauth2/authorize?" + data.ToUrl();
                Response.Redirect(authorizeUrl);
            }
            else
            {
                string code = Request.Query["code"];
                try
                {
                    var tokenResult = OAuthApi.GetAccessToken(WXConfig.APPID, WXConfig.APPSECRET, code);
                    if (string.IsNullOrEmpty(tokenResult.access_token))
                    {
                        tokenResult = OAuthApi.GetAccessToken(WXConfig.APPID, WXConfig.APPSECRET, code);
                    }
                    HttpContext.Session.SetString("openid",tokenResult.openid);
                    //WXMolde data = new WXMolde();
                    //data.SetValue("appid",WXConfig.APPID);
                    //data.SetValue("secret",WXConfig.APPSECRET);
                    //data.SetValue("code",code);
                    //data.SetValue("grant_type","authrization_code");
                    //string url = "https://api.weixin.qq.com/sns/oauth2/access_token?" + data.ToUrl();
                    //HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                    //request.Method = "GET";
                    //HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    //Stream respStream = response.GetResponseStream();

                    //StreamReader respStreamReader = new StreamReader(respStream, Encoding.UTF8);
                    //var res = respStreamReader.ReadToEnd();
                    //var userInfo = JsonConvert.DeserializeObject<WXUserInfoModel>(res);
                }
                catch (Exception e)
                {
                    this._logger.LogError(2333,e, e.Message);
                }
            }
        }

    }
}
