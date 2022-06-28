#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SLS_VehicleStock repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_CRM_SLS_VehicleStock;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_SLS_VehicleStockRepository : BaseDNetRepository<VWI_CRM_SLS_VehicleStock>, IVWI_CRM_SLS_VehicleStockRepository<VWI_CRM_SLS_VehicleStock, int>
    {
        #region Constructor
        public VWI_CRM_SLS_VehicleStockRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_SLS_VehicleStock
        /// <summary>
        /// Create VWI_CRM_SLS_VehicleStock
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_SLS_VehicleStock entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_SLS_VehicleStock
        /// <summary>
        /// Update VWI_CRM_SLS_VehicleStock
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_SLS_VehicleStock entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_SLS_VehicleStock
        /// <summary>
        /// Delete VWI_CRM_SLS_VehicleStock
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_SLS_VehicleStock By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_SLS_VehicleStock Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_SLS_VehicleStock>(
                        VWI_CRM_SLS_VehicleStockQuery.GetVWI_CRM_SLS_VehicleStockById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_SLS_VehicleStock
        /// <summary>
        /// Get All VWI_CRM_SLS_VehicleStock
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_SLS_VehicleStock> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_SLS_VehicleStock
        public List<VWI_CRM_SLS_VehicleStock> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_SLS_VehicleStock>();
        }
        #endregion

		#region Search VWI_CRM_SLS_VehicleStock        
        public new List<VWI_CRM_SLS_VehicleStock> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_SLS_VehicleStock> result = Search<VWI_CRM_SLS_VehicleStock>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_SLS_VehicleStock>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_SLS_VehicleStockQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_SLS_VehicleStock.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_SLS_VehicleStockQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_SLS_VehicleStock>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_SLS_VehicleStock vWI_CRM_SLS_VehicleStock)
        {
            //vWI_CRM_SLS_VehicleStock.CreatedBy = UserLogin;
            //vWI_CRM_SLS_VehicleStock.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_SLS_VehicleStock);
        }

        protected void SetLastModifiedLog(VWI_CRM_SLS_VehicleStock vWI_CRM_SLS_VehicleStock)
        {
            //vWI_CRM_SLS_VehicleStock.LastUpdateBy = UserLogin;
            //vWI_CRM_SLS_VehicleStock.LastUpdateTime = DateTime.Now;
        }
    }
}