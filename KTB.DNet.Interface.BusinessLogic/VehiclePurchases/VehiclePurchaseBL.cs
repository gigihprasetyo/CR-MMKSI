#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : VehiclePurchase business logic class
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
using KTB.DNet.Interface.Model;
using KTB.DNet.Interface.Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.ExceptionServices;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class VehiclePurchaseBL : AbstractBusinessLogic, IVehiclePurchaseBL
    {
        #region Variables
        private readonly IMapper _vehiclePurchaseHeaderMapper;
        private readonly IMapper _vehiclePurchaseDetailMapper;
        private readonly AutoMapper.IMapper _mapper;

        private TransactionManager _transactionManager;
        #endregion

        #region Constructor
        public VehiclePurchaseBL()
        {
            _vehiclePurchaseHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(VehiclePurchaseHeader).ToString());
            _vehiclePurchaseDetailMapper = MapperFactory.GetInstance().GetMapper(typeof(VehiclePurchaseDetail).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();

            this._transactionManager = new TransactionManager();
            _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);

        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="objUpdate"></param>
        /// <returns></returns>
        public ResponseBase<VehiclePurchaseHeaderDto> Update(VehiclePurchaseHeaderParameterDto objUpdate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Read
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VehiclePurchaseHeaderDto>> Read(VehiclePurchaseHeaderFilterDto filterDto, int pageSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<VehiclePurchaseHeaderDto> Delete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create a new VehiclePurchase
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<VehiclePurchaseHeaderDto> Create(VehiclePurchaseHeaderParameterDto objCreate)
        {
            // set default response
            var result = new ResponseBase<VehiclePurchaseHeaderDto>();
            var validationResults = new List<DNetValidationResult>();

            try
            {
                // create VehiclePurchaseHeader object
                VehiclePurchaseHeader VehiclePurchaseHeader = _mapper.Map<VehiclePurchaseHeader>(objCreate);
                VehiclePurchaseHeader.CreatedTime = DateTime.Now;
                List<VehiclePurchaseDetail> listOfVehiclePurchaseDetail = new List<VehiclePurchaseDetail>();

                foreach (VehiclePurchaseDetailParameterDto detail in objCreate.VehiclePurchaseDetails)
                {
                    listOfVehiclePurchaseDetail.Add(_mapper.Map<VehiclePurchaseDetail>(detail));
                }

                int insertResult = InsertWithTransactionManager(VehiclePurchaseHeader, listOfVehiclePurchaseDetail);
                if (insertResult > 0)
                {
                    result.success = true;
                    result._id = insertResult;
                    result.total = 1;
                }
                else
                {
                    ErrorMsgHelper.ErrorMsgDBSave(result.messages, FieldResource.VehiclePurchase);
                }
            }
            catch (Exception ex)
            {
                ErrorMsgHelper.SqlException(result.messages, ex.Message);
            }

            return result;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Insert spk with transaction manager
        /// </summary>
        /// <param name="VehiclePurchaseHeader"></param>
        /// <returns></returns>
        private int InsertWithTransactionManager(VehiclePurchaseHeader VehiclePurchaseHeader, List<VehiclePurchaseDetail> VehiclePurchaseDetails)
        {
            int result = -1;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // add command to insert VehiclePurchase header
                    this._transactionManager.AddInsert(VehiclePurchaseHeader, DNetUserName);

                    // add command to insert VehiclePurchase detail
                    foreach (VehiclePurchaseDetail item in VehiclePurchaseDetails)
                    {
                        item.VehiclePurchaseHeader = VehiclePurchaseHeader;
                        this._transactionManager.AddInsert(item, DNetUserName);
                    }

                    this._transactionManager.PerformTransaction();
                    result = VehiclePurchaseHeader.ID;
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
            if (args.DomainObject.GetType() == typeof(VehiclePurchaseHeader))
            {
                ((VehiclePurchaseHeader)args.DomainObject).ID = args.ID;
                ((VehiclePurchaseHeader)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(VehiclePurchaseDetail))
            {
                ((VehiclePurchaseDetail)args.DomainObject).ID = args.ID;
                ((VehiclePurchaseDetail)args.DomainObject).MarkLoaded();
            }
        }
        #endregion
    }
}
