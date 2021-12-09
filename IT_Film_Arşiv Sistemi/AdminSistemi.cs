using IT_Film_Arşiv_Sistemi.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IT_Film_Arşiv_Sistemi
{
    public partial class AdminSistemi : Form
    {
        List<Archive> archives = new List<Archive>();
        public AdminSistemi()
        {
            InitializeComponent();
        }
        public string a, b, c, d, k,f;

        SinemaOtomasyon db = new SinemaOtomasyon();
        
            
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            //Archive archive = new Archive { Director = txtDirector.Text, FilmCategory = cmbFilmCategory.Text, FilmName = txtFilmName.Text, FilmTime = txtFilmTime.Text, Menşei = cmbMenşei.Text, VisionDate = dtpVisionDate.Value,FilmID=archives.Count };
            //archives.Add(archive);

            //FrmRezervasyon rezervasyon = new FrmRezervasyon();
            //rezervasyon.dgwfilmsec.DataSource = archives;
            ////rezervasyon.dgwfilmsec.Visible = false;       
            //rezervasyon.Show();

            int? filmturuID = null, ulkeID = null;

            if (Convert.ToInt32(cmbFilmCategory.SelectedValue) != 0) filmturuID = Convert.ToInt32(cmbFilmCategory.SelectedValue);
            if (Convert.ToInt32(cmbMenşei.SelectedValue) != 0) ulkeID = Convert.ToInt32(cmbMenşei.SelectedValue);

            db.SpFilmKaydet(txtFilmName.Text, txtDirector.Text, filmturuID, txtFilmTime.Text, ulkeID, dtpVisionDate.Value);
            //db.Films.Add(new Film
            //{
            //    Film_Adı = txtFilmName.Text,
            //    FilmTürüID = filmturuID,
            //    FilmSüresi = txtFilmTime.Text,
            //    Yönetmen = txtDirector.Text,
            //    ÜlkeID = ulkeID,
            //    Vizyon_Tarihi = dtpVisionDate.Value
            //});
            db.SaveChanges();
            Listele(db.Films.ToList());           
            txtDirector.Text = "";
            cmbFilmCategory.SelectedIndex = -1;
            txtFilmName.Text = "";
            txtFilmTime.ForeColor = Color.DarkRed;
            txtFilmTime.Text = "";
            cmbMenşei.Text = "";
            dtpVisionDate.Value = DateTime.Now;
            txtFilmName.Focus();
            MessageBox.Show("Film Başarıyla Kaydedilmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        //private void cmbFilmCategory_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    // db.Films.Add(new Film { FilmTürüID = Convert.ToInt32(cmbFilmCategory.SelectedValue) });

        //    //int secilifilmID = Convert.ToInt32(cmbFilmCategory.SelectedValue);
        //    //List<FilmTürü> filmtürüs = db.FilmTürü.Where(x => x.ID == secilifilmID).ToList();
        //    //if (filmtürüs.Any()) cmbFilmCategory.DataSource = filmtürüs;
        //    //else cmbFilmCategory.DataSource = (new List<FilmTürü> { new FilmTürü { ID = 0, FilmTur_Adı = "Film Türü Bulunamadı." } }).ToList();

        //    if (cmbFilmCategory.SelectedIndex != -1)
        //    {
        //        var filmcategory = from fg in db.FilmTürü
        //                           where fg.ID == Convert.ToInt32(cmbMenşei.SelectedValue)
        //                           select fg;
        //        cmbMenşei.DataSource = filmcategory;
        //    }
        //}

        //private void cmbMenşei_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    // db.Films.Add(new Film { ÜlkeID = Convert.ToInt32(cmbMenşei.SelectedValue) });

        //    //int seciliUlkeID = Convert.ToInt32(cmbMenşei.SelectedValue);
        //    //List<Ülke> ülkeler = db.Ülke.Where(x => x.ID == seciliUlkeID).ToList();
        //    //if(ülkeler.Any()) cmbMenşei.DataSource = ülkeler;
        //    //else cmbMenşei.DataSource = (new List<Ülke> { new Ülke { ID = 0, Ülke_Adı = "Ülke Bulunamadı." } }).ToList();

        //    if (cmbMenşei.SelectedIndex != -1)
        //    {
        //        var ulke = from u in db.Ülke
        //                   where u.ID == Convert.ToInt32(cmbMenşei.SelectedValue)
        //                   select u;
        //        cmbMenşei.DataSource = ulke;
        //    }
        //}
        public void Listele(List<Film> filmler)
        {
           // dgwFİLM.DataSource = null;
            if (filmler.Count > 0)
            {
                dgwFİLM.DataSource = db.SpFilmListele().ToList();
                dgwFİLM.Columns["ID"].Visible = false;
                dgwFİLM.Columns["Film_Adı"].HeaderText = "Film Adı";
                dgwFİLM.Columns["Film_Türü"].HeaderText = "Film Türü";
                dgwFİLM.Columns["Film_Süresi"].HeaderText = "Film Süresi";
                dgwFİLM.Columns["Vizyon_Tarihi"].HeaderText = "Vizyon Tarihi";              

                //dgwFİLM.DataSource = archives;
                //dgwFİLM.Columns["FilmID"].Visible = false;
                //dgwFİLM.Columns["FilmName"].HeaderText = "FİLM ADI";
                //dgwFİLM.Columns["FilmCategory"].HeaderText = "FİLM TÜRÜ";
                //dgwFİLM.Columns["FilmTime"].HeaderText = "FİLM SÜRESİ";
                //dgwFİLM.Columns["Director"].HeaderText = "YÖNETMEN";
                //dgwFİLM.Columns["Menşei"].HeaderText = "YAPILDIĞI ÜLKE";
                //dgwFİLM.Columns["VisionDate"].HeaderText = "VİZYON TARİHİ";
                //dgwFİLM.ContextMenuStrip = contextMenuStrip1;
            }
            else dgwFİLM.ContextMenuStrip = null;
        }
      

    
        private void ArsivSistemi_Load(object sender, EventArgs e)
        {
            Listele(db.Films.ToList());
            cmbFilmCategory.DataSource = db.FilmTürü.ToList();
            cmbMenşei.DataSource = db.Ülke.ToList();
        }

        private void btnFilmSil_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bu veriyi silmek istediğinizden emin misiniz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result==DialogResult.Yes)
            {
                int seciliID = Convert.ToInt32(dgwFİLM.CurrentRow.Cells["ID"].Value);
                db.SpFilmSil(seciliID);
                db.SaveChanges();
                Listele(db.Films.ToList());
                MessageBox.Show("Film Başarıyla Silinmiştir.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //Archive seciliarchive = archives.First(x => x.FilmID == id);
                //archives.Remove(seciliarchive);
            }
        }

        private void düzenleyiciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int seciliID = Convert.ToInt32(dgwFİLM.CurrentRow.Cells["ID"].Value);
            Film secilifilm = db.Films.First(d => d.ID == seciliID); 

            txtFilmName.Text = secilifilm.Film_Adı;           
            txtFilmTime.Text = secilifilm.FilmSüresi;
            txtDirector.Text = secilifilm.Yönetmen;            
            dtpVisionDate.Value = secilifilm.Vizyon_Tarihi.Value;
            if (secilifilm.FilmTürüID != null && secilifilm.ÜlkeID != null)
            {
                cmbFilmCategory.SelectedValue = secilifilm.FilmTürüID;
                cmbMenşei.SelectedValue = secilifilm.ÜlkeID;
            }
            else
            {
                cmbFilmCategory.DataSource = (new List<FilmTürü> { new FilmTürü { ID = 0, FilmTur_Adı = "Film Türü Bulunamadı." } }).ToList();
                cmbMenşei.DataSource = (new List<Ülke> { new Ülke { ID = 0, Ülke_Adı = "Ülke Bulunamadı." } }).ToList();
            }
            //Archive seciliarchive = archives.First(x => x.FilmID == id);
        }

        private void btnFilmGüncelle_Click(object sender, EventArgs e)
        {
            int? filmturuID = null, ulkeID = null;

            if (Convert.ToInt32(cmbFilmCategory.SelectedValue) != 0) filmturuID = Convert.ToInt32(cmbFilmCategory.SelectedValue);
            if (Convert.ToInt32(cmbMenşei.SelectedValue) != 0) ulkeID = Convert.ToInt32(cmbMenşei.SelectedValue);

            int seciliID = Convert.ToInt32(dgwFİLM.CurrentRow.Cells["ID"].Value);
            db.SpFilmGüncelle(seciliID, txtFilmName.Text, txtDirector.Text, filmturuID, txtFilmTime.Text, ulkeID, dtpVisionDate.Value);
            db.SaveChanges();
            Listele(db.Films.ToList());

            txtFilmName.Text = "";
            cmbFilmCategory.Text = "";
            txtFilmTime.Text = "";
            txtDirector.Text = "";
            cmbMenşei.SelectedIndex = -1;
            dtpVisionDate.Value = DateTime.Now;

            //Archive seciliarchive = archives.First(x => x.FilmID == id);
            //seciliarchive.FilmID = id;
            //seciliarchive.FilmName = txtFilmName.Text;
            //seciliarchive.FilmCategory = cmbFilmCategory.Text;
            //seciliarchive.FilmTime = txtFilmTime.Text;
            //seciliarchive.Director = txtDirector.Text;
            //seciliarchive.Menşei = cmbMenşei.Text;
            //seciliarchive.VisionDate = dtpVisionDate.Value;
            //archives.Remove(seciliarchive);
            //archives.Add(seciliarchive);
        }
     
        private void menüRengiDegiştirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color[] renkler = new Color[10] { Color.LightCoral, Color.Teal, Color.Khaki, Color.LawnGreen, Color.Aqua, Color.LightPink, Color.Maroon, Color.Magenta, Color.DarkGray, Color.SandyBrown };
            Random rnd = new Random();
            int dizi_elemani = rnd.Next(1, 10);
            this.BackColor = renkler[dizi_elemani];
        }

        private void hakkımızdaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.dgwFİLM.Dock = DockStyle.Fill;
            this.WindowState = FormWindowState.Maximized;           
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bu proje Muhammet Numan Sungur tarafından 11 Haziran 2020 tarihinde kodlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void çıkışToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void kucükEkranToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void btnev_Click(object sender, EventArgs e)
        {
            FrmAnasayfa anasayfa = new FrmAnasayfa();
            anasayfa.Show();
            this.Hide();
        }
    }
}
