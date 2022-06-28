using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.IO;
using Newtonsoft.Json;
using KTB.DNet.Domain;
using KTB.DNET.BusinessFacade;
using KTB.DNET.BusinessFacade.Service;
using KTB.DNet.BusinessFacade.General;
using System.Collections;
using KTB.DNet.Domain.Search;
using KTB.DNet.WebApi.Models;

namespace KTB.DNet.WebApi.Controllers
{
    public class ServiceReminderController : BaseApiController
    {
        [HttpPost]
        public IDictionary<string, object> Update()
        {
            bool success = false; string message = string.Empty; bool isvalid = false; string jsonBody = String.Empty;
            if (!IsLogin()) return result(false, "-1", 0, String.Concat(_ACCESS_DENIED_AUTHENTICATION, " Your Token is invalid."), null);

            jsonBody = Request.Content.ReadAsStringAsync().Result;
            var updateData = JsonConvert.DeserializeObject<ServiceReminderUpdate>(jsonBody);

            if (updateData.SalesForceID.Length <= 10)
            {
                message = "Salesforce ID tidak dikenali (ID kurang dari 10 karakter)";
                success = false;
                isvalid = false;
            }
            else
                isvalid = true;

            ArrayList dataResult;
            CriteriaComposite criterias;
            criterias = new CriteriaComposite(new Criteria(typeof(ServiceReminder), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(ServiceReminder), "SalesforceID", MatchType.Exact, updateData.SalesForceID));
            ServiceReminderFacade svcReminderFacade = new ServiceReminderFacade(User);
            ServiceReminderFollowUpFacade svcReminderFUFacade = new ServiceReminderFollowUpFacade(User);
            dataResult = svcReminderFacade.Retrieve(criterias);

            isvalid = validateSFID(dataResult, updateData.SalesForceID);

            if(isvalid)
            {
                if(dataResult.Count > 0)
                {
                    ServiceReminder svcReminder = (ServiceReminder)dataResult[0];
                    if(svcReminder.Status == 4)
                    {
                        svcReminder.Status = updateData.Status;

                        ServiceReminderFollowUp svcReminderFU = new ServiceReminderFollowUp()
                        {
                            ServiceReminder = svcReminder,
                            FollowUpDate = DateTime.Now,
                            FollowUpStatus = updateData.Status,
                            FollowUpAction = "Salesforce"
                        };

                        try
                        {
                            var fuRes = svcReminderFUFacade.Insert(svcReminderFU);
                            var res = svcReminderFacade.Update(svcReminder);
                            if (res < 1 || fuRes < 1)
                                throw new System.ArgumentException("Update gagal\nSRFUInsert_res:"+fuRes+";SRUpdate:"+res);

                            message = "Service Reminder dengan SalesForceID " + updateData.SalesForceID + " berhasil di update";
                            success = true;
                        }
                        catch(Exception ex)
                        {
                            success = false;
                            message = ex.Message;
                        }
                    }
                    else
                    {
                        success = true;
                        message = "Tidak bisa update data Service Reminder dengan status complete!";
                    }
                }
                else
                {
                    success = true;
                    message = "Service Reminder dengan SalesForceID " + updateData.SalesForceID + " tidak ditemukan";
                }
            }
            else
            {
                success = false;
                message = "Service Reminder dengan SalesForceID " + updateData.SalesForceID + " tidak ditemukan";
            }

            #region Log
            WsLog wslog = new WsLog();
            wslog.Source = GetClientIp();
            wslog.Status = success.ToString();
            wslog.Message = message;
            wslog.Body = String.Concat("K:SALESFORCESERVICEREMINDERUPDATE\n", jsonBody);
            wslog.RowStatus = 0;
            wslog.CreatedBy = "WebService";

            WsLogFacade wslogfacade = new WsLogFacade(User);
            wslogfacade.Insert(wslog);
            #endregion

            return this.result(success, "-1", 0, message, null);
        }

        private bool validateSFID(ArrayList arrSvcReminder, string sfid)
        {
            foreach (ServiceReminder s in arrSvcReminder)
            {
                if (!s.SalesforceID.Equals(sfid))
                    return false;
            }

            return true;
        }

        private string GetClientIp(HttpRequestMessage request = null)
        {
            request = request ?? Request;

            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                RemoteEndpointMessageProperty prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
                return prop.Address;
            }
            else if (HttpContext.Current != null)
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                return null;
            }
        }
	}
}