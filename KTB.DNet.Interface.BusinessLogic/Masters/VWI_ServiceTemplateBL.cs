#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_ServiceTemplate business logic class
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
    public class VWI_ServiceTemplateBL : AbstractBusinessLogic, IVWI_ServiceTemplateBL
    {
        #region Variables
        private readonly IMapper _svcVTemplateMapper;
        #endregion

        #region Constructor
        public VWI_ServiceTemplateBL()
        {
            _svcVTemplateMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_ServiceTemplate).ToString());
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Get Service Template by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_ServiceTemplateDto>> Read(VWI_ServiceTemplateFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only            
            var result = new ResponseBase<List<VWI_ServiceTemplateDto>>();
            var sortColl = new SortCollection();

            try
            {
                // define sql
                var sql = Helper.GenerateSQLFromCriteriasAndSort(typeof(VWI_ServiceTemplate), filterDto, sortColl);

                // get data
                var data = _svcVTemplateMapper.RetrieveSP("SELECT * FROM VWI_ServiceTemplate " + sql);
                if (data.Count > 0)
                {
                    // calculate the skip 
                    int skip = filterDto.pages < 1 ? 0 : (filterDto.pages - 1) * pageSize;

                    // filter out the data based on the paging
                    var list = data.Cast<VWI_ServiceTemplate>().Skip(skip).Take(pageSize).ToList();

                    // convert to dto object
                    List<VWI_ServiceTemplateDto> listData = list.ConvertList<VWI_ServiceTemplate, VWI_ServiceTemplateDto>();

                    result.lst = listData;
                    result.total = data.Count;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_ServiceTemplate), filterDto);
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

