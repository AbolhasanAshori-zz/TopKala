using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TopKala.Models
{
    public class User : BaseEntity<int>
    {
        public string Image { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdNumber { get; set; }
        public string CardNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsImageUploaded { get; set; }
        public bool IsNewsletterSubscripted { get; set; }
        public bool IsUserForeign { get; set; }
        public bool IsPhoneNumberConfirmed { get; set; }

        #region Relation Properties
        public UserRole Role { get; set; }
        #endregion
    }

    #region Fluent Api Configs
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(user => user.Username).IsRequired();

            builder.Property(user => user.PasswordHash).IsRequired();
        }
    }
    #endregion
}