#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_workordertimeregister class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV 
 GENERATED BY  : Nando
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 01 Sep 2020 12:25:09
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
    public class CRM_xts_workordertimeregisterBL : AbstractBusinessLogic, ICRM_xts_workordertimeregisterBL
    {
        #region Variables
        private ICRM_xts_workordertimeregisterRepository<CRM_xts_workordertimeregister, int> _CRM_xts_workordertimeregisterRepo;
        #endregion

        #region Constructor
        public CRM_xts_workordertimeregisterBL(ICRM_xts_workordertimeregisterRepository<CRM_xts_workordertimeregister, int> CRM_xts_workordertimeregisterRepo)
        {
            _CRM_xts_workordertimeregisterRepo = CRM_xts_workordertimeregisterRepo;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Get CRM_xts_workordertimeregister by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<CRM_xts_workordertimeregisterDto>> Read(CRM_xts_workordertimeregisterFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<CRM_xts_workordertimeregisterDto>> ReadList(CRM_xts_workordertimeregisterFilterDto filterDto, int pageSize)
        {

            var result = new ResponseBase<List<CRM_xts_workordertimeregisterDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                var criterias = Helper.InitialStrCriteria(typeof(CRM_xts_workordertimeregister), filterDto);

                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_workordertimeregister), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_workordertimeregister), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_workordertimeregister), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(CRM_xts_workordertimeregister), filterDto, "CRM_xts_workordertimeregister");

                List<CRM_xts_workordertimeregister> data = _CRM_xts_workordertimeregisterRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    CRM_xts_workordertimeregister x = new CRM_xts_workordertimeregister();
                    result.lst = data.ConvertList<CRM_xts_workordertimeregister, CRM_xts_workordertimeregisterDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(CRM_xts_workordertimeregister), filterDto);
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
        /// Create a new CRM_xts_workordertimeregister
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_workordertimeregisterDto> Create(CRM_xts_workordertimeregisterParameterDto objCreate)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_workordertimeregisterDto> Create(CRM_xts_workordertimeregisterCreateParameterDto paramCreate)
        {
            var result = new ResponseBaseCustom<CRM_xts_workordertimeregisterDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_workordertimeregister), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_workordertimeregister), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_workordertimeregister), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_workordertimeregister), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_workordernumber", paramCreate.xts_workordernumber.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_workordertimeregister), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_workordertimeregister", paramCreate.xts_workordertimeregister.ToString(), false, criterias);

                List<CRM_xts_workordertimeregister> data = _CRM_xts_workordertimeregisterRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data != null && data.Count == 0)
                {
                    PropertyInfo[] properties = typeof(CRM_xts_workordertimeregisterCreateParameterDto).GetProperties();
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

                    CRM_xts_workordertimeregister createRow = paramCreate.ConvertObject<CRM_xts_workordertimeregister>();
                    Guid newId = Guid.NewGuid();
                    createRow.xts_workordertimeregisterid = newId;
                    createRow.DealerCode = DealerCode;
                    createRow.SourceType = "NON DMS"; createRow.createdby = (createRow.createdby == null || createRow.createdby.ToString() == "00000000-0000-0000-0000-000000000000") ? createRow.modifiedby : createRow.createdby;
                    createRow.RowStatus = "0"; createRow.statecode = 0;

                    //Add Validation Data Input
                    ValidationParameterDto paramCheck = new ValidationParameterDto();
                    paramCheck.dealerCode = DealerCode;
                    paramCheck.xts_businessunitid = paramCreate.xts_businessunitid;
                    paramCheck.ownerid = paramCreate.ownerid;
                    paramCheck.xts_employeeidWOTR = paramCreate.xts_employeeid;
                    paramCheck.xts_serviceactivityid = paramCreate.xts_serviceactivityid;
                    paramCheck.xts_workorderid = paramCreate.xts_workorderid;
                    #region PickList
                    List<ParamPick> param = new List<ParamPick>();
                    param.Add(new ParamPick { Category = "xts_workordertimeregister.xts_status", ValueId = paramCreate.xts_status });
                    paramCheck.pickList = param;
                    #endregion

                    if (!ValidationHelper.ValidateDataInput(paramCheck, validationResults))
                    {
                        ValidationDapper dapper = new ValidationDapper();
                        List<MessageBase> errMsg = dapper.messageList(validationResults);
                        result.messages = errMsg;
                    }
                    else
                    {
                        var nResult = _CRM_xts_workordertimeregisterRepo.Create(createRow);
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
                    ErrorMsgHelper.Exception(result.messages, String.Concat("Data already exist | " + data[0].xts_workordertimeregisterid.ToString()));
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



        public ResponseBase<List<CRM_xts_workordertimeregisterDto>> BulkCreate(List<CRM_xts_workordertimeregisterCreateParameterDto> lstObjCreate)
        {
            var result = new ResponseBase<List<CRM_xts_workordertimeregisterDto>>();
            var listOfExistingData = new List<CRM_xts_workordertimeregisterDto>();
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
                    return PopulateValidationError<List<CRM_xts_workordertimeregisterDto>>(validationResults.Distinct().ToList(), null);
                }
                else
                {
                    // insert into db
                    var CRM_xts_workordertimeregisterList = lstObjCreate.ConvertList<CRM_xts_workordertimeregisterCreateParameterDto, CRM_xts_workordertimeregister>();
                    foreach (var item in CRM_xts_workordertimeregisterList.Select((value, index) => new { value, index }))
                    {
                        PropertyInfo[] properties = typeof(CRM_xts_workordertimeregister).GetProperties();
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
                        item.value.xts_workordertimeregisterid = newId;
                        item.value.DealerCode = DealerCode;
                        item.value.SourceType = "NON DMS"; item.value.createdby = (item.value.createdby == null || item.value.createdby.ToString() == "00000000-0000-0000-0000-000000000000") ? item.value.modifiedby : item.value.createdby;
                        item.value.RowStatus = "0"; 
                        item.value.statecode = item.value.statecode == null ? 0 : item.value.statecode;

                        //Add Validation Data Input
                        ValidationParameterDto paramCheck = new ValidationParameterDto();
                        paramCheck.dealerCode = DealerCode;
                        paramCheck.xts_businessunitid = item.value.xts_businessunitid;
                        paramCheck.ownerid = item.value.ownerid;
                        paramCheck.xts_employeeidWOTR = item.value.xts_employeeid;
                        paramCheck.xts_serviceactivityid = item.value.xts_serviceactivityid;
                        paramCheck.xts_workorderid = item.value.xts_workorderid;
                        #region PickList
                        List<ParamPick> param = new List<ParamPick>();
                        param.Add(new ParamPick { Category = "xts_workordertimeregister.xts_status", ValueId = Convert.ToInt32(item.value.xts_status) });
                        paramCheck.pickList = param;
                        #endregion

                        if (ValidationHelper.ValidateDataInput(paramCheck, validationResults, item.index, true))
                        {
                            //_CRM_xts_workordertimeregisterRepo.SetCreatedLog(item.value);
                        }
                    }

                    if (validationResults.Any())
                    {
                        return PopulateValidationError<List<CRM_xts_workordertimeregisterDto>>(validationResults.Distinct().ToList(), null);
                    }
                    else
                    {
                        var isSuccess = _CRM_xts_workordertimeregisterRepo.BulkInsert(CRM_xts_workordertimeregisterList);
                        result.success = isSuccess;
                        if (result.success)
                            result.total = CRM_xts_workordertimeregisterList.Count;
                        result.lst = CRM_xts_workordertimeregisterList.ConvertList<CRM_xts_workordertimeregister, CRM_xts_workordertimeregisterDto>();
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
        /// Update CRM_xts_workordertimeregister
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_workordertimeregisterDto> Update(CRM_xts_workordertimeregisterParameterDto paramUpdate)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_workordertimeregisterDto> Update(CRM_xts_workordertimeregisterUpdateParameterDto paramUpdate)
        {
            var result = new ResponseBaseCustom<CRM_xts_workordertimeregisterDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_workordertimeregister), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_workordertimeregister), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_workordertimeregister), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_workordertimeregister), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_workordertimeregisterid", paramUpdate.xts_workordertimeregisterid.ToString(), false, criterias);
                List<CRM_xts_workordertimeregister> data = _CRM_xts_workordertimeregisterRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    PropertyInfo[] properties = typeof(CRM_xts_workordertimeregisterUpdateParameterDto).GetProperties();
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
                    paramCheck.xts_employeeidWOTR = paramUpdate.xts_employeeid;
                    paramCheck.xts_serviceactivityid = paramUpdate.xts_serviceactivityid;
                    paramCheck.xts_workorderid = paramUpdate.xts_workorderid;
                    #region PickList
                    List<ParamPick> param = new List<ParamPick>();
                    param.Add(new ParamPick { Category = "xts_workordertimeregister.xts_status", ValueId = paramUpdate.xts_status });
                    paramCheck.pickList = param;
                    #endregion

                    if (!ValidationHelper.ValidateDataInput(paramCheck, validationResults))
                    {
                        ValidationDapper dapper = new ValidationDapper();
                        List<MessageBase> errMsg = dapper.messageList(validationResults);
                        result.messages = errMsg;
                    }
                    else
                    {
                        CRM_xts_workordertimeregister updateRow = paramUpdate.ConvertObject<CRM_xts_workordertimeregister>();
                        updateRow.DealerCode = DealerCode;
                        updateRow.SourceType = "NON DMS";
                        updateRow.RowStatus = string.IsNullOrEmpty(updateRow.RowStatus) ? "0" : updateRow.RowStatus;

                        var nResult = _CRM_xts_workordertimeregisterRepo.Update(updateRow);
                        if (nResult.Success == true)
                        {
                            result.success = true;
                            result._idGuid = nResult.Data.ConvertObject<CRM_xts_workordertimeregister>().xts_workordertimeregisterid;
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
        /// Delete CRM_xts_workordertimeregister by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_workordertimeregisterDto> Delete(int ID)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_workordertimeregisterDto> Delete(CRM_xts_workordertimeregisterDeleteParameterDto paramDelete)
        {
            var result = new ResponseBaseCustom<CRM_xts_workordertimeregisterDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_workordertimeregister), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_workordertimeregister), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_workordertimeregister), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_workordertimeregister), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_workordertimeregisterid", paramDelete.xts_workordertimeregisterid.ToString(), false, criterias);

                List<CRM_xts_workordertimeregister> data = _CRM_xts_workordertimeregisterRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);
                if (data != null && data.Count > 0)
                {
                    CRM_xts_workordertimeregister deleteRow = data[0];
                    deleteRow.RowStatus = "-1"; deleteRow.modifiedby = paramDelete.modifiedby; deleteRow.modifiedon = Convert.ToDateTime(paramDelete.modifiedon).AddHours(-7);
                    var nResult = _CRM_xts_workordertimeregisterRepo.Update(deleteRow);
                    if (nResult.Success == true)
                    {
                        result.success = true;
                        result._idGuid = nResult.Data.ConvertObject<CRM_xts_workordertimeregister>().xts_workordertimeregisterid;
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
        private bool ValidateDuplicateParamData(List<CRM_xts_workordertimeregisterCreateParameterDto> lstObjCreate, List<DNetValidationResult> validationResults)
        {
            foreach (var item in lstObjCreate)
            {
                var lst = lstObjCreate.Where(x => x.xts_workordernumber.ToString().Trim() == item.xts_workordernumber.ToString().Trim() &&
                                                x.xts_workordertimeregister.ToString().Trim() == item.xts_workordertimeregister.ToString().Trim());
                if (lst.Count() > 1)
                {
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDuplicateData) + string.Format(" ({0},{1})", "xts_workordernumber = " + item.xts_workordernumber.ToString(), "xts_workordertimeregister = " + item.xts_workordertimeregister.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }
            return validationResults.Count == 0;
        }

        private bool ValidateDuplicateDataInDB(List<CRM_xts_workordertimeregisterCreateParameterDto> CRM_xts_workordertimeregisterList, List<DNetValidationResult> validationResults, ref List<CRM_xts_workordertimeregisterDto> listOfExistingData)
        {
            listOfExistingData = new List<CRM_xts_workordertimeregisterDto>();

            foreach (CRM_xts_workordertimeregisterCreateParameterDto item in CRM_xts_workordertimeregisterList)
            {
                //check duplicate
                List<CRM_xts_workordertimeregister> duplicateinDBlist = GetExistingCRM_xts_workordertimeregister(item);
                if (duplicateinDBlist != null)
                {
                    CRM_xts_workordertimeregister duplicateinDB = duplicateinDBlist.FirstOrDefault();
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDataDBExist) + string.Format(" ({0},{1},{2})", "xts_workordernumber = " + item.xts_workordernumber.ToString(), "xts_workordertimeregister = " + item.xts_workordertimeregister.ToString(), "statecode=0" + " | " + duplicateinDB.xts_workordertimeregisterid.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }

            return validationResults.Count == 0;
        }

        private List<CRM_xts_workordertimeregister> GetExistingCRM_xts_workordertimeregister(CRM_xts_workordertimeregisterCreateParameterDto param)
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_workordertimeregister), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_workordertimeregister), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_workordertimeregister), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_workordertimeregister), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_workordernumber", param.xts_workordernumber.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_workordertimeregister), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_workordertimeregister", param.xts_workordertimeregister.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_workordertimeregister), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "statecode", "0", false, criterias);

                List<CRM_xts_workordertimeregister> data = _CRM_xts_workordertimeregisterRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data.Count > 0)
                {
                    return data.Cast<CRM_xts_workordertimeregister>().ToList();
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
