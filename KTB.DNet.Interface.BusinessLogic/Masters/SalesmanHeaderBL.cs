#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SalesmanHeader business logic class
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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using IFDomain = KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Repository.Interface;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class SalesmanHeaderBL : AbstractBusinessLogic, ISalesmanHeaderBL
    {
        #region Variables
        private readonly IMapper _salesmanHeaderMapper;
        private readonly IMapper _salesmanDSEMapper;
        private readonly IMapper _salesmanProfileMapper;
        private readonly IMapper _profileGroupMapper;
        private readonly IMapper _profileDetailMapper;
        private readonly IMapper _salesmanCategoryLevelMapper;
        private readonly IMapper _dealerAdditionalMapper;
        private readonly IMapper _v_SparePartOrganizationMapper;
        private readonly IMapper _jobPossitionToMenuMapper;
        private readonly IMapper _salesmanAreaMapper;
        private readonly IMapper _salesmanLevelMapper;
        private readonly IMapper _profileHeaderMapper;
        private readonly IMapper _jobPositionMapper;
        private readonly IMapper _dealerMapper;
        private readonly IMapper _dealerSystemMapper;
        private readonly AutoMapper.IMapper _mapper;
        private TransactionManager _transactionManager;
        private StandardCodeBL _enumBL;
        private const string ROW_STATUS = "RowStatus";
        private const string CODE = "Code";
        private const int SALES_JOB_POSITION_MENU = 1;
        private readonly IMapper _spkMasterCountryCodePhoneMapper;
        #endregion

        #region Constructor
        public SalesmanHeaderBL()
        {
            _salesmanHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(SalesmanHeader).ToString());
            _salesmanDSEMapper = MapperFactory.GetInstance().GetMapper(typeof(SalesmanDSE).ToString());
            _salesmanProfileMapper = MapperFactory.GetInstance().GetMapper(typeof(SalesmanProfile).ToString());
            _profileGroupMapper = MapperFactory.GetInstance().GetMapper(typeof(ProfileGroup).ToString());
            _profileDetailMapper = MapperFactory.GetInstance().GetMapper(typeof(ProfileDetail).ToString());
            _salesmanCategoryLevelMapper = MapperFactory.GetInstance().GetMapper(typeof(SalesmanCategoryLevel).ToString());
            _dealerAdditionalMapper = MapperFactory.GetInstance().GetMapper(typeof(DealerAdditional).ToString());
            _v_SparePartOrganizationMapper = MapperFactory.GetInstance().GetMapper(typeof(V_SparePartOrganization).ToString());
            _jobPossitionToMenuMapper = MapperFactory.GetInstance().GetMapper(typeof(JobPositionToMenu).ToString());
            _salesmanAreaMapper = MapperFactory.GetInstance().GetMapper(typeof(SalesmanArea).ToString());
            _salesmanLevelMapper = MapperFactory.GetInstance().GetMapper(typeof(SalesmanLevel).ToString());
            _profileHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(ProfileHeader).ToString());
            _jobPositionMapper = MapperFactory.GetInstance().GetMapper(typeof(JobPosition).ToString());
            _dealerMapper = MapperFactory.GetInstance().GetMapper(typeof(Dealer).ToString());
            _dealerSystemMapper = MapperFactory.GetInstance().GetMapper(typeof(DealerSystems).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _enumBL = new StandardCodeBL(_mapper);
            _transactionManager = new TransactionManager();
            _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);
            _spkMasterCountryCodePhoneMapper = MapperFactory.GetInstance().GetMapper(typeof(SPKMasterCountryCodePhone).ToString());
        }
        #endregion

        #region General
        /// <summary>
        /// Get SalesmanHeader by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<SalesmanHeaderDto>> Read(SalesmanHeaderFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(SalesmanHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<SalesmanHeaderDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(SalesmanHeader), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(SalesmanHeader), filterDto, sortColl);

                // get data
                var data = _salesmanHeaderMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<SalesmanHeader>().ToList();
                    var listData = new List<SalesmanHeaderDto>();
                    foreach (var item in list)
                    {
                        // map it
                        var salesmanheaderDto = _mapper.Map<SalesmanHeaderDto>(item);

                        if (item.Dealer != null)
                        {
                            salesmanheaderDto.Dealer = _mapper.Map<DealerDto>(item.Dealer);
                        }
                        if (item.JobPosition != null)
                        {
                            salesmanheaderDto.JobPosition = _mapper.Map<JobPositionDto>(item.JobPosition);
                        }
                        if (item.SalesmanArea != null)
                        {
                            salesmanheaderDto.SalesmanArea = _mapper.Map<SalesmanAreaDto>(item.SalesmanArea);
                        }
                        if (item.SalesmanLevel != null)
                        {
                            salesmanheaderDto.SalesmanLevel = _mapper.Map<SalesmanLevelDto>(item.SalesmanLevel);
                        }

                        // add to list
                        listData.Add(salesmanheaderDto);
                    }

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SalesmanHeader), filterDto);
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
        /// Delete SalesmanHeader by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<SalesmanHeaderDto> Delete(int id)
        {
            var result = new ResponseBase<SalesmanHeaderDto>();

            try
            {
                var salesmanheader = (SalesmanHeader)_salesmanHeaderMapper.Retrieve(id);
                if (salesmanheader != null)
                {
                    salesmanheader.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _salesmanHeaderMapper.Update(salesmanheader, DNetUserName);
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
        /// Create a new SalesmanHeader
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<SalesmanHeaderDto> Create(SalesmanHeaderParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Update SalesmanHeader
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<SalesmanHeaderDto> Update(SalesmanHeaderParameterDto objUpdate)
        {
            return null;
        }
        #endregion

        #region EmployeeSales
        /// <summary>
        /// Create EmployeeSales 
        /// </summary>
        /// <param name="paramDto"></param>
        /// <returns></returns>
        public ResponseBase<EmployeeSalesDto> CreateEmployeeSales(EmployeeSalesParameterDto paramDto)
        {
            #region Initialize
            // set default response
            var result = new ResponseBase<EmployeeSalesDto>();
            List<DNetValidationResult> validationResults = new List<DNetValidationResult>();
            List<SalesmanProfile> listOfSalesmanProfile = new List<SalesmanProfile>();
            // id for create
            paramDto.ID = 0;
            SalesmanHeader salesmanDomain = null;
            bool isValid = true;
            #endregion

            try
            {
                if (isValid) { isValid = ValidateEmployeeSales(paramDto, validationResults, ref salesmanDomain); }

                if (isValid) { isValid = ValidateSalesmanImage(paramDto.Image, validationResults, salesmanDomain); }

                if (isValid) { isValid = ValidateSalesmanProfile<EmployeeSalesParameterDto>(paramDto, validationResults, Constants.ProfileGroup.SalesmanDatabaseUnit, salesmanDomain, listOfSalesmanProfile); }

                if (isValid)
                {
                    int resultID = 0;
                    if (salesmanDomain.ID != 0)
                    {
                        resultID = UpdateWithTransactionManager(salesmanDomain, listOfSalesmanProfile, null);
                    }
                    else
                    {
                        resultID = InsertWithTransactionManager(salesmanDomain, listOfSalesmanProfile, null);
                    }

                    if (resultID > 0)
                    {
                        result.success = true;
                        result._id = resultID;
                        result.total = 1;
                        result.lst = _mapper.Map<EmployeeSalesDto>(salesmanDomain);
                    }
                    else
                    {
                        ErrorMsgHelper.ErrorMsgDBSaveContactAdmin(result.messages);
                    }
                }
                else
                {
                    return PopulateValidationError<EmployeeSalesDto>(validationResults, null);
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
        /// Update employee sales
        /// </summary>
        /// <param name="paramDto"></param>
        /// <returns></returns>
        public ResponseBase<EmployeeSalesDto> UpdateEmployeeSales(EmployeeSalesParameterDto paramDto)
        {
            #region Initialize
            // set default response
            var result = new ResponseBase<EmployeeSalesDto>();
            List<DNetValidationResult> validationResults = new List<DNetValidationResult>();
            List<SalesmanProfile> listOfSalesmanProfile = new List<SalesmanProfile>();
            SalesmanHeader salesmanDomain = null;
            SalesmanHeader salesmanOnDb = null;
            bool isValid = true;
            bool isUpdateSalesmanDSE = false;
            SalesmanDSE salesmanDSE = null;

            #endregion

            try
            {
                if (isValid) { isValid = GetCurrentSalesmanObject(paramDto, validationResults, ref salesmanOnDb); }

                if (isValid) { isValid = ValidateEmployeeSales(paramDto, validationResults, ref salesmanDomain, salesmanOnDb); }

                if (isValid) { isValid = ValidateSalesmanImage(paramDto.Image, validationResults, salesmanDomain); }

                if (isValid) { isValid = ValidateSalesmanProfile<EmployeeSalesParameterDto>(paramDto, validationResults, Constants.ProfileGroup.SalesmanDatabaseUnit, salesmanDomain, listOfSalesmanProfile); }

                if (isValid)
                {
                    ValidateSalesmanDSE(paramDto, salesmanOnDb, ref isUpdateSalesmanDSE, ref salesmanDSE);
                    int updateResultID = UpdateWithTransactionManager(salesmanDomain, listOfSalesmanProfile, null, isUpdateSalesmanDSE, salesmanDSE);
                    if (updateResultID > 0)
                    {
                        if (isUpdateSalesmanDSE)
                        {
                            _salesmanDSEMapper.Update(salesmanDSE, DNetUserName);
                        }

                        result.success = true;
                        result._id = updateResultID;
                        result.total = 1;
                        result.lst = _mapper.Map<EmployeeSalesDto>(salesmanDomain);
                    }
                    else
                    {
                        ErrorMsgHelper.ErrorMsgDBSaveContactAdmin(result.messages);
                    }
                }
                else
                {
                    return PopulateValidationError<EmployeeSalesDto>(validationResults, null);
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

        private void ValidateSalesmanDSE(EmployeeSalesParameterDto paramDto, SalesmanHeader salesmanOnDb, ref bool isUpdateSalesmanDSE, ref SalesmanDSE salesmanDSE)
        {
            isUpdateSalesmanDSE = false;
            salesmanDSE = new SalesmanDSE();

            // check salesmanDSE based on SalesmanHeaderID
            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(SalesmanDSE), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(SalesmanDSE), "SalesmanHeader.ID", MatchType.Exact, salesmanOnDb.ID));
            var lst = _salesmanDSEMapper.RetrieveByCriteria(criterias);
            if (lst.Count > 0)
            {
                salesmanDSE = (SalesmanDSE)lst[0];

                // compare phone number
                foreach (SalesmanProfile item in salesmanOnDb.SalesmanProfiles)
                {
                    if (item.ProfileHeader.Code == "NO_HP")
                    {
                        if (item.ProfileValue != paramDto.No_HP)
                        {
                            // set update salesman DSE
                            isUpdateSalesmanDSE = true;
                            salesmanDSE.PhoneNumber = paramDto.No_HP;
                        }
                    }
                }

                // update salesman dse if salesman is resign
                if (paramDto.ResignDate != Convert.ToDateTime("01/01/1753 0:00:00"))
                {
                    isUpdateSalesmanDSE = true;
                    salesmanDSE.RowStatus = -1;
                }
            }
        }
        #endregion

        #region EmployeeParts
        /// <summary>
        /// Create SalesmanHeader for EmployeePart
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<EmployeePartDto> CreateEmployeePart(EmployeePartParameterDto objCreate)
        {
            #region Initialize
            var result = new ResponseBase<EmployeePartDto>();
            List<SalesmanProfile> salesProfileList = new List<SalesmanProfile>();
            SalesmanAdditionalInfo additionalInfo = null;
            List<DNetValidationResult> validationResults = new List<DNetValidationResult>();
            // id for create
            objCreate.ID = 0;
            bool isValid = true;
            SalesmanHeader domainSalesman = null;
            #endregion

            try
            {
                if (isValid) { isValid = ValidateEmployeePart(objCreate, validationResults, ref domainSalesman); }

                if (isValid) { isValid = ValidateSalesmanProfile<EmployeePartParameterDto>(objCreate, validationResults, Constants.ProfileGroup.SalesmanDatabaseSpareParts, domainSalesman, salesProfileList); }

                if (isValid) { isValid = ValidateSalesmanAdditionalInfo(objCreate, domainSalesman, validationResults, ref additionalInfo); }

                if (isValid) { isValid = ValidateSalesmanImage(objCreate.Image, validationResults, domainSalesman); }

                if (isValid)
                {
                    int insertedID = InsertWithTransactionManager(domainSalesman, salesProfileList, additionalInfo);
                    if (insertedID > 0)
                    {
                        result.success = true;
                        result._id = insertedID;
                        result.total = 1;
                        result.lst = _mapper.Map<EmployeePartDto>(domainSalesman);
                    }
                    else
                    {
                        ErrorMsgHelper.ErrorMsgDBSaveContactAdmin(result.messages);
                    }
                }
                else
                {
                    return PopulateValidationError<EmployeePartDto>(validationResults, null);
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
        /// Update SalesmanHeader for EmployeePart
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<EmployeePartDto> UpdateEmployeePart(EmployeePartParameterDto objUpdate)
        {
            #region Initialize
            // set default response
            var result = new ResponseBase<EmployeePartDto>();
            SalesmanHeader domainSalesman = null;
            List<SalesmanProfile> salesProfileList = new List<SalesmanProfile>();
            SalesmanAdditionalInfo additionalInfo = null;
            List<DNetValidationResult> validationResults = new List<DNetValidationResult>();
            bool isValid = true;
            #endregion

            try
            {
                if (isValid) { isValid = ValidateEmployeePart(objUpdate, validationResults, ref domainSalesman, true); }

                if (isValid) { isValid = ValidateSalesmanProfile<EmployeePartParameterDto>(objUpdate, validationResults, Constants.ProfileGroup.SalesmanDatabaseSpareParts, domainSalesman, salesProfileList); }

                if (isValid) { isValid = ValidateSalesmanAdditionalInfo(objUpdate, domainSalesman, validationResults, ref additionalInfo, true); }

                if (isValid) { isValid = ValidateSalesmanImage(objUpdate.Image, validationResults, domainSalesman); }

                if (isValid)
                {
                    int updatedID = UpdateWithTransactionManager(domainSalesman, salesProfileList, additionalInfo);
                    if (updatedID > 0)
                    {
                        result.success = true;
                        result._id = updatedID;
                        result.total = 1;
                        result.lst = _mapper.Map<EmployeePartDto>(domainSalesman);
                    }
                    else
                    {
                        ErrorMsgHelper.ErrorMsgDBSaveContactAdmin(result.messages);
                    }
                }
                else
                {
                    return PopulateValidationError<EmployeePartDto>(validationResults, null);
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
        #endregion

        #region Private Methods

        #region Transaction manager
        /// <summary>
        /// Insert with transaction manager
        /// </summary>
        /// <param name="salesmandomain"></param>
        /// <param name="listOfSalesmanProfile"></param>
        /// <param name="additionalInfo"></param>
        /// <returns></returns>
        private int InsertWithTransactionManager(SalesmanHeader salesmandomain, List<SalesmanProfile> listOfSalesmanProfile, SalesmanAdditionalInfo additionalInfo)
        {
            int result = -1;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // add command to insert salesman
                    this._transactionManager.AddInsert(salesmandomain, DNetUserName);

                    // add command to insert salesman profile
                    foreach (SalesmanProfile profile in listOfSalesmanProfile)
                    {
                        profile.SalesmanHeader = salesmandomain;
                        this._transactionManager.AddInsert(profile, DNetUserName);

                        profile.MarkLoaded();

                        // insert profile history
                        this._transactionManager.AddInsert(new SalesmanProfileHistory() { SalesmanProfile = profile, ProvileValue = profile.ProfileValue }, DNetUserName);
                    }

                    // insert if there's any additionalInfo 
                    if (additionalInfo != null)
                    {
                        additionalInfo.SalesmanHeader = salesmandomain;
                        this._transactionManager.AddInsert(additionalInfo, DNetUserName);
                    }

                    this._transactionManager.PerformTransaction();

                    result = salesmandomain.ID;
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
        private int UpdateWithTransactionManager(SalesmanHeader salesmandomain, List<SalesmanProfile> listOfSalesmanProfile, SalesmanAdditionalInfo additionalInfo, bool isUpdateSalesmanDSE = false, SalesmanDSE salesmanDSE = null)
        {
            // set default result
            int result = -1;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // add command to insert salesman profile
                    foreach (SalesmanProfile profile in listOfSalesmanProfile)
                    {
                        profile.SalesmanHeader = salesmandomain;
                        if (profile.ID != 0)
                        {
                            this._transactionManager.AddUpdate(profile, DNetUserName);
                        }
                        else
                        {
                            this._transactionManager.AddInsert(profile, DNetUserName);
                        }

                        profile.MarkLoaded();

                        // insert profile history
                        this._transactionManager.AddInsert(new SalesmanProfileHistory() { SalesmanProfile = profile, ProvileValue = profile.ProfileValue }, DNetUserName);
                    }

                    // update if there's any additionalInfo 
                    if (additionalInfo != null)
                    {
                        additionalInfo.SalesmanHeader = salesmandomain;
                        if (additionalInfo.ID != 0)
                        {
                            this._transactionManager.AddUpdate(additionalInfo, DNetUserName);
                        }
                        else
                        {
                            this._transactionManager.AddInsert(additionalInfo, DNetUserName);
                        }
                    }

                    // add command to update spk
                    _transactionManager.AddUpdate(salesmandomain, DNetUserName);

                    _transactionManager.PerformTransaction();

                    result = salesmandomain.ID;
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
        /// Insert with trasaction manager response handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void InsertWithTransactionManagerHandler(object sender, TransactionManager.OnInsertArgs args)
        {
            // set the object ID from db returned id
            if (args.DomainObject.GetType() == typeof(SalesmanHeader))
            {
                ((SalesmanHeader)args.DomainObject).ID = args.ID;
                ((SalesmanHeader)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(SalesmanProfile))
            {
                ((SalesmanProfile)args.DomainObject).ID = args.ID;
                ((SalesmanProfile)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(SalesmanAdditionalInfo))
            {
                ((SalesmanAdditionalInfo)args.DomainObject).ID = args.ID;
                ((SalesmanAdditionalInfo)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(SalesmanProfileHistory))
            {
                ((SalesmanProfileHistory)args.DomainObject).ID = args.ID;
                ((SalesmanProfileHistory)args.DomainObject).MarkLoaded();
            }
        }
        #endregion

        /// <summary>
        /// Validate employee part
        /// </summary>
        /// <param name="employeeSalesParam"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidateEmployeeSales(EmployeeSalesParameterDto employeeSalesParam, List<DNetValidationResult> validationResults, ref SalesmanHeader salesmanDomain, SalesmanHeader salesmanOnDB = null)
        {
            #region Initialize
            bool isUpdate = employeeSalesParam.ID != 0;
            Dealer dealer = null;
            SalesmanLevel level = null;
            DealerBranch dealerBranch = null;
            #endregion

            #region Validate Gender
            if (!_enumBL.IsExistByCategoryAndValue("EnumGender.Gender", employeeSalesParam.Gender.ToString()))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgGenderInvalid, FieldResource.Gender)));
            }
            #endregion

            #region Validate Status
            if (!_enumBL.IsExistByCategoryAndValue("EnumSalesmanStatus.SalesmanStatus", employeeSalesParam.Status))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSalesmanStatusInvalid, FieldResource.Status)));
            }
            #endregion

            #region Validate Married Status
            if (!_enumBL.IsExistByCategoryAndValue("EnumSalesmanMarriedStatus.MarriedStatus", employeeSalesParam.MarriedStatus))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgMarriedStatusInvalid, FieldResource.MarriedStatus)));
            }
            #endregion

            #region ValidateReffSalesmanCode
            if (!string.IsNullOrEmpty(employeeSalesParam.RefSalesmanCode))
            {
                ValidationHelper.ValidateSalesmanHeader(employeeSalesParam.RefSalesmanCode, DealerCode, validationResults, ref salesmanDomain, false);
                if (validationResults.Any()) { return false; }
            }

            #endregion

            string CityOfBirth = string.Empty;
            #region Validate KTP
            if (!isUpdate)
            {
                if (salesmanDomain != null)
                {
                    ValidateKTP(employeeSalesParam, validationResults, salesmanDomain, isUpdate);
                }
                else
                {
                    SalesmanHeader salesmanHeaders = employeeSalesParam.ConvertObject<SalesmanHeader>();

                    ValidateKTP(employeeSalesParam, validationResults, salesmanHeaders, isUpdate);
                }
            }
            else
            {
                ValidateKTP(employeeSalesParam, validationResults, salesmanOnDB, isUpdate);
            }

            if (validationResults.Any()) { return false; }
            else
            {
                CityOfBirth = ValidatePlaceOfBirth(employeeSalesParam, validationResults);
            }
            #endregion

            #region Validate Dealer Category
            ValidationHelper.ValidateDealerCategory(this.DealerCode, employeeSalesParam.Kategori_Tim, validationResults);
            #endregion

            // get valid JobPosition
            JobPosition jobPosition = GetValidJobPositionByToMenu(SALES_JOB_POSITION_MENU, employeeSalesParam.JobPositionCode, validationResults);

            // get valid city
            City city = GetValidCity(employeeSalesParam.CityCode, validationResults);

            // get vaild dealer            
            ValidationHelper.ValidateDealer(employeeSalesParam.DealerCode, validationResults, this.DealerCode, ref dealer);

            // get valid salesman area
            SalesmanArea salesmanArea = GetValidSalesmanArea(employeeSalesParam.SalesmanAreaCode, validationResults);

            // check if it is manager
            bool isManager = IsManager(jobPosition);
            bool isBranchManager = IsBranchManager(jobPosition);

            // get valid leader
            SalesmanHeader leader = GetValidLeader(employeeSalesParam.LeaderCode, employeeSalesParam.DealerCode, jobPosition, isManager, isBranchManager, validationResults);

            //get valid jobpositionid_leader
            var jobpositionleaderID = 0;
            if (leader != null)
            {
                jobpositionleaderID = leader.JobPosition.ID;
            }
            // validate dealer branch code if any
            ValidationHelper.ValidateDealerBranch(this.DealerCode, validationResults, employeeSalesParam.DealerBranchCode, ref dealerBranch);

            // get valid salesman level
            #region Validate SalesmanLevel
            if (jobPosition != null)
            {
                if (isManager || isBranchManager)
                {
                    level = GetSalesmanLevelBlank();
                }
                else
                {
                    // validate salesman level
                    level = GetValidSalesmanLevel(employeeSalesParam.SalesmanLevelID, isManager, isBranchManager, validationResults);
                }
            }
            #endregion

            // if any error found
            if (validationResults.Count > 0) { return false; }

            // if update
            if (isUpdate)
            {
                string status = salesmanOnDB.Status;
                salesmanDomain = employeeSalesParam.ConvertObject<SalesmanHeader>(salesmanOnDB);
                salesmanDomain.Status = status;
                // Resign validation
                ValidateResignation(employeeSalesParam.ResignType, employeeSalesParam.ResignDate, employeeSalesParam.ResignReason, validationResults, ref salesmanDomain, isBranchManager, true);
            }
            else
            {
                // create new
                if (salesmanDomain == null)
                {
                    salesmanDomain = employeeSalesParam.ConvertObject<SalesmanHeader>();
                    salesmanDomain.RegisterStatus = "0";
                    if (this.ValidateDealerSystems() > 2)
                    {
                        salesmanDomain.IsRequestID = 1;
                    }
                    else
                    {
                        salesmanDomain.IsRequestID = 0;
                    }

                    salesmanDomain.Status = "1";
                    salesmanDomain.IsRequestID = 1;
                }
                // create from the x-employee data
                else
                {
                    var oldSalesmanID = salesmanDomain.ID;
                    salesmanDomain = employeeSalesParam.ConvertObject<SalesmanHeader>(salesmanDomain);
                    salesmanDomain.ID = oldSalesmanID;
                    salesmanDomain.RegisterStatus = "1";
                    salesmanDomain.Status = "4";
                    salesmanDomain.ResignDate = Constants.DATETIME_DEFAULT_VALUE;
                    salesmanDomain.ResignReason = string.Empty;
                    salesmanDomain.ResignType = 0;
                }

                // unit = 1
                salesmanDomain.SalesIndicator = 1;
            }
            salesmanDomain.PlaceOfBirth = CityOfBirth;
            salesmanDomain.JobPositionId_Leader = jobpositionleaderID;
            salesmanDomain.JobPosition = jobPosition;
            salesmanDomain.City = city.CityName.ToUpper();
            salesmanDomain.IsOtherCity = employeeSalesParam.IsOtherCity;
            salesmanDomain.Dealer = dealer;
            salesmanDomain.DealerBranch = dealerBranch;
            salesmanDomain.SalesmanArea = salesmanArea;
            salesmanDomain.SalesmanLevel = level;
            salesmanDomain.LeaderId = leader == null ? 0 : leader.ID;

            return validationResults.Count == 0;
        }

        /// <summary>
        /// CHeck if its Sales Manager
        /// </summary>
        /// <param name="jobPosition"></param>
        /// <returns></returns>
        private bool IsManager(JobPosition jobPosition)
        {
            // if JobPosition is manager, set salesmanlevel with blank
            List<JobPosition> ManagerPositions = GetManagerJobPositions();

            JobPosition jobPos = ManagerPositions.Where(e => e.ID == jobPosition.ID).SingleOrDefault();

            return jobPos != null;
        }

        /// <summary>
        /// check if its branch manager
        /// </summary>
        /// <param name="jobPosition"></param>
        /// <returns></returns>
        private bool IsBranchManager(JobPosition jobPosition)
        {
            // if JobPosition is manager, set salesmanlevel with blank
            List<JobPosition> ManagerPositions = GetBranchManagerJobPositions();

            JobPosition jobPos = ManagerPositions.Where(e => e.ID == jobPosition.ID).SingleOrDefault();

            return jobPos != null;
        }

        /// <summary>
        /// Validate KTP Number
        /// </summary>
        /// <param name="employeeSalesParam"></param>
        /// <param name="validationResults"></param>
        /// <param name="salesmanDomain"></param>
        /// <returns></returns>
        private SalesmanHeader ValidateKTP(EmployeeSalesParameterDto employeeSalesParam, List<DNetValidationResult> validationResults, SalesmanHeader salesmanDomain, bool isUpdate)
        {
            // validate no KTP
            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(SalesmanProfile), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            criterias.opAnd(new Criteria(typeof(SalesmanProfile), "ProfileHeader.ID", MatchType.Exact, 29));
            criterias.opAnd(new Criteria(typeof(SalesmanProfile), "ProfileValue", MatchType.Exact, employeeSalesParam.NoKTP.Trim()));

            ArrayList arrKTPExist = _salesmanProfileMapper.RetrieveByCriteria(criterias);
            SalesmanProfile objSalesmanProfile = null;
            SalesmanHeader objSalesmanExist = null;

            if (string.IsNullOrEmpty(employeeSalesParam.RefSalesmanCode))
            {
                if (arrKTPExist.Count > 0)
                {
                    if (employeeSalesParam.ID > 0)
                    {
                        criterias.opAnd(new Criteria(typeof(SalesmanProfile), "SalesmanHeader.ID", MatchType.Exact, employeeSalesParam.ID));
                        ArrayList arrSalesmanHeadeExist = _salesmanProfileMapper.RetrieveByCriteria(criterias);
                        if (arrSalesmanHeadeExist.Count > 0)
                        {
                            objSalesmanProfile = arrSalesmanHeadeExist[0] as SalesmanProfile;
                        }
                        else
                        {
                            if (!isUpdate)
                            {
                                validationResults.Add(new DNetValidationResult("Salesman Header dengan No KTP " + employeeSalesParam.NoKTP + " tidak ditemukan"));

                            }
                        }
                    }
                    else
                    {
                        objSalesmanProfile = arrKTPExist[0] as SalesmanProfile;
                    }
                    if (objSalesmanProfile != null)
                    {
                        objSalesmanExist = objSalesmanProfile.SalesmanHeader;
                        if (objSalesmanExist.SalesIndicator == 1)
                        {
                            // if current salesman status notactive, need refsalesman code to update the data
                            if (objSalesmanExist.Status == _enumBL.GetByCategoryAndCode("EnumSalesmanStatus.SalesmanStatus", "Tidak_Aktif").ValueId.ToString())
                            {
                                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSalesmanNotActive, employeeSalesParam.NoKTP, objSalesmanExist.Name)));
                                validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, MessageResource.ErrorMsgRefSalesmanCodeRequired));
                            }
                            else
                            {
                                // if update using diffrence KTP
                                if (isUpdate)
                                {
                                    if (salesmanDomain.ID != objSalesmanExist.ID)
                                    {
                                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgKTPAlreadyExist, employeeSalesParam.NoKTP, objSalesmanExist.Name)));
                                    }
                                    else
                                    {
                                        ValidateNameDatePlaceofBirth(employeeSalesParam, validationResults, salesmanDomain);
                                    }
                                }
                                // if create and no KTP exist
                                if (!isUpdate)
                                {
                                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgKTPAlreadyExist, employeeSalesParam.NoKTP, objSalesmanExist.Name)));
                                }
                            }
                        }
                        else
                        {
                            // validate name, date and place of birth
                            ValidateNameDatePlaceofBirth(employeeSalesParam, validationResults, salesmanDomain);
                        }
                    }
                    else
                    {
                        if (isUpdate)
                        {
                            validationResults.Add(new DNetValidationResult("No KTP tidak boleh di update"));
                        }
                    }
                }
                else
                {
                    if (isUpdate)
                    {
                        // validate name, date and place of birth
                        ValidateNameDatePlaceofBirth(employeeSalesParam, validationResults, salesmanDomain);
                    }
                }
            }
            // if refsalesmancode exist, it means create new salesman by reactivate
            else
            {
                if (!isUpdate)
                {
                    SalesmanHeader refSalesman = null;
                    // get sales by refsalescode
                    CriteriaComposite criteriaref = new CriteriaComposite(new Criteria(typeof(SalesmanHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    criteriaref.opAnd(new Criteria(typeof(SalesmanHeader), "SalesmanCode", MatchType.Exact, employeeSalesParam.RefSalesmanCode));

                    var refSalesmanData = _salesmanHeaderMapper.RetrieveByCriteria(criteriaref);
                    if (refSalesmanData.Count > 0)
                    {
                        // cast the object
                        refSalesman = refSalesmanData[0] as SalesmanHeader;

                        if (refSalesman != null)
                        {
                            // ref salesman status should be non active
                            if (refSalesman.Status != _enumBL.GetByCategoryAndCode("EnumSalesmanStatus.SalesmanStatus", "Tidak_Aktif").ValueId.ToString())
                            {
                                validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, MessageResource.ErrorMsgRefSalesmanActive));
                            }
                            else
                            {
                                if (arrKTPExist.Count > 0)
                                {
                                    objSalesmanProfile = arrKTPExist[0] as SalesmanProfile;
                                    objSalesmanExist = objSalesmanProfile.SalesmanHeader;

                                    if (objSalesmanExist.SalesIndicator == 1)
                                    {
                                        if (refSalesman.SalesmanProfiles.Count > 0)
                                        {
                                            SalesmanProfile refSalesmanprofile = null;
                                            IEnumerable<SalesmanProfile> profiles = refSalesman.SalesmanProfiles.Cast<SalesmanProfile>().ToList();
                                            refSalesmanprofile = profiles.Where(e => e.ProfileHeader.ID == 29).FirstOrDefault();

                                            if (refSalesmanprofile != null)
                                            {
                                                // if NoKTP on param not same as noKTP on refSalesman
                                                if (employeeSalesParam.NoKTP.Trim() != refSalesmanprofile.ProfileValue)
                                                {
                                                    validationResults.Add(new DNetValidationResult(string.Format("No KTP {0} tidak sesuai dengan No KTP Reff Salesman Code {1}", employeeSalesParam.NoKTP, objSalesmanExist.SalesmanCode)));
                                                }
                                                else
                                                {
                                                    // validate name, date and place of birth
                                                    ValidateNameDatePlaceofBirth(employeeSalesParam, validationResults, refSalesman);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ValidateNameDatePlaceofBirth(employeeSalesParam, validationResults, refSalesman);
                                    }
                                }
                                else
                                {
                                    // validate name, date and place of birth
                                    ValidateNameDatePlaceofBirth(employeeSalesParam, validationResults, refSalesman);
                                }
                            }
                        }
                        else
                        {
                            validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.SalesmanHeader, employeeSalesParam.RefSalesmanCode)));
                        }
                    }
                }
            }

            #region old method comment
            //if (arrKTPExist.Count > 0)
            //{
            //    SalesmanProfile objSalesmanProfile = arrKTPExist[0] as SalesmanProfile;
            //    SalesmanHeader objSalesmanExist = objSalesmanProfile.SalesmanHeader;                              

            //    if (objSalesmanExist.RowStatus == (short)(DBRowStatus.Active))
            //    {
            //    if (objSalesmanExist.Status == _enumBL.GetByCategoryAndCode("EnumSalesmanStatus.SalesmanStatus", "Aktif").ValueId.ToString())
            //    {
            //        // if update using diffrence KTP
            //        if (objSalesmanProfile.ProfileValue != employeeSalesParam.NoKTP)
            //            validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgKTPAlreadyExist, employeeSalesParam.NoKTP, objSalesmanExist.Name)));
            //    }
            //    else
            //    {
            //        // validate refSalesmanCode
            //        if (!string.IsNullOrEmpty(employeeSalesParam.RefSalesmanCode))
            //        {
            //            if (salesmanDomain != null)
            //            {
            //                if (salesmanDomain.SalesmanCode.Trim().ToUpper() != objSalesmanExist.SalesmanCode.Trim().ToUpper())
            //                {
            //                    validationResults.Add(new DNetValidationResult(ErrorCode.DataOptionNotMatch, MessageResource.ErrorMsgSalesmanCodeDifferentWithExistingSalesman));
            //                }
            //            }
            //            else
            //            {
            //                validationResults.Add(new DNetValidationResult(ErrorCode.DataOptionNotMatch, MessageResource.ErrorMsgSalesmanCodeDifferentWithExistingSalesman));
            //            }

            //        }
            //        else
            //        {
            //            validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, MessageResource.ErrorMsgRefSalesmanCodeRequired));
            //        }

            //    }

            //    }
            //}
            //else
            //{

            //}
            #endregion

            return salesmanDomain;
        }

        private void ValidateNameDatePlaceofBirth(EmployeeSalesParameterDto employeeSalesParam, List<DNetValidationResult> validationResults, SalesmanHeader salesmanDomain)
        {
            // validate name and date of birth
            if (IsValidNameandBirthDate(employeeSalesParam, salesmanDomain))
            {
                // validate place of birth
                //if (!IsValidBirthPlace(employeeSalesParam, salesmanDomain))
                //{
                //    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSalesmanPlaceofBirthNotSame, employeeSalesParam.PlaceOfBirth, salesmanDomain.PlaceOfBirth)));
                //}
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSalesmanNameandDateofBirthNotSame,
                    employeeSalesParam.Name, employeeSalesParam.DateOfBirth.ToString("yyyy-MM-dd"), salesmanDomain.Name, salesmanDomain.DateOfBirth.ToString("yyyy-MM-dd"))));
            }
        }

        /// <summary>
        /// check if name and date of birth same
        /// </summary>
        /// <param name="employeeSalesParam"></param>
        /// <param name="salesmanDomain"></param>
        /// <returns>bool status</returns>
        private bool IsValidNameandBirthDate(EmployeeSalesParameterDto employeeSalesParam, SalesmanHeader salesmanDomain)
        {
            return ((employeeSalesParam.Name.ToLower() == salesmanDomain.Name.ToLower().Trim()) && (employeeSalesParam.DateOfBirth.ToString("yyyy-MM-dd") == salesmanDomain.DateOfBirth.ToString("yyyy-MM-dd")));
        }

        /// <summary>
        /// check if place of birth same
        /// </summary>
        /// <param name="employeeSalesParam"></param>
        /// <param name="salesmanDomain"></param>
        /// <returns>bool status</returns>
        private bool IsValidBirthPlace(EmployeeSalesParameterDto employeeSalesParam, SalesmanHeader salesmanDomain)
        {
            return (employeeSalesParam.PlaceOfBirth.ToLower() == salesmanDomain.PlaceOfBirth.ToLower());
        }

        /// <summary>
        /// Validate resignation
        /// </summary>
        /// <param name="resignType"></param>
        /// <param name="resignDate"></param>
        /// <param name="resignReason"></param>
        /// <param name="validationResults"></param>
        /// <param name="salesmanDomain"></param>
        private void ValidateResignation(int resignType, DateTime resignDate, string resignReason, List<DNetValidationResult> validationResults, ref SalesmanHeader salesmanDomain, bool isBranchManager = false, bool isSalesProcess = false)
        {
            if (resignType > 0 )
            {
                CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(SalesmanHeader), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                criterias.opAnd(new Criteria(typeof(SalesmanHeader), "Status", MatchType.Exact, 2));
                criterias.opAnd(new Criteria(typeof(SalesmanHeader), "LeaderId", MatchType.Exact, salesmanDomain.ID));

                var teamSalesman = _salesmanHeaderMapper.RetrieveByCriteria(criterias);
                if (resignDate == Constants.DATETIME_DEFAULT_VALUE || resignDate == DateTime.MinValue)
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ResignDate)));
                }
                else if (resignDate <= salesmanDomain.HireDate)
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgComparisonLessOrEqual, FieldResource.ResignDate, FieldResource.HireDate)));
                }
                else if (teamSalesman != null && teamSalesman.Count > 0 && !isBranchManager)
                {
                    validationResults.Add(new DNetValidationResult("Salesman ini masih menjadi Leader bagi Salesman Aktif! Pindahkan team Leader ini ke Salesman Lain Terlebih Dahulu!"));
                }
            }

            if (resignType > 0 && !isSalesProcess)
            {
                if (!_enumBL.IsExistByCategoryAndValue("EMP_RESIGN_TYPE", resignType.ToString()))
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ResignType)));
                }
                else
                {
                    if (string.IsNullOrEmpty(resignReason))
                    {
                        validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.ResignReason)));
                    }
                    else
                    {
                        salesmanDomain.ResignType = (short)resignType;
                        salesmanDomain.Status = _enumBL.GetByCategoryAndCode("EnumSalesmanStatus.SalesmanStatus", "Tidak_Aktif").ValueId.ToString();
                        salesmanDomain.ResignDate = resignDate;
                        salesmanDomain.ResignReason = resignReason;
                    }
                }
            }

            if (isSalesProcess && resignDate != Constants.DATETIME_DEFAULT_VALUE && resignDate != DateTime.MinValue)
            {
                CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(SalesmanHeader), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
                criterias.opAnd(new Criteria(typeof(SalesmanHeader), "Status", MatchType.Exact, 2));
                criterias.opAnd(new Criteria(typeof(SalesmanHeader), "LeaderId", MatchType.Exact, salesmanDomain.ID));

                var teamSalesman = _salesmanHeaderMapper.RetrieveByCriteria(criterias);
                if (resignDate <= salesmanDomain.HireDate)
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgComparisonLessOrEqual, FieldResource.ResignDate, FieldResource.HireDate)));
                }
                else if (teamSalesman != null && teamSalesman.Count > 0 && !isBranchManager)
                {
                    validationResults.Add(new DNetValidationResult("Salesman ini masih menjadi Leader bagi Salesman Aktif! Pindahkan team Leader ini ke Salesman Lain Terlebih Dahulu!"));
                }

                if (!_enumBL.IsExistByCategoryAndValue("EnumResignReason", resignType.ToString()))
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ResignReason)));
                }
                else
                {
                    StandardCodeDto ResignReasonMaster = _enumBL.GetByCategoryAndValue("EnumResignReason", resignType.ToString());
                    if (ResignReasonMaster.ValueCode == "LainLain" && string.IsNullOrEmpty(resignReason))
                    {
                        validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.ResignReasonDesc)));
                    }
                    else
                    {
                        salesmanDomain.ResignType = (short)resignType;
                        salesmanDomain.Status = _enumBL.GetByCategoryAndCode("EnumSalesmanStatus.SalesmanStatus", "Tidak_Aktif").ValueId.ToString();
                        salesmanDomain.ResignDate = resignDate;
                        salesmanDomain.ResignReasonType = Convert.ToInt16(resignType);
                        if (ResignReasonMaster.ValueCode == "LainLain")
                            salesmanDomain.ResignReason = resignReason;
                        else
                            salesmanDomain.ResignReason = ResignReasonMaster.ValueDesc;
                    }
                }
            }
        }

        /// <summary>
        /// Set object for insert
        /// </summary>
        /// <param name="employeePart"></param>
        /// <param name="validationResults"></param>
        /// <param name="salesmanHeader"></param>
        /// <returns></returns>
        private bool ValidateEmployeePart(EmployeePartParameterDto employeePart, List<DNetValidationResult> validationResults, ref SalesmanHeader salesmanHeader, bool isUpdate = false)
        {
            #region Validate Create or Update operation
            if (isUpdate)
            {
                salesmanHeader = (SalesmanHeader)_salesmanHeaderMapper.Retrieve(employeePart.ID);
                if (salesmanHeader == null)
                {
                    validationResults.Add(new DNetValidationResult(MessageResource.ErrorMsgDataUpdateNotAvailable));
                }
                else
                {
                    salesmanHeader.Name = employeePart.Name;
                    salesmanHeader.PlaceOfBirth = employeePart.PlaceOfBirth;
                    salesmanHeader.DateOfBirth = employeePart.DateOfBirth;
                    salesmanHeader.HireDate = employeePart.HireDate;
                    salesmanHeader.Address = employeePart.Address;

                    ValidateResignation(employeePart.ResignType, employeePart.ResignDate, employeePart.ResignReason, validationResults, ref salesmanHeader);
                }
            }
            else
            {
                salesmanHeader = new SalesmanHeader
                {
                    SalesmanCode = string.Empty,
                    Name = employeePart.Name,
                    PlaceOfBirth = employeePart.PlaceOfBirth,
                    DateOfBirth = employeePart.DateOfBirth,
                    HireDate = employeePart.HireDate,
                    Address = employeePart.Address,
                    ResignDate = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue.Value,
                    ResignReason = string.Empty,

                    // SalesIndicator SparePart(0)
                    SalesIndicator = 0,
                    // status Baru (1)
                    Status = "1",
                    // Belum Register(0)
                    RegisterStatus = "0",
                    // Belum Request (0)
                    IsRequestID = 0,
                    // set unknown(0) as default
                    SalesmanArea = new SalesmanArea { ID = 0 },
                    // set default Lead Id
                    LeaderId = 0
                };

                if (this.ValidateDealerSystems() > 2)
                {
                    salesmanHeader.IsRequestID = 1;
                }
                else
                {
                    salesmanHeader.IsRequestID = 0;
                }
            }
            #endregion

            #region Validate Gender
            if (!_enumBL.IsExistByCategoryAndValue(".Gender", ((employeePart.Gender)).ToString())) { validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.Gender))); }
            salesmanHeader.Gender = employeePart.Gender;
            #endregion

            #region validate Province and City
            City city = new City();
            city = GetValidCityByProvince(employeePart.ProvinceCode, employeePart.CityCode, validationResults);

            if (city != null)
            {
                salesmanHeader.City = city.CityName.ToUpper();
            }
            #endregion

            #region Validate Dealer
            Dealer dealer = null;
            if (ValidationHelper.ValidateDealer(employeePart.DealerCode, validationResults, this.DealerCode, ref dealer))
            {
                salesmanHeader.Dealer = dealer;
            }
            #endregion

            #region Validate Dealer Branch
            DealerBranch dealerBranch = null;
            if (ValidationHelper.ValidateDealerBranch(this.DealerCode, validationResults, employeePart.DealerBranchCode, ref dealerBranch))
            {
                salesmanHeader.DealerBranch = dealerBranch == null ? salesmanHeader.DealerBranch : dealerBranch;
            }
            #endregion

            #region Validate MarriedStatus
            if (!_enumBL.IsExistByCategoryAndValue(".MarriedStatus", ((employeePart.MarriedStatus)).ToString())) { validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.MarriedStatus))); }
            salesmanHeader.MarriedStatus = employeePart.MarriedStatus.ToString();
            #endregion

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Get Valid Salesman Additional Info for EmployeeParts
        /// </summary>
        /// <param name="employeePart"></param>
        /// <param name="dealer"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidateSalesmanAdditionalInfo(EmployeePartParameterDto employeePart, SalesmanHeader salesmanHeader, List<DNetValidationResult> validationResults, ref SalesmanAdditionalInfo additionalInfo, bool isUpdate = false)
        {
            // declare
            int level = 0;
            SalesmanCategoryLevel categoryLevel = null;
            List<DNetValidationResult> levelValidationResults = new List<DNetValidationResult>();

            // get organization
            V_SparePartOrganization orgView = GetSparepartOrganization(salesmanHeader.Dealer.ID, employeePart.SalesmanParentCategoryLevel, employeePart.SalesmanCategoryLevel, validationResults);
            if (validationResults.Any())
            {
                return false;
            }
            else
            {
                // get category level object
                categoryLevel = (SalesmanCategoryLevel)_salesmanCategoryLevelMapper.Retrieve(orgView.SalesmanCategoryLevelID);

                //12 = sales penentuan level berdasarkan lama kerja dari tgl masuk, HireDate
                if (orgView.SalesmanCategoryLevelID == 12)
                {
                    TimeSpan spanDate = employeePart.HireDate - DateTime.Now;
                    int totalYear = (int)(spanDate.Days / 365.25);

                    level = GetSalesmanLevelByYear(totalYear);
                }
                else
                {
                    level = 99;
                }
            }

            if (!_enumBL.IsExist(".Religion", ((employeePart.ReligionID)).ToString()))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.Religion)));
            }

            if (isUpdate && salesmanHeader.SalesmanAdditionalInfo.Count > 0)
            {
                additionalInfo = salesmanHeader.SalesmanAdditionalInfo[0] as SalesmanAdditionalInfo;
                additionalInfo.SalesmanCategoryLevel = categoryLevel;
                additionalInfo.SalesmanLevel = level;
                additionalInfo.Salary = employeePart.Salary;
                additionalInfo.ReligionID = employeePart.ReligionID;
            }
            else
            {
                additionalInfo = new SalesmanAdditionalInfo
                {
                    SalesmanHeader_Ref = null,
                    SalesmanCategoryLevel = categoryLevel,
                    SalesmanLevel = level,
                    Salary = employeePart.Salary,
                    ReligionID = employeePart.ReligionID,
                };
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Get valid salesman profile
        /// </summary>
        /// <typeparam name="TParam"></typeparam>
        /// <param name="employee"></param>
        /// <param name="validationResults"></param>
        /// <param name="profileGroupCode"></param>
        /// <param name="salesmanDomain"></param>
        /// <param name="listOfSalesmanProfile"></param>
        /// <returns></returns>
        private bool ValidateSalesmanProfile<TParam>(TParam employee, List<DNetValidationResult> validationResults, string profileGroupCode, SalesmanHeader salesmanDomain, List<SalesmanProfile> listOfSalesmanProfile)
        {
            // get profile group
            ProfileGroup profileGroup = GetProfileGroup(profileGroupCode);
            if (profileGroup != null)
            {
                List<StandardCodeDto> enumMandatoryMode = _enumBL.GetByCategory("EnumMandatory.MandatoryMode");
                short mandatoryFlag = (short)enumMandatoryMode.Where(s => s.ValueCode == "Benar").SingleOrDefault().ValueId;

                List<ProfileHeaderToGroup> listOfProfileHeaderToGroup = profileGroup.ProfileHeaderToGroups.Cast<ProfileHeaderToGroup>().ToList();

                foreach (var headerToGroup in listOfProfileHeaderToGroup)
                {
                    dynamic value;

                    var prop = typeof(TParam).GetProperty(headerToGroup.ProfileHeader.Code, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    if (prop != null)
                    {
                        value = prop.GetValue(employee, null);

                        string strValue = Convert.ToString(value);

                        if (headerToGroup.ProfileHeader.Mandatory == mandatoryFlag && (strValue == null || string.IsNullOrWhiteSpace(strValue)))
                        {
                            validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.ResourceManager.GetString(headerToGroup.ProfileHeader.Code))));
                            continue;
                        }

                        if (headerToGroup.ProfileHeader.Code.Equals("NOKTP", StringComparison.OrdinalIgnoreCase))
                        {
                            if (strValue.Trim().Length != 16)
                            {
                                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ResourceManager.GetString(headerToGroup.ProfileHeader.Code))));
                            }
                        }
                        else if (headerToGroup.ProfileHeader.Code.Equals("NOTELP", StringComparison.OrdinalIgnoreCase))
                        {
                            if (strValue.Length < 6 || strValue.Substring(0, 2) == "00")
                            {
                                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ResourceManager.GetString(headerToGroup.ProfileHeader.Code))));
                            }
                        }
                        else if (headerToGroup.ProfileHeader.Code.Equals("NO_HP", StringComparison.OrdinalIgnoreCase))
                        {
                            if (!string.IsNullOrEmpty(strValue))
                            {
                                if (!Utils.IsNoHPValid(strValue) || strValue.Trim().Length > 15)
                                {
                                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.ResourceManager.GetString(headerToGroup.ProfileHeader.Code))));
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(strValue))
                        {
                            // check the posible strValue from profileDetail
                            if (headerToGroup.ProfileHeader.ControlType == 2)
                            {
                                ArrayList arrayOfProfileDetail = _profileDetailMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(ProfileDetail), "ProfileHeader.ID", headerToGroup.ProfileHeader.ID.ToString()));

                                if (arrayOfProfileDetail.Count > 0)
                                {
                                    List<ProfileDetail> listOfProfileDetail = arrayOfProfileDetail.Cast<ProfileDetail>().ToList();

                                    List<ProfileDetail> listOfMatchedProfileDetail = new List<ProfileDetail>();

                                    // for kategori tim use ID instead
                                    if (prop.Name.Equals("Kategori_Tim"))
                                    {
                                        listOfMatchedProfileDetail = listOfProfileDetail.Where(p => p.ID == value).ToList();
                                        if (listOfMatchedProfileDetail.Any())
                                        {
                                            // store the code instead of the ID
                                            strValue = (listOfMatchedProfileDetail.First() as ProfileDetail).Code;
                                        }
                                    }
                                    else
                                        listOfMatchedProfileDetail = listOfProfileDetail.Where(p => p.Code.Trim().ToUpper() == strValue.Trim().ToUpper()).ToList();

                                    if (listOfMatchedProfileDetail.Count == 0)
                                    {
                                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgInvalidProfileHeaderValue, headerToGroup.ProfileHeader.Code, strValue)));
                                    }
                                }
                                else
                                {
                                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgInvalidProfileHeaderValue, headerToGroup.ProfileHeader.Code, strValue)));
                                }
                            }
                        }

                        SalesmanProfile profile = GetValidSalesmanProfile(salesmanDomain.ID, headerToGroup.ProfileGroup.ID, headerToGroup.ProfileHeader.ID, strValue);
                        profile.SalesmanHeader = salesmanDomain;
                        listOfSalesmanProfile.Add(profile);
                    }
                }
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validated salesman image file
        /// </summary>
        /// <param name="objCreate"></param>        
        /// <param name="validationResults"></param>
        /// <param name="domainSalesman"></param>
        /// <returns></returns>
        private bool ValidateSalesmanImage(AttachmentParameterDto image, List<DNetValidationResult> validationResults, SalesmanHeader domainSalesman)
        {
            byte[] fileBytes = null;

            // validate the Photo file if exists
            if (image != null && !string.IsNullOrEmpty(image.FileName))
            {
                validationResults.AddRange(FileUtility.ValidateEvidenceOrIdentityFile(image, _mapper, out fileBytes, FieldResource.ImageAttachment));
                if (validationResults.Any())
                {
                    return false;
                }
                else
                {
                    domainSalesman.Image = fileBytes;
                }
            }

            return true;
        }

        /// <summary>
        /// Get the existing salesman object
        /// </summary>
        /// <param name="paramDto"></param>
        /// <param name="validationResults"></param>
        /// <param name="salesmanOnDb"></param>
        /// <returns></returns>
        private bool GetCurrentSalesmanObject(EmployeeSalesParameterDto paramDto, List<DNetValidationResult> validationResults, ref SalesmanHeader salesmanOnDb)
        {
            salesmanOnDb = (SalesmanHeader)_salesmanHeaderMapper.Retrieve(paramDto.ID);
            if (salesmanOnDb == null)
            {
                validationResults.Add(new DNetValidationResult(MessageResource.ErrorMsgDataUpdateNotAvailable));
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check the spk match faktur status
        /// </summary>
        /// <returns></returns>
        private int ValidateDealerSystems()
        {
            int dealerID = GetCurrentDealerID();
            if (dealerID == 0)
                return 1;

            var dealerSystems = _dealerSystemMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(DealerSystems), "Dealer.ID", dealerID));
            if (dealerSystems.Count > 0)
            {
                var dealer = (dealerSystems[0] as DealerSystems);
                return dealer.SystemID;
            }
            else
                return 1;
        }

        /// <summary>
        /// Get current active dealer id
        /// </summary>
        /// <returns></returns>
        private int GetCurrentDealerID()
        {
            // get dealer ID
            var dealers = _dealerMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(Dealer), "DealerCode", this.DealerCode));
            if (dealers.Count > 0)
                return (dealers[0] as Dealer).ID;
            else
                return 0;
        }

        #endregion

        #region Common Methods
        /// <summary>
        /// Get Valid Leader by SalesmanCode
        /// </summary>
        /// <param name="leaderCode"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private SalesmanHeader GetValidLeader(string leaderCode, string dealerCode, JobPosition jobPosition, bool isManager, bool isBranchManager, List<DNetValidationResult> validationResults)
        {
            // manager no need leader validation
            if (isBranchManager) { return null; }

            // init
            SalesmanHeader leader = null;

            // except manager the leader code is mandatory
            if (string.IsNullOrEmpty(leaderCode))
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.Leader)));
            }
            else
            {
                // validate salesman header
                if (ValidationHelper.ValidateSalesmanHeaderEmployeeSales(leaderCode, dealerCode, validationResults, ref leader))
                {
                    if (leader.Status.Equals(_enumBL.GetByCategoryAndCode("EnumSalesmanStatus.SalesmanStatus", "Tidak_Aktif").ValueId.ToString()))
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgLeaderCodeInactive, leaderCode)));
                    }

                    if (jobPosition != null)
                    {
                        // salesman and salesman counter treated as same level 
                        int currentPosition = jobPosition.ID;
                        if (currentPosition == 2)
                            currentPosition = 3;

                        if (currentPosition + 2 < leader.JobPosition.ID || currentPosition >= leader.JobPosition.ID)
                            validationResults.Add(new DNetValidationResult(MessageResource.ErrorMsgLeaderLevel));
                    }
                }
            }
            return leader;
        }

        /// <summary>
        /// Get profile group
        /// </summary>
        /// <param name="profileGroupCode"></param>
        /// <returns></returns>
        private ProfileGroup GetProfileGroup(string profileGroupCode)
        {
            short activeDBRowStatus = (short)(_enumBL.GetByCategoryAndCode("DBRowStatus", "Active").ValueId);
            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(ProfileGroup), ROW_STATUS, MatchType.Exact, activeDBRowStatus));
            criterias.opAnd(new Criteria(typeof(ProfileGroup), CODE, MatchType.Exact, profileGroupCode));
            ArrayList listOfProfileGroup = _profileGroupMapper.RetrieveByCriteria(criterias);

            // get profile group if any otherwise set to null
            ProfileGroup profileGroup = listOfProfileGroup.Count > 0 ? ((ProfileGroup)(listOfProfileGroup[0])) : null;
            return profileGroup;
        }

        /// <summary>
        /// GetSalesmanProfileByProfileHeader
        /// </summary>
        /// <param name="salesmanHeaderId"></param>
        /// <param name="headerID"></param>
        /// <returns></returns>
        private SalesmanProfile GetSalesmanProfileByProfileHeader(int salesmanHeaderId, int profGroupID, int headerID)
        {
            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(SalesmanProfile), "RowStatus", MatchType.Exact, ((short)(_enumBL.GetByCategoryAndCode("DBRowStatus", "Active").ValueId))));
            criterias.opAnd(new Criteria(typeof(SalesmanProfile), "SalesmanHeader.ID", MatchType.Exact, salesmanHeaderId));
            criterias.opAnd(new Criteria(typeof(SalesmanProfile), "ProfileHeader.ID", MatchType.Exact, headerID));
            criterias.opAnd(new Criteria(typeof(SalesmanProfile), "ProfileGroup.ID", MatchType.Exact, profGroupID));

            var salesmanProfiles = _salesmanProfileMapper.RetrieveByCriteria(criterias);
            SalesmanProfile salesmanProfile = salesmanProfiles.Count > 0 ? (SalesmanProfile)salesmanProfiles[0] : null;
            return salesmanProfile;
        }

        /// <summary>
        /// Get valid SalesmanProfile
        /// </summary>
        /// <param name="salesmanHeaderID"></param>
        /// <param name="profileGroupID"></param>
        /// <param name="profileHeaderID"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private SalesmanProfile GetValidSalesmanProfile(int salesmanHeaderID, int profileGroupID, int profileHeaderID, string value)
        {
            SalesmanProfile salesmanProfile = GetSalesmanProfileByProfileHeader(salesmanHeaderID, profileGroupID, profileHeaderID);
            if (salesmanProfile != null)
            {
                salesmanProfile.ProfileValue = value;
                return salesmanProfile;
            }
            else
            {
                var objProHeader = _profileHeaderMapper.Retrieve(profileHeaderID);
                var objProGroup = _profileGroupMapper.Retrieve(profileGroupID);

                SalesmanProfile newProfile = new SalesmanProfile()
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
        /// Get valid salesman level
        /// </summary>
        /// <param name="salesmanLevelID"></param>
        /// <param name="validationResults"></param>
        /// <param name="isManager"></param>
        /// <returns></returns>
        private SalesmanLevel GetValidSalesmanLevel(int salesmanLevelID, bool isManager, bool isBranchManager, List<DNetValidationResult> validationResults)
        {
            SalesmanLevel salesmanLevel = null;

            // non manager should provide salesman level
            if ((!isManager || !isBranchManager) && salesmanLevelID == 0)
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.DataRequiredField, string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.SalesmanLevel)));
                return salesmanLevel;
            }

            CriteriaComposite criterias = Helper.GenerateCriteria(typeof(SalesmanLevel), "ID", salesmanLevelID.ToString());
            ArrayList listOfSalesmanLevel = _salesmanLevelMapper.RetrieveByCriteria(criterias);
            if (listOfSalesmanLevel.Count > 0)
            {
                salesmanLevel = (SalesmanLevel)listOfSalesmanLevel[0];
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSalesmanLevelInvalid, FieldResource.SalesmanLevel)));
            }

            return salesmanLevel;
        }

        /// <summary>
        /// Get valid salesman area
        /// </summary>
        /// <param name="salesmanAreaCode"></param>
        /// <returns></returns>
        private SalesmanArea GetValidSalesmanArea(string salesmanAreaCode, List<DNetValidationResult> validationResults)
        {
            CriteriaComposite criterias = Helper.GenerateCriteria(typeof(SalesmanArea), "AreaCode", salesmanAreaCode);
            ArrayList listOfSalesmanArea = _salesmanAreaMapper.RetrieveByCriteria(criterias);
            SalesmanArea salesmanArea = null;
            if (listOfSalesmanArea.Count > 0)
            {
                salesmanArea = (SalesmanArea)listOfSalesmanArea[0];
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSalesmanAreaInvalid, FieldResource.SalesmanArea)));
            }

            return salesmanArea;
        }

        /// <summary>
        /// Get valid JobPosition
        /// </summary>
        /// <param name="jobPositionMenu"></param>
        /// <param name="jobPositionCode"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private JobPosition GetValidJobPositionByToMenu(int jobPositionMenu, string jobPositionCode, List<DNetValidationResult> validationResults)
        {
            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(JobPositionToMenu), ROW_STATUS, MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(JobPositionToMenu), "JobPosition.Code", MatchType.Exact, jobPositionCode));
            criterias.opAnd(new Criteria(typeof(JobPositionToMenu), "JobPositionMenu.ID", MatchType.Exact, jobPositionMenu));

            ArrayList listOfJobPositionToMenu = _jobPossitionToMenuMapper.RetrieveByCriteria(criterias);
            if (listOfJobPositionToMenu.Count > 0)
            {
                return ((JobPositionToMenu)listOfJobPositionToMenu[0]).JobPosition;
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgJobPositionToMenuInvalid, FieldResource.JobPosition)));
                return null;
            }
        }

        /// <summary>
        /// Get JobPosition list by manager config
        /// </summary>
        /// <returns></returns>
        private List<JobPosition> GetManagerJobPositions()
        {
            List<JobPosition> results = new List<JobPosition>();
            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(JobPosition), ROW_STATUS, MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(JobPosition), "Code", MatchType.InSet, "('" + AppConfigs.GetString("SManCode") + "')"));
            results = _jobPositionMapper.RetrieveByCriteria(criterias).Cast<JobPosition>().ToList();
            return results;
        }

        /// <summary>
        /// Get JobPosition list by branch manager config
        /// </summary>
        /// <returns></returns>
        private List<JobPosition> GetBranchManagerJobPositions()
        {
            List<JobPosition> results = new List<JobPosition>();
            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(JobPosition), ROW_STATUS, MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(JobPosition), "Code", MatchType.InSet, "('" + AppConfigs.GetString("BManCode") + "')"));
            results = _jobPositionMapper.RetrieveByCriteria(criterias).Cast<JobPosition>().ToList();
            return results;
        }

        /// <summary>
        /// Get valid city
        /// </summary>
        /// <param name="cityCode"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private City GetValidCity(string cityCode, List<DNetValidationResult> validationResults)
        {
            City city = null;
            if (!ValidationHelper.ValidateCity(cityCode, validationResults, ref city, false))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgCityInvalid, cityCode)));
                return null;
            }

            return city;
        }

        /// <summary>
        /// validation for City based on province
        /// </summary>
        /// <param name="provinceCode"></param>
        /// <param name="cityCode"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private City GetValidCityByProvince(string provinceCode, string cityCode, List<DNetValidationResult> validationResults)
        {
            ProvinceBL provBL = new ProvinceBL(_mapper);
            City city = null;

            // get prov
            Province prov = provBL.GetProvinceByCode(provinceCode);
            if (prov == null)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgPropinsiInvalid, provinceCode)));
            }
            else
            {
                CityBL cityBL = new CityBL(_mapper);
                city = cityBL.GetCityByProvinceCityCode(prov.ID, cityCode);
                if (city == null)
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgPropinsiCityInvalid, cityCode, prov.ProvinceName)));
                }
            }

            return city;
        }

        /// <summary>
        /// Get Salesman Level
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private int GetSalesmanLevelByYear(int year)
        {
            int result = 0;
            List<StandardCodeDto> enumList = _enumBL.GetByCategory(".Level");

            if (year < 5)
                return result = enumList.Where(e => e.ValueCode == "Junior").FirstOrDefault().ValueId;
            if (year < 10)
                return result = enumList.Where(e => e.ValueCode == "Senior").FirstOrDefault().ValueId;
            if (year >= 10)
                return result = enumList.Where(e => e.ValueCode == "Top").FirstOrDefault().ValueId;

            return result;
        }

        /// <summary>
        /// Validate category level and parent category level
        /// </summary>
        /// <param name="dealerID"></param>
        /// <param name="parentCategoryLevelID"></param>
        /// <param name="categoryLevelID"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private V_SparePartOrganization GetSparepartOrganization(int dealerID, int parentCategoryLevelID, int categoryLevelID, List<DNetValidationResult> validationResults)
        {
            // get the organization list
            List<V_SparePartOrganization> masterOrganizationList = GetSparepartOrganizationList(dealerID);
            // get parent category level list
            List<V_SparePartOrganization> parentOrganizationList = masterOrganizationList.Where(e => e.LevelNumber == 1).ToList();
            // get category level list
            List<V_SparePartOrganization> organizationList = masterOrganizationList.Where(e => e.LevelNumber == 2).ToList();

            // validate parent category level
            var parentOrganization = parentOrganizationList.Where(e => e.SalesmanCategoryLevelID == parentCategoryLevelID).FirstOrDefault();
            if (parentOrganization == null)
            {
                validationResults.Add(new DNetValidationResult(FieldResource.ParentCategoryLevel + " " + string.Format(MessageResource.ErrorMsgDataInvalid, parentCategoryLevelID)));
                return null;
            }

            // validate category level            
            var organization = organizationList.Where(e => e.SalesmanCategoryLevelID == categoryLevelID && e.ParentID == parentOrganization.SalesmanCategoryLevelID).FirstOrDefault();
            if (organization == null)
            {
                validationResults.Add(new DNetValidationResult(FieldResource.CategoryLevel + " " + string.Format(MessageResource.ErrorMsgDataInvalid, categoryLevelID)));
                return null;
            }

            return organization;
        }

        /// <summary>
        /// Get Organization List for Level and Position Sparerpart 
        /// </summary>
        /// <param name="dealerId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        private List<V_SparePartOrganization> GetSparepartOrganizationList(int dealerId)
        {
            // get dealer additional
            DealerAdditional dealerAdditional = new DealerAdditional();
            CriteriaComposite dealerAdditionalCriterias = new CriteriaComposite(new Criteria(typeof(DealerAdditional), "RowStatus", MatchType.Exact, ((short)(DBRowStatus.Active))));
            dealerAdditionalCriterias.opAnd(new Criteria(typeof(DealerAdditional), "Dealer.ID", MatchType.Exact, dealerId));
            var dealerAddData = _dealerAdditionalMapper.RetrieveByCriteria(dealerAdditionalCriterias);
            if (dealerAddData.Count > 0)
            {
                var list = dealerAddData.Cast<DealerAdditional>().ToList();
                dealerAdditional = list.FirstOrDefault();
            }

            // get organization list
            List<V_SparePartOrganization> result = new List<V_SparePartOrganization>();
            string grade = string.IsNullOrEmpty(dealerAdditional.SparePartGrade) ? "A" : dealerAdditional.SparePartGrade;
            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(V_SparePartOrganization), "Grade", MatchType.Exact, grade));
            criterias.opAnd(new Criteria(typeof(V_SparePartOrganization), "LevelNumber", MatchType.InSet, "(1,2)"));
            var data = _v_SparePartOrganizationMapper.RetrieveByCriteria(criterias);
            if (data.Count > 0)
            {
                result = data.Cast<V_SparePartOrganization>().ToList();
            }

            return result;
        }

        /// <summary>
        /// ged blank ID in salesmanlevel
        /// </summary>
        /// <returns></returns>
        private SalesmanLevel GetSalesmanLevelBlank()
        {
            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(SalesmanLevel), ROW_STATUS, MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(SalesmanLevel), "Description", MatchType.Exact, "Senior"));

            ArrayList listOfSalesmanLevel = _salesmanLevelMapper.RetrieveByCriteria(criterias);
            SalesmanLevel salesmanLevel = listOfSalesmanLevel.Count > 0 ? ((SalesmanLevel)(listOfSalesmanLevel[0])) : null;
            return salesmanLevel;
        }

        private string ValidatePlaceOfBirth(EmployeeSalesParameterDto employeeSalesParam, List<DNetValidationResult> validationResults)
        {
            string cityName = employeeSalesParam.PlaceOfBirth;
            City city = new City();
            List<StandardCodeDto> ListStdCodeIsOtherCity = _enumBL.GetByCategory("SalesmanHeader.IsOtherCity");
            List<StandardCodeDto> validateIsOtherCity = ListStdCodeIsOtherCity.Where(s => s.ValueId == employeeSalesParam.IsOtherCity).ToList();
            string isOtherCity = string.Empty;
            isOtherCity = validateIsOtherCity.Count == 0 ? "" : validateIsOtherCity[0].ValueCode;

            if (string.IsNullOrEmpty(isOtherCity))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMessageDataNotFoundWithColumn, FieldResource.IsOtherCity, "value " + employeeSalesParam.IsOtherCity.ToString())));
            }
            else
            {
                if (isOtherCity.ToLower().Trim() == "no")
                {
                    city = GetValidCity(cityName, validationResults);
                    if (city != null)
                    {
                        cityName = city.CityName;
                    }
                }
            }

            return cityName;
        }
        #endregion
    }
}

