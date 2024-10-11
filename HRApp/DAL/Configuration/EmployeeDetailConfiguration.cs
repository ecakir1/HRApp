using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class EmployeeDetailConfiguration : IEntityTypeConfiguration<EmployeeDetail>
    {
        public void Configure(EntityTypeBuilder<EmployeeDetail> builder)
        {
            builder.ToTable("EmployeeDetails");

            builder.HasKey(ed => ed.EmployeeDetailId);

            builder.Property(ed => ed.Address)
                   .HasMaxLength(200);

            builder.Property(ed => ed.City)
                   .HasMaxLength(100);

            builder.Property(ed => ed.Position)
                   .HasMaxLength(50);

            builder.Property(ed => ed.Department)
                   .HasMaxLength(50);

            builder.Property(ed => ed.RemainingLeaveDays)
                   .IsRequired()
                   .HasDefaultValue(0);

            builder.HasOne(ed => ed.Employee)
                   .WithOne(e => e.EmployeeDetail)
                   .HasForeignKey<EmployeeDetail>(ed => ed.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationships
            builder.HasMany(ed => ed.Educations)
                   .WithOne(e => e.EmployeeDetail)
                   .HasForeignKey(e => e.EmployeeDetailId);

            builder.HasMany(ed => ed.Certifications)
                   .WithOne(c => c.EmployeeDetail)
                   .HasForeignKey(c => c.EmployeeDetailId);

            builder.HasMany(ed => ed.Experiences)
                   .WithOne(exp => exp.EmployeeDetail)
                   .HasForeignKey(exp => exp.EmployeeDetailId);
        }
    }
}
