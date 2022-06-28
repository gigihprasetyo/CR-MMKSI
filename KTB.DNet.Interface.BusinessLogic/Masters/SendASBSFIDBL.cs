using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.Model;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.BusinessLogic
{
    public class SendASBSFIDBL : AbstractBusinessLogic, ISendASBSFIDBL
    {
        #region Variables
        private readonly IMapper _dealerMapper;
        private readonly IMapper _cityMapper;
        private readonly IMapper _salesmanHeaderMapper;
        private readonly IMapper _vehicleColorMapper;
        private readonly IMapper _sapCustomerMapper;
        private readonly IMapper _vehicleTypeMapper;
        private readonly IMapper _profileGroupMapper;
        private readonly IMapper _salesVehicleModelMapper;
        private readonly IMapper _vehicleModelMapper;
        public string connectionString = "Endpoint=sb://sbmeqa.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=wcxDxfvP1r8/8AvjvVaUL/1zjGOwlR+3gjnUQ5vCGps=";
        //public string connectionString = "Endpoint=sb://sbmuat.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=wEazn9iIN7ZFHGm2YVo5miiFb1xYE9bbZdtlGtkq2Rs=";
        public string queueName = "sfidyanamap";


 
        #endregion
        #region Constructor
        public SendASBSFIDBL()
        {
            _dealerMapper = MapperFactory.GetInstance().GetMapper(typeof(Dealer).ToString());
            _cityMapper = MapperFactory.GetInstance().GetMapper(typeof(City).ToString());
            _vehicleColorMapper = MapperFactory.GetInstance().GetMapper(typeof(VechileColor).ToString());
            _vehicleTypeMapper = MapperFactory.GetInstance().GetMapper(typeof(VechileType).ToString());
            _profileGroupMapper = MapperFactory.GetInstance().GetMapper(typeof(ProfileGroup).ToString());
            _vehicleModelMapper = MapperFactory.GetInstance().GetMapper(typeof(VechileModel).ToString());
            _salesVehicleModelMapper = MapperFactory.GetInstance().GetMapper(typeof(SalesVechileModel).ToString());
            _sapCustomerMapper = MapperFactory.GetInstance().GetMapper(typeof(SAPCustomer).ToString());
            _salesmanHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(SalesmanHeader).ToString());
        }
        #endregion
        public ResponseBase<SendASBSFIDDto> SendCustomer(SendASBSFIDParameterDto.CustomerSFIDParameterDto param)
        {
            var validationResults = new List<DNetValidationResult>();
            //var connectionString = "Endpoint=sb://sbmeqa.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=wcxDxfvP1r8/8AvjvVaUL/1zjGOwlR+3gjnUQ5vCGps=";
            //var queueName = "sfidyanamap";
            var result = new ResponseBase<SendASBSFIDDto>();
            var sortColl = new SortCollection();
            var totalRow = 0;
            var dealercode = string.Empty;
            var CityCode = string.Empty;

            QueueClient _queueClient = null;
            try
            {
                _queueClient =  QueueClient.CreateFromConnectionString(connectionString, queueName);
            }
            catch (Exception ex)
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.DataContentCorrupt, "Gagal koneksi ke ASB" +ex));
                return PopulateValidationError<SendASBSFIDDto>(validationResults, null);
            }

            var dealerid = 0;
            var criteriassalesmanheader = new CriteriaComposite(new Criteria(typeof(SalesmanHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteriassalesmanheader.opAnd(new Criteria(typeof(SalesmanHeader), "SalesmanCode", MatchType.Exact, param.SalesmanCode));
            var datasalesmanheader = _salesmanHeaderMapper.RetrieveByCriteria(criteriassalesmanheader);
            if (datasalesmanheader.Count > 0)
            {
                var salesmanheader = datasalesmanheader[0] as SalesmanHeader;
                if (salesmanheader.DealerBranch != null)
                {
                    dealerid = salesmanheader.DealerBranch.ID;
                }
                else
                {
                    dealerid = salesmanheader.Dealer.ID;
                }
            }
            if (dealerid != 0)
            {
                var criterias = new CriteriaComposite(new Criteria(typeof(Dealer), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(Dealer), "ID", MatchType.Exact, dealerid));
                var data = _dealerMapper.RetrieveByCriteria(criterias);
                if (data.Count > 0)
                {
                    var dealer = data[0] as Dealer;
                    dealercode = dealer.DealerCode;
                }
            }
            var criterias_city = new CriteriaComposite(new Criteria(typeof(City), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias_city.opAnd(new Criteria(typeof(City), "ID", MatchType.Exact, param.CityID));
            var data_city = _cityMapper.RetrieveByCriteria(criterias_city);
            if (data_city.Count > 0)
            {
                var city = data_city[0] as City;
                CityCode = city.CityCode;
            }
            
            if (param.CustomerTypeDNET==0)
            {
                if(param.CustomerSubClass==0)
                {
                    param.CustomerSubClass = 1;
                }else if (param.CustomerSubClass == 1)
                {
                    param.CustomerSubClass = 2;
                }
            }else if (param.CustomerTypeDNET == 1)
            {
                if (param.CustomerSubClass == 0)
                {
                    param.CustomerSubClass = 3;
                }
                else if (param.CustomerSubClass == 1)
                {
                    param.CustomerSubClass = 9;
                }
                else if (param.CustomerSubClass == 2)
                {
                    param.CustomerSubClass = 4;
                }
                else if (param.CustomerSubClass == 4)
                {
                    param.CustomerSubClass = 8;
                }
                else if (param.CustomerSubClass == 5)
                {
                    param.CustomerSubClass = 5;
                }
                else if (param.CustomerSubClass == 6)
                {
                    param.CustomerSubClass = 6;
                }
                else if (param.CustomerSubClass == 7)
                {
                    param.CustomerSubClass = 7;
                }
            }
            var subclass = string.Empty;
            if(param.CustomerSubClass!=0)
            {
                subclass = param.CustomerSubClass.ToString();
            }else
            {
                subclass = null;
            }
            if(param.IdentityType==0)
            {
                param.IdentityType = 1;
            }else if (param.IdentityType == 1)
            {
                param.IdentityType = 8;
            }
            else if (param.IdentityType == 2)
            {
                param.IdentityType = 2;
            }
            else if (param.IdentityType == 3)
            {
                param.IdentityType = 1;
            }
            else if (param.IdentityType == 4)
            {
                param.IdentityType = 3;
            }
            else if (param.IdentityType == 5)
            {
                param.IdentityType = 3;
            }
            else if (param.IdentityType == 6)
            {
                param.IdentityType = 5;
            }
            else if (param.IdentityType == 7)
            {
                param.IdentityType = 6;
            }
            else if (param.IdentityType == 8)
            {
                param.IdentityType = 7;
            }
            var gender = string.Empty;
            if(param.Gender!=0 && param.Gender!=null)
            {
                gender = param.Gender.ToString();
            }else
            {
                gender = null;
            }
            var customerclass = string.Empty;
            if(param.CustomerTypeDNET==0)
            {
                customerclass = "Retail";
            }else if(param.CustomerTypeDNET==1)
            {
                customerclass = "Corporate";
            }
            else if(param.CustomerTypeDNET==2)
            {
                customerclass = "Government/BUMN";
            }
            var cetakprovinsi = false;
            if(param.PrintRegion==0)
            {
                cetakprovinsi = true;
            }
            var interfacecustsales = false;
            if(param.InterfaceCustSales==0)
            {
                interfacecustsales = true;
            }
            var newparam = new KTB.DNet.Interface.Model.SendASBSFIDParameterDto.CustomerSend()
            {
                dealerCode = dealercode,
                context = "account",
                externalCode = param.ID.ToString(),
                extCodeRunning = param.GUIDUpdate,
                guidUpdate = param.GUIDUpdate,
                customerType = string.IsNullOrEmpty(param.CustomerType) ? "2" : param.CustomerType.ToString(),
                customerClass = customerclass,
                classType = Convert.ToInt32(param.CustomerTypeDNET),
                customerNo = param.CustomerNo,
                firstName = param.FirstName,
                lastName = param.LastName,
                aliasName = null,
                walkin = null,
                checkOverDue = null,
                checkCreditLimit = null,
                levelData = string.IsNullOrEmpty(param.LevelData) ? 3 : Convert.ToInt32(param.LevelData),
                identificationType = param.IdentityType,
                identificationNo = param.IdentityNumber,
                birthdate = param.BirthDate.HasValue ? (param.BirthDate.Value.ToString("dd/MM/yyyy") == "01/01/1753" ? null : param.BirthDate.Value.ToString("dd/MM/yyyy")) : null,
                taxZone = null,
                taxRegistrationNo = param.NPWPNo,
                taxRegistrationName = param.NPWPName,
                customerwithNPWP = null,
                homePhone = param.OtherPhoneNo,
                mobilePhone = param.HPNo,
                businessPhone = null,
                email = param.Email==""?null:param.Email,
                address1 = param.Alamat,
                gedung = param.Gedung,
                kelurahan = param.Kelurahan,
                kecamatan = param.Kecamatan,
                postalCode = param.PostalCode,
                city = CityCode,
                preArea = param.PreArea,
                cetakProvinsi = cetakprovinsi,
                pobox = param.POBox,
                tanggalAttach = null,
                linkKTPDNet = param.IdentityURLPath,
                interfaceUploadMessage = null,
                originatingLead = null,
                originatingContact = null,
                originatingCustomerPublic = null,
                customerPublicNo = null,
                preferredMethod = null,
                salesmanCode = param.SalesmanCode,
                parentCustomerNo = param.ParentCustomerNo,
                subClass = subclass,
                CountryCode = param.CountryCode,
                ocrnik = null,
                ocrniksim = null,
                attachments = null,
                gendercode = gender,
                InterfaceCustSales = interfacecustsales,
                ID = param.GUID
            };
            var json = JsonConvert.SerializeObject(newparam);
            BrokeredMessage message = new BrokeredMessage(json);
            _queueClient.SendAsync(message);
            result.success = true;
            result.total = 1;
            result._id = Convert.ToInt32(param.ID);
            return result ;
        }

        public ResponseBase<SendASBSFIDDto> Create(SendASBSFIDParameterDto objCreate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<SendASBSFIDDto> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<List<SendASBSFIDDto>> Read(SendASBSFIDFilterDto filterDto, int pageSize)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<SendASBSFIDDto> Update(SendASBSFIDParameterDto objUpdate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<SendASBSFIDDto> SendSuspect(KTB.DNet.Interface.Model.SendASBSFIDParameterDto.SuspectSFIDParameterDto param)
        {
            var validationResults = new List<DNetValidationResult>();
            var result = new ResponseBase<SendASBSFIDDto>();
            var sortColl = new SortCollection();
            var totalRow = 0;
            var dealercode = string.Empty;
            
            QueueClient _queueClient = null;
            try
            {
                _queueClient = QueueClient.CreateFromConnectionString(connectionString, queueName);
            }
            catch (Exception ex)
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.DataContentCorrupt, "Gagal koneksi ke ASB" + ex));
                return PopulateValidationError<SendASBSFIDDto>(validationResults, null);
            }

            var salesmancode = string.Empty;
            var dealerid = 0;
            var criteriassalesmanheader = new CriteriaComposite(new Criteria(typeof(SalesmanHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteriassalesmanheader.opAnd(new Criteria(typeof(SalesmanHeader), "ID", MatchType.Exact, param.SalesmanHeaderID));
            var datasalesmanheader = _salesmanHeaderMapper.RetrieveByCriteria(criteriassalesmanheader);
            if (datasalesmanheader.Count > 0)
            {
                var salesmanheader = datasalesmanheader[0] as SalesmanHeader;
                salesmancode = salesmanheader.SalesmanCode;
                if (salesmanheader.DealerBranch != null)
                {
                    dealerid = salesmanheader.DealerBranch.ID;
                }
                else
                {
                    dealerid = salesmanheader.Dealer.ID;
                }
            }
            if (dealerid != 0)
            {
                var criterias = new CriteriaComposite(new Criteria(typeof(Dealer), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(Dealer), "ID", MatchType.Exact, dealerid));
                var data = _dealerMapper.RetrieveByCriteria(criterias);
                if (data.Count > 0)
                {
                    var dealer = data[0] as Dealer;
                    dealercode = dealer.DealerCode;
                }
            }

            var vehiclecolorcode = string.Empty;
            if(param.VechileColorID!=null && param.VechileColorID!=0)
            {
                var criteriascolor = new CriteriaComposite(new Criteria(typeof(VechileColor), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criteriascolor.opAnd(new Criteria(typeof(VechileColor), "ID", MatchType.Exact, param.VechileColorID));
                var datacolor = _vehicleColorMapper.RetrieveByCriteria(criteriascolor);
                if (datacolor.Count > 0)
                {
                    var vehiclecolor = datacolor[0] as VechileColor;
                    vehiclecolorcode = vehiclecolor.ColorCode;
                }
            }
            
            var vehicletypecode = string.Empty;
            if (param.VechileTypeID != null && param.VechileTypeID != 0)
            {
                var criteriastipe = new CriteriaComposite(new Criteria(typeof(VechileType), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criteriastipe.opAnd(new Criteria(typeof(VechileType), "ID", MatchType.Exact, param.VechileTypeID));
                var datatipe = _vehicleTypeMapper.RetrieveByCriteria(criteriastipe);
                if (datatipe.Count > 0)
                {
                    var vehicletype = datatipe[0] as VechileType;
                    vehicletypecode = vehicletype.VechileTypeCode;
                }
            }

            var vehiclemodelcode = string.Empty;
            if(param.VechileModelID != 0 && param.VechileModelID != null)
            {
                var criteriasmodel = new CriteriaComposite(new Criteria(typeof(SalesVechileModel), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criteriasmodel.opAnd(new Criteria(typeof(SalesVechileModel), "ID", MatchType.Exact, param.VechileModelID));
                var datasalesmodel = _salesVehicleModelMapper.RetrieveByCriteria(criteriasmodel);
                if (datasalesmodel.Count > 0)
                {
                    var vehiclemodel = datasalesmodel[0] as SalesVechileModel;

                    var criteriasmodelcode= new CriteriaComposite(new Criteria(typeof(VechileModel), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    criteriasmodelcode.opAnd(new Criteria(typeof(VechileModel), "ID", MatchType.Exact, vehiclemodel.VechileModel.ID));
                    var datamodel = _vehicleModelMapper.RetrieveByCriteria(criteriasmodelcode);
                    if (datamodel.Count > 0)
                    {
                        var salesmodelcode = datamodel[0] as VechileModel;
                        vehiclemodelcode = salesmodelcode.Description;
                    }
                }
            }

            var customerclass = string.Empty;
            if (param.CustomerType == 0)
            {
                customerclass = "Retail";
            }
            else if (param.CustomerType == 1)
            {
                customerclass = "Corporate";
            }
            else if (param.CustomerType == 2)
            {
                customerclass = "Government/BUMN";
            }
            int? gender = null;
            if (param.Sex != 0 && param.Sex != null)
            {
                gender = param.Sex;
            }
            if (param.IdentityType == 0)
            {
                param.IdentityType = 1;
            }
            else if (param.IdentityType == 1)
            {
                param.IdentityType = 8;
            }
            else if (param.IdentityType == 2)
            {
                param.IdentityType = 2;
            }
            else if (param.IdentityType == 3)
            {
                param.IdentityType = 1;
            }
            else if (param.IdentityType == 4)
            {
                param.IdentityType = 3;
            }
            else if (param.IdentityType == 5)
            {
                param.IdentityType = 3;
            }
            else if (param.IdentityType == 6)
            {
                param.IdentityType = 5;
            }
            else if (param.IdentityType == 7)
            {
                param.IdentityType = 6;
            }
            else if (param.IdentityType == 8)
            {
                param.IdentityType = 7;
            }
            int? leadsource = null;
            if(param.InformationSource==5)
            {
                leadsource = 125;
            }else if(param.InformationSource==6)
            {
                leadsource = 126;
            }else if(param.InformationSource==7)
            {
                leadsource = 127;
            }else if(param.InformationSource==15)
            {
                leadsource = 135;
            }else if(param.InformationSource==16)
            {
                leadsource = 130;
            }else if (param.InformationSource==1)
            {
                leadsource = 121;
            }
            else if (param.InformationSource == 2)
            {
                leadsource = 122;
            }
            else if (param.InformationSource == 3)
            {
                leadsource = 123;
            }
            else if (param.InformationSource == 4)
            {
                leadsource = 124;
            }
            else if (param.InformationSource == 8)
            {
                leadsource = 128;
            }
            else if (param.InformationSource == 9)
            {
                leadsource = 129;
            }
            else if (param.InformationSource == 11)
            {
                leadsource = 131;
            }
            else if (param.InformationSource == 12)
            {
                leadsource = 132;
            }
            else if (param.InformationSource == 13)
            {
                leadsource = 133;
            }
            else if (param.InformationSource == 14)
            {
                leadsource = 134;
            }
            else if (param.InformationSource == 17)
            {
                leadsource = 137;
            }
            else if (param.InformationSource == 18)
            {
                leadsource = 138;
            }
            else if (param.InformationSource == 19)
            {
                leadsource = 139;
            }
            int? reasonforvisit = 0;
            if (param.CustomerPurpose == 1)
            {
                reasonforvisit = 121;
            }
            else if (param.CustomerPurpose == 2)
            {
                reasonforvisit = 122;
            }
            else if (param.CustomerPurpose == 3)
            {
                reasonforvisit = 123;
            }
            else if (param.CustomerPurpose == 4)
            {
                reasonforvisit = 124;
            }
            else if (param.CustomerPurpose == 5)
            {
                reasonforvisit = 125;
            }
            else if (param.CustomerPurpose == 6)
            {
                reasonforvisit = 126;
            }
            else if (param.CustomerPurpose == 7)
            {
                reasonforvisit = 127;
            }
            else if (param.CustomerPurpose == 8)
            {
                reasonforvisit = 128;
            }
            var json = string.Empty;
            var disqualifiedleadstatus = new List<int>() { 2, 4, 5, 6, 7 };
            if (param.Status==3 && param.LeadStatus==1)
            {
                var gen = 0;
                var newparam = new KTB.DNet.Interface.Model.SendASBSFIDParameterDto.SuspectSend()
                {
                    DealerCode = dealercode,
                    Context = "lead",
                    ExternalCode = param.ID.ToString(),
                    ExtCodeRunning = null,
                    Topic = param.Topic,
                    GUIDUpdate = param.GUIDUpdate,
                    IdentificationNumber = param.IdentityNumber,
                    MobilePhone = param.Phone,
                    GUID = param.GUID,
                    UpdateState = param.StateCode.ToString(),
                    FirstName = param.CustomerName,
                    LastName = param.Name2,
                    Email = param.Email,
                    Alamat = null,
                    Gedung = null,
                    Kelurahan = null,
                    Kecamatan = null,
                    PostalCode = null,
                    City = null,
                    CountryCode = param.CountryCode,
                    BusinessPhone = null,
                    HomePhone = param.Telp,
                    Catatan = param.Note,
                    CustomerClass = customerclass,
                    PreferredVecModel = param.VehicleComparison,
                    CurrentVehicleBrand = param.CurrVehicleBrand,
                    CurrentVehicleModel = param.CurrVehicleType,
                    RegistrationCode = null,
                    SubClass = null,
                    IdentificationType = param.IdentityType,
                    Gender = gender,
                    Quantity = param.Qty,
                    LeadSourceCode = leadsource,
                    CurrentVehicleBrandValue = null,
                    AgeSegment = null,
                    InformationSource = null,
                    ReasonForVisit = reasonforvisit == 0 ? null: reasonforvisit,
                    IndustryCode = null,
                    TypeOfVisit = null,
                    BirthDate = null,
                    LeadDate = param.ProspectDate.ToString("dd/MM/yyyy"),
                    EstimatePurchaseDate = null,
                    Product = vehicletypecode,
                    ProductColor = vehiclecolorcode,
                    EmployeeCode = salesmancode,
                    Contact = null,
                    Activities = null,
                    CampaignID = param.CampaignName,
                    VehicleModel = vehiclemodelcode
                };
                json = JsonConvert.SerializeObject(newparam);
            }else if (param.Status==3 && disqualifiedleadstatus.Contains(param.LeadStatus))
            {
                var newparam = new KTB.DNet.Interface.Model.SendASBSFIDParameterDto.SuspectDisqualifiedSend()
                {
                    DealerCode = dealercode,
                    Context = "lead",
                    ExternalCode = param.ID.ToString(),
                    ExtCodeRunning = null,
                    GUIDUpdate = param.GUIDUpdate,
                    ID = null,
                    UpdateState = param.LeadStatus,
                    Product = vehicletypecode,
                    ProductColor = vehiclecolorcode,
                    EmployeeCode = salesmancode
                };
                json = JsonConvert.SerializeObject(newparam);
            }
            BrokeredMessage message = new BrokeredMessage(json);
            _queueClient.SendAsync(message);
            result.success = true;
            result.total = 1;
            result._id = Convert.ToInt32(param.ID);
            return result;
        }

        
        public ResponseBase<SendASBSFIDDto> SendSuspectContact(KTB.DNet.Interface.Model.SendASBSFIDParameterDto.SuspectContactSFIDParameterDto param)
        {
            var validationResults = new List<DNetValidationResult>();
            var result = new ResponseBase<SendASBSFIDDto>();
            var sortColl = new SortCollection();
            var totalRow = 0;
            var dealercode = string.Empty;

            QueueClient _queueClient = null;
            try
            {
                _queueClient = QueueClient.CreateFromConnectionString(connectionString, queueName);
            }
            catch (Exception ex)
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.DataContentCorrupt, "Gagal koneksi ke ASB" + ex));
                return PopulateValidationError<SendASBSFIDDto>(validationResults, null);
            }

            var salesmancode = string.Empty;
            var dealerid = 0;
            var criteriassalesmanheader = new CriteriaComposite(new Criteria(typeof(SalesmanHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteriassalesmanheader.opAnd(new Criteria(typeof(SalesmanHeader), "ID", MatchType.Exact, param.Lead.SalesmanHeaderID));
            var datasalesmanheader = _salesmanHeaderMapper.RetrieveByCriteria(criteriassalesmanheader);
            if (datasalesmanheader.Count > 0)
            {
                var salesmanheader = datasalesmanheader[0] as SalesmanHeader;
                salesmancode = salesmanheader.SalesmanCode;
                if (salesmanheader.DealerBranch != null)
                {
                    dealerid = salesmanheader.DealerBranch.ID;
                }
                else
                {
                    dealerid = salesmanheader.Dealer.ID;
                }
            }
            if (dealerid != 0)
            {
                var criterias = new CriteriaComposite(new Criteria(typeof(Dealer), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(Dealer), "ID", MatchType.Exact, dealerid));
                var data = _dealerMapper.RetrieveByCriteria(criterias);
                if (data.Count > 0)
                {
                    var dealer = data[0] as Dealer;
                    dealercode = dealer.DealerCode;
                }
            }
            var vehiclecolorcode = string.Empty;
            if(param.Lead.VechileColorID!=0 && param.Lead.VechileColorID!=null)
            {
                var criteriascolor = new CriteriaComposite(new Criteria(typeof(VechileColor), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criteriascolor.opAnd(new Criteria(typeof(VechileColor), "ID", MatchType.Exact, param.Lead.VechileColorID));
                var datacolor = _vehicleColorMapper.RetrieveByCriteria(criteriascolor);
                if (datacolor.Count > 0)
                {
                    var vehiclecolor = datacolor[0] as VechileColor;
                    vehiclecolorcode = vehiclecolor.ColorCode;
                }
            }
            
            var vehicletypecode = string.Empty;
            if (param.Lead.VechileTypeID != 0 && param.Lead.VechileTypeID != null)
            {
                var criteriastipe = new CriteriaComposite(new Criteria(typeof(VechileType), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criteriastipe.opAnd(new Criteria(typeof(VechileType), "ID", MatchType.Exact, param.Lead.VechileTypeID));
                var datatipe = _vehicleTypeMapper.RetrieveByCriteria(criteriastipe);
                if (datatipe.Count > 0)
                {
                    var vehicletype = datatipe[0] as VechileType;
                    vehicletypecode = vehicletype.VechileTypeCode;
                }
            }

            var customerclass = string.Empty;
            if (param.Lead.CustomerType == 0)
            {
                customerclass = "Retail";
            }
            else if (param.Lead.CustomerType == 1)
            {
                customerclass = "Corporate";
            }
            else if (param.Lead.CustomerType == 2)
            {
                customerclass = "Government/BUMN";
            }
            int? gender = null;
            if (param.Lead.Sex != 0 && param.Lead.Sex != null)
            {
                gender = param.Lead.Sex;
            }
            if (param.Lead.IdentityType == 0)
            {
                param.Lead.IdentityType = 1;
            }
            else if (param.Lead.IdentityType == 1)
            {
                param.Lead.IdentityType = 8;
            }
            else if (param.Lead.IdentityType == 2)
            {
                param.Lead.IdentityType = 2;
            }
            else if (param.Lead.IdentityType == 3)
            {
                param.Lead.IdentityType = 1;
            }
            else if (param.Lead.IdentityType == 4)
            {
                param.Lead.IdentityType = 3;
            }
            else if (param.Lead.IdentityType == 5)
            {
                param.Lead.IdentityType = 3;
            }
            else if (param.Lead.IdentityType == 6)
            {
                param.Lead.IdentityType = 5;
            }
            else if (param.Lead.IdentityType == 7)
            {
                param.Lead.IdentityType = 6;
            }
            else if (param.Lead.IdentityType == 8)
            {
                param.Lead.IdentityType = 7;
            }
            
            int? reasonforvisit = 0;
            if(param.Lead.CustomerPurpose==1)
            {
                reasonforvisit = 121;
            }else if (param.Lead.CustomerPurpose == 2)
            {
                reasonforvisit = 122;
            }
            else if (param.Lead.CustomerPurpose == 3)
            {
                reasonforvisit = 123;
            }
            else if (param.Lead.CustomerPurpose == 4)
            {
                reasonforvisit = 124;
            }
            else if (param.Lead.CustomerPurpose == 5)
            {
                reasonforvisit = 125;
            }
            else if (param.Lead.CustomerPurpose == 6)
            {
                reasonforvisit = 126;
            }
            else if (param.Lead.CustomerPurpose == 7)
            {
                reasonforvisit = 127;
            }
            else if (param.Lead.CustomerPurpose == 8)
            {
                reasonforvisit = 128;
            }
            int? leadsource = null;
            if (param.Lead.InformationSource == 5)
            {
                leadsource = 125;
            }
            else if (param.Lead.InformationSource == 6)
            {
                leadsource = 126;
            }
            else if (param.Lead.InformationSource == 7)
            {
                leadsource = 127;
            }
            else if (param.Lead.InformationSource == 15)
            {
                leadsource = 135;
            }
            else if (param.Lead.InformationSource == 16)
            {
                leadsource = 130;
            }
            else if (param.Lead.InformationSource == 1)
            {
                leadsource = 121;
            }
            else if (param.Lead.InformationSource == 2)
            {
                leadsource = 122;
            }
            else if (param.Lead.InformationSource == 3)
            {
                leadsource = 123;
            }
            else if (param.Lead.InformationSource == 4)
            {
                leadsource = 124;
            }
            else if (param.Lead.InformationSource == 8)
            {
                leadsource = 128;
            }
            else if (param.Lead.InformationSource == 9)
            {
                leadsource = 129;
            }
            else if (param.Lead.InformationSource == 11)
            {
                leadsource = 131;
            }
            else if (param.Lead.InformationSource == 12)
            {
                leadsource = 132;
            }
            else if (param.Lead.InformationSource == 13)
            {
                leadsource = 133;
            }
            else if (param.Lead.InformationSource == 14)
            {
                leadsource = 134;
            }
            else if (param.Lead.InformationSource == 17)
            {
                leadsource = 137;
            }
            else if (param.Lead.InformationSource == 18)
            {
                leadsource = 138;
            }
            else if (param.Lead.InformationSource == 19)
            {
                leadsource = 139;
            }
            
            var newcontact = new KTB.DNet.Interface.Model.SendASBSFIDParameterDto.ContactSend()
            {
                IdentificationNumber = param.Lead.IdentityNumber,
                GUIDUpdate = param.Lead.GUIDUpdate,
                ExternalCode = param.Lead.ID.ToString(),
                MobilePhone = param.Lead.Phone,
                FirstName = param.Lead.CustomerName,
                LastName = param.Lead.Name2,
                HomePhone = param.Lead.Telp,
                Email = param.Lead.Email,
                Gender = gender,
                BirthDate = param.Lead.BirthDate.HasValue ?( param.Lead.BirthDate.Value.ToString("dd/MM/yyyy") == "01/01/1753" ? null : param.Lead.BirthDate.Value.ToString("dd/MM/yyyy")) : null,
                Address1 = null,
                Address2 = null,
                Address3 = null,
                Address4 = null,
                City = null,
                CustomerType = param.Lead.CustomerType,
                IdentificationType = param.Lead.IdentityType
            };
            var newparam = new KTB.DNet.Interface.Model.SendASBSFIDParameterDto.SuspectContactSend()
            {
                DealerCode = dealercode,
                Context ="lead",
                ExternalCode = param.Lead.ID.ToString(),
                ExtCodeRunning = null,
                Topic = param.Lead.Topic,
                GUIDUpdate = param.Lead.GUIDUpdate,
                IdentificationNumber = param.Lead.IdentityNumber,
                MobilePhone = param.Lead.Phone,
                Contact = newcontact,
                ID = param.Lead.GUID,
                UpdateState = 1,
                Catatan = param.Lead.Note,
                FirstName = param.Lead.CustomerName,
                LastName = param.Lead.Name2,
                Email = param.Lead.Email,
                Alamat = null,
                Gedung = null,
                Kelurahan = null,
                Kecamatan = null,
                PostalCode = null,
                City = null,
                CountryCode = param.Lead.CountryCode,
                BusinessPhone = null,
                HomePhone = param.Lead.Telp,
                CustomerClass = customerclass,
                PreferredVecModel = param.Lead.VehicleComparison,
                CurrentVehicleBrand = param.Lead.CurrVehicleBrand,
                CurrentVehicleModel = param.Lead.CurrVehicleType,
                RegistrationCode = null,
                SubClass = null,
                IdentificationType = param.Lead.IdentityType,
                Gender = gender,
                Quantity = param.Lead.Qty,
                LeadSourceCode = leadsource,
                CurrentVehicleBrandValue = null,
                AgeSegment = null,
                InformationSource = null,
                ReasonForVisit = reasonforvisit == 0 ? null : reasonforvisit,
                IndustryCode = null,
                TypeOfVisit = null,
                BirthDate = param.Lead.BirthDate.HasValue ? (param.Lead.BirthDate.Value.ToString("dd/MM/yyyy") == "01/01/1753" ? null : param.Lead.BirthDate.Value.ToString("dd/MM/yyyy")) : null,
                LeadDate = param.Lead.ProspectDate.ToString("dd/MM/yyyy"),
                EstimatePurchaseDate = null,
                Product = vehicletypecode,
                ProductColor = vehiclecolorcode,
                EmployeeCode = salesmancode,
            };

            var json = JsonConvert.SerializeObject(newparam);
            BrokeredMessage message = new BrokeredMessage(json);
            _queueClient.SendAsync(message);
            
            result.success = true;
            result.total = 1;
            result._id = Convert.ToInt32(param.Lead.ID);
            return result;
        }

        public ResponseBase<SendASBSFIDDto> SendProspect(SendASBSFIDParameterDto.SuspectSFIDParameterDto param)
        {
            var validationResults = new List<DNetValidationResult>();
            var result = new ResponseBase<SendASBSFIDDto>();
            var sortColl = new SortCollection();
            var totalRow = 0;
            var dealercode = string.Empty;

            QueueClient _queueClient = null;
            try
            {
                _queueClient = QueueClient.CreateFromConnectionString(connectionString, queueName);
            }
            catch (Exception ex)
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.DataContentCorrupt, "Gagal koneksi ke ASB" + ex));
                return PopulateValidationError<SendASBSFIDDto>(validationResults, null);
            }

            var salesmancode = string.Empty;
            var dealerid = 0;
            var criteriassalesmanheader = new CriteriaComposite(new Criteria(typeof(SalesmanHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteriassalesmanheader.opAnd(new Criteria(typeof(SalesmanHeader), "ID", MatchType.Exact, param.SalesmanHeaderID));
            var datasalesmanheader = _salesmanHeaderMapper.RetrieveByCriteria(criteriassalesmanheader);
            if (datasalesmanheader.Count > 0)
            {
                var salesmanheader = datasalesmanheader[0] as SalesmanHeader;
                salesmancode = salesmanheader.SalesmanCode;
                if (salesmanheader.DealerBranch != null)
                {
                    dealerid = salesmanheader.DealerBranch.ID;
                }
                else
                {
                    dealerid = salesmanheader.Dealer.ID;
                }
            }
            if (dealerid != 0)
            {
                var criterias = new CriteriaComposite(new Criteria(typeof(Dealer), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(Dealer), "ID", MatchType.Exact, dealerid));
                var data = _dealerMapper.RetrieveByCriteria(criterias);
                if (data.Count > 0)
                {
                    var dealer = data[0] as Dealer;
                    dealercode = dealer.DealerCode;
                }
            }

            var vehiclecolorcode = string.Empty;
            if(param.VechileColorID!=0 && param.VechileColorID!=null)
            {
                var criteriascolor = new CriteriaComposite(new Criteria(typeof(VechileColor), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criteriascolor.opAnd(new Criteria(typeof(VechileColor), "ID", MatchType.Exact, param.VechileColorID));
                var datacolor = _vehicleColorMapper.RetrieveByCriteria(criteriascolor);
                if (datacolor.Count > 0)
                {
                    var vehiclecolor = datacolor[0] as VechileColor;
                    vehiclecolorcode = vehiclecolor.ColorCode;
                }
            }
            var vehicletypecode = string.Empty;
            if (param.VechileTypeID != 0 && param.VechileTypeID != null)
            {
                var criteriastipe = new CriteriaComposite(new Criteria(typeof(VechileType), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criteriastipe.opAnd(new Criteria(typeof(VechileType), "ID", MatchType.Exact, param.VechileTypeID));
                var datatipe = _vehicleTypeMapper.RetrieveByCriteria(criteriastipe);
                if (datatipe.Count > 0)
                {
                    var vehicletype = datatipe[0] as VechileType;
                    vehicletypecode = vehicletype.VechileTypeCode;
                }
            }
            var vehiclemodelcode = string.Empty;
            if (param.VechileModelID != 0 && param.VechileModelID != null)
            {
                var criteriasmodel = new CriteriaComposite(new Criteria(typeof(SalesVechileModel), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criteriasmodel.opAnd(new Criteria(typeof(SalesVechileModel), "ID", MatchType.Exact, param.VechileModelID));
                var datasalesmodel = _salesVehicleModelMapper.RetrieveByCriteria(criteriasmodel);
                if (datasalesmodel.Count > 0)
                {
                    var vehiclemodel = datasalesmodel[0] as SalesVechileModel;

                    var criteriasmodelcode = new CriteriaComposite(new Criteria(typeof(VechileModel), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    criteriasmodelcode.opAnd(new Criteria(typeof(VechileModel), "ID", MatchType.Exact, vehiclemodel.VechileModel.ID));
                    var datamodel = _vehicleModelMapper.RetrieveByCriteria(criteriasmodelcode);
                    if (datamodel.Count > 0)
                    {
                        var salesmodelcode = datamodel[0] as VechileModel;
                        vehiclemodelcode = salesmodelcode.Description;
                    }
                }
            }
            var rating = param.Rating;
            if(param.Rating==5)
            {
                rating = 3;
            }
            else if (param.Rating == null || param.Rating == 0)
            {
                rating = 2;
            }
            var json = string.Empty;
            var statusprospect = new List<int>() { 1, 2, 5 };
            var statuscodeprospect = new List<int>() { 3,4,6,7,8,9,10,11,12,13 };
            int? bbntype = param.BBNType;
            int? jobkind = param.JobKind;
            if(param.JobKind==2)
            {
                jobkind = 1;
            }else if(param.JobKind==5)
            {
                jobkind = 2;
            }
            else if (param.JobKind == 8)
            {
                jobkind = 5;
            }
            else if (param.JobKind == 1)
            {
                jobkind = 7;
            }
            else if (param.JobKind == 4)
            {
                jobkind = 8;
            }
            else if (param.JobKind == 7)
            {
                jobkind =9;
            }
            if (statusprospect.Contains(param.Status) && param.LeadStatus==3)
            {
                if (param.StateCode == 0 && param.StatusCode==1)
                {
                    var newparam = new KTB.DNet.Interface.Model.SendASBSFIDParameterDto.ProspectSend()
                    {
                        GUID = param.GUID,
                        GUIDUpdate = param.GUIDUpdate,
                        ExtCodeRunning = null,
                        ExternalCode = param.ID.ToString(),
                        Topic = param.Topic,
                        ID = null,
                        UpdateState = param.StateCode.ToString(),
                        LeadId = param.GUID,
                        ContactId = null,
                        DealerCode = dealercode,
                        Context = "opportunity",
                        EstimatedCloseDate = null,
                        TipeBBN = bbntype == 0?null: bbntype,
                        BookingFee = param.BookingFee.ToString(),
                        Occupation = jobkind == 0 ? null : jobkind,
                        Rating =rating,
                        Quantity = param.Qty,
                        LeadDate = param.ProspectDate.ToString("dd/MM/yyyy"),
                        EmployeeCode = salesmancode,
                        ProductCode = vehicletypecode,
                        ProductColorCode = vehiclecolorcode,
                        Activities = null,
                        CampaignID = param.CampaignName,
                        Catatan = param.Note,
                        VehicleModel = vehiclemodelcode,
                        Email = param.Email,
                        BirthDate = param.BirthDate.HasValue ? (param.BirthDate.Value.ToString("dd/MM/yyyy") == "01/01/1753" ? null : param.BirthDate.Value.ToString("dd/MM/yyyy")) : null

                    };
                    json = JsonConvert.SerializeObject(newparam);
                }
                else if ((param.StateCode == 1 || param.StateCode == 2) && statuscodeprospect.Contains(param.StatusCode))
                {
                    if (param.StatusCode == 3)
                    {
                        var newparam = new KTB.DNet.Interface.Model.SendASBSFIDParameterDto.ProspectWonLostSend()
                        {
                            GUID = param.GUID,
                            GUIDUpdate = param.GUIDUpdate,
                            ExtCodeRunning = null,
                            ExternalCode = param.ID.ToString(),
                            Topic = param.Topic,
                            ID = null,
                            UpdateState = "1",
                            LeadId = param.GUID,
                            ContactId = null,
                            DealerCode = dealercode,
                            Context = "opportunity",
                            EstimatedCloseDate = null,
                            TipeBBN = bbntype == 0 ? null : bbntype,
                            BookingFee = param.BookingFee.ToString(),
                            Occupation = jobkind == 0 ? null : jobkind,
                            Rating = rating,
                            Quantity = param.Qty,
                            LeadDate = param.ProspectDate.ToString("dd/MM/yyyy"),
                            EmployeeCode = salesmancode,
                            ProductCode = vehicletypecode,
                            ProductColorCode = vehiclecolorcode,
                            AlasanBatal = null
                        };
                        json = JsonConvert.SerializeObject(newparam);
                    }
                    else
                    {
                        var newparam = new KTB.DNet.Interface.Model.SendASBSFIDParameterDto.ProspectWonLostSend()
                        {
                            GUID = param.GUID,
                            GUIDUpdate = param.GUIDUpdate,
                            ExtCodeRunning = null,
                            ExternalCode = param.ID.ToString(),
                            Topic = param.Topic,
                            ID = null,
                            UpdateState = "2",
                            LeadId = param.GUID,
                            ContactId = null,
                            DealerCode = dealercode,
                            Context = "opportunity",
                            EstimatedCloseDate =  null,
                            TipeBBN = bbntype == 0 ? null : bbntype,
                            BookingFee = param.BookingFee.ToString(),
                            Occupation = jobkind == 0 ? null : jobkind,
                            Rating = rating,
                            Quantity = param.Qty,
                            LeadDate = param.ProspectDate.ToString("dd/MM/yyyy"),
                            EmployeeCode = salesmancode,
                            ProductCode = vehicletypecode,
                            ProductColorCode = vehiclecolorcode,
                            AlasanBatal = param.StatusCode
                        };
                        json = JsonConvert.SerializeObject(newparam);
                    }
                    
                }

                BrokeredMessage message = new BrokeredMessage(json);
                _queueClient.SendAsync(message);
                result.success = true;
                result.total = 1;
                result._id = Convert.ToInt32(param.ID);
            }
            else
            {
                MessageBase msg = new MessageBase();
                msg.ErrorMessage = "Status tidak  valid";
                result.success = false;
                result.messages.Add(msg);
            }
            
            return result;
        }

        public ResponseBase<SendASBSFIDDto> SendActivity(KTB.DNet.Interface.Model.SendASBSFIDParameterDto.ActivitySFIDParameterDto param)
        {
            var validationResults = new List<DNetValidationResult>();
            var result = new ResponseBase<SendASBSFIDDto>();
            var sortColl = new SortCollection();
            var totalRow = 0;
            var dealercode = string.Empty;

            QueueClient _queueClient = null;
            try
            {
                _queueClient = QueueClient.CreateFromConnectionString(connectionString, queueName);
            }
            catch (Exception ex)
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.DataContentCorrupt, "Gagal koneksi ke ASB" + ex));
                return PopulateValidationError<SendASBSFIDDto>(validationResults, null);
            }

            var salesmancode = string.Empty;
            var dealerid = 0;
            var criteriassalesmanheader = new CriteriaComposite(new Criteria(typeof(SalesmanHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteriassalesmanheader.opAnd(new Criteria(typeof(SalesmanHeader), "ID", MatchType.Exact, param.lead.SalesmanHeaderID));
            var datasalesmanheader = _salesmanHeaderMapper.RetrieveByCriteria(criteriassalesmanheader);
            if (datasalesmanheader.Count > 0)
            {
                var salesmanheader = datasalesmanheader[0] as SalesmanHeader;
                salesmancode = salesmanheader.SalesmanCode;
                if (salesmanheader.DealerBranch != null)
                {
                    dealerid = salesmanheader.DealerBranch.ID;
                }
                else
                {
                    dealerid = salesmanheader.Dealer.ID;
                }
            }
            if (dealerid != 0)
            {
                var criterias = new CriteriaComposite(new Criteria(typeof(Dealer), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(Dealer), "ID", MatchType.Exact, dealerid));
                var data = _dealerMapper.RetrieveByCriteria(criterias);
                if (data.Count > 0)
                {
                    var dealer = data[0] as Dealer;
                    dealercode = dealer.DealerCode;
                }
            }

            var vehiclecolorcode = string.Empty;
            if(param.lead.VechileColorID!=null && param.lead.VechileColorID!=0)
            {
                var criteriascolor = new CriteriaComposite(new Criteria(typeof(VechileColor), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criteriascolor.opAnd(new Criteria(typeof(VechileColor), "ID", MatchType.Exact, param.lead.VechileColorID));
                var datacolor = _vehicleColorMapper.RetrieveByCriteria(criteriascolor);
                if (datacolor.Count > 0)
                {
                    var vehiclecolor = datacolor[0] as VechileColor;
                    vehiclecolorcode = vehiclecolor.ColorCode;
                }
            }
            var vehicletypecode = string.Empty;

            if (param.lead.VechileTypeID != null && param.lead.VechileTypeID != 0)
            {
                var criteriastipe = new CriteriaComposite(new Criteria(typeof(VechileType), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criteriastipe.opAnd(new Criteria(typeof(VechileType), "ID", MatchType.Exact, param.lead.VechileTypeID));
                var datatipe = _vehicleTypeMapper.RetrieveByCriteria(criteriastipe);
                if (datatipe.Count > 0)
                {
                    var vehicletype = datatipe[0] as VechileType;
                    vehicletypecode = vehicletype.VechileTypeCode;
                }
            }
            var vehiclemodelcode = string.Empty;
            if (param.lead.VechileModelID != 0 && param.lead.VechileModelID != null)
            {
                var criteriasmodel = new CriteriaComposite(new Criteria(typeof(SalesVechileModel), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criteriasmodel.opAnd(new Criteria(typeof(SalesVechileModel), "ID", MatchType.Exact, param.lead.VechileModelID));
                var datasalesmodel = _salesVehicleModelMapper.RetrieveByCriteria(criteriasmodel);
                if (datasalesmodel.Count > 0)
                {
                    var vehiclemodel = datasalesmodel[0] as SalesVechileModel;

                    var criteriasmodelcode = new CriteriaComposite(new Criteria(typeof(VechileModel), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    criteriasmodelcode.opAnd(new Criteria(typeof(VechileModel), "ID", MatchType.Exact, vehiclemodel.VechileModel.ID));
                    var datamodel = _vehicleModelMapper.RetrieveByCriteria(criteriasmodelcode);
                    if (datamodel.Count > 0)
                    {
                        var salesmodelcode = datamodel[0] as VechileModel;
                        vehiclemodelcode = salesmodelcode.Description;
                    }
                }
            }
            var profilegroupcode = string.Empty;
            if(param.activity.GroupID!=0)
            {
                var criteriasgroup = new CriteriaComposite(new Criteria(typeof(ProfileGroup), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criteriasgroup.opAnd(new Criteria(typeof(ProfileGroup), "ID", MatchType.Exact, param.activity.GroupID));
                var datagroup = _profileGroupMapper.RetrieveByCriteria(criteriasgroup);
                if (datagroup.Count > 0)
                {
                    var profilegroup = datagroup[0] as ProfileGroup;
                    profilegroupcode = profilegroup.Code;
                }
            }
            

            var customerclass = string.Empty;
            if (param.lead.CustomerType == 0)
            {
                customerclass = "Retail";
            }
            else if (param.lead.CustomerType == 1)
            {
                customerclass = "Corporate";
            }
            else if (param.lead.CustomerType == 2)
            {
                customerclass = "Government/BUMN";
            }
            int? gender = null;
            if (param.lead.Sex != 0 && param.lead.Sex != null)
            {
                gender = param.lead.Sex.Value;
            }

            if (param.lead.IdentityType == 0)
            {
                param.lead.IdentityType = 1;
            }
            else if (param.lead.IdentityType == 1)
            {
                param.lead.IdentityType = 8;
            }
            else if (param.lead.IdentityType == 2)
            {
                param.lead.IdentityType = 2;
            }
            else if (param.lead.IdentityType == 3)
            {
                param.lead.IdentityType = 1;
            }
            else if (param.lead.IdentityType == 4)
            {
                param.lead.IdentityType = 3;
            }
            else if (param.lead.IdentityType == 5)
            {
                param.lead.IdentityType = 3;
            }
            else if (param.lead.IdentityType == 6)
            {
                param.lead.IdentityType = 5;
            }
            else if (param.lead.IdentityType == 7)
            {
                param.lead.IdentityType = 6;
            }
            else if (param.lead.IdentityType == 8)
            {
                param.lead.IdentityType = 7;
            }
            int? leadsource = null;
            if(param.lead.InformationSource==5)
            {
                leadsource = 125;
            }else if(param.lead.InformationSource==6)
            {
                leadsource = 126;
            }else if(param.lead.InformationSource==7)
            {
                leadsource = 127;
            }else if(param.lead.InformationSource==15)
            {
                leadsource = 135;
            }else if(param.lead.InformationSource==16)
            {
                leadsource = 130;
            }else if (param.lead.InformationSource==1)
            {
                leadsource = 121;
            }
            else if (param.lead.InformationSource == 2)
            {
                leadsource = 122;
            }
            else if (param.lead.InformationSource == 3)
            {
                leadsource = 123;
            }
            else if (param.lead.InformationSource == 4)
            {
                leadsource = 124;
            }
            else if (param.lead.InformationSource == 8)
            {
                leadsource = 128;
            }
            else if (param.lead.InformationSource == 9)
            {
                leadsource = 129;
            }
            else if (param.lead.InformationSource == 11)
            {
                leadsource = 131;
            }
            else if (param.lead.InformationSource == 12)
            {
                leadsource = 132;
            }
            else if (param.lead.InformationSource == 13)
            {
                leadsource = 133;
            }
            else if (param.lead.InformationSource == 14)
            {
                leadsource = 134;
            }
            else if (param.lead.InformationSource == 17)
            {
                leadsource = 137;
            }
            else if (param.lead.InformationSource == 18)
            {
                leadsource = 138;
            }
            else if (param.lead.InformationSource == 19)
            {
                leadsource = 139;
            }
            var statuscode = 0;
            var activitystatus = 0;
            var activityresult = 0;
            var newparamActivitylist = new List<KTB.DNet.Interface.Model.SendASBSFIDParameterDto.Activities>();
            if(profilegroupcode== "SAPCustAct_JanjiTemu")
            {
                if(Convert.ToInt16(param.activity.JanjiTemu.HasilAktivitas)== 1)
                {
                    activityresult = 121;
                }else if (Convert.ToInt16(param.activity.JanjiTemu.HasilAktivitas) == 2)
                {
                    activityresult = 122;
                }
                else if (Convert.ToInt16(param.activity.JanjiTemu.HasilAktivitas) == 3)
                {
                    activityresult = 123;
                }

                if (Convert.ToInt16(param.activity.JanjiTemu.StatusAktivitas) == 1)
                {
                    activitystatus = 0;
                }
                else if (Convert.ToInt16(param.activity.JanjiTemu.StatusAktivitas) == 2)
                {
                    activitystatus = 3;
                }
                else if (Convert.ToInt16(param.activity.JanjiTemu.StatusAktivitas) == 3)
                {
                    activitystatus = 1;
                    statuscode = 3;
                }
                else if (Convert.ToInt16(param.activity.JanjiTemu.StatusAktivitas) == 4)
                {
                    activitystatus = 2;
                    statuscode = 4;
                }
                if (statuscode == 0)
                {
                    if (param.activity.JanjiTemu.TampilkanWaktu == "1A")
                    {
                        statuscode = 1;
                    }
                    else if (param.activity.JanjiTemu.TampilkanWaktu == "1B")
                    {
                        statuscode = 2;
                    }
                    else if (param.activity.JanjiTemu.TampilkanWaktu == "1C")
                    {
                        statuscode = 5;
                    }
                    else if (param.activity.JanjiTemu.TampilkanWaktu == "1D")
                    {
                        statuscode = 6;
                    }
                }
                int? type = null;
                if(param.activity.JanjiTemu.TipeJanjiTemu=="NK")
                {
                    type = 1;
                }else if (param.activity.JanjiTemu.TipeJanjiTemu=="KB")
                {
                    type = 2;
                }
                var newparamActivity = new KTB.DNet.Interface.Model.SendASBSFIDParameterDto.Activities()
                {
                    ActivityType = 1,
                    ScheduledStart = Convert.ToDateTime(param.activity.JanjiTemu.TanggalMulai),
                    ScheduledEnd = Convert.ToDateTime(param.activity.JanjiTemu.TanggalBerakhir),
                    ActivityResult = activityresult,
                    ActivityRating = Convert.ToInt32(param.activity.JanjiTemu.Rating),
                    ActivityStatus = activitystatus,
                    Subject = param.activity.JanjiTemu.Subject,
                    StatusCode = statuscode,
                    Catatan = param.activity.JanjiTemu.Catatan,
                    From = param.activity.SalesmanCode,
                    To = param.lead.GUIDUpdate,
                    CC = param.lead.GUIDUpdate,
                    Direction = null,
                    ExternalCode = param.activity.ProfileID.ToString(),
                    Type = type,
                    ShowTimeAs = param.activity.JanjiTemu.TampilkanWaktu,
                    Email = null,
                    Sender = null,
                    Receipt = null,
                    StatusReason = null,
                    Description = null,
                    Due = null
                };
                newparamActivitylist.Add(newparamActivity);
            }
            else if(profilegroupcode == "SAPCustAct_Email")
            {
                if (Convert.ToInt16(param.activity.Email.HasilAktivitas) == 1)
                {
                    activityresult = 121;
                }
                else if (Convert.ToInt16(param.activity.Email.HasilAktivitas) == 2)
                {
                    activityresult = 122;
                }
                else if (Convert.ToInt16(param.activity.Email.HasilAktivitas) == 3)
                {
                    activityresult = 123;
                }
                if (Convert.ToInt16(param.activity.Email.StatusAktivitas) == 1)
                {
                    activitystatus = 0;
                }
                else if (Convert.ToInt16(param.activity.Email.StatusAktivitas) == 2)
                {
                    activitystatus = 3;
                }
                else if (Convert.ToInt16(param.activity.Email.StatusAktivitas) == 3)
                {
                    activitystatus = 1;
                }
                else if (Convert.ToInt16(param.activity.Email.StatusAktivitas) == 4)
                {
                    activitystatus = 2;
                }
                var newparamActivity = new KTB.DNet.Interface.Model.SendASBSFIDParameterDto.Activities()
                {
                    ActivityType = 2,
                    ScheduledStart = null,
                    ScheduledEnd = null,
                    ActivityResult = activityresult,
                    ActivityRating = Convert.ToInt32(param.activity.Email.Rating),
                    ActivityStatus = activitystatus,
                    Subject = param.activity.Email.Subject,
                    StatusCode = null,
                    Catatan = param.activity.Email.Catatan,
                    From = param.activity.Email.EmailPengirim,
                    To = param.activity.Email.EmailPenerima,
                    CC = param.activity.Email.cc,
                    Direction = null,
                    ExternalCode = param.activity.ProfileID.ToString(),
                    Type = null,
                    ShowTimeAs = null,
                    Email = param.activity.Email.IsiPesan,
                    Sender = null,
                    Receipt = null,
                    StatusReason = null,
                    Description = null,
                    Due = Convert.ToDateTime(param.activity.Email.TanggalKirim).ToString("dd/MM/yyyy")
                };
                newparamActivitylist.Add(newparamActivity);
            }
            else if(profilegroupcode == "SAPCustAct_Call")
            {
                if (Convert.ToInt16(param.activity.Telp.HasilAktivitas) == 1)
                {
                    activityresult = 121;
                }
                else if (Convert.ToInt16(param.activity.Telp.HasilAktivitas) == 2)
                {
                    activityresult = 122;
                }
                else if (Convert.ToInt16(param.activity.Telp.HasilAktivitas) == 3)
                {
                    activityresult = 123;
                }
                if (Convert.ToInt16(param.activity.Telp.StatusAktivitas) == 1)
                {
                    activitystatus = 0;
                }
                else if (Convert.ToInt16(param.activity.Telp.StatusAktivitas) == 2)
                {
                    activitystatus = 3;
                }
                else if (Convert.ToInt16(param.activity.Telp.StatusAktivitas) == 3)
                {
                    activitystatus = 1;
                }
                else if (Convert.ToInt16(param.activity.Telp.StatusAktivitas) == 4)
                {
                    activitystatus = 2;
                }
                var newparamActivity = new KTB.DNet.Interface.Model.SendASBSFIDParameterDto.Activities()
                {
                    ActivityType = 3,
                    ScheduledStart = Convert.ToDateTime(param.activity.Telp.TanggalStart),
                    ScheduledEnd = Convert.ToDateTime(param.activity.Telp.TanggalEnd),
                    ActivityResult = activityresult,
                    ActivityRating = Convert.ToInt32(param.activity.Telp.Rating),
                    ActivityStatus = activitystatus,
                    Subject = param.activity.Telp.Subject,
                    StatusCode = null,
                    Catatan = param.activity.Telp.Catatan,
                    From = param.activity.SalesmanCode,
                    To = param.lead.GUIDUpdate,
                    CC = param.lead.GUIDUpdate,
                    Direction = 1,
                    ExternalCode = param.activity.ProfileID.ToString(),
                    Type = null,
                    ShowTimeAs = null,
                    Email = null,
                    Sender = param.activity.SalesmanCode,
                    Receipt = param.activity.Telp.NomorSuspect,
                    StatusReason = null,
                    Description = null,
                    Due = null
                };
                newparamActivitylist.Add(newparamActivity);
            }
            else if(profilegroupcode == "SAPCustAct_Tugas")
            {
                if (Convert.ToInt16(param.activity.PesanTugas.HasilAktivitas) == 1)
                {
                    activityresult = 121;
                }
                else if (Convert.ToInt16(param.activity.PesanTugas.HasilAktivitas) == 2)
                {
                    activityresult = 122;
                }
                else if (Convert.ToInt16(param.activity.PesanTugas.HasilAktivitas) == 3)
                {
                    activityresult = 123;
                }
                if (Convert.ToInt16(param.activity.PesanTugas.StatusAktivitas) == 1)
                {
                    activitystatus = 0;
                }
                else if (Convert.ToInt16(param.activity.PesanTugas.StatusAktivitas) == 2)
                {
                    activitystatus = 3;
                }
                else if (Convert.ToInt16(param.activity.PesanTugas.StatusAktivitas) == 3)
                {
                    activitystatus = 1;
                    statuscode = 5;
                }
                else if (Convert.ToInt16(param.activity.PesanTugas.StatusAktivitas) == 4)
                {
                    activitystatus = 2;
                    statuscode = 6;
                }
                if (statuscode == 0)
                {
                    if (Convert.ToInt16(param.activity.PesanTugas.Reason) == 1)
                    {
                        statuscode = 2;
                    }
                    else if (Convert.ToInt16(param.activity.PesanTugas.Reason) == 2)
                    {
                        statuscode = 3;
                    }
                    else if (Convert.ToInt16(param.activity.PesanTugas.Reason) == 3)
                    {
                        statuscode = 4;
                    }
                    else if (Convert.ToInt16(param.activity.PesanTugas.Reason) == 4)
                    {
                        statuscode = 7;
                    }
                }
                var newparamActivity = new KTB.DNet.Interface.Model.SendASBSFIDParameterDto.Activities()
                {
                    ActivityType = 4,
                    ScheduledStart = null,
                    ScheduledEnd = null,
                    ActivityResult = activityresult,
                    ActivityRating = Convert.ToInt32(param.activity.PesanTugas.Rating),
                    ActivityStatus = activitystatus,
                    Subject = param.activity.PesanTugas.Subject,
                    StatusCode = statuscode,
                    Catatan = param.activity.PesanTugas.Catatan,
                    From = param.activity.SalesmanCode,
                    To = param.lead.GUIDUpdate,
                    CC = param.lead.GUIDUpdate,
                    Direction = null,
                    ExternalCode = param.activity.ProfileID.ToString(),
                    Type = null,
                    ShowTimeAs = null,
                    Email = null,
                    Sender = param.activity.SalesmanCode,
                    Receipt = param.activity.PesanTugas.NomorSuspect,
                    StatusReason = param.activity.PesanTugas.Reason,
                    Description = param.activity.PesanTugas.Deskripsi,
                    Due = Convert.ToDateTime(param.activity.PesanTugas.TanggalKirim).ToString("dd/MM/yyyy")
                };
                newparamActivitylist.Add(newparamActivity);
            }

            int? reasonforvisit = 0;
            if (param.lead.CustomerPurpose == 1)
            {
                reasonforvisit = 121;
            }
            else if (param.lead.CustomerPurpose == 2)
            {
                reasonforvisit = 122;
            }
            else if (param.lead.CustomerPurpose == 3)
            {
                reasonforvisit = 123;
            }
            else if (param.lead.CustomerPurpose == 4)
            {
                reasonforvisit = 124;
            }
            else if (param.lead.CustomerPurpose == 5)
            {
                reasonforvisit = 125;
            }
            else if (param.lead.CustomerPurpose == 6)
            {
                reasonforvisit = 126;
            }
            else if (param.lead.CustomerPurpose == 7)
            {
                reasonforvisit = 127;
            }
            else if (param.lead.CustomerPurpose == 8)
            {
                reasonforvisit = 128;
            }
            var json = string.Empty;
            if(param.lead.LeadStatus!=3)
            {
                var newparam = new KTB.DNet.Interface.Model.SendASBSFIDParameterDto.ActivitySend()
                {
                    DealerCode = dealercode,
                    Context = "lead",
                    ExternalCode = param.lead.ID.ToString(),
                    ExtCodeRunning = null,
                    Topic = param.lead.Topic,
                    GUIDUpdate = param.lead.GUIDUpdate,
                    IdentificationNumber = param.lead.IdentityNumber,
                    MobilePhone = param.lead.Phone,
                    GUID = param.lead.GUID,
                    UpdateState = param.lead.StateCode.ToString(),
                    FirstName = param.lead.CustomerName,
                    LastName = param.lead.Name2,
                    Email = param.lead.Email,
                    Alamat = null,
                    Gedung = null,
                    Kelurahan = null,
                    Kecamatan = null,
                    PostalCode = null,
                    City = null,
                    CountryCode = param.lead.CountryCode,
                    BusinessPhone = null,
                    HomePhone = param.lead.Telp,
                    Catatan = param.lead.Note,
                    CustomerClass = customerclass,
                    PreferredVecModel = param.lead.VehicleComparison,
                    CurrentVehicleBrand = param.lead.CurrVehicleBrand,
                    CurrentVehicleModel = param.lead.CurrVehicleType,
                    RegistrationCode = null,
                    SubClass = null,
                    IdentificationType = param.lead.IdentityType,
                    Gender = gender,
                    Quantity = param.lead.Qty,
                    LeadSourceCode = leadsource,
                    CurrentVehicleBrandValue = null,
                    AgeSegment = null,
                    InformationSource = null,
                    ReasonForVisit = reasonforvisit == 0 ? null : reasonforvisit,
                    IndustryCode = null,
                    TypeOfVisit = null,
                    BirthDate = param.lead.BirthDate.HasValue ? (param.lead.BirthDate.Value.ToString("dd/MM/yyyy") == "01/01/1753" ? null : param.lead.BirthDate.Value.ToString("dd/MM/yyyy")) : null,
                    LeadDate = param.lead.ProspectDate.HasValue ? param.lead.ProspectDate.Value.ToString("dd/MM/yyyy") : null,
                    EstimatePurchaseDate = null,
                    Product = vehicletypecode,
                    ProductColor = vehiclecolorcode,
                    EmployeeCode = salesmancode,
                    Contact = null,
                    Activities = newparamActivitylist,
                    CampaignID = param.lead.CampaignName,
                    VehicleModel = vehiclemodelcode
                };

                json = JsonConvert.SerializeObject(newparam);
            }
            else
            {
                var rating = param.lead.Rating;
                if (param.lead.Rating == 5)
                {
                    rating = 3;
                }
                else if (param.lead.Rating == null || param.lead.Rating == 0)
                {
                    rating = 2;
                }
                int? bbntype = param.lead.BBNType;
                int? jobkind = param.lead.JobKind;
                if (param.lead.JobKind == 2)
                {
                    jobkind = 1;
                }
                else if (param.lead.JobKind == 5)
                {
                    jobkind = 2;
                }
                else if (param.lead.JobKind == 8)
                {
                    jobkind = 5;
                }
                else if (param.lead.JobKind == 1)
                {
                    jobkind = 7;
                }
                else if (param.lead.JobKind == 4)
                {
                    jobkind = 8;
                }
                else if (param.lead.JobKind == 7)
                {
                    jobkind = 9;
                }

                var newparam = new KTB.DNet.Interface.Model.SendASBSFIDParameterDto.CreateProspectSend()
                {
                    GUID = param.lead.GUID,
                    GUIDUpdate = param.lead.GUIDUpdate,
                    ExtCodeRunning = null,
                    ExternalCode = param.lead.ID.ToString(),
                    Topic = param.lead.Topic,
                    ID = null,
                    UpdateState = param.lead.StateCode.ToString(),
                    LeadId = param.lead.GUID,
                    ContactId = null,
                    DealerCode = dealercode,
                    Context = "opportunity",
                    EstimatedCloseDate = null,
                    TipeBBN = bbntype == 0 ? null : bbntype,
                    BookingFee = param.lead.BookingFee.ToString(),
                    Occupation = jobkind == 0 ? null : jobkind,
                    Rating = rating.Value,
                    Quantity = param.lead.Qty==0?0:param.lead.Qty.Value,
                    LeadDate = param.lead.ProspectDate.HasValue? param.lead.ProspectDate.Value.ToString("dd/MM/yyyy"):null,
                    EmployeeCode = salesmancode,
                    ProductCode = vehicletypecode,
                    ProductColorCode = vehiclecolorcode,
                    Activities = newparamActivitylist,
                    CampaignID = param.lead.CampaignName,
                    Catatan = param.lead.Note,
                    VehicleModel = vehiclemodelcode,
                    Email = param.lead.Email,
                    BirthDate = param.lead.BirthDate.HasValue ? (param.lead.BirthDate.Value.ToString("dd/MM/yyyy") == "01/01/1753" ? null : param.lead.BirthDate.Value.ToString("dd/MM/yyyy")) : null
                };
                json = JsonConvert.SerializeObject(newparam);
            }
            
                
            BrokeredMessage message = new BrokeredMessage(json);
            _queueClient.SendAsync(message);
            result.success = true;
            result.total = 1;
            result._id = Convert.ToInt32(param.activity.ProfileID);
            return result;
        }

        public ResponseBase<SendASBSFIDDto> SendActivityContact(KTB.DNet.Interface.Model.SendASBSFIDParameterDto.ActivityContactSFIDParameterDto param)
        {
            var validationResults = new List<DNetValidationResult>();
            var result = new ResponseBase<SendASBSFIDDto>();
            var sortColl = new SortCollection();
            var totalRow = 0;
            var dealercode = string.Empty;

            QueueClient _queueClient = null;
            try
            {
                _queueClient = QueueClient.CreateFromConnectionString(connectionString, queueName);
            }
            catch (Exception ex)
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.DataContentCorrupt, "Gagal koneksi ke ASB" + ex));
                return PopulateValidationError<SendASBSFIDDto>(validationResults, null);
            }

            var salesmancode = string.Empty;
            var dealerid = 0;
            var criteriassalesmanheader = new CriteriaComposite(new Criteria(typeof(SalesmanHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteriassalesmanheader.opAnd(new Criteria(typeof(SalesmanHeader), "SalesmanCode", MatchType.Exact, param.SalesmanCode));
            var datasalesmanheader = _salesmanHeaderMapper.RetrieveByCriteria(criteriassalesmanheader);
            if (datasalesmanheader.Count > 0)
            {
                var salesmanheader = datasalesmanheader[0] as SalesmanHeader;
                salesmancode = salesmanheader.SalesmanCode;
                if(salesmanheader.DealerBranch!=null)
                {
                    dealerid = salesmanheader.DealerBranch.ID;
                }else
                {
                    dealerid = salesmanheader.Dealer.ID;
                }
            }
            if(dealerid!=0)
            {
                var criterias = new CriteriaComposite(new Criteria(typeof(Dealer), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(Dealer), "ID", MatchType.Exact, dealerid));
                var data = _dealerMapper.RetrieveByCriteria(criterias);
                if (data.Count > 0)
                {
                    var dealer = data[0] as Dealer;
                    dealercode = dealer.DealerCode;
                }
            }
            
            //var vehiclecolorcode = string.Empty;
            //var criteriascolor = new CriteriaComposite(new Criteria(typeof(VechileColor), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            //criteriascolor.opAnd(new Criteria(typeof(VechileColor), "ID", MatchType.Exact, param.VechileColorID));
            //var datacolor = _vehicleColorMapper.RetrieveByCriteria(criteriascolor);
            //if (datacolor.Count > 0)
            //{
            //    var vehiclecolor = datacolor[0] as VechileColor;
            //    vehiclecolorcode = vehiclecolor.ColorCode;
            //}
            //var vehicletypecode = string.Empty;
            //var criteriastipe = new CriteriaComposite(new Criteria(typeof(VechileType), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            //criteriastipe.opAnd(new Criteria(typeof(VechileType), "ID", MatchType.Exact, param.VechileTypeID));
            //var datatipe = _vehicleTypeMapper.RetrieveByCriteria(criteriastipe);
            //if (datatipe.Count > 0)
            //{
            //    var vehicletype = datatipe[0] as VechileType;
            //    vehicletypecode = vehicletype.VechileTypeCode;
            //}


            //var newparam = new ProspectSend()
            //{
            //    GUID = param.GUID,
            //    GUIDUpdate = param.GUIDUpdate,
            //    ExtCodeRunning = null,
            //    ExternalCode = param.ID.ToString(),
            //    Topic = param.Topic,
            //    ID = null,
            //    UpdateState = param.StateCode.ToString(),
            //    LeadId = null,
            //    ContactId = null,
            //    DealerCode = dealercode,
            //    Context = "opportunity",
            //    EstimatedCloseDate = param.EstimatedCloseDate.ToString("dd/MM/yyyy"),
            //    TipeBBN = param.BBNType,
            //    BookingFee = param.BookingFee.ToString(),
            //    Occupation = param.JobKind,
            //    Rating = param.Status,
            //    Quantity = param.Qty,
            //    LeadDate = param.ProspectDate.ToString("dd/MM/yyyy"),
            //    EmployeeCode = salesmancode,
            //    ProductCode = vehicletypecode,
            //    ProductColorCode = vehiclecolorcode
            //};
            var json = JsonConvert.SerializeObject(param);

            BrokeredMessage message = new BrokeredMessage(json);
            _queueClient.SendAsync(message);
            result.success = true;
            result.total = 1;
            result._id = Convert.ToInt32(param.ProfileID);
            return result;
        }
        public ResponseBase<SendASBSFIDDto> SendActivitySuspect(KTB.DNet.Interface.Model.SendASBSFIDParameterDto.ActivitySuspectQualifiedSend param)
        {
            var validationResults = new List<DNetValidationResult>();
            var result = new ResponseBase<SendASBSFIDDto>();
            var sortColl = new SortCollection();
            var totalRow = 0;
            var dealercode = string.Empty;

            QueueClient _queueClient = null;
            try
            {
                _queueClient = QueueClient.CreateFromConnectionString(connectionString, queueName);
            }
            catch (Exception ex)
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.DataContentCorrupt, "Gagal koneksi ke ASB" + ex));
                return PopulateValidationError<SendASBSFIDDto>(validationResults, null);
            }

            var criterias = new CriteriaComposite(new Criteria(typeof(Dealer), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(Dealer), "ID", MatchType.Exact, param.DealerID));
            var data = _dealerMapper.RetrieveByCriteria(criterias);
            if (data.Count > 0)
            {
                var dealer = data[0] as Dealer;
                dealercode = dealer.DealerCode;
            }
            var salesmancode = string.Empty;
            var criteriassalesmanheader = new CriteriaComposite(new Criteria(typeof(SalesmanHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteriassalesmanheader.opAnd(new Criteria(typeof(SalesmanHeader), "SalesmanCode", MatchType.Exact, param.SalesmanCode));
            var datasalesmanheader = _salesmanHeaderMapper.RetrieveByCriteria(criteriassalesmanheader);
            if (datasalesmanheader.Count > 0)
            {
                var salesmanheader = datasalesmanheader[0] as SalesmanHeader;
                salesmancode = salesmanheader.SalesmanCode;
            }

            //var vehiclecolorcode = string.Empty;
            //var criteriascolor = new CriteriaComposite(new Criteria(typeof(VechileColor), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            //criteriascolor.opAnd(new Criteria(typeof(VechileColor), "ID", MatchType.Exact, param.VechileColorID));
            //var datacolor = _vehicleColorMapper.RetrieveByCriteria(criteriascolor);
            //if (datacolor.Count > 0)
            //{
            //    var vehiclecolor = datacolor[0] as VechileColor;
            //    vehiclecolorcode = vehiclecolor.ColorCode;
            //}
            //var vehicletypecode = string.Empty;
            //var criteriastipe = new CriteriaComposite(new Criteria(typeof(VechileType), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            //criteriastipe.opAnd(new Criteria(typeof(VechileType), "ID", MatchType.Exact, param.VechileTypeID));
            //var datatipe = _vehicleTypeMapper.RetrieveByCriteria(criteriastipe);
            //if (datatipe.Count > 0)
            //{
            //    var vehicletype = datatipe[0] as VechileType;
            //    vehicletypecode = vehicletype.VechileTypeCode;
            //}


            //var newparam = new ProspectSend()
            //{
            //    GUID = param.GUID,
            //    GUIDUpdate = param.GUIDUpdate,
            //    ExtCodeRunning = null,
            //    ExternalCode = param.ID.ToString(),
            //    Topic = param.Topic,
            //    ID = null,
            //    UpdateState = param.StateCode.ToString(),
            //    LeadId = null,
            //    ContactId = null,
            //    DealerCode = dealercode,
            //    Context = "opportunity",
            //    EstimatedCloseDate = param.EstimatedCloseDate.ToString("dd/MM/yyyy"),
            //    TipeBBN = param.BBNType,
            //    BookingFee = param.BookingFee.ToString(),
            //    Occupation = param.JobKind,
            //    Rating = param.Status,
            //    Quantity = param.Qty,
            //    LeadDate = param.ProspectDate.ToString("dd/MM/yyyy"),
            //    EmployeeCode = salesmancode,
            //    ProductCode = vehicletypecode,
            //    ProductColorCode = vehiclecolorcode
            //};
            var json = JsonConvert.SerializeObject(param);

            BrokeredMessage message = new BrokeredMessage(json);
            _queueClient.SendAsync(message);
            result.success = true;
            result.total = param.JanjiTemu.Count()+param.Tugas.Count()+param.Email.Count()+param.Telp.Count();
            result._id = 0;
            return result;
        }

        public ResponseBase<SendASBSFIDDto> SendProspectCreate(SendASBSFIDParameterDto.CreateProspect param)
        {
            var validationResults = new List<DNetValidationResult>();
            var result = new ResponseBase<SendASBSFIDDto>();
            var sortColl = new SortCollection();
            var totalRow = 0;
            var dealercode = string.Empty;

            QueueClient _queueClient = null;
            try
            {
                _queueClient = QueueClient.CreateFromConnectionString(connectionString, queueName);
            }
            catch (Exception ex)
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.DataContentCorrupt, "Gagal koneksi ke ASB" + ex));
                return PopulateValidationError<SendASBSFIDDto>(validationResults, null);
            }

            var salesmancode = string.Empty;
            var dealerid = 0;
            var criteriassalesmanheader = new CriteriaComposite(new Criteria(typeof(SalesmanHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteriassalesmanheader.opAnd(new Criteria(typeof(SalesmanHeader), "ID", MatchType.Exact, param.lead.SalesmanHeaderID));
            var datasalesmanheader = _salesmanHeaderMapper.RetrieveByCriteria(criteriassalesmanheader);
            if (datasalesmanheader.Count > 0)
            {
                var salesmanheader = datasalesmanheader[0] as SalesmanHeader;
                salesmancode = salesmanheader.SalesmanCode;
                if (salesmanheader.DealerBranch != null)
                {
                    dealerid = salesmanheader.DealerBranch.ID;
                }
                else
                {
                    dealerid = salesmanheader.Dealer.ID;
                }
            }
            if (dealerid != 0)
            {
                var criterias = new CriteriaComposite(new Criteria(typeof(Dealer), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(Dealer), "ID", MatchType.Exact, dealerid));
                var data = _dealerMapper.RetrieveByCriteria(criterias);
                if (data.Count > 0)
                {
                    var dealer = data[0] as Dealer;
                    dealercode = dealer.DealerCode;
                }
            }

            var vehiclecolorcode = string.Empty;
            if (param.lead.VechileColorID != 0 && param.lead.VechileColorID != null)
            {
                var criteriascolor = new CriteriaComposite(new Criteria(typeof(VechileColor), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criteriascolor.opAnd(new Criteria(typeof(VechileColor), "ID", MatchType.Exact, param.lead.VechileColorID));
                var datacolor = _vehicleColorMapper.RetrieveByCriteria(criteriascolor);
                if (datacolor.Count > 0)
                {
                    var vehiclecolor = datacolor[0] as VechileColor;
                    vehiclecolorcode = vehiclecolor.ColorCode;
                }
            }

            var vehicletypecode = string.Empty;
            if (param.lead.VechileTypeID != 0 && param.lead.VechileTypeID != null)
            {
                var criteriastipe = new CriteriaComposite(new Criteria(typeof(VechileType), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criteriastipe.opAnd(new Criteria(typeof(VechileType), "ID", MatchType.Exact, param.lead.VechileTypeID));
                var datatipe = _vehicleTypeMapper.RetrieveByCriteria(criteriastipe);
                if (datatipe.Count > 0)
                {
                    var vehicletype = datatipe[0] as VechileType;
                    vehicletypecode = vehicletype.VechileTypeCode;
                }
            }
            
            var vehiclemodelcode = string.Empty;
            if (param.lead.VechileModelID != 0 && param.lead.VechileModelID != null)
            {
                var criteriasmodel = new CriteriaComposite(new Criteria(typeof(SalesVechileModel), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criteriasmodel.opAnd(new Criteria(typeof(SalesVechileModel), "ID", MatchType.Exact, param.lead.VechileModelID));
                var datasalesmodel = _salesVehicleModelMapper.RetrieveByCriteria(criteriasmodel);
                if (datasalesmodel.Count > 0)
                {
                    var vehiclemodel = datasalesmodel[0] as SalesVechileModel;

                    var criteriasmodelcode = new CriteriaComposite(new Criteria(typeof(VechileModel), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    criteriasmodelcode.opAnd(new Criteria(typeof(VechileModel), "ID", MatchType.Exact, vehiclemodel.VechileModel.ID));
                    var datamodel = _vehicleModelMapper.RetrieveByCriteria(criteriasmodelcode);
                    if (datamodel.Count > 0)
                    {
                        var salesmodelcode = datamodel[0] as VechileModel;
                        vehiclemodelcode = salesmodelcode.Description;
                    }
                }
            }

            var rating = param.lead.Rating;
            if (param.lead.Rating == 5)
            {
                rating = 3;
            }else if(param.lead.Rating==null || param.lead.Rating==0)
            {
                rating = 2;
            }
            var json = string.Empty;
            var statusprospect = new List<int>() { 1, 2, 5 };
            var statuscodeprospect = new List<int>() { 4, 6, 7, 8, 9, 10, 11, 12, 13 };
            int? bbntype = param.lead.BBNType;

            int? jobkind = param.lead.JobKind;
            if (param.lead.JobKind == 2)
            {
                jobkind = 1;
            }
            else if (param.lead.JobKind == 5)
            {
                jobkind = 2;
            }
            else if (param.lead.JobKind == 8)
            {
                jobkind = 5;
            }
            else if (param.lead.JobKind == 1)
            {
                jobkind = 7;
            }
            else if (param.lead.JobKind == 4)
            {
                jobkind = 8;
            }
            else if (param.lead.JobKind == 7)
            {
                jobkind = 9;
            }

            //activity
            var activityresult = 0;
            var activitystatus = 0;
            var statuscode = 0;
            var newparamActivitylist = new List<KTB.DNet.Interface.Model.SendASBSFIDParameterDto.Activities>();
            if(param.activity.JanjiTemu.Count()>0)
            {
                foreach(var i in param.activity.JanjiTemu)
                {
                    if (Convert.ToInt16(i.HasilAktivitas) == 1)
                    {
                        activityresult = 121;
                    }
                    else if (Convert.ToInt16(i.HasilAktivitas) == 2)
                    {
                        activityresult = 122;
                    }
                    else if (Convert.ToInt16(i.HasilAktivitas) == 3)
                    {
                        activityresult = 123;
                    }

                    if (Convert.ToInt16(i.StatusAktivitas) == 1)
                    {
                        activitystatus = 0;
                    }
                    else if (Convert.ToInt16(i.StatusAktivitas) == 2)
                    {
                        activitystatus = 3;
                    }
                    else if (Convert.ToInt16(i.StatusAktivitas) == 3)
                    {
                        activitystatus = 1;
                        statuscode = 3;
                    }
                    else if (Convert.ToInt16(i.StatusAktivitas) == 4)
                    {
                        activitystatus = 2;
                        statuscode = 4;
                    }
                    if(statuscode==0)
                    {
                        if(i.TampilkanWaktu=="1A")
                        {
                            statuscode = 1;
                        }
                        else if (i.TampilkanWaktu == "1B")
                        {
                            statuscode = 2;
                        }
                        else if (i.TampilkanWaktu == "1C")
                        {
                            statuscode = 5;
                        }
                        else if (i.TampilkanWaktu == "1D")
                        {
                            statuscode = 6;
                        }
                    }
                    int? type = null;
                    if (i.TipeJanjiTemu == "NK")
                    {
                        type = 1;
                    }
                    else if (i.TipeJanjiTemu == "KB")
                    {
                        type = 2;
                    }
                    var newparamActivity = new KTB.DNet.Interface.Model.SendASBSFIDParameterDto.Activities()
                    {
                        ActivityType = 1,
                        ScheduledStart = Convert.ToDateTime(i.TanggalMulai),
                        ScheduledEnd = Convert.ToDateTime(i.TanggalBerakhir),
                        ActivityResult = activityresult,
                        ActivityRating = Convert.ToInt32(i.Rating),
                        ActivityStatus = activitystatus,
                        Subject = i.Subject,
                        StatusCode = statuscode,
                        Catatan = i.Catatan,
                        From = param.activity.SalesmanCode,
                        To = param.lead.GUIDUpdate,
                        CC = param.lead.GUIDUpdate,
                        Direction = null,
                        ExternalCode = i.ProfileID.ToString(),
                        Type = type,
                        ShowTimeAs = i.TampilkanWaktu,
                        Email = null,
                        Sender = null,
                        Receipt = null,
                        StatusReason = null,
                        Description = null,
                        Due = null
                    };
                    newparamActivitylist.Add(newparamActivity);
                }
            }
            if (param.activity.Email.Count() > 0)
            {
                foreach (var i in param.activity.Email)
                {
                    if (Convert.ToInt16(i.HasilAktivitas) == 1)
                    {
                        activityresult = 121;
                    }
                    else if (Convert.ToInt16(i.HasilAktivitas) == 2)
                    {
                        activityresult = 122;
                    }
                    else if (Convert.ToInt16(i.HasilAktivitas) == 3)
                    {
                        activityresult = 123;
                    }
                    if (Convert.ToInt16(i.StatusAktivitas) == 1)
                    {
                        activitystatus = 0;
                    }
                    else if (Convert.ToInt16(i.StatusAktivitas) == 2)
                    {
                        activitystatus = 3;
                    }
                    else if (Convert.ToInt16(i.StatusAktivitas) == 3)
                    {
                        activitystatus = 1;
                    }
                    else if (Convert.ToInt16(i.StatusAktivitas) == 4)
                    {
                        activitystatus = 2;
                    }
                    var newparamActivity = new KTB.DNet.Interface.Model.SendASBSFIDParameterDto.Activities()
                    {
                        ActivityType = 2,
                        ScheduledStart = null,
                        ScheduledEnd = null,
                        ActivityResult = activityresult,
                        ActivityRating = Convert.ToInt32(i.Rating),
                        ActivityStatus = activitystatus,
                        Subject = i.Subject,
                        StatusCode = null,
                        Catatan = i.Catatan,
                        From = i.EmailPengirim,
                        To = i.EmailPenerima,
                        CC = i.cc,
                        Direction = null,
                        ExternalCode = i.ProfileID.ToString(),
                        Type = null,
                        ShowTimeAs = null,
                        Email = i.IsiPesan,
                        Sender = null,
                        Receipt = null,
                        StatusReason = null,
                        Description = null,
                        Due = Convert.ToDateTime(i.TanggalKirim).ToString("dd/MM/yyyy")
                    };
                    newparamActivitylist.Add(newparamActivity);
                }
            }
            if (param.activity.Telp.Count() > 0)
            {
                foreach (var i in param.activity.Telp)
                {
                    if (Convert.ToInt16(i.HasilAktivitas) == 1)
                    {
                        activityresult = 121;
                    }
                    else if (Convert.ToInt16(i.HasilAktivitas) == 2)
                    {
                        activityresult = 122;
                    }
                    else if (Convert.ToInt16(i.HasilAktivitas) == 3)
                    {
                        activityresult = 123;
                    }
                    if (Convert.ToInt16(i.StatusAktivitas) == 1)
                    {
                        activitystatus = 0;
                    }
                    else if (Convert.ToInt16(i.StatusAktivitas) == 2)
                    {
                        activitystatus = 3;
                    }
                    else if (Convert.ToInt16(i.StatusAktivitas) == 3)
                    {
                        activitystatus = 1;
                    }
                    else if (Convert.ToInt16(i.StatusAktivitas) == 4)
                    {
                        activitystatus = 2;
                    }
                    var newparamActivity = new KTB.DNet.Interface.Model.SendASBSFIDParameterDto.Activities()
                    {
                        ActivityType = 3,
                        ScheduledStart = Convert.ToDateTime(i.TanggalStart),
                        ScheduledEnd = Convert.ToDateTime(i.TanggalEnd),
                        ActivityResult = activityresult,
                        ActivityRating = Convert.ToInt32(i.Rating),
                        ActivityStatus = activitystatus,
                        Subject = i.Subject,
                        StatusCode = null,
                        Catatan = i.Catatan,
                        From = param.activity.SalesmanCode,
                        To = param.lead.GUIDUpdate,
                        CC = param.lead.GUIDUpdate,
                        Direction = 1,
                        ExternalCode = i.ProfileID.ToString(),
                        Type = null,
                        ShowTimeAs = null,
                        Email = null,
                        Sender = param.activity.SalesmanCode,
                        Receipt = i.NomorSuspect,
                        StatusReason = null,
                        Description = null,
                        Due = null
                    };
                    newparamActivitylist.Add(newparamActivity);
                }
            }
            if (param.activity.Tugas.Count() > 0)
            {
                foreach (var i in param.activity.Tugas)
                {
                    if (Convert.ToInt16(i.HasilAktivitas) == 1)
                    {
                        activityresult = 121;
                    }
                    else if (Convert.ToInt16(i.HasilAktivitas) == 2)
                    {
                        activityresult = 122;
                    }
                    else if (Convert.ToInt16(i.HasilAktivitas) == 3)
                    {
                        activityresult = 123;
                    }

                    if (Convert.ToInt16(i.StatusAktivitas) == 1)
                    {
                        activitystatus = 0;
                    }
                    else if (Convert.ToInt16(i.StatusAktivitas) == 2)
                    {
                        activitystatus = 3;
                    }
                    else if (Convert.ToInt16(i.StatusAktivitas) == 3)
                    {
                        activitystatus = 1;
                        statuscode = 5;
                    }
                    else if (Convert.ToInt16(i.StatusAktivitas) == 4)
                    {
                        activitystatus = 2;
                        statuscode = 6;
                    }
                    if (statuscode == 0)
                    {
                        if (Convert.ToInt16(i.Reason) == 1)
                            {
                            statuscode = 2;
                        }
                        else if (Convert.ToInt16(i.Reason) == 2)
                        {
                            statuscode = 3;
                        }
                        else if (Convert.ToInt16(i.Reason) == 3)
                        {
                            statuscode = 4;
                        }
                        else if (Convert.ToInt16(i.Reason) == 4)
                        {
                            statuscode = 7;
                        }
                    }

                    var newparamActivity = new KTB.DNet.Interface.Model.SendASBSFIDParameterDto.Activities()
                    {
                        ActivityType = 4,
                        ScheduledStart = null,
                        ScheduledEnd = null,
                        ActivityResult = activityresult,
                        ActivityRating = Convert.ToInt32(i.Rating),
                        ActivityStatus = activitystatus,
                        Subject = i.Subject,
                        StatusCode = statuscode,
                        Catatan = i.Catatan,
                        From = param.activity.SalesmanCode,
                        To = param.lead.GUIDUpdate,
                        CC = param.lead.GUIDUpdate,
                        Direction = null,
                        ExternalCode = i.ProfileID.ToString(),
                        Type = null,
                        ShowTimeAs = null,
                        Email = null,
                        Sender = param.activity.SalesmanCode,
                        Receipt = i.NomorSuspect,
                        StatusReason = i.Reason,
                        Description = i.Deskripsi,
                        Due = Convert.ToDateTime(i.TanggalKirim).ToString("dd/MM/yyyy")
                    };
                    newparamActivitylist.Add(newparamActivity);
                }
            }
            //activity
            var newparam = new KTB.DNet.Interface.Model.SendASBSFIDParameterDto.CreateProspectSend()
            {
                GUID = param.lead.GUID,
                GUIDUpdate = param.lead.GUIDUpdate,
                ExtCodeRunning = null,
                ExternalCode = param.lead.ID.ToString(),
                Topic = param.lead.Topic,
                ID = null,
                UpdateState = param.lead.StateCode.ToString(),
                LeadId = param.lead.GUID,
                ContactId = null,
                DealerCode = dealercode,
                Context = "opportunity",
                EstimatedCloseDate = null,
                TipeBBN = bbntype == 0 ? null : bbntype,
                BookingFee = param.lead.BookingFee.ToString(),
                Occupation = jobkind == 0 ? null : jobkind,
                Rating = rating,
                Quantity = param.lead.Qty,
                LeadDate = param.lead.ProspectDate.ToString("dd/MM/yyyy"),
                EmployeeCode = salesmancode,
                ProductCode = vehicletypecode,
                ProductColorCode = vehiclecolorcode,
                Activities = newparamActivitylist,
                CampaignID = param.lead.CampaignName,
                Catatan = param.lead.Note,
                VehicleModel = vehiclemodelcode,
                Email = param.lead.Email,
                BirthDate = param.lead.BirthDate.HasValue ?( param.lead.BirthDate.Value.ToString("dd/MM/yyyy") == "01/01/1753" ? null : param.lead.BirthDate.Value.ToString("dd/MM/yyyy") ): null
            };
            json = JsonConvert.SerializeObject(newparam);
                
            BrokeredMessage message = new BrokeredMessage(json);
            _queueClient.SendAsync(message);
            result.success = true;
            result.total = 1;
            result._id = Convert.ToInt32(param.lead.ID);
            
            return result;
        }
    }
}
