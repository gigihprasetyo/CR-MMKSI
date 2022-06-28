#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_WOInvoice repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/02/2021 16:16:28
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_CRM_WOInvoice;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_WOInvoiceRepository : BaseDNetRepository<VWI_CRM_WOInvoice>, IVWI_CRM_WOInvoiceRepository<VWI_CRM_WOInvoice, int>
    {
        #region Constructor
        public VWI_CRM_WOInvoiceRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_WOInvoice
        /// <summary>
        /// Create VWI_CRM_WOInvoice
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_WOInvoice entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_WOInvoice
        /// <summary>
        /// Update VWI_CRM_WOInvoice
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_WOInvoice entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_WOInvoice
        /// <summary>
        /// Delete VWI_CRM_WOInvoice
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_WOInvoice By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_WOInvoice Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_WOInvoice>(
                        VWI_CRM_WOInvoiceQuery.GetVWI_CRM_WOInvoiceById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_WOInvoice
        /// <summary>
        /// Get All VWI_CRM_WOInvoice
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_WOInvoice> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_WOInvoice
        public List<VWI_CRM_WOInvoice> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_WOInvoice>();
        }
        #endregion

		#region Search VWI_CRM_WOInvoice        
        public new List<VWI_CRM_WOInvoice> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_WOInvoice> result = Search<VWI_CRM_WOInvoice>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_WOInvoice>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_WOInvoiceQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_WOInvoice.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_WOInvoiceQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_WOInvoice>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_WOInvoice vWI_CRM_WOInvoice)
        {
            //vWI_CRM_WOInvoice.CreatedBy = UserLogin;
            //vWI_CRM_WOInvoice.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_WOInvoice);
        }

        protected void SetLastModifiedLog(VWI_CRM_WOInvoice vWI_CRM_WOInvoice)
        {
            //vWI_CRM_WOInvoice.LastUpdateBy = UserLogin;
            //vWI_CRM_WOInvoice.LastUpdateTime = DateTime.Now;
        }
    }
}