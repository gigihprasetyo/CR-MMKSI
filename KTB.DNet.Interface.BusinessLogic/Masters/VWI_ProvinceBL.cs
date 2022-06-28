#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_Province business logic class
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
    public class VWI_ProvinceBL : AbstractBusinessLogic, IVWI_ProvinceBL
    {
        #region Variables
        private readonly IMapper _vWI_provinceMapper;
        #endregion

        #region Constructor
        public VWI_ProvinceBL()
        {
            _vWI_provinceMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_Province).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_Province by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_ProvinceDto>> Read(VWI_ProvinceFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var result = new ResponseBase<List<VWI_ProvinceDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                var criterias = Helper.BuildCriteria(typeof(VWI_Province), filterDto);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_Province), filterDto, sortColl);

                var data = _vWI_provinceMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VWI_Province>().ToList();
                    List<VWI_ProvinceDto> listData = list.ConvertList<VWI_Province, VWI_ProvinceDto>();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_Province), filterDto);
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

