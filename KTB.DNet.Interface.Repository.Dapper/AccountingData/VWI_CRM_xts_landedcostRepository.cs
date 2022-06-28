#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_landedcostRepository repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-04 09:30:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_landedcost;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_landedcostRepository : BaseDNetRepository<VWI_CRM_xts_landedcost>, IVWI_CRM_xts_landedcostRepository<VWI_CRM_xts_landedcost, int>
    {
        #region Constructor
        public VWI_CRM_xts_landedcostRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_landedcost
        /// <summary>
        /// Create VWI_CRM_xts_landedcost
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_landedcost entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_landedcost
        /// <summary>
        /// Update VWI_CRM_xts_landedcost
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_landedcost entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_landedcost
        /// <summary>
        /// Delete VWI_CRM_xts_landedcost
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_landedcost By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_landedcost Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_landedcost>(
                        VWI_CRM_xts_landedcostQuery.GetVWI_CRM_xts_landedcostById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_landedcost
        /// <summary>
        /// Get All VWI_CRM_xts_landedcost
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_landedcost> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_landedcost
        public List<VWI_CRM_xts_landedcost> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_landedcost>();
        }
        #endregion

        #region Search VWI_CRM_xts_landedcost        
        public new List<VWI_CRM_xts_landedcost> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_landedcost.rowstatus", "isnull(vwi_crm_xts_landedcost.rowstatus, 0)");

                List<VWI_CRM_xts_landedcost> result = SearchData<VWI_CRM_xts_landedcost>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_landedcost>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_landedcostQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_landedcost.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_landedcostQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_landedcost>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_landedcost vWI_CRM_xts_landedcost)
        {
            //vWI_CRM_xts_landedcost.CreatedBy = UserLogin;
            //vWI_CRM_xts_landedcost.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_landedcost);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_landedcost vWI_CRM_xts_landedcost)
        {
            //vWI_CRM_xts_landedcost.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_landedcost.LastUpdateTime = DateTime.Now;
        }
    }
}
