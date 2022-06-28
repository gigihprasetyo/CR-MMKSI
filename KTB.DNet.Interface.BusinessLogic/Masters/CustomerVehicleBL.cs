#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : CustomerVehicle business logic class
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
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.IO;
using IFDomain = KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Repository.Interface;
using System.Data;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class CustomerVehicleBL : AbstractBusinessLogic, ICustomerVehicleBL
    {
        #region Variables
        private readonly IMapper _customerMapper;
        private readonly IMapper _customerRequestMapper;
        private readonly IMapper _customerRequestProfileMapper;
        private readonly IMapper _customerRequestProfileHistoryMapper;
        private readonly IMapper _profileHeaderMapper;
        private readonly IMapper _profileGroupMapper;
        private readonly IMapper _profileDetailMapper;
        private readonly IMapper _oCRIdentityMapper;
        private readonly IMapper _sFDCustomerMapper;
        private readonly IMapper _cityMapper;
        private readonly AutoMapper.IMapper _mapper;
        private TransactionManager _transactionManager;
        private StandardCodeBL _enumBL;
        private ICustomerVehicleRepository<SFDCustomer, int> _CustomerVehicleRepo;
        public string connectionString = "Data Source=172.17.31.122;Initial Catalog=BSIDNET_MMKSI_QA_Release;User id=admin;Password=hunter2;";
        private readonly IMapper _spkMasterCountryCodePhoneMapper;
        #endregion

        #region Constructor
        public CustomerVehicleBL(IVWI_SPKMasterCountryCodePhoneRepository<IFDomain.VWI_SPKMasterCountryCodePhone, int> SPKMasterCountryCodePhoneRepo,ICustomerVehicleRepository<SFDCustomer, int> CustomerVehicleRepo)
        {
            _customerMapper = MapperFactory.GetInstance().GetMapper(typeof(Customer).ToString());
            _customerRequestMapper = MapperFactory.GetInstance().GetMapper(typeof(CustomerRequest).ToString());
            _customerRequestProfileMapper = MapperFactory.GetInstance().GetMapper(typeof(CustomerRequestProfile).ToString());
            _customerRequestProfileHistoryMapper = MapperFactory.GetInstance().GetMapper(typeof(CustomerRequestProfileHistory).ToString());
            _profileHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(ProfileHeader).ToString());
            _profileGroupMapper = MapperFactory.GetInstance().GetMapper(typeof(ProfileGroup).ToString());
            _profileDetailMapper = MapperFactory.GetInstance().GetMapper(typeof(ProfileDetail).ToString());
            _oCRIdentityMapper = MapperFactory.GetInstance().GetMapper(typeof(OCRIdentity).ToString());
            _sFDCustomerMapper = MapperFactory.GetInstance().GetMapper(typeof(SFDCustomer).ToString());
            _cityMapper = MapperFactory.GetInstance().GetMapper(typeof(City).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _transactionManager = new TransactionManager();
            _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);
            _enumBL = new StandardCodeBL(_mapper);
            _CustomerVehicleRepo = CustomerVehicleRepo;

            _spkMasterCountryCodePhoneMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKMasterCountryCodePhone).ToString());
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Create a new CustomerRequest
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<CustomerVehicleDto> Create(CustomerVehicleParameterDto objCreate)
        {
            #region Declarations
            var result = new ResponseBase<CustomerVehicleDto>();
            var validationResults = new List<DNetValidationResult>();
            City city = null;
            Dealer dealer = null;
            OCRIdentity ocrIdentity = null;
            CustomerStatusHistory customerStatusHistory = new CustomerStatusHistory();
            List<CustomerRequestProfile> customerRequestProfiles = new List<CustomerRequestProfile>();
            List<CustomerRequestProfileHistory> customerRequestProfileHistories = new List<CustomerRequestProfileHistory>();
            bool isValid = true;
            string imagePath = string.Empty;
            bool isNewOcrIdentity = false;
            StandardCodeDto tipePelanggan = null;
            byte[] fileBytes = null;
            #endregion

            try
            {
                isValid = ValidateEnum(ref objCreate, validationResults, ref tipePelanggan);

                if (isValid) { isValid = ValidateCustomerByRequestType(objCreate.CustomerCode, objCreate.ReffCode, objCreate.RequestType, validationResults); }

                if (isValid) { isValid = ValidationHelper.ValidateDealer(objCreate.DealerCode, validationResults, this.DealerCode, ref dealer); }

                if (isValid) { isValid = ValidationHelper.ValidateCustomerDealer(objCreate.ReffCode, objCreate.DealerCode, validationResults); }

                if (isValid) { isValid = ValidateCustomerRequestByRefRequestNo(objCreate.RefRequestNo, objCreate.DealerCode, validationResults); }

                if (isValid) { isValid = ValidateCustomerByRefCode(objCreate.ReffCode, validationResults); }

                if (isValid) { isValid = ValidationHelper.ValidateCity(objCreate.CityCode, validationResults, ref city); }

                if (isValid) { isValid = ValidateCustomerRequestProfiles(objCreate, customerRequestProfiles, validationResults); }

                if (isValid) { isValid = ValidatePreArea(objCreate, city, validationResults); }

                if (isValid) { isValid = ValidatePhoneAndCountryCode(objCreate, validationResults); }

                // only for perorangan
                if (isValid && objCreate.Status1 == 0)
                {
                    // validate OCR
                    //isValid = ValidateOCR(objCreate, validationResults, ref ocrIdentity, ref isNewOcrIdentity);

                    // validate image file
                    //validationResults.AddRange(FileUtility.ValidateEvidenceOrIdentityFile(objCreate.OCRIdentity.IdentityFile, _mapper, out fileBytes, FieldResource.ImageAttachment));

                    // reset flag
                    isValid = validationResults.Count == 0;
                }

                if (isValid)
                {
                    // create new customer request object
                    var newCustomerRequest = _mapper.Map<CustomerRequest>(objCreate);
                    newCustomerRequest.CityID = city.ID;
                    newCustomerRequest.Dealer = dealer;
                    newCustomerRequest.ProcessDate = SetProcessDate(objCreate);
                    newCustomerRequest.RequestDate = DateTime.Now;
                    newCustomerRequest.PrintRegion = Convert.ToString(objCreate.PrintRegion);
                    newCustomerRequest.TypePerorangan = objCreate.TypePerorangan;
                    newCustomerRequest.TypeIdentitas = objCreate.TypeIdentitas;
                    newCustomerRequest.CountryCode = objCreate.CountryCode;

                    // update some fields
                    UpdateProfileHistoryAndStatusHistory(customerStatusHistory, customerRequestProfiles, customerRequestProfileHistories, newCustomerRequest);

                    // insert via trans manager
                    int custRequestID = InsertWithTransactionManager(newCustomerRequest, customerRequestProfiles, customerRequestProfileHistories, customerStatusHistory);
                    if (custRequestID > 0)
                    {
                        if (objCreate.Status1 == 0)
                        {
                            //if (isNewOcrIdentity)
                            //{
                            //    // save file ocr
                            //    SaveFileCustomerRequest(objCreate, validationResults, ref ocrIdentity, custRequestID, fileBytes);
                            //}

                            if (validationResults.Count == 0)
                            {
                                // insert OCR Identity
                                //var ocrIdentityID = isNewOcrIdentity ? InsertWithTransactionManager(ocrIdentity) : ocrIdentity.ID;
                                //if (ocrIdentityID > 0)
                                //{
                                // link the ocr with customer request
                                    var customerReqOCR = new CustomerRequestOCR();
                                    customerReqOCR.CreatedTime = DateTime.Now;
                                    customerReqOCR.CustomerRequestID = custRequestID;
                                    customerReqOCR.OCRIdentityID = 0;

                                    // insert customer request OCR
                                    var insertedCustomerReqOCRID = InsertWithTransactionManager(customerReqOCR);
                                    if (insertedCustomerReqOCRID > 0)
                                    {
                                        result.success = true;
                                        result._id = custRequestID;
                                        result.total = 1;
                                        result.lst = null;
                                    }
                                    else
                                    {
                                        ErrorMsgHelper.ErrorMsgDBSave(result.messages);
                                    }
                                //}
                                //else
                                //{
                                //    ErrorMsgHelper.ErrorMsgDBSave(result.messages);
                                //}
                            }
                        }
                        else
                        {
                            result.success = true;
                            result._id = custRequestID;
                            result.total = 1;
                            result.lst = null;
                        }
                    }
                    else
                    {
                        ErrorMsgHelper.ErrorMsgDBSave(result.messages);
                    }
                }
                else
                {
                    return PopulateValidationError<CustomerVehicleDto>(validationResults, null);
                }
            }
            catch (SqlException ex)
            {
                ErrorMsgHelper.SqlException(result.messages, ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.SqlException(result.messages, ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Update CustomerRequest
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<CustomerVehicleDto> Update(CustomerVehicleUpdateParameterDto objUpdate)
        {
            #region Declarations
            City city = null;
            Dealer dealer = null;
            CustomerRequest customerRequest = null;
            var ocrIdentity = new OCRIdentity();
            var result = new ResponseBase<CustomerVehicleDto>();
            var validationResults = new List<DNetValidationResult>();
            List<CustomerRequestProfile> customerRequestProfiles = new List<CustomerRequestProfile>();
            List<CustomerRequestProfileHistory> customerRequestProfileHistories = new List<CustomerRequestProfileHistory>();
            var isValid = true;
            bool isNewOcrIdentity = false;
            StandardCodeDto tipePelanggan = null;
            byte[] fileBytes = null;
            #endregion

            try
            {
                isValid = ValidationHelper.ValidateDealer(objUpdate.DealerCode, validationResults, this.DealerCode, ref dealer);
                if (isValid)
                {
                    //update sfdcustomer
                    var criterias = new CriteriaComposite(new Criteria(typeof(SFDCustomer), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    criterias.opAnd(new Criteria(typeof(SFDCustomer), "ID", MatchType.Exact, objUpdate.ID));


                    var data = _sFDCustomerMapper.RetrieveByCriteria(criterias);
                    if (data.Count == 0)
                    {
                        result.messages.Add(new MessageBase { ErrorCode = ErrorCode.DataUpdateNotAvailable, ErrorMessage = "Data SFD Customer dengan GUID Update " + objUpdate.GUIDUpdate + " dan ID " + objUpdate.ID + " tidak ditemukan" });
                    }
                    else
                    {
                        var sfdcustomer = data[0] as SFDCustomer;

                        var sfdcity = new City();
                        var sfdcountrycode = new SPKMasterCountryCodePhone();
                        if (!string.IsNullOrEmpty(objUpdate.CountryCode))
                        {
                            var criteriascity = new CriteriaComposite(new Criteria(typeof(City), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                            criteriascity.opAnd(new Criteria(typeof(City), "CityCode", MatchType.Exact, objUpdate.CityCode));
                            var datacity = _cityMapper.RetrieveByCriteria(criteriascity);

                            sfdcity = datacity[0] as City;
                        }
                        if (!string.IsNullOrEmpty(objUpdate.CountryCode))
                        {
                            var criteriascountrycode = new CriteriaComposite(new Criteria(typeof(SPKMasterCountryCodePhone), "CountryCode", MatchType.Exact, objUpdate.CountryCode));
                            var datacountrycode = _spkMasterCountryCodePhoneMapper.RetrieveByCriteria(criteriascountrycode);

                            sfdcountrycode = datacountrycode[0] as SPKMasterCountryCodePhone;
                        }
                        short gender = 0;
                        if(objUpdate.JenisKelamin=="LK")
                        {
                            gender = 1;
                        }else if(objUpdate.JenisKelamin=="PR")
                        {
                            gender = 2;
                        }
                        short interfacecustsales = 0;
                        if(objUpdate.InterfaceCustSales==false)
                        {
                            interfacecustsales = 1;
                        }
                        if (!string.IsNullOrEmpty(objUpdate.GUIDUpdate))
                        {
                            if (sfdcustomer.GUIDUpdate != objUpdate.GUIDUpdate)
                            {
                                sfdcustomer.GUID = objUpdate.GUID;
                                sfdcustomer.CustomerClass = objUpdate.CustomerClass;
                                sfdcustomer.CustomerType = objUpdate.CustomerType;
                                sfdcustomer.ClassType = objUpdate.ClassType;
                                sfdcustomer.LevelData = objUpdate.LevelData;
                                sfdcustomer.LastUpdateTime = DateTime.Now;
                                sfdcustomer.LastUpdateBy = objUpdate.DealerCode;
                                sfdcustomer.CustomerNo = objUpdate.CustomerNumber;

                                //tambahan
                                var identificationtype = 0;
                                var tipeper = new List<int>() { 0, 1, 2, 4, 5 };
                                if (objUpdate.Status1==2)
                                {
                                    identificationtype = 8;
                                }else if (objUpdate.Status1 == 1 && objUpdate.TipePerusahaan==6)
                                {
                                    identificationtype = 6;
                                }
                                else if (objUpdate.Status1 == 1 && objUpdate.TipePerusahaan == 7)
                                {
                                    identificationtype = 7;
                                }else if(objUpdate.Status1==1 && tipeper.Contains(objUpdate.TipePerusahaan))
                                {
                                    identificationtype = 4;
                                }else
                                {
                                    identificationtype = objUpdate.TypeIdentitas;
                                }
                                var subclass = 0;
                                if (objUpdate.Status1 == 0)
                                {
                                    subclass = objUpdate.TypePerorangan;
                                }else if (objUpdate.Status1 == 1)
                                {
                                    subclass = objUpdate.TipePerusahaan;
                                }

                                sfdcustomer.CustomerTypeDNET = objUpdate.Status1;
                                sfdcustomer.CustomerSubClass = Convert.ToInt16(subclass);
                                sfdcustomer.IdentityType = Convert.ToInt16(identificationtype);

                                sfdcustomer.ParentCustomerNo = objUpdate.ParentCustomerNo;
                                sfdcustomer.FirstName = objUpdate.FirstName;
                                sfdcustomer.LastName = objUpdate.LastName;
                                sfdcustomer.SPKMasterCountryCodePhoneID = sfdcountrycode.ID;
                                sfdcustomer.HPNo = objUpdate.PhoneNo;
                                sfdcustomer.OtherPhoneNo = objUpdate.OtherPhoneNo;
                                sfdcustomer.Email = objUpdate.Email;
                                sfdcustomer.Gedung = objUpdate.Gedung;
                                sfdcustomer.Alamat = objUpdate.Alamat;
                                sfdcustomer.Kelurahan = objUpdate.Kelurahan;
                                sfdcustomer.Kecamatan = objUpdate.Kecamatan;
                                sfdcustomer.PostalCode = objUpdate.PostalCode;
                                sfdcustomer.City = sfdcity;
                                sfdcustomer.CreatedBy = sfdcity.ID.ToString();
                                sfdcustomer.POBox = objUpdate.POBox;
                                sfdcustomer.BirthDate = Convert.ToDateTime(objUpdate.TGLLAHIR);
                                sfdcustomer.IdentityNumber = objUpdate.KTP;
                                sfdcustomer.NPWPNo = objUpdate.NPWPNo;
                                sfdcustomer.NPWPName = objUpdate.NPWPName;
                                sfdcustomer.PreArea = objUpdate.PreArea;
                                sfdcustomer.PrintRegion = objUpdate.PrintRegion;
                                sfdcustomer.InterfaceCustSales = interfacecustsales;
                                sfdcustomer.Notes = objUpdate.Notes;
                                sfdcustomer.Gender = gender;

                            }
                            else
                            {
                                sfdcustomer.GUIDUpdate = objUpdate.GUIDUpdate;
                                sfdcustomer.GUID = objUpdate.GUID;
                                sfdcustomer.InterfaceStatus = Convert.ToInt16(objUpdate.InterfaceStatus);
                                sfdcustomer.InterfaceMessage = objUpdate.InterfaceMessage;
                                sfdcustomer.CustomerClass = objUpdate.CustomerClass;
                                sfdcustomer.CustomerType = objUpdate.CustomerType;
                                sfdcustomer.ClassType = objUpdate.ClassType;
                                sfdcustomer.LevelData = objUpdate.LevelData;
                                sfdcustomer.LastUpdateTime = DateTime.Now;
                                sfdcustomer.LastUpdateBy = objUpdate.DealerCode;
                                sfdcustomer.CustomerNo = objUpdate.CustomerNumber;

                                //tambahan
                                var identificationtype = 0;
                                var tipeper = new List<int>() { 0, 1, 2, 4, 5 };
                                if (objUpdate.Status1 == 2)
                                {
                                    identificationtype = 8;
                                }
                                else if (objUpdate.Status1 == 1 && objUpdate.TipePerusahaan == 6)
                                {
                                    identificationtype = 6;
                                }
                                else if (objUpdate.Status1 == 1 && objUpdate.TipePerusahaan == 7)
                                {
                                    identificationtype = 7;
                                            }
                                else if (objUpdate.Status1 == 1 && tipeper.Contains(objUpdate.TipePerusahaan))
                                {
                                    identificationtype = 4;
                                }
                                else
                                {
                                    identificationtype = objUpdate.TypeIdentitas;
                                }
                                var subclass = 0;
                                if (objUpdate.Status1 == 0)
                                {
                                    subclass = objUpdate.TypePerorangan;
                                }
                                else if (objUpdate.Status1 == 1)
                                {
                                    subclass = objUpdate.TipePerusahaan;
                                }

                                sfdcustomer.CustomerTypeDNET = objUpdate.Status1;
                                sfdcustomer.CustomerSubClass = Convert.ToInt16(subclass);
                                sfdcustomer.IdentityType = Convert.ToInt16(identificationtype);

                                sfdcustomer.ParentCustomerNo = objUpdate.ParentCustomerNo;
                                sfdcustomer.FirstName = objUpdate.FirstName;
                                sfdcustomer.LastName = objUpdate.LastName;
                                sfdcustomer.SPKMasterCountryCodePhoneID = sfdcountrycode.ID;
                                sfdcustomer.HPNo = objUpdate.PhoneNo;
                                sfdcustomer.OtherPhoneNo = objUpdate.OtherPhoneNo;
                                sfdcustomer.Email = objUpdate.Email;
                                sfdcustomer.Gedung = objUpdate.Gedung;
                                sfdcustomer.Alamat = objUpdate.Alamat;
                                sfdcustomer.Kelurahan = objUpdate.Kelurahan;
                                sfdcustomer.Kecamatan = objUpdate.Kecamatan;
                                sfdcustomer.PostalCode = objUpdate.PostalCode;
                                sfdcustomer.City = sfdcity;
                                sfdcustomer.CreatedBy = sfdcity.ID.ToString();
                                sfdcustomer.POBox = objUpdate.POBox;
                                sfdcustomer.BirthDate = Convert.ToDateTime(objUpdate.TGLLAHIR);
                                sfdcustomer.IdentityNumber = objUpdate.KTP;
                                sfdcustomer.NPWPNo = objUpdate.NPWPNo;
                                sfdcustomer.NPWPName = objUpdate.NPWPName;
                                sfdcustomer.PreArea = objUpdate.PreArea;
                                sfdcustomer.PrintRegion = objUpdate.PrintRegion;
                                sfdcustomer.InterfaceCustSales = interfacecustsales;
                                sfdcustomer.Notes = objUpdate.Notes;
                                sfdcustomer.Gender = gender;
                            }
                        }
                        else
                        {
                            sfdcustomer.GUID = objUpdate.GUID;
                            sfdcustomer.InterfaceStatus = Convert.ToInt16(objUpdate.InterfaceStatus);
                            sfdcustomer.InterfaceMessage = objUpdate.InterfaceMessage;
                            sfdcustomer.CustomerClass = objUpdate.CustomerClass;
                            sfdcustomer.CustomerType = objUpdate.CustomerType;
                            sfdcustomer.ClassType = objUpdate.ClassType;
                            sfdcustomer.LevelData = objUpdate.LevelData;
                            sfdcustomer.LastUpdateTime = DateTime.Now;
                            sfdcustomer.LastUpdateBy = objUpdate.DealerCode;
                            sfdcustomer.CustomerNo = objUpdate.CustomerNumber;

                            //tambahan
                            var identificationtype = 0;
                            var tipeper = new List<int>() { 0, 1, 2, 4, 5 };
                            if (objUpdate.Status1 == 2)
                            {
                                identificationtype = 8;
                            }
                            else if (objUpdate.Status1 == 1 && objUpdate.TipePerusahaan == 6)
                            {
                                identificationtype = 6;
                            }
                            else if (objUpdate.Status1 == 1 && objUpdate.TipePerusahaan == 7)
                            {
                                identificationtype = 7;
                            }
                            else if (objUpdate.Status1 == 1 && tipeper.Contains(objUpdate.TipePerusahaan))
                            {
                                identificationtype = 4;
                            }
                            else
                            {
                                identificationtype = objUpdate.TypeIdentitas;
                            }
                            var subclass = 0;
                            if (objUpdate.Status1 == 0)
                            {
                                subclass = objUpdate.TypePerorangan;
                            }
                            else if (objUpdate.Status1 == 1)
                            {
                                subclass = objUpdate.TipePerusahaan;
                            }

                            sfdcustomer.CustomerTypeDNET = objUpdate.Status1;
                            sfdcustomer.CustomerSubClass = Convert.ToInt16(subclass);
                            sfdcustomer.IdentityType = Convert.ToInt16(identificationtype);

                            sfdcustomer.ParentCustomerNo = objUpdate.ParentCustomerNo;
                            sfdcustomer.FirstName = objUpdate.FirstName;
                            sfdcustomer.LastName = objUpdate.LastName;
                            sfdcustomer.SPKMasterCountryCodePhoneID = sfdcountrycode.ID;
                            sfdcustomer.HPNo = objUpdate.PhoneNo;
                            sfdcustomer.OtherPhoneNo = objUpdate.OtherPhoneNo;
                            sfdcustomer.Email = objUpdate.Email;
                            sfdcustomer.Gedung = objUpdate.Gedung;
                            sfdcustomer.Alamat = objUpdate.Alamat;
                            sfdcustomer.Kelurahan = objUpdate.Kelurahan;
                            sfdcustomer.Kecamatan = objUpdate.Kecamatan;
                            sfdcustomer.PostalCode = objUpdate.PostalCode;
                            sfdcustomer.City = sfdcity;
                            sfdcustomer.CreatedBy = sfdcity.ID.ToString();
                            sfdcustomer.POBox = objUpdate.POBox;
                            sfdcustomer.BirthDate = Convert.ToDateTime(objUpdate.TGLLAHIR);
                            sfdcustomer.IdentityNumber = objUpdate.KTP;
                            sfdcustomer.NPWPNo = objUpdate.NPWPNo;
                            sfdcustomer.NPWPName = objUpdate.NPWPName;
                            sfdcustomer.PreArea = objUpdate.PreArea;
                            sfdcustomer.PrintRegion = objUpdate.PrintRegion;
                            sfdcustomer.InterfaceCustSales = interfacecustsales;
                            sfdcustomer.Notes = objUpdate.Notes;
                            sfdcustomer.Gender = gender;
                        }

                        var nResult = _CustomerVehicleRepo.Update(sfdcustomer);
                        if (nResult.Success == true)
                        {
                            result.success = true;
                            result.total = 1;
                            result._id = Convert.ToInt32(sfdcustomer.ID);

                            if (!string.IsNullOrEmpty(objUpdate.GUIDUpdate))
                            {
                                using (SqlConnection con = new SqlConnection(connectionString))
                                {
                                    using (SqlCommand cmd = new SqlCommand("SP_SFID_UpdateQueueStatus", con))
                                    {
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.Add("@DnetID", SqlDbType.Int).Value = objUpdate.ID;
                                        cmd.Parameters.Add("@SalesmanHeaderID", SqlDbType.Int).Value = 0;
                                        cmd.Parameters.Add("@GUIDUpdate", SqlDbType.VarChar).Value = objUpdate.GUIDUpdate;

                                        con.Open();
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }
                            }
                        }
                        else
                        {
                            ErrorMsgHelper.ErrorMsgDBSave(result.messages);
                        }
                    }
                }
                else
                {
                    return PopulateValidationError<CustomerVehicleDto>(validationResults, null);
                }
            }
            catch (SqlException ex)
            {
                ErrorMsgHelper.SqlException(result.messages, ex.Message);
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.SqlException(result.messages, ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Get CustomerRequest by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<CustomerVehicleDto>> Read(CustomerVehicleFilterDto filterDto, int pageSize)
        {
            return null;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<CustomerVehicleDto> Delete(int id)
        {
            return null;
        }

        public ResponseBase<CustomerVehicleUploadImageDto> UploadImage(CustomerUploadImageParameterDto param)
        {
            #region Initialize
            // set default response
            var result = new ResponseBase<CustomerVehicleUploadImageDto>();
            var obj = new CustomerVehicleUploadImageDto();
            var attachment = new AttachmentParameterDto();
            string filePath = string.Empty;
            byte[] fileBytes = null;
            List<DNetValidationResult> validationResults = new List<DNetValidationResult>();
            #endregion

            try
            {
                attachment.FileName = param.FileName;
                attachment.Base64OfStream = param.Base64OfStream;

                // validate the evidence file if exists
                if (attachment != null)
                {
                    validationResults.AddRange(FileUtility.ValidateEvidenceOrIdentityFile(attachment, _mapper, out fileBytes, FieldResource.IdentityFile));
                }
                else
                {
                    validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.IdentityFile)));
                }

                // return if any errors found
                if (validationResults.Any())
                {
                    return null;
                }

                if (fileBytes != null)
                {
                    // save the file
                    string uploadErrorMessage = FileUtility.SaveIdentityFileCustom(attachment, DealerCode, fileBytes, param.DocType, out filePath);
                    if (!string.IsNullOrEmpty(uploadErrorMessage))
                    {
                        validationResults.Add(new DNetValidationResult(uploadErrorMessage));
                    }

                    // return if any errors found
                    if (validationResults.Any())
                    {
                        return null;
                    }

                    obj.ImagePath = filePath;
                    result.success = true;
                    result.lst = obj;
                }
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }

        #endregion

        #region Private Methods

        #region Transaction Managers
        /// <summary>
        /// Insert transaction manager
        /// </summary>
        /// <param name="customerRequest"></param>
        /// <param name="customerRequestProfileList"></param>
        /// <param name="customerRequestProfileHistoryList"></param>
        /// <returns></returns>
        private int InsertWithTransactionManager(CustomerRequest customerRequest, List<CustomerRequestProfile> customerRequestProfileList, List<CustomerRequestProfileHistory> customerRequestProfileHistoryList, CustomerStatusHistory customerStatusHistory)
        {
            int result = -1;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // add command to insert customer request
                    this._transactionManager.AddInsert(customerRequest, DNetUserName);

                    // add command to insert customer request profile
                    foreach (CustomerRequestProfile customerProfile in customerRequestProfileList)
                    {
                        this._transactionManager.AddInsert(customerProfile, DNetUserName);
                    }

                    // add command to insert customer request profile
                    foreach (CustomerRequestProfileHistory customerProfileHistory in customerRequestProfileHistoryList)
                    {
                        this._transactionManager.AddInsert(customerProfileHistory, DNetUserName);
                    }

                    // add customer status history
                    this._transactionManager.AddInsert(customerStatusHistory, DNetUserName);

                    this._transactionManager.PerformTransaction();
                    result = customerRequest.ID;
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
        /// Insert OCRIdentity Data
        /// </summary>
        /// <param name="ocrIdentity"></param>
        /// <returns></returns>
        private int InsertWithTransactionManager(OCRIdentity ocrIdentity)
        {
            int result = -1;
            if (this.IsTaskFree())
            {
                try
                {
                    // reinstantiate
                    this._transactionManager = new TransactionManager();
                    _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);

                    this.SetTaskLocking();

                    // set 0 as default
                    ocrIdentity.SPKCustomerID = 0;

                    // add command to insert customer request ocr                    
                    this._transactionManager.AddInsert(ocrIdentity, DNetUserName);
                    this._transactionManager.PerformTransaction();
                    result = ocrIdentity.ID;
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
        /// Insert Customer Request OCR Data
        /// </summary>
        /// <param name="customerRequestOCR"></param>
        /// <returns></returns>
        private int InsertWithTransactionManager(CustomerRequestOCR customerRequestOCR)
        {
            int result = -1;
            if (this.IsTaskFree())
            {
                try
                {
                    // reinstantiate
                    this._transactionManager = new TransactionManager();
                    _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);

                    this.SetTaskLocking();

                    if (customerRequestOCR.ID == 0)
                    {
                        // add command to insert customer request ocr
                        this._transactionManager.AddInsert(customerRequestOCR, DNetUserName);
                    }
                    else
                    {
                        // add command to update customer request ocr
                        this._transactionManager.AddUpdate(customerRequestOCR, DNetUserName);
                    }

                    this._transactionManager.PerformTransaction();
                    result = customerRequestOCR.ID;
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
        /// Insert with transaction manager handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void InsertWithTransactionManagerHandler(object sender, TransactionManager.OnInsertArgs args)
        {
            // set the object ID from db returned id
            if (args.DomainObject.GetType() == typeof(CustomerRequest))
            {
                ((CustomerRequest)args.DomainObject).ID = args.ID;
                ((CustomerRequest)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(CustomerRequestProfile))
            {
                ((CustomerRequestProfile)args.DomainObject).ID = args.ID;
                ((CustomerRequestProfile)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(CustomerRequestProfileHistory))
            {
                ((CustomerRequestProfileHistory)args.DomainObject).ID = args.ID;
                ((CustomerRequestProfileHistory)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(CustomerRequestOCR))
            {
                ((CustomerRequestOCR)args.DomainObject).ID = args.ID;
                ((CustomerRequestOCR)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(OCRIdentity))
            {
                ((OCRIdentity)args.DomainObject).ID = args.ID;
                ((OCRIdentity)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(SFDCustomer))
            {
                ((OCRIdentity)args.DomainObject).ID = args.ID;
                ((OCRIdentity)args.DomainObject).MarkLoaded();
            }
        }

        /// <summary>
        /// Update transaction manager
        /// </summary>
        /// <param name="customerRequest"></param>
        /// <param name="customerRequestProfileList"></param>
        /// <param name="customerRequestProfileHistoryList"></param>
        /// <returns></returns>
        private int UpdateWithTransactionManager(CustomerRequest customerRequest, List<CustomerRequestProfile> customerRequestProfileList, List<CustomerRequestProfileHistory> customerRequestProfileHistoryList)
        {
            // mark as loaded to prevent it loads from db
            customerRequest.MarkLoaded();

            // set default result
            int result = -1;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // add command to insert customer request profile
                    foreach (CustomerRequestProfile profile in customerRequestProfileList)
                    {
                        if (profile.ID != 0)
                        {
                            _transactionManager.AddUpdate(profile, DNetUserName);
                        }
                        else
                        {
                            _transactionManager.AddInsert(profile, DNetUserName);
                        }

                        profile.MarkLoaded();
                    }

                    // add command to insert customer request profile history
                    foreach (CustomerRequestProfileHistory history in customerRequestProfileHistoryList)
                    {
                        // history should be stored as new record 
                        this._transactionManager.AddInsert(history, DNetUserName);

                        history.MarkLoaded();
                    }

                    // add command to update spk
                    _transactionManager.AddUpdate(customerRequest, DNetUserName);
                    _transactionManager.PerformTransaction();
                    result = customerRequest.ID;
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
        /// Update transaction manager
        /// </summary>
        /// <param name="sFDCustomer"></param>
        /// <returns></returns>
        private int UpdateWithTransactionManager(SFDCustomer sFDCustomer)
        {
            // mark as loaded to prevent it loads from db
            sFDCustomer.MarkLoaded();

            // set default result
            int result = -1;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // add command to update spk
                    _transactionManager.AddUpdate(sFDCustomer, DNetUserName);
                    _transactionManager.PerformTransaction();
                    result = Convert.ToInt32(sFDCustomer.ID);
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
        #endregion

        /// <summary>
        /// Save customer request ocr file
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="ocrIdentity"></param>
        /// <param name="custRequestID"></param>
        private void SaveFileCustomerRequest(CustomerVehicleParameterDto objCreate, List<DNetValidationResult> validationResults, ref OCRIdentity ocrIdentity, int custRequestID, byte[] fileBytes)
        {
            string filePath = null;

            string requestNo = GetRequestNumber(custRequestID);
            string uploadErrorMessage = FileUtility.SaveCustomerRequestFile(objCreate.OCRIdentity.IdentityFile, requestNo, fileBytes, out filePath);
            if (!string.IsNullOrEmpty(uploadErrorMessage))
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.DataTypeOrDataFormatInvalid, string.Format(MessageResource.ErrorMsgDataType, uploadErrorMessage)));
            }
            else
            {
                // create ocr object based on parameter
                ocrIdentity = objCreate.OCRIdentity.ConvertObject<OCRIdentity>(ocrIdentity);
                ocrIdentity.ImagePath = filePath;
            }
        }

        /// <summary>
        /// Validate passed customer code parameter if request type REVISI
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        /// <param name="customer"></param>
        private bool ValidateCustomerByRequestType(string customerCode, string reffCode, int requestType, List<DNetValidationResult> validationResults, bool isMandatory = false)
        {
            // only validate for revision type
            if (requestType == 0 && !isMandatory)
                return true;

            // get by criteria
            var masters = _customerMapper.RetrieveByCriteria(Helper.GenerateCriteriaAllStatus(typeof(Customer), "Code", customerCode));
            if (masters.Count > 0)
            {
                var refmasters = _customerMapper.RetrieveByCriteria(Helper.GenerateCriteriaAllStatus(typeof(Customer), "Code", reffCode));
                if (refmasters.Count > 0)
                {
                    // cast the object
                    var customer = refmasters[0] as Customer;
                    if (customer.RowStatus == -1)
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataIsNotActive, FieldResource.Customer, reffCode)));
                    }
                }
                else
                {
                    validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.ReffCode)));
                }
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.KodePelanggan, customerCode)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate Customer by Reff Code
        /// </summary>
        /// <param name="reffCode"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidateCustomerByRefCode(string reffCode, List<DNetValidationResult> validationResults)
        {
            // only validate if provided just follow dnet funny logic -_-
            if (string.IsNullOrEmpty(reffCode))
                return true;

            // get by criteria
            var masters = _customerMapper.RetrieveByCriteria(Helper.GenerateCriteriaAllStatus(typeof(Customer), "Code", reffCode));
            if (masters.Count > 0)
            {
                // cast the object
                var customer = masters[0] as Customer;
                if (customer.RowStatus == -1)
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataIsNotActive, FieldResource.Customer, reffCode)));
                }
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.Customer, reffCode)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate customer request by ref request no if any
        /// </summary>
        /// <param name="refRequestNo"></param>
        /// <param name="dealerCode"></param>
        /// <param name="validationResults"></param>
        /// <param name="isMandatory"></param>
        /// <returns></returns>
        private bool ValidateCustomerRequestByRefRequestNo(string refRequestNo, string dealerCode, List<DNetValidationResult> validationResults, bool isMandatory = false)
        {
            // no need to validate if not mandatory
            if (string.IsNullOrEmpty(refRequestNo) && !isMandatory)
            {
                return true;
            }

            // get by criteria
            var masters = _customerRequestMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(CustomerRequest), "RefRequestNo", "Dealer.DealerCode", refRequestNo, dealerCode));
            if (masters.Count == 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrence, FieldResource.CustomerRequest, refRequestNo, dealerCode)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate OCR Identity
        /// </summary>
        /// <param name="param"></param>
        /// <param name="validationResults"></param>
        /// <param name="ocrIdentity"></param>        
        /// <param name="isNewOcrIdentitry"></param>
        private bool ValidateOCR(CustomerVehicleParameterDto param, List<DNetValidationResult> validationResults, ref OCRIdentity ocrIdentity, ref bool isNewOcrIdentitry)
        {
            if (param.OCRIdentity == null)
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.OCRIdentity)));
            }
            else
            {
                if (ValidationHelper.ValidateOCRIdentityServer(param.OCRIdentity.IdentityType, param.OCRIdentity.ImageID, validationResults))
                {
                    if (!ValidationHelper.ValidateOCRIdentity(param.OCRIdentity.ImageID, validationResults, ref ocrIdentity, true))
                    {
                        // set flag as new ocr
                        isNewOcrIdentitry = true;
                    }
                }
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Update profile and history
        /// </summary>
        /// <param name="customerStatusHistory"></param>
        /// <param name="customerRequestProfiles"></param>
        /// <param name="customerRequestProfileHistories"></param>
        /// <param name="newCustomerRequest"></param>
        private void UpdateProfileHistoryAndStatusHistory(CustomerStatusHistory customerStatusHistory, List<CustomerRequestProfile> customerRequestProfiles, List<CustomerRequestProfileHistory> customerRequestProfileHistories, CustomerRequest newCustomerRequest)
        {
            // populate profiles
            foreach (CustomerRequestProfile profile in customerRequestProfiles)
            {
                profile.CustomerRequest = newCustomerRequest;
                profile.CreatedTime = DateTime.Now;

                // add history
                CustomerRequestProfileHistory history = new CustomerRequestProfileHistory
                {
                    CustomerRequestProfile = profile,
                    ProvileValue = profile.ProfileValue,
                    CreatedBy = DNetUserName,
                    CreatedTime = DateTime.Now,

                };

                customerRequestProfileHistories.Add(history);
            }

            if (customerStatusHistory != null)
            {
                // update status history
                customerStatusHistory.CustomerRequest = newCustomerRequest;
                customerStatusHistory.OldStatus = 99;
                customerStatusHistory.NewStatus = (byte)EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Baru;
            }
        }

        /// <summary>
        /// Validate enum
        /// </summary>
        /// <param name="model"></param>
        /// <param name="validationResults"></param>
        /// <param name="tipePelangganStdCode"></param>
        /// <returns></returns>
        private bool ValidateEnum(ref CustomerVehicleParameterDto model, List<DNetValidationResult> validationResults, ref StandardCodeDto tipePelangganStdCode)
        {
            if (!_enumBL.IsExistByCategoryAndValue("EnumTipePelanggan", ((int)(model.Status1)).ToString()))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.TipePelanggan)));
            }
            else
            {
                // get tipe pelanggan standard code
                tipePelangganStdCode = _enumBL.GetByCategoryAndValue("EnumTipePelanggan", model.Status1.ToString());
                if (tipePelangganStdCode.ValueCode.Equals("Perusahaan"))
                {
                    // if tipe pelanggan is perusahaan, the tipe perusahaan should not be empty or invalid
                    if (!_enumBL.IsExistByCategoryAndValue("EnumTipePerusahaan", ((int)(model.TipePerusahaan)).ToString()))
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.TipePerusahaan)));
                    }
                }
            }

            if (!_enumBL.IsExistByCategoryAndValue("EnumStatusCustomerRequest.TipePengajuanCustomerRequest", ((int)(model.Status)).ToString())) { validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.TipePengajuan))); }

            if (!string.IsNullOrEmpty(model.PreArea) && !_enumBL.IsExistByCategoryAndCode("EnumPreArea", model.PreArea)) { validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.PreArea))); }

            return validationResults.Count == 0;
        }

        ///// <summary>
        ///// Validate enum
        ///// </summary>
        ///// <param name="model"></param>
        ///// <param name="validationResults"></param>
        ///// <param name="tipePelangganStdCode"></param>
        ///// <returns></returns>
        //private bool ValidateEnum(ref CustomerVehicleUpdateParameterDto model, List<DNetValidationResult> validationResults, ref StandardCodeDto tipePelangganStdCode)
        //{
        //    if (!_enumBL.IsExistByCategoryAndValue("EnumTipePelanggan", ((int)(model.Status1)).ToString()))
        //    {
        //        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.TipePelanggan)));
        //    }
        //    else
        //    {
        //        // get tipe pelanggan standard code
        //        tipePelangganStdCode = _enumBL.GetByCategoryAndValue("EnumTipePelanggan", model.Status1.ToString());
        //        if (tipePelangganStdCode.ValueCode.Equals("Perusahaan"))
        //        {
        //            // if tipe pelanggan is perusahaan, the tipe perusahaan should not be empty or invalid
        //            if (!_enumBL.IsExistByCategoryAndValue("EnumTipePerusahaan", ((int)(model.TipePerusahaan)).ToString()))
        //            {
        //                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.TipePerusahaan)));
        //            }
        //        }
        //    }

        //    if (!_enumBL.IsExistByCategoryAndValue("EnumStatusCustomerRequest.TipePengajuanCustomerRequest", ((int)(model.Status)).ToString())) { validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.TipePengajuan))); }

        //    if (!string.IsNullOrEmpty(model.PreArea) && !_enumBL.IsExistByCategoryAndCode("EnumPreArea", model.PreArea)) { validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.PreArea))); }

        //    return validationResults.Count == 0;
        //}

        /// <summary>
        /// Validate customer request profiles
        /// </summary>
        /// <param name="param"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidateCustomerRequestProfiles(CustomerVehicleParameterDto param, List<CustomerRequestProfile> customerRequestProfileList, List<DNetValidationResult> validationResults)
        {
            var profileGroupResponse = new ResponseBase<ProfileGroupDto>();
            string validataionTypeIdentity = string.Empty;

            switch (param.Status1)
            {
                case (short)EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perorangan:

                    profileGroupResponse = new ProfileGroupBL(_mapper).GetByCode(Constants.ProfileGroup.CustomerDatabasePerorangan);
                    if (profileGroupResponse.success)
                    {
                        /* Validate TypePerorangan & TypeIdentitas */
                        validataionTypeIdentity = ValidateTypePeroranganTypeIdentity(param, validationResults);

                        ValidateCustomerRequestProfile(param, validationResults, customerRequestProfileList, profileGroupResponse, EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perorangan, validataionTypeIdentity);
                    }
                    break;

                case (short)EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perusahaan:

                    profileGroupResponse = new ProfileGroupBL(_mapper).GetByCode(Constants.ProfileGroup.CustomerDatabasePerusahaan);
                    if (profileGroupResponse.success)
                    {
                        if (param.TipePerusahaan < 6) /* PT PP CV UD PF */
                        {
                            param.TypeIdentitas = 4;  /* TDP */
                        }
                        else if (param.TipePerusahaan == 6) /* YAYASAN */
                        {
                            param.TypeIdentitas = 5;  /* TDY */
                        }
                        else  /* KOPERASI */
                        {
                            param.TypeIdentitas = 6;  /* SIK */
                        }

                        ValidateCustomerRequestProfile(param, validationResults, customerRequestProfileList, profileGroupResponse, EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perusahaan);
                    }
                    break;

                case (short)EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah:

                    profileGroupResponse = new ProfileGroupBL(_mapper).GetByCode(Constants.ProfileGroup.CustomerDatabasBUMN);
                    if (profileGroupResponse.success)
                    {
                        ValidateCustomerRequestProfile(param, validationResults, customerRequestProfileList, profileGroupResponse, EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.BUMN_Pemerintah);
                    }
                    break;

                case (short)EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Lainnya:

                    profileGroupResponse = new ProfileGroupBL(_mapper).GetByCode(Constants.ProfileGroup.CustomerDatabaseLainnya);
                    if (profileGroupResponse.success)
                    {
                        ValidateCustomerRequestProfile(param, validationResults, customerRequestProfileList, profileGroupResponse, EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Lainnya);
                    }
                    break;

                default:
                    break;
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate customer request profile
        /// </summary>
        /// <param name="param"></param>
        /// <param name="validationResults"></param>
        /// <param name="customerRequestProfileList"></param>
        /// <param name="profileGroupResponse"></param>
        /// <param name="tipePelanggan"></param>
        private void ValidateCustomerRequestProfile(CustomerVehicleParameterDto param, List<DNetValidationResult> validationResults, List<CustomerRequestProfile> customerRequestProfileList, ResponseBase<ProfileGroupDto> profileGroupResponse, EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest tipePelanggan, string validataionTypeIdentity = "")
        {
            List<StandardCodeDto> enumMandatoryMode = _enumBL.GetByCategory("EnumMandatory.MandatoryMode");

            foreach (var group in profileGroupResponse.lst.ProfileHeaderToGroups)
            {
                dynamic value;
                // mapping for profileheader validation purpose
                var prop = param.GetType().GetProperty(group.ProfileHeader.Code.Replace("NOKTP", "KTP").Replace("JK", "JenisKelamin").Replace("PENDIDIKAN", "Pendidikan").Replace("NOTELP", "PhoneNo"));
                if (prop != null)
                {
                    value = prop.GetValue(param, null);

                    if (group.ProfileHeader.Mandatory == (short)enumMandatoryMode.Where(s => s.ValueCode.Equals("Benar", StringComparison.OrdinalIgnoreCase)).SingleOrDefault().ValueId && string.IsNullOrEmpty(value.ToString()))
                    {
                        validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.ResourceManager.GetString(group.ProfileHeader.Code))));
                        continue;
                    }

                    if (group.ProfileHeader.Code == "TGLLAHIR")
                    {
                        value = Convert.ToDateTime(value).ToString("dd/MM/yyyy");
                    }

                    if (group.ProfileHeader.Code.Equals("NOKTP", StringComparison.OrdinalIgnoreCase))
                    {
                        if (tipePelanggan == EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perorangan)
                        {
                            if (validataionTypeIdentity == "KTP")
                            {
                            if (param.KTP.Trim().Length != 16)
                            {
                                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ResourceManager.GetString(group.ProfileHeader.Code))));
                                }
                                else
                                {
                                    string TglLahirKTP = param.KTP.Substring(6, 6);
                                    string TglLahir = string.Empty;

                                    if (param.JenisKelamin == "PR")
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
                                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgTglLahirNotMatchKTP, param.KTP, Convert.ToDateTime(param.TGLLAHIR).ToString("yyyy-MM-dd"))));
                                    }
                                }
                            }
                            else if (validataionTypeIdentity == "SIM")
                            {
                                if (param.KTP.Trim().Length < 9 || param.KTP.Trim().Length > 17)
                                {
                                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ResourceManager.GetString(group.ProfileHeader.Code))));
                                }
                            }
                            else if ((validataionTypeIdentity == "KITAS" || validataionTypeIdentity == "KITAP"))
                            {
                                if (param.KTP.Trim().Length > 11)
                                {
                                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ResourceManager.GetString(group.ProfileHeader.Code))));
                                }
                            }

                            /* Comment CR CDP & SPK Enhancement
                            if (param.KTP.Trim().Length != 16)
                            {
                                validationResults.Add(new DNetValidationResult(ErrorCode.DataTypeOrDataFormatInvalid, string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ResourceManager.GetString(group.ProfileHeader.Code))));
                            }
                            */
                        }
                        else
                        {
                            if (param.KTP.Trim().Length < 8 || param.KTP.Trim().Length > 40)
                            {
                                validationResults.Add(new DNetValidationResult(ErrorCode.DataTypeOrDataFormatInvalid, string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ResourceManager.GetString(group.ProfileHeader.Code))));
                            }
                        }
                    }
                    else if (group.ProfileHeader.Code.Equals("NOTELP", StringComparison.OrdinalIgnoreCase))
                    {
                        if (param.PhoneNo.Length < 6 || param.PhoneNo.Substring(0, 2) == "00")
                        {
                            validationResults.Add(new DNetValidationResult(ErrorCode.DataTypeOrDataFormatInvalid, string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ResourceManager.GetString(group.ProfileHeader.Code))));
                        }
                    }
                    else if (group.ProfileHeader.Code.Equals("JK", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(param.JenisKelamin))
                    {
                        var detail = GetProfileDetail(param.JenisKelamin, 27);
                        if (detail == null)
                        {
                            validationResults.Add(new DNetValidationResult(ErrorCode.DataTypeOrDataFormatInvalid, string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.Gender)));
                        };
                    }
                    else if (group.ProfileHeader.Code.Equals("PENDIDIKAN", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(param.Pendidikan))
                    {
                        var detail = GetProfileDetail(param.Pendidikan, 31);
                        if (detail == null)
                        {
                            validationResults.Add(new DNetValidationResult(ErrorCode.DataTypeOrDataFormatInvalid, string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.PENDIDIKAN)));
                        };
                    }

                    customerRequestProfileList.Add(GetValidCustomerRequestProfile(param.ID, group.ProfileHeader.ID, group.ProfileGroup.ID, value.ToString()));
                }
            }
        }

        /// <summary>
        /// Create or update customer req profile
        /// </summary>
        /// <param name="customerRequestID"></param>
        /// <param name="profileHeaderID"></param>
        /// <param name="profileGroupID"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private CustomerRequestProfile GetValidCustomerRequestProfile(int customerRequestID, int profileHeaderID, int profileGroupID, string value)
        {
            if (customerRequestID == 0)
            {
                var objProHeader = _profileHeaderMapper.Retrieve(profileHeaderID);
                var objProGroup = _profileGroupMapper.Retrieve(profileGroupID);

                CustomerRequestProfile newCustReqProfile = new CustomerRequestProfile()
                {
                    ID = 0,
                    ProfileGroup = (ProfileGroup)objProGroup,
                    ProfileHeader = (ProfileHeader)objProHeader,
                    ProfileValue = value
                };

                return newCustReqProfile;
            }
            else
            {
                CustomerRequestProfile existing = GetCustomerRequestProfile(customerRequestID, profileHeaderID, profileGroupID);
                if (existing != null)
                {
                    existing.ProfileValue = value;
                    return existing;
                }
            }

            return null;
        }

        /// <summary>
        /// Validate pre area
        /// </summary>
        /// <param name="model"></param>
        /// <param name="city"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidatePreArea(CustomerVehicleParameterDto model, City city, List<DNetValidationResult> validationResults)
        {
            int cityLength = city.CityName.Length;

            if (cityLength > 31 && !string.IsNullOrEmpty(model.PreArea))
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.DataTypeOrDataFormatInvalid, string.Format(ValidationResource.ValidationEmptyPreArea, FieldResource.PreArea)));
            }
            else if (cityLength == 30 && !string.IsNullOrEmpty(model.PreArea) && model.PreArea.Length > 4)
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.DataTypeOrDataFormatInvalid, string.Format(ValidationResource.ValidationUseKabKota, FieldResource.PreArea)));
            }
            else if (cityLength == 31 && !string.IsNullOrEmpty(model.PreArea) && model.PreArea.Length > 3)
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.DataTypeOrDataFormatInvalid, string.Format(ValidationResource.ValidationUseKab, FieldResource.PreArea)));
            }
            else if (cityLength < 30 && !string.IsNullOrEmpty(model.PreArea) && model.PreArea.Length > 5)
            {
                if (cityLength > 25)
                {
                    validationResults.Add(new DNetValidationResult(ErrorCode.DataTypeOrDataFormatInvalid, string.Format(ValidationResource.ValidationUseKabKotaKodya, FieldResource.PreArea)));
                }
            }

            return validationResults.Count == 0;
        }


        /// <summary>
        /// Get Profile Header
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string GetRequestNumber(int id)
        {
            var custReq = _customerRequestMapper.Retrieve(id);
            if (custReq != null)
            {
                return (custReq as CustomerRequest).RequestNo;
            }

            return string.Empty;
        }

        /// <summary>
        /// Get customer request OCR
        /// </summary>
        /// <param name="customerRequestID"></param>        
        /// <returns></returns>
        private CustomerRequestOCR GetCustomerRequestOCR(int customerRequestID)
        {
            CustomerRequestOCR result = null;
            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(CustomerRequestOCR), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias.opAnd(new Criteria(typeof(CustomerRequestOCR), "CustomerRequestID", MatchType.Exact, customerRequestID));

            var masters = _profileDetailMapper.RetrieveByCriteria(criterias);
            if (masters.Count > 0)
            {
                result = masters[0] as CustomerRequestOCR;
            }

            return result;
        }

        /// <summary>
        /// Get profile detail
        /// </summary>
        /// <param name="code"></param>
        /// <param name="id"></param>        
        /// <returns></returns>
        private ProfileDetail GetProfileDetail(string code, int id)
        {
            ProfileDetail detail = null;

            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(ProfileDetail), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias.opAnd(new Criteria(typeof(ProfileDetail), "ProfileHeader.ID", MatchType.Exact, id));
            criterias.opAnd(new Criteria(typeof(ProfileDetail), "Code", MatchType.Exact, code));

            var profileDetails = _profileDetailMapper.RetrieveByCriteria(criterias);
            if (profileDetails.Count > 0)
            {
                detail = profileDetails[0] as ProfileDetail;
            }

            return detail;
        }

        /// <summary>
        /// Get customer request profile
        /// </summary>
        /// <param name="customerRequestID"></param>
        /// <param name="profileHeaderID"></param>
        /// <param name="profileGroupID"></param>
        /// <returns></returns>
        private CustomerRequestProfile GetCustomerRequestProfile(int customerRequestID, int profileHeaderID, int profileGroupID)
        {
            CustomerRequestProfile profile = null;

            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(CustomerRequestProfile), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias.opAnd(new Criteria(typeof(CustomerRequestProfile), "CustomerRequest.ID", MatchType.Exact, customerRequestID));
            criterias.opAnd(new Criteria(typeof(CustomerRequestProfile), "ProfileHeader.ID", MatchType.Exact, profileHeaderID));
            criterias.opAnd(new Criteria(typeof(CustomerRequestProfile), "ProfileGroup.ID", MatchType.Exact, profileGroupID));

            var customerRequestProfiles = _customerRequestProfileMapper.RetrieveByCriteria(criterias);
            if (customerRequestProfiles.Count > 0)
            {
                profile = customerRequestProfiles[0] as CustomerRequestProfile;
            }

            return profile;
        }

        /// <summary>
        /// Get Profile Header
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private ProfileHeader GetProfileHeader(int id)
        {
            ProfileHeader profileHeader = null;
            var criterias = new CriteriaComposite(new Criteria(typeof(ProfileHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(ProfileHeader), "ID", MatchType.Exact, id));
            var result = new ResponseBase<List<ProfileHeader>>();

            var list = _profileHeaderMapper.RetrieveByCriteria(criterias);
            if (list.Count > 0)
            {
                profileHeader = (list[0] as ProfileHeader);
            }

            return profileHeader;
        }

        /// <summary>
        /// Get Profile Group
        /// </summary>
        /// <param name="model"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private ProfileGroup GetProfileGroup(CustomerVehicleParameterDto model, List<DNetValidationResult> validationResults)
        {
            ProfileGroup profileGroup = null;
            var criterias = new CriteriaComposite(new Criteria(typeof(ProfileGroup), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(ProfileGroup), "ID", MatchType.Exact, model.KategoriGroup));
            var result = new ResponseBase<List<ProfileGroup>>();

            var list = _profileGroupMapper.RetrieveByCriteria(criterias);
            if (list.Count > 0)
            {
                profileGroup = (list[0] as ProfileGroup);
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.GroupCategory)));
            }

            return profileGroup;
        }

        /// <summary>
        /// Set process date
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private DateTime SetProcessDate(CustomerVehicleParameterDto model)
        {
            DateTime tglProcess = DateTime.ParseExact("19750101", "yyyyMMdd", CultureInfo.InvariantCulture);

            if (!model.ProcessDate.HasValue) return tglProcess;

            return model.ProcessDate.Value;
        }

        private bool ValidatePhoneAndCountryCode(CustomerVehicleParameterDto param, List<DNetValidationResult> results)
        {
            bool isValid = true;
            if (!string.IsNullOrEmpty(param.CountryCode))
            {
                var criterias = new CriteriaComposite(new Criteria(typeof(SPKMasterCountryCodePhone), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(SPKMasterCountryCodePhone), "CountryCode", MatchType.Exact, param.CountryCode));
                var data = _spkMasterCountryCodePhoneMapper.RetrieveByCriteria(criterias);

                if (data.Count < 1)
                {
                    isValid = false;
                    results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMessageDataNotFoundWithColumn, FieldResource.CountryCode, "code " + param.CountryCode)));
                }
                else
                {
                    string PhoneNumber = param.PhoneNo.Trim();
                    if (PhoneNumber.Substring(0, 1) == "0")
                    {
                        PhoneNumber = PhoneNumber.Remove(0, 1);
                    }

                    if (PhoneNumber.Length > 13 || PhoneNumber.Length < 9)
                    {
                        isValid = false;
                        results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataPhoneLength, FieldResource.Phone)));
                    }
                }
            }
            else
            {
                isValid = false;
                results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.CountryCode)));
            }

            return isValid;
        }

        private string ValidateTypePeroranganTypeIdentity(CustomerVehicleParameterDto param, List<DNetValidationResult> validationResults)
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

        public ResponseBase<CustomerVehicleDto> Update(CustomerVehicleParameterDto objUpdate)
        {
            throw new NotImplementedException();
        }
        
        #endregion
    }
}

