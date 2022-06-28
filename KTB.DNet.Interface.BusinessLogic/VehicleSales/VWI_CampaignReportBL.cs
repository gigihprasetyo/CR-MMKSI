#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_CampaignReport business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region "Namespace Imports"
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
    /// <summary>
    /// Campaign report BL
    /// </summary>
    public class VWI_CampaignReportBL : AbstractBusinessLogic, IVWI_CampaignReportBL
    {
        private readonly IMapper _campaignreportMapper;
        private readonly AutoMapper.IMapper _mapper;

        public VWI_CampaignReportBL()
        {
            _campaignreportMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_CampaignReport).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }

        #region Public Methods
        /// <summary>
        /// Get VWI_CampaignReport 
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CampaignReportDto>> Read(VWI_CampaignReportFilterDto filterDto, int pageSize)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_CampaignReport), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(VWI_CampaignReport), "DealerCode", MatchType.Exact, DealerCode));

            var result = new ResponseBase<List<VWI_CampaignReportDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(VWI_CampaignReport), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_CampaignReport), filterDto, sortColl);

                // get data
                var data = _campaignreportMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VWI_CampaignReport>().ToList();
                    var listData = list.Select(item => _mapper.Map<VWI_CampaignReportDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CampaignReport), filterDto);
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
