using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using CMS.Models;
using ServiceStack.OrmLite;
using System.IO;

namespace CMS.Controllers
{
    [Authorize]
    //[RequireHttps]
    public class AccountController : BaseController
    {
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
            if (!User.Identity.IsAuthenticated)
            {
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }
            else
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var currentUser = dbConn.SingleOrDefault<Users>("name={0}", User.Identity.Name);
                    if (currentUser != null && !String.IsNullOrEmpty(currentUser.homePage))
                    {
                        return RedirectToAction("Index", currentUser.homePage);
                    }
                }
                return RedirectToLocal("Home");
            }
        }

        [AllowAnonymous]
        public ActionResult ForgotPass()
        {
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
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    using (var dbConn = Helpers.OrmliteConnection.openConn())
                    {
                        var currentUser = dbConn.SingleOrDefault<Users>("name={0}", model.UserName);
                        if (currentUser != null && !String.IsNullOrEmpty(currentUser.homePage))
                        {
                            return RedirectToAction("Index", currentUser.homePage);
                        }
                    }
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
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
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var exist = dbConn.SingleOrDefault<Users>("email={0}", model.Email);
                    if (exist == null)
                    {
                        var user = new ApplicationUser() { UserName = model.UserName, PhoneNumber = model.PhoneNumber, Email = model.Email };
                        var result = await UserManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            var newUser = new Users();
                            newUser.name = model.UserName;
                            newUser.email = model.Email;
                            newUser.phone = model.PhoneNumber;
                            newUser.fullName = model.FullName;
                            newUser.registerAt = DateTime.Now;
                            newUser.active = true;
                            newUser.vendorAuth = "";
                            newUser.userKey = user.Id;
                            newUser.createdAt = DateTime.Now;
                            newUser.createdBy = "administrator";
                            dbConn.Insert(newUser);
                            Int64 userId = (Int64)dbConn.GetLastInsertId();

                            var groupId = dbConn.Scalar<Int64>("select id from UserGroup where name = 'guest'");
                            if (groupId > 0)
                            {
                                var userInGroup = new UserInGroup();
                                userInGroup.userId = userId;
                                userInGroup.groupId = groupId;
                                userInGroup.createdAt = DateTime.Now;
                                userInGroup.createdBy = "administrator";
                                dbConn.Insert(userInGroup);
                            }

                            string body = string.Empty;
                            using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/EmailTemplate/newUserTemplate.html")))
                            {
                                body = reader.ReadToEnd();
                            }
                            body = body.Replace("{username}", model.UserName);
                            body = body.Replace("{fullname}", model.FullName);
                            body = body.Replace("{phone}", model.PhoneNumber);
                            body = body.Replace("{email}", model.Email);

                            

                            await SignInAsync(user, isPersistent: false);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            AddErrors(result);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email is existed");
                    }
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
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
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
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
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
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
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
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
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
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = Helpers.RemoveVietNameChar.Remove(loginInfo.DefaultUserName).ToLower() });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ForgotPass(string username, string email)
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var aspUser = UserManager.FindByName(username);
                var exist = dbConn.SingleOrDefault<Users>("name={0} and email={1}", username, email);
                if (exist != null && aspUser != null)
                {
                    string body = string.Empty;
                    using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/EmailTemplate/resetPassUserTemplate.html")))
                    {
                        body = reader.ReadToEnd();
                    }

                    string newPass = Helpers.RandomString.Generate(8);
                    body = body.Replace("{fullName}", exist.fullName);
                    body = body.Replace("{newPass}", newPass);

                    UserManager.RemovePassword(aspUser.Id);
                    IdentityResult result = UserManager.AddPassword(aspUser.Id, newPass);
                    if (result.Succeeded)
                    {
                           }
                    else
                    {
                        return Json(new { success = false, error = AddErrorss(result) });
                    }
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, error = "Sorry, we not found your account" });
                }
            }
        }

        private string AddErrorss(IdentityResult result)
        {
            return String.Join(",", result.Errors.Select(s => s));
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

                using (var dbConn = Helpers.OrmliteConnection.openConn())
                {
                    var exist = dbConn.SingleOrDefault<Users>("email={0}", model.Email);
                    if (exist == null)
                    {
                        var user = new ApplicationUser() { UserName = Helpers.RemoveVietNameChar.Remove(model.UserName).ToLower(), PhoneNumber = model.PhoneNumber, Email = model.Email };
                        var result = await UserManager.CreateAsync(user);
                        if (result.Succeeded)
                        {
                            var newUser = new Users();
                            newUser.name = Helpers.RemoveVietNameChar.Remove(model.UserName).ToLower();
                            newUser.email = model.Email;
                            newUser.phone = model.PhoneNumber;
                            newUser.fullName = model.FullName;
                            newUser.registerAt = DateTime.Now;
                            newUser.active = true;
                            newUser.vendorAuth = info.Login.LoginProvider;
                            newUser.userKey = user.Id;
                            newUser.createdAt = DateTime.Now;
                            newUser.createdBy = "administrator";
                            dbConn.Insert(newUser);

                            Int64 userId = (Int64)dbConn.GetLastInsertId();

                            var groupId = dbConn.Scalar<Int64>("select id from UserGroup where name = 'guest'");
                            if (groupId > 0)
                            {
                                var userInGroup = new UserInGroup();
                                userInGroup.userId = userId;
                                userInGroup.groupId = groupId;
                                userInGroup.createdAt = DateTime.Now;
                                userInGroup.createdBy = "administrator";
                                dbConn.Insert(userInGroup);
                            }

                            result = await UserManager.AddLoginAsync(user.Id, info.Login);
                            if (result.Succeeded)
                            {
                                string body = string.Empty;
                                using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/EmailTemplate/newUserTemplate.html")))
                                {
                                    body = reader.ReadToEnd();
                                }
                                body = body.Replace("{username}", Helpers.RemoveVietNameChar.Remove(model.UserName).ToLower());
                                body = body.Replace("{fullname}", model.FullName);
                                body = body.Replace("{phone}", model.PhoneNumber);
                                body = body.Replace("{email}", model.Email);

                               
                                await SignInAsync(user, isPersistent: false);
                                return RedirectToLocal(returnUrl);
                            }
                        }

                        AddErrors(result);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email is existed");
                    }
                }
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                var user = dbConn.SingleOrDefault<Users>("name={0}", User.Identity.Name);
                if (user != null && !String.IsNullOrEmpty(user.homePage))
                {
                    return RedirectToAction("Index", user.homePage);
                }
            }
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
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
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
            var user = UserManager.FindById(User.Identity.GetUserId());
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

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}