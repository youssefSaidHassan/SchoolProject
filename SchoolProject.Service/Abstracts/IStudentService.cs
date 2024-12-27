using SchoolProject.Data.Entities;
using SchoolProject.Data.Enums;

namespace SchoolProject.Service.Abstracts
{
    public interface IStudentService
    {
        public Task<List<Student>> GetStudentsAsync();
        public Task<Student> GetStudentByIDAsync(int id);
        public Task<Student> GetStudentByIDWithoutIncludingAsync(int id);

        public Task<string> CreateAsync(Student student);
        public Task<bool> IsNameExist(string name);
        public Task<bool> IsNameExistExcludeSelf(string name, int id);

        public Task<string> EditAsync(Student student);
        public Task<string> DeleteAsync(Student student);
        public IQueryable<Student> GetStudentsQuerable();
        public IQueryable<Student> GetStudentsQuerableByDepartmentId(int departmentId);

        public IQueryable<Student> FilterStudentPaginatedQuerable(StudentOrderingEnum ordering, string search);


    }
}
