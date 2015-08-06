using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shop.ViewModels
{
    public class CommentViewModel
    {
        public string ProductName { get; set; }
        public int  Id { get; set; }
        [DisplayName("امتیاز شما")]
        [Required(ErrorMessage = "این فیلد اجباری مباشد")]
        [UIHint("Rate")]
        public int Rating { get; set; }
        [DisplayName("متن نظر")]
        [Required(ErrorMessage = "این فیلد اجباری مباشد")]
        public string Comment { get; set; }
    }
}