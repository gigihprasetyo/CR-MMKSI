#region Namespace Imports
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using KTB.DNet.Interface.Domain;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class SFDContactBL : AbstractBusinessLogic, ISFDContactBL
    {
        #region Variables
        private ISFDContactRepository<SFDContact, int> _vWI_CRM_BabitMasterRetailTargetRepo;
        #endregion

        #region Constructor
        public SFDContactBL(ISFDContactRepository<SFDContact, int> vWI_CRM_BabitMasterRetailTargetRepo)
        {
            _vWI_CRM_BabitMasterRetailTargetRepo = vWI_CRM_BabitMasterRetailTargetRepo;
        }
        #endregion

        public ResponseBase<SFDContactDto> Create(SFDContactParameterDto objCreate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<SFDContactDto> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<List<SFDContactDto>> Read(SFDContactFilterDto filterDto, int pageSize)
        {
            var result = new ResponseBase<List<SFDContactDto>>();
            var sortColl = new SortCollection();
            int totalRow = 0;
            int filteredTotalRow = 0;

            try
            {
                var innerQueryCriteria = string.Empty;

                sortColl = Helper.UpdateSortColumn(typeof(SFDContact), filterDto, sortColl);

                var criterias = Helper.InitialStrCriteria(typeof(SFDContact), filterDto);
                criterias = Helper.UpdateStrCriteria(typeof(SFDContact), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(SFDContact), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(SFDContact), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.StartsWith.GetHashCode(), "Createdby", "S-", false, criterias);

                List<SFDContact> data = _vWI_CRM_BabitMasterRetailTargetRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, innerQueryCriteria == string.Empty ? filterDto.pages : 1, pageSize, out filteredTotalRow, out totalRow);


                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<SFDContact, SFDContactDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SFDContact), filterDto);
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

        public ResponseBase<SFDContactDto> Update(SFDContactParameterDto objUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
