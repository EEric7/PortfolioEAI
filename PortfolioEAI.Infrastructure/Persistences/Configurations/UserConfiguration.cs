using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioEAI.Domain.Entities;

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

            builder.OwnsOne(u => u.Role, role =>
            {
                role.Property(p => p.Name)
                    .HasColumnName("Role")
                    .HasColumnType("varchar(30)")
                    .HasMaxLength(30)
                    .IsRequired(true);
                
                role.HasIndex(p => p.Id)
                    .IsUnique();
            });

           builder.OwnsOne(a => a.ProfilePhoto, imageUrl =>
            {
                imageUrl.Property(p => p.Name)
                    .IsRequired(true)
                    .HasMaxLength(255)
                    .HasColumnType("varchar(255)");

                imageUrl.Property(p => p.Type)
                    .IsRequired(true)
                    .HasColumnType("varchar(127)")
                    .HasMaxLength(127);
                
                imageUrl.Property(p => p.Size)
                    .IsRequired(true)
                    .HasColumnType("bigint")
                    .HasMaxLength(500);

                imageUrl.Property(p => p.Content)
                    .IsRequired(true)
                    .HasColumnType("LONGBLOB");
                    

                imageUrl.HasIndex(p => p.UploadedAt)
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
            
            builder.Navigation("_skills")
                .HasField("_skills")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Navigation("_experiences")
                .HasField("_experiences")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

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
