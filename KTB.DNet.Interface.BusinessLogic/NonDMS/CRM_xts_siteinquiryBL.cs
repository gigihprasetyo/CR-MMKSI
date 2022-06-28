#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_siteinquiry class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV 
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 19 Jan 2021 09:47:11
 ===========================================================================
*/
#endregion


#region Namespace Imports
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using KTB.DNet.Interface.Framework;
using AutoMapper;
using System.Reflection;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class CRM_xts_siteinquiryBL : AbstractBusinessLogic, ICRM_xts_siteinquiryBL
    {
        #region Variables
        private ICRM_xts_siteinquiryRepository<CRM_xts_siteinquiry, int> _CRM_xts_siteinquiryRepo;
        #endregion

        #region Constructor
        public CRM_xts_siteinquiryBL(ICRM_xts_siteinquiryRepository<CRM_xts_siteinquiry, int> CRM_xts_siteinquiryRepo)
        {
            _CRM_xts_siteinquiryRepo = CRM_xts_siteinquiryRepo;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Get CRM_xts_siteinquiry by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<CRM_xts_siteinquiryDto>> Read(CRM_xts_siteinquiryFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<CRM_xts_siteinquiryDto>> ReadList(CRM_xts_siteinquiryFilterDto filterDto, int pageSize)
        {

            var result = new ResponseBase<List<CRM_xts_siteinquiryDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                var criterias = Helper.InitialStrCriteria(typeof(CRM_xts_siteinquiry), filterDto);

                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_siteinquiry), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_siteinquiry), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_siteinquiry), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(CRM_xts_siteinquiry), filterDto, "CRM_xts_siteinquiry");

                List<CRM_xts_siteinquiry> data = _CRM_xts_siteinquiryRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    CRM_xts_siteinquiry x = new CRM_xts_siteinquiry();
                    result.lst = data.ConvertList<CRM_xts_siteinquiry, CRM_xts_siteinquiryDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(CRM_xts_siteinquiry), filterDto);
                }

                result.success = true;

            }
            catch (SqlException ex)
            {
                ErrorMsgHelper.SqlExceptionRead(result.messages, ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }
            return result;

        }

        /// <summary>
        /// Create a new CRM_xts_siteinquiry
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_siteinquiryDto> Create(CRM_xts_siteinquiryParameterDto objCreate)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_siteinquiryDto> Create(CRM_xts_siteinquiryCreateParameterDto paramCreate)
        {
            var result = new ResponseBaseCustom<CRM_xts_siteinquiryDto>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;
            var innerQueryCriteria = string.Empty;
            var page = 1;
            var size = 20;
            var validationResults = new List<DNetValidationResult>();

            try
            {

                var criterias = "";
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_siteinquiry), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_siteinquiry), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_siteinquiry), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_siteinquiry), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_siteid", paramCreate.xts_siteid.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_siteinquiry), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_productid", paramCreate.xts_productid.ToString(), false, criterias);

                List<CRM_xts_siteinquiry> data = _CRM_xts_siteinquiryRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data != null && data.Count == 0)
                {
                    PropertyInfo[] properties = typeof(CRM_xts_siteinquiryCreateParameterDto).GetProperties();
                    foreach (PropertyInfo property in properties)
                    {
                        if (property.PropertyType.FullName.Contains("DateTime"))
                        {
                            if (property.GetValue(paramCreate) != null && (property.GetValue(paramCreate).ToString() == "01/01/0001 0:00:00" || property.GetValue(paramCreate).ToString() == "01/01/1753 0:00:00"))
                            {
                                property.SetValue(paramCreate, null);
                            }

                            if (property.GetValue(paramCreate) != null && property.GetValue(paramCreate).ToString() != "01/01/0001 0:00:00")
                            {
                                DateTime datetimeUTCzero = Convert.ToDateTime(property.GetValue(paramCreate)).AddHours(-7);
                                property.SetValue(paramCreate, datetimeUTCzero);
                            }
                        }
                    }

                    CRM_xts_siteinquiry createRow = paramCreate.ConvertObject<CRM_xts_siteinquiry>();
                    Guid newId = Guid.NewGuid();
                    createRow.xts_siteinquiryid = newId;
                    createRow.DealerCode = DealerCode;
                    createRow.SourceType = "NON DMS"; createRow.createdby = (createRow.createdby == null || createRow.createdby.ToString() == "00000000-0000-0000-0000-000000000000") ? createRow.modifiedby : createRow.createdby;
                    createRow.RowStatus = 0; createRow.statecode = 0;

                    //Add Validation Data Input
                    ValidationParameterDto paramCheck = new ValidationParameterDto();
                    paramCheck.dealerCode = DealerCode;
                    //paramCheck.xts_siteid = paramCreate.xts_siteid;

                    ValidationDapper dapper_ = new ValidationDapper();
                    string xts_companyByDealerCode = dapper_.getBUbyDealerCode(DealerCode).msdyn_companycode;
                    if (createRow.xts_company.ToLower() != xts_companyByDealerCode.ToLower())
                    {
                        createRow.xts_company = xts_companyByDealerCode;
                    }

                    if (!ValidationHelper.ValidateDataInput(paramCheck, validationResults))
                    {
                        ValidationDapper dapper = new ValidationDapper();
                        List<MessageBase> errMsg = dapper.messageList(validationResults);
                        result.messages = errMsg;
                    }
                    else
                    {
                        var nResult = _CRM_xts_siteinquiryRepo.Create(createRow);
                        if (nResult.Success == true)
                        {
                            result.success = true;
                            result._idGuid = newId;
                            result.total = 1;
                        }
                        else
                        {
                            ErrorMsgHelper.ErrorMsgDBSave(result.messages);
                        }
                    }
                }
                else
                {
                    ErrorMsgHelper.Exception(result.messages, String.Concat("Data already exist | " + data[0].xts_siteinquiryid.ToString()));
                }

            }
            catch (SqlException ex)
            {
                ErrorMsgHelper.SqlException(result.messages, ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }



        public ResponseBase<List<CRM_xts_siteinquiryDto>> BulkCreate(List<CRM_xts_siteinquiryCreateParameterDto> lstObjCreate)
        {
            var result = new ResponseBase<List<CRM_xts_siteinquiryDto>>();
            var listOfExistingData = new List<CRM_xts_siteinquiryDto>();
            var validationResults = new List<DNetValidationResult>();

            try
            {

                if (lstObjCreate.Count > 0)
                {

                    if (ValidateDuplicateParamData(lstObjCreate, validationResults) == true)
                    {
                        // validate any duplicate data in database
                        ValidateDuplicateDataInDB(lstObjCreate, validationResults, ref listOfExistingData);
                    }
                }

                // if any error found
                if (validationResults.Any())
                {
                    return PopulateValidationError<List<CRM_xts_siteinquiryDto>>(validationResults.Distinct().ToList(), null);
                }
                else
                {
                    // insert into db
                    var CRM_xts_siteinquiryList = lstObjCreate.ConvertList<CRM_xts_siteinquiryCreateParameterDto, CRM_xts_siteinquiry>();
                    foreach (var item in CRM_xts_siteinquiryList.Select((value, index) => new { value, index }))
                    {
                        PropertyInfo[] properties = typeof(CRM_xts_siteinquiry).GetProperties();
                        foreach (PropertyInfo property in properties)
                        {
                            if (property.PropertyType.FullName.Contains("DateTime"))
                            {
                                if (property.GetValue(item.value) != null && (property.GetValue(item.value).ToString() == "01/01/0001 0:00:00" || property.GetValue(item.value).ToString() == "01/01/1753 0:00:00"))
                                {
                                    property.SetValue(item.value, null);
                                }

                                if (property.GetValue(item.value) != null && property.GetValue(item.value).ToString() != "01/01/0001 0:00:00")
                                {
                                    DateTime datetimeUTCzero = Convert.ToDateTime(property.GetValue(item.value)).AddHours(-7);
                                    property.SetValue(item.value, datetimeUTCzero);
                                }
                            }
                        }

                        Guid newId = Guid.NewGuid();
                        item.value.xts_siteinquiryid = newId;
                        item.value.DealerCode = DealerCode;
                        item.value.SourceType = "NON DMS"; item.value.createdby = (item.value.createdby == null || item.value.createdby.ToString() == "00000000-0000-0000-0000-000000000000") ? item.value.modifiedby : item.value.createdby;
                        item.value.RowStatus = 0; 
                        item.value.statecode = item.value.statecode == null ? 0 : item.value.statecode;

                        //Add Validation Data Input
                        ValidationParameterDto paramCheck = new ValidationParameterDto();
                        paramCheck.dealerCode = DealerCode;
                        //paramCheck.xts_siteid = item.value.xts_siteid;

                        ValidationDapper dapper_ = new ValidationDapper();
                        string xts_companyByDealerCode = dapper_.getBUbyDealerCode(DealerCode).msdyn_companycode;
                        if (item.value.xts_company.ToLower() != xts_companyByDealerCode.ToLower())
                        {
                            item.value.xts_company = xts_companyByDealerCode;
                        }

                        if (ValidationHelper.ValidateDataInput(paramCheck, validationResults, item.index, true))
                        {
                            //_CRM_xts_siteinquiryRepo.SetCreatedLog(item.value);
                        }
                    }

                    if (validationResults.Any())
                    {
                        return PopulateValidationError<List<CRM_xts_siteinquiryDto>>(validationResults.Distinct().ToList(), null);
                    }
                    else
                    {
                        var isSuccess = _CRM_xts_siteinquiryRepo.BulkInsert(CRM_xts_siteinquiryList);
                        result.success = isSuccess;
                        if (result.success)
                            result.total = CRM_xts_siteinquiryList.Count;
                        result.lst = CRM_xts_siteinquiryList.ConvertList<CRM_xts_siteinquiry, CRM_xts_siteinquiryDto>();
                    }
                }
            }
            catch (SqlException ex)
            {
                ErrorMsgHelper.SqlException(result.messages, ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }


        /// <summary>
        /// Update CRM_xts_siteinquiry
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_siteinquiryDto> Update(CRM_xts_siteinquiryParameterDto paramUpdate)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_siteinquiryDto> Update(CRM_xts_siteinquiryUpdateParameterDto paramUpdate)
        {
            var result = new ResponseBaseCustom<CRM_xts_siteinquiryDto>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;
            var innerQueryCriteria = string.Empty;
            var page = 1;
            var size = 20;
            var validationResults = new List<DNetValidationResult>();

            try
            {
                var criterias = "";
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_siteinquiry), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_siteinquiry), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_siteinquiry), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_siteinquiry), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_siteinquiryid", paramUpdate.xts_siteinquiryid.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_siteinquiry), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_productid", paramUpdate.xts_productid.ToString(), false, criterias);
                List<CRM_xts_siteinquiry> data = _CRM_xts_siteinquiryRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    PropertyInfo[] properties = typeof(CRM_xts_siteinquiryUpdateParameterDto).GetProperties();
                    foreach (PropertyInfo property in properties)
                    {
                        if (property.PropertyType.FullName.Contains("DateTime"))
                        {
                            if (property.GetValue(paramUpdate) != null && (property.GetValue(paramUpdate).ToString() == "01/01/0001 0:00:00" || property.GetValue(paramUpdate).ToString() == "01/01/1753 0:00:00"))
                            {
                                property.SetValue(paramUpdate, null);
                            }

                            if (property.GetValue(paramUpdate) != null && property.GetValue(paramUpdate).ToString() != "01/01/0001 0:00:00")
                            {
                                DateTime datetimeUTCzero = Convert.ToDateTime(property.GetValue(paramUpdate)).AddHours(-7);
                                property.SetValue(paramUpdate, datetimeUTCzero);
                            }
                        }
                    }

                    //Add Validation Data Input
                    ValidationParameterDto paramCheck = new ValidationParameterDto();
                    paramCheck.dealerCode = DealerCode;
                    //paramCheck.xts_siteid = paramUpdate.xts_siteid;

                    if (!ValidationHelper.ValidateDataInput(paramCheck, validationResults))
                    {
                        ValidationDapper dapper = new ValidationDapper();
                        List<MessageBase> errMsg = dapper.messageList(validationResults);
                        result.messages = errMsg;
                    }
                    else
                    {
                        CRM_xts_siteinquiry updateRow = paramUpdate.ConvertObject<CRM_xts_siteinquiry>();
                        updateRow.DealerCode = DealerCode;
                        updateRow.SourceType = "NON DMS";
                        updateRow.RowStatus = updateRow.RowStatus == null ? 0 : updateRow.RowStatus;

                        ValidationDapper dapper_ = new ValidationDapper();
                        string xts_companyByDealerCode = dapper_.getBUbyDealerCode(DealerCode).msdyn_companycode;
                        if (updateRow.xts_company.ToLower() != xts_companyByDealerCode.ToLower())
                        {
                            updateRow.xts_company = xts_companyByDealerCode;
                        }

                        var nResult = _CRM_xts_siteinquiryRepo.Update(updateRow);
                        if (nResult.Success == true)
                        {
                            result.success = true;
                            result._idGuid = nResult.Data.ConvertObject<CRM_xts_siteinquiry>().xts_siteinquiryid;
                            result.total = 1;
                        }
                        else
                        {
                            ErrorMsgHelper.ErrorMsgDBSave(result.messages);
                        }
                    }
                }
                else
                {
                    ErrorMsgHelper.UpdateNotAvailable(result.messages);
                }
            }
            catch (SqlException ex)
            {
                ErrorMsgHelper.SqlException(result.messages, ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }


        /// <summary>
        /// Delete CRM_xts_siteinquiry by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_siteinquiryDto> Delete(int ID)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_siteinquiryDto> Delete(CRM_xts_siteinquiryDeleteParameterDto paramDelete)
        {
            var result = new ResponseBaseCustom<CRM_xts_siteinquiryDto>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;
            var innerQueryCriteria = string.Empty;
            var page = 1;
            var size = 20;
            try
            {
                var criterias = "";
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_siteinquiry), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_siteinquiry), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_siteinquiry), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_siteinquiry), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_siteinquiryid", paramDelete.xts_siteinquiryid.ToString(), false, criterias);

                List<CRM_xts_siteinquiry> data = _CRM_xts_siteinquiryRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);
                if (data != null && data.Count > 0)
                {
                    CRM_xts_siteinquiry deleteRow = data[0];
                    deleteRow.RowStatus = -1; deleteRow.modifiedby = paramDelete.modifiedby; deleteRow.modifiedon = Convert.ToDateTime(paramDelete.modifiedon).AddHours(-7);
                    var nResult = _CRM_xts_siteinquiryRepo.Update(deleteRow);
                    if (nResult.Success == true)
                    {
                        result.success = true;
                        result._idGuid = nResult.Data.ConvertObject<CRM_xts_siteinquiry>().xts_siteinquiryid;
                        result.total = 1;
                    }
                    else
                    {
                        ErrorMsgHelper.ErrorMsgDBSave(result.messages);
                    }
                }
                else
                {
                    ErrorMsgHelper.DeleteNotAvailable(result.messages);
                }
            }
            catch (SqlException ex)
            {
                ErrorMsgHelper.SqlException(result.messages, ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }

        #endregion


        #region Private Methods
        private bool ValidateDuplicateParamData(List<CRM_xts_siteinquiryCreateParameterDto> lstObjCreate, List<DNetValidationResult> validationResults)
        {
            foreach (var item in lstObjCreate)
            {
                var lst = lstObjCreate.Where(x => x.xts_siteid.ToString().Trim() == item.xts_siteid.ToString().Trim() &&
                                                x.xts_productid.ToString().Trim() == item.xts_productid.ToString().Trim());
                if (lst.Count() > 1)
                {
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDuplicateData) + string.Format(" ({0},{1})", "xts_siteid = " + item.xts_siteid.ToString(), "xts_productid = " + item.xts_productid.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }
            return validationResults.Count == 0;
        }

        private bool ValidateDuplicateDataInDB(List<CRM_xts_siteinquiryCreateParameterDto> CRM_xts_siteinquiryList, List<DNetValidationResult> validationResults, ref List<CRM_xts_siteinquiryDto> listOfExistingData)
        {
            listOfExistingData = new List<CRM_xts_siteinquiryDto>();

            foreach (CRM_xts_siteinquiryCreateParameterDto item in CRM_xts_siteinquiryList)
            {
                //check duplicate
                List<CRM_xts_siteinquiry> duplicateinDBlist = GetExistingCRM_xts_siteinquiry(item);
                if (duplicateinDBlist != null)
                {
                    CRM_xts_siteinquiry duplicateinDB = duplicateinDBlist.FirstOrDefault();
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDataDBExist) + string.Format(" ({0},{1},{2})", "xts_siteid = " + item.xts_siteid.ToString(), "xts_productid = " + item.xts_productid.ToString(), "statecode=0" + " | " + duplicateinDB.xts_siteinquiryid.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }

            return validationResults.Count == 0;
        }

        private List<CRM_xts_siteinquiry> GetExistingCRM_xts_siteinquiry(CRM_xts_siteinquiryCreateParameterDto param)
        {
            try
            {
                String criterias = "";
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_siteinquiry), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_siteinquiry), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_siteinquiry), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_siteinquiry), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_siteid", param.xts_siteid.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_siteinquiry), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_productid", param.xts_productid.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_siteinquiry), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "statecode", "0", false, criterias);

                List<CRM_xts_siteinquiry> data = _CRM_xts_siteinquiryRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data.Count > 0)
                {
                    return data.Cast<CRM_xts_siteinquiry>().ToList();
                }

                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion


    }
}
