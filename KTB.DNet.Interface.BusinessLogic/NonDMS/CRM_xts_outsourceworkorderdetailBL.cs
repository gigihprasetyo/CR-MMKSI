#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_outsourceworkorderdetail class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV 
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 08 Feb 2021 11:28:52
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
    public class CRM_xts_outsourceworkorderdetailBL : AbstractBusinessLogic, ICRM_xts_outsourceworkorderdetailBL
    {
        #region Variables
        private ICRM_xts_outsourceworkorderdetailRepository<CRM_xts_outsourceworkorderdetail, int> _CRM_xts_outsourceworkorderdetailRepo;
        #endregion

        #region Constructor
        public CRM_xts_outsourceworkorderdetailBL(ICRM_xts_outsourceworkorderdetailRepository<CRM_xts_outsourceworkorderdetail, int> CRM_xts_outsourceworkorderdetailRepo)
        {
            _CRM_xts_outsourceworkorderdetailRepo = CRM_xts_outsourceworkorderdetailRepo;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Get CRM_xts_outsourceworkorderdetail by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<CRM_xts_outsourceworkorderdetailDto>> Read(CRM_xts_outsourceworkorderdetailFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<CRM_xts_outsourceworkorderdetailDto>> ReadList(CRM_xts_outsourceworkorderdetailFilterDto filterDto, int pageSize)
        {

            var result = new ResponseBase<List<CRM_xts_outsourceworkorderdetailDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                var criterias = Helper.InitialStrCriteria(typeof(CRM_xts_outsourceworkorderdetail), filterDto);

                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(CRM_xts_outsourceworkorderdetail), filterDto, "CRM_xts_outsourceworkorderdetail");

                List<CRM_xts_outsourceworkorderdetail> data = _CRM_xts_outsourceworkorderdetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    CRM_xts_outsourceworkorderdetail x = new CRM_xts_outsourceworkorderdetail();
                    result.lst = data.ConvertList<CRM_xts_outsourceworkorderdetail, CRM_xts_outsourceworkorderdetailDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(CRM_xts_outsourceworkorderdetail), filterDto);
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
        /// Create a new CRM_xts_outsourceworkorderdetail
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_outsourceworkorderdetailDto> Create(CRM_xts_outsourceworkorderdetailParameterDto objCreate)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_outsourceworkorderdetailDto> Create(CRM_xts_outsourceworkorderdetailCreateParameterDto paramCreate)
        {
            var result = new ResponseBaseCustom<CRM_xts_outsourceworkorderdetailDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_businessunitid", paramCreate.xts_businessunitid.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_outsourceworkorderdetail", paramCreate.xts_outsourceworkorderdetail.ToString(), false, criterias);

                List<CRM_xts_outsourceworkorderdetail> data = _CRM_xts_outsourceworkorderdetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data != null && data.Count == 0)
                {
                    PropertyInfo[] properties = typeof(CRM_xts_outsourceworkorderdetailCreateParameterDto).GetProperties();
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

                    CRM_xts_outsourceworkorderdetail createRow = paramCreate.ConvertObject<CRM_xts_outsourceworkorderdetail>();
                    Guid newId = Guid.NewGuid();
                    createRow.xts_outsourceworkorderdetailid = newId;
                    createRow.DealerCode = DealerCode;
                    createRow.SourceType = "NON DMS"; createRow.createdby = (createRow.createdby == null || createRow.createdby.ToString() == "00000000-0000-0000-0000-000000000000") ? createRow.modifiedby : createRow.createdby;
                    createRow.RowStatus = "0"; createRow.statecode = 0;

                    //Add Validation Data Input
                    ValidationParameterDto paramCheck = new ValidationParameterDto();
                    paramCheck.dealerCode = DealerCode;
                    paramCheck.xts_businessunitid = paramCreate.xts_businessunitid;
                    paramCheck.ownerid = paramCreate.ownerid;
                    paramCheck.xts_servicecategoryid = paramCreate.xts_servicecategoryid;
                    paramCheck.xts_productid = paramCreate.xts_productid;

                    if (!ValidationHelper.ValidateDataInput(paramCheck, validationResults))
                    {
                        ValidationDapper dapper = new ValidationDapper();
                        List<MessageBase> errMsg = dapper.messageList(validationResults);
                        result.messages = errMsg;
                    }
                    else
                    {
                        var nResult = _CRM_xts_outsourceworkorderdetailRepo.Create(createRow);
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
                    ErrorMsgHelper.Exception(result.messages, String.Concat("Data already exist | " + data[0].xts_outsourceworkorderdetailid.ToString()));
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



        public ResponseBase<List<CRM_xts_outsourceworkorderdetailDto>> BulkCreate(List<CRM_xts_outsourceworkorderdetailCreateParameterDto> lstObjCreate)
        {
            var result = new ResponseBase<List<CRM_xts_outsourceworkorderdetailDto>>();
            var listOfExistingData = new List<CRM_xts_outsourceworkorderdetailDto>();
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
                    return PopulateValidationError<List<CRM_xts_outsourceworkorderdetailDto>>(validationResults.Distinct().ToList(), null);
                }
                else
                {
                    // insert into db
                    var CRM_xts_outsourceworkorderdetailList = lstObjCreate.ConvertList<CRM_xts_outsourceworkorderdetailCreateParameterDto, CRM_xts_outsourceworkorderdetail>();
                    foreach (var item in CRM_xts_outsourceworkorderdetailList.Select((value, index) => new { value, index }))
                    {
                        PropertyInfo[] properties = typeof(CRM_xts_outsourceworkorderdetail).GetProperties();
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
                        item.value.xts_outsourceworkorderdetailid = newId;
                        item.value.DealerCode = DealerCode;
                        item.value.SourceType = "NON DMS"; item.value.createdby = (item.value.createdby == null || item.value.createdby.ToString() == "00000000-0000-0000-0000-000000000000") ? item.value.modifiedby : item.value.createdby;
                        item.value.RowStatus = "0"; 
                        item.value.statecode = item.value.statecode == null ? 0 : item.value.statecode;

                        //Add Validation Data Input
                        ValidationParameterDto paramCheck = new ValidationParameterDto();
                        paramCheck.dealerCode = DealerCode;
                        paramCheck.xts_businessunitid = item.value.xts_businessunitid;
                        paramCheck.ownerid = item.value.ownerid;
                        paramCheck.xts_servicecategoryid = item.value.xts_servicecategoryid;
                        paramCheck.xts_productid = item.value.xts_productid;

                        if (ValidationHelper.ValidateDataInput(paramCheck, validationResults, item.index, true))
                        {
                            //_CRM_xts_outsourceworkorderdetailRepo.SetCreatedLog(item.value);
                        }
                    }

                    if (validationResults.Any())
                    {
                        return PopulateValidationError<List<CRM_xts_outsourceworkorderdetailDto>>(validationResults.Distinct().ToList(), null);
                    }
                    else
                    {
                        var isSuccess = _CRM_xts_outsourceworkorderdetailRepo.BulkInsert(CRM_xts_outsourceworkorderdetailList);
                        result.success = isSuccess;
                        if (result.success)
                            result.total = CRM_xts_outsourceworkorderdetailList.Count;
                        result.lst = CRM_xts_outsourceworkorderdetailList.ConvertList<CRM_xts_outsourceworkorderdetail, CRM_xts_outsourceworkorderdetailDto>();
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
        /// Update CRM_xts_outsourceworkorderdetail
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_outsourceworkorderdetailDto> Update(CRM_xts_outsourceworkorderdetailParameterDto paramUpdate)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_outsourceworkorderdetailDto> Update(CRM_xts_outsourceworkorderdetailUpdateParameterDto paramUpdate)
        {
            var result = new ResponseBaseCustom<CRM_xts_outsourceworkorderdetailDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_outsourceworkorderdetailid", paramUpdate.xts_outsourceworkorderdetailid.ToString(), false, criterias);
                List<CRM_xts_outsourceworkorderdetail> data = _CRM_xts_outsourceworkorderdetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    PropertyInfo[] properties = typeof(CRM_xts_outsourceworkorderdetailUpdateParameterDto).GetProperties();
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
                    paramCheck.xts_servicecategoryid = paramUpdate.xts_servicecategoryid;
                    paramCheck.xts_productid = paramUpdate.xts_productid;

                    if (!ValidationHelper.ValidateDataInput(paramCheck, validationResults))
                    {
                        ValidationDapper dapper = new ValidationDapper();
                        List<MessageBase> errMsg = dapper.messageList(validationResults);
                        result.messages = errMsg;
                    }
                    else
                    {
                        CRM_xts_outsourceworkorderdetail updateRow = paramUpdate.ConvertObject<CRM_xts_outsourceworkorderdetail>();
                        updateRow.DealerCode = DealerCode;
                        updateRow.SourceType = "NON DMS";
                        updateRow.RowStatus = string.IsNullOrEmpty(updateRow.RowStatus) ? "0" : updateRow.RowStatus;

                        var nResult = _CRM_xts_outsourceworkorderdetailRepo.Update(updateRow);
                        if (nResult.Success == true)
                        {
                            result.success = true;
                            result._idGuid = nResult.Data.ConvertObject<CRM_xts_outsourceworkorderdetail>().xts_outsourceworkorderdetailid;
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
        /// Delete CRM_xts_outsourceworkorderdetail by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_outsourceworkorderdetailDto> Delete(int ID)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_outsourceworkorderdetailDto> Delete(CRM_xts_outsourceworkorderdetailDeleteParameterDto paramDelete)
        {
            var result = new ResponseBaseCustom<CRM_xts_outsourceworkorderdetailDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_outsourceworkorderdetailid", paramDelete.xts_outsourceworkorderdetailid.ToString(), false, criterias);

                List<CRM_xts_outsourceworkorderdetail> data = _CRM_xts_outsourceworkorderdetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);
                if (data != null && data.Count > 0)
                {
                    CRM_xts_outsourceworkorderdetail deleteRow = data[0];
                    deleteRow.RowStatus = "-1"; deleteRow.modifiedby = paramDelete.modifiedby; deleteRow.modifiedon = Convert.ToDateTime(paramDelete.modifiedon).AddHours(-7);
                    var nResult = _CRM_xts_outsourceworkorderdetailRepo.Update(deleteRow);
                    if (nResult.Success == true)
                    {
                        result.success = true;
                        result._idGuid = nResult.Data.ConvertObject<CRM_xts_outsourceworkorderdetail>().xts_outsourceworkorderdetailid;
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
        private bool ValidateDuplicateParamData(List<CRM_xts_outsourceworkorderdetailCreateParameterDto> lstObjCreate, List<DNetValidationResult> validationResults)
        {
            foreach (var item in lstObjCreate)
            {
                var lst = lstObjCreate.Where(x => x.xts_businessunitid.ToString().Trim() == item.xts_businessunitid.ToString().Trim() &&
                                                x.xts_outsourceworkorderdetail.ToString().Trim() == item.xts_outsourceworkorderdetail.ToString().Trim());
                if (lst.Count() > 1)
                {
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDuplicateData) + string.Format(" ({0},{1})", "xts_businessunitid = " + item.xts_businessunitid.ToString(), "xts_outsourceworkorderdetail = " + item.xts_outsourceworkorderdetail.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }
            return validationResults.Count == 0;
        }

        private bool ValidateDuplicateDataInDB(List<CRM_xts_outsourceworkorderdetailCreateParameterDto> CRM_xts_outsourceworkorderdetailList, List<DNetValidationResult> validationResults, ref List<CRM_xts_outsourceworkorderdetailDto> listOfExistingData)
        {
            listOfExistingData = new List<CRM_xts_outsourceworkorderdetailDto>();

            foreach (CRM_xts_outsourceworkorderdetailCreateParameterDto item in CRM_xts_outsourceworkorderdetailList)
            {
                //check duplicate
                List<CRM_xts_outsourceworkorderdetail> duplicateinDBlist = GetExistingCRM_xts_outsourceworkorderdetail(item);
                if (duplicateinDBlist != null)
                {
                    CRM_xts_outsourceworkorderdetail duplicateinDB = duplicateinDBlist.FirstOrDefault();
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDataDBExist) + string.Format(" ({0},{1},{2})", "xts_businessunitid = " + item.xts_businessunitid.ToString(), "xts_outsourceworkorderdetail = " + item.xts_outsourceworkorderdetail.ToString(), "statecode=0" + " | " + duplicateinDB.xts_outsourceworkorderdetailid.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }

            return validationResults.Count == 0;
        }

        private List<CRM_xts_outsourceworkorderdetail> GetExistingCRM_xts_outsourceworkorderdetail(CRM_xts_outsourceworkorderdetailCreateParameterDto param)
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_businessunitid", param.xts_businessunitid.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_outsourceworkorderdetail", param.xts_outsourceworkorderdetail.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_outsourceworkorderdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "statecode", "0", false, criterias);

                List<CRM_xts_outsourceworkorderdetail> data = _CRM_xts_outsourceworkorderdetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data.Count > 0)
                {
                    return data.Cast<CRM_xts_outsourceworkorderdetail>().ToList();
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
