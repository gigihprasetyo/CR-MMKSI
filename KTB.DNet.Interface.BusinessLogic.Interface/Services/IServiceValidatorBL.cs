#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ServiceValidator interface
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 29/10/2018 9:44
//
// ===========================================================================	
#endregion

using KTB.DNet.Interface.Model;

namespace KTB.DNet.Interface.BusinessLogic.Interface
{
    public interface IServiceValidatorBL
    {
        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        ResponseBase<ServiceValidatorDto> Validate(ServiceValidatorParameterDto param);

        /// <summary>
        /// Setup the credentials
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="dealerCode"></param>
        void Initialize(string userName, string dealerCode);
    }
}
