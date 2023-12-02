using System;
using System.Collections.Generic;

namespace Project_63135901.Models
{
    public partial class Shipper
    {
        public int ShipperId { get; set; }
        public string? ShipperName { get; set; }
        public string? Phone { get; set; }
        public string? Company { get; set; }
        public DateTime? ShipDate { get; set; }
    }
}
