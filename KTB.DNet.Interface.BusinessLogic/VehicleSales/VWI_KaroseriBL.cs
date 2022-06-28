#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_Karoseri business logic class
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
    public class VWI_KaroseriBL : AbstractBusinessLogic, IVWI_KaroseriBL
    {
        #region Variables
        private readonly IMapper _karoseriMapper;
        #endregion

        #region Constructor
        public VWI_KaroseriBL()
        {
            _karoseriMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_Karoseri).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get Karoseri by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_KaroseriDto>> Read(VWI_KaroseriFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only          
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_Karoseri), "Code", MatchType.IsNotNull, null));
            var result = new ResponseBase<List<VWI_KaroseriDto>>();
            var sortColl = new SortCollection();

            try
            {
                // define sql
                var sql = Helper.GenerateSQLFromCriteriasAndSort(typeof(VWI_Karoseri), filterDto, sortColl, criterias);

                // get data
                var data = _karoseriMapper.RetrieveSP("SELECT * FROM VWI_Karoseri " + sql);
                if (data.Count > 0)
                {
                    // calculate the skip 
                    int skip = filterDto.pages < 1 ? 0 : (filterDto.pages - 1) * pageSize;

                    // filter out the data based on the paging                    
                    List<VWI_Karoseri> list = new List<VWI_Karoseri>();
                    if (sortColl != null && sortColl.Count > 0)
                        list = data.Cast<VWI_Karoseri>().Skip(skip).Take(pageSize).ToList();
                    else
                        list = data.Cast<VWI_Karoseri>().OrderBy(x => x.Code).Skip(skip).Take(pageSize).ToList();

                    // convert to dto object
                    List<VWI_KaroseriDto> listData = list.ConvertList<VWI_Karoseri, VWI_KaroseriDto>();

                    result.lst = listData;
                    result.total = data.Count;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_Karoseri), filterDto);
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

