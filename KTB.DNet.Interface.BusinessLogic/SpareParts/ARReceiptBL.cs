#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ARReceipt business logic class
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
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ExceptionServices;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class ARReceiptBL : AbstractBusinessLogic, IARReceiptBL
    {
        #region Variables
        private readonly IMapper _arreceiptMapper;
        private readonly AutoMapper.IMapper _mapper;
        private TransactionManager _transactionManager;
        private StandardCodeBL _enumBL;
        #endregion

        #region Constructor
        public ARReceiptBL()
        {
            _arreceiptMapper = MapperFactory.GetInstance().GetMapper(typeof(ARReceipt).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _enumBL = new StandardCodeBL(_mapper);
            _transactionManager = new TransactionManager();
            _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get ARReceipt by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<ARReceiptDto>> Read(ARReceiptFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(ARReceipt), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<ARReceiptDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(ARReceipt), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(ARReceipt), filterDto, sortColl);

                // get data
                var data = _arreceiptMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<ARReceipt>().ToList();
                    var listData = list.Select(item => _mapper.Map<ARReceiptDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(ARReceipt), filterDto);
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
        /// Delete ARReceipt by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<ARReceiptDto> Delete(int id)
        {
            var result = new ResponseBase<ARReceiptDto>();

            try
            {
                var arreceipt = (ARReceipt)_arreceiptMapper.Retrieve(id);
                if (arreceipt != null)
                {
                    arreceipt.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _arreceiptMapper.Update(arreceipt, DNetUserName);
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
        /// Create a new ARReceipt
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<ARReceiptDto> Create(ARReceiptParameterDto objCreate)
        {
            // set default response
            var result = new ResponseBase<ARReceiptDto>();

            try
            {
                List<DNetValidationResult> validationResults = new List<DNetValidationResult>();
                List<ARReceiptDetail> domainDetailData = new List<ARReceiptDetail>();

                if (objCreate.ID != Constants.NUMBER_DEFAULT_VALUE)
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgCreateID, objCreate.ID)));
                }

                objCreate.LastUpdateBy = DNetUserName;

                validationResults.AddRange(Validate(objCreate));
                if (validationResults.Count > 0)
                {
                    return PopulateValidationError<ARReceiptDto>(validationResults, null);
                }

                ARReceipt domainData = _mapper.Map<ARReceipt>(objCreate);

                if (objCreate.ARReceiptDetails.Count > 0)
                {
                    domainDetailData = _mapper.Map<IList<ARReceiptDetailParameterDto>, IList<ARReceiptDetail>>(objCreate.ARReceiptDetails).ToList();
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
        /// Update ARReceipt
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<ARReceiptDto> Update(ARReceiptParameterDto objUpdate)
        {
            return null;
        }
        #endregion

        #region Private method
        /// <summary>
        /// Insert data using trans manager
        /// </summary>
        /// <param name="arreceipt"></param>
        /// <param name="arreceiptDetails"></param>
        /// <returns></returns>
        private int InsertWithTransactionManager(ARReceipt arreceipt, List<ARReceiptDetail> arreceiptDetails)
        {
            int result = -1;
            ;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // add command to insert data
                    this._transactionManager.AddInsert(arreceipt, DNetUserName);

                    // add command to insert detail data
                    foreach (ARReceiptDetail item in arreceiptDetails)
                    {
                        item.LastUpdateBy = DNetUserName;
                        item.ARReceipt = arreceipt;
                        this._transactionManager.AddInsert(item, DNetUserName);
                    }

                    this._transactionManager.PerformTransaction();
                    result = arreceipt.ID;
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
            if (args.DomainObject.GetType() == typeof(ARReceipt))
            {
                ((ARReceipt)args.DomainObject).ID = args.ID;
                ((ARReceipt)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(ARReceiptDetail))
            {
                ((ARReceiptDetail)args.DomainObject).ID = args.ID;
                ((ARReceiptDetail)args.DomainObject).MarkLoaded();
            }
        }

        /// <summary>
        /// Validate model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private List<DNetValidationResult> Validate(ARReceiptParameterDto model)
        {
            List<DNetValidationResult> results = new List<DNetValidationResult>();
            List<StandardCodeDto> enumSourceType = _enumBL.GetByCategory("ARReceiptDetail.SourceType");

            // Enum validation
            if (!_enumBL.IsExistByCategoryAndValue("ARReceipt.Type", ((int)(model.Type)).ToString())) { results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.Type))); }
            if (!_enumBL.IsExistByCategoryAndValue("ARReceipt.State", ((int)(model.State)).ToString())) { results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.State))); }

            foreach (ARReceiptDetailParameterDto i in model.ARReceiptDetails)
            {
                if (enumSourceType.Where(e => e.ValueId == i.SourceType).FirstOrDefault() == null) { results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.SourceType))); }
            }

            return results;
        }
        #endregion
    }
}

