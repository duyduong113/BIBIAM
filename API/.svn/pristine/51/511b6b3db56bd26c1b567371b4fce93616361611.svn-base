using System;
using System.Web.Http;
using ERP_API.Models;
using ERP_API.Providers;


namespace ERP_API.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        public User Login(string username, string password)
        {
            try
            {
                User loginUser = new User();
                if (loginUser.Login(username, password))
                {
                    return loginUser.GetUserByID(username);
                }
                else return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet]
        public string UpdateUser(string userid, string fullname, string email, string phone, string password, string newpassword, string retypepassword)
        {
            try
            {
                User currentUser = new User().GetUserByID(userid);
                currentUser.FullName = !string.IsNullOrEmpty(fullname) ? fullname : currentUser.FullName;
                currentUser.Email = !string.IsNullOrEmpty(email) ? email : currentUser.Email;
                currentUser.Phone = !string.IsNullOrEmpty(phone) ? phone : currentUser.Phone;
                if (string.IsNullOrEmpty(password))
                {
                    currentUser.Password = "";
                    currentUser.RowUpdatedBy = userid;
                    currentUser.RowUpdatedAt = DateTime.Now;
                    currentUser.Update();
                }
                else
                {
                    if (string.IsNullOrEmpty(newpassword))
                    {
                        return "Vui lòng nhập mật khẩu mới!";
                    }
                    else if (string.IsNullOrEmpty(retypepassword))
                    {
                        return "Vui lòng nhập lại mật khẩu mới!";
                    }
                    else if (newpassword != retypepassword)
                    {
                        return "Mật khẩu mới không giống nhau!";
                    }
                    if (SqlHelper.GetMd5Hash(password) != currentUser.Password)
                    {
                        return "Mật khẩu cũ không đúng!";
                    }
                    else
                    {
                        currentUser.Password = newpassword;
                        currentUser.RowUpdatedBy = userid;
                        currentUser.RowUpdatedAt = DateTime.Now;
                        currentUser.Update();
                    }
                }
                return "true";
            }
            catch (Exception ex)
            {
                return "Tài khoản không tồn tại " + ex.Message.ToString();
            }
        }
    }
}
