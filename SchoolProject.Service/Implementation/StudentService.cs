using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Enums;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementation
{
    public class StudentService : IStudentService
    {
        #region Fields
        private readonly IStudentRepository _studentRepository;
        #endregion

        #region Constructor
        public StudentService(IStudentRepository studentRepository)
        {
            this._studentRepository = studentRepository;
        }

        #endregion

        #region Handel Functions
        public async Task<List<Student>> GetStudentsAsync() => await _studentRepository.GetStudentsAsync();
        public async Task<Student> GetStudentByIDAsync(int id)
        {
            return _studentRepository.GetTableNoTracking().Include(s => s.Department).FirstOrDefault(s => s.Id == id);
        }

        public async Task<string> CreateAsync(Student student)
        {
            await _studentRepository.AddAsync(student);
            return "Success";
        }

        public async Task<bool> IsNameExist(string name)
        {
            var student = _studentRepository.GetTableNoTracking().Where(s => s.NameAr.Equals(name)).FirstOrDefault();
            if (student == null)
                return false;
            else
                return true;
        }
        public async Task<bool> IsNameExistExcludeSelf(string name, int id)
        {
            var student = await _studentRepository.GetTableNoTracking().Where(s => s.NameEn.Equals(name) & !s.Id.Equals(id)).FirstOrDefaultAsync();
            if (student == null)
                return false;
            else
                return true;
        }

        public async Task<string> EditAsync(Student student)
        {
            await _studentRepository.UpdateAsync(student);
            return "Success";
        }

        public async Task<string> DeleteAsync(Student student)
        {
            var trans = _studentRepository.BeginTransaction();
            try
            {
                await _studentRepository.DeleteAsync(student);
                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "Falied";
            }
        }

        public async Task<Student> GetStudentByIDWithoutIncludingAsync(int id)
        {

            return await _studentRepository.GetTableNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        }

        public IQueryable<Student> GetStudentsQuerable()
        {
            return _studentRepository.GetTableNoTracking().Include(s => s.Department).AsQueryable();
        }

        public IQueryable<Student> FilterStudentPaginatedQuerable(StudentOrderingEnum ordering, string search)
        {
            var querable = _studentRepository.GetTableNoTracking().Include(s => s.Department).AsQueryable();
            if (search != null)
            {
                querable = querable.Where(s => s.NameAr.ToLower().Contains(search.ToLower()));
            }
            switch (ordering)
            {
                case StudentOrderingEnum.Name:
                    querable = querable.OrderBy(s => s.NameAr);
                    break;
                case StudentOrderingEnum.Address:
                    querable = querable.OrderBy(s => s.Address);
                    break;
                case StudentOrderingEnum.DepartmentName:
                    querable = querable.OrderBy(s => s.Department.NameAr);
                    break;
                default:
                    querable = querable.OrderBy(s => s.Id);
                    break;
            }

            return querable;
        }

        public IQueryable<Student> GetStudentsQuerableByDepartmentId(int departmentId)
        {
            return _studentRepository.GetTableNoTracking().Where(s => s.Id.Equals(departmentId)).AsQueryable();
        }
        #endregion
    }
}
