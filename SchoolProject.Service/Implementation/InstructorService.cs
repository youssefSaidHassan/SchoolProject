using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Abstract.Functions;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Service.Abstracts;
using System.Data;

namespace SchoolProject.Service.Implementation
{
    public class InstructorService : IInstructorService
    {
        #region Fields
        private readonly ApplicationDbContext _context;
        private readonly IInstructorFunctionsRepository _instructorFunctionsRepository;
        private readonly IInstructorRepository _instructorRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileService _fileService;
        #endregion

        #region Constructor
        public InstructorService(ApplicationDbContext context, IInstructorFunctionsRepository instructorFunctionsRepository, IFileService fileService,
            IInstructorRepository instructorRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _instructorFunctionsRepository = instructorFunctionsRepository;
            _fileService = fileService;
            _instructorRepository = instructorRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion


        #region Handel Functions
        public async Task<string> AddInstructorAsync(Instructor instructor, IFormFile file)
        {
            var context = _httpContextAccessor.HttpContext.Request;
            var url = context.Scheme + "://" + context.Host;
            var imageUrl = await _fileService.UploadFile("Instructors", file);
            switch (imageUrl)
            {
                case "NoFile": return "NoImage";
                case "FailedToUpload": return "FailedToUpload";
            }
            instructor.ImageUrl = url + imageUrl;
            try
            {
                var result = await _instructorRepository.AddAsync(instructor);
                return "Success";
            }
            catch (Exception)
            {
                return "FailedInAdd";
                throw;
            }
        }
        public Task<decimal> GetSalarySummationOfInstructor()
        {
            decimal result = 0;
            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }
                result = _instructorFunctionsRepository.GetSalarySummationOfInstructor("select dbo.GetSalarySummation()", cmd);
            }
            return Task.FromResult(result);
        }

        #endregion
    }
}
