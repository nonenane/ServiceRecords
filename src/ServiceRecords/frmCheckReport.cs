using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Logging;
using Nwuram.Framework.Settings.User;
using Nwuram.Framework.ToExcel;

namespace ServiceRecords
{    
    public partial class frmCheckReport : Form
    {
        public bool isFartForward { set; private get; }
        DataTable dtReport = new DataTable();
        int countcolV = 0;
        public frmCheckReport()
        {
            InitializeComponent();                       
        }

        private void frmCheckReport_Load(object sender, EventArgs e)
        {
            colV.Visible = btnAccept.Visible = btnRefuse.Visible = !new List<string>(new string[] { "РКВ", "КНТ" }).Contains(Config.CodeUser);

            btViewHardwareList.Visible = label5.Visible = label6.Visible = dateTimeEnd.Visible = dateTimeStart.Visible = lbStatusReport.Visible = cmbStatusReport.Visible = btnUpdate.Visible = !isFartForward;

            if (isFartForward)
            {
                lbHasDebt.Location = label5.Location;
                cmbDebt.Location = new Point(lbHasDebt.Location.X + 88, lbHasDebt.Location.Y-4);
            }

            setDopElements();
            createCmbStatusReport();
            createCmbDebt();
            createGrid();
        }
        private void setDopElements()
        {
            dgvReport.AutoGenerateColumns = false;
            ToolTip tt = new ToolTip();
            tt.SetToolTip(btnPrintReport, "Отчет");
            tt.SetToolTip(btnAccept, "Подтвердить");
            tt.SetToolTip(btnRefuse, "Отклонить");
            tt.SetToolTip(btnUpdate, "Обновить");
            tt.SetToolTip(btClose, "Выход");
            tt.SetToolTip(btViewHardwareList, "Просмотр компьютерного оборудования");

        }
        private void createCmbStatusReport()
        {
            DataTable dt = createDt();
            dt.Rows.Add(0, "Все");
            dt.Rows.Add(1, "Отчет предоставлен");
            dt.Rows.Add(2, "Ожидание отчета");
            dt.Rows.Add(3, "Отчет отклонен");
            dt.Rows.Add(4, "Отчет  подтвержден");
            cmbStatusReport.DataSource = dt;
            cmbStatusReport.DisplayMember = "name";
            cmbStatusReport.ValueMember = "id";

            if (new List<string>(new string[] { "ОП" }).Contains(Config.CodeUser))
                cmbStatusReport.SelectedValue = 1;

        }

        private void createCmbDebt()
        {
            DataTable dt = createDt();
            dt.Rows.Add(0, "Все");
            dt.Rows.Add(1, "Есть долг");
            dt.Rows.Add(2, "Нет долга");
            cmbDebt.DataSource = dt;
            cmbDebt.DisplayMember = "name";
            cmbDebt.ValueMember = "id";
        }

        private void createGrid()
        {
            dateTimeStart.Value = dateTimeEnd.Value.AddDays(-7);
            getData();
            Filter();
        }
        private DataTable createDt()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("name", typeof(string));
            return dt;
        }

