using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPAPD.Models;
using ERPAPD.Helpers;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
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
namespace ERPAPD.Controllers
{
    public class Merchant_ImageController : CustomController
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
                return View();
            }
            else
            {
                return RedirectToAction("NoAccessRights", "Error");
            }
        }
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            if (asset.View)
            {
                var data = new Merchant_Image_DAO().ReadAll();
                return Json(data.ToDataSourceResult(request));
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
                result = new Merchant_Image_DAO().Delete(st).ToString();
                if (result == "true")
                    return Json(new { success = true, message = "Thành công" });
                else
                    return Json(new { success = false, message = result });
            }
            else
                return Json(new { success = false, message = "Bạn không có quyền xóa" });
        }
        public ActionResult Publish(string id)
        {
            if (asset.View)
            {

                using (IDbConnection db = OrmliteConnection.openConn())
                {
                    using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                    {
                        try
                        {
                            string rs = "";
                            var checkID = db.SingleOrDefault<Merchant_Image>("id={0}", id);
                            if (checkID != null)
                            {
                                //if (checkID.trang_thai_xuat_ban == "DA_XUAT_BAN")
                                //{
                                //    checkID.trang_thai_xuat_ban = "NGUNG_XUAT_BAN";
                                //    rs = "Ngừng Xuất Bản";
                                //}
                                //else
                                //{
                                //    checkID.trang_thai_xuat_ban = "DA_XUAT_BAN";
                                //    rs = "Xuất Bản";
                                //}
                                db.Update(checkID);
                            }
                            else
                            {
                                dbTrans.Rollback();
                                return Json(new { success = true, message = "Không tồn tại ảnh này" });
                            }
                            dbTrans.Commit();
                            return Json(new { success = true, message = rs + " Thành Công" });
                        }
                        catch (Exception e)
                        {
                            dbTrans.Rollback();
                            return Json(new { success = false, message = "Thất bại" });
                        }
                    }
                }
            }
            else
                return Json(new { success = false, message = "Bạn không có quyền" });
        }
        public ActionResult Approve(string id)
        {
            if (asset.View)
            {

                using (IDbConnection db = OrmliteConnection.openConn())
                {
                    using (var dbTrans = db.OpenTransaction(IsolationLevel.ReadCommitted))
                    {
                        try
                        {
                            string rs = "";
                            var checkID = db.SingleOrDefault<Merchant_Image>("id={0}", id);
                            if (checkID != null)
                            {
                                //if (checkID.trang_thai_duyet == "Rejected")
                                //{
                                //    checkID.trang_thai_duyet = "Approved";
                                //    rs = "Đã Duyệt";
                                //}
                                //else
                                //{
                                //    checkID.trang_thai_duyet = "Rejected";
                                //    rs = "Từ Chối Duyệt";
                                //}
                                db.Update(checkID);
                            }
                            else
                            {
                                dbTrans.Rollback();
                                return Json(new { success = true, message = "Không tồn tại ảnh này" });
                            }
                            dbTrans.Commit();
                            return Json(new { success = true, message = rs + " Thành công" });
                        }
                        catch (Exception e)
                        {
                            dbTrans.Rollback();
                            return Json(new { success = false, message = "Thất bại" });
                        }
                    }
                }
            }
            else
                return Json(new { success = false, message = "Bạn không có quyền" });
        }
        public ActionResult Upsert(Merchant_Image item, HttpPostedFileBase file)
        {
            if (asset.Create || asset.Update)
            {
                string result = "";
                try
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var refix = "Merchant_Image_" + currentUser.UserName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                        Helpers.UploadFile.CreateFolder(Server.MapPath("~/Images/Merchant_Image/"));
                        var path = Path.Combine(Server.MapPath("~/Images/Merchant_Image/"), refix + Path.GetExtension(fileName));
                        file.SaveAs(path);
                        item.ten_anh = refix + Path.GetExtension(fileName);
                    }
                    List<Merchant_Image> lstProdInfo = new List<Merchant_Image>();
                    lstProdInfo.Add(item);
                    result = new Merchant_Image_DAO().UpSert(lstProdInfo, currentUser.UserName, "Insert");
                    if (result == "true")
                    {
                        if (item.id == 0)// 0 insert, 1 update
                            return Json(new { success = true, type = 0 });
                        else
                            return Json(new { success = true, type = 1 });
                    }
                    else
                        return Json(new { success = false, message = result });
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = "Có lỗi file upload" + e.Message });
                }
            }
            else
            {
                return Json(new { success = false, message = "Bạn không có quyền." });

            }
        }
        public ActionResult GetImageById(string id)
        {
            if (asset.View)
            {
                using (IDbConnection db = OrmliteConnection.openConn())
                {
                    var str = db.Select<Merchant_Image>(s => s.id == Int16.Parse(id));
                    if (str != null)
                        return Json(new { success = true, str, JsonRequestBehavior.AllowGet });
                    else
                        return Json(new { success = false, message = "That bai" });
                }
            }
            else
                return Json(new { success = false, message = "Bạn không có quyền" });
        }
        public ActionResult GetImageBySP(string ma_gian_hang)
        {
            if (asset.View)
            {
                using (IDbConnection db = OrmliteConnection.openConn())
                {
                    var data = db.Select<Merchant_Image>("select distinct ma_anh_goc from Merchant_Image where ma_gian_hang='" + ma_gian_hang + "'");
                    if (data != null)
                        return Json(new { success = true, data = data }, JsonRequestBehavior.AllowGet);
                    else
                        return Json(new { success = false, message = "Thất bại" });
                }
            }
            else
                return Json(new { success = false, message = "Bạn không có quyền" });
        }


        public ActionResult Update(string ma_gian_hang, string data)
        {
            using (IDbConnection db = OrmliteConnection.openConn())
            {
                if (asset.Create || asset.Update)
                {
                    try
                    {
                        new Merchant_Image_DAO().DeleteForCode(ma_gian_hang);
                        string[] separators = { "@@" };
                        var listdata = data.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in listdata)
                        {
                            new Merchant_Image_DAO().Insert(ma_gian_hang, item, currentUser.UserName);
                        }
                        return Json(new { success = true });
                    }

                    catch (Exception ex)
                    {
                        return RedirectToAction("NoAccess", "Error");
                        //return Json(new { success = false, message = e.Message });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Bạn không có quyền." });
                }
            }
        }
    }
}