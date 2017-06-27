using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a Deca_RT_Holiday.
    /// </summary>
    public class Deca_RT_Holiday
    {
        #region Members

        public string HolidayID { get; set; }


        public string Name { get; set; }


        public string Description { get; set; }


        public string Note { get; set; }


        public DateTime StartDate { get; set; }


        public DateTime EndDate { get; set; }


        public string Department { get; set; }


        public string Status { get; set; }


        public int RowID { get; set; }


        public DateTime RowCreatedTime { get; set; }


        public string RowCreatedUser { get; set; }


        public DateTime RowLastUpdatedTime { get; set; }


        public string RowLastUpdatedUser { get; set; }

        #endregion

        public Deca_RT_Holiday()
        { }


        public int Save()
        {
            string PROCEDURE = "p_SaveDeca_RT_Holiday";
            SqlParameter[] parameters = new SqlParameter[9];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Name";
            parameters[i].Value = this.Name;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Description";
            parameters[i].Value = this.Description;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Note";
            parameters[i].Value = this.Note;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@StartDate";
            parameters[i].Value = this.StartDate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@EndDate";
            parameters[i].Value = this.EndDate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Department";
            parameters[i].Value = this.Department;
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
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateDeca_RT_Holiday";
            SqlParameter[] parameters = new SqlParameter[10];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@HolidayID";
            parameters[i].Value = this.HolidayID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Name";
            parameters[i].Value = this.Name;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Description";
            parameters[i].Value = this.Description;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Note";
            parameters[i].Value = this.Note;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@StartDate";
            parameters[i].Value = this.StartDate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@EndDate";
            parameters[i].Value = this.EndDate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Department";
            parameters[i].Value = this.Department;
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
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Delete()
        {
            string PROCEDURE = "p_DeleteDeca_RT_Holiday";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@HolidayID";
            parameters[0].Value = HolidayID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);

        }

        public static Deca_RT_Holiday GetDeca_RT_Holiday(string HolidayID)
        {
            string PROCEDURE = "p_SelectDeca_RT_Holiday";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@HolidayID";
            parameters[0].Value = HolidayID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new Deca_RT_Holiday
            {
                HolidayID = row["HolidayID"].ToString(),
                Name = row["Name"].ToString(),
                Description = row["Description"].ToString(),
                Note = row["Note"].ToString(),
                StartDate = Convert.ToDateTime(row["StartDate"].ToString()),
                EndDate = Convert.ToDateTime(row["EndDate"].ToString()),
                Department = row["Department"].ToString(),
                Status = row["Status"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }

        //public static List<Deca_RT_Holiday> GetDeca_RT_Holidays(string whereCondition, string orderByExpression)
        //{
        //    string PROCEDURE = "p_SelectDeca_RT_HolidaysDynamic";
        //    SqlParameter[] parameters = new SqlParameter[2];
        //    int i = 0;
        //    parameters[i] = new SqlParameter();
        //    parameters[i].ParameterName = "@WhereCondition";
        //    parameters[i].Value = whereCondition;
        //    i++;
        //    parameters[i] = new SqlParameter();
        //    parameters[i].ParameterName = "@OrderByExpression";
        //    parameters[i].Value = orderByExpression;

        //    DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        //    return dt.AsEnumerable().Select(row => new Deca_RT_Holiday
        //    {
        //        HolidayID = row["HolidayID"].ToString(),
        //        Name = row["Name"].ToString(),
        //        Description = row["Description"].ToString(),
        //        Note = row["Note"].ToString(),
        //        StartDate = Convert.ToDateTime(row["StartDate"].ToString()),
        //        EndDate = Convert.ToDateTime(row["EndDate"].ToString()),
        //        Department = row["Department"].ToString(),
        //        Status = Convert.ToBoolean(row["Status"].ToString()),
        //        RowID = Convert.ToInt32(row["RowID"].ToString()),
        //        RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
        //        RowCreatedUser = row["RowCreatedUser"].ToString(),
        //        RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
        //        RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
        //    }).ToList();
        //}
        //get holiday later than CreateAtDate
        public static List<Deca_RT_Holiday> GetDeca_RT_Holiday_ByDate(DateTime CreateAtDate)
        {
            string PROCEDURE = "p_SelectDeca_RT_Holiday_ByDate";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@CreateAtDate";
            parameters[0].Value = CreateAtDate;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new Deca_RT_Holiday
            {
                HolidayID = row["HolidayID"].ToString(),
                Name = row["Name"].ToString(),
                Description = row["Description"].ToString(),
                Note = row["Note"].ToString(),
                StartDate = Convert.ToDateTime(row["StartDate"].ToString()),
                EndDate = Convert.ToDateTime(row["EndDate"].ToString()),
                Department = row["Department"].ToString(),
                Status = row["Status"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

        public static List<Deca_RT_Holiday> GetAllDeca_RT_Holidays()
        {
            string PROCEDURE = "p_SelectDeca_RT_HolidaysAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new Deca_RT_Holiday
            {
                HolidayID = row["HolidayID"].ToString(),
                Name = row["Name"].ToString(),
                Description = row["Description"].ToString(),
                Note = row["Note"].ToString(),
                StartDate = Convert.ToDateTime(row["StartDate"].ToString()),
                EndDate = Convert.ToDateTime(row["EndDate"].ToString()),
                Department = row["Department"].ToString(),
                Status = row["Status"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }
    }
}
