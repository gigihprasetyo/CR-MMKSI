#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_AX_PRT_StockMovement repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_AX_PRT_StockMovement;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_AX_PRT_StockMovementRepository : BaseDNetRepository<VWI_AX_PRT_StockMovement>, IVWI_AX_PRT_StockMovementRepository<VWI_AX_PRT_StockMovement, int>
    {
        #region Constructor
        public VWI_AX_PRT_StockMovementRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_AX_PRT_StockMovement
        /// <summary>
        /// Create VWI_AX_PRT_StockMovement
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_AX_PRT_StockMovement entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_AX_PRT_StockMovement
        /// <summary>
        /// Update VWI_AX_PRT_StockMovement
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_AX_PRT_StockMovement entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_AX_PRT_StockMovement
        /// <summary>
        /// Delete VWI_AX_PRT_StockMovement
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_AX_PRT_StockMovement By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_AX_PRT_StockMovement Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_AX_PRT_StockMovement>(
                        VWI_AX_PRT_StockMovementQuery.GetVWI_AX_PRT_StockMovementById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_AX_PRT_StockMovement
        /// <summary>
        /// Get All VWI_AX_PRT_StockMovement
        /// </summary>
        /// <returns></returns>
        public List<VWI_AX_PRT_StockMovement> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_AX_PRT_StockMovement
        public List<VWI_AX_PRT_StockMovement> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_AX_PRT_StockMovement>();
        }
        #endregion

		#region Search VWI_AX_PRT_StockMovement        
        public new List<VWI_AX_PRT_StockMovement> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_AX_PRT_StockMovement> result = Search<VWI_AX_PRT_StockMovement>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_AX_PRT_StockMovement>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_AX_PRT_StockMovementQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_AX_PRT_StockMovement.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_AX_PRT_StockMovementQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_AX_PRT_StockMovement>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_AX_PRT_StockMovement vWI_AX_PRT_StockMovement)
        {
            //vWI_AX_PRT_StockMovement.CreatedBy = UserLogin;
            //vWI_AX_PRT_StockMovement.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_AX_PRT_StockMovement);
        }

        protected void SetLastModifiedLog(VWI_AX_PRT_StockMovement vWI_AX_PRT_StockMovement)
        {
            //vWI_AX_PRT_StockMovement.LastUpdateBy = UserLogin;
            //vWI_AX_PRT_StockMovement.LastUpdateTime = DateTime.Now;
        }
    }
}