#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : EstimationEquipHeader business logic class
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
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.ExceptionServices;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class EstimationEquipHeaderBL : AbstractBusinessLogic, IEstimationEquipHeaderBL
    {
        #region Variables
        private readonly IMapper _estimationEquipHeaderMapper;
        private readonly AutoMapper.IMapper _mapper;
        private TransactionManager _transactionManager;
        private SparePartMasterBL _sparePartMasterBL;
        private StandardCodeBL _enumBL;
        #endregion

        #region Constructor
        public EstimationEquipHeaderBL()
        {
            _estimationEquipHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(EstimationEquipHeader).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _transactionManager = new TransactionManager();
            _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);
            _enumBL = new StandardCodeBL(_mapper);
            _sparePartMasterBL = new SparePartMasterBL(_mapper);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get EstimationEquipHeader by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<EstimationEquipHeaderDto>> Read(EstimationEquipHeaderFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(EstimationEquipHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(EstimationEquipHeader), "DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<EstimationEquipHeaderDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(EstimationEquipHeader), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(EstimationEquipHeader), filterDto, sortColl);

                // get data
                var data = _estimationEquipHeaderMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<EstimationEquipHeader>().ToList();
                    var listData = list.Select(item => _mapper.Map<EstimationEquipHeaderDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(EstimationEquipHeader), filterDto);
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
        /// Delete EstimationEquipHeader by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<EstimationEquipHeaderDto> Delete(int id)
        {
            var result = new ResponseBase<EstimationEquipHeaderDto>();

            try
            {
                var estimationequipheader = (EstimationEquipHeader)_estimationEquipHeaderMapper.Retrieve(id);
                if (estimationequipheader != null)
                {
                    estimationequipheader.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _estimationEquipHeaderMapper.Update(estimationequipheader, DNetUserName);
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
        /// Default create
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<EstimationEquipHeaderDto> Create(EstimationEquipHeaderParameterDto objCreate)
        {
            return null;
        }

        /// <summary>
        /// Create a new EstimationEquipHeader
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<EstimationEquipHeaderResponseDto> Insert(EstimationEquipHeaderParameterDto objCreate)
        {
            var result = new ResponseBase<EstimationEquipHeaderResponseDto>();
            var validationResults = new List<DNetValidationResult>();

            EstimationEquipHeader newEntity = new EstimationEquipHeader();
            newEntity = _mapper.Map<EstimationEquipHeader>(objCreate);

            // based on app config
            newEntity.Status = Helper.IsAutoSendToSAP(_mapper) ?
                (byte)_enumBL.GetByCategoryAndCode("EnumEstimationEquipStatus.EstimationEquipStatusHeader", "Kirim").ValueId :
                (byte)_enumBL.GetByCategoryAndCode("EnumEstimationEquipStatus.EstimationEquipStatusHeader", "Baru").ValueId;

            newEntity.CreatedTime = DateTime.Now;

            // Validate and get relation 
            validationResults.AddRange(ValidateSave(objCreate, newEntity));

            if (validationResults.Any())
            {
                return PopulateValidationError<EstimationEquipHeaderResponseDto>(validationResults, null);
            }

            try
            {
                int resultID = InsertWithTransactionManager(newEntity);
                if (resultID > 0)
                {
                    var newData = (EstimationEquipHeader)_estimationEquipHeaderMapper.Retrieve(resultID);
                    result.lst = new EstimationEquipHeaderResponseDto()
                    {
                        EstimationDate = newData.CreatedTime,
                        EstimationNumber = newData.EstimationNumber
                    };

                    result.success = true;
                    result._id = resultID;
                    result.total = 1;
                }
                else
                {
                    ErrorMsgHelper.ErrorMsgDBSave(result.messages, FieldResource.EstimationEquipHeader);
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
        /// Update EstimationEquipHeader
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<EstimationEquipHeaderDto> Update(EstimationEquipHeaderParameterDto paramDto)
        {
            var result = new ResponseBase<EstimationEquipHeaderDto>();
            var validationResults = new List<DNetValidationResult>();

            try
            {
                var existingEntity = (EstimationEquipHeader)_estimationEquipHeaderMapper.Retrieve(paramDto.ID);
                existingEntity.LastUpdatedTime = DateTime.Now;
                int resultID = UpdateWithTransactionManager(existingEntity);

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

        /// <summary>
        /// Update spk with transaction manager
        /// </summary>
        /// <param name="objDomain"></param>
        /// <returns></returns>
        private int UpdateWithTransactionManager(EstimationEquipHeader objDomain)
        {
            // set default result
            int result = -1;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

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
        /// Insert spk with transaction manager
        /// </summary>
        /// <param name="spkCustomer"></param>
        /// <returns></returns>
        private int InsertWithTransactionManager(EstimationEquipHeader newEntity)
        {
            int result = -1;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // add command to insert spk
                    this._transactionManager.AddInsert(newEntity, DNetUserName);

                    // add command to insert spk detail
                    foreach (EstimationEquipDetail item in newEntity.EstimationEquipDetails)
                    {
                        this._transactionManager.AddInsert(item, DNetUserName);
                    }

                    this._transactionManager.PerformTransaction();
                    result = newEntity.ID;
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
        /// Handler on executed insert command with transaction manager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void InsertWithTransactionManagerHandler(object sender, TransactionManager.OnInsertArgs args)
        {
            // set the object ID from db returned id
            if (args.DomainObject.GetType() == typeof(EstimationEquipHeader))
            {
                ((EstimationEquipHeader)args.DomainObject).ID = args.ID;
                ((EstimationEquipHeader)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(EstimationEquipDetail))
            {
                ((EstimationEquipDetail)args.DomainObject).ID = args.ID;
                ((EstimationEquipDetail)args.DomainObject).MarkLoaded();
            }
        }

        /// <summary>
        /// Validate Save
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private List<DNetValidationResult> ValidateSave(EstimationEquipHeaderParameterDto objCreate, EstimationEquipHeader entity)
        {
            List<DNetValidationResult> validationResults = new List<DNetValidationResult>();

            Dealer dealer = null;
            if (ValidationHelper.ValidateDealer(objCreate.DealerCode, validationResults, this.DealerCode, ref dealer, false))
            {
                entity.Dealer = dealer;
            }

            // Validate DMSPRNo
            var existingDMSPRNo = GetByDMSPRNo(objCreate.DMSPRNo);
            if (existingDMSPRNo != null)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataIsExist, FieldResource.DMSPRNo)));
            }

            // Validate the details
            foreach (EstimationEquipDetailParameterDto detail in objCreate.EstimationEquipDetails)
            {
                //var spm = _sparePartMasterBL.GetByPartNumberActiveStatus(detail.PartNumber);
                var spm = _sparePartMasterBL.GetValidateSparePartActive(detail.PartNumber, validationResults);
                if (spm != null)
                {
                    if (spm != null && spm.TypeCode != "E")
                    {
                        validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSparepartOnlyAllowedByTypeCode, "E", spm.PartNumber, spm.TypeCode)));
                    }
                    else
                    {
                        StandardCodeDto enumStatusSPK = new StandardCodeDto();
                        enumStatusSPK = _enumBL.GetByCategoryAndCode("EnumSparePartActiveStatus.SparePartActiveStatus", "Active");
                        if (spm.ActiveStatus != enumStatusSPK.ValueId)
                        {
                            validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataNotFound, FieldResource.SparePartMaster)));
                        }
                        else
                        {
                            EstimationEquipDetail domainDetail = _mapper.Map<EstimationEquipDetail>(detail);
                            domainDetail.SparePartMaster = spm;
                            domainDetail.Harga = spm.RetalPrice;
                            domainDetail.EstimationEquipHeader = entity;
                            if (domainDetail.ConfirmedDate == DateTime.MinValue)
                            {
                                domainDetail.ConfirmedDate = SqlDateTime.MinValue.Value;
                            }
                            entity.EstimationEquipDetails.Add(domainDetail);
                        }
                    }
                }
            }

            return validationResults;
        }

        #region For BL Purpose

        /// <summary>
        /// Get by code
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public EstimationEquipHeader GetByDMSPRNo(string no)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(EstimationEquipHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(EstimationEquipHeader), "DMSPRNo", MatchType.Exact, no));
            var arrayListResult = _estimationEquipHeaderMapper.RetrieveByCriteria(criterias);
            if (arrayListResult.Count > 0)
            {
                return (EstimationEquipHeader)arrayListResult[0];
            }

            return null;
        }

        #endregion

        #endregion
    }
}

