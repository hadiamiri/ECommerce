using System;
using System.Collections.Generic;

namespace Shop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsPaid { get; set; }
        public virtual User User { get; set; }
        public decimal Price { get; set; }
        public string Address { get; set; }
        public virtual  ICollection<OrderDetail> OrderDetails{ get; set; }
    }
}