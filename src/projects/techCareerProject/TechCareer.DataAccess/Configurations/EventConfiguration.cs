using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechCareer.Models.Entities;

namespace TechCareer.DataAccess.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
        
            builder.ToTable("Events").HasKey(u => u.Id);

       
            builder.Property(u => u.Id).HasColumnName("Id").IsRequired();
            builder.Property(u => u.Title).HasColumnName("Title").IsRequired().HasMaxLength(100);
            builder.Property(u => u.Description).HasColumnName("Description").IsRequired().HasMaxLength(500);
            builder.Property(u => u.ImageUrl).HasColumnName("ImageUrl").IsRequired();
            builder.Property(u => u.StartDate).HasColumnName("StartDate").IsRequired();
            builder.Property(u => u.EndDate).HasColumnName("EndDate").IsRequired();
            builder.Property(u => u.ApplicationDeadline).HasColumnName("ApplicationDeadline").IsRequired();
            builder.Property(u => u.ParticipationText).HasColumnName("ParticipationText").HasMaxLength(200).IsRequired();
            builder.Property(u => u.CategoryId).HasColumnName("CategoryId").IsRequired();

            builder.HasQueryFilter(u => !u.DeletedDate.HasValue);

            builder.HasOne(u => u.CategoryName)
                .WithMany(c => c.Events)
                .HasForeignKey(u => u.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
