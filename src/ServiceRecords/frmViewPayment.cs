using Nwuram.Framework.Settings.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Nwuram.Framework.Logging;

namespace ServiceRecords
{
    public partial class frmViewPayment : Form
    {
        public frmViewPayment()
        {
            InitializeComponent();
            dgvNote.AutoGenerateColumns = false;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void frmViewPayment_Load(object sender, EventArgs e)
        {
            if (Config.CodeUser.Equals("РКВ"))
            {
                cFio.Visible = false;
                cPass.Visible = false;
                cDate.Visible = true;
                cOperation.Visible = true;
                btListNotePeriod.Visible = false;
                btReportNoteSingleDeps.Visible = false;
                pictureBox1.Visible = false;
                lbHighlightInPayment.Visible = false;
            }

            label1.Visible = tbItogo.Visible = btTakeMoney.Visible = cControl.Visible = Config.CodeUser.Equals("ОП");

            getData();
        }

        DataTable dtPayment;
        private void getData()
        {
            if (Config.CodeUser.Equals("РКВ"))
                dtPayment = Config.hCntMain.getPayment(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            else dtPayment = Config.hCntMain.getPayment(-1);

            if (!dtPayment.Columns.Equals("isControl"))
            {
                DataColumn col = new DataColumn("isControl", typeof(bool));
                col.DefaultValue = false;
                dtPayment.Columns.Add(col);
            }

            selectedMoneyRecipient = -1;
            sumTovar();
            dgvNote.DataSource = dtPayment;
        }


        private void sumTovar()
        {
            //dtPayment.AcceptChanges();
            object sum = dtPayment.Compute("SUM(Summa)", "isControl = 1");
            if (sum != DBNull.Value)
                tbItogo.Text = decimal.Parse(sum.ToString()).ToString("### ### ##0.00").Trim();
            else
                tbItogo.Text = "0,00";

            btTakeMoney.Enabled = selectedMoneyRecipient != -1;
        }

        int selectedMoneyRecipient = -1;

        private void dgvNote_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (e.RowIndex != -1 && senderGrid.Columns[e.ColumnIndex].Name == cControl.Name)
            {
                bool _isControl = false;
                if (dtPayment.DefaultView[e.RowIndex]["isControl"] != DBNull.Value)
                    _isControl = bool.Parse(dtPayment.DefaultView[e.RowIndex]["isControl"].ToString());

                int MoneyRecipient = (int)dtPayment.DefaultView[e.RowIndex]["id_MoneyRecipient"];

                if (dtPayment.Select("isControl = 1").Count() == 0)
                {
                    selectedMoneyRecipient = MoneyRecipient;
                    
                }
                else
                {
                    if (MoneyRecipient != selectedMoneyRecipient)
                    {
                        dtPayment.DefaultView[e.RowIndex]["isControl"] = false;
                        dtPayment.AcceptChanges();
                        senderGrid.Refresh();
                        return;
                    }
                }

                dtPayment.DefaultView[e.RowIndex]["isControl"] = !_isControl;


                dtPayment.AcceptChanges();
                senderGrid.Refresh();

                if (dtPayment.Select("isControl = 1").Count() == 0)
                {
                    selectedMoneyRecipient = -1;
                    dtPayment.DefaultView.RowFilter = "";
                }else
                    if (dtPayment.Select("isControl = 1").Count() == 1 && selectedMoneyRecipient != -1)
                    {
                        dtPayment.DefaultView.RowFilter = "id_MoneyRecipient = " + selectedMoneyRecipient;
                    }

                sumTovar();
            }
            else
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                    e.RowIndex >= 0)
                {
                    //TODO - Button Clicked - Execute Code Here
                    int id = (int)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["id"];
                    int id_ServiceRecords = (int)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["id_ServiceRecords"];
                    string nameType = (string)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["nameType"];
                    decimal Summa = (decimal)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["Summa"];
                    int Number = (int)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["Number"];
                    string FIO = (string)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["FIO"];
                    try
                    {
                        int id_MoneyRecipient = (int)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["id_MoneyRecipient"];

                        int type = (int)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["type"];
                        bool bCashNonCash = (bool)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["bCashNonCash"];

                        //if (type == 2 || bCashNonCash) id_MoneyRecipient = UserSettings.User.Id;

                        globalForm.frmPassword frmPass = new globalForm.frmPassword();
                        frmPass.idUser = id_MoneyRecipient;
                        if (DialogResult.OK == frmPass.ShowDialog())
                        {
                            setLog(id_ServiceRecords, nameType, Summa, Number, FIO);
                            Config.hCntMain.updatePayment(id, id_MoneyRecipient);
                            Config.hCntMain.updateStatus(id_ServiceRecords, 14);
                            getData();
                        }
                    }
                    catch { MessageBox.Show("null"); }
                }
        }

