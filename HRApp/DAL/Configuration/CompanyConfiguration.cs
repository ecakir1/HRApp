using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies");

            builder.HasKey(c => c.CompanyId);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Address)
                   .HasMaxLength(200);

            builder.Property(c => c.Email)
                   .HasMaxLength(100);

            builder.Property(c => c.PhoneNumber)
                   .HasMaxLength(20);

            // Relationships
            builder.HasMany(c => c.Employees)
                   .WithOne(e => e.Company)
                   .HasForeignKey(e => e.CompanyId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Holidays)
                   .WithOne(h => h.Company)
                   .HasForeignKey(h => h.CompanyId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Events)
                   .WithOne(e => e.Company)
                   .HasForeignKey(e => e.CompanyId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Expenses)
                   .WithOne(e => e.Company)
                   .HasForeignKey(e => e.CompanyId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Configure the Applicant relationship
            builder.HasOne(c => c.Applicant)
                   .WithMany()
                   .HasForeignKey(c => c.ApplicantId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
