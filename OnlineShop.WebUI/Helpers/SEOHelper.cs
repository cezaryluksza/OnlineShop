using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.WebUI.Helpers
{
    public static class SEOHelper
    {
        public static string EscapeUrl(string url)
        {
            url = url.Replace(' ', '-');
            url = url.ToLower();
            url = url.Replace('ą', 'a');
            url = url.Replace('ć', 'c');
            url = url.Replace('ę', 'e');
            url = url.Replace('ł', 'l');
            url = url.Replace('ó', 'o');
            url = url.Replace('ś', 's');
            url = url.Replace('ź', 'z');
            url = url.Replace('ż', 'z');
            return url;
        }
    }
}