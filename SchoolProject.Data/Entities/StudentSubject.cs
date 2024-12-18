using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class StudentSubject
    {
        [Key]
        public int StudentId { get; set; }
        [Key]
        public int SubjectId { get; set; }
        public decimal? Grade { get; set; }

        [ForeignKey("StudentId")]
        [InverseProperty("StudentSubjects")]
        public virtual Student Student { get; set; }

        [ForeignKey("SubjectId")]
        [InverseProperty("StudentSubjects")]

        public virtual Subject Subject { get; set; }
    }
}
