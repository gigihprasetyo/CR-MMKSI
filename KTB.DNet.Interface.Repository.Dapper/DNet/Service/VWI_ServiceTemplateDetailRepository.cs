#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_WOInvoiceDetail repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 26/02/2021 16:24:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.Service.SqlQuery.VWI_ServiceTemplateDetail;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class VWI_ServiceTemplateDetailRepository : BaseDNetRepository<VWI_ServiceTemplateDetail>, IVWI_ServiceTemplateDetailRepository<VWI_ServiceTemplateDetail, int>
    {
        #region Constructor
        public VWI_ServiceTemplateDetailRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_ServiceTemplateDetail
        /// <summary>
        /// Create VWI_ServiceTemplateDetail
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_ServiceTemplateDetail entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_ServiceTemplateDetail
        /// <summary>
        /// Update VWI_ServiceTemplateDetail
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_ServiceTemplateDetail entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_ServiceTemplateDetail
        /// <summary>
        /// Delete VWI_ServiceTemplateDetail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_ServiceTemplateDetail By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_ServiceTemplateDetail Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_ServiceTemplateDetail>(
                        VWI_ServiceTemplateDetailQuery.GetVWI_ServiceTemplateDetailById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_ServiceTemplateDetail
        /// <summary>
        /// Get All VWI_ServiceTemplateDetail
        /// </summary>
        /// <returns></returns>
        public List<VWI_ServiceTemplateDetail> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_ServiceTemplateDetail
        public List<VWI_ServiceTemplateDetail> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_ServiceTemplateDetail>();
        }
        #endregion

        #region Search VWI_ServiceTemplateDetail        
        public new List<VWI_ServiceTemplateDetail> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_ServiceTemplateDetail> result = Search<VWI_ServiceTemplateDetail>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_ServiceTemplateDetail>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_ServiceTemplateDetailQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_ServiceTemplateDetail.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_ServiceTemplateDetailQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_ServiceTemplateDetail>();
            }
        }

        public new List<VWI_ServiceTemplateDetail> SearchCustom(string strCriteria)
        {
            try
            {
                int page = 1;
                int pageSize = 5000;
                string orderBy = null;
                int filteredResultsCount = 0;
                string strInnerCriteria = string.Empty;

                List<VWI_ServiceTemplateDetail> result = SearchFetchPaging<VWI_ServiceTemplateDetail>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_ServiceTemplateDetail>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_ServiceTemplateDetailQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_ServiceTemplateDetail.ID", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_ServiceTemplateDetailQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "ID");

                return result;

            }
            catch (Exception ex)
            {
                return new List<VWI_ServiceTemplateDetail>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_ServiceTemplateDetail serviceTemplateDetail)
        {
            //vWI_CRM_WOInvoiceDetail.CreatedBy = UserLogin;
            //vWI_CRM_WOInvoiceDetail.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_WOInvoiceDetail);
        }

        protected void SetLastModifiedLog(VWI_ServiceTemplateDetail serviceTemplateDetail)
        {
            //vWI_CRM_WOInvoiceDetail.LastUpdateBy = UserLogin;
            //vWI_CRM_WOInvoiceDetail.LastUpdateTime = DateTime.Now;
        }
    }
}