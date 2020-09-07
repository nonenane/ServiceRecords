namespace ServiceRecords.workDoc
{
    partial class frmSetReport
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
            this.label1 = new System.Windows.Forms.Label();
            this.lbSumma = new System.Windows.Forms.Label();
            this.lbSummaGetReturn = new System.Windows.Forms.Label();
            this.tbSumma = new System.Windows.Forms.TextBox();
            this.txNumberSR = new System.Windows.Forms.TextBox();
            this.lbSummaInValuta = new System.Windows.Forms.Label();
            this.tbSummaAmountReceived = new System.Windows.Forms.TextBox();
            this.lbSummaInReport = new System.Windows.Forms.Label();
            this.tbSummaInReport = new System.Windows.Forms.TextBox();
            this.lbDebt = new System.Windows.Forms.Label();
            this.tbDebt = new System.Windows.Forms.TextBox();
            this.btnAddDocument = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbTypeOrderMoney = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Номер СЗ";
            // 
            // lbSumma
            // 
            this.lbSumma.AutoSize = true;
            this.lbSumma.Location = new System.Drawing.Point(16, 53);
            this.lbSumma.Name = "lbSumma";
            this.lbSumma.Size = new System.Drawing.Size(113, 13);
            this.lbSumma.TabIndex = 1;
            this.lbSumma.Text = "Общая сумма по СЗ:";
            // 
            // lbSummaGetReturn
            // 
            this.lbSummaGetReturn.AutoSize = true;
            this.lbSummaGetReturn.Location = new System.Drawing.Point(16, 89);
            this.lbSummaGetReturn.Name = "lbSummaGetReturn";
            this.lbSummaGetReturn.Size = new System.Drawing.Size(40, 13);
            this.lbSummaGetReturn.TabIndex = 2;
            this.lbSummaGetReturn.Text = "Взято:";
            // 
            // tbSumma
            // 
            this.tbSumma.Enabled = false;
            this.tbSumma.Location = new System.Drawing.Point(132, 50);
            this.tbSumma.Name = "tbSumma";
            this.tbSumma.Size = new System.Drawing.Size(64, 20);
            this.tbSumma.TabIndex = 3;
            // 
            // txNumberSR
            // 
            this.txNumberSR.Enabled = false;
            this.txNumberSR.Location = new System.Drawing.Point(132, 14);
            this.txNumberSR.Name = "txNumberSR";
            this.txNumberSR.Size = new System.Drawing.Size(64, 20);
            this.txNumberSR.TabIndex = 4;
            // 
            // lbSummaInValuta
            // 
            this.lbSummaInValuta.AutoSize = true;
            this.lbSummaInValuta.Location = new System.Drawing.Point(213, 53);
            this.lbSummaInValuta.Name = "lbSummaInValuta";
            this.lbSummaInValuta.Size = new System.Drawing.Size(45, 13);
            this.lbSummaInValuta.TabIndex = 5;
            this.lbSummaInValuta.Text = "Валюта";
            // 
            // tbSummaAmountReceived
            // 
            this.tbSummaAmountReceived.Enabled = false;
            this.tbSummaAmountReceived.Location = new System.Drawing.Point(132, 85);
            this.tbSummaAmountReceived.Name = "tbSummaAmountReceived";
            this.tbSummaAmountReceived.Size = new System.Drawing.Size(64, 20);
            this.tbSummaAmountReceived.TabIndex = 7;
            this.tbSummaAmountReceived.TextChanged += new System.EventHandler(this.tbSummaInReport_TextChanged);
            // 
            // lbSummaInReport
            // 
            this.lbSummaInReport.AutoSize = true;
            this.lbSummaInReport.Location = new System.Drawing.Point(16, 118);
            this.lbSummaInReport.Name = "lbSummaInReport";
            this.lbSummaInReport.Size = new System.Drawing.Size(80, 13);
            this.lbSummaInReport.TabIndex = 10;
            this.lbSummaInReport.Text = "Сумма отчета:";
            // 
            // tbSummaInReport
            // 
            this.tbSummaInReport.Location = new System.Drawing.Point(132, 115);
            this.tbSummaInReport.Name = "tbSummaInReport";
            this.tbSummaInReport.Size = new System.Drawing.Size(64, 20);
            this.tbSummaInReport.TabIndex = 11;
            this.tbSummaInReport.TextChanged += new System.EventHandler(this.tbSummaInReport_TextChanged);
            this.tbSummaInReport.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSummaInReport_KeyPress);
            this.tbSummaInReport.Leave += new System.EventHandler(this.tbSummaInReport_Leave);
            // 
            // lbDebt
            // 
            this.lbDebt.AutoSize = true;
            this.lbDebt.Location = new System.Drawing.Point(17, 148);
            this.lbDebt.Name = "lbDebt";
            this.lbDebt.Size = new System.Drawing.Size(36, 13);
            this.lbDebt.TabIndex = 12;
            this.lbDebt.Text = "Долг:";
            // 
            // tbDebt
            // 
            this.tbDebt.Enabled = false;
            this.tbDebt.Location = new System.Drawing.Point(132, 145);
            this.tbDebt.Name = "tbDebt";
            this.tbDebt.Size = new System.Drawing.Size(64, 20);
            this.tbDebt.TabIndex = 13;
            // 
            // btnAddDocument
            // 
            this.btnAddDocument.Location = new System.Drawing.Point(12, 178);
            this.btnAddDocument.Name = "btnAddDocument";
            this.btnAddDocument.Size = new System.Drawing.Size(117, 32);
            this.btnAddDocument.TabIndex = 14;
            this.btnAddDocument.Text = "Добавить документ";
            this.btnAddDocument.UseVisualStyleBackColor = true;
            this.btnAddDocument.Click += new System.EventHandler(this.tsmiAddDoc_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = global::ServiceRecords.Properties.Resources.save_edit;
            this.btnSave.Location = new System.Drawing.Point(166, 179);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(32, 32);
            this.btnSave.TabIndex = 15;
            this.toolTip1.SetToolTip(this.btnSave, "Сохранить");
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btClose
            // 
            this.btClose.Image = global::ServiceRecords.Properties.Resources.exit_8633;
            this.btClose.Location = new System.Drawing.Point(212, 179);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 40;
            this.toolTip1.SetToolTip(this.btClose, "Выход");
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(213, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 41;
            this.label2.Text = "RUB";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(213, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 42;
            this.label3.Text = "RUB";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(213, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 43;
            this.label4.Text = "RUB";
            // 
            // cbTypeOrderMoney
            // 
            this.cbTypeOrderMoney.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypeOrderMoney.Enabled = false;
            this.cbTypeOrderMoney.FormattingEnabled = true;
            this.cbTypeOrderMoney.Location = new System.Drawing.Point(202, 13);
            this.cbTypeOrderMoney.Name = "cbTypeOrderMoney";
            this.cbTypeOrderMoney.Size = new System.Drawing.Size(75, 21);
            this.cbTypeOrderMoney.TabIndex = 51;
            this.cbTypeOrderMoney.SelectedIndexChanged += new System.EventHandler(this.cbTypeOrderMoney_SelectedIndexChanged);
            // 
            // frmSetReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 228);
            this.ControlBox = false;
            this.Controls.Add(this.cbTypeOrderMoney);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAddDocument);
            this.Controls.Add(this.tbDebt);
            this.Controls.Add(this.lbDebt);
            this.Controls.Add(this.tbSummaInReport);
            this.Controls.Add(this.lbSummaInReport);
            this.Controls.Add(this.tbSummaAmountReceived);
            this.Controls.Add(this.lbSummaInValuta);
            this.Controls.Add(this.txNumberSR);
            this.Controls.Add(this.tbSumma);
            this.Controls.Add(this.lbSummaGetReturn);
            this.Controls.Add(this.lbSumma);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(310, 360);
            this.MinimizeBox = false;
            this.Name = "frmSetReport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Предоставить отчет";
            this.Load += new System.EventHandler(this.frmSetReport_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbSumma;
        private System.Windows.Forms.Label lbSummaGetReturn;
        private System.Windows.Forms.TextBox tbSumma;
        private System.Windows.Forms.TextBox txNumberSR;
        private System.Windows.Forms.Label lbSummaInValuta;
        private System.Windows.Forms.TextBox tbSummaAmountReceived;
        private System.Windows.Forms.Label lbSummaInReport;
        private System.Windows.Forms.TextBox tbSummaInReport;
        private System.Windows.Forms.Label lbDebt;
        private System.Windows.Forms.TextBox tbDebt;
        private System.Windows.Forms.Button btnAddDocument;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbTypeOrderMoney;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}