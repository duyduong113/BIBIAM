using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using ServiceStack.OrmLite;

namespace ERPAPD.Models
{
    public class Deca_Customer_Meta
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }
        [Required]
        [StringLengthAttribute(50)]
        public string CustomerID { get; set; }
        [Required]
        [StringLengthAttribute(50)]
        public string Factor { get; set; }
        [Required]
        [StringLengthAttribute(255)]
        public string Value { get; set; }
        [StringLengthAttribute(50)]
        public string DataSource { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string UpdatedBy { get; set; }
        public bool InsertUpdate(IDbConnection dbConn)
        {
            try
            {
                Deca_Customer_Meta meta = dbConn.FirstOrDefault<Deca_Customer_Meta>("CustomerID = {0} AND DataSource= {1} AND Factor={2}", this.CustomerID, this.DataSource, this.Factor);
                if (meta == null)
                {
                    //insert meta
                    dbConn.Insert(this);
                }
                else
                {
                    //update meta
                    meta.UpdatedAt = DateTime.Now;
                    meta.UpdatedBy = this.UpdatedBy;
                    meta.Value = this.Value;
                    dbConn.Update(meta);
                }
                return true;
            }
            catch { return false; }

        }
    }
}