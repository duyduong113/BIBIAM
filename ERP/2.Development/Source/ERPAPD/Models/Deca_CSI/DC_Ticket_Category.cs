using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;



namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a DC_Ticket_Category.
    /// </summary>
    public class DC_Ticket_Category
    {
        #region Members

        public string CategoryID { get; set; }


        public string Name { get; set; }


        public string Description { get; set; }


        public string Status { get; set; }


        public int RowID { get; set; }


        public DateTime RowCreatedTime { get; set; }


        public string RowCreatedUser { get; set; }


        public DateTime RowLastUpdatedTime { get; set; }


        public string RowLastUpdatedUser { get; set; }
        public string UserID { get; set; }

        #endregion

        public DC_Ticket_Category()
        { }


        public int Save()
        {
            string PROCEDURE = "p_SaveDC_Ticket_Category";
            SqlParameter[] parameters = new SqlParameter[5];
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
            string PROCEDURE = "p_UpdateDC_Ticket_Category";
            SqlParameter[] parameters = new SqlParameter[6];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CategoryID";
            parameters[i].Value = this.CategoryID;
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
            string PROCEDURE = "p_DeleteDC_Ticket_Category";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@CategoryID";
            parameters[0].Value = CategoryID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);

        }

        public static DC_Ticket_Category GetDC_Ticket_Category(string CategoryID)
        {
            string PROCEDURE = "p_SelectDC_Ticket_Category";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@CategoryID";
            parameters[0].Value = CategoryID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Ticket_Category
            {
                CategoryID = row["CategoryID"].ToString(),
                Name = row["Name"].ToString(),
                Description = row["Description"].ToString(),
                Status = row["Status"].ToString(),
                RowID = Convert.ToInt16(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }

        //public static List<DC_Ticket_Category> GetDC_Ticket_Categorys(string whereCondition, string orderByExpression)
        //{
        //    string PROCEDURE = "p_SelectDC_Ticket_CategorysDynamic";
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
        //    return dt.AsEnumerable().Select(row => new DC_Ticket_Category
        //    {
        //        CategoryID = row["CategoryID"].ToString(),
        //        Name = row["Name"].ToString(),
        //        Description = row["Description"].ToString(),
        //        Status = row["Status"].ToString(),
        //        RowID = Convert.ToInt16(row["RowID"].ToString()),
        //        RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
        //        RowCreatedUser = row["RowCreatedUser"].ToString(),
        //        RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
        //        RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
        //    }).ToList();
        //}

        public static List<DC_Ticket_Category> GetAllDC_Ticket_Categorys()
        {
            string PROCEDURE = "p_SelectDC_Ticket_CategorysAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Ticket_Category
            {
                CategoryID = row["CategoryID"].ToString(),
                Name = row["Name"].ToString(),
                Description = row["Description"].ToString(),
                Status = row["Status"].ToString(),
                RowID = Convert.ToInt16(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }
      
        public static List<DropListDown> GetAllDC_Ticket_Category_Active()
        {
            string PROCEDURE = "p_SelectDC_Ticket_CategorysAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Where(s => s["Status"].ToString() == "True").Select(row => new DropListDown
            {
                Value = row["CategoryID"].ToString(),
                Text = row["Name"].ToString()
                
            }).ToList();
        }

        public static List<DC_Ticket_Category> GetListUserInCategory(string CategoryID)
        {
            string PROCEDURE = "p_SelectDC_Ticket_Category_User";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@CategoryID";
            parameters[0].Value = CategoryID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Ticket_Category
            {
                UserID = row["UserID"].ToString(),
               
            }).ToList();
        }


        public static List<DC_Ticket_Category> GetAllDC_Ticket_CategorysForIX()
        {
            string PROCEDURE = "p_SelectDC_Ticket_CategorysAllForIX";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Ticket_Category
            {
                CategoryID = row["CategoryID"].ToString(),
                Name = row["Name"].ToString(),
                Description = row["Description"].ToString(),
                Status = row["Status"].ToString(),
                RowID = Convert.ToInt16(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }
    }
}
