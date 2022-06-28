#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : nventoryTransaction business logic class
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
using System.Linq;
using System.Runtime.ExceptionServices;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class InventoryTransactionBL : AbstractBusinessLogic, IInventoryTransactionBL
    {
        #region Variables
        private readonly IMapper _inventorytransactionMapper;
        private TransactionManager _transactionManager;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public InventoryTransactionBL()
        {
            _inventorytransactionMapper = MapperFactory.GetInstance().GetMapper(typeof(InventoryTransaction).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();

            this._transactionManager = new TransactionManager();
            _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get InventoryTransaction by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<InventoryTransactionDto>> Read(InventoryTransactionFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(InventoryTransaction), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(InventoryTransaction), "DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<InventoryTransactionDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(InventoryTransaction), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(InventoryTransaction), filterDto, sortColl);

                // get data
                var data = _inventorytransactionMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<InventoryTransaction>().ToList();
                    var listData = list.Select(item => _mapper.Map<InventoryTransactionDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(InventoryTransaction), filterDto);
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
        /// Delete InventoryTransaction by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<InventoryTransactionDto> Delete(int id)
        {
            var result = new ResponseBase<InventoryTransactionDto>();

            try
            {
                var inventorytransaction = (InventoryTransaction)_inventorytransactionMapper.Retrieve(id);
                if (inventorytransaction != null)
                {
                    inventorytransaction.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _inventorytransactionMapper.Update(inventorytransaction, DNetUserName);
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
        /// Create a new InventoryTransaction
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<InventoryTransactionDto> Create(InventoryTransactionParameterDto objCreate)
        {
            var result = new ResponseBase<InventoryTransactionDto>();
            var validationResults = new List<DNetValidationResult>();
            bool isValid = true;
            List<InventoryTransactionDetail> inventorytransactionDetails = new List<InventoryTransactionDetail>();

            InventoryTransaction inventorytransaction = _mapper.Map<InventoryTransaction>(objCreate);

            if (ValidateInventoryTransaction(objCreate, validationResults))
            {
                foreach (InventoryTransactionDetailParameterDto item in objCreate.InventoryTransactionDetails)
                {
                    var detail = _mapper.Map<InventoryTransactionDetail>(item) as InventoryTransactionDetail;
                    if (ValidateInventoryTransactionDetail(item, validationResults))
                    {
                        inventorytransactionDetails.Add(detail);
                    }
                }
            }

            isValid = validationResults.Count == 0;
            if (isValid)
            {
                try
                {
                    var createdObject = InsertWithTransactionManager(inventorytransaction, inventorytransactionDetails);
                    if (createdObject != null)
                    {
                        result._id = createdObject.ID;
                    }
                    else
                    {
                        validationResults.Add(new DNetValidationResult(ErrorCode.DBSaveFailed, string.Format(MessageResource.ErrorMsgDBSave, MessageResource.ErrorMsgOnSavingPleaseContactAdmin)));
                    }
                }
                catch (Exception ex)
                {
                    validationResults.Add(new DNetValidationResult(ErrorCode.UnhandledException, string.Format(MessageResource.ErrorMsgPRGUnhandle, ex.Message)));
                }
            }

            isValid = validationResults.Count == 0;
            if (isValid)
            {
                result.success = true;
                result.total = 1;
            }
            else
            {
                return PopulateValidationError<InventoryTransactionDto>(validationResults, null);
            }

            return result;
        }

        /// <summary>
        /// Validate transaction
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        public bool ValidateInventoryTransaction(InventoryTransactionParameterDto obj, List<DNetValidationResult> validationResults)
        {
            var scBL = new StandardCodeBL(_mapper);
            if (!scBL.IsExistByCategoryAndValue("InventoryTransaction.State", obj.State.ToString()))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgEnumNotFound, FieldResource.State)));
            }
            if (!scBL.IsExistByCategoryAndValue("InventoryTransaction.TransactionType", obj.TransactionType.ToString()))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgEnumNotFound, FieldResource.TransactionType)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate inventory
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        public bool ValidateInventoryTransactionDetail(InventoryTransactionDetailParameterDto obj, List<DNetValidationResult> validationResults)
        {
            var scBL = new StandardCodeBL(_mapper);
            if (!scBL.IsExistByCategoryAndValue("InventoryTransaction.TransactionType", obj.TransactionType.ToString()))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgEnumNotFound, FieldResource.TransactionType)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Update InventoryTransaction
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<InventoryTransactionDto> Update(InventoryTransactionParameterDto objUpdate)
        {
            return null;
        }

        /// <summary>
        /// Insert Spare Part PO with transaction manager
        /// </summary>
        /// <param name="spare part POCustomer"></param>
        /// <returns></returns>
        private InventoryTransaction InsertWithTransactionManager(InventoryTransaction inventorytransaction, List<InventoryTransactionDetail> inventorytransactionDetails)
        {
            InventoryTransaction result = null;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // add command to insert spare part PO
                    this._transactionManager.AddInsert(inventorytransaction, DNetUserName);

                    // add command to insert spare part PO detail
                    foreach (InventoryTransactionDetail item in inventorytransactionDetails)
                    {
                        item.InventoryTransaction = inventorytransaction;
                        this._transactionManager.AddInsert(item, DNetUserName);
                    }

                    this._transactionManager.PerformTransaction();
                    result = inventorytransaction;
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
            if (args.DomainObject.GetType() == typeof(InventoryTransaction))
            {
                ((InventoryTransaction)args.DomainObject).ID = args.ID;
                ((InventoryTransaction)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(InventoryTransactionDetail))
            {
                ((InventoryTransactionDetail)args.DomainObject).ID = args.ID;
                ((InventoryTransactionDetail)args.DomainObject).MarkLoaded();
            }
        }
        #endregion
    }
}

