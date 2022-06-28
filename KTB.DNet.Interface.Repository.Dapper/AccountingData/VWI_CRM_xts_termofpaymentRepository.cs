#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_xts_termofpaymentRepository repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/29/2020 08:30:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xts_termofpayment;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_xts_termofpaymentRepository : BaseDNetRepository<VWI_CRM_xts_termofpayment>, IVWI_CRM_xts_termofpaymentRepository<VWI_CRM_xts_termofpayment, int>
    {
        #region Constructor
        public VWI_CRM_xts_termofpaymentRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_xts_termofpayment
        /// <summary>
        /// Create VWI_CRM_xts_termofpayment
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_xts_termofpayment entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_xts_termofpayment
        /// <summary>
        /// Update VWI_CRM_xts_termofpayment
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_xts_termofpayment entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_xts_termofpayment
        /// <summary>
        /// Delete VWI_CRM_xts_termofpayment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xts_termofpayment By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_xts_termofpayment Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xts_termofpayment>(
                        VWI_CRM_xts_termofpaymentQuery.GetVWI_CRM_xts_termofpaymentById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_xts_termofpayment
        /// <summary>
        /// Get All VWI_CRM_xts_termofpayment
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_xts_termofpayment> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xts_termofpayment
        public List<VWI_CRM_xts_termofpayment> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xts_termofpayment>();
        }
        #endregion

        #region Search VWI_CRM_xts_termofpayment        
        public new List<VWI_CRM_xts_termofpayment> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xts_termofpayment.rowstatus", "isnull(vwi_crm_xts_termofpayment.rowstatus, 0)");

                List<VWI_CRM_xts_termofpayment> result = SearchData<VWI_CRM_xts_termofpayment>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xts_termofpayment>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xts_termofpaymentQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xts_termofpayment.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xts_termofpaymentQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xts_termofpayment>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_xts_termofpayment vWI_CRM_xts_termofpayment)
        {
            //vWI_CRM_xts_termofpayment.CreatedBy = UserLogin;
            //vWI_CRM_xts_termofpayment.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_xts_termofpayment);
        }

        protected void SetLastModifiedLog(VWI_CRM_xts_termofpayment vWI_CRM_xts_termofpayment)
        {
            //vWI_CRM_xts_termofpayment.LastUpdateBy = UserLogin;
            //vWI_CRM_xts_termofpayment.LastUpdateTime = DateTime.Now;
        }
    }
}
