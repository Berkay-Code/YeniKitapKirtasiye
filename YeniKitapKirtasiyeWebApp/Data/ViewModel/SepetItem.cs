using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YeniKitapKirtasiyeWebApp.Data.ViewModel
{
    public class SepetItem
    {
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public string Fotograf { get; set; }
        public decimal Fiyat { get; set; }
        public int Adet { get; set; }
        public decimal Toplam => Fiyat * Adet;
    }
}