﻿using System;
using System.Net;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Web.Configuration;
using Dashboard.Models;
using System.Linq;

namespace Dashboard
{
    public class Pocket
    {
        public object GetPocketData()
        {
            string PocketConsumerKey = ConfigurationManager.AppSettings["consumer_key"];
            string PocketAccessToken = ConfigurationManager.AppSettings["access_token"];
            var client = new WebClient();       
            JavaScriptSerializer javascriptSerializer = new JavaScriptSerializer();
            var postData = javascriptSerializer.Serialize(new { consumer_key = PocketConsumerKey, access_token = PocketAccessToken });
            client.Headers.Add(System.Net.HttpRequestHeader.ContentType, "application/json");
            var content = client.UploadString("https://getpocket.com/v3/get", postData);
            var pocketJSON = javascriptSerializer.Deserialize<BaseObject>(content);

            // list data coming back null
            var test = pocketJSON.list.First().Value.given_title;
            return pocketJSON;
        }

    }
    public class Weather
    {
        public object GetWeatherData()
        {
            string WeatherUndergroundKey = ConfigurationManager.AppSettings["api-key"];
            string WeatherURL = String.Format("http://api.wunderground.com/api/{0}/conditions/q/NJ/Caldwell.json", WeatherUndergroundKey);
            var client = new WebClient();
            var data = client.DownloadString(WeatherURL);
            var serializer = new JavaScriptSerializer();
            var weatherContent = serializer.Deserialize<object>(data);
            return weatherContent;
        }
    }
    public class Habitica
    {
        public object GetHabiticaData()
        {
            string HabiticaUserToken = ConfigurationManager.AppSettings["x-api-user-token"];
            string HabiticaApiKey = ConfigurationManager.AppSettings["x-api-user-key"];
            var client = new WebClient();
            client.Headers.Add(System.Net.HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add("x-api-user", HabiticaUserToken);
            client.Headers.Add("x-api-key", HabiticaApiKey);
            var content = client.DownloadString("https://habitica.com/export/userdata.json");
            var serializer = new JavaScriptSerializer();
            var jsonContent = serializer.Deserialize<object>(content);
            return jsonContent;
        }
    }
}
