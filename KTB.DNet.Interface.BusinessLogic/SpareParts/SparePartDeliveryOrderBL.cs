#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartDeliveryOrder business logic class
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
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ExceptionServices;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class SparePartDeliveryOrderBL : AbstractBusinessLogic, ISparePartDeliveryOrderBL
    {
        #region Variables
        private readonly IMapper _sparepartdeliveryorderMapper;
        private readonly AutoMapper.IMapper _mapper;
        private StandardCodeBL _enumBL;
        private TransactionManager _transactionManager;
        #endregion

        #region Constructor
        public SparePartDeliveryOrderBL()
        {
            _sparepartdeliveryorderMapper = MapperFactory.GetInstance().GetMapper(typeof(SparePartDeliveryOrder).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _enumBL = new StandardCodeBL(_mapper);
            _transactionManager = new TransactionManager();
            _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get SparePartDeliveryOrder by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<SparePartDeliveryOrderDto>> Read(SparePartDeliveryOrderFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(SparePartDeliveryOrder), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<SparePartDeliveryOrderDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(SparePartDeliveryOrder), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(SparePartDeliveryOrder), filterDto, sortColl);

                // get data
                var data = _sparepartdeliveryorderMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<SparePartDeliveryOrder>().ToList();
                    var listData = list.Select(item => _mapper.Map<SparePartDeliveryOrderDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SparePartDeliveryOrder), filterDto);
                }

                result.success = true;

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
        /// Delete SparePartDeliveryOrder by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<SparePartDeliveryOrderDto> Delete(int id)
        {
            var result = new ResponseBase<SparePartDeliveryOrderDto>();

            try
            {
                var sparepartdeliveryorder = (SparePartDeliveryOrder)_sparepartdeliveryorderMapper.Retrieve(id);
                if (sparepartdeliveryorder != null)
                {
                    sparepartdeliveryorder.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _sparepartdeliveryorderMapper.Update(sparepartdeliveryorder, DNetUserName);
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
        /// Create a new SparePartDeliveryOrder
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<SparePartDeliveryOrderDto> Create(SparePartDeliveryOrderParameterDto objCreate)
        {
            var result = new ResponseBase<SparePartDeliveryOrderDto>();
            var validationResults = new List<DNetValidationResult>();

            try
            {
                var existingDomain = _sparepartdeliveryorderMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(SparePartDeliveryOrder), "DeliveryOrderNo", objCreate.DeliveryOrderNo));
                // Update existing Delivery 
                if (existingDomain.Count > 0)
                {
                    var existing = (SparePartDeliveryOrder)existingDomain[0];
                    objCreate.ID = existing.ID;
                    objCreate.CreatedBy = existing.CreatedBy;
                    objCreate.CreatedTime = existing.CreatedTime;
                    objCreate.LastUpdateTime = DateTime.Now;

                    List<SparePartDeliveryOrderDetail> tempDetails = existing.SparePartDeliveryOrderDetails.Cast<SparePartDeliveryOrderDetail>().ToList();
                    List<SparePartDeliveryOrderDetail> newDetails = new List<SparePartDeliveryOrderDetail>();
                    foreach (SparePartDeliveryOrderDetailParameterDto detail in objCreate.SparePartDeliveryOrderDetails)
                    {
                        var tempDetail = tempDetails.Where(w => w.Product == detail.Product).FirstOrDefault();
                        if (tempDetail != null)
                        {
                            detail.ID = tempDetail.ID;
                        }

                        var newDetail = _mapper.Map<SparePartDeliveryOrderDetailParameterDto, SparePartDeliveryOrderDetail>(detail);
                        newDetails.Add(newDetail);
                    }

                    // soft delete details
                    var deleteitems = tempDetails.Where(p => objCreate.SparePartDeliveryOrderDetails.All(p2 => p2.Product != p.Product)).ToList();
                    if (deleteitems.Count > 0)
                    {
                        foreach (SparePartDeliveryOrderDetail detail in deleteitems)
                        {
                            detail.RowStatus = 1;
                            newDetails.Add(detail);
                        }
                    }

                    var mergeData = _mapper.Map<SparePartDeliveryOrderParameterDto, SparePartDeliveryOrder>(objCreate, existing);
                    mergeData.SparePartDeliveryOrderDetails = new ArrayList(newDetails); ;

                    int resultID = UpdateWithTransactionManager(mergeData);
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
                else // insert new row
                {
                    SparePartDeliveryOrder newEntity = new SparePartDeliveryOrder();
                    newEntity = _mapper.Map<SparePartDeliveryOrder>(objCreate);

                    newEntity.CreatedTime = DateTime.Now;

                    // Validate and get relation 
                    validationResults.AddRange(ValidateSave(objCreate, newEntity));

                    foreach (SparePartDeliveryOrderDetailParameterDto detail in objCreate.SparePartDeliveryOrderDetails)
                    {
                        SparePartDeliveryOrderDetail domainDetail = _mapper.Map<SparePartDeliveryOrderDetail>(detail);
                        domainDetail.SparePartDeliveryOrder = newEntity;
                        newEntity.SparePartDeliveryOrderDetails.Add(domainDetail);
                    }

                    if (validationResults.Any())
                    {
                        return PopulateValidationError<SparePartDeliveryOrderDto>(validationResults, null);
                    }

                    int resultID = InsertWithTransactionManager(newEntity);

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
        /// Update SparePartDeliveryOrder
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<SparePartDeliveryOrderDto> Update(SparePartDeliveryOrderParameterDto objUpdate)
        {
            return null;
        }
        #endregion

        #region Private Methods


        private List<DNetValidationResult> ValidateSave(SparePartDeliveryOrderParameterDto objCreate, SparePartDeliveryOrder entity)
        {
            List<DNetValidationResult> results = new List<DNetValidationResult>();

            // Enum validation
            if (!_enumBL.IsExistByCategoryAndValue("SparePartDeliveryOrder.DeliveryType", ((int)(objCreate.DeliveryType)).ToString()))
            {
                results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.DeliveryType)));
            }

            if (!_enumBL.IsExistByCategoryAndValue("SparePartDeliveryOrder.Handling", ((int)(objCreate.Status)).ToString()))
            {
                results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.Handling)));
            }
            if (!_enumBL.IsExistByCategoryAndValue("SparePartDeliveryOrder.State", ((int)(objCreate.State)).ToString()))
            {
                results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.State)));
            }

            var count = objCreate.SparePartDeliveryOrderDetails.Select(x => x.Product).Distinct().Count();
            if (count > 1)
            {
                results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgMustBeUnique, FieldResource.Product)));
            }
            return results;
        }

        /// <summary>
        /// Update spk with transaction manager
        /// </summary>
        /// <param name="objDomain"></param>
        /// <returns></returns>
        private int UpdateWithTransactionManager(SparePartDeliveryOrder objDomain)
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

                    // add command to insert spk detail
                    objDomain.MarkLoaded();
                    foreach (SparePartDeliveryOrderDetail item in objDomain.SparePartDeliveryOrderDetails)
                    {
                        item.SparePartDeliveryOrder = objDomain;
                        if (item.ID > 0)
                        {
                            this._transactionManager.AddUpdate(item, UserName);
                        }
                        else
                        {
                            this._transactionManager.AddInsert(item, UserName);
                        }
                    }

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
        /// Insert with transaction manager
        /// </summary>
        /// <param name="newEntity"></param>
        /// <returns></returns>
        private int InsertWithTransactionManager(SparePartDeliveryOrder newEntity)
        {
            int result = -1;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // add command to insert spk
                    this._transactionManager.AddInsert(newEntity, UserName);

                    // add command to insert spk detail
                    foreach (SparePartDeliveryOrderDetail item in newEntity.SparePartDeliveryOrderDetails)
                    {
                        this._transactionManager.AddInsert(item, UserName);
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
            if (args.DomainObject.GetType() == typeof(SparePartDeliveryOrder))
            {
                ((SparePartDeliveryOrder)args.DomainObject).ID = args.ID;
                ((SparePartDeliveryOrder)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(SparePartDeliveryOrderDetail))
            {
                ((SparePartDeliveryOrderDetail)args.DomainObject).ID = args.ID;
                ((SparePartDeliveryOrderDetail)args.DomainObject).MarkLoaded();
            }
            //else if (args.DomainObject.GetType() == typeof(Dealer))
            //{
            //    ((Dealer)args.DomainObject).ID = args.ID;
            //    ((Dealer)args.DomainObject).MarkLoaded();
            //}
        }

        #endregion
    }
}
