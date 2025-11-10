namespace DO_AN_BMCSDL.Phan_GUI
{
    partial class QL_phong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QL_phong));
            this.btn_thoat = new System.Windows.Forms.Button();
            this.btn_TK = new System.Windows.Forms.Button();
            this.pnl_thanhquanly = new System.Windows.Forms.Panel();
            this.lab_muontra = new System.Windows.Forms.Label();
            this.txt_timkiem = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.menuHideTimer = new System.Windows.Forms.Timer(this.components);
            this.dataGrid_hocgia = new System.Windows.Forms.DataGridView();
            this.logo = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lab_ = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btn_thongtinphong = new System.Windows.Forms.Button();
            this.btn_tailieusach = new System.Windows.Forms.Button();
            this.pnl_thanhquanly.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_hocgia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_thoat
            // 
            this.btn_thoat.BackColor = System.Drawing.Color.RoyalBlue;
            this.btn_thoat.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_thoat.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_thoat.Location = new System.Drawing.Point(0, 0);
            this.btn_thoat.Name = "btn_thoat";
            this.btn_thoat.Size = new System.Drawing.Size(190, 67);
            this.btn_thoat.TabIndex = 0;
            this.btn_thoat.Text = "Thoát";
            this.btn_thoat.UseVisualStyleBackColor = false;
            this.btn_thoat.Click += new System.EventHandler(this.btn_thoat_Click);
            // 
            // btn_TK
            // 
            this.btn_TK.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btn_TK.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_TK.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_TK.Location = new System.Drawing.Point(1069, 26);
            this.btn_TK.Name = "btn_TK";
            this.btn_TK.Size = new System.Drawing.Size(97, 35);
            this.btn_TK.TabIndex = 15;
            this.btn_TK.Text = "Tìm";
            this.btn_TK.UseVisualStyleBackColor = false;
            // 
            // pnl_thanhquanly
            // 
            this.pnl_thanhquanly.Controls.Add(this.btn_thoat);
            this.pnl_thanhquanly.Location = new System.Drawing.Point(2, 140);
            this.pnl_thanhquanly.Name = "pnl_thanhquanly";
            this.pnl_thanhquanly.Size = new System.Drawing.Size(189, 67);
            this.pnl_thanhquanly.TabIndex = 29;
            // 
            // lab_muontra
            // 
            this.lab_muontra.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lab_muontra.Location = new System.Drawing.Point(3, 1);
            this.lab_muontra.Name = "lab_muontra";
            this.lab_muontra.Size = new System.Drawing.Size(287, 41);
            this.lab_muontra.TabIndex = 14;
            this.lab_muontra.Text = "QUẢN LÝ ĐỌC GIẢ";
            this.lab_muontra.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_timkiem
            // 
            this.txt_timkiem.Location = new System.Drawing.Point(613, 26);
            this.txt_timkiem.Multiline = true;
            this.txt_timkiem.Name = "txt_timkiem";
            this.txt_timkiem.Size = new System.Drawing.Size(459, 35);
            this.txt_timkiem.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.panel3.Controls.Add(this.lab_muontra);
            this.panel3.Location = new System.Drawing.Point(2, 92);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1186, 42);
            this.panel3.TabIndex = 28;
            // 
            // menuHideTimer
            // 
            this.menuHideTimer.Interval = 1000;
            // 
            // dataGrid_hocgia
            // 
            this.dataGrid_hocgia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_hocgia.Location = new System.Drawing.Point(107, 319);
            this.dataGrid_hocgia.Name = "dataGrid_hocgia";
            this.dataGrid_hocgia.RowHeadersWidth = 51;
            this.dataGrid_hocgia.RowTemplate.Height = 24;
            this.dataGrid_hocgia.Size = new System.Drawing.Size(993, 271);
            this.dataGrid_hocgia.TabIndex = 30;
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
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(3, 93);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1097, 64);
            this.panel2.TabIndex = 1;
            // 
            // lab_
            // 
            this.lab_.Dock = System.Windows.Forms.DockStyle.Right;
            this.lab_.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lab_.Location = new System.Drawing.Point(585, 0);
            this.lab_.Name = "lab_";
            this.lab_.Size = new System.Drawing.Size(583, 90);
            this.lab_.TabIndex = 2;
            this.lab_.Text = " HỌC HẾT SỨC-CHƠI HẾT MÌNH";
            this.lab_.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.panel1.Size = new System.Drawing.Size(1168, 90);
            this.panel1.TabIndex = 27;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btn_thongtinphong);
            this.panel4.Controls.Add(this.btn_tailieusach);
            this.panel4.Controls.Add(this.btn_TK);
            this.panel4.Controls.Add(this.txt_timkiem);
            this.panel4.Location = new System.Drawing.Point(-12, 233);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1177, 69);
            this.panel4.TabIndex = 31;
            // 
            // btn_thongtinphong
            // 
            this.btn_thongtinphong.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btn_thongtinphong.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_thongtinphong.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_thongtinphong.Location = new System.Drawing.Point(318, 15);
            this.btn_thongtinphong.Name = "btn_thongtinphong";
            this.btn_thongtinphong.Size = new System.Drawing.Size(280, 48);
            this.btn_thongtinphong.TabIndex = 16;
            this.btn_thongtinphong.Text = "Phòng";
            this.btn_thongtinphong.UseVisualStyleBackColor = false;
            // 
            // btn_tailieusach
            // 
            this.btn_tailieusach.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btn_tailieusach.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_tailieusach.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_tailieusach.Location = new System.Drawing.Point(71, 15);
            this.btn_tailieusach.Name = "btn_tailieusach";
            this.btn_tailieusach.Size = new System.Drawing.Size(248, 48);
            this.btn_tailieusach.TabIndex = 16;
            this.btn_tailieusach.Text = "Tài liệu sách";
            this.btn_tailieusach.UseVisualStyleBackColor = false;
            // 
            // QL_phong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1168, 619);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnl_thanhquanly);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.dataGrid_hocgia);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "QL_phong";
            this.Text = "QL_phong";
            this.pnl_thanhquanly.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_hocgia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_thoat;
        private System.Windows.Forms.Button btn_TK;
        private System.Windows.Forms.Panel pnl_thanhquanly;
        private System.Windows.Forms.Label lab_muontra;
        private System.Windows.Forms.TextBox txt_timkiem;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Timer menuHideTimer;
        private System.Windows.Forms.DataGridView dataGrid_hocgia;
        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lab_;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btn_thongtinphong;
        private System.Windows.Forms.Button btn_tailieusach;
    }
}