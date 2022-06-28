#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : BenefitMasterDetail business logic class
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
    public class BenefitMasterDetailBL : AbstractBusinessLogic, IBenefitMasterDetailBL
    {
        #region Variables
        private readonly IMapper _benefitmasterdetailMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public BenefitMasterDetailBL()
        {
            _benefitmasterdetailMapper = MapperFactory.GetInstance().GetMapper(typeof(BenefitMasterDetail).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get BenefitMasterDetail by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<BenefitMasterDetailDto>> Read(BenefitMasterDetailFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(BenefitMasterDetail), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<BenefitMasterDetailDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(BenefitMasterDetail), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(BenefitMasterDetail), filterDto, sortColl);

                // get data
                var data = _benefitmasterdetailMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<BenefitMasterDetail>().ToList();
                    var listData = list.Select(item => _mapper.Map<BenefitMasterDetailDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(BenefitMasterDetail), filterDto);
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
        /// Delete BenefitMasterDetail by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<BenefitMasterDetailDto> Delete(int id)
        {
            var result = new ResponseBase<BenefitMasterDetailDto>();

            try
            {
                var benefitmasterdetail = (BenefitMasterDetail)_benefitmasterdetailMapper.Retrieve(id);
                if (benefitmasterdetail != null)
                {
                    benefitmasterdetail.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _benefitmasterdetailMapper.Update(benefitmasterdetail, DNetUserName);
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
        /// Create a new BenefitMasterDetail
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<BenefitMasterDetailDto> Create(BenefitMasterDetailParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update BenefitMasterDetail
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<BenefitMasterDetailDto> Update(BenefitMasterDetailParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}

