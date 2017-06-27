using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace ERPAPD.Models
{
    public class CRM_Contract_Staff
    {
        [AutoIncrement]
        public Int32 pk_staff { get; set; }
        public Int32 fk_contract { get; set; }
        public int c_unit_id { get; set; }
        public int c_staff_id { get; set; }
        public int c_departments_id { get; set; }
        public int c_group_id { get; set; }
        public double c_percent { get; set; }
        public double c_money { get; set; }
        public double c_money_in_year { get; set; }
        public double c_money_web_other { get; set; }
        public string c_xml_data { get; set; }
        public string c_charge { get; set; }
        public double c_money_next_year { get; set; }
        public double c_money_next_year2 { get; set; }

        [Ignore]
        public string TeamName { get; set; }
        [Ignore]
        public string Province { get; set; }
        [Ignore]
        public string FullName { get; set; }
        [Ignore]
        public string RefStaffId { get; set; }

        public static object Save(IEnumerable<CRM_Contract_Staff> listItem, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (listItem != null)
                {
                    try
                    {
                        foreach (var request in listItem)
                        {
                            var checkExits = dbConn.SingleOrDefault<CRM_Contract_Staff>("pk_staff= {0}", request.pk_staff);
                            if (checkExits == null)
                            {
                                if (request.fk_contract != 0)
                                {
                                    var row = new CRM_Contract_Staff();
                                    row.fk_contract = (request.fk_contract != 0) ? request.fk_contract : 0;
                                    row.c_unit_id = (request.c_unit_id != 0) ? request.c_unit_id : 0;
                                    row.c_staff_id = (request.c_staff_id != 0) ? request.c_staff_id : 0;
                                    row.c_departments_id = (request.c_departments_id != 0) ? request.c_departments_id : 0;
                                    row.c_group_id = (request.c_group_id != 0) ? request.c_group_id : 0;
                                    row.c_percent = (request.c_percent != 0) ? request.c_percent : 0;
                                    row.c_money = (request.c_money != 0) ? request.c_money : 0;
                                    row.c_money_in_year = (request.c_money_in_year != 0) ? request.c_money_in_year : 0;
                                    row.c_money_web_other = (request.c_money_web_other != 0) ? request.c_money_web_other : 0;
                                    row.c_xml_data ="";
                                    row.c_charge = !string.IsNullOrEmpty(request.c_charge) ? request.c_charge : ""  ;
                                    row.c_money_next_year = (request.c_money_next_year != 0) ? request.c_money_next_year : 0;
                                    row.c_money_next_year2 = (request.c_money_next_year2 != 0) ? request.c_money_next_year2 : 0;
                                    dbConn.Insert(row);
                                }
                                else
                                {
                                    return new { success = false, message = "Data is null!" };
                                }
                            }
                            else {

                                checkExits.fk_contract = (request.fk_contract != 0) ? request.fk_contract : 0;
                                checkExits.fk_contract = (request.fk_contract != 0) ? request.fk_contract : 0;
                                checkExits.c_unit_id = (request.c_unit_id != 0) ? request.c_unit_id : 0;
                                checkExits.c_staff_id = (request.c_staff_id != 0) ? request.c_staff_id : 0;
                                checkExits.c_departments_id = (request.c_departments_id != 0) ? request.c_departments_id : 0;
                                checkExits.c_group_id = (request.c_group_id != 0) ? request.c_group_id : 0;
                                checkExits.c_percent = (request.c_percent != 0) ? request.c_percent : 0;
                                checkExits.c_money = (request.c_money != 0) ? request.c_money : 0;
                                checkExits.c_money_in_year = (request.c_money_in_year != 0) ? request.c_money_in_year : 0;
                                checkExits.c_money_web_other = (request.c_money_web_other != 0) ? request.c_money_web_other : 0;
                                checkExits.c_xml_data = "";
                                checkExits.c_charge = !string.IsNullOrEmpty(request.c_charge) ? request.c_charge : "";
                                checkExits.c_money_next_year = (request.c_money_next_year != 0) ? request.c_money_next_year : 0;
                                checkExits.c_money_next_year2 = (request.c_money_next_year2 != 0) ? request.c_money_next_year2 : 0;
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