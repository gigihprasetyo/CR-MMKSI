#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ThrottleInfo class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.Interface.Framework.Models;
using System;
#endregion

namespace KTB.DNet.Interface.WebApi.Helper.Throttle
{
    /// <summary>
    /// Throttle information class
    /// </summary>
    public class ThrottleInfo : IThrottleInfo
    {
        /// <summary>
        /// Expires property
        /// </summary>
        public DateTime ExpiresAt { get; set; }

        /// <summary>
        /// Request Count
        /// </summary>
        public int RequestCount { get; set; }

        /// <summary>
        /// Request limit
        /// </summary>
        public int RequestLimit { get; set; }

        /// <summary>
        /// Time in seconds
        /// </summary>
        public int TimeInSeconds { get; set; }

        /// <summary>
        /// Flag to activate or disabled throttle checking
        /// </summary>
        public bool Enable { get; set; }



        public int Id
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string GetURI()
        {
            throw new NotImplementedException();
        }
    }
}