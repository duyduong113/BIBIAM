using System;
using System.Collections.Generic;
using BIBIAM.Core.Data.DataObject;
using BIBIAM.Core.Entities;
using System.Data;
using Newtonsoft.Json;
using System.Web.Http;

namespace ERP_API.Controllers
{
    public class HierarchyController : ApiController
    {
        [HttpGet]
        public string GetAll(string key)
        {
            try
            {
                if (AllConstant.KeyAPI == key)
                {
                    DataTable dt = new Hierarchy_DAO().ReadAll(AllConstant.ERPConnectionString);
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
                return new Hierarchy_DAO().Delete(ids, AllConstant.ERPConnectionString);
            }
            else
                return "error_key";
        }
        [HttpGet]
        public string UpSert(string key, string data, string UserName, string Type)
        {
            if (AllConstant.KeyAPI == key)
            {
                List<Hierarchy> lstHierarchy = JsonConvert.DeserializeObject<List<Hierarchy>>(data) as List<Hierarchy>;
               return  new Hierarchy_DAO().UpSert(lstHierarchy, UserName,Type, AllConstant.ERPConnectionString);
            }
            else
            {
                return "error_key";
            }

        }
    }
}