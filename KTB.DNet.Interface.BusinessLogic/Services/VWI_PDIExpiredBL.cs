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

namespace KTB.DNet.Interface.BusinessLogic
{
    public class VWI_PDIExpiredBL : AbstractBusinessLogic, IVWI_PDIExpiredBL
    {
        #region Variables
        private IVWI_PDIExpiredRepository<VWI_PDIExpired, int> _VWI_PDIExpiredRepo;
        #endregion

        #region Constructor
        public VWI_PDIExpiredBL(IVWI_PDIExpiredRepository<VWI_PDIExpired, int> VWI_PDIExpiredRepo)
        {
            _VWI_PDIExpiredRepo = VWI_PDIExpiredRepo;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get VWI_PDIExpired by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_PDIExpiredDto>> Read(VWI_PDIExpiredFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<VWI_PDIExpiredDto>> ReadList(VWI_PDIExpiredFilterDto filterDto, int pageSize, List<Domain.Dealer> listDealer, string dealerCompanyCode)
        {
            var result = new ResponseBase<List<VWI_PDIExpiredDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                // get criteria
                var criterias = Helper.InitialStrCriteria(typeof(VWI_PDIExpired), filterDto);

                // filter by Dealer Code
                criterias = Helper.UpdateStrCriteria(typeof(VWI_PDIExpired), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.InSet.GetHashCode(), "DealerCode", dealerCompanyCode, false, criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(VWI_PDIExpired), filterDto);

                List<VWI_PDIExpired> data = _VWI_PDIExpiredRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_PDIExpired, VWI_PDIExpiredDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_PDIExpired), filterDto);
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

        /// <summary>
        /// Delete VWI_PDIExpired by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VWI_PDIExpiredDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new VWI_PDIExpired
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_PDIExpiredDto> Create(VWI_PDIExpiredParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update VWI_PDIExpired
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VWI_PDIExpiredDto> Update(VWI_PDIExpiredParameterDto objUpdate)
        {
            return null;
        }
        #endregion

    }
}
