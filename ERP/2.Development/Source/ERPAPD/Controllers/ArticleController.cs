using System;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERPAPD.Models;
using ERPAPD.Helpers;
using ServiceStack.OrmLite;
using System.Data;

namespace ERPAPD.Controllers
{
    public class ArticleController : CustomController
    {
        readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //
        // GET: /TeleSalesAssignment/
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            //this.parentAsset = Asset.GetAsset(1);
            base.Initialize(requestContext);
        }
        public ActionResult Index()
        {
            if (asset.View)
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] = asset.Delete;
                ViewData["Asset"] = asset;
                ViewData["UserGroups"] = currentUser.Groups;
                
                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
       
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                var dbConn = Helpers.OrmliteConnection.openConn();
                var data = new DataSourceResult();
                string strQuery = @"SELECT * FROM [DC_Article]";
                data = KendoApplyFilter.KendoDataByQuery<DC_Article>(request, strQuery, "");
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
            // var data = DC_Article.GetAllDC_Articles();
            // return Json(data.ToDataSourceResult(request));
        }
        public ActionResult Create()
        {
            if (asset.Create)
            {
                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Edit(int id)
        {
            //check update rights for current controller
            if (asset.Update)
            {
                //chet update rights for selected record
                DC_Article hOld = DC_Article.GetDC_Article(id);
                if (hOld != null)
                {
                    return View(hOld);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Create(DC_Article article)
        {
            if (asset.Create)
            {
                if (ModelState.IsValid)
                {
                    DC_Article aOld = DC_Article.GetDC_Articles("[ArticleId]='" + Convert.ToInt16(article.ArticleId) + "'", "").FirstOrDefault();
                    if (aOld == null)
                    {
                       
                        if (String.IsNullOrEmpty(article.Title))
                        {
                            return Json(new { success = false, error = "Please input Title" });
                        }
                        if (String.IsNullOrEmpty(article.PostContent))
                        {
                            return Json(new { success = false, error = "Please input PostContent" });
                        }
                        article.UpdatedDate = DateTime.Now;
                        article.UpdatedBy = currentUser.UserName;
                        article.Save();
                    }
                    else {
                        return Json(new { success = false, error = "Article is already existed" });    
                    }
                }
                else
                {
                    return Json(new { success = false, error = "Please insert all fields required" });
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

            return Json(new { success = true});
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Edit(DC_Article article)
        {
            if (asset.Create)
            {
                if (ModelState.IsValid)
                {
                    DC_Article aOld = DC_Article.GetDC_Article(article.ArticleId);
                    if (aOld != null)
                    {
                        if (String.IsNullOrEmpty(article.Title))
                        {
                            return Json(new { success = false, error = "Please input Title" });
                        }
                        if (String.IsNullOrEmpty(article.PostContent))
                        {
                            return Json(new { success = false, error = "Please input PostContent" });
                        }
                        article.UpdatedDate = DateTime.Now;
                        article.UpdatedBy = currentUser.UserName;
                        article.Update();
                    }
                    else
                    {
                        return Json(new { success = false, error = "Article is already existed" });
                    }
                }
                else
                {
                    return Json(new { success = false, error = "Please insert all fields required" });
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }

            return Json(new { success = true });
        }
        public ActionResult Delete(string data)
        {
            if (asset.Delete)
            {
                try
                {
                    var newdata = data.Split(',').ToList();
                    var article = DC_Article.GetAllDC_Articles().Where(h => newdata.Contains(h.ArticleId.ToString())).ToList();
                    foreach (var item in article)
                    {
                        item.Delete();
                    }
                    return Json(new { success = true });
                }
                catch (Exception e)
                {
                    log.Error(e);
                    return Json(new { success = false, alert = e });
                }
            }
            else
            {
                return Json(new { success = false, alert = "Don't have permission to delete" });
            }
        }
        public ActionResult Active(string data)
        {
            if (asset.Update)
            {
                try
                {
                    IDbConnection dbConn = Helpers.OrmliteConnection.openConn();
                    var newdata = data.Split(',').ToList();
                    var article = DC_Article.GetAllDC_Articles().Where(h => newdata.Contains(h.ArticleId.ToString())).ToList();
                    foreach (var item in article)
                    {
                        item.Active = true;
                        item.UpdatedDate = DateTime.Now;
                        item.UpdatedBy = currentUser.UserName;
                        dbConn.Update(item);
                    }
                    return Json(new { success = true });
                }
                catch (Exception e)
                {
                    log.Error(e);
                    return Json(new { success = false, alert = e });
                }
            }
            else
            {
                return Json(new { success = false, alert = "Don't have permission to delete" });
            }
        }
        public ActionResult Inactive(string data)
        {
            if (asset.Update)
            {
                try
                {
                    IDbConnection dbConn = Helpers.OrmliteConnection.openConn();
                    var newdata = data.Split(',').ToList();
                    var article = DC_Article.GetAllDC_Articles().Where(h => newdata.Contains(h.ArticleId.ToString())).ToList();
                    foreach (var item in article)
                    {
                        item.Active = false;
                        item.UpdatedDate = DateTime.Now;
                        item.UpdatedBy = currentUser.UserName;
                        dbConn.Update(item);
                    }
                    return Json(new { success = true });
                }
                catch (Exception e)
                {
                    log.Error(e);
                    return Json(new { success = false, alert = e });
                }
            }
            else
            {
                return Json(new { success = false, alert = "Don't have permission to update" });
            }
        }
    }
}
