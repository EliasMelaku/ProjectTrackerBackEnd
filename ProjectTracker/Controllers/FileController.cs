using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTracker.Models;
using ProjectTrackingTool.Repositories;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ProjectTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private IHostingEnvironment _hostingEnvironment;
        private readonly IProjectRepository _projectRepository;

        public FileController(IHostingEnvironment hostingEnvironment, IProjectRepository projectRepository)
        {
            this._hostingEnvironment = hostingEnvironment;
            this._projectRepository = projectRepository;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UploadFile(IFormFile report, int id)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(report.FileName);
                var newFileName = id + fileInfo.Name;
                var path = Path.Combine("", _hostingEnvironment.ContentRootPath + "Reports\\" + newFileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    report.CopyTo(stream);
                };

                await _projectRepository.Update(id, path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok(e.Message);
            }
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReport(int id)
        {
            var path = await _projectRepository.GetReport(id);
            return Ok(path);
        }
    }
}
