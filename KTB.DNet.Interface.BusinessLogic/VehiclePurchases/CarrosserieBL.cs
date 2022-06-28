#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : Carrosserie business logic class
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
    public class CarrosserieBL : AbstractBusinessLogic, ICarrosserieBL
    {
        #region Variables
        private readonly IMapper _carrosserieHeaderMapper;
        private readonly IMapper _carrosserieDetailMapper;
        private readonly AutoMapper.IMapper _mapper;

        private TransactionManager _transactionManager;
        #endregion

        #region Constructor
        public CarrosserieBL()
        {
            _carrosserieHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(CarrosserieHeader).ToString());
            _carrosserieDetailMapper = MapperFactory.GetInstance().GetMapper(typeof(CarrosserieDetail).ToString());
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
        public ResponseBase<CarrosserieHeaderDto> Update(CarrosserieHeaderParameterDto objUpdate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Read
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<CarrosserieHeaderDto>> Read(CarrosserieHeaderFilterDto filterDto, int pageSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseBase<CarrosserieHeaderDto> Delete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create a new Carrosserie
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<CarrosserieHeaderDto> Create(CarrosserieHeaderParameterDto objCreate)
        {
            // set default response
            var result = new ResponseBase<CarrosserieHeaderDto>();
            var validationResults = new List<DNetValidationResult>();

            try
            {
                // create CarrosserieHeader object
                CarrosserieHeader carrosserieHeader = _mapper.Map<CarrosserieHeader>(objCreate);
                carrosserieHeader.CreatedTime = DateTime.Now;
                List<CarrosserieDetail> listOfCarrosserieDetail = new List<CarrosserieDetail>();

                foreach (CarrosserieDetailParameterDto detail in objCreate.CarrosserieDetails)
                {
                    listOfCarrosserieDetail.Add(_mapper.Map<CarrosserieDetail>(detail));
                }

                int insertResult = InsertWithTransactionManager(carrosserieHeader, listOfCarrosserieDetail);
                if (insertResult > 0)
                {
                    result.success = true;
                    result._id = insertResult;
                    result.total = 1;
                }
                else
                {
                    ErrorMsgHelper.ErrorMsgDBSave(result.messages, FieldResource.CarrosserieHeader);
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
        /// Insert spk with transaction manager
        /// </summary>
        /// <param name="carrosserieHeader"></param>
        /// <returns></returns>
        private int InsertWithTransactionManager(CarrosserieHeader carrosserieHeader, List<CarrosserieDetail> carrosserieDetails)
        {
            int result = -1;
            if (this.IsTaskFree())
            {
                try
                {
                    this.SetTaskLocking();

                    // add command to insert carrosserie header
                    this._transactionManager.AddInsert(carrosserieHeader, DNetUserName);

                    // add command to insert carrosserie detail
                    foreach (CarrosserieDetail item in carrosserieDetails)
                    {
                        item.CarrosserieHeader = carrosserieHeader;
                        this._transactionManager.AddInsert(item, DNetUserName);
                    }

                    this._transactionManager.PerformTransaction();
                    result = carrosserieHeader.ID;
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
            if (args.DomainObject.GetType() == typeof(CarrosserieHeader))
            {
                ((CarrosserieHeader)args.DomainObject).ID = args.ID;
                ((CarrosserieHeader)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(CarrosserieDetail))
            {
                ((CarrosserieDetail)args.DomainObject).ID = args.ID;
                ((CarrosserieDetail)args.DomainObject).MarkLoaded();
            }
        }
        #endregion
    }
}

