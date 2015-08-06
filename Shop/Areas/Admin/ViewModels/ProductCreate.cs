using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shop.Areas.Admin.ViewModels
{
    public class ProductCreate
    {
        public int Id { get; set; }
        [DisplayName("نام کالا")]
        [Required(ErrorMessage = "این فیلد اجباری میباشد")]
        public string Name { get; set; }
        [DisplayName("توضیحات")]
        [Required(ErrorMessage = "این فیلد اجباری میباشد")]
        public string Description { get; set; }
        [DisplayName("قیمت")]
        [Required(ErrorMessage = "این فیلد اجباری میباشد")]
        public decimal Price { get; set; }
        [DisplayName("دسته")]
        [Required(ErrorMessage = "این فیلد اجباری میباشد")]
      
        public int CategoryId { get; set; }
      
    }
}