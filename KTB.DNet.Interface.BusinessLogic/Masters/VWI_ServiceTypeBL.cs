#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_ServiceType business logic class
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
    public class VWI_ServiceTypeBL : AbstractBusinessLogic, IVWI_ServiceTypeBL
    {
        #region Variables
        private readonly IMapper _serviceTypeMapper;
        #endregion

        #region Constructor
        public VWI_ServiceTypeBL()
        {
            _serviceTypeMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_ServiceType).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_ServiceType by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_ServiceTypeDto>> Read(VWI_ServiceTypeFilterDto filterDto, int pageSize)
        {
            var result = new ResponseBase<List<VWI_ServiceTypeDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                var criterias = Helper.BuildCriteria(typeof(VWI_ServiceType), filterDto);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_ServiceType), filterDto, sortColl);

                // get data
                var data = _serviceTypeMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VWI_ServiceType>().ToList();
                    List<VWI_ServiceTypeDto> listData = list.ConvertList<VWI_ServiceType, VWI_ServiceTypeDto>();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_ServiceType), filterDto);
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

