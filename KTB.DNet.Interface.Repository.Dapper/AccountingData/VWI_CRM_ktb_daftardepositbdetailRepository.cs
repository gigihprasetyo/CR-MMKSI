#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_daftardepositbdetail repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_ktb_daftardepositbdetail;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_ktb_daftardepositbdetailRepository : BaseDNetRepository<VWI_CRM_ktb_daftardepositbdetail>, IVWI_CRM_ktb_daftardepositbdetailRepository<VWI_CRM_ktb_daftardepositbdetail, int>
    {
        #region Constructor
        public VWI_CRM_ktb_daftardepositbdetailRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_ktb_daftardepositbdetail
        /// <summary>
        /// Create VWI_CRM_ktb_daftardepositbdetail
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_ktb_daftardepositbdetail entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_ktb_daftardepositbdetail
        /// <summary>
        /// Update VWI_CRM_ktb_daftardepositbdetail
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_ktb_daftardepositbdetail entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_ktb_daftardepositbdetail
        /// <summary>
        /// Delete VWI_CRM_ktb_daftardepositbdetail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_ktb_daftardepositbdetail By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_ktb_daftardepositbdetail Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_ktb_daftardepositbdetail>(
                        VWI_CRM_ktb_daftardepositbdetailQuery.GetVWI_CRM_ktb_daftardepositbdetailById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_ktb_daftardepositbdetail
        /// <summary>
        /// Get All VWI_CRM_ktb_daftardepositbdetail
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_ktb_daftardepositbdetail> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_ktb_daftardepositbdetail
        public List<VWI_CRM_ktb_daftardepositbdetail> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_ktb_daftardepositbdetail>();
        }
        #endregion

        #region Search VWI_CRM_ktb_daftardepositbdetail        
        public new List<VWI_CRM_ktb_daftardepositbdetail> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_ktb_daftardepositbdetail.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_ktb_daftardepositbdetail> result = SearchData<VWI_CRM_ktb_daftardepositbdetail>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_ktb_daftardepositbdetail>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_ktb_daftardepositbdetailQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_ktb_daftardepositbdetail.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_ktb_daftardepositbdetailQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_ktb_daftardepositbdetail>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_ktb_daftardepositbdetail vWI_CRM_ktb_daftardepositbdetail)
        {
            //vWI_CRM_ktb_daftardepositbdetail.CreatedBy = UserLogin;
            //vWI_CRM_ktb_daftardepositbdetail.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_ktb_daftardepositbdetail);
        }

        protected void SetLastModifiedLog(VWI_CRM_ktb_daftardepositbdetail vWI_CRM_ktb_daftardepositbdetail)
        {
            //vWI_CRM_ktb_daftardepositbdetail.LastUpdateBy = UserLogin;
            //vWI_CRM_ktb_daftardepositbdetail.LastUpdateTime = DateTime.Now;
        }
    }
}