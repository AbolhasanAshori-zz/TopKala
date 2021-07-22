using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TopKala.Models.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "ایمیل یا شماره موبایل", Prompt = "ایمیل یا شماره موبایل خود را وارد نمایید")]
        [Required(ErrorMessage = "ایمیل یا شماره موبایل نمی تواند خالی باشد")]
        public string Email_Phone { get; set; }

        [Display(Name = "نام کاربری", Prompt = "نام کاربری خود را وارد نمایید")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "طول نام کاربری باید بین 3 تا 60 حروف باشد")]
        [Required(ErrorMessage = "نام کاربری نمی تواند خالی باشد")]
        public string Username { get; set; }

        [Display(Name = "رمز عبور", Prompt = "رمز عبور خود را وارد نمایید")]
        [DataType(DataType.Password)]
        [StringLength(int.MaxValue, MinimumLength = 8, ErrorMessage = "طول رمز عبور باید بیشتر از 8 حرف باشد")]
        [Required(ErrorMessage = "رمز عبور نمی تواند خالی باشد")]
        public string Password { get; set; }

        [Display(Name = "تکرار رمز عبور", Prompt = "رمز عبور خود را تکرار نمایید")]
        [DataType(DataType.Password)]
        [StringLength(int.MaxValue, MinimumLength = 8, ErrorMessage = "طول رمز عبور باید بیشتر از 8 حرف باشد")]
        [Required(ErrorMessage = "رمز عبور نمی تواند خالی باشد")]
        [Compare(nameof(Password), ErrorMessage = "رمز عبور وارد شده تطابق ندارد")]
        public string PasswordRepeat { get; set; }

        [DisplayName("حریم خصوصی و شرایط و قوانین استفاده از سرویس")]
        public bool PrivacyCheck { get; set; }

        public string ReturnUrl { get; set; }
    }
}