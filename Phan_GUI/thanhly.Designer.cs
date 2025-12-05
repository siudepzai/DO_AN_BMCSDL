namespace DO_AN_BMCSDL.Phan_GUI
{
    partial class thanhly
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(thanhly));
            this.menuHideTimer = new System.Windows.Forms.Timer(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.lab_QLdocgia = new System.Windows.Forms.Label();
            this.lab_ = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnl_thanhquanly = new System.Windows.Forms.Panel();
            this.btn_thoat = new System.Windows.Forms.Button();
            this.btn_xoa = new System.Windows.Forms.Button();
            this.btn_sua = new System.Windows.Forms.Button();
            this.btn_them = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.logo = new System.Windows.Forms.PictureBox();
            this.dgvThanhLy = new System.Windows.Forms.DataGridView();
            this.panel3.SuspendLayout();
            this.pnl_thanhquanly.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThanhLy)).BeginInit();
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
            this.panel3.Location = new System.Drawing.Point(3, 91);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1173, 42);
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
            this.lab_.Location = new System.Drawing.Point(593, 0);
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
            // pnl_thanhquanly
            // 
            this.pnl_thanhquanly.Controls.Add(this.btn_thoat);
            this.pnl_thanhquanly.Controls.Add(this.btn_xoa);
            this.pnl_thanhquanly.Controls.Add(this.btn_sua);
            this.pnl_thanhquanly.Controls.Add(this.btn_them);
            this.pnl_thanhquanly.Location = new System.Drawing.Point(3, 150);
            this.pnl_thanhquanly.Name = "pnl_thanhquanly";
            this.pnl_thanhquanly.Size = new System.Drawing.Size(1173, 67);
            this.pnl_thanhquanly.TabIndex = 19;
            // 
            // btn_thoat
            // 
            this.btn_thoat.Location = new System.Drawing.Point(942, 9);
            this.btn_thoat.Name = "btn_thoat";
            this.btn_thoat.Size = new System.Drawing.Size(190, 45);
            this.btn_thoat.TabIndex = 0;
            this.btn_thoat.Text = "Thoát";
            this.btn_thoat.UseVisualStyleBackColor = true;
            this.btn_thoat.Click += new System.EventHandler(this.btn_thoat_Click);
            // 
            // btn_xoa
            // 
            this.btn_xoa.Location = new System.Drawing.Point(617, 9);
            this.btn_xoa.Name = "btn_xoa";
            this.btn_xoa.Size = new System.Drawing.Size(190, 45);
            this.btn_xoa.TabIndex = 0;
            this.btn_xoa.Text = "Xóa";
            this.btn_xoa.UseVisualStyleBackColor = true;
            this.btn_xoa.Click += new System.EventHandler(this.btn_xoa_Click);
            // 
            // btn_sua
            // 
            this.btn_sua.Location = new System.Drawing.Point(312, 9);
            this.btn_sua.Name = "btn_sua";
            this.btn_sua.Size = new System.Drawing.Size(190, 45);
            this.btn_sua.TabIndex = 0;
            this.btn_sua.Text = "Sửa";
            this.btn_sua.UseVisualStyleBackColor = true;
            this.btn_sua.Click += new System.EventHandler(this.btn_sua_Click);
            // 
            // btn_them
            // 
            this.btn_them.Location = new System.Drawing.Point(19, 9);
            this.btn_them.Name = "btn_them";
            this.btn_them.Size = new System.Drawing.Size(190, 45);
            this.btn_them.TabIndex = 0;
            this.btn_them.Text = "Thêm";
            this.btn_them.UseVisualStyleBackColor = true;
            this.btn_them.Click += new System.EventHandler(this.btn_them_Click);
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
            this.panel1.Size = new System.Drawing.Size(1176, 90);
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
            // dgvThanhLy
            // 
            this.dgvThanhLy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThanhLy.Location = new System.Drawing.Point(6, 244);
            this.dgvThanhLy.Name = "dgvThanhLy";
            this.dgvThanhLy.RowHeadersWidth = 51;
            this.dgvThanhLy.RowTemplate.Height = 24;
            this.dgvThanhLy.Size = new System.Drawing.Size(1170, 370);
            this.dgvThanhLy.TabIndex = 20;
            this.dgvThanhLy.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvThanhLy_CellContentClick);
            // 
            // thanhly
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 619);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.pnl_thanhquanly);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvThanhLy);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "thanhly";
            this.Text = "thanhly";
            this.Load += new System.EventHandler(this.thanhly_Load_1);
            this.panel3.ResumeLayout(false);
            this.pnl_thanhquanly.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThanhLy)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer menuHideTimer;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lab_QLdocgia;
        private System.Windows.Forms.Label lab_;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnl_thanhquanly;
        private System.Windows.Forms.Button btn_thoat;
        private System.Windows.Forms.Button btn_xoa;
        private System.Windows.Forms.Button btn_sua;
        private System.Windows.Forms.Button btn_them;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.DataGridView dgvThanhLy;
    }
}