#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_inventtrans repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 22/03/2022 13:51:21
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_inventtrans;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_inventtransRepository : BaseDNetRepository<VWI_CRM_xts_inventtrans>, IVWI_CRM_xts_inventtransRepository<VWI_CRM_xts_inventtrans, int>
    {
        #region Constructor
        public VWI_CRM_xts_inventtransRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_inventtrans
        /// <summary>
        /// Create VWI_CRM_xts_inventtrans
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_inventtrans entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_inventtrans
        /// <summary>
        /// Update VWI_CRM_xts_inventtrans
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_inventtrans entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_inventtrans
        /// <summary>
        /// Delete VWI_CRM_xts_inventtrans
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_xts_inventtrans By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_inventtrans Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_inventtrans>(
                        VWI_CRM_xts_inventtransQuery.GetVWI_CRM_xts_inventtransById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_inventtrans
        /// <summary>
        /// Get All VWI_CRM_xts_inventtrans
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_inventtrans> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_inventtrans
        public List<VWI_CRM_xts_inventtrans> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_inventtrans>();
        }
        #endregion

		#region Search VWI_CRM_xts_inventtrans        
        public new List<VWI_CRM_xts_inventtrans> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_xts_inventtrans> result = Search<VWI_CRM_xts_inventtrans>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_inventtrans>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_inventtransQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_inventtrans.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_inventtransQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_inventtrans>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_inventtrans vWI_CRM_xts_inventtrans)
        {
            //vWI_CRM_xts_inventtrans.CreatedBy = UserLogin;
            //vWI_CRM_xts_inventtrans.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_inventtrans);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_inventtrans vWI_CRM_xts_inventtrans)
        {
            //vWI_CRM_xts_inventtrans.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_inventtrans.LastUpdateTime = DateTime.Now;
        }
    }
}