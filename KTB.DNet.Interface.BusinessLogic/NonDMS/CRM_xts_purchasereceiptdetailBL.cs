#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_purchasereceiptdetail class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV 
 GENERATED BY  : Fika
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 31 Aug 2020 15:47:31
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
    public class CRM_xts_purchasereceiptdetailBL : AbstractBusinessLogic, ICRM_xts_purchasereceiptdetailBL
    {
        #region Variables
        private ICRM_xts_purchasereceiptdetailRepository<CRM_xts_purchasereceiptdetail, int> _CRM_xts_purchasereceiptdetailRepo;
        #endregion

        #region Constructor
        public CRM_xts_purchasereceiptdetailBL(ICRM_xts_purchasereceiptdetailRepository<CRM_xts_purchasereceiptdetail, int> CRM_xts_purchasereceiptdetailRepo)
        {
            _CRM_xts_purchasereceiptdetailRepo = CRM_xts_purchasereceiptdetailRepo;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Get CRM_xts_purchasereceiptdetail by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<CRM_xts_purchasereceiptdetailDto>> Read(CRM_xts_purchasereceiptdetailFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<CRM_xts_purchasereceiptdetailDto>> ReadList(CRM_xts_purchasereceiptdetailFilterDto filterDto, int pageSize)
        {

            var result = new ResponseBase<List<CRM_xts_purchasereceiptdetailDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                var criterias = Helper.InitialStrCriteria(typeof(CRM_xts_purchasereceiptdetail), filterDto);

                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(CRM_xts_purchasereceiptdetail), filterDto, "CRM_xts_purchasereceiptdetail");

                List<CRM_xts_purchasereceiptdetail> data = _CRM_xts_purchasereceiptdetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    CRM_xts_purchasereceiptdetail x = new CRM_xts_purchasereceiptdetail();
                    result.lst = data.ConvertList<CRM_xts_purchasereceiptdetail, CRM_xts_purchasereceiptdetailDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(CRM_xts_purchasereceiptdetail), filterDto);
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
        /// Create a new CRM_xts_purchasereceiptdetail
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_purchasereceiptdetailDto> Create(CRM_xts_purchasereceiptdetailParameterDto objCreate)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_purchasereceiptdetailDto> Create(CRM_xts_purchasereceiptdetailCreateParameterDto paramCreate)
        {
            var result = new ResponseBaseCustom<CRM_xts_purchasereceiptdetailDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "ktb_deliveryorderno", paramCreate.ktb_deliveryorderno.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_purchasereceiptdetail", paramCreate.xts_purchasereceiptdetail.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_purchasereceiptid", paramCreate.xts_purchasereceiptid.ToString(), false, criterias);

                List<CRM_xts_purchasereceiptdetail> data = _CRM_xts_purchasereceiptdetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data != null && data.Count == 0)
                {
                    PropertyInfo[] properties = typeof(CRM_xts_purchasereceiptdetailCreateParameterDto).GetProperties();
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

                    CRM_xts_purchasereceiptdetail createRow = paramCreate.ConvertObject<CRM_xts_purchasereceiptdetail>();
                    Guid newId = Guid.NewGuid();
                    createRow.xts_purchasereceiptdetailid = newId;
                    createRow.DealerCode = DealerCode;
                    createRow.SourceType = "NON DMS"; createRow.createdby = (createRow.createdby == null || createRow.createdby.ToString() == "00000000-0000-0000-0000-000000000000") ? createRow.modifiedby : createRow.createdby;
                    createRow.RowStatus = "0"; createRow.statecode = 0;

                    //Add Validation Data Input
                    ValidationParameterDto paramCheck = new ValidationParameterDto();
                    paramCheck.dealerCode = DealerCode;
                    paramCheck.xts_businessunitid = paramCreate.xts_businessunitid;

                    paramCheck.ownerid = paramCreate.ownerid;
                    paramCheck.xts_productid = paramCreate.xts_productid;
                    paramCheck.xts_purchasereceiptid = paramCreate.xts_purchasereceiptid;
                    paramCheck.xts_purchaseunitid = paramCreate.xts_purchaseunitid;
                    paramCheck.xts_siteid = paramCreate.xts_siteid;
                    paramCheck.xts_warehouseid = paramCreate.xts_warehouseid;

                    if (!ValidationHelper.ValidateDataInput(paramCheck, validationResults))
                    {
                        ValidationDapper dapper = new ValidationDapper();
                        List<MessageBase> errMsg = dapper.messageList(validationResults);
                        result.messages = errMsg;
                    }
                    else
                    {
                        var nResult = _CRM_xts_purchasereceiptdetailRepo.Create(createRow);
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
                    ErrorMsgHelper.Exception(result.messages, String.Concat("Data already exist | " + data[0].xts_purchasereceiptdetailid.ToString()));
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



        public ResponseBase<List<CRM_xts_purchasereceiptdetailDto>> BulkCreate(List<CRM_xts_purchasereceiptdetailCreateParameterDto> lstObjCreate)
        {
            var result = new ResponseBase<List<CRM_xts_purchasereceiptdetailDto>>();
            var listOfExistingData = new List<CRM_xts_purchasereceiptdetailDto>();
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
                    return PopulateValidationError<List<CRM_xts_purchasereceiptdetailDto>>(validationResults.Distinct().ToList(), null);
                }
                else
                {
                    // insert into db
                    var CRM_xts_purchasereceiptdetailList = lstObjCreate.ConvertList<CRM_xts_purchasereceiptdetailCreateParameterDto, CRM_xts_purchasereceiptdetail>();
                    foreach (var item in CRM_xts_purchasereceiptdetailList.Select((value, index) => new { value, index }))
                    {
                        PropertyInfo[] properties = typeof(CRM_xts_purchasereceiptdetail).GetProperties();
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
                        item.value.xts_purchasereceiptdetailid = newId;
                        item.value.DealerCode = DealerCode;
                        item.value.SourceType = "NON DMS"; item.value.createdby = (item.value.createdby == null || item.value.createdby.ToString() == "00000000-0000-0000-0000-000000000000") ? item.value.modifiedby : item.value.createdby;
                        item.value.RowStatus = "0"; 
                        item.value.statecode = item.value.statecode == null ? 0 : item.value.statecode;

                        //Add Validation Data Input
                        ValidationParameterDto paramCheck = new ValidationParameterDto();
                        paramCheck.dealerCode = DealerCode;
                        paramCheck.xts_businessunitid = item.value.xts_businessunitid;

                        paramCheck.ownerid = item.value.ownerid;
                        paramCheck.xts_productid = item.value.xts_productid;
                        paramCheck.xts_purchasereceiptid = item.value.xts_purchasereceiptid;
                        paramCheck.xts_purchaseunitid = item.value.xts_purchaseunitid;
                        paramCheck.xts_siteid = item.value.xts_siteid;
                        paramCheck.xts_warehouseid = item.value.xts_warehouseid;

                        if (ValidationHelper.ValidateDataInput(paramCheck, validationResults, item.index, true))
                        {
                            //_CRM_xts_purchasereceiptdetailRepo.SetCreatedLog(item.value);
                        }
                    }

                    if (validationResults.Any())
                    {
                        return PopulateValidationError<List<CRM_xts_purchasereceiptdetailDto>>(validationResults.Distinct().ToList(), null);
                    }
                    else
                    {
                        var isSuccess = _CRM_xts_purchasereceiptdetailRepo.BulkInsert(CRM_xts_purchasereceiptdetailList);
                        result.success = isSuccess;
                        if (result.success)
                            result.total = CRM_xts_purchasereceiptdetailList.Count;
                        result.lst = CRM_xts_purchasereceiptdetailList.ConvertList<CRM_xts_purchasereceiptdetail, CRM_xts_purchasereceiptdetailDto>();
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
        /// Update CRM_xts_purchasereceiptdetail
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_purchasereceiptdetailDto> Update(CRM_xts_purchasereceiptdetailParameterDto paramUpdate)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_purchasereceiptdetailDto> Update(CRM_xts_purchasereceiptdetailUpdateParameterDto paramUpdate)
        {
            var result = new ResponseBaseCustom<CRM_xts_purchasereceiptdetailDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_purchasereceiptdetailid", paramUpdate.xts_purchasereceiptdetailid.ToString(), false, criterias);
                List<CRM_xts_purchasereceiptdetail> data = _CRM_xts_purchasereceiptdetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    PropertyInfo[] properties = typeof(CRM_xts_purchasereceiptdetailUpdateParameterDto).GetProperties();
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
                    paramCheck.xts_productid = paramUpdate.xts_productid;
                    paramCheck.xts_purchasereceiptid = paramUpdate.xts_purchasereceiptid;
                    paramCheck.xts_purchaseunitid = paramUpdate.xts_purchaseunitid;
                    paramCheck.xts_siteid = paramUpdate.xts_siteid;
                    paramCheck.xts_warehouseid = paramUpdate.xts_warehouseid;

                    if (!ValidationHelper.ValidateDataInput(paramCheck, validationResults))
                    {
                        ValidationDapper dapper = new ValidationDapper();
                        List<MessageBase> errMsg = dapper.messageList(validationResults);
                        result.messages = errMsg;
                    }
                    else
                    {
                        CRM_xts_purchasereceiptdetail updateRow = paramUpdate.ConvertObject<CRM_xts_purchasereceiptdetail>();
                        updateRow.DealerCode = DealerCode;
                        updateRow.SourceType = "NON DMS";
                        updateRow.RowStatus = string.IsNullOrEmpty(updateRow.RowStatus) ? "0" : updateRow.RowStatus;

                        var nResult = _CRM_xts_purchasereceiptdetailRepo.Update(updateRow);
                        if (nResult.Success == true)
                        {
                            result.success = true;
                            result._idGuid = nResult.Data.ConvertObject<CRM_xts_purchasereceiptdetail>().xts_purchasereceiptdetailid;
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
        /// Delete CRM_xts_purchasereceiptdetail by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_purchasereceiptdetailDto> Delete(int ID)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_purchasereceiptdetailDto> Delete(CRM_xts_purchasereceiptdetailDeleteParameterDto paramDelete)
        {
            var result = new ResponseBaseCustom<CRM_xts_purchasereceiptdetailDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_purchasereceiptdetailid", paramDelete.xts_purchasereceiptdetailid.ToString(), false, criterias);

                List<CRM_xts_purchasereceiptdetail> data = _CRM_xts_purchasereceiptdetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);
                if (data != null && data.Count > 0)
                {
                    CRM_xts_purchasereceiptdetail deleteRow = data[0];
                    deleteRow.RowStatus = "-1"; deleteRow.modifiedby = paramDelete.modifiedby; deleteRow.modifiedon = Convert.ToDateTime(paramDelete.modifiedon).AddHours(-7);
                    var nResult = _CRM_xts_purchasereceiptdetailRepo.Update(deleteRow);
                    if (nResult.Success == true)
                    {
                        result.success = true;
                        result._idGuid = nResult.Data.ConvertObject<CRM_xts_purchasereceiptdetail>().xts_purchasereceiptdetailid;
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
        private bool ValidateDuplicateParamData(List<CRM_xts_purchasereceiptdetailCreateParameterDto> lstObjCreate, List<DNetValidationResult> validationResults)
        {
            foreach (var item in lstObjCreate)
            {
                var lst = lstObjCreate.Where(x => x.ktb_deliveryorderno.ToString().Trim() == item.ktb_deliveryorderno.ToString().Trim() &&
                                                x.xts_purchasereceiptdetail.ToString().Trim() == item.xts_purchasereceiptdetail.ToString().Trim() &&
                                                x.xts_purchasereceiptid.ToString().Trim() == item.xts_purchasereceiptid.ToString().Trim());
                if (lst.Count() > 1)
                {
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDuplicateData) + string.Format(" ({0},{1},{2})", "ktb_deliveryorderno = " + item.ktb_deliveryorderno.ToString(), "xts_purchasereceiptdetail = " + item.xts_purchasereceiptdetail.ToString(), "xts_purchasereceiptid = " + item.xts_purchasereceiptid.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }
            return validationResults.Count == 0;
        }

        private bool ValidateDuplicateDataInDB(List<CRM_xts_purchasereceiptdetailCreateParameterDto> CRM_xts_purchasereceiptdetailList, List<DNetValidationResult> validationResults, ref List<CRM_xts_purchasereceiptdetailDto> listOfExistingData)
        {
            listOfExistingData = new List<CRM_xts_purchasereceiptdetailDto>();

            foreach (CRM_xts_purchasereceiptdetailCreateParameterDto item in CRM_xts_purchasereceiptdetailList)
            {
                //check duplicate
                List<CRM_xts_purchasereceiptdetail> duplicateinDBlist = GetExistingCRM_xts_purchasereceiptdetail(item);
                if (duplicateinDBlist != null)
                {
                    CRM_xts_purchasereceiptdetail duplicateinDB = duplicateinDBlist.FirstOrDefault();
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDataDBExist) + string.Format(" ({0},{1},{2},{3})", "ktb_deliveryorderno = " + item.ktb_deliveryorderno.ToString(), "xts_purchasereceiptdetail = " + item.xts_purchasereceiptdetail.ToString(), "xts_purchasereceiptid = " + item.xts_purchasereceiptid.ToString(), "statecode=0" + " | " + duplicateinDB.xts_purchasereceiptdetailid.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }

            return validationResults.Count == 0;
        }

        private List<CRM_xts_purchasereceiptdetail> GetExistingCRM_xts_purchasereceiptdetail(CRM_xts_purchasereceiptdetailCreateParameterDto param)
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "ktb_deliveryorderno", param.ktb_deliveryorderno.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_purchasereceiptdetail", param.xts_purchasereceiptdetail.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_purchasereceiptid", param.xts_purchasereceiptid.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchasereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "statecode", "0", false, criterias);

                List<CRM_xts_purchasereceiptdetail> data = _CRM_xts_purchasereceiptdetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data.Count > 0)
                {
                    return data.Cast<CRM_xts_purchasereceiptdetail>().ToList();
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
