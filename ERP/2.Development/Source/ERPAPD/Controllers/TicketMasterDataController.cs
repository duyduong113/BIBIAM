using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ERPAPD.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERPAPD.Helpers;
using System.Data;
namespace ERPAPD.Controllers
{
    public class TicketMasterDataController : CustomController
    {

        public ActionResult Index()
        {
            if (asset.View)
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] = asset.Delete;
                ViewData["AllowExport"] = asset.Export;
                ViewData["Asset"] = asset;
                using (var dbConn = OrmliteConnection.openConn())
                {
                    ViewBag.listPriority = dbConn.Select<Parameters>("Type='TicketPriority'").OrderBy(s => s.ParamID);
                    ViewBag.listImpact = dbConn.Select<Parameters>("Type='TicketImpact'").OrderBy(s => s.ParamID);
                    ViewBag.listUser = dbConn.Select<Users>().OrderBy(s => s.UserName);
                    // ViewBag.listDepartment = Deca_Department.GetAllDeca_Departments();
                    // ViewBag.listTeam = Deca_Team.GetAllDeca_Teams();
                    ViewBag.listTeam = dbConn.Select<ERPAPD.Models.CRM_Team>().OrderBy(s => s.ID);
                    ViewBag.listDepartment = dbConn.Select<ERPAPD.Models.CRM_Hierarchy>().OrderBy(s => s.ID);
                    ViewBag.listCategory = CRM_Ticket_Category.GetAllDeca_RT_Categorys();
                    ViewBag.listType = CRM_Ticket_Type.GetAllCRM_Ticket_Types();
                }
                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }

