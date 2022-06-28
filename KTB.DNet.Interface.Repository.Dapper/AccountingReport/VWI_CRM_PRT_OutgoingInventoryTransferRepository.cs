#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_OutgoingInventoryTransfer repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_CRM_PRT_OutgoingInventoryTransfer;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_PRT_OutgoingInventoryTransferRepository : BaseDNetRepository<VWI_CRM_PRT_OutgoingInventoryTransfer>, IVWI_CRM_PRT_OutgoingInventoryTransferRepository<VWI_CRM_PRT_OutgoingInventoryTransfer, int>
    {
        #region Constructor
        public VWI_CRM_PRT_OutgoingInventoryTransferRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_PRT_OutgoingInventoryTransfer
        /// <summary>
        /// Create VWI_CRM_PRT_OutgoingInventoryTransfer
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_PRT_OutgoingInventoryTransfer entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_PRT_OutgoingInventoryTransfer
        /// <summary>
        /// Update VWI_CRM_PRT_OutgoingInventoryTransfer
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_PRT_OutgoingInventoryTransfer entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_PRT_OutgoingInventoryTransfer
        /// <summary>
        /// Delete VWI_CRM_PRT_OutgoingInventoryTransfer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_PRT_OutgoingInventoryTransfer By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_PRT_OutgoingInventoryTransfer Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_PRT_OutgoingInventoryTransfer>(
                        VWI_CRM_PRT_OutgoingInventoryTransferQuery.GetVWI_CRM_PRT_OutgoingInventoryTransferById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_PRT_OutgoingInventoryTransfer
        /// <summary>
        /// Get All VWI_CRM_PRT_OutgoingInventoryTransfer
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_PRT_OutgoingInventoryTransfer> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_PRT_OutgoingInventoryTransfer
        public List<VWI_CRM_PRT_OutgoingInventoryTransfer> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_PRT_OutgoingInventoryTransfer>();
        }
        #endregion

		#region Search VWI_CRM_PRT_OutgoingInventoryTransfer        
        public new List<VWI_CRM_PRT_OutgoingInventoryTransfer> Search(string strCriteria, string strInnerCriteria, ICollection sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != null && sortColumns.Count > 0 ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_PRT_OutgoingInventoryTransfer> result = Search<VWI_CRM_PRT_OutgoingInventoryTransfer>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_PRT_OutgoingInventoryTransfer>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_PRT_OutgoingInventoryTransferQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_PRT_OutgoingInventoryTransfer.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_PRT_OutgoingInventoryTransferQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_PRT_OutgoingInventoryTransfer>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_PRT_OutgoingInventoryTransfer vWI_CRM_PRT_OutgoingInventoryTransfer)
        {
            //vWI_CRM_PRT_OutgoingInventoryTransfer.CreatedBy = UserLogin;
            //vWI_CRM_PRT_OutgoingInventoryTransfer.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_PRT_OutgoingInventoryTransfer);
        }

        protected void SetLastModifiedLog(VWI_CRM_PRT_OutgoingInventoryTransfer vWI_CRM_PRT_OutgoingInventoryTransfer)
        {
            //vWI_CRM_PRT_OutgoingInventoryTransfer.LastUpdateBy = UserLogin;
            //vWI_CRM_PRT_OutgoingInventoryTransfer.LastUpdateTime = DateTime.Now;
        }
    }
}