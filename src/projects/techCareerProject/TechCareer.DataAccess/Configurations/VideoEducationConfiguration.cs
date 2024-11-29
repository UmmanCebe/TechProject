using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechCareer.Models.Entities;

namespace TechCareer.DataAccess.Configurations;

public class VideoEducationConfiguration : IEntityTypeConfiguration<VideoEducation>
{
    public void Configure(EntityTypeBuilder<VideoEducation> builder)
    {
        builder.ToTable("VideoEducations").HasKey(u => u.Id);

        builder.Property(u => u.Id).HasColumnName("Id").IsRequired();
        builder.Property(u => u.Title).HasColumnName("Title").IsRequired();
        builder.Property(u => u.Description).HasColumnName("Description").IsRequired();
        builder.Property(u => u.TotalHour).HasColumnName("TotalHour").IsRequired();
        builder.Property(u => u.IsCertified).HasColumnName("IsCertified").HasDefaultValue(true);
        builder.Property(u => u.Level).HasColumnName("Level").IsRequired();
        builder.Property(u => u.ImageUrl).HasColumnName("ImageUrl").IsRequired();
        builder.Property(u => u.InstructorId).HasColumnName("InstructorId").IsRequired();
        builder.Property(u => u.ProgrammingLanguage).HasColumnName("ProgrammingLanguage").IsRequired();

        builder.Property(u => u.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(u => u.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(u => u.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(u => !u.DeletedDate.HasValue);

        builder.HasOne(u => u.Instructor)
            .WithMany(instructor => instructor.VideoEducations)
            .HasForeignKey(videoEducation => videoEducation.InstructorId)
            .OnDelete(DeleteBehavior.NoAction);

    }

}