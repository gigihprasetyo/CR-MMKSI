#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPK business logic class
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
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using DNetDomain = KTB.DNet.Domain;
using InterfaceFramework = KTB.DNet.Interface.Framework;
using SPKCustomerProfile = KTB.DNet.Domain.SPKCustomerProfile;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class SPKBL : AbstractBusinessLogic, ISPKBL
    {
        #region Variables
        private readonly IMapper _spkHeaderMapper;
        private readonly IMapper _spkDetailMapper;
        private readonly IMapper _spkFakturMapper;
        private readonly IMapper _revisionSpkFakturMapper;
        private readonly IMapper _vehicleTypeMapper;
        private readonly IMapper _categoryMapper;
        private readonly IMapper _sp_GetSPKStatusMapper;
        private readonly IMapper _sapCustomerMapper;
        private AutoMapper.IMapper _mapper;
        private TransactionManager _transactionManager;
        private StandardCodeBL _enumBL;
        private readonly IMapper _statusChangeHistoryMapper;
        private readonly IMapper _sPKCustomerHaveRequestMapper;
        private readonly IMapper _spkFakturRevisionMapper;
        private readonly IMapper _spkChassisMapper;
        private readonly IMapper _vWISPKTrackingMapper;
        private readonly IMapper _lkppheaderMapper;
        private readonly IMapper _lkppdetailMapper;
        #endregion

        #region Constructor
        public SPKBL()
        {
            _spkHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKHeader).ToString());
            _spkDetailMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKDetail).ToString());
            _spkFakturMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKFaktur).ToString());
            _revisionSpkFakturMapper = MapperFactory.GetInstance().GetMapper(typeof(RevisionSPKFaktur).ToString());
            _vehicleTypeMapper = MapperFactory.GetInstance().GetMapper(typeof(VechileType).ToString());
            _categoryMapper = MapperFactory.GetInstance().GetMapper(typeof(Category).ToString());
            _sp_GetSPKStatusMapper = MapperFactory.GetInstance().GetMapper(typeof(sp_GetSPKStatus).ToString());
            _sapCustomerMapper = MapperFactory.GetInstance().GetMapper(typeof(SAPCustomer).ToString());
            _statusChangeHistoryMapper = MapperFactory.GetInstance().GetMapper(typeof(StatusChangeHistory).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _transactionManager = new TransactionManager();
            _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);
            _enumBL = new StandardCodeBL(_mapper);
            _sPKCustomerHaveRequestMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_SPKCustomerHaveRequest).ToString());
            _spkFakturRevisionMapper = MapperFactory.GetInstance().GetMapper(typeof(RevisionSPKFaktur).ToString());
            _spkChassisMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKChassis).ToString());
            _vWISPKTrackingMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_SPKTracking).ToString());
            _lkppheaderMapper = MapperFactory.GetInstance().GetMapper(typeof(LKPPHeader).ToString());
            _lkppdetailMapper = MapperFactory.GetInstance().GetMapper(typeof(LKPPDetail).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get filtered data
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<SPKHeaderDto>> Read(SPKHeaderFilterDto filterDto, int pageSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create spk
        /// </summary>
        /// <param name="spk"></param>
        /// <returns></returns>
        public ResponseBase<SPKHeaderDto> Create(SPKHeaderParameterDto spk)
        {
            #region Initialize
            // set default response
            var result = new ResponseBase<SPKHeaderDto>();
            string filePath = string.Empty;
            List<KTB.DNet.Domain.SPKCustomerProfile> listOfSPKCustomerProfile = null;
            List<DNetValidationResult> validationResults = new List<DNetValidationResult>();
            List<SPKDetail> listOfDomainSPKDetail = new List<SPKDetail>();
            bool allowToUpdateDataCustomer = true;
            OCRIdentity oCRIdentity = null;
            List<MessageBase> getFakturbySpkDetailList = new List<MessageBase>();
            List<OCRFamilyCard> ocrKKIdentity = null;
            string msgSPKDetailCantUpdate = string.Empty;
            #endregion

            try
            {
                // define id create
                spk.ID = 0;

                // validate spk domain
                SPKHeader domainSPK = GetValidSPKDomainObject(spk, out listOfDomainSPKDetail, validationResults, out oCRIdentity, out ocrKKIdentity, out filePath, out allowToUpdateDataCustomer, out listOfSPKCustomerProfile, out getFakturbySpkDetailList, out msgSPKDetailCantUpdate);

                // validate SPK reference number if provided
                ValidateSPKReferenceNumber(spk, validationResults);

                //validate LKPP
                ValidateLKPP(spk, validationResults);

                // if any errors
                if (validationResults.Any())
                {
                    return PopulateValidationError<SPKHeaderDto>(validationResults, null);
                }

                // generate validation key [not implemented in existing ktb]
                domainSPK.ValidationKey = new RandomGenerator().GetActivationCode(8);

                // set status = 'Awal' as default on create spk
                domainSPK.Status = _enumBL.GetByCategoryAndCode("EnumStatusSPK.Status", "Awal").ValueId.ToString();

                #region Insert via Trans Manager
                int insertedID = InsertWithTransactionManager(domainSPK, listOfDomainSPKDetail, listOfSPKCustomerProfile, oCRIdentity, ocrKKIdentity);
                if (insertedID > 0)
                {
                    // get the spk number from the new spk
                    SPKHeader spkOnDB = (SPKHeader)_spkHeaderMapper.Retrieve(domainSPK.ID);
                    domainSPK.SPKNumber = spkOnDB.SPKNumber;
                    domainSPK.CreatedBy = spkOnDB.CreatedBy;

                    // set result
                    result.success = true;
                    result._id = insertedID;
                    result.total = 1;
                    result.lst = _mapper.Map<SPKHeaderDto>(spkOnDB);

                    // it will be handle by dnet team
                    //RecordStatusChangeHistory(domainSPK, (int)EnumStatusSPK.Status.Awal, (int)EnumStatusSPK.Status.Awal);

                    #region Not implemented on existing KTB
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        string path = FileUtility.MoveSPKEvidenceFile(domainSPK, filePath, GetEvidenceFileName(domainSPK, spk.EvidenceFile));
                        if (!string.IsNullOrEmpty(path))
                        {
                            domainSPK.EvidenceFile = path;

                            _spkHeaderMapper.Update(domainSPK, DNetUserName);
                        }
                    }

                    // update SAPCustomer Status --> DEAL/SPK
                    if (domainSPK.SPKCustomer.SAPCustomer != null)
                    {
                        domainSPK.SPKCustomer.SAPCustomer.Status = (byte)_enumBL.GetByCategoryAndCode("EnumSAPCustomerStatus.SAPCustomerStatus", "Deal_SPK").ValueId;

                        _sapCustomerMapper.Update(domainSPK.SPKCustomer.SAPCustomer, DNetUserName);
                    }
                    #endregion
                }
                else
                {
                    DeleteEvidenceFile(filePath, string.Empty);

                    result.messages.Add(new MessageBase { ErrorCode = ErrorCode.UnhandledException, ErrorMessage = MessageResource.ErrorMsgOnSavingPleaseContactAdmin });
                }
                #endregion
            }
            catch (Exception ex)
            {
                DeleteEvidenceFile(filePath, string.Empty);

                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Update spk
        /// </summary>
        /// <param name="spk"></param>
        /// <returns></returns>
        public ResponseBase<SPKHeaderDto> Update(SPKHeaderParameterDto spk)
        {
            #region Declare
            var result = new ResponseBase<SPKHeaderDto>();
            List<DNetValidationResult> validationResults = new List<DNetValidationResult>();
            List<SPKDetail> listOfDomainSPKDetail = new List<SPKDetail>();
            List<KTB.DNet.Domain.SPKCustomerProfile> listOfSPKCustomerProfile = null;
            OCRIdentity oCRIdentity = null;
            string filePath = string.Empty;
            string customerIdentityFilePath = string.Empty;
            bool allowToUpdateDataCustomer = true;
            int iQtyH = 0;
            int iQtyD = 0;
            int iQtyBatal = 0;
            int iQtyFinish = 0;
            int iAllQtySPKDetails = 0;
            List<MessageBase> getFakturbySpkDetailList = new List<MessageBase>();
            List<OCRFamilyCard> ocrKKIdentity = null;
            string msgSPKDetailCantUpdate = string.Empty;
            #endregion

            try
            {
                SPKHeader domainSPK = null;
                // check if the data is available on db
                SPKHeader spkOnDB = (SPKHeader)_spkHeaderMapper.Retrieve(spk.ID);

                if (spkOnDB == null)
                {
                    validationResults.Add(new DNetValidationResult(MessageResource.ErrorMsgDataUpdateNotAvailable));
                    return PopulateValidationError<SPKHeaderDto>(validationResults, null);
                }

                // check if there is a changes in spk customer id
                if (spkOnDB.SPKCustomer.ID != spk.SPKCustomer.ID)
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.SPKCustomerIDOnSPK)));
                    return PopulateValidationError<SPKHeaderDto>(validationResults, null);
                }

                // get spk domain
                domainSPK = GetValidSPKDomainObject(spk, out listOfDomainSPKDetail, validationResults, out oCRIdentity, out ocrKKIdentity, out filePath, out allowToUpdateDataCustomer, out listOfSPKCustomerProfile, out getFakturbySpkDetailList,out msgSPKDetailCantUpdate, spkOnDB);

                // validate SPK reference number if provided
                ValidateSPKReferenceNumber(spk, validationResults);

                //validate LKPP
                ValidateLKPP(spk, validationResults);

                // if any errors
                if (validationResults.Any()) 
                {
                    string MessageStatusCompleted = MessageResource.ErrorMsgCouldNotUpdateSPKWithCancelOrFinishStatus.Replace("/batal", "");
                    bool isCustomReturn = false;
                    foreach (DNetValidationResult item in validationResults)
                    {
                        if (item.ErrorMessage == MessageStatusCompleted)
                        {
                            isCustomReturn = true;
                            break;
                        }
                    }

                    if (isCustomReturn)
                    {
                        result.success = true;
                        result._id = spkOnDB.ID;
                        result.total = 1;
                        result.lst = _mapper.Map<SPKHeaderDto>(spkOnDB);
                        result.messages.Add(new MessageBase { ErrorCode = ErrorCode.Er_OK, ErrorMessage = MessageStatusCompleted });

                        return result;
                    }
                    else
                    {
                        return PopulateValidationError<SPKHeaderDto>(validationResults, null);
                    }
                }

                // update using the previous data 
                //domainSPK.CustomerRequestID = spkOnDB.CustomerRequestID;
                //domainSPK.SPKNumber = spkOnDB.SPKNumber;
                //domainSPK.ValidationKey = spkOnDB.ValidationKey;
                //domainSPK.CreatedBy = spkOnDB.CreatedBy;
                //domainSPK.CreatedTime = spkOnDB.CreatedTime;

                #region Not implemented on existing KTB
                // get list of faktur
                ArrayList listOfFaktur = GetSPKFaktur(spk);
                foreach (SPKDetail detail in listOfDomainSPKDetail)
                {
                    if (detail.Status == 1)
                    {
                        iQtyBatal = (iQtyBatal + 1);
                    }
                    else
                    {
                        iQtyH = detail.Quantity;
                        if (detail.VechileColor != null && detail.VechileColor.VechileType != null)
                        {
                            foreach (SPKFaktur faktur in listOfFaktur)
                            {
                                if (faktur.EndCustomer != null && faktur.EndCustomer.ChassisMaster != null && faktur.EndCustomer.ChassisMaster.VechileColor != null)
                                {
                                    if (faktur.EndCustomer.ChassisMaster.VechileColor.VechileType.ID == detail.VechileColor.VechileType.ID)
                                    {
                                        if (faktur.EndCustomer.ChassisMaster.FakturStatus != "0")
                                        {
                                            iQtyD = iQtyD + 1;
                                        }
                                    }
                                }
                            }
                        }

                        if (iQtyH == iQtyD)
                        {
                            iQtyFinish = iQtyFinish + 1;
                        }
                    }
                }

                bool allowToUpdateSPKStatus = true;
                // validate if any
                if (listOfDomainSPKDetail.Count > 0)
                {
                    //bool isStatusSPKSelesai;                
                    List<StandardCodeDto> enumsStatusSPK = _enumBL.GetByCategory("EnumStatusSPK.Status");

                    iAllQtySPKDetails = iQtyBatal + iQtyFinish;
                    if (iAllQtySPKDetails == listOfDomainSPKDetail.Count() && iQtyFinish > 0)
                    {
                        domainSPK.Status = enumsStatusSPK.Where(s => s.ValueCode == "Selesai").SingleOrDefault().ValueId.ToString();

                        // it will be handle by dnet team
                        int oldStatus = enumsStatusSPK.Where(s => s.ValueId == Convert.ToInt32(domainSPK.Status)).SingleOrDefault().ValueId;
                        RecordStatusChangeHistory(domainSPK, oldStatus, (int)EnumStatusSPK.Status.Selesai);
                        // isStatusSPKSelesai = true;
                        allowToUpdateSPKStatus = false;
                    }
                    else if (iQtyBatal == listOfDomainSPKDetail.Count())
                    {
                        //domainSPK.Status = enumsStatusSPK.Where(s => s.ValueCode == "Batal").SingleOrDefault().ValueId.ToString();

                        // it will be handle by dnet team
                        int oldStatus = enumsStatusSPK.Where(s => s.ValueId == Convert.ToInt32(domainSPK.Status)).SingleOrDefault().ValueId;
                        RecordStatusChangeHistory(domainSPK, oldStatus, (int)EnumStatusSPK.Status.Batal);
                        allowToUpdateSPKStatus = false;
                    }
                }
                #endregion

                bool doUpdateSPKStatus = false;

                #region On Update SPK Status
                if (allowToUpdateSPKStatus)
                {
                    // validate if any changes on SPK Header status
                    if (spkOnDB.Status.Trim() != spk.Status.ToString())
                    {

                        // validate spk status
                        if (ValidateSPKStatus(ref spkOnDB, spk, listOfDomainSPKDetail, validationResults))
                        {

                            foreach (SPKDetail spkDetail in listOfDomainSPKDetail)
                            {
                                if (spkDetail.Status != 1 && spkDetail.Status != 3)
                                {
                                    spkDetail.Status = (byte)spk.Status;
                                    spkDetail.RejectedReason = domainSPK.RejectedReason;
                                }
                            }

                            domainSPK.FlagUpdate = 0;
                            doUpdateSPKStatus = true;
                        }
                        else
                        {
                            return PopulateValidationError<SPKHeaderDto>(validationResults, null);
                        }
                    }
                }
                #endregion

                //Check Count getFakturSPKDetailList
                bool allowtoUpdateDataDetail = true;
                if (getFakturbySpkDetailList.Count > 0) { allowToUpdateDataCustomer = false; allowtoUpdateDataDetail = false; }


                int updateResult = UpdateWithTransactionManager(domainSPK, listOfDomainSPKDetail, allowToUpdateDataCustomer, allowtoUpdateDataDetail, listOfSPKCustomerProfile, oCRIdentity, getFakturbySpkDetailList, ocrKKIdentity);
                if (updateResult > 0)
                {
                    // not implemented on existing KTB
                    UpdateSPKEvidencePath(spk, filePath, domainSPK);
                    // it will be handle by dnet team
                    if (doUpdateSPKStatus)
                    {
                        RecordStatusChangeHistory(spkOnDB, int.Parse(spkOnDB.Status), spk.Status);
                        result.messages.Add(new MessageBase { ErrorCode = ErrorCode.Er_OK, ErrorMessage = MessageResource.ErrorMsgSPKStatusSucceed });
                    }

                    domainSPK.SPKDetails.Clear();

                    result.success = true;
                    result._id = domainSPK.ID;
                    result.total = 1;
                    result.lst = _mapper.Map<SPKHeaderDto>(domainSPK);

                    if (getFakturbySpkDetailList.Count == 0 && allowtoUpdateDataDetail)
                    {
                        result.messages.Add(new MessageBase { ErrorCode = ErrorCode.Er_OK, ErrorMessage = MessageResource.ErrorMsgSPKEditSucceed });
                    }
                    else if (getFakturbySpkDetailList.Count > 0 && !allowtoUpdateDataDetail)
                    {
                        result.messages.AddRange(getFakturbySpkDetailList);
                    }

                    if (!string.IsNullOrEmpty(msgSPKDetailCantUpdate))
                    {
                        result.messages.Add(new MessageBase { ErrorCode = ErrorCode.Er_OK, ErrorMessage = msgSPKDetailCantUpdate });
                    }
                }
                else
                {
                    ErrorMsgHelper.ErrorMsgDBSave(result.messages, FieldResource.SPK);
                }

            }
            catch (Exception ex)
            {
                DeleteEvidenceFile(filePath, customerIdentityFilePath);

                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Delete spk by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<SPKHeaderDto> Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Get SPK Document
        /// </summary>
        /// <param name="spkDocumentParam"></param>        
        /// <returns></returns>
        public ResponseBase<SPKDocumentDto> GetSPKDocument(SPKDocumentParameterDto spkDocumentParam)
        {
            ResponseBase<SPKDocumentDto> result = new ResponseBase<SPKDocumentDto>();
            List<DNetValidationResult> validationResults = new List<DNetValidationResult>();

            // set the credentials to access the repository server            
            UserImpersonater imp = GetImpersonater();

            try
            {
                SPKHeader oldSPK = null;
                if (!ValidationHelper.ValidateSPKHeader(spkDocumentParam.SPKNumber, validationResults, ref oldSPK))
                {
                    return PopulateValidationError<SPKDocumentDto>(validationResults, null);
                }

                // check if dealer code not match with user login dealer code
                if (this.DealerCode != oldSPK.Dealer.DealerCode)
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgInvalidSPKDealerCode, oldSPK.Dealer.DealerCode)) { });
                    return PopulateValidationError<SPKDocumentDto>(validationResults, null);
                }

                bool success = imp.Start();
                if (success)
                {
                    // get the file path
                    string filePath = Path.Combine(AppConfigs.GetString("SAN"), oldSPK.EvidenceFile);
                    if (!File.Exists(filePath))
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataNotFound, FieldResource.SPKEvidence)) { });
                        return PopulateValidationError<SPKDocumentDto>(validationResults, null);
                    }

                    // read file all bytes
                    byte[] bytes = File.ReadAllBytes(filePath);

                    // convert to base64string
                    string base64String = Convert.ToBase64String(bytes);

                    imp.Stop();

                    string[] filePaths = filePath.Split(Path.DirectorySeparatorChar);
                    string filename = filePaths[filePaths.Length - 1];

                    result._id = oldSPK.ID;
                    result.total = 1;
                    result.success = true;
                    result.lst = new SPKDocumentDto() { SPKNumber = oldSPK.SPKNumber, DealerCode = oldSPK.Dealer.DealerCode, Document = new AttachmentDto() { Base64OfStream = base64String, FileName = filename } };
                }
                else
                {
                    validationResults.Add(new DNetValidationResult(ErrorCode.UnhandledException, MessageResource.ErrorMsgFailedToAccessFile) { });
                    return PopulateValidationError<SPKDocumentDto>(validationResults, null);
                }
            }
            catch
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.UnhandledException, string.Format(MessageResource.ErrorMsgPRGUnhandle, string.Empty)) { });
                return PopulateValidationError<SPKDocumentDto>(validationResults, null);
            }
            finally
            {
                imp.Dispose();
            }

            return result;
        }

        /// <summary>
        /// Get SPK CUstomer KTP
        /// </summary>
        /// <param name="spkDocumentParam"></param>
        /// <returns></returns>
        public ResponseBase<SPKDocumentDto> GetSPKCustomerKTP(SPKDocumentParameterDto spkDocumentParam)
        {
            // default filter to get the Active Row Status only
            var validationResults = new List<DNetValidationResult>();
            var result = new ResponseBase<SPKDocumentDto>();

            // set the credentials to access the repository server
            UserImpersonater imp = GetImpersonater();

            try
            {
                // default filter to get the Active Row Status only
                var data = GetSPKHeaders(spkDocumentParam);
                if (data.Count > 0)
                {
                    var list = data.Cast<SPKHeader>().ToList();
                    var spkHeader = list.FirstOrDefault();

                    // validate KTP path exist
                    ValidateKTP(validationResults, list);

                    // validate Dealer
                    if (DealerCode != spkHeader.Dealer.DealerCode)
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.SPKNumberDealerCode)));
                    }

                    if (validationResults.Count > 0)
                    {
                        return PopulateValidationError<SPKDocumentDto>(validationResults, null);
                    }

                    var success = imp.Start();
                    if (success)
                    {
                        // validate filepath
                        var filePath = ValidateFilePath(validationResults, spkHeader);
                        if (validationResults.Count == 0)
                        {
                            // read file all bytes
                            byte[] bytes = File.ReadAllBytes(filePath);

                            // convert to base64string
                            string base64String = Convert.ToBase64String(bytes);

                            imp.Stop();

                            string[] filePaths = filePath.Split(Path.DirectorySeparatorChar);
                            string filename = filePaths[filePaths.Length - 1];

                            result.lst = new SPKDocumentDto() { SPKNumber = spkHeader.SPKNumber, DealerCode = spkHeader.Dealer.DealerCode, Document = new AttachmentDto() { Base64OfStream = base64String, FileName = filename } };
                            result._id = spkHeader.ID;
                            result.total = 1;
                            result.success = true;
                        }
                        else
                        {
                            return PopulateValidationError<SPKDocumentDto>(validationResults, null);
                        }
                    }
                    else
                    {
                        validationResults.Add(new DNetValidationResult(ErrorCode.UnhandledException, MessageResource.ErrorMsgFailedToAccessFile));
                        return PopulateValidationError<SPKDocumentDto>(validationResults, null);
                    }
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SPKHeader), null);

                    result.success = true;

                }
            }
            catch (SqlException ex)
            {
                ErrorMsgHelper.SqlExceptionRead(result.messages, ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }
            finally
            {
                imp.Dispose();
            }

            return result;
        }

        /// <summary>
        /// Get SPK with Customer Request Status
        /// </summary>
        /// <param name="VWI_SPKCustomerHaveRequestFilterDto"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_SPKCustomerHaveRequestDto>> GetSPKCustomerHaveRequest(VWI_SPKCustomerHaveRequestFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_SPKCustomerHaveRequest), "DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<VWI_SPKCustomerHaveRequestDto>>();
            var sortColl = new SortCollection();

            try
            {
                // define sql
                var sql = Helper.GenerateSQLFromCriteriasAndSort(typeof(VWI_SPKCustomerHaveRequest), filterDto, sortColl, criterias);

                // get data
                var data = _sPKCustomerHaveRequestMapper.RetrieveSP("SELECT * FROM VWI_SPKCustomerHaveRequest " + sql);
                if (data.Count > 0)
                {
                    // calculate the skip 
                    int skip = filterDto.pages < 1 ? 0 : (filterDto.pages - 1) * pageSize;

                    // filter out the data based on the paging                    
                    List<VWI_SPKCustomerHaveRequest> list = new List<VWI_SPKCustomerHaveRequest>();
                    if (sortColl != null && sortColl.Count > 0)
                        list = data.Cast<VWI_SPKCustomerHaveRequest>().Skip(skip).Take(pageSize).ToList();
                    else
                        list = data.Cast<VWI_SPKCustomerHaveRequest>().OrderBy(x => x.ID).Skip(skip).Take(pageSize).ToList();

                    // convert to dto object
                    List<VWI_SPKCustomerHaveRequestDto> listData = list.ConvertList<VWI_SPKCustomerHaveRequest, VWI_SPKCustomerHaveRequestDto>();

                    result.lst = listData;
                    result.total = data.Count;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_SPKCustomerHaveRequest), filterDto);
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
        /// Get SPK Tracking
        /// </summary>
        /// <param name="VWI_SPKTrackingFilterDto"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_SPKTrackingDto>> Read(VWI_SPKTrackingFilterDto filterDto, int pageSize)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_SPKTracking), "ID", MatchType.Greater, 0));
            var result = new ResponseBase<List<VWI_SPKTrackingDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(VWI_SPKTracking), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_SPKTracking), filterDto, sortColl);

                // get data
                var data = _vWISPKTrackingMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<VWI_SPKTracking>().ToList();
                    List<VWI_SPKTrackingDto> listData = list.Select(item => _mapper.Map<VWI_SPKTrackingDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_SPKTracking), filterDto);
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


        #endregion

        #region Private Methods
        private SPKHeaderParameterDto GroupingSPKDetail(SPKHeaderParameterDto spk)
        {
            SPKHeaderParameterDto newSPK = new SPKHeaderParameterDto();
            SPKCustomerParameterDto newSPKCustomer = new SPKCustomerParameterDto();
            SPKDetailParameterDto newSPKDetail = new SPKDetailParameterDto();
            List<SPKDetailParameterDto> newListSPKDetail = new List<SPKDetailParameterDto>();
            List<SPKDetailCustomerParameterDto> newSPKDEtailCustomer = new List<SPKDetailCustomerParameterDto>();
            Dictionary<string, SPKDetailParameterDto> dicTypeColor = new Dictionary<string, SPKDetailParameterDto>();

            newSPK = spk;
            newSPK.SPKDetails = null;

            foreach (SPKDetailParameterDto item in spk.SPKDetails)
            {
                newSPKDetail = new SPKDetailParameterDto();
                string keyTypeColorCode = item.VehicleTypeCode + item.VehicleColorCode;

                // cek type & color code duplicate than merge it
                if (dicTypeColor.ContainsKey(keyTypeColorCode))
                {
                    dicTypeColor.TryGetValue(keyTypeColorCode, out newSPKDetail);
                    newSPKDetail.Quantity += item.Quantity;

                    // get detail customer
                    //foreach (SPKDetailCustomerParameterDto itemDetail in item.SPKDetailCustomers)
                    //{
                    //    newSPKDetail.SPKDetailCustomers.Add(itemDetail);
                    //}

                    //update dictionary
                    dicTypeColor[keyTypeColorCode] = newSPKDetail;
                }
                else
                {
                    newSPKDetail = item;

                    // add dictionary
                    dicTypeColor.Add(keyTypeColorCode, newSPKDetail);
                }
            }

            foreach (KeyValuePair<string, SPKDetailParameterDto> entry in dicTypeColor)
            {
                newSPK.SPKDetails.Add(entry.Value);
            }

            return newSPK;
        }

        /// <summary>
        /// Check if there is any changes in spk detail
        /// </summary>
        /// <param name="spkOnDB"></param>
        /// <param name="spk"></param>
        /// <returns></returns>
        private bool IsSPKDetailChanged(SPKHeader spkOnDB, SPKHeaderParameterDto spk)
        {
            // compare detail count
            if (spkOnDB.SPKDetails.Count != spk.SPKDetails.Count)
                return true;

            // validate each detail
            foreach (var detail in spk.SPKDetails)
            {
                // create temporary object
                SPKDetail tempDetailDB = null;

                // get the spk detail object
                foreach (SPKDetail detailDB in spkOnDB.SPKDetails)
                {
                    if (detailDB.ID == detail.ID)
                    {
                        tempDetailDB = detailDB;
                        break;
                    }
                }

                // detail missing / deleted
                if (tempDetailDB == null) { return true; }

                // spk detail comparison
                if (tempDetailDB.Additional != detail.Additional) { return true; }
                if (tempDetailDB.Amount != detail.Amount) { return true; }
                if (tempDetailDB.Category.CategoryCode != detail.CategoryCode) { return true; }
                if (tempDetailDB.LineItem != detail.LineItem) { return true; }
                if (tempDetailDB.Quantity != detail.Quantity) { return true; }
                if (tempDetailDB.RejectedReason != detail.RejectedReason) { return true; }
                if (tempDetailDB.Remarks != detail.Remarks) { return true; }
                if (tempDetailDB.Status != detail.Status) { return true; }
                if (tempDetailDB.VehicleColorCode != detail.VehicleColorCode) { return true; }
                if (tempDetailDB.VehicleTypeCode != detail.VehicleTypeCode) { return true; }
                if (tempDetailDB.ProfileDetail != null && tempDetailDB.ProfileDetail.Code != detail.ProfileDetailCode) { return true; }

                // spk profile comparison
                foreach (SPKProfile profile in tempDetailDB.SPKProfiles)
                {
                    if (profile.ProfileHeader.Code.Equals("CBU_BODYTYPE1") && !profile.ProfileValue.Equals(detail.CBU_BODYTYPE1, StringComparison.OrdinalIgnoreCase)) { return true; }
                    if (profile.ProfileHeader.Code.Equals("CBU_BODYTYPELCV1") && !profile.ProfileValue.Equals(detail.CBU_BODYTYPELCV1, StringComparison.OrdinalIgnoreCase)) { return true; }
                    if (profile.ProfileHeader.Code.Equals("CBU_LOADPROFILE1") && !profile.ProfileValue.Equals(detail.CBU_LOADPROFILE1, StringComparison.OrdinalIgnoreCase)) { return true; }
                    if (profile.ProfileHeader.Code.Equals("CBU_MEDANOPERASI1") && !profile.ProfileValue.Equals(detail.CBU_MEDANOPERASI1, StringComparison.OrdinalIgnoreCase)) { return true; }
                    if (profile.ProfileHeader.Code.Equals("CBU_OWNERSHIP1") && !profile.ProfileValue.Equals(detail.CBU_OWNERSHIP1, StringComparison.OrdinalIgnoreCase)) { return true; }
                    if (profile.ProfileHeader.Code.Equals("CBU_PURCSTAT") && !profile.ProfileValue.Equals(detail.CBU_PURCSTAT, StringComparison.OrdinalIgnoreCase)) { return true; }
                    if (profile.ProfileHeader.Code.Equals("CBU_PURPOSE1") && !profile.ProfileValue.Equals(detail.CBU_PURPOSE1, StringComparison.OrdinalIgnoreCase)) { return true; }
                    if (profile.ProfileHeader.Code.Equals("CBU_USERAGE1") && !profile.ProfileValue.Equals(detail.CBU_USERAGE1, StringComparison.OrdinalIgnoreCase)) { return true; }
                    if (profile.ProfileHeader.Code.Equals("CBU_WAYPAID1") && !profile.ProfileValue.Equals(detail.CBU_WAYPAID1, StringComparison.OrdinalIgnoreCase)) { return true; }
                    if (profile.ProfileHeader.Code.Equals("CBU_PURPOSE2") && !profile.ProfileValue.Equals(detail.CBU_PURPOSE2, StringComparison.OrdinalIgnoreCase)) { return true; }
                    if (profile.ProfileHeader.Code.Equals("CBU_LEASING") && !profile.ProfileValue.Equals(detail.CBU_LEASING, StringComparison.OrdinalIgnoreCase)) { return true; }
                    if (profile.ProfileHeader.Code.Equals("CBU_CARROSSERIE") && !profile.ProfileValue.Equals(detail.CBU_CARROSSERIE, StringComparison.OrdinalIgnoreCase)) { return true; }
                }
            }

            return false;
        }

        /// <summary>
        /// Validate SPK status update
        /// </summary>
        /// <param name="spkOnDB"></param>
        /// <param name="spk"></param>
        /// <param name="validationResults"></param>
        private bool ValidateSPKStatus(ref SPKHeader spkOnDB, SPKHeaderParameterDto spk, List<SPKDetail> listOfSPKDetailDomain, List<DNetValidationResult> validationResults)
        {
            // initialization
            int iStatus = spk.Status;
            bool iProfileChecking = false;
            bool isBatal = false;

            // get the pre status list
            var arrPreStatus = GetPreStatus(spk.Status);
            arrPreStatus.Sort();

            // validate spk against pre status list
            if (!ValidateSPKPreStatus(arrPreStatus, iStatus, spkOnDB, spk, ref isBatal))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSPKStatusNotListedInPreStatusList, spkOnDB.Status, string.Join(",", arrPreStatus), iStatus)));
                return false;
            }

            // validate new status
            switch (iStatus)
            {
                case 3:
                    iProfileChecking = false;
                    if (isBatal)
                    {
                        if (string.IsNullOrEmpty(spk.RejectedReason))
                        {
                            validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, MessageResource.ErrorMsgSPKBatalRequiredReasonID));
                        }

                        if (spkOnDB.SPKFakturs != null && spkOnDB.SPKFakturs.Count > 0)
                        {
                            validationResults.Add(new DNetValidationResult(MessageResource.ErrorMsgSPKHasFaktur));
                        }

                        var criterias = new CriteriaComposite(new Criteria(typeof(RevisionSPKFaktur), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                        criterias.opAnd(new Criteria(typeof(RevisionSPKFaktur), "SPKHeader.ID", MatchType.Exact, spkOnDB.ID));
                        var data = _spkFakturRevisionMapper.RetrieveByCriteria(criterias);

                        if (data != null && data.Count > 0)
                        {
                            validationResults.Add(new DNetValidationResult(MessageResource.ErrorMsgSPKHasFaktur));
                        }

                        if (listOfSPKDetailDomain != null && listOfSPKDetailDomain.Count > 0)
                        {
                            int total = 0;
                            foreach (SPKDetail spkDetailData in listOfSPKDetailDomain)
                            {
                                CriteriaComposite spkChassisCriterias = null;

                                if (spkDetailData.ID != 0)
                                {
                                    spkChassisCriterias = new CriteriaComposite(new Criteria(typeof(SPKChassis), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                                    spkChassisCriterias.opAnd(new Criteria(typeof(SPKChassis), "SPKDetail.ID", MatchType.Exact, spkDetailData.ID));
                                    //spkChassisCriterias.opAnd(new Criteria(typeof(SPKChassis), "MatchingType", MatchType.Exact, 1));

                                    var dataSPKChassis = _spkChassisMapper.RetrieveByCriteria(spkChassisCriterias);

                                    if (dataSPKChassis != null && dataSPKChassis.Count > 0)
                                    {
                                        if (dataSPKChassis.Count == 1 && ((SPKChassis)dataSPKChassis[0]).MatchingType == 1)
                                        {
                                            total += 1;
                                        }
                                        else if (dataSPKChassis.Count > 1)
                                        {
                                            int totalDataTemp = 0;
                                            foreach (var dataSPKChassisTemp in dataSPKChassis)
                                            {
                                                if (((SPKChassis)dataSPKChassisTemp).MatchingType == 1 || ((SPKChassis)dataSPKChassisTemp).MatchingType == 3)
                                                {
                                                    totalDataTemp += 1;
                                                }
                                                else
                                                {
                                                    totalDataTemp += -1;
                                                }
                                            }

                                            if (totalDataTemp <= 0)
                                            {
                                                total += 0;
                                            }
                                            else
                                            {
                                                total += 1;
                                            }
                                        }
                                        else
                                        {
                                            total += 0;
                                        }
                                    }
                                    else
                                    {
                                        total += 0;
                                    }
                                }
                            }

                            if (total > 0)
                            {
                                validationResults.Add(new DNetValidationResult("SPK ini memiliki Matching kendaraan!"));
                                return false;
                            }
                        }
                    };
                    break;
                case 4:
                case 5:
                case 6:
                    iProfileChecking = false; break;
                default:
                    iProfileChecking = true; break;
            }

            #region Validatsi Tunggu Unit
            int tunggu_unit = _enumBL.GetByCategoryAndCode("EnumStatusSPK.Status", "Tunggu_Unit").ValueId;
            if (iStatus >= tunggu_unit)
            {
                int pending_konsumen = _enumBL.GetByCategoryAndCode("EnumStatusSPK.Status", "Pending_Konsumen").ValueId;
                SortCollection sortColl = new SortCollection();
                sortColl.Add(new Sort(typeof(StatusChangeHistory), "CreatedTime", Sort.SortDirection.ASC));
                CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(StatusChangeHistory), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                criterias.opAnd(new Criteria(typeof(StatusChangeHistory), "DocumentRegNumber", MatchType.Exact, spkOnDB.SPKNumber));
                ArrayList arlStatus = _statusChangeHistoryMapper.RetrieveByCriteria(criterias, sortColl);

                if (arlStatus.Count > 0)
                {
                    string strValue = _enumBL.GetByCategoryAndValue("EnumStatusSPK.Status", iStatus.ToString()).ValueCode;
                    string strValue2 = _enumBL.GetByCategoryAndValue("EnumStatusSPK.Status", (iStatus - 1).ToString()).ValueCode;
                    StatusChangeHistory objStatusChangeHistory = (StatusChangeHistory)arlStatus[arlStatus.Count - 1];
                    if (objStatusChangeHistory.NewStatus == pending_konsumen || spkOnDB.Status == pending_konsumen.ToString())
                    {
                        StatusChangeHistory objStatusChangeHistory2 = null;
                        foreach (var item in arlStatus)
                        {
                            objStatusChangeHistory2 = (StatusChangeHistory)item;
                            if (objStatusChangeHistory2.NewStatus != pending_konsumen && objStatusChangeHistory2.NewStatus >= tunggu_unit)
                            {
                                break;
                            }
                            else
                            {
                                objStatusChangeHistory2 = null;
                            }
                        }

                        if (objStatusChangeHistory2 != null)
                        {
                            string strValue3 = _enumBL.GetByCategoryAndValue("EnumStatusSPK.Status", objStatusChangeHistory2.NewStatus.ToString()).ValueCode;
                            if (iStatus != objStatusChangeHistory2.NewStatus)
                            {
                                DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                                DateTime createdDay = new DateTime(objStatusChangeHistory.CreatedTime.Year, objStatusChangeHistory.CreatedTime.Month, 1);

                                if (objStatusChangeHistory.CreatedTime.ToString("yyyyMM") == DateTime.Now.ToString("yyyyMM"))
                                {
                                    if (iStatus < objStatusChangeHistory2.NewStatus)
                                    {
                                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSPKStatusUpdateFailed, strValue, strValue3)));
                                        return false;
                                    }
                                }
                                else if (DateTime.Compare(today, createdDay) > 0)
                                {
                                    if (iStatus > (objStatusChangeHistory2.NewStatus + 1))
                                    {
                                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSPKStatusUpdateFailedSyarat, strValue, strValue2)));
                                        return false;
                                    }
                                    else if (iStatus < objStatusChangeHistory2.NewStatus)
                                    {
                                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSPKStatusUpdateFailed, strValue, strValue3)));
                                        return false;
                                    }
                                }
                            }
                        }
                    }

                    int iMaxStatus = 0;
                    DateTime iMaxDate = new DateTime(1900, 1, 1);
                    int iPreStatus = 0;
                    int iPreStatus2 = 0;
                    foreach (StatusChangeHistory status in arlStatus)
                    {
                        if (iStatus > tunggu_unit && status.NewStatus == (iStatus - 1))
                            iPreStatus++;

                        if (iStatus >= tunggu_unit && iStatus > status.NewStatus)
                        {
                            if (status.CreatedTime.ToString("yyyyMM") == DateTime.Now.ToString("yyyyMM"))
                                iPreStatus2++;
                        }

                        if (status.NewStatus > iMaxStatus)
                        {
                            iMaxStatus = status.NewStatus;
                            iMaxDate = status.CreatedTime;
                        }
                    }

                    if (iStatus > tunggu_unit && iPreStatus == 0)
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSPKStatusUpdateFailedSyarat, strValue, strValue2)));
                        return false;
                    }

                    if (iMaxDate != new DateTime(1900, 1, 1))
                    {
                        if (iMaxDate.ToString("yyyyMM") == DateTime.Now.ToString("yyyyMM"))
                        {
                            if (iMaxStatus > pending_konsumen && iStatus > iMaxStatus)
                            {
                                validationResults.Add(new DNetValidationResult(MessageResource.ErrorMsgSPKStatusUpdateInSameMonth));
                                return false;
                            }

                            if (iStatus > pending_konsumen && iStatus == (iMaxStatus - 1))
                            {
                                validationResults.Add(new DNetValidationResult(MessageResource.ErrorMsgSPKStatusUpdateInSameMonth));
                                return false;
                            }
                        }
                    }
                }
            }
            #endregion

            // validate spk profile
            if (iProfileChecking)
            {
                List<string> spkNumberList = new List<string>();
                foreach (SPKDetail spkDetail in listOfSPKDetailDomain)
                {
                    if (spkDetail.SPKProfiles.Count == 0)
                        spkNumberList.Add(spkDetail.SPKHeader.SPKNumber);
                }

                if (spkNumberList.Count > 0)
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSPKNoProfiles, string.Join(",", spkNumberList))));
                    return false;
                }
            }

            // Farid additional 20180903
            if (iStatus != spkOnDB.IsSend)
                spkOnDB.IsSend = 0;

            return true;
        }

        /// <summary>
        /// Get pre status
        /// </summary>
        /// <param name="iStatus"></param>
        /// <returns></returns>
        private List<int> GetPreStatus(int iStatus)
        {
            List<int> arrPreStatus = new List<int>();

            if (iStatus == 3 || iStatus == 8)
            {
                arrPreStatus.Add(0);
                // EnumStatusSPK.Status.Awal)
                arrPreStatus.Add(7);
                // EnumStatusSPK.Status.Indent)
                arrPreStatus.Add(1);
                // EnumStatusSPK.Status.Tanda_Jadi)
                arrPreStatus.Add(2);
                // EnumStatusSPK.Status.Lunas)
                arrPreStatus.Add(4);
                // EnumStatusSPK.Status.Pending)
                arrPreStatus.Add(9);
                // EnumStatusSPK.Status.Tunggu_Unit)
                arrPreStatus.Add(10);
                // EnumStatusSPK.Status.Tunggu_Unit_I)
                arrPreStatus.Add(11);
                // EnumStatusSPK.Status.Tunggu_Unit_II)
                arrPreStatus.Add(12);
                // EnumStatusSPK.Status.Tunggu_Unit_III)
                arrPreStatus.Add(13);
                // EnumStatusSPK.Status.Tunggu_Unit_IV)
                arrPreStatus.Add(14);
                // EnumStatusSPK.Status.Tunggu_Unit_V)
                arrPreStatus.Add(15);
                // EnumStatusSPK.Status.Tunggu_Unit_VI)
                arrPreStatus.Add(16);
                // EnumStatusSPK.Status.Tunggu_Unit_VII)
            }
            if (iStatus != 8)
            {
                arrPreStatus.Add(8);
                // EnumStatusSPK.Status.Pending_Konsumen
            }
            if (iStatus == 9)
            {
                arrPreStatus.Add(0);
                // EnumStatusSPK.Status.Awal
                arrPreStatus.Add(4);
                // EnumStatusSPK.Status.Pending
                arrPreStatus.Add(2);
                // EnumStatusSPK.Status.Lunas
            }
            if (iStatus >= 10)
            {
                // arrPreStatus.Remove(8)
                arrPreStatus.Add((iStatus - 1));
            }

            return arrPreStatus;
        }

        /// <summary>
        /// Populate SPK to process
        /// </summary>
        /// <param name="preStatus"></param>
        /// <param name="newStatus"></param>
        /// <param name="spkOnDB"></param>
        /// <param name="isBatal"></param>
        /// <returns></returns>
        private bool ValidateSPKPreStatus(List<int> preStatus, int newStatus, SPKHeader spkOnDB, SPKHeaderParameterDto spk, ref bool isBatal)
        {
            // validate batal
            if (newStatus == _enumBL.GetByCategoryAndCode("EnumStatusSPK.Status", "Batal").ValueId)
            {
                spkOnDB.RejectedReason = spk.RejectedReason;
                isBatal = true;
            }

            // get spk status ID
            var statusSPK = _enumBL.GetByCategoryAndValue("EnumStatusSPK.Status", spkOnDB.Status.ToString()).ValueId;

            // validate spk status id against pre status
            var isValidSPK = preStatus.IndexOf(statusSPK) != -1;

            return isValidSPK;
        }

        /// <summary>
        /// Validate spk reference number
        /// </summary>
        /// <param name="spk"></param>
        /// <param name="validationResults"></param>
        private void ValidateSPKReferenceNumber(SPKHeaderParameterDto spk, List<DNetValidationResult> validationResults)
        {
            if (!string.IsNullOrEmpty(spk.SPKReferenceNumber))
            {
                var criterias = new CriteriaComposite(new Criteria(typeof(SPKHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(SPKHeader), "SPKNumber", MatchType.Exact, spk.SPKReferenceNumber));
                var data = _spkHeaderMapper.RetrieveByCriteria(criterias);
                if (data.Count == 0)
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataNotFound, FieldResource.SPKReferenceNumber)));
                }
                else
                {
                    var spkHeader = data[0] as SPKHeader;
                    if (spkHeader.Dealer != null && !spkHeader.Dealer.DealerCode.Equals(this.DealerCode, StringComparison.OrdinalIgnoreCase))
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSPKNumberExistInOtherDealer, spk.SPKReferenceNumber, spkHeader.Dealer.DealerCode)));
                    }
                }
            }
        }

        private void ValidateLKPP(SPKHeaderParameterDto spk, List<DNetValidationResult> validationResults)
        {
            foreach (SPKDetailParameterDto item in spk.SPKDetails)
            {
                if (!string.IsNullOrEmpty(item.SPKDetailCustomer.LKPPReference))
                { 
                    //get dealer for validate dealergroup in LKPP
                    Dealer dealer = new Dealer();
                    LKPPHeader lkppHeader = new LKPPHeader();
                    LKPPDealer lkppdealer = new LKPPDealer();
                    var _mapper = MapperFactory.GetInstance().GetMapper(typeof(Dealer).ToString());
                    var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(Dealer), "DealerCode", spk.DealerCode));
                    if (masters.Count > 0)
                    {
                        dealer = masters[0] as Dealer;
                    }
                    //cek jika LKPP sudah disetujui dan berada dalam 1 dealergroup
                    var criteriasLKPPHeader = new CriteriaComposite(new Criteria(typeof(LKPPHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    criteriasLKPPHeader.opAnd(new Criteria(typeof(LKPPHeader), "ReferenceNumber", MatchType.Exact, item.SPKDetailCustomer.LKPPReference));
                    criteriasLKPPHeader.opAnd(new Criteria(typeof(LKPPHeader), "Status", MatchType.Exact, 2)); //disetujui
                    var LKPPHeaders = _lkppheaderMapper.RetrieveByCriteria(criteriasLKPPHeader);
                    if (LKPPHeaders.Count > 0)
                    {
                        lkppHeader = LKPPHeaders[0] as LKPPHeader;
                        lkppdealer = lkppHeader.LKPPDealers[0] as LKPPDealer;
                        if (lkppdealer.Dealer.DealerGroup.DealerGroupCode != dealer.DealerGroup.DealerGroupCode)
                        {
                            validationResults.Add(new DNetValidationResult(string.Format("LKPP Reference " + item.SPKDetailCustomer.LKPPReference + " tidak ditemukan pada dealer group " + dealer.DealerGroup.GroupName)));
                        }
                        else
                        {
                            //cek vehicletypecode di SPK harus ada di list LKPP Detail
                            var criteriasLKPPDetail = new CriteriaComposite(new Criteria(typeof(LKPPDetail), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                            criteriasLKPPDetail.opAnd(new Criteria(typeof(LKPPDetail), "VechileType.VechileTypeCode", MatchType.Exact, item.VehicleTypeCode));
                            criteriasLKPPDetail.opAnd(new Criteria(typeof(LKPPDetail), "LKPPHeader.ID", MatchType.Exact, lkppHeader.ID));

                            var LKPPDetails = _lkppdetailMapper.RetrieveByCriteria(criteriasLKPPDetail);
                            if (LKPPDetails.Count == 0)
                            {
                                validationResults.Add(new DNetValidationResult(string.Format("Data Tipe Kendaraan " + item.VehicleTypeCode + " tidak ditemukan pada LKPP Reference " + item.SPKDetailCustomer.LKPPReference)));
                            }
                        }
                    }
                    else
                    {
                        validationResults.Add(new DNetValidationResult(string.Format("LKPP Reference " + item.SPKDetailCustomer.LKPPReference + " tidak ditemukan atau belum disetujui")));
                    }
                }
            }

        }
            /// <summary>
            /// Update spk evidence file path
            /// </summary>
            /// <param name="spk"></param>
            /// <param name="filePath"></param>
            /// <param name="domainSPK"></param>
        private void UpdateSPKEvidencePath(SPKHeaderParameterDto spk, string filePath, SPKHeader domainSPK)
        { 
            if (!string.IsNullOrEmpty(filePath))
            {
                string path = FileUtility.MoveSPKEvidenceFile(domainSPK, filePath, GetEvidenceFileName(domainSPK, spk.EvidenceFile));
                if (!string.IsNullOrEmpty(path))
                {
                    domainSPK.EvidenceFile = path;

                    _spkHeaderMapper.Update(domainSPK, DNetUserName);
                }
            }
        }

        /// <summary>
        /// Get the impersonater
        /// </summary>
        /// <returns></returns>
        private UserImpersonater GetImpersonater()
        {
            string user = AppConfigs.GetString("User");
            string password = AppConfigs.GetString("Password");
            string webServer = AppConfigs.GetString("WebServer");
            UserImpersonater imp = new UserImpersonater(user, password, webServer);
            return imp;
        }

        /// <summary>
        /// Validate file path
        /// </summary>
        /// <param name="validationResults"></param>
        /// <param name="spkHeader"></param>
        /// <returns></returns>
        private string ValidateFilePath(List<DNetValidationResult> validationResults, SPKHeader spkHeader)
        {
            string sapFolder = InterfaceFramework.AppConfigs.GetString("SAPFolder");
            string sapDir = Path.Combine(Path.Combine(InterfaceFramework.AppConfigs.GetString("SAPFileDirectory"), @"OCR\"));
            string destFolder = Path.Combine(sapFolder, sapDir);

            var filePath = Path.Combine(destFolder, spkHeader.SPKCustomer.OCRIdentity.ImagePath);
            if (!File.Exists(filePath))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataNotFound, FieldResource.KTP)));
            }

            return filePath;
        }

        /// <summary>
        /// Validate KTP existance
        /// </summary>
        /// <param name="validationResults"></param>
        /// <param name="list"></param>
        private void ValidateKTP(List<DNetValidationResult> validationResults, List<SPKHeader> list)
        {
            var isKTPExist = list.Any(x => x.SPKCustomer.OCRIdentity != null && !string.IsNullOrEmpty(x.SPKCustomer.OCRIdentity.ImagePath));
            if (!isKTPExist)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSPKNoFile, FieldResource.KTP)));
            }
        }

        /// <summary>
        /// Get SPK Header List
        /// </summary>
        /// <param name="spkDocumentParam"></param>
        /// <returns></returns>
        private ArrayList GetSPKHeaders(SPKDocumentParameterDto spkDocumentParam)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(SPKHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(SPKHeader), "SPKNumber", MatchType.Exact, spkDocumentParam.SPKNumber));
            var data = _spkHeaderMapper.RetrieveByCriteria(criterias);
            return data;
        }

        /// <summary>
        /// Delete evidence file if any
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="customerIdentityFilePath"></param>
        private static void DeleteEvidenceFile(string filePath, string customerIdentityFilePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                FileUtility.DeleteEvidenceFile(filePath);
            }

            if (!string.IsNullOrEmpty(customerIdentityFilePath))
            {
                FileUtility.DeleteEvidenceFile(FileUtility.GetSPKCustomerIdentityAbsoluteFilePath(customerIdentityFilePath));
            }
        }

        /// <summary>
        /// Insert spk with transaction manager
        /// </summary>
        /// <param name="spk"></param>
        /// <returns></returns>
        private int InsertWithTransactionManager(SPKHeader spk, List<SPKDetail> spkDetails, List<KTB.DNet.Domain.SPKCustomerProfile> listOfSPKCustomerProfile, OCRIdentity oCRIdentity, List<OCRFamilyCard> ocrKKIdentity)
        {
            int result = -1;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // add command to insert spk customer
                    this._transactionManager.AddInsert(spk.SPKCustomer, DNetUserName);

                    if (oCRIdentity != null)
                    {
                        oCRIdentity.SPKCustomer = spk.SPKCustomer;

                        if (oCRIdentity.ID == 0)
                        {
                            // add command to insert ocr identity
                            this._transactionManager.AddInsert(oCRIdentity, DNetUserName);
                        }
                        else
                        {
                            this._transactionManager.AddUpdate(oCRIdentity, DNetUserName);
                        }
                    }

                    // add command to insert spk customer profile
                    foreach (SPKCustomerProfile customerProfile in listOfSPKCustomerProfile)
                    {
                        customerProfile.SPKCustomer = spk.SPKCustomer;
                        this._transactionManager.AddInsert(customerProfile, DNetUserName);
                    }

                    // add command to insert spk
                    this._transactionManager.AddInsert(spk, DNetUserName);
                    // add command to insert OCR KK
                    if (ocrKKIdentity != null)
                    {
                        for (int i = 0; i < ocrKKIdentity.Count; i++)
                        {
                            OCRFamilyCard ocrKK = ocrKKIdentity[i];

                            ocrKK.SPKHeaderID = spk;
                            /* //remarks insert/update data OCR KK
                            if (ocrKK.ID == 0)
                            {
                                this._transactionManager.AddInsert(ocrKK, DNetUserName);
                            }
                                else
                            {
                                this._transactionManager.AddUpdate(ocrKK, DNetUserName);
                            }
                            */
                        }
                    }


                    // add command to insert spk detail
                    foreach (SPKDetail item in spkDetails)
                    {
                        this._transactionManager.AddInsert(item, DNetUserName);

                        // add command to insert spk detail customer
                        foreach (SPKDetailCustomer itemDetailCustomer in item.SPKDetailCustomers)
                        {
                            itemDetailCustomer.SPKDetail = item;
                            this._transactionManager.AddInsert(itemDetailCustomer, DNetUserName);

                            // add command to insert spk detail customer profile
                            foreach (DNetDomain.SPKDetailCustomerProfile detailCustomerProfile in itemDetailCustomer.SPKDetailCustomerProfiles)
                            {
                                detailCustomerProfile.SPKDetailCustomer = itemDetailCustomer;
                                detailCustomerProfile.LasUpdateTime = DateTime.Now;
                                this._transactionManager.AddInsert(detailCustomerProfile, DNetUserName);
                            }

                            // add command to insert spk profiles
                            foreach (SPKProfile profile in item.SPKProfiles)
                            {
                                profile.SPKDetail = item;
                                profile.SPKDetailCustomer = itemDetailCustomer;
                                this._transactionManager.AddInsert(profile, DNetUserName);
                            }
                        }
                    }

                    this._transactionManager.PerformTransaction();
                    result = spk.ID;
                }
                catch (SqlException sqlException)
                {
                    ExceptionDispatchInfo.Capture(sqlException).Throw();
                }
                catch (Exception ex)
                {
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }
                finally
                {
                    this.RemoveTaskLocking();
                }
            }

            return result;
        }

        /// <summary>
        /// Update spk with transaction manager
        /// </summary>
        /// <param name="objDomain"></param>
        /// <returns></returns>
        private int UpdateWithTransactionManager(SPKHeader spk, List<SPKDetail> spkDetails, bool updateCustomer, bool updateDetail, List<KTB.DNet.Domain.SPKCustomerProfile> listOfSPKCustomerProfile, OCRIdentity oCRIdentity, List<MessageBase> getFakturbySpkDetailList, List<OCRFamilyCard> ocrKKIdentity)
        {
            // mark as loaded to prevent it loads from db
            spk.SPKCustomer.MarkLoaded();
            spk.MarkLoaded();

            // set default result
            int result = -1;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    if (updateCustomer)
                    {
                        // add command to insert new spk customer profile
                        foreach (SPKCustomerProfile spkCustomerProfile in listOfSPKCustomerProfile)
                        {
                            spkCustomerProfile.SPKCustomer = spk.SPKCustomer;

                            SPKCustomer spkCustOnDB = spk.SPKCustomer;
                            List<SPKCustomerProfile> cpDB = new List<SPKCustomerProfile>();
                            long ProfileGroupIDonDB = 0;
                            foreach (SPKCustomerProfile cp in spkCustOnDB.SPKCustomerProfiles)
                            {
                                if (cp.ProfileGroup.RowStatus == 0)
                                    ProfileGroupIDonDB = cp.ProfileGroup.ID;
                                cpDB.Add(cp);
                            }

                            if (spkCustomerProfile.ProfileGroup.ID == ProfileGroupIDonDB)
                            {
                                if (spkCustomerProfile.ID != 0)
                                {
                                    this._transactionManager.AddUpdate(spkCustomerProfile, DNetUserName);
                                }
                                else
                                {
                                    this._transactionManager.AddInsert(spkCustomerProfile, DNetUserName);
                                }
                            }
                            else
                            {
                                foreach (SPKCustomerProfile cp in spkCustOnDB.SPKCustomerProfiles)
                                {
                                    cp.RowStatus = -1;
                                    cp.LasUpdateTime = DateTime.Now;
                                    this._transactionManager.AddUpdate(cp, DNetUserName);
                                }
                                this._transactionManager.AddInsert(spkCustomerProfile, DNetUserName);
                            }
                        }

                        if (oCRIdentity != null)
                        {
                            oCRIdentity.SPKCustomer = spk.SPKCustomer;

                            if (oCRIdentity.ID == 0)
                            {
                                // add command to insert ocr identity
                                this._transactionManager.AddInsert(oCRIdentity, DNetUserName);
                            }
                            else
                            {
                                this._transactionManager.AddUpdate(oCRIdentity, DNetUserName);
                            }
                        }

                        // add command to update spk customer
                        this._transactionManager.AddUpdate(spk.SPKCustomer, DNetUserName);
                    }

                    if (updateDetail)
                    {
                        // add command to insert spk detail
                        foreach (SPKDetail detail in spkDetails)
                    {
                        string msgTemp = string.Empty;
                        foreach (MessageBase mb in getFakturbySpkDetailList)
                        {
                            msgTemp += mb.ErrorMessage;
                        }

                        if (msgTemp.Contains(detail.ID.ToString()) == false)
                        {
                            if (detail.ID != 0)
                            {
                                _transactionManager.AddUpdate(detail, DNetUserName);
                            }
                            else
                            {
                                detail.LastUpdateBy = DNetUserName;
                                _transactionManager.AddInsert(detail, DNetUserName);
                            }

                            detail.MarkLoaded();

                            // add command to udpate spk detail customer
                            int count = 0;
                            if (detail.SPKDetailCustomers.Count > 1)
                            {
                                count = detail.SPKDetailCustomers.Count - 1;
                            }

                            var n = 0;
                            foreach (SPKDetailCustomer itemDetailCustomer in detail.SPKDetailCustomers)
                            {
                                //to handle fail remove spkdetailcustomer
                                if (count == n)
                                {
                                    itemDetailCustomer.SPKDetail = detail;
                                    if (itemDetailCustomer.ID != 0)
                                    {
                                        this._transactionManager.AddUpdate(itemDetailCustomer, DNetUserName);
                                    }
                                    else
                                    {
                                        itemDetailCustomer.LastUpdateBy = DNetUserName;
                                        this._transactionManager.AddInsert(itemDetailCustomer, DNetUserName);
                                    }

                                    List<int> groupId = new List<int>();
                                    foreach (DNetDomain.SPKDetailCustomerProfile item_ in itemDetailCustomer.SPKDetailCustomerProfiles)
                                    {
                                        groupId.Add(item_.ProfileGroup.ID);
                                    }
                                    groupId = groupId.Distinct().ToList();

                                    // add command to update spk detail customer profile
                                    foreach (DNetDomain.SPKDetailCustomerProfile detailCustomerProfile in itemDetailCustomer.SPKDetailCustomerProfiles)
                                    {
                                        detailCustomerProfile.SPKDetailCustomer = itemDetailCustomer;

                                        if (detail.ID != 0)
                                        {
                                            SPKCustomer spkCustOnDB = spk.SPKCustomer;
                                            SPKDetail spkDetailOnDB = (SPKDetail)_spkDetailMapper.Retrieve(detail.ID);
                                            long ProfileGroupIDonDB = 0;
                                            if (spkDetailOnDB.SPKDetailCustomers != null)
                                            {
                                                foreach (SPKDetailCustomer x in spkDetailOnDB.SPKDetailCustomers)
                                                {
                                                    foreach (DNetDomain.SPKDetailCustomerProfile z in x.SPKDetailCustomerProfiles)
                                                    {
                                                        if (z.ProfileGroup.RowStatus == 0)
                                                            ProfileGroupIDonDB = z.ProfileGroup.ID;
                                                    }
                                                }
                                            }

                                            detailCustomerProfile.LasUpdateTime = DateTime.Now;
                                            if (detailCustomerProfile.ProfileGroup.ID == ProfileGroupIDonDB)
                                            {
                                                if (detailCustomerProfile.ID != 0)
                                                {
                                                    if (groupId.Count > 1 && detailCustomerProfile.RowStatus == 0)
                                                    {
                                                        detailCustomerProfile.RowStatus = -1;
                                                    }
                                                    this._transactionManager.AddUpdate(detailCustomerProfile, DNetUserName);
                                                }
                                                else
                                                {
                                                    this._transactionManager.AddInsert(detailCustomerProfile, DNetUserName);
                                                }
                                            }
                                            else
                                            {
                                                this._transactionManager.AddInsert(detailCustomerProfile, DNetUserName);
                                            }
                                        }
                                        else
                                        {
                                            this._transactionManager.AddInsert(detailCustomerProfile, DNetUserName);
                                        }
                                    }

                                    // add command to update spk profiles
                                    foreach (SPKProfile profile in detail.SPKProfiles)
                                    {
                                        profile.SPKDetail = detail;
                                        profile.SPKDetailCustomer = itemDetailCustomer;
                                        if (profile.ID != 0)
                                        {
                                            this._transactionManager.AddUpdate(profile, DNetUserName);
                                        }
                                        else
                                        {
                                            profile.LastUpdateBy = DNetUserName;
                                            this._transactionManager.AddInsert(profile, DNetUserName);
                                        }

                                    }
                                }
                                n++;
                            }
                        }
                    }
                    }

                    // add command to update spk
                _transactionManager.AddUpdate(spk, DNetUserName);
                    // add command to insert OCR KK
                    if (ocrKKIdentity != null)
                    {
                        for (int i = 0; i < ocrKKIdentity.Count; i++)
                        {
                            OCRFamilyCard ocrKK = ocrKKIdentity[i];

                            ocrKK.SPKHeaderID = spk;
                            /* //remarks insert/update data OCR KK
                            if (ocrKK.ID == 0)
                            {
                                this._transactionManager.AddInsert(ocrKK, DNetUserName);
                            }
                            else
                            {
                                this._transactionManager.AddUpdate(ocrKK, DNetUserName);
                            }
                            */
                        }
                    }

                    _transactionManager.PerformTransaction();
                    result = spk.ID;
                }
                catch (Exception ex)
                {
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }
                finally
                {
                    this.RemoveTaskLocking();
                }
            }

            return result;
        }

        /// <summary>
        /// Handler on executed insert command with transaction manager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void InsertWithTransactionManagerHandler(object sender, TransactionManager.OnInsertArgs args)
        {
            // set the object ID from db returned id
            if (args.DomainObject.GetType() == typeof(SPKHeader))
            {
                ((SPKHeader)args.DomainObject).ID = args.ID;
                ((SPKHeader)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(SPKCustomer))
            {
                ((SPKCustomer)args.DomainObject).ID = args.ID;
                ((SPKCustomer)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(SPKDetail))
            {
                ((SPKDetail)args.DomainObject).ID = args.ID;
                ((SPKDetail)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(SPKDetailCustomer))
            {
                ((SPKDetailCustomer)args.DomainObject).ID = args.ID;
                ((SPKDetailCustomer)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(DNetDomain.SPKDetailCustomerProfile))
            {
                ((DNetDomain.SPKDetailCustomerProfile)args.DomainObject).ID = args.ID;
                ((DNetDomain.SPKDetailCustomerProfile)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(SPKProfile))
            {
                ((SPKProfile)args.DomainObject).ID = args.ID;
                ((SPKProfile)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(SPKCustomerProfile))
            {
                ((SPKCustomerProfile)args.DomainObject).ID = args.ID;
                ((SPKCustomerProfile)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(OCRIdentity))
            {
                ((OCRIdentity)args.DomainObject).ID = args.ID;
                ((OCRIdentity)args.DomainObject).MarkLoaded();
            }
        }

        /// <summary>
        /// Record status changes
        /// </summary>
        /// <param name="spk"></param>
        /// <param name="oldStatus"></param>
        /// <param name="newStatus"></param>
        private void RecordStatusChangeHistory(SPKHeader spk, int oldStatus, int newStatus)
        {
            IStatusChangeHistoryBL statusChangeHistoryBL = new StatusChangeHistoryBL();
            statusChangeHistoryBL.Initialize(UserName, DealerCode);
            statusChangeHistoryBL.Insert((int)LookUp.DocumentType.Surat_Pesanan_Kendaraan, spk.SPKNumber, oldStatus, newStatus);
        }

        /// <summary>
        /// Get MCP status
        /// </summary>
        /// <param name="isCVExists"></param>
        /// <param name="spkCustomer"></param>
        /// <returns></returns>
        private short GetMCPStatus(bool isCVExists, SPKCustomerParameterDto spkCustomer)
        {
            return 0;
            // ToDo : MCP Validation 
            // line : 1264 FRMSPKCustomer.aspx

            //List<StandardCodeDto> enumMCPStatus = new List<StandardCodeDto>();
            //enumMCPStatus = _enumBL.GetByCategory("EnumMCPStatus.MCPStatus");

            //if (spkCustomer.MCPStatus == (enumMCPStatus.Where(s => s.ValueCode == "NotVerifiedMCP").SingleOrDefault().ValueId) && !isCVExists)
            //{
            //    return (short)enumMCPStatus.Where(s => s.ValueCode == "NonMCP").SingleOrDefault().ValueId;
            //}

            //if (spkCustomer.TipeCustomer == (_enumBL.GetByCategoryDesc("EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest", "BUMN_Pemerintah").ValueId) && isCVExists)
            //{
            //    return (short)enumMCPStatus.Where(s => s.ValueCode == "NotVerifiedMCP").SingleOrDefault().ValueId;
            //}

            //return (short)spkCustomer.Status;
        }

        /// <summary>
        /// Get LKPP Status
        /// </summary>
        /// <param name="isPCLCVExists"></param>
        /// <param name="spkCustomer"></param>
        /// <returns></returns>
        private short GetLKPPStatus(bool isPCLCVExists, SPKCustomerParameterDto spkCustomer)
        {
            return 0;
            // ToDo : MCP Validation 
            // line : 1333 FRMSPKCustomer.aspx

            //if (spkCustomer.TipeCustomer == (_enumBL.GetByCategoryDesc("EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest", "BUMN_Pemerintah").ValueId) && isPCLCVExists)
            //{
            //    return (short)_enumBL.GetByCategoryDesc("EnumLKPPStatus.LKPPStatus", "NotVerifiedLKPP").ValueId;
            //}

            //return spkCustomer.LKPPStatus;
        }

        /// <summary>
        /// Get spk status
        /// </summary>
        /// <param name="dealerID"></param>
        /// <returns></returns>
        private ArrayList GetSPKStatus(int dealerID)
        {
            return _sp_GetSPKStatusMapper.RetrieveSP("exec sp_GetSPKStatus " + dealerID);
        }

        /// <summary>
        /// Get list of new model code
        /// </summary>
        /// <returns></returns>
        private ArrayList GetNewModelCode()
        {
            // get new model code from AppConfig (db)
            ArrayList result = new ArrayList();
            AppConfigBL appConfigBL = new AppConfigBL(_mapper);
            AppConfigDto spkModelCode = appConfigBL.GetByName("SPKModelCodeFilter").lst;
            if (spkModelCode != null)
            {
                result.AddRange(spkModelCode.Value.Trim().Split(';'));
            }

            return result;
        }

        /// <summary>
        /// Get list of new type
        /// </summary>
        /// <returns></returns>
        private ArrayList GetListOfNewType()
        {
            // get list of new type code from AppConfig
            AppConfigBL appConfigBL = new AppConfigBL(_mapper);
            AppConfigDto spkModelCode = appConfigBL.GetByName("SPKModelCodeFilter").lst;
            if (spkModelCode != null)
            {
                string modelCode = string.Format("('{0}')", spkModelCode.Value.Trim().Replace(";", "','"));

                // get list of new type based on list of new type code
                CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(VechileType), "RowStatus", MatchType.Exact, (int)_enumBL.GetByCategoryAndCode("DBRowStatus", "Active").ValueId));
                criteria.opAnd(new Criteria(typeof(VechileType), "VechileModel.VechileModelCode", MatchType.InSet, modelCode));
                return _vehicleTypeMapper.RetrieveByCriteria(criteria);
            }

            return new ArrayList();
        }

        /// <summary>
        /// Get list of spk faktur
        /// </summary>
        /// <param name="spk"></param>
        /// <returns></returns>
        private ArrayList GetSPKFaktur(SPKHeaderParameterDto spk)
        {
            CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(SPKFaktur), "RowStatus", MatchType.Exact, (int)_enumBL.GetByCategoryAndCode("DBRowStatus", "Active").ValueId));
            criteria.opAnd(new Criteria(typeof(SPKFaktur), "SPKHeader.ID", MatchType.Exact, spk.ID));
            return _spkFakturMapper.RetrieveByCriteria(criteria);
        }

        /// <summary>
        /// Get salesman by code
        /// </summary>
        /// <param name="salesmanCode"></param>
        /// <param name="spkDealerCode"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private SalesmanHeader GetSalesmanByCode(string salesmanCode, string spkDealerCode, List<DNetValidationResult> validationResults)
        {
            SalesmanHeader salesman = null;

            /// SalesmanHeader hanya bisa input yang aktif di dealer tersebut dan status harus dicek => SELECT * FROM dbo.SalesmanHeader 
            /// WHERE Status = 2 AND DealerId = 3 AND SalesIndicator = 1 AND RowStatus = 0
            if (ValidationHelper.ValidateSalesmanHeader(salesmanCode, spkDealerCode, validationResults, ref salesman, false))
            {
                bool isValid = true;
                //if (salesman.RowStatus != (int)_enumBL.GetByCategoryAndCode("DBRowStatus", "Active").ValueId)
                //{
                //    isValid = false;
                //    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.Salesman)));
                //}

                if (salesman.SalesIndicator != 1)
                {
                    isValid = false;
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.Salesman)));
                }

                //if (salesman.Status != _enumBL.GetByCategoryAndCode("EnumSalesmanStatus.SalesmanStatus", "Aktif").ValueId.ToString())
                //{
                //    isValid = false;
                //    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.Salesman)));
                //}

                if (!isValid)
                {
                    return null;
                }
            }

            return salesman;
        }

        /// <summary>
        /// Check if new type exists
        /// </summary>
        /// <param name="spkDetail"></param>
        /// <returns></returns>
        private bool IsNewTypeExists(List<SPKDetail> spkDetails)
        {
            List<VechileType> listOfNewType = GetListOfNewType().Cast<VechileType>().ToList();
            if (listOfNewType != null && listOfNewType.Count > 0)
            {
                foreach (SPKDetail item in spkDetails)
                {
                    IEnumerable<VechileType> itemNewTypes = listOfNewType.Where(v => item.VehicleTypeCode.ToString().ToUpper() == v.VechileTypeCode.Trim().ToUpper());
                    if (itemNewTypes.Any())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Check if it is a new model
        /// </summary>
        /// <param name="modelCode"></param>
        /// <returns></returns>
        private bool IsNewModel(string modelCode)
        {
            // get list on new model code
            ArrayList listOfNewModelCode = GetNewModelCode();
            bool isNewModel = false;

            foreach (string code in listOfNewModelCode)
            {
                // check if the list of new model code contain the model code
                if (code.ToUpper() == modelCode.ToUpper())
                {
                    isNewModel = true;
                    break;
                }
            }

            return isNewModel;
        }

        /// <summary>
        /// Check if evidence is mandatory from AppConfig table 
        /// </summary>
        /// <returns></returns>
        private bool IsEvidenceMandatory()
        {
            // get appconfig using AppConfigBL
            IAppConfigBL appConfigBL = new AppConfigBL(_mapper);
            ResponseBase<AppConfigDto> response = appConfigBL.GetByName("SPKEvidence");

            if (response.success && response.lst.ID > 0)
            {
                return response.lst.Value.Trim() == "1";
            }

            return false;
        }

        /// <summary>
        /// Check if need spk validation 
        /// </summary>
        /// <returns></returns>
        private bool NeedSPKValidation()
        {
            // get spk validation config
            IAppConfigBL appConfigBL = new AppConfigBL(_mapper);
            ResponseBase<AppConfigDto> response = appConfigBL.GetByName("SPKValidation");

            if (response.success && response.lst.ID > 0)
            {
                return response.lst.Value.Trim() == "1";
            }

            return false;
        }

        /// <summary>
        /// Is category CV exists on spk details
        /// </summary>
        /// <returns></returns>
        private bool IsCVExists(List<SPKDetailParameterDto> spkDetails)
        {
            bool isCVExists = false;
            foreach (SPKDetailParameterDto detail in spkDetails)
            {
                // retrieve category from db                
                ArrayList listOfCategory = _categoryMapper.RetrieveByCriteria(Helper.GetCodeCriteria(typeof(Category), "CategoryCode", detail.CategoryCode));
                if (listOfCategory.Count > 0)
                {
                    var category = listOfCategory[0] as Category;
                    if (category.CategoryCode == "CV")
                    {
                        isCVExists = true;
                        break;
                    }
                }
            }

            return isCVExists;
        }

        /// <summary>
        /// Is PCLCV exists
        /// </summary>
        /// <param name="spkDetails"></param>
        /// <returns></returns>
        private bool IsPCLCVExists(List<SPKDetailParameterDto> spkDetails)
        {
            bool isPCLCVExists = false;
            foreach (SPKDetailParameterDto detail in spkDetails)
            {
                // retrieve category from db                
                ArrayList listOfCategory = _categoryMapper.RetrieveByCriteria(Helper.GetCodeCriteria(typeof(Category), "CategoryCode", detail.CategoryCode));
                if (listOfCategory.Count > 0)
                {
                    var category = listOfCategory[0] as Category;
                    if (category.CategoryCode == "PC" || category.CategoryCode == "LCV")
                    {
                        isPCLCVExists = true;
                        break;
                    }
                }
            }

            return isPCLCVExists;
        }

        /// <summary>
        /// Validate spk 
        /// </summary>
        /// <param name="spk"></param>
        /// <param name="domainSPK"></param>
        /// <returns></returns>
        private SPKHeader GetValidSPKDomainObject(SPKHeaderParameterDto spk, out List<SPKDetail> listOfDomainSPKDetail, List<DNetValidationResult> validationResults, out OCRIdentity oCRIdentity, out List<OCRFamilyCard> ocrKKIdentity, out string filePath, out bool allowToUpdateDataCustomer, out List<DNetDomain.SPKCustomerProfile> listOfSPKCustomerProfile, out List<MessageBase> getFakturbySpkDetailList, out string msgSPKDetailCantUpdate, SPKHeader spkOnDB = null)
        {

            #region Initialize
            oCRIdentity = null;
            listOfDomainSPKDetail = new List<SPKDetail>();
            bool isUpdateDetailStatusWithSPKStatus = false;
            listOfSPKCustomerProfile = null;
            byte[] fileBytes = null;
            filePath = string.Empty;
            allowToUpdateDataCustomer = true;
            Dealer dealer = null;
            getFakturbySpkDetailList = new List<MessageBase>();
            ocrKKIdentity = null;
            msgSPKDetailCantUpdate = string.Empty;
            #endregion
            // check Is Wo SF Config
            var isWoSf = ValidationHelper.ValidateWoSf(this.DealerCode);

            // validate dealer
            ValidationHelper.ValidateDealer(spk.DealerCode, validationResults, this.DealerCode, ref dealer);

            // validate salesman code
            SalesmanHeader salesman = GetSalesmanByCode(spk.SalesmanCode, spk.DealerCode, validationResults);

            // return if any errors found
            if (validationResults.Count > 0)
            {
                return null;
            }

            #region Save SPK File
            // validate the evidence file if exists
            if (spk.EvidenceFile != null)
            {
                validationResults.AddRange(FileUtility.ValidateEvidenceOrIdentityFile(spk.EvidenceFile, _mapper, out fileBytes, FieldResource.SPKEvidence));
                if (validationResults.Any())
                {
                    return null;
                }
                else if (fileBytes != null)
                {
                    // save the file
                    string uploadErrorMessage = FileUtility.SaveSPKEvidenceFile(spk.EvidenceFile, fileBytes, out filePath);
                    if (!string.IsNullOrEmpty(uploadErrorMessage))
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataType, uploadErrorMessage)));
                        return null;
                    }
                }
            }
            #endregion

            #region Validate spk
            //// check the invoice date for KTB
            //if (spk.PlanInvoiceDate < spk.PlanDeliveryDate)
            //{
            //    validationResults.Add(new DNetValidationResult(MessageResource.ErrorMsgInvalidFakturDate));
            //}

            // check spk customer
            if (spk.SPKCustomer == null)
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.SPKCustomer)));
            }

            // get spk status reference
            List<StandardCodeDto> listOfSPKStatus = _enumBL.GetByCategory("EnumStatusSPK.Status");
            if (listOfSPKStatus.Where(status => status.ValueId == spk.Status).Count() == 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.SPKStatus + " " + spk.Status)));
            }

            // if it is an update process
            if (spk.ID != 0)
            {
                // unable to update customer data if the faktur has already created
                #region Not implemented on existing KTB
                // on KTB Customer could be update unconditionally
                ArrayList listOfFaktur = GetSPKFaktur(spk);
                if (listOfFaktur != null && listOfFaktur.Count > 0)
                {
                    allowToUpdateDataCustomer = false;
                }
                #endregion

                if (spkOnDB != null)
                {
                    int status = spkOnDB.Status.ToInt();
                    if (status == _enumBL.GetByCategoryAndCode("EnumStatusSPK.Status", "Selesai").ValueId)
                    {
                        validationResults.Add(new DNetValidationResult(MessageResource.ErrorMsgCouldNotUpdateSPKWithCancelOrFinishStatus.Replace("/batal","")));
                    }
                    else if (status == _enumBL.GetByCategoryAndCode("EnumStatusSPK.Status", "Batal").ValueId)
                    {
                        validationResults.Add(new DNetValidationResult(MessageResource.ErrorMsgCouldNotUpdateSPKWithCancelOrFinishStatus.Replace("selesai/", "")));
                    }
                }

                isUpdateDetailStatusWithSPKStatus = spkOnDB.Status.Trim() != spk.Status.ToString().Trim();

                // if spkstatus = batal then rejected reason should not empty
                List<StandardCodeDto> listOfStatusBatal = listOfSPKStatus.Where(st => st.ValueCode.Trim().ToUpper() == "BATAL").ToList();
                if (listOfStatusBatal.Any() && listOfStatusBatal[0].ValueId == spk.Status)
                {
                    List<StandardCodeDto> listOfRejectedReason = _enumBL.GetByCategory("enumRejectedReason.RejectedReason");
                    if (listOfRejectedReason.Any())
                    {
                        if (string.IsNullOrEmpty(spk.RejectedReason))
                        {
                            validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.RejectedReason)));
                            return null;
                        }
                        else
                        {
                            // set default rejected reason to -1 if it is null or empty
                            int rejectedReasonID = -1;
                            bool isAbleToParseRejectedReasonID = int.TryParse(spk.RejectedReason, out rejectedReasonID);
                            rejectedReasonID = isAbleToParseRejectedReasonID ? rejectedReasonID : -1;

                            if (listOfRejectedReason.Where(reason => reason.ValueId == rejectedReasonID).Count() == 0)
                            {
                                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.RejectedReason)));
                            }
                        }
                    }
                    else
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataNotFound, string.Format(FieldResource.Listof, FieldResource.RejectedReason))));
                    }
                }
            }

            //validate EventType & validate CampaignName
            if (spk.EventType != null)
            {
                int SPKEventType = Convert.ToInt32(spk.EventType);
                if (_enumBL.GetByCategory("BabitEventCampaign.EventType").Where(x => x.ValueId == SPKEventType).Count() > 0)
                {
                    if (SPKEventType != _enumBL.GetByCategoryAndCode("BabitEventCampaign.EventType", "Others_Transaction").ValueId)
                    {
                        if (!string.IsNullOrEmpty(spk.CampaignName))
                        {
                            if (!ValidationHelper.ValidateDataCampaign(SPKEventType, spk.CampaignName, DealerCode))
                            {
                                validationResults.Add(new DNetValidationResult(string.Format("Data Campaign dengan {0} {1} dan EventType {2} tidak ditemukan", FieldResource.CampaignName, spk.CampaignName, spk.EventType)));
                            }
                        }
                        else
                        {
                            validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.CampaignName)));
                        }
                    }
                }
                else
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataNotFound, string.Format("EventType", spk.EventType.ToString()))));
                }
            }
            
            // return if any errors found
            if (validationResults.Count > 0)
            {
                return null;
            }
            #endregion

            #region Validate spk detail
            // create domain spk object

            SPKHeader domainSPK = new SPKHeader();
            if (spkOnDB != null)
            {
                _mapper.Map<SPKHeader, SPKHeader>(spkOnDB, domainSPK);
            }
            _mapper.Map<SPKHeaderParameterDto, SPKHeader>(spk, domainSPK);
            domainSPK.MarkLoaded();
            SPKDetailBL spkDetailBL = new SPKDetailBL(_mapper);
            List<int> listOfDetailIDExistsOnDB = new List<int>();
            List<SPKDetail> listOfDetailOnDB = domainSPK.SPKDetails.Cast<SPKDetail>().ToList();

            // get the enum
            int rowStatusActiveCode = (int)_enumBL.GetByCategoryAndCode("DBRowStatus", "Active").ValueId;
            List<StandardCodeDto> listOfDBRowStatus = _enumBL.GetByCategory("DBRowStatus");


            List<MessageBase> tempGetFakturbySpkDetailList = new List<MessageBase>();
            // validate every spk detail on parameter input
            foreach (SPKDetailParameterDto detail in spk.SPKDetails)
            {
                // validate dealer category
                ValidationHelper.ValidateDealerCategory(spk.DealerCode, spk.SalesmanCode, detail.VehicleTypeCode, validationResults);

                SPKDetail domainSPKDetail;
                if (detail.SPKHeaderID == spk.ID)
                {
                    validationResults.AddRange(spkDetailBL.SetSPKDetailFromParameterDto(detail, out domainSPKDetail, rowStatusActiveCode, listOfDBRowStatus, listOfSPKStatus, out msgSPKDetailCantUpdate, spk.ID == 0));
                    if (domainSPKDetail != null)
                    {
                        domainSPKDetail.SPKHeader = domainSPK;

                        // merge with detail on db
                        if (listOfDetailOnDB.Any(d => d.ID == domainSPKDetail.ID))
                        {
                            SPKDetailCustomer spkDetailCustomer = new SPKDetailCustomerBL(_mapper).GetValidSPKDetailCustomerDomain(detail.SPKDetailCustomer, validationResults, spk.DealerCode, out oCRIdentity, out tempGetFakturbySpkDetailList);

                            // assign getFakturbySPKDetailList message
                            if (tempGetFakturbySpkDetailList.Count > 0)
                            {
                                getFakturbySpkDetailList.Add(tempGetFakturbySpkDetailList[0]);
                            }


                            if (validationResults.Any())
                            {
                                return domainSPK;
                            }

                            //validate EventType & validate CampaignName
                            if (detail.EventType != null)
                            {
                                int SPKDetailEventType = Convert.ToInt32(detail.EventType);
                                if (_enumBL.GetByCategory("BabitEventCampaign.EventType").Where(x => x.ValueId == SPKDetailEventType).Count() > 0)
                                {
                                    if (SPKDetailEventType != _enumBL.GetByCategoryAndCode("BabitEventCampaign.EventType", "Others_Transaction").ValueId)
                                    {
                                        if (!string.IsNullOrEmpty(detail.CampaignName))
                                        {
                                            if (!ValidationHelper.ValidateDataCampaign(SPKDetailEventType, detail.CampaignName, DealerCode))
                                            {
                                                validationResults.Add(new DNetValidationResult(string.Format("Data Campaign dengan {0} {1} dan EventType {2} tidak ditemukan", FieldResource.CampaignName, detail.CampaignName, detail.EventType)));
                                            }
                                            else
                                            {
                                                domainSPKDetail.EventType = SPKDetailEventType;
                                                domainSPKDetail.CampaignName = detail.CampaignName;
                                            }
                                        }
                                        else
                                        {
                                            validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.CampaignName)));
                                        }
                                    }
                                }
                                else
                                {
                                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataNotFound, string.Format("EventType", detail.EventType.ToString()))));
                                }
                            }
                            
                            spkDetailCustomer.MarkLoaded();
                            domainSPKDetail.SPKDetailCustomers.Clear();
                            domainSPKDetail.SPKDetailCustomers.Add(spkDetailCustomer);

                            listOfDetailIDExistsOnDB.Add(domainSPKDetail.ID);
                            listOfDomainSPKDetail.Add(domainSPKDetail);
                        }
                        else
                        {
                            if (domainSPKDetail.ID != 0)
                            {
                                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMessageDataNotFound, FieldResource.SPKDetail, domainSPKDetail.ID)));
                            }
                            else
                            {
                                SPKDetailCustomer spkDetailCustomer = new SPKDetailCustomerBL(_mapper).GetValidSPKDetailCustomerDomain(detail.SPKDetailCustomer, validationResults, spk.DealerCode, out oCRIdentity, out tempGetFakturbySpkDetailList);

                                // assign getFakturbySPKDetailList message
                                if (tempGetFakturbySpkDetailList.Count > 0)
                                {
                                    getFakturbySpkDetailList.Add(tempGetFakturbySpkDetailList[0]);
                                }

                                if (validationResults.Any())
                                {
                                    return domainSPK;
                                }
                                
                                //validate EventType & validate CampaignName
                                if (detail.EventType != null)
                                {
                                    int SPKDetailEventType = Convert.ToInt32(detail.EventType);
                                    if (_enumBL.GetByCategory("BabitEventCampaign.EventType").Where(x => x.ValueId == SPKDetailEventType).Count() > 0)
                                    {
                                        if (SPKDetailEventType != _enumBL.GetByCategoryAndCode("BabitEventCampaign.EventType", "Others_Transaction").ValueId)
                                        {
                                            if (!string.IsNullOrEmpty(detail.CampaignName))
                                            {
                                                if (!ValidationHelper.ValidateDataCampaign(SPKDetailEventType, detail.CampaignName, DealerCode))
                                                {
                                                    validationResults.Add(new DNetValidationResult(string.Format("Data Campaign dengan {0} {1} dan EventType {2} tidak ditemukan", FieldResource.CampaignName, detail.CampaignName, detail.EventType)));
                                                }
                                                else
                                                {
                                                    domainSPKDetail.EventType = SPKDetailEventType;
                                                    domainSPKDetail.CampaignName = detail.CampaignName;
                                                }
                                            }
                                            else
                                            {
                                                validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.CampaignName)));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataNotFound, string.Format("EventType", detail.EventType.ToString()))));
                                    }
                                }

                                spkDetailCustomer.MarkLoaded();
                                domainSPKDetail.SPKDetailCustomers.Clear();
                                domainSPKDetail.SPKDetailCustomers.Add(spkDetailCustomer);

                                listOfDomainSPKDetail.Add(domainSPKDetail);
                            }
                        }
                    }
                }
                else
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.SPKHeaderIDOnSPKDetail)));
                }               
            }

            listOfDomainSPKDetail.AddRange(listOfDetailOnDB.Where(d => !listOfDetailIDExistsOnDB.Contains(d.ID)));

            // update all detail status based on spk header status
            // except for Tanda_Jadi (1) status, bacause it has the same value with spk detail Canceled status (1) 
            if (isUpdateDetailStatusWithSPKStatus)
            {
                int canceledDetailStatus = _enumBL.GetByCategoryAndCode("DBRowStatus", "Canceled").ValueId;
                if (!(int.Parse(domainSPK.Status) == canceledDetailStatus))
                {
                    listOfDomainSPKDetail.ForEach(detail =>
                    {
                        if (spkDetailBL.AllowToUpdateSPKDetail(detail))
                        {
                            detail.Status = (byte)int.Parse(domainSPK.Status);
                        }
                    });
                }
            }

            bool newTypeExists = false;

            // validate spk detail
            ValidateSPKDetails(listOfDomainSPKDetail, dealer, validationResults, out newTypeExists);
            if (validationResults.Any())
            {
                return domainSPK;
            }

            // if there's any new model then evidence is mandatory
            #region Not implemented on existing KTB
            if (newTypeExists)
            {
                // check if evidence is mandatory from config
                bool isEvidenceMandatory = IsEvidenceMandatory();
                if (fileBytes == null && isEvidenceMandatory)
                {
                    validationResults.Add(new DNetValidationResult(MessageResource.ErrorMsgSPKEvidenceIsRequired));
                }
            }
            #endregion

            // retrun if any errors found
            if (validationResults.Count > 0)
            {
                return domainSPK;
            }
            #endregion

            // add new fuction for check the new type for Xpander Cross

            bool isEmailMandatory = false;

            foreach (SPKDetail detailData in listOfDomainSPKDetail)
            {
                if (detailData.VehicleTypeCode.Contains("NI") || detailData.VehicleTypeCode.Contains("NJ") || detailData.VehicleTypeCode.Contains("NK"))
                {
                    isEmailMandatory = true;
                }

            }

            #region Validate spk customer
            // check if the spk customer is different on update spk
            if (spk.ID != 0)
            {
                if (spkOnDB != null)
                {
                    if (spkOnDB.SPKCustomer.ID != spk.SPKCustomer.ID)
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataNotMatch, FieldResource.SPKCustomer)));
                        return domainSPK;
                    }
                }
            }
            else
            {
                spk.SPKCustomer.ID = 0;
            }

            SPKCustomer spkCustomer = new SPKCustomerBL(_mapper).GetValidSPKCustomerDomain(spk.SPKCustomer, validationResults, spk.DealerCode, out oCRIdentity, out listOfSPKCustomerProfile);
            if (validationResults.Any())
            {
                return domainSPK;
            }

            if (!string.IsNullOrEmpty(spk.SPKCustomer.ReffCode))
            {
                ValidateReffCustomerCode(spk.SPKCustomer.ReffCode, spk.DealerCode, validationResults);
                if (validationResults.Any())
                {
                    return domainSPK;
                }
            }

            // add new fuction for check the new type for Xpander Cross
            if (isEmailMandatory)
            {
                if (string.IsNullOrEmpty(spkCustomer.Email))
                {
                    validationResults.Add(new DNetValidationResult("Untuk Tipe Kendaraan XPander Cross, email konsumen harus diinput."));
                    return domainSPK;
                }
            }
            // end of discussion

            if (isWoSf)
            {
                // Only check on Create
                if (spk.ID == 0)
                {
                    // Get SAP Customer from SPK Customer
                    var sapCustomer = spkCustomer.SAPCustomer;

                    // SAP Customer Status must be 3 and Lead State Code must be 2
                    if (sapCustomer.Status != 3 || sapCustomer.StateCode != 2)
                    {
                        validationResults.Add(new DNetValidationResult("Status harus 'Hot Prospect' dan State harus 'Won'"));
                    }
                }

            }
            #endregion

            #region OCR KK
            if (spk.OCRFamilyIdentity != null)
            {
                if (string.IsNullOrEmpty(spk.OCRFamilyIdentity.IdentityFile.ImagePath))
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.ImageAttachment)));
                    return null;
                }

                ocrKKIdentity = new List<OCRFamilyCard>();
                if (!string.IsNullOrEmpty(spk.OCRFamilyIdentity.IdentityFile.ImageID))
                {
                    if (spk.ID != 0)
                    {
                        /* Waiting OCRKK Engine 
                        if (ValidationHelper.ValidateOCRFamilyCardServer())
                        {
                        */
                        ValidationHelper.ValidateOCRFamilyCard(spk.ID, validationResults, ref ocrKKIdentity);
                        /* 
                            // reset
                            validationResults.Clear();
                        }
                        else
                        {
                            return null;
                        }
                        */
                    }
                    else
                    {
                        /* Waiting OCRKK Engine */
                        //if (!ValidationHelper.ValidateOCRFamilyCardServer())
                        //{
                        //    return null;
                        //}
                    }
                }
                else
                {
                    if (spk.ID != 0)
                    {
                        // initialize the ocridentity mapper
                        var ocrKKMapper = MapperFactory.GetInstance().GetMapper(typeof(OCRFamilyCard).ToString());

                        // get by criteria
                        var listOCRKK = ocrKKMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(OCRFamilyCard), "SPKHeader.ID", spk.ID));
                        if (listOCRKK.Count > 0)
                        {
                            for (int i = 0; i < listOCRKK.Count; i++)
                            {
                                ocrKKIdentity.Add(listOCRKK[i] as OCRFamilyCard);
                            }
                        }
                    }
                }

                int countOCRKKDetail = spk.OCRFamilyIdentity.FamilyMembers.Count();
                for (int x = 0; x < countOCRKKDetail; x++)
                {
                    OCRFamilyCard dataParamKK = spk.OCRFamilyIdentity.FamilyMembers[x].ConvertObject<OCRFamilyCard>();
                    dataParamKK.ImagePath = spk.OCRFamilyIdentity.IdentityFile.ImagePath;
                    dataParamKK.ImageID = spk.OCRFamilyIdentity.IdentityFile.ImageID;

                    ocrKKIdentity.Add(dataParamKK);
                }
            }
            #endregion

            #region Create domain object
            // create domain object 
            // map SPKHeaderParameterDto -> SPKHeader

            // define mcp and lkpp status
            /********** It will be implemented on KTB ****************/

            //bool isCVExists = IsCVExists(spk.SPKDetails);
            //short mcpStatus = GetMCPStatus(isCVExists, spk.SPKCustomer);

            //bool isPCLCV = IsPCLCVExists(spk.SPKDetails);
            //short lkppStatus = !isCVExists ? GetLKPPStatus(isPCLCV, spk.SPKCustomer) : spk.SPKCustomer.LKPPStatus;
            //spkCustomer.MCPStatus = mcpStatus;
            //spkCustomer.LKPPStatus = lkppStatus;
            /**********************************************************/

            // mark as loaded because it will be used by spk header
            spkCustomer.MarkLoaded();

            // update the other properties
            if (spkCustomer.ID == 0)
            {
                spkCustomer.CreatedTime = DateTime.Now;
            }

            spkCustomer.LastUpdateTime = DateTime.Now;
            domainSPK.Dealer = dealer;
            domainSPK.SPKCustomer = spkCustomer;
            domainSPK.DealerBranch = salesman.DealerBranch;
            domainSPK.SalesmanHeader = salesman;
            if (!string.IsNullOrEmpty(spk.SPKReferenceNumber))
                domainSPK.SPKReferenceNumber = spk.SPKReferenceNumber;
            if (spk.EventType != null)
                domainSPK.EventType = Convert.ToInt32(spk.EventType); 
            domainSPK.CampaignName = spk.CampaignName;
            #endregion

            #region Not implemented on existing KTB
            // if it is an update process
            // move to spkbl impacted by cr spk faktur 20201104 
            // spk detail vehicletype on spkdetail separate to each row and qty = 1
            if (domainSPK.ID != 0)
            {
                ArrayList listOfFaktur = GetSPKFaktur(domainSPK.ID);
                if ((listOfFaktur != null && listOfFaktur.Count > 0))
                {
                    // if faktur exist
                    Dictionary<string, int> dictVehicleTypeQty = new Dictionary<string, int>();
                    foreach (SPKDetail itemDEtail in domainSPK.SPKDetails)
                    {
                        if (dictVehicleTypeQty.ContainsKey(itemDEtail.VehicleTypeCode))
                        {
                            int qty = 0;
                            dictVehicleTypeQty.TryGetValue(itemDEtail.VehicleTypeCode, out qty);
                            qty += itemDEtail.Quantity;
                            dictVehicleTypeQty[itemDEtail.VehicleTypeCode] = qty;
                        }
                        else
                        {
                            dictVehicleTypeQty.Add(itemDEtail.VehicleTypeCode, itemDEtail.Quantity);
                        }
                    }

                    foreach (KeyValuePair<string, int> entry in dictVehicleTypeQty)
                    {
                        int iQtyAssigned = 0;
                        foreach (SPKFaktur faktur in listOfFaktur)
                        {
                            if (faktur.EndCustomer != null && faktur.EndCustomer.ChassisMaster != null)
                            {
                                if (!string.IsNullOrEmpty(faktur.EndCustomer.ChassisMaster.VechileType) &&
                                    entry.Key == faktur.EndCustomer.ChassisMaster.VechileType)
                                {
                                    iQtyAssigned = iQtyAssigned + 1;
                                }
                            }
                        }

                        if (iQtyAssigned > 0 && entry.Value < iQtyAssigned)
                        {
                            validationResults.Add(new DNetValidationResult(string.Format("Jumlah quantity [{0}] tidak boleh kurang dari jumlah yang sudah dibuat permohonan faktur. SPK untuk tipe ini sudah dibuat faktur sejumlah {1}", entry.Key, iQtyAssigned.ToString())));
                        }
                    }
                }

                if (validationResults.Count > 0)
                {
                    return domainSPK;
                }
            }
            #endregion


            return domainSPK;
        }

        /// <summary>
        /// Validate spk detail
        /// </summary>
        /// <param name="spkDetails"></param>
        /// <param name="spk"></param>
        /// <param name="newTypeExists"></param>
        /// <returns></returns>
        private List<DNetValidationResult> ValidateSPKDetails(List<SPKDetail> spkDetails, Dealer dealer, List<DNetValidationResult> validationResults, out bool newTypeExists)
        {
            newTypeExists = false;

            // check if spk detail is null or empty
            if (spkDetails == null || spkDetails.Count() == 0)
            {
                validationResults.Add(new DNetValidationResult(MessageResource.ErrorMsgDetailEmpty));
            }

            #region Not implemented on existing KTB
            // check if new type Exists
            newTypeExists = IsNewTypeExists(spkDetails);

            // dalam 1 SPK model baru tidak boleh digabung dengan model lain
            ValidateNewType(validationResults, spkDetails, dealer.ID, newTypeExists);
            #endregion

            // validate duplicate type and color
            //ValidateDuplicateSPKDetail(spkDetails, validationResults);

            return validationResults;
        }

        /// <summary>
        /// Get list of spk faktur
        /// </summary>
        /// <param name="spkID"></param>
        /// <returns></returns>
        private ArrayList GetSPKFaktur(int spkID)
        {
            CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(SPKFaktur), "RowStatus", MatchType.Exact, (int)_enumBL.GetByCategoryAndCode("DBRowStatus", "Active").ValueId));
            criteria.opAnd(new Criteria(typeof(SPKFaktur), "SPKHeader.ID", MatchType.Exact, spkID));
            return _spkFakturMapper.RetrieveByCriteria(criteria);
        }

        private ArrayList GetRevisionSPKFaktur(int spkID)
        {
            CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(RevisionSPKFaktur), "RowStatus", MatchType.Exact, (int)_enumBL.GetByCategoryAndCode("DBRowStatus", "Active").ValueId));
            criteria.opAnd(new Criteria(typeof(RevisionSPKFaktur), "SPKHeader.ID", MatchType.Exact, spkID));
            return _revisionSpkFakturMapper.RetrieveByCriteria(criteria);
        }

        /// <summary>
        /// Validate ref customer code
        /// </summary>
        /// <param name="reffCode"></param>
        /// <returns></returns>
        private List<DNetValidationResult> ValidateReffCustomerCode(string reffCode, string dealerCode, List<DNetValidationResult> validationResults)
        {
            Customer customer = null;
            ValidationHelper.ValidateCustomer(reffCode, validationResults, ref customer);
            if (validationResults.Count > 0)
            {
                return validationResults;
            }

            // Jika ada Ref Code Pelanggan, maka harus dicek dari table customer join ke customerdealer cek berdasar dealer code -> 
            // kalo exist maka accept kalo tidak : "Customer tidak ada di dalam dealer ini"
            Dealer dealer = null;
            if (ValidationHelper.ValidateDealer(dealerCode, validationResults, this.DealerCode, ref dealer, false))
            {
                CustomerDealer customerDealer = null;
                ValidationHelper.ValidateCustomerDealer(customer.ID, validationResults, ref customerDealer, dealer.ID);
            }

            return validationResults;
        }

        /// <summary>
        /// Validate new type
        /// </summary>
        /// <param name="spkDetails"></param>
        /// <param name="dealerID"></param>
        /// <param name="newTypeExists"></param>
        /// <returns></returns>
        private void ValidateNewType(List<DNetValidationResult> validationResults, List<SPKDetail> spkDetails, int dealerID, bool newTypeExists)
        {
            try
            {
                if (NeedSPKValidation())
                {
                    if (newTypeExists)
                    {
                        if (spkDetails != null && spkDetails.Count > 0)
                        {
                            if (spkDetails[0].SPKHeader.ID == 0)
                            {
                                ValidateLastMonthTransaction(dealerID, validationResults);
                            }
                        }
                    }

                    ValidateNewTypeExistsOnListOfOtherType(validationResults, spkDetails, newTypeExists);
                }
            }
            catch (Exception ex)
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.UnhandledException, string.Format(MessageResource.ErrorMsgPRGUnhandle, ex.Message)));
            }
        }

        /// <summary>
        /// Validate last mounth transaction
        /// </summary>
        /// <param name="dealerID"></param>
        /// <returns></returns>
        private void ValidateLastMonthTransaction(int dealerID, List<DNetValidationResult> validationResults)
        {
            //Penambahan blocking alert saat create SPK yaitu jika terdapat status SPK pada bulan sebelumnya dengan status :
            //- Awal
            //- Pending Konsumen
            //- Tunggu Unit, 1, 2, 3, 4, 5
            //dan last update status nya tidak sama dengan bulan berjalan, maka create SPK tidak dapat dilakukan
            //*Validasi hanya berlaku untuk SPK yang akan di create dengan type RN dan SPK yang dilihat pada bulan sebelumnya juga adalah SPK dengan pembelian unit RN

            try
            {
                // get list of spk status
                ArrayList listOfSPKStatus = GetSPKStatus(dealerID);
                if (listOfSPKStatus != null && listOfSPKStatus.Count > 0)
                {
                    string errorMessage = string.Empty;
                    foreach (sp_GetSPKStatus status in listOfSPKStatus)
                    {
                        errorMessage = errorMessage + status.Values.ToString() + " " + _enumBL.GetByCategoryAndValue("EnumStatusSPK.Status", status.Status.ToString()).ValueCode + "\n";
                    }

                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgHasUnupdatedXPanderSPK, errorMessage)));
                    }
                }
            }
            catch
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.UnhandledException, string.Format(MessageResource.ErrorMsgPRGUnhandle, MessageResource.ErrorMsgCheckingLastTransaction)));
            }
        }

        /// <summary>
        /// Validate if new type exists on list of other type
        /// </summary>
        /// <param name="spkDetails"></param>
        /// <param name="newTypeExists"></param>
        /// <returns></returns>
        private void ValidateNewTypeExistsOnListOfOtherType(List<DNetValidationResult> validationResults, List<SPKDetail> spkDetails, bool newTypeExists)
        {
            try
            {
                int newModelCount = 0;
                int oldModelCount = 0;

                // check every spk detail
                foreach (SPKDetail detail in spkDetails)
                {
                    // get vehicle color from db                    
                    //if (detail.VechileColor != null)
                    //{
                    VechileType vehicleType = null;
                    CriteriaComposite vehicleTypeCriteria = new CriteriaComposite(new Criteria(typeof(VechileType), "RowStatus", MatchType.Exact, ((short)(_enumBL.GetByCategoryAndCode("DBRowStatus", "Active").ValueId))));

                    vehicleTypeCriteria.opAnd(new Criteria(typeof(VechileType), "VechileTypeCode", MatchType.Exact, detail.VehicleTypeCode));
                    ArrayList listOfVehicleType = _vehicleTypeMapper.RetrieveByCriteria(vehicleTypeCriteria);
                    if (listOfVehicleType.Count > 0)
                    {
                        vehicleType = listOfVehicleType[0] as VechileType;
                    }

                    if (vehicleType != null)
                    {
                        if (vehicleType.VechileModel != null)
                        {
                            if (IsNewModel(vehicleType.VechileModel.VechileModelCode.ToUpper()))
                            {
                                #region Not Implemented on KTB
                                //if (detail.ID != 0)
                                //{
                                //    SPKDetail detailOnDB = (SPKDetail)_spkDetailMapper.Retrieve(detail.ID);
                                //    if (detailOnDB != null && detailOnDB.Quantity < detail.Quantity)
                                //    {
                                //        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgMaxQuantityXPander, detailOnDB.Quantity)));
                                //    }
                                //}
                                #endregion
                                newModelCount += 1;
                            }
                            else
                            {
                                oldModelCount += 1;
                            }
                        }
                        else
                        {
                            validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataNotFoundComplete, FieldResource.VehicleModel, FieldResource.VehicleType, detail.VechileColor.VechileType.VechileTypeCode)));
                        }
                    }
                    else
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataNotFoundComplete, FieldResource.VehicleModel, FieldResource.VehicleType, detail.VehicleTypeCode)));
                    }
                    //}
                    //else
                    //{
                    //    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgVehicleColorNotFound, detail.VehicleColorCode)));
                    //}
                }

                // return error if the new type exists with some of other types
                if (newTypeExists && oldModelCount > 0)
                {
                    validationResults.Add(new DNetValidationResult(MessageResource.ErrorMsgCouldNotIncludXPanderWithAnotherType));
                }
            }
            catch
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.UnhandledException, string.Format(MessageResource.ErrorMsgPRGUnhandle, MessageResource.ErrorMsgVehicleValidation)));
            }
        }

        /// <summary>
        /// Validate duplicate spk detail
        /// </summary>
        /// <param name="spkDetails"></param>
        /// <returns></returns>
        private List<DNetValidationResult> ValidateDuplicateSPKDetail(List<SPKDetail> spkDetails, List<DNetValidationResult> validationResults)
        {
            List<string> duplicatedCode = new List<string>();
            foreach (SPKDetail detail in spkDetails)
            {
                // check only for color code != zzzz
                if (detail.VehicleColorCode.Trim() != "ZZZZ")
                {
                    // check if there's more than 1 data which have similar type code and color code
                    if (!duplicatedCode.Contains(detail.VehicleTypeCode.Trim() + detail.VehicleColorCode.Trim()))
                    {
                        List<SPKDetail> listOfDuplicateDetails = spkDetails.Where(d => d.VehicleTypeCode.Trim() == detail.VehicleTypeCode.Trim() && d.VehicleColorCode.Trim() == detail.VehicleColorCode.Trim()).ToList();
                        if (listOfDuplicateDetails.Count() > 1)
                        {
                            duplicatedCode.Add(detail.VehicleTypeCode.Trim() + detail.VehicleColorCode.Trim());
                            validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataType, string.Format(MessageResource.ErrorMsgDuplicateVehicle, detail.VehicleTypeCode.Trim(), detail.VehicleColorCode.Trim()))));
                        }
                    }
                }
            }

            return validationResults;
        }

        /// <summary>
        /// Get evidence file name
        /// </summary>
        /// <param name="spk"></param>
        /// <param name="attachment"></param>
        /// <returns></returns>
        private string GetEvidenceFileName(SPKHeader spk, AttachmentParameterDto attachment)
        {
            string filename = string.Format("{0}_{1}{2}",
                              spk.Dealer.DealerCode,
                              string.IsNullOrEmpty(spk.SPKNumber) ? spk.ID.ToString() : spk.SPKNumber,
                              attachment.FileName.Substring(attachment.FileName.Length - 4));

            return filename;
        }

        #endregion
    }
}
