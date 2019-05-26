using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using OnlineShop.Domain.Entities;

namespace OnlineShop.WebUI.Models
{
    public class IndexViewModel
    {
        public string Email { get; set; }
        [Display(Name = "Imię")]
        public string FirstName { get; set; }
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        public Address Address { get; set; }
        public bool HasPassword { get; set; }
        [Display(Name = "Numer telefonu")]
        public string PhoneNumber { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Aktualne hasło")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi się składać z co najmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź nowe hasło")]
        [Compare("NewPassword", ErrorMessage = "Nowe hasło musi się zgadzać.")]
        public string ConfirmPassword { get; set; }
    }


}