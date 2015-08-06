using System;

namespace Shop.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime AddDate { get; set; }
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}