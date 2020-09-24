using Nwuram.Framework.Logging;
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

namespace ServiceRecords.workDoc
{
    public partial class frmDocument : Form
    {
        private int ZoomValue = 10;
        public int typeDoc = 0;
        public int id_ServiceRecords { private get; set; }
        private DataTable dtScan;
        private bool isView = false;

        public frmDocument(bool isView)
        {
            InitializeComponent();
            this.isView = isView;
            dgvScan.AutoGenerateColumns = false;

            btAddFile.Visible = btEditName.Visible = btScan.Visible = !isView;
            if (Config.CodeUser.Equals("КД"))
                btAddFile.Visible = btEditName.Visible = btScan.Visible = false;

            ToolTip tt = new ToolTip();
            tt.SetToolTip(btClose, "Выход");
        }

        private void frmDocument_Load(object sender, EventArgs e)
        {
            getData();
        }

        private void getData()
        {
            dtScan = Config.hCntMain.getScan(id_ServiceRecords, -1);
            if (id_ServiceRecords == -1)
            {
                if (Config.bufferDataTable == null)
                    Config.bufferDataTable = dtScan.Clone();

                if (!Config.bufferDataTable.Columns.Contains("img"))
                    Config.bufferDataTable.Columns.Add("img", typeof(byte[]));

                dgvScan.DataSource = Config.bufferDataTable;
            }
            else
                dgvScan.DataSource = dtScan;
        }

        private void btZoomOut_Click(object sender, EventArgs e)
        {
            ZoomValue -= 1;
            if (ZoomValue == 0)
                ZoomValue = 1;
            imagePanel1.Zoom = ZoomValue * 0.02f;
        }

        private void btZoomIn_Click(object sender, EventArgs e)
        {
            ZoomValue += 1;
            imagePanel1.Zoom = ZoomValue * 0.02f;
        }

        private void Scan()
        {
            try
            {
                Nwuram.Framework.scan.scanImg fImg = new Nwuram.Framework.scan.scanImg();
                fImg.ShowDialog();
                this.TopMost = true;
                byte[] img_array = fImg.img_array;
                this.TopMost = false;
                if (img_array != null)
                {
                    frmNameFile frmNF = new frmNameFile();
                    if (DialogResult.OK == frmNF.ShowDialog())
                    {
                        string fileName = frmNF.getComment;
                        byte[] byteFile = img_array;
                        string @Extension = ".jpg";
                        saveFileToDataBase(byteFile, fileName,@Extension);


                        //MemoryStream ms = new MemoryStream(img_array);
                        //System.Drawing.Image img_end = System.Drawing.Image.FromStream(ms);
                        //dgvUL.CurrentRow.Cells["scan"].Value = ImageToByteArray(ScaleImage(img_end, 800, 1200));

                        //int current_id = Convert.ToInt32(dgvUL.CurrentRow.Cells["id"].Value);
                        //if (id_del_image.Contains(current_id))
                        //{
                        //    id_del_image.Remove(current_id);
                        //}
                        //if (!id_add_image.Contains(current_id))
                        //{
                        //    id_add_image.Add(current_id);
                        //}

                        //imagePanel1.Image = ScaleImage(img_end, 284, 275) as Bitmap;
                    }
                    //SetButtonsEnabled();
                }

            }
            catch
            {
                MessageBox.Show("Ошибка при работе со сканером!");
            }
        }

