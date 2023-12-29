using System;
using System.Collections.Generic;

namespace Project_63135901.Models
{
    public partial class TransactionStatus
    {
        public TransactionStatus()
        {
            Orders = new HashSet<Order>();
        }

        public int TransactStatusId { get; set; }
        public string? TransStatus { get; set; }
        public string? TransactDescription { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
