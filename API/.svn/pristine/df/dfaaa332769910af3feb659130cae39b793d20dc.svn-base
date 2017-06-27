using System;
using System.Collections.Generic;
using ERP_API.Providers;
using System.Data.SqlClient;
using System.Data;

namespace ERP_API.Models
{
    public class User
    {
        public string UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string EmployeeID { get; set; } //Ma nhan vien
        public string EmployeeName { get; set; } //Ten nhan vien
        public string EmployeeType { get; set; } //Loai nhan vien lai xe hoac dieu xe
        public DateTime RowUpdatedAt { get; set; }
        public string RowUpdatedBy { get; set; }

        public bool Login(string userid, string password)
        {
            password = SqlHelper.GetMd5Hash(password); ;
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@UserID", userid));
            param.Add(new SqlParameter("@Password", password));
            DataTable dt = new SqlHelper().ExecuteQuery("p_Login", param);
            if (dt.Rows.Count > 0)
                return true;
            else return false;
        }

        public User GetUserByID(string userid)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@UserID", userid));
            DataTable dt = new SqlHelper().ExecuteQuery("p_API_Get_User_ByUserID", param);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                var item = new User();
                item.UserID = row["UserID"].ToString();
                item.Password = row["Password"] != null ? row["Password"].ToString() : "";
                item.FullName = row["FullName"] != null ? row["FullName"].ToString() : "";
                item.Email = row["Email"] != null ? row["Email"].ToString() : "";
                item.Phone = row["Phone"] != null ? row["Phone"].ToString() : "";
                item.EmployeeID = row["EmployeeID"] != null ? row["EmployeeID"].ToString() : "";
                item.EmployeeName = row["EmployeeName"] != null ? row["EmployeeName"].ToString() : "";
                item.EmployeeType = row["EmployeeType"] != null ? row["EmployeeType"].ToString() : "";
                item.RowUpdatedAt = row["RowUpdatedAt"] != null ? DateTime.Parse(row["RowUpdatedAt"].ToString()) : DateTime.Now;
                item.RowUpdatedBy = row["RowUpdatedBy"] != null ? row["RowUpdatedBy"].ToString() : "";
                return item;
            }
            return null;
        }
        public int Update()
        {
            string password = SqlHelper.GetMd5Hash(this.Password);
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@UserID", this.UserID));
            param.Add(new SqlParameter("@Password", password));
            param.Add(new SqlParameter("@FullName", this.FullName));
            param.Add(new SqlParameter("@Email", this.Email));
            param.Add(new SqlParameter("@Phone", this.Phone));
            param.Add(new SqlParameter("@RowUpdatedAt", this.RowUpdatedAt));
            param.Add(new SqlParameter("@RowUpdatedBy", this.RowUpdatedBy));
            return new SqlHelper().ExecuteNoneQuery("p_API_Update_User_ByUserID", param);
        }
    }
}