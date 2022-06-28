#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : PDI business logic class
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
using KTB.DNet.Interface.Model.Parameters.Services;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class PDIBL : AbstractBusinessLogic, IPDIBL
    {
        #region Variables
        private readonly IMapper _pdiMapper;
        private readonly IMapper _chassisMaster;
        private readonly IMapper _vehicleColor;
        private readonly IMapper _vehicleType;
        private readonly IMapper _productCategory;
        private readonly AutoMapper.IMapper _mapper;
        private StandardCodeBL _enumBL;
        private AppConfigBL _appConfigBL;
        private readonly IMapper _wlogMapper;
        private bool isHaveRevitionFakture = false;

        #endregion

        #region Constructor
        public PDIBL()
        {
            _pdiMapper = MapperFactory.GetInstance().GetMapper(typeof(PDI).ToString());
            _chassisMaster = MapperFactory.GetInstance().GetMapper(typeof(ChassisMaster).ToString());
            _vehicleColor = MapperFactory.GetInstance().GetMapper(typeof(VechileColor).ToString());
            _vehicleType = MapperFactory.GetInstance().GetMapper(typeof(VechileType).ToString());
            _productCategory = MapperFactory.GetInstance().GetMapper(typeof(ProductCategory).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _enumBL = new StandardCodeBL(_mapper);
            _appConfigBL = new AppConfigBL(_mapper);
            _wlogMapper = MapperFactory.GetInstance().GetMapper(typeof(WsLog).ToString());

        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get PDI by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<PDIDto>> Read(PDIFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(PDI), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<PDIDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(PDI), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(PDI), filterDto, sortColl);

                // get data
                var data = _pdiMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<PDI>().ToList();
                    var listData = list.Select(item => _mapper.Map<PDIDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(PDI), filterDto);
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
        /// Delete PDI by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<PDIDto> Delete(int id)
        {
            var result = new ResponseBase<PDIDto>();

            try
            {
                var pdi = (PDI)_pdiMapper.Retrieve(id);
                if (pdi != null)
                {
                    pdi.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _pdiMapper.Update(pdi, DNetUserName);
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
        /// Create a new PDI
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<PDIDto> Create(PDIParameterDto objCreate)
        {
            #region Declare
            var result = new ResponseBase<PDIDto>();
            var validationResults = new List<DNetValidationResult>();
            var isValid = true;
            ChassisMaster chassisMaster = null;
            Dealer dealer = null;
            DealerBranch dealerBranch = null;
            bool successconnect = false;
            string user = AppConfigs.GetString("User");
            string password = AppConfigs.GetString("Password");
            string webServer = AppConfigs.GetString("WebServer");
            UserImpersonater imp = new UserImpersonater(user, password, webServer);
            List<int> isAlreadyPDISameDealer = new List<int>();
            #endregion

            try
            {
                // validate pdi parameter values
                isValid = ValidatePDI(objCreate, validationResults, ref chassisMaster, ref dealer, ref dealerBranch, ref isAlreadyPDISameDealer);


                // insert if valid
                if (isValid)
                {
                    if (isAlreadyPDISameDealer.Count > 0 && isAlreadyPDISameDealer[0] == 1)
                    {
                        result.success = true;
                        result._id = isAlreadyPDISameDealer[1];
                    }
                    else
                    {
                        // create pdi object
                        var newPDI = _mapper.Map<PDI>(objCreate);
                        newPDI.Dealer = dealer;
                        newPDI.ChassisMaster = chassisMaster;
                        newPDI.ReleaseDate = DateTime.Now;
                        newPDI.CreatedTime = DateTime.Now;
                        newPDI.LastUpdateBy = DNetUserName;
                        newPDI.LastUpdateTime = DateTime.Now;
                        if (dealerBranch != null)
                            newPDI.DealerBranch = dealerBranch;

                        //Generate PDF
                        var filename = string.Empty;
                        FileStream fs = null;
                        bool isUpdate = false;
                        var parameter = new PDIGetFileParameter();
                        parameter.ChassisNumber = objCreate.ChassisNumber;
                        parameter.IsEncrypted = false;
                        List<ValidResult> _validationResults = new List<ValidResult>();

                        PDIValidation pdiValidation = new PDIValidation(AppConfigs.GetString("SAN"), AppConfigs.GetString("User"), AppConfigs.GetString("Password"), AppConfigs.GetString("WebServer"));
                        if (pdiValidation.GenerateCertificate(newPDI, ref isUpdate, ref filename, ref fs, _validationResults, parameter.IsEncrypted))
                        {
                            newPDI.FileName = filename;
                        }
                        else
                        {
                            foreach (var i in _validationResults)
                            {
                                validationResults.Add(new DNetValidationResult(i.ToString()));
                            }
                            isValid = false;
                        }

                        if (isValid)
                        {
                            if (isHaveRevitionFakture)
                            {
                                // remove pdi on db
                                var criteriaDelete = new CriteriaComposite(new Criteria(typeof(PDI), "RowStatus", MatchType.Exact, (int)DBRowStatus.Active));
                                criteriaDelete.opAnd(new Criteria(typeof(PDI), "ChassisMaster.ChassisNumber", MatchType.Exact, objCreate.ChassisNumber));

                                var data = _pdiMapper.RetrieveByCriteria(criteriaDelete);
                                if (data.Count > 0)
                                {
                                    foreach (var item in data)
                                    {
                                        try
                                        {
                                            var pdi = (PDI)item;
                                            pdi.RowStatus = (short)DBRowStatus.Deleted;
                                            _pdiMapper.Update(pdi, DNetUserName);
                                        }
                                        catch (Exception e)
                                        {
                                            ErrorMsgHelper.SqlException(result.messages, e.Message);
                                        }

                                    }
                                }

                            }

                            // insert a new pdi object
                            var success = (int)_pdiMapper.Insert(newPDI, DNetUserName);
                            result.success = success > 0;
                            try
                            {
                                if (success > 0)
                                {
                                    var productcatCode = String.Empty;
                                    productcatCode = GetProductCategoryCode(success);
                                    if (String.IsNullOrEmpty(productcatCode))
                                    {
                                        validationResults.Add(new DNetValidationResult("Product Category Code tidak ditemukan"));
                                    }
                                    else
                                    {
                                        productcatCode = productcatCode.ToLower();
                                    }
                                    string pathFile = AppConfigs.GetString("StallManagementDirectory");
                                    var nameFile = "PDIData" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + productcatCode + ".txt";
                                    string filePath = Path.Combine(pathFile, nameFile);

                                    try
                                    {
                                        successconnect = imp.Start();
                                        if (successconnect)
                                        {
                                            if (!FileUtility.CheckExistsImagePath(filePath))
                                            {
                                                using (StreamWriter sw = File.CreateText(filePath))
                                                {
                                                    sw.WriteLine(newPDI.Dealer.DealerCode + ',' + newPDI.ChassisMaster.ChassisNumber + ',' + newPDI.Kind + ',' + newPDI.PDIDate.ToString("ddMMyyyy") + ',' + newPDI.ReleaseDate.ToString("ddMMyyyy"));
                                                }
                                                imp.Stop();
                                            }
                                        }
                                    }
                                    finally
                                    {
                                        imp.Dispose();
                                    }

                                }
                            }
                            catch (SqlException ex)
                            {
                                ErrorMsgHelper.SqlException(result.messages, ex.Message);
                            }

                            if (!result.success) ErrorMsgHelper.UpdateNotAvailable(result.messages);
                            // return output ID
                            result._id = success;
                            result.total = 1;
                        }
                        else
                        {
                            return PopulateValidationError<PDIDto>(validationResults, null);
                        }
                    }

                }
                else
                {
                    return PopulateValidationError<PDIDto>(validationResults, null);
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
        /// Validate create parameters
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="chassisMaster"></param>
        /// <param name="dealer"></param>
        /// <param name="dealerBranch"></param>
        /// <returns></returns>
        public bool ValidatePDI(PDIParameterDto objCreate, List<DNetValidationResult> validationResults, ref ChassisMaster chassisMaster, ref Dealer dealer, ref DealerBranch dealerBranch, ref List<int> isAlreadyPDISameDealer)
        {
            bool isValid = true;
            objCreate.ReleaseDate = DateTime.Now;

            // compare the date
            if (!CompareDateWithCurrentDate(objCreate.PDIDate))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDateCompareError, FieldResource.PDIDate, ValidationResource.GreaterThan, FieldResource.TodayDate)));
            }

            // compare the date
            if (!CompareDateWithCurrentDate(objCreate.ReleaseDate))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDateCompareError, FieldResource.ReleaseDate, ValidationResource.GreaterThan, FieldResource.TodayDate)));
            }

            // pdidate can't be more then release date
            if(objCreate.PDIDate > objCreate.ReleaseDate)
            {
                    validationResults.Add(new DNetValidationResult("PDI Date tidak boleh lebih dari Release Date."));
            }

            // validate pdi kind 
            if (!objCreate.Kind.Equals("A"))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.Kind)));
            }

            // validate PDI Kind exists on ENUM
            if (!_enumBL.IsExistByCategoryAndCode("EnumPDIKind.PDIKind", objCreate.Kind))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.Kind)));
            }

            // validate PDI status
            if (!_enumBL.IsExistByCategoryAndValue("PDIStatus", objCreate.PDIStatus))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.PDIStatus)));
            }

            if (objCreate.PDIStatus != _enumBL.GetByCategoryAndCode("PDIStatus", "Proses").ValueId.ToString())
            {
                validationResults.Add(new DNetValidationResult("PDIStatus bukan dikirmkan dengan status 2 (Proses) "));
            }

            // return if any errors found
            isValid = validationResults.Count == 0;

            if (isValid) { isValid = ValidationHelper.ValidateChassisMaster(objCreate.ChassisNumber, validationResults, ref chassisMaster); }

            if (isValid)
            {
                // validate faktur status and end customer id
                // REMOVE FAKTUR VALIDATION -- rEQUEST 20190910 - ako
                //if (chassisMaster.EndCustomerID == 0 || !chassisMaster.FakturStatus.Equals("4"))
                //{
                //    validationResults.Add(new DNetValidationResult(MessageResource.ErrorMsgChassisNotSubmittedYet));
                //}
                // Validate chassis master existence                    
                //else 
                if (IsPDIExist(objCreate, ref isAlreadyPDISameDealer))
                {
                    if (ValidationHelper.IsRevisionFakturExists(objCreate.ChassisNumber))
                    {
                        // set flag is have revition faktur
                        isHaveRevitionFakture = true;
                    }
                    else
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataIsExist, FieldResource.PDI)));
                    }
                }
            }

            // re-evalute the flag
            isValid = validationResults.Count == 0;

            if (isValid) { isValid = ValidationHelper.ValidateDealer(objCreate.DealerCode, validationResults, this.DealerCode, ref dealer); }

            if (isValid) { isValid = ValidationHelper.ValidateDealerBranch(dealer.DealerCode, validationResults, objCreate.DealerBranchCode, ref dealerBranch); }

            // validasi tgl PDI and faktur
            // Remove Validation for PDI dan Faktur - 20190917
            //if (isValid)
            //{
            //    try
            //    {
            //        if (chassisMaster.EndCustomer == null)
            //        {
            //            //validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgEndCustomerNotFound, chassisMaster.EndCustomerID, chassisMaster.ChassisNumber)));
            //        }
            //        else
            //        {
            //            if (chassisMaster.EndCustomer.OpenFakturDate.Year <= 1900)
            //            {
            //                validationResults.Add(new DNetValidationResult(string.Format(ValidationResource.OpenFakturDateEmpty)));
            //            }

            //            if (objCreate.PDIDate < chassisMaster.EndCustomer.OpenFakturDate && chassisMaster.EndCustomer.OpenFakturDate.Year > 1900)
            //            {
            //                validationResults.Add(new DNetValidationResult(string.Format(ValidationResource.PDIDateLessThanOpenFakturDate)));
            //            }
            //        }
            //    }
            //    catch
            //    {
            //        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgEndCustomerNotFound, chassisMaster.EndCustomerID, chassisMaster.ChassisNumber)));
            //    }
            //}

            // re-evalute the flag
            isValid = validationResults.Count == 0;

            if (isValid)
            {
                // check if it is valid to create faktur
                if (!chassisMaster.isValidToCreateFaktur)
                {
                    validationResults.Add(new DNetValidationResult(string.Format(ValidationResource.ChassisBeingRetur, chassisMaster.ChassisNumber)));
                }
            }

            // re-evalute the flag
            return validationResults.Count == 0;
        }

        /// <summary>
        /// Update PDI
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<PDIDto> Update(PDIParameterDto objUpdate)
        {
            #region declare
            var result = new ResponseBase<PDIDto>();
            var validationResults = new List<DNetValidationResult>();
            var isValid = true;
            ChassisMaster chassisMaster = null;
            Dealer dealer = null;
            DealerBranch dealerBranch = null;
            #endregion

            try
            {
                // compare the date
                if (!CompareDateWithCurrentDate(objUpdate.PDIDate))
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDateCompareError, FieldResource.PDIDate, ValidationResource.GreaterThan, FieldResource.TodayDate)));
                }

                // compare the date
                if (!CompareDateWithCurrentDate(objUpdate.ReleaseDate))
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDateCompareError, FieldResource.ReleaseDate, ValidationResource.GreaterThan, FieldResource.TodayDate)));
                }

                // pdidate can't be more then release date
                if (objUpdate.PDIDate > objUpdate.ReleaseDate)
                {
                    validationResults.Add(new DNetValidationResult("PDI Date tidak boleh lebih dari Release Date."));
                }

                // validate pdi kind
                if (!_enumBL.IsExistByCategoryAndCode("EnumPDIKind.PDIKind", objUpdate.Kind))
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.Kind)));
                }

                // re-evalute the flag
                isValid = validationResults.Count == 0;

                if (isValid) { isValid = ValidationHelper.ValidateChassisMaster(objUpdate.ChassisNumber, validationResults, ref chassisMaster); }

                if (isValid) { isValid = ValidationHelper.ValidateDealer(objUpdate.DealerCode, validationResults, this.DealerCode, ref dealer); }

                if (isValid) { isValid = ValidationHelper.ValidateDealerBranch(dealer.DealerCode, validationResults, objUpdate.DealerBranchCode, ref dealerBranch); }

                // validasi tgl PDI and faktur
                if (isValid)
                {
                    if (chassisMaster.EndCustomer != null)
                    {
                        if (chassisMaster.EndCustomer.OpenFakturDate.Year <= 1900)
                        {
                            validationResults.Add(new DNetValidationResult(ErrorCode.DataTypeOrDataFormatInvalid, string.Format(ValidationResource.OpenFakturDateEmpty)));
                        }

                        if (objUpdate.PDIDate < chassisMaster.EndCustomer.OpenFakturDate && chassisMaster.EndCustomer.OpenFakturDate.Year > 1900)
                        {
                            validationResults.Add(new DNetValidationResult(ErrorCode.DataTypeOrDataFormatInvalid, string.Format(ValidationResource.PDIDateLessThanOpenFakturDate)));
                        }
                    }
                    else
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.OpenFakturDate)));
                    }
                }

                // re-evalute the flag
                isValid = validationResults.Count == 0;

                if (isValid)
                {
                    // create pdi object
                    var newPDI = _mapper.Map<PDI>(objUpdate);
                    newPDI.Dealer = dealer;
                    if (dealerBranch != null)
                    {
                        dealerBranch.MarkLoaded();
                        newPDI.DealerBranch = dealerBranch;
                    }
                    newPDI.ChassisMaster = chassisMaster;
                    newPDI.ReleaseDate = DateTime.Now;
                    newPDI.LastUpdateTime = DateTime.Now;

                    var success = (int)_pdiMapper.Update(newPDI, DNetUserName);
                    result.success = success > 0;
                    if (!result.success) ErrorMsgHelper.UpdateNotAvailable(result.messages);
                    // return output ID
                    result._id = success;
                    result.total = 1;
                }
                else
                {
                    return PopulateValidationError<PDIDto>(validationResults, null);
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

        public ResponseBase<PDIDto> Delete(PDIDeleteParameterDto paramDelete)
        {
            var result = new ResponseBase<PDIDto>()
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
                    var pdiCriteria = new CriteriaComposite(new Criteria(typeof(PDI), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    //check if WO Number exist
                    pdiCriteria.opAnd(new Criteria(typeof(PDI), "WorkOrderNumber", MatchType.Exact, paramDelete.WorkOrderNumber));
                    //check Dealer
                    pdiCriteria.opAnd(new Criteria(typeof(PDI), "Dealer.DealerCode", MatchType.Exact, DealerCode));
                    //check Dealer Branch if Exist
                    if (paramDelete.DealerBranchCode.Trim() != string.Empty)
                    {
                        pdiCriteria.opAnd(new Criteria(typeof(PDI), "DealerBranch.DealerBranchCode", MatchType.Exact, paramDelete.DealerBranchCode));
                    }
                    //check Chassis
                    pdiCriteria.opAnd(new Criteria(typeof(PDI), "ChassisMaster.ChassisNumber", MatchType.Exact, paramDelete.ChassisNumber));

                    var pdis = _pdiMapper.RetrieveByCriteria(pdiCriteria);
                    if (pdis.Count == 0)
                    {
                        validationResults.Add(new DNetValidationResult(string.Format("Work Order Number {0} tidak terdaftar untuk Dealer {1}", paramDelete.WorkOrderNumber, DealerCode)));
                    }
                    else
                    {
                        var pdi = pdis.Cast<PDI>().ToList()[0];
                        // check row status
                        if (pdi.RowStatus != 0)
                        {
                            validationResults.Add(new DNetValidationResult(string.Format("Work Order Number {0} tidak aktif, tidak dapat membatalkan PCI dengan Work Order Number yang tidak aktif", paramDelete.WorkOrderNumber)));
                        }
                        // check if status == baru
                        else if (pdi.PDIStatus != _enumBL.GetByCategoryAndCode("PDIStatus", "Baru").ValueId.ToString())
                        {
                            validationResults.Add(new DNetValidationResult(string.Format("Work Order Number {0} sedang diproses dan tidak dapat dibatalkan", paramDelete.WorkOrderNumber)));
                        }
                        // delete
                        else
                        {

                            pdi.RowStatus = -1;
                            var success = (int)_pdiMapper.Update(pdi, DNetUserName);
                            result.success = success > 0;
                            if (!result.success)
                            {
                                ErrorMsgHelper.UpdateNotAvailable(result.messages);
                            }
                            // return output ID
                            result._id = pdi.ID;
                            result.total = 1;
                        }

                    }
                }
                if (validationResults.Count > 0)
                {
                    return PopulateValidationError<PDIDto>(validationResults, null);
                }
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            return result;
        }

        public FileStream GetFile(PDIGetFileParameter parameter, out string filename)
        {
            FileStream fs = null;
            filename = string.Empty;
            try
            {
                var crRelease = Convert.ToDateTime(_appConfigBL.GetConfigByName("PDIPKTReleaseDate", string.Empty).Value).Date;

                CriteriaComposite crit = new CriteriaComposite(new Criteria(typeof(PDI), "RowStatus", MatchType.Exact, (int)DBRowStatus.Active));
                crit.opAnd(new Criteria(typeof(PDI), "PDIStatus", MatchType.InSet, string.Format("('{0}','{1}')", (int)EnumFSStatus.FSStatus.Proses, (int)EnumFSStatus.FSStatus.Selesai)));
                crit.opAnd(new Criteria(typeof(PDI), "CreatedTime", MatchType.GreaterOrEqual, crRelease.Date));
                crit.opAnd(new Criteria(typeof(PDI), "ChassisMaster.ChassisNumber", MatchType.Exact, parameter.ChassisNumber.Trim()));
                crit.opAnd(new Criteria(typeof(PDI), "ChassisMaster.RowStatus", MatchType.Exact, (int)DBRowStatus.Active));
                PDI objPdi = _pdiMapper.RetrieveByCriteria(crit).Cast<PDI>().FirstOrDefault();

                if (objPdi == null)
                    return null;

                bool isUpdate = false;
                List<ValidResult> validationResults = new List<ValidResult>();

                PDIValidation pdiValidation = new PDIValidation(AppConfigs.GetString("SAN"), AppConfigs.GetString("User"), AppConfigs.GetString("Password"), AppConfigs.GetString("WebServer"));
                if (pdiValidation.GenerateCertificate(objPdi, ref isUpdate, ref filename, ref fs, validationResults, parameter.IsEncrypted))
                {
                    if (isUpdate)
                    {
                        objPdi.FileName = filename;
                        _pdiMapper.Update(objPdi, DNetUserName);
                        var arr = filename.Split('\\');
                        filename = parameter.IsEncrypted ? arr[3].Replace(".pdf", "-pwd.pdf") : arr[3];
                    }
                }
            }
            catch (Exception ex)
            {
                WsLog wlog = new WsLog();
                wlog.Body = ex.Message;
                _wlogMapper.Insert(wlog, DNetUserName);

            }

            return fs;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Compare with current date
        /// </summary>
        /// <param name="entryDate"></param>
        /// <returns></returns>
        private bool CompareDateWithCurrentDate(DateTime? entryDate)
        {
            DateTime currentDate = DateTime.Now;

            if (entryDate.HasValue)
            {
                if (entryDate.Value.Date > currentDate.Date)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validate chassis master in PDI
        /// </summary>
        /// <param name="modelDTO"></param>
        /// <returns></returns>
        private bool IsPDIExist(PDIParameterDto objCreate, ref List<int> isAlreadyPDISameDealer)
        {
            // PDI Validation         
            var criterias = new CriteriaComposite(new Criteria(typeof(PDI), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(PDI), "ChassisMaster.ChassisNumber", MatchType.Exact, objCreate.ChassisNumber));
            PDI pdi = null;

            var pdiResult = _pdiMapper.RetrieveByCriteria(criterias);

            if (pdiResult.Count == 0) return false;

            var list = pdiResult.Cast<PDI>().ToList();
            pdi = list.FirstOrDefault();
            if (pdi == null)
            {
                return false;
            }
            else
            {
                if(pdi.Dealer.DealerCode == objCreate.DealerCode)
                {
                    isAlreadyPDISameDealer.Add(1);
                    isAlreadyPDISameDealer.Add(pdi.ID);
                    return false;
                }
                else
                {
                    return true;
                }
            }

            
        }
        private string GetProductCategoryCode(int success)
        {
            bool Vreturn = true;
            var code = string.Empty;
            var pdiCriteria = new CriteriaComposite(new Criteria(typeof(PDI), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            pdiCriteria.opAnd(new Criteria(typeof(PDI), "ID", MatchType.Exact, success));
            var pdis = _pdiMapper.RetrieveByCriteria(pdiCriteria);
            if (pdis.Count == 0)
            {
                Vreturn = false;
            }
            else
            {
                var pdiData = pdis.Cast<PDI>().ToList()[0];
                var chassismasterCriteria = new CriteriaComposite(new Criteria(typeof(ChassisMaster), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                chassismasterCriteria.opAnd(new Criteria(typeof(ChassisMaster), "ID", MatchType.Exact, pdiData.ChassisMaster.ID));
                var chassismasters = _chassisMaster.RetrieveByCriteria(chassismasterCriteria);
                if (chassismasters.Count == 0)
                {
                    Vreturn = false;
                }
                else
                {
                    var chassisData = chassismasters.Cast<ChassisMaster>().ToList()[0];
                    var vechilceColorCriteria = new CriteriaComposite(new Criteria(typeof(VechileColor), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                    vechilceColorCriteria.opAnd(new Criteria(typeof(VechileColor), "ID", MatchType.Exact, chassisData.VechileColor.ID));
                    var vehicleColorsRetrieve = _vehicleColor.RetrieveByCriteria(vechilceColorCriteria);
                    if (vehicleColorsRetrieve.Count == 0)
                    {
                        Vreturn = false;
                    }
                    else
                    {
                        var vehicleColorData = vehicleColorsRetrieve.Cast<VechileColor>().ToList()[0];
                        var vechilceTypeCriteria = new CriteriaComposite(new Criteria(typeof(VechileType), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                        vechilceTypeCriteria.opAnd(new Criteria(typeof(VechileType), "ID", MatchType.Exact, vehicleColorData.VechileType.ID));
                        var vehicleTypeRetrieve = _vehicleType.RetrieveByCriteria(vechilceTypeCriteria);
                        if (vehicleTypeRetrieve.Count == 0)
                        {
                            Vreturn = false;
                        }
                        else
                        {
                            var vehicleTypeData = vehicleTypeRetrieve.Cast<VechileType>().ToList()[0];
                            var productcategoryCriteria = new CriteriaComposite(new Criteria(typeof(ProductCategory), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                            productcategoryCriteria.opAnd(new Criteria(typeof(ProductCategory), "ID", MatchType.Exact, vehicleTypeData.ProductCategory.ID));
                            var productcategoryRetrieve = _productCategory.RetrieveByCriteria(productcategoryCriteria);
                            if (productcategoryRetrieve.Count == 0)
                            {
                                Vreturn = false;
                            }
                            else
                            {
                                var productCategoryData = productcategoryRetrieve.Cast<ProductCategory>().ToList()[0];
                                code = productCategoryData.Code;
                            }
                        }
                    }
                }
            }
            return code;
        }


        #endregion
    }
}

