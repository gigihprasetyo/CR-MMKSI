#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_AX_PRT_FlowReportPart repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 31/01/2020 8:18:25
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_AX_PRT_FlowReportPart;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_AX_PRT_FlowReportPartRepository : BaseDNetRepository<VWI_AX_PRT_FlowReportPart>, IVWI_AX_PRT_FlowReportPartRepository<VWI_AX_PRT_FlowReportPart, int>
    {
        #region Constructor
        public VWI_AX_PRT_FlowReportPartRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_AX_PRT_FlowReportPart
        /// <summary>
        /// Create VWI_AX_PRT_FlowReportPart
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_AX_PRT_FlowReportPart entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_AX_PRT_FlowReportPart
        /// <summary>
        /// Update VWI_AX_PRT_FlowReportPart
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_AX_PRT_FlowReportPart entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_AX_PRT_FlowReportPart
        /// <summary>
        /// Delete VWI_AX_PRT_FlowReportPart
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_AX_PRT_FlowReportPart By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_AX_PRT_FlowReportPart Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_AX_PRT_FlowReportPart>(
                        VWI_AX_PRT_FlowReportPartQuery.GetVWI_AX_PRT_FlowReportPartById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_AX_PRT_FlowReportPart
        /// <summary>
        /// Get All VWI_AX_PRT_FlowReportPart
        /// </summary>
        /// <returns></returns>
        public List<VWI_AX_PRT_FlowReportPart> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_AX_PRT_FlowReportPart
        public List<VWI_AX_PRT_FlowReportPart> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_AX_PRT_FlowReportPart>();
        }
        #endregion

		#region Search VWI_AX_PRT_FlowReportPart        
        public new List<VWI_AX_PRT_FlowReportPart> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                // replace custom query
                var selectQuery = VWI_AX_PRT_FlowReportPartQuery.SelectQuery.Replace("#DATEFROM#",dateFrom.ToString("yyyy-MM-dd HH:mm:ss")).Replace("#DATETO#",dateTo.ToString("yyyy-MM-dd HH:mm:ss"));
                var getTotalQuery = VWI_AX_PRT_FlowReportPartQuery.GetTotalQuery.Replace("#DATEFROM#", dateFrom.ToString("yyyy-MM-dd HH:mm:ss")).Replace("#DATETO#", dateTo.ToString("yyyy-MM-dd HH:mm:ss"));

                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_AX_PRT_FlowReportPart> result = Search<VWI_AX_PRT_FlowReportPart>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_AX_PRT_FlowReportPart>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(selectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_AX_PRT_FlowReportPart.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(getTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_AX_PRT_FlowReportPart>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_AX_PRT_FlowReportPart vWI_AX_PRT_FlowReportPart)
        {
            //vWI_AX_PRT_FlowReportPart.CreatedBy = UserLogin;
            //vWI_AX_PRT_FlowReportPart.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_AX_PRT_FlowReportPart);
        }

        protected void SetLastModifiedLog(VWI_AX_PRT_FlowReportPart vWI_AX_PRT_FlowReportPart)
        {
            //vWI_AX_PRT_FlowReportPart.LastUpdateBy = UserLogin;
            //vWI_AX_PRT_FlowReportPart.LastUpdateTime = DateTime.Now;
        }
    }
}