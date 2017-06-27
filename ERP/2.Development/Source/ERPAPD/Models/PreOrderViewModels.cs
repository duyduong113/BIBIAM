using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Kendo.Mvc.UI;
using Kendo.Mvc;
using System.ComponentModel;
using ServiceStack.OrmLite;
namespace ERPAPD.Models
{
    public class PreOrderViewModels
    {
        public static DataSourceResult GetAll_PreOrder_ProductChange_Dynamic(DataSourceRequest request, string whereCondition)
        {
            List<string> sort = new List<string>();
            if (request.Sorts.Any())
            {
                foreach (SortDescriptor sortDescriptor in request.Sorts)
                {
                    if (sortDescriptor.SortDirection == ListSortDirection.Ascending)
                    {
                        sort.Add(sortDescriptor.Member + " ASC");
                    }
                    else
                    {
                        sort.Add(sortDescriptor.Member + " DESC");
                    }
                }
            }

            string sortString = string.Join(",", sort.Select(s => s));
            var order = (!String.IsNullOrEmpty(sortString) ? (" Order By " + sortString) : "");
            var lst = new List<Deca_Order_Header>();
            using (var dbConn = Helpers.OrmliteConnection.openConn())
                lst = dbConn.SqlList<Deca_Order_Header>("EXEC p_SelectDeca_Order_Header_ProductChange_ViewAll_Dynamic  '" + request.Page + "', '" + request.PageSize + "', N'" + whereCondition + "', '" + order + "'");
            request.Filters = null;
            DataSourceResult result = new DataSourceResult();
            result.Data = lst;
            result.Total = lst.Count > 0 ? lst[0].RowCount : 0;
            return result;
        }
        public static List<Deca_Order_ExcelModel> GetAll_PreOrder_ProductChange_ExportExcel(string whereCondition)
        {


            var lst = new List<Deca_Order_ExcelModel>();
            using (var dbConn = Helpers.OrmliteConnection.openConn())
                lst = dbConn.SqlList<Deca_Order_ExcelModel>("EXEC p_SelectDeca_Order_Header_ProductChange_ExportExcel  @WHERE, @Row", new { WHERE = whereCondition, Row = "10000" });
            return lst;
        }
    }
}