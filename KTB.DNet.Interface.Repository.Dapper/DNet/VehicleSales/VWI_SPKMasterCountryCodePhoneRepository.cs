﻿#region Summary
/*
 ===========================================================================
 AUTHOR        : Ivan
 PURPOSE       : VWI_SPKMasterCountryCodePhoneRepository class
 GENERATED BY  : Admin
 ---------------------
 Copyright  (c) 2021 
 ---------------------
 $History      : $
 Created on 01 July 2021 
 ===========================================================================
*/
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.VehicleSales.SqlQuery.VWI_SPKMasterCountryCodePhone;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class VWI_SPKMasterCountryCodePhoneRepository : BaseDNetRepository<VWI_SPKMasterCountryCodePhone>, IVWI_SPKMasterCountryCodePhoneRepository<VWI_SPKMasterCountryCodePhone, int>
    {
        #region Constructor
        public VWI_SPKMasterCountryCodePhoneRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_SPKMasterCountryCodePhone
        /// <summary>
        /// Create VWI_SPKMasterCountryCodePhone
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_SPKMasterCountryCodePhone entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_SPKMasterCountryCodePhone
        /// <summary>
        /// Update VWI_SPKMasterCountryCodePhone
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_SPKMasterCountryCodePhone entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_SPKMasterCountryCodePhone
        /// <summary>
        /// Delete VWI_SPKMasterCountryCodePhone
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_SPKMasterCountryCodePhone By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_SPKMasterCountryCodePhone Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_SPKMasterCountryCodePhone>(
                        VWI_SPKMasterCountryCodePhoneQuery.GetVWI_SPKMasterCountryCodePhoneById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_SPKMasterCountryCodePhone
        /// <summary>
        /// Get All VWI_SPKMasterCountryCodePhone
        /// </summary>
        /// <returns></returns>
        public List<VWI_SPKMasterCountryCodePhone> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_SPKMasterCountryCodePhone
        public List<VWI_SPKMasterCountryCodePhone> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_SPKMasterCountryCodePhone>();
        }
        #endregion

        #region Search VWI_SPKMasterCountryCodePhone        
        public new List<VWI_SPKMasterCountryCodePhone> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_SPKMasterCountryCodePhone> result = Search<VWI_SPKMasterCountryCodePhone>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_SPKMasterCountryCodePhone>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_SPKMasterCountryCodePhoneQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_SPKMasterCountryCodePhone.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_SPKMasterCountryCodePhoneQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_SPKMasterCountryCodePhone>();
            }
        }

        #endregion

        protected void SetCreatedLog(VWI_SPKMasterCountryCodePhone VWI_SPKMasterCountryCodePhone)
        {
            //VWI_SPKMasterCountryCodePhone.CreatedBy = UserLogin;
            //VWI_SPKMasterCountryCodePhone.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(VWI_SPKMasterCountryCodePhone);
        }

        protected void SetLastModifiedLog(VWI_SPKMasterCountryCodePhone VWI_SPKMasterCountryCodePhone)
        {
            //VWI_SPKMasterCountryCodePhone.LastUpdateBy = UserLogin;
            //VWI_SPKMasterCountryCodePhone.LastUpdateTime = DateTime.Now;
        }
    }
}
