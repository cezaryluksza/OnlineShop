using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace OnlineShop.WebUI.Helpers
{
    static public class EnumHelper
    {
        static public string GetDescription(System.Enum type)
        {
            var attrs = type.GetType().GetMember(type.ToString()).FirstOrDefault().GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attrs.Length > 0)
            {
                return ((DescriptionAttribute)attrs[0]).Description.ToString();
            }
            return string.Empty;
        }
    }
}