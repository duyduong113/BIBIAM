using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BIBIAM.Core.Entities;
using BIBIAM.Core.Data.Providers;
using System.Data.SqlClient;
using ServiceStack.OrmLite;

namespace BIBIAM.Core.Data.DataObject
{
    public class Merchant_DAO
    {
        public DataTable ReadAll(string connectionString)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            return new SqlHelper(connectionString).ExecuteString("select * from Merchant_Info order by id desc", param);
        }
        public DataTable ReadById(string id, string connectionString)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@id", id));
            return new SqlHelper(connectionString).ExecuteString("select * from Merchant_Info where id = @id order by id desc", param);
        }   
    }
}
