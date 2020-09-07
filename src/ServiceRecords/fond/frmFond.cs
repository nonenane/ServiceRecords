using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ServiceRecords.fond
{
    public partial class frmFond : Form
    {
        public int selectId { get; private set; }
        public string selectSrtData { get; private set; }
        public decimal Summa { get; private set; }

        private DataTable dtData;
        public int idSZ { set; private get; }
        public int TypeServiceRecordOnTime { set; private get; }

        public frmFond()
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;

            //Random rnd = new Random();

            //int r = rnd.Next(0, 255);
            //int g = rnd.Next(0, 255);
            //int b = rnd.Next(0, 255);

            //panel1.BackColor = Color.FromArgb(r, g, b);

            panel1.BackColor = Color.Aquamarine;

            ToolTip tp = new ToolTip();
            tp.SetToolTip(btClose, "Выход");
            tp.SetToolTip(btSelect, "Выбрать");
        }

        private void frmFond_Load(object sender, EventArgs e)
        {
            getData();
        }

        private void dgvData_Paint(object sender, PaintEventArgs e)
        {
            tbNum.Size = new Size(cNum.Width, tbNum.Height);
            tbNum.Location = new Point(dgvData.Location.X,tbNum.Location.Y);

            tbInfo.Size = new Size(cInfo.Width, tbInfo.Height);
            tbInfo.Location = new Point(dgvData.Location.X + cNum.Width + cDate.Width, tbNum.Location.Y);            
        }

        private void tbNum_TextChanged(object sender, EventArgs e)
        {
            setFilter();
        }

        private void getData()
        {
            dtData = Config.hCntMain.getFondSelect(idSZ, TypeServiceRecordOnTime);
            dgvData.DataSource = dtData;
            setFilter();            
        }

        private void setFilter()
        {
            if (dtData == null || dtData.Rows.Count == 0) { btSelect.Enabled = false; return; }

            try
            {
                string filter = "";

                if (!string.IsNullOrEmpty(tbInfo.Text.Trim()))
                    filter += (filter.Trim().Length == 0 ? "" : " and ") + $"Description like '%{tbInfo.Text.Trim()}%'";

                if (!string.IsNullOrEmpty(tbNum.Text.Trim()))
                    filter += (filter.Trim().Length == 0 ? "" : " and ") + $"CONVERT(Number,'System.String') like '%{tbNum.Text.Trim()}%'";

                dtData.DefaultView.RowFilter = filter;
            }
            catch {
                dtData.DefaultView.RowFilter = "id = -1";
            }
            finally {
                btSelect.Enabled = dtData.DefaultView.Count != 0;
                dgvData_SelectionChanged(null, null);
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

        private void dgvData_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex != -1 && dtData != null && dtData.DefaultView.Count != 0)
            {
                Color rColor = Color.White;
                if ((decimal)dtData.DefaultView[e.RowIndex]["resultSum"] <= 0)
                    rColor = panel1.BackColor;

                dgvData.Rows[e.RowIndex].DefaultCellStyle.BackColor = rColor;
                dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;
                dgvData.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;                
            }
        }

        private void dgvData_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            btSelect_Click(null, null);
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            if ((decimal)dtData.DefaultView[dgvData.CurrentRow.Index]["resultSum"] <= 0)
                return;

            selectId = (int)dtData.DefaultView[dgvData.CurrentRow.Index]["id"];
            selectSrtData = $"№{dtData.DefaultView[dgvData.CurrentRow.Index]["Number"].ToString()} на {dtData.DefaultView[dgvData.CurrentRow.Index]["sumString"].ToString()} от {((DateTime)dtData.DefaultView[dgvData.CurrentRow.Index]["DateConfirmationD"]).ToShortDateString()}";
            Summa = (decimal)dtData.DefaultView[dgvData.CurrentRow.Index]["Summa"];
            this.DialogResult = DialogResult.OK;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void dgvData_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvData.CurrentRow == null || dgvData.CurrentRow.Index == -1 || dtData == null || dtData.DefaultView.Count == 0)
            {
                btSelect.Enabled = false;
                return;
            }

            try
            {
                btSelect.Enabled = (decimal)dtData.DefaultView[dgvData.CurrentRow.Index]["resultSum"] > 0;
            }
            catch {
                btSelect.Enabled = false;
            }

        }
    }
}
