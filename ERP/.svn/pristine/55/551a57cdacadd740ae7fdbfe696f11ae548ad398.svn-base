using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ServiceStack.OrmLite;

namespace ERPAPD.Models
{
    public class DC_Location_District
    {
        #region Members
        [Required]
        [StringLengthAttribute(5)]
        public string DistrictID { get; set; }
        [Required]
        [StringLengthAttribute(128)]
        public string DistrictName { get; set; }

        public string CityID { get; set; }

        public bool Active { get; set; }


        public DateTime RowCreatedTime { get; set; }

        public string RowCreatedUser { get; set; }

        public DateTime? RowLastUpdatedTime { get; set; }

        public string RowLastUpdatedUser { get; set; }

        #endregion
        [Ignore]
        public string RegionName
        {
            get;
            set;
        }
        [Ignore]
        public string CountryName { get; set; }
        [Ignore]
        public string AliasName { get; set; }
        [Ignore]
        public string CityName { get; set; }
        public static List<DropListDown> GetListDistrict(string Region)
        {
            string PROCEDURE = "select distinct DistrictID,DistrictName from DC_Location_District  where Active = 1 and RegionID='" + Region.Replace("'", "''") + "'";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DropListDown
            {
                Text = row["DistrictName"].ToString(),
                Value = row["DistrictID"].ToString(),
            }).ToList();
        }
        public static List<DC_Location_District> GetDC_Location_Districts(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_Location_DistrictDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_Location_District
            {
                DistrictID = row["DistrictID"].ToString(),
                DistrictName = row["DistrictName"].ToString(),
                CityID = row["CityID"].ToString(),
                CityName = row["CityName"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                RegionName = row["RegionName"].ToString(),
                CountryName = row["CountryName"].ToString(),
                AliasName = row["AliasName"].ToString(),
            }).ToList();
        }

    }
}