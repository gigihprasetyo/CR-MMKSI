#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_account class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV 
 GENERATED BY  : Nando
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 31 Aug 2020 17:24:46
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
    public class CRM_accountBL : AbstractBusinessLogic, ICRM_accountBL
    {
        #region Variables
        private ICRM_accountRepository<CRM_account, int> _CRM_accountRepo;
        #endregion

        #region Constructor
        public CRM_accountBL(ICRM_accountRepository<CRM_account, int> CRM_accountRepo)
        {
            _CRM_accountRepo = CRM_accountRepo;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Get CRM_account by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<CRM_accountDto>> Read(CRM_accountFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<CRM_accountDto>> ReadList(CRM_accountFilterDto filterDto, int pageSize)
        {

            var result = new ResponseBase<List<CRM_accountDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                var criterias = Helper.InitialStrCriteria(typeof(CRM_account), filterDto);

                criterias = Helper.UpdateStrCriteria(typeof(CRM_account), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_account), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_account), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(CRM_account), filterDto, "CRM_account");

                List<CRM_account> data = _CRM_accountRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    CRM_account x = new CRM_account();
                    result.lst = data.ConvertList<CRM_account, CRM_accountDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(CRM_account), filterDto);
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
        /// Create a new CRM_account
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<CRM_accountDto> Create(CRM_accountParameterDto objCreate)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_accountDto> Create(CRM_accountCreateParameterDto paramCreate)
        {
            var result = new ResponseBaseCustom<CRM_accountDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_account), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_account), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_account), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_account), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "accountnumber", paramCreate.accountnumber.ToString(), false, criterias);
                //criterias = Helper.UpdateStrCriteria(typeof(CRM_account), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "ktb_customercodesap", paramCreate.ktb_customercodesap.ToString(), false, criterias);
                //criterias = Helper.UpdateStrCriteria(typeof(CRM_account), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "ktb_customerfleetcode", paramCreate.ktb_customerfleetcode.ToString(), false, criterias);
                if (paramCreate.xts_customerclasstype != 3) { criterias = Helper.UpdateStrCriteria(typeof(CRM_account), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_identificationnumber", paramCreate.xts_identificationnumber.ToString(), false, criterias); }

                List<CRM_account> data = _CRM_accountRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data != null && data.Count == 0)
                {
                    PropertyInfo[] properties = typeof(CRM_accountCreateParameterDto).GetProperties();
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

                    CRM_account createRow = paramCreate.ConvertObject<CRM_account>();
                    Guid newId = Guid.NewGuid();
                    createRow.accountid = newId;
                    createRow.DealerCode = DealerCode;
                    createRow.SourceType = "NON DMS"; createRow.createdby = (createRow.createdby == null || createRow.createdby.ToString() == "00000000-0000-0000-0000-000000000000") ? createRow.modifiedby : createRow.createdby; 
                    createRow.RowStatus = "0"; createRow.statecode = 0;
                    createRow.LastSyncDate = DateTime.Now;

                    //Add Validation Data Input
                    ValidationParameterDto paramCheck = new ValidationParameterDto();
                    #region Lookup
                    paramCheck.dealerCode = DealerCode;
                    paramCheck.xts_businessunitid = paramCreate.xts_businessunitid;
                    paramCheck.ownerid = paramCreate.ownerid;
                    paramCheck.xts_cityid = paramCreate.xts_cityid;
                    paramCheck.xts_customerclassid = paramCreate.xts_customerclassid;
                    paramCheck.xts_taxzoneid = paramCreate.xts_taxzoneid;
                    #endregion
                    #region PickList
                    List<ParamPick> param = new List<ParamPick>();
                    param.Add(new ParamPick { Category = "account.ktb_leveldata", ValueId = paramCreate.ktb_leveldata });
                    param.Add(new ParamPick { Category = "account.xts_customertype", ValueId = paramCreate.xts_customertype });
                    param.Add(new ParamPick { Category = "account.xts_customerclasstype", ValueId = paramCreate.xts_customerclasstype });
                    paramCheck.pickList = param;
                    #endregion

                    /* if not PartShop */
                    if (paramCreate.xts_customerclasstype != 3 && string.IsNullOrEmpty(paramCreate.xts_identificationnumber))
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, "xts_identificationnumber")));
                    }

                    if (!ValidationHelper.ValidateDataInput(paramCheck, validationResults))
                    {
                        ValidationDapper dapper = new ValidationDapper();
                        List<MessageBase> errMsg = dapper.messageList(validationResults);
                        result.messages = errMsg;
                    }
                    else
                    {
                        var nResult = _CRM_accountRepo.Create(createRow);
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
                    ErrorMsgHelper.Exception(result.messages, String.Concat("Data already exist | " + data[0].accountid.ToString()));
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



        public ResponseBase<List<CRM_accountDto>> BulkCreate(List<CRM_accountCreateParameterDto> lstObjCreate)
        {
            var result = new ResponseBase<List<CRM_accountDto>>();
            var listOfExistingData = new List<CRM_accountDto>();
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
                    return PopulateValidationError<List<CRM_accountDto>>(validationResults.Distinct().ToList(), null);
                }
                else
                {
                    // insert into db
                    var CRM_accountList = lstObjCreate.ConvertList<CRM_accountCreateParameterDto, CRM_account>();
                    foreach (var item in CRM_accountList.Select((value, index) => new { value, index }))
                    {
                        PropertyInfo[] properties = typeof(CRM_account).GetProperties();
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
                        item.value.accountid = newId;
                        item.value.DealerCode = DealerCode;
                        item.value.SourceType = "NON DMS"; item.value.createdby = (item.value.createdby == null || item.value.createdby.ToString() == "00000000-0000-0000-0000-000000000000") ? item.value.modifiedby : item.value.createdby; 
                        item.value.RowStatus = "0"; 
                        item.value.statecode = item.value.statecode == null ? 0 : item.value.statecode;
                        item.value.LastSyncDate = DateTime.Now;

                        //Add Validation Data Input
                        ValidationParameterDto paramCheck = new ValidationParameterDto();
                        #region Lookup
                        paramCheck.dealerCode = DealerCode;
                        paramCheck.xts_businessunitid = item.value.xts_businessunitid;
                        paramCheck.ownerid = item.value.ownerid;
                        paramCheck.xts_cityid = item.value.xts_cityid;
                        paramCheck.xts_customerclassid = item.value.xts_customerclassid;
                        paramCheck.xts_taxzoneid = item.value.xts_taxzoneid;
                        #endregion
                        #region PickList
                        List<ParamPick> param = new List<ParamPick>();
                        param.Add(new ParamPick { Category = "account.ktb_leveldata", ValueId = Convert.ToInt32(item.value.ktb_leveldata) });
                        param.Add(new ParamPick { Category = "account.xts_customertype", ValueId = Convert.ToInt32(item.value.xts_customertype) });
                        param.Add(new ParamPick { Category = "account.xts_customerclasstype", ValueId = Convert.ToInt32(item.value.xts_customerclasstype) });
                        paramCheck.pickList = param;
                        #endregion

                        /* if not PartShop */
                        if (Convert.ToInt32(item.value.xts_customerclasstype) != 3 && string.IsNullOrEmpty(item.value.xts_identificationnumber))
                        {
                            validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, "xts_identificationnumber")));
                        }

                        if (ValidationHelper.ValidateDataInput(paramCheck, validationResults, item.index, true))
                        {
                            //_CRM_accountRepo.SetCreatedLog(item.value);
                        }
                    }

                    if (validationResults.Any())
                    {
                        return PopulateValidationError<List<CRM_accountDto>>(validationResults.Distinct().ToList(), null);
                    }
                    else
                    {
                        var isSuccess = _CRM_accountRepo.BulkInsert(CRM_accountList);
                        result.success = isSuccess;
                        if (result.success)
                            result.total = CRM_accountList.Count;
                        result.lst = CRM_accountList.ConvertList<CRM_account, CRM_accountDto>();
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
        /// Update CRM_account
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<CRM_accountDto> Update(CRM_accountParameterDto paramUpdate)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_accountDto> Update(CRM_accountUpdateParameterDto paramUpdate)
        {
            var result = new ResponseBaseCustom<CRM_accountDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_account), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_account), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_account), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_account), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "accountid", paramUpdate.accountid.ToString(), false, criterias);
                List<CRM_account> data = _CRM_accountRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    PropertyInfo[] properties = typeof(CRM_accountUpdateParameterDto).GetProperties();
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
                    #region Lookup
                    paramCheck.dealerCode = DealerCode;
                    paramCheck.xts_businessunitid = paramUpdate.xts_businessunitid;
                    paramCheck.ownerid = paramUpdate.ownerid;
                    paramCheck.xts_cityid = paramUpdate.xts_cityid;
                    paramCheck.xts_customerclassid = paramUpdate.xts_customerclassid;
                    paramCheck.xts_taxzoneid = paramUpdate.xts_taxzoneid;
                    #endregion
                    #region PickList
                    List<ParamPick> param = new List<ParamPick>();
                    param.Add(new ParamPick { Category = "account.ktb_leveldata", ValueId = paramUpdate.ktb_leveldata });
                    param.Add(new ParamPick { Category = "account.xts_customertype", ValueId = paramUpdate.xts_customertype });
                    param.Add(new ParamPick { Category = "account.xts_customerclasstype", ValueId = paramUpdate.xts_customerclasstype });
                    paramCheck.pickList = param;
                    #endregion

                    /* if not PartShop */
                    if (paramUpdate.xts_customerclasstype != 3 && string.IsNullOrEmpty(paramUpdate.xts_identificationnumber))
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, "xts_identificationnumber")));
                    }

                    if (!ValidationHelper.ValidateDataInput(paramCheck, validationResults))
                    {
                        ValidationDapper dapper = new ValidationDapper();
                        List<MessageBase> errMsg = dapper.messageList(validationResults);
                        result.messages = errMsg;
                    }
                    else
                    {
                        CRM_account updateRow = paramUpdate.ConvertObject<CRM_account>();
                        updateRow.DealerCode = DealerCode;
                        updateRow.SourceType = "NON DMS";
                        updateRow.RowStatus = string.IsNullOrEmpty(updateRow.RowStatus) ? "0" : updateRow.RowStatus;
                        updateRow.LastSyncDate = DateTime.Now;

                        var nResult = _CRM_accountRepo.Update(updateRow);
                        if (nResult.Success == true)
                        {
                            result.success = true;
                            result._idGuid = nResult.Data.ConvertObject<CRM_account>().accountid;
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
        /// Delete CRM_account by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<CRM_accountDto> Delete(int ID)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_accountDto> Delete(CRM_accountDeleteParameterDto paramDelete)
        {
            var result = new ResponseBaseCustom<CRM_accountDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_account), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_account), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_account), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_account), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "accountid", paramDelete.accountid.ToString(), false, criterias);

                List<CRM_account> data = _CRM_accountRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);
                if (data != null && data.Count > 0)
                {
                    CRM_account deleteRow = data[0];
                    deleteRow.RowStatus = "-1"; deleteRow.modifiedby = paramDelete.modifiedby; deleteRow.modifiedon = Convert.ToDateTime(paramDelete.modifiedon).AddHours(-7);
                    deleteRow.LastSyncDate = DateTime.Now;
                    var nResult = _CRM_accountRepo.Update(deleteRow);
                    if (nResult.Success == true)
                    {
                        result.success = true;
                        result._idGuid = nResult.Data.ConvertObject<CRM_account>().accountid;
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
        private bool ValidateDuplicateParamData(List<CRM_accountCreateParameterDto> lstObjCreate, List<DNetValidationResult> validationResults)
        {
            foreach (var item in lstObjCreate)
            {
                if (item.xts_customerclasstype != 3)
                {
                    var lst = lstObjCreate.Where(x => x.accountnumber.ToString().Trim() == item.accountnumber.ToString().Trim() &&
                                                //x.ktb_customercodesap.ToString().Trim() == item.ktb_customercodesap.ToString().Trim() &&
                                                //x.ktb_customerfleetcode.ToString().Trim() == item.ktb_customerfleetcode.ToString().Trim() &&
                                                x.xts_identificationnumber.ToString().Trim() == item.xts_identificationnumber.ToString().Trim());
                    if (lst.Count() > 1)
                    {
                        //String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDuplicateData) + string.Format(" ({0},{1},{2},{3})", "accountnumber = " + item.accountnumber.ToString(), "ktb_customercodesap = " + item.ktb_customercodesap.ToString(), "ktb_customerfleetcode = " + item.ktb_customerfleetcode.ToString(), "xts_identificationnumber = " + item.xts_identificationnumber.ToString()));
                        String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDuplicateData) + string.Format(" ({0},{1})", "accountnumber = " + item.accountnumber.ToString(), "xts_identificationnumber = " + item.xts_identificationnumber.ToString()));
                        bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                        if (isErrorExist == false)
                        {
                            validationResults.Add(new DNetValidationResult(errorMessage));
                        }
                    }
                }
                else
                {
                    var lst = lstObjCreate.Where(x => x.accountnumber.ToString().Trim() == item.accountnumber.ToString().Trim());
                    if (lst.Count() > 1)
                    {
                        String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDuplicateData) + string.Format(" ({0})", "accountnumber = " + item.accountnumber.ToString()));
                        bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                        if (isErrorExist == false)
                        {
                            validationResults.Add(new DNetValidationResult(errorMessage));
                        }
                    }
                }
            }
            return validationResults.Count == 0;
        }

        private bool ValidateDuplicateDataInDB(List<CRM_accountCreateParameterDto> CRM_accountList, List<DNetValidationResult> validationResults, ref List<CRM_accountDto> listOfExistingData)
        {
            listOfExistingData = new List<CRM_accountDto>();

            foreach (CRM_accountCreateParameterDto item in CRM_accountList)
            {
                //check duplicate
                List<CRM_account> duplicateinDBlist = GetExistingCRM_account(item);
                if (duplicateinDBlist != null)
                {
                    CRM_account duplicateinDB = duplicateinDBlist.FirstOrDefault();
                    //String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDataDBExist) + string.Format(" ({0},{1},{2},{3})", "accountnumber = " + item.accountnumber.ToString(), "ktb_customercodesap = " + item.ktb_customercodesap.ToString(), "ktb_customerfleetcode = " + item.ktb_customerfleetcode.ToString(), "xts_identificationnumber = " + item.xts_identificationnumber.ToString() + " | " + duplicateinDB.accountid.ToString()));
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDataDBExist) + string.Format(" ({0},{1},{2})", "accountnumber = " + item.accountnumber.ToString(), "xts_identificationnumber = " + item.xts_identificationnumber.ToString(), "statecode=0" + " | " + duplicateinDB.accountid.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }

            return validationResults.Count == 0;
        }

        private List<CRM_account> GetExistingCRM_account(CRM_accountCreateParameterDto param)
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_account), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_account), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_account), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_account), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "accountnumber", param.accountnumber.ToString(), false, criterias);
                //criterias = Helper.UpdateStrCriteria(typeof(CRM_account), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "ktb_customercodesap", param.ktb_customercodesap.ToString(), false, criterias);
                //criterias = Helper.UpdateStrCriteria(typeof(CRM_account), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "ktb_customerfleetcode", param.ktb_customerfleetcode.ToString(), false, criterias);
                if (param.xts_customerclasstype != 3) { criterias = Helper.UpdateStrCriteria(typeof(CRM_account), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_identificationnumber", param.xts_identificationnumber.ToString(), false, criterias); }
                criterias = Helper.UpdateStrCriteria(typeof(CRM_account), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "statecode", "0", false, criterias);

                List<CRM_account> data = _CRM_accountRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data.Count > 0)
                {
                    return data.Cast<CRM_account>().ToList();
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
