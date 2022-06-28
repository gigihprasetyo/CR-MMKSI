#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_SparePartConversion business logic class
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
    public class VWI_SparePartConversionBL : AbstractBusinessLogic, IVWI_SparePartConversionBL
    {
        #region Variables
        private readonly IMapper _sparePartConversionMapper;
        #endregion

        #region Constructor
        public VWI_SparePartConversionBL()
        {
            _sparePartConversionMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_SparePartConversion).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get SparePartConversion by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_SparePartConversionDto>> Read(VWI_SparePartConversionFilterDto filterDto, int pageSize)
        {
            var result = new ResponseBase<List<VWI_SparePartConversionDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                var criterias = Helper.BuildCriteria(typeof(VWI_SparePartConversion), filterDto);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_SparePartConversion), filterDto, sortColl);

                // get data
                var data = _sparePartConversionMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VWI_SparePartConversion>().ToList();
                    List<VWI_SparePartConversionDto> listData = list.ConvertList<VWI_SparePartConversion, VWI_SparePartConversionDto>();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_SparePartConversion), filterDto);
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