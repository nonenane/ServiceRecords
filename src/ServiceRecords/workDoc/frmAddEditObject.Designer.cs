namespace ServiceRecords.workDoc
{
    partial class frmAddEditObject
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
            this.tbStr = new System.Windows.Forms.TextBox();
            this.btnCloseForm = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbStr
            // 
            this.tbStr.Location = new System.Drawing.Point(12, 22);
            this.tbStr.Name = "tbStr";
            this.tbStr.Size = new System.Drawing.Size(270, 20);
            this.tbStr.TabIndex = 0;
            // 
            // btnCloseForm
            // 
            this.btnCloseForm.BackgroundImage = global::ServiceRecords.Properties.Resources.pict_close;
            this.btnCloseForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCloseForm.Location = new System.Drawing.Point(252, 47);
            this.btnCloseForm.Margin = new System.Windows.Forms.Padding(2);
            this.btnCloseForm.Name = "btnCloseForm";
            this.btnCloseForm.Size = new System.Drawing.Size(30, 32);
            this.btnCloseForm.TabIndex = 7;
            this.btnCloseForm.UseVisualStyleBackColor = true;
            this.btnCloseForm.Click += new System.EventHandler(this.btnCloseForm_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImage = global::ServiceRecords.Properties.Resources.pict_save;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSave.Location = new System.Drawing.Point(212, 47);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(30, 32);
            this.btnSave.TabIndex = 8;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmAddEditObject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 89);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCloseForm);
            this.Controls.Add(this.tbStr);
            this.Name = "frmAddEditObject";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавить/Редактировать";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbStr;
        private System.Windows.Forms.Button btnCloseForm;
        private System.Windows.Forms.Button btnSave;
    }
}