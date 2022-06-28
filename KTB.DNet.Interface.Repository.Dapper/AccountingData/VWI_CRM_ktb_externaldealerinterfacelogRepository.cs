#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : VWI_CRM_ktb_externaldealerinterfacelog class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_QA
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 29 Apr 2021 09:23:25
 ===========================================================================
*/
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_ktb_externaldealerinterfacelog;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_ktb_externaldealerinterfacelogRepository : BaseDNetRepository<VWI_CRM_ktb_externaldealerinterfacelog>, IVWI_CRM_ktb_externaldealerinterfacelogRepository<VWI_CRM_ktb_externaldealerinterfacelog, int>
    {
        #region Constructor
        public VWI_CRM_ktb_externaldealerinterfacelogRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

		
        #region Create VWI_CRM_ktb_externaldealerinterfacelog
        public ResponseMessage Create(VWI_CRM_ktb_externaldealerinterfacelog entity)
        {
			return null;
		}
        #endregion
		
		
        #region Update VWI_CRM_ktb_externaldealerinterfacelog
        public ResponseMessage Update(VWI_CRM_ktb_externaldealerinterfacelog entity)
        {
			return null;
		}
        #endregion
		

        #region Delete VWI_CRM_ktb_externaldealerinterfacelog
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

		#region Get VWI_CRM_ktb_externaldealerinterfacelog By Id
        public VWI_CRM_ktb_externaldealerinterfacelog Get(Guid ktb_externaldealerinterfacelogid)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_ktb_externaldealerinterfacelog>(
                        VWI_CRM_ktb_externaldealerinterfacelogQuery.GetVWI_CRM_ktb_externaldealerinterfacelogByID, new { ktb_externaldealerinterfacelogid = ktb_externaldealerinterfacelogid }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public VWI_CRM_ktb_externaldealerinterfacelog Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All VWI_CRM_ktb_externaldealerinterfacelog
        public List<VWI_CRM_ktb_externaldealerinterfacelog> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_ktb_externaldealerinterfacelog
        public List<VWI_CRM_ktb_externaldealerinterfacelog> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_ktb_externaldealerinterfacelog>();
        }
        #endregion

		#region Search VWI_CRM_ktb_externaldealerinterfacelog        
        public new List<VWI_CRM_ktb_externaldealerinterfacelog> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string dbAccConnectionString = AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection);
                string[] dbcon = dbAccConnectionString.Split(';');
                string db = "[" + dbcon[1].Substring(dbcon[1].LastIndexOf('=') + 1) + "].";

                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_ktb_externaldealerinterfacelog.rowstatus", "isnull(vwi_crm_ktb_externaldealerinterfacelog.rowstatus, 0)");

                List<VWI_CRM_ktb_externaldealerinterfacelog> result = SearchData<VWI_CRM_ktb_externaldealerinterfacelog>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_ktb_externaldealerinterfacelog>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_ktb_externaldealerinterfacelogQuery.SelectQuery, db,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_ktb_externaldealerinterfacelog.ktb_externaldealerinterfacelogid", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_ktb_externaldealerinterfacelogQuery.GetTotalQuery, db,
                                                strCriteria,
                                                strInnerCriteria), "ktb_externaldealerinterfacelogid");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_ktb_externaldealerinterfacelog>();
            }
        }
        #endregion

        public void SetCreatedLog(VWI_CRM_ktb_externaldealerinterfacelog VWI_CRM_ktb_externaldealerinterfacelog)
        {
            VWI_CRM_ktb_externaldealerinterfacelog.createdbyname = UserLogin;
            VWI_CRM_ktb_externaldealerinterfacelog.createdon = DateTime.Now;
            VWI_CRM_ktb_externaldealerinterfacelog.RowStatus = 0;
            SetLastModifiedLog(VWI_CRM_ktb_externaldealerinterfacelog);
        }

        public void SetLastModifiedLog(VWI_CRM_ktb_externaldealerinterfacelog VWI_CRM_ktb_externaldealerinterfacelog)
        {
            VWI_CRM_ktb_externaldealerinterfacelog.modifiedbyname = UserLogin;
            VWI_CRM_ktb_externaldealerinterfacelog.modifiedon = DateTime.Now;
        }

		
    }
}