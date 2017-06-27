﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBIAM.Core
{
    public static class AllConstant
    {
        //public const string Trang_Thai_SP_Hoat_Dong = "Active";
        //public const string Trang_Thai_SP_Ngung_Hoat_Dong = "InActive";
        //public const string Trang_Thai_Duyet_SP_Tu_Choi = "Rejected";
        //public const string Trang_Thai_Duyet_SP_Cho_Duyet = "New";
        //public const string Trang_Thai_Duyet_SP_Duyet = "Approved";
        //public const string Trang_Thai_XB_SP_Moi = "CHUA_XUAT_BAN";
        //public const string Trang_Thai_XB_SP_XB = "DA_XUAT_BAN";
        //public const string Trang_Thai_Product_Hierarchy_Active = "DANG_SU_DUNG";
        //public const string Trang_Thai_Product_Hierarchy_InActive = "KHONG_SU_DUNG";

        public const string FoldderName_Merchant_Voucher = "Thẻ giảm giá";
        public const string FoldderName_Merchant_Product = "Sản Phẩm";
        public const string FoldderName_Merchant_Catalog = "Catalog";
        public const string FoldderName_Merchant_Article = "Article";
        public const string FoldderName_Banner = "Banner";
        public const string FoldderName_Footer = "Footer";
        public const string FoldderName_Brand = "Brand";
        public const string FoldderName_Merchant = "UserProfile";
        public const string FoldderName_User = "User";
        public const string ProjectName_ERP = "ERP";
        public const string ProjectName_MCC = "MCC";
        public static string KeyAPI = SqlHelper.GetMd5Hash(AppConfigs.ReadConfig("KeyAPI"));

        public static class trang_thai
        {
            public const string DANG_SU_DUNG = "DANG_SU_DUNG";
            public const string KHONG_SU_DUNG = "KHONG_SU_DUNG";
        }
        public static class trang_thai_xuat_kho
        {
            public const string DA_XUAT_KHO = "DA_XUAT_KHO";
        }
        public static class trang_thai_duyet
        {
            public const string DA_DUYET = "DA_DUYET";
            public const string CHUA_DUYET = "CHUA_DUYET";
            public const string TU_CHOI = "TU_CHOI";
            public const string NHAP = "NHAP";
        }
        public static class trang_thai_xuat_ban
        {
            public const string DA_XUAT_BAN = "DA_XUAT_BAN";
            public const string CHUA_XUAT_BAN = "CHUA_XUAT_BAN";
        }
        public static class trang_thai_don_hang
        {
            public const string NEW = "New";
            public const string CONFIRM = "Confirm";
            public const string SHIPPING = "Shipping";
            public const string POD = "POD";
            public const string COMPLETED = "Completed";
            public const string CANCEL = "cancel";
        }
        public static class trang_thai_xac_thuc
        {
            public const string DA_XAC_THUC = "DA_XAC_THUC";
            public const string CHUA_XAC_THUC = "CHUA_XAC_THUC";
        }
        
    }
}
