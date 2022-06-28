using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Model.Parameters;
using KTB.DNet.Interface.Repository.Interface;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace KTB.DNet.Interface.BusinessLogic
{
    public class SparePartClaimBL : AbstractBusinessLogic, ISparePartClaimBL
    {
        private ISparePartClaimRepository<SparePartClaim, int> _sparePartClaimRepository;
        private TransactionManager _transactionManager;
        private DealerBL _dealerBL;
        private SparePartMasterBL _spareparmastertBL;
        private readonly AutoMapper.IMapper _mapper;
        private readonly IMapper _sparepartpostatusMaster;
        private readonly IMapper _sparepartpostatusdetailMaster;
        private readonly IMapper _claimDetailMapper;

        public SparePartClaimBL(ISparePartClaimRepository<SparePartClaim, int> sparePartClaimRepository)
        {
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _dealerBL = new DealerBL();
            _sparepartpostatusMaster = MapperFactory.GetInstance().GetMapper(typeof(SparePartPOStatus).ToString());
            _sparepartpostatusdetailMaster = MapperFactory.GetInstance().GetMapper(typeof(SparePartPOStatusDetail).ToString());
            _spareparmastertBL = new SparePartMasterBL(_mapper);
            _sparePartClaimRepository = sparePartClaimRepository;
            _transactionManager = new TransactionManager();
            _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);
            _claimDetailMapper = MapperFactory.GetInstance().GetMapper(typeof(ClaimDetail).ToString());
        }

        /// <summary>
        /// Get SparePartClaim by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<SparePartClaimDto>> Read(SparePartClaimFilterDto filterDto, int pageSize)
        {
            var result = new ResponseBase<List<SparePartClaimDto>>();
            var sortColl = string.Empty;
            var innerQueryCriteria = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            int filteredTotalRowDetail = 0;
            int totalRowDetail = 0;

            try
            {
                if (filterDto.find != null)
                {
                    if (filterDto.find[0].PropertyName.Equals("LastUpdateTime"))
                    {
                        DateTime nextDay = DateTime.Parse(filterDto.find[0].PropertyValue).AddDays(1);
                        filterDto.find.Add(new MatchTypeFilter { MatchType = MatchType.Lesser, PropertyName = "LastUpdateTime", PropertyValue = nextDay.ToString("yyyy-MM-dd"), SqlOperation = SQLOperation.And });
                    }
                }
                var criterias = Helper.InitialStrCriteria(typeof(SparePartClaimDto), filterDto);

                // filter by Dealer
                criterias = Helper.UpdateStrCriteria(typeof(SparePartClaimDto), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.InSet.GetHashCode(), "DealerCode", DealerCode, false, criterias);

                sortColl = Helper.UpdateSortColumnDapper(typeof(SparePartClaimDto), filterDto);

                List<SparePartClaimDto> data = _sparePartClaimRepository.Search(
                                    criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRow, out totalRow);

                if (data != null && data.Count > 0)
                {
                    criterias = Helper.InitialStrCriteria(typeof(SparePartClaimDetailParameterDto), filterDto);

                    // filter by Dealer
                    criterias = Helper.UpdateStrCriteria(typeof(SparePartClaimDetailParameterDto), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.InSet.GetHashCode(), "DealerCode", DealerCode, false, criterias);
                    //sortColl = Helper.UpdateSortColumnDapper(typeof(SparePartClaimDetailParameterDto), filterDto, "SparePartClaimDetailParameterDto");

                    var detailClaim = _sparePartClaimRepository.SearchDetail(
                         criterias, innerQueryCriteria, sortColl, filterDto.pages, pageSize, out filteredTotalRowDetail, out totalRowDetail);
                    foreach(var dt in data)
                    {
                        dt.ClaimDetails = detailClaim.Where(x => x.ID == dt.ID).ToList();
                    }

                    result.lst = data;
                    result.total = filteredTotalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SparePartClaimDto), filterDto);
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
        /// Create a new SparePartClaim
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<SparePartClaimDto> Create(SparePartClaimParameterDto objCreate)
        {
            // set default response
            var result = new ResponseBase<SparePartClaimDto>();

            try
            {
                List<DNetValidationResult> validationResults = new List<DNetValidationResult>();
                var domainClaimHeader = new ClaimHeader
                {
                    
                };
                List<ClaimDetail> domainClaimDetailList = new List<ClaimDetail>();
                List<SpClaimDocument> domainSpClaimDocument = new List<SpClaimDocument>();

                if (ValidateSparePartClaim(objCreate, out domainClaimHeader, out domainClaimDetailList, out domainSpClaimDocument, validationResults))
                {
                    #region Insert via Trans Manager
                    int insertedID = InsertWithTransactionManager(domainClaimHeader, domainClaimDetailList, domainSpClaimDocument);
                    if (insertedID > 0)
                    {
                        // get the spk number from the new spk
                        //ClaimHeader spkOnDB = (SPKHeader)_spkHeaderMapper.Retrieve(domainSPK.ID);
                        //domainSPK.SPKNumber = spkOnDB.SPKNumber;
                        //domainSPK.CreatedBy = spkOnDB.CreatedBy;

                        // set result
                        result.success = true;
                        result._id = insertedID;
                        result.total = 1;
                        //result.lst = _mapper.Map<SPKHeaderDto>(spkOnDB);
                    }
                    else
                    {
                        result.messages.Add(new MessageBase { ErrorCode = ErrorCode.UnhandledException, ErrorMessage = MessageResource.ErrorMsgOnSavingPleaseContactAdmin });
                    }
                    #endregion
                }
                else
                {
                    return PopulateValidationError<SparePartClaimDto>(validationResults, null);
                }
                //if (objCreate.ID != Constants.NUMBER_DEFAULT_VALUE)
                //{
                //    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgCreateID, objCreate.ID)));
                //}

                //objCreate.LastUpdateBy = DNetUserName;

                //validationResults.AddRange(ValidateSparePartClaim(objCreate,));
                //if (validationResults.Count > 0)
                //{
                //    return PopulateValidationError<APPaymentDto>(validationResults, null);
                //}

                //APPayment domainData = _mapper.Map<APPayment>(objCreate);

                //if (objCreate.APPaymentDetails.Count > 0)
                //{
                //    domainDetailData = _mapper.Map<IList<APPaymentDetailParameterDto>, IList<APPaymentDetail>>(objCreate.APPaymentDetails).ToList();
                //}

                //int insertedID = InsertWithTransactionManager(domainData, domainDetailData);
                //if (insertedID > 0)
                //{
                //    result.success = true;
                //    result._id = insertedID;
                //    result.total = 1;
                //    result.lst = null;
                //}
                //else
                //{
                //    ErrorMsgHelper.ErrorMsgDBSaveContactAdmin(result.messages);
                //}
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
        /// Update SparePartDO
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<SparePartClaimDto> Update(SparePartClaimParameterDto objUpdate)
        {
            return null;
        }

        /// <summary>
        /// Delete SparePartDO by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<SparePartClaimDto> Delete(int id)
        {
            return null;
        }

        #region private method
        /// <summary>
        /// Insert data using trans manager
        /// </summary>
        /// <param name="appayment"></param>
        /// <param name="appaymentDetails"></param>
        /// <returns></returns>
        private int InsertWithTransactionManager(ClaimHeader claimheader, List<ClaimDetail> claimDetails, List<SpClaimDocument> spClaimDocuments)
        {
            int result = -1;
            ;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // add command to insert data
                    this._transactionManager.AddInsert(claimheader, DNetUserName);

                    // add command to insert detail data
                    foreach (ClaimDetail item in claimDetails)
                    {
                        item.LastUpdateBy = DNetUserName;
                        item.ClaimHeader = claimheader;
                        this._transactionManager.AddInsert(item, DNetUserName);
                    }

                    //add command to insert spClaimDocument
                    foreach(SpClaimDocument item in spClaimDocuments)
                    {
                        item.LastUpdateBy = DNetUserName;
                        item.ClaimHeader = claimheader;
                        this._transactionManager.AddInsert(item, DNetUserName);
                    }

                    this._transactionManager.PerformTransaction();
                    result = claimheader.ID;
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
            if (args.DomainObject.GetType() == typeof(ClaimHeader))
            {
                ((ClaimHeader)args.DomainObject).ID = args.ID;
                ((ClaimHeader)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(ClaimDetail))
            {
                ((ClaimDetail)args.DomainObject).ID = args.ID;
                ((ClaimDetail)args.DomainObject).MarkLoaded();
            }
        }

        /// <summary>
        /// Validate assist part stock
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="entity"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        private bool ValidateSparePartClaim(SparePartClaimParameterDto objCreate, out ClaimHeader claimHeader,out List<ClaimDetail> claimDetail, out List<SpClaimDocument> spClaimDocument, List<DNetValidationResult> validationResults)
        {
            //Initialize 
            claimHeader = new ClaimHeader();
            claimDetail = new List<ClaimDetail>();
            spClaimDocument = new List<SpClaimDocument>();

            // Validate Dealer            
            DNet.Domain.Dealer dealer = null;
            if (ValidationHelper.ValidateDealer(objCreate.DealerCode, validationResults, this.DealerCode, ref dealer))
            {
                claimHeader.Dealer = dealer;
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMessageDataNotFound, FieldResource.DealerCode, objCreate.DealerCode)));
            }

            //Validate ClaimDate
            if(DateTime.Parse(objCreate.ClaimDate).Date < DateTime.Today)
            {
                validationResults.Add(new DNetValidationResult(string.Format("{0} isbackdate with value {1}", "ClaimDate", objCreate.ClaimDate)));
            }
            else
            {
                claimHeader.ClaimDate = DateTime.Parse(objCreate.ClaimDate);
            }

            //set value
            claimHeader.Status = objCreate.Status;
            claimHeader.StatusKTB = objCreate.StatusKTB;
            claimHeader.ClaimProgress = new ClaimProgress { ID= objCreate.ClaimProgressId };
            claimHeader.ClaimReason = new ClaimReason { ID = objCreate.ClaimReasonid };

            //SONumber for Get SparePartPOStatusId
            var criterias = new CriteriaComposite(new Criteria(typeof(SparePartPOStatus), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(SparePartPOStatus), "SONumber", MatchType.Exact, objCreate.SONumber));
            var arrListResult = _sparepartpostatusMaster.RetrieveByCriteria(criterias);
            if (objCreate.SONumber == "" || string.IsNullOrEmpty(objCreate.SONumber))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataInvalid, FieldResource.SONumber, objCreate.SONumber)));
            }
            else
            {

                if (arrListResult.Count > 0)
                {
                    var list = arrListResult.Cast<SparePartPOStatus>().ToList();
                    claimHeader.SparePartPOStatus = new SparePartPOStatus { ID = list[0].ID };
                }
            }
            
            foreach(var detail in objCreate.ClaimDetails)
            {
                var itemClaimDetail = new ClaimDetail();
                //validate No Faktur
                if (String.IsNullOrEmpty(objCreate.NoFaktur) || objCreate.NoFaktur == "")
                {
                    validationResults.Add(new DNetValidationResult(string.Format("{0} tidak valid with value {1}", "NoFaktur", objCreate.NoFaktur)));
                }
                else
                {
                    if (arrListResult.Count > 0)
                    {
                        //var list = arrListResult.Cast<SparePartPOStatus>().ToList();
                        var sparePartMaster = _spareparmastertBL.GetActivePartByPartNumber(detail.NoBarang);
                        if(sparePartMaster != null)
                        {
                            criterias = new CriteriaComposite(new Criteria(typeof(SparePartPOStatusDetail), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                            criterias.opAnd(new Criteria(typeof(SparePartPOStatusDetail), "SparePartPOStatus", MatchType.Exact, claimHeader.SparePartPOStatus.ID));
                            criterias.opAnd(new Criteria(typeof(SparePartPOStatusDetail), "SparePartMaster", MatchType.Exact, sparePartMaster.ID));
                            var arrListPOStatusDetailResult = _sparepartpostatusdetailMaster.RetrieveByCriteria(criterias);
                            if (arrListPOStatusDetailResult.Count > 0)
                            {
                                var listPOStatusDetail = arrListPOStatusDetailResult.Cast<SparePartPOStatusDetail>().ToList();
                                itemClaimDetail.SparePartPOStatusDetail = new SparePartPOStatusDetail { ID = listPOStatusDetail[0].ID };
                                itemClaimDetail.Keterangan = detail.Keterangan;
                                itemClaimDetail.Qty = detail.QtyClaim;
                                itemClaimDetail.ApprovedQty = 0;
                                itemClaimDetail.StatusDetail = detail.StatusDetail;
                                itemClaimDetail.StatusDetailKTB = detail.StatusDetailKTB;
                                itemClaimDetail.ClaimGoodCondition = new ClaimGoodCondition { ID = detail.ClaimGoodConditionId };

                            }
                            else
                            {
                                validationResults.Add(new DNetValidationResult(string.Format("data {0} tidak ditemukan", "SparePartPOStatusDetail")));
                            }
                        }
                        else
                        {
                            validationResults.Add(new DNetValidationResult(string.Format("{0} tidak valid with value {1}", "NoSparepart", detail.NoBarang)));
                        }
                    }
                }
                claimDetail.Add(itemClaimDetail);
            }

            foreach(var detailSpDocument in objCreate.DocumentUpload)
            {
                var itemSpDocument = new SpClaimDocument();
                if(detailSpDocument.Path == "" || string.IsNullOrWhiteSpace(detailSpDocument.Path))
                {
                    validationResults.Add(new DNetValidationResult(string.Format("{0} tidak valid with value {1}", "Path", itemSpDocument.FilePath)));
                }
                else
                {
                    itemSpDocument.FileName = detailSpDocument.FileName;
                    itemSpDocument.FilePath = detailSpDocument.Path;
                    itemSpDocument.SPSupportClaimDoc = new SPSupportClaimDoc { ID = detailSpDocument.Type };
                }

                spClaimDocument.Add(itemSpDocument);
            }
            

            return validationResults.Count == 0;
        }
        #endregion

        public ResponseBase<List<SparePartClaimResponseDto>> ReadSparePartClaim(SparePartClaimFilterDto filterDto, int pageSize)
        {
            return new ResponseBase<List<SparePartClaimResponseDto>>();
        }

        public ResponseBase<List<SparePartClaimResponseDto>> ReadSparePartClaim1(SparePartClaimFilterDto filterDto, int pageSize)
        {
            return new ResponseBase<List<SparePartClaimResponseDto>>();

        }

       public ResponseBase<List<SparePartClaimResponseDto>> ReadSparePartClaim2(SparePartClaimFilterDto filterDto, int pageSize)
        {
            return new ResponseBase<List<SparePartClaimResponseDto>>();
        }

        public ResponseBase<List<SparePartClaimResponseDto>> ReadSparePartClaimWithCriteria(SparePartClaimFilterDto filterDto, int pageSize)
        {
            return new ResponseBase<List<SparePartClaimResponseDto>>();
        }
    }
}
