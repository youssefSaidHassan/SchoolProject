using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Configurations
{
    public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {

            builder.HasKey(e => e.Id);
            builder.Property(e => e.NameAr).HasMaxLength(30);
            builder.Property(e => e.NameEn).HasMaxLength(30);

            builder.HasMany(e => e.Students)
                        .WithOne(e => e.Department)
                        .HasForeignKey(e => e.DepartmentID)
                        .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(e => e.Instructors)
                       .WithOne(e => e.Department)
                       .HasForeignKey(e => e.DepartmentId);

            builder.HasOne(e => e.Instructor)
                .WithOne(x => x.DepartmentManger)
                .HasForeignKey<Department>(e => e.InstructorManager)
                .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
