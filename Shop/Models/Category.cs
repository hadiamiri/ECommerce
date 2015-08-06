using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class Category
    {
        public int Id { get; set; }
        [DisplayName("نام گروه کالا")]
        [Required(ErrorMessage = "نام گروه کالا اجباری میباشد")]
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}