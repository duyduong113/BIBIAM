using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a DC_Ticket_Follower.
    /// </summary>
    public class DC_Ticket_Follower
    {
        #region Members

        public string TicketID { get; set; }


        public string UserID { get; set; }


        public int RowID { get; set; }


        public DateTime RowCreatedTime { get; set; }


        public string RowCreatedUser { get; set; }


        public DateTime RowLastUpdatedTime { get; set; }


        public string RowLastUpdatedUser { get; set; }

        #endregion

        public DC_Ticket_Follower()
        {
        }

        public DC_Ticket_Follower(string UserID)
        { 
            this.UserID = UserID;
        }
        

        public static List<DC_Ticket_Follower> GetListUser()
        {
            string text = "select UserID from [User] where active = 1 and userid <> 'administrator'";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, text, null);
            return dt.AsEnumerable().Select(row => new DC_Ticket_Follower
            {
                UserID = row["UserID"].ToString()
            }).ToList();
        }
        public int Save()
        {
            string PROCEDURE = "p_SaveDC_Ticket_Follower";
            SqlParameter[] parameters = new SqlParameter[4];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@TicketID";
            parameters[i].Value = this.TicketID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UserID";
            parameters[i].Value = this.UserID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedTime";
            parameters[i].Value = this.RowCreatedTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedUser";
            parameters[i].Value = this.RowCreatedUser;
            i++;
            try
            {
                return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            }
            catch
            {
                return 1;
            }
        }

        //public int Update()
        //{
        //    string PROCEDURE = "p_UpdateDC_Ticket_Follower";
        //    SqlParameter[] parameters = new SqlParameter[4];
        //    int i = 0;
        //    parameters[i] = new SqlParameter();
        //    parameters[i].ParameterName = "@TicketID";
        //    parameters[i].Value = this.TicketID;
        //    i++;
        //    parameters[i] = new SqlParameter();
        //    parameters[i].ParameterName = "@UserID";
        //    parameters[i].Value = this.UserID;
        //    i++;
        //    parameters[i] = new SqlParameter();
        //    parameters[i].ParameterName = "@RowLastUpdatedTime";
        //    parameters[i].Value = this.RowLastUpdatedTime;
        //    i++;
        //    parameters[i] = new SqlParameter();
        //    parameters[i].ParameterName = "@RowLastUpdatedUser";
        //    parameters[i].Value = this.RowLastUpdatedUser;
        //    i++;
        //    return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        //}

        public int Delete()
        {
            string PROCEDURE = "p_DeleteDC_Ticket_Follower";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@TicketID";
            parameters[0].Value = TicketID;

            parameters[1] = new SqlParameter();
            parameters[1].ParameterName = "@UserID";
            parameters[1].Value = UserID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);

        }
        public static int DeleteAll( string TicketID)
        {
            string PROCEDURE = "p_DeleteDC_Ticket_Follower_All";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@TicketID";
            parameters[0].Value = TicketID;


            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);

        }
        public static DC_Ticket_Follower GetDC_Ticket_Follower(string TicketID, string UserID)
        {
            string PROCEDURE = "p_SelectDC_Ticket_Follower";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@TicketID";
            parameters[0].Value = TicketID;

            parameters[1] = new SqlParameter();
            parameters[1].ParameterName = "@UserID";
            parameters[1].Value = UserID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Ticket_Follower
            {
                TicketID = row["TicketID"].ToString(),
                UserID = row["UserID"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }

        //public static List<DC_Ticket_Follower> GetDC_Ticket_Followers(string whereCondition, string orderByExpression)
        //{
        //    string PROCEDURE = "p_SelectDC_Ticket_FollowersDynamic";
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
        //    return dt.AsEnumerable().Select(row => new DC_Ticket_Follower
        //    {
        //        TicketID = row["TicketID"].ToString(),
        //        UserID = row["UserID"].ToString(),
        //        RowID = Convert.ToInt16(row["RowID"].ToString()),
        //        RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
        //        RowCreatedUser = row["RowCreatedUser"].ToString(),
        //        RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
        //        RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
        //    }).ToList();
        //}

        public static List<DC_Ticket_Follower> GetAllDC_Ticket_Followers(string TicketID)
        {
            string PROCEDURE = "p_SelectDC_Ticket_FollowersAll";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@TicketID";
            parameters[0].Value = TicketID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Ticket_Follower
            {
                TicketID = row["TicketID"].ToString(),
                UserID = row["UserID"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }
    }
}
