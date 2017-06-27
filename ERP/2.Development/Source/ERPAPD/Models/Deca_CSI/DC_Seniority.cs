using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a DC_Seniority.
    /// </summary>
    public class DC_Seniority
    {
        #region Members
        private int _seniorityid;
        public int SeniorityID { get { return _seniorityid; } set { this._seniorityid = value; } }

        private string _seniority = String.Empty;
        public string Seniority { get { return _seniority; } set { this._seniority = value; } }

        private double _quit;
        public double Quit { get { return _quit; } set { this._quit = value; } }

        private bool _status;
        public bool Status { get { return _status; } set { this._status = value; } }

        private DateTime _rowcreatedtime;
        public DateTime RowCreatedTime { get { return _rowcreatedtime; } set { this._rowcreatedtime = value; } }

        private string _rowcreateduser = String.Empty;
        public string RowCreatedUser { get { return _rowcreateduser; } set { this._rowcreateduser = value; } }

        private DateTime _rowlastupdatedtime;
        public DateTime RowLastUpdatedTime { get { return _rowlastupdatedtime; } set { this._rowlastupdatedtime = value; } }

        private string _rowlastupdateduser = String.Empty;
        public string RowLastUpdatedUser { get { return _rowlastupdateduser; } set { this._rowlastupdateduser = value; } }

        private int _mounthto;
        public int MounthTo { get { return _mounthto; } set { this._mounthto = value; } }

        private int _mounthfrom;
        public int MounthFrom { get { return _mounthfrom; } set { this._mounthfrom = value; } }

        #endregion

        public DC_Seniority()
        { }

        public DC_Seniority(int SeniorityID, string Seniority, double Quit, bool Status, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser, int MounthTo, int MounthFrom)
        {
            this._seniorityid = SeniorityID;
            this._seniority = Seniority;
            this._quit = Quit;
            this._status = Status;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;
            this._rowlastupdatedtime = RowLastUpdatedTime;
            this._rowlastupdateduser = RowLastUpdatedUser;
            this._mounthto = MounthTo;
            this._mounthfrom = MounthFrom;
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveDC_Seniority";
            SqlParameter[] parameters = new SqlParameter[10];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@SeniorityID";
            parameters[i].Value = this._seniorityid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Seniority";
            parameters[i].Value = this._seniority;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Quit";
            parameters[i].Value = this._quit;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this._status;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedTime";
            parameters[i].Value = this._rowcreatedtime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedUser";
            parameters[i].Value = this._rowcreateduser;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedTime";
            parameters[i].Value = this._rowlastupdatedtime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedUser";
            parameters[i].Value = this._rowlastupdateduser;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@MounthTo";
            parameters[i].Value = this._mounthto;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@MounthFrom";
            parameters[i].Value = this._mounthfrom;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateDC_Seniority";
            SqlParameter[] parameters = new SqlParameter[10];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@SeniorityID";
            parameters[i].Value = this._seniorityid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Seniority";
            parameters[i].Value = this._seniority;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Quit";
            parameters[i].Value = this._quit;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this._status;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedTime";
            parameters[i].Value = this._rowcreatedtime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedUser";
            parameters[i].Value = this._rowcreateduser;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedTime";
            parameters[i].Value = this._rowlastupdatedtime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedUser";
            parameters[i].Value = this._rowlastupdateduser;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@MounthTo";
            parameters[i].Value = this._mounthto;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@MounthFrom";
            parameters[i].Value = this._mounthfrom;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Delete()
        {
            string PROCEDURE = "p_DeleteDC_Seniority";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@SeniorityID";
            parameters[0].Value = SeniorityID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public static DC_Seniority GetDC_Seniority(int SeniorityID)
        {
            string PROCEDURE = "p_SelectDC_Seniority";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@SeniorityID";
            parameters[0].Value = SeniorityID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Seniority
            {
                SeniorityID = Convert.ToInt32(row["SeniorityID"].ToString()),
                Seniority = row["Seniority"].ToString(),
                Quit = Convert.ToDouble(row["Quit"].ToString()),
                Status = Convert.ToBoolean(row["Status"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                MounthTo = Convert.ToInt32(row["MounthTo"].ToString()),
                MounthFrom = Convert.ToInt32(row["MounthFrom"].ToString())
            }).ToList().FirstOrDefault();
        }

        public static List<DC_Seniority> GetDC_Senioritys(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_SenioritiesDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_Seniority
            {
                SeniorityID = Convert.ToInt32(row["SeniorityID"].ToString()),
                Seniority = row["Seniority"].ToString(),
                Quit = Convert.ToDouble(row["Quit"].ToString()),
                Status = Convert.ToBoolean(row["Status"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                MounthTo = Convert.ToInt32(row["MounthTo"].ToString()),
                MounthFrom = Convert.ToInt32(row["MounthFrom"].ToString())
            }).ToList();
        }

        public static List<DC_Seniority> GetAllDC_Senioritys()
        {
            string PROCEDURE = "p_SelectDC_SenioritiesAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Seniority
            {
                SeniorityID = Convert.ToInt32(row["SeniorityID"].ToString()),
                Seniority = row["Seniority"].ToString(),
                Quit = Convert.ToDouble(row["Quit"].ToString()),
                Status = Convert.ToBoolean(row["Status"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                MounthTo = Convert.ToInt32(row["MounthTo"].ToString()),
                MounthFrom = Convert.ToInt32(row["MounthFrom"].ToString()),
                id_default = Convert.ToInt32(row["id_default"].ToString()),
                id_defaultText = row["id_defaultText"].ToString(),
                id_defaultColor = row["id_defaultColor"].ToString()
            }).ToList();
        }


        public static List<DC_Seniority> GetListDC_Senioritys()
        {
            string PROCEDURE = "SELECT SeniorityID,Seniority FROM DC_Seniority ";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Seniority
            {
                SeniorityID = Convert.ToInt32(row["SeniorityID"].ToString()),
                Seniority = row["Seniority"].ToString()
            }).ToList();
        }

        public string id_defaultColor { get; set; }
        public int id_default { get; set; }
        public string id_defaultText { get; set; }

    }
}
