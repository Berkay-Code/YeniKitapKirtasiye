using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YeniKitapKirtasiyeWebApp.Models
{
    public class Orders
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public virtual User user { get; set; }

        public DateTime OrderDateTime { get; set; }

        public decimal SumPrice { get; set; }

        public string KartIsim { get; set; }
        public string KartNo { get; set; }
        public string SonKullanmaTarihi { get; set; }
        public string CVV { get; set; }
    }
}