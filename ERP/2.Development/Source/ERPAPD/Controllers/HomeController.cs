using System.Web.Mvc;
//using ERPAPD.DCCore;
using System.Globalization;

namespace ERPAPD.Controllers
{
    [RequireHttps]
    public class HomeController : CustomController
    {
        [Authorize]
        public ActionResult Index()
        {
            //using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            //{
            //    dbConn.DropTables(typeof(Users), typeof(Assets), typeof(Groups));
            //    const bool overwrite = false;
            //    dbConn.CreateTables(overwrite, typeof(Users), typeof(Assets), typeof(Groups));
            //    var group = new Groups();
            //    group.Name = "administrator";
            //    group.Description = "SuperAdmin";
            //    group.CreatedAt = DateTime.Now;
            //    group.CreatedBy = "system";
            //    group.Active = true;
            //    dbConn.Insert(group);
            //    int Id = (int)dbConn.GetLastInsertId();
            //    var user = new Users();
            //    user.UserName = "administrator";
            //    user.Password = Helpers.GetMd5Hash.Generate("Aa123456");
            //    user.Groups = new List<GroupViewModel> { 
            //        new ERPAPD.Models.Users.GroupViewModel { Id = Id, Name = "administrator" } };
            //    user.CreatedAt = DateTime.Now;
            //    user.CreatedBy = "system";
            //    user.Active = true;
            //    dbConn.Insert(user);
            //
            //}
            //var dbConn = Helpers.OrmliteConnection.openConn();
            //var itemconfig = new ConfigDefaultSystems();
            //var Exitconfig = dbConn.Select<ConfigDefaultSystems>().FirstOrDefault();
            //if (Exitconfig != null)
            //{
            //    Session["Culture"] = new CultureInfo(Exitconfig.Culture);
            //    Session["LanguageID"] = Exitconfig.LanguageID;
            //    Session["DateTime"] = Exitconfig.DateTimeFormatDisplay;

            //}
            //else
            //{
            //    itemconfig.Culture = "vi";
            //    itemconfig.LanguageID = "VN";
            //    itemconfig.DateTimeFormatID = 0;
            //    itemconfig.DateTimeFormatDisplay = "dd/MM/yyyy";
            //    itemconfig.TimeZoneID = "SE Asia Standard Time";
            //    itemconfig.TimeZoneDisplayName = "(GMT+07:00) Bangkok, Hanoi, Jakarta";
            //    itemconfig.CreatedAt = DateTime.Now;
            //    itemconfig.CreatedBy = currentUser.UserName;
            //    itemconfig.UpdatedAt = DateTime.Parse("1900-01-01");
            //    itemconfig.UpdatedBy = "";
            //    dbConn.Insert(itemconfig);
            //    Session["Culture"] = new CultureInfo(itemconfig.Culture);
            //    Session["LanguageID"] = itemconfig.LanguageID;
            //    Session["DateTime"] = itemconfig.DateTimeFormatDisplay;
            //}
            return View();

        }
        public ActionResult ChangeCulture(string languageID, string lang, string returnurl)
        {

            Session["Culture"] = new CultureInfo(lang);
            Session["LanguageID"] = languageID;
            return Redirect(returnurl);
        }
       
    //    public ActionResult Chart()
    //    {
    //        var data = SpendingByMonth.GetAllSpedingbyMonth().ToList();
    //        return Json(data);
    //    }
    //    public ActionResult Articles_Read([DataSourceRequest] DataSourceRequest request)
    //    {
    //        var data = DC_Article.GetAllDC_Articles().OrderByDescending(a => a.ArticleId).ToList();
    //        return Json(data.ToDataSourceResult(request));
    //    }

    //    public ActionResult ChangeCulture(string languageID, string lang, string returnurl)
    //    {
           
    //        Session["Culture"] = new CultureInfo(lang);
    //        Session["LanguageID"] = languageID; 
    //        return Redirect(returnurl);
    //    }
       

    //    [HttpPost]
    //    public ActionResult getUserInfo(string username)
    //    {
    //        using (var dbConn = Helpers.OrmliteConnection.openConn())
    //        {
    //            EmployeeInfo ei = dbConn.SingleOrDefault<EmployeeInfo>("UserName = {0}", username);
    //            return PartialView("_UserInfoTooltip", ei);
    //        }
    //    }
    //    [HttpPost]
    //    public ActionResult getListFollowerUser()
    //    {
    //        var result = Deca_RT_Follower.GetListUser();
    //        return Json(new { success = true, list = result.Select(x => x.UserName).ToArray() });
    //    }
    //    public ActionResult SaveXliteCode(string XliteCode)
    //    {
    //        try
    //        {
    //            Session["XliteID"] = XliteCode;
    //            using (var dbConn = Helpers.OrmliteConnection.openConn())
    //            {
    //                var currentEmployyeeInfo = dbConn.FirstOrDefault<EmployeeInfo>("UserName={0}", currentUser.UserName);
    //                currentEmployyeeInfo.XLiteID = XliteCode;
    //                currentEmployyeeInfo.LastUpdatedDateTime = DateTime.Now;
    //                currentEmployyeeInfo.LastUpdatedUser = currentUser.UserName;
    //                dbConn.Update(currentEmployyeeInfo);
    //            }
    //            return Json(new { success = true, message = "" });
    //        }
    //        catch
    //        {
    //            return Json(new { success = false, message = "Xảy ra lỗi khi cập nhật extension. Vui lòng liên lạc hỗ trợ kĩ thuật." });
    //        }
    //    }
    }
}