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
    public partial class FormDangKyTheMoi : Form
    {
        public FormDangKyTheMoi()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDangKyThe_Click(object sender, EventArgs e)
        {
            lblDangKyThanhCong.Visible = true;
        }

        private void lblDangKyThanhCong_Click(object sender, EventArgs e)
        {

        }
    }
}
