#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_Campaign business logic class
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
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using System.Threading.Tasks;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class VWI_CampaignBL : AbstractBusinessLogic, IVWI_CampaignBL
    {
        #region Variables
        private readonly IVWI_CampaignRepository<VWI_Campaign, int> _vWICampaignRepository;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public VWI_CampaignBL(IVWI_CampaignRepository<VWI_Campaign, int> VWICampaignRepository)
        {
            _vWICampaignRepository = VWICampaignRepository;
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
        }
        #endregion

        #region Public Methods
        public ResponseBase<List<VWI_CampaignDto>> Read(VWI_CampaignFilterDto filterDto, int pageSize)
        {
            var result = new ResponseBase<List<VWI_CampaignDto>>();
            var sortColl = new SortCollection();
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                // get criteria
                var criterias = Helper.BuildCriteria(typeof(VWI_Campaign), filterDto);

                sortColl = Helper.UpdateSortColumn(typeof(VWI_Campaign), filterDto, sortColl);

                List<VWI_Campaign> data = _vWICampaignRepository.Search(criterias, DealerCode, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_Campaign, VWI_CampaignDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_Campaign), filterDto);
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
