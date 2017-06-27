using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using System.Data.SqlClient;

namespace ERPAPD.Models
{
    public class Deca_Customer_Rule
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string PhysicalID { get; set; }
        public string Rule { get; set; }
        public DateTime RuleDate { get; set; }
        public bool isRequest { get; set; }
        public bool isAvoid { get; set; }
        public string CustID { get; set; }
        public string CustName { get; set; }
        public string Phone { get; set; }
        public int CompanyID { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}