using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    public class DC_Article
    {
        #region Members
        private int _articleid;
        public int ArticleId { get { return _articleid; } set { this._articleid = value; } }

        private string _title = String.Empty;
        public string Title { get { return _title; } set { this._title = value; } }

        private string _postcontent = String.Empty;
        public string PostContent { get { return _postcontent; } set { this._postcontent = value; } }

        private bool _active;
        public bool Active { get { return _active; } set { this._active = value; } }

        private DateTime _updateddate;
        public DateTime UpdatedDate { get { return _updateddate; } set { this._updateddate = value; } }

        private string _updatedby = String.Empty;
        public string UpdatedBy { get { return _updatedby; } set { this._updatedby = value; } }

        #endregion

        public DC_Article()
        { }

        public DC_Article(int ArticleId, string Title, string PostContent, bool Active, DateTime UpdatedDate, string UpdatedBy)
        {
            this._articleid = ArticleId;
            this._title = Title;
            this._postcontent = PostContent;
            this._active = Active;
            this._updateddate = UpdatedDate;
            this._updatedby = UpdatedBy;
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveDC_Article";
            SqlParameter[] parameters = new SqlParameter[6];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ArticleId";
            parameters[i].Value = this._articleid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Title";
            parameters[i].Value = this._title;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@PostContent";
            parameters[i].Value = this._postcontent;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Active";
            parameters[i].Value = this._active;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UpdatedDate";
            parameters[i].Value = this._updateddate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UpdatedBy";
            parameters[i].Value = this._updatedby;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);

        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateDC_Article";
            SqlParameter[] parameters = new SqlParameter[6];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ArticleId";
            parameters[i].Value = this._articleid;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Title";
            parameters[i].Value = this._title;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@PostContent";
            parameters[i].Value = this._postcontent;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Active";
            parameters[i].Value = this._active;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UpdatedDate";
            parameters[i].Value = this._updateddate;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@UpdatedBy";
            parameters[i].Value = this._updatedby;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Delete()
        {
            string PROCEDURE = "p_DeleteDC_Article";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ArticleId";
            parameters[0].Value = ArticleId;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public static DC_Article GetDC_Article(int ArticleId)
        {
            string PROCEDURE = "p_SelectDC_Article";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ArticleId";
            parameters[0].Value = ArticleId;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_Article
            {
                ArticleId = Convert.ToInt16(row["ArticleId"].ToString()),
                Title = row["Title"].ToString(),
                PostContent = row["PostContent"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                UpdatedDate = Convert.ToDateTime(row["UpdatedDate"].ToString()),
                UpdatedBy = row["UpdatedBy"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static List<DC_Article> GetDC_Articles(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_ArticlesDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_Article
            {
                ArticleId = Convert.ToInt16(row["ArticleId"].ToString()),
                Title = row["Title"].ToString(),
                PostContent = row["PostContent"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                UpdatedDate = Convert.ToDateTime(row["UpdatedDate"].ToString()),
                UpdatedBy = row["UpdatedBy"].ToString()
            }).ToList();
        }

        public static List<DC_Article> GetAllDC_Articles()
        {
            string PROCEDURE = "p_SelectDC_ArticlesAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_Article
            {
                ArticleId = Convert.ToInt16(row["ArticleId"].ToString()),
                Title = row["Title"].ToString(),
                PostContent = row["PostContent"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                UpdatedDate = Convert.ToDateTime(row["UpdatedDate"].ToString()),
                UpdatedBy = row["UpdatedBy"].ToString()
            }).ToList();
        }
    }
}
