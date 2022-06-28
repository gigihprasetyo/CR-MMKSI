
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Web.Configuration;


namespace KTB.DNet.Interface.BusinessLogic
{
    public class DSFPaymentBL : AbstractBusinessLogic, IDSFPaymentBL
    {
        #region Variables
        private readonly IMapper _sparepartPaymentMapper;
        #endregion

        #region Constructor
        public DSFPaymentBL()
        {
            _sparepartPaymentMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_FinishUnitPayment).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_FinishUnitPayment by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<DSFPaymentDto>> Read(DSFPaymentFilterDto filterDto, int pageSize)
        {
            //var criterias = new CriteriaComposite(new Criteria(typeof(VWI_SparePartPayment), "DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<DSFPaymentDto>>();
            var sortColl = new SortCollection();

            try
            {
                // populate the criterias
                var criterias = Helper.BuildCriteria(typeof(VWI_FinishUnitPayment), filterDto);

                // define sql
                var sql = Helper.GenerateSQLFromCriteriasAndSort(typeof(VWI_FinishUnitPayment), filterDto, sortColl, criterias);

                // get data
                var data = _sparepartPaymentMapper.RetrieveSP("SELECT * FROM VWI_FinishUnitPayment " + sql);
                if (data.Count > 0)
                {
                    // calculate the skip 
                    int skip = filterDto.pages < 1 ? 0 : (filterDto.pages - 1) * pageSize;

                    // filter out the data based on the paging                    
                    List<VWI_FinishUnitPayment> list = new List<VWI_FinishUnitPayment>();
                    if (sortColl != null && sortColl.Count > 0)
                        list = data.Cast<VWI_FinishUnitPayment>().Skip(skip).Take(pageSize).ToList();
                    else
                        list = data.Cast<VWI_FinishUnitPayment>().OrderBy(x => x.SalesOrderNumber).Skip(skip).Take(pageSize).ToList();

                    // convert to dto object
                    List<DSFPaymentDto> listData = list.ConvertList<VWI_FinishUnitPayment, DSFPaymentDto>();

                    result.lst = listData;
                    result.total = data.Count;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_FinishUnitPayment), filterDto);
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
