using DO_AN_BMCSDL.Phan_GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DO_AN_BMCSDL
{
    public partial class Create_key : Form
    {
        public Create_key()
        {
            InitializeComponent();
        }

        private void btn_taokhoa_Click(object sender, EventArgs e)
        {
            QL_tailieu qL_Tailieu = new QL_tailieu();
            Grey.Run();
            MessageBox.Show("Tạo khóa thành công! Vui lòng kiểm tra trong thư mục Debug của dự án.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
