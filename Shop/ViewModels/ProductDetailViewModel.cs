using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Shop.Models;

namespace Shop.ViewModels
{
    public class ProductDetailViewModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ReviewsCount { get; set; }
        [UIHint("Stars")]
        public int Rating { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}