using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KTB.DNet.Domain;
using KTB.DNet.Domain.Search;
using KTB.DNet.DataMapper.Framework;

namespace KTB.DNet.BusinessValidation
{
    public class ServiceReminderValidation
    {

        #region Constructor
        public ServiceReminderValidation()
        { }
        #endregion

        /// <summary>Validates the chassis number.</summary>
        /// <param name="chassisNumber">The chassis number.</param>
        /// <param name="validResultList">The valid result list.</param>
        /// <param name="chassisMaster">The chassis master.</param>
        /// <returns></returns>
        public static bool ValidateSvcReminder(ServiceReminder svcReminder, ref string errMsg)
        {
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(ServiceReminder).ToString());

            var svcReminderObj = _mapper.Retrieve(svcReminder.ID);

            if (svcReminderObj == null)
            {
                errMsg = "Reminder is not exist";
                return false;
            }

            var tempDays = DateTime.Today - svcReminder.MaxFUDealerDate;

            if (tempDays.TotalDays < -44 || tempDays.TotalDays > 0)
            {
                errMsg = "Reminder is not exist!";
                return false;
            }

            return true;
        }

        public static bool ValidateSvcReminder(int id, ref string errMsg)
        {
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(ServiceReminder).ToString());

            var svcReminderObj = (ServiceReminder)_mapper.Retrieve(id);

            if (svcReminderObj == null)
            {
                errMsg = "Reminder is not exist";
                return false;
            }

            var tempDays = DateTime.Today - svcReminderObj.MaxFUDealerDate;

            if (tempDays.TotalDays < -44 || tempDays.TotalDays > 0)
            {
                errMsg = "Reminder is not exist!";
                return false;
            }

            return true;
        }

        /// <summary>Validates the chassis number.</summary>
        /// <param name="chassisNumber">The chassis number.</param>
        /// <param name="validResultList">The valid result list.</param>
        /// <param name="chassisMaster">The chassis master.</param>
        /// <returns></returns>
        public static bool ValidateCompleteSvcReminder(ServiceReminder svcReminder, ref string errMsg )
        {
            //
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(StandardCode).ToString());
            var criteria = new CriteriaComposite(new Criteria(typeof(StandardCode), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteria.opAnd(new Criteria(typeof(StandardCode), "Category", MatchType.Exact, "GlobalServiceReminder.Status"));
            // kurang Service reminder id ?
            var enumSvcReminderStatus = _mapper.RetrieveByCriteria(criteria);

            StandardCode temp = new StandardCode();
            foreach(StandardCode e in enumSvcReminderStatus)
            {
                temp = e;
                if (e.ValueCode == "Complete")
                    break;
            }

            var _serviceReminderMapper = MapperFactory.GetInstance().GetMapper(typeof(ServiceReminder).ToString());
            var svcReminderObj = (ServiceReminder)_serviceReminderMapper.Retrieve(svcReminder.ID);

            if (svcReminderObj != null && svcReminderObj.Status == temp.ValueId)
            {
                errMsg = "Cannot create new follow up of reminder with status complete";
                return false;
            }

            return true;
        }

        public static bool ValidateWOForIncompleteSvcReminder(ServiceReminder svcReminder, ref string errMsg)
        {
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(StandardCode).ToString());
            var criteria = new CriteriaComposite(new Criteria(typeof(StandardCode), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteria.opAnd(new Criteria(typeof(StandardCode), "Category", MatchType.Exact, "GlobalServiceReminder.Status"));
            var enumSvcReminderStatus = _mapper.RetrieveByCriteria(criteria);

            StandardCode temp = new StandardCode();
            foreach (StandardCode e in enumSvcReminderStatus)
            {
                temp = e;
                if (e.ValueCode == "Complete")
                    break;
            }

            if (svcReminder.Status != temp.ValueId && (svcReminder.WONumber != "" || !string.IsNullOrEmpty(svcReminder.WONumber)))
            {
                errMsg = "Cannot input WO Number";
                return false;
            }

            return true;
        }

        public static bool ValidateFollowUp(ref ServiceReminderFollowUp serviceReminderFollowUp, ref List<ValidResult> resultList, string dealerCode = "")
        {
            var validResultList = new List<ValidResult>();
            var isValid = false;
            var validLst = new List<bool>();
            var errMsg = string.Empty;

            //validate ServiceReminder
            isValid = ValidateSvcReminder(serviceReminderFollowUp.ServiceReminder.ID, ref errMsg);
            if(!isValid)
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20007,
                    Message = errMsg
                };
                resultList.Add(validResult);
            }

            validLst.Add(isValid);

           // validate WO can't input on incomplete servicer reminder
            //if (serviceReminderFollowUp.ServiceReminder.WONumber != string.Empty || !string.IsNullOrWhiteSpace(serviceReminderFollowUp.ServiceReminder.WONumber))
            //{
                isValid = ValidateWOForIncompleteSvcReminder(serviceReminderFollowUp.ServiceReminder, ref errMsg);
                if (!isValid)
                {
                    var validResult = new ValidResult()
                    {
                        IsValid = false,
                        ErrorCode = 20008,
                        Message = errMsg
                    };
                    resultList.Add(validResult);
                }

                validLst.Add(isValid);
            //}

            //validate can't add new followup to complete service reminder
            /*
            isValid = ValidateCompleteSvcReminder(serviceReminderFollowUp.ServiceReminder, ref errMsg);
            if (!isValid)
            {
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = errMsg
                };
                resultList.Add(validResult);
            }

            validLst.Add(isValid);
            */

            //validate booking date not greather than 14 days from MaxFUDealerDate
            var marginBookingDate = (serviceReminderFollowUp.BookingDate - serviceReminderFollowUp.ServiceReminder.MaxFUDealerDate).TotalDays;
            if (marginBookingDate > 45) //tambahkan di tabel appconfig
            {
                isValid = false;
                var validResult = new ValidResult()
                {
                    IsValid = false,
                    ErrorCode = 20009,
                    Message = "Booking Date tidak boleh lebih dari 45 hari setelah MaxFUDealerDate"
                };
                resultList.Add(validResult);
            }
            else
                isValid = true;

            validLst.Add(isValid);

            for (int i = 0; i < validLst.Count; i++)
                isValid = isValid & validLst[i];

                return isValid;
        }
    }
}
