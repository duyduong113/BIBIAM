using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using CMS.Models;

namespace CMS.Filters
{
    public class CustomActionFilter : ActionFilterAttribute
    {
        public string permission { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!String.IsNullOrEmpty(permission))
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                { 
                    var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                    var listPermission = permission.Split(',');
                    var exist = dbConn.SingleOrDefault<AccessRight>("controllerName={0}", controllerName);
                    if (exist != null)
                    {
                        if (listPermission.ToList().Except(exist.access).Count() > 0)
                        {
                            exist.access = listPermission.Distinct().ToList();
                            exist.updatedAt = DateTime.Now;
                            exist.updatedBy = "administrator";
                            dbConn.Update(exist);
                        }
                    }
                    else
                    {
                        var newAccess = new AccessRight();
                        newAccess.controllerName = controllerName;
                        newAccess.access = listPermission.Distinct().ToList();
                        newAccess.createdAt = DateTime.Now;
                        newAccess.createdBy = "administrator";
                        dbConn.Insert(newAccess);
                    }

                    var existPermission = dbConn.Select<AccessDetail>("controllerName={0}", controllerName);
                    foreach (var item in existPermission)
                    {
                        var intersect = exist.access.Except(item.access.Select(s => s.Key));
                        foreach (var item1 in intersect)
                        {
                            item.access.Add(item1, false);
                        }
                        dbConn.Update(item);
                    }

                    var existAdminPermission = dbConn.SingleOrDefault<AccessDetail>("groupId = 1 AND controllerName={0}", controllerName);
                    if (existAdminPermission != null)
                    {
                        if (String.Join(",", existAdminPermission.access.Select(s => s.Key)) != permission)
                        {
                            var access = new Dictionary<string, bool>();
                            foreach (var item in listPermission.Distinct().ToList())
                            {
                                if (item == "all")
                                {
                                    access.Add(item, true);
                                }
                                else
                                {
                                    access.Add(item, false);
                                }

                            }
                            existAdminPermission.access = access;
                            existAdminPermission.updatedAt = DateTime.Now;
                            existAdminPermission.updatedBy = "administrator";
                            dbConn.Update(existAdminPermission);
                        }
                    }
                    else
                    {
                        var accessDetail = new AccessDetail();
                        accessDetail.controllerName = controllerName;
                        accessDetail.groupId = 1;
                        var access = new Dictionary<string, bool>();
                        foreach (var item in listPermission.Distinct().ToList())
                        {
                            if (item == "all")
                            {
                                access.Add(item, true);
                            }
                            else
                            {
                                access.Add(item, false);
                            }
                        }
                        accessDetail.access = access;
                        accessDetail.createdAt = DateTime.Now;
                        accessDetail.createdBy = "administrator";
                        dbConn.Insert(accessDetail);
                    }
                }
            }
            base.OnActionExecuting(filterContext);

        }

    }
}