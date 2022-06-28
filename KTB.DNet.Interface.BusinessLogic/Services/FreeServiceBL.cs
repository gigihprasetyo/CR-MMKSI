#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : FreeService business logic class
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
using KTB.DNet.BusinessValidation;
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class FreeServiceBL : AbstractBusinessLogic, IFreeServiceBL
    {
        #region Variables
        private readonly IMapper _freeServiceMapper;
        private readonly IMapper _freeServicePartDetailMapper;
        private readonly IMapper _freeServiceBBMapper;
        private readonly IMapper _chassisMasterMapper;
        private readonly IMapper _chassisMasterBBMapper;
        private readonly IMapper _fleetFakturMapper;
        private readonly IMapper _fsKindMapper;
        private readonly IMapper _pdiMapper;
        private readonly IMapper _pmHeaderMapper;
        private readonly IMapper _fsCampaignMapper;
        private readonly IMapper _fsChassisCampaignMapper;
        private readonly IMapper _fsKindOnVehicleTypeMapper;
        private readonly AutoMapper.IMapper _mapper;
        private StandardCodeBL _enumBL;
        private StandardCodeCharBL _enumCharBL;
        private readonly IMapper _chassisMasterPKTMapper;

        #endregion

        #region Constructor
        public FreeServiceBL()
        {
            _freeServiceMapper = MapperFactory.GetInstance().GetMapper(typeof(FreeService).ToString());
            _freeServicePartDetailMapper = MapperFactory.GetInstance().GetMapper(typeof(FreeServicePartDetail).ToString());
            _freeServiceBBMapper = MapperFactory.GetInstance().GetMapper(typeof(FreeServiceBB).ToString());
            _chassisMasterMapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMaster).ToString());
            _chassisMasterBBMapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMasterBB).ToString());
            _fleetFakturMapper = MapperFactory.GetInstance().GetMapper(typeof(FleetFaktur).ToString());
            _fsKindMapper = MapperFactory.GetInstance().GetMapper(typeof(FSKind).ToString());
            _pdiMapper = MapperFactory.GetInstance().GetMapper(typeof(PDI).ToString());
            _fsKindOnVehicleTypeMapper = MapperFactory.GetInstance().GetMapper(typeof(FSKindOnVechileType).ToString());
            _fsChassisCampaignMapper = MapperFactory.GetInstance().GetMapper(typeof(FSChassisCampaign).ToString());
            _fsCampaignMapper = MapperFactory.GetInstance().GetMapper(typeof(FSCampaign).ToString());
            _pmHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(PMHeader).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _enumBL = new StandardCodeBL(_mapper);
            _enumCharBL = new StandardCodeCharBL(_mapper);
            _chassisMasterPKTMapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMasterPKT).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get FreeService by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<FreeServiceDto>> Read(FreeServiceFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(FreeService), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(FreeService), "Dealer.DealerCode", MatchType.Exact, DealerCode));
            criterias.opAnd(new Criteria(typeof(FreeService), "Status", MatchType.Exact, 3));
            var result = new ResponseBase<List<FreeServiceDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(FreeService), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(FreeService), filterDto, sortColl);

                // get data
                var data = _freeServiceMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<FreeService>().ToList();
                    List<FreeServiceDto> lstResult = new List<FreeServiceDto>();
                    // get detail part
                    foreach (var itemHd in list)
                    {
                        var freeServiceDto = _mapper.Map<FreeServiceDto>(itemHd);
                        freeServiceDto.FreeServicePartDetails = new List<FreeServicePartDetailDto>();
                        var crtDetail = new CriteriaComposite(new Criteria(typeof(FreeServicePartDetail), "FreeService.ID", MatchType.Exact, freeServiceDto.ID));
                        var dtDetails = _freeServicePartDetailMapper.RetrieveByCriteria(crtDetail);
                        decimal totalPartBeforeRounding = 0;
                        if (dtDetails.Count > 0)
                        {
                            foreach(FreeServicePartDetail itemDetail in dtDetails)
                            {
                                var detail = new FreeServicePartDetailDto();
                                detail.PartNumber = itemDetail.SparePartMaster.PartNumber;
                                detail.UnitPrice = itemDetail.PartPrice == 0 || itemDetail.Quantity == 0 ? 0 : itemDetail.PartPrice / itemDetail.Quantity;
                                detail.Qty = itemDetail.Quantity;
                                detail.PartPrice = itemDetail.PartPrice;

                                freeServiceDto.FreeServicePartDetails.Add(detail);

                                // count all part price
                                totalPartBeforeRounding += itemDetail.PartPrice;
                            }
                        }

                        freeServiceDto.TotalPartBeforeRounding = totalPartBeforeRounding;
                        freeServiceDto.Rounding = freeServiceDto.PartAmount - totalPartBeforeRounding;

                        lstResult.Add(freeServiceDto);
                    }

                    result.lst = lstResult;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(FreeService), filterDto);
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
        /// Delete FreeService by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<FreeServiceDto> Delete(int id)
        {
            var result = new ResponseBase<FreeServiceDto>();

            try
            {
                var freeService = (FreeService)_freeServiceMapper.Retrieve(id);
                if (freeService != null)
                {
                    freeService.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _freeServiceMapper.Update(freeService, DNetUserName);
                    if (nResult != 0)
                    {
                        result.success = true;
                        result._id = id;
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

        /// <summary>
        /// Create a new FreeService
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<FreeServiceDto> Create(FreeServiceParameterDto objCreate)
        {
            #region Declare
            var result = new ResponseBase<FreeServiceDto>();
            var validationResults = new List<DNetValidationResult>();
            var validResultList = new List<ValidResult>();
            var isValid = true;
            ChassisMaster chassisMaster = null;
            Dealer soldDealer = null;
            Dealer fsDealer = null;
            DealerBranch dealerBranch = null;
            FSKind fsKind = null;
            FleetFaktur objFleetFaktur = null;
            var attachment = new AttachmentParameterDto();
            byte[] fileBytes = null;
            string filePath = string.Empty;


            #endregion

            // check if it is BB
            if (objCreate.isBB) { return CreateFreeServiceBB(objCreate); }

            try
            {
                // validate parameter values
                var newFreeService = new FreeService();
                
                var freeServiceValidation = new FreeServiceValidation();
                //validResultList = freeServiceValidation.ValidateFreeService(ref newFreeService, this.DealerCode);
                isValid = ValidateFreeService(objCreate, validationResults, ref validResultList, ref newFreeService);
                // insert if valid
                if (validResultList.Count == 0)
                {
                    //validate evidence
                    if(!string.IsNullOrEmpty(objCreate.FileName) && !string.IsNullOrEmpty(objCreate.Base64OfStream))
                    {
                        attachment.FileName = objCreate.FileName;
                        attachment.Base64OfStream = objCreate.Base64OfStream;
                        // validate the evidence file if exists
                        if (attachment != null)
                        {
                            validationResults.AddRange(FileUtility.ValidateEvidenceOrIdentityFileFS(attachment, _mapper, out fileBytes, FieldResource.FSEvidence));
                            if (validationResults.Any())
                            {
                                return PopulateValidationError<FreeServiceDto>(validationResults, null);
                            }
                            else if (fileBytes != null)
                            {
                                // save the file
                                string ext = Path.GetExtension(attachment.FileName);
                                Guid ID = Guid.NewGuid();
                                var new_ID = Convert.ToString(ID);
                                new_ID = new_ID.Substring(new_ID.Length - 10);
                                var new_file_name = (objCreate.ChassisNumber).Trim() + "-" + new_ID + ext;


                                string uploadErrorMessage = FileUtility.SaveFSEvidenceFile(new_file_name, fileBytes, out filePath,true);
                                if (!string.IsNullOrEmpty(uploadErrorMessage))
                                {
                                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataType, uploadErrorMessage)));
                                    // return if any errors found
                                    if (validationResults.Any())
                                    {
                                        return PopulateValidationError<FreeServiceDto>(validationResults, null);
                                    }
                                }
                                else
                                {
                                    var isvalidFile = ValidationCorruptFileFS(filePath, validationResults);
                                    if(!isvalidFile)
                                    {
                                        //delete uploaded files
                                        string uploadErrorMessageDelete = FileUtility.SaveFSEvidenceFile(new_file_name, fileBytes, out filePath,false);
                                        return PopulateValidationError<FreeServiceDto>(validationResults, null);
                                    }
                                    else
                                    {
                                        if (!String.IsNullOrEmpty(objCreate.ChassisNumber))
                                        {
                                            newFreeService.FileName = attachment.FileName;

                                            var filepathfix = filePath.Replace(AppConfigs.GetString("SAN"), @"\");
                                            newFreeService.FilePath = filepathfix;
                                        }
                                        else
                                        {
                                            validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, "Chassis Number tidak boleh kosong"));
                                        }
                                    }
                                    
                                }
                            }
                        }
                        else
                        {
                            validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, "Silakan masukkan file evidence WO yang telah dilengkapi tanda tangan konsumen dan petugas Dealer untuk pengajuan FS Claim ke APM"));
                        }
                    }
                    else
                    {
                        validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, "Silakan masukkan file evidence WO yang telah dilengkapi tanda tangan konsumen dan petugas Dealer untuk pengajuan FS Claim ke APM"));
                    }
                    if(validationResults.Count==0)
                    {
                        newFreeService.Status = _enumBL.GetByCategoryAndCode("EnumFSStatus.FSStatus", "Baru").ValueId.ToString();
                        newFreeService.CreatedBy = DNetUserName;
                        newFreeService.CreatedTime = DateTime.Now;
                        newFreeService.LastUpdateBy = DNetUserName;
                        newFreeService.LastUpdateTime = DateTime.Now;
                        newFreeService.ReleaseDate = SqlDateTime.MinValue.Value;

                        if (newFreeService.SoldDate == Convert.ToDateTime("1753-01-01"))
                        {
                            newFreeService.SoldDate = getDataSoldDate(newFreeService.ChassisMaster.ID);
                        }

                        var success = (int)_freeServiceMapper.Insert(newFreeService, DNetUserName);
                        result.success = success > 0;
                        if (!result.success)
                        {
                            ErrorMsgHelper.UpdateNotAvailable(result.messages);
                        }
                        // return output ID
                        result._id = success;
                        result.total = 1;
                    }
                    else
                    {
                        return PopulateValidationError<FreeServiceDto>(validationResults, null);
                    }
                }
                else
                {
                    return PopulateValidationError<FreeServiceDto>(validationResults, null);
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

        
        public ResponseBase<FreeServiceDto> Delete(FreeServiceDeleteParameterDto paramDelete)
        {
            var result = new ResponseBase<FreeServiceDto>()
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
                    var freeServiceCriteria = new CriteriaComposite(new Criteria(typeof(FreeService), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    //check if WO Number exist
                    freeServiceCriteria.opAnd(new Criteria(typeof(FreeService), "WorkOrderNumber", MatchType.Exact, paramDelete.WorkOrderNumber));
                    //check Dealer
                    freeServiceCriteria.opAnd(new Criteria(typeof(FreeService), "Dealer.DealerCode", MatchType.Exact, DealerCode));
                    if (paramDelete.DealerBranchCode.Trim() != string.Empty)
                    {
                        freeServiceCriteria.opAnd(new Criteria(typeof(FreeService), "DealerBranch.DealerBranchCode", MatchType.Exact, paramDelete.DealerBranchCode));
                    }
                    freeServiceCriteria.opAnd(new Criteria(typeof(FreeService), "ChassisMaster.ChassisNumber", MatchType.Exact, paramDelete.ChassisNumber));

                    var freeServices = _freeServiceMapper.RetrieveByCriteria(freeServiceCriteria);
                    if (freeServices.Count == 0)
                    {
                        validationResults.Add(new DNetValidationResult(string.Format("Work Order Number {0} tidak terdaftar untuk Dealer {1}", paramDelete.WorkOrderNumber, DealerCode)));
                    }
                    else
                    {
                        var freeService = freeServices.Cast<FreeService>().ToList()[0];
                        // check row status
                        if (freeService.RowStatus != 0)
                        {
                            validationResults.Add(new DNetValidationResult(string.Format("Work Order Number {0} tidak aktif, tidak dapat membatalkan Free Service dengan Work Order Number yang tidak aktif", paramDelete.WorkOrderNumber)));
                        }
                        // check if status == baru
                        else if (freeService.Status != _enumBL.GetByCategoryAndCode("EnumFSStatus.FSStatus", "Baru").ValueId.ToString())
                        {
                            validationResults.Add(new DNetValidationResult(string.Format("Work Order Number {0} sedang diproses dan tidak dapat dibatalkan", paramDelete.WorkOrderNumber)));
                        }
                        // delete
                        else
                        {

                            freeService.RowStatus = -1;
                            var success = (int)_freeServiceMapper.Update(freeService, DNetUserName);
                            result.success = success > 0;
                            if (!result.success)
                            {
                                ErrorMsgHelper.UpdateNotAvailable(result.messages);
                            }
                            // return output ID
                            result._id = freeService.ID;
                            result.total = 1;
                        }

                    }
                }

                if (validationResults.Count > 0)
                {
                    return PopulateValidationError<FreeServiceDto>(validationResults, null);
                }
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Validate free service
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="isFleetExists"></param>
        /// <param name="chassisMaster"></param>
        /// <param name="soldDealer"></param>
        /// <param name="fsDealer"></param>
        /// <param name="dealerBranch"></param>
        /// <param name="fsKind"></param>
        /// <param name="objFleetFaktur"></param>
        /// <returns></returns>
        public bool ValidateFreeService(FreeServiceParameterDto objCreate, List<DNetValidationResult> validationResults, ref List<ValidResult> validResultList, ref FreeService newFreeService)
        {
            // validate parameter values
            //isValid = ValidateFreeService(objCreate, validationResults, ref chassisMaster, ref soldDealer, ref fsDealer, ref dealerBranch, ref fsKind, ref objFleetFaktur);
            #region Map Free Service from Parameter
            newFreeService = new FreeService();
            newFreeService = _mapper.Map<FreeService>(objCreate);
            var newChassisMaster = new ChassisMaster()
            {
                ID = 0,
                ChassisNumber = objCreate.ChassisNumber,
                EngineNumber = objCreate.EngineNumber
            };
            newChassisMaster.MarkLoaded();

            var newDealer = new Dealer()
            {
                ID = 0,
                DealerCode = objCreate.DealerCode
            };
            newDealer.MarkLoaded();

            var newDealerBranch = new DealerBranch()
            {
                ID = 0,
                DealerBranchCode = objCreate.DealerBranchCode
            };
            newDealerBranch.MarkLoaded();

            var newFSKind = new FSKind()
            {
                ID = 0,
                KindCode = objCreate.FSKindCode
            };
            newFSKind.MarkLoaded();

            //cannot map via automapper
            newFreeService.ChassisMaster = newChassisMaster;
            newFreeService.Dealer = newDealer;
            newFreeService.DealerBranch = newDealerBranch;
            newFreeService.FSKind = newFSKind;
            #endregion
            var freeServiceValidation = new FreeServiceValidation();
            validResultList = freeServiceValidation.ValidateFreeServiceCentralize(ref newFreeService, this.DealerCode, objCreate.ServiceDate);
            if (validResultList.Count > 0)
            {
                foreach (var validResult in validResultList)
                {
                    validationResults.Add(new DNetValidationResult(validResult.Message));
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Create free service BB
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        private ResponseBase<FreeServiceDto> CreateFreeServiceBB(FreeServiceParameterDto objCreate)
        {
            #region Declare
            var result = new ResponseBase<FreeServiceDto>();
            var validationResults = new List<DNetValidationResult>();
            var validResults = new List<ValidResult>();
            #endregion

            try
            {
                var newFreeServiceBB = new FreeServiceBB();
                
                var freeServiceValidation = new FreeServiceValidation();
                var isValid = ValidateFreeServiceBB(objCreate, validationResults, ref validResults, ref newFreeServiceBB);

                if (validResults.Count == 0)
                {
                    newFreeServiceBB.Status = _enumBL.GetByCategoryAndCode("EnumFSStatus.FSStatus", "Baru").ValueId.ToString();
                    newFreeServiceBB.CreatedBy = DNetUserName;
                    newFreeServiceBB.CreatedTime = DateTime.Now;
                    newFreeServiceBB.LastUpdateBy = DNetUserName;
                    newFreeServiceBB.LastUpdateTime = DateTime.Now;
                    newFreeServiceBB.ReleaseDate = SqlDateTime.MinValue.Value;

                    var success = (int)_freeServiceBBMapper.Insert(newFreeServiceBB, DNetUserName);
                    result.success = success > 0;
                    if (!result.success) ErrorMsgHelper.UpdateNotAvailable(result.messages);
                    // return output ID
                    result._id = success;
                    result.total = 1;
                }
                else
                {
                    return PopulateValidationError<FreeServiceDto>(validationResults, null);
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
        /// Validate free service BB
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        /// <param name="chassisMasterBB"></param>
        /// <param name="soldDealer"></param>
        /// <param name="fsDealer"></param>
        /// <param name="dealerBranch"></param>
        /// <param name="fsKind"></param>
        /// <returns></returns>
        public bool ValidateFreeServiceBB(FreeServiceParameterDto objCreate, List<DNetValidationResult> validationResults, ref List<ValidResult> validResultList, ref FreeServiceBB newFreeServiceBB)
        {
            #region Map Parameter
            newFreeServiceBB = new FreeServiceBB();
            newFreeServiceBB = _mapper.Map<FreeServiceBB>(objCreate);
            var newChassisMasterBB = new ChassisMasterBB()
            {
                ID = 0,
                ChassisNumber = objCreate.ChassisNumber,
                EngineNumber = objCreate.EngineNumber
            };
            newChassisMasterBB.MarkLoaded();

            var newDealer = new Dealer()
            {
                ID = 0,
                DealerCode = objCreate.DealerCode
            };
            newDealer.MarkLoaded();

            var newDealerBranch = new DealerBranch()
            {
                ID = 0,
                DealerBranchCode = objCreate.DealerBranchCode
            };
            newDealerBranch.MarkLoaded();

            var newFSKind = new FSKind()
            {
                ID = 0,
                KindCode = objCreate.FSKindCode
            };
            newFSKind.MarkLoaded();

            //cannot map via automapper
            newFreeServiceBB.ChassisMasterBB = newChassisMasterBB;
            newFreeServiceBB.Dealer = newDealer;
            newFreeServiceBB.DealerBranch = newDealerBranch;
            newFreeServiceBB.FSKind = newFSKind;
            #endregion

            var freeServiceValidation = new FreeServiceValidation();
            validResultList = freeServiceValidation.ValidateFreeServiceBB(ref newFreeServiceBB, this.DealerCode);
            if (validResultList.Count > 0)
            {
                foreach (var validResult in validResultList)
                {
                    validationResults.Add(new DNetValidationResult(validResult.Message));
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Update FreeService
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<FreeServiceDto> Update(FreeServiceParameterDto objUpdate)
        {
            return null;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Validate chassis and fskind code
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="isFleetExists"></param>
        /// <param name="chassisMaster"></param>
        /// <param name="fsDealer"></param>
        /// <param name="fsKind"></param>
        private bool ValidateChassisAndKindCode(FreeServiceParameterDto objCreate, List<DNetValidationResult> validationResults, bool isFleetExists, ChassisMaster chassisMaster, Dealer fsDealer, FSKind fsKind)
        {
            if (fsKind.KindCode.Length == 1)
            {
                if (objCreate.FSKindCode == "1" || objCreate.FSKindCode == "2")
                {
                    return true;
                }
            }

            bool isAllowed = IsChassisAllowed(objCreate.ChassisNumber) || isFleetExists || IsAllowFreeService(chassisMaster, fsKind, fsDealer.DealerCode);
            if (!isAllowed)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgFSNoKupon, objCreate.ChassisNumber)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate if valid for fs special
        /// </summary>
        /// <param name="chassisNumber"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool IsValidCMForFSSpecial(string chassisNumber, List<DNetValidationResult> validationResults)
        {
            CriteriaComposite cCMBB = new CriteriaComposite(new Criteria(typeof(ChassisMasterBB), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            cCMBB.opAnd(new Criteria(typeof(ChassisMasterBB), "ChassisNumber", MatchType.Exact, chassisNumber));
            var aCMBBs = _chassisMasterBBMapper.RetrieveByCriteria(cCMBB);
            if (aCMBBs.Count == 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(ValidationResource.ChassisNoFreeServiceSpecial, chassisNumber)));
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate if already PM
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="fsType"></param>
        /// <param name="isValid"></param>
        private bool ValidateAlreadyPM(FreeServiceParameterDto objCreate, List<DNetValidationResult> validationResults, string fsType)
        {
            int _fsType;
            int.TryParse(fsType, out _fsType);
            if (_fsType == 2)
            {
                ArrayList arlPMHeader;
                CriteriaComposite criteriasPMHeader = new CriteriaComposite(new Criteria(typeof(KTB.DNet.Domain.PMHeader), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                criteriasPMHeader.opAnd(new Criteria(typeof(KTB.DNet.Domain.PMHeader), "ChassisMaster.ChassisNumber", MatchType.Exact, objCreate.ChassisNumber));

                arlPMHeader = _pmHeaderMapper.RetrieveByCriteria(criteriasPMHeader);
                if (arlPMHeader.Count == 0)
                {
                    validationResults.Add(new DNetValidationResult(string.Format(ValidationResource.ChassisNotExistInPMHeader, objCreate.ChassisNumber)));
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Validate visit type
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        private bool ValidateVisitType(FreeServiceParameterDto objCreate, List<DNetValidationResult> validationResults)
        {
            var result = _enumCharBL.GetByCategoryAndCode("VisitType", objCreate.VisitType);
            if (result == null)
            {
                validationResults.Add(new DNetValidationResult(string.Format(ValidationResource.VisitTypeDoesNotExist, objCreate.VisitType)));
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate chassis and engine for Chassis Master BB
        /// </summary>
        /// <param name="paramDto"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        private bool ValidateChassisAndEngineBB(FreeServiceParameterDto paramDto, List<DNetValidationResult> validationResults)
        {
            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(ChassisMasterBB), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias.opAnd(new Criteria(typeof(ChassisMasterBB), "ChassisNumber", MatchType.Exact, paramDto.ChassisNumber));

            ArrayList masters = _chassisMasterBBMapper.RetrieveByCriteria(criterias);
            if (masters.Count == 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgChassisNotFound, paramDto.ChassisNumber)));
            }
            else
            {
                var chassis = masters[0] as ChassisMasterBB;
                if (!chassis.EngineNumber.Equals(paramDto.EngineNumber, StringComparison.OrdinalIgnoreCase))
                {
                    validationResults.Add(new DNetValidationResult(string.Format(ValidationResource.ChassisAndEngineDoesNotMatch, paramDto.EngineNumber, paramDto.ChassisNumber)));
                }
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate sold date
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        private bool ValidateSoldDate(FreeServiceParameterDto objCreate, List<DNetValidationResult> validationResults)
        {
            //if (objCreate.SoldDate > objCreate.ServiceDate)
            //{
            //    validationResults.Add(new DNetValidationResult(ErrorCode.DataTypeOrDataFormatInvalid, string.Format(ValidationResource.SoldDateGreaterThanServiceDate, objCreate.SoldDate, objCreate.ServiceDate)));
            //}

            return validationResults.Count == 0;
        }

        /// <summary>
        /// validate fs dealer
        /// </summary>
        /// <param name="dealerCode"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        /// <param name="chassisMasterID"></param>
        /// <param name="soldDealer"></param>
        /// <param name="fsDealer"></param>
        private bool ValidateFSDealer(string dealerCode, List<DNetValidationResult> validationResults, ChassisMaster chassisMaster, Dealer soldDealer, ref Dealer fsDealer)
        {
            // validate dealer code
            var isValid = ValidationHelper.ValidateDealer(dealerCode, validationResults, this.DealerCode, ref fsDealer);
            if (isValid)
            {
                // check if sold dealer and free service dealer is the same
                if (soldDealer.ID == fsDealer.ID)
                {
                    CriteriaComposite critIsPDI = new CriteriaComposite(new Criteria(typeof(PDI), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                    critIsPDI.opAnd(new Criteria(typeof(PDI), "PDIStatus", MatchType.Exact, _enumBL.GetByCategoryAndCode("EnumFSStatus.FSStatus", "Selesai").ValueId));
                    critIsPDI.opAnd(new Criteria(typeof(PDI), "ChassisMaster.ID", MatchType.Exact, chassisMaster.ID));
                    ArrayList pdis = _pdiMapper.RetrieveByCriteria(critIsPDI);
                    if (pdis.Count == 0)
                    {
                        // klo sudah PDI boleh insert
                        validationResults.Add(new DNetValidationResult(string.Format(ValidationResource.ChassisMasterIDNoPDI, chassisMaster.ChassisNumber)));
                        return false;
                    }
                }
            }

            return isValid;
        }

        /// <summary>
        /// Validate fs kind
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="fsKind"></param>
        /// <param name="isFleetExists"></param>
        /// <returns></returns>
        private bool ValidateFSKindCode(FreeServiceParameterDto objCreate, List<DNetValidationResult> validationResults, ref FSKind fsKind, bool isFleetExists)
        {
            // validasi km ke 4 dan validasi jenis FS (FSKInd)
            CriteriaComposite critFS = new CriteriaComposite(new Criteria(typeof(FSKind), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            critFS.opAnd(new Criteria(typeof(FSKind), "KindCode", MatchType.Exact, objCreate.FSKindCode));

            ArrayList fsKinds = _fsKindMapper.RetrieveByCriteria(critFS);
            if (fsKinds.Count > 0)
            {
                fsKind = fsKinds[0] as FSKind;
                if (objCreate.MileAge > fsKind.KM && !isFleetExists)
                {
                    validationResults.Add(new DNetValidationResult(ValidationResource.MileAgeInvalid));
                }
            }
            else
            {
                validationResults.Add(new DNetValidationResult(ValidationResource.FSKindNotRegistered));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate fs kind bb
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="fsKind"></param>
        /// <returns></returns>
        private bool ValidateFSKindBB(FreeServiceParameterDto objCreate, List<DNetValidationResult> validationResults, ref FSKind fsKind)
        {
            // validasi km ke 4 dan validasi jenis FS (FSKInd)
            CriteriaComposite critFS = new CriteriaComposite(new Criteria(typeof(FSKind), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            critFS.opAnd(new Criteria(typeof(FSKind), "KindCode", MatchType.Exact, objCreate.FSKindCode));
            ArrayList fsKinds = _fsKindMapper.RetrieveByCriteria(critFS);
            if (fsKinds.Count > 0)
            {
                fsKind = fsKinds[0] as FSKind;
                if (objCreate.MileAge > fsKind.KM)
                {
                    validationResults.Add(new DNetValidationResult(ValidationResource.MileAgeInvalid));
                }
                else
                {
                    CriteriaComposite crtFS = new CriteriaComposite(new Criteria(typeof(FSKind), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                    crtFS.opAnd(new Criteria(typeof(FSKind), "KM", MatchType.Lesser, fsKind.KM));
                    SortCollection srtFS = new SortCollection();
                    srtFS.Add(new Sort(typeof(FSKind), "KM", Sort.SortDirection.DESC));

                    var arlFS = _fsKindMapper.RetrieveByCriteria(crtFS, srtFS);
                    int MinValue = 0;
                    if (arlFS.Count > 0)
                    {
                        MinValue = (arlFS[0] as FSKind).KM + 1;
                    }

                    if (objCreate.MileAge < MinValue)
                    {
                        validationResults.Add(new DNetValidationResult(ValidationResource.MileAgeNotMatchWithFSType));
                    }
                }
            }
            else
            {
                validationResults.Add(new DNetValidationResult(ValidationResource.FSKindNotRegistered));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate fleet
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="objFleetFaktur"></param>
        /// <returns></returns>
        private bool ValidateFleetFaktur(FreeServiceParameterDto objCreate, ref FleetFaktur objFleetFaktur)
        {
            CriteriaComposite critFleetFaktur = new CriteriaComposite(new Criteria(typeof(FleetFaktur), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            critFleetFaktur.opAnd(new Criteria(typeof(FleetFaktur), "ChassisMaster.ChassisNumber", MatchType.Exact, objCreate.ChassisNumber));
            ArrayList arrFleetFaktur = _fleetFakturMapper.RetrieveByCriteria(critFleetFaktur);
            if (arrFleetFaktur.Count > 0)
            {
                objFleetFaktur = arrFleetFaktur[0] as FleetFaktur;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check if FS allowed
        /// </summary>
        /// <param name="chassisMaster"></param>
        /// <param name="fsKind"></param>
        /// <param name="dealerCode"></param>
        /// <returns></returns>
        private bool IsAllowFreeService(ChassisMaster chassisMaster, FSKind fsKind, string dealerCode)
        {
            if (ChassisException(fsKind, chassisMaster.ChassisNumber) && IsAllowFSCampaign(fsKind, chassisMaster, dealerCode))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// CHassis exception list
        /// </summary>
        /// <param name="fsKind"></param>
        /// <param name="chassisNumber"></param>
        /// <returns></returns>
        private bool ChassisException(FSKind fsKind, string chassisNumber)
        {
            bool IsAllowInsert = true;
            // Start  :CR;by:dna;for:Rina;On:20100616;Remark:allow for specified CM
            if (fsKind.KindCode == "3" || fsKind.KindCode == "4" || fsKind.KindCode == "5" || fsKind.KindCode == "6" || fsKind.KindCode == "7")
            {
                string[] sForbiddenCMs = { "MHMFE71P1AK018514", "MHMFE73P2AK014642", "MHMFE73P2AK014643", "MHMFE73P2AK014715", "MHMFE73P2AK014760" };

                return !sForbiddenCMs.Any(x => x.ToUpper() == chassisNumber.ToUpper());
            }

            return IsAllowInsert;
            // End    :CR;by:dna;for:Rina;On:20100616;Remark:allow for specified CM
        }

        /// <summary>
        /// Validate if fs campaign allowed
        /// </summary>
        /// <param name="fsKind"></param>
        /// <param name="chassisMaster"></param>
        /// <param name="dealerCode"></param>
        /// <returns></returns>
        private bool IsAllowFSCampaign(FSKind fsKind, ChassisMaster chassisMaster, string dealerCode)
        {
            bool vReturn = false;
            FSChassisCampaign ObjFsChassisCampaign = new FSChassisCampaign();
            bool ObjIsByPass = false;
            CriteriaComposite criteriasFsChassisCampaign = new CriteriaComposite(new Criteria(typeof(KTB.DNet.Domain.FSChassisCampaign), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criteriasFsChassisCampaign.opAnd(new Criteria(typeof(KTB.DNet.Domain.FSChassisCampaign), "ChassisMaster.ChassisNumber", MatchType.Exact, chassisMaster.ChassisNumber));
            criteriasFsChassisCampaign.opAnd(new Criteria(typeof(KTB.DNet.Domain.FSChassisCampaign), "FSKind.ID", MatchType.Exact, fsKind.ID));
            ArrayList arrFsChassisCampaign = _fsChassisCampaignMapper.RetrieveByCriteria(criteriasFsChassisCampaign);
            ObjFsChassisCampaign.IsAllow = false;
            if (arrFsChassisCampaign.Count > 0)
            {
                ObjIsByPass = true;
                ObjFsChassisCampaign = ((FSChassisCampaign)(arrFsChassisCampaign[0]));
            }

            if (ObjIsByPass)
            {
                return ObjFsChassisCampaign.IsAllow;
            }

            // 'End of Bypass modul
            ArrayList arlFSCampaign = RetrieveFSCampaign();
            if ((arlFSCampaign.Count > 0))
            {
                foreach (FSCampaign objFSCampaign in arlFSCampaign)
                {
                    // Dealer checking
                    bool bDealer = true;
                    if (objFSCampaign.DealerChecked)
                    {
                        bDealer = false;
                        foreach (FSCampaignDealer objFSCampaignDealer in objFSCampaign.FSCampaignDealers)
                        {
                            if (objFSCampaignDealer.DealerCode == dealerCode)
                            {
                                bDealer = true;
                            }
                        }
                    }

                    // FSKind checking
                    bool bFSKind = true;
                    if (objFSCampaign.FSTypeChecked)
                    {
                        bFSKind = false;
                        foreach (FSCampaignKind objFSCampaignKind in objFSCampaign.FSCampaignKinds)
                        {
                            if (objFSCampaignKind.FSKind.KindCode == fsKind.KindCode)
                            {
                                bFSKind = true;
                            }
                        }
                    }

                    // VehicleType checking
                    bool bVehicle = true;
                    if (objFSCampaign.VehicleTypeChecked)
                    {
                        bVehicle = false;
                        foreach (FSCampaignVehicle objFSCampaignVehicle in objFSCampaign.FSCampaignVehicles)
                        {
                            if ((objFSCampaignVehicle.VechileType.ID == chassisMaster.VechileColor.VechileType.ID))
                            {
                                bVehicle = true;
                            }
                        }
                    }

                    // Faktur Validation checking
                    bool bFaktur = true;
                    if (objFSCampaign.FakturDateChecked)
                    {
                        bFaktur = false;
                        if (chassisMaster.EndCustomer != null)
                        {
                            if (objFSCampaign.DateFrom <= chassisMaster.EndCustomer.ValidateTime && objFSCampaign.DateTo >= chassisMaster.EndCustomer.ValidateTime)
                            {
                                bFaktur = true;
                            }
                        }
                        else
                        {
                            bFaktur = true;
                        }
                    }

                    // Combine value above
                    if (bDealer && (bFSKind && (bVehicle && bFaktur)))
                    {
                        vReturn = true;
                        break;
                    }
                }
            }

            return vReturn;
        }

        /// <summary>
        /// Get fs campaign
        /// </summary>
        /// <returns></returns>
        public ArrayList RetrieveFSCampaign()
        {
            ArrayList arlFSCampaign = new ArrayList();
            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(FSCampaign), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias.opAnd(new Criteria(typeof(FSCampaign), "Status", MatchType.Exact, 0));
            arlFSCampaign = _fsCampaignMapper.RetrieveByCriteria(criterias);
            return arlFSCampaign;
        }

        // added by anh 20111206 'sementara sebelum dibikin parameterise
        /// <summary>
        /// Check if chassis is allowed
        /// </summary>
        /// <param name="chassisNumber"></param>
        /// <returns></returns>
        public bool IsChassisAllowed(string chassisNumber)
        {
            string[] objType ={
            "MMBJNKB40AD026535",
            "MMBJNKB40AD033824",
            "MMBJNKB40AD042965",
            "MMBJNKB40AD043166",
            "MMBJNKB40AD020735",
            "MMBJNKB40AD030855",
            "MMBJNKB40AD030954",
            "MMBJNKB40AD038483",
            "MMBJNKB40AD038487",
            "MMBJNKB40AD038974"};

            return objType.Any(x => x == chassisNumber);
        }

        /// <summary>
        /// Validate the FS Kind to the Vehicle Type ID
        /// </summary>
        /// <param name="FSKindID"></param>
        /// <param name="vehicleTypeID"></param>
        /// <returns></returns>
        private bool ValidateFSKindOnVehicleType(int FSKindID, int vehicleTypeID)
        {
            try
            {
                CriteriaComposite critComp = new CriteriaComposite(new Criteria(typeof(FSKindOnVechileType), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                critComp.opAnd(new Criteria(typeof(FSKindOnVechileType), "FSKind.ID", MatchType.Exact, FSKindID));
                critComp.opAnd(new Criteria(typeof(FSKindOnVechileType), "VechileType.ID", MatchType.Exact, vehicleTypeID));

                return _fsKindOnVehicleTypeMapper.RetrieveByCriteria(critComp).Count > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Validate to prevent duplicate key
        /// </summary>
        /// <param name="ChassisID"></param>
        /// <param name="FSKindID"></param>
        /// <returns></returns>
        private bool IsExistCodeForInsert(ChassisMaster chassisMaster, int FSKindID, List<DNetValidationResult> validationResults)
        {
            // Periksa agar tidak ada key ganda 
            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(KTB.DNet.Domain.FreeService), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias.opAnd(new Criteria(typeof(KTB.DNet.Domain.FreeService), "ChassisMaster.ID", MatchType.Exact, chassisMaster.ID));
            criterias.opAnd(new Criteria(typeof(KTB.DNet.Domain.FreeService), "FSKind.ID", MatchType.Exact, FSKindID));
            if (_freeServiceMapper.RetrieveByCriteria(criterias).Count > 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(ValidationResource.ChassisMasterIDAndFsKindIDExist, chassisMaster.ChassisNumber, FSKindID)));
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate to prevent duplicate key
        /// </summary>
        /// <param name="ChassisID"></param>
        /// <param name="FSKindID"></param>
        /// <returns></returns>
        private bool ValidateFreeServiceBBDuplicate(ChassisMasterBB chassisMasterBB, int FSKindID, List<DNetValidationResult> validationResults)
        {
            // Periksa agar tidak ada key ganda 
            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(KTB.DNet.Domain.FreeServiceBB), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias.opAnd(new Criteria(typeof(KTB.DNet.Domain.FreeServiceBB), "ChassisMasterBB.ID", MatchType.Exact, chassisMasterBB.ID));
            criterias.opAnd(new Criteria(typeof(KTB.DNet.Domain.FreeServiceBB), "FSKind.ID", MatchType.Exact, FSKindID));
            if (_freeServiceBBMapper.RetrieveByCriteria(criterias).Count > 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(ValidationResource.ChassisMasterIDAndFsKindIDExist, chassisMasterBB.ChassisNumber, FSKindID)));
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate size uploaded file FS
        /// </summary>
        /// <param name="attachment"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidationCorruptFileFS(string filePath,List<DNetValidationResult> validationResults)
        {
            string user = AppConfigs.GetString("User");
            string password = AppConfigs.GetString("Password");
            string webServer = AppConfigs.GetString("WebServer");
            UserImpersonater imp = new UserImpersonater(user, password, webServer);
            var isvalid = true;

            try
            {
                // start it
                bool success = imp.Start();
                if (success)
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(filePath);
                    try
                    {
                        var minsize = 1024 * 50;
                        var maxsize = 1024 * 2000;
                        if (fi.Length < minsize)
                        {
                            isvalid = false;
                            validationResults.Add(new DNetValidationResult("Minimum File yang diupload adalah 50Kb"));
                        }
                        else if (fi.Length > maxsize)
                        {
                            isvalid = false;
                            validationResults.Add(new DNetValidationResult("Maximum File yang diupload adalah 2Mb"));
                        }
                        else
                        {
                            isvalid = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        isvalid = false;
                        validationResults.Add(new DNetValidationResult("File yang di upload Corrupt. Silahkan periksa dan upload file kembali."));
                    }
                    // stop 
                    imp.Stop();
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                imp.Dispose();
            }
            
            return isvalid;
        }

        private DateTime getDataSoldDate(int chassisMasterID)
        {
            DateTime SoldDate = new DateTime();
            try
            {
                ChassisMasterPKT cmPKT = new ChassisMasterPKT();
                var criterias = new CriteriaComposite(new Criteria(typeof(ChassisMasterPKT), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(ChassisMasterPKT), "ChassisMaster.ID", MatchType.Exact, chassisMasterID));
                var chassisMasterPKTData = _chassisMasterPKTMapper.RetrieveByCriteria(criterias);
                if (chassisMasterPKTData != null)
                {
                    cmPKT = chassisMasterPKTData[0] as ChassisMasterPKT;
                    SoldDate = cmPKT.PKTDate;
                }
                else
                {
                    SoldDate = Convert.ToDateTime("1900-01-01");
                }
            }
            catch (Exception ex)
            {
                SoldDate = Convert.ToDateTime("1900-01-01");
            }
            return SoldDate;
        }

        #endregion
    }
}

