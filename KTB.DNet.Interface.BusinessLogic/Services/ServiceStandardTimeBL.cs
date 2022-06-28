
#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : ServiceStandardTime business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/02/2021 19:21:36
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using KTB.DNet.Interface.Framework;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class ServiceStandardTimeBL : AbstractBusinessLogic, IServiceStandardTimeBL
    {
        #region Variables
        private IVWI_ServiceStandardTimeRepository<VWI_ServiceStandardTime, int> _serviceStandardTimeRepo;
        #endregion

        #region Constructor
        public ServiceStandardTimeBL(IVWI_ServiceStandardTimeRepository<VWI_ServiceStandardTime, int> serviceStandardTime)
        {
            _serviceStandardTimeRepo = serviceStandardTime;
        }
        #endregion

        #region Public Methods
        public ResponseBase<ServiceStandardTimeDto> Create(ServiceStandardTimeParameterDto objCreate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<ServiceStandardTimeDto> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<List<ServiceStandardTimeDto>> Read(ServiceStandardTimeFilterDto filterDto, int pageSize)
        {
            var result = new ResponseBase<List<ServiceStandardTimeDto>>();
            var sortColl = string.Empty;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;
            int totalRow = 0;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                var criterias = Helper.InitialStrCriteria(typeof(VWI_ServiceStandardTime), filterDto);
                //criterias = Helper.UpdateStrCriteria(typeof(ServiceTemplateHeader), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "msdyn_companycode", dealerCompanyCode);

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_ServiceStandardTime), filterDto);

                List<VWI_ServiceStandardTime> data = _serviceStandardTimeRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_ServiceStandardTime, ServiceStandardTimeDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_ServiceStandardTime), filterDto);
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
        public ResponseBase<ServiceStandardTimeDto> Update(ServiceStandardTimeParameterDto objUpdate)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}