#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_productvariant repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_productvariant;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_productvariantRepository : BaseDNetRepository<VWI_CRM_xts_productvariant>, IVWI_CRM_xts_productvariantRepository<VWI_CRM_xts_productvariant, int>
    {
        #region Constructor
        public VWI_CRM_xts_productvariantRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_productvariant
        /// <summary>
        /// Create VWI_CRM_xts_productvariant
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_productvariant entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_productvariant
        /// <summary>
        /// Update VWI_CRM_xts_productvariant
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_productvariant entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_productvariant
        /// <summary>
        /// Delete VWI_CRM_xts_productvariant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_productvariant By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_productvariant Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_productvariant>(
                        VWI_CRM_xts_productvariantQuery.GetVWI_CRM_xts_productvariantById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_productvariant
        /// <summary>
        /// Get All VWI_CRM_xts_productvariant
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_productvariant> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_productvariant
        public List<VWI_CRM_xts_productvariant> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_productvariant>();
        }
        #endregion

        #region Search VWI_CRM_xts_productvariant        
        public new List<VWI_CRM_xts_productvariant> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_productvariant.rowstatus", "isnull(vwi_crm_xts_productvariant.rowstatus, 0)");

                List<VWI_CRM_xts_productvariant> result = SearchData<VWI_CRM_xts_productvariant>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_productvariant>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_productvariantQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_productvariant.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_productvariantQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_productvariant>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_productvariant vWI_CRM_xts_productvariant)
        {
            //vWI_CRM_xts_productvariant.CreatedBy = UserLogin;
            //vWI_CRM_xts_productvariant.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_productvariant);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_productvariant vWI_CRM_xts_productvariant)
        {
            //vWI_CRM_xts_productvariant.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_productvariant.LastUpdateTime = DateTime.Now;
        }
    }
}