#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SVC_FreeServiceToBeInvoiced repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 03/02/2020 15:09:45
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_CRM_SVC_FreeServiceToBeInvoiced;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_SVC_FreeServiceToBeInvoicedRepository : BaseDNetRepository<VWI_CRM_SVC_FreeServiceToBeInvoiced>, IVWI_CRM_SVC_FreeServiceToBeInvoicedRepository<VWI_CRM_SVC_FreeServiceToBeInvoiced, int>
    {
        #region Constructor
        public VWI_CRM_SVC_FreeServiceToBeInvoicedRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_SVC_FreeServiceToBeInvoiced
        /// <summary>
        /// Create VWI_CRM_SVC_FreeServiceToBeInvoiced
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_SVC_FreeServiceToBeInvoiced entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_SVC_FreeServiceToBeInvoiced
        /// <summary>
        /// Update VWI_CRM_SVC_FreeServiceToBeInvoiced
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_SVC_FreeServiceToBeInvoiced entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_SVC_FreeServiceToBeInvoiced
        /// <summary>
        /// Delete VWI_CRM_SVC_FreeServiceToBeInvoiced
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_SVC_FreeServiceToBeInvoiced By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_SVC_FreeServiceToBeInvoiced Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_SVC_FreeServiceToBeInvoiced>(
                        VWI_CRM_SVC_FreeServiceToBeInvoicedQuery.GetVWI_CRM_SVC_FreeServiceToBeInvoicedById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_SVC_FreeServiceToBeInvoiced
        /// <summary>
        /// Get All VWI_CRM_SVC_FreeServiceToBeInvoiced
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_SVC_FreeServiceToBeInvoiced> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_SVC_FreeServiceToBeInvoiced
        public List<VWI_CRM_SVC_FreeServiceToBeInvoiced> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_SVC_FreeServiceToBeInvoiced>();
        }
        #endregion

		#region Search VWI_CRM_SVC_FreeServiceToBeInvoiced        
        public new List<VWI_CRM_SVC_FreeServiceToBeInvoiced> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_SVC_FreeServiceToBeInvoiced> result = Search<VWI_CRM_SVC_FreeServiceToBeInvoiced>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_SVC_FreeServiceToBeInvoiced>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_SVC_FreeServiceToBeInvoicedQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_SVC_FreeServiceToBeInvoiced.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_SVC_FreeServiceToBeInvoicedQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_SVC_FreeServiceToBeInvoiced>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_SVC_FreeServiceToBeInvoiced vWI_CRM_SVC_FreeServiceToBeInvoiced)
        {
            //vWI_CRM_SVC_FreeServiceToBeInvoiced.CreatedBy = UserLogin;
            //vWI_CRM_SVC_FreeServiceToBeInvoiced.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_SVC_FreeServiceToBeInvoiced);
        }

        protected void SetLastModifiedLog(VWI_CRM_SVC_FreeServiceToBeInvoiced vWI_CRM_SVC_FreeServiceToBeInvoiced)
        {
            //vWI_CRM_SVC_FreeServiceToBeInvoiced.LastUpdateBy = UserLogin;
            //vWI_CRM_SVC_FreeServiceToBeInvoiced.LastUpdateTime = DateTime.Now;
        }
    }
}