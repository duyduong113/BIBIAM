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
using ERPAPD.Controllers;
using ServiceStack.OrmLite;
using System;

namespace ERPAPD.Controllers
{
    public class CRMTicketController : CustomController
    {
        // GET: CRMTicket
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
                using (var dbConn = OrmliteConnection.openConn())
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
                ViewBag.listType = CRM_Ticket_Type.GetAllCRM_Ticket_Types().Where(s => s.TypeID != "TIC0092");
                ViewBag.listQueue = CRM_Ticket_Queue.GetAllCRM_Ticket_Queues();
                ViewBag.listTypeActive = CRM_Ticket_Type.GetAllCRM_Ticket_Types().Where(s => s.Status == "True" && s.TypeID != "TIC0092").ToList();
                ViewBag.listCategory = CRM_Ticket_Category.GetAllDeca_RT_Categorys();
                ViewBag.currentuser = currentUser.UserName;
                using (var dbConn = OrmliteConnection.openConn())
                //ViewData["ListMerchant"] = dbConn.Select<DC_OCM_MerchantModelView>("SELECT PKMerchantID,MerchantName FROM DC_OCM_Merchant");
                //    ViewBag.listOrgSettlement = Deca_RT_Settlement.GetListOrganizationID().ToList();
                ViewBag.listTypeCanUpdateAll = Deca_RT_RoleUpdate.GetListType(currentUser.UserName);
              //  ViewBag.listDepartment = Deca_Department.GetAllDeca_Departments();
               // ViewBag.listTeam = Deca_Team.GetAllDeca_Teams();
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
        public string  Create_Ticket_From_CustomerSupport(CRM_Ticket_Task_Appointment req)
        {
                try
                {
                    if (!string.IsNullOrEmpty(req.Title))
                    {
                        var checktype = CRM_Ticket_Type.GetCRM_Ticket_Type("TIC0001");
                        var ticket = new CRM_Ticket();
                        ticket.Title = !string.IsNullOrEmpty(req.Title) ? req.Title.Trim() : "";
                        ticket.Detail = !string.IsNullOrEmpty(req.Description) ? req.Description.Trim() : "";
                        ticket.RequestFrom = !string.IsNullOrEmpty(req.Type) ? req.Type.Trim() : "";
                        ticket.CustomerID = !string.IsNullOrEmpty(req.CustomerID) ? req.CustomerID.Trim() : "";
                        ticket.Status = "New";
                        ticket.TypeID = "TIC0001";
                        ticket.Requestor = "administrator";
                        ticket.Priority = "TPRI002";
                        ticket.Impact = "TIMP002";
                        ticket.Owner = checktype.Owner;
                    //check recall
                    if (!ticket.ReCallTime.HasValue)
                        {
                            ticket.IsDone = true;
                        }
                        else
                        {
                            ticket.IsDone = false;
                            ticket.ReCallTime = req.RecallTime;
                        }

                        ticket.RowCreatedUser = "administrator";
                        ticket.RowCreatedTime = DateTime.Now;
                        var id = ticket.Save();
                    return id;
                    }
                    else
                    {
                    return null;
                    }
                }
                catch (Exception ex)
                {
                    return null ;
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
                var listgroupviewall = CRM_Ticket_Role.GetAllCRM_Ticket_Roles();
                if (currentUser.Groups.Where(s => listgroupviewall.Where(p => p.ID == s.Id).Count() > 0).Count() > 0)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        // CRUD Ticket
        public ActionResult Ticket_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                DataSourceResult data;
                var where = KendoApplyFilter.ApplyFilter(request.Filters[0], "data.");

                if (checkrole("ViewAll") == true)
                {
                    data = CRM_Ticket.GetAllCRM_Tickets_ViewAll_Dynamic(currentUser.UserName, where, request);
                }
                else
                {
                    data = CRM_Ticket.GetAllCRM_Tickets_Dynamic(currentUser.UserName, where, request);
                }

                string pathForSaving = Server.MapPath("~/UploadFileTicket/");
                foreach (CRM_Ticket i in data.Data)
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
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Ticket_Create([DataSourceRequest] DataSourceRequest request, CRM_Ticket row)
        {
            IEnumerable<CRM_Ticket> u = new CRM_Ticket[] { };
            if (asset.Create)
            {
                row.RowCreatedTime = DateTime.Now;
                row.RowLastUpdatedTime = DateTime.Now;
                row.OrganizationID = "";
                if (row != null && row.TypeID != null && row.Title != null && row.Detail != null && row.Priority != null && row.Impact != null)
                {
                    string message = "";
                    var w = new CRM_Ticket();
                    var checktype = CRM_Ticket_Type.GetCRM_Ticket_Type(row.TypeID);
                    //if (checktype.RequireCustomerID == "True" && (row.CustomerID == null || row.CustomerID == ""))
                    //{
                    //    ModelState.AddModelError("", "This Type need CustomerID");
                    //    return Json(u.ToDataSourceResult(request, ModelState));
                    //}
                    //if (checktype.RequireOrderID == "True" && (row.OrderID == null || row.OrderID == ""))
                    //{
                    //    ModelState.AddModelError("", "This Type need OrderID");
                    //    return Json(u.ToDataSourceResult(request, ModelState));
                    //}
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

                    CRM_Ticket_Follower.DeleteAll(id);
                    if (row.Follower != null)
                    {
                        for (int i = 0; i < row.Follower.Count; i++)
                        {
                            CRM_Ticket_Follower f = new CRM_Ticket_Follower();
                            f.TicketID = id;
                            f.RowCreatedTime = DateTime.Now;
                            f.RowCreatedUser = currentUser.UserName;
                           // f.UserName = Request.Form["Follower[" + i.ToString() + "][UserName]"];
                            f.UserName = row.Follower[i].UserName;
                            f.Save();
                        }
                    }

                    if (id != "")
                    {
                        //save logs

                        CRM_Ticket_Log savelog = new CRM_Ticket_Log();
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
        [HttpPost, ValidateInput(false)]
        public ActionResult Ticket_Update([DataSourceRequest] DataSourceRequest request, CRM_Ticket row)
        {

            IEnumerable<CRM_Ticket> u = new CRM_Ticket[] { };
            if (asset.Update)
            {
                if (row != null)
                {
                    CRM_Ticket w = CRM_Ticket.GetCRM_Ticket(row.TicketID);
                    if (w != null)
                    {
                        string message = "";
                        int haveupdate = 0;
                        // create savelog
                        CRM_Ticket_Log savelog = new CRM_Ticket_Log();
                        savelog.TicketID = row.TicketID;
                        savelog.RowCreatedTime = DateTime.Now;
                        savelog.RowCreatedUser = currentUser.UserName;
                        if (row.preAssignee == null)
                        {
                            row.preAssignee = "";
                        }

                        var checktype = CRM_Ticket_Type.GetCRM_Ticket_Type(row.TypeID);
                        //if (checktype.RequireCustomerID == "True" && (row.CustomerID == null || row.CustomerID == ""))
                        //{
                        //    ModelState.AddModelError("", "This Type need CustomerID");
                        //    return Json(u.ToDataSourceResult(request, ModelState));
                        //}

                        if (row.Follower != null)
                        {
                            CRM_Ticket_Follower.DeleteAll(row.TicketID);
                            for (int i = 0; i < row.Follower.Count; i++)
                            {
                                CRM_Ticket_Follower f = new CRM_Ticket_Follower();
                                f.TicketID = row.TicketID;
                                f.RowCreatedTime = DateTime.Now;
                                f.RowCreatedUser = currentUser.UserName;
                                f.UserName = row.Follower[i].UserName;
                                //f.UserName = Request.Form["Follower[" + i.ToString() + "][UserName]"];
                                f.Save();
                            }
                        }

                        var check1 = CRM_Ticket.GetCRM_Ticket_checkintype(w.TypeID, currentUser.UserName);
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
                        if (w.Status == "Escalated" && row.preAssignee != w.preAssignee && (row.Assignee == currentUser.UserName || CRM_Ticket_RoleUpdate.Check(currentUser.UserName, row.TypeID)))
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
                        if (w.Status == "Assigned" && row.preAssignee != w.preAssignee && (row.Assignee == currentUser.UserName || CRM_Ticket_RoleUpdate.Check(currentUser.UserName, row.TypeID)))
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
                        if ((w.Status == "WIP" || w.Status == "Reopen" || w.Status == "Accepted" || w.Status == "Denied-WIP") && row.preAssignee != "" && (row.Assignee == currentUser.UserName || CRM_Ticket_RoleUpdate.Check(currentUser.UserName, row.TypeID)))
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

        public ActionResult Ticket_CreateFromCustomerSupport()
        {
            if (asset.Create)
            {
                try
                {
                    var title = Request.Form["title-ticket"];
                    var detail = Request.Form["detail-ticket"];
                    var felling = Request.Form["behaviour-ticket"];
                    var trend = Request.Form["vocation-ticket"];
                    var categorie = Request.Form["categorie-ticket"];
                    var type = Request.Form["type-ticket"];
                    var dateCallBack = Request.Form["date-call-back"];
                    var requestFrom = Request.Form["RequestFrom"];
                    var status = Request.Form["Status"];
                    var customerid = Request.Form["CustomerID"];
                    if (string.IsNullOrEmpty(requestFrom))
                    {
                        return Json(new { success = false, message = "Bạn cần chọn nguồn yêu cầu." });
                    }

                    if (!string.IsNullOrEmpty(customerid) && !string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(detail) && !string.IsNullOrEmpty(categorie) && !string.IsNullOrEmpty(type))
                    {
                        var checktype = CRM_Ticket_Type.GetCRM_Ticket_Type(type);

                        var ticket = new CRM_Ticket();
                        ticket.Title = title;
                        ticket.Detail = detail;
                        ticket.Status = status;
                        ticket.TypeID = type;
                        ticket.Requestor = currentUser.UserName;
                        ticket.Owner = checktype.Owner;
                        ticket.RequestFrom = requestFrom;
                        ticket.Priority = "TPRI002";
                        ticket.Impact = "TIMP002";
                        ticket.CustomerID = customerid;
                        //check recall
                        if (!ticket.ReCallTime.HasValue)
                        {
                            ticket.IsDone = true;
                        }
                        else
                        {
                            ticket.IsDone = false;
                            ticket.ReCallTime = DateTime.Parse(dateCallBack);
                        }

                        ticket.RowCreatedUser = currentUser.UserName;
                        ticket.RowCreatedTime = DateTime.Now;
                        var id = ticket.Save();

                        if (id != "")
                        {
                            //save logs

                            CRM_Ticket_Log savelog = new CRM_Ticket_Log();
                            savelog.TicketID = id;
                            savelog.Activites = "Add New Ticket";
                            savelog.RowCreatedTime = DateTime.Now;
                            savelog.RowCreatedUser = currentUser.UserName;
                            savelog.Save();

                          
                            CRM_CS_Appendix newAppendix = new CRM_CS_Appendix();
                            newAppendix.TicketID = id;
                            newAppendix.Feeling = felling;
                            newAppendix.Trend = trend;
                            newAppendix.CreatedAt = DateTime.Now;
                            newAppendix.CreatedBy = currentUser.UserName;
                            newAppendix.UpdatedAt = DateTime.Now;
                            newAppendix.UpdatedBy = currentUser.UserName;
                            using (var dbConn = OrmliteConnection.openConn())
                                      dbConn.Insert(newAppendix);
                            

                        }
                        else
                        {
                            ModelState.AddModelError("", "Data not valid");
                            return Json(new { error = true, message = "500: Không tạo được ticket mới" });

                        }

                    }
                    else
                    {
                        return Json(new { success = false, message = "Nhập đầy đủ các thông tin bắt buộc." });
                    }

                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(new { success = false, message = "Chưa được cấp quyền." });
            }
          
            
            
        }
        // Change status ticket
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

                var checktype = CRM_Ticket_Type.GetCRM_Ticket_Type(typeid);
                if (checktype == null)
                {
                    return Json(new { error = 1, success = 0, message = "This Type not exist" });
                }
              
                foreach (var id in listdata)
                {
                    var check = CRM_Ticket.GetCRM_Ticket(id);

                    if (check != null)
                    {
                        //kiem tra phan quyen
                        if ((check.Assignee == currentUser.UserName || checktype.Owner == currentUser.UserName || check.Status == "Accepted" || check.Status == "WIP" || check.Status == "Reopen" || check.Status == "Denied-WIP" || check.Status == "Escalated" || check.Status == "Assigned"))
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

                                CRM_Ticket_Log savelog = new CRM_Ticket_Log();
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
                var checkqueue = CRM_Ticket_Queue.GetCRM_Ticket_Queue(QueueID);
                if (UserName != "" && CRM_Ticket_Queue.CheckUserInQueue(UserName, QueueID) == false)
                {
                    return Json(new { error = 1, success = 0, message = "User not valid" });
                }
                foreach (var id in listdata)
                {
                    var check = CRM_Ticket.GetCRM_Ticket(id);

                    if (check != null)
                    {
                        //kiem tra phan quyen
                        if ((check.Assignee == currentUser.UserName || check.Owner == currentUser.UserName || CRM_Ticket_RoleUpdate.Check(currentUser.UserName, check.TypeID)) && (check.Status == "Accepted" || check.Status == "WIP" || check.Status == "Reopen" || check.Status == "Denied-WIP" || check.Status == "Escalated" || check.Status == "Assigned"))
                        {
                            check.preEscalateQueue = QueueID;
                            check.preAssignee = UserName;
                            check.Status = "Escalated";
                            check.RowLastUpdatedTime = DateTime.Now;
                            check.RowLastUpdatedUser = currentUser.UserName;
                            check.Update(0);

                            //save logs

                            CRM_Ticket_Log savelog = new CRM_Ticket_Log();
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
                    var check = CRM_Ticket.GetCRM_Ticket(id);
                    if (check != null)
                    {

                        var check1 = CRM_Ticket.GetCRM_Ticket_checkintype(check.TypeID, currentUser.UserName);

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

                                CRM_Ticket_Log savelog = new CRM_Ticket_Log();
                                savelog.TicketID = id;
                                savelog.Activites = currentUser.UserName + " deny assign";
                                savelog.RowCreatedTime = DateTime.Now;
                                savelog.RowCreatedUser = currentUser.UserName;
                                savelog.Save();

                                // save activities
                                CRM_Ticket_Activities acc = new CRM_Ticket_Activities();
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

                            CRM_Ticket_Log savelog = new CRM_Ticket_Log();
                            savelog.TicketID = id;
                            savelog.Activites = currentUser.UserName + " deny assign";
                            savelog.RowCreatedTime = DateTime.Now;
                            savelog.RowCreatedUser = currentUser.UserName;
                            savelog.Save();
                            // save activities
                            CRM_Ticket_Activities acc = new CRM_Ticket_Activities();
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

                            CRM_Ticket_Log savelog = new CRM_Ticket_Log();
                            savelog.TicketID = id;
                            savelog.Activites = currentUser.UserName + " deny assign";
                            savelog.RowCreatedTime = DateTime.Now;
                            savelog.RowCreatedUser = currentUser.UserName;
                            savelog.Save();
                            // save activities
                            CRM_Ticket_Activities acc = new CRM_Ticket_Activities();
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

                            CRM_Ticket_Log savelog = new CRM_Ticket_Log();
                            savelog.TicketID = id;
                            savelog.Activites = currentUser.UserName + " deny assign";
                            savelog.RowCreatedTime = DateTime.Now;
                            savelog.RowCreatedUser = currentUser.UserName;
                            savelog.Save();
                            // save activities
                            CRM_Ticket_Activities acc = new CRM_Ticket_Activities();
                            acc.RowCreatedTime = DateTime.Now;
                            acc.RowCreatedUser = currentUser.UserName;
                            acc.TicketID = id;
                            acc.Activities = currentUser.UserName + " từ chối nhận ticket lúc " + DateTime.Now.ToString("HH:mm dd/MM/yyyy") + ", lý do: " + denyreason;
                            acc.Save();

                        }
                        else if (check.preAssignee == "" && CRM_Ticket_Queue.CheckUserInQueue(currentUser.UserName, check.preEscalateQueue) && (check.Status == "Escalated"))
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

                            CRM_Ticket_Log savelog = new CRM_Ticket_Log();
                            savelog.TicketID = id;
                            savelog.Activites = currentUser.UserName + " deny assign";
                            savelog.RowCreatedTime = DateTime.Now;
                            savelog.RowCreatedUser = currentUser.UserName;
                            savelog.Save();
                            // save activities
                            CRM_Ticket_Activities acc = new CRM_Ticket_Activities();
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

                            CRM_Ticket_Log savelog = new CRM_Ticket_Log();
                            savelog.TicketID = id;
                            savelog.Activites = currentUser.UserName + " deny assign";
                            savelog.RowCreatedTime = DateTime.Now;
                            savelog.RowCreatedUser = currentUser.UserName;
                            savelog.Save();
                            // save activities
                            CRM_Ticket_Activities acc = new CRM_Ticket_Activities();
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
                    var check = CRM_Ticket.GetCRM_Ticket(id);
                    if (check != null)
                    {
                        if ((check.Status == "Accepted" || check.Status == "Denied-WIP" || check.Status == "Escalated" || check.Status == "Assigned" || check.Status == "Reopen") && (check.Assignee == currentUser.UserName  )) //CRM_Ticket_RoleUpdate.Check(currentUser.UserName, check.TypeID)
                        {
                            check.Status = "WIP";
                            check.preEscalateQueue = "";
                            check.preAssignee = "";
                            check.RowLastUpdatedTime = DateTime.Now;
                            check.RowLastUpdatedUser = currentUser.UserName;
                            check.Update(0);
                            total++;
                            //save logs
                            CRM_Ticket_Log savelog = new CRM_Ticket_Log();
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
                    var check = CRM_Ticket.GetCRM_Ticket(id);
                    if (check != null)
                    {

                        if ((check.Status == "WIP" || check.Status == "Reopen" || check.Status == "Accepted" || check.Status == "Denied-WIP" || check.Status == "Accepted" || check.Status == "Escalated" || check.Status == "Assigned") && (check.Owner == currentUser.UserName || check.Assignee == currentUser.UserName )) //|| Deca_RT_RoleUpdate.Check(currentUser.UserName, check.TypeID)
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

                            CRM_Ticket_Log savelog = new CRM_Ticket_Log();
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
                    var check = CRM_Ticket.GetCRM_Ticket(id);
                    if (check != null)
                    {

                        if (check.Status == "Resolved" && (check.Requestor == currentUser.UserName )) //|| Deca_RT_RoleUpdate.Check(currentUser.UserName, check.TypeID)
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

                                CRM_Ticket_Log savelog = new CRM_Ticket_Log();
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
        public ActionResult Clone(string data)
        {
            if (asset.Update)
            {
                var check = CRM_Ticket.GetCRM_Ticket(data);
                string message = "";
                if (check != null && check.Requestor == currentUser.UserName)
                {
                    CRM_Ticket newticket = new CRM_Ticket();
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

                        CRM_Ticket_Log savelog = new CRM_Ticket_Log();
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
                    var check = CRM_Ticket.GetCRM_Ticket(id);
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

                            CRM_Ticket_Log savelog = new CRM_Ticket_Log();
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
                    var check = CRM_Ticket.GetCRM_Ticket(id);
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

                            CRM_Ticket_Log savelog = new CRM_Ticket_Log();
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
                    var check = CRM_Ticket.GetCRM_Ticket(id);
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

        // Activities
        public ActionResult Activities_Read([DataSourceRequest] DataSourceRequest request, string TicketID)
        {
            if (asset.View && TicketID != null)
            {

                var data = CRM_Ticket_Activities.GetAllCRM_Ticket_Activities(TicketID).OrderByDescending(s => s.RowCreatedTime).ToList();
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Activities_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CRM_Ticket_Activities> listorder, string TicketID)
        {

            if (asset.View)
            {

                if (listorder != null && ModelState.IsValid)
                {
                    if (TicketID != null)
                    {
                        var check = CRM_Ticket.GetCRM_Ticket(TicketID);
                        if (check != null && (check.Status == "WIP" || check.Status == "Reopen" || check.Status == "Denied-WIP" || check.Status == "Assigned" || check.Status == "Escalated" || check.Status == "Accepted") && (check.Assignee == currentUser.UserName || check.Owner == currentUser.UserName || check.Requestor == currentUser.UserName))
                        {
                            CRM_Ticket_Activities acc = new CRM_Ticket_Activities();
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


        // Comment
        public ActionResult Comment_Read([DataSourceRequest] DataSourceRequest request, string TicketID)
        {
            if (asset.View && TicketID != null)
            {

                var data = CRM_Ticket_Comment.GetAllCRM_Ticket_Comments(TicketID).OrderByDescending(s => s.RowCreatedTime).ToList();
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Comment_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CRM_Ticket_Comment> listcomment, string TicketID)
        {

            if (asset.View)
            {

                if (listcomment != null && ModelState.IsValid)
                {
                    if (TicketID != null)
                    {
                        var check = CRM_Ticket.GetCRM_Ticket(TicketID);
                        if (check != null && check.Status != "Closed" && check.Status != "Cancelled")
                        {
                            CRM_Ticket_Comment acc = new CRM_Ticket_Comment();
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

        // Log
        public ActionResult Log_Read([DataSourceRequest] DataSourceRequest request, string TicketID)
        {
            if (asset.View && TicketID != null)
            {

                var data = CRM_Ticket_Log.GetAllCRM_Ticket_Logs(TicketID).OrderByDescending(s => s.RowCreatedTime).ToList();
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

        }

        // Email
        public ActionResult Email_Read([DataSourceRequest] DataSourceRequest request, string TicketID)
        {
            if (asset.View && TicketID != null)
            {
                using (var dbConn = OrmliteConnection.openConn())
                {
                    return Json(KendoApplyFilter.KendoData<CRM_Ticket_Email>(request, "TicketID='" + TicketID + "'"));
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SendEmail(Deca_RT_Email model, HttpPostedFileBase[] FileUploads)
        {
            if (asset.Update && ModelState.IsValid)
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                var ticket = CRM_Ticket.GetCRM_Ticket(model.TicketID);
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
                        CRM_Ticket_Activities acc = new CRM_Ticket_Activities();
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

        public ActionResult MergeTicket(string ToTicketID, string MergeTicket)
        {
            if (asset.Update)
            {
                var To = CRM_Ticket.GetCRM_Ticket(ToTicketID);
                var From = CRM_Ticket.GetCRM_Ticket(MergeTicket);
                if (From.Status == "Cancelled" || From.Status == "Closed")
                {
                    return (Json(new { success = false, message = "Ticket này đã đóng hoặc hủy." }));
                }
                if (To != null && From != null && (From.Assignee == currentUser.UserName || From.Owner == currentUser.UserName || From.Requestor == currentUser.UserName) && (To.Assignee == currentUser.UserName || To.Owner == currentUser.UserName || To.Requestor == currentUser.UserName))
                {
                    CRM_Ticket_Activities acc = new CRM_Ticket_Activities();
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

        // get list from master data:
        public ActionResult Ticket_GetType([DataSourceRequest] DataSourceRequest request, string categoryid)
        {
            if (asset.View)
            {
                var data = CRM_Ticket_Type.GetAllCRM_Ticket_Types_Category(categoryid).Where(s => s.Status == "True" && s.TypeID != "TIC0092").ToList();
                return Json(new { success = true, Data = data });
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult GetListCategory()
        {
            if (asset.View)
            {
                var data = CRM_Ticket_Category.GetAllDeca_RT_Categorys().ToList();
                return Json(data, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult GetListType(string categories)
        {
            if (asset.View)
            {
                var data = CRM_Ticket_Type.GetAllCRM_Ticket_Types_Category(categories).Where(s => s.Status == "True" && s.TypeID != "TIC0092").ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
               
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

                var check = CRM_Ticket_Type.GetCRM_Ticket_Type(typeid);
                var data = CRM_Ticket_Type.GetListAssignee(typeid);
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

                    var listdata = CRM_Ticket_Queue.GetListUserInQueue(EscalateQueue);
                    return Json(new { success = true, Data = listdata });
                }
                else
                {
                    var listdata = CRM_Ticket_Type.GetListUserInType(TypeID);
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

                var data = CRM_Ticket_Queue.GetListUserInQueue(QueueID);
                return Json(new { success = true, Data = data });
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        [HttpPost]
        public ActionResult ChangeFollower(string TicketID, string Follower)
        {
            try
            {
                var check = CRM_Ticket.GetCRM_Ticket(TicketID);
                if (asset.Update && check != null && (check.Assignee == currentUser.UserName || check.Owner == currentUser.UserName || check.Requestor == currentUser.UserName))
                {
                    CRM_Ticket_Follower.DeleteAll(TicketID);
                    if (!string.IsNullOrEmpty(Follower))
                    {
                        for (int i = 0; i < Follower.Split(',').Length; i++)
                        {
                            CRM_Ticket_Follower f = new CRM_Ticket_Follower();
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
                var check = CRM_Ticket.GetCRM_Ticket(id);
                //   var check2 = Deca_RT_Ticket.GetDeca_RT_Ticket_checkcanview(id, currentUser.UserName, "", currentUser.Team);
                if (checkrole("ViewAll") == true || Deca_RT_Queue.CheckUserInQueue(currentUser.UserName, check.EscalateQueue) || CRM_Ticket_Queue.CheckUserInQueue(currentUser.UserName, check.preEscalateQueue))
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
                    var check = CRM_Ticket.GetCRM_Ticket(id);
                    if (check != null && (check.Status != "Closed" && check.Status != "Resolved") && (check.Assignee == currentUser.UserName || check.Requestor == currentUser.UserName || check.Owner == currentUser.UserName))
                    {
                        string fileLocation = Server.MapPath("~/UploadFileTicket/" + id + "/" + filename);

                        if (System.IO.File.Exists(fileLocation))
                            System.IO.File.Delete(fileLocation);
                        //save logs

                        CRM_Ticket_Log savelog = new CRM_Ticket_Log();
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
                        var check = CRM_Ticket.GetCRM_Ticket(id);
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

                                        CRM_Ticket_Log savelog = new CRM_Ticket_Log();
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


        // Other
        public ActionResult GetRemind(string UserName)
        {
            if (asset.View)
            {
                try
                {
                    var myticket = CRM_Ticket.CountMyTicket(currentUser.UserName);
                    var newticket = CRM_Ticket.CountNewTicket(currentUser.UserName);
                    var queueticket = CRM_Ticket.CountQueueTicket(currentUser.UserName);
                    var createdticket = CRM_Ticket.CountCreatedTicket(currentUser.UserName);
                    var resolvedticket = CRM_Ticket.CountResolvedTicket(currentUser.UserName);
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
        public ActionResult LoadTicketIDSuggest(string TicketID)
        {
            List<CRM_Ticket> data = new List<CRM_Ticket>();
            var where = " TicketID LIKE N'%" + TicketID + "%'";
           // data = CRM_Ticket.GetCRM_Tickets_Suggets_ViewAll(currentUser.UserName, where);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Ticket_CheckCustomer(string CustomerID, string TypeID, string OrganizationID)
        {
            if (asset.View)
            {
                //var type = CRM_Ticket_Type.GetCRM_Ticket_Type(TypeID);
                //if (type == null || (type.RequireCustomerID == "True" && (CustomerID == null || CustomerID == "" || OrganizationID == null || OrganizationID == "")))
                //{
                //    return Json(new { success = false, message = "This Type need CustomerID and OrganizationID" });
                //}
                //if (OrganizationID == null || OrganizationID == "" && (CustomerID != null && CustomerID != ""))
                //{
                //    return Json(new { success = false, message = "Need select OrganizationID" });
                //}
                //if (CustomerID != null && CustomerID != "" && OrganizationID != null && OrganizationID != "")
                //{
                //    try
                //    {
                //        var temp = CustomerID.Trim().Split(':');
                //        if (temp.Count() > 1)
                //        {
                //            if (temp[0].ToUpper() != OrganizationID.ToUpper())
                //            {
                //                return Json(new { success = false, message = "CustomerID not valid" });
                //            }
                //            //var data = DW_Deca_Customer.CheckCustomerID(CustomerID.Trim());
                //            //if (data == null)
                //            //{
                //            //    return Json(new { success = false, message = "CustomerID not exist" });
                //            //}
                //            return Json(new { success = true });
                //        }
                //        else
                //        {
                //            //var checkEployeeID = DW_Deca_Customer.GetCustomerID_ByEmployeeID(CustomerID, OrganizationID);
                //            //if (checkEployeeID == null)
                //            //{
                //            //    var checkcus = DW_Deca_Customer.CheckCustomerID(OrganizationID + ":" + CustomerID.Trim());
                //            //    if (checkcus == null)
                //            //    {
                //            //        return Json(new { success = false, message = "CustomerID not exist" });
                //            //    }
                //            //    else
                //            //    {
                //            //        return Json(new { success = true, CustomerID = checkcus.CustomerID, message = "CustomerID: " + checkcus.CustomerID + " ; CustomerName: " + checkcus.FullName + ". Are you sure this Customer?" });
                //            //    }
                //            //}
                //            //else
                //            //{
                //            //    return Json(new { success = true, CustomerID = checkEployeeID.CustomerID, message = "CustomerID: " + checkEployeeID.CustomerID + " ; CustomerName: " + checkEployeeID.FullName + ". Are you sure this Customer?" });
                //            //}
                //        }
                //    }
                //    catch
                //    {
                //        return Json(new { success = false, message = "Error" });
                //    }
                //}
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "Error" });
            }
        }
    }
}