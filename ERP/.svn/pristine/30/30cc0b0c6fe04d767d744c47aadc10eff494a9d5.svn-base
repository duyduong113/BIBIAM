using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Data;
using ServiceStack.OrmLite;
using System.IO;
using CMS.Helpers;
using System.Collections;

namespace CMS.Models
{
    public class cms_News
    {
        [AutoIncrement]
        [PrimaryKey]
        public int id { get; set; }
        public string ma_website { get; set; }
        public string ma_chuyen_muc { get; set; }
        public string ma_vi_tri { get; set; }
        public string ma_bai_viet { get; set; }
        public string tieu_de { get; set; }
        public string mo_ta { get; set; }
        public string ma_hinh_goc { get; set; }
        public string hinh_dai_dien { get; set; }
        public string noi_dung { get; set; }
        public string link_xem_bai_viet { get; set; }
        public string tu_khoa { get; set; }
        public string nguon_bai_viet { get; set; }
        public string link_nguon_bai_viet { get; set; }
        public int uu_tien { get; set; }
        public int trang_thai_tao { get; set; }
        public int trang_thai_xuat_ban { get; set; }
        public int luot_xem { get; set; }
        public DateTime? ngay_viet_bai { get; set; }
        public string nguoi_viet_bai { get; set; }
        public DateTime? ngay_sua_bai { get; set; }
        public string nguoi_sua_bai { get; set; }
        public string nguoi_xuat_ban { get; set; }
        public bool cho_phep_comment { get; set; }
        public string slug { get; set; }
        public DateTime? createdat { get; set; }
        public string createdby { get; set; }
        public DateTime? updatedat { get; set; }
        public string updatedby { get; set; }
        [Ignore]
        public string ten_vi_tri { get; set; }
        [Ignore]
        public string ten_chuyen_muc { get; set; }
        [Ignore]
        public string ten_website { get; set; }

        [Ignore]
        public DateTime? ngay_gio_xuat_ban { get; set; }
        [Ignore]
        public string ghi_chu { get; set; }
        [Ignore]
        public string nguoi_dat_lenh { get; set; }
        [Ignore]
        public DateTime? ngay_dat_lenh { get; set; }
        [Ignore]
        public string nguoi_cap_nhat { get; set; }
        [Ignore]
        public DateTime? ngay_cap_nhat { get; set; }
   


        public Image ResizeImage(Image img, int width, int height)
        {
            var newImage = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(img, 0, 0, width, height);
            return newImage;
        }
        public Image AutoResize(Image img, string type)
        {
            using (IDbConnection db = Helpers.OrmliteConnection.openConn())
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

        public string UploadImageToFolder(string FolderName, HttpPostedFileBase file, string UserName, string ma_website)
        {
            using (IDbConnection db = Helpers.OrmliteConnection.openConn())
            {
                string duong_dan_day_du = string.Empty;
                if (FolderName != "" || FolderName != null)
                {
                    var checkID = db.FirstOrDefault<cms_Merchant_Folder_Info>(s => s.ten_thu_muc == FolderName);
                    if (checkID == null)
                    {
                        cms_Merchant_Folder_Info itemcms = new cms_Merchant_Folder_Info();
                        itemcms.ma_gian_hang = "All";
                        itemcms.ten_thu_muc = FolderName;
                        itemcms.nguoi_cap_nhat = itemcms.nguoi_tao = UserName;
                        itemcms.ngay_tao = itemcms.ngay_cap_nhat = DateTime.Now;
                        new cms_Merchant_Folder_Info_DAO().Insert(itemcms);
                    }
                }
                List<string> listPath = new List<string>();
                List<Image_Info> list = new List<Image_Info>();
                string idref = UserName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var img = Image.FromStream(file.InputStream, true, true);

                var listType = new List<Code_Master>();
                var listExtension = listType;
                listType = db.Select<Code_Master>(s => s.Type == "IMGResizeType");
                listExtension = db.Select<Code_Master>(s => s.Type == "IMGResizeFormat");
                //if (listType.Count == 0)
                //    return "Không tìm thấy cấu hình";
                foreach (Code_Master type in listType)
                {
                    var refix = idref;
                    Image_Info item = new Image_Info();
                    img = AutoResize(img, type.Value);
                    //if (img == null)
                    //    return "Không tìm thấy cấu hình";

                    var i = 0;
                    string FolderPath = System.Web.HttpContext.Current.Server.MapPath("~/Images/Merchant_Image_Info/All" + "/" + FolderName + "/");
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
                    item.duong_dan_day_du = new AzureHelper().UploadFile("All", item.url, destinationPath);
                    item.loai = Int16.Parse(type.Value);
                    item.trang_thai = "Active";
                    item.trang_thai_duyet = "New";
                    item.ten_anh = fileName;
                    item.mo_ta = item.mo_ta_khong_dau = "";
                    item.ma_website = ma_website;
                    list.Add(item);

                    if(type.Name=="Small")
                    {
                        duong_dan_day_du = item.duong_dan_day_du;
                    }
                }

                string result = new Image_Info().UpSert(list, UserName);

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
}