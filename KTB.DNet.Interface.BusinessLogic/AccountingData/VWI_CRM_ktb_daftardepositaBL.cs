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
    public class VWI_CRM_ktb_daftardepositaBL : AbstractBusinessLogic, IVWI_CRM_ktb_daftardepositaBL
    {
        #region Variables
        private IVWI_CRM_ktb_daftardepositaRepository<VWI_CRM_ktb_daftardeposita, int> _vWI_CRM_ktb_daftardepositaRepo;
        #endregion

        #region Constructor
        public VWI_CRM_ktb_daftardepositaBL(IVWI_CRM_ktb_daftardepositaRepository<VWI_CRM_ktb_daftardeposita, int> vWI_CRM_ktb_daftardepositaRepo)
        {
            _vWI_CRM_ktb_daftardepositaRepo = vWI_CRM_ktb_daftardepositaRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_CRM_ktb_daftardeposita by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_CRM_ktb_daftardepositaDto>> Read(VWI_CRM_ktb_daftardepositaFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<VWI_CRM_ktb_daftardepositaDto>> ReadList(VWI_CRM_ktb_daftardepositaFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_CRM_ktb_daftardepositaDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_CRM_ktb_daftardeposita), filterDto);

                if (DealerCode.ToUpper() == "MKS")
                {
                    if (listDealer.Count > 0)
                    {
                        // filter by company
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_ktb_daftardeposita), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                        criterias = Helper.UpdateStrCriteria(typeof(VWI_CRM_ktb_daftardeposita), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "msdyn_companycode", dealerCompanyCode, false, criterias);

                        sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_CRM_ktb_daftardeposita), filterDto);

                        List<VWI_CRM_ktb_daftardeposita> data = _vWI_CRM_ktb_daftardepositaRepo.Search(
                                            criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                        if (data != null && data.Count > 0)
                        {
                            result.lst = data.ConvertList<VWI_CRM_ktb_daftardeposita, VWI_CRM_ktb_daftardepositaDto>();
                            result.total = filteredTotalRow;
                        }
                        else
                        {
                            ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_CRM_ktb_daftardeposita), filterDto);
                        }

                        result.success = true;
                    }
                    else
                    {
                        ErrorMsgHelper.Exception(result.messages, "Dealer Company to Dealer Configuration is not set");
                    }
                }
                else
                {
                    ErrorMsgHelper.Exception(result.messages, "Dealer Company Configuration is not set");
                }
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

        /// <summary>
        /// Delete VWI_CRM_ktb_daftardeposita by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_CRM_ktb_daftardepositaDto> Delete(int id)
        {
            return null;
        }
        public ResponseBase<VWI_CRM_ktb_daftardepositaDto> Create(VWI_CRM_ktb_daftardepositaDtoParameter objCreate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<VWI_CRM_ktb_daftardepositaDto> Update(VWI_CRM_ktb_daftardepositaDtoParameter objUpdate)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
