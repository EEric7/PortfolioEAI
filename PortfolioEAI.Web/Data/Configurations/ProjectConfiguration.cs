using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioEAI.Web.Domain.Entities;

namespace PortfolioEAI.Web.Data.Configurations
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

            builder.Property(c => c.ImageUrl)
                .HasColumnName("ImageUrl")
                .HasMaxLength(500);

            builder.OwnsOne(c => c.Url, email =>
            {
                email.Property(e => e.Value)
                    .HasColumnName("Url");
            });
        }
    }
}