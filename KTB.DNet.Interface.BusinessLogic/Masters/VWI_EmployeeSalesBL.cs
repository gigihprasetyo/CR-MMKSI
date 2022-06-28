#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_EmployeeSales business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region "Namespace Imports"
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class VWI_EmployeeSalesBL : AbstractBusinessLogic, IVWI_EmployeeSalesBL
    {
        #region Variable
        private readonly IMapper _vwi_employeesalesMapper;
        private readonly IMapper _citymapper;
        private IVWI_EmployeeSalesRepository<VWI_EmployeeSales, int> _employeeSalesRepo;
        #endregion

        #region Constructor
        public VWI_EmployeeSalesBL(IVWI_EmployeeSalesRepository<VWI_EmployeeSales, int> employeeSalesRepo)
        {
            _vwi_employeesalesMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_EmployeeSales).ToString());
            _citymapper = MapperFactory.GetInstance().GetMapper(typeof(City).ToString());
            _employeeSalesRepo = employeeSalesRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Read employee sales master data
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_EmployeeSalesDto>> ReadWithProfileCriteria(VWI_EmployeeSalesFilterDto filterDto, int pageSize)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_EmployeeSales), "DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<VWI_EmployeeSalesDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;
            int filteredTotalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(VWI_EmployeeSales), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_EmployeeSales), filterDto, sortColl);

                // check dealer category
                var dealerCategoryIdList = new List<int>();
                var isCheckDealerCategory = ValidationHelper.CheckDealerCategoryConfig();
                if (isCheckDealerCategory)
                {
                    var isCategoryIdValid = ValidationHelper.GetDealerCategoryId(this.DealerCode, out dealerCategoryIdList);
                }
                // get data
                var data = _employeeSalesRepo.Search(
                    criterias, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow, dealerCategoryIdList, isCheckDealerCategory);
                if (data.Count > 0)
                {
                    List<VWI_EmployeeSalesDto> listData = data.ConvertList<VWI_EmployeeSales, VWI_EmployeeSalesDto>();
                    if (listData.Count > 0)
                    {
                        if (!String.IsNullOrEmpty(listData[0].PlaceOfBirth))
                        {
                            CriteriaComposite criteria_birth = new CriteriaComposite(new Criteria(typeof(City), "CityName", MatchType.Exact, listData[0].PlaceOfBirth));
                            var masters_birth = _citymapper.RetrieveByCriteria(criteria_birth);
                            if (masters_birth.Count > 0)
                            {
                                var birth = masters_birth[0] as City;
                                listData[0].BirthPlaceCityCode = birth.CityCode;
                            }
                        }
                        else
                        {
                            listData[0].BirthPlaceCityCode = "";
                        }
                        if (!String.IsNullOrEmpty(listData[0].City))
                        {
                            CriteriaComposite criteria_city = new CriteriaComposite(new Criteria(typeof(City), "CityName", MatchType.Exact, listData[0].City));
                            var masters_city = _citymapper.RetrieveByCriteria(criteria_city);
                            if (masters_city.Count > 0)
                            {
                                var city = masters_city[0] as City;
                                listData[0].AddressCityCode = city.CityCode;
                            }
                        }
                        else
                        {
                            listData[0].AddressCityCode = "";
                        }
                    }
                    
                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_EmployeeSales), filterDto);
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
        /// Read employee sales master data
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_EmployeeSalesDto>> Read(VWI_EmployeeSalesFilterDto filterDto, int pageSize)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_EmployeeSales), "DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<VWI_EmployeeSalesDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;
            int filteredTotalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(VWI_EmployeeSales), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_EmployeeSales), filterDto, sortColl);

                // get data
                //var data = _vwi_employeesalesMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                var data = _employeeSalesRepo.Search(
                    criterias, null, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VWI_EmployeeSales>().ToList();
                    List<VWI_EmployeeSalesDto> listData = list.ConvertList<VWI_EmployeeSales, VWI_EmployeeSalesDto>();

                    List<SalesmanArea> area = GetSalesmanAreaByID(listData.Where(e => e.SalesmanAreaID > 0).GroupBy(e => e.SalesmanAreaID).Select(e => e.First().SalesmanAreaID).ToList());
                    List<SalesmanLevel> level = GetSalesmanLevelByID(listData.Where(e => e.SalesmanLevelID > 0).GroupBy(e => e.SalesmanLevelID).Select(e => e.First().SalesmanLevelID).ToList());
                    List<JobPosition> job = GetJobPositionByID(listData.Where(e => e.JobPositionID > 0).GroupBy(e => e.JobPositionID).Select(e => e.First().JobPositionID).ToList());
                    List<SalesmanProfile> profiles = GetSalesmanProfileBySalesmanID(listData.Where(e => e.ID > 0).Select(e => e.ID).ToList());

                    listData = listData.Select(e =>
                    {
                        string areaDesc = area.Where(a => a.ID == e.SalesmanAreaID).Select(a => a.AreaDesc).SingleOrDefault();
                        e.SalesmanAreaDesc = areaDesc ?? string.Empty;
                        string levelDesc = level.Where(a => a.ID == e.SalesmanLevelID).Select(a => a.Description).SingleOrDefault();
                        e.SalesmanLevelDesc = levelDesc ?? string.Empty;
                        string jobDesc = job.Where(a => a.ID == e.JobPositionID).Select(a => a.Description).SingleOrDefault();
                        e.JobPositionDesc = jobDesc ?? string.Empty;

                        List<SalesmanProfile> additionalProps = profiles.Where(p => p.SalesmanHeader.ID == e.ID).ToList();

                        e.NoKTP = additionalProps.Where(p => p.ProfileHeader.Description.Contains("KTP")).Select(p => p.ProfileValue).SingleOrDefault();
                        e.Kategori = additionalProps.Where(p => p.ProfileHeader.Description.Contains("KATEGORI")).Select(p => p.ProfileValue).SingleOrDefault();
                        e.Pendidikan = additionalProps.Where(p => p.ProfileHeader.Description.Contains("PENDIDIKAN")).Select(p => p.ProfileValue).SingleOrDefault();
                        e.NoHP = additionalProps.Where(p => p.ProfileHeader.Description.Contains("NO HP")).Select(p => p.ProfileValue).SingleOrDefault();
                        e.Email = additionalProps.Where(p => p.ProfileHeader.Description.Contains("EMAIL")).Select(p => p.ProfileValue).SingleOrDefault();

                        return e;
                    }).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_EmployeeSales), filterDto);
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
        /// Read employee sales resign master data
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_EmployeeResignDto>> ReadResignEmployee(VWI_EmployeeResignFilterDto filterDto, int pageSize)
        {
            var result = new ResponseBase<List<VWI_EmployeeResignDto>>();
            var totalRow = 0;
            int filteredTotalRow = 0;
            var sortColl = new SortCollection();

            try
            {
                // populate the criterias
                var criterias = Helper.BuildCriteria(typeof(VWI_EmployeeResign), filterDto);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_EmployeeResign), filterDto, sortColl);

                // get data
                var data = _employeeSalesRepo.SearchResign(criterias, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);
                if (data.Count > 0)
                {
                    List<VWI_EmployeeResignDto> listData = data.ConvertList<VWI_EmployeeResign, VWI_EmployeeResignDto>();

                    result.lst = listData;
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_EmployeeResign), filterDto);
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
        /// Read employee sales master read resign data
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_EmployeeSalesDto>> ReadDataResign(string salesmanCode, string NoKTP)
        {
            var result = new ResponseBase<List<VWI_EmployeeSalesDto>>();
            if (salesmanCode != null || NoKTP != null)
            {
                var criterias = new CriteriaComposite(new Criteria(typeof(VWI_EmployeeSales), "SalesmanStatusDNET", MatchType.Exact, 3));
                if (!string.IsNullOrEmpty(salesmanCode))
                {
                    criterias.opAnd(new Criteria(typeof(VWI_EmployeeSales), "SalesmanCode", MatchType.Exact, salesmanCode));
                }
                if (!string.IsNullOrEmpty(NoKTP))
                {
                    criterias.opAnd(new Criteria(typeof(VWI_EmployeeSales), "NoKTP", MatchType.Exact, NoKTP));
                }
                if (string.IsNullOrEmpty(salesmanCode) && string.IsNullOrEmpty(NoKTP))
                {
                    criterias.opAnd(new Criteria(typeof(VWI_EmployeeSales), "SalesmanCode", MatchType.Exact, salesmanCode));
                    criterias.opAnd(new Criteria(typeof(VWI_EmployeeSales), "NoKTP", MatchType.Exact, NoKTP));
                }
                var sortColl = new SortCollection();
                var totalRow = 0;
                int filteredTotalRow = 0;

                try
                {
                    // get data
                    var data = _employeeSalesRepo.Search(
                        criterias, null, 1, 200, out filteredTotalRow, out totalRow, null, false);
                    if (data.Count > 0)
                    {
                        List<VWI_EmployeeSalesDto> listData = data.ConvertList<VWI_EmployeeSales, VWI_EmployeeSalesDto>();

                        result.lst = listData;
                        result.total = filteredTotalRow;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(salesmanCode) && string.IsNullOrEmpty(NoKTP))
                        {
                            ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_EmployeeSales), null, "SalesmanCode", salesmanCode);
                        }
                        else if (!string.IsNullOrEmpty(NoKTP) && string.IsNullOrEmpty(salesmanCode))
                        {
                            ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_EmployeeSales), null, "NoKTP", NoKTP);
                        }
                        else if (!string.IsNullOrEmpty(NoKTP) && !string.IsNullOrEmpty(salesmanCode))
                        {
                            var err = "SalesmanCode' = '" + salesmanCode + "' dan dengan 'NoKTP";
                            ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_EmployeeSales), null, err, NoKTP);
                        }
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
            }
            else
            {
                ErrorMsgHelper.Exception(result.messages, "Wajib mengisikan parameter SalesmanCode atau NoKTP");
            }

            return result;
        }
        #endregion

        #region Private Method
        private List<SalesmanArea> GetSalesmanAreaByID(List<int> listOfId)
        {
            List<SalesmanArea> result = new List<SalesmanArea>();
            if (listOfId.Any())
            {
                // initialize the mapper
                var _mapper = MapperFactory.GetInstance().GetMapper(typeof(SalesmanArea).ToString());

                string salesmanAreaID = "(" + string.Join(", ", listOfId) + ")";

                var criterias = new CriteriaComposite(new Criteria(typeof(SalesmanArea), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(SalesmanArea), "ID", MatchType.InSet, salesmanAreaID));

                // get by criteria                
                result = _mapper.RetrieveByCriteria(criterias).Cast<SalesmanArea>().ToList();
            }

            return result;
        }

        private List<SalesmanLevel> GetSalesmanLevelByID(List<int> listOfId)
        {
            List<SalesmanLevel> result = new List<SalesmanLevel>();
            if (listOfId.Any())
            {
                // initialize the mapper
                var _mapper = MapperFactory.GetInstance().GetMapper(typeof(SalesmanLevel).ToString());

                string salesmanLevelID = "(" + string.Join(", ", listOfId) + ")";

                var criterias = new CriteriaComposite(new Criteria(typeof(SalesmanLevel), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(SalesmanLevel), "ID", MatchType.InSet, salesmanLevelID));

                // get by criteria                
                result = _mapper.RetrieveByCriteria(criterias).Cast<SalesmanLevel>().ToList();
            }

            return result;
        }

        private List<JobPosition> GetJobPositionByID(List<int> listOfId)
        {
            List<JobPosition> result = new List<JobPosition>();
            if (listOfId.Any())
            {
                // initialize the mapper
                var _mapper = MapperFactory.GetInstance().GetMapper(typeof(JobPosition).ToString());

                string salesChannelCodes = "(" + string.Join(", ", listOfId) + ")";

                var criterias = new CriteriaComposite(new Criteria(typeof(JobPosition), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(JobPosition), "ID", MatchType.InSet, salesChannelCodes));

                // get by criteria                
                result = _mapper.RetrieveByCriteria(criterias).Cast<JobPosition>().ToList();
            }

            return result;
        }

        private List<SalesmanProfile> GetSalesmanProfileBySalesmanID(List<int> listOfSalesmanID)
        {
            List<SalesmanProfile> result = new List<SalesmanProfile>();
            if (listOfSalesmanID.Any())
            {
                // initialize the mapper
                var _mapper = MapperFactory.GetInstance().GetMapper(typeof(SalesmanProfile).ToString());

                string salesmanIDs = "(" + string.Join(", ", listOfSalesmanID) + ")";

                var criterias = new CriteriaComposite(new Criteria(typeof(SalesmanProfile), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(SalesmanProfile), "ProfileGroup.Code", MatchType.Exact, "sals_dbs_unit"));
                criterias.opAnd(new Criteria(typeof(SalesmanProfile), "SalesmanHeader.ID", MatchType.InSet, salesmanIDs));

                // get by criteria                
                result = _mapper.RetrieveByCriteria(criterias).Cast<SalesmanProfile>().ToList();
            }

            return result;
        }
        #endregion
    }
}