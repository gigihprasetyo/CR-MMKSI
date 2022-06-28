#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_newvehiclesalesorder class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV 
 GENERATED BY  : Kemin
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 01 Sep 2020 10:12:24
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
    public class CRM_xts_newvehiclesalesorderBL : AbstractBusinessLogic, ICRM_xts_newvehiclesalesorderBL
    {
        #region Variables
        private ICRM_xts_newvehiclesalesorderRepository<CRM_xts_newvehiclesalesorder, int> _CRM_xts_newvehiclesalesorderRepo;
        #endregion

        #region Constructor
        public CRM_xts_newvehiclesalesorderBL(ICRM_xts_newvehiclesalesorderRepository<CRM_xts_newvehiclesalesorder, int> CRM_xts_newvehiclesalesorderRepo)
        {
            _CRM_xts_newvehiclesalesorderRepo = CRM_xts_newvehiclesalesorderRepo;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Get CRM_xts_newvehiclesalesorder by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<CRM_xts_newvehiclesalesorderDto>> Read(CRM_xts_newvehiclesalesorderFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<CRM_xts_newvehiclesalesorderDto>> ReadList(CRM_xts_newvehiclesalesorderFilterDto filterDto, int pageSize)
        {

            var result = new ResponseBase<List<CRM_xts_newvehiclesalesorderDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                var criterias = Helper.InitialStrCriteria(typeof(CRM_xts_newvehiclesalesorder), filterDto);

                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_newvehiclesalesorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_newvehiclesalesorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_newvehiclesalesorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(CRM_xts_newvehiclesalesorder), filterDto, "CRM_xts_newvehiclesalesorder");

                List<CRM_xts_newvehiclesalesorder> data = _CRM_xts_newvehiclesalesorderRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    CRM_xts_newvehiclesalesorder x = new CRM_xts_newvehiclesalesorder();
                    result.lst = data.ConvertList<CRM_xts_newvehiclesalesorder, CRM_xts_newvehiclesalesorderDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(CRM_xts_newvehiclesalesorder), filterDto);
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
        /// Create a new CRM_xts_newvehiclesalesorder
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_newvehiclesalesorderDto> Create(CRM_xts_newvehiclesalesorderParameterDto objCreate)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_newvehiclesalesorderDto> Create(CRM_xts_newvehiclesalesorderCreateParameterDto paramCreate)
        {
            var result = new ResponseBaseCustom<CRM_xts_newvehiclesalesorderDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_newvehiclesalesorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_newvehiclesalesorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_newvehiclesalesorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_newvehiclesalesorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "ktb_dnetid", paramCreate.ktb_dnetid.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_newvehiclesalesorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "ktb_dnetspknumber", paramCreate.ktb_dnetspknumber.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_newvehiclesalesorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_newvehiclesalesordernumber", paramCreate.xts_newvehiclesalesordernumber.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_newvehiclesalesorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_opportunityid", paramCreate.xts_opportunityid.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_newvehiclesalesorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_wholesaleorderid", paramCreate.xts_wholesaleorderid.ToString(), false, criterias);

                List<CRM_xts_newvehiclesalesorder> data = _CRM_xts_newvehiclesalesorderRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data != null && data.Count == 0)
                {
                    PropertyInfo[] properties = typeof(CRM_xts_newvehiclesalesorderCreateParameterDto).GetProperties();
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

                    CRM_xts_newvehiclesalesorder createRow = paramCreate.ConvertObject<CRM_xts_newvehiclesalesorder>();
                    Guid newId = Guid.NewGuid();
                    createRow.xts_newvehiclesalesorderid = newId;
                    createRow.DealerCode = DealerCode;
                    createRow.SourceType = "NON DMS"; createRow.createdby = (createRow.createdby == null || createRow.createdby.ToString() == "00000000-0000-0000-0000-000000000000") ? createRow.modifiedby : createRow.createdby;
                    createRow.RowStatus = "0"; createRow.statecode = 0;

                    //Add Validation Data Input
                    ValidationParameterDto paramCheck = new ValidationParameterDto();
                    paramCheck.dealerCode = DealerCode;
                    paramCheck.xts_businessunitid = paramCreate.xts_businessunitid;
                    paramCheck.xts_billtoid = paramCreate.xts_billtoid;
                    paramCheck.xts_ordertypeid = paramCreate.xts_ordertypeid;
                    paramCheck.xts_ownerid = paramCreate.xts_ownerid;
                    paramCheck.xts_potentialcustomerid = paramCreate.xts_potentialcustomerid;
                    paramCheck.xts_productid = paramCreate.xts_productid;
                    paramCheck.xts_salespersonid = paramCreate.xts_salespersonid;
                    paramCheck.xts_siteid = paramCreate.xts_siteid;
                    paramCheck.xts_specialcolorpriceid = paramCreate.xts_specialcolorpriceid;
                    paramCheck.xts_vehiclepricelistid = paramCreate.xts_vehiclepricelistid;
                    paramCheck.xts_wholesaleorderid = paramCreate.xts_wholesaleorderid;
                    #region PickList
                    List<ParamPick> param = new List<ParamPick>();
                    param.Add(new ParamPick { Category = "xts_newvehiclesalesorder.xjp_taxationform", ValueId = paramCreate.xjp_taxationform });
                    param.Add(new ParamPick { Category = "xts_newvehiclesalesorder.xjp_usagecategory", ValueId = paramCreate.xjp_usagecategory });
                    param.Add(new ParamPick { Category = "xts_newvehiclesalesorder.xjp_vehiclemanagementcategory", ValueId = paramCreate.xjp_vehiclemanagementcategory });
                    param.Add(new ParamPick { Category = "xts_newvehiclesalesorder.ktb_pembayaran", ValueId = paramCreate.ktb_pembayaran });
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
                        var nResult = _CRM_xts_newvehiclesalesorderRepo.Create(createRow);
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
                    ErrorMsgHelper.Exception(result.messages, String.Concat("Data already exist | " + data[0].xts_newvehiclesalesorderid.ToString()));
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



        public ResponseBase<List<CRM_xts_newvehiclesalesorderDto>> BulkCreate(List<CRM_xts_newvehiclesalesorderCreateParameterDto> lstObjCreate)
        {
            var result = new ResponseBase<List<CRM_xts_newvehiclesalesorderDto>>();
            var listOfExistingData = new List<CRM_xts_newvehiclesalesorderDto>();
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
                    return PopulateValidationError<List<CRM_xts_newvehiclesalesorderDto>>(validationResults.Distinct().ToList(), null);
                }
                else
                {
                    // insert into db
                    var CRM_xts_newvehiclesalesorderList = lstObjCreate.ConvertList<CRM_xts_newvehiclesalesorderCreateParameterDto, CRM_xts_newvehiclesalesorder>();
                    foreach (var item in CRM_xts_newvehiclesalesorderList.Select((value, index) => new { value, index }))
                    {
                        PropertyInfo[] properties = typeof(CRM_xts_newvehiclesalesorder).GetProperties();
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
                        item.value.xts_newvehiclesalesorderid = newId;
                        item.value.DealerCode = DealerCode;
                        item.value.SourceType = "NON DMS"; item.value.createdby = (item.value.createdby == null || item.value.createdby.ToString() == "00000000-0000-0000-0000-000000000000") ? item.value.modifiedby : item.value.createdby;
                        item.value.RowStatus = "0"; 
                        item.value.statecode = item.value.statecode == null ? 0 : item.value.statecode;

                        //Add Validation Data Input
                        ValidationParameterDto paramCheck = new ValidationParameterDto();
                        paramCheck.dealerCode = DealerCode;
                        paramCheck.xts_businessunitid = item.value.xts_businessunitid;
                        paramCheck.xts_billtoid = item.value.xts_billtoid;
                        paramCheck.xts_ordertypeid = item.value.xts_ordertypeid;
                        paramCheck.xts_ownerid = item.value.xts_ownerid;
                        paramCheck.xts_potentialcustomerid = item.value.xts_potentialcustomerid;
                        paramCheck.xts_productid = item.value.xts_productid;
                        paramCheck.xts_salespersonid = item.value.xts_salespersonid;
                        paramCheck.xts_siteid = item.value.xts_siteid;
                        paramCheck.xts_specialcolorpriceid = item.value.xts_specialcolorpriceid;
                        paramCheck.xts_vehiclepricelistid = item.value.xts_vehiclepricelistid;
                        paramCheck.xts_wholesaleorderid = item.value.xts_wholesaleorderid;
                        #region PickList
                        List<ParamPick> param = new List<ParamPick>();
                        param.Add(new ParamPick { Category = "xts_newvehiclesalesorder.xjp_taxationform", ValueId = Convert.ToInt32(item.value.xjp_taxationform) });
                        param.Add(new ParamPick { Category = "xts_newvehiclesalesorder.xjp_usagecategory", ValueId = Convert.ToInt32(item.value.xjp_usagecategory) });
                        param.Add(new ParamPick { Category = "xts_newvehiclesalesorder.xjp_vehiclemanagementcategory", ValueId = Convert.ToInt32(item.value.xjp_vehiclemanagementcategory) });
                        param.Add(new ParamPick { Category = "xts_newvehiclesalesorder.ktb_pembayaran", ValueId = Convert.ToInt32(item.value.ktb_pembayaran) });
                        paramCheck.pickList = param;
                        #endregion

                        if (ValidationHelper.ValidateDataInput(paramCheck, validationResults, item.index, true))
                        {
                            //_CRM_xts_newvehiclesalesorderRepo.SetCreatedLog(item.value);
                        }
                    }

                    if (validationResults.Any())
                    {
                        return PopulateValidationError<List<CRM_xts_newvehiclesalesorderDto>>(validationResults.Distinct().ToList(), null);
                    }
                    else
                    {
                        var isSuccess = _CRM_xts_newvehiclesalesorderRepo.BulkInsert(CRM_xts_newvehiclesalesorderList);
                        result.success = isSuccess;
                        if (result.success)
                            result.total = CRM_xts_newvehiclesalesorderList.Count;
                        result.lst = CRM_xts_newvehiclesalesorderList.ConvertList<CRM_xts_newvehiclesalesorder, CRM_xts_newvehiclesalesorderDto>();
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
        /// Update CRM_xts_newvehiclesalesorder
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_newvehiclesalesorderDto> Update(CRM_xts_newvehiclesalesorderParameterDto paramUpdate)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_newvehiclesalesorderDto> Update(CRM_xts_newvehiclesalesorderUpdateParameterDto paramUpdate)
        {
            var result = new ResponseBaseCustom<CRM_xts_newvehiclesalesorderDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_newvehiclesalesorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_newvehiclesalesorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_newvehiclesalesorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_newvehiclesalesorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_newvehiclesalesorderid", paramUpdate.xts_newvehiclesalesorderid.ToString(), false, criterias);
                List<CRM_xts_newvehiclesalesorder> data = _CRM_xts_newvehiclesalesorderRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    PropertyInfo[] properties = typeof(CRM_xts_newvehiclesalesorderUpdateParameterDto).GetProperties();
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
                    paramCheck.xts_billtoid = paramUpdate.xts_billtoid;
                    paramCheck.xts_ordertypeid = paramUpdate.xts_ordertypeid;
                    paramCheck.xts_ownerid = paramUpdate.xts_ownerid;
                    paramCheck.xts_potentialcustomerid = paramUpdate.xts_potentialcustomerid;
                    paramCheck.xts_productid = paramUpdate.xts_productid;
                    paramCheck.xts_salespersonid = paramUpdate.xts_salespersonid;
                    paramCheck.xts_siteid = paramUpdate.xts_siteid;
                    paramCheck.xts_specialcolorpriceid = paramUpdate.xts_specialcolorpriceid;
                    paramCheck.xts_vehiclepricelistid = paramUpdate.xts_vehiclepricelistid;
                    paramCheck.xts_wholesaleorderid = paramUpdate.xts_wholesaleorderid;
                    #region PickList
                    List<ParamPick> param = new List<ParamPick>();
                    param.Add(new ParamPick { Category = "xts_newvehiclesalesorder.xjp_taxationform", ValueId = paramUpdate.xjp_taxationform });
                    param.Add(new ParamPick { Category = "xts_newvehiclesalesorder.xjp_usagecategory", ValueId = paramUpdate.xjp_usagecategory });
                    param.Add(new ParamPick { Category = "xts_newvehiclesalesorder.xjp_vehiclemanagementcategory", ValueId = paramUpdate.xjp_vehiclemanagementcategory });
                    param.Add(new ParamPick { Category = "xts_newvehiclesalesorder.ktb_pembayaran", ValueId = paramUpdate.ktb_pembayaran });
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
                        CRM_xts_newvehiclesalesorder updateRow = paramUpdate.ConvertObject<CRM_xts_newvehiclesalesorder>();
                        updateRow.DealerCode = DealerCode;
                        updateRow.SourceType = "NON DMS";
                        updateRow.RowStatus = string.IsNullOrEmpty(updateRow.RowStatus) ? "0" : updateRow.RowStatus;

                        var nResult = _CRM_xts_newvehiclesalesorderRepo.Update(updateRow);
                        if (nResult.Success == true)
                        {
                            result.success = true;
                            result._idGuid = nResult.Data.ConvertObject<CRM_xts_newvehiclesalesorder>().xts_newvehiclesalesorderid;
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
        /// Delete CRM_xts_newvehiclesalesorder by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_newvehiclesalesorderDto> Delete(int ID)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_newvehiclesalesorderDto> Delete(CRM_xts_newvehiclesalesorderDeleteParameterDto paramDelete)
        {
            var result = new ResponseBaseCustom<CRM_xts_newvehiclesalesorderDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_newvehiclesalesorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_newvehiclesalesorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_newvehiclesalesorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_newvehiclesalesorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_newvehiclesalesorderid", paramDelete.xts_newvehiclesalesorderid.ToString(), false, criterias);

                List<CRM_xts_newvehiclesalesorder> data = _CRM_xts_newvehiclesalesorderRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);
                if (data != null && data.Count > 0)
                {
                    CRM_xts_newvehiclesalesorder deleteRow = data[0];
                    deleteRow.RowStatus = "-1"; deleteRow.modifiedby = paramDelete.modifiedby; deleteRow.modifiedon = Convert.ToDateTime(paramDelete.modifiedon).AddHours(-7);
                    var nResult = _CRM_xts_newvehiclesalesorderRepo.Update(deleteRow);
                    if (nResult.Success == true)
                    {
                        result.success = true;
                        result._idGuid = nResult.Data.ConvertObject<CRM_xts_newvehiclesalesorder>().xts_newvehiclesalesorderid;
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
        private bool ValidateDuplicateParamData(List<CRM_xts_newvehiclesalesorderCreateParameterDto> lstObjCreate, List<DNetValidationResult> validationResults)
        {
            foreach (var item in lstObjCreate)
            {
                var lst = lstObjCreate.Where(x => x.ktb_dnetid.ToString().Trim() == item.ktb_dnetid.ToString().Trim() &&
                                                x.ktb_dnetspknumber.ToString().Trim() == item.ktb_dnetspknumber.ToString().Trim() &&
                                                x.xts_newvehiclesalesordernumber.ToString().Trim() == item.xts_newvehiclesalesordernumber.ToString().Trim() &&
                                                x.xts_opportunityid.ToString().Trim() == item.xts_opportunityid.ToString().Trim() &&
                                                x.xts_wholesaleorderid.ToString().Trim() == item.xts_wholesaleorderid.ToString().Trim());
                if (lst.Count() > 1)
                {
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDuplicateData) + string.Format(" ({0},{1},{2},{3},{4})", "ktb_dnetid = " + item.ktb_dnetid.ToString(), "ktb_dnetspknumber = " + item.ktb_dnetspknumber.ToString(), "xts_newvehiclesalesordernumber = " + item.xts_newvehiclesalesordernumber.ToString(), "xts_opportunityid = " + item.xts_opportunityid.ToString(), "xts_wholesaleorderid = " + item.xts_wholesaleorderid.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }
            return validationResults.Count == 0;
        }

        private bool ValidateDuplicateDataInDB(List<CRM_xts_newvehiclesalesorderCreateParameterDto> CRM_xts_newvehiclesalesorderList, List<DNetValidationResult> validationResults, ref List<CRM_xts_newvehiclesalesorderDto> listOfExistingData)
        {
            listOfExistingData = new List<CRM_xts_newvehiclesalesorderDto>();

            foreach (CRM_xts_newvehiclesalesorderCreateParameterDto item in CRM_xts_newvehiclesalesorderList)
            {
                //check duplicate
                List<CRM_xts_newvehiclesalesorder> duplicateinDBlist = GetExistingCRM_xts_newvehiclesalesorder(item);
                if (duplicateinDBlist != null)
                {
                    CRM_xts_newvehiclesalesorder duplicateinDB = duplicateinDBlist.FirstOrDefault();
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDataDBExist) + string.Format(" ({0},{1},{2},{3},{4},{5})", "ktb_dnetid = " + item.ktb_dnetid.ToString(), "ktb_dnetspknumber = " + item.ktb_dnetspknumber.ToString(), "xts_newvehiclesalesordernumber = " + item.xts_newvehiclesalesordernumber.ToString(), "xts_opportunityid = " + item.xts_opportunityid.ToString(), "xts_wholesaleorderid = " + item.xts_wholesaleorderid.ToString(), "statecode=0" + " | " + duplicateinDB.xts_newvehiclesalesorderid.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }

            return validationResults.Count == 0;
        }

        private List<CRM_xts_newvehiclesalesorder> GetExistingCRM_xts_newvehiclesalesorder(CRM_xts_newvehiclesalesorderCreateParameterDto param)
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_newvehiclesalesorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_newvehiclesalesorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_newvehiclesalesorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_newvehiclesalesorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "ktb_dnetid", param.ktb_dnetid.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_newvehiclesalesorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "ktb_dnetspknumber", param.ktb_dnetspknumber.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_newvehiclesalesorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_newvehiclesalesordernumber", param.xts_newvehiclesalesordernumber.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_newvehiclesalesorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_opportunityid", param.xts_opportunityid.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_newvehiclesalesorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_wholesaleorderid", param.xts_wholesaleorderid.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_newvehiclesalesorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "statecode", "0", false, criterias);

                List<CRM_xts_newvehiclesalesorder> data = _CRM_xts_newvehiclesalesorderRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data.Count > 0)
                {
                    return data.Cast<CRM_xts_newvehiclesalesorder>().ToList();
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
