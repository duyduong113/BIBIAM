using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a DC_NumberNetwork.
    /// </summary>
    public class DC_NumberNetwork
    {
        #region Members
        private int _id;
        public int ID { get { return _id; } set { this._id = value; } }

        private int _id_network;
        //[UIHint("HomeNetwork")]
        public int ID_Network { get { return _id_network; } set { this._id_network = value; } }

        [Required]
        [Range(0, 99999999999999)]
        [Display(Name = "Please check numer")]
        private string _number = String.Empty;
        
        [Required]
        [Range(0, 99999999999999)]
        [Display(Name = "Please check numer")]
        public string Number { get { return _number; } set { this._number = value; } }

        private bool _active;
        public bool Active { get { return _active; } set { this._active = value; } }

        private string _type = String.Empty;
        public string Type { get { return _type; } set { this._type = value; } }

        private DateTime _createddatetime;
        public DateTime CreatedDatetime { get { return _createddatetime; } set { this._createddatetime = value; } }

        private string _createduser = String.Empty;
        public string CreatedUser { get { return _createduser; } set { this._createduser = value; } }

        private DateTime _lastupdateddatetime;
        public DateTime LastUpdatedDateTime { get { return _lastupdateddatetime; } set { this._lastupdateddatetime = value; } }

        private string _lastupdateduser = String.Empty;
        public string LastUpdatedUser { get { return _lastupdateduser; } set { this._lastupdateduser = value; } }

        public string ValidText { get; set; }

        public string Telco { get; set; }

        #endregion
        public string NetworkName { get; set; }
        public DC_NumberNetwork()
        { }

        public DC_NumberNetwork(int ID, int ID_Network, string Number, bool Active, string Type, DateTime CreatedDatetime, string CreatedUser, DateTime LastUpdatedDateTime, string LastUpdatedUser)
        {
            this._id = ID;
            this._id_network = ID_Network;
            this._number = Number;
            this._active = Active;
            this._type = Type;
            this._createddatetime = CreatedDatetime;
            this._createduser = CreatedUser;
            this._lastupdateddatetime = LastUpdatedDateTime;
            this._lastupdateduser = LastUpdatedUser;
        }

        public int Save()
        {
            string PROCEDURE = "p_SaveDC_NumberNetwork";
            SqlParameter[] parameters = new SqlParameter[9];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ID";
            parameters[i].Value = this._id;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ID_Network";
            parameters[i].Value = this._id_network;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Number";
            parameters[i].Value = this._number;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Active";
            parameters[i].Value = this._active;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Type";
            parameters[i].Value = this._type;
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
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Update()
        {
            string PROCEDURE = "p_UpdateDC_NumberNetwork";
            SqlParameter[] parameters = new SqlParameter[9];
            int i = 0;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ID";
            parameters[i].Value = this._id;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@ID_Network";
            parameters[i].Value = this._id_network;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Number";
            parameters[i].Value = this._number;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Active";
            parameters[i].Value = this._active;
            i++;
            parameters[i] = new SqlParameter();
            parameters[i].ParameterName = "@Type";
            parameters[i].Value = this._type;
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
            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public int Delete()
        {
            string PROCEDURE = "p_DeleteDC_NumberNetwork";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ID";
            parameters[0].Value = ID;

            return SqlHelperAsync.ExecuteNonQuery(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
        }

        public static DC_NumberNetwork GetDC_NumberNetwork(int ID)
        {
            string PROCEDURE = "p_SelectDC_NumberNetwork";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter();
            parameters[0].ParameterName = "@ID";
            parameters[0].Value = ID;

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, parameters);
            return dt.AsEnumerable().Select(row => new DC_NumberNetwork
            {
                ID = Convert.ToInt32(row["ID"].ToString()),
                ID_Network = Convert.ToInt32(row["ID_Network"].ToString()),
                Number = row["Number"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                Type = row["Type"].ToString(),
                CreatedDatetime = Convert.ToDateTime(row["CreatedDatetime"].ToString()),
                CreatedUser = row["CreatedUser"].ToString(),
                LastUpdatedDateTime = Convert.ToDateTime(row["LastUpdatedDateTime"].ToString()),
                LastUpdatedUser = row["LastUpdatedUser"].ToString()
            }).ToList().FirstOrDefault();
        }

        public static List<DC_NumberNetwork> GetDC_NumberNetworks(string whereCondition, string orderByExpression)
        {
            string PROCEDURE = "p_SelectDC_NumberNetworksDynamic";
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
            return dt.AsEnumerable().Select(row => new DC_NumberNetwork
            {
                ID = Convert.ToInt32(row["ID"].ToString()),
                ID_Network = Convert.ToInt32(row["ID_Network"].ToString()),
                Number = row["Number"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                Type = row["Type"].ToString(),
                CreatedDatetime = Convert.ToDateTime(row["CreatedDatetime"].ToString()),
                CreatedUser = row["CreatedUser"].ToString(),
                LastUpdatedDateTime = Convert.ToDateTime(row["LastUpdatedDateTime"].ToString()),
                LastUpdatedUser = row["LastUpdatedUser"].ToString()
            }).ToList();
        }

        public static List<DC_NumberNetwork> GetAllDC_NumberNetworks()
        {
            string PROCEDURE = "p_SelectDC_NumberNetworksAll";

            DataTable dt = SqlHelperAsync.ExecuteDataTable(Constants.AllConstants().CONNECTION_STRING, CommandType.StoredProcedure, PROCEDURE, null);
            return dt.AsEnumerable().Select(row => new DC_NumberNetwork
            {
                ID = Convert.ToInt32(row["ID"].ToString()),
                ID_Network = Convert.ToInt32(row["ID_Network"].ToString()),
                Number = row["Number"].ToString(),
                Active = Convert.ToBoolean(row["Active"].ToString()),
                Type = row["Type"].ToString(),
                CreatedDatetime = Convert.ToDateTime(row["CreatedDatetime"].ToString()),
                CreatedUser = row["CreatedUser"].ToString(),
                LastUpdatedDateTime = Convert.ToDateTime(row["LastUpdatedDateTime"].ToString()),
                LastUpdatedUser = row["LastUpdatedUser"].ToString(),
                ValidText = row["ValidText"].ToString(),
                NetworkName = row["NetworkName"].ToString(),
            }).ToList();
        }


       
    }
}
