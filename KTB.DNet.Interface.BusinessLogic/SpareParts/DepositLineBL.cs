#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : DepositLine business logic class
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
    public class DepositLineBL : AbstractBusinessLogic, IDepositLineBL
    {
        #region Variables
        private readonly IMapper _depositlineMapper;
        private readonly IMapper _sparePartBillingMapper;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public DepositLineBL()
        {
            _depositlineMapper = MapperFactory.GetInstance().GetMapper(typeof(DepositLine).ToString());
            _sparePartBillingMapper = MapperFactory.GetInstance().GetMapper(typeof(SparePartBilling).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get DepositLine by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<DepositLineDto>> Read(DepositLineFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(DepositLine), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(DepositLine), "InvoiceNo", MatchType.IsNotNull, null));
            criterias.opAnd(new Criteria(typeof(DepositLine), "InvoiceNo", MatchType.No, ""));
            criterias.opAnd(new Criteria(typeof(DepositLine), "Deposit.Dealer.DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<DepositLineDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            ISparePartBillingBL sparePartBillingBL = new SparePartBillingBL(_mapper);

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(DepositLine), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(DepositLine), filterDto, sortColl);

                // get data
                var data = _depositlineMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<DepositLine>().ToList();
                    var listData = new List<DepositLineDto>();

                    foreach (var item in list)
                    {
                        var depositlineDto = _mapper.Map<DepositLineDto>(item);

                        var sparePartBilling = sparePartBillingBL.GetByBillingNumber(item.InvoiceNo);

                        if (sparePartBilling.success)
                        {
                            var sparePartBillingDomain = (SparePartBilling)_sparePartBillingMapper.Retrieve(sparePartBilling._id);
                            if (sparePartBillingDomain != null && sparePartBillingDomain.SparePartBillingDetails.Count > 0)
                            {
                                var firstSPB = (SparePartBillingDetail)sparePartBillingDomain.SparePartBillingDetails[0];
                                if (firstSPB.SparePartDODetail != null)
                                {
                                    var spPOEstimate = firstSPB.SparePartDODetail.SparePartPOEstimate;
                                    depositlineDto.OrderNo = spPOEstimate.SONumber;
                                }
                            }
                        }
                        depositlineDto.DealerCode = item.Deposit.Dealer.DealerCode;

                        listData.Add(depositlineDto);
                    }

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(DepositLine), filterDto);
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
        /// Delete DepositLine by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<DepositLineDto> Delete(int id)
        {
            var result = new ResponseBase<DepositLineDto>();

            try
            {
                var depositline = (DepositLine)_depositlineMapper.Retrieve(id);
                if (depositline != null)
                {
                    depositline.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _depositlineMapper.Update(depositline, DNetUserName);
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
        /// Create a new DepositLine
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<DepositLineDto> Create(DepositLineParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update DepositLine
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<DepositLineDto> Update(DepositLineParameterDto objUpdate)
        {
            return null;
        }
        #endregion
    }
}