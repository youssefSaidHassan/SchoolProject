using Microsoft.AspNetCore.Http;

namespace SchoolProject.Service.Abstracts
{
    public interface IFileService
    {
        public Task<string> UploadFile(string location, IFormFile file);
    }
}
