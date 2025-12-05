namespace DO_AN_BMCSDL
{
    partial class Create_key
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
            this.btn_taokhoa = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_taokhoa
            // 
            this.btn_taokhoa.Location = new System.Drawing.Point(137, 113);
            this.btn_taokhoa.Name = "btn_taokhoa";
            this.btn_taokhoa.Size = new System.Drawing.Size(121, 47);
            this.btn_taokhoa.TabIndex = 0;
            this.btn_taokhoa.Text = "Tạo khóa";
            this.btn_taokhoa.UseVisualStyleBackColor = true;
            this.btn_taokhoa.Click += new System.EventHandler(this.btn_taokhoa_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(74, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(283, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tạo khóa công khai và khóa bí mật";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 200);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_taokhoa);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_taokhoa;
        private System.Windows.Forms.Label label1;
    }
}

