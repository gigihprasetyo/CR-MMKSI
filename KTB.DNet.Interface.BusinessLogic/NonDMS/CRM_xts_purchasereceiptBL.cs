#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_purchasereceipt class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV 
 GENERATED BY  : Fika
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 26 Aug 2020 14:50:37
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
    public class CRM_xts_purchasereceiptBL : AbstractBusinessLogic, ICRM_xts_purchasereceiptBL
    {
        #region Variables
        private ICRM_xts_purchasereceiptRepository<CRM_xts_purchasereceipt, int> _CRM_xts_purchasereceiptRepo;
        #endregion

        #region Constructor
        public CRM_xts_purchasereceiptBL(ICRM_xts_purchasereceiptRepository<CRM_xts_purchasereceipt, int> CRM_xts_purchasereceiptRepo)
        {
            _CRM_xts_purchasereceiptRepo = CRM_xts_purchasereceiptRepo;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Get CRM_xts_purchasereceipt by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<CRM_xts_purchasereceiptDto>> Read(CRM_xts_purchasereceiptFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<CRM_xts_purchasereceiptDto>> ReadList(CRM_xts_purchasereceiptFilterDto filterDto, int pageSize)
        {

            var result = new ResponseBase<List<CRM_xts_purchasereceiptDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                var criterias = Helper.InitialStrCriteria(typeof(CRM_xts_purchasereceipt), filterDto);

                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(CRM_xts_purchasereceipt), filterDto, "CRM_xts_purchasereceipt");

                List<CRM_xts_purchasereceipt> data = _CRM_xts_purchasereceiptRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    CRM_xts_purchasereceipt x = new CRM_xts_purchasereceipt();
                    result.lst = data.ConvertList<CRM_xts_purchasereceipt, CRM_xts_purchasereceiptDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(CRM_xts_purchasereceipt), filterDto);
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
        /// Create a new CRM_xts_purchasereceipt
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_purchasereceiptDto> Create(CRM_xts_purchasereceiptParameterDto objCreate)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_purchasereceiptDto> Create(CRM_xts_purchasereceiptCreateParameterDto paramCreate)
        {
            var result = new ResponseBaseCustom<CRM_xts_purchasereceiptDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_deliveryordernumber", paramCreate.xts_deliveryordernumber.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_purchasereceiptnumber", paramCreate.xts_purchasereceiptnumber.ToString(), false, criterias);

                List<CRM_xts_purchasereceipt> data = _CRM_xts_purchasereceiptRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data != null && data.Count == 0)
                {
                    PropertyInfo[] properties = typeof(CRM_xts_purchasereceiptCreateParameterDto).GetProperties();
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

                    CRM_xts_purchasereceipt createRow = paramCreate.ConvertObject<CRM_xts_purchasereceipt>();
                    Guid newId = Guid.NewGuid();
                    createRow.xts_purchasereceiptid = newId;
                    createRow.DealerCode = DealerCode;
                    createRow.SourceType = "NON DMS"; createRow.createdby = (createRow.createdby == null || createRow.createdby.ToString() == "00000000-0000-0000-0000-000000000000") ? createRow.modifiedby : createRow.createdby;
                    createRow.RowStatus = "0"; createRow.statecode = 0;

                    //Add Validation Data Input
                    ValidationParameterDto paramCheck = new ValidationParameterDto();
                    paramCheck.dealerCode = DealerCode;
                    paramCheck.xts_businessunitid = paramCreate.xts_businessunitid;
                    paramCheck.ownerid = paramCreate.ownerid;
                    paramCheck.xts_purchaseorderid = paramCreate.xts_purchaseorderid;
                    paramCheck.xts_termofpaymentid = paramCreate.xts_termsofpaymentid;
                    #region PickList
                    List<ParamPick> param = new List<ParamPick>();
                    param.Add(new ParamPick { Category = "xts_purchasereceipt.xts_type", ValueId = paramCreate.xts_type });
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
                        var nResult = _CRM_xts_purchasereceiptRepo.Create(createRow);
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
                    ErrorMsgHelper.Exception(result.messages, String.Concat("Data already exist | " + data[0].xts_purchasereceiptid.ToString()));
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



        public ResponseBase<List<CRM_xts_purchasereceiptDto>> BulkCreate(List<CRM_xts_purchasereceiptCreateParameterDto> lstObjCreate)
        {
            var result = new ResponseBase<List<CRM_xts_purchasereceiptDto>>();
            var listOfExistingData = new List<CRM_xts_purchasereceiptDto>();
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
                    return PopulateValidationError<List<CRM_xts_purchasereceiptDto>>(validationResults.Distinct().ToList(), null);
                }
                else
                {
                    // insert into db
                    var CRM_xts_purchasereceiptList = lstObjCreate.ConvertList<CRM_xts_purchasereceiptCreateParameterDto, CRM_xts_purchasereceipt>();
                    foreach (var item in CRM_xts_purchasereceiptList.Select((value, index) => new { value, index }))
                    {
                        PropertyInfo[] properties = typeof(CRM_xts_purchasereceipt).GetProperties();
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
                        item.value.xts_purchasereceiptid = newId;
                        item.value.DealerCode = DealerCode;
                        item.value.SourceType = "NON DMS"; item.value.createdby = (item.value.createdby == null || item.value.createdby.ToString() == "00000000-0000-0000-0000-000000000000") ? item.value.modifiedby : item.value.createdby;
                        item.value.RowStatus = "0"; 
                        item.value.statecode = item.value.statecode == null ? 0 : item.value.statecode;

                        //Add Validation Data Input
                        ValidationParameterDto paramCheck = new ValidationParameterDto();
                        paramCheck.dealerCode = DealerCode;
                        paramCheck.xts_businessunitid = item.value.xts_businessunitid;
                        paramCheck.ownerid = item.value.ownerid;
                        paramCheck.xts_purchaseorderid = item.value.xts_purchaseorderid;
                        paramCheck.xts_termofpaymentid = item.value.xts_termsofpaymentid;
                        #region PickList
                        List<ParamPick> param = new List<ParamPick>();
                        param.Add(new ParamPick { Category = "xts_purchasereceipt.xts_type", ValueId = Convert.ToInt32(item.value.xts_type) });
                        paramCheck.pickList = param;
                        #endregion

                        if (ValidationHelper.ValidateDataInput(paramCheck, validationResults, item.index, true))
                        {
                            //_CRM_xts_purchasereceiptRepo.SetCreatedLog(item.value);
                        }
                    }

                    if (validationResults.Any())
                    {
                        return PopulateValidationError<List<CRM_xts_purchasereceiptDto>>(validationResults.Distinct().ToList(), null);
                    }
                    else
                    {
                        var isSuccess = _CRM_xts_purchasereceiptRepo.BulkInsert(CRM_xts_purchasereceiptList);
                        result.success = isSuccess;
                        if (result.success)
                            result.total = CRM_xts_purchasereceiptList.Count;
                        result.lst = CRM_xts_purchasereceiptList.ConvertList<CRM_xts_purchasereceipt, CRM_xts_purchasereceiptDto>();
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
        /// Update CRM_xts_purchasereceipt
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_purchasereceiptDto> Update(CRM_xts_purchasereceiptParameterDto paramUpdate)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_purchasereceiptDto> Update(CRM_xts_purchasereceiptUpdateParameterDto paramUpdate)
        {
            var result = new ResponseBaseCustom<CRM_xts_purchasereceiptDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_purchasereceiptid", paramUpdate.xts_purchasereceiptid.ToString(), false, criterias);
                List<CRM_xts_purchasereceipt> data = _CRM_xts_purchasereceiptRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    PropertyInfo[] properties = typeof(CRM_xts_purchasereceiptUpdateParameterDto).GetProperties();
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
                    paramCheck.xts_purchaseorderid = paramUpdate.xts_purchaseorderid;
                    paramCheck.xts_termofpaymentid = paramUpdate.xts_termsofpaymentid;
                    #region PickList
                    List<ParamPick> param = new List<ParamPick>();
                    param.Add(new ParamPick { Category = "xts_purchasereceipt.xts_type", ValueId = paramUpdate.xts_type });
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
                        CRM_xts_purchasereceipt updateRow = paramUpdate.ConvertObject<CRM_xts_purchasereceipt>();
                        updateRow.DealerCode = DealerCode;
                        updateRow.SourceType = "NON DMS";
                        updateRow.RowStatus = string.IsNullOrEmpty(updateRow.RowStatus) ? "0" : updateRow.RowStatus;

                        var nResult = _CRM_xts_purchasereceiptRepo.Update(updateRow);
                        if (nResult.Success == true)
                        {
                            result.success = true;
                            result._idGuid = nResult.Data.ConvertObject<CRM_xts_purchasereceipt>().xts_purchasereceiptid;
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
        /// Delete CRM_xts_purchasereceipt by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_purchasereceiptDto> Delete(int ID)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_purchasereceiptDto> Delete(CRM_xts_purchasereceiptDeleteParameterDto paramDelete)
        {
            var result = new ResponseBaseCustom<CRM_xts_purchasereceiptDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_purchasereceiptid", paramDelete.xts_purchasereceiptid.ToString(), false, criterias);

                List<CRM_xts_purchasereceipt> data = _CRM_xts_purchasereceiptRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);
                if (data != null && data.Count > 0)
                {
                    CRM_xts_purchasereceipt deleteRow = data[0];
                    deleteRow.RowStatus = "-1"; deleteRow.modifiedby = paramDelete.modifiedby; deleteRow.modifiedon = Convert.ToDateTime(paramDelete.modifiedon).AddHours(-7);
                    var nResult = _CRM_xts_purchasereceiptRepo.Update(deleteRow);
                    if (nResult.Success == true)
                    {
                        result.success = true;
                        result._idGuid = nResult.Data.ConvertObject<CRM_xts_purchasereceipt>().xts_purchasereceiptid;
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
        private bool ValidateDuplicateParamData(List<CRM_xts_purchasereceiptCreateParameterDto> lstObjCreate, List<DNetValidationResult> validationResults)
        {
            foreach (var item in lstObjCreate)
            {
                var lst = lstObjCreate.Where(x => x.xts_deliveryordernumber.ToString().Trim() == item.xts_deliveryordernumber.ToString().Trim() &&
                                                x.xts_purchasereceiptnumber.ToString().Trim() == item.xts_purchasereceiptnumber.ToString().Trim());
                if (lst.Count() > 1)
                {
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDuplicateData) + string.Format(" ({0},{1})", "xts_deliveryordernumber = " + item.xts_deliveryordernumber.ToString(), "xts_purchasereceiptnumber = " + item.xts_purchasereceiptnumber.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }
            return validationResults.Count == 0;
        }

        private bool ValidateDuplicateDataInDB(List<CRM_xts_purchasereceiptCreateParameterDto> CRM_xts_purchasereceiptList, List<DNetValidationResult> validationResults, ref List<CRM_xts_purchasereceiptDto> listOfExistingData)
        {
            listOfExistingData = new List<CRM_xts_purchasereceiptDto>();

            foreach (CRM_xts_purchasereceiptCreateParameterDto item in CRM_xts_purchasereceiptList)
            {
                //check duplicate
                List<CRM_xts_purchasereceipt> duplicateinDBlist = GetExistingCRM_xts_purchasereceipt(item);
                if (duplicateinDBlist != null)
                {
                    CRM_xts_purchasereceipt duplicateinDB = duplicateinDBlist.FirstOrDefault();
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDataDBExist) + string.Format(" ({0},{1},{2})", "xts_deliveryordernumber = " + item.xts_deliveryordernumber.ToString(), "xts_purchasereceiptnumber = " + item.xts_purchasereceiptnumber.ToString(), "statecode=0" + " | " + duplicateinDB.xts_purchasereceiptid.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }

            return validationResults.Count == 0;
        }

        private List<CRM_xts_purchasereceipt> GetExistingCRM_xts_purchasereceipt(CRM_xts_purchasereceiptCreateParameterDto param)
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_deliveryordernumber", param.xts_deliveryordernumber.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_purchasereceiptnumber", param.xts_purchasereceiptnumber.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "statecode", "0", false, criterias);

                List<CRM_xts_purchasereceipt> data = _CRM_xts_purchasereceiptRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data.Count > 0)
                {
                    return data.Cast<CRM_xts_purchasereceipt>().ToList();
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
