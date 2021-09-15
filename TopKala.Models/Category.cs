using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TopKala.Models
{
    public class Category : TreeEntity<Category>
    {
        public string Value { get; set; }
    }

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasMany(c => c.Children)
                   .WithOne(c => c.Parent)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Parent)
                   .WithMany(c => c.Children)
                   .HasForeignKey(c => c.ParentId)
                   .OnDelete(DeleteBehavior.Restrict);
                   
            builder.HasIndex(c => c.Value)
                   .IsUnique();
        }
    }
}