#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ServiceValidator business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 29/10/2018 9:43
//
// ===========================================================================	
#endregion

#region Namespaces
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class ServiceValidatorBL : AbstractBusinessLogic, IServiceValidatorBL
    {
        #region Public Method
        /// <summary>
        /// Validate four kind of service parameters in one place
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ResponseBase<ServiceValidatorDto> Validate(ServiceValidatorParameterDto param)
        {
            var result = new ResponseBase<ServiceValidatorDto>();

            if (param.ServiceType.Equals("FS", System.StringComparison.OrdinalIgnoreCase))
            {
                if (param.isBB)
                    return ValidateFreeServiceBB(result, param);
                else
                    return ValidateFreeService(result, param);
            }
            else if (param.ServiceType.Equals("PDI", System.StringComparison.OrdinalIgnoreCase))
            {
                return ValidatePDI(result, param);
            }
            else if (param.ServiceType.Equals("PM", System.StringComparison.OrdinalIgnoreCase))
            {
                return ValidatePM(result, param);
            }
            //else if (param.ServiceType.Equals("MSP", System.StringComparison.OrdinalIgnoreCase))
            //{
            //    return ValidateMSP(result, param);
            //}
            else
            {
                result.success = true;
            }

            return result;
        }
        #endregion

        #region Private Methods
        ///// <summary>
        ///// Validate MSP Claim parameters
        ///// </summary>
        ///// <param name="result"></param>
        ///// <param name="param"></param>
        ///// <returns></returns>
        //private ResponseBase<ServiceValidatorDto> ValidateMSP(ResponseBase<ServiceValidatorDto> result, ServiceValidatorParameterDto param)
        //{
        //    #region Declare
        //    List<DNetValidationResult> validationResults = new List<DNetValidationResult>();
        //    ChassisMaster chassisMaster = null;
        //    Dealer dealer = null;
        //    DealerBranch dealerBranch = null;
        //    PMKind pmKind = null;
        //    MSPClaimBL bl = new MSPClaimBL();
        //    MSPClaim mspClaim = null;
        //    PMHeader pmHeader = null;
        //    #endregion

        //    try
        //    {
        //        // init
        //        bl.Initialize(this.UserName, this.DealerCode);

        //        // object parsing
        //        var mspClaimParameter = param.ConvertObject<MSPClaimParameterDto>();

        //        // validate 
        //        if (ValidateRequiredFields(mspClaimParameter, validationResults))
        //        {
        //            // validate
        //            bl.ValidateMSP(mspClaimParameter, validationResults, ref mspClaim, ref chassisMaster, ref pmHeader, ref pmKind, ref dealer, ref dealerBranch);

        //            // if any errors
        //            if (validationResults.Any())
        //            {
        //                return PopulateValidationError<ServiceValidatorDto>(validationResults, null);
        //            }
        //            else
        //            {
        //                result.success = true;
        //            }
        //        }
        //        else
        //        {
        //            return PopulateValidationError<ServiceValidatorDto>(validationResults, null);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.messages.Add(new MessageBase { ErrorCode = ErrorCode.UnhandledException, ErrorMessage = ex.Message });
        //    }

        //    return result;
        //}

        /// <summary>
        /// Validate PM parameters
        /// </summary>
        /// <param name="result"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private ResponseBase<ServiceValidatorDto> ValidatePM(ResponseBase<ServiceValidatorDto> result, ServiceValidatorParameterDto param)
        {
            #region Declare
            List<DNetValidationResult> validationResults = new List<DNetValidationResult>();
            ChassisMaster chassisMaster = null;
            Dealer dealer = null;
            DealerBranch dealerBranch = null;
            PMKind pmKind = null;
            PMHeaderBL bl = new PMHeaderBL();
            #endregion

            try
            {
                // init
                bl.Initialize(this.UserName, this.DealerCode);

                // object parsing
                var pmHeaderParameter = param.ConvertObject<PMHeaderCreateParameterDto>();

                // validate 
                if (ValidateRequiredFields(pmHeaderParameter, validationResults))
                {
                    // validate
                    bl.ValidatePM(pmHeaderParameter, validationResults, ref dealerBranch, ref chassisMaster, ref pmKind, ref dealer);

                    // if any errors
                    if (validationResults.Any())
                    {
                        return PopulateValidationError<ServiceValidatorDto>(validationResults, null);
                    }
                    else
                    {
                        result.success = true;
                    }
                }
                else
                {
                    return PopulateValidationError<ServiceValidatorDto>(validationResults, null);
                }
            }
            catch (Exception ex)
            {
                result.messages.Add(new MessageBase { ErrorCode = ErrorCode.UnhandledException, ErrorMessage = ex.Message });
            }

            return result;
        }

        /// <summary>
        /// Validate PDI parameters
        /// </summary>
        /// <param name="result"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private ResponseBase<ServiceValidatorDto> ValidatePDI(ResponseBase<ServiceValidatorDto> result, ServiceValidatorParameterDto param)
        {
            #region Declare
            List<DNetValidationResult> validationResults = new List<DNetValidationResult>();
            ChassisMaster chassisMaster = null;
            Dealer dealer = null;
            DealerBranch dealerBranch = null;
            PDIBL bl = new PDIBL();
            List<int> isAlreadyPDISameDealer = new List<int>();
            #endregion

            try
            {
                // init
                bl.Initialize(this.UserName, this.DealerCode);

                // object parsing
                var pdiParameter = param.ConvertObject<PDIParameterDto>();

                // validate 
                if (ValidateRequiredFields(pdiParameter, validationResults))
                {
                    // validate
                    bl.ValidatePDI(pdiParameter, validationResults, ref chassisMaster, ref dealer, ref dealerBranch, ref isAlreadyPDISameDealer);

                    // if any errors
                    if (validationResults.Any())
                    {
                        return PopulateValidationError<ServiceValidatorDto>(validationResults, null);
                    }
                    else
                    {
                        result.success = true;
                    }
                }
                else
                {
                    return PopulateValidationError<ServiceValidatorDto>(validationResults, null);
                }
            }
            catch (Exception ex)
            {
                result.messages.Add(new MessageBase { ErrorCode = ErrorCode.UnhandledException, ErrorMessage = ex.Message });
            }

            return result;
        }

        /// <summary>
        /// Validate Free Service parameters
        /// </summary>
        /// <param name="result"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private ResponseBase<ServiceValidatorDto> ValidateFreeService(ResponseBase<ServiceValidatorDto> result, ServiceValidatorParameterDto param)
        {
            #region Declare
            List<DNetValidationResult> validationResults = new List<DNetValidationResult>();
            List<ValidResult> validResults = new List<ValidResult>();
            ChassisMaster chassisMaster = null;
            Dealer soldDealer = null;
            Dealer fsDealer = null;
            DealerBranch dealerBranch = null;
            FSKind fsKind = null;
            FleetFaktur objFleetFaktur = null;
            FreeServiceBL bl = new FreeServiceBL();
            FreeService freeService = new FreeService();
            #endregion

            try
            {
                // init
                bl.Initialize(this.UserName, this.DealerCode);

                // object parsing
                var fsParameter = param.ConvertObject<FreeServiceParameterDto>();
                
                // validate 
                if (ValidateRequiredFields(fsParameter, validationResults))
                {
                    // validate
                    bl.ValidateFreeService(fsParameter, validationResults, ref validResults, ref freeService);

                    // if any errors
                    if (validationResults.Any())
                    {
                        return PopulateValidationError<ServiceValidatorDto>(validationResults, null);
                    }
                    else
                    {
                        result.success = true;
                    }
                }
                else
                {
                    return PopulateValidationError<ServiceValidatorDto>(validationResults, null);
                }
            }
            catch (Exception ex)
            {
                result.messages.Add(new MessageBase { ErrorCode = ErrorCode.UnhandledException, ErrorMessage = ex.Message });
            }

            return result;
        }

        /// <summary>
        /// Validate Free Service BB
        /// </summary>
        /// <param name="result"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private ResponseBase<ServiceValidatorDto> ValidateFreeServiceBB(ResponseBase<ServiceValidatorDto> result, ServiceValidatorParameterDto param)
        {
            #region Declare
            List<DNetValidationResult> validationResults = new List<DNetValidationResult>();
            List<ValidResult> validResults = new List<ValidResult>();
            ChassisMasterBB chassisMaster = null;
            Dealer soldDealer = null;
            Dealer fsDealer = null;
            DealerBranch dealerBranch = null;
            FSKind fsKind = null;
            FreeServiceBL bl = new FreeServiceBL();
            FreeServiceBB freeServiceBB = new FreeServiceBB();
            #endregion

            try
            {
                // init
                bl.Initialize(this.UserName, this.DealerCode);

                // object parsing
                var fsParameter = param.ConvertObject<FreeServiceParameterDto>();

                // validate 
                if (ValidateRequiredFields(fsParameter, validationResults))
                {
                    // validate
                    bl.ValidateFreeServiceBB(fsParameter, validationResults, ref validResults, ref freeServiceBB);

                    // if any errors
                    if (validationResults.Any())
                    {
                        return PopulateValidationError<ServiceValidatorDto>(validationResults, null);
                    }
                    else
                    {
                        result.success = true;
                    }
                }
                else
                {
                    return PopulateValidationError<ServiceValidatorDto>(validationResults, null);
                }
            }
            catch (Exception ex)
            {
                result.messages.Add(new MessageBase { ErrorCode = ErrorCode.UnhandledException, ErrorMessage = ex.Message });
            }

            return result;
        }

        /// <summary>
        /// Validate required fields
        /// </summary>
        /// <param name="param"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidateRequiredFields(object param, List<DNetValidationResult> validationResults)
        {
            PropertyInfo[] sourceProperties = param.GetType().GetProperties();

            foreach (PropertyInfo property in sourceProperties)
            {
                if (property.CustomAttributes.Where(att => att.AttributeType == typeof(RequiredAttribute)).Count() > 0)
                {
                    object propValue = property.GetValue(param, null);

                    if (property.PropertyType == typeof(string))
                    {
                        if (propValue == null || string.IsNullOrEmpty(propValue.ToString()))
                        {
                            validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, property.Name)));
                        }
                    }
                    else if (property.PropertyType == typeof(Int32))
                    {
                        if ((int)propValue == 0)
                        {
                            validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, property.Name)));
                        }
                    }
                    else if (property.PropertyType == typeof(DateTime))
                    {
                        DateTime temp;
                        if (DateTime.TryParse(propValue.ToString(), out temp))
                        {
                            if (temp == DateTime.MinValue)
                                validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, property.Name)));
                        }
                    }
                }
            }

            return validationResults.Count == 0;
        }
        #endregion
    }
}
