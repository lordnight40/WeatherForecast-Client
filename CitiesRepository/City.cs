using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CitiesRepository
{
    public static class City
    {
        private static IEnumerable<string> _cities;
        private const string _serviceKey = "8392167383921673839216737983e144598839283921673dcba64355bb85e8c8e13e9c0";

        public static async Task<IEnumerable<string>> Load()
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync(
                "https://api.vk.com/method/database.getCities?" +
                $"access_token={_serviceKey}" +
                "&v=5.22" +
                "&country_id=1" +
                "&need_all=1"
            );
            
            var content = await response.Content.ReadAsStringAsync();
            content = JsonSerializer.Deserialize<string>(content);

            return null;
        }
    }
}
