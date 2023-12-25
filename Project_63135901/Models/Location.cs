using System;
using System.Collections.Generic;

namespace Project_63135901.Models
{
    public partial class Location
    {
        public Location()
        {
            Customers = new HashSet<Customer>();
            Orders = new HashSet<Order>();
        }

        public int LocationId { get; set; }
        public string? LocationName { get; set; }
        public string? LocationType { get; set; }
        public string? Slug { get; set; }
        public string? NameWithType { get; set; }
        public string? PathWithType { get; set; }
        public int? ParentCode { get; set; }
        public int? Levels { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
