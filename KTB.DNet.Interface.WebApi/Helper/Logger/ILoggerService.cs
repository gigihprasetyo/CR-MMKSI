#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ILoggerService class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:14
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.WebApi.Models;
using System;
#endregion

namespace KTB.DNet.Interface.WebApi
{
    /// <summary>
    /// Log <see cref="Exception"/> objects.
    /// </summary>
    public interface ILoggerService
    {
        /// <summary>
        /// Set user modifier
        /// </summary>
        /// <param name="username"></param>
        void SetUserModifier(string username);

        /// <summary>
        /// Logs the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void Log(Exception exception);

        /// <summary>
        /// Log Data into Log table
        /// </summary>
        /// <param name="dataLog"></param>
        /// <param name="dealerCode"></param>
        long LogTransaction(DataLogModel dataLog, string dealerCode);

        /// <summary>
        /// Thread Log Data 
        /// </summary>
        /// <param name="threadLog">Thread Log</param>
        long LogTransactionRuntime(TransactionRuntime threadLog);

        /// <summary>
        /// Reset log status from memory cache
        /// </summary>
        void LogCachingReset();

        /// <summary>
        /// Get Thread by Log Id 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        TransactionRuntime GetTransactionRuntime(long Id);
    }
}
