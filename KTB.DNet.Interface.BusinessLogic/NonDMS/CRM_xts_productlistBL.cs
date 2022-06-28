#region Summary
/*
 ===========================================================================
 AUTHOR        : BSI Code Generator Interface 
 PURPOSE       : CRM_xts_productlist class
 SPECIAL NOTES : Generated from database BSIDMS_MMKSI_IF_ACC_DEV 
 GENERATED BY  : Nando (version : v.1.08)
 ---------------------
 Copyright  (c) 2020 
 ---------------------
 $History      : $
 Created on 03 Sep 2020 10:37:59
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
    public class CRM_xts_productlistBL : AbstractBusinessLogic, ICRM_xts_productlistBL
    {
        #region Variables
        private ICRM_xts_productlistRepository<CRM_xts_productlist, int> _CRM_xts_productlistRepo;
        private IDealerCompanyRepository<DealerCompany, int> _dealerCompanyRepo;
        #endregion

        #region Constructor
        public CRM_xts_productlistBL(ICRM_xts_productlistRepository<CRM_xts_productlist, int> CRM_xts_productlistRepo, IDealerCompanyRepository<DealerCompany, int> DealerCompanyRepo)
        {
            _CRM_xts_productlistRepo = CRM_xts_productlistRepo;
            _dealerCompanyRepo = DealerCompanyRepo;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Get CRM_xts_productlist by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<CRM_xts_productlistDto>> Read(CRM_xts_productlistFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<CRM_xts_productlistDto>> ReadList(CRM_xts_productlistFilterDto filterDto, int pageSize)
        {

            var result = new ResponseBase<List<CRM_xts_productlistDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                // get dealer group id
                var validationResults = new List<DNetValidationResult>();
                var dealer = new DNet.Domain.Dealer();
                var validateDealer = ValidationHelper.ValidateDealer(DealerCode, validationResults, DealerCode, ref dealer, false);

                // get dealer company code
                string dealerCompanyCode = string.Empty;
                int filteredresultCount = 0;
                int totalResultCount = 0;
                DealerCompanyPostModel dealerCompanyPostModel = new DealerCompanyPostModel();
                dealerCompanyPostModel.DealerGroupID = dealer.DealerGroup.ID;
                var dtDealerCompany = _dealerCompanyRepo.Search(dealerCompanyPostModel, out filteredresultCount, out totalResultCount);
                if (dtDealerCompany.Count > 0)
                {
                    dealerCompanyCode = dtDealerCompany[0].DealerCompanyCode;
                }
                else
                {
                    ErrorMsgHelper.Exception(result.messages, "Dealer Company belum terdaftar untuk DealerCode " + DealerCode);
                    return result;
                }

                var innerQueryCriteria = string.Empty;
                var criterias = Helper.InitialStrCriteria(typeof(CRM_xts_productlist), filterDto);

                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_productlist), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_company", dealerCompanyCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_productlist), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(CRM_xts_productlist), filterDto, "CRM_xts_productlist");

                List<CRM_xts_productlist> data = _CRM_xts_productlistRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    CRM_xts_productlist x = new CRM_xts_productlist();
                    result.lst = data.ConvertList<CRM_xts_productlist, CRM_xts_productlistDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(CRM_xts_productlist), filterDto);
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
        /// Create a new CRM_xts_productlist
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_productlistDto> Create(CRM_xts_productlistParameterDto objCreate)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_productlistDto> Create(CRM_xts_productlistCreateParameterDto paramCreate)
        {
            var result = new ResponseBaseCustom<CRM_xts_productlistDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_productlist), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_productlist), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_productlist), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_productlist), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_product", paramCreate.xts_product.ToString(), false, criterias);

                List<CRM_xts_productlist> data = _CRM_xts_productlistRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data != null && data.Count == 0)
                {
                    PropertyInfo[] properties = typeof(CRM_xts_productlistCreateParameterDto).GetProperties();
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

                    CRM_xts_productlist createRow = paramCreate.ConvertObject<CRM_xts_productlist>();
                    Guid newId = Guid.NewGuid();
                    createRow.xts_productlistid = newId;
                    createRow.DealerCode = DealerCode;
                    createRow.SourceType = "NON DMS"; createRow.createdby = (createRow.createdby == null || createRow.createdby.ToString() == "00000000-0000-0000-0000-000000000000") ? createRow.modifiedby : createRow.createdby;
                    createRow.RowStatus = 0; createRow.statecode = 0;

                    //Add Validation Data Input
                    ValidationParameterDto paramCheck = new ValidationParameterDto();
                    paramCheck.dealerCode = DealerCode;
                    //paramCheck.xts_company = paramCreate.xts_company;
                    paramCheck.ownerid = paramCreate.ownerid;
                    paramCheck.xts_purchaseunitid = paramCreate.xts_purchaseunitid;
                    paramCheck.xts_salesunitid = paramCreate.xts_salesunitid;

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
                        var nResult = _CRM_xts_productlistRepo.Create(createRow);
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
                    ErrorMsgHelper.Exception(result.messages, String.Concat("Data already exist | " + data[0].xts_productlistid.ToString()));
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



        public ResponseBase<List<CRM_xts_productlistDto>> BulkCreate(List<CRM_xts_productlistCreateParameterDto> lstObjCreate)
        {
            var result = new ResponseBase<List<CRM_xts_productlistDto>>();
            var listOfExistingData = new List<CRM_xts_productlistDto>();
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
                    return PopulateValidationError<List<CRM_xts_productlistDto>>(validationResults.Distinct().ToList(), null);
                }
                else
                {
                    // insert into db
                    var CRM_xts_productlistList = lstObjCreate.ConvertList<CRM_xts_productlistCreateParameterDto, CRM_xts_productlist>();
                    foreach (var item in CRM_xts_productlistList.Select((value, index) => new { value, index }))
                    {
                        PropertyInfo[] properties = typeof(CRM_xts_productlist).GetProperties();
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
                        item.value.xts_productlistid = newId;
                        item.value.DealerCode = DealerCode;
                        item.value.SourceType = "NON DMS"; item.value.createdby = (item.value.createdby == null || item.value.createdby.ToString() == "00000000-0000-0000-0000-000000000000") ? item.value.modifiedby : item.value.createdby;
                        item.value.RowStatus = 0; 
                        item.value.statecode = item.value.statecode == null ? 0 : item.value.statecode;

                        //Add Validation Data Input
                        ValidationParameterDto paramCheck = new ValidationParameterDto();
                        paramCheck.dealerCode = DealerCode;
                        //paramCheck.xts_company = item.value.xts_company;
                        paramCheck.ownerid = item.value.ownerid;
                        paramCheck.xts_purchaseunitid = item.value.xts_purchaseunitid;
                        paramCheck.xts_salesunitid = item.value.xts_salesunitid;

                        ValidationDapper dapper_ = new ValidationDapper();
                        string xts_companyByDealerCode = dapper_.getBUbyDealerCode(DealerCode).msdyn_companycode;
                        if (item.value.xts_company.ToLower() != xts_companyByDealerCode.ToLower())
                        {
                            item.value.xts_company = xts_companyByDealerCode;
                        }

                        if (ValidationHelper.ValidateDataInput(paramCheck, validationResults, item.index, true))
                        {
                            //_CRM_xts_productlistRepo.SetCreatedLog(item.value);
                        }
                    }

                    if (validationResults.Any())
                    {
                        return PopulateValidationError<List<CRM_xts_productlistDto>>(validationResults.Distinct().ToList(), null);
                    }
                    else
                    {
                        var isSuccess = _CRM_xts_productlistRepo.BulkInsert(CRM_xts_productlistList);
                        result.success = isSuccess;
                        if (result.success)
                            result.total = CRM_xts_productlistList.Count;
                        result.lst = CRM_xts_productlistList.ConvertList<CRM_xts_productlist, CRM_xts_productlistDto>();
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
        /// Update CRM_xts_productlist
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_productlistDto> Update(CRM_xts_productlistParameterDto paramUpdate)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_productlistDto> Update(CRM_xts_productlistUpdateParameterDto paramUpdate)
        {
            var result = new ResponseBaseCustom<CRM_xts_productlistDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_productlist), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_productlist), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_productlist), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_productlist), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_productlistid", paramUpdate.xts_productlistid.ToString(), false, criterias);
                List<CRM_xts_productlist> data = _CRM_xts_productlistRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    PropertyInfo[] properties = typeof(CRM_xts_productlistUpdateParameterDto).GetProperties();
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
                    //paramCheck.xts_company = paramUpdate.xts_company;
                    paramCheck.ownerid = paramUpdate.ownerid;
                    paramCheck.xts_purchaseunitid = paramUpdate.xts_purchaseunitid;
                    paramCheck.xts_salesunitid = paramUpdate.xts_salesunitid;


                    if (!ValidationHelper.ValidateDataInput(paramCheck, validationResults))
                    {
                        ValidationDapper dapper = new ValidationDapper();
                        List<MessageBase> errMsg = dapper.messageList(validationResults);
                        result.messages = errMsg;
                    }
                    else
                    {
                        CRM_xts_productlist updateRow = paramUpdate.ConvertObject<CRM_xts_productlist>();
                        updateRow.DealerCode = DealerCode;
                        updateRow.SourceType = "NON DMS";
                        updateRow.RowStatus = updateRow.RowStatus == null ? 0 : updateRow.RowStatus;

                        ValidationDapper dapper_ = new ValidationDapper();
                        string xts_companyByDealerCode = dapper_.getBUbyDealerCode(DealerCode).msdyn_companycode;
                        if (updateRow.xts_company.ToLower() != xts_companyByDealerCode.ToLower())
                        {
                            updateRow.xts_company = xts_companyByDealerCode;
                        }

                        var nResult = _CRM_xts_productlistRepo.Update(updateRow);
                        if (nResult.Success == true)
                        {
                            result.success = true;
                            result._idGuid = nResult.Data.ConvertObject<CRM_xts_productlist>().xts_productlistid;
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
        /// Delete CRM_xts_productlist by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<CRM_xts_productlistDto> Delete(int ID)
        {
            return null;
        }


        public ResponseBaseCustom<CRM_xts_productlistDto> Delete(CRM_xts_productlistDeleteParameterDto paramDelete)
        {
            var result = new ResponseBaseCustom<CRM_xts_productlistDto>();
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_productlist), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_productlist), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_productlist), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_productlist), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_productlistid", paramDelete.xts_productlistid.ToString(), false, criterias);

                List<CRM_xts_productlist> data = _CRM_xts_productlistRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);
                if (data != null && data.Count > 0)
                {
                    CRM_xts_productlist deleteRow = data[0];
                    deleteRow.RowStatus = -1; deleteRow.modifiedby = paramDelete.modifiedby; deleteRow.modifiedon = Convert.ToDateTime(paramDelete.modifiedon).AddHours(-7);
                    var nResult = _CRM_xts_productlistRepo.Update(deleteRow);
                    if (nResult.Success == true)
                    {
                        result.success = true;
                        result._idGuid = nResult.Data.ConvertObject<CRM_xts_productlist>().xts_productlistid;
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
        private bool ValidateDuplicateParamData(List<CRM_xts_productlistCreateParameterDto> lstObjCreate, List<DNetValidationResult> validationResults)
        {
            foreach (var item in lstObjCreate)
            {
                var lst = lstObjCreate.Where(x => x.xts_product.ToString().Trim() == item.xts_product.ToString().Trim());
                if (lst.Count() > 1)
                {
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDuplicateData) + string.Format(" ({0})", "xts_product = " + item.xts_product.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }
            return validationResults.Count == 0;
        }

        private bool ValidateDuplicateDataInDB(List<CRM_xts_productlistCreateParameterDto> CRM_xts_productlistList, List<DNetValidationResult> validationResults, ref List<CRM_xts_productlistDto> listOfExistingData)
        {
            listOfExistingData = new List<CRM_xts_productlistDto>();

            foreach (CRM_xts_productlistCreateParameterDto item in CRM_xts_productlistList)
            {
                //check duplicate
                List<CRM_xts_productlist> duplicateinDBlist = GetExistingCRM_xts_productlist(item);
                if (duplicateinDBlist != null)
                {
                    CRM_xts_productlist duplicateinDB = duplicateinDBlist.FirstOrDefault();
                    String errorMessage = string.Format(string.Format(MessageResource.ErrorMsgDataDBExist) + string.Format(" ({0},{1})", "xts_product = " + item.xts_product.ToString(), "statecode=0" + " | " + duplicateinDB.xts_productlistid.ToString()));
                    bool isErrorExist = validationResults.Any(q => q.ErrorMessage == errorMessage);
                    if (isErrorExist == false)
                    {
                        validationResults.Add(new DNetValidationResult(errorMessage));
                    }
                }
            }

            return validationResults.Count == 0;
        }

        private List<CRM_xts_productlist> GetExistingCRM_xts_productlist(CRM_xts_productlistCreateParameterDto param)
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
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_productlist), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_productlist), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_productlist), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_productlist), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_product", param.xts_product.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(CRM_xts_productlist), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "statecode", "0", false, criterias);

                List<CRM_xts_productlist> data = _CRM_xts_productlistRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data.Count > 0)
                {
                    return data.Cast<CRM_xts_productlist>().ToList();
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