#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SAPCustomer business logic class
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
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using System.Configuration;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class SAPCustomerBL : AbstractBusinessLogic, ISAPCustomerBL
    {
        #region Variables
        private ISAPCustomerRepository<VWI_LeadCustomerSalesForce, int> _sAPCustomerRepo;
        private ISAPCustomerLeadRepository<KTB.DNet.Interface.Domain.SAPCustomer, int> _sAPCustomerLeadRepo;
        private readonly IMapper _sapCustomerMapper;
        private readonly IMapper _subCategoryVehicleMapper;
        private readonly IMapper _vehicleModelMapper;
        private readonly IMapper _trLeadStatusToStatusMapper;
        private readonly IMapper _businessSectorDetailMapper;
        private readonly IMapper _vwiLeadCustomerSalesForceMapper;
        private readonly IMapper _vechileTypeMapper;
        private readonly IMapper _appConfigMapper;
        private readonly IMapper _dealerSystemsMapper;
        private readonly ITrLeadStatusToStatusRepository<TrLeadStatusToStatus, int> _trLeadStatusToStatusRepo;
        private readonly ITrStatusToLeadStatusCodeToLeadStateCodeRepository<TrStatusToLeadStatusCodeToLeadStateCode, int> _trStatusToLeadStatusCodeToLeadStateCodeRepo;
        private readonly AutoMapper.IMapper _mapper;
        private StandardCodeBL _enumBL;
        private readonly IMapper _spkMasterCountryCodePhoneMapper;
        public string connectionString = "Data Source=172.17.31.122;Initial Catalog=BSIDNET_MMKSI_QA_Release;User id=admin;Password=hunter2;";

        #endregion

        #region Constructor
        public SAPCustomerBL(ITrLeadStatusToStatusRepository<TrLeadStatusToStatus, int> trLeadStatusToStatusRepo,
          ITrStatusToLeadStatusCodeToLeadStateCodeRepository<TrStatusToLeadStatusCodeToLeadStateCode, int> trStatusToLeadStatusCodeToLeadStateCodeRepo, ISAPCustomerRepository<VWI_LeadCustomerSalesForce, int> sAPCustomerRepository, ISAPCustomerLeadRepository<KTB.DNet.Interface.Domain.SAPCustomer, int> sAPCustomerLeadRepository)
        {
            _appConfigMapper = MapperFactory.GetInstance().GetMapper(typeof(AppConfig).ToString());
            _dealerSystemsMapper = MapperFactory.GetInstance().GetMapper(typeof(DealerSystems).ToString());
            _sapCustomerMapper = MapperFactory.GetInstance().GetMapper(typeof(SAPCustomer).ToString());
            _businessSectorDetailMapper = MapperFactory.GetInstance().GetMapper(typeof(BusinessSectorDetail).ToString());
            _vwiLeadCustomerSalesForceMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_LeadCustomerSalesForce).ToString());
            _vechileTypeMapper = MapperFactory.GetInstance().GetMapper(typeof(VechileType).ToString());
            _subCategoryVehicleMapper = MapperFactory.GetInstance().GetMapper(typeof(SubCategoryVehicle).ToString());
            _vehicleModelMapper = MapperFactory.GetInstance().GetMapper(typeof(VechileModel).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _enumBL = new StandardCodeBL(_mapper);

            _trLeadStatusToStatusRepo = trLeadStatusToStatusRepo;
            _trStatusToLeadStatusCodeToLeadStateCodeRepo = trStatusToLeadStatusCodeToLeadStateCodeRepo;
            _sAPCustomerRepo = sAPCustomerRepository;
            _spkMasterCountryCodePhoneMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKMasterCountryCodePhone).ToString());
            _sAPCustomerLeadRepo = sAPCustomerLeadRepository;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Create a new SAP Customer
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<SAPCustomerDto> Create(SAPCustomerParameterDto objCreate)
        {
            #region Initialization
            var result = new ResponseBase<SAPCustomerDto>();
            var validationResults = new List<DNetValidationResult>();
            VechileType vehicleType = null;
            Dealer dealer = null;
            SalesmanHeader salesmanHeader = null;
            BusinessSectorDetail businessSectorDetail = null;
            var InformationSource = 0;
            var InformationType = 0;
            #endregion

            try
            {
                // validate parameter values
                var isValid = ValidateSAPCustomer(objCreate, validationResults, ref vehicleType, ref dealer, ref salesmanHeader, ref businessSectorDetail);
                if (isValid)
                {
                    //migration informationsource and information type
                    var IsvalidInformationSource = ValidateInformationSource(objCreate, objCreate.WebID, validationResults, ref InformationSource, ref InformationType, false);

                    // create sapcustomer object
                    var newSAPCustomer = _mapper.Map<SAPCustomer>(objCreate);

                    // update the other properties
                    newSAPCustomer.CreatedTime = DateTime.Now;
                    newSAPCustomer.LastUpdateTime = DateTime.Now;
                    newSAPCustomer.SalesmanHeader = salesmanHeader;
                    newSAPCustomer.VechileType = vehicleType;
                    newSAPCustomer.Dealer = dealer;
                    newSAPCustomer.Qty = objCreate.Qty;
                    newSAPCustomer.BusinessSectorDetail = businessSectorDetail;
                    newSAPCustomer.CountryCode = objCreate.CountryCode;
                    if (IsvalidInformationSource)
                    {
                        newSAPCustomer.InformationSource = Convert.ToInt16(InformationSource);
                        newSAPCustomer.InformationType = Convert.ToInt16(InformationType);
                    }
                    else
                    {
                        result.success = false;
                    }
                    newSAPCustomer.AgeSegmentDate =Convert.ToDateTime(newSAPCustomer.AgeSegmentDate)==Convert.ToDateTime("0001-01-01")? Convert.ToDateTime("1753-01-01"): newSAPCustomer.AgeSegmentDate;
                    newSAPCustomer.BirthDate =Convert.ToDateTime(newSAPCustomer.BirthDate) ==Convert.ToDateTime("0001-01-01")? Convert.ToDateTime("1753-01-01"): newSAPCustomer.BirthDate;
                    
                    // insert a new sapcustomer object
                    var succeed = _sapCustomerMapper.Insert(newSAPCustomer, DNetUserName);
                    result.success = succeed > 0;
                    if (result.success)
                    {
                        // return output ID
                        result._id = succeed;
                        result.total = 1;
                    }
                    else
                    {
                        ErrorMsgHelper.DataCorrupt(result.messages);
                    }
                }
                else
                {
                    return PopulateValidationError<SAPCustomerDto>(validationResults, null);
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
        /// Update a sapcustomer data
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<SAPCustomerDto> Update(SAPCustomerParameterDto objUpdate)
        {
            #region Initialization
            var result = new ResponseBase<SAPCustomerDto>();
            var validationResults = new List<DNetValidationResult>();
            Dealer dealer = null;
            SalesmanHeader salesmanHeader = null;
            VechileType vehicleType = null;
            BusinessSectorDetail businessSectorDetail = null;
            var checkInformationSource = string.Empty;
            var InformationSource = 0;
            var InformationType = 0;
            #endregion

            try
            {
                // validate parameter values

                var isValid = ValidateSAPCustomer(objUpdate, validationResults, ref vehicleType, ref dealer, ref salesmanHeader, ref businessSectorDetail, false);
                // get the existing customer data
                var existSAPCustomer = (SAPCustomer)_sapCustomerMapper.Retrieve(objUpdate.ID);
                if (existSAPCustomer == null)
                {
                    validationResults.Add(new DNetValidationResult("Data dengan ID " + objUpdate.ID + " tidak ditemukan."));
                    isValid = false;
                    return PopulateValidationError<SAPCustomerDto>(validationResults, null);
                }
                var namadepan = existSAPCustomer.CustomerName;
                var namabelakang = existSAPCustomer.Name2;
                var phone = existSAPCustomer.Phone;
                var email = existSAPCustomer.Email;
                var topic = existSAPCustomer.Topic;
                short? statecode = null;
                short? statuscode = null;
                int? vehiclemodel = null;
                try
                {
                    if(existSAPCustomer.VechileModel!=null)
                    {
                        vehiclemodel = existSAPCustomer.VechileModel.ID;
                    }
                }
                catch { vehiclemodel = null; }
                var status = existSAPCustomer.Status;
                if (status != 3)
                {
                    statecode = existSAPCustomer.StateCode;
                    statuscode = existSAPCustomer.StatusCode;
                }
                var leadstatus = existSAPCustomer.LeadStatus;

                if (existSAPCustomer == null)
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgCustomerNotFound, objUpdate.ID)));
                    isValid = false;
                    return PopulateValidationError<SAPCustomerDto>(validationResults, null);
                }
                else
                {
                    var IsvalidInformationSource = false;
                    var WebID = existSAPCustomer.WebID;
                    if (string.IsNullOrEmpty(objUpdate.GUIDUpdate))
                    {
                        IsvalidInformationSource = ValidateInformationSource(objUpdate, WebID, validationResults, ref InformationSource, ref InformationType, true);
                    }
                    if (validationResults.Count > 0)
                    {
                        result.success = false;
                    }
                    if (isValid)
                    {
                        // save the createdBy and CreatedTime into temp variables
                        string createdBy = existSAPCustomer.CreatedBy;
                        DateTime createdTime = existSAPCustomer.CreatedTime;
                        if (string.IsNullOrEmpty(objUpdate.GUIDUpdate))
                        {
                            if (IsvalidInformationSource)
                            {
                                objUpdate.InformationSource = Convert.ToInt16(InformationSource);
                                objUpdate.InformationType = Convert.ToInt16(InformationType);
                            }
                        }

                        // combine the object update with the existing sapcustomer object
                        var newdomain = new KTB.DNet.Interface.Domain.SAPCustomer();
                        var newSAPCustomer = _mapper.Map<SAPCustomerParameterDto,
                          SAPCustomer>(objUpdate, existSAPCustomer);
                        var newDomainSAPCustomer = _mapper.Map<SAPCustomer,
                          KTB.DNet.Interface.Domain.SAPCustomer>(newSAPCustomer, newdomain);

                        // update the other properties
                        newSAPCustomer.CreatedBy = createdBy;
                        newSAPCustomer.CreatedTime = createdTime;
                        newSAPCustomer.LastUpdateTime = DateTime.Now;
                        newSAPCustomer.SalesmanHeader = salesmanHeader;

                        newDomainSAPCustomer.CreatedBy = createdBy;
                        newDomainSAPCustomer.CreatedTime = createdTime;
                        newDomainSAPCustomer.LastUpdateBy = objUpdate.DealerCode;
                        newDomainSAPCustomer.LastUpdateTime = DateTime.Now;
                        newDomainSAPCustomer.Topic = topic;

                        if(!string.IsNullOrEmpty(objUpdate.VehicleTypeCode))
                        {
                            var criteriastype = new CriteriaComposite(new Criteria(typeof(VechileType), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                            criteriastype.opAnd(new Criteria(typeof(VechileType), "VechileTypeCode", MatchType.Exact, objUpdate.VehicleTypeCode));
                            criteriastype.opAnd(new Criteria(typeof(VechileType), "Status", MatchType.Exact, "A"));
                            var datatype = _vechileTypeMapper.RetrieveByCriteria(criteriastype);
                            if (datatype.Count > 0)
                            {
                                var vehicletype = datatype[0] as VechileType;
                                try
                                {
                                    newDomainSAPCustomer.VechileModelID = vehicletype.SalesVechileModel.ID;
                                }
                                catch {
                                    newDomainSAPCustomer.VechileModelID = null;
                                }
                            }else
                            {
                                newDomainSAPCustomer.VechileModelID = null;
                            }
                        }else
                        {
                            newDomainSAPCustomer.VechileModelID = Convert.ToInt16(vehiclemodel);
                        }
                        if (newDomainSAPCustomer.EstimatedCloseDate == null || Convert.ToDateTime(newDomainSAPCustomer.EstimatedCloseDate).Date == Convert.ToDateTime("1753-01-01").Date || Convert.ToDateTime(newDomainSAPCustomer.BirthDate).Date == Convert.ToDateTime("0001-01-01").Date)
                        {
                            newDomainSAPCustomer.EstimatedCloseDate = null;
                        }
                        if (newDomainSAPCustomer.BirthDate == null || Convert.ToDateTime(newDomainSAPCustomer.BirthDate).Date == Convert.ToDateTime("1753-01-01").Date || Convert.ToDateTime(newDomainSAPCustomer.BirthDate).Date == Convert.ToDateTime("0001-01-01").Date)
                        {
                            newDomainSAPCustomer.BirthDate = null;
                        }
                        if (objUpdate.LeadStatus == 3)
                        {
                            newDomainSAPCustomer.Phone = phone;
                            newDomainSAPCustomer.Email = email;
                        }
                        Int32? salesmanheaderid = null;
                        if (newDomainSAPCustomer.IdentityType == 0)
                        {
                            newDomainSAPCustomer.IdentityType = null;
                        }
                        if (salesmanHeader != null)
                        {
                            salesmanheaderid = Convert.ToInt32(salesmanHeader.ID);
                        }
                        newDomainSAPCustomer.SalesmanHeaderID = salesmanheaderid;
                        if (existSAPCustomer.VechileColor != null)
                        {
                            newDomainSAPCustomer.VechileColorID = existSAPCustomer.VechileColor.ID;
                        }
                        else
                        {
                            newDomainSAPCustomer.VechileColorID = null;
                        }

                        List<StandardCodeDto> _enumLead = new List<StandardCodeDto>();
                        _enumLead = _enumBL.GetByCategory("LeadStatus");
                        var enum_new = (_enumLead.Where(e => e.ValueCode == "New").SingleOrDefault().ValueId);
                        var enum_qualified = (_enumLead.Where(e => e.ValueCode == "Qualified").SingleOrDefault().ValueId);
                        if (objUpdate.VehicleTypeCode == "9999" && newSAPCustomer.LeadStatus != enum_new && newSAPCustomer.LeadStatus != enum_qualified)
                        {
                            newSAPCustomer.VechileType = null;
                            newDomainSAPCustomer.VechileTypeID = null;
                        }
                        else if (objUpdate.VehicleTypeCode == "9999" && (newSAPCustomer.LeadStatus == enum_new || newSAPCustomer.LeadStatus == enum_qualified))
                        {
                            if(!string.IsNullOrEmpty(objUpdate.GUIDUpdate))
                            {
                                newSAPCustomer.VechileType = null;
                                newDomainSAPCustomer.VechileTypeID = null;
                            }
                            else
                            {
                                validationResults.Add(new DNetValidationResult("Kode Tipe Kendaraan " + objUpdate.VehicleTypeCode + " tidak tersedia pada sistem"));
                            }
                        }
                        else
                        {
                            newSAPCustomer.VechileType = vehicleType;
                            short? vehicletypeid = null;
                            if (vehicleType != null)
                            {
                                vehicletypeid = Convert.ToInt16(vehicleType.ID);
                            }
                            newDomainSAPCustomer.VechileTypeID = vehicletypeid;
                        }
                        newSAPCustomer.Dealer = dealer;
                        newSAPCustomer.BusinessSectorDetail = businessSectorDetail;
                        newSAPCustomer.CountryCode = objUpdate.CountryCode;
                        newSAPCustomer.BusinessSectorDetail = businessSectorDetail;

                        newDomainSAPCustomer.DealerID = dealer.ID;
                        int? businessid = null;
                        if (businessSectorDetail != null)
                        {
                            businessid = Convert.ToInt16(businessSectorDetail.ID);
                        }
                        newDomainSAPCustomer.BusinessSectorDetailID = businessid;
                        if (validationResults.Count > 0)
                        {
                            result.success = false;
                        }
                        else
                        {
                            newDomainSAPCustomer.CustomerName = namadepan;
                            newDomainSAPCustomer.Name2 = namabelakang;
                            if (!string.IsNullOrEmpty(objUpdate.GUIDUpdate))
                            {
                                newDomainSAPCustomer.StatusCode = statuscode;
                                newDomainSAPCustomer.StateCode = statecode;
                                newDomainSAPCustomer.LeadStatus = leadstatus;
                                newDomainSAPCustomer.Status = status;
                                if (newSAPCustomer.GUIDUpdate != objUpdate.GUIDUpdate)
                                {
                                    newDomainSAPCustomer.GUID = objUpdate.Guid;
                                    newDomainSAPCustomer.LastUpdateTime = DateTime.Now;
                                }
                                else
                                {
                                    newDomainSAPCustomer.GUID = objUpdate.Guid;
                                    newDomainSAPCustomer.GUIDUpdate = objUpdate.GUIDUpdate;
                                    newDomainSAPCustomer.InterfaceStatus = Convert.ToInt16(objUpdate.InterfaceStatus);
                                    newDomainSAPCustomer.InterfaceMessage = objUpdate.InterfaceMessage;
                                }
                            }
                            else
                            {
                                newDomainSAPCustomer.GUID = objUpdate.Guid;
                                newDomainSAPCustomer.InterfaceStatus = Convert.ToInt16(objUpdate.InterfaceStatus);
                                newDomainSAPCustomer.InterfaceMessage = objUpdate.InterfaceMessage;
                            }
                            var nResult = _sAPCustomerLeadRepo.Update(newDomainSAPCustomer);
                            if (nResult.Success == true)
                            {
                                result.success = true;
                                //if (!string.IsNullOrEmpty(objUpdate.GUIDUpdate))
                                //{
                                //    _sAPCustomerLeadRepo.UpdateQueue(newDomainSAPCustomer);
                                //}
                                if (!string.IsNullOrEmpty(objUpdate.GUIDUpdate))
                                {
                                    using (SqlConnection con = new SqlConnection(connectionString))
                                    {
                                        using (SqlCommand cmd = new SqlCommand("SP_SFID_UpdateQueueStatus", con))
                                        {
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.Add("@DnetID", SqlDbType.Int).Value = objUpdate.ID;
                                            cmd.Parameters.Add("@SalesmanHeaderID", SqlDbType.Int).Value = newDomainSAPCustomer.SalesmanHeaderID;
                                            cmd.Parameters.Add("@GUIDUpdate", SqlDbType.VarChar).Value = objUpdate.GUIDUpdate;

                                            con.Open();
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                        }
                                    }
                                }
                            }

                            //var success = (int)_sapCustomerMapper.Update(newSAPCustomer, DNetUserName);
                            //result.success = success > 0;
                        }

                        if (result.success)
                        {
                            // return output ID
                            result._id = objUpdate.ID;
                            result.total = 1;
                        }
                        else
                        {
                            return PopulateValidationError<SAPCustomerDto>(validationResults, null);
                        }
                    }
                    else
                    {
                        return PopulateValidationError<SAPCustomerDto>(validationResults, null);
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

        private bool ValidateInformationSource(SAPCustomerParameterDto objUpdate, string WebID, List<DNetValidationResult> validationResults, ref int informationSource, ref int informationType, bool isUpdated)
        {
            var IsvalidInformationSource = true;
            List<StandardCodeDto> _enumSource = new List<StandardCodeDto>();
            List<StandardCodeDto> _enumType = new List<StandardCodeDto>();
            _enumSource = _enumBL.GetAllByCategory("InformationSource");
            _enumType = _enumBL.GetAllByCategory("InformationType");
            var informationSourceStatus = (_enumSource.Where(e => e.ValueId == objUpdate.InformationSource).SingleOrDefault().RowStatus);
            var informationTypeStatus = (_enumType.Where(e => e.ValueId == objUpdate.InformationType).SingleOrDefault().RowStatus);
            var isConvert = false;
            var isValid = false;

            if (isUpdated == true)
            {
                if (objUpdate.InformationSource == (_enumSource.Where(e => e.ValueCode == "Microsite").SingleOrDefault().ValueId) ||
                  objUpdate.InformationSource == (_enumSource.Where(e => e.ValueCode == "Web_Campaign").SingleOrDefault().ValueId) ||
                  objUpdate.InformationSource == (_enumSource.Where(e => e.ValueCode == "Web_Dealer").SingleOrDefault().ValueId) ||
                  objUpdate.InformationSource == (_enumSource.Where(e => e.ValueCode == "Call_Center").SingleOrDefault().ValueId) ||
                  objUpdate.InformationSource == (_enumSource.Where(e => e.ValueCode == "Website").SingleOrDefault().ValueId)
                )
                {
                    if (objUpdate.InformationType == (_enumType.Where(e => e.ValueCode == "Internet").SingleOrDefault().ValueId))
                    {
                        isValid = true;
                    }
                }
                else if (objUpdate.InformationSource == (_enumSource.Where(e => e.ValueCode == "Social_Media").SingleOrDefault().ValueId))
                {
                    if (objUpdate.InformationType == (_enumType.Where(e => e.ValueCode == "Social_Media").SingleOrDefault().ValueId))
                    {
                        isValid = true;
                    }
                }
                else
                {
                    isValid = true;
                }
            }
            else
            {
                isValid = true;
            }

            if (informationSourceStatus == -1 && isValid == true)
            {
                var caseSwitch = objUpdate.InformationSource;
                switch (caseSwitch)
                {
                    case 1:
                        informationSource = (_enumSource.Where(e => e.ValueCode == "Walk_In").SingleOrDefault().ValueId);
                        informationType = (_enumType.Where(e => e.ValueCode == "Surat_Kabar").SingleOrDefault().ValueId);
                        isConvert = true;
                        break;
                    case 2:
                        informationSource = (_enumSource.Where(e => e.ValueCode == "Walk_In").SingleOrDefault().ValueId);
                        informationType = (_enumType.Where(e => e.ValueCode == "Televisi").SingleOrDefault().ValueId);
                        isConvert = true;
                        break;
                    case 3:
                        informationSource = (_enumSource.Where(e => e.ValueCode == "Walk_In").SingleOrDefault().ValueId);
                        informationType = (_enumType.Where(e => e.ValueCode == "Majalah").SingleOrDefault().ValueId);
                        isConvert = true;
                        break;
                    case 4:
                        informationSource = (_enumSource.Where(e => e.ValueCode == "Walk_In").SingleOrDefault().ValueId);
                        informationType = (_enumType.Where(e => e.ValueCode == "Radio").SingleOrDefault().ValueId);
                        isConvert = true;
                        break;
                    /*case 5:
                        informationSource = (_enumSource.Where(e => e.ValueCode == "Rekomendasi").SingleOrDefault().ValueId);
                        informationType = (_enumType.Where(e => e.ValueCode == "Database").SingleOrDefault().ValueId);
                        isConvert = true;
                        break;
                    case 6:
                        informationSource = (_enumSource.Where(e => e.ValueCode == "Kunjungan").SingleOrDefault().ValueId);
                        informationType = (_enumType.Where(e => e.ValueCode == "Database").SingleOrDefault().ValueId);
                        isConvert = true;
                        break;
                    case 7:
                        informationSource = (_enumSource.Where(e => e.ValueCode == "Pameran").SingleOrDefault().ValueId);
                        informationType = (_enumType.Where(e => e.ValueCode == "Internet").SingleOrDefault().ValueId);
                        isConvert = true;
                        break;*/
                    case 8:
                        informationSource = (_enumSource.Where(e => e.ValueCode == "Walk_In").SingleOrDefault().ValueId);
                        informationType = (_enumType.Where(e => e.ValueCode == "Papan_Reklame").SingleOrDefault().ValueId);
                        isConvert = true;
                        break;
                    case 9:
                        informationSource = (_enumSource.Where(e => e.ValueCode == "Walk_In").SingleOrDefault().ValueId);
                        informationType = (_enumType.Where(e => e.ValueCode == "Internet").SingleOrDefault().ValueId);
                        isConvert = true;
                        break;
                    case 10:
                        informationSource = (_enumSource.Where(e => e.ValueCode == "Walk_In").SingleOrDefault().ValueId);
                        informationType = (_enumType.Where(e => e.ValueCode == "Kebetulan_Melintas").SingleOrDefault().ValueId);
                        isConvert = true;
                        break;
                    /*case 11:
                        if (string.IsNullOrEmpty(existSAPCustomer.WebID))
                        {
                            informationSource = (_enumSource.Where(e => e.ValueCode == "Microsite").SingleOrDefault().ValueId);
                            informationType = (_enumType.Where(e => e.ValueCode == "Internet").SingleOrDefault().ValueId);
                            isConvert = true;
                        }
                        else
                        {
                            informationSource = (_enumSource.Where(e => e.ValueCode == "Website").SingleOrDefault().ValueId);
                            informationType = (_enumType.Where(e => e.ValueCode == "Internet").SingleOrDefault().ValueId);
                            isConvert = true;
                        }
                        break;*/
                    case 12:
                        informationSource = (_enumSource.Where(e => e.ValueCode == "Kunjungan").SingleOrDefault().ValueId);
                        informationType = (_enumType.Where(e => e.ValueCode == "Mobile_Apps").SingleOrDefault().ValueId);
                        isConvert = true;
                        break;
                    /*case 13:
                        informationSource = (_enumSource.Where(e => e.ValueCode == "Kunjungan").SingleOrDefault().ValueId);
                        informationType = (_enumType.Where(e => e.ValueCode == "Social_Media").SingleOrDefault().ValueId);
                        isConvert = true;
                        break;
                    case 14:
                        if (string.IsNullOrEmpty(existSAPCustomer.WebID))
                        {
                            informationSource = (_enumSource.Where(e => e.ValueCode == "Kunjungan").SingleOrDefault().ValueId);
                            informationType = (_enumType.Where(e => e.ValueCode == "Database").SingleOrDefault().ValueId);
                            isConvert = true;
                        }
                        else
                        {
                            informationSource = (_enumSource.Where(e => e.ValueCode == "Call_Center").SingleOrDefault().ValueId);
                            informationType = (_enumType.Where(e => e.ValueCode == "Internet").SingleOrDefault().ValueId);
                            isConvert = true;
                        }
                        break;*/
                    default:
                        validationResults.Add(new DNetValidationResult("Information Source Tidak Valid"));
                        IsvalidInformationSource = false;
                        break;
                }
            }
            else if (informationTypeStatus == -1 && isConvert == false && isValid == true)
            {
                var caseSwitchType = objUpdate.InformationSource;
                switch (caseSwitchType)
                {
                    case 1:
                        informationSource = objUpdate.InformationSource;
                        informationType = (_enumType.Where(e => e.ValueCode == "Surat_Kabar").SingleOrDefault().ValueId);
                        isConvert = true;
                        break;
                    case 2:
                        informationSource = objUpdate.InformationSource;
                        informationType = (_enumType.Where(e => e.ValueCode == "Televisi").SingleOrDefault().ValueId);
                        isConvert = true;
                        break;
                    case 3:
                        informationSource = objUpdate.InformationSource;
                        informationType = (_enumType.Where(e => e.ValueCode == "Majalah").SingleOrDefault().ValueId);
                        isConvert = true;
                        break;
                    case 4:
                        informationSource = objUpdate.InformationSource;
                        informationType = (_enumType.Where(e => e.ValueCode == "Radio").SingleOrDefault().ValueId);
                        isConvert = true;
                        break;
                    case 5:
                        informationSource = objUpdate.InformationSource;
                        informationType = (_enumType.Where(e => e.ValueCode == "Database").SingleOrDefault().ValueId);
                        isConvert = true;
                        break;
                    case 6:
                        informationSource = objUpdate.InformationSource;
                        informationType = (_enumType.Where(e => e.ValueCode == "Database").SingleOrDefault().ValueId);
                        isConvert = true;
                        break;
                    case 7:
                        informationSource = objUpdate.InformationSource;
                        informationType = (_enumType.Where(e => e.ValueCode == "Internet").SingleOrDefault().ValueId);
                        isConvert = true;
                        break;
                    case 8:
                        informationSource = objUpdate.InformationSource;
                        informationType = (_enumType.Where(e => e.ValueCode == "Papan_Reklame").SingleOrDefault().ValueId);
                        isConvert = true;
                        break;
                    case 9:
                        informationSource = objUpdate.InformationSource;
                        informationType = (_enumType.Where(e => e.ValueCode == "Internet").SingleOrDefault().ValueId);
                        isConvert = true;
                        break;
                    case 10:
                        informationSource = objUpdate.InformationSource;
                        informationType = (_enumType.Where(e => e.ValueCode == "Kebetulan_Melintas").SingleOrDefault().ValueId);
                        isConvert = true;
                        break;
                    case 11:
                        if (string.IsNullOrEmpty(WebID))
                        {
                            informationSource = (_enumSource.Where(e => e.ValueCode == "Microsite").SingleOrDefault().ValueId);
                            informationType = (_enumType.Where(e => e.ValueCode == "Internet").SingleOrDefault().ValueId);
                            isConvert = true;
                        }
                        else
                        {
                            informationSource = (_enumSource.Where(e => e.ValueCode == "Website").SingleOrDefault().ValueId);
                            informationType = (_enumType.Where(e => e.ValueCode == "Internet").SingleOrDefault().ValueId);
                            isConvert = true;
                        }
                        break;
                    case 12:
                        informationSource = objUpdate.InformationSource;
                        informationType = (_enumType.Where(e => e.ValueCode == "Mobile_Apps").SingleOrDefault().ValueId);
                        isConvert = true;
                        break;
                    case 13:
                        informationSource = (_enumSource.Where(e => e.ValueCode == "Kunjungan").SingleOrDefault().ValueId);
                        informationType = (_enumType.Where(e => e.ValueCode == "Social_Media").SingleOrDefault().ValueId);
                        isConvert = true;
                        break;
                    case 14:
                        if (((objUpdate.Note).ToUpper()).Contains("MMKSI"))
                        {
                            informationSource = (_enumSource.Where(e => e.ValueCode == "Call_Center").SingleOrDefault().ValueId);
                            informationType = (_enumType.Where(e => e.ValueCode == "Internet").SingleOrDefault().ValueId);
                            isConvert = true;
                        }
                        else
                        {
                            informationSource = (_enumSource.Where(e => e.ValueCode == "Kunjungan").SingleOrDefault().ValueId);
                            informationType = (_enumType.Where(e => e.ValueCode == "Database").SingleOrDefault().ValueId);
                            isConvert = true;
                        }
                        break;
                    default:
                        validationResults.Add(new DNetValidationResult("Information Type Tidak Valid"));
                        IsvalidInformationSource = false;
                        break;
                }
            }
            else
            {
                if (isValid == true)
                {
                    informationSource = objUpdate.InformationSource;
                    informationType = objUpdate.InformationType;
                }
                else
                {
                    validationResults.Add(new DNetValidationResult("Sumber Informasi Digital Lead tidak boleh di ubah"));
                    IsvalidInformationSource = false;
                }

            }

            return IsvalidInformationSource;
        }

        /// <summary>
        /// Delete certain SAP Customer by updating their status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<SAPCustomerDto> Delete(int id)
        {
            var result = new ResponseBase<SAPCustomerDto>();

            try
            {
                var sapCustomer = (SAPCustomer)_sapCustomerMapper.Retrieve(id);
                if (sapCustomer != null)
                {
                    sapCustomer.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _sapCustomerMapper.Update(sapCustomer, DNetUserName);
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
        /// Get certain SAP Customer by customer name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ResponseBase<SAPCustomerDto> GetByName(string name)
        {
            var sapCustomer = new SAPCustomer();
            var sapCustomerDto = new SAPCustomerDto();
            var result = new ResponseBase<SAPCustomerDto>();

            try
            {
                CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(SAPCustomer), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                criterias.opAnd(new Criteria(typeof(SAPCustomer), "CustomerName", MatchType.Exact, name));
                ArrayList sapCustomerColl = _sapCustomerMapper.RetrieveByCriteria(criterias);
                if ((sapCustomerColl.Count > 0))
                {
                    sapCustomer = ((SAPCustomer)(sapCustomerColl[0]));
                }

                if (sapCustomer != null)
                {
                    // map it
                    sapCustomerDto = _mapper.Map<SAPCustomerDto>(sapCustomer);
                    if (sapCustomer.Dealer != null)
                    {
                        sapCustomerDto.Dealer = _mapper.Map<DealerDto>(sapCustomer.Dealer);
                    }

                    if (sapCustomer.SalesmanHeader != null)
                    {
                        sapCustomerDto.SalesmanHeader = _mapper.Map<SalesmanHeaderDto>(sapCustomer.SalesmanHeader);
                    }

                    sapCustomerDto.VehicleType = _mapper.Map<VehicleTypeDto>(sapCustomer.VechileType);

                    result.lst = sapCustomerDto;
                    result.messages = null;
                    // return output ID
                    result._id = sapCustomerDto.ID;
                    result.total = 1;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SAPCustomer), null, "Name", name);
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
        /// Get SAPCustomer by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<SAPCustomerDto>> Read(SAPCustomerFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(SAPCustomer), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(SAPCustomer), "Dealer.DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<SAPCustomerDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(SAPCustomer), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(SAPCustomer), filterDto, sortColl);

                // get data
                var data = _sapCustomerMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<SAPCustomer>().ToList();
                    var listData = new List<SAPCustomerDto>();
                    foreach (var item in list)
                    {
                        // map it
                        var sapcustomerDto = _mapper.Map<SAPCustomerDto>(item);

                        if (item.Dealer != null)
                        {
                            sapcustomerDto.Dealer = _mapper.Map<DealerDto>(item.Dealer);
                        }
                        if (item.VechileType != null)
                        {
                            sapcustomerDto.VehicleType = _mapper.Map<VehicleTypeDto>(item.VechileType);
                        }
                        if (item.SalesmanHeader != null)
                        {
                            sapcustomerDto.SalesmanHeader = _mapper.Map<SalesmanHeaderDto>(item.SalesmanHeader);
                        }
                        if (item.BusinessSectorDetail != null)
                        {
                            sapcustomerDto.BusinessSectorDetail = _mapper.Map<BusinessSectorDetailDto>(item.BusinessSectorDetail);
                        }

                        // add to list
                        listData.Add(sapcustomerDto);
                    }

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SAPCustomer), filterDto);
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
        /// Get Lead Customer Sales Force
        /// </summary>
        /// <param name="filterDto"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_LeadCustomerSalesForceDto>> GetLeadCustomerSalesForce(VWI_LeadCustomerSalesForceFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_LeadCustomerSalesForce), "DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<VWI_LeadCustomerSalesForceDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(VWI_LeadCustomerSalesForce), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(VWI_LeadCustomerSalesForce), filterDto, sortColl);

                var data = _vwiLeadCustomerSalesForceMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    List<VWI_LeadCustomerSalesForce> list = data.Cast<VWI_LeadCustomerSalesForce>().ToList();

                    result.lst = list.ConvertList<VWI_LeadCustomerSalesForce, VWI_LeadCustomerSalesForceDto>();
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_LeadCustomerSalesForce), filterDto);
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

        public ResponseBase<List<VWI_LeadCustomerSalesForceDto>> GetLeadCustomerSalesForceDapper(VWI_LeadCustomerSalesForceFilterDto filterDto, int pageSize)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_LeadCustomerSalesForce), "DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<VWI_LeadCustomerSalesForceDto>>();
            var sortColl = new SortCollection();
            int totalRow = 0;
            int filteredTotalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(VWI_LeadCustomerSalesForce), filterDto, criterias);

                // populate the sort info
                /*sortColl = Helper.UpdateSortColumn(typeof(VWI_LeadCustomerSalesForce), filterDto, sortColl);*/
                sortColl = null;

                List<VWI_LeadCustomerSalesForce> data = _sAPCustomerRepo.Search(
                  criterias, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    result.lst = data.ConvertList<VWI_LeadCustomerSalesForce, VWI_LeadCustomerSalesForceDto>();
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_LeadCustomerSalesForce), filterDto);
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
        /// <summary>
        /// Validate sapcustomer parameter
        /// </summary>
        /// <param name="param"></param>
        /// <param name="validationResults"></param>
        /// <param name="vehicleType"></param>
        /// <param name="dealer"></param>
        /// <param name="salesmanHeader"></param>
        /// <param name="businessSectorDetail"></param>
        /// <returns></returns>
        private bool ValidateSAPCustomer(SAPCustomerParameterDto param, List<DNetValidationResult> validationResults, ref VechileType vehicleType, ref Dealer dealer, ref SalesmanHeader salesmanHeader, ref BusinessSectorDetail businessSectorDetail, bool isCreate = true)
        {
            // check Sales Funnel
            var isWoSf = ValidationHelper.ValidateWoSf(this.DealerCode);

            #region Vehicle Type Validation
            // Vehicle Type Validation, if 9999 not validate
            if (param.VehicleTypeCode != "9999")
            {
                ValidationHelper.ValidateVehicleType(param.VehicleTypeCode, validationResults, ref vehicleType);
            }
            #endregion

            #region Dealer Validation
            // Dealer Validation
            if (!string.IsNullOrEmpty(param.DealerCode))
            {
                if (ValidationHelper.ValidateDealer(param.DealerCode, validationResults, this.DealerCode, ref dealer))
                {
                    AppConfigBL appConfigBL = new AppConfigBL(_mapper);
                    AppConfigDto isSalesmanRequiredConfig = appConfigBL.GetByName("IsSalesmanRequiredForLead").lst;
                    bool isSalesmanRequiredForLead = isSalesmanRequiredConfig == null ? true : isSalesmanRequiredConfig.Value.ToBool();

                    if (isSalesmanRequiredForLead && string.IsNullOrEmpty(param.SalesmanHeaderCode))
                    {
                        validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.SalesmanCode)));
                    }
                    else
                    {
                        // SalesmanHeader validation
                        if (ValidationHelper.ValidateSalesmanHeader(param.SalesmanHeaderCode, this.DealerCode, validationResults, ref salesmanHeader))
                        {
                            if (salesmanHeader != null)
                            {
                                ValidateSalesmanHeader(salesmanHeader, dealer, param, validationResults);
                            }
                        }
                    }
                }
            }
            #endregion

            #region Dealer Category Validation
            ValidationHelper.ValidateDealerCategory(this.DealerCode, param.SalesmanHeaderCode, param.VehicleTypeCode, validationResults);
            #endregion

            #region Customer Number Reference
            // Customer number reference validation
            ValidateCustomerReference(param, validationResults);
            #endregion

            #region Validationfor Create
            if (isCreate)
            {
                // Customer existance validation
                string salesmanName = String.Empty;
                if (IsCustomerExist(param.Phone, vehicleType, out salesmanName))
                {
                    if (vehicleType != null && !string.IsNullOrEmpty(vehicleType.VechileTypeCode))
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSAPCustomerExist, param.Phone, vehicleType.VechileTypeCode, salesmanName)));
                    else
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSAPCustomerExistV2, param.Phone, salesmanName)));
                }

                // validation for create without SF
                if (isWoSf)
                {
                    // check if lead status = new
                    var leadStatusStandardCode = _enumBL.GetByCategoryAndCode("LeadStatus", "New");
                    var isNew = param.LeadStatus == leadStatusStandardCode.ValueId;
                    if (!isNew)
                    {
                        validationResults.Add(new DNetValidationResult("Insert hanya boleh dilakukan untuk Customer dengan Lead Status New"));
                    }
                }
            }
            #endregion

            #region Validation for Update Without SF
            if (!isCreate)
                {
                    #region Get existing data from database
                    // get existing data from database
                    var existingSAPCustomer = (SAPCustomer)_sapCustomerMapper.Retrieve(param.ID);
                    if (existingSAPCustomer == null)
                    {
                        validationResults.Add(new DNetValidationResult(string.Format("Customer dengan ID {0} tidak ditemukan", param.ID)));
                        return false;
                    }
                    #endregion

                    #region Customer existance validation
                    string salesmanName = String.Empty;
                    existingSAPCustomer.DealerCode = param.DealerCode;
                    existingSAPCustomer.SalesmanHeaderCode = param.SalesmanHeaderCode;
                    existingSAPCustomer.Phone = param.Phone;
                    existingSAPCustomer.VehicleTypeCode = param.VehicleTypeCode;
                    var systemID = ValidationHelper.ValidateDealerSystems(existingSAPCustomer.DealerCode);
                    var success = true;
                    string salesmanname = String.Empty;
                    if (systemID > 2)
                    {
                        success = IsDataExist(existingSAPCustomer, validationResults, out salesmanname);
                    }
                    if (success == true)
                    {
                        if (isWoSf)
                        {
                            #region Check Next Lead Status

                            // get allowed lead status based on customer status and current lead status
                            var trLeadStatusToStatus = _trLeadStatusToStatusRepo.GetByCustomerStatusAndNextLeadStatus(existingSAPCustomer.Status, existingSAPCustomer.LeadStatus);

                            if (trLeadStatusToStatus == null || trLeadStatusToStatus.ID == 0)
                            {
                                validationResults.Add(new DNetValidationResult("Data Customer Status dan Lead Status tidak ditemukan"));
                                return false;
                            }
                            else if (trLeadStatusToStatus.FinalStatus)
                            {
                                if (existingSAPCustomer.LeadStatus != param.LeadStatus || existingSAPCustomer.Status != param.Status || existingSAPCustomer.StateCode != param.StateCode)
                                {
                                    validationResults.Add(new DNetValidationResult("Data Customer Status, Lead Status, dan State Code tidak boleh diubah"));
                                    return false;
                                }
                            }

                            var newTrLeadStatusToStatus = _trLeadStatusToStatusRepo.GetByParam((int)param.Status, (int)param.LeadStatus, (int)existingSAPCustomer.LeadStatus);
                            if (newTrLeadStatusToStatus == null || newTrLeadStatusToStatus.ID == 0)
                            {
                                if (existingSAPCustomer.LeadStatus != param.LeadStatus || existingSAPCustomer.Status != param.Status || existingSAPCustomer.StateCode != param.StateCode)
                                {
                                    validationResults.Add(new DNetValidationResult("Data Customer Status dan Lead Status tidak sesuai"));
                                    return false;
                                }
                            }

                            #endregion

                            #region Check Existing Status, Lead Status, and State Code
                            var trStatusToLeadStatusCodeToStateCode = _trStatusToLeadStatusCodeToLeadStateCodeRepo.GetByParam(existingSAPCustomer.Status, existingSAPCustomer.LeadStatus, existingSAPCustomer.StateCode);
                            if (trStatusToLeadStatusCodeToStateCode == null || trStatusToLeadStatusCodeToStateCode.ID == 0)
                            {
                                validationResults.Add(new DNetValidationResult("Kombinasi Customer Status, Lead Status, dan State Code tidak ditemukan"));
                                return false;
                            }

                            if (trStatusToLeadStatusCodeToStateCode.FinalStatus)
                            {
                                if (param.Status != existingSAPCustomer.Status || param.LeadStatus != existingSAPCustomer.LeadStatus || param.StateCode != existingSAPCustomer.StateCode)
                                {
                                    validationResults.Add(new DNetValidationResult("Customer Status, Lead Status, dan State Code sudah tidak boleh diubah"));
                                    return false;
                                }
                            }
                            #endregion

                            #region Check New Status, Lead Status, and State Code
                            var newTrStatusToLeadStatusCodeToStateCode = _trStatusToLeadStatusCodeToLeadStateCodeRepo.GetByParam((int)param.Status, (int)param.LeadStatus, (int)param.StateCode);
                            if (newTrStatusToLeadStatusCodeToStateCode == null || newTrStatusToLeadStatusCodeToStateCode.ID == 0)
                            {
                                if (param.Status != existingSAPCustomer.Status || param.LeadStatus != existingSAPCustomer.LeadStatus || param.StateCode != existingSAPCustomer.StateCode)
                                {
                                    validationResults.Add(new DNetValidationResult("Kombinasi Customer Status, Lead Status, dan State Code tidak ditemukan"));
                                    return false;
                                }
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        validationResults.Add(new DNetValidationResult("Kombinasi Nomor HP " + param.Phone + " dan Type " + param.VehicleTypeCode + " sudah direcord oleh " + salesmanname + " dan belum SPK"));
                    }
                    #endregion
                }
            #endregion

            #region Business Sector Detail
            // Get Business Sector Detail if any
            if (param.BusinessSectorDetailID > 0)
                businessSectorDetail = (BusinessSectorDetail)_businessSectorDetailMapper.Retrieve(param.BusinessSectorDetailID);
            #endregion

            #region Validate Enum
            // Validate enum
            ValidateEnum(param, validationResults);
            #endregion

            #region Lead Status Create
            if (param.Status > 0)
            {
                if (isCreate)
                    ValidateLeadStatusCreate(param, validationResults);
                else
                    ValidateLeadStatusUpdate(param, validationResults);
            }
            #endregion

            #region PhoneNumber & CountryCode Validation
            string PhoneNo = string.Empty;
            string KodeNegara = string.Empty;
            string CountryCode = param.CountryCode.Trim();
            string PhoneNumber = param.Phone.Trim();
            ValidatePhoneAndCountryCode(PhoneNumber, CountryCode, validationResults);
            #endregion

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate salesman header
        /// </summary>
        /// <param name="salesmanHeader"></param>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        private void ValidateSalesmanHeader(SalesmanHeader salesmanHeader, Dealer dealer, SAPCustomerParameterDto objCreate, List<DNetValidationResult> validationResults)
        {
            /// SalesmanHeader hanya bisa input yang aktif di dealer tersebut dan status harus dicek => SELECT * FROM dbo.SalesmanHeader 
            /// WHERE Status = 2 AND DealerId = 3 AND SalesIndicator = 1 AND RowStatus = 0
            if (salesmanHeader == null)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataNotFound, FieldResource.SalesmanHeader)));
            }
            else if (salesmanHeader.Dealer.ID != dealer.ID)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.SalesmanHeaderDealer)));
            }
            else if (salesmanHeader.Status != "2")
            {
                validationResults.Add(new DNetValidationResult("Status Salesman yang digunakan sudah tidak aktif"));
            }
            else if (salesmanHeader.SalesIndicator != 1)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotList, FieldResource.SalesmanHeaderSalesIndicator)));
            }
        }

        /// <summary>
        /// Validate customer number reference
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        private void ValidateCustomerReference(SAPCustomerParameterDto objCreate, List<DNetValidationResult> validationResults)
        {
            // Jika ada Ref Code Pelanggan, maka harus dicek dari table customer join ke customerdealer cek berdasar dealer code -> 
            // kalo exist maka accept kalo tidak : "Customer tidak ada di dalam dealer ini"
            if (!string.IsNullOrEmpty(objCreate.CustomerCode))
            {
                try
                {
                    Customer customer = null;
                    if (ValidationHelper.ValidateCustomer(objCreate.CustomerCode, validationResults, ref customer))
                    {
                        CustomerDealer customerDealer = null;
                        ValidationHelper.ValidateCustomerDealer(customer.ID, validationResults, ref customerDealer);
                    }
                }
                catch
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.RefCustomerCode)));
                }
            }
        }

        /// <summary>
        /// Validation for Data Update
        /// </summary>
        /// <returns></returns>
        private bool IsDataExist(SAPCustomer existingSAPCustomer, List<DNetValidationResult> validationResults, out string salesmanname)
        {
            bool vReturn = false;
            salesmanname = String.Empty;
            var _dealer = MapperFactory.GetInstance().GetMapper(typeof(Dealer).ToString());

            try
            {
                CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(SAPCustomer), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                /*criteria.opAnd(new Criteria(typeof(SAPCustomer), "SalesmanHeader.SalesmanCode", MatchType.Exact, existingSAPCustomer.SalesmanHeaderCode));*/
                criteria.opAnd(new Criteria(typeof(SAPCustomer), "Dealer.DealerCode", MatchType.Exact, existingSAPCustomer.DealerCode));
                criteria.opAnd(new Criteria(typeof(SAPCustomer), "Phone", MatchType.Exact, existingSAPCustomer.Phone));
                if (!String.IsNullOrEmpty(existingSAPCustomer.VehicleTypeCode))
                {
                    criteria.opAnd(new Criteria(typeof(SAPCustomer), "VechileType.VechileTypeCode", MatchType.Exact, existingSAPCustomer.VehicleTypeCode));
                }

                var arrSAPCustomer = _sapCustomerMapper.RetrieveByCriteria(criteria);

                List<StandardCodeDto> _enumStatus = new List<StandardCodeDto>();
                _enumStatus = _enumBL.GetByCategory("SAPCustomerStatus");
                if (arrSAPCustomer.Count > 0)
                {
                    SAPCustomer sapCustomer = arrSAPCustomer.OfType<SAPCustomer>().ToList().FirstOrDefault();
                    if (sapCustomer.Status == (_enumStatus.Where(e => e.ValueCode == "Deal_SPK").SingleOrDefault().ValueId) || sapCustomer.Status == (_enumStatus.Where(e => e.ValueCode == "Cancelled").SingleOrDefault().ValueId))
                    {
                        vReturn = true;
                    }
                    else
                    {
                        if (sapCustomer.ID == existingSAPCustomer.ID)
                        {
                            vReturn = true;
                        }
                        else
                        {
                            vReturn = false;
                            salesmanname = sapCustomer.SalesmanHeader.Name;
                        }
                    }
                }
                else
                {
                    vReturn = true;
                }
            }
            catch
            {
                vReturn = false;
            }

            return vReturn;
        }

        /// <summary>
        /// Validation for customer
        /// </summary>
        /// <returns></returns>
        private bool IsCustomerExist(string phone, VechileType vechileType, out string salesmanName)
        {
            bool vReturn = false;
            salesmanName = String.Empty;
            try
            {
                CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(SAPCustomer), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                criteria.opAnd(new Criteria(typeof(SAPCustomer), "Phone", MatchType.Exact, phone));
                criteria.opAnd(new Criteria(typeof(SAPCustomer), "Status", MatchType.No, _enumBL.GetByCategoryAndCode("SAPCustomerStatus", "Deal_SPK").ValueId));
                if (vechileType != null && !string.IsNullOrEmpty(vechileType.VechileTypeCode))
                    criteria.opAnd(new Criteria(typeof(SAPCustomer), "VechileType.VechileTypeCode", MatchType.Exact, vechileType.VechileTypeCode));
                criteria.opAnd(new Criteria(typeof(SAPCustomer), "Dealer.DealerCode", MatchType.Exact, this.DealerCode));

                var arrSAPCustomer = _sapCustomerMapper.RetrieveByCriteria(criteria);
                if (arrSAPCustomer.Count > 0)
                {
                    SAPCustomer sapCustomer = arrSAPCustomer.OfType<SAPCustomer>().ToList().FirstOrDefault();
                    if (sapCustomer.SalesmanHeader != null)
                    {
                        salesmanName = sapCustomer.SalesmanHeader.Name;
                    }
                    vReturn = true;
                }
            }
            catch
            {
                vReturn = false;
            }

            return vReturn;
        }

        /// <summary>
        /// Validate model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private void ValidateEnum(SAPCustomerParameterDto model, List<DNetValidationResult> results)
        {
            // Enum validation
            if (!_enumBL.IsExistByCategoryAndValue("TipePelanggan", ((int)(model.CustomerType)).ToString()))
                results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.CustomerType)));
            if (model.Sex > 0 && !_enumBL.IsExistByCategoryAndValue(".Gender", ((int)(model.Sex)).ToString()))
                results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.CustomerGender)));
            /*if (model.InformationType > 0 && !_enumBL.IsExistByCategoryAndValue(".InformationType", ((int)(model.InformationType)).ToString()))
                results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.InformationType)));*/
            if (model.CustomerPurpose > 0 && !_enumBL.IsExistByCategoryAndValue(".CustomerPurpose", ((int)(model.CustomerPurpose)).ToString()))
                results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.CustomerPurpose)));
            /*if (model.InformationSource > 0 && !_enumBL.IsExistByCategoryAndValue(".InformationSource", ((int)(model.InformationSource)).ToString()))
                      results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.InformationSource)));
      */
            if (model.AgeSegment.HasValue)
            {
                if (!_enumBL.IsExistByCategoryAndValue(".AgeSegment", ((int)(model.AgeSegment)).ToString()))
                {
                    results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.AgeSegment)));
                }
            }

            if (model.LeadStatus.HasValue)
            {
                if (!_enumBL.IsExistByCategoryAndValue("LeadStatus", ((int)(model.LeadStatus)).ToString()))
                {
                    results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.LeadStatus)));
                }
            }

            if (model.StateCode.HasValue)
            {
                if (!_enumBL.IsExistByCategoryAndValue("LeadStateCode", ((int)(model.StateCode)).ToString()))
                {
                    results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.StateCode)));
                }
            }

            if (model.StatusCode.HasValue)
            {
                if (!_enumBL.IsExistByCategoryAndValue("LeadStatusCode", ((int)(model.StatusCode)).ToString()))
                {
                    results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.StatusCode)));
                }
            }
        }

        /// <summary>
        /// Validate lead status create
        /// </summary>
        /// <param name="model"></param>
        /// <param name="results"></param>
        private void ValidateLeadStatusCreate(SAPCustomerParameterDto model, List<DNetValidationResult> results)
        {
            List<StandardCodeDto> standardCodes = _enumBL.GetByCategory("SAPCustomerStatus");

            bool isSuspect = standardCodes.Any(x => x.ValueCode == "Suspect" && x.ValueId == model.Status);

            if (!isSuspect)
            {
                results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.Status)));
            };
        }

        /// <summary>
        /// Validate lead status update
        /// </summary>
        /// <param name="model"></param>
        /// <param name="results"></param>
        private void ValidateLeadStatusUpdate(SAPCustomerParameterDto model, List<DNetValidationResult> results)
        {
            if (!_enumBL.IsExistByCategoryAndValue(".SAPCustomerStatus", ((int)(model.Status)).ToString()))
            {
                results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.Status)));
            }
        }

        /// <summary>
        /// Validation for customer checking
        /// </summary>
        /// <returns></returns>
        private void CheckCustomerExist(string phone, VechileType vechileType, short updatedStatus, string salesmanCode, List<DNetValidationResult> validationResults, short oldStatus = 0, int customerId = 0)
        {
            try
            {
                if (oldStatus != 0 && oldStatus != _enumBL.GetByCategoryAndCode("SAPCustomerStatus", "Suspect").ValueId && updatedStatus == _enumBL.GetByCategoryAndCode("SAPCustomerStatus", "Suspect").ValueId)
                {
                    validationResults.Add(new DNetValidationResult("Status lebih tinggi tidak bisa turun ke Suspect! Data Suspect harus create baru!"));
                }
                else if (oldStatus == 0)
                {
                    CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(SAPCustomer), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                    criteria.opAnd(new Criteria(typeof(SAPCustomer), "Phone", MatchType.Exact, phone));
                    criteria.opAnd(new Criteria(typeof(SAPCustomer), "Status", MatchType.No, _enumBL.GetByCategoryAndCode("SAPCustomerStatus", "Deal_SPK").ValueId), "((", true);
                    criteria.opAnd(new Criteria(typeof(SAPCustomer), "Status", MatchType.No, _enumBL.GetByCategoryAndCode("SAPCustomerStatus", "Cancelled").ValueId), ")", false);
                    //criteria.opOr(new Criteria(typeof(SAPCustomer), "StateCode", MatchType.No, _enumBL.GetByCategoryAndCode("LeadStateCode", "Lost").ValueId));
                    //criteria.opOr(new Criteria(typeof(SAPCustomer), "StatusCode", MatchType.No, _enumBL.GetByCategoryAndCode("LeadStatusCode", "Cancelled").ValueId));
                    criteria.opAnd(new Criteria(typeof(SAPCustomer), "LeadStatus", MatchType.No, _enumBL.GetByCategoryAndCode("LeadStatus", "Cancelled").ValueId), "(", true);
                    criteria.opAnd(new Criteria(typeof(SAPCustomer), "LeadStatus", MatchType.No, _enumBL.GetByCategoryAndCode("LeadStatus", "Lost").ValueId), "))", false);
                    if (vechileType != null && !string.IsNullOrEmpty(vechileType.VechileTypeCode))
                        criteria.opAnd(new Criteria(typeof(SAPCustomer), "VechileType.VechileTypeCode", MatchType.Exact, vechileType.VechileTypeCode));
                    criteria.opAnd(new Criteria(typeof(SAPCustomer), "Dealer.DealerCode", MatchType.Exact, this.DealerCode));

                    var arrSAPCustomer = _sapCustomerMapper.RetrieveByCriteria(criteria);
                    if (arrSAPCustomer.Count > 0)
                    {
                        List<SAPCustomer> listSAPCustomerResult = arrSAPCustomer.Cast<SAPCustomer>().ToList();
                        var tempData = listSAPCustomerResult.Where(x => x.Status == _enumBL.GetByCategoryAndCode("SAPCustomerStatus", "Suspect").ValueId).ToList();
                        if (tempData != null && tempData.Count > 0)
                        {
                            foreach (SAPCustomer data in tempData)
                            {
                                if (data.SalesmanHeader.SalesmanCode.Trim().ToUpper() == salesmanCode.Trim().ToUpper())
                                {
                                    if (vechileType != null && !string.IsNullOrEmpty(vechileType.VechileTypeCode))
                                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSAPCustomerExist, phone, vechileType.VechileTypeCode, data.SalesmanHeader.Name)));
                                    else
                                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSAPCustomerExistV2, phone, data.SalesmanHeader.Name)));
                                }
                            }
                        }
                    }
                    //else
                    //{
                    //    criteria = new CriteriaComposite(new Criteria(typeof(SAPCustomer), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                    //    criteria.opAnd(new Criteria(typeof(SAPCustomer), "Phone", MatchType.Exact, phone));
                    //    criteria.opAnd(new Criteria(typeof(SAPCustomer), "Status", MatchType.No, _enumBL.GetByCategoryAndCode("SAPCustomerStatus", "Deal_SPK").ValueId));
                    //    criteria.opAnd(new Criteria(typeof(SAPCustomer), "StateCode", MatchType.No, _enumBL.GetByCategoryAndCode("LeadStateCode", "Lost").ValueId));
                    //    criteria.opAnd(new Criteria(typeof(SAPCustomer), "LeadStatus", MatchType.No, _enumBL.GetByCategoryAndCode("LeadStatus", "Lost").ValueId));
                    //    criteria.opAnd(new Criteria(typeof(SAPCustomer), "Dealer.DealerCode", MatchType.Exact, this.DealerCode));

                    //    var arrSAPCustomerWOType = _sapCustomerMapper.RetrieveByCriteria(criteria);
                    //    if (arrSAPCustomerWOType.Count > 0)
                    //    {
                    //        List<SAPCustomer> listSAPCustomerResult = arrSAPCustomerWOType.Cast<SAPCustomer>().ToList();
                    //        var tempData = listSAPCustomerResult.Where(x => x.Status == _enumBL.GetByCategoryAndCode("SAPCustomerStatus", "Suspect").ValueId).ToList();
                    //        if (tempData != null && tempData.Count > 0)
                    //        {
                    //            foreach (SAPCustomer data in tempData)
                    //            {
                    //                if (data.SalesmanHeader.SalesmanCode.Trim().ToUpper() == salesmanCode.Trim().ToUpper())
                    //                {
                    //                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSAPCustomerExistV2, phone, data.SalesmanHeader.Name)));
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                }
                else
                {
                    CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(SAPCustomer), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                    criteria.opAnd(new Criteria(typeof(SAPCustomer), "Phone", MatchType.Exact, phone));
                    criteria.opAnd(new Criteria(typeof(SAPCustomer), "Status", MatchType.No, _enumBL.GetByCategoryAndCode("SAPCustomerStatus", "Deal_SPK").ValueId), "((", true);
                    criteria.opAnd(new Criteria(typeof(SAPCustomer), "Status", MatchType.No, _enumBL.GetByCategoryAndCode("SAPCustomerStatus", "Cancelled").ValueId), ")", false);
                    //criteria.opOr(new Criteria(typeof(SAPCustomer), "StateCode", MatchType.No, _enumBL.GetByCategoryAndCode("LeadStateCode", "Lost").ValueId));
                    //criteria.opOr(new Criteria(typeof(SAPCustomer), "StatusCode", MatchType.No, _enumBL.GetByCategoryAndCode("LeadStatusCode", "Cancelled").ValueId));
                    criteria.opAnd(new Criteria(typeof(SAPCustomer), "LeadStatus", MatchType.No, _enumBL.GetByCategoryAndCode("LeadStatus", "Cancelled").ValueId), "(", true);
                    criteria.opAnd(new Criteria(typeof(SAPCustomer), "LeadStatus", MatchType.No, _enumBL.GetByCategoryAndCode("LeadStatus", "Lost").ValueId), "))", false);
                    //if (vechileType != null && !string.IsNullOrEmpty(vechileType.VechileTypeCode))
                    //    criteria.opAnd(new Criteria(typeof(SAPCustomer), "VechileType.VechileTypeCode", MatchType.Exact, vechileType.VechileTypeCode));
                    criteria.opAnd(new Criteria(typeof(SAPCustomer), "Dealer.DealerCode", MatchType.Exact, this.DealerCode));

                    var arrSAPCustomer = _sapCustomerMapper.RetrieveByCriteria(criteria);
                    if (arrSAPCustomer.Count > 0)
                    {
                        List<SAPCustomer> listSAPCustomerResult = arrSAPCustomer.Cast<SAPCustomer>().ToList();

                        // jika data baru
                        if (1 == 1)
                        {
                            //if (oldStatus == _enumBL.GetByCategoryAndCode("SAPCustomerStatus", "Suspect").ValueId && updatedStatus != _enumBL.GetByCategoryAndCode("SAPCustomerStatus", "Suspect").ValueId)
                            if (updatedStatus != _enumBL.GetByCategoryAndCode("SAPCustomerStatus", "Suspect").ValueId)
                            {
                                var tempData = listSAPCustomerResult.Where(x => x.Status != _enumBL.GetByCategoryAndCode("SAPCustomerStatus", "Suspect").ValueId).ToList();

                                if (tempData != null && tempData.Count > 0)
                                {
                                    foreach (SAPCustomer data in tempData)
                                    {
                                        if (data.ID != customerId)
                                        {
                                            if (vechileType != null && !string.IsNullOrEmpty(vechileType.VechileTypeCode))
                                                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSAPCustomerExistV2, phone, tempData[0].SalesmanHeader.Name)));
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                validationResults.Add(new DNetValidationResult(string.Format("Ada Kesalahan pada system : {0}. Hubungi Administrator", ex.Message)));
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
                        results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.Phone)));
                    }
                }
            }
            else
            {
                results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.CountryCode)));
            }
        }
        #endregion
    }
}