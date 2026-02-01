using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioEAI.Domain.Entities;
using PortfolioEAI.Domain.Enums;

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
                .HasColumnType("varchar(50)")
                .HasColumnName("Name");

            builder.Property(s => s.Level)
                .IsRequired()
                .HasColumnType("int")
                .HasColumnName("Level")
                .HasConversion(
                    level => level.Id,
                    id => SkillLevelFromId(id));

            builder.Property(s => s.Category)
                .IsRequired()
                .HasColumnType("int")
                .HasColumnName("Category")
                .HasConversion(
                    category => category.Id,
                    id => SkillCategoryFromId(id));
        }

        private static SkillLevel SkillLevelFromId(int id) => id switch
        {
            25 => SkillLevel.Beginner,
            50 => SkillLevel.Intermediate,
            75 => SkillLevel.Advanced,
            100 => SkillLevel.Expert,
            _ => SkillLevel.None
        };

        private static SkillCategory SkillCategoryFromId(int id) => id switch
        {
            1 => SkillCategory.Frontend,
            2 => SkillCategory.Backend,
            3 => SkillCategory.Fullstack,
            4 => SkillCategory.DevOps,
            5 => SkillCategory.Framework,
            6 => SkillCategory.Languages,
            7 => SkillCategory.Mobile,
            8 => SkillCategory.Design,
            _ => SkillCategory.None
        };
    }
}