        private void dgvReport_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            dgvReport.Rows[e.RowIndex].DefaultCellStyle.BackColor = double.Parse(dtReport.DefaultView[e.RowIndex]["DebtReport"].ToString()) > 0 ? panel4.BackColor : Color.White;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            getData();
            Filter();
        }

        private void getData()
        {
            dtReport = Config.hCntMain.getReportForCheck(DateTime.Parse(dateTimeStart.Value.ToShortDateString()), DateTime.Parse(dateTimeEnd.Value.ToShortDateString()), isFartForward);
            if (isFartForward && (dtReport == null || dtReport.Rows.Count == 0)) Close();
            Filter();
            dgvReport.DataSource = dtReport;
        }

        private void Filter()
        {
            if (dtReport == null || dtReport.Rows.Count == 0) {

                btViewHardwareList.Enabled = false;
                btnPrintReport.Enabled = false; return; 
            }
            try
            {                
                string filter = "";

                if (new List<string>(new string[] { "РКВ" }).Contains(Config.CodeUser))
                    //filter += (filter.Trim().Length == 0 ? "" : " and ") + $"id_Creator  = {Nwuram.Framework.Settings.User.UserSettings.User.Id}";
                    filter += "( id_Department = '" + UserSettings.User.IdDepartment + "' OR id_Block = '" + UserSettings.User.IdDepartment + "' )";


                filter += cmbStatusReport.SelectedIndex == 1 ? "id_Status = 15" :
                          cmbStatusReport.SelectedIndex == 2 ? "id_Status = 14" :
                          cmbStatusReport.SelectedIndex == 3 ? "id_Status = 19" :
                          cmbStatusReport.SelectedIndex == 4 ? "id_Status = 20" : ""; // id_Status
                filter += filter.Length != 0 && cmbDebt.SelectedIndex != 0 ? " AND " : "";
                filter += cmbDebt.SelectedIndex == 1 ? "DebtReport > 0" :
                          cmbDebt.SelectedIndex == 2 ? "DebtReport = 0" : " ";

                if (tbNumber.Text.Trim().Length != 0)
                    filter += (filter.Trim().Length == 0 ? "" : " and ") + $"CONVERT(Number,'System.String') like '%{tbNumber.Text.Trim()}%'";

                if (tbDiscript.Text.Trim().Length != 0)
                    filter += (filter.Trim().Length == 0 ? "" : " and ") + $"Description like '%{tbDiscript.Text.Trim()}%'";

                dtReport.DefaultView.RowFilter = filter;
                btnPrintReport.Enabled = dtReport.DefaultView.Count != 0;
                dgvReport_SelectionChanged(null, null);
            }
            catch //(Exception ex)
            {
                btViewHardwareList.Enabled = false;
                btnPrintReport.Enabled = false;
            }
        }


        private void cmbDebt_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            setNewStatus(sender, e, (int)Config.StatusSZ.Отчет_подтвержден);
              getData();
        }

        private void btnRefuse_Click(object sender, EventArgs e)
        {
            setNewStatus(sender, e, (int)Config.StatusSZ.Отчет_отклонен);

            getData();
        }

        private void setNewStatus(object sender, EventArgs e, int status)
        {
            DataTable dt = Config.hCntMain.getReportForCheck(DateTime.Parse(dateTimeStart.Value.ToShortDateString()), DateTime.Parse(dateTimeEnd.Value.ToShortDateString()));
            for (int i = 0; i < dgvReport.Rows.Count; i++)
            {
                if (dgvReport.Rows[i].Cells["ColV"].Value == null) continue;
                if ((bool)dgvReport.Rows[i].Cells["ColV"].Value == false) continue;





                DataTable dtResult = null;

                if (status == (int)Config.StatusSZ.Отчет_подтвержден)
                {
                    int id = (int)dtReport.DefaultView[dgvReport.Rows[i].Index]["id"];

                    //Для обычной СЗ
                    if (checkReportDebt(int.Parse(dgvReport.Rows[i].Cells["id"].Value.ToString()))
                            && dt.Select("id = " + dgvReport.Rows[i].Cells["id"].Value.ToString()).ToList().Count() > 0) // для проверки текущего статуса СЗ
                        dtResult = Config.hCntMain.updateStatus(id, (int)Config.StatusSZ.Отчет_подтвержден);

                    foreach (DataRow l in dt.Select("id = " + dgvReport.Rows[i].Cells["id"].Value.ToString()).ToList())
                        Config.hCntMain.updateStatusReport((int)l["id_report"], 1);
                    //Config.hCntMain.updateStatusReport((int)dgvReport.Rows[i].Cells["id_report"].Value, 1);
                }
                else if (status == (int)Config.StatusSZ.Отчет_отклонен)
                {
                    if (dt.Select("id = " + dgvReport.Rows[i].Cells["id"].Value.ToString()).ToList().Count() > 0)
                        dtResult = Config.hCntMain.updateStatus((int)dtReport.DefaultView[dgvReport.Rows[i].Index]["id"], status);

                    foreach (DataRow l in dt.Select("id = " + dgvReport.Rows[i].Cells["id"].Value.ToString()).ToList())
                        Config.hCntMain.updateStatusReport((int)l["id_report"], 2);
                    //Config.hCntMain.updateStatusReport((int)dgvReport.Rows[i].Cells["id_report"].Value, 2);
                }

                Logging.StartFirstLevel((int)Config.StatusSZ.Отчет_подтвержден == status ? 1505 : 1506);
                Logging.Comment("СЗ Id: " + dtReport.DefaultView[i]["id"].ToString() + " ;Номер: " + dtReport.DefaultView[i]["Number"].ToString());
                Logging.Comment("Описание: " + dtReport.DefaultView[i]["Description"].ToString());
                Logging.Comment("Сумма: " + dtReport.DefaultView[i]["Summa"].ToString() + " " + dtReport.DefaultView[i]["Valuta"].ToString());
                Logging.Comment("Взято: " + dtReport.DefaultView[i]["sumGet"].ToString());
                Logging.Comment("Сумма отчета: " + dtReport.DefaultView[i]["SummaReport"].ToString());
                Logging.Comment("Долг: " + dtReport.DefaultView[i]["DebtReport"].ToString());
                Logging.Comment("Дата и время предоставления отчета: " + dtReport.DefaultView[i]["DateEdit"].ToString());
                Logging.Comment("Тип оплаты : " + dtReport.DefaultView[i]["typeCashNonCash"].ToString());

                if (dtResult != null && dtResult.Rows.Count > 0 && !dtResult.Columns.Contains("error"))
                {
                    Logging.Comment("Статус ДО ID: " + dtResult.Rows[0]["id_prev"].ToString() + "; Наименование: " + dtResult.Rows[0]["cName_prev"].ToString());
                    Logging.Comment("Статус После ID: " + dtResult.Rows[0]["id"].ToString() + "; Наименование: " + dtResult.Rows[0]["cName"].ToString());
                }

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();

            }
        }

        private bool checkReportDebt(int idServiceRecords)
        {
            bool mix = (bool)Config.hCntMain.getServiceRecordsBody(idServiceRecords).Rows[0]["Mix"];
            if (mix)
            {
                return decimal.Parse(Config.hCntMain.getHistoryOrderAndReturnMix(idServiceRecords).Rows[0]["debtReportCash"].ToString()) <= 0
                    && decimal.Parse(Config.hCntMain.getHistoryOrderAndReturnMix(idServiceRecords).Rows[0]["debtReportNonCash"].ToString()) <= 0;
            }
            else return decimal.Parse(Config.hCntMain.getHistoryOrderAndReturn(idServiceRecords).Rows[0]["debtReport"].ToString()) <= 0;

        }

        private void dgvReport_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtReport == null || dtReport.DefaultView.Count == 0 || dgvReport.CurrentRow == null || dgvReport.CurrentRow.Index == -1) return;
            int id = (int)dtReport.DefaultView[dgvReport.CurrentRow.Index]["id"];

            frmServiceNote frmS = new frmServiceNote();
            frmS.id = id;
            frmS.Text = "Просмотр СЗ";
            frmS.setIsView();
            if (DialogResult.OK == frmS.ShowDialog())
            {
                getData();
            }
        }

        private void setWidthColumn(int indexRow, int indexCol, int width, Nwuram.Framework.ToExcelNew.ExcelUnLoad report)
        {
            report.SetColumnWidth(indexRow, indexCol, indexRow, indexCol, width);
        }

        private void btnPrintReport_Click(object sender, EventArgs e)
        {
            if (dtReport.DefaultView.Count == 0)
            {
                MessageBox.Show("Нет данных для выгрузки отчёта!","Выгрузка отчёта",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }


            Logging.StartFirstLevel(79);

            Logging.Comment("Произведена выгрузка отчета по проверке отчетов, со следующими параметрами");
            Logging.Comment($"Период с {dateTimeStart.Value.ToShortDateString()} по {dateTimeEnd.Value.ToShortDateString()}");
            Logging.Comment($"Статус:{cmbStatusReport.Text}");
            Logging.Comment($"Наличие долга:{cmbDebt.Text}");

            Logging.StopFirstLevel();

            Nwuram.Framework.ToExcelNew.ExcelUnLoad report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad();

            int indexRow = 1;

            int maxColumns = 0;

            foreach (DataGridViewColumn col in dgvReport.Columns)
                if (col.Visible && !col.Name.Equals(colV.Name))
                {
                    maxColumns++;
                    if (col.Name.Equals("Number")) setWidthColumn(indexRow, maxColumns, 20, report);
                    if (col.Name.Equals("Description")) setWidthColumn(indexRow, maxColumns, 22, report);
                    if (col.Name.Equals("Summa")) setWidthColumn(indexRow, maxColumns, 22, report);
                    if (col.Name.Equals("Valuta")) setWidthColumn(indexRow, maxColumns, 13, report);
                    if (col.Name.Equals("sumGet")) setWidthColumn(indexRow, maxColumns, 20, report);
                    if (col.Name.Equals("SummaReport")) setWidthColumn(indexRow, maxColumns, 18, report);
                    if (col.Name.Equals("DebtReport")) setWidthColumn(indexRow, maxColumns, 16, report);
                    if (col.Name.Equals("DateEdit")) setWidthColumn(indexRow, maxColumns, 10, report);
                    if (col.Name.Equals("typeCashNonCash")) setWidthColumn(indexRow, maxColumns, 10, report);
                    if (col.Name.Equals("nameStatusReport")) setWidthColumn(indexRow, maxColumns, 10, report);
                }

            #region "Head"
            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Проверка отчётов", indexRow, 1);
            report.SetFontBold(indexRow, 1, indexRow, 1);
            report.SetFontSize(indexRow, 1, indexRow, 1, 16);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 1);
            indexRow++;
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Период с {dateTimeStart.Value.ToShortDateString()} по {dateTimeEnd.Value.ToShortDateString()}", indexRow, 1);
            indexRow++;


            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Статус: {cmbStatusReport.Text}", indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"{lbHasDebt.Text}: {cmbDebt.Text}", indexRow, 1);
            indexRow++;

            if (tbNumber.Text.Trim().Length != 0 || tbDiscript.Text.Trim().Length != 0)
            {
                report.Merge(indexRow, 1, indexRow, maxColumns);
                report.AddSingleValue($"Фильтры:" +
                    $"{(tbNumber.Text.Trim().Length==0?"":$"{Number.HeaderText}:{tbNumber.Text}")} " +
                    $"{(tbDiscript.Text.Trim().Length == 0 ? "" : $"{Description.HeaderText}:{tbDiscript.Text}")}", indexRow, 1);
                indexRow++;
            }

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue("Выгрузил: " + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername, indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue("Дата выгрузки: " + DateTime.Now.ToString(), indexRow, 1);
            indexRow++;
            indexRow++;
            #endregion

            int indexCol = 0;
            foreach (DataGridViewColumn col in dgvReport.Columns)
                if (col.Visible && !col.Name.Equals(colV.Name))
                {
                    indexCol++;
                    report.AddSingleValue(col.HeaderText, indexRow, indexCol);
                }
            report.SetFontBold(indexRow, 1, indexRow, maxColumns);
            report.SetBorders(indexRow, 1, indexRow, maxColumns);
            report.SetWrapText(indexRow, 1, indexRow, maxColumns);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
            report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);
            indexRow++;

            foreach (DataRowView row in dtReport.DefaultView)
            {
                indexCol = 1;
                report.SetWrapText(indexRow, indexCol, indexRow, maxColumns);
                foreach (DataGridViewColumn col in dgvReport.Columns)
                {
                    if (col.Visible && !col.Name.Equals(colV.Name))
                    {
                        if (row[col.DataPropertyName] is DateTime)
                            report.AddSingleValue(((DateTime)row[col.DataPropertyName]).ToShortDateString(), indexRow, indexCol);
                        else
                         if (row[col.DataPropertyName] is bool)
                            report.AddSingleValue((bool)row[col.DataPropertyName] ? "Да" : "Нет", indexRow, indexCol);
                        else
                           if (row[col.DataPropertyName] is decimal)
                        {
                            report.AddSingleValueObject(row[col.DataPropertyName], indexRow, indexCol);
                            report.SetFormat(indexRow, indexCol, indexRow, indexCol, "0.00");
                        }
                        else
                            report.AddSingleValue(row[col.DataPropertyName].ToString(), indexRow, indexCol);

                        indexCol++;
                    }
                }

                report.SetBorders(indexRow, 1, indexRow, maxColumns);
                report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
                report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);
                indexRow++;
            }
            report.SetPageSetup(1, 999, true);
            report.Show();
        }

        private void PrintReport()
        {
            DataTable dt = ((DataTable)dgvReport.DataSource).Copy();
            dt.Columns.Remove("colV");
            dt.Columns.Remove("id");
            dt.Columns.Remove("id_Status");
            Report temp = new Report();
            if (Report.OOAvailable || Report.ExcelAvailable)
            {
                temp.AddMultiValues(dt, "dt");
                if (!temp.CreateTemplate(Application.StartupPath + "\\Templates\\Report", Application.StartupPath + "\\Report", null))
                {
                    MessageBox.Show(temp.ErrorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            temp.OpenFile(Application.StartupPath + "\\Report");
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void dgvReport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0) return;

            if (dgvReport.CurrentRow.Cells["colV"].Value == null) dgvReport.CurrentRow.Cells["colV"].Value = false;

            if (dgvReport.CurrentRow.Cells["colV"].Value != null && e.ColumnIndex == 0 && (bool)dgvReport.CurrentRow.Cells["colV"].Value == false)
            {
                if (dgvReport.CurrentRow.Cells["DateEdit"].Value.ToString().Length < 5 
                    || double.Parse(dgvReport.CurrentRow.Cells["DebtReport"].Value.ToString()) > 0
                    || int.Parse(dgvReport.CurrentRow.Cells["id_Status"].Value.ToString()) == 19
                    || int.Parse(dgvReport.CurrentRow.Cells["id_Status"].Value.ToString()) == 14)
                {
                    dgvReport.CurrentRow.Cells["colV"].Value = false; return;
                }
                dgvReport.CurrentRow.Cells["colV"].Value = true;
                foreach (DataGridViewRow r in dgvReport.Rows)
                    if (r.Cells["Number"].Value.ToString() == dgvReport.CurrentRow.Cells["Number"].Value.ToString())
                    {
                        r.Cells["colV"].Value = true;
                        countcolV++;
                    }
                countcolV++;
            }
            else
            {
                dgvReport.CurrentRow.Cells["colV"].Value = false;
                foreach (DataGridViewRow r in dgvReport.Rows)
                    if (r.Cells["Number"].Value.ToString() == dgvReport.CurrentRow.Cells["Number"].Value.ToString())
                    {
                        r.Cells["colV"].Value = false;
                        countcolV--;
                    }
                countcolV--;
            }

            btnAccept.Enabled = btnRefuse.Enabled = countcolV != 0;
        }

        private void lbDebt_Click(object sender, EventArgs e)
        {

        }

        private void cmbStatusReport_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Filter();
        }

        private void dgvReport_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            int width = 0;
            foreach (DataGridViewColumn col in dgvReport.Columns)
            {
                if (!col.Visible) continue;

                if (col.Name.Equals(Number.Name))
                {
                    tbNumber.Location = new Point(dgvReport.Location.X + width + 1, tbNumber.Location.Y);
                    tbNumber.Size = new Size(Number.Width, tbNumber.Size.Height);
                }
                else
                    if (col.Name.Equals(Description.Name))
                {
                    tbDiscript.Location = new Point(dgvReport.Location.X + width + 1, tbNumber.Location.Y);
                    tbDiscript.Size = new Size(Description.Width, tbNumber.Size.Height);
                }
              

                width += col.Width;
            }
        }

        private void tbNumber_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void dgvReport_SelectionChanged(object sender, EventArgs e)
        {
            if(dtReport==null || dtReport.Rows.Count==0)
            {
                btViewHardwareList.Enabled = false;
                return; 
            }
            if (dgvReport.CurrentRow == null || dgvReport.CurrentRow.Index == -1)
            {
                btViewHardwareList.Enabled = false;
                return;
            }
            try
            {
                if (dtReport.DefaultView[dgvReport.CurrentRow.Index]["inType"] == DBNull.Value)
                {
                    btViewHardwareList.Enabled = false; return;
                }

                btViewHardwareList.Enabled = (int)dtReport.DefaultView[dgvReport.CurrentRow.Index]["inType"] == 1;
            }
            catch
            {
                btViewHardwareList.Enabled = false;
            }
        }

        private void btViewHardwareList_Click(object sender, EventArgs e)
        {
            int id_ServiceRecod = (int)dtReport.DefaultView[dgvReport.CurrentRow.Index]["id"];
            new HardWare.frmListHardware() { id_ServiceRecod = id_ServiceRecod }.ShowDialog();
        }
    }
}
