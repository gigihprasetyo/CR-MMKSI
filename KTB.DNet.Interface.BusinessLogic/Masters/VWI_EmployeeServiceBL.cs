#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_EmployeeService business logic class
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
    public class VWI_EmployeeServiceBL : AbstractBusinessLogic, IVWI_EmployeeServiceBL
    {
        #region Variable
        private readonly IMapper _employeeServiceMapper;
        private IVWI_EmployeeServicesRepository<VWI_EmployeeService, int> _employeeServicesRepo;
        #endregion

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="VWI_EmployeeServiceBL"/> class.</summary>
        /// <param name="employeeServicesRepo">The employee services repo.</param>
        public VWI_EmployeeServiceBL(IVWI_EmployeeServicesRepository<VWI_EmployeeService, int> employeeServicesRepo)
        {
            _employeeServiceMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_EmployeeService).ToString());
            _employeeServicesRepo = employeeServicesRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get Employee Mechanic by certain criteria
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_EmployeeServiceDto>> Read(VWI_EmployeeServiceFilterDto filterDto, int pageSize)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_EmployeeService), "DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<VWI_EmployeeServiceDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(VWI_EmployeeService), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_EmployeeService), filterDto, sortColl);

                // get data
                var data = _employeeServiceMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VWI_EmployeeService>().ToList();
                    List<VWI_EmployeeServiceDto> listData = list.ConvertList<VWI_EmployeeService, VWI_EmployeeServiceDto>();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_EmployeeService), filterDto);
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

        /// <summary>Reads the with profile.</summary>
        /// <param name="filterDto">The filter dto.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public ResponseBase<List<VWI_EmployeeServiceDto>> ReadWithProfile(VWI_EmployeeServiceFilterDto filterDto, int pageSize)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_EmployeeService), "DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<VWI_EmployeeServiceDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;
            int filteredTotalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(VWI_EmployeeService), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_EmployeeService), filterDto, sortColl);

                // get data
                var data = _employeeServicesRepo.Search(
                    criterias, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);
                if (data.Count > 0)
                {
                    //var list = data.Cast<VWI_EmployeeService>().ToList();
                    List<VWI_EmployeeServiceDto> listData = data.ConvertList<VWI_EmployeeService, VWI_EmployeeServiceDto>();

                    result.lst = listData;
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_EmployeeService), filterDto);
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

        /// <summary>Reads the resign employee.</summary>
        /// <param name="filterDto">The filter dto.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public ResponseBase<List<VWI_EmployeeServiceResignDto>> ReadResignEmployee(VWI_EmployeeResignFilterDto filterDto, int pageSize)
        {
            var result = new ResponseBase<List<VWI_EmployeeServiceResignDto>>();
            var totalRow = 0;
            int filteredTotalRow = 0;
            var sortColl = new SortCollection();

            try
            {
                // populate the criterias
                var criterias = Helper.BuildCriteria(typeof(VWI_EmployeeService), filterDto);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_EmployeeService), filterDto, sortColl);

                // get data
                var data = _employeeServicesRepo.SearchResign(criterias, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);
                if (data.Count > 0)
                {
                    List<VWI_EmployeeServiceResignDto> listData = data.ConvertList<VWI_EmployeeService, VWI_EmployeeServiceResignDto>();

                    result.lst = listData;
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_EmployeeService), filterDto);
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

        /// <summary>Reads the data resign.</summary>
        /// <param name="salesmanID">The salesman identifier.</param>
        /// <returns></returns>
        public ResponseBase<List<VWI_EmployeeServiceDto>> ReadDataResign(int salesmanID)
        {
            var result = new ResponseBase<List<VWI_EmployeeServiceDto>>();
            //if (salesmanID != null)
            //{
                var criterias = new CriteriaComposite(new Criteria(typeof(VWI_EmployeeService), "ID", MatchType.Exact, salesmanID));
                criterias.opAnd(new Criteria(typeof(VWI_EmployeeService), "SalesmanStatusDNET", MatchType.Exact, 2));
                var sortColl = new SortCollection();
                var totalRow = 0;
                int filteredTotalRow = 0;

                try
                {
                    // get data
                    var data = _employeeServicesRepo.Search(
                        criterias, null, 1, 200, out filteredTotalRow, out totalRow);
                    if (data.Count > 0)
                    {
                        List<VWI_EmployeeServiceDto> listData = data.ConvertList<VWI_EmployeeService, VWI_EmployeeServiceDto>();

                        result.lst = listData;
                        result.total = filteredTotalRow;
                    }
                    else
                    {
                        ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_EmployeeService), null, "ID", salesmanID.ToString());
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
            //}
            //else
            //{
            //    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_EmployeeService), null, "ID", salesmanID.ToString());
            //}

            return result;
        }
        #endregion
    }
}