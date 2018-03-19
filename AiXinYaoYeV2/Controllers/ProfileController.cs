using System;
using System.Web;
using AiXinYaoYeV2.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Senparc.Weixin.Helpers.Extensions;
using Senparc.Weixin.MP.AdvancedAPIs;
using OAuthScope = Senparc.Weixin.MP.OAuthScope;

namespace AiXinYaoYeV2.Controllers
{
    public class ProfileController : Controller
    {
        private ILogger _logger;
        private readonly AiXinYaoYeDb _aiXinYaoYeDb;
        private IConfiguration _config;

        public ProfileController( ILogger<ProfileController> logger,AiXinYaoYeDb aiXinYaoYeDb, IConfiguration config)
        {
            _logger = logger;
            _aiXinYaoYeDb = aiXinYaoYeDb;
            _config = config;
        }
        [HttpGet]
        public ActionResult GetUserProfile()
        {
            this._logger.LogError(123, HttpContext.Session.GetString("openid"));
            var userInfo = _aiXinYaoYeDb.GetUserProfile(HttpContext.Session.GetString("openid"));

            if (userInfo == null)
            {
                return Json(new {success = false});
            }

            return Json(new{success=true, userInfo });
        }

        public IActionResult Index()
        {
            this._logger.LogError(123, _config["WxConfig:AppId"]+ ":" + _config["WxConfig:AppSecret"]);
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("openid")))
            {
                GetOpenId();
            }
            //var userProfile = AiXinYaoYeDb.GetUserProfile(HttpContext.Session.GetString("openid"));
            //if (userProfile == null)
            //{
            //    return RedirectToAction("Connect");
            //}
            return View();
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
                this._logger.LogError(123, e, e.Message);
            }

            return Json(new {msg});
        }

        public void GetOpenId()
        {
            if (string.IsNullOrEmpty(Request.Query["code"]))
            {
                string redirect_uri = Request.GetEncodedUrl();
                var authorizeUrl = OAuthApi.GetAuthorizeUrl(_config["WxConfig:AppId"], redirect_uri,"state",OAuthScope.snsapi_base);
                this._logger.LogError(123, "authorizeUrl:" + authorizeUrl);
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
                    this._logger.LogError(123, "code:" + code);
                    var tokenResult = OAuthApi.GetAccessToken(_config["WxConfig:AppId"], _config["WxConfig:AppSecret"], code);
                    this._logger.LogError(123, "authorizeUrl:" + tokenResult);
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
                    this._logger.LogError(123, e, e.Message);
                }
            }
        }

    }
}