        private void btListNotePeriod_Click(object sender, EventArgs e)
        {
            createSZMoney(false);
            createSZMoney(true);
        }

        private void createSZMoney(bool isCart)
        {
            Logging.StartFirstLevel(79);

            Logging.Comment("Запрос на выдачу " + (isCart ? "Безнал." : "Нал.") + " ДС за " + DateTime.Now.ToShortDateString());

            Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
            Logging.StopFirstLevel();

            DataTable dtTmp = Config.hCntMain.getReportPayment(false, isCart);

            if (dtTmp == null || dtTmp.Rows.Count == 0) return;

            var groupedData
                    = from b in dtTmp.AsEnumerable()
                      group b by new { id_Department = b.Field<Int32>("id_Department") } into g
                      select new
                      {
                          id_Department = g.Key.id_Department
                          //  netto = g.Sum(x => x.Field<decimal>("netto"))
                      };
            for (int i = dtTmp.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dtTmp.Rows[i];
                if (dr["type"].ToString().Equals("2"))
                    dr.Delete();
            }
            Nwuram.Framework.ToExcelNew.ExcelUnLoad report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad("Лист - 1");

            int indexRow = 1;

            report.Merge(indexRow, 1, indexRow, 4);
            report.AddSingleValue("Запрос на выдачу " + (isCart ? "Безнал." : "Нал.") + " ДС за " + DateTime.Now.ToShortDateString(), indexRow, 1);
            report.SetCellAlignmentToJustify(indexRow, 1, indexRow, 1);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 1);
            report.SetFontSize(indexRow, 1, indexRow, 1, 16);
            report.SetFontBold(indexRow, 1, indexRow, 1);
            report.SetRowHeight(indexRow, 1, indexRow, 4, 40);
            indexRow++;
            indexRow++;


            report.AddSingleValue("№ СЗ", indexRow, 1);
            report.AddSingleValue("Описание", indexRow, 2);
            report.AddSingleValue("Должность", indexRow, 3);
            report.AddSingleValue("Сумма", indexRow, 4);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 4);
            report.SetBorders(indexRow, 1, indexRow, 4);
            indexRow++;


            foreach (var grp in groupedData)
            {
                DataRow[] row = dtTmp.Select(string.Format("id_Department = {0}", grp.id_Department));
                report.Merge(indexRow, 1, indexRow, 4);
                report.AddSingleValue(row[0]["nameDeps"].ToString(), indexRow, 1);
                report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 4);
                report.SetBorders(indexRow, 1, indexRow, 4);
                indexRow++;

