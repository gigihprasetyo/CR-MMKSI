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
    public class VWI_QuickProductAllBL : AbstractBusinessLogic, IVWI_QuickProductAllBL
    {
        #region Variables
        private readonly IMapper _vWI_quickProductAllMapper;
        #endregion

        #region Constructor
        public VWI_QuickProductAllBL()
        {
            _vWI_quickProductAllMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_QuickProductAll).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_QuickProduct by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_QuickProductAllDto>> Read(VWI_QuickProductAllFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var result = new ResponseBase<List<VWI_QuickProductAllDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                var criterias = Helper.BuildCriteria(typeof(VWI_QuickProductAll), filterDto);

                #region Dealer Category
                // populate the criterias Dealer Category <<<<<----
                //var criterias = Helper.BuildCriteria(typeof(VWI_QuickProductAll), filterDto);
                //if (criterias == null)
                //{
                //    criterias = new CriteriaComposite(new Criteria(typeof(VWI_QuickProductAll), "DealerCode", MatchType.Exact, this.DealerCode));   
                //}
                //else
                //{
                //    criterias.opAnd(new Criteria(typeof(VWI_QuickProductAll), "DealerCode", MatchType.Exact, this.DealerCode));
                //}
                //criterias.opAnd(new Criteria(typeof(VWI_QuickProductAll), "Status", MatchType.Exact, 0));
                #endregion

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_QuickProductAll), filterDto, sortColl);

                // get data
                var data = _vWI_quickProductAllMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VWI_QuickProductAll>().ToList();
                    List<VWI_QuickProductAllDto> listData = list.ConvertList<VWI_QuickProductAll, VWI_QuickProductAllDto>();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_QuickProductAll), filterDto);
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

