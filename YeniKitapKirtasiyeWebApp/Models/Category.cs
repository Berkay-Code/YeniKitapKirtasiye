using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YeniKitapKirtasiyeWebApp.Models
{
    public class Category
    {
        // Eğer Property Adı ID ise Entity otomatik olarak bu kolonu PrimaryKey olarak yapar. Identity Specification uygular.
        public int ID { get; set; }
        [Display(Name = "Kategori Adı")]
        [Required(ErrorMessage = "Kategori Adı Zorunludur")]
        [StringLength(maximumLength: 50, ErrorMessage = "En Fazla 50 Karakter Olmalıdır")]
        public string Name { get; set; }
        [Display(Name = "Aktif Mi")]
        public bool IsActive { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}