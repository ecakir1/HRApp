using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");

            builder.HasKey(e => e.EventId);

            builder.Property(e => e.EventName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.EventDate)
                   .IsRequired();

            // Relationships
            builder.HasOne(e => e.Company)
                   .WithMany(c => c.Events)
                   .HasForeignKey(e => e.CompanyId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
