#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : nventoryTransfer business logic class
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
    public class InventoryTransferBL : AbstractBusinessLogic, IInventoryTransferBL
    {
        #region Variables
        private readonly IMapper _inventoryTransferMapper;
        private readonly AutoMapper.IMapper _mapper;
        private TransactionManager _transactionManager;
        private StandardCodeBL _enumBL;
        #endregion

        #region Constructor
        public InventoryTransferBL()
        {
            _inventoryTransferMapper = MapperFactory.GetInstance().GetMapper(typeof(InventoryTransfer).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _enumBL = new StandardCodeBL(_mapper);
            _transactionManager = new TransactionManager();
            _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);

        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get InventoryTransfer by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<InventoryTransferDto>> Read(InventoryTransferFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(InventoryTransfer), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<InventoryTransferDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(InventoryTransfer), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(InventoryTransfer), filterDto, sortColl);

                // get data
                var data = _inventoryTransferMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<InventoryTransfer>().ToList();
                    var listData = list.Select(item => _mapper.Map<InventoryTransferDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(InventoryTransfer), filterDto);
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
        /// Delete InventoryTransfer by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<InventoryTransferDto> Delete(int id)
        {
            var result = new ResponseBase<InventoryTransferDto>();

            try
            {
                var inventorytransfer = (InventoryTransfer)_inventoryTransferMapper.Retrieve(id);
                if (inventorytransfer != null)
                {
                    inventorytransfer.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _inventoryTransferMapper.Update(inventorytransfer, DNetUserName);
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
        /// Create a new InventoryTransfer
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<InventoryTransferDto> Create(InventoryTransferParameterDto objCreate)
        {
            // set default response
            var result = new ResponseBase<InventoryTransferDto>();

            try
            {
                List<DNetValidationResult> validationResults = new List<DNetValidationResult>();
                List<InventoryTransferDetail> domainDetailData = new List<InventoryTransferDetail>();

                objCreate.LastUpdateBy = DNetUserName;

                validationResults.AddRange(Validate(objCreate));
                if (validationResults.Count > 0)
                {
                    return PopulateValidationError<InventoryTransferDto>(validationResults, null);
                }

                InventoryTransfer domainData = _mapper.Map<InventoryTransfer>(objCreate);

                if (objCreate.InventoryTransferDetails.Count > 0)
                {
                    domainDetailData = _mapper.Map<IList<InventoryTransferDetailParameterDto>, IList<InventoryTransferDetail>>(objCreate.InventoryTransferDetails).ToList();
                }

                int insertedID = InsertWithTransactionManager(domainData, domainDetailData);
                if (insertedID > 0)
                {
                    result.success = true;
                    result._id = insertedID;
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

        /// <summary>
        /// Update InventoryTransfer
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<InventoryTransferDto> Update(InventoryTransferParameterDto objUpdate)
        {
            var domainData = new InventoryTransfer();
            var mergeData = _mapper.Map<InventoryTransfer, InventoryTransferParameterDto>(domainData, objUpdate);

            return null;
        }
        #endregion

        #region private method
        /// <summary>
        /// Trans manager
        /// </summary>
        /// <param name="inventoryTransfer"></param>
        /// <param name="inventoryTransferDetails"></param>
        /// <returns></returns>
        private int InsertWithTransactionManager(InventoryTransfer inventoryTransfer, List<InventoryTransferDetail> inventoryTransferDetails)
        {
            int result = -1;
            ;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // add command to insert data
                    this._transactionManager.AddInsert(inventoryTransfer, DNetUserName);

                    // add command to insert detail data
                    foreach (InventoryTransferDetail item in inventoryTransferDetails)
                    {
                        item.InventoryTransfer = inventoryTransfer;
                        item.LastUpdateBy = DNetUserName;
                        this._transactionManager.AddInsert(item, DNetUserName);
                    }

                    this._transactionManager.PerformTransaction();
                    result = inventoryTransfer.ID;
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
            if (args.DomainObject.GetType() == typeof(InventoryTransfer))
            {
                ((InventoryTransfer)args.DomainObject).ID = args.ID;
                ((InventoryTransfer)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(InventoryTransferDetail))
            {
                ((InventoryTransferDetail)args.DomainObject).ID = args.ID;
                ((InventoryTransferDetail)args.DomainObject).MarkLoaded();
            }
        }

        /// <summary>
        /// Validate model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private List<DNetValidationResult> Validate(InventoryTransferParameterDto model)
        {
            List<DNetValidationResult> results = new List<DNetValidationResult>();
            // Enum validation
            if (!_enumBL.IsExistByCategoryAndValue("InventoryTransfer.ItemTypeforTransfer", ((int)(model.ItemTypeForTransfer)).ToString())) { results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.ItemTypeForTransfer))); }
            if (!_enumBL.IsExistByCategoryAndValue("InventoryTransfer.State", ((int)(model.State)).ToString())) { results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.State))); }
            if (!_enumBL.IsExistByCategoryAndValue("InventoryTransfer.TransactionType", ((int)(model.TransactionType)).ToString())) { results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.TransactionType))); }
            if (!_enumBL.IsExistByCategoryAndValue("InventoryTransfer.TransferStatus", ((int)(model.TransferStatus)).ToString())) { results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.TransferStatus))); }

            return results;
        }
        #endregion
    }
}

