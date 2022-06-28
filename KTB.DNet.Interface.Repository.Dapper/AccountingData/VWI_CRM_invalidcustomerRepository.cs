#region Summary
// ===========================================================================
// AUTHOR        : BSI Code Generator
// PURPOSE       : VWI_CRM_invalidcustomer repository class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 03/02/2020 17:40:59
//
// ===========================================================================	
#endregion

#region Namespace Imports
using Dapper;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using KTB.DNet.Domain.Search;
using System.Collections;
using KTB.DNet.Interface.Repository.Dapper.AccountingData.SqlQuery.VWI_CRM_invalidcustomer;
#endregion

namespace KTB.DNet.Interface.Repository.Dapper.AccountingData
{
    public class VWI_CRM_invalidcustomerRepository : BaseDNetRepository<VWI_CRM_invalidcustomer>, IVWI_CRM_invalidcustomerRepository<VWI_CRM_invalidcustomer, int>
    {
        #region Constructor
        public VWI_CRM_invalidcustomerRepository(string connectionString)
            : base(connectionString)
        { }
        #endregion

        #region Create VWI_CRM_invalidcustomer
        /// <summary>
        /// Create VWI_CRM_invalidcustomer
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Create(VWI_CRM_invalidcustomer entity)
        {
            return null;
        }
        #endregion

        #region Update VWI_CRM_invalidcustomer
        /// <summary>
        /// Update VWI_CRM_invalidcustomer
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseMessage Update(VWI_CRM_invalidcustomer entity)
        {
            return null;
        }
        #endregion

        #region Delete VWI_CRM_invalidcustomer
        /// <summary>
        /// Delete VWI_CRM_invalidcustomer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessage Delete(int id)
        {
            return null;
        }
        #endregion

