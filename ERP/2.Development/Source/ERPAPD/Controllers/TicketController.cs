using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPAPD.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERPAPD.Helpers;
using System.Data;
using System.IO;
using OfficeOpenXml;
using System.Collections;
using System.Data.OleDb;

namespace ERPAPD.Controllers
{
    [Authorize]
    [RequireHttps]
    public class TicketController : CustomController
    {
        //
        // GET: /Ticket/
        public ActionResult Index(string TicketID)
        {
            //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            //{
            //    OrmLiteConfig.DialectProvider.UseUnicode = true;
            //    dbConn.DropTables(typeof(Deca_RT_Email));
            //    const bool overwrite = false;
            //    dbConn.CreateTables(overwrite, typeof(Deca_RT_Email));
            //}
            if (asset.View)
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] = asset.Delete;
                ViewData["AllowExport"] = asset.Export;
                ViewData["Asset"] = asset;
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    ViewBag.listAssignee = dbConn.Select<Users>();
                }
                if (string.IsNullOrEmpty(TicketID))
                {
                    ViewBag.TicketID = "";
                }
                else
                {
                    ViewBag.TicketID = TicketID;
                }
                ViewBag.listType = Deca_RT_Type.GetAllDeca_RT_Types().Where(s => s.TypeID != "TIC0092");
                ViewBag.listQueue = Deca_RT_Queue.GetAllDeca_RT_Queues();
                ViewBag.listTypeActive = Deca_RT_Type.GetAllDeca_RT_Types().Where(s => s.Status == "True" && s.TypeID != "TIC0092").ToList();
                ViewBag.listCategory = Deca_RT_Category.GetAllDeca_RT_Categorys().Where(s => s.Status == "True");
                ViewBag.currentuser = currentUser.UserName;
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                    ViewData["ListMerchant"] = dbConn.Select<DC_OCM_MerchantModelView>("SELECT PKMerchantID,MerchantName FROM DC_OCM_Merchant");
                //    ViewBag.listOrgSettlement = Deca_RT_Settlement.GetListOrganizationID().ToList();
                ViewBag.listTypeCanUpdateAll = Deca_RT_RoleUpdate.GetListType(currentUser.UserName);
                ViewBag.listDepartment = Deca_Department.GetAllDeca_Departments();
                ViewBag.listTeam = Deca_Team.GetAllDeca_Teams();
                ViewBag.listPosition = Deca_Position.GetAllDeca_Positions();
                //   ViewBag.listMOPRegion = Deca_RT_MOP_Region.GetListRegion();
                if (Request.QueryString["option"] != null)
                {
                    ViewBag.ViewOption = Request.QueryString["option"].ToString();
                }
                else
                {
                    ViewBag.ViewOption = "";
                }
                return View();
            }
            return RedirectToAction("NoAccessRights", "Error");
        }

        public ActionResult GetListCategoryForTelesales()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                return Json(Deca_RT_Category.GetAllDeca_RT_Categorys().Where(s => s.Status == "True"), JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetListTypeForTelesales(string GroupID)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                return Json(Deca_RT_Type.GetAllDeca_RT_Types().Where(s => s.Status == "True" && s.TypeID != "TIC0092" && s.Category == GroupID).ToList(), JsonRequestBehavior.AllowGet);
            }

        }
        public bool checkrole(string rolename)
        {
            if (rolename == "ViewAll")
            {
                if (currentUser.UserName == "administrator" || currentUser.UserName.Contains("hunghh"))
                {
                    return true;
                }
                var listgroupviewall = Deca_RT_Role.GetAllDeca_RT_Roles();
                if (currentUser.Groups.Where(s => listgroupviewall.Where(p => p.ID == s.Id).Count() > 0).Count() > 0)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        public ActionResult GetRemind(string UserName)
        {
            if (asset.View)
            {
                try
                {
                    var myticket = Deca_RT_Ticket.CountMyTicket(currentUser.UserName);
                    var newticket = Deca_RT_Ticket.CountNewTicket(currentUser.UserName);
                    var queueticket = Deca_RT_Ticket.CountQueueTicket(currentUser.UserName);
                    var createdticket = Deca_RT_Ticket.CountCreatedTicket(currentUser.UserName);
                    var resolvedticket = Deca_RT_Ticket.CountResolvedTicket(currentUser.UserName);
                    return Json(new { success = true, myticket = myticket, newticket = newticket, queueticket = queueticket, createdticket = createdticket, resolvedticket = resolvedticket, user = currentUser.UserName });
                }
                catch
                {
                    return Json(new { success = false, error = true });
                }
            }
            else
            {
                return Json(new { success = true, myticket = 0, newticket = 0, queueticket = 0, createdticket = 0, resolvedticket = 0, user = currentUser.UserName });
            }
        }
        public ActionResult Ticket_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                DataSourceResult data;
                var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "data.");

                if (checkrole("ViewAll") == true)
                {
                    data = Deca_RT_Ticket.GetAllDeca_RT_Tickets_ViewAll_Dynamic(currentUser.UserName, where, request);
                }
                else
                {
                    data = Deca_RT_Ticket.GetAllDeca_RT_Tickets_Dynamic(currentUser.UserName, where, request);
                    //data = Deca_RT_Ticket.GetAllDeca_RT_Tickets(currentUser.UserName);
                }

                string pathForSaving = Server.MapPath("~/UploadFileTicket/");
                foreach (Deca_RT_Ticket i in data.Data)
                {
                    i.GetFileNumber(pathForSaving);
                }
                return Json(data);
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

        }

        public ActionResult Activities_Read([DataSourceRequest] DataSourceRequest request, string TicketID)
        {
            if (asset.View && TicketID != null)
            {

                var data = Deca_RT_Activities.GetAllDeca_RT_Activities(TicketID).OrderByDescending(s => s.RowCreatedTime).ToList();
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

        }
        public ActionResult Comment_Read([DataSourceRequest] DataSourceRequest request, string TicketID)
        {
            if (asset.View && TicketID != null)
            {

                var data = Deca_RT_Comment.GetAllDeca_RT_Comments(TicketID).OrderByDescending(s => s.RowCreatedTime).ToList();
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

        }
        public ActionResult Log_Read([DataSourceRequest] DataSourceRequest request, string TicketID)
        {
            if (asset.View && TicketID != null)
            {

                var data = Deca_RT_Log.GetAllDeca_RT_Logs(TicketID).OrderByDescending(s => s.RowCreatedTime).ToList();
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

        }
        public ActionResult Email_Read([DataSourceRequest] DataSourceRequest request, string TicketID)
        {
            if (asset.View && TicketID != null)
            {
                using (var dbConn = OrmliteConnection.openConn())
                {
                    return Json(KendoApplyFilter.KendoData<Deca_RT_Email>(request, "TicketID='" + TicketID + "'"));
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

        }

        public ActionResult GetSuggestCustomer(string Phone)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Phone) && Phone.Count() > 3)
                {
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        List<DC_OCM_Customer> customer = dbConn.Select<DC_OCM_Customer>("SELECT TOP 5 * FROM DC_OCM_Customer c WHERE [MobilePhone] LIKE N'" + Phone + "%' OR [Email] COLLATE Latin1_General_CI_AI LIKE N'" + Phone + "%'");
                        if (customer.Count > 0)
                        {
                            return Json(customer, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(customer, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                return Json(new List<DC_OCM_Customer>(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi xảy ra!" });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Ticket_Create([DataSourceRequest] DataSourceRequest request, Deca_RT_Ticket row)
        {

            IEnumerable<Deca_RT_Ticket> u = new Deca_RT_Ticket[] { };
            if (asset.Create)
            {
                row.RowCreatedTime = DateTime.Now;
                row.RowLastUpdatedTime = DateTime.Now;
                row.OrganizationID = "";
                if (row != null && row.TypeID != null && row.Title != null && row.Detail != null && row.Priority != null && row.Impact != null)
                {
                    string message = "";
                    var w = new Deca_RT_Ticket();
                    var checktype = Deca_RT_Type.GetDeca_RT_Type(row.TypeID);
                    if (checktype.RequireCustomerID == "True" && (row.CustomerID == null || row.CustomerID == ""))
                    {
                        ModelState.AddModelError("", "This Type need CustomerID");
                        return Json(u.ToDataSourceResult(request, ModelState));
                    }
                    if (checktype.RequireOrderID == "True" && (row.OrderID == null || row.OrderID == ""))
                    {
                        ModelState.AddModelError("", "This Type need OrderID");
                        return Json(u.ToDataSourceResult(request, ModelState));
                    }
                    w.Status = "New";
                    w.TypeID = row.TypeID;
                    w.Title = row.Title;
                    w.Detail = row.Detail;
                    w.Requestor = currentUser.UserName;
                    w.Assignee = "";
                    w.preAssignee = "";
                    w.Owner = checktype.Owner;
                    if (checktype.Owner == currentUser.UserName)
                    {
                        if (row.preAssignee != null && row.preAssignee != "")
                        {
                            w.preAssignee = row.preAssignee;
                        }
                    }
                    if (row.OrganizationID == null)
                    {
                        row.OrganizationID = "";
                    }
                    if (row.CustomerID == null)
                    {
                        row.CustomerID = "";
                    }
                    w.OrganizationID = row.OrganizationID;
                    w.CustomerID = row.CustomerID;
                    //var checkcus = DW_Deca_Customer.CheckCustomerID(row.CustomerID.Trim());
                    //if (checkcus != null)
                    //{
                    //    w.CustomerID = checkcus.CustomerID;
                    //}
                    w.OrderID = row.OrderID;
                    w.Priority = row.Priority;
                    w.Impact = row.Impact;
                    w.RowCreatedUser = currentUser.UserName;
                    w.RowCreatedTime = DateTime.Now;
                    var id = w.Save();

                    Deca_RT_Follower.DeleteAll(id);
                    if (row.Follower != null)
                    {
                        for (int i = 0; i < row.Follower.Count; i++)
                        {
                            Deca_RT_Follower f = new Deca_RT_Follower();
                            f.TicketID = id;
                            f.RowCreatedTime = DateTime.Now;
                            f.RowCreatedUser = currentUser.UserName;
                            f.UserName = Request.Form["Follower[" + i.ToString() + "][UserName]"];
                            f.Save();
                        }
                    }

                    if (id != "")
                    {
                        //save logs

                        Deca_RT_Log savelog = new Deca_RT_Log();
                        savelog.TicketID = id;
                        savelog.Activites = "Add New Ticket";
                        savelog.RowCreatedTime = DateTime.Now;
                        savelog.RowCreatedUser = currentUser.UserName;
                        savelog.Save();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Data not valid");
                        return Json(u.ToDataSourceResult(request, ModelState));
                    }

                    return Json(new { success = true, message = message });
                }
                ModelState.AddModelError("", "Data not valid");
                return Json(u.ToDataSourceResult(request, ModelState));
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(u.ToDataSourceResult(request, ModelState));
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Ticket_CreateFromTelesales(Deca_RT_Ticket row)
        {

            IEnumerable<Deca_RT_Ticket> u = new Deca_RT_Ticket[] { };
            if (asset.Create)
            {
                row.RowCreatedTime = DateTime.Now;
                row.RowLastUpdatedTime = DateTime.Now;
                row.OrganizationID = "";
                if (row != null && row.TypeID != null && row.Title != null && row.Detail != null && row.Priority != null && row.Impact != null)
                {
                    string message = "";
                    var w = new Deca_RT_Ticket();
                    var checktype = Deca_RT_Type.GetDeca_RT_Type(row.TypeID);
                    if (checktype.RequireCustomerID == "True" && (row.CustomerID == null || row.CustomerID == ""))
                    {
                        return Json(new { error = true, message = "This Type need CustomerID" });
                    }
                    if (checktype.RequireOrderID == "True" && (row.OrderID == null || row.OrderID == ""))
                    {
                        ModelState.AddModelError("", "This Type need OrderID");
                        return Json(new { error = true, message = "This Type need OrderID" });
                    }
                    w.Status = "New";
                    w.TypeID = row.TypeID;
                    w.Title = row.Title;
                    w.Detail = row.Detail;
                    w.Requestor = currentUser.UserName;
                    w.Assignee = "";
                    w.preAssignee = "";
                    w.Owner = checktype.Owner;
                    if (checktype.Owner == currentUser.UserName)
                    {
                        if (row.preAssignee != null && row.preAssignee != "")
                        {
                            w.preAssignee = row.preAssignee;
                        }
                    }
                    if (row.OrganizationID == null)
                    {
                        row.OrganizationID = "";
                    }
                    if (row.CustomerID == null)
                    {
                        row.CustomerID = "";
                    }
                    w.OrganizationID = row.OrganizationID;
                    w.CustomerID = row.CustomerID;
                    w.CustomerName = row.CustomerName;
                    //var checkcus = DW_Deca_Customer.CheckCustomerID(row.CustomerID.Trim());
                    //if (checkcus != null)
                    //{
                    //    w.CustomerID = checkcus.CustomerID;
                    //}
                    w.OrderID = row.OrderID;
                    w.Priority = row.Priority;
                    w.Impact = row.Impact;
                    w.RowCreatedUser = currentUser.UserName;
                    w.RowCreatedTime = DateTime.Now;
                    var id = w.Save();

                    Deca_RT_Follower.DeleteAll(id);
                    if (row.Follower != null)
                    {
                        for (int i = 0; i < row.Follower.Count; i++)
                        {
                            Deca_RT_Follower f = new Deca_RT_Follower();
                            f.TicketID = id;
                            f.RowCreatedTime = DateTime.Now;
                            f.RowCreatedUser = currentUser.UserName;
                            f.UserName = Request.Form["Follower[" + i.ToString() + "][UserName]"];
                            f.Save();
                        }
                    }

                    if (id != "")
                    {
                        //save logs

                        Deca_RT_Log savelog = new Deca_RT_Log();
                        savelog.TicketID = id;
                        savelog.Activites = "Add New Ticket";
                        savelog.RowCreatedTime = DateTime.Now;
                        savelog.RowCreatedUser = currentUser.UserName;
                        savelog.Save();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Data not valid");
                        return Json(new { error = true, message = "Data not valid" });

                    }

                    return Json(new { error = false, message = message });
                }
                ModelState.AddModelError("", "Data not valid");
                return Json(new { error = true, message = "Data not valid" });

            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(new { error = true, message = "You don't have permission to create record" });
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Ticket_CreateFromCustomerSupport(Deca_RT_Ticket row)
        {

            IEnumerable<Deca_RT_Ticket> u = new Deca_RT_Ticket[] { };
            if (asset.Create)
            {
                if (string.IsNullOrEmpty(row.RequestFrom))
                {
                    return Json(new { error = true, message = "Bạn cần chọn nguồn yêu cầu." });
                }
                row.RowCreatedTime = DateTime.Now;
                row.RowLastUpdatedTime = DateTime.Now;
                row.OrganizationID = "";
                if (row != null && row.TypeID != null && row.Title != null && row.Detail != null && row.Priority != null && row.Impact != null)
                {
                    string message = "";
                    var w = new Deca_RT_Ticket();
                    var checktype = Deca_RT_Type.GetDeca_RT_Type(row.TypeID);
                    if (checktype.RequireCustomerID == "True" && (row.CustomerID == null || row.CustomerID == ""))
                    {
                        return Json(new { error = true, message = "Loại yêu cầu này cần mã khách hàng" });
                    }
                    if (checktype.RequireOrderID == "True" && (row.OrderID == null || row.OrderID == ""))
                    {
                        ModelState.AddModelError("", "This Type need OrderID");
                        return Json(new { error = true, message = "Loại yêu cầu này cần mã đơn hàng" });
                    }
                    //check recall
                    if (!row.ReCallTime.HasValue)
                    {
                        w.IsDone = true;
                    }
                    else
                    {
                        w.IsDone = false;
                        w.ReCallTime = row.ReCallTime;
                    }
                    w.Status = row.Status;
                    w.TypeID = row.TypeID;
                    w.Title = row.Title;
                    w.Detail = row.Detail;
                    if (row.CustomerExpectationTimeLine.Year < 1900)
                    {
                        w.CustomerExpectationTimeLine = DateTime.Parse("1900-01-01");
                    }
                    else
                    {
                        w.CustomerExpectationTimeLine = row.CustomerExpectationTimeLine;
                    }
                    w.Requestor = currentUser.UserName;
                    if (row.Status == "New")
                    {
                        w.Assignee = "";
                    }
                    else
                    {
                        w.Assignee = currentUser.UserName;
                    }
                    w.preAssignee = "";
                    w.Owner = checktype.Owner;
                    if (row.OrganizationID == null)
                    {
                        row.OrganizationID = "";
                    }
                    if (row.CustomerID == null)
                    {
                        row.CustomerID = "";
                    }
                    if (row.CustomerName == null)
                    {
                        row.CustomerName = "";
                    }
                    if (row.CustomerSource == null)
                    {
                        row.CustomerSource = "";
                    }
                    if (string.IsNullOrEmpty(row.RefID))
                    {
                        row.RefID = "";
                    }
                    else
                    {
                        using (var dbConn = OrmliteConnection.openConn())
                        {
                            dbConn.Update("Deca_RT_Ticket", "IsDone=1", "TicketID='" + row.RefID + "'");
                        }
                    }

                    w.RequestFrom = row.RequestFrom;
                    w.OrganizationID = row.OrganizationID;
                    w.CustomerID = row.CustomerID;
                    w.CustomerName = row.CustomerName;
                    w.CustomerSource = row.CustomerSource;
                    w.RefID = row.RefID;
                    //var checkcus = DW_Deca_Customer.CheckCustomerID(row.CustomerID.Trim());
                    //if (checkcus != null)
                    //{
                    //    w.CustomerID = checkcus.CustomerID;
                    //}
                    w.OrderID = row.OrderID;
                    w.Priority = row.Priority;
                    w.Impact = row.Impact;
                    w.RowCreatedUser = currentUser.UserName;
                    w.RowCreatedTime = DateTime.Now;
                    var id = w.Save();

                    Deca_RT_Follower.DeleteAll(id);
                    if (row.Follower != null)
                    {
                        for (int i = 0; i < row.Follower.Count; i++)
                        {
                            Deca_RT_Follower f = new Deca_RT_Follower();
                            f.TicketID = id;
                            f.RowCreatedTime = DateTime.Now;
                            f.RowCreatedUser = currentUser.UserName;
                            f.UserName = Request.Form["Follower[" + i.ToString() + "][UserName]"];
                            f.Save();
                        }
                    }

                    if (id != "")
                    {
                        //save logs

                        Deca_RT_Log savelog = new Deca_RT_Log();
                        savelog.TicketID = id;
                        savelog.Activites = "Add New Ticket";
                        savelog.RowCreatedTime = DateTime.Now;
                        savelog.RowCreatedUser = currentUser.UserName;
                        savelog.Save();

                        //save appendix
                        if (!string.IsNullOrEmpty(row.Feeling))
                        {
                            DC_CS_Appendix newAppendix = new DC_CS_Appendix();
                            newAppendix.TicketID = id;
                            newAppendix.CustomerID = row.CustomerID;
                            newAppendix.CustomerName = row.CustomerName;
                            newAppendix.CustomerSource = row.CustomerSource;
                            newAppendix.Feeling = row.Feeling;
                            newAppendix.CreatedAt = DateTime.Now;
                            newAppendix.CreatedBy = currentUser.UserName;
                            newAppendix.UpdatedAt = DateTime.Now;
                            newAppendix.UpdatedBy = currentUser.UserName;
                            using (var dbConn = OrmliteConnection.openConn())
                                dbConn.Insert(newAppendix);
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "Data not valid");
                        return Json(new { error = true, message = "500: Không tạo được ticket mới" });

                    }

                    return Json(new { error = false, message = message });
                }
                ModelState.AddModelError("", "Data not valid");
                return Json(new { error = true, message = "Nhập đầy đủ các thông tin bắt buộc: Loại yêu cầu, Tiêu đề, Mô tả." });

            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(new { error = true, message = "Chưa được cấp quyền." });
            }

        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Ticket_Update([DataSourceRequest] DataSourceRequest request, Deca_RT_Ticket row)
        {

            IEnumerable<Deca_RT_Ticket> u = new Deca_RT_Ticket[] { };
            if (asset.Update)
            {
                if (row != null && ModelState.IsValid)
                {
                    Deca_RT_Ticket w = Deca_RT_Ticket.GetDeca_RT_Ticket(row.TicketID);
                    if (w != null)
                    {
                        string message = "";
                        int haveupdate = 0;
                        // create savelog
                        Deca_RT_Log savelog = new Deca_RT_Log();
                        savelog.TicketID = row.TicketID;
                        savelog.RowCreatedTime = DateTime.Now;
                        savelog.RowCreatedUser = currentUser.UserName;
                        if (row.preAssignee == null)
                        {
                            row.preAssignee = "";
                        }

                        var checktype = Deca_RT_Type.GetDeca_RT_Type(row.TypeID);
                        if (checktype.RequireCustomerID == "True" && (row.CustomerID == null || row.CustomerID == ""))
                        {
                            ModelState.AddModelError("", "This Type need CustomerID");
                            return Json(u.ToDataSourceResult(request, ModelState));
                        }

                        if (row.Follower != null)
                        {
                            Deca_RT_Follower.DeleteAll(row.TicketID);
                            for (int i = 0; i < row.Follower.Count; i++)
                            {
                                Deca_RT_Follower f = new Deca_RT_Follower();
                                f.TicketID = row.TicketID;
                                f.RowCreatedTime = DateTime.Now;
                                f.RowCreatedUser = currentUser.UserName;
                                f.UserName = Request.Form["Follower[" + i.ToString() + "][UserName]"];
                                f.Save();
                            }
                        }

                        var check1 = Deca_RT_Ticket.GetDeca_RT_Ticket_checkintype(w.TypeID, currentUser.UserName);
                        if (w.Status == "New")
                        {
                            // 
                            if (checktype.Owner == currentUser.UserName)
                            {
                                w.Priority = row.Priority;
                                w.Impact = row.Impact;
                                if ((row.preAssignee == null || row.preAssignee == "") && w.preAssignee != "")
                                {
                                    w.preAssignee = "";
                                    //save logs
                                    savelog.Activites = currentUser.UserName + " cancel assignee ";
                                    savelog.Save();
                                }
                                else if ((row.preAssignee != null && row.preAssignee != ""))
                                {
                                    w.preAssignee = row.preAssignee;
                                    //save logs
                                    savelog.Activites = currentUser.UserName + " assignee to : " + row.preAssignee;
                                    savelog.Save();
                                }
                                w.TypeID = row.TypeID;
                                w.Owner = checktype.Owner;
                                haveupdate = 1;
                            }
                            else if (w.Requestor == currentUser.UserName)
                            {
                                w.Title = row.Title;
                                w.Detail = row.Detail;
                                w.Priority = row.Priority;
                                w.Impact = row.Impact;
                                w.Owner = checktype.Owner;
                                w.TypeID = row.TypeID;
                                haveupdate = 1;
                            }
                            else if (check1 != null)
                            {
                                w.Priority = row.Priority;
                                w.Impact = row.Impact;
                                w.Owner = checktype.Owner;
                                w.TypeID = row.TypeID;
                                haveupdate = 1;
                            }
                        }

                        if (w.Status == "Denied")
                        {
                            if (w.Requestor == currentUser.UserName)
                            {
                                w.Title = row.Title;
                                w.Detail = row.Detail;
                                w.Priority = row.Priority;
                                w.Impact = row.Impact;

                                w.TypeID = row.TypeID;
                                w.Owner = checktype.Owner;
                                if (w.TypeID != "")
                                {
                                    w.Status = "New";
                                }
                                haveupdate = 1;
                                w.RowCreatedTime = DateTime.Now;
                                w.UpdateCreateTime();
                            }
                            if (checktype.Owner == currentUser.UserName)
                            {
                                if ((row.preAssignee != null && row.preAssignee != "") && w.TypeID != "")
                                {
                                    w.preAssignee = row.preAssignee;
                                    //save logs
                                    savelog.Activites = currentUser.UserName + " assignee to : " + row.preAssignee;
                                    savelog.Save();
                                }
                                haveupdate = 1;
                            }
                        }

                        // status = Escalated, khi assign sẽ đổi trạng thái
                        if (w.Status == "Escalated" && row.preAssignee != w.preAssignee && (row.Assignee == currentUser.UserName || Deca_RT_RoleUpdate.Check(currentUser.UserName, row.TypeID)))
                        {
                            w.preEscalateQueue = "";
                            w.Status = "WIP";
                            //save logs
                            savelog.Activites = currentUser.UserName + " cancel Escalated ";
                            savelog.Save();

                            if (w.preAssignee != "")
                            {
                                w.Status = "Assigned";
                                //save logs
                                savelog.Activites = currentUser.UserName + " assignee to : " + row.preAssignee;
                                savelog.Save();
                            }

                            w.preAssignee = row.preAssignee;
                            haveupdate = 1;
                        }
                        if (w.Status == "Assigned" && row.preAssignee != w.preAssignee && (row.Assignee == currentUser.UserName || Deca_RT_RoleUpdate.Check(currentUser.UserName, row.TypeID)))
                        {
                            w.preAssignee = row.preAssignee;

                            if (w.preAssignee != "")
                            {
                                //save logs
                                savelog.Activites = currentUser.UserName + " assignee to : " + row.preAssignee;
                                savelog.Save();
                            }
                            else
                            {
                                //save logs
                                w.Status = "WIP";
                                savelog.Activites = currentUser.UserName + " cancel assignee ";
                                savelog.Save();
                            }

                            haveupdate = 1;
                        }
                        if ((w.Status == "WIP" || w.Status == "Reopen" || w.Status == "Accepted" || w.Status == "Denied-WIP") && row.preAssignee != "" && (row.Assignee == currentUser.UserName || Deca_RT_RoleUpdate.Check(currentUser.UserName, row.TypeID)))
                        {
                            w.preAssignee = row.preAssignee;
                            w.Status = "Assigned";

                            //save logs
                            savelog.Activites = currentUser.UserName + " assignee to : " + row.preAssignee;
                            savelog.Save();

                            haveupdate = 1;
                        }
                        if (w.Status == "New" || w.Status == "Denied")
                        {
                            if (w.Requestor == currentUser.UserName)
                            {
                                if (row.OrganizationID == null)
                                {
                                    row.OrganizationID = "";
                                }
                                if (row.CustomerID == null)
                                {
                                    row.CustomerID = "";
                                }
                                w.OrganizationID = row.OrganizationID;
                                //var checkcus = DW_Deca_Customer.CheckCustomerID(row.CustomerID.Trim());
                                //if (checkcus != null)
                                //{
                                //    w.CustomerID = checkcus.CustomerID;
                                //}
                                w.OrderID = row.OrderID;
                                haveupdate = 1;
                            }
                        }
                        if (w.Status == "Accepted" && row.Assignee == currentUser.UserName && w.EscalateQueue != "")
                        {

                            w.EscalateQueue = "";

                            w.TypeID = row.TypeID;
                            w.Owner = checktype.Owner;
                            haveupdate = 1;
                        }
                        if (haveupdate == 1)
                        {
                            if (w.preAssignee == null)
                            {
                                w.preAssignee = "";
                            }
                            w.RowLastUpdatedUser = currentUser.UserName;
                            w.RowLastUpdatedTime = DateTime.Now;
                            //update(1) : tính lại giá trị deadline, kpi
                            w.Update(1);

                            //save logs

                            savelog.Activites = currentUser.UserName + " Edit Ticket";
                            savelog.Save();
                            return Json(new { success = true, message = message });
                        }
                        ModelState.AddModelError("", "You don't have permission to update record");
                        return Json(u.ToDataSourceResult(request, ModelState));
                    }
                }
                ModelState.AddModelError("", "Data not valid");
                return Json(u.ToDataSourceResult(request, ModelState));
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to update record");
                return Json(u.ToDataSourceResult(request, ModelState));
            }

        }
        public ActionResult Ticket_GetType([DataSourceRequest] DataSourceRequest request, string categoryid)
        {
            if (asset.View)
            {
                var data = Deca_RT_Type.GetAllDeca_RT_Types_Category(categoryid).Where(s => s.Status == "True" && s.TypeID != "TIC0092").ToList();
                return Json(new { success = true, Data = data });
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult Ticket_GetListAssignee([DataSourceRequest] DataSourceRequest request, string typeid)
        {
            if (asset.View)
            {

                var check = Deca_RT_Type.GetDeca_RT_Type(typeid);
                var data = Deca_RT_Type.GetListAssignee(typeid);
                return Json(new { success = true, Data = data, Owner = check.Owner });
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult GetListUserInType(string TypeID, string EscalateQueue)
        {
            if (asset.View)
            {
                if (EscalateQueue != null && EscalateQueue != "")
                {

                    var listdata = Deca_RT_Queue.GetListUserInQueue(EscalateQueue);
                    return Json(new { success = true, Data = listdata });
                }
                else
                {
                    var listdata = Deca_RT_Type.GetListUserInType(TypeID);
                    return Json(new { success = true, Data = listdata });
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult GetListUserInQueue(string QueueID)
        {
            if (asset.View)
            {

                var data = Deca_RT_Queue.GetListUserInQueue(QueueID);
                return Json(new { success = true, Data = data });
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Ticket_CheckCustomer(string CustomerID, string TypeID, string OrganizationID)
        {
            if (asset.View)
            {
                var type = Deca_RT_Type.GetDeca_RT_Type(TypeID);
                if (type == null || (type.RequireCustomerID == "True" && (CustomerID == null || CustomerID == "" || OrganizationID == null || OrganizationID == "")))
                {
                    return Json(new { success = false, message = "This Type need CustomerID and OrganizationID" });
                }
                if (OrganizationID == null || OrganizationID == "" && (CustomerID != null && CustomerID != ""))
                {
                    return Json(new { success = false, message = "Need select OrganizationID" });
                }
                if (CustomerID != null && CustomerID != "" && OrganizationID != null && OrganizationID != "")
                {
                    try
                    {
                        var temp = CustomerID.Trim().Split(':');
                        if (temp.Count() > 1)
                        {
                            if (temp[0].ToUpper() != OrganizationID.ToUpper())
                            {
                                return Json(new { success = false, message = "CustomerID not valid" });
                            }
                            //var data = DW_Deca_Customer.CheckCustomerID(CustomerID.Trim());
                            //if (data == null)
                            //{
                            //    return Json(new { success = false, message = "CustomerID not exist" });
                            //}
                            return Json(new { success = true });
                        }
                        else
                        {
                            //var checkEployeeID = DW_Deca_Customer.GetCustomerID_ByEmployeeID(CustomerID, OrganizationID);
                            //if (checkEployeeID == null)
                            //{
                            //    var checkcus = DW_Deca_Customer.CheckCustomerID(OrganizationID + ":" + CustomerID.Trim());
                            //    if (checkcus == null)
                            //    {
                            //        return Json(new { success = false, message = "CustomerID not exist" });
                            //    }
                            //    else
                            //    {
                            //        return Json(new { success = true, CustomerID = checkcus.CustomerID, message = "CustomerID: " + checkcus.CustomerID + " ; CustomerName: " + checkcus.FullName + ". Are you sure this Customer?" });
                            //    }
                            //}
                            //else
                            //{
                            //    return Json(new { success = true, CustomerID = checkEployeeID.CustomerID, message = "CustomerID: " + checkEployeeID.CustomerID + " ; CustomerName: " + checkEployeeID.FullName + ". Are you sure this Customer?" });
                            //}
                        }
                    }
                    catch
                    {
                        return Json(new { success = false, message = "Error" });
                    }
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "Error" });
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Activities_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Deca_RT_Activities> listorder, string TicketID)
        {

            if (asset.View)
            {

                if (listorder != null && ModelState.IsValid)
                {
                    if (TicketID != null)
                    {
                        var check = Deca_RT_Ticket.GetDeca_RT_Ticket(TicketID);
                        if (check != null && (check.Status == "WIP" || check.Status == "Reopen" || check.Status == "Denied-WIP" || check.Status == "Assigned" || check.Status == "Escalated" || check.Status == "Accepted") && (check.Assignee == currentUser.UserName || check.Owner == currentUser.UserName || check.Requestor == currentUser.UserName))
                        {
                            Deca_RT_Activities acc = new Deca_RT_Activities();
                            acc.RowCreatedTime = DateTime.Now;
                            acc.RowCreatedUser = currentUser.UserName;
                            acc.TicketID = TicketID;
                            foreach (var order in listorder)
                            {
                                acc.Activities = order.Activities;
                                acc.Save();
                            }
                            check.LastActivities = currentUser.UserName + " cập nhật lúc " + DateTime.Now.ToString("HH:mm dd/MM/yyyy") + " : " + listorder.FirstOrDefault().Activities;
                            check.RowLastUpdatedTime = DateTime.Now;
                            check.RowLastUpdatedUser = currentUser.UserName;
                            check.Update(0);
                            //save logs
                            //Deca_RT_Ticket_Log savelog = new Deca_RT_Ticket_Log();
                            //savelog.TicketID = TicketID;
                            //savelog.Activites = "Add Activities";
                            //savelog.RowCreatedTime = DateTime.Now;
                            //savelog.RowCreatedUser = currentUser.UserName;
                            //savelog.Save();


                            return Json(new { success = true });
                        }
                        else
                        {
                            ModelState.AddModelError("", "Can not create Activities this order");
                            return Json(listorder.ToDataSourceResult(request, ModelState));
                        }

                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(listorder.ToDataSourceResult(request, ModelState));
            }

            return Json(listorder.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Comment_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Deca_RT_Comment> listcomment, string TicketID)
        {

            if (asset.View)
            {

                if (listcomment != null && ModelState.IsValid)
                {
                    if (TicketID != null)
                    {
                        var check = Deca_RT_Ticket.GetDeca_RT_Ticket(TicketID);
                        if (check != null && check.Status != "Closed" && check.Status != "Cancelled")
                        {
                            Deca_RT_Comment acc = new Deca_RT_Comment();
                            acc.RowCreatedTime = DateTime.Now;
                            acc.RowCreatedUser = currentUser.UserName;
                            acc.TicketID = TicketID;
                            foreach (var comment in listcomment)
                            {
                                acc.Comment = comment.Comment;
                                acc.Save();
                            }
                            check.LastComment = currentUser.UserName + " bình luận lúc " + DateTime.Now.ToString("HH:mm dd/MM/yyyy") + " : " + listcomment.FirstOrDefault().Comment;
                            check.RowLastUpdatedTime = DateTime.Now;
                            check.RowLastUpdatedUser = currentUser.UserName;
                            check.Update(0);
                            return Json(new { success = true });
                        }
                        else
                        {
                            ModelState.AddModelError("", "Không thể comment ticket đã đóng hoặc hủy.");
                            return Json(listcomment.ToDataSourceResult(request, ModelState));
                        }

                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Bạn chỉ có quyền xem, liên hệ 1080 để được cấp quyền comment.");
                return Json(listcomment.ToDataSourceResult(request, ModelState));
            }

            return Json(listcomment.ToDataSourceResult(request, ModelState));
        }


        [HttpPost]
        public ActionResult WIP(string data)
        {
            if (asset.Update)
            {
                int count = 0;
                int total = 0;
                string message = "";
                string[] separators = { ";;" };
                var listdata = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var id in listdata)
                {
                    var check = Deca_RT_Ticket.GetDeca_RT_Ticket(id);
                    if (check != null)
                    {

                        //if (check.Status == "New" && check.Assignee == currentUser.UserName)
                        //{
                        //    check.Status = "WIP";
                        //    check.ResponseDate = DateTime.Now;
                        //    check.RowLastUpdatedTime = DateTime.Now;
                        //    check.RowLastUpdatedUser = currentUser.UserName;
                        //    check.Update(0);
                        //    total++;
                        //    //save logs
                        //    Deca_RT_Ticket_Log savelog = new Deca_RT_Ticket_Log();
                        //    savelog.TicketID = id;
                        //    savelog.Activites = "Change Status to WIP";
                        //    savelog.RowCreatedTime = DateTime.Now;
                        //    savelog.RowCreatedUser = currentUser.UserName;
                        //    savelog.Save();
                        //}
                        if ((check.Status == "Accepted" || check.Status == "Denied-WIP" || check.Status == "Escalated" || check.Status == "Assigned" || check.Status == "Reopen") && (check.Assignee == currentUser.UserName || Deca_RT_RoleUpdate.Check(currentUser.UserName, check.TypeID)))
                        {
                            check.Status = "WIP";
                            check.preEscalateQueue = "";
                            check.preAssignee = "";
                            check.RowLastUpdatedTime = DateTime.Now;
                            check.RowLastUpdatedUser = currentUser.UserName;
                            check.Update(0);
                            total++;
                            //save logs
                            Deca_RT_Log savelog = new Deca_RT_Log();
                            savelog.TicketID = id;
                            savelog.Activites = "Change Status to WIP";
                            savelog.RowCreatedTime = DateTime.Now;
                            savelog.RowCreatedUser = currentUser.UserName;
                            savelog.Save();
                        }
                        else
                        {
                            message = "Không thể đổi trạng thái ticket sang  WIP";
                            count = 1;
                        }
                    }
                    else
                    {
                        message = "Ticket không tồn tại";
                        count = 1;
                    }
                }
                return Json(new { error = count, success = total, message = message });
            }
            else
            {
                return Json(new { error = 1, success = 0, message = "Không có quyền cập nhật" });
            }
        }


        [HttpPost]
        public ActionResult Resolved(string data)
        {
            if (asset.Update)
            {
                int count = 0;
                int total = 0;
                string message = "";
                string[] separators = { ";;" };
                var listdata = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var id in listdata)
                {
                    var check = Deca_RT_Ticket.GetDeca_RT_Ticket(id);
                    if (check != null)
                    {

                        if ((check.Status == "WIP" || check.Status == "Reopen" || check.Status == "Accepted" || check.Status == "Denied-WIP" || check.Status == "Accepted" || check.Status == "Escalated" || check.Status == "Assigned") && (check.Owner == currentUser.UserName || check.Assignee == currentUser.UserName || Deca_RT_RoleUpdate.Check(currentUser.UserName, check.TypeID)))
                        {
                            check.Status = "Resolved";
                            check.preAssignee = "";
                            check.preEscalateQueue = "";
                            check.ResolveDate = DateTime.Now;
                            check.RowLastUpdatedTime = DateTime.Now;
                            check.RowLastUpdatedUser = currentUser.UserName;
                            check.Update(0);
                            total++;
                            //save logs

                            Deca_RT_Log savelog = new Deca_RT_Log();
                            savelog.TicketID = id;
                            savelog.Activites = "Change Status to Resolved";
                            savelog.RowCreatedTime = DateTime.Now;
                            savelog.RowCreatedUser = currentUser.UserName;
                            savelog.Save();
                        }
                        else
                        {
                            message = "Không thể đổi trạng thái ticket sang  Resolved";
                            count = 1;
                        }
                    }
                    else
                    {
                        message = "Ticket không tồn tại";
                        count = 1;
                    }
                }
                return Json(new { error = count, success = total, message = message });
            }
            else
            {
                return Json(new { error = 1, success = 0, message = "Không có quyền cập nhật" });
            }
        }


        [HttpPost]
        public ActionResult Closed(string data)
        {
            if (asset.Update)
            {
                int count = 0;
                int total = 0;
                string message = "";
                string[] separators = { ";;" };
                var listdata = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var id in listdata)
                {
                    var check = Deca_RT_Ticket.GetDeca_RT_Ticket(id);
                    if (check != null)
                    {

                        if (check.Status == "Resolved" && (check.Requestor == currentUser.UserName || Deca_RT_RoleUpdate.Check(currentUser.UserName, check.TypeID)))
                        {
                            check.Status = "Closed";
                            check.preAssignee = check.Assignee;
                            try
                            {
                                check.CloseDate = DateTime.Now;
                                check.RowLastUpdatedTime = DateTime.Now;
                                check.RowLastUpdatedUser = currentUser.UserName;
                                check.Update(0);
                                total++;
                                //save logs

                                Deca_RT_Log savelog = new Deca_RT_Log();
                                savelog.TicketID = id;
                                savelog.Activites = "Change Status to Closed";
                                savelog.RowCreatedTime = DateTime.Now;
                                savelog.RowCreatedUser = currentUser.UserName;
                                savelog.Save();
                            }
                            catch
                            {
                                count = 1;
                            }
                        }
                        else
                        {
                            message = "Không thể đổi trạng thái ticket sang  Closed";
                            count = 1;
                        }
                    }
                    else
                    {
                        message = "Ticket không tồn tại";
                        count = 1;
                    }
                }
                return Json(new { error = count, success = total, message = message });
            }
            else
            {
                return Json(new { error = 1, success = 0, message = "Không có quyền cập nhật" });
            }
        }

        [HttpPost]
        public ActionResult Cancelled(string data)
        {
            if (asset.Update)
            {
                int count = 0;
                int total = 0;
                string message = "";
                string[] separators = { ";;" };
                var listdata = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var id in listdata)
                {
                    var check = Deca_RT_Ticket.GetDeca_RT_Ticket(id);
                    if (check != null && check.Requestor == currentUser.UserName && check.Status != "Closed")
                    {

                        check.Status = "Cancelled";
                        try
                        {
                            check.CloseDate = DateTime.Now;
                            check.RowLastUpdatedTime = DateTime.Now;
                            check.RowLastUpdatedUser = currentUser.UserName;
                            check.Update(0);
                            total++;
                            //save logs

                            Deca_RT_Log savelog = new Deca_RT_Log();
                            savelog.TicketID = id;
                            savelog.Activites = "Change Status to Cancelled";
                            savelog.RowCreatedTime = DateTime.Now;
                            savelog.RowCreatedUser = currentUser.UserName;
                            savelog.Save();
                        }
                        catch
                        {
                            count = 1;
                        }

                    }
                    else
                    {
                        message = "Không thể Cancelled";
                        count = 1;
                    }
                }
                return Json(new { error = count, success = total, message = message });
            }
            else
            {
                return Json(new { error = 1, success = 0, message = "Không có quyền cập nhật" });
            }
        }
        [HttpPost]
        public ActionResult Delete(string data)
        {
            if (asset.Delete)
            {
                int count = 0;
                int total = 0;
                string message = "";
                string[] separators = { ";;" };
                var listdata = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var id in listdata)
                {
                    var check = Deca_RT_Ticket.GetDeca_RT_Ticket(id);
                    if (check != null && (check.Status == "New" || check.Status == "Denied") && check.Requestor == currentUser.UserName)
                    {
                        try
                        {
                            check.Delete();
                            total++;
                        }
                        catch
                        {
                            count = 1;
                        }

                    }
                    else
                    {
                        message = "Không thể Delete";
                        count = 1;
                    }
                }
                return Json(new { error = count, success = total, message = message });
            }
            else
            {
                return Json(new { error = 1, success = 0, message = "You don't have permisson to Delete" });
            }
        }
        [HttpPost]
        public ActionResult Reopen(string data)
        {
            if (asset.Update)
            {
                int count = 0;
                int total = 0;
                string message = "";
                string[] separators = { ";;" };
                var listdata = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var id in listdata)
                {
                    var check = Deca_RT_Ticket.GetDeca_RT_Ticket(id);
                    if (check != null && (check.Status == "Closed" || check.Status == "Resolved") && (check.Requestor == currentUser.UserName || check.Owner == currentUser.UserName))
                    {

                        check.Status = "Reopen";
                        // check.Assignee = "";
                        // check.preAssignee = "";
                        try
                        {
                            check.Reopentime = DateTime.Now;
                            check.preAssignee = "";
                            check.preEscalateQueue = "";
                            check.RowLastUpdatedTime = DateTime.Now;
                            check.RowLastUpdatedUser = currentUser.UserName;
                            check.Update(0);
                            check.UpdateNew();
                            total++;
                            //save logs

                            Deca_RT_Log savelog = new Deca_RT_Log();
                            savelog.TicketID = id;
                            savelog.Activites = "Change Status to Reopen";
                            savelog.RowCreatedTime = DateTime.Now;
                            savelog.RowCreatedUser = currentUser.UserName;
                            savelog.Save();
                        }
                        catch
                        {
                            count = 1;
                        }

                    }
                    else
                    {
                        message = "Ticket không thể Reopen";
                        count = 1;
                    }
                }
                return Json(new { error = count, success = total, message = message });
            }
            else
            {
                return Json(new { error = 1, success = 0, message = "Không có quyền cập nhật" });
            }
        }
        [HttpPost]
        public ActionResult Clone(string data)
        {
            if (asset.Update)
            {
                var check = Deca_RT_Ticket.GetDeca_RT_Ticket(data);
                string message = "";
                if (check != null && check.Requestor == currentUser.UserName)
                {
                    Deca_RT_Ticket newticket = new Deca_RT_Ticket();
                    newticket.Status = "New";
                    try
                    {
                        newticket.TypeID = check.TypeID;
                        newticket.Title = check.Title;
                        newticket.Detail = check.Detail;
                        newticket.Requestor = check.Requestor;
                        newticket.Owner = check.Owner;
                        newticket.OrganizationID = check.OrganizationID;
                        newticket.CustomerID = check.CustomerID;
                        newticket.Priority = check.Priority;
                        newticket.Impact = check.Impact;
                        newticket.Assignee = "";
                        newticket.preAssignee = "";

                        newticket.RowCreatedTime = DateTime.Now;
                        newticket.RowCreatedUser = currentUser.UserName;
                        var id = newticket.Save();
                        message = "Nhân bản ticket thành công, TicketID mới: " + id;
                        //save logs

                        Deca_RT_Log savelog = new Deca_RT_Log();
                        savelog.TicketID = id;
                        savelog.Activites = "Clone new ticket";
                        savelog.RowCreatedTime = DateTime.Now;
                        savelog.RowCreatedUser = currentUser.UserName;
                        savelog.Save();

                        // new ticket log
                        savelog.TicketID = id;
                        savelog.Activites = "Clone new Ticket (old ticket:" + check.TicketID;
                        savelog.Save();
                    }
                    catch
                    {
                        return Json(new { success = false, message = "Error" });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Không thể nhân bản ticket này" });
                }

                return Json(new { success = true, message = message });
            }
            else
            {
                return Json(new { success = false, message = "Không có quyền cập nhật ticket" });
            }
        }
        [HttpPost]
        public ActionResult Accept(string data)
        {
            if (asset.Update)
            {
                int count = 0;
                int total = 0;
                string message = "";
                string[] separators = { ";;" };
                var listdata = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var id in listdata)
                {
                    var check = Deca_RT_Ticket.GetDeca_RT_Ticket(id);
                    if (check != null)
                    {
                        var check1 = Deca_RT_Ticket.GetDeca_RT_Ticket_checkintype(check.TypeID, currentUser.UserName);
                        var check2 = Deca_RT_Type.GetDeca_RT_Type(check.TypeID);
                        Deca_RT_Follower f = new Deca_RT_Follower();
                        f.RowCreatedTime = DateTime.Now;
                        f.RowCreatedUser = currentUser.UserName;
                        // status New, có assign 1 user cụ thể
                        if (check.preAssignee != "" && check.Status == "New")
                        {
                            if (check.preAssignee == currentUser.UserName)
                            {
                                // save follower
                                f.UserName = check.Assignee;
                                f.TicketID = check.TicketID;
                                f.Save();

                                check.Assignee = currentUser.UserName;
                                check.preAssignee = "";
                                check.Status = "Accepted";
                                check.ResponseDate = DateTime.Now;
                                check.Owner = check2.Owner;
                                check.RowLastUpdatedTime = DateTime.Now;
                                check.RowLastUpdatedUser = currentUser.UserName;
                                check.Update(0);
                                total++;


                                //save logs
                                Deca_RT_Log savelog = new Deca_RT_Log();
                                savelog.TicketID = id;
                                savelog.Activites = currentUser.UserName + " accept assignee";
                                savelog.RowCreatedTime = DateTime.Now;
                                savelog.RowCreatedUser = currentUser.UserName;
                                savelog.Save();
                            }
                        }
                        // status New, ko assign 1 user cụ thể, user cùng type có thể accept
                        else if (check.Assignee == "" && check.Status == "New" && check1 != null)
                        {
                            // save follower
                            f.UserName = check.Assignee;
                            f.TicketID = check.TicketID;
                            f.Save();

                            check.Assignee = currentUser.UserName;
                            check.preAssignee = "";
                            check.ResponseDate = DateTime.Now;
                            check.Status = "Accepted";
                            check.ResponseDate = DateTime.Now;
                            check.Owner = check2.Owner;
                            check.RowLastUpdatedTime = DateTime.Now;
                            check.RowLastUpdatedUser = currentUser.UserName;
                            check.Update(0);
                            total++;


                            //save logs
                            Deca_RT_Log savelog = new Deca_RT_Log();
                            savelog.TicketID = id;
                            savelog.Activites = currentUser.UserName + " accept assignee";
                            savelog.RowCreatedTime = DateTime.Now;
                            savelog.RowCreatedUser = currentUser.UserName;
                            savelog.Save();
                        }
                        else if (check.preAssignee == currentUser.UserName && (check.Status == "Assigned"))
                        {
                            // save follower
                            f.UserName = check.Assignee;
                            f.TicketID = check.TicketID;
                            f.Save();

                            check.Assignee = check.preAssignee;
                            check.preAssignee = "";
                            check.Status = "Accepted";
                            check.RowLastUpdatedTime = DateTime.Now;
                            check.RowLastUpdatedUser = currentUser.UserName;
                            check.Update(0);
                            total++;

                            //save logs
                            Deca_RT_Log savelog = new Deca_RT_Log();
                            savelog.TicketID = id;
                            savelog.Activites = currentUser.UserName + " accept assignee";
                            savelog.RowCreatedTime = DateTime.Now;
                            savelog.RowCreatedUser = currentUser.UserName;
                            savelog.Save();

                        }
                        else if (check.preAssignee == currentUser.UserName && (check.Status == "Escalated"))
                        {
                            // save follower
                            f.UserName = check.Assignee;
                            f.TicketID = check.TicketID;
                            f.Save();

                            check.EscalateQueue = check.preEscalateQueue;
                            check.preEscalateQueue = "";
                            check.Assignee = check.preAssignee;
                            check.preAssignee = "";
                            check.Status = "Accepted";
                            check.RowLastUpdatedTime = DateTime.Now;
                            check.RowLastUpdatedUser = currentUser.UserName;
                            check.Update(0);
                            total++;

                            //save logs
                            Deca_RT_Log savelog = new Deca_RT_Log();
                            savelog.TicketID = id;
                            savelog.Activites = currentUser.UserName + " accept assignee";
                            savelog.RowCreatedTime = DateTime.Now;
                            savelog.RowCreatedUser = currentUser.UserName;
                            savelog.Save();

                        }
                        else if (check.preAssignee == "" && Deca_RT_Queue.CheckUserInQueue(currentUser.UserName, check.preEscalateQueue) && (check.Status == "Escalated"))
                        {
                            // save follower
                            f.UserName = check.Assignee;
                            f.TicketID = check.TicketID;
                            f.Save();

                            check.EscalateQueue = check.preEscalateQueue;
                            check.preEscalateQueue = "";
                            check.Assignee = currentUser.UserName;
                            check.preAssignee = "";
                            check.Status = "Accepted";
                            check.RowLastUpdatedTime = DateTime.Now;
                            check.RowLastUpdatedUser = currentUser.UserName;
                            check.Update(0);
                            total++;

                            //save logs
                            Deca_RT_Log savelog = new Deca_RT_Log();
                            savelog.TicketID = id;
                            savelog.Activites = currentUser.UserName + " accept assignee";
                            savelog.RowCreatedTime = DateTime.Now;
                            savelog.RowCreatedUser = currentUser.UserName;
                            savelog.Save();
                        }
                    }
                    else
                    {
                        message = "Ticket không tồn tại";
                        count = 1;
                    }
                }
                return Json(new { error = count, success = total, message = message });
            }
            else
            {
                return Json(new { error = 1, success = 0, message = "Không có quyền cập nhật" });
            }
        }
        [HttpPost]
        public ActionResult Deny(string data, string denyreason)
        {
            if (asset.Update)
            {
                int count = 0;
                int total = 0;
                string message = "";
                string[] separators = { ";;" };
                var listdata = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if (denyreason == null || denyreason == "")
                {
                    return Json(new { error = 1, success = 0, message = "Please write deny reason" });
                }
                foreach (var id in listdata)
                {
                    var check = Deca_RT_Ticket.GetDeca_RT_Ticket(id);
                    if (check != null)
                    {

                        var check1 = Deca_RT_Ticket.GetDeca_RT_Ticket_checkintype(check.TypeID, currentUser.UserName);

                        // status New, có assign 1 user cụ thể
                        if (check.preAssignee != "" && check.Status == "New")
                        {
                            if (check.preAssignee == currentUser.UserName)
                            {
                                check.preAssignee = "";
                                // check.TypeID = "";
                                // check.Owner = "";
                                // check.Status = "Denied";
                                check.RowLastUpdatedTime = DateTime.Now;
                                check.RowLastUpdatedUser = currentUser.UserName;
                                check.LastActivities = currentUser.UserName + " từ chối nhận ticket lúc " + DateTime.Now.ToString("HH:mm dd/MM/yyyy") + ", lý do: " + denyreason;
                                check.Update(0);
                                total++;
                                //save logs

                                Deca_RT_Log savelog = new Deca_RT_Log();
                                savelog.TicketID = id;
                                savelog.Activites = currentUser.UserName + " deny assign";
                                savelog.RowCreatedTime = DateTime.Now;
                                savelog.RowCreatedUser = currentUser.UserName;
                                savelog.Save();
                                // save activities
                                Deca_RT_Activities acc = new Deca_RT_Activities();
                                acc.RowCreatedTime = DateTime.Now;
                                acc.RowCreatedUser = currentUser.UserName;
                                acc.TicketID = id;
                                acc.Activities = currentUser.UserName + " từ chối nhận ticket lúc " + DateTime.Now.ToString("HH:mm dd/MM/yyyy") + ", lý do: " + denyreason;
                                acc.Save();


                            }
                        }
                        // status New, ko assign 1 user cụ thể, user cùng type có thể accept
                        else if (check.Assignee == "" && (check.Status == "New") && check1 != null)
                        {
                            check.TypeID = "";
                            check.Owner = "";
                            check.Status = "Denied";
                            check.RowLastUpdatedTime = DateTime.Now;
                            check.RowLastUpdatedUser = currentUser.UserName;
                            check.LastActivities = currentUser.UserName + " từ chối nhận ticket lúc " + DateTime.Now.ToString("HH:mm dd/MM/yyyy") + ", lý do: " + denyreason;
                            check.Update(0);
                            total++;

                            //save logs

                            Deca_RT_Log savelog = new Deca_RT_Log();
                            savelog.TicketID = id;
                            savelog.Activites = currentUser.UserName + " deny assign";
                            savelog.RowCreatedTime = DateTime.Now;
                            savelog.RowCreatedUser = currentUser.UserName;
                            savelog.Save();
                            // save activities
                            Deca_RT_Activities acc = new Deca_RT_Activities();
                            acc.RowCreatedTime = DateTime.Now;
                            acc.RowCreatedUser = currentUser.UserName;
                            acc.TicketID = id;
                            acc.Activities = currentUser.UserName + " từ chối nhận ticket lúc " + DateTime.Now.ToString("HH:mm dd/MM/yyyy") + ", lý do: " + denyreason;
                            acc.Save();
                        }
                        else if (check.preAssignee == currentUser.UserName && (check.Status == "Assigned"))
                        {
                            check.preAssignee = "";
                            check.Status = "Denied-WIP";
                            check.RowLastUpdatedTime = DateTime.Now;
                            check.RowLastUpdatedUser = currentUser.UserName;
                            check.LastActivities = currentUser.UserName + " từ chối nhận ticket lúc " + DateTime.Now.ToString("HH:mm dd/MM/yyyy") + ", lý do: " + denyreason;
                            check.Update(0);
                            total++;

                            //save logs

                            Deca_RT_Log savelog = new Deca_RT_Log();
                            savelog.TicketID = id;
                            savelog.Activites = currentUser.UserName + " deny assign";
                            savelog.RowCreatedTime = DateTime.Now;
                            savelog.RowCreatedUser = currentUser.UserName;
                            savelog.Save();
                            // save activities
                            Deca_RT_Activities acc = new Deca_RT_Activities();
                            acc.RowCreatedTime = DateTime.Now;
                            acc.RowCreatedUser = currentUser.UserName;
                            acc.TicketID = id;
                            acc.Activities = "Deny reason: " + denyreason;
                            acc.Save();
                        }
                        else if (check.preAssignee == currentUser.UserName && (check.Status == "Escalated"))
                        {
                            check.preEscalateQueue = "";
                            check.preAssignee = "";
                            check.Status = "Denied-WIP";
                            check.RowLastUpdatedTime = DateTime.Now;
                            check.RowLastUpdatedUser = currentUser.UserName;
                            check.LastActivities = currentUser.UserName + " từ chối nhận ticket lúc " + DateTime.Now.ToString("HH:mm dd/MM/yyyy") + ", lý do: " + denyreason;
                            check.Update(0);
                            total++;

                            //save logs

                            Deca_RT_Log savelog = new Deca_RT_Log();
                            savelog.TicketID = id;
                            savelog.Activites = currentUser.UserName + " deny assign";
                            savelog.RowCreatedTime = DateTime.Now;
                            savelog.RowCreatedUser = currentUser.UserName;
                            savelog.Save();
                            // save activities
                            Deca_RT_Activities acc = new Deca_RT_Activities();
                            acc.RowCreatedTime = DateTime.Now;
                            acc.RowCreatedUser = currentUser.UserName;
                            acc.TicketID = id;
                            acc.Activities = currentUser.UserName + " từ chối nhận ticket lúc " + DateTime.Now.ToString("HH:mm dd/MM/yyyy") + ", lý do: " + denyreason;
                            acc.Save();

                        }
                        else if (check.preAssignee == "" && Deca_RT_Queue.CheckUserInQueue(currentUser.UserName, check.preEscalateQueue) && (check.Status == "Escalated"))
                        {
                            check.preEscalateQueue = "";
                            check.preAssignee = "";
                            check.Status = "Denied-WIP";
                            check.RowLastUpdatedTime = DateTime.Now;
                            check.RowLastUpdatedUser = currentUser.UserName;
                            check.LastActivities = currentUser.UserName + " từ chối nhận ticket lúc " + DateTime.Now.ToString("HH:mm dd/MM/yyyy") + ", lý do: " + denyreason;
                            check.Update(0);
                            total++;

                            //save logs

                            Deca_RT_Log savelog = new Deca_RT_Log();
                            savelog.TicketID = id;
                            savelog.Activites = currentUser.UserName + " deny assign";
                            savelog.RowCreatedTime = DateTime.Now;
                            savelog.RowCreatedUser = currentUser.UserName;
                            savelog.Save();
                            // save activities
                            Deca_RT_Activities acc = new Deca_RT_Activities();
                            acc.RowCreatedTime = DateTime.Now;
                            acc.RowCreatedUser = currentUser.UserName;
                            acc.TicketID = id;
                            acc.Activities = currentUser.UserName + " từ chối nhận ticket lúc " + DateTime.Now.ToString("HH:mm dd/MM/yyyy") + ", lý do: " + denyreason;
                            acc.Save();
                        }
                        else if (check.Assignee == currentUser.UserName && (check.Status == "Denied-WIP"))
                        {
                            check.preEscalateQueue = "";
                            check.preAssignee = "";
                            check.EscalateQueue = "";
                            check.Assignee = "";
                            check.TypeID = "";
                            check.Owner = "";
                            check.Status = "Denied";
                            check.RowLastUpdatedTime = DateTime.Now;
                            check.RowLastUpdatedUser = currentUser.UserName;
                            check.LastActivities = currentUser.UserName + " từ chối nhận ticket lúc " + DateTime.Now.ToString("HH:mm dd/MM/yyyy") + ", lý do: " + denyreason;
                            check.Update(0);
                            total++;

                            //save logs

                            Deca_RT_Log savelog = new Deca_RT_Log();
                            savelog.TicketID = id;
                            savelog.Activites = currentUser.UserName + " deny assign";
                            savelog.RowCreatedTime = DateTime.Now;
                            savelog.RowCreatedUser = currentUser.UserName;
                            savelog.Save();
                            // save activities
                            Deca_RT_Activities acc = new Deca_RT_Activities();
                            acc.RowCreatedTime = DateTime.Now;
                            acc.RowCreatedUser = currentUser.UserName;
                            acc.TicketID = id;
                            acc.Activities = currentUser.UserName + " từ chối nhận ticket lúc " + DateTime.Now.ToString("HH:mm dd/MM/yyyy") + ", lý do: " + denyreason;
                            acc.Save();
                        }
                    }
                    else
                    {
                        message = "Ticket không tồn tại";
                        count = 1;
                    }
                }
                return Json(new { error = count, success = total, message = message });
            }
            else
            {
                return Json(new { error = 1, success = 0, message = "Không có quyền cập nhật" });
            }
        }


        [HttpPost]
        public ActionResult Assign(string data, string UserName, string typeid)
        {
            if (asset.Update)
            {
                int count = 0;
                int total = 0;
                string message = "";
                string[] separators = { ";;" };
                var listdata = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                var checktype = Deca_RT_Type.GetDeca_RT_Type(typeid);
                if (checktype == null)
                {
                    return Json(new { error = 1, success = 0, message = "This Type not exist" });
                }
                //var listuser = Deca_RT_Type.GetListUserInType(typeid);
                //if (listuser.Where(s => s.UserName == currentUser.UserName).Count() == 0)
                //{
                //    return Json(new { error = 1, success = 0, message = "User not valid" });
                //}
                foreach (var id in listdata)
                {
                    var check = Deca_RT_Ticket.GetDeca_RT_Ticket(id);

                    if (check != null)
                    {
                        //kiem tra phan quyen
                        if ((check.Assignee == currentUser.UserName || checktype.Owner == currentUser.UserName || Deca_RT_RoleUpdate.Check(currentUser.UserName, check.TypeID)) && (check.Status == "Accepted" || check.Status == "WIP" || check.Status == "Reopen" || check.Status == "Denied-WIP" || check.Status == "Escalated" || check.Status == "Assigned"))
                        {
                            try
                            {
                                check.preAssignee = UserName;
                                check.Status = "Assigned";
                                check.RowLastUpdatedTime = DateTime.Now;
                                check.RowLastUpdatedUser = currentUser.UserName;
                                check.Update(0);
                                total++;

                                //save logs

                                Deca_RT_Log savelog = new Deca_RT_Log();
                                savelog.TicketID = id;
                                savelog.Activites = currentUser.UserName + " assignee to : " + check.preAssignee;
                                savelog.RowCreatedTime = DateTime.Now;
                                savelog.RowCreatedUser = currentUser.UserName;
                                savelog.Save();
                            }
                            catch
                            {
                                count = 1;
                            }
                        }
                        else if (checktype.Owner == currentUser.UserName && (check.Status == "New"))
                        {
                            try
                            {
                                check.preAssignee = UserName;
                                check.Status = "Assigned";
                                check.RowLastUpdatedTime = DateTime.Now;
                                check.RowLastUpdatedUser = currentUser.UserName;
                                check.Update(0);
                                total++;

                                //save logs

                                Deca_RT_Log savelog = new Deca_RT_Log();
                                savelog.TicketID = id;
                                savelog.Activites = currentUser.UserName + " assignee to : " + check.preAssignee;
                                savelog.RowCreatedTime = DateTime.Now;
                                savelog.RowCreatedUser = currentUser.UserName;
                                savelog.Save();
                            }
                            catch
                            {
                                count = 1;
                            }
                        }
                    }
                    else
                    {
                        message = "Ticket không tồn tại";
                        count = 1;
                    }
                }

                return Json(new { error = count, success = total, message = message });
            }
            else
            {
                return Json(new { error = 1, success = 0, message = "Không có quyền cập nhật" });
            }
        }


        [HttpPost]
        public ActionResult Escalate(string data, string UserName, string QueueID)
        {
            if (asset.Update)
            {
                int count = 0;
                int total = 0;
                string message = "";
                string[] separators = { ";;" };
                var listdata = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                var checkqueue = Deca_RT_Queue.GetDeca_RT_Queue(QueueID);
                if (UserName != "" && Deca_RT_Queue.CheckUserInQueue(UserName, QueueID) == false)
                {
                    return Json(new { error = 1, success = 0, message = "User not valid" });
                }
                foreach (var id in listdata)
                {
                    var check = Deca_RT_Ticket.GetDeca_RT_Ticket(id);

                    if (check != null)
                    {
                        //kiem tra phan quyen
                        if ((check.Assignee == currentUser.UserName || check.Owner == currentUser.UserName || Deca_RT_RoleUpdate.Check(currentUser.UserName, check.TypeID)) && (check.Status == "Accepted" || check.Status == "WIP" || check.Status == "Reopen" || check.Status == "Denied-WIP" || check.Status == "Escalated" || check.Status == "Assigned"))
                        {
                            check.preEscalateQueue = QueueID;
                            check.preAssignee = UserName;
                            check.Status = "Escalated";
                            check.RowLastUpdatedTime = DateTime.Now;
                            check.RowLastUpdatedUser = currentUser.UserName;
                            check.Update(0);

                            //save logs

                            Deca_RT_Log savelog = new Deca_RT_Log();
                            savelog.TicketID = id;
                            savelog.Activites = currentUser.UserName + " Escalate to : " + UserName + " ( " + checkqueue.QueueName + " ) ";
                            savelog.RowCreatedTime = DateTime.Now;
                            savelog.RowCreatedUser = currentUser.UserName;
                            savelog.Save();
                            total++;
                        }
                        else
                        {
                            message = "Không có quyền chuyển nhóm ticket";
                            count = 1;
                        }
                    }
                }

                return Json(new { error = count, success = total, message = message });
            }
            else
            {
                return Json(new { error = 1, success = 0, message = "Không có quyền cập nhật" });
            }
        }

        [HttpPost]
        public ActionResult ChangeFollower(string TicketID, string Follower)
        {
            try
            {
                var check = Deca_RT_Ticket.GetDeca_RT_Ticket(TicketID);
                if (asset.Update && check != null && (check.Assignee == currentUser.UserName || check.Owner == currentUser.UserName || check.Requestor == currentUser.UserName))
                {
                    Deca_RT_Follower.DeleteAll(TicketID);
                    if (!string.IsNullOrEmpty(Follower))
                    {
                        for (int i = 0; i < Follower.Split(',').Length; i++)
                        {
                            Deca_RT_Follower f = new Deca_RT_Follower();
                            f.TicketID = TicketID;
                            f.RowCreatedTime = DateTime.Now;
                            f.RowCreatedUser = currentUser.UserName;
                            f.UserName = Follower.Split(',')[i];
                            f.Save();
                        }
                    }
                    return Json(new { success = true, message = "true" });
                }
                else
                {
                    return Json(new { success = false, message = "Không có quyền cập nhật." });

                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }


        ///////// file attach
        public virtual ActionResult Download(string id, string file)
        {
            if (asset.Update)
            {
                var check = Deca_RT_Ticket.GetDeca_RT_Ticket(id);
                //   var check2 = Deca_RT_Ticket.GetDeca_RT_Ticket_checkcanview(id, currentUser.UserName, "", currentUser.Team);
                if (checkrole("ViewAll") == true || Deca_RT_Queue.CheckUserInQueue(currentUser.UserName, check.EscalateQueue) || Deca_RT_Queue.CheckUserInQueue(currentUser.UserName, check.preEscalateQueue))
                {
                    string fullPath = Path.Combine(Server.MapPath("~/UploadFileTicket/"), id + "/" + file);
                    if (System.IO.File.Exists(fullPath))
                        return File(fullPath, "application/vnd.ms-excel", file);
                    else
                        return null;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

        }
        public ActionResult getListFile([DataSourceRequest] DataSourceRequest request, string id)
        {
            if (asset.Update)
            {
                try
                {
                    string pathForSaving = Server.MapPath("~/UploadFileTicket/" + id);
                    var list = new List<uFile>();
                    var totalfile = Directory.GetFiles(pathForSaving);
                    foreach (var i in totalfile)
                    {
                        list.Add(new uFile(i.ToString().Split('\\').Last(), id));
                    }

                    return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new { success = false, error = e }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        private bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception e)
                {
                    /*TODO: You must process this exception.*/
                    result = false;
                }
            }
            return result;
        }

        public ActionResult DeleteFile(string filename, string id)
        {
            if (asset.Update)
            {

                try
                {
                    var check = Deca_RT_Ticket.GetDeca_RT_Ticket(id);
                    if (check != null && (check.Status != "Closed" && check.Status != "Resolved") && (check.Assignee == currentUser.UserName || check.Requestor == currentUser.UserName || check.Owner == currentUser.UserName))
                    {
                        string fileLocation = Server.MapPath("~/UploadFileTicket/" + id + "/" + filename);

                        if (System.IO.File.Exists(fileLocation))
                            System.IO.File.Delete(fileLocation);
                        //save logs

                        Deca_RT_Log savelog = new Deca_RT_Log();
                        savelog.TicketID = id;
                        savelog.Activites = currentUser.UserName + " delete file " + filename;
                        savelog.RowCreatedTime = DateTime.Now;
                        savelog.RowCreatedUser = currentUser.UserName;
                        savelog.Save();


                        return Json(new { success = true });
                    }
                    return Json(new { success = false, error = "You don't have permission to delete file" });
                }
                catch (Exception e)
                {
                    return Json(new { success = false, error = e });
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }



        public ActionResult uploadfile(string id)
        {
            if (asset.Update)
            {
                HttpPostedFileBase fileU = Request.Files[0];
                try
                {
                    if (fileU != null && fileU.ContentLength > 0)
                    {
                        var check = Deca_RT_Ticket.GetDeca_RT_Ticket(id);
                        if (check != null && (check.Status != "Closed" && check.Status != "Resolved") && (check.Assignee == currentUser.UserName || check.Requestor == currentUser.UserName || check.Owner == currentUser.UserName))
                        {
                            if (fileU != null && fileU.ContentLength > 0)
                            {
                                string pathForSaving = Server.MapPath("~/UploadFileTicket/" + id);
                                if (this.CreateFolderIfNeeded(pathForSaving))
                                {
                                    try
                                    {
                                        if (fileU.ContentLength > 5242880)
                                        {
                                            return Json(new { success = false, error = "File size Maximum 5M" });
                                        }
                                        var totalfile = Directory.GetFiles(pathForSaving).Count();
                                        if (totalfile == 5)
                                        {
                                            return Json(new { success = false, error = "Maximum 5 files" });
                                        }

                                        var filename = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + Locdau.ConvertFileName(fileU.FileName);
                                        fileU.SaveAs(Path.Combine(pathForSaving, filename));


                                        //save logs

                                        Deca_RT_Log savelog = new Deca_RT_Log();
                                        savelog.TicketID = id;
                                        savelog.Activites = currentUser.UserName + "upload file " + Locdau.ConvertFileName(fileU.FileName);
                                        savelog.RowCreatedTime = DateTime.Now;
                                        savelog.RowCreatedUser = currentUser.UserName;
                                        savelog.Save();

                                        return Json(new { success = true });
                                    }
                                    catch (Exception e)
                                    {
                                        return Json(new { success = false, error = e });
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        return Json(new { success = false, error = "Please choose file" });
                    }
                    return Json(new { success = false, error = "You don't have permission to upload file" });
                }
                catch (Exception e)
                {
                    return Json(new { success = false });
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        //////////////////////////



        public ActionResult Follower_Read(string ticketid)
        {
            try
            {
                if (asset.View)
                {
                    var w = Deca_RT_Follower.GetAllDeca_RT_Followers(ticketid);
                    if (w != null)
                    {
                        return Json(new { success = true, dataFollower = w });
                    }
                    else
                    {
                        return Json(new { success = false, dataFollower = "" });
                    }
                }
                else
                {
                    return RedirectToAction("NoAccessRights", "Error");
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, data = e });
            }
        }

        public ActionResult Follower_Update(string ticketid, string listfollower)
        {

            if (asset.Update)
            {
                if (ticketid != null)
                {
                    var check = Deca_RT_Ticket.GetDeca_RT_Ticket(ticketid);
                    if (check != null && (check.Assignee == currentUser.UserName || check.Owner == currentUser.UserName || check.Requestor == currentUser.UserName))
                    {
                        Deca_RT_Follower.DeleteAll(ticketid);
                        string[] separators = { ";;" };
                        var listRowID = listfollower.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        Deca_RT_Follower f = new Deca_RT_Follower();
                        f.TicketID = ticketid;
                        f.RowCreatedTime = DateTime.Now;
                        f.RowCreatedUser = currentUser.UserName;

                        foreach (var item in listRowID)
                        {
                            f.UserName = item;
                            f.Save();
                        }
                        return Json(new { success = true });
                    }
                }
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = false });
            }

        }

        public FileResult Export([DataSourceRequest] DataSourceRequest request, string option)
        {
            if (asset.Export)
            {

                //using (ExcelPackage excelPkg = new ExcelPackage())
                FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_RT_Ticket Master.xlsx"));
                var excelPkg = new ExcelPackage(fileInfo);
                ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["Ticket Master"];

                var listType = Deca_RT_Type.GetAllDeca_RT_Types().OrderBy(s => s.Name);
                var listPriority = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='Ticket Priority'", "").OrderBy(s => s.CodeID);
                var listImpact = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='Ticket Impact'", "").OrderBy(s => s.CodeID);
                var listDepartment = Deca_Department.GetAllDeca_Departments();
                var listTeam = Deca_Team.GetAllDeca_Teams();
                var listGender = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='Gender'", "").OrderBy(s => s.CodeID);
                // var listPosition = Deca_Position.GetAllDeca_Positions();

                int i = 1;
                if (option == null || option == "")
                {
                    IEnumerable datas;
                    var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "data.");
                    request.PageSize = 1000000;
                    request.Page = 1;
                    if (checkrole("ViewAll") == true)
                    {
                        datas = Deca_RT_Ticket.GetAllDeca_RT_Tickets_ViewAll_Dynamic(currentUser.UserName, where, request).Data;
                    }
                    else
                    {
                        datas = Deca_RT_Ticket.GetAllDeca_RT_Tickets_Dynamic(currentUser.UserName, where, request).Data;
                    }

                    int rowData = 1;
                    foreach (Deca_RT_Ticket data in datas)
                    {
                        i = 1;
                        rowData++;
                        dataSheet.Cells[rowData, i++].Value = data.TicketID;
                        dataSheet.Cells[rowData, i++].Value = data.Title;
                        dataSheet.Cells[rowData, i++].Value = data.Detail;
                        foreach (Deca_RT_Type item in listType)
                        {
                            if (item.TypeID == data.TypeID)
                            {
                                dataSheet.Cells[rowData, i].Value = item.Name;
                                break;
                            }
                        }
                        i++;
                        foreach (Deca_Code_Master item in listPriority)
                        {
                            if (item.CodeID == data.Priority)
                            {
                                dataSheet.Cells[rowData, i].Value = item.Description;
                                break;
                            }
                        }
                        i++;
                        foreach (Deca_Code_Master item in listImpact)
                        {
                            if (item.CodeID == data.Impact)
                            {
                                dataSheet.Cells[rowData, i].Value = item.Description;
                                break;
                            }
                        }
                        i++;
                        dataSheet.Cells[rowData, i++].Value = data.CustomerID;
                        dataSheet.Cells[rowData, i++].Value = data.OrderID;
                        dataSheet.Cells[rowData, i++].Value = data.Status;
                        dataSheet.Cells[rowData, i++].Value = data.LastActivities;
                        dataSheet.Cells[rowData, i++].Value = data.LastComment;
                        dataSheet.Cells[rowData, i++].Value = data.Assignee;
                        dataSheet.Cells[rowData, i++].Value = data.Owner;
                        dataSheet.Cells[rowData, i++].Value = data.Requestor;
                        //Thời gian KH y/c
                        if (data.CustomerExpectationTimeLine.Year < 2000)
                        {
                            dataSheet.Cells[rowData, i++].Value = "";
                        }
                        else
                        {
                            dataSheet.Cells[rowData, i++].Value = data.CustomerExpectationTimeLine;
                        }
                        //Thời gian đã qua (p)
                        dataSheet.Cells[rowData, i++].Value = data.PassedTime;
                        //Hạn phản hồi
                        if (data.ResponseDeadline.ToString("dd/MM/yyyy") != "01/01/1900")
                        {
                            dataSheet.Cells[rowData, i++].Value = data.ResponseDeadline;
                        }
                        else
                        {
                            dataSheet.Cells[rowData, i++].Value = "";
                        }
                        //Hạn xử lý
                        if (data.ResolveDeadline.ToString("dd/MM/yyyy") != "01/01/1900")
                        {
                            dataSheet.Cells[rowData, i++].Value = data.ResolveDeadline;
                        }
                        else
                        {
                            dataSheet.Cells[rowData, i++].Value = "";
                        }
                        //Hạn đóng y/c
                        if (data.CloseDeadline.ToString("dd/MM/yyyy") != "01/01/1900")
                        {
                            dataSheet.Cells[rowData, i++].Value = data.CloseDeadline;
                        }
                        else
                        {
                            dataSheet.Cells[rowData, i++].Value = "";
                        }
                        //Ngày phản hồi
                        if (data.ResponseDate.ToString("dd/MM/yyyy") != "01/01/1900")
                        {
                            dataSheet.Cells[rowData, i++].Value = data.ResponseDate;
                        }
                        else
                        {
                            dataSheet.Cells[rowData, i++].Value = "";
                        }
                        //Ngày xử lý
                        if (data.ResolveDate.ToString("dd/MM/yyyy") != "01/01/1900")
                        {
                            dataSheet.Cells[rowData, i++].Value = data.ResolveDate;
                        }
                        else
                        {
                            dataSheet.Cells[rowData, i++].Value = "";
                        }
                        //Ngày đóng y/c
                        if (data.CloseDate.ToString("dd/MM/yyyy") != "01/01/1900")
                        {
                            dataSheet.Cells[rowData, i++].Value = data.CloseDate;
                        }
                        else
                        {
                            dataSheet.Cells[rowData, i++].Value = "";
                        }
                        //Tạo lúc
                        if (data.RowCreatedTime.ToString("dd/MM/yyyy") != "01/01/1900")
                        {
                            dataSheet.Cells[rowData, i++].Value = data.RowCreatedTime;
                        }
                        else
                        {
                            dataSheet.Cells[rowData, i++].Value = "";
                        }
                        //Cập nhật lúc
                        if (data.RowLastUpdatedTime.ToString("dd/MM/yyyy") != "01/01/1900")
                        {
                            dataSheet.Cells[rowData, i++].Value = data.RowLastUpdatedTime;
                        }
                        else
                        {
                            dataSheet.Cells[rowData, i++].Value = "";
                        }
                        //Cập nhật bởi
                        dataSheet.Cells[rowData, i++].Value = data.RowLastUpdatedUser;
                        //Tên người yêu cầu
                        dataSheet.Cells[rowData, i++].Value = data.RName;
                        //Email người yêu cầu
                        dataSheet.Cells[rowData, i++].Value = data.REmail;
                        //Phòng ban
                        foreach (Deca_Department item in listDepartment)
                        {
                            if (item.DepartmentID.ToString() == data.RDepartmentID)
                            {
                                dataSheet.Cells[rowData, i].Value = item.Department;
                                break;
                            }
                        }
                        i++;
                        //Team
                        foreach (Deca_Team item in listTeam)
                        {
                            if (item.TeamID.ToString() == data.RTeam)
                            {
                                dataSheet.Cells[rowData, i].Value = item.TeamName;
                                break;
                            }
                        }
                        i++;
                        //Điện thoại
                        dataSheet.Cells[rowData, i++].Value = data.RPhone;
                        //Phân công cho
                        dataSheet.Cells[rowData, i++].Value = data.preAssignee;
                        //EscalateQueue
                        dataSheet.Cells[rowData, i++].Value = data.EscalateQueue;
                        //EscalateQueueTo
                        dataSheet.Cells[rowData, i++].Value = data.preEscalateQueue;
                    }
                }


                i = 2;
                ExcelWorksheet dataSheet2 = excelPkg.Workbook.Worksheets["List1"];
                foreach (Deca_RT_Type data in listType)
                {
                    dataSheet2.Cells[i++, 1].Value = data.Name;
                }

                i = 2;
                ExcelWorksheet dataSheet3 = excelPkg.Workbook.Worksheets["List2"];
                foreach (Deca_Code_Master data in listPriority)
                {
                    dataSheet3.Cells[i++, 1].Value = data.Description;
                }

                i = 2;
                ExcelWorksheet dataSheet4 = excelPkg.Workbook.Worksheets["List3"];
                foreach (Deca_Code_Master data in listImpact)
                {
                    dataSheet4.Cells[i++, 1].Value = data.Description;
                }

                //Write the workbook to a memory stream
                MemoryStream output = new MemoryStream();
                excelPkg.SaveAs(output);
                //Return the result to the end user
                return File(output.ToArray(), //The binary data of the XLS file
                    "application/vnd.ms-excel", //MIME type of Excel files
                    "Deca_RT_Ticket Master_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
            }
            else
            {
                return File("", //The binary data of the XLS file
                    "application/vnd.ms-excel", //MIME type of Excel files
                    "Deca_RT_Ticket Master_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");     //Suggested file name in the "Save as" dialog which will be displayed to the end user
            }
        }


        // import 
        [HttpGet]
        public virtual ActionResult DownloadError(string file)
        {
            if (asset.Export)
            {
                string fullPath = Path.Combine(Server.MapPath("~/Excel"), "Deca_RT_Ticket Master Error_" + file);
                return File(fullPath, "application/vnd.ms-excel", "Deca_RT_Ticket Master Error_" + file);
            }
            return RedirectToAction("NoAccessRights", "Error");
        }


        [HttpPost]
        public ActionResult Ticket_Import()
        {
            if (asset.Update)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        List<Deca_RT_Ticket> listData = new List<Deca_RT_Ticket>();
                        int total = 0;


                        /////create excel error file
                        string filename = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";

                        int rowData = 1;


                        FileInfo fileInfo = new FileInfo(Server.MapPath(@"~\ExportExcelFile\Deca_RT_Ticket Master.xlsx"));
                        var excelPkg = new ExcelPackage(fileInfo);
                        ExcelWorksheet dataSheet = excelPkg.Workbook.Worksheets["Ticket Master"];

                        string ErrorfileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "Deca_RT_Ticket Master Error_" + filename);
                        FileStream fileStream = new FileStream(ErrorfileLocation, FileMode.Create, FileAccess.Write);


                        if (Request.Files["FileUpload"] != null && Request.Files["fileUpload"].ContentLength > 0)
                        {
                            string fileExtension =
                                System.IO.Path.GetExtension(Request.Files["fileUpload"].FileName);

                            if (fileExtension == ".xls" || fileExtension == ".xlsx")
                            {
                                // Create a folder in App_Data named ExcelFiles because you need to save the file temporarily location and getting data from there. 
                                string fileLocation = string.Format("{0}/{1}", Server.MapPath("~/Excel"), "[" + currentUser.UserName + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "]" + Request.Files["fileUpload"].FileName);
                                if (System.IO.File.Exists(fileLocation))
                                    System.IO.File.Delete(fileLocation);
                                Request.Files["fileUpload"].SaveAs(fileLocation);
                                string excelConnectionString = string.Empty;
                                excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";

                                OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                                excelConnection.Open();
                                DataTable dt = new DataTable();
                                dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                if (dt == null)
                                {
                                    return null;
                                }
                                String[] excelSheets = new String[dt.Rows.Count];

                                OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);
                                DataSet ds = new DataSet();
                                string query = "Select * from ['Ticket Master$']";
                                using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                                {
                                    dataAdapter.Fill(ds);
                                }
                                var exist_Title = false;
                                var exist_Detail = false;
                                var exist_Type = false;
                                var exist_Priority = false;
                                var exist_Impact = false;
                                var exist_OrganizationID = false;
                                var exist_CustomerID = false;

                                foreach (var item in ds.Tables[0].Columns)
                                {

                                    if (item.ToString() == "Title")
                                    {
                                        exist_Title = true;
                                    }
                                    if (item.ToString() == "Detail")
                                    {
                                        exist_Detail = true;
                                    }
                                    if (item.ToString() == "Type")
                                    {
                                        exist_Type = true;
                                    }
                                    if (item.ToString() == "Priority")
                                    {
                                        exist_Priority = true;
                                    }
                                    if (item.ToString() == "Impact")
                                    {
                                        exist_Impact = true;
                                    }
                                    if (item.ToString() == "OrganizationID")
                                    {
                                        exist_OrganizationID = true;
                                    }
                                    if (item.ToString() == "CustomerID")
                                    {
                                        exist_CustomerID = true;
                                    }
                                }
                                if (exist_Title && exist_Detail && exist_Type && exist_Priority && exist_Impact && exist_OrganizationID && exist_CustomerID)
                                {

                                    var listPriority = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='Ticket Priority'", "").OrderBy(s => s.CodeID);
                                    var listImpact = Deca_Code_Master.GetDeca_Code_Masters("[CodeType]='Ticket Impact'", "").OrderBy(s => s.CodeID);
                                    var listType = Deca_RT_Type.GetAllDeca_RT_Types();
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {

                                        int err1 = 0;
                                        Deca_RT_Ticket icare = new Deca_RT_Ticket();
                                        Deca_RT_Ticket icareerr = new Deca_RT_Ticket();
                                        icare.Title = icareerr.Title = ds.Tables[0].Rows[i]["Title"].ToString();
                                        icare.Detail = icareerr.Detail = ds.Tables[0].Rows[i]["Detail"].ToString();
                                        var TypeID = ds.Tables[0].Rows[i]["Type"].ToString();
                                        var OrganizationID = ds.Tables[0].Rows[i]["OrganizationID"].ToString();
                                        var CustomerID = ds.Tables[0].Rows[i]["CustomerID"].ToString();
                                        var Priority = ds.Tables[0].Rows[i]["Priority"].ToString();
                                        var Impact = ds.Tables[0].Rows[i]["Impact"].ToString();
                                        var type = listType.Where(s => s.Name == TypeID).FirstOrDefault();
                                        if (type == null)
                                        {
                                            icareerr.ImportNote = "This Type not exist";
                                            listData.Add(icareerr);
                                            err1 = 1;
                                        }
                                        else
                                        {
                                            TypeID = type.TypeID;
                                        }

                                        var pri = listPriority.Where(s => s.Description == Priority).FirstOrDefault();
                                        if (pri == null)
                                        {
                                            Priority = "TPRI002";
                                        }
                                        else
                                        {
                                            Priority = pri.CodeID;
                                        }

                                        var imp = listImpact.Where(s => s.Description == Impact).FirstOrDefault();
                                        if (imp == null)
                                        {
                                            Impact = "TIMP002";
                                        }
                                        else
                                        {
                                            Impact = imp.CodeID;
                                        }

                                        var checktype = Deca_RT_Type.GetDeca_RT_Type(TypeID);
                                        if (err1 == 0)
                                        {
                                            if (checktype.RequireCustomerID == "True" && (CustomerID == null || CustomerID == ""))
                                            {
                                                icareerr.ImportNote = "This Type need CustomerID";
                                                listData.Add(icareerr);
                                                err1 = 1;
                                            }
                                        }
                                        //if (err1 == 0)
                                        //{
                                        //    var checkcus = DW_Deca_Customer.CheckCustomerID(CustomerID.Trim());
                                        //    if (checkcus != null)
                                        //    {
                                        //        CustomerID = checkcus.CustomerID;
                                        //    }
                                        //}

                                        if (err1 == 0)
                                        {
                                            icare.Status = "New";
                                            icare.TypeID = TypeID;
                                            icare.Requestor = currentUser.UserName;
                                            icare.Assignee = "";
                                            icare.preAssignee = "";
                                            icare.Owner = checktype.Owner;
                                            icare.OrganizationID = OrganizationID;
                                            icare.CustomerID = CustomerID;
                                            icare.Priority = Priority;
                                            icare.Impact = Impact;
                                            icare.RowCreatedUser = currentUser.UserName;
                                            icare.RowCreatedTime = DateTime.Now;
                                            var id = icare.Save();
                                            //save logs

                                            Deca_RT_Log savelog = new Deca_RT_Log();
                                            savelog.TicketID = id;
                                            savelog.Activites = "Add New Ticket";
                                            savelog.RowCreatedTime = DateTime.Now;
                                            savelog.RowCreatedUser = currentUser.UserName;
                                            savelog.Save();
                                        }
                                        if (err1 == 1)
                                        {
                                            rowData++;
                                            dataSheet.Cells[rowData, 2].Value = ds.Tables[0].Rows[i]["Title"].ToString();
                                            dataSheet.Cells[rowData, 3].Value = ds.Tables[0].Rows[i]["Detail"].ToString();
                                            dataSheet.Cells[rowData, 4].Value = ds.Tables[0].Rows[i]["Type"].ToString();
                                            dataSheet.Cells[rowData, 5].Value = ds.Tables[0].Rows[i]["Priority"].ToString();
                                            dataSheet.Cells[rowData, 6].Value = ds.Tables[0].Rows[i]["Impact"].ToString();
                                            dataSheet.Cells[rowData, 7].Value = ds.Tables[0].Rows[i]["OrganizationID"].ToString();
                                            dataSheet.Cells[rowData, 8].Value = ds.Tables[0].Rows[i]["CustomerID"].ToString();
                                        }
                                        else
                                        {
                                            total++;
                                        }
                                    }
                                }
                                else
                                {
                                    return Json(new { success = false, message = "Please use Export Template" });
                                }

                                excelPkg.SaveAs(fileStream);
                                fileStream.Close();
                                excelConnection.Close();
                                excelConnection1.Close();
                            }
                            else
                            {
                                return Json(new { success = false, message = "Plese select Excel File." });
                            }
                        }
                        return Json(new { success = true, data = listData, total = total, link = filename });
                    }
                    else
                    {
                        return Json(new { success = false, message = "You don't havve permission import" });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false });
                }
            }
            else
            {
                return Json(new { success = false });
            }
        }

        public ActionResult LoadTicketIDSuggest(string TicketID)
        {
            List<Deca_RT_Ticket> data = new List<Deca_RT_Ticket>();
            var where = " TicketID LIKE N'%" + TicketID + "%'";
            data = Deca_RT_Ticket.GetDeca_RT_Tickets_Suggets_ViewAll(currentUser.UserName, where);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MergeTicket(string ToTicketID, string MergeTicket)
        {
            if (asset.Update)
            {
                var To = Deca_RT_Ticket.GetDeca_RT_Ticket(ToTicketID);
                var From = Deca_RT_Ticket.GetDeca_RT_Ticket(MergeTicket);
                if (From.Status == "Cancelled" || From.Status == "Closed")
                {
                    return (Json(new { success = false, message = "Ticket này đã đóng hoặc hủy." }));
                }
                if (To != null && From != null && (From.Assignee == currentUser.UserName || From.Owner == currentUser.UserName || From.Requestor == currentUser.UserName) && (To.Assignee == currentUser.UserName || To.Owner == currentUser.UserName || To.Requestor == currentUser.UserName))
                {
                    Deca_RT_Activities acc = new Deca_RT_Activities();
                    acc.RowCreatedTime = DateTime.Now;
                    acc.RowCreatedUser = currentUser.UserName;
                    acc.TicketID = To.TicketID;
                    acc.Activities = currentUser.UserName + " cập nhật lúc " + DateTime.Now.ToString("HH:mm dd/MM/yyyy") + " : " + From.Detail;
                    acc.Save();
                    To.LastActivities = currentUser.UserName + " cập nhật lúc " + DateTime.Now.ToString("HH:mm dd/MM/yyyy") + " : " + From.Detail;
                    To.RowLastUpdatedTime = DateTime.Now;
                    To.RowLastUpdatedUser = currentUser.UserName;
                    To.Update(0);

                    From.Status = "Cancelled";
                    From.RowLastUpdatedTime = DateTime.Now;
                    From.RowLastUpdatedUser = currentUser.UserName;
                    From.Update(0);
                    return (Json(new { success = true, message = "true" }));
                }
                else
                {
                    return (Json(new { success = false, message = "Không có quyền cập nhật ticket." }));
                }
            }
            else
            {
                return (Json(new { success = false, message = "Không có quyền cập nhật ticket." }));
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SendEmail(Deca_RT_Email model, HttpPostedFileBase[] FileUploads)
        {
            if (asset.Update && ModelState.IsValid)
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                var ticket = Deca_RT_Ticket.GetDeca_RT_Ticket(model.TicketID);
                if (ticket != null && (ticket.Assignee == currentUser.UserName || ticket.Owner == currentUser.UserName || ticket.Requestor == currentUser.UserName))
                {
                    using (var dbConn = OrmliteConnection.openConn())
                    using (var dbTrans = dbConn.OpenTransaction())
                    {
                        if (FileUploads.Length > 0)
                            foreach (var file in FileUploads)
                            {
                                if (file != null && file.ContentLength > 0)
                                {
                                    var fileName = Path.GetFileName(file.FileName);
                                    var path = Path.Combine(Server.MapPath("~/UploadFileTicket"), fileName);
                                    if (System.IO.File.Exists(path))
                                        System.IO.File.Delete(path);
                                    file.SaveAs(path);
                                    System.Net.Mail.Attachment attachment;
                                    attachment = new System.Net.Mail.Attachment(Server.MapPath("~/UploadFileTicket/" + fileName));
                                    mail.Attachments.Add(attachment);
                                }
                            }
                        SendMail.SendEmail(model.EmailTo, model.CCTo, model.Subject, model.EmailContent, mail.Attachments);
                        model.CreatedAt = DateTime.Now;
                        model.CreatedBy = currentUser.UserName;
                        dbConn.Insert(model);
                        Deca_RT_Activities acc = new Deca_RT_Activities();
                        acc.RowCreatedTime = DateTime.Now;
                        acc.RowCreatedUser = currentUser.UserName;
                        acc.TicketID = ticket.TicketID;
                        acc.Activities = currentUser.UserName + " cập nhật lúc " + DateTime.Now.ToString("HH:mm dd/MM/yyyy") + " : " + "Gửi email với tiêu đề:" + model.Subject;
                        acc.Save();
                        ticket.LastActivities = currentUser.UserName + " cập nhật lúc " + DateTime.Now.ToString("HH:mm dd/MM/yyyy") + " : " + "Gửi email với tiêu đề:" + model.Subject;
                        ticket.RowLastUpdatedTime = DateTime.Now;
                        ticket.RowLastUpdatedUser = currentUser.UserName;
                        ticket.Update(0);
                        dbTrans.Commit();
                    }
                    return Json(new { success = true, message = "" });

                }
                else
                {
                    return (Json(new { success = false, message = "Không có quyền cập nhật ticket." }));
                }
            }
            else
            {
                if (!asset.Update) return Json(new { success = false, message = "Không có quyền cập nhật." });
                string message = "";
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        message += error.ErrorMessage + " ";
                    }
                }
                return Json(new { success = false, message = message });
            }


        }
        public ActionResult GetDetailTicket(string TicketID)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                var ticket = Deca_RT_Ticket.GetDeca_RT_Ticket(TicketID);
                ticket.Requestor = dbConn.FirstOrDefault<Users>("UserName={0}", ticket.Requestor).Email;
                if (!string.IsNullOrEmpty(ticket.CustomerID) && ticket.CustomerID.Contains("@"))
                {
                    ticket.Requestor += ";" + ticket.CustomerID;
                }
                ticket.Follower = Deca_RT_Follower.GetAllDeca_RT_Followers(TicketID);
                List<string> strFollower = new List<string>();
                foreach (var follower in ticket.Follower)
                {

                    if (follower.UserName.Contains("@"))
                        strFollower.Add(follower.UserName);
                    else
                    {
                        try
                        {
                            strFollower.Add(dbConn.FirstOrDefault<Users>("UserName={0}", follower.UserName).Email);
                        }
                        catch
                        {
                            strFollower.Add(follower.UserName);
                        }
                    }
                }
                return Json(new { success = true, requestor = ticket.Requestor, follower = strFollower });
            }

        }
    }
}