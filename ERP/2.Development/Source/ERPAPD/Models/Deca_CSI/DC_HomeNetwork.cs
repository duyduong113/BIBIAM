using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a DC_HomeNetwork.
    /// </summary>
    public class DC_HomeNetwork
    {
        #region Members
        private int _id;
        public int ID { get { return _id; } set { this._id = value; } }

        private string _networkname = String.Empty;
        public string NetworkName { get { return _networkname; } set { this._networkname = value; } }

        private string _decription = String.Empty;
        public string Decription { get { return _decription; } set { this._decription = value; } }

        private bool _active;
        public bool Active { get { return _active; } set { this._active = value; } }

        private DateTime _createddatetime;
        public DateTime CreatedDatetime { get { return _createddatetime; } set { this._createddatetime = value; } }

        private string _createduser = String.Empty;
        public string CreatedUser { get { return _createduser; } set { this._createduser = value; } }

        private DateTime _lastupdateddatetime;
        public DateTime LastUpdatedDateTime { get { return _lastupdateddatetime; } set { this._lastupdateddatetime = value; } }

        private string _lastupdateduser = String.Empty;
        public string LastUpdatedUser { get { return _lastupdateduser; } set { this._lastupdateduser = value; } }

        private string _validtext = String.Empty;
        public string ValidText { get { return _validtext; } set { this._validtext = value; } }

        #endregion

        public DC_HomeNetwork()
        { }

        public DC_HomeNetwork(int ID, string NetworkName, string Decription, bool Active, DateTime CreatedDatetime, string CreatedUser, DateTime LastUpdatedDateTime, string LastUpdatedUser, string ValidText)
        {
            this._id = ID;
            this._networkname = NetworkName;
            this._decription = Decription;
            this._active = Active;
            this._createddatetime = CreatedDatetime;
            this._createduser = CreatedUser;
            this._lastupdateddatetime = LastUpdatedDateTime;
            this._lastupdateduser = LastUpdatedUser;
            this._validtext = ValidText;
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveDC_HomeNetwork";
            SqlParameter[] parameters = new SqlParameter[9];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ID";
            parameters[i].Value = this._id;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@NetworkName";
            parameters[i].Value = this._networkname;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Decription";
            parameters[i].Value = this._decription;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Active";
            parameters[i].Value = this._active;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CreatedDatetime";
            parameters[i].Value = this._createddatetime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CreatedUser";
            parameters[i].Value = this._createduser;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LastUpdatedDateTime";
            parameters[i].Value = this._lastupdateddatetime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LastUpdatedUser";
            parameters[i].Value = this._lastupdateduser;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ValidText";
            parameters[i].Value = this._validtext;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateDC_HomeNetwork";
            SqlParameter[] parameters = new SqlParameter[9];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ID";
            parameters[i].Value = this._id;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@NetworkName";
            parameters[i].Value = this._networkname;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Decription";
            parameters[i].Value = this._decription;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Active";
            parameters[i].Value = this._active;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CreatedDatetime";
            parameters[i].Value = this._createddatetime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@CreatedUser";
            parameters[i].Value = this._createduser;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LastUpdatedDateTime";
            parameters[i].Value = this._lastupdateddatetime;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@LastUpdatedUser";
            parameters[i].Value = this._lastupdateduser;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ValidText";
            parameters[i].Value = this._validtext;
            i++;
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Delete()
        {
            string PROCEDURE = "p_DeleteDC_HomeNetwork";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ID";
            parameters[0].Value = ID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public static DC_HomeNetwork GetDC_HomeNetwork(int ID)
        {
            string PROCEDURE = "p_SelectDC_HomeNetwork";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ID";
            parameters[0].Value = ID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_HomeNetwork
            {
                ID = Convert.ToInt32(row["ID"].ToString()),
                NetworkName = row["NetworkName"].ToString(),
                Decription = row["Decription"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                CreatedDatetime = Convert.ToDateTime(row["CreatedDatetime"].ToString()),
                CreatedUser = row["CreatedUser"].ToString(),
                LastUpdatedDateTime = Convert.ToDateTime(row["LastUpdatedDateTime"].ToString()),
                LastUpdatedUser = row["LastUpdatedUser"].ToString(),
                ValidText = row["ValidText"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static List<DC_HomeNetwork> GetDC_HomeNetworks(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_HomeNetworksDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_HomeNetwork
            {
                ID = Convert.ToInt32(row["ID"].ToString()),
                NetworkName = row["NetworkName"].ToString(),
                Decription = row["Decription"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                CreatedDatetime = Convert.ToDateTime(row["CreatedDatetime"].ToString()),
                CreatedUser = row["CreatedUser"].ToString(),
                LastUpdatedDateTime = Convert.ToDateTime(row["LastUpdatedDateTime"].ToString()),
                LastUpdatedUser = row["LastUpdatedUser"].ToString(),
                ValidText = row["ValidText"].ToString()
            }).ToList();
        }

        public static List<DC_HomeNetwork> GetAllDC_HomeNetworks()
        {
            string PROCEDURE = "p_SelectDC_HomeNetworksAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_HomeNetwork
            {
                ID = Convert.ToInt32(row["ID"].ToString()),
                NetworkName = row["NetworkName"].ToString(),
                Decription = row["Decription"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                CreatedDatetime = Convert.ToDateTime(row["CreatedDatetime"].ToString()),
                CreatedUser = row["CreatedUser"].ToString(),
                LastUpdatedDateTime = Convert.ToDateTime(row["LastUpdatedDateTime"].ToString()),
                LastUpdatedUser = row["LastUpdatedUser"].ToString(),
                ValidText = row["ValidText"].ToString()
            }).ToList();
        }
    }
}
