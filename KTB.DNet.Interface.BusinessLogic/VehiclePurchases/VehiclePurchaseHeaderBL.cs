#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VehiclePurchaseHeader business logic class
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
    public class VehiclePurchaseHeaderBL : AbstractBusinessLogic, IVehiclePurchaseHeaderBL
    {
        #region Variables
        private readonly IMapper _vehiclePurchaseHeaderMapper;
        private readonly AutoMapper.IMapper _mapper;

        #endregion

        #region Constructor
        public VehiclePurchaseHeaderBL()
        {
            _vehiclePurchaseHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(VehiclePurchaseHeader).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VehiclePurchaseHeader by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VehiclePurchaseHeaderDto>> Read(VehiclePurchaseHeaderFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(VehiclePurchaseHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<VehiclePurchaseHeaderDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(VehiclePurchaseHeader), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VehiclePurchaseHeader), filterDto, sortColl);

                // get data
                var data = _vehiclePurchaseHeaderMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VehiclePurchaseHeader>().ToList();
                    var listData = list.Select(item => _mapper.Map<VehiclePurchaseHeaderDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VehiclePurchaseHeader), filterDto);
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
        /// Delete VehiclePurchaseHeader by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VehiclePurchaseHeaderDto> Delete(int id)
        {
            var result = new ResponseBase<VehiclePurchaseHeaderDto>();

            try
            {
                var vehiclepurchaseheader = (VehiclePurchaseHeader)_vehiclePurchaseHeaderMapper.Retrieve(id);
                if (vehiclepurchaseheader != null)
                {
                    vehiclepurchaseheader.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _vehiclePurchaseHeaderMapper.Update(vehiclepurchaseheader, DNetUserName);
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
                ErrorMsgHelper.SqlExceptionRead(result.messages, ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Create a new VehiclePurchaseHeader
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VehiclePurchaseHeaderDto> Create(VehiclePurchaseHeaderParameterDto objCreate)
        {
            var result = new ResponseBase<VehiclePurchaseHeaderDto>();
            var validationResults = new List<DNetValidationResult>();

            try
            {
                // create VehiclePurchaseHeader object
                var newVehiclePurchaseHeader = _mapper.Map<VehiclePurchaseHeader>(objCreate);
                newVehiclePurchaseHeader.CreatedTime = DateTime.Now;

                var success = (int)_vehiclePurchaseHeaderMapper.Insert(newVehiclePurchaseHeader, newVehiclePurchaseHeader.CreatedBy);
                if (success > 0)
                {
                    result.success = true;
                    result._id = success;
                    result.total = 1;
                }
                else
                {
                    ErrorMsgHelper.DataCorrupt(result.messages);
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
        /// Update VehiclePurchaseHeader
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VehiclePurchaseHeaderDto> Update(VehiclePurchaseHeaderParameterDto objUpdate)
        {
            var result = new ResponseBase<VehiclePurchaseHeaderDto>();
            var validationResults = new List<DNetValidationResult>();

            try
            {
                var newVehiclePurchaseHeader = _mapper.Map<VehiclePurchaseHeader>(objUpdate);
                newVehiclePurchaseHeader.LastUpdateTime = DateTime.Now;

                var success = (int)_vehiclePurchaseHeaderMapper.Update(newVehiclePurchaseHeader, newVehiclePurchaseHeader.CreatedBy);
                if (success > 0)
                {
                    result.success = true;
                    result._id = success;
                    result.total = 1;
                }
                else
                {
                    ErrorMsgHelper.UpdateNotAvailable(result.messages);
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
        #endregion
    }
}

