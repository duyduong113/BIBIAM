using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;

namespace ERPAPD.Models
{
    public class DC_Salesman_CAAT
    {
        [AutoIncrement]
        public int Id { get; set; }
        [StringLength(256)]
        public string Organization { get; set; }
        [StringLength(256)]
        public string CustomerID { get; set; }
        [StringLength(20)]
        public string MobilePhone { get; set; }
        [StringLength(20)]
        public string Type { get; set; }
        [StringLength(256)]
        public string TxnID { get; set; }
        [StringLength(2000)]
        public string FieldSaleNote { get; set; }
        public bool IsValid { get; set; }
        [StringLength(20)]
        public string BDTeam { get; set; }
        public DateTime RowCreatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowCreatedUser { get; set; }
        public DateTime RowLastUpdatedTime { get; set; }
        [StringLengthAttribute(100)]
        public string RowLastUpdatedUser { get; set; }
        [StringLength(20)]
        public string Status { get; set; }
        [StringLength(1000)]
        public string NoteByCS { get; set; }
        [StringLength(32)]
        public string AccountID { get; set; }
        [StringLengthAttribute(256)]
        public string AccountName { get; set; }
        [StringLengthAttribute(256)]
        public string BankName { get; set; }
        [StringLengthAttribute(256)]
        public string BankBranch { get; set; }
        [StringLength(256)]
        public string Relationship { get; set; }

        [Ignore]
        public string Description { get; set; }
        [Ignore]
        public int Amount { get; set; }
        [Ignore]
        public DateTime TranDate { get; set; }
        [Ignore]
        public DateTime CompletedDate { get; set; }
        [Ignore]
        public string TxnType { get; set; }
        [Ignore]
        public string Createdate { get; set; }
        [Ignore]
        public string Issuer { get; set; }
        [Ignore]
        public string FullName { get; set; }
        [Ignore]
        public float CreditLimit { get; set; }
        public string ReceiverPhone { get; set; }
        [Ignore]
        public string StatusTxn { get; set; }
        [Ignore]
        public string StatusBG { get; set; }

        public static List<DC_Salesman_CAAT> p_DC_Salesman_CAATGetAll()
        {
            string PROCEDURE = "p_DC_Salesman_CAATGetAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Salesman_CAAT
            {
                Id = int.Parse(row["Id"].ToString()),
                CustomerID = row["CustomerID"].ToString(),
                Type = row["Type"].ToString(),
                FieldSaleNote = row["FieldSaleNote"].ToString(),
                IsValid = bool.Parse(row["IsValid"].ToString()),
                MobilePhone = row["MobilePhone"].ToString(),
                Organization = row["Organization"].ToString(),
                TxnType = row["TxnType"].ToString(),
                TxnID = row["TxnID"].ToString(),
                Amount = int.Parse(row["Amount"].ToString()),
                TranDate = DateTime.Parse(row["TranDate"].ToString()),
                CompletedDate = DateTime.Parse(row["CompletedDate"].ToString()),
                Status = row["Status"].ToString(),
                StatusTxn = row["StatusTxn"].ToString(),
                Description = row["Description"].ToString(),
                RowCreatedTime = DateTime.Parse(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = DateTime.Parse(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                Issuer = row["Issuer"].ToString(),
                AccountID = row["AccountID"].ToString(),
                AccountName = row["AccountName"].ToString(),
                BankName = row["BankName"].ToString(),
                BankBranch = row["BankBranch"].ToString(),
                Relationship = row["Relationship"].ToString(),
                NoteByCS = row["NoteByCS"].ToString(),
                FullName = row["FullName"].ToString(),
                CreditLimit = float.Parse(row["CreditLimit"].ToString()),
                ReceiverPhone = row["ReceiverPhone"].ToString(),
                StatusBG = row["StatusBG"].ToString()
            }).ToList();
        }

        public static List<DC_Salesman_CAAT> p_DC_Salesman_CAATGetAllAT()
        {
            string PROCEDURE = "p_DC_Salesman_CAATGetAllAT";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Salesman_CAAT
            {
                Id = int.Parse(row["Id"].ToString()),
                CustomerID = row["CustomerID"].ToString(),
                Type = row["Type"].ToString(),
                FieldSaleNote = row["FieldSaleNote"].ToString(),
                IsValid = bool.Parse(row["IsValid"].ToString()),
                MobilePhone = row["MobilePhone"].ToString(),
                Organization = row["Organization"].ToString(),
                TxnType = row["TxnType"].ToString(),
                TxnID = row["TxnID"].ToString(),
                Amount = int.Parse(row["Amount"].ToString()),
                TranDate = DateTime.Parse(row["TranDate"].ToString()),
                CompletedDate = DateTime.Parse(row["CompletedDate"].ToString()),
                StatusTxn = row["StatusTxn"].ToString(),
                Description = row["Description"].ToString(),
                RowCreatedTime = DateTime.Parse(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = DateTime.Parse(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
            }).ToList();
        }
    }
}