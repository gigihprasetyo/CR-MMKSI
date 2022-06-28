#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PaymentMethod business logic class
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
    public class PaymentMethodBL : AbstractBusinessLogic, IPaymentMethodBL
    {
        #region Variables
        private readonly IMapper _paymentmethodMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public PaymentMethodBL()
        {
            _paymentmethodMapper = MapperFactory.GetInstance().GetMapper(typeof(PaymentMethod).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get PaymentMethod by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<PaymentMethodDto>> Read(PaymentMethodFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(PaymentMethod), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<PaymentMethodDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(PaymentMethod), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(PaymentMethod), filterDto, sortColl);

                // get data
                var data = _paymentmethodMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<PaymentMethod>().ToList();
                    var listData = list.Select(item => _mapper.Map<PaymentMethodDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(PaymentMethod), filterDto);
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
        /// Delete PaymentMethod by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<PaymentMethodDto> Delete(int id)
        {
            var result = new ResponseBase<PaymentMethodDto>();

            try
            {
                var paymentmethod = (PaymentMethod)_paymentmethodMapper.Retrieve(id);
                if (paymentmethod != null)
                {
                    paymentmethod.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _paymentmethodMapper.Update(paymentmethod, DNetUserName);
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
        /// Create a new PaymentMethod
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<PaymentMethodDto> Create(PaymentMethodParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update PaymentMethod
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<PaymentMethodDto> Update(PaymentMethodParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}

