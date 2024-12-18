using SchoolProject.Data.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class Subject : LocalizableEntity
    {
        public Subject()
        {
            StudentSubjects = new HashSet<StudentSubject>();
            DepartmentSubjects = new HashSet<DepartmentSubject>();
            InstructorSubjects = new HashSet<InstructorSubject>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50)]
        public string? NameAr { get; set; }
        [StringLength(50)]

        public string? NameEn { get; set; }
        public int? Period { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }

        [InverseProperty("Subject")]
        public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; }

        [InverseProperty("Subject")]
        public virtual ICollection<InstructorSubject> InstructorSubjects { get; set; }


    }
}
