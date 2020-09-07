namespace ServiceRecords.workDoc
{
    partial class frmTypeDoc
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
            this.rbType1 = new System.Windows.Forms.RadioButton();
            this.rbType2 = new System.Windows.Forms.RadioButton();
            this.rbType3 = new System.Windows.Forms.RadioButton();
            this.btSelect = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rbType1
            // 
            this.rbType1.AutoSize = true;
            this.rbType1.Checked = true;
            this.rbType1.Location = new System.Drawing.Point(28, 21);
            this.rbType1.Name = "rbType1";
            this.rbType1.Size = new System.Drawing.Size(274, 17);
            this.rbType1.TabIndex = 0;
            this.rbType1.TabStop = true;
            this.rbType1.Text = "к описанию СЗ (при создании и редактировании)";
            this.rbType1.UseVisualStyleBackColor = true;
            // 
            // rbType2
            // 
            this.rbType2.AutoSize = true;
            this.rbType2.Location = new System.Drawing.Point(28, 44);
            this.rbType2.Name = "rbType2";
            this.rbType2.Size = new System.Drawing.Size(277, 17);
            this.rbType2.TabIndex = 0;
            this.rbType2.Text = "при оплате безналом (обязательное добавление)";
            this.rbType2.UseVisualStyleBackColor = true;
            // 
            // rbType3
            // 
            this.rbType3.AutoSize = true;
            this.rbType3.Location = new System.Drawing.Point(28, 67);
            this.rbType3.Name = "rbType3";
            this.rbType3.Size = new System.Drawing.Size(157, 17);
            this.rbType3.TabIndex = 0;
            this.rbType3.Text = "отчет по тратам ДС по СЗ";
            this.rbType3.UseVisualStyleBackColor = true;
            // 
            // btSelect
            // 
            this.btSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSelect.Image = global::ServiceRecords.Properties.Resources.Select;
            this.btSelect.Location = new System.Drawing.Point(255, 108);
            this.btSelect.Name = "btSelect";
            this.btSelect.Size = new System.Drawing.Size(32, 32);
            this.btSelect.TabIndex = 9;
            this.btSelect.UseVisualStyleBackColor = true;
            this.btSelect.Click += new System.EventHandler(this.btSelect_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.Image = global::ServiceRecords.Properties.Resources.exit_8633;
            this.btClose.Location = new System.Drawing.Point(293, 108);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 9;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // frmTypeDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 148);
            this.ControlBox = false;
            this.Controls.Add(this.btSelect);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.rbType3);
            this.Controls.Add(this.rbType2);
            this.Controls.Add(this.rbType1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTypeDoc";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор типа добавляемого документа";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbType1;
        private System.Windows.Forms.RadioButton rbType2;
        private System.Windows.Forms.RadioButton rbType3;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btSelect;
    }
}