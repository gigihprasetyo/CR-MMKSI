using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Repository.Dapper.DNet.VehicleSales.SqlQuery.TrLeadStatusToStatus;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class TrLeadStatusToStatusRepository : BaseDNetRepository<TrLeadStatusToStatus>, ITrLeadStatusToStatusRepository<TrLeadStatusToStatus, int>
    {
        public TrLeadStatusToStatusRepository(string connectionString)
           : base(connectionString)
        { }

        #region Public Method
        public TrLeadStatusToStatus GetByCustomerStatusAndNextLeadStatus(int customerStatus, int nextLeadStatus)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<TrLeadStatusToStatus>(
                        TrLeadStatusToStatusQuery.SelectTrLeadStatusToStatus, 
                            new 
                            {   
                                @LeadStatusId = (int?) null,
                                @SAPCustomerStatusID = customerStatus,
                                @NextLeadStatusID = nextLeadStatus
                            }
                        ).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return new TrLeadStatusToStatus();
            }
        }

        public TrLeadStatusToStatus GetByParam(int customerStatus, int nextLeadStatus, int leadStatus)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<TrLeadStatusToStatus>(
                        TrLeadStatusToStatusQuery.SelectTrLeadStatusToStatus,
                            new
                            {
                                @SAPCustomerStatusID = customerStatus,
                                @NextLeadStatusID = nextLeadStatus,
                                @LeadStatusID = leadStatus
                            }
                        ).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return new TrLeadStatusToStatus();
            }
        }
        #endregion

        #region Private Method

        #endregion

        #region Not Implemented
        public TrLeadStatusToStatus Get(int id)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Create(TrLeadStatusToStatus entity)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Update(TrLeadStatusToStatus entity)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<TrLeadStatusToStatus> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<TrLeadStatusToStatus> Search(ICriteria criteria, System.Collections.ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
