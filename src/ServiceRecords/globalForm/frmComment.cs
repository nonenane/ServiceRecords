using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ServiceRecords.globalForm
{
    public partial class frmComment : Form
    {
        private bool isEdit = false;
        public string getComment { private set; get; }
        public bool isCommentAdd { set; private get; }
        public frmComment()
        {
            InitializeComponent();
            ToolTip tt = new ToolTip();
            tt.SetToolTip(btClose, "Выход");
            tt.SetToolTip(btSelect, "Сохранить");
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            if (isCommentAdd)
                if (tbComments.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Необходимо ввести комментарий!", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            isEdit = false;
            getComment = tbComments.Text.Trim();
            this.DialogResult = DialogResult.OK;
        }

        private void frmComment_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = isEdit && DialogResult.No == MessageBox.Show(Config.centralText("На форме есть несохраненные данные.\nЗакрыть форму без сохранения?\n"), "Выход из программы", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        private void tbComments_TextChanged(object sender, EventArgs e)
        {
            isEdit = true;
        }
    }
}
