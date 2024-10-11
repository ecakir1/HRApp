using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.UserName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.Email) 
                   .IsRequired() 
                   .HasMaxLength(256); 

            builder.HasIndex(e => e.Email) 
                   .IsUnique();

            builder.Property(e => e.Created_At)
                   .IsRequired();

            builder.Property(e => e.Updated_At)
                   .IsRequired();

            // Relationships
            builder.HasMany(e => e.LeaveRequests)
                   .WithOne(l => l.Employee)
                   .HasForeignKey(l => l.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.ExpenseRequests)
                   .WithOne(er => er.Employee)
                   .HasForeignKey(er => er.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(e => e.EmployeeDetail)
                   .WithOne(ed => ed.Employee)
                   .HasForeignKey<EmployeeDetail>(ed => ed.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
