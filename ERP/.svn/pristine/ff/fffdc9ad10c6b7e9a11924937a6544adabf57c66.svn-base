using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    public class khu_vuc
    {
        #region Members
        private string _aliasid = String.Empty;
        public string ma_khu_vuc { get { return _aliasid; } set { this._aliasid = value; } }

        private string _aliasname = String.Empty;
        public string ten_khu_vuc { get { return _aliasname; } set { this._aliasname = value; } }

        private bool _active;
        public bool trang_thai { get { return _active; } set { this._active = value; } }

        private int _rowid;
        public int RowID { get { return _rowid; } set { this._rowid = value; } }

        private DateTime _rowcreatedtime;
        public DateTime ngay_tao { get { return _rowcreatedtime; } set { this._rowcreatedtime = value; } }

        private string _rowcreateduser = String.Empty;
        public string nguoi_tao { get { return _rowcreateduser; } set { this._rowcreateduser = value; } }

        private DateTime _rowlastupdatedtime;
        public DateTime ngay_cap_nhat { get { return _rowlastupdatedtime; } set { this._rowlastupdatedtime = value; } }

        private string _rowlastupdateduser = String.Empty;
        public string nguoi_cap_nhat { get { return _rowlastupdateduser; } set { this._rowlastupdateduser = value; } }

        #endregion

        public khu_vuc()
        { }

        public khu_vuc(string AliasID, string AliasName, bool Active, int RowID, DateTime RowCreatedTime, string RowCreatedUser, DateTime RowLastUpdatedTime, string RowLastUpdatedUser)
        {
            this._aliasid = AliasID;
            this._aliasname = AliasName;
            this._active = Active;
            this._rowid = RowID;
            this._rowcreatedtime = RowCreatedTime;
            this._rowcreateduser = RowCreatedUser;
            this._rowlastupdatedtime = RowLastUpdatedTime;
            this._rowlastupdateduser = RowLastUpdatedUser;
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveCRM_Location_Alias";
            SqlParameter[] parameters = new SqlParameter[5];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@AliasID";
            parameters[i].Value = this._aliasid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@AliasName";
            parameters[i].Value = this._aliasname;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Active";
            parameters[i].Value = this._active;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedTime";
            parameters[i].Value = this._rowcreatedtime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowCreatedUser";
            parameters[i].Value = this._rowcreateduser;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateCRM_Location_Alias";
            SqlParameter[] parameters = new SqlParameter[5];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@AliasID";
            parameters[i].Value = this._aliasid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@AliasName";
            parameters[i].Value = this._aliasname;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Active";
            parameters[i].Value = this._active;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedTime";
            parameters[i].Value = this._rowlastupdatedtime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@RowLastUpdatedUser";
            parameters[i].Value = this._rowlastupdateduser;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Delete()
        {
            string PROCEDURE = "p_DeleteCRM_Location_Alias";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@AliasID";
            parameters[0].Value = ma_khu_vuc;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public static khu_vuc GetDC_Location_Alia(string AliasID)
        {
            string PROCEDURE = "p_SelectCRM_Location_Alias";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@AliasID";
            parameters[0].Value = AliasID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new khu_vuc
            {
                ma_khu_vuc = row["ma_khu_vuc"].ToString(),
                ten_khu_vuc = row["ten_khu_vuc"].ToString(),
                trang_thai = Convert.ToBoolean(row["trang_thai"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                ngay_tao = Convert.ToDateTime(row["ngay_tao"].ToString()),
                nguoi_tao = row["nguoi_tao"].ToString(),
                ngay_cap_nhat = Convert.ToDateTime(row["ngay_cap_nhat"].ToString()),
                nguoi_cap_nhat = row["nguoi_cap_nhat"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static List<khu_vuc> GetCRM_Location_Alias(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectCRM_Location_AliasDynamic";
            SqlParameter[] parameters = new SqlParameter[2];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@WhereCondition";
            parameters[i].Value = whereCondition;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@OrderByExpression";
            parameters[i].Value = orderByExpression;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new khu_vuc
            {
                ma_khu_vuc = row["ma_khu_vuc"].ToString(),
                ten_khu_vuc = row["ten_khu_vuc"].ToString(),
                trang_thai = Convert.ToBoolean(row["trang_thai"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                ngay_tao = Convert.ToDateTime(row["ngay_tao"].ToString()),
                nguoi_tao = row["nguoi_tao"].ToString(),
                ngay_cap_nhat = Convert.ToDateTime(row["ngay_cap_nhat"].ToString()),
                nguoi_cap_nhat = row["nguoi_cap_nhat"].ToString()
            }).ToList();
        }

        public static List<khu_vuc> GetAllCRM_Location_Alias()
        {
            string PROCEDURE = "p_SelectCRM_Location_AliasAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new khu_vuc
            {
                ma_khu_vuc = row["ma_khu_vuc"].ToString(),
                ten_khu_vuc = row["ten_khu_vuc"].ToString(),
                trang_thai = Convert.ToBoolean(row["trang_thai"].ToString()),
                RowID = Convert.ToInt32(row["RowID"].ToString()),
                ngay_tao = Convert.ToDateTime(row["ngay_tao"].ToString()),
                nguoi_tao = row["nguoi_tao"].ToString(),
                ngay_cap_nhat = Convert.ToDateTime(row["ngay_cap_nhat"].ToString()),
                nguoi_cap_nhat = row["nguoi_cap_nhat"].ToString()
            }).ToList();
        }

        public static List<khu_vuc> GetList_Alias()
        {
            string PROCEDURE = "select distinct ma_khu_vuc,ten_khu_vuc from CRM_Location_Alias  where trang_thai = 1";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.Text, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new khu_vuc
            {
                ma_khu_vuc = row["ma_khu_vuc"].ToString(),
                ten_khu_vuc = row["ten_khu_vuc"].ToString(),
            }).ToList();
        }
    }
}