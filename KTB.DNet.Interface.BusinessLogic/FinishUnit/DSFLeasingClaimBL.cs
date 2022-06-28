
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Repository.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Web.Configuration;


namespace KTB.DNet.Interface.BusinessLogic
{
    public class DSFLeasingClaimBL : AbstractBusinessLogic, IDSFLeasingClaimBL
    {
        #region Variables
        private readonly IDSFLeasingClaimRepository<DSFLeasingClaim, int> _repo;
        private readonly IDSFLeasingClaimDocumentRepository<DSFLeasingClaimDocument, int> _repoDSFLeasingClaimDocument;

        private readonly AutoMapper.IMapper _mapper;
        private TransactionManager _transactionManager;
        private readonly IMapper _benefitClaimHeaderMapper;
        private readonly IMapper _chassisMasterMapper;
        private readonly IMapper _dealerMapper;
        private readonly IMapper _dSFLeasingClaimMapper;
        private readonly IMapper _endCustomerMapper;
        private readonly IMapper _vechileColorMapper;
        private readonly IMapper _benefitClaimDetailsMapper;
        private readonly IMapper _dSFLeasingClaimDocumentMapper;
        private readonly IMapper _statusChangeHistoryMapper;
        #endregion

        #region Constructor
        public DSFLeasingClaimBL(
            IDSFLeasingClaimRepository<DSFLeasingClaim, int> repo, 
            IDSFLeasingClaimDocumentRepository<DSFLeasingClaimDocument, int> repoDSFLeasingClaimDocument)
        {
            _repo = repo;
            _repoDSFLeasingClaimDocument = repoDSFLeasingClaimDocument;
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _dSFLeasingClaimMapper = MapperFactory.GetInstance().GetMapper(typeof(DSFLeasingClaim).ToString());
            _dSFLeasingClaimDocumentMapper = MapperFactory.GetInstance().GetMapper(typeof(DSFLeasingClaimDocument).ToString());
            _benefitClaimHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(BenefitClaimHeader).ToString());
            _chassisMasterMapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMaster).ToString());
            _dealerMapper = MapperFactory.GetInstance().GetMapper(typeof(Dealer).ToString());
            _endCustomerMapper = MapperFactory.GetInstance().GetMapper(typeof(EndCustomer).ToString());
            _vechileColorMapper = MapperFactory.GetInstance().GetMapper(typeof(VechileColor).ToString());
            _benefitClaimDetailsMapper = MapperFactory.GetInstance().GetMapper(typeof(BenefitClaimDetails).ToString());
            _statusChangeHistoryMapper = MapperFactory.GetInstance().GetMapper(typeof(StatusChangeHistory).ToString());
            _transactionManager = new TransactionManager();
            _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);
        }
        #endregion

        #region Public Methods

        public bool Update(List<ResubmitClaimParamater> dataFromClient, out List<ResubmitClaimResponse> dataForResponse)
        {
            dataForResponse = new List<ResubmitClaimResponse>();
            DoUpdate(dataFromClient, out dataForResponse);
            return 1 == 1;
        }

        private void DoUpdate(List<ResubmitClaimParamater> dataFromClient, out List<ResubmitClaimResponse> dataForResponse)
        {
            List<ResubmitClaimResponse> _dataForResponse = new List<ResubmitClaimResponse>();
            dataFromClient.ForEach(i => {
                try
                {

                    DSFLeasingClaim objDSFLeasingClaim = new DSFLeasingClaim();
                    DoUpdateDSFLeasingClaim(i, out objDSFLeasingClaim);
                    if (objDSFLeasingClaim != null)
                    {
                        DoInsertDSFLeasingClaimDocument(i.EvidenceFiles, objDSFLeasingClaim);
                        _dataForResponse.Add(new ResubmitClaimResponse()
                        {
                            Message = "Success",
                            Result = "Success",
                            RegNumber = i.RegNumber
                        });
                    }
                    else
                    {
                        _dataForResponse.Add(new ResubmitClaimResponse()
                        {
                            Message = string.Format("Claim reg number {0} tidak ditemukan.", i.RegNumber),
                            Result = "Failed",
                            RegNumber = i.RegNumber
                        });
                    }
                }
                catch(Exception ex)
                {
                    _dataForResponse.Add(new ResubmitClaimResponse() {
                        Message = ex.Message,
                        Result = "Not Success",
                        RegNumber = i.RegNumber
                    });
                }
            });
            dataForResponse = _dataForResponse;
        }
        private void DoUpdateDSFLeasingClaim(ResubmitClaimParamater data, out DSFLeasingClaim dSFLeasingClaim)
        {
            CriteriaComposite crit = new CriteriaComposite(new Criteria(typeof(DSFLeasingClaim), "RowStatus", MatchType.Exact, (int)DBRowStatus.Active));
            crit.opAnd(new Criteria(typeof(DSFLeasingClaim), "RegNumber", MatchType.Exact, data.RegNumber));
            DSFLeasingClaim objDSFLeasingClaim = _repo.Search(crit).Cast<DSFLeasingClaim>().FirstOrDefault();

            if (objDSFLeasingClaim != null)
            {
                ChassisMaster objChassisMaster = (ChassisMaster)_chassisMasterMapper.Retrieve(int.Parse(objDSFLeasingClaim.ChassisMasterID));
                Dealer objDealer = (Dealer)_dealerMapper.Retrieve(objChassisMaster.Dealer.ID);
                short oldStatus = objDSFLeasingClaim.Status;
                objDSFLeasingClaim.Status = (short)data.Status;
                objDSFLeasingClaim.RemarkByDSF = data.RemarkByDSF;
                objDSFLeasingClaim.Dealer = objDealer;
                _repo.Update(objDSFLeasingClaim);
                DoInsertStatusChangeHistory(oldStatus, objDSFLeasingClaim);
            }
            dSFLeasingClaim = objDSFLeasingClaim;
        }
        private void DoInsertStatusChangeHistory(short oldStatus, DSFLeasingClaim dSFLeasingClaim)
        {
            StatusChangeHistory sch = new StatusChangeHistory()
            {
                DocumentType = 15,
                DocumentRegNumber = dSFLeasingClaim.RegNumber,
                OldStatus = oldStatus,
                NewStatus = dSFLeasingClaim.Status,
                RowStatus = 0,
                CreatedBy = "SYSTEM",
                CreatedTime = DateTime.Now,
                LastUpdateBy = "SYSTEM",
                LastUpdateTime = DateTime.Now
            };
            _statusChangeHistoryMapper.Insert(sch, "SYSTEM");
        }
        private void DoInsertDSFLeasingClaimDocument(List<ResubmitClaimEvidenceFileParamater> data, DSFLeasingClaim dSFLeasingClaim)
        {
            List<DSFLeasingClaimDocument> listObj = new List<DSFLeasingClaimDocument>();
            data.ForEach(i => {
                string filePath = AppConfigs.GetString("DSFClaimDocumentDirectory") + dSFLeasingClaim.Dealer.DealerCode + @"\" + TimeStamp() + Path.GetExtension(i.FileName);
                DSFLeasingClaimDocument obj = new DSFLeasingClaimDocument() {
                    DSFLeasingClaim = dSFLeasingClaim,
                    FileDescription = i.FileDescription,
                    FileName = i.FileName,
                    Path = filePath
                };
                if (_repoDSFLeasingClaimDocument.Save(obj))
                {
                    SaveFile(i, filePath);
                }
            });
        }
        private void SaveFile(ResubmitClaimEvidenceFileParamater evidence, string filePath)
        {
            byte[] bytes = Convert.FromBase64String(evidence.Base64OfStreamFile);
            UserImpersonater imp = GetImpersonater();
            bool success = imp.Start();
            FileStream fs = null;
            if (success)
            {
                filePath = Path.Combine(AppConfigs.GetString("SAN"), filePath);
                FileInfo fi = new FileInfo(filePath);
                if (!fi.Directory.Exists)
                {
                    Directory.CreateDirectory(fi.DirectoryName);
                }
                fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
                imp.Stop();
            }
        }

        private string TimeStamp()
        {
            return DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
        }

        public bool Insert(List<DSFLeasingClaimCreateParameter> dataFromClient, out List<DSFLeasingClaimCreateResponse> dataForResponse)
        {
            dataForResponse = new List<DSFLeasingClaimCreateResponse>();
            List<DSFLeasingClaim> listForInsert = new List<DSFLeasingClaim>();
            DataValidating(dataFromClient, out dataForResponse, out listForInsert);
            DoInsert(listForInsert);
            return 1 == 1;
        }

        public FileStream GetFile(string regnumber, string path, out string filename){
            
            UserImpersonater imp = GetImpersonater();
            filename = string.Empty;

            CriteriaComposite crit = new CriteriaComposite(new Criteria(typeof(DSFLeasingClaim), "RowStatus", MatchType.Exact, (int)DBRowStatus.Active));
            crit.opAnd(new Criteria(typeof(DSFLeasingClaim), "RegNumber", MatchType.Exact, regnumber.Trim()));
            DSFLeasingClaim objDSFLeasingClaim = _repo.Search(crit).Cast<DSFLeasingClaim>().FirstOrDefault();

            CriteriaComposite crit2 = new CriteriaComposite(new Criteria(typeof(DSFLeasingClaimDocument), "RowStatus", MatchType.Exact, (int)DBRowStatus.Active));
            crit2.opAnd(new Criteria(typeof(DSFLeasingClaimDocument), "DSFLeasingClaim.ID", MatchType.Exact, objDSFLeasingClaim.ID));
            crit2.opAnd(new Criteria(typeof(DSFLeasingClaimDocument), "Path", MatchType.Exact, path.Trim().Replace(@"\\",@"\")));
            DSFLeasingClaimDocument objDSFLeasingClaimDocument = _repo.ListDSFLeasingClaimDocument(crit2).Cast<DSFLeasingClaimDocument>().FirstOrDefault();

            bool success = imp.Start();
            FileStream fs = null;
            if (success)
            {
                string filePath = Path.Combine(AppConfigs.GetString("SAN"), objDSFLeasingClaimDocument.Path);
                if (!File.Exists(filePath))
                {
                    return null;
                }
                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                filename = objDSFLeasingClaimDocument.FileName;
                imp.Stop();
            }
            return fs;
        }


        //Backup
        //public ResponseBase<List<DSFLeasingClaimDAPPDto>> ReadLeasingDAPP(DSFLeasingClaimDAPPFilterDto filterDto, int pageSize)
        //{
        //    var result = new ResponseBase<List<DSFLeasingClaimDAPPDto>>();
        //    var sortColl = new SortCollection();
        //    int totalRow = 0;
        //    string rawSql = string.Empty;

        //    try
        //    {
        //        var criteria = Helper.BuildCriteria(typeof(VW_DSFLeasingClaimDAPP), filterDto);
        //        sortColl = Helper.UpdateSortColumn(typeof(VW_DSFLeasingClaimDAPP), filterDto, sortColl);

        //        var dataResult = _vW_DSFLeasingClaimDAPPMapper.RetrieveByCriteria(criteria, sortColl, filterDto.pages, pageSize, ref totalRow);
        //        if (dataResult != null && dataResult.Count > 0)
        //        {
        //            var list = dataResult.Cast<VW_DSFLeasingClaimDAPP>().ToList();
        //            var listData = list.Select(item => _mapper.Map<DSFLeasingClaimDAPPDto>(item)).ToList();

        //            result.lst = listData;
        //            result.total = totalRow;
        //        }
        //        else
        //        {
        //            ErrorMsgHelper.DataNotFound(result.messages, typeof(DSFLeasingClaim), filterDto);
        //        }

        //        result.success = true;
        //    }
        //    catch (SqlException ex)
        //    {
        //        ErrorMsgHelper.SqlExceptionRead(result.messages, ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMsgHelper.Exception(result.messages, ex.Message);
        //    }

        //    return result;
        //}


        public ResponseBase<List<DSFLeasingClaimDAPPDto>> ReadLeasingDAPP(string _status, string _sourceData, string _lastUpdateTime)
        {
            var result = new ResponseBase<List<DSFLeasingClaimDAPPDto>>();
            var sortColl = new SortCollection();
            string rawSql = string.Empty;

            DateTime _lastUpdateTimes = DateTime.Now;

            try
            {
                _lastUpdateTimes = Convert.ToDateTime(_lastUpdateTime);
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.Exception(result.messages, ex.Message);
            }

            DateTime dtStart = new DateTime(_lastUpdateTimes.Year, _lastUpdateTimes.Month, _lastUpdateTimes.Day, 0, 0, 0);
            DateTime dtEnd = new DateTime(_lastUpdateTimes.Year, _lastUpdateTimes.Month, _lastUpdateTimes.Day, 0, 0, 0);
            dtEnd = dtEnd.AddDays(1);

            try
            {
                CriteriaComposite crit = new CriteriaComposite(new Criteria(typeof(DSFLeasingClaimDocument), "RowStatus", MatchType.Exact, (int)DBRowStatus.Active));
                if (_status != null || _status.ToString() != "")
                {
                    crit.opAnd(new Criteria(typeof(DSFLeasingClaimDocument), "DSFLeasingClaim.Status", MatchType.Exact, _status.ToString()));
                }
                if (_lastUpdateTimes != null)
                {
                    crit.opAnd(new Criteria(typeof(DSFLeasingClaimDocument), "DSFLeasingClaim.LastUpdateTime", MatchType.GreaterOrEqual, dtStart));
                    crit.opAnd(new Criteria(typeof(DSFLeasingClaimDocument), "DSFLeasingClaim.LastUpdateTime", MatchType.Lesser, dtEnd));
                }
                if (_sourceData != null || _sourceData.ToString() != "")
                {
                    crit.opAnd(new Criteria(typeof(DSFLeasingClaimDocument), "SourceData", MatchType.Exact, _sourceData));
                }
                var lstDSFLeasingClaimDocument = _dSFLeasingClaimDocumentMapper.RetrieveByCriteria(crit);
                if (lstDSFLeasingClaimDocument != null && lstDSFLeasingClaimDocument.Count > 0)
                {
                    var listData = new List<DSFLeasingClaimDAPPDto>();
                    foreach (DSFLeasingClaimDocument obj in lstDSFLeasingClaimDocument)
                    {
                        var objDto = new DSFLeasingClaimDAPPDto();
                        objDto.RegNumber = obj.DSFLeasingClaim.RegNumber;
                        objDto.RemarkByDealer = obj.DSFLeasingClaim.RemarkByDealer;
                        objDto.FileName = obj.FileName;
                        objDto.FileDescription = obj.FileDescription;
                        objDto.Path = obj.Path;
                        listData.Add(objDto);
                    }
                    result.lst = listData;
                    result.total = listData.Count;
                }
                else
                {
                    ErrorMsgHelper.SqlExceptionRead(result.messages, "Silahkan isi kriteria pencarian yang sesuai.");
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
        private void DataValidating(List<DSFLeasingClaimCreateParameter> dataToValidate, out List<DSFLeasingClaimCreateResponse> dataForResponse, out List<DSFLeasingClaim> dataForInsert)
        {
            dataForResponse = new List<DSFLeasingClaimCreateResponse>();
            dataForInsert = new List<DSFLeasingClaim>();
            List<DSFLeasingClaimCreateResponse> listDataResponse = new List<DSFLeasingClaimCreateResponse>();
            List<DSFLeasingClaim> listForInsert = new List<DSFLeasingClaim>();
            DSFLeasingClaimCreateResponse objDSFLeasingClaimCreateResponse = new DSFLeasingClaimCreateResponse();
            DSFLeasingClaim objDSFLeasingClaim = new DSFLeasingClaim();
            int seq = 1;
            dataToValidate.ForEach(i => {
                DoValidating(i, out objDSFLeasingClaimCreateResponse, out objDSFLeasingClaim);
                bool isValidToSave = objDSFLeasingClaimCreateResponse.RemarksCode == "00" ? true : objDSFLeasingClaimCreateResponse.RemarksCode == "04"? true:false;
                if (isValidToSave)
                {
                    objDSFLeasingClaim.RegNumber = SetRegNumber(seq);
                    objDSFLeasingClaimCreateResponse.RegNumber = objDSFLeasingClaim.RegNumber;
                    listForInsert.Add(objDSFLeasingClaim);
                    seq += 1;
                }
                listDataResponse.Add(objDSFLeasingClaimCreateResponse);
            });
            dataForInsert.AddRange(listForInsert);
            dataForResponse.AddRange(listDataResponse);
        }
        private void DoValidating(DSFLeasingClaimCreateParameter data, out DSFLeasingClaimCreateResponse result, out DSFLeasingClaim dSFLeasingClaim)
        {
            dSFLeasingClaim = new DSFLeasingClaim();
            result = new DSFLeasingClaimCreateResponse();
            CriteriaComposite critChassisMaster = new CriteriaComposite(new Criteria(typeof(ChassisMaster), "RowStatus", MatchType.Exact, (int)DBRowStatus.Active));
            CriteriaComposite critEndCustomer = new CriteriaComposite(new Criteria(typeof(EndCustomer), "RowStatus", MatchType.Exact, (int)DBRowStatus.Active));
            CriteriaComposite critVechileColor = new CriteriaComposite(new Criteria(typeof(VechileColor), "RowStatus", MatchType.Exact, (int)DBRowStatus.Active));
            critChassisMaster.opAnd(new Criteria(typeof(ChassisMaster), "ChassisNumber", MatchType.Exact, data.ChassisNumber));
            critChassisMaster.opOr(new Criteria(typeof(ChassisMaster), "EngineNumber", MatchType.Exact, data.EngineNumber));
            ChassisMaster objChassisMaster = _chassisMasterMapper.RetrieveByCriteria(critChassisMaster).Cast<ChassisMaster>().FirstOrDefault();
            
            if (objChassisMaster != null)
            {
                critEndCustomer.opAnd(new Criteria(typeof(EndCustomer), "ID", MatchType.Exact, objChassisMaster.EndCustomerID));
                EndCustomer objEndCustomer = (EndCustomer)_endCustomerMapper.Retrieve(objChassisMaster.EndCustomerID != null ? objChassisMaster.EndCustomerID : 0);
                VechileColor objVechileColor = (VechileColor)_vechileColorMapper.Retrieve(objChassisMaster.VechileColor.ID);
                Dealer dealer = (Dealer)_dealerMapper.Retrieve(objChassisMaster.Dealer.ID);
                if (objChassisMaster.ChassisNumber != "" && objChassisMaster.ChassisNumber.Trim() == data.ChassisNumber.Trim())
                {
                    if (objChassisMaster.EngineNumber != "" && string.Concat(objChassisMaster.EngineNumber.Trim().Where(c => !char.IsWhiteSpace(c))).Replace("-", "") == data.EngineNumber.Trim())
                    {
                        if (!IsExistChassisMaster(objChassisMaster.ID))
                        {
                            string[] resultRemark = RetrieveUploadRemark(objChassisMaster.ChassisNumber, objChassisMaster.EngineNumber);
                            short _Status = 0;
                            switch(resultRemark[1].ToString()){
                                case "00":
                                    _Status = 2;
                                    break;
                                case "04":
                                    _Status = 1;
                                    break;
                            }
                            result.RemarksDescription = resultRemark[0];
                            dSFLeasingClaim.ChassisMaster = objChassisMaster;
                            result.ChassisNumber = objChassisMaster.ChassisNumber;
                            result.EngineNumber = objChassisMaster.EngineNumber;
                            dSFLeasingClaim.Dealer = dealer;
                            dSFLeasingClaim.Status = _Status;
                            if (objEndCustomer != null)
                            {
                                dSFLeasingClaim.CustomerName = objEndCustomer.Name1;
                            }
                            dSFLeasingClaim.Unit = 1;
                            dSFLeasingClaim.ObjectLease = objVechileColor.MaterialDescription;
                            dSFLeasingClaim.ClaimDate = DateTime.Now;
                            dSFLeasingClaim.AssetSeqNo = data.AssetSeqNo;
                            dSFLeasingClaim.AgreementNo = data.AgreementNumber;
                            dSFLeasingClaim.SKDNumber = data.SKDNumber;
                            dSFLeasingClaim.SKDDate = data.SKDDate;
                            dSFLeasingClaim.SKDApprovalDate = data.SKDApprovalDate;
                            dSFLeasingClaim.GoLiveDate = data.GoLiveDate;
                            dSFLeasingClaim.ATPMSubsidy = data.ATPMSubsidy;
                            dSFLeasingClaim.SupplierName = data.SupplierName;
                            dSFLeasingClaim.ProgramName = data.ProgramName;
                            dSFLeasingClaim.CollectionPeriodMonth = byte.Parse(data.CollectionPeriodMonth.ToString());
                            dSFLeasingClaim.CollectionPeriodYear = short.Parse(data.CollectionPeriodYear.ToString());
                            dSFLeasingClaim.TotalDP = data.TotalDownPayment;
                            dSFLeasingClaim.TotalAmountLease = data.TotalAmountLease;
                            dSFLeasingClaim.PeriodLease = data.PeriodLease;
                            dSFLeasingClaim.InterestLease = data.InterestLease;
                            dSFLeasingClaim.Insurance = data.Insurance;
                            dSFLeasingClaim.TypeInsurance = data.TypeInsurance;
                            dSFLeasingClaim.RowStatus = 0;
                            dSFLeasingClaim.CreatedBy = "ADMIN_DSF";
                            dSFLeasingClaim.CreatedTime = DateTime.Now;
                            dSFLeasingClaim.LastUpdateBy = "ADMIN_DSF";
                            dSFLeasingClaim.LastUpdateTime = DateTime.Now;
                            result.RemarksCode = resultRemark[1].ToString();
                            result.RemarksDescription = resultRemark[0];
                            result.DealerCode = dealer.DealerCode;
                            result.DealerName = dealer.DealerName;
                            result.AssetSeqNo = data.AssetSeqNo;
                            result.AgreementNumber = data.AgreementNumber;
                            result.ClaimDate = DateTime.Now;
                            result.CustomerName = objEndCustomer != null ? objEndCustomer.Name1 : "";
                            result.Unit = 1;
                            result.ObjectLease = objVechileColor.MaterialDescription;
                        }
                        else
                        {
                            result.RemarksCode = "07";
                            result.RemarksDescription = "Claim sudah pernah diupload.";
                            result.ChassisNumber = objChassisMaster.ChassisNumber;
                            result.EngineNumber = objChassisMaster.EngineNumber;
                            result.DealerCode = dealer.DealerCode;
                            result.DealerName = dealer.DealerName;
                            result.AssetSeqNo = data.AssetSeqNo;
                            result.AgreementNumber = data.AgreementNumber;
                            result.ClaimDate = DateTime.Now;
                            result.CustomerName = objEndCustomer != null ? objEndCustomer.Name1 : "";
                            result.Unit = 1;
                            result.ObjectLease = objVechileColor.MaterialDescription;
                        }
                    }
                    else
                    {
                        result.ChassisNumber = objChassisMaster.ChassisNumber;
                        result.EngineNumber = data.EngineNumber;
                        result.RemarksCode = "02";
                        result.RemarksDescription = "Nomor Mesin tidak valid.";
                        result.DealerCode = null;
                        result.DealerName = null;
                        result.AssetSeqNo = data.AssetSeqNo;
                        result.AgreementNumber = data.AgreementNumber;
                        result.ClaimDate = DateTime.Now;
                        result.CustomerName = null;
                        result.Unit = 1;
                        result.ObjectLease = null;
                    }
                }
                else
                {
                    result.ChassisNumber = data.ChassisNumber;
                    result.RemarksCode = "01";
                    result.RemarksDescription = "Nomor Rangka tidak valid.";
                    if (objChassisMaster.EngineNumber != "" && objChassisMaster.EngineNumber.Trim() == data.EngineNumber.Trim())
                    {
                        result.EngineNumber = objChassisMaster.EngineNumber;
                    }
                    else
                    {
                        result.EngineNumber = data.EngineNumber;
                    }
                    result.DealerCode = null;
                    result.DealerName = null;
                    result.AssetSeqNo = data.AssetSeqNo;
                    result.AgreementNumber = data.AgreementNumber;
                    result.ClaimDate = DateTime.Now;
                    result.CustomerName = null;
                    result.Unit = 1;
                    result.ObjectLease = null;
                }
            }
            else
            {
                result.RemarksCode = "03";
                result.RemarksDescription = "Nomor Rangka dan Nomor Mesin tidak valid.";
                result.ChassisNumber = data.ChassisNumber;
                result.EngineNumber = data.EngineNumber;
                result.DealerCode = null;
                result.DealerName = null;
                result.AssetSeqNo = data.AssetSeqNo;
                result.AgreementNumber = data.AgreementNumber;
                result.ClaimDate = DateTime.Now;
                result.CustomerName = null;
                result.Unit = 1;
                result.ObjectLease = null;
            }
            
        }
        private bool IsExistChassisMaster(int chassisMasterID)
        {
            bool result = new bool();
            CriteriaComposite crit = new CriteriaComposite(new Criteria(typeof(DSFLeasingClaim), "RowStatus", MatchType.Exact, (int)DBRowStatus.Active));
            crit.opAnd(new Criteria(typeof(DSFLeasingClaim), "ChassisMaster.ID", MatchType.Exact, chassisMasterID));
            DSFLeasingClaim objDSFLeasingClaim = _repo.Search(crit).Cast<DSFLeasingClaim>().FirstOrDefault();
            result = objDSFLeasingClaim == null ? false : true;
            return result; 
        }
        private bool IsAlreadyClaimByDealer(int chassisMasterID)
        {
            bool result = new bool();
            CriteriaComposite crit = new CriteriaComposite(new Criteria(typeof(BenefitClaimDetails), "RowStatus", MatchType.Exact, (int)DBRowStatus.Active));
            crit.opAnd(new Criteria(typeof(BenefitClaimDetails), "ChassisMaster.ID", MatchType.Exact, chassisMasterID));
            BenefitClaimDetails objBenefitClaimDetails = _benefitClaimDetailsMapper.RetrieveByCriteria(crit).Cast<BenefitClaimDetails>().FirstOrDefault();
            result = objBenefitClaimDetails == null ? false : true;
            return result; 
        }
        private string[] RetrieveUploadRemark(string chassisNumber, string engineNumber)
        {
            string[] _result = new string[2];
            DataSet dtbs = new DataSet();
            string strSPName = "up_GetRemarkDSFClaimUpload  ";
            List<SqlParameter> _params = new List<SqlParameter>();
            _params.Add(new SqlParameter("@chassisNumber ", chassisNumber));
            _params.Add(new SqlParameter("@EngineNumber", engineNumber));

            dtbs = _dSFLeasingClaimMapper.RetrieveDataSet(strSPName, new ArrayList(_params));

            if(dtbs.Tables.Count > 0)
            {
                if(dtbs.Tables[1].Rows.Count > 0){
                    _result[0] = dtbs.Tables[1].Rows[0][0].ToString();
                    _result[1] = dtbs.Tables[1].Rows[0][1].ToString();
                }
            }
            return _result;
        }
        private void DoInsert(List<DSFLeasingClaim> dataFor)
        {
            _repo.Save(dataFor);
        }
        private UserImpersonater GetImpersonater()
        {
            string user = AppConfigs.GetString("User");
            string password = AppConfigs.GetString("Password");
            string webServer = AppConfigs.GetString("WebServer");
            UserImpersonater imp = new UserImpersonater(user, password, webServer);
            return imp;
        }
        #endregion

        
        private string SetRegNumber(int seq)
        {
            string result = "";
            string prefix = "DSFCL";
            int sSeqLength = 0;
            string numSegment = "";
            DateTime TempToDate = DateTime.Now.AddMonths(1);

            DateTime ToDate = new DateTime(TempToDate.Year, TempToDate.Month, 1);

            CriteriaComposite crit = new CriteriaComposite(new Criteria(typeof(DSFLeasingClaim), "RegNumber", MatchType.StartsWith, prefix));
            crit.opAnd(new Criteria(typeof(DSFLeasingClaim), "CreatedTime", MatchType.GreaterOrEqual, DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString("d2") + "01") );

            crit.opAnd(new Criteria(typeof(DSFLeasingClaim), "CreatedTime", MatchType.Lesser, ToDate.ToString("yyyyMMdd")));
            List<DSFLeasingClaim> arrl = _repo.Search(crit).Cast<DSFLeasingClaim>().ToList();
            if (arrl.Count > 0)
            {
                DSFLeasingClaim objBH = arrl.OrderByDescending(i => i.RegNumber).ToList<DSFLeasingClaim>().FirstOrDefault();
                string noReg  = objBH.RegNumber;
                int lastNum = int.Parse((noReg.Substring(noReg.Length - (noReg.Length - 9))));
                seq = lastNum + seq;
                sSeqLength = seq.ToString().Length;
            }
            else { 
                sSeqLength = seq.ToString().Length; 
            }
            switch(sSeqLength){
                case 1:
                    numSegment = "0000" + seq.ToString();
                    break;
                case 2:
                    numSegment = "000" + seq.ToString();
                    break;
                case 3:
                    numSegment = "00" + seq.ToString();
                    break;
                case 4:
                    numSegment = "0" + seq.ToString();
                    break;
                case 5:
                    numSegment = "" + seq.ToString();
                    break;
            }
            result = prefix + DateTime.Today.ToString("yyMM") + numSegment;
            return result;
        }

        public ResponseBase<DSFLeasingClaimDto> Create(DSFLeasingClaimParameterDto objCreate)
        {
            var result = new ResponseBase<DSFLeasingClaimDto>();
            BenefitClaimHeader benefitClaimHeader = new BenefitClaimHeader();
            ChassisMaster chassisMaster = (ChassisMaster)_chassisMasterMapper.Retrieve(objCreate.ChassisMasterID);
            Dealer dealer = (Dealer)_dealerMapper.Retrieve(objCreate.DealerID);
            objCreate.RegNumber = SetRegNumber(1);
            objCreate.ChassisMaster = chassisMaster;
            objCreate.Dealer = dealer;
            DSFLeasingClaimDto objDSFLeasingClaimDto = _mapper.Map<DSFLeasingClaimDto>(objCreate);
            DSFLeasingClaim objDSFLeasingClaim = _mapper.Map<DSFLeasingClaim>(objDSFLeasingClaimDto);
            try
            {
                int insertedID = InsertWithTransactionManager(objDSFLeasingClaim);
                if (insertedID > 0)
                {
                    CriteriaComposite crit = new CriteriaComposite(new Criteria(typeof(DSFLeasingClaim), "RowStatus", MatchType.Exact, (int)DBRowStatus.Active));
                    crit.opAnd(new Criteria(typeof(DSFLeasingClaim), "ID", MatchType.Exact, insertedID));
                    DSFLeasingClaim dSFLeasingClaimDB = _repo.Search(crit).FirstOrDefault();
                    result.success = true;
                    result._id = insertedID;
                    result.total = 1;
                    result.lst = _mapper.Map<DSFLeasingClaimDto>(dSFLeasingClaimDB);
                }
            }
            catch (Exception ex) { ErrorMsgHelper.Exception(result.messages, ex.Message); }
            return result;
        }

        public List<DSFLeasingClaim> Create(List<DSFLeasingClaimParameterDto> listObjectCreate)
        {
            var result = new List<DSFLeasingClaim>();
            int seq = 1;
            foreach (DSFLeasingClaimParameterDto objCreate in listObjectCreate)
            {
                //BenefitClaimHeader benefitClaimHeader = (BenefitClaimHeader)_benefitClaimHeaderMapper.Retrieve(0);
                BenefitClaimHeader benefitClaimHeader = new BenefitClaimHeader();
                ChassisMaster chassisMaster = (ChassisMaster)_chassisMasterMapper.Retrieve(objCreate.ChassisMasterID);
                Dealer dealer = (Dealer)_dealerMapper.Retrieve(objCreate.DealerID);

                DSFLeasingClaim objDSFLeasingClaim = new DSFLeasingClaim();
                objDSFLeasingClaim.RegNumber = SetRegNumber(seq);
                objDSFLeasingClaim.AgreementNo = objCreate.AgreementNo;
                objDSFLeasingClaim.AssetSeqNo = objCreate.AssetSeqNo;
                objDSFLeasingClaim.BenefitClaimHeader = benefitClaimHeader;
                objDSFLeasingClaim.ChassisMaster = chassisMaster;
                objDSFLeasingClaim.ChassisNumber = chassisMaster.ChassisNumber;
                objDSFLeasingClaim.ClaimDate = objCreate.ClaimDate;
                objDSFLeasingClaim.Dealer = dealer;
                objDSFLeasingClaim.SKDNumber = objCreate.SKDNumber;
                objDSFLeasingClaim.SKDDate = objCreate.SKDDate;
                objDSFLeasingClaim.SKDApprovalDate = objCreate.SKDApprovalDate;
                objDSFLeasingClaim.GoLiveDate = objCreate.GoLiveDate;
                objDSFLeasingClaim.CustomerName = objCreate.CustomerName;
                objDSFLeasingClaim.Unit = objCreate.Unit;
                objDSFLeasingClaim.ObjectLease = objCreate.ObjectLease;
                objDSFLeasingClaim.ATPMSubsidy = objCreate.ATPMSubsidy;
                objDSFLeasingClaim.SupplierName = objCreate.SupplierName;
                objDSFLeasingClaim.ProgramName = objCreate.ProgramName;
                objDSFLeasingClaim.CollectionPeriodMonth = byte.Parse(objCreate.CollectionPeriodMonth.ToString());
                objDSFLeasingClaim.CollectionPeriodYear = short.Parse(objCreate.CollectionPeriodYear.ToString());
                objDSFLeasingClaim.TotalDP = objCreate.TotalDP;
                objDSFLeasingClaim.TotalAmountLease = objCreate.TotalAmountLease;
                objDSFLeasingClaim.PeriodLease = objCreate.PeriodLease;
                objDSFLeasingClaim.InterestLease = objCreate.InterestLease;
                objDSFLeasingClaim.Insurance = objCreate.Insurance;
                objDSFLeasingClaim.TypeInsurance = objCreate.TypeInsurance;
                objDSFLeasingClaim.RemarkByDealer = objCreate.RemarkByDealer;
                objDSFLeasingClaim.RemarkByDSF = objCreate.RemarkByDSF;
                objDSFLeasingClaim.Status = (short)objCreate.Status;
                objDSFLeasingClaim.CreatedBy = "SYSTEM";
                objDSFLeasingClaim.CreatedTime = DateTime.Now;
                objDSFLeasingClaim.LastUpdateBy = "SYSTEM";
                objDSFLeasingClaim.LastUpdateTime = DateTime.Now;
                objDSFLeasingClaim.RowStatus = 0;
                result.Add(objDSFLeasingClaim);
                try
                {
                    string INDATA = "";
                    List<int> insertedListId = InsertWithTransactionManager(result, out INDATA);

                    if (insertedListId.Count > 0)
                    {
                        CriteriaComposite crit = new CriteriaComposite(new Criteria(typeof(DSFLeasingClaim), "RowStatus", MatchType.Exact, DBRowStatus.Active));
                        crit.opAnd(new Criteria(typeof(DSFLeasingClaim), "ID", MatchType.InSet, INDATA));
                        List<DSFLeasingClaim> dSFLeasingClaimDB = _repo.Search(crit);
                    }
                }
                catch (Exception ex) {
                    
                };
            }

            
            return result;
        }

        public ResponseBase<DSFLeasingClaimDto> Update(DSFLeasingClaimParameterDto objUpdate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<DSFLeasingClaimDto> Delete(int id)
        {
            throw new NotImplementedException();
        }

        #region Table CRUD
        private int InsertWithTransactionManager(DSFLeasingClaim dataFor)
        {
            int result = -1;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();
                    _repo.Create(dataFor);
                    result = dataFor.ID;
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
        private List<int> InsertWithTransactionManager(List<DSFLeasingClaim> dataFor, out string IN)
        {
            List<int> result = new List<int>();
            IN = "(";
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();
                    foreach (DSFLeasingClaim i in dataFor)
                    {
                        _repo.Create(i);
                        result.Add(i.ID);
                        IN = IN + i.ID + ",";
                    }
                    IN = IN.Remove(IN.Length - 1) + ")";
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
        private void InsertWithTransactionManagerHandler(object sender, TransactionManager.OnInsertArgs args)
        {
            // set the object ID from db returned id
            if (args.DomainObject.GetType() == typeof(DSFLeasingClaim))
            {
                ((DSFLeasingClaim)args.DomainObject).ID = args.ID;
                ((DSFLeasingClaim)args.DomainObject).MarkLoaded();
            }
        }
        #endregion




        public ResponseBase<List<DSFLeasingClaimDto>> Read(DSFLeasingClaimFilterDto filterDto, int pageSize)
        {
            throw new NotImplementedException();
        }

    }

    public class DSFLeasingClaimDocumentBL: AbstractBusinessLogic, IDSFLeasingClaimDocumentBL
    {
        private readonly IDSFLeasingClaimDocumentRepository<DSFLeasingClaimDocument, int> _repo;
        private TransactionManager _transactionManager;

        #region Constructor
        public DSFLeasingClaimDocumentBL(IDSFLeasingClaimDocumentRepository<DSFLeasingClaimDocument, int> repo)
        {
            _repo = repo;
        }
        #endregion

        public ResponseBase<DSFLeasingClaimDocumentDto> Create(DSFLeasingClaimDocumentParameterDto objCreate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<DSFLeasingClaimDocumentDto> Update(DSFLeasingClaimDocumentParameterDto objUpdate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<List<DSFLeasingClaimDocumentDto>> Read(DSFLeasingClaimDocumentFilterDto filterDto, int pageSize)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<DSFLeasingClaimDocumentDto> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
