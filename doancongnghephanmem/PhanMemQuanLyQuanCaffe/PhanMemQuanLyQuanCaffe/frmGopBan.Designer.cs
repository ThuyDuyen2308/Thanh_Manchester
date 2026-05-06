namespace PhanMemQuanLyQuanCaffe
{
    partial class frmGopBan
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

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblSoLuongChon = new System.Windows.Forms.Label();
            this.clbBanNguon = new System.Windows.Forms.CheckedListBox();
            this.cboBanDich = new System.Windows.Forms.ComboBox();
            this.dtgvChiTiet = new System.Windows.Forms.DataGridView();
            this.colBan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTongGop = new System.Windows.Forms.Label();
            this.btnGopBan = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvChiTiet)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(643, 48);
            this.panel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(10, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 30);
            this.label3.TabIndex = 0;
            this.label3.Text = "⊞ Gộp Bàn";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.label1.Location = new System.Drawing.Point(13, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Chọn các bàn cần gộp (tick ✓):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.label2.Location = new System.Drawing.Point(232, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "Gộp vào bàn (đích):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.label4.Location = new System.Drawing.Point(184, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(249, 19);
            this.label4.TabIndex = 6;
            this.label4.Text = "Chi tiết đồ uống các bàn được chọn:";
            // 
            // lblSoLuongChon
            // 
            this.lblSoLuongChon.AutoSize = true;
            this.lblSoLuongChon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblSoLuongChon.ForeColor = System.Drawing.Color.Gray;
            this.lblSoLuongChon.Location = new System.Drawing.Point(13, 81);
            this.lblSoLuongChon.Name = "lblSoLuongChon";
            this.lblSoLuongChon.Size = new System.Drawing.Size(87, 15);
            this.lblSoLuongChon.TabIndex = 2;
            this.lblSoLuongChon.Text = "Đã chọn: 0 bàn";
            // 
            // clbBanNguon
            // 
            this.clbBanNguon.CheckOnClick = true;
            this.clbBanNguon.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.clbBanNguon.FormattingEnabled = true;
            this.clbBanNguon.Location = new System.Drawing.Point(13, 97);
            this.clbBanNguon.Name = "clbBanNguon";
            this.clbBanNguon.Size = new System.Drawing.Size(155, 158);
            this.clbBanNguon.TabIndex = 3;
            this.clbBanNguon.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbBanNguon_ItemCheck);
            // 
            // cboBanDich
            // 
            this.cboBanDich.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBanDich.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboBanDich.Location = new System.Drawing.Point(184, 82);
            this.cboBanDich.Name = "cboBanDich";
            this.cboBanDich.Size = new System.Drawing.Size(172, 28);
            this.cboBanDich.TabIndex = 5;
            // 
            // dtgvChiTiet
            // 
            this.dtgvChiTiet.AllowUserToAddRows = false;
            this.dtgvChiTiet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvChiTiet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgvChiTiet.ColumnHeadersHeight = 30;
            this.dtgvChiTiet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colBan,
            this.colTen,
            this.colSL,
            this.colDG,
            this.colTT});
            this.dtgvChiTiet.EnableHeadersVisualStyles = false;
            this.dtgvChiTiet.Location = new System.Drawing.Point(184, 143);
            this.dtgvChiTiet.Name = "dtgvChiTiet";
            this.dtgvChiTiet.ReadOnly = true;
            this.dtgvChiTiet.RowHeadersVisible = false;
            this.dtgvChiTiet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvChiTiet.Size = new System.Drawing.Size(446, 173);
            this.dtgvChiTiet.TabIndex = 7;
            // 
            // colBan
            // 
            this.colBan.FillWeight = 60F;
            this.colBan.HeaderText = "Bàn";
            this.colBan.Name = "colBan";
            this.colBan.ReadOnly = true;
            // 
            // colTen
            // 
            this.colTen.FillWeight = 140F;
            this.colTen.HeaderText = "Tên Đồ Uống";
            this.colTen.Name = "colTen";
            this.colTen.ReadOnly = true;
            // 
            // colSL
            // 
            this.colSL.FillWeight = 50F;
            this.colSL.HeaderText = "SL";
            this.colSL.Name = "colSL";
            this.colSL.ReadOnly = true;
            // 
            // colDG
            // 
            this.colDG.FillWeight = 80F;
            this.colDG.HeaderText = "Đơn Giá";
            this.colDG.Name = "colDG";
            this.colDG.ReadOnly = true;
            // 
            // colTT
            // 
            this.colTT.FillWeight = 90F;
            this.colTT.HeaderText = "Thành Tiền";
            this.colTT.Name = "colTT";
            this.colTT.ReadOnly = true;
            // 
            // lblTongGop
            // 
            this.lblTongGop.AutoSize = true;
            this.lblTongGop.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongGop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.lblTongGop.Location = new System.Drawing.Point(184, 328);
            this.lblTongGop.Name = "lblTongGop";
            this.lblTongGop.Size = new System.Drawing.Size(137, 20);
            this.lblTongGop.TabIndex = 8;
            this.lblTongGop.Text = "Tổng tiền gộp: 0 đ";
            // 
            // btnGopBan
            // 
            this.btnGopBan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.btnGopBan.FlatAppearance.BorderSize = 0;
            this.btnGopBan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGopBan.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnGopBan.ForeColor = System.Drawing.Color.White;
            this.btnGopBan.Location = new System.Drawing.Point(441, 355);
            this.btnGopBan.Name = "btnGopBan";
            this.btnGopBan.Size = new System.Drawing.Size(94, 36);
            this.btnGopBan.TabIndex = 9;
            this.btnGopBan.Text = "⊞ Gộp Bàn";
            this.btnGopBan.UseVisualStyleBackColor = false;
            this.btnGopBan.Click += new System.EventHandler(this.btnGopBan_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnThoat.FlatAppearance.BorderSize = 0;
            this.btnThoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnThoat.ForeColor = System.Drawing.Color.White;
            this.btnThoat.Location = new System.Drawing.Point(544, 355);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(86, 36);
            this.btnThoat.TabIndex = 10;
            this.btnThoat.Text = "✕ Thoát";
            this.btnThoat.UseVisualStyleBackColor = false;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // frmGopBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 407);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblSoLuongChon);
            this.Controls.Add(this.clbBanNguon);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboBanDich);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtgvChiTiet);
            this.Controls.Add(this.lblTongGop);
            this.Controls.Add(this.btnGopBan);
            this.Controls.Add(this.btnThoat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGopBan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gộp Bàn";
            this.Load += new System.EventHandler(this.frmGopBan_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvChiTiet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblSoLuongChon;
        private System.Windows.Forms.CheckedListBox clbBanNguon;
        private System.Windows.Forms.ComboBox cboBanDich;
        private System.Windows.Forms.DataGridView dtgvChiTiet;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSL;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDG;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTT;
        private System.Windows.Forms.Label lblTongGop;
        private System.Windows.Forms.Button btnGopBan;
        private System.Windows.Forms.Button btnThoat;
    }
}
