using DevTrends.MvcDonutCaching;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a DC_ProductList_Detail.
    /// </summary>
    public class InventoryMeta
    {
        public int month;
    }
    public class DC_ProductList_Detail
    {
        #region Members
        public string ProductListID { get; set; }

        public string MSIN { get; set; }
        public string Name { get; set; }

        public string Note { get; set; }

        public string Status { get; set; }
        public int Priority { get; set; }

        public int RowID { get; set; }

        public DateTime RowCreatedTime { get; set; }

        public string RowCreatedUser { get; set; }

        public DateTime RowLastUpdatedTime { get; set; }

        public string RowLastUpdatedUser { get; set; }
        public string ProductListName { get; set; }
        public string ProductListStatus { get; set; }
        public string ImportNote { get; set; }

        public string BrandName { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public long Price { get; set; }
        public long ReferPrice { get; set; }
        public double Margin { get; set; }
        public string SMSCode { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime InactivedDate { get; set; }
        public float Commission { get; set; }
        public string MSINexistedColor { get; set; }

        #endregion

        public DC_ProductList_Detail()
        { }


        public int Save()
        {
            string PROCEDURE = "p_SaveDC_ProductList_Detail";
            SqlParameter[] parameters = new SqlParameter[9];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ProductListID";
            parameters[i].Value = this.ProductListID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@MSIN";
            parameters[i].Value = this.MSIN;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Note";
            parameters[i].Value = this.Note;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this.Status;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Priority";
            parameters[i].Value = this.Priority;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@EffectiveDate";
            parameters[i].Value = this.EffectiveDate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedTime";
            parameters[i].Value = this.RowCreatedTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedUser";
            parameters[i].Value = this.RowCreatedUser;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Commission";
            parameters[i].Value = this.Commission;
            i++;

            int result =  SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            var cacheManager = new OutputCacheManager();
            cacheManager.RemoveItems("TelesalePluginCode", "ProductList_Detail_Read");
            return result;
        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateDC_ProductList_Detail";
            SqlParameter[] parameters = new SqlParameter[10];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ProductListID";
            parameters[i].Value = this.ProductListID;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@MSIN";
            parameters[i].Value = this.MSIN;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Note";
            parameters[i].Value = this.Note;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Status";
            parameters[i].Value = this.Status;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Priority";
            parameters[i].Value = this.Priority;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@EffectiveDate";
            parameters[i].Value = this.EffectiveDate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@InactivedDate";
            parameters[i].Value = this.InactivedDate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedTime";
            parameters[i].Value = this.RowLastUpdatedTime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedUser";
            parameters[i].Value = this.RowLastUpdatedUser;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Commission";
            parameters[i].Value = this.Commission;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Delete()
        {
            string PROCEDURE = "p_DeleteDC_ProductList_Detail";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ProductListID";
            parameters[0].Value = ProductListID;

            parameters[1] = new SqlParameter();
            parameters[1].ParameterName = "@MSIN";
            parameters[1].Value = MSIN;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);

        }

        public static DC_ProductList_Detail GetDC_ProductList_Detail(string ProductListID, string MSIN)
        {
            string PROCEDURE = "p_SelectDC_ProductList_Detail";
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ProductListID";
            parameters[0].Value = ProductListID;
            parameters[1] = new SqlParameter();
            parameters[1].ParameterName = "@MSIN";
            parameters[1].Value = MSIN;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_ProductList_Detail
            {
                ProductListID = row["ProductListID"].ToString(),
                MSIN = row["MSIN"].ToString(),
                Note = row["Note"].ToString(),
                Status = row["Status"].ToString(),
                Priority = Convert.ToInt32(row["Priority"].ToString()),
                EffectiveDate = Convert.ToDateTime(row["EffectiveDate"].ToString()),
                InactivedDate = Convert.ToDateTime(row["InactivedDate"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                Commission = float.Parse(row["Commission"].ToString())
            }).ToList().FirstOrDefault();
        }

        //public static List<DC_ProductList_Detail> GetDC_ProductList_Details(string whereCondition, string orderByExpression)
        //{
        //    string PROCEDURE = "p_SelectDC_ProductList_DetailsDynamic";
        //    SqlParameter[] parameters = new SqlParameter[2];
        //    int i = 0;
        //    parameters[i] = new SqlParameter();
        //    parameters[i].ParameterName = "@WhereCondition";
        //    parameters[i].Value = whereCondition;
        //    i++;
        //    parameters[i] = new SqlParameter();
        //    parameters[i].ParameterName = "@OrderByExpression";
        //    parameters[i].Value = orderByExpression;

        //    DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        //    return dt.AsEnumerable().Select(row => new DC_ProductList_Detail
        //    {
        //        ProductListID = row["ProductListID"].ToString(),
        //        MSIN = row["MSIN"].ToString(),
        //        Note = row["Note"].ToString(),
        //        Status = Convert.ToBoolean(row["Status"].ToString()),
        //        RowID = Convert.ToInt32(row["RowID"].ToString()),
        //        RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
        //        RowCreatedUser = row["RowCreatedUser"].ToString(),
        //        RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
        //        RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString()
        //    }).ToList();
        //}

        public static List<DC_ProductList_Detail> GetAllDC_ProductList_Details(string ProductListID)
        {
            string PROCEDURE = "p_SelectDC_ProductList_DetailsAll";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ProductListID";
            parameters[0].Value = ProductListID;


            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_ProductList_Detail
            {
                ProductListID = row["ProductListID"].ToString(),
                MSIN = row["MSIN"].ToString(),
                Name = row["Name"].ToString(),
                Note = row["Note"].ToString(),
                Status = row["Status"].ToString(),
                Priority = Convert.ToInt32(row["Priority"].ToString()),
                EffectiveDate = Convert.ToDateTime(row["EffectiveDate"].ToString()),
                InactivedDate = Convert.ToDateTime(row["InactivedDate"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                BrandName = row["BrandName"].ToString(),
                Category = row["Category"].ToString(),
                SubCategory = row["SubCategory"].ToString(),
                Price = long.Parse(row["Price"].ToString()),
                ReferPrice = long.Parse(row["ReferPrice"].ToString()),
                Margin = double.Parse(row["Margin"].ToString()),
                Installment = ParseMonth(row["Meta"].ToString()),
                Commission = float.Parse(row["Commission"].ToString())
            }).ToList();
        }

        public static List<DC_ProductList_Detail> GetAllDC_ProductList_DetailsProduct()
        {
            string PROCEDURE = "p_SelectDC_ProductList_DetailsAllProduct";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_ProductList_Detail
            {
                ProductListID = row["ProductListID"].ToString(),
                MSIN = row["MSIN"].ToString(),
                Name = row["Name"].ToString(),
                Note = row["Note"].ToString(),
                Status = row["Status"].ToString(),
                Priority = Convert.ToInt32(row["Priority"].ToString()),
                EffectiveDate = Convert.ToDateTime(row["EffectiveDate"].ToString()),
                InactivedDate = Convert.ToDateTime(row["InactivedDate"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                BrandName = row["BrandName"].ToString(),
                Category = row["Category"].ToString(),
                SubCategory = row["SubCategory"].ToString(),
                Price = long.Parse(row["Price"].ToString()),
                ReferPrice = long.Parse(row["ReferPrice"].ToString()),
                Margin = double.Parse(row["Margin"].ToString()),
                Installment = ParseMonth(row["Meta"].ToString()),
                Commission = float.Parse(row["Commission"].ToString())
            }).ToList();
        }

        //DungNT: add function load productlist detail
        public static List<DC_ProductList_Detail> GetAllDC_ProductList_DetailsProductByRegion(string Region)
        {
            string PROCEDURE = "p_SelectDC_ProductList_DetailsAllProductForTeleSale";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@Region";
            parameters[0].Value = Region;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_ProductList_Detail
            {
                MSIN = row["MSIN"].ToString(),
                Name = row["Name"].ToString(),
                Price = long.Parse(row["SellingPrice"].ToString()),
                Installment = ParseMonth(row["Meta"].ToString()),
                Commission = float.Parse(row["Commission"].ToString())
            }).ToList();
        }

        public static List<DC_ProductList_Detail> GetAllDC_ProductList_DetailsProductByRegionForCustomer(string Region,string CustomerID)
        {
            string PROCEDURE = "p_SelectDC_ProductList_DetailsAllProductForCustomer";

            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@Region";
            parameters[0].Value = Region;

            parameters[1] = new SqlParameter();
            parameters[1].ParameterName = "@CustomerID";
            parameters[1].Value = CustomerID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_ProductList_Detail
            {
                MSIN = row["MSIN"].ToString(),
                Name = row["Name"].ToString(),
                Price = long.Parse(row["SellingPrice"].ToString()),
                Installment = ParseMonth(row["Meta"].ToString()),
                Commission = float.Parse(row["Commission"].ToString()),
                MSINexistedColor = row["MSINexistedColor"].ToString()
            }).ToList();
        }


        public static List<DC_ProductList_Detail> GetAllDC_ProductList_Detail_CallActivities( )
        {
            string PROCEDURE = "p_SelectDC_ProductList_Detail_CallActivities";


            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_ProductList_Detail
            {
                ProductListName = row["ProductListName"].ToString(),
                MSIN = row["MSIN"].ToString(),
                Price = long.Parse(row["Price"].ToString()),
                Installment = ParseMonth(row["Meta"].ToString()),
                SMSCode = row["SMSCode"].ToString(),
                Name = row["Name"].ToString(),
            }).ToList();
        }
        public static int ParseMonth(string x)
        {
            try
            {
                return JsonConvert.DeserializeObject<InventoryMeta>(x).month;
            }
            catch
            {
                return 0;
            }
        }
        public static List<DC_ProductList_Detail> GetAllDC_ProductList_Detail_Export(string MSIN )
        {
            string PROCEDURE = "p_SelectDC_ProductList_Detail_Export";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@MSIN";
            parameters[0].Value = MSIN;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_ProductList_Detail
            {
                ProductListID = row["ProductListID"].ToString(),
                MSIN = row["MSIN"].ToString(),
                Name = row["Name"].ToString(),
                Note = row["Note"].ToString(),
                Status = row["Status"].ToString(),
                Priority = Convert.ToInt32(row["Priority"].ToString()),
                EffectiveDate = Convert.ToDateTime(row["EffectiveDate"].ToString()),
                InactivedDate = Convert.ToDateTime(row["InactivedDate"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                ProductListName = row["ProductListName"].ToString(),
                ProductListStatus = row["ProductListStatus"].ToString(),
                BrandName = row["BrandName"].ToString(),
                Category = row["Category"].ToString(),
                SubCategory = row["SubCategory"].ToString(),
                Price = long.Parse(row["Price"].ToString()),
                ReferPrice = long.Parse(row["ReferPrice"].ToString()),
                Margin = double.Parse(row["Margin"].ToString()),
                Installment = ParseMonth(row["Meta"].ToString()),
                Commission = float.Parse(row["Commission"].ToString())
            }).ToList();
        }

        public static List<DC_ProductList_Detail> GetMustSellProductList()
        {
            string PROCEDURE = "p_SelectDC_ProductList_Detail_MustSellList";


            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_ProductList_Detail
            {
                ProductListID = row["ProductListID"].ToString(),
                MSIN = row["MSIN"].ToString(),
                Name = row["Name"].ToString(),
                Note = row["Note"].ToString(),
                Status = row["Status"].ToString(),
                Priority = Convert.ToInt32(row["Priority"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                ProductListName = row["ProductListName"].ToString(),
                ProductListStatus = row["ProductListStatus"].ToString(),
                BrandName = row["BrandName"].ToString(),
                Category = row["Category"].ToString(),
                SubCategory = row["SubCategory"].ToString(),
                Price = long.Parse(row["Price"].ToString()),
                Installment = ParseMonth(row["Meta"].ToString()),
                SMSCode = row["SMSCode"].ToString(),
                EffectiveDate = Convert.ToDateTime(row["EffectiveDate"].ToString()),
            }).ToList();
        }


        public static DC_ProductList_Detail CheckMSIN(string MSIN)
        {
            string PROCEDURE = "select MSIN, [Status] from DW_DC_Inventory where MSIN = '" + MSIN.Replace("'", "''") + "'";
            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_ProductList_Detail
            {
                MSIN = row["MSIN"].ToString(),
                Status = row["Status"].ToString(),
            }).ToList().FirstOrDefault();
        }


        public static List<DC_ProductList_Detail> GetAllDC_ProductList_Details_Dynamic(string WhereCondition, string OrderByExpression)
        {
            string PROCEDURE = "p_SelectDC_ProductList_DetailsAllByListProductIDDynamic";

            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@WhereCondition";
            parameters[0].Value = WhereCondition;

            parameters[1] = new SqlParameter();
            parameters[1].ParameterName = "@OrderByExpression";
            parameters[1].Value = OrderByExpression;


            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_ProductList_Detail
            {
                ProductListID = row["ProductListID"].ToString(),
                MSIN = row["MSIN"].ToString(),
                Name = row["Name"].ToString(),
                Note = row["Note"].ToString(),
                Status = row["Status"].ToString(),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                RowCreatedTime = Convert.ToDateTime(row["RowCreatedTime"].ToString()),
                RowCreatedUser = row["RowCreatedUser"].ToString(),
                RowLastUpdatedTime = Convert.ToDateTime(row["RowLastUpdatedTime"].ToString()),
                RowLastUpdatedUser = row["RowLastUpdatedUser"].ToString(),
                BrandName = row["BrandName"].ToString(),
                Category = row["Category"].ToString(),
                SubCategory = row["SubCategory"].ToString(),
                Price = long.Parse(row["Price"].ToString()),
                Meta = getImage(row["Meta"].ToString()),
                MSRP = row["MSRP"].ToString(),
                Letter = row["Letter"].ToString(),
                //Nếu tháng = 0 thì lấy giá hiện tại. Nếu khác 0 thì lấy giá hiện tại / tháng ra giá TG
                Installment = GetInstallment(row["Meta"].ToString()) == 0 ? double.Parse(row["Price"].ToString()) : (double.Parse(row["Price"].ToString()) / GetInstallment(row["Meta"].ToString())),
                Month = GetInstallment(row["Meta"].ToString()),
                SMSCode = row["SMSCODE"].ToString(),
                CountI = int.Parse(row["RowNumber"].ToString()) - 1,
            }).ToList();
        }
        public double Month { get; set; }
        public int CountI { get; set; }

        public static double GetInstallment(string x)
        {
            try
            {
                return JsonConvert.DeserializeObject<InventoryMeta>(x).month;
            }
            catch
            {
                return 0;
            }
        }
        public class Image
        {
            public IEnumerable<ImageUrl> imgOriginals { get; set; }
        }
        public class ImageUrl
        {
            public String url { get; set; }
        }
        public static string getImage(string Meta)
        {
            string image = String.Empty;
            try
            {
                if (!String.IsNullOrEmpty(Meta))
                {
                    var data = JsonConvert.DeserializeObject<Image>(Meta);
                    if (data.imgOriginals.FirstOrDefault() != null)
                    {
                        image = data.imgOriginals.FirstOrDefault().url;
                    }
                }
            }
            catch (Exception e)
            {
                return "";
            }

            return image;
        }
        public double Installment { get; set; }

        public class InventoryMeta
        {
            public int month;
        }

        public string Meta { get; set; }
        public string MSRP { get; set; }
        public string Letter { get; set; }
    }
}
