﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace prs_server.Models
{
    public class Product
    {

        public int Id { get; set; }
        [StringLength(30), Required]
        public string PartNbr { get; set; }
        [StringLength(30), Required]
        public string Name { get; set; }
        [Column(TypeName = "decimal(11,2)")]
        public decimal Price { get; set; }
        [StringLength(30), Required]
        public string Unit { get; set; }
        [StringLength(255)]
        public string PhotoPath { get; set; }
        public int VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }
        [JsonIgnore]
        public virtual ICollection<RequestLine> RequestLines { get; set; }



        public Product () { }
    }
}
