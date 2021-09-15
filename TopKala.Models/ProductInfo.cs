using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TopKala.Models
{
    public class ProductInfo : BaseEntity<int>
    {
        public string Value { get; set; }

        #region Relation Properties
        public int? ProductInfoGroupId { get; set; }
        public ProductInfoGroup ProductInfoGroup { get; set; }

        public int InfoId { get; set; }
        public Info Info { get; set; }
        #endregion
    }

    public class ProductInfoConfiguration : IEntityTypeConfiguration<ProductInfo>
    {
        public void Configure(EntityTypeBuilder<ProductInfo> builder)
        {
            builder.HasOne(pi => pi.ProductInfoGroup)
                   .WithMany(pig => pig.ProductInfos)
                   .HasForeignKey(pi => pi.ProductInfoGroupId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(pi => pi.Info)
                   .WithMany()
                   .HasForeignKey(pi => pi.InfoId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}