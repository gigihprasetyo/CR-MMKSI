#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_vendorclass repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_vendorclass;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_vendorclassRepository : BaseDNetRepository<VWI_CRM_xts_vendorclass>, IVWI_CRM_xts_vendorclassRepository<VWI_CRM_xts_vendorclass, int>
    {
        #region Constructor
        public VWI_CRM_xts_vendorclassRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_vendorclass
        /// <summary>
        /// Create VWI_CRM_xts_vendorclass
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_vendorclass entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_vendorclass
        /// <summary>
        /// Update VWI_CRM_xts_vendorclass
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_vendorclass entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_vendorclass
        /// <summary>
        /// Delete VWI_CRM_xts_vendorclass
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_xts_vendorclass By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_vendorclass Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_vendorclass>(
                        VWI_CRM_xts_vendorclassQuery.GetVWI_CRM_xts_vendorclassById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_vendorclass
        /// <summary>
        /// Get All VWI_CRM_xts_vendorclass
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_vendorclass> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_vendorclass
        public List<VWI_CRM_xts_vendorclass> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_vendorclass>();
        }
        #endregion

		#region Search VWI_CRM_xts_vendorclass        
        public new List<VWI_CRM_xts_vendorclass> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_vendorclass.rowstatus", "isnull(vwi_crm_xts_vendorclass.rowstatus, 0)");

                List<VWI_CRM_xts_vendorclass> result = SearchData<VWI_CRM_xts_vendorclass>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_vendorclass>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_vendorclassQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_vendorclass.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_vendorclassQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_vendorclass>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_vendorclass vWI_CRM_xts_vendorclass)
        {
            //vWI_CRM_xts_vendorclass.CreatedBy = UserLogin;
            //vWI_CRM_xts_vendorclass.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_vendorclass);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_vendorclass vWI_CRM_xts_vendorclass)
        {
            //vWI_CRM_xts_vendorclass.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_vendorclass.LastUpdateTime = DateTime.Now;
        }
    }
}