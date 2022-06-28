#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_Leasing business logic class
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
    public class VWI_LeasingBL : AbstractBusinessLogic, IVWI_LeasingBL
    {
        #region Variables
        private readonly IMapper _LeasingMapper;
        #endregion

        #region Constructor
        public VWI_LeasingBL()
        {
            _LeasingMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_Leasing).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get Leasing by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_LeasingDto>> Read(VWI_LeasingFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only          
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_Leasing), "LeasingCode", MatchType.IsNotNull, null));
            var result = new ResponseBase<List<VWI_LeasingDto>>();
            var sortColl = new SortCollection();

            try
            {
                // define sql
                var sql = Helper.GenerateSQLFromCriteriasAndSort(typeof(VWI_Leasing), filterDto, sortColl, criterias);

                // get data
                var data = _LeasingMapper.RetrieveSP("SELECT * FROM VWI_Leasing " + sql);
                if (data.Count > 0)
                {
                    // calculate the skip 
                    int skip = filterDto.pages < 1 ? 0 : (filterDto.pages - 1) * pageSize;

                    // filter out the data based on the paging                    
                    List<VWI_Leasing> list = new List<VWI_Leasing>();
                    if (sortColl != null && sortColl.Count > 0)
                        list = data.Cast<VWI_Leasing>().Skip(skip).Take(pageSize).ToList();
                    else
                        list = data.Cast<VWI_Leasing>().OrderBy(x => x.LeasingCode).Skip(skip).Take(pageSize).ToList();

                    // convert to dto object
                    List<VWI_LeasingDto> listData = list.ConvertList<VWI_Leasing, VWI_LeasingDto>();

                    result.lst = listData;
                    result.total = data.Count;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_Leasing), filterDto);
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

