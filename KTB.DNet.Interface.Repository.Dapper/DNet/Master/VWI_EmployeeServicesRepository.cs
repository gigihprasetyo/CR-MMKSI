using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.Master.SqlQuery.VWI_EmployeeService;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class VWI_EmployeeServicesRepository : BaseDNetRepository<VWI_EmployeeService>, IVWI_EmployeeServicesRepository<VWI_EmployeeService, int>
    {
        public VWI_EmployeeServicesRepository(string connectionString)
            : base(connectionString)
        { }

        public List<VWI_EmployeeService> Search(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != null && sortColumns.Count > 0 ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_EmployeeService> result = Search<VWI_EmployeeService>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_EmployeeService>(query, sqlParams, null, true, Timeout, null).ToList();
                },
                Connection,
                string.Format(VWI_EmployeeServiceQuery.SelectWithProfile, criteria == null ? string.Empty : criteria.ToString()),
                "VWI_EmployeeService.ID", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_EmployeeServiceQuery.GetTotalWithProfile, criteria == null ? string.Empty : criteria.ToString()));

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_EmployeeService>();
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
        public List<VWI_EmployeeService> SearchResign(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != null && sortColumns.Count > 0 ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_EmployeeService> result = Search<VWI_EmployeeService>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_EmployeeService>(query, sqlParams, null, true, Timeout, null).ToList();
                },
                Connection,
                string.Format(VWI_EmployeeServiceQuery.SelectTotalResignSpecificFields, criteria == null ? string.Empty : criteria.ToString()),
                "VWI_EmployeeService.ID", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_EmployeeServiceQuery.GetTotalResignWithSpecificData, criteria == null ? string.Empty : criteria.ToString()));

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_EmployeeService>();
            }
        }

        #region Not Implemented

        public ResponseMessage Create(VWI_EmployeeService entity)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public VWI_EmployeeService Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<VWI_EmployeeService> GetAll()
        {
            throw new NotImplementedException();
        }

        public ResponseMessage Update(VWI_EmployeeService entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
