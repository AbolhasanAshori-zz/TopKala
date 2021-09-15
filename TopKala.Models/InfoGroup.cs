using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TopKala.Models
{
    public class InfoGroup : BaseEntity<int>
    {
        public string Value { get; set; }
    }

    public class InfoGroupConfiguration : IEntityTypeConfiguration<InfoGroup>
    {
        public void Configure(EntityTypeBuilder<InfoGroup> builder)
        {
            builder.HasIndex(c => c.Value)
                   .IsUnique();
        }
    }
}