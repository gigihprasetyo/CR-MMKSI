#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_SparePartPOEstimate business logic class
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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class VWI_SparePartPOEstimateBL : AbstractBusinessLogic, IVWI_SparePartPOEstimateBL
    {
        #region Variables
        private readonly IMapper _sparePartBillingDetail;
        private readonly IMapper _vwi_POEstimateHaveBillingMapper;
        private readonly AutoMapper.IMapper _mapper;
        private SparePartMasterBL _sparePartMasterBL;
        ISparePartPOEstimateRepository<VWI_POEstimateHaveBillingOne, int> _pOEstimateRepository;
        ISparePartPOEstimateDetailRepository<VWI_POEstimateHaveBillingDetail, int> _pOEstimateDetailRepository;
        #endregion

        #region Constructor
        public VWI_SparePartPOEstimateBL(ISparePartPOEstimateRepository<VWI_POEstimateHaveBillingOne, int> pOEstimateRepository,
            ISparePartPOEstimateDetailRepository<VWI_POEstimateHaveBillingDetail, int> pOEstimateDetailRepository)
        {
            _sparePartBillingDetail = MapperFactory.GetInstance().GetMapper(typeof(SparePartBillingDetail).ToString());
            _vwi_POEstimateHaveBillingMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_POEstimateHaveBilling).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _sparePartMasterBL = new SparePartMasterBL(_mapper);
            _pOEstimateRepository = pOEstimateRepository;
            _pOEstimateDetailRepository = pOEstimateDetailRepository;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get SparePartPOEstimate by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<SparePartPOEstimateDto>> Read(SparePartPOEstimateFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_POEstimateHaveBilling), "DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<SparePartPOEstimateDto>>();
            var sortColl = new SortCollection();

            try
            {
                // define sql
                var sql = Helper.GenerateSQLFromCriteriasAndSort(typeof(VWI_POEstimateHaveBilling), filterDto, sortColl, criterias);

                // get data                
                var data = _vwi_POEstimateHaveBillingMapper.RetrieveSP("SELECT * FROM VWI_POEstimateHaveBilling " + sql);
                if (data.Count > 0)
                {
                    // calculate the skip 
                    int skip = filterDto.pages < 1 ? 0 : (filterDto.pages - 1) * pageSize;

                    // filter out the data based on the paging
                    var list = data.Cast<VWI_POEstimateHaveBilling>().OrderBy(x => x.SparePartPOEstimateID).Skip(skip).Take(pageSize).ToList();
                    List<SparePartPOEstimateDto> listData = new List<SparePartPOEstimateDto>();

                    foreach (var item in list)
                    {
                        SparePartPOEstimateDto sparePartPOEstimateDto = new SparePartPOEstimateDto();
                        sparePartPOEstimateDto = _mapper.Map<SparePartPOEstimateDto>(item);
                        sparePartPOEstimateDto.SparePartPOEstimateDetails = new List<SparePartPOEstimateDetailDto>();

                        var details = item.SparePartPOEstimateDetails.Cast<SparePartPOEstimateDetail>().ToList();

                        foreach (var dt in details)
                        {
                            SparePartPOEstimateDetailDto detailDto = new SparePartPOEstimateDetailDto();
                            detailDto = _mapper.Map<SparePartPOEstimateDetail, SparePartPOEstimateDetailDto>(dt);

                            var spMaster = _sparePartMasterBL.GetActivePartByPartNumber(dt.PartNumber);
                            detailDto.UOM = spMaster != null ? spMaster.UoM : null;

                            // get tax from billing detail
                            detailDto.Tax = GetSparepartBillingDetailTax(item, dt);

                            sparePartPOEstimateDto.SparePartPOEstimateDetails.Add(detailDto);
                        }

                        // Set Header if have detail
                        if (sparePartPOEstimateDto.SparePartPOEstimateDetails.Count > 0)
                        {
                            // add to list
                            listData.Add(sparePartPOEstimateDto);
                        }
                    }

                    result.lst = listData;
                    result.total = data.Count;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_POEstimateHaveBilling), filterDto);
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
        /// Get SparePartPOEstimate by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<SparePartPOEstimateDto>> Read1(SparePartPOEstimateFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_POEstimateHaveBilling), "DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<SparePartPOEstimateDto>>();
            var sortColl = new SortCollection();
            var list = new List<VWI_POEstimateHaveBillingOne>();
            var detailList = new List<VWI_POEstimateHaveBillingDetail>();
            int totalRow = 0;
            List<SparePartPOEstimateDto> listData = new List<SparePartPOEstimateDto>();
            int filteredTotalRow = 0;

            try
            {
                // define sql
                var sql = Helper.GenerateSQLFromCriteriasAndSort(typeof(VWI_POEstimateHaveBilling), filterDto, sortColl, criterias);

                list = _pOEstimateRepository.Search(sql, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (list.Count > 0)
                {
                    List<int> listOfPOId = list.Select(d => d.SparePartPOEstimateID).Distinct().ToList();

                    if (listOfPOId != null && listOfPOId.Count > 0)
                    {
                        detailList = _pOEstimateDetailRepository.GetByListOfPOEstimateId(listOfPOId);
                    }

                    foreach (var item in list)
                    {
                        SparePartPOEstimateDto sparePartPOEstimateDto = new SparePartPOEstimateDto();
                        sparePartPOEstimateDto = _mapper.Map<SparePartPOEstimateDto>(item);
                        sparePartPOEstimateDto.SparePartPOEstimateDetails = new List<SparePartPOEstimateDetailDto>();

                        List<VWI_POEstimateHaveBillingDetail> vwiDetailList = new List<VWI_POEstimateHaveBillingDetail>();
                        vwiDetailList = detailList.Where(e => e.SparePartPOEstimateID == item.SparePartPOEstimateID).ToList();
                        vwiDetailList = vwiDetailList.Select(e => { e.UOM = e.ActiveStatus == 1 ? "" : e.UOM; return e; }).ToList();

                        List<SparePartPOEstimateDetailDto> detailDto = new List<SparePartPOEstimateDetailDto>();
                        detailDto = _mapper.Map<List<SparePartPOEstimateDetailDto>>(vwiDetailList);
                        sparePartPOEstimateDto.SparePartPOEstimateDetails.AddRange(detailDto);

                        // add to list
                        listData.Add(sparePartPOEstimateDto);
                    }

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_POEstimateHaveBilling), filterDto);
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
        /// ReadWithCriteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<SparePartPOEstimateDto>> ReadWithCriteria(SparePartPOEstimateFilterDto filterDto, int pageSize)
        {
            var result = new ResponseBase<List<SparePartPOEstimateDto>>();

            try
            {
                var sortColl = new SortCollection();

                int totalRow = 0;
                int filteredTotalRow = 0;

                var criteria = new CriteriaComposite(new Criteria(typeof(VWI_POEstimateHaveBilling), "DealerCode", MatchType.Exact, DealerCode));
                criteria = Helper.UpdateCriteria(typeof(VWI_POEstimateHaveBilling), filterDto, criteria);

                sortColl = Helper.UpdateSortColumn(typeof(VWI_POEstimateHaveBilling), filterDto, sortColl);

                List<VWI_POEstimateHaveBillingOne> listOfPOEstimate = _pOEstimateRepository.Search(criteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                List<SparePartPOEstimateDto> listResult = new List<SparePartPOEstimateDto>();
                if (listOfPOEstimate != null && listOfPOEstimate.Count > 0)
                {
                    var details = new List<VWI_POEstimateHaveBillingDetail>();

                    List<int> listOfPOId = listOfPOEstimate.Select(d => d.SparePartPOEstimateID).Distinct().ToList();

                    if (listOfPOId != null && listOfPOId.Count > 0)
                    {
                        details = _pOEstimateDetailRepository.GetByListOfPOEstimateId(listOfPOId);
                    }

                    foreach (var item in listOfPOEstimate)
                    {
                        SparePartPOEstimateDto sparePartPOEstimateDto = new SparePartPOEstimateDto();
                        sparePartPOEstimateDto = _mapper.Map<SparePartPOEstimateDto>(item);
                        sparePartPOEstimateDto.SparePartPOEstimateDetails = new List<SparePartPOEstimateDetailDto>();

                        List<VWI_POEstimateHaveBillingDetail> vwiDetailList = new List<VWI_POEstimateHaveBillingDetail>();
                        vwiDetailList = details.Where(e => e.SparePartPOEstimateID == item.SparePartPOEstimateID)
                                                    .Select(e => { e.UOM = e.ActiveStatus == 1 ? "" : e.UOM; return e; }).ToList();

                        List<SparePartPOEstimateDetailDto> detailDto = new List<SparePartPOEstimateDetailDto>();
                        detailDto = _mapper.Map<List<SparePartPOEstimateDetailDto>>(vwiDetailList);
                        sparePartPOEstimateDto.SparePartPOEstimateDetails.AddRange(detailDto);

                        // add to list
                        listResult.Add(sparePartPOEstimateDto);
                    }

                    result.lst = listResult;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_POEstimateHaveBilling), filterDto);
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
        #endregion

        #region Private Methods
        /// <summary>
        /// Get the tax from billing detail
        /// </summary>
        /// <param name="item"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private decimal GetSparepartBillingDetailTax(VWI_POEstimateHaveBilling item, SparePartPOEstimateDetail dt)
        {
            var billingDetailcriterias = new CriteriaComposite(new Criteria(typeof(SparePartBillingDetail), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            billingDetailcriterias.opAnd(new Criteria(typeof(SparePartBillingDetail), "SparePartDODetail.SparePartPOEstimate.ID", MatchType.Exact, item.SparePartPOEstimateID));
            billingDetailcriterias.opAnd(new Criteria(typeof(SparePartBillingDetail), "SparePartDODetail.SparePartMaster.PartNumber", MatchType.Exact, dt.PartNumber));
            ArrayList billingDetails = _sparePartBillingDetail.RetrieveByCriteria(billingDetailcriterias);
            if (billingDetails.Count > 0)
            {
                SparePartBillingDetail sparePartBillingDetail = billingDetails[0] as SparePartBillingDetail;
                return sparePartBillingDetail.Tax;
            }

            return 0;
        }
        #endregion
    }
}

