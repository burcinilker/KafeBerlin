using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeBerlin.Ui
{
    public class Siparis
    {
        public int MasaNo { get; set; }
        public SiparisDurum Durum { get; set; } = SiparisDurum.Aktif;
        public decimal OdenenTutar { get; set; }
        public DateTime? AcilisZamani { get; set; }=DateTime.Now;
        public DateTime? KapanisZamani { get; set; }
        public List<SiparisDetay> SiparisDetaylar { get; set; }=new List<SiparisDetay>();
        public string ToplamTutarTL => $"{ToplamTutar():c2}";
        public decimal ToplamTutar()
        {
            //decimal toplam = 0;
            //foreach (var siparisDetay in SiparisDetaylar)
            //{
            //    toplam += siparisDetay.Tutar();
            //}
            //return toplam;

            return SiparisDetaylar.Sum(sd => sd.Tutar());
        }
    }
}
