using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using SharpInstaAPI.Instagram.Models;


/// <summary>
/// Zusammenfassungsbeschreibung für InstagramClient
/// </summary>
/// 
namespace SharpInstaAPI.Instagram
{
    public class InstagramClient
    {
        public string AccessToken { get; set; }

        public InstagramClient()
        {

        }

        public InstagramObject GetRecentMedia()
        {
            string result = GetData("https://api.instagram.com/v1/users/self/media/recent/");
            InstagramObject media = JsonConvert.DeserializeObject<InstagramObject>(result);

            if (media != null) return media;
            return null;
        }

        public void GetUserInformation()
        {
            string result = GetData("https://api.instagram.com/v1/users/self/");
        }

        private string GetData(string url)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("?access_token=" + AccessToken).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                client.Dispose();
                return result;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                client.Dispose();
                return null;
            }
        }
    }
}
