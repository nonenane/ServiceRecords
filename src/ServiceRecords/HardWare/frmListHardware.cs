using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ServiceRecords.HardWare
{
    public partial class frmListHardware : Form
    {
        public int id_ServiceRecod { set; private get; }
        private DataTable dtData;
        public frmListHardware()
        {
            InitializeComponent();
            dgvNote.AutoGenerateColumns = false;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmListHardware_Load(object sender, EventArgs e)
        {
            dtData = Config.hCntMain.getListHardwareForServiceRecord(id_ServiceRecod);
           

            if (dtData == null || dtData.Rows.Count == 0) return;

            tbNumSZ.Text = dtData.Rows[0]["number"].ToString();
            dgvNote.DataSource = dtData;
        }

        private void dgvNote_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            int width = 0;
            foreach (DataGridViewColumn col in dgvNote.Columns)
            {
                if (!col.Visible) continue;

                if (col.Name.Equals(cNum.Name))
                {
                    tbNumber.Location = new Point(dgvNote.Location.X + width + 1, tbNumber.Location.Y);
                    tbNumber.Size = new Size(cNum.Width, tbNumber.Size.Height);
                }
                else
                    if (col.Name.Equals(cEan.Name))
                {
                    tbEan.Location = new Point(dgvNote.Location.X + width + 1, tbNumber.Location.Y);
                    tbEan.Size = new Size(cEan.Width, tbNumber.Size.Height);
                }
                else if (col.Name.Equals(cName.Name))
                {
                    tbName.Location = new Point(dgvNote.Location.X + width + 1, tbNumber.Location.Y);
                    tbName.Size = new Size(cName.Width, tbNumber.Size.Height);
                }

                width += col.Width;
            }
        }

        private void tbNumber_TextChanged(object sender, EventArgs e)
        {
            setFilter();
        }

        private void setFilter()
        {
            if (dtData == null || dtData.Rows.Count == 0)
            {
                return;
            }

            try
            {
                string filter = "";

                if (tbNumber.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"InventoryNumber like '%{tbNumber.Text.Trim()}%'";
                
                if (tbEan.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"EAN like '%{tbEan.Text.Trim()}%'";
                
                if (tbName.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"cName like '%{tbName.Text.Trim()}%'";


                dtData.DefaultView.RowFilter = filter;
            }
            catch
            {
                dtData.DefaultView.RowFilter = "id = -1";
            }
            finally
            {
            }
        }
    }
}
