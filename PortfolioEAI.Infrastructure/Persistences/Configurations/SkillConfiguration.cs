using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioEAI.Domain.Entities;

namespace PortfolioEAI.Infrastructure.Persistance.Configurations
{
    public class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.HasKey(s => s.Id)
                .HasName("IdSkill");

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("varchar(100)")
                .HasColumnName("Name");

            builder.Property(s => s.Level)
                .IsRequired()
                .HasColumnType("int")
                .HasColumnName("Level");

            builder.Property(s => s.Category)
                .IsRequired()
                .HasColumnType("int")
                .HasColumnName("Category");
        }
    }
}