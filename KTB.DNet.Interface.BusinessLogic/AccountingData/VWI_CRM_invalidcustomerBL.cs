#region Summary
// ===========================================================================
// AUTHOR        : BSI DMS Code Generator
// PURPOSE       : VWI_CRM_invalidcustomer business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/18/2019 10:09:30 AM
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Interface;
using x = KTB.DNet.Domain;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class VWI_CRM_invalidcustomerBL : AbstractBusinessLogic, IVWI_CRM_invalidcustomerBL
    {
        #region Variables
        private IVWI_CRM_invalidcustomerRepository<VWI_CRM_invalidcustomer, int> _vWI_CRM_invalidcustomerRepo;
        #endregion

        #region Constructor
        public VWI_CRM_invalidcustomerBL(IVWI_CRM_invalidcustomerRepository<VWI_CRM_invalidcustomer, int> vWI_CRM_invalidcustomerRepo)
        {
            _vWI_CRM_invalidcustomerRepo = vWI_CRM_invalidcustomerRepo;
        }
        #endregion
        public ResponseBase<VWI_CRM_invalidcustomerDto> Create(VWI_CRM_invalidcustomerDtoParameter objCreate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<VWI_CRM_invalidcustomerDto> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<List<VWI_CRM_invalidcustomerDto>> Read(VWI_CRM_invalidcustomerFilterDto filterDto, int pageSize)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<List<VWI_CRM_invalidcustomerDto>> ReadList(VWI_CRM_invalidcustomerFilterDto filterDto, int pageSize)
        {
            var result = new ResponseBase<List<VWI_CRM_invalidcustomerDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;
            List<DNetValidationResult> validationResults = new List<DNetValidationResult>();

            try
            {
                VWI_CRM_invalidcustomerFilterDto InnerfilterDto = new VWI_CRM_invalidcustomerFilterDto();
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                if (filterDto.find != null)
                {
                    for (int i = 0; i < filterDto.find.Count; i++)
                    {
                        MatchTypeFilter item = filterDto.find[i];
                        if (item.PropertyName == "NGData")
                        {
                            List<MatchTypeFilter> listItem = new List<MatchTypeFilter>();
                            if (item.PropertyValue == "true")
                            {
                                item.PropertyValue = "1";
                            }
                            else if (item.PropertyValue == "false")
                            {
                                item.PropertyValue = "0";
                            }
                            listItem.Add(item);
                            InnerfilterDto.find = listItem;
                            filterDto.find.RemoveAt(i);
                        }
                    }
                }
                

                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_invalidcustomer), filterDto);
                innerQueryCriteria = Helper.InitialStrCriteria(typeof(VWI_CRM_invalidcustomer), InnerfilterDto);

                criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_invalidcustomer), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                // filter by company
                if (DealerCode.ToUpper() == "MKS")
                {
                    string CompanyCode = UserName.Trim().Split('@')[0].Substring(0, 4);
                    criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_invalidcustomer), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.InSet.GetHashCode(), "msdyn_companycode", CompanyCode, false, criterias);
                }
                else
                {
                    string DealerName = ValidateLoginSunAndBosowa(validationResults);
                    if (DealerName.Contains("SUN"))
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_invalidcustomer), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.InSet.GetHashCode(), "msdyn_companycode", "SSMT", false, criterias);
                    else if (DealerName.Contains("BOSOWA"))
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_invalidcustomer), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.InSet.GetHashCode(), "msdyn_companycode", "BSWB", false, criterias);
                    else
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_invalidcustomer), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.NotInSet.GetHashCode(), "msdyn_companycode", "SSMT', 'BSWB", false, criterias);
                }

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_invalidcustomer), filterDto);

                List<VWI_CRM_invalidcustomer> data = _vWI_CRM_invalidcustomerRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_CRM_invalidcustomer, VWI_CRM_invalidcustomerDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_invalidcustomer), filterDto);
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

        public ResponseBase<VWI_CRM_invalidcustomerDto> Update(VWI_CRM_invalidcustomerDtoParameter objUpdate)
        {
            throw new NotImplementedException();
        }


        #region Private Method

        private string ValidateLoginSunAndBosowa(List<DNetValidationResult> validationResults)
        {
            string DealerName = string.Empty;
            x.Dealer dealer = null;
            if (ValidationHelper.ValidateDealer(DealerCode, validationResults, this.DealerCode, ref dealer))
            {
                DealerName = dealer.DealerName;
            }
            return DealerName;
        }

        #endregion
    }
}
