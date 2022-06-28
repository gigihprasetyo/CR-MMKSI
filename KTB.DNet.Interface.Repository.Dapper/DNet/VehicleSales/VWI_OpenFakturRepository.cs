using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Repository.Dapper.DNet.VehicleSales.SqlQuery.VWI_OpenFaktur;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class VWI_OpenFakturRepository : BaseDNetRepository<VWI_OpenFaktur>, IVWI_OpenFakturRepository<VWI_OpenFaktur, int>
    {
        public VWI_OpenFakturRepository(string connectionString)
            : base(connectionString)
        { }

        public List<VWI_OpenFaktur> Search(ICriteria criteria, ICriteria chassisQueryCriteria, ICriteria lastUpdateQueryCriteria, ICriteria createdTimeQueryCriteria, System.Collections.ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                var newStrCriteria = criteria == null ? "WHERE CBUReturn.ID IS NULL" : criteria.ToString() + " AND CBUReturn.ID IS NULL";
                ChassisMaster chassis = null;
                ChassisMaster chassisIR = null;
                List<VWI_OpenFaktur> result = null;

                string orderBy = sortColumns != null && sortColumns.Count > 0 ? sortColumns.ToString() : null;
                filteredResultsCount = 0;


                if (chassisQueryCriteria != null)
                {
                    using (var cn = Connection)
                    {
                        chassis = cn.Query<ChassisMaster>(string.Format(VWI_OpenFakturQuery.SelectQueryChassisID, chassisQueryCriteria.ToString())).FirstOrDefault();
                    }

                    if (chassis != null)
                    {
                        using (var cn = Connection)
                        {
                            chassisIR = cn.Query<ChassisMaster>(string.Format(
                                VWI_OpenFakturQuery.SelectQueryChassisIR), new { ChassisMasterID = chassis.ID }).FirstOrDefault();
                        }

                        if (chassisIR != null)
                        {
                            using (var cn = Connection)
                            {
                                result = cn.Query<VWI_OpenFaktur>(string.Format(
                                    VWI_OpenFakturQuery.SelectQueryResultByIR, newStrCriteria),
                                    new { ChassisMasterID = chassis.ID }).ToList();
                            }
                        }
                        else
                        {
                            using (var cn = Connection)
                            {
                                result = cn.Query<VWI_OpenFaktur>(string.Format(VWI_OpenFakturQuery.SelectQueryResultByChassis, chassisQueryCriteria.ToString(), null)).ToList();
                            }
                        }
                    }

                    if (result != null && result.Count > 0)
                    {
                        filteredResultsCount = 1;
                    }
                    else
                    {
                        filteredResultsCount = 0;
                    }
                }
                else if (chassisQueryCriteria == null && (lastUpdateQueryCriteria != null || createdTimeQueryCriteria != null))
                {
                    result = Search<VWI_OpenFaktur>((connection, query, sqlparams) =>
                    {
                        return connection.Query<VWI_OpenFaktur>(query, sqlparams, null, true, 500, null).ToList();
                    }, Connection, string.Format(VWI_OpenFakturQuery.SelectQueryResultByOpenFaktur,
                                                    lastUpdateQueryCriteria != null ? lastUpdateQueryCriteria : createdTimeQueryCriteria, newStrCriteria)
                    , "VWI_OpenFaktur.ID", null, orderBy, out filteredResultsCount, page, pageSize,
                    string.Format(VWI_OpenFakturQuery.GetTotalQueryByLastUpdate,
                                                    lastUpdateQueryCriteria != null ? lastUpdateQueryCriteria : createdTimeQueryCriteria, newStrCriteria), "ID");

                }
                else
                {
                    result = Search<VWI_OpenFaktur>((connection, query, sqlparams) =>
                    {
                        return connection.Query<VWI_OpenFaktur>(query, sqlparams, null, true, Timeout, null).ToList();
                    }, Connection, string.Format(VWI_OpenFakturQuery.SelectQueryResultGeneral,
                                                                        null, newStrCriteria)
                                        , "VWI_OpenFaktur.ID", null, orderBy, out filteredResultsCount, page, pageSize,
                                        string.Format(VWI_OpenFakturQuery.GetTotalQuery,
                                                                        null, newStrCriteria), "ID");


                }

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_OpenFaktur>();
            }
        }


        #region Not Implemented
        public VWI_OpenFaktur Get(int id)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Create(VWI_OpenFaktur entity)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Update(VWI_OpenFaktur entity)
        {
            throw new NotImplementedException();
        }

        public Framework.ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<VWI_OpenFaktur> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<VWI_OpenFaktur> Search(ICriteria criteria, System.Collections.ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
