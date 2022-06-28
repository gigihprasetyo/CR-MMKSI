using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Threading.Tasks;
using KTB.DNet.WebApi.Models;
using KTB.DNet.WebApi.Models.SalesForce;

namespace KTB.DNet.WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {           
            var paramwalkinopportunity = new paramWalkinOpportunity
            {
                Name = "API 1 Test BSI 3",
                Address__c = "jln Ahmad Yani 3",
                Mobile_Phone__c = "081838294123",
                Gender__c = "Male",
                //Dealer__c = "a020l00000193bZ",
                //Car__c = "a010l000000TE5m",
                Information_Type__c = "Salesforce",
                Customer_Purposes__c = "Tanya kendaraan",
                Quantity__c = "1",
                Prospect_Date__c = DateTime.Now.ToString("yyyy-MM-dd"),
                LeadSource = "Call Center",
                Email__c = "test1@bsi.co.id",
                Consumen_Type__c = "Perorangan",
                SPK_No__c = "A23456789",
                SPK_Status__c = "OK",
                Validation_Key__c = "123123",
                StageName = "Closed Won",
                CloseDate = DateTime.Now.ToString("yyyy-MM-dd"),
                AccountID = "0010l000002YPbC"
            };

            Task.Run(
                () => SalesForce.Send(User, String.Concat("services/apexrest/", paramWalkinOpportunity.SObjectTypeName), paramwalkinopportunity)
            ).Wait();

            if (SalesForce.IsSuccess)
            {
                return String.Concat(paramwalkinopportunity.Name, " ", "Successfuly Added");
            }
            else
            {
                return SalesForce.Message;
            }
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
