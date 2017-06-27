using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using BIBIAM.Core.Entities;
using BIBIAM.Core.Data.DataObject;
using System.IO;
using System.Drawing;
using MCC.Models;
using ServiceStack.OrmLite;
using System.Data;
using System.Security.AccessControl;
using System.Security.Principal;
using OfficeOpenXml;
using BIBIAM.Core;

namespace MCC.Helpers
{
    public class ProcessImage
    {

        public string UploadImageToAzure(string FolderName, HttpPostedFileBase file, string ma_gian_hang, string UserName)
        {
            if (String.IsNullOrEmpty(FolderName.Trim()))
                FolderName = AllConstant.FoldderName_Merchant_Product;

            Merchant_Folder_Info item = new Merchant_Folder_Info();
            item.ma_gian_hang = ma_gian_hang;
            item.ten_thu_muc = FolderName;
            item.nguoi_tao = UserName;
            item.nguoi_cap_nhat = UserName;
            string a = new Merchant_Folder_Info_DAO().Insert(item, AppConfigs.MCCConnectionString);


            string idref = UserName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var fileName = Path.GetFileNameWithoutExtension(file.FileName);

            var img = Image.FromStream(file.InputStream, true, true);
            var listType = new List<Code_Master>();
            var listExtension = listType;
            using (IDbConnection db = OrmliteConnection.openConn(AppConfigs.MCCConnectionString))
            {
                listType = db.Select<Code_Master>(s => s.Type == "IMGResizeType");
                listExtension = db.Select<Code_Master>(s => s.Type == "IMGResizeFormat");
            }

            if (listType.Count == 0)
                return "Không tìm thấy cấu hình";
            foreach (Code_Master type in listType)
            {
                var refix = idref;
                img = AutoResize(img, type.Value);
                if (img == null)
                    return "Không tìm thấy cấu hình";
                var i = 0;
                string FolderPath = System.Web.HttpContext.Current.Server.MapPath("~/Images/Merchant_Image_Info/" + ma_gian_hang + "/" + FolderName + "/");
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
                return new BIBIAM.Core.AzureHelper().UploadFile(ma_gian_hang, refix + i.ToString() + lastRefix + extension, destinationPath);
            }

            return string.Empty;
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
            using (IDbConnection db = OrmliteConnection.openConn(AppConfigs.MCCConnectionString))
            {
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

        public string UploadImageToFolder(string FolderName, HttpPostedFileBase file, string ma_gian_hang, string UserName)
        {
            List<Merchant_Image_Info> list = new List<Merchant_Image_Info>();
            List<string> listPath = new List<string>();
            string duong_dan_day_du = string.Empty;
            if (String.IsNullOrEmpty(FolderName.Trim()))
                FolderName = AllConstant.FoldderName_Merchant_Product;

            Merchant_Folder_Info item = new Merchant_Folder_Info();
            item.ma_gian_hang = ma_gian_hang;
            item.ten_thu_muc = FolderName;
            item.nguoi_tao = UserName;
            item.nguoi_cap_nhat = UserName;
            string a = new Merchant_Folder_Info_DAO().Insert(item, AppConfigs.MCCConnectionString);


            string idref = UserName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var fileName = Path.GetFileNameWithoutExtension(file.FileName);

            var img = Image.FromStream(file.InputStream, true, true);
            var listType = new List<Code_Master>();
            var listExtension = listType;
            using (IDbConnection db = OrmliteConnection.openConn(AppConfigs.MCCConnectionString))
            {
                listType = db.Select<Code_Master>(s => s.Type == "IMGResizeType");
                listExtension = db.Select<Code_Master>(s => s.Type == "IMGResizeFormat");
            }

            if (listType.Count == 0)
                return "Không tìm thấy cấu hình";
            foreach (Code_Master type in listType)
            {
                Merchant_Image_Info _item = new Merchant_Image_Info();
                var refix = idref;
                img = AutoResize(img, type.Value);
                if (img == null)
                    return "Không tìm thấy cấu hình";
                var i = 0;
                string FolderPath = System.Web.HttpContext.Current.Server.MapPath("~/Images/Merchant_Image_Info/" + ma_gian_hang + "/" + FolderName + "/");
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
                _item.url = refix + i.ToString() + lastRefix + extension;
                _item.ma_anh_goc = idref;
                _item.chieu_rong = img.Width;
                _item.chieu_cao = img.Height;
                _item.dung_luong = new FileInfo(destinationPath).Length;
                _item.thu_muc = ma_gian_hang + "/" + FolderName + "/";
                _item.duong_dan_day_du = new AzureHelper().UploadFile(ma_gian_hang, _item.url, destinationPath);
                _item.loai = Int16.Parse(type.Value);
                _item.trang_thai = "Active";
                _item.trang_thai_duyet = "New";
                _item.ten_anh = fileName;
                _item.mo_ta = _item.mo_ta_khong_dau = "";
                _item.ma_gian_hang = ma_gian_hang;
                list.Add(_item);

                if (type.Name == "Small")
                {
                    duong_dan_day_du = _item.duong_dan_day_du;
                }
            }

            string result = new Merchant_Image_Info_DAO().UpSert(list, UserName, AppConfigs.MCCConnectionString);

            if (result != "true")
            {
                foreach (var path in listPath)
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
            }
            return duong_dan_day_du;
        }


    }
}