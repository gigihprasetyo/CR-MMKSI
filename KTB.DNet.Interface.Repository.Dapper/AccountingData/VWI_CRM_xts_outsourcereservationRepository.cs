#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_outsourcereservation repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/28/2020 08:21:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_outsourcereservation;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_outsourcereservationRepository : BaseDNetRepository<VWI_CRM_xts_outsourcereservation>, IVWI_CRM_xts_outsourcereservationRepository<VWI_CRM_xts_outsourcereservation, int>
    {
        #region Constructor
        public VWI_CRM_xts_outsourcereservationRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_outsourcereservation
        /// <summary>
        /// Create VWI_CRM_xts_outsourcereservation
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_outsourcereservation entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_outsourcereservation
        /// <summary>
        /// Update VWI_CRM_xts_outsourcereservation
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_outsourcereservation entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_outsourcereservation
        /// <summary>
        /// Delete VWI_CRM_xts_outsourcereservation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_outsourcereservation By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_outsourcereservation Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_outsourcereservation>(
                        VWI_CRM_xts_outsourcereservationQuery.GetVWI_CRM_xts_outsourcereservationById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_outsourcereservation
        /// <summary>
        /// Get All VWI_CRM_xts_outsourcereservation
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_outsourcereservation> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_outsourcereservation
        public List<VWI_CRM_xts_outsourcereservation> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_outsourcereservation>();
        }
        #endregion

        #region Search VWI_CRM_xts_outsourcereservation        
        public new List<VWI_CRM_xts_outsourcereservation> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_outsourcereservation.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xts_outsourcereservation> result = SearchData<VWI_CRM_xts_outsourcereservation>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_outsourcereservation>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_outsourcereservationQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_outsourcereservation.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_outsourcereservationQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_outsourcereservation>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_outsourcereservation vWI_CRM_xts_outsourcereservation)
        {
            //vWI_CRM_xts_outsourcereservation.CreatedBy = UserLogin;
            //vWI_CRM_xts_outsourcereservation.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_outsourcereservation);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_outsourcereservation vWI_CRM_xts_outsourcereservation)
        {
            //vWI_CRM_xts_outsourcereservation.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_outsourcereservation.LastUpdateTime = DateTime.Now;
        }
    }
}