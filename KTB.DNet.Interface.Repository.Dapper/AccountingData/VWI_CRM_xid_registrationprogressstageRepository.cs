#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xid_registrationprogressstageRepository repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-04 08:45:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xid_registrationprogressstage;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xid_registrationprogressstageRepository : BaseDNetRepository<VWI_CRM_xid_registrationprogressstage>, IVWI_CRM_xid_registrationprogressstageRepository<VWI_CRM_xid_registrationprogressstage, int>
    {
        #region Constructor
        public VWI_CRM_xid_registrationprogressstageRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xid_registrationprogressstage
        /// <summary>
        /// Create VWI_CRM_xid_registrationprogressstage
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xid_registrationprogressstage entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xid_registrationprogressstage
        /// <summary>
        /// Update VWI_CRM_xid_registrationprogressstage
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xid_registrationprogressstage entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xid_registrationprogressstage
        /// <summary>
        /// Delete VWI_CRM_xid_registrationprogressstage
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xid_registrationprogressstage By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xid_registrationprogressstage Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xid_registrationprogressstage>(
                        VWI_CRM_xid_registrationprogressstageQuery.GetVWI_CRM_xid_registrationprogressstageById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xid_registrationprogressstage
        /// <summary>
        /// Get All VWI_CRM_xid_registrationprogressstage
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xid_registrationprogressstage> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xid_registrationprogressstage
        public List<VWI_CRM_xid_registrationprogressstage> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xid_registrationprogressstage>();
        }
        #endregion

        #region Search VWI_CRM_xid_registrationprogressstage        
        public new List<VWI_CRM_xid_registrationprogressstage> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xid_registrationprogressstage.rowstatus", "isnull(vwi_crm_xid_registrationprogressstage.rowstatus, 0)");

                List<VWI_CRM_xid_registrationprogressstage> result = SearchData<VWI_CRM_xid_registrationprogressstage>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xid_registrationprogressstage>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xid_registrationprogressstageQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xid_registrationprogressstage.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xid_registrationprogressstageQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xid_registrationprogressstage>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xid_registrationprogressstage vWI_CRM_xid_registrationprogressstage)
        {
            //vWI_CRM_xid_registrationprogressstage.CreatedBy = UserLogin;
            //vWI_CRM_xid_registrationprogressstage.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xid_registrationprogressstage);
        }

        protected void SetLastModifiedLog(VWI_CRM_xid_registrationprogressstage vWI_CRM_xid_registrationprogressstage)
        {
            //vWI_CRM_xid_registrationprogressstage.LastUpdateBy = UserLogin;
            //vWI_CRM_xid_registrationprogressstage.LastUpdateTime = DateTime.Now;
        }
    }
}
