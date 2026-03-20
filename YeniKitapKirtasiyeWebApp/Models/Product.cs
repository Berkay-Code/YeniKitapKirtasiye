using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YeniKitapKirtasiyeWebApp.Models
{
    public class Product
    {
        public int ID { get; set; }
        public int Category_ID { get; set; }

        [ForeignKey("Category_ID")]
        public virtual Category category { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur")]
        [StringLength(maximumLength: 250, ErrorMessage = "En Fazla 250 Karakter Olmalıdır")]
        public string Name { get; set; }
        public short Stock { get; set; }
        public decimal Price { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}