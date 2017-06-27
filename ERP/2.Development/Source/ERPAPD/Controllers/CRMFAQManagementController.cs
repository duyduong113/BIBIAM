using ServiceStack.OrmLite;
using System;
using System.Linq;
using System.Web.Mvc;
using ERPAPD.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERPAPD.Helpers;
using System.Data;

namespace ERPAPD.Controllers
{
    [Authorize]
    [RequireHttps]
    public class CRMFAQManagementController : CustomController
    {
        //
        // GET: /FAQManagement/
        public ActionResult Index()
        {
            //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            //{
            //    dbConn.DropTables(typeof(Deca_FAQ_Rating), typeof(Deca_FAQ_Comment), typeof(Deca_FAQ), typeof(Deca_Topic));
            //    const bool overwrite = false;
            //    dbConn.CreateTables(overwrite, typeof(Deca_Topic), typeof(Deca_FAQ), typeof(Deca_FAQ_Comment), typeof(Deca_FAQ_Rating));
            //}
            if (asset.View)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    ViewBag.listTopic = dbConn.Select<CRM_FAQ_Topic>();
                    ViewBag.listFAQ = dbConn.Select<CRM_FAQ>();
                    ViewBag.Assets = asset;
                }
                return View("CRMFAQManagement");
            }
            return RedirectToAction("NoAccessRights", "Error");
        }

        public ActionResult Feedback()
        {
            //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            //{
            //    dbConn.DropTables(typeof(Deca_FAQ_Rating), typeof(Deca_FAQ_Comment), typeof(Deca_FAQ), typeof(Deca_Topic));
            //    const bool overwrite = false;
            //    dbConn.CreateTables(overwrite, typeof(Deca_Topic), typeof(Deca_FAQ), typeof(Deca_FAQ_Comment), typeof(Deca_FAQ_Rating));
            //}
            if (asset.View)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    ViewBag.listTopic = dbConn.Select<CRM_FAQ_Topic>();
                    ViewBag.listFAQ = dbConn.Select<CRM_FAQ>();
                    ViewBag.Assets = asset;
                }
                return View("CRMFAQFeedback");
            }
            return RedirectToAction("NoAccessRights", "Error");
        }


        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    data = KendoApplyFilter.KendoData<CRM_FAQ>(request);
                }
                return Json(data);
            }
        }
               
        public ActionResult ReadComment([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    data = KendoApplyFilter.KendoData<CRM_FAQ_Comment>(request);
                }
                return Json(data);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateAnswer(int Id, string Question, string Content, string Published, int TopicId)
        {
            try
            {
                if (String.IsNullOrEmpty(Question) || String.IsNullOrEmpty(Content))
                {
                    return Json(new { success = false, error = "" });
                }
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var data = dbConn.SingleOrDefault<CRM_FAQ>("Id={0}", Id);
                    if (data != null)
                    {
                        data.Question = Question;
                        data.TopicId = TopicId;
                        data.Published = Published != null ? true : false;
                        data.Answer = Content;
                        data.UpdatedAt = DateTime.Now;
                        data.UpdatedBy = currentUser.UserName;
                        dbConn.Update(data);
                    }
                }
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateAnswer(string content, string Question, string Published, int TopicId)
        {
            try
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var data = new CRM_FAQ();
                    data.TopicId = TopicId;
                    data.Question = Question;
                    data.Published = Published != null ? true : false;
                    data.Answer = content;
                    data.CreatedAt = DateTime.Now;
                    data.CreatedBy = currentUser.UserName;
                    dbConn.Insert(data);
                }
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
            }
        }

        public ActionResult ChangeStatusOfQuestion(string data, int type)
        {
            if (asset.Update)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    using (var dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                    {
                        var listFAQ = dbConn.Select<CRM_FAQ>("[Id] IN (" + string.Join(",", listRowID.Select(p => "'" + p + "'")) + ")");
                        if (listFAQ.Count > 0)
                        {
                            foreach (var f in listFAQ)
                            {
                                if (type == 1)
                                    f.Published = true;
                                else if (type == 2)
                                    f.Published = false;
                                f.UpdatedAt = DateTime.Now;
                                f.UpdatedBy = currentUser.UserName;
                                dbConn.Update(f);
                            }
                            return Json(new { success = true });
                        }
                        else
                        {
                            return Json(new { success = false, alert = "No record" });
                        }
                    }                    
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }               
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to update record" });
            }
        }
        public ActionResult DeleteQuestion(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        using (var dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                        {
                            dbConn.Delete<CRM_FAQ>(where: "Id =  " + item);
                            dbConn.Delete<CRM_FAQ_Comment>(where: "QuestionId = " + item);
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
        public ActionResult ChangeStatusComment(string data, int type)
        {
            if (asset.Update)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    using (var dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                    {
                        var listFAQ = dbConn.Select<CRM_FAQ_Comment>("[Id] IN (" + string.Join(",", listRowID.Select(p => "'" + p + "'")) + ")");
                        if (listFAQ.Count > 0)
                        {
                            foreach (var f in listFAQ)
                            {
                                if (type == 1) f.Published = true;
                                else if (type == 2) f.Published = false;
                                f.UpdatedAt = DateTime.Now;
                                f.UpdatedBy = currentUser.UserName;
                                dbConn.Update(f);
                            }
                            return Json(new { success = true });
                        }
                        else
                        {
                            return Json(new { success = false, alert = "No record" });
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to update record" });
            }
        }
        public ActionResult DeleteComment(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    string[] separators = { "@@" };
                    var listRowID = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in listRowID)
                    {
                        using (var dbConn = ERPAPD.Helpers.OrmliteConnection.openConn())
                        {
                            dbConn.Delete<CRM_FAQ_Comment>(where: " Id = " + item);
                        }
                    }
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, alert = ex.Message });
                }
            }
            else
            {
                return Json(new { success = false, alert = "You don't have permission to delete record" });
            }
        }
    }
}