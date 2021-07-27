using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DogSkill.Api.Data.Entities
{
    public class UserActivity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        [Required]
        public int Score { get; set; }
        [Required]
        public int LastScore { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public DateTime ModifiedAt { get; set; }

        public virtual User User { get; set; }
    }

    public class UserActivityEntityConfiguration : IEntityTypeConfiguration<UserActivity>
    {
        public void Configure(EntityTypeBuilder<UserActivity> builder)
        {
            builder.HasKey(f => f.Id);
            builder
                .HasOne(f => f.User)
                .WithOne();
        }
    }
}
