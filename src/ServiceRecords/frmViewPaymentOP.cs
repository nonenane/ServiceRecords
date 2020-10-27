using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Nwuram.Framework.Logging;
using System.IO;
using System.Diagnostics;
using Nwuram.Framework.ToExcel;
using Nwuram.Framework.Settings.User;


namespace ServiceRecords
{
    public partial class frmViewPaymentOP : Form
    {
        private int id_User = 0;
        DataTable dtPayment;
        public frmViewPaymentOP(int id_User = -1)
        {
            InitializeComponent();
            this.id_User = id_User;
            dgvNote.AutoGenerateColumns = false;
            getData();
            setVisibleElements();
            ToolTip tt = new ToolTip();
            tt.SetToolTip(btReportNal, "Запрос на выдачу Нал.");
            tt.SetToolTip(btReportNoNal, "Запрос на выдачу Безнал.");
            tt.SetToolTip(btnPrintJournal, "Отчет по выданным средствам");
            tt.SetToolTip(btClose, "Выход");
            tt.SetToolTip(btnUpdate, "Обновить");
            tt.SetToolTip(btnClose2, "Выход");
            tt.SetToolTip(btnEdit, "Редактирование");
            tt.SetToolTip(btnDelete, "Удаление");

        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void frmViewPaymentOP_Load(object sender, EventArgs e)
        {
            if (Config.CodeUser.Equals("РКВ"))
            {
                cFio.Visible = true;
                cPass.Visible = false;
                cDate.Visible = true;
                cOperation.Visible = true;
                //btListNotePeriod.Visible = false;
                //btReportNoteSingleDeps.Visible = false;
                pictureBox1.Visible = true;
                lbHighlightInPayment.Visible = true;
                btnEdit.Visible = btnDelete.Visible = true;
            }
            if (Config.CodeUser.Equals("ОП"))
                btnEdit.Visible = btnDelete.Visible = true;
            label1.Visible = tbItogo.Visible = btTakeMoney.Visible = cControl.Visible = Config.CodeUser.Equals("ОП");

            getData();

            if (dtPayment.Rows.Count > 0 )
                if ( (int)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["id_Creator"] == Nwuram.Framework.Settings.User.UserSettings.User.Id)
                btnEdit.Enabled = btnDelete.Enabled = true;
        }

        private void getData()
        {
            if (Config.CodeUser.Equals("РКВ"))
            dtPayment = Config.hCntMain.getPaymentOP(id_User);
            else if (Config.CodeUser.Equals("ОП")) dtPayment = Config.hCntMain.getPaymentOP(-1);

            if (!dtPayment.Columns.Equals("isControl"))
            {
                DataColumn col = new DataColumn("isControl", typeof(bool));
                col.DefaultValue = false;
                dtPayment.Columns.Add(col);
            }

            selectedMoneyRecipient = -1;
            sumTovar();
            dgvNote.DataSource = dtPayment;

            for (int i = 0; i < dgvNote.Rows.Count; i++)
            dgvNote.Rows[i].Cells["ValutaVisible"].Value = "RUB";

            FilterDtPayment();
            if (dtPayment != null && dgvNote.CurrentRow != null)
                btnEdit.Enabled = btnDelete.Enabled = (int)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["id_Creator"] == Nwuram.Framework.Settings.User.UserSettings.User.Id;
            else btnEdit.Enabled = btnDelete.Enabled = false;
        }

        private void setVisibleElements()
        {
            if (Config.CodeUser.Equals("ОП")) return;

            btReportNal.Visible = btReportNoNal.Visible = btTakeMoney.Visible = false;

        }

        private void FilterDtPayment()
        {
            string constFilter = "";
            if (Config.CodeUser.Equals("РКВ"))
                constFilter += "( id_MoneyRecipient = '" + UserSettings.User.Id + "' OR id_Creator = '" + UserSettings.User.Id + "' )";
            dtPayment.DefaultView.RowFilter = constFilter;

        }

        private void sumTovar()
        {
            //dtPayment.AcceptChanges();
            object sum = dtPayment.Compute("SUM(Summa)", "isControl = 1 and type = 1");
            object sum2 = dtPayment.Compute("SUM(Summa)", "isControl = 1 and type = 2");
            if (sum != DBNull.Value && sum2 != DBNull.Value)
                sum = decimal.Parse(sum.ToString()) - decimal.Parse(sum2.ToString());
            else if (sum != DBNull.Value)
                sum = decimal.Parse(sum.ToString());
            else if (sum2 != DBNull.Value)
                sum = - decimal.Parse(sum2.ToString());

            if (sum != DBNull.Value)
                tbItogo.Text = decimal.Parse(sum.ToString()).ToString("### ### ##0.00").Trim();
            else
                tbItogo.Text = "0,00";

            btTakeMoney.Enabled = selectedMoneyRecipient != -1;
        }

        int selectedMoneyRecipient = -1;

        private void dgvNote_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Config.CodeUser.Equals("РКВ")) return;

