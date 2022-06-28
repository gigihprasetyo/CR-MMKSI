#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_vehiclespecification repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 03/02/2020 17:40:59
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_vehiclespecification;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_vehiclespecificationRepository : BaseDNetRepository<VWI_CRM_xts_vehiclespecification>, IVWI_CRM_xts_vehiclespecificationRepository<VWI_CRM_xts_vehiclespecification, int>
    {
        #region Constructor
        public VWI_CRM_xts_vehiclespecificationRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_vehiclespecification
        /// <summary>
        /// Create VWI_CRM_xts_vehiclespecification
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_vehiclespecification entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_vehiclespecification
        /// <summary>
        /// Update VWI_CRM_xts_vehiclespecification
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_vehiclespecification entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_vehiclespecification
        /// <summary>
        /// Delete VWI_CRM_xts_vehiclespecification
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_xts_vehiclespecification By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_vehiclespecification Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_vehiclespecification>(
                        VWI_CRM_xts_vehiclespecificationQuery.GetVWI_CRM_xts_vehiclespecificationById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_vehiclespecification
        /// <summary>
        /// Get All VWI_CRM_xts_vehiclespecification
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_vehiclespecification> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_vehiclespecification
        public List<VWI_CRM_xts_vehiclespecification> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_vehiclespecification>();
        }
        #endregion

		#region Search VWI_CRM_xts_vehiclespecification        
        public new List<VWI_CRM_xts_vehiclespecification> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_vehiclespecification.rowstatus", "isnull(vwi_crm_xts_vehiclespecification.rowstatus, 0)");

                List<VWI_CRM_xts_vehiclespecification> result = SearchData<VWI_CRM_xts_vehiclespecification>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_vehiclespecification>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_vehiclespecificationQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_vehiclespecification.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_vehiclespecificationQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_vehiclespecification>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_vehiclespecification vWI_CRM_xts_vehiclespecification)
        {
            //vWI_CRM_xts_vehiclespecification.CreatedBy = UserLogin;
            //vWI_CRM_xts_vehiclespecification.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_vehiclespecification);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_vehiclespecification vWI_CRM_xts_vehiclespecification)
        {
            //vWI_CRM_xts_vehiclespecification.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_vehiclespecification.LastUpdateTime = DateTime.Now;
        }
    }
}