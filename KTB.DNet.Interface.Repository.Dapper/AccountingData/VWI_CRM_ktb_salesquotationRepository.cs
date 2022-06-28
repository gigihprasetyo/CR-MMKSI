#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_salesquotationRepository repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/22/2020 16:56:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_ktb_salesquotation;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_ktb_salesquotationRepository : BaseDNetRepository<VWI_CRM_ktb_salesquotation>, IVWI_CRM_ktb_salesquotationRepository<VWI_CRM_ktb_salesquotation, int>
    {
        #region Constructor
        public VWI_CRM_ktb_salesquotationRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_ktb_salesquotation
        /// <summary>
        /// Create VWI_CRM_ktb_salesquotation
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_ktb_salesquotation entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_ktb_salesquotation
        /// <summary>
        /// Update VWI_CRM_ktb_salesquotation
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_ktb_salesquotation entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_ktb_salesquotation
        /// <summary>
        /// Delete VWI_CRM_ktb_salesquotation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_ktb_salesquotation By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_ktb_salesquotation Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_ktb_salesquotation>(
                        VWI_CRM_ktb_salesquotationQuery.GetVWI_CRM_ktb_salesquotationById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_ktb_salesquotation
        /// <summary>
        /// Get All VWI_CRM_ktb_salesquotation
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_ktb_salesquotation> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_ktb_salesquotation
        public List<VWI_CRM_ktb_salesquotation> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_ktb_salesquotation>();
        }
        #endregion

        #region Search VWI_CRM_ktb_salesquotation        
        public new List<VWI_CRM_ktb_salesquotation> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_ktb_salesquotation.rowstatus", "isnull(vwi_crm_ktb_salesquotation.rowstatus, 0)");

                List<VWI_CRM_ktb_salesquotation> result = SearchData<VWI_CRM_ktb_salesquotation>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_ktb_salesquotation>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_ktb_salesquotationQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_ktb_salesquotation.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_ktb_salesquotationQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_ktb_salesquotation>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_ktb_salesquotation vWI_CRM_ktb_salesquotation)
        {
            //vWI_CRM_ktb_salesquotation.CreatedBy = UserLogin;
            //vWI_CRM_ktb_salesquotation.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_ktb_salesquotation);
        }

        protected void SetLastModifiedLog(VWI_CRM_ktb_salesquotation vWI_CRM_ktb_salesquotation)
        {
            //vWI_CRM_ktb_salesquotation.LastUpdateBy = UserLogin;
            //vWI_CRM_ktb_salesquotation.LastUpdateTime = DateTime.Now;
        }
    }
}
