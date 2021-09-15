using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
        public int PriceOff { get; set; } = 0;

        #region Relation Properties
        public int ColorId { get; set; }
        public Color Color { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int SellerId { get; set; }
        public Seller Seller { get; set; }
        #endregion
    }

    public class ProductSellerConfiguration : IEntityTypeConfiguration<ProductSeller>
    {
        public void Configure(EntityTypeBuilder<ProductSeller> builder)
        {
            builder.HasOne(ps => ps.Product)
                   .WithMany(p => p.ProductSellers)
                   .HasForeignKey(ps => ps.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(ps => ps.Color)
                   .WithOne()
                   .HasForeignKey<ProductSeller>(ps => ps.ColorId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(ps => ps.Seller)
                   .WithOne()
                   .HasForeignKey<ProductSeller>(ps => ps.SellerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}