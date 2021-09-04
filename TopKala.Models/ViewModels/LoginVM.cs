using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TopKala.Models.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "ایمیل یا شماره موبایل", Prompt = "ایمیل یا شماره موبایل خود را وارد نمایید")]
        [Required(ErrorMessage = "ایمیل یا شماره موبایل نمی تواند خالی باشد")]
        public string UserEmailPhone { get; set; }

        [Display(Name = "رمز عبور", Prompt = "رمز عبور خود را وارد نمایید")]
        [DataType(DataType.Password)]
        [StringLength(60, MinimumLength = 8, ErrorMessage = "طول رمز عبور باید بین 8 تا 60 حروف باشد")]
        [Required(ErrorMessage = "رمز عبور نمی تواند خالی باشد")]
        public string Password { get; set; }

        [DisplayName("مرا به خاطر داشته باش")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}