#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AssistPartStock business logic class
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
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class AssistPartStockBL : AbstractBusinessLogic, IAssistPartStockBL
    {
        #region Variables
        private readonly IMapper _assistpartstockMapper;
        private readonly AutoMapper.IMapper _mapper;
        private TransactionManager _transactionManager;
        private SparePartMasterBL _spareparmastertBL;
        ISparePartMasterRepository<SparePartMaster, int> _sparePartMasterRepository;
        IDealerBranchRepository<DealerBranch, int> _dealerBranchRepository;
        IAssistPartStockRepository<AssistPartStock, int> _assistPartStockRepository;
        #endregion

        #region Constructor
        public AssistPartStockBL(ISparePartMasterRepository<SparePartMaster, int> sparePartMasterRepository,
            IDealerBranchRepository<DealerBranch, int> dealerBranchRepository, IAssistPartStockRepository<AssistPartStock, int> assistPartStockRepository)
        {
            _assistpartstockMapper = MapperFactory.GetInstance().GetMapper(typeof(AssistPartStock).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _spareparmastertBL = new SparePartMasterBL(_mapper);
            _transactionManager = new TransactionManager();
            _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);

            _sparePartMasterRepository = sparePartMasterRepository;
            _dealerBranchRepository = dealerBranchRepository;
            _assistPartStockRepository = assistPartStockRepository;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get AssistPartStock by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<AssistPartStockDto>> Read(AssistPartStockFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(AssistPartStock), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(AssistPartStock), "DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<AssistPartStockDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(AssistPartStock), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(AssistPartStock), filterDto, sortColl);

                // get data
                var data = _assistpartstockMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<AssistPartStock>().ToList();
                    var listData = list.Select(item => _mapper.Map<AssistPartStockDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(AssistPartStock), filterDto);
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
        /// Delete AssistPartStock by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<AssistPartStockDto> Delete(int id)
        {
            var result = new ResponseBase<AssistPartStockDto>();

            try
            {
                var assistpartstock = (AssistPartStock)_assistpartstockMapper.Retrieve(id);
                if (assistpartstock != null)
                {
                    assistpartstock.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _assistpartstockMapper.Update(assistpartstock, DNetUserName);
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
        /// Create a new AssistPartStock
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<AssistPartStockDto> Create(AssistPartStockParameterDto objCreate)
        {
            // initialization
            var result = new ResponseBase<AssistPartStockDto>();
            var validationResults = new List<DNetValidationResult>();

            try
            {
                // parse the object
                AssistPartStock partStock = _mapper.Map<AssistPartStock>(objCreate);
                ValidateDuplicateData(new List<AssistPartStockParameterDto>() { objCreate }, validationResults, true);

                if (validationResults.Any())
                {
                    return PopulateValidationError<AssistPartStockDto>(validationResults, null);
                }

                // validate parameter values
                var isValid = ValidateAssistPartStock(objCreate, partStock, validationResults);
                if (isValid)
                {
                    var id = (int)_assistpartstockMapper.Insert(partStock, DNetUserName);
                    result.success = id > 0;
                    if (!result.success) ErrorMsgHelper.DataCorrupt(result.messages);
                    result._id = id;
                    result.total = 1;
                }
                else
                {
                    return PopulateValidationError<AssistPartStockDto>(validationResults, null);
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
        /// Create a new AssistPartStock
        /// </summary>
        /// <param name="lstObjCreate"></param>
        /// <returns></returns>
        public ResponseBase<List<AssistPartStockDto>> Create(List<AssistPartStockParameterDto> lstObjCreate)
        {
            // initialization
            var result = new ResponseBase<List<AssistPartStockDto>>();
            var stockList = new List<AssistPartStock>();
            var validationResults = new List<DNetValidationResult>();

            try
            {
                // validate duplicate data
                if (ValidateDuplicateData(lstObjCreate, validationResults, true))
                {
                    foreach (var item in lstObjCreate)
                    {
                        // parse the object
                        AssistPartStock obj = _mapper.Map<AssistPartStock>(item);

                        // validate part stock                        
                        if (ValidateAssistPartStock(item, obj, validationResults))
                        {
                            stockList.Add(obj);
                        }
                    }
                }

                // if any error found
                if (validationResults.Any())
                {
                    return PopulateValidationError<List<AssistPartStockDto>>(validationResults, null);
                }
                else
                {
                    result.success = InsertWithTransactionManager(stockList) != null;
                    if (result.success)
                        result.total = stockList.Count;
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
        /// Create a new AssistPartStock
        /// </summary>
        /// <param name="lstObjCreate"></param>
        /// <returns></returns>
        public async Task<ResponseBase<List<AssistPartStockDto>>> BulkCreateAsync(List<AssistPartStockParameterDto> lstObjCreate)
        {
            // initialization
            var result = new ResponseBase<List<AssistPartStockDto>>();
            var stockList = new List<AssistPartStock>();
            var validationResults = new List<DNetValidationResult>();

            try
            {
                if (lstObjCreate.Count > 0)
                {
                    // validate duplicate data
                    if (ValidateDuplicateData(lstObjCreate, validationResults, true))
                    {
                        if (await ValidListAssistPartStockAsync(lstObjCreate, stockList, validationResults))
                        {
                            await ValidateListAssistPartStockAsync(stockList, validationResults);
                        }
                    }

                    // if any error found
                    if (validationResults.Any())
                    {
                        return PopulateValidationError<List<AssistPartStockDto>>(validationResults, null);
                    }
                    else
                    {
                        var isSuccess = await _assistPartStockRepository.BulkInsertAsync(stockList);
                        result.success = isSuccess;
                        if (result.success)
                            result.total = stockList.Count;
                    }
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
        /// Create a new AssistPartStock
        /// </summary>
        /// <param name="lstObjCreate"></param>
        /// <returns></returns>
        public ResponseBase<List<AssistPartStockDto>> BulkCreate(List<AssistPartStockParameterDto> lstObjCreate)
        {
            // initialization
            var result = new ResponseBase<List<AssistPartStockDto>>();
            var stockList = new List<AssistPartStock>();
            var validationResults = new List<DNetValidationResult>();

            try
            {
                if (lstObjCreate.Count > 0)
                {
                    // validate duplicate data
                    if (ValidateDuplicateData(lstObjCreate, validationResults, true))
                    {
                        if (ValidListAssistPartStock(lstObjCreate, stockList, validationResults))
                        {
                            ValidateListAssistPartStock(stockList, validationResults);
                        }
                    }

                    // if any error found
                    if (validationResults.Any())
                    {
                        return PopulateValidationError<List<AssistPartStockDto>>(validationResults, null);
                    }
                    else
                    {
                        var isSuccess = _assistPartStockRepository.BulkInsert(stockList);
                        result.success = isSuccess;
                        if (result.success)
                            result.total = stockList.Count;
                    }
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
        /// Update AssistPartStock
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<AssistPartStockDto> Update(AssistPartStockParameterDto objUpdate)
        {
            // initialization
            var result = new ResponseBase<AssistPartStockDto>();
            var validationResults = new List<DNetValidationResult>();

            try
            {
                // validate some fields
                AssistPartStock partStock = UpdateProperties(objUpdate, validationResults);
                if (validationResults.Count == 0)
                {
                    var id = (int)_assistpartstockMapper.Update(partStock, DNetUserName);
                    result.success = id > 0;
                    if (result.success)
                    {
                        result._id = objUpdate.ID;
                        result.total = 1;
                    }
                    else
                    {
                        ErrorMsgHelper.DataCorrupt(result.messages);
                    }
                }
                else
                {
                    return PopulateValidationError<AssistPartStockDto>(validationResults, null);
                }
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Update AssistPartStock
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<List<AssistPartStockDto>> Update(List<AssistPartStockParameterDto> lstObjUpdate)
        {
            // initialization
            var result = new ResponseBase<List<AssistPartStockDto>>();
            var validationResults = new List<DNetValidationResult>();
            var stockList = new List<AssistPartStock>();

            // validate duplicate data            
            if (ValidateDuplicateData(lstObjUpdate, validationResults, false))
            {
                // proceed each object
                foreach (var obj in lstObjUpdate)
                {
                    // update some property
                    AssistPartStock stock = UpdateProperties(obj, validationResults);
                    if (validationResults.Count == 0)
                    {
                        // validate part stock
                        ValidateAssistPartStock(obj, stock, validationResults);

                        // add it to list
                        stockList.Add(stock);
                    }
                }
            }

            // if any error found
            if (validationResults.Any())
            {
                return PopulateValidationError<List<AssistPartStockDto>>(validationResults, null);
            }
            else
            {
                result.success = UpdateWithTransactionManager(stockList) != null;
                if (result.success)
                    result.total = stockList.Count;
            }

            return result;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Validate assist part stock
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="entity"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidateAssistPartStock(AssistPartStockParameterDto objCreate, AssistPartStock entity, List<DNetValidationResult> validationResults)
        {
            // get sparepart master
            var spMaster = _spareparmastertBL.GetPartByPartNumberRowStatus(objCreate.NoParts);
            if (spMaster != null)
            {
                entity.SparePartMaster = spMaster;
            }
            else
            {
                // ignore the validation if the part number has NPN as its prefix
                if (!objCreate.NoParts.StartsWith("NPN", StringComparison.OrdinalIgnoreCase))
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSparepartNotFound, objCreate.NoParts)));
                }
            }

            // Validate Dealer            
            Dealer dealer = null;
            if (ValidationHelper.ValidateDealer(objCreate.DealerCode, validationResults, this.DealerCode, ref dealer))
            {
                entity.Dealer = dealer;
            }

            // Validate Dealer Branch
            DealerBranch dealerBranch = null;
            if (ValidationHelper.ValidateDealerBranch(this.DealerCode, validationResults, objCreate.DealerBranchCode, ref dealerBranch))
            {
                if (dealerBranch != null)
                {
                    entity.DealerBranch = dealerBranch;
                    entity.DealerBranchCode = dealerBranch.DealerBranchCode;
                }
            }

            if (objCreate.HargaBeli < 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSparepartQuantity, FieldResource.HargaBeli, objCreate.NoParts)));
            }

            if (objCreate.JumlahDatang < 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSparepartQuantity, FieldResource.JumlahDatang, objCreate.NoParts)));
            }

            if (objCreate.JumlahStokAwal < 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSparepartQuantity, FieldResource.JumlahStokAwal, objCreate.NoParts)));
            }

            if (ValidateDataExist(objCreate))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataIsExist, FieldResource.AssistPartStock)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate assist part stock
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private async Task ValidateListAssistPartStockAsync(List<AssistPartStock> stockList, List<DNetValidationResult> validationResults)
        {
            var spMasterDataList = await _sparePartMasterRepository.GetByPartNumbersAsync(stockList.Select(e => e.NoParts).ToList());
            List<SparePartMaster> spMasterList = spMasterDataList.ToList();

            foreach (var item in stockList)
            {
                // get sparepart master
                var spMaster = spMasterList.Where(e => e.PartNumber == item.NoParts).SingleOrDefault();
                if (spMaster != null)
                {
                    item.SparepartMasterID = spMaster.ID;
                    item.SparePartMaster = spMaster;
                }
                else
                {
                    // ignore the validation if the part number has NPN as its prefix
                    if (!item.NoParts.StartsWith("NPN", StringComparison.OrdinalIgnoreCase))
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSparepartNotFound, item.NoParts)));
                    }
                }
            }

            if (!validationResults.Any())
            {
                string dealerCode = stockList[0].DealerCode;
                List<string> listDealerBranchCode = stockList.Where(e => !string.IsNullOrEmpty(e.DealerBranchCode)).Select(e => e.DealerBranchCode).ToList();
                List<string> listMonth = stockList.Select(e => e.Month).ToList();
                List<string> listYear = stockList.Select(e => e.Year).ToList();
                List<string> listNoParts = stockList.Select(e => e.NoParts).ToList();

                var items = await _assistPartStockRepository.GetDuplicateDataAsync(dealerCode, listDealerBranchCode, listMonth, listYear, listNoParts);
                List<AssistPartStock> arrayListResult = items.ToList();
                if (arrayListResult.Any())
                {
                    foreach (AssistPartStock data in arrayListResult)
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataIsExist, FieldResource.AssistPartStock + " " + data.NoParts)));
                    }
                }
            }
        }

        /// <summary>
        /// Validate assist part stock
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private void ValidateListAssistPartStock(List<AssistPartStock> stockList, List<DNetValidationResult> validationResults)
        {
            var spMasterDataList = _sparePartMasterRepository.GetByPartNumbers(stockList.Select(e => e.NoParts).ToList());
            List<SparePartMaster> spMasterList = spMasterDataList.ToList();

            foreach (var item in stockList)
            {
                // get sparepart master
                var spMaster = spMasterList.Where(e => e.PartNumber == item.NoParts).SingleOrDefault();
                if (spMaster != null)
                {
                    item.SparepartMasterID = spMaster.ID;
                    item.SparePartMaster = spMaster;
                }
                else
                {
                    // ignore the validation if the part number has NPN as its prefix
                    if (!item.NoParts.StartsWith("NPN", StringComparison.OrdinalIgnoreCase))
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSparepartNotFound, item.NoParts)));
                    }
                }
            }

            if (!validationResults.Any())
            {
                string dealerCode = stockList[0].DealerCode;
                List<string> listDealerBranchCode = stockList.Where(e => !string.IsNullOrEmpty(e.DealerBranchCode)).Select(e => e.DealerBranchCode).ToList();
                List<string> listMonth = stockList.Select(e => e.Month).ToList();
                List<string> listYear = stockList.Select(e => e.Year).ToList();
                List<string> listNoParts = stockList.Select(e => e.NoParts).ToList();

                var items = _assistPartStockRepository.GetDuplicateData(dealerCode, listDealerBranchCode, listMonth, listYear, listNoParts);
                List<AssistPartStock> arrayListResult = items.ToList();
                if (arrayListResult.Any())
                {
                    foreach (AssistPartStock data in arrayListResult)
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataIsExist, FieldResource.AssistPartStock + " " + data.NoParts)));
                    }
                }
            }
        }

        /// <summary>
        /// Validate assist part stock
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="entity"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private async Task<bool> ValidListAssistPartStockAsync(List<AssistPartStockParameterDto> lstObjCreate, List<AssistPartStock> stockList, List<DNetValidationResult> validationResults)
        {
            string dealerCode = lstObjCreate[0].DealerCode;

            // Validate Dealer            
            Dealer dealer = null;
            if (ValidationHelper.ValidateDealer(dealerCode, validationResults, this.DealerCode, ref dealer))
            {
                List<DealerBranch> dealerBranchMaster = new List<DealerBranch>();
                dealerBranchMaster = await _dealerBranchRepository.GetDealerBranchByDealerIDAsync(dealer.ID);

                List<string> dealerBranchCodeList = lstObjCreate.Where(e => e.DealerBranchCode != null).Select(e => e.DealerBranchCode).ToList();
                foreach (AssistPartStockParameterDto item in lstObjCreate)
                {
                    if (item.HargaBeli < 0)
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSparepartQuantity, FieldResource.HargaBeli, item.NoParts)));
                    }

                    if (item.JumlahDatang < 0)
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSparepartQuantity, FieldResource.JumlahDatang, item.NoParts)));
                    }

                    if (item.JumlahStokAwal < 0)
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSparepartQuantity, FieldResource.JumlahStokAwal, item.NoParts)));
                    }

                    DealerBranch dealerBranch = null;
                    if (dealerBranchCodeList.Any() && !string.IsNullOrEmpty(item.DealerBranchCode))
                    {
                        dealerBranch = dealerBranchMaster.Where(e => e.DealerBranchCode == item.DealerBranchCode).FirstOrDefault();
                        if (dealerBranch == null)
                        {
                            validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDealerBranchCodeNotMatch, item.DealerBranchCode, dealerCode)));
                        }
                    }
                    else
                    {
                        AssistPartStock entity = _mapper.Map<AssistPartStock>(item);

                        entity.DealerID = dealer.ID;
                        entity.Dealer = dealer;

                        if (dealerBranch != null)
                        {
                            entity.DealerBranchID = dealerBranch.ID;
                            entity.DealerBranch = dealerBranch;
                        }

                        entity.CreatedBy = DNetUserName;
                        entity.CreatedTime = DateTime.Now;
                        entity.LastUpdateTime = DateTime.Now;

                        stockList.Add(entity);
                    }
                }
            }

            return !validationResults.Any();
        }

        /// <summary>
        /// Validate assist part stock
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="entity"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidListAssistPartStock(List<AssistPartStockParameterDto> lstObjCreate, List<AssistPartStock> stockList, List<DNetValidationResult> validationResults)
        {
            string dealerCode = lstObjCreate[0].DealerCode;

            // Validate Dealer            
            Dealer dealer = null;
            if (ValidationHelper.ValidateDealer(dealerCode, validationResults, this.DealerCode, ref dealer))
            {
                List<DealerBranch> dealerBranchMaster = new List<DealerBranch>();
                dealerBranchMaster = _dealerBranchRepository.GetDealerBranchByDealerID(dealer.ID);

                List<string> dealerBranchCodeList = lstObjCreate.Where(e => e.DealerBranchCode != null).Select(e => e.DealerBranchCode).ToList();
                foreach (AssistPartStockParameterDto item in lstObjCreate)
                {
                    if (item.HargaBeli < 0)
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSparepartQuantity, FieldResource.HargaBeli, item.NoParts)));
                    }

                    if (item.JumlahDatang < 0)
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSparepartQuantity, FieldResource.JumlahDatang, item.NoParts)));
                    }

                    if (item.JumlahStokAwal < 0)
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSparepartQuantity, FieldResource.JumlahStokAwal, item.NoParts)));
                    }

                    DealerBranch dealerBranch = null;
                    if (dealerBranchCodeList.Any() && !string.IsNullOrEmpty(item.DealerBranchCode))
                    {
                        dealerBranch = dealerBranchMaster.Where(e => e.DealerBranchCode == item.DealerBranchCode).FirstOrDefault();
                        if (dealerBranch == null)
                        {
                            validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDealerBranchCodeNotMatch, item.DealerBranchCode, dealerCode)));
                        }
                    }
                    else
                    {
                        AssistPartStock entity = _mapper.Map<AssistPartStock>(item);

                        entity.DealerID = dealer.ID;
                        entity.Dealer = dealer;

                        if (dealerBranch != null)
                        {
                            entity.DealerBranchID = dealerBranch.ID;
                            entity.DealerBranch = dealerBranch;
                        }

                        entity.CreatedBy = DNetUserName;
                        entity.CreatedTime = DateTime.Now;
                        entity.LastUpdateTime = DateTime.Now;

                        stockList.Add(entity);
                    }
                }
            }

            return !validationResults.Any();
        }

        /// <summary>
        /// Validate data exist
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool ValidateDataExist(AssistPartStockParameterDto obj)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(AssistPartStock), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(AssistPartStock), "Month", MatchType.Exact, obj.Month.ToString()));
            criterias.opAnd(new Criteria(typeof(AssistPartStock), "Year", MatchType.Exact, obj.Year.ToString()));
            criterias.opAnd(new Criteria(typeof(AssistPartStock), "NoParts", MatchType.Exact, obj.NoParts));
            criterias.opAnd(new Criteria(typeof(AssistPartStock), "DealerCode", MatchType.Exact, obj.DealerCode));
            criterias.opAnd(new Criteria(typeof(AssistPartStock), "DealerBranchCode", MatchType.Exact, obj.DealerBranchCode));
            ArrayList arrayListResult = _assistpartstockMapper.RetrieveByCriteria(criterias);
            if (arrayListResult.Count == 1)
            {
                return (arrayListResult[0] as AssistPartStock).ID != obj.ID;
            }

            return arrayListResult.Count > 0;
        }

        /// <summary>
        /// Validate duplicate data
        /// </summary>
        /// <param name="lstPartStocks"></param>
        /// <returns></returns>
        private bool ValidateDuplicateData(List<AssistPartStockParameterDto> lstPartStocks, List<DNetValidationResult> validationResults, bool checkDuplicateOnDB)
        {
            List<string> duplicatedCode = new List<string>();

            List<AssistPartStock> listOfExistingData = new List<AssistPartStock>();
            if (checkDuplicateOnDB)
            {
                List<AssistPartStockParameterDto> listOfFilteredPartStock = (from part in lstPartStocks
                                                                             group part by new
                                                                             {
                                                                                 part.Month,
                                                                                 part.Year
                                                                             } into grp
                                                                             select grp.First()).ToList();

                foreach (AssistPartStockParameterDto assistPart in listOfFilteredPartStock)
                {
                    List<string> listOfPartNo = lstPartStocks.Where(stock => stock.Year.Trim() == assistPart.Year.Trim() && stock.Month.Trim() == assistPart.Month.Trim()).Select(part => part.NoParts.Trim()).ToList();
                    listOfExistingData.AddRange(GetExistingAssistPartStock(assistPart.Month, assistPart.Year, listOfPartNo));
                }
            }

            foreach (var item in lstPartStocks)
            {
                string dealerBranchCode = string.IsNullOrEmpty(item.DealerBranchCode) ? string.Empty : item.DealerBranchCode.Trim();
                string keyword = item.Month.Trim() + item.Year.Trim() + item.NoParts.Trim() + item.DealerCode.Trim() + dealerBranchCode;
                if (!duplicatedCode.Contains(keyword))
                {
                    var lst = lstPartStocks.Where(d => d.Month.Trim() == item.Month.Trim() &&
                                                d.Year.Trim() == item.Year.Trim() &&
                                                d.NoParts.Trim() == item.NoParts.Trim() &&
                                                d.DealerCode.Trim() == item.DealerCode.Trim() &&    
                                                d.DealerBranchCode == dealerBranchCode);

                    var listWhichExistOnDB = listOfExistingData.Where(d => d.Month.Trim() == item.Month.Trim() &&
                                                d.Year.Trim() == item.Year.Trim() &&
                                                d.NoParts.Trim() == item.NoParts.Trim() &&
                                                d.DealerCode.Trim() == item.DealerCode.Trim() && 
                                                d.DealerBranchCode == dealerBranchCode);

                    if (lst.Count() > 1 || listWhichExistOnDB.Count() > 0)
                    {
                        duplicatedCode.Add(item.Month.Trim() + item.Year.Trim() + item.NoParts.Trim() + item.DealerCode.Trim() + dealerBranchCode);
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataType, string.Format(MessageResource.ErrorMsgDuplicateData) + string.Format(" ({0},{1},{2})", item.NoParts, item.Month, item.Year))));
                    }
                }
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// GetExistingAssistPartSales
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        private List<AssistPartStock> GetExistingAssistPartStock(string month, string year, List<string> listOfPartNo)
        {
            try
            {

                if (listOfPartNo != null && listOfPartNo.Count() > 0)
                {
                    var criterias = new CriteriaComposite(new Criteria(typeof(AssistPartStock), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    criterias.opAnd(new Criteria(typeof(AssistPartStock), "NoParts", MatchType.InSet, "('" + string.Join("','", listOfPartNo) + "')"));
                    criterias.opAnd(new Criteria(typeof(AssistPartStock), "DealerCode", MatchType.Exact, DealerCode));
                    criterias.opAnd(new Criteria(typeof(AssistPartStock), "Month", MatchType.Exact, month));
                    criterias.opAnd(new Criteria(typeof(AssistPartStock), "Year", MatchType.Exact, year));
                    criterias.opAnd(new Criteria(typeof(AssistPartStock), "StatusAktif", MatchType.Exact, 1));

                    var data = _assistpartstockMapper.RetrieveByCriteria(criterias);
                    if (data.Count > 0)
                    {
                        return data.Cast<AssistPartStock>().ToList();
                    }
                }

                return new List<AssistPartStock>();
            }
            catch (Exception e)
            {
                return new List<AssistPartStock>();
            }
        }

        /// <summary>
        /// Update validation
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private AssistPartStock UpdateProperties(AssistPartStockParameterDto objUpdate, List<DNetValidationResult> validationResults)
        {
            AssistPartStock existingObj = (AssistPartStock)_assistpartstockMapper.Retrieve(objUpdate.ID);
            if (existingObj != null)
            {
                existingObj.DealerCode = objUpdate.DealerCode;
                existingObj.DealerBranchCode = objUpdate.DealerBranchCode;
                existingObj.NoParts = objUpdate.NoParts;
                existingObj.HargaBeli = (decimal)objUpdate.HargaBeli;
                existingObj.JumlahDatang = (decimal)objUpdate.JumlahDatang;
                existingObj.JumlahStokAwal = (decimal)objUpdate.JumlahStokAwal;
                existingObj.Month = objUpdate.Month.ToString();
                existingObj.Year = objUpdate.Year.ToString();

                //existingObj.RemarksSystem = objUpdate.RemarksSystem;
                //existingObj.StatusAktif = (short)objUpdate.StatusAktif;
                //existingObj.ValidateSystemStatus = (short)objUpdate.ValidateSystemStatus;

                ValidateAssistPartStock(objUpdate, existingObj, validationResults);
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, "AssistPartStock", objUpdate.ID)));
            }

            return existingObj;
        }

        /// <summary>
        /// Insert Spare Part PO with transaction manager
        /// </summary>
        /// <param name="spare part POCustomer"></param>
        /// <returns></returns>
        private List<AssistPartStock> InsertWithTransactionManager(List<AssistPartStock> listStock)
        {
            List<AssistPartStock> result = null;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    foreach (var item in listStock)
                    {
                        this._transactionManager.AddInsert(item, DNetUserName);
                    }

                    this._transactionManager.PerformTransaction();
                    result = listStock;
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
        /// Handler on executed insert command with transaction manager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void InsertWithTransactionManagerHandler(object sender, TransactionManager.OnInsertArgs args)
        {
            // set the object ID from db returned id
            if (args.DomainObject.GetType() == typeof(AssistPartStock))
            {
                ((AssistPartStock)args.DomainObject).ID = args.ID;
                ((AssistPartStock)args.DomainObject).MarkLoaded();
            }
        }

        /// <summary>
        /// Insert Spare Part PO with transaction manager
        /// </summary>
        /// <param name="listStock"></param>
        /// <returns></returns>
        private List<AssistPartStock> UpdateWithTransactionManager(List<AssistPartStock> listStock)
        {
            List<AssistPartStock> result = null;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    foreach (var item in listStock)
                    {
                        this._transactionManager.AddUpdate(item, DNetUserName);
                    }

                    this._transactionManager.PerformTransaction();
                    result = listStock;
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
        #endregion
    }
}

