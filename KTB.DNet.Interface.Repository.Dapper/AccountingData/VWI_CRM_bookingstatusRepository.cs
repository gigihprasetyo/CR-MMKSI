#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_bookingstatusRepository repository class
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
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_bookingstatus;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_bookingstatusRepository : BaseDNetRepository<VWI_CRM_bookingstatus>, IVWI_CRM_bookingstatusRepository<VWI_CRM_bookingstatus, int>
    {
        #region Constructor
        public VWI_CRM_bookingstatusRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_bookingstatus
        /// <summary>
        /// Create VWI_CRM_bookingstatus
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_bookingstatus entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_bookingstatus
        /// <summary>
        /// Update VWI_CRM_bookingstatus
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_bookingstatus entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_bookingstatus
        /// <summary>
        /// Delete VWI_CRM_bookingstatus
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_bookingstatus By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_bookingstatus Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_bookingstatus>(
                        VWI_CRM_bookingstatusQuery.GetVWI_CRM_bookingstatusById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_bookingstatus
        /// <summary>
        /// Get All VWI_CRM_bookingstatus
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_bookingstatus> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_bookingstatus
        public List<VWI_CRM_bookingstatus> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_bookingstatus>();
        }
        #endregion

        #region Search VWI_CRM_bookingstatus        
        public new List<VWI_CRM_bookingstatus> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_bookingstatus.rowstatus", "isnull(vwi_crm_bookingstatus.rowstatus, 0)");

                List<VWI_CRM_bookingstatus> result = SearchData<VWI_CRM_bookingstatus>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_bookingstatus>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_bookingstatusQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_bookingstatus.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_bookingstatusQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_bookingstatus>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_bookingstatus vWI_CRM_bookingstatus)
        {
            //vWI_CRM_bookingstatus.CreatedBy = UserLogin;
            //vWI_CRM_bookingstatus.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_bookingstatus);
        }

        protected void SetLastModifiedLog(VWI_CRM_bookingstatus vWI_CRM_bookingstatus)
        {
            //vWI_CRM_bookingstatus.LastUpdateBy = UserLogin;
            //vWI_CRM_bookingstatus.LastUpdateTime = DateTime.Now;
        }
    }
}
