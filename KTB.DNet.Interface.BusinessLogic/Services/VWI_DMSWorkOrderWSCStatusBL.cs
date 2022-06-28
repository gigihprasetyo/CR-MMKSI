#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_DMSWorkOrderWSCStatus business logic class
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
    public class VWI_DMSWorkOrderWSCStatusBL : AbstractBusinessLogic, IVWI_DMSWorkOrderWSCStatusBL
    {
        #region Variables
        private readonly IMapper _dmsWorkOrderWSCStatusMapper;
        #endregion

        #region Constructor
        public VWI_DMSWorkOrderWSCStatusBL()
        {
            _dmsWorkOrderWSCStatusMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_DMSWorkOrderWSCStatus).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get DMSWorkOrderWSCStatus by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_DMSWorkOrderWSCStatusDto>> Read(VWI_DMSWorkOrderWSCStatusFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only          
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_DMSWorkOrderWSCStatus), "DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<VWI_DMSWorkOrderWSCStatusDto>>();
            var sortColl = new SortCollection();

            try
            {
                // define sql
                var sql = Helper.GenerateSQLFromCriteriasAndSort(typeof(VWI_DMSWorkOrderWSCStatus), filterDto, sortColl, criterias);

                // get data
                var data = _dmsWorkOrderWSCStatusMapper.RetrieveSP("SELECT * FROM VWI_DMSWorkOrderWSCStatus " + sql);
                if (data.Count > 0)
                {
                    // calculate the skip 
                    int skip = filterDto.pages < 1 ? 0 : (filterDto.pages - 1) * pageSize;

                    // filter out the data based on the paging
                    List<VWI_DMSWorkOrderWSCStatus> list = new List<VWI_DMSWorkOrderWSCStatus>();
                    if (sortColl != null && sortColl.Count > 0)
                        list = data.Cast<VWI_DMSWorkOrderWSCStatus>().Skip(skip).Take(pageSize).ToList();
                    else
                        list = data.Cast<VWI_DMSWorkOrderWSCStatus>().OrderBy(x => x.ChassisNumber).Skip(skip).Take(pageSize).ToList();

                    // convert to dto object
                    List<VWI_DMSWorkOrderWSCStatusDto> listData = list.ConvertList<VWI_DMSWorkOrderWSCStatus, VWI_DMSWorkOrderWSCStatusDto>();

                    result.lst = listData;
                    result.total = listData.Count;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_DMSWorkOrderWSCStatus), filterDto);
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

