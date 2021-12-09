using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IT_Film_Arşiv_Sistemi
{
    public partial class FrmAnasayfa : Form
    {
        public FrmAnasayfa()
        {
            InitializeComponent();
        }

        private void btnkullanici_Click(object sender, EventArgs e)
        {
            FrmKullaniciGiriş frmKullanici = new FrmKullaniciGiriş();
            frmKullanici.Show();
            this.Hide();
        }

        private void btnAdminİşlemleri_Click(object sender, EventArgs e)
        {
            Admin_Giriş admin_Giriş = new Admin_Giriş();
            admin_Giriş.Show();
            this.Hide();
        }

        private void btnÇikisYap_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
