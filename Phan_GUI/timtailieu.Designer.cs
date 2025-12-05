namespace DO_AN_BMCSDL.Phan_GUI
{
    partial class timtailieu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(timtailieu));
            this.btn_thoat = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txt_timkiem = new System.Windows.Forms.TextBox();
            this.menuHideTimer = new System.Windows.Forms.Timer(this.components);
            this.dgvTaiLieu = new System.Windows.Forms.DataGridView();
            this.lab_QLtailieu = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lab_ = new System.Windows.Forms.Label();
            this.logo = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn_TK = new System.Windows.Forms.Button();
            this.lab_danhsachtailieu = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaiLieu)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_thoat
            // 
            this.btn_thoat.BackColor = System.Drawing.Color.RoyalBlue;
            this.btn_thoat.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_thoat.Location = new System.Drawing.Point(811, 546);
            this.btn_thoat.Name = "btn_thoat";
            this.btn_thoat.Size = new System.Drawing.Size(190, 45);
            this.btn_thoat.TabIndex = 56;
            this.btn_thoat.Text = "Thoát";
            this.btn_thoat.UseVisualStyleBackColor = false;
            this.btn_thoat.Click += new System.EventHandler(this.btn_thoat_Click);
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(3, 93);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1097, 64);
            this.panel2.TabIndex = 1;
            // 
            // txt_timkiem
            // 
            this.txt_timkiem.Location = new System.Drawing.Point(510, 20);
            this.txt_timkiem.Name = "txt_timkiem";
            this.txt_timkiem.Size = new System.Drawing.Size(403, 30);
            this.txt_timkiem.TabIndex = 2;
            // 
            // menuHideTimer
            // 
            this.menuHideTimer.Interval = 1000;
            // 
            // dgvTaiLieu
            // 
            this.dgvTaiLieu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTaiLieu.Location = new System.Drawing.Point(8, 204);
            this.dgvTaiLieu.Name = "dgvTaiLieu";
            this.dgvTaiLieu.RowHeadersWidth = 51;
            this.dgvTaiLieu.RowTemplate.Height = 24;
            this.dgvTaiLieu.Size = new System.Drawing.Size(1010, 336);
            this.dgvTaiLieu.TabIndex = 59;
            // 
            // lab_QLtailieu
            // 
            this.lab_QLtailieu.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lab_QLtailieu.Location = new System.Drawing.Point(3, 1);
            this.lab_QLtailieu.Name = "lab_QLtailieu";
            this.lab_QLtailieu.Size = new System.Drawing.Size(501, 66);
            this.lab_QLtailieu.TabIndex = 14;
            this.lab_QLtailieu.Text = "QUẢN LÝ TÀI LIỆU";
            this.lab_QLtailieu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.panel1.Size = new System.Drawing.Size(1018, 90);
            this.panel1.TabIndex = 57;
            // 
            // lab_
            // 
            this.lab_.Dock = System.Windows.Forms.DockStyle.Right;
            this.lab_.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lab_.Location = new System.Drawing.Point(509, 0);
            this.lab_.Name = "lab_";
            this.lab_.Size = new System.Drawing.Size(509, 90);
            this.lab_.TabIndex = 2;
            this.lab_.Text = " HỌC HẾT SỨC-CHƠI HẾT MÌNH";
            this.lab_.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.panel3.Controls.Add(this.btn_TK);
            this.panel3.Controls.Add(this.lab_QLtailieu);
            this.panel3.Controls.Add(this.txt_timkiem);
            this.panel3.Location = new System.Drawing.Point(6, 90);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1012, 67);
            this.panel3.TabIndex = 58;
            // 
            // btn_TK
            // 
            this.btn_TK.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btn_TK.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_TK.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_TK.Location = new System.Drawing.Point(919, 18);
            this.btn_TK.Name = "btn_TK";
            this.btn_TK.Size = new System.Drawing.Size(76, 30);
            this.btn_TK.TabIndex = 15;
            this.btn_TK.Text = "Tìm";
            this.btn_TK.UseVisualStyleBackColor = false;
            this.btn_TK.Click += new System.EventHandler(this.btn_TK_Click);
            // 
            // lab_danhsachtailieu
            // 
            this.lab_danhsachtailieu.AutoSize = true;
            this.lab_danhsachtailieu.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_danhsachtailieu.Location = new System.Drawing.Point(381, 169);
            this.lab_danhsachtailieu.Name = "lab_danhsachtailieu";
            this.lab_danhsachtailieu.Size = new System.Drawing.Size(229, 32);
            this.lab_danhsachtailieu.TabIndex = 60;
            this.lab_danhsachtailieu.Text = "Danh sách tài liệu";
            // 
            // timtailieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 593);
            this.Controls.Add(this.btn_thoat);
            this.Controls.Add(this.dgvTaiLieu);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.lab_danhsachtailieu);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "timtailieu";
            this.Text = "timtailieu";
            this.Load += new System.EventHandler(this.timtailieu_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaiLieu)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_thoat;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txt_timkiem;
        private System.Windows.Forms.Timer menuHideTimer;
        private System.Windows.Forms.DataGridView dgvTaiLieu;
        private System.Windows.Forms.Label lab_QLtailieu;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lab_;
        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_TK;
        private System.Windows.Forms.Label lab_danhsachtailieu;
    }
}