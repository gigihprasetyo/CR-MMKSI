#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_pricelevel repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_pricelevel;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_pricelevelRepository : BaseDNetRepository<VWI_CRM_pricelevel>, IVWI_CRM_pricelevelRepository<VWI_CRM_pricelevel, int>
    {
        #region Constructor
        public VWI_CRM_pricelevelRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_pricelevel
        /// <summary>
        /// Create VWI_CRM_pricelevel
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_pricelevel entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_pricelevel
        /// <summary>
        /// Update VWI_CRM_pricelevel
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_pricelevel entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_pricelevel
        /// <summary>
        /// Delete VWI_CRM_pricelevel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_pricelevel By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_pricelevel Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_pricelevel>(
                        VWI_CRM_pricelevelQuery.GetVWI_CRM_pricelevelById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_pricelevel
        /// <summary>
        /// Get All VWI_CRM_pricelevel
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_pricelevel> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_pricelevel
        public List<VWI_CRM_pricelevel> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_pricelevel>();
        }
        #endregion

        #region Search VWI_CRM_pricelevel        
        public new List<VWI_CRM_pricelevel> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_pricelevel.rowstatus", "isnull(vwi_crm_pricelevel.rowstatus, 0)");

                List<VWI_CRM_pricelevel> result = SearchData<VWI_CRM_pricelevel>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_pricelevel>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_pricelevelQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_pricelevel.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_pricelevelQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_pricelevel>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_pricelevel vWI_CRM_pricelevel)
        {
            //vWI_CRM_pricelevel.CreatedBy = UserLogin;
            //vWI_CRM_pricelevel.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_pricelevel);
        }

        protected void SetLastModifiedLog(VWI_CRM_pricelevel vWI_CRM_pricelevel)
        {
            //vWI_CRM_pricelevel.LastUpdateBy = UserLogin;
            //vWI_CRM_pricelevel.LastUpdateTime = DateTime.Now;
        }
    }
}