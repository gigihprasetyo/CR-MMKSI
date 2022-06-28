#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ndentPartPO business logic class
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
    public class IndentPartPOBL : AbstractBusinessLogic, IIndentPartPOBL
    {
        #region Variables
        private readonly IMapper _indentPartPOMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public IndentPartPOBL()
        {
            _indentPartPOMapper = MapperFactory.GetInstance().GetMapper(typeof(IndentPartPO).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get spare part by id
        /// </summary>
        /// <param name="sparePartPODetailID"></param>
        /// <returns></returns>
        public ResponseBase<List<IndentPartPODto>> GetBySparePartPODetailID(int sparePartPODetailID)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(IndentPartPO), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(IndentPartPO), "SparePartPODetail.ID", MatchType.Exact, sparePartPODetailID));
            var result = new ResponseBase<List<IndentPartPODto>>();

            try
            {
                var data = _indentPartPOMapper.RetrieveByCriteria(criterias);
                if (data.Count > 0)
                {
                    var list = data.Cast<IndentPartPO>().ToList();
                    var listData = list.Select(item => _mapper.Map<IndentPartPODto>(item)).ToList();

                    result.lst = listData;
                    result.total = listData.Count;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(IndentPartPO), null, "SparePartPODetail.ID", sparePartPODetailID.ToString());
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
        /// Get IndentPartPO by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<IndentPartPODto>> Read(IndentPartPOFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(IndentPartPO), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<IndentPartPODto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(IndentPartPO), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(IndentPartPO), filterDto, sortColl);

                // get data
                var data = _indentPartPOMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<IndentPartPO>().ToList();
                    var listData = new List<IndentPartPODto>();
                    foreach (var item in list)
                    {
                        // map it
                        var indentpartpoDto = _mapper.Map<IndentPartPODto>(item);

                        //if (item.SparePartPODetail != null)
                        //{
                        //    indentpartpoDto.SparePartPODetail = _mapper.Map<SparePartPODetailDto>(item.SparePartPODetail);
                        //}
                        //if (item.IndentPartDetail != null)
                        //{
                        //    indentpartpoDto.IndentPartDetail = _mapper.Map<IndentPartDetailDto>(item.IndentPartDetail);
                        //}

                        // add to list
                        listData.Add(indentpartpoDto);
                    };

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(IndentPartPO), filterDto);
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
        /// Delete IndentPartPO by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<IndentPartPODto> Delete(int id)
        {
            var result = new ResponseBase<IndentPartPODto>();

            try
            {
                var indentpartpo = (IndentPartPO)_indentPartPOMapper.Retrieve(id);
                if (indentpartpo != null)
                {
                    indentpartpo.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _indentPartPOMapper.Update(indentpartpo, DNetUserName);
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
        /// Create a new IndentPartPO
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<IndentPartPODto> Create(IndentPartPOParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update IndentPartPO
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<IndentPartPODto> Update(IndentPartPOParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}

