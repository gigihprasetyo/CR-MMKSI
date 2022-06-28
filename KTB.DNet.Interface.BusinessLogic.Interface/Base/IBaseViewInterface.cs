#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : BaseViewInterface.cs interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 8/11/2018 13:05
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;
using System.Collections.Generic;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    /// <summary>
    /// Base Interface 
    /// This base interface contains READ process only
    /// </summary>    
    /// <typeparam name="TFilterParameter"></typeparam>
    /// <typeparam name="TObjectResult"></typeparam>
    public interface IBaseViewInterface<TFilterParameter, TObjectResult>
        where TFilterParameter : class
        where TObjectResult : class
    {
        /// <summary>
        /// Filter Object 
        /// </summary>
        /// <param name="filterDto"></param>
        /// <returns></returns>
        ResponseBase<List<TObjectResult>> Read(TFilterParameter filterDto, int pageSize);

        /// <summary>
        /// Setup the credentials
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="dealerCode"></param>
        void Initialize(string userName, string dealerCode);
    }
}
