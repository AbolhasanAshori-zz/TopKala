using System.Collections.Generic;

namespace TopKala.Models.ViewModels
{
    public class SearchVM
    {
        public SearchVM(string searchString)
        {
            SearchString = searchString;
        }

        public string SearchString { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public int MyProperty { get; set; }
    }
}