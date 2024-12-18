using SchoolProject.Data.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class Student : LocalizableEntity
    {
        public Student()
        {
            StudentSubjects = new HashSet<StudentSubject>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50)]

        public string? NameAr { get; set; }
        [StringLength(50)]

        public string? NameEn { get; set; }
        [StringLength(500)]
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public int? DepartmentID { get; set; }

        [ForeignKey("DepartmentID")]
        [InverseProperty("Students")]
        public virtual Department Department { get; set; }

        [InverseProperty(nameof(StudentSubject.Student))]
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }

    }
}