                foreach (DataRow r in row)
                {

                    report.AddSingleValue(r["Number"].ToString(), indexRow, 1);
                    report.AddSingleValue(r["Description"].ToString(), indexRow, 2);
                    report.AddSingleValue(r["FIO"].ToString(), indexRow, 3);
                    report.AddSingleValue(r["Summa"].ToString(), indexRow, 4);

                    report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 4);
                    report.SetBorders(indexRow, 1, indexRow, 4);
                    indexRow++;
                }
            }

            report.SetColumnAutoSize(3, 1, indexRow - 1, 4);
            report.Show();

        }

        private void btReportNoteSingleDeps_Click(object sender, EventArgs e)
        {

            if (!Directory.Exists("Report"))
            {
                Directory.CreateDirectory("Report");
            }

            string[] dirs = Directory.GetFiles(@"Report\", "*.xlsx");

            foreach (string n in dirs)
            {
                try
                {
                    System.GC.Collect(); // попытка остановить процесс и удалить предыдущий файл excel
                    System.GC.WaitForPendingFinalizers();
                    File.Delete(n);
                }
                catch { MessageBox.Show("Удалите Excel файлы"); return; }
            }

            createReportWithPicter(true);
            createReportWithPicter(false);

            dirs = Directory.GetFiles(@"Report\", "*.xlsx");
            foreach (string n in dirs)
            {
                //cnvXLSToPDF.ConvertData(n);
                try { Process.Start(n); }
                catch { MessageBox.Show("Не удалось открыть файл"); return; }
                //File.Delete(n);
            }
        }


        #region Отчет без подписей
        private void createReport(bool isCart)
        {
            string nameFile = DateTime.Now.ToShortDateString() + " - " + (isCart ? "Нал." : "БезНал.");

            DataTable dtTmp = Config.hCntMain.getReportPayment(true, isCart);

            if (dtTmp == null || dtTmp.Rows.Count == 0) { MessageBox.Show("Данных для печати нет"); return; };

            var groupedData
                    = from b in dtTmp.AsEnumerable()
                      group b by new { id_Department = b.Field<Int32>("id_Department") } into g
                      select new
                      {
                          id_Department = g.Key.id_Department
                          //  netto = g.Sum(x => x.Field<decimal>("netto"))
                      };

            Nwuram.Framework.ToExcelNew.ExcelUnLoad report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad("Лист - 1");

            int indexRow = 1;

            report.Merge(indexRow, 1, indexRow, 6);
            report.AddSingleValue("Отчёт по выданным " + (isCart ? "Безнал." : "Нал.") + " ДС ", indexRow, 1);
            report.SetCellAlignmentToJustify(indexRow, 1, indexRow, 1);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 1);
            report.SetFontSize(indexRow, 1, indexRow, 1, 16);
            report.SetFontBold(indexRow, 1, indexRow, 1);
            indexRow++;
            indexRow++;

            report.Merge(indexRow, 1, indexRow, 6);
            report.AddSingleValue("Выгрузил: " + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername, indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, 6);
            report.AddSingleValue("Дата выгрузки: " + DateTime.Now.ToString(), indexRow, 1);
            indexRow++;
            indexRow++;

            report.AddSingleValue("№ СЗ", indexRow, 1);
            report.AddSingleValue("Описание", indexRow, 2);
            report.AddSingleValue("Должность", indexRow, 3);
            report.AddSingleValue("Сумма", indexRow, 4);
            report.AddSingleValue("Дата", indexRow, 5);
            report.AddSingleValue("Подпись руководителя", indexRow, 6);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 6);
            report.SetBorders(indexRow, 1, indexRow, 6);
            indexRow++;


            foreach (var grp in groupedData)
            {
                DataRow[] row = dtTmp.Select(string.Format("id_Department = {0}", grp.id_Department));
                report.Merge(indexRow, 1, indexRow, 6);
                report.AddSingleValue(row[0]["nameDeps"].ToString(), indexRow, 1);
                report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 6);
                report.SetBorders(indexRow, 1, indexRow, 6);
                indexRow++;

                foreach (DataRow r in row)
                {

                    report.AddSingleValue(r["Number"].ToString(), indexRow, 1);
                    report.AddSingleValue(r["Description"].ToString(), indexRow, 2);
                    report.AddSingleValue(r["FIO"].ToString(), indexRow, 3);
                    report.AddSingleValue(r["Summa"].ToString(), indexRow, 4);
                    report.AddSingleValue(r["DataSumma"].ToString(), indexRow, 5);
                    //report.AddSingleValue(r["Summa"].ToString(), indexRow, 6);

                    report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 4);
                    report.SetBorders(indexRow, 1, indexRow, 6);
                    indexRow++;
                }
            }

            report.SetColumnAutoSize(6, 1, indexRow - 1, 6);
            //report.Show();
            report.SaveToFile(@"Report\" + nameFile + ".xlsx");
        }
        #endregion

        private void createReportWithPicter(bool isCart)
        {
            Logging.StartFirstLevel(79);

            Logging.Comment("Отчёт по выданным " + (isCart ? "БезНал." : "Нал.") + " ДС ");

            Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
            Logging.StopFirstLevel();

            #region Получение значений
            string nameFile = DateTime.Now.ToShortDateString() + " - " + (isCart ? "БезНал." : "Нал.");
            DataTable dtTmp = Config.hCntMain.getReportPayment(true, isCart);

            if (dtTmp == null || dtTmp.Rows.Count == 0) { MessageBox.Show("Данных для печати " + (isCart ? " БезНал." : " Нал.") + " нет"); return; };

            var groupedData
                    = from b in dtTmp.AsEnumerable()
                      group b by new { id_Department = b.Field<Int32>("id_Department") } into g
                      select new
                      {
                          id_Department = g.Key.id_Department
                      };

            #endregion
            #region Заполнение таблицы
            var eP = new ExcelPackage();
            var sheet = eP.Workbook.Worksheets.Add("Лист-1");
            int indexRow = 1;
            sheet.Cells[indexRow, 1, indexRow, 6].Merge = true;
            sheet.Cells[indexRow, 1, indexRow, 6].Value = "Отчёт по выданным " + (isCart ? "БезНал." : "Нал.") + " ДС ";
            var Rng = sheet.Cells[indexRow, 1, indexRow, 6];
            Rng.Style.Font.Size = 16;
            Rng.Style.Font.Bold = true;
            Rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            indexRow++;
            indexRow++;
            sheet.Cells[indexRow, 1, indexRow, 6].Merge = true;
            sheet.Cells[indexRow, 1, indexRow, 6].Value = "Выгрузил: " + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername;
            indexRow++;
            sheet.Cells[indexRow, 1, indexRow, 6].Merge = true;
            sheet.Cells[indexRow, 1, indexRow, 6].Value = "Дата выгрузки: " + DateTime.Now.ToString();
            indexRow++;
            indexRow++;
            sheet.Cells[indexRow, 1].Value = "№ СЗ";
            sheet.Cells[indexRow, 2].Value = "Описание";
            sheet.Cells[indexRow, 3].Value = "Должность";
            sheet.Cells[indexRow, 4].Value = "Сумма";
            sheet.Cells[indexRow, 5].Value = "Дата";
            sheet.Cells[indexRow, 5, indexRow, 6].Merge = true;
            sheet.Cells[indexRow, 7].Value = "Подпись";

            indexRow++;
            int setPos = -20;

            foreach (var grp in groupedData)
            {
                DataRow[] row = dtTmp.Select(string.Format("id_Department = {0}", grp.id_Department));
                sheet.Cells[indexRow, 1, indexRow, 7].Merge = true;
                sheet.Cells[indexRow, 1].Value = row[0]["nameDeps"].ToString();
                var cells = sheet.Cells[indexRow, 1, indexRow, 6];
                cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                cells.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                cells.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                cells.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                cells.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                cells.AutoFitColumns();
                indexRow++;
                setPos = setPos + 20;

                foreach (DataRow r in row)
                {
                    sheet.Cells[indexRow, 1].Value = r["Number"].ToString();
                    sheet.Cells[indexRow, 2].Value = r["Description"].ToString();
                    sheet.Cells[indexRow, 3].Value = r["FIO"].ToString();
                    sheet.Cells[indexRow, 4].Value = r["Summa"].ToString();
                    sheet.Cells[indexRow, 5].Value = r["DataSumma"].ToString();
                    #region Вставка картинки
                    float iw, ih;
                    float zExcelPixel = 0.036835443f;
                    float zExcelPixelW = 0.29635443f;
                    int st, sp;
                    st = 143;
                    sp = 385;

                    iw = zExcelPixelW * 460;
                    ih = zExcelPixel * 400;

                    try
                    {
                        DataTable dtFile = Config.hCntSecond.getScanSignature((int)r["id_Access"]);
                        // if (dtFile == null || dtFile.Rows.Count == 0) MessageBox.Show("Изображение не найдено");
                        byte[] img = (byte[])dtFile.Rows[0]["imgSign"];
                        MemoryStream stream = new MemoryStream(img);
                        Image logo = Image.FromStream(stream);
                        var pic = sheet.Drawings.AddPicture("Photo" + indexRow, logo);
                        pic.From.Column = 6;
                        pic.From.Row = indexRow - 1;
                        pic.SetSize((int)iw, (int)ih);
                        pic.SetPosition(st + setPos, sp);
                        setPos = setPos + 20;
                    }
                    catch { MessageBox.Show("Подпись для пользователя " + r["FIO"].ToString() + " не найдена"); setPos = setPos + 20; }
                    #endregion
                    indexRow++;
                }
                var cells2 = sheet.Cells[6, 1, indexRow - 1, 7];
                cells2.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                cells2.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                cells2.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                cells2.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                sheet.Column(2).Width = 30;
                sheet.Column(3).Width = 20;
                sheet.Column(4).Width = 15;
                sheet.Column(7).Width = 20;
                sheet.Protection.IsProtected = true;
            }
            try
            {
                var bin = eP.GetAsByteArray();
                File.WriteAllBytes(Directory.GetCurrentDirectory() + @"\Report\" + nameFile + ".xlsx", bin);
            }
            catch { MessageBox.Show("Запись в файл не удалась"); }
            #endregion
        }

        private void dgvNote_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (Config.CodeUser.Equals("ОП"))
            {
                try
                {
                    int Creator = (int)dtPayment.DefaultView[e.RowIndex]["id_Creator"];
                    int MoneyRecipient = (int)dtPayment.DefaultView[e.RowIndex]["id_MoneyRecipient"];
                    if (Creator != MoneyRecipient)
                        dgvNote.Rows[e.RowIndex].DefaultCellStyle.BackColor = pictureBox1.BackColor; //ColorTranslator.FromHtml("#ffff99");
                }
                catch { }
            }
        }
        ~frmViewPayment() { }

        private void setLog(int id, string nameType, decimal Summa, int Number, string FIO)
        {
            DataTable dtTmpData = Config.hCntMain.getServiceRecordsBody(id);

            Logging.StartFirstLevel(1261);


            //Logging.Comment("Номер: " + Number);
            //Logging.Comment("Сумма: " + Summa.ToString());
            //Logging.Comment("ФИО: " + FIO);
            //Logging.Comment("Тип: " + nameType);
            //Logging.Comment("Предполагаемая дата" + dtpDate.Value.ToShortDateString());

            //Logging.Comment(chbChangeDirector.Text + ": " + (chbChangeDirector.Checked ? "Включен" : "Отключен"));
            //if (chbChangeDirector.Checked)
            //    Logging.Comment("Получатель отличается от заказчика");

            //Logging.Comment("Руководиель ID: " + cmbDirector.SelectedValue + "; Наименование:" + cmbDirector.Text.ToString());

            Logging.Comment("Id СЗ: " + id);
            Logging.Comment("Номер СЗ: " + dtTmpData.Rows[0]["Number"].ToString());
            Logging.Comment("Тип СЗ: " + ((int)dtTmpData.Rows[0]["TypeServiceRecord"] == 0 ? "стандарт." : "предварит."));
            //Logging.Comment("Тип СЗ по времени: " + ((int)dtTmpData.Rows[0]["TypeServiceRecordOnTime"] == 1 ? "разовая" : "ежемесячная"));
            Logging.Comment("Тип СЗ по времени: " + ((int)dtTmpData.Rows[0]["TypeServiceRecordOnTime"] == 1 ? "разовая" : ((int)dtTmpData.Rows[0]["TypeServiceRecordOnTime"] == 2 ? "ежемесячная" : "Фонд")));

            int? idFond = dtTmpData.Rows[0]["id_ServiceRecordsFond"] == DBNull.Value ? null : (int?)dtTmpData.Rows[0]["id_ServiceRecordsFond"];

            if (idFond != null)
            {
                DataTable dtTmpFond = Config.hCntMain.getFondInfo(idFond, id);
                if (dtTmpFond != null && dtTmpFond.Rows.Count > 0)
                {
                    Logging.Comment($"№{dtTmpFond.Rows[0]["Number"].ToString()} на {dtTmpFond.Rows[0]["sumString"].ToString()} от {((DateTime)dtTmpFond.Rows[0]["DateConfirmationD"]).ToShortDateString()}");
                }
            }
            else
            {
                Logging.Comment((int)dtTmpData.Rows[0]["TypeServiceRecordOnTime"] == 3 ? "Доп.фонд не выбран" : "Фонд не выбран");
            }


            if (dtTmpData.Rows[0]["inType"] != DBNull.Value)
            {
                DataTable dtTypicalWorks = Config.hCntMain.getTypicalWorks(false);
                if (dtTypicalWorks != null && dtTypicalWorks.Rows.Count > 0)
                {
                    EnumerableRowCollection<DataRow> rowType = dtTypicalWorks.AsEnumerable().Where(r => r.Field<int>("id") == (int)dtTmpData.Rows[0]["inType"]);
                    if (rowType.Count() > 0)
                    {
                        Logging.Comment($"Тип работ ID:{rowType.First()["id"]}; Наименование:{rowType.First()["cName"]}");
                    }
                }
            }

            Logging.Comment("Сумма: " + Summa.ToString());
            Logging.Comment("ФИО: " + FIO);
            Logging.Comment("Тип: " + nameType);

            Logging.Comment("Сумма:" + decimal.Parse(dtTmpData.Rows[0]["Summa"].ToString()).ToString("0.00"));
            if ((bool)dtTmpData.Rows[0]["Mix"])
            {
                Logging.Comment("Сумма нал:" + dtTmpData.Rows[0]["SummaCash"].ToString());
                Logging.Comment("Сумма безнал:" + dtTmpData.Rows[0]["SummaNonCash"].ToString());
            }

            Logging.Comment("Объект ID: " + dtTmpData.Rows[0]["id_Object"].ToString() + "; Наименование:" + dtTmpData.Rows[0]["name_Object"].ToString());
            Logging.Comment("Описание, комментарий: " + dtTmpData.Rows[0]["Description"].ToString());
            Logging.Comment("Оплата: " + (!(bool)dtTmpData.Rows[0]["bCashNonCash"] ? "Наличные" : "Карта"));

            Logging.Comment("Дата создания СЗ: " + ((DateTime)dtTmpData.Rows[0]["CreateServiceRecord"]).ToShortDateString());

            Logging.Comment("Блок ID: " + dtTmpData.Rows[0]["id_Block"].ToString() + "; Наименование:" + dtTmpData.Rows[0]["nameBlock"].ToString());
            Logging.Comment("Отдел ID: " + dtTmpData.Rows[0]["id_Department"].ToString() + "; Наименование:" + dtTmpData.Rows[0]["nameDeps"].ToString());

            Logging.Comment("На рассмотрении РП. Прошу включить в Б: " + ((DateTime)dtTmpData.Rows[0]["MonthB"]).ToShortDateString());

            if (dtTmpData.Rows[0]["bDataSumma"] != DBNull.Value && !(bool)dtTmpData.Rows[0]["bDataSumma"] && dtTmpData.Rows[0]["DataSumma"] != DBNull.Value)
            {
                Logging.Comment("Дата получения ДС (при единовременном получении ДС): " + ((DateTime)dtTmpData.Rows[0]["DataSumma"]).ToShortDateString());

            }
            if (dtTmpData.Rows[0]["DataSumma"] == DBNull.Value && dtTmpData.Rows[0]["bDataSumma"] != DBNull.Value && (bool)dtTmpData.Rows[0]["bDataSumma"])
            {
                DataTable dtMultipleReceivingMone = Config.hCntMain.getMultipleReceivingMone(id);
                Logging.Comment("Дата получения ДС (при распределенном ДС)");

                foreach (DataRow r in dtMultipleReceivingMone.Rows)
                {
                    Logging.Comment("Подномер:" + r["SubNumber"].ToString() + ";Сумма:" + r["Summa"].ToString() + ";Предполагаемая дата:" + r["DataSumma"].ToString());
                }
            }
            Logging.Comment("Комментарий:" + dtTmpData.Rows[0]["Comments"].ToString().Replace(@"\r", "\r\n"));

            //if (comment.Length != 0)
            //    Logging.Comment("Комментрий с формы:" + comment);

            Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
               + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
            Logging.StopFirstLevel();
        }

        private void btTakeMoney_Click(object sender, EventArgs e)
        {
            int id_MoneyRecipient = selectedMoneyRecipient;
            globalForm.frmPassword frmPass = new globalForm.frmPassword();
            frmPass.idUser = id_MoneyRecipient;
            if (DialogResult.OK == frmPass.ShowDialog())
            {
                foreach (DataRow r in dtPayment.Select("isControl = 1"))
                {
                    int id = (int)r["id"];
                    int id_ServiceRecords = (int)r["id_ServiceRecords"];
                    string nameType = (string)r["nameType"];
                    decimal Summa = (decimal)r["Summa"];
                    int Number = (int)(r["Number"] == DBNull.Value ? 0 : r["Number"]);
                    string FIO = (string)r["FIO"];
                    try
                    {

                        int type = (int)r["type"];
                        bool bCashNonCash = (bool)r["bCashNonCash"];
                        //if (type == 2 || bCashNonCash) id_MoneyRecipient = UserSettings.User.Id;
                        setLog(id_ServiceRecords, nameType, Summa, Number, FIO);
                        Config.hCntMain.updatePayment(id, id_MoneyRecipient);
                        Config.hCntMain.updateStatus(id_ServiceRecords, 14);
                        //getData();
                    }
                    catch { MessageBox.Show("null"); }
                }

                getData();
            }
        }
    }
    
}
