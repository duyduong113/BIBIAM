using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ServiceStack.OrmLite;
using Dapper;
using System.Data;

namespace ERPAPD.Models
{
    public class Deca_Bank_Transactions
    {
        [AutoIncrement]
        [PrimaryKey]
        public int ID { get; set; }
        [StringLengthAttribute(50)]
        public string OrderID { get; set; }

        public DateTime AuthTime { get; set; }
        [StringLengthAttribute(50)]
        public string BillTransRef { get; set; }
        [StringLengthAttribute(50)]
        public string AuthTransRef { get; set; }

        [StringLengthAttribute(50)]
        public string BankActionStatus { get; set; }
        public int BankInstallment { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(32)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(32)]
        public string UpdatedBy { get; set; }

    }
    public class Deca_Bank_TransactionsViewModel
    {

        public int ID { get; set; }
        public string OrderID { get; set; }
        public DateTime AuthTime { get; set; }
        public string BillTransRef { get; set; }
        public string AuthTransRef { get; set; }
        public string TransactionInfo { get; set; }
        public string BankActionStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(32)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(32)]
        public string UpdatedBy { get; set; }
        public double Amount { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string PhysicalID { get; set; }
        public string MobilePhone { get; set; }
        public int Installment { get; set; }
        public int BankInstallment { get; set; }
        public string Bank { get; set; }
        public string RefID { get; set; }
        public string Status { get; set; }
        public static List<Deca_Bank_TransactionsViewModel> getRead(string where)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new List<Deca_Bank_TransactionsViewModel>();
                if (!string.IsNullOrEmpty(where))
                {
                    data = dbConn.Query<Deca_Bank_TransactionsViewModel>("[p_Select_ReadBankTransaction]", commandType: CommandType.StoredProcedure).ToList();
                }
                else
                {
                    data = dbConn.Query<Deca_Bank_TransactionsViewModel>("[p_Select_ReadBankTransaction]", commandType: CommandType.StoredProcedure).ToList();
                }
                return data;
            }
        }
    }

}