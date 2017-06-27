using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using ERP_API.Models;
using ERP_API.Providers;
using ERP_API.Results;
using System.Data;
using System.Data.SqlClient;
using BIBIAM.Core.Data;
using System.Net;

namespace ERP_API.Controllers
{
    public class ActivationAccountController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Activation(string key, string email, string username, string ten_gian_hang)
        {
            using (SqlConnection conn = new SqlConnection(AllConstant.MCCDBName))
            {
                //SqlParameter[] param = { new SqlParameter("@ActivationCode", key), new SqlParameter("@UserName", username), new SqlParameter("@MerchantName", ten_gian_hang), new SqlParameter("@Email", email), new SqlParameter("@Date", DateTime.Now) };
                //var activationcode = BIBIAM.Core.Data.Providers.SqlHelper.getValueProcWithParameter("p_CheckExist_UserActivation", param, conn);

                List<SqlParameter> lstParameter = new List<SqlParameter>();
                lstParameter.Add(new SqlParameter("@ActivationCode", key));
                lstParameter.Add(new SqlParameter("@UserName", username));
                lstParameter.Add(new SqlParameter("@MerchantName", ten_gian_hang));
                lstParameter.Add(new SqlParameter("@Email", email));
                DataTable dt = new SqlHelper().ExecuteQuery("p_CheckExist_UserActivation", lstParameter);
                if (dt.Rows.Count > 0)
                {
                    //await SignInAsync(user, isPersistent: false);
                    //return RedirectToAction("Index", "Home");
                    new SqlHelper().ExecuteNoneQuery("update tw_User set active=1 where name='" + username + "'", new List<SqlParameter>(), CommandType.Text);
                    var response = Request.CreateResponse(HttpStatusCode.Moved);
                    response.Headers.Location = new Uri(AllConstant.Url + "Account/AutoLogin?UserName=" + username);
                    return response;
                }
                return null;
            }
        }
    }
}
