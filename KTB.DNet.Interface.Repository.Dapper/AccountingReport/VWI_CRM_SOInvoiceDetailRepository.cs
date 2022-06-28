#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SOInvoiceDetail repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 01/03/2021 0:32:40
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_CRM_SOInvoiceDetail;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_SOInvoiceDetailRepository : BaseDNetRepository<VWI_CRM_SOInvoiceDetail>, IVWI_CRM_SOInvoiceDetailRepository<VWI_CRM_SOInvoiceDetail, int>
    {
        #region Constructor
        public VWI_CRM_SOInvoiceDetailRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_SOInvoiceDetail
        /// <summary>
        /// Create VWI_CRM_SOInvoiceDetail
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_SOInvoiceDetail entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_SOInvoiceDetail
        /// <summary>
        /// Update VWI_CRM_SOInvoiceDetail
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_SOInvoiceDetail entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_SOInvoiceDetail
        /// <summary>
        /// Delete VWI_CRM_SOInvoiceDetail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_SOInvoiceDetail By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_SOInvoiceDetail Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_SOInvoiceDetail>(
                        VWI_CRM_SOInvoiceDetailQuery.GetVWI_CRM_SOInvoiceDetailById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_SOInvoiceDetail
        /// <summary>
        /// Get All VWI_CRM_SOInvoiceDetail
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_SOInvoiceDetail> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_SOInvoiceDetail
        public List<VWI_CRM_SOInvoiceDetail> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_SOInvoiceDetail>();
        }
        #endregion

		#region Search VWI_CRM_SOInvoiceDetail        
        public new List<VWI_CRM_SOInvoiceDetail> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_SOInvoiceDetail> result = Search<VWI_CRM_SOInvoiceDetail>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_SOInvoiceDetail>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_SOInvoiceDetailQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_SOInvoiceDetail.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_SOInvoiceDetailQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_SOInvoiceDetail>();
            }
        }

        public new List<VWI_CRM_SOInvoiceDetail> SearchCustom(string strCriteria)
        {
            try
            {
                int page = 1;
                int pageSize = 5000;
                string orderBy = null;
                int filteredResultsCount = 0;
                string strInnerCriteria = string.Empty;

                List<VWI_CRM_SOInvoiceDetail> result = SearchFetchPaging<VWI_CRM_SOInvoiceDetail>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_SOInvoiceDetail>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_SOInvoiceDetailQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_SOInvoiceDetail.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_SOInvoiceDetailQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                return result;

            }
            catch (Exception ex)
            {
                return new List<VWI_CRM_SOInvoiceDetail>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_SOInvoiceDetail vWI_CRM_SOInvoiceDetail)
        {
            //vWI_CRM_SOInvoiceDetail.CreatedBy = UserLogin;
            //vWI_CRM_SOInvoiceDetail.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_SOInvoiceDetail);
        }

        protected void SetLastModifiedLog(VWI_CRM_SOInvoiceDetail vWI_CRM_SOInvoiceDetail)
        {
            //vWI_CRM_SOInvoiceDetail.LastUpdateBy = UserLogin;
            //vWI_CRM_SOInvoiceDetail.LastUpdateTime = DateTime.Now;
        }
    }
}