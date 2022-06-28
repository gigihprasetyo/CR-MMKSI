#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_WholeSalesPrice business logic class
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
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class VWI_WholeSalesPriceBL : AbstractBusinessLogic, IVWI_WholeSalesPriceBL
    {
        #region Variables
        private readonly IMapper _wholeSalesPriceMapper;
        #endregion

        #region Constructor
        public VWI_WholeSalesPriceBL()
        {
            _wholeSalesPriceMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_WholeSalesPrice).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get WholeSalesPrice by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_WholeSalesPriceDto>> Read(VWI_WholeSalesPriceFilterDto filterDto, int pageSize)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_WholeSalesPrice), "DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<VWI_WholeSalesPriceDto>>();
            var sortColl = new SortCollection();

            try
            {
                // define sql
                var sql = Helper.GenerateSQLFromCriteriasAndSort(typeof(VWI_WholeSalesPrice), filterDto, sortColl, criterias);

                // get data
                var data = _wholeSalesPriceMapper.RetrieveSP("SELECT * FROM VWI_WholeSalesPrice " + sql);
                if (data.Count > 0)
                {
                    // calculate the skip 
                    int skip = filterDto.pages < 1 ? 0 : (filterDto.pages - 1) * pageSize;

                    // filter out the data based on the paging                    
                    List<VWI_WholeSalesPrice> list = new List<VWI_WholeSalesPrice>();
                    if (sortColl != null && sortColl.Count > 0)
                        list = data.Cast<VWI_WholeSalesPrice>().Skip(skip).Take(pageSize).ToList();
                    else
                        list = data.Cast<VWI_WholeSalesPrice>().OrderBy(x => x.VehicleColorID).Skip(skip).Take(pageSize).ToList();

                    // convert to dto object
                    List<VWI_WholeSalesPriceDto> listData = list.ConvertList<VWI_WholeSalesPrice, VWI_WholeSalesPriceDto>();

                    result.lst = listData;
                    result.total = data.Count;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_WholeSalesPrice), filterDto);
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

