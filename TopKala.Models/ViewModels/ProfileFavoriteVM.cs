using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopKala.Models.ViewModels
{
    public class ProfileFavoriteVM
    {
        public IEnumerable<Product> Products { get; set; }
        public int ColSize { get; set; }
    }
}