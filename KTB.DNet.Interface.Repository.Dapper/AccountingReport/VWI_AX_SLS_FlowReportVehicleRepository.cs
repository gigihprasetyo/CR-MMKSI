#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_AX_SLS_FlowReportVehicle repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 05/02/2020 9:46:08
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_AX_SLS_FlowReportVehicle;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_AX_SLS_FlowReportVehicleRepository : BaseDNetRepository<VWI_AX_SLS_FlowReportVehicle>, IVWI_AX_SLS_FlowReportVehicleRepository<VWI_AX_SLS_FlowReportVehicle, int>
    {
        #region Constructor
        public VWI_AX_SLS_FlowReportVehicleRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_AX_SLS_FlowReportVehicle
        /// <summary>
        /// Create VWI_AX_SLS_FlowReportVehicle
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_AX_SLS_FlowReportVehicle entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_AX_SLS_FlowReportVehicle
        /// <summary>
        /// Update VWI_AX_SLS_FlowReportVehicle
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_AX_SLS_FlowReportVehicle entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_AX_SLS_FlowReportVehicle
        /// <summary>
        /// Delete VWI_AX_SLS_FlowReportVehicle
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_AX_SLS_FlowReportVehicle By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_AX_SLS_FlowReportVehicle Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_AX_SLS_FlowReportVehicle>(
                        VWI_AX_SLS_FlowReportVehicleQuery.GetVWI_AX_SLS_FlowReportVehicleById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_AX_SLS_FlowReportVehicle
        /// <summary>
        /// Get All VWI_AX_SLS_FlowReportVehicle
        /// </summary>
        /// <returns></returns>
        public List<VWI_AX_SLS_FlowReportVehicle> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_AX_SLS_FlowReportVehicle
        public List<VWI_AX_SLS_FlowReportVehicle> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_AX_SLS_FlowReportVehicle>();
        }
        #endregion

		#region Search VWI_AX_SLS_FlowReportVehicle        
        public new List<VWI_AX_SLS_FlowReportVehicle> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount,DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                // replace custom query
                var selectQuery = VWI_AX_SLS_FlowReportVehicleQuery.SelectQuery.Replace("#DATEFROM#", dateFrom.ToString("yyyy-MM-dd HH:mm:ss")).Replace("#DATETO#", dateTo.ToString("yyyy-MM-dd HH:mm:ss"));
                var getTotalQuery = VWI_AX_SLS_FlowReportVehicleQuery.GetTotalQuery.Replace("#DATEFROM#", dateFrom.ToString("yyyy-MM-dd HH:mm:ss")).Replace("#DATETO#", dateTo.ToString("yyyy-MM-dd HH:mm:ss"));

                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_AX_SLS_FlowReportVehicle> result = Search<VWI_AX_SLS_FlowReportVehicle>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_AX_SLS_FlowReportVehicle>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(selectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_AX_SLS_FlowReportVehicle.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
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
                return new List<VWI_AX_SLS_FlowReportVehicle>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_AX_SLS_FlowReportVehicle vWI_AX_SLS_FlowReportVehicle)
        {
            //vWI_AX_SLS_FlowReportVehicle.CreatedBy = UserLogin;
            //vWI_AX_SLS_FlowReportVehicle.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_AX_SLS_FlowReportVehicle);
        }

        protected void SetLastModifiedLog(VWI_AX_SLS_FlowReportVehicle vWI_AX_SLS_FlowReportVehicle)
        {
            //vWI_AX_SLS_FlowReportVehicle.LastUpdateBy = UserLogin;
            //vWI_AX_SLS_FlowReportVehicle.LastUpdateTime = DateTime.Now;
        }
    }
}