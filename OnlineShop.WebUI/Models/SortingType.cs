using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace OnlineShop.WebUI.Models
{
    public enum SortingType
    {
        [Description("Brak Sortowania")]
        NoSorting = 0,
        [Description("Popularność")]
        MostPopular = 1,
        [Description("Nazwa A-Z")]
        NameAZ = 2,
        [Description("Nazwa Z-A")]
        NameZA = 3,
        [Description("Najwyższa cena")]
        HighestPrice = 4,
        [Description("Najniższa cena")]
        LowestPrice = 5,
    }
}