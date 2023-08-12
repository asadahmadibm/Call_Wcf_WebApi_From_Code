using BankService;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;

namespace SanaServiceTest2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {

            SanaBankServiceClient sanaBankServiceClient = new SanaBankServiceClient();
            var result = sanaBankServiceClient.GetSanaInfo(new SanaRequestInfo { TrackingCode = "4809205136", NationalId = "4284891545", Amount = 74, CurrencyId = 1, CurrencyUseId = 102 });


            //SanaGomrokService.SanaGomrokServiceClient sanaClient=new SanaGomrokService.SanaGomrokServiceClient();
            //sanaClient.ClientCredentials.UserName.UserName = "user_gomrok";
            //sanaClient.ClientCredentials.UserName.Password = "GMrk@1401#08";
            //sanaClient.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.PeerTrust;
            //var result =sanaClient.GetSanaInfo("3732026353");


            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

      
    }
}