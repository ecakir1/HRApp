using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class ExperienceConfiguration : IEntityTypeConfiguration<Experience>
    {
        public void Configure(EntityTypeBuilder<Experience> builder)
        {
            builder.ToTable("Experiences");

            builder.HasKey(exp => exp.ExperienceId);

            builder.Property(exp => exp.CompanyName)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(exp => exp.Position)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(exp => exp.Description)
                   .HasMaxLength(500);

            builder.Property(exp => exp.StartDate)
                   .IsRequired();

            builder.Property(exp => exp.EndDate)
                   .IsRequired(false);
        }
    }
}
