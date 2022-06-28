#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_leasingcompany repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_ktb_leasingcompany;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_ktb_leasingcompanyRepository : BaseDNetRepository<VWI_CRM_ktb_leasingcompany>, IVWI_CRM_ktb_leasingcompanyRepository<VWI_CRM_ktb_leasingcompany, int>
    {
        #region Constructor
        public VWI_CRM_ktb_leasingcompanyRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_ktb_leasingcompany
        /// <summary>
        /// Create VWI_CRM_ktb_leasingcompany
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_ktb_leasingcompany entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_ktb_leasingcompany
        /// <summary>
        /// Update VWI_CRM_ktb_leasingcompany
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_ktb_leasingcompany entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_ktb_leasingcompany
        /// <summary>
        /// Delete VWI_CRM_ktb_leasingcompany
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_ktb_leasingcompany By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_ktb_leasingcompany Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_ktb_leasingcompany>(
                        VWI_CRM_ktb_leasingcompanyQuery.GetVWI_CRM_ktb_leasingcompanyById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_ktb_leasingcompany
        /// <summary>
        /// Get All VWI_CRM_ktb_leasingcompany
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_ktb_leasingcompany> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_ktb_leasingcompany
        public List<VWI_CRM_ktb_leasingcompany> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_ktb_leasingcompany>();
        }
        #endregion

		#region Search VWI_CRM_ktb_leasingcompany        
        public new List<VWI_CRM_ktb_leasingcompany> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_ktb_leasingcompany.rowstatus", "isnull(vwi_crm_ktb_leasingcompany.rowstatus, 0)");

                List<VWI_CRM_ktb_leasingcompany> result = SearchData<VWI_CRM_ktb_leasingcompany>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_ktb_leasingcompany>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_ktb_leasingcompanyQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_ktb_leasingcompany.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_ktb_leasingcompanyQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_ktb_leasingcompany>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_ktb_leasingcompany vWI_CRM_ktb_leasingcompany)
        {
            //vWI_CRM_ktb_leasingcompany.CreatedBy = UserLogin;
            //vWI_CRM_ktb_leasingcompany.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_ktb_leasingcompany);
        }

        protected void SetLastModifiedLog(VWI_CRM_ktb_leasingcompany vWI_CRM_ktb_leasingcompany)
        {
            //vWI_CRM_ktb_leasingcompany.LastUpdateBy = UserLogin;
            //vWI_CRM_ktb_leasingcompany.LastUpdateTime = DateTime.Now;
        }
    }
}