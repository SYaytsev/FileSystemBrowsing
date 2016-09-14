using FileSystem.Models;
using FileSystem.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace FileSystem.Api
{
    [Route("api/[controller]")]
    public class BrowsingController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IBrowsingRepository _repository;

        public BrowsingController(IHostingEnvironment hostingEnvironment, IBrowsingRepository repository)
        {
            _hostingEnvironment = hostingEnvironment;
            _repository = repository;
        }

        [HttpGet]
        public FileSystemModel GetFileSystemModel(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = _hostingEnvironment.WebRootPath;
            }
            return _repository.GetFileSystemModel(path);
        }
    }
}
