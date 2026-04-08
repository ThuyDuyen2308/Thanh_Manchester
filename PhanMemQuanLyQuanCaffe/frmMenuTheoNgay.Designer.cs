namespace PhanMemQuanLyQuanCaffe
{
    partial class frmMenuTheoNgay
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
            this.dtpNgay = new System.Windows.Forms.DateTimePicker();
            this.clbDoUong = new System.Windows.Forms.CheckedListBox();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnXem = new System.Windows.Forms.Button();
            this.dtgvMenu = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpNgay
            // 
            this.dtpNgay.Location = new System.Drawing.Point(56, 25);
            this.dtpNgay.Name = "dtpNgay";
            this.dtpNgay.Size = new System.Drawing.Size(200, 20);
            this.dtpNgay.TabIndex = 0;
            // 
            // clbDoUong
            // 
            this.clbDoUong.CheckOnClick = true;
            this.clbDoUong.FormattingEnabled = true;
            this.clbDoUong.Location = new System.Drawing.Point(-2, 61);
            this.clbDoUong.Name = "clbDoUong";
            this.clbDoUong.Size = new System.Drawing.Size(204, 379);
            this.clbDoUong.TabIndex = 1;
            this.clbDoUong.Click += new System.EventHandler(this.frmMenuTheoNgay_Load);
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(713, 119);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(75, 23);
            this.btnLuu.TabIndex = 2;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnXem
            // 
            this.btnXem.Location = new System.Drawing.Point(713, 61);
            this.btnXem.Name = "btnXem";
            this.btnXem.Size = new System.Drawing.Size(75, 23);
            this.btnXem.TabIndex = 3;
            this.btnXem.Text = "Xem";
            this.btnXem.UseVisualStyleBackColor = true;
            this.btnXem.Click += new System.EventHandler(this.btnXem_Click);
            // 
            // dtgvMenu
            // 
            this.dtgvMenu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvMenu.Location = new System.Drawing.Point(208, 61);
            this.dtgvMenu.Name = "dtgvMenu";
            this.dtgvMenu.Size = new System.Drawing.Size(488, 377);
            this.dtgvMenu.TabIndex = 4;
            // 
            // frmMenuTheoNgay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dtgvMenu);
            this.Controls.Add(this.btnXem);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.clbDoUong);
            this.Controls.Add(this.dtpNgay);
            this.Name = "frmMenuTheoNgay";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dtgvMenu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpNgay;
        private System.Windows.Forms.CheckedListBox clbDoUong;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnXem;
        private System.Windows.Forms.DataGridView dtgvMenu;
    }
}