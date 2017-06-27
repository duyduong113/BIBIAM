using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    public class DC_ImportDataFromXLITE
    {
        #region Members
        private string _id = String.Empty;
        public string ID { get { return _id; } set { this._id = value; } }

        private DateTime _date;
        public DateTime Date { get { return _date; } set { this._date = value; } }

        private DateTime _time;
        public DateTime Time { get { return _time; } set { this._time = value; } }

        private string _source = String.Empty;
        public string Source { get { return _source; } set { this._source = value; } }

        private string _destination = String.Empty;
        public string Destination { get { return _destination; } set { this._destination = value; } }

        private string _duration;
        public string Duration { get { return _duration; } set { this._duration = value; } }

        private string _type = String.Empty;
        public string Type { get { return _type; } set { this._type = value; } }

        private string _file = String.Empty;
        public string File { get { return _file; } set { this._file = value; } }

        private DateTime _lastupdatedtime;
        public DateTime LastUpdatedTime { get { return _lastupdatedtime; } set { this._lastupdatedtime = value; } }

        private string _lastupdateduser = String.Empty;
        public string LastUpdatedUser { get { return _lastupdateduser; } set { this._lastupdateduser = value; } }

        public string CustomerID { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public double CreditLimit { get; set; }
        public string DCStatus { get; set; }
        public string Gender { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string EmployeeID { get; set; }
        public string OrganizationID { get; set; }
        public string PhysicalID { get; set; }
        public string BD { get; set; }
        public double RunningBalance { get; set; }
        public double SettledBalance { get; set; }
        public double DueBalance { get; set; }
        public double DueLimit { get; set; }
        #endregion

        public DC_ImportDataFromXLITE()
        { }

        public DC_ImportDataFromXLITE(string ID, DateTime Date, DateTime Time, string Source, string Destination, string Duration, string Type, string File, DateTime LastUpdatedTime, string LastUpdatedUser)
        {
            this._id = ID;
            this._date = Date;
            this._time = Time;
            this._source = Source;
            this._destination = Destination;
            this._duration = Duration;
            this._type = Type;
            this._file = File;
            this._lastupdatedtime = LastUpdatedTime;
            this._lastupdateduser = LastUpdatedUser;
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveDC_ImportDataFromXLITE";
            SqlParameter[] parameters = new SqlParameter[10];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ID";
            parameters[i].Value = this._id;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Date";
            parameters[i].Value = this._date;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Time";
            parameters[i].Value = this._time;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Source";
            parameters[i].Value = this._source;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Destination";
            parameters[i].Value = this._destination;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Duration";
            parameters[i].Value = this._duration;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Type";
            parameters[i].Value = this._type;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@File";
            parameters[i].Value = this._file;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LastUpdatedTime";
            parameters[i].Value = this._lastupdatedtime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LastUpdatedUser";
            parameters[i].Value = this._lastupdateduser;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateDC_ImportDataFromXLITE";
            SqlParameter[] parameters = new SqlParameter[10];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ID";
            parameters[i].Value = this._id;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Date";
            parameters[i].Value = this._date;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Time";
            parameters[i].Value = this._time;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Source";
            parameters[i].Value = this._source;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Destination";
            parameters[i].Value = this._destination;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Duration";
            parameters[i].Value = this._duration;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Type";
            parameters[i].Value = this._type;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@File";
            parameters[i].Value = this._file;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LastUpdatedTime";
            parameters[i].Value = this._lastupdatedtime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LastUpdatedUser";
            parameters[i].Value = this._lastupdateduser;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Delete()
        {
            string PROCEDURE = "p_DeleteDC_ImportDataFromXLITE";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ID";
            parameters[0].Value = ID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public static DC_ImportDataFromXLITE GetDC_ImportDataFromXLITE(string ID)
        {
            string PROCEDURE = "p_SelectDC_ImportDataFromXLITE";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ID";
            parameters[0].Value = ID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_ImportDataFromXLITE
            {
                ID = row["ID"].ToString(),
                Date = Convert.ToDateTime(row["Date"].ToString()),
                Time = Convert.ToDateTime(row["Time"].ToString()),
                Source = row["Source"].ToString(),
                Destination = row["Destination"].ToString(),
                Duration = row["Duration"].ToString(),
                Type = row["Type"].ToString(),
                File = row["File"].ToString(),
                LastUpdatedTime = Convert.ToDateTime(row["LastUpdatedTime"].ToString()),
                LastUpdatedUser = row["LastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static List<DC_ImportDataFromXLITE> GetDC_ImportDataFromXLITEs(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_ImportDataFromXLITEsDynamic";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = whereCondition;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrderByExpression";
            parameters[i].Value = orderByExpression;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_ImportDataFromXLITE
            {
                ID = row["ID"].ToString(),
                Date = Convert.ToDateTime(row["Date"].ToString()),
                Time = Convert.ToDateTime(row["Time"].ToString()),
                Source = row["Source"].ToString(),
                Destination = row["Destination"].ToString(),
                Duration = row["Duration"].ToString(),
                Type = row["Type"].ToString(),
                File = row["File"].ToString(),
                LastUpdatedTime = Convert.ToDateTime(row["LastUpdatedTime"].ToString()),
                LastUpdatedUser = row["LastUpdatedUser"].ToString()
            }).ToList();
        }

        public static List<DC_ImportDataFromXLITE> GetAllDC_ImportDataFromXLITEs()
        {
            string PROCEDURE = "p_SelectDC_ImportDataFromXLITEsAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_ImportDataFromXLITE
            {
                ID = row["ID"].ToString(),
                Date = Convert.ToDateTime(row["Date"].ToString()),
                Time = Convert.ToDateTime(row["Time"].ToString()),
                Source = row["Source"].ToString(),
                Destination = row["Destination"].ToString(),
                Duration = row["Duration"].ToString(),
                Type = row["Type"].ToString(),
                File = row["File"].ToString(),
                LastUpdatedTime = Convert.ToDateTime(row["LastUpdatedTime"].ToString()),
                LastUpdatedUser = row["LastUpdatedUser"].ToString(),

                CustomerID = row["CustomerID"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                FullName = row["FullName"].ToString(),
                Position = row["Position"].ToString(),
                PhysicalID = row["PhysicalID"].ToString(),
                BD = row["BD"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                CreditLimit = double.Parse(row["CreditLimit"].ToString()),
                DCStatus = row["DCStatus"].ToString(),
                Gender = row["Gender"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Email = row["Email"].ToString(),
                DueLimit = double.Parse(row["DueLimit"].ToString()),
            }).ToList();
        }

        public static List<DC_ImportDataFromXLITE> GetTypeFromXLITEs()
        {
            string PROCEDURE = "SELECT DISTINCT [Type] from DC_Xlite_History";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_ImportDataFromXLITE
            {
                Type = row["Type"].ToString(),
            }).ToList();
        }

        public static List<DC_ImportDataFromXLITE> GetListOrg()
        {
            string PROCEDURE = "select distinct OrganizationID from DW_DC_Organization";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_ImportDataFromXLITE
            {
                OrganizationID = row["OrganizationID"].ToString(),
            }).ToList();
        }

        public static List<DC_ImportDataFromXLITE> GetAllDC_ImportDataFromXLITEDynamicExport(string WhereCondition)
        {
            string PROCEDURE = "p_SelectDC_ImportDataFromXLITEsAllDynamicExport";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = WhereCondition;
            i++;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_ImportDataFromXLITE
            {
                ID = row["ID"].ToString(),
                Date = Convert.ToDateTime(row["Date"].ToString()),
                Time = Convert.ToDateTime(row["Time"].ToString()),
                Source = row["Source"].ToString(),
                Destination = row["Destination"].ToString(),
                Duration = row["Duration"].ToString(),
                Type = row["Type"].ToString(),
                File = row["File"].ToString(),
                LastUpdatedTime = Convert.ToDateTime(row["LastUpdatedTime"].ToString()),
                LastUpdatedUser = row["LastUpdatedUser"].ToString(),

                CustomerID = row["CustomerID"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                FullName = row["FullName"].ToString(),
                Position = row["Position"].ToString(),
                PhysicalID = row["PhysicalID"].ToString(),
                BD = row["BD"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                CreditLimit = double.Parse(row["CreditLimit"].ToString()),
                DCStatus = row["DCStatus"].ToString(),
                Gender = row["Gender"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Email = row["Email"].ToString(),
                DueLimit = double.Parse(row["DueLimit"].ToString()),
            }).ToList();
        }


        public static DataSourceResult GetAllDC_ImportDataFromXLITEDynamicRead(string WhereCondition, [DataSourceRequest] DataSourceRequest request)
        {
            var from1 = (request.Page - 1) * request.PageSize;
            var to = (request.Page - 1) * request.PageSize + request.PageSize;

            string PROCEDURE = "p_SelectDC_ImportDataFromXLITEsAllDynamic";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = WhereCondition;
            i++;

            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition1";
            parameters[i].Value = "RowNum BETWEEN " + from1 + " AND " + to;
            i++;

            var data = new DataSourceResult();
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, PROCEDURE, parameters);
            data.Data = dt.AsEnumerable().Select(row => new DC_ImportDataFromXLITE
            {
                ID = row["ID"].ToString(),
                Date = Convert.ToDateTime(row["Date"].ToString()),
                Time = Convert.ToDateTime(row["Date"].ToString()),
                Source = row["Source"].ToString(),
                Destination = row["Destination"].ToString(),
                Duration = row["Duration"].ToString(),
                Type = row["Type"].ToString(),
                File = row["File"].ToString(),
                LastUpdatedTime = Convert.ToDateTime(row["LastUpdatedTime"].ToString()),
                LastUpdatedUser = row["LastUpdatedUser"].ToString(),

                CustomerID = row["CustomerID"].ToString(),
                EmployeeID = row["EmployeeID"].ToString(),
                FullName = row["FullName"].ToString(),
                Position = row["Position"].ToString(),
                PhysicalID = row["PhysicalID"].ToString(),
                BD = row["BD"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                CreditLimit = double.Parse(row["CreditLimit"].ToString()),
                DCStatus = row["DCStatus"].ToString(),
                Gender = row["Gender"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                Email = row["Email"].ToString(),
                DueLimit = double.Parse(row["DueLimit"].ToString()),
            }).ToList();

            var total = dt.AsEnumerable().Select(row => new DC_ImportDataFromXLITE
            {
                TotalRows = Int32.Parse(row["TotalRows"].ToString())
            }).ToList().FirstOrDefault();

            data.Total = total != null ? total.TotalRows : 0;

            return data;
        }

        public int TotalRows { get; set; }
    }
}