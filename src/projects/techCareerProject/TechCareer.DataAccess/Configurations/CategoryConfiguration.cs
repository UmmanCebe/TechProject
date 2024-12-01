using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechCareer.Models.Entities;

namespace TechCareer.DataAccess.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories").HasKey(category => category.Id);

            builder.Property(c => c.Name).HasColumnName("Name").IsRequired();

            builder.HasQueryFilter(category => !category.DeletedDate.HasValue);

            builder.HasMany(category => category.Events)
                .WithOne(@event => @event.Category)
                .HasForeignKey(@event => @event.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Navigation(category => category.Events).AutoInclude();
        }
    }
}
