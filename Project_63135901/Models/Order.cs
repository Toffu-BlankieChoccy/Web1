using System;
using System.Collections.Generic;

namespace Project_63135901.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int? CustomersId { get; set; }
        public int? LocationId { get; set; }
        public string? District { get; set; }
        public string? Ward { get; set; }
        public string? CusAddress { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public int? TransactStatusId { get; set; }
        public bool? Deleted { get; set; }
        public bool? Paid { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int? PaymentId { get; set; }
        public decimal? TotalMoney { get; set; }
        public string? Note { get; set; }

        public virtual Customer? Customers { get; set; }
        public virtual Location? Location { get; set; }
        public virtual TransactionStatus? TransactStatus { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