        private void addFile()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            //fileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png, *.gif, *.wmf) | *.jpg; *.jpeg; *.png; *.gif; *.wmf";
            //fileDialog.Filter = @"(All Image Files)|*.BMP;*.bmp;*.JPG;*.JPEG*.jpg;*.jpeg;*.PNG;*.png;*.GIF;*.gif;*.tif;*.tiff;*.ico;*.ICO" +
                //"|(PNG)|*.PNG;*.png|(JPEG)|*.JPG;*.JPEG*.jpg;*.jpeg|(Bitmap(.BMP,.bmp))|*.BMP;*.bmp|(GIF)|*.GIF;*.gif|(TIF)|*.tif;*.tiff|(ICO)|*.ico;*.ICO";
            //"|(Microsoft Word)|*.doc;*.docx" +
            //"|(Microsoft Excel)|*.xls;*.xlsx" +
            //"|(Portable Document Format)|*.pdf" +
            //"|(ALL FILES)|*.*";
            fileDialog.Filter = "All files (*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = Path.GetFileNameWithoutExtension(fileDialog.FileName);
                string @Extension = Path.GetExtension(fileDialog.FileName);
                byte[] byteFile = File.ReadAllBytes(fileDialog.FileName);

                saveFileToDataBase(byteFile, fileName, @Extension);



                //dtData.DefaultView[dgvUL.CurrentRow.Index]["nameFile"] = fileName;
                //dtData.DefaultView[dgvUL.CurrentRow.Index]["pathFile"] = fileDialog.FileName;

                //dgvUL.CurrentRow.Cells["scan"].Value = File.ReadAllBytes(fileDialog.FileName);
                //(dgvUL.DataSource as DataTable).AcceptChanges();

                //int id_current = Convert.ToInt32(dgvUL.CurrentRow.Cells["id"].Value);

                //if (id_del_image.Contains(id_current))
                //{
                //    id_del_image.Remove(id_current);
                //}

                //if (!id_add_image.Contains(id_current))
                //{
                //    id_add_image.Add(id_current);
                //}

