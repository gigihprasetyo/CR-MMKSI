using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.SFIntegration.Helpers
{
    public class GenericHelpers
    {
        public async Task<TOut> PostRequest<TIn, TOut>(string uri, TIn content) where TOut :class
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                

                //var auth = new AuthenticationClient(new HttpClient(handler));
                //await auth.UsernamePasswordAsync(ConsumerKey, ConsumerSecret, Username, Password, url);

                //var client = new ForceClient(auth.InstanceUrl, auth.AccessToken, auth.ApiVersion, new HttpClient(handler), new HttpClient(handler));

                using (var client = new HttpClient())
                {
                    HttpClientHandler handler = new HttpClientHandler();
                    String strWebProxy = string.Empty;
                    if (strWebProxy.Length > 0)
                    {
                        handler = new HttpClientHandler
                        {
                            UseDefaultCredentials = false,
                            Proxy = new WebProxy(strWebProxy, true, new string[] { }),
                            UseProxy = true,
                        };
                    }
                   
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    var serialized = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

                    using (HttpResponseMessage response = await client.PostAsync(uri, serialized))
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();

                        return JsonConvert.DeserializeObject<TOut>(responseBody);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
