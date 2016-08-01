using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Transcode.Services
{
    public class ConversionApi
    {

        private HttpClient httpClient = new HttpClient();
        private readonly string apiBaseAddress = ConfigurationManager.AppSettings["apiAddress"];

        public ConversionApi()
        {
            httpClient.BaseAddress = new Uri(apiBaseAddress);
        }

        public async Task<String> Convert(string fileName, string convertTo, string userId)
        {
            //RequestMethod 
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string jsonData = "{userId : \"" + userId + "\", fileName : \"" + fileName + "\", convertTo : \"" + convertTo + " \"}";
            HttpContent httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            String uri = "api/Conversion/Convert";
            HttpResponseMessage response = await httpClient.PostAsync(uri, httpContent);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();

            }
            return null;

        }
        public async Task<IEnumerable<string>> GetVideoFormatsSupported()
        {
            string uri = "api/Conversion/Formats/Video";

            HttpResponseMessage response = await httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<string>>(data);
            }
            return new List<string>();
        }

        public async Task<IEnumerable<string>> GetAudioFormatsSupported()
        {
            string uri = "api/Conversion/Formats/Audio";

            HttpResponseMessage response = await httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<string>>(data);
            }
            return new List<string>();
        }
    }
}