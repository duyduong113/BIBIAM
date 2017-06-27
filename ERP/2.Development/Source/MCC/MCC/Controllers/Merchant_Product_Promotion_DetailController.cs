using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using BIBIAM.Core.Entities;
using System.Data;
using BIBIAM.Core.Data.DataObject;
using System.Text.RegularExpressions;
using System.Text;

namespace MCC.Controllers
{
    [Authorize]
    public class Merchant_Product_Promotion_DetailController : CustomController
    {
        public ActionResult Index()
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                return View();
            }
            return RedirectToAction("NoAccess", "Error");
        }
        public ActionResult Read([DataSourceRequest] DataSourceRequest request, string ma_km)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                using (var db = MCC.Helpers.OrmliteConnection.openConn())
                {
                    var data = db.SqlList<Merchant_Product_Promotion_Detail>("exec _p_Get_Promotion_Detail {0},{1}".Params(ma_km, isAdmin ? "All" : currentUser.ma_gian_hang));
                    return Json(data.ToDataSourceResult(request));
                }
            }
            return RedirectToAction("NoAccess", "Error");
        }
        public ActionResult ReadToAddPromotion([DataSourceRequest] DataSourceRequest request, string ma_km)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                using (var db = MCC.Helpers.OrmliteConnection.openConn())
                {
                    var data = db.SqlList<Merchant_Product_Promotion_Detail>("exec _p_Get_Product_To_Promotion {0},{1}".Params(ma_km, isAdmin ? "All" : currentUser.ma_gian_hang));
                    return Json(data.ToDataSourceResult(request));
                }
            }
            return RedirectToAction("NoAccess", "Error");
        }
        private string ConvertToUnsign(string str)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = str.Normalize(NormalizationForm.FormD);
            string s = regex.Replace(temp, String.Empty)
                        .Replace('\u0111', 'd').Replace('\u0110', 'D');
            regex = new Regex("[;,\t\r ]");
            return regex.Replace(s, string.Empty).ToUpper();
        }
        public ActionResult Search([DataSourceRequest] DataSourceRequest request, List<string> condition, List<string> hierarchies)//0:makm,1:gia_tri,2:loai,3:ten_san_pham,4:(bool)checkbox sp chua dc km
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                List<Merchant_Product_Promotion_Detail> AllData = new List<Merchant_Product_Promotion_Detail>();
                if (condition.Count != 7)
                {
                    return Json(new List<Merchant_Product_Promotion_Detail>().ToDataSourceResult(request));
                }
                try
                {
                    string makm = condition.ElementAt(0);
                    float gia_tri = float.Parse(condition.ElementAt(1));
                    string loai = condition.ElementAt(2);
                    string ten_san_pham = condition.ElementAt(3);
                    ten_san_pham = ConvertToUnsign(ten_san_pham);
                    bool checkbox = bool.Parse(condition.ElementAt(4)); // true : select, false dont select
                    string startTime = "";
                    string endTime = "";
                    if (checkbox)
                    {
                        startTime = condition.ElementAt(5);
                        endTime = condition.ElementAt(6);
                    }
                    float minPrice = 50000;
                    string hierarchiesString = "";
                    if (hierarchies != null)
                    {
                        foreach (string index in hierarchies)
                        {
                            if (!String.IsNullOrEmpty(index))
                                hierarchiesString += index + ",";
                        }
                        hierarchiesString = hierarchiesString.Length > 0 ? hierarchiesString.Substring(0, hierarchiesString.Length - 1) : "";
                    }

                    using (var db = MCC.Helpers.OrmliteConnection.openConn())
                    {
                        AllData = db.SqlList<Merchant_Product_Promotion_Detail>("exec _p_Get_Product_To_Promotion {0},{1},{2},{3},{4}".Params(makm, isAdmin ? "All": currentUser.ma_gian_hang, hierarchiesString, startTime, endTime));
                    }
                    List<Merchant_Product_Promotion_Detail> data = new List<Merchant_Product_Promotion_Detail>();
                    if (checkbox)
                    {
                        if (String.IsNullOrEmpty(ten_san_pham))
                        {
                            data = AllData.Where(s => (loai == "tien" ? s.don_gia - gia_tri : s.don_gia * (1 - gia_tri / 100)) > minPrice
                                            && String.IsNullOrEmpty(s.ma_chuong_trinh_km) == checkbox
                                       ).ToList();
                        }
                        else
                        {
                            data = AllData.Where(s => ConvertToUnsign(s.ten_san_pham).Contains(ten_san_pham)
                                            && (loai == "tien" ? s.don_gia - gia_tri : s.don_gia * (1 - gia_tri / 100)) > minPrice
                                            && String.IsNullOrEmpty(s.ma_chuong_trinh_km) == checkbox
                                       ).ToList();
                        }

                    }
                    else
                    {
                        if (String.IsNullOrEmpty(ten_san_pham))
                        {
                            data = AllData.Where(s => (loai == "tien" ? s.don_gia - gia_tri : s.don_gia * (1 - gia_tri / 100)) > minPrice
                                      ).ToList();
                        }
                        else
                        {
                            data = AllData.Where(s => ConvertToUnsign(s.ten_san_pham).Contains(ten_san_pham)
                                           && (loai == "tien" ? s.don_gia - gia_tri : s.don_gia * (1 - gia_tri / 100)) > minPrice
                                      ).ToList();
                        }
                    }
                    return Json(data.ToDataSourceResult(request));
                }
                catch (Exception)
                {
                    return Json(new List<Merchant_Product_Promotion_Detail>().ToDataSourceResult(request));
                }
            }
            return RedirectToAction("NoAccess", "Error");
        }
    }
}
