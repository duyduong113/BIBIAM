﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BIBIAM.Core.Entities;
using BIBIAM.Core.Data.Providers;
using System.Data.SqlClient;
using ServiceStack.OrmLite;
using MySql.Data.MySqlClient;

namespace BIBIAM.Core.Data.DataObject
{
    public class BrandManagementDAO
    {
        public string CreateUpdate(BrandManagement brand, string UserName, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                try
                {

                    var checkID = db.SingleOrDefault<BrandManagement>("ma_thuong_hieu={0}", brand.ma_thuong_hieu);
                    if (checkID != null)
                    {

                        checkID.logo = (!string.IsNullOrEmpty(brand.logo) && checkID.logo != brand.logo) ? brand.logo : checkID.logo;
                        checkID.ten_thuong_hieu = !string.IsNullOrEmpty(brand.ten_thuong_hieu) ? brand.ten_thuong_hieu : checkID.ten_thuong_hieu;
                        checkID.trang_thai = !string.IsNullOrEmpty(brand.trang_thai) ? brand.trang_thai : checkID.trang_thai;
                        checkID.mo_ta = !string.IsNullOrEmpty(brand.mo_ta) ? brand.mo_ta : checkID.mo_ta;
                        checkID.slug = StringHelper.convertToUnSign3(brand.ten_thuong_hieu);
                        checkID.nguoi_cap_nhat = UserName;
                        checkID.ngay_cap_nhat = DateTime.Now;
                        db.Update(checkID);

                    }
                    else
                    {
                        var lastId = db.FirstOrDefault<BrandManagement>("SELECT TOP 1 * FROM BrandManagement ORDER BY id DESC");
                        if (lastId != null)
                        {
                            if (lastId.ma_thuong_hieu.Contains("BRA"))
                            {
                                var nextNo = Int32.Parse(lastId.ma_thuong_hieu.Substring(3, 10)) + 1;
                                brand.ma_thuong_hieu = "BRA" + String.Format("{0:0000000000}", nextNo);
                            }
                        }
                        else
                        {
                            brand.ma_thuong_hieu = "BRA" + "0000000001";
                        }
                        brand.logo = !string.IsNullOrEmpty(brand.logo) ? brand.logo : "";
                        brand.ten_thuong_hieu = !string.IsNullOrEmpty(brand.ten_thuong_hieu) ? brand.ten_thuong_hieu : "";
                        brand.trang_thai = !string.IsNullOrEmpty(brand.trang_thai) ? brand.trang_thai : AllConstant.trang_thai.DANG_SU_DUNG;
                        brand.mo_ta = !string.IsNullOrEmpty(brand.mo_ta) ? brand.mo_ta : "";
                        brand.slug = StringHelper.convertToUnSign3(brand.ten_thuong_hieu);
                        brand.nguoi_tao = UserName;
                        brand.ngay_tao = DateTime.Now;
                        brand.nguoi_cap_nhat = UserName;
                        brand.ngay_cap_nhat = DateTime.Now;
                        db.Insert(brand);
                    }
                    //SyncToMySQL
                    List<SqlParameter> lstParameter = new List<SqlParameter>();
                    lstParameter.Clear();
                    lstParameter.Add(new SqlParameter("@ma_thuong_hieu", brand.ma_thuong_hieu));
                    new SqlHelper(connectionString).ExecuteNoneQuery("p_Brand_SyncToMySQL", lstParameter);
                    return "true@@" + brand.ma_thuong_hieu;
                }
                catch (Exception e)
                {
                    return "false@@" + e.Message;
                }
            }
        }
        public string Delete(string ma_thuong_hieu, string connectionString)
        {
            using (var db = new OrmliteConnection().openConn(connectionString))
            {
                try
                {
                    var checkID = db.SingleOrDefault<BrandManagement>("ma_thuong_hieu={0}", ma_thuong_hieu);
                    if (checkID == null)
                    {
                        return "Không có thương hiệu này!";
                    }
                    List<SqlParameter> param = new List<SqlParameter>();
                    param.Add(new SqlParameter("@ma_thuong_hieu", ma_thuong_hieu));
                    new SqlHelper().ExecuteNoneQuery("delete BrandManagement where ma_thuong_hieu = @ma_thuong_hieu", param, CommandType.Text);
                    return "true";
                }
                catch (Exception e)
                {
                    return e.Message.ToString();
                }

            }
        }
    }
}
