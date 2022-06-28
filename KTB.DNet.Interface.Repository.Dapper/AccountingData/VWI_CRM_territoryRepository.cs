#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_territoryRepository repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-04 09:08:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_territory;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_territoryRepository : BaseDNetRepository<VWI_CRM_territory>, IVWI_CRM_territoryRepository<VWI_CRM_territory, int>
    {
        #region Constructor
        public VWI_CRM_territoryRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_territory
        /// <summary>
        /// Create VWI_CRM_territory
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_territory entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_territory
        /// <summary>
        /// Update VWI_CRM_territory
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_territory entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_territory
        /// <summary>
        /// Delete VWI_CRM_territory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_territory By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_territory Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_territory>(
                        VWI_CRM_territoryQuery.GetVWI_CRM_territoryById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_territory
        /// <summary>
        /// Get All VWI_CRM_territory
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_territory> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_territory
        public List<VWI_CRM_territory> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_territory>();
        }
        #endregion

        #region Search VWI_CRM_territory        
        public new List<VWI_CRM_territory> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_territory.rowstatus", "isnull(vwi_crm_territory.rowstatus, 0)");

                List<VWI_CRM_territory> result = SearchData<VWI_CRM_territory>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_territory>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_territoryQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_territory.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_territoryQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_territory>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_territory vWI_CRM_territory)
        {
            //vWI_CRM_territory.CreatedBy = UserLogin;
            //vWI_CRM_territory.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_territory);
        }

        protected void SetLastModifiedLog(VWI_CRM_territory vWI_CRM_territory)
        {
            //vWI_CRM_territory.LastUpdateBy = UserLogin;
            //vWI_CRM_territory.LastUpdateTime = DateTime.Now;
        }
    }
}
