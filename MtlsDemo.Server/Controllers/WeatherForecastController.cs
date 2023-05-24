using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace MtlsDemo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var certificate = HttpContext.Items["ClientCertificate"] as X509Certificate2;
            if (certificate != null)
            {
                // Use certificate information as needed
                var subjectName = certificate.SubjectName.Name;
                return Ok($"Greeting from server -  Certificate presented by client: {subjectName} " +
                    $"- The presented certificate {(certificate.HasPrivateKey ? "has" : "doesn't have")} private key");
            }
            else
            {
                return Ok("Greeting from server - No client certificate presented.");
            }
        }
    }
}