using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ServiceRecords.docmoverDZ
{
    public partial class frmSelect : Form
    {
        public int id_ListServiceRecords { set; private get; }
        private Dictionary<int, string> dicListMemorandum = new Dictionary<int, string>();
        private EnumerableRowCollection<DataRow> rowCollectToSend;
        public decimal sumDZ { get; private set; }
        public string textDZ { get; private set; }
        public Dictionary<int, string> getListMemorandum()
        {
            return dicListMemorandum;
        }

        public EnumerableRowCollection<DataRow> getRowCollect()
        {
            return rowCollectToSend;
        }

        public void setListMemorandum(Dictionary<int, string> dirLM)
        {
            dicListMemorandum = dirLM;
        }

        private DataTable dtData, dtSingle;
        public frmSelect()
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
            dgvSingle.AutoGenerateColumns = false;
            dtpDate.Value = DateTime.Now.AddDays(1 - DateTime.Now.Day);            
            dtpEnd.Value = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.Day);
            ToolTip tp = new ToolTip();
            tp.SetToolTip(btClose, "Выход");
            tp.SetToolTip(btEdit, "Изменить");
            tp.SetToolTip(btSave, "Выбрать");
            tp.SetToolTip(btUpdate, "Обновить");

        }

        private void frmSelect_Load(object sender, EventArgs e)
        {
            getData();
        }

        private void getData()
        {
            dtData = Config.hCntMain.getMemorandums(dtpDate.Value.Date, dtpEnd.Value.Date, id_ListServiceRecords,true);

            if (dtData != null && dtData.Rows.Count > 0)
            {
                foreach (int key in dicListMemorandum.Keys)
                {
                    EnumerableRowCollection<DataRow> rowCollect = dtData.AsEnumerable().Where(r => r.Field<int>("id") == key);
                    if (rowCollect.Count() > 0)
                        rowCollect.First()["isSelect"] = true;
                }
            }

            setFilter();
            dgvData.DataSource = dtData;
        }

        private void dtpDate_Leave(object sender, EventArgs e)
        {
            getData();
        }

        private void dtpDate_CloseUp(object sender, EventArgs e)
        {
            getData();
        }

        private void dgvData_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow == null || dgvData.CurrentRow.Index == -1 || dtData == null || dtData.DefaultView.Count == 0)
            {
                dgvSingle.DataSource = null;
                return;
            }

            int id_Memorandums = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id"];
            dtSingle = Config.hCntMain.getListViolation(id_Memorandums);
            dgvSingle.DataSource = dtSingle;
        }

        private void dgvData_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            int width = 0;
            foreach (DataGridViewColumn col in dgvData.Columns)
            {
                if (!col.Visible) continue;

                if (col.Name.Equals(cNum.Name))
                {
                    tbNum.Location = new Point(dgvData.Location.X + 1 + width, tbNum.Location.Y);
                    tbNum.Size = new Size(cNum.Width, tbNum.Size.Height);
                }

                if (col.Name.Equals(cDeps.Name))
                {
                    tbDeps.Location = new Point(dgvData.Location.X + 1 + width, tbNum.Location.Y);
                    tbDeps.Size = new Size(cDeps.Width, tbNum.Size.Height);
                }

                if (col.Name.Equals(cTitle.Name))
                {
                    tbTitle.Location = new Point(dgvData.Location.X + 1 + width, tbNum.Location.Y);
                    tbTitle.Size = new Size(cTitle.Width, tbNum.Size.Height);
                }

                if (col.Name.Equals(cTypeDz.Name))
                {
                    tbTypeDz.Location = new Point(dgvData.Location.X + 1 + width, tbNum.Location.Y);
                    tbTypeDz.Size = new Size(cTypeDz.Width, tbNum.Size.Height);
                }

                width += col.Width;
            }
        }

        private void dgvData_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {

                Color rColor = Color.White;
                if ((bool)dtData.DefaultView[e.RowIndex]["isEdit"])
                    rColor = panel6.BackColor;

                dgvData.Rows[e.RowIndex].DefaultCellStyle.BackColor =  dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;

                dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;

                //if (!(bool)dtTable.DefaultView[e.RowIndex]["isUse"])
                    //dgvTable.Rows[e.RowIndex].Cells[cFieldName.Index].Style.BackColor =
                      //   dgvTable.Rows[e.RowIndex].Cells[cFieldName.Index].Style.SelectionBackColor = panel1.BackColor;
            }
        }

        private void dgvData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            //Рисуем рамку для выделеной строки
            if (dgv.Rows[e.RowIndex].Selected)
            {
                int width = dgv.Width;
                Rectangle r = dgv.GetRowDisplayRectangle(e.RowIndex, false);
                Rectangle rect = new Rectangle(r.X, r.Y, width - 1, r.Height - 1);

                ControlPaint.DrawBorder(e.Graphics, rect,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid);
            }
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow == null || dgvData.CurrentRow.Index == -1 || dtData == null || dtData.DefaultView.Count == 0) return;

            DataRowView row = dtData.DefaultView[dgvData.CurrentRow.Index];
            if (new frmEdit() { row = row }.ShowDialog() == DialogResult.OK)
                dtData.AcceptChanges();

        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (dtData == null || dtData.Rows.Count == 0) { MessageBox.Show("Выберите ДЗ", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            EnumerableRowCollection<DataRow> rowCollect = dtData.AsEnumerable().Where(r => r.Field<bool>("isSelect"));
            rowCollectToSend = rowCollect;
            if (rowCollect.Count() == 0) { MessageBox.Show("Выберите ДЗ", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            var groupBonus = rowCollect.GroupBy(r => new { FIOBonus = r.Field<string>("FIOBonus") })
                .Select(s => new
                {
                    s.Key.FIOBonus,
                    SumBonus = s.Sum(r => r.Field<decimal>("SumBonus"))
                });       

            dicListMemorandum.Clear();
            sumDZ = 0;
            foreach (DataRow row in rowCollect)
            {
                string str = $"{row["FIOBonus"]} - {row["SumBonus"]}";
                int _id = (int)row["id"];
                dicListMemorandum.Add(_id, str);
                sumDZ += (decimal)row["SumBonus"];
            }

            textDZ = "";
            int npp = 1;
            foreach (var v in groupBonus)
            {
                textDZ += $"{npp}.{v.FIOBonus} - {v.SumBonus}" + Environment.NewLine;
                npp++;
            }

            this.DialogResult = DialogResult.OK;

        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpDate.Value.Date > dtpEnd.Value.Date)
                dtpEnd.Value = dtpDate.Value.Date;
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            if (dtpDate.Value.Date > dtpEnd.Value.Date)
                dtpDate.Value = dtpEnd.Value.Date;
        }

        private void tbNum_TextChanged(object sender, EventArgs e)
        {
            setFilter();
        }
              
        private void setFilter()
        {
            if (dtData == null || dtData.Rows.Count == 0)
            {
                btEdit.Enabled  = false;
                return;
            }

            try
            {
                string filter = "";

                if (tbNum.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"no_doc like '%{tbNum.Text.Trim()}%'";

                if (tbDeps.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"depPenalty like '%{tbDeps.Text.Trim()}%'";

                if (tbTitle.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"cname like '%{tbTitle.Text.Trim()}%'";

                if (tbTypeDz.Text.Trim().Length != 0)
                    filter += (filter.Length == 0 ? "" : " and ") + $"DistrType like '%{tbTypeDz.Text.Trim()}%'";

                dtData.DefaultView.RowFilter = filter;
            }
            catch
            {
                dtData.DefaultView.RowFilter = "id = -1";
            }
            finally
            {
                btEdit.Enabled = dtData.DefaultView.Count != 0;
                dgvData_SelectionChanged(null, null);
            }
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            getData();            
        }

        private void dgvSingle_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex != -1 && dtSingle != null && dtSingle.DefaultView.Count != 0)
            {
                Color rColor = Color.White;
                dgvSingle.Rows[e.RowIndex].DefaultCellStyle.BackColor = dgvSingle.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;
                dgvSingle.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
            }
        }
    }
}
