#region Namespace Imports
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using System.Collections;
using Dapper;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Repository.Dapper.DNet.SpareParts.SqlQuery.SparePartPenaltyHeader;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion
namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class SparePartPenaltyHeaderRepository : BaseDNetRepository<SparePartPenaltyHeader>, ISparePartPenaltyHeaderRepository<SparePartPenaltyHeader, int>
    {
        #region Constructor
        public SparePartPenaltyHeaderRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create SparePartPenaltyHeader
        /// <summary>
        /// Create SparePartPenaltyHeader
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(SparePartPenaltyHeader entity)
        {
            return null;
        }
        #endregion

        #region Update SparePartPenaltyHeader
        /// <summary>
        /// Update SparePartPenaltyHeader
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(SparePartPenaltyHeader entity)
        {
            return null;
        }
        #endregion

        #region Delete SparePartPenaltyHeader
        /// <summary>
        /// Delete SparePartPenaltyHeader
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get SparePartPenaltyHeader By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SparePartPenaltyHeader Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<SparePartPenaltyHeader>(
                        SparePartPenaltyHeaderQuery.GetSparePartPenaltyHeaderById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All SparePartPenaltyHeader
        /// <summary>
        /// Get All SparePartPenaltyHeader
        /// </summary>
        /// <returns></returns>
        public List<SparePartPenaltyHeader> GetAll()
        {
            return null;
        }
        #endregion

        #region Search SparePartPenaltyHeader
        public List<SparePartPenaltyHeader> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<SparePartPenaltyHeader>();
        }
        #endregion

        #region Search SparePartPenaltyHeader        
        public new List<SparePartPenaltyHeader> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<SparePartPenaltyHeader> result = Search<SparePartPenaltyHeader>((connection, query, sqlParams) =>
                {
                    return connection.Query<SparePartPenaltyHeader>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(SparePartPenaltyHeaderQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "SparePartPenaltyHeader.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(SparePartPenaltyHeaderQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<SparePartPenaltyHeader>();
            }
        }
        #endregion

        protected void SetCreatedLog(SparePartPenaltyHeader sparePartPenaltyHeader)
        {
            //vWI_CRM_WOInvoice.CreatedBy = UserLogin;
            //vWI_CRM_WOInvoice.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_WOInvoice);
        }

        protected void SetLastModifiedLog(SparePartPenaltyHeader sparePartPenaltyHeader)
        {
            //vWI_CRM_WOInvoice.LastUpdateBy = UserLogin;
            //vWI_CRM_WOInvoice.LastUpdateTime = DateTime.Now;
        }

        public new List<SparePartPenaltyHeader> Search(ICriteria criteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            throw new NotImplementedException();
        }
    }
}


