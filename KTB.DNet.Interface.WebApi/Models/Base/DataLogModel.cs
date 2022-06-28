#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : DataLogModel class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:14
//
// ===========================================================================	
#endregion

using System;
namespace KTB.DNet.Interface.WebApi.Models
{
    public class DataLogModel
    {
        /// <summary>
        /// Sender's IP
        /// </summary>
        public string SenderIP { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// DealerCode
        /// </summary>
        public string DealerCode { get; set; }

        /// <summary>
        /// ClientId
        /// </summary>
        public Guid ClientId { get; set; }

        /// <summary>
        /// AppId
        /// </summary>
        public Guid AppId { get; set; }

        /// <summary>
        /// Request Data
        /// </summary>
        public object RequestData { get; set; }

        /// <summary>
        /// Result of Transaction/ Response
        /// </summary>
        public object ResponseData { get; set; }
        /// <summary>
        /// Succeed object
        /// </summary>
        public bool Succeed { get; set; }

        /// <summary>
        /// Value is false if its not Re-Send process and vice versa
        /// </summary>
        public bool IsResend { get; set; }

        /// <summary>
        /// Parent id transaction to track resend process
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// The one who did the resend process
        /// </summary>
        public string UpdatedBy { get; set; }
    }
}