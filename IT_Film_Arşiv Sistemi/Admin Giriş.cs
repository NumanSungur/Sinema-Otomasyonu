using IT_Film_Arşiv_Sistemi.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using System.Globalization;

namespace IT_Film_Arşiv_Sistemi
{
    public partial class Admin_Giriş : Form
    {
        public Admin_Giriş()
        {
            InitializeComponent();
        }
        SinemaOtomasyon db = new SinemaOtomasyon();
        private void btnSistemeGiriş_Click(object sender, EventArgs e)
        {
            AdminGiri adminGiris = new AdminGiri();
            bool admin(string ad, string sifre)
            {
                var sorgu = from a in db.AdminGiris where a.Admin_Adı == ad && a.Sifre == sifre select a;
                if (sorgu.Any())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            if (admin(txtUserName.Text.ToLower(), txtPassword.Text.ToLower()))
            {
                MessageBox.Show("Sayın " + CultureInfo.CurrentCulture.TextInfo.ToTitleCase((string)txtUserName.Text) + "  Hoşgeldiniz");
                AdminSistemi frmArsiv = new AdminSistemi();
                frmArsiv.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Geçersiz Admin adı veya şifre...");
                txtUserName.Text = "";
                txtPassword.Text = "";
            }
           

            //User user = new User();
            //if (txtUserName.Text == user.Name && txtPassword.Text == user.Password)
            //{
            //    MessageBox.Show("Sayın " + user.Name + " Hoşgeldiniz");
            //    AdminSistemi frmArsiv = new AdminSistemi();
            //    frmArsiv.Show();
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
