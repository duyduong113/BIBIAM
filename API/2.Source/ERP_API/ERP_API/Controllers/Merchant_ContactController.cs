using System;
using System.Collections.Generic;
using BIBIAM.Core.Data.DataObject;
using BIBIAM.Core.Entities;
using System.Data;
using Newtonsoft.Json;
using System.Web.Http;
namespace ERP_API.Controllers
{
    public class Merchant_ContactController : ApiController
    {
        [HttpGet]
        public string GetAll(string key)
        {
            try
            {
                if (AllConstant.KeyAPI == key)
                {
                    DataTable dt = new Merchant_Contact_DAO().ReadAll(AllConstant.ERPConnectionString);
                    string st = JsonConvert.SerializeObject(dt);
                    return st;
                }
                return "error_key";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        [HttpGet]
        public string Delete(string key, string data)
        {
            if (AllConstant.KeyAPI == key)
            {
                string[] separators = { "@@" };
                string[] ids = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                return new Merchant_Contact_DAO().Delete(ids, AllConstant.ERPConnectionString);
            }
            else
                return "error_key";
        }
        [HttpGet]
        public string UpSert(string key, string data, string UserName, string Type)
        {
            if (AllConstant.KeyAPI == key)
            {
                List<Merchant_Contact> lstProdInfo = JsonConvert.DeserializeObject<List<Merchant_Contact>>(data) as List<Merchant_Contact>;
                return new Merchant_Contact_DAO().UpSert(lstProdInfo, UserName, Type, AllConstant.ERPConnectionString);
            }
            return "error_key";
        }
    }
}
