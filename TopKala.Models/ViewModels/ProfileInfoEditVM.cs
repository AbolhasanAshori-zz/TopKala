using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TopKala.Utility.StaticData;

namespace TopKala.Models.ViewModels
{
    public class ProfileInfoEditVM
    {
        [Display(Name = "نام", Prompt = "نام خود را وارد کنید")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی", Prompt = "نام خانوادگی خود را وارد کنید")]
        public string LastName { get; set; }

        [Display(Name = "شماره موبایل", Prompt = "شماره موبایل خود را وارد کنید")]
        [RegularExpression(SD_Regex.NativePhoneNumber, ErrorMessage = "لطفا شماره موبایل خود را به درستی وارد نمایید")]
        public string PhoneNumber { get; set; }

        [Display(Name = "کد ملی", Prompt = "کد ملی خود را وارد کنید")]
        public string IdNumber { get; set; }

        [Display(Name = "شماره کارت", Prompt = "شماره کارت خود را وارد کنید")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "اعداد شماره کارت وارد شده باید ۱۶ رقم باشد")]
        public string CardNumber { get; set; }
        
        [Display(Name = "آدرس ایمیل", Prompt = "آدرس ایمیل خود را وارد کنید")]
        [EmailAddress(ErrorMessage = "لطفا آدرس ایمیل خود را به درستی وارد نمایید")]
        [Required(ErrorMessage = "ایمیل نمی تواند خالی باشد")]
        public string Email { get; set; }

        [DisplayName("تبعه خارجی فاقد کد ملی هستم")]
        public bool ForeignUser { get; set; }

        [DisplayName("اشتراک در خبرنامه تاپ کالا")]
        public bool Subscript { get; set; }
    }
}