// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using ET22_OpenWeather;
//
//    var ipStackProxy = IpStackProxy.FromJson(jsonString);

namespace ET22_OpenWeather
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class IpStackProxy
    {
        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("continent_code")]
        public string ContinentCode { get; set; }

        [JsonProperty("continent_name")]
        public string ContinentName { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("region_code")]
        public string RegionCode { get; set; }

        [JsonProperty("region_name")]
        public string RegionName { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("geoname_id")]
        public long GeonameId { get; set; }

        [JsonProperty("capital")]
        public string Capital { get; set; }

        [JsonProperty("languages")]
        public Language[] Languages { get; set; }

        [JsonProperty("country_flag")]
        public string CountryFlag { get; set; }

        [JsonProperty("country_flag_emoji")]
        public string CountryFlagEmoji { get; set; }

        [JsonProperty("country_flag_emoji_unicode")]
        public string CountryFlagEmojiUnicode { get; set; }

        [JsonProperty("calling_code")]
        public string CallingCode { get; set; }

        [JsonProperty("is_eu")]
        public bool IsEu { get; set; }
    }

    public partial class Language
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("native")]
        public string Native { get; set; }
    }

    public partial class IpStackProxy
    {
        public static IpStackProxy FromJson(string json) => JsonConvert.DeserializeObject<IpStackProxy>(json, Converter.Settings);
    }

    public partial class IpStackProxy
    {
        public static async Task<IpStackProxy> RecuperaCoordenadas()
        {
            HttpClient http = new HttpClient();

            var peticion = await http.GetAsync("http://api.ipstack.com/check?access_key=6ab4149d3fdd101c8414574ffabfcc73");

            var respuesta = await peticion.Content.ReadAsStringAsync();
            var objetoCoordenadas = IpStackProxy.FromJson(respuesta);
            return objetoCoordenadas;
        }
    }
}

