using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Configurations
{
    public class DepartmentSubjectConfigurations : IEntityTypeConfiguration<DepartmentSubject>
    {
        public void Configure(EntityTypeBuilder<DepartmentSubject> builder)
        {
            builder.HasKey(e => new { e.SubjectId, e.DepartmentId });
            builder.HasOne(e => e.Subject)
                   .WithMany(e => e.DepartmentSubjects);
            builder.HasOne(e => e.Department)
                   .WithMany(e => e.DepartmentSubjects);
        }
    }
}
