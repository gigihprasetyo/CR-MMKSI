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
    public class DMSAnnouncementBL : AbstractBusinessLogic, IDMSAnnouncementBL
    {
        #region Variables
        private IDMSAnnouncementRepository<DMSAnnouncement, int> _dMSAnnouncementRepo;
        #endregion

        #region Constructor
        public DMSAnnouncementBL(IDMSAnnouncementRepository<DMSAnnouncement, int> DMSAnnouncementRepo)
        {
            _dMSAnnouncementRepo = DMSAnnouncementRepo;
        }
        #endregion

        public ResponseBase<DMSAnnouncementDto> Create(DMSAnnouncementParameterDto objCreate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<DMSAnnouncementDto> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<List<DMSAnnouncementDto>> Read(DMSAnnouncementFilterDto filterDto, int pageSize)
        {
            var result = new ResponseBase<List<DMSAnnouncementDto>>();
            var sortColl = new SortCollection();
            int totalRow = 0;
            int filteredTotalRow = 0;

            try
            {
                var innerQueryCriteria = string.Empty;

                sortColl = Helper.UpdateSortColumn(typeof(DMSAnnouncement), filterDto, sortColl);

                var criterias = Helper.InitialStrCriteria(typeof(DMSAnnouncement), filterDto);
                criterias = Helper.UpdateStrCriteria(typeof(DMSAnnouncement), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                List<DMSAnnouncement> data = _dMSAnnouncementRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, innerQueryCriteria == string.Empty ? filterDto.pages : 1, pageSize, out filteredTotalRow, out totalRow);


                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<DMSAnnouncement, DMSAnnouncementDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(DMSAnnouncement), filterDto);
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

        public ResponseBase<DMSAnnouncementDto> Update(DMSAnnouncementParameterDto objUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
