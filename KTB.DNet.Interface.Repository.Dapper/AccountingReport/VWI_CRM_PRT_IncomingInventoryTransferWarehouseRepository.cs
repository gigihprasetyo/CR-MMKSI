#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_IncomingInventoryTransferWarehouse repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.PRT_IncomingInventoryTransferWarehouse;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_PRT_IncomingInventoryTransferWarehouseRepository : BaseDNetRepository<VWI_CRM_PRT_IncomingInventoryTransferWarehouse>, IVWI_CRM_PRT_IncomingInventoryTransferWarehouseRepository<VWI_CRM_PRT_IncomingInventoryTransferWarehouse, int>
    {
        #region Constructor
        public VWI_CRM_PRT_IncomingInventoryTransferWarehouseRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_PRT_IncomingInventoryTransferWarehouse
        /// <summary>
        /// Create VWI_CRM_PRT_IncomingInventoryTransferWarehouse
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_PRT_IncomingInventoryTransferWarehouse entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_PRT_IncomingInventoryTransferWarehouse
        /// <summary>
        /// Update VWI_CRM_PRT_IncomingInventoryTransferWarehouse
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_PRT_IncomingInventoryTransferWarehouse entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_PRT_IncomingInventoryTransferWarehouse
        /// <summary>
        /// Delete VWI_CRM_PRT_IncomingInventoryTransferWarehouse
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_PRT_IncomingInventoryTransferWarehouse By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_PRT_IncomingInventoryTransferWarehouse Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_PRT_IncomingInventoryTransferWarehouse>(
                        VWI_CRM_PRT_IncomingInventoryTransferWarehouseQuery.GetVWI_CRM_PRT_IncomingInventoryTransferWarehouseById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_PRT_IncomingInventoryTransferWarehouse
        /// <summary>
        /// Get All VWI_CRM_PRT_IncomingInventoryTransferWarehouse
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_PRT_IncomingInventoryTransferWarehouse> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_PRT_IncomingInventoryTransferWarehouse
        public List<VWI_CRM_PRT_IncomingInventoryTransferWarehouse> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_PRT_IncomingInventoryTransferWarehouse>();
        }
        #endregion

		#region Search VWI_CRM_PRT_IncomingInventoryTransferWarehouse        
        public new List<VWI_CRM_PRT_IncomingInventoryTransferWarehouse> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_PRT_IncomingInventoryTransferWarehouse> result = Search<VWI_CRM_PRT_IncomingInventoryTransferWarehouse>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_PRT_IncomingInventoryTransferWarehouse>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_PRT_IncomingInventoryTransferWarehouseQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_PRT_IncomingInventoryTransferWarehouse.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_PRT_IncomingInventoryTransferWarehouseQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_PRT_IncomingInventoryTransferWarehouse>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_PRT_IncomingInventoryTransferWarehouse vWI_CRM_PRT_IncomingInventoryTransferWarehouse)
        {
            //vWI_CRM_PRT_IncomingInventoryTransferWarehouse.CreatedBy = UserLogin;
            //vWI_CRM_PRT_IncomingInventoryTransferWarehouse.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_PRT_IncomingInventoryTransferWarehouse);
        }

        protected void SetLastModifiedLog(VWI_CRM_PRT_IncomingInventoryTransferWarehouse vWI_CRM_PRT_IncomingInventoryTransferWarehouse)
        {
            //vWI_CRM_PRT_IncomingInventoryTransferWarehouse.LastUpdateBy = UserLogin;
            //vWI_CRM_PRT_IncomingInventoryTransferWarehouse.LastUpdateTime = DateTime.Now;
        }
    }
}