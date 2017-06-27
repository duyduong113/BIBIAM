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
using OfficeOpenXml;
using System.IO;
using Kendo.Mvc;
using System.ComponentModel;

namespace ERPAPD.Controllers
{
    [Authorize]
    [RequireHttps]
    public class FAQManagementController : CustomController
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
                    ViewBag.listTopic = dbConn.Select<Deca_Topic>();
                    ViewBag.listFAQ = dbConn.Select<Deca_FAQ>();
                    ViewBag.Assets = asset;
                }
                return View();
            }
            return RedirectToAction("NoAccessRights", "Error");
        }

        public ActionResult ReadUnPublish([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    data = KendoApplyFilter.KendoData<Deca_FAQ>(request, "Published=0");
                }
                return Json(data);
            }
        }

        public ActionResult ReadPublish([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                if (asset.View)
                {
                    data = KendoApplyFilter.KendoData<Deca_FAQ>(request, "Published=1");
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
                    data = KendoApplyFilter.KendoData<Deca_FAQ_Comment>(request);
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
                    var data = dbConn.SingleOrDefault<Deca_FAQ>("Id={0}", Id);
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
                    var data = new Deca_FAQ();
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
    }
}