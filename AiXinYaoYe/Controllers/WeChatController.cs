using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AiXinYaoYe.Controllers
{
    public class WeChatController : Controller
    {
        [HttpGet]
        public ActionResult Index(string signature, string timestamp, string nonce, string echostr)
        {
            var token = "microex_wechat_mrkkk_lulus";
            var ent = "";
            if (!CheckSignature(signature,timestamp,nonce,token,out ent))
            {
                return Content("参数错误！");
            }

            return Content(echostr);
        }

        public static bool CheckSignature(string signature, string timestamp, string nonce, string token, out string ent)
        {
            var arr = new[] { token, timestamp, nonce }.OrderBy(z => z).ToArray();
            var arrString = string.Join("", arr);
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var sha1Arr = sha1.ComputeHash(Encoding.UTF8.GetBytes(arrString));
            StringBuilder enText = new StringBuilder();
            foreach (var b in sha1Arr)
            {
                enText.AppendFormat("{0:x2}", b);
            }
            ent = enText.ToString();
            return signature == enText.ToString();
        }
    }
}