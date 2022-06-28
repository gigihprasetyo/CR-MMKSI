#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_daerahlogisticRepository repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/01/2021 11:51:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_ktb_daerahlogistic;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_ktb_daerahlogisticRepository : BaseDNetRepository<VWI_CRM_ktb_daerahlogistic>, IVWI_CRM_ktb_daerahlogisticRepository<VWI_CRM_ktb_daerahlogistic, int>
    {
        #region Constructor
        public VWI_CRM_ktb_daerahlogisticRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_ktb_daerahlogistic
        /// <summary>
        /// Create VWI_CRM_ktb_daerahlogistic
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_ktb_daerahlogistic entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_ktb_daerahlogistic
        /// <summary>
        /// Update VWI_CRM_ktb_daerahlogistic
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_ktb_daerahlogistic entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_ktb_daerahlogistic
        /// <summary>
        /// Delete VWI_CRM_ktb_daerahlogistic
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_ktb_daerahlogistic By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_ktb_daerahlogistic Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_ktb_daerahlogistic>(
                        VWI_CRM_ktb_daerahlogisticQuery.GetVWI_CRM_ktb_daerahlogisticById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_ktb_daerahlogistic
        /// <summary>
        /// Get All VWI_CRM_ktb_daerahlogistic
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_ktb_daerahlogistic> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_ktb_daerahlogistic
        public List<VWI_CRM_ktb_daerahlogistic> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_ktb_daerahlogistic>();
        }
        #endregion

        #region Search VWI_CRM_ktb_daerahlogistic        
        public new List<VWI_CRM_ktb_daerahlogistic> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_ktb_daerahlogistic.rowstatus", "isnull(vwi_crm_ktb_daerahlogistic.rowstatus, 0)");

                List<VWI_CRM_ktb_daerahlogistic> result = SearchData<VWI_CRM_ktb_daerahlogistic>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_ktb_daerahlogistic>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_ktb_daerahlogisticQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_ktb_daerahlogistic.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_ktb_daerahlogisticQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_ktb_daerahlogistic>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_ktb_daerahlogistic vWI_CRM_ktb_daerahlogistic)
        {
            //vWI_CRM_ktb_daerahlogistic.CreatedBy = UserLogin;
            //vWI_CRM_ktb_daerahlogistic.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_ktb_daerahlogistic);
        }

        protected void SetLastModifiedLog(VWI_CRM_ktb_daerahlogistic vWI_CRM_ktb_daerahlogistic)
        {
            //vWI_CRM_ktb_daerahlogistic.LastUpdateBy = UserLogin;
            //vWI_CRM_ktb_daerahlogistic.LastUpdateTime = DateTime.Now;
        }
    }
}
