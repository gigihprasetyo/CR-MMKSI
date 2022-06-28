#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_OpenFakturForPDI business logic class
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
    public class VWI_OpenFakturForPDIBL : AbstractBusinessLogic, IVWI_OpenFakturForPDIBL
    {
        #region Variables
        private readonly IMapper _vehicleSalesOpenFakturForPDIMapper;
        #endregion

        #region Constructor
        public VWI_OpenFakturForPDIBL()
        {
            _vehicleSalesOpenFakturForPDIMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_OpenFakturForPDI).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_OpenFakturForPDI by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_OpenFakturForPDIDto>> Read(VWI_OpenFakturForPDIFilterDto filterDto, int pageSize)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_OpenFakturForPDI), "ChassisNumber", MatchType.Exact, filterDto.ChassisNumber));
            var result = new ResponseBase<List<VWI_OpenFakturForPDIDto>>();
            var sortColl = new SortCollection();

            try
            {
                // define sql
                var sql = Helper.GenerateSQLFromCriteriasAndSort(typeof(VWI_OpenFakturForPDI), filterDto, sortColl, criterias);

                // get data
                var data = _vehicleSalesOpenFakturForPDIMapper.RetrieveSP("SELECT * FROM VWI_OpenFakturForPDI " + sql);
                if (data.Count > 0)
                {
                    // calculate the skip 
                    int skip = filterDto.pages < 1 ? 0 : (filterDto.pages - 1) * pageSize;

                    // filter out the data based on the paging                    
                    List<VWI_OpenFakturForPDI> list = new List<VWI_OpenFakturForPDI>();
                    if (sortColl != null && sortColl.Count > 0)
                        list = data.Cast<VWI_OpenFakturForPDI>().Skip(skip).Take(pageSize).ToList();
                    else
                        list = data.Cast<VWI_OpenFakturForPDI>().OrderBy(x => x.ID).Skip(skip).Take(pageSize).ToList();

                    // convert to dto object
                    List<VWI_OpenFakturForPDIDto> listData = list.ConvertList<VWI_OpenFakturForPDI, VWI_OpenFakturForPDIDto>();

                    result.lst = listData;
                    result.total = data.Count;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_OpenFakturForPDI), filterDto);
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

        public ResponseBase<List<VWI_OpenFakturForPDIDto>> Read(FilterDtoBase filterDto, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
