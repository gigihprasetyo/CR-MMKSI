#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_bookableresourcebookingRepository repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 2021-10-08
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_bookableresourcebooking;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_bookableresourcebookingRepository : BaseDNetRepository<VWI_CRM_bookableresourcebooking>, IVWI_CRM_bookableresourcebookingRepository<VWI_CRM_bookableresourcebooking, int>
    {
        #region Constructor
        public VWI_CRM_bookableresourcebookingRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_bookableresourcebooking
        /// <summary>
        /// Create VWI_CRM_bookableresourcebooking
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_bookableresourcebooking entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_bookableresourcebooking
        /// <summary>
        /// Update VWI_CRM_bookableresourcebooking
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_bookableresourcebooking entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_bookableresourcebooking
        /// <summary>
        /// Delete VWI_CRM_bookableresourcebooking
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_bookableresourcebooking By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_bookableresourcebooking Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_bookableresourcebooking>(
                        VWI_CRM_bookableresourcebookingQuery.GetVWI_CRM_bookableresourcebookingById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_bookableresourcebooking
        /// <summary>
        /// Get All VWI_CRM_bookableresourcebooking
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_bookableresourcebooking> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_bookableresourcebooking
        public List<VWI_CRM_bookableresourcebooking> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_bookableresourcebooking>();
        }
        #endregion

        #region Search VWI_CRM_bookableresourcebooking        
        public new List<VWI_CRM_bookableresourcebooking> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_bookableresourcebooking.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_bookableresourcebooking> result = SearchData<VWI_CRM_bookableresourcebooking>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_bookableresourcebooking>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_bookableresourcebookingQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_bookableresourcebooking.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_bookableresourcebookingQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_bookableresourcebooking>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_bookableresourcebooking vWI_CRM_bookableresourcebooking)
        {
            //vWI_CRM_bookableresourcebooking.CreatedBy = UserLogin;
            //vWI_CRM_bookableresourcebooking.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_bookableresourcebooking);
        }

        protected void SetLastModifiedLog(VWI_CRM_bookableresourcebooking vWI_CRM_bookableresourcebooking)
        {
            //vWI_CRM_bookableresourcebooking.LastUpdateBy = UserLogin;
            //vWI_CRM_bookableresourcebooking.LastUpdateTime = DateTime.Now;
        }
    }
}
