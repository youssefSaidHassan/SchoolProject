using Microsoft.AspNetCore.Http;
using SchoolProject.Data.Entities;

namespace SchoolProject.Service.Abstracts
{
    public interface IInstructorService
    {
        public Task<decimal> GetSalarySummationOfInstructor();
        public Task<string> AddInstructorAsync(Instructor instructor, IFormFile file);
    }
}
