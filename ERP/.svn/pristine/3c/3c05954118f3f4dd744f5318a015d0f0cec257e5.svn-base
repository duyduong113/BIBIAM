using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System.Data;
using System.ComponentModel;
using ERPAPD.Helpers;
using System.Globalization;
using System.Data.SqlClient;

namespace ERPAPD.Models
{
    public class CRM_PaymentProgress
    {
        [AutoIncrement]
        public long PKPayment { get; set; }
        public long FKContract { get; set; }
        public string Number { get; set; }
        public DateTime? PaymentDate { get; set; }
        public float Money { get; set; }
        public string PaymentForm { get; set; }
        public string BankCode { get; set; }
        public string NumberReceipt { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public string BillCode { get; set; }

        public DateTime RowCreatedAt { get; set; }
        [DefaultValue("1900-01-01")]
        public DateTime? RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }

        [Ignore]
        public string PaymentDateString { get; set; }
        [Ignore]
        public string CustomerCode { get; set; }
        [Ignore]
        public string CustomerName { get; set; }
        [Ignore]
        public string BankName { get; set; }
        [Ignore]
        public string ContractCode { get; set; }
        public static object Save(IEnumerable<CRM_PaymentProgress> listItem, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (listItem != null)
                {
                    try
                    {
                        foreach (var request in listItem)
                        {
                            var checkExits = dbConn.SingleOrDefault<CRM_PaymentProgress>("PKPayment= {0}", request.PKPayment);
                            if (checkExits == null)
                            {
                                if (request.FKContract != 0)
                                {
                                    var row = new CRM_PaymentProgress();
                                    row.FKContract = (request.FKContract != 0) ? request.FKContract : 0;
                                    row.Number = (request.Number != "") ? request.Number : "0";
                                    DateTime paymentDate = DateTime.Now;
                                    if (DateTime.TryParseExact(request.PaymentDateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out paymentDate))
                                    {
                                        row.PaymentDate = !string.IsNullOrEmpty(paymentDate.ToString()) ? paymentDate : DateTime.Parse("1900-01-01");
                                    }

                                    row.Money = (request.Money != 0) ? request.Money : 0;
                                    row.PaymentForm = !string.IsNullOrEmpty(request.PaymentForm) ? request.PaymentForm.Trim() : "";
                                    row.BankCode = !string.IsNullOrEmpty(request.BankCode) ? request.BankCode.Trim() : "";
                                    row.NumberReceipt = !string.IsNullOrEmpty(request.NumberReceipt) ? request.NumberReceipt.Trim() : "";

                                    row.RowCreatedUser = username;
                                    row.RowUpdatedUser = "";
                                    row.RowCreatedAt = DateTime.Now;
                                    row.RowUpdatedAt = DateTime.Parse("1900-01-01");
                                    dbConn.Insert(row);
                                }
                                else
                                {
                                    return new { success = false, message = "Data is null!" };
                                }
                            }
                            else {
                                checkExits.FKContract = (request.FKContract != 0) ? request.FKContract : 0;
                                checkExits.Number = (request.Number != "") ? request.Number : "0";
                               
                                DateTime paymentDate = DateTime.Now;
                                if (DateTime.TryParseExact(request.PaymentDateString.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out paymentDate))
                                {
                                    checkExits.PaymentDate = !string.IsNullOrEmpty(paymentDate.ToString()) ? paymentDate : DateTime.Parse("1900-01-01");
                                }
                                checkExits.Money = (request.Money != 0) ? request.Money : 0;
                                checkExits.PaymentForm = !string.IsNullOrEmpty(request.PaymentForm) ? request.PaymentForm.Trim() : "";
                                checkExits.BankCode = !string.IsNullOrEmpty(request.BankCode) ? request.BankCode.Trim() : "";
                                checkExits.NumberReceipt = !string.IsNullOrEmpty(request.NumberReceipt) ? request.NumberReceipt.Trim() : "";
                                checkExits.RowUpdatedUser = username;
                                checkExits.RowUpdatedAt = DateTime.Now;
                                dbConn.Update(checkExits);
                            }
                        }
                        return new { success = true, message = "Save success!" };

                    }
                    catch (Exception e)
                    {
                        return new { success = false, message = e.Message };
                    }
                }
                else
                {
                    return new { success = false, message = "Data is null!" };
                }

            }

        }
        public static DataTable ReadDataByCustomerCode(string CustomerCode)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@CustomerCode", CustomerCode));
            return new SqlHelper().ExecuteString("select pay.*, c.c_code as ContractCode from CRM_PaymentProgress pay inner join CRM_Contract c on pay.FKContract = c.pk_contract where c.c_customer_code=@CustomerCode order by RowCreatedAt desc", param);
        }
    }
}