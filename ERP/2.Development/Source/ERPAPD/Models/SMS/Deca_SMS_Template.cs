using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ERPAPD.Models
{
    /// <summary>
    /// This object represents the properties and methods of a MCA_SMS_Template.
    /// </summary>
    public class Deca_SMS_Template
    {
        [AutoIncrement]
        [PrimaryKey]
        public int ID { get; set; }
        [StringLength(128)]
        public string Title { get; set; }

        public int MaxNumber { get; set; }
        [StringLength(1000)]
        public string Message { get; set; }
        [StringLength(500)]
        public string Roles { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLength(100)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        [StringLength(100)]
        public string UpdatedBy { get; set; }

        public Deca_SMS_Template()
        { }

    }
}
