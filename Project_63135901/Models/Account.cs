using System;
using System.Collections.Generic;

#nullable disable

namespace Project_63135901.Models
{
    public partial class Account
    {
        public int AccountId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string AccPassword { get; set; }
        public string AdvPassword { get; set; }
        public bool Active { get; set; }
        public string Fullname { get; set; }
        public int? RoleId { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual Role Role { get; set; }
    }
}
