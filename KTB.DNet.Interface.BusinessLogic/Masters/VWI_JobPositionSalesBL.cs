#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_JobPositionSales business logic class
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
    public class VWI_JobPositionSalesBL : AbstractBusinessLogic, IVWI_JobPositionSalesBL
    {
        #region Variable
        private readonly IMapper _jobPositionSalesMapper;
        #endregion

        #region Constructor
        public VWI_JobPositionSalesBL()
        {
            _jobPositionSalesMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_JobPositionSales).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Read fleet data
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_JobPositionSalesDto>> Read(FilterDtoBase filterDto, int pageSize)
        {
            var result = new ResponseBase<List<VWI_JobPositionSalesDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                var criterias = Helper.BuildCriteria(typeof(VWI_JobPositionSales), filterDto);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_JobPositionSales), filterDto, sortColl);

                // get data
                var data = _jobPositionSalesMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VWI_JobPositionSales>().ToList();
                    List<VWI_JobPositionSalesDto> listData = list.ConvertList<VWI_JobPositionSales, VWI_JobPositionSalesDto>();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_JobPositionSales), filterDto);
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