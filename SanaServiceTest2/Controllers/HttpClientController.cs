using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private IMemoryCache _memoryCache;
        public HttpClientController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpPost]
        [Route("GetWebApiTokenByHttpClient")]
        public async Task<String> GetWebApiTokenByHttpClient()
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

        [HttpPost]
        [Route("GetGroupByHttpClient")]
        public async Task<String> GetGroupByHttpClient()
        {
            var client1 = new HttpClient();
            client1.BaseAddress = new Uri("https://swagger.tnlink.ir/");
            var request1 = new HttpRequestMessage(HttpMethod.Post, "/Group/GetById");
            string token =await GetToken();
            client1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            idDto crmRegionDto = new idDto() { id = 1 };
            request1.Content = new StringContent(JsonConvert.SerializeObject(crmRegionDto), UnicodeEncoding.UTF8, "application/json");
            var response1 = await client1.SendAsync(request1);
            string contents1 = "";
            if (response1.IsSuccessStatusCode)
            {
                contents1 = await response1.Content.ReadAsStringAsync();
            }
            return contents1;

        }

        [HttpGet]
        [Route("GetToken")]
        public async Task<string> GetToken()
        {
            string token = "";
            //var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOlsiMSIsIjEiXSwidW5pcXVlX25hbWUiOiLYp9iz2LnYryDYp9it2YXYr9uMIiwiVXNlck5hbWUiOiLYp9iz2LnYryDYp9it2YXYr9uMIiwicGVybWlzc2lvbiI6IlN5c0FkbWluLEVjYXJTYWxlcyIsIlVzZXJUeXBlIjoiMSIsInByb3ZpbmNlcyI6IiIsIlJvbGVzIjoiU3lzQWRtaW4iLCJuYmYiOjE2OTE4MTU3NjAsImV4cCI6MTY5MTgxOTM2MCwiaWF0IjoxNjkxODE1NzYwLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MDEyIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo0MjAwIn0.GZa72vsSZuaCblDIw469gfYW2gyLTShsVQaLXsLvVak";

            if (!_memoryCache.TryGetValue("token", out token))
            {
                var contents =await GetWebApiTokenByHttpClient();
                _memoryCache.Set("token", contents);

            }
            return token;
        }
        public class secur
        {
            public string username { get; set; }
            public string password { get; set; }

        }

        public class idDto
        {
            public int  id { get; set; }

        }

    }
}