            var senderGrid = (DataGridView)sender; 

            if (e.RowIndex != -1 && senderGrid.Columns[e.ColumnIndex].Name == cControl.Name)
            {
                bool _isControl = false;
                if (dtPayment.DefaultView[e.RowIndex]["isControl"] != DBNull.Value)
                    _isControl = bool.Parse(dtPayment.DefaultView[e.RowIndex]["isControl"].ToString());

                int MoneyRecipient = (int)dtPayment.DefaultView[e.RowIndex]["id_MoneyRecipient"];
                //int id_Creator = (int)dtPayment.DefaultView[e.RowIndex]["id_Creator"];

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
                }
                else
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
                int Number = dtPayment.DefaultView[dgvNote.CurrentRow.Index]["Number"] == DBNull.Value ? 0 : (int)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["Number"];
                string FIO = (string)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["FIO"];
                try
                {
                    int id_MoneyRecipient = (int)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["id_MoneyRecipient"];
                    int id_Creator = (int)dtPayment.DefaultView[e.RowIndex]["id_Creator"];

                    int type = (int)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["type"];
                    int bCashNonCash = (bool)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["bCashNonCash"] == false ? 0 : 1;
                    int typeCashNonCash = dtPayment.DefaultView[dgvNote.CurrentRow.Index]["nameType"].ToString().Contains("Нал.") ? 0 : 1;
                    //if (type == 2 || bCashNonCash) id_MoneyRecipient = UserSettings.User.Id;

                    globalForm.frmPassword frmPass = new globalForm.frmPassword();
                    frmPass.idUser = id_MoneyRecipient;
                    frmPass.idCreator = id_Creator;
                    if (DialogResult.OK == frmPass.ShowDialog())
                    {
                        setLog(id_ServiceRecords, nameType, Summa, Number, FIO);
                        Config.hCntMain.updatePayment(id, id_MoneyRecipient);
                        if (dtPayment.Select($"Number = {Number}").ToList().Count() == 1)
                            Config.hCntMain.updateStatus(id_ServiceRecords, 14);
                        if (type == 1)
                            Config.hCntMain.addReport(id_ServiceRecords, Summa, typeCashNonCash, type, 0);
                        else
                            Config.hCntMain.addReport(id_ServiceRecords, 0 , typeCashNonCash, type, Summa);
                        // Config.hCntMain.updateStatus(id_ServiceRecords, 22);
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
            //Logging.StartFirstLevel(79);

            //Logging.Comment("Запрос на выдачу " + (isCart ? "Безнал." : "Нал.") + " ДС за " + DateTime.Now.ToShortDateString());

            //Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
            //    + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
            //Logging.StopFirstLevel();

            DataTable dtTmp = Config.hCntMain.getReportPayment(false, isCart);

            if (dtTmp == null || dtTmp.Rows.Count == 0) if (dtTmp == null || dtTmp.Rows.Count == 0) { MessageBox.Show("Данных для печати " + (isCart ? " БезНал." : " Нал.") + " нет.", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information); return; };

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
            report.SetCellAlignmentToJustify(indexRow, 1, indexRow, 4);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 4);
            report.SetColumnAutoSize(indexRow, 1, indexRow, 4);
            report.SetFontSize(indexRow, 1, indexRow, 1, 16);
            report.SetFontBold(indexRow, 1, indexRow, 1);
            report.SetWrapText(indexRow, 1, indexRow, 4);
            report.SetRowHeight(indexRow, 1, indexRow, 4, 40);
            indexRow++;
            indexRow++;


            report.AddSingleValue("№ СЗ", indexRow, 1);
            report.AddSingleValue("Описание", indexRow, 2);
            report.AddSingleValue("Получатель", indexRow, 3);
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

            if (dtTmp == null || dtTmp.Rows.Count == 0) { MessageBox.Show("Данных для печати " + (isCart ? " БезНал." : " Нал.") + " нет.", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information); return; };

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
            sheet.Cells[indexRow, 3].Value = "Получатель";
            sheet.Cells[indexRow, 4].Value = "Сумма";
            sheet.Cells[indexRow, 5].Value = "Валюта";
            sheet.Cells[indexRow, 6].Value = "Дата";
            sheet.Cells[indexRow, 7, indexRow, 8].Merge = true;
            sheet.Cells[indexRow, 8].Value = "Подпись";

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
                    sheet.Cells[indexRow, 5].Value = r["Valuta"].ToString();
                    sheet.Cells[indexRow, 6].Value = r["DataSumma"].ToString();
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
                        pic.From.Column = 7;
                        pic.From.Row = indexRow - 1;
                        pic.SetSize((int)iw, (int)ih);
                        pic.SetPosition(st + setPos, sp);
                        setPos = setPos + 20;
                    }
                    catch { MessageBox.Show("Подпись для пользователя " + r["FIO"].ToString() + " не найдена"); setPos = setPos + 20; }
                    #endregion
                    indexRow++;
                }
                var cells2 = sheet.Cells[6, 1, indexRow - 1, 8];
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
                try
                {
                    int Creator = (int)dtPayment.DefaultView[e.RowIndex]["id_Creator"];
                    int MoneyRecipient = (int)dtPayment.DefaultView[e.RowIndex]["id_MoneyRecipient"];
                    if (Creator != MoneyRecipient)
                        dgvNote.Rows[e.RowIndex].DefaultCellStyle.BackColor = pictureBox1.BackColor; //ColorTranslator.FromHtml("#ffff99");
                }
                catch { }

        }
        ~frmViewPaymentOP() { }

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
            Logging.Comment("Тип СЗ по времени: " + ((int)dtTmpData.Rows[0]["TypeServiceRecordOnTime"] == 1 ? "разовая" : "ежемесячная"));

            Logging.Comment("Сумма: " + Summa.ToString());
            Logging.Comment("ФИО: " + FIO);
            Logging.Comment("Тип: " + nameType);            

            Logging.Comment("Сумма:" + decimal.Parse(dtTmpData.Rows[0]["Summa"].ToString()).ToString("0.00"));
            Logging.Comment("Валюта:" + dtTmpData.Rows[0]["Valuta"].ToString());
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
            DataTable dt= dtPayment;
            int id_MoneyRecipient = selectedMoneyRecipient;
            globalForm.frmPassword frmPass = new globalForm.frmPassword();
            frmPass.idUser = (int)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["id_MoneyRecipient"];
            frmPass.idCreator = (int)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["id_Creator"];
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
                        int bCashNonCash = (bool)r["bCashNonCash"] == false ? 0 : 1;
                        int typeCashNonCash = r["nameType"].ToString().Contains("Нал.") ? 0 : 1;

                        //if (type == 2 || bCashNonCash) id_MoneyRecipient = UserSettings.User.Id;
                        
                        Config.hCntMain.updatePayment(id, id_MoneyRecipient);
                        if (dtPayment.Select($"Number = {Number} and isControl = 1").ToList().Count()== 2 
                            || dtPayment.Select($"Number = {Number}").ToList().Count() == 1)
                                Config.hCntMain.updateStatus(id_ServiceRecords, 14);
                        if (type == 1)
                        Config.hCntMain.addReport(id_ServiceRecords, Summa, typeCashNonCash, type, 0);
                        else
                            Config.hCntMain.addReport(id_ServiceRecords, 0 , typeCashNonCash, type, Summa);
                        //    Config.hCntMain.updateStatus(id_ServiceRecords, 22);
                        //getData();
                        setLog(id_ServiceRecords, nameType, Summa, Number, FIO);
                    }
                    catch { MessageBox.Show("null"); }
                }

                getData();
            }
        }

        private void btReportNal_Click(object sender, EventArgs e)
        {
            Logging.StartFirstLevel(79);
            Logging.Comment("Отчет \"Запрос на выдачу Нал.\"");
            Logging.Comment("Выгружен в Excel отчет по СЗ со следующими параметрами");
            Logging.Comment("Дата выгрузки: " + DateTime.Now.ToString());            
            Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
            Logging.StopFirstLevel();

            createSZMoney(false); // запрос на выдачу нал
            //PrintReport(false); // отчет по выданным нал
        }

        private void btReportNoNal_Click(object sender, EventArgs e)
        {
            Logging.StartFirstLevel(79);
            Logging.Comment("Отчет \"Запрос на выдачу Безнал.\"");
            Logging.Comment("Выгружен в Excel отчет по СЗ со следующими параметрами");
            Logging.Comment("Дата выгрузки: " + DateTime.Now.ToString());
            Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
            Logging.StopFirstLevel();

            createSZMoney(true); // запрос на выдачу безнал
            // PrintReport(true); //отчет по выданным нал
        }

        private void PrintReport(bool nal)
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

            createReportWithPicter(nal);

            dirs = Directory.GetFiles(@"Report\", "*.xlsx");
            foreach (string n in dirs)
            {

                try { Process.Start(n); }
                catch { MessageBox.Show("Не удалось открыть файл"); return; }

            }
        }

        DataTable dtNoteJournal = new DataTable();
        private void tabControl1_Click(object sender, EventArgs e)
        {
            dgvNoteJournal.AutoGenerateColumns = false;
            dateTimeStart.Value = dateTimeEnd.Value.AddDays(-7);
            createComboBox();
            UpdateJournal(dateTimeStart.Value, dateTimeEnd.Value);
            

        }
        private void createComboBox()
        {
            DataTable dtTypeOperation = createTable();

            dtTypeOperation.Rows.Add(0, "Все");
            dtTypeOperation.Rows.Add(1, "Выдача");
            dtTypeOperation.Rows.Add(2, "Возврат");

            DataTable dtTypeGet = createTable();

            dtTypeGet.Rows.Add(0, "Все");
            dtTypeGet.Rows.Add(1, "Нал");
            dtTypeGet.Rows.Add(2, "Безнал");

            DataTable dtTypeSR = createTable();

            dtTypeSR.Rows.Add(0, "Все");
            dtTypeSR.Rows.Add(1, "Разовая");
            dtTypeSR.Rows.Add(2, "Ежемесячная");

            cbTypeOperation.DataSource = dtTypeOperation;
            cbTypeGet.DataSource = dtTypeGet;
            cbTypeSR.DataSource = dtTypeSR;
            cbTypeOperation.DisplayMember = cbTypeGet.DisplayMember = cbTypeSR.DisplayMember = "name";
            cbTypeOperation.ValueMember = cbTypeGet.ValueMember = cbTypeSR.ValueMember = "id";
            cbTypeGet.SelectedValue = 0;
            cbTypeOperation.SelectedValue = cbTypeGet.SelectedValue = cbTypeSR.SelectedValue = 0;

        }

        private DataTable createTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("name", typeof(string));
            return dt;
        }

        private void UpdateJournal(DateTime dateStart, DateTime dateEnd)
        {
            dtNoteJournal = Config.hCntMain.getJournalPayments(dateStart, dateEnd);
            dgvNoteJournal.DataSource = dtNoteJournal;
            FilterJournal();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateJournal(DateTime.Parse(dateTimeStart.Value.ToShortDateString()), DateTime.Parse(dateTimeEnd.Value.ToShortDateString()));
         }

        private void FilterJournal()
        {
            //MessageBox.Show(cbTypeGet.SelectedValue.ToString());
            string constFilter = "";
            if (Config.CodeUser.Equals("РКВ"))
                //constFilter += "( id_MoneyRecipient = '" + UserSettings.User.Id + "' OR id_Creator = '" + UserSettings.User.Id + "' )";
                constFilter += "( id_Department = '" + UserSettings.User.IdDepartment + "' OR id_Block = '" + UserSettings.User.IdDepartment + "' )";

            string filter = "";
            filter += constFilter.Length == 0 ? (cbTypeOperation.SelectedIndex == 0 ? ""
                                                            : " typeOperation =" + cbTypeOperation.SelectedIndex)
                                          : (cbTypeOperation.SelectedIndex == 0 ? ""
                                                            : " AND typeOperation =" + cbTypeOperation.SelectedIndex); // typeOperation = 1 or 2
            filter += filter.Length == 0 && constFilter.Length == 0 ? ((int)cbTypeGet.SelectedValue == 0 ? "" : "bCashNonCash = " + ((int)cbTypeGet.SelectedValue - 1)) //TypeGet = 0 or 1
                                    : "" + ((int)cbTypeGet.SelectedValue == 0 ? "" : " AND bCashNonCash = " + ((int)cbTypeGet.SelectedValue - 1));
            filter += filter.Length == 0 && constFilter.Length == 0 ? (cbTypeSR.SelectedIndex <= 0 ? "" : "TypeServiceRecordOnTime = " + cbTypeSR.SelectedIndex)
                        : "" + (cbTypeSR.SelectedIndex <= 0 ? " " : " AND TypeServiceRecordOnTime = " + cbTypeSR.SelectedIndex); //TypeServiceRecordOnTime = 1 or 2


            dtNoteJournal.DefaultView.RowFilter = constFilter + filter;
            dgvNoteJournal.DataSource = dtNoteJournal;
        }

        private void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dtNoteJournal != null ? dtNoteJournal.Rows.Count < 1 ? true : false : true) return;
            if (cbTypeSR.SelectedValue == null) return;
            FilterJournal();

        }

        private void btnPrintJournal_Click(object sender, EventArgs e)
        {
            DataTable dt = ((DataTable)dgvNoteJournal.DataSource).DefaultView.ToTable();
            dt.Columns.Remove("id");
            dt.Columns.Remove("id_MoneyRecipient");
            dt.Columns.Remove("id_Creator");
            dt.Columns.Remove("id_ServiceRecords");
            //dt.Columns.Remove("DateProtect");
            dt.Columns.Remove("bCashNonCash");
            dt.Columns.Remove("typeOperation");
            dt.Columns.Remove("TypeServiceRecordOnTime");
            //dt.Columns.Remove("Type");
            string header = "Отчет по " + ((int)cbTypeOperation.SelectedValue != 0 
                                         ? (int)cbTypeOperation.SelectedValue == 1 ?
                                                                                "выданным" :
                                                                                "возвращенным" :
                                                                                "выданным/возвращенным")
                             + " средствам";

            Logging.StartFirstLevel(79);
            Logging.Comment("Отчет «Отчет по выданным средствам»");
            Logging.Comment("Выгружен в Excel отчет по СЗ со следующими параметрами");
            Logging.Comment("Период с " + dateTimeStart.Value.ToShortDateString() + " по " + dateTimeEnd.Value.ToShortDateString());
            Logging.Comment("Тип операции ID: " + cbTypeOperation.SelectedValue.ToString() + "; наименование: " + cbTypeOperation.Text);
            Logging.Comment("Тип получения ID: " + cbTypeGet.SelectedValue.ToString() + "; наименование: " + cbTypeGet.Text);
            Logging.Comment("Тип СЗ ID: " + cbTypeSR.SelectedValue.ToString() + "; наименование: " + cbTypeSR.Text);

            Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
            Logging.StopFirstLevel();


            Report temp = new Report();
            if (Report.OOAvailable || Report.ExcelAvailable)
            {
                temp.AddSingleValue("{header}", header);
                temp.AddSingleValue("{date_from}", dateTimeStart.Text);
                temp.AddSingleValue("{date_to}", dateTimeEnd.Text);
                temp.AddSingleValue("{type_operation}", cbTypeOperation.Text);
                temp.AddSingleValue("{type_get}", cbTypeGet.Text);
                temp.AddSingleValue("{type_sz}", cbTypeSR.Text);
                temp.AddSingleValue("{name}", UserSettings.User.FullUsername);
                temp.AddSingleValue("{date}", DateTime.Now.ToString());
                temp.AddMultiValues(dt, "dt");
                if (!temp.CreateTemplate(Application.StartupPath + "\\Templates\\Journal", Application.StartupPath + "\\Journal", null))
                {
                    MessageBox.Show(temp.ErrorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            temp.OpenFile(Application.StartupPath + "\\Journal");
        }

        private void dgvNote_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
 
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!Config.CodeUser.Equals("РКВ") && !Config.CodeUser.Equals("ОП")) return;
           if ((int)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["id_Creator"] != Nwuram.Framework.Settings.User.UserSettings.User.Id) return;


            int idOrder = (int)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["id"];
            int id_ServiceRecords = (int)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["id_ServiceRecords"];
            string nameType = (string)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["nameType"];
            decimal Summa = (decimal)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["Summa"];
            decimal maxSumma = (decimal)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["maxSumma"];
            decimal SummaInValuta = (decimal)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["SummaInValuta"];
            //int Number = (int)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["Number"];            
            int Number = dtPayment.DefaultView[dgvNote.CurrentRow.Index]["Number"] == DBNull.Value ? 0 : (int)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["Number"];
            
            string FIO = (string)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["FIO"];
            string Valuta = (string)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["Valuta"];
            int type = (int)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["type"];

            int t = type;
            frmOrderMoney frmO = new frmOrderMoney(Summa.ToString(), Valuta, idOrder, FIO, SummaInValuta)
            {
                type = type,
                status = type == 1 ? 16 : 17,
                id_ServiceRecords = id_ServiceRecords,
                maxSumma = maxSumma,
                valuta = Valuta,
                idDirector = (int)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["id_MoneyRecipient"],
                isEdit = true
            };

            frmOrderMoneyMix frmO2 = new frmOrderMoneyMix(Summa.ToString(),
                 //Valuta, idOrder, FIO, (int)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["typeCashNonCash"]
                 Valuta, idOrder, FIO, dtPayment.DefaultView[dgvNote.CurrentRow.Index]["typeCashNonCash"] == DBNull.Value ? 0 : (int)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["typeCashNonCash"]
                 , SummaInValuta)
            {
                type = type,
                status = type == 1 ? 16 : 17,
                id_ServiceRecords = id_ServiceRecords,
                maxSumma = maxSumma,
                valuta = Valuta,
                idDirector = (int)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["id_MoneyRecipient"],
                isEdit = true
    };

            if ((bool)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["Mix"])
            {
                frmO2.setDirector();
                if (frmO2.ShowDialog() == DialogResult.OK)
                {
                    getData();
                }
            }
            else
            {
                frmO.setDirector();
                if (frmO.ShowDialog() == DialogResult.OK)
                {
                    getData();
                }
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int idOrder = (int)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["id"];
            int id_ServiceRecords = (int)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["id_ServiceRecords"];

            if (DialogResult.Yes == MessageBox.Show(Config.centralText("Вы уверены,\nчто хотите удалить запись\nпо СЗ № " +
                (int)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["Number"] + "?\n"), "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                DataTable dt = Config.hCntMain.deletePayments(idOrder);
                if (dt != null? dt.Rows.Count >0 && dt.Columns.Contains("error") ? true : false: false)
                {
                    MessageBox.Show("Ошибка удаления!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    getData();
                    return;
                }


                DataTable dtTmpMemo = Config.hCntMain.getMemorandums(DateTime.Now, DateTime.Now, id_ServiceRecords, false);
                if (dtTmpMemo != null && dtTmpMemo.Rows.Count > 0)
                {
                    foreach (DataRow row in dtTmpMemo.Rows)
                    {
                        int id_doc = (int)row["id_doc"];
                        Config.hCntDocumentsDZ.rollbackMoveDocument(id_doc);
                    }
                }


                if ((bool)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["Mix"] && dtPayment.Select($"Number = {(int)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["Number"]}").ToList().Count().ToString() == "2")
                    MessageBox.Show(Config.centralText($"Для выполнения повторной операции\nна «{dgvNote.CurrentRow.Cells["cOperation"].Value.ToString() }»  ДС\n" +
                        $"должны быть удалены обе части\nопераций по текущей СЗ на ДС.\n"), "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                Logging.StartFirstLevel(1507);
                Logging.Comment("Произведено удаление операции по ДС");
                Logging.Comment("Предполагаемая дата: " + dtPayment.DefaultView[dgvNote.CurrentRow.Index]["DataSumma"].ToString());
                Logging.Comment("Id: " + dtPayment.DefaultView[dgvNote.CurrentRow.Index]["id_ServiceRecords"].ToString());
                Logging.Comment("Номер СЗ: " + dtPayment.DefaultView[dgvNote.CurrentRow.Index]["Number"].ToString());
                Logging.Comment("Автор ID: " + dtPayment.DefaultView[dgvNote.CurrentRow.Index]["id_Creator"].ToString() + "; ФИО: " + dtPayment.DefaultView[dgvNote.CurrentRow.Index]["Author"].ToString());
                Logging.Comment("Получатель ID: " + dtPayment.DefaultView[dgvNote.CurrentRow.Index]["id_MoneyRecipient"].ToString() + "; ФИО: " + dtPayment.DefaultView[dgvNote.CurrentRow.Index]["FIO"].ToString());
                //Logging.Comment("Сумма: " + dtPayment.DefaultView[dgvNote.CurrentRow.Index]["Summa"].ToString() + " "+ dgvNote.CurrentRow.Cells[Valuta.Index].Value.ToString());// + dtPayment.DefaultView[dgvNote.CurrentRow.Index]["Valuta"].ToString());
                Logging.Comment("Сумма: " + dtPayment.DefaultView[dgvNote.CurrentRow.Index]["Summa"].ToString() + " " + dgvNote.CurrentRow.Cells[ValutaVisible.Index].EditedFormattedValue.ToString());// + dtPayment.DefaultView[dgvNote.CurrentRow.Index]["Valuta"].ToString());

                Logging.Comment("Описание: " + dtPayment.DefaultView[dgvNote.CurrentRow.Index]["Description"]);
                Logging.Comment("Тип операции: " + dtPayment.DefaultView[dgvNote.CurrentRow.Index]["NameType"]);
                
                if (dt != null && dt.Rows.Count > 0 && !dt.Columns.Contains("error"))
                {
                    Logging.Comment("Статус ДО ID: " + dt.Rows[0]["id_prev"].ToString() + "; Наименование: " + dt.Rows[0]["cName_prev"].ToString());
                    Logging.Comment("Статус После ID: " + dt.Rows[0]["id"].ToString() + "; Наименование: " + dt.Rows[0]["cName"].ToString());
                }


                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();

            }
            getData();
        }

        private void dgvNote_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && (int)dtPayment.DefaultView[dgvNote.CurrentRow.Index]["id_Creator"] == Nwuram.Framework.Settings.User.UserSettings.User.Id)
                btnEdit.Enabled = btnDelete.Enabled = true;
            else btnEdit.Enabled = btnDelete.Enabled = false;

            //btnEdit.Enabled = btnDelete.Enabled = true;
        }
    }

}
