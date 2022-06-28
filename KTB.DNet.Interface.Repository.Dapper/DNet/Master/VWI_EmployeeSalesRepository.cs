using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.Master.SqlQuery.VWI_EmployeeSales;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class VWI_EmployeeSalesRepository : BaseDNetRepository<VWI_EmployeeSales>, IVWI_EmployeeSalesRepository<VWI_EmployeeSales, int>
    {
        public VWI_EmployeeSalesRepository(string connectionString)
            : base(connectionString)
        { }

        public List<VWI_EmployeeSales> Search(ICriteria criteria, ICriteria innerQueryCriteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != null && sortColumns.Count > 0 ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_EmployeeSales> result = Search<VWI_EmployeeSales>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_EmployeeSales>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_EmployeeSalesQuery.SelectQuery,
                                                innerQueryCriteria == null ? string.Empty : innerQueryCriteria.ToString(),
                                                criteria == null ? string.Empty : criteria.ToString())
                , "VWI_EmployeeSales.ID", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_EmployeeSalesQuery.GetTotalQuery,
                                                innerQueryCriteria == null ? string.Empty : innerQueryCriteria.ToString(),
                                                criteria == null ? string.Empty : criteria.ToString()));
                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_EmployeeSales>();
            }
        }

        public List<VWI_EmployeeSales> Search(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount, List<int> dealerCategoryIdList, bool isCheckDealerCategory = false)
        {
            try
            {
                string orderBy = sortColumns != null && sortColumns.Count > 0 ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_EmployeeSales> result = Search<VWI_EmployeeSales>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_EmployeeSales>(query, sqlParams, null, true, Timeout, null).ToList();
                }, 
                Connection, 
                string.Format(VWI_EmployeeSalesQuery.SelectWithProfile, criteria == null ? string.Empty : criteria.ToString()), 
                "VWI_EmployeeSales.ID", 
                new 
                { 
                    @CheckDealerCategory = isCheckDealerCategory, 
                    @DealerCategoryIdList = dealerCategoryIdList
                }, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_EmployeeSalesQuery.GetTotalWithProfile,
                                                criteria == null ? string.Empty : criteria.ToString()));

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_EmployeeSales>();
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
                string.Format(VWI_EmployeeSalesQuery.SelectTotalResignSpecificFields, criteria == null ? string.Empty : criteria.ToString()),
                "VWI_EmployeeResign.ID", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_EmployeeSalesQuery.GetTotalResignWithSpecificData,
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
        public VWI_EmployeeSales Get(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Create(VWI_EmployeeSales entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Update(VWI_EmployeeSales entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<VWI_EmployeeSales> GetAll()
        {
            throw new NotImplementedException();
        }


        public List<VWI_EmployeeSales> Search(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}
