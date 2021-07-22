using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TopKala.Models
{
    public class User : BaseEntity<int>
    {
        public string Username { get; set; }
        public string Email{ get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdNumber { get; set; }
        public long CardNumber { get; set; }
        public bool isActive { get; set; }
        public bool IsEmailConfirmed { get; set; }

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