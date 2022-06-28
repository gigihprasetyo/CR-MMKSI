
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
    public class DSFCeilingBL : AbstractBusinessLogic, IDSFCeilingBL
    {
        #region Variables
        private readonly IMapper _ceilingMaster;
        #endregion

        #region Constructor
        public DSFCeilingBL()
        {
            _ceilingMaster = MapperFactory.GetInstance().GetMapper(typeof(VWI_FinishUnitCeiling).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_FinishUnitCeiling by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<DSFCeilingDto>> Read(DSFCeilingFilterDto filterDto, int pageSize)
        {
            
            var result = new ResponseBase<List<DSFCeilingDto>>();
            var sortColl = new SortCollection();

            try
            {
                // populate the criterias
                var criterias = Helper.BuildCriteria(typeof(VWI_FinishUnitCeiling), filterDto);

                // define sql
                var sql = Helper.GenerateSQLFromCriteriasAndSort(typeof(VWI_FinishUnitCeiling), filterDto, sortColl, criterias);

                // get data
                var data = _ceilingMaster.RetrieveSP("SELECT * FROM VWI_FinishUnitCeiling " + sql);
                if (data.Count > 0)
                {
                    // calculate the skip 
                    int skip = filterDto.pages < 1 ? 0 : (filterDto.pages - 1) * pageSize;

                    // filter out the data based on the paging                    
                    List<VWI_FinishUnitCeiling> list = new List<VWI_FinishUnitCeiling>();
                    if (sortColl != null && sortColl.Count > 0)
                        list = data.Cast<VWI_FinishUnitCeiling>().Skip(skip).Take(pageSize).ToList();
                    else
                        list = data.Cast<VWI_FinishUnitCeiling>().OrderBy(x => x.CreditAccount).Skip(skip).Take(pageSize).ToList();

                    // convert to dto object
                    List<DSFCeilingDto> listData = list.ConvertList<VWI_FinishUnitCeiling, DSFCeilingDto>();

                    result.lst = listData;
                    result.total = data.Count;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_FinishUnitCeiling), filterDto);
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
