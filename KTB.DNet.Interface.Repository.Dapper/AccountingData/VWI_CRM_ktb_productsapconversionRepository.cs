#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_productsapconversion repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/12/2020 17:06:21
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_ktb_productsapconversion;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_ktb_productsapconversionRepository : BaseDNetRepository<VWI_CRM_ktb_productsapconversion>, IVWI_CRM_ktb_productsapconversionRepository<VWI_CRM_ktb_productsapconversion, int>
    {
        #region Constructor
        public VWI_CRM_ktb_productsapconversionRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_ktb_productsapconversion
        /// <summary>
        /// Create VWI_CRM_ktb_productsapconversion
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_ktb_productsapconversion entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_ktb_productsapconversion
        /// <summary>
        /// Update VWI_CRM_ktb_productsapconversion
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_ktb_productsapconversion entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_ktb_productsapconversion
        /// <summary>
        /// Delete VWI_CRM_ktb_productsapconversion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_ktb_productsapconversion By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_ktb_productsapconversion Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_ktb_productsapconversion>(
                        VWI_CRM_ktb_productsapconversionQuery.GetVWI_CRM_ktb_productsapconversionById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_ktb_productsapconversion
        /// <summary>
        /// Get All VWI_CRM_ktb_productsapconversion
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_ktb_productsapconversion> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_ktb_productsapconversion
        public List<VWI_CRM_ktb_productsapconversion> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_ktb_productsapconversion>();
        }
        #endregion

		#region Search VWI_CRM_ktb_productsapconversion        
        public new List<VWI_CRM_ktb_productsapconversion> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_ktb_productsapconversion.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_ktb_productsapconversion> result = SearchData<VWI_CRM_ktb_productsapconversion>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_ktb_productsapconversion>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_ktb_productsapconversionQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_ktb_productsapconversion.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_ktb_productsapconversionQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_ktb_productsapconversion>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_ktb_productsapconversion vWI_CRM_ktb_productsapconversion)
        {
            //vWI_CRM_ktb_productsapconversion.CreatedBy = UserLogin;
            //vWI_CRM_ktb_productsapconversion.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_ktb_productsapconversion);
        }

        protected void SetLastModifiedLog(VWI_CRM_ktb_productsapconversion vWI_CRM_ktb_productsapconversion)
        {
            //vWI_CRM_ktb_productsapconversion.LastUpdateBy = UserLogin;
            //vWI_CRM_ktb_productsapconversion.LastUpdateTime = DateTime.Now;
        }
    }
}