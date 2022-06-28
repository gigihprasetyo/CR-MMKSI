#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_OutgoingInventoryTransferOutlet repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.PRT_OutgoingInventoryTransferOutlet;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_PRT_OutgoingInventoryTransferOutletRepository : BaseDNetRepository<VWI_CRM_PRT_OutgoingInventoryTransferOutlet>, IVWI_CRM_PRT_OutgoingInventoryTransferOutletRepository<VWI_CRM_PRT_OutgoingInventoryTransferOutlet, int>
    {
        #region Constructor
        public VWI_CRM_PRT_OutgoingInventoryTransferOutletRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_PRT_OutgoingInventoryTransferOutlet
        /// <summary>
        /// Create VWI_CRM_PRT_OutgoingInventoryTransferOutlet
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_PRT_OutgoingInventoryTransferOutlet entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_PRT_OutgoingInventoryTransferOutlet
        /// <summary>
        /// Update VWI_CRM_PRT_OutgoingInventoryTransferOutlet
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_PRT_OutgoingInventoryTransferOutlet entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_PRT_OutgoingInventoryTransferOutlet
        /// <summary>
        /// Delete VWI_CRM_PRT_OutgoingInventoryTransferOutlet
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_PRT_OutgoingInventoryTransferOutlet By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_PRT_OutgoingInventoryTransferOutlet Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_PRT_OutgoingInventoryTransferOutlet>(
                        VWI_CRM_PRT_OutgoingInventoryTransferOutletQuery.GetVWI_CRM_PRT_OutgoingInventoryTransferOutletById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_PRT_OutgoingInventoryTransferOutlet
        /// <summary>
        /// Get All VWI_CRM_PRT_OutgoingInventoryTransferOutlet
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_PRT_OutgoingInventoryTransferOutlet> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_PRT_OutgoingInventoryTransferOutlet
        public List<VWI_CRM_PRT_OutgoingInventoryTransferOutlet> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_PRT_OutgoingInventoryTransferOutlet>();
        }
        #endregion

		#region Search VWI_CRM_PRT_OutgoingInventoryTransferOutlet        
        public new List<VWI_CRM_PRT_OutgoingInventoryTransferOutlet> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_PRT_OutgoingInventoryTransferOutlet> result = Search<VWI_CRM_PRT_OutgoingInventoryTransferOutlet>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_PRT_OutgoingInventoryTransferOutlet>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_PRT_OutgoingInventoryTransferOutletQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_PRT_OutgoingInventoryTransferOutlet.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_PRT_OutgoingInventoryTransferOutletQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_PRT_OutgoingInventoryTransferOutlet>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_PRT_OutgoingInventoryTransferOutlet vWI_CRM_PRT_OutgoingInventoryTransferOutlet)
        {
            //vWI_CRM_PRT_OutgoingInventoryTransferOutlet.CreatedBy = UserLogin;
            //vWI_CRM_PRT_OutgoingInventoryTransferOutlet.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_PRT_OutgoingInventoryTransferOutlet);
        }

        protected void SetLastModifiedLog(VWI_CRM_PRT_OutgoingInventoryTransferOutlet vWI_CRM_PRT_OutgoingInventoryTransferOutlet)
        {
            //vWI_CRM_PRT_OutgoingInventoryTransferOutlet.LastUpdateBy = UserLogin;
            //vWI_CRM_PRT_OutgoingInventoryTransferOutlet.LastUpdateTime = DateTime.Now;
        }
    }
}