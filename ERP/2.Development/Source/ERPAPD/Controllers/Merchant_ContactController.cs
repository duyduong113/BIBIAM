﻿using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPAPD.Helpers;
using ERPAPD.Models ;
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
    public class Merchant_ContactController : CustomController
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
                    ViewBag.listDanhXung = db.Select<BIBIAM.Core.Entities.Parameters >(s => s.Type == "DanhXung").OrderBy(s => s.ID);
                    ViewBag.listMerchant = db.Select<Merchant_Info>().OrderBy(s => s.id);
                }
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
                var data = new Merchant_Contact_DAO().ReadAll("");
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
                result = new Merchant_Contact_DAO().Delete(st,"").ToString();
                if (result == "true")
                    return Json(new { success = true, message = "Thành công" });
                else
                    return Json(new { success = false, message = result });
            }
            else
                return Json(new { success = false, message = "Bạn không có quyền xóa" });
        }

        public ActionResult Upsert(Merchant_Contact item)
        {
            if (asset.Create || asset.Update)
            {
                string result = "";
                List<Merchant_Contact> list = new List<Merchant_Contact>();
                list.Add(item);
                result = new Merchant_Contact_DAO().UpSert(list, currentUser.UserName, "Insert","");
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
            else
            {
                return Json(new { success = false, message = "Bạn không có quyền." });
            }
        }
        //public ActionResult GetByMaGH(string data)
        //{
        //    if (asset.View)
        //    {
        //        using (IDbConnection db = OrmliteConnection.openConn())
        //        {
        //            var str = db.Select<Merchant_Contact>(s => s.ma_gian_hang == data);
        //            if (str != null)
        //                return Json(str, JsonRequestBehavior.AllowGet);
        //            else
        //                return Json(new { success = false, message = "That bai" });
        //        }
        //    }
        //    else
        //        return Json(new { success = false, message = "Bạn không có quyền" });
        //}
        //public ActionResult GetById(string data)
        //{
        //    if (asset.View)
        //    {
        //        using (IDbConnection db = OrmliteConnection.openConn())
        //        {
        //            var str = db.Select<Merchant_Contact>(s => s.id == Int16.Parse(data));
        //            if (str != null)
        //                return Json(str, JsonRequestBehavior.AllowGet);
        //            else
        //                return Json(new { success = false, message = "That bai" });
        //        }
        //    }
        //    else
        //        return Json(new { success = false, message = "Bạn không có quyền" });
        //}
    }
}
