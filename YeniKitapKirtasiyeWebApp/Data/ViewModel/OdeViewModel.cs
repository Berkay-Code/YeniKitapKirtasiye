using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YeniKitapKirtasiyeWebApp.Data.ViewModel
{
    public class OdeViewModel
    {
        [Required(ErrorMessage = "*")]
        public string CardName { get; set; }
        [Required(ErrorMessage = "*")]
        public string CardNumber { get; set; }
        [Required(ErrorMessage = "*")]
        public string Expiry { get; set; }
        [Required(ErrorMessage = "*")]
        public string CVV { get; set; }
    }
}