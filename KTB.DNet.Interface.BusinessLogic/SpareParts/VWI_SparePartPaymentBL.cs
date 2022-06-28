#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_SparePartPayment business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 3/10/2018 12:21
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class VWI_SparePartPaymentBL : AbstractBusinessLogic, IVWI_SparePartPaymentBL
    {
        #region Variables
        private readonly IMapper _sparepartPaymentMapper;
        #endregion

        #region Constructor
        public VWI_SparePartPaymentBL()
        {
            _sparepartPaymentMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_SparePartPayment).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_SparePartPayment by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_SparePartPaymentDto>> Read(VWI_SparePartPaymentFilterDto filterDto, int pageSize)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_SparePartPayment), "DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<VWI_SparePartPaymentDto>>();
            var sortColl = new SortCollection();

            try
            {
                // define sql
                var sql = Helper.GenerateSQLFromCriteriasAndSort(typeof(VWI_SparePartPayment), filterDto, sortColl, criterias);

                // get data
                var data = _sparepartPaymentMapper.RetrieveSP("SELECT * FROM VWI_SparePartPayment " + sql);
                if (data.Count > 0)
                {
                    // calculate the skip 
                    int skip = filterDto.pages < 1 ? 0 : (filterDto.pages - 1) * pageSize;

                    // filter out the data based on the paging                    
                    List<VWI_SparePartPayment> list = new List<VWI_SparePartPayment>();
                    if (sortColl != null && sortColl.Count > 0)
                        list = data.Cast<VWI_SparePartPayment>().Skip(skip).Take(pageSize).ToList();
                    else
                        list = data.Cast<VWI_SparePartPayment>().OrderBy(x => x.InvoiceNo).Skip(skip).Take(pageSize).ToList();

                    // convert to dto object
                    List<VWI_SparePartPaymentDto> listData = list.ConvertList<VWI_SparePartPayment, VWI_SparePartPaymentDto>();

                    result.lst = listData;
                    result.total = data.Count;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_SparePartPayment), filterDto);
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

