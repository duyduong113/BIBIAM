using System;
using System.Data;
using System.Linq;
using System.Web;
using ERPAPD.Helpers;
using ServiceStack.OrmLite;
using ServiceStack.DataAnnotations;

namespace ERPAPD.Models
{
    public class Portal_Message
    {
        public string SendTo { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public int IsRead { get; set; }
        [Ignore]
        public int RowID { get; set; }
        public DateTime RowCreatedAt { get; set; }
        public string RowCreatedBy { get; set; }
        public DateTime RowLastUpdatedAt { get; set; }

        public static int SendMessage(string SendTo, string Type, string Description, string Link)
        {
            var mess = new Portal_Message();
            using (IDbConnection dbConn =  OrmliteConnection.openConn())
            {
                mess.SendTo = SendTo;
                mess.Type = Type;
                mess.Description = Description;
                mess.Link = Link;
                mess.IsRead = 0;
                mess.RowCreatedAt = DateTime.Now;
                mess.RowCreatedBy = HttpContext.Current.User.Identity.Name;
                mess.RowLastUpdatedAt = DateTime.Now;
                dbConn.Insert(mess);
            }

            ChatHub.SendGroup(SendTo, Description, Type);
            ChatHub.SendMessageToAll(SendTo, Description);
            return 1;
        }
        public static int CountMessage(string UserID)
        {
            //List<SqlParameter> param = new List<SqlParameter>();
            //param.Add(new SqlParameter("@UserID", UserID));
            //DataTable dt = new SqlHelper().ExecuteQuery("p_Portal_Message_Count", param);
            //if (dt.Rows.Count > 0)
            //{
            //    return int.Parse(dt.Rows[0][0].ToString());
            //}
            //return 0;
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var rs = dbConn.SqlList<int>("p_Portal_Message_Count @UserID", new  {  UserID = UserID }).First();
                return rs;
            }
               
        }
        public static int UpdateIsRead(string UserID)
        {
            //List<SqlParameter> param = new List<SqlParameter>();
            //param.Add(new SqlParameter("@UserID", UserID));
            //return new SqlHelper().ExecuteNoneQuery("p_Portal_Message_UpdateIsRead", param);

            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var rs = dbConn.SqlList<int>("p_Portal_Message_UpdateIsRead @UserID",
                                       new
                                       {
                                           UserID = UserID

                                       }).First();
                return rs;
            }
        }
        public static int UpdateIsClick(string UserID, int id)
        {
            //List<SqlParameter> param = new List<SqlParameter>();
            //param.Add(new SqlParameter("@UserID", UserID));
            //param.Add(new SqlParameter("@RowID", id));
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
               var rs = dbConn.SqlList<int>("p_Portal_Message_UpdateIsClick @UserID,@RowID",
                new
                {
                    UserID = UserID,
                    RowID = id,
                }).First();
                return rs;
            }

        }
        public static DataTable GetData(string UserID, string Type)
        {
            //List<SqlParameter> param = new List<SqlParameter>();
            //param.Add(new SqlParameter("@UserID", UserID));
            //param.Add(new SqlParameter("@Type", Type));
            //DataTable dt = new SqlHelper().ExecuteQuery("p_Portal_Message_GetData", param);
            //return dt;

            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                var rs = dbConn.SqlList<DataTable>("p_Portal_Message_GetData @UserID,@Type",
                 new
                 {
                     UserID = UserID,
                     Type = Type,
                 }).First();
                return rs;
            }
        }
       
    }
}