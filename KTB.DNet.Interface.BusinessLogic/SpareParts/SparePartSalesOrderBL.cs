#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartSalesOrder business logic class
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
    public class SparePartSalesOrderBL : AbstractBusinessLogic, ISparePartSalesOrderBL
    {
        #region Variables
        private readonly IMapper _sparepartsalesorderMapper;
        private readonly AutoMapper.IMapper _mapper;
        private TransactionManager _transactionManager;
        private StandardCodeBL _enumBL;
        #endregion

        #region Constructor
        public SparePartSalesOrderBL()
        {
            _sparepartsalesorderMapper = MapperFactory.GetInstance().GetMapper(typeof(SparePartSalesOrder).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _enumBL = new StandardCodeBL(_mapper);
            _transactionManager = new TransactionManager();
            _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get SparePartSalesOrder by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<SparePartSalesOrderDto>> Read(SparePartSalesOrderFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(SparePartSalesOrder), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(SparePartSalesOrder), "DealerCode", MatchType.Exact, DealerCode));
            var result = new ResponseBase<List<SparePartSalesOrderDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(SparePartSalesOrder), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(SparePartSalesOrder), filterDto, sortColl);

                // get data
                var data = _sparepartsalesorderMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<SparePartSalesOrder>().ToList();
                    var listData = list.Select(item => _mapper.Map<SparePartSalesOrderDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(SparePartSalesOrder), filterDto);
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
        /// Delete SparePartSalesOrder by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<SparePartSalesOrderDto> Delete(int id)
        {
            var result = new ResponseBase<SparePartSalesOrderDto>();

            try
            {
                var sparepartsalesorder = (SparePartSalesOrder)_sparepartsalesorderMapper.Retrieve(id);
                if (sparepartsalesorder != null)
                {
                    sparepartsalesorder.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _sparepartsalesorderMapper.Update(sparepartsalesorder, DNetUserName);
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
        /// Create a new SparePartSalesOrder
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<SparePartSalesOrderDto> Create(SparePartSalesOrderParameterDto objCreate)
        {
            // set default response
            var result = new ResponseBase<SparePartSalesOrderDto>();

            try
            {
                List<DNetValidationResult> validationResults = new List<DNetValidationResult>();
                List<SparePartSalesOrderDetail> domainDetailData = new List<SparePartSalesOrderDetail>();

                objCreate.LastUpdateBy = DNetUserName;

                validationResults.AddRange(Validate(objCreate));
                if (validationResults.Count > 0)
                {
                    return PopulateValidationError<SparePartSalesOrderDto>(validationResults, null);
                }

                SparePartSalesOrder domainData = _mapper.Map<SparePartSalesOrder>(objCreate);

                if (objCreate.SparePartSalesOrderDetails.Count > 0)
                {
                    domainDetailData = _mapper.Map<IList<SparePartSalesOrderDetailParameterDto>, IList<SparePartSalesOrderDetail>>(objCreate.SparePartSalesOrderDetails).ToList();
                }

                int insertedID = InsertWithTransactionManager(domainData, domainDetailData);
                if (insertedID > 0)
                {
                    result.success = true;
                    result._id = insertedID;
                    result.total = 1;
                    result.lst = null;
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
        /// Update SparePartSalesOrder
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<SparePartSalesOrderDto> Update(SparePartSalesOrderParameterDto objUpdate)
        {
            return null;
        }
        #endregion

        #region Private method
        private int InsertWithTransactionManager(SparePartSalesOrder sparepartsalesorder, List<SparePartSalesOrderDetail> sparepartsalesorderDetails)
        {
            int result = -1;
            ;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // add command to insert data
                    this._transactionManager.AddInsert(sparepartsalesorder, DNetUserName);

                    // add command to insert detail data
                    foreach (SparePartSalesOrderDetail item in sparepartsalesorderDetails)
                    {
                        item.KodeDealer = DealerCode;
                        item.LastUpdateBy = DNetUserName;
                        item.SparePartSalesOrder = sparepartsalesorder;
                        this._transactionManager.AddInsert(item, DNetUserName);
                    }

                    this._transactionManager.PerformTransaction();
                    result = sparepartsalesorder.ID;
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
            if (args.DomainObject.GetType() == typeof(SparePartSalesOrder))
            {
                ((SparePartSalesOrder)args.DomainObject).ID = args.ID;
                ((SparePartSalesOrder)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(SparePartSalesOrderDetail))
            {
                ((SparePartSalesOrderDetail)args.DomainObject).ID = args.ID;
                ((SparePartSalesOrderDetail)args.DomainObject).MarkLoaded();
            }
        }

        /// <summary>
        /// Validate model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private List<DNetValidationResult> Validate(SparePartSalesOrderParameterDto model)
        {
            List<DNetValidationResult> results = new List<DNetValidationResult>();

            List<StandardCodeDto> enumStatus = _enumBL.GetByCategory("SparePart.Status");

            // Enum validation
            if (!_enumBL.IsExistByCategoryAndValue("SparePartSO.SalesChannel", ((int)(model.SalesChannel)).ToString())) { results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.SalesChannel))); }
            if (enumStatus.Where(e => e.ValueId == model.Status).FirstOrDefault() == null) { results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.SparePartStatus))); }
            if (!_enumBL.IsExistByCategoryAndValue("SparePartSO.Handling", ((int)(model.Handling)).ToString())) { results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.Handling))); }

            if (!string.IsNullOrEmpty(model.ShipmentType))
            {
                if (!_enumBL.IsExistByCategoryAndCode("SparePartSO.ShipmentType", model.ShipmentType)) { results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.ShipmentType))); }
            }

            if (!_enumBL.IsExistByCategoryAndCode("SparePartSO.State", model.State)) { results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.State))); }

            foreach (SparePartSalesOrderDetailParameterDto i in model.SparePartSalesOrderDetails)
            {
                if (enumStatus.Where(e => e.ValueId == i.Status).FirstOrDefault() == null) { results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.SparePartStatus))); }
            }

            return results;
        }
        #endregion
    }
}

