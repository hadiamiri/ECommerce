using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shop.ViewModels
{
    public class Register
    {
        [DisplayName("نام")]
        [Required(ErrorMessage = "این فیلد اجباری میباشد")]
        public string FirstName { get; set; }
        [DisplayName("نام خوانوادگی")]
        [Required(ErrorMessage = "این فیلد اجباری میباشد")]
        public string LastName { get; set; }
        [DisplayName("نام کاربری")]
        [Required(ErrorMessage = "این فیلد اجباری میباشد")]
        public string UserName { get; set; }
        [DisplayName("رمز عبور")]
        [Required(ErrorMessage = "این فیلد اجباری میباشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("تکرار رمز عبور")]
        [Required(ErrorMessage = "این فیلد اجباری میباشد")]
        [Compare("Password", ErrorMessage = "تکرار رمز با رمز یکی نمیباشد")]
        [DataType(DataType.Password)]
        public string PasswordRepeate { get; set; }
        [DisplayName("ایمیل")]
        [Required(ErrorMessage = "این فیلد اجباری میباشد")]
        [EmailAddress(ErrorMessage = "ایمیل معتبر نمیباشد")]
        public string Email { get; set; }
        [DisplayName("تلفن همراه")]
        [Required(ErrorMessage = "این فیلد اجباری میباشد")]
        public string Mobile { get; set; }

        [DisplayName("آدرس")]
        [Required(ErrorMessage = "این فیلد اجباری میباشد")]
        public string Address { get; set; }
    }
}