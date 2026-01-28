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
                .HasMaxLength(200);
            
            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(e => e.Position)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            builder.Property(e => e.StartDate)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(e => e.EndDate)
                .HasColumnType("date");

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

            builder.OwnsOne(c => c.Url, email =>
            {
                email.Property(e => e.Value)
                    .HasColumnName("Url");
            });
        }
    }
}