using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace KTB.DNet.WebApi.Helpers
{
    public class APIHelpers<Tout>
        where Tout : class
    {
        public APIHelpers() { this.sendAPI = false; }
        public APIHelpers(string uri) : this() { this.Url = uri; }

        private Tout restPost;
        private bool sendAPI;

        public string ProxyAddress { get; set; }
        public string ProxyPort { get; set; }
        public string Url { get; set; }
        public string JsonContent { get; set; }
        public string JsonResult { get; set; }
        public string StatusCode { get; set; }

        public Tout ResultPost
        {
            get{
                if (sendAPI) { return restPost; }
                return null;
            }
        }

        public async Task POST()
        {
            //construct content to send
            try
            {
                var content = new System.Net.Http.StringContent(JsonContent, Encoding.UTF8, "application/json");
                var client = new HttpClient();

                if (!string.IsNullOrEmpty(ProxyAddress))
                {
                    System.Net.WebProxy wp = new System.Net.WebProxy(ProxyAddress, Convert.ToInt32(ProxyPort));
                    var httpClientHandler = new HttpClientHandler
                    {
                        Proxy = wp,
                    };

                    client = new HttpClient(handler: httpClientHandler, disposeHandler: true);
                }

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri(this.Url),
                    Content = content,
                    Method = HttpMethod.Post
                };

                var res = await client.SendAsync(request);
                this.JsonResult = res.Content.ReadAsStringAsync().Result.Replace("\\", "")
                                                   .Trim(new char[1] { '"' });

                if (res.IsSuccessStatusCode && !String.IsNullOrEmpty(this.JsonResult))
                {
                    this.restPost = JsonConvert.DeserializeObject<Tout>(this.JsonResult);
                    sendAPI = true;
                }
                else { this.StatusCode = res.StatusCode.ToString(); }

            }
            catch (Exception ex)
            {
                this.StatusCode = ex.Message.ToString();
            }

        }
    }
}