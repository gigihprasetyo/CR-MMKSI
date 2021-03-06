#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : VWI_CRM_xjp_vehiclecostinput class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 17 Sep 2021 09:41:46
 ===========================================================================
*/
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_xjp_vehiclecostinput;
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
    public class VWI_CRM_xjp_vehiclecostinputRepository : BaseDNetRepository<VWI_CRM_xjp_vehiclecostinput>, IVWI_CRM_xjp_vehiclecostinputRepository<VWI_CRM_xjp_vehiclecostinput, int>
    {
        #region Constructor
        public VWI_CRM_xjp_vehiclecostinputRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion


        #region Create VWI_CRM_xjp_vehiclecostinput
        public ResponseMessage Create(VWI_CRM_xjp_vehiclecostinput entity)
        {
            return null;
        }
        #endregion


        #region Update VWI_CRM_xjp_vehiclecostinput
        public ResponseMessage Update(VWI_CRM_xjp_vehiclecostinput entity)
        {
            return null;
        }
        #endregion


        #region Delete VWI_CRM_xjp_vehiclecostinput
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_xjp_vehiclecostinput By Id
        public VWI_CRM_xjp_vehiclecostinput Get(Guid xjp_vehiclecostinputid)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_xjp_vehiclecostinput>(
                        VWI_CRM_xjp_vehiclecostinputQuery.GetVWI_CRM_xjp_vehiclecostinputByID, new { xjp_vehiclecostinputid = xjp_vehiclecostinputid }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public VWI_CRM_xjp_vehiclecostinput Get(int id)
        {
            return null;
        }
        #endregion

        #region Get All VWI_CRM_xjp_vehiclecostinput
        public List<VWI_CRM_xjp_vehiclecostinput> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_xjp_vehiclecostinput
        public List<VWI_CRM_xjp_vehiclecostinput> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_xjp_vehiclecostinput>();
        }
        #endregion

        #region Search VWI_CRM_xjp_vehiclecostinput        
        public new List<VWI_CRM_xjp_vehiclecostinput> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_xjp_vehiclecostinput.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.rowstatus", "isnull(a.rowstatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.company", "b.ktb_bucompany");
                strCriteria = strCriteria.ToLower().Replace("a.businessunitcode", "b.ktb_dealercode");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "b.msdyn_companycode");

                List<VWI_CRM_xjp_vehiclecostinput> result = SearchData<VWI_CRM_xjp_vehiclecostinput>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_xjp_vehiclecostinput>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_xjp_vehiclecostinputQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_xjp_vehiclecostinput.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_xjp_vehiclecostinputQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "xjp_vehiclecostinputid");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_xjp_vehiclecostinput>();
            }
        }
        #endregion

        public void SetCreatedLog(VWI_CRM_xjp_vehiclecostinput VWI_CRM_xjp_vehiclecostinput)
        {
            VWI_CRM_xjp_vehiclecostinput.createdbyname = UserLogin;
            VWI_CRM_xjp_vehiclecostinput.createdon = DateTime.Now;
            VWI_CRM_xjp_vehiclecostinput.RowStatus = 0;
            SetLastModifiedLog(VWI_CRM_xjp_vehiclecostinput);
        }

        public void SetLastModifiedLog(VWI_CRM_xjp_vehiclecostinput VWI_CRM_xjp_vehiclecostinput)
        {
            VWI_CRM_xjp_vehiclecostinput.modifiedbyname = UserLogin;
            VWI_CRM_xjp_vehiclecostinput.modifiedon = DateTime.Now;
        }


    }
}
