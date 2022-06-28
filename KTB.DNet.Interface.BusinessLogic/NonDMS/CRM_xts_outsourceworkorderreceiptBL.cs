#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_outsourceworkorderreceipt class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV 
 GENERATED BY  : Gugun (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 03 Sep 2020 12:38:22
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
    public class CRM_xts_outsourceworkorderreceiptBL : AbstractBusinessLogic, ICRM_xts_outsourceworkorderreceiptBL
    {
        #region Variables
        private ICRM_xts_outsourceworkorderreceiptRepository<CRM_xts_outsourceworkorderreceipt, int> _CRM_xts_outsourceworkorderreceiptRepo;
        #endregion

        #region Constructor
        public CRM_xts_outsourceworkorderreceiptBL(ICRM_xts_outsourceworkorderreceiptRepository<CRM_xts_outsourceworkorderreceipt, int> CRM_xts_outsourceworkorderreceiptRepo)
        {
            _CRM_xts_outsourceworkorderreceiptRepo = CRM_xts_outsourceworkorderreceiptRepo;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Get CRM_xts_outsourceworkorderreceipt by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<CRM_xts_outsourceworkorderreceiptDto>> Read(CRM_xts_outsourceworkorderreceiptFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<CRM_xts_outsourceworkorderreceiptDto>> ReadList(CRM_xts_outsourceworkorderreceiptFilterDto filterDto, int pageSize)
        {

            var result = new ResponseBase<List<CRM_xts_outsourceworkorderreceiptDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                var criterias = Helper.InitialStrCriteria(typeof(CRM_xts_outsourceworkorderreceipt), filterDto);

                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(CRM_xts_outsourceworkorderreceipt), filterDto, "CRM_xts_outsourceworkorderreceipt");

                List<CRM_xts_outsourceworkorderreceipt> data = _CRM_xts_outsourceworkorderreceiptRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    CRM_xts_outsourceworkorderreceipt x = new CRM_xts_outsourceworkorderreceipt();
                    result.lst = data.ConvertList<CRM_xts_outsourceworkorderreceipt, CRM_xts_outsourceworkorderreceiptDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(CRM_xts_outsourceworkorderreceipt), filterDto);
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
        /// Create a new CRM_xts_outsourceworkorderreceipt
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_outsourceworkorderreceiptDto> Create(CRM_xts_outsourceworkorderreceiptParameterDto objCreate)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_outsourceworkorderreceiptDto> Create(CRM_xts_outsourceworkorderreceiptCreateParameterDto paramCreate)
        {
            var result = new ResponseBaseCustom<CRM_xts_outsourceworkorderreceiptDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_deliveryinvoicenumber", paramCreate.xts_deliveryinvoicenumber.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_outsourceworkorderreceipt", paramCreate.xts_outsourceworkorderreceipt.ToString(), false, criterias);

                List<CRM_xts_outsourceworkorderreceipt> data = _CRM_xts_outsourceworkorderreceiptRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data != null && data.Count == 0)
                {
                    PropertyInfo[] properties = typeof(CRM_xts_outsourceworkorderreceiptCreateParameterDto).GetProperties();
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

                    CRM_xts_outsourceworkorderreceipt createRow = paramCreate.ConvertObject<CRM_xts_outsourceworkorderreceipt>();
                    Guid newId = Guid.NewGuid();
                    createRow.xts_outsourceworkorderreceiptid = newId;
                    createRow.DealerCode = DealerCode;
                    createRow.SourceType = "NON DMS"; createRow.createdby = (createRow.createdby == null || createRow.createdby.ToString() == "00000000-0000-0000-0000-000000000000") ? createRow.modifiedby : createRow.createdby;
                    createRow.RowStatus = "0"; createRow.statecode = 0;

                    //Add Validation Data Input
                    ValidationParameterDto paramCheck = new ValidationParameterDto();
                    paramCheck.dealerCode = DealerCode;
                    paramCheck.xts_businessunitid = paramCreate.xts_businessunitid;
                    paramCheck.ownerid = paramCreate.ownerid;
                    paramCheck.xts_outsourceworkorderid = paramCreate.xts_outsourceworkorderid;
                    paramCheck.xts_outsourceworkshopid = paramCreate.xts_outsourceworkshopid;
                    #region PickList
                    List<ParamPick> param = new List<ParamPick>();
                    param.Add(new ParamPick { Category = "xts_outsourceworkorderreceipt.xts_category", ValueId = paramCreate.xts_category });
                    param.Add(new ParamPick { Category = "xts_outsourceworkorderreceipt.xts_status", ValueId = paramCreate.xts_status });
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
                        var nResult = _CRM_xts_outsourceworkorderreceiptRepo.Create(createRow);
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
                    ErrorMsgHelper.Exception(result.messages, String.Concat("Data already exist | " + data[0].xts_outsourceworkorderreceiptid.ToString()));
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



        public ResponseBase<List<CRM_xts_outsourceworkorderreceiptDto>> BulkCreate(List<CRM_xts_outsourceworkorderreceiptCreateParameterDto> lstObjCreate)
        {
            var result = new ResponseBase<List<CRM_xts_outsourceworkorderreceiptDto>>();
            var listOfExistingData = new List<CRM_xts_outsourceworkorderreceiptDto>();
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
                    return PopulateValidationError<List<CRM_xts_outsourceworkorderreceiptDto>>(validationResults.Distinct().ToList(), null);
                }
                else
                {
                    // insert into db
                    var CRM_xts_outsourceworkorderreceiptList = lstObjCreate.ConvertList<CRM_xts_outsourceworkorderreceiptCreateParameterDto, CRM_xts_outsourceworkorderreceipt>();
                    foreach (var item in CRM_xts_outsourceworkorderreceiptList.Select((value, index) => new { value, index }))
                    {
                        PropertyInfo[] properties = typeof(CRM_xts_outsourceworkorderreceipt).GetProperties();
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
                        item.value.xts_outsourceworkorderreceiptid = newId;
                        item.value.DealerCode = DealerCode;
                        item.value.SourceType = "NON DMS"; item.value.createdby = (item.value.createdby == null || item.value.createdby.ToString() == "00000000-0000-0000-0000-000000000000") ? item.value.modifiedby : item.value.createdby;
                        item.value.RowStatus = "0"; 
                        item.value.statecode = item.value.statecode == null ? 0 : item.value.statecode;

                        //Add Validation Data Input
                        ValidationParameterDto paramCheck = new ValidationParameterDto();
                        paramCheck.dealerCode = DealerCode;
                        paramCheck.xts_businessunitid = item.value.xts_businessunitid;
                        paramCheck.ownerid = item.value.ownerid;
                        paramCheck.xts_outsourceworkorderid = item.value.xts_outsourceworkorderid;
                        paramCheck.xts_outsourceworkshopid = item.value.xts_outsourceworkshopid;
                        #region PickList
                        List<ParamPick> param = new List<ParamPick>();
                        param.Add(new ParamPick { Category = "xts_outsourceworkorderreceipt.xts_category", ValueId = Convert.ToInt32(item.value.xts_category) });
                        param.Add(new ParamPick { Category = "xts_outsourceworkorderreceipt.xts_status", ValueId = Convert.ToInt32(item.value.xts_status) });
                        paramCheck.pickList = param;
                        #endregion

                        if (ValidationHelper.ValidateDataInput(paramCheck, validationResults, item.index, true))
                        {
                            //_CRM_xts_outsourceworkorderreceiptRepo.SetCreatedLog(item.value);
                        }
                    }

                    if (validationResults.Any())
                    {
                        return PopulateValidationError<List<CRM_xts_outsourceworkorderreceiptDto>>(validationResults.Distinct().ToList(), null);
                    }
                    else
                    {
                        var isSuccess = _CRM_xts_outsourceworkorderreceiptRepo.BulkInsert(CRM_xts_outsourceworkorderreceiptList);
                        result.success = isSuccess;
                        if (result.success)
                            result.total = CRM_xts_outsourceworkorderreceiptList.Count;
                        result.lst = CRM_xts_outsourceworkorderreceiptList.ConvertList<CRM_xts_outsourceworkorderreceipt, CRM_xts_outsourceworkorderreceiptDto>();
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
        /// Update CRM_xts_outsourceworkorderreceipt
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_outsourceworkorderreceiptDto> Update(CRM_xts_outsourceworkorderreceiptParameterDto paramUpdate)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_outsourceworkorderreceiptDto> Update(CRM_xts_outsourceworkorderreceiptUpdateParameterDto paramUpdate)
        {
            var result = new ResponseBaseCustom<CRM_xts_outsourceworkorderreceiptDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_outsourceworkorderreceiptid", paramUpdate.xts_outsourceworkorderreceiptid.ToString(), false, criterias);
                List<CRM_xts_outsourceworkorderreceipt> data = _CRM_xts_outsourceworkorderreceiptRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    PropertyInfo[] properties = typeof(CRM_xts_outsourceworkorderreceiptUpdateParameterDto).GetProperties();
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
                    paramCheck.xts_outsourceworkorderid = paramUpdate.xts_outsourceworkorderid;
                    paramCheck.xts_outsourceworkshopid = paramUpdate.xts_outsourceworkshopid;
                    #region PickList
                    List<ParamPick> param = new List<ParamPick>();
                    param.Add(new ParamPick { Category = "xts_outsourceworkorderreceipt.xts_category", ValueId = paramUpdate.xts_category });
                    param.Add(new ParamPick { Category = "xts_outsourceworkorderreceipt.xts_status", ValueId = paramUpdate.xts_status });
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
                        CRM_xts_outsourceworkorderreceipt updateRow = paramUpdate.ConvertObject<CRM_xts_outsourceworkorderreceipt>();
                        updateRow.DealerCode = DealerCode;
                        updateRow.SourceType = "NON DMS";
                        updateRow.RowStatus = string.IsNullOrEmpty(updateRow.RowStatus) ? "0" : updateRow.RowStatus;

                        var nResult = _CRM_xts_outsourceworkorderreceiptRepo.Update(updateRow);
                        if (nResult.Success == true)
                        {
                            result.success = true;
                            result._idGuid = nResult.Data.ConvertObject<CRM_xts_outsourceworkorderreceipt>().xts_outsourceworkorderreceiptid;
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
        /// Delete CRM_xts_outsourceworkorderreceipt by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_outsourceworkorderreceiptDto> Delete(int ID)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_outsourceworkorderreceiptDto> Delete(CRM_xts_outsourceworkorderreceiptDeleteParameterDto paramDelete)
        {
            var result = new ResponseBaseCustom<CRM_xts_outsourceworkorderreceiptDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_outsourceworkorderreceiptid", paramDelete.xts_outsourceworkorderreceiptid.ToString(), false, criterias);

                List<CRM_xts_outsourceworkorderreceipt> data = _CRM_xts_outsourceworkorderreceiptRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);
                if (data != null && data.Count > 0)
                {
                    CRM_xts_outsourceworkorderreceipt deleteRow = data[0];
                    deleteRow.RowStatus = "-1"; deleteRow.modifiedby = paramDelete.modifiedby; deleteRow.modifiedon = Convert.ToDateTime(paramDelete.modifiedon).AddHours(-7);
                    var nResult = _CRM_xts_outsourceworkorderreceiptRepo.Update(deleteRow);
                    if (nResult.Success == true)
                    {
                        result.success = true;
                        result._idGuid = nResult.Data.ConvertObject<CRM_xts_outsourceworkorderreceipt>().xts_outsourceworkorderreceiptid;
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
        private bool ValidateDuplicateParamData(List<CRM_xts_outsourceworkorderreceiptCreateParameterDto> lstObjCreate, List<DNetValidationResult> validationResults)
        {
            foreach (var item in lstObjCreate)
            {
                var lst = lstObjCreate.Where(x => x.xts_deliveryinvoicenumber.ToString().Trim() == item.xts_deliveryinvoicenumber.ToString().Trim() &&
                                                x.xts_outsourceworkorderreceipt.ToString().Trim() == item.xts_outsourceworkorderreceipt.ToString().Trim());
                if (lst.Count() > 1)
                {
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDuplicateData) + string.Format(" ({0},{1})", "xts_deliveryinvoicenumber = " + item.xts_deliveryinvoicenumber.ToString(), "xts_outsourceworkorderreceipt = " + item.xts_outsourceworkorderreceipt.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }
            return validationResults.Count == 0;
        }

        private bool ValidateDuplicateDataInDB(List<CRM_xts_outsourceworkorderreceiptCreateParameterDto> CRM_xts_outsourceworkorderreceiptList, List<DNetValidationResult> validationResults, ref List<CRM_xts_outsourceworkorderreceiptDto> listOfExistingData)
        {
            listOfExistingData = new List<CRM_xts_outsourceworkorderreceiptDto>();

            foreach (CRM_xts_outsourceworkorderreceiptCreateParameterDto item in CRM_xts_outsourceworkorderreceiptList)
            {
                //check duplicate
                List<CRM_xts_outsourceworkorderreceipt> duplicateinDBlist = GetExistingCRM_xts_outsourceworkorderreceipt(item);
                if (duplicateinDBlist != null)
                {
                    CRM_xts_outsourceworkorderreceipt duplicateinDB = duplicateinDBlist.FirstOrDefault();
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDataDBExist) + string.Format(" ({0},{1},{2})", "xts_deliveryinvoicenumber = " + item.xts_deliveryinvoicenumber.ToString(), "xts_outsourceworkorderreceipt = " + item.xts_outsourceworkorderreceipt.ToString(), "statecode=0" + " | " + duplicateinDB.xts_outsourceworkorderreceiptid.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }

            return validationResults.Count == 0;
        }

        private List<CRM_xts_outsourceworkorderreceipt> GetExistingCRM_xts_outsourceworkorderreceipt(CRM_xts_outsourceworkorderreceiptCreateParameterDto param)
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_deliveryinvoicenumber", param.xts_deliveryinvoicenumber.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_outsourceworkorderreceipt", param.xts_outsourceworkorderreceipt.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderreceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "statecode", "0", false, criterias);

                List<CRM_xts_outsourceworkorderreceipt> data = _CRM_xts_outsourceworkorderreceiptRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data.Count > 0)
                {
                    return data.Cast<CRM_xts_outsourceworkorderreceipt>().ToList();
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
