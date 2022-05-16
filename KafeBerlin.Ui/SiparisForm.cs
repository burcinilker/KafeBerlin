using KafeBerlin.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KafeBerlin.Ui
{
    public partial class SiparisForm : Form
    {
        private readonly KafeVeri _db;
        private readonly Siparis _siparis;
        BindingList<SiparisDetay> _blSiparisDetaylar;
        public SiparisForm(KafeVeri db,Siparis siparis)
        {
            InitializeComponent();
            _siparis = siparis;
            _db = db;
            dgvDetaylar.AutoGenerateColumns = false;
            MasaNoGuncelle();
            OdemeTutariGuncelle();
            UrunleriListele();
            DetaylariListele();
        }

        private void DetaylariListele()
        {
            _blSiparisDetaylar = new BindingList<SiparisDetay>(_siparis.SiparisDetaylar);
            _blSiparisDetaylar.ListChanged += _blSiparisDetaylar_ListChanged;
            dgvDetaylar.DataSource = _blSiparisDetaylar;
            //dgvDetaylar.Columns[0].HeaderText = "Ürün"; // ilk sütunun başına Ürün yazar
        }

        private void _blSiparisDetaylar_ListChanged(object sender, ListChangedEventArgs e)
        {
            OdemeTutariGuncelle();
        }

        private void OdemeTutariGuncelle()
        {
            lblOdemeTutari.Text = _siparis.ToplamTutarTL;
        }

        private void UrunleriListele()
        {
            cboUrun.DataSource = _db.Urunler;
            //cboUrun.DisplayMember = "UrunAd"; sadece ürün adını gösterir comboboxda
        }

        private void MasaNoGuncelle()
        {
            Text = $"Masa {_siparis.MasaNo}";
            lblMasaNo.Text =_siparis.MasaNo.ToString("00");
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (cboUrun.SelectedItem == null)
            {
                MessageBox.Show("Önce bir ürün seçiniz");
                return;
            }
            Urun urun =(Urun)cboUrun.SelectedItem;
            _blSiparisDetaylar.Add(new SiparisDetay()
            {
                Adet = (int)nudAdet.Value,
                UrunAd =urun.UrunAd,
                BirimFiyat=urun.BirimFiyat,

            }) ;

            nudAdet.Value = 1;
        }

        private void btnAnaSayfayaDon_Click(object sender, EventArgs e)
        {
            Close();
            //DialogResult = DialogResult.Cancel; // 2. yöntem
        }

        private void btnSiparisIptal_Click(object sender, EventArgs e)
        {
            //_siparis.OdenenTutar = 0;
            //_siparis.Durum = SiparisDurum.Iptal;
            //_db.AktifSiparisler.Remove(_siparis);
            //_db.GecmisSiparisler.Add(_siparis);
            //DialogResult= DialogResult.OK;

            SiparisKapat(_siparis.ToplamTutar(), SiparisDurum.Odendi);
        }

        private void btnOdemeAl_Click(object sender, EventArgs e)
        {
            SiparisKapat(_siparis.ToplamTutar(), SiparisDurum.Odendi);

            //_siparis.OdenenTutar = _siparis.ToplamTutar();
            //_siparis.Durum = SiparisDurum.Odendi;
            //_db.AktifSiparisler.Remove(_siparis);
            //_db.GecmisSiparisler.Add(_siparis);
            //DialogResult = DialogResult.OK;
        }
        void SiparisKapat(decimal odenenTutar,SiparisDurum durum)
        {
            string eylem = durum == SiparisDurum.Iptal ? "iptal edilecektir" : "kaptılacaktır";
            string baslik = durum == SiparisDurum.Iptal ? "iptal" : "Kapatma"; 
            DialogResult dr = MessageBox.Show($"{_siparis.MasaNo} nolu masanın siparişi {eylem} .Emin misiniz?",
                $"Masa {baslik} Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Yes)
            {
            _siparis.OdenenTutar = odenenTutar;
            _siparis.Durum = durum;
            _db.AktifSiparisler.Remove(_siparis);
            _db.GecmisSiparisler.Add(_siparis);
            DialogResult = DialogResult.OK;

            }
        }
    }
}
