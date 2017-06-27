using ERPAPD.Models;
using ERPAPD.Models.API_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace ERPAPD.Controllers.WebApi
{
    [RoutePrefix("api/ticket")]
    public class TicketController : ApiController
    {
        // GET api/ticket
        [Route("gettype/{username}/{password}")]
        public IEnumerable<Deca_RT_Type> Get(string username, string password)
        {
            var data = new List<Deca_RT_Type>();
            if (username == "ocmcustomer" && password == "005ab6f1f7db9df17bb643bf335f30bf67f83379")
            {
                data = Deca_RT_Type.GetAllDeca_RT_Types().Where(s => s.TypeID != "TIC0092").ToList();
            }
            return data;
        }

        [Route("create")]
        [ResponseType(typeof(Ticket_Request_API))]
        public IHttpActionResult Post([FromBody] Ticket_Request_API data)
        {
            if (String.IsNullOrEmpty(data.ticket.ticketType) || String.IsNullOrEmpty(data.ticket.title) || String.IsNullOrEmpty(data.ticket.detail)
                || String.IsNullOrEmpty(data.ticket.priority) || String.IsNullOrEmpty(data.ticket.impact) //|| String.IsNullOrEmpty(data.ticket.status)
                || String.IsNullOrEmpty(data.ticket.customerId) || String.IsNullOrEmpty(data.ticket.customerName) || String.IsNullOrEmpty(data.ticket.customerSource)
                || String.IsNullOrEmpty(data.ticket.requestFrom))
            {
                return BadRequest("missing required params");
            }
            else if (!"TPRI001,TPRI002,TPRI003".Split(',').Contains(data.ticket.priority))
            {
                return BadRequest("priority is not valid");
            }
            else if (!"TIMP001,TIMP002,TIMP003".Split(',').Contains(data.ticket.impact))
            {
                return BadRequest("impact is not valid");
            }
            //sha-1 ocmcustomer2015
            if (data.username == "ocmcustomer" && data.password == "005ab6f1f7db9df17bb643bf335f30bf67f83379")
            {
                var checktype = Deca_RT_Type.GetDeca_RT_Type(data.ticket.ticketType);
                if (checktype == null)
                {
                    return BadRequest("ticketType is not valid");
                }
                var w = new Deca_RT_Ticket();
                w.Status = "New";
                w.TypeID = checktype.TypeID;
                w.Title = data.ticket.title;
                w.Detail = data.ticket.detail;
                w.Requestor = data.ticket.customerSource;
                w.Assignee = "";
                w.preAssignee = "";
                w.Owner = checktype.Owner;
                w.OrganizationID = "";
                w.CustomerID = data.ticket.customerId;
                w.CustomerName = data.ticket.customerName;
                w.Priority = data.ticket.priority;
                w.Impact = data.ticket.impact;
                w.CustomerSource = data.ticket.customerSource;
                w.RequestFrom = data.ticket.requestFrom;
                w.RowCreatedUser = data.ticket.customerSource;
                w.RowCreatedTime = DateTime.Now;
                var id = w.Save();
                if (id != "")
                {
                    //save logs
                    Deca_RT_Log savelog = new Deca_RT_Log();
                    savelog.TicketID = id;
                    savelog.Activites = "Add New Ticket";
                    savelog.RowCreatedTime = DateTime.Now;
                    savelog.RowCreatedUser = data.ticket.customerSource;
                    savelog.Save();
                }
                return Ok(id);
            }
            else
            {
                return BadRequest("username & password is not valid");
            }
        }

        [Route("getTicket/{username}/{password}/{fromDate}/{toDay}")]
        public IEnumerable<Deca_RT_Ticket> Get(string username, string password, string fromDate, string toDay)
        {
            var data = new List<Deca_RT_Ticket>();
            if (username == "ocmcustomer" && password == "005ab6f1f7db9df17bb643bf335f30bf67f83379")
            {
                data = Deca_RT_Ticket.GetAllDeca_RT_Tickets_ViewAll_Dynamic_API(fromDate, toDay);
            }
            return data;
        }

        [Route("getTicketById/{username}/{password}/{id}")]
        public Deca_RT_Ticket Get(string username, string password, string id)
        {
            var data = new Deca_RT_Ticket();
            if (username == "ocmcustomer" && password == "005ab6f1f7db9df17bb643bf335f30bf67f83379")
            {
                data = Deca_RT_Ticket.GetDeca_RT_Ticket(id);
            }
            return data;
        }


    }
}
