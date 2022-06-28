#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : StatusChangeHistory business logic class
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
    public class StatusChangeHistoryBL : AbstractBusinessLogic, IStatusChangeHistoryBL
    {
        #region Variables
        private readonly IMapper _statusChangeHistoryMapper;
        private readonly AutoMapper.IMapper _mapper;

        #endregion

        #region Constructor
        public StatusChangeHistoryBL()
        {
            _statusChangeHistoryMapper = MapperFactory.GetInstance().GetMapper(typeof(StatusChangeHistory).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Insert data
        /// </summary>
        /// <param name="documentType"></param>
        /// <param name="documentNumber"></param>
        /// <param name="oldStatus"></param>
        /// <param name="newstatus"></param>
        /// <returns></returns>
        public ResponseBase<int> Insert(int documentType, string documentNumber, int oldStatus, int newstatus)
        {
            int result = 0;
            StatusChangeHistory objDomain = new StatusChangeHistory();
            objDomain.DocumentType = documentType;
            objDomain.DocumentRegNumber = documentNumber;
            objDomain.OldStatus = oldStatus;
            objDomain.NewStatus = newstatus;

            try
            {
                result = _statusChangeHistoryMapper.Insert(objDomain, DNetUserName);
            }
            catch
            {
                result = -1;
            }

            return new DNet.Interface.Model.ResponseBase<int>() { success = result != -1, lst = result };
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<StatusChangeHistoryDto> Create(StatusChangeHistoryParameterDto objCreate)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<StatusChangeHistoryDto> Update(StatusChangeHistoryParameterDto objUpdate)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Delete StatusChangeHistory by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<StatusChangeHistoryDto> Delete(int id)
        {
            var result = new ResponseBase<StatusChangeHistoryDto>();

            try
            {
                var statuschangehistory = (StatusChangeHistory)_statusChangeHistoryMapper.Retrieve(id);
                if (statuschangehistory != null)
                {
                    statuschangehistory.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _statusChangeHistoryMapper.Update(statuschangehistory, DNetUserName);
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
        /// Get StatusChangeHistory by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<StatusChangeHistoryDto>> Read(StatusChangeHistoryFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(StatusChangeHistory), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<StatusChangeHistoryDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(StatusChangeHistory), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(StatusChangeHistory), filterDto, sortColl);

                // get data
                var data = _statusChangeHistoryMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<StatusChangeHistory>().ToList();
                    var listData = list.Select(item => _mapper.Map<StatusChangeHistoryDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(StatusChangeHistory), filterDto);
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
        #endregion
    }
}
