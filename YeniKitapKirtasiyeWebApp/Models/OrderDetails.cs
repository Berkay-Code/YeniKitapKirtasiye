using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YeniKitapKirtasiyeWebApp.Models
{
    public class OrderDetails
    {
        public int OrderID { get; set; }

        [ForeignKey("OrderID")]
        public virtual Orders order { get; set; }
        public int ProductID { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product product { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}