#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : SPPenalty business logic class
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
    public class SparePartPenaltyBL : AbstractBusinessLogic, ISparePartPenaltyBL
    {
        #region Variables
        private ISparePartPenaltyHeaderRepository<SparePartPenaltyHeader, int> _sppenaltyHeaderRepo;
        private ISparePartPenaltyDetailRepository<SparePartPenaltyDetail, int> _sppenaltyDetailRepo;
        #endregion

        #region Constructor
        public SparePartPenaltyBL(ISparePartPenaltyHeaderRepository<SparePartPenaltyHeader, int> sppenaltyHeaderRepo, ISparePartPenaltyDetailRepository<SparePartPenaltyDetail, int> sppenaltyDetailRepo)
        {
            _sppenaltyHeaderRepo = sppenaltyHeaderRepo;
            _sppenaltyDetailRepo = sppenaltyDetailRepo;
        }

        public ResponseBase<SparePartPenaltyHeaderDto> Create(SparePartPenaltyHeaderParameterDto objCreate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<SparePartPenaltyHeaderDto> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<List<SparePartPenaltyHeaderDto>> Read(SparePartPenaltyHeaderFilterDto filterDto, int pageSize)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Public Methods
        public ResponseBase<List<SparePartPenaltyHeaderDto>> ReadList(SparePartPenaltyHeaderFilterDto filterDto, int pageSize)
        {
            var result = new ResponseBase<List<SparePartPenaltyHeaderDto>>();
            var sortColl = string.Empty;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;
            int totalRow = 0;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                var criterias = Helper.InitialStrCriteria(typeof(SparePartPenaltyHeader), filterDto);
                criterias = Helper.UpdateStrCriteria(typeof(SparePartPenaltyHeader), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", dc,false,criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(SparePartPenaltyHeader), filterDto);

                List<SparePartPenaltyHeader> data = _sppenaltyHeaderRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    // get Spare Part Penalty Detail
                    foreach (var item in data)
                    {
                        List<SparePartPenaltyDetail> details = _sppenaltyDetailRepo.SearchCustom("WHERE SparePartPenaltyDetail.TOPSPPenaltyID = '" + item.ID.ToString() + "'");

                        if (details != null && details.Count > 0)
                        {
                            item.sparepartpenaltydetail = new List<SparePartPenaltyDetail>();
                            item.sparepartpenaltydetail.AddRange(details);
                        }

                    }

                    result.lst = data.ConvertList<SparePartPenaltyHeader, SparePartPenaltyHeaderDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SparePartPenaltyHeader), filterDto);
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

        public ResponseBase<SparePartPenaltyHeaderDto> Update(SparePartPenaltyHeaderParameterDto objUpdate)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}