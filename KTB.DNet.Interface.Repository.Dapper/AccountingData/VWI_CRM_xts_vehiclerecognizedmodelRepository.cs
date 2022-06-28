#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_vehiclerecognizedmodel repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-05 09:34:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_vehiclerecognizedmodel;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_vehiclerecognizedmodelRepository : BaseDNetRepository<VWI_CRM_xts_vehiclerecognizedmodel>, IVWI_CRM_xts_vehiclerecognizedmodelRepository<VWI_CRM_xts_vehiclerecognizedmodel, int>
    {
        #region Constructor
        public VWI_CRM_xts_vehiclerecognizedmodelRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_vehiclerecognizedmodel
        /// <summary>
        /// Create VWI_CRM_xts_vehiclerecognizedmodel
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_vehiclerecognizedmodel entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_vehiclerecognizedmodel
        /// <summary>
        /// Update VWI_CRM_xts_vehiclerecognizedmodel
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_vehiclerecognizedmodel entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_vehiclerecognizedmodel
        /// <summary>
        /// Delete VWI_CRM_xts_vehiclerecognizedmodel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_vehiclerecognizedmodel By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_vehiclerecognizedmodel Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_vehiclerecognizedmodel>(
                        VWI_CRM_xts_vehiclerecognizedmodelQuery.GetVWI_CRM_xts_vehiclerecognizedmodelById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_vehiclerecognizedmodel
        /// <summary>
        /// Get All VWI_CRM_xts_vehiclerecognizedmodel
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_vehiclerecognizedmodel> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_vehiclerecognizedmodel
        public List<VWI_CRM_xts_vehiclerecognizedmodel> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_vehiclerecognizedmodel>();
        }
        #endregion

        #region Search VWI_CRM_xts_vehiclerecognizedmodel        
        public new List<VWI_CRM_xts_vehiclerecognizedmodel> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_vehiclerecognizedmodel.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xts_vehiclerecognizedmodel> result = SearchData<VWI_CRM_xts_vehiclerecognizedmodel>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_vehiclerecognizedmodel>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_vehiclerecognizedmodelQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_vehiclerecognizedmodel.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_vehiclerecognizedmodelQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_vehiclerecognizedmodel>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_vehiclerecognizedmodel vWI_CRM_xts_vehiclerecognizedmodel)
        {
            //vWI_CRM_xts_vehiclerecognizedmodel.CreatedBy = UserLogin;
            //vWI_CRM_xts_vehiclerecognizedmodel.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_vehiclerecognizedmodel);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_vehiclerecognizedmodel vWI_CRM_xts_vehiclerecognizedmodel)
        {
            //vWI_CRM_xts_vehiclerecognizedmodel.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_vehiclerecognizedmodel.LastUpdateTime = DateTime.Now;
        }
    }
}