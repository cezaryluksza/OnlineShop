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
            return url.Replace(' ', '-');
        }
    }
}