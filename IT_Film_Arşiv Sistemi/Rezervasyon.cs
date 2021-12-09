using IT_Film_Arşiv_Sistemi.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IT_Film_Arşiv_Sistemi
{
    public partial class FrmRezervasyon : Form
    {        
        SinemaOtomasyon db = new SinemaOtomasyon();        
        public FrmRezervasyon()
        {
            InitializeComponent();
        }        
        public string adsoyad;
        int koltukno, sayac = 0, boskoltuk = 35, dolukoltuk = 0;
        
        int[] dolukoltukdizi = new int[0];

        private void btnBiletAyır_Click(object sender, EventArgs e)
        {
            Ucret ücret = new Ucret();
            if (rbTam.Checked == true) ücret.Bilet_Ucreti = 20;
            else ücret.Bilet_Ucreti = 16;
            int ucretID = db.Ucrets.FirstOrDefault(x => x.Bilet_Ucreti == ücret.Bilet_Ucreti.Value).ID;
            if (txtAdSoyad.Text == "" || txtKoltukNO.Text == "" || cmbFilmAd.SelectedIndex == -1 || cmbSalon.SelectedIndex == -1 || cmbSeans.SelectedIndex == -1) MessageBox.Show("Lütfen Boş alanları doldurunuz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {              
                try
                {
                    koltukno = Convert.ToInt32(txtKoltukNO.Text);
                    adsoyad = txtAdSoyad.Text;
                    if (koltukno < 1 || koltukno > 35)
                    {
                        MessageBox.Show("Lütfen geçerli bir koltuk numarası giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtKoltukNO.Text = "";                       
                    }
                    else
                    {
                        if (Array.IndexOf(dolukoltukdizi, koltukno) == -1)
                        {                           
                            Label koltukara = this.Controls.Find("koltuk" + koltukno.ToString(), true).FirstOrDefault() as Label;
                            if (koltukara != null)
                            {
                                db.BiletSatış.Add(new BiletSatış { FilmID = Convert.ToInt32(cmbFilmAd.SelectedValue), SalonID = Convert.ToInt32(cmbSalon.SelectedValue), SeansID = Convert.ToInt32(cmbSeans.SelectedValue),KoltukNo=Convert.ToString(koltukno), MusteriID = db.KullanıcıGiris.FirstOrDefault(x => x.Musteri_Adı_Soyadı == txtAdSoyad.Text).ID, UcretID = ucretID });
                                //koltukara.Text += "\r" + adsoyad;
                                koltukara.BackColor = Color.IndianRed;                          
                                dolukoltuk++;
                                boskoltuk--;

                                Array.Resize(ref dolukoltukdizi, dolukoltukdizi.Length + 1);
                                dolukoltukdizi[sayac] = koltukno;
                                sayac++;
                                
                                lblDolu.Text = dolukoltuk.ToString();
                                lblBoş.Text = boskoltuk.ToString();
                                db.SaveChanges();
                                Yükle(db.BiletSatış.ToList());
                                txtKoltukNO.Text = "";
                                txtAdSoyad.Text = "";
                                cmbFilmAd.SelectedIndex = -1;
                                cmbSalon.SelectedIndex = -1;
                                cmbSeans.SelectedIndex = -1;
                                rbTam.Checked = false;
                                rbOgrenci.Checked = false;
                                MessageBox.Show("Bilet Başarıyla Satılmıştır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }                           
                        }
                        else
                        {
                            MessageBox.Show("Girmiş olduğunuz koltuk numarasına ait koltuk dolu", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtKoltukNO.Text = "";
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Lütfen geçerli bir koltuk numarası giriniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtKoltukNO.Text = "";
                }                
            }           
        }
        private void cmbSalon_SelectedIndexChanged(object sender, EventArgs e)
        {
            dolukoltuk = 0;
            boskoltuk = 35;
            if (cmbSalon.Text == "Salon 1")
            {
                YenidenRenklendir();
                VeritabanıDoluKoltuklar();
            }
            else if (cmbSalon.Text == "Salon 2")
            {
                YenidenRenklendir();
                VeritabanıDoluKoltuklar();
            }                
            else if (cmbSalon.Text == "Salon 3")
            {
                YenidenRenklendir();
                VeritabanıDoluKoltuklar();
            }           
            lblDolu.Text = dolukoltuk.ToString();
            lblBoş.Text = boskoltuk.ToString();
        }
        private void cmbFilmAd_SelectedIndexChanged(object sender, EventArgs e)
        {
            dolukoltuk = 0;
            boskoltuk = 35;
            if (cmbFilmAd.Text != "")
            {
                YenidenRenklendir();
                VeritabanıDoluKoltuklar();
            }            
            lblDolu.Text = dolukoltuk.ToString();
            lblBoş.Text = boskoltuk.ToString();
        }
        private void cmbSeans_SelectedIndexChanged(object sender, EventArgs e)
        {
            dolukoltuk = 0;
            boskoltuk = 35;
            if (cmbSeans.Text != "")
            {
                YenidenRenklendir();
                VeritabanıDoluKoltuklar();
            }            
            lblDolu.Text = dolukoltuk.ToString();
            lblBoş.Text = boskoltuk.ToString();
        }
        private void btnBiletİptalEt_Click(object sender, EventArgs e)
        {
            try
            {
                koltukno = Convert.ToInt32(txtKoltukNOİptal.Text);  
                
                if (koltukno < 1 || koltukno > 35)
                {
                    MessageBox.Show("Lütfen geçerli bir koltuk numarası giriniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtKoltukNOİptal.Text = "";                    
                }

                else
                {                  
                    if (Array.IndexOf(dolukoltukdizi, koltukno) != -1)
                    {
                        Label koltukara = this.Controls.Find("koltuk" + koltukno.ToString(), true).FirstOrDefault() as Label;
                        if (koltukara != null)
                        {
                            DialogResult result = MessageBox.Show("Bu koltuğu iptal etmek istediğinizden emin misiniz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (result == DialogResult.Yes)
                            {                             
                                string seciliID = Convert.ToString(koltukno);
                                db.SpBiletSatısIptal(seciliID);
                                
                                koltukara.Text = koltukno + ".koltuk";
                                koltukara.BackColor = Color.GreenYellow;
                                dolukoltuk--;
                                boskoltuk++;

                                int sirano = Array.IndexOf(dolukoltukdizi, koltukno);
                                Array.Clear(dolukoltukdizi, sirano, 1);                                                        
                            }
                            //lblDolu.Text = dolukoltuk.ToString();
                            //lblBoş.Text = boskoltuk.ToString();
                            db.SaveChanges();
                            Yükle(db.BiletSatış.ToList());
                            txtKoltukNOİptal.Text = "";
                            MessageBox.Show("Biletiniz İptal Edilmiştir", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);                     
                        }
                    }
                    else
                    {
                        MessageBox.Show("İptal edilmek istenen koltuk zaten boş!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtKoltukNOİptal.Text = "";
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen geçerli bir koltuk numarası giriniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtKoltukNOİptal.Text = "";
            }          
        }     
        private void btnAnasayfaDön_Click(object sender, EventArgs e)
        {
            FrmAnasayfa anasayfa = new FrmAnasayfa();
            anasayfa.Show();
            this.Hide();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblSaat.Text = DateTime.Now.ToLongTimeString();
            lblTarih.Text = DateTime.Now.Date.ToShortDateString();
        }
        private void FrmRezervasyon_Load(object sender, EventArgs e)
        {          
            timer1.Enabled = true;
            txtAdSoyad.Text = adsoyad;            
            Yükle(db.BiletSatış.ToList());
            cmbFilmAd.DataSource = db.Films.ToList();
            cmbSalon.DataSource = db.Salons.ToList();
            cmbSeans.DataSource = db.Seans.ToList();
            
            //AdminSistemi admin = new AdminSistemi();
            //Archive archive = new Archive { Director = admin.txtDirector.Text, FilmCategory = admin. cmbFilmCategory.Text, FilmName =admin.txtFilmName.Text, FilmTime =admin.txtFilmTime.Text, Menşei = admin.cmbMenşei.Text, VisionDate=admin.dtpVisionDate.Value, FilmID = archives.Count };
            //archives.Add(archive);
        }
       
        void Yükle(List<BiletSatış> biletler)
        {
            if (biletler.Count > 0)
            {
                dgwfilmsec.DataSource = null;
                dgwfilmsec.DataSource = db.SpBiletSatısListele().ToList();
                dgwfilmsec.Columns["Kayıt_Sırası"].Visible = false;
                dgwfilmsec.Columns["Musteri_Adı_Soyadı"].HeaderText = "Müşteri Adı Soyadı";
                dgwfilmsec.Columns["Film_Adı"].HeaderText = "Film Adı";
                dgwfilmsec.Columns["Salon_Adı"].HeaderText = "Salon Adı";
                dgwfilmsec.Columns["Bilet_Ucreti"].HeaderText = "Bilet Ücreti";
                dgwfilmsec.Columns["KoltukNo"].HeaderText = "Koltuk No";
            }


            //if (archives.Count > 0)
            //{
            //    dgwfilmsec.DataSource = archives;
            //    dgwfilmsec.Columns["FilmID"].Visible = false;
            //    dgwfilmsec.Columns["FilmName"].HeaderText = "FİLM ADI";
            //    dgwfilmsec.Columns["FilmCategory"].HeaderText = "FİLM TÜRÜ";
            //    dgwfilmsec.Columns["FilmTime"].HeaderText = "FİLM SÜRESİ";
            //    dgwfilmsec.Columns["Director"].HeaderText = "YÖNETMEN";
            //    dgwfilmsec.Columns["Menşei"].HeaderText = "YAPILDIĞI ÜLKE";
            //    dgwfilmsec.Columns["VisionDate"].HeaderText = "VİZYON TARİHİ";

            //}
        }     
        private void dgwfilmsec_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtKoltukNOİptal.Text = dgwfilmsec.CurrentRow.Cells["KoltukNo"].Value.ToString();         
        }
        private void VeritabanıDoluKoltuklar()
        {           
            var sorgu1 = from k in db.BiletSatış join f in db.Films on k.FilmID equals f.ID
             join b in db.Salons on k.SalonID equals b.ID join s in db.Seans on k.SeansID equals s.ID where f.Film_Adı == cmbFilmAd.Text && b.Salon_Adı == cmbSalon.Text && s.Saat==cmbSeans.Text select k.KoltukNo;
            foreach (var item in sorgu1)
            {
                void dolukoltuklar()
                {                    
                    if (Array.IndexOf(dolukoltukdizi, item) == -1)
                    {
                        Label koltukara = this.Controls.Find("koltuk" + item.ToString(), true).FirstOrDefault() as Label;
                        if (koltukara != null)
                        {
                            //koltukara.Text += "\r" + adsoyad;
                            dolukoltuk++;
                            boskoltuk--;

                            Array.Resize(ref dolukoltukdizi, dolukoltukdizi.Length + 1);
                            dolukoltukdizi[sayac] = Convert.ToInt32(item);
                            sayac++;

                            lblDolu.Text = dolukoltuk.ToString();
                            lblBoş.Text = boskoltuk.ToString();
                        }
                    }
                }                
                if (item == "1") { dolukoltuklar(); koltuk1.BackColor = Color.IndianRed; }
                else if (item == "2") { dolukoltuklar(); koltuk2.BackColor = Color.IndianRed; }
                else if (item == "3") { dolukoltuklar(); koltuk3.BackColor = Color.IndianRed; }
                else if (item == "4") { dolukoltuklar(); koltuk4.BackColor = Color.IndianRed; }
                else if (item == "5") { dolukoltuklar(); koltuk5.BackColor = Color.IndianRed; }
                else if (item == "6") { dolukoltuklar(); koltuk6.BackColor = Color.IndianRed; }
                else if (item == "7") { dolukoltuklar(); koltuk7.BackColor = Color.IndianRed; }
                else if (item == "8") { dolukoltuklar(); koltuk8.BackColor = Color.IndianRed; }
                else if (item == "9") { dolukoltuklar(); koltuk9.BackColor = Color.IndianRed; }
                else if (item == "10") { dolukoltuklar(); koltuk10.BackColor = Color.IndianRed; }
                else if (item == "11") { dolukoltuklar(); koltuk11.BackColor = Color.IndianRed; }
                else if (item == "12") { dolukoltuklar(); koltuk12.BackColor = Color.IndianRed; }
                else if (item == "13") { dolukoltuklar(); koltuk13.BackColor = Color.IndianRed; }
                else if (item == "14") { dolukoltuklar(); koltuk14.BackColor = Color.IndianRed; }
                else if (item == "15") { dolukoltuklar(); koltuk15.BackColor = Color.IndianRed; }
                else if (item == "16") { dolukoltuklar(); koltuk16.BackColor = Color.IndianRed; }
                else if (item == "17") { dolukoltuklar(); koltuk17.BackColor = Color.IndianRed; }
                else if (item == "18") { dolukoltuklar(); koltuk18.BackColor = Color.IndianRed; }
                else if (item == "19") { dolukoltuklar(); koltuk19.BackColor = Color.IndianRed; }
                else if (item == "20") { dolukoltuklar(); koltuk20.BackColor = Color.IndianRed; }
                else if (item == "21") { dolukoltuklar(); koltuk21.BackColor = Color.IndianRed; }
                else if (item == "22") { dolukoltuklar(); koltuk22.BackColor = Color.IndianRed; }
                else if (item == "23") { dolukoltuklar(); koltuk23.BackColor = Color.IndianRed; }
                else if (item == "24") { dolukoltuklar(); koltuk24.BackColor = Color.IndianRed; }
                else if (item == "25") { dolukoltuklar(); koltuk25.BackColor = Color.IndianRed; }
                else if (item == "26") { dolukoltuklar(); koltuk26.BackColor = Color.IndianRed; }
                else if (item == "27") { dolukoltuklar(); koltuk27.BackColor = Color.IndianRed; }
                else if (item == "28") { dolukoltuklar(); koltuk28.BackColor = Color.IndianRed; }
                else if (item == "29") { dolukoltuklar(); koltuk29.BackColor = Color.IndianRed; }
                else if (item == "30") { dolukoltuklar(); koltuk30.BackColor = Color.IndianRed; }
                else if (item == "31") { dolukoltuklar(); koltuk31.BackColor = Color.IndianRed; }
                else if (item == "32") { dolukoltuklar(); koltuk32.BackColor = Color.IndianRed; }
                else if (item == "33") { dolukoltuklar(); koltuk33.BackColor = Color.IndianRed; }
                else if (item == "34") { dolukoltuklar(); koltuk34.BackColor = Color.IndianRed; }
                else if (item == "35") { dolukoltuklar(); koltuk35.BackColor = Color.IndianRed; }
            }
        }  
        void YenidenRenklendir()
        {            
            FrmRezervasyon rezervasyon = new FrmRezervasyon();
            foreach (Control item in rezervasyon.Controls)
            {                
                koltuk1.BackColor = Color.GreenYellow;
                koltuk2.BackColor = Color.GreenYellow;                
                koltuk3.BackColor = Color.GreenYellow;
                koltuk4.BackColor = Color.GreenYellow;
                koltuk5.BackColor = Color.GreenYellow;
                koltuk6.BackColor = Color.GreenYellow;
                koltuk7.BackColor = Color.GreenYellow;
                koltuk8.BackColor = Color.GreenYellow;
                koltuk9.BackColor = Color.GreenYellow;
                koltuk10.BackColor = Color.GreenYellow;
                koltuk11.BackColor = Color.GreenYellow;
                koltuk12.BackColor = Color.GreenYellow;
                koltuk13.BackColor = Color.GreenYellow;
                koltuk14.BackColor = Color.GreenYellow;
                koltuk15.BackColor = Color.GreenYellow;
                koltuk16.BackColor = Color.GreenYellow;
                koltuk17.BackColor = Color.GreenYellow;
                koltuk18.BackColor = Color.GreenYellow;
                koltuk19.BackColor = Color.GreenYellow;
                koltuk20.BackColor = Color.GreenYellow;
                koltuk21.BackColor = Color.GreenYellow;
                koltuk22.BackColor = Color.GreenYellow;
                koltuk23.BackColor = Color.GreenYellow;
                koltuk24.BackColor = Color.GreenYellow;
                koltuk25.BackColor = Color.GreenYellow;
                koltuk26.BackColor = Color.GreenYellow;
                koltuk27.BackColor = Color.GreenYellow;
                koltuk28.BackColor = Color.GreenYellow;
                koltuk29.BackColor = Color.GreenYellow;
                koltuk30.BackColor = Color.GreenYellow;
                koltuk31.BackColor = Color.GreenYellow;
                koltuk32.BackColor = Color.GreenYellow;
                koltuk33.BackColor = Color.GreenYellow;
                koltuk34.BackColor = Color.GreenYellow;
                koltuk35.BackColor = Color.GreenYellow;               
            }           
        }
    }
}
