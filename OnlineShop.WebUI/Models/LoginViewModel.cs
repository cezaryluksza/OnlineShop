using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.WebUI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Proszę podać nazwę użytkownika")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Proszę podać hasło")]
        public string Password { get; set; }
    }
}