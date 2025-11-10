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
    public partial class Them_doc_gia : Form
    {
        public Them_doc_gia()
        {
            InitializeComponent();
        }

        private void lab_themdocgia_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_huy_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hủy thêm độc giả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {


            //MessageBox.Show("Thêm độc giả thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //this.Close();
        }
    }
}
