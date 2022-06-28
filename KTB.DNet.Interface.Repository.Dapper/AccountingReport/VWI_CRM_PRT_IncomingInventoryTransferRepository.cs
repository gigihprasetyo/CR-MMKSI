#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_IncomingInventoryTransfer repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/12/2019 10:31:03
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_CRM_PRT_IncomingInventoryTransfer;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_PRT_IncomingInventoryTransferRepository : BaseDNetRepository<VWI_CRM_PRT_IncomingInventoryTransfer>, IVWI_CRM_PRT_IncomingInventoryTransferRepository<VWI_CRM_PRT_IncomingInventoryTransfer, int>
    {
        #region Constructor
        public VWI_CRM_PRT_IncomingInventoryTransferRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_PRT_IncomingInventoryTransfer
        /// <summary>
        /// Create VWI_CRM_PRT_IncomingInventoryTransfer
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_PRT_IncomingInventoryTransfer entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_PRT_IncomingInventoryTransfer
        /// <summary>
        /// Update VWI_CRM_PRT_IncomingInventoryTransfer
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_PRT_IncomingInventoryTransfer entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_PRT_IncomingInventoryTransfer
        /// <summary>
        /// Delete VWI_CRM_PRT_IncomingInventoryTransfer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_PRT_IncomingInventoryTransfer By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_PRT_IncomingInventoryTransfer Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_PRT_IncomingInventoryTransfer>(
                        VWI_CRM_PRT_IncomingInventoryTransferQuery.GetVWI_CRM_PRT_IncomingInventoryTransferById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_PRT_IncomingInventoryTransfer
        /// <summary>
        /// Get All VWI_CRM_PRT_IncomingInventoryTransfer
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_PRT_IncomingInventoryTransfer> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_PRT_IncomingInventoryTransfer
        public List<VWI_CRM_PRT_IncomingInventoryTransfer> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_PRT_IncomingInventoryTransfer>();
        }
        #endregion

		#region Search VWI_CRM_PRT_IncomingInventoryTransfer        
        public new List<VWI_CRM_PRT_IncomingInventoryTransfer> Search(string strCriteria, string strInnerCriteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != null && sortColumns.Count > 0 ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_PRT_IncomingInventoryTransfer> result = Search<VWI_CRM_PRT_IncomingInventoryTransfer>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_PRT_IncomingInventoryTransfer>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_PRT_IncomingInventoryTransferQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_PRT_IncomingInventoryTransfer.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_PRT_IncomingInventoryTransferQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_PRT_IncomingInventoryTransfer>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_PRT_IncomingInventoryTransfer vWI_CRM_PRT_IncomingInventoryTransfer)
        {
            //vWI_CRM_PRT_IncomingInventoryTransfer.CreatedBy = UserLogin;
            //vWI_CRM_PRT_IncomingInventoryTransfer.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_PRT_IncomingInventoryTransfer);
        }

        protected void SetLastModifiedLog(VWI_CRM_PRT_IncomingInventoryTransfer vWI_CRM_PRT_IncomingInventoryTransfer)
        {
            //vWI_CRM_PRT_IncomingInventoryTransfer.LastUpdateBy = UserLogin;
            //vWI_CRM_PRT_IncomingInventoryTransfer.LastUpdateTime = DateTime.Now;
        }
    }
}