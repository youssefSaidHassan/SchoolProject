using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Commons;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities.Views
{
    [Keyless]
    public class ViewDepartment : LocalizableEntity
    {
        public int Id { get; set; }
        public string? NameEn { get; set; }
        public string? NameAr { get; set; }
        [Column("students")]
        public int StudentCount { get; set; }

    }
}
