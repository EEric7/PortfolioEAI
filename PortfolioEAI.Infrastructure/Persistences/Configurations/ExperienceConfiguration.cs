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

            builder.Property(e => e.Position)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)")
                .HasColumnName("Position");

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(1000)
                .HasColumnType("varchar(1000)")
                .HasColumnName("Description");

            builder.OwnsOne(a => a.ImageUrl, imageUrl =>
            {
                imageUrl.Property(p => p.FileName)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasMaxLength(100)
                    .HasColumnName("FileName")
                    .IsRequired(true);

                imageUrl.Property(p => p.Title)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasMaxLength(100)
                    .HasColumnName("FileTitle")
                    .IsRequired(true);
                
                imageUrl.Property(p => p.Url)
                    .IsRequired()
                    .HasColumnType("varchar(500)")
                    .HasMaxLength(500)
                    .HasColumnName("FileUrl")
                    .IsRequired(true);

                imageUrl.Property(p => p.CreatedAt)
                    .IsRequired()
                    .HasColumnType("date")
                    .HasColumnName("FileCreatedAt");

                imageUrl.HasIndex(p => p.Url)
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