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
    public partial class xulyvipham : Form
    {
        public xulyvipham()
        {
            InitializeComponent();
        }

        private void btn_thongtin_Click(object sender, EventArgs e)
        {
            THONGTINVIPHAM thongtinvipham = new THONGTINVIPHAM();
            thongtinvipham.Show();

        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
