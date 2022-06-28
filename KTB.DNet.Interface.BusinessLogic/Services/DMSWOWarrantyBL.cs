#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : DMSWOWarranty business logic class
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
using KTB.DNet.Interface.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using KTB.DNet.Domain.Search;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class DMSWOWarrantyBL : AbstractBusinessLogic, IDMSWOWarrantyBL
    {
        #region Variables
        private readonly IMapper _dmsWOWarrantyMapper;
        private readonly AutoMapper.IMapper _mapper;
        private readonly IMapper _dealerBranchMapper;
        #endregion

        #region Constructor
        public DMSWOWarrantyBL()
        {
            _dmsWOWarrantyMapper = MapperFactory.GetInstance().GetMapper(typeof(DMSWOWarrantyClaim).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _dealerBranchMapper = MapperFactory.GetInstance().GetMapper(typeof(DealerBranch).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get DMSWOWarranty by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<DMSWOWarrantyDto>> Read(DMSWOWarrantyFilterDto filterDto, int pageSize)
        {
            return null;
        }

        /// <summary>
        /// Delete DMSWOWarranty by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<DMSWOWarrantyDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new DMSWOWarranty
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<DMSWOWarrantyDto> Create(DMSWOWarrantyParameterDto objCreate)
        {
            var result = new ResponseBase<DMSWOWarrantyDto>();
            var validationResults = new List<DNetValidationResult>();
            ChassisMaster chassisMaster = null;
            ChassisMasterBB chassisMasterBB = null;
            DealerBranch dealerBranch = null;
            Dealer dealer = null;
            DMSWOWarrantyClaim dMSWOWarrantyClaim = null;

            try
            {
                if (!objCreate.isBB)
                {
                    ValidationHelper.ValidateChassisMaster(objCreate.ChassisNumber, validationResults, ref chassisMaster);
                }
                else
                {
                    ValidationHelper.ValidateChassisMasterBB(objCreate.ChassisNumber, validationResults, ref chassisMasterBB);
                }
                ValidationHelper.ValidateDealer(objCreate.DealerCode, validationResults, this.DealerCode, ref dealer);

                ValidationHelper.ValidateDealerBranch(this.DealerCode, validationResults, objCreate.DealerBranchCode, ref dealerBranch);

                bool isValid = validationResults.Count == 0;

                if (isValid)
                {
                    isValid = !this.ValidateDataDMSWOWarrantyExists(this.DealerCode, objCreate.DealerBranchCode, objCreate.WorkOrderNumber, objCreate.ChassisNumber, ref dMSWOWarrantyClaim);

                    if (isValid == false)
                    {
                        validationResults.Add(new DNetValidationResult(string.Format("Data Warranty Claim untuk Chassis {0} dengan WorkOrder {1} pada Dealer {2} Sudah Tersedia!!", objCreate.ChassisNumber, objCreate.WorkOrderNumber, DealerCode)));
                    }
                }

                if (isValid)
                {
                    DMSWOWarrantyClaim dmsWOwarrantyClaim = _mapper.Map<DMSWOWarrantyClaim>(objCreate);
                    dmsWOwarrantyClaim.Dealer = dealer;
                    if (dealerBranch != null)
                        dmsWOwarrantyClaim.DealerBranch = dealerBranch;

                    int id = (int)_dmsWOWarrantyMapper.Insert(dmsWOwarrantyClaim, DNetUserName);

                    result.success = id > 0;
                    if (!result.success)
                    {
                        foreach (var item in validationResults)
                        {
                            result.messages.Add(new MessageBase { ErrorCode = ErrorCode.DataTypeOrDataFormatInvalid, ErrorMessage = item.ErrorMessage });
                        }
                        if (validationResults.Count == 0)
                        {
                            ErrorMsgHelper.DataCorrupt(result.messages);
                        }
                    }

                    result._id = id;
                    result.total = 1;
                }
                else
                {
                    return PopulateValidationError<DMSWOWarrantyDto>(validationResults, null);
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
        /// Update DMSWOWarranty
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<DMSWOWarrantyDto> Update(DMSWOWarrantyParameterDto objUpdate)
        {
            return null;
        }

        /// <summary>
        /// Delete DMSWOWarranty Existing
        /// </summary>
        /// <param name="objDelete"></param>
        /// <returns></returns>
        public ResponseBase<DMSWOWarrantyDto> Delete(DMSWOWarrantyDeleteParameterDto objDelete)
        {
            var result = new ResponseBase<DMSWOWarrantyDto>();
            var validationResults = new List<DNetValidationResult>();
            DealerBranch dealerBranch = null;
            Dealer dealer = null;
            DMSWOWarrantyClaim dMSWOWarrantyClaim = null;

            try
            {
                ValidationHelper.ValidateDealer(objDelete.DealerCode, validationResults, this.DealerCode, ref dealer);

                ValidationHelper.ValidateDealerBranch(this.DealerCode, validationResults, objDelete.DealerBranchCode, ref dealerBranch);

                bool isValid = validationResults.Count == 0;

                if (isValid)
                {
                    isValid = this.ValidateDataDMSWOWarrantyExists(this.DealerCode, objDelete.DealerBranchCode, objDelete.WorkOrderNumber, objDelete.ChassisNumber, ref dMSWOWarrantyClaim);

                    if (isValid == false)
                    {
                        validationResults.Add(new DNetValidationResult(string.Format("Data Warranty Claim untuk Chassis {0} dengan WorkOrder {1} pada Dealer {2} Tidak Tersedia!!", objDelete.ChassisNumber, objDelete.WorkOrderNumber, DealerCode)));
                    }
                }

                if (isValid)
                {
                    dMSWOWarrantyClaim.RowStatus = -1;

                    int id = (int)_dmsWOWarrantyMapper.Update(dMSWOWarrantyClaim, DNetUserName);

                    result.success = id > 0;
                    if (!result.success)
                    {
                        foreach (var item in validationResults)
                        {
                            result.messages.Add(new MessageBase { ErrorCode = ErrorCode.DataTypeOrDataFormatInvalid, ErrorMessage = item.ErrorMessage });
                        }
                        if (validationResults.Count == 0)
                        {
                            ErrorMsgHelper.DataCorrupt(result.messages);
                        }
                    }

                    result._id = dMSWOWarrantyClaim.ID;
                    result.total = 1;
                }
                else
                {
                    return PopulateValidationError<DMSWOWarrantyDto>(validationResults, null);
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

        private bool ValidateDataDMSWOWarrantyExists (string dealerCode, string dealerBranchCode, string workOrderNumber, string chassisNumber, ref DMSWOWarrantyClaim dMSWOWarrantyClaim)
        {
            var dMSWOWarrantyCriteria = new CriteriaComposite(new Criteria(typeof(DMSWOWarrantyClaim), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            dMSWOWarrantyCriteria.opAnd(new Criteria(typeof(DMSWOWarrantyClaim), "WorkOrderNumber", MatchType.Exact, workOrderNumber));
            //check Dealer
            dMSWOWarrantyCriteria.opAnd(new Criteria(typeof(DMSWOWarrantyClaim), "Dealer.DealerCode", MatchType.Exact, dealerCode));
            if (dealerBranchCode != null && dealerBranchCode.Trim() != string.Empty && dealerBranchCode.Trim() != "")
            {
                dMSWOWarrantyCriteria.opAnd(new Criteria(typeof(DMSWOWarrantyClaim), "DealerBranch.DealerBranchCode", MatchType.Exact, dealerBranchCode));
            }

            var dMSWOWarranty = _dmsWOWarrantyMapper.RetrieveByCriteria(dMSWOWarrantyCriteria);

            if (dMSWOWarranty.Count == 0)
            {
                return false;
            }
            else
            {
                dMSWOWarrantyClaim = dMSWOWarranty.Cast<DMSWOWarrantyClaim>().ToList()[0];
                return true;
            }
        }
        #endregion
    }
}

