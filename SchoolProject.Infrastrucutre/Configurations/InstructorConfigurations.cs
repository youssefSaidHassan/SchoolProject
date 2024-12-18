using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Configurations
{
    public class InstructorConfigurations : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.HasOne(e => e.Supervisor).WithMany(e => e.Instructors).HasForeignKey(e => e.SupervisorId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
