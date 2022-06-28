#region Summary
/*
 ===========================================================================
 AUTHOR        : Ivan
 PURPOSE       : ServiceMMSBL BL class
 SPECIAL NOTES : DNet WebApi Project
 ---------------------
 Copyright  (c) 2021 
 ---------------------
 $History      : $
 Created on 2021-10-26
 ===========================================================================
*/
#endregion


#region Namespace Imports
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.Domain;
using System.Linq.Expressions;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class ServiceMMSBL : AbstractBusinessLogic, IServiceMMSBL
    {
        #region Variables
        private StandardCodeBL _enumBL;
        private IServiceMMSRepository<ServiceMMS_IF, int> _ServiceMMSRepo;
        #endregion

        #region Constructor
        public ServiceMMSBL(IServiceMMSRepository<ServiceMMS_IF, int> ServiceMMSRepo)
        {
            _ServiceMMSRepo = ServiceMMSRepo;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Get ServiceMMS_IF by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<ServiceMMSDto>> Read(ServiceMMSFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<List<ServiceMMSDto>> ReadList(ServiceMMSFilterDto filterDto, int pageSize)
        {

            var result = new ResponseBase<List<ServiceMMSDto>>();
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            string rawSql = string.Empty;

            try
            {
                var innerQueryCriteria = string.Empty;
                var dc = DealerCode;
                var criterias = Helper.InitialStrCriteria(typeof(ServiceMMS_IF), filterDto);

                criterias = Helper.UpdateStrCriteria(typeof(ServiceMMS_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(ServiceMMS_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "SourceType", "NON DMS", false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(ServiceMMS_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(ServiceMMS_IF), filterDto);

                List<ServiceMMS_IF> data = _ServiceMMSRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    ServiceMMS_IF x = new ServiceMMS_IF();
                    result.lst = data.ConvertList<ServiceMMS_IF, ServiceMMSDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(ServiceMMS_IF), filterDto);
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
        /// Create a new ServiceMMS_IF
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        /// 

        public ResponseBase<ServiceMMSDto> Create(ServiceMMSParameterDto objCreate)
        {
            return null;
        }

        public ResponseBase<ServiceMMSDto> Create(ServiceMMSCreateParameterDto objCreate)
        {
            #region Declarations
            var result = new ResponseBase<ServiceMMSDto>();
            var validationResults = new List<DNetValidationResult>();
            var isValid = true;
            KTB.DNet.Domain.Dealer dealer = null;
            DealerBranch dealerBranch = null;
            ChassisMaster chassisMaster = null;
            ServiceMMS_IF createRow = null;
            #endregion

            try
            {
                if (isValid) { isValid = ValidationHelper.ValidateDealer(objCreate.BU, validationResults, this.DealerCode, ref dealer); }

                if (isValid) { isValid = ValidationHelper.ValidateDealerBranch(objCreate.BU, validationResults, objCreate.BU_Branch, ref dealerBranch); }

                if (isValid) { isValid = ValidationHelper.ValidateChassisMaster(objCreate.ChassisNo, validationResults, ref chassisMaster); }

                if (isValid) { isValid = convertParamToDomain(objCreate, dealer, dealerBranch, chassisMaster, ref createRow, true); }

                if (isValid) { isValid = ValidateDuplicateData(objCreate, createRow, validationResults); }

                if (isValid)
                {
                    var nResult = _ServiceMMSRepo.Create(createRow);

                    if (nResult.Success == true)
                    {
                        result.success = true;
                        result.total = 1;

                        ServiceMMS_IF resultData = nResult.Data as ServiceMMS_IF;
                        result._id = resultData.ID;
                    }
                    else
                    {
                        ErrorMsgHelper.ErrorMsgDBSave(result.messages);
                    }
                }
                else
                {
                    return PopulateValidationError<ServiceMMSDto>(validationResults, null);
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


        public ResponseBase<ServiceMMSDto> Update(ServiceMMSParameterDto objCreate)
        {
            return null;
        }


        /// <summary>
        /// Delete ServiceMMS_IF by its id
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<ServiceMMSDto> Update(ServiceMMSUpdateParameterDto objUpdate)
        {
            #region Declarations
            var result = new ResponseBase<ServiceMMSDto>();
            var validationResults = new List<DNetValidationResult>();
            var isValid = true;
            KTB.DNet.Domain.Dealer dealer = null;
            DealerBranch dealerBranch = null;
            ChassisMaster chassisMaster = null;
            ServiceMMS_IF updateRow = null;
            ServiceMMS_IF dataOnDB = null;
            #endregion

            try
            {
                if (isValid) { isValid = ValidationHelper.ValidateDealer(objUpdate.BU, validationResults, this.DealerCode, ref dealer); }

                if (isValid) { isValid = ValidationHelper.ValidateDealerBranch(objUpdate.BU, validationResults, objUpdate.BU_Branch, ref dealerBranch); }

                if (isValid) { isValid = checkDataOnDB(objUpdate, dealer, dealerBranch, ref dataOnDB, validationResults); }

                if (objUpdate.Status == 0)
                {
                    if (isValid) { isValid = ValidationHelper.ValidateChassisMaster(objUpdate.ChassisNo, validationResults, ref chassisMaster); }

                    if (isValid) { isValid = convertParamToDomain(objUpdate, dealer, dealerBranch, chassisMaster, ref updateRow, false); }
                }

                if (isValid)
                {
                    if (objUpdate.Status != 0 && dataOnDB.Status == 0)
                    {
                        dataOnDB.Status = 1;
                        dataOnDB.LastUpdateBy = DNetUserName;
                        dataOnDB.LastUpdateTime = DateTime.Now;
                    }
                    else if (objUpdate.Status != 0 && dataOnDB.Status != 0)
                    {
                        validationResults.Add(new DNetValidationResult(string.Format("Data Service MMS dengan ID '{0)' statusnya sudah InActive", objUpdate.ID)));
                        return PopulateValidationError<ServiceMMSDto>(validationResults, null);
                    }

                    if (updateRow != null && dataOnDB != null)
                    {
                        if (updateRow.DealerID != dataOnDB.DealerID)
                        {
                            validationResults.Add(new DNetValidationResult("Data BU tidak dapat diubah"));
                        }
                        if (updateRow.DealerBranchID != dataOnDB.DealerBranchID)
                        {
                            validationResults.Add(new DNetValidationResult("Data BU_Branch tidak dapat diubah"));
                        }
                        if (updateRow.ChassisMasterID != dataOnDB.ChassisMasterID)
                        {
                            validationResults.Add(new DNetValidationResult("Data ChassisNo tidak dapat diubah"));
                        }
                        if (updateRow.WONumber != dataOnDB.WONumber)
                        {
                            validationResults.Add(new DNetValidationResult("Data WO_No tidak dapat diubah"));
                        }

                        if (validationResults.Count > 0)
                        {
                            return PopulateValidationError<ServiceMMSDto>(validationResults, null);
                        }
                    }

                    var nResult = objUpdate.Status == 0 ? _ServiceMMSRepo.Update(updateRow) : _ServiceMMSRepo.Update(dataOnDB);

                    if (nResult.Success == true)
                    {
                        result.success = true;
                        result.total = 1;

                        ServiceMMS_IF resultData = nResult.Data as ServiceMMS_IF;
                        result._id = resultData.ID;
                    }
                    else
                    {
                        ErrorMsgHelper.ErrorMsgDBSave(result.messages);
                    }
                }
                else
                {
                    return PopulateValidationError<ServiceMMSDto>(validationResults, null);
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
        /// Delete ServiceMMS_IF by its id
        /// </summary>
        /// <param name="objDelete"></param>
        /// <returns></returns>
        public ResponseBase<ServiceMMSDto> Delete(int ID)
        {
            return null;
        }

        #endregion

        #region Private Method

        private bool convertParamToDomain(ServiceMMSParameterDto objParam, KTB.DNet.Domain.Dealer dealer, DealerBranch dealerBranch, ChassisMaster chassisMaster, ref ServiceMMS_IF createupdateRow, bool IsCreate)
        {
            createupdateRow = new ServiceMMS_IF();
            createupdateRow.DealerID = dealer.ID;
            if (dealerBranch != null)
            {
                createupdateRow.DealerBranchID = dealerBranch.ID;
            }
            createupdateRow.WONumber = objParam.WO_No;
            createupdateRow.ServiceDate = objParam.WO_Service_Date;
            createupdateRow.ChassisMasterID = chassisMaster.ID;
            createupdateRow.PlateNo = objParam.PlateNo;
            createupdateRow.NextEstimatedServiceDate = objParam.Next_Estimated_Service_Date;
            createupdateRow.Notes = objParam.Notes;
            createupdateRow.Status = objParam.Status;
            createupdateRow.RowStatus = 0;

            if (IsCreate)
            {
                createupdateRow.CreatedBy = DNetUserName;
                createupdateRow.CreatedTime = DateTime.Now;
            }
            else
            {
                createupdateRow.ID = Convert.ToInt32(objParam.ID);
                createupdateRow.LastUpdateBy = DNetUserName;
                createupdateRow.LastUpdateTime = DateTime.Now;
            }


            return true;
        }

        private bool ValidateDuplicateData(ServiceMMSParameterDto objParam, ServiceMMS_IF domainData, List<DNetValidationResult> validationResults)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criterias = "";
                criterias = Helper.UpdateStrCriteria(typeof(ServiceMMS_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerID", domainData.DealerID.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(ServiceMMS_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "WONumber", domainData.WONumber, false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(ServiceMMS_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "ServiceDate", Convert.ToDateTime(domainData.ServiceDate).ToString("yyyy-MM-dd"), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(ServiceMMS_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "ChassisMasterID", domainData.ChassisMasterID.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(ServiceMMS_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "Notes", domainData.Notes.Replace('\n', ' '), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(ServiceMMS_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "Status", "0", false, criterias);
                criterias = criterias.Replace("_IF", "");

                List<ServiceMMS_IF> data = _ServiceMMSRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data.Count > 0)
                {
                    string ErrorMessageDuplicate = string.Format("dengan BU '{0}', WO No '{1}', WO Service Date '{2}', ChassisNo '{3}' dan Notes '{4}'", objParam.BU, objParam.WO_No, Convert.ToDateTime(objParam.WO_Service_Date).ToString("dd/MM/yyyy"), objParam.ChassisNo, objParam.Notes);
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataIsExist, ErrorMessageDuplicate)));
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                validationResults.Add(new DNetValidationResult(ex.Message));
                return false;
                //throw;
            }
        }

        private bool checkDataOnDB(ServiceMMSParameterDto objParam, KTB.DNet.Domain.Dealer dealer, DealerBranch dealerBranch, ref ServiceMMS_IF domainData, List<DNetValidationResult> validationResults)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criterias = "";
                criterias = Helper.UpdateStrCriteria(typeof(ServiceMMS_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "ID", objParam.ID.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(ServiceMMS_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerID", dealer.ID.ToString(), false, criterias);
                criterias = Helper.UpdateStrCriteria(typeof(ServiceMMS_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);
                if (dealerBranch != null)
                {
                    criterias = Helper.UpdateStrCriteria(typeof(ServiceMMS_IF), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "DealerBranchID", dealerBranch.ID.ToString(), false, criterias);
                }
                criterias = criterias.Replace("_IF", "");

                List<ServiceMMS_IF> data = _ServiceMMSRepo.Search(
                                    criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                if (data.Count > 0)
                {
                    domainData = data[0];
                    return true;
                }

                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMessageDataNotFound, "Service MMS", objParam.ID)));
                return false;
            }
            catch (Exception ex)
            {
                validationResults.Add(new DNetValidationResult(ex.Message));
                return false;
                //throw;
            }
        }

        #endregion
    }
}
