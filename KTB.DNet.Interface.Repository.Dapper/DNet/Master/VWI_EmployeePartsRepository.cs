using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.Master.SqlQuery.VWI_EmployeeParts;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class VWI_EmployeePartsRepository : BaseDNetRepository<VWI_EmployeeParts>, IVWI_EmployeePartsRepository<VWI_EmployeeParts, int>
    {
        public VWI_EmployeePartsRepository(string connectionString)
            : base(connectionString)
        { }

        public List<VWI_EmployeeParts> Search(ICriteria criteria, ICriteria innerQueryCriteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != null && sortColumns.Count > 0 ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_EmployeeParts> result = Search<VWI_EmployeeParts>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_EmployeeParts>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_EmployeePartsQuery.SelectQuery,
                                                innerQueryCriteria == null ? string.Empty : innerQueryCriteria.ToString(),
                                                criteria == null ? string.Empty : criteria.ToString())
                , "VWI_EmployeeParts.ID", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_EmployeePartsQuery.GetTotalQuery,
                                                innerQueryCriteria == null ? string.Empty : innerQueryCriteria.ToString(),
                                                criteria == null ? string.Empty : criteria.ToString()));

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_EmployeeParts>();
            }
        }

        public List<VWI_EmployeeParts> Search(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != null && sortColumns.Count > 0 ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_EmployeeParts> result = Search<VWI_EmployeeParts>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_EmployeeParts>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_EmployeePartsQuery.SelectWithProfile,
                                                criteria == null ? string.Empty : criteria.ToString())
                , "VWI_EmployeeParts.ID", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_EmployeePartsQuery.GetTotalWithProfile,
                                                criteria == null ? string.Empty : criteria.ToString()));

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_EmployeeParts>();
            }
        }

        /// <summary>Searches the resign.</summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="filteredResultsCount">The filtered results count.</param>
        /// <param name="totalResultsCount">The total results count.</param>
        /// <returns></returns>
        public List<VWI_EmployeeResign> SearchResign(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != null && sortColumns.Count > 0 ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_EmployeeResign> result = Search<VWI_EmployeeResign>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_EmployeeResign>(query, sqlParams, null, true, Timeout, null).ToList();
                },
                Connection,
                string.Format(VWI_EmployeePartsQuery.SelectTotalResignSpecificFields, criteria == null ? string.Empty : criteria.ToString()),
                "VWI_EmployeeResign.ID", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_EmployeePartsQuery.GetTotalResignWithSpecificData,
                                                criteria == null ? string.Empty : criteria.ToString()));

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_EmployeeResign>();
            }
        }


        #region Not Implemented
        public VWI_EmployeeParts Get(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Create(VWI_EmployeeParts entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Update(VWI_EmployeeParts entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<VWI_EmployeeParts> GetAll()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
