#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : EstimationEquipDetail business logic class
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
    public class EstimationEquipDetailBL : AbstractBusinessLogic, IEstimationEquipDetailBL
    {
        #region Variables
        private readonly IMapper _estimationEquipDetailMapper;
        private readonly AutoMapper.IMapper _mapper;
        private StandardCodeBL _enumBL;
        #endregion

        #region Constructor
        public EstimationEquipDetailBL()
        {
            _estimationEquipDetailMapper = MapperFactory.GetInstance().GetMapper(typeof(EstimationEquipDetail).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _enumBL = new StandardCodeBL(_mapper);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get EstimationEquipDetail by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<EstimationEquipDetailDto>> Read(EstimationEquipDetailFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(EstimationEquipDetail), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<EstimationEquipDetailDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(EstimationEquipDetail), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(EstimationEquipDetail), filterDto, sortColl);

                // get data
                var data = _estimationEquipDetailMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<EstimationEquipDetail>().ToList();
                    var listData = list.Select(item => _mapper.Map<EstimationEquipDetailDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(EstimationEquipDetail), filterDto);
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
        /// Delete EstimationEquipDetail by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<EstimationEquipDetailDto> Delete(int id)
        {
            var result = new ResponseBase<EstimationEquipDetailDto>();

            try
            {
                var estimationequipheader = (EstimationEquipDetail)_estimationEquipDetailMapper.Retrieve(id);
                if (estimationequipheader != null)
                {
                    estimationequipheader.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _estimationEquipDetailMapper.Update(estimationequipheader, DNetUserName);
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
        /// Create a new EstimationEquipDetail
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<EstimationEquipDetailDto> Create(EstimationEquipDetailParameterDto objCreate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update EstimationEquipDetail
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<EstimationEquipDetailDto> Update(EstimationEquipDetailParameterDto objUpdate)
        {
            return null;
        }

        #endregion
    }
}

