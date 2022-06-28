#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_PODealer business logic class
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
    public class VWI_PODealerBL : AbstractBusinessLogic, IVWI_PODealerBL
    {
        #region Variables
        private readonly IMapper _poDealerMapper;
        #endregion

        #region Constructor
        public VWI_PODealerBL()
        {
            _poDealerMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_PODealer).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_PODealer by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_PODealerDto>> Read(VWI_PODealerFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_PODealer), "DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<VWI_PODealerDto>>();
            var sortColl = new SortCollection();

            try
            {
                // define sql
                var sql = Helper.GenerateSQLFromCriteriasAndSort(typeof(VWI_PODealer), filterDto, sortColl, criterias);

                // get data
                var data = _poDealerMapper.RetrieveSP("SELECT * FROM VWI_PODealer " + sql);
                if (data.Count > 0)
                {
                    // calculate the skip 
                    int skip = filterDto.pages < 1 ? 0 : (filterDto.pages - 1) * pageSize;

                    // filter out the data based on the paging                    
                    List<VWI_PODealer> list = new List<VWI_PODealer>();
                    if (sortColl != null && sortColl.Count > 0)
                        list = data.Cast<VWI_PODealer>().Skip(skip).Take(pageSize).ToList();
                    else
                        list = data.Cast<VWI_PODealer>().OrderBy(x => x.POHeaderId).Skip(skip).Take(pageSize).ToList();

                    // convert to dto object
                    List<VWI_PODealerDto> listData = list.ConvertList<VWI_PODealer, VWI_PODealerDto>();

                    result.lst = listData;
                    result.total = data.Count;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_PODealer), filterDto);
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