        public ActionResult Category_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {

                var data = CRM_Ticket_Category.GetAllDeca_RT_Categorys().OrderByDescending(s => s.Status).ToList();
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

        }
        public ActionResult Queue_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {

                var data = CRM_Ticket_Queue.GetAllCRM_Ticket_Queues().OrderByDescending(s => s.Status).ToList();
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

        }
        public ActionResult Type_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {

                var data = CRM_Ticket_Type.GetAllCRM_Ticket_Types().OrderByDescending(s => s.Status).ToList();
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

        }
        public ActionResult WorkingTime_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {

                var data = CRM_Ticket_WorkTime.GetAllCRM_Ticket_WorkTimes();
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

        }
        public ActionResult Holiday_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {

                var data = CRM_Ticket_Holiday.GetAllCRM_Ticket_Holidays().OrderByDescending(s => s.Status).ToList();
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

        }
        public ActionResult KPI_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {

                var data = CRM_Ticket_KPI.GetAllCRM_Ticket_KPIs().OrderByDescending(s => s.Status).ToList();
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Category_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CRM_Ticket_Category> listrow)
        {

            IEnumerable<CRM_Ticket_Category> u = new CRM_Ticket_Category[] { };
            if (asset.Create)
            {

                if (listrow != null && ModelState.IsValid)
                {
                    foreach (var row in listrow)
                    {
                        var w = new CRM_Ticket_Category();
                        if (row.Name == null)
                        {
                            row.Name = "";
                        }
                        if (row.Description == null)
                        {
                            row.Description = "";
                        }
                        if (row.Status != "False")
                        {
                            row.Status = "True";
                        }
                        w.Name = row.Name;
                        w.Description = row.Description;
                        w.Status = row.Status;
                        w.RowCreatedUser = currentUser.UserName;
                        w.RowCreatedTime = DateTime.Now;
                        w.Save();
                    }
                    return Json(new { success = true });
                }
                ModelState.AddModelError("", "");
                return Json(u.ToDataSourceResult(request, ModelState));
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(u.ToDataSourceResult(request, ModelState));
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Queue_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CRM_Ticket_Queue> listrow)
        {

            IEnumerable<CRM_Ticket_Queue> u = new CRM_Ticket_Queue[] { };
            if (asset.Create)
            {

                if (listrow != null && ModelState.IsValid)
                {
                    foreach (var row in listrow)
                    {
                        var w = new CRM_Ticket_Queue();
                        if (row.QueueName == null)
                        {
                            row.QueueName = "";
                        }
                        if (row.Description == null)
                        {
                            row.Description = "";
                        }
                        if (row.Status != "False")
                        {
                            row.Status = "True";
                        }
                        w.QueueName = row.QueueName;
                        w.Description = row.Description;
                        w.Status = row.Status;
                        w.RowCreatedUser = currentUser.UserName;
                        w.RowCreatedTime = DateTime.Now;
                        w.Save();
                    }
                    return Json(new { success = true });
                }
                ModelState.AddModelError("", "");
                return Json(u.ToDataSourceResult(request, ModelState));
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(u.ToDataSourceResult(request, ModelState));
            }

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Type_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CRM_Ticket_Type> listrow)
        {

            IEnumerable<CRM_Ticket_Type> u = new CRM_Ticket_Type[] { };
            if (asset.Create)
            {

                if (listrow != null && ModelState.IsValid)
                {
                    foreach (var row in listrow)
                    {
                        var w = new CRM_Ticket_Type();
                        if (row.Name == null)
                        {
                            row.Name = "";
                        }
                        if (row.Description == null)
                        {
                            row.Description = "";
                        }
                        if (row.Category == null)
                        {
                            row.Category = "";
                        }
                        if (row.Owner == null)
                        {
                            row.Owner = "";
                        }
                        if (row.ResponeTime == null)
                        {
                            row.ResponeTime = 0;
                        }
                        if (row.ResolveTime == null)
                        {
                            row.ResolveTime = 0;
                        }
                        if (row.ClosedTime == null)
                        {
                            row.ClosedTime = 0;
                        }
                        if (row.RequireCustomerID != "False")
                        {
                            row.RequireCustomerID = "True";
                        }
                        if (row.Status != "False")
                        {
                            row.Status = "True";
                        }
                        w.Name = row.Name;
                        w.Description = row.Description;
                        w.Category = row.Category;
                        w.Owner = row.Owner;
                        w.ResponeTime = row.ResponeTime;
                        w.ResolveTime = row.ResolveTime;
                        w.ClosedTime = row.ClosedTime;
                        w.RequireCustomerID = row.RequireCustomerID;
                        w.RequireOrderID = row.RequireOrderID;
                        w.Status = row.Status;
                        w.Priority = row.Priority;
                        w.QueueList = "";
                        w.RowCreatedUser = currentUser.UserName;
                        w.RowCreatedTime = DateTime.Now;
                        w.Save();
                    }
                    return Json(new { success = true });
                }
                ModelState.AddModelError("", "");
                return Json(u.ToDataSourceResult(request, ModelState));
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(u.ToDataSourceResult(request, ModelState));
            }

        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult KPI_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CRM_Ticket_KPI> listrow)
        {

            IEnumerable<CRM_Ticket_KPI> u = new CRM_Ticket_KPI[] { };
            if (asset.Create)
            {

                if (listrow != null && ModelState.IsValid)
                {
                    foreach (var row in listrow)
                    {
                        var w = new CRM_Ticket_KPI();
                        if (row.TypeID == null)
                        {
                            row.TypeID = "";
                        }
                        if (row.Description == null)
                        {
                            row.Description = "";
                        }
                        if (row.Priority == null)
                        {
                            row.Priority = "";
                        }
                        if (row.Impact == null)
                        {
                            row.Impact = "";
                        }
                        if (row.ResponeTime == null)
                        {
                            row.ResponeTime = 0;
                        }
                        if (row.ResolveTime == null)
                        {
                            row.ResolveTime = 0;
                        }
                        if (row.ClosedTime == null)
                        {
                            row.ClosedTime = 0;
                        }
                        if (row.Status != "False")
                        {
                            row.Status = "True";
                        }
                        w.TypeID = row.TypeID;
                        w.Description = row.Description;
                        w.KPIID = row.KPIID;
                        w.Priority = row.Priority;
                        w.Impact = row.Impact;
                        w.ResponeTime = row.ResponeTime;
                        w.ResolveTime = row.ResolveTime;
                        w.ClosedTime = row.ClosedTime;
                        w.Status = row.Status;
                        w.RowCreatedUser = currentUser.UserName;
                        w.RowCreatedTime = DateTime.Now;
                        w.Save();
                    }
                    return Json(new { success = true });
                }
                ModelState.AddModelError("", "");
                return Json(u.ToDataSourceResult(request, ModelState));
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(u.ToDataSourceResult(request, ModelState));
            }

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult WorkingTime_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CRM_Ticket_WorkTime> listrow)
        {

            IEnumerable<CRM_Ticket_WorkTime> u = new CRM_Ticket_WorkTime[] { };
            if (asset.Update)
            {


                if (listrow != null && ModelState.IsValid)
                {
                    foreach (var row in listrow)
                    {

                        if (row.EndWorkingTime.TimeOfDay < row.StartWorkingTime.TimeOfDay)
                        {

                            ModelState.AddModelError("", "Working Time not valid");
                            return Json(u.ToDataSourceResult(request, ModelState));
                        }
                        if (row.EndBreakingTime.TimeOfDay < row.StartWorkingTime.TimeOfDay || row.EndBreakingTime.TimeOfDay > row.EndWorkingTime.TimeOfDay || row.StartBreakingTime.TimeOfDay < row.StartWorkingTime.TimeOfDay || row.StartBreakingTime.TimeOfDay > row.EndWorkingTime.TimeOfDay || row.EndBreakingTime.TimeOfDay < row.StartBreakingTime.TimeOfDay)
                        {
                            if (row.EndBreakingTime.TimeOfDay.ToString() != "00:00:00" || row.StartBreakingTime.TimeOfDay.ToString() != "00:00:00")
                            {
                                ModelState.AddModelError("", "Breaking Time not valid");
                                return Json(u.ToDataSourceResult(request, ModelState));
                            }

                        }
                        CRM_Ticket_WorkTime w = new CRM_Ticket_WorkTime();
                        w.StartWorkingTime = row.StartWorkingTime;
                        w.EndWorkingTime = row.EndWorkingTime;
                        w.StartBreakingTime = row.StartBreakingTime;
                        w.EndBreakingTime = row.EndBreakingTime;
                        w.Sun = row.Sun;
                        w.Mon = row.Mon;
                        w.Tue = row.Tue;
                        w.Wed = row.Wed;
                        w.Thu = row.Thu;
                        w.Fri = row.Fri;
                        w.Sat = row.Sat;
                        w.Status = "True";

                        w.RowCreatedUser = currentUser.UserName;
                        w.RowCreatedTime = DateTime.Now;
                        w.Save();


                    }
                    return Json(new { success = true });
                }
                ModelState.AddModelError("", "");
                return Json(u.ToDataSourceResult(request, ModelState));
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(u.ToDataSourceResult(request, ModelState));
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Holiday_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CRM_Ticket_Holiday> listrow)
        {

            IEnumerable<CRM_Ticket_Holiday> u = new CRM_Ticket_Holiday[] { };
            if (asset.Create)
            {

                if (listrow != null && ModelState.IsValid)
                {
                    foreach (var row in listrow)
                    {
                        if (row.StartDate > row.EndDate)
                        {
                            ModelState.AddModelError("", "StartDate and EndDate must not valid");
                            return Json(u.ToDataSourceResult(request, ModelState));
                        }
                        var w = new CRM_Ticket_Holiday();
                        if (row.Name == null)
                        {
                            row.Name = "";
                        }
                        if (row.Description == null)
                        {
                            row.Description = "";
                        }
                        if (row.Note == null)
                        {
                            row.Note = "";
                        }
                        if (row.StartDate == null)
                        {
                            row.StartDate = DateTime.Parse("1900-01-01");
                        }
                        if (row.EndDate == null)
                        {
                            row.EndDate = DateTime.Parse("1900-01-01");
                        }
                        row.Status = "True";
                        w.Name = row.Name;
                        w.Description = row.Description;
                        w.Note = row.Note;
                        w.StartDate = row.StartDate;
                        w.EndDate = row.EndDate;
                        w.Status = row.Status;
                        w.Department = "";
                        w.RowCreatedUser = currentUser.UserName;
                        w.RowCreatedTime = DateTime.Now;
                        w.Save();
                    }
                    return Json(new { success = true });
                }
                ModelState.AddModelError("", "");
                return Json(u.ToDataSourceResult(request, ModelState));
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(u.ToDataSourceResult(request, ModelState));
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Category_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CRM_Ticket_Category> listrow)
        {

            IEnumerable<CRM_Ticket_Category> u = new CRM_Ticket_Category[] { };
            if (asset.Update)
            {
                if (listrow != null && ModelState.IsValid)
                {
                    foreach (var row in listrow)
                    {
                        CRM_Ticket_Category w = CRM_Ticket_Category.GetCRM_RT_Category(row.CategoryID);
                        if (w != null)
                        {

                            if (row.Name == null)
                            {
                                row.Name = "";
                            }
                            if (row.Description == null)
                            {
                                row.Description = "";
                            }
                            if (row.Status != "True")
                            {
                                row.Status = "False";
                            }
                            w.Name = row.Name;
                            w.Description = row.Description;
                            w.Status = row.Status;
                            w.RowLastUpdatedUser = currentUser.UserName;
                            w.RowLastUpdatedTime = DateTime.Now;
                            w.Update();
                        }
                    }
                    return Json(new { success = true });
                }
                ModelState.AddModelError("", "");
                return Json(u.ToDataSourceResult(request, ModelState));
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to update record");
                return Json(u.ToDataSourceResult(request, ModelState));
            }

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Type_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CRM_Ticket_Type> listrow)
        {

            IEnumerable<CRM_Ticket_Type> u = new CRM_Ticket_Type[] { };
            if (asset.Update)
            {


                if (listrow != null && ModelState.IsValid)
                {
                    foreach (var row in listrow)
                    {

                        CRM_Ticket_Type w = CRM_Ticket_Type.GetCRM_Ticket_Type(row.TypeID);
                        if (w != null)
                        {

                            if (row.Name == null)
                            {
                                row.Name = "";
                            }
                            if (row.Description == null)
                            {
                                row.Description = "";
                            }
                            if (row.Category == null)
                            {
                                row.Category = "";
                            }
                            if (row.Owner == null)
                            {
                                row.Owner = "";
                            }
                            if (row.ResponeTime == null)
                            {
                                row.ResponeTime = 0;
                            }
                            if (row.ResolveTime == null)
                            {
                                row.ResolveTime = 0;
                            }
                            if (row.ClosedTime == null)
                            {
                                row.ClosedTime = 0;
                            }
                            if (row.RequireCustomerID != "False")
                            {
                                row.RequireCustomerID = "True";
                            }
                            if (row.Status != "True")
                            {
                                row.Status = "False";
                            }
                            w.Name = row.Name;
                            w.Description = row.Description;
                            w.Category = row.Category;
                            w.Owner = row.Owner;
                            w.ResponeTime = row.ResponeTime;
                            w.ResolveTime = row.ResolveTime;
                            w.ClosedTime = row.ClosedTime;
                            w.RequireCustomerID = row.RequireCustomerID;
                            w.RequireOrderID = row.RequireOrderID;
                            w.Status = row.Status;
                            w.Priority = row.Priority;
                            w.RowLastUpdatedUser = currentUser.UserName;
                            w.RowLastUpdatedTime = DateTime.Now;
                            w.Update();
                        }
                    }
                    return Json(new { success = true });
                }
                ModelState.AddModelError("", "");
                return Json(u.ToDataSourceResult(request, ModelState));
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to update record");
                return Json(u.ToDataSourceResult(request, ModelState));
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult KPI_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CRM_Ticket_KPI> listrow)
        {

            IEnumerable<CRM_Ticket_KPI> u = new CRM_Ticket_KPI[] { };
            if (asset.Update)
            {


                if (listrow != null && ModelState.IsValid)
                {
                    foreach (var row in listrow)
                    {

                        CRM_Ticket_KPI w = CRM_Ticket_KPI.GetCRM_Ticket_KPI(row.KPIID);
                        if (w != null)
                        {
                            if (row.Status != "True")
                            {
                                row.Status = "False";
                            }
                            if (row.TypeID == null)
                            {
                                row.TypeID = "";
                            }
                            if (row.Description == null)
                            {
                                row.Description = "";
                            }
                            if (row.Priority == null)
                            {
                                row.Priority = "";
                            }
                            if (row.Impact == null)
                            {
                                row.Impact = "";
                            }
                            if (row.ResponeTime == null)
                            {
                                row.ResponeTime = 0;
                            }
                            if (row.ResolveTime == null)
                            {
                                row.ResolveTime = 0;
                            }
                            if (row.ClosedTime == null)
                            {
                                row.ClosedTime = 0;
                            }
                            if (row.Status != "False")
                            {
                                row.Status = "True";
                            }

                            w.TypeID = row.TypeID;
                            w.Description = row.Description;
                            w.KPIID = row.KPIID;
                            w.Priority = row.Priority;
                            w.Impact = row.Impact;
                            w.ResponeTime = row.ResponeTime;
                            w.ResolveTime = row.ResolveTime;
                            w.ClosedTime = row.ClosedTime;
                            w.Status = row.Status;
                            w.RowCreatedUser = currentUser.UserName;
                            w.RowCreatedTime = DateTime.Now;
                            w.Update();
                        }
                    }
                    return Json(new { success = true });
                }
                ModelState.AddModelError("", "");
                return Json(u.ToDataSourceResult(request, ModelState));
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to update record");
                return Json(u.ToDataSourceResult(request, ModelState));
            }

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Queue_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CRM_Ticket_Queue> listrow)
        {

            IEnumerable<CRM_Ticket_Queue> u = new CRM_Ticket_Queue[] { };
            if (asset.Update)
            {


                if (listrow != null && ModelState.IsValid)
                {
                    foreach (var row in listrow)
                    {

                        CRM_Ticket_Queue w = CRM_Ticket_Queue.GetCRM_Ticket_Queue(row.QueueID);
                        if (w != null)
                        {

                            if (row.QueueName == null)
                            {
                                row.QueueName = "";
                            }
                            if (row.Description == null)
                            {
                                row.Description = "";
                            }
                            if (row.Status != "True")
                            {
                                row.Status = "False";
                            }
                            w.QueueName = row.QueueName;
                            w.Description = row.Description;
                            w.Status = row.Status;
                            w.RowLastUpdatedUser = currentUser.UserName;
                            w.RowLastUpdatedTime = DateTime.Now;
                            w.Update();
                        }
                    }
                    return Json(new { success = true });
                }
                ModelState.AddModelError("", "");
                return Json(u.ToDataSourceResult(request, ModelState));
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to update record");
                return Json(u.ToDataSourceResult(request, ModelState));
            }

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult WorkingTime_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CRM_Ticket_WorkTime> listrow)
        {

            IEnumerable<CRM_Ticket_WorkTime> u = new CRM_Ticket_WorkTime[] { };
            if (asset.Update)
            {


                if (listrow != null && ModelState.IsValid)
                {
                    foreach (var row in listrow)
                    {

                        if (row.EndWorkingTime.TimeOfDay < row.StartWorkingTime.TimeOfDay)
                        {

                            ModelState.AddModelError("", "Working Time not valid");
                            return Json(u.ToDataSourceResult(request, ModelState));
                        }
                        if (row.EndBreakingTime.TimeOfDay < row.StartWorkingTime.TimeOfDay || row.EndBreakingTime.TimeOfDay > row.EndWorkingTime.TimeOfDay || row.StartBreakingTime.TimeOfDay < row.StartWorkingTime.TimeOfDay || row.StartBreakingTime.TimeOfDay > row.EndWorkingTime.TimeOfDay || row.EndBreakingTime.TimeOfDay < row.StartBreakingTime.TimeOfDay)
                        {
                            if (row.EndBreakingTime.TimeOfDay.ToString() != "00:00:00" || row.StartBreakingTime.TimeOfDay.ToString() != "00:00:00")
                            {
                                ModelState.AddModelError("", "Breaking Time not valid");
                                return Json(u.ToDataSourceResult(request, ModelState));
                            }

                        }
                        CRM_Ticket_WorkTime w = CRM_Ticket_WorkTime.GetCRM_Ticket_WorkTime(row.WorkingTimeID);
                        if (w != null)
                        {
                            w.StartWorkingTime = row.StartWorkingTime;
                            w.EndWorkingTime = row.EndWorkingTime;
                            w.StartBreakingTime = row.StartBreakingTime;
                            w.EndBreakingTime = row.EndBreakingTime;
                            w.Sun = row.Sun;
                            w.Mon = row.Mon;
                            w.Tue = row.Tue;
                            w.Wed = row.Wed;
                            w.Thu = row.Thu;
                            w.Fri = row.Fri;
                            w.Sat = row.Sat;

                            w.RowLastUpdatedUser = currentUser.UserName;
                            w.RowLastUpdatedTime = DateTime.Now;
                            w.Update();
                        }

                    }
                    return Json(new { success = true });
                }
                ModelState.AddModelError("", "");
                return Json(u.ToDataSourceResult(request, ModelState));
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to update record");
                return Json(u.ToDataSourceResult(request, ModelState));
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Holiday_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<CRM_Ticket_Holiday> listrow)
        {

            IEnumerable<CRM_Ticket_Holiday> u = new CRM_Ticket_Holiday[] { };
            if (asset.Update)
            {


                if (listrow != null && ModelState.IsValid)
                {
                    foreach (var row in listrow)
                    {
                        if (row.StartDate > row.EndDate)
                        {
                            ModelState.AddModelError("", "StartDate and EndDate must not valid");
                            return Json(u.ToDataSourceResult(request, ModelState));
                        }
                        CRM_Ticket_Holiday w = CRM_Ticket_Holiday.GetCRM_Ticket_Holiday(row.HolidayID);
                        if (w != null)
                        {

                            if (row.Name == null)
                            {
                                row.Name = "";
                            }
                            if (row.Description == null)
                            {
                                row.Description = "";
                            }
                            if (row.Note == null)
                            {
                                row.Note = "";
                            }
                            if (row.StartDate == null)
                            {
                                row.StartDate = DateTime.Parse("1900-01-01");
                            }
                            if (row.EndDate == null)
                            {
                                row.EndDate = DateTime.Parse("1900-01-01");
                            }
                            if (row.Status != "True")
                            {
                                row.Status = "False";
                            }
                            w.Name = row.Name;
                            w.Description = row.Description;
                            w.Note = row.Note;
                            w.StartDate = row.StartDate;
                            w.EndDate = row.EndDate;
                            w.Status = row.Status;
                            w.RowLastUpdatedUser = currentUser.UserName;
                            w.RowLastUpdatedTime = DateTime.Now;
                            w.Update();
                        }
                    }
                    return Json(new { success = true });
                }
                ModelState.AddModelError("", "");
                return Json(u.ToDataSourceResult(request, ModelState));
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to update record");
                return Json(u.ToDataSourceResult(request, ModelState));
            }

        }

        public ActionResult Queue_Detail_Read(string queueid)
        {
            try
            {
                if (asset.View)
                {
                    CRM_Ticket_Queue w = CRM_Ticket_Queue.GetCRM_Ticket_Queue(queueid);
                    if (w != null)
                    {
                        var listDepartment = w.Department;
                        var listTeam = w.Team;
                        var listUser = w.User;
                        return Json(new { success = true, dataDepartment = listDepartment, dataTeam = listTeam, dataUser = listUser });
                    }
                    else
                    {
                        return Json(new { success = false, dataDepartment = "", dataTeam = "", dataUser = "" });
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
        public ActionResult Type_Detail_Read(string typeid)
        {

            try
            {
                if (asset.View)
                {
                    CRM_Ticket_Type w = CRM_Ticket_Type.GetCRM_Ticket_Type(typeid);
                    if (w != null)
                    {
                        var listQueue = w.QueueList;
                        return Json(new { success = true, dataQueue = listQueue });
                    }
                    else
                    {
                        return Json(new { success = false, dataQueue = "" });
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
        public ActionResult Queue_Update_Detail(string queueid, string listdepartment, string listteam, string listuser)
        {

            if (asset.Update)
            {
                if (queueid != null)
                {
                    CRM_Ticket_Queue w = CRM_Ticket_Queue.GetCRM_Ticket_Queue(queueid);
                    if (w != null)
                    {

                        w.Department = listdepartment;
                        w.Team = listteam;
                        w.User = listuser;
                        w.RowLastUpdatedUser = currentUser.UserName;
                        w.RowLastUpdatedTime = DateTime.Now;
                        w.Update();
                    }
                    return Json(new { success = true });
                }
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = false });
            }

        }
        public ActionResult Type_Update_Detail(string typeid, string listqueue)
        {

            if (asset.Update)
            {
                if (typeid != null)
                {
                    CRM_Ticket_Type w = CRM_Ticket_Type.GetCRM_Ticket_Type(typeid);
                    if (w != null)
                    {
                        w.QueueList = listqueue;
                        w.RowLastUpdatedUser = currentUser.UserName;
                        w.RowLastUpdatedTime = DateTime.Now;
                        w.Update();
                        string[] separators = { ";" };
                        var data = listqueue.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        w.DeleteQueue();
                        foreach (var q in data)
                        {
                            w.InsertQueue(q);
                        }
                    }
                    return Json(new { success = true });
                }
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = false });
            }

        }
        public ActionResult WorkingTime_Update_Detail(string workingtimeid, string listdepartment)
        {

            if (asset.Update)
            {
                if (workingtimeid != null)
                {
                    CRM_Ticket_WorkTime w = CRM_Ticket_WorkTime.GetCRM_Ticket_WorkTime(workingtimeid);
                    if (w != null)
                    {

                        w.DepartmentID = listdepartment;

                        w.RowLastUpdatedUser = currentUser.UserName;
                        w.RowLastUpdatedTime = DateTime.Now;
                        w.Update();
                    }
                    return Json(new { success = true });
                }
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = false });
            }

        }
        public ActionResult Holiday_Update_Detail(string holidayid, string listdepartment)
        {

            if (asset.Update)
            {
                if (holidayid != null)
                {
                    CRM_Ticket_Holiday w = CRM_Ticket_Holiday.GetCRM_Ticket_Holiday(holidayid);
                    if (w != null)
                    {
                        w.Department = listdepartment;
                        w.RowLastUpdatedUser = currentUser.UserName;
                        w.RowLastUpdatedTime = DateTime.Now;
                        w.Update();
                    }
                    return Json(new { success = true });
                }
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = false });
            }

        }

        public ActionResult WorkingTime_Detail_Read(string workingtimeid)
        {
            try
            {
                if (asset.View)
                {
                    CRM_Ticket_WorkTime w = CRM_Ticket_WorkTime.GetCRM_Ticket_WorkTime(workingtimeid);
                    if (w != null)
                    {
                        var Department = w.DepartmentID;
                        //var listDepartment = CRM_Ticket_WorkTime.GetDeca_Department(workingtimeid);
                        var listDepartment = "";
                        return Json(new { success = true, dataDepartment = Department, datalistDepartment = listDepartment });
                    }
                    else
                    {
                        return Json(new { success = false, dataDepartment = "" });
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

        public ActionResult Holiday_Detail_Read(string holidayid)
        {
            try
            {
                if (asset.View)
                {
                    CRM_Ticket_Holiday w = CRM_Ticket_Holiday.GetCRM_Ticket_Holiday(holidayid);
                    if (w != null)
                    {


                        return Json(new { success = true, dataDepartment = w.Department });
                    }
                    else
                    {
                        return Json(new { success = false, dataDepartment = "" });
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


        public ActionResult KPI_GetType([DataSourceRequest] DataSourceRequest request)
        {

            if (asset.View)
            {

                try
                {
                    var data = CRM_Ticket_Type.GetAllCRM_Ticket_Types_Active();

                    return Json(new { data = data });
                }
                catch
                {
                    List<DropDownList> data = new List<DropDownList>();
                    return Json(new { data = data });
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        // role viewall
        public ActionResult Role_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {

                var data = CRM_Ticket_Role.GetAllCRM_Ticket_Roles();
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Role_GetlistGroup([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                var data = CRM_Ticket_Role.GetListGroupID();
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Role_SavelistGroup([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.Update)
            {

                string[] separators = { "@@" };
                var data = Request.Form["data"].Split(separators, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < data.Length; i++)
                {
                    var check = CRM_Ticket_Role.GetCRM_Ticket_Role(int.Parse(data[i]));
                    if (check == null)
                    {
                        CRM_Ticket_Role role = new CRM_Ticket_Role();
                        role.ID = int.Parse(data[i]);
                        role.RowCreatedTime = DateTime.Now;
                        role.RowCreatedUser = currentUser.UserName;
                        role.Save();
                    }
                }

                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

        }

        public ActionResult DeleteGroup(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        CRM_Ticket_Role check = new CRM_Ticket_Role();
                        check.ID = int.Parse(item);
                        check.Delete();
                    }
                }
                catch (Exception e)
                {

                    return Json(new { success = false, alert = e });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to delete record" });
            }
        }



        // role update
        public ActionResult RoleUpdate_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {

                var data = CRM_Ticket_RoleUpdate.GetAllCRM_Ticket_RoleUpdates();
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RoleUpdate_GetlistUser([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                var data = CRM_Ticket_RoleUpdate.GetListUserName();
                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RoleUpdate_SavelistUser([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.Update)
            {

                string[] separators = { "@@" };
                var data = Request.Form["data"].Split(separators, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < data.Length; i++)
                {
                    var check = CRM_Ticket_RoleUpdate.GetCRM_Ticket_RoleUpdate(data[i]);
                    if (check == null)
                    {
                        CRM_Ticket_RoleUpdate role = new CRM_Ticket_RoleUpdate();
                        role.UserID = data[i];
                        role.UserName = data[i];
                        role.RowCreatedTime = DateTime.Now;
                        role.RowCreatedUser = currentUser.UserName;
                        role.Save();
                    }
                }

                return Json(data.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

        }

        public ActionResult DeleteUser(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        CRM_Ticket_RoleUpdate check = new CRM_Ticket_RoleUpdate();
                        check.UserName = item;
                        check.Delete();
                    }
                }
                catch (Exception e)
                {

                    return Json(new { success = false, alert = e });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to delete record" });
            }
        }

        public ActionResult CloneType(string data, string CategoryID)
        {
            if (asset.Create && !string.IsNullOrEmpty(CategoryID))
            {
                try
                {
                    string[] separators = { ";;" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        CRM_Ticket_Type check = CRM_Ticket_Type.GetCRM_Ticket_Type(item);
                        if (check != null)
                        {
                            check.Category = CategoryID;
                            check.RowCreatedUser = currentUser.UserName;
                            check.RowCreatedTime = DateTime.Now;
                            check.Save();
                        }
                    }
                }
                catch (Exception e)
                {

                    return Json(new { success = false, alert = e });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to Create record" });
            }
        }

   


        [HttpPost]
        public ActionResult DeleteCategory(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { ";;" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        using (var dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                        {
                            dbConn.Delete<CRM_Ticket_Category>("CategoryID = {0}", item );
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to delete record" });
            }
        }

        [HttpPost]
        public ActionResult DeleteQueue(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { ";;" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        using (var dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                        {
                            dbConn.Delete<CRM_Ticket_Queue>("QueueID = {0}", item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to delete record" });
            }
        }
        public ActionResult DeleteType(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { ";;" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    using (var dbConn = OrmliteConnection.openConn())
                    {
                        foreach (var item in listRowID)
                        {
                            CRM_Ticket_Type check = CRM_Ticket_Type.GetCRM_Ticket_Type(item);
                            if (dbConn.Scalar<int>("SELECT COUNT(TicketID) FROM CRM_Ticket WHERE TypeID ='" + item + "'") == 0)
                            {
                                if (check != null) check.Delete();
                            }
                            else
                            {
                                return Json(new { success = false, alert = "Đã được sử dụng trong ticket!" });
                            }
                                
                        }
                    }
                }
                catch (Exception e)
                {

                    return Json(new { success = false, alert = e });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to Delete record" });
            }
        }
        public ActionResult DeleteWorkTime(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { ";;" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    using (var dbConn = OrmliteConnection.openConn())
                    {
                        foreach (var item in listRowID)
                        {
                            dbConn.Delete<CRM_Ticket_WorkTime>("WorkingTimeID = {0}", item);
                        }
                    }
                }
                catch (Exception e)
                {

                    return Json(new { success = false, alert = e });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to Delete record" });
            }
        }
        public ActionResult DeleteHoliday(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { ";;" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    using (var dbConn = OrmliteConnection.openConn())
                    {
                        foreach (var item in listRowID)
                        {
                            dbConn.Delete<CRM_Ticket_Holiday>("HolidayID = {0}", item);
                        }
                    }
                }
                catch (Exception e)
                {

                    return Json(new { success = false, alert = e });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to Delete record" });
            }
        }
        public ActionResult DeleteKPI(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { ";;" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    using (var dbConn = OrmliteConnection.openConn())
                    {
                        foreach (var item in listRowID)
                        {
                            dbConn.Delete<CRM_Ticket_KPI>("KPIID = {0}", item);
                        }
                    }
                }
                catch (Exception e)
                {

                    return Json(new { success = false, alert = e });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to Delete record" });
            }
        }
    }
}
