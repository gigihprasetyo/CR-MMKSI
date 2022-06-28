#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : EstimationEquipPO business logic class
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
    public class EstimationEquipPOBL : AbstractBusinessLogic, IEstimationEquipPOBL
    {
        #region Variables
        private readonly IMapper _estimationequippoMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public EstimationEquipPOBL()
        {
            _estimationequippoMapper = MapperFactory.GetInstance().GetMapper(typeof(EstimationEquipPO).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get EstimationEquipPO by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<EstimationEquipPODto>> Read(EstimationEquipPOFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(EstimationEquipPO), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<EstimationEquipPODto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(EstimationEquipPO), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(EstimationEquipPO), filterDto, sortColl);

                // get data
                var data = _estimationequippoMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<EstimationEquipPO>().ToList();
                    var listData = list.Select(item => _mapper.Map<EstimationEquipPODto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(EstimationEquipPO), filterDto);
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
        /// Delete EstimationEquipPO by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<EstimationEquipPODto> Delete(int id)
        {
            var result = new ResponseBase<EstimationEquipPODto>();

            try
            {
                var estimationequippo = (EstimationEquipPO)_estimationequippoMapper.Retrieve(id);
                if (estimationequippo != null)
                {
                    estimationequippo.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _estimationequippoMapper.Update(estimationequippo, DNetUserName);
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
        /// Create a new EstimationEquipPO
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<EstimationEquipPODto> Create(EstimationEquipPOParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update EstimationEquipPO
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<EstimationEquipPODto> Update(EstimationEquipPOParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}

