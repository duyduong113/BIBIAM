using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBIAM.Core.Entities
{
    public class UserActivation : BaseEntity
    {
        public string UserName { get; set;  }
        public string MerchantName { get; set; } 
        public string Email { get; set; }
        public string ActivationCode { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DeadTime { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
    }
}
