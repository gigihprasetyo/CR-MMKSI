#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_QuickProduct business logic class
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
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class VWI_QuickProductBL : AbstractBusinessLogic, IVWI_QuickProductBL
    {
        #region Variables
        private readonly IMapper _vWI_quickProductMapper;
        #endregion

        #region Constructor
        public VWI_QuickProductBL()
        {
            _vWI_quickProductMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_QuickProduct).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_QuickProduct by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_QuickProductDto>> Read(VWI_QuickProductFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var result = new ResponseBase<List<VWI_QuickProductDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                var criterias = Helper.BuildCriteria(typeof(VWI_QuickProduct), filterDto);

                #region dealer category
                //if (criterias == null)
                //{
                //    criterias = new CriteriaComposite(new Criteria(typeof(VWI_QuickProduct), "DealerCode", MatchType.Exact, this.DealerCode));                    
                //}
                //else
                //{
                //    criterias.opAnd(new Criteria(typeof(VWI_QuickProduct), "DealerCode", MatchType.Exact, this.DealerCode));
                //}
                //criterias.opAnd(new Criteria(typeof(VWI_QuickProduct), "Status", MatchType.Exact, 0));
                #endregion

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_QuickProduct), filterDto, sortColl);

                // get data
                var data = _vWI_quickProductMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VWI_QuickProduct>().ToList();
                    List<VWI_QuickProductDto> listData = list.ConvertList<VWI_QuickProduct, VWI_QuickProductDto>();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_QuickProduct), filterDto);
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

