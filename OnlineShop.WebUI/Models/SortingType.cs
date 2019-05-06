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
        [Description("Najwyższa cena")]
        HighestPrice = 1,
        [Description("Najniższa cena")]
        LowestPrice = 2,
        [Description("Najbardziej popularne")]
        MostPopular = 3,
        [Description("Najwyżej oceniane")]
        HighestRating = 4,
        [Description("Największa liczba komentarzy")]
        LargestNumberOfComments = 5,
    }


    //static public class SortingHelper
    //{
    //    public static SortingType GetEnumEnglishName(string polishName)
    //    {
    //        SortingType englishName = 0;
    //        switch (polishName)
    //        {
    //            case "NajwyzszaCena":
    //                englishName = SortingType.HighestPrice;
    //                break;
    //            case "NajnizszaCena":
    //                englishName = SortingType.LowestPrice;
    //                break;
    //            case "Popularnosc":
    //                englishName = SortingType.MostPopular;
    //                break;
    //            case "Ocena":
    //                englishName = SortingType.HighestRating;
    //                break;
    //            case "Komentarze":
    //                englishName = SortingType.LargestNumberOfComments;
    //                break;
    //            default:
    //                break;
    //        }
    //        return englishName;
    //    }

    //    public static string GetEnumPolishName(SortingType englishName)
    //    {
    //        string polishName = string.Empty;
    //        switch (englishName)
    //        {
    //            case SortingType.HighestPrice:
    //                polishName = "NajwyzszaCena";
    //                break;
    //            case SortingType.LowestPrice:
    //                polishName = "NajnizszaCena";
    //                break;
    //            case SortingType.MostPopular:
    //                polishName = "Popularnosc";
    //                break;
    //            case SortingType.HighestRating:
    //                polishName = "Ocena";
    //                break;
    //            case SortingType.LargestNumberOfComments:
    //                polishName = "Komentarze";
    //                break;
    //            default:
    //                break;
    //        }
    //        return polishName;
    //    }
    //}
}