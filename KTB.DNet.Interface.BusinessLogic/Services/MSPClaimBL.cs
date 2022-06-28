#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : MSPClaim business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 9/11/2018 9:51
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class MSPClaimBL : AbstractBusinessLogic, IMSPClaimBL
    {
        #region Variables
        private readonly IMapper _mspclaimMapper;
        private readonly IMapper _pmKindMapper;
        private readonly IMapper _pmHeaderMapper;
        private readonly IMapper _mspDurationPMKindMapper;
        private readonly IMapper _mspRegistrationHistoryMapper;
        private readonly AutoMapper.IMapper _mapper;
        private StandardCodeCharBL _enumCharBL;
        private AppConfigBL _appConfigBL;
        #endregion

        #region Constructor
        public MSPClaimBL()
        {
            _mspclaimMapper = MapperFactory.GetInstance().GetMapper(typeof(MSPClaim).ToString());
            _pmKindMapper = MapperFactory.GetInstance().GetMapper(typeof(PMKind).ToString());
            _pmHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(PMHeader).ToString());
            _mspDurationPMKindMapper = MapperFactory.GetInstance().GetMapper(typeof(MSPDurationPMKind).ToString());
            _mspRegistrationHistoryMapper = MapperFactory.GetInstance().GetMapper(typeof(MSPRegistrationHistory).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _enumCharBL = new StandardCodeCharBL(_mapper);
            _appConfigBL = new AppConfigBL(_mapper);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get MSPClaim by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <returns></returns>
        public ResponseBase<List<MSPClaimDto>> Read(MSPClaimFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(MSPClaim), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(MSPClaim), "Dealer.DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<MSPClaimDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(MSPClaim), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(MSPClaim), filterDto, sortColl);

                // get data
                var data = _mspclaimMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<MSPClaim>().ToList();
                    var listData = list.Select(item => _mapper.Map<MSPClaimDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(MSPClaim), filterDto);
                }

                result.success = true;

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
        /// Delete MSPClaim by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<MSPClaimDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Create a new MSPClaim
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<MSPClaimDto> Create(MSPClaimParameterDto objCreate)
        {
            #region Declare
            var result = new ResponseBase<MSPClaimDto>();
            var validationResults = new List<DNetValidationResult>();
            ChassisMaster chassisMaster = null;
            Dealer dealer = null;
            DealerBranch dealerBranch = null;
            PMKind pmKind = null;
            PMHeader pmHeader = null;
            var isValid = true;
            #endregion

            try
            {
                // parse the parameter into object
                MSPClaim newMSPClaim = _mapper.Map<MSPClaim>(objCreate);

                // validate mspclaim parameter values
                isValid = ValidateMSP(objCreate, validationResults, ref newMSPClaim, ref chassisMaster, ref pmHeader, ref pmKind, ref dealer, ref dealerBranch);

                // insert if valid
                if (isValid)
                {
                    //if (dealerBranch != null)
                    //    newMSPClaim.DealerBranch = dealerBranch;

                    // insert a new mspclaim object
                    var success = (int)_mspclaimMapper.Insert(newMSPClaim, DNetUserName);
                    result.success = success > 0;
                    if (!result.success) ErrorMsgHelper.ErrorMsgDBSave(result.messages);
                    // return output ID
                    result._id = success;
                    result.total = 1;
                }
                else
                {
                    return PopulateValidationError<MSPClaimDto>(validationResults, null);
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
        /// Update MSPClaim
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<MSPClaimDto> Update(MSPClaimParameterDto objUpdate)
        {
            #region Declare
            var result = new ResponseBase<MSPClaimDto>();
            var validationResults = new List<DNetValidationResult>();
            ChassisMaster chassisMaster = null;
            Dealer dealer = null;
            DealerBranch dealerBranch = null;
            PMKind pmKind = null;
            PMHeader pmHeader = null;
            MSPClaim mspClaim = null;
            var isValid = true;
            #endregion

            try
            {
                // get the old mspClaim
                if (isValid) { isValid = ValidateExistingMSPClaim(objUpdate.ID, ref mspClaim, validationResults); }

                // validate mspclaim parameter values
                if (isValid) { isValid = ValidateMSP(objUpdate, validationResults, ref mspClaim, ref chassisMaster, ref pmHeader, ref pmKind, ref dealer, ref dealerBranch); }

                // insert if valid
                if (isValid)
                {
                    // update mspclaim object
                    mspClaim.LastUpdateTime = DateTime.Now;

                    // insert a new mspclaim object
                    var success = (int)_mspclaimMapper.Update(mspClaim, DNetUserName);
                    result.success = success > 0;
                    if (!result.success) ErrorMsgHelper.ErrorMsgDBSave(result.messages);
                    // return output ID
                    result._id = success;
                    result.total = 1;
                }
                else
                {
                    return PopulateValidationError<MSPClaimDto>(validationResults, null);
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
        /// Validate MSP Claim Create process
        /// </summary>
        /// <param name="param"></param>
        /// <param name="validationResults"></param>
        /// <param name="chassisMaster"></param>
        /// <param name="dealer"></param>
        /// <param name="dealerBranch"></param>
        /// <returns></returns>
        public bool ValidateMSP(MSPClaimParameterDto param, List<DNetValidationResult> validationResults, ref MSPClaim mspClaim, ref ChassisMaster chassisMaster, ref PMHeader pmHeader, ref PMKind pmKind, ref Dealer dealer, ref DealerBranch dealerBranch)
        {
            bool isValid = true;
            bool isLockBackwardAllowed = false;
            bool isBackwardPMExist = false;

            if (isValid) { isValid = ValidationHelper.ValidateServiceDate(param.ServiceDate, validationResults); }

            if (isValid) { isValid = ValidateVisitType(param, validationResults); }

            if (isValid) { isValid = IsPMKindFoundForMSP(param.StandKM, ref pmKind, validationResults); }

            if (isValid) { isValid = ValidateMSPStatus(param, pmHeader, pmKind, validationResults); }

            if (isValid) { isValid = ValidationHelper.ValidateChassisAndEngine(param.ChassisNumber, param.EngineNumber, validationResults, ref chassisMaster); }

            if (isValid) { isValid = ValidationHelper.ValidateDealer(chassisMaster.Dealer.DealerCode, validationResults, this.DealerCode, ref dealer, false); }

            if (isValid) { isValid = ValidationHelper.ValidateDealerBranch(this.DealerCode, validationResults, param.DealerBranchCode, ref dealerBranch); }

            if (isValid) { UpdateMSPClaim(param, ref mspClaim, chassisMaster, dealer, pmHeader, pmKind); }

            if (isValid) { isValid = IsExistCodeForInsert(chassisMaster, pmKind, param.StandKM, validationResults); }

            if (isValid) { isValid = ValidateLockBackward(validationResults, ref isLockBackwardAllowed); }

            if (isValid) { isValid = ValidateBackWardPMExist(chassisMaster.ID, pmKind, mspClaim.ID, validationResults, ref isBackwardPMExist); }

            if (isValid && isLockBackwardAllowed && isBackwardPMExist)
            {
                isValid = false;
            }

            return isValid;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Get the existing mspClaim
        /// </summary>
        /// <param name="mspClaimID"></param>
        /// <param name="mspClaim"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidateExistingMSPClaim(int mspClaimID, ref MSPClaim mspClaim, List<DNetValidationResult> validationResults)
        {
            if (mspClaimID == 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, "ID")));
            }
            else
            {
                // get by id
                var oldObject = _mspclaimMapper.Retrieve(mspClaimID);
                if (oldObject != null)
                {
                    mspClaim = oldObject as MSPClaim;
                }
                else
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, "MSPClaim", mspClaimID)));
                }
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Check the backward status from app config
        /// </summary>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidateLockBackward(List<DNetValidationResult> validationResults, ref bool _strAllowBackward)
        {
            var appconfig = _appConfigBL.GetConfigByName("MSPBackwardInput", string.Empty);

            if (appconfig == null || !bool.TryParse(appconfig.Value, out _strAllowBackward))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, "MSPBackwardInput")));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Check if backward PM exist
        /// </summary>
        /// <param name="chassisID"></param>
        /// <param name="obPMKIND"></param>
        /// <param name="validationResults"></param>
        /// <param name="mspClaimID"></param>
        /// <returns></returns>
        private bool ValidateBackWardPMExist(int chassisID, PMKind obPMKIND, int mspClaimID, List<DNetValidationResult> validationResults, ref bool isBackwardExist)
        {
            // Warning!!! Optional parameters not supported
            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(MSPClaim), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias.opAnd(new Criteria(typeof(MSPClaim), "ChassisMaster.ID", MatchType.Exact, chassisID));
            criterias.opAnd(new Criteria(typeof(MSPClaim), "PMKind.KM", MatchType.Greater, obPMKIND.KM));
            if (mspClaimID > 0)
            {
                criterias.opAnd(new Criteria(typeof(MSPClaim), "ID", MatchType.No, mspClaimID));
            }

            ArrayList mspClaim = _mspclaimMapper.RetrieveByCriteria(criterias);
            if (mspClaim.Count > 0)
            {
                MSPClaim obClaim = mspClaim[0] as MSPClaim;
                validationResults.Add(new DNetValidationResult(string.Format(ValidationResource.ChassisMasterIDAndPMKindIDExist, obClaim.ChassisMaster.ChassisNumber, obClaim.StandKM.ToString("#,##0"), obClaim.PMKind.KindCode)));
                isBackwardExist = true;
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Update MSP Claim
        /// </summary>
        /// <param name="param"></param>
        /// <param name="mspClaim"></param>
        /// <param name="chassisMaster"></param>
        /// <param name="pmHeader"></param>
        /// <param name="pmKind"></param>
        private void UpdateMSPClaim(MSPClaimParameterDto param, ref MSPClaim mspClaim, ChassisMaster chassisMaster, Dealer dealer, PMHeader pmHeader, PMKind pmKind)
        {
            if (mspClaim == null)
                mspClaim = _mapper.Map<MSPClaim>(param);

            mspClaim.Dealer = dealer;
            mspClaim.ChassisMaster = chassisMaster;
            mspClaim.StandKM = param.StandKM;
            mspClaim.ServiceDate = param.ServiceDate;
            mspClaim.Status = (short)EnumStatusMSP.Status.Baru;
            mspClaim.VisitType = param.VisitType;
            mspClaim.ClaimDate = DateTime.Now;
            mspClaim.Remarks = pmHeader != null ? pmHeader.Remarks : string.Empty;
            mspClaim.PMKind = pmKind;
            mspClaim.MSPRegistrationHistory = pmHeader == null ? null : _mspRegistrationHistoryMapper.Retrieve(pmHeader.MSPRegistrationHistoryID) as MSPRegistrationHistory;
        }

        /// <summary>
        /// Validate to prevent duplicate key
        /// </summary>
        /// <param name="ChassisID"></param>
        /// <param name="pmKindID"></param>
        /// <returns></returns>
        private bool IsExistCodeForInsert(ChassisMaster chassisMaster, PMKind pmKind, int standKM, List<DNetValidationResult> validationResults, int mspClaimID = 0)
        {
            // Periksa agar tidak ada key ganda 
            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(KTB.DNet.Domain.MSPClaim), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias.opAnd(new Criteria(typeof(KTB.DNet.Domain.MSPClaim), "ChassisMaster.ID", MatchType.Exact, chassisMaster.ID));
            criterias.opAnd(new Criteria(typeof(KTB.DNet.Domain.MSPClaim), "PMKind.ID", MatchType.Exact, pmKind.ID));
            if (_mspclaimMapper.RetrieveByCriteria(criterias).Count > 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(ValidationResource.ChassisMasterIDAndPMKindIDExist, chassisMaster.ChassisNumber, standKM, pmKind.KindCode)));
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate MSP Status
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="pmHeader"></param>
        /// <param name="pmKind"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidateMSPStatus(MSPClaimParameterDto objCreate, PMHeader pmHeader, PMKind pmKind, List<DNetValidationResult> validationResults)
        {
            // initiate
            pmHeader = new PMHeader();
            pmHeader.PMKind = pmKind;

            // check msp status
            string mspStatus = CheckMSPStatus(objCreate.ChassisNumber, pmKind.ID, ref pmHeader, objCreate.StandKM, objCreate.ServiceDate);
            if (string.IsNullOrEmpty(mspStatus))
            {
                if (!pmHeader.IsValidMSP)
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgPMIsNotMSP, pmHeader.ID)));
                }
            }
            else
            {
                validationResults.Add(new DNetValidationResult(mspStatus));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Check if it is for MSP
        /// </summary>
        /// <param name="standKM"></param>
        /// <returns></returns>
        public bool IsPMKindFoundForMSP(int standKM, ref PMKind pmKind, List<DNetValidationResult> validationResults)
        {
            // set the SP
            string spName = "up_RetrievePMKind_forMSP";

            // construct sql parameter
            System.Data.SqlClient.SqlParameter sqlParameter = new System.Data.SqlClient.SqlParameter();
            sqlParameter.ParameterName = "@KM";
            sqlParameter.Value = standKM;

            // add it to param list
            ArrayList param = new ArrayList();
            param.Add(sqlParameter);

            // search
            ArrayList pmKindColl = _pmKindMapper.RetrieveSP(spName, param);
            if (pmKindColl != null && pmKindColl.Count > 0)
            {
                pmKind = pmKindColl[0] as PMKind;
                return true;
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(ValidationResource.StandKMInvalid, standKM)));
                return false;
            }
        }

        /// <summary>
        /// Validate visit type
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        private bool ValidateVisitType(MSPClaimParameterDto objCreate, List<DNetValidationResult> validationResults)
        {
            var result = _enumCharBL.GetByCategoryAndCode("VisitType", objCreate.VisitType);
            if (result == null)
            {
                validationResults.Add(new DNetValidationResult(string.Format(ValidationResource.VisitTypeDoesNotExist, objCreate.VisitType)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Check MSP status
        /// </summary>
        /// <param name="chassisNumber"></param>
        /// <param name="pmKindID"></param>
        /// <param name="pmHeader"></param>
        /// <param name="inputKM"></param>
        /// <param name="serviceDate"></param>
        /// <returns></returns>
        private string CheckMSPStatus(string chassisNumber, int pmKindID, ref PMHeader pmHeader, int inputKM, DateTime serviceDate)
        {
            // untuk cek status MSP based on chassisnumber dan PMKindID yang terregister sebagai MSP
            string strMSPStatus = string.Empty;
            DataSet dtSet = _pmHeaderMapper.RetrieveDataSet("EXEC sp_MSP_GetMSPStatus " + chassisNumber + "," + pmKindID);
            if (dtSet.Tables.Count > 0)
            {
                DataTable dtTbl = dtSet.Tables[0];
                if (dtTbl.Rows.Count > 0)
                {
                    string id = (dtTbl.Rows[0]["MSPRegHistoryID"]).ToString();
                    MSPRegistrationHistory mspRegHistory = _mspRegistrationHistoryMapper.Retrieve(int.Parse(id)) as MSPRegistrationHistory;

                    if (mspRegHistory != null)
                    {

                        if (dtTbl.Rows[0]["MSPStatus"].ToString().Equals("Need Payment", StringComparison.OrdinalIgnoreCase))
                        {
                            PMKind objPMKIndVal = _pmKindMapper.Retrieve(pmKindID) as PMKind;

                            // validate msp duration pm kind
                            strMSPStatus = ValidateMSPDurationPMKind(chassisNumber, strMSPStatus, mspRegHistory, objPMKIndVal);
                        }
                        else
                        {
                            if (!dtTbl.Rows[0]["MSPStatus"].ToString().Equals("MSP EXPIRED", StringComparison.OrdinalIgnoreCase))
                            {
                                //  validasi jika masih ada upgrade registrasi dengan status belum selesai
                                if (mspRegHistory != null)
                                {
                                    foreach (MSPRegistrationHistory item in mspRegHistory.MSPRegistration.MSPRegistrationHistorys)
                                    {
                                        if (item.Status != (short)EnumStatusMSP.Status.Selesai && item.RequestType == ((int)EnumStatusMSP.StatusType.Upgrade).ToString())
                                        {
                                            strMSPStatus = string.Format(MessageResource.ErrorMsgMSPUpgradeNotCompleted, chassisNumber);
                                        }
                                    }

                                    if (string.IsNullOrEmpty(strMSPStatus))
                                    {
                                        //DateTime regDate = DateTime.Parse((string)dtTbl.Rows[0]["RegistrationDate"]);
                                        DateTime regDate = (DateTime)dtTbl.Rows[0]["RegistrationDate"];
                                        if (serviceDate <= regDate)
                                        {
                                            strMSPStatus = string.Format(MessageResource.ErrorMsgMSPServiceDateLessThanMSPRegistration, DateTime.Parse((string)dtTbl.Rows[0]["RegistrationDate"]).ToString("yyyy/MM/dd"));
                                        }
                                    }
                                }
                            }

                            if (string.IsNullOrEmpty(strMSPStatus) && !dtTbl.Rows[0]["MSPStatus"].ToString().Equals("PM", StringComparison.OrdinalIgnoreCase))
                            {
                                // update pm header
                                pmHeader = UpdatePMHeader(ref pmHeader, dtTbl);
                            }
                        }
                    }
                    else
                    {
                        strMSPStatus = "Tidak Terdaftar Sebagai MSP";
                    }
                }
                else
                {
                    strMSPStatus = "Tidak Terdaftar Sebagai MSP";
                }
            }
            else
            {
                strMSPStatus = "Tidak Terdaftar Sebagai MSP";
            }

            return strMSPStatus;
        }

        /// <summary>
        /// Update PM Header
        /// </summary>
        /// <param name="pmHeader"></param>
        /// <param name="dtTbl"></param>
        /// <returns></returns>
        private static PMHeader UpdatePMHeader(ref PMHeader pmHeader, DataTable dtTbl)
        {
            pmHeader = new PMHeader();
            pmHeader.Remarks = (string)dtTbl.Rows[0]["MSPStatus"];
            pmHeader.MSPRegistrationHistoryID = int.Parse(dtTbl.Rows[0]["MSPRegHistoryID"].ToString());

            if (dtTbl.Rows[0]["MSPStatus"].ToString().Equals("MSP EXPIRED", StringComparison.OrdinalIgnoreCase))
            {
                pmHeader.IsValidMSP = false;
            }

            return pmHeader;
        }

        /// <summary>
        /// Validate MSP duration pm kind
        /// </summary>
        /// <param name="chassisNumber"></param>
        /// <param name="strMSPStatus"></param>
        /// <param name="mspRegHistory"></param>
        /// <param name="pmKind"></param>
        /// <returns></returns>
        private string ValidateMSPDurationPMKind(string chassisNumber, string strMSPStatus, MSPRegistrationHistory mspRegHistory, PMKind pmKind)
        {
            CriteriaComposite crtMspPM = new CriteriaComposite(new Criteria(typeof(MSPDurationPMKind), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            crtMspPM.opAnd(new Criteria(typeof(MSPDurationPMKind), "Duration", MatchType.Exact, mspRegHistory.MSPMaster.Duration));
            crtMspPM.opAnd(new Criteria(typeof(MSPDurationPMKind), "PMKindCode", MatchType.Exact, pmKind.KindCode));
            ArrayList arrMspPm = _mspDurationPMKindMapper.RetrieveByCriteria(crtMspPM);
            if (arrMspPm.Count > 0)
            {
                strMSPStatus = string.Format(MessageResource.ErrorMsgMSPChassisNoPayment, chassisNumber);
            }
            else
            {
                strMSPStatus = "PM";
            }

            return strMSPStatus;
        }
        #endregion
    }
}

