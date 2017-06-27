using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    [Authorize]
    public class ChangePasswordController : CustomController
    {
        public ChangePasswordController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public ChangePasswordController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        //
        // GET: /ChangePassword/
        public ActionResult Index()
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            return View();
        }

        [HttpPost]
        public ActionResult Confirm(string oldPassword, string newPassword, string confirmNewPassword)
        {
            try
            {
                bool hasPassword = HasPassword();

                if (newPassword == confirmNewPassword)
                {
                    //string pattern = @"^(?=.*[0-9])(?=.*[!@#$%^&*])[0-9a-zA-Z!@#$%^&*0-9]{6,}$";
                    if (newPassword.Length < 6)
                    {
                        return Json(new { success = false, error = "Your password must be longer 6 characters" });
                    }
                    else
                    {
                        if (hasPassword)
                        {
                            IdentityResult result = UserManager.ChangePassword(User.Identity.GetUserId(), oldPassword, newPassword);
                            if (result.Succeeded)
                            {
                                return Json(new { success = true });
                            }
                            else
                            {
                                return Json(new { success = false, error = AddErrors(result) });
                            }
                        }
                        else
                        {
                            IdentityResult result = UserManager.AddPassword(User.Identity.GetUserId(), newPassword);
                            if (result.Succeeded)
                            {
                                return Json(new { success = true });
                            }
                            else
                            {
                                return Json(new { success = false, error = AddErrors(result) });
                            }
                        }
                    }
                }
                else
                {
                    return Json(new { success = false, error = "Confirm new password not match" });
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
            }
        }

        private string AddErrors(IdentityResult result)
        {
            var errors = string.Empty;
            foreach (var error in result.Errors)
            {
                errors += error;
            }
            return errors;
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
    }
}