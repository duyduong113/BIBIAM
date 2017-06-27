using ServiceStack.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace ERPAPD.Models
{
    public class CRM_Hierarchy
    {
        #region Members
        [AutoIncrement]
        public int ID 
        { get; set; }
        [Required]
        public string HierarchyID 
        { get; set; }
          [Required]
        public string HierarchyType 
        { get; set; }
          [Required]
        public string Value 
        { get; set; }
        //[Required]
        public string ParentID
        { get; set; }
        [Required]
        public int Level 
        { get; set; }
        public bool Status 
        { get; set; }
        public DateTime? CreatedAt 
        { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? 
            UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        //meta
        #endregion
        public CRM_Hierarchy()
        {
            HierarchyID = HierarchyType =Value= ParentID="";
            CreatedAt = DateTime.Now;
            CreatedBy = "";
            UpdatedAt = DateTime.Parse("1900-01-01");
            UpdatedBy = "";
            Status = true;
            Level = 0;
        }
    }
    public class CRM_CategoryHierarchy
    {
        #region Members
        [AutoIncrement]
        public int ID
        { get; set; }
        [Required]
        public string HierarchyID
        { get; set; }
        [Required]
        public string HierarchyType
        { get; set; }
        [Required]
        public string Value
        { get; set; }
        //[Required]
        public string ParentID
        { get; set; }
        [Required]
        public int Level
        { get; set; }
        public bool Status
        { get; set; }
        public DateTime? CreatedAt
        { get; set; }
        public string CreatedBy { get; set; }
        public DateTime?
            UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        //meta
        #endregion
        //public CRM_CategoryHierarchy()
        //{
        //    HierarchyID = HierarchyType = Value = ParentID = "";
        //    CreatedAt = DateTime.Now;
        //    CreatedBy = "";
        //    UpdatedAt = DateTime.Parse("1900-01-01");
        //    UpdatedBy = "";
        //    Status = true;
        //    Level = 0;
        //}
    }
    public class CRM_PageHierarchy
    {
        #region Members
        [AutoIncrement]
        public int ID
        { get; set; }
        [Required]
        public string HierarchyID
        { get; set; }
        [Required]
        public string HierarchyType
        { get; set; }
        [Required]
        public string Value
        { get; set; }
        //[Required]
        public string ParentID
        { get; set; }
        [Required]
        public int Level
        { get; set; }
        public bool Status
        { get; set; }
        public DateTime? CreatedAt
        { get; set; }
        public string CreatedBy { get; set; }
        public DateTime?
            UpdatedAt
        { get; set; }
        public string UpdatedBy { get; set; }
        //meta
        #endregion
        public CRM_PageHierarchy()
        {
            HierarchyID = HierarchyType = Value = ParentID = "";
            CreatedAt = DateTime.Now;
            CreatedBy = "";
            UpdatedAt = DateTime.Parse("1900-01-01");
            UpdatedBy = "";
            Status = true;
            Level = 0;
        }
    }
}