using System;
using System.Collections.Generic;

#nullable disable

namespace Project_63135901.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ShortDescription { get; set; }
        public string ProductDescription { get; set; }
        public int? CatId { get; set; }
        public int? Price { get; set; }
        public decimal? Discount { get; set; }
        public string Thumb { get; set; }
        public string Video { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool BestSellers { get; set; }
        public bool HomeFlag { get; set; }
        public bool Active { get; set; }
        public string Tags { get; set; }
        public string Title { get; set; }
        public string Alias { get; set; }
        public string MetaDesc { get; set; }
        public string MetaKey { get; set; }
        public int? UnitsInStock { get; set; }

        public virtual Category Cat { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
