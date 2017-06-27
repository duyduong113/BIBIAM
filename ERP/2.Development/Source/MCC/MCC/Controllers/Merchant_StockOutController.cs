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
using MCC.Filters;
using MCC.Models;
using BIBIAM.Core;
using MCC.Helpers;
using System.Data.SqlClient;

namespace MCC.Controllers
{
    [Authorize]
    [CustomActionFilter(permission = "all,view,update,create,delete,export")]
    public class Merchant_StockOutController : CustomController
    {
        public ActionResult Index()
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                ViewBag.isAdmin = isAdmin;
                using (var db = MCC.Helpers.OrmliteConnection.openConn(AppConfigs.MCCConnectionString))
                {
                    string culture = Request.Cookies["_culture"] != null ? Request.Cookies["_culture"].Value : "vi";
                    ViewBag.units = db.GetDictionary<string, string>("SELECT Value, " + (culture == "vi" ? "Name_Vi" : "Name") + " FROM Code_Master WHERE Type = 'UnitProduct' Order By order_Num").Select(s => new Code_Master_Json { Value = s.Key, Name = s.Value });
                }
                return View();
            }
            return RedirectToAction("NoAccess", "Error");
        }
        public ActionResult ReadHeader([DataSourceRequest]DataSourceRequest request)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                using (var dbConn = MCC.Helpers.OrmliteConnection.openConn(AppConfigs.MCCConnectionString))
                {
                    var data = new DataSourceResult();

                    if (isAdmin)
                    {
                        data = KendoApplyFilter.KendoDataByQuery<Merchant_StockOutHeader>(request, "select a.*,b.ten_kho from Merchant_StockOutHeader a left join Merchant_WareHouse b on a.ma_kho=b.ma_kho", "");
                    }
                    else
                    {
                        data=KendoApplyFilter.KendoDataByQuery<Merchant_StockOutHeader>(request, "select a.*,b.ten_kho from Merchant_StockOutHeader a left join Merchant_WareHouse b on a.ma_kho=b.ma_kho", "ma_gian_hang = {0}".Params(currentUser.ma_gian_hang));
                    }
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            return RedirectToAction("NoAccess", "Error");
        }
        public ActionResult CreateUpdate(Merchant_StockOutHeader header,List<Merchant_StockOutDetail> details)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["create"] || accessDetail.access["update"]))
            {
                try
                {
                    using (var dbConn = MCC.Helpers.OrmliteConnection.openConn())
                    {
                        Merchant_StockOutHeader check = dbConn.FirstOrDefault<Merchant_StockOutHeader>("ma_phieu_xuat_kho={0}".Params(header.ma_phieu_xuat_kho));
                        if (check == null)
                        {
                            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                            var beginString = new char[3];
                            var lastString = new char[3];
                            var random = new Random();

                            for (int i = 0; i < beginString.Length; i++)
                            {
                                beginString[i] = chars[random.Next(chars.Length)];
                            }
                            for (int i = 0; i < lastString.Length; i++)
                            {
                                lastString[i] = chars[random.Next(chars.Length)];
                            }
                            header.ma_phieu_xuat_kho = string.Join("", beginString) + DateTime.Now.ToString("yyyyMMddHHmm") + string.Join("", lastString);
                            //Nếu là admin tạo thiếu xuất kho thì chưa điền phiếu xuất kho này của gian hàng nào
                            if(isAdmin)
                            {
                                header.ma_gian_hang = "NONE";
                            }
                            else
                            {
                                header.ma_gian_hang = currentUser.ma_gian_hang;
                            }
                            header.danh_sach_don_hang = "NONE";
                            header.nguoi_xuat_kho = currentUser.name;
                            header.nguoi_tao = currentUser.name;
                            header.nguoi_cap_nhat = currentUser.name;
                            header.ngay_tao = DateTime.Now;
                            header.ngay_cap_nhat = DateTime.Now;
                            header.ngay_xuat_kho = DateTime.Now;
                            header.ngay_duyet = DateTime.Now;
                            header.trang_thai = AllConstant.trang_thai_duyet.CHUA_DUYET;
                            dbConn.Insert(header);
                            int Id = (int)dbConn.GetLastInsertId();
                            header.id = Id;
                            check = header;
                        }
                        else
                        {
                            check.ghi_chu = header.ghi_chu;
                            check.ma_chung_tu = header.ma_chung_tu;
                            check.ma_kho = header.ma_kho;
                            check.ngay_cap_nhat = DateTime.Now;
                            check.ngay_xuat_kho = DateTime.Now;
                            check.nguoi_nhan = header.nguoi_nhan;
                            check.nguoi_cap_nhat = currentUser.name;
                            check.nguoi_xuat_kho = currentUser.name;
                            dbConn.UpdateOnly(check,
                                        onlyFields: p =>
                                        new
                                        {
                                            p.ghi_chu,
                                            p.ma_chung_tu,
                                            p.ma_kho,
                                            p.ngay_cap_nhat,
                                            p.ngay_xuat_kho,
                                            p.nguoi_nhan,
                                            p.nguoi_cap_nhat,
                                            p.nguoi_xuat_kho
                                        },
                                         where: p => p.ma_phieu_xuat_kho == check.ma_phieu_xuat_kho);
                            
                        }
                        if(details!=null)
                        {
                            //Xóa tất cả các sản phẩm đang có trong phiếu xuất kho hiện tại
                            dbConn.Delete<Merchant_StockOutDetail>("ma_phieu_xuat_kho={0}".Params(check.ma_phieu_xuat_kho));
                            //Thêm các sản phẩm của đơn hàng mới
                            Merchant_StockOutDetail data = new Merchant_StockOutDetail();
                            foreach (var item in details)
                            {
                                data.don_vi_tinh = item.don_vi_tinh;
                                data.ma_gian_hang = check.ma_gian_hang;
                                data.ma_phieu_xuat_kho = check.ma_phieu_xuat_kho;
                                data.ma_san_pham = item.ma_san_pham;
                                data.ngay_cap_nhat = DateTime.Now;
                                data.ngay_tao = DateTime.Now;
                                data.nguoi_cap_nhat = currentUser.name;
                                data.nguoi_tao = currentUser.name;
                                data.so_luong_yeu_cau = item.so_luong_yeu_cau;
                                dbConn.Insert(data);
                            }
                            List<SqlParameter> lstParameter = new List<SqlParameter>();
                            lstParameter.Add(new SqlParameter("@ma_don_hang", check.danh_sach_don_hang));
                            lstParameter.Add(new SqlParameter("@ma_phieu_xuat_kho", check.ma_phieu_xuat_kho));
                            new BIBIAM.Core.Data.Providers.SqlHelper().ExecuteQuery("p_Update_Order_StockOut", lstParameter);
                        }
                        return Json(new { success = true, data = check });
                        
                    }
                }
                catch(Exception e)
                {
                    return Json(new { success = false, error = "Lỗi trong quá trình tạo!" });
                }
            }
            return Json(new { success = false, error = "Không có quyền tạo!" });
        }
        public ActionResult ReadDetail([DataSourceRequest]DataSourceRequest request, string ma_phieu_xuat_kho)
        {
                if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"] || accessDetail.access["create"] || accessDetail.access["update"]))
                {
                    using (var dbConn = MCC.Helpers.OrmliteConnection.openConn(AppConfigs.MCCConnectionString))
                    {
                        var data = new DataSourceResult();

                        if (isAdmin)
                        {
                            data = KendoApplyFilter.KendoDataByQuery<Merchant_StockOutDetail>(request, "select c.*,d.ten_san_pham from (select a.*,isNull(b.stock_available,0) as stock_available  from Merchant_StockOutDetail a left join Merchant_Product_Warehouse b on a.ma_san_pham=b.ma_san_pham and a.ma_gian_hang=b.ma_gian_hang) c left join Merchant_Product d on c.ma_san_pham=d.ma_san_pham where ma_phieu_xuat_kho={0}".Params(ma_phieu_xuat_kho), "");
                        }
                        else
                        {
                            data = KendoApplyFilter.KendoDataByQuery<Merchant_StockOutDetail>(request, "select c.*,d.ten_san_pham from (select a.*,isNull(b.stock_available,0) as stock_available  from Merchant_StockOutDetail a left join Merchant_Product_Warehouse b on a.ma_san_pham=b.ma_san_pham and a.ma_gian_hang=b.ma_gian_hang) c left join Merchant_Product d on c.ma_san_pham=d.ma_san_pham where ma_phieu_xuat_kho={0}".Params(ma_phieu_xuat_kho), "ma_gian_hang = {0}".Params(currentUser.ma_gian_hang));
                        }
                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                }
                return RedirectToAction("NoAccess", "Error");
        }
        public ActionResult ReadOrder([DataSourceRequest]DataSourceRequest request)
        {

            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                using (var dbConn = MCC.Helpers.OrmliteConnection.openConn(AppConfigs.MCCConnectionString))
                {
                    var data = new DataSourceResult();
                    if (isAdmin)
                        data = KendoApplyFilter.KendoData<Merchant_OrderHeader>(request,"trang_thai_don_hang={0}".Params(AllConstant.trang_thai_don_hang.CONFIRM));
                    else
                        data = KendoApplyFilter.KendoData<Merchant_OrderHeader>(request, "trang_thai_don_hang={0} and ma_gian_hang={1}".Params(AllConstant.trang_thai_don_hang.CONFIRM,currentUser.ma_gian_hang));
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            return RedirectToAction("NoAccess", "Error");
        }
        public ActionResult AddProduct_Order(string ma_phieu_xuat_kho, string ma_don_hang)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["create"] || accessDetail.access["update"]))
            {
                try
                {
                    using (var dbConn = MCC.Helpers.OrmliteConnection.openConn())
                    {
                        Merchant_OrderHeader orderheader = dbConn.FirstOrDefault<Merchant_OrderHeader>("ma_don_hang={0}".Params(ma_don_hang));
                        if(orderheader==null)
                        {
                            return Json(new { success = false, message = "Không tồn tại đơn hàng này!" });
                        }
                        Merchant_StockOutHeader existheader = dbConn.FirstOrDefault<Merchant_StockOutHeader>("ma_phieu_xuat_kho={0}".Params(ma_phieu_xuat_kho));
                        if (existheader == null)
                        {
                            return Json(new { success = false, message = "Không có phiếu xuất kho này!" });
                        }
                        //Nếu là admin khi chọn đơn hàng mới quyết định phiếu xuất kho này của gian hàng nào
                        if(isAdmin)
                        {
                            existheader.ma_gian_hang = orderheader.ma_gian_hang;
                        }
                        //Xóa tất cả các sản phẩm đang có trong phiếu xuất kho hiện tại
                        dbConn.Delete<Merchant_StockOutDetail>("ma_phieu_xuat_kho={0}".Params(ma_phieu_xuat_kho));
                        //Thêm các sản phẩm của đơn hàng mới
                        List<Merchant_OrderDetail> orderdetail = dbConn.Select<Merchant_OrderDetail>("ma_don_hang={0} and trang_thai_xuat_kho<>{1}".Params(ma_don_hang,"DA_XUAT_KHO"));
                        Merchant_StockOutDetail data = new Merchant_StockOutDetail();
                        foreach(var item in orderdetail)
                        {
                            data.don_vi_tinh = "cai";
                            data.ma_gian_hang = orderheader.ma_gian_hang;
                            data.ma_phieu_xuat_kho = ma_phieu_xuat_kho;
                            data.ma_san_pham = item.ma_san_pham;
                            data.ngay_cap_nhat = DateTime.Now;
                            data.ngay_tao = DateTime.Now;
                            data.nguoi_cap_nhat = currentUser.name;
                            data.nguoi_tao = currentUser.name;
                            data.so_luong_yeu_cau = item.so_luong;
                            dbConn.Insert(data);
                        }
                        List<SqlParameter> lstParameter = new List<SqlParameter>();
                        lstParameter.Add(new SqlParameter("@ma_don_hang", ma_don_hang));
                        lstParameter.Add(new SqlParameter("@ma_phieu_xuat_kho", ma_phieu_xuat_kho));
                        new BIBIAM.Core.Data.Providers.SqlHelper().ExecuteQuery("p_Update_Order_StockOut", lstParameter);
                        existheader.danh_sach_don_hang = ma_don_hang;
                        dbConn.UpdateOnly(existheader,
                                        onlyFields: p =>
                                        new
                                        {
                                            p.danh_sach_don_hang,
                                            p.ma_gian_hang
                                        },
                                         where: p => p.ma_phieu_xuat_kho == existheader.ma_phieu_xuat_kho);
                        return Json(new { success = true, error = "Thành công!" });

                    }
                }
                catch(Exception e)
                {
                    return Json(new { success = false, error = "Lỗi trong quá trình tạo!" });
                }
            }
            return Json(new { success = false, error = "Không có quyền tạo!" });
        }
    }
}
