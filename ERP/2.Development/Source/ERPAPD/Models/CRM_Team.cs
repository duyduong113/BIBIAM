using System;
using System.Collections.Generic;
using ServiceStack.OrmLite;
namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a CRM_Team.
    /// </summary>
    public class CRM_Team
    {
        #region Members
        public int ID { get; set; }
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
        public string LastUpdatedUser { get; set; }
        public string Manager { get; set; }
        public int FKUnit { get; set; }
        public string Code { get; set; }
        #endregion
        public static List<CRM_Team> GetAllCRM_Teams()
        {
            var dbConn = Helpers.OrmliteConnection.openConn();
            return dbConn.Select<CRM_Team>();
        }
    }
}
