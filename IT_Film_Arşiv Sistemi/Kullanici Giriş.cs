using IT_Film_Arşiv_Sistemi.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IT_Film_Arşiv_Sistemi
{
    public partial class FrmKullaniciGiriş : Form
    {
        
        public FrmKullaniciGiriş()
        {
            InitializeComponent();
        }
        SinemaOtomasyon db = new SinemaOtomasyon();
        private void btnSistemeGiriş_Click(object sender, EventArgs e)
        {
            bool Kullanıcı(string kullanıcıAd,string kullanıcıSifre)
            {
                var sorgu = from m in db.KullanıcıGiris where m.Musteri_Adı_Soyadı == kullanıcıAd && m.sifre == kullanıcıSifre select m;
                if (sorgu.Any())
                {
                    return true;
                }
                else
                {
                    return false;    
                }
            }

            if (Kullanıcı(txtUserName.Text.ToLower(),txtPassword.Text.ToLower()))
            {
                MessageBox.Show("Sayın " + CultureInfo.CurrentCulture.TextInfo.ToTitleCase((string)txtUserName.Text) + " Hoşgeldiniz");
                FrmRezervasyon frmbilet = new FrmRezervasyon();
                frmbilet.adsoyad = CultureInfo.CurrentCulture.TextInfo.ToTitleCase((string)txtUserName.Text);
                frmbilet.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Geçersiz Kullanıcı adı veya şifre...");
                txtUserName.Text = "";
                txtPassword.Text = "";
            }

            //User user = new User();
            //if (txtUserName.Text == user.UserName && txtPassword.Text == user.Password)
            //{
            //    MessageBox.Show("Hoşgeldiniz");
            //    FrmRezervasyon frmbilet = new FrmRezervasyon();
            //    frmbilet.adsoyad = txtUserName.Text;                            
            //    frmbilet.Show();
            //    this.Hide();
            //}
            //else
            //{
            //    MessageBox.Show("Geçersiz kullanıcı adı veya şifre...");
            //    txtUserName.Text = "";
            //    txtPassword.Text = "";
            //}
        }
    }
}
