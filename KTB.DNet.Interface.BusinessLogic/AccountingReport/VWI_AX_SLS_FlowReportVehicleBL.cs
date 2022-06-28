	#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_AX_SLS_FlowReportVehicle business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 05/02/2020 9:46:08
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using KTB.DNet.Interface.Framework;	
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class VWI_AX_SLS_FlowReportVehicleBL : AbstractBusinessLogic, IVWI_AX_SLS_FlowReportVehicleBL
    {
        #region Variables
        private IVWI_AX_SLS_FlowReportVehicleRepository<VWI_AX_SLS_FlowReportVehicle, int> _vWI_AX_SLS_FlowReportVehicleRepo;
        #endregion

        #region Constructor
        public VWI_AX_SLS_FlowReportVehicleBL(IVWI_AX_SLS_FlowReportVehicleRepository<VWI_AX_SLS_FlowReportVehicle, int> vWI_AX_SLS_FlowReportVehicleRepo)
        {
            _vWI_AX_SLS_FlowReportVehicleRepo = vWI_AX_SLS_FlowReportVehicleRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_AX_SLS_FlowReportVehicle by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_AX_SLS_FlowReportVehicleDto>> Read(VWI_AX_SLS_FlowReportVehicleFilterDto filterDto, int pageSize)
        {            
            return null;
        }

		public ResponseBase<List<VWI_AX_SLS_FlowReportVehicleDto>> ReadList(VWI_AX_SLS_FlowReportVehicleFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_AX_SLS_FlowReportVehicleDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                DateTime dateFrom = DateTime.MinValue, dateTo = DateTime.MinValue;
                var innerQueryCriteria = string.Empty;
                var period = string.Empty;

                #region Check TransactionDateTo Parameter
                if (filterDto.find != null && filterDto.find.Count > 0)
                {
                    var indexs = 0;
                    List<int> indexRemoves = new List<int>();
                    foreach (var item in filterDto.find)
                    {
                        var propertyName = item.PropertyName;
                        var propertyValue = item.PropertyValue;
                        if (propertyName == "TransactionDateTo")
                        {
                            if (propertyValue == "01/01/1753 0:00:00")
                            {
                                ErrorMsgHelper.Exception(result.messages, "TransactionDateTo is not set");
                                return result;
                            }
                            else
                            {
                                dateTo = Convert.ToDateTime(propertyValue);
                                dateFrom = dateTo.FirstDayOfMonth();
                                period = dateTo.ToString("yyyyMM");
                                indexRemoves.Add(indexs);
                            }
                        }
                        else if (propertyName == "TransactionDateFrom")
                        {
                            indexRemoves.Add(indexs);
                        }

                        indexs++;
                    }

                    // remove filter transaction date to
                    var itemRemove = 0;
                    foreach (var item in indexRemoves)
                    {
                        filterDto.find.RemoveAt(item - itemRemove);
                        itemRemove++;
                    }

                    // period
                    if (period != string.Empty)
                    {
                        var param = new MatchTypeFilter();
                        param.MatchType = 0;
                        param.PropertyName = "Period";
                        param.PropertyValue = period;
                        param.SqlOperation = 0;
                        filterDto.find.Add(param);
                    }

                }
                else
                {
                    ErrorMsgHelper.Exception(result.messages, "TransactionDateTo is not set");
                    return result;
                }
                #endregion

                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_AX_SLS_FlowReportVehicle), filterDto);

                if(DealerCode.ToUpper() == "MKS")
                {
                    // filter by dealer company
                    if (listDealer.Count > 0)
                    {
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_AX_SLS_FlowReportVehicle), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "Company", dealerCompanyCode,false,criterias);

                        sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_AX_SLS_FlowReportVehicle), filterDto);

                        List<VWI_AX_SLS_FlowReportVehicle> data = _vWI_AX_SLS_FlowReportVehicleRepo.Search(
                                            criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow, dateFrom, dateTo);

                        if (data != null && data.Count > 0)
                        {
                            result.lst = data.ConvertList<VWI_AX_SLS_FlowReportVehicle, VWI_AX_SLS_FlowReportVehicleDto>();
                            result.total = filteredTotalRow;
                            result.success = true;
                        }
                        else
                        {
                            ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_AX_SLS_FlowReportVehicle), filterDto);
                            return result;
                        }
                    }
                    else
                    {
                        ErrorMsgHelper.Exception(result.messages, "Dealer Company to dealer Configuration is not set");
                        return result;
                    }
                }
                else
                {
                    ErrorMsgHelper.Exception(result.messages, "Dealer Company Configuration is not set");
                    return result;
                }
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
        /// Delete VWI_AX_SLS_FlowReportVehicle by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_AX_SLS_FlowReportVehicleDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_AX_SLS_FlowReportVehicle
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_AX_SLS_FlowReportVehicleDto> Create(VWI_AX_SLS_FlowReportVehicleParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_AX_SLS_FlowReportVehicle
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_AX_SLS_FlowReportVehicleDto> Update(VWI_AX_SLS_FlowReportVehicleParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}