#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_StandardCodeRepository repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/30/2020 08:41:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_StandardCode;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_StandardCodeRepository : BaseDNetRepository<VWI_CRM_StandardCode>
    {
        #region Constructor
        public VWI_CRM_StandardCodeRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_StandardCode
        /// <summary>
        /// Create VWI_CRM_StandardCode
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_StandardCode entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_StandardCode
        /// <summary>
        /// Update VWI_CRM_StandardCode
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_StandardCode entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_StandardCode
        /// <summary>
        /// Delete VWI_CRM_StandardCode
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_StandardCode By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_StandardCode Get(string id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_StandardCode>(
                        VWI_CRM_StandardCodeQuery.GetVWI_CRM_StandardCodeById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_StandardCode
        /// <summary>
        /// Get All VWI_CRM_StandardCode
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_StandardCode> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_StandardCode
        public List<VWI_CRM_StandardCode> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_StandardCode>();
        }
        #endregion

        #region Search VWI_CRM_StandardCode        
        public new List<VWI_CRM_StandardCode> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_StandardCode> result = SearchData<VWI_CRM_StandardCode>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_StandardCode>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_StandardCodeQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_StandardCode.Category asc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_StandardCodeQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_StandardCode>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_StandardCode VWI_CRM_StandardCode)
        {
            //VWI_CRM_StandardCode.CreatedBy = UserLogin;
            //VWI_CRM_StandardCode.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(VWI_CRM_StandardCode);
        }

        protected void SetLastModifiedLog(VWI_CRM_StandardCode VWI_CRM_StandardCode)
        {
            //VWI_CRM_StandardCode.LastUpdateBy = UserLogin;
            //VWI_CRM_StandardCode.LastUpdateTime = DateTime.Now;
        }
    }
}
