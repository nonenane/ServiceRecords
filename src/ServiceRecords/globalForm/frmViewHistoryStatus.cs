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
    public partial class frmViewHistoryStatus : Form
    {
        public int id_ServiceRecords { private get; set; }
        private DataTable dtHistoryStatus;

        public frmViewHistoryStatus()
        {
            InitializeComponent();
            ToolTip tt = new ToolTip();
            tt.SetToolTip(btClose, "Выход");
            dgvStatus.AutoGenerateColumns = false;
        }

        private void frmViewHistoryStatus_Load(object sender, EventArgs e)
        {
            getData();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void getData()
        {
            dtHistoryStatus = Config.hCntMain.getHistoryStatus(id_ServiceRecords);
            if (dtHistoryStatus != null && dtHistoryStatus.Columns.Contains("DateChange"))
                dtHistoryStatus.DefaultView.Sort = "DateChange ASC";

            dgvStatus.DataSource = dtHistoryStatus;
        }
    }
}
