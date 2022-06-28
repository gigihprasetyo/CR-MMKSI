#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_servicetemplateproductnongenuine repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/01/2021 16:00:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_ktb_servicetemplateproductnongenuine;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_ktb_servicetemplateproductnongenuineRepository : BaseDNetRepository<VWI_CRM_ktb_servicetemplateproductnongenuine>, IVWI_CRM_ktb_servicetemplateproductnongenuineRepository<VWI_CRM_ktb_servicetemplateproductnongenuine, int>
    {
        #region Constructor
        public VWI_CRM_ktb_servicetemplateproductnongenuineRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_ktb_servicetemplateproductnongenuine
        /// <summary>
        /// Create VWI_CRM_ktb_servicetemplateproductnongenuine
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_ktb_servicetemplateproductnongenuine entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_ktb_servicetemplateproductnongenuine
        /// <summary>
        /// Update VWI_CRM_ktb_servicetemplateproductnongenuine
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_ktb_servicetemplateproductnongenuine entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_ktb_servicetemplateproductnongenuine
        /// <summary>
        /// Delete VWI_CRM_ktb_servicetemplateproductnongenuine
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_ktb_servicetemplateproductnongenuine By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_ktb_servicetemplateproductnongenuine Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_ktb_servicetemplateproductnongenuine>(
                        VWI_CRM_ktb_servicetemplateproductnongenuineQuery.GetVWI_CRM_ktb_servicetemplateproductnongenuineById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_ktb_servicetemplateproductnongenuine
        /// <summary>
        /// Get All VWI_CRM_ktb_servicetemplateproductnongenuine
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_ktb_servicetemplateproductnongenuine> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_ktb_servicetemplateproductnongenuine
        public List<VWI_CRM_ktb_servicetemplateproductnongenuine> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_ktb_servicetemplateproductnongenuine>();
        }
        #endregion

        #region Search VWI_CRM_ktb_servicetemplateproductnongenuine        
        public new List<VWI_CRM_ktb_servicetemplateproductnongenuine> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_ktb_servicetemplateproductnongenuine.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_ktb_servicetemplateproductnongenuine> result = SearchData<VWI_CRM_ktb_servicetemplateproductnongenuine>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_ktb_servicetemplateproductnongenuine>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_ktb_servicetemplateproductnongenuineQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_ktb_servicetemplateproductnongenuine.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_ktb_servicetemplateproductnongenuineQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_ktb_servicetemplateproductnongenuine>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_ktb_servicetemplateproductnongenuine vWI_CRM_ktb_servicetemplateproductnongenuine)
        {
            //vWI_CRM_ktb_servicetemplateproductnongenuine.CreatedBy = UserLogin;
            //vWI_CRM_ktb_servicetemplateproductnongenuine.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_ktb_servicetemplateproductnongenuine);
        }

        protected void SetLastModifiedLog(VWI_CRM_ktb_servicetemplateproductnongenuine vWI_CRM_ktb_servicetemplateproductnongenuine)
        {
            //vWI_CRM_ktb_servicetemplateproductnongenuine.LastUpdateBy = UserLogin;
            //vWI_CRM_ktb_servicetemplateproductnongenuine.LastUpdateTime = DateTime.Now;
        }
    }
}