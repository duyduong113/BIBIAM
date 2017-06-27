using ERPAPD.Helpers;
using ERPAPD.Models;
using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using Kendo.Mvc.Extensions;
using System.Text;
using System.IO;
using Dapper;

namespace ERPAPD.Controllers
{
    public class AjaxController : CustomController
    {
        // GET: Ajax
        public ActionResult ConvertUFT8()
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    // Convert 1 lần bảng CRM_Adv
                    //var data = dbConn.Select<CRM_Adv>(@"select * from CRM_Adv");
                    //foreach (var item in data)
                    //{
                    //    item.c_note = Helpers.ConvertANSIToUTF8.Convert(item.c_note);
                    //    dbConn.Update(set: "c_note=N"+item.c_note, where: "pk_adv="+item.pk_adv);
                    //}
                    //var data = dbConn.Select<CRM_Contract>(@"select pk_contract,c_labels from CRM_Contract");
                    //foreach (var item in data)
                    //{
                    //    item.c_labels = Helpers.ConvertANSIToUTF8.Convert(item.c_labels);
                    //    dbConn.Update<CRM_Contract>(set: "c_labels=N'" + item.c_labels + "'", where: "pk_contract=" + item.pk_contract);
                    //}
                    //var data = dbConn.Select<CRM_Adv>(@"select pk_adv,c_noi_dung_duyet,c_noi_dung_gui_duyet,c_noi_dung_xn_dang_dv from CRM_Adv");
                    //foreach (var item in data)
                    //{
                    //    item.c_noi_dung_duyet = Helpers.ConvertANSIToUTF8.Convert(item.c_noi_dung_duyet);
                    //    item.c_noi_dung_gui_duyet = Helpers.ConvertANSIToUTF8.Convert(item.c_noi_dung_gui_duyet);
                    //    item.c_noi_dung_xn_dang_dv = Helpers.ConvertANSIToUTF8.Convert(item.c_noi_dung_xn_dang_dv);
                    //    dbConn.Update<CRM_Adv>(set: "c_noi_dung_duyet=N'" + item.c_noi_dung_duyet + "', "+ "c_noi_dung_gui_duyet=N'" + item.c_noi_dung_gui_duyet + "', "+ "c_noi_dung_xn_dang_dv=N'" + item.c_noi_dung_xn_dang_dv + "'"
                    //        , where: "pk_adv=" + item.pk_adv);
                    //}
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
        }
        public ActionResult checkExitContactID(string ContractCode)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                try
                {
                    var data = dbConn.FirstOrDefault<CRM_Contract_Draff>(@"SELECT PKContractDraft,Code FROM CRM_Contract_Draff where  TrangThai=0 AND Code={0}", ContractCode);
                    if (data != null)
                    {
                        return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, data = data }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message });
                }
            }
        }
        public ActionResult GetlistUser(string PKContactID)
        {

            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                ViewBag.listCategory = dbConn.Select<CRM_Category>("Active={0}", 1);
                if (!string.IsNullOrEmpty(PKContactID))
                {
                    ViewBag.contact = dbConn.Select<ERPAPD_Contacts>(s => s.PKContactID == int.Parse(PKContactID)).FirstOrDefault();
                }
            }
            return View("GetContact");
        }
        public ActionResult GetlistEmployeeByGroup(string[] GroupID)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                if (GroupID != null)
                {
                    if (GroupID[0] != "")
                    {
                        string stringvalue = "";
                        for (int i = 0; i < GroupID.Length; i++)
                        {
                            stringvalue = GroupID[i] + "," + stringvalue;
                        }
                        stringvalue = stringvalue.Substring(0, stringvalue.Length - 1);
                        ViewBag.listEmployee = dbConn.Select<ERPAPD_Employee>(@"Select crm.RowID,crm.RefEmployeeID,crm.UserName,crm.Name
                                FROM ERPAPD_Employee crm
                                LEFT JOIN EmployeeInfo em
                                ON crm.UserName=em.UserName
                                WHere em.Team IN (" + stringvalue + ") ");
                    }
                    else
                    {
                        ViewBag.listEmployee = dbConn.Select<ERPAPD_Employee>(@"Select crm.RowID,crm.RefEmployeeID,crm.UserName,crm.Name
                                FROM ERPAPD_Employee crm
                                LEFT JOIN EmployeeInfo em
                                ON crm.UserName=em.UserName
                                ");
                    }
                }


            }
            return View("_listEmloyeeByGroup");
        }
        public ActionResult GetlistGroupByRegion(string[] RegionID, string type)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                string stringvalue = "";
                string sql = "";
                if (type == "ALL")
                {
                    sql = @"SELECT * FROM [CRM_Team] WHERE FKUnit IN (12,13,14) ";
                }
                else
                { sql = @"SELECT * FROM [CRM_Team] WHERE FKUnit IN (12,13) "; }

                if (RegionID != null)
                {
                    if (RegionID[0] != "")
                    {
                        for (int i = 0; i < RegionID.Length; i++)
                        {
                            stringvalue = RegionID[i] + "," + stringvalue;
                        }
                        stringvalue = stringvalue.Substring(0, stringvalue.Length - 1);
                        sql = @"SELECT * FROM [CRM_Team] WHERE FKUnit IN (" + stringvalue + ")";
                    }

                }
                ViewBag.listTeam = dbConn.Select<CRM_Team>(sql);
            }
            return View("_listGroupByRegion");
        }
        public ActionResult GetListCurrencyNotDefault()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                ViewBag.listCurrenciesND = dbConn.Select<ti_gia_tien_te>(@"Select b.ID,a.ma_tien_te,B.ten_tien_te +' ( '+ b.ki_hieu_tien_te+' )' AS ten_tien_te 
                                                                        from ti_gia_tien_te a
                                                                        LEFT JOIN tien_te b
                                                                        ON a.ma_tien_te=b.ma_tien_te 
                                                                        where a.[trang_thai]= 1 AND a.tien_te_mac_dinh <> 1").OrderBy(a => a.ID);
            }
            return View("_listDefaultcurrency");
        }
        public ActionResult GetListCurrency(string typeid)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                if (typeid == "0")
                {
                    ViewBag.listAllCurrencies = dbConn.Select<tien_te>(@"Select a.ID,a.ma_tien_te,a.ten_tien_te +' ( '+ a.ki_hieu_tien_te+' )' AS ten_tien_te 
                                            from tien_te a
                                            LEFT JOIN ti_gia_tien_te b
                                            ON a.ma_tien_te=b.ma_tien_te 
                                            where b.ma_tien_te IS NULL
                                            ").OrderBy(a => a.ID);
                }
                else
                {
                    ViewBag.listAllCurrencies = dbConn.Select<tien_te>(@"Select a.ID,a.ma_tien_te,a.ten_tien_te +' ( '+ a.ki_hieu_tien_te+' )' AS ten_tien_te 
                                            from tien_te a
                                            LEFT JOIN ti_gia_tien_te b
                                            ON a.ma_tien_te=b.ma_tien_te 
                                            where b.ma_tien_te ='" + typeid + @"'
                                            ").OrderBy(a => a.ID);

                }
            }
            return View("_listCurrency");
        }
        public ActionResult GetDefaultColumns(string table)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                ViewBag.listColumns = dbConn.Select<ERPAPD_Contacts>("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'ERPAPD_Contacts'");
            }
            return View("_default_columns");
        }
        public ActionResult ViewAppointment(Int32 rowID)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                ViewBag.appointment = dbConn.SingleOrDefault<CRM_Appointment>(@"SELECT a.*,co.Name as Name,c.CustomerName AS CustomerName FROM CRM_Appointment a 
                    LEFT JOIN ERPAPD_Customer c ON a.CustomerID = c.CustomerID 
                    LEFT JOIN ERPAPD_Contacts co ON co.PKContactID = a.Person_contact
                    WHERE a.RowID ='" + rowID + "'");
                ViewBag.listAppointmentType = dbConn.Select<Parameters>("Type='AppointmentType'").OrderBy(s => s.ParamID);
            }
            return View("_detail_appointment");
        }
        public ActionResult GetViewWork(Int32 rowID)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                //ViewBag.work = dbConn.SingleOrDefault<CRM_Works>("rowID= {0}", rowID);
                ViewBag.work = dbConn.SingleOrDefault<CRM_Works>(@"SELECT w.*,e.Name as EmName,c.CustomerName AS CustomerName FROM CRM_Works w 
                LEFT JOIN ERPAPD_Customer c ON w.CustomerID = c.CustomerID 
                LEFT JOIN ERPAPD_Contacts e ON w.Person_contact = e.PKContactID   
                WHERE w.RowID ='" + rowID + "'");
                ViewBag.listWorkType = dbConn.Select<Parameters>("Type='WorkType'").OrderBy(s => s.ParamID);
            }
            return View("_detail_work");
        }
        public ActionResult GetCountNotificationType(string type)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var count = dbConn.Scalar<int>("select count(*) from Portal_Message where type = '" + type + "' AND isRead = 0");
                return Json(new { data = count });
            }

        }
        public ActionResult UpdateNotificationRead(string type)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                dbConn.ExecuteSql("UPDATE Portal_Message SET  isRead = 1 WHERE Type = '" + type + "'");
                return Json(new { success = true });
            }

        }

        public ActionResult ViewContactByCus(string CustomerID)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                ViewBag.listContactByCus = dbConn.Select<ERPAPD_Contacts>("Select PKContactID, FKCustomer, Name FROM ERPAPD_Contacts WHERE CustomerID ={0}", CustomerID);
            }
            return View("_contactlistByCustomer");
        }
        public ActionResult Inactive_exts(Int32 RowID, int Status)
        {
            var rs = CRM_ExtsInfo_Meta.save(RowID, Status, currentUser.UserName);
            return Json(rs);
        }
        public ActionResult GetContactListByCustomerID(string CustomerID, string RowID, string Type)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var detailCustomer = dbConn.SingleOrDefault<ERPAPD_Customer>("CustomerID= {0}", CustomerID);
                ViewBag.detailCustomer = detailCustomer;
                //ViewBag.listContactByCus = dbConn.Select<ERPAPD_Contacts>("Select * FROM ERPAPD_Contacts WHERE CustomerID ={0}", CustomerID);
                ViewBag.listContactByCus = dbConn.Select<ERPAPD_Contacts>("Select * FROM ERPAPD_Contacts WHERE FKCustomer ={0}", detailCustomer.ReferID);
                ViewBag.detailCustomer = dbConn.SingleOrDefault<ERPAPD_Customer>("CustomerID= {0}", CustomerID);
                ViewBag.RowID = RowID;
                ViewBag.Type = Type;
            }
            return View("_contactlist_to_call");
        }
        public ActionResult GetInputSuggestEmployer(int incree)
        {
            ViewBag.countID = incree;
            return View("_input_suggest_employer");
        }
        public ActionResult getInputPaypalContract(int incree)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                ViewBag.countID = incree;
                ViewBag.listStatusPaypal = dbConn.Select<ERPAPD_List>(s => s.FKListtype == 29 && s.Status == 1);
                return View("_list_suggest_paypalContract");
            }
        }
        public ActionResult GetInputSuggestStaff(int incree)
        {
            ViewBag.countID = incree;
            return View("_list_suggest_staff");
        }
        public ActionResult GetListProductByType(string Type)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                switch (Type)
                {
                    case "BANNER":
                        {
                            ViewBag.listProduct = dbConn.Select<ERPAPD_Product>("SELECT  PKProduct,Code,Name, Website +'-'+ProductType+'-'+Category+'-'+Location+'-'+Nature+'-'+Size AS Nature FROM ERPAPD_Product Where ProductType= 'BANNER'");
                            break;
                        }
                    case "DB":
                        {
                            ViewBag.listProduct = dbConn.Select<ERPAPD_Product>("SELECT  PKProduct,Code,Name, Website +'-'+ProductType+'-'+Category+'-'+Location+'-'+Nature+'-'+Size AS Nature FROM ERPAPD_Product Where ProductType='QC_TEXT'");
                            break;
                        }
                    case "PRRe":
                        {
                            ViewBag.listProduct = dbConn.Select<ERPAPD_Product>("SELECT  PKProduct,Code,Name, Website +'-'+ProductType+'-'+Category+'-'+Location+'-'+Nature+'-'+Size AS Nature FROM ERPAPD_Product Where ProductType='QC_TEXT'");
                            break;
                        }
                    case "":
                        {
                            ViewBag.listProduct = dbConn.Select<ERPAPD_Product>("SELECT  PKProduct,Code,Name, Website +'-'+ProductType+'-'+Category+'-'+Location+'-'+Nature+'-'+Size AS Nature FROM ERPAPD_Product ");
                            break;
                        }
                    default:
                        ViewBag.listProduct = dbConn.Select<ERPAPD_Product>("SELECT  PKProduct,Code,Name, Website +'-'+ProductType+'-'+Category+'-'+Location+'-'+Nature+'-'+Size AS Nature FROM ERPAPD_Product ");
                        break;
                }

            }
            return View("_listproductbytype");
        }
        public ActionResult GetProductByCode(string Code)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {

                var data = dbConn.Select<ERPAPD_Product>("SELECT  PKProduct,Code,Name, Website +'-'+ProductType+'-'+Category+'-'+Location+'-'+Nature+'-'+Size AS Nature FROM ERPAPD_Product Where PKProduct= {0}", Code);
                return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetListTimeUpdownProductDraff(int FKProduct, int FKContract)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                ViewBag.listTimeUpDown = dbConn.Select<CRM_Contract_ListTime_Draff>(@"SELECT 
                                                                                   [PKTime]
                                                                                  ,[FKProduct]
                                                                                  ,[FKContract]
                                                                                  ,FORMAT([DateUp],'MM/dd/yyyy') AS DateUp
	                                                                              ,FORMAT([DateDown],'MM/dd/yyyy') AS DateDown
                                                                                  ,[Week]
                                                                                  ,[NumberDay]
                                                                                  ,[TotalAmtNoVAT]
                                                                              FROM [CRM_Contract_ListTime_Draff]
                                                                              WHERE [FKProduct]= {0} AND [FKContract] = {1}", FKProduct, FKContract);
            }
            return View("_listTimeUpDownProductDraff");
        }
        public ActionResult GetListPromotionProductDraff(int FKProduct, int FKContract)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                ViewBag.listPromotionProduct = dbConn.Select<CRM_Contract_ListPromotionProduct_Draff>(@"SELECT * 
                                                                              FROM CRM_Contract_ListPromotionProduct_Draff
                                                                               WHERE [FKProduct]= {0} AND [FKContract] = {1}", FKProduct, FKContract);

                // ViewBag.listPromotion = dbConn.Select<ERPAPD_List>(@" Select * from [ERPAPD_List] where FKListtype=31");
                ViewBag.listPromotion = dbConn.Select<ERPAPD_List>(@"SELECT * FROM ERPAPD_List WHERE FKListtype=31");
            }
            return View("_listPromotionProductDraff");
        }
        public ActionResult GetInputSuggestService(int incree, string Type)
        {
            ViewBag.countID = incree;
            string viewName = "";
            switch (Type)
            {
                case "HD_THUONG":
                    viewName = "_input_suggest_hdt";
                    break;
                case "PHIEUPR":
                    viewName = "_input_suggest_hdt";
                    break;
                case "DOI_NGOAI":
                    viewName = "_input_suggest_hdt";
                    break;
                case "GUI_GIA":
                    viewName = "_input_suggest_hdt";
                    break;
                case "GOI":
                    viewName = "_input_suggest_hdt";
                    break;
                case "PHIEU":
                    viewName = "_input_suggest_hdt";
                    break;
                case "HD_THUONG_DATE":
                    viewName = "_input_suggest_hdt_date";
                    break;
                case "CPM":
                    viewName = "_input_suggest_channel";
                    break;
                case "PHIEUCPM":
                    viewName = "_input_suggest_channel";
                    break;
                case "REAL_BANNER":
                    viewName = "_input_suggest_real_banner";
                    break;
                case "REAL_PR":
                    viewName = "_input_suggest_real_pr";
                    break;
                case "DRAFF_THUONG":
                    viewName = "_input_suggest_draff_nm";
                    break;
                case "DRAFF_PHIEUPR":
                    viewName = "_input_suggest_draff_pr";
                    break;
                case "DRAFF_CPM":
                    viewName = "_input_suggest_draff_cpm";
                    break;
            }
            return View(viewName);
        }
        public ActionResult GetTemplateCtrAdditionalList(Int32 Id, bool isView, string Type)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                string viewName = "print/_template_export_additionalList";
                ViewBag.itemAdditional = dbConn.Select<CRM_Contract_Draff_AdditionalList>(s => s.FKContractDraft == Id).FirstOrDefault();
                ViewBag.listStaff = dbConn.Select<CRM_Contract_Draff_Additional_Staff>(@"
                SELECT s.*,
                (select TOP 1 FullName from EmployeeInfo  where RefStaffId = s.StaffId) AS FullName
                FROM CRM_Contract_Draff_Additional_Staff  s
                WHERE s.FKContract =" + Id);
                
                if (Type == "print")
                {
                    viewName = "print/_template_print_additionalList";
                }
                if (Type == "contract" || ViewBag.itemAdditional == null)
                {
                    CRM_Contract_Draff itemDraff = dbConn.Select<CRM_Contract_Draff>(@" 
                         SELECT condraff.*,cus.CustomerName AS CustomerName,cus.CustomerType as CustomerType,
                        (select Value FROM CRM_CategoryHierarchy Where HierarchyID = condraff.CategoryCode ) AS CategoryName,
                        (select TOP 1 FullName from EmployeeInfo  where RefStaffId = condraff.FKStaff) AS UserNameStaff,
                        (select TOP 1 t.TeamName from EmployeeInfo e LEFT JOIN CRM_Team t ON e.Team = t.TeamID where e.RefStaffId = condraff.FKStaff) AS TeamName
                        FROM CRM_Contract_Draff condraff
                        LEFT JOIN  ERPAPD_Customer cus ON cus.CodeLink =condraff.CustomerCode
                        WHERE condraff.PKContractDraft='" + Id + "'").FirstOrDefault();
                    ViewBag.itemdraff = itemDraff;
                    viewName = "print/_template_export_additionalList_by_contract";
                    ViewBag.staffDraff = dbConn.Select<CRM_Contract_Staff_Draff>(
                            @" select s.*,t.TeamName as TeamName, h.Value as Province,e.FullName as FullName from CRM_Contract_Staff_Draff  s
	                            left join CRM_Contract_Draff c ON s.FKContract = c.PKContractDraft
	                            left join CRM_Team t ON s.GroupID = t.TeamID
	                            left join EmployeeInfo e ON s.StaffId = e.RefStaffId
	                            left join CRM_Hierarchy h ON s.UnitId = h.HierarchyID
                                where s.FKContract = " + Id);
                }

                string html = RenderPartialViewToString(viewName);
                if (isView)
                {
                    return View(viewName);
                }
                try
                {
                    ExportToWord(html, Id, Type);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e });
                }
            }
            return Json(new { success = true });
        }
        public ActionResult GetTemplateContract(Int32 Id, bool isView, string Type)
        {
            using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
            {
                ViewBag.typeContract = Type;
                string viewName = "print/_template_export_contract";
                var itemdraffansi = dbConn.Select<CRM_Contract_Draff>(@" 
                        SELECT condraff.*,cus.CustomerID,cus.CustomerName,cus.CategoryParent as CategoryID,
                        cus.Category as CategorySubID,cus.CustomerType as CustomerType, P.Value AS StatusName
                        FROM CRM_Contract_Draff condraff
                        LEFT JOIN  ERPAPD_Customer cus ON cus.CustomerID =condraff.CustomerID
                        LEFT JOIN Parameters P ON P.ParamID=condraff.TrangThai AND P.Type='ContractAPStatus'
                        WHERE condraff.PKContractDraft='" + Id + "'").FirstOrDefault();

                itemdraffansi.Dieu2 = ConvertANSIToUTF8.Convert(itemdraffansi.Dieu2);
                itemdraffansi.DieuKhoan = ConvertANSIToUTF8.Convert(itemdraffansi.DieuKhoan);
                itemdraffansi.GhiChu = ConvertANSIToUTF8.Convert(itemdraffansi.GhiChu);
                ViewBag.itemdraff = itemdraffansi;
                ViewBag.subWiewName = "";
                switch (Type)
                {
                    case "THUONG":
                        ViewBag.subWiewName = "partial/_temp_product_hdt";
                        ViewBag.product = CRM_Contract_Product_Draff.getProductByPKContract(Id);
                        ViewBag.discount = dbConn.Select<CRM_Contract_Draff_Promotion>(s => s.FKContract == Id);
                        break;
                    case "PHIEUPR":
                        viewName = "print/_template_export_contract_p_pr";
                        ViewBag.subWiewName = "partial/_temp_product_p_pr";
                        ViewBag.product = CRM_Contract_Product_Draff.getProductByPKContract(Id);
                        ViewBag.discount = dbConn.Select<CRM_Contract_Draff_Promotion>(s => s.FKContract == Id);
                        break;
                    case "CPM":
                        ViewBag.subWiewName = "partial/_temp_product_cpm";
                        ViewBag.product = dbConn.Select<CRM_Contract_Product_CPM_Draff>(s => s.FkContract == Id);
                        ViewBag.discount = dbConn.Select<CRM_Contract_Draff_Promotion>(@"
                            SELECT SUM(ChietKhauChung) AS SumDiscount FROM CRM_Contract_Draff_Promotion where FKContract = " + Id).FirstOrDefault();

                        break;
                    case "GOI":
                        ViewBag.subWiewName = "partial/_temp_product_hdg";
                        var list = dbConn.Select<CRM_Contract_Product_Packet_Draff>(s => s.FKContract == Id);
                        if (list.Count==0) {
                            ViewBag.product = dbConn.Select<CRM_Contract_Product_Packet>(@"SELECT A.PKProduct AS PKPacket,
                                                     A.FKContract, '' AS Code,'GOI' AS 'Type',A.HUONG AS Name, B.DateUp ,B.DateDown, A.Price AS UnitPrice, B.Discount1 AS Discount,B.Money AS Total
                                                     FROM CRM_Contract_Product_Draff A
                                                     LEFT JOIN CRM_Contract_Time_Draff B
                                                     ON A.FKContract=B.FKContract where A.FKContract ={0}", Id);
                            ViewBag.discount = dbConn.Select<CRM_Contract_Draff_Promotion>(@"
                            SELECT TOP 1 1 AS ID, 0 AS SumDiscount FROM CRM_Contract_Draff_Promotion").FirstOrDefault();
                        }
                        else
                        {
                            ViewBag.product = list;
                            ViewBag.discount = dbConn.Select<CRM_Contract_Draff_Promotion>(@"
                            SELECT  1 AS ID, ISNULL(SUM(ChietKhauChung),0) AS SumDiscount FROM CRM_Contract_Draff_Promotion where FKContract = " + Id).FirstOrDefault();
                        }
                       
                        break;
                    case "PHIEU":
                        ViewBag.product = CRM_Contract_Product_Draff.getProductByPKContract(Id);
                        ViewBag.discount = dbConn.Select<CRM_Contract_Draff_Promotion>(s => s.FKContract == Id);
                        ViewBag.subWiewName = "partial/_temp_product_pdkqc";
                        break;
                    case "PHIEUCPM":
                        ViewBag.subWiewName = "partial/_temp_product_p_cpm";
                        ViewBag.product = dbConn.Select<CRM_Contract_Product_CPM_Draff>(s => s.FkContract == Id);
                        ViewBag.discount = dbConn.Select<CRM_Contract_Draff_Promotion>(@"
                            SELECT SUM(ChietKhauChung) AS SumDiscount FROM CRM_Contract_Draff_Promotion where FKContract = " + Id).FirstOrDefault();
                        break;
                }
                string html = RenderPartialViewToString(viewName);
                if (isView)
                {
                    return View(viewName);
                }
                try
                {
                    ExportToWord(html, Id, Type);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e });

                }
            }
            return Json(new { success = true });
        }
        public void ExportToWord(string content, Int32 PKContract, string type)
        {
            StringBuilder sbTop = new System.Text.StringBuilder();
            string strDocbody = "";
            strDocbody = "<html " + "xmlns:o='urn:schemas-microsoft-com:office:office' " +
                            "xmlns:w='urn:schemas-microsoft-com:office:word'" +
                            "xmlns='http://www.w3.org/TR/REC-html40'>" +
                            "<head><title></title></head>";

            strDocbody += "<body lang = 'EN - US' style = 'tab-interval:.5in'>" +
                            "<div id = 'container'>" + content + "</div>" +
                            "</body></html>"
                            ;
            sbTop.Append(strDocbody);

            string strBody = sbTop.ToString();
            string fileName = "HD_" + type + "_" + PKContract;
            Response.AppendHeader("Content-Type", "application/msword");
            Response.AppendHeader("Content-disposition", "attachment; filename=" + fileName + ".doc");
            Response.Write(strBody);
            Response.End();
            Response.Flush();
        }
        protected string RenderPartialViewToString(string viewName, object model = null)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
        public ActionResult GetInputSuggestGuarantee(int incree)
        {
            ViewBag.countID = incree;
            return View("_list_suggest_guarantee");
        }
        public ActionResult CountStatus(string view)
        {
            var dbConn = ERPAPD.Helpers.OrmliteConnection.openConn();
            var StaffID = dbConn.FirstOrDefault<EmployeeInfo>("SELECT Id,UserName,DepartmentID,Team,RefStaffId FROM EmployeeInfo where UserName='" + currentUser.UserName + "'");
            var UserID = "All";
            var TypeName = "NV";
            if (StaffID != null)
            {
                if (StaffID.DepartmentID == 50 || StaffID.DepartmentID == 51)
                {
                    UserID = StaffID.RefStaffId.ToString();
                    TypeName = "All";
                }
                else if (StaffID.UserName.Contains("administrator"))
                {
                    UserID = StaffID.RefStaffId.ToString();
                    TypeName = "All";
                }
                else
                {
                    UserID = StaffID.RefStaffId.ToString();
                }
            }
            var data = new object();
            switch (view)
            {
                case "ContractDraft":
                    data = CRM_Contract_Draff_QuickSort.CountStatus(UserID, TypeName);
                    break;
                case "Contract":
                    data = CRM_Contract_QuickSort.CountStatus(UserID, TypeName);
                    break;
                case "Customer":
                    data = ERPAPD_Customer_CStatus.CountStatus(UserID, TypeName);
                    break;
                case "DANGQC":
                    data = CRM_Adv_CStatus.CountStatus(UserID, TypeName);
                    break;

            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetInputSuggest_DNQC_Banner(int incree, int stt)
        {
            ViewBag.countID = incree;
            ViewBag.STT = stt;
            return View("_list_ndqc_banner");
        }
        public ActionResult GetInputSuggest_DNQC_Counter(int incree)
        {
            ViewBag.countID = incree;
            return View("_list_ndqc_counter");
        }
        public ActionResult GetInputSuggest_DNQC_Package(int incree)
        {
            ViewBag.countID = incree;
            return View("_list_ndqc_packagePR");
        }

        public ActionResult GetInputSuggest_DNQC_ContractPR(int incree)
        {
            ViewBag.countID = incree;
            return View("_list_ndqc_contractPR");
        }
        public ActionResult GetInputSuggest_DNQC_Cpm(int incree, int cpmStt)
        {
            ViewBag.countID = incree;
            ViewBag.STT = cpmStt;
            return View("_list_ndqc_cpm");
        }


        public ActionResult GetInputSuggest_DNQC_Banner_BindData(int incree, int sttBanner, string contractCode, string[] listBook)
        {
            try
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {

                    var id = string.Join(",", listBook.Select(s => "'" + s + "'"));
                    var i = "(" + id + ")";
                    var c = new DynamicParameters();
                    c.Add("@contractCode", contractCode);
                    c.Add("@IdLocation", i);
                    var detail = dbConn.Query<CRM_Adv_BookingBanner_View>("Choice_Book_Banner", c, commandType: System.Data.CommandType.StoredProcedure);

                    ViewBag.countID = incree;
                    ViewBag.STT = sttBanner;
                    ViewBag.listData = detail;
                    return View("_list_ndqc_banner_Data");
                }
            }
            catch (Exception)
            {

                throw;
            }


        }
        public ActionResult GetInputSuggest_DNQC_Package_BindData(int incree, int coStt, string[] listBook)
        {
            try
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {

                    var id = string.Join(",", listBook.Select(s => "'" + s + "'"));
                    string sql = @"
                                   SELECT	 a.Code
		                                    ,ISNULL(b.[Website],0) AS Website	 
                                            ,ISNULL(b.[Category],0) AS Category
                                            ,ISNULL(b.[Location],0) AS Location
                                            ,ISNULL(b.NgayLen,'1900-01-01') AS NgayLen
                                            ,ISNULL(b.GioLen,0) AS GioLen
                                            ,ISNULL(b.VungMien,0) AS VungMien
		                                    ,ISNULL(web.Name,'') AS WebsiteName	 
		                                    ,ISNULL(cat.Name,'') AS CategoryName	
		                                    ,ISNULL(location.Name,'') AS LocationName	
		                                    ,ISNULL(reg.Name,'') AS RegionName	
		                                    ,ISNULL(timeUp.Name,'') AS TimeUpName	
                                    FROM CRM_Book_PR a
                                    LEFT JOIN [CRM_BookPR_Location] b ON a.PKBookPR = b.FKBookPR
                                    LEFT JOIN ERPAPD_List web ON web.PKList = b.Website and FKListtype = 20
                                    LEFT JOIN ERPAPD_List cat ON cat.PKList = b.Category and cat.FKListtype = 22
                                    LEFT JOIN ERPAPD_List location ON location.PKList = b.Location 
                                    LEFT JOIN ERPAPD_List reg ON reg.Code = b.VungMien and reg.PKList IN (724,725,727)
                                    LEFT JOIN ERPAPD_List timeUp ON timeUp.Code = b.GioLen and timeUp.FKListtype = 72
                                    WHERE b.PKBookPRLocation IN (" + id + ")";

                    var data = dbConn.Select<CRM_Adv_BookingPR_View>(sql);
                    ViewBag.countID = incree;
                    ViewBag.STT = coStt;
                    ViewBag.listData = data;
                    return View("_list_ndqc_packagePR_Data");
                }
            }
            catch (Exception)
            {

                throw;
            }


        }
        public ActionResult GetInputSuggest_DNQC_Contract_BindData(int incree, int coStt, string[] listBook)
        {
            try
            {
                using (IDbConnection dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                {

                    var id = string.Join(",", listBook.Select(s => "'" + s + "'"));
                    string sql = @"
                                   SELECT	 a.Code
		                                    ,ISNULL(b.[Website],0) AS Website	 
                                            ,ISNULL(b.[Category],0) AS Category
                                            ,ISNULL(b.[Location],0) AS Location
                                            ,ISNULL(b.NgayLen,'1900-01-01') AS NgayLen
                                            ,ISNULL(b.GioLen,0) AS GioLen
                                            ,ISNULL(b.VungMien,0) AS VungMien
		                                    ,ISNULL(web.Name,'') AS WebsiteName	 
		                                    ,ISNULL(cat.Name,'') AS CategoryName	
		                                    ,ISNULL(location.Name,'') AS LocationName	
		                                    ,ISNULL(reg.Name,'') AS RegionName	
		                                    ,ISNULL(timeUp.Name,'') AS TimeUpName	
                                    FROM CRM_BookPR a
                                    LEFT JOIN [CRM_BookPRLocation] b ON a.PKBookPR = b.FKBookPR
                                    LEFT JOIN ERPAPD_List web ON web.PKList = b.Website and FKListtype = 20
                                    LEFT JOIN ERPAPD_List cat ON cat.PKList = b.Category and cat.FKListtype = 22
                                    LEFT JOIN ERPAPD_List location ON location.PKList = b.Location 
                                    LEFT JOIN ERPAPD_List reg ON reg.Code = b.VungMien and reg.PKList IN (724,725,727)
                                    LEFT JOIN ERPAPD_List timeUp ON timeUp.Code = b.GioLen and timeUp.FKListtype = 72
                                    WHERE b.ID IN (" + id + ")";

                    var data = dbConn.Select<CRM_Adv_BookingPR_View>(sql);
                    ViewBag.countID = incree;
                    ViewBag.STT = coStt;
                    ViewBag.listData = data;
                    return View("_list_ndqc_contractPR_Data");
                }
            }
            catch (Exception)
            {

                throw;
            }


        }
        public JsonResult GetExtraContractByType(string Type)
        {
            string text = "";
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                string nameID = "";
                switch (Type)
                {
                    case "GOI":
                        nameID = "GOI_CAC_DIEU_KHOAN_KHAC_GOI";
                        break;
                    case "GBANNER":
                        nameID = "GOI_CAC_DIEU_KHOAN_KHAC_GOI_BANNER";
                        break;
                    case "BANNER":
                        nameID = "THUONG_CAC_DIEU_KHOAN_KHAC";
                        break;
                    case "BAI":
                        nameID = "THUONG_CAC_DIEU_KHOAN_KHAC_BAI";
                        break;
                    case "FANPAGE":
                        nameID = "THUONG_CAC_DIEU_KHOAN_KHAC_FANPAGE";
                        break;

                }
                CRM_Contract_Extra extra = dbConn.Select<CRM_Contract_Extra>("NameID= {0}", nameID).FirstOrDefault();
                if (extra != null)
                {
                    text = extra.Content;
                }
                else
                {
                    return Json(new { success = false, content = "Không có dữ liệu" + Type });

                }
            }
            return Json(new { success = true, content = text });
        }
        public ActionResult CloneServiceHDT(int incree, CRM_Contract_Product_Draff normal)
        {
            ViewBag.countID = incree;
            ViewBag.product = normal;
            return View("clone/_clone_draff_nm");
        }
        public ActionResult CloneServicePR(int incree, CRM_Contract_Product_Draff normal)
        {
            ViewBag.countID = incree;
            ViewBag.product = normal;
            return View("clone/_clone_draff_pr");
        }
        public ActionResult CloneServiceCPM(int incree, CRM_Contract_Product_CPM_Draff cpm)
        {
            ViewBag.countID = incree;
            ViewBag.productCPM = cpm;
            return View("clone/_clone_draff_cpm");
        }
        public ActionResult DNDQC_CloneService_ContractPR(int incree, int coStt, CRM_Adv_Services service)
        {
            try
            {
                string viewName = "_clone_dndqc_service_contractPR";
                ViewBag.Service = service;
                ViewBag.countID = incree;
                ViewBag.STT = coStt;
                return View(viewName);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }

        public ActionResult DNDQC_CloneService_PackagePR(int incree, int coStt, CRM_Adv_Services service)
        {
            try
            {
                string viewName = "_clone_dndqc_service_packagePR";
                ViewBag.Service = service;
                ViewBag.countID = incree;
                ViewBag.STT = coStt;
                return View(viewName);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }

        public ActionResult DNDQC_CloneService_CPM(int incree, int cpmStt, CRM_Adv_Services service)
        {
            try
            {
                string viewName = "_clone_dndqc_service_cpm";
                ViewBag.Service = service;
                ViewBag.countID = incree;
                ViewBag.STT = cpmStt;
                return View(viewName);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public ActionResult DNDQC_CloneService_Banner(int incree, int sttBanner, CRM_Adv_Services service)
        {
            try
            {
                string viewName = "_clone_dndqc_service_banner";
                ViewBag.Service = service;
                ViewBag.countID = incree;
                ViewBag.STT = sttBanner;
                return View(viewName);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }
        public ActionResult CloneServiceRealNM(int incree, CRM_Real_Update row)
        {
            ViewBag.countID = incree;
            ViewBag.product = row;
            return View("clone/_clone_real_banner_nm");
        }
        public ActionResult CloneServiceRealNMPR(int incree, CRM_Real_PRLocation row)
        {
            ViewBag.countID = incree;
            ViewBag.product = row;
            return View("clone/_clone_real_pr_nm");
        }
        public ActionResult GetRealCount(Int32 FKContract, string Type)
        {
            string viewName = "";
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                if (Type == "REAL_BN")
                {
                    ViewBag.realBanner = dbConn.Select<CRM_Real_Update>(@"SELECT re.c_website,re.c_category,re.c_location,re.c_nature,
	                  (select  name from ERPAPD_List where code = re.c_website and FKListtype = 20) AS WebsiteName, 
                      (select  name from ERPAPD_List where code = re.c_category and FKListtype = 22) AS CategoryName,
                      (select  name from ERPAPD_List where code = re.c_location and FKListtype = 23) AS LocationName, 
                      (select  name from ERPAPD_List where code = re.c_nature and FKListtype = 25) AS NatureName,
	                  sum(case when c_km <> 'checked' then DATEDIFF(d,c_real_update, c_real_downdate ) + 1 end) as c_days,
	                  sum(case when c_km = 'checked' then DATEDIFF(d,c_real_update, c_real_downdate ) + 1 end) as c_km_days
                    FROM CRM_Real_Update re
                    where fk_contract = " + FKContract + " group by c_location,c_website,c_category,c_nature"
                   );
                    viewName = "_get_real_banner_count";

                }
                if (Type == "REAL_PR")
                {
                    ViewBag.realPR = dbConn.Select<CRM_Real_PRLocation>(@" SELECT pr.Website,pr.Category,pr.Location,
	                (select  name from ERPAPD_List where code = pr.Website and FKListtype = 20) AS WebsiteName, 
                    (select  name from ERPAPD_List where code = pr.Category and FKListtype = 22) AS CategoryName,
                    (select  name from ERPAPD_List where code = pr.Location and FKListtype = 23) AS LocationName, 
	                 sum(pr.DonGia * pr.SoLuong) as DonGia,
                    sum(pr.SoLuong) as SoLuong
                    FROM CRM_Real_PRLocation pr
                    where FKContract = " + FKContract +" group by Website,Category,Location"
                   );
                    viewName = "_get_real_pr_statistical";
                }
            }
            return View(viewName);
        }
        public ActionResult GetRealDetail(Int32 FKContract, string Type ,string Web, string Cat, string Lot, string Nat)
        {
            string viewName = "";
            using (IDbConnection dbConn = OrmliteConnection.openConn())
            {
                if (Type == "REAL_BN")
                {
                    ViewBag.realBanner = dbConn.Select<CRM_Real_Update>(@"
	                  SELECT re.*, (select  name from ERPAPD_List where code = re.c_website and FKListtype = 20) AS WebsiteName, 
                    (select  name from ERPAPD_List where code = re.c_category and FKListtype = 22) AS CategoryName,
                    (select  name from ERPAPD_List where code = re.c_location and FKListtype = 23) AS LocationName, 
                    (select  name from ERPAPD_List where code = re.c_nature and FKListtype = 25) AS NatureName ,
                    (select Top 1 FullName from EmployeeInfo where RefStaffId = re.c_staff_id ) AS EmployerName
                    FROM CRM_Real_Update re 
                    WHERE re.fk_contract = " + FKContract + " AND re.c_website = '" + Web+ "' AND re.c_category = '" + Cat+ "' AND re.c_location = '" + Lot + "' AND re.c_nature = '" + Nat + "'");
                    ViewBag.sumDay = dbConn.Select<CRM_Real_Update>(@"SELECT 
	                  sum(case when c_km <> 'checked' then DATEDIFF(d,c_real_update, c_real_downdate ) + 1 end) as c_days,
	                  sum(case when c_km = 'checked' then DATEDIFF(d,c_real_update, c_real_downdate ) + 1 end) as c_km_days
                    FROM CRM_Real_Update re
                    WHERE re.fk_contract = " + FKContract + " AND re.c_website = '" + Web + "' AND re.c_category = '" 
                    + Cat + "' AND re.c_location = '" + Lot + "' AND re.c_nature = '" + Nat + "' group by c_location,c_website,c_category,c_nature").FirstOrDefault();
                    viewName = "_get_real_banner_detail";

                }
                if (Type == "REAL_PR")
                {
                    ViewBag.realPR = dbConn.Select<CRM_Real_PRLocation>(@"
	                SELECT re.*, (select  name from ERPAPD_List where code = re.Website and FKListtype = 20) AS WebsiteName, 
                    (select  name from ERPAPD_List where code = re.Category and FKListtype = 22) AS CategoryName,
                    (select  name from ERPAPD_List where code = re.Location and FKListtype = 23) AS LocationName, 
                    (select  name from ERPAPD_List where code = re.VungMien and FKListtype = 52) AS TenVungMien
                    FROM CRM_Real_PRLocation re 
                    WHERE re.FKContract = " + FKContract + " AND re.Website = '" + Web + "' AND re.Category = '" + Cat + "' AND re.Location = '" + Lot + "'");
                    viewName = "_get_real_pr_detail";
                }
            }
            return View(viewName);
        }

    }

}