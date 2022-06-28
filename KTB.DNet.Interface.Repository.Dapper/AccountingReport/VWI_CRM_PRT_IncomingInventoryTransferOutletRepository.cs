#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_IncomingInventoryTransferOutlet repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.PRT_IncomingInventoryTransferOutlet;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_PRT_IncomingInventoryTransferOutletRepository : BaseDNetRepository<VWI_CRM_PRT_IncomingInventoryTransferOutlet>, IVWI_CRM_PRT_IncomingInventoryTransferOutletRepository<VWI_CRM_PRT_IncomingInventoryTransferOutlet, int>
    {
        #region Constructor
        public VWI_CRM_PRT_IncomingInventoryTransferOutletRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_PRT_IncomingInventoryTransferOutlet
        /// <summary>
        /// Create VWI_CRM_PRT_IncomingInventoryTransferOutlet
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_PRT_IncomingInventoryTransferOutlet entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_PRT_IncomingInventoryTransferOutlet
        /// <summary>
        /// Update VWI_CRM_PRT_IncomingInventoryTransferOutlet
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_PRT_IncomingInventoryTransferOutlet entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_PRT_IncomingInventoryTransferOutlet
        /// <summary>
        /// Delete VWI_CRM_PRT_IncomingInventoryTransferOutlet
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_PRT_IncomingInventoryTransferOutlet By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_PRT_IncomingInventoryTransferOutlet Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_PRT_IncomingInventoryTransferOutlet>(
                        VWI_CRM_PRT_IncomingInventoryTransferOutletQuery.GetVWI_CRM_PRT_IncomingInventoryTransferOutletById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_PRT_IncomingInventoryTransferOutlet
        /// <summary>
        /// Get All VWI_CRM_PRT_IncomingInventoryTransferOutlet
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_PRT_IncomingInventoryTransferOutlet> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_PRT_IncomingInventoryTransferOutlet
        public List<VWI_CRM_PRT_IncomingInventoryTransferOutlet> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_PRT_IncomingInventoryTransferOutlet>();
        }
        #endregion

		#region Search VWI_CRM_PRT_IncomingInventoryTransferOutlet        
        public new List<VWI_CRM_PRT_IncomingInventoryTransferOutlet> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_PRT_IncomingInventoryTransferOutlet> result = Search<VWI_CRM_PRT_IncomingInventoryTransferOutlet>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_PRT_IncomingInventoryTransferOutlet>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_PRT_IncomingInventoryTransferOutletQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_PRT_IncomingInventoryTransferOutlet.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_PRT_IncomingInventoryTransferOutletQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_PRT_IncomingInventoryTransferOutlet>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_PRT_IncomingInventoryTransferOutlet vWI_CRM_PRT_IncomingInventoryTransferOutlet)
        {
            //vWI_CRM_PRT_IncomingInventoryTransferOutlet.CreatedBy = UserLogin;
            //vWI_CRM_PRT_IncomingInventoryTransferOutlet.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_PRT_IncomingInventoryTransferOutlet);
        }

        protected void SetLastModifiedLog(VWI_CRM_PRT_IncomingInventoryTransferOutlet vWI_CRM_PRT_IncomingInventoryTransferOutlet)
        {
            //vWI_CRM_PRT_IncomingInventoryTransferOutlet.LastUpdateBy = UserLogin;
            //vWI_CRM_PRT_IncomingInventoryTransferOutlet.LastUpdateTime = DateTime.Now;
        }
    }
}