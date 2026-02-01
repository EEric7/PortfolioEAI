using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Infrastructure.Persistance.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(200);
            
            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(e => e.Position)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            builder.Property(e => e.StartDate)
                .IsRequired(true);

            builder.Property(e => e.EndDate)
                .IsRequired(false);

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

            builder.OwnsOne(c => c.Url, url =>
            {
                url.Property(e => e.Value)
                    .HasColumnName("Url");
            });
        }
    }
}