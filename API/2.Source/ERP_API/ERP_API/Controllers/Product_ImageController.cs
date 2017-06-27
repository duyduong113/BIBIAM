using BIBIAM.Core.Data.DataObject;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using BIBIAM.Core.Entities;


namespace ERP_API.Controllers
{
    public class Product_ImageController : ApiController
    {
        [HttpGet]
        public string GetAll(string key)
        {
            try
            {
                if (AllConstant.KeyAPI == key)
                {
                    DataTable dt = new Product_Image_DAO().ReadAll(AllConstant.ERPConnectionString);
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
                var ids = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                return new Product_Image_DAO().Delete(ids,AllConstant.ERPConnectionString);
                
            }                                                                                     
            else
                return "error_key";
        }
        public string UpSert(string key, string data, string UserName, string Type)
        {
            if (AllConstant.KeyAPI == key)
            {
                List<Product_Image> lstProdImage = JsonConvert.DeserializeObject<List<Product_Image>>(data) as List<Product_Image>;
               return new Product_Image_DAO().UpSert(lstProdImage, UserName, Type, AllConstant.ERPConnectionString);
            }
            else
                return "error_key";
        }
    }
}
