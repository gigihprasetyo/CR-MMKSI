#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_productcrossreference repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/18/2020 10:16:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_productcrossreference;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_productcrossreferenceRepository : BaseDNetRepository<VWI_CRM_xts_productcrossreference>, IVWI_CRM_xts_productcrossreferenceRepository<VWI_CRM_xts_productcrossreference, int>
    {
        #region Constructor
        public VWI_CRM_xts_productcrossreferenceRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_productcrossreference
        /// <summary>
        /// Create VWI_CRM_xts_productcrossreference
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_productcrossreference entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_productcrossreference
        /// <summary>
        /// Update VWI_CRM_xts_productcrossreference
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_productcrossreference entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_productcrossreference
        /// <summary>
        /// Delete VWI_CRM_xts_productcrossreference
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_productcrossreference By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_productcrossreference Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_productcrossreference>(
                        VWI_CRM_xts_productcrossreferenceQuery.GetVWI_CRM_xts_productcrossreferenceById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_productcrossreference
        /// <summary>
        /// Get All VWI_CRM_xts_productcrossreference
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_productcrossreference> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_productcrossreference
        public List<VWI_CRM_xts_productcrossreference> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_productcrossreference>();
        }
        #endregion

        #region Search VWI_CRM_xts_productcrossreference        
        public new List<VWI_CRM_xts_productcrossreference> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_productcrossreference.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xts_productcrossreference> result = SearchData<VWI_CRM_xts_productcrossreference>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_productcrossreference>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_productcrossreferenceQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_productcrossreference.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_productcrossreferenceQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_productcrossreference>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_productcrossreference vWI_CRM_xts_productcrossreference)
        {
            //vWI_CRM_xts_productcrossreference.CreatedBy = UserLogin;
            //vWI_CRM_xts_productcrossreference.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_productcrossreference);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_productcrossreference vWI_CRM_xts_productcrossreference)
        {
            //vWI_CRM_xts_productcrossreference.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_productcrossreference.LastUpdateTime = DateTime.Now;
        }
    }
}