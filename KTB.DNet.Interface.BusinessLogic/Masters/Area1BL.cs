#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Area1 business logic class
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
    public class Area1BL : AbstractBusinessLogic, IArea1BL
    {
        #region Variables
        private readonly IMapper _area1Mapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public Area1BL()
        {
            _area1Mapper = MapperFactory.GetInstance().GetMapper(typeof(Area1).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get Area1 by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<Area1Dto>> Read(Area1FilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(Area1), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<Area1Dto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(Area1), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(Area1), filterDto, sortColl);

                // get data
                var data = _area1Mapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);

                if (data.Count > 0)
                {
                    var list = data.Cast<Area1>().ToList();
                    var listData = list.Select(item => _mapper.Map<Area1Dto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(Area1), filterDto);
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
        /// Delete Area1 by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<Area1Dto> Delete(int id)
        {
            var result = new ResponseBase<Area1Dto>();

            try
            {
                var area1 = (Area1)_area1Mapper.Retrieve(id);
                if (area1 != null)
                {
                    area1.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _area1Mapper.Update(area1, DNetUserName);
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
        /// Create a new Area1
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<Area1Dto> Create(Area1ParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update Area1
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<Area1Dto> Update(Area1ParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}

