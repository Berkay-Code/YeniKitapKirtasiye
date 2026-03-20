using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YeniKitapKirtasiyeWebApp.Models
{
    public class Manager
    {
        public int ID { get; set; }
        [StringLength(maximumLength: 50, ErrorMessage = "En Fazla 50 Karakter Olmalıdır")]
        public string Name { get; set; }
        [StringLength(maximumLength: 50, ErrorMessage = "En Fazla 50 Karakter Olmalıdır")]
        public string Surname { get; set; }
        [DataType(DataType.EmailAddress)]
        [StringLength(maximumLength: 150, ErrorMessage = "En Fazla 150 Karakter Olmalıdır")]
        public string Mail { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsActive { get; set; }
    }
}