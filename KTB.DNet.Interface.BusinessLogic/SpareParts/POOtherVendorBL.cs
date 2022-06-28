#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : POOtherVendor business logic class
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
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.ExceptionServices;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class POOtherVendorBL : AbstractBusinessLogic, IPOOtherVendorBL
    {
        #region Variables
        private readonly IMapper _poothervendorMapper;
        private TransactionManager _transactionManager;
        private readonly AutoMapper.IMapper _mapper;
        #endregion

        #region Constructor
        public POOtherVendorBL()
        {
            _poothervendorMapper = MapperFactory.GetInstance().GetMapper(typeof(POOtherVendor).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();

            this._transactionManager = new TransactionManager();
            _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);
        }
        #endregion

        #region Public Methods
        public ResponseBase<List<POOtherVendorDto>> Read(POOtherVendorFilterDto filterDto, int pageSize)
        {
            return null;
        }

        public ResponseBase<POOtherVendorDto> Delete(int id)
        {
            return null;
        }

        public ResponseBase<POOtherVendorDto> Update(POOtherVendorParameterDto objUpdate)
        {
            return null;
        }

        /// <summary>
        /// Create a new POOtherVendor
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<POOtherVendorDto> Create(POOtherVendorParameterDto objCreate)
        {
            var result = new ResponseBase<POOtherVendorDto>();
            var validationResults = new List<DNetValidationResult>();
            bool isValid = true;
            List<POOtherVendorDetail> poothervendorDetails = new List<POOtherVendorDetail>();

            POOtherVendor poothervendor = _mapper.Map<POOtherVendor>(objCreate);
            poothervendor.DealerCode = DealerCode;

            if (ValidatePOOtherVendor(objCreate, validationResults))
            {
                foreach (POOtherVendorDetailParameterDto item in objCreate.POOtherVendorDetails)
                {
                    var detail = _mapper.Map<POOtherVendorDetail>(item) as POOtherVendorDetail;
                    detail.DealerCode = DealerCode;

                    if (ValidatePOOtherVendorDetail(item, validationResults))
                    {
                        poothervendorDetails.Add(detail);
                    }
                }
            }

            isValid = validationResults.Count == 0;
            if (isValid)
            {
                try
                {
                    var createdObject = InsertWithTransactionManager(poothervendor, poothervendorDetails);
                    if (createdObject != null)
                    {
                        var obj = (POOtherVendor)_poothervendorMapper.Retrieve(createdObject.ID);
                        result._id = createdObject.ID;
                        result.lst = _mapper.Map<POOtherVendorDto>(obj);
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
                return PopulateValidationError<POOtherVendorDto>(validationResults, null);
            }

            return result;
        }

        /// <summary>
        /// Validate vendor
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        public bool ValidatePOOtherVendor(POOtherVendorParameterDto obj, List<DNetValidationResult> validationResults)
        {
            var stcodeBL = new StandardCodeBL(_mapper);
            if (!stcodeBL.IsExistByCategoryAndValue("DeliveryMethod", obj.DeliveryMethod.ToString()))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgEnumNotFound, FieldResource.DeliveryMethod)));
            }
            if (obj.PaymentGroup != Constants.NUMBER_DEFAULT_VALUE)
            {
                if (!stcodeBL.IsExistByCategoryAndValue("PaymentGroup", obj.PaymentGroup.ToString()))
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgEnumNotFound, FieldResource.PaymentGroup)));
                }
            }
            if (!stcodeBL.IsExistByCategoryAndValue("POOtherVendorState", obj.State.ToString()))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgEnumNotFound, FieldResource.State)));
            }
            if (!stcodeBL.IsExistByCategoryAndValue("Taxable", obj.Taxable.ToString()))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgEnumNotFound, FieldResource.Taxable)));
            }
            if (obj.FormSource != Constants.NUMBER_DEFAULT_VALUE)
            {
                if (!stcodeBL.IsExistByCategoryAndValue("FormSource", obj.FormSource.ToString()))
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgEnumNotFound, FieldResource.FromSource)));
                }
            }
            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate detail
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        public bool ValidatePOOtherVendorDetail(POOtherVendorDetailParameterDto obj, List<DNetValidationResult> validationResults)
        {
            var stcodeBL = new StandardCodeBL(_mapper);
            if (!stcodeBL.IsExistByCategoryAndValue("PurchaseFor", obj.PurchaseFor.ToString()))
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgEnumNotFound, FieldResource.PurchaseFor)));
            }
            if (obj.FormSource != Constants.NUMBER_DEFAULT_VALUE)
            {
                if (!stcodeBL.IsExistByCategoryAndValue("FormSource", obj.FormSource.ToString()))
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgEnumNotFound, FieldResource.FromSource)));
                }
            }
            if (obj.DealerCode != DealerCode)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDealerCode, FieldResource.DealerCode)));
            }
            return validationResults.Count == 0;
        }

        /// <summary>
        ///  Insert PO Other Vendor with transaction manager
        /// </summary>
        /// <param name="poothervendor"></param>
        /// <param name="poothervendorDetails"></param>
        /// <returns></returns>
        private POOtherVendor InsertWithTransactionManager(POOtherVendor poothervendor, List<POOtherVendorDetail> poothervendorDetails)
        {
            POOtherVendor result = null;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // add command to insert spare part PO
                    this._transactionManager.AddInsert(poothervendor, DNetUserName);

                    // add command to insert spare part PO detail
                    foreach (POOtherVendorDetail item in poothervendorDetails)
                    {
                        item.POOtherVendor = poothervendor;
                        this._transactionManager.AddInsert(item, DNetUserName);
                    }

                    this._transactionManager.PerformTransaction();
                    result = poothervendor;
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
            if (args.DomainObject.GetType() == typeof(POOtherVendor))
            {
                ((POOtherVendor)args.DomainObject).ID = args.ID;
                ((POOtherVendor)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(POOtherVendorDetail))
            {
                ((POOtherVendorDetail)args.DomainObject).ID = args.ID;
                ((POOtherVendorDetail)args.DomainObject).MarkLoaded();
            }
        }
        #endregion
    }
}

