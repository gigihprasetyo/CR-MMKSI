#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_gljournaldetailsRepository repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_gljournaldetails;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_gljournaldetailsRepository : BaseDNetRepository<VWI_CRM_xts_gljournaldetails>, IVWI_CRM_xts_gljournaldetailsRepository<VWI_CRM_xts_gljournaldetails, int>
    {

        #region Constructor
        public VWI_CRM_xts_gljournaldetailsRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_gljournaldetails
        /// <summary>
        /// Create VWI_CRM_xts_gljournaldetails
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_gljournaldetails entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_gljournaldetails
        /// <summary>
        /// Update VWI_CRM_xts_gljournaldetails
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_gljournaldetails entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_gljournaldetails
        /// <summary>
        /// Delete VWI_CRM_xts_gljournaldetails
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_gljournaldetails By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_gljournaldetails Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_gljournaldetails>(
                        VWI_CRM_xts_gljournaldetailsQuery.GetVWI_CRM_xts_gljournaldetailsById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_gljournaldetails
        /// <summary>
        /// Get All VWI_CRM_xts_gljournaldetails
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_gljournaldetails> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_gljournaldetails
        public List<VWI_CRM_xts_gljournaldetails> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_gljournaldetails>();
        }
        #endregion

        #region Search VWI_CRM_xts_gljournaldetails        
        public new List<VWI_CRM_xts_gljournaldetails> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_gljournaldetails.rowstatus", "isnull(vwi_crm_xts_gljournaldetails.rowstatus, 0)");
                
                List<VWI_CRM_xts_gljournaldetails> result = SearchData<VWI_CRM_xts_gljournaldetails>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_gljournaldetails>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_gljournaldetailsQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_gljournaldetails.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_gljournaldetailsQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_gljournaldetails>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_gljournaldetails VWI_CRM_xts_gljournaldetails)
        {
            //VWI_CRM_xts_gljournaldetails.CreatedBy = UserLogin;
            //VWI_CRM_xts_gljournaldetails.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(VWI_CRM_xts_gljournaldetails);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_gljournaldetails VWI_CRM_xts_gljournaldetails)
        {
            //VWI_CRM_xts_gljournaldetails.LastUpdateBy = UserLogin;
            //VWI_CRM_xts_gljournaldetails.LastUpdateTime = DateTime.Now;
        }
    }
}
