#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Fleet business logic class
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
    public class FleetBL : AbstractBusinessLogic, IFleetBL
    {
        #region Variables
        private readonly IMapper _fleetMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public FleetBL()
        {
            _fleetMapper = MapperFactory.GetInstance().GetMapper(typeof(Fleet).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get Fleet by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<FleetDto>> Read(FleetFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(Fleet), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<FleetDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(Fleet), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(Fleet), filterDto, sortColl);

                // get data
                var data = _fleetMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<Fleet>().ToList();
                    var listData = list.Select(item => _mapper.Map<FleetDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(Fleet), filterDto);
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
        /// Delete Fleet by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<FleetDto> Delete(int id)
        {
            var result = new ResponseBase<FleetDto>();

            try
            {
                var fleet = (Fleet)_fleetMapper.Retrieve(id);
                if (fleet != null)
                {
                    fleet.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _fleetMapper.Update(fleet, DNetUserName);
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
        /// Create a new Fleet
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<FleetDto> Create(FleetParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update Fleet
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<FleetDto> Update(FleetParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}

