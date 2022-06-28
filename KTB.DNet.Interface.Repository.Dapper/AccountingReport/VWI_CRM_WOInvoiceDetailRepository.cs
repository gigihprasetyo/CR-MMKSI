#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_WOInvoiceDetail repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_CRM_WOInvoiceDetail;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_WOInvoiceDetailRepository : BaseDNetRepository<VWI_CRM_WOInvoiceDetail>, IVWI_CRM_WOInvoiceDetailRepository<VWI_CRM_WOInvoiceDetail, int>
    {
        #region Constructor
        public VWI_CRM_WOInvoiceDetailRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_WOInvoiceDetail
        /// <summary>
        /// Create VWI_CRM_WOInvoiceDetail
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_WOInvoiceDetail entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_WOInvoiceDetail
        /// <summary>
        /// Update VWI_CRM_WOInvoiceDetail
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_WOInvoiceDetail entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_WOInvoiceDetail
        /// <summary>
        /// Delete VWI_CRM_WOInvoiceDetail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_WOInvoiceDetail By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_WOInvoiceDetail Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_WOInvoiceDetail>(
                        VWI_CRM_WOInvoiceDetailQuery.GetVWI_CRM_WOInvoiceDetailById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_WOInvoiceDetail
        /// <summary>
        /// Get All VWI_CRM_WOInvoiceDetail
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_WOInvoiceDetail> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_WOInvoiceDetail
        public List<VWI_CRM_WOInvoiceDetail> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_WOInvoiceDetail>();
        }
        #endregion

		#region Search VWI_CRM_WOInvoiceDetail        
        public new List<VWI_CRM_WOInvoiceDetail> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_WOInvoiceDetail> result = Search<VWI_CRM_WOInvoiceDetail>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_WOInvoiceDetail>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_WOInvoiceDetailQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_WOInvoiceDetail.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_WOInvoiceDetailQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_WOInvoiceDetail>();
            }
        }

        public new List<VWI_CRM_WOInvoiceDetail> SearchCustom(string strCriteria)
        {
            try
            {
                int page = 1;
                int pageSize = 5000;
                string orderBy = null;
                int filteredResultsCount = 0;
                string strInnerCriteria = string.Empty;

                List<VWI_CRM_WOInvoiceDetail> result = SearchFetchPaging<VWI_CRM_WOInvoiceDetail>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_WOInvoiceDetail>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_WOInvoiceDetailQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_WOInvoiceDetail.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_WOInvoiceDetailQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                return result;

            }
            catch (Exception ex)
            {
                return new List<VWI_CRM_WOInvoiceDetail>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_WOInvoiceDetail vWI_CRM_WOInvoiceDetail)
        {
            //vWI_CRM_WOInvoiceDetail.CreatedBy = UserLogin;
            //vWI_CRM_WOInvoiceDetail.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_WOInvoiceDetail);
        }

        protected void SetLastModifiedLog(VWI_CRM_WOInvoiceDetail vWI_CRM_WOInvoiceDetail)
        {
            //vWI_CRM_WOInvoiceDetail.LastUpdateBy = UserLogin;
            //vWI_CRM_WOInvoiceDetail.LastUpdateTime = DateTime.Now;
        }
    }
}