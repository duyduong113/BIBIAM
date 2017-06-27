using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using ERPAPD.Models;
using System.Net.Mail;
using ServiceStack.OrmLite;
using System.Web.Security;
using System.Globalization;
using System.Runtime.Serialization;
using System.Net;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Collections;
using NPOI.HSSF.UserModel;
using System.Threading;

namespace ERPAPD.Controllers
{
    [DataContract]
    public class RecaptchaResult
    {
        [DataMember(Name = "success")]
        public bool Success { get; set; }

        [DataMember(Name = "error-codes")]
        public string[] ErrorCodes { get; set; }
    }

    [Authorize]
    public class AccountController : CustomController
    {
        readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public AccountController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            var dbConn = Helpers.OrmliteConnection.openConn();
            var itemconfig = new ConfigDefaultSystems();
            var Exitconfig = dbConn.Select<ConfigDefaultSystems>().FirstOrDefault();
            if (Exitconfig != null)
            {
                Session["Culture"] = new CultureInfo(Exitconfig.Culture);
                Session["LanguageID"] = Exitconfig.LanguageID;
                Session["DateTime"] = Exitconfig.DateTimeFormatDisplay;
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Exitconfig.Culture);
            }
            else
            {
                itemconfig.Culture = "vi";
                itemconfig.LanguageID = "VN";
                itemconfig.DateTimeFormatID = 25;
                itemconfig.DateTimeFormatDisplay = "dd/MM/yyyy";
                itemconfig.TimeZoneID = "SE Asia Standard Time";
                itemconfig.TimeZoneDisplayName = "(GMT+07:00) Bangkok, Hanoi, Jakarta";
                itemconfig.CreatedAt = DateTime.Now;
                itemconfig.CreatedBy = "System";
                itemconfig.UpdatedAt = DateTime.Parse("1900-01-01");
                itemconfig.UpdatedBy = "";
                dbConn.Insert(itemconfig);
                Session["Culture"] = new CultureInfo(itemconfig.Culture);
                Session["LanguageID"] = itemconfig.LanguageID;
                Session["DateTime"] = itemconfig.DateTimeFormatDisplay;
            }
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                const string verifyUrl = "https://www.google.com/recaptcha/api/siteverify";
                string secret = System.Configuration.ConfigurationManager.AppSettings["captchaSecret"].ToString().Trim();
                var response = Request.Form["g-recaptcha-response"];
                var remoteIp = Request.ServerVariables["REMOTE_ADDR"];

                var myParameters = String.Format("secret={0}&response={1}&remoteip={2}", secret, response, remoteIp);

