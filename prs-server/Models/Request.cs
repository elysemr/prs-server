﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace prs_server.Models
{
    public class Request
    {
        public int Id { get; set; }
        [StringLength(80), Required]
        public string Description { get; set; }
        [StringLength(80), Required]
        public string Justification { get; set; }
        [StringLength(80)]
        public string RejectionReason { get; set; }
        [StringLength(20), Required]
        public string DeliveryMode { get; set; } = "Pickup";
        [StringLength(10), Required]
        public string Status { get; set; } = "NEW";
        [Column(TypeName = "decimal(11,2)")]
        public decimal Total { get; set; } = 0;
        public int UserId { get; set; }
  
        public virtual User User { get; set; }
        public virtual IEnumerable<RequestLine> RequestLines { get; set; }


        public Request () { }
    }
}
