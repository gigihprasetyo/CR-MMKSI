#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_OpenFaktur business logic class
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
    public class VWI_OpenFakturBL : AbstractBusinessLogic, IVWI_OpenFakturBL
    {
        #region Variables
        private readonly IMapper _vehicleSalesOpenFakturMapper;
        private IVWI_OpenFakturRepository<VWI_OpenFaktur, int> _vWI_OpenFakturRepository;
        #endregion

        #region Constructor
        public VWI_OpenFakturBL(IVWI_OpenFakturRepository<VWI_OpenFaktur, int> vWI_OpenFakturRepository)
        {
            _vWI_OpenFakturRepository = vWI_OpenFakturRepository;
            _vehicleSalesOpenFakturMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_OpenFaktur).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_OpenFaktur by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_OpenFakturDto>> ReadByView(VWI_OpenFakturFilterDto filterDto, int pageSize)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_OpenFaktur), "DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<VWI_OpenFakturDto>>();
            var sortColl = new SortCollection();

            try
            {
                // define sql
                var sql = Helper.GenerateSQLFromCriteriasAndSort(typeof(VWI_OpenFaktur), filterDto, sortColl, criterias);

                // get data
                var data = _vehicleSalesOpenFakturMapper.RetrieveSP("SELECT * FROM VWI_OpenFaktur " + sql);
                if (data.Count > 0)
                {
                    // calculate the skip 
                    int skip = filterDto.pages < 1 ? 0 : (filterDto.pages - 1) * pageSize;

                    // filter out the data based on the paging                    
                    List<VWI_OpenFaktur> list = new List<VWI_OpenFaktur>();
                    if (sortColl != null && sortColl.Count > 0)
                        list = data.Cast<VWI_OpenFaktur>().Skip(skip).Take(pageSize).ToList();
                    else
                        list = data.Cast<VWI_OpenFaktur>().OrderBy(x => x.ID).Skip(skip).Take(pageSize).ToList();

                    // convert to dto object
                    List<VWI_OpenFakturDto> listData = list.ConvertList<VWI_OpenFaktur, VWI_OpenFakturDto>();

                    result.lst = listData;
                    result.total = data.Count;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_OpenFaktur), filterDto);
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
        /// Get VWI_OpenFaktur by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_OpenFakturDto>> Read(VWI_OpenFakturFilterDto filterDto, int pageSize)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_OpenFaktur), "DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<VWI_OpenFakturDto>>();
            var sortColl = new SortCollection();
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                CriteriaComposite chassisQueryCriteria = null;
                CriteriaComposite lastUpdateQueryCriteria = null;
                CriteriaComposite createdTimeQueryCriteria = null;

                if (filterDto.find != null && filterDto.find.Count > 0)
                {
                    foreach (var data in filterDto.find)
                    {
                        if (data.PropertyName.ToUpper().Equals("CHASSISNUMBER"))
                        {
                            chassisQueryCriteria = new CriteriaComposite(new Criteria(typeof(ChassisMaster), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                            chassisQueryCriteria.opAnd(new Criteria(typeof(ChassisMaster), "ChassisNumber", data.MatchType, data.PropertyValue.Trim()));
                            chassisQueryCriteria.opAnd(new Criteria(typeof(ChassisMaster), "EndCustomerID", MatchType.IsNotNull, null));
                        }
                    }

                    foreach (var data in filterDto.find)
                    {
                        if (data.PropertyName.ToUpper().Equals("CHASSISNUMBER"))
                        { }
                        else if (data.PropertyName.ToUpper().Equals("LASTUPDATETIME") && chassisQueryCriteria == null)
                        {
                            if (lastUpdateQueryCriteria != null)
                            {
                                if (data.SqlOperation == SQLOperation.And)
                                {
                                    lastUpdateQueryCriteria.opAnd(new Criteria(typeof(EndCustomer), "LastUpdateTime", data.MatchType, data.PropertyValue.Trim()));
                                }
                                else if (data.SqlOperation == SQLOperation.Or)
                                {
                                    lastUpdateQueryCriteria.opOr(new Criteria(typeof(EndCustomer), "LastUpdateTime", data.MatchType, data.PropertyValue.Trim()));

                                }
                            }
                            else
                            {
                                lastUpdateQueryCriteria = new CriteriaComposite(new Criteria(typeof(EndCustomer), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                                lastUpdateQueryCriteria.opAnd(new Criteria(typeof(EndCustomer), "LastUpdateTime", data.MatchType, data.PropertyValue.Trim()));
                            }
                            
                        }
                        else if (data.PropertyName.ToUpper().Equals("CREATEDTIME") && chassisQueryCriteria == null && lastUpdateQueryCriteria == null)
                        {
                            if (createdTimeQueryCriteria != null)
                            {
                                if (data.SqlOperation == SQLOperation.And)
                                {
                                    createdTimeQueryCriteria.opAnd(new Criteria(typeof(EndCustomer), "CreatedTime", data.MatchType, data.PropertyValue.Trim()));
                                }
                                else if (data.SqlOperation == SQLOperation.Or)
                                {
                                    createdTimeQueryCriteria.opOr(new Criteria(typeof(EndCustomer), "CreatedTime", data.MatchType, data.PropertyValue.Trim()));

                                }
                            }
                            else
                            {
                                createdTimeQueryCriteria = new CriteriaComposite(new Criteria(typeof(EndCustomer), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                                createdTimeQueryCriteria.opAnd(new Criteria(typeof(EndCustomer), "CreatedTime", data.MatchType, data.PropertyValue.Trim()));
                            }
                        }
                        else
                        {
                            if (data.SqlOperation == SQLOperation.And)
                            {
                                criterias.opAnd(new Criteria(typeof(VWI_OpenFaktur), data.PropertyName.Trim(), data.MatchType, data.PropertyValue.Trim()));
                            }
                            else if (data.SqlOperation == SQLOperation.Or)
                            {
                                criterias.opOr(new Criteria(typeof(VWI_OpenFaktur), data.PropertyName.Trim(), data.MatchType, data.PropertyValue.Trim()));
                            }
                        }
                    }
                }

                // get criteria


                sortColl = Helper.UpdateSortColumn(typeof(VWI_OpenFaktur), filterDto, sortColl);

                List<VWI_OpenFaktur> dataResult = _vWI_OpenFakturRepository.Search(criterias, chassisQueryCriteria, lastUpdateQueryCriteria, createdTimeQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (dataResult != null && dataResult.Count > 0)
                {        
                    result.lst = dataResult.ConvertList<VWI_OpenFaktur, VWI_OpenFakturDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_OpenFaktur), filterDto);
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
        /// Check if it is has chassis master criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <returns></returns>
        private List<string> GetInnerQueryParams()
        {
            return new List<string>(){
                "ChassisNumber",
                "LastUpdateTime"
            };
        }
        #endregion
    }
}
