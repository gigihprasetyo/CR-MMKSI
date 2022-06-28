#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_vendorlogisticRepository repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/01/2021 13:23:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_ktb_vendorlogistic;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_ktb_vendorlogisticRepository : BaseDNetRepository<VWI_CRM_ktb_vendorlogistic>, IVWI_CRM_ktb_vendorlogisticRepository<VWI_CRM_ktb_vendorlogistic, int>
    {
        #region Constructor
        public VWI_CRM_ktb_vendorlogisticRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_ktb_vendorlogistic
        /// <summary>
        /// Create VWI_CRM_ktb_vendorlogistic
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_ktb_vendorlogistic entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_ktb_vendorlogistic
        /// <summary>
        /// Update VWI_CRM_ktb_vendorlogistic
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_ktb_vendorlogistic entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_ktb_vendorlogistic
        /// <summary>
        /// Delete VWI_CRM_ktb_vendorlogistic
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_ktb_vendorlogistic By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_ktb_vendorlogistic Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_ktb_vendorlogistic>(
                        VWI_CRM_ktb_vendorlogisticQuery.GetVWI_CRM_ktb_vendorlogisticById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_ktb_vendorlogistic
        /// <summary>
        /// Get All VWI_CRM_ktb_vendorlogistic
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_ktb_vendorlogistic> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_ktb_vendorlogistic
        public List<VWI_CRM_ktb_vendorlogistic> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_ktb_vendorlogistic>();
        }
        #endregion

        #region Search VWI_CRM_ktb_vendorlogistic        
        public new List<VWI_CRM_ktb_vendorlogistic> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_ktb_vendorlogistic.rowstatus", "isnull(vwi_crm_ktb_vendorlogistic.rowstatus, 0)");

                List<VWI_CRM_ktb_vendorlogistic> result = SearchData<VWI_CRM_ktb_vendorlogistic>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_ktb_vendorlogistic>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_ktb_vendorlogisticQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_ktb_vendorlogistic.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_ktb_vendorlogisticQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_ktb_vendorlogistic>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_ktb_vendorlogistic vWI_CRM_ktb_vendorlogistic)
        {
            //vWI_CRM_ktb_vendorlogistic.CreatedBy = UserLogin;
            //vWI_CRM_ktb_vendorlogistic.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_ktb_vendorlogistic);
        }

        protected void SetLastModifiedLog(VWI_CRM_ktb_vendorlogistic vWI_CRM_ktb_vendorlogistic)
        {
            //vWI_CRM_ktb_vendorlogistic.LastUpdateBy = UserLogin;
            //vWI_CRM_ktb_vendorlogistic.LastUpdateTime = DateTime.Now;
        }
    }
}
