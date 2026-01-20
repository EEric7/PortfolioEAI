using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.ValueObjects;

namespace PortfolioEAI.Infrastructure.Persistance.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(a => a.Id)
                .HasName("IdUser");
            
            builder.Property(a => a.Firstname)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("varchar(50)")
                .HasColumnName("FirstName");

            builder.Property(a => a.Lastname)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("varchar(50)")
                .HasColumnName("LastName");

            builder.Property(a => a.DisplayName)
                .IsRequired(false)
                .HasMaxLength(100)
                .HasColumnType("varchar(100)")
                .HasColumnName("DisplayName");
            
            builder.Property(a => a.Description)
                .HasColumnType("text")
                .IsRequired(false);

            builder.OwnsOne(a => a.Password, password =>
            {
                password.Property(p => p.HashValue)
                    .HasColumnName("Password")
                    .HasColumnType("varchar(100)")
                    .HasMaxLength(100)
                    .IsRequired(true);
                
                password.HasIndex(p => p.HashValue)
                    .IsUnique();
            });

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

            builder.OwnsOne(a => a.ProfilePhoto, profilePhoto =>
            {
                profilePhoto.Property(p => p.FileName)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasMaxLength(100)
                    .HasColumnName("FileName")
                    .IsRequired(true);

                profilePhoto.Property(p => p.Title)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasMaxLength(100)
                    .HasColumnName("FileTitle")
                    .IsRequired(true);
                
                profilePhoto.Property(p => p.Url)
                    .IsRequired()
                    .HasColumnType("varchar(500)")
                    .HasMaxLength(500)
                    .HasColumnName("FileUrl")
                    .IsRequired(true);

                profilePhoto.Property(p => p.CreatedAt)
                    .IsRequired()
                    .HasColumnType("date")
                    .HasColumnName("FileCreatedAt");

                profilePhoto.HasIndex(p => p.Url)
                    .IsUnique();
            });
            
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

            builder.Navigation("_roles")
                .HasField("_roles")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            
            builder.Navigation("_skills")
                .HasField("_skills")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Navigation("_experiences")
                .HasField("_experiences")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            
            builder.HasMany<Role>("_roles")
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey("IdUser")
                .IsRequired(true);

            builder.HasMany<Skill>("_skills")
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey("IdUser")
                .IsRequired(true);
            
            builder.HasMany<Experience>("_experiences")
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey("IdUser")
                .IsRequired(true);
        }
    }
}
