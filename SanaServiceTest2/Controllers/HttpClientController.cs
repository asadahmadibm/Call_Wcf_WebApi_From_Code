using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace SanaServiceTest2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HttpClientController : ControllerBase
    {
        [HttpPost(Name = "GetWebApiByHttpClient")]
        public async Task<String> GetWebApiByHttpClient()
        {
            //var httpClient = new HttpClient();
            //var parameters = new Dictionary<string, string>();
            ////parameters["text"] = text;
            //secur secur = new secur() { username = "demo@example.com", password = "demo#123" };
            //var parameter = new StringContent(JsonConvert.SerializeObject(secur), UnicodeEncoding.UTF8, "application/json");
            //var response = await httpClient.PostAsync("https://swagger.tnlink.ir/Auth", parameter);
            //var contents = await response.Content.ReadAsStringAsync();

            //return contents;
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://swagger.tnlink.ir/");
            var request = new HttpRequestMessage(HttpMethod.Post, "/Auth");

            //var byteArray = new UTF8Encoding().GetBytes("token:demo#123");
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            secur secur = new secur() { username = "demo@example.com", password = "demo#123" };
            request.Content = new StringContent(JsonConvert.SerializeObject(secur), UnicodeEncoding.UTF8, "application/json");
            var response = await client.SendAsync(request);
            //return response;
            string contents = "";
            if (response.IsSuccessStatusCode)
            {
                contents = await response.Content.ReadAsStringAsync();
            }
            return contents;


        }


        public class secur
        {
            public string username { get; set; }
            public string password { get; set; }

        }
    }
}
