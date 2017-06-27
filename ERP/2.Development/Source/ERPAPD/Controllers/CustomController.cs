using ERPAPD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using Microsoft.Owin.Security;
using System.Web.Security;

namespace ERPAPD.Controllers
{
    public class CustomController : Controller
    {
        protected Users currentUser;
        protected List<Groups> currentGroups;
        protected Asset asset = new Asset() { View = false, Create = false, Update = false, Delete = false, Export = false, Import = false };
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (this.User.Identity.IsAuthenticated)
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    //get again TimeZone when change config
                    Constants contant = new Constants();
                    currentUser = dbConn.FirstOrDefault<Users>("UserName={0}", User.Identity.Name);
                    if (currentUser == null || !currentUser.Active)
                    {
                        AuthenticationManager.SignOut();
                        FormsAuthentication.SignOut();
                    }
                    //else if (currentUser.Groups == null)
                    //{
                    //    AuthenticationManager.SignOut();
                    //    FormsAuthentication.SignOut();
                    //}
                    else
                    {
                        var assets = dbConn.FirstOrDefault<Assets>("Name={0}", this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller")));
                        if (assets == null)
                        {
                            var newAssets = new Assets();
                            newAssets.Name = this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller"));
                            newAssets.CreatedAt = DateTime.Now;
                            newAssets.CreatedBy = "system";
                            dbConn.Insert(newAssets);
                        }
                        if (currentUser.Groups != null)
                        {
                            if (currentUser.Groups.Select(s => s.Name).Contains("administrator"))
                            {
                                asset.View = true;
                                asset.Create = true;
                                asset.Update = true;
                                asset.Delete = true;
                                asset.Export = true;
                                asset.Import = true;
                            }
                            else
                            {
                                asset.View = currentUser.Groups.Select(s => s.Id).Intersect(assets.View != null ? assets.View : new List<int>()).Count() > 0;
                                asset.Create = currentUser.Groups.Select(s => s.Id).Intersect(assets.Create != null ? assets.Create : new List<int>()).Count() > 0;
                                asset.Update = currentUser.Groups.Select(s => s.Id).Intersect(assets.Update != null ? assets.Update : new List<int>()).Count() > 0;
                                asset.Delete = currentUser.Groups.Select(s => s.Id).Intersect(assets.Delete != null ? assets.Delete : new List<int>()).Count() > 0;
                                asset.Export = currentUser.Groups.Select(s => s.Id).Intersect(assets.Export != null ? assets.Export : new List<int>()).Count() > 0;
                                asset.Import = currentUser.Groups.Select(s => s.Id).Intersect(assets.Import != null ? assets.Import : new List<int>()).Count() > 0;
                            }
                        }

                    }
                }
            }
        }
        //protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        //{
        //    return new ServiceStackJsonResult
        //    {
        //        Data = data,
        //        ContentType = contentType,
        //        ContentEncoding = contentEncoding
        //    };
        //}
    }
    //public class ServiceStackJsonResult : JsonResult
    //{
    //    public override void ExecuteResult(ControllerContext context)
    //    {
    //        HttpResponseBase response = context.HttpContext.Response;
    //        response.ContentType = !String.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

    //        if (ContentEncoding != null)
    //        {
    //            response.ContentEncoding = ContentEncoding;
    //        }

    //        if (Data != null)
    //        {
    //            response.Write(JsonSerializer.SerializeToString(Data));
    //        }
    //    }
    //}    
}