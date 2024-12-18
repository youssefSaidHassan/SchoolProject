using SchoolProject.Data.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class Department : LocalizableEntity
    {
        public Department()
        {
            Students = new HashSet<Student>();
            DepartmentSubjects = new HashSet<DepartmentSubject>();
            Instructors = new HashSet<Instructor>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50)]
        public string? NameAr { get; set; }
        [StringLength(50)]

        public string? NameEn { get; set; }

        public int? InstructorManager { get; set; }


        [InverseProperty(nameof(Student.Department))]
        public virtual ICollection<Student> Students { get; set; }
        [InverseProperty(nameof(DepartmentSubject.Department))]
        public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; }

        [InverseProperty("Department")]
        public virtual ICollection<Instructor> Instructors { get; set; }

        [ForeignKey(nameof(InstructorManager))]
        [InverseProperty("DepartmentManger")]
        public virtual Instructor Instructor { get; set; }


    }
}
