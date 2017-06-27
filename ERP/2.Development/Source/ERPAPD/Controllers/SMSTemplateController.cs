using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERPAPD.Models;
using ERPAPD.Helpers;
using Hangfire;
using ServiceStack.OrmLite;
using ERPAPD.Controllers;
using Newtonsoft.Json;

namespace MobiviPortal.Controllers
{
    public class SMSTemplateController : CustomController
    {

        public ActionResult Index()
        {
            //using (IDbConnection dbConn = OrmliteConnection.openConn())
            //{
            //    OrmLiteConfig.DialectProvider.UseUnicode = true;
            //    dbConn.DropTables(typeof(Deca_SMS_Template));
            //    const bool overwrite = false;
            //    dbConn.CreateTables(overwrite, typeof(Deca_SMS_Template));
            //}
            if (asset.View)
            {
                ViewData["AllowCreate"] = asset.Create;
                ViewData["AllowUpdate"] = asset.Update;
                ViewData["AllowDelete"] = asset.Delete;
                ViewData["AllowExport"] = asset.Export;
                ViewBag.AllowView = true;
                using (var dbConn = OrmliteConnection.openConn())
                {
                    ViewBag.listUser = dbConn.Select<Users>();
                    ViewBag.listGroup = dbConn.Select<Groups>();
                }

                return View();
            }
            else
            {
                ViewBag.AllowView = false;
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult SMS_Template_Read([DataSourceRequest] DataSourceRequest request)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                var list = KendoApplyFilter.KendoData<Deca_SMS_Template>(request);
                //var listGroup = UserGroup.GetAllUserGroups();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult SendMessage(string maxNum, string message, string title, string roles, string id)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                if (id == "0")
                {
                    //Neu co id= 0 thi insert
                    Deca_SMS_Template template = new Deca_SMS_Template();

                    template.Message = message.ToString();
                    template.MaxNumber = int.Parse(maxNum.ToString());
                    template.Title = title;
                    template.IsActive = true;
                    template.UpdatedBy = currentUser.UserName;
                    template.CreatedAt = DateTime.Now;
                    template.CreatedBy = currentUser.UserName;
                    template.UpdatedAt = DateTime.Now;

                    string[] sep = { "," };
                    var Arr = roles.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < Arr.Length; i++)
                    {
                        if (Arr[i].ToString() == "All")
                        {
                            template.Roles = "";
                            template.Roles = "All";
                            dbConn.Insert(template);
                            return Json(new { data = "Send", success = "Sucessfull" });
                        }

                        else
                            template.Roles = roles;
                    }

                    dbConn.Insert(template);
                    return Json(new { data = "Send", success = "Sucessfull" });

                }
                else
                {
                    //id != 0     updates
                    var w = new Deca_SMS_Template();
                    var list = dbConn.FirstOrDefault<Deca_SMS_Template>("ID=" + id);
                    list.CreatedAt = DateTime.Now;
                    list.CreatedBy = currentUser.UserName;
                    list.UpdatedBy = currentUser.UserName;
                    list.UpdatedAt = DateTime.Now;

                    list.MaxNumber = int.Parse(maxNum);
                    list.Message = message;
                    list.Title = title;

                    string[] sep = { "," };
                    var Arr = roles.Split(sep, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < Arr.Length; i++)
                    {
                        if (Arr[i].ToString() == "All")
                        {
                            list.Roles = "";
                            list.Roles = "All";
                            dbConn.Update(list);
                            return Json(new { data = "Send", success = "Update Sucessfull" });
                        }
                        else
                            list.Roles = roles;
                    }

                    dbConn.Update(list);
                    return Json(new { data = "Send", success = "Update Sucessfull" });
                }
            }
        }

        public ActionResult SMS_Template_Update([DataSourceRequest] DataSourceRequest request,
                                                [Bind(Prefix = "models")] IEnumerable<Deca_SMS_Template> listTemplate)
        {
            IEnumerable<Deca_SMS_Template> u = new Deca_SMS_Template[] { };
            if (asset.Create)
            {
                using (var dbConn = OrmliteConnection.openConn())
                {
                    if (listTemplate != null && ModelState.IsValid)
                    {
                        foreach (var smstem in listTemplate)
                        {
                            var list = dbConn.FirstOrDefault<Deca_SMS_Template>("ID=" + smstem.ID);
                            list.UpdatedAt = DateTime.Now;
                            list.IsActive = smstem.IsActive;
                            list.CreatedBy = currentUser.UserName;
                            list.UpdatedBy = currentUser.UserName;
                            list.CreatedAt = DateTime.Now;
                            list.MaxNumber = smstem.MaxNumber;
                            list.Message = smstem.Message;
                            list.Title = smstem.Title;
                            dbConn.Update(list);
                        }
                        return Json(new { success = true });
                    }
                    ModelState.AddModelError("", "Model not valid");
                    return Json(u.ToDataSourceResult(request, ModelState));
                }
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to create record");
                return Json(u.ToDataSourceResult(request, ModelState));
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SMS_Template_Delete([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<Deca_SMS_Template> u)
        {
            if (asset.Delete)
            {
                if (u != null)
                {
                    using (var dbConn = OrmliteConnection.openConn())
                        foreach (var item in u)
                        {
                            //item.SmsTempID = item.SmsTempID;
                            dbConn.Delete(item);
                        }
                }

                return Json(new[] { u }.ToDataSourceResult(request, ModelState));
            }
            else
            {
                ModelState.AddModelError("", "You don't have permission to delete this record");
                return Json(new[] { u }.ToDataSourceResult(request, ModelState));
            }
        }


        public ActionResult SmsTemplateID([DataSourceRequest] DataSourceRequest request, string id)
        {
            using (var dbConn = OrmliteConnection.openConn())
                if (!string.IsNullOrEmpty(id))
                {
                    var list = dbConn.Select<Deca_SMS_Template>("ID=" + id);

                    var listGroup = dbConn.Select<Groups>();

                    if (list.Count > 0)
                    {
                        return Json(new { success = true, data = JsonConvert.SerializeObject(list), data2 = listGroup });
                    }

                    return Json(new { success = false });
                }
                else
                {
                    return Json(new { success = false });
                }

        }

    }
}
