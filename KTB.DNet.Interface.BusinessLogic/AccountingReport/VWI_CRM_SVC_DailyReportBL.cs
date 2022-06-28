	#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_SVC_DailyReport business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/02/2020 10:45:39
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
    public class VWI_CRM_SVC_DailyReportBL : AbstractBusinessLogic, IVWI_CRM_SVC_DailyReportBL
    {
        #region Variables
        private IVWI_CRM_SVC_DailyReportRepository<VWI_CRM_SVC_DailyReport, int> _vWI_CRM_SVC_DailyReportRepo;
        #endregion

        #region Constructor
        public VWI_CRM_SVC_DailyReportBL(IVWI_CRM_SVC_DailyReportRepository<VWI_CRM_SVC_DailyReport, int> vWI_CRM_SVC_DailyReportRepo)
        {
            _vWI_CRM_SVC_DailyReportRepo = vWI_CRM_SVC_DailyReportRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_SVC_DailyReport by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_SVC_DailyReportDto>> Read(VWI_CRM_SVC_DailyReportFilterDto filterDto, int pageSize)
        {            
            return null;
        }

		public ResponseBase<List<VWI_CRM_SVC_DailyReportDto>> ReadList(VWI_CRM_SVC_DailyReportFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_SVC_DailyReportDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                string strSPParameter = "'{0}','{1}', {2}, '{3}', {4}, {5}";

                if (DealerCode.ToUpper() == "MKS")
                {                    
                    if(listDealer.Count > 0)
                    {
                        // filter by company         
                        string fromDateIn = string.Empty;
                        string toDateIn = string.Empty;
                        DateTime curDate = DateTime.Now;
                        string businessUnitCode = string.Empty;

                        if (filterDto.find != null)
                        {
                            bool isFromDateIn = false;
                            bool isToDateIn = false;
                            foreach(MatchTypeFilter item in filterDto.find)
                            {
                                if(item.PropertyName == "FromDateIn")
                                {
                                    fromDateIn = item.PropertyValue;
                                    isFromDateIn = true;
                                }else if(item.PropertyName == "ToDateIn")
                                {
                                    toDateIn = item.PropertyValue;
                                    isToDateIn = true;
                                }else if(item.PropertyName == "businessunitcode")
                                {
                                    businessUnitCode = item.PropertyValue;
                                }
                            }

                            if(isFromDateIn && isToDateIn)
                            {
                                if(Convert.ToDateTime(fromDateIn).Month != Convert.ToDateTime(toDateIn).Month)
                                {
                                    // if FromDateIn Month and ToDateIn month is not same
                                    // show err message
                                    ErrorMsgHelper.Exception(result.messages, "FromDateIn and ToDateIn parameter must be in the same month");
                                    return result;
                                }
                            }else if (isFromDateIn && !isToDateIn)
                            {
                                // if user input only FromDateIn parameter
                                // show err message to input ToDateIn parameter
                                ErrorMsgHelper.Exception(result.messages, "Must be input To Date parameter");
                                return result;
                            }else if(!isFromDateIn && isToDateIn)
                            {
                                // if user input only ToDateIn parameter
                                // replace FromDateIn with first date on ToDateIn parameter
                                fromDateIn = Convert.ToDateTime(toDateIn).FirstDayOfMonth().ToString("yyyy-MM-dd hh:mm:ss");
                            }
                        }
                        else
                        {
                            fromDateIn = curDate.FirstDayOfMonth().ToString("yyyy-MM-dd hh:mm:ss");
                            toDateIn = curDate.ToString("yyyy-MM-dd hh:mm:ss");
                        }

                        strSPParameter = string.Format(strSPParameter, fromDateIn, toDateIn, businessUnitCode==string.Empty?"NULL":businessUnitCode, dealerCompanyCode, pageSize.ToString(), filterDto.pages.ToString());
                    }
                    else
                    {
                        ErrorMsgHelper.Exception(result.messages, "Dealer Company to Dealer Configuration is not set");
						return result; 
                    }
                }else
                {
                    ErrorMsgHelper.Exception(result.messages, "Dealer Company Configuration is not set");
					return result;
                }

                List<VWI_CRM_SVC_DailyReport> data = _vWI_CRM_SVC_DailyReportRepo.Search(strSPParameter, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_CRM_SVC_DailyReport, VWI_CRM_SVC_DailyReportDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_SVC_DailyReport), filterDto);
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
        /// Delete VWI_CRM_SVC_DailyReport by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_SVC_DailyReportDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_CRM_SVC_DailyReport
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_SVC_DailyReportDto> Create(VWI_CRM_SVC_DailyReportParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_CRM_SVC_DailyReport
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_SVC_DailyReportDto> Update(VWI_CRM_SVC_DailyReportParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}