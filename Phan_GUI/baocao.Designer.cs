namespace DO_AN_BMCSDL.Phan_GUI
{
    partial class baocao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(baocao));
            this.panel2 = new System.Windows.Forms.Panel();
            this.menuHideTimer = new System.Windows.Forms.Timer(this.components);
            this.lab_QLdocgia = new System.Windows.Forms.Label();
            this.logo = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lab_ = new System.Windows.Forms.Label();
            this.pnl_thanhquanly = new System.Windows.Forms.Panel();
            this.cb_luachon = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_Thongke = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txt_TK = new System.Windows.Forms.TextBox();
            this.btn_TK = new System.Windows.Forms.Button();
            this.lab_danhsachtailieu = new System.Windows.Forms.Label();
            this.dgvBaoCao = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnl_thanhquanly.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoCao)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(3, 93);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1097, 64);
            this.panel2.TabIndex = 1;
            // 
            // menuHideTimer
            // 
            this.menuHideTimer.Interval = 1000;
            // 
            // lab_QLdocgia
            // 
            this.lab_QLdocgia.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lab_QLdocgia.Location = new System.Drawing.Point(3, 1);
            this.lab_QLdocgia.Name = "lab_QLdocgia";
            this.lab_QLdocgia.Size = new System.Drawing.Size(584, 41);
            this.lab_QLdocgia.TabIndex = 14;
            this.lab_QLdocgia.Text = "THỐNG KÊ BÁO CÁO";
            this.lab_QLdocgia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel1.Controls.Add(this.lab_);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.logo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1197, 90);
            this.panel1.TabIndex = 44;
            // 
            // lab_
            // 
            this.lab_.Dock = System.Windows.Forms.DockStyle.Right;
            this.lab_.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lab_.Location = new System.Drawing.Point(614, 0);
            this.lab_.Name = "lab_";
            this.lab_.Size = new System.Drawing.Size(583, 90);
            this.lab_.TabIndex = 2;
            this.lab_.Text = " HỌC HẾT SỨC-CHƠI HẾT MÌNH";
            this.lab_.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_thanhquanly
            // 
            this.pnl_thanhquanly.Controls.Add(this.cb_luachon);
            this.pnl_thanhquanly.Controls.Add(this.button1);
            this.pnl_thanhquanly.Controls.Add(this.btn_Thongke);
            this.pnl_thanhquanly.Location = new System.Drawing.Point(15, 199);
            this.pnl_thanhquanly.Name = "pnl_thanhquanly";
            this.pnl_thanhquanly.Size = new System.Drawing.Size(1173, 67);
            this.pnl_thanhquanly.TabIndex = 46;
            // 
            // cb_luachon
            // 
            this.cb_luachon.FormattingEnabled = true;
            this.cb_luachon.Items.AddRange(new object[] {
            "Tất cả dữ liệu",
            "Dữ liệu theo tháng",
            "Dữ liệu theo Quý",
            "Dữ liệu theo năm"});
            this.cb_luachon.Location = new System.Drawing.Point(280, 17);
            this.cb_luachon.Name = "cb_luachon";
            this.cb_luachon.Size = new System.Drawing.Size(319, 30);
            this.cb_luachon.TabIndex = 1;
            this.cb_luachon.Text = "Tất cả dữ liệu";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.RoyalBlue;
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(814, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 45);
            this.button1.TabIndex = 0;
            this.button1.Text = "Thoát";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_Thongke
            // 
            this.btn_Thongke.BackColor = System.Drawing.Color.RoyalBlue;
            this.btn_Thongke.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_Thongke.Location = new System.Drawing.Point(641, 9);
            this.btn_Thongke.Name = "btn_Thongke";
            this.btn_Thongke.Size = new System.Drawing.Size(143, 45);
            this.btn_Thongke.TabIndex = 0;
            this.btn_Thongke.Text = "Thông kê";
            this.btn_Thongke.UseVisualStyleBackColor = false;
            this.btn_Thongke.Click += new System.EventHandler(this.btn_Thongke_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.panel3.Controls.Add(this.txt_TK);
            this.panel3.Controls.Add(this.btn_TK);
            this.panel3.Controls.Add(this.lab_QLdocgia);
            this.panel3.Location = new System.Drawing.Point(0, 93);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1195, 50);
            this.panel3.TabIndex = 45;
            // 
            // txt_TK
            // 
            this.txt_TK.Location = new System.Drawing.Point(712, 12);
            this.txt_TK.Name = "txt_TK";
            this.txt_TK.Size = new System.Drawing.Size(385, 30);
            this.txt_TK.TabIndex = 16;
            // 
            // btn_TK
            // 
            this.btn_TK.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btn_TK.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_TK.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_TK.Location = new System.Drawing.Point(1094, 12);
            this.btn_TK.Name = "btn_TK";
            this.btn_TK.Size = new System.Drawing.Size(76, 30);
            this.btn_TK.TabIndex = 15;
            this.btn_TK.Text = "Tìm";
            this.btn_TK.UseVisualStyleBackColor = false;
            this.btn_TK.Click += new System.EventHandler(this.btn_TK_Click);
            // 
            // lab_danhsachtailieu
            // 
            this.lab_danhsachtailieu.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_danhsachtailieu.Location = new System.Drawing.Point(15, 302);
            this.lab_danhsachtailieu.Name = "lab_danhsachtailieu";
            this.lab_danhsachtailieu.Size = new System.Drawing.Size(1173, 32);
            this.lab_danhsachtailieu.TabIndex = 55;
            this.lab_danhsachtailieu.Text = "Báo cáo thống kê trong thư viện";
            this.lab_danhsachtailieu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvBaoCao
            // 
            this.dgvBaoCao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBaoCao.Location = new System.Drawing.Point(15, 337);
            this.dgvBaoCao.Name = "dgvBaoCao";
            this.dgvBaoCao.RowHeadersWidth = 51;
            this.dgvBaoCao.RowTemplate.Height = 24;
            this.dgvBaoCao.Size = new System.Drawing.Size(1155, 270);
            this.dgvBaoCao.TabIndex = 56;
            // 
            // baocao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1197, 619);
            this.Controls.Add(this.dgvBaoCao);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnl_thanhquanly);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.lab_danhsachtailieu);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "baocao";
            this.Text = "baocao";
            this.Load += new System.EventHandler(this.baocao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.pnl_thanhquanly.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoCao)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Timer menuHideTimer;
        private System.Windows.Forms.Label lab_QLdocgia;
        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lab_;
        private System.Windows.Forms.Panel pnl_thanhquanly;
        private System.Windows.Forms.Button btn_Thongke;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_TK;
        private System.Windows.Forms.Label lab_danhsachtailieu;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cb_luachon;
        private System.Windows.Forms.TextBox txt_TK;
        private System.Windows.Forms.DataGridView dgvBaoCao;
    }
}