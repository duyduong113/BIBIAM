using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERPAPD.Models
{
    public class DC_SignedVietinReponse_Log
    {
        [AutoIncrement]
        public int Id { get; set; }
        public DC_SignedVietinReponse log { get; set; }
        public DateTime createdAt { get; set; }
        [StringLengthAttribute(100)]
        public string createdBy { get; set; }
    }
}