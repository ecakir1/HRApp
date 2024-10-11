using DAL.Models;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {

            builder.ToTable("Expenses");


            builder.HasKey(e => e.ExpenseId);


            builder.Property(e => e.Amount)
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.Property(e => e.Date)
                   .IsRequired();

            builder.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(100); 

            builder.Property(e => e.Description)
                   .HasMaxLength(500); 

            builder.HasOne(e => e.Employee) 
                   .WithMany(emp => emp.Expense ) 
                   .HasForeignKey(e => e.EmployeeId) 
                   .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(e => e.Company)
                   .WithMany(c => c.Expenses)
                   .HasForeignKey(e => e.CompanyId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
