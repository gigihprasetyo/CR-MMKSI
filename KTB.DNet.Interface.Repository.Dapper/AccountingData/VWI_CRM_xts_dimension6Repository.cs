#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_dimension6Repository repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/30/2020 10:33:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_dimension6;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_dimension6Repository : BaseDNetRepository<VWI_CRM_xts_dimension6>, IVWI_CRM_xts_dimension6Repository<VWI_CRM_xts_dimension6, int>
    {
        #region Constructor
        public VWI_CRM_xts_dimension6Repository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_dimension6
        /// <summary>
        /// Create VWI_CRM_xts_dimension6
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_dimension6 entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_dimension6
        /// <summary>
        /// Update VWI_CRM_xts_dimension6
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_dimension6 entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_dimension6
        /// <summary>
        /// Delete VWI_CRM_xts_dimension6
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_dimension6 By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_dimension6 Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_dimension6>(
                        VWI_CRM_xts_dimension6Query.GetVWI_CRM_xts_dimension6ById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_dimension6
        /// <summary>
        /// Get All VWI_CRM_xts_dimension6
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_dimension6> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_dimension6
        public List<VWI_CRM_xts_dimension6> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_dimension6>();
        }
        #endregion

        #region Search VWI_CRM_xts_dimension6        
        public new List<VWI_CRM_xts_dimension6> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_dimension6.rowstatus", "isnull(vwi_crm_xts_dimension6.rowstatus, 0)");

                List<VWI_CRM_xts_dimension6> result = SearchData<VWI_CRM_xts_dimension6>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_dimension6>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_dimension6Query.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_dimension6.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_dimension6Query.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_dimension6>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_dimension6 vWI_CRM_xts_dimension6)
        {
            //vWI_CRM_xts_dimension6.CreatedBy = UserLogin;
            //vWI_CRM_xts_dimension6.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_dimension6);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_dimension6 vWI_CRM_xts_dimension6)
        {
            //vWI_CRM_xts_dimension6.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_dimension6.LastUpdateTime = DateTime.Now;
        }
    }
}
