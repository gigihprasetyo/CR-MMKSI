﻿#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VehicleType business logic class
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
    public class VehicleTypeBL : AbstractBusinessLogic, IVehicleTypeBL
    {
        #region Variables
        private readonly IMapper _vehicletypeMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public VehicleTypeBL()
        {
            _vehicletypeMapper = MapperFactory.GetInstance().GetMapper(typeof(VechileType).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VehicleType by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VehicleTypeDto>> Read(VehicleTypeFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(VechileType), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<VehicleTypeDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(VechileType), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VechileType), filterDto, sortColl);

                // get data
                var data = _vehicletypeMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VechileType>().ToList();
                    var listData = list.Select(item => _mapper.Map<VehicleTypeDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VechileType), filterDto);
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
        /// Delete VehicleType by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VehicleTypeDto> Delete(int id)
        {
            var result = new ResponseBase<VehicleTypeDto>();

            try
            {
                var vehicletype = (VechileType)_vehicletypeMapper.Retrieve(id);
                if (vehicletype != null)
                {
                    vehicletype.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _vehicletypeMapper.Update(vehicletype, DNetUserName);
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
        /// Create a new VehicleType
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VehicleTypeDto> Create(VehicleTypeParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VehicleType
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VehicleTypeDto> Update(VehicleTypeParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}

