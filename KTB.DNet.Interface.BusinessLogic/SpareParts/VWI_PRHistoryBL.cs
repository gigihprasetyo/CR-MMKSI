#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_PRHistory SO and DO business logic class
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
    public class VWI_PRHistoryBL : AbstractBusinessLogic, IVWI_PRHistoryBL
    {
        #region Variables
        private readonly IMapper _pRHistorySOMapper;
        private readonly IMapper _pRHistoryDOMapper;
        #endregion

        #region Constructor
        public VWI_PRHistoryBL()
        {
            _pRHistorySOMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_PRHistorySO).ToString());
            _pRHistoryDOMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_PRHistoryDO).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get PRHistorySO
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_PRHistorySODto>> GetPRHistorySO(VWI_PRHistorySOFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_PRHistorySO), "DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<VWI_PRHistorySODto>>();
            var sortColl = new SortCollection();

            try
            {
                // define sql                
                var sql = Helper.GenerateSQLFromCriteriasAndSort(typeof(VWI_PRHistorySO), filterDto, sortColl, criterias);

                // get data
                var data = _pRHistorySOMapper.RetrieveSP("SELECT * FROM VWI_PRHistorySO " + sql);
                if (data.Count > 0)
                {
                    // calculate the skip 
                    int skip = filterDto.pages < 1 ? 0 : (filterDto.pages - 1) * pageSize;

                    // filter out the data based on the paging
                    var list = data.Cast<VWI_PRHistorySO>().Skip(skip).Take(pageSize).ToList();

                    // convert to dto object
                    List<VWI_PRHistorySODto> listData = list.ConvertList<VWI_PRHistorySO, VWI_PRHistorySODto>();

                    result.lst = listData;
                    result.total = data.Count;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_PRHistorySO), filterDto);
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
        /// Get PRHistoryDO
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_PRHistoryDODto>> GetPRHistoryDO(VWI_PRHistoryDOFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_PRHistoryDO), "DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<VWI_PRHistoryDODto>>();
            var sortColl = new SortCollection();

            try
            {
                // define sql
                var sql = Helper.GenerateSQLFromCriteriasAndSort(typeof(VWI_PRHistoryDO), filterDto, sortColl, criterias);

                // get data
                var data = _pRHistoryDOMapper.RetrieveSP("SELECT * FROM VWI_PRHistoryDO " + sql);
                if (data.Count > 0)
                {
                    // calculate the skip 
                    int skip = filterDto.pages < 1 ? 0 : (filterDto.pages - 1) * pageSize;

                    // filter out the data based on the paging
                    var list = data.Cast<VWI_PRHistoryDO>().Skip(skip).Take(pageSize).ToList();

                    // convert to dto object
                    List<VWI_PRHistoryDODto> listData = list.ConvertList<VWI_PRHistoryDO, VWI_PRHistoryDODto>();

                    result.lst = listData;
                    result.total = data.Count;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_PRHistoryDO), filterDto);
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
        /// Default read
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_PRHistorySODto>> Read(VWI_PRHistorySOFilterDto filterDto, int pageSize)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
