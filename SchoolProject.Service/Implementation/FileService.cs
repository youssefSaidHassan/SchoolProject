using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementation
{
    public class FileService : IFileService
    {
        #region Fileds
        private readonly IWebHostEnvironment _webHostEnvironment;

        #endregion

        #region Constructor
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion

        #region Handel Functions
        public async Task<string> UploadFile(string location, IFormFile file)
        {
            var path = _webHostEnvironment.WebRootPath + "/" + location + "/";
            if (file.Length > 0)
            {
                try
                {
                    var extension = Path.GetExtension(file.FileName);
                    var fileName = Guid.NewGuid().ToString().Replace("-", string.Empty) + extension;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fileStream = File.Create(path + fileName))
                    {
                        await file.CopyToAsync(fileStream);
                        await fileStream.FlushAsync();
                        return $"/{location}/{fileName}";
                    }
                }
                catch (Exception)
                {

                    return "FailedToUpload";
                }

            }
            else
            {
                return "NoFile";
            }
        }
        #endregion
    }
}
