#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SparePartPRFromVendor business logic class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

#region "Namespace Imports"
using KTB.DNet.Interface.BusinessLogic.Interface;
using KTB.DNet.Interface.BusinessLogic.MapperBL;
using KTB.DNet.DataMapper.Framework;
using KTB.DNet.Domain;
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
    public class SparePartPRFromVendorBL : AbstractBusinessLogic, ISparePartPRFromVendorBL
    {
        #region Variables
        private readonly IMapper _sparepartprfromvendorMapper;
        private readonly AutoMapper.IMapper _mapper;
        private TransactionManager _transactionManager;
        private StandardCodeBL _enumBL;
        #endregion

        #region Constructor
        public SparePartPRFromVendorBL()
        {
            _sparepartprfromvendorMapper = MapperFactory.GetInstance().GetMapper(typeof(SparePartPRFromVendor).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _transactionManager = new TransactionManager();
            _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);
            _enumBL = new StandardCodeBL(_mapper);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Create a new SparePartPRFromVendor
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<SparePartPRFromVendorDto> Create(SparePartPRFromVendorParameterDto objCreate)
        {
            var result = new ResponseBase<SparePartPRFromVendorDto>();

            try
            {
                List<DNetValidationResult> validationResults = new List<DNetValidationResult>();
                List<SparePartPRDetailFromVendor> domainDetailData = new List<SparePartPRDetailFromVendor>();

                #region validate model Attribute
                // validate enum
                validationResults.AddRange(ValidateEnum(objCreate));

                if (validationResults.Count > 0)
                {
                    return PopulateValidationError<SparePartPRFromVendorDto>(validationResults, null);
                }
                #endregion

                SparePartPRFromVendor domainData = _mapper.Map<SparePartPRFromVendor>(objCreate);
                var userName = Helper.GetUserName(DealerCode, UserName);

                if (objCreate.SparePartPRDetailFromVendors.Count > 0)
                {
                    domainDetailData = _mapper.Map<IList<SparePartPRDetailFromVendorParameterDto>, IList<SparePartPRDetailFromVendor>>(objCreate.SparePartPRDetailFromVendors).ToList();

                    domainDetailData = domainDetailData.Select(c =>
                    {
                        c.CreatedBy = DNetUserName;
                        c.LastUpdateBy = DNetUserName;
                        return c;
                    }).ToList();
                }

                if (validationResults.Any())
                {
                    return PopulateValidationError<SparePartPRFromVendorDto>(validationResults, null);
                }

                foreach (SparePartPRDetailFromVendor detail in domainDetailData)
                {
                    detail.SparePartPRFromVendor = domainData;
                }

                int insertedID = InsertWithTransactionManager(domainData, domainDetailData, userName);
                if (insertedID > 0)
                {
                    result.success = true;
                    result._id = insertedID;
                    result.total = 1;
                    result.lst = null;
                }
                else
                {
                    ErrorMsgHelper.ErrorMsgDBSave(result.messages);
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
        /// Update SparePartPRFromVendor
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<SparePartPRFromVendorDto> Update(SparePartPRFromVendorParameterDto objUpdate)
        {
            return null;
        }

        /// <summary>
        /// Delete SparePartPRFromVendor by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<SparePartPRFromVendorDto> Delete(int id)
        {
            return null;
        }

        /// <summary>
        /// Get SparePartPRFromVendor 
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<SparePartPRFromVendorDto>> Read(SparePartPRFromVendorFilterDto filterDto, int pageSize)
        {
            return null;
        }

        #endregion

        #region Private Method

        /// <summary>
        /// Insert data using transaction manager
        /// </summary>
        /// <param name="sparePartPRFromVendor"></param>
        /// <param name="sparePartPRDetailFromVendor"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        private int InsertWithTransactionManager(SparePartPRFromVendor sparePartPRFromVendor, List<SparePartPRDetailFromVendor> sparePartPRDetailFromVendor, string userName)
        {
            int result = -1;
            ;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // add command to insert data
                    this._transactionManager.AddInsert(sparePartPRFromVendor, userName);

                    // add command to insert detail data
                    foreach (SparePartPRDetailFromVendor item in sparePartPRDetailFromVendor)
                    {
                        this._transactionManager.AddInsert(item, userName);
                    }

                    this._transactionManager.PerformTransaction();
                    result = sparePartPRFromVendor.ID;
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
        /// Insert transaction manager handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void InsertWithTransactionManagerHandler(object sender, TransactionManager.OnInsertArgs args)
        {
            // set the object ID from db returned id
            if (args.DomainObject.GetType() == typeof(SparePartPRFromVendor))
            {
                ((SparePartPRFromVendor)args.DomainObject).ID = args.ID;
                ((SparePartPRFromVendor)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(SparePartPRDetailFromVendor))
            {
                ((SparePartPRDetailFromVendor)args.DomainObject).ID = args.ID;
                ((SparePartPRDetailFromVendor)args.DomainObject).MarkLoaded();
            }
        }

        /// <summary>
        /// Validate 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private List<DNetValidationResult> ValidateEnum(SparePartPRFromVendorParameterDto model)
        {
            List<DNetValidationResult> results = new List<DNetValidationResult>();

            if (!_enumBL.IsExistByCategoryAndValue("PRFromVendorType", ((int)(model.Type)).ToString())) { results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.Type))); }

            if (!_enumBL.IsExistByCategoryAndValue("PRFromVendorState", ((int)(model.State)).ToString())) { results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.State))); }

            if (!_enumBL.IsExistByCategoryAndValue("PRFromVendorHandling", ((int)(model.Handling)).ToString())) { results.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.Handling))); }

            return results;
        }

        #endregion
    }
}