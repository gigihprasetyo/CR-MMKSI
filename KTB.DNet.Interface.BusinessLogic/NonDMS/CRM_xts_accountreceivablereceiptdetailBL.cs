#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_accountreceivablereceiptdetail class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV 
 GENERATED BY  : Gugun (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 04 Sep 2020 10:27:39
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
    public class CRM_xts_accountreceivablereceiptdetailBL : AbstractBusinessLogic, ICRM_xts_accountreceivablereceiptdetailBL
    {
        #region Variables
        private ICRM_xts_accountreceivablereceiptdetailRepository<CRM_xts_accountreceivablereceiptdetail, int> _CRM_xts_accountreceivablereceiptdetailRepo;
        #endregion

        #region Constructor
        public CRM_xts_accountreceivablereceiptdetailBL(ICRM_xts_accountreceivablereceiptdetailRepository<CRM_xts_accountreceivablereceiptdetail, int> CRM_xts_accountreceivablereceiptdetailRepo)
        {
            _CRM_xts_accountreceivablereceiptdetailRepo = CRM_xts_accountreceivablereceiptdetailRepo;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Get CRM_xts_accountreceivablereceiptdetail by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<CRM_xts_accountreceivablereceiptdetailDto>> Read(CRM_xts_accountreceivablereceiptdetailFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<CRM_xts_accountreceivablereceiptdetailDto>> ReadList(CRM_xts_accountreceivablereceiptdetailFilterDto filterDto, int pageSize)
        {

            var result = new ResponseBase<List<CRM_xts_accountreceivablereceiptdetailDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                var criterias = Helper.InitialStrCriteria(typeof(CRM_xts_accountreceivablereceiptdetail), filterDto);

                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_accountreceivablereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_accountreceivablereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_accountreceivablereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(CRM_xts_accountreceivablereceiptdetail), filterDto, "CRM_xts_accountreceivablereceiptdetail");

                List<CRM_xts_accountreceivablereceiptdetail> data = _CRM_xts_accountreceivablereceiptdetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    CRM_xts_accountreceivablereceiptdetail x = new CRM_xts_accountreceivablereceiptdetail();
                    result.lst = data.ConvertList<CRM_xts_accountreceivablereceiptdetail, CRM_xts_accountreceivablereceiptdetailDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(CRM_xts_accountreceivablereceiptdetail), filterDto);
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
        /// Create a new CRM_xts_accountreceivablereceiptdetail
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_accountreceivablereceiptdetailDto> Create(CRM_xts_accountreceivablereceiptdetailParameterDto objCreate)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_accountreceivablereceiptdetailDto> Create(CRM_xts_accountreceivablereceiptdetailCreateParameterDto paramCreate)
        {
            var result = new ResponseBaseCustom<CRM_xts_accountreceivablereceiptdetailDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_accountreceivablereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_accountreceivablereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_accountreceivablereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_accountreceivablereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_accountreceivablereceiptdetail", paramCreate.xts_accountreceivablereceiptdetail.ToString(), false, criterias);

                List<CRM_xts_accountreceivablereceiptdetail> data = _CRM_xts_accountreceivablereceiptdetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data != null && data.Count == 0)
                {
                    PropertyInfo[] properties = typeof(CRM_xts_accountreceivablereceiptdetailCreateParameterDto).GetProperties();
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

                    CRM_xts_accountreceivablereceiptdetail createRow = paramCreate.ConvertObject<CRM_xts_accountreceivablereceiptdetail>();
                    Guid newId = Guid.NewGuid();
                    createRow.xts_accountreceivablereceiptdetailid = newId;
                    createRow.DealerCode = DealerCode;
                    createRow.SourceType = "NON DMS"; createRow.createdby = (createRow.createdby == null || createRow.createdby.ToString() == "00000000-0000-0000-0000-000000000000") ? createRow.modifiedby : createRow.createdby;
                    createRow.RowStatus = "0"; createRow.statecode = 0;

                    //Add Validation Data Input
                    ValidationParameterDto paramCheck = new ValidationParameterDto();
                    paramCheck.dealerCode = DealerCode;
                    paramCheck.xts_businessunitid = paramCreate.xts_businessunitid;
                    paramCheck.ownerid = paramCreate.ownerid;
                    paramCheck.xts_accountreceivablereceiptid = paramCreate.xts_accountreceivablereceiptid;
                    if (paramCreate.xts_source == "1")       // xts_source = 1 (new vehicle mandatory to check spkdetail id)
                    {
                        paramCheck.xts_ordernvsoid = paramCreate.xts_ordernvsoid;
                    };
                    //paramCheck.xts_transactiondocumentid = paramCreate.xts_transactiondocumentid;

                    if (!ValidationHelper.ValidateDataInput(paramCheck, validationResults))
                    {
                        ValidationDapper dapper = new ValidationDapper();
                        List<MessageBase> errMsg = dapper.messageList(validationResults);
                        result.messages = errMsg;
                    }
                    else
                    {
                        var nResult = _CRM_xts_accountreceivablereceiptdetailRepo.Create(createRow);
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
                    ErrorMsgHelper.Exception(result.messages, String.Concat("Data already exist | " + data[0].xts_accountreceivablereceiptdetailid.ToString()));
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



        public ResponseBase<List<CRM_xts_accountreceivablereceiptdetailDto>> BulkCreate(List<CRM_xts_accountreceivablereceiptdetailCreateParameterDto> lstObjCreate)
        {
            var result = new ResponseBase<List<CRM_xts_accountreceivablereceiptdetailDto>>();
            var listOfExistingData = new List<CRM_xts_accountreceivablereceiptdetailDto>();
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
                    return PopulateValidationError<List<CRM_xts_accountreceivablereceiptdetailDto>>(validationResults.Distinct().ToList(), null);
                }
                else
                {

                    // insert into db
                    var CRM_xts_accountreceivablereceiptdetailList = lstObjCreate.ConvertList<CRM_xts_accountreceivablereceiptdetailCreateParameterDto, CRM_xts_accountreceivablereceiptdetail>();
                    foreach (var item in CRM_xts_accountreceivablereceiptdetailList.Select((value, index) => new { value, index }))
                    {
                        PropertyInfo[] properties = typeof(CRM_xts_accountreceivablereceiptdetail).GetProperties();
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
                        item.value.xts_accountreceivablereceiptdetailid = newId;
                        item.value.DealerCode = DealerCode;
                        item.value.SourceType = "NON DMS"; item.value.createdby = (item.value.createdby == null || item.value.createdby.ToString() == "00000000-0000-0000-0000-000000000000") ? item.value.modifiedby : item.value.createdby;
                        item.value.RowStatus = "0"; 
                        item.value.statecode = item.value.statecode == null ? 0 : item.value.statecode;

                        //Add Validation Data Input
                        ValidationParameterDto paramCheck = new ValidationParameterDto();
                        paramCheck.dealerCode = DealerCode;
                        paramCheck.xts_businessunitid = item.value.xts_businessunitid;
                        paramCheck.ownerid = item.value.ownerid;
                        paramCheck.xts_accountreceivablereceiptid = item.value.xts_accountreceivablereceiptid;
                        if(item.value.xts_source == "1")         // xts_source = 1 (new vehicle mandatory to check spkdetail id)
                        {
                            paramCheck.xts_ordernvsoid = item.value.xts_ordernvsoid;
                        }                        
                        //paramCheck.xts_transactiondocumentid = item.value.xts_transactiondocumentid;

                        if (ValidationHelper.ValidateDataInput(paramCheck, validationResults, item.index, true))
                        {
                            //_CRM_xts_accountreceivablereceiptdetailRepo.SetCreatedLog(item.value);
                        }
                    }

                    if (validationResults.Any())
                    {
                        return PopulateValidationError<List<CRM_xts_accountreceivablereceiptdetailDto>>(validationResults.Distinct().ToList(), null);
                    }
                    else
                    {
                        var isSuccess = _CRM_xts_accountreceivablereceiptdetailRepo.BulkInsert(CRM_xts_accountreceivablereceiptdetailList);
                        result.success = isSuccess;
                        if (result.success)
                            result.total = CRM_xts_accountreceivablereceiptdetailList.Count;
                        result.lst = CRM_xts_accountreceivablereceiptdetailList.ConvertList<CRM_xts_accountreceivablereceiptdetail, CRM_xts_accountreceivablereceiptdetailDto>();
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
        /// Update CRM_xts_accountreceivablereceiptdetail
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_accountreceivablereceiptdetailDto> Update(CRM_xts_accountreceivablereceiptdetailParameterDto paramUpdate)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_accountreceivablereceiptdetailDto> Update(CRM_xts_accountreceivablereceiptdetailUpdateParameterDto paramUpdate)
        {
            var result = new ResponseBaseCustom<CRM_xts_accountreceivablereceiptdetailDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_accountreceivablereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_accountreceivablereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_accountreceivablereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_accountreceivablereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_accountreceivablereceiptdetailid", paramUpdate.xts_accountreceivablereceiptdetailid.ToString(), false, criterias);
                List<CRM_xts_accountreceivablereceiptdetail> data = _CRM_xts_accountreceivablereceiptdetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    PropertyInfo[] properties = typeof(CRM_xts_accountreceivablereceiptdetailUpdateParameterDto).GetProperties();
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
                    paramCheck.xts_businessunitid = paramUpdate.xts_businessunitid;
                    paramCheck.ownerid = paramUpdate.ownerid;
                    paramCheck.xts_accountreceivablereceiptid = paramUpdate.xts_accountreceivablereceiptid;
                    if(paramUpdate.xts_source == "1")       // xts_source = 1 (new vehicle mandatory to check spkdetail id)
                    {
                        paramCheck.xts_ordernvsoid = paramUpdate.xts_ordernvsoid;
                    }
                    //paramCheck.xts_transactiondocumentid = paramUpdate.xts_transactiondocumentid;

                    if (!ValidationHelper.ValidateDataInput(paramCheck, validationResults))
                    {
                        ValidationDapper dapper = new ValidationDapper();
                        List<MessageBase> errMsg = dapper.messageList(validationResults);
                        result.messages = errMsg;
                    }
                    else
                    {
                        CRM_xts_accountreceivablereceiptdetail updateRow = paramUpdate.ConvertObject<CRM_xts_accountreceivablereceiptdetail>();
                        updateRow.DealerCode = DealerCode;
                        updateRow.SourceType = "NON DMS";
                        updateRow.RowStatus = string.IsNullOrEmpty(updateRow.RowStatus) ? "0" : updateRow.RowStatus;

                        var nResult = _CRM_xts_accountreceivablereceiptdetailRepo.Update(updateRow);
                        if (nResult.Success == true)
                        {
                            result.success = true;
                            result._idGuid = nResult.Data.ConvertObject<CRM_xts_accountreceivablereceiptdetail>().xts_accountreceivablereceiptdetailid;
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
        /// Delete CRM_xts_accountreceivablereceiptdetail by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_accountreceivablereceiptdetailDto> Delete(int ID)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_accountreceivablereceiptdetailDto> Delete(CRM_xts_accountreceivablereceiptdetailDeleteParameterDto paramDelete)
        {
            var result = new ResponseBaseCustom<CRM_xts_accountreceivablereceiptdetailDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_accountreceivablereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_accountreceivablereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_accountreceivablereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_accountreceivablereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_accountreceivablereceiptdetailid", paramDelete.xts_accountreceivablereceiptdetailid.ToString(), false, criterias);

                List<CRM_xts_accountreceivablereceiptdetail> data = _CRM_xts_accountreceivablereceiptdetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);
                if (data != null && data.Count > 0)
                {
                    CRM_xts_accountreceivablereceiptdetail deleteRow = data[0];
                    deleteRow.RowStatus = "-1"; deleteRow.modifiedby = paramDelete.modifiedby; deleteRow.modifiedon = Convert.ToDateTime(paramDelete.modifiedon).AddHours(-7);
                    var nResult = _CRM_xts_accountreceivablereceiptdetailRepo.Update(deleteRow);
                    if (nResult.Success == true)
                    {
                        result.success = true;
                        result._idGuid = nResult.Data.ConvertObject<CRM_xts_accountreceivablereceiptdetail>().xts_accountreceivablereceiptdetailid;
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
        private bool ValidateDuplicateParamData(List<CRM_xts_accountreceivablereceiptdetailCreateParameterDto> lstObjCreate, List<DNetValidationResult> validationResults)
        {
            foreach (var item in lstObjCreate)
            {
                var lst = lstObjCreate.Where(x => x.xts_accountreceivablereceiptdetail.ToString().Trim() == item.xts_accountreceivablereceiptdetail.ToString().Trim());
                if (lst.Count() > 1)
                {
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDuplicateData) + string.Format(" ({0})", "xts_accountreceivablereceiptdetail = " + item.xts_accountreceivablereceiptdetail.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }
            return validationResults.Count == 0;
        }

        private bool ValidateDuplicateDataInDB(List<CRM_xts_accountreceivablereceiptdetailCreateParameterDto> CRM_xts_accountreceivablereceiptdetailList, List<DNetValidationResult> validationResults, ref List<CRM_xts_accountreceivablereceiptdetailDto> listOfExistingData)
        {
            listOfExistingData = new List<CRM_xts_accountreceivablereceiptdetailDto>();

            foreach (CRM_xts_accountreceivablereceiptdetailCreateParameterDto item in CRM_xts_accountreceivablereceiptdetailList)
            {
                //check duplicate
                List<CRM_xts_accountreceivablereceiptdetail> duplicateinDBlist = GetExistingCRM_xts_accountreceivablereceiptdetail(item);
                if (duplicateinDBlist != null)
                {
                    CRM_xts_accountreceivablereceiptdetail duplicateinDB = duplicateinDBlist.FirstOrDefault();
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDataDBExist) + string.Format(" ({0},{1})", "xts_accountreceivablereceiptdetail = " + item.xts_accountreceivablereceiptdetail.ToString(), "statecode=0" + " | " + duplicateinDB.xts_accountreceivablereceiptdetailid.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }

            return validationResults.Count == 0;
        }

        private List<CRM_xts_accountreceivablereceiptdetail> GetExistingCRM_xts_accountreceivablereceiptdetail(CRM_xts_accountreceivablereceiptdetailCreateParameterDto param)
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_accountreceivablereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_accountreceivablereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_accountreceivablereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_accountreceivablereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_accountreceivablereceiptdetail", param.xts_accountreceivablereceiptdetail.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_accountreceivablereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "statecode", "0", false, criterias);

                List<CRM_xts_accountreceivablereceiptdetail> data = _CRM_xts_accountreceivablereceiptdetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data.Count > 0)
                {
                    return data.Cast<CRM_xts_accountreceivablereceiptdetail>().ToList();
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