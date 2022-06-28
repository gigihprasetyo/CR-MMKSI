#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : CRM_ktb_openfacture repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 17/02/2021 11:49:03
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.CRM_ktb_openfacture;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class CRM_ktb_openfactureRepository : BaseDNetRepository<VWI_CRM_ktb_openfacture>, ICRM_ktb_openfactureRepository<VWI_CRM_ktb_openfacture, int>
    {
        #region Constructor
        public CRM_ktb_openfactureRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create CRM_ktb_openfacture
        /// <summary>
        /// Create CRM_ktb_openfacture
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_ktb_openfacture entity)
        {
            return null;
        }
        #endregion

        #region Update CRM_ktb_openfacture
        /// <summary>
        /// Update CRM_ktb_openfacture
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_ktb_openfacture entity)
        {
            return null;
        }
        #endregion

        #region Delete CRM_ktb_openfacture
        /// <summary>
        /// Delete CRM_ktb_openfacture
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get CRM_ktb_openfacture By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_ktb_openfacture Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_ktb_openfacture>(
                        CRM_ktb_openfactureQuery.GetCRM_ktb_openfactureById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All CRM_ktb_openfacture
        /// <summary>
        /// Get All CRM_ktb_openfacture
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_ktb_openfacture> GetAll()
        {
            return null;
        }
        #endregion

        #region Search CRM_ktb_openfacture
        public List<VWI_CRM_ktb_openfacture> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_ktb_openfacture>();
        }
        #endregion

		#region Search CRM_ktb_openfacture        
        public new List<VWI_CRM_ktb_openfacture> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_ktb_openfacture.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_ktb_openfacture> result = SearchData<VWI_CRM_ktb_openfacture>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_ktb_openfacture>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(CRM_ktb_openfactureQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_ktb_openfacture.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(CRM_ktb_openfactureQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_ktb_openfacture>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_ktb_openfacture cRM_ktb_openfacture)
        {
            //cRM_ktb_openfacture.CreatedBy = UserLogin;
            //cRM_ktb_openfacture.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(cRM_ktb_openfacture);
        }

        protected void SetLastModifiedLog(VWI_CRM_ktb_openfacture cRM_ktb_openfacture)
        {
            //cRM_ktb_openfacture.LastUpdateBy = UserLogin;
            //cRM_ktb_openfacture.LastUpdateTime = DateTime.Now;
        }
    }
}