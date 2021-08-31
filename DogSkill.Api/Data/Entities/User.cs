using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace DogSkill.Api.Data.Entities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid UserId { get; set; } = Guid.NewGuid();
        [Required, StringLength(256)]
        public string UserName { get; set; }
        [Required, StringLength(25)]
        public string Phone { get; set; }
        [Required, StringLength(256)]
        public string Email { get; set; }
        [JsonIgnore]
        public string Salt { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        [Required]
        public bool IsAgree { get; set; }
    }

    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(f => f.UserId);
            builder.HasAlternateKey(f => f.Id);
        }
    }
}
