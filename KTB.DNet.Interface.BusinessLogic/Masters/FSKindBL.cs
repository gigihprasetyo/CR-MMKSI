#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : FSKind business logic class
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
    public class FSKindBL : AbstractBusinessLogic, IFSKindBL
    {
        #region Variables
        private readonly IMapper _fskindMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public FSKindBL()
        {
            _fskindMapper = MapperFactory.GetInstance().GetMapper(typeof(FSKind).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get FSKind by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<FSKindDto>> Read(FSKindFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(FSKind), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(FSKind), "Status", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<FSKindDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(FSKind), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(FSKind), filterDto, sortColl);

                // get data
                var data = _fskindMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<FSKind>().ToList();
                    var listData = list.Select(item => _mapper.Map<FSKindDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(FSKind), filterDto);
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
        public ResponseBase<FSKindDto> Delete(int id)
        {
            var result = new ResponseBase<FSKindDto>();

            try
            {
                var fskind = (FSKind)_fskindMapper.Retrieve(id);
                if (fskind != null)
                {
                    fskind.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _fskindMapper.Update(fskind, DNetUserName);
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
        /// Create a new FSKind
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<FSKindDto> Create(FSKindParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update FSKind
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<FSKindDto> Update(FSKindParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}

