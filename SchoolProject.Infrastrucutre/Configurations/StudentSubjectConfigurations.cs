using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Configurations
{
    public class StudentSubjectConfigurations : IEntityTypeConfiguration<StudentSubject>
    {
        public void Configure(EntityTypeBuilder<StudentSubject> builder)
        {
            builder.HasKey(e => new { e.StudentId, e.SubjectId });

            builder.HasOne(e => e.Student)
                   .WithMany(e => e.StudentSubjects);
            builder.HasOne(e => e.Subject)
                   .WithMany(e => e.StudentSubjects);
        }
    }
}
