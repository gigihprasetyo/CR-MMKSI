#region Summary
// ===========================================================================
// AUTHOR        : PT BSI 
// PURPOSE       : SparePartForecast business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 11/01/2022 15:13
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.ExceptionServices;
using DNetIF = KTB.DNet.Interface.Domain;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class SparePartForecastBL : AbstractBusinessLogic, ISparePartForecastBL
    {
        #region Variables
        private ISparePartForecastRepository<DNetIF.SparePartForecast_IF, int> _SparePartForecastRepo;
        private readonly IMapper _sparePartForecaastMapper;
        private readonly IMapper _sparePartForecaastDetailMapper;
        private readonly IMapper _termofpaymentMapper;
        private readonly IMapper _sparePartMasterMapper;
        private readonly IMapper _pqrHeaderMapper;
        private readonly IMapper _sparePartForecastMasterMapper;
        private readonly AutoMapper.IMapper _mapper;
        private TransactionManager _transactionManager;
        #endregion

        #region Constructor
        public SparePartForecastBL(ISparePartForecastRepository<DNetIF.SparePartForecast_IF, int> SparePartForecastRepo)
        {
            _SparePartForecastRepo = SparePartForecastRepo;
            _termofpaymentMapper = MapperFactory.GetInstance().GetMapper(typeof(TermOfPayment).ToString());
            _sparePartForecaastMapper = MapperFactory.GetInstance().GetMapper(typeof(DNet.Domain.SparePartForecastHeader).ToString());
            _sparePartForecaastDetailMapper = MapperFactory.GetInstance().GetMapper(typeof(DNet.Domain.SparePartForecastDetail).ToString());
            _sparePartMasterMapper = MapperFactory.GetInstance().GetMapper(typeof(SparePartMaster).ToString());
            _pqrHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(PQRHeader).ToString());
            _sparePartForecastMasterMapper = MapperFactory.GetInstance().GetMapper(typeof(SparePartForecastMaster).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _transactionManager = new TransactionManager();
            _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get SparePartForecast by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<SparePartForecastDto>> Read(SparePartForecastFilterDto filterDto, int pageSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Read SparePartForecast Data
        /// </summary>
        /// <param name="filterDto">Filter Parameter</param>
        /// <param name="pageSize">Page Size</param>
        /// <returns></returns>
        public ResponseBase<List<SparePartForecastDto>> ReadData(SparePartForecastFilterDto filterDto, int pageSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete SparePartForecast by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<SparePartForecastDto> Delete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create a new SparePartForecast
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<SparePartForecastDto> Create(SparePartForecastParameterDto objCreate)
        {
            #region Initialized
            var result = new ResponseBase<SparePartForecastDto>();
            var validationResults = new List<DNetValidationResult>();
            SparePartForecastHeader SparePartForecastDomain;
            List<SparePartForecastDetail> SparePartForecastDetails = new List<SparePartForecastDetail>();
            #endregion

            try
            {
                /* Validate Data */
                var isValid = ValidateData(objCreate, out SparePartForecastDomain, out SparePartForecastDetails, validationResults);
                if (isValid)
                {
                    var createdObject = InsertWithTransactionManager(SparePartForecastDomain, SparePartForecastDetails);
                    if (createdObject != null)
                    {
                        var obj = (SparePartForecastHeader)_sparePartForecaastMapper.Retrieve(createdObject.ID);
                        if (obj != null)
                        {
                            result.success = true;
                            result.total = 1;
                            result._id = createdObject.ID;
                            result.lst = new SparePartForecastDto
                            {
                                ID = obj.ID,
                                PoDate = obj.PoDate,
                                PoNumber = obj.PoNumber
                            };
                        }
                    }
                    else
                    {
                        validationResults.Add(new DNetValidationResult(ErrorCode.DBSaveFailed, string.Format(MessageResource.ErrorMsgPRGUnhandle, MessageResource.ErrorMsgOnSavingPleaseContactAdmin)));
                    }
                }
                else
                {
                    return PopulateValidationError<SparePartForecastDto>(validationResults, null);
                }
            }
            catch (Exception ex)
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.UnhandledException, string.Format(MessageResource.ErrorMsgPRGUnhandle, ex.Message)));
            }

            return result;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<SparePartForecastDto> Update(SparePartForecastParameterDto objUpdate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update SparePartForecast
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<SparePartForecastHeader> Update(SparePartForecastUpdateParameterDto objUpdate, bool isUpdateDMSPRNoOnly)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<List<SparePartForecastStockManagementDto>> ReadStockManagement(SparePartForecastStockManagementFilterDto filterDto, int pageSize)
        {
            var result = new ResponseBase<List<SparePartForecastStockManagementDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                var criterias = Helper.InitialStrCriteria(typeof(DNetIF.SparePartForecast_IF), filterDto);
                criterias = Helper.UpdateStrCriteria(typeof(DNetIF.SparePartForecast_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                sortColl = Helper.UpdateSortColumnDapper(typeof(DNetIF.SparePartForecast_IF), filterDto);

                List<DNetIF.SparePartForecast_IF> data = _SparePartForecastRepo.SearchStockManagement(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    if (data[0].StockManagement.Count > 0)
                    {
                        result.lst = data[0].StockManagement.ConvertList<DNetIF.SparePartForecastStockManagement_IF, SparePartForecastStockManagementDto>();
                        result.total = filteredTotalRow;
                    }
                    else
                    {
                        ErrorMsgHelper.DataNotFound(result.messages, typeof(SparePartForecastStockManagementDto), filterDto);
                    }
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SparePartForecastStockManagementDto), filterDto);
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

        public ResponseBase<List<SparePartForecastRejectDto>> ReadReject(SparePartForecastRejectFilterDto filterDto, int pageSize)
        {
            var result = new ResponseBase<List<SparePartForecastRejectDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                var criterias = Helper.InitialStrCriteria(typeof(DNetIF.SparePartForecast_IF), filterDto);
                criterias = Helper.UpdateStrCriteria(typeof(DNetIF.SparePartForecast_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(DNetIF.SparePartForecast_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "Status", "5", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(DNetIF.SparePartForecast_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                sortColl = Helper.UpdateSortColumnDapper(typeof(DNetIF.SparePartForecast_IF), filterDto);

                List<DNetIF.SparePartForecast_IF> data = _SparePartForecastRepo.SearchRejectedData(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    if (data[0].Reject.Count > 0)
                    {
                        result.lst = data[0].Reject.ConvertList<DNetIF.SparepartForecastReject_IF, SparePartForecastRejectDto>();
                        result.total = filteredTotalRow;
                    }
                    else
                    {
                        ErrorMsgHelper.DataNotFound(result.messages, typeof(SparePartForecastRejectDto), filterDto);
                    }
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SparePartForecastRejectDto), filterDto);
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

        public ResponseBase<List<SparePartForecastPOEstimateDto>> ReadPOEstimate(SparePartForecastPOEstimateFilterDto filterDto, int pageSize)
        {
            var result = new ResponseBase<List<SparePartForecastPOEstimateDto>>();
            var sortColl = string.Empty;
            var sortColldtl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            int filteredTotalRow_ = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                var criterias = Helper.InitialStrCriteria(typeof(DNetIF.SparePartForecast_IF), filterDto);
                criterias = Helper.UpdateStrCriteria(typeof(DNetIF.SparePartForecast_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(DNetIF.SparePartForecast_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                sortColl = Helper.UpdateSortColumnDapper(typeof(DNetIF.SparePartForecast_IF), filterDto);

                List<DNetIF.SparePartForecast_IF> data = _SparePartForecastRepo.SearchPOEstimateHeader(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    if (data[0].POEstimateHeader.Count > 0)
                    {
                        List<SparePartForecastPOEstimateDto> rst = data[0].POEstimateHeader.ConvertList<DNetIF.SparepartForecastPOEstimateHeader_IF, SparePartForecastPOEstimateDto>();
                        for (int i = 0; i < rst.Count; i++)
                        {
                            SparePartForecastPOEstimateFilterDto fltr = new SparePartForecastPOEstimateFilterDto();
                            var criteriasdtl = Helper.InitialStrCriteria(typeof(DNetIF.SparePartForecast_IF), fltr);
                            criteriasdtl = Helper.UpdateStrCriteria(typeof(DNetIF.SparePartForecast_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SparePartForecastHeaderID", rst[i].ID.ToString(), false, criteriasdtl);
                            criteriasdtl = Helper.UpdateStrCriteria(typeof(DNetIF.SparePartForecast_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SONumber", rst[i].SONumber.ToString(), false, criteriasdtl);
                            criteriasdtl = Helper.UpdateStrCriteria(typeof(DNetIF.SparePartForecast_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criteriasdtl);
                            sortColldtl = Helper.UpdateSortColumnDapper(typeof(DNetIF.SparePartForecast_IF), fltr);

                            List<DNetIF.SparePartForecast_IF> dataDetail = _SparePartForecastRepo.SearchPOEstimateDetail(
                                                criteriasdtl, innerQueryCriteria, sortColldtl, fltr.pages, pageSize, out filteredTotalRow_, out totalRow);

                            if (dataDetail != null && dataDetail.Count > 0)
                            {
                                if (dataDetail[0].POEstimateDetail.Count > 0)
                                {
                                    rst[i].Details = dataDetail[0].POEstimateDetail.ConvertList<DNetIF.SparepartForecastPOEstimateDetail_IF, SparePartForecastPOEstimateDetailDto>();
                                }
                            }
                        }

                        result.lst = rst;
                        result.total = filteredTotalRow;

                    }
                    else
                    {
                        ErrorMsgHelper.DataNotFound(result.messages, typeof(SparePartForecastPOEstimateDto), filterDto);
                    }
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SparePartForecastPOEstimateDto), filterDto);
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

        public ResponseBase<List<SparePartForecastGoodReceiptDto>> ReadGoodReceipt(SparePartForecastGoodReceiptFilterDto filterDto, int pageSize)
        {
            var result = new ResponseBase<List<SparePartForecastGoodReceiptDto>>();
            var sortColl = string.Empty;
            var sortColldtl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            int filteredTotalRow_ = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                var criterias = Helper.InitialStrCriteria(typeof(DNetIF.SparePartForecast_IF), filterDto);
                criterias = Helper.UpdateStrCriteria(typeof(DNetIF.SparePartForecast_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(DNetIF.SparePartForecast_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                sortColl = Helper.UpdateSortColumnDapper(typeof(DNetIF.SparePartForecast_IF), filterDto);

                List<DNetIF.SparePartForecast_IF> data = _SparePartForecastRepo.SearchGoodReceiptHeader(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    if (data[0].GoodReceiptHeader.Count > 0)
                    {
                        List<SparePartForecastGoodReceiptDto> rst = data[0].GoodReceiptHeader.ConvertList<DNetIF.SparepartForecastGoodReceiptHeader_IF, SparePartForecastGoodReceiptDto>();
                        for (int i = 0; i < rst.Count; i++)
                        {
                            SparePartForecastGoodReceiptFilterDto fltr = new SparePartForecastGoodReceiptFilterDto();
                            var criteriasdtl = Helper.InitialStrCriteria(typeof(DNetIF.SparePartForecast_IF), fltr);
                            criteriasdtl = Helper.UpdateStrCriteria(typeof(DNetIF.SparePartForecast_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SparePartForecastHeaderID", rst[i].ID.ToString(), false, criteriasdtl);
                            criteriasdtl = Helper.UpdateStrCriteria(typeof(DNetIF.SparePartForecast_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DONumber", rst[i].DONumber.ToString(), false, criteriasdtl);
                            criteriasdtl = Helper.UpdateStrCriteria(typeof(DNetIF.SparePartForecast_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criteriasdtl);
                            sortColldtl = Helper.UpdateSortColumnDapper(typeof(DNetIF.SparePartForecast_IF), fltr);

                            List<DNetIF.SparePartForecast_IF> dataDetail = _SparePartForecastRepo.SearchGoodReceiptDetail(
                                                criteriasdtl, innerQueryCriteria, sortColldtl, fltr.pages, pageSize, out filteredTotalRow_, out totalRow);

                            if (dataDetail != null && dataDetail.Count > 0)
                            {
                                if (dataDetail[0].GoodReceiptDetail.Count > 0)
                                {
                                    rst[i].Details = dataDetail[0].GoodReceiptDetail.ConvertList<DNetIF.SparepartForecastGoodReceiptDetail_IF, SparePartForecastGoodReceiptDetailDto>();
                                }
                            }
                        }

                        result.lst = rst;
                        result.total = filteredTotalRow;

                    }
                    else
                    {
                        ErrorMsgHelper.DataNotFound(result.messages, typeof(SparePartForecastGoodReceiptDto), filterDto);
                    }
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SparePartForecastGoodReceiptDto), filterDto);
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

        public ResponseBase<SparePartForecastValidatorDto> Validator(SparePartForecastParameterDto param)
        {
            #region Initialized
            var result = new ResponseBase<SparePartForecastValidatorDto>();
            var validationResults = new List<DNetValidationResult>();
            SparePartForecastHeader SparePartForecastDomain;
            List<SparePartForecastDetail> SparePartForecastDetails = new List<SparePartForecastDetail>();
            #endregion

            try
            {
                /* Validate Data */
                var isValid = ValidateData(param, out SparePartForecastDomain, out SparePartForecastDetails, validationResults);
                if (!isValid)
                {
                    return PopulateValidationError<SparePartForecastValidatorDto>(validationResults, null);
                }
                else
                {
                    result.success = true;
                    result.total = 1;
                }
            }
            catch (Exception ex)
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.UnhandledException, string.Format(MessageResource.ErrorMsgPRGUnhandle, ex.Message)));
            }

            return result;
        }
        #endregion

        #region Private Methods
        private bool ValidateData(SparePartForecastParameterDto param, out SparePartForecastHeader header, out List<SparePartForecastDetail> detail, List<DNetValidationResult> validationResults)
        {
            SparePartForecastHeader SparePartForecastDomain = new SparePartForecastHeader();
            List<SparePartForecastDetail> SparePartForecastDetailsDomain = new List<SparePartForecastDetail>();

            /* Validate SparePartForeCastHeader */
            var isValid = ValidateSparePartForecast(param, out SparePartForecastDomain, validationResults);
            /* Validate SparePartForeCastDetail */
            if (isValid) { ValidateSparePartForecastDetail(param, validationResults, SparePartForecastDetailsDomain); }

            header = SparePartForecastDomain;
            detail = SparePartForecastDetailsDomain;

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Check existance
        /// </summary>
        /// <param name="DMSPRNo"></param>
        /// <returns></returns>
        private bool DMSPRNoExist(string DMSPRNo)
        {
            if (!string.IsNullOrEmpty(DMSPRNo))
            {
                var criterias = new CriteriaComposite(new Criteria(typeof(SparePartForecastHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(SparePartForecastHeader), "DMSPRNo", MatchType.Exact, DMSPRNo));
                criterias.opAnd(new Criteria(typeof(SparePartForecastHeader), "Dealer.DealerCode", MatchType.Exact, DealerCode));

                ArrayList results = _sparePartForecaastMapper.RetrieveByCriteria(criterias);
                return results.Count > 0;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Validate SparePartForecast detail
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="SparePartForecastDetails"></param>
        private bool ValidateSparePartForecastDetail(SparePartForecastParameterDto objCreate, List<DNetValidationResult> validationResults, List<SparePartForecastDetail> SparePartForecastDetails)
        {
            if (objCreate.SparePartForecastDetails.Count == 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgEmptyList, "")));
            }

            /* Validate Duplicate Param Detail */
            ValidateDuplicateParamData(objCreate.SparePartForecastDetails, validationResults);

            foreach (SparePartForecastDetailParameterDto item in objCreate.SparePartForecastDetails)
            {
                SparePartForecastDetail detail;
                if (ValidateSparePartForecastDetails(item, out detail, validationResults))
                {
                    detail.RequestDate = objCreate.PoDate;
                    SparePartForecastDetails.Add(detail);
                }
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate po
        /// </summary>
        /// <param name="paramDto"></param>
        /// <param name="entity"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidateSparePartForecast(SparePartForecastParameterDto paramDto, out SparePartForecastHeader entity, List<DNetValidationResult> validationResults)
        {
            entity = _mapper.Map<SparePartForecastHeader>(paramDto);

            /* Set Data */
            entity.RowStatus = 0;
            entity.Status = 1;
            entity.CreatedBy = DNetUserName;
            entity.CreatedTime = DateTime.Now;

            /* Validate Dealer */
            Dealer dealer = null;
            if (ValidationHelper.ValidateDealer(paramDto.DealerCode, validationResults, this.DealerCode, ref dealer, false))
            {
                entity.Dealer = dealer;
            }

            /* Validate DMSPRNo */
            if (DMSPRNoExist(paramDto.DMSPRNo))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataIsExist, FieldResource.DMSPRNo + " : " + paramDto.DMSPRNo)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate detail
        /// </summary>
        /// <param name="paramDto"></param>
        /// <param name="entity"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidateSparePartForecastDetails(SparePartForecastDetailParameterDto paramDto, out SparePartForecastDetail entity, List<DNetValidationResult> validationResults)
        {
            entity = _mapper.Map<SparePartForecastDetail>(paramDto);

            /* Set Data */
            entity.RowStatus = 0;
            entity.Status = 1;
            entity.SendDate = SqlDateTime.MinValue.Value;
            entity.CreatedBy = DNetUserName;
            entity.CreatedTime = DateTime.Now;

            /* Validate SparePartForecastMasterID / PartNumber */
            var criterias = new CriteriaComposite(new Criteria(typeof(SparePartForecastMaster), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(SparePartForecastMaster), "Status", MatchType.Exact, "0"));
            criterias.opAnd(new Criteria(typeof(SparePartForecastMaster), "SparePartMaster.PartNumber", MatchType.Exact, paramDto.PartNumber));
            var ForecastMaster = _sparePartForecastMasterMapper.RetrieveByCriteria(criterias);
            if (ForecastMaster.Count > 0)
            {
                SparePartForecastMaster spfm = (SparePartForecastMaster)ForecastMaster[0];

                /* Validate RequestQty */
                if (paramDto.RequestQty > spfm.MaxOrder || paramDto.RequestQty > spfm.Stock)
                {
                    string msgVal = string.Format("RequestQty '{0}' harus kurang dari sama dengan MaxOrder '{1}' atau RequestQty '{0}' harus kurang dari sama dengan  Stock '{2}'.", paramDto.RequestQty, spfm.MaxOrder, spfm.Stock);
                    validationResults.Add(new DNetValidationResult(msgVal));
                }
                else if (paramDto.RequestQty <= 0)
                {
                    validationResults.Add(new DNetValidationResult("RequestQty minimal 1"));
                }

                var SparepartMasters = _sparePartMasterMapper.Retrieve(spfm.SparePartMaster.ID);
                if (SparepartMasters != null)
                {
                    SparePartMaster spm = (SparePartMaster)SparepartMasters;
                    entity.SparePartForecastMaster = spfm;
                    entity.SparePartForecastMaster.SparePartMaster = spm;
                }
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSparepartNotFound, paramDto.PartNumber)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Handler on executed insert command with transaction manager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void InsertWithTransactionManagerHandler(object sender, TransactionManager.OnInsertArgs args)
        {
            // set the object ID from db returned id
            if (args.DomainObject.GetType() == typeof(SparePartForecastHeader))
            {
                ((SparePartForecastHeader)args.DomainObject).ID = args.ID;
                ((SparePartForecastHeader)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(SparePartForecastDetail))
            {
                ((SparePartForecastDetail)args.DomainObject).ID = args.ID;
                ((SparePartForecastDetail)args.DomainObject).MarkLoaded();
            }
        }

        /// <summary>
        /// Insert Spare Part PO with transaction manager
        /// </summary>
        /// <param name="spare part POCustomer"></param>
        /// <returns></returns>
        private SparePartForecastHeader InsertWithTransactionManager(SparePartForecastHeader SparePartForecast, List<SparePartForecastDetail> SparePartForecastDetails)
        {
            SparePartForecastHeader result = null;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // add command to insert spare part PO
                    this._transactionManager.AddInsert(SparePartForecast, DNetUserName);

                    // add command to insert spare part PO detail
                    foreach (SparePartForecastDetail item in SparePartForecastDetails)
                    {
                        item.SparePartForecastHeader = SparePartForecast;
                        this._transactionManager.AddInsert(item, DNetUserName);
                    }

                    this._transactionManager.PerformTransaction();
                    result = SparePartForecast;
                }
                catch (SqlException sqlException)
                {
                    ExceptionDispatchInfo.Capture(sqlException).Throw();
                }
                catch (Exception ex)
                {
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }
                finally
                {
                    this.RemoveTaskLocking();
                }
            }

            return result;
        }

        /// <summary>
        /// Update spare part PO with transaction manager
        /// </summary>
        /// <param name="objDomain"></param>
        /// <returns></returns>
        private SparePartForecastHeader UpdateWithTransactionManager(SparePartForecastHeader SparePartForecast, ArrayList existingDetails, ArrayList newDetails)
        {
            // set default result
            SparePartForecastHeader result = null;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // add command to insert spare part PO detail
                    foreach (SparePartForecastDetail item in newDetails)
                    {
                        item.SparePartForecastHeader = SparePartForecast;
                        this._transactionManager.AddInsert(item, DNetUserName);
                    }
                    foreach (SparePartForecastDetail item in existingDetails)
                    {
                        this._transactionManager.AddUpdate(item, DNetUserName);
                    }

                    // add command to update spare part PO
                    _transactionManager.AddUpdate(SparePartForecast, DNetUserName);

                    _transactionManager.PerformTransaction();
                    result = SparePartForecast;
                }
                catch (Exception ex)
                {
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }
                finally
                {
                    this.RemoveTaskLocking();
                }
            }

            return result;
        }

        private bool ValidateDuplicateParamData(List<SparePartForecastDetailParameterDto> lstObjCreate, List<DNetValidationResult> validationResults)
        {
            foreach (var item in lstObjCreate)
            {
                var lst = lstObjCreate.Where(x => x.PartNumber.Trim() == item.PartNumber.Trim());
                if (lst.Count() > 1)
                {
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDuplicateData) + string.Format(" ({0})", "PartNumber = " + item.PartNumber));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }
            return validationResults.Count == 0;
        }
        #endregion
    }
}

