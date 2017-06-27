using CMS.Filters;
using CMS.Helpers;
using CMS.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace CMS.Controllers
{
    [Authorize]
    [CustomActionFilter(permission = "all,view,update,create,delete")]
    public class NewsManageController : CustomController
    {
        // GET: NewsManage
        public ActionResult Index()
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                ViewBag.isAdmin = isAdmin;
                return View();
            }
            return RedirectToAction("NoAccess", "Error");
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (IDbConnection dbConn = Helpers.OrmliteConnection.openConn())
            {
                var data = new DataSourceResult();
                string sqlquery = @"select a.*,b.ten_website,c.ten_chuyen_muc,d.ten_vi_tri,e.ngay_gio_xuat_ban,e.ghi_chu from cms_News a left join cms_Websites b on a.ma_website=b.ma_website left join cms_Categorys c on a.ma_chuyen_muc=c.ma_chuyen_muc left join cms_Positions d on a.ma_vi_tri=d.ma_vi_tri left join cms_Schedule_News e on a.ma_bai_viet=e.ma_bai_viet";
                data = KendoApplyFilter.KendoDataByQuery<cms_News>(request,sqlquery,"");              
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Detail(string id)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                if (String.IsNullOrEmpty(id))
                    return RedirectToAction("Create", "NewsManage");
                using (var dbConn = CMS.Helpers.OrmliteConnection.openConn())
                {
                    var data = dbConn.FirstOrDefault<cms_News>("id = {0}", id);
                    if (data == null)
                    {
                        return RedirectToAction("Create", "NewsManage");
                    }
                    else
                    {
                        ViewBag.data = data;
                    }
                    ViewBag.listCategorys = dbConn.Select<Code_Master_Json>("Select isnull(ma_chuyen_muc,'') as Value , isnull(ten_chuyen_muc,'') as Name from cms_Categorys where trang_thai=N'Active'");
                    ViewBag.listWebsites  = dbConn.Select<Code_Master_Json>("Select isnull(ma_website,'') as Value , isnull(ten_website,'') as Name from cms_Websites where trang_thai=N'Active'");
                    ViewBag.listPositions = dbConn.Select<Code_Master_Json>("Select isnull(ma_vi_tri,'') as Value , isnull(ten_vi_tri,'') as Name from cms_Positions where trang_thai=N'Active'");
                    return View();
                }
            }
            return RedirectToAction("NoAccess", "Error");
        }
        public ActionResult Create()
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                using (var dbConn = CMS.Helpers.OrmliteConnection.openConn())
                {
                    var data = new List<Code_Master_Json>();
                    ViewBag.listCategorys = ViewBag.listPositions = data;
                    ViewBag.listWebsites = dbConn.Select<Code_Master_Json>("Select isnull(ma_website,'') as Value , isnull(ten_website,'') as Name from cms_Websites where trang_thai=N'Active'");
                    return View();
                }
            }

            return RedirectToAction("NoAccess", "Error");
        }

        public ActionResult DeleteNews(Int64 id)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["delete"]))
            {
                try
                {
                    using (var dbConn = CMS.Helpers.OrmliteConnection.openConn())
                    {
                        var exists = dbConn.FirstOrDefault<cms_News>("id={0}".Params(id));
                        if (exists == null)
                        {
                            return Json(new { success = false, error = "Xóa thất bại!" });
                        }

                        dbConn.Delete<cms_News>("id={0}".Params(id));
                        return Json(new { success = true });
                    }
                }
                catch (Exception e)
                {
                    return Json(new { success = false, error = e.Message });
                }
            }
            else
            {
                return Json(new { success = false, error = "Bạn không có quyền xóa" });
            }
        }
        public ActionResult StatusNews(int id)
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["delete"]))
            {
                try
                {
                    using (var dbConn = CMS.Helpers.OrmliteConnection.openConn())
                    {
                        var exists = dbConn.FirstOrDefault<cms_News>("id={0}".Params(id));
                        if (exists == null)
                        {
                            return Json(new { success = false, error = "Thất bại!" });
                        }

                        dbConn.UpdateOnly(new cms_News { trang_thai_tao = exists.trang_thai_tao!=1?1:0 }, onlyFields: p => p.trang_thai_tao, where: p => p.id == id);
                        return Json(new { success = true });
                    }
                }
                catch (Exception e)
                {
                    return Json(new { success = false, error = e.Message });
                }
            }
            else
            {
                return Json(new { success = false, error = "Bạn không có quyền thực hiên chức năng này!" });
            }
        }

        public ActionResult PublishNews(int id)
        {
            if (accessDetail != null && (accessDetail.access["all"]))
            {
                try
                {
                    using (var dbConn = CMS.Helpers.OrmliteConnection.openConn())
                    {
                        var exists = dbConn.FirstOrDefault<cms_News>("id={0}".Params(id));
                        if (exists == null)
                        {
                            return Json(new { success = false, error = "Xuất bản thất bại!" });
                        }
                        dbConn.UpdateOnly(new cms_News { trang_thai_xuat_ban = exists.trang_thai_xuat_ban != 1 ? 1 : 0, nguoi_xuat_ban = currentUser.name }, onlyFields: p => new { p.trang_thai_xuat_ban,p.nguoi_xuat_ban },where: p => p.id == id);
                        return Json(new { success = true });
                    }
                }
                catch (Exception e)
                {
                    return Json(new { success = false, error = e.Message });
                }
            }
            else
            {
                return Json(new { success = false, error = "Không có quyền Xuất bản" });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateUpdateSchedule(cms_Schedule_News data)
        {
            try
            {
                using (var dbConn = CMS.Helpers.OrmliteConnection.openConn())
                using (var dbTrans = dbConn.OpenTransaction())
                {
                    if(data.ngay_gio_xuat_ban!=null)
                    {
                        if(data.ngay_gio_xuat_ban<DateTime.Now)
                            return Json(new { success = false, error = "Ngày giờ xuất bản phải lớn hơn ngày hiện tại" });
                    }
                    var exists = dbConn.FirstOrDefault<cms_Schedule_News>("ma_bai_viet={0}".Params(data.ma_bai_viet));
                    if (exists == null)
                    {
                        if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["create"]))
                        {

                            data.nguoi_dat_lenh = currentUser.name;
                            data.ngay_dat_lenh = DateTime.Now;
                            dbConn.Insert(data);
                            int Id = (int)dbConn.GetLastInsertId();
                            data.id = Id;
                            dbTrans.Commit();
                        }
                        else
                        {
                            return Json(new { success = false, error = "Don't have permission to create" });
                        }
                    }
                    else
                    {
                        if (accessDetail != null && accessDetail.access["all"] || accessDetail.access["update"])
                        {
                            data.ngay_cap_nhat  = DateTime.Now;
                            data.nguoi_cap_nhat = currentUser.name;
                            if (data.ngay_gio_xuat_ban == null)
                                data.ngay_gio_xuat_ban = exists.ngay_gio_xuat_ban;
                            dbConn.UpdateOnly(data,
                                    onlyFields: p =>
                                        new
                                        {
                                            p.ngay_gio_xuat_ban,
                                            p.ghi_chu,
                                            p.ngay_cap_nhat,
                                            p.nguoi_cap_nhat,
                                        },
                                where: p => p.ma_bai_viet == data.ma_bai_viet);
                            dbTrans.Commit();
                        }
                        else
                        {
                            return Json(new { success = false, error = "Don't have permission to update" });
                        }
                    }
                }
                return Json(new { success = true, data = data });
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
            }
        }

        public ActionResult GetFolderbyName(string ten_thu_muc)
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                ten_thu_muc = "NEWS";
                var data = new List<DropDownJsonString>();
                data = dbConn.GetDictionary<string, string>("SELECT distinct ten_thu_muc as id, ten_thu_muc as name FROM cms_Merchant_Folder_Info where ten_thu_muc =N'"+ ten_thu_muc + "'").Select(s => new DropDownJsonString { id = s.Key, name = s.Value }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetHashtag()
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                var data = new List<DropDownHashtag>();
                data = dbConn.GetDictionary<string, string>("select distinct hashtagcode ,hashtagname from Hashtag ").Select(s => new DropDownHashtag { hashtagcode = s.Key, hashtagname = s.Value }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateUpdate(cms_News data, List<Image_Info> listimage, List<Image_Info> listimagecontent, HttpPostedFileBase file, List<string> tu_khoas)
        {
            try
            {
                using (var dbConn = Helpers.OrmliteConnection.openConn())
                using (var dbTrans = dbConn.OpenTransaction())
                {
                    if (listimage != null)
                    {
                        foreach (var item in listimage)
                        {
                            data.hinh_dai_dien = item.duong_dan_day_du;
                            data.ma_hinh_goc = item.ma_anh_goc;
                        }
                        //return Json(new { success = false, message = "Vui lòng chọn hình ảnh tin tức", JsonRequestBehavior.AllowGet });
                    }

                    if (file != null)
                    {

                        data.hinh_dai_dien = new cms_News().UploadImageToFolder("NEWS", file, currentUser.name, data.ma_website);
                        data.ma_hinh_goc = currentUser.name + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    }

                    string ma_tu_tang = String.Empty;
                    var lastId = dbConn.FirstOrDefault<cms_News>("SELECT TOP 1 * FROM cms_News ORDER BY ma_bai_viet DESC");
                    if (lastId != null)
                    {
                        var nextNo = Int32.Parse(lastId.ma_bai_viet.Substring(3, 3)) + 1;
                        ma_tu_tang = "BAI" + String.Format("{0:000}", nextNo);
                    }
                    else
                    {
                        ma_tu_tang = "BAI001";
                    }
                    data.ma_bai_viet = data.id == 0 ? ma_tu_tang : dbConn.SingleOrDefault<cms_News>("id={0}", data.id).ma_bai_viet;

                    if (listimagecontent != null)
                    {
                        foreach (var items in listimagecontent)
                        {
                            var test = dbConn.Select<cms_New_Images>(c => c.duong_dan_day_du == items.duong_dan_day_du && c.ma_bai_viet == data.ma_bai_viet);
                            if (dbConn.Select<cms_New_Images>(c => c.duong_dan_day_du == items.duong_dan_day_du && c.ma_bai_viet == data.ma_bai_viet).Count() == 0)
                            {
                                cms_New_Images item = new cms_New_Images();
                                item.ma_bai_viet = data.ma_bai_viet;
                                item.nguoi_tao = currentUser.name;
                                item.ngay_tao = DateTime.Now;
                                item.ten_anh = items.ma_anh_goc;
                                item.duong_dan_day_du = items.duong_dan_day_du;
                                item.trang_thai = true;
                                dbConn.Insert(item);
                            }

                        }
                    }

                    if (data.slug == "" || data.slug == null)
                        data.slug = SqlHelper.convertToUnSign3(data.tieu_de);
                    else
                        data.slug = SqlHelper.convertToUnSign3(data.slug);

                    data.tu_khoa = "";
                    if (tu_khoas != null)
                    {
                        foreach (string item in tu_khoas)
                            data.tu_khoa += item + ";";
                    }

                    if (data.id > 0)
                    {
                        if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["update"]))
                        {

                            var exist = dbConn.SingleOrDefault<cms_News>("id={0}", data.id);
                            if(data.hinh_dai_dien==null)
                            {
                                data.hinh_dai_dien = exist.hinh_dai_dien;
                                data.ma_hinh_goc = exist.ma_hinh_goc;
                            }
                            data.ngay_sua_bai = data.updatedat = DateTime.Now;
                            data.nguoi_sua_bai = data.updatedby = currentUser.name;
                           

                            dbConn.UpdateOnly(data,
                                    onlyFields: p =>
                                        new
                                        {
                                            p.ma_website,
                                            p.ma_chuyen_muc,
                                            p.ma_vi_tri,
                                            p.tieu_de,
                                            p.mo_ta,
                                            p.uu_tien,
                                            p.hinh_dai_dien,
                                            p.ma_hinh_goc,
                                            p.noi_dung,
                                            p.ngay_viet_bai,
                                            p.tu_khoa,
                                            p.nguon_bai_viet,
                                            p.link_nguon_bai_viet,
                                            p.luot_xem,
                                            p.nguoi_sua_bai,
                                            p.ngay_sua_bai,
                                            //p.cho_phep_comment,
                                            p.slug,
                                            p.updatedat,
                                            p.updatedby
                                        },
                                where: p => p.id == data.id);
                            dbTrans.Commit();
                        }
                        else
                        {
                            return Json(new { success = false, error = "Don't have permission to update" });
                        }
                    }
                    else
                    {
                        if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["create"]))
                        {
                            data.trang_thai_tao = 1;
                            data.nguoi_viet_bai = data.nguoi_viet_bai == null ? currentUser.name : data.nguoi_viet_bai;
                            data.ngay_viet_bai = data.ngay_viet_bai == null ? DateTime.Now : data.ngay_viet_bai;
                            data.createdat = DateTime.Now;
                            data.createdby = currentUser.name;
                            dbConn.Insert(data);
                            int Id = (int)dbConn.GetLastInsertId();
                            data.id = Id;

                            dbTrans.Commit();
                        }
                        else
                        {
                            return Json(new { success = false, error = "Don't have permission to create" });
                        }
                    }
                }
                return Json(new { success = true, data = data });
            }
            catch (Exception e)
            {
                return Json(new { success = false, error = e.Message });
            }
        }

        public ActionResult getPositions(string chuyen_muc)
        {
            IDbConnection db = OrmliteConnection.openConn();
            try
            {
                var data = new List<Code_Master_Json>();
                data = db.Query<Code_Master_Json>("select isnull(pos.ma_vi_tri,'') as Value, isnull(pos.ten_vi_tri,'') as Name from cms_Positions pos, cms_WCL wcl where pos.ma_vi_tri=wcl.vi_tri and wcl.chuyen_muc = N'" + chuyen_muc + "' and pos.trang_thai= N'Active'").ToList();
                return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }
        public ActionResult getCategorys(string website)
        {
            IDbConnection db = OrmliteConnection.openConn();
            try
            {
                var data = new List<Code_Master_Json>();
                data = db.Query<Code_Master_Json>("Select isnull(ma_chuyen_muc,'') as Value , isnull(ten_chuyen_muc,'') as Name from cms_Categorys where website = N'" + website + "' and trang_thai = N'Active' ").ToList();
                return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        public ActionResult getCategoryName(string categoryID)
        {
            IDbConnection db = OrmliteConnection.openConn();
            try
            {
                var data = db.QueryScalar<string>("Select ten_chuyen_muc from cms_Categorys where ma_chuyen_muc = N'" + categoryID + "' and trang_thai = N'Active' ");
                return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        public ActionResult getNewImages([DataSourceRequest] DataSourceRequest request,string ma_tin_tuc)
        {
            using (var dbConn = CMS.Helpers.OrmliteConnection.openConn())
            {
                return Json(dbConn.Select<cms_New_Images>(s => s.ma_bai_viet == ma_tin_tuc).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetWebsite()
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                var data = new List<DropDownJsonString>();
                data = dbConn.GetDictionary<string, string>("SELECT distinct ma_website as id, ten_website as name FROM cms_Websites").Select(s => new DropDownJsonString { id = s.Key, name = s.Value }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetPosition()
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                var data = new List<DropDownJsonString>();
                data = dbConn.GetDictionary<string, string>("SELECT distinct ma_vi_tri as id, ten_vi_tri as name FROM cms_Positions").Select(s => new DropDownJsonString { id = s.Key, name = s.Value }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        
        public ActionResult GetCategory()
        {
            using (var dbConn = OrmliteConnection.openConn())
            {
                var data = new List<DropDownJsonString>();
                data = dbConn.GetDictionary<string, string>("SELECT distinct ma_chuyen_muc as id, ten_chuyen_muc as name FROM cms_Categorys").Select(s => new DropDownJsonString { id = s.Key, name = s.Value }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }




    }
}