#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_customerclass repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_customerclass;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_customerclassRepository : BaseDNetRepository<VWI_CRM_xts_customerclass>, IVWI_CRM_xts_customerclassRepository<VWI_CRM_xts_customerclass, int>
    {
        #region Constructor
        public VWI_CRM_xts_customerclassRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_customerclass
        /// <summary>
        /// Create VWI_CRM_xts_customerclass
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_customerclass entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_customerclass
        /// <summary>
        /// Update VWI_CRM_xts_customerclass
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_customerclass entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_customerclass
        /// <summary>
        /// Delete VWI_CRM_xts_customerclass
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_xts_customerclass By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_customerclass Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_customerclass>(
                        VWI_CRM_xts_customerclassQuery.GetVWI_CRM_xts_customerclassById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_customerclass
        /// <summary>
        /// Get All VWI_CRM_xts_customerclass
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_customerclass> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_customerclass
        public List<VWI_CRM_xts_customerclass> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_customerclass>();
        }
        #endregion

		#region Search VWI_CRM_xts_customerclass        
        public new List<VWI_CRM_xts_customerclass> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_customerclass.rowstatus", "isnull(vwi_crm_xts_customerclass.rowstatus, 0)");

                List<VWI_CRM_xts_customerclass> result = SearchData<VWI_CRM_xts_customerclass>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_customerclass>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_customerclassQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_customerclass.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_customerclassQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_customerclass>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_customerclass vWI_CRM_xts_customerclass)
        {
            //vWI_CRM_xts_customerclass.CreatedBy = UserLogin;
            //vWI_CRM_xts_customerclass.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_customerclass);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_customerclass vWI_CRM_xts_customerclass)
        {
            //vWI_CRM_xts_customerclass.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_customerclass.LastUpdateTime = DateTime.Now;
        }
    }
}