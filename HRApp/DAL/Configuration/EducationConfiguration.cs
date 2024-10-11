using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class EducationConfiguration : IEntityTypeConfiguration<Education>
    {
        public void Configure(EntityTypeBuilder<Education> builder)
        {
            builder.ToTable("Educations");

            builder.HasKey(edu => edu.EducationId);

            builder.Property(edu => edu.School)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(edu => edu.Degree)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(edu => edu.FieldOfStudy)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(edu => edu.StartDate)
                   .IsRequired();

            builder.Property(edu => edu.EndDate)
                   .IsRequired();
        }
    }
}
