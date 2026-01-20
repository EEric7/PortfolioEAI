using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Infrastructure.Persistance.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(p => p.Id)
                .HasName("IdProject");

            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("Title");
            
            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(1000)
                .HasColumnName("Description");

            builder.Property(e => e.StartDate)
                .IsRequired()
                .HasColumnType("date")
                .HasColumnName("StartDate");

            builder.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("EndDate");

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

            builder.OwnsOne(c => c.Url, email =>
            {
                email.Property(e => e.Value)
                    .HasColumnName("Url");
            });
        }
    }
}