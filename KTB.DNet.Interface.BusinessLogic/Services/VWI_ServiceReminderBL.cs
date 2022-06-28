#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ServiceReminder business logic class
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
using KTB.DNet.BusinessValidation;
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
using KTB.DNet.Interface.Framework;
using System.Runtime.ExceptionServices;
using KTB.DNet.Interface.Repository.Interface;
using System.Globalization;
#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public class VWI_ServiceReminderBL : AbstractBusinessLogic, IVWI_ServiceReminderBL
    {
        #region Variables
        private readonly IMapper _vwiServiceReminderMapper;
        private readonly IMapper _serviceReminderMapper;
        private readonly IMapper _dealerMapper;
        private readonly IMapper _serviceReminderFollowUpMapper;
        private TransactionManager _transactionManager;
        private readonly AutoMapper.IMapper _mapper;
        private StandardCodeBL _enumBL;
        private StandardCodeCharBL _enumCharBL;
        private IServiceReminderRepository<KTB.DNet.Interface.Domain.ServiceReminder, int> _ServiceReminderRepo;

        #endregion

        #region Constructor
        public VWI_ServiceReminderBL(IServiceReminderRepository<KTB.DNet.Interface.Domain.ServiceReminder, int> ServiceReminderRepo)
        {
            _ServiceReminderRepo = ServiceReminderRepo;
            _serviceReminderMapper = MapperFactory.GetInstance().GetMapper(typeof(ServiceReminder).ToString());
            _dealerMapper = MapperFactory.GetInstance().GetMapper(typeof(Dealer).ToString());
            _vwiServiceReminderMapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_ServiceReminder).ToString());
            _serviceReminderFollowUpMapper = MapperFactory.GetInstance().GetMapper(typeof(ServiceReminderFollowUp).ToString());
            _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
            _enumBL = new StandardCodeBL(_mapper);
            _enumCharBL = new StandardCodeCharBL(_mapper);
            _transactionManager = new TransactionManager();
            _transactionManager.Insert += new TransactionManager.OnInsertEventHandler(InsertWithTransactionManagerHandler);

        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get ServiceReminder by certain criteria
        /// </summary>
        /// <param name="filterDto"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseBase<List<VWI_ServiceReminderDto>> Read(VWI_ServiceReminderFilterDto filterDto, int pageSize)
        {
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_ServiceReminder), "DealerCode", MatchType.Exact, this.DealerCode));

            var result = new ResponseBase<List<VWI_ServiceReminderDto>>();
            var sortColl = new SortCollection();

            try
            {
                // define sql
                var sql = Helper.GenerateSQLFromCriteriasAndSort(typeof(VWI_ServiceReminder), filterDto, sortColl, criterias);

                // get data
                var data = _vwiServiceReminderMapper.RetrieveSP("SELECT * FROM VWI_ServiceReminder " + sql);
                if (data.Count > 0)
                {
                    // calculate the skip 
                    int skip = filterDto.pages < 1 ? 0 : (filterDto.pages - 1) * pageSize;

                    // filter out the data based on the paging                    
                    List<VWI_ServiceReminder> list = new List<VWI_ServiceReminder>();
                    if (sortColl != null && sortColl.Count > 0)
                        list = data.Cast<VWI_ServiceReminder>().Skip(skip).Take(pageSize).ToList();
                    else
                        list = data.Cast<VWI_ServiceReminder>().OrderBy(x => x.ID).Skip(skip).Take(pageSize).ToList();

                    // convert to dto object
                    List<VWI_ServiceReminderDto> listData = list.ConvertList<VWI_ServiceReminder, VWI_ServiceReminderDto>();

                    result.lst = listData;
                    result.total = data.Count;
                }
                else
                {
                    ErrorMsgHelper.DataNotFound(result.messages, typeof(VWI_ServiceReminder), filterDto);
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
        /// Create a new ServiceReminder
        /// </summary>
        /// <param name="objCreate"></param>
        /// <returns></returns>
        public ResponseBase<ServiceReminderFollowUpDto> FollowUp(ServiceReminderFollowUpParameterDto objCreate)
        {
            #region Declare
            var result = new ResponseBase<ServiceReminderFollowUpDto>();
            var validationResults = new List<DNetValidationResult>();
            var validResultList = new List<ValidResult>();
            KTB.DNet.Interface.Domain.ServiceReminder serviceReminder = null;
            Dealer dealer = null;
            PMKind pmKind = null;
            var isValid = true;
            int StatusServiceReminder = 0;
            #endregion

            try
            {
                // parse the parameter into object
                ServiceReminderFollowUp newServiceReminderFollowUp = new ServiceReminderFollowUp();

                // validate service reminder follow up parameter values
                isValid = ValidateServiceReminderFollowUp(objCreate, validationResults, ref validResultList, ref newServiceReminderFollowUp, ref StatusServiceReminder, ref serviceReminder);

                // insert if valid
                if (isValid)
                {
                    var success = (int)_serviceReminderFollowUpMapper.Insert(newServiceReminderFollowUp, DNetUserName);
                    var ID = success;
                    result.success = success > 0;
                    if (!result.success)
                    {
                        ErrorMsgHelper.ErrorMsgDBSave(result.messages);
                    }
                    else
                    {
                        KTB.DNet.Interface.Domain.ServiceReminder newServiceReminder = serviceReminder;
                        newServiceReminder.Status = objCreate.FollowUpStatus;
                        newServiceReminder.BookingDate = newServiceReminderFollowUp.BookingDate;
                        DateTime dateTime = DateTime.ParseExact(objCreate.BookingDate.ToString("HH:mm:ss"), "HH:mm:ss",
                                        CultureInfo.InvariantCulture);
                        newServiceReminder.BookingTime = dateTime;
                        newServiceReminder.LastUpdateBy = DNetUserName;
                        newServiceReminder.LastUpdateTime = DateTime.Now;
                        if(serviceReminder.ServiceActualDate==null)
                        {
                            newServiceReminder.ServiceActualDate = null;
                        }

                        // update maxfudate if booking date more than maxfudate on service reminder
                        if (newServiceReminder.MaxFUDealerDate < newServiceReminderFollowUp.BookingDate)
                        {
                            newServiceReminder.MaxFUDealerDate = newServiceReminderFollowUp.BookingDate;
                        }
                        // update wonumber if followUpStatus is complete
                        if (newServiceReminderFollowUp.FollowUpStatus == 3)
                        {
                            newServiceReminder.WONumber = objCreate.WorkOrderNumber;
                        }
                        if (StatusServiceReminder <= 3)
                        {
                            var successupdate = _ServiceReminderRepo.Update(newServiceReminder);
                            result.success = successupdate.Success;
                            if (!result.success)
                            {
                                ErrorMsgHelper.ErrorMsgDBSave(result.messages);
                            }else
                            {
                                result._id = ID;
                                result.success = true;
                                result.total = 1;
                            }
                        }
                    }
                    //int success = InsertUpdateWithTransactionManager(newServiceReminderFollowUp, newServiceReminder, StatusServiceReminder);
                    //if (success < 0)
                    //{
                    //    result.success = false;
                    //    result.total = 0;
                    //    ErrorMsgHelper.ErrorMsgDBSave(result.messages);
                    //}
                    //else
                    //{
                    //    // return output ID
                    //    result._id = success;
                    //    result.success = true;
                    //    result.total = 1;
                    //}
                }
                else
                {
                    return PopulateValidationError<ServiceReminderFollowUpDto>(validationResults, null);
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
        /// Validate free service
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        public bool ValidateServiceReminderFollowUp(ServiceReminderFollowUpParameterDto objCreate, List<DNetValidationResult> validationResults, ref List<ValidResult> validResultList, ref ServiceReminderFollowUp newServiceReminderFollowUp, ref int StatusServiceReminder, ref KTB.DNet.Interface.Domain.ServiceReminder serviceReminder)
        {
            #region Map Free Service from Parameter
            newServiceReminderFollowUp = new ServiceReminderFollowUp();
            var isValid = false;
            var sortColl = string.Empty;
            int totalRow = 0;
            int filteredTotalRow = 0;
            var innerQueryCriteria = string.Empty;
            var page = 1;
            var size = 20;
            var criterias = "";
            criterias = Helper.UpdateStrCriteria(typeof(KTB.DNet.Interface.Domain.ServiceReminder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "ID", objCreate.ServiceReminderID.ToString(), false, criterias);
            criterias = Helper.UpdateStrCriteria(typeof(KTB.DNet.Interface.Domain.ServiceReminder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criterias);

            var data = _ServiceReminderRepo.Search(
                                criterias, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

            if(data.Count==0)
            {
                validationResults.Add(new DNetValidationResult("Service Reminder ID "+ objCreate.ServiceReminderID.ToString() + " tidak ditemukan"));
            }
            else
            {
                var newServiceReminder = data.FirstOrDefault() as KTB.DNet.Interface.Domain.ServiceReminder;

                StatusServiceReminder = newServiceReminder.Status;

                var dealerServiceReminder = (Dealer)_dealerMapper.Retrieve(newServiceReminder.DealerID);
                //var new_ServiceReminder = (ServiceReminder)_serviceReminderMapper.Retrieve(objCreate.ServiceReminderID);

                if (dealerServiceReminder.DealerCode != DealerCode)
                {
                    validationResults.Add(new DNetValidationResult("Dealer service reminder harus sama dengan dealer request."));
                    return false;
                }

                if (newServiceReminder != null)
                {
                    if (objCreate.FollowUpStatus == 2 || objCreate.FollowUpStatus == 3)
                    {
                        newServiceReminder.Status = objCreate.FollowUpStatus;
                        //newServiceReminder.MarkLoaded();
                    }

                    if (objCreate.FollowUpStatus == 2)
                    {
                        newServiceReminder.WONumber = string.Empty;
                    }
                    else if (objCreate.FollowUpStatus == 3)
                    {
                        newServiceReminder.WONumber = objCreate.WorkOrderNumber;
                    }
                }
                serviceReminder = newServiceReminder;
                KTB.DNet.Domain.ServiceReminder svr = newServiceReminder.ConvertObject<KTB.DNet.Domain.ServiceReminder>();

                //cannot map via automapper
                newServiceReminderFollowUp.ServiceReminder = svr;
                newServiceReminderFollowUp.FollowUpAction = objCreate.FollowUpAction;
                newServiceReminderFollowUp.FollowUpDate = objCreate.FollowUpDate;
                newServiceReminderFollowUp.FollowUpStatus = objCreate.FollowUpStatus;
                newServiceReminderFollowUp.BookingDate = objCreate.BookingDate;
                newServiceReminderFollowUp.LastUpdateBy = DNetUserName;
                newServiceReminderFollowUp.LastUpdateTime = DateTime.Now;

                #endregion
                var ServiceReminderValidation = new ServiceReminderValidation();
                isValid = ServiceReminderValidation.ValidateFollowUp(ref newServiceReminderFollowUp, ref validResultList, this.DealerCode);
                if (validResultList.Count > 0)
                {
                    foreach (var validResult in validResultList)
                    {
                        validationResults.Add(new DNetValidationResult(validResult.Message));
                    }
                    return false;
                }
                else
                {
                    return true;
                }
            }
             return true;
        }

        public ResponseBase<VWI_ServiceReminderDto> Update(ServiceReminderFollowUpParameterDto objUpdate)
        {
            throw new NotImplementedException();
        }

        public ResponseBase<VWI_ServiceReminderDto> Delete(int id)
        {
            throw new NotImplementedException();
        }

        ResponseBase<VWI_ServiceReminderDto> IBaseInterface<ServiceReminderFollowUpParameterDto, VWI_ServiceReminderFilterDto, VWI_ServiceReminderDto>.Create(ServiceReminderFollowUpParameterDto objCreate)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Handler on executed insert command with transaction manager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void InsertWithTransactionManagerHandler(object sender, TransactionManager.OnInsertArgs args)
        {
            // set the object ID from db returned id
            if (args.DomainObject.GetType() == typeof(ServiceReminderFollowUp))
            {
                ((ServiceReminderFollowUp)args.DomainObject).ID = args.ID;
                ((ServiceReminderFollowUp)args.DomainObject).MarkLoaded();
            }
            else if (args.DomainObject.GetType() == typeof(ServiceReminder))
            {
                ((ServiceReminder)args.DomainObject).ID = args.ID;
                ((ServiceReminder)args.DomainObject).MarkLoaded();
            }
        }


        private int InsertUpdateWithTransactionManager(ServiceReminderFollowUp dataFollowUp, ServiceReminder dataServiceReminder, int StatusServiceReminder)
        {
            // mark as loaded to prevent it loads from db
            dataFollowUp.ServiceReminder.MarkLoaded();
            dataFollowUp.MarkLoaded();

            // set default result
            int result = -1;
            if (this.IsTaskFree())
            {
                try
                {
                    _transactionManager.AddInsert(dataFollowUp, DNetUserName);
                    if (StatusServiceReminder <= 3)
                    {
                        _transactionManager.AddUpdate(dataServiceReminder, DNetUserName);
                    }
                    _transactionManager.PerformTransaction();
                    result = dataFollowUp.ID;
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
        #endregion
    }
}

