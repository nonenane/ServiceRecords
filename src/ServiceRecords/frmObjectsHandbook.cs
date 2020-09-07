using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace ServiceRecords
{
    public partial class frmObjectsHandbook : Form
    {
        DataTable dtObjects = new DataTable();
        public frmObjectsHandbook()
        {
            InitializeComponent();
            Grid_Load();
        }
        private void Grid_Load()
        {
            dtObjects = Config.hCntMain.getObjects();
            dgvObjectsHandbook.DataSource = dtObjects;

        }

        private void btnAddObject_Click(object sender, EventArgs e)
        {
            AddNewObject();
            Refresh();
        }

        private void btnEditObject_Click(object sender, EventArgs e)
        {
            EditObject();
            Refresh();
        }

        private void btnDeleteObject_Click(object sender, EventArgs e)
        {
            DeleteObject();
            Refresh();
        }

        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void tbSearchObject_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }
        private void cbVievWorkObjects_CheckedChanged(object sender, EventArgs e)
        {
            ViewRows();
        }
        private void dgvObjectsHandbook_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            
        }

        private void AddNewObject()
        {
            workDoc.frmAddEditObject frm = new workDoc.frmAddEditObject();
            frm.Show();
            Refresh();
        }

        private void DeleteObject()
        {
            Config.hCntMain.addEditObject(dgvObjectsHandbook.CurrentRow.Cells[1].Value.ToString(), 1, int.Parse(dgvObjectsHandbook.CurrentRow.Cells[0].Value.ToString()));
            Refresh();
        }

        private void EditObject()
        {
            workDoc.frmAddEditObject frm = new workDoc.frmAddEditObject();
            frm.id = int.Parse(dgvObjectsHandbook.CurrentRow.Cells[0].Value.ToString());
            frm.str = dgvObjectsHandbook.CurrentRow.Cells[1].Value.ToString();
            frm.Show();
            Refresh();
        }

        private void Filter()
        {
            string filter = "";
            if (cbVievWorkObjects.Checked)
                filter += "" + "CONVERT(is_Active, 'System.Int32')  = 0";
            else 
                filter += "" + "CONVERT(is_Active, 'System.Int32')  = 1";

            dtObjects.DefaultView.RowFilter = filter;
        }

        private void Refresh()
        {
            Grid_Load();
        }

        private void ViewRows()
        {

        }




    }
}
