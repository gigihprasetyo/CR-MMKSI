#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_warehouse class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV 
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 05 Feb 2021 13:59:12
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
    public class CRM_xts_warehouseBL : AbstractBusinessLogic, ICRM_xts_warehouseBL
    {
        #region Variables
        private ICRM_xts_warehouseRepository<CRM_xts_warehouse, int> _CRM_xts_warehouseRepo;
        #endregion

        #region Constructor
        public CRM_xts_warehouseBL(ICRM_xts_warehouseRepository<CRM_xts_warehouse, int> CRM_xts_warehouseRepo)
        {
            _CRM_xts_warehouseRepo = CRM_xts_warehouseRepo;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Get CRM_xts_warehouse by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<CRM_xts_warehouseDto>> Read(CRM_xts_warehouseFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<CRM_xts_warehouseDto>> ReadList(CRM_xts_warehouseFilterDto filterDto, int pageSize)
        {

            var result = new ResponseBase<List<CRM_xts_warehouseDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                var criterias = Helper.InitialStrCriteria(typeof(CRM_xts_warehouse), filterDto);

                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_warehouse), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_warehouse), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_warehouse), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(CRM_xts_warehouse), filterDto, "CRM_xts_warehouse");

                List<CRM_xts_warehouse> data = _CRM_xts_warehouseRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    CRM_xts_warehouse x = new CRM_xts_warehouse();
                    result.lst = data.ConvertList<CRM_xts_warehouse, CRM_xts_warehouseDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(CRM_xts_warehouse), filterDto);
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
        /// Create a new CRM_xts_warehouse
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_warehouseDto> Create(CRM_xts_warehouseParameterDto objCreate)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_warehouseDto> Create(CRM_xts_warehouseCreateParameterDto paramCreate)
        {
            var result = new ResponseBaseCustom<CRM_xts_warehouseDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_warehouse), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_warehouse), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_warehouse), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_warehouse), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_transitwarehouseid", paramCreate.xts_transitwarehouseid.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_warehouse), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_warehouse", paramCreate.xts_warehouse.ToString(), false, criterias);

                List<CRM_xts_warehouse> data = _CRM_xts_warehouseRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data != null && data.Count == 0)
                {
                    PropertyInfo[] properties = typeof(CRM_xts_warehouseCreateParameterDto).GetProperties();
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

                    CRM_xts_warehouse createRow = paramCreate.ConvertObject<CRM_xts_warehouse>();
                    Guid newId = Guid.NewGuid();
                    createRow.xts_warehouseid = newId;
                    createRow.DealerCode = DealerCode;
                    createRow.SourceType = "NON DMS"; createRow.createdby = (createRow.createdby == null || createRow.createdby.ToString() == "00000000-0000-0000-0000-000000000000") ? createRow.modifiedby : createRow.createdby;
                    createRow.RowStatus = "0"; createRow.statecode = 0;

                    //Add Validation Data Input
                    ValidationParameterDto paramCheck = new ValidationParameterDto();
                    paramCheck.dealerCode = DealerCode;
                    //paramCheck.xts_transitwarehouseid = paramCreate.xts_transitwarehouseid;
                    paramCheck.xts_siteid = paramCreate.xts_siteid;
                    #region PickList
                    List<ParamPick> param = new List<ParamPick>();
                    param.Add(new ParamPick { Category = "xts_warehouse.ktb_identificationtype", ValueId = paramCreate.ktb_identificationtype });
                    param.Add(new ParamPick { Category = "xts_warehouse.xts_type", ValueId = paramCreate.xts_type });
                    paramCheck.pickList = param;
                    #endregion

                    ValidationDapper dapper_ = new ValidationDapper();
                    string xts_companyByDealerCode = dapper_.getBUbyDealerCode(DealerCode).msdyn_companycode;
                    if (createRow.xts_company.ToLower() != xts_companyByDealerCode.ToLower())
                    {
                        createRow.xts_company = xts_companyByDealerCode;
                    }

                    if (!ValidationHelper.ValidateDataInput(paramCheck, validationResults))
                    {
                        ValidationDapper dapper = new ValidationDapper();
                        List<MessageBase> errMsg = dapper.messageList(validationResults);
                        result.messages = errMsg;
                    }
                    else
                    {
                        var nResult = _CRM_xts_warehouseRepo.Create(createRow);
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
                    ErrorMsgHelper.Exception(result.messages, String.Concat("Data already exist | " + data[0].xts_warehouseid.ToString()));
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



        public ResponseBase<List<CRM_xts_warehouseDto>> BulkCreate(List<CRM_xts_warehouseCreateParameterDto> lstObjCreate)
        {
            var result = new ResponseBase<List<CRM_xts_warehouseDto>>();
            var listOfExistingData = new List<CRM_xts_warehouseDto>();
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
                    return PopulateValidationError<List<CRM_xts_warehouseDto>>(validationResults.Distinct().ToList(), null);
                }
                else
                {
                    // insert into db
                    var CRM_xts_warehouseList = lstObjCreate.ConvertList<CRM_xts_warehouseCreateParameterDto, CRM_xts_warehouse>();
                    foreach (var item in CRM_xts_warehouseList.Select((value, index) => new { value, index }))
                    {
                        PropertyInfo[] properties = typeof(CRM_xts_warehouse).GetProperties();
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
                        item.value.xts_warehouseid = newId;
                        item.value.DealerCode = DealerCode;
                        item.value.SourceType = "NON DMS"; item.value.createdby = (item.value.createdby == null || item.value.createdby.ToString() == "00000000-0000-0000-0000-000000000000") ? item.value.modifiedby : item.value.createdby;
                        item.value.RowStatus = "0"; 
                        item.value.statecode = item.value.statecode == null ? 0 : item.value.statecode;

                        //Add Validation Data Input
                        ValidationParameterDto paramCheck = new ValidationParameterDto();
                        paramCheck.dealerCode = DealerCode;
                        //paramCheck.xts_transitwarehouseid = item.value.xts_transitwarehouseid;
                        paramCheck.xts_siteid = item.value.xts_siteid;
                        #region PickList
                        List<ParamPick> param = new List<ParamPick>();
                        param.Add(new ParamPick { Category = "xts_warehouse.ktb_identificationtype", ValueId = Convert.ToInt32(item.value.ktb_identificationtype) });
                        param.Add(new ParamPick { Category = "xts_warehouse.xts_type", ValueId = Convert.ToInt32(item.value.xts_type) });
                        paramCheck.pickList = param;
                        #endregion

                        ValidationDapper dapper_ = new ValidationDapper();
                        string xts_companyByDealerCode = dapper_.getBUbyDealerCode(DealerCode).msdyn_companycode;
                        if (item.value.xts_company.ToLower() != xts_companyByDealerCode.ToLower())
                        {
                            item.value.xts_company = xts_companyByDealerCode;
                        }

                        if (ValidationHelper.ValidateDataInput(paramCheck, validationResults, item.index, true))
                        {
                            //_CRM_xts_warehouseRepo.SetCreatedLog(item.value);
                        }
                    }

                    if (validationResults.Any())
                    {
                        return PopulateValidationError<List<CRM_xts_warehouseDto>>(validationResults.Distinct().ToList(), null);
                    }
                    else
                    {
                        var isSuccess = _CRM_xts_warehouseRepo.BulkInsert(CRM_xts_warehouseList);
                        result.success = isSuccess;
                        if (result.success)
                            result.total = CRM_xts_warehouseList.Count;
                        result.lst = CRM_xts_warehouseList.ConvertList<CRM_xts_warehouse, CRM_xts_warehouseDto>();
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
        /// Update CRM_xts_warehouse
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_warehouseDto> Update(CRM_xts_warehouseParameterDto paramUpdate)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_warehouseDto> Update(CRM_xts_warehouseUpdateParameterDto paramUpdate)
        {
            var result = new ResponseBaseCustom<CRM_xts_warehouseDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_warehouse), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_warehouse), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_warehouse), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_warehouse), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_warehouseid", paramUpdate.xts_warehouseid.ToString(), false, criterias);
                List<CRM_xts_warehouse> data = _CRM_xts_warehouseRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    PropertyInfo[] properties = typeof(CRM_xts_warehouseUpdateParameterDto).GetProperties();
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
                    //paramCheck.xts_transitwarehouseid = paramUpdate.xts_transitwarehouseid;
                    paramCheck.xts_siteid = paramUpdate.xts_siteid;
                    #region PickList
                    List<ParamPick> param = new List<ParamPick>();
                    param.Add(new ParamPick { Category = "xts_warehouse.ktb_identificationtype", ValueId = paramUpdate.ktb_identificationtype });
                    param.Add(new ParamPick { Category = "xts_warehouse.xts_type", ValueId = paramUpdate.xts_type });
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
                        CRM_xts_warehouse updateRow = paramUpdate.ConvertObject<CRM_xts_warehouse>();
                        updateRow.DealerCode = DealerCode;
                        updateRow.SourceType = "NON DMS";
                        updateRow.RowStatus = string.IsNullOrEmpty(updateRow.RowStatus) ? "0" : updateRow.RowStatus;

                        ValidationDapper dapper_ = new ValidationDapper();
                        string xts_companyByDealerCode = dapper_.getBUbyDealerCode(DealerCode).msdyn_companycode;
                        if (updateRow.xts_company.ToLower() != xts_companyByDealerCode.ToLower())
                        {
                            updateRow.xts_company = xts_companyByDealerCode;
                        }

                        var nResult = _CRM_xts_warehouseRepo.Update(updateRow);
                        if (nResult.Success == true)
                        {
                            result.success = true;
                            result._idGuid = nResult.Data.ConvertObject<CRM_xts_warehouse>().xts_warehouseid;
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
        /// Delete CRM_xts_warehouse by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_warehouseDto> Delete(int ID)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_warehouseDto> Delete(CRM_xts_warehouseDeleteParameterDto paramDelete)
        {
            var result = new ResponseBaseCustom<CRM_xts_warehouseDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_warehouse), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_warehouse), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_warehouse), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_warehouse), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_warehouseid", paramDelete.xts_warehouseid.ToString(), false, criterias);

                List<CRM_xts_warehouse> data = _CRM_xts_warehouseRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);
                if (data != null && data.Count > 0)
                {
                    CRM_xts_warehouse deleteRow = data[0];
                    deleteRow.RowStatus = "-1"; deleteRow.modifiedby = paramDelete.modifiedby; deleteRow.modifiedon = Convert.ToDateTime(paramDelete.modifiedon).AddHours(-7);
                    var nResult = _CRM_xts_warehouseRepo.Update(deleteRow);
                    if (nResult.Success == true)
                    {
                        result.success = true;
                        result._idGuid = nResult.Data.ConvertObject<CRM_xts_warehouse>().xts_warehouseid;
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
        private bool ValidateDuplicateParamData(List<CRM_xts_warehouseCreateParameterDto> lstObjCreate, List<DNetValidationResult> validationResults)
        {
            foreach (var item in lstObjCreate)
            {
                var lst = lstObjCreate.Where(x => x.xts_transitwarehouseid.ToString().Trim() == item.xts_transitwarehouseid.ToString().Trim() &&
                                                x.xts_warehouse.ToString().Trim() == item.xts_warehouse.ToString().Trim());
                if (lst.Count() > 1)
                {
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDuplicateData) + string.Format(" ({0},{1})", "xts_transitwarehouseid = " + item.xts_transitwarehouseid.ToString(), "xts_warehouse = " + item.xts_warehouse.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }
            return validationResults.Count == 0;
        }

        private bool ValidateDuplicateDataInDB(List<CRM_xts_warehouseCreateParameterDto> CRM_xts_warehouseList, List<DNetValidationResult> validationResults, ref List<CRM_xts_warehouseDto> listOfExistingData)
        {
            listOfExistingData = new List<CRM_xts_warehouseDto>();

            foreach (CRM_xts_warehouseCreateParameterDto item in CRM_xts_warehouseList)
            {
                //check duplicate
                List<CRM_xts_warehouse> duplicateinDBlist = GetExistingCRM_xts_warehouse(item);
                if (duplicateinDBlist != null)
                {
                    CRM_xts_warehouse duplicateinDB = duplicateinDBlist.FirstOrDefault();
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDataDBExist) + string.Format(" ({0},{1},{2})", "xts_transitwarehouseid = " + item.xts_transitwarehouseid.ToString(), "xts_warehouse = " + item.xts_warehouse.ToString(), "statecode=0" + " | " + duplicateinDB.xts_warehouseid.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }

            return validationResults.Count == 0;
        }

        private List<CRM_xts_warehouse> GetExistingCRM_xts_warehouse(CRM_xts_warehouseCreateParameterDto param)
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_warehouse), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_warehouse), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_warehouse), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_warehouse), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_transitwarehouseid", param.xts_transitwarehouseid.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_warehouse), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_warehouse", param.xts_warehouse.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_warehouse), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "statecode", "0", false, criterias);

                List<CRM_xts_warehouse> data = _CRM_xts_warehouseRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data.Count > 0)
                {
                    return data.Cast<CRM_xts_warehouse>().ToList();
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
