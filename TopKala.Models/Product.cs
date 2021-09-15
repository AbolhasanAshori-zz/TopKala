using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TopKala.Models
{
    public class Product : BaseEntity<int>
    {
        public string Title { get; set; }
        public string EngTitle { get; set; }
        public string Description { get; set; }

        #region Relation Properties
        public int? BrandId { get; set; }
        public Brand Brand { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public IEnumerable<ProductImage> Images { get; set; }
        public IEnumerable<Color> Colors { get; set; }
        public IEnumerable<Comment> Comments { get; set; }

        public IEnumerable<ProductInfo> ProductTopInfo { get; set; }
        public IEnumerable<ProductInfoGroup> ProductInfoGroups { get; set; }
        public IEnumerable<ProductSeller> ProductSellers { get; set; }
        #endregion
    }

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasMany(p => p.ProductSellers)
                   .WithOne(ps => ps.Product)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.ProductInfoGroups)
                   .WithOne(pi => pi.Product)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.Comments)
                   .WithOne(c => c.Product)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.Colors)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(p => p.Images)
                   .WithOne(i => i.Product)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Category)
                   .WithOne()
                   .HasForeignKey<Product>(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.Brand)
                   .WithOne()
                   .HasForeignKey<Product>(p => p.BrandId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}