#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : APPayment business logic class
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
    public class APPaymentBL : AbstractBusinessLogic, IAPPaymentBL
    {
        #region Variables
        private readonly IMapper _appaymentMapper;
        private readonly AutoMapper.IMapper _mapper;
        private TransactionManager _transactionManager;
        private StandardCodeBL _enumBL;
        #endregion

        #region Constructor
        public APPaymentBL()
        {
            _appaymentMapper = MapperFactory.GetInstance().GetMapper(typeof(APPayment).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _enumBL = new StandardCodeBL(_mapper);
            _transactionManager = new TransactionManager();
            _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get APPayment by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<APPaymentDto>> Read(APPaymentFilterDto filterDto, int pageSize)
        {
            // default filter to get the Active Row Status only
            var criterias = new CriteriaComposite(new Criteria(typeof(APPayment), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            var result = new ResponseBase<List<APPaymentDto>>();
            var sortColl = new SortCollection();
            var totalRow = 0;

            try
            {
                // populate the criterias
                criterias = Helper.UpdateCriteria(typeof(APPayment), filterDto, criterias);

                // populate the sort info
                sortColl = Helper.UpdateSortColumn(typeof(APPayment), filterDto, sortColl);

                // get data
                var data = _appaymentMapper.RetrieveByCriteria(criterias, sortColl, filterDto.pages, pageSize, ref totalRow);
                if (data.Count > 0)
                {
                    var list = data.Cast<APPayment>().ToList();
                    var listData = list.Select(item => _mapper.Map<APPaymentDto>(item)).ToList();

                    result.lst = listData;
                    result.total = totalRow;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(APPayment), filterDto);
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
        /// Delete APPayment by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<APPaymentDto> Delete(int id)
        {
            var result = new ResponseBase<APPaymentDto>();

            try
            {
                var appayment = (APPayment)_appaymentMapper.Retrieve(id);
                if (appayment != null)
                {
                    appayment.RowStatus = (short)DBRowStatus.Deleted;
                    var nResult = _appaymentMapper.Update(appayment, DNetUserName);
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
        /// Create a new APPayment
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<APPaymentDto> Create(APPaymentParameterDto objCreate)
        {
            // set default response
            var result = new ResponseBase<APPaymentDto>();

            try
            {
                List<DNetValidationResult> validationResults = new List<DNetValidationResult>();
                List<APPaymentDetail> domainDetailData = new List<APPaymentDetail>();

                if (objCreate.ID != Constants.NUMBER_DEFAULT_VALUE)
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgCreateID, objCreate.ID)));
                }

                objCreate.LastUpdateBy = DNetUserName;

                validationResults.AddRange(Validate(objCreate));
                if (validationResults.Count > 0)
                {
                    return PopulateValidationError<APPaymentDto>(validationResults, null);
                }

                APPayment domainData = _mapper.Map<APPayment>(objCreate);

                if (objCreate.APPaymentDetails.Count > 0)
                {
                    domainDetailData = _mapper.Map<IList<APPaymentDetailParameterDto>, IList<APPaymentDetail>>(objCreate.APPaymentDetails).ToList();
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
        /// Update APPayment
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<APPaymentDto> Update(APPaymentParameterDto objUpdate)
        {
            return null;
        }
        #endregion

        #region private method
        /// <summary>
        /// Insert data using trans manager
        /// </summary>
        /// <param name="appayment"></param>
        /// <param name="appaymentDetails"></param>
        /// <returns></returns>
        private int InsertWithTransactionManager(APPayment appayment, List<APPaymentDetail> appaymentDetails)
        {
            int result = -1;
            ;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // add command to insert data
                    this._transactionManager.AddInsert(appayment, DNetUserName);

                    // add command to insert detail data
                    foreach (APPaymentDetail item in appaymentDetails)
                    {
                        item.LastUpdateBy = DNetUserName;
                        item.APPayment = appayment;
                        this._transactionManager.AddInsert(item, DNetUserName);
                    }

                    this._transactionManager.PerformTransaction();
                    result = appayment.ID;
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
            if (args.DomainObject.GetType() == typeof(APPayment))
            {
                ((APPayment)args.DomainObject).ID = args.ID;
                ((APPayment)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(APPaymentDetail))
            {
                ((APPaymentDetail)args.DomainObject).ID = args.ID;
                ((APPaymentDetail)args.DomainObject).MarkLoaded();
            }
        }

        /// <summary>
        /// Validate model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private List<DNetValidationResult> Validate(APPaymentParameterDto model)
        {
            List<DNetValidationResult> results = new List<DNetValidationResult>();
            // Enum validation
            if (!_enumBL.IsExistByCategoryAndValue("APPayment.Type", ((int)(model.Type)).ToString())) { results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.Type))); }
            if (!_enumBL.IsExistByCategoryAndValue("APPayment.State", ((int)(model.State)).ToString())) { results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.State))); }

            List<StandardCodeDto> enumExDocType = _enumBL.GetByCategory("APPaymentDetail.ExternalDocumentType");
            List<StandardCodeDto> enumSourceType = _enumBL.GetByCategory("APPaymentDetail.SourceType");

            foreach (APPaymentDetailParameterDto i in model.APPaymentDetails)
            {
                if (enumExDocType.Where(e => e.ValueId == i.ExternalDocumentType).FirstOrDefault() == null) { results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.ExternalDocumentType))); }
                if (enumSourceType.Where(e => e.ValueId == i.SourceType).FirstOrDefault() == null) { results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.SourceType))); }
            }

            return results;
        }
        #endregion
    }
}

