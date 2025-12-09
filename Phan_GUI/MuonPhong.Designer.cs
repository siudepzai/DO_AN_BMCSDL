namespace DO_AN_BMCSDL.Phan_GUI
{
    partial class FormMuonPhong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMuonPhong));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnTim = new System.Windows.Forms.Button();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTrangChu = new System.Windows.Forms.Button();
            this.dgvTraCuuPhong = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnGiaHan = new System.Windows.Forms.Button();
            this.txtNhapMaPhong = new System.Windows.Forms.TextBox();
            this.txtNhapSoGioMuon = new System.Windows.Forms.TextBox();
            this.btnTraPhongSom = new System.Windows.Forms.Button();
            this.btnDangKyMuon = new System.Windows.Forms.Button();
            this.dgvPhongDaMuon = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTraCuuPhong)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhongDaMuon)).BeginInit();
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
            this.panel1.TabIndex = 4;
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
            this.panel2.Location = new System.Drawing.Point(0, 145);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(878, 52);
            this.panel2.TabIndex = 5;
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
            // dgvTraCuuPhong
            // 
            this.dgvTraCuuPhong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTraCuuPhong.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvTraCuuPhong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTraCuuPhong.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTraCuuPhong.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvTraCuuPhong.Location = new System.Drawing.Point(13, 204);
            this.dgvTraCuuPhong.Name = "dgvTraCuuPhong";
            this.dgvTraCuuPhong.RowHeadersWidth = 51;
            this.dgvTraCuuPhong.RowTemplate.Height = 24;
            this.dgvTraCuuPhong.Size = new System.Drawing.Size(855, 150);
            this.dgvTraCuuPhong.TabIndex = 6;
            this.dgvTraCuuPhong.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTraCuuPhong_CellContentClick);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel3.Controls.Add(this.btnGiaHan);
            this.panel3.Controls.Add(this.txtNhapMaPhong);
            this.panel3.Controls.Add(this.txtNhapSoGioMuon);
            this.panel3.Controls.Add(this.btnTraPhongSom);
            this.panel3.Controls.Add(this.btnDangKyMuon);
            this.panel3.Location = new System.Drawing.Point(13, 375);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(855, 71);
            this.panel3.TabIndex = 7;
            // 
            // btnGiaHan
            // 
            this.btnGiaHan.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnGiaHan.FlatAppearance.BorderSize = 0;
            this.btnGiaHan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGiaHan.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGiaHan.ForeColor = System.Drawing.Color.White;
            this.btnGiaHan.Location = new System.Drawing.Point(722, 16);
            this.btnGiaHan.Name = "btnGiaHan";
            this.btnGiaHan.Size = new System.Drawing.Size(116, 42);
            this.btnGiaHan.TabIndex = 1;
            this.btnGiaHan.Text = "Gia hạn";
            this.btnGiaHan.UseVisualStyleBackColor = false;
            // 
            // txtNhapMaPhong
            // 
            this.txtNhapMaPhong.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNhapMaPhong.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNhapMaPhong.Location = new System.Drawing.Point(18, 16);
            this.txtNhapMaPhong.Multiline = true;
            this.txtNhapMaPhong.Name = "txtNhapMaPhong";
            this.txtNhapMaPhong.Size = new System.Drawing.Size(169, 42);
            this.txtNhapMaPhong.TabIndex = 0;
            // 
            // txtNhapSoGioMuon
            // 
            this.txtNhapSoGioMuon.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNhapSoGioMuon.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNhapSoGioMuon.Location = new System.Drawing.Point(203, 16);
            this.txtNhapSoGioMuon.Multiline = true;
            this.txtNhapSoGioMuon.Name = "txtNhapSoGioMuon";
            this.txtNhapSoGioMuon.Size = new System.Drawing.Size(169, 42);
            this.txtNhapSoGioMuon.TabIndex = 0;
            // 
            // btnTraPhongSom
            // 
            this.btnTraPhongSom.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnTraPhongSom.FlatAppearance.BorderSize = 0;
            this.btnTraPhongSom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTraPhongSom.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTraPhongSom.ForeColor = System.Drawing.Color.White;
            this.btnTraPhongSom.Location = new System.Drawing.Point(558, 16);
            this.btnTraPhongSom.Name = "btnTraPhongSom";
            this.btnTraPhongSom.Size = new System.Drawing.Size(158, 42);
            this.btnTraPhongSom.TabIndex = 1;
            this.btnTraPhongSom.Text = "Trả phòng sớm";
            this.btnTraPhongSom.UseVisualStyleBackColor = false;
            this.btnTraPhongSom.Click += new System.EventHandler(this.btnTraPhongSom_Click);
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
            this.btnDangKyMuon.Click += new System.EventHandler(this.btnDangKyMuon_Click);
            // 
            // dgvPhongDaMuon
            // 
            this.dgvPhongDaMuon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPhongDaMuon.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvPhongDaMuon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPhongDaMuon.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvPhongDaMuon.Location = new System.Drawing.Point(13, 453);
            this.dgvPhongDaMuon.Name = "dgvPhongDaMuon";
            this.dgvPhongDaMuon.RowHeadersWidth = 51;
            this.dgvPhongDaMuon.RowTemplate.Height = 24;
            this.dgvPhongDaMuon.Size = new System.Drawing.Size(855, 150);
            this.dgvPhongDaMuon.TabIndex = 8;
            this.dgvPhongDaMuon.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPhongDaMuon_CellContentClick_1);
            // 
            // FormMuonPhong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(880, 609);
            this.Controls.Add(this.dgvPhongDaMuon);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.dgvTraCuuPhong);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "FormMuonPhong";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mượn Phòng";
            this.Load += new System.EventHandler(this.FormMuonPhong_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTraCuuPhong)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhongDaMuon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnTrangChu;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.DataGridView dgvTraCuuPhong;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnGiaHan;
        private System.Windows.Forms.TextBox txtNhapMaPhong;
        private System.Windows.Forms.TextBox txtNhapSoGioMuon;
        private System.Windows.Forms.Button btnTraPhongSom;
        private System.Windows.Forms.Button btnDangKyMuon;
        private System.Windows.Forms.DataGridView dgvPhongDaMuon;
    }
}