using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Infrastructure.Persistance.Configurations
{
    public class ExperienceConfiguration : IEntityTypeConfiguration<Experience>
    {
        public void Configure(EntityTypeBuilder<Experience> builder)
        {
            builder.HasKey(e => e.Id)
                .HasName("IdExperience");

            builder.Property(e => e.Company)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("varchar(50)")
                .HasColumnName("Company");

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(1000)
                .HasColumnType("varchar(1000)")
                .HasColumnName("Description");

            builder.OwnsOne(a => a.ImageUrl, imageUrl =>
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

            builder.Navigation("_projects")
                .HasField("_projects")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany<Project>("_projects")
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey("IdExperience")
                .IsRequired(true);
        }
    }
}