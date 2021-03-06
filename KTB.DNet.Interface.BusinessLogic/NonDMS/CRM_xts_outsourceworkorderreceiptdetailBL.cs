#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_outsourceworkorderreceiptdetail class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV 
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 18 Jan 2021 16:16:54
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
    public class CRM_xts_outsourceworkorderreceiptdetailBL : AbstractBusinessLogic, ICRM_xts_outsourceworkorderreceiptdetailBL
    {
        #region Variables
        private ICRM_xts_outsourceworkorderreceiptdetailRepository<CRM_xts_outsourceworkorderreceiptdetail, int> _CRM_xts_outsourceworkorderreceiptdetailRepo;
        #endregion

        #region Constructor
        public CRM_xts_outsourceworkorderreceiptdetailBL(ICRM_xts_outsourceworkorderreceiptdetailRepository<CRM_xts_outsourceworkorderreceiptdetail, int> CRM_xts_outsourceworkorderreceiptdetailRepo)
        {
            _CRM_xts_outsourceworkorderreceiptdetailRepo = CRM_xts_outsourceworkorderreceiptdetailRepo;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Get CRM_xts_outsourceworkorderreceiptdetail by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<CRM_xts_outsourceworkorderreceiptdetailDto>> Read(CRM_xts_outsourceworkorderreceiptdetailFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<CRM_xts_outsourceworkorderreceiptdetailDto>> ReadList(CRM_xts_outsourceworkorderreceiptdetailFilterDto filterDto, int pageSize)
        {

            var result = new ResponseBase<List<CRM_xts_outsourceworkorderreceiptdetailDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                var criterias = Helper.InitialStrCriteria(typeof(CRM_xts_outsourceworkorderreceiptdetail), filterDto);

                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(CRM_xts_outsourceworkorderreceiptdetail), filterDto, "CRM_xts_outsourceworkorderreceiptdetail");

                List<CRM_xts_outsourceworkorderreceiptdetail> data = _CRM_xts_outsourceworkorderreceiptdetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    CRM_xts_outsourceworkorderreceiptdetail x = new CRM_xts_outsourceworkorderreceiptdetail();
                    result.lst = data.ConvertList<CRM_xts_outsourceworkorderreceiptdetail, CRM_xts_outsourceworkorderreceiptdetailDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(CRM_xts_outsourceworkorderreceiptdetail), filterDto);
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
        /// Create a new CRM_xts_outsourceworkorderreceiptdetail
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_outsourceworkorderreceiptdetailDto> Create(CRM_xts_outsourceworkorderreceiptdetailParameterDto objCreate)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_outsourceworkorderreceiptdetailDto> Create(CRM_xts_outsourceworkorderreceiptdetailCreateParameterDto paramCreate)
        {
            var result = new ResponseBaseCustom<CRM_xts_outsourceworkorderreceiptdetailDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_businessunitid", paramCreate.xts_businessunitid.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_outsourceworkorderreceiptdetail", paramCreate.xts_outsourceworkorderreceiptdetail.ToString(), false, criterias);

                List<CRM_xts_outsourceworkorderreceiptdetail> data = _CRM_xts_outsourceworkorderreceiptdetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data != null && data.Count == 0)
                {
                    PropertyInfo[] properties = typeof(CRM_xts_outsourceworkorderreceiptdetailCreateParameterDto).GetProperties();
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

                    CRM_xts_outsourceworkorderreceiptdetail createRow = paramCreate.ConvertObject<CRM_xts_outsourceworkorderreceiptdetail>();
                    Guid newId = Guid.NewGuid();
                    createRow.xts_outsourceworkorderreceiptdetailid = newId;
                    createRow.DealerCode = DealerCode;
                    createRow.SourceType = "NON DMS"; createRow.createdby = (createRow.createdby == null || createRow.createdby.ToString() == "00000000-0000-0000-0000-000000000000") ? createRow.modifiedby : createRow.createdby;
                    createRow.RowStatus = "0"; createRow.statecode = 0;

                    //Add Validation Data Input
                    ValidationParameterDto paramCheck = new ValidationParameterDto();
                    paramCheck.dealerCode = DealerCode;
                    paramCheck.xts_businessunitid = paramCreate.xts_businessunitid;
                    paramCheck.xts_productid = paramCreate.xts_productid;
                    paramCheck.ownerid = paramCreate.ownerid;
                    paramCheck.xts_outsourceworkorderreceiptid = paramCreate.xts_outsourceworkorderreceiptid;
                    #region PickList
                    List<ParamPick> param = new List<ParamPick>();
                    param.Add(new ParamPick { Category = "xts_outsourceworkorderreceiptdetail.xts_producttype", ValueId = paramCreate.xts_producttype });
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
                        var nResult = _CRM_xts_outsourceworkorderreceiptdetailRepo.Create(createRow);
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
                    ErrorMsgHelper.Exception(result.messages, String.Concat("Data already exist | " + data[0].xts_outsourceworkorderreceiptdetailid.ToString()));
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



        public ResponseBase<List<CRM_xts_outsourceworkorderreceiptdetailDto>> BulkCreate(List<CRM_xts_outsourceworkorderreceiptdetailCreateParameterDto> lstObjCreate)
        {
            var result = new ResponseBase<List<CRM_xts_outsourceworkorderreceiptdetailDto>>();
            var listOfExistingData = new List<CRM_xts_outsourceworkorderreceiptdetailDto>();
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
                    return PopulateValidationError<List<CRM_xts_outsourceworkorderreceiptdetailDto>>(validationResults.Distinct().ToList(), null);
                }
                else
                {
                    // insert into db
                    var CRM_xts_outsourceworkorderreceiptdetailList = lstObjCreate.ConvertList<CRM_xts_outsourceworkorderreceiptdetailCreateParameterDto, CRM_xts_outsourceworkorderreceiptdetail>();
                    foreach (var item in CRM_xts_outsourceworkorderreceiptdetailList.Select((value, index) => new { value, index }))
                    {
                        PropertyInfo[] properties = typeof(CRM_xts_outsourceworkorderreceiptdetail).GetProperties();
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
                        item.value.xts_outsourceworkorderreceiptdetailid = newId;
                        item.value.DealerCode = DealerCode;
                        item.value.SourceType = "NON DMS"; item.value.createdby = (item.value.createdby == null || item.value.createdby.ToString() == "00000000-0000-0000-0000-000000000000") ? item.value.modifiedby : item.value.createdby;
                        item.value.RowStatus = "0"; 
                        item.value.statecode = item.value.statecode == null ? 0 : item.value.statecode;

                        //Add Validation Data Input
                        ValidationParameterDto paramCheck = new ValidationParameterDto();
                        paramCheck.dealerCode = DealerCode;
                        paramCheck.xts_businessunitid = item.value.xts_businessunitid;
                        paramCheck.xts_productid = item.value.xts_productid;
                        paramCheck.ownerid = item.value.ownerid;
                        paramCheck.xts_outsourceworkorderreceiptid = item.value.xts_outsourceworkorderreceiptid;
                        #region PickList
                        List<ParamPick> param = new List<ParamPick>();
                        param.Add(new ParamPick { Category = "xts_outsourceworkorderreceiptdetail.xts_producttype", ValueId = Convert.ToInt32(item.value.xts_producttype) });
                        paramCheck.pickList = param;
                        #endregion

                        if (ValidationHelper.ValidateDataInput(paramCheck, validationResults, item.index, true))
                        {
                            //_CRM_xts_outsourceworkorderreceiptdetailRepo.SetCreatedLog(item.value);
                        }
                    }

                    if (validationResults.Any())
                    {
                        return PopulateValidationError<List<CRM_xts_outsourceworkorderreceiptdetailDto>>(validationResults.Distinct().ToList(), null);
                    }
                    else
                    {
                        var isSuccess = _CRM_xts_outsourceworkorderreceiptdetailRepo.BulkInsert(CRM_xts_outsourceworkorderreceiptdetailList);
                        result.success = isSuccess;
                        if (result.success)
                            result.total = CRM_xts_outsourceworkorderreceiptdetailList.Count;
                        result.lst = CRM_xts_outsourceworkorderreceiptdetailList.ConvertList<CRM_xts_outsourceworkorderreceiptdetail, CRM_xts_outsourceworkorderreceiptdetailDto>();
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
        /// Update CRM_xts_outsourceworkorderreceiptdetail
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_outsourceworkorderreceiptdetailDto> Update(CRM_xts_outsourceworkorderreceiptdetailParameterDto paramUpdate)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_outsourceworkorderreceiptdetailDto> Update(CRM_xts_outsourceworkorderreceiptdetailUpdateParameterDto paramUpdate)
        {
            var result = new ResponseBaseCustom<CRM_xts_outsourceworkorderreceiptdetailDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_outsourceworkorderreceiptdetailid", paramUpdate.xts_outsourceworkorderreceiptdetailid.ToString(), false, criterias);
                List<CRM_xts_outsourceworkorderreceiptdetail> data = _CRM_xts_outsourceworkorderreceiptdetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    PropertyInfo[] properties = typeof(CRM_xts_outsourceworkorderreceiptdetailUpdateParameterDto).GetProperties();
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
                    paramCheck.xts_productid = paramUpdate.xts_productid;
                    paramCheck.ownerid = paramUpdate.ownerid;
                    paramCheck.xts_outsourceworkorderreceiptid = paramUpdate.xts_outsourceworkorderreceiptid;
                    #region PickList
                    List<ParamPick> param = new List<ParamPick>();
                    param.Add(new ParamPick { Category = "xts_outsourceworkorderreceiptdetail.xts_producttype", ValueId = paramUpdate.xts_producttype });
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
                        CRM_xts_outsourceworkorderreceiptdetail updateRow = paramUpdate.ConvertObject<CRM_xts_outsourceworkorderreceiptdetail>();
                        updateRow.DealerCode = DealerCode;
                        updateRow.SourceType = "NON DMS";
                        updateRow.RowStatus = string.IsNullOrEmpty(updateRow.RowStatus) ? "0" : updateRow.RowStatus;

                        var nResult = _CRM_xts_outsourceworkorderreceiptdetailRepo.Update(updateRow);
                        if (nResult.Success == true)
                        {
                            result.success = true;
                            result._idGuid = nResult.Data.ConvertObject<CRM_xts_outsourceworkorderreceiptdetail>().xts_outsourceworkorderreceiptdetailid;
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
        /// Delete CRM_xts_outsourceworkorderreceiptdetail by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_outsourceworkorderreceiptdetailDto> Delete(int ID)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_outsourceworkorderreceiptdetailDto> Delete(CRM_xts_outsourceworkorderreceiptdetailDeleteParameterDto paramDelete)
        {
            var result = new ResponseBaseCustom<CRM_xts_outsourceworkorderreceiptdetailDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_outsourceworkorderreceiptdetailid", paramDelete.xts_outsourceworkorderreceiptdetailid.ToString(), false, criterias);

                List<CRM_xts_outsourceworkorderreceiptdetail> data = _CRM_xts_outsourceworkorderreceiptdetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);
                if (data != null && data.Count > 0)
                {
                    CRM_xts_outsourceworkorderreceiptdetail deleteRow = data[0];
                    deleteRow.RowStatus = "-1"; deleteRow.modifiedby = paramDelete.modifiedby; deleteRow.modifiedon = Convert.ToDateTime(paramDelete.modifiedon).AddHours(-7);
                    var nResult = _CRM_xts_outsourceworkorderreceiptdetailRepo.Update(deleteRow);
                    if (nResult.Success == true)
                    {
                        result.success = true;
                        result._idGuid = nResult.Data.ConvertObject<CRM_xts_outsourceworkorderreceiptdetail>().xts_outsourceworkorderreceiptdetailid;
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
        private bool ValidateDuplicateParamData(List<CRM_xts_outsourceworkorderreceiptdetailCreateParameterDto> lstObjCreate, List<DNetValidationResult> validationResults)
        {
            foreach (var item in lstObjCreate)
            {
                var lst = lstObjCreate.Where(x => x.xts_businessunitid.ToString().Trim() == item.xts_businessunitid.ToString().Trim() &&
                                                x.xts_outsourceworkorderreceiptdetail.ToString().Trim() == item.xts_outsourceworkorderreceiptdetail.ToString().Trim());
                if (lst.Count() > 1)
                {
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDuplicateData) + string.Format(" ({0},{1})", "xts_businessunitid = " + item.xts_businessunitid.ToString(), "xts_outsourceworkorderreceiptdetail = " + item.xts_outsourceworkorderreceiptdetail.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }
            return validationResults.Count == 0;
        }

        private bool ValidateDuplicateDataInDB(List<CRM_xts_outsourceworkorderreceiptdetailCreateParameterDto> CRM_xts_outsourceworkorderreceiptdetailList, List<DNetValidationResult> validationResults, ref List<CRM_xts_outsourceworkorderreceiptdetailDto> listOfExistingData)
        {
            listOfExistingData = new List<CRM_xts_outsourceworkorderreceiptdetailDto>();

            foreach (CRM_xts_outsourceworkorderreceiptdetailCreateParameterDto item in CRM_xts_outsourceworkorderreceiptdetailList)
            {
                //check duplicate
                List<CRM_xts_outsourceworkorderreceiptdetail> duplicateinDBlist = GetExistingCRM_xts_outsourceworkorderreceiptdetail(item);
                if (duplicateinDBlist != null)
                {
                    CRM_xts_outsourceworkorderreceiptdetail duplicateinDB = duplicateinDBlist.FirstOrDefault();
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDataDBExist) + string.Format(" ({0},{1},{2})", "xts_businessunitid = " + item.xts_businessunitid.ToString(), "xts_outsourceworkorderreceiptdetail = " + item.xts_outsourceworkorderreceiptdetail.ToString(), "statecode=0" + " | " + duplicateinDB.xts_outsourceworkorderreceiptdetailid.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }

            return validationResults.Count == 0;
        }

        private List<CRM_xts_outsourceworkorderreceiptdetail> GetExistingCRM_xts_outsourceworkorderreceiptdetail(CRM_xts_outsourceworkorderreceiptdetailCreateParameterDto param)
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_businessunitid", param.xts_businessunitid.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_outsourceworkorderreceiptdetail", param.xts_outsourceworkorderreceiptdetail.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "statecode", "0", false, criterias);

                List<CRM_xts_outsourceworkorderreceiptdetail> data = _CRM_xts_outsourceworkorderreceiptdetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data.Count > 0)
                {
                    return data.Cast<CRM_xts_outsourceworkorderreceiptdetail>().ToList();
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
