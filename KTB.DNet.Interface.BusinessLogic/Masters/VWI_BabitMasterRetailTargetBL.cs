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
    public class VWI_BabitMasterRetailTargetBL : AbstractBusinessLogic, IVWI_BabitMasterRetailTargetBL
    {
        #region Variables
        private IVWI_BabitMasterRetailTargetRepository<VWI_BabitMasterRetailTarget, int> _vWI_CRM_BabitMasterRetailTargetRepo;
        #endregion

        #region Constructor
        public VWI_BabitMasterRetailTargetBL(IVWI_BabitMasterRetailTargetRepository<VWI_BabitMasterRetailTarget, int> vWI_CRM_BabitMasterRetailTargetRepo)
        {
            _vWI_CRM_BabitMasterRetailTargetRepo = vWI_CRM_BabitMasterRetailTargetRepo;
        }
        #endregion

        public ResponseBase<VWI_BabitMasterRetailTargetDto> Create(VWI_BabitMasterRetailTargetParameterDto objCreate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<VWI_BabitMasterRetailTargetDto> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<List<VWI_BabitMasterRetailTargetDto>> Read(VWI_BabitMasterRetailTargetFilterDto filterDto, int pageSize)
        {
            var result = new ResponseBase<List<VWI_BabitMasterRetailTargetDto>>();
            var sortColl = new SortCollection();
            int totalRow = 0;
            int filteredTotalRow = 0;

            try
            {
                var innerQueryCriteria = string.Empty;
                
                sortColl = Helper.UpdateSortColumn(typeof(VWI_BabitMasterRetailTarget), filterDto, sortColl);

                var criterias = Helper.InitialStrCriteria(typeof(VWI_BabitMasterRetailTarget), filterDto);
                criterias = Helper.UpdateStrCriteria(typeof(VWI_BabitMasterRetailTarget), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(VWI_BabitMasterRetailTarget), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);

                List<VWI_BabitMasterRetailTarget> data = _vWI_CRM_BabitMasterRetailTargetRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, innerQueryCriteria == string.Empty ? filterDto.pages : 1, pageSize, out filteredTotalRow, out totalRow);


                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_BabitMasterRetailTarget, VWI_BabitMasterRetailTargetDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_BabitMasterRetailTarget), filterDto);
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

        public ResponseBase<VWI_BabitMasterRetailTargetDto> Update(VWI_BabitMasterRetailTargetParameterDto objUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
