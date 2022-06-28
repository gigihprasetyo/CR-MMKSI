#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_MSPMembership business logic class
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
    public class VWI_MSPMembershipBL : AbstractBusinessLogic, IVWI_MSPMembershipBL
    {
        #region Variables
        private readonly IMapper _mspmembershipMapper;
        #endregion

        #region Constructor
        public VWI_MSPMembershipBL()
        {
            _mspmembershipMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_MSPMembership).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get MSPMembership by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_MSPMembershipDto>> Read(VWI_MSPMembershipFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only                     
            var result = new ResponseBase<List<VWI_MSPMembershipDto>>();
            var sortColl = new SortCollection();

            try
            {
                // get sql
                var sql = Helper.GenerateSQLFromCriteriasAndSort(typeof(VWI_MSPMembership), filterDto, sortColl);

                // get data
                var data = _mspmembershipMapper.RetrieveSP("SELECT * FROM VWI_MSPMembership " + sql);
                if (data.Count > 0)
                {
                    // calculate the skip 
                    int skip = filterDto.pages < 1 ? 0 : (filterDto.pages - 1) * pageSize;

                    // filter out the data based on the paging                    
                    List<VWI_MSPMembership> list = new List<VWI_MSPMembership>();
                    if (sortColl != null && sortColl.Count > 0)
                        list = data.Cast<VWI_MSPMembership>().Skip(skip).Take(pageSize).ToList();
                    else
                        list = data.Cast<VWI_MSPMembership>().OrderBy(x => x.MSPCustomerID).Skip(skip).Take(pageSize).ToList();

                    // convert to dto object
                    List<VWI_MSPMembershipDto> listData = list.ConvertList<VWI_MSPMembership, VWI_MSPMembershipDto>();

                    result.lst = listData;
                    result.total = data.Count;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_MSPMembership), filterDto);
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

