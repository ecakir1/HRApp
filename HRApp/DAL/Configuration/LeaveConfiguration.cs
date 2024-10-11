using DAL.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Data.Models;

namespace DAL.Configuration
{
    public class LeaveConfiguration : IEntityTypeConfiguration<Leave>
    {
        public void Configure(EntityTypeBuilder<Leave> builder)
        {
            builder.ToTable("Leaves");

            builder.HasKey(l => l.LeaveId);

            builder.Property(l => l.StartDate)
                   .IsRequired();

            builder.Property(l => l.EndDate)
                   .IsRequired();

            builder.Property(l => l.Reason)
                   .HasMaxLength(500);

            builder.Property(l => l.LeaveType)
                   .IsRequired();

            // Relationships
            builder.HasOne(l => l.Employee)
                   .WithMany(e => e.Leaves) 
                   .HasForeignKey(l => l.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
