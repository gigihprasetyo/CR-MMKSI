#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : BaseInterface.cs interface
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
using KTB.DNet.Interface.Domain;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    /// <summary>
    /// Base Interface 
    /// This base interface contains the Standard CRUD process 
    /// </summary>
    /// <typeparam name="TObjectParameter"></typeparam>
    /// <typeparam name="TFilterParameter"></typeparam>
    /// <typeparam name="TObjectResult"></typeparam>
    public interface IBaseInterface<TObjectParameter, TFilterParameter, TObjectResult>
        where TFilterParameter : class
        where TObjectParameter : class
        where TObjectResult : class
    {
        /// <summary>
        /// Create Object 
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        ResponseBase<TObjectResult> Create(TObjectParameter objCreate);

        /// <summary>
        /// Update Object 
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        ResponseBase<TObjectResult> Update(TObjectParameter objUpdate);

        /// <summary>
        /// Filter Object 
        /// </summary>
        /// <param name="filterDto"></param>
        /// <returns></returns>
        ResponseBase<List<TObjectResult>> Read(TFilterParameter filterDto, int pageSize);

        /// <summary>
        /// Delete by Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ResponseBase<TObjectResult> Delete(int id);

        /// <summary>
        /// Setup the credentials
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="dealerCode"></param>
        void Initialize(string userName, string dealerCode);

        /// <summary>
        /// Setup the credentials
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="dealerCode"></param>
        //void CustomInitialize(string userName, string dealerCode, List<Dealer> listDealer, string dealerCompanyName);

    }
}
