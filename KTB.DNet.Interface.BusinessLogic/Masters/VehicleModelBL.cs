#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VehicleModel business logic class
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
    public class VehicleModelBL : AbstractBusinessLogic, IVehicleModelBL
    {
        #region Variables
        private readonly IMapper _vehiclemodelMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public VehicleModelBL()
        {
            _vehiclemodelMapper = MapperFactory.GetInstance().GetMapper(typeof(VechileModel).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VehicleModel by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VehicleModelDto>> Read(VehicleModelFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(VechileModel), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<VehicleModelDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(VechileModel), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VechileModel), filterDto, sortColl);

                // get data
                var data = _vehiclemodelMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VechileModel>().ToList();
                    var listData = list.Select(item => _mapper.Map<VehicleModelDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VechileModel), filterDto);
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
        /// Delete VehicleModel by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VehicleModelDto> Delete(int id)
        {
            var result = new ResponseBase<VehicleModelDto>();

            try
            {
                var vehiclemodel = (VechileModel)_vehiclemodelMapper.Retrieve(id);
                if (vehiclemodel != null)
                {
                    vehiclemodel.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _vehiclemodelMapper.Update(vehiclemodel, DNetUserName);
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
        /// Create a new VehicleModel
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VehicleModelDto> Create(VehicleModelParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VehicleModel
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VehicleModelDto> Update(VehicleModelParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}

