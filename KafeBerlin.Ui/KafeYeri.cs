using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeBerlin.Ui
{
    public class KafeYeri
    {
        public int MasaAdet { get; set; } = 20; // default olarak 20 masayla başlasın
        public List<Urun> Urunler { get; set; }=new List<Urun>(); // default değerleri
        public List<Siparis> AktifSiparisler { get; set; }=new List<Siparis>();
        public List<Siparis> GecmisSiparisler { get; set; }=new List<Siparis>();

    }
}
