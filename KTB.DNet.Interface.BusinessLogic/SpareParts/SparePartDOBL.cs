#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartDO business logic class
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
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class SparePartDOBL : AbstractBusinessLogic, ISparePartDOBL
    {
        #region Variables
        private readonly IMapper _sparepartdoMapper;
        private readonly IMapper _sparepartdoHaveBillingMapper;
        private readonly IMapper _sparePartDetailBillingMapper;
        private readonly AutoMapper.IMapper _mapper;
        ISparePartPODORepository<VWI_SparePartPODOHaveBillingOne, int> _pODORepository;
        ISparePartPODORepository<VWI_SparePartPODOHaveBilling, int> _pODOOldRepository;
        ISparePartPODODetailRepository<VWI_SparePartPODOHaveBillingDetail, int> _pODODetailRepository;
        #endregion

        #region Constructor
        public SparePartDOBL(ISparePartPODORepository<VWI_SparePartPODOHaveBillingOne, int> pODORepository,
            ISparePartPODORepository<VWI_SparePartPODOHaveBilling, int> pODOOldRepository,
            ISparePartPODODetailRepository<VWI_SparePartPODOHaveBillingDetail, int> pODODetailRepository)
        {
            _sparepartdoMapper = MapperFactory.GetInstance().GetMapper(typeof(SparePartDO).ToString());
            _sparepartdoHaveBillingMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_SparePartPODOHaveBilling).ToString());
            _sparePartDetailBillingMapper = MapperFactory.GetInstance().GetMapper(typeof(SparePartBillingDetail).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _pODORepository = pODORepository;
            _pODOOldRepository = pODOOldRepository;
            _pODODetailRepository = pODODetailRepository;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get SparePartDO by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<SparePartDODto>> Read(SparePartDOFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(SparePartDO), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(SparePartDO), "Dealer.DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<SparePartDODto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(SparePartDO), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(SparePartDO), filterDto, sortColl);

                // get data
                var data = _sparepartdoMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<SparePartDO>().ToList();
                    var listData = list.Select(item => _mapper.Map<SparePartDODto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SparePartDO), filterDto);
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
        /// Delete SparePartDO by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<SparePartDODto> Delete(int id)
        {
            var result = new ResponseBase<SparePartDODto>();

            try
            {
                var sparepartdo = (SparePartDO)_sparepartdoMapper.Retrieve(id);
                if (sparepartdo != null)
                {
                    sparepartdo.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _sparepartdoMapper.Update(sparepartdo, DNetUserName);
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
        /// Create a new SparePartDO
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<SparePartDODto> Create(SparePartDOParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update SparePartDO
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<SparePartDODto> Update(SparePartDOParameterDto objUpdate)
        {
            return null;
        }

        /// <summary>
        /// Read
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<DeliveryOrderBillingResponseDto>> ReadDeliveryOrderBilling(SparePartDOFilterDto filterDto, int pageSize)
        {
            #region Fields
            // default filter to get the Active Row Status only            
            var result = new ResponseBase<List<DeliveryOrderBillingResponseDto>>();
            var list = new List<VWI_SparePartPODOHaveBilling>();
            var sortColl = new SortCollection();
            SparePartBillingDetail sparePartBillingDetail = null;
            DeliveryOrderBillingResponseDto orderBilling = null;
            DeliveryOrderDetailResponseDto detail = null;
            SparePartPOEstimateDetail sparePartPOEstimateDetail = null;
            string rawSql = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            // declare
            List<DeliveryOrderBillingResponseDto> listData = new List<DeliveryOrderBillingResponseDto>();
            #endregion

            try
            {
                // calculate the skip 
                int skip = filterDto.pages < 1 ? 0 : (filterDto.pages - 1) * pageSize;

                // define the raw sql query
                if (IsSearchBySparePartBillingCriteria(filterDto))
                {
                    var criterias = Helper.BuildCriteria(typeof(VWI_SparePartPODOHaveBilling), filterDto);

                    rawSql = Helper.GetDapperQueryView(RepositoryResource.ViewPurchaseReceipt, filterDto, criterias, sortColl, typeof(VWI_SparePartPODOHaveBilling), "VWI_PODOHaveBilling", DealerCode, true);
                }
                else
                {
                    var criterias = new CriteriaComposite(new Criteria(typeof(VWI_SparePartPODOHaveBilling), "DealerCode", MatchType.Exact, DealerCode));

                    var sql = Helper.GenerateSQLFromCriteriasAndSort(typeof(VWI_SparePartPODOHaveBilling), filterDto, sortColl, criterias);

                    rawSql = "SELECT * FROM VWI_PODOHaveBilling " + sql.ToString();
                }

                list = _pODOOldRepository.Search(rawSql, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                totalRow = list.Count;

                if (totalRow > 0)
                {
                    foreach (var item in list)
                    {
                        // map it
                        orderBilling = _mapper.Map<DeliveryOrderBillingResponseDto>(item);

                        if (item.SparePartDODetails.Count > 0)
                        {
                            orderBilling.DeliveryOrderDetail = new List<DeliveryOrderDetailResponseDto>();

                            foreach (SparePartDODetail doDetail in item.SparePartDODetails)
                            {
                                // Get billing Detail                                                                 
                                if (GetBillingDetails(doDetail, ref sparePartBillingDetail))
                                {
                                    // define Billing Detail
                                    detail = _mapper.Map<DeliveryOrderDetailResponseDto>(doDetail);
                                    detail.Qty = sparePartBillingDetail.Quantity;
                                    detail.Tax = sparePartBillingDetail.Tax;

                                    detail.Discount = sparePartBillingDetail.Discount;
                                    detail.RetailPrice = sparePartBillingDetail.RetailPrice;

                                    //get discount
                                    if (doDetail.SparePartPOEstimate != null)
                                    {
                                        detail.SONumber = doDetail.SparePartPOEstimate.SONumber;

                                        //sparePartPOEstimateDetail = doDetail.SparePartPOEstimate.SparePartPOEstimateDetails.Cast<SparePartPOEstimateDetail>().ToList().Where(e => e.PartNumber == doDetail.SparePartMaster.PartNumber).FirstOrDefault();
                                        //if (sparePartPOEstimateDetail != null)
                                        //{
                                        //    detail.Discount = sparePartPOEstimateDetail.Discount;
                                        //    detail.RetailPrice = sparePartPOEstimateDetail.RetailPrice;
                                        //}
                                    }

                                    orderBilling.DeliveryOrderDetail.Add(detail);
                                }
                            }

                            // add to list if it has detail
                            if (orderBilling.DeliveryOrderDetail.Count > 0)
                            {
                                listData.Add(orderBilling);
                            }
                        }
                    }

                    if (listData.Count == 0)
                    {
                        ErrorMsgHelper.DataNotFound(result.messages, typeof(SparePartDO), filterDto);
                    }
                    else
                    {
                        result.lst = listData;
                        result.total = totalRow;
                    }

                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SparePartDO), filterDto);
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
        /// ReadDeliveryOrderBillingWithCriteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<DeliveryOrderBillingResponseDto>> ReadDeliveryOrderBillingWithCriteria(SparePartDOFilterDto filterDto, int pageSize)
        {
            var result = new ResponseBase<List<DeliveryOrderBillingResponseDto>>();
            var detailList = new List<VWI_SparePartPODOHaveBillingDetail>();

            try
            {
                var criteria = new CriteriaComposite(new Criteria(typeof(VWI_SparePartPODOHaveBilling), "DealerCode", MatchType.Exact, DealerCode));
                criteria = Helper.UpdateCriteria(typeof(VWI_SparePartPODOHaveBillingOne), filterDto, criteria);

                CriteriaComposite innerQueryCriteria = null;
                innerQueryCriteria = Helper.UpdateCriteria(typeof(SparePartBilling), filterDto, innerQueryCriteria, GetInnerQueryParams());

                var sortColl = new SortCollection();
                sortColl = Helper.UpdateSortColumn(typeof(VWI_SparePartPODOHaveBillingOne), filterDto, sortColl);

                int totalRow = 0;
                int filteredTotalRow = 0;

                List<VWI_SparePartPODOHaveBillingOne> list = _pODORepository.Search(
                                    criteria, innerQueryCriteria, sortColl,
                                    filterDto.pages, pageSize, out filteredTotalRow, out totalRow);


                if (list != null && list.Count > 0)
                {
                    List<int> listOfDOId = list.Select(d => d.SparePartDOID).Distinct().ToList();
                    detailList = _pODODetailRepository.GetByListOfSparePartDOId(listOfDOId);

                    List<DeliveryOrderBillingResponseDto> listData = new List<DeliveryOrderBillingResponseDto>();
                    DeliveryOrderBillingResponseDto orderBilling = null;
                    foreach (var item in list)
                    {
                        // map it
                        orderBilling = _mapper.Map<DeliveryOrderBillingResponseDto>(item);
                        orderBilling.DeliveryOrderDetail = new List<DeliveryOrderDetailResponseDto>();

                        List<VWI_SparePartPODOHaveBillingDetail> vwiDetailList = new List<VWI_SparePartPODOHaveBillingDetail>();
                        vwiDetailList = detailList.Where(e => e.SparePartDOID == item.SparePartDOID).ToList();

                        List<DeliveryOrderDetailResponseDto> detailDto = new List<DeliveryOrderDetailResponseDto>();
                        detailDto = _mapper.Map<List<DeliveryOrderDetailResponseDto>>(vwiDetailList);
                        orderBilling.DeliveryOrderDetail.AddRange(detailDto);

                        listData.Add(orderBilling);

                    }

                    if (listData.Count == 0)
                    {
                        ErrorMsgHelper.DataNotFound(result.messages, typeof(SparePartDO), filterDto);
                    }
                    else
                    {
                        result.lst = listData;
                        result.total = filteredTotalRow;
                    }

                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SparePartDO), filterDto);
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
        /// Read
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<DeliveryOrderBillingResponseDto>> ReadDeliveryOrderBilling1(SparePartDOFilterDto filterDto, int pageSize)
        {
            #region Fields
            // default filter to get the Active Row Status only            
            var result = new ResponseBase<List<DeliveryOrderBillingResponseDto>>();
            var list = new List<VWI_SparePartPODOHaveBillingOne>();
            var detailList = new List<VWI_SparePartPODOHaveBillingDetail>();
            var sortColl = new SortCollection();
            DeliveryOrderBillingResponseDto orderBilling = null;
            string rawSql = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            // declare
            List<DeliveryOrderBillingResponseDto> listData = new List<DeliveryOrderBillingResponseDto>();
            #endregion

            try
            {
                // define the raw sql query
                if (IsSearchBySparePartBillingCriteria(filterDto))
                {
                    var criterias = Helper.BuildCriteria(typeof(VWI_SparePartPODOHaveBilling), filterDto);

                    rawSql = Helper.GetDapperQueryView(RepositoryResource.ViewPurchaseReceipt, filterDto, criterias, sortColl, typeof(VWI_SparePartPODOHaveBilling), "VWI_PODOHaveBilling", DealerCode, true);
                }
                else
                {
                    var criterias = new CriteriaComposite(new Criteria(typeof(VWI_SparePartPODOHaveBilling), "DealerCode", MatchType.Exact, DealerCode));

                    var sql = Helper.GenerateSQLFromCriteriasAndSort(typeof(VWI_SparePartPODOHaveBilling), filterDto, sortColl, criterias);

                    rawSql = string.Format(RepositoryResource.ViewPODO, sql.ToString());
                }


                list = _pODORepository.Search(rawSql, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (list.Count > 0)
                {
                    List<int> listOfDOId = list.Select(d => d.SparePartDOID).Distinct().ToList();
                    detailList = _pODODetailRepository.GetByListOfSparePartDOId(listOfDOId);
                }

                if (totalRow > 0)
                {
                    foreach (var item in list)
                    {
                        // map it
                        orderBilling = _mapper.Map<DeliveryOrderBillingResponseDto>(item);
                        orderBilling.DeliveryOrderDetail = new List<DeliveryOrderDetailResponseDto>();

                        List<VWI_SparePartPODOHaveBillingDetail> vwiDetailList = new List<VWI_SparePartPODOHaveBillingDetail>();
                        vwiDetailList = detailList.Where(e => e.SparePartDOID == item.SparePartDOID).ToList();

                        List<DeliveryOrderDetailResponseDto> detailDto = new List<DeliveryOrderDetailResponseDto>();
                        detailDto = _mapper.Map<List<DeliveryOrderDetailResponseDto>>(vwiDetailList);
                        orderBilling.DeliveryOrderDetail.AddRange(detailDto);

                        listData.Add(orderBilling);

                    }

                    if (listData.Count == 0)
                    {
                        ErrorMsgHelper.DataNotFound(result.messages, typeof(SparePartDO), filterDto);
                    }
                    else
                    {
                        result.lst = listData;
                        result.total = totalRow;
                    }

                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SparePartDO), filterDto);
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
        /// Read
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<DeliveryOrderBillingResponseDto>> ReadDeliveryOrderBilling2(SparePartDOFilterDto filterDto, int pageSize)
        {
            #region Fields
            // default filter to get the Active Row Status only            
            var result = new ResponseBase<List<DeliveryOrderBillingResponseDto>>();
            var list = new List<VWI_SparePartPODOHaveBillingHeaderDetail>();
            var sortColl = new SortCollection();
            string rawSql = string.Empty;
            int totalRow = 0;
            // declare
            List<DeliveryOrderBillingResponseDto> listData = new List<DeliveryOrderBillingResponseDto>();
            #endregion

            try
            {
                // calculate the skip 
                int skip = filterDto.pages < 1 ? 0 : (filterDto.pages - 1) * pageSize;

                // define the raw sql query
                if (IsSearchBySparePartBillingCriteria(filterDto))
                {
                    var criterias = Helper.BuildCriteria(typeof(VWI_SparePartPODOHaveBillingHeaderDetail), filterDto);

                    rawSql = Helper.GetDapperQueryView(RepositoryResource.ViewPurchaseReceipt, filterDto, criterias, sortColl, typeof(VWI_SparePartPODOHaveBillingHeaderDetail), "VWI_PODOHaveBillingHeaderDetail", DealerCode, true);
                }
                else
                {
                    var criterias = new CriteriaComposite(new Criteria(typeof(VWI_SparePartPODOHaveBillingHeaderDetail), "DealerCode", MatchType.Exact, DealerCode));

                    var sql = Helper.GenerateSQLFromCriteriasAndSort(typeof(VWI_SparePartPODOHaveBillingHeaderDetail), filterDto, sortColl, criterias);

                    rawSql = "SELECT * FROM VWI_PODOHaveBillingHeaderDetail " + sql.ToString();
                }

                list = _pODODetailRepository.GetByQuery(rawSql);

                totalRow = list.Count;

                // filter out the data based on the paging                                            
                if (sortColl != null && sortColl.Count > 0)
                    list = list.Skip(skip).Take(pageSize).ToList();
                else
                    list = list.OrderBy(x => x.ID).Skip(skip).Take(pageSize).ToList();


                if (totalRow > 0)
                {

                    List<DeliveryOrderBillingResponseDto> header = _mapper.Map<List<DeliveryOrderBillingResponseDto>>(list);
                    ///.GroupBy(e => e.DONumber).ToList();

                    //listData = header.Select(e => e.DeliveryOrderDetail = new List<DeliveryOrderDetailResponseDto> {

                    //})

                    //listData = list




                    if (listData.Count == 0)
                    {
                        ErrorMsgHelper.DataNotFound(result.messages, typeof(SparePartDO), filterDto);
                    }
                    else
                    {
                        result.lst = listData;
                        result.total = totalRow;
                    }

                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SparePartDO), filterDto);
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
        /// Get billing details
        /// </summary>
        /// <param name="doDetail"></param>
        /// <returns></returns>
        private bool GetBillingDetails(SparePartDODetail doDetail, ref SparePartBillingDetail billingDetail)
        {
            var billingCriterias = new CriteriaComposite(new Criteria(typeof(SparePartBillingDetail), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            billingCriterias.opAnd(new Criteria(typeof(SparePartBillingDetail), "SparePartDODetail.ID", MatchType.Exact, doDetail.ID.ToString()));
            billingCriterias.opAnd(new Criteria(typeof(SparePartBillingDetail), "SparePartBilling.BillingDate", MatchType.IsNotNull, null));

            //ArrayList billingDetails = _sparePartDetailBillingMapper.RetrieveByCriteria(billingCriterias);
            //if (billingDetails.Count > 0)
            //{
            //    billingDetail = billingDetails[0] as SparePartBillingDetail;
            //    return true;
            //}

            //return false;

            string rawSql = "Select SparePartBillingDetail.* from SparePartBillingDetail " + billingCriterias.ToString();

            billingDetail = _pODODetailRepository.GetSparePartBillingDetailByQuery(rawSql);

            if (billingDetail != null)
            { return true; }

            return false;
        }

        /// <summary>
        /// GetInnerQueryParams
        /// </summary>
        /// <returns></returns>
        private List<string> GetInnerQueryParams()
        {
            return new List<string>() { "BillingDate", "BillingNumber", "LastUpdateTime" };
        }

        /// <summary>
        /// Check if it is has sparepart billing criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <returns></returns>
        private bool IsSearchBySparePartBillingCriteria(FilterDtoBase filterDto)
        {
            if (filterDto.find != null && filterDto.find.Count > 0)
            {
                foreach (var filter in filterDto.find)
                {
                    if (filter.PropertyName != null)
                    {
                        if (filter.PropertyName.Equals("DealerID") ||
                            filter.PropertyName.Equals("BillingDate") ||
                            filter.PropertyName.Equals("BillingNumber") ||
                            filter.PropertyName.Equals("LastUpdateTime"))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        #endregion
    }
}

