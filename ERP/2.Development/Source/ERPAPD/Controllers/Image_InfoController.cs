using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ERPAPD.Helpers;
using ERPAPD.Models;
using System.Data;
using System.IO;
using OfficeOpenXml;
using System.Collections;
using System.Data.OleDb;
using NPOI.HSSF.UserModel;
using System.Dynamic;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using BIBIAM.Core.Entities;
using BIBIAM.Core.Data.DataObject;
using System.Drawing;


namespace ERPAPD.Controllers
{
    public class Image_InfoController : CustomController
    {
        public ActionResult Index()
        {
            if (asset.View)
            {
                ViewBag.Create = asset.Create;
                ViewBag.Update = asset.Update;
                ViewBag.Delete = asset.Delete;
                ViewBag.Export = asset.Export;
                ViewBag.listWebsite = CRM_Website.GetList();
                using (IDbConnection db = OrmliteConnection.openConn())
                {
                    ViewBag.listStatus = db.Select<BIBIAM.Core.Entities.Parameters>(s => s.Type == "TTSP").OrderBy(s => s.ID);
                    ViewBag.listLoai = db.Select<BIBIAM.Core.Entities.Parameters>(s => s.Type == "ImageType").OrderBy(s => s.ID);
                    ViewBag.listStatusDuyet = db.Select<BIBIAM.Core.Entities.Parameters>(s => s.Type == "TTDSP").OrderBy(s => s.ID);
                    ViewBag.listStatusXuatBan = db.Select<BIBIAM.Core.Entities.Parameters>(s => s.Type == "StatusPublish").OrderBy(s => s.ID);
                }
                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        //public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        //{
        //    if (asset.View)
        //    {
        //        var data = new Image_Info_DAO().ReadAll();
        //        return Json(data.ToDataSourceResult(request));
        //    }
        //    else
        //    {
        //        return RedirectToAction("NoAccessRights", "Error");
        //    }
        //}
        public ActionResult Read([DataSourceRequest] DataSourceRequest request, int loai = 1)
        {
            if (asset.View)
            {
                using (IDbConnection db = OrmliteConnection.openConn())
                    if (loai == -1)
                        return Json(db.Select<Image_Info>().ToDataSourceResult(request));
                    else
                        return Json(db.Select<Image_Info>(s => s.loai == loai).ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Delete(string data)
        {
            if (asset.Delete)
            {
                string result = "";
                string[] st = new string[1];
                st[0] = data;
                result = new Image_Info_DAO().Delete(st).ToString();
                if (result == "true")
                    return Json(new { success = true, message = "Thành công" });
                else
                    return Json(new { success = false, message = result });
            }
            else
                return Json(new { success = false, message = "Bạn không có quyền xóa" });
        }
        public ActionResult Save(IEnumerable<HttpPostedFileBase> files)
        {
            try
            {
                List<Image_Info> list = new List<Image_Info>();
                List<string> listPath = new List<string>();
                foreach (var file in files)
                {
                    string idref = currentUser.UserName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);

                    var img = Image.FromStream(file.InputStream, true, true);
                    var listType = new List<BIBIAM.Core.Entities.Parameters>();
                    var listExtension = listType;
                    using (IDbConnection db = OrmliteConnection.openConn())
                    {
                        listType = db.Select<BIBIAM.Core.Entities.Parameters>(s => s.Type == "IMGResizeType");
                        listExtension = db.Select<BIBIAM.Core.Entities.Parameters>(s => s.Type == "IMGResizeFormat");
                    }
                    if (listType.Count == 0)
                        return Json(new { success = false, message = "Không tìm thấy cấu hình" });
                    foreach (BIBIAM.Core.Entities.Parameters type in listType)
                    {
                        var refix = idref;
                        Image_Info item = new Image_Info();
                        img = AutoResize(img, type.ParamID);
                        if (img == null)
                            return Json(new { success = false, message = "Không tìm thấy cấu hình" });
                        var i = 0;
                        var destinationPath = Path.Combine(Server.MapPath("~/Images/Image_Info"), refix);
                        var lastRefix = "_" + img.Width.ToString() + "x" + img.Height.ToString();
                        string extension = ".jpg";
                        foreach (var ex in listExtension)
                        {
                            if (ex.ParamID == type.ParamID)
                            {
                                extension = ex.Value;
                                break;
                            }
                        }
                        while (System.IO.File.Exists(destinationPath + i.ToString() + lastRefix + extension))
                        {
                            i++;
                        }
                        destinationPath += i.ToString() + lastRefix + extension;
                        img.Save(destinationPath);
                        listPath.Add(destinationPath);
                        item.url = refix + i.ToString() + lastRefix + extension;
                        item.ma_anh_goc = idref;
                        item.chieu_rong = img.Width;
                        item.chieu_cao = img.Height;
                        item.dung_luong = new FileInfo(destinationPath).Length;
                        item.loai = Int16.Parse(type.ParamID);
                        item.trang_thai = "Active";
                        item.trang_thai_duyet = "New";
                        item.ten_anh = fileName + lastRefix;
                        item.mo_ta = item.mo_ta_khong_dau = "";
                        list.Add(item);
                    }
                }
                string result = new Image_Info_DAO().UpSert(list, currentUser.UserName, "Insert");
                if (result != "true")
                {
                    foreach (var path in listPath)
                        if (System.IO.File.Exists(path))
                            System.IO.File.Delete(path);
                    return Json(new { success = false, message = result });
                }
                else
                    return Json(new { success = true, message = "Thành công" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }
        private Image ResizeImage(Image img, int width, int height)
        {
            var newImage = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(img, 0, 0, width, height);
            return newImage;
        }
        private Image AutoResize(Image img, string type)
        {
            using (IDbConnection db = OrmliteConnection.openConn())
            {
                var x = db.Select<BIBIAM.Core.Entities.Parameters>(s => s.Type == "IMGAutoResize").OrderBy(s => s.ParamID);
                int width = 0, height = 0;
                foreach (BIBIAM.Core.Entities.Parameters item in x)
                {
                    if (item.ParamID == type + "_WIDTH")
                        width = Int16.Parse(item.Value);
                    if (item.ParamID == type + "_HEIGHT")
                        height = Int16.Parse(item.Value);
                }
                if (width * height != 0)
                    return ResizeImage(img, width, height);
                else
                    return null;
            }
        }
    }
}
