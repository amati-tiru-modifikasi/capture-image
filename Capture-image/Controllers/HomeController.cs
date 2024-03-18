using Capture_image.Data;
using Capture_image.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Capture_image.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// wwwroot folder
        /// </summary>
        private IWebHostEnvironment _environment;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _environment = environment;
            _dbContext = dbContext;
        }

        // add method upload image
        public IActionResult UploadImage(string imageData)
        {
            var fileName = $"image_{DateTime.Now.Ticks}.png";
            var filePath = Path.Combine(_environment.WebRootPath, fileName);

            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                using (var bw = new BinaryWriter(fs))
                {
                    var data = Convert.FromBase64String(imageData.Split(',')[1]);
                    bw.Write(data);
                    bw.Close();
                }
                fs.Close();
            }

            var image = new ImageModel
            {
                FileName = fileName,
                FilePath = filePath
            };

            _dbContext.Images.Add(image);
            _dbContext.SaveChanges();

            return Json(new { success = true, imagePath = filePath });
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}