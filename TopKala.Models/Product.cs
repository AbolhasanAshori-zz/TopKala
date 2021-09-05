using System.Collections.Generic;

namespace TopKala.Models
{
    public class Product : BaseEntity<int>
    {
        public string Title { get; set; }
        public string EngTitle { get; set; }
        public string Info { get; set; }
        public string About { get; set; }
        public string Description { get; set; }

        #region Relation Properties
        public int? BrandId { get; set; }
        public Brand Brand { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public IEnumerable<ProductImage> Images { get; set; }
        public IEnumerable<ProductColor> Colors { get; set; }
        public IEnumerable<Comment> Comments { get; set; }

        public IEnumerable<ProductInfo> ProductInfos { get; set; }
        public IEnumerable<ProductSeller> ProductSellers { get; set; }
        #endregion
    }
}