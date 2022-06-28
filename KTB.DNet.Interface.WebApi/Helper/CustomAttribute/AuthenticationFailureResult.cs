#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AuthenticationFailureResult class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $ Muhamad Ridwan - Initial Checkin
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.WebApi.Helper.Messages;
using KTB.DNet.Interface.Resources;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http; 
using KTB.DNet.Interface.Domain;
#endregion

namespace KTB.DNet.Interface.WebApi.Helper
{
    public class AuthenticationFailureResult : IHttpActionResult
    {
        /// <summary>
        /// Authentication failure result
        /// </summary>
        /// <param name="reasonPhrase"></param>
        /// <param name="request"></param>
        public AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage request)
        {
            ReasonPhrase = reasonPhrase;
            Request = request;
        }

        /// <summary>
        /// Reason phrase
        /// </summary>
        public string ReasonPhrase { get; private set; }

        /// <summary>
        /// Request message
        /// </summary>
        public HttpRequestMessage Request { get; private set; }

        /// <summary>
        /// Execute asynchronus
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        /// <summary>
        /// Execute response message
        /// </summary>
        /// <returns></returns>
        private HttpResponseMessage Execute()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized);

            response.RequestMessage = Request;
            var message = new MessageBase()
            {
                ErrorCode = ErrorCode.AuthNoPrivilege,
                ErrorMessage = MessageResource.ErrorMsgAuthPrivilege
            };

            var errorResult = HttpCodeMessage.BuildErrorResult(message);
            HttpStatusCode httpStatusCode = HttpCodeMessage.GetHttpStatusCode(message);
            var json = JsonConvert.SerializeObject(errorResult);

            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            response.ReasonPhrase = Regex.Replace(ReasonPhrase, @"\t|\n|\r", "");
            return response;
        }
    }
}