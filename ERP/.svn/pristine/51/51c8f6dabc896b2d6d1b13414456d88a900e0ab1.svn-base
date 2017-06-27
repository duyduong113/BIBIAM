using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a DC_Tracking_Order_ReasonMail.
    /// </summary>
    public class DC_Tracking_Order_ReasonMail
    {
        #region Members
        public string CodeID { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }
        public string EmailToOPS { get; set; }
        public int Department { get; set; }
        public int KPIWarning { get; set; }
        public int KPIRemind { get; set; }
        public bool PrintInvoice { get; set; }
        public bool ReceiveCheck { get; set; }
        public string CategoryID { get; set; }
        public string TypeID { get; set; }
        public string Status { get; set; }

        public int RowID { get; set; }

        public DateTime RowCreatedTime { get; set; }

        public string RowCreatedUser { get; set; }

        public DateTime RowLastUpdatedTime { get; set; }

        public string RowLastUpdatedUser { get; set; }

        #endregion

        public DC_Tracking_Order_ReasonMail()
        { }

      

        public int Save()
        {
            string PROCEDURE = "p_SaveDC_Tracking_Order_ReasonMail";
            SqlParameter[] parameters = new SqlParameter[13];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Description";
            parameters[i].Value = this.Description;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Email";
            parameters[i].Value = this.Email;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@EmailToOPS";
            parameters[i].Value = this.EmailToOPS;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Department";
            parameters[i].Value = this.Department;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@KPIWarning";
            parameters[i].Value = this.KPIWarning;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@KPIRemind";
            parameters[i].Value = this.KPIRemind;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@PrintInvoice";
            parameters[i].Value = this.PrintInvoice;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ReceiveCheck";
            parameters[i].Value = this.ReceiveCheck;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CategoryID";
            parameters[i].Value = this.CategoryID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@TypeID";
            parameters[i].Value = this.TypeID;
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
            string PROCEDURE = "p_UpdateDC_Tracking_Order_ReasonMail";
            SqlParameter[] parameters = new SqlParameter[14];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CodeID";
            parameters[i].Value = this.CodeID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Description";
            parameters[i].Value = this.Description;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Email";
            parameters[i].Value = this.Email;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@EmailToOPS";
            parameters[i].Value = this.EmailToOPS;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Department";
            parameters[i].Value = this.Department;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@KPIWarning";
            parameters[i].Value = this.KPIWarning;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@KPIRemind";
            parameters[i].Value = this.KPIRemind;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@PrintInvoice";
            parameters[i].Value = this.PrintInvoice;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ReceiveCheck";
            parameters[i].Value = this.ReceiveCheck;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CategoryID";
            parameters[i].Value = this.CategoryID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@TypeID";
            parameters[i].Value = this.TypeID;
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
            string PROCEDURE = "p_DeleteDC_Tracking_Order_ReasonMail";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@CodeID";
            parameters[0].Value = CodeID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);

        }

        public static DC_Tracking_Order_ReasonMail GetDC_Tracking_Order_ReasonMail(string CodeID)
        {
            string PROCEDURE = "p_SelectDC_Tracking_Order_ReasonMail";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@CodeID";
            parameters[0].Value = CodeID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Tracking_Order_ReasonMail
            {
                CodeID = row["CodeID"].ToString(),
                Description = row["Description"].ToString(),
                Email = row["Email"].ToString(),
                EmailToOPS = row["EmailToOPS"].ToString(),
                Department = Convert.ToInt32(row["Department"].ToString()),
                KPIWarning = Convert.ToInt32(row["KPIWarning"].ToString()),
                KPIRemind = Convert.ToInt32(row["KPIRemind"].ToString()),
                PrintInvoice = Convert.ToBoolean(row["PrintInvoice"].ToString()),
                ReceiveCheck = Convert.ToBoolean(row["ReceiveCheck"].ToString()),
                CategoryID = row["CategoryID"].ToString(),
                TypeID = row["TypeID"].ToString(),
                Status = row["Status"].ToString(),
                RowID = Convert.ToInt16(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }


        public static List<DC_Tracking_Order_ReasonMail> GetAllDC_Tracking_Order_ReasonMails()
        {
            string PROCEDURE = "p_SelectDC_Tracking_Order_ReasonMailsAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Tracking_Order_ReasonMail
            {
                CodeID = row["CodeID"].ToString(),
                Description = row["Description"].ToString(),
                Email = row["Email"].ToString(),
                EmailToOPS = row["EmailToOPS"].ToString(),
                Department = Convert.ToInt32(row["Department"].ToString()),
                KPIWarning = Convert.ToInt32(row["KPIWarning"].ToString()),
                KPIRemind = Convert.ToInt32(row["KPIRemind"].ToString()),
                PrintInvoice = Convert.ToBoolean(row["PrintInvoice"].ToString()),
                ReceiveCheck = Convert.ToBoolean(row["ReceiveCheck"].ToString()),
                CategoryID = row["CategoryID"].ToString(),
                TypeID = row["TypeID"].ToString(),
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
