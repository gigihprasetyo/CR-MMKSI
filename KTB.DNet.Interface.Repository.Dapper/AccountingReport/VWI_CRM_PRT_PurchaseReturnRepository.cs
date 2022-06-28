#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_PRT_PurchaseReturn repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_CRM_PRT_PurchaseReturn;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_PRT_PurchaseReturnRepository : BaseDNetRepository<VWI_CRM_PRT_PurchaseReturn>, IVWI_CRM_PRT_PurchaseReturnRepository<VWI_CRM_PRT_PurchaseReturn, int>
    {
        #region Constructor
        public VWI_CRM_PRT_PurchaseReturnRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_PRT_PurchaseReturn
        /// <summary>
        /// Create VWI_CRM_PRT_PurchaseReturn
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_PRT_PurchaseReturn entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_PRT_PurchaseReturn
        /// <summary>
        /// Update VWI_CRM_PRT_PurchaseReturn
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_PRT_PurchaseReturn entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_PRT_PurchaseReturn
        /// <summary>
        /// Delete VWI_CRM_PRT_PurchaseReturn
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_PRT_PurchaseReturn By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_PRT_PurchaseReturn Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_PRT_PurchaseReturn>(
                        VWI_CRM_PRT_PurchaseReturnQuery.GetVWI_CRM_PRT_PurchaseReturnById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_PRT_PurchaseReturn
        /// <summary>
        /// Get All VWI_CRM_PRT_PurchaseReturn
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_PRT_PurchaseReturn> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_PRT_PurchaseReturn
        public List<VWI_CRM_PRT_PurchaseReturn> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_PRT_PurchaseReturn>();
        }
        #endregion

		#region Search VWI_CRM_PRT_PurchaseReturn        
        public new List<VWI_CRM_PRT_PurchaseReturn> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_PRT_PurchaseReturn> result = Search<VWI_CRM_PRT_PurchaseReturn>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_PRT_PurchaseReturn>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_PRT_PurchaseReturnQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_PRT_PurchaseReturn.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_PRT_PurchaseReturnQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_PRT_PurchaseReturn>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_PRT_PurchaseReturn vWI_CRM_PRT_PurchaseReturn)
        {
            //vWI_CRM_PRT_PurchaseReturn.CreatedBy = UserLogin;
            //vWI_CRM_PRT_PurchaseReturn.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_PRT_PurchaseReturn);
        }

        protected void SetLastModifiedLog(VWI_CRM_PRT_PurchaseReturn vWI_CRM_PRT_PurchaseReturn)
        {
            //vWI_CRM_PRT_PurchaseReturn.LastUpdateBy = UserLogin;
            //vWI_CRM_PRT_PurchaseReturn.LastUpdateTime = DateTime.Now;
        }
    }
}