#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_BusinessSector business logic class
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
    public class VWI_BusinessSectorBL : AbstractBusinessLogic, IVWI_BusinessSectorBL
    {
        #region Variables
        private readonly IMapper _businessSectorMapper;
        #endregion

        #region Constructor
        public VWI_BusinessSectorBL()
        {
            _businessSectorMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_BusinessSector).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_BusinessSector by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_BusinessSectorDto>> Read(VWI_BusinessSectorFilterDto filterDto, int pageSize)
        {
            var result = new ResponseBase<List<VWI_BusinessSectorDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                var criterias = Helper.BuildCriteria(typeof(VWI_BusinessSector), filterDto);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_BusinessSector), filterDto, sortColl);

                // get data
                var data = _businessSectorMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VWI_BusinessSector>().ToList();
                    List<VWI_BusinessSectorDto> listData = list.ConvertList<VWI_BusinessSector, VWI_BusinessSectorDto>();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_BusinessSector), filterDto);
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

