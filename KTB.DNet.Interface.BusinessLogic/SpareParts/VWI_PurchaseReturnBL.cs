#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_PurchaseReturn business logic class
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
    public class VWI_PurchaseReturnBL : AbstractBusinessLogic, IVWI_PurchaseReturnBL
    {
        #region Variables
        private IVWI_PurchaseReturnRepository<VWI_PurchaseReturn, int> _vWI_PurchaseReturnRepo;
        #endregion

        #region Constructor
        public VWI_PurchaseReturnBL(IVWI_PurchaseReturnRepository<VWI_PurchaseReturn, int> VWI_PurchaseReturnRepo)
        {
            _vWI_PurchaseReturnRepo = VWI_PurchaseReturnRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_PurchaseReturn by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_PurchaseReturnDto>> Read(VWI_PurchaseReturnFilterDto filterDto, int pageSize)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_PurchaseReturn), "DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<VWI_PurchaseReturnDto>>();
            var sortColl = new SortCollection();
            int totalRow = 0;
            int filteredTotalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(VWI_PurchaseReturn), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_PurchaseReturn), filterDto, sortColl);

                List<VWI_PurchaseReturn> data = _vWI_PurchaseReturnRepo.Search(
                                    criterias, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_PurchaseReturn, VWI_PurchaseReturnDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_PurchaseReturn), filterDto);
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

