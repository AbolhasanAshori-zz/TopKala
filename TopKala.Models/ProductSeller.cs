using System;
using System.ComponentModel.DataAnnotations;

namespace TopKala.Models
{
    public class ProductSeller : BaseEntity<int>
    {

        [Range(0, long.MaxValue)]
        public long Price { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Warrenty { get; set; }

        [Range(0, int.MaxValue)]
        [Required]
        public int Quantity { get; set; }

        [Range(0, 100)]
        public int? PriceOff { get; set; }

        #region Relation Properties
        public int ProductColorId { get; set; }
        public ProductColor ProductColor { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int SellerId { get; set; }
        public Seller Seller { get; set; }
        #endregion
    }
}