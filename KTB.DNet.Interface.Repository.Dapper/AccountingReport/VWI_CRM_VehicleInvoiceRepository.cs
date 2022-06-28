#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_VehicleInvoice repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 02/03/2021 6:32:17
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingReport.SqlQuery.VWI_CRM_VehicleInvoice;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingReport
{
    public class VWI_CRM_VehicleInvoiceRepository : BaseDNetRepository<VWI_CRM_VehicleInvoice>, IVWI_CRM_VehicleInvoiceRepository<VWI_CRM_VehicleInvoice, int>
    {
        #region Constructor
        public VWI_CRM_VehicleInvoiceRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_VehicleInvoice
        /// <summary>
        /// Create VWI_CRM_VehicleInvoice
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_VehicleInvoice entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_VehicleInvoice
        /// <summary>
        /// Update VWI_CRM_VehicleInvoice
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_VehicleInvoice entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_VehicleInvoice
        /// <summary>
        /// Delete VWI_CRM_VehicleInvoice
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_VehicleInvoice By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_VehicleInvoice Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_VehicleInvoice>(
                        VWI_CRM_VehicleInvoiceQuery.GetVWI_CRM_VehicleInvoiceById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_VehicleInvoice
        /// <summary>
        /// Get All VWI_CRM_VehicleInvoice
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_VehicleInvoice> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_VehicleInvoice
        public List<VWI_CRM_VehicleInvoice> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_VehicleInvoice>();
        }
        #endregion

		#region Search VWI_CRM_VehicleInvoice        
        public new List<VWI_CRM_VehicleInvoice> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_CRM_VehicleInvoice> result = Search<VWI_CRM_VehicleInvoice>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_VehicleInvoice>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_VehicleInvoiceQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_VehicleInvoice.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_VehicleInvoiceQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_VehicleInvoice>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_VehicleInvoice vWI_CRM_VehicleInvoice)
        {
            //vWI_CRM_VehicleInvoice.CreatedBy = UserLogin;
            //vWI_CRM_VehicleInvoice.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_VehicleInvoice);
        }

        protected void SetLastModifiedLog(VWI_CRM_VehicleInvoice vWI_CRM_VehicleInvoice)
        {
            //vWI_CRM_VehicleInvoice.LastUpdateBy = UserLogin;
            //vWI_CRM_VehicleInvoice.LastUpdateTime = DateTime.Now;
        }
    }
}