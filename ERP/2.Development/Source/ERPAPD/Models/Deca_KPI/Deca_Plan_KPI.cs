using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ERPAPD.Models
{
    public class Deca_Plan_KPI
    {
        [AutoIncrement]
        public int Id { get; set; }
        [StringLengthAttribute(100)]
        [Required]
        public string Name { get; set; }
        [StringLengthAttribute(4)]
        [Required]
        public string Year { get; set; }
        [StringLengthAttribute(20)]
        [Index(Unique = true)]
        public string NameYear { get { return Name + Year; } }
        public double Month1 { get; set; }
        public double Month2 { get; set; }
        public double Month3 { get; set; }
        public double Month4 { get; set; }
        public double Month5 { get; set; }
        public double Month6 { get; set; }
        public double Month7 { get; set; }
        public double Month8 { get; set; }
        public double Month9 { get; set; }
        public double Month10 { get; set; }
        public double Month11 { get; set; }
        public double Month12 { get; set; }
        public DateTime CreatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLengthAttribute(100)]
        public string UpdatedBy { get; set; }

    }
}