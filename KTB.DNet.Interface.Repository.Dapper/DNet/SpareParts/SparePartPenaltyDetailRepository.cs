
#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : SparePartPenaltyDetail repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/02/2021 16:24:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.SpareParts.SqlQuery.SparePartPenaltyDetail;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class SparePartPenaltyDetailRepository : BaseDNetRepository<SparePartPenaltyDetail>, ISparePartPenaltyDetailRepository<SparePartPenaltyDetail, int>
    {
        #region Constructor
        public SparePartPenaltyDetailRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create SparePartPenaltyDetail
        /// <summary>
        /// Create SparePartPenaltyDetail
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(SparePartPenaltyDetail entity)
        {
            return null;
        }
        #endregion

        #region Update SparePartPenaltyDetail
        /// <summary>
        /// Update SparePartPenaltyDetail
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(SparePartPenaltyDetail entity)
        {
            return null;
        }
        #endregion

        #region Delete SparePartPenaltyDetail
        /// <summary>
        /// Delete SparePartPenaltyDetail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get SparePartPenaltyDetail By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SparePartPenaltyDetail Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<SparePartPenaltyDetail>(
                        SparePartPenaltyDetailQuery.GetSparePartPenaltyDetailById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All SparePartPenaltyDetail
        /// <summary>
        /// Get All SparePartPenaltyDetail
        /// </summary>
        /// <returns></returns>
        public List<SparePartPenaltyDetail> GetAll()
        {
            return null;
        }
        #endregion

        #region Search SparePartPenaltyDetail
        public List<SparePartPenaltyDetail> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<SparePartPenaltyDetail>();
        }
        #endregion

        #region Search SparePartPenaltyDetail        
        public new List<SparePartPenaltyDetail> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<SparePartPenaltyDetail> result = Search<SparePartPenaltyDetail>((connection, query, sqlParams) =>
                {
                    return connection.Query<SparePartPenaltyDetail>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(SparePartPenaltyDetailQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "SparePartPenaltyDetail.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(SparePartPenaltyDetailQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<SparePartPenaltyDetail>();
            }
        }

        public new List<SparePartPenaltyDetail> SearchCustom(string strCriteria)
        {
            try
            {
                int page = 1;
                int pageSize = 5000;
                string orderBy = null;
                int filteredResultsCount = 0;
                string strInnerCriteria = string.Empty;

                List<SparePartPenaltyDetail> result = SearchFetchPaging<SparePartPenaltyDetail>((connection, query, sqlParams) =>
                {
                    return connection.Query<SparePartPenaltyDetail>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(SparePartPenaltyDetailQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "SparePartPenaltyDetail.ID", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(SparePartPenaltyDetailQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "ID");

                return result;

            }
            catch (Exception ex)
            {
                return new List<SparePartPenaltyDetail>();
            }
        }
        #endregion

        protected void SetCreatedLog(SparePartPenaltyDetail sparePartPenaltyDetail)
        {
            //vWI_CRM_WOInvoiceDetail.CreatedBy = UserLogin;
            //vWI_CRM_WOInvoiceDetail.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_WOInvoiceDetail);
        }

        protected void SetLastModifiedLog(SparePartPenaltyDetail sparePartPenaltyDetail)
        {
            //vWI_CRM_WOInvoiceDetail.LastUpdateBy = UserLogin;
            //vWI_CRM_WOInvoiceDetail.LastUpdateTime = DateTime.Now;
        }
    }
}
