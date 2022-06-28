#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PositionWSC business logic class
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
    public class PositionWSCBL : AbstractBusinessLogic, IPositionWSCBL
    {
        #region Variables
        private readonly IMapper _positionwscMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public PositionWSCBL()
        {
            _positionwscMapper = MapperFactory.GetInstance().GetMapper(typeof(PositionWSC).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get PositionWSC by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<PositionWSCDto>> Read(PositionWSCFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(PositionWSC), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<PositionWSCDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(PositionWSC), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(PositionWSC), filterDto, sortColl);

                // get data
                var data = _positionwscMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<PositionWSC>().ToList();
                    var listData = list.Select(item => _mapper.Map<PositionWSCDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(PositionWSC), filterDto);
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
        /// Delete PositionWSC by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<PositionWSCDto> Delete(int id)
        {
            var result = new ResponseBase<PositionWSCDto>();

            try
            {
                var positionwsc = (PositionWSC)_positionwscMapper.Retrieve(id);
                if (positionwsc != null)
                {
                    positionwsc.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _positionwscMapper.Update(positionwsc, DNetUserName);
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
        /// Create a new PositionWSC
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<PositionWSCDto> Create(PositionWSCParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update PositionWSC
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<PositionWSCDto> Update(PositionWSCParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}

