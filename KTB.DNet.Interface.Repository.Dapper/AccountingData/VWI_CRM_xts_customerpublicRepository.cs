#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_customerpublic repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_customerpublic;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_customerpublicRepository : BaseDNetRepository<VWI_CRM_xts_customerpublic>, IVWI_CRM_xts_customerpublicRepository<VWI_CRM_xts_customerpublic, int>
    {
        #region Constructor
        public VWI_CRM_xts_customerpublicRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_customerpublic
        /// <summary>
        /// Create VWI_CRM_xts_customerpublic
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_customerpublic entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_customerpublic
        /// <summary>
        /// Update VWI_CRM_xts_customerpublic
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_customerpublic entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_customerpublic
        /// <summary>
        /// Delete VWI_CRM_xts_customerpublic
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_xts_customerpublic By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_customerpublic Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_customerpublic>(
                        VWI_CRM_xts_customerpublicQuery.GetVWI_CRM_xts_customerpublicById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_customerpublic
        /// <summary>
        /// Get All VWI_CRM_xts_customerpublic
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_customerpublic> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_customerpublic
        public List<VWI_CRM_xts_customerpublic> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_customerpublic>();
        }
        #endregion

		#region Search VWI_CRM_xts_customerpublic        
        public new List<VWI_CRM_xts_customerpublic> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_customerpublic.rowstatus", "isnull(vwi_crm_xts_customerpublic.rowstatus, 0)");

                List<VWI_CRM_xts_customerpublic> result = SearchData<VWI_CRM_xts_customerpublic>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_customerpublic>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_customerpublicQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_customerpublic.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_customerpublicQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_customerpublic>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_customerpublic vWI_CRM_xts_customerpublic)
        {
            //vWI_CRM_xts_customerpublic.CreatedBy = UserLogin;
            //vWI_CRM_xts_customerpublic.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_customerpublic);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_customerpublic vWI_CRM_xts_customerpublic)
        {
            //vWI_CRM_xts_customerpublic.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_customerpublic.LastUpdateTime = DateTime.Now;
        }
    }
}