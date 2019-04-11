using OtelRezervasyonu.Enums;
using OtelRezervasyonu.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtelRezervasyonu
{
    public partial class FormOdaEkle : Form
    {
        OtelRezervasyonDBEntities _db;
        decimal a;
        public FormOdaEkle()
        {
            InitializeComponent();
            _db = new OtelRezervasyonDBEntities();
            txtOdaNo.KeyPress += Helper.OnlyNumber;
            txtOdaFiyati.KeyPress += Helper.OnlyNumber;
        }
        public void Listele()
        {
            dgvOdaListesi.DataSource = (from o in _db.Oda select new { o.OdaID, o.OdaNo, o.OdaTuruEnum, o.Kapasite, o.Fiyat, o.Silindi }).OrderBy(o => o.OdaNo).ToList();
        }

        public void OdaEkle()
        {
            string hataMesaji = "";
            if (string.IsNullOrWhiteSpace(txtOdaNo.Text) || txtOdaNo.Text.Length != 3)
            {
                hataMesaji += "Oda no boş geçilemez ve üç haneli olmalıdır.\n";
            }

            if (cmbOdaKapasitesi.SelectedIndex == -1)
            {
                hataMesaji += "Oda kapasitesi belirtilmedi.\n";
            }

            if (cmbOdaTuru.SelectedIndex == -1)
            {
                hataMesaji += "Oda türü belirtilmedi.\n";
            }

            if (hataMesaji.Length > 0)
            {
                MessageBox.Show(hataMesaji, " ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Oda oda = new Oda();
            oda.OdaNo = txtOdaNo.Text;
            oda.Kapasite = (byte)(int)cmbOdaKapasitesi.SelectedValue;
            oda.OdaTuruEnum = (byte)(int)cmbOdaTuru.SelectedValue;
            oda.Fiyat = Convert.ToDecimal(txtOdaFiyati.Text);
            _db.Oda.Add(oda);
            _db.SaveChanges();
            MessageBox.Show("Kayıt işlemi başarılı.", "Kaydedildi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
        }
        public void OdaSil()
        {
            if (dgvOdaListesi.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silme işlemi yapabilmek için ilk önce silinecek oda seçilmelidir.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int odaID = (int)dgvOdaListesi.SelectedRows[0].Cells[0].Value;
            Oda oda = _db.Oda.Find(odaID);
            oda.Silindi = true;
            _db.SaveChanges();
            MessageBox.Show("Silme işlemi başarıyla tamamlandı.", "Silindi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();

        }
        public void OdaGuncelle()
        {
            if (dgvOdaListesi.SelectedRows.Count == 0)
            {
                MessageBox.Show("Güncelleme işlemi yapabilmek için ilk önce güncellenecek oda seçilmelidir.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string hataMesaji = "";
            if (string.IsNullOrWhiteSpace(txtOdaNo.Text) || txtOdaNo.Text.Length != 3)
            {
                hataMesaji += "Oda no boş geçilemez ve üç haneli olmalıdır.\n";
            }

            if (cmbOdaKapasitesi.SelectedIndex == -1)
            {
                hataMesaji += "Oda kapasitesi belirtilmedi.\n";
            }

            if (cmbOdaTuru.SelectedIndex == -1)
            {
                hataMesaji += "Oda türü belirtilmedi.\n";
            }

            if (hataMesaji.Length > 0)
            {
                MessageBox.Show(hataMesaji, " ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int odaID = (int)dgvOdaListesi.SelectedRows[0].Cells[0].Value;
            Oda oda = _db.Oda.Find(odaID);
            oda.Kapasite = (byte)(int)cmbOdaKapasitesi.SelectedValue;
            oda.OdaTuruEnum = (byte)(int)cmbOdaTuru.SelectedValue;
            _db.SaveChanges();
            MessageBox.Show("Oda bilgileri başarıyla güncellendi", "Güncellendi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
        }

        public void Filtrele()
        {
            var filtrele = (from o in _db.Oda select o);

            if (txtOdaNo.Text != null)
            {
                filtrele = filtrele.Where(o => o.OdaNo.Contains(txtOdaNo.Text)).Select(o => o).OrderBy(o => o.OdaNo);
            }
            if (txtOdaFiyati.Text != null)
            {
                filtrele = filtrele.Where(o => o.Fiyat == a).Select(o => o).OrderBy(o => o.OdaNo);
            }
            if (cmbOdaKapasitesi.SelectedIndex != -1)
            {
                filtrele = filtrele.Where(o => o.Kapasite.Equals((byte)(int)cmbOdaKapasitesi.SelectedValue)).Select(o => o).OrderBy(o => o.OdaNo);
            }
            if (cmbOdaTuru.SelectedIndex != -1)
            {
                filtrele = filtrele.Where(o => o.OdaTuruEnum.Equals((byte)(int)cmbOdaTuru.SelectedValue)).Select(o => o).OrderBy(o => o.OdaNo);
            }

            dgvOdaListesi.DataSource = filtrele.ToList();
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            OdaEkle();
            Helper.ControlTemizle(this);
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            Listele();
            Helper.ControlTemizle(this);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            OdaGuncelle();
            Helper.ControlTemizle(this);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            OdaSil();
            Helper.ControlTemizle(this);
        }

        private void FormOdaEkle_Load(object sender, EventArgs e)
        {
            cmbOdaTuru.ListControlDoldur<OdaTuruEnum>();
            cmbOdaKapasitesi.ListControlDoldur<OdaKapasitesiEnum>();
            Listele();
        }

        private void FormOdaEkle_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormAnaSayfa formAnaSayfa = new FormAnaSayfa();
            formAnaSayfa.Show();
        }

        private void txtOdaNo_TextChanged(object sender, EventArgs e)
        {
            Filtrele();
        }

        private void cmbOdaTuru_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filtrele();
        }

        private void cmbOdaKapasitesi_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filtrele();
        }

        private void txtOdaFiyati_TextChanged(object sender, EventArgs e)
        {
            a = Convert.ToDecimal(txtOdaFiyati.Text);
            Filtrele();
        }
    }
}
