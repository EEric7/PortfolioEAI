using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Data.Configurations
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

            builder.Property(e => e.StartDate)
                .IsRequired()
                .HasColumnType("date")
                .HasColumnName("StartDate");

            builder.Property(e => e.EndDate)
                .IsRequired()
                .HasColumnType("date")
                .HasColumnName("EndDate");

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(1000)
                .HasColumnType("varchar(1000)")
                .HasColumnName("Description");

            builder.Property(e => e.ImageUrl)
                .HasColumnName("ImageUrl")
                .HasMaxLength(500)
                .HasColumnType("varchar(500)");

            builder.HasMany(e => e.Projects)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey("IdExperience")
                .IsRequired(false);
        }
    }
}