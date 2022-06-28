using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Configuration;

using KTB.DNet.Domain;
using KTB.DNet.BusinessFacade.General;
using KTB.DNet.Domain.Search;
using KTB.DNET.BusinessFacade;
using System.Collections;

namespace KTB.DNet.SFIntegration.SchedulingNonSF
{
    public static class MessageTransactionLogic
    {
        public static async Task Send()
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            HttpClient client = new HttpClient();

            MessageTransactionFacade func = new MessageTransactionFacade(User);
            ArrayList arrSendMessager = func.RetrieveSendMessage();
            foreach (var item in arrSendMessager)
            {
                MessageTransaction nItem = (MessageTransaction)item;
                MessageTransaction checkItem = func.Retrieve(nItem.ID);
                if (checkItem.Status != 0)
                {
                    continue;
                }
                nItem.Status = 1;
                int rest = func.Update(nItem);
                
                //Send WA, Email, Or Sms
                try
                {

                }
                catch (Exception ex)
                {
                    nItem.Status = 3; //Set Status Resend
                    nItem.Nomer = 0;
                    func.Update(nItem);
                }
            }
        }

        public static async Task ReSend()
        {
            GenericPrincipal User = new System.Security.Principal.GenericPrincipal(new GenericIdentity("DNetHangfire"), null);
            HttpClient client = new HttpClient();

            MessageTransactionFacade func = new MessageTransactionFacade(User);
            ArrayList arrSendMessager = func.RetrieveSendMessage();
            foreach (var item in arrSendMessager)
            {
                MessageTransaction nItem = (MessageTransaction)item;

                MessageTransaction checkItem = func.Retrieve(nItem.ID);
                if (checkItem.Status != 0){
                    continue;
                }
                nItem.Status = 1;
                int rest = func.Update(nItem);

                //Send WA, Email, Or Sms
                try
                {
                    
                }
                catch (Exception ex)
                {  
                    nItem.Nomer = short.Parse((nItem.Nomer + 1).ToString());
                    if (nItem.Nomer == 3)
                        nItem.Status = 4;//Set Status Fail
                    else
                        nItem.Status = 3; //Set Status Resend
                    func.Update(nItem);
                }
            }
        }
    }
}
