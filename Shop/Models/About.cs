using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Shop.Models
{
    public class About
    {
        public int Id { get; set; }
        [AllowHtml]
        [DisplayName("متن")]
        [Required(ErrorMessage = "متن نمیتواند خالی باشد")]
        public string Text { get; set; }
    }
}