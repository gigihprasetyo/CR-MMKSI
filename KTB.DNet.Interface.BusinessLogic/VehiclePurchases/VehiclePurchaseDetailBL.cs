#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VehiclePurchaseDetail business logic class
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
    public class VehiclePurchaseDetailBL : AbstractBusinessLogic, IVehiclePurchaseDetailBL
    {
        #region Variables
        private readonly IMapper _vehiclePurchaseDetailMapper;
        private readonly AutoMapper.IMapper _mapper;

        #endregion

        #region Constructor
        public VehiclePurchaseDetailBL()
        {
            _vehiclePurchaseDetailMapper = MapperFactory.GetInstance().GetMapper(typeof(VehiclePurchaseDetail).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VehiclePurchaseDetail by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VehiclePurchaseDetailDto>> Read(VehiclePurchaseDetailFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(VehiclePurchaseDetail), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<VehiclePurchaseDetailDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(VehiclePurchaseDetail), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VehiclePurchaseDetail), filterDto, sortColl);

                // get data
                var data = _vehiclePurchaseDetailMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VehiclePurchaseDetail>().ToList();
                    var listData = list.Select(item => _mapper.Map<VehiclePurchaseDetailDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VehiclePurchaseDetail), filterDto);
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
        /// Delete VehiclePurchaseDetail by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VehiclePurchaseDetailDto> Delete(int id)
        {
            var result = new ResponseBase<VehiclePurchaseDetailDto>();

            try
            {
                var vehiclepurchasedetail = (VehiclePurchaseDetail)_vehiclePurchaseDetailMapper.Retrieve(id);
                if (vehiclepurchasedetail != null)
                {
                    vehiclepurchasedetail.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _vehiclePurchaseDetailMapper.Update(vehiclepurchasedetail, DNetUserName);
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
        /// Create a new VehiclePurchaseDetail
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VehiclePurchaseDetailDto> Create(VehiclePurchaseDetailParameterDto objCreate)
        {
            var result = new ResponseBase<VehiclePurchaseDetailDto>();
            var validationResults = new List<DNetValidationResult>();

            try
            {
                // create VehiclePurchaseDetail object
                var newVehiclePurchaseDetail = _mapper.Map<VehiclePurchaseDetail>(objCreate);
                newVehiclePurchaseDetail.CreatedTime = DateTime.Now;

                var success = (int)_vehiclePurchaseDetailMapper.Insert(newVehiclePurchaseDetail, newVehiclePurchaseDetail.CreatedBy);
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
        /// Update VehiclePurchaseDetail
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VehiclePurchaseDetailDto> Update(VehiclePurchaseDetailParameterDto objUpdate)
        {
            var result = new ResponseBase<VehiclePurchaseDetailDto>();
            var validationResults = new List<DNetValidationResult>();

            try
            {
                //Process update
                var newVehiclePurchaseDetail = _mapper.Map<VehiclePurchaseDetail>(objUpdate);
                newVehiclePurchaseDetail.LastUpdateTime = DateTime.Now;

                var success = (int)_vehiclePurchaseDetailMapper.Update(newVehiclePurchaseDetail, newVehiclePurchaseDetail.CreatedBy);
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

