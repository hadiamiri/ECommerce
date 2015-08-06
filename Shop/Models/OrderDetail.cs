namespace Shop.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        
        public virtual Order Order { get; set; }
        
        public string ProductName { get; set; }
        public int Count { get; set; }
    }
}