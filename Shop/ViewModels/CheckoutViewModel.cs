using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Shop.Models;

namespace Shop.ViewModels
{
    public class CheckoutViewModel
    {
        public List<CartLine> Lines { get; set; }
        [DisplayName("آدرس")]
        [Required(ErrorMessage = "این فیلد اجباری می باشد")]
        public string Address { get; set; }
    }
}