using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AiXinYaoYe.WeChat
{
    public class WXMolde
    {
        public WXMolde() { }

        private SortedDictionary<string, object> m_values = new SortedDictionary<string, object>();

        public void SetValue(string key, object value)
        {
            m_values[key] = value;
        }

        public object GetValue(string key)
        {
            object obj = null;
            m_values.TryGetValue(key, out obj);
            return obj;
        }

        public string ToUrl()
        {
            string buff = "";
            foreach (KeyValuePair<string, object> pair in m_values)
            {
                if (pair.Value == null)
                {
                    throw new Exception("WxPayData内部含有值为null的字段!");
                }

                if (pair.Key != "sign" && pair.Value.ToString() != "")
                {
                    buff += pair.Key + "=" + pair.Value + "&";
                }
            }
            buff = buff.Trim('&');
            return buff;
        }
    }
}