namespace ERPAPD.Models.CRM_Ticket
{
    public class CRM_Ticket_API
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string TypeID { get; set; }
        public string Status { get; set; }
        public string AgentConsoleAcc { get; set; }
        public string Priority { get; set; }
        public string Impact { get; set; }

        public CRM_Ticket_API()
        {
            Title = "";
            Description = "";
            Phone = "";
            TypeID = "";
            Status = "New";
            AgentConsoleAcc = "";
            Priority = "Medium";
            Impact = "Medium";
        }
    }
}