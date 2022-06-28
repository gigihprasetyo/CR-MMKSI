#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : RecallService business logic class
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
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class RecallServiceBL : AbstractBusinessLogic, IRecallServiceBL
    {
        #region Variables
        private readonly IMapper _recallServiceMapper;
        private readonly IMapper _fielFixMapper;
        private readonly IMapper _recallCategoryMapper;
        private readonly IMapper _recallChassisMasterMapper;
        private readonly AutoMapper.IMapper _mapper;

        #endregion

        #region Constructor
        public RecallServiceBL()
        {
            _recallServiceMapper = MapperFactory.GetInstance().GetMapper(typeof(RecallService).ToString());
            _fielFixMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_FieldFixList).ToString());
            _recallChassisMasterMapper = MapperFactory.GetInstance().GetMapper(typeof(RecallChassisMaster).ToString());
            _recallCategoryMapper = MapperFactory.GetInstance().GetMapper(typeof(RecallCategory).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Create a new RecallService
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<RecallServiceDto> Create(RecallServiceParameterDto objCreate)
        {
            #region Initialization
            var result = new ResponseBase<RecallServiceDto>();
            var validationResults = new List<DNetValidationResult>();
            var isValid = true;
            ChassisMaster chassisMaster = null;
            ChassisMasterBB chassisMasterBB = null;
            RecallChassisMaster recallChassisMaster = null;
            Dealer dealer = null;
            RecallCategory recallCategory = null;
            DealerBranch dealerBranch = null;
            #endregion

            try
            {
                isValid = ValidationHelper.ValidateServiceDate(objCreate.ServiceDate, validationResults);

                if (isValid) 
                {
                    if (!string.IsNullOrEmpty(objCreate.ChassisNumber))
                    { 
                        isValid = ValidationHelper.ValidateChassisMaster(objCreate.ChassisNumber, validationResults, ref chassisMaster); 
                    }

                    if (!isValid)
                    { 
                        isValid = ValidationHelper.ValidateChassisMasterBB(objCreate.ChassisNumber, validationResults, ref chassisMasterBB);
                    }
                }

                if (isValid) 
                { 
                    GetRecallCategoryMaster(objCreate, validationResults, ref isValid, ref recallCategory); 
                }

                if (isValid)
                {
                    GetRecallChassisMaster(objCreate, validationResults, recallCategory.ID, ref isValid, ref recallChassisMaster);
                    if (recallChassisMaster != null)
                    {
                        recallChassisMaster.RecallCategory = recallCategory;
                }
                }

                if (isValid) 
                {
                    if (chassisMaster != null) 
                    { 
                        IsRecallServiceNotExist(chassisMaster.ID, recallChassisMaster.ID, objCreate.ChassisNumber, objCreate.RecallRegNo, validationResults, ref isValid); 
                    }

                    else if (chassisMasterBB != null)
                    { 
                        IsRecallServiceNotExistBB(chassisMasterBB.ID, recallChassisMaster.ID, objCreate.ChassisNumber, objCreate.RecallRegNo, validationResults, ref isValid); 
                    }

                }

                if (isValid) 
                { 
                    isValid = ValidationHelper.ValidateDealer(objCreate.DealerCode, validationResults, this.DealerCode, ref dealer); 
                }

                if (isValid) 
                { 
                    isValid = ValidationHelper.ValidateDealerBranch(this.DealerCode, validationResults, objCreate.DealerBranchCode, ref dealerBranch);
                }

                if (isValid)
                {
                    RecallService newRecallService = _mapper.Map<RecallService>(objCreate);
                    if (chassisMaster != null)
                    { 
                    newRecallService.ChassisMaster = chassisMaster;
                    }
                    else if (chassisMasterBB != null)
                    { 
                        newRecallService.ChassisMasterBB = chassisMasterBB; 
                    }
                    newRecallService.RecallChassisMaster = recallChassisMaster;
                    newRecallService.Dealer = dealer;
                    newRecallService.DealerBranch = dealerBranch;
                    newRecallService.BuletinNo = recallCategory.BuletinDescription;
                    newRecallService.CreatedBy = DNetUserName;
                    newRecallService.CreatedTime = DateTime.Now;
                    newRecallService.LastUpdateBy = DNetUserName;
                    newRecallService.LastUpdateTime = DateTime.Now;
                    var id = (int)_recallServiceMapper.Insert(newRecallService, DNetUserName);
                    result.success = id > 0;
                    if (!result.success)
                    {
                        ErrorMsgHelper.DataCorrupt(result.messages);
                    }
                    result._id = id;
                    result.total = 1;
                }
                else
                {
                    return PopulateValidationError<RecallServiceDto>(validationResults, null);
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
        /// Get RecallService by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<RecallServiceDto>> Read(RecallServiceFilterDto filterDto, int pageSize)
        {
            return null;
        }

        /// <summary>
        /// Get RecallService by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<FieldFixListDto>> Get(RecallServiceFilterDto filterDto, int pageSize)
        {
            var result = new ResponseBase<List<FieldFixListDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias                
                var criterias = Helper.BuildCriteria(typeof(VWI_FieldFixList), filterDto);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_FieldFixList), filterDto, sortColl);

                // get data
                var data = _fielFixMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VWI_FieldFixList>().ToList();
                    var listData = list.Select(item => _mapper.Map<FieldFixListDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_FieldFixList), filterDto);
                }

                result.success = true;

            }
            catch (SqlException ex)
            {
                ErrorMsgHelper.SqlExceptionRead(result.messages, ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Delete RecallService by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<RecallServiceDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Update RecallService
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<RecallServiceDto> Update(RecallServiceParameterDto objUpdate)
        {
            return null;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Validate recall service existance
        /// </summary>
        /// <param name="chassisMasterID"></param>
        /// <param name="recallChassisMasterID"></param>
        /// <param name="chassisNumber"></param>
        /// <param name="recallRegNo"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        private void IsRecallServiceNotExist(int chassisMasterID, int recallChassisMasterID, string chassisNumber, string recallRegNo, List<DNetValidationResult> validationResults, ref bool isValid)
        {
            // check in recall service master
            CriteriaComposite criteriaRecallService = new CriteriaComposite(new Criteria(typeof(RecallService), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteriaRecallService.opAnd(new Criteria(typeof(RecallService), "ChassisMaster.ID", MatchType.Exact, chassisMasterID));
            criteriaRecallService.opAnd(new Criteria(typeof(RecallService), "RecallChassisMaster.ID", MatchType.Exact, recallChassisMasterID));
            var recallServices = _recallServiceMapper.RetrieveByCriteria(criteriaRecallService);
            if (recallServices.Count > 0)
            {
                isValid = false;
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataRecallServiceExist, chassisNumber, recallRegNo)));
            }
        }

        /// <summary>
        /// Validate Recall Service Existence for Chassis Master BB
        /// </summary>
        /// <param name="chassisMasterID"></param>
        /// <param name="recallChassisMasterID"></param>
        /// <param name="chassisNumber"></param>
        /// <param name="recallRegNo"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        private void IsRecallServiceNotExistBB(int chassisMasterBBID, int recallChassisMasterID, string chassisNumberBB, string recallRegNo, List<DNetValidationResult> validationResults, ref bool isValid)
        {
            // check in recall service master
            CriteriaComposite criteriaRecallService = new CriteriaComposite(new Criteria(typeof(RecallService), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteriaRecallService.opAnd(new Criteria(typeof(RecallService), "ChassisMasterBB.ID", MatchType.Exact, chassisMasterBBID));
            criteriaRecallService.opAnd(new Criteria(typeof(RecallService), "RecallChassisMaster.ID", MatchType.Exact, recallChassisMasterID));
            var recallServices = _recallServiceMapper.RetrieveByCriteria(criteriaRecallService);
            if (recallServices.Count > 0)
            {
                isValid = false;
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataRecallServiceExist, chassisNumberBB, recallRegNo)));
            }
        }
        /// <summary>
        /// Get recall category
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        /// <param name="recallCategory"></param>
        private void GetRecallCategoryMaster(RecallServiceParameterDto objCreate, List<DNetValidationResult> validationResults, ref bool isValid, ref RecallCategory recallCategory)
        {
            // get recall category
            CriteriaComposite criteriaRecallCategory = new CriteriaComposite(new Criteria(typeof(RecallCategory), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteriaRecallCategory.opAnd(new Criteria(typeof(RecallCategory), "RecallRegNo", MatchType.Exact, (objCreate.RecallRegNo)));
            var recallCategories = _recallCategoryMapper.RetrieveByCriteria(criteriaRecallCategory);
            if (recallCategories.Count > 0)
            {
                // cast the object
                recallCategory = recallCategories[0] as RecallCategory;
            }
            else
            {
                isValid = false;
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, "RecallRegNo", objCreate.RecallRegNo)));
            }
        }

        /// <summary>
        /// Get recall master
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="recallCategoryID"></param>
        /// <param name="isValid"></param>
        /// <param name="recallChassisMaster"></param>
        private void GetRecallChassisMaster(RecallServiceParameterDto objCreate, List<DNetValidationResult> validationResults, int recallCategoryID, ref bool isValid, ref RecallChassisMaster recallChassisMaster)
        {
            // get recall chassis master
            CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(RecallChassisMaster), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteria.opAnd(new Criteria(typeof(RecallChassisMaster), "ChassisNo", MatchType.Exact, (objCreate.ChassisNumber)));
            criteria.opAnd(new Criteria(typeof(RecallChassisMaster), "RecallCategory.ID", MatchType.Exact, recallCategoryID));
            var masters = _recallChassisMasterMapper.RetrieveByCriteria(criteria);
            if (masters.Count > 0)
            {
                // cast the object
                recallChassisMaster = masters[0] as RecallChassisMaster;
            }
            else
            {
                isValid = false;
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgChassisNumberNotRegisteredInCM, objCreate.ChassisNumber)));
            }
        }

        #endregion
    }
}

