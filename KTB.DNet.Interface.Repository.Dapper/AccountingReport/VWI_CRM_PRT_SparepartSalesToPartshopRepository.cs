#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_SparepartSalesToPartshop repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 03/02/2020 15:09:45
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_CRM_PRT_SparepartSalesToPartshop;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_PRT_SparepartSalesToPartshopRepository : BaseDNetRepository<VWI_CRM_PRT_SparepartSalesToPartshop>, IVWI_CRM_PRT_SparepartSalesToPartshopRepository<VWI_CRM_PRT_SparepartSalesToPartshop, int>
    {
        #region Constructor
        public VWI_CRM_PRT_SparepartSalesToPartshopRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_PRT_SparepartSalesToPartshop
        /// <summary>
        /// Create VWI_CRM_PRT_SparepartSalesToPartshop
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_PRT_SparepartSalesToPartshop entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_PRT_SparepartSalesToPartshop
        /// <summary>
        /// Update VWI_CRM_PRT_SparepartSalesToPartshop
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_PRT_SparepartSalesToPartshop entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_PRT_SparepartSalesToPartshop
        /// <summary>
        /// Delete VWI_CRM_PRT_SparepartSalesToPartshop
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_PRT_SparepartSalesToPartshop By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_PRT_SparepartSalesToPartshop Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_PRT_SparepartSalesToPartshop>(
                        VWI_CRM_PRT_SparepartSalesToPartshopQuery.GetVWI_CRM_PRT_SparepartSalesToPartshopById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_PRT_SparepartSalesToPartshop
        /// <summary>
        /// Get All VWI_CRM_PRT_SparepartSalesToPartshop
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_PRT_SparepartSalesToPartshop> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_PRT_SparepartSalesToPartshop
        public List<VWI_CRM_PRT_SparepartSalesToPartshop> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_PRT_SparepartSalesToPartshop>();
        }
        #endregion

		#region Search VWI_CRM_PRT_SparepartSalesToPartshop        
        public new List<VWI_CRM_PRT_SparepartSalesToPartshop> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_PRT_SparepartSalesToPartshop> result = Search<VWI_CRM_PRT_SparepartSalesToPartshop>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_PRT_SparepartSalesToPartshop>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_PRT_SparepartSalesToPartshopQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_PRT_SparepartSalesToPartshop.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_PRT_SparepartSalesToPartshopQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_PRT_SparepartSalesToPartshop>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_PRT_SparepartSalesToPartshop vWI_CRM_PRT_SparepartSalesToPartshop)
        {
            //vWI_CRM_PRT_SparepartSalesToPartshop.CreatedBy = UserLogin;
            //vWI_CRM_PRT_SparepartSalesToPartshop.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_PRT_SparepartSalesToPartshop);
        }

        protected void SetLastModifiedLog(VWI_CRM_PRT_SparepartSalesToPartshop vWI_CRM_PRT_SparepartSalesToPartshop)
        {
            //vWI_CRM_PRT_SparepartSalesToPartshop.LastUpdateBy = UserLogin;
            //vWI_CRM_PRT_SparepartSalesToPartshop.LastUpdateTime = DateTime.Now;
        }
    }
}