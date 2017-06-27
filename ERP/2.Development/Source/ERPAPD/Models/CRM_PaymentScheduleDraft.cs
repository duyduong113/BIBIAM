using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System.Data;
using System.ComponentModel;

namespace ERPAPD.Controllers
{
    public class CRM_PaymentScheduleDraft
    {
        [AutoIncrement]
        public int PKPayment { get; set; }
        public int FKContract { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
        [DefaultValue("1900-01-01")]
        public double Money { get; set; }
        public string Note { get; set; }
        public string IsNew { get; set; }

        public DateTime RowCreatedAt { get; set; }
        [DefaultValue("1900-01-01")]
        public DateTime? RowUpdatedAt { get; set; }
        [DefaultValue("1900-01-01")]
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }
        [Ignore]
        public string DateString { get; set; }

        public static object Save(IEnumerable<CRM_PaymentScheduleDraft> listItem, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (listItem != null)
                {
                    try
                    {
                        foreach (var request in listItem)
                        {
                            var checkExits = dbConn.SingleOrDefault<CRM_PaymentScheduleDraft>("PKPayment= {0}", request.PKPayment);
                            if (checkExits == null)
                            {
                                if (request.FKContract != 0)
                                {
                                    var row = new CRM_PaymentScheduleDraft();
                                    row.FKContract = (request.FKContract != 0) ? request.FKContract : 0;
                                    row.Number = (request.Number != 0) ? request.Number : 0;
                                    row.Date = !string.IsNullOrEmpty(request.DateString.ToString()) ? DateTime.Parse(request.DateString) : DateTime.Parse("1900-01-01");
                                    row.Money = (request.Money != 0) ? request.Money : 0;
                                    row.Note = !string.IsNullOrEmpty(request.Note) ? request.Note.Trim() : "";
                                    row.IsNew = "1";

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
                                checkExits.Number = (request.Number != 0) ? request.Number : 0;
                                checkExits.Date = !string.IsNullOrEmpty(request.DateString.ToString()) ? DateTime.Parse(request.DateString) : checkExits.Date;
                                checkExits.Money = (request.Money != 0) ? request.Money : 0;
                                checkExits.Note = !string.IsNullOrEmpty(request.Note) ? request.Note.Trim() : "";
                                checkExits.IsNew = "1";

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
    }
}