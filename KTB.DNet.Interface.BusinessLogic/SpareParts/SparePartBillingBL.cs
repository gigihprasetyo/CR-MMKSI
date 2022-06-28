#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartBilling business logic class
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
    public class SparePartBillingBL : AbstractBusinessLogic, ISparePartBillingBL
    {
        #region Variables
        private readonly IMapper _sparePartBillingMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public SparePartBillingBL()
        {
            _sparePartBillingMapper = MapperFactory.GetInstance().GetMapper(typeof(SparePartBilling).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        public SparePartBillingBL(AutoMapper.IMapper mapper)
        {
            _sparePartBillingMapper = MapperFactory.GetInstance().GetMapper(typeof(SparePartBilling).ToString());
            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get billing number
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public ResponseBase<SparePartBillingDto> GetByBillingNumber(string no)
        {
            var result = new ResponseBase<SparePartBillingDto>();
            var criterias = new CriteriaComposite(new Criteria(typeof(SparePartBilling), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(SparePartBilling), "BillingNumber", MatchType.Exact, no));
            var arrayListResult = _sparePartBillingMapper.RetrieveByCriteria(criterias);
            if (arrayListResult.Count > 0)
            {
                foreach (var item in arrayListResult)
                {
                    var SparePartBillingDto = _mapper.Map<SparePartBillingDto>(item);
                    result.lst = SparePartBillingDto;
                    // return output ID
                    result._id = SparePartBillingDto.ID;
                    result.total = 1;
                    result.success = true;
                }
            }
            else
            {
                ErrorMsgHelper.DataNotFound(result.messages, typeof(SparePartBilling), null, "BillingNumber", no);
            }


            return result;
        }

        /// <summary>
        /// Get SparePartBilling by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<SparePartBillingDto>> Read(SparePartBillingFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(SparePartBilling), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<SparePartBillingDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(SparePartBilling), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(SparePartBilling), filterDto, sortColl);

                // get data
                var data = _sparePartBillingMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<SparePartBilling>().ToList();
                    var listData = list.Select(item => _mapper.Map<SparePartBillingDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SparePartBilling), filterDto);
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
        /// Delete SparePartBilling by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<SparePartBillingDto> Delete(int id)
        {
            var result = new ResponseBase<SparePartBillingDto>();

            try
            {
                var sparepartbilling = (SparePartBilling)_sparePartBillingMapper.Retrieve(id);
                if (sparepartbilling != null)
                {
                    sparepartbilling.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _sparePartBillingMapper.Update(sparepartbilling, DNetUserName);
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
        /// Create a new SparePartBilling
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<SparePartBillingDto> Create(SparePartBillingParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update SparePartBilling
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<SparePartBillingDto> Update(SparePartBillingParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}

