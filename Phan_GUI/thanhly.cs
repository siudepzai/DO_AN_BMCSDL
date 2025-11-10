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
    public partial class thanhly : Form
    {
        public thanhly()
        {
            InitializeComponent();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            themtailieu_thanhly themtailieu_Thanhly = new themtailieu_thanhly();
            themtailieu_Thanhly.Show();
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            suathanhlytailieu suathanhlyTailieu = new suathanhlytailieu();
            suathanhlyTailieu.Show();
        }
    }
}
