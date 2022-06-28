#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PMHeader business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
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
using System.Data.SqlClient;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class PMHeaderBL : AbstractBusinessLogic, IPMHeaderBL
    {
        #region Variables
        private readonly IMapper _pMHeaderMapper;
        private readonly IMapper _pMKindMapper;
        private readonly AutoMapper.IMapper _mapper;
        private TransactionManager _transactionManager;
        private StandardCodeBL _enumBL;
        private readonly IMapper _mspmembershipMapper;
        #endregion

        #region Constructor
        public PMHeaderBL()
        {
            _pMHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(PMHeader).ToString());
            _pMKindMapper = MapperFactory.GetInstance().GetMapper(typeof(PMKind).ToString());
            _mspmembershipMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_MSPMembership).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _transactionManager = new TransactionManager();
            _enumBL = new StandardCodeBL(_mapper);
        }
        #endregion

        #region Public Methods
        public ResponseBase<PMHeaderDto> Create(PMHeaderParameterDto objCreate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<PMHeaderDto> Update(PMHeaderParameterDto objUpdate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<List<PMHeaderDto>> Read(PMHeaderFilterDto filterDto, int pageSize)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<PMHeaderDto> Delete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create WO
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ResponseBase<PMHeaderDto> CreateWO(PMHeaderCreateParameterDto param)
        {
            #region Declarations
            // set default response
            List<DNetValidationResult> validationResults = new List<DNetValidationResult>();
            var result = new ResponseBase<PMHeaderDto>();
            DealerBranch dealerBranch = null;
            ChassisMaster chassisMaster = null;
            bool isValid = true;
            PMKind pmKind = null;
            Dealer dealer = null;
            #endregion

            try
            {
                // validate parameters
                isValid = ValidatePM(param, validationResults, ref dealerBranch, ref chassisMaster, ref pmKind, ref dealer);

                // insert if valid
                if (isValid)
                {
                    #region Set value

                    var objCreate = new PMHeaderParameterDto
                    {
                        DealerID = dealer.ID,
                        ChassisNumberID = chassisMaster.ID,
                        PMKindID = pmKind.ID,
                        ServiceDate = param.ServiceDate,
                        PMStatus = _enumBL.GetByCategoryAndCode("EnumPMStatus.PMStatus", "Baru").ValueId.ToString(),
                        StandKM = param.StandKM,
                        EntryType = "Interface",
                        WorkOrderNumber = param.WorkOrderNumber,
                        BookingNo = param.BookingNo,
                        VisitType = param.VisitType,
                        ReleaseDate = new DateTime(1753, 1, 1),
                        CreatedBy = DNetUserName,
                        CreatedTime = DateTime.Now,
                        LastUpdateBy = DNetUserName,
                        LastUpdateTime = DateTime.Now,
                    };
                    if (dealerBranch != null)
                        objCreate.DealerBranchID = dealerBranch.ID;

                    #endregion

                    // update the remarks if this customer is registerd in MSP membership
                    //string remarks = UpdatePMRemarks(objCreate, param.ChassisNumber);

                    var domainData = _mapper.Map<PMHeader>(objCreate);
                    domainData.Dealer = dealer;
                    domainData.PMKind = pmKind;
                    domainData.ChassisMaster = chassisMaster;
                    domainData.DealerBranch = dealerBranch;
                    //domainData.Remarks = remarks;

                    int insertedID = _pMHeaderMapper.Insert(domainData, DNetUserName);
                    if (insertedID > 0)
                    {
                        result.success = true;
                        result._id = insertedID;
                        result.total = 1;
                        result.lst = null;
                    }
                    else
                    {
                        ErrorMsgHelper.ErrorMsgDBSaveContactAdmin(result.messages);
                    }
                }
                else
                {
                    return PopulateValidationError<PMHeaderDto>(validationResults, null);
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
        /// Validate PM paramater values
        /// </summary>
        /// <param name="param"></param>
        /// <param name="validationResults"></param>
        /// <param name="dealerBranch"></param>
        /// <param name="chassisMaster"></param>
        /// <param name="pmKind"></param>
        /// <param name="dealer"></param>
        /// <returns></returns>
        public bool ValidatePM(PMHeaderCreateParameterDto param, List<DNetValidationResult> validationResults, ref DealerBranch dealerBranch, ref ChassisMaster chassisMaster, ref PMKind pmKind, ref Dealer dealer)
        {
            bool isValid = true;

            if (isValid) { isValid = IsPMKindFound(param.StandKM, param.PMKindCode, validationResults, ref pmKind); }

            if(isValid) { isValid = IsKMLower(param.StandKM, param.ChassisNumber, validationResults); }

            if (isValid) { isValid = ValidationHelper.ValidateDealer(this.DealerCode, validationResults, this.DealerCode, ref dealer, false); }

            if (isValid) { isValid = ValidationHelper.ValidateDealerBranch(this.DealerCode, validationResults, param.DealerBranchCode, ref dealerBranch); }

            if (isValid) { isValid = ValidationHelper.ValidateChassisAndEngine(param.ChassisNumber, param.EngineNumber, validationResults, ref chassisMaster); }

            if (isValid) { isValid = IsExistCodeForInsert(chassisMaster, param.ServiceDate, validationResults); }

            if (isValid) { isValid = IsChassisAndKMEquals(pmKind, param.ChassisNumber, validationResults); }

            if (isValid) { isValid = IsChassisAndPMDateEquals(param.ChassisNumber, param.ServiceDate, validationResults); }

            return isValid;
        }

        public ResponseBase<PMHeaderDto> Delete(WorkOrderPMDeleteParameterDto paramDelete)
        {
            var result = new ResponseBase<PMHeaderDto>()
            {
                success = false
            };

            try
            {
                #region initialize
                var validationResults = new List<DNetValidationResult>();
                #endregion

                if (DealerCode != paramDelete.DealerCode)
                {
                    validationResults.Add(new DNetValidationResult("Dealer Login dengan dengan Dealer yang dikirimkan tidak sesuai."));
                }
                else
                {
                    var workOrderPMCriteria = new CriteriaComposite(new Criteria(typeof(PMHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    //check if WO Number exist
                    workOrderPMCriteria.opAnd(new Criteria(typeof(PMHeader), "WorkOrderNumber", MatchType.Exact, paramDelete.WorkOrderNumber));
                    //check Dealer
                    workOrderPMCriteria.opAnd(new Criteria(typeof(PMHeader), "Dealer.DealerCode", MatchType.Exact, DealerCode));
                    //check Dealer Branch if Exist
                    if (paramDelete.DealerBranchCode.Trim() != string.Empty)
                    {
                        workOrderPMCriteria.opAnd(new Criteria(typeof(PMHeader), "DealerBranch.DealerBranchCode", MatchType.Exact, paramDelete.DealerBranchCode));
                    }
                    //check Chassis
                    workOrderPMCriteria.opAnd(new Criteria(typeof(PMHeader), "ChassisMaster.ChassisNumber", MatchType.Exact, paramDelete.ChassisNumber));

                    var pmHeaders = _pMHeaderMapper.RetrieveByCriteria(workOrderPMCriteria);
                    if (pmHeaders.Count == 0)
                    {
                        validationResults.Add(new DNetValidationResult(string.Format("Work Order Number {0} tidak terdaftar untuk Dealer {1}", paramDelete.WorkOrderNumber, DealerCode)));
                    }
                    else
                    {
                        var pmHeader = pmHeaders.Cast<PMHeader>().ToList()[0];
                        // check row status
                        if (pmHeader.RowStatus != 0)
                        {
                            validationResults.Add(new DNetValidationResult(string.Format("Work Order Number {0} tidak aktif, tidak dapat membatalkan PM dengan Work Order Number yang tidak aktif", paramDelete.WorkOrderNumber)));
                        }
                        // check if status == baru
                        else if (pmHeader.PMStatus != _enumBL.GetByCategoryAndCode("EnumPMStatus.PMStatus", "Baru").ValueId.ToString())
                        {
                            validationResults.Add(new DNetValidationResult(string.Format("Work Order Number {0} sedang diproses dan tidak dapat dibatalkan", paramDelete.WorkOrderNumber)));
                        }
                        // delete
                        else
                        {

                            pmHeader.RowStatus = -1;
                            var success = (int)_pMHeaderMapper.Update(pmHeader, DNetUserName);
                            result.success = success > 0;
                            if (!result.success)
                            {
                                ErrorMsgHelper.UpdateNotAvailable(result.messages);
                            }
                            // return output ID
                            result._id = pmHeader.ID;
                            result.total = 1;
                        }

                    }
                }
                if (validationResults.Count > 0)
                {
                    return PopulateValidationError<PMHeaderDto>(validationResults, null);
                }
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }
        #endregion

        #region Validation
        /// <summary>
        /// Validate PM Kind
        /// </summary>
        /// <param name="param"></param>
        /// <param name="validationResults"></param>
        /// <param name="pmKind"></param>
        /// <returns></returns>
        private bool ValidatePMKind(PMHeaderCreateParameterDto param, List<DNetValidationResult> validationResults, ref PMKind pmKind)
        {
            // get dealer by code
            ArrayList pmKinds = _pMKindMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(PMKind), "KindCode", param.PMKindCode));
            if (pmKinds.Count > 0)
            {
                pmKind = pmKinds[0] as PMKind;
                return true;
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataCodeNotFound, "PMKind", param.PMKindCode)));
                return false;
            }
        }

        /// <summary>
        /// Validate PM Status
        /// </summary>
        /// <param name="param"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidatePMStatus(PMHeaderCreateParameterDto param, List<DNetValidationResult> validationResults)
        {
            var pmStatus = _enumBL.GetByCategoryAndValue("EnumPMStatus.PMStatus", param.PMStatus);
            if (pmStatus == null)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgPMStatusNotFound, param.PMStatus)));
            }
            else if (!pmStatus.ValueCode.Equals("Baru", StringComparison.OrdinalIgnoreCase))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgPMStatusShouldNew)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Update remarks
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="chassisNumber"></param>
        /// <returns></returns>
        private string UpdatePMRemarks(PMHeaderParameterDto objCreate, string chassisNumber)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_MSPMembership), "ChassisNumber", MatchType.Exact, chassisNumber));
            ArrayList memberships = _mspmembershipMapper.RetrieveByCriteria(criterias);
            if (memberships.Count > 0)
            {
                VWI_MSPMembership member = memberships[0] as VWI_MSPMembership;

                var criterias1 = new CriteriaComposite(new Criteria(typeof(ChassisMasterPKT), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias1.opAnd(new Criteria(typeof(ChassisMasterPKT), "ChassisMaster.ID", MatchType.Exact, objCreate.ChassisNumberID));
                var chassisPKTMapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMasterPKT).ToString());
                ArrayList chassisPKTs = chassisPKTMapper.RetrieveByCriteria(criterias1);
                if (chassisPKTs.Count > 0)
                {
                    ChassisMasterPKT chassisPKT = chassisPKTs[0] as ChassisMasterPKT;
                    TimeSpan TS = objCreate.ServiceDate - chassisPKT.PKTDate;
                    double Years = TS.TotalDays / 365.25;
                    DateTime validUntil = chassisPKT.PKTDate.AddYears((int)member.Duration);
                    string remarks = string.Format(ValidationResource.PMHeaderRemarksFormat, member.Description, Math.Ceiling(Years), validUntil.ToString("dd/MM/yyyy"));
                    return remarks;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// validate if chassis no and km lower than last service
        /// </summary>
        /// <param name="standKM"></param>
        /// <param name="chassisNumber"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool IsKMLower(int standKM, string chassisNumber, List<DNetValidationResult> validationResults)
        {
            bool bResult = true;

            var criterias = new CriteriaComposite(new Criteria(typeof(PMHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(PMHeader), "ChassisMaster.ChassisNumber", MatchType.Exact, chassisNumber));

            var sortColl = new SortCollection();
            sortColl.Add(new Sort(typeof(PMHeader), "StandKM", Sort.SortDirection.DESC));

            var pmHeaderColl = _pMHeaderMapper.RetrieveByCriteria(criterias, sortColl);
            if (pmHeaderColl.Count > 0)
            {
                var pmHeader = pmHeaderColl[0] as PMHeader;
                if(pmHeader.StandKM > standKM)
                {
                    validationResults.Add(new DNetValidationResult("StandKM tidak boleh kurang dari " + pmHeader.StandKM.ToString()));
                    bResult = false;
                }
            }
           
            return bResult;
        }

            /// <summary>
            /// Validate if pm kind found
            /// </summary>
            /// <param name="pmKindCode"></param>
            /// <returns></returns>
            private bool IsPMKindFound(int standKM, string pmKindCode, List<DNetValidationResult> validationResults, ref PMKind kind)
        {
            bool bResult = false;

            if (string.IsNullOrEmpty(pmKindCode))
            {
                validationResults.Add(new DNetValidationResult("PMKindCode is mandatory."));
                return bResult;
            }
            else
            {
                var crit = new CriteriaComposite(new Criteria(typeof(PMKind), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                crit.opAnd(new Criteria(typeof(PMKind), "KindCode", MatchType.Exact, pmKindCode));

                var sort = new SortCollection();
                sort.Add(new Sort(typeof(PMKind), "KM", Sort.SortDirection.ASC));
                var resKind = _pMKindMapper.RetrieveByCriteria(crit, sort);
                if(resKind.Count == 0)
                {
                    validationResults.Add(new DNetValidationResult("PMKindCode " + pmKindCode + " tidak terdaftar di DNet"));
                    return bResult;
                }
                else
                {
                    kind = resKind[0] as PMKind;
                    if (standKM > kind.KM)
                    {
                        validationResults.Add(new DNetValidationResult("StandKM tidak boleh lebih dari " + kind.KM.ToString()));
                        return bResult;
                    }
                    else
                    {
                        bResult = true;
                    }
                }
            }
           
            return bResult;
        }

        /// <summary>
        /// Validate if exist
        /// </summary>
        /// <param name="chassisID"></param>
        /// <param name="serviceDate"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool IsExistCodeForInsert(ChassisMaster chassisMaster, DateTime serviceDate, List<DNetValidationResult> validationResults)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(PMHeader), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias.opAnd(new Criteria(typeof(PMHeader), "ChassisMaster.ID", MatchType.Exact, chassisMaster.ID));
            criterias.opAnd(new Criteria(typeof(PMHeader), "ServiceDate", MatchType.LesserOrEqual, new DateTime(serviceDate.Year, serviceDate.Month, serviceDate.Day, 23, 59, 59)));
            criterias.opAnd(new Criteria(typeof(PMHeader), "ServiceDate", MatchType.GreaterOrEqual, new DateTime(serviceDate.Year, serviceDate.Month, serviceDate.Day, 0, 0, 0)));
            ArrayList isExist = _pMHeaderMapper.RetrieveByCriteria(criterias);
            if (isExist.Count > 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgChassisNumberHasAlreadyExistOnThatDate, chassisMaster.ChassisNumber, serviceDate.ToShortDateString())));
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate if chassis and its combinations exist
        /// </summary>
        /// <param name="chassisMaster"></param>
        /// <param name="pmKindCode"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidateChassisIDAndPMKindCodeCombinationDuplicate(ChassisMaster chassisMaster, string pmKindCode, List<DNetValidationResult> validationResults)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(PMHeader), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias.opAnd(new Criteria(typeof(PMHeader), "ChassisMaster.ID", MatchType.Exact, chassisMaster.ID));
            criterias.opAnd(new Criteria(typeof(PMHeader), "PMKind.KindCode", MatchType.Exact, pmKindCode));
            ArrayList pms = _pMHeaderMapper.RetrieveByCriteria(criterias);
            if (pms.Count > 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgChassisNumberWithPMKindCodeExist, chassisMaster.ChassisNumber, pmKindCode)));
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate chassis and pm date
        /// </summary>
        /// <param name="chassisNumber"></param>
        /// <param name="serviceDate"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool IsChassisAndPMDateEquals(string chassisNumber, DateTime serviceDate, List<DNetValidationResult> validationResults)
        {
            // ---cek chasiss dgn tanggal PM
            var criterias = new CriteriaComposite(new Criteria(typeof(KTB.DNet.Domain.PMHeader), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias.opAnd(new Criteria(typeof(KTB.DNet.Domain.PMHeader), "ChassisMaster.ChassisNumber", MatchType.Exact, chassisNumber));
            criterias.opAnd(new Criteria(typeof(KTB.DNet.Domain.PMHeader), "ServiceDate", MatchType.Exact, serviceDate));
            ArrayList arlCek = _pMHeaderMapper.RetrieveByCriteria(criterias);
            if (arlCek.Count > 0)
            {
                validationResults.Add(new DNetValidationResult(MessageResource.ErrorMsgChassisNumberAndPMDateHasAlreadyExist));
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check if chassis and KM already exist
        /// </summary>
        /// <param name="pmKindCode"></param>
        /// <param name="chassisNumber"></param>
        /// <param name="parID"></param>
        /// <returns></returns>
        private bool IsChassisAndKMEquals(PMKind pmKind, string chassisNumber, List<DNetValidationResult> validationResults, int parID = 0)
        {
            // ---cek Chassis yang sama dengan KM yang sama Warning!!! Optional parameters not supported
            string StrFunction = string.Format("(SELECT ID FROM dbo.fn_PmHeaderChecking(\'{0}\',\'{1}\',{2}))", chassisNumber.Trim(), pmKind.KindCode, parID.ToString());
            CriteriaComposite checkRuleChassis_No = new CriteriaComposite(new Criteria(typeof(KTB.DNet.Domain.PMHeader), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            checkRuleChassis_No.opAnd(new Criteria(typeof(KTB.DNet.Domain.PMHeader), "ChassisMaster.ChassisNumber", MatchType.Exact, chassisNumber));
            checkRuleChassis_No.opAnd(new Criteria(typeof(KTB.DNet.Domain.PMHeader), "ID", MatchType.InSet, StrFunction));
            ArrayList arlRuleChassis = _pMHeaderMapper.RetrieveByCriteria(checkRuleChassis_No);
            if (arlRuleChassis.Count > 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgChassisNumberKMEquals, chassisNumber, pmKind.KindCode + "("+ pmKind.KindDescription + ")" )));
                return false;
            }

            return true;
        }
        #endregion
    }
}
