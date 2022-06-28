#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_OutgoingInventoryTransferWarehouse repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.PRT_OutgoingInventoryTransferWarehouse;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_PRT_OutgoingInventoryTransferWarehouseRepository : BaseDNetRepository<VWI_CRM_PRT_OutgoingInventoryTransferWarehouse>, IVWI_CRM_PRT_OutgoingInventoryTransferWarehouseRepository<VWI_CRM_PRT_OutgoingInventoryTransferWarehouse, int>
    {
        #region Constructor
        public VWI_CRM_PRT_OutgoingInventoryTransferWarehouseRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_PRT_OutgoingInventoryTransferWarehouse
        /// <summary>
        /// Create VWI_CRM_PRT_OutgoingInventoryTransferWarehouse
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_PRT_OutgoingInventoryTransferWarehouse entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_PRT_OutgoingInventoryTransferWarehouse
        /// <summary>
        /// Update VWI_CRM_PRT_OutgoingInventoryTransferWarehouse
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_PRT_OutgoingInventoryTransferWarehouse entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_PRT_OutgoingInventoryTransferWarehouse
        /// <summary>
        /// Delete VWI_CRM_PRT_OutgoingInventoryTransferWarehouse
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_PRT_OutgoingInventoryTransferWarehouse By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_PRT_OutgoingInventoryTransferWarehouse Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_PRT_OutgoingInventoryTransferWarehouse>(
                        VWI_CRM_PRT_OutgoingInventoryTransferWarehouseQuery.GetVWI_CRM_PRT_OutgoingInventoryTransferWarehouseById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_PRT_OutgoingInventoryTransferWarehouse
        /// <summary>
        /// Get All VWI_CRM_PRT_OutgoingInventoryTransferWarehouse
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_PRT_OutgoingInventoryTransferWarehouse> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_PRT_OutgoingInventoryTransferWarehouse
        public List<VWI_CRM_PRT_OutgoingInventoryTransferWarehouse> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_PRT_OutgoingInventoryTransferWarehouse>();
        }
        #endregion

		#region Search VWI_CRM_PRT_OutgoingInventoryTransferWarehouse        
        public new List<VWI_CRM_PRT_OutgoingInventoryTransferWarehouse> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_PRT_OutgoingInventoryTransferWarehouse> result = Search<VWI_CRM_PRT_OutgoingInventoryTransferWarehouse>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_PRT_OutgoingInventoryTransferWarehouse>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_PRT_OutgoingInventoryTransferWarehouseQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_PRT_OutgoingInventoryTransferWarehouse.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_PRT_OutgoingInventoryTransferWarehouseQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_PRT_OutgoingInventoryTransferWarehouse>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_PRT_OutgoingInventoryTransferWarehouse vWI_CRM_PRT_OutgoingInventoryTransferWarehouse)
        {
            //vWI_CRM_PRT_OutgoingInventoryTransferWarehouse.CreatedBy = UserLogin;
            //vWI_CRM_PRT_OutgoingInventoryTransferWarehouse.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_PRT_OutgoingInventoryTransferWarehouse);
        }

        protected void SetLastModifiedLog(VWI_CRM_PRT_OutgoingInventoryTransferWarehouse vWI_CRM_PRT_OutgoingInventoryTransferWarehouse)
        {
            //vWI_CRM_PRT_OutgoingInventoryTransferWarehouse.LastUpdateBy = UserLogin;
            //vWI_CRM_PRT_OutgoingInventoryTransferWarehouse.LastUpdateTime = DateTime.Now;
        }
    }
}