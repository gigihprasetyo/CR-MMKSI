#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_VehicleExteriorColor business logic class
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
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.Interface.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class VWI_VehicleExteriorColorBL : AbstractBusinessLogic, IVWI_VehicleExteriorColorBL
    {
        #region Variables
        private readonly IMapper _vehicleExteriorColorMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public VWI_VehicleExteriorColorBL()
        {
            _vehicleExteriorColorMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_VehicleExteriorColor).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Read Vehicle Code master data
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_VehicleExteriorColorDto>> Read(VWI_VehicleExteriorColorFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_VehicleExteriorColor), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<VWI_VehicleExteriorColorDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(VWI_VehicleExteriorColor), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_VehicleExteriorColor), filterDto, sortColl);

                // get data
                var data = _vehicleExteriorColorMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VWI_VehicleExteriorColor>().ToList();
                    var listData = list.Select(item => _mapper.Map<VWI_VehicleExteriorColorDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_VehicleExteriorColor), filterDto);
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

