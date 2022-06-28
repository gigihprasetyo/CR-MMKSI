#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ndentPartHeader business logic class
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
    public class IndentPartHeaderBL : AbstractBusinessLogic, IIndentPartHeaderBL
    {
        #region Variables
        private readonly IMapper _indentPartHeaderMapper;
        private readonly IMapper _indentPartDetailMapper;
        private readonly AutoMapper.IMapper _mapper;
        private TransactionManager _transactionManager;
        private StandardCodeBL _enumBL;
        private IMapper _termofpaymentMapper;
        #endregion

        #region Constructor
        public IndentPartHeaderBL()
        {
            _termofpaymentMapper = MapperFactory.GetInstance().GetMapper(typeof(TermOfPayment).ToString());
            _indentPartHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(IndentPartHeader).ToString());
            _indentPartDetailMapper = MapperFactory.GetInstance().GetMapper(typeof(IndentPartDetail).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _transactionManager = new TransactionManager();
            _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);
            _enumBL = new StandardCodeBL(_mapper);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get IndentPartHeader by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<IndentPartHeaderDto>> Read(IndentPartHeaderFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(IndentPartHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(IndentPartHeader), "Dealer.DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<IndentPartHeaderDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(IndentPartHeader), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(IndentPartHeader), filterDto, sortColl);

                // get data
                var data = _indentPartHeaderMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<IndentPartHeader>().ToList();
                    var listData = list.Select(item => _mapper.Map<IndentPartHeaderDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(IndentPartHeader), filterDto);
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
        /// Delete IndentPartHeader by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<IndentPartHeaderDto> Delete(int id)
        {
            var result = new ResponseBase<IndentPartHeaderDto>();

            try
            {
                var indentpartheader = (IndentPartHeader)_indentPartHeaderMapper.Retrieve(id);
                if (indentpartheader != null)
                {
                    indentpartheader.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _indentPartHeaderMapper.Update(indentpartheader, DNetUserName);
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
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Create a new IndentPartHeader
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<IndentPartHeaderCreateResponseDto> Create(IndentPartHeaderCreateParameterDto param)
        {
            // set default response
            var result = new ResponseBase<IndentPartHeaderCreateResponseDto>();
            var objCreate = new IndentPartHeaderParameterDto();

            IndentPartHeader indentPartHeader = null;

            try
            {
                List<DNetValidationResult> validationResults = new List<DNetValidationResult>();
                List<IndentPartDetail> indentPartDetailList = new List<IndentPartDetail>();

                objCreate = _mapper.Map<IndentPartHeaderParameterDto>(param);
                if (param.IndentPartDetail.Count > 0)
                {
                    var listDetail = param.IndentPartDetail.Cast<IndentPartDetailCreateParameterDto>().ToList();
                    objCreate.IndentPartDetail = _mapper.Map<IList<IndentPartDetailCreateParameterDto>, IList<IndentPartDetailParameterDto>>(listDetail).ToList();
                }

                // validate duplicate DMSPRNO if any
                if (!string.IsNullOrEmpty(objCreate.DMSPRNo))
                {
                    if (!ValidationHelper.ValidateIndentPartHeader(objCreate.DMSPRNo, validationResults))
                        return PopulateValidationError<IndentPartHeaderCreateResponseDto>(validationResults, null);
                }

                // validate indent part
                objCreate.DealerCode = param.DealerCode;
                indentPartHeader = GetValidInsertIndentPartObject(objCreate, indentPartDetailList, validationResults);
                if (validationResults.Any())
                {
                    return PopulateValidationError<IndentPartHeaderCreateResponseDto>(validationResults, null);
                }

                // validate TOP
                ValidateTOP(objCreate.DealerCode, ref indentPartHeader, validationResults, indentPartDetailList);
                if (validationResults.Any())
                {
                    return PopulateValidationError<IndentPartHeaderCreateResponseDto>(validationResults, null);
                }

                int insertedID = InsertWithTransactionManager(indentPartHeader, indentPartDetailList);
                if (insertedID > 0)
                {
                    result.success = true;
                    result._id = insertedID;
                    result.total = 1;

                    IndentPartHeader ipOnDB = (IndentPartHeader)_indentPartHeaderMapper.Retrieve(indentPartHeader.ID);
                    if (ipOnDB != null)
                    {
                        result.lst = new IndentPartHeaderCreateResponseDto();
                        result.lst.RequestDate = ipOnDB.RequestDate;
                        result.lst.RequestNo = ipOnDB.RequestNo;
                    }
                }
                else
                {
                    ErrorMsgHelper.ErrorMsgDBSaveContactAdmin(result.messages);
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
        /// Update IndentPartHeader
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<IndentPartHeaderDto> Update(IndentPartHeaderUpdateParameterDto param)
        {
            // set default response
            var result = new ResponseBase<IndentPartHeaderDto>();
            var objUpdate = new IndentPartHeaderParameterDto();

            IndentPartHeader domainiPH = null;

            try
            {
                List<DNetValidationResult> validationResults = new List<DNetValidationResult>();
                List<IndentPartDetail> listOfDomainIndentPartDetail = new List<IndentPartDetail>();
                objUpdate = _mapper.Map<IndentPartHeaderParameterDto>(param);
                if (param.IndentPartDetail.Count > 0)
                {
                    objUpdate.IndentPartDetail = param.IndentPartDetail;
                }

                IndentPartHeader ipOnDB = null;
                // Get Haeader
                if (!string.IsNullOrEmpty(objUpdate.RequestNo.Trim()))
                {
                    ArrayList arr = _indentPartHeaderMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(IndentPartHeader), FieldResource.RequestNo, objUpdate.RequestNo));
                    if (arr.Count > 0)
                    {
                        ipOnDB = arr[0] as IndentPartHeader;
                    }
                }
                else if (objUpdate.ID > 0)
                {
                    ipOnDB = (IndentPartHeader)_indentPartHeaderMapper.Retrieve(objUpdate.ID);
                }

                if (ipOnDB == null)
                {
                    validationResults.Add(new DNetValidationResult(ErrorCode.DataUpdateNotAvailable, MessageResource.ErrorMsgDataUpdateNotAvailable));
                    return PopulateValidationError<IndentPartHeaderDto>(validationResults, null);
                }

                // set value
                objUpdate.RequestNo = ipOnDB.RequestNo;
                objUpdate.RequestDate = ipOnDB.RequestDate;
                objUpdate.KTBConfirmedDate = ipOnDB.KTBConfirmedDate;

                // get valid value
                domainiPH = GetValidUpdateIndentPartObject(objUpdate, ipOnDB, out listOfDomainIndentPartDetail, out validationResults);

                if (validationResults.Any())
                {
                    return PopulateValidationError<IndentPartHeaderDto>(validationResults, null);
                }

                int updateResult = UpdateWithTransactionManager(domainiPH, listOfDomainIndentPartDetail);
                if (updateResult > 0)
                {
                    IndentPartHeaderDto responseModel = _mapper.Map<IndentPartHeaderDto>(domainiPH);
                    result.success = true;
                    result._id = responseModel.ID;
                    result.total = 1;
                }
                else
                {
                    ErrorMsgHelper.ErrorMsgDBSave(result.messages, FieldResource.IndentPartHeader);
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

        public ResponseBase<IndentPartHeaderDto> Create(IndentPartHeaderParameterDto objCreate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<IndentPartHeaderDto> Update(IndentPartHeaderParameterDto objUpdate)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region private method
        /// <summary>
        /// Validate Indent Part TOP
        /// </summary>
        /// <param name="indentPartHeader"></param>
        /// <param name="validationResults"></param>
        /// <param name="indentPartDetails"></param>
        private void ValidateTOP(string dealerCode, ref IndentPartHeader indentPartHeader, List<DNetValidationResult> validationResults, List<IndentPartDetail> indentPartDetails)
        {
            // validate dealer top
            bool isDealerTOP = ValidationHelper.ValidateDealerTOP(dealerCode, _mapper);

            // get Non TOP ID
            int sparepartNonTOPID = ValidationHelper.GetSparepartTOPIdNotTOP("I");

            // if dealer non top
            if (!isDealerTOP)
            {
                var term = _termofpaymentMapper.Retrieve(sparepartNonTOPID);
                if (term != null)
                {
                    indentPartHeader.TermOfPayment = term as TermOfPayment;
                }
            }
            else
            {
                // validate sparepart po type top            
                SparePartPOTypeTOP poType = ValidationHelper.GetSparepartPOTypeTOP("I");
                if (poType != null)
                {
                    // if order type is TOP
                    if (poType.IsTOP)
                    {
                        bool isSparePartDetailsValid = true;
                        List<SparePartMasterTOP> masterTOPList = new List<SparePartMasterTOP>();
                        foreach (var detail in indentPartDetails)
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
                                        indentPartHeader.TermOfPayment = term as TermOfPayment;
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
                    else
                    {
                        var term = _termofpaymentMapper.Retrieve(sparepartNonTOPID);
                        if (term != null)
                        {
                            indentPartHeader.TermOfPayment = term as TermOfPayment;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Insert via trans manager
        /// </summary>
        /// <param name="indentPart"></param>
        /// <param name="indentPartDetails"></param>
        /// <returns></returns>
        private int InsertWithTransactionManager(IndentPartHeader indentPart, List<IndentPartDetail> indentPartDetails)
        {
            int result = -1;
            ;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // add command to insert indentPart
                    this._transactionManager.AddInsert(indentPart, DNetUserName);

                    // add command to insert indentPart detail
                    foreach (IndentPartDetail item in indentPartDetails)
                    {
                        this._transactionManager.AddInsert(item, DNetUserName);
                    }

                    this._transactionManager.PerformTransaction();
                    result = indentPart.ID;
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
        /// Trans manager handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void InsertWithTransactionManagerHandler(object sender, TransactionManager.OnInsertArgs args)
        {
            // set the object ID from db returned id
            if (args.DomainObject.GetType() == typeof(IndentPartHeader))
            {
                ((IndentPartHeader)args.DomainObject).ID = args.ID;
                ((IndentPartHeader)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(IndentPartDetail))
            {
                ((IndentPartDetail)args.DomainObject).ID = args.ID;
                ((IndentPartDetail)args.DomainObject).MarkLoaded();
            }
        }

        /// <summary>
        /// Update indentPart with transaction manager
        /// </summary>
        /// <param name="objDomain"></param>
        /// <returns></returns>
        private int UpdateWithTransactionManager(IndentPartHeader indentPart, List<IndentPartDetail> indentPartDetails)
        {
            // set default result
            int result = -1;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // add command to insert indentPart detail
                    foreach (IndentPartDetail detail in indentPartDetails)
                    {
                        if (detail.ID != 0)
                        {
                            _transactionManager.AddUpdate(detail, DNetUserName);
                        }
                        else
                        {
                            _transactionManager.AddInsert(detail, DNetUserName);
                        }

                        detail.MarkLoaded();
                    }

                    // add command to update indentPart
                    _transactionManager.AddUpdate(indentPart, DNetUserName);
                    _transactionManager.PerformTransaction();
                    result = indentPart.ID;
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
        /// Set object for insert
        /// </summary>
        /// <param name="indentPart"></param>
        /// <param name="domainIPDetailList"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private IndentPartHeader GetValidInsertIndentPartObject(IndentPartHeaderParameterDto indentPart, List<IndentPartDetail> domainIPDetailList, List<DNetValidationResult> validationResults)
        {
            IndentPartHeader domainIndentPart = null;

            #region Set default value
            indentPart.RequestNo = string.Empty;
            indentPart.RequestDate = DateTime.Now;
            // based on app config
            if (Helper.IsAutoSendToSAP(_mapper))
            {
                indentPart.StatusKTB = 1;
                indentPart.Status = (byte)_enumBL.GetByCategoryAndCode("IndentPartStatusDealer", "Kirim").ValueId;
            }
            else
            {
                indentPart.StatusKTB = 0;
                indentPart.Status = (byte)_enumBL.GetByCategoryAndCode("IndentPartStatusDealer", "Baru").ValueId;
            }

            indentPart.SubmitFile = null;
            indentPart.PaymentType = 0;
            indentPart.Price = 0;
            #endregion

            #region Validate Dealer
            Dealer dealer = null;
            if (ValidationHelper.ValidateDealer(indentPart.DealerCode, validationResults, this.DealerCode, ref dealer))
            {
                indentPart.DealerID = dealer.ID;
            }
            #endregion

            #region Validate desc and chassis number
            validationResults.AddRange(ValidateIndentDesc(indentPart));
            if (validationResults.Any())
            {
                return domainIndentPart;
            }
            #endregion

            domainIndentPart = _mapper.Map<IndentPartHeader>(indentPart);
            domainIndentPart.Dealer = dealer;

            #region Validate Indent Part Detail
            IndentPartDetailBL partDetailBL = new IndentPartDetailBL(_mapper);
            List<int> listOfDetailIDExistsOnDB = new List<int>();
            List<IndentPartDetail> listOfDetailOnDB = domainIndentPart.IndentPartDetails.Cast<IndentPartDetail>().ToList();
            // validate every indentPart detail on parameter input
            foreach (IndentPartDetailParameterDto detail in indentPart.IndentPartDetail)
            {
                IndentPartDetail domainIndentPartDetail;
                if (detail.IndentPartHeaderID == indentPart.ID)
                {
                    validationResults.AddRange(partDetailBL.ValidateCreateParameterDto(detail, out domainIndentPartDetail));
                    if (domainIndentPartDetail != null)
                    {
                        domainIndentPartDetail.IndentPartHeader = domainIndentPart;

                        #region merge with detail on db
                        if (listOfDetailOnDB.Any(d => d.ID == domainIndentPartDetail.ID))
                        {
                            listOfDetailIDExistsOnDB.Add(domainIndentPartDetail.ID);
                            domainIPDetailList.Add(domainIndentPartDetail);
                        }
                        else
                        {
                            if (domainIndentPartDetail.ID != 0)
                            {
                                validationResults.Add(new DNetValidationResult(MessageResource.ErrorMsgDataUpdateNotAvailable + string.Format(FieldResource.IndentPartDetail, MessageResource.ErrorMsgDataNotFound, domainIndentPartDetail.ID)));
                            }
                            else
                            {
                                domainIPDetailList.Add(domainIndentPartDetail);
                            }
                        }
                        #endregion

                        if (domainIndentPartDetail.SparePartMaster != null)
                        {
                            // validate part
                            if (!IsValidatePart(domainIndentPartDetail.SparePartMaster.TypeCode, domainIndentPart.MaterialType))
                            {
                                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNoBarangNotMatch, domainIndentPartDetail.SparePartMaster.PartNumber)));
                            }
                        }
                    }
                }
                else
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataNotMatch, FieldResource.IndentPartHeader)));
                }
            }

            if (validationResults.Any())
            {
                return domainIndentPart;
            }

            domainIPDetailList.AddRange(listOfDetailOnDB.Where(d => !listOfDetailIDExistsOnDB.Contains(d.ID)));
            #endregion

            return domainIndentPart;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indentPart"></param>
        /// <param name="domainIPDetailList"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private IndentPartHeader GetValidUpdateIndentPartObject(IndentPartHeaderParameterDto indentPart, IndentPartHeader indentPartOnDB, out List<IndentPartDetail> domainIPDetailList, out List<DNetValidationResult> validationResults)
        {
            IndentPartHeader domainIndentPart = null;
            domainIPDetailList = new List<IndentPartDetail>();
            validationResults = new List<DNetValidationResult>();

            #region Validate desc and chassis number
            validationResults.AddRange(ValidateIndentDesc(indentPart));
            if (validationResults.Any())
            {
                return domainIndentPart;
            }
            #endregion

            #region Validate Dealer
            Dealer dealer = null;
            if (ValidationHelper.ValidateDealer(indentPart.DealerCode, validationResults, this.DealerCode, ref dealer))
            {
                indentPart.DealerID = dealer.ID;
            }

            if (validationResults.Any())
            {
                return domainIndentPart;
            }
            #endregion

            #region Validate Status changes
            List<StandardCodeDto> dealerStatusList = _enumBL.GetByCategory("IndentPartStatusDealer");
            List<StandardCodeDto> kTBStatusList = _enumBL.GetByCategory("IndentPartStatusKTB");

            // if current status > than kirim can not changed to Batal                
            if (indentPart.Status == (byte)dealerStatusList.Where(e => e.ValueCode == "Batal").FirstOrDefault().ValueId)
            {
                if (indentPartOnDB.Status > (byte)dealerStatusList.Where(e => e.ValueCode == "Kirim").FirstOrDefault().ValueId)
                {
                    validationResults.Add(new DNetValidationResult(FieldResource.StatusPart + indentPartOnDB.StatusDealerDesc + MessageResource.ErrorMsgStatusCannotCancel));
                }
                else
                {
                    // set Status KTB
                    indentPart.StatusKTB = (byte)kTBStatusList.Where(e => e.ValueCode == "BelumValidasi").FirstOrDefault().ValueId;
                }
            }

            // if current status > than kirim can not changed to Kirim                
            if (indentPart.Status == (byte)dealerStatusList.Where(e => e.ValueCode == "Kirim").FirstOrDefault().ValueId)
            {
                if (indentPartOnDB.Status > (byte)dealerStatusList.Where(e => e.ValueCode == "Kirim").FirstOrDefault().ValueId)
                {
                    validationResults.Add(new DNetValidationResult(FieldResource.StatusPart + indentPartOnDB.StatusDealerDesc + MessageResource.ErrorMsgStatusChangeToSend));
                }
                else
                {
                    // set Status KTB
                    indentPart.StatusKTB = (byte)kTBStatusList.Where(e => e.ValueCode == "Baru").FirstOrDefault().ValueId;
                }
            }

            if (validationResults.Any())
            {
                return domainIndentPart;
            }
            #endregion

            domainIndentPart = _mapper.Map<IndentPartHeader>(indentPart);
            domainIndentPart.Dealer = dealer;

            #region Validate Indent Part Detail
            IndentPartDetailBL partDetailBL = new IndentPartDetailBL(_mapper);
            List<int> listOfDetailIDExistsOnDB = new List<int>();
            List<IndentPartDetail> listOfDetailOnDB = indentPartOnDB.IndentPartDetails.Cast<IndentPartDetail>().ToList();
            // validate every indentPart detail on parameter input
            foreach (IndentPartDetailParameterDto detail in indentPart.IndentPartDetail)
            {
                IndentPartDetail domainIndentPartDetail;
                if (detail.IndentPartHeaderID == indentPart.ID)
                {
                    validationResults.AddRange(partDetailBL.ValidateCreateParameterDto(detail, out domainIndentPartDetail));
                    if (domainIndentPartDetail != null)
                    {
                        domainIndentPartDetail.IndentPartHeader = domainIndentPart;
                        domainIPDetailList.Add(domainIndentPartDetail);

                        // validate part
                        if (!IsValidatePart(domainIndentPartDetail.SparePartMaster.TypeCode, domainIndentPart.MaterialType))
                        {
                            validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotMatch, FieldResource.MaterialType, FieldResource.PartNumber)));
                        }
                    }
                }
                else
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrence, FieldResource.IndentPartHeader, ValidationResource.ValidationIndentPartListOf, string.Empty)));
                }
            }
            #endregion

            return domainIndentPart;
        }

        /// <summary>
        /// validate indent desc and chasiss master
        /// </summary>
        /// <param name="indentPart"></param>
        /// <returns></returns>
        private List<DNetValidationResult> ValidateIndentDesc(IndentPartHeaderParameterDto indentPart)
        {
            List<DNetValidationResult> validationResults = new List<DNetValidationResult>();
            ChassisMaster chassisMaster = null;

            // validate Description
            List<StandardCodeDto> _enumIndentDesc = new List<StandardCodeDto>();
            _enumIndentDesc = _enumBL.GetByCategory("IndentDesc");

            // check is inentdesc valid
            if (!_enumBL.IsExistByCategoryAndValue("IndentDesc", indentPart.DescID.ToString()))
            {
                validationResults.Add(new DNetValidationResult(FieldResource.DescID + " enum " + string.Format(MessageResource.ErrorMsgDataInvalid, indentPart.DescID)));
            }
            else
            {
                if (indentPart.DescID == (_enumIndentDesc.Where(e => e.ValueCode == "Silakan_Pilih").SingleOrDefault().ValueId))
                {
                    validationResults.Add(new DNetValidationResult(FieldResource.DescID + " enum " + string.Format(MessageResource.ErrorMsgDataInvalid, indentPart.DescID + "(Silakan_Pilih)")));
                }
                else if (indentPart.DescID == (_enumIndentDesc.Where(e => e.ValueCode == "Pasang_or_Stamping").SingleOrDefault().ValueId))
                {
                    if (string.IsNullOrEmpty(indentPart.ChassisNumber.Trim()))
                    {
                        //check if materialtype valid
                        if (_enumBL.IsExistByCategoryAndValue("MaterialType", indentPart.MaterialType.ToString()))
                        {
                            // if desc is Pasang or stamping and Material Type accessories, chassis number is required
                            if ((indentPart.MaterialType == _enumBL.GetByCategoryAndCode("MaterialType", "Accessories").ValueId))
                            {
                                validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.ChassisNumber + ValidationResource.For + FieldResource.MaterialType + " " + indentPart.MaterialType + "." + ValidationResource.Accessories + FieldResource.DescID + " " + indentPart.DescID + "." + ValidationResource.Put)));
                            }
                        }
                        else
                        {
                            validationResults.Add(new DNetValidationResult(FieldResource.MaterialType + " enum " + string.Format(MessageResource.ErrorMsgDataInvalid, indentPart.MaterialType)));
                        }
                    }
                    else
                    {
                        ValidationHelper.ValidateChassisMaster(indentPart.ChassisNumber, validationResults, ref chassisMaster);
                    }
                }
                else if ((indentPart.DescID == (_enumIndentDesc.Where(e => e.ValueCode == "Kirim").SingleOrDefault().ValueId)) && (!string.IsNullOrEmpty(indentPart.ChassisNumber.Trim())))
                {
                    ValidationHelper.ValidateChassisMaster(indentPart.ChassisNumber, validationResults, ref chassisMaster);
                }
            }

            return validationResults;
        }

        /// <summary>
        /// validate Part
        /// </summary>
        /// <param name="typeCode"></param>
        /// <param name="materialType"></param>
        /// <returns></returns>
        private bool IsValidatePart(string typeCode, int materialType)
        {
            bool result = false;
            string tmpCode = string.Empty;

            switch (materialType)
            {
                case 1:
                    tmpCode = "I";
                    break;
                case 2:
                    tmpCode = "E";
                    break;
                case 3:
                    tmpCode = "A";
                    break;
            }

            if (tmpCode == typeCode) { result = true; }

            return result;
        }
        #endregion
    }
}

