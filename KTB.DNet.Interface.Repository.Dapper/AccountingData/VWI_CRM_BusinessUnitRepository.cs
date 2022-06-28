#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_BusinessUnit repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/12/2020 17:06:21
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_BusinessUnit;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_BusinessUnitRepository : BaseDNetRepository<VWI_CRM_BusinessUnit>, IVWI_CRM_BusinessUnitRepository<VWI_CRM_BusinessUnit, int>
    {
        #region Constructor
        public VWI_CRM_BusinessUnitRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_BusinessUnit
        /// <summary>
        /// Create VWI_CRM_BusinessUnit
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_BusinessUnit entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_BusinessUnit
        /// <summary>
        /// Update VWI_CRM_BusinessUnit
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_BusinessUnit entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_BusinessUnit
        /// <summary>
        /// Delete VWI_CRM_BusinessUnit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_BusinessUnit By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_BusinessUnit Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_BusinessUnit>(
                        VWI_CRM_BusinessUnitQuery.GetVWI_CRM_BusinessUnitById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_BusinessUnit
        /// <summary>
        /// Get All VWI_CRM_BusinessUnit
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_BusinessUnit> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_BusinessUnit
        public List<VWI_CRM_BusinessUnit> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_BusinessUnit>();
        }
        #endregion

		#region Search VWI_CRM_BusinessUnit        
        public new List<VWI_CRM_BusinessUnit> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_businessunit.", "crm_businessunit.");
                strCriteria = strCriteria.ToLower().Replace("crm_businessunit.rowstatus", "isnull(crm_businessunit.rowstatus, 0)");

                List<VWI_CRM_BusinessUnit> result = SearchData<VWI_CRM_BusinessUnit>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_BusinessUnit>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_BusinessUnitQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_BusinessUnit.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_BusinessUnitQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_BusinessUnit>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_BusinessUnit vWI_CRM_BusinessUnit)
        {
            //vWI_CRM_BusinessUnit.CreatedBy = UserLogin;
            //vWI_CRM_BusinessUnit.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_BusinessUnit);
        }

        protected void SetLastModifiedLog(VWI_CRM_BusinessUnit vWI_CRM_BusinessUnit)
        {
            //vWI_CRM_BusinessUnit.LastUpdateBy = UserLogin;
            //vWI_CRM_BusinessUnit.LastUpdateTime = DateTime.Now;
        }
    }
}