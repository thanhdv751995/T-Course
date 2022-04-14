using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Project.NguoiDungs;
using Project.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Project.HttpClients
{
    public class HttpClientService : ProjectAppService, IHttpClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public HttpClientService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<ResponseResult> ResponseWithModel<Model>(object body, string path)
        {
            var json = JsonConvert.SerializeObject(body);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration.GetSection("AuthServer")["Authority"]);
            var response = await client.PostAsync(path, httpContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = new ResponseResult
            {
                Success = response.IsSuccessStatusCode
            };
            if (response.IsSuccessStatusCode)
            {
                result.Data = JsonConvert.DeserializeObject<Model>(jsonResponse);
            }
            else
            {
                result.Data = JsonConvert.DeserializeObject<ErrorMessage>(jsonResponse);
            }
            return result;
        }
        public async Task<ResponseResult> ResponseWithoutModel(object body, string path)
        {
            var json = JsonConvert.SerializeObject(body);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration.GetSection("AuthServer")["Authority"]);
            var response = await client.PostAsync(path, httpContent);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = new ResponseResult
            {
                Success = response.IsSuccessStatusCode
            };
            if (!response.IsSuccessStatusCode)
            {
                result.Data = JsonConvert.DeserializeObject<ErrorMessage>(jsonResponse);
            }
            return result;
        }

        public async Task<ResponseResult> ResponseTokenModel(object body, string path)
        {
            var json = JsonConvert.SerializeObject(body);
            var payload = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            Console.WriteLine(payload);

            var content = new FormUrlEncodedContent(payload);
            content.Headers.Clear();
            content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration.GetSection("AuthServer")["Authority"]);
            var response = await client.PostAsync(path, content);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = new ResponseResult
            {
                Success = response.IsSuccessStatusCode
            };
            if (response.IsSuccessStatusCode)
            {
                result.Data = JsonConvert.DeserializeObject<Token>(jsonResponse);
            }
            else
            {
                result.Data = JsonConvert.DeserializeObject<TokenError>(jsonResponse);
            }
            return result;
        }
    }
}
