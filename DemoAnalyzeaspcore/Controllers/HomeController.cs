using DemoAnalyzeaspcore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Diagnostics;

namespace DemoAnalyzeaspcore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PrivacyAsync([FromForm(Name ="image")]IFormFile file)
        {
            string apiKey = "2d938cadd5c543898907bebbc18380d0";
            string endpoint = "https://sprkle-analyze-image.cognitiveservices.azure.com/";
            string imageUrl = "https://sprklestore.blob.core.windows.net/images/00004344-2919-4562-ab5e-8e1e9cef2b51";


            var apiKeyServiceClientCredentials = new ApiKeyServiceClientCredentials(apiKey);

            var computerVisionClient = new ComputerVisionClient(apiKeyServiceClientCredentials);
            computerVisionClient.Endpoint = endpoint;
            var requestOptions = new List<VisualFeatureTypes?>
        {
            VisualFeatureTypes.Adult
        };
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                try
                {
                var image=await computerVisionClient.AnalyzeImageInStreamAsync(memoryStream, requestOptions);

                }
                catch (Exception ex)
                {

                }
            }
            // Load the image to be analyzed.
            //var image = await computerVisionClient.AnalyzeImageInStreamAsync(file, requestOptions);
            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}