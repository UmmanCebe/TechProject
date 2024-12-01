using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechCareer.Models.Entities;

namespace TechCareer.DataAccess.Configurations;
public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.ToTable("Instructors").HasKey(i => i.Id);

        builder.Property(i => i.Id).HasColumnName("Id");
        builder.Property(i => i.Name).HasColumnName("Name").IsRequired();
        builder.Property(i => i.About).HasColumnName("About").IsRequired().HasMaxLength(150);

        builder.HasQueryFilter(i => !i.DeletedDate.HasValue);

        builder.HasMany(i =>i.VideoEducations)
            .WithOne(v => v.Instructor)
            .HasForeignKey(v => v.InstructorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}