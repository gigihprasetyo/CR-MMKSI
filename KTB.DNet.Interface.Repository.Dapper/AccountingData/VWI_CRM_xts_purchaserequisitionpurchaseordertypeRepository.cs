#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_purchaserequisitionpurchaseordertype repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 24/12/2019 3:25:51
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.xts_purchaserequisitionpurchaseordertype;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_purchaserequisitionpurchaseordertypeRepository : BaseDNetRepository<VWI_CRM_xts_purchaserequisitionpurchaseordertype>, IVWI_CRM_xts_purchaserequisitionpurchaseordertypeRepository<VWI_CRM_xts_purchaserequisitionpurchaseordertype, int>
    {
        #region Constructor
        public VWI_CRM_xts_purchaserequisitionpurchaseordertypeRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_purchaserequisitionpurchaseordertype
        /// <summary>
        /// Create VWI_CRM_xts_purchaserequisitionpurchaseordertype
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_purchaserequisitionpurchaseordertype entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_purchaserequisitionpurchaseordertype
        /// <summary>
        /// Update VWI_CRM_xts_purchaserequisitionpurchaseordertype
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_purchaserequisitionpurchaseordertype entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_purchaserequisitionpurchaseordertype
        /// <summary>
        /// Delete VWI_CRM_xts_purchaserequisitionpurchaseordertype
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_xts_purchaserequisitionpurchaseordertype By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_purchaserequisitionpurchaseordertype Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_purchaserequisitionpurchaseordertype>(
                        VWI_CRM_xts_purchaserequisitionpurchaseordertypeQuery.GetVWI_CRM_xts_purchaserequisitionpurchaseordertypeById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_purchaserequisitionpurchaseordertype
        /// <summary>
        /// Get All VWI_CRM_xts_purchaserequisitionpurchaseordertype
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_purchaserequisitionpurchaseordertype> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_purchaserequisitionpurchaseordertype
        public List<VWI_CRM_xts_purchaserequisitionpurchaseordertype> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_purchaserequisitionpurchaseordertype>();
        }
        #endregion

		#region Search VWI_CRM_xts_purchaserequisitionpurchaseordertype        
        public new List<VWI_CRM_xts_purchaserequisitionpurchaseordertype> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_purchaserequisitionpurchaseordertype.rowstatus", "isnull(vwi_crm_xts_purchaserequisitionpurchaseordertype.rowstatus, 0)");

                List<VWI_CRM_xts_purchaserequisitionpurchaseordertype> result = SearchData<VWI_CRM_xts_purchaserequisitionpurchaseordertype>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_purchaserequisitionpurchaseordertype>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_purchaserequisitionpurchaseordertypeQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_purchaserequisitionpurchaseordertype.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_purchaserequisitionpurchaseordertypeQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_purchaserequisitionpurchaseordertype>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_purchaserequisitionpurchaseordertype vWI_CRM_xts_purchaserequisitionpurchaseordertype)
        {
            //vWI_CRM_xts_purchaserequisitionpurchaseordertype.CreatedBy = UserLogin;
            //vWI_CRM_xts_purchaserequisitionpurchaseordertype.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_purchaserequisitionpurchaseordertype);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_purchaserequisitionpurchaseordertype vWI_CRM_xts_purchaserequisitionpurchaseordertype)
        {
            //vWI_CRM_xts_purchaserequisitionpurchaseordertype.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_purchaserequisitionpurchaseordertype.LastUpdateTime = DateTime.Now;
        }
    }
}