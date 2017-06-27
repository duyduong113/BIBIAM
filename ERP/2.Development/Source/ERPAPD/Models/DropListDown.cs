using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
//using WebMatrix.WebData;
using System.Linq;
namespace ERPAPD.Models
{
    public class DropListDown
    {
        public string Value { get; set; }
        public string Text { get; set; }

        public static List<DropListDown> GetHIERARCHYBY_list(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectHIERARCHiesDynamic";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = whereCondition;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrderByExpression";
            parameters[i].Value = orderByExpression;


            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);

            return dt.AsEnumerable().Select(row => new DropListDown
            {

                Text = row["DESCRIPTION"].ToString(),
                Value = row["ID"].ToString(),
            }).ToList();
        }

        public static List<DropListDown> GetMetric(string KPIID)
        {
            string PROCEDURE = "p_SelectMetric";
            SqlParameter[] parameters = new SqlParameter[1];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@KPIID";
            parameters[i].Value = KPIID;
            i++;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DropListDown
            {

                Text = row["Name"].ToString(),
                Value = row["MetricID"].ToString(),
            }).ToList();
        }

    }
}