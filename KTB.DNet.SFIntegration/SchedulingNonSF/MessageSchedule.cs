using KTB.DNet.BusinessFacade.General;
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.SFIntegration.SchedulingNonSF
{
    public static class MessageSchedule
    {
        class MessagerResponse
        {
            public MessagerResponse() { }
            public MessagerResponse(string status, string code, string message)
            {
                this.Status = status;
                this.Message = message;
                this.Code = code;
            }

            public string Status { get; set; }
            public string Code { get; set; }
            public string Message { get; set; }
        }

        class MessageServiceUser
        {
            public string ClientID { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string TypeMessage { get; set; }
            public string BodyMessage { get; set; }
            public string DestinationNo { get; set; }
            public string Email { get; set; }
            public string FID { get; set; }
        }

        class APIHelpers<Tout>
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
                get
                {
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


        public static void Send()
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            IMapper i_Mapper = MapperFactory.GetInstance().GetMapper(typeof(MessageNotification).ToString());
            AppConfigFacade appConfigFacade = new AppConfigFacade(User);

            ArrayList arrMsg = i_Mapper.RetrieveSP("up_MessageNotificationSchedule");
            foreach (var item in arrMsg)
            {
                try
                {
                    MessageNotification nItem = (MessageNotification)item;
                    MessageServiceUser msg = new MessageServiceUser();
                    msg.ClientID = nItem.MessageTemplate.ClientID.ToString();
                    msg.UserName = nItem.MessageTemplate.UserName;
                    msg.Password = nItem.MessageTemplate.Password;
                    msg.TypeMessage = "2";
                    msg.BodyMessage = nItem.Message;
                    msg.DestinationNo = nItem.PhoneNumber;

                    APIHelpers<MessagerResponse> api = new APIHelpers<MessagerResponse>();
                    api.JsonContent = JsonConvert.SerializeObject(msg);
                    api.Url = appConfigFacade.Retrieve("MSScheduleApiUrl").Value;
                    api.ProxyAddress = appConfigFacade.Retrieve("MSScheduleApiProxyAddress").Value;
                    api.ProxyPort = appConfigFacade.Retrieve("MSScheduleApiProxyPort").Value;
                    Task.Run(() => api.POST()).Wait();

                    WsLogFacade func = new WsLogFacade(User);
                    WsLog iData = new WsLog();
                    iData.Body = api.JsonContent;
                    iData.Message = api.StatusCode;
                    iData.Source = "MessageSchedule";
                    func.Insert(iData);

                    bool IsSuccess = false;
                    if (string.IsNullOrEmpty(api.StatusCode))
                    {
                        if (api.ResultPost.Code.IndexOf("OK") > -1)
                        {
                            IsSuccess = true;
                        }
                    }

                    if (IsSuccess)
                    {
                        nItem.SendDateTime = DateTime.Now;
                        nItem.Status = 1;//OK Terkirim
                        nItem.TransactionID = api.ResultPost.Message.Split(new char[';'], StringSplitOptions.RemoveEmptyEntries)[1].Trim();
                        var x = i_Mapper.Update(nItem, User.Identity.Name);
                    }
                    else
                    {
                        nItem.Status = 2;//Gagal Terkirim
                        var x = i_Mapper.Update(nItem, User.Identity.Name);
                    }
                }
                catch { }
            }
        }
    }
}
