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
    public partial class QL_Doc_Gia : Form
    {
        public QL_Doc_Gia()
        {

            {
                InitializeComponent();

            }
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            Them_doc_gia them_Doc_Gia = new Them_doc_gia();
            them_Doc_Gia.ShowDialog();

        }

        private void QL_Doc_Gia_Load(object sender, EventArgs e)
        {

        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}