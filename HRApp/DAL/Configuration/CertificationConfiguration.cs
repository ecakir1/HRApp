using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configuration
{
    public class CertificationConfiguration : IEntityTypeConfiguration<Certification>
    {
        public void Configure(EntityTypeBuilder<Certification> builder)
        {
            builder.ToTable("Certifications");

            builder.HasKey(cert => cert.CertificationId);

            builder.Property(cert => cert.CertificationName)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(cert => cert.CertificationProvider)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(cert => cert.QualificationId)
                   .HasMaxLength(100);

            builder.Property(cert => cert.CertificationDate)
                   .IsRequired();
        }
    }
}
