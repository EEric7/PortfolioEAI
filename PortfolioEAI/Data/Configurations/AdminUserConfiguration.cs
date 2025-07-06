using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Data.Configurations
{
    public class AdminUserConfiguration : IEntityTypeConfiguration<AdminUser>
    {
        public void Configure(EntityTypeBuilder<AdminUser> builder)
        {
            builder.HasKey(a => a.Id)
                .HasName("IdAdminUser");

            builder.Property(a => a.Username)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("varchar(50)")
                .HasColumnName("UserName");

            builder.Property(a => a.Password)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)")
                .HasColumnName("Password");

            builder.OwnsOne(a => a.Email, email =>
            {
                email.Property(e => e.Value)
                    .HasColumnName("Email")
                    .HasColumnType("varchar(100)")
                    .HasMaxLength(100)
                    .IsRequired();
            });

            builder.HasMany(a => a.Skills)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey("AdminUserId")
                .IsRequired(false);
        }
    }
}