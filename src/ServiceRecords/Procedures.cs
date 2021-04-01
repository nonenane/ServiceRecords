using System.Text;
using System.Collections;
using Nwuram.Framework.Data;
using Nwuram.Framework.Settings.Connection;
using System.Data;
using System;
using Nwuram.Framework.Settings.User;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ServiceRecords
{
    public class Procedures : SqlProvider
    {
        public Procedures(string server, string database, string username, string password, string appName)
            : base(server, database, username, password, appName)
        {
        }
        ArrayList ap = new ArrayList();

        public DateTime GetDate()
        {
            ap.Clear();
            
            DataTable dt = executeProcedure("[ServiceRecords].[GetDate]",
                new string[] {  },
                new DbType[] { }, ap);

            if (dt == null || dt.Rows.Count == 0)
                return DateTime.Now;
            else
                return (DateTime)dt.Rows[0][0];
        }

        public DataTable getDeps()
        {
            ap.Clear();
            return executeProcedure("[ServiceRecords].[getDeps]",
                 new string[] { },
                 new DbType[] { }, ap);
        }

        #region "Работа с файлами"

        public DataTable setScan(int id_ServiceRecords, byte[] byteFile, string nameFile, int typeDoc, string @Extension)
        {
            ap.Clear();
            ap.Add(id_ServiceRecords);
            ap.Add(byteFile);
            ap.Add(nameFile);
            ap.Add(typeDoc);
            ap.Add(UserSettings.User.Id);
            ap.Add(@Extension);

            return executeProcedure("[ServiceRecords].[setScan]",
                new string[] { "@id_ServiceRecords","@byteFile", "@nameFile","@typeFile","@idUser","@Extension" },
                new DbType[] { DbType.Int32, DbType.Binary,DbType.String,DbType.Int32,DbType.Int32,DbType.String }, ap);
        }

        # region Скан пальцев
        public DataTable getFingerScan()
        {
          ap.Clear();
          ap.Add(UserSettings.User.Id);

          return executeProcedure("[ServiceRecords].[getFingerScan]",
              new string[] {"@id_user" },
              new DbType[] { DbType.Int32}, ap);
        }

        #endregion
        public DataTable getScan(int id_ServiceRecords, int id)
        {
            ap.Clear();
            ap.Add(id_ServiceRecords);
            ap.Add(id);

            return executeProcedure("[ServiceRecords].[getScan]",
                new string[] { "@id_ServiceRecords", "@id" },
                new DbType[] { DbType.Int32, DbType.Int32 }, ap);
        }
        public DataTable getScanSignature(int id_user)
        {
            ap.Clear();
            ap.Add(id_user);

            return executeProcedure("[ServiceRecords].[getScanSignature]",
                    new string[] { "@id_User" },
                    new DbType[] { DbType.Int32 }, ap);
        }

        public DataTable updateScanName(int id, string nameFile)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(nameFile);

            return executeProcedure("[ServiceRecords].[updateScanName]",
                new string[] { "@id", "@nameFile" },
                new DbType[] { DbType.Int32, DbType.String }, ap);
        }

        #endregion

        #region "Статусы"

        public DataTable getStatus()
        {
            ap.Clear();
            return executeProcedure("[ServiceRecords].[getStatus]",
                 new string[] { },
                 new DbType[] { }, ap);
        }


        public DataTable getHistoryStatus(int id_ServiceRecords)
        {
            ap.Clear();
            ap.Add(id_ServiceRecords);

            return executeProcedure("[ServiceRecords].[getHistoryStatus]",
                new string[] { "@id_ServiceRecords"},
                new DbType[] { DbType.Int32}, ap);
        }

        #endregion

        #region "Настройки" 

        public void SetSettings(string id_value, string value)
        {
            ap.Clear();
            ap.Add(ConnectionSettings.GetIdProgram());
            ap.Add(id_value);
            ap.Add("N");
            ap.Add("");
            ap.Add(value);

            executeProcedure("[ServiceRecords].[SetSettings]",
                new string[] { "@id_prog", "@id_value", "@type_value", "@value_name", "@value" },
                new DbType[] { DbType.Int32, DbType.String, DbType.String, DbType.String, DbType.String }, ap);
        }

        public string GetSettings(string id_value, int idProg = 0)
        {
            ap.Clear();

            if (idProg == 0)
                ap.Add(ConnectionSettings.GetIdProgram());
            else
                ap.Add(idProg);

            ap.Add(id_value);

            DataTable dt = new DataTable();

            dt = executeProcedure("[ServiceRecords].[GetSettings]",
                new string[2] { "@id_prog", "@id_value" },
                new DbType[2] { DbType.Int32, DbType.String }, ap);

            if ((dt != null) && (dt.Rows.Count > 0))
            {
                return dt.Rows[0]["value"].ToString();
            }
            else
            {
                return "";
            }
        }

        public DataTable GetSettingsTable(string id_value)
        {
            ap.Clear();

            ap.Add(ConnectionSettings.GetIdProgram());

            ap.Add(id_value);


            return executeProcedure("[ServiceRecords].[GetSettings]",
                new string[2] { "@id_prog", "@id_value" },
                new DbType[2] { DbType.Int32, DbType.String }, ap);
        }

        public void SetSettingsMulti(string id_value, string value,string value_name,bool isDel)
        {
            ap.Clear();
            ap.Add(ConnectionSettings.GetIdProgram());
            ap.Add(id_value);
            ap.Add("N");
            ap.Add(value_name);
            ap.Add(value);
            ap.Add(isDel);

            executeProcedure("[ServiceRecords].[SetSettingsMulti]",
                new string[] { "@id_prog", "@id_value", "@type_value", "@value_name", "@value","@isDel" },
                new DbType[] { DbType.Int32, DbType.String, DbType.String, DbType.String, DbType.String,DbType.Boolean }, ap);
        }

        #endregion

        #region "Маршруты"

        public DataTable getRoute()
        {
            ap.Clear();
            return executeProcedure("[ServiceRecords].[getRoute]",
                 new string[] { },
                 new DbType[] { }, ap);
        }

        public DataTable getRouteVsDepartment()
        {
            ap.Clear();
            return executeProcedure("[ServiceRecords].[getRouteVsDepartment]",
                 new string[] { },
                 new DbType[] { }, ap);
        }

        public DataTable setRouteVsDepartment(int id_route, int id_deps, int id)
        {
            ap.Clear();

            ap.Add(id_route);
            ap.Add(id_deps);            
            ap.Add(UserSettings.User.Id);
            ap.Add(id);

            return executeProcedure("[ServiceRecords].[setRouteVsDepartment]",
                 new string[] { "@id_route", "@id_deps", "@id_user", "@id" },
                 new DbType[] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32 }, ap);
        }

        #endregion

        #region "Блоки"

        public DataTable getBlockVsDepartment()
        {
            ap.Clear();
            return executeProcedure("[ServiceRecords].[getBlockVsDepartment]",
                 new string[] { },
                 new DbType[] { }, ap);
        }

        public DataTable getObjects()
        { 
            ap.Clear();
            return executeProcedure("[ServiceRecords].[getObjects]",
                 new string[] { },
                 new DbType[] { }, ap);
        }

        public DataTable setBlockVsDepartment(int id_block, int id_deps, int id, bool isDel)
        {
            ap.Clear();

            ap.Add(id_block);
            ap.Add(id_deps);
            ap.Add(UserSettings.User.Id);
            ap.Add(id);
            ap.Add(isDel);

            return executeProcedure("[ServiceRecords].[setBlockVsDepartment]",
                 new string[] { "@id_block", "@id_deps", "@id_user", "@id","@isDel" },
                 new DbType[] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32,DbType.Boolean }, ap);
        }

        #endregion

        #region "Служебка"
        public DataTable setServiceRecords(
            string Description,
            DateTime CreateServiceRecord,
            int TypeServiceRecord,
            int id_Block,
            int id_Department,
            decimal Summa,
            bool bCashNonCash,
            object DataSumma,
            object bDataSumma,
            DateTime MonthB,
            string Comments,
            int id_Object,
            int TypeServiceRecordOnTime,
            string Valuta,
            decimal SummaCash = 0,
            decimal SummaNonCash = 0,
            bool Mix = false,
            int? id_fond = null,
            int? inType = null)
        {
            ap.Clear();

            ap.Add(Description);
            ap.Add(CreateServiceRecord);
            ap.Add(TypeServiceRecord);
            ap.Add(id_Block);
            ap.Add(id_Department);
            ap.Add(Summa);
            ap.Add(bCashNonCash);
            ap.Add(DataSumma);
            ap.Add(bDataSumma);
            ap.Add(MonthB);
            ap.Add(Comments);
            ap.Add(UserSettings.User.Id);
            ap.Add(id_Object);
            ap.Add(TypeServiceRecordOnTime);
            ap.Add(Valuta);
            ap.Add(SummaCash);
            ap.Add(SummaNonCash);
            ap.Add(Mix);
            ap.Add(id_fond);
            ap.Add(inType);

            return executeProcedure("[ServiceRecords].[setServiceRecords]",
                 new string[] { "@Description",		
			                    "@CreateServiceRecord",
			                    "@TypeServiceRecord",
			                    "@id_Block",	
			                    "@id_Department",
			                    "@Summa",		
			                    "@bCashNonCash",
			                    "@DataSumma",		
			                    "@bDataSumma",				
			                    "@MonthB",			
			                    "@Comments",				
			                    "@idUser",
                                "@id_Object",
                                "@TypeServiceRecordOnTime",
                                "@Valuta",
                                "@SummaCash",
                                "@SummaNonCash",
                                "@Mix",
                                "@id_fond",
                                "@inType"
                            },
                 new DbType[] { 
                                DbType.String,
                                DbType.DateTime,
                                DbType.Int32,
                                DbType.Int32,
                                DbType.Int32,
                                DbType.Decimal,
                                DbType.Boolean,
                                DbType.DateTime,
                                DbType.Boolean,
                                DbType.DateTime,
                                DbType.String,
                                DbType.Int32,
                                DbType.Int32,
                                DbType.Int32,
                                DbType.String,
                                DbType.Decimal,
                                DbType.Decimal,
                                DbType.Boolean,
                                DbType.Int32,
                                DbType.Int32
                 }, ap);
        }

        public DataTable checkBlock(int id_dep)
        {
            ap.Clear();
            ap.Add(id_dep);
            return executeProcedure("[ServiceRecords].[checkBlock]", new string[] { "@id_dep" }, new DbType[] { DbType.Int32 }, ap);
        }


        public DataTable getBlock(int id_block)
        {
            ap.Clear();
            ap.Add(id_block);

            return executeProcedure("[ServiceRecords].[getBlock]",
                 new string[] { "@id_block" },
                 new DbType[] { DbType.Int32 }, ap);
        }

        public DataTable getDirectors()
        {

            ap.Clear();
            return executeProcedure("[ServiceRecords].[getDirectors]",
                 new string[] { },
                 new DbType[] { }, ap);
        }

        public DataTable getUserDepartment()
        {
            ap.Clear();
            //ap.Add(idUser);
            //ap.Add(UserSettings.User.Id);
            ap.Add(UserSettings.User.IdDepartment);

            return executeProcedure("[ServiceRecords].[getUserDepartment]",
                 new string[] { "@idUser" },
                 new DbType[] { DbType.String }, ap);

           }
        
        public DataTable getUserDepartmentId()
        {
            ap.Clear();
            //ap.Add(idUser);
            //ap.Add(UserSettings.User.Id);
            ap.Add(UserSettings.User.IdDepartment);

            return executeProcedure("[ServiceRecords].[getUserDepartmentId]",
                 new string[] { "@idUser" },
                 new DbType[] { DbType.Int32 }, ap);




        }
        
        public DataTable getUserProgramsStatus()
        {
            ap.Clear();
            //ap.Add(idUser);
            ap.Add(UserSettings.User.Id);
            return executeProcedure("[ServiceRecords].[getUserProgramsStatus]",
                   new string[] { "@idUser" },
                   new DbType[] { DbType.Int32 }, ap);
        }

        public DataTable getUsersBlockId(int idDepartment)
        {
            ap.Clear();
            ap.Add(idDepartment);

            return executeProcedure("[ServiceRecords].[getUserBlock]", new string[] { "@idDepartment" }, new DbType[] { DbType.Int32 }, ap);

        }
        public DataTable getUserName()
        {
            ap.Clear();
            //ap.Add(idUser);
            ap.Add(UserSettings.User.Id);

            return executeProcedure("[ServiceRecords].[getUserName]",
                 new string[] { "@idUser" },
                 new DbType[] { DbType.String }, ap);

        }

        public DataTable getServiceRecords(DateTime dateStart, DateTime dateEnd,bool isReport=false)
        {
            ap.Clear();

            ap.Add(dateStart);
            ap.Add(dateEnd);
            ap.Add(isReport);

            return executeProcedure("[ServiceRecords].[getServiceRecords]",
                 new string[3] { "@dateStart", "@dateEnd","@isReport" },
                 new DbType[3] { DbType.Date, DbType.Date,DbType.Boolean }, ap);
        }

        public DataTable delServiceRecords(int id)
        {
            ap.Clear();
            ap.Add(id);

            return executeProcedure("[ServiceRecords].[delServiceRecords]",
                 new string[] { "@id" },
                 new DbType[] { DbType.Int32 }, ap);
        }

        public DataTable getServiceRecordsBody(int id)
        {
            ap.Clear();
            ap.Add(id);

            return executeProcedure("[ServiceRecords].[getServiceRecordsBody]",
                 new string[] { "@id" },
                 new DbType[] { DbType.Int32 }, ap);
        }

        public DataTable getMultipleReceivingMone(int id_ServiceRecords)
        {
            ap.Clear();
            ap.Add(id_ServiceRecords);

            return executeProcedure("[ServiceRecords].[getMultipleReceivingMone]",
                 new string[] { "@id" },
                 new DbType[] { DbType.Int32 }, ap);
        }

        public DataTable setMultipleReceivingMoney(int id_ServiceRecords, string SubNumber, decimal Summa, DateTime DataSumma, int id, bool isDel)
        {
            ap.Clear();

            ap.Add(id_ServiceRecords);
            ap.Add(SubNumber);
            ap.Add(Summa);
            ap.Add(DataSumma);
            //ap.Add(UserSettings.User.Id);
            ap.Add(id);
            ap.Add(isDel);

            return executeProcedure("[ServiceRecords].[setMultipleReceivingMoney]",
                 new string[] { "@id_ServiceRecords", "@SubNumber", "@Summa", "@DataSumma", "@id", "@isDel" },
                 new DbType[] { DbType.Int32, DbType.String, DbType.Decimal, DbType.Date, DbType.Int32, DbType.Boolean }, ap);
        }

        public DataTable updateServiceRecordsStatus(int id_ServiceRecords, int id_status,string comments = "")
        {
            ap.Clear();

            ap.Add(id_ServiceRecords);
            ap.Add(id_status);
            ap.Add(UserSettings.User.Id);
            ap.Add(comments);


            return executeProcedure("[ServiceRecords].[updateServiceRecordsStatus]",
                 new string[] { "@id", "@id_status", "@id_user","@comment" },
                 new DbType[] { DbType.Int32, DbType.Int32, DbType.Int32,DbType.String }, ap);
        }

        public DataTable setPayments(int id_ServiceRecords, DateTime DataSumma, 
                                    decimal Summa, int type, 
                                    int idUser, int idMoneyRecipient,
                                    int status,
                                    decimal summaInValuta,
                                    string Valuta,
                                    int typeCashNonCash = 9
                                    )
        {
            ap.Clear();

            ap.Add(id_ServiceRecords);
            ap.Add(DataSumma);
            ap.Add(Summa);
            ap.Add(type);
            ap.Add(idUser);
            ap.Add(idMoneyRecipient);
            ap.Add(status);
            ap.Add(summaInValuta);
            ap.Add(Valuta);
            ap.Add(typeCashNonCash);

            return executeProcedure("[ServiceRecords].[setPayments]",
                 new string[] { "@id_ServiceRecords", "@DataSumma", "@Summa", "@type",
                                "@idUser", "@idMoneyRecipient", "@status",
                                 "@summaInValuta", "@Valuta", "@typeCashNonCash"},
                 new DbType[] { DbType.Int32, DbType.DateTime, DbType.Decimal, DbType.Int32,
                                DbType.Int32, DbType.Int32, DbType.Int32,
                                DbType.Decimal, DbType.String, DbType.Int32}, ap);
        }


        public DataTable getPayment(int id_user)
        {
            ap.Clear();
            ap.Add(id_user);

            return executeProcedure("[ServiceRecords].[getPayment]",
                 new string[] { "@id_user" },
                 new DbType[] { DbType.Int32 }, ap);
        }

        public DataTable getPaymentOP(int id_user)
        {
            ap.Clear();
            ap.Add(id_user);

            return executeProcedure("[ServiceRecords].[getPaymentOP]",
                 new string[] { "@id_user" },
                 new DbType[] { DbType.Int32 }, ap);
        }

        public DataTable updatePayment(int id,int id_user)
        {
            ap.Clear();

            ap.Add(id);
            ap.Add(id_user);
            //ap.Add(UserSettings.User.Id);


            return executeProcedure("[ServiceRecords].[updatePayment]",
                 new string[] { "@id", "@idUser" },
                 new DbType[] { DbType.Int32,DbType.Int32 }, ap);
        }

        public DataTable updateServiceRecords(
            string Description,
            DateTime CreateServiceRecord,
            int TypeServiceRecord,
            int id_Block,
            int id_Department,
            decimal Summa,
            bool bCashNonCash,
            object DataSumma,
            object bDataSumma,
            DateTime MonthB,
            string Comments,
            int id,
            int id_Object,
            int TypeServiceRecordOnTime,
            string Valuta,
            decimal SummaCash,
            decimal SummaNonCash,
            bool Mix,
            int? id_fond,
            int? inType)
        {
            ap.Clear();

            ap.Add(Description);
            ap.Add(CreateServiceRecord);
            ap.Add(TypeServiceRecord);
            ap.Add(id_Block);
            ap.Add(id_Department);
            ap.Add(Summa);
            ap.Add(bCashNonCash);
            ap.Add(DataSumma);
            ap.Add(bDataSumma);
            ap.Add(MonthB);
            ap.Add(Comments);
            ap.Add(UserSettings.User.Id);
            ap.Add(id);
            ap.Add(id_Object);
            ap.Add(TypeServiceRecordOnTime);
            ap.Add(Valuta);
            ap.Add(SummaCash);
            ap.Add(SummaNonCash);
            ap.Add(Mix);
            ap.Add(id_fond);
            ap.Add(inType);

            return executeProcedure("[ServiceRecords].[updateServiceRecords]",
                 new string[] { "@Description",		
			                    "@CreateServiceRecord",
			                    "@TypeServiceRecord",
			                    "@id_Block",	
			                    "@id_Department",
			                    "@Summa",		
			                    "@bCashNonCash",
			                    "@DataSumma",		
			                    "@bDataSumma",				
			                    "@MonthB",			
			                    "@Comments",				
			                    "@idUser",
	                            "@id",
                                "@id_Object",
                                "@TypeServiceRecordOnTime",
                                "@Valuta",
                                "@SummaCash",
                                "@SummaNonCash",
                                "@Mix",
                                "@id_fond",
                                "@inType"
                            },
                 new DbType[] { 
                                DbType.String,
                                DbType.DateTime,
                                DbType.Int32,
                                DbType.Int32,
                                DbType.Int32,
                                DbType.Decimal,
                                DbType.Boolean,
                                DbType.DateTime,
                                DbType.Boolean,
                                DbType.DateTime,
                                DbType.String,
                                DbType.Int32,
                                DbType.Int32,
                                DbType.Int32,
                                DbType.Int32,
                                DbType.String,
                                DbType.Decimal,
                                DbType.Decimal,
                                DbType.Boolean,
                                DbType.Int32,
                                DbType.Int32
                 }, ap);
        }

        #endregion

        #region "проверка пароля"

        public DataTable setUsers(Guid guiPassword)
        {
            ap.Clear();
            //ap.Add(UserSettings.User.StatusCode == "КД" || UserSettings.User.StatusCode == "КНТ" || UserSettings.User.StatusCode == "ПР");
            //ap.Add(UserSettings.User.Id);
            //ap.Add(DateTime.Now.AddDays(-1));

            ap.Add(UserSettings.User.Id);
            ap.Add(guiPassword);

            return executeProcedure("[ServiceRecords].[setrUsers]",
                new string[2] { "@id_user", "@password" },
                new DbType[2] { DbType.Int32, DbType.Guid },
                ap);
        }


        public DataTable verificationPasswordrUsers(int idUsers, Guid guiPassword)
        {
            ap.Clear();
            //ap.Add(UserSettings.User.StatusCode == "КД" || UserSettings.User.StatusCode == "КНТ" || UserSettings.User.StatusCode == "ПР");
            //ap.Add(UserSettings.User.Id);
            //ap.Add(DateTime.Now.AddDays(-1));

            ap.Add(idUsers);
            ap.Add(guiPassword);

            return executeProcedure("[ServiceRecords].[verificationPasswordrUsers]",
                new string[2] { "@idUsers", "@password" },
                new DbType[2] { DbType.Int32, DbType.Guid },
                ap);
        }

        public DataTable getrUsers()
        {
            ap.Clear();
            //ap.Add(UserSettings.User.StatusCode == "КД" || UserSettings.User.StatusCode == "КНТ" || UserSettings.User.StatusCode == "ПР");
            //ap.Add(UserSettings.User.Id);
            //ap.Add(DateTime.Now.AddDays(-1));

            ap.Add(UserSettings.User.Id);

            return executeProcedure("[ServiceRecords].[getrUsers]",
                new string[1] { "@id_user" },
                new DbType[1] { DbType.Int32},
                ap);
        }

        #endregion

        public DataTable getReportPayment(bool isReport, bool isCart)
        {
            ap.Clear();

            ap.Add(isReport);
            ap.Add(isCart);

            return executeProcedure("[ServiceRecords].[getReportPayment]",
                 new string[] { "@isReport", "@isCart" },
                 new DbType[] {DbType.Boolean,DbType.Boolean }, ap);
        }
        public DataTable addEditObject (string name, int typework, int id = 0)
        {
            ap.Clear();

            ap.Add(id);
            ap.Add(name);
            ap.Add(typework);

            return executeProcedure("[ServiceRecords].[addEditObject]",
                 new string[] { "@id", "@name", "@typework" },
                 new DbType[] { DbType.Int32, DbType.String, DbType.Int32 }, ap);
        }

        public DataTable getJournalPayments(DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            ap.Clear();

            ap.Add(dateTimeStart);
            ap.Add(dateTimeEnd);

            return executeProcedure("[ServiceRecords].[getJournalPayments]",
                 new string[] { "@dateTimeStart", "@dateTimeEnd"},
                 new DbType[] { DbType.DateTime, DbType.DateTime}, ap);
        }

        public DataTable updateStatus(int id_ServiceRecords, int status)
        {
            ap.Clear();

            ap.Add(id_ServiceRecords);
            ap.Add(status);
            ap.Add(UserSettings.User.Id);

            return executeProcedure("[ServiceRecords].[updateStatus]",
                 new string[] { "@id_ServiceRecords", "@status" , "@id_user" },
                 new DbType[] { DbType.Int32, DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable getReportForCheck(DateTime dateTimeStart, DateTime dateTimeEnd,bool isFartForward = false)
        {
            ap.Clear();

            ap.Add(dateTimeStart);
            ap.Add(dateTimeEnd);
            if (new List<string>(new string[] { "КНТ" }).Contains(Config.CodeUser))
                ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.IdDepartment);
            else
                ap.Add(0);

            ap.Add(isFartForward);

            return executeProcedure("[ServiceRecords].[getReportForCheck]",
                 new string[] { "@dateTimeStart", "@dateTimeEnd","@id_Block","@isFartForward" },
                 new DbType[] { DbType.DateTime, DbType.DateTime,DbType.Int32 ,DbType.Boolean}, ap);

        }

        public void updateStatusReport(int id_Report, int status)
        {
            ap.Clear();

            ap.Add(id_Report);
            ap.Add(status);

            executeProcedure("[ServiceRecords].[updateStatusReport]",
                 new string[] { "@id_Report", "@status" },
                 new DbType[] { DbType.Int32, DbType.Int32 }, ap);
        }

        public decimal getSumReports(int id_ServiceRecords)
        {
            //ap.Clear();

            //ap.Add(id_ServiceRecords);
            //DataTable dt = executeProcedure("[ServiceRecords].[getSumReports]",
            //     new string[] { "@id_ServiceRecords" },
            //     new DbType[] { DbType.Int32 }, ap);
            //if (dt != null ? dt.Rows.Count > 0 ? true : false : false)
            //    return decimal.Parse(dt.Rows[0][0].ToString());
            //else return 0;

            return 0;
        }
       
        public decimal getSumPayments(int id_ServiceRecords)
        {
            //ap.Clear();

            //ap.Add(id_ServiceRecords);
            //DataTable dt = executeProcedure("[ServiceRecords].[getSumPayments]",
            //     new string[] { "@id_ServiceRecords" },
            //     new DbType[] { DbType.Int32 }, ap);
            //if (dt != null ? dt.Rows.Count > 0 ? true : false : false)
            //    return decimal.Parse(dt.Rows[0][0].ToString());
            //else return 0;

            return 0;
        }

        public DataTable getHistoryOrderAndReturn(int id_ServiceRecords)
        {
            ap.Clear();

            ap.Add(id_ServiceRecords);

            return executeProcedure("[ServiceRecords].[getHistoryOrderAndReturn]",
                 new string[] { "@id_ServiceRecords" },
                 new DbType[] { DbType.Int32 }, ap);
        }


        public DataTable getHistoryOrderAndReturnMix(int id_ServiceRecords)
        {
            ap.Clear();

            ap.Add(id_ServiceRecords);

            return executeProcedure("[ServiceRecords].[getHistoryOrderAndReturnMix]",
                 new string[] { "@id_ServiceRecords" },
                 new DbType[] { DbType.Int32 }, ap);
        }


        public DataTable updatePayments(int idOrder, DateTime DataSumma,
                                   decimal Summa, int type,
                                   int idUser, int idMoneyRecipient,
                                   int status,
                                   decimal summaInValuta)
        {
            ap.Clear();

            ap.Add(idOrder);
            ap.Add(DataSumma);
            ap.Add(Summa);
            ap.Add(type);
            ap.Add(idUser);
            ap.Add(idMoneyRecipient);
            ap.Add(status);
            ap.Add(summaInValuta);


            return executeProcedure("[ServiceRecords].[updatePayments]",
                 new string[] {"@idOrder",  "@DataSumma", "@Summa", "@type",
                                "@idUser", "@idMoneyRecipient", "@status",
                                 "@summaInValuta"},
                 new DbType[] {DbType.Int32,  DbType.DateTime, DbType.Decimal, DbType.Int32,
                                DbType.Int32, DbType.Int32, DbType.Int32,
                                DbType.Decimal,}, ap);
        }

        public DataTable deletePayments(int idOrder)
        {
            ap.Clear();

            ap.Add(idOrder);
            ap.Add(UserSettings.User.Id);

            return executeProcedure("[ServiceRecords].[deletePayments]",
                 new string[] { "@idOrder", "@id_user"},
                 new DbType[] { DbType.Int32, DbType.Int32 }, ap);

         }


        public void addReport(int id_ServiceRecords, decimal debt, int CashNonCach, int operation, decimal summa_return,int id_payment)
        {
            ap.Clear();

            ap.Add(id_ServiceRecords);
            ap.Add(debt);
            ap.Add(CashNonCach);
            ap.Add(UserSettings.User.Id);
            ap.Add(operation);
            ap.Add(summa_return);
            ap.Add(id_payment);


            executeProcedure("[ServiceRecords].[addReport]",
                 new string[] { "@id_ServiceRecords", "@debt", "@CashNonCach", "@id_creator", "@operation", "@summa_return", "@id_payment" },
                 new DbType[] { DbType.Int32, DbType.Decimal, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Decimal,DbType.Int32 }, ap);

        }

        public void updateReport(int id_ServiceRecords, decimal summa_report, decimal debt, int CashNonCach)
        {
            ap.Clear();

            ap.Add(id_ServiceRecords);
            ap.Add(summa_report);
            ap.Add(debt);
            ap.Add(CashNonCach);
            ap.Add(UserSettings.User.Id);

            executeProcedure("[ServiceRecords].[updateReport]",
                 new string[] { "@id_ServiceRecords", "@summa_report", "@debt", "@CashNonCach", "@id_creator" },
                 new DbType[] { DbType.Int32, DbType.Decimal, DbType.Decimal, DbType.Int32, DbType.Int32 }, ap);

        }

        public int getSettingUpdateButton()
        {
            ap.Clear();

            ap.Add(Nwuram.Framework.Settings.Connection.ConnectionSettings.GetIdProgram());
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);

            DataTable dt = executeProcedure("[ServiceRecords].[getSettingUpdateButton]",
                  new string[] { "@id_prog", "@id_user" },
                  new DbType[] { DbType.Int32, DbType.Int32 }, ap);
            return dt != null ? dt.Rows.Count > 0 ? int.Parse(dt.Rows[0][0].ToString()) : 0 : 0;

        }
        public void setSettingUpdateButton(string val)
        {
            ap.Clear();

            ap.Add(Nwuram.Framework.Settings.Connection.ConnectionSettings.GetIdProgram());
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            ap.Add(val);

            executeProcedure("[ServiceRecords].[setSettingUpdateButton]",
                  new string[] { "@id_prog", "@id_user", "@val" },
                  new DbType[] { DbType.Int32, DbType.Int32, DbType.String }, ap);


        }

        //NEW
        #region "Фонд"
        public DataTable getFondSelect(int id,int  TypeServiceRecordOnTime)
        {
            ap.Clear();

            ap.Add(id);
            ap.Add(TypeServiceRecordOnTime);
            ap.Add(UserSettings.User.IdDepartment);

            return  executeProcedure("[ServiceRecords].[getFondSelect]",
                  new string[3] {"@id", "@TypeServiceRecordOnTime","@id_dep" },
                  new DbType[3] {DbType.Int32,DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable getFondInfo(int? id, int idSR)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(idSR);

            return executeProcedure("[ServiceRecords].[getFondInfo]",
                  new string[2] { "@id", "@idSR" },
                  new DbType[2] { DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable getListRecordFond(int id)
        {
            ap.Clear();
            ap.Add(id);

            return executeProcedure("[ServiceRecords].[getListRecordFond]",
                  new string[1] { "@id" },
                  new DbType[1] { DbType.Int32 }, ap);
        }

        public DataTable validateFondSumma(int? id_fond, int? id)
        {
            ap.Clear();
            ap.Add(id_fond);
            ap.Add(id);

            return executeProcedure("[ServiceRecords].[validateFondSumma]",
                  new string[2] {"@id_fond", "@id" },
                  new DbType[2] { DbType.Int32, DbType.Int32 }, ap);
        }

        public DataTable getDataReportFond(int id_fond)
        {
            ap.Clear();
            ap.Add(id_fond);

            return executeProcedure("[ServiceRecords].[getDataReportFond]",
                  new string[1] { "@id_fond" },
                  new DbType[1] { DbType.Int32 }, ap);
        }

        #endregion

        #region "Типы работ"
        public DataTable getTypicalWorks(bool withAllDeps = false)
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[ServiceRecords].[getTypicalWorks]",
                 new string[0] { },
                 new DbType[0] { }, ap);

            if (withAllDeps)
            {
                if (dtResult != null)
                {
                    if (!dtResult.Columns.Contains("isMain"))
                    {
                        DataColumn col = new DataColumn("isMain", typeof(int));
                        col.DefaultValue = 1;
                        dtResult.Columns.Add(col);
                        dtResult.AcceptChanges();
                    }

                    DataRow row = dtResult.NewRow();

                    row["cName"] = "Все типы работ";
                    row["id"] = 0;
                    row["isMain"] = 0;
                    row["isBonus"] = 0;
                    dtResult.Rows.Add(row);
                    dtResult.AcceptChanges();
                    dtResult.DefaultView.RowFilter = "isActive = 1";
                    dtResult.DefaultView.Sort = "isMain asc, id asc";
                    dtResult = dtResult.DefaultView.ToTable().Copy();
                }
            }
            else
            {
                dtResult.DefaultView.RowFilter = "isActive = 1";
                dtResult.DefaultView.Sort = "id asc";
                dtResult = dtResult.DefaultView.ToTable().Copy();
            }

            return dtResult;
        }

        public DataTable getListHardwareForServiceRecord(int id_ServiceRecord)
        {
            ap.Clear();
            ap.Add(id_ServiceRecord);

            return executeProcedure("[ServiceRecords].[getListHardwareForServiceRecord]",
                  new string[1] { "@id_ServiceRecord" },
                  new DbType[1] { DbType.Int32 }, ap);            

        }


        #endregion

        #region "Документооборот"

        public DataTable getMemorandums(DateTime dateStart,DateTime dateEnd, int id_ListServiceRecords,bool isAll)
        {
            ap.Clear();
            ap.Add(dateStart);
            ap.Add(dateEnd);
            ap.Add(id_ListServiceRecords);
            ap.Add(isAll);

            return executeProcedure("[ServiceRecords].[getMemorandums]",
                  new string[4] { "@dateStart", "@dateEnd", "@id_ListServiceRecords", "@isAll" },
                  new DbType[4] { DbType.Date, DbType.Date, DbType.Int32,DbType.Boolean }, ap);

        }


        public DataTable getListViolation(int id_Memorandums)
        {
            ap.Clear();
            ap.Add(id_Memorandums);

            return executeProcedure("[ServiceRecords].[getListViolation]",
                  new string[1] { "@id_Memorandums" },
                  new DbType[1] { DbType.Int32 }, ap);

        }

        public DataTable setMemorandums(int id, int? id_ListServiceRecords,decimal SumBonus, bool isEdit,bool isDel,bool isUpdateData)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(id_ListServiceRecords);
            ap.Add(SumBonus);
            ap.Add(isEdit);
            ap.Add(UserSettings.User.Id);
            ap.Add(isDel);
            ap.Add(isUpdateData);

            return executeProcedure("[ServiceRecords].[setMemorandums]",
                  new string[7] {"@id", "@id_ListServiceRecords", "@SumBonus", "@isEdit", "@id_user", "@isDel","@isUpdateData" },
                  new DbType[7] { DbType.Int32,DbType.Int32,DbType.Decimal,DbType.Boolean,DbType.Int32,DbType.Boolean, DbType.Boolean }, ap);

        }

        public DataTable setMoveDocument(int? id_doc)
        {
            ap.Clear();
            ap.Add(id_doc);

            return executeProcedure("[dbo].[setMoveDocument]",
                  new string[1] { "@id_doc" },
                  new DbType[1] { DbType.Int32 }, ap);
        }

        public DataTable rollbackMoveDocument(int? id_doc)
        {
            ap.Clear();
            ap.Add(id_doc);

            return executeProcedure("[dbo].[rollbackMoveDocument]",
                  new string[1] { "@id_doc" },
                  new DbType[1] { DbType.Int32 }, ap);
        }

        #endregion


        public DataTable GetLimitSumRecord(int id_ListServiceRecords)
        {
            ap.Clear();
            ap.Add(id_ListServiceRecords);

            return executeProcedure("[ServiceRecords].[GetLimitSumRecord]",
                  new string[1] { "@id_ListServiceRecords" },
                  new DbType[1] { DbType.Int32 }, ap);
        }

    }
}
