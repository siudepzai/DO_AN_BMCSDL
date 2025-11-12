namespace DO_AN_BMCSDL.Phan_GUI
{
    partial class xulyvipham
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(xulyvipham));
            this.menuHideTimer = new System.Windows.Forms.Timer(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.lab_QLdocgia = new System.Windows.Forms.Label();
            this.lab_ = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.logo = new System.Windows.Forms.PictureBox();
            this.pnl_thanhquanly = new System.Windows.Forms.Panel();
            this.btn_thoat = new System.Windows.Forms.Button();
            this.btn_thongtin = new System.Windows.Forms.Button();
            this.btn_xoa = new System.Windows.Forms.Button();
            this.dgv_thongtinvipham = new System.Windows.Forms.DataGridView();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.pnl_thanhquanly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_thongtinvipham)).BeginInit();
            this.SuspendLayout();
            // 
            // menuHideTimer
            // 
            this.menuHideTimer.Interval = 1000;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.panel3.Controls.Add(this.lab_QLdocgia);
            this.panel3.Location = new System.Drawing.Point(3, 96);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1186, 42);
            this.panel3.TabIndex = 18;
            // 
            // lab_QLdocgia
            // 
            this.lab_QLdocgia.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lab_QLdocgia.Location = new System.Drawing.Point(3, 1);
            this.lab_QLdocgia.Name = "lab_QLdocgia";
            this.lab_QLdocgia.Size = new System.Drawing.Size(287, 41);
            this.lab_QLdocgia.TabIndex = 14;
            this.lab_QLdocgia.Text = "QUẢN LÝ ĐỌC GIẢ";
            this.lab_QLdocgia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lab_
            // 
            this.lab_.Dock = System.Windows.Forms.DockStyle.Right;
            this.lab_.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lab_.Location = new System.Drawing.Point(600, 0);
            this.lab_.Name = "lab_";
            this.lab_.Size = new System.Drawing.Size(583, 90);
            this.lab_.TabIndex = 2;
            this.lab_.Text = " HỌC HẾT SỨC-CHƠI HẾT MÌNH";
            this.lab_.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(3, 93);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1097, 64);
            this.panel2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel1.Controls.Add(this.lab_);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.logo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1183, 90);
            this.panel1.TabIndex = 17;
            // 
            // logo
            // 
            this.logo.ErrorImage = ((System.Drawing.Image)(resources.GetObject("logo.ErrorImage")));
            this.logo.Image = global::DO_AN_BMCSDL.Properties.Resources.Logo_1;
            this.logo.InitialImage = global::DO_AN_BMCSDL.Properties.Resources.Logo_1;
            this.logo.Location = new System.Drawing.Point(3, 3);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(490, 84);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logo.TabIndex = 0;
            this.logo.TabStop = false;
            // 
            // pnl_thanhquanly
            // 
            this.pnl_thanhquanly.Controls.Add(this.btn_thoat);
            this.pnl_thanhquanly.Controls.Add(this.btn_thongtin);
            this.pnl_thanhquanly.Controls.Add(this.btn_xoa);
            this.pnl_thanhquanly.Location = new System.Drawing.Point(3, 155);
            this.pnl_thanhquanly.Name = "pnl_thanhquanly";
            this.pnl_thanhquanly.Size = new System.Drawing.Size(1173, 67);
            this.pnl_thanhquanly.TabIndex = 19;
            // 
            // btn_thoat
            // 
            this.btn_thoat.BackColor = System.Drawing.Color.Cyan;
            this.btn_thoat.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_thoat.Location = new System.Drawing.Point(734, 9);
            this.btn_thoat.Name = "btn_thoat";
            this.btn_thoat.Size = new System.Drawing.Size(190, 45);
            this.btn_thoat.TabIndex = 0;
            this.btn_thoat.Text = "Thoát";
            this.btn_thoat.UseVisualStyleBackColor = false;
            this.btn_thoat.Click += new System.EventHandler(this.btn_thoat_Click);
            // 
            // btn_thongtin
            // 
            this.btn_thongtin.BackColor = System.Drawing.Color.Cyan;
            this.btn_thongtin.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_thongtin.Location = new System.Drawing.Point(485, 9);
            this.btn_thongtin.Name = "btn_thongtin";
            this.btn_thongtin.Size = new System.Drawing.Size(190, 45);
            this.btn_thongtin.TabIndex = 0;
            this.btn_thongtin.Text = "Thông tin";
            this.btn_thongtin.UseVisualStyleBackColor = false;
            this.btn_thongtin.Click += new System.EventHandler(this.btn_thongtin_Click);
            // 
            // btn_xoa
            // 
            this.btn_xoa.BackColor = System.Drawing.Color.Cyan;
            this.btn_xoa.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_xoa.Location = new System.Drawing.Point(226, 9);
            this.btn_xoa.Name = "btn_xoa";
            this.btn_xoa.Size = new System.Drawing.Size(190, 45);
            this.btn_xoa.TabIndex = 0;
            this.btn_xoa.Text = "Xóa";
            this.btn_xoa.UseVisualStyleBackColor = false;
            this.btn_xoa.Click += new System.EventHandler(this.btn_xoa_Click_1);
            // 
            // dgv_thongtinvipham
            // 
            this.dgv_thongtinvipham.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_thongtinvipham.Location = new System.Drawing.Point(6, 253);
            this.dgv_thongtinvipham.Name = "dgv_thongtinvipham";
            this.dgv_thongtinvipham.RowHeadersWidth = 51;
            this.dgv_thongtinvipham.RowTemplate.Height = 24;
            this.dgv_thongtinvipham.Size = new System.Drawing.Size(1170, 366);
            this.dgv_thongtinvipham.TabIndex = 20;
            // 
            // xulyvipham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 619);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnl_thanhquanly);
            this.Controls.Add(this.dgv_thongtinvipham);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "xulyvipham";
            this.Text = "xulyvipham";
            this.Load += new System.EventHandler(this.xulyvipham_Load);
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.pnl_thanhquanly.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_thongtinvipham)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer menuHideTimer;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lab_QLdocgia;
        private System.Windows.Forms.Label lab_;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.Panel pnl_thanhquanly;
        private System.Windows.Forms.Button btn_thoat;
        private System.Windows.Forms.Button btn_thongtin;
        private System.Windows.Forms.Button btn_xoa;
        private System.Windows.Forms.DataGridView dgv_thongtinvipham;
    }
}