using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TopKala.Models
{
    public class ProductInfoGroup : BaseEntity<int>
    {
        public int InfoGroupId { get; set; }
        public InfoGroup InfoGroup { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public IEnumerable<ProductInfo> ProductInfos { get; set; }
    }

    public class ProductInfoGroupConfiguration : IEntityTypeConfiguration<ProductInfoGroup>
    {
        public void Configure(EntityTypeBuilder<ProductInfoGroup> builder)
        {
            builder.HasOne(pig => pig.InfoGroup)
                   .WithOne()
                   .HasForeignKey<ProductInfoGroup>(pig => pig.InfoGroupId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(pig => pig.Product)
                   .WithMany(p => p.ProductInfoGroups)
                   .HasForeignKey(pig => pig.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(pig => pig.ProductInfos)
                   .WithOne(pi => pi.ProductInfoGroup)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}