#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_SparePartUoM business logic class
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
    public class VWI_SparePartUoMBL : AbstractBusinessLogic, IVWI_SparePartUoMBL
    {
        #region Variables
        private readonly IMapper _sparepartuomMapper;
        #endregion

        #region Constructor
        public VWI_SparePartUoMBL()
        {
            _sparepartuomMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_SparePartUoM).ToString());
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Get VWI_SparePartUoMBL by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_SparePartUoMDto>> Read(VWI_SparePartUoMFilterDto filterDto, int pageSize)
        {
            var result = new ResponseBase<List<VWI_SparePartUoMDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                var criterias = Helper.BuildCriteria(typeof(VWI_SparePartUoM), filterDto);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_SparePartUoM), filterDto, sortColl);

                // get data
                var data = _sparepartuomMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VWI_SparePartUoM>().ToList();
                    List<VWI_SparePartUoMDto> listData = list.ConvertList<VWI_SparePartUoM, VWI_SparePartUoMDto>();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_SparePartUoM), filterDto);
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