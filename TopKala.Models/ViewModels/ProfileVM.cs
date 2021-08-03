using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopKala.Models.ViewModels
{
    public class ProfileVM
    {
        public User User { get; set; }
        public ProfileSideVM ProfileSideVM { get; set; }
    }
}