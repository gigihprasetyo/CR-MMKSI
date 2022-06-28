#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Area2 business logic class
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
    public class Area2BL : AbstractBusinessLogic, IArea2BL
    {
        #region Variables
        private readonly IMapper _area2Mapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public Area2BL()
        {
            _area2Mapper = MapperFactory.GetInstance().GetMapper(typeof(Area2).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get Area2 by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<Area2Dto>> Read(Area2FilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(Area2), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<Area2Dto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(Area2), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(Area2), filterDto, sortColl);

                // get data
                var data = _area2Mapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<Area2>().ToList();
                    var listData = list.Select(item => _mapper.Map<Area2Dto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(Area2), filterDto);
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
        /// Delete Area2 by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<Area2Dto> Delete(int id)
        {
            var result = new ResponseBase<Area2Dto>();

            try
            {
                var area2 = (Area2)_area2Mapper.Retrieve(id);
                if (area2 != null)
                {
                    area2.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _area2Mapper.Update(area2, DNetUserName);
                    if (nResult != 0)
                    {
                        result._id = id;
                        result.total = 1;
                        result.success = true;
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
        /// Create a new Area2
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<Area2Dto> Create(Area2ParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update Area2
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<Area2Dto> Update(Area2ParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}

