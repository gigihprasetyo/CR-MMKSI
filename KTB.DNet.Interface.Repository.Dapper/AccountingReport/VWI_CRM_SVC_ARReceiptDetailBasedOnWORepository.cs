#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SVC_ARReceiptDetailBasedOnWO repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_CRM_SVC_ARReceiptDetailBasedOnWO;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_SVC_ARReceiptDetailBasedOnWORepository : BaseDNetRepository<VWI_CRM_SVC_ARReceiptDetailBasedOnWO>, IVWI_CRM_SVC_ARReceiptDetailBasedOnWORepository<VWI_CRM_SVC_ARReceiptDetailBasedOnWO, int>
    {
        #region Constructor
        public VWI_CRM_SVC_ARReceiptDetailBasedOnWORepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_SVC_ARReceiptDetailBasedOnWO
        /// <summary>
        /// Create VWI_CRM_SVC_ARReceiptDetailBasedOnWO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_SVC_ARReceiptDetailBasedOnWO entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_SVC_ARReceiptDetailBasedOnWO
        /// <summary>
        /// Update VWI_CRM_SVC_ARReceiptDetailBasedOnWO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_SVC_ARReceiptDetailBasedOnWO entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_SVC_ARReceiptDetailBasedOnWO
        /// <summary>
        /// Delete VWI_CRM_SVC_ARReceiptDetailBasedOnWO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_SVC_ARReceiptDetailBasedOnWO By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_SVC_ARReceiptDetailBasedOnWO Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_SVC_ARReceiptDetailBasedOnWO>(
                        VWI_CRM_SVC_ARReceiptDetailBasedOnWOQuery.GetVWI_CRM_SVC_ARReceiptDetailBasedOnWOById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_SVC_ARReceiptDetailBasedOnWO
        /// <summary>
        /// Get All VWI_CRM_SVC_ARReceiptDetailBasedOnWO
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_SVC_ARReceiptDetailBasedOnWO> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_SVC_ARReceiptDetailBasedOnWO
        public List<VWI_CRM_SVC_ARReceiptDetailBasedOnWO> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_SVC_ARReceiptDetailBasedOnWO>();
        }
        #endregion

		#region Search VWI_CRM_SVC_ARReceiptDetailBasedOnWO        
        public new List<VWI_CRM_SVC_ARReceiptDetailBasedOnWO> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_SVC_ARReceiptDetailBasedOnWO> result = Search<VWI_CRM_SVC_ARReceiptDetailBasedOnWO>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_SVC_ARReceiptDetailBasedOnWO>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_SVC_ARReceiptDetailBasedOnWOQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_SVC_ARReceiptDetailBasedOnWO.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_SVC_ARReceiptDetailBasedOnWOQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_SVC_ARReceiptDetailBasedOnWO>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_SVC_ARReceiptDetailBasedOnWO vWI_CRM_SVC_ARReceiptDetailBasedOnWO)
        {
            //vWI_CRM_SVC_ARReceiptDetailBasedOnWO.CreatedBy = UserLogin;
            //vWI_CRM_SVC_ARReceiptDetailBasedOnWO.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_SVC_ARReceiptDetailBasedOnWO);
        }

        protected void SetLastModifiedLog(VWI_CRM_SVC_ARReceiptDetailBasedOnWO vWI_CRM_SVC_ARReceiptDetailBasedOnWO)
        {
            //vWI_CRM_SVC_ARReceiptDetailBasedOnWO.LastUpdateBy = UserLogin;
            //vWI_CRM_SVC_ARReceiptDetailBasedOnWO.LastUpdateTime = DateTime.Now;
        }
    }
}