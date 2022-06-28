#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_accountreceivableinvoice repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 03/02/2020 17:40:59
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_accountreceivableinvoice;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_accountreceivableinvoiceRepository : BaseDNetRepository<VWI_CRM_xts_accountreceivableinvoice>, IVWI_CRM_xts_accountreceivableinvoiceRepository<VWI_CRM_xts_accountreceivableinvoice, int>
    {
        #region Constructor
        public VWI_CRM_xts_accountreceivableinvoiceRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_accountreceivableinvoice
        /// <summary>
        /// Create VWI_CRM_xts_accountreceivableinvoice
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_accountreceivableinvoice entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_accountreceivableinvoice
        /// <summary>
        /// Update VWI_CRM_xts_accountreceivableinvoice
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_accountreceivableinvoice entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_accountreceivableinvoice
        /// <summary>
        /// Delete VWI_CRM_xts_accountreceivableinvoice
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_xts_accountreceivableinvoice By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_accountreceivableinvoice Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_accountreceivableinvoice>(
                        VWI_CRM_xts_accountreceivableinvoiceQuery.GetVWI_CRM_xts_accountreceivableinvoiceById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_accountreceivableinvoice
        /// <summary>
        /// Get All VWI_CRM_xts_accountreceivableinvoice
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_accountreceivableinvoice> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_accountreceivableinvoice
        public List<VWI_CRM_xts_accountreceivableinvoice> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_accountreceivableinvoice>();
        }
        #endregion

		#region Search VWI_CRM_xts_accountreceivableinvoice        
        public new List<VWI_CRM_xts_accountreceivableinvoice> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_accountreceivableinvoice.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xts_accountreceivableinvoice> result = SearchData<VWI_CRM_xts_accountreceivableinvoice>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_accountreceivableinvoice>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_accountreceivableinvoiceQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_accountreceivableinvoice.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_accountreceivableinvoiceQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_accountreceivableinvoice>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_accountreceivableinvoice vWI_CRM_xts_accountreceivableinvoice)
        {
            //vWI_CRM_xts_accountreceivableinvoice.CreatedBy = UserLogin;
            //vWI_CRM_xts_accountreceivableinvoice.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_accountreceivableinvoice);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_accountreceivableinvoice vWI_CRM_xts_accountreceivableinvoice)
        {
            //vWI_CRM_xts_accountreceivableinvoice.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_accountreceivableinvoice.LastUpdateTime = DateTime.Now;
        }
    }
}