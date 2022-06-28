#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_uvsoreferralinformation repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-05 09:02:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_uvsoreferralinformation;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_uvsoreferralinformationRepository : BaseDNetRepository<VWI_CRM_xts_uvsoreferralinformation>, IVWI_CRM_xts_uvsoreferralinformationRepository<VWI_CRM_xts_uvsoreferralinformation, int>
    {
        #region Constructor
        public VWI_CRM_xts_uvsoreferralinformationRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_uvsoreferralinformation
        /// <summary>
        /// Create VWI_CRM_xts_uvsoreferralinformation
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_uvsoreferralinformation entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_uvsoreferralinformation
        /// <summary>
        /// Update VWI_CRM_xts_uvsoreferralinformation
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_uvsoreferralinformation entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_uvsoreferralinformation
        /// <summary>
        /// Delete VWI_CRM_xts_uvsoreferralinformation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_uvsoreferralinformation By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_uvsoreferralinformation Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_uvsoreferralinformation>(
                        VWI_CRM_xts_uvsoreferralinformationQuery.GetVWI_CRM_xts_uvsoreferralinformationById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_uvsoreferralinformation
        /// <summary>
        /// Get All VWI_CRM_xts_uvsoreferralinformation
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_uvsoreferralinformation> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_uvsoreferralinformation
        public List<VWI_CRM_xts_uvsoreferralinformation> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_uvsoreferralinformation>();
        }
        #endregion

        #region Search VWI_CRM_xts_uvsoreferralinformation        
        public new List<VWI_CRM_xts_uvsoreferralinformation> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_uvsoreferralinformation.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xts_uvsoreferralinformation> result = SearchData<VWI_CRM_xts_uvsoreferralinformation>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_uvsoreferralinformation>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_uvsoreferralinformationQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_uvsoreferralinformation.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_uvsoreferralinformationQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_uvsoreferralinformation>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_uvsoreferralinformation vWI_CRM_xts_uvsoreferralinformation)
        {
            //vWI_CRM_xts_uvsoreferralinformation.CreatedBy = UserLogin;
            //vWI_CRM_xts_uvsoreferralinformation.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_uvsoreferralinformation);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_uvsoreferralinformation vWI_CRM_xts_uvsoreferralinformation)
        {
            //vWI_CRM_xts_uvsoreferralinformation.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_uvsoreferralinformation.LastUpdateTime = DateTime.Now;
        }
    }
}