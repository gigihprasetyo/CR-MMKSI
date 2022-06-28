#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_ktb_saleschannel repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 12/30/2020 08:19:00
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_ktb_saleschannel;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_ktb_saleschannelRepository : BaseDNetRepository<VWI_CRM_ktb_saleschannel>, IVWI_CRM_ktb_saleschannelRepository<VWI_CRM_ktb_saleschannel, int>
    {
        #region Constructor
        public VWI_CRM_ktb_saleschannelRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_ktb_saleschannel
        /// <summary>
        /// Create VWI_CRM_ktb_saleschannel
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_ktb_saleschannel entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_ktb_saleschannel
        /// <summary>
        /// Update VWI_CRM_ktb_saleschannel
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_ktb_saleschannel entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_ktb_saleschannel
        /// <summary>
        /// Delete VWI_CRM_ktb_saleschannel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_ktb_saleschannel By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_ktb_saleschannel Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_ktb_saleschannel>(
                        VWI_CRM_ktb_saleschannelQuery.GetVWI_CRM_ktb_saleschannelById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_ktb_saleschannel
        /// <summary>
        /// Get All VWI_CRM_ktb_saleschannel
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_ktb_saleschannel> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_ktb_saleschannel
        public List<VWI_CRM_ktb_saleschannel> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_ktb_saleschannel>();
        }
        #endregion

        #region Search VWI_CRM_ktb_saleschannel        
        public new List<VWI_CRM_ktb_saleschannel> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_ktb_saleschannel.rowstatus", "isnull(vwi_crm_ktb_saleschannel.rowstatus, 0)");

                List<VWI_CRM_ktb_saleschannel> result = SearchData<VWI_CRM_ktb_saleschannel>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_ktb_saleschannel>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_ktb_saleschannelQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_ktb_saleschannel.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_ktb_saleschannelQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_ktb_saleschannel>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_ktb_saleschannel vWI_CRM_ktb_saleschannel)
        {
            //vWI_CRM_ktb_saleschannel.CreatedBy = UserLogin;
            //vWI_CRM_ktb_saleschannel.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_ktb_saleschannel);
        }

        protected void SetLastModifiedLog(VWI_CRM_ktb_saleschannel vWI_CRM_ktb_saleschannel)
        {
            //vWI_CRM_ktb_saleschannel.LastUpdateBy = UserLogin;
            //vWI_CRM_ktb_saleschannel.LastUpdateTime = DateTime.Now;
        }
    }
}