#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : CustomerCase business logic class
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
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class CustomerCaseBL : AbstractBusinessLogic, ICustomerCaseBL
    {
        #region Variables
        private readonly IMapper _customerCaseMapper;
        private readonly IMapper _customerResponseMapper;
        private readonly IMapper _customerResponseEvidenceMapper;
        private readonly AutoMapper.IMapper _mapper;
        private StandardCodeBL _enumBL;
        #endregion

        #region Constructor
        public CustomerCaseBL()
        {
            _customerCaseMapper = MapperFactory.GetInstance().GetMapper(typeof(CustomerCase).ToString());
            _customerResponseMapper = MapperFactory.GetInstance().GetMapper(typeof(CustomerCaseResponse).ToString());
            _customerResponseEvidenceMapper = MapperFactory.GetInstance().GetMapper(typeof(CustomerCaseResponseEvidence).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _enumBL = new StandardCodeBL(_mapper);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get CustomerCase by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<CustomerCaseDto>> Read(CustomerCaseFilterDto filterDto, int pageSize)
        {
            return null;
        }

        /// <summary>
        /// Delete CustomerCase by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<CustomerCaseDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new CustomerCase
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<CustomerCaseDto> Create(CustomerCaseParameterDto objCreate)
        {
            var result = new ResponseBase<CustomerCaseDto>();
            byte[] fileBytes = null;
            var validationResults = new List<DNetValidationResult>();
            var isValid = true;
            CustomerCase customerCase = null;

            try
            {
                // validate if case is exist
                isValid = IsCustomerCaseByCaseNumberExist(objCreate.CaseNumber, objCreate.DealerCode, validationResults, ref customerCase);

                // check if the evidence is provided
                if (isValid && objCreate.EvidenceFile != null && !string.IsNullOrEmpty(objCreate.EvidenceFile.FileName))
                {
                    validationResults.AddRange(FileUtility.ValidateEvidenceOrIdentityFile(objCreate.EvidenceFile, _mapper, out fileBytes, FieldResource.EvidenceFile));
                }

                isValid = validationResults.Count == 0;

                // validate status
                if (isValid) { ValidateCustomerCaseStatus(objCreate, customerCase.Status, validationResults); }

                isValid = validationResults.Count == 0;

                if (isValid)
                {
                    // update customer case
                    customerCase.ReservationNumber = objCreate.ReservationNumber;
                    customerCase.Status = (short)objCreate.Status;

                    var id = (int)_customerCaseMapper.Update(customerCase, DNetUserName);
                    if (id > 0)
                    {
                        // insert response
                        CustomerCaseResponse newResponse = new CustomerCaseResponse();
                        newResponse.CustomerCase = customerCase;
                        newResponse.Description = objCreate.Description;
                        newResponse.CreatedBy = UserName;
                        newResponse.CreatedTime = DateTime.Now;
                        newResponse.LastUpdateTIme = DateTime.Now;

                        id = (int)_customerResponseMapper.Insert(newResponse, DNetUserName);
                        if (id > 0 && fileBytes != null)
                        {
                            // save the file
                            string filePath;
                            string uploadErrorMessage = FileUtility.SaveEvidenceFile(objCreate.EvidenceFile, fileBytes, out filePath);
                            if (!string.IsNullOrEmpty(uploadErrorMessage))
                            {
                                validationResults.Add(new DNetValidationResult(ErrorCode.DBSaveFailed, string.Format(MessageResource.ErrorMsgDataType, uploadErrorMessage)));
                            }
                            else
                            {
                                // insert evidence
                                CustomerCaseResponseEvidence newResponseEvidence = new CustomerCaseResponseEvidence();
                                newResponseEvidence.EvidenceFile = filePath;
                                newResponseEvidence.CustomerCaseResponse = _customerResponseMapper.Retrieve(id) as CustomerCaseResponse;
                                newResponseEvidence.CreatedBy = UserName;
                                newResponseEvidence.CreatedTime = DateTime.Now;
                                newResponseEvidence.LastUpdateTIme = DateTime.Now;

                                id = (int)_customerResponseEvidenceMapper.Insert(newResponseEvidence, DNetUserName);
                            }
                        }
                    }

                    result.success = id > 0;

                    if (!result.success) ErrorMsgHelper.DataCorrupt(result.messages);
                    result._id = id;
                    result.total = 1;
                }
                else
                {
                    return PopulateValidationError<CustomerCaseDto>(validationResults, null);
                }
            }
            catch (SqlException ex)
            {
                ErrorMsgHelper.SqlException(result.messages, ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Update CustomerCase
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<CustomerCaseDto> Update(CustomerCaseParameterDto objUpdate)
        {
            return null;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Validate Customer Case Status
        /// </summary>
        /// <param name="p"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidateCustomerCaseStatus(CustomerCaseParameterDto param, short currentStatus, List<DNetValidationResult> validationResults)
        {
            // ignore validation if not set for now
            if (param.Status == 0)
            {
                param.Status = currentStatus;
                return true;
            }

            // get case status
            var caseStatus = _enumBL.GetByCategoryAndValue("EnumCustomerCaseResponse.CustomerCaseResponse", param.Status.ToString());
            if (caseStatus.ValueCode.Equals("Re_Open", StringComparison.OrdinalIgnoreCase) && currentStatus != 4)
            {
                validationResults.Add(new DNetValidationResult(MessageResource.ErrorMsgCustomerCaseStatusNotValid));
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get CustomerCase by case number
        /// </summary>
        /// <param name="caseNumber"></param>
        /// <returns></returns>
        private bool IsCustomerCaseByCaseNumberExist(string caseNumber, string dealerCode, List<DNetValidationResult> validationResults, ref CustomerCase customerCase)
        {
            int dealerId = 0;
            // at first check in dealer branch master
            DealerBranch branch = null;
            if (ValidationHelper.ValidateDealerBranch(dealerCode, validationResults, this.DealerCode, ref branch))
            {
                // get the id
                dealerId = branch == null ? 0 : branch.ID;
            }

            if (branch == null)
            {
                // reset validation result 
                validationResults.Clear();

                // check in dealer instead
                Dealer dealer = null;
                if (ValidationHelper.ValidateDealer(dealerCode, validationResults, this.DealerCode, ref dealer))
                {
                    // get the id
                    dealerId = dealer.ID;
                }
            }

            if (validationResults.Count == 0)
            {
                CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(CustomerCase), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(CustomerCase), "CaseNumber", MatchType.Exact, caseNumber));
                criterias.opAnd(new Criteria(typeof(CustomerCase), "Dealer.ID", MatchType.Exact, dealerId));
                var customerCases = _customerCaseMapper.RetrieveByCriteria(criterias);
                if (customerCases.Count > 0)
                {
                    customerCase = customerCases[0] as CustomerCase;
                    return true;
                }
                else
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgCustomerCaseNotFound, caseNumber, dealerCode)));
                }
            }

            return false;
        }
        #endregion
    }
}

