#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : JWTManager  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $ Muhamad Ridwan - Initial
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
#endregion

namespace KTB.DNet.Interface.Framework.Helper
{
    public class JWTManager
    {
        /// <summary>
        /// Generate token
        /// </summary>
        /// <param name="claimsData"></param>
        /// <param name="secret"></param>
        /// <param name="expires"></param>
        /// <returns></returns>
        public string GenerateToken(IEnumerable<Claim> claimsData, Guid secret, DateTime expires)
        {
            expires = expires < DateTime.UtcNow ? DateTime.UtcNow.AddDays(14) : expires;

            string issuer = ConfigurationManager.AppSettings["TokenIssuer"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret.ToString()));
            var signInCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenIdentifier = new JwtSecurityToken(
                    issuer: issuer,
                    expires: expires,
                    claims: claimsData,
                    signingCredentials: signInCredentials
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(tokenIdentifier);
            return token;
        }

        /// <summary>
        /// Get principal
        /// </summary>
        /// <param name="token"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public ClaimsPrincipal GetPrincipal(string token, out string errorMessage)
        {
            errorMessage = string.Empty;

            var tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = ReadToken(token, tokenHandler, out errorMessage);

            if (jwtToken == null)
            {
                return null;
            }

            if (ValidateClaimsHandler == null)
            {
                return null;
            }

            string secretKey = string.Empty;

            if (!ValidateClaimsHandler(jwtToken.Claims, token, out secretKey, out errorMessage))
            {
                return null;
            }

            var symmetricKey = Encoding.UTF8.GetBytes(secretKey.ToString());

            var validationParameters = new TokenValidationParameters()
            {
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ValidateIssuer = true,
                ValidIssuer = ConfigurationManager.AppSettings["TokenIssuer"],
                ValidateAudience = true,
                ValidAudience = ConfigurationManager.AppSettings["TokenAudience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
            };

            SecurityToken securityToken;

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                return principal;

            }
            catch (Exception e)
            {
                DateTime currentTime = DateTime.Now;
                DateTime currentUTCTime = DateTime.UtcNow;

                var audiences = jwtToken.Audiences.ToList();
                errorMessage = string.Format("{0}. Server: {1}, Token: {2}, [ServerExp: {3}; TokenValidTo:{4} ]", e.Message, validationParameters.ValidAudience + ";" + validationParameters.ValidIssuer, ((audiences != null && audiences.Count() > 0) ? string.Format("({0})", string.Join(" | ", audiences)) : "") + ";" + jwtToken.Issuer, currentTime.ToString("yyyy-MM-dd HH:mm:ss:ffff") + "||" + currentUTCTime.ToString("yyyy-MM-dd HH:mm:ss:ffff"), jwtToken.ValidTo.ToString("yyyy-MM-dd HH:mm:ss:ffff"));
                return null;
            }
        }

        /// <summary>
        /// Read token
        /// </summary>
        /// <param name="token"></param>
        /// <param name="tokenHandler"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private JwtSecurityToken ReadToken(string token, JwtSecurityTokenHandler tokenHandler, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                JwtSecurityToken jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                {
                    errorMessage = "Token is not valid. Failed to read token.";
                    return null;
                }

                return jwtToken;
            }
            catch
            {
                errorMessage = "Token is not valid. Failed to read token.";
                return null;
            }
        }

        public ValidateClaims ValidateClaimsHandler;
        public delegate bool ValidateClaims(IEnumerable<Claim> claims, string token, out string secretKey, out string errorMessage);
    }
}
