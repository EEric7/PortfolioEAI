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
                    .IsRequired(true);
                
                email.HasIndex(e => e.Value)
                    .IsUnique();
            });

            builder.Property(a => a.Description)
                   .HasColumnType("text")
                   .IsRequired(false);
            
            builder.OwnsOne(a => a.Address, address =>
            {
                address.Property(a => a.Street)
                    .HasColumnName("Street")
                    .HasColumnType("varchar(200)")
                    .HasMaxLength(200)
                    .IsRequired(false);

                address.Property(a => a.City)
                    .HasColumnName("City")
                    .HasColumnType("varchar(100)")
                    .HasMaxLength(100)
                    .IsRequired(false);

                address.Property(a => a.PostalCode)
                    .HasColumnName("PostalCode")
                    .HasColumnType("varchar(20)")
                    .HasMaxLength(20)
                    .IsRequired(false);

                address.Property(a => a.Country)
                    .HasColumnName("Country")
                    .HasColumnType("varchar(100)")
                    .HasMaxLength(100)
                    .IsRequired(false);
            });

            builder.HasMany(a => a.Skills)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey("IdAdminUser")
                .IsRequired(false);

            builder.HasMany(a => a.Experiences)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey("IdAdminUser")
                .IsRequired(false);
        }
    }
}
