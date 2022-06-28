#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xjp_registrationrequest repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xjp_registrationrequest;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xjp_registrationrequestRepository : BaseDNetRepository<VWI_CRM_xjp_registrationrequest>, IVWI_CRM_xjp_registrationrequestRepository<VWI_CRM_xjp_registrationrequest, int>
    {
        #region Constructor
        public VWI_CRM_xjp_registrationrequestRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xjp_registrationrequest
        /// <summary>
        /// Create VWI_CRM_xjp_registrationrequest
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xjp_registrationrequest entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xjp_registrationrequest
        /// <summary>
        /// Update VWI_CRM_xjp_registrationrequest
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xjp_registrationrequest entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xjp_registrationrequest
        /// <summary>
        /// Delete VWI_CRM_xjp_registrationrequest
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xjp_registrationrequest By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xjp_registrationrequest Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xjp_registrationrequest>(
                        VWI_CRM_xjp_registrationrequestQuery.GetVWI_CRM_xjp_registrationrequestById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xjp_registrationrequest
        /// <summary>
        /// Get All VWI_CRM_xjp_registrationrequest
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xjp_registrationrequest> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xjp_registrationrequest
        public List<VWI_CRM_xjp_registrationrequest> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xjp_registrationrequest>();
        }
        #endregion

        #region Search VWI_CRM_xjp_registrationrequest        
        public new List<VWI_CRM_xjp_registrationrequest> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xjp_registrationrequest.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xjp_registrationrequest> result = SearchData<VWI_CRM_xjp_registrationrequest>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xjp_registrationrequest>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xjp_registrationrequestQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xjp_registrationrequest.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xjp_registrationrequestQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xjp_registrationrequest>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xjp_registrationrequest vWI_CRM_xjp_registrationrequest)
        {
            //vWI_CRM_xjp_registrationrequest.CreatedBy = UserLogin;
            //vWI_CRM_xjp_registrationrequest.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xjp_registrationrequest);
        }

        protected void SetLastModifiedLog(VWI_CRM_xjp_registrationrequest vWI_CRM_xjp_registrationrequest)
        {
            //vWI_CRM_xjp_registrationrequest.LastUpdateBy = UserLogin;
            //vWI_CRM_xjp_registrationrequest.LastUpdateTime = DateTime.Now;
        }
    }
}