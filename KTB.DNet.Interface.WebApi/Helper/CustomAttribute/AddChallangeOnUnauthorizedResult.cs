#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AddChallangeOnUnauthorizedResult class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $ Muhamad RIdwan - Initial checkin
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region Namespace Import
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using KTB.DNet.Interface.WebApi.Helper.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
#endregion

namespace KTB.DNet.Interface.WebApi.Helper
{
    public class AddChallengeOnUnauthorizedResult : IHttpActionResult
    {
        /// <summary>
        /// Construct authorization result
        /// </summary>
        /// <param name="challenge"></param>
        /// <param name="actionResult"></param>
        public AddChallengeOnUnauthorizedResult(AuthenticationHeaderValue challenge, IHttpActionResult actionResult)
        {
            Challenge = challenge;
            InnerResult = actionResult;
        }

        /// <summary>
        /// Challenge
        /// </summary>
        public AuthenticationHeaderValue Challenge { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public IHttpActionResult InnerResult { get; private set; }

        /// <summary>
        /// Execute asynchronus method if failed with cancellation token
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await InnerResult.ExecuteAsync(cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized ||
                response.StatusCode == HttpStatusCode.Forbidden)
            {
                // Only add one challenge per authentication scheme.
                if (!response.Headers.WwwAuthenticate.Any((h) => h.Scheme == Challenge.Scheme))
                {
                    response.Headers.WwwAuthenticate.Add(Challenge);
                }

                MessageBase message = new MessageBase();
                message.ErrorCode = ErrorCode.AuthNoPrivilege;
                bool isAuthResponse = true;
                isAuthResponse = string.IsNullOrEmpty(GetHeader(response.Headers, "X-Scheduler"));
                if (isAuthResponse)
                {
                    message.ErrorMessage =
                    response.StatusCode == HttpStatusCode.Forbidden ?
                            MessageResource.ErrorMsgAuthPermission :
                            (
                                string.IsNullOrEmpty(response.ReasonPhrase) ?
                                MessageResource.ErrorMsgAuthPrivilege :
                                string.Format("{0} ({1})", MessageResource.ErrorMsgAuthPrivilege, response.ReasonPhrase)
                             );

                    List<MessageBase> messages = new List<MessageBase>();
                    messages.Add(message);

                    var errorResult = HttpCodeMessage.BuildErrorResult(messages);
                    HttpStatusCode httpStatusCode = HttpCodeMessage.GetHttpStatusCode(messages);

                    var json = JsonConvert.SerializeObject(errorResult);

                    response.Content = new StringContent(json, Encoding.UTF8, "application/json");
                }
            }

            return response;
        }

        private string GetHeader(HttpResponseHeaders headers, string headerKey)
        {
            try
            {
                IEnumerable<string> values;
                if (headers.TryGetValues(headerKey, out values))
                {
                    return values.First();
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}