#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_SVC_ARReceipt repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_CRM_SVC_ARReceipt;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_SVC_ARReceiptRepository : BaseDNetRepository<VWI_CRM_SVC_ARReceipt>, IVWI_CRM_SVC_ARReceiptRepository<VWI_CRM_SVC_ARReceipt, int>
    {
        #region Constructor
        public VWI_CRM_SVC_ARReceiptRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_SVC_ARReceipt
        /// <summary>
        /// Create VWI_CRM_SVC_ARReceipt
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_SVC_ARReceipt entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_SVC_ARReceipt
        /// <summary>
        /// Update VWI_CRM_SVC_ARReceipt
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_SVC_ARReceipt entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_SVC_ARReceipt
        /// <summary>
        /// Delete VWI_CRM_SVC_ARReceipt
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_SVC_ARReceipt By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_SVC_ARReceipt Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_SVC_ARReceipt>(
                        VWI_CRM_SVC_ARReceiptQuery.GetVWI_CRM_SVC_ARReceiptById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_SVC_ARReceipt
        /// <summary>
        /// Get All VWI_CRM_SVC_ARReceipt
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_SVC_ARReceipt> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_SVC_ARReceipt
        public List<VWI_CRM_SVC_ARReceipt> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_SVC_ARReceipt>();
        }
        #endregion

		#region Search VWI_CRM_SVC_ARReceipt        
        public new List<VWI_CRM_SVC_ARReceipt> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_SVC_ARReceipt> result = Search<VWI_CRM_SVC_ARReceipt>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_SVC_ARReceipt>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_SVC_ARReceiptQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_SVC_ARReceipt.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_SVC_ARReceiptQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_SVC_ARReceipt>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_SVC_ARReceipt vWI_CRM_SVC_ARReceipt)
        {
            //vWI_CRM_SVC_ARReceipt.CreatedBy = UserLogin;
            //vWI_CRM_SVC_ARReceipt.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_SVC_ARReceipt);
        }

        protected void SetLastModifiedLog(VWI_CRM_SVC_ARReceipt vWI_CRM_SVC_ARReceipt)
        {
            //vWI_CRM_SVC_ARReceipt.LastUpdateBy = UserLogin;
            //vWI_CRM_SVC_ARReceipt.LastUpdateTime = DateTime.Now;
        }
    }
}