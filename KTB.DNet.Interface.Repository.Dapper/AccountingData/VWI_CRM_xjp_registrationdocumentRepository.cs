#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xjp_registrationdocument repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/24/2020 09:26:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xjp_registrationdocument;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xjp_registrationdocumentRepository : BaseDNetRepository<VWI_CRM_xjp_registrationdocument>, IVWI_CRM_xjp_registrationdocumentRepository<VWI_CRM_xjp_registrationdocument, int>
    {
        #region Constructor
        public VWI_CRM_xjp_registrationdocumentRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xjp_registrationdocument
        /// <summary>
        /// Create VWI_CRM_xjp_registrationdocument
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xjp_registrationdocument entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xjp_registrationdocument
        /// <summary>
        /// Update VWI_CRM_xjp_registrationdocument
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xjp_registrationdocument entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xjp_registrationdocument
        /// <summary>
        /// Delete VWI_CRM_xjp_registrationdocument
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xjp_registrationdocument By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xjp_registrationdocument Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xjp_registrationdocument>(
                        VWI_CRM_xjp_registrationdocumentQuery.GetVWI_CRM_xjp_registrationdocumentById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xjp_registrationdocument
        /// <summary>
        /// Get All VWI_CRM_xjp_registrationdocument
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xjp_registrationdocument> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xjp_registrationdocument
        public List<VWI_CRM_xjp_registrationdocument> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xjp_registrationdocument>();
        }
        #endregion

        #region Search VWI_CRM_xjp_registrationdocument        
        public new List<VWI_CRM_xjp_registrationdocument> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xjp_registrationdocument.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xjp_registrationdocument> result = SearchData<VWI_CRM_xjp_registrationdocument>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xjp_registrationdocument>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xjp_registrationdocumentQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xjp_registrationdocument.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xjp_registrationdocumentQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xjp_registrationdocument>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xjp_registrationdocument vWI_CRM_xjp_registrationdocument)
        {
            //vWI_CRM_xjp_registrationdocument.CreatedBy = UserLogin;
            //vWI_CRM_xjp_registrationdocument.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xjp_registrationdocument);
        }

        protected void SetLastModifiedLog(VWI_CRM_xjp_registrationdocument vWI_CRM_xjp_registrationdocument)
        {
            //vWI_CRM_xjp_registrationdocument.LastUpdateBy = UserLogin;
            //vWI_CRM_xjp_registrationdocument.LastUpdateTime = DateTime.Now;
        }
    }
}