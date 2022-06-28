using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Repository.Dapper.DNet.VehicleSales.SqlQuery.TrLeadStatusToLeadState;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class TrStatusToLeadStatusCodeToLeadStateCodeRepository : BaseDNetRepository<TrStatusToLeadStatusCodeToLeadStateCode>, ITrStatusToLeadStatusCodeToLeadStateCodeRepository<TrStatusToLeadStatusCodeToLeadStateCode, int>
    {
        public TrStatusToLeadStatusCodeToLeadStateCodeRepository(string connectionString)
           : base(connectionString)
        { }

        #region Public Method
        public TrStatusToLeadStatusCodeToLeadStateCode GetByParam(int customerStatus, int leadStatus, int leadState)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<TrStatusToLeadStatusCodeToLeadStateCode>(
                        TrLeadStatusToLeadStateQuery.SelectTrLeadStatusToLeadState,
                            new
                            {
                                @StatusID = customerStatus,
                                @LeadStatusCodeID = leadStatus,
                                @LeadStateCodeID = leadState
                            }
                        ).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return new TrStatusToLeadStatusCodeToLeadStateCode();
            }
        }


        #endregion

        #region Private Method

        #endregion

        #region Not Implemented

        TrStatusToLeadStatusCodeToLeadStateCode IBaseDNetRepository<TrStatusToLeadStatusCodeToLeadStateCode, int>.Get(int id)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Create(TrStatusToLeadStatusCodeToLeadStateCode entity)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Update(TrStatusToLeadStatusCodeToLeadStateCode entity)
        {
            throw new NotImplementedException();
        }

        List<TrStatusToLeadStatusCodeToLeadStateCode> IBaseDNetRepository<TrStatusToLeadStatusCodeToLeadStateCode, int>.GetAll()
        {
            throw new NotImplementedException();
        }

        List<TrStatusToLeadStatusCodeToLeadStateCode> IBaseDNetRepository<TrStatusToLeadStatusCodeToLeadStateCode, int>.Search(ICriteria criteria, System.Collections.ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }
        #endregion



        
    }
}
