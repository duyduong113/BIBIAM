using System;
using System.Collections.Generic;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;

namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a CRM_Position.
    /// </summary>
    public class CRM_Position
    {
        #region Members
        [AutoIncrement]
        public int Id { get; set; }
        public string PositionID { get; set; }
        public string PositionName { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDatetime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
        public string LastUpdatedUser { get; set; }

        #endregion
       
        public static List<CRM_Position> GetAllCRM_Positions()
        {
            var dbConn = Helpers.OrmliteConnection.openConn();
            return dbConn.Select<CRM_Position>();
        }
    }

}
