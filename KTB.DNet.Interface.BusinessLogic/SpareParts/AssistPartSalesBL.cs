#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AssistPartSales business logic class
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
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class AssistPartSalesBL : AbstractBusinessLogic, IAssistPartSalesBL
    {
        #region Variables
        private readonly IMapper _assistpartsalesMapper;
        private readonly AutoMapper.IMapper _mapper;
        private TransactionManager _transactionManager;
        private SparePartMasterBL _sparePartMasterBL;
        private StandardCodeBL _enumBL;
        private AppConfigBL _appConfBL;

        IAssistPartSalesRepository<AssistPartSales, int> _assistPartSalesRepository;
        ISparePartMasterRepository<SparePartMaster, int> _sparePartMasterRepository;
        IDealerBranchRepository<DealerBranch, int> _dealerBranchRepository;
        ISalesmanHeaderRepository<SalesmanHeader, int> _salesmanRepository;
        IPartShopRepository<PartShop, int> _partShopRepository;
        #endregion

        #region data Variables
        AppConfig _appConfig = null;
        List<PartShop> _partShopMaster = null;
        List<VWI_Fleet> _fleetMaster = null;
        List<Dealer> _dealerMaster = null;
        List<DealerBranch> _dealerBranchMaster = null;
        #endregion

        #region Constructor
        public AssistPartSalesBL(IAssistPartSalesRepository<AssistPartSales, int> assistPartSalesRepository, ISalesmanHeaderRepository<SalesmanHeader, int> salesmanRepository,
            IDealerBranchRepository<DealerBranch, int> dealerBranchRepository, ISparePartMasterRepository<SparePartMaster, int> sparePartMasterRepository,
            IPartShopRepository<PartShop, int> partShopRepository)
        {
            _assistpartsalesMapper = MapperFactory.GetInstance().GetMapper(typeof(AssistPartSales).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _transactionManager = new TransactionManager();
            _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);
            _enumBL = new StandardCodeBL(_mapper);
            _sparePartMasterBL = new SparePartMasterBL(_mapper);
            _appConfBL = new AppConfigBL(_mapper);

            _assistPartSalesRepository = assistPartSalesRepository;
            _dealerBranchRepository = dealerBranchRepository;
            _sparePartMasterRepository = sparePartMasterRepository;
            _salesmanRepository = salesmanRepository;
            _partShopRepository = partShopRepository;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get AssistPartSales by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<AssistPartSalesReadDto>> ReadData(AssistPartSalesFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(AssistPartSales), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            //criterias.opAnd(new Criteria(typeof(AssistPartSales), "DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<AssistPartSalesReadDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(AssistPartSales), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(AssistPartSales), filterDto, sortColl);

                // get data
                var data = _assistpartsalesMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<AssistPartSales>().ToList();
                    var listData = list.Select(item => _mapper.Map<AssistPartSalesReadDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(AssistPartSales), filterDto);
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
        /// Delete AssistPartSales by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<AssistPartSalesDto> Delete(int id)
        {
            var result = new ResponseBase<AssistPartSalesDto>();

            try
            {
                var assistpartsales = (AssistPartSales)_assistpartsalesMapper.Retrieve(id);
                if (assistpartsales != null)
                {
                    assistpartsales.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _assistpartsalesMapper.Update(assistpartsales, DNetUserName);
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
        /// Create
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<AssistPartSalesDto> Create(AssistPartSalesParameterDto objCreate)
        {
            // declare
            var result = new ResponseBase<AssistPartSalesDto>();
            var validationResults = new List<DNetValidationResult>();
            var listOfExistingOnDb = new List<AssistPartSales>();

            try
            {
                // parsing the object
                AssistPartSales newEntity = _mapper.Map<AssistPartSales>(objCreate);
                newEntity.CreatedTime = DateTime.Now;

                ValidateDuplicateParamData(new List<AssistPartSalesParameterDto>() 
                { 
                    objCreate 
                }, 
                validationResults, ref listOfExistingOnDb);
                if (validationResults.Any())
                {
                    return PopulateValidationError<AssistPartSalesDto>(validationResults, null);
                }

                // validate and get relation                 
                if (ValidateSave(objCreate, newEntity, validationResults))
                {
                    result = NewInsertProcess(newEntity, listOfExistingOnDb);
                }
                else
                {
                    return PopulateValidationError<AssistPartSalesDto>(validationResults, null);
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
        /// Create
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<List<AssistPartSalesDto>> Create(List<AssistPartSalesParameterDto> lstObjCreate)
        {
            // declare
            var result = new ResponseBase<List<AssistPartSalesDto>>();
            var assistPartSalesList = new List<AssistPartSales>();
            var validationResults = new List<DNetValidationResult>();
            var listOfExistingData = new List<AssistPartSales>();

            try
            {
                // validate any duplicate data
                if (ValidateDuplicateParamData(lstObjCreate, validationResults, ref listOfExistingData))
                {
                    // proceed each object
                    foreach (var objCreate in lstObjCreate)
                    {
                        // parse param into object
                        AssistPartSales newEntity = _mapper.Map<AssistPartSales>(objCreate);
                        newEntity.CreatedTime = DateTime.Now;

                        // validate each object
                        if (ValidateSave(objCreate, newEntity, validationResults))
                        {
                            assistPartSalesList.Add(newEntity);
                        }
                    }
                }

                // if any error found
                if (validationResults.Any())
                {
                    return PopulateValidationError<List<AssistPartSalesDto>>(validationResults, null);
                }
                else
                {
                    // insert into db
                    foreach(var item in assistPartSalesList)
                    {
                        var res = NewInsertProcess(item, listOfExistingData);
                        result.success = res.success;
                        result._id = 1;
                        result.messages = res.messages;
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
        /// Create
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public async Task<ResponseBase<List<AssistPartSalesDto>>> BulkCreateAsync(List<AssistPartSalesParameterDto> lstObjCreate)
        {
            // declare
            var result = new ResponseBase<List<AssistPartSalesDto>>();
            var assistPartSalesList = new List<AssistPartSales>();
            var validationResults = new List<DNetValidationResult>();
            var listOfExistingData = new List<AssistPartSales>();

            try
            {
                if (lstObjCreate.Count > 0)
                {
                    // validate any duplicate data
                    if (ValidateDuplicateParamData(lstObjCreate, validationResults, ref listOfExistingData))
                    {
                        await ValidateListPartSalesAsync(lstObjCreate, assistPartSalesList, validationResults);
                    }
                }

                // if any error found
                if (validationResults.Any())
                {
                    return PopulateValidationError<List<AssistPartSalesDto>>(validationResults, null);
                }
                else
                {
                    // insert into db
                    var isSuccess = await _assistPartSalesRepository.BulkInsertAsync(assistPartSalesList);
                    result.success = isSuccess;
                    if (result.success)
                        result.total = assistPartSalesList.Count;
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
        /// Create
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<List<AssistPartSalesDto>> BulkCreate(List<AssistPartSalesParameterDto> lstObjCreate)
        {
            // declare
            var result = new ResponseBase<List<AssistPartSalesDto>>();
            var assistPartSalesList = new List<AssistPartSales>();
            var validationResults = new List<DNetValidationResult>();
            var listOfExistingData = new List<AssistPartSales>();

            try
            {
                if (lstObjCreate.Count > 0)
                {
                    // validate any duplicate data
                    if (ValidateDuplicateParamData(lstObjCreate, validationResults,ref listOfExistingData))
                    {
                        ValidateListPartSales(lstObjCreate, assistPartSalesList, validationResults);
                    }
                }

                // if any error found
                if (validationResults.Any())
                {
                    return PopulateValidationError<List<AssistPartSalesDto>>(validationResults, null);
                }
                else
                {
                    // insert into db
                    var isSuccess = _assistPartSalesRepository.BulkInsert(assistPartSalesList);
                    result.success = isSuccess;
                    if (result.success)
                        result.total = assistPartSalesList.Count;
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
        /// Update
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ResponseBase<AssistPartSalesDto> Update(AssistPartSalesParameterDto param)
        {
            // declare
            var result = new ResponseBase<AssistPartSalesDto>();
            var validationResults = new List<DNetValidationResult>();

            try
            {
                // get the existing object
                var existingDomain = (AssistPartSales)_assistpartsalesMapper.Retrieve((int)param.ID);
                if (existingDomain == null)
                {
                    result.messages.Add(new MessageBase { ErrorCode = ErrorCode.DataUpdateNotAvailable, ErrorMessage = string.Format(MessageResource.ErrorMsgDataNotFound, FieldResource.AssistPartSales) });
                }
                else
                {
                    // parsing the object
                    var mergeData = _mapper.Map<AssistPartSalesParameterDto, AssistPartSales>(param, existingDomain);
                    mergeData.LastUpdateTime = DateTime.Now;

                    // validate and get relation 
                    ValidateSave(param, mergeData, validationResults);

                    // if any error found
                    if (validationResults.Any())
                    {
                        return PopulateValidationError<AssistPartSalesDto>(validationResults, null);
                    }

                    // insert into db
                    int resultID = UpdateWithTransactionManager(mergeData);
                    if (resultID > 0)
                    {
                        result.success = true;
                        result._id = resultID;
                        result.total = 1;
                    }
                    else
                    {
                        ErrorMsgHelper.ErrorMsgDBSaveContactAdmin(result.messages);
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

        #endregion

        #region Private Methods
        /// <summary>
        /// Duplicate Data Validation
        /// </summary>
        /// <param name="partSalesList"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidateDuplicateParamData(List<AssistPartSalesParameterDto> partSalesList, List<DNetValidationResult> validationResults, ref List<AssistPartSales> listOfExistingData)
        {
            List<string> duplicatedCode = new List<string>();
            listOfExistingData = new List<AssistPartSales>();

            // get existing data on db
            List<AssistPartSalesParameterDto> listOfFilteredPartSales = (from part in partSalesList
                                                                         group part by new
                                                                         {
                                                                             part.NoWorkOrder,
                                                                             part.TglTransaksi
                                                                         } into grp
                                                                         select grp.First()).ToList();

            foreach (AssistPartSalesParameterDto assistPart in listOfFilteredPartSales)
            {
                listOfExistingData.AddRange(GetExistingAssistPartSales(assistPart.NoWorkOrder, assistPart.TglTransaksi));
            }
            // end get existing data on db

            // check duplicate
            foreach (var item in partSalesList)
            {
                string dealerBranchCode = string.IsNullOrEmpty(item.DealerBranchCode) ? string.Empty : item.DealerBranchCode.Trim();
                string keyword = item.KodeCustomer.Trim() + item.KodeSalesman.Trim() + item.NoParts.Trim() + item.NoWorkOrder.Trim() + item.Qty.ToString().Trim() + item.HargaBeli.ToString().Trim() + item.HargaJual.ToString().Trim() + item.DealerCode.Trim() + dealerBranchCode;
                if (!duplicatedCode.Contains(keyword))
                {
                    // check duplicate on parameter inputted
                    var lst = partSalesList.Where(d => d.KodeCustomer.Trim() == item.KodeCustomer.Trim() &&
                                                d.KodeSalesman.Trim() == item.KodeSalesman.Trim() &&
                                                d.NoParts.Trim() == item.NoParts.Trim() &&
                                                d.NoWorkOrder.Trim() == item.NoWorkOrder.Trim() &&
                                                d.Qty.ToString().Trim() == item.Qty.ToString().Trim() &&
                                                d.HargaBeli == item.HargaBeli &&
                                                d.HargaJual == item.HargaJual &&
                                                d.DealerCode.Trim() == item.DealerCode.Trim() &&
                                                d.DealerBranchCode == dealerBranchCode &&
                                                d.TglTransaksi == item.TglTransaksi);

                    // check duplicate on db
                    //var listWhichExistOnDB = listOfExistingData.Where(d =>
                    //{
                    //    return
                    //                            d.KodeCustomer.Trim() == item.KodeCustomer.Trim() &&
                    //                            d.KodeSalesman.Trim() == (string.IsNullOrEmpty(item.KodeSalesman.Trim()) && item.TrTraineeSalesSparepartID > 0 ?
                    //                                                        item.TrTraineeSalesSparepartID.ToString().Trim() :
                    //                                                        item.KodeSalesman.Trim()) &&
                    //                            d.NoParts.Trim() == item.NoParts.Trim() &&
                    //                            d.NoWorkOrder.Trim() == item.NoWorkOrder.Trim() &&
                    //                            d.Qty.ToString().Trim() == item.Qty.ToString().Trim() &&
                    //                            ((int)d.HargaBeli) == item.HargaBeli &&
                    //                            ((int)d.HargaJual) == item.HargaJual &&
                    //                            d.DealerCode.Trim() == item.DealerCode.Trim() &&
                    //                            d.DealerBranch.DealerBranchCode == dealerBranchCode &&
                    //                            ((DateTime)d.TglTransaksi) == item.TglTransaksi;
                    //}
                    //    );

                    //if (lst.Count() > 1 || listWhichExistOnDB.Count() > 0)
                    // updated | it's only check parameter inputted by user
                    if (lst.Count() > 1 )
                    {
                        string code = item.KodeCustomer.Trim() + item.KodeSalesman.Trim() + item.NoParts.Trim() + item.NoWorkOrder.Trim() + item.Qty.ToString().Trim() + item.HargaBeli.ToString().Trim() + item.HargaJual.ToString().Trim() + item.DealerCode.Trim() + dealerBranchCode;
                        duplicatedCode.Add(code);
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataType, string.Format(MessageResource.ErrorMsgDuplicateData) + string.Format(" ({0},{1},{2})", item.TglTransaksi.ToString("yyyy-MM-dd"), item.NoWorkOrder, item.NoParts))));
                    }
                }
            }
            // end check duplicate
            return validationResults.Count == 0;
        }

        private ResponseBase<AssistPartSalesDto> NewInsertProcess(AssistPartSales newEntity, List<AssistPartSales> listOfExistingOnDb)
        {
            var result = new ResponseBase<AssistPartSalesDto>();

            if (listOfExistingOnDb.Count > 0)
            {
                var cekExist = listOfExistingOnDb.Where(x => x.NoWorkOrder.Trim() == newEntity.NoWorkOrder.Trim() && x.TglTransaksi.ToString() == newEntity.TglTransaksi.ToString());
                if (cekExist.Count() > 0)
                {
                    if (newEntity.Qty > 0)
                    {
                        // update on duplicate and param qty > 0
                        var listDuplicate = listOfExistingOnDb.Where(x => x.NoWorkOrder == newEntity.NoWorkOrder && x.TglTransaksi.ToString() == newEntity.TglTransaksi.ToString() && x.NoParts == newEntity.NoParts);
                        if (listDuplicate.Count() > 0)
                        {
                            newEntity.ID = listDuplicate.ToList()[0].ID;
                            newEntity.CreatedBy = DNetUserName;
                            newEntity.MarkLoaded();
                            int resultID = _assistpartsalesMapper.Update(newEntity, DNetUserName);
                            if (resultID > 0)
                            {
                                result.success = true;
                                result._id = resultID;
                                result.total = 1;
                            }
                            else
                            {
                                ErrorMsgHelper.ErrorMsgDBSaveContactAdmin(result.messages);
                            }
                        }
                        else
                        {
                            int resultID = _assistpartsalesMapper.Insert(newEntity, DNetUserName);
                            if (resultID > 0)
                            {
                                result.success = true;
                                result._id = resultID;
                                result.total = 1;
                            }
                            else
                            {
                                ErrorMsgHelper.ErrorMsgDBSaveContactAdmin(result.messages);
                            }
                        }
                    }
                    else
                    {
                        // update rowstatus = -1 when duplicate and qty < 0
                        var criterias = new CriteriaComposite(new Criteria(typeof(AssistPartSales), "NoWorkOrder", MatchType.Exact, newEntity.NoWorkOrder));
                        var listDelete = _assistpartsalesMapper.RetrieveByCriteria(criterias);
                        if (listDelete.Count > 0)
                        {
                            foreach (AssistPartSales item in listDelete)
                            {
                                if(newEntity.SalesChannelCode != "C" && newEntity.SalesChannelCode != "S" && newEntity.SalesChannelCode != "I")
                                {
                                    item.RowStatus = (short)DBRowStatus.Deleted;
                                }
                                var nResult = _assistpartsalesMapper.Update(item, DNetUserName);
                                if (nResult != 0)
                                {
                                    result.success = true;
                                    result._id = item.ID;
                                    result.total = 1;
                                }
                                else
                                {
                                    ErrorMsgHelper.ErrorMsgDBSave(result.messages);
                                }
                            }
                        }

                    }

                }
                // if not exist
                else
                {
                    // insert if not duplicate and qty > 0
                    if (newEntity.Qty > 0)
                    {
                        int resultID = _assistpartsalesMapper.Insert(newEntity, DNetUserName);
                        if (resultID > 0)
                        {
                            result.success = true;
                            result._id = resultID;
                            result.total = 1;
                        }
                        else
                        {
                            ErrorMsgHelper.ErrorMsgDBSaveContactAdmin(result.messages);
                        }
                    }
                }
            }
            // if not exist on db
            else
            {
                // insert if not duplicate and qty > 0
                if (newEntity.Qty > 0 && newEntity.SalesChannelCode == "W" || (newEntity.SalesChannelCode == "C" || newEntity.SalesChannelCode == "S" || newEntity.SalesChannelCode == "I"))
                {
                    //int resultID = InsertWithTransactionManager(newEntity);
                    int resultID = _assistpartsalesMapper.Insert(newEntity, DNetUserName);
                    if (resultID > 0)
                    {
                        result.success = true;
                        result._id = resultID;
                        result.total = 1;
                    }
                    else
                    {
                        ErrorMsgHelper.ErrorMsgDBSaveContactAdmin(result.messages);
                    }
                }
                else
                {
                    result.success = false;
                    result._id = 0;
                    result.total = 0;
                    result.messages.Add(new MessageBase { ErrorCode = ErrorCode.Er_OK, ErrorMessage = "Work Order ini belum memiliki status invoiced." });   
                }
            }

            return result;
        }

        private List<AssistPartSales> GetExistingAssistPartSales(string WONumber, DateTime transactionDate)
        {
            try
            {

                var criterias = new CriteriaComposite(new Criteria(typeof(AssistPartSales), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(AssistPartSales), "DealerCode", MatchType.Exact, DealerCode));
                criterias.opAnd(new Criteria(typeof(AssistPartSales), "NoWorkOrder", MatchType.Exact, WONumber));
                criterias.opAnd(new Criteria(typeof(AssistPartSales), "TglTransaksi", MatchType.Exact, transactionDate));

                var data = _assistpartsalesMapper.RetrieveByCriteria(criterias);
                if (data.Count > 0)
                {
                    return data.Cast<AssistPartSales>().ToList();
                }

                return new List<AssistPartSales>();
            }
            catch (Exception e)
            {
                return new List<AssistPartSales>();
            }
        }

        /// <summary>
        /// Validate Customer
        /// </summary>
        /// <param name="kodeCustomer"></param>
        /// <returns></returns>
        private bool ValidateCustomer(string kodeCustomer)
        {
            // check if customer retail
            bool isCustomerRetail = ValidationHelper.IsCustomerRetailByCode(kodeCustomer, _mapper);
            if (isCustomerRetail) return true;

            // check if partshop
            var partShopCust = ValidationHelper.GetByPartShopCode(kodeCustomer);
            if (partShopCust != null) return true;

            // check if fleet customer
            var fleetCust = ValidationHelper.GetByVWIFleetCode(kodeCustomer);
            if (fleetCust != null) return true;

            // check if dealer 
            var dealerCustResponse = ValidationHelper.GetDealerDtoByCode(kodeCustomer, this.DealerCode);
            if (dealerCustResponse.success) return true;

            // check if dealer branch
            var dealerBranchCustResponse = ValidationHelper.GetDealerBranchByCode(kodeCustomer);
            if (dealerBranchCustResponse.success) return true;

            return false;
        }

        /// <summary>
        /// Validate customer async
        /// </summary>
        /// <param name="kodeCustomer"></param>
        /// <param name="codeCustomerList"></param>
        /// <returns></returns>
        private async Task<bool> ValidateCustomerAsync(string kodeCustomer, List<string> codeCustomerList)
        {
            // check if customer retail
            if (_appConfig != null)
            {
                if (_appConfig.Value.Equals(kodeCustomer, StringComparison.OrdinalIgnoreCase)) return true;
            }

            // check if partshop
            var partShop = await _partShopRepository.GetByCodesAsync(codeCustomerList);
            _partShopMaster = _partShopMaster ?? partShop.ToList();
            if (_partShopMaster.Any())
            {
                if (_partShopMaster.Where(e => e.PartShopCode == kodeCustomer).SingleOrDefault() != null) return true;
            }

            // check if fleet customer
            _fleetMaster = _fleetMaster ?? GetFleetByCustomerCode(codeCustomerList);
            if (_fleetMaster.Any())
            {
                if (_fleetMaster.Where(e => e.FleetCode == kodeCustomer).SingleOrDefault() != null) return true;
            }

            // check if dealer 
            _dealerMaster = _dealerMaster ?? GetDealerByCustomerCode(codeCustomerList);
            if (_dealerMaster.Any())
            {
                if (_dealerMaster.Where(e => e.DealerCode == kodeCustomer).SingleOrDefault() != null) return true;
            }

            // check if dealer branch
            var dealerBranch = await _dealerBranchRepository.GetAllByCodesAsync(codeCustomerList);
            _dealerBranchMaster = _dealerBranchMaster ?? dealerBranch.ToList();
            if (_dealerBranchMaster.Any())
            {
                if (_dealerBranchMaster.Where(e => e.DealerBranchCode == kodeCustomer).SingleOrDefault() != null) return true;
            }

            return false;
        }

        /// <summary>
        /// Validate customer async
        /// </summary>
        /// <param name="kodeCustomer"></param>
        /// <param name="codeCustomerList"></param>
        /// <returns></returns>
        private bool ValidateCustomer(string kodeCustomer, List<string> codeCustomerList)
        {
            // check if customer retail
            if (_appConfig != null)
            {
                if (_appConfig.Value.Equals(kodeCustomer, StringComparison.OrdinalIgnoreCase)) return true;
            }

            // check if partshop
            var partShop = _partShopRepository.GetByCodes(codeCustomerList);
            _partShopMaster = _partShopMaster ?? partShop.ToList();
            if (_partShopMaster.Any())
            {
                if (_partShopMaster.Where(e => e.PartShopCode == kodeCustomer).SingleOrDefault() != null) return true;
            }

            // check if fleet customer
            _fleetMaster = _fleetMaster ?? GetFleetByCustomerCode(codeCustomerList);
            if (_fleetMaster.Any())
            {
                if (_fleetMaster.Where(e => e.FleetCode == kodeCustomer).SingleOrDefault() != null) return true;
            }

            // check if dealer 
            _dealerMaster = _dealerMaster ?? GetDealerByCustomerCode(codeCustomerList);
            if (_dealerMaster.Any())
            {
                if (_dealerMaster.Where(e => e.DealerCode == kodeCustomer).SingleOrDefault() != null) return true;
            }

            // check if dealer branch
            var dealerBranch = _dealerBranchRepository.GetAllByCodes(codeCustomerList);
            _dealerBranchMaster = _dealerBranchMaster ?? dealerBranch.ToList();
            if (_dealerBranchMaster.Any())
            {
                if (_dealerBranchMaster.Where(e => e.DealerBranchCode == kodeCustomer).SingleOrDefault() != null) return true;
            }

            return false;
        }

        /// <summary>
        /// Validate Save
        /// </summary>
        /// <param name="objParam"></param>
        /// <param name="entity"></param>>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidateSave(AssistPartSalesParameterDto objParam, AssistPartSales entity, List<DNetValidationResult> validationResults)
        {
            // Validate Kode Costumer
            if (!ValidateCustomer(objParam.KodeCustomer))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataValueInvalid, FieldResource.KodeCustomer, objParam.KodeCustomer)));
            }

            // Validate Sales Channel Code
            AssistSalesChannel salesChannel = null;
            if (ValidationHelper.ValidateSalesChannel(objParam.SalesChannelCode, validationResults, ref salesChannel))
            {
                entity.AssistSalesChannel = salesChannel;
            }

            if (!objParam.SalesChannelCode.Equals("W", StringComparison.OrdinalIgnoreCase) && !objParam.SalesChannelCode.Equals("I", StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrEmpty(objParam.KodeSalesman))
                {
                    validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgMandatorySalesCodeExceptW)));
                }
            }
            
            // Validate Dealer            
            Dealer dealer = null;
            if (ValidationHelper.ValidateDealer(objParam.DealerCode, validationResults, this.DealerCode, ref dealer))
            {
                entity.Dealer = dealer;
            }

            // Validate Dealer Branch
            DealerBranch dealerBranch = null;
            if (ValidationHelper.ValidateDealerBranch(this.DealerCode, validationResults, objParam.DealerBranchCode, ref dealerBranch))
            {
                if (dealerBranch != null)
                {
                    entity.DealerBranch = dealerBranch;
                    entity.DealerBranchCode = dealerBranch.DealerBranchCode;
                }
            }

            // Validate Salesman Header
            if (!string.IsNullOrEmpty(objParam.KodeSalesman))
            {
                SalesmanHeader salesman = null;
                if (ValidationHelper.ValidateSalesmanHeader(objParam.KodeSalesman, this.DealerCode, validationResults, ref salesman))
                {
                    // TODO: Should be an object
                    entity.SalesmanHeaderID = salesman.ID;
                }
            }
            else
            {
                if (objParam.TrTraineeSalesSparepartID > 0)
                {
                    entity.KodeSalesman = objParam.TrTraineeSalesSparepartID.ToString();
                }
            }

            // Validate Sparepart Master
            //var sparePartMaster = _sparePartMasterBL.GetActivePartByPartNumber(objParam.NoParts);
            var sparePartMaster = _sparePartMasterBL.GetPartByPartNumberRowStatus(objParam.NoParts);
            if (sparePartMaster == null)
            {
                // ignore the validation if the part number has NPN as its prefix
                if (!objParam.NoParts.StartsWith("NPN", StringComparison.OrdinalIgnoreCase))
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSparepartNotFound, objParam.NoParts)));
                }
            }
            else
            {
                entity.SparePartMaster = sparePartMaster;
            }

            // Validate IsCampaign
            if (objParam.IsCampaign)
            {
                entity.CampaignNo = objParam.CampaignNo;
                entity.CampaignDescription = objParam.CampaignDescription;
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate Save
        /// </summary>
        /// <param name="objParam"></param>
        /// <param name="entity"></param>>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private async Task<bool> ValidateListPartSalesAsync(List<AssistPartSalesParameterDto> lstObjCreate, List<AssistPartSales> assistPartSalesList, List<DNetValidationResult> validationResults)
        {
            string dealerCode = lstObjCreate[0].DealerCode;

            // Validate Dealer            
            Dealer dealer = null;
            if (ValidationHelper.ValidateDealer(dealerCode, validationResults, this.DealerCode, ref dealer))
            {

                #region Get Data for validation
                // DealerBranch data based on param value
                var getDealerBranchTask = _dealerBranchRepository.GetDealerBranchByDealerIDAsync(dealer.ID);
                // SalesmanHeader data based on param val
                var getSalesmanTask = _salesmanRepository.GetByCodesAndDealerAsync(lstObjCreate.Where(e => !string.IsNullOrEmpty(e.KodeSalesman)).Select(e => e.KodeSalesman).ToList(), dealer.ID);
                // SparePartMaster data based on param value                
                var getSpMasterTask = _sparePartMasterRepository.GetByPartNumbersAsync(lstObjCreate.Where(e => !string.IsNullOrEmpty(e.NoParts)).Select(e => e.NoParts).ToList());

                // AppConfigData
                _appConfig = GetAppConfig();
                List<string> customerCodes = lstObjCreate.Where(e => !string.IsNullOrEmpty(e.KodeCustomer)).Select(e => e.KodeCustomer).ToList();

                // AssistSalesChannel data based on param value
                List<AssistSalesChannel> salesChannelMaster = new List<AssistSalesChannel>();
                salesChannelMaster = GetSalesChannelByCode(lstObjCreate.Where(e => !string.IsNullOrEmpty(e.SalesChannelCode)).Select(e => e.SalesChannelCode).ToList());

                await Task.WhenAll(getDealerBranchTask, getSalesmanTask, getSpMasterTask);
                List<DealerBranch> dealerBranchMaster = new List<DealerBranch>();
                dealerBranchMaster = getDealerBranchTask.Result;
                List<SalesmanHeader> salesmanMaster = new List<SalesmanHeader>();
                salesmanMaster = getSalesmanTask.Result;
                List<SparePartMaster> spMasterList = new List<SparePartMaster>();
                spMasterList = getSpMasterTask.Result;
                #endregion

                foreach (AssistPartSalesParameterDto item in lstObjCreate)
                {
                    #region Validation
                    // Validate Dealer Branch
                    DealerBranch dealerBranch = null;
                    if (!string.IsNullOrEmpty(item.DealerBranchCode))
                    {
                        dealerBranch = dealerBranchMaster.Any() ? dealerBranchMaster.Where(e => e.DealerBranchCode == item.DealerBranchCode).FirstOrDefault() : null;
                        if (dealerBranch == null)
                        {
                            validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDealerBranchCodeNotMatch, item.DealerBranchCode, dealerCode)));
                        }
                    }

                    // Validate Sales Channel Code
                    AssistSalesChannel salesChannel = null;
                    if (!string.IsNullOrEmpty(item.SalesChannelCode))
                    {
                        salesChannel = salesChannelMaster.Any() ? salesChannelMaster.Where(e => e.SalesChannelCode == item.SalesChannelCode).FirstOrDefault() : null;
                        if (salesChannel == null)
                        {
                            validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.SalesChannelCode, item.SalesChannelCode)));
                        }
                    }
                    if (!item.SalesChannelCode.Equals("W", StringComparison.OrdinalIgnoreCase) && !item.SalesChannelCode.Equals("I", StringComparison.OrdinalIgnoreCase))
                    {
                        if (string.IsNullOrEmpty(item.KodeSalesman))
                        {
                            validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgMandatorySalesCodeExceptW)));
                        }
                    }

                    // Validate Kode Costumer
                    if (!await ValidateCustomerAsync(item.KodeCustomer, customerCodes))
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataValueInvalid, FieldResource.KodeCustomer, item.KodeCustomer)));
                    }

                    // Validate Salesman Header
                    SalesmanHeader salesman = null;
                    if (!string.IsNullOrEmpty(item.KodeSalesman))
                    {
                        salesman = salesmanMaster.Any() ? salesmanMaster.Where(e => e.SalesmanCode == item.KodeSalesman).FirstOrDefault() : null;
                        if (salesman == null)
                        {
                            validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.SalesmanHeader, item.KodeSalesman)));
                        }
                    }

                    // Validate Sparepart Master
                    SparePartMaster sparePartMaster = null;
                    if (!string.IsNullOrEmpty(item.NoParts))
                    {
                        sparePartMaster = spMasterList.Any() ? spMasterList.Where(e => e.PartNumber == item.NoParts).SingleOrDefault() : null;
                        if (sparePartMaster == null)
                        {
                            // ignore the validation if the part number has NPN as its prefix
                            if (!item.NoParts.StartsWith("NPN", StringComparison.OrdinalIgnoreCase))
                            {
                                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSparepartNotFound, item.NoParts)));
                            }
                        }
                    }
                    #endregion

                    #region Mapping object
                    if (!validationResults.Any())
                    {
                        AssistPartSales entity = _mapper.Map<AssistPartSales>(item);

                        entity.DealerID = dealer.ID;
                        entity.Dealer = dealer;

                        // Set Dealer Branch
                        if (dealerBranch != null)
                        {
                            entity.DealerBranchID = dealerBranch.ID;
                            entity.DealerBranch = dealerBranch;
                        }

                        // Set Sales Channel
                        if (salesChannel != null)
                        {
                            entity.SalesChannelID = salesChannel.ID;
                            entity.AssistSalesChannel = salesChannel;
                        }

                        // Set Salesman
                        if (!string.IsNullOrEmpty(item.KodeSalesman) && salesman != null)
                        {
                            entity.SalesmanHeaderID = salesman.ID;
                        }
                        else
                        {
                            if (item.TrTraineeSalesSparepartID > 0)
                            {
                                entity.KodeSalesman = item.TrTraineeSalesSparepartID.ToString();
                            }
                        }

                        // Set SparePart Master
                        if (sparePartMaster != null)
                        {
                            entity.SparepartMasterID = sparePartMaster.ID;
                            entity.SparePartMaster = sparePartMaster;
                        }

                        // Validate IsCampaign
                        if (item.IsCampaign)
                        {
                            entity.CampaignNo = item.CampaignNo;
                            entity.CampaignDescription = item.CampaignDescription;
                        }

                        entity.CreatedBy = DNetUserName;
                        entity.CreatedTime = DateTime.Now;
                        entity.LastUpdateTime = DateTime.Now;

                        assistPartSalesList.Add(entity);
                    }
                    #endregion
                }
            }

            return !validationResults.Any();
        }

        /// <summary>
        /// Validate Save
        /// </summary>
        /// <param name="objParam"></param>
        /// <param name="entity"></param>>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidateListPartSales(List<AssistPartSalesParameterDto> lstObjCreate, List<AssistPartSales> assistPartSalesList, List<DNetValidationResult> validationResults)
        {
            string dealerCode = lstObjCreate[0].DealerCode;

            // Validate Dealer            
            Dealer dealer = null;
            if (ValidationHelper.ValidateDealer(dealerCode, validationResults, this.DealerCode, ref dealer))
            {
                #region Get Data for validation
                // DealerBranch data based on param value
                List<DealerBranch> dealerBranchMaster = new List<DealerBranch>();
                dealerBranchMaster = _dealerBranchRepository.GetDealerBranchByDealerID(dealer.ID);

                // AppConfigData
                _appConfig = GetAppConfig();
                List<string> customerCodes = lstObjCreate.Where(e => !string.IsNullOrEmpty(e.KodeCustomer)).Select(e => e.KodeCustomer).ToList();

                // AssistSalesChannel data based on param value
                List<AssistSalesChannel> salesChannelMaster = new List<AssistSalesChannel>();
                salesChannelMaster = GetSalesChannelByCode(lstObjCreate.Where(e => !string.IsNullOrEmpty(e.SalesChannelCode)).Select(e => e.SalesChannelCode).ToList());

                // SalesmanHeader data based on param val                
                List<SalesmanHeader> salesmanMaster = new List<SalesmanHeader>();
                salesmanMaster = _salesmanRepository.GetByCodesAndDealer(lstObjCreate.Where(e => !string.IsNullOrEmpty(e.KodeSalesman)).Select(e => e.KodeSalesman).ToList(), dealer.ID);

                // SparePartMaster data based on param value
                var spMasterDataList = _sparePartMasterRepository.GetByPartNumbers(lstObjCreate.Where(e => !string.IsNullOrEmpty(e.NoParts)).Select(e => e.NoParts).ToList());
                List<SparePartMaster> spMasterList = spMasterDataList.ToList();
                #endregion

                foreach (AssistPartSalesParameterDto item in lstObjCreate)
                {
                    #region Validation
                    // Validate Dealer Branch
                    DealerBranch dealerBranch = null;
                    if (!string.IsNullOrEmpty(item.DealerBranchCode))
                    {
                        dealerBranch = dealerBranchMaster.Any() ? dealerBranchMaster.Where(e => e.DealerBranchCode == item.DealerBranchCode).FirstOrDefault() : null;
                        if (dealerBranch == null)
                        {
                            validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDealerBranchCodeNotMatch, item.DealerBranchCode, dealerCode)));
                        }
                    }

                    // Validate Sales Channel Code
                    AssistSalesChannel salesChannel = null;
                    if (!string.IsNullOrEmpty(item.SalesChannelCode))
                    {
                        salesChannel = salesChannelMaster.Any() ? salesChannelMaster.Where(e => e.SalesChannelCode == item.SalesChannelCode).FirstOrDefault() : null;
                        if (salesChannel == null)
                        {
                            validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.SalesChannelCode, item.SalesChannelCode)));
                        }
                    }
                    if (!item.SalesChannelCode.Equals("W", StringComparison.OrdinalIgnoreCase) && !item.SalesChannelCode.Equals("I", StringComparison.OrdinalIgnoreCase))
                    {
                        if (string.IsNullOrEmpty(item.KodeSalesman))
                        {
                            validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgMandatorySalesCodeExceptW)));
                        }
                    }

                    // Validate Kode Costumer
                    if (!ValidateCustomer(item.KodeCustomer, customerCodes))
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataValueInvalid, FieldResource.KodeCustomer, item.KodeCustomer)));
                    }

                    // Validate Salesman Header
                    SalesmanHeader salesman = null;
                    if (!string.IsNullOrEmpty(item.KodeSalesman))
                    {
                        salesman = salesmanMaster.Any() ? salesmanMaster.Where(e => e.SalesmanCode == item.KodeSalesman).FirstOrDefault() : null;
                        if (salesman == null)
                        {
                            validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.SalesmanHeader, item.KodeSalesman)));
                        }
                    }

                    // Validate Sparepart Master
                    SparePartMaster sparePartMaster = null;
                    if (!string.IsNullOrEmpty(item.NoParts))
                    {
                        sparePartMaster = spMasterList.Any() ? spMasterList.Where(e => e.PartNumber == item.NoParts).SingleOrDefault() : null;
                        if (sparePartMaster == null)
                        {
                            // ignore the validation if the part number has NPN as its prefix
                            if (!item.NoParts.StartsWith("NPN", StringComparison.OrdinalIgnoreCase))
                            {
                                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSparepartNotFound, item.NoParts)));
                            }
                        }
                    }
                    #endregion

                    #region Mapping object
                    if (!validationResults.Any())
                    {
                        AssistPartSales entity = _mapper.Map<AssistPartSales>(item);

                        entity.DealerID = dealer.ID;
                        entity.Dealer = dealer;

                        // Set Dealer Branch
                        if (dealerBranch != null)
                        {
                            entity.DealerBranchID = dealerBranch.ID;
                            entity.DealerBranch = dealerBranch;
                        }

                        // Set Sales Channel
                        if (salesChannel != null)
                        {
                            entity.SalesChannelID = salesChannel.ID;
                            entity.AssistSalesChannel = salesChannel;
                        }

                        // Set Salesman
                        if (!string.IsNullOrEmpty(item.KodeSalesman) && salesman != null)
                        {
                            entity.SalesmanHeaderID = salesman.ID;
                        }
                        else
                        {
                            if (item.TrTraineeSalesSparepartID > 0)
                            {
                                entity.KodeSalesman = item.TrTraineeSalesSparepartID.ToString();
                            }
                        }

                        // Set SparePart Master
                        if (sparePartMaster != null)
                        {
                            entity.SparepartMasterID = sparePartMaster.ID;
                            entity.SparePartMaster = sparePartMaster;
                        }

                        // Validate IsCampaign
                        if (item.IsCampaign)
                        {
                            entity.CampaignNo = item.CampaignNo;
                            entity.CampaignDescription = item.CampaignDescription;
                        }

                        entity.CreatedBy = DNetUserName;
                        entity.CreatedTime = DateTime.Now;
                        entity.LastUpdateTime = DateTime.Now;

                        assistPartSalesList.Add(entity);
                    }
                    #endregion
                }
            }

            return !validationResults.Any();
        }

        /// <summary>
        /// Update spk with transaction manager
        /// </summary>
        /// <param name="objDomain"></param>
        /// <returns></returns>
        private int UpdateWithTransactionManager(AssistPartSales objDomain)
        {
            // set default result
            int result = -1;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // add command to update spk
                    _transactionManager.AddUpdate(objDomain, DNetUserName);
                    _transactionManager.PerformTransaction();
                    result = objDomain.ID;
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
        /// Insert with transaction manager
        /// </summary>
        /// <param name="newEntity"></param>
        /// <returns></returns>
        private int InsertWithTransactionManager(AssistPartSales newEntity)
        {
            int result = -1;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();
                    this._transactionManager.AddInsert(newEntity, DNetUserName);
                    this._transactionManager.PerformTransaction();
                    result = newEntity.ID;
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
        /// Insert with transaction manager
        /// </summary>
        /// <param name="newEntity"></param>
        /// <returns></returns>
        private int InsertWithTransactionManager(List<AssistPartSales> lstNewEntity)
        {
            int result = -1;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    foreach (var newEntity in lstNewEntity)
                    {
                        this._transactionManager.AddInsert(newEntity, DNetUserName);
                    }

                    this._transactionManager.PerformTransaction();
                    result = 1;
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
            if (args.DomainObject.GetType() == typeof(AssistPartSales))
            {
                ((AssistPartSales)args.DomainObject).ID = args.ID;
                ((AssistPartSales)args.DomainObject).MarkLoaded();
            }
        }

        private List<AssistSalesChannel> GetSalesChannelByCode(List<string> codes)
        {
            List<AssistSalesChannel> result = new List<AssistSalesChannel>();
            if (codes.Any())
            {
                // initialize the mapper
                var _mapper = MapperFactory.GetInstance().GetMapper(typeof(AssistSalesChannel).ToString());

                codes = codes.Select(e => { e = "'" + e + "'"; return e; }).ToList();
                string salesChannelCodes = "(" + string.Join(", ", codes) + ")";

                var criterias = new CriteriaComposite(new Criteria(typeof(AssistSalesChannel), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(AssistSalesChannel), "SalesChannelCode", MatchType.InSet, salesChannelCodes));

                // get by criteria                
                result = _mapper.RetrieveByCriteria(criterias).Cast<AssistSalesChannel>().ToList();
            }

            return result;
        }

        private List<VWI_Fleet> GetFleetByCustomerCode(List<string> codes)
        {
            List<VWI_Fleet> result = new List<VWI_Fleet>();
            if (codes.Any())
            {
                // initialize the mapper
                var _mapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_Fleet).ToString());

                codes = codes.Select(e => { e = "'" + e + "'"; return e; }).ToList();
                string fleetCodes = "(" + string.Join(", ", codes) + ")";

                var criterias = new CriteriaComposite(new Criteria(typeof(VWI_Fleet), "FleetCode", MatchType.InSet, fleetCodes));

                // get by criteria                
                result = _mapper.RetrieveByCriteria(criterias).Cast<VWI_Fleet>().ToList();
            }

            return result;
        }

        private List<Dealer> GetDealerByCustomerCode(List<string> codes)
        {
            List<Dealer> result = new List<Dealer>();
            if (codes.Any())
            {
                // initialize the mapper
                var _mapper = MapperFactory.GetInstance().GetMapper(typeof(Dealer).ToString());

                codes = codes.Select(e => { e = "'" + e + "'"; return e; }).ToList();
                string dealerCodes = "(" + string.Join(", ", codes) + ")";

                var criterias = new CriteriaComposite(new Criteria(typeof(Dealer), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(Dealer), "DealerCode", MatchType.InSet, dealerCodes));

                // get by criteria                
                result = _mapper.RetrieveByCriteria(criterias).Cast<Dealer>().ToList();
            }

            return result;
        }

        private AppConfig GetAppConfig()
        {
            AppConfig appConfig = _appConfBL.GetConfigByName("CustomerRetailCode");
            return appConfig;
        }


        public ResponseBase<List<AssistPartSalesDto>> Read(AssistPartSalesFilterDto filterDto, int pageSize)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

