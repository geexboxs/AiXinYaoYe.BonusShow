using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AiXinYaoYe.WeChat
{
    public class WXUserInfoModel
    {
        public string Access_token { get; set; }

        public string Expires_in { get; set; }

        public string Refresh_token { get; set; }

        public string Openid { get; set; }

        public string Scope { get; set; }
    }
}