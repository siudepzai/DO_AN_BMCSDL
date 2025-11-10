namespace DO_AN_BMCSDL.Phan_GUI
{
    partial class suathanhlytailieu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_xoa = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_TK = new System.Windows.Forms.Button();
            this.txt_timkiem = new System.Windows.Forms.TextBox();
            this.btn_them = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txt_ngay = new System.Windows.Forms.TextBox();
            this.txt_ghichu = new System.Windows.Forms.TextBox();
            this.txt_maphieu = new System.Windows.Forms.TextBox();
            this.lab_ghichu = new System.Windows.Forms.Label();
            this.lab_ngaylapphieu = new System.Windows.Forms.Label();
            this.lab_maphieu = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lab_tailieuthanhly = new System.Windows.Forms.Label();
            this.btn_huy = new System.Windows.Forms.Button();
            this.btn_luu = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_xoa
            // 
            this.btn_xoa.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btn_xoa.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_xoa.Location = new System.Drawing.Point(135, 226);
            this.btn_xoa.Name = "btn_xoa";
            this.btn_xoa.Size = new System.Drawing.Size(96, 30);
            this.btn_xoa.TabIndex = 2;
            this.btn_xoa.Text = "Xóa";
            this.btn_xoa.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.Controls.Add(this.btn_TK);
            this.panel1.Controls.Add(this.txt_timkiem);
            this.panel1.Controls.Add(this.btn_xoa);
            this.panel1.Controls.Add(this.btn_them);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(12, 72);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(966, 489);
            this.panel1.TabIndex = 6;
            // 
            // btn_TK
            // 
            this.btn_TK.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btn_TK.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_TK.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_TK.Location = new System.Drawing.Point(739, 25);
            this.btn_TK.Name = "btn_TK";
            this.btn_TK.Size = new System.Drawing.Size(176, 32);
            this.btn_TK.TabIndex = 17;
            this.btn_TK.Text = "Tìm";
            this.btn_TK.UseVisualStyleBackColor = false;
            // 
            // txt_timkiem
            // 
            this.txt_timkiem.Location = new System.Drawing.Point(124, 27);
            this.txt_timkiem.Name = "txt_timkiem";
            this.txt_timkiem.Size = new System.Drawing.Size(618, 30);
            this.txt_timkiem.TabIndex = 16;
            // 
            // btn_them
            // 
            this.btn_them.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btn_them.Location = new System.Drawing.Point(21, 226);
            this.btn_them.Name = "btn_them";
            this.btn_them.Size = new System.Drawing.Size(96, 30);
            this.btn_them.TabIndex = 2;
            this.btn_them.Text = "Thêm";
            this.btn_them.UseVisualStyleBackColor = false;
            this.btn_them.Click += new System.EventHandler(this.btn_them_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.txt_ngay);
            this.panel2.Controls.Add(this.txt_ghichu);
            this.panel2.Controls.Add(this.txt_maphieu);
            this.panel2.Controls.Add(this.lab_ghichu);
            this.panel2.Controls.Add(this.lab_ngaylapphieu);
            this.panel2.Controls.Add(this.lab_maphieu);
            this.panel2.Location = new System.Drawing.Point(21, 75);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(925, 145);
            this.panel2.TabIndex = 1;
            // 
            // txt_ngay
            // 
            this.txt_ngay.Location = new System.Drawing.Point(612, 20);
            this.txt_ngay.Name = "txt_ngay";
            this.txt_ngay.Size = new System.Drawing.Size(297, 30);
            this.txt_ngay.TabIndex = 1;
            // 
            // txt_ghichu
            // 
            this.txt_ghichu.Location = new System.Drawing.Point(102, 56);
            this.txt_ghichu.Multiline = true;
            this.txt_ghichu.Name = "txt_ghichu";
            this.txt_ghichu.Size = new System.Drawing.Size(807, 79);
            this.txt_ghichu.TabIndex = 2;
            // 
            // txt_maphieu
            // 
            this.txt_maphieu.Location = new System.Drawing.Point(103, 20);
            this.txt_maphieu.Name = "txt_maphieu";
            this.txt_maphieu.Size = new System.Drawing.Size(356, 30);
            this.txt_maphieu.TabIndex = 0;
            // 
            // lab_ghichu
            // 
            this.lab_ghichu.AutoSize = true;
            this.lab_ghichu.Location = new System.Drawing.Point(15, 59);
            this.lab_ghichu.Name = "lab_ghichu";
            this.lab_ghichu.Size = new System.Drawing.Size(78, 22);
            this.lab_ghichu.TabIndex = 0;
            this.lab_ghichu.Text = "Ghi chú:";
            // 
            // lab_ngaylapphieu
            // 
            this.lab_ngaylapphieu.AutoSize = true;
            this.lab_ngaylapphieu.Location = new System.Drawing.Point(465, 20);
            this.lab_ngaylapphieu.Name = "lab_ngaylapphieu";
            this.lab_ngaylapphieu.Size = new System.Drawing.Size(135, 22);
            this.lab_ngaylapphieu.TabIndex = 0;
            this.lab_ngaylapphieu.Text = "Ngày lập phiếu:";
            // 
            // lab_maphieu
            // 
            this.lab_maphieu.AutoSize = true;
            this.lab_maphieu.Location = new System.Drawing.Point(3, 20);
            this.lab_maphieu.Name = "lab_maphieu";
            this.lab_maphieu.Size = new System.Drawing.Size(90, 22);
            this.lab_maphieu.TabIndex = 0;
            this.lab_maphieu.Text = "Mã phiếu:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(21, 255);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(925, 226);
            this.dataGridView1.TabIndex = 0;
            // 
            // lab_tailieuthanhly
            // 
            this.lab_tailieuthanhly.Dock = System.Windows.Forms.DockStyle.Top;
            this.lab_tailieuthanhly.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_tailieuthanhly.Location = new System.Drawing.Point(0, 0);
            this.lab_tailieuthanhly.Name = "lab_tailieuthanhly";
            this.lab_tailieuthanhly.Size = new System.Drawing.Size(992, 69);
            this.lab_tailieuthanhly.TabIndex = 5;
            this.lab_tailieuthanhly.Text = "THÊM TẠI LIỆU THANH LÝ";
            this.lab_tailieuthanhly.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_huy
            // 
            this.btn_huy.BackColor = System.Drawing.Color.IndianRed;
            this.btn_huy.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_huy.Location = new System.Drawing.Point(865, 567);
            this.btn_huy.Name = "btn_huy";
            this.btn_huy.Size = new System.Drawing.Size(93, 32);
            this.btn_huy.TabIndex = 7;
            this.btn_huy.Text = "Hủy bỏ";
            this.btn_huy.UseVisualStyleBackColor = false;
            this.btn_huy.Click += new System.EventHandler(this.btn_huy_Click);
            // 
            // btn_luu
            // 
            this.btn_luu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn_luu.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_luu.Location = new System.Drawing.Point(751, 567);
            this.btn_luu.Name = "btn_luu";
            this.btn_luu.Size = new System.Drawing.Size(93, 32);
            this.btn_luu.TabIndex = 8;
            this.btn_luu.Text = "Lưu";
            this.btn_luu.UseVisualStyleBackColor = false;
            this.btn_luu.Click += new System.EventHandler(this.btn_luu_Click);
            // 
            // suathanhlytailieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 619);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lab_tailieuthanhly);
            this.Controls.Add(this.btn_huy);
            this.Controls.Add(this.btn_luu);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "suathanhlytailieu";
            this.Text = "suathanhlytailieu";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_xoa;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_them;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txt_ngay;
        private System.Windows.Forms.TextBox txt_ghichu;
        private System.Windows.Forms.TextBox txt_maphieu;
        private System.Windows.Forms.Label lab_ghichu;
        private System.Windows.Forms.Label lab_ngaylapphieu;
        private System.Windows.Forms.Label lab_maphieu;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lab_tailieuthanhly;
        private System.Windows.Forms.Button btn_huy;
        private System.Windows.Forms.Button btn_luu;
        private System.Windows.Forms.Button btn_TK;
        private System.Windows.Forms.TextBox txt_timkiem;
    }
}