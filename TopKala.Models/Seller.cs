using System.Collections.Generic;

namespace TopKala.Models
{
    public class Seller : BaseEntity<int>
    {
        public string Name { get; set; }

        public IEnumerable<ProductSeller> ProductSellers { get; set; }
    }
}