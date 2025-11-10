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
    public partial class baocao : Form
    {
        public baocao()
        {
            InitializeComponent();
        }

        private void cb_luacho_SelectedIndexChanged(object sender, EventArgs e)
        {
            string luaChon = cb_luacho.SelectedItem.ToString();

            switch (luaChon)
            {
                case "Tất cả dữ liệu":
                    LoadBaoCaoThongKe("ALL");
                    break;

                case "Dữ liệu theo tháng":
                    // Bạn có thể mở thêm một Dialog để chọn tháng cụ thể
                    LoadBaoCaoThongKe("MONTHLY");
                    break;

                case "Dữ liệu theo quý":
                    LoadBaoCaoThongKe("QUARTERLY");
                    break;

                case "Dữ liệu theo năm":
                    // Bạn có thể mở thêm một Dialog để chọn năm cụ thể
                    LoadBaoCaoThongKe("YEARLY");
                    break;

                default:
                    // Xử lý mặc định
                    break;
            }
        }
        private void LoadBaoCaoThongKe(string scope)
        {
            // Đây là nơi bạn viết code kết nối CSDL và truy vấn dữ liệu thống kê
            // Dựa vào tham số 'scope' ('ALL', 'MONTHLY', 'QUARTERLY', 'YEARLY') 
            // để xây dựng câu lệnh SQL có điều kiện WHERE (ví dụ: WHERE [thời gian] >= [ngày bắt đầu] AND [thời gian] <= [ngày kết thúc])

            // Sau khi có DataTable từ CSDL, bạn gán nó vào DataGridView hiển thị báo cáo.
            // dgvBaoCao.DataSource = yourDataTable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
