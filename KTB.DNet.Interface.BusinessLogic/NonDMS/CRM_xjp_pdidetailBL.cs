#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xjp_pdidetail class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV 
 GENERATED BY  : Nando (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 03 Sep 2020 10:19:00
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
    public class CRM_xjp_pdidetailBL : AbstractBusinessLogic, ICRM_xjp_pdidetailBL
    {
        #region Variables
        private ICRM_xjp_pdidetailRepository<CRM_xjp_pdidetail, int> _CRM_xjp_pdidetailRepo;
        #endregion

        #region Constructor
        public CRM_xjp_pdidetailBL(ICRM_xjp_pdidetailRepository<CRM_xjp_pdidetail, int> CRM_xjp_pdidetailRepo)
        {
            _CRM_xjp_pdidetailRepo = CRM_xjp_pdidetailRepo;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Get CRM_xjp_pdidetail by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<CRM_xjp_pdidetailDto>> Read(CRM_xjp_pdidetailFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<CRM_xjp_pdidetailDto>> ReadList(CRM_xjp_pdidetailFilterDto filterDto, int pageSize)
        {

            var result = new ResponseBase<List<CRM_xjp_pdidetailDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                var criterias = Helper.InitialStrCriteria(typeof(CRM_xjp_pdidetail), filterDto);

                criterias = Helper.UpdateStrCriteria(typeof(CRM_xjp_pdidetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xjp_pdidetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xjp_pdidetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(CRM_xjp_pdidetail), filterDto, "CRM_xjp_pdidetail");

                List<CRM_xjp_pdidetail> data = _CRM_xjp_pdidetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    CRM_xjp_pdidetail x = new CRM_xjp_pdidetail();
                    result.lst = data.ConvertList<CRM_xjp_pdidetail, CRM_xjp_pdidetailDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(CRM_xjp_pdidetail), filterDto);
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
        /// Create a new CRM_xjp_pdidetail
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xjp_pdidetailDto> Create(CRM_xjp_pdidetailParameterDto objCreate)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xjp_pdidetailDto> Create(CRM_xjp_pdidetailCreateParameterDto paramCreate)
        {
            var result = new ResponseBaseCustom<CRM_xjp_pdidetailDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xjp_pdidetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xjp_pdidetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xjp_pdidetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xjp_pdidetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xjp_pdidetailnumber", paramCreate.xjp_pdidetailnumber.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xjp_pdidetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xjp_predeliveryinspectionid", paramCreate.xjp_predeliveryinspectionid.ToString(), false, criterias);

                List<CRM_xjp_pdidetail> data = _CRM_xjp_pdidetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data != null && data.Count == 0)
                {
                    PropertyInfo[] properties = typeof(CRM_xjp_pdidetailCreateParameterDto).GetProperties();
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

                    CRM_xjp_pdidetail createRow = paramCreate.ConvertObject<CRM_xjp_pdidetail>();
                    Guid newId = Guid.NewGuid();
                    createRow.xjp_pdidetailid = newId;
                    createRow.DealerCode = DealerCode;
                    createRow.SourceType = "NON DMS"; createRow.createdby = (createRow.createdby == null || createRow.createdby.ToString() == "00000000-0000-0000-0000-000000000000") ? createRow.modifiedby : createRow.createdby;
                    createRow.RowStatus = "0"; createRow.statecode = 0;

                    //Add Validation Data Input
                    ValidationParameterDto paramCheck = new ValidationParameterDto();
                    paramCheck.dealerCode = DealerCode;
                    paramCheck.xts_businessunitid = paramCreate.xjp_businessunitid;
                    paramCheck.ownerid = paramCreate.ownerid;
                    paramCheck.xjp_accessoriesid = paramCreate.xjp_accessoriesid;
                    paramCheck.xjp_predeliveryinspectionid = paramCreate.xjp_predeliveryinspectionid;
                    paramCheck.xjp_servicecategoryid = paramCreate.xjp_servicecategoryid;

                    if (!ValidationHelper.ValidateDataInput(paramCheck, validationResults))
                    {
                        ValidationDapper dapper = new ValidationDapper();
                        List<MessageBase> errMsg = dapper.messageList(validationResults);
                        result.messages = errMsg;
                    }
                    else
                    {
                        var nResult = _CRM_xjp_pdidetailRepo.Create(createRow);
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
                    ErrorMsgHelper.Exception(result.messages, String.Concat("Data already exist | " + data[0].xjp_pdidetailid.ToString()));
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



        public ResponseBase<List<CRM_xjp_pdidetailDto>> BulkCreate(List<CRM_xjp_pdidetailCreateParameterDto> lstObjCreate)
        {
            var result = new ResponseBase<List<CRM_xjp_pdidetailDto>>();
            var listOfExistingData = new List<CRM_xjp_pdidetailDto>();
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
                    return PopulateValidationError<List<CRM_xjp_pdidetailDto>>(validationResults.Distinct().ToList(), null);
                }
                else
                {
                    // insert into db
                    var CRM_xjp_pdidetailList = lstObjCreate.ConvertList<CRM_xjp_pdidetailCreateParameterDto, CRM_xjp_pdidetail>();
                    foreach (var item in CRM_xjp_pdidetailList.Select((value, index) => new { value, index }))
                    {
                        PropertyInfo[] properties = typeof(CRM_xjp_pdidetail).GetProperties();
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
                        item.value.xjp_pdidetailid = newId;
                        item.value.DealerCode = DealerCode;
                        item.value.SourceType = "NON DMS"; item.value.createdby = (item.value.createdby == null || item.value.createdby.ToString() == "00000000-0000-0000-0000-000000000000") ? item.value.modifiedby : item.value.createdby;
                        item.value.RowStatus = "0"; 
                        item.value.statecode = item.value.statecode == null ? 0 : item.value.statecode;

                        //Add Validation Data Input
                        ValidationParameterDto paramCheck = new ValidationParameterDto();
                        paramCheck.dealerCode = DealerCode;
                        paramCheck.xts_businessunitid = item.value.xjp_businessunitid;
                        paramCheck.ownerid = item.value.ownerid;
                        paramCheck.xjp_accessoriesid = item.value.xjp_accessoriesid;
                        paramCheck.xjp_predeliveryinspectionid = item.value.xjp_predeliveryinspectionid;
                        paramCheck.xjp_servicecategoryid = item.value.xjp_servicecategoryid;

                        if (ValidationHelper.ValidateDataInput(paramCheck, validationResults, item.index, true))
                        {
                            //_CRM_xjp_pdidetailRepo.SetCreatedLog(item.value);
                        }
                    }

                    if (validationResults.Any())
                    {
                        return PopulateValidationError<List<CRM_xjp_pdidetailDto>>(validationResults.Distinct().ToList(), null);
                    }
                    else
                    {
                        var isSuccess = _CRM_xjp_pdidetailRepo.BulkInsert(CRM_xjp_pdidetailList);
                        result.success = isSuccess;
                        if (result.success)
                            result.total = CRM_xjp_pdidetailList.Count;
                        result.lst = CRM_xjp_pdidetailList.ConvertList<CRM_xjp_pdidetail, CRM_xjp_pdidetailDto>();
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
        /// Update CRM_xjp_pdidetail
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xjp_pdidetailDto> Update(CRM_xjp_pdidetailParameterDto paramUpdate)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xjp_pdidetailDto> Update(CRM_xjp_pdidetailUpdateParameterDto paramUpdate)
        {
            var result = new ResponseBaseCustom<CRM_xjp_pdidetailDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xjp_pdidetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xjp_pdidetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xjp_pdidetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xjp_pdidetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xjp_pdidetailid", paramUpdate.xjp_pdidetailid.ToString(), false, criterias);
                List<CRM_xjp_pdidetail> data = _CRM_xjp_pdidetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    PropertyInfo[] properties = typeof(CRM_xjp_pdidetailUpdateParameterDto).GetProperties();
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
                    paramCheck.xts_businessunitid = paramUpdate.xjp_businessunitid;
                    paramCheck.ownerid = paramUpdate.ownerid;
                    paramCheck.xjp_accessoriesid = paramUpdate.xjp_accessoriesid;
                    paramCheck.xjp_predeliveryinspectionid = paramUpdate.xjp_predeliveryinspectionid;
                    paramCheck.xjp_servicecategoryid = paramUpdate.xjp_servicecategoryid;

                    if (!ValidationHelper.ValidateDataInput(paramCheck, validationResults))
                    {
                        ValidationDapper dapper = new ValidationDapper();
                        List<MessageBase> errMsg = dapper.messageList(validationResults);
                        result.messages = errMsg;
                    }
                    else
                    {
                        CRM_xjp_pdidetail updateRow = paramUpdate.ConvertObject<CRM_xjp_pdidetail>();
                        updateRow.DealerCode = DealerCode;
                        updateRow.SourceType = "NON DMS";
                        updateRow.RowStatus = string.IsNullOrEmpty(updateRow.RowStatus) ? "0" : updateRow.RowStatus;

                        var nResult = _CRM_xjp_pdidetailRepo.Update(updateRow);
                        if (nResult.Success == true)
                        {
                            result.success = true;
                            result._idGuid = nResult.Data.ConvertObject<CRM_xjp_pdidetail>().xjp_pdidetailid;
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
        /// Delete CRM_xjp_pdidetail by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xjp_pdidetailDto> Delete(int ID)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xjp_pdidetailDto> Delete(CRM_xjp_pdidetailDeleteParameterDto paramDelete)
        {
            var result = new ResponseBaseCustom<CRM_xjp_pdidetailDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xjp_pdidetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xjp_pdidetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xjp_pdidetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xjp_pdidetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xjp_pdidetailid", paramDelete.xjp_pdidetailid.ToString(), false, criterias);

                List<CRM_xjp_pdidetail> data = _CRM_xjp_pdidetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);
                if (data != null && data.Count > 0)
                {
                    CRM_xjp_pdidetail deleteRow = data[0];
                    deleteRow.RowStatus = "-1"; deleteRow.modifiedby = paramDelete.modifiedby; deleteRow.modifiedon = Convert.ToDateTime(paramDelete.modifiedon).AddHours(-7);
                    var nResult = _CRM_xjp_pdidetailRepo.Update(deleteRow);
                    if (nResult.Success == true)
                    {
                        result.success = true;
                        result._idGuid = nResult.Data.ConvertObject<CRM_xjp_pdidetail>().xjp_pdidetailid;
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
        private bool ValidateDuplicateParamData(List<CRM_xjp_pdidetailCreateParameterDto> lstObjCreate, List<DNetValidationResult> validationResults)
        {
            foreach (var item in lstObjCreate)
            {
                var lst = lstObjCreate.Where(x => x.xjp_pdidetailnumber.ToString().Trim() == item.xjp_pdidetailnumber.ToString().Trim() &&
                                                x.xjp_predeliveryinspectionid.ToString().Trim() == item.xjp_predeliveryinspectionid.ToString().Trim());
                if (lst.Count() > 1)
                {
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDuplicateData) + string.Format(" ({0},{1})", "xjp_pdidetailnumber = " + item.xjp_pdidetailnumber.ToString(), "xjp_predeliveryinspectionid = " + item.xjp_predeliveryinspectionid.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }
            return validationResults.Count == 0;
        }

        private bool ValidateDuplicateDataInDB(List<CRM_xjp_pdidetailCreateParameterDto> CRM_xjp_pdidetailList, List<DNetValidationResult> validationResults, ref List<CRM_xjp_pdidetailDto> listOfExistingData)
        {
            listOfExistingData = new List<CRM_xjp_pdidetailDto>();

            foreach (CRM_xjp_pdidetailCreateParameterDto item in CRM_xjp_pdidetailList)
            {
                //check duplicate
                List<CRM_xjp_pdidetail> duplicateinDBlist = GetExistingCRM_xjp_pdidetail(item);
                if (duplicateinDBlist != null)
                {
                    CRM_xjp_pdidetail duplicateinDB = duplicateinDBlist.FirstOrDefault();
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDataDBExist) + string.Format(" ({0},{1},{2})", "xjp_pdidetailnumber = " + item.xjp_pdidetailnumber.ToString(), "xjp_predeliveryinspectionid = " + item.xjp_predeliveryinspectionid.ToString(), "statecode=0" + " | " + duplicateinDB.xjp_pdidetailid.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }

            return validationResults.Count == 0;
        }

        private List<CRM_xjp_pdidetail> GetExistingCRM_xjp_pdidetail(CRM_xjp_pdidetailCreateParameterDto param)
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xjp_pdidetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xjp_pdidetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xjp_pdidetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xjp_pdidetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xjp_pdidetailnumber", param.xjp_pdidetailnumber.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xjp_pdidetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xjp_predeliveryinspectionid", param.xjp_predeliveryinspectionid.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xjp_pdidetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "statecode", "0", false, criterias);

                List<CRM_xjp_pdidetail> data = _CRM_xjp_pdidetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data.Count > 0)
                {
                    return data.Cast<CRM_xjp_pdidetail>().ToList();
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
