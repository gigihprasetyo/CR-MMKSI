#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SOInvoice repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 01/03/2021 0:51:45
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_CRM_SOInvoice;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_SOInvoiceRepository : BaseDNetRepository<VWI_CRM_SOInvoice>, IVWI_CRM_SOInvoiceRepository<VWI_CRM_SOInvoice, int>
    {
        #region Constructor
        public VWI_CRM_SOInvoiceRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_SOInvoice
        /// <summary>
        /// Create VWI_CRM_SOInvoice
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_SOInvoice entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_SOInvoice
        /// <summary>
        /// Update VWI_CRM_SOInvoice
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_SOInvoice entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_SOInvoice
        /// <summary>
        /// Delete VWI_CRM_SOInvoice
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_SOInvoice By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_SOInvoice Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_SOInvoice>(
                        VWI_CRM_SOInvoiceQuery.GetVWI_CRM_SOInvoiceById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_SOInvoice
        /// <summary>
        /// Get All VWI_CRM_SOInvoice
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_SOInvoice> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_SOInvoice
        public List<VWI_CRM_SOInvoice> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_SOInvoice>();
        }
        #endregion

		#region Search VWI_CRM_SOInvoice        
        public new List<VWI_CRM_SOInvoice> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_SOInvoice> result = SearchFetchPaging<VWI_CRM_SOInvoice>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_SOInvoice>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_SOInvoiceQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_SOInvoice.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_SOInvoiceQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_SOInvoice>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_SOInvoice vWI_CRM_SOInvoice)
        {
            //vWI_CRM_SOInvoice.CreatedBy = UserLogin;
            //vWI_CRM_SOInvoice.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_SOInvoice);
        }

        protected void SetLastModifiedLog(VWI_CRM_SOInvoice vWI_CRM_SOInvoice)
        {
            //vWI_CRM_SOInvoice.LastUpdateBy = UserLogin;
            //vWI_CRM_SOInvoice.LastUpdateTime = DateTime.Now;
        }
    }
}