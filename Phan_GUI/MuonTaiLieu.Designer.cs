namespace DO_AN_BMCSDL.Phan_GUI
{
    partial class FormMuonTaiLieu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMuonTaiLieu));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnTim = new System.Windows.Forms.Button();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTrangChu = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtNhapMaTaiLieu = new System.Windows.Forms.TextBox();
            this.txtNhapSoLuongMuon = new System.Windows.Forms.TextBox();
            this.btnDangKyMuon = new System.Windows.Forms.Button();
            this.dgvTraCuuTaiLieu = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTraCuuTaiLieu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(880, 133);
            this.panel1.TabIndex = 9;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(178, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(595, 104);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Controls.Add(this.btnTim);
            this.panel2.Controls.Add(this.txtTimKiem);
            this.panel2.Controls.Add(this.btnTrangChu);
            this.panel2.Location = new System.Drawing.Point(0, 148);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(878, 52);
            this.panel2.TabIndex = 10;
            // 
            // btnTim
            // 
            this.btnTim.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnTim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTim.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTim.Location = new System.Drawing.Point(761, 4);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(75, 45);
            this.btnTim.TabIndex = 2;
            this.btnTim.Text = "Tìm";
            this.btnTim.UseVisualStyleBackColor = false;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimKiem.Location = new System.Drawing.Point(385, 3);
            this.txtTimKiem.Multiline = true;
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(370, 46);
            this.txtTimKiem.TabIndex = 1;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // btnTrangChu
            // 
            this.btnTrangChu.BackColor = System.Drawing.Color.Black;
            this.btnTrangChu.FlatAppearance.BorderSize = 0;
            this.btnTrangChu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrangChu.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrangChu.ForeColor = System.Drawing.Color.White;
            this.btnTrangChu.Location = new System.Drawing.Point(31, 3);
            this.btnTrangChu.Name = "btnTrangChu";
            this.btnTrangChu.Size = new System.Drawing.Size(135, 46);
            this.btnTrangChu.TabIndex = 0;
            this.btnTrangChu.Text = "Trang chủ";
            this.btnTrangChu.UseVisualStyleBackColor = false;
            this.btnTrangChu.Click += new System.EventHandler(this.btnTrangChu_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel3.Controls.Add(this.txtNhapMaTaiLieu);
            this.panel3.Controls.Add(this.txtNhapSoLuongMuon);
            this.panel3.Controls.Add(this.btnDangKyMuon);
            this.panel3.Location = new System.Drawing.Point(13, 378);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(855, 71);
            this.panel3.TabIndex = 12;
            // 
            // txtNhapMaTaiLieu
            // 
            this.txtNhapMaTaiLieu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNhapMaTaiLieu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNhapMaTaiLieu.Location = new System.Drawing.Point(18, 16);
            this.txtNhapMaTaiLieu.Multiline = true;
            this.txtNhapMaTaiLieu.Name = "txtNhapMaTaiLieu";
            this.txtNhapMaTaiLieu.Size = new System.Drawing.Size(169, 42);
            this.txtNhapMaTaiLieu.TabIndex = 0;
            // 
            // txtNhapSoLuongMuon
            // 
            this.txtNhapSoLuongMuon.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNhapSoLuongMuon.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNhapSoLuongMuon.Location = new System.Drawing.Point(203, 16);
            this.txtNhapSoLuongMuon.Multiline = true;
            this.txtNhapSoLuongMuon.Name = "txtNhapSoLuongMuon";
            this.txtNhapSoLuongMuon.Size = new System.Drawing.Size(169, 42);
            this.txtNhapSoLuongMuon.TabIndex = 0;
            // 
            // btnDangKyMuon
            // 
            this.btnDangKyMuon.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnDangKyMuon.FlatAppearance.BorderSize = 0;
            this.btnDangKyMuon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangKyMuon.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangKyMuon.ForeColor = System.Drawing.Color.White;
            this.btnDangKyMuon.Location = new System.Drawing.Point(393, 16);
            this.btnDangKyMuon.Name = "btnDangKyMuon";
            this.btnDangKyMuon.Size = new System.Drawing.Size(159, 42);
            this.btnDangKyMuon.TabIndex = 1;
            this.btnDangKyMuon.Text = "Đăng ký mượn";
            this.btnDangKyMuon.UseVisualStyleBackColor = false;
            // 
            // dgvTraCuuTaiLieu
            // 
            this.dgvTraCuuTaiLieu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTraCuuTaiLieu.Location = new System.Drawing.Point(13, 207);
            this.dgvTraCuuTaiLieu.Name = "dgvTraCuuTaiLieu";
            this.dgvTraCuuTaiLieu.RowHeadersWidth = 51;
            this.dgvTraCuuTaiLieu.RowTemplate.Height = 24;
            this.dgvTraCuuTaiLieu.Size = new System.Drawing.Size(855, 150);
            this.dgvTraCuuTaiLieu.TabIndex = 11;
            this.dgvTraCuuTaiLieu.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTraCuuTaiLieu_CellContentClick);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(13, 456);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(855, 150);
            this.dataGridView2.TabIndex = 13;
            // 
            // FormMuonTaiLieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(880, 609);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.dgvTraCuuTaiLieu);
            this.Controls.Add(this.dataGridView2);
            this.Name = "FormMuonTaiLieu";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MuonTaiLieu";
            this.Load += new System.EventHandler(this.FormMuonTaiLieu_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTraCuuTaiLieu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTrangChu;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtNhapMaTaiLieu;
        private System.Windows.Forms.TextBox txtNhapSoLuongMuon;
        private System.Windows.Forms.Button btnDangKyMuon;
        private System.Windows.Forms.DataGridView dgvTraCuuTaiLieu;
        private System.Windows.Forms.DataGridView dataGridView2;
    }
}