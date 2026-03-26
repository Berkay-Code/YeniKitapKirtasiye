using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YeniKitapKirtasiyeWebApp.Data.ViewModel;

namespace YeniKitapKirtasiyeWebApp.Data.Helper
{
    public class SepetHelper
    {
        public static List<SepetItem> SepetAl(HttpSessionStateBase session)
        {
            List<SepetItem> sepet = session["Sepet"] as List<SepetItem>;
            if (sepet == null)
            {
                sepet = new List<SepetItem>();
                session["Sepet"] = sepet;
            }
            return sepet;
        }

        public static void UrunEkle(HttpSessionStateBase session, SepetItem item)
        {
            List<SepetItem> sepet = SepetAl(session);
            SepetItem mevcutItem = sepet.FirstOrDefault(x => x.UrunId == item.UrunId);

            if (mevcutItem != null)
            {
                mevcutItem.Adet++;
            }
            else
            {
                sepet.Add(item);
            }

            session["Sepet"] = sepet;
        }

        public static void UrunCikar(HttpSessionStateBase session, int urunId)
        {
            List<SepetItem> sepet = SepetAl(session);
            SepetItem item = sepet.FirstOrDefault(x => x.UrunId == urunId);
            if (item != null)
            {
                sepet.Remove(item);
            }

            session["Sepet"] = sepet;
        }

        public static void SepetTemizle(HttpSessionStateBase session)
        {
            session["Sepet"] = new List<SepetItem>();
        }

        public static int ToplamAdet(HttpSessionStateBase session)
        {
            return SepetAl(session).Sum(x => x.Adet);
        }

        public static void AdetGuncelle(HttpSessionStateBase session, int urunId, int yeniAdet)
        {
            List<SepetItem> sepet = SepetAl(session);
            SepetItem item = sepet.FirstOrDefault(x => x.UrunId == urunId);

            if (item != null)
            {
                if (yeniAdet <= 0)
                {
                    sepet.Remove(item);
                }
                else
                {
                    item.Adet = yeniAdet;
                }
            }

            session["Sepet"] = sepet;
        }
    }
}