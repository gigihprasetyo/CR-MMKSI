#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_FieldFixServiced business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 7/11/2018 10:38
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
    public class VWI_FieldFixServicedBL : AbstractBusinessLogic, IVWI_FieldFixServicedBL
    {
        #region Variables
        private readonly IMapper _fieldFixServicedMapper;
        #endregion

        #region Constructor
        public VWI_FieldFixServicedBL()
        {
            _fieldFixServicedMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_FieldFixServiced).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_FieldFixServiced by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_FieldFixServicedDto>> Read(FilterDtoBase filterDto, int pageSize)
        {
            var result = new ResponseBase<List<VWI_FieldFixServicedDto>>();
            var sortColl = new SortCollection();

            try
            {
                // get sql
                var sql = Helper.GenerateSQLFromCriteriasAndSort(typeof(VWI_FieldFixServiced), filterDto, sortColl);

                // get data
                var data = _fieldFixServicedMapper.RetrieveSP("SELECT * FROM VWI_RecallChassisServiced " + sql);
                if (data.Count > 0)
                {
                    // calculate the skip 
                    int skip = filterDto.pages < 1 ? 0 : (filterDto.pages - 1) * pageSize;

                    // filter out the data based on the paging                    
                    List<VWI_FieldFixServiced> list = new List<VWI_FieldFixServiced>();
                    if (sortColl != null && sortColl.Count > 0)
                        list = data.Cast<VWI_FieldFixServiced>().Skip(skip).Take(pageSize).ToList();
                    else
                        list = data.Cast<VWI_FieldFixServiced>().OrderBy(x => x.ID).Skip(skip).Take(pageSize).ToList();

                    // convert to dto object
                    List<VWI_FieldFixServicedDto> listData = list.ConvertList<VWI_FieldFixServiced, VWI_FieldFixServicedDto>();

                    result.lst = listData;
                    result.total = listData.Count;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_FieldFixServiced), filterDto);
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

