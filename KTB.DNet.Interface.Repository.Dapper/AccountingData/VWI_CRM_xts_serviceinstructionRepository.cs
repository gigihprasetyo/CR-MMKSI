#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_serviceinstruction repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-01-04 08:24:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_serviceinstruction;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_serviceinstructionRepository : BaseDNetRepository<VWI_CRM_xts_serviceinstruction>, IVWI_CRM_xts_serviceinstructionRepository<VWI_CRM_xts_serviceinstruction, int>
    {
        #region Constructor
        public VWI_CRM_xts_serviceinstructionRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_serviceinstruction
        /// <summary>
        /// Create VWI_CRM_xts_serviceinstruction
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_serviceinstruction entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_serviceinstruction
        /// <summary>
        /// Update VWI_CRM_xts_serviceinstruction
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_serviceinstruction entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_serviceinstruction
        /// <summary>
        /// Delete VWI_CRM_xts_serviceinstruction
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_serviceinstruction By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_serviceinstruction Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_serviceinstruction>(
                        VWI_CRM_xts_serviceinstructionQuery.GetVWI_CRM_xts_serviceinstructionById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_serviceinstruction
        /// <summary>
        /// Get All VWI_CRM_xts_serviceinstruction
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_serviceinstruction> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_serviceinstruction
        public List<VWI_CRM_xts_serviceinstruction> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_serviceinstruction>();
        }
        #endregion

        #region Search VWI_CRM_xts_serviceinstruction        
        public new List<VWI_CRM_xts_serviceinstruction> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_serviceinstruction.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xts_serviceinstruction> result = SearchData<VWI_CRM_xts_serviceinstruction>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_serviceinstruction>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_serviceinstructionQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_serviceinstruction.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_serviceinstructionQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_serviceinstruction>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_serviceinstruction vWI_CRM_xts_serviceinstruction)
        {
            //vWI_CRM_xts_serviceinstruction.CreatedBy = UserLogin;
            //vWI_CRM_xts_serviceinstruction.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_serviceinstruction);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_serviceinstruction vWI_CRM_xts_serviceinstruction)
        {
            //vWI_CRM_xts_serviceinstruction.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_serviceinstruction.LastUpdateTime = DateTime.Now;
        }
    }
}