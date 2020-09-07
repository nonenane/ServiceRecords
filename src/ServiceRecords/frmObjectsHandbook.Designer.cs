namespace ServiceRecords
{
    partial class frmObjectsHandbook
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
            this.dgvObjectsHandbook = new System.Windows.Forms.DataGridView();
            this.tbSearchObject = new System.Windows.Forms.TextBox();
            this.cbVievWorkObjects = new System.Windows.Forms.CheckBox();
            this.btnAddObject = new System.Windows.Forms.Button();
            this.btnEditObject = new System.Windows.Forms.Button();
            this.btnDeleteObject = new System.Windows.Forms.Button();
            this.btnCloseForm = new System.Windows.Forms.Button();
            this.lbDontWork = new System.Windows.Forms.Label();
            this.picbDontWork = new System.Windows.Forms.PictureBox();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colObjects = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_Active = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvObjectsHandbook)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbDontWork)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvObjectsHandbook
            // 
            this.dgvObjectsHandbook.AllowUserToAddRows = false;
            this.dgvObjectsHandbook.AllowUserToDeleteRows = false;
            this.dgvObjectsHandbook.AllowUserToResizeRows = false;
            this.dgvObjectsHandbook.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvObjectsHandbook.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.colObjects,
            this.is_Active});
            this.dgvObjectsHandbook.Location = new System.Drawing.Point(17, 35);
            this.dgvObjectsHandbook.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvObjectsHandbook.MultiSelect = false;
            this.dgvObjectsHandbook.Name = "dgvObjectsHandbook";
            this.dgvObjectsHandbook.ReadOnly = true;
            this.dgvObjectsHandbook.RowHeadersVisible = false;
            this.dgvObjectsHandbook.RowTemplate.Height = 24;
            this.dgvObjectsHandbook.Size = new System.Drawing.Size(265, 385);
            this.dgvObjectsHandbook.TabIndex = 0;
            this.dgvObjectsHandbook.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvObjectsHandbook_RowPrePaint);
            // 
            // tbSearchObject
            // 
            this.tbSearchObject.Location = new System.Drawing.Point(18, 11);
            this.tbSearchObject.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbSearchObject.Name = "tbSearchObject";
            this.tbSearchObject.Size = new System.Drawing.Size(265, 20);
            this.tbSearchObject.TabIndex = 1;
            this.tbSearchObject.TextChanged += new System.EventHandler(this.tbSearchObject_TextChanged);
            // 
            // cbVievWorkObjects
            // 
            this.cbVievWorkObjects.AutoSize = true;
            this.cbVievWorkObjects.Location = new System.Drawing.Point(17, 442);
            this.cbVievWorkObjects.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbVievWorkObjects.Name = "cbVievWorkObjects";
            this.cbVievWorkObjects.Size = new System.Drawing.Size(15, 14);
            this.cbVievWorkObjects.TabIndex = 2;
            this.cbVievWorkObjects.UseVisualStyleBackColor = true;
            this.cbVievWorkObjects.CheckedChanged += new System.EventHandler(this.cbVievWorkObjects_CheckedChanged);
            // 
            // btnAddObject
            // 
            this.btnAddObject.BackgroundImage = global::ServiceRecords.Properties.Resources.document_add;
            this.btnAddObject.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAddObject.Location = new System.Drawing.Point(144, 432);
            this.btnAddObject.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAddObject.Name = "btnAddObject";
            this.btnAddObject.Size = new System.Drawing.Size(30, 32);
            this.btnAddObject.TabIndex = 3;
            this.btnAddObject.UseVisualStyleBackColor = true;
            this.btnAddObject.Click += new System.EventHandler(this.btnAddObject_Click);
            // 
            // btnEditObject
            // 
            this.btnEditObject.BackgroundImage = global::ServiceRecords.Properties.Resources.edit_1761;
            this.btnEditObject.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnEditObject.Location = new System.Drawing.Point(181, 432);
            this.btnEditObject.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnEditObject.Name = "btnEditObject";
            this.btnEditObject.Size = new System.Drawing.Size(30, 32);
            this.btnEditObject.TabIndex = 4;
            this.btnEditObject.UseVisualStyleBackColor = true;
            this.btnEditObject.Click += new System.EventHandler(this.btnEditObject_Click);
            // 
            // btnDeleteObject
            // 
            this.btnDeleteObject.BackgroundImage = global::ServiceRecords.Properties.Resources.document_delete;
            this.btnDeleteObject.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDeleteObject.Location = new System.Drawing.Point(218, 432);
            this.btnDeleteObject.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDeleteObject.Name = "btnDeleteObject";
            this.btnDeleteObject.Size = new System.Drawing.Size(30, 32);
            this.btnDeleteObject.TabIndex = 5;
            this.btnDeleteObject.UseVisualStyleBackColor = true;
            this.btnDeleteObject.Click += new System.EventHandler(this.btnDeleteObject_Click);
            // 
            // btnCloseForm
            // 
            this.btnCloseForm.BackgroundImage = global::ServiceRecords.Properties.Resources.pict_close;
            this.btnCloseForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCloseForm.Location = new System.Drawing.Point(253, 432);
            this.btnCloseForm.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCloseForm.Name = "btnCloseForm";
            this.btnCloseForm.Size = new System.Drawing.Size(30, 32);
            this.btnCloseForm.TabIndex = 6;
            this.btnCloseForm.UseVisualStyleBackColor = true;
            this.btnCloseForm.Click += new System.EventHandler(this.btnCloseForm_Click);
            // 
            // lbDontWork
            // 
            this.lbDontWork.AutoSize = true;
            this.lbDontWork.Location = new System.Drawing.Point(54, 442);
            this.lbDontWork.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbDontWork.Name = "lbDontWork";
            this.lbDontWork.Size = new System.Drawing.Size(89, 13);
            this.lbDontWork.TabIndex = 7;
            this.lbDontWork.Text = "- неработающие";
            // 
            // picbDontWork
            // 
            this.picbDontWork.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.picbDontWork.Location = new System.Drawing.Point(34, 443);
            this.picbDontWork.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.picbDontWork.Name = "picbDontWork";
            this.picbDontWork.Size = new System.Drawing.Size(14, 14);
            this.picbDontWork.TabIndex = 8;
            this.picbDontWork.TabStop = false;
            // 
            // id
            // 
            this.id.DataPropertyName = "id_Object";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // colObjects
            // 
            this.colObjects.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colObjects.DataPropertyName = "objects";
            this.colObjects.HeaderText = "                        Наименование объекта";
            this.colObjects.Name = "colObjects";
            this.colObjects.ReadOnly = true;
            // 
            // is_Active
            // 
            this.is_Active.DataPropertyName = "is_Active";
            this.is_Active.HeaderText = "is_Active";
            this.is_Active.Name = "is_Active";
            this.is_Active.ReadOnly = true;
            this.is_Active.Visible = false;
            // 
            // frmObjectsHandbook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 482);
            this.Controls.Add(this.picbDontWork);
            this.Controls.Add(this.lbDontWork);
            this.Controls.Add(this.btnCloseForm);
            this.Controls.Add(this.btnDeleteObject);
            this.Controls.Add(this.btnEditObject);
            this.Controls.Add(this.btnAddObject);
            this.Controls.Add(this.cbVievWorkObjects);
            this.Controls.Add(this.tbSearchObject);
            this.Controls.Add(this.dgvObjectsHandbook);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmObjectsHandbook";
            this.Text = "Справочник объектов";
            ((System.ComponentModel.ISupportInitialize)(this.dgvObjectsHandbook)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbDontWork)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvObjectsHandbook;
        private System.Windows.Forms.TextBox tbSearchObject;
        private System.Windows.Forms.CheckBox cbVievWorkObjects;
        private System.Windows.Forms.Button btnAddObject;
        private System.Windows.Forms.Button btnEditObject;
        private System.Windows.Forms.Button btnDeleteObject;
        private System.Windows.Forms.Button btnCloseForm;
        private System.Windows.Forms.Label lbDontWork;
        private System.Windows.Forms.PictureBox picbDontWork;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn colObjects;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_Active;
    }
}