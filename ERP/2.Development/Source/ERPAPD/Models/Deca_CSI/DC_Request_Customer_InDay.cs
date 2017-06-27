using ServiceStack.DataAnnotations;

namespace ERPAPD.Models
{
    public class DC_Request_Customer_InDay
    {
        [PrimaryKey]
        public string CustomerID { get; set; }
        public string Requestor { get; set; }
        public string RequestedAt { get; set; }
    }
}