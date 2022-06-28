#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_ServiceHistory business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 16/10/2018 3:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class VWI_ServiceHistoryBL : AbstractBusinessLogic, IVWI_ServiceHistoryBL
    {
        #region Variables
        private IServiceHistoryRepository<VWI_ServiceHistory, int> _serviceHistoryRepo;
        #endregion

        #region Constructor
        public VWI_ServiceHistoryBL(IServiceHistoryRepository<VWI_ServiceHistory, int> serviceHistoryRepo)
        {
            _serviceHistoryRepo = serviceHistoryRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get ServiceHistory by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_ServiceHistoryDto>> Read(VWI_ServiceHistoryFilterDto filterDto, int pageSize)
        {
            var result = new ResponseBase<List<VWI_ServiceHistoryDto>>();
            var sortColl = new SortCollection();
            int totalRow = 0;
            int filteredTotalRow = 0;

            try
            {
                var criteria = Helper.BuildCriteria(typeof(VWI_ServiceHistory), filterDto);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_ServiceHistory), filterDto, sortColl);

                List<VWI_ServiceHistory> data = _serviceHistoryRepo.Search(
                                    criteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_ServiceHistory, VWI_ServiceHistoryDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_ServiceHistory), filterDto);
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
    }
}

