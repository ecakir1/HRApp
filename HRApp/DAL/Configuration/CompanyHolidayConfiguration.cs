using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class CompanyHolidayConfiguration : IEntityTypeConfiguration<CompanyHoliday>
    {
        public void Configure(EntityTypeBuilder<CompanyHoliday> builder)
        {
            builder.ToTable("CompanyHolidays");

            builder.HasKey(ch => ch.CompanyHolidayId);

            builder.Property(ch => ch.HolidayName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(ch => ch.Date)
                   .IsRequired();

            // Relationships
            builder.HasOne(ch => ch.Company)
                   .WithMany(c => c.Holidays)
                   .HasForeignKey(ch => ch.CompanyId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
