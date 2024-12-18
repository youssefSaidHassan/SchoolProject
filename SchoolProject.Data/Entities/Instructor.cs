using SchoolProject.Data.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class Instructor : LocalizableEntity
    {
        public Instructor()
        {
            Instructors = new HashSet<Instructor>();
            InstructorSubjects = new HashSet<InstructorSubject>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? Address { get; set; }
        public string? Position { get; set; }
        public int? SupervisorId { get; set; }
        public decimal? Salary { get; set; }
        public int? DepartmentId { get; set; }


        // Department Relation
        [ForeignKey(nameof(DepartmentId))]
        [InverseProperty("Instructors")]
        public Department? Department { get; set; }
        [InverseProperty("Instructor")]
        public Department DepartmentManger { get; set; }

        // Instructor Relation
        [ForeignKey(nameof(SupervisorId))]
        [InverseProperty(nameof(Instructors))]

        public Instructor Supervisor { get; set; }
        [InverseProperty(nameof(Supervisor))]
        public virtual ICollection<Instructor> Instructors { get; set; }

        // Subject Relation
        [InverseProperty("Instructor")]
        public virtual ICollection<InstructorSubject> InstructorSubjects { get; set; }

    }
}
