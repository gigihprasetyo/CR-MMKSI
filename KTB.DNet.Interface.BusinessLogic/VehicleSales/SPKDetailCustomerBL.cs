#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKDetailCustomer business logic class
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
using DNetDomain = KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using System.IO;
using System.Text.RegularExpressions;

#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class SPKDetailCustomerBL : AbstractBusinessLogic, ISPKDetailCustomerBL
    {
        #region Variables
        private readonly IMapper _spkDetailCustomerMapper;
        private readonly IMapper _spkDetailCustomerProfileMapper;
        private readonly IMapper _sapCustomerMapper;
        private readonly IMapper _spkChassisMapper;
        private readonly IMapper _cityMapper;
        private readonly IMapper _profileHeaderMapper;
        private readonly IMapper _profileGroupMapper;
        private readonly IMapper _businessSectorDetailMapper;
        private TransactionManager _transactionManager;
        private readonly AutoMapper.IMapper _mapper;
        private StandardCodeBL _enumBL;
        private readonly IMapper _spkMasterCountryCodePhoneMapper;
        #endregion

        #region Constructor
        public SPKDetailCustomerBL()
        {
            _cityMapper = MapperFactory.GetInstance().GetMapper(typeof(City).ToString());
            _profileHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(ProfileHeader).ToString());
            _profileGroupMapper = MapperFactory.GetInstance().GetMapper(typeof(ProfileGroup).ToString());
            _spkDetailCustomerMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKDetailCustomer).ToString());
            _spkChassisMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKChassis).ToString());
            _spkDetailCustomerProfileMapper = MapperFactory.GetInstance().GetMapper(typeof(DNetDomain.SPKDetailCustomerProfile).ToString());
            _sapCustomerMapper = MapperFactory.GetInstance().GetMapper(typeof(SAPCustomer).ToString());
            _businessSectorDetailMapper = MapperFactory.GetInstance().GetMapper(typeof(BusinessSectorDetail).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _enumBL = new StandardCodeBL(_mapper);
            _transactionManager = new TransactionManager();
            _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);
            _spkMasterCountryCodePhoneMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKMasterCountryCodePhone).ToString());
        }

        public SPKDetailCustomerBL(AutoMapper.IMapper mapper)
        {
            _cityMapper = MapperFactory.GetInstance().GetMapper(typeof(City).ToString());
            _profileHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(ProfileHeader).ToString());
            _profileGroupMapper = MapperFactory.GetInstance().GetMapper(typeof(ProfileGroup).ToString());
            _spkDetailCustomerMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKDetailCustomer).ToString());
            _spkDetailCustomerProfileMapper = MapperFactory.GetInstance().GetMapper(typeof(DNetDomain.SPKDetailCustomerProfile).ToString());
            _sapCustomerMapper = MapperFactory.GetInstance().GetMapper(typeof(SAPCustomer).ToString());
            _businessSectorDetailMapper = MapperFactory.GetInstance().GetMapper(typeof(BusinessSectorDetail).ToString());
            _mapper = mapper;
            _enumBL = new StandardCodeBL(mapper);
            _transactionManager = new TransactionManager();
            _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);
            _spkMasterCountryCodePhoneMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKMasterCountryCodePhone).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Create SPK
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<SPKDetailCustomerDto> Create(SPKDetailCustomerParameterDto objCreate)
        {
            var result = new ResponseBase<SPKDetailCustomerDto>();
            var validationResults = new List<DNetValidationResult>();
            OCRIdentity oCRIdentity = null;
            List<MessageBase> getFakturbySpkDetailList = new List<MessageBase>();

            try
            {
                // validate spk detail customer
                SPKDetailCustomer newSPKDetailCustomer = GetValidSPKDetailCustomerDomain(objCreate, validationResults, this.DealerCode, out oCRIdentity, out getFakturbySpkDetailList);

                // if valid
                if (validationResults.Count == 0)
                {
                    // update the other properties
                    newSPKDetailCustomer.CreatedTime = DateTime.Now;
                    foreach (DNet.Domain.SPKDetailCustomerProfile item in newSPKDetailCustomer.SPKDetailCustomerProfiles)
                    {
                        /* set value POBOX if null or empty*/
                        if (item.ProfileHeader.Code == "POBOX" && string.IsNullOrEmpty(item.ProfileValue))
                        {
                            item.ProfileValue = "00000";
                        }
                    }
                    newSPKDetailCustomer.SPKDetailCustomerProfiles.AddRange(newSPKDetailCustomer.SPKDetailCustomerProfiles);

                    // insert spk 
                    int insertedID = InsertWithTransactionManager(newSPKDetailCustomer, oCRIdentity);
                    if (insertedID > 0)
                    {
                        result.success = true;
                        result._id = insertedID;
                        result.total = 1;
                    }
                    else
                    {
                        ErrorMsgHelper.ErrorMsgDBSaveContactAdmin(result.messages);
                    }
                }
                else
                {
                    return PopulateValidationError<SPKDetailCustomerDto>(validationResults, null);
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

            return result;
        }

        /// <summary>
        /// Update 
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<SPKDetailCustomerDto> Update(SPKDetailCustomerParameterDto objUpdate)
        {
            var result = new ResponseBase<SPKDetailCustomerDto>();
            var validationResults = new List<DNetValidationResult>();
            var listSPKDetailCustomerProfile = new List<DNet.Domain.SPKDetailCustomerProfile>();
            OCRIdentity oCRIdentity = null;
            List<MessageBase> getFakturbySpkDetailList = new List<MessageBase>();

            try
            {
                // validate spk detail customer
                SPKDetailCustomer newSPKDetailCustomer = GetValidSPKDetailCustomerDomain(objUpdate, validationResults, this.DealerCode, out oCRIdentity, out getFakturbySpkDetailList);

                // if valid
                if (validationResults.Count == 0)
                {
                    newSPKDetailCustomer.LastUpdateTime = DateTime.Now;

                    foreach (DNet.Domain.SPKDetailCustomerProfile item in newSPKDetailCustomer.SPKDetailCustomerProfiles)
                    {
                        /* set value POBOX if null or empty*/
                        if (item.ProfileHeader.Code == "POBOX" && string.IsNullOrEmpty(item.ProfileValue))
                        {
                            item.ProfileValue = "00000";
                        }

                        listSPKDetailCustomerProfile.Add(item);
                    }

                    int resultID = UpdateWithTransactionManager(newSPKDetailCustomer, listSPKDetailCustomerProfile, oCRIdentity);
                    if (resultID > 0)
                    {
                        result.success = true;
                        result._id = resultID;
                        result.total = 1;
                        if (getFakturbySpkDetailList.Count > 0)
                        {
                            result.messages = getFakturbySpkDetailList;
                        }
                    }
                    else
                    {
                        ErrorMsgHelper.ErrorMsgDBSaveContactAdmin(result.messages);
                    }
                }
                else
                {
                    return PopulateValidationError<SPKDetailCustomerDto>(validationResults, null);
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

        public ResponseBase<List<SPKDetailCustomerDto>> Read(SPKDetailCustomerFilterDto filterDto, int pageSize)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<SPKDetailCustomerDto> Delete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get Valid spk detail customer Domain
        /// </summary>
        /// <param name="sPKDetailCustomer"></param>
        /// <returns></returns>
        public SPKDetailCustomer GetValidSPKDetailCustomerDomain(SPKDetailCustomerParameterDto sPKDetailCustomer, List<DNetValidationResult> validationResults, string dealerCode, out OCRIdentity oCRIdentity, out List<MessageBase> getFakturbySpkDetailList)
        {
            #region Declare
            SPKDetailCustomer sPKDetailCustomerDomain = null;
            List<DNet.Domain.SPKDetailCustomerProfile> listOfSPKDetailCustomerProfile = new List<DNetDomain.SPKDetailCustomerProfile>();
            SPKDetailCustomer SPKDetailCustomerOnDB = null;
            byte[] fileBytes = null;
            oCRIdentity = null;
            string filePath = null;
            getFakturbySpkDetailList = new List<MessageBase>();
            #endregion

            // set isupdate flag
            bool isUpdate = sPKDetailCustomer.ID != 0;
            if (isUpdate)
            {
                SPKDetailCustomerOnDB = (SPKDetailCustomer)_spkDetailCustomerMapper.Retrieve(sPKDetailCustomer.ID);
                if (SPKDetailCustomerOnDB == null)
                {
                    validationResults.Add(new DNetValidationResult(MessageResource.ErrorMsgDataUpdateNotAvailable));
                    return sPKDetailCustomerDomain;
                }
                else
                {
                    sPKDetailCustomer.CreatedBy = SPKDetailCustomerOnDB.CreatedBy;
                    //sPKDetailCustomer.SAPCustomerID = SPKDetailCustomerOnDB.SAPCustomer.ID;

                    // cek faktur by spkdetail
                    if (GetSPKFakturBySPKDetail(SPKDetailCustomerOnDB.SPKDetail.ID).Count > 0)
                    {
                        string msg = "SPK Detail Customer tidak dapat di update karena SPK Detail ID " + SPKDetailCustomerOnDB.SPKDetail.ID.ToString() + " sudah memiliki Faktur/Revisi Faktur";
                        MessageBase newMessageBase = new MessageBase();
                        newMessageBase.ErrorMessage = msg;
                        getFakturbySpkDetailList.Add(newMessageBase);

                        // looping SPKDetailCustomerProfile
                        foreach (DNetDomain.SPKDetailCustomerProfile item in SPKDetailCustomerOnDB.SPKDetailCustomerProfiles)
                        {
                            listOfSPKDetailCustomerProfile.Add(item);
                        }

                        return SPKDetailCustomerOnDB;
                    }
                    else
                    {
                        // don't update SPKDetailCustomer if update customer is same with spk update
                        if (SPKDetailCustomerOnDB.LastUpdateCustomer == sPKDetailCustomer.LastUpdateCustomer)
                        {
                            // looping SPKDetailCustomerProfile
                            foreach (DNetDomain.SPKDetailCustomerProfile item in SPKDetailCustomerOnDB.SPKDetailCustomerProfiles)
                            {
                                listOfSPKDetailCustomerProfile.Add(item);
                            }

                            return SPKDetailCustomerOnDB;
                        }

                    }
                }
            }

            // validate city
            City city = GetValidCity(sPKDetailCustomer, validationResults);

            // validate reff code
            if (!string.IsNullOrEmpty(sPKDetailCustomer.ReffCode))
            {
                Customer customer = null;
                ValidationHelper.ValidateCustomer(sPKDetailCustomer.ReffCode, validationResults, ref customer);
            }

            // tipe customer should exist on standardcode
            if (!_enumBL.IsExistByCategoryAndValue("EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer", sPKDetailCustomer.TipeCustomer.ToString()))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.CustomerType)));
                return null;
            }

            string validataionTypeIdentity = string.Empty;
            #region Validate TipePerusahaan
            // if tipecustomer == perusahaan, then tipePerusahaan is mandatory and valid
            if (sPKDetailCustomer.TipeCustomer == (short)_enumBL.GetByCategoryAndCode("EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer", "Perusahaan").ValueId)
            {
                if (!_enumBL.IsExistByCategoryAndValue("EnumTipePerusahaan", sPKDetailCustomer.TipePerusahaan.ToString()))
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.CompanyType)));
                    return null;
                }
                else
                {
                    validataionTypeIdentity = _enumBL.GetByCategoryAndValue("EnumTipePerusahaan", sPKDetailCustomer.TipePerusahaan.ToString()).ValueCode;

                    if (sPKDetailCustomer.TipePerusahaan < 6) /* PT PP CV UD PF */
                    {
                        sPKDetailCustomer.TypeIdentitas = 4;  /* TDP */
                    }
                    else if (sPKDetailCustomer.TipePerusahaan == 6) /* YAYASAN */
                    {
                        sPKDetailCustomer.TypeIdentitas = 5;  /* TDY */
                    }
                    else  /* KOPERASI */
                    {
                        sPKDetailCustomer.TypeIdentitas = 6;  /* SIK */
                    }
                }
            }
            #endregion

            #region Validate TipePerorangan
            if (sPKDetailCustomer.TipeCustomer == (short)_enumBL.GetByCategoryAndCode("EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer", "Perorangan").ValueId)
            {
                // validate the OCR
                oCRIdentity = new OCRIdentity();

                if (string.IsNullOrEmpty(sPKDetailCustomer.ImagePath))
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.ImageAttachment)));
                    return null;
                }

                /* Validate TypePerorangan & TypeIdentitas */
                validataionTypeIdentity = ValidateTypePeroranganTypeIdentity(sPKDetailCustomer, validationResults);
            }
            #endregion

            // prearea is not mandatory 
            if (!string.IsNullOrEmpty(sPKDetailCustomer.PreArea))
            {
                List<StandardCodeDto> listOfPreArea = _enumBL.GetByCategory("EnumPreArea");
                string listOfPreAreaOnString = "(" + string.Join(",", listOfPreArea.Select(preArea => preArea.ValueCode).ToList()) + ")";
                if (!listOfPreArea.Any(preArea => preArea.ValueCode.Trim().Equals(sPKDetailCustomer.PreArea.Trim(), StringComparison.OrdinalIgnoreCase)))
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.PreArea)));
                    return null;
                }
            }

            #region One Customer posibly has many SPK
            //if (!isUpdate)
            //{
            //    // 1 SAPCustomer only for 1 SPK
            //    CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(SPKDetailCustomer), "SAPCustomer.ID", MatchType.Exact, SPKDetailCustomer.SAPCustomerID));
            //    ArrayList listOfExistingSPKDetailCustomerWithSpecifiedSAPCust = _SPKDetailCustomerMapper.RetrieveByCriteria(criteria);
            //    if (listOfExistingSPKDetailCustomerWithSpecifiedSAPCust.Count > 0)
            //    {
            //        validationResults.Add(new DNetValidationResult(MessageResource.ErrorMsgSAPCustomerOnlyForOneSPK));
            //        return null;
            //    }
            //} 
            #endregion

            // validate customer
            //SAPCustomer sapCustomer = GetValidSAPCustomer(sPKDetailCustomer.SAPCustomerID, validationResults);

            // validate customer profile
            listOfSPKDetailCustomerProfile = GetListOfValidSPKDetailCustomerProfile(sPKDetailCustomer, validationResults, validataionTypeIdentity);
            if (validationResults.Count > 0)
            {
                return null;
            }

            // Get business sector
            BusinessSectorDetail businessSectorDetail = (BusinessSectorDetail)_businessSectorDetailMapper.Retrieve(sPKDetailCustomer.BusinessSectorDetailID);
            if (businessSectorDetail == null)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.BusinessSectorDetail, sPKDetailCustomer.BusinessSectorDetailID)));
                return null;
            }

            #region PhoneNumber&CountryCode Validation
            string CountryCode = sPKDetailCustomer.CountryCode.Trim();
            string PhoneNumber = sPKDetailCustomer.HpNo.Trim();
            ValidatePhoneAndCountryCode(PhoneNumber, CountryCode, validationResults);
            #endregion

            sPKDetailCustomerDomain = _mapper.Map<SPKDetailCustomer>(sPKDetailCustomer);

            if (sPKDetailCustomer.TipeCustomer == (short)_enumBL.GetByCategoryAndCode("EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer", "Perorangan").ValueId)
            {
                // validate ImagePath
                // get destination folder from web config
                string sapFolder = AppConfigs.GetString("SAPFolder");
                string sapDir = Path.Combine(AppConfigs.GetString("SAPFileDirectory"), @"OCR\");
                string destFolder = Path.Combine(sapFolder, sapDir);
                string imagePath = destFolder + sPKDetailCustomer.ImagePath;
                if (FileUtility.CheckExistsImagePath(imagePath))
                {
                    sPKDetailCustomerDomain.ImagePath = sPKDetailCustomer.ImagePath;
                }
                else
                {
                    validationResults.Add(new DNetValidationResult("ImagePath " + sPKDetailCustomer.ImagePath + " tidak valid."));
                    return null;
                }
                oCRIdentity = null;
                if (oCRIdentity != null)
                {
                    sPKDetailCustomerDomain.ImagePath = oCRIdentity.ImagePath;
                }
            }

            sPKDetailCustomerDomain = ResetSPKDetailCustomerValueFromSPKDetailCustomerProfile(sPKDetailCustomerDomain, listOfSPKDetailCustomerProfile);
            sPKDetailCustomerDomain.LastUpdateTime = DateTime.Now;
            sPKDetailCustomerDomain.Quantity = sPKDetailCustomer.Quantity;
            sPKDetailCustomerDomain.DMSSPKDetailNo = sPKDetailCustomer.DMSSPKDetailNo;
            sPKDetailCustomerDomain.LastUpdateCustomer = sPKDetailCustomer.LastUpdateCustomer;
            sPKDetailCustomerDomain.SAPCustomer = null;
            sPKDetailCustomerDomain.City = city;
            sPKDetailCustomerDomain.BusinessSectorDetail = businessSectorDetail;
            //sPKDetailCustomerDomain.PrintRegion = sPKDetailCustomer.PrintRegion == true ? "1" : "0";
            sPKDetailCustomerDomain.PrintRegion = Convert.ToString(sPKDetailCustomer.PrintRegion);
            sPKDetailCustomerDomain.TypePerorangan = (short)sPKDetailCustomer.TypePerorangan;
            sPKDetailCustomerDomain.TypeIdentitas = (short)sPKDetailCustomer.TypeIdentitas;
            sPKDetailCustomerDomain.CountryCode = sPKDetailCustomer.CountryCode;

            sPKDetailCustomerDomain.SPKDetailCustomerProfiles.AddRange(listOfSPKDetailCustomerProfile);
            if (!isUpdate) { sPKDetailCustomerDomain.CreatedTime = DateTime.Now; }

            // cek jika tipe Customer = BUMN Pemerintah dan LKPPReference ada isinya, LKPPStatus = 1 (NotVerifiedLKPP)
            short tipeBUMNPemerintah = (short)_enumBL.GetByCategoryAndCode("EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer", "BUMN_Pemerintah").ValueId;
            if (sPKDetailCustomerDomain.TipeCustomer == tipeBUMNPemerintah)
            {
                if (string.IsNullOrEmpty(sPKDetailCustomer.LKPPReference))
                {
                    sPKDetailCustomerDomain.LKPPStatus = (short)_enumBL.GetByCategoryAndCode("EnumLKPPStatus.LKPPStatus", "NonLKPP").ValueId;
                }
                else
                {
                    sPKDetailCustomerDomain.LKPPStatus = (short)_enumBL.GetByCategoryAndCode("EnumLKPPStatus.LKPPStatus", "NotVerifiedLKPP").ValueId;
                }
            }
            else
            {
                sPKDetailCustomerDomain.LKPPStatus = (short)_enumBL.GetByCategoryAndCode("EnumLKPPStatus.LKPPStatus", "NonLKPP").ValueId;
            }

            return sPKDetailCustomerDomain;
        }
        #endregion

        #region Private Methods
        private ArrayList GetSPKFakturBySPKDetail(int spkDetailID)
        {
            var sql = "SELECT * FROM VWI_GetSPKFakturBySPKDetail WHERE SPKDetailID = " + spkDetailID.ToString();
            var lst = _spkDetailCustomerMapper.RetrieveSP(sql);
            return lst;
        }

        /// <summary>
        /// Reset SPKDetailCustomer Value From SPKDetailCustomerProfile
        /// </summary>
        /// <returns></returns>
        private SPKDetailCustomer ResetSPKDetailCustomerValueFromSPKDetailCustomerProfile(SPKDetailCustomer SPKDetailCustomer, List<DNetDomain.SPKDetailCustomerProfile> listOfSPKDetailCustomerProfile)
        {
            foreach (DNetDomain.SPKDetailCustomerProfile profile in listOfSPKDetailCustomerProfile)
            {
                if (profile.ProfileHeader.Code == "EMAIL")
                {
                    SPKDetailCustomer.Email = profile.ProfileValue;
                }

                if (profile.ProfileHeader.Code == "KODEPOS")
                {
                    SPKDetailCustomer.PostalCode = profile.ProfileValue;
                }
            }

            return SPKDetailCustomer;
        }

        /// <summary>
        /// Validate Address
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private City GetValidCity(SPKDetailCustomerParameterDto obj, List<DNetValidationResult> validationResults)
        {
            ArrayList listOfCity = _cityMapper.RetrieveByCriteria(Helper.GetCodeCriteria(typeof(City), "CityCode", obj.CityCode));
            City city = null;
            if (listOfCity.Count > 0)
            {
                city = (City)listOfCity[0];
                //if (city.CityName.Length > 31 && !string.IsNullOrEmpty(obj.PreArea))
                //{
                //    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgClearCityOrRegency)));
                //}
                //else if (city.CityName.Length == 30 && !string.IsNullOrEmpty(obj.PreArea) && obj.PreArea.Length > 4)
                //{
                //    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgUseKabOrClearCity)));
                //}
                //else if (city.CityName.Length == 31 && !string.IsNullOrEmpty(obj.PreArea) && obj.PreArea.Length > 3)
                //{
                //    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgUseKabOrClearCityRegency)));
                //}
                //else if (city.CityName.Length > 25 && city.CityName.Length < 30 && !string.IsNullOrEmpty(obj.PreArea) && obj.PreArea.Length > 5)
                //{
                //    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgUseKabCityKodya)));
                //}
                if (!string.IsNullOrEmpty(obj.PreArea))
                {
                    if(city.CityName.Length + obj.PreArea.Length > 40)
                {
                        validationResults.Add(new DNetValidationResult("Jumlah karakter PreArea & CityName melebihi 40"));
                    }
                }
                else
                {
                    if (city.CityName.Length > 40)
                {
                        validationResults.Add(new DNetValidationResult("Jumlah karakter CityName melebihi 40"));
                }
                }
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.City)));
            }

            return city;
        }

        /// <summary>
        /// Get valid SAP Customer
        /// </summary>
        /// <param name="sapCustomerId"></param>
        /// <returns></returns>
        private SAPCustomer GetValidSAPCustomer(int sapCustomerId, List<DNetValidationResult> validationResults)
        {
            SAPCustomer sapCustomer = (SAPCustomer)_sapCustomerMapper.Retrieve(sapCustomerId);
            if (sapCustomer == null)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMessageDataNotFound, FieldResource.SAPCustomer, sapCustomerId)));
            }

            return sapCustomer;
        }

        /// <summary>
        /// Validate customer profile
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private List<DNetDomain.SPKDetailCustomerProfile> GetListOfValidSPKDetailCustomerProfile(SPKDetailCustomerParameterDto param, List<DNetValidationResult> validationResults, string validataionTypeIdentity)
        {
            List<DNetDomain.SPKDetailCustomerProfile> listOfSPKDetailCustomerProfile = new List<DNetDomain.SPKDetailCustomerProfile>();
            IProfileGroupBL profileGroupBL = new ProfileGroupBL(_mapper);
            var profileGroupResponse = new ResponseBase<ProfileGroupDto>();
            List<StandardCodeDto> enumMandatoryMode = _enumBL.GetByCategory("EnumMandatory.MandatoryMode");

            switch (param.TipeCustomer)
            {
                case (short)EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perorangan:

                    profileGroupResponse = profileGroupBL.GetByCode(Constants.ProfileGroup.CustomerDatabasePerorangan);
                    if (profileGroupResponse.success)
                    {
                        ValidateSPKDetailCustomerProfile(param, validationResults, listOfSPKDetailCustomerProfile, profileGroupResponse, enumMandatoryMode, EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perorangan, validataionTypeIdentity);
                    }
                    break;

                case (short)EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perusahaan:

                    profileGroupResponse = profileGroupBL.GetByCode(Constants.ProfileGroup.CustomerDatabasePerusahaan);
                    if (profileGroupResponse.success)
                    {
                        ValidateSPKDetailCustomerProfile(param, validationResults, listOfSPKDetailCustomerProfile, profileGroupResponse, enumMandatoryMode, EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perusahaan, validataionTypeIdentity);
                    }
                    break;

                case (short)EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah:

                    profileGroupResponse = profileGroupBL.GetByCode(Constants.ProfileGroup.CustomerDatabasBUMN);
                    if (profileGroupResponse.success)
                    {
                        ValidateSPKDetailCustomerProfile(param, validationResults, listOfSPKDetailCustomerProfile, profileGroupResponse, enumMandatoryMode, EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah);
                    }
                    break;

                case (short)EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Lainnya:

                    profileGroupResponse = profileGroupBL.GetByCode(Constants.ProfileGroup.CustomerDatabaseLainnya);
                    if (profileGroupResponse.success)
                    {
                        ValidateSPKDetailCustomerProfile(param, validationResults, listOfSPKDetailCustomerProfile, profileGroupResponse, enumMandatoryMode, EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Lainnya);
                    }
                    break;

                default:
                    break;
            }

            return listOfSPKDetailCustomerProfile;
        }

        /// <summary>
        /// Populate spk detail customer Profile
        /// </summary>
        /// <param name="param"></param>
        /// <param name="validationResults"></param>
        /// <param name="listOfSPKDetailCustomerProfile"></param>
        /// <param name="profileGroupResponse"></param>
        /// <param name="enumMandatoryMode"></param>
        private void ValidateSPKDetailCustomerProfile(SPKDetailCustomerParameterDto param, List<DNetValidationResult> validationResults, List<DNetDomain.SPKDetailCustomerProfile> listOfSPKDetailCustomerProfile, ResponseBase<ProfileGroupDto> profileGroupResponse, List<StandardCodeDto> enumMandatoryMode, EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest tipePelanggan, string validataionTypeIdentity = "")
        {
            // initialize BL
            ISPKDetailCustomerProfileBL SPKDetailCustomerProfileBL = new SPKDetailCustomerProfileBL(_mapper);

            foreach (var group in profileGroupResponse.lst.ProfileHeaderToGroups)
            {
                dynamic value;

                var prop = param.GetType().GetProperty(group.ProfileHeader.Code);
                if (prop != null)
                {
                    value = prop.GetValue(param, null);

                    if (group.ProfileHeader.Mandatory == (short)enumMandatoryMode.Where(s => s.ValueCode == "Benar").SingleOrDefault().ValueId && (value == null || string.IsNullOrWhiteSpace(value.ToString())))
                    {
                        validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.ResourceManager.GetString(group.ProfileHeader.Code))));
                        continue;
                    }

                    if (group.ProfileHeader.Code == "TGLLAHIR")
                    {
                        value = Convert.ToDateTime(value).ToString("dd/MM/yyyy");
                    }

                    // validate NO KTP
                    if (group.ProfileHeader.Code == "NOKTP")
                    {
                        if (tipePelanggan == EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perorangan)
                        {
                            if (validataionTypeIdentity == "KTP")
                            {
                                if (param.NOKTP.Trim().Length != 16)
                                {
                                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ResourceManager.GetString(group.ProfileHeader.Code))));
                                }
                                else
                                {
                                    string TglLahirKTP = param.NOKTP.Substring(6, 6);
                                    string TglLahir = string.Empty;

                                    if (param.JK == "PR")
                                    {
                                        TglLahir = Convert.ToDateTime(param.TGLLAHIR).ToString("ddMMyy");
                                        int firstDigit = Convert.ToInt32(TglLahir.Substring(0, 1)) + 4;
                                        TglLahir = firstDigit.ToString() + TglLahir.Substring(1, 5);
                                    }
                                    else
                                    {
                                        TglLahir = Convert.ToDateTime(param.TGLLAHIR).ToString("ddMMyy");
                                    }

                                    if (TglLahirKTP != TglLahir)
                                    {
                                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgTglLahirNotMatchKTP, param.NOKTP, Convert.ToDateTime(param.TGLLAHIR).ToString("yyyy-MM-dd"))));
                                    }
                                }
                            }
                            else if (validataionTypeIdentity == "SIM")
                            {
                                if (param.NOKTP.Trim().Length < 9 || param.NOKTP.Trim().Length > 17)
                                {
                                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ResourceManager.GetString(group.ProfileHeader.Code))));
                                }
                            }
                            else if ((validataionTypeIdentity == "KITAS" || validataionTypeIdentity == "KITAP"))
                            {
                                if (param.NOKTP.Trim().Length > 11)
                                {
                                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ResourceManager.GetString(group.ProfileHeader.Code))));
                                }
                            }

                            // if NO KTP then should has 16 digit, there is no validation for NO SIM similar with DNET
                            /* Comment CR CDP & SPK Enhancement
                            if ( param.NOKTP.Trim().Length != 16)
                            {
                                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ResourceManager.GetString(group.ProfileHeader.Code))));
                            }
                            */
                        }
                        else if (tipePelanggan == EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perusahaan)
                        {
                            if (param.NOKTP.Trim().Length < 9 || param.NOKTP.Trim().Length > 40)
                            {
                                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ResourceManager.GetString(group.ProfileHeader.Code))));
                            }
                            else
                            {
                                if (validataionTypeIdentity == "PT" || validataionTypeIdentity == "UD" || validataionTypeIdentity == "PF" || validataionTypeIdentity == "CV")
                                {
                                    if (!param.NOKTP.Any(char.IsDigit))
                                    {
                                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ResourceManager.GetString(group.ProfileHeader.Code))));
                                    }
                                }
                                // 20220329 - Halimi by WA - remove validation no ktp for customer Yayasan
                                //else
                                //{
                                //    var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

                                //    if (!regexItem.IsMatch(param.NOKTP))
                                //    {
                                //        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ResourceManager.GetString(group.ProfileHeader.Code))));
                                //    }
                                //}
                            }
                        }
                        else if (tipePelanggan == EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah)
                        {
                            if (param.NOKTP.Trim().Length < 1 || param.NOKTP.Trim().Length > 40)
                            {
                                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ResourceManager.GetString(group.ProfileHeader.Code))));
                            }
                        }
                        else if (tipePelanggan == EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Lainnya)
                        {
                            if (param.NOKTP.Trim().Length > 40)
                            {
                                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ResourceManager.GetString(group.ProfileHeader.Code))));
                            }
                        }
                    }

                    // validate NO TELP
                    if ((group.ProfileHeader.Code == "NOTELP"))
                    {
                        if (tipePelanggan == EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perorangan)
                        {
                            if (param.NOTELP.Length < 8 || param.NOTELP.Substring(0, 2) == "00")
                            {
                                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ResourceManager.GetString(group.ProfileHeader.Code))));
                            }
                        }
                        else if (tipePelanggan == EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perusahaan)
                        {
                            if (param.NOTELP.Length < 8 || param.NOTELP.Substring(0, 2) == "00")
                            {
                                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ResourceManager.GetString(group.ProfileHeader.Code))));
                            }
                        }
                        else if (tipePelanggan == EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah)
                        {
                            if (param.NOTELP.Length < 6 || param.NOTELP.Substring(0, 2) == "00")
                            {
                                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ResourceManager.GetString(group.ProfileHeader.Code))));
                            }
                        }
                        else if (tipePelanggan == EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Lainnya)
                        {
                            if (param.NOTELP.Length < 6 || param.NOTELP.Substring(0, 2) == "00")
                            {
                                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ResourceManager.GetString(group.ProfileHeader.Code))));
                            }
                        }
                    }

                    listOfSPKDetailCustomerProfile.Add(GetValidSPKDetailCustomerProfile(SPKDetailCustomerProfileBL, param.ID, group.ProfileGroup.ID, group.ProfileHeader.ID, value.ToString()));
                }
            }
        }

        /// <summary>
        /// Get valid SPKDetailCustomerProfile
        /// </summary>
        /// <param name="SPKDetailCustomerID"></param>
        /// <param name="profileGroupID"></param>
        /// <param name="profileHeaderID"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private KTB.DNet.Domain.SPKDetailCustomerProfile GetValidSPKDetailCustomerProfile(ISPKDetailCustomerProfileBL SPKDetailCustomerProfileBL, int SPKDetailCustomerID, int profileGroupID, int profileHeaderID, string value)
        {
            var existing = SPKDetailCustomerProfileBL.GetSPKDetailCustomerProfiles(SPKDetailCustomerID, profileGroupID, profileHeaderID);
            if (existing.success)
            {
                var item = (DNetDomain.SPKDetailCustomerProfile)_spkDetailCustomerProfileMapper.Retrieve(existing._id);
                item.ProfileValue = value;
                return item;
            }
            else
            {
                var objProHeader = _profileHeaderMapper.Retrieve(profileHeaderID);
                var objProGroup = _profileGroupMapper.Retrieve(profileGroupID);

                DNetDomain.SPKDetailCustomerProfile newCust = new DNetDomain.SPKDetailCustomerProfile()
                {
                    ID = 0,
                    ProfileGroup = (ProfileGroup)objProGroup,
                    ProfileHeader = (ProfileHeader)objProHeader,
                    ProfileValue = value
                };

                return newCust;
            }
        }

        /// <summary>
        /// Insert spk with transaction manager
        /// </summary>
        /// <param name="SPKDetailCustomer"></param>
        /// <returns></returns>
        private int InsertWithTransactionManager(SPKDetailCustomer SPKDetailCustomer, OCRIdentity oCRIdentity)
        {
            int result = -1;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // add command to insert spk detail customer
                    this._transactionManager.AddInsert(SPKDetailCustomer, DNetUserName);

                    if (oCRIdentity != null)
                    {
                        // add command to insert ocr identity
                        this._transactionManager.AddInsert(oCRIdentity, DNetUserName);
                    }

                    // add command to insert spk detail
                    foreach (DNetDomain.SPKDetailCustomerProfile item in SPKDetailCustomer.SPKDetailCustomerProfiles)
                    {
                        this._transactionManager.AddInsert(item, DNetUserName);
                    }

                    this._transactionManager.PerformTransaction();
                    result = SPKDetailCustomer.ID;
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
        private int UpdateWithTransactionManager(SPKDetailCustomer objDomain, List<DNetDomain.SPKDetailCustomerProfile> listOfSPKDetailCustomerProfile, OCRIdentity oCRIdentity)
        {
            // set default result
            int result = -1;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // add command to insert spk detail customer profile
                    foreach (DNetDomain.SPKDetailCustomerProfile item in listOfSPKDetailCustomerProfile)
                    {
                        item.SPKDetailCustomer = objDomain;
                        if (item.ID == 0)
                        {
                            this._transactionManager.AddInsert(item, DNetUserName);
                        }
                        else
                        {
                            this._transactionManager.AddUpdate(item, DNetUserName);
                        }
                    }

                    // add command to update ocr identity
                    if (oCRIdentity != null)
                    {
                        if (oCRIdentity.ID == 0)
                        {
                            // add command to insert ocr identity
                            this._transactionManager.AddInsert(oCRIdentity, DNetUserName);
                        }
                        else
                        {
                            // add command to insert ocr identity
                            this._transactionManager.AddUpdate(oCRIdentity, DNetUserName);
                        }
                    }

                    // add command to update spk
                    _transactionManager.AddUpdate(objDomain, DNetUserName);
                    _transactionManager.PerformTransaction();
                    result = objDomain.ID;
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
            if (args.DomainObject.GetType() == typeof(SPKDetailCustomer))
            {
                ((SPKDetailCustomer)args.DomainObject).ID = args.ID;
                ((SPKDetailCustomer)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(DNetDomain.SPKDetailCustomerProfile))
            {
                ((DNetDomain.SPKDetailCustomerProfile)args.DomainObject).ID = args.ID;
                ((DNetDomain.SPKDetailCustomerProfile)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(DNetDomain.OCRIdentity))
            {
                ((DNetDomain.OCRIdentity)args.DomainObject).ID = args.ID;
                ((DNetDomain.OCRIdentity)args.DomainObject).MarkLoaded();
            }
        }
        private void ValidatePhoneAndCountryCode(string Phone, string CountryCode, List<DNetValidationResult> results)
        {
            if (!string.IsNullOrEmpty(CountryCode))
            {
                var criterias = new CriteriaComposite(new Criteria(typeof(SPKMasterCountryCodePhone), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(SPKMasterCountryCodePhone), "CountryCode", MatchType.Exact, CountryCode));
                var data = _spkMasterCountryCodePhoneMapper.RetrieveByCriteria(criterias);

                if (data.Count < 1)
                {
                    results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMessageDataNotFoundWithColumn, FieldResource.CountryCode, "code " + CountryCode)));
                }
                else
                {
                    string PhoneNumber = Phone;
                    if (Phone.Substring(0, 1) == "0")
                    {
                        PhoneNumber = PhoneNumber.Remove(0, 1);
                    }

                    if (PhoneNumber.Length > 13 || PhoneNumber.Length < 9)
                    {
                        results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataPhoneLength, FieldResource.Phone)));
                    }
                }
            }
            else
            {
                results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.CountryCode)));
            }
        }

        private string ValidateTypePeroranganTypeIdentity(SPKDetailCustomerParameterDto param, List<DNetValidationResult> validationResults)
        {
            string result = string.Empty;
            string typePerorangan = string.Empty;
            string typeIdentity = string.Empty;
            List<StandardCodeDto> listPerorangan = _enumBL.GetByCategory("EnumTipePelangganSPKCustomer.TipePeroranganSPKCustomer");
            List<StandardCodeDto> listIdentity = _enumBL.GetByCategory("IdentityTypeSPK");

            List<StandardCodeDto> validateTypePerorangan = listPerorangan.Where(s => s.ValueId == param.TypePerorangan).ToList();
            typePerorangan = validateTypePerorangan.Count() < 1 ? string.Empty : validateTypePerorangan[0].ValueCode;

            List<StandardCodeDto> validateTypeIdentity = listIdentity.Where(s => s.ValueId == param.TypeIdentitas).ToList();
            typeIdentity = validateTypeIdentity.Count() < 1 ? string.Empty : validateTypeIdentity[0].ValueCode;

            if (!string.IsNullOrEmpty(typePerorangan))
            {
                if (string.IsNullOrEmpty(typeIdentity))
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMessageDataNotFoundWithColumn, FieldResource.TypeIdentity, "value : " + param.TypeIdentitas)));
                }
                else
                {
                    if (typePerorangan == "Domestik")
                    {
                        listIdentity = listIdentity.Where(s => s.ValueId == 0 || s.ValueId == 1).ToList();
                    }
                    else if (typePerorangan == "Asing")
                    {
                        listIdentity = listIdentity.Where(s => s.ValueId == 2 || s.ValueId == 3).ToList();
                    }
                    var checkIdentity = listIdentity.Where(s => s.ValueCode == typeIdentity);

                    if (checkIdentity.Count() < 1)
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataValueInvalid, FieldResource.TypePerorangan, FieldResource.TypeIdentity)));
                    }
                    else
                    {
                        result = typeIdentity;
                    }
                }
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMessageDataNotFoundWithColumn, FieldResource.TypePerorangan, "value : " + param.TypePerorangan)));
            }

            return result;
        }
        #endregion
    }
}
