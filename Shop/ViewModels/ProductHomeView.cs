using System.ComponentModel.DataAnnotations;

namespace Shop.ViewModels
{
    public class ProductHomeView
    {
        public int Id { get; set; }
        [UIHint("Stars")]
        public int Rating { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ReviewsCount { get; set; }
    }
}