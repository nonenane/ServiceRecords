using System.Text;
using System.Collections;
using Nwuram.Framework.Data;
using Nwuram.Framework.Settings.Connection;
using System.Data;
using System;
using Nwuram.Framework.Settings.User;
using System.Threading.Tasks;
using System.Threading;


namespace ReportPays
{
    class Procedures : SqlProvider
    {
        public Procedures(string server, string database, string username, string password, string appName)
              : base(server, database, username, password, appName)
        {
        }
        ArrayList ap = new ArrayList();

        public DataTable getDepartments(bool isAll)
        {
            ap.Clear();

            DataTable dtData = executeProcedure("[dbo].[getDepartments]",
                 new string[0] { },
                 new DbType[0] { }, ap);

            if (dtData == null) return null;

            if (dtData.Rows.Count > 0)
            {
                if (isAll)
                {
                    dtData.Columns.Add(new DataColumn("isMain", typeof(bool)) { DefaultValue = false });
                    DataRow newRow = dtData.NewRow();
                    newRow["id"] = 0;
                    newRow["cName"] = "Все";
                    newRow["isMain"] = true;
                    dtData.Rows.Add(newRow);

                    dtData.DefaultView.Sort = "isMain desc, cName asc";
                }
                //else
                //{ }
            }

            return dtData;
        }

        public DataTable GetObjects(bool isAll)
        {
            ap.Clear();
            ap.Add(true);

            DataTable dtData =  executeProcedure("[dbo].[GetObjects]",
                 new string[1] { "@is_active" },
                 new DbType[1] { DbType.Boolean }, ap);


            if (dtData == null) return null;

            if (dtData.Rows.Count > 0)
            {
                if (isAll)
                {
                    dtData.Columns.Add(new DataColumn("isMain", typeof(bool)) { DefaultValue = false });
                    DataRow newRow = dtData.NewRow();
                    newRow["id"] = 0;
                    newRow["cname"] = "Все";
                    newRow["isMain"] = true;
                    dtData.Rows.Add(newRow);

                    dtData.DefaultView.Sort = "isMain desc, cname asc";
                }
                //else
                //{ }
            }

            return dtData;
        }

        public DataTable getDepartmentBonus(bool isAll)
        {
            ap.Clear();

            DataTable dtData = executeProcedure("[dbo].[getDepartmentBonus]",
                 new string[0] { },
                 new DbType[0] { }, ap);


            if (dtData == null) return null;

            if (dtData.Rows.Count > 0)
            {
                if (isAll)
                {
                    dtData.Columns.Add(new DataColumn("isMain", typeof(bool)) { DefaultValue = false });
                    DataRow newRow = dtData.NewRow();
                    newRow["id"] = 0;
                    newRow["nameDep"] = "Все";
                    newRow["isMain"] = true;
                    dtData.Rows.Add(newRow);

                    dtData.DefaultView.Sort = "isMain desc, nameDep asc";
                }
                //else
                //{ }
            }

            return dtData;
        }

        public DataTable GetTypeWorks(bool isAll)
        {
            ap.Clear();
            ap.Add(true);

            DataTable dtData =  executeProcedure("[dbo].[GetTypeWorks]",
                 new string[1] { "@is_active" },
                 new DbType[1] { DbType.Boolean }, ap);


            if (dtData == null) return null;

            if (dtData.Rows.Count > 0)
            {
                if (isAll)
                {
                    dtData.Columns.Add(new DataColumn("isMain", typeof(bool)) { DefaultValue = false });
                    DataRow newRow = dtData.NewRow();
                    newRow["id"] = 0;
                    newRow["cname"] = "Все";
                    newRow["isMain"] = true;
                    dtData.Rows.Add(newRow);

                    dtData.DefaultView.Sort = "isMain desc, cname asc";
                }
                //else
                //{ }
            }

            return dtData;
        }

        public DataTable dmGetDocTypes(bool isAll)
        {
            ap.Clear();
            ap.Add(null);

            DataTable dtData =  executeProcedure("[dbo].[dmGetDocTypes]",
                 new string[1] { "@id_class" },
                 new DbType[1] { DbType.Int32 }, ap);


            if (dtData == null) return null;

            if (dtData.Rows.Count > 0)
            {
                if (isAll)
                {
                    dtData.Columns.Add(new DataColumn("isMain", typeof(bool)) { DefaultValue = false });
                    DataRow newRow = dtData.NewRow();
                    newRow["id_doctype"] = 0;
                    newRow["doctype_name"] = "Все";
                    newRow["isMain"] = true;
                    dtData.Rows.Add(newRow);

                    dtData.DefaultView.Sort = "isMain desc, doctype_name asc";
                }
                //else
                //{ }
            }

            return dtData;
        }

        public DataTable getStatusDocVsProgConfig()
        {
            ap.Clear();
            ap.Add(Nwuram.Framework.Settings.Connection.ConnectionSettings.GetIdProgram());

            return executeProcedure("[dbo].[getStatusDocVsProgConfig]",
                 new string[1] { "@id_prog" },
                 new DbType[1] { DbType.Int32 }, ap);
        }

        public DataTable getReportMemorandumDep(DateTime dateStart, DateTime dateEnd, int id_Object, int id_dep_bonus, int id_distrType, int id_dep, int id_doctype, string listIdStatusDoc)
        {
            ap.Clear();

            ap.Add(dateStart);
            ap.Add(dateEnd);
            ap.Add(id_Object);
            ap.Add(id_dep_bonus);
            ap.Add(id_distrType);
            ap.Add(id_dep);
            ap.Add(id_doctype);
            ap.Add(listIdStatusDoc);

            return executeProcedure("[dbo].[getReportMemorandumDep]",
                 new string[8] { "@dateStart", "@dateEnd", "@id_Object", "@id_dep_bonus", "@id_distrType", "@id_dep", "@id_doctype", "@listIdStatusDoc" },
                 new DbType[8] { DbType.Date, DbType.Date, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.String }, ap);
        }
    }
}
