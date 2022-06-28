using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.SpareParts.SqlQuery.POEstimate;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class SparePartPOEstimateDetailRepository : BaseDNetRepository<VWI_POEstimateHaveBillingDetail>, ISparePartPOEstimateDetailRepository<VWI_POEstimateHaveBillingDetail, int>
    {
        public SparePartPOEstimateDetailRepository(string connectionString)
            : base(connectionString)
        { }

        public List<VWI_POEstimateHaveBillingDetail> GetByListOfPOEstimateId(List<int> listOfPOEstimateId)
        {
            List<VWI_POEstimateHaveBillingDetail> result = new List<VWI_POEstimateHaveBillingDetail>();
            if (listOfPOEstimateId != null && listOfPOEstimateId.Count() > 0)
            {
                try
                {
                    using (var cn = Connection)
                    {
                        result = cn.Query<VWI_POEstimateHaveBillingDetail>(
                            POEstimateQuery.GetDetailByListOfPOEstimateId,
                            new { ListOfPOEstimateId = listOfPOEstimateId }, null, true, Timeout).ToList();
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    return new List<VWI_POEstimateHaveBillingDetail>();
                }
            }

            return result;

        }

        #region Not Implemented
        public VWI_POEstimateHaveBillingDetail Get(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Create(VWI_POEstimateHaveBillingDetail entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Update(VWI_POEstimateHaveBillingDetail entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<VWI_POEstimateHaveBillingDetail> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<VWI_POEstimateHaveBillingDetail> Search(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
