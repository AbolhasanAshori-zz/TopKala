using System.ComponentModel.DataAnnotations;

namespace TopKala.Models.ViewModels
{
    public class UsernameVM
    {
        [Display(Name = "نام کاربری قبلی")]
        public string OldUsername { get; set; }

        [Display(Name = "نام کاربری جدید", Prompt = "نام کاربری جدید خود را وارد نمایید")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "طول نام کاربری باید بین 3 تا 60 حروف باشد")]
        [Required(ErrorMessage = "نام کاربری نمی تواند خالی باشد")]
        public string NewUsername { get; set; }

        public string ReturnUrl { get; set; }
    }
}