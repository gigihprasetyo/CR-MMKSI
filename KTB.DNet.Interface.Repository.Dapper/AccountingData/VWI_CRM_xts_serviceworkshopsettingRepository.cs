#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_serviceworkshopsetting repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/01/2021 09:41:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_serviceworkshopsetting;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_serviceworkshopsettingRepository : BaseDNetRepository<VWI_CRM_xts_serviceworkshopsetting>, IVWI_CRM_xts_serviceworkshopsettingRepository<VWI_CRM_xts_serviceworkshopsetting, int>
    {
        #region Constructor
        public VWI_CRM_xts_serviceworkshopsettingRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_serviceworkshopsetting
        /// <summary>
        /// Create VWI_CRM_xts_serviceworkshopsetting
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_serviceworkshopsetting entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_serviceworkshopsetting
        /// <summary>
        /// Update VWI_CRM_xts_serviceworkshopsetting
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_serviceworkshopsetting entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_serviceworkshopsetting
        /// <summary>
        /// Delete VWI_CRM_xts_serviceworkshopsetting
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_serviceworkshopsetting By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_serviceworkshopsetting Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_serviceworkshopsetting>(
                        VWI_CRM_xts_serviceworkshopsettingQuery.GetVWI_CRM_xts_serviceworkshopsettingById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_serviceworkshopsetting
        /// <summary>
        /// Get All VWI_CRM_xts_serviceworkshopsetting
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_serviceworkshopsetting> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_serviceworkshopsetting
        public List<VWI_CRM_xts_serviceworkshopsetting> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_serviceworkshopsetting>();
        }
        #endregion

        #region Search VWI_CRM_xts_serviceworkshopsetting        
        public new List<VWI_CRM_xts_serviceworkshopsetting> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_serviceworkshopsetting.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xts_serviceworkshopsetting> result = SearchData<VWI_CRM_xts_serviceworkshopsetting>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_serviceworkshopsetting>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_serviceworkshopsettingQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_serviceworkshopsetting.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_serviceworkshopsettingQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_serviceworkshopsetting>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_serviceworkshopsetting vWI_CRM_xts_serviceworkshopsetting)
        {
            //vWI_CRM_xts_serviceworkshopsetting.CreatedBy = UserLogin;
            //vWI_CRM_xts_serviceworkshopsetting.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_serviceworkshopsetting);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_serviceworkshopsetting vWI_CRM_xts_serviceworkshopsetting)
        {
            //vWI_CRM_xts_serviceworkshopsetting.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_serviceworkshopsetting.LastUpdateTime = DateTime.Now;
        }
    }
}