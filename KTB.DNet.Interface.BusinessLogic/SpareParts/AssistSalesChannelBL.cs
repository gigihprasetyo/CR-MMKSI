#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AssistSalesChannel business logic class
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
    public class AssistSalesChannelBL : AbstractBusinessLogic, IAssistSalesChannelBL
    {
        #region Variables
        private readonly IMapper _assistSalesChannelMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public AssistSalesChannelBL()
        {
            _assistSalesChannelMapper = MapperFactory.GetInstance().GetMapper(typeof(AssistSalesChannel).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get AssistSalesChannel by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<AssistSalesChannelDto>> ReadMaster(AssistSalesChannelFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var result = new ResponseBase<List<AssistSalesChannelDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                var criterias = Helper.BuildCriteria(typeof(AssistSalesChannel), filterDto);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(AssistSalesChannel), filterDto, sortColl);

                // get data
                var data = _assistSalesChannelMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<AssistSalesChannel>().ToList();
                    var listData = new List<AssistSalesChannelDto>();
                    foreach (var item in list)
                    {
                        // map it
                        var assistsaleschannelDto = _mapper.Map<AssistSalesChannelDto>(item);

                        // validate the status
                        if (item.RowStatus == -1)
                        {
                            assistsaleschannelDto.Status = item.RowStatus;
                        }
                        else if (item.RowStatus == 0)
                        {
                            assistsaleschannelDto.Status = item.Status - 1;
                        }

                        // add to list
                        listData.Add(assistsaleschannelDto);
                    }

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(AssistSalesChannel), filterDto);
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
        /// Get AssistSalesChannel by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <returns></returns>
        public ResponseBase<List<AssistSalesChannelDto>> Read(AssistSalesChannelFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(AssistSalesChannel), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<AssistSalesChannelDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(AssistSalesChannel), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(AssistSalesChannel), filterDto, sortColl);

                var data = _assistSalesChannelMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<AssistSalesChannel>().ToList();
                    var listData = list.Select(item => _mapper.Map<AssistSalesChannelDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(AssistSalesChannel), filterDto);
                }

                result.success = true;

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
        /// Delete AssistSalesChannel by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<AssistSalesChannelDto> Delete(int id)
        {
            var result = new ResponseBase<AssistSalesChannelDto>();

            try
            {
                var assistsaleschannel = (AssistSalesChannel)_assistSalesChannelMapper.Retrieve(id);
                if (assistsaleschannel != null)
                {
                    assistsaleschannel.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _assistSalesChannelMapper.Update(assistsaleschannel, DNetUserName);
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
        /// Create a new AssistSalesChannel
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<AssistSalesChannelDto> Create(AssistSalesChannelParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update AssistSalesChannel
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<AssistSalesChannelDto> Update(AssistSalesChannelParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}

