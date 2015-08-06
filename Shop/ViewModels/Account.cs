using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shop.ViewModels
{
    public class Account
    {
        [DisplayName("نام کاربری")]
        [Required(ErrorMessage = "نام کاربری اجباری میباشد")]
        public string UserName { get; set; }

        [DisplayName("رمز عبور")]
        [Required(ErrorMessage = "رمز اجباری میباشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool  RememberMe { get; set; }
    }
}