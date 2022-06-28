#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : TermOfPayment business logic class
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
    public class TermOfPaymentBL : AbstractBusinessLogic, ITermOfPaymentBL
    {
        #region Variables
        private readonly IMapper _termofpaymentMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public TermOfPaymentBL()
        {
            _termofpaymentMapper = MapperFactory.GetInstance().GetMapper(typeof(TermOfPayment).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get TermOfPayment by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<TermOfPaymentDto>> Read(TermOfPaymentFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(TermOfPayment), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<TermOfPaymentDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(TermOfPayment), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(TermOfPayment), filterDto, sortColl);

                // get data
                var data = _termofpaymentMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<TermOfPayment>().ToList();
                    var listData = list.Select(item => _mapper.Map<TermOfPaymentDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(TermOfPayment), filterDto);
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
        /// Delete TermOfPayment by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<TermOfPaymentDto> Delete(int id)
        {
            var result = new ResponseBase<TermOfPaymentDto>();

            try
            {
                var termofpayment = (TermOfPayment)_termofpaymentMapper.Retrieve(id);
                if (termofpayment != null)
                {
                    termofpayment.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _termofpaymentMapper.Update(termofpayment, DNetUserName);
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
        /// Create a new TermOfPayment
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<TermOfPaymentDto> Create(TermOfPaymentParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update TermOfPayment
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<TermOfPaymentDto> Update(TermOfPaymentParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}

