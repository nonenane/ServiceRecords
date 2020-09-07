namespace ServiceRecords
{
    partial class frmViewPayment
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvNote = new System.Windows.Forms.DataGridView();
            this.btClose = new System.Windows.Forms.Button();
            this.btReportNoteSingleDeps = new System.Windows.Forms.Button();
            this.btListNotePeriod = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbHighlightInPayment = new System.Windows.Forms.Label();
            this.cControl = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cFio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSumma = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cComment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPass = new System.Windows.Forms.DataGridViewButtonColumn();
            this.cOperation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbItogo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btTakeMoney = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvNote
            // 
            this.dgvNote.AllowUserToAddRows = false;
            this.dgvNote.AllowUserToDeleteRows = false;
            this.dgvNote.AllowUserToResizeRows = false;
            this.dgvNote.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNote.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvNote.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNote.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cControl,
            this.cDate,
            this.cNumber,
            this.cFio,
            this.cSumma,
            this.cComment,
            this.cPass,
            this.cOperation});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNote.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvNote.Location = new System.Drawing.Point(12, 12);
            this.dgvNote.MultiSelect = false;
            this.dgvNote.Name = "dgvNote";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNote.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvNote.RowHeadersVisible = false;
            this.dgvNote.Size = new System.Drawing.Size(941, 313);
            this.dgvNote.TabIndex = 42;
            this.dgvNote.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNote_CellContentClick);
            this.dgvNote.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvNote_RowPrePaint);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Image = global::ServiceRecords.Properties.Resources.exit_8633;
            this.btClose.Location = new System.Drawing.Point(921, 367);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 40;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btReportNoteSingleDeps
            // 
            this.btReportNoteSingleDeps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btReportNoteSingleDeps.Image = global::ServiceRecords.Properties.Resources.x_office_spreadsheet;
            this.btReportNoteSingleDeps.Location = new System.Drawing.Point(50, 367);
            this.btReportNoteSingleDeps.Name = "btReportNoteSingleDeps";
            this.btReportNoteSingleDeps.Size = new System.Drawing.Size(32, 32);
            this.btReportNoteSingleDeps.TabIndex = 49;
            this.btReportNoteSingleDeps.UseVisualStyleBackColor = true;
            this.btReportNoteSingleDeps.Click += new System.EventHandler(this.btReportNoteSingleDeps_Click);
            // 
            // btListNotePeriod
            // 
            this.btListNotePeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btListNotePeriod.Image = global::ServiceRecords.Properties.Resources.klpq_2511;
            this.btListNotePeriod.Location = new System.Drawing.Point(12, 367);
            this.btListNotePeriod.Name = "btListNotePeriod";
            this.btListNotePeriod.Size = new System.Drawing.Size(32, 32);
            this.btListNotePeriod.TabIndex = 50;
            this.btListNotePeriod.UseVisualStyleBackColor = true;
            this.btListNotePeriod.Click += new System.EventHandler(this.btListNotePeriod_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(19, 341);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(17, 17);
            this.pictureBox1.TabIndex = 51;
            this.pictureBox1.TabStop = false;
            // 
            // lbHighlightInPayment
            // 
            this.lbHighlightInPayment.AutoSize = true;
            this.lbHighlightInPayment.Location = new System.Drawing.Point(46, 343);
            this.lbHighlightInPayment.Name = "lbHighlightInPayment";
            this.lbHighlightInPayment.Size = new System.Drawing.Size(200, 13);
            this.lbHighlightInPayment.TabIndex = 52;
            this.lbHighlightInPayment.Text = "- получатель отличается от заказчика";
            // 
            // cControl
            // 
            this.cControl.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cControl.DataPropertyName = "isControl";
            this.cControl.HeaderText = "V";
            this.cControl.MinimumWidth = 45;
            this.cControl.Name = "cControl";
            this.cControl.Width = 45;
            // 
            // cDate
            // 
            this.cDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cDate.DataPropertyName = "DataSumma";
            dataGridViewCellStyle7.Format = "d";
            dataGridViewCellStyle7.NullValue = null;
            this.cDate.DefaultCellStyle = dataGridViewCellStyle7;
            this.cDate.HeaderText = "Дата";
            this.cDate.MinimumWidth = 85;
            this.cDate.Name = "cDate";
            this.cDate.ReadOnly = true;
            this.cDate.Width = 85;
            // 
            // cNumber
            // 
            this.cNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cNumber.DataPropertyName = "Number";
            this.cNumber.HeaderText = "Номер СЗ";
            this.cNumber.MinimumWidth = 85;
            this.cNumber.Name = "cNumber";
            this.cNumber.ReadOnly = true;
            this.cNumber.Width = 85;
            // 
            // cFio
            // 
            this.cFio.DataPropertyName = "FIO";
            this.cFio.HeaderText = "ФИО";
            this.cFio.Name = "cFio";
            this.cFio.ReadOnly = true;
            // 
            // cSumma
            // 
            this.cSumma.DataPropertyName = "Summa";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.Format = "N2";
            dataGridViewCellStyle8.NullValue = null;
            this.cSumma.DefaultCellStyle = dataGridViewCellStyle8;
            this.cSumma.HeaderText = "Сумма";
            this.cSumma.Name = "cSumma";
            this.cSumma.ReadOnly = true;
            // 
            // cComment
            // 
            this.cComment.DataPropertyName = "Description";
            this.cComment.HeaderText = "Описание";
            this.cComment.Name = "cComment";
            this.cComment.ReadOnly = true;
            // 
            // cPass
            // 
            this.cPass.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.cPass.DataPropertyName = "nameType";
            this.cPass.HeaderText = "";
            this.cPass.MinimumWidth = 100;
            this.cPass.Name = "cPass";
            this.cPass.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cPass.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // cOperation
            // 
            this.cOperation.DataPropertyName = "Type";
            this.cOperation.HeaderText = "Операция";
            this.cOperation.Name = "cOperation";
            this.cOperation.ReadOnly = true;
            this.cOperation.Visible = false;
            // 
            // tbItogo
            // 
            this.tbItogo.Location = new System.Drawing.Point(440, 341);
            this.tbItogo.Name = "tbItogo";
            this.tbItogo.ReadOnly = true;
            this.tbItogo.Size = new System.Drawing.Size(201, 20);
            this.tbItogo.TabIndex = 53;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(394, 344);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 52;
            this.label1.Text = "Итого:";
            // 
            // btTakeMoney
            // 
            this.btTakeMoney.Location = new System.Drawing.Point(647, 338);
            this.btTakeMoney.Name = "btTakeMoney";
            this.btTakeMoney.Size = new System.Drawing.Size(118, 23);
            this.btTakeMoney.TabIndex = 54;
            this.btTakeMoney.Text = "Выдать все";
            this.btTakeMoney.UseVisualStyleBackColor = true;
            this.btTakeMoney.Click += new System.EventHandler(this.btTakeMoney_Click);
            // 
            // frmViewPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 407);
            this.ControlBox = false;
            this.Controls.Add(this.btTakeMoney);
            this.Controls.Add(this.tbItogo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbHighlightInPayment);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btReportNoteSingleDeps);
            this.Controls.Add(this.btListNotePeriod);
            this.Controls.Add(this.dgvNote);
            this.Controls.Add(this.btClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmViewPayment";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmViewPayment";
            this.Load += new System.EventHandler(this.frmViewPayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.DataGridView dgvNote;
        private System.Windows.Forms.Button btReportNoteSingleDeps;
        private System.Windows.Forms.Button btListNotePeriod;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbHighlightInPayment;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cControl;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFio;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSumma;
        private System.Windows.Forms.DataGridViewTextBoxColumn cComment;
        private System.Windows.Forms.DataGridViewButtonColumn cPass;
        private System.Windows.Forms.DataGridViewTextBoxColumn cOperation;
        private System.Windows.Forms.TextBox tbItogo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btTakeMoney;

    }
}