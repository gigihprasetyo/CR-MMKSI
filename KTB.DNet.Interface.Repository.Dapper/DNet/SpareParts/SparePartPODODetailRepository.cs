using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.SpareParts.SqlQuery.PODO;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class SparePartPODODetailRepository : BaseDNetRepository<VWI_SparePartPODOHaveBillingDetail>, ISparePartPODODetailRepository<VWI_SparePartPODOHaveBillingDetail, int>
    {
        public SparePartPODODetailRepository(string connectionString)
            : base(connectionString)
        { }

        public List<VWI_SparePartPODOHaveBillingDetail> GetByListOfSparePartDOId(List<int> listOfId)
        {
            List<VWI_SparePartPODOHaveBillingDetail> result = new List<VWI_SparePartPODOHaveBillingDetail>();
            if (listOfId != null && listOfId.Count() > 0)
            {
                try
                {
                    using (var cn = Connection)
                    {
                        result = cn.Query<VWI_SparePartPODOHaveBillingDetail>(PODOQuery.GetPODODetailByListOfDOId, new { ListOfDOId = listOfId }, null, true, Timeout).ToList();
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    return new List<VWI_SparePartPODOHaveBillingDetail>();
                }
            }

            return result;
        }

        public List<VWI_SparePartPODOHaveBillingHeaderDetail> GetByQuery(string query)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_SparePartPODOHaveBillingHeaderDetail>(query).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<VWI_SparePartPODOHaveBillingHeaderDetail>();
            }
        }

        public SparePartBillingDetail GetSparePartBillingDetailByQuery(string query)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<SparePartBillingDetail>(query).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #region Not Implemented
        public VWI_SparePartPODOHaveBillingDetail Get(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Create(VWI_SparePartPODOHaveBillingDetail entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Update(VWI_SparePartPODOHaveBillingDetail entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<VWI_SparePartPODOHaveBillingDetail> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<VWI_SparePartPODOHaveBillingDetail> Search(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
        #endregion






    }
}
