#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKDetail business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region "Namespace Imports"
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
using System.Linq;
using System.Runtime.ExceptionServices;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class SPKDetailBL : AbstractBusinessLogic, ISPKDetailBL
    {
        # region Variables
        private readonly IMapper _spkDetailMapper;
        private readonly IMapper _vehicleTypeMapper;
        private readonly IMapper _vehicleColorMapper;
        private readonly IMapper _categoryMapper;
        private readonly IMapper _profileHeaderMapper;
        private readonly IMapper _profileDetailMapper;
        private readonly IMapper _profileGroupMapper;
        private readonly IMapper _spkProfileMapper;
        private readonly IMapper _spkHeaderMapper;
        private readonly IMapper _statusChHistMapper;
        private readonly IMapper _spkFakturMapper;
        private readonly IMapper _revisionSpkFakturMapper;
        private readonly IMapper _vehicleKindMapper;
        private readonly IMapper _vehicleKindGroupMapper;
        private readonly IMapper _karoseriMapper;
        private readonly IMapper _leasingMapper;
        private readonly AutoMapper.IMapper _mapper;
        private TransactionManager _transactionManager;
        private StandardCodeBL _enumBL;
        # endregion

        # region Constructor
        public SPKDetailBL()
        {
            _spkDetailMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKDetail).ToString());
            _vehicleColorMapper = MapperFactory.GetInstance().GetMapper(typeof(VechileColor).ToString());
            _vehicleTypeMapper = MapperFactory.GetInstance().GetMapper(typeof(VechileType).ToString());
            _categoryMapper = MapperFactory.GetInstance().GetMapper(typeof(Category).ToString());
            _profileHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(ProfileHeader).ToString());
            _profileDetailMapper = MapperFactory.GetInstance().GetMapper(typeof(ProfileDetail).ToString());
            _profileGroupMapper = MapperFactory.GetInstance().GetMapper(typeof(ProfileGroup).ToString());
            _spkProfileMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKProfile).ToString());
            _spkHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKHeader).ToString());
            _statusChHistMapper = MapperFactory.GetInstance().GetMapper(typeof(StatusChangeHistory).ToString());
            _spkFakturMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKFaktur).ToString());
            _revisionSpkFakturMapper = MapperFactory.GetInstance().GetMapper(typeof(RevisionSPKFaktur).ToString());
            _vehicleKindMapper = MapperFactory.GetInstance().GetMapper(typeof(VehicleKind).ToString());
            _vehicleKindGroupMapper = MapperFactory.GetInstance().GetMapper(typeof(VehicleKindGroup).ToString());
            _karoseriMapper = MapperFactory.GetInstance().GetMapper(typeof(Karoseri).ToString());
            _leasingMapper = MapperFactory.GetInstance().GetMapper(typeof(Leasing).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _enumBL = new StandardCodeBL(_mapper);
            _transactionManager = new TransactionManager();
            _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);
        }

        public SPKDetailBL(AutoMapper.IMapper mapper)
        {
            _spkDetailMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKDetail).ToString());
            _vehicleColorMapper = MapperFactory.GetInstance().GetMapper(typeof(VechileColor).ToString());
            _vehicleTypeMapper = MapperFactory.GetInstance().GetMapper(typeof(VechileType).ToString());
            _categoryMapper = MapperFactory.GetInstance().GetMapper(typeof(Category).ToString());
            _profileHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(ProfileHeader).ToString());
            _profileDetailMapper = MapperFactory.GetInstance().GetMapper(typeof(ProfileDetail).ToString());
            _profileGroupMapper = MapperFactory.GetInstance().GetMapper(typeof(ProfileGroup).ToString());
            _spkProfileMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKProfile).ToString());
            _spkHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKHeader).ToString());
            _statusChHistMapper = MapperFactory.GetInstance().GetMapper(typeof(StatusChangeHistory).ToString());
            _spkFakturMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKFaktur).ToString());
            _revisionSpkFakturMapper = MapperFactory.GetInstance().GetMapper(typeof(RevisionSPKFaktur).ToString());
            _vehicleKindMapper = MapperFactory.GetInstance().GetMapper(typeof(VehicleKind).ToString());
            _vehicleKindGroupMapper = MapperFactory.GetInstance().GetMapper(typeof(VehicleKindGroup).ToString());
            _karoseriMapper = MapperFactory.GetInstance().GetMapper(typeof(Karoseri).ToString());
            _leasingMapper = MapperFactory.GetInstance().GetMapper(typeof(Leasing).ToString());
            _mapper = mapper;
            _enumBL = new StandardCodeBL(mapper);
            _transactionManager = new TransactionManager();
            _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);
        }
        # endregion

        #region Public Methods
        /// <summary>
        /// Create a new SPKDetail
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<SPKDetailDto> Create(SPKDetailParameterDto objCreate)
        {
            objCreate.ID = 0;
            var result = new ResponseBase<SPKDetailDto>();
            var validationResults = new List<DNetValidationResult>();
            SPKDetail spkDetail;
            string msgSPKDetailCantUpdate = string.Empty;

            try
            {
                // get the enum
                int rowStatusActiveCode = (int)_enumBL.GetByCategoryAndCode("DBRowStatus", "Active").ValueId;
                List<StandardCodeDto> listOfDBRowStatus = _enumBL.GetByCategory("DBRowStatus");
                List<StandardCodeDto> listOfSPKStatus = _enumBL.GetByCategory("EnumStatusSPK.Status");

                // validate
                validationResults.AddRange(SetSPKDetailFromParameterDto(objCreate, out spkDetail, rowStatusActiveCode, listOfDBRowStatus, listOfSPKStatus, out msgSPKDetailCantUpdate));

                if (validationResults.Count == 0)
                {
                    int insertedID = InsertWithTransactionManager(spkDetail);
                    if (insertedID > 0)
                    {
                        result.success = true;
                        result._id = insertedID;
                        result.lst = _mapper.Map<SPKDetailDto>(spkDetail);
                        result.total = 1;
                    }
                    else
                    {
                        ErrorMsgHelper.DataCorrupt(result.messages);
                    }
                }
                else
                {
                    return PopulateValidationError<SPKDetailDto>(validationResults, null);
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
        /// Update SPKDetail
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<SPKDetailDto> Update(SPKDetailParameterDto objUpdate)
        {
            var result = new ResponseBase<SPKDetailDto>();
            var validationResults = new List<DNetValidationResult>();
            string msgSPKDetailCantUpdate = string.Empty;

            try
            {
                // check if the data is available on db
                SPKDetail spkDetailOnDB = (SPKDetail)_spkDetailMapper.Retrieve(objUpdate.ID);
                if (spkDetailOnDB == null)
                {
                    result.messages.Add(new MessageBase { ErrorCode = ErrorCode.DataUpdateNotAvailable, ErrorMessage = MessageResource.ErrorMsgDataUpdateNotAvailable });
                    return result;
                }

                SPKDetail spkDetail;
                int rowStatusActiveCode = (int)_enumBL.GetByCategoryAndCode("DBRowStatus", "Active").ValueId;
                List<StandardCodeDto> listOfDBRowStatus = _enumBL.GetByCategory("DBRowStatus");
                List<StandardCodeDto> listOfSPKStatus = _enumBL.GetByCategory("EnumStatusSPK.Status");
                validationResults.AddRange(SetSPKDetailFromParameterDto(objUpdate, out spkDetail, rowStatusActiveCode, listOfDBRowStatus, listOfSPKStatus, out msgSPKDetailCantUpdate));

                ArrayList existingProfiles;
                ArrayList newProfiles;
                validationResults.AddRange(SetSPKProfiles(objUpdate, out existingProfiles, out newProfiles));

                if (validationResults.Count == 0)
                {
                    int insertedID = UpdateWithTransactionManager(spkDetail, existingProfiles, newProfiles);
                    result.success = true;
                    result.lst = _mapper.Map<SPKDetailDto>(spkDetail);
                    result._id = insertedID;
                    result.total = 1;
                }
                else
                {
                    return PopulateValidationError<SPKDetailDto>(validationResults, null);
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
        /// Cancel SPK Detail
        /// </summary>
        /// <param name="spkDetailParam"></param>
        /// <returns></returns>
        public ResponseBase<SPKDetailDto> Cancel(SPKDetailParameterDto spkDetailParam)
        {
            var result = new ResponseBase<SPKDetailDto>();

            try
            {
                spkDetailParam.Status = (short)_enumBL.GetByCategoryAndCode("DBRowStatus", "Canceled").ValueId;

                List<DNetValidationResult> validationResults = ValidateCanceledSPKDetail(spkDetailParam);

                if (validationResults.Any())
                {
                    result.messages.Add(new MessageBase
                    {
                        ErrorCode = ErrorCode.DataRequiredField,
                        ErrorMessage = validationResults[0].ErrorMessage
                    });
                    return result;
                }

                var spkdetail = (SPKDetail)_spkDetailMapper.Retrieve(spkDetailParam.ID);

                if (spkdetail != null)
                {
                    spkdetail.RejectedReason = spkDetailParam.RejectedReason;
                    spkdetail.Status = (byte)spkDetailParam.Status;

                    var nResult = _spkDetailMapper.Update(spkdetail, DNetUserName);
                    if (nResult != 0)
                    {
                        result.success = true;
                        // return output ID
                        result.lst = _mapper.Map<SPKDetailDto>(spkdetail);
                        result._id = spkDetailParam.ID;
                        result.total = 1;
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
        /// Delete SPKDetail by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<SPKDetailDto> Delete(int id)
        {
            var result = new ResponseBase<SPKDetailDto>();

            try
            {
                var spkdetail = (SPKDetail)_spkDetailMapper.Retrieve(id);
                if (spkdetail != null)
                {
                    spkdetail.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _spkDetailMapper.Update(spkdetail, DNetUserName);
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
        /// Get SPKDetail by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<SPKDetailDto>> Read(SPKDetailFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(SPKDetail), "RowStatus", MatchType.Exact, (short)_enumBL.GetByCategoryAndCode("DBRowStatus", "Active").ValueId));
            var result = new ResponseBase<List<SPKDetailDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(SPKDetail), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(SPKDetail), filterDto, sortColl);

                // get data
                var data = _spkDetailMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<SPKDetail>().ToList();
                    var listData = list.Select(item => _mapper.Map<SPKDetailDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SPKDetail), filterDto);
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
        /// Set SPK Detail parameter
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="spkDetail"></param>
        /// <param name="rowStatusActiveCode"></param>
        /// <param name="listOfSPKStatus"></param>
        /// <param name="listOfDBRowStatus"></param>
        /// <param name="isNewSPK"></param>
        /// <returns></returns>
        public List<DNetValidationResult> SetSPKDetailFromParameterDto(SPKDetailParameterDto objCreate, out SPKDetail spkDetail, int rowStatusActiveCode, List<StandardCodeDto> listOfDBRowStatus, List<StandardCodeDto> listOfSPKStatus, out string msgSPKDetailCantUpdate, bool isNewSPK = false)
        {
            List<DNetValidationResult> validationResults = new List<DNetValidationResult>();
            bool allowToUpdateDetail = true;
            List<SPKProfile> listOfSPKProfile = new List<SPKProfile>();
            spkDetail = new SPKDetail();
            SPKDetail spkDetailOnDB = null;
            bool isUpdate = objCreate.ID != 0;
            msgSPKDetailCantUpdate = string.Empty;

            // validate 
            ValidateAdditionalAndDBRowStatus(objCreate, listOfDBRowStatus, validationResults);

            // if it is an update process
            if (isUpdate)
            {
                spkDetailOnDB = (SPKDetail)_spkDetailMapper.Retrieve(objCreate.ID);
                if (spkDetailOnDB == null)
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataNotFound, FieldResource.SPKDetail + " " + FieldResource.SPKHeaderID)));
                    return validationResults;
                }
                else
                {
                    if (spkDetailOnDB.SPKHeader.ID != objCreate.SPKHeaderID)
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.SPKHeaderID)));
                        return validationResults;
                    }

                    objCreate.CreatedBy = spkDetailOnDB.CreatedBy;
                    if (!AllowToUpdateSPKDetail(spkDetailOnDB))
                    {
                        spkDetail = spkDetailOnDB;
                        return validationResults;
                    }
                }

                // validate status change history
                ValidateStatusChangeHistory(objCreate, rowStatusActiveCode, listOfSPKStatus, validationResults, spkDetailOnDB);
                if (validationResults.Count > 0)
                {
                    return validationResults;
                }               
                    
            }

            Category category = null;
            ArrayList listOfCategory = _categoryMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(Category), "CategoryCode", objCreate.CategoryCode));
            if (listOfCategory.Count > 0)
            {
                category = listOfCategory[0] as Category;
            }

            if (category == null)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.Category)));
            }
            else
            {
                spkDetail = _mapper.Map<SPKDetail>(objCreate);

                // update the other properties
                if (ValidateSPKDetailItem(objCreate.VehicleTypeCode, objCreate.VehicleColorCode, objCreate.Quantity, validationResults))
                {
                    // get vehicle type
                    VechileType vehicleType = null;
                    ArrayList listOfVehicleType = _vehicleTypeMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(VechileType), "VechileTypeCode", objCreate.VehicleTypeCode));
                    if (listOfVehicleType.Count > 0)
                    {
                        vehicleType = listOfVehicleType[0] as VechileType;
                    }

                    // category id for MMKSI = 1, 2
                    // category id for KTB = 3
                    if (!(category.ID == 1 || category.ID == 2))
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.Category)));
                        return validationResults;
                    }

                    if (!(vehicleType.Category.ID == 1 || vehicleType.Category.ID == 2))
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgVehicleTypeCategoryInvalid, vehicleType.Category.ID, objCreate.VehicleTypeCode)));
                        return validationResults;
                    }

                    if (vehicleType.Category.CategoryCode != category.CategoryCode)
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgVehicleTypeCodeCategoryInvalid, category.CategoryCode, vehicleType.Category.CategoryCode, objCreate.VehicleTypeCode)));
                        return validationResults;
                    }

                    if (category.CategoryCode == "PC" && !string.IsNullOrEmpty(objCreate.ProfileDetailCode))
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ProfileDetail)));
                        return validationResults;
                    }

                    VehicleKindGroup vehicleKindGroup = null;
                    string vehicleKindInput = string.Empty;
                    bool isThereAnyVehicleKindInput = false;

                    ResponseBase<ProfileGroupDto> profGroupResponse = GetProfileGroupByCategory(category.CategoryCode);
                    if (profGroupResponse.success)
                    {
                        List<StandardCodeDto> enumMandatoryMode = _enumBL.GetByCategory("EnumMandatory.MandatoryMode");
                        short mandatoryFlag = (short)enumMandatoryMode.Where(s => s.ValueCode == "Benar").SingleOrDefault().ValueId;
                        List<VehicleKindGroup> listOfVehicleKindGroup = GetListOfVehicleKindGroup(vehicleType);

                        ValidateProfileGroup(objCreate, spkDetail, validationResults, listOfSPKProfile, mandatoryFlag, listOfVehicleKindGroup, ref vehicleKindGroup, ref vehicleKindInput, ref isThereAnyVehicleKindInput, profGroupResponse);
                    }

                    // check if there's any vehicle kind input (CBU_MODELKEND)
                    // it should be listed on vehicle kind group (CBU_JENISKEND)
                    if (isThereAnyVehicleKindInput)
                    {
                        if (vehicleKindGroup != null)
                        {
                            if (vehicleKindGroup.ID != spkDetail.VehicleKind.VehicleKindGroup.ID)
                            {
                                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgInvalidProfileHeaderValue, FieldResource.VehicleKind, vehicleKindInput)));
                            }
                        }
                        else
                        {
                            validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgInvalidProfileHeaderValue, FieldResource.VehicleKind, vehicleKindInput)));
                        }
                    }

                    if (!isNewSPK)
                    {
                        SPKHeader spkHeader = (SPKHeader)_spkHeaderMapper.Retrieve(objCreate.SPKHeaderID);
                        if (spkHeader != null)
                        {
                            spkDetail.SPKHeader = spkHeader;
                        }
                        else
                        {
                            validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.SPKHeaderIDOnSPKDetail)));
                        }
                    }

                    ArrayList vehicleColors = GetVehicleColors(objCreate);
                    if (vehicleColors.Count > 0)
                    {
                        spkDetail.VechileColor = (VechileColor)vehicleColors[0];
                        spkDetail.VehicleColorName = spkDetail.VechileColor.ColorEngName;
                    }
                    else
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.VehicleColorCode + " " + objCreate.VehicleColorCode)));
                    }

                    #region Not implemented on existing KTB
                    // if it is an update process
                    // move to spkbl impacted by cr spk faktur 20201104 
                    // spk detail vehicletype on spkdetail separate to each row and qty = 1
                    if (objCreate.ID != 0)
                    {
                        ArrayList listOfFaktur = GetSPKFaktur(objCreate.SPKHeaderID);
                        if (listOfFaktur != null && listOfFaktur.Count > 0)
                        {
                            // if have spk faktur
                            //int iQtyAssigned = 0;
                            foreach (SPKFaktur faktur in listOfFaktur)
                            {
                                if (faktur.EndCustomer != null && faktur.EndCustomer.ChassisMaster != null)
                                {
                                    var customerReqCode = string.Empty;
                                    SPKDetailCustomer sPKDetailCustomerOnDB = (SPKDetailCustomer)spkDetailOnDB.SPKDetailCustomers[0];
                                    if(sPKDetailCustomerOnDB.CustomerRequest != null)
                                    {
                                        customerReqCode = sPKDetailCustomerOnDB.CustomerRequest.CustomerCode;
                                    }

                                    if (spkDetailOnDB.VechileColor.ID == faktur.EndCustomer.ChassisMaster.VechileColor.ID &&
                                        spkDetailOnDB.VehicleColorCode == faktur.EndCustomer.ChassisMaster.VechileColor.ColorCode && faktur.EndCustomer.Customer.Code == customerReqCode)
                                    {
                                        allowToUpdateDetail = false;
                                        msgSPKDetailCantUpdate = "SPK Detail tidak dapat diubah karena sudah memiliki Faktur";
                                    }

                                    // validation qty moved to header impacted CR SPKFaktur 202011
                                    //if (!string.IsNullOrEmpty(faktur.EndCustomer.ChassisMaster.VechileType) &&
                                    //    objCreate.VehicleTypeCode == faktur.EndCustomer.ChassisMaster.VechileType)
                                    //{
                                    //    iQtyAssigned = iQtyAssigned + 1;
                                    //}
                                }
                            }

                            // validation qty moved to header impacted CR SPKFaktur 202011
                            //if (iQtyAssigned > 0 && objCreate.Quantity < iQtyAssigned)
                            //{
                            //    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgInvalidQuantityBasedOnFactur, objCreate.VehicleTypeCode, objCreate.VehicleColorCode, iQtyAssigned.ToString())));
                            //    return validationResults;
                            //}
                        }
                        else 
                        {
                            // if have revision spk faktur
                            ArrayList listOfRevisionSPKFaktur = GetRevisionSPKFaktur(objCreate.SPKHeaderID);
                            if (listOfRevisionSPKFaktur != null && listOfRevisionSPKFaktur.Count > 0)
                            {
                                allowToUpdateDetail = false;
                                msgSPKDetailCantUpdate = "SPK Detail tidak dapat diubah karena sudah memiliki Revisi Faktur";
                            }
                        }
                    }
                    #endregion

                    if (allowToUpdateDetail)
                    {
                        spkDetail.TotalAmount = spkDetail.Quantity * spkDetail.Amount;
                        spkDetail.Category = category;
                        spkDetail.CreatedTime = DateTime.Now;
                        spkDetail.LastUpdateTime = DateTime.Now;
                        spkDetail.SPKProfiles.AddRange(listOfSPKProfile);

                    }
                    else
                    {
                        spkDetail = spkDetailOnDB;
                    }
                }
            }

            return validationResults;
        }


        /// <summary>
        /// Allow to update spk detail
        /// </summary>
        /// <param name="spkDetailOnDB"></param>        
        /// <returns></returns>
        public bool AllowToUpdateSPKDetail(SPKDetail spkDetailOnDB)
        {
            int canceledStatus = _enumBL.GetByCategoryAndCode("DBRowStatus", "Canceled").ValueId;
            int SPKCanceledStatus = _enumBL.GetByCategoryAndCode("EnumStatusSPK.Status", "Batal").ValueId;

            if (spkDetailOnDB.Status == SPKCanceledStatus || spkDetailOnDB.Status == canceledStatus)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate spk profiles
        /// </summary>
        /// <param name="spkDetailParam"></param>
        /// <param name="existingProfiles"></param>
        /// <param name="newProfiles"></param>
        /// <returns></returns>
        public List<DNetValidationResult> SetSPKProfiles(SPKDetailParameterDto spkDetailParam, out ArrayList existingProfiles, out ArrayList newProfiles)
        {
            existingProfiles = new ArrayList();
            newProfiles = new ArrayList();

            List<DNetValidationResult> validationResults = new List<DNetValidationResult>();
            Category cat = null;
            ArrayList listOfCategory = _categoryMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(Category), "CategoryCode", spkDetailParam.CategoryCode));
            if (listOfCategory.Count > 0)
            {
                cat = listOfCategory[0] as Category;
            }

            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.Category)));
            }

            var categoryCode = cat.CategoryCode;
            ResponseBase<ProfileGroupDto> profGroupResponse = GetProfileGroupByCategory(categoryCode);

            if (profGroupResponse.success)
            {
                foreach (var group in profGroupResponse.lst.ProfileHeaderToGroups)
                {
                    //SPKProfile spkProfile;
                    bool isExist = true;
                    SPKProfile spkProfile = GetSPKProfileByProfileHeader(spkDetailParam.ID, group.ProfileGroup.ID, group.ProfileHeader.ID);
                    if (spkProfile == null)
                    {
                        isExist = false;
                        spkProfile = new SPKProfile();
                    }
                    spkProfile.ProfileGroup = _mapper.Map<ProfileGroup>(group.ProfileGroup);
                    spkProfile.ProfileHeader = _mapper.Map<ProfileHeader>(group.ProfileHeader);

                    // spkProfile.SPKDetail = spkDetail;
                    dynamic value;
                    var prop = spkDetailParam.GetType().GetProperty(group.ProfileHeader.Code);

                    if (prop != null)
                    {
                        value = prop.GetValue(spkDetailParam, null);
                        spkProfile.ProfileValue = value;
                    }

                    spkProfile.CreatedTime = DateTime.Now;
                    spkProfile.LastUpdateTime = DateTime.Now;

                    if (isExist) { existingProfiles.Add(spkProfile); }
                    else { newProfiles.Add(spkProfile); }
                }
            }

            return validationResults;
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Validate additional and db row status
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="listOfDBRowStatus"></param>
        /// <param name="validationResults"></param>
        private void ValidateAdditionalAndDBRowStatus(SPKDetailParameterDto objCreate, List<StandardCodeDto> listOfDBRowStatus, List<DNetValidationResult> validationResults)
        {
            List<StandardCodeDto> listOfAdditional = _enumBL.GetByCategory("EnumSPKAdditional.SPKAdditionalParts");
            if (listOfAdditional.Any(add => add.ValueId == objCreate.Additional))
            {
                if (objCreate.Additional != 2 && string.IsNullOrEmpty(objCreate.Remarks))
                {
                    validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.Remarks)));
                }
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.Additional)));
            }

            if (listOfDBRowStatus.Any(dbStatus => dbStatus.ValueId == (int)objCreate.Status))
            {
                validationResults.AddRange(ValidateCanceledSPKDetail(objCreate));
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.SPKDetailStatus)));
            }
        }

        /// <summary>
        /// Validate Profile Group
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="spkDetail"></param>
        /// <param name="validationResults"></param>
        /// <param name="listOfSPKProfile"></param>
        /// <param name="mandatoryFlag"></param>
        /// <param name="listOfVehicleKindGroup"></param>
        /// <param name="vehicleKindGroup"></param>
        /// <param name="vehicleKindInput"></param>
        /// <param name="isThereAnyVehicleKindInput"></param>
        /// <param name="profGroupResponse"></param>
        private void ValidateProfileGroup(SPKDetailParameterDto objCreate, SPKDetail spkDetail, List<DNetValidationResult> validationResults, List<SPKProfile> listOfSPKProfile, short mandatoryFlag, List<VehicleKindGroup> listOfVehicleKindGroup, ref VehicleKindGroup vehicleKindGroup, ref string vehicleKindInput, ref bool isThereAnyVehicleKindInput, ResponseBase<ProfileGroupDto> profGroupResponse)
        {
            int vehicleKindGroupID = 0;
            foreach (var group in profGroupResponse.lst.ProfileHeaderToGroups)
            {
                dynamic value;

                var prop = objCreate.GetType().GetProperty(group.ProfileHeader.Code);
                if (prop != null)
                {
                    value = prop.GetValue(objCreate, null);
                    if (group.ProfileHeader.Mandatory == mandatoryFlag && string.IsNullOrEmpty(value))
                    {
                        validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.ResourceManager.GetString(group.ProfileHeader.Code))));
                        continue;
                    }

                    if (!string.IsNullOrEmpty((string)value))
                    {
                        if (group.ProfileHeader.Code == "CBU_WAYPAID1" && ((string)value).Trim().ToUpper() == "T" && !string.IsNullOrEmpty(objCreate.CBU_LEASING))
                        {
                            validationResults.Add(new DNetValidationResult(MessageResource.ErrorMsgInvalidLeasingWithPaidTunai));
                        }

                        // for CBU_JENISKEND and CBU_MODELKEND, set the value id, not the code
                        if (group.ProfileHeader.Code == "CBU_JENISKEND" || group.ProfileHeader.Code == "CBU_MODELKEND")
                        {
                            if (group.ProfileHeader.Code == "CBU_MODELKEND")
                            {
                                // validate CBU_MODELKEND value
                                // and set SPKDetail.VehicleKind with CBU_MODELKend value
                                var crt = new CriteriaComposite(new Criteria(typeof(VehicleKind), "RowStatus", MatchType.Exact, (int)DBRowStatus.Active));
                                crt.opAnd(new Criteria(typeof(VehicleKind), "Code", MatchType.Exact, (string)value));
                                crt.opAnd(new Criteria(typeof(VehicleKind), "VehicleKindGroup.ID", MatchType.Exact, (int)vehicleKindGroupID));
                                ArrayList listOfVehicleKind = _vehicleKindMapper.RetrieveByCriteria(crt);
                                if (listOfVehicleKind != null && listOfVehicleKind.Count > 0)
                                {
                                    spkDetail.VehicleKind = listOfVehicleKind[0] as VehicleKind;
                                    vehicleKindInput = value;
                                    value = spkDetail.VehicleKind.ID.ToString();
                                    isThereAnyVehicleKindInput = true;
                                }
                                else
                                {
                                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgInvalidProfileHeaderValue, group.ProfileHeader.Code, value)));
                                }
                            }
                            else
                            {
                                // for CBU_JENISKEND
                                List<VehicleKindGroup> listOfMatchedVehicleKindGroup = listOfVehicleKindGroup.Where(vGroup => string.Equals(vGroup.Code.Trim(), (string)value.Trim(), StringComparison.InvariantCultureIgnoreCase)).ToList();
                                if (listOfMatchedVehicleKindGroup != null && listOfMatchedVehicleKindGroup.Count > 0)
                                {
                                    vehicleKindGroup = listOfMatchedVehicleKindGroup[0];
                                    value = listOfMatchedVehicleKindGroup[0].ID.ToString();

                                    // fill vehkindgroupid 20201030 req by septia
                                    vehicleKindGroupID = vehicleKindGroup.ID;
                                }
                                else
                                {
                                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgInvalidProfileHeaderValue, group.ProfileHeader.Code, value)));
                                }
                            }
                        }
                        else
                        {
                            // check the posible value from profileDetail
                            if (group.ProfileHeader.ControlType == 2)
                            {
                                ValidateProfileHeader(objCreate, spkDetail, validationResults, group, value);
                            }
                        }
                    }

                    KTB.DNet.Domain.SPKProfile profile = GetValidSPKCustomerProfile(spkDetail.ID, group.ProfileGroup.ID, group.ProfileHeader.ID, value);
                    profile.SPKDetail = spkDetail;
                    listOfSPKProfile.Add(profile);
                }
            }
        }

        /// <summary>
        /// Validate status change history
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="rowStatusActiveCode"></param>
        /// <param name="listOfSPKStatus"></param>
        /// <param name="validationResults"></param>
        /// <param name="spkDetailOnDB"></param>
        private void ValidateStatusChangeHistory(SPKDetailParameterDto objCreate, int rowStatusActiveCode, List<StandardCodeDto> listOfSPKStatus, List<DNetValidationResult> validationResults, SPKDetail spkDetailOnDB)
        {
            #region Not implemented on existing KTB

            List<string> listOfSPKTungguUnitStatusCode = listOfSPKStatus.Where(s => s.ValueCode.ToUpper().Contains("TUNGGU")).Select(s => s.ValueId.ToString()).ToList();

            CriteriaComposite criteriasSCH = new CriteriaComposite(new Criteria(typeof(StatusChangeHistory), "RowStatus", MatchType.Exact, rowStatusActiveCode));
            criteriasSCH.opAnd(new Criteria(typeof(StatusChangeHistory), "DocumentType", MatchType.Exact, (int)_enumBL.GetByCategoryAndCode("LookUp.DocumentType", "Surat_Pesanan_Kendaraan").ValueId));
            criteriasSCH.opAnd(new Criteria(typeof(StatusChangeHistory), "DocumentRegNumber", MatchType.Exact, spkDetailOnDB.SPKHeader.SPKNumber));
            criteriasSCH.opAnd(new Criteria(typeof(StatusChangeHistory), "OldStatus", MatchType.InSet, string.Format("({0})", string.Join(",", listOfSPKTungguUnitStatusCode))));
            criteriasSCH.opAnd(new Criteria(typeof(StatusChangeHistory), "NewStatus", MatchType.InSet, string.Format("({0})", string.Join(",", listOfSPKTungguUnitStatusCode))));

            ArrayList listOfChangeHistoryStatus = _statusChHistMapper.RetrieveByCriteria(criteriasSCH);
            if (listOfChangeHistoryStatus.Count > 0)
            {
                CriteriaComposite criteriasVT2 = new CriteriaComposite(new Criteria(typeof(VechileType), "RowStatus", MatchType.Exact, rowStatusActiveCode));
                criteriasVT2.opAnd(new Criteria(typeof(VechileType), "Status", MatchType.No, "X"));
                criteriasVT2.opAnd(new Criteria(typeof(VechileType), "VechileTypeCode", MatchType.Exact, objCreate.VehicleTypeCode));

                ArrayList objVechileType = _vehicleTypeMapper.RetrieveByCriteria(criteriasVT2);
                if (objVechileType.Count > 0)
                {
                    CriteriaComposite criteriasVT = new CriteriaComposite(new Criteria(typeof(VechileType), "RowStatus", MatchType.Exact, rowStatusActiveCode));
                    criteriasVT.opAnd(new Criteria(typeof(VechileType), "Status", MatchType.No, "X"));
                    criteriasVT.opAnd(new Criteria(typeof(VechileType), "VechileModel.ID", MatchType.Exact, ((VechileType)objVechileType[0]).VechileModel.ID));
                    criteriasVT.opAnd(new Criteria(typeof(VechileType), "VechileTypeCode", MatchType.Exact, objCreate.VehicleTypeCode.Trim().ToUpper()));

                    ArrayList ArrVehicleType = _vehicleTypeMapper.RetrieveByCriteria(criteriasVT);
                    if (ArrVehicleType.Count == 0)
                    {
                        validationResults.Add(new DNetValidationResult(MessageResource.ErrorMsgVehicleTypeIsNotMatchWithPreviousData));
                    }
                }
            }
            #endregion
        }


        /// <summary>
        /// Get vehicle colors
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        private ArrayList GetVehicleColors(SPKDetailParameterDto objCreate)
        {
            ArrayList vehicleColors = new ArrayList();
            if (objCreate.VehicleColorCode != "ZZZZ")
            {
                CriteriaComposite criteriaColor = new CriteriaComposite(new Criteria(typeof(VechileColor), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criteriaColor.opAnd(new Criteria(typeof(VechileColor), "ColorCode", MatchType.Exact, objCreate.VehicleColorCode));
                criteriaColor.opAnd(new Criteria(typeof(VechileColor), "VechileType.VechileTypeCode", MatchType.Exact, objCreate.VehicleTypeCode));
                vehicleColors = _vehicleColorMapper.RetrieveByCriteria(criteriaColor);
            }
            else
            {
                vehicleColors = _vehicleColorMapper.RetrieveByCriteria(Helper.GetCodeCriteria(typeof(VechileColor), "ColorCode", "ZZZZ"));
            }

            return vehicleColors;
        }

        /// <summary>
        /// Validate Profile Header
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="spkDetail"></param>
        /// <param name="validationResults"></param>
        /// <param name="group"></param>
        /// <param name="value"></param>
        private void ValidateProfileHeader(SPKDetailParameterDto objCreate, SPKDetail spkDetail, List<DNetValidationResult> validationResults, ProfileHeaderToGroupDto group, dynamic value)
        {
            if (group.ProfileHeader.Code == "CBU_LEASING")
            {
                ArrayList arrayOfLeasing = _leasingMapper.RetrieveByCriteria(Helper.GetCodeCriteria(typeof(Leasing), "LeasingCode", ((string)value).Trim().ToUpper()));
                if (arrayOfLeasing.Count == 0)
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgInvalidProfileHeaderValue, group.ProfileHeader.Code, value)));
                }
            }
            else if (group.ProfileHeader.Code == "CBU_CARROSSERIE")
            {
                ArrayList arrayOfKaroseri = _karoseriMapper.RetrieveByCriteria(Helper.GetCodeCriteria(typeof(Karoseri), "Code", ((string)value).Trim().ToUpper()));
                if (arrayOfKaroseri.Count == 0)
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgInvalidProfileHeaderValue, group.ProfileHeader.Code, value)));
                }
            }
            else
            {
                ArrayList arrayOfProfileDetail = _profileDetailMapper.RetrieveByCriteria(Helper.GetCodeCriteria(typeof(ProfileDetail), "ProfileHeader.ID", group.ProfileHeader.ID.ToString()));
                if (arrayOfProfileDetail.Count > 0)
                {
                    List<ProfileDetail> listOfProfileDetail = arrayOfProfileDetail.Cast<ProfileDetail>().ToList();
                    List<ProfileDetail> listOfMatchedProfileDetail = listOfProfileDetail.Where(p => p.Code.Trim().ToUpper() == ((string)value).Trim().ToUpper()).ToList();
                    if (!(listOfMatchedProfileDetail.Count > 0))
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgInvalidProfileHeaderValue, group.ProfileHeader.Code, value)));
                    }
                    else
                    {
                        if (group.ProfileHeader.Code == Constants.ProfileHeader.BodyTypeCV || group.ProfileHeader.Code == Constants.ProfileHeader.BodyTypeLCV)
                        {
                            // check SPKDetail.ProfileDetailCode is exist on list of profile detail
                            if (string.IsNullOrEmpty(objCreate.ProfileDetailCode))
                            {
                                validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.ProfileDetail)));
                            }
                            else
                            {
                                listOfMatchedProfileDetail = listOfProfileDetail.Where(p => p.Code.Trim().ToUpper() == objCreate.ProfileDetailCode.Trim().ToUpper()).ToList();
                                if (listOfMatchedProfileDetail.Count > 0)
                                {
                                    spkDetail.ProfileDetail = listOfMatchedProfileDetail[0];
                                }
                                else
                                {
                                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ProfileDetail)));
                                }
                            }
                        }
                    }
                }
                else
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgInvalidProfileHeaderValue, group.ProfileHeader.Code, value)));
                }
            }
        }

        /// <summary>
        /// Get valid SPKCustomerProfile
        /// </summary>
        /// <param name="spkDetailID"></param>
        /// <param name="profileGroupID"></param>
        /// <param name="profileHeaderID"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private KTB.DNet.Domain.SPKProfile GetValidSPKCustomerProfile(int spkDetailID, int profileGroupID, int profileHeaderID, string value)
        {
            SPKProfile spkProfile = GetSPKProfileByProfileHeader(spkDetailID, profileGroupID, profileHeaderID);
            if (spkProfile != null)
            {
                spkProfile.ProfileValue = value;
                return spkProfile;
            }
            else
            {
                var objProHeader = _profileHeaderMapper.Retrieve(profileHeaderID);
                var objProGroup = _profileGroupMapper.Retrieve(profileGroupID);

                SPKProfile newProfile = new SPKProfile()
                {
                    ID = 0,
                    ProfileGroup = (ProfileGroup)objProGroup,
                    ProfileHeader = (ProfileHeader)objProHeader,
                    ProfileValue = value
                };

                return newProfile;
            }
        }

        /// <summary>
        /// GetSPKProfileByProfileHeader
        /// </summary>
        /// <param name="spkDetailID"></param>
        /// <param name="headerID"></param>
        /// <returns></returns>
        private SPKProfile GetSPKProfileByProfileHeader(int spkDetailID, int profGroupID, int headerID)
        {
            CriteriaComposite critSPKProfile = new CriteriaComposite(new Criteria(typeof(SPKProfile), "RowStatus", MatchType.Exact, ((short)(_enumBL.GetByCategoryAndCode("DBRowStatus", "Active").ValueId))));
            critSPKProfile.opAnd(new Criteria(typeof(SPKProfile), "SPKDetail.ID", MatchType.Exact, spkDetailID));
            critSPKProfile.opAnd(new Criteria(typeof(SPKProfile), "ProfileHeader.ID", MatchType.Exact, headerID));
            critSPKProfile.opAnd(new Criteria(typeof(SPKProfile), "ProfileGroup.ID", MatchType.Exact, profGroupID));

            var spkProfiles = _spkProfileMapper.RetrieveByCriteria(critSPKProfile);
            SPKProfile spkProfile = spkProfiles.Count > 0 ? (SPKProfile)spkProfiles[0] : null;
            return spkProfile;
        }

        /// <summary>
        /// ValidateSPKDetailItem
        /// </summary>
        /// <param name="kodeModel"></param>
        /// <param name="kodeWarna"></param>
        /// <param name="Unit"></param>
        /// <returns></returns>
        private bool ValidateSPKDetailItem(string kodeModel, string kodeWarna, int Unit, List<DNetValidationResult> validationResults)
        {
            if (string.IsNullOrEmpty(kodeModel))
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.VehicleType)));
                return false;
            }

            if (string.IsNullOrEmpty(kodeWarna))
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.VehicleColor)));
                return false;
            }

            if (Unit < 1)
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.Quantity)));
                return false;
            }

            bool iReturn = true;

            CriteriaComposite criterias1 = new CriteriaComposite(new Criteria(typeof(VechileType), "RowStatus", MatchType.Exact, ((short)(_enumBL.GetByCategoryAndCode("DBRowStatus", "Active").ValueId))));
            criterias1.opAnd(new Criteria(typeof(VechileType), "Status", MatchType.No, "X"));
            criterias1.opAnd(new Criteria(typeof(VechileType), "VechileTypeCode", MatchType.Exact, kodeModel));
            ArrayList ArrVehicleType = _vehicleTypeMapper.RetrieveByCriteria(criterias1);
            if (ArrVehicleType.Count == 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.VehicleType)));
                iReturn = false;
            }
            else if (kodeWarna != "ZZZZ" && kodeWarna != "zzzz")
            {
                CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(VechileColor), "RowStatus", MatchType.Exact, ((short)(_enumBL.GetByCategoryAndCode("DBRowStatus", "Active").ValueId))));
                criterias.opAnd(new Criteria(typeof(VechileColor), "ColorCode", MatchType.Exact, kodeWarna));
                criterias.opAnd(new Criteria(typeof(VechileColor), "VechileType.VechileTypeCode", MatchType.Exact, kodeModel));
                criterias.opAnd(new Criteria(typeof(VechileColor), "VechileType.Status", MatchType.No, "X"));
                criterias.opAnd(new Criteria(typeof(VechileColor), "Status", MatchType.No, "x"));
                criterias.opAnd(new Criteria(typeof(VechileColor), "SpecialFlag", MatchType.No, "x"));
                ArrayList ArrListVechileColor = _vehicleColorMapper.RetrieveByCriteria(criterias);
                if (ArrListVechileColor.Count == 0)
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.VehicleColor)));
                    iReturn = false;
                }
            }

            return iReturn;
        }

        /// <summary>
        /// Get list of spk faktur
        /// </summary>
        /// <param name="spk"></param>
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
        /// Get ProfileGroup By Category
        /// </summary>
        /// <param name="kategori"></param>
        /// <returns></returns>
        private ResponseBase<ProfileGroupDto> GetProfileGroupByCategory(string kategori)
        {
            IProfileGroupBL profileGroupBL = new ProfileGroupBL(_mapper);
            ResponseBase<ProfileGroupDto> profGroupResponse = new ResponseBase<ProfileGroupDto>();

            switch (kategori)
            {
                case "PC":
                    profGroupResponse = profileGroupBL.GetByCode(Constants.ProfileGroup.CustomerProfilePC);
                    break;

                case "LCV":
                    profGroupResponse = profileGroupBL.GetByCode(Constants.ProfileGroup.CustomerProfileLCV);
                    break;

                case "CV":
                    profGroupResponse = profileGroupBL.GetByCode(Constants.ProfileGroup.CustomerProfileCV);
                    break;

                default:
                    profGroupResponse.success = false;
                    break;
            }
            return profGroupResponse;
        }

        /// <summary>
        /// Handler on executed insert command with transaction manager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void InsertWithTransactionManagerHandler(object sender, TransactionManager.OnInsertArgs args)
        {
            // set the object ID from db returned id
            if (args.DomainObject.GetType() == typeof(SPKDetail))
            {
                ((SPKDetail)args.DomainObject).ID = args.ID;
                ((SPKDetail)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(SPKProfile))
            {
                ((SPKProfile)args.DomainObject).ID = args.ID;
                ((SPKProfile)args.DomainObject).MarkLoaded();
            }
        }

        /// <summary>
        /// Insert SPK Detail With Transaction Manager
        /// </summary>
        /// <param name="spkDetail"></param>
        /// <returns></returns>
        private int InsertWithTransactionManager(SPKDetail spkDetail)
        {
            int result = -1;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // add command to insert spk
                    this._transactionManager.AddInsert(spkDetail, DNetUserName);

                    // add command to insert spk detail
                    foreach (SPKProfile item in spkDetail.SPKProfiles)
                    {
                        // item.SPKDetail = spkDetail;
                        this._transactionManager.AddInsert(item, DNetUserName);
                    }

                    this._transactionManager.PerformTransaction();
                    result = spkDetail.ID;
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
        /// Update SPK Detail With Transaction Manager
        /// </summary>
        /// <param name="spkDetail"></param>
        /// <param name="existingProfiles"></param>
        /// <param name="newProfiles"></param>
        /// <returns></returns>
        private int UpdateWithTransactionManager(SPKDetail spkDetail, ArrayList existingProfiles, ArrayList newProfiles)
        {
            int result = -1;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    foreach (SPKProfile item in existingProfiles)
                    {
                        item.SPKDetail = spkDetail;
                        this._transactionManager.AddUpdate(item, DNetUserName);
                    }

                    foreach (SPKProfile item in newProfiles)
                    {
                        item.SPKDetail = spkDetail;
                        this._transactionManager.AddInsert(item, DNetUserName);
                    }

                    this._transactionManager.AddUpdate(spkDetail, DNetUserName);
                    this._transactionManager.PerformTransaction();
                    result = spkDetail.ID;
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
        /// Validate canceled spk detail
        /// </summary>
        /// <param name="spkDetail"></param>
        /// <param name="validationResults"></param>
        private List<DNetValidationResult> ValidateCanceledSPKDetail(SPKDetailParameterDto spkDetail)
        {
            List<DNetValidationResult> validationResults = new List<DNetValidationResult>();
            short detailCanceledStatus = (short)_enumBL.GetByCategoryAndCode("DBRowStatus", "Canceled").ValueId;
            if (spkDetail.Status == detailCanceledStatus)
            {
                if (string.IsNullOrEmpty(spkDetail.RejectedReason))
                {
                    validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.RejectedReason)));
                }
            }

            return validationResults;
        }

        /// <summary>
        /// Get SPKDetail by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<SPKDetailDto> GetById(int id)
        {
            var spkdetail = (SPKDetail)_spkDetailMapper.Retrieve(id);
            var spkdetailDTO = new SPKDetailDto();
            var result = new ResponseBase<SPKDetailDto>();

            try
            {
                if (spkdetail != null)
                {
                    spkdetailDTO = _mapper.Map<SPKDetailDto>(spkdetail);

                    result.lst = spkdetailDTO;
                    result.success = true;
                    result.messages = null;
                }
                else
                {
                    result.lst = null;
                    result.success = false;

                    result.total = 0;
                    result.messages = new List<MessageBase> { new MessageBase { ErrorCode = ErrorCode.DBRetrieveFailed, ErrorMessage = String.Format(MessageResource.ErrorMessageDataNotFoundWithColumn, typeof(SPKDetail).Name, Helper.GetCriteriasMessageFormat(typeof(SPKDetail), null, "ID", id.ToString())) } };
                }

            }
            catch (Exception)
            {
                result.success = false;

                result.messages.Add(new MessageBase { ErrorCode = ErrorCode.UnhandledException, ErrorMessage = string.Format(MessageResource.ErrorMsgPRGUnhandle, string.Empty) });
            }

            return result;
        }

        /// <summary>
        /// Get list of Vehicle Kind Group
        /// </summary>
        /// <param name="vehicleType"></param>
        /// <returns></returns>
        private List<VehicleKindGroup> GetListOfVehicleKindGroup(VechileType vehicleType)
        {
            List<VehicleKindGroup> result = new List<VehicleKindGroup>();
            List<int> listOfGroupId = new List<int>();
            if (vehicleType == null)
            {
                return result;
            }

            if (vehicleType.IsVehicleKind1 > 0)
            {
                // add (2) MPG	MOBIL PENUMPANG
                listOfGroupId.Add(2);
            }

            if (vehicleType.IsVehicleKind2 > 0)
            {
                // add (3) MBS	MOBIL BUS
                listOfGroupId.Add(3);
            }

            if (vehicleType.IsVehicleKind3 > 0)
            {
                // add (4) MBG	MOBIL BARANG
                listOfGroupId.Add(4);
            }

            if (vehicleType.IsVehicleKind4 > 0)
            {
                // add (5) KKH	KENDARAAN KHUSUS
                listOfGroupId.Add(5);
            }

            try
            {
                CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(VehicleKindGroup), "RowStatus", MatchType.Exact, _enumBL.GetByCategoryAndCode("DBRowStatus", "Active").ValueId));
                criterias.opAnd(new Criteria(typeof(VehicleKindGroup), "ID", MatchType.InSet, string.Format("({0})", string.Join(",", listOfGroupId))));

                ArrayList listOfVehicleGroup = _vehicleKindGroupMapper.RetrieveByCriteria(criterias);
                if (listOfVehicleGroup.Count > 0)
                {
                    result = listOfVehicleGroup.Cast<VehicleKindGroup>().ToList();
                }

                return result;
            }
            catch
            {
                return result;
            }
        }

        #endregion
    }
}

