#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_jobpositionRepository repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/29/2020 09:13:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_ktb_jobposition;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_ktb_jobpositionRepository : BaseDNetRepository<VWI_CRM_ktb_jobposition>, IVWI_CRM_ktb_jobpositionRepository<VWI_CRM_ktb_jobposition, int>
    {
        #region Constructor
        public VWI_CRM_ktb_jobpositionRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_ktb_jobposition
        /// <summary>
        /// Create VWI_CRM_ktb_jobposition
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_ktb_jobposition entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_ktb_jobposition
        /// <summary>
        /// Update VWI_CRM_ktb_jobposition
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_ktb_jobposition entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_ktb_jobposition
        /// <summary>
        /// Delete VWI_CRM_ktb_jobposition
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_ktb_jobposition By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_ktb_jobposition Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_ktb_jobposition>(
                        VWI_CRM_ktb_jobpositionQuery.GetVWI_CRM_ktb_jobpositionById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_ktb_jobposition
        /// <summary>
        /// Get All VWI_CRM_ktb_jobposition
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_ktb_jobposition> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_ktb_jobposition
        public List<VWI_CRM_ktb_jobposition> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_ktb_jobposition>();
        }
        #endregion

        #region Search VWI_CRM_ktb_jobposition        
        public new List<VWI_CRM_ktb_jobposition> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_ktb_jobposition.rowstatus", "isnull(vwi_crm_ktb_jobposition.rowstatus, 0)");

                List<VWI_CRM_ktb_jobposition> result = SearchData<VWI_CRM_ktb_jobposition>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_ktb_jobposition>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_ktb_jobpositionQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_ktb_jobposition.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_ktb_jobpositionQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_ktb_jobposition>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_ktb_jobposition vWI_CRM_ktb_jobposition)
        {
            //vWI_CRM_ktb_jobposition.CreatedBy = UserLogin;
            //vWI_CRM_ktb_jobposition.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_ktb_jobposition);
        }

        protected void SetLastModifiedLog(VWI_CRM_ktb_jobposition vWI_CRM_ktb_jobposition)
        {
            //vWI_CRM_ktb_jobposition.LastUpdateBy = UserLogin;
            //vWI_CRM_ktb_jobposition.LastUpdateTime = DateTime.Now;
        }
    }
}
