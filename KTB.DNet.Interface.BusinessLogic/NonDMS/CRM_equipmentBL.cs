#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_equipment class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV 
 GENERATED BY  : Ivan
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 18 Jan 2021 16:07:58
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
    public class CRM_equipmentBL : AbstractBusinessLogic, ICRM_equipmentBL
    {
        #region Variables
        private ICRM_equipmentRepository<CRM_equipment, int> _CRM_equipmentRepo;
        #endregion

        #region Constructor
        public CRM_equipmentBL(ICRM_equipmentRepository<CRM_equipment, int> CRM_equipmentRepo)
        {
            _CRM_equipmentRepo = CRM_equipmentRepo;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Get CRM_equipment by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<CRM_equipmentDto>> Read(CRM_equipmentFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<CRM_equipmentDto>> ReadList(CRM_equipmentFilterDto filterDto, int pageSize)
        {

            var result = new ResponseBase<List<CRM_equipmentDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                var criterias = Helper.InitialStrCriteria(typeof(CRM_equipment), filterDto);

                criterias = Helper.UpdateStrCriteria(typeof(CRM_equipment), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_equipment), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_equipment), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(CRM_equipment), filterDto, "CRM_equipment");

                List<CRM_equipment> data = _CRM_equipmentRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    CRM_equipment x = new CRM_equipment();
                    result.lst = data.ConvertList<CRM_equipment, CRM_equipmentDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(CRM_equipment), filterDto);
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
        /// Create a new CRM_equipment
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<CRM_equipmentDto> Create(CRM_equipmentParameterDto objCreate)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_equipmentDto> Create(CRM_equipmentCreateParameterDto paramCreate)
        {
            var result = new ResponseBaseCustom<CRM_equipmentDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_equipment), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_equipment), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_equipment), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_equipment), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "businessunitid", paramCreate.businessunitid.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_equipment), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "name", paramCreate.name.ToString(), false, criterias);

                List<CRM_equipment> data = _CRM_equipmentRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data != null && data.Count == 0)
                {
                    PropertyInfo[] properties = typeof(CRM_equipmentCreateParameterDto).GetProperties();
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

                    CRM_equipment createRow = paramCreate.ConvertObject<CRM_equipment>();
                    Guid newId = Guid.NewGuid();
                    createRow.equipmentid = newId;
                    createRow.DealerCode = DealerCode;
                    createRow.SourceType = "NON DMS"; createRow.createdby = (createRow.createdby == null || createRow.createdby.ToString() == "00000000-0000-0000-0000-000000000000") ? createRow.modifiedby : createRow.createdby;
                    createRow.RowStatus = 0;

                    //Add Validation Data Input
                    ValidationParameterDto paramCheck = new ValidationParameterDto();
                    paramCheck.dealerCode = DealerCode;
                    paramCheck.xts_businessunitid = paramCreate.businessunitid;
                    #region PickList
                    List<ParamPick> param = new List<ParamPick>();
                    param.Add(new ParamPick { Category = "equipment.xts_type", ValueId = paramCreate.xts_type });
                    paramCheck.pickList = param;
                    #endregion

                    if (paramCreate.xts_type == 3 && (paramCreate.xts_vehicleidentificationnumber == null || paramCreate.xts_vehicleidentificationnumber.Trim() == ""))
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, "xts_vehicleidentificationnumber")));
                    }
                    if (paramCreate.xts_type == 4 || paramCreate.xts_type == 5)
                    {
                        if (paramCreate.xts_employeeid.ToString() == "00000000-0000-0000-0000-000000000000" || paramCreate.xts_employeeid == null)
                        {
                            string ErrorMssageGuid = string.Format(MessageResource.ErrorMsgDataRequired, "xts_employeeid");
                            ErrorMssageGuid = ErrorMssageGuid + " Tidak dapat menggunakan '00000000-0000-0000-0000-000000000000'";
                            validationResults.Add(new DNetValidationResult(ErrorMssageGuid));
                        }
                        else
                        {
                            paramCheck.xts_employeeid = paramCreate.xts_employeeid;
                        }
                    }

                    if (!ValidationHelper.ValidateDataInput(paramCheck, validationResults))
                    {
                        ValidationDapper dapper = new ValidationDapper();
                        List<MessageBase> errMsg = dapper.messageList(validationResults);
                        result.messages = errMsg;
                    }
                    else
                    {
                        var nResult = _CRM_equipmentRepo.Create(createRow);
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
                    ErrorMsgHelper.Exception(result.messages, String.Concat("Data already exist | " + data[0].equipmentid.ToString()));
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



        public ResponseBase<List<CRM_equipmentDto>> BulkCreate(List<CRM_equipmentCreateParameterDto> lstObjCreate)
        {
            var result = new ResponseBase<List<CRM_equipmentDto>>();
            var listOfExistingData = new List<CRM_equipmentDto>();
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
                    return PopulateValidationError<List<CRM_equipmentDto>>(validationResults.Distinct().ToList(), null);
                }
                else
                {
                    // insert into db
                    var CRM_equipmentList = lstObjCreate.ConvertList<CRM_equipmentCreateParameterDto, CRM_equipment>();
                    foreach (var item in CRM_equipmentList.Select((value, index) => new { value, index }))
                    {
                        PropertyInfo[] properties = typeof(CRM_equipment).GetProperties();
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
                        item.value.equipmentid = newId;
                        item.value.DealerCode = DealerCode;
                        item.value.SourceType = "NON DMS"; item.value.createdby = (item.value.createdby == null || item.value.createdby.ToString() == "00000000-0000-0000-0000-000000000000") ? item.value.modifiedby : item.value.createdby;
                        item.value.RowStatus = 0;

                        //Add Validation Data Input
                        ValidationParameterDto paramCheck = new ValidationParameterDto();
                        paramCheck.dealerCode = DealerCode;
                        paramCheck.xts_businessunitid = item.value.businessunitid;
                        #region PickList
                        List<ParamPick> param = new List<ParamPick>();
                        param.Add(new ParamPick { Category = "equipment.xts_type", ValueId = Convert.ToInt32(item.value.xts_type) });
                        paramCheck.pickList = param;
                        #endregion

                        if (item.value.xts_type == "3" && (item.value.xts_vehicleidentificationnumber == null || item.value.xts_vehicleidentificationnumber.Trim() == ""))
                        {
                            validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, "xts_vehicleidentificationnumber")));
                        }
                        if (item.value.xts_type == "4" || item.value.xts_type == "5")
                        {
                            if (item.value.xts_employeeid.ToString() == "00000000-0000-0000-0000-000000000000" || item.value.xts_employeeid == null)
                            {
                                string ErrorMssageGuid = string.Format(MessageResource.ErrorMsgDataRequired, "xts_employeeid");
                                ErrorMssageGuid = ErrorMssageGuid + " Tidak dapat menggunakan '00000000-0000-0000-0000-000000000000'";
                                validationResults.Add(new DNetValidationResult(ErrorMssageGuid));
                            }
                            else
                            {
                                paramCheck.xts_employeeid = item.value.xts_employeeid;
                            }
                        }


                        if (ValidationHelper.ValidateDataInput(paramCheck, validationResults, item.index, true))
                        {
                            //_CRM_equipmentRepo.SetCreatedLog(item.value);
                        }
                    }

                    if (validationResults.Any())
                    {
                        return PopulateValidationError<List<CRM_equipmentDto>>(validationResults.Distinct().ToList(), null);
                    }
                    else
                    {
                        var isSuccess = _CRM_equipmentRepo.BulkInsert(CRM_equipmentList);
                        result.success = isSuccess;
                        if (result.success)
                            result.total = CRM_equipmentList.Count;
                        result.lst = CRM_equipmentList.ConvertList<CRM_equipment, CRM_equipmentDto>();
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
        /// Update CRM_equipment
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<CRM_equipmentDto> Update(CRM_equipmentParameterDto paramUpdate)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_equipmentDto> Update(CRM_equipmentUpdateParameterDto paramUpdate)
        {
            var result = new ResponseBaseCustom<CRM_equipmentDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_equipment), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_equipment), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_equipment), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_equipment), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "equipmentid", paramUpdate.equipmentid.ToString(), false, criterias);
                List<CRM_equipment> data = _CRM_equipmentRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    PropertyInfo[] properties = typeof(CRM_equipmentUpdateParameterDto).GetProperties();
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
                    paramCheck.xts_businessunitid = paramUpdate.businessunitid;
                    #region PickList
                    List<ParamPick> param = new List<ParamPick>();
                    param.Add(new ParamPick { Category = "equipment.xts_type", ValueId = paramUpdate.xts_type });
                    paramCheck.pickList = param;
                    #endregion

                    if (paramUpdate.xts_type == 3 && (paramUpdate.xts_vehicleidentificationnumber == null || paramUpdate.xts_vehicleidentificationnumber.Trim() == ""))
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, "xts_vehicleidentificationnumber")));
                    }
                    if (paramUpdate.xts_type == 4 || paramUpdate.xts_type == 5)
                    {
                        if (paramUpdate.xts_employeeid.ToString() == "00000000-0000-0000-0000-000000000000" || paramUpdate.xts_employeeid == null)
                        {
                            string ErrorMssageGuid = string.Format(MessageResource.ErrorMsgDataRequired, "xts_employeeid");
                            ErrorMssageGuid = ErrorMssageGuid + " Tidak dapat menggunakan '00000000-0000-0000-0000-000000000000'";
                            validationResults.Add(new DNetValidationResult(ErrorMssageGuid));
                        }
                        else
                        {
                            paramCheck.xts_employeeid = paramUpdate.xts_employeeid;
                        }
                    }


                    if (!ValidationHelper.ValidateDataInput(paramCheck, validationResults))
                    {
                        ValidationDapper dapper = new ValidationDapper();
                        List<MessageBase> errMsg = dapper.messageList(validationResults);
                        result.messages = errMsg;
                    }
                    else
                    {
                        CRM_equipment updateRow = paramUpdate.ConvertObject<CRM_equipment>();
                        updateRow.DealerCode = DealerCode;
                        updateRow.SourceType = "NON DMS";
                        updateRow.RowStatus = updateRow.RowStatus == null ? 0 : updateRow.RowStatus;

                        var nResult = _CRM_equipmentRepo.Update(updateRow);
                        if (nResult.Success == true)
                        {
                            result.success = true;
                            result._idGuid = nResult.Data.ConvertObject<CRM_equipment>().equipmentid;
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
        /// Delete CRM_equipment by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<CRM_equipmentDto> Delete(int ID)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_equipmentDto> Delete(CRM_equipmentDeleteParameterDto paramDelete)
        {
            var result = new ResponseBaseCustom<CRM_equipmentDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_equipment), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_equipment), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_equipment), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_equipment), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "equipmentid", paramDelete.equipmentid.ToString(), false, criterias);

                List<CRM_equipment> data = _CRM_equipmentRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);
                if (data != null && data.Count > 0)
                {
                    CRM_equipment deleteRow = data[0];
                    deleteRow.RowStatus = -1; deleteRow.modifiedby = paramDelete.modifiedby; deleteRow.modifiedon = Convert.ToDateTime(paramDelete.modifiedon).AddHours(-7);
                    var nResult = _CRM_equipmentRepo.Update(deleteRow);
                    if (nResult.Success == true)
                    {
                        result.success = true;
                        result._idGuid = nResult.Data.ConvertObject<CRM_equipment>().equipmentid;
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
        private bool ValidateDuplicateParamData(List<CRM_equipmentCreateParameterDto> lstObjCreate, List<DNetValidationResult> validationResults)
        {
            foreach (var item in lstObjCreate)
            {
                var lst = lstObjCreate.Where(x => x.businessunitid.ToString().Trim() == item.businessunitid.ToString().Trim() &&
                                                x.name.ToString().Trim() == item.name.ToString().Trim());
                if (lst.Count() > 1)
                {
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDuplicateData) + string.Format(" ({0},{1})", "businessunitid = " + item.businessunitid.ToString(), "name = " + item.name.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }
            return validationResults.Count == 0;
        }

        private bool ValidateDuplicateDataInDB(List<CRM_equipmentCreateParameterDto> CRM_equipmentList, List<DNetValidationResult> validationResults, ref List<CRM_equipmentDto> listOfExistingData)
        {
            listOfExistingData = new List<CRM_equipmentDto>();

            foreach (CRM_equipmentCreateParameterDto item in CRM_equipmentList)
            {
                //check duplicate
                List<CRM_equipment> duplicateinDBlist = GetExistingCRM_equipment(item);
                if (duplicateinDBlist != null)
                {
                    CRM_equipment duplicateinDB = duplicateinDBlist.FirstOrDefault();
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDataDBExist) + string.Format(" ({0},{1})", "businessunitid = " + item.businessunitid.ToString(), "name = " + item.name.ToString() + " | " + duplicateinDB.equipmentid.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }

            return validationResults.Count == 0;
        }

        private List<CRM_equipment> GetExistingCRM_equipment(CRM_equipmentCreateParameterDto param)
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_equipment), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_equipment), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_equipment), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_equipment), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "businessunitid", param.businessunitid.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_equipment), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "name", param.name.ToString(), false, criterias);

                List<CRM_equipment> data = _CRM_equipmentRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data.Count > 0)
                {
                    return data.Cast<CRM_equipment>().ToList();
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
