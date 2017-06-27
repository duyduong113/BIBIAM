using ServiceStack.DataAnnotations;
using System;

namespace ERPAPD.Models
{
    public class CRM_Manage_Sync_Log
    {
        [AutoIncrement]
        public int ID { get; set; }
        public string SpNameSync { get; set; }
        public string SpNameSync24H { get; set; }
        public string TableNameSync { get; set; }
        public string DataType { get; set; }
        public string ErrorMessage { get; set; } 
        public int TypeSync { get; set; }  
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int InsertedRow { get; set; }
        public int UpdatedRow { get; set; }
        public int IncrementalRow { get; set; }
        public int CleanedRow { get; set; }
        public int TotalRow { get; set; }
        public string Status { get; set; }
        public DateTime RowCreatedAt { get; set; }
        public string RowCreatedBy { get; set; }
        public DateTime RowLastUpdatedAt { get; set; }
        public string RowLastUpdatedBy { get; set; }
    }
    public class CRM_Sync_Log
    {
        [AutoIncrement]
        public int ID { get; set; }
        public string DataType { get; set; }
        public string StoreName { get; set; }
        public string StoreName2 { get; set; }
        public string TableName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int InsertedRows { get; set; }
        public int UpdatedRows { get; set; }
        public int IncrementalRows { get; set; }
        public int TotalRows { get; set; }
        public string SyncType { get; set; }
        public string Status { get; set; }
        public string ErrorMessage { get; set; } 
        public DateTime RowCreatedAt { get; set; }
        public string RowCreatedBy { get; set; }
  
    }
    
}