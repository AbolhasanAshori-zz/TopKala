using System.ComponentModel.DataAnnotations;

namespace TopKala.Models.ViewModels
{
    public class PasswordVM
    {
        [Display(Name = "رمز عبور قبلی", Prompt = "رمز عبور قبلی خود را وارد نمایید")]
        [DataType(DataType.Password)]
        [StringLength(int.MaxValue, MinimumLength = 8, ErrorMessage = "طول رمز عبور باید بیشتر از 8 حرف باشد")]
        [Required(ErrorMessage = "رمز عبور نمی تواند خالی باشد")]
        public string OldPassword { get; set; }

        [Display(Name = "رمز عبور جدید", Prompt = "رمز عبور جدید خود را وارد نمایید")]
        [DataType(DataType.Password)]
        [StringLength(int.MaxValue, MinimumLength = 8, ErrorMessage = "طول رمز عبور باید بیشتر از 8 حرف باشد")]
        [Required(ErrorMessage = "رمز عبور نمی تواند خالی باشد")]
        public string NewPassword { get; set; }

        [Display(Name = "تکرار رمز عبور جدید", Prompt = "رمز عبور جدید خود را مجددا وارد نمایید")]
        [DataType(DataType.Password)]
        [StringLength(int.MaxValue, MinimumLength = 8, ErrorMessage = "طول رمز عبور باید بیشتر از 8 حرف باشد")]
        [Required(ErrorMessage = "رمز عبور نمی تواند خالی باشد")]
        [Compare(nameof(NewPassword), ErrorMessage = "رمز عبور وارد شده تطابق ندارد")]
        public string NewPasswordRepeat { get; set; }
    }
}