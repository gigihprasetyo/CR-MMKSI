#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_manufacturerRepository repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/17/2020 16:11:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_manufacturer;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_manufacturerRepository : BaseDNetRepository<VWI_CRM_xts_manufacturer>, IVWI_CRM_xts_manufacturerRepository<VWI_CRM_xts_manufacturer, int>
    {
        #region Constructor
        public VWI_CRM_xts_manufacturerRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_manufacturer
        /// <summary>
        /// Create VWI_CRM_xts_manufacturer
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_manufacturer entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_manufacturer
        /// <summary>
        /// Update VWI_CRM_xts_manufacturer
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_manufacturer entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_manufacturer
        /// <summary>
        /// Delete VWI_CRM_xts_manufacturer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_manufacturer By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_manufacturer Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_manufacturer>(
                        VWI_CRM_xts_manufacturerQuery.GetVWI_CRM_xts_manufacturerById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_manufacturer
        /// <summary>
        /// Get All VWI_CRM_xts_manufacturer
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_manufacturer> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_manufacturer
        public List<VWI_CRM_xts_manufacturer> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_manufacturer>();
        }
        #endregion

        #region Search VWI_CRM_xts_manufacturer        
        public new List<VWI_CRM_xts_manufacturer> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_manufacturer.rowstatus", "isnull(vwi_crm_xts_manufacturer.rowstatus, 0)");

                List<VWI_CRM_xts_manufacturer> result = SearchData<VWI_CRM_xts_manufacturer>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_manufacturer>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_manufacturerQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_manufacturer.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_manufacturerQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_manufacturer>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_manufacturer vWI_CRM_xts_manufacturer)
        {
            //vWI_CRM_xts_manufacturer.CreatedBy = UserLogin;
            //vWI_CRM_xts_manufacturer.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_manufacturer);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_manufacturer vWI_CRM_xts_manufacturer)
        {
            //vWI_CRM_xts_manufacturer.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_manufacturer.LastUpdateTime = DateTime.Now;
        }
    }
}
