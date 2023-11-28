using System;
using System.Collections.Generic;

#nullable disable

namespace Project_63135901.Models
{
    public partial class Role
    {
        public Role()
        {
            Accounts = new HashSet<Account>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
