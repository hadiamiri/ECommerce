using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Shop.Areas.Admin.ViewModels
{
    public class ContentCreateModel
    {
        public int Id { get; set; }
        [DisplayName("نام محتوا")]
        [Required(ErrorMessage = "نام را حتما وارد کنید")]
        public string Name { get; set; }

        [AllowHtml]
        [DisplayName("متن محتوا")]
        [Required(ErrorMessage = "متن محتوا را وارد کنید")]
        public string Text { get; set; }
    }
}