using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a DC_ClassDefinition.
    /// </summary>
    public class DC_ClassDefinition
    {
        #region Members
        private int _classid;
        public int ClassID { get { return _classid; } set { this._classid = value; } }

        private string _classname = String.Empty;
        public string ClassName { get { return _classname; } set { this._classname = value; } }

        private double _min;
        public double Min { get { return _min; } set { this._min = value; } }

        private double _max;
        public double Max { get { return _max; } set { this._max = value; } }

        private double _avg;
        public double AVG { get { return _avg; } set { this._avg = value; } }

        private string _decription = String.Empty;
        public string Decription { get { return _decription; } set { this._decription = value; } }

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

        public int id_default { get; set; }
        public string id_defaultText { get; set; }

        #endregion

        public DC_ClassDefinition()
        { }

        public DC_ClassDefinition(int ClassID, string ClassName, double Min, double Max, double AVG, string Decription, bool Status, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
        {
            this._classid = ClassID;
            this._classname = ClassName;
            this._min = Min;
            this._max = Max;
            this._avg = AVG;
            this._decription = Decription;
            this._status = Status;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;
            this._rowlastupdatedtime = RowLastUpdatedTime;
            this._rowlastupdateduser = RowLastUpdatedUser;
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveDC_ClassDefinition";
            SqlParameter[] parameters = new SqlParameter[11];
            int i = 0;
           
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ClassName";
            parameters[i].Value = this._classname;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Min";
            parameters[i].Value = this._min;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Max";
            parameters[i].Value = this._max;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@AVG";
            parameters[i].Value = this._avg;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Decription";
            parameters[i].Value = this._decription;
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
            parameters[i].ParameterName = "@ClassID";
            parameters[i].Direction = ParameterDirection.InputOutput;
            parameters[i].Value = this._classid;
            i++;

            int rs = SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return int.Parse(parameters[parameters.Length - 1].Value.ToString());
        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateDC_ClassDefinition";
            SqlParameter[] parameters = new SqlParameter[11];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ClassID";
            parameters[i].Value = this._classid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ClassName";
            parameters[i].Value = this._classname;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Min";
            parameters[i].Value = this._min;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Max";
            parameters[i].Value = this._max;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@AVG";
            parameters[i].Value = this._avg;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Decription";
            parameters[i].Value = this._decription;
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
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Delete()
        {
            string PROCEDURE = "p_DeleteDC_ClassDefinition";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ClassID";
            parameters[0].Value = ClassID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public static DC_ClassDefinition GetDC_ClassDefinition(int ClassID)
        {
            string PROCEDURE = "p_SelectDC_ClassDefinition";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ClassID";
            parameters[0].Value = ClassID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_ClassDefinition
            {
                ClassID = Convert.ToInt32(row["ClassID"].ToString()),
                ClassName = row["ClassName"].ToString(),
                Min = Convert.ToDouble(row["Min"].ToString()),
                Max = Convert.ToDouble(row["Max"].ToString()),
                AVG = Convert.ToDouble(row["AVG"].ToString()),
                Decription = row["Decription"].ToString(),
                Status = Convert.ToBoolean(row["Status"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static List<DC_ClassDefinition> GetDC_ClassDefinitions(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_ClassDefinitionsDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_ClassDefinition
            {
                ClassID = Convert.ToInt32(row["ClassID"].ToString()),
                ClassName = row["ClassName"].ToString(),
                Min = Convert.ToDouble(row["Min"].ToString()),
                Max = Convert.ToDouble(row["Max"].ToString()),
                AVG = Convert.ToDouble(row["AVG"].ToString()),
                Decription = row["Decription"].ToString(),
                Status = Convert.ToBoolean(row["Status"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

        public static List<DC_ClassDefinition> GetAllDC_ClassDefinitions()
        {
            string PROCEDURE = "p_SelectDC_ClassDefinitionsAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_ClassDefinition
            {
                ClassID = Convert.ToInt32(row["ClassID"].ToString()),
                ClassName = row["ClassName"].ToString(),
                Min = Convert.ToDouble(row["Min"].ToString()),
                Max = Convert.ToDouble(row["Max"].ToString()),
                AVG = Convert.ToDouble(row["AVG"].ToString()),
                Decription = row["Decription"].ToString(),
                Status = Convert.ToBoolean(row["Status"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                id_default = Convert.ToInt32(row["id_default"].ToString()),
                id_defaultText = row["id_defaultText"].ToString(),
                id_defaultColor = row["id_defaultColor"].ToString()
            }).ToList();
        }


        public static List<DC_ClassDefinition> GetList_ClassDefinitions()
        {
            string PROCEDURE = "SELECT ClassID,ClassName FROM DC_ClassDefinition";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_ClassDefinition
            {
                ClassID = Convert.ToInt32(row["ClassID"].ToString()),
                ClassName = row["ClassName"].ToString(),
            }).ToList();
        }


        public string id_defaultColor { get; set; }

    }
}
