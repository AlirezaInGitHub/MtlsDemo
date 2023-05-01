using Microsoft.AspNetCore.Mvc;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace MtlsDemo.Client.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private readonly ILogger<ClientController> _logger;

    public ClientController(ILogger<ClientController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var certFile = "./Certs/Self-Signed/cert.pfx";
        //certFile = "./Certs/server.pfx";
        var certificate = new X509Certificate2(certFile, "Password01");
        var handler = new HttpClientHandler
        {
            ClientCertificateOptions = ClientCertificateOption.Manual,
            ServerCertificateCustomValidationCallback = 
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
        };
        handler.ClientCertificates.Add(certificate);
        var client = new HttpClient(handler);

        try
        {
            var response = await client.GetAsync("https://localhost:54321/weatherforecast");
            return Ok($"Server Response: {await response.Content.ReadAsStringAsync()}");
        }
        catch
        {
            return Ok("⚠️ An error occured");
        }
    }
}
