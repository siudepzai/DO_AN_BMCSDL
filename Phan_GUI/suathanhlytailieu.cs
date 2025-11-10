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
    public partial class suathanhlytailieu : Form
    {
        public suathanhlytailieu()
        {
            InitializeComponent();
        }

        private void btn_them_Click(object sender, EventArgs e)
        {

        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Lưu tài liệu thanh lý thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btn_huy_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hủy lưu tài liệu thanh lý thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
