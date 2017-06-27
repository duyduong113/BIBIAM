using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a Deca_RT_Type.
    /// </summary>
    public class Deca_RT_Type
    {
        #region Members

        public string TypeID { get; set; }


        public string Name { get; set; }


        public string Description { get; set; }


        public string Category { get; set; }


        public string Owner { get; set; }


        public int ResponeTime { get; set; }


        public int ResolveTime { get; set; }


        public int ClosedTime { get; set; }


        public string QueueList { get; set; }

        public string RequireCustomerID { get; set; }
        public string Status { get; set; }
        public int Priority { get; set; }


        public int RowID { get; set; }


        public DateTime RowCreatedTime { get; set; }


        public string RowCreatedUser { get; set; }


        public DateTime RowLastUpdatedTime { get; set; }


        public string RowLastUpdatedUser { get; set; }
        public string UserName { get; set; }

        public string RequireOrderID { get; set; }
        #endregion

        public Deca_RT_Type()
        { }


        public int Save()
        {
            string PROCEDURE = "p_SaveDeca_RT_Type";
            SqlParameter[] parameters = new SqlParameter[14];
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
            parameters[i].ParameterName = "@Category";
            parameters[i].Value = this.Category;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Owner";
            parameters[i].Value = this.Owner;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ResponeTime";
            parameters[i].Value = this.ResponeTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ResolveTime";
            parameters[i].Value = this.ResolveTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ClosedTime";
            parameters[i].Value = this.ClosedTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@QueueList";
            parameters[i].Value = this.QueueList;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RequireCustomerID";
            parameters[i].Value = this.RequireCustomerID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RequireOrderID";
            parameters[i].Value = this.RequireOrderID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this.Status;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Priority";
            parameters[i].Value = this.Priority;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedTime";
            parameters[i].Value = this.RowCreatedTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedUser";
            parameters[i].Value = this.RowCreatedUser;
            i++;
            var result =  SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);

            return result;
        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateDeca_RT_Type";
            SqlParameter[] parameters = new SqlParameter[15];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@TypeID";
            parameters[i].Value = this.TypeID;
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
            parameters[i].ParameterName = "@Category";
            parameters[i].Value = this.Category;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Owner";
            parameters[i].Value = this.Owner;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ResponeTime";
            parameters[i].Value = this.ResponeTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ResolveTime";
            parameters[i].Value = this.ResolveTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ClosedTime";
            parameters[i].Value = this.ClosedTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@QueueList";
            parameters[i].Value = this.QueueList;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RequireCustomerID";
            parameters[i].Value = this.RequireCustomerID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RequireOrderID";
            parameters[i].Value = this.RequireOrderID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this.Status;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Priority";
            parameters[i].Value = this.Priority;
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
            string PROCEDURE = "p_DeleteDeca_RT_Type";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@TypeID";
            parameters[0].Value = TypeID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);

        }
        public static Deca_RT_Type GetDeca_RT_Type(string TypeID)
        {
            string PROCEDURE = "p_SelectDeca_RT_Type";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@TypeID";
            parameters[0].Value = TypeID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new Deca_RT_Type
            {
                TypeID = row["TypeID"].ToString(),
                Name = row["Name"].ToString(),
                Description = row["Description"].ToString(),
                Category = row["Category"].ToString(),
                Owner = row["Owner"].ToString(),
                ResponeTime = Convert.ToInt32(row["ResponeTime"].ToString()),
                ResolveTime = Convert.ToInt32(row["ResolveTime"].ToString()),
                ClosedTime = Convert.ToInt32(row["ClosedTime"].ToString()),
                QueueList = row["QueueList"].ToString(),
                RequireCustomerID = row["RequireCustomerID"].ToString(),
                RequireOrderID = row["RequireOrderID"].ToString(),
                Status = row["Status"].ToString(),
                Priority = Convert.ToInt32(row["Priority"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }
        

        //public static List<Deca_RT_Type> GetDeca_RT_Types(string whereCondition, string orderByExpression)
        //{
        //    string PROCEDURE = "p_SelectDeca_RT_TypesDynamic";
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
        //    return dt.AsEnumerable().Select(row => new Deca_RT_Type
        //    {
        //        TypeID = row["TypeID"].ToString(),
        //        Name = row["Name"].ToString(),
        //        Description = row["Description"].ToString(),
        //        Category = row["Category"].ToString(),
        //        Owner = row["Owner"].ToString(),
        //        Priority = row["Priority"].ToString(),
        //        Impact = row["Impact"].ToString(),
        //        ResponeTime = Convert.ToInt32(row["ResponeTime"].ToString()),
        //        ResolveTime = Convert.ToInt32(row["ResolveTime"].ToString()),
        //        ClosedTime = Convert.ToInt32(row["ClosedTime"].ToString()),
        //        QueueList = row["QueueList"].ToString(),
        //        Status = row["Status"].ToString(),
        //        RowID = Convert.ToInt32(row["RowID"].ToString()),
        //        RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
        //        RowCreatedUser = row["RowCreatedUser"].ToString(),
        //        RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
        //        RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
        //    }).ToList();
        //}

        
        public static List<Deca_RT_Type> GetAllDeca_RT_Types()
        {
            string PROCEDURE = "p_SelectDeca_RT_TypesAll";
            
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new Deca_RT_Type
            {
                    TypeID = row["TypeID"].ToString(), 
                    Name = row["Name"].ToString(), 
                    Description = row["Description"].ToString(), 
                    Category = row["Category"].ToString(),
                    Owner = row["Owner"].ToString(),
                    ResponeTime = Convert.ToInt32(row["ResponeTime"].ToString()),
                    ResolveTime = Convert.ToInt32(row["ResolveTime"].ToString()),
                    ClosedTime = Convert.ToInt32(row["ClosedTime"].ToString()),
                    QueueList = row["QueueList"].ToString(),
                    RequireCustomerID = row["RequireCustomerID"].ToString(),
                    RequireOrderID = row["RequireOrderID"].ToString(),
                    Status = row["Status"].ToString(),
                    Priority = Convert.ToInt32(row["Priority"].ToString()),
                    RowID = Convert.ToInt32(row["RowID"].ToString()), 
                    RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()), 
                    RowCreatedUser = row["RowCreatedUser"].ToString(), 
                    RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()), 
                    RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }

        public static List<Deca_RT_Type> GetAllDeca_RT_Types_Category(string categoryid)
        {
            string PROCEDURE = "p_SelectDeca_RT_TypesAll_Category";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@categoryid";
            parameters[i].Value = categoryid;
            i++;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new Deca_RT_Type
            {
                TypeID = row["TypeID"].ToString(),
                Name = row["Name"].ToString(),
                Description = row["Description"].ToString(),
                Category = row["Category"].ToString(),
                Owner = row["Owner"].ToString(),
                ResponeTime = Convert.ToInt32(row["ResponeTime"].ToString()),
                ResolveTime = Convert.ToInt32(row["ResolveTime"].ToString()),
                ClosedTime = Convert.ToInt32(row["ClosedTime"].ToString()),
                QueueList = row["QueueList"].ToString(),
                RequireCustomerID = row["RequireCustomerID"].ToString(),
                RequireOrderID = row["RequireOrderID"].ToString(),
                Status = row["Status"].ToString(),
                Priority = Convert.ToInt32(row["Priority"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList();
        }
        public static List<DropListDown> GetAllDeca_RT_Types_Active()
        {
            string PROCEDURE = "p_SelectDeca_RT_TypesAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Where(s => s["Status"].ToString() == "True").Select(row => new DropListDown
            {
                Value = row["TypeID"].ToString(),
                Text = row["Name"].ToString()

            }).ToList();
        }
        public static List<Deca_RT_Type> GetListAssignee(string typeid)
        {
            string PROCEDURE = "p_SelectDeca_RT_Type_GetListAssignee";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@TypeID";
            parameters[i].Value = typeid;
            i++;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new Deca_RT_Type
            {
                Name = row["UserName"].ToString(),

            }).ToList();
        }

        public static List<Deca_RT_Type> GetListUserInType(string TypeID)
        {
            string PROCEDURE = "p_SelectDeca_RT_Type_User";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@TypeID";
            parameters[0].Value = TypeID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new Deca_RT_Type
            {
                UserName = row["UserName"].ToString(),

            }).ToList();
        }
        public static List<DropListDown> GetListTypeForTrackingOrder(string CategoryID)
        {
            string PROCEDURE = "p_SelectDeca_RT_TypesAll_Category";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@categoryid";
            parameters[i].Value = CategoryID;
            i++;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);

            return dt.AsEnumerable().Where(s=>s["Status"].ToString() == "True").Select(row => new DropListDown
            {
                Text = row["Name"].ToString(),
                Value = row["TypeID"].ToString(),
            }).ToList();
        }
        public int InsertQueue(string QueueID)
        {
            string PROCEDURE = "insert into Deca_RT_Type_Queue(TypeID, QueueID) values(@TypeID, @QueueID)";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@TypeID";
            parameters[i].Value = this.TypeID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@QueueID";
            parameters[i].Value = QueueID;
            i++;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, parameters);

        }
        public int DeleteQueue()
        {
            string PROCEDURE = "delete Deca_RT_Type_Queue where @TypeID= TypeID";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@TypeID";
            parameters[i].Value = this.TypeID;
            i++;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, parameters);

        }

    }
}
