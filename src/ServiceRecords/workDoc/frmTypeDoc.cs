using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ServiceRecords.workDoc
{
    public partial class frmTypeDoc : Form
    {
        public int typeDoc = 0;
        public frmTypeDoc()
        {
            InitializeComponent();
            ToolTip tt = new ToolTip();
            tt.SetToolTip(btClose, "Выход");
            tt.SetToolTip(btSelect, "Подтвердить");
            if (typeDoc == 3) rbType3.Checked = true;
        }

        public frmTypeDoc(int typeDoc)
        {
            InitializeComponent();
            ToolTip tt = new ToolTip();
            tt.SetToolTip(btClose, "Выход");
            tt.SetToolTip(btSelect, "Подтвердить");
            this.typeDoc = typeDoc;
            if (typeDoc == 3) rbType3.Checked = true;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            if (typeDoc == 3 && rbType3.Checked != true)
            {
                MessageBox.Show("У вас отчет по тратам ДС по СЗ", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                rbType3.Checked = true;
            }
            if (rbType1.Checked) typeDoc = 1;
            else if (rbType2.Checked) typeDoc = 2;
            else if (rbType3.Checked) typeDoc = 3;

            this.DialogResult = DialogResult.OK;
        }
    }
}
