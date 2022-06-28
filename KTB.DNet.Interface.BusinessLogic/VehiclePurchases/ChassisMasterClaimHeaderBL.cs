#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ChassisMasterClaimHeader business logic class
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
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Web.UI;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class ChassisMasterClaimHeaderBL : AbstractBusinessLogic, IChassisMasterClaimHeaderBL
    {
        #region Variables
        private readonly IMapper _chassisMasterClaimHeaderMapper;
        private readonly IMapper _chassisMasterClaimDetailMapper;
        private readonly IMapper _chassisMasterLogisticCompany;
        private readonly IMapper _documentUploadMapper;
        private readonly AutoMapper.IMapper _mapper;
        private TransactionManager _transactionManager;
        private StandardCodeBL _enumBL;
        #endregion

        #region Constructor
        public ChassisMasterClaimHeaderBL()
        {
            _chassisMasterClaimHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMasterClaimHeader).ToString());
            _chassisMasterClaimDetailMapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMasterClaimDetail).ToString());
            _documentUploadMapper = MapperFactory.GetInstance().GetMapper(typeof(DocumentUpload).ToString());
            _chassisMasterLogisticCompany = MapperFactory.GetInstance().GetMapper(typeof(ChassisMasterLogisticCompany).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _enumBL = new StandardCodeBL(_mapper);
            _transactionManager = new TransactionManager();
            _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Create a new ChassisMasterClaim
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<ChassisMastertClaimHeaderCreateResponseDto> Create(ChassisMasterClaimHeaderParameterDto objCreate)
        {

            #region Initialized
            var result = new ResponseBase<ChassisMastertClaimHeaderCreateResponseDto>();
            var validationResults = new List<DNetValidationResult>();
            ChassisMasterClaimHeader chassisMasterClaimHeader;
            List<ChassisMasterClaimDetail> chassisMasterClaimDetails = new List<ChassisMasterClaimDetail>();
            List<DocumentUpload> documentUpload = new List<DocumentUpload>();
            #endregion

            objCreate.CreatedBy = DNetUserName;
            objCreate.CreatedTime = DateTime.Now;
            objCreate.LastUpdateBy = DNetUserName;
            objCreate.LastUpdateTime = DateTime.Now;

            // validate ChassisMasterClaim
            var message = "";
            ValidateChassisMasterClaimHeader(objCreate, out chassisMasterClaimHeader, validationResults);
            if (validationResults.Count == 0) { ValidateDocumentUpload(objCreate, validationResults, documentUpload); }
            if (validationResults.Count == 0)
            {
                CBUReturnValidation.IsValidThisClaim(chassisMasterClaimHeader, documentUpload, ref message);
                if (!String.IsNullOrEmpty(message))
                {
                    validationResults.Add(new DNetValidationResult(message));
                }
            }
            int id = 0;
            if (validationResults.Count == 0) { 
                CBUReturnValidation.IsDuplicateDataClaim(objCreate.ChassisNumber, ref id);
                if(id!=0)
                {
                    message = "Claim dengan Chassis Number " + objCreate.ChassisNumber + " sudah pernah dibuat";
                    validationResults.Add(new DNetValidationResult(message));
                }
            }
            if (validationResults.Count == 0) { ValidateChassisMasterClaimDetail(objCreate, validationResults, chassisMasterClaimDetails); }
            if (validationResults.Count == 0)
            {
                try
                {
                    chassisMasterClaimHeader.ClaimNumber = GenerateClaimNumber();
                    var createdObject = InsertWithTransactionManager(chassisMasterClaimHeader, chassisMasterClaimDetails, documentUpload);
                    if (createdObject != null)
                    {
                        var obj = (ChassisMasterClaimHeader)_chassisMasterClaimHeaderMapper.Retrieve(createdObject.ID);
                        ChassisMastertClaimHeaderCreateResponseDto responseModel = _mapper.Map<ChassisMastertClaimHeaderCreateResponseDto>(obj);

                        if (obj != null)
                        {
                            result._id = createdObject.ID;
                            result.success = true;
                            result.total = 1;
                            result.lst = new ChassisMastertClaimHeaderCreateResponseDto()
                            {
                                ClaimNumber = obj.ClaimNumber,
                                ID = obj.ID,
                                ChassisMasterClaimDetails = responseModel.ChassisMasterClaimDetails,
                                DocumentUpload = responseModel.DocumentUpload,
                                LastUpdateTime = obj.LastUpdateTIme
                               
                            };
                            
                            /*result.lst = _mapper.Map<ChassisMastertClaimHeaderCreateResponseDto>(obj);*/

                        }
                    }
                    else
                    {
                        ErrorMsgHelper.ErrorMsgDBSaveContactAdmin(result.messages);
                    }
                }
                catch (SqlException ex)
                {
                    ErrorMsgHelper.SqlException(result.messages, ex.Message);
                }
            }
            else
            {
                return PopulateValidationError<ChassisMastertClaimHeaderCreateResponseDto>(validationResults, null);
            }
            
            return result;
        }

        public ResponseBase<ChassisMastertClaimHeaderDto> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<List<ChassisMastertClaimHeaderDto>> Read(ChassisMasterClaimHeaderFilterDto filterDto, int pageSize)
        {
            throw new NotImplementedException();
        }
        

        /// <summary>
        /// Read ChassisMasterClaimHeader Data
        /// </summary>
        /// <param name="filterDto">Filter Parameter</param>
        /// <param name="pageSize">Page Size</param>
        /// <returns></returns>
        public ResponseBase<List<ChassisMastertClaimHeaderDto>> ReadData(ChassisMasterClaimHeaderFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(ChassisMasterClaimHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(ChassisMasterClaimHeader), "Dealer.DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<ChassisMastertClaimHeaderDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(ChassisMasterClaimHeader), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(ChassisMasterClaimHeader), filterDto, sortColl);

                var data = _chassisMasterClaimHeaderMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<ChassisMasterClaimHeader>().ToList();
                    var listData = new List<ChassisMastertClaimHeaderDto>();
                    var _mapperIndicator = MapperFactory.GetInstance().GetMapper(typeof(StatusChangeHistory).ToString());
                    foreach (var item in list)
                    {
                        
                        // map it
                        var chassismasterclaimheaderDto = _mapper.Map<ChassisMastertClaimHeaderDto>(item);
                        if (item.TransferDate.Year == 1753)
                        {
                            chassismasterclaimheaderDto.TransferDate = null;
                        }
                        var claimnumber = chassismasterclaimheaderDto.ClaimNumber;
                        CriteriaComposite criteriaIndicator = new CriteriaComposite(new Criteria(typeof(StatusChangeHistory), "DocumentRegNumber", MatchType.Exact, claimnumber));
                        criteriaIndicator.opAnd(new Criteria(typeof(StatusChangeHistory), "DocumentType", MatchType.Exact, 20));
                        var masters = _mapperIndicator.RetrieveByCriteria(criteriaIndicator);
                        var id_indicator = 0;
                        if (masters.Count > 0)
                        {
                            foreach(var i in masters)
                            {
                                var indicator = i as StatusChangeHistory;
                                if(indicator.OldStatus == 4 || indicator.OldStatus == 5 || indicator.NewStatus == 4 || indicator.NewStatus==5)
                                {
                                    
                                    if (indicator.OldStatus==4 || indicator.OldStatus==5)
                                    {
                                        id_indicator = indicator.OldStatus;
                                    }else if(indicator.NewStatus == 4 || indicator.NewStatus == 5)
                                    {
                                        id_indicator = indicator.NewStatus;
                                    }
                                }
                            }
                            chassismasterclaimheaderDto.IndicatorID = id_indicator;
                        }
                        listData.Add(chassismasterclaimheaderDto);
                    };

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(ChassisMasterClaimHeader), filterDto);
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
        /// Update ChassisMasterClaim
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ResponseBase<ChassisMastertClaimHeaderUpdateResponseDto> Update(ChassisMasterClaimHeaderUpdateParameterDto objUpdate)
        {
            // set default response
            var result = new ResponseBase<ChassisMastertClaimHeaderUpdateResponseDto>();
            ChassisMasterClaimHeader chassisMasterClaimHeader = null;
            List<DNetValidationResult> validationResults = new List<DNetValidationResult>();
            List<ChassisMasterClaimDetail> chassisMasterClaimDetail = new List<ChassisMasterClaimDetail>();
            List<DocumentUpload> documentUpload = new List<DocumentUpload>();

            try
            {
                //get header and details
                var criterias = new CriteriaComposite(new Criteria(typeof(ChassisMasterClaimHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                criterias.opAnd(new Criteria(typeof(ChassisMasterClaimHeader), "ID", MatchType.Exact, objUpdate.ID));

                var data = _chassisMasterClaimHeaderMapper.RetrieveByCriteria(criterias);
                if (data.Count > 0)
                {
                    chassisMasterClaimHeader = (ChassisMasterClaimHeader)data[0];
                }
                else
                {
                    result.messages.Add(new MessageBase { ErrorCode = ErrorCode.DataUpdateNotAvailable, ErrorMessage = string.Format(MessageResource.ErrorMsgDataNotFound, FieldResource.ID) });
                }
                objUpdate.LastUpdateBy = DNetUserName;
                objUpdate.LastUpdateTime = DateTime.Now;
                objUpdate.CreatedBy = chassisMasterClaimHeader.CreatedBy;
                objUpdate.ClaimNumber = chassisMasterClaimHeader.ClaimNumber;

                //add details to object update
                List<ChassisMasterClaimDetail> claimDetail = new List<ChassisMasterClaimDetail>();
                if (chassisMasterClaimHeader.ChassisMasterClaimDetails.Count > 0)
                {
                    foreach (ChassisMasterClaimDetailParameterDto item in objUpdate.ChassisMasterClaimDetails)
                    {
                        foreach (ChassisMasterClaimDetail detail in chassisMasterClaimHeader.ChassisMasterClaimDetails)
                        {
                            claimDetail.Add(detail);
                            ChassisMasterClaimDetail detailDomain = _mapper.Map<ChassisMasterClaimDetail>(detail);
                            item.CreatedBy = detailDomain.CreatedBy;
                            item.CreatedTime = detailDomain.CreatedTime;
                        }
                    }
                }

                //add document upload to object update
                List<DocumentUpload> oldDocumentUpload = new List<DocumentUpload>();
                if (chassisMasterClaimHeader.DocumentUploads.Count > 0)
                {
                    foreach (ChassisMasterClaimDocumentUploadParameterDto item in objUpdate.DocumentUpload)
                    {
                        foreach (DocumentUpload document in chassisMasterClaimHeader.DocumentUploads)
                        {
                            oldDocumentUpload.Add(document);
                            DocumentUpload documentDomain = _mapper.Map<DocumentUpload>(document);
                            item.CreatedBy = documentDomain.CreatedBy;
                            item.CreatedTime = documentDomain.CreatedTime;
                            item.DocRegNumber = documentDomain.DocRegNumber;
                        }
                    }
                }

                List<StandardCodeDto> _enumChassisClaim = new List<StandardCodeDto>();
                _enumChassisClaim = _enumBL.GetByCategory("StatusClaim");
                var msg = String.Empty;
                if (chassisMasterClaimHeader.StatusID == (_enumChassisClaim.Where(e => e.ValueCode == "Baru").SingleOrDefault().ValueId) || chassisMasterClaimHeader.StatusID == (_enumChassisClaim.Where(e => e.ValueCode == "Validasi").SingleOrDefault().ValueId) || chassisMasterClaimHeader.StatusID == (_enumChassisClaim.Where(e => e.ValueCode == "Konfirmasi").SingleOrDefault().ValueId) || chassisMasterClaimHeader.StatusID == (_enumChassisClaim.Where(e => e.ValueCode == "Revisi").SingleOrDefault().ValueId) || chassisMasterClaimHeader.StatusID == (_enumChassisClaim.Where(e => e.ValueCode == "Proses").SingleOrDefault().ValueId))
                {
                    if (chassisMasterClaimHeader.StatusID == (_enumChassisClaim.Where(e => e.ValueCode == "Baru").SingleOrDefault().ValueId))
                    {
                        if (objUpdate.ClaimDate.ToString("yyyy-MM-dd") != chassisMasterClaimHeader.ClaimDate.ToString("yyyy-MM-dd"))
                        {
                            validationResults.Add(new DNetValidationResult("Claim Date " + objUpdate.ClaimDate + " tidak dapat di update"));
                        }else
                        {
                            // get valid value
                            if (validationResults.Count == 0) { ValidateUpdateChassisMasterClaimHeader(objUpdate, out chassisMasterClaimHeader, validationResults); }
                            chassisMasterClaimHeader.StatusID = (_enumChassisClaim.Where(e => e.ValueCode == "Baru").SingleOrDefault().ValueId);
                            if (validationResults.Count == 0) { ValidateChassisMasterClaimUpdateDetail(objUpdate, validationResults, chassisMasterClaimDetail, claimDetail); }
                            if (validationResults.Count == 0) { ValidateUpdateDocumentUpload(objUpdate, validationResults, documentUpload, oldDocumentUpload); }

                            //validate parameter input statusID hanya boleh baru(0) dan validasi(1)
                            if (validationResults.Count == 0)
                            {
                                ValidateUpdateStatusID(objUpdate.StatusID, chassisMasterClaimHeader.StatusID, validationResults, out msg);
                            }
                        }
                        
                    }else if(chassisMasterClaimHeader.StatusID == (_enumChassisClaim.Where(e => e.ValueCode == "Validasi").SingleOrDefault().ValueId))
                    {
                        //validate parameter input statusID hanya boleh validasi(1)
                        if(validationResults.Count==0)
                        {
                            ValidateUpdateStatusID(objUpdate.StatusID, chassisMasterClaimHeader.StatusID, validationResults, out msg);
                        }
                    }
                    else if(chassisMasterClaimHeader.StatusID == (_enumChassisClaim.Where(e => e.ValueCode == "Konfirmasi").SingleOrDefault().ValueId))
                    {
                        //validate parameter input statusID hanya boleh Konfirmasi(2)
                        if (validationResults.Count == 0)
                        {
                            ValidateUpdateStatusID(objUpdate.StatusID, chassisMasterClaimHeader.StatusID, validationResults, out msg);
                        }

                    }
                    else if (chassisMasterClaimHeader.StatusID == (_enumChassisClaim.Where(e => e.ValueCode == "Proses").SingleOrDefault().ValueId))
                    {
                        //validate parameter input statusID hanya boleh Konfirmasi(2)
                        if (validationResults.Count == 0)
                        {
                            ValidateUpdateStatusID(objUpdate.StatusID, chassisMasterClaimHeader.StatusID, validationResults, out msg);
                        }

                    }
                    else if (chassisMasterClaimHeader.StatusID == (_enumChassisClaim.Where(e => e.ValueCode == "Revisi").SingleOrDefault().ValueId))
                    {
                        if (objUpdate.ClaimDate.ToString("yyyy-MM-dd") != chassisMasterClaimHeader.ClaimDate.ToString("yyyy-MM-dd"))
                        {
                            validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, "ClaimDate", objUpdate.ClaimDate)));

                        }
                        else
                        {
                            //validate parameter input statusID hanya boleh Validasi(1)
                            // get valid value
                            if (validationResults.Count == 0) { ValidateUpdateChassisMasterClaimHeader(objUpdate, out chassisMasterClaimHeader, validationResults); }
                            chassisMasterClaimHeader.StatusID = (_enumChassisClaim.Where(e => e.ValueCode == "Revisi").SingleOrDefault().ValueId);
                            if (validationResults.Count == 0) { ValidateChassisMasterClaimUpdateDetail(objUpdate, validationResults, chassisMasterClaimDetail, claimDetail); }
                            if (validationResults.Count == 0) { ValidateUpdateDocumentUpload(objUpdate, validationResults, documentUpload, oldDocumentUpload); }

                            //validate parameter input statusID hanya boleh baru(0) dan validasi(1)
                            if (validationResults.Count == 0)
                            {
                                ValidateUpdateStatusID(objUpdate.StatusID, chassisMasterClaimHeader.StatusID, validationResults, out msg);
                            }
                        }
                    }
                    if(validationResults.Count>0)
                    {
                        if(string.IsNullOrEmpty(msg))
                        {
                            msg = validationResults[0].ErrorMessage;
                        }
                    }
                    if (validationResults.Count == 0)
                    {
                        int updateResult = UpdateWithTransactionManager(chassisMasterClaimHeader, chassisMasterClaimDetail, documentUpload, objUpdate);
                        if (updateResult > 0)
                        {
                            ChassisMastertClaimHeaderUpdateResponseDto responseModel = _mapper.Map<ChassisMastertClaimHeaderUpdateResponseDto>(chassisMasterClaimHeader);
                            result.success = true;
                            result._id = responseModel.ID;
                            result.lst = new ChassisMastertClaimHeaderUpdateResponseDto()
                            {
                                ClaimNumber = responseModel.ClaimNumber,
                                ID = responseModel.ID,
                                LastUpdateTime = responseModel.LastUpdateTime,
                                ChassisMasterClaimDetails = responseModel.ChassisMasterClaimDetails,
                                DocumentUpload = responseModel.DocumentUpload
                            };
                            result.total = 1;
                        }
                        else
                        {
                            ErrorMsgHelper.ErrorMsgDBSave(result.messages, FieldResource.ID);
                        }
                    }
                    else
                    {
                        result.messages.Add(new MessageBase { ErrorCode = ErrorCode.DataUpdateNotAvailable, ErrorMessage = msg });
                    }

                }
                else
                {
                    result.messages.Add(new MessageBase { ErrorCode = ErrorCode.DataUpdateNotAvailable, ErrorMessage = "Data statusID tidak valid" });
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

        ResponseBase<ChassisMastertClaimHeaderDto> IBaseInterface<ChassisMasterClaimHeaderParameterDto, ChassisMasterClaimHeaderFilterDto, ChassisMastertClaimHeaderDto>.Create(ChassisMasterClaimHeaderParameterDto objCreate)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region Private Methods

        private string GenerateClaimNumber()
        {
            var claimnumber = "";
            var claimNumberGenerator = _chassisMasterClaimHeaderMapper.RetrieveDataSet("SELECT dbo.fn_CBUReturnClaimNumberGenerator(getdate()) as ClaimNumber");
            if (claimNumberGenerator.Tables.Count > 0)
            {
                if (claimNumberGenerator.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in claimNumberGenerator.Tables[0].Rows)
                    {
                        claimnumber = item["ClaimNumber"].ToString();
                    }
                }
            }
            return claimnumber;
        }

        /// <summary>
        /// Validate Chassis Claim Document Upload
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="documentUpload"></param>
        private void ValidateDocumentUpload(ChassisMasterClaimHeaderParameterDto objCreate, List<DNetValidationResult> validationResults, List<DocumentUpload> documentUpload)
        {
            if (objCreate.ChassisMasterClaimDetails.Count == 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgEmptyList)));
            }

            // validate ImagePath
            // get destination folder from web config
            string sapFolder = AppConfigs.GetString("SAPFolder");
            string sapDir = AppConfigs.GetString("CBUReturnClaimDirectory");
            string destFolder = Path.Combine(sapFolder, sapDir);
            foreach (ChassisMasterClaimDocumentUploadParameterDto item in objCreate.DocumentUpload)
            {
                DocumentUpload document = _mapper.Map<DocumentUpload>(item);
                //VALIDATE TYPE
                // initialize the mapper
                var category = "DocumentUpload.Type";
                // get by criteria
                var _mapperStandardCode = MapperFactory.GetInstance().GetMapper(typeof(StandardCode).ToString());
                var masters = _mapperStandardCode.RetrieveByCriteria(Helper.GenerateCriteria(typeof(StandardCode), "Category", "ValueId", category, document.Type));
                if (masters.Count > 0)
                {
                    var pathList = document.Path.Split('\\');
                    var path = pathList[pathList.Length - 5] + '\\' + pathList[pathList.Length - 4] + '\\' + pathList[pathList.Length - 3] + '\\' + pathList[pathList.Length - 2] + '\\' + pathList[pathList.Length - 1];
                    string imagePath = destFolder + path;
                    document.Path = sapDir + path;
                    if (FileUtility.CheckExistsImagePath(imagePath))
                    {
                        documentUpload.Add(document);
                    }
                    else
                    {
                        validationResults.Add(new DNetValidationResult("ImagePath " + item.Path + " tidak valid."));
                    }
                }
                else
                {
                    validationResults.Add(new DNetValidationResult(document.Type+" tidak tersedia dalam standardcode"));
                }

                
            }

        }

        /// <summary>
        /// Validate Update Chassis Claim Document Upload
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <param name="validationResults"></param>
        /// <param name="documentUpload"></param>
        private void ValidateUpdateDocumentUpload(ChassisMasterClaimHeaderUpdateParameterDto objUpdate, List<DNetValidationResult> validationResults, List<DocumentUpload> documentUpload, List<DocumentUpload> oldDocumentUpload)
        {
            if (objUpdate.ChassisMasterClaimDetails.Count == 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgEmptyList)));
            }

            // validate ImagePath
            // get destination folder from web config
            string sapFolder = AppConfigs.GetString("SAPFolder");
            string sapDir = AppConfigs.GetString("CBUReturnClaimDirectory");
            string destFolder = Path.Combine(sapFolder, sapDir);

            string user = AppConfigs.GetString("User");
            string password = AppConfigs.GetString("Password");
            string webServer = AppConfigs.GetString("WebServer");
            UserImpersonater imp = new UserImpersonater(user, password, webServer);
            bool success = false;

            foreach (ChassisMasterClaimDocumentUploadParameterDto item in objUpdate.DocumentUpload)
            {
                DocumentUpload document = _mapper.Map<DocumentUpload>(item);
                //VALIDATE TYPE
                // initialize the mapper
                var category = "DocumentUpload.Type";
                // get by criteria
                var _mapperStandardCode = MapperFactory.GetInstance().GetMapper(typeof(StandardCode).ToString());
                var masters = _mapperStandardCode.RetrieveByCriteria(Helper.GenerateCriteria(typeof(StandardCode), "Category", "ValueId", category, document.Type));
                if (masters.Count > 0)
                {
                    var pathList = document.Path.Split('\\');
                    var pathexisting = pathList[pathList.Length-5] + '\\' + pathList[pathList.Length - 4] + '\\' + pathList[pathList.Length - 3] + '\\' + pathList[pathList.Length - 2] + '\\';
                    var claimnumber = objUpdate.ClaimNumber.Replace("/", "");

                    var newpathImage = destFolder + pathexisting + claimnumber;

                    var newFileImage = destFolder + pathexisting + claimnumber +"\\"+ pathList[pathList.Length-1];
                    string imageFileExisting = destFolder + pathexisting +"\\"+ pathList[pathList.Length - 1];
                    var valid = true;
                    //check di general
                    if (FileUtility.CheckExistsImagePath(imageFileExisting))
                    {
                        //exist in general folder
                        //check in claim folder
                        if (FileUtility.CheckExistsImagePath(newFileImage))
                        {
                            //exist in general and claim folder
                            try
                            {
                                success = imp.Start();
                                if (success)
                                {
                                    System.IO.File.Delete(imageFileExisting);
                                    imp.Stop();
                                }
                            }
                            finally
                            {
                                imp.Dispose();
                            }
                        }
                        else
                        {
                            //exist in general but not exist in claim folder
                            try
                            {
                                success = imp.Start();
                                if (success)
                                {
                                    System.IO.Directory.CreateDirectory(newpathImage);
                                    System.IO.File.Copy(imageFileExisting, newFileImage, true);
                                    System.IO.File.Delete(imageFileExisting);
                                    imp.Stop();
                                }
                            }
                            finally
                            {
                                imp.Dispose();
                            }
                        }
                        document.Path = sapDir + pathexisting + claimnumber + "\\" + pathList[pathList.Length - 1];
                        documentUpload.Add(document);
                    }
                    else
                    {
                        //not exist in general folder
                        //check in claim folder 
                        if (FileUtility.CheckExistsImagePath(newFileImage))
                        {
                            //not exist in general but exist in claim folder
                            document.Path = sapDir + pathexisting + claimnumber + "\\" + pathList[pathList.Length - 1];
                            documentUpload.Add(document);
                        }
                        else
                        {
                            //not exist in general and claim folder
                            validationResults.Add(new DNetValidationResult("Path " + document.Path + " tidak valid"));
                            valid = false;
                        }
                    }
                    if (valid == true)
                    {
                        for (var i = 0; i < oldDocumentUpload.Count(); i++)
                        {
                            if (oldDocumentUpload[i].ID == item.ID)
                            {
                                var oldimage = oldDocumentUpload[i].Path.Split('\\');
                                var newImage = item.Path.Split('\\');
                                if (oldimage[oldimage.Length - 1] != newImage[newImage.Length - 1])
                                {
                                    var deletedImage = destFolder + oldimage[oldimage.Length - 6] + '\\' + oldimage[oldimage.Length - 5] + '\\' + oldimage[oldimage.Length - 4] + '\\' + oldimage[oldimage.Length - 3] + '\\' + oldimage[oldimage.Length - 2] + '\\' + oldimage[oldimage.Length - 1];
                                    if (FileUtility.CheckExistsImagePath(deletedImage))
                                    {
                                        try
                                        {
                                            success = imp.Start();
                                            if (success)
                                            {
                                                System.IO.File.Delete(deletedImage);
                                                imp.Stop();
                                            }
                                        }
                                        finally
                                        {
                                            imp.Dispose();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    validationResults.Add(new DNetValidationResult("Item "+document.Type+" tidak tersedia dalam standardcode"));
                }
            }
            //delete file yang tidak diinput dan diupdate
            List<int> new_document = new List<int>();
            List<string> new_path_document = new List<string>();
            foreach (DocumentUpload newItem in documentUpload)
            {
                var split = newItem.Path.Split('\\');
                new_path_document.Add(split[split.Length - 1]);
                new_document.Add(newItem.ID);
            }
            foreach (DocumentUpload item in oldDocumentUpload)
            {
                if (new_document.IndexOf(item.ID) == -1)
                {
                    try
                    {
                        success = imp.Start();
                        if (success)
                        {
                            var itemPath = item.Path.Split('\\'); ;
                            var pathImageSplit = itemPath[itemPath.Length - 6 ] + '\\' + itemPath[itemPath.Length - 5 ] + '\\' + itemPath[itemPath.Length - 4] + '\\' + itemPath[itemPath.Length - 3] + '\\' + itemPath[itemPath.Length - 2] + '\\' + itemPath[itemPath.Length - 1];
                            if(new_path_document.IndexOf(itemPath[itemPath.Length-1]) == -1)
                            {
                                System.IO.File.Delete(destFolder + pathImageSplit);
                            }
                            imp.Stop();
                        }
                    }
                    finally
                    {
                        imp.Dispose();
                    }
                    item.RowStatus = (short)DBRowStatus.Deleted;
                    documentUpload.Add(item);
                }
            }
        }

        private string GenerateCode(string dealerCode, int year, int month)
        {
            var _ret = "";
            var noBuilder = "";
            var RunningNumber = 0;
            var docregnumber = "";

            noBuilder = "CLM/" + month + "/" + year.ToString();

            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMasterClaimHeader).ToString());

            // get by criteria
            CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(ChassisMasterClaimHeader), "ClaimNumber", MatchType.StartsWith, noBuilder.ToString()));
            var sortColl = new SortCollection();
            sortColl.Add(new Sort(typeof(ChassisMasterClaimHeader), "ClaimNumber", Sort.SortDirection.DESC));
            var masters = _mapper.RetrieveByCriteria(criteria, sortColl);
            if (masters.Count > 0)
            {
                var partShop = masters[0] as ChassisMasterClaimHeader;
                docregnumber = partShop.ClaimNumber;
                docregnumber = docregnumber.Substring(docregnumber.Length - 3);
                RunningNumber = Convert.ToInt32(docregnumber) + 1;
                docregnumber = RunningNumber.ToString().PadLeft(3, '0');
                _ret = noBuilder.ToString() + "/" + docregnumber;
            }
            else
            {
                _ret = noBuilder.ToString() +"/"+ "001";
            }

            return _ret;
        }

        /// <summary>
        /// Validate Chassis Claim Detail
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="chassisMasterClaimDetails"></param>
        private void ValidateChassisMasterClaimDetail(ChassisMasterClaimHeaderParameterDto objCreate, List<DNetValidationResult> validationResults, List<ChassisMasterClaimDetail> chassisMasterClaimDetails)
        {
            if (objCreate.ChassisMasterClaimDetails.Count == 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgEmptyList, FieldResource.ChassisMasterClaimDetail)));
            }

            foreach (ChassisMasterClaimDetailParameterDto item in objCreate.ChassisMasterClaimDetails)
            {
                ChassisMasterClaimDetail detail = _mapper.Map<ChassisMasterClaimDetail>(item);
                //VALIDATE CLAIM TYPE
                // initialize the mapper
                var category = "ChassisMasterClaim.TipeClaim";
                // get by criteria
                var _mapperStandardCode = MapperFactory.GetInstance().GetMapper(typeof(StandardCode).ToString());
                var masters = _mapperStandardCode.RetrieveByCriteria(Helper.GenerateCriteria(typeof(StandardCode), "Category", "ValueId", category, detail.ClaimType));
                if (masters.Count > 0)
                {
                    chassisMasterClaimDetails.Add(detail);
                }
                else
                {
                    validationResults.Add(new DNetValidationResult(detail.ClaimType + " tidak tersedia dalam standardcode"));
                }

            }
        }

        /// <summary>
        /// Validate Chassis Claim Detail
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="chassisMasterClaimDetails"></param>
        private void ValidateChassisMasterClaimUpdateDetail(ChassisMasterClaimHeaderUpdateParameterDto objCreate, List<DNetValidationResult> validationResults, List<ChassisMasterClaimDetail> chassisMasterClaimDetails, List<ChassisMasterClaimDetail> claimDetail)
        {
            if (objCreate.ChassisMasterClaimDetails.Count == 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgEmptyList, FieldResource.ChassisMasterClaimDetail)));
            }
            List<int> new_detail = new List<int>();
            foreach (ChassisMasterClaimDetailParameterDto item in objCreate.ChassisMasterClaimDetails)
            {
                ChassisMasterClaimDetail detail = _mapper.Map<ChassisMasterClaimDetail>(item);
                //VALIDATE CLAIM TYPE
                // initialize the mapper
                var category = "ChassisMasterClaim.TipeClaim";
                // get by criteria
                var _mapperStandardCode = MapperFactory.GetInstance().GetMapper(typeof(StandardCode).ToString());
                var masters = _mapperStandardCode.RetrieveByCriteria(Helper.GenerateCriteria(typeof(StandardCode), "Category", "ValueId", category, detail.ClaimType));
                if (masters.Count > 0)
                {
                    chassisMasterClaimDetails.Add(detail);
                    new_detail.Add(item.ID);
                }
                else
                {
                    validationResults.Add(new DNetValidationResult(detail.ClaimType + " tidak tersedia dalam standardcode"));
                }

            }
            //delete file yang tidak diinput dan update
            foreach (ChassisMasterClaimDetail item in claimDetail)
            {
                if (new_detail.IndexOf(item.ID) == -1)
                {
                    item.RowStatus = (short)DBRowStatus.Deleted;
                    chassisMasterClaimDetails.Add(item);
                }
            }
        }

        /// <summary>
        /// Validate Header
        /// </summary>
        /// <param name="paramDto"></param>
        /// <param name="entity"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidateChassisMasterClaimHeader(ChassisMasterClaimHeaderParameterDto paramDto, out ChassisMasterClaimHeader entity, List<DNetValidationResult> validationResults)
        {
            entity = _mapper.Map<ChassisMasterClaimHeader>(paramDto);

            // Validate Dealer            
            Dealer dealer = null;
            if (ValidationHelper.ValidateDealer(paramDto.DealerCode, validationResults, this.DealerCode, ref dealer))
            {
                entity.Dealer = dealer;
            }

            // Validate PODestination            
            PODestination poDestination = null;
            bool isValidateDealerCode = true;
            if (ValidatePODestination(paramDto.PODestinationCode, paramDto.DealerCode, validationResults, ref poDestination, isValidateDealerCode))
            {
                entity.PODestination = poDestination;
            }

            // Validate ChassisMaster            
            ChassisMaster chassisMaster = null;
            if (ValidateChassisMaster(paramDto.ChassisNumber, validationResults, ref chassisMaster))
            {
                entity.ChassisMaster = chassisMaster;
            }

           /* // Validate LogisticCompany            
            ChassisMasterLogisticCompany chassisMasterLogisticCompany = null;
            if (ValidateLogisticCompany(paramDto.LogisticCompanyName, validationResults, ref chassisMasterLogisticCompany))
            {
                entity.ChassisMasterLogisticCompany = chassisMasterLogisticCompany;
            }*/

            // Validate ChassisPODestination            
            PODestination chassisPODestination = null;
            if (ValidateChassisPODestination(paramDto.ChassisNumber, validationResults, ref chassisPODestination))
            {
                entity.ChassisPODestination = chassisPODestination;
            }

            //Validate StatusID
            if (validationResults.Count == 0)
            {
                ValidateStatusIDHeader(paramDto.StatusID, validationResults);
            }

            if(validationResults.Count==0)
            {
                if(paramDto.ClaimDate.ToString("yyyy-MM-dd") !=DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    validationResults.Add(new DNetValidationResult("Claim Date " + paramDto.ClaimDate + " harus sesuai dengan tanggal hari ini"));
                }
            }

            //Validate StatusStockDMS
            if (validationResults.Count == 0)
            {
                ValidateStatusStockDMS(paramDto.StatusStockDMS, validationResults);
            }
            

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate Update Header
        /// </summary>
        /// <param name="paramDto"></param>
        /// <param name="entity"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidateUpdateChassisMasterClaimHeader(ChassisMasterClaimHeaderUpdateParameterDto paramDto, out ChassisMasterClaimHeader entity, List<DNetValidationResult> validationResults)
        {
            entity = _mapper.Map<ChassisMasterClaimHeader>(paramDto);

            // Validate Dealer            
            Dealer dealer = null;
            if (ValidationHelper.ValidateDealer(paramDto.DealerCode, validationResults, this.DealerCode, ref dealer))
            {
                entity.Dealer = dealer;
            }

            // Validate PODestination            
            PODestination poDestination = null;
            bool isValidateDealerCode = true;
            if (ValidatePODestination(paramDto.PODestinationCode, paramDto.DealerCode, validationResults, ref poDestination, isValidateDealerCode))
            {
                entity.PODestination = poDestination;
            }

            // Validate ChassisMaster            
            ChassisMaster chassisMaster = null;
            if (ValidateChassisMaster(paramDto.ChassisNumber, validationResults, ref chassisMaster))
            {
                entity.ChassisMaster = chassisMaster;
            }

           /* // Validate LogisticCompany            
            ChassisMasterLogisticCompany chassisMasterLogisticCompany = null;
            if (ValidateLogisticCompany(paramDto.LogisticCompanyName, validationResults, ref chassisMasterLogisticCompany))
            {
                entity.ChassisMasterLogisticCompany = chassisMasterLogisticCompany;
            }*/

            // Validate ChassisPODestination            
            PODestination chassisPODestination = null;
            if (ValidateChassisPODestination(paramDto.ChassisNumber, validationResults, ref chassisPODestination))
            {
                entity.ChassisPODestination = chassisPODestination;
            }
           
            //Validate StatusStockDMS
            if (validationResults.Count == 0)
            {
                ValidateStatusStockDMS(paramDto.StatusStockDMS, validationResults);
            }
            return validationResults.Count == 0;
        }

        private bool ValidateStatusIDHeader(int statusID, List<DNetValidationResult> validationResults)
        {
            List<StandardCodeDto> _enumChassisClaim = new List<StandardCodeDto>();
            _enumChassisClaim = _enumBL.GetByCategory("StatusClaim");
            if (statusID == (_enumChassisClaim.Where(e => e.ValueCode == "Baru").SingleOrDefault().ValueId))
            { 
                // initialize the mapper
                var _mapper = MapperFactory.GetInstance().GetMapper(typeof(StandardCode).ToString());
                var category = "ChassisMasterClaim.StatusClaim";

                // get by criteria
                CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(StandardCode), "Category", MatchType.Exact, category));
                criteria.opAnd(new Criteria(typeof(StandardCode), "ValueId", MatchType.Exact, statusID));
                // get by criteria
                var masters = _mapper.RetrieveByCriteria(criteria);
                if (masters.Count == 0)
                {
                    validationResults.Add(new DNetValidationResult("Status ID"+ statusID+" tidak terdaftar dalam Standarcode"));
                }
            }
            else
            {
                validationResults.Add(new DNetValidationResult("Status ID "+statusID+" harus 0 (Baru)"));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate passed chassis number parameter
        /// </summary>
        /// <param name="chassisNumber"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        /// <param name="chassisMaster"></param>
        private static bool ValidateChassisMaster(string chassisNumber, List<DNetValidationResult> validationResults, ref ChassisMaster chassisMaster)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMaster).ToString());

            // get by criteria
            CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(ChassisMaster), "ChassisNumber", MatchType.Exact, chassisNumber));

            var masters = _mapper.RetrieveByCriteria(criteria);
            if (masters.Count > 0)
            {
                // cast the object
                chassisMaster = masters[0] as ChassisMaster;
            }
            else
            {
                validationResults.Add(new DNetValidationResult("ChassisNumber "+chassisNumber+" tidak ditemukan"));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate Status Stock DMS
        /// </summary>
        /// <param name="statusStockDMS"></param>
        /// <param name="validationResults"></param>
        private static bool ValidateStatusStockDMS(int statusStockDMS, List<DNetValidationResult> validationResults)
        {
            var category = "ChassisMasterClaim.StatusStokDMS";
            // get by criteria
            var _mapperStandardCode = MapperFactory.GetInstance().GetMapper(typeof(StandardCode).ToString());
            var masters = _mapperStandardCode.RetrieveByCriteria(Helper.GenerateCriteria(typeof(StandardCode), "Category", "ValueId", category, statusStockDMS));
            if (masters.Count == 0)
            {
                validationResults.Add(new DNetValidationResult("statusstockDMS "+statusStockDMS+" tidak terdapat dalam standardcode"));
            }
           
            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate passed logistic company parameter
        /// </summary>
        /// <param name="LogisticCompanyName"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        /// <param name="chassisMasterLogisticCompany"></param>
        private static bool ValidateLogisticCompany(string LogisticCompanyName, List<DNetValidationResult> validationResults, ref ChassisMasterLogisticCompany chassisMasterLogisticCompany)
        {
            if (string.IsNullOrEmpty(LogisticCompanyName))
                return true;

            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMasterLogisticCompany).ToString());

            // get by criteria
            CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(ChassisMasterLogisticCompany), "Name", MatchType.Exact, LogisticCompanyName));

            var masters = _mapper.RetrieveByCriteria(criteria);
            if (masters.Count > 0)
            {
                // cast the object
                chassisMasterLogisticCompany = masters[0] as ChassisMasterLogisticCompany;
            }
            /*else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.Name, LogisticCompanyName)));
            }*/

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate passed po destination parameter
        /// </summary>
        /// <param name="PODestinationCode"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        /// <param name="PODestination"></param>
        private static bool ValidatePODestination(string PODestinationCode, string dealerCode, List<DNetValidationResult> validationResults, ref PODestination poDestination, bool isValidateDealerCode = true)
        {
            /*if (string.IsNullOrEmpty(PODestinationCode))
                return true;
*/
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(PODestination).ToString());

            // get by criteria
            CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(PODestination), "Code", MatchType.Exact, PODestinationCode));
            if (isValidateDealerCode)
            {
                criteria.opAnd(new Criteria(typeof(PODestination), "Dealer.DealerCode", MatchType.Exact, dealerCode));
            }

            var masters = _mapper.RetrieveByCriteria(criteria);
            if (masters.Count > 0)
            {
                // cast the object
                poDestination = masters[0] as PODestination;
            }
            /*else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.Code, PODestinationCode)));
            }*/

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate passed chassis po destination parameter
        /// </summary>
        /// <param name="ChassisNumber"></param>
        /// <param name="validationResults"></param>
        /// <param name="podestinationID"></param>
        private static bool ValidateChassisPODestination(string ChassisNumber, List<DNetValidationResult> validationResults, ref PODestination podestinationID)
        {
            if (string.IsNullOrEmpty(ChassisNumber))
                return true;

            #region initialize mapper
            var _chassisMaster = MapperFactory.GetInstance().GetMapper(typeof(ChassisMaster).ToString());
            var _salesOrder = MapperFactory.GetInstance().GetMapper(typeof(SalesOrder).ToString());
            var _POHeader = MapperFactory.GetInstance().GetMapper(typeof(POHeader).ToString());
            var _PODestination = MapperFactory.GetInstance().GetMapper(typeof(PODestination).ToString());
            #endregion
            // initialize the mapper

            #region Get ChassisMaster SO Number
            // Get Chassis Master SO Number By ChassisNumber
            var sonumbers = _chassisMaster.RetrieveByCriteria(Helper.GenerateCriteria(typeof(ChassisMaster), "ChassisNumber", ChassisNumber));
            if (sonumbers.Count == 0)
            {
                validationResults.Add(new DNetValidationResult("ChassisNumber " + ChassisNumber + " tidak ditemukan"));
                return false;
            }
            var chassismaster = (ChassisMaster)sonumbers[0];
            #endregion

            #region Get SalesOrder
            // Get SalesOrder POHeaderID By SONumber
            var poHeaderID = _salesOrder.RetrieveByCriteria(Helper.GenerateCriteria(typeof(SalesOrder), "SONumber", chassismaster.SONumber));
            if (poHeaderID.Count == 0)
            {
                validationResults.Add(new DNetValidationResult("SONumber " + chassismaster.SONumber + " tidak ditemukan"));
                return false;
            }
            var salesorder = (SalesOrder)poHeaderID[0];
            #endregion

            #region Get POHeader
            // Get POHeader ChassisPODestinationID By POHeaderID
            var PODestinationID = _POHeader.RetrieveByCriteria(Helper.GenerateCriteria(typeof(POHeader), "ID", salesorder.POHeader.ID));
            if (PODestinationID.Count == 0)
            {
                validationResults.Add(new DNetValidationResult("POHeaderID " + salesorder.POHeader.ID + " tidak ditemukan"));
                return false;
            }
            var poheader = (POHeader)PODestinationID[0];
            #endregion

            #region Get PODestination
            // Get POHeader ChassisPODestinationID By POHeaderID
            var poDestination = _PODestination.RetrieveByCriteria(Helper.GenerateCriteria(typeof(PODestination), "ID", poheader.PODestination.ID));
            if (poDestination.Count == 0)
            {
                validationResults.Add(new DNetValidationResult("PODestinationID " + poheader.PODestination.ID + " tidak ditemukan"));
                return false;
            }
            podestinationID = (PODestination)poDestination[0];
            #endregion

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Insert via trans manager
        /// </summary>
        /// <param name="chassisMasterClaimHeader"></param>
        /// <param name="chassisMasterClaimDetail"></param>
        /// <param name="documentUpload"></param>
        /// <returns></returns>
        private ChassisMasterClaimHeader InsertWithTransactionManager(ChassisMasterClaimHeader chassisMasterClaimHeader, List<ChassisMasterClaimDetail> chassisMasterClaimDetail, List<DocumentUpload> documentUpload)
        {
            ChassisMasterClaimHeader result = null;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();
                    /*chassisMasterClaimHeader.PODestination.ID = chassisMasterClaimHeader.ChassisPODestination.ID;*/
                    // add command to insert chassismasterclaim header
                    this._transactionManager.AddInsert(chassisMasterClaimHeader, DNetUserName);

                    // add command to insert chassismasterclaim detail
                    foreach (ChassisMasterClaimDetail item in chassisMasterClaimDetail)
                    {
                        item.ChassisMasterClaimHeader = chassisMasterClaimHeader;
                        this._transactionManager.AddInsert(item, DNetUserName);
                    }

                    string sapFolder = AppConfigs.GetString("SAPFolder");
                    string sapDir = AppConfigs.GetString("CBUReturnClaimDirectory");
                    string destFolder = Path.Combine(sapFolder, sapDir);
                    var pathexisting = "";
                    String[] pathList;
                    var newpathImage = "";

                    // add command to insert chassismasterclaim detail
                    foreach (DocumentUpload item in documentUpload)
                    {
                        //create new directory
                        var path = item.Path;
                        pathList = path.Split('\\');
                        pathexisting = pathList[pathList.Length - 5] + '\\' + pathList[pathList.Length - 4] + '\\' + pathList[pathList.Length - 3] + '\\' + pathList[pathList.Length - 2] + '\\';
                        var claimnumber = chassisMasterClaimHeader.ClaimNumber.Replace("/", "");
                        newpathImage = destFolder + pathexisting + claimnumber;

                        string sourceFile = destFolder + pathexisting + pathList[pathList.Length - 1];
                        string destFile = destFolder + pathexisting + claimnumber + '\\' + pathList[pathList.Length - 1];


                        // set the credentials to access the repository server
                        string user = AppConfigs.GetString("User");
                        string password = AppConfigs.GetString("Password");
                        string webServer = AppConfigs.GetString("WebServer");
                        UserImpersonater imp = new UserImpersonater(user, password, webServer);
                        bool success = false;
                        try
                        {
                            success = imp.Start();
                            if (success)
                            {
                                System.IO.Directory.CreateDirectory(newpathImage);
                                System.IO.File.Copy(sourceFile, destFile, true);

                                imp.Stop();
                            }
                        }
                        finally
                        {
                            imp.Dispose();
                        }


                        item.Path = sapDir + pathexisting + claimnumber + '\\' + pathList[pathList.Length - 1];
                        // default filter to get the Active Row Status only
                        var criterias = new CriteriaComposite(new Criteria(typeof(ChassisMasterClaimHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                        criterias.opAnd(new Criteria(typeof(ChassisMasterClaimHeader), "ClaimNumber", MatchType.Exact, chassisMasterClaimHeader.ClaimNumber));
                        var masters = _chassisMasterClaimHeaderMapper.RetrieveByCriteria(criterias);
                        if (masters.Count > 0)
                        {
                            item.DocRegNumber = GenerateClaimNumber();
                        }
                        else
                        {
                            item.DocRegNumber = chassisMasterClaimHeader.ClaimNumber;
                        }
                        this._transactionManager.AddInsert(item, DNetUserName);
                        bool success_imp = false;
                        try
                        {
                            success_imp = imp.Start();
                            if (success_imp)
                            {
                                System.IO.File.Delete(sourceFile);
                                imp.Stop();
                            }
                        }
                        finally
                        {
                            imp.Dispose();
                        }

                    }

                    this._transactionManager.PerformTransaction();
                    result = chassisMasterClaimHeader;
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
        /// Trans manager handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void InsertWithTransactionManagerHandler(object sender, TransactionManager.OnInsertArgs args)
        {
            // set the object ID from db returned id
            if (args.DomainObject.GetType() == typeof(ChassisMasterClaimHeader))
            {
                ((ChassisMasterClaimHeader)args.DomainObject).ID = args.ID;
                ((ChassisMasterClaimHeader)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(ChassisMasterClaimDetail))
            {
                ((ChassisMasterClaimDetail)args.DomainObject).ID = args.ID;
                ((ChassisMasterClaimDetail)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(DocumentUpload))
            {
                ((DocumentUpload)args.DomainObject).ID = args.ID;
                ((DocumentUpload)args.DomainObject).MarkLoaded();
            }
        }

        /// <summary>
        /// Update ChassisMasterClaim with transaction manager
        /// </summary>
        /// <param name="objDomain"></param>
        /// <returns></returns>
        private int UpdateWithTransactionManager(ChassisMasterClaimHeader chassisMasterClaimHeader, List<ChassisMasterClaimDetail> chassisMasterClaimDetail, List<DocumentUpload> documentUpload, ChassisMasterClaimHeaderUpdateParameterDto objUpdate)
        {
            // set default result
            int result = -1;
            PODestination poDestination = null;
            bool isValidateDealerCode = true;
            List<DNetValidationResult> validationResults = new List<DNetValidationResult>();
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    //check if status ID = "Baru" Update All", if status ID="Validasi" only update StatusID
                    List<StandardCodeDto> _enumChassisClaim = new List<StandardCodeDto>();
                    _enumChassisClaim = _enumBL.GetByCategory("StatusClaim");
                    if (chassisMasterClaimHeader.StatusID == (_enumChassisClaim.Where(e => e.ValueCode == "Baru").SingleOrDefault().ValueId))
                    {
                        if(objUpdate.StatusID == (_enumChassisClaim.Where(e => e.ValueCode == "Baru").SingleOrDefault().ValueId) || objUpdate.StatusID == (_enumChassisClaim.Where(e => e.ValueCode == "Validasi").SingleOrDefault().ValueId))
                        {
                            // add command to update Chassis Master Claim document upload
                            foreach (DocumentUpload document in documentUpload)
                            {
                                document.DocRegNumber = objUpdate.ClaimNumber;
                                if (document.ID != 0)
                                {
                                    _transactionManager.AddUpdate(document, DNetUserName);
                                }
                                else
                                {
                                    _transactionManager.AddInsert(document, DNetUserName);
                                }
                                document.MarkLoaded();
                            }

                            // add command to update Chassis Master Claim detail
                            foreach (ChassisMasterClaimDetail detail in chassisMasterClaimDetail)
                            {
                                detail.ChassisMasterClaimHeader = chassisMasterClaimHeader;
                                if (detail.ID != 0)
                                {
                                    _transactionManager.AddUpdate(detail, DNetUserName);
                                }
                                else
                                {
                                    _transactionManager.AddInsert(detail, DNetUserName);
                                }
                                detail.MarkLoaded();
                            }

                            // add command to update Chassis Master Claim Header
                            chassisMasterClaimHeader.StatusID = objUpdate.StatusID;
                            chassisMasterClaimHeader.ClaimDate = objUpdate.ClaimDate;
                            chassisMasterClaimHeader.DealerPIC = objUpdate.DealerPIC;
                            chassisMasterClaimHeader.DateOccur = objUpdate.DateOccur;
                            chassisMasterClaimHeader.PlaceOccur = objUpdate.PlaceOccur;
                            chassisMasterClaimHeader.StatusStockDMS = objUpdate.StatusStockDMS;
                            chassisMasterClaimHeader.ReporterIssue = objUpdate.ReporterIssue;
                            if (!String.IsNullOrEmpty(objUpdate.PODestinationCode))
                            {
                                ValidatePODestination(objUpdate.PODestinationCode, objUpdate.DealerCode, validationResults, ref poDestination, isValidateDealerCode);
                                if (poDestination != null)
                                {
                                    chassisMasterClaimHeader.PODestination.Code = objUpdate.PODestinationCode;
                                }
                            }
                            //chassisMasterClaimHeader.PODestination.ID = chassisMasterClaimHeader.ChassisPODestination.ID;
                            chassisMasterClaimHeader.Dealer.DealerCode = objUpdate.DealerCode;
                            chassisMasterClaimHeader.ChassisMaster.ChassisNumber = objUpdate.ChassisNumber;
                            _transactionManager.AddUpdate(chassisMasterClaimHeader, DNetUserName);
                        }
                    }
                    else if(chassisMasterClaimHeader.StatusID == (_enumChassisClaim.Where(e => e.ValueCode == "Validasi").SingleOrDefault().ValueId))
                    {
                        if(objUpdate.StatusID== (_enumChassisClaim.Where(e => e.ValueCode == "Baru").SingleOrDefault().ValueId))
                        {
                            chassisMasterClaimHeader.LastUpdateTIme = DateTime.Now;
                            chassisMasterClaimHeader.StatusID = objUpdate.StatusID;
                            _transactionManager.AddUpdate(chassisMasterClaimHeader, DNetUserName);
                        }
                    }
                    else if (chassisMasterClaimHeader.StatusID == (_enumChassisClaim.Where(e => e.ValueCode == "Konfirmasi").SingleOrDefault().ValueId))
                    {
                        if (objUpdate.StatusID == (_enumChassisClaim.Where(e => e.ValueCode == "Konfirmasi").SingleOrDefault().ValueId))
                        {
                            chassisMasterClaimHeader.LastUpdateTIme = DateTime.Now;
                            chassisMasterClaimHeader.StatusStockDMS = objUpdate.StatusStockDMS;
                            _transactionManager.AddUpdate(chassisMasterClaimHeader, DNetUserName);
                        }
                    }
                    else if (chassisMasterClaimHeader.StatusID == (_enumChassisClaim.Where(e => e.ValueCode == "Proses").SingleOrDefault().ValueId))
                    {
                        if (objUpdate.StatusID == (_enumChassisClaim.Where(e => e.ValueCode == "Proses").SingleOrDefault().ValueId))
                        {
                            chassisMasterClaimHeader.LastUpdateTIme = DateTime.Now;
                            chassisMasterClaimHeader.StatusStockDMS = objUpdate.StatusStockDMS;
                            _transactionManager.AddUpdate(chassisMasterClaimHeader, DNetUserName);
                        }
                    }
                    else if (chassisMasterClaimHeader.StatusID == (_enumChassisClaim.Where(e => e.ValueCode == "Revisi").SingleOrDefault().ValueId))
                    {
                        if (objUpdate.StatusID == (_enumChassisClaim.Where(e => e.ValueCode == "Validasi").SingleOrDefault().ValueId))
                        {
                            // add command to update Chassis Master Claim document upload
                            foreach (DocumentUpload document in documentUpload)
                            {
                                if (document.ID != 0)
                                {
                                    _transactionManager.AddUpdate(document, DNetUserName);
                                }
                                else
                                {
                                    _transactionManager.AddInsert(document, DNetUserName);
                                }
                                document.MarkLoaded();
                            }

                            // add command to update Chassis Master Claim detail
                            foreach (ChassisMasterClaimDetail detail in chassisMasterClaimDetail)
                            {
                                detail.ChassisMasterClaimHeader = chassisMasterClaimHeader;
                                if (detail.ID != 0)
                                {
                                    _transactionManager.AddUpdate(detail, DNetUserName);
                                }
                                else
                                {
                                    _transactionManager.AddInsert(detail, DNetUserName);
                                }
                                detail.MarkLoaded();
                            }

                            // add command to update Chassis Master Claim Header
                            chassisMasterClaimHeader.StatusID = objUpdate.StatusID;
                            chassisMasterClaimHeader.ClaimDate = objUpdate.ClaimDate;
                            chassisMasterClaimHeader.DealerPIC = objUpdate.DealerPIC;
                            chassisMasterClaimHeader.DateOccur = objUpdate.DateOccur;
                            chassisMasterClaimHeader.PlaceOccur = objUpdate.PlaceOccur;
                            chassisMasterClaimHeader.ReporterIssue = objUpdate.ReporterIssue;
                            if (!String.IsNullOrEmpty(objUpdate.PODestinationCode))
                            {
                                ValidatePODestination(objUpdate.PODestinationCode, objUpdate.DealerCode, validationResults, ref poDestination, isValidateDealerCode);
                                if (poDestination != null)
                                {
                                    chassisMasterClaimHeader.PODestination.Code = objUpdate.PODestinationCode;
                                }
                            }
                            //chassisMasterClaimHeader.PODestination.ID = chassisMasterClaimHeader.ChassisPODestination.ID;
                            chassisMasterClaimHeader.Dealer.DealerCode = objUpdate.DealerCode;
                            /*chassisMasterClaimHeader.ChassisMasterLogisticCompany.Name = objUpdate.LogisticCompanyName;*/
                            chassisMasterClaimHeader.ChassisMaster.ChassisNumber = objUpdate.ChassisNumber;
                            _transactionManager.AddUpdate(chassisMasterClaimHeader, DNetUserName);
                        }
                    }

                    _transactionManager.PerformTransaction();
                    result = chassisMasterClaimHeader.ID;
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
        /// Validate Update Status ID
        /// </summary>
        /// <param name="statusID"></param>
        /// <param name="statusIDexisting"></param>
        /// <returns></returns>
        private bool ValidateUpdateStatusID(int statusID, int statusIDexisting, List<DNetValidationResult> validationResults, out string msg)
        {
            msg = String.Empty;
            List<StandardCodeDto> _enumChassisClaim = new List<StandardCodeDto>();
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(StandardCode).ToString());
            var category = "ChassisMasterClaim.StatusClaim";
            _enumChassisClaim = _enumBL.GetByCategory("StatusClaim");
            // get by criteria
            CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(StandardCode), "Category", MatchType.Exact, category));
            criteria.opAnd(new Criteria(typeof(StandardCode), "ValueId", MatchType.Exact, statusID));
            // get by criteria
            var masters = _mapper.RetrieveByCriteria(criteria);
            bool success = false;
            if (masters.Count == 0)
            {
                validationResults.Add(new DNetValidationResult("StatusID " + statusID + " tidak tersedia di standardcode"));
                msg = "StatusID " + statusID + " tidak tersedia di standardcode";
            }
            else
            {
                if (statusIDexisting == (_enumChassisClaim.Where(e => e.ValueCode == "Baru").SingleOrDefault().ValueId) && (statusID == (_enumChassisClaim.Where(e => e.ValueCode == "Baru").SingleOrDefault().ValueId) || statusID == (_enumChassisClaim.Where(e => e.ValueCode == "Validasi").SingleOrDefault().ValueId)))
                {
                    success = true;
                }
                else if (statusIDexisting == (_enumChassisClaim.Where(e => e.ValueCode == "Validasi").SingleOrDefault().ValueId) && statusID == (_enumChassisClaim.Where(e => e.ValueCode == "Baru").SingleOrDefault().ValueId))
                {
                    success = true;
                }
                else if (statusIDexisting == (_enumChassisClaim.Where(e => e.ValueCode == "Konfirmasi").SingleOrDefault().ValueId) && statusID == (_enumChassisClaim.Where(e => e.ValueCode == "Konfirmasi").SingleOrDefault().ValueId))
                {
                    success = true;
                }
                else if (statusIDexisting == (_enumChassisClaim.Where(e => e.ValueCode == "Proses").SingleOrDefault().ValueId) && statusID == (_enumChassisClaim.Where(e => e.ValueCode == "Proses").SingleOrDefault().ValueId))
                {
                    success = true;
                }
                else if (statusIDexisting == (_enumChassisClaim.Where(e => e.ValueCode == "Revisi").SingleOrDefault().ValueId) && statusID == (_enumChassisClaim.Where(e => e.ValueCode == "Validasi").SingleOrDefault().ValueId))
                {
                    success = true;
                }
                else
                {
                    var valueStatusIDExisting = (_enumChassisClaim.Where(e => e.ValueId == statusIDexisting).SingleOrDefault().ValueCode);
                    var valueStatusIDUpdate = (_enumChassisClaim.Where(e => e.ValueId == statusID).SingleOrDefault().ValueCode);
                    validationResults.Add(new DNetValidationResult("Tidak dapat di update menjadi " + valueStatusIDUpdate + " karena status di DNET sudah " + valueStatusIDExisting));
                    msg = "Tidak dapat di update menjadi " + valueStatusIDUpdate + " karena status di DNET sudah " + valueStatusIDExisting;
                }
            }

            return validationResults.Count == 0;
        }

        ResponseBase<ChassisMastertClaimHeaderDto> IBaseInterface<ChassisMasterClaimHeaderParameterDto, ChassisMasterClaimHeaderFilterDto, ChassisMastertClaimHeaderDto>.Update(ChassisMasterClaimHeaderParameterDto objUpdate)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
