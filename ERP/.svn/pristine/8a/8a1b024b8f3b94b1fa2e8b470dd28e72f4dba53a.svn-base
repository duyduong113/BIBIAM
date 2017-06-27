using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System.Data;

namespace ERPAPD.Models
{
    public class ERPAPD_Banner_Update
    {
        [AutoIncrement]
        public Int64 pk_update { get; set; }
        public Int64 fk_contract { get; set; }
        public string c_website { get; set; }
        public string c_category { get; set; }
        public string c_nature { get; set; }
        public DateTime c_update { get; set; }
        public DateTime c_downdate { get; set; }
        public string c_xml_data { get; set; }
        public string c_time { get; set; }
        public string c_note { get; set; }
        public string c_location { get; set; }
        public Int32 c_number { get; set; }
        public int IsNew { get; set; }
        public DateTime RowCreatedAt { get; set; }
        public DateTime RowUpdatedAt { get; set; }
        public string RowCreatedUser { get; set; }
        public string RowUpdatedUser { get; set; }

        [Ignore]
        public string WebsiteName { get; set; }
        [Ignore]
        public string CategoryName { get; set; }
        [Ignore]
        public string LocationName { get; set; }
        [Ignore]
        public string NatureName { get; set; }
        [Ignore]
        public string StrUpdate { get; set; }
        [Ignore]
        public string StrDowndate { get; set; }

        public static object Save(IEnumerable<ERPAPD_Banner_Update> listItem, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (listItem != null)
                {
                    try
                    {
                        foreach (var request in listItem)
                        {
                            var checkExits = dbConn.SingleOrDefault<ERPAPD_Banner_Update>("pk_update= {0}", request.pk_update);
                            if (checkExits == null)
                            {
                                if (request.fk_contract != 0)
                                {
                                    var row = new ERPAPD_Banner_Update();
                                    row.fk_contract = (request.fk_contract != 0) ? request.fk_contract : 0;
                                    row.c_website = !string.IsNullOrEmpty(request.c_website) ? request.c_website.Trim() : "";
                                    row.c_location = !string.IsNullOrEmpty(request.c_location) ? request.c_location.Trim() : "";
                                    row.c_category = !string.IsNullOrEmpty(request.c_category) ? request.c_category.Trim() : "";
                                    row.c_xml_data = !string.IsNullOrEmpty(request.c_xml_data) ? request.c_xml_data.Trim() : "";
                                    row.c_nature = !string.IsNullOrEmpty(request.c_nature) ? request.c_nature.Trim() : "";
                                    row.c_time = !string.IsNullOrEmpty(request.c_time) ? request.c_time.Trim() : "";
                                    row.c_number = (request.c_number != 0) ? request.c_number : 0;
                                    row.c_update = !string.IsNullOrEmpty(request.StrUpdate) ? DateTime.Parse(request.StrUpdate) : DateTime.Parse("1900-01-01");
                                    row.c_downdate = !string.IsNullOrEmpty(request.StrDowndate) ? DateTime.Parse(request.StrDowndate) : DateTime.Parse("1900-01-01");

                                    row.IsNew = 1;
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
                            else
                            {
                                checkExits.fk_contract = (request.fk_contract != 0) ? request.fk_contract : 0;
                                checkExits.c_website = !string.IsNullOrEmpty(request.c_website) ? request.c_website.Trim() : "";
                                checkExits.c_location = !string.IsNullOrEmpty(request.c_location) ? request.c_location.Trim() : "";
                                checkExits.c_category = !string.IsNullOrEmpty(request.c_category) ? request.c_category.Trim() : "";
                                checkExits.c_xml_data = !string.IsNullOrEmpty(request.c_xml_data) ? request.c_xml_data.Trim() : "";
                                checkExits.c_nature = !string.IsNullOrEmpty(request.c_nature) ? request.c_nature.Trim() : "";
                                checkExits.c_time = !string.IsNullOrEmpty(request.c_time) ? request.c_time.Trim() : "";
                                checkExits.c_number = (request.c_number != 0) ? request.c_number : 0;
                                checkExits.c_update = !string.IsNullOrEmpty(request.StrUpdate) ? DateTime.Parse(request.StrUpdate) : checkExits.c_update;
                                checkExits.c_downdate = !string.IsNullOrEmpty(request.StrDowndate) ? DateTime.Parse(request.StrDowndate) : checkExits.c_downdate;

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

        public static object SaveSingleItem(ERPAPD_Banner_Update request, string username)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                if (request != null)
                {
                    try
                    {
                        
                            var checkExits = dbConn.SingleOrDefault<ERPAPD_Banner_Update>("pk_update= {0}", request.pk_update);
                            if (checkExits == null)
                            {
                                if (request.fk_contract != 0)
                                {
                                    var row = new ERPAPD_Banner_Update();
                                    row.fk_contract = (request.fk_contract != 0) ? request.fk_contract : 0;
                                    row.c_website = !string.IsNullOrEmpty(request.c_website) ? request.c_website.Trim() : "";
                                    row.c_location = !string.IsNullOrEmpty(request.c_location) ? request.c_location.Trim() : "";
                                    row.c_category = !string.IsNullOrEmpty(request.c_category) ? request.c_category.Trim() : "";
                                    row.c_xml_data = !string.IsNullOrEmpty(request.c_xml_data) ? request.c_xml_data.Trim() : "";
                                    row.c_nature = !string.IsNullOrEmpty(request.c_nature) ? request.c_nature.Trim() : "";
                                    row.c_time = !string.IsNullOrEmpty(request.c_time) ? request.c_time.Trim() : "";
                                    row.c_number = (request.c_number != 0) ? request.c_number : 0;
                                    row.c_update = !string.IsNullOrEmpty(request.StrUpdate) ? DateTime.Parse(request.StrUpdate) : DateTime.Parse("1900-01-01");
                                    row.c_downdate = !string.IsNullOrEmpty(request.StrDowndate) ? DateTime.Parse(request.StrDowndate) : DateTime.Parse("1900-01-01");

                                    row.IsNew = 1;
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
                            else
                            {
                                checkExits.fk_contract = (request.fk_contract != 0) ? request.fk_contract : 0;
                                checkExits.c_website = !string.IsNullOrEmpty(request.c_website) ? request.c_website.Trim() : "";
                                checkExits.c_location = !string.IsNullOrEmpty(request.c_location) ? request.c_location.Trim() : "";
                                checkExits.c_category = !string.IsNullOrEmpty(request.c_category) ? request.c_category.Trim() : "";
                                checkExits.c_xml_data = !string.IsNullOrEmpty(request.c_xml_data) ? request.c_xml_data.Trim() : "";
                                checkExits.c_nature = !string.IsNullOrEmpty(request.c_nature) ? request.c_nature.Trim() : "";
                                checkExits.c_time = !string.IsNullOrEmpty(request.c_time) ? request.c_time.Trim() : "";
                                checkExits.c_number = (request.c_number != 0) ? request.c_number : 0;
                                checkExits.c_update = !string.IsNullOrEmpty(request.StrUpdate) ? DateTime.Parse(request.StrUpdate) : checkExits.c_update;
                                checkExits.c_downdate = !string.IsNullOrEmpty(request.StrDowndate) ? DateTime.Parse(request.StrDowndate) : checkExits.c_downdate;

                                checkExits.RowUpdatedUser = username;
                                checkExits.RowUpdatedAt = DateTime.Now;
                                dbConn.Update(checkExits);
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