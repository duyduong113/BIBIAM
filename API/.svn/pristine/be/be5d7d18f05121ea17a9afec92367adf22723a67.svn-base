using System;
using System.Collections.Generic;
using BIBIAM.Core.Data.DataObject;
using BIBIAM.Core.Entities;
using System.Data;
using Newtonsoft.Json;
using System.Web.Http;

namespace ERP_API.Controllers
{
    public class Product_PromotionController : ApiController
    {
        [HttpGet]
        public string GetAll(string key)
        {
            try
            {
                if (AllConstant.KeyAPI == key)
                {
                    DataTable dt = new Product_Promotion_DAO().ReadAll(AllConstant.ERPConnectionString);
                    string st = JsonConvert.SerializeObject(dt);
                    return st;
                }
                return "";
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
                return new Product_Promotion_DAO().Delete(ids, AllConstant.ERPConnectionString);
                
            }
            else
                return "error_key";
        }
        [HttpGet]
        public string UpSert(string key, string data, string UserName, string Type)
        {
            if (AllConstant.KeyAPI == key)
            {
                List<Product_Promotion> lstProdInfo = JsonConvert.DeserializeObject<List<Product_Promotion>>(data) as List<Product_Promotion>;
                return new Product_Promotion_DAO().UpSert(lstProdInfo, UserName, Type, AllConstant.ERPConnectionString);
            }
            else
            {
                return "error_key";
            }                   
        }
    }
}