﻿#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_CustomerVehicle business logic class
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
    public class VWI_CustomerVehicleBL : AbstractBusinessLogic, IVWI_CustomerVehicleBL
    {
        #region Variables
        private readonly IMapper _customervehicleMapper;
        #endregion

        #region Constructor
        public VWI_CustomerVehicleBL()
        {
            _customervehicleMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_CustomerVehicle).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CustomerVehicle by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CustomerVehicleDto>> Read(VWI_CustomerVehicleFilterDto filterDto, int pageSize)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_CustomerVehicle), "DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<VWI_CustomerVehicleDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(VWI_CustomerVehicle), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_CustomerVehicle), filterDto, sortColl);

                // get data
                var data = _customervehicleMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VWI_CustomerVehicle>().ToList();
                    List<VWI_CustomerVehicleDto> listData = list.ConvertList<VWI_CustomerVehicle, VWI_CustomerVehicleDto>();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CustomerVehicle), filterDto);
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

