using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public DateTime AddDate { get; set; }
        public int SellCount { get; set; }
        public int CategoryId { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        
        public byte[] ImageData { get; set; }
        public byte[] ThumbnailImage { get; set; }
        public string ImageMimeType { get; set; }
    }
}