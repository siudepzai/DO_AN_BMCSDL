namespace DO_AN_BMCSDL.Phan_GUI
{
    partial class themtailieu_thanhly
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
            this.lab_tailieuthanhly = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txt_ngay = new System.Windows.Forms.TextBox();
            this.txt_ghichu = new System.Windows.Forms.TextBox();
            this.txt_maphieu = new System.Windows.Forms.TextBox();
            this.lab_ghichu = new System.Windows.Forms.Label();
            this.lab_ngaylapphieu = new System.Windows.Forms.Label();
            this.lab_maphieu = new System.Windows.Forms.Label();
            this.btn_huy = new System.Windows.Forms.Button();
            this.btn_luu = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lab_tailieuthanhly
            // 
            this.lab_tailieuthanhly.Dock = System.Windows.Forms.DockStyle.Top;
            this.lab_tailieuthanhly.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_tailieuthanhly.Location = new System.Drawing.Point(0, 0);
            this.lab_tailieuthanhly.Name = "lab_tailieuthanhly";
            this.lab_tailieuthanhly.Size = new System.Drawing.Size(990, 69);
            this.lab_tailieuthanhly.TabIndex = 0;
            this.lab_tailieuthanhly.Text = "THÊM TẠI LIỆU THANH LÝ";
            this.lab_tailieuthanhly.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(12, 114);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(966, 338);
            this.panel1.TabIndex = 1;
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
            this.panel2.Location = new System.Drawing.Point(21, 58);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(925, 219);
            this.panel2.TabIndex = 1;
            // 
            // txt_ngay
            // 
            this.txt_ngay.Location = new System.Drawing.Point(613, 54);
            this.txt_ngay.Name = "txt_ngay";
            this.txt_ngay.Size = new System.Drawing.Size(297, 30);
            this.txt_ngay.TabIndex = 1;
            // 
            // txt_ghichu
            // 
            this.txt_ghichu.Location = new System.Drawing.Point(103, 90);
            this.txt_ghichu.Multiline = true;
            this.txt_ghichu.Name = "txt_ghichu";
            this.txt_ghichu.Size = new System.Drawing.Size(807, 79);
            this.txt_ghichu.TabIndex = 2;
            // 
            // txt_maphieu
            // 
            this.txt_maphieu.Location = new System.Drawing.Point(104, 54);
            this.txt_maphieu.Name = "txt_maphieu";
            this.txt_maphieu.Size = new System.Drawing.Size(356, 30);
            this.txt_maphieu.TabIndex = 0;
            // 
            // lab_ghichu
            // 
            this.lab_ghichu.AutoSize = true;
            this.lab_ghichu.Location = new System.Drawing.Point(16, 93);
            this.lab_ghichu.Name = "lab_ghichu";
            this.lab_ghichu.Size = new System.Drawing.Size(83, 23);
            this.lab_ghichu.TabIndex = 0;
            this.lab_ghichu.Text = "Ghi chú:";
            // 
            // lab_ngaylapphieu
            // 
            this.lab_ngaylapphieu.AutoSize = true;
            this.lab_ngaylapphieu.Location = new System.Drawing.Point(466, 54);
            this.lab_ngaylapphieu.Name = "lab_ngaylapphieu";
            this.lab_ngaylapphieu.Size = new System.Drawing.Size(141, 23);
            this.lab_ngaylapphieu.TabIndex = 0;
            this.lab_ngaylapphieu.Text = "Ngày lập phiếu:";
            // 
            // lab_maphieu
            // 
            this.lab_maphieu.AutoSize = true;
            this.lab_maphieu.Location = new System.Drawing.Point(4, 54);
            this.lab_maphieu.Name = "lab_maphieu";
            this.lab_maphieu.Size = new System.Drawing.Size(95, 23);
            this.lab_maphieu.TabIndex = 0;
            this.lab_maphieu.Text = "Mã phiếu:";
            // 
            // btn_huy
            // 
            this.btn_huy.BackColor = System.Drawing.Color.IndianRed;
            this.btn_huy.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_huy.Location = new System.Drawing.Point(865, 488);
            this.btn_huy.Name = "btn_huy";
            this.btn_huy.Size = new System.Drawing.Size(93, 32);
            this.btn_huy.TabIndex = 3;
            this.btn_huy.Text = "Hủy bỏ";
            this.btn_huy.UseVisualStyleBackColor = false;
            this.btn_huy.Click += new System.EventHandler(this.btn_huy_Click_1);
            // 
            // btn_luu
            // 
            this.btn_luu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn_luu.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_luu.Location = new System.Drawing.Point(751, 488);
            this.btn_luu.Name = "btn_luu";
            this.btn_luu.Size = new System.Drawing.Size(93, 32);
            this.btn_luu.TabIndex = 4;
            this.btn_luu.Text = "Lưu";
            this.btn_luu.UseVisualStyleBackColor = false;
            this.btn_luu.Click += new System.EventHandler(this.btn_luu_Click);
            // 
            // themtailieu_thanhly
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 528);
            this.Controls.Add(this.btn_huy);
            this.Controls.Add(this.btn_luu);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lab_tailieuthanhly);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "themtailieu_thanhly";
            this.Text = "themtailieu_thanhly";
            this.Load += new System.EventHandler(this.themtailieu_thanhly_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lab_tailieuthanhly;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txt_ngay;
        private System.Windows.Forms.TextBox txt_ghichu;
        private System.Windows.Forms.TextBox txt_maphieu;
        private System.Windows.Forms.Label lab_ghichu;
        private System.Windows.Forms.Label lab_ngaylapphieu;
        private System.Windows.Forms.Label lab_maphieu;
        private System.Windows.Forms.Button btn_huy;
        private System.Windows.Forms.Button btn_luu;
    }
}