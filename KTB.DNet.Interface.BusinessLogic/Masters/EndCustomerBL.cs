#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : EndCustomer business logic class
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
    public class EndCustomerBL : AbstractBusinessLogic, IEndCustomerBL
    {
        #region Variables
        private readonly IMapper _endcustomerMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public EndCustomerBL()
        {
            _endcustomerMapper = MapperFactory.GetInstance().GetMapper(typeof(EndCustomer).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get EndCustomer by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<EndCustomerDto>> Read(EndCustomerFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(EndCustomer), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(EndCustomer), "SPKFaktur.SPKHeader.Dealer.DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<EndCustomerDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(EndCustomer), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(EndCustomer), filterDto, sortColl);

                // get data
                var data = _endcustomerMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<EndCustomer>().ToList();
                    var listData = list.Select(item => _mapper.Map<EndCustomerDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(EndCustomer), filterDto);
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
        /// Delete EndCustomer by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<EndCustomerDto> Delete(int id)
        {
            var result = new ResponseBase<EndCustomerDto>();

            try
            {
                var endcustomer = (EndCustomer)_endcustomerMapper.Retrieve(id);
                if (endcustomer != null)
                {
                    endcustomer.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _endcustomerMapper.Update(endcustomer, DNetUserName);
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
        /// Create a new EndCustomer
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<EndCustomerDto> Create(EndCustomerParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update EndCustomer
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<EndCustomerDto> Update(EndCustomerParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}

