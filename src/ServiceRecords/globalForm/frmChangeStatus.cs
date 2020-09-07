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
    public partial class frmChangeStatus : Form
    {
        public int getStatus { private set; get; }
        public int id_ServiceRecords { set; private get; }
        private DataTable dtStatus;
        public frmChangeStatus()
        {
            InitializeComponent();
            ToolTip tt = new ToolTip();
            tt.SetToolTip(btClose, "Выход");
            tt.SetToolTip(btSelect, "Сохранить");
        }

        private void frmChangeStatus_Load(object sender, EventArgs e)
        {
             dtStatus = Config.hCntMain.getStatus();
             cmbStatus.DataSource = dtStatus;
             cmbStatus.DisplayMember = "cName";
             cmbStatus.ValueMember = "id";
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            if (cmbStatus.SelectedValue == null)
            {
                MessageBox.Show("Необходимо выбрать статус!", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            getStatus = (int)cmbStatus.SelectedValue;

            this.DialogResult = DialogResult.OK;
        }

        
    }
}
