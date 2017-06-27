using System;
using System.Collections.Generic;
using BIBIAM.Core.Data.DataObject;
using BIBIAM.Core.Entities;
using System.Data;
using Newtonsoft.Json;
using System.Web.Http;

namespace ERP_API.Controllers
{
    public class Product_PropertyController : ApiController
    {
        [HttpGet]
        public string GetAll(string key)
        {
            try
            {
                if (AllConstant.KeyAPI == key)
                {
                    DataTable dt = new Product_Property_DAO().ReadAll(AllConstant.ERPConnectionString);
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
        public bool Delete(string key, string data)
        {
            if (AllConstant.KeyAPI == key)
            {
                string[] separators = { "@@" };
                string[] ids = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                new Product_Property_DAO().Delete(ids, AllConstant.ERPConnectionString);
                return true;
            }
            else
                return false;
        }
        [HttpGet]
        public void UpSert(string key, string data, string UserName, string Type)
        {
            if (AllConstant.KeyAPI == key)
            {
                List<Product_Property> lstProduct_Property = JsonConvert.DeserializeObject<List<Product_Property>>(data) as List<Product_Property>;
                new Product_Property_DAO().UpSert(lstProduct_Property, UserName, Type, AllConstant.ERPConnectionString);
            }

        }
    }
}