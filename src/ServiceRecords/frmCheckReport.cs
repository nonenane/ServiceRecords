using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Nwuram.Framework.Logging;
using Nwuram.Framework.ToExcel;

namespace ServiceRecords
{
    public partial class frmCheckReport : Form
    {
        DataTable dtReport = new DataTable();
        int countcolV = 0;
        public frmCheckReport()
        {
            InitializeComponent();
        }

        private void frmCheckReport_Load(object sender, EventArgs e)
        {
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

        }
        private void createCmbStatusReport()
        {
            DataTable dt = createDt();
            dt.Rows.Add(0, "Все");
            dt.Rows.Add(1, "Отчет предоставлен");
            dt.Rows.Add(2, "Ожидание отчета");
            dt.Rows.Add(3, "Отчет отклонен");
            cmbStatusReport.DataSource = dt;
            cmbStatusReport.DisplayMember = "name";
            cmbStatusReport.ValueMember = "id";
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
            dtReport = Config.hCntMain.getReportForCheck(DateTime.Parse(dateTimeStart.Value.ToShortDateString()), DateTime.Parse(dateTimeEnd.Value.ToShortDateString()));
            dgvReport.DataSource = dtReport;
            Filter();
        }

        private void Filter()
        {
            try
            {
                string filter = "";
                filter += cmbStatusReport.SelectedIndex == 1 ? "id_Status = 15" :
                          cmbStatusReport.SelectedIndex == 2 ? "id_Status = 14" :
                          cmbStatusReport.SelectedIndex == 3 ? "id_Status = 19" : ""; // id_Status
                filter += filter.Length != 0 && cmbDebt.SelectedIndex  != 0 ? " AND " : "";
                filter += cmbDebt.SelectedIndex == 1 ? "DebtReport > 0" :
                          cmbDebt.SelectedIndex == 2 ? "DebtReport = 0" : " ";

                dtReport.DefaultView.RowFilter = filter;
            }
            catch { }
        }

        private void cmbStatusReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filter();
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

        private void btnPrintReport_Click(object sender, EventArgs e)
        {
            if (dgvReport != null ? dgvReport.Rows.Count > 0 ? true: false: false)
                PrintReport();
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

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
