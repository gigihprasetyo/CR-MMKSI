#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SPKCustomer business logic class
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
using System.IO;
using DNetDomain = KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using System.Text.RegularExpressions;

#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class SPKCustomerBL : AbstractBusinessLogic, ISPKCustomerBL
    {
        #region Variables
        private readonly IMapper _spkCustomerMapper;
        private readonly IMapper _spkCustomerProfileMapper;
        private readonly IMapper _sapCustomerMapper;
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
        public SPKCustomerBL()
        {
            _cityMapper = MapperFactory.GetInstance().GetMapper(typeof(City).ToString());
            _profileHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(ProfileHeader).ToString());
            _profileGroupMapper = MapperFactory.GetInstance().GetMapper(typeof(ProfileGroup).ToString());
            _spkCustomerMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKCustomer).ToString());
            _spkCustomerProfileMapper = MapperFactory.GetInstance().GetMapper(typeof(DNetDomain.SPKCustomerProfile).ToString());
            _sapCustomerMapper = MapperFactory.GetInstance().GetMapper(typeof(SAPCustomer).ToString());
            _businessSectorDetailMapper = MapperFactory.GetInstance().GetMapper(typeof(BusinessSectorDetail).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _enumBL = new StandardCodeBL(_mapper);
            _transactionManager = new TransactionManager();
            _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);
            _spkMasterCountryCodePhoneMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKMasterCountryCodePhone).ToString());
        }

        public SPKCustomerBL(AutoMapper.IMapper mapper)
        {
            _cityMapper = MapperFactory.GetInstance().GetMapper(typeof(City).ToString());
            _profileHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(ProfileHeader).ToString());
            _profileGroupMapper = MapperFactory.GetInstance().GetMapper(typeof(ProfileGroup).ToString());
            _spkCustomerMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKCustomer).ToString());
            _spkCustomerProfileMapper = MapperFactory.GetInstance().GetMapper(typeof(DNetDomain.SPKCustomerProfile).ToString());
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
        public ResponseBase<SPKCustomerDto> Create(SPKCustomerParameterDto objCreate)
        {
            var result = new ResponseBase<SPKCustomerDto>();
            var validationResults = new List<DNetValidationResult>();
            List<DNetDomain.SPKCustomerProfile> listOfSPKCustomerProfile = null;
            OCRIdentity oCRIdentity = null;

            try
            {
                // validate spk customer
                SPKCustomer newSPKCustomer = GetValidSPKCustomerDomain(objCreate, validationResults, this.DealerCode, out oCRIdentity, out listOfSPKCustomerProfile);

                // if valid
                if (validationResults.Count == 0)
                {
                    // update the other properties
                    newSPKCustomer.CreatedTime = DateTime.Now;
                    newSPKCustomer.SPKCustomerProfiles.AddRange(listOfSPKCustomerProfile);

                    // insert spk 
                    int insertedID = InsertWithTransactionManager(newSPKCustomer, oCRIdentity);
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
                    return PopulateValidationError<SPKCustomerDto>(validationResults, null);
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
        public ResponseBase<SPKCustomerDto> Update(SPKCustomerParameterDto objUpdate)
        {
            var result = new ResponseBase<SPKCustomerDto>();
            var validationResults = new List<DNetValidationResult>();
            List<DNetDomain.SPKCustomerProfile> listOfSPKCustomerProfile = null;
            OCRIdentity oCRIdentity = null;

            try
            {
                // validate spk customer
                SPKCustomer newSPKCustomer = GetValidSPKCustomerDomain(objUpdate, validationResults, this.DealerCode, out oCRIdentity, out listOfSPKCustomerProfile);

                // if valid
                if (validationResults.Count == 0)
                {
                    newSPKCustomer.LastUpdateTime = DateTime.Now;

                    int resultID = UpdateWithTransactionManager(newSPKCustomer, listOfSPKCustomerProfile, oCRIdentity);
                    if (resultID > 0)
                    {
                        result.success = true;
                        result._id = resultID;
                        result.total = 1;
                    }
                    else
                    {
                        ErrorMsgHelper.ErrorMsgDBSaveContactAdmin(result.messages);
                    }
                }
                else
                {
                    return PopulateValidationError<SPKCustomerDto>(validationResults, null);
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

        public ResponseBase<List<SPKCustomerDto>> Read(SPKCustomerFilterDto filterDto, int pageSize)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<SPKCustomerDto> Delete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get Valid SPK Customer Domain
        /// </summary>
        /// <param name="spkCustomer"></param>
        /// <returns></returns>
        public SPKCustomer GetValidSPKCustomerDomain(SPKCustomerParameterDto spkCustomer, List<DNetValidationResult> validationResults, string dealerCode, out OCRIdentity oCRIdentity, out List<DNetDomain.SPKCustomerProfile> listOfSPKCustomerProfile)
        {
            #region Declare
            SPKCustomer spkCustomerDomain = null;
            listOfSPKCustomerProfile = null;
            byte[] fileBytes = null;
            oCRIdentity = null;
            string filePath = null;
            #endregion

            // set isupdate flag
            bool isUpdate = spkCustomer.ID != 0;
            if (isUpdate)
            {
                SPKCustomer spkCustomerOnDB = (SPKCustomer)_spkCustomerMapper.Retrieve(spkCustomer.ID);
                if (spkCustomerOnDB == null)
                {
                    validationResults.Add(new DNetValidationResult(MessageResource.ErrorMsgDataUpdateNotAvailable));
                    return spkCustomerDomain;
                }
                else
                {
                    spkCustomer.CreatedBy = spkCustomerOnDB.CreatedBy;
                    spkCustomer.SAPCustomerID = spkCustomerOnDB.SAPCustomer.ID;
                }
            }

            // validate city
            City city = GetValidCity(spkCustomer, validationResults);

            // validate reff code
            if (!string.IsNullOrEmpty(spkCustomer.ReffCode))
            {
                Customer customer = null;
                ValidationHelper.ValidateCustomer(spkCustomer.ReffCode, validationResults, ref customer);
            }

            // tipe customer should exist on standardcode
            if (!_enumBL.IsExistByCategoryAndValue("EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer", spkCustomer.TipeCustomer.ToString()))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.CustomerType)));
                return null;
            }

            string validataionTypeIdentity = string.Empty;
            #region Validate TipePerusahaan
            // if tipecustomer == perusahaan, then tipePerusahaan is mandatory and valid
            if (spkCustomer.TipeCustomer == (short)_enumBL.GetByCategoryAndCode("EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer", "Perusahaan").ValueId)
            {
                if (!_enumBL.IsExistByCategoryAndValue("EnumTipePerusahaan", spkCustomer.TipePerusahaan.ToString()))
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.CompanyType)));
                    return null;
                }
                else
                {
                    validataionTypeIdentity = _enumBL.GetByCategoryAndValue("EnumTipePerusahaan", spkCustomer.TipePerusahaan.ToString()).ValueCode;

                    if (spkCustomer.TipePerusahaan < 6) /* PT PP CV UD PF */
                    {
                        spkCustomer.TypeIdentitas = 4;  /* TDP */
                    }
                    else if (spkCustomer.TipePerusahaan == 6) /* YAYASAN */
                    {
                        spkCustomer.TypeIdentitas = 5;  /* TDY */
                    }
                    else  /* KOPERASI */
                    {
                        spkCustomer.TypeIdentitas = 6;  /* SIK */
                    }
                }
            }
            #endregion

            #region Validate TipePerorangan
            if (spkCustomer.TipeCustomer == (short)_enumBL.GetByCategoryAndCode("EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer", "Perorangan").ValueId)
            {
                if (string.IsNullOrEmpty(spkCustomer.ImagePath))
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.ImageAttachment)));
                    return null;
                }
                // return if any errors found
                if (validationResults.Any())
                {
                    return null;
                }

                if (spkCustomer.OCRIdentity != null)
                {
                    if (spkCustomer.OCRIdentity.IdentityType != -1)
                    {
                        if (!_enumBL.IsExistByCategoryAndValue("IdentityTypeSPK", spkCustomer.OCRIdentity.IdentityType.ToString()))
                        {
                            validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.IdentityType + " " + spkCustomer.OCRIdentity.IdentityType)));
                            return null;
                        }
                    }

                    if (string.IsNullOrEmpty(spkCustomer.ImagePath))
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.ImageAttachment)));
                        return null;
                    }

                    // validate the evidence file if exists
                    //if (spkCustomer.OCRIdentity.IdentityFile != null)
                    //{
                    //    validationResults.AddRange(FileUtility.ValidateEvidenceOrIdentityFile(spkCustomer.OCRIdentity.IdentityFile, _mapper, out fileBytes, FieldResource.IdentityFile));
                    //}
                    //else
                    //{
                    //    validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.IdentityFile)));
                    //}

                    // return if any errors found
                    if (validationResults.Any())
                    {
                        return null;
                    }

                    // check if there is a file on param (the fileBytes has been initialized on file validation)
                    if (fileBytes != null)
                    {
                        // save the file
                        string uploadErrorMessage = FileUtility.SaveIdentityFile(spkCustomer.OCRIdentity.IdentityFile, dealerCode, fileBytes, out filePath);
                        if (!string.IsNullOrEmpty(uploadErrorMessage))
                        {
                            validationResults.Add(new DNetValidationResult(uploadErrorMessage));
                        }

                        // return if any errors found
                        if (validationResults.Any())
                        {
                            return null;
                        }
                    }

                    // validate the OCR
                    oCRIdentity = new OCRIdentity();
                    if (!string.IsNullOrEmpty(spkCustomer.OCRIdentity.ImageID))
                    {
                        if (isUpdate)
                        {
                            if (ValidationHelper.ValidateOCRIdentityServer(spkCustomer.OCRIdentity.IdentityType, spkCustomer.OCRIdentity.ImageID, validationResults))
                            {
                                // validate OCRIdentity
                                ValidationHelper.ValidateOCRIdentity(spkCustomer.ID, validationResults, ref oCRIdentity);

                                // reset
                                validationResults.Clear();
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            if (!ValidationHelper.ValidateOCRIdentityServer(spkCustomer.OCRIdentity.IdentityType, spkCustomer.OCRIdentity.ImageID, validationResults))
                            {
                                return null;
                            }
                        }
                    }
                    else
                    {

                        if (isUpdate)
                        {
                            // initialize the ocridentity mapper
                            var ocrIdentityMapper = MapperFactory.GetInstance().GetMapper(typeof(OCRIdentity).ToString());

                            // get by criteria
                            var listOfOcrIdentity = ocrIdentityMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(OCRIdentity), "SPKCustomer.ID", spkCustomer.ID));
                            if (listOfOcrIdentity.Count > 0)
                            {
                                oCRIdentity = listOfOcrIdentity[0] as OCRIdentity;
                            }
                        }

                    }

                    // update the ocr identity fields based on the ocr parameter
                    oCRIdentity = spkCustomer.OCRIdentity.ConvertObject<OCRIdentity>(oCRIdentity);
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        oCRIdentity.ImagePath = filePath;
                    }

                }

                /* Validate TypePerorangan & TypeIdentitas */
                validataionTypeIdentity = ValidateTypePeroranganTypeIdentity(spkCustomer, validationResults);
            }
            #endregion

            // prearea is not mandatory 
            if (!string.IsNullOrEmpty(spkCustomer.PreArea))
            {
                List<StandardCodeDto> listOfPreArea = _enumBL.GetByCategory("EnumPreArea");
                string listOfPreAreaOnString = "(" + string.Join(",", listOfPreArea.Select(preArea => preArea.ValueCode).ToList()) + ")";
                if (!listOfPreArea.Any(preArea => preArea.ValueCode.Trim().Equals(spkCustomer.PreArea.Trim(), StringComparison.OrdinalIgnoreCase)))
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.PreArea)));
                    return null;
                }
            }

            #region One Customer posibly has many SPK
            //if (!isUpdate)
            //{
            //    // 1 SAPCustomer only for 1 SPK
            //    CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(SPKCustomer), "SAPCustomer.ID", MatchType.Exact, spkCustomer.SAPCustomerID));
            //    ArrayList listOfExistingSPKCustomerWithSpecifiedSAPCust = _spkCustomerMapper.RetrieveByCriteria(criteria);
            //    if (listOfExistingSPKCustomerWithSpecifiedSAPCust.Count > 0)
            //    {
            //        validationResults.Add(new DNetValidationResult(MessageResource.ErrorMsgSAPCustomerOnlyForOneSPK));
            //        return null;
            //    }
            //} 
            #endregion

            // validate customer
            SAPCustomer sapCustomer = GetValidSAPCustomer(spkCustomer.SAPCustomerID, validationResults);

            // validate customer profile
            listOfSPKCustomerProfile = GetListOfValidSPKCustomerProfile(spkCustomer, validationResults, validataionTypeIdentity);
            if (validationResults.Count > 0)
            {
                return null;
            }

            // Get business sector
            BusinessSectorDetail businessSectorDetail = (BusinessSectorDetail)_businessSectorDetailMapper.Retrieve(spkCustomer.BusinessSectorDetailID);
            if (businessSectorDetail == null)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.BusinessSectorDetail, spkCustomer.BusinessSectorDetailID)));
                return null;
            }

            #region PhoneNumber&CountryCode Validation
            string CountryCode = spkCustomer.CountryCode.Trim();
            string PhoneNumber = spkCustomer.HpNo.Trim();
            ValidatePhoneAndCountryCode(PhoneNumber, CountryCode, validationResults);
            #endregion

            // create sapcustomer object
            spkCustomerDomain = _mapper.Map<SPKCustomer>(spkCustomer);

            if (spkCustomer.TipeCustomer == (short)_enumBL.GetByCategoryAndCode("EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer", "Perorangan").ValueId)
            {
                // validate image path
                string sapFolder = AppConfigs.GetString("SAPFolder");
                string sapDir = Path.Combine(AppConfigs.GetString("SAPFileDirectory"), @"OCR\");
                string destFolder = Path.Combine(sapFolder, sapDir);
                string imagePath = destFolder + spkCustomer.ImagePath;
                if (FileUtility.CheckExistsImagePath(imagePath))
                {
                    spkCustomerDomain.ImagePath = spkCustomer.ImagePath;
                }
                else
                {
                    validationResults.Add(new DNetValidationResult("ImagePath " + spkCustomer.ImagePath + " tidak valid."));
                    return null;
                }
                oCRIdentity = null;
                if (oCRIdentity != null)
                {
                    spkCustomerDomain.ImagePath = oCRIdentity.ImagePath;
                }
            }

            spkCustomerDomain = ResetSPKCustomerValueFromSPKCustomerProfile(spkCustomerDomain, listOfSPKCustomerProfile);
            spkCustomerDomain.LastUpdateTime = DateTime.Now;
            spkCustomerDomain.SAPCustomer = sapCustomer;
            spkCustomerDomain.City = city;
            spkCustomerDomain.BusinessSectorDetail = businessSectorDetail;
            spkCustomerDomain.PrintRegion = spkCustomer.PrintRegion.ToString(); //spkCustomer.PrintRegion == true ? "1" : "0";
            spkCustomerDomain.TypePerorangan = spkCustomer.TypePerorangan;
            spkCustomerDomain.TypeIdentitas = spkCustomer.TypeIdentitas.ToString();
            spkCustomerDomain.CountryCode = spkCustomer.CountryCode;

            if (!isUpdate) { spkCustomerDomain.CreatedTime = DateTime.Now; }

            // cek jika tipe Customer = BUMN Pemerintah dan LKPPReference ada isinya, LKPPStatus = 1 (NotVerifiedLKPP)
            short tipeBUMNPemerintah = (short)_enumBL.GetByCategoryAndCode("EnumTipePelangganSPKCustomer.TipePelangganSPKCustomer", "BUMN_Pemerintah").ValueId;
            if (spkCustomerDomain.TipeCustomer == tipeBUMNPemerintah)
            {
                if (string.IsNullOrEmpty(spkCustomer.LKPPReference))
                {
                    spkCustomerDomain.LKPPStatus = (short)_enumBL.GetByCategoryAndCode("EnumLKPPStatus.LKPPStatus", "NonLKPP").ValueId;
                }
                else
                {
                    spkCustomerDomain.LKPPStatus = (short)_enumBL.GetByCategoryAndCode("EnumLKPPStatus.LKPPStatus", "NotVerifiedLKPP").ValueId;
                }
            }
            else
            {
                spkCustomerDomain.LKPPStatus = (short)_enumBL.GetByCategoryAndCode("EnumLKPPStatus.LKPPStatus", "NonLKPP").ValueId;
            }

            return spkCustomerDomain;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Reset SPKCustomer Value From SPKCustomerProfile
        /// </summary>
        /// <returns></returns>
        private SPKCustomer ResetSPKCustomerValueFromSPKCustomerProfile(SPKCustomer spkCustomer, List<DNetDomain.SPKCustomerProfile> listOfSPKCustomerProfile)
        {
            foreach (DNetDomain.SPKCustomerProfile profile in listOfSPKCustomerProfile)
            {
                if (profile.ProfileHeader.Code == "EMAIL")
                {
                    spkCustomer.Email = profile.ProfileValue;
                }

                if (profile.ProfileHeader.Code == "KODEPOS")
                {
                    spkCustomer.PostalCode = profile.ProfileValue;
                }
            }

            return spkCustomer;
        }

        /// <summary>
        /// Validate Address
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private City GetValidCity(SPKCustomerParameterDto obj, List<DNetValidationResult> validationResults)
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
                    if (city.CityName.Length + obj.PreArea.Length > 40)
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
        private List<DNetDomain.SPKCustomerProfile> GetListOfValidSPKCustomerProfile(SPKCustomerParameterDto param, List<DNetValidationResult> validationResults, string validataionTypeIdentity)
        {
            List<DNetDomain.SPKCustomerProfile> listOfSPKCustomerProfile = new List<DNetDomain.SPKCustomerProfile>();
            IProfileGroupBL profileGroupBL = new ProfileGroupBL(_mapper);
            var profileGroupResponse = new ResponseBase<ProfileGroupDto>();
            List<StandardCodeDto> enumMandatoryMode = _enumBL.GetByCategory("EnumMandatory.MandatoryMode");

            switch (param.TipeCustomer)
            {
                case (short)EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perorangan:

                    profileGroupResponse = profileGroupBL.GetByCode(Constants.ProfileGroup.CustomerDatabasePerorangan);
                    if (profileGroupResponse.success)
                    {
                        ValidateSPKCustomerProfile(param, validationResults, listOfSPKCustomerProfile, profileGroupResponse, enumMandatoryMode, EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perorangan, validataionTypeIdentity);
                    }
                    break;

                case (short)EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perusahaan:

                    profileGroupResponse = profileGroupBL.GetByCode(Constants.ProfileGroup.CustomerDatabasePerusahaan);
                    if (profileGroupResponse.success)
                    {
                        ValidateSPKCustomerProfile(param, validationResults, listOfSPKCustomerProfile, profileGroupResponse, enumMandatoryMode, EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perusahaan, validataionTypeIdentity);
                    }
                    break;

                case (short)EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah:

                    profileGroupResponse = profileGroupBL.GetByCode(Constants.ProfileGroup.CustomerDatabasBUMN);
                    if (profileGroupResponse.success)
                    {
                        ValidateSPKCustomerProfile(param, validationResults, listOfSPKCustomerProfile, profileGroupResponse, enumMandatoryMode, EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah);
                    }
                    break;

                case (short)EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Lainnya:

                    profileGroupResponse = profileGroupBL.GetByCode(Constants.ProfileGroup.CustomerDatabaseLainnya);
                    if (profileGroupResponse.success)
                    {
                        ValidateSPKCustomerProfile(param, validationResults, listOfSPKCustomerProfile, profileGroupResponse, enumMandatoryMode, EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Lainnya);
                    }
                    break;

                default:
                    break;
            }

            return listOfSPKCustomerProfile;
        }

        /// <summary>
        /// Populate SPK Customer Profile
        /// </summary>
        /// <param name="param"></param>
        /// <param name="validationResults"></param>
        /// <param name="listOfSPKCustomerProfile"></param>
        /// <param name="profileGroupResponse"></param>
        /// <param name="enumMandatoryMode"></param>
        private void ValidateSPKCustomerProfile(SPKCustomerParameterDto param, List<DNetValidationResult> validationResults, List<DNetDomain.SPKCustomerProfile> listOfSPKCustomerProfile, ResponseBase<ProfileGroupDto> profileGroupResponse, List<StandardCodeDto> enumMandatoryMode, EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest tipePelanggan, string validataionTypeIdentity = "")
        {
            // initialize BL
            ISPKCustomerProfileBL spkCustomerProfileBL = new SPKCustomerProfileBL(_mapper);

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
                                /* else
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
                                */
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
                            if (param.OCRIdentity != null)
                            {
                                if (param.OCRIdentity.IdentityType == 0 && param.NOKTP.Trim().Length != 16)
                                {
                                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ResourceManager.GetString(group.ProfileHeader.Code))));
                                }
                            }
                            /*  Comment CR CDP & SPK Enhancement
                            else
                            {
                                if (param.NOKTP.Trim().Length != 16)
                                {
                                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ResourceManager.GetString(group.ProfileHeader.Code))));
                                }
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

                    /* set value POBOX if null or empty*/
                    if (group.ProfileHeader.Code == "POBOX" && string.IsNullOrEmpty(value))
                    {
                        value = "00000";
                    }

                    listOfSPKCustomerProfile.Add(GetValidSPKCustomerProfile(spkCustomerProfileBL, param.ID, group.ProfileGroup.ID, group.ProfileHeader.ID, value.ToString()));
                }
            }
        }

        /// <summary>
        /// Get valid SPKCustomerProfile
        /// </summary>
        /// <param name="spkCustomerID"></param>
        /// <param name="profileGroupID"></param>
        /// <param name="profileHeaderID"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private KTB.DNet.Domain.SPKCustomerProfile GetValidSPKCustomerProfile(ISPKCustomerProfileBL spkCustomerProfileBL, int spkCustomerID, int profileGroupID, int profileHeaderID, string value)
        {
            var existing = spkCustomerProfileBL.GetSPKCustomerProfiles(spkCustomerID, profileGroupID, profileHeaderID);
            if (existing.success)
            {
                var item = (DNetDomain.SPKCustomerProfile)_spkCustomerProfileMapper.Retrieve(existing._id);
                item.ProfileValue = value;
                return item;
            }
            else
            {
                var objProHeader = _profileHeaderMapper.Retrieve(profileHeaderID);
                var objProGroup = _profileGroupMapper.Retrieve(profileGroupID);

                DNetDomain.SPKCustomerProfile newCust = new DNetDomain.SPKCustomerProfile()
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
        /// <param name="spkCustomer"></param>
        /// <returns></returns>
        private int InsertWithTransactionManager(SPKCustomer spkCustomer, OCRIdentity oCRIdentity)
        {
            int result = -1;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // add command to insert spk customer
                    this._transactionManager.AddInsert(spkCustomer, DNetUserName);

                    if (oCRIdentity != null)
                    {
                        // add command to insert ocr identity
                        this._transactionManager.AddInsert(oCRIdentity, DNetUserName);
                    }

                    // add command to insert spk detail
                    foreach (DNetDomain.SPKCustomerProfile item in spkCustomer.SPKCustomerProfiles)
                    {
                        this._transactionManager.AddInsert(item, DNetUserName);
                    }

                    this._transactionManager.PerformTransaction();
                    result = spkCustomer.ID;
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
        private int UpdateWithTransactionManager(SPKCustomer objDomain, List<DNetDomain.SPKCustomerProfile> listOfSPKCustomerProfile, OCRIdentity oCRIdentity)
        {
            // set default result
            int result = -1;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // add command to insert spk customer profile
                    foreach (DNetDomain.SPKCustomerProfile item in listOfSPKCustomerProfile)
                    {
                        item.SPKCustomer = objDomain;
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
            if (args.DomainObject.GetType() == typeof(SPKCustomer))
            {
                ((SPKCustomer)args.DomainObject).ID = args.ID;
                ((SPKCustomer)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(DNetDomain.SPKCustomerProfile))
            {
                ((DNetDomain.SPKCustomerProfile)args.DomainObject).ID = args.ID;
                ((DNetDomain.SPKCustomerProfile)args.DomainObject).MarkLoaded();
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

        private string ValidateTypePeroranganTypeIdentity(SPKCustomerParameterDto param, List<DNetValidationResult> validationResults)
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
