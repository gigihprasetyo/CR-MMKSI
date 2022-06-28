#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_Fleet business logic class
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
    public class VWI_FleetBL : AbstractBusinessLogic, IVWI_FleetBL
    {
        #region Variable
        private readonly IMapper _vwi_fleetMapper;
        #endregion

        #region Constructor
        public VWI_FleetBL()
        {
            _vwi_fleetMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_Fleet).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Read fleet data
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_FleetDto>> Read(VWI_FleetFilterDto filterDto, int pageSize)
        {
            var result = new ResponseBase<List<VWI_FleetDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                var criterias = Helper.BuildCriteria(typeof(VWI_Fleet), filterDto);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_Fleet), filterDto, sortColl);

                // get data
                var data = _vwi_fleetMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VWI_Fleet>().ToList();
                    List<VWI_FleetDto> listData = list.ConvertList<VWI_Fleet, VWI_FleetDto>();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_Fleet), filterDto);
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