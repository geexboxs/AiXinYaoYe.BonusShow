using System;
using Microsoft.Extensions.Caching.Memory;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.CommonAPIs;

namespace AiXinYaoYeV2
{
    public class WXConfig
    {
        
        private readonly IMemoryCache _memoryCache;

        public WXConfig(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public const string APPID = "wx3c0a54573d979889";
        public const string APPSECRET = "2e687ca4bf427f5ae4b68716c94c9131";

        public string AccessToken
        {
            get
            {

                if (_memoryCache.TryGetValue(nameof(AccessToken), out string accessToken))
                {
                    return accessToken;
                }

                var tokenResult = CommonApi.GetToken(APPID, APPSECRET);
                AccessToken = tokenResult.access_token;

                return tokenResult.access_token;
            }
            set { _memoryCache.Set(nameof(AccessToken), value, TimeSpan.FromSeconds(7100)); }
        }
    }
}