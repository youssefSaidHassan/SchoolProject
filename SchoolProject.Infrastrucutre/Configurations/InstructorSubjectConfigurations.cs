using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Configurations
{
    public class InstructorSubjectConfigurations : IEntityTypeConfiguration<InstructorSubject>
    {
        public void Configure(EntityTypeBuilder<InstructorSubject> builder)
        {
            builder.HasKey(e => new { e.SubjectId, e.InstructorId });

            builder.HasOne(e => e.Instructor)
                   .WithMany(e => e.InstructorSubjects);
            builder.HasOne(e => e.Subject)
                   .WithMany(e => e.InstructorSubjects);
        }
    }
}
