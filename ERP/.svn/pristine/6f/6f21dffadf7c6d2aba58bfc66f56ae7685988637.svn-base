using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ERPAPD.Models
{
    public class Deca_Code_Master_List
    {
        public Dictionary<string, string> data;
        public Deca_Code_Master_List(string whereCondition)
        {
            this.data = new Dictionary<string, string>();
            var listdata = Deca_Code_Master.GetDeca_Code_Masters(whereCondition,"");
            foreach (var d in listdata)
            {
                data.Add(d.CodeID, d.Description);
            }
        }

        public Deca_Code_Master_List(string whereCondition, string o)
        {
            this.data = new Dictionary<string, string>();
            var listdata = Deca_Code_Master.GetDeca_Code_Masters(whereCondition, "");
            foreach (var d in listdata)
            {
                data.Add(d.CodeID, d.Description);
            }
        }
        public string SearchID(String CodeID)
        {
            try
            {
                return this.data[CodeID];
            }
            catch { }
            return "";
        }
    }
    public class Deca_Code_Master
    {
        #region Members
        private string _codeid = String.Empty;
        public string CodeID { get { return _codeid; } set { this._codeid = value; } }

        private string _codetype = String.Empty;
        public string CodeType { get { return _codetype; } set { this._codetype = value; } }

        private string _description = String.Empty;
        public string Description { get { return _description; } set { this._description = value; } }

        private bool _active;
        public bool Active { get { return _active; } set { this._active = value; } }

        private int _rowid;
        public int RowID { get { return _rowid; } set { this._rowid = value; } }

        private DateTime _rowcreatedtime;
        public DateTime RowCreatedTime { get { return _rowcreatedtime; } set { this._rowcreatedtime = value; } }

        private string _rowcreateduser = String.Empty;
        public string RowCreatedUser { get { return _rowcreateduser; } set { this._rowcreateduser = value; } }

        private DateTime _rowlastupdatedtime;
        public DateTime RowLastUpdatedTime { get { return _rowlastupdatedtime; } set { this._rowlastupdatedtime = value; } }

        private string _rowlastupdateduser = String.Empty;
        public string RowLastUpdatedUser { get { return _rowlastupdateduser; } set { this._rowlastupdateduser = value; } }

        #endregion

        public Deca_Code_Master()
        { }

        public Deca_Code_Master(string CodeID, string CodeType, string Description, bool Active, int RowID, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
        {
            this._codeid = CodeID;
            this._codetype = CodeType;
            this._description = Description;
            this._active = Active;
            this._rowid = RowID;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;
            this._rowlastupdatedtime = RowLastUpdatedTime;
            this._rowlastupdateduser = RowLastUpdatedUser;
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveDeca_Code_Master";
            SqlParameter[] parameters = new SqlParameter[5];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CodeID";
            parameters[i].Value = this._codeid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CodeType";
            parameters[i].Value = this._codetype;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Description";
            parameters[i].Value = this._description;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedTime";
            parameters[i].Value = this._rowcreatedtime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedUser";
            parameters[i].Value = this._rowcreateduser;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
        }

      
        public int Update()
        {
            string PROCEDURE = "p_UpdateDeca_Code_Master";
            SqlParameter[] parameters = new SqlParameter[5];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CodeID";
            parameters[i].Value = this._codeid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CodeType";
            parameters[i].Value = this._codetype;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Description";
            parameters[i].Value = this._description;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedTime";
            parameters[i].Value = this._rowlastupdatedtime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedUser";
            parameters[i].Value = this._rowlastupdateduser;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Delete()
        {
            string PROCEDURE = "p_DeleteDeca_Code_Master";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CodeID";
            parameters[i].Value = this._codeid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CodeType";
            parameters[i].Value = this._codetype;
            i++;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public static Deca_Code_Master GetDeca_Code_Master(string CodeID,string CodeType)
        {
            string PROCEDURE = "p_SelectDeca_Code_Master";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CodeID";
            parameters[i].Value = CodeID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CodeType";
            parameters[i].Value = CodeType;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new Deca_Code_Master
            {
                CodeID = row["CodeID"].ToString(),
                CodeType = row["CodeType"].ToString(),
                Description = row["Description"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static List<Deca_Code_Master> GetDeca_Code_Masters(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDeca_Code_MastersDynamic";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = whereCondition;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrderByExpression";
            parameters[i].Value = orderByExpression;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new Deca_Code_Master
            {
                CodeID = row["CodeID"].ToString(),
                CodeType = row["CodeType"].ToString(),
                Description = row["Description"].ToString(),
                //Active = Convert.ToBoolean(row["Active"].ToString()),
                //RowID = Convert.ToInt32(row["RowID"].ToString()),
                //RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                //RowCreatedUser = row["RowCreatedUser"].ToString(),
                //RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                //RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }
   
        public static List<Deca_Code_Master> GetDeca_Code_Masters_Replace(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDeca_Code_MastersDynamic";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = whereCondition;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrderByExpression";
            parameters[i].Value = orderByExpression;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new Deca_Code_Master
            {
                CodeID = row["CodeID"].ToString(),
                CodeType = row["CodeType"].ToString(),
                Description = row["Description"].ToString().Replace("#", "\\#"),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }
        public static List<Deca_Code_Master> GetAllDeca_Code_Masters()
        {
            string PROCEDURE = "p_SelectDeca_Code_MastersAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING,CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new Deca_Code_Master
            {
                CodeID = row["CodeID"].ToString(),
                CodeType = row["CodeType"].ToString(),
                Description = row["Description"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }


        public static List<Deca_Code_Master> GetAllDeca_CodeDescription_Masters()
        {
            string PROCEDURE = "p_getDescription";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new Deca_Code_Master
            {
                CodeID = row["CodeID"].ToString(),
                Description = row["Description"].ToString()
               
            }).ToList();
        }

        public static List<Deca_Code_Master> GetListRank()
        {
            string PROCEDURE = "select CodeID,[Description] from Deca_Code_Master where CodeType = 'Rangking'";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new Deca_Code_Master
            {
                CodeID = row["CodeID"].ToString(),
                Description = row["Description"].ToString()

            }).ToList();
        }
        public static int GetID(string CodeType, string Pre)
        {
            string PROCEDURE = "select MAX(SUBSTRING(CodeID," + (Pre.Length + 1).ToString() + ",20)) as Result  from Deca_Code_Master where CodeType = '" + CodeType + "' and CodeID like'" + Pre + "%'";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            try
            {
                return int.Parse(dt.Rows[0]["Result"].ToString());
            }
            catch
            {
                return 0;
            }
        }

        public static Deca_Code_Master GetLastID()
        {
            string PROCEDURE = "Select top 1 * from Deca_Code_Master WHERE CodeType= 'CollectionType' order by RowID desc";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new Deca_Code_Master
            {
                RowID = int.Parse(row["RowID"].ToString()),
                CodeID = row["CodeID"].ToString()
            }).ToList().FirstOrDefault();
        }
    }
}