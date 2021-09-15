using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TopKala.Models
{
    public class Info : BaseEntity<int>
    {
        public string Value { get; set; }
    }

    public class InfoConfiguration : IEntityTypeConfiguration<Info>
    {
        public void Configure(EntityTypeBuilder<Info> builder)
        {
            builder.HasIndex(i => i.Value)
                   .IsUnique();
        }
    }
}