                using (var wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    var json = wc.UploadString(verifyUrl, myParameters);
                    var js = new DataContractJsonSerializer(typeof(RecaptchaResult));
                    var ms = new MemoryStream(Encoding.ASCII.GetBytes(json));
                    var result = js.ReadObject(ms) as RecaptchaResult;
                    if (result == null || !result.Success) // SUCCESS!!!
                    {
                        ModelState.AddModelError("", "Please insert captchar");
                        return View(model);
                    }
                }

                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var user = dbConn.SingleOrDefault<Users>("UserName = {0} AND Password = {1}", model.UserName, Helpers.GetMd5Hash.Generate(model.Password));
                    if (user != null)
                    {
                        SetupFormsAuthTicket(model.UserName, model.RememberMe);
                        //get employee info
                        var employeeinfo = dbConn.FirstOrDefault<EmployeeInfo>("UserName ={0}", user.UserName);
                        if (employeeinfo != null)
                        {
                            Session["XliteID"] = employeeinfo.XLiteID;
                        }
                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid username or password.");
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private Users SetupFormsAuthTicket(string userName, bool persistanceFlag)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                Users user = dbConn.SingleOrDefault<Users>("UserName={0}", userName);
                var UserName = user.UserName;
                var userData = UserName.ToString(CultureInfo.InvariantCulture);
                var authTicket = new FormsAuthenticationTicket(1, //version
                                                            userName, // user name
                                                            DateTime.Now,             //creation
                                                            DateTime.Now.AddMinutes(2880), //Expiration
                                                            persistanceFlag, //Persistent
                                                            userData);

                var encTicket = FormsAuthentication.Encrypt(authTicket);
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                return user;
            }

        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserName(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserName(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserName(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            string userName = String.Empty;
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            MailAddress mail = new MailAddress(loginInfo.Email);

            //if (mail.Host != "mobivi.com" && mail.Host != "mobivi.vn")
            //{
            //    return RedirectToAction("Login");
            //}

            userName = mail.User;

            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var userO = dbConn.SingleOrDefault<Users>("UserName={0}", userName);
                if (userO == null)
                {
                    var u = new Users();
                    u.UserName = userName;
                    u.CreatedAt = DateTime.Now;
                    u.CreatedBy = "administrator";
                    u.Active = true;
                    dbConn.Insert(u);

                    var e = new EmployeeInfo();
                    e.UserName = userName;
                    e.Active = 1;
                    e.CreatedDatetime = DateTime.Now;
                    e.CreatedUser = "google";

                    e.Id = int.Parse(dbConn.GetLastInsertId().ToString());
                    dbConn.Insert(e);
                }
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {

                // If the user does not have an account, then prompt the user to create an account
                //ViewBag.ReturnUrl = returnUrl;
                //ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                //return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });

                var userN = new ApplicationUser() { UserName = userName };
                var result = await UserManager.CreateAsync(userN);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(userN.Id, loginInfo.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(userN, isPersistent: false);
                        return RedirectToLocal(returnUrl);
                    }
                }

                AddErrors(result);

                return RedirectToAction("Login");

            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserName());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserName());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserName(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpGet]
        // [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserName());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            identity.AddClaim(new Claim("PhoneNumber", "123-456-7890"));
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserName());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string UserName)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserName = UserName;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserName { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserName != null)
                {
                    properties.Dictionary[XsrfKey] = UserName;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
        #region ChangePassWord
        [Authorize]
        public ActionResult ChangePassword(string message = "")
        {
            ViewBag.Message = message;
            return View();
        }

        // ChangePassword method not implemented in CustomMembershipProvider.cs
        // Feel free to update!

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded = true;
                try
                {
                    string passwOldMd5 = ERPAPD.Helpers.GetMd5Hash.Generate(model.OldPassword);
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        var userO = dbConn.SingleOrDefault<Users>("UserName={0} and Password={1}", User.Identity.Name, passwOldMd5);
                        if (userO != null)
                        {


                            string passwNew = ERPAPD.Helpers.GetMd5Hash.Generate(model.NewPassword);
                            userO.Password = passwNew;
                            userO.LastUpdatedDateTime = DateTime.Now;
                            userO.UpdatedBy = userO.UserName;
                            dbConn.Update(userO);
                        }
                        else
                        {
                            changePasswordSucceeded = false;
                        }
                    }
                    //MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    //changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }
        #endregion
        #region ChangeProfile
        public ActionResult ChangeProfile()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                if (User.Identity.Name == "")
                {
                    return RedirectToAction("NoAccessRights", "Error");
                }
                else
                {
                    var currentUser = dbConn.FirstOrDefault<Users>("UserName={0}", User.Identity.Name);
                    return View(currentUser);
                }
            }
        }
        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ChangeProfile(Models.Users model, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.Name == "")
                {
                    return RedirectToAction("NoAccessRights", "Error");
                }
                if (model.UserName == "" || model.Email == "")
                {
                    return Json(new { success = false });
                }
                var dbConn = Helpers.OrmliteConnection.openConn();

                var updateuser = dbConn.SingleOrDefault<Users>("UserName={0}", User.Identity.Name);
                updateuser.LastUpdatedDateTime = DateTime.Now;
                updateuser.LastUpdatedUser = User.Identity.Name;
                if (model.Phone == null)
                    updateuser.Phone = "";
                else
                    updateuser.Phone = model.Phone;

                if (model.Gender == null)
                    updateuser.Gender = "";
                else
                    updateuser.Gender = model.Gender;


                if (Request != null)
                {
                    HttpPostedFileBase file = Request.Files["UploadedFile"];

                    if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                    {
                        if (file.ContentLength > (100 * 1024))
                        {
                            return RedirectToAction("ErrorAvatar");
                        }
                        string fileName = file.FileName;
                        string fileContentType = file.ContentType;
                        byte[] fileBytes = new byte[file.ContentLength];
                        file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                        Random TenBienRanDom = new Random();

                        var filename = Path.GetFileName(file.FileName);
                        string tenfile = TenBienRanDom.Next(1, 100).ToString() + filename;
                        filename = Path.Combine(Server.MapPath("~/Content/image/upload/"), tenfile);
                        file.SaveAs(filename);
                        updateuser.ImageUrl = tenfile;
                    }

                }
                dbConn.Update(updateuser);
                //dbConn.Execute("update EmployeeInfo set XLiteID = '" + model.XLiteID + "',XLiteCode = '" + model.XLiteCode + "' WHERE UserName = '" + User.Identity.Name + "'");
                //UserProfile.Update(updateuser.UserName, updateuser.LastUpdatedDateTime,
                //updateuser.LastUpdatedUser,updateuser.CompanyID, updateuser.DepartmentID, updateuser.Phone, updateuser.Team, updateuser.Position, updateuser.Gender,updateuser.LevelID);
                return RedirectToAction("ChangeProfileSuccess");
            }
            return Json(new { success = false });
        }

        public ActionResult ChangeProfileSuccess()
        {
            return View();
        }

        #endregion
        #region AcountBank
        public ActionResult ReadAccountBank([DataSourceRequest] DataSourceRequest request)
        {
            var u = new Users();
            u.UserName = User.Identity.Name;
            return Json(Bank_Account.Get_BankAccounts(u.UserName).ToDataSourceResult(request));
        }


        public ActionResult CreateAccountBank([DataSourceRequest] DataSourceRequest request, Bank_Account b)
        {
            if (b != null && ModelState.IsValid)
            {

                try
                {
                    Bank_Account ba = Bank_Account.Get_BankAccounts(b.BankAccountNumber, b.BankBranchID);
                    if (ba == null)
                    {
                        b.UserName = User.Identity.Name;
                        b.CreatedUser = User.Identity.Name;
                        b.CreatedDatetime = DateTime.Now;
                        b.Save();
                        return Json(new { success = true });
                    }
                    else
                    {
                        ModelState.AddModelError("Bank Account", " Bank Account is exist");
                        return Json(new[] { b }.ToDataSourceResult(request, ModelState));
                    }
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("error", "");
                    return Json(new[] { b }.ToDataSourceResult(request, ModelState));
                }

            }
            return Json(new { success = true });
        }

        public ActionResult UpdateAccountBank([DataSourceRequest] DataSourceRequest request, Bank_Account b)
        {
            if (b != null && ModelState.IsValid)
            {
                Bank_Account ba = Bank_Account.CheckExist(b.BankAccountNumber, b.BankBranchID, b.ID);
                if (ba == null)
                {
                    try
                    {
                        b.UserName = User.Identity.Name;
                        b.LastUpdatedUser = User.Identity.Name;
                        b.LastUpdatedDateTime = DateTime.Now;
                        b.Update();
                        return Json(new { success = true });
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("BankAccountNumber", "BankAccountNumber is already existed");
                        return Json(new[] { b }.ToDataSourceResult(request, ModelState));
                    }
                }
                else
                {
                    ModelState.AddModelError("Bank Account", " Bank Account is exist");
                    return Json(new[] { b }.ToDataSourceResult(request, ModelState));
                }
            }
            return Json(new[] { b }.ToDataSourceResult(request, ModelState));

        }
        #endregion
        public FileResult Export([DataSourceRequest] DataSourceRequest request)
        {
            var dbConn = Helpers.OrmliteConnection.openConn();
            var currentUser = dbConn.SingleOrDefault<Users>("UserName={0}", User.Identity.Name);
            //Get the data representing the current grid state - page, sort and filter
            IEnumerable datas = ERPAPD.Models.Bank_Account.Get_BankAccounts(currentUser.UserName).ToDataSourceResult(request).Data;
            //Create new Excel workbook
            FileStream fs = new FileStream(Server.MapPath(@"~\ExportExcelFile\Bank_Account.xls"), FileMode.Open, FileAccess.Read);
            var workbook = new HSSFWorkbook(fs, true);

            //Create new Excel sheet
            var sheet = workbook.GetSheet("Bank Account");

            int rowNumber = 1;

            //Populate the sheet with values from the grid data
            foreach (ERPAPD.Models.Bank_Account data in datas)
            {
                //Create a new row
                var row = sheet.CreateRow(rowNumber++);
                //Set values for the cells
                row.CreateCell(0).SetCellValue(data.BankAccountNumber);
                row.CreateCell(1).SetCellValue(data.BankAccountName);
                row.CreateCell(2).SetCellValue(data.BankName);
                row.CreateCell(3).SetCellValue(data.BankBranchName);
                row.CreateCell(4).SetCellValue(data.isDefault);
                row.CreateCell(5).SetCellValue(data.CreatedDatetime.ToString());
                row.CreateCell(6).SetCellValue(data.CreatedUser);
                row.CreateCell(7).SetCellValue(data.LastUpdatedDateTime.ToString());
                row.CreateCell(8).SetCellValue(data.LastUpdatedUser);
            }

            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            //Return the result to the end user
            log.Info("Export organization");
            return File(output.ToArray(), //The binary data of the XLS file
                "application/vnd.ms-excel", //MIME type of Excel files
                "Bank_Account_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user


        }
    }
}