using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Repositories
{
    public class StudentRepository :GenericRepository<Student>, IStudentRepository 
    {
        #region fields
        private readonly DbSet<Student> _students;
        #endregion

        #region Constructor
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
            _students = context.Set<Student>();
        }
        #endregion

        #region Handel Functions
        public async Task<List<Student>> GetStudentsAsync() => await _students.Include(s => s.Department).ToListAsync();
      
        #endregion
    }
}
