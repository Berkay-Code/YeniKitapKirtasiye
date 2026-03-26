using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YeniKitapKirtasiyeWebApp.Data.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Mail Adresi Zorunludur")]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Şifre Zorunludur")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}