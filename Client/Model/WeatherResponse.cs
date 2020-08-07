using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Model
{
    public class WeatherResponse
    {
        [Newtonsoft.Json.JsonProperty("message")]
        public List<WeatherRecord> WeatherRecords { get; set; }
        [Newtonsoft.Json.JsonProperty("error")]
        public string error { get; set; }
        [Newtonsoft.Json.JsonProperty("success")]
        public bool success { get; set; }
    }
}