                //SetButtonsEnabled();
            }
        }

        private void btScan_Click(object sender, EventArgs e)
        {
            //MessageBoxManager.Yes = "Сканировать";
            //MessageBoxManager.No = "Из файла";
            //MessageBoxManager.Cancel = "Отмена";
            //MessageBoxManager.Register();

            //DialogResult result = MessageBox.Show("Сканировать изображение или выбрать из файла?", "Тип добавления изображения", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            //if (DialogResult.Yes == result)
            //{
            frmTypeDoc frmTD = new frmTypeDoc();
            if (frmTD.ShowDialog() == DialogResult.OK)
            {
                typeDoc = frmTD.typeDoc;
                Scan();
            }
            //}
            //else
            //    if (DialogResult.No == result)
            //    {
            //        addFile();
            //    }

            //MessageBoxManager.Unregister();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btView_Click(object sender, EventArgs e)
        {
            if (dtScan != null && dtScan.DefaultView.Count > 0 && dgvScan.CurrentRow != null && id_ServiceRecords != -1)
            {
                int indexRow = dgvScan.CurrentRow.Index;
                int id = int.Parse(dtScan.DefaultView[indexRow]["id"].ToString());
                DataTable dtFile = Config.hCntMain.getScan(id_ServiceRecords, id);
                if (dtFile != null && dtFile.Rows.Count > 0 && dtFile.Rows[0]["Scan"] != DBNull.Value)
                {
                    byte[] img = (byte[])dtFile.Rows[0]["Scan"];
                    string @Extension = (string)dtScan.DefaultView[indexRow]["Extension"];

                    try
                    {
                        using (var fs = new FileStream("tmpFile" + @Extension, FileMode.Create, FileAccess.Write))
                        {
                            fs.Write(img, 0, img.Length);
                            Process.Start("tmpFile" + @Extension);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception caught in process: {0}", ex);
                        return;
                    }
                    
                    //MemoryStream ms = new MemoryStream(img);
                    //Bitmap b = new Bitmap(ms);
                    //b.Save("tmpFile.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    //Process.Start("tmpFile.jpg");
                }
            }
            else
                if (id_ServiceRecords == -1 && Config.bufferDataTable != null && Config.bufferDataTable.Rows.Count > 0 && dgvScan.CurrentRow != null)
                {
                    int indexRow = dgvScan.CurrentRow.Index;
                    byte[] img = (byte[])Config.bufferDataTable.DefaultView[indexRow]["img"];
                    string @Extension = (string)Config.bufferDataTable.DefaultView[indexRow]["Extension"];

                    try
                    {
                        using (var fs = new FileStream("tmpFile" + @Extension, FileMode.Create, FileAccess.Write))
                        {
                            fs.Write(img, 0, img.Length);
                            Process.Start("tmpFile" + @Extension);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception caught in process: {0}", ex);
                        return;
                    }


                    //MemoryStream ms = new MemoryStream(img);
                    //Bitmap b = new Bitmap(ms);
                    //b.Save("tmpFile.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    //Process.Start("tmpFile.jpg");
                }
        }
        
        private void btAddFile_Click(object sender, EventArgs e)
        {
            frmTypeDoc frmTD = new frmTypeDoc(typeDoc);
            if (frmTD.ShowDialog() == DialogResult.OK)
            {
                typeDoc = frmTD.typeDoc;
                addFile();
            }
        }

        private void btEditName_Click(object sender, EventArgs e)
        {
            if (id_ServiceRecords != -1 && dtScan != null && dtScan.DefaultView.Count > 0 && dgvScan.CurrentRow != null && id_ServiceRecords != -1)
            {
                int indexRow = dgvScan.CurrentRow.Index;
                int id = int.Parse(dtScan.DefaultView[indexRow]["id"].ToString());
                string oldName = (string)dtScan.DefaultView[indexRow]["cName"];
                frmNameFile frmNF = new frmNameFile();
                if (DialogResult.OK == frmNF.ShowDialog())
                {
                    string fileName = frmNF.getComment;
                    setLog(id_ServiceRecords, "", 0, 2, typeDoc, fileName, oldName);
                    Config.hCntMain.updateScanName(id, fileName);
                    getData();
                }
            }
            else
                if (id_ServiceRecords == -1 && Config.bufferDataTable != null && Config.bufferDataTable.Rows.Count > 0 && dgvScan.CurrentRow != null)
                {

                    int indexRow = dgvScan.CurrentRow.Index;
                    int id = int.Parse(Config.bufferDataTable.DefaultView[indexRow]["id"].ToString());
                    frmNameFile frmNF = new frmNameFile();
                    if (DialogResult.OK == frmNF.ShowDialog())
                    {
                        string fileName = frmNF.getComment;
                        Config.bufferDataTable.DefaultView[indexRow]["cName"] = fileName;
                        Config.bufferDataTable.AcceptChanges();
                    }
                }
        }

        private void saveFileToDataBase(byte[] byteFile, string nameFile, string @Extension)
        {
            if (id_ServiceRecords == -1)
            {
               DataRow row =  Config.bufferDataTable.Rows.Add();
               row["id"] = -1;
               row["cName"] = nameFile;
               row["TypeScan"] = typeDoc;
               row["img"] = byteFile;
               row["Extension"] = @Extension;
               dgvScan_CurrentCellChanged(null, null);
            }
            else
            {
                Config.hCntMain.setScan(id_ServiceRecords, byteFile, nameFile, typeDoc, @Extension);

                setLog(id_ServiceRecords, "", 0, 1, typeDoc, nameFile);

                getData(); 
            }
            
        }

        private void dgvScan_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex != -1 && dtScan != null && dtScan.DefaultView.Count > 0 && id_ServiceRecords!=-1)
            {
                int indexRow = e.RowIndex;
                int TypeScan = int.Parse(dtScan.DefaultView[indexRow]["TypeScan"].ToString());

                Color rColor = Color.White;
                if (TypeScan == 2)
                    rColor = panel2.BackColor;
                else if (TypeScan == 3)
                    rColor = panel1.BackColor;

                dgvScan.Rows[indexRow].DefaultCellStyle.BackColor =
                dgvScan.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;

                dgvScan.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;

            }
            else if (e.RowIndex != -1 && Config.bufferDataTable != null && Config.bufferDataTable.DefaultView.Count > 0 && id_ServiceRecords == -1)
            {
                int indexRow = e.RowIndex;
                int TypeScan = int.Parse(Config.bufferDataTable.DefaultView[indexRow]["TypeScan"].ToString());

                Color rColor = Color.White;
                if (TypeScan == 2)
                    rColor = panel2.BackColor;
                else if (TypeScan == 3)
                    rColor = panel1.BackColor;

                dgvScan.Rows[indexRow].DefaultCellStyle.BackColor =                 
                dgvScan.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;

                dgvScan.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
            }
        }

        private void dgvScan_CurrentCellChanged(object sender, EventArgs e)
        {
            if (id_ServiceRecords!=-1 && dtScan != null && dtScan.DefaultView.Count > 0 && dgvScan.CurrentRow != null)
            {

                int indexRow = dgvScan.CurrentRow.Index;
                int id = int.Parse(dtScan.DefaultView[indexRow]["id"].ToString());
                             
                DataTable dtFile = Config.hCntMain.getScan(id_ServiceRecords, id);
                if (dtFile != null && dtFile.Rows.Count > 0 && dtFile.Rows[0]["Scan"] != DBNull.Value)
                {
                    try
                    {
                        byte[] img = (byte[])dtFile.Rows[0]["Scan"];
                        MemoryStream ms = new MemoryStream(img);
                        Bitmap b = new Bitmap(ms);
                        imagePanel1.Image = (Bitmap)b;
                        ZoomValue = 10;
                        imagePanel1.Zoom = ZoomValue * 0.02f;
                    }
                    catch
                    {
                        imagePanel1.Image = null;
                    }
                }
                else
                {
                    imagePanel1.Image = null;
                }
            }
            else if (id_ServiceRecords == -1 && Config.bufferDataTable != null && Config.bufferDataTable.Rows.Count > 0 && dgvScan.CurrentRow != null)
            {
                try
                {
                    int indexRow = dgvScan.CurrentRow.Index;
                    int id = int.Parse(Config.bufferDataTable.DefaultView[indexRow]["id"].ToString());

                    byte[] img = (byte[])Config.bufferDataTable.DefaultView[indexRow]["img"];

                    MemoryStream ms = new MemoryStream(img);
                    Bitmap b = new Bitmap(ms);
                    imagePanel1.Image = (Bitmap)b;
                    ZoomValue = 10;
                    imagePanel1.Zoom = ZoomValue * 0.02f;
                }
                catch
                {
                    imagePanel1.Image = null;
                }
            }
            else
            {
                imagePanel1.Image = null;
            }
        }

        private void tbNameImg_TextChanged(object sender, EventArgs e)
        {
            setFilter();
        }

        private void setFilter()
        {
            string filter = "";

            filter += (tbNameImg.Text.Length != 0 ?
                (filter.Length == 0 ? "" : " AND ") + "CONVERT(cName, 'System.String') LIKE '%" + tbNameImg.Text + "%'" : "");
                     
            try
            {
                if (id_ServiceRecords == -1)
                {
                    Config.bufferDataTable.DefaultView.RowFilter = filter;
                }
                else
                {
                    dtScan.DefaultView.RowFilter = filter;
                }
                //dtMain.DefaultView.Sort = "DateCreate ASC, Server ASC, DB ASC";
            }
            catch (Exception)
            {
                MessageBox.Show("Некорректное значение фильтра!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }


        private void setLog(int id, string comment, int id_status, int isSend, int TypeScan, string nameFile,string oldName = "")
        {
            DataTable dtTmpData = Config.hCntMain.getServiceRecordsBody(id);
            if (isSend == 1)
            {
                Logging.StartFirstLevel(823);
                Logging.Comment("Добавленный файл: Тип:" + (TypeScan == 1 ? "к описанию СЗ" : (TypeScan == 2 ? "при оплате безналом " : "отчет по тратам ДС по СЗ")) + ";Наименование: " + nameFile);
            }
            else
            {               
                Logging.StartFirstLevel(600);
                Logging.VariableChange("Имя файла", nameFile, oldName);
            }
            //Logging.Comment("Сумма: " + tbMoney.Text);
            //Logging.Comment("Предполагаемая дата" + dtpDate.Value.ToShortDateString());

            //Logging.Comment(chbChangeDirector.Text + ": " + (chbChangeDirector.Checked ? "Включен" : "Отключен"));
            //if (chbChangeDirector.Checked)
            //    Logging.Comment("Получатель отличается от заказчика");
            //Logging.Comment("Руководиель ID: " + cmbDirector.SelectedValue + "; Наименование:" + cmbDirector.Text.ToString());

            Logging.Comment("Id СЗ: " + id);
            Logging.Comment("Номер СЗ: " + dtTmpData.Rows[0]["Number"].ToString());
            Logging.Comment("Тип СЗ: " + ((int)dtTmpData.Rows[0]["TypeServiceRecord"] == 0 ? "стандарт." : "предварит."));
            Logging.Comment("Тип СЗ по времени: " + ((int)dtTmpData.Rows[0]["TypeServiceRecordOnTime"] == 1 ? "разовая" : "ежемесячная"));

            Logging.Comment("Сумма:" + decimal.Parse(dtTmpData.Rows[0]["Summa"].ToString()).ToString("0.00"));
            Logging.Comment("Валюта:" + dtTmpData.Rows[0]["Valuta"].ToString());
            if ((bool)dtTmpData.Rows[0]["Mix"])
            {
                Logging.Comment("Сумма нал:" + dtTmpData.Rows[0]["SummaCash"].ToString());
                Logging.Comment("Сумма безнал:" + dtTmpData.Rows[0]["SummaNonCash"].ToString());
            }

            Logging.Comment("Объект ID: " + dtTmpData.Rows[0]["id_Object"].ToString() + "; Наименование:" + "");
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

            if (comment.Length != 0)
                Logging.Comment("Комментрий с формы:" + comment);

            Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
               + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
            Logging.StopFirstLevel();
        }

        private void dgvScan_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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

        private void btSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName;

            if (dtScan != null && dtScan.DefaultView.Count > 0 && dgvScan.CurrentRow != null && id_ServiceRecords != -1)
            {
                int indexRow = dgvScan.CurrentRow.Index;
                int id = int.Parse(dtScan.DefaultView[indexRow]["id"].ToString());
                DataTable dtFile = Config.hCntMain.getScan(id_ServiceRecords, id);
                if (dtFile != null && dtFile.Rows.Count > 0 && dtFile.Rows[0]["Scan"] != DBNull.Value)
                {
                    byte[] img = (byte[])dtFile.Rows[0]["Scan"];
                    string @Extension = (string)dtScan.DefaultView[indexRow]["Extension"];

                    try
                    {
                        using (var fs = new FileStream(filename + @Extension, FileMode.Create, FileAccess.Write))
                        {
                            fs.Write(img, 0, img.Length);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception caught in process: {0}", ex);
                        return;
                    }
                }
            }
            else
                if (id_ServiceRecords == -1 && Config.bufferDataTable != null && Config.bufferDataTable.Rows.Count > 0 && dgvScan.CurrentRow != null)
            {
                int indexRow = dgvScan.CurrentRow.Index;
                byte[] img = (byte[])Config.bufferDataTable.DefaultView[indexRow]["img"];
                string @Extension = (string)Config.bufferDataTable.DefaultView[indexRow]["Extension"];

                try
                {
                    using (var fs = new FileStream(filename + @Extension, FileMode.Create, FileAccess.Write))
                    {
                        fs.Write(img, 0, img.Length);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception caught in process: {0}", ex);
                    return;
                }
            }
        }

    }
}