        #region Get VWI_CRM_invalidcustomer By Id
        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VWI_CRM_invalidcustomer Get(int id)
        {
            try
            {
                using (var cn = Connection)
                {
                    return cn.Query<VWI_CRM_invalidcustomer>(
                        VWI_CRM_invalidcustomerQuery.GetVWI_CRM_invalidcustomerById, new { Id = id }
                        ).SingleOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Get All VWI_CRM_invalidcustomer
        /// <summary>
        /// Get All VWI_CRM_invalidcustomer
        /// </summary>
        /// <returns></returns>
        public List<VWI_CRM_invalidcustomer> GetAll()
        {
            return null;
        }
        #endregion

        #region Search VWI_CRM_invalidcustomer
        public List<VWI_CRM_invalidcustomer> Search(DataTablePostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = 0;
            totalResultsCount = 0;
            return new List<VWI_CRM_invalidcustomer>();
        }
        #endregion

        #region Search VWI_CRM_invalidcustomer        
        public new List<VWI_CRM_invalidcustomer> Search(string strCriteria, string strInnerCriteria, string sortColumns, int page, int pageSize, out int filteredResultsCount, out int totalResultsCount)
        {
            try
            {
                string orderBy = sortColumns != string.Empty ? sortColumns.ToString() : null;
                filteredResultsCount = 0;
                strCriteria = strCriteria.ToLower().Replace("vwi_crm_invalidcustomer.", "a.");
                strCriteria = strCriteria.ToLower().Replace("a.RowStatus", "isnull(a.RowStatus, 0)");
                strCriteria = strCriteria.ToLower().Replace("a.msdyn_companycode", "isnull(b.msdyn_companycode, '')");
                strCriteria = strCriteria.ToLower().Replace("a.id", "a.accountid");
                strCriteria = strCriteria.ToLower().Replace("a.invalididcard", "a.Error_ID_Card");
                strCriteria = strCriteria.ToLower().Replace("a.invalididcard_length", "a.InvalidLength");
                strCriteria = strCriteria.ToLower().Replace("a.invalididcard_format", "a.InvalidFormat");
                strCriteria = strCriteria.ToLower().Replace("a.invalididcard_duplicate", "a.Duplicate");
                strCriteria = strCriteria.ToLower().Replace("a.invalididcard_dummy", "a.Dummy");
                strCriteria = strCriteria.ToLower().Replace("a.invalidmobile", "a.Error_Mobile_Nbr");
                strCriteria = strCriteria.ToLower().Replace("a.invalidmobile_blank", "a.InvalidBlankMobile");
                strCriteria = strCriteria.ToLower().Replace("a.invalidmobile_length", "a.InvalidLengthMobile");
                strCriteria = strCriteria.ToLower().Replace("a.invalidmobile_format", "a.InvalidFormatMobile");
                strCriteria = strCriteria.ToLower().Replace("a.invalidmobile_duplicate", "a.DuplicateMobile");
                strCriteria = strCriteria.ToLower().Replace("a.invalidmobile_dummy", "a.DummyMobile");
                strCriteria = strCriteria.ToLower().Replace("a.invalidbirthdate", "a.Error_BirthDate");
                strCriteria = strCriteria.ToLower().Replace("a.invalidbirthdate_blank", "a.InvalidBlankBirthDate");
                strCriteria = strCriteria.ToLower().Replace("a.invalidbirthdate_range", "a.InvalidRangeBD");
                strCriteria = strCriteria.ToLower().Replace("a.invalidemail", "a.Error_EmailAddress");
                strCriteria = strCriteria.ToLower().Replace("a.invalidemail_blank", "a.InvalidBlankEmail");
                strCriteria = strCriteria.ToLower().Replace("a.invalidemail_length", "a.InvalidLengthEmail");
                strCriteria = strCriteria.ToLower().Replace("a.invalidemail_format", "a.InvalidFormatEmail");
                strCriteria = strCriteria.ToLower().Replace("a.invalidemail_duplicate", "a.DuplicateEmail");
                strCriteria = strCriteria.ToLower().Replace("a.invalidcustclass", "a.Error_Customer_Class");
                strCriteria = strCriteria.ToLower().Replace("a.invalidcustclass_blank", "a.InvalidBlankCustClass");
                strCriteria = strCriteria.ToLower().Replace("a.invalidcustclass_data", "a.InvalidDataCustClass");
                strCriteria = strCriteria.ToLower().Replace("a.id_card_error_reason", "a.ID_Card_Error_Reason");
                strCriteria = strCriteria.ToLower().Replace("a.mobile_nbr_error_reason", "a.Mobile_Nbr_Error_Reason");
                strCriteria = strCriteria.ToLower().Replace("a.birth_date_error_reason", "a.Birth_Date_Error_Reason");
                strCriteria = strCriteria.ToLower().Replace("a.email_address_error_reason", "a.Email_Address_Error_Reason");
                strCriteria = strCriteria.ToLower().Replace("a.customer_class_error_reason", "a.Customer_Class_Error_Reason");



                List<VWI_CRM_invalidcustomer> result = SearchData<VWI_CRM_invalidcustomer>((connection, query, sqlParams) =>
                {
                    return connection.Query<VWI_CRM_invalidcustomer>(query, sqlParams, null, true, Timeout, null).ToList();
                }, Connection, string.Format(VWI_CRM_invalidcustomerQuery.SelectQuery,
                                                strCriteria,
                                                strInnerCriteria)
                , "VWI_CRM_invalidcustomer.modifiedon desc", null, orderBy, out filteredResultsCount, page, pageSize,
                string.Format(VWI_CRM_invalidcustomerQuery.GetTotalQuery,
                                                strCriteria,
                                                strInnerCriteria), "IDRow");

                totalResultsCount = filteredResultsCount;

                return result;

            }
            catch (Exception ex)
            {
                filteredResultsCount = 0;
                totalResultsCount = 0;
                return new List<VWI_CRM_invalidcustomer>();
            }
        }
        #endregion

        protected void SetCreatedLog(VWI_CRM_invalidcustomer vWI_CRM_invalidcustomer)
        {
            //vWI_CRM_invalidcustomer.CreatedBy = UserLogin;
            //vWI_CRM_invalidcustomer.CreatedTime = DateTime.Now;
            //SetLastModifiedLog(vWI_CRM_invalidcustomer);
        }

        protected void SetLastModifiedLog(VWI_CRM_invalidcustomer vWI_CRM_invalidcustomer)
        {
            //vWI_CRM_invalidcustomer.LastUpdateBy = UserLogin;
            //vWI_CRM_invalidcustomer.LastUpdateTime = DateTime.Now;
        }
    }
}
