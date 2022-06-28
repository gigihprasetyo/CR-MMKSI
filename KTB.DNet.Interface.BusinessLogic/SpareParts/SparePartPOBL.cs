#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartPO business logic class
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
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ExceptionServices;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class SparePartPOBL : AbstractBusinessLogic, ISparePartPOBL
    {
        #region Variables
        private readonly IMapper _sparepartpoMapper;
        private readonly IMapper _termofpaymentMapper;
        private readonly IMapper _sparePartMasterMapper;
        private readonly IMapper _pqrHeaderMapper;
        private readonly AutoMapper.IMapper _mapper;
        private TransactionManager _transactionManager;
        #endregion

        #region Constructor
        public SparePartPOBL()
        {
            _termofpaymentMapper = MapperFactory.GetInstance().GetMapper(typeof(TermOfPayment).ToString());
            _sparepartpoMapper = MapperFactory.GetInstance().GetMapper(typeof(SparePartPO).ToString());
            _sparePartMasterMapper = MapperFactory.GetInstance().GetMapper(typeof(SparePartMaster).ToString());
            _pqrHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(PQRHeader).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _transactionManager = new TransactionManager();
            _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get SparePartPO by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<SparePartPOResponse>> Read(SparePartPOFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(SparePartPO), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(SparePartPO), "Dealer.DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<SparePartPOResponse>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(SparePartPO), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(SparePartPO), filterDto, sortColl);

                // get data
                var data = _sparepartpoMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<SparePartPO>().ToList();
                    var listData = list.Select(item => _mapper.Map<SparePartPOResponse>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SparePartPO), filterDto);
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
        /// Read SparePartPO Data
        /// </summary>
        /// <param name="filterDto">Filter Parameter</param>
        /// <param name="pageSize">Page Size</param>
        /// <returns></returns>
        public ResponseBase<List<SparePartPODto>> ReadData(SparePartPOFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(SparePartPO), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(SparePartPO), "Dealer.DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<SparePartPODto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(SparePartPO), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(SparePartPO), filterDto, sortColl);

                var data = _sparepartpoMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<SparePartPO>().ToList();
                    var listData = new List<SparePartPODto>();
                    foreach (var item in list)
                    {
                        // map it
                        var sparepartpoDto = _mapper.Map<SparePartPODto>(item);

                        // add to list
                        listData.Add(sparepartpoDto);
                    };

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SparePartPO), filterDto);
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
        /// Read Spare Part PO for Others Order Type with null DMSPRNo
        /// </summary>
        /// <param name="filterDto">Filter Parameter</param>
        /// <param name="pageSize">Page Size</param>
        /// <returns></returns>
        public ResponseBase<List<SparePartPOOtherDto>> ReadPOOthers(SparePartPOFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(SparePartPO), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(SparePartPO), "Dealer.DealerCode", MatchType.Exact, DealerCode));
            //criterias.opAnd(new Criteria(typeof(SparePartPO), "DMSPRNo", MatchType.IsNull, "null"));
            criterias.opAnd(new Criteria(typeof(SparePartPO), "OrderType", MatchType.Exact, "Y"), "(", true);
            criterias.opOr(new Criteria(typeof(SparePartPO), "OrderType", MatchType.Exact, "Z"), ")", false);
            var result = new ResponseBase<List<SparePartPOOtherDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(SparePartPO), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(SparePartPO), filterDto, sortColl);

                var data = _sparepartpoMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<SparePartPO>().ToList();
                    var listData = new List<SparePartPOOtherDto>();
                    foreach (var item in list)
                    {
                        // map it
                        var sparepartpoDto = _mapper.Map<SparePartPOOtherDto>(item);
                        sparepartpoDto.Details = new List<SparePartPOOtherDetailDto>();
                        foreach (var detail in item.SparePartPODetails)
                        {
                            var detailDto = _mapper.Map<SparePartPOOtherDetailDto>(detail);
                            sparepartpoDto.Details.Add(detailDto);
                        }

                        // add to list
                        listData.Add(sparepartpoDto);
                    };

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SparePartPO), filterDto);
                }

                result.success = true;

            }
            catch (SqlException ex)
            {
                ErrorMsgHelper.SqlException(result.messages, ex.Message);
                return result;
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
                return result;
            }

            return result;
        }

        /// <summary>
        /// Delete SparePartPO by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<SparePartPOResponse> Delete(int id)
        {
            var result = new ResponseBase<SparePartPOResponse>();

            try
            {
                var sparepartpo = (SparePartPO)_sparepartpoMapper.Retrieve(id);
                if (sparepartpo != null)
                {
                    sparepartpo.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _sparepartpoMapper.Update(sparepartpo, DNetUserName);
                    if (nResult != 0)
                    {
                        result.success = true;
                        result._id = id;
                        result.total = 1;
                    }
                    else
                    {
                        ErrorMsgHelper.ErrorMsgDBSave(result.messages);
                    }
                }
                else
                {
                    ErrorMsgHelper.DeleteNotAvailable(result.messages);
                }
            }
            catch (SqlException ex)
            {
                ErrorMsgHelper.SqlException(result.messages, ex.Message);
                return result;
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
                return result;
            }

            return result;
        }

        /// <summary>
        /// Create a new SparePartPO
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<SparePartPOResponse> Create(SparePartPOParameterDto objCreate)
        {
            #region Initialized
            var result = new ResponseBase<SparePartPOResponse>();
            var validationResults = new List<DNetValidationResult>();
            SparePartPO sparepartPODomain;
            PQRHeader pqrheader = null ;
            List<SparePartPODetail> sparepartPODetails = new List<SparePartPODetail>();
            var isValidTotal = true;
            var total = 0;
            #endregion

            // validate sparepart
            var isValid = ValidateSparePartPO(objCreate, out sparepartPODomain, validationResults);
            isValidTotal = ValidateTotalErrMsg(validationResults, ref total);

            if (isValidTotal) { ValidateSparePartDetail(objCreate, validationResults, sparepartPODetails); }

            isValidTotal = ValidateTotalErrMsg(validationResults, ref total);

            // validate if TOP
            if (isValidTotal) { ValidateTOP(objCreate, ref sparepartPODomain, validationResults, sparepartPODetails); }

            isValidTotal = ValidateTotalErrMsg(validationResults, ref total);

            if(objCreate.OrderType=="P")
            {
                if (isValidTotal) { ValidatePQRHeader(objCreate.DealerCode, objCreate.PQRNo, validationResults, ref pqrheader); }
                sparepartPODomain.PQRHeader = pqrheader;
                isValidTotal = ValidateTotalErrMsg(validationResults, ref total);
            }

            isValid = validationResults.Count == 0;

            if (isValid)
            {
                try
                {
                    // based on app config
                    bool isAutoSendToSAP = Helper.IsAutoSendToSAP(_mapper);
                    sparepartPODomain.ProcessCode = isAutoSendToSAP ? "S" : string.Empty;

                    var createdObject = InsertWithTransactionManager(sparepartPODomain, sparepartPODetails);
                    if (createdObject != null)
                    {
                        var obj = (SparePartPO)_sparepartpoMapper.Retrieve(createdObject.ID);
                        if (obj != null)
                        {
                            result._id = createdObject.ID;
                            result.lst = new SparePartPOResponse
                            {
                                PODate = obj.PODate,
                                PONumber = obj.PONumber
                            };

                            // generate file if order type is emergency
                            if (objCreate.OrderType.Equals("E", StringComparison.OrdinalIgnoreCase) && isAutoSendToSAP)
                            {
                                FileUtility.CreateTextFileForKTB(obj, true);
                            }
                        }
                    }
                    else
                    {
                        validationResults.Add(new DNetValidationResult(ErrorCode.DBSaveFailed, string.Format(MessageResource.ErrorMsgPRGUnhandle, MessageResource.ErrorMsgOnSavingPleaseContactAdmin)));
                    }
                }
                catch (Exception ex)
                {
                    validationResults.Add(new DNetValidationResult(ErrorCode.UnhandledException, string.Format(MessageResource.ErrorMsgPRGUnhandle, ex.Message)));
                }
            }

            isValid = validationResults.Count == 0;
            if (isValid)
            {
                result.success = true;
                result.total = 1;
            }
            else
            {
                return PopulateValidationError<SparePartPOResponse>(validationResults, null);
            }

            return result;
        }

        private bool ValidatePQRHeader(string DealerCode, string pQRNo, List<DNetValidationResult> validationResults, ref PQRHeader pqrheader)
        {
            var isvalid = true;
            var criterias = new CriteriaComposite(new Criteria(typeof(PQRHeader), "PQRNo", MatchType.Exact, pQRNo));
            criterias.opAnd(new Criteria(typeof(PQRHeader), "Dealer.DealerCode", MatchType.Exact, DealerCode));
            criterias.opAnd(new Criteria(typeof(PQRHeader), "RowStatus", MatchType.No, (short)DBRowStatus.Deleted));

            var pqr = _pqrHeaderMapper.RetrieveByCriteria(criterias);
            if (pqr.Count > 0)
            {
                pqrheader = pqr[0] as PQRHeader;
            }
            else
            {
                validationResults.Add(new DNetValidationResult("PQRNo "+pQRNo+" tidak ditemukan dalam data master"));
                isvalid = false;
            }
            return isvalid;
        }

        private bool ValidateTotalErrMsg(List<DNetValidationResult> validationResults, ref int total)
        {
            total = 0;
            var isvalid = true;
            foreach (var i in validationResults)
            {
                total = total + i.ErrorMessage.ToString().Count();
                if (total > 4000)
                {
                    isvalid = false;
                }
            }
            return isvalid;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<SparePartPOResponse> Update(SparePartPOParameterDto objUpdate)
        {
            return null;
        }

        /// <summary>
        /// Check existance
        /// </summary>
        /// <param name="DMSPRNo"></param>
        /// <returns></returns>
        public bool DMSPRNoExist(string DMSPRNo)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(SparePartPO), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(SparePartPO), "DMSPRNo", MatchType.Exact, DMSPRNo));

            ArrayList results = _sparepartpoMapper.RetrieveByCriteria(criterias);
            return results.Count > 0;
        }

        /// <summary>
        /// Update SparePartPO
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<SparePartPOResponse> Update(SparePartPOUpdateParameterDto objUpdate, bool isUpdateDMSPRNoOnly)
        {
            var result = new ResponseBase<SparePartPOResponse>();
            var criterias = new CriteriaComposite(new Criteria(typeof(SparePartPO), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(SparePartPO), "PONumber", MatchType.Exact, objUpdate.PONumber));
            try
            {
                var data = _sparepartpoMapper.RetrieveByCriteria(criterias);
                if (data.Count > 0)
                {
                    var sparepartPO = (SparePartPO)data[0];

                    if (isUpdateDMSPRNoOnly)
                    {
                        // update the dmsprno
                        sparepartPO.DMSPRNo = objUpdate.DMSPRNo;
                    }
                    else
                    {
                        // If not only update dmsprno need to validate the process code
                        if (!string.IsNullOrEmpty(sparepartPO.ProcessCode))
                        {
                            var poStatus = ValidationHelper.GetStandardCodeCharByValue("SparePartPOStatus", sparepartPO.ProcessCode);
                            result.messages.Add(new MessageBase { ErrorMessage = string.Format(MessageResource.ErrorMsgCannotUpdateStatusChanged, poStatus.ValueCode) });
                            return result;
                        }

                        // update sparepartPO dmsprno and other fields here
                        sparepartPO.DMSPRNo = objUpdate.DMSPRNo;
                    }

                    var success = _sparepartpoMapper.Update(sparepartPO, DNetUserName);
                    result.success = success > 0;
                    if (result.success)
                    {
                        result._id = sparepartPO.ID;
                        result.total = 1;
                        result.lst = new SparePartPOResponse
                        {
                            PODate = sparepartPO.PODate,
                            PONumber = sparepartPO.PONumber
                        };
                    }
                }
                else
                {
                    result.messages.Add(new MessageBase { ErrorCode = ErrorCode.DataUpdateNotAvailable, ErrorMessage = string.Format(MessageResource.ErrorMsgDataNotFound, FieldResource.SparePartPO) });
                }
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Validate spare part detail
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="sparepartPODetails"></param>
        private void ValidateSparePartDetail(SparePartPOParameterDto objCreate, List<DNetValidationResult> validationResults, List<SparePartPODetail> sparepartPODetails)
        {
            if (objCreate.SparePartPODetails.Count == 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgEmptyList, FieldResource.SparePartPODetails)));
            }

            foreach (SparePartPODetailParameterDto item in objCreate.SparePartPODetails)
            {
                SparePartPODetail detail;
                if (ValidateSparePartPODetail(item, out detail, validationResults))
                {
                    sparepartPODetails.Add(detail);
                }
            }
        }

        /// <summary>
        /// Validate if TOP
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="sparepartPODetails"></param>
        /// <param name="sparepartPO"></param>
        private void ValidateTOP(SparePartPOParameterDto objCreate, ref SparePartPO sparepartPO, List<DNetValidationResult> validationResults, List<SparePartPODetail> sparepartPODetails)
        {
            // validate dealer top
            bool isDealerTOP = ValidationHelper.ValidateDealerTOP(objCreate.DealerCode, _mapper);

            // get Non TOP ID
            int sparepartNonTOPID = ValidationHelper.GetSparepartTOPIdNotTOP(objCreate.OrderType);

            // if dealer non top
            if (!isDealerTOP)
            {
                var term = _termofpaymentMapper.Retrieve(sparepartNonTOPID);
                if (term != null)
                {
                    sparepartPO.TermOfPayment = term as TermOfPayment;
                }
            }
            else
            {
                // validate sparepart po type top            
                SparePartPOTypeTOP poType = ValidationHelper.GetSparepartPOTypeTOP(objCreate.OrderType);
                if (poType != null)
                {
                    // if order type is TOP
                    if (poType.IsTOP)
                    {
                        bool isSparePartDetailsValid = true;
                        List<SparePartMasterTOP> masterTOPList = new List<SparePartMasterTOP>();
                        foreach (var detail in sparepartPODetails)
                        {
                            var masterTop = ValidationHelper.GetMasterTop(detail.SparePartMaster.ID, poType.ID);
                            if (masterTop != null)
                            {
                                masterTOPList.Add(masterTop);
                            }
                            else
                            {
                                isSparePartDetailsValid = false;
                                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSparePartMasterTOPNotExist, detail.SparePartMaster.PartNumber)));
                                break;
                            }
                        }

                        if (isSparePartDetailsValid)
                        {
                            // get TOP status
                            bool isAllTop = masterTOPList.All(x => x.Status);

                            // valid if all top
                            if (!isAllTop)
                            {
                                bool isAllNonTop = masterTOPList.All(x => !x.Status);
                                if (isAllNonTop)
                                {
                                    var term = _termofpaymentMapper.Retrieve(sparepartNonTOPID);
                                    if (term != null)
                                    {
                                        sparepartPO.TermOfPayment = term as TermOfPayment;
                                    }
                                }
                                else
                                {
                                    // error if a mix between top and non top
                                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSparePartTOPMix, "SparePart")));
                                }
                            }
                        }
                    }
                }
                else
                {
                    var term = _termofpaymentMapper.Retrieve(sparepartNonTOPID);
                    if (term != null)
                    {
                        sparepartPO.TermOfPayment = term as TermOfPayment;
                    }
                }
            }
        }

        /// <summary>
        /// Validate po
        /// </summary>
        /// <param name="paramDto"></param>
        /// <param name="entity"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidateSparePartPO(SparePartPOParameterDto paramDto, out SparePartPO entity, List<DNetValidationResult> validationResults)
        {
            
            entity = _mapper.Map<SparePartPO>(paramDto);
            entity.DeliveryDate = new DateTime(1753, 1, 1);
            entity.IndentTransfer = 0;
            entity.SentPODate = new DateTime(1753, 1, 1);

            Dealer dealer = null;
            if (ValidationHelper.ValidateDealer(paramDto.DealerCode, validationResults, this.DealerCode, ref dealer, false))
            {
                entity.Dealer = dealer;
            }

            var orderTypeResult = ValidationHelper.GetStandardCodeCharByValue("SPPOOrderType.EnumOrderType", paramDto.OrderType);
            if (orderTypeResult == null)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgEnumNotFound, FieldResource.OrderType)));
            }
            else if (paramDto.OrderType.Equals("I") || paramDto.OrderType.Equals("K"))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMessageCouldNotInclude, FieldResource.OrderType, paramDto.OrderType)));
            }

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
        private bool ValidateSparePartPODetail(SparePartPODetailParameterDto paramDto, out SparePartPODetail entity, List<DNetValidationResult> validationResults)
        {
            entity = _mapper.Map<SparePartPODetail>(paramDto);
            entity.StopMark = 0;

            var sparepartMasterBL = new SparePartMasterBL(_mapper);
            //var masterResult = sparepartMasterBL.GetValidPartByPartNumber(paramDto.PartNumber);
            //if (masterResult == null)
            //{
            //    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSparepartNotFound, paramDto.PartNumber)));
            //}
            //else
            //{
            //    entity.SparePartMaster = masterResult;
            //    entity.RetailPrice = masterResult.RetalPrice;
            //}
            var masterResult = sparepartMasterBL.GetValidPartByPartNumber(paramDto.PartNumber, validationResults);

            if (masterResult != null)
            {
                entity.SparePartMaster = masterResult;
                entity.RetailPrice = masterResult.RetalPrice;
            }

            if (paramDto.Quantity < 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSparepartQuantity, FieldResource.Quantity, paramDto.PartNumber)));
            }

            if (paramDto.TotalForecast < 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSparepartQuantity, FieldResource.TotalForecast, paramDto.PartNumber)));
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
            if (args.DomainObject.GetType() == typeof(SparePartPO))
            {
                ((SparePartPO)args.DomainObject).ID = args.ID;
                ((SparePartPO)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(SparePartPODetail))
            {
                ((SparePartPODetail)args.DomainObject).ID = args.ID;
                ((SparePartPODetail)args.DomainObject).MarkLoaded();
            }
        }

        /// <summary>
        /// Insert Spare Part PO with transaction manager
        /// </summary>
        /// <param name="spare part POCustomer"></param>
        /// <returns></returns>
        private SparePartPO InsertWithTransactionManager(SparePartPO sparepartPO, List<SparePartPODetail> sparepartPODetails)
        {
            SparePartPO result = null;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // add command to insert spare part PO
                    this._transactionManager.AddInsert(sparepartPO, DNetUserName);

                    // add command to insert spare part PO detail
                    foreach (SparePartPODetail item in sparepartPODetails)
                    {
                        item.SparePartPO = sparepartPO;
                        this._transactionManager.AddInsert(item, DNetUserName);
                    }

                    this._transactionManager.PerformTransaction();
                    result = sparepartPO;
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
        private SparePartPO UpdateWithTransactionManager(SparePartPO sparepartPO, ArrayList existingDetails, ArrayList newDetails)
        {
            // set default result
            SparePartPO result = null;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // add command to insert spare part PO detail
                    foreach (SparePartPODetail item in newDetails)
                    {
                        item.SparePartPO = sparepartPO;
                        this._transactionManager.AddInsert(item, DNetUserName);
                    }
                    foreach (SparePartPODetail item in existingDetails)
                    {
                        this._transactionManager.AddUpdate(item, DNetUserName);
                    }

                    // add command to update spare part PO
                    _transactionManager.AddUpdate(sparepartPO, DNetUserName);

                    _transactionManager.PerformTransaction();
                    result = sparepartPO;
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
        #endregion
    }
}

