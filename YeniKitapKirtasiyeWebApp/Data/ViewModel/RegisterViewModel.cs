using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YeniKitapKirtasiyeWebApp.Data.ViewModel
{
    public class RegisterViewModel
    {
        [StringLength(maximumLength: 50, ErrorMessage = "En Fazla 50 Karakter Olmalıdır")]
        public string Name { get; set; }

        [StringLength(maximumLength: 50, ErrorMessage = "En Fazla 50 Karakter Olmalıdır")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Mail Adresi Zorunludur")]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Şifre Zorunludur")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string PasswordAgain { get; set; }
    }
}