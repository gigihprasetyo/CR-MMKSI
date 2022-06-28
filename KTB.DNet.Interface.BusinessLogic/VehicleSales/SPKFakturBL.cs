#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKFaktur business logic class
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
using KTB.DNet.Interface.Model.Parameters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class SPKFakturBL : AbstractBusinessLogic, ISPKFakturBL
    {
        #region Variable

        private readonly IMapper _spkFakturMapper;
        private readonly AutoMapper.IMapper _mapper;

        #endregion

        #region Constuctor

        public SPKFakturBL()
        {
            _spkFakturMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKFaktur).ToString());

            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }

        #endregion

        #region Public Method
        /// <summary>
        /// Get SPK Faktur
        /// </summary>
        /// <param name="spkId"></param>
        /// <returns></returns>
        public ResponseBase<List<SPKFakturDto>> GetSPKFakturBySpkHeaderID(int spkId)
        {
            var result = new ResponseBase<List<SPKFakturDto>>();

            ArrayList arrayListResult = new ArrayList();
            CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(SPKFaktur), "RowStatus", MatchType.Exact, short.Parse(DBRowStatus.Active.ToString())));
            criteria.opAnd(new Criteria(typeof(AppConfig), "SPKHeaderID", MatchType.Exact, spkId));
            arrayListResult = _spkFakturMapper.RetrieveByCriteria(criteria);

            var listResult = new List<SPKFakturDto>();
            if (arrayListResult.Count > 0)
            {
                foreach (var spkFaktur in arrayListResult)
                {
                    var spkFakturDto = _mapper.Map<SPKFakturDto>(spkFaktur);
                    result.lst.Add(spkFakturDto);
                }
            }

            result.success = true;

            return result;

        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<SPKFakturDto> Create(SPKFakturParameterDto objCreate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<SPKFakturDto> Update(SPKFakturParameterDto objUpdate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete SPKFaktur by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<SPKFakturDto> Delete(int id)
        {
            var result = new ResponseBase<SPKFakturDto>();

            try
            {
                var spkfaktur = (SPKFaktur)_spkFakturMapper.Retrieve(id);
                if (spkfaktur != null)
                {
                    spkfaktur.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _spkFakturMapper.Update(spkfaktur, DNetUserName);
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
        /// Get SPKFaktur by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<SPKFakturDto>> Read(SPKFakturFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(SPKFaktur), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<SPKFakturDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(SPKFaktur), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(SPKFaktur), filterDto, sortColl);

                // get data
                var data = _spkFakturMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<SPKFaktur>().ToList();
                    var listData = new List<SPKFakturDto>();
                    foreach (var item in list)
                    {
                        // map it
                        var spkfakturDto = _mapper.Map<SPKFakturDto>(item);

                        if (item.SPKHeader != null)
                        {
                            spkfakturDto.SPKHeader = _mapper.Map<SPKHeaderDto>(item.SPKHeader);
                        }
                        if (item.EndCustomer != null)
                        {
                            spkfakturDto.EndCustomer = _mapper.Map<EndCustomerDto>(item.EndCustomer);
                        }

                        // add to list
                        listData.Add(spkfakturDto);
                    }

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SPKFaktur), filterDto);
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
