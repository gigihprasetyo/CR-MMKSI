#region Summary
// ===========================================================================
// AUTHOR        : Ivan
// PURPOSE       : VWI_PQRRepository  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2021 
// ---------------------
// $History      : $
// Created on 2021/06/29
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.DNet.SpareParts.SqlQuery.VWI_PQR;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.DNet
{
    public class VWI_PQRSparePartMasterRepository : BaseDNetRepository<VWI_PQR>, IVWI_PQRSparePartMasterRepository<VWI_PQRSparePartMaster, int>
    {
        #region Constructor
        public VWI_PQRSparePartMasterRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_PQR
        /// <summary>
        /// Create VWI_PQR
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_PQRSparePartMaster entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_PQR
        /// <summary>
        /// Update VWI_PQR
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_PQRSparePartMaster entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_PQR
        /// <summary>
        /// Delete VWI_PQR
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_PQR By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_PQRSparePartMaster Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_PQRSparePartMaster>(
                        VWI_PQRQuery.GetVWI_PQRById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_PQR
        /// <summary>
        /// Get All VWI_PQR
        /// </summary>
        /// <returns></returns>
        public List<VWI_PQRSparePartMaster> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_PQRSparePartMaster
        public List<VWI_PQRSparePartMaster> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_PQRSparePartMaster>();
        }
        #endregion

        #region Search VWI_PQRSparePartMaster    

        public new List<VWI_PQRSparePartMaster> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;

                List<VWI_PQRSparePartMaster> result = Search<VWI_PQRSparePartMaster>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_PQRSparePartMaster>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_PQRQuery.SelectQuerySPM,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_PQRSparePartMaster.IDRow", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_PQRQuery.GetTotalQuerySPM,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_PQRSparePartMaster>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_PQRSparePartMaster VWI_PQRSparePartMaster)
        {
            //VWI_PQR.CreatedBy = UserLogin;
            //VWI_PQR.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(VWI_PQR);
        }

        protected void SetLastModifiedLog(VWI_PQRSparePartMaster VWI_PQRSparePartMaster)
        {
            //VWI_PQR.LastUpdateBy = UserLogin;
            //VWI_PQR.LastUpdateTime = DateTime.Now;
        }
    }
}
