using SeaportClientApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SeaportClientApplication.Rest
{
    public static class RestRequests
    {
        private static string requestUrl = "http://localhost:54154/";

        public static async Task<HttpResponseMessage> GetRequest(string requestUri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(requestUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
                return await client.GetAsync(requestUri);
            }
        }

        public static async Task<HttpResponseMessage> PostRequest(string requestUri, FormUrlEncodedContent content)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(requestUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return await client.PostAsync(requestUri, content);
            }
        }

        public static HttpResponseMessage JsonPostRequest(string requestUri, PierBooking content)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(requestUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return client.PostAsJsonAsync<PierBooking>(requestUri, content).Result;
            }
        }
    }
}