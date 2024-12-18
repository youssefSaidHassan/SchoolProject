using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class DepartmentSubject
    {
        [Key]
        public int DepartmentId { get; set; }
        [Key]
        public int SubjectId { get; set; }
        [ForeignKey("DepartmentId")]
        [InverseProperty("DepartmentSubjects")]
        public virtual Department Department { get; set; }
        [ForeignKey("SubjectId")]
        [InverseProperty("DepartmentSubjects")]

        public virtual Subject Subject { get; set; }
    }
}
