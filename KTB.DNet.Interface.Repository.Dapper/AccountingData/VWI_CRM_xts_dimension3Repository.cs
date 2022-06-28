#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_dimension3Repository repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/30/2020 09:22:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_dimension3;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_dimension3Repository : BaseDNetRepository<VWI_CRM_xts_dimension3>, IVWI_CRM_xts_dimension3Repository<VWI_CRM_xts_dimension3, int>
    {
        #region Constructor
        public VWI_CRM_xts_dimension3Repository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_dimension3
        /// <summary>
        /// Create VWI_CRM_xts_dimension3
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_dimension3 entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_dimension3
        /// <summary>
        /// Update VWI_CRM_xts_dimension3
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_dimension3 entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_dimension3
        /// <summary>
        /// Delete VWI_CRM_xts_dimension3
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_dimension3 By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_dimension3 Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_dimension3>(
                        VWI_CRM_xts_dimension3Query.GetVWI_CRM_xts_dimension3ById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_dimension3
        /// <summary>
        /// Get All VWI_CRM_xts_dimension3
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_dimension3> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_dimension3
        public List<VWI_CRM_xts_dimension3> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_dimension3>();
        }
        #endregion

        #region Search VWI_CRM_xts_dimension3        
        public new List<VWI_CRM_xts_dimension3> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_dimension3.rowstatus", "isnull(vwi_crm_xts_dimension3.rowstatus, 0)");

                List<VWI_CRM_xts_dimension3> result = SearchData<VWI_CRM_xts_dimension3>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_dimension3>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_dimension3Query.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_dimension3.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_dimension3Query.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_dimension3>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_dimension3 vWI_CRM_xts_dimension3)
        {
            //vWI_CRM_xts_dimension3.CreatedBy = UserLogin;
            //vWI_CRM_xts_dimension3.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_dimension3);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_dimension3 vWI_CRM_xts_dimension3)
        {
            //vWI_CRM_xts_dimension3.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_dimension3.LastUpdateTime = DateTime.Now;
        }
    }
}
