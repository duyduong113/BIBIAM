using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ERPAPD.Models
{
    public class CRM_Contract_Test
    {
        [AutoIncrement]
        public long pk_test { get; set; }
        public long fk_contract { get; set; }

        public DateTime D { get; set; }
        public double T1 { get; set; }
        public string N1 { get; set; }

        public double T2 { get; set; }
        public string N2 { get; set; }

        public double T3 { get; set; }
        public string N3 { get; set; }

        public DateTime? RowCreatedAt { get; set; }
        public DateTime? RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }

        public static object Save(IEnumerable<CRM_Contract_Test> list, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                foreach (var item in list)
                {
                    if (item != null)
                    {
                        try
                        {

                            var checkExits = dbConn.SingleOrDefault<CRM_Contract_Test>("pk_test= {0}", item.pk_test);
                            if (checkExits == null)
                            {
                                var row = new CRM_Contract_Test();
                                row.fk_contract = (item.fk_contract != 0) ? item.fk_contract : 0;
                                row.D = !string.IsNullOrEmpty(item.D.ToString()) ? item.D : DateTime.Parse("1900-01-01");
                                row.T1 = (item.T1 != 0) ? item.T1 : 0;
                                row.T2 = (item.T2 != 0) ? item.T2 : 0;
                                row.T3 = (item.T3 != 0) ? item.T3 : 0;
                                row.N1 = !string.IsNullOrEmpty(item.N1) ? item.N1.Trim() : "";
                                row.N2 = !string.IsNullOrEmpty(item.N2) ? item.N2.Trim() : "";
                                row.N3 = !string.IsNullOrEmpty(item.N3) ? item.N3.Trim() : "";
                                row.RowCreatedUser = username;
                                row.RowUpdatedUser = "";
                                row.RowCreatedAt = DateTime.Now;
                                row.RowUpdatedAt = DateTime.Parse("1900-01-01");

                                dbConn.Insert(row);
                                long id = dbConn.GetLastInsertId();
                                return new { success = true, message = "Save success!", PK = id };
                            }
                            else
                            {
                                checkExits.fk_contract = (item.fk_contract != 0) ? item.fk_contract : 0;
                                checkExits.D = !string.IsNullOrEmpty(item.D.ToString()) ? item.D : DateTime.Parse("1900-01-01");
                                checkExits.T1 = (item.T1 != 0) ? item.T1 : 0;
                                checkExits.T2 = (item.T2 != 0) ? item.T2 : 0;
                                checkExits.T3 = (item.T3 != 0) ? item.T3 : 0;

                                checkExits.N1 = !string.IsNullOrEmpty(item.N1) ? item.N1.Trim() : "";
                                checkExits.N2 = !string.IsNullOrEmpty(item.N2) ? item.N2.Trim() : "";
                                checkExits.N3 = !string.IsNullOrEmpty(item.N3) ? item.N3.Trim() : "";
                                checkExits.RowUpdatedUser = username;
                                checkExits.RowUpdatedAt = DateTime.Now;
                                dbConn.Update(checkExits);
                                return new { success = true, message = "Save success!" };
                            }
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

                return new { success = false, message ="" };

            }


        }
    }
}