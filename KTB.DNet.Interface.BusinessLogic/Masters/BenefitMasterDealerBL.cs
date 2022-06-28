#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : BenefitMasterDealer business logic class
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
    public class BenefitMasterDealerBL : AbstractBusinessLogic, IBenefitMasterDealerBL
    {
        #region Variables
        private readonly IMapper _benefitmasterdealerMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public BenefitMasterDealerBL()
        {
            _benefitmasterdealerMapper = MapperFactory.GetInstance().GetMapper(typeof(BenefitMasterDealer).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get BenefitMasterDealer by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<BenefitMasterDealerDto>> Read(BenefitMasterDealerFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(BenefitMasterDealer), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<BenefitMasterDealerDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(BenefitMasterDealer), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(BenefitMasterDealer), filterDto, sortColl);

                // get data
                var data = _benefitmasterdealerMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<BenefitMasterDealer>().ToList();
                    var listData = list.Select(item => _mapper.Map<BenefitMasterDealerDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(BenefitMasterDealer), filterDto);
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
        /// Delete BenefitMasterDealer by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<BenefitMasterDealerDto> Delete(int id)
        {
            var result = new ResponseBase<BenefitMasterDealerDto>();

            try
            {
                var benefitmasterdealer = (BenefitMasterDealer)_benefitmasterdealerMapper.Retrieve(id);
                if (benefitmasterdealer != null)
                {
                    benefitmasterdealer.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _benefitmasterdealerMapper.Update(benefitmasterdealer, DNetUserName);
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
        /// Create a new BenefitMasterDealer
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<BenefitMasterDealerDto> Create(BenefitMasterDealerParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update BenefitMasterDealer
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<BenefitMasterDealerDto> Update(BenefitMasterDealerParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}

