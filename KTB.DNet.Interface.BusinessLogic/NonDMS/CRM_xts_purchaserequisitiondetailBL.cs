#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_purchaserequisitiondetail class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV 
 GENERATED BY  : Khalil
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 01 Sep 2020 14:10:52
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
    public class CRM_xts_purchaserequisitiondetailBL : AbstractBusinessLogic, ICRM_xts_purchaserequisitiondetailBL
    {
        #region Variables
        private ICRM_xts_purchaserequisitiondetailRepository<CRM_xts_purchaserequisitiondetail, int> _CRM_xts_purchaserequisitiondetailRepo;
        #endregion

        #region Constructor
        public CRM_xts_purchaserequisitiondetailBL(ICRM_xts_purchaserequisitiondetailRepository<CRM_xts_purchaserequisitiondetail, int> CRM_xts_purchaserequisitiondetailRepo)
        {
            _CRM_xts_purchaserequisitiondetailRepo = CRM_xts_purchaserequisitiondetailRepo;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Get CRM_xts_purchaserequisitiondetail by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<CRM_xts_purchaserequisitiondetailDto>> Read(CRM_xts_purchaserequisitiondetailFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<CRM_xts_purchaserequisitiondetailDto>> ReadList(CRM_xts_purchaserequisitiondetailFilterDto filterDto, int pageSize)
        {

            var result = new ResponseBase<List<CRM_xts_purchaserequisitiondetailDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                var criterias = Helper.InitialStrCriteria(typeof(CRM_xts_purchaserequisitiondetail), filterDto);

                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchaserequisitiondetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchaserequisitiondetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchaserequisitiondetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(CRM_xts_purchaserequisitiondetail), filterDto, "CRM_xts_purchaserequisitiondetail");

                List<CRM_xts_purchaserequisitiondetail> data = _CRM_xts_purchaserequisitiondetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    CRM_xts_purchaserequisitiondetail x = new CRM_xts_purchaserequisitiondetail();
                    result.lst = data.ConvertList<CRM_xts_purchaserequisitiondetail, CRM_xts_purchaserequisitiondetailDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(CRM_xts_purchaserequisitiondetail), filterDto);
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
        /// Create a new CRM_xts_purchaserequisitiondetail
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_purchaserequisitiondetailDto> Create(CRM_xts_purchaserequisitiondetailParameterDto objCreate)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_purchaserequisitiondetailDto> Create(CRM_xts_purchaserequisitiondetailCreateParameterDto paramCreate)
        {
            var result = new ResponseBaseCustom<CRM_xts_purchaserequisitiondetailDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchaserequisitiondetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchaserequisitiondetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchaserequisitiondetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchaserequisitiondetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_purchaserequisitiondetail", paramCreate.xts_purchaserequisitiondetail.ToString(), false, criterias);

                List<CRM_xts_purchaserequisitiondetail> data = _CRM_xts_purchaserequisitiondetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data != null && data.Count == 0)
                {
                    PropertyInfo[] properties = typeof(CRM_xts_purchaserequisitiondetailCreateParameterDto).GetProperties();
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

                    CRM_xts_purchaserequisitiondetail createRow = paramCreate.ConvertObject<CRM_xts_purchaserequisitiondetail>();
                    Guid newId = Guid.NewGuid();
                    createRow.xts_purchaserequisitiondetailid = newId;
                    createRow.DealerCode = DealerCode;
                    createRow.SourceType = "NON DMS"; createRow.createdby = (createRow.createdby == null || createRow.createdby.ToString() == "00000000-0000-0000-0000-000000000000") ? createRow.modifiedby : createRow.createdby;
                    createRow.RowStatus = "0"; createRow.statecode = 0;

                    //Add Validation Data Input
                    ValidationParameterDto paramCheck = new ValidationParameterDto();
                    paramCheck.dealerCode = DealerCode;
                    paramCheck.xts_businessunitid = paramCreate.xts_businessunitid;
                    paramCheck.xts_productid = paramCreate.xts_productid;
                    paramCheck.xts_purchaserequisitionid = paramCreate.xts_purchaserequisitionid;
                    paramCheck.xts_siteid = paramCreate.xts_siteid;
                    #region PickList
                    List<ParamPick> param = new List<ParamPick>();
                    param.Add(new ParamPick { Category = "xts_purchaserequisitiondetail.xts_purchasefor", ValueId = paramCreate.xts_purchasefor });
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
                        var nResult = _CRM_xts_purchaserequisitiondetailRepo.Create(createRow);
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
                    ErrorMsgHelper.Exception(result.messages, String.Concat("Data already exist | " + data[0].xts_purchaserequisitiondetailid.ToString()));
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



        public ResponseBase<List<CRM_xts_purchaserequisitiondetailDto>> BulkCreate(List<CRM_xts_purchaserequisitiondetailCreateParameterDto> lstObjCreate)
        {
            var result = new ResponseBase<List<CRM_xts_purchaserequisitiondetailDto>>();
            var listOfExistingData = new List<CRM_xts_purchaserequisitiondetailDto>();
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
                    return PopulateValidationError<List<CRM_xts_purchaserequisitiondetailDto>>(validationResults.Distinct().ToList(), null);
                }
                else
                {
                    // insert into db
                    var CRM_xts_purchaserequisitiondetailList = lstObjCreate.ConvertList<CRM_xts_purchaserequisitiondetailCreateParameterDto, CRM_xts_purchaserequisitiondetail>();
                    foreach (var item in CRM_xts_purchaserequisitiondetailList.Select((value, index) => new { value, index }))
                    {
                        PropertyInfo[] properties = typeof(CRM_xts_purchaserequisitiondetail).GetProperties();
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
                        item.value.xts_purchaserequisitiondetailid = newId;
                        item.value.DealerCode = DealerCode;
                        item.value.SourceType = "NON DMS"; item.value.createdby = (item.value.createdby == null || item.value.createdby.ToString() == "00000000-0000-0000-0000-000000000000") ? item.value.modifiedby : item.value.createdby;
                        item.value.RowStatus = "0"; 
                        item.value.statecode = item.value.statecode == null ? 0 : item.value.statecode;

                        //Add Validation Data Input
                        ValidationParameterDto paramCheck = new ValidationParameterDto();
                        paramCheck.dealerCode = DealerCode;
                        paramCheck.xts_businessunitid = item.value.xts_businessunitid;
                        paramCheck.xts_productid = item.value.xts_productid;
                        paramCheck.xts_purchaserequisitionid = item.value.xts_purchaserequisitionid;
                        paramCheck.xts_siteid = item.value.xts_siteid;
                        #region PickList
                        List<ParamPick> param = new List<ParamPick>();
                        param.Add(new ParamPick { Category = "xts_purchaserequisitiondetail.xts_purchasefor", ValueId = Convert.ToInt32(item.value.xts_purchasefor) });
                        paramCheck.pickList = param;
                        #endregion

                        if (ValidationHelper.ValidateDataInput(paramCheck, validationResults, item.index, true))
                        {
                            //_CRM_xts_purchaserequisitiondetailRepo.SetCreatedLog(item.value);
                        }
                    }

                    if (validationResults.Any())
                    {
                        return PopulateValidationError<List<CRM_xts_purchaserequisitiondetailDto>>(validationResults.Distinct().ToList(), null);
                    }
                    else
                    {
                        var isSuccess = _CRM_xts_purchaserequisitiondetailRepo.BulkInsert(CRM_xts_purchaserequisitiondetailList);
                        result.success = isSuccess;
                        if (result.success)
                            result.total = CRM_xts_purchaserequisitiondetailList.Count;
                        result.lst = CRM_xts_purchaserequisitiondetailList.ConvertList<CRM_xts_purchaserequisitiondetail, CRM_xts_purchaserequisitiondetailDto>();
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
        /// Update CRM_xts_purchaserequisitiondetail
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_purchaserequisitiondetailDto> Update(CRM_xts_purchaserequisitiondetailParameterDto paramUpdate)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_purchaserequisitiondetailDto> Update(CRM_xts_purchaserequisitiondetailUpdateParameterDto paramUpdate)
        {
            var result = new ResponseBaseCustom<CRM_xts_purchaserequisitiondetailDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchaserequisitiondetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchaserequisitiondetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchaserequisitiondetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchaserequisitiondetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_purchaserequisitiondetailid", paramUpdate.xts_purchaserequisitiondetailid.ToString(), false, criterias);
                List<CRM_xts_purchaserequisitiondetail> data = _CRM_xts_purchaserequisitiondetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    PropertyInfo[] properties = typeof(CRM_xts_purchaserequisitiondetailUpdateParameterDto).GetProperties();
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
                    paramCheck.xts_purchaserequisitionid = paramUpdate.xts_purchaserequisitionid;
                    paramCheck.xts_siteid = paramUpdate.xts_siteid;
                    #region PickList
                    List<ParamPick> param = new List<ParamPick>();
                    param.Add(new ParamPick { Category = "xts_purchaserequisitiondetail.xts_purchasefor", ValueId = paramUpdate.xts_purchasefor });
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
                        CRM_xts_purchaserequisitiondetail updateRow = paramUpdate.ConvertObject<CRM_xts_purchaserequisitiondetail>();
                        updateRow.DealerCode = DealerCode;
                        updateRow.SourceType = "NON DMS";
                        updateRow.RowStatus = string.IsNullOrEmpty(updateRow.RowStatus) ? "0" : updateRow.RowStatus;

                        var nResult = _CRM_xts_purchaserequisitiondetailRepo.Update(updateRow);
                        if (nResult.Success == true)
                        {
                            result.success = true;
                            result._idGuid = nResult.Data.ConvertObject<CRM_xts_purchaserequisitiondetail>().xts_purchaserequisitiondetailid;
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
        /// Delete CRM_xts_purchaserequisitiondetail by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_purchaserequisitiondetailDto> Delete(int ID)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_purchaserequisitiondetailDto> Delete(CRM_xts_purchaserequisitiondetailDeleteParameterDto paramDelete)
        {
            var result = new ResponseBaseCustom<CRM_xts_purchaserequisitiondetailDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchaserequisitiondetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchaserequisitiondetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchaserequisitiondetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchaserequisitiondetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_purchaserequisitiondetailid", paramDelete.xts_purchaserequisitiondetailid.ToString(), false, criterias);

                List<CRM_xts_purchaserequisitiondetail> data = _CRM_xts_purchaserequisitiondetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);
                if (data != null && data.Count > 0)
                {
                    CRM_xts_purchaserequisitiondetail deleteRow = data[0];
                    deleteRow.RowStatus = "-1"; deleteRow.modifiedby = paramDelete.modifiedby; deleteRow.modifiedon = Convert.ToDateTime(paramDelete.modifiedon).AddHours(-7);
                    var nResult = _CRM_xts_purchaserequisitiondetailRepo.Update(deleteRow);
                    if (nResult.Success == true)
                    {
                        result.success = true;
                        result._idGuid = nResult.Data.ConvertObject<CRM_xts_purchaserequisitiondetail>().xts_purchaserequisitiondetailid;
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
        private bool ValidateDuplicateParamData(List<CRM_xts_purchaserequisitiondetailCreateParameterDto> lstObjCreate, List<DNetValidationResult> validationResults)
        {
            foreach (var item in lstObjCreate)
            {
                var lst = lstObjCreate.Where(x => x.xts_purchaserequisitiondetail.ToString().Trim() == item.xts_purchaserequisitiondetail.ToString().Trim());
                if (lst.Count() > 1)
                {
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDuplicateData) + string.Format(" ({0})", "xts_purchaserequisitiondetail = " + item.xts_purchaserequisitiondetail.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }
            return validationResults.Count == 0;
        }

        private bool ValidateDuplicateDataInDB(List<CRM_xts_purchaserequisitiondetailCreateParameterDto> CRM_xts_purchaserequisitiondetailList, List<DNetValidationResult> validationResults, ref List<CRM_xts_purchaserequisitiondetailDto> listOfExistingData)
        {
            listOfExistingData = new List<CRM_xts_purchaserequisitiondetailDto>();

            foreach (CRM_xts_purchaserequisitiondetailCreateParameterDto item in CRM_xts_purchaserequisitiondetailList)
            {
                //check duplicate
                List<CRM_xts_purchaserequisitiondetail> duplicateinDBlist = GetExistingCRM_xts_purchaserequisitiondetail(item);
                if (duplicateinDBlist != null)
                {
                    CRM_xts_purchaserequisitiondetail duplicateinDB = duplicateinDBlist.FirstOrDefault();
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDataDBExist) + string.Format(" ({0},{1})", "xts_purchaserequisitiondetail = " + item.xts_purchaserequisitiondetail.ToString(), "statecode=0" + " | " + duplicateinDB.xts_purchaserequisitiondetailid.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }

            return validationResults.Count == 0;
        }

        private List<CRM_xts_purchaserequisitiondetail> GetExistingCRM_xts_purchaserequisitiondetail(CRM_xts_purchaserequisitiondetailCreateParameterDto param)
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchaserequisitiondetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchaserequisitiondetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchaserequisitiondetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchaserequisitiondetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_purchaserequisitiondetail", param.xts_purchaserequisitiondetail.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_purchaserequisitiondetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "statecode", "0", false, criterias);

                List<CRM_xts_purchaserequisitiondetail> data = _CRM_xts_purchaserequisitiondetailRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data.Count > 0)
                {
                    return data.Cast<CRM_xts_purchaserequisitiondetail>().ToList();
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
