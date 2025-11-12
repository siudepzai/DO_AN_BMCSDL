using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DO_AN_BMCSDL.Phan_GUI
{
    public partial class FormMuonPhong : Form
    {
        public FormMuonPhong()
        {
            InitializeComponent();
        }

        private void btnDangKyMuon_Click(object sender, EventArgs e)
        {
            FormThongBaoDangKy formThongBaoDangKy = new FormThongBaoDangKy();
            formThongBaoDangKy.FormClosed += (s, args) => this.Show();
            formThongBaoDangKy.Show();
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
