using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a DC_Ticket_WorkingTime.
    /// </summary>
    public class DC_Ticket_WorkingTime
    {
        #region Members

        public string WorkingTimeID { get; set; }


        public string DepartmentID { get; set; }


        public DateTime StartWorkingTime { get; set; }


        public DateTime EndWorkingTime { get; set; }


        public DateTime StartBreakingTime { get; set; }


        public DateTime EndBreakingTime { get; set; }


        public bool Sun { get; set; }


        public bool Mon { get; set; }


        public bool Tue { get; set; }


        public bool Wed { get; set; }


        public bool Thu { get; set; }


        public bool Fri { get; set; }


        public bool Sat { get; set; }


        public double TotalWorkTime { get; set; }


        public double TotalBreakTime { get; set; }


        public string Status { get; set; }


        public int RowID { get; set; }


        public DateTime RowCreatedTime { get; set; }


        public string RowCreatedUser { get; set; }


        public DateTime RowLastUpdatedTime { get; set; }


        public string RowLastUpdatedUser { get; set; }

        #endregion

        public DC_Ticket_WorkingTime()
        { }


        public int Save()
        {
            string PROCEDURE = "p_SaveDC_Ticket_WorkingTime";
            SqlParameter[] parameters = new SqlParameter[14];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@StartWorkingTime";
            parameters[i].Value = this.StartWorkingTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@EndWorkingTime";
            parameters[i].Value = this.EndWorkingTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@StartBreakingTime";
            parameters[i].Value = this.StartBreakingTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@EndBreakingTime";
            parameters[i].Value = this.EndBreakingTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Sun";
            parameters[i].Value = this.Sun;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Mon";
            parameters[i].Value = this.Mon;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Tue";
            parameters[i].Value = this.Tue;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Wed";
            parameters[i].Value = this.Wed;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Thu";
            parameters[i].Value = this.Thu;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Fri";
            parameters[i].Value = this.Fri;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Sat";
            parameters[i].Value = this.Sat;
            i++;
           
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this.Status;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedTime";
            parameters[i].Value = this.RowCreatedTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedUser";
            parameters[i].Value = this.RowCreatedUser;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, parameters);
        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateDC_Ticket_WorkingTime";
            SqlParameter[] parameters = new SqlParameter[18];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WorkingTimeID";
            parameters[i].Value = this.WorkingTimeID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@DepartmentID";
            parameters[i].Value = this.DepartmentID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@StartWorkingTime";
            parameters[i].Value = this.StartWorkingTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@EndWorkingTime";
            parameters[i].Value = this.EndWorkingTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@StartBreakingTime";
            parameters[i].Value = this.StartBreakingTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@EndBreakingTime";
            parameters[i].Value = this.EndBreakingTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Sun";
            parameters[i].Value = this.Sun;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Mon";
            parameters[i].Value = this.Mon;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Tue";
            parameters[i].Value = this.Tue;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Wed";
            parameters[i].Value = this.Wed;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Thu";
            parameters[i].Value = this.Thu;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Fri";
            parameters[i].Value = this.Fri;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Sat";
            parameters[i].Value = this.Sat;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@TotalWorkTime";
            parameters[i].Value = this.TotalWorkTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@TotalBreakTime";
            parameters[i].Value = this.TotalBreakTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this.Status;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedTime";
            parameters[i].Value = this.RowLastUpdatedTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedUser";
            parameters[i].Value = this.RowLastUpdatedUser;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, parameters);
        }

        public int Delete()
        {
            string PROCEDURE = "p_DeleteDC_Ticket_WorkingTime";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@WorkingTimeID";
            parameters[0].Value = WorkingTimeID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, parameters);

        }

        public static DC_Ticket_WorkingTime GetDC_Ticket_WorkingTime(string WorkingTimeID)
        {
            string PROCEDURE = "p_SelectDC_Ticket_WorkingTime";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@WorkingTimeID";
            parameters[0].Value = WorkingTimeID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Ticket_WorkingTime
            {
                WorkingTimeID = row["WorkingTimeID"].ToString(),
                DepartmentID = row["DepartmentID"].ToString(),
                StartWorkingTime = Convert.ToDateTime(row["StartWorkingTime"].ToString()),
                EndWorkingTime = Convert.ToDateTime(row["EndWorkingTime"].ToString()),
                StartBreakingTime = Convert.ToDateTime(row["StartBreakingTime"].ToString()),
                EndBreakingTime = Convert.ToDateTime(row["EndBreakingTime"].ToString()),
                Sun = Convert.ToBoolean(row["Sun"].ToString()),
                Mon = Convert.ToBoolean(row["Mon"].ToString()),
                Tue = Convert.ToBoolean(row["Tue"].ToString()),
                Wed = Convert.ToBoolean(row["Wed"].ToString()),
                Thu = Convert.ToBoolean(row["Thu"].ToString()),
                Fri = Convert.ToBoolean(row["Fri"].ToString()),
                Sat = Convert.ToBoolean(row["Sat"].ToString()),
                TotalWorkTime = Convert.ToInt32(row["TotalWorkTime"].ToString()),
                TotalBreakTime = Convert.ToInt32(row["TotalBreakTime"].ToString()),
                Status =row["Status"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }

        //public static List<DC_Ticket_WorkingTime> GetDC_Ticket_WorkingTimes(string whereCondition, string orderByExpression)
        //{
        //    string PROCEDURE = "p_SelectDC_Ticket_WorkingTimesDynamic";
        //    SqlParameter[] parameters = new SqlParameter[2];
        //    int i = 0;
        //    parameters[i] = new SqlParameter();
        //    parameters[i].ParameterName = "@WhereCondition";
        //    parameters[i].Value = whereCondition;
        //    i++;
        //    parameters[i] = new SqlParameter();
        //    parameters[i].ParameterName = "@OrderByExpression";
        //    parameters[i].Value = orderByExpression;

        //    DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, parameters);
        //    return dt.AsEnumerable().Select(row => new DC_Ticket_WorkingTime
        //    {
        //        WorkingTimeID = row["WorkingTimeID"].ToString(),
        //        DepartmentID = row["DepartmentID"].ToString(),
        //        StartWorkingTime = Convert.ToDateTime(row["StartWorkingTime"].ToString()),
        //        EndWorkingTime = Convert.ToDateTime(row["EndWorkingTime"].ToString()),
        //        StartBreakingTime = Convert.ToDateTime(row["StartBreakingTime"].ToString()),
        //        EndBreakingTime = Convert.ToDateTime(row["EndBreakingTime"].ToString()),
        //        Sun = Convert.ToBoolean(row["Sun"].ToString()),
        //        Mon = Convert.ToBoolean(row["Mon"].ToString()),
        //        Tue = Convert.ToBoolean(row["Tue"].ToString()),
        //        Wed = Convert.ToBoolean(row["Wed"].ToString()),
        //        Thu = Convert.ToBoolean(row["Thu"].ToString()),
        //        Fri = Convert.ToBoolean(row["Fri"].ToString()),
        //        Sat = Convert.ToBoolean(row["Sat"].ToString()),
        //        TotalWorkTime = Convert.ToInt32(row["TotalWorkTime"].ToString()),
        //        TotalBreakTime = Convert.ToInt32(row["TotalBreakTime"].ToString()),
        //        Status = Convert.ToBoolean(row["Status"].ToString()),
        //        RowID = Convert.ToInt32(row["RowID"].ToString()),
        //        RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
        //        RowCreatedUser = row["RowCreatedUser"].ToString(),
        //        RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
        //        RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
        //    }).ToList();
        //}

        public static List<DC_Ticket_WorkingTime> GetAllDC_Ticket_WorkingTimes()
        {
            string PROCEDURE = "p_SelectDC_Ticket_WorkingTimesAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Ticket_WorkingTime
            {
                WorkingTimeID = row["WorkingTimeID"].ToString(),
                DepartmentID = row["DepartmentID"].ToString(),
                StartWorkingTime = Convert.ToDateTime(row["StartWorkingTime"].ToString()),
                EndWorkingTime = Convert.ToDateTime(row["EndWorkingTime"].ToString()),
                StartBreakingTime = Convert.ToDateTime(row["StartBreakingTime"].ToString()),
                EndBreakingTime = Convert.ToDateTime(row["EndBreakingTime"].ToString()),
                Sun = Convert.ToBoolean(row["Sun"].ToString()),
                Mon = Convert.ToBoolean(row["Mon"].ToString()),
                Tue = Convert.ToBoolean(row["Tue"].ToString()),
                Wed = Convert.ToBoolean(row["Wed"].ToString()),
                Thu = Convert.ToBoolean(row["Thu"].ToString()),
                Fri = Convert.ToBoolean(row["Fri"].ToString()),
                Sat = Convert.ToBoolean(row["Sat"].ToString()),
                TotalWorkTime = Convert.ToInt32(row["TotalWorkTime"].ToString()),
                TotalBreakTime = Convert.ToInt32(row["TotalBreakTime"].ToString()),
                Status = row["Status"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

        public static DC_Ticket_WorkingTime GetDC_Ticket_WorkingTime_ByUserID(string UserID)
        {
            string PROCEDURE = "p_SelectDC_Ticket_WorkingTime_ByUserID";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@UserID";
            parameters[0].Value = UserID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Ticket_WorkingTime
            {
                WorkingTimeID = row["WorkingTimeID"].ToString(),
                DepartmentID = row["DepartmentID"].ToString(),
                StartWorkingTime = Convert.ToDateTime(row["StartWorkingTime"].ToString()),
                EndWorkingTime = Convert.ToDateTime(row["EndWorkingTime"].ToString()),
                StartBreakingTime = Convert.ToDateTime(row["StartBreakingTime"].ToString()),
                EndBreakingTime = Convert.ToDateTime(row["EndBreakingTime"].ToString()),
                Sun = Convert.ToBoolean(row["Sun"].ToString()),
                Mon = Convert.ToBoolean(row["Mon"].ToString()),
                Tue = Convert.ToBoolean(row["Tue"].ToString()),
                Wed = Convert.ToBoolean(row["Wed"].ToString()),
                Thu = Convert.ToBoolean(row["Thu"].ToString()),
                Fri = Convert.ToBoolean(row["Fri"].ToString()),
                Sat = Convert.ToBoolean(row["Sat"].ToString()),
                TotalWorkTime = (Convert.ToDateTime(row["EndWorkingTime"].ToString()) - Convert.ToDateTime(row["StartWorkingTime"].ToString())).TotalMinutes,
                TotalBreakTime = (Convert.ToDateTime(row["EndBreakingTime"].ToString()) - Convert.ToDateTime(row["StartBreakingTime"].ToString())).TotalMinutes,
                Status = row["Status"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }
        //public static List<Deca_Department> GetDC_Department(string workingtimeid)
        //{
        //    string PROCEDURE = "p_SelectDC_Ticket_WorkingTime_Getdepartment";
        //    SqlParameter[] parameters = new SqlParameter[1];
        //    parameters[0] = new SqlParameter();
        //    parameters[0].ParameterName = "@WorkingTimeID";
        //    parameters[0].Value = workingtimeid;
        //    DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.Text, PROCEDURE, parameters);
        //    return dt.AsEnumerable().Select(row => new Deca_Department
        //    {
        //        DepartmentID = Convert.ToInt32(row["DepartmentID"].ToString()),
        //        Department = row["Department"].ToString(), 
        //    }).ToList();
        //}
    }
}
