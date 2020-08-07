using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Client.Model;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Client.WeatherService
{
    class WeatherForecastService
    {
        private Lazy<HttpClient> httpClient = new Lazy<HttpClient>();
        public WeatherForecastService() { }

        public async Task<List<WeatherRecord>> GetForecast(string city, DateTime date)
        {
            try
            {
                string sDate = date.ToString("yyyy-MM-dd");
                var response = await httpClient.Value.GetAsync(
                    $"{Properties.Resources.ApiLocation}" +
                    $"/weather" +
                    $"/get" +
                    $"?city={city}" +
                    $"&date={sDate}"
                );

                if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError) 
                {
                    throw new HttpRequestException(message: "При выполнении запроса произошла ошибка.");
                }

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<WeatherResponse>(content);

                return result.success ? result.WeatherRecords : null;

            } catch (HttpRequestException)
            {
                throw new HttpRequestException(message: "При выполнении запроса произошла ошибка.");
            }
        }
    }
}
