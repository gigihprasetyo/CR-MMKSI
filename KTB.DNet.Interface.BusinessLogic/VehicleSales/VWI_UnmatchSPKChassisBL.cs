#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_UnmatchSPKChassis business logic class
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
    public class VWI_UnmatchSPKChassisBL : AbstractBusinessLogic, IVWI_UnmatchSPKChassisBL
    {
        #region Variables
        private readonly IMapper _unmatchChassisMapper;
        #endregion

        #region Constructor
        public VWI_UnmatchSPKChassisBL()
        {
            _unmatchChassisMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_UnmatchSPKChassis).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_UnmatchSPKChassis by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_UnmatchSPKChassisDto>> Read(VWI_UnmatchSPKChassisFilterDto filterDto, int pageSize)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_UnmatchSPKChassis), "ChassisNumber", MatchType.Exact, filterDto.ChassisNumber));
            var result = new ResponseBase<List<VWI_UnmatchSPKChassisDto>>();
            var sortColl = new SortCollection();

            try
            {
                // define sql
                var sql = Helper.GenerateSQLFromCriteriasAndSort(typeof(VWI_UnmatchSPKChassis), filterDto, sortColl, criterias);

                // get data
                var data = _unmatchChassisMapper.RetrieveSP("SELECT * FROM VWI_UnmatchSPKChassis " + sql);
                if (data.Count > 0)
                {
                    // calculate the skip 
                    int skip = filterDto.pages < 1 ? 0 : (filterDto.pages - 1) * pageSize;

                    // filter out the data based on the paging                    
                    List<VWI_UnmatchSPKChassis> list = new List<VWI_UnmatchSPKChassis>();
                    if (sortColl != null && sortColl.Count > 0)
                        list = data.Cast<VWI_UnmatchSPKChassis>().Skip(skip).Take(pageSize).ToList();
                    else
                        list = data.Cast<VWI_UnmatchSPKChassis>().OrderBy(x => x.SPKNumber).Skip(skip).Take(pageSize).ToList();

                    // convert to dto object
                    List<VWI_UnmatchSPKChassisDto> listData = list.ConvertList<VWI_UnmatchSPKChassis, VWI_UnmatchSPKChassisDto>();

                    result.lst = listData;
                    result.total = data.Count;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_UnmatchSPKChassis), filterDto);
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

