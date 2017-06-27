using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a DC_Tracking_Order.
    /// </summary>
    public class DC_Tracking_Order
    {
        #region Members
        public string ID { get; set; }
        public string ColorDay { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public string ReasonName { get; set; }
        public string Note { get; set; }
        public int RowID { get; set; }
        public DateTime RowCreatedTime { get; set; }
        public string RowCreatedUser { get; set; }
        public DateTime RowLastUpdatedTime { get; set; }
        public string RowLastUpdatedUser { get; set; }

        public string OrderID  { get; set; }
        public string MSIN  { get; set; }
        public string LastActivities { get; set; }
        public string LastActivitiesStatus { get; set; }
        public double Fee  { get; set; }
        public string ItemName  { get; set; }
        public double Amount  { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime UpdateDate { get; set; } 
        public string OrderStatus { get; set; }
        public string OrderStatusColor { get; set; }
        public string Customer { get; set; }
        public string OrganizationID { get; set; }
        public string Email { get; set; }
        public string Duration { get; set; }
        public double DueBalance { get; set; }
        public double RunningBalance { get; set; }
        public double SettledBalance { get; set; }
        public string Group { get; set; }
        public string Department { get; set; }

        public string WarrantyStatus { get; set; }
        public string AssignTo { get; set; }
        public string Assignee { get; set; }
        public string ReasonDenied { get; set; }
        public string NoteDenied { get; set; }
        public string WareHouseID { get; set; }
        public string WareHouseLocationID { get; set; }

        public string BrandName { get; set; }
        public string Category { get; set; }
        public string SubCat { get; set; }
        #endregion

        public DC_Tracking_Order()
        { }



        public int Save()
        {
            string PROCEDURE = "p_SaveDC_Tracking_Order";
            SqlParameter[] parameters = new SqlParameter[8];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ID";
            parameters[i].Value = this.ID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this.Status;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Reason";
            parameters[i].Value = this.Reason;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Note";
            parameters[i].Value = this.Note;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WareHouseID";
            parameters[i].Value = this.WareHouseID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WareHouseLocationID";
            parameters[i].Value = this.WareHouseLocationID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedTime";
            parameters[i].Value = this.RowCreatedTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedUser";
            parameters[i].Value = this.RowCreatedUser;
            i++;

            foreach (var x in parameters)
            {
                if (x.Value == null)
                {
                    x.Value = "";
                }
            }
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateDC_Tracking_Order";
            SqlParameter[] parameters = new SqlParameter[10];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ID";
            parameters[i].Value = this.ID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this.Status;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Reason";
            parameters[i].Value = this.Reason;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Note";
            parameters[i].Value = this.Note;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WarrantyStatus";
            parameters[i].Value = this.WarrantyStatus;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WareHouseID";
            parameters[i].Value = this.WareHouseID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WareHouseLocationID";
            parameters[i].Value = this.WareHouseLocationID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowID";
            parameters[i].Value = this.RowID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedTime";
            parameters[i].Value = this.RowLastUpdatedTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedUser";
            parameters[i].Value = this.RowLastUpdatedUser;
            i++;
            foreach (var x in parameters)
            {
                if (x.Value == null)
                {
                    x.Value = "";
                }
            }
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }


        public int Cancelled()
        {
            string PROCEDURE = "p_UpdateDC_Tracking_Order_Cancelled";
            SqlParameter[] parameters = new SqlParameter[5];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ID";
            parameters[i].Value = this.ID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = "Cancelled";
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowID";
            parameters[i].Value = this.RowID;
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
            string PROCEDURE = "p_DeleteDC_Tracking_Order";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ID";
            parameters[0].Value = ID;

            parameters[1] = new SqlParameter();
            parameters[1].ParameterName = "@RowID";
            parameters[1].Value = RowID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);

        }
        public int DeleteNotify()
        {
            string PROCEDURE = "delete dbo.DC_Tracking_Order_Notify_log where [TicketID] = '" + ID.Replace("'", "''") + "'";

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);

        }
        public static DC_Tracking_Order GetDC_Tracking_Order(string ID)
        {
            string PROCEDURE = "p_SelectDC_Tracking_Order";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ID";
            parameters[0].Value = ID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Tracking_Order
            {
                ID = row["ID"].ToString(),
                Status = row["Status"].ToString(),
                Reason = row["Reason"].ToString(),
                Note = row["Note"].ToString(),
                WarrantyStatus = row["WarrantyStatus"].ToString(),
                WareHouseID = row["WareHouseID"].ToString(),
                WareHouseLocationID = row["WareHouseLocationID"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                AssignTo = row["AssignTo"].ToString(),
                Assignee = row["Assignee"].ToString()
            }).ToList().FirstOrDefault();
        }

        //public static List<DC_Tracking_Order> GetDC_Tracking_Orders(string whereCondition, string orderByExpression)
        //{
        //    string PROCEDURE = "p_SelectDC_Tracking_OrdersDynamic";
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
        //    return dt.AsEnumerable().Select(row => new DC_Tracking_Order
        //    {
        //        ID = row["ID"].ToString(),
        //        Status = row["Status"].ToString(),
        //        Reason = row["Reason"].ToString(),
        //        Note = row["Note"].ToString(),
        //        RowID = Convert.ToInt32(row["RowID"].ToString()),
        //        RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
        //        RowCreatedUser = row["RowCreatedUser"].ToString(),
        //        RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
        //        RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
        //    }).ToList();
        //}

        public static List<DC_Tracking_Order> GetAllDC_Tracking_Orders(string where)
        {
            string PROCEDURE = "p_SelectDC_Tracking_OrdersAll";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@where";
            parameters[0].Value = where;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Tracking_Order
            {
                ID = row["ID"].ToString(),
                OrderID = row["OrderID"].ToString(),
                MSIN = row["MSIN"].ToString(),
                Status = row["Status"].ToString(),
                Reason = row["Reason"].ToString(),
                ReasonName = row["ReasonName"].ToString(),
                Note = row["Note"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                LastActivities = row["LastActivities"].ToString(),
                LastComment = row["LastComment"].ToString(),
                LastActivitiesStatus = row["LastActivitiesStatus"].ToString(),
                Fee = Double.Parse(row["Fee"].ToString()),
                ItemName = row["ItemName"].ToString(),
                Amount = Convert.ToDouble(row["Amount"].ToString()),
                OrderDate = Convert.ToDateTime(row["OrderDate"].ToString()),
                UpdateDate =Convert.ToDateTime( row["UpdateDate"].ToString()),
                OrderStatus = row["OrderStatus"].ToString(),
                OrderStatusColor = row["OrderStatusColor"].ToString(),
                Customer = row["Customer"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                //DueBalance = Convert.ToDouble( row["DueBalance"].ToString()),
                //RunningBalance =Convert.ToDouble(  row["RunningBalance"].ToString()), 
                //SettledBalance =Convert.ToDouble(  row["SettledBalance"].ToString()), 
                ColorDay = row["ColorDay"].ToString(),
                Duration = row["Duration"].ToString(),
                
                WarrantyStatus = row["WarrantyStatus"].ToString(),
                AssignTo = row["AssignTo"].ToString(),
                Assignee = row["Assignee"].ToString(),
                ReasonDenied = row["ReasonDenied"].ToString(),
                NoteDenied = row["NoteDenied"].ToString(),
                BD = row["BD"].ToString(),
                ShippingAddress = row["ShippingAddress"].ToString(),
                ShippingProvince = row["ShippingProvince"].ToString(),
                ShippingCity = row["ShippingCity"].ToString(),
                ShippingMobile = row["ShippingMobile"].ToString(),
                AssignedDate = DateTime.Parse(row["AssignedDate"].ToString()),
                Type = row["Type"].ToString(),
                Telco = row["NetworkName"].ToString(),
                EmailUser = row["EmailUser"].ToString(),
                MobilePhoneUser = row["MobilePhoneUser"].ToString(),
                DepartmentUser = row["DepartmentUser"].ToString(),
                Department = row["Department"].ToString(),
                DepartmentName = row["DepartmentName"].ToString(),
                BrandName = row["BrandName"].ToString(),
                Category = row["Category"].ToString(),
                SubCat = row["SubCat"].ToString(),
                Model = row["Model"].ToString(),
                Active = row["Active"].ToString()
            }).ToList();
        }


        public string Type { get; set; }
        public string BD { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingProvince { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingMobile { get; set; }
        public string Telco { get; set; }
        public static List<DC_Tracking_Order> GetAllDC_Tracking_Orders_History( string ID)
        {
            string PROCEDURE = "p_SelectDC_Tracking_Order_History";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ID";
            parameters[i].Value = ID;


            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Tracking_Order
            {
                ID = row["ID"].ToString(),
                OrderID = row["OrderID"].ToString(),
                MSIN = row["MSIN"].ToString(),
                Status = row["Status"].ToString(),
                Reason = row["Reason"].ToString(),
                ReasonName = row["ReasonName"].ToString(),
                Note = row["Note"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                LastActivities = row["LastActivities"].ToString(),
                LastActivitiesStatus = row["LastActivitiesStatus"].ToString(),
                Fee = Double.Parse(row["Fee"].ToString()),
                ItemName = row["ItemName"].ToString(),
                Amount = Convert.ToDouble(row["Amount"].ToString()),
                OrderDate = Convert.ToDateTime(row["OrderDate"].ToString()),
                UpdateDate = Convert.ToDateTime(row["UpdateDate"].ToString()),
                OrderStatus = row["OrderStatus"].ToString(),
                OrderStatusColor = row["OrderStatusColor"].ToString(),
                Customer = row["Customer"].ToString(),
                OrganizationID = row["OrganizationID"].ToString(),
                ColorDay = row["ColorDay"].ToString(),
                Email = row["Email"].ToString(),
                Duration = row["Duration"].ToString(),
                Department = row["Department"].ToString(),
            }).ToList();
        }


        public static List<DC_Tracking_Order> GetListGroup()
        {
            string PROCEDURE = "select distinct [Group] from dbo.IXOrderFulfillmentTable order by [Group]";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Tracking_Order
            {
                Group = row["Group"].ToString(),
            }).ToList();
        }

        public static List<DC_Tracking_Order> GetListStatus()
        {
            string PROCEDURE = "select distinct [Status] from dbo.IXOrderFulfillmentTable order by [Status]";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Tracking_Order
            {
                Status = row["Status"].ToString(),
            }).ToList();
        }
        public static int SendEmailWhenRemoveOrderToTracking()
        {
            string PROCEDURE = "p_TrackingOrderNotification_Removing";
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);

        }

        public static DC_Tracking_Order GetOrder(string id)
        {
            string PROCEDURE = "select top 1 ID, RowID from DC_Tracking_Order where ID = N'" + id + "' order by RowCreatedTime desc";
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Tracking_Order
            {
                ID = row["ID"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
            }).ToList().FirstOrDefault();

        }

        //DungNT
        public string MobilePhone { get; set; }
        public DateTime Deliverydate { get; set; }
        public string MailTo { get; set; }
        public string Description { get; set; }
        public static DC_Tracking_Order GetOrderSendMail(string id)
        {
            string PROCEDURE = "select track.ID,ix.ItemName,track.Note,track.[Status],track.RowCreatedTime,track.RowCreatedUser,ix.OrderID ";
            PROCEDURE += ",ix.Customer,ix.MobilePhone,ix.MSIN,ix.ShippingAddress,ix.Deliverydate,cm.Email,cm.[Description] ";
            PROCEDURE += " from DC_Tracking_Order track  ";
            PROCEDURE += " LEFT JOIN IXOrderFulfillmentTable ix ON ix.ID = track.ID  ";
            PROCEDURE += " LEFT JOIN DC_Tracking_Order_ReasonMail cm ON track.Reason = cm.CodeID";
            PROCEDURE += " Where track.ID = '" + id + "'";
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Tracking_Order
            {
                ID = row["ID"].ToString(),
                ItemName = row["ItemName"].ToString(),
                Note = row["Note"].ToString(),
                Status = row["Status"].ToString(),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                OrderID = row["OrderID"].ToString(),
                MailTo = row["Email"].ToString(),
                Customer = row["Customer"].ToString(),
                MobilePhone = row["MobilePhone"].ToString(),
                MSIN = row["MSIN"].ToString(),
                ShippingAddress = row["ShippingAddress"].ToString(),
                Deliverydate = Convert.ToDateTime(row["Deliverydate"].ToString()),
                Description = row["Description"].ToString()
            }).ToList().FirstOrDefault();

        }

        public static List<DC_Tracking_Order> GetBalance(string ID)
        {
            string PROCEDURE = "p_TrackingOrder_GetBalance";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ID";
            parameters[i].Value = ID;


            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Tracking_Order
            {
                SettledBalance = Convert.ToDouble(row["SettledBalance"].ToString()),
                RunningBalance = Convert.ToDouble(row["RunningBalance"].ToString()),
                DueBalance = Convert.ToDouble(row["DueBalance"].ToString()),
            }).ToList();

        }
        public static int SendEmailWhenDone(string listorderid)
        {
            string PROCEDURE = "p_TrackingOrderNotification_Done";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@listorderid";
            parameters[i].Value = listorderid;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);

        }

        // namnh 
        public string UserID { get; set; }
        public string UserName { get; set; }
        public static List<DC_Tracking_Order> GetAllListUser(string user)
        {
            string PROCEDURE = "select distinct UserID, UserName from [User] order by UserID asc";
            
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Tracking_Order
            {
                UserID = row["UserID"].ToString(),
                UserName = row["UserName"].ToString(),
            }).ToList();
        }

        public static List<DC_Tracking_Order> GetListUserInAFS(string user)
        {
            string PROCEDURE  = "select usg.UserID,us.UserName from UserInGroup usg ";
                   PROCEDURE += "left join [User] us on us.UserID = usg.UserID ";
                   PROCEDURE += "where GroupID = 18 ";
                   PROCEDURE += " order by usg.UserID asc ";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Tracking_Order
            {
                UserID = row["UserID"].ToString(),
                UserName = row["UserName"].ToString(),
            }).ToList();
        }


        public static List<DC_Tracking_Order> GetListProvider()
        {
            string PROCEDURE = "SELECT DISTINCT ProviderID from DW_DC_Order";
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Tracking_Order
            {
                ProviderID = row["ProviderID"].ToString()
            }).ToList();
        }

        
        public static DC_Tracking_Order getMyTask(string user)
        {
            string PROCEDURE = "p_GetRemind_Tracking_Order";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UserID";
            parameters[i].Value = user;
            i++;
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Tracking_Order
            {
                countMyTask = int.Parse(row["CountMyTask"].ToString())
            }).ToList().FirstOrDefault();
        }
        public string ProviderID { get; set; }
        public DateTime AssignedDate { get; set; }
        public int countMyTask { get; set; }

        public string EmailUser { get; set; }

        public string MobilePhoneUser { get; set; }

        public string DepartmentUser { get; set; }
        public string DepartmentName { get; set; }

        public string Model { get; set; }

        public string LastComment { get; set; }

        public string Active { get; set; }
    }
}
