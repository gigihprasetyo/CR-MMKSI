#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VehicleColor business logic class
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
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.Interface.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class VehicleColorBL : AbstractBusinessLogic, IVehicleColorBL
    {
        #region Variables
        private readonly IMapper _vehiclecolorMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public VehicleColorBL()
        {
            _vehiclecolorMapper = MapperFactory.GetInstance().GetMapper(typeof(VechileColor).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VehicleColor by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VehicleColorDto>> Read(VehicleColorFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(VechileColor), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<VehicleColorDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(VechileColor), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VechileColor), filterDto, sortColl);

                // get data
                var data = _vehiclecolorMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VechileColor>().ToList();
                    var listData = list.Select(item => _mapper.Map<VehicleColorDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VechileColor), filterDto);
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

        /// <summary>
        /// Delete VehicleColor by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VehicleColorDto> Delete(int id)
        {
            var result = new ResponseBase<VehicleColorDto>();

            try
            {
                var vehiclecolor = (VechileColor)_vehiclecolorMapper.Retrieve(id);
                if (vehiclecolor != null)
                {
                    vehiclecolor.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _vehiclecolorMapper.Update(vehiclecolor, DNetUserName);
                    if (nResult != 0)
                    {
                        result.success = true;
                        result._id = id;
                        result.total = 1;
                    }
                    else
                    {
                        ErrorMsgHelper.ErrorMsgDBSave(result.messages);
                    }
                }
                else
                {
                    ErrorMsgHelper.DeleteNotAvailable(result.messages);
                }
            }
            catch (SqlException ex)
            {
                ErrorMsgHelper.SqlException(result.messages, ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Create a new VehicleColor
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VehicleColorDto> Create(VehicleColorParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VehicleColor
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VehicleColorDto> Update(VehicleColorParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}

