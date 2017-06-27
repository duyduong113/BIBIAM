﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a CRM_Ticket_Comment.
    /// </summary>
    public class CRM_Ticket_Comment
    {
        #region Members

        public string TicketID { get; set; }


        public string Comment { get; set; }


        public int RowID { get; set; }


        public DateTime RowCreatedTime { get; set; }


        public string RowCreatedUser { get; set; }


        public DateTime RowLastUpdatedTime { get; set; }


        public string RowLastUpdatedUser { get; set; }

        #endregion

        public CRM_Ticket_Comment()
        { }


        public int Save()
        {
            string PROCEDURE = "p_SaveCRM_Ticket_Comment";
            SqlParameter[] parameters = new SqlParameter[4];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@TicketID";
            parameters[i].Value = this.TicketID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Comment";
            parameters[i].Value = this.Comment;
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

        //public int Update()
        //{
        //    string PROCEDURE = "p_UpdateCRM_Ticket_Comment";
        //    SqlParameter[] parameters = new SqlParameter[4];
        //    int i = 0;
        //    parameters[i] = new SqlParameter();
        //    parameters[i].ParameterName = "@TicketID";
        //    parameters[i].Value = this.TicketID;
        //    i++;
        //    parameters[i] = new SqlParameter();
        //    parameters[i].ParameterName = "@Comment";
        //    parameters[i].Value = this.Comment;
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
            string PROCEDURE = "p_DeleteCRM_Ticket_Comment";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@TicketID";
            parameters[0].Value = TicketID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);

        }

        //public static CRM_Ticket_Comment GetCRM_Ticket_Comment(string TicketID)
        //{
        //    string PROCEDURE = "p_SelectCRM_Ticket_Comment";
        //    SqlParameter[] parameters = new SqlParameter[1];
        //    parameters[0] = new SqlParameter();
        //    parameters[0].ParameterName = "@TicketID";
        //    parameters[0].Value = TicketID;

        //    DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        //    return dt.AsEnumerable().Select(row => new CRM_Ticket_Comment
        //    {
        //        TicketID = row["TicketID"].ToString(),
        //        Comment = row["Comment"].ToString(),
        //        RowID = Convert.ToInt16(row["RowID"].ToString()),
        //        RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
        //        RowCreatedUser = row["RowCreatedUser"].ToString(),
        //        RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
        //        RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
        //    }).ToList().FirstOrDefault();
        //}

        //public static List<CRM_Ticket_Comment> GetCRM_Ticket_Comments(string whereCondition, string orderByExpression)
        //{
        //    string PROCEDURE = "p_SelectCRM_Ticket_CommentsDynamic";
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
        //    return dt.AsEnumerable().Select(row => new CRM_Ticket_Comment
        //    {
        //        TicketID = row["TicketID"].ToString(),
        //        Comment = row["Comment"].ToString(),
        //        RowID = Convert.ToInt16(row["RowID"].ToString()),
        //        RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
        //        RowCreatedUser = row["RowCreatedUser"].ToString(),
        //        RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
        //        RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
        //    }).ToList();
        //}

        public static List<CRM_Ticket_Comment> GetAllCRM_Ticket_Comments(string TicketID)
        {
            string PROCEDURE = "p_SelectCRM_Ticket_CommentsAll";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@TicketID";
            parameters[0].Value = TicketID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new CRM_Ticket_Comment
            {
                TicketID = row["TicketID"].ToString(),
                Comment = row["Comment"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }
    }
}
