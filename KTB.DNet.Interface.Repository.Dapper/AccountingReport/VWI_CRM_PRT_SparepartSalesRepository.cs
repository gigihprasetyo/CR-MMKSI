#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_SparepartSales repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_CRM_PRT_SparepartSales;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_PRT_SparepartSalesRepository : BaseDNetRepository<VWI_CRM_PRT_SparepartSales>, IVWI_CRM_PRT_SparepartSalesRepository<VWI_CRM_PRT_SparepartSales, int>
    {
        #region Constructor
        public VWI_CRM_PRT_SparepartSalesRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_PRT_SparepartSales
        /// <summary>
        /// Create VWI_CRM_PRT_SparepartSales
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_PRT_SparepartSales entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_PRT_SparepartSales
        /// <summary>
        /// Update VWI_CRM_PRT_SparepartSales
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_PRT_SparepartSales entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_PRT_SparepartSales
        /// <summary>
        /// Delete VWI_CRM_PRT_SparepartSales
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_PRT_SparepartSales By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_PRT_SparepartSales Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_PRT_SparepartSales>(
                        VWI_CRM_PRT_SparepartSalesQuery.GetVWI_CRM_PRT_SparepartSalesById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_PRT_SparepartSales
        /// <summary>
        /// Get All VWI_CRM_PRT_SparepartSales
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_PRT_SparepartSales> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_PRT_SparepartSales
        public List<VWI_CRM_PRT_SparepartSales> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_PRT_SparepartSales>();
        }
        #endregion

		#region Search VWI_CRM_PRT_SparepartSales        
        public new List<VWI_CRM_PRT_SparepartSales> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_PRT_SparepartSales> result = Search<VWI_CRM_PRT_SparepartSales>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_PRT_SparepartSales>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_PRT_SparepartSalesQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_PRT_SparepartSales.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_PRT_SparepartSalesQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_PRT_SparepartSales>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_PRT_SparepartSales vWI_CRM_PRT_SparepartSales)
        {
            //vWI_CRM_PRT_SparepartSales.CreatedBy = UserLogin;
            //vWI_CRM_PRT_SparepartSales.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_PRT_SparepartSales);
        }

        protected void SetLastModifiedLog(VWI_CRM_PRT_SparepartSales vWI_CRM_PRT_SparepartSales)
        {
            //vWI_CRM_PRT_SparepartSales.LastUpdateBy = UserLogin;
            //vWI_CRM_PRT_SparepartSales.LastUpdateTime = DateTime.Now;
        }
    }
}