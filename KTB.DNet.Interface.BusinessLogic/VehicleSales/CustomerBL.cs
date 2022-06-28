#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Customer business logic class
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
    public class CustomerBL : AbstractBusinessLogic, ICustomerBL
    {
        #region Variables
        private readonly IMapper _customerMapper;
        private readonly IMapper _endCustomerMapper;
        private readonly IMapper _customerDealerMapper;
        private readonly IMapper _customerProfileMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public CustomerBL()
        {
            _customerMapper = MapperFactory.GetInstance().GetMapper(typeof(Customer).ToString());
            _endCustomerMapper = MapperFactory.GetInstance().GetMapper(typeof(VechileType).ToString());
            _customerDealerMapper = MapperFactory.GetInstance().GetMapper(typeof(Dealer).ToString());
            _customerProfileMapper = MapperFactory.GetInstance().GetMapper(typeof(Dealer).ToString());

            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get Customer by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<CustomerDto>> Read(CustomerFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(Customer), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<CustomerDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(Customer), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(Customer), filterDto, sortColl);

                // get data
                var data = _customerMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<Customer>().ToList();
                    var listData = list.Select(item => _mapper.Map<CustomerDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(Customer), filterDto);
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
        /// Delete Customer by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<CustomerDto> Delete(int id)
        {
            var result = new ResponseBase<CustomerDto>();

            try
            {
                var customer = (Customer)_customerMapper.Retrieve(id);
                if (customer != null)
                {
                    customer.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _customerMapper.Update(customer, DNetUserName);
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
        /// Get customer by code
        /// </summary>
        /// <param name="codeCustomer"></param>
        /// <returns></returns>
        public ResponseBase<CustomerDto> GetCustomerByCode(string codeCustomer)
        {
            ResponseBase<CustomerDto> result = new ResponseBase<CustomerDto>();
            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(Customer), "Code", MatchType.Exact, codeCustomer));
            var customer = _customerMapper.RetrieveByCriteria(criterias);
            if (customer.Count > 0)
            {
                var cus = (Customer)customer[0];
                result.lst = _mapper.Map<CustomerDto>(cus);
                result._id = cus.ID;
            }
            else
            {
                ErrorMsgHelper.DataNotFound(result.messages, typeof(Customer), null, "Code", codeCustomer);
            }

            result.success = true;

            return result;
        }

        /// <summary>
        /// Create a new Customer
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<CustomerDto> Create(CustomerParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update Customer
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<CustomerDto> Update(CustomerParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}

