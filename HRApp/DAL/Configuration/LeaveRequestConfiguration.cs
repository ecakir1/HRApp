using DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DAL.Configuration
{
    public class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
    {
        public void Configure(EntityTypeBuilder<LeaveRequest> builder)
        {
            builder.ToTable("LeaveRequests");

            builder.HasKey(lr => lr.LeaveRequestId);

            builder.Property(lr => lr.StartDate)
                   .IsRequired();

            builder.Property(lr => lr.EndDate)
                   .IsRequired();

            builder.Property(lr => lr.Status)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(lr => lr.Reason)
                   .HasMaxLength(500);

            builder.Property(lr => lr.LeaveType)
                   .IsRequired();

            // Relationships
            builder.HasOne(lr => lr.Employee)
                   .WithMany(e => e.LeaveRequests) 
                   .HasForeignKey(lr => lr.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
