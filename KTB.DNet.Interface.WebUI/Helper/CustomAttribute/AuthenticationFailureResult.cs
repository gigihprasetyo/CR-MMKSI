#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AuthenticationFailureResult.cs class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $ Muhamad Ridwan
// Created on 18/10/2018 17:05
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Resources;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
#endregion

namespace KTB.DNet.Interface.WebUI.Helper
{
    /// </summary>
    /// <summary>
    /// Authentication failure result
    /// </summary>
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

            var json = JsonConvert.SerializeObject(
                                        new ResponseMessage()
                                        {
                                            Success = false,
                                            Status = ResponseStatus.Warning,
                                            Message = MessageResource.ErrorMsgAuthPrivilege
                                        });

            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            response.ReasonPhrase = Regex.Replace(ReasonPhrase, @"\t|\n|\r", "");
            return response;
        }
    }
}