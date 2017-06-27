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
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.IO;

namespace CMS.Controllers
{
    [Authorize]
    [CustomActionFilter(permission = "all,view,update,create,delete")]
    public class Image_InfoManageController : CustomController
    {
        public ActionResult Index()
        {
            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                ViewBag.listStatus = GetListCode_Master_Json("TTSP");
                ViewBag.listLoai = GetListCode_Master_Json("ImageType");
                ViewBag.listStatusDuyet = GetListCode_Master_Json("TTDSP");
                ViewBag.listStatusXuatBan = GetListCode_Master_Json("StatusPublish");
                ViewBag.isAdmin = isAdmin;
                ViewBag.ma_gian_hang = currentUser.companycode;
                return View();
            }
            else
            {
                return RedirectToAction("NoAccess", "Error");
            }
        }
        public ActionResult Read([DataSourceRequest] DataSourceRequest request, int loai = 1, string FolderName = "")
        {

            if (accessDetail != null && (accessDetail.access["all"] || accessDetail.access["view"]))
            {
                using (IDbConnection db = Helpers.OrmliteConnection.openConn())
                {
                    if (isAdmin)
                    {
                        if (!string.IsNullOrEmpty(FolderName.Trim()))
                        {
                            if (loai == -1)
                                return Json(db.Select<Image_Info>(s => s.thu_muc.Contains(FolderName)).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                            else
                                return Json(db.Select<Image_Info>(s => s.loai == loai && s.thu_muc.Contains(FolderName)).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            if (loai == -1)
                                return Json(db.Select<Image_Info>().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                            else
                                return Json(db.Select<Image_Info>(s => s.loai == loai).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return RedirectToAction("NoAccessRights", "Error");
                    }
                }
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Save(List<HttpPostedFileBase> files, string FolderName,string ma_website)
        {
            try
            {
                List<Image_Info> list = new List<Image_Info>();
                List<string> listPath = new List<string>();

                if (String.IsNullOrEmpty(FolderName.Trim()))
                {
                    FolderName = "Sản Phẩm";
                    cms_Merchant_Folder_Info item = new cms_Merchant_Folder_Info();
                    item.ma_gian_hang = "All";
                    item.ten_thu_muc = FolderName;
                    item.nguoi_tao = currentUser.name;
                    item.nguoi_cap_nhat = currentUser.name;
                    string a = new cms_Merchant_Folder_Info_DAO().Insert(item);
                }

                foreach (var file in files)
                {
                    string idref = currentUser.name + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);

                    var img = Image.FromStream(file.InputStream, true, true);
                    var listType = new List<Code_Master>();
                    var listExtension = listType;
                    using (IDbConnection db = Helpers.OrmliteConnection.openConn())
                    {
                        listType = db.Select<Code_Master>(s => s.Type == "IMGResizeType");
                        listExtension = db.Select<Code_Master>(s => s.Type == "IMGResizeFormat");
                    }
                    if (listType.Count == 0)
                        return Json(new { success = false, message = "Không tìm thấy cấu hình" });
                    foreach (Code_Master type in listType)
                    {
                        var refix = idref;
                        Image_Info item = new Image_Info();
                        img = AutoResize(img, type.Value);
                        if (img == null)
                            return Json(new { success = false, message = "Không tìm thấy cấu hình" });
                        var i = 0;
                        string FolderPath = Server.MapPath("~/Images/Merchant_Image_Info/All" + "/" + FolderName + "/");
                        var destinationPath = Path.Combine(FolderPath, refix);

                        if (!Directory.Exists(FolderPath))
                        {
                            Directory.CreateDirectory(FolderPath);
                        }
                        var lastRefix = "_" + img.Width.ToString() + "x" + img.Height.ToString();
                        string extension = ".jpg";
                        foreach (var ex in listExtension)
                        {
                            if (ex.Value == type.Value)
                            {
                                extension = ex.Name;
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
                        item.thu_muc = "All" + "/" + FolderName + "/";
                        //item.duong_dan_day_du = FolderPath + item.url;
                        item.duong_dan_day_du = new AzureHelper().UploadFile("All", item.url, destinationPath);
                        item.loai = Int16.Parse(type.Value);
                        item.trang_thai = "Active";
                        item.trang_thai_duyet = "New";
                        item.ten_anh = fileName;
                        item.mo_ta = item.mo_ta_khong_dau = "";
                        item.ma_website = ma_website;
                        list.Add(item);
                    }
                }
                string result = new Image_Info().UpSert(list, currentUser.name);

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
            using (IDbConnection db = Helpers.OrmliteConnection.openConn())
            {
                //db.ChangeDatabase(AllConstant.ERPDBName);
                var x = db.Select<Code_Master>(s => s.Type == "IMGAutoResize").OrderBy(s => s.Value);
                int width = 0, height = 0;
                foreach (Code_Master item in x)
                {
                    if (item.Value == type + "_WIDTH")
                        width = Int16.Parse(item.Name);
                    if (item.Value == type + "_HEIGHT")
                        height = Int16.Parse(item.Name);
                }
                if (width * height != 0)
                    return ResizeImage(img, width, height);
                else
                    return null;
            }
        }
        public ActionResult CreateFolder(string foldername)
        {
            try
            {
                cms_Merchant_Folder_Info item = new cms_Merchant_Folder_Info();
                item.ma_gian_hang = "All";
                item.ten_thu_muc = foldername.Trim();
                item.nguoi_tao = currentUser.name;
                item.nguoi_cap_nhat = currentUser.name;
                string result = new cms_Merchant_Folder_Info_DAO().Insert(item);
                if (result != "true")
                {
                    return Json(new { success = false, message = result });
                }
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
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


    }
}