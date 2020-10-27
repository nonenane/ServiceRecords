using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportPays
{
    public partial class frmReport : Form
    {
        private DataTable dtBonus;
        public frmReport()
        {
            InitializeComponent();
            dgvStatus.AutoGenerateColumns = false;
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            cmbObject.DataSource = Config.hCntMain.GetObjects(true);
            cmbObject.DisplayMember = "cname";
            cmbObject.ValueMember = "id";


            cmbDepsBonus.DataSource = Config.hCntMain.getDepartmentBonus(true);
            cmbDepsBonus.DisplayMember = "nameDep";
            cmbDepsBonus.ValueMember = "id";


            cmbType.DataSource = Config.hCntMain.GetTypeWorks(true);
            cmbType.DisplayMember = "cname";
            cmbType.ValueMember = "id";


            cmbDeps.DataSource = Config.hCntMain.getDepartments(true);
            cmbDeps.DisplayMember = "cName";
            cmbDeps.ValueMember = "id";

            cmbTypeDoc.DataSource = Config.hCntMain.dmGetDocTypes(true);
            cmbTypeDoc.DisplayMember = "doctype_name";
            cmbTypeDoc.ValueMember = "id_doctype";


            dgvStatus.DataSource = Config.hCntMain.getStatusDocVsProgConfig();

            dtBonus = Config.hCntMain.getDepartmentBonus(false);
        }

        private void cmbObject_DropDown(object sender, EventArgs e)
        {
            var senderComboBox = (ComboBox)sender;
            int width = senderComboBox.DropDownWidth;
            Graphics g = senderComboBox.CreateGraphics();
            Font font = senderComboBox.Font;

            int vertScrollBarWidth = (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                    ? SystemInformation.VerticalScrollBarWidth : 0;

            DataTable dtList = (DataTable)senderComboBox.DataSource;

            foreach (DataRow r in dtList.Rows)
            {
                string s = "";
                if (dtList.Columns.Contains("cname"))
                    s = (string)r["cname"];
                else
                    if (dtList.Columns.Contains("name"))
                    s = (string)r["name"];
                else
                    if (dtList.Columns.Contains("cName"))
                    s = (string)r["cName"];
                else
                    if (dtList.Columns.Contains("nameDep"))
                    s = (string)r["nameDep"];
                else
                    if (dtList.Columns.Contains("doctype_name"))
                    s = (string)r["doctype_name"];
                

                int newWidth = (int)g.MeasureString(s, font).Width + vertScrollBarWidth;

                if (width < newWidth)
                {
                    width = newWidth;
                }
            }

            senderComboBox.DropDownWidth = width;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void setWidthColumn(int indexRow, int indexCol, int width, Nwuram.Framework.ToExcelNew.ExcelUnLoad report)
        {
            report.SetColumnWidth(indexRow, indexCol, indexRow, indexCol, width);
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            DateTime dateStart, dateEnd;
            int id_Object,id_dep_bonus,id_distrType,id_dep,id_doctype;
            string listIdStatusDoc = "";

            dateStart = dtpStart.Value.Date;
            dateEnd = dtpEnd.Value.Date;
            id_Object = (int)cmbObject.SelectedValue;
            id_dep_bonus = (int)cmbDepsBonus.SelectedValue;
            id_distrType = (int)cmbType.SelectedValue;
            id_dep = (int)cmbDeps.SelectedValue;
            id_doctype = (int)cmbTypeDoc.SelectedValue;

            string listNameStatus = "";

            EnumerableRowCollection<DataRow> rowCollect = (dgvStatus.DataSource as DataTable).AsEnumerable().Where(r => r.Field<bool>("isSelect"));
            foreach (DataRow row in rowCollect)
            {
                listIdStatusDoc += $"{row["id"]},";
                listNameStatus += $"{row["cName"]},";
            }


            DataTable dtReport = Config.hCntMain.getReportMemorandumDep(dateStart, dateEnd, id_Object, id_dep_bonus, id_distrType, id_dep, id_doctype, listIdStatusDoc);

            if (dtReport == null || dtReport.Rows.Count == 0) { MessageBox.Show("Нет данных для формирования отчёта","Печать",MessageBoxButtons.OK,MessageBoxIcon.Information); return; }


            Nwuram.Framework.ToExcelNew.ExcelUnLoad report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad();

            int indexRow = 1;

            int maxColumns = 3;

            setWidthColumn(indexRow, 1, 30, report);
            setWidthColumn(indexRow, 2, 18, report);
            setWidthColumn(indexRow, 3, 18, report);

            #region "Head"
            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Отчёт по оплате {cmbObject.Text} за {dtpStart.Value.ToShortDateString()} -  {dtpEnd.Value.ToShortDateString()}", indexRow, 1);
            report.SetFontBold(indexRow, 1, indexRow, 1);
            report.SetFontSize(indexRow, 1, indexRow, 1, 16);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 1);
            indexRow++;
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"{lObject.Text}: {cmbObject.Text}", indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"{lType.Text}: {cmbType.Text}", indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"{lTypeDoc.Text}: {cmbTypeDoc.Text}", indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"{lStatus.Text}: {listNameStatus}", indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue("Выгрузил: " + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername, indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue("Дата выгрузки: " + DateTime.Now.ToString(), indexRow, 1);
            indexRow++;
            indexRow++;
            #endregion

            report.AddSingleValue("Отдел", indexRow, 1);
            report.AddSingleValue("Штраф", indexRow, 2);
            report.AddSingleValue("Премия", indexRow, 3);

            report.SetFontBold(indexRow, 1, indexRow, maxColumns);
            report.SetBorders(indexRow, 1, indexRow, maxColumns);
            report.SetWrapText(indexRow, 1, indexRow, maxColumns);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
            report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);
            indexRow++;

            foreach (DataRowView row in dtReport.DefaultView)
            {

                if (dtBonus != null || dtBonus.Rows.Count > 0)
                {
                    EnumerableRowCollection<DataRow> rowBonus = dtBonus.AsEnumerable().Where(r => r.Field<int>("id") == (int)row["id_deps"]);
                    if (rowBonus.Count() > 0)
                    {                        
                        //if((decimal)row["MinPayment"] <= (decimal)row["PercentPayment"])
                        if ((decimal)rowBonus.First()["MinPayment"] >= (decimal)row["summa"])
                            row["resultBonus"] = row["summa"];
                        else
                            row["resultBonus"] = (decimal)row["summa"] * (decimal)rowBonus.First()["PercentPayment"] / 100;

                        //row["resultBonus"] = 0.0M;
                    }
                }

                report.SetWrapText(indexRow, 1, indexRow, maxColumns);
                addDataToCell(row, indexRow, 1, "nameDep", report);
                addDataToCell(row, indexRow, 2, "summa", report);
                addDataToCell(row, indexRow, 3, "resultBonus", report);

                //if (row[col.DataPropertyName] is DateTime)
                //    report.AddSingleValue(((DateTime)row[col.DataPropertyName]).ToShortDateString(), indexRow, indexCol);
                //else
                //         if (row[col.DataPropertyName] is bool)
                //    report.AddSingleValue((bool)row[col.DataPropertyName] ? "Да" : "Нет", indexRow, indexCol);
                //else
                //           if (row[col.DataPropertyName] is decimal)
                //{
                //    report.AddSingleValueObject(row[col.DataPropertyName], indexRow, indexCol);
                //    report.SetFormat(indexRow, indexCol, indexRow, indexCol, "0.00");
                //}
                //else
                //    report.AddSingleValue(row[col.DataPropertyName].ToString(), indexRow, indexCol);


                report.SetBorders(indexRow, 1, indexRow, maxColumns);
                report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
                report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);

                indexRow++;
            }
            report.Show();


        }


        private void addDataToCell(DataRowView row, int indexRow, int indexCol, string nameCol, Nwuram.Framework.ToExcelNew.ExcelUnLoad report)
        {
            if (row[nameCol] is DateTime)
                report.AddSingleValue(((DateTime)row[nameCol]).ToShortDateString(), indexRow, indexCol);
            else
                            if (row[nameCol] is bool)
                report.AddSingleValue((bool)row[nameCol] ? "Да" : "Нет", indexRow, indexCol);
            else
                              if (row[nameCol] is decimal)
            {
                report.AddSingleValueObject(row[nameCol], indexRow, indexCol);
                report.SetFormat(indexRow, indexCol, indexRow, indexCol, "0.00");
            }
            else
                report.AddSingleValue(row[nameCol].ToString(), indexRow, indexCol);
        }
    }
}
