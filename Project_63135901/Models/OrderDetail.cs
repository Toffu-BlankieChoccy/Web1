using System;
using System.Collections.Generic;

namespace Project_63135901.Models
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? OrderNumber { get; set; }
        public int? Price { get; set; }
        public int? Quantity { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Total { get; set; }
        public DateTime? ShipDate { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}
