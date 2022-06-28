#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_JobPositionServices business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region "Namespace Imports"
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
    public class VWI_JobPositionServicesBL : AbstractBusinessLogic, IVWI_JobPositionServicesBL
    {
        #region Variable
        private readonly IMapper _jobPositionServicesMapper;
        #endregion

        #region Constructor
        public VWI_JobPositionServicesBL()
        {
            _jobPositionServicesMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_JobPositionServices).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Read fleet data
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_JobPositionServicesDto>> Read(FilterDtoBase filterDto, int pageSize)
        {
            var result = new ResponseBase<List<VWI_JobPositionServicesDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                var criterias = Helper.BuildCriteria(typeof(VWI_JobPositionServices), filterDto);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_JobPositionServices), filterDto, sortColl);

                // get data
                var data = _jobPositionServicesMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VWI_JobPositionServices>().ToList();
                    List<VWI_JobPositionServicesDto> listData = list.ConvertList<VWI_JobPositionServices, VWI_JobPositionServicesDto>();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_JobPositionServices), filterDto);
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