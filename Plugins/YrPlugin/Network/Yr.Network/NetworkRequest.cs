using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Yr.Network
{
    public class NetworkRequest
    {

        private HttpClient client; 

        protected HttpClient Client
        {
            get
            {
                if (client == null)
                    client = new HttpClient();

                return client; 
            }
        }

        protected async Task<string> preformRequest(string path)
        {
            if (!checkUrl(path))
            {
                throw new ArgumentException("Not a valid url.");
            }

            var task =  Client.GetAsync(path)
                        .ContinueWith(tsk => tsk.Result.EnsureSuccessStatusCode())
                        .ContinueWith(tsk => tsk.Result.Content.ReadAsStringAsync());

            return await await task; 
        }

        protected async Task<string> preformRequest(string path, string content)
        {
            StringContent httpBody = new StringContent(content);

            return await preformRequest(path, httpBody); 
        }

        protected async Task<string> preformRequest(string path, HttpContent content)
        {
            if (!checkUrl(path))
            {
                throw new ArgumentException("Not a valid url.");
            }

            var task = Client.PostAsync(path, content)
                .ContinueWith(tsk => tsk.Result.EnsureSuccessStatusCode())
                .ContinueWith(tsk => tsk.Result.Content.ReadAsStringAsync());

            return await task.Unwrap(); 
        }

        protected async Task<string> preformRequest(string path, HttpContent content, HttpHeaders headers = null)
        {
            if (!checkUrl(path))
            {
                throw new ArgumentException("Not a valid url.");
            }

            HttpClient client = Client; 

            if(headers != null) 
            {
                foreach(var header in headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            var task = Client.PostAsync(path, content)
                .ContinueWith(tsk => tsk.Result.EnsureSuccessStatusCode())
                .ContinueWith(tsk => tsk.Result.Content.ReadAsStringAsync());

            return await task.Unwrap(); 
        }


        /// <summary>
        /// Returns a clean HTML input without the tags. 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected string sanitzeHtml(string input)
        {
            return Regex.Replace(input, @"<[^>]+>|&nbsp;", "").Trim();
        }

        /// <summary>
        /// Check if the url is valid. 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool checkUrl(string url)
        {
            Uri uriResult;
            return Uri.TryCreate(url, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
