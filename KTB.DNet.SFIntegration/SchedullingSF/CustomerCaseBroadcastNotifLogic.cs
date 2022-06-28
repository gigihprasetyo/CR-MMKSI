using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using KTB.DNet.BusinessFacade.PO;
using KTB.DNet.Domain;
using KTB.DNet.BusinessFacade.General;
using KTB.DNET.BusinessFacade;
using KTB.DNet.Domain.Search;
using System.Net;
using System.Net.Http;
using KTB.DNet.WebApi.Models;
using KTB.DNet.WebApi.Helpers;
using Newtonsoft.Json;

namespace KTB.DNet.SFIntegration.SchedullingSF
{
    public static class CustomerCaseBroadcastNotif
    {
        static GenericPrincipal _user = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);

        public static void Notify()
        {
            //try
            //{
                //CustomerCaseFacade objCustomerCaseFacade = new CustomerCaseFacade(User);
            DataSet ds = new CustomerCaseFacade(_user).RetrieveSp("sp_CustomerCaseReminderNotification");
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if(dt.Rows.Count > 0)
                    {
                        foreach(DataRow row in dt.Rows)
                        {
                            int ID = Convert.ToInt32(row[0]);
                            ArrayList contacts = new CustomerCaseFacade(_user).RetrievePhoneNumber(ID);
                            if (contacts.Count > 0)
                            {
                                Dictionary<string, string> dcConfig = new CustomerCaseFacade(_user).GetBodyMessage(ID);
                                AppConfigFacade funcConfig = new AppConfigFacade(_user);
                                foreach (string phoneNumber in contacts)
                                {
                                    MessageServiceUser msg = new MessageServiceUser();
                                    msg.ClientID = dcConfig.FirstOrDefault(x => x.Key == "ClientID").Value;
                                    msg.UserName = dcConfig.FirstOrDefault(x => x.Key == "Username").Value;
                                    msg.Password = dcConfig.FirstOrDefault(x => x.Key == "Password").Value;
                                    msg.FID = ID.ToString();
                                    msg.TypeMessage = "1";
                                    msg.BodyMessage = dcConfig.FirstOrDefault(x => x.Key == "BodyMessage").Value;
                                    msg.DestinationNo = phoneNumber;


                                    APIHelpers<MessagerResponse> api = new APIHelpers<MessagerResponse>();
                                    api.JsonContent = JsonConvert.SerializeObject(msg);
                                    api.Url = funcConfig.Retrieve("MSLeadApiUrl").Value;// "http://localhost/MMKSI.MessageServices/Api/MessageService/";
                                    api.ProxyAddress = funcConfig.Retrieve("MSLeadApiProxyAddress").Value;
                                    api.ProxyPort = funcConfig.Retrieve("MSLeadApiProxyPort").Value;
                                    Task.Run(() => api.POST()).Wait();
                                }
                            }
                        }
                    }
                }
            //}
            //catch
            //{

            //}
        }
    }
}
