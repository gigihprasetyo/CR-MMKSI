#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_EmployeeParts business logic class
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
    public class VWI_EmployeePartsBL : AbstractBusinessLogic, IVWI_EmployeePartsBL
    {
        #region Variable
        private readonly IMapper _vwi_employeepartsMapper;
        private IVWI_EmployeePartsRepository<VWI_EmployeeParts, int> _employeePartsRepo;
        #endregion

        #region Constructor
        public VWI_EmployeePartsBL(IVWI_EmployeePartsRepository<VWI_EmployeeParts, int> employeePartsRepo)
        {
            _employeePartsRepo = employeePartsRepo;
            _vwi_employeepartsMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_EmployeeParts).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Read employee parts master data
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_EmployeePartsDto>> Read(VWI_EmployeePartsFilterDto filterDto, int pageSize)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_EmployeeParts), "DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<VWI_EmployeePartsDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;
            int filteredTotalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(VWI_EmployeeParts), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_EmployeeParts), filterDto, sortColl);

                // get data
                //var data = _vwi_employeepartsMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                var data = _employeePartsRepo.Search(
                    criterias, null, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VWI_EmployeeParts>().ToList();
                    List<VWI_EmployeePartsDto> listData = list.ConvertList<VWI_EmployeeParts, VWI_EmployeePartsDto>();

                    List<SalesmanAdditionalInfo> salesmanInfo = GetSalesmanInfoBySalesmanID(listData.Where(e => e.ID > 0).Select(e => e.ID).ToList());
                    List<VWI_JobPositionParts> jobPositions = GetPositionBySalesmanCategoryID(salesmanInfo.Where(e => e.SalesmanCategoryLevel.ID > 0).GroupBy(e => e.SalesmanCategoryLevel.ID).Select(e => e.First().SalesmanCategoryLevel.ID).ToList());
                    List<SalesmanProfile> profiles = GetSalesmanProfileBySalesmanID(listData.Where(e => e.ID > 0).Select(e => e.ID).ToList());

                    listData = listData.Select(e =>
                    {
                        SalesmanAdditionalInfo additionalInfo = salesmanInfo.Where(i => i.SalesmanHeader.ID == e.ID).SingleOrDefault();
                        VWI_JobPositionParts jobPosition = new VWI_JobPositionParts();
                        if (additionalInfo != null)
                        {
                            jobPosition = jobPositions.Where(j => j.ID == additionalInfo.SalesmanCategoryLevel.ID).SingleOrDefault();
                        }

                        e.SalesmanCategoryLevelId = jobPosition.ID;
                        e.PositionCode = jobPosition.Code;
                        e.PositionName = jobPosition.PositionName;
                        e.ParentSalesmanCategoryLevelId = jobPosition.ParentID;
                        e.ParentPositionCode = jobPosition.ParentCode;
                        e.ParentPositionName = jobPosition.ParentPositionName;

                        List<SalesmanProfile> additionalProps = profiles.Where(p => p.SalesmanHeader.ID == e.ID).ToList();
                        e.NoKTP = additionalProps.Where(p => p.ProfileHeader.Description.Contains("KTP")).Select(p => p.ProfileValue).SingleOrDefault();
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
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_EmployeeParts), filterDto);
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
        /// ReadWithProfileCriteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_EmployeePartsDto>> ReadWithProfileCriteria(VWI_EmployeePartsFilterDto filterDto, int pageSize)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_EmployeeParts), "DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<VWI_EmployeePartsDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;
            int filteredTotalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(VWI_EmployeeParts), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_EmployeeParts), filterDto, sortColl);

                // get data
                //var data = _vwi_employeepartsMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                var data = _employeePartsRepo.Search(
                    criterias, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);
                if (data.Count > 0)
                {
                    List<VWI_EmployeePartsDto> listData = data.ConvertList<VWI_EmployeeParts, VWI_EmployeePartsDto>();

                    result.lst = listData;
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_EmployeeParts), filterDto);
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
                var data = _employeePartsRepo.SearchResign(criterias, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);
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
        public ResponseBase<List<VWI_EmployeePartsDto>> ReadDataResign(string salesmanCode)
        {
            var result = new ResponseBase<List<VWI_EmployeePartsDto>>();
            if (salesmanCode != null)
            {
                var criterias = new CriteriaComposite(new Criteria(typeof(VWI_EmployeeParts), "SalesmanCode", MatchType.Exact, salesmanCode));
                criterias.opAnd(new Criteria(typeof(VWI_EmployeeParts), "SalesmanStatusDNET", MatchType.Exact, 3));
                var sortColl = new SortCollection();
                var totalRow = 0;
                int filteredTotalRow = 0;

                try
                {
                    // get data
                    var data = _employeePartsRepo.Search(
                        criterias, null, 1, 200, out filteredTotalRow, out totalRow);
                    if (data.Count > 0)
                    {
                        List<VWI_EmployeePartsDto> listData = data.ConvertList<VWI_EmployeeParts, VWI_EmployeePartsDto>();

                        result.lst = listData;
                        result.total = filteredTotalRow;
                    }
                    else
                    {
                        ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_EmployeeParts), null, "SalesmanCode", salesmanCode);
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
                ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_EmployeeParts), null, "SalesmanCode", salesmanCode);
            }

            return result;
        }
        #endregion

        #region private method
        private List<SalesmanProfile> GetSalesmanProfileBySalesmanID(List<int> listOfSalesmanID)
        {
            List<SalesmanProfile> result = new List<SalesmanProfile>();
            if (listOfSalesmanID.Any())
            {
                // initialize the mapper
                var _mapper = MapperFactory.GetInstance().GetMapper(typeof(SalesmanProfile).ToString());

                string salesmanIDs = "(" + string.Join(", ", listOfSalesmanID) + ")";

                var criterias = new CriteriaComposite(new Criteria(typeof(SalesmanProfile), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(SalesmanProfile), "ProfileGroup.Code", MatchType.Exact, "sals_dbs_parts"));
                criterias.opAnd(new Criteria(typeof(SalesmanProfile), "SalesmanHeader.ID", MatchType.InSet, salesmanIDs));

                // get by criteria                
                result = _mapper.RetrieveByCriteria(criterias).Cast<SalesmanProfile>().ToList();
            }

            return result;
        }

        private List<SalesmanAdditionalInfo> GetSalesmanInfoBySalesmanID(List<int> listOfSalesmanID)
        {
            List<SalesmanAdditionalInfo> result = new List<SalesmanAdditionalInfo>();
            if (listOfSalesmanID.Any())
            {
                // initialize the mapper
                var _mapper = MapperFactory.GetInstance().GetMapper(typeof(SalesmanAdditionalInfo).ToString());

                string salesmanIDs = "(" + string.Join(", ", listOfSalesmanID) + ")";

                var criterias = new CriteriaComposite(new Criteria(typeof(SalesmanAdditionalInfo), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(SalesmanAdditionalInfo), "SalesmanHeader.ID", MatchType.InSet, salesmanIDs));

                // get by criteria                
                result = _mapper.RetrieveByCriteria(criterias).Cast<SalesmanAdditionalInfo>().ToList();
            }

            return result;
        }

        private List<VWI_JobPositionParts> GetPositionBySalesmanCategoryID(List<int> listOfSalesmanCategoryID)
        {
            List<VWI_JobPositionParts> result = new List<VWI_JobPositionParts>();
            if (listOfSalesmanCategoryID.Any())
            {
                // initialize the mapper
                var _mapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_JobPositionParts).ToString());

                string salesmancategoryIDs = "(" + string.Join(", ", listOfSalesmanCategoryID) + ")";

                var criterias = new CriteriaComposite(new Criteria(typeof(VWI_JobPositionParts), "ID", MatchType.InSet, salesmancategoryIDs));

                // get by criteria                
                result = _mapper.RetrieveByCriteria(criterias).Cast<VWI_JobPositionParts>().ToList();
            }

            return result;
        }
        #endregion
    }
}