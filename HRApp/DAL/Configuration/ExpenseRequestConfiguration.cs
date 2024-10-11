using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class ExpenseRequestConfiguration : IEntityTypeConfiguration<ExpenseRequest>
    {
        public void Configure(EntityTypeBuilder<ExpenseRequest> builder)
        {
            builder.ToTable("ExpenseRequests");

            builder.HasKey(er => er.ExpenseRequestId);

            builder.Property(er => er.Amount)
                   .HasPrecision(18,2)
                   .IsRequired();

            builder.Property(er => er.Date)
                   .IsRequired();

            builder.Property(er => er.Description)
                   .HasMaxLength(500);

            builder.Property(er => er.Status)
                   .IsRequired()
                   .HasMaxLength(20);

            // Relationships
            builder.HasOne(er => er.Employee)
                   .WithMany(e => e.ExpenseRequests)
                   .HasForeignKey(er => er.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
