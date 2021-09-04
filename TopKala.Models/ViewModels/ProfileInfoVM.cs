using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopKala.Models.ViewModels
{
    public class ProfileInfoVM
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string IdNumber { get; set; }
        public bool NewsletterSubscript { get; set; }
        public string CardNumber { get; set; }
        public int ColSize { get; set; }
    }
}