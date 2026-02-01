using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Infrastructure.Persistance.Configurations
{
    public class ExperienceConfiguration : IEntityTypeConfiguration<Experience>
    {
        public void Configure(EntityTypeBuilder<Experience> builder)
        {
            builder.HasKey(e => e.Id);

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

            builder.OwnsOne(a => a.Image, img =>
            {
                img.Property(p => p.Name)
                    .IsRequired(true)
                    .HasMaxLength(255)
                    .HasColumnType("varchar(255)");

                img.Property(p => p.Type)
                    .IsRequired(true)
                    .HasColumnType("varchar(127)")
                    .HasMaxLength(127);
                
                img.Property(p => p.Size)
                    .IsRequired(true)
                    .HasColumnType("bigint")
                    .HasMaxLength(500);

                img.Property(p => p.Content)
                    .IsRequired(true)
                    .HasColumnType("LONGBLOB");
                    
                img.HasIndex(p => p.UploadedAt)
                    .IsUnique();
            });

            builder.Navigation(e => e.Projects)
                .HasField("_projects")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(e => e.Projects)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey("IdExperience")
                .IsRequired(true);
        }
    }
}