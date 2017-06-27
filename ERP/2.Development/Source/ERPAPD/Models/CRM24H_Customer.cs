using ServiceStack.DataAnnotations;
using System;
using System.Linq;
using System.Data.SqlClient;
using ServiceStack.OrmLite;
using System.Data;

namespace ERPAPD.Models
{
    public class ERPAPD_Customer
    {
        [AutoIncrement]
        public int RowID { get; set; }
        public string CustomerID { get; set; }
        public long ReferID { get; set; }
        public int AgencyRule { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string EnglishName { get; set; }
        public string ShortName { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string BranchAddress { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Wards { get; set; }
        public string Category { get; set; }
        public string CategoryParent { get; set; }
        public string CompanyType { get; set; }
        public string Account { get; set; }
        public string BankCode { get; set; }
        public string CodeLink { get; set; }
        public string BankBranch { get; set; }
        public string TaxCode { get; set; }
        public string Sponsor { get; set; }
        public string Position { get; set; }
        public int StaffId { get; set; }
        public int UnitId { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public string FileName { get; set; }
        public string CustomerType { get; set; }
        public DateTime? RowCreatedAt { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        public string Source { get; set; }
        public string Area { get; set; }
        public string AgencyType { get; set; }
        public string BankName { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ApprovedContent { get; set; }
        public string DeniedContent { get; set; }
        public string Label { get; set; }
      
        
        [Ignore]
        public string StatusName { get; set; }
        [Ignore]
        public string TypeName { get; set; }
        [Ignore]
        public string SourceName { get; set; }
        [Ignore]
        public int FKAgency { get; set; }
        [Ignore]
        public string PriorityType { get; set; }
        [Ignore]
        public string EmployeeName { get; set; }
        [Ignore]
        public string TeamName { get; set; }
        [Ignore]
        public string RegionName { get; set; }
        [Ignore]
        public string Name { get; set; }
        [Ignore]
        public string Title { get; set; }

    }
    public class ERPAPD_Customer_CStatus
    {
        [AutoIncrement]
        public string TAT_CA { get; set; }
        public string CHUA_DUYET { get; set; }
        public string TU_CHOI { get; set; }
        public string DA_CO_HD { get; set; }
        public string DL_CHUA_HD { get; set; }
        public string DA_DUYET_CHUA_HD { get; set; }
        public static ERPAPD_Customer_CStatus CountStatus(string UserID,string TypeName)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            int y = 0;
            parameters[y] = new SqlParameter();
            parameters[y].ParameterName = "@UserName";
            parameters[y].Value = UserID;
            y++;
            parameters[y] = new SqlParameter();
            parameters[y].ParameterName = "@TypeName";
            parameters[y].Value = TypeName;

            System.Data.DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, "p_select_count_customer_by_status", parameters);
            var lst = new ERPAPD_Customer_CStatus[dt.Rows.Count];
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                var item = new ERPAPD_Customer_CStatus();
                item.CHUA_DUYET = int.Parse(row["CHUA_DUYET"].ToString()).ToString("#,##0");
                item.TU_CHOI = int.Parse(row["TU_CHOI"].ToString()).ToString("#,##0");
                item.DA_CO_HD = int.Parse(row["DA_CO_HD"].ToString()).ToString("#,##0");
                item.DL_CHUA_HD = int.Parse(row["DL_CHUA_HD"].ToString()).ToString("#,##0");
                item.DA_DUYET_CHUA_HD = int.Parse(row["DA_DUYET_CHUA_HD"].ToString()).ToString("#,##0");
                item.TAT_CA = int.Parse(row["TAT_CA"].ToString()).ToString("#,##0"); ;//(int.Parse(row["CHUA_DUYET"].ToString()) + int.Parse(row["TU_CHOI"].ToString()) + int.Parse(row["DA_CO_HD"].ToString())).ToString("#,##0");
                lst[i] = item;
                i++;
            }
            return lst.FirstOrDefault();
        }
      
    }
    public class ERPAPD_Customer_CS
    {
        [AutoIncrement]
        public string TAT_CA { get; set; }
        public string DAI_LY { get; set; }
        public string DS_CAO { get; set; }
        public string SAP_HET_HD { get; set; }
        public string CO_HD_NAM_TRUOC { get; set; }
        public string CHUA_KHAI_THAC { get; set; }
        public static ERPAPD_Customer_CS CountContract(string UserID)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            int y = 0;
            parameters[y] = new SqlParameter();
            parameters[y].ParameterName = "@UserName";
            parameters[y].Value = UserID;

            System.Data.DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, System.Data.CommandType.StoredProcedure, "p_select_count_customer_by_emloyee", parameters);
            var lst = new ERPAPD_Customer_CS[dt.Rows.Count];
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                var item = new ERPAPD_Customer_CS();
                item.DAI_LY = int.Parse(row["DAI_LY"].ToString()).ToString("#,##0");
                item.DS_CAO = int.Parse(row["DS_CAO"].ToString()).ToString("#,##0");
                item.SAP_HET_HD = int.Parse(row["SAP_HET_HD"].ToString()).ToString("#,##0");
                item.CO_HD_NAM_TRUOC = int.Parse(row["CO_HD_NAM_TRUOC"].ToString()).ToString("#,##0");
                item.CHUA_KHAI_THAC = int.Parse(row["CHUA_KHAI_THAC"].ToString()).ToString("#,##0");
                item.TAT_CA = int.Parse(row["TAT_CA"].ToString()).ToString("#,##0");
                lst[i] = item;
                i++;
            }
            return lst.FirstOrDefault();
        }

    }

}