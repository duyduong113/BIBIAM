using System;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using ERPAPD.Models;
using ERPAPD.Helpers;
using ServiceStack.OrmLite;

namespace ERPAPD.Controllers
{
    public class CRMExchangeRateController : CustomController
    {
        //
        // GET: /ExchangeRate/
        public ActionResult Index()
        {
            using (var dbConn = Helpers.OrmliteConnection.openConn())
            {
                if (asset.View)
                {
                    ViewData["AllowCreate"] = asset.Create;
                    ViewData["AllowUpdate"] = asset.Update;
                    ViewData["AllowDelete"] = asset.Delete;
                    ViewData["AllowExport"] = asset.Export;
                    var currencyExit = dbConn.Select<ti_gia_tien_te>("Select * from ti_gia_tien_te where tien_te_mac_dinh=1");
                    if (currencyExit.Count!=0)
                    {
                        ViewBag.currentCurrency = currencyExit.FirstOrDefault().ma_tien_te;
                    }
                    else
                    {
                        ViewBag.currentCurrency = Resources.Multi.CurencyCode;
                    }
                    return View("CRMExchangeRate");
                }
                else
                {
                    return RedirectToAction("NoAccessRights", "Error");
                }
            }
        }
        public ActionResult GetExchangeRate([DataSourceRequest] DataSourceRequest request, string fromCurrence)
        {
            return Json(KendoApplyFilter.KendoData<ti_gia_quy_doi>(request, "ChangeFrom='" + fromCurrence + "'"));
        }
        public ActionResult ExchangeRateCreate(string from)
        {
            try
            {
                var dbConn = Helpers.OrmliteConnection.openConn();
                var listCurrent = dbConn.Select<tien_te>();
                dbConn.ExecuteNonQuery("Delete from ti_gia_quy_doi where ChangeFrom='" + from + "'");
                bool flagCheckComma = false;
                var datacheckComa = Helpers.RestfulClient.GET("https://rate-exchange-1.appspot.com/currency?from=" + from + "&to=" + from, "");
                if (string.IsNullOrEmpty(datacheckComa))
                {
                    return Json(new { success = false, error = "Can't get Currency Exchange" });
                }
                string[] listSplitColonCheck = datacheckComa.Split(':');
                if (listSplitColonCheck[0].Contains("err"))
                {
                    datacheckComa = Helpers.RestfulClient.GET("https://rate-exchange-1.appspot.com/currency?from=" + from + "&to=" + from, "");
                }
                if (!listSplitColonCheck[0].Contains("err"))
                {
                    listSplitColonCheck = datacheckComa.Split(':');
                    string[] listSplitcheckComma = listSplitColonCheck[2].Split(',');
                    double amount = convertData(listSplitcheckComma[0].Replace('.', ','));
                    if(amount!=1)
                    {
                        flagCheckComma = true;
                    }
                }
                
                foreach (var item in listCurrent)
                {
                    

                    var data = Helpers.RestfulClient.GET("https://rate-exchange-1.appspot.com/currency?from=" + item.ma_tien_te + "&to=" + from, "");
                    if (string.IsNullOrEmpty(data))
                    {
                        return Json(new { success = false, error = "Can't get Currency Exchange" });
                    }
                    string[] listSplitColon = data.Split(':');
                    if(listSplitColon[0].Contains("err"))
                    {
                        data = Helpers.RestfulClient.GET("https://rate-exchange-1.appspot.com/currency?from=" + item.ma_tien_te + "&to=" + from, "");
                    }
                    listSplitColon = data.Split(':');
                    if (listSplitColon[0].Contains("err"))
                    {
                        continue;
                    }
                    string[] listSplitComma = listSplitColon[2].Split(',');
                    double amount;
                    if(flagCheckComma)
                    {
                        amount = convertData(listSplitComma[0]);
                    }
                    else
                    {
                        amount = convertData(listSplitComma[0].Replace('.',','));
                    }
                    //if(amount>100)
                    //    amount = convertData(listSplitComma[0]);
                    var exchangeRate = new ti_gia_quy_doi();
                    exchangeRate.ma_tien_te = item.ma_tien_te;
                    exchangeRate.don_vi_quy_doi = 1;
                    exchangeRate.so_tien_quy_doi = Math.Round(amount,6);
                    exchangeRate.nguoi_quy_doi = currentUser.UserName;
                    exchangeRate.ngay_quy_doi = DateTime.Now;
                    exchangeRate.ChangeFrom = from;
                    dbConn.Insert(exchangeRate);
                }
                return Json(new { success = true });
            }
            catch(Exception ex)
            {
                return Json(new { success = false, message=ex.Message });
            }
        }
        private double convertData(string dataRate)
        {
            double rate = 0;
            if(double.TryParse(dataRate,out rate))
            {

            }
            return rate;

        }
	}
}