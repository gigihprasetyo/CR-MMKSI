#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AddChallangeOnUnauthorizedResult.cs class
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
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
#endregion

namespace KTB.DNet.Interface.WebUI.Helper
{
    /// </summary>
    /// <summary>
    /// Add authorization result
    /// </summary>
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

                var json = JsonConvert.SerializeObject(
                    new ResponseMessage()
                    {
                        Success = false,
                        Status = ResponseStatus.Warning,
                        Message = response.StatusCode == HttpStatusCode.Forbidden ?
                                    MessageResource.ErrorMsgAuthPermission :
                                    MessageResource.ErrorMsgAuthPrivilege
                    });

                response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            return response;
        }

    }
}