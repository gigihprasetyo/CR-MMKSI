#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VWI_JobPositionParts business logic class
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
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class VWI_JobPositionPartsBL : AbstractBusinessLogic, IVWI_JobPositionPartsBL
    {
        #region Variable
        private readonly IMapper _jobPositionPartsMapper;
        private readonly IMapper _dealerMapper;
        private readonly IMapper _salesmanCategoryLevelMapper;
        private readonly IMapper _dealerAdditionalMapper;
        private readonly IMapper _v_SparePartOrganizationMapper;
        #endregion

        #region Constructor
        public VWI_JobPositionPartsBL()
        {
            _jobPositionPartsMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_JobPositionParts).ToString());
            _dealerMapper = MapperFactory.GetInstance().GetMapper(typeof(Dealer).ToString());
            _dealerAdditionalMapper = MapperFactory.GetInstance().GetMapper(typeof(DealerAdditional).ToString());
            _v_SparePartOrganizationMapper = MapperFactory.GetInstance().GetMapper(typeof(V_SparePartOrganization).ToString());
            _salesmanCategoryLevelMapper = MapperFactory.GetInstance().GetMapper(typeof(SalesmanCategoryLevel).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Read fleet data
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_JobPositionPartsDto>> Read(FilterDtoBase filterDto, int pageSize)
        {
            var result = new ResponseBase<List<VWI_JobPositionPartsDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                SalesmanHeaderBL salesmanHeaderBL = new SalesmanHeaderBL();
                var dealer = _dealerMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(Dealer), "DealerCode", DealerCode));
                Dealer dealerData = dealer[0] as Dealer;

                List<V_SparePartOrganization> masterOrganizationList = this.GetSparepartOrganizationList(dealerData.ID);
                List<SalesmanCategoryLevel> listSalesmanCategoryLevel = (_salesmanCategoryLevelMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(SalesmanCategoryLevel)))).Cast<SalesmanCategoryLevel>().ToList();

                if (masterOrganizationList != null && masterOrganizationList.Count > 0 && listSalesmanCategoryLevel != null && listSalesmanCategoryLevel.Count > 0)
                {
                    List<VWI_JobPositionParts> listVWI_JobPositionParts = new List<VWI_JobPositionParts>();

                    SalesmanCategoryLevel salesCategory = new SalesmanCategoryLevel();
                    SalesmanCategoryLevel salesParentCategory = new SalesmanCategoryLevel();

                    for (int i = 0; i < masterOrganizationList.Count; i++)
                    {
                        salesCategory = listSalesmanCategoryLevel.Where(e => e.ID == masterOrganizationList[i].SalesmanCategoryLevelID).FirstOrDefault();
                        salesParentCategory = listSalesmanCategoryLevel.Where(e => e.ID == masterOrganizationList[i].ParentID).FirstOrDefault();
                        listVWI_JobPositionParts.Add(new VWI_JobPositionParts()
                        {
                            ID = salesCategory.ID,
                            Code = salesCategory.Kode,
                            PositionName = salesCategory.PositionName,
                            ParentID = salesParentCategory.ID,
                            ParentCode = salesParentCategory.Kode,
                            ParentPositionName = salesParentCategory.PositionName
                        });
                    }

                    if (listVWI_JobPositionParts.Count > 0)
                    {
                        List<VWI_JobPositionPartsDto> listData = listVWI_JobPositionParts.ConvertList<VWI_JobPositionParts, VWI_JobPositionPartsDto>();
                        totalRow = listData.Count();
                        listData.OrderBy(x => x.ID).ToList();
                        result.lst = listData;
                        result.total = totalRow;
                    }
                    else
                    {
                        ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_JobPositionParts), filterDto);
                    }
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_JobPositionParts), filterDto);
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


        public List<V_SparePartOrganization> GetSparepartOrganizationList(int dealerId)
        {
            // get dealer additional
            DealerAdditional dealerAdditional = new DealerAdditional();
            CriteriaComposite dealerAdditionalCriterias = new CriteriaComposite(new Criteria(typeof(DealerAdditional), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            dealerAdditionalCriterias.opAnd(new Criteria(typeof(DealerAdditional), "Dealer.ID", MatchType.Exact, dealerId));
            var dealerAddData = _dealerAdditionalMapper.RetrieveByCriteria(dealerAdditionalCriterias);
            if (dealerAddData.Count > 0)
            {
                var list = dealerAddData.Cast<DealerAdditional>().ToList();
                dealerAdditional = list.FirstOrDefault();
            }

            // get organization list
            List<V_SparePartOrganization> result = new List<V_SparePartOrganization>();
            string grade = string.IsNullOrEmpty(dealerAdditional.SparePartGrade) ? "A" : dealerAdditional.SparePartGrade;
            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(V_SparePartOrganization), "Grade", MatchType.Exact, grade));
            criterias.opAnd(new Criteria(typeof(V_SparePartOrganization), "LevelNumber", MatchType.InSet, "(1,2)"));
            var data = _v_SparePartOrganizationMapper.RetrieveByCriteria(criterias);
            if (data.Count > 0)
            {
                result = data.Cast<V_SparePartOrganization>().ToList();
            }

            return result;
        }
        #endregion
    }
}