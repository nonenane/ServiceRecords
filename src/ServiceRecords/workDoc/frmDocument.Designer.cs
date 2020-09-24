namespace ServiceRecords.workDoc
{
    partial class frmDocument
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btEditName = new System.Windows.Forms.Button();
            this.btAddFile = new System.Windows.Forms.Button();
            this.btView = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.btScan = new System.Windows.Forms.Button();
            this.btZoomIn = new System.Windows.Forms.Button();
            this.btZoomOut = new System.Windows.Forms.Button();
            this.dgvScan = new System.Windows.Forms.DataGridView();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbNameImg = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.imagePanel1 = new ServiceRecords.ImagePanel();
            this.btSave = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScan)).BeginInit();
            this.SuspendLayout();
            // 
            // btEditName
            // 
            this.btEditName.Image = global::ServiceRecords.Properties.Resources.edit_1761;
            this.btEditName.Location = new System.Drawing.Point(327, 353);
            this.btEditName.Name = "btEditName";
            this.btEditName.Size = new System.Drawing.Size(32, 32);
            this.btEditName.TabIndex = 8;
            this.toolTip1.SetToolTip(this.btEditName, "Переименовать файл");
            this.btEditName.UseVisualStyleBackColor = true;
            this.btEditName.Click += new System.EventHandler(this.btEditName_Click);
            // 
            // btAddFile
            // 
            this.btAddFile.Image = global::ServiceRecords.Properties.Resources.folder_htm_5356;
            this.btAddFile.Location = new System.Drawing.Point(367, 353);
            this.btAddFile.Name = "btAddFile";
            this.btAddFile.Size = new System.Drawing.Size(32, 32);
            this.btAddFile.TabIndex = 8;
            this.toolTip1.SetToolTip(this.btAddFile, "Добавить файл");
            this.btAddFile.UseVisualStyleBackColor = true;
            this.btAddFile.Click += new System.EventHandler(this.btAddFile_Click);
            // 
            // btView
            // 
            this.btView.Image = global::ServiceRecords.Properties.Resources.find_9299;
            this.btView.Location = new System.Drawing.Point(685, 353);
            this.btView.Name = "btView";
            this.btView.Size = new System.Drawing.Size(32, 32);
            this.btView.TabIndex = 8;
            this.toolTip1.SetToolTip(this.btView, "Просмотр");
            this.btView.UseVisualStyleBackColor = true;
            this.btView.Click += new System.EventHandler(this.btView_Click);
            // 
            // btClose
            // 
            this.btClose.Image = global::ServiceRecords.Properties.Resources.exit_8633;
            this.btClose.Location = new System.Drawing.Point(725, 353);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 8;
            this.toolTip1.SetToolTip(this.btClose, "Выход");
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btScan
            // 
            this.btScan.Image = global::ServiceRecords.Properties.Resources.scanner;
            this.btScan.Location = new System.Drawing.Point(645, 353);
            this.btScan.Name = "btScan";
            this.btScan.Size = new System.Drawing.Size(32, 32);
            this.btScan.TabIndex = 9;
            this.toolTip1.SetToolTip(this.btScan, "Сканировать");
            this.btScan.UseVisualStyleBackColor = true;
            this.btScan.Click += new System.EventHandler(this.btScan_Click);
            // 
            // btZoomIn
            // 
            this.btZoomIn.Image = global::ServiceRecords.Properties.Resources.zoom_in;
            this.btZoomIn.Location = new System.Drawing.Point(451, 353);
            this.btZoomIn.Name = "btZoomIn";
            this.btZoomIn.Size = new System.Drawing.Size(32, 32);
            this.btZoomIn.TabIndex = 11;
            this.toolTip1.SetToolTip(this.btZoomIn, "Увеличить");
            this.btZoomIn.UseVisualStyleBackColor = true;
            this.btZoomIn.Click += new System.EventHandler(this.btZoomIn_Click);
            // 
            // btZoomOut
            // 
            this.btZoomOut.Image = global::ServiceRecords.Properties.Resources.zoom_out;
            this.btZoomOut.Location = new System.Drawing.Point(416, 353);
            this.btZoomOut.Name = "btZoomOut";
            this.btZoomOut.Size = new System.Drawing.Size(32, 32);
            this.btZoomOut.TabIndex = 12;
            this.toolTip1.SetToolTip(this.btZoomOut, "Уменьшить");
            this.btZoomOut.UseVisualStyleBackColor = true;
            this.btZoomOut.Click += new System.EventHandler(this.btZoomOut_Click);
            // 
            // dgvScan
            // 
            this.dgvScan.AllowUserToAddRows = false;
            this.dgvScan.AllowUserToDeleteRows = false;
            this.dgvScan.AllowUserToResizeRows = false;
            this.dgvScan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvScan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvScan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvScan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cName});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvScan.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgvScan.Location = new System.Drawing.Point(12, 38);
            this.dgvScan.MultiSelect = false;
            this.dgvScan.Name = "dgvScan";
            this.dgvScan.ReadOnly = true;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvScan.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvScan.RowHeadersVisible = false;
            this.dgvScan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvScan.Size = new System.Drawing.Size(299, 304);
            this.dgvScan.TabIndex = 13;
            this.dgvScan.CurrentCellChanged += new System.EventHandler(this.dgvScan_CurrentCellChanged);
            this.dgvScan.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvScan_RowPostPaint);
            this.dgvScan.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvScan_RowPrePaint);
            // 
            // cName
            // 
            this.cName.DataPropertyName = "cName";
            this.cName.HeaderText = "Имя файла";
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            // 
            // tbNameImg
            // 
            this.tbNameImg.Location = new System.Drawing.Point(12, 12);
            this.tbNameImg.Name = "tbNameImg";
            this.tbNameImg.Size = new System.Drawing.Size(299, 20);
            this.tbNameImg.TabIndex = 14;
            this.tbNameImg.TextChanged += new System.EventHandler(this.tbNameImg_TextChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(153)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(12, 359);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(21, 21);
            this.panel1.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(84, 359);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(21, 21);
            this.panel2.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 363);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Отчёт";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(111, 363);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Безнал.";
            // 
            // imagePanel1
            // 
            this.imagePanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imagePanel1.CanvasSize = new System.Drawing.Size(600, 400);
            this.imagePanel1.Image = null;
            this.imagePanel1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            this.imagePanel1.Location = new System.Drawing.Point(326, 12);
            this.imagePanel1.Name = "imagePanel1";
            this.imagePanel1.Size = new System.Drawing.Size(431, 330);
            this.imagePanel1.TabIndex = 0;
            this.imagePanel1.Zoom = 1F;
            // 
            // btSave
            // 
            this.btSave.Image = global::ServiceRecords.Properties.Resources.pict_save;
            this.btSave.Location = new System.Drawing.Point(593, 353);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(32, 32);
            this.btSave.TabIndex = 17;
            this.toolTip1.SetToolTip(this.btSave, "Сохранить файл");
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // frmDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 391);
            this.ControlBox = false;
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tbNameImg);
            this.Controls.Add(this.dgvScan);
            this.Controls.Add(this.btEditName);
            this.Controls.Add(this.btAddFile);
            this.Controls.Add(this.btView);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btScan);
            this.Controls.Add(this.btZoomIn);
            this.Controls.Add(this.btZoomOut);
            this.Controls.Add(this.imagePanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDocument";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление документа";
            this.Load += new System.EventHandler(this.frmDocument_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvScan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ImagePanel imagePanel1;
        private System.Windows.Forms.Button btScan;
        private System.Windows.Forms.Button btZoomIn;
        private System.Windows.Forms.Button btZoomOut;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btView;
        private System.Windows.Forms.Button btAddFile;
        private System.Windows.Forms.Button btEditName;
        private System.Windows.Forms.DataGridView dgvScan;
        private System.Windows.Forms.TextBox tbNameImg;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}