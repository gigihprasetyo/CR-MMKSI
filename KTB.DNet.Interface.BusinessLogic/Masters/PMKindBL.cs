#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PMKind business logic class
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
    public class PMKindBL : AbstractBusinessLogic, IPMKindBL
    {
        #region Variables
        private readonly IMapper _pmKindMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public PMKindBL()
        {
            _pmKindMapper = MapperFactory.GetInstance().GetMapper(typeof(PMKind).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get PMKind by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<PMKindDto>> Read(PMKindFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(PMKind), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<PMKindDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(PMKind), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(PMKind), filterDto, sortColl);

                // get data
                var data = _pmKindMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<PMKind>().ToList();
                    var listData = list.Select(item => _mapper.Map<PMKindDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(PMKind), filterDto);
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
        /// Delete FSKind by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<PMKindDto> Delete(int id)
        {
            var result = new ResponseBase<PMKindDto>();

            try
            {
                var pmKind = (FSKind)_pmKindMapper.Retrieve(id);
                if (pmKind != null)
                {
                    pmKind.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _pmKindMapper.Update(pmKind, DNetUserName);
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
        /// Create a new PMKind
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<PMKindDto> Create(PMKindParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update PMKind
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<PMKindDto> Update(PMKindParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}
