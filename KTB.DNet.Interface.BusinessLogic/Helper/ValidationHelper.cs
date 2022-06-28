#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ValidationHelper class
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
using IFDomain = KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Repository.Dapper;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using KTB.DNet.Interface.Repository.Dapper.AccountingData;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper.NonDMS;

#endregion

namespace KTB.DNet.Interface.BusinessLogic
{
    public static class ValidationHelper
    {
        #region Validate Methods
        /// <summary>
        /// Validate service date
        /// </summary>
        /// <param name="objCreate"></param>
        /// <param name="validationResults"></param>
        /// <param name="isBB"></param>
        /// <returns></returns>
        public static bool ValidateServiceDate(DateTime serviceDate, List<DNetValidationResult> validationResults)
        {
            if ((serviceDate <= System.Data.SqlTypes.SqlDateTime.MinValue.Value) || (serviceDate >= System.Data.SqlTypes.SqlDateTime.MaxValue.Value))
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.DataTypeOrDataFormatInvalid, string.Format(ValidationResource.InvalidDateFormat, FieldResource.ServiceDate, serviceDate)));
            }

            if (serviceDate.Date > DateTime.Now.Date)
            {
                validationResults.Add(new DNetValidationResult(ErrorCode.DataTypeOrDataFormatInvalid, string.Format(ValidationResource.ServiceDateGreaterThanTodayDate, serviceDate, DateTime.Now.ToString("ddMMyyyy"))));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate chassis and engine
        /// </summary>
        /// <param name="chassisNumber"></param>
        /// <param name="engineNumber"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        public static bool ValidateChassisAndEngine(string chassisNumber, string engineNumber, List<DNetValidationResult> validationResults, ref ChassisMaster chassisMaster)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMaster).ToString());

            // get by criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(ChassisMaster), "ChassisNumber", "Category.ProductCategory.Code", chassisNumber, ConfigurationManager.AppSettings["CompanyCode"]));
            if (masters.Count == 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgChassisNotFound, chassisNumber)));
            }
            else
            {
                var tempCM = masters[0] as ChassisMaster;
                if (!tempCM.EngineNumber.Equals(engineNumber, StringComparison.OrdinalIgnoreCase))
                {
                    validationResults.Add(new DNetValidationResult(string.Format(ValidationResource.ChassisAndEngineDoesNotMatch, engineNumber, chassisNumber)));
                }
                else
                {
                    chassisMaster = tempCM;
                }
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate passed dealer code parameter
        /// </summary>
        /// <param name="dealerCode"></param>
        /// <param name="validationResults"></param>
        /// <param name="loginDealerCode"></param>
        /// <param name="isValid"></param>
        /// <param name="dealer"></param>
        public static bool ValidateDealer(string dealerCode, List<DNetValidationResult> validationResults, string loginDealerCode, ref Dealer dealer, bool isCompareToLoginDealerCode = true)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(Dealer).ToString());

            // get by criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(Dealer), "DealerCode", dealerCode));
            if (masters.Count > 0)
            {
                // cast the object
                dealer = masters[0] as Dealer;

                // validate dealer code against login dealer code
                if (isCompareToLoginDealerCode && !dealerCode.Equals(loginDealerCode, System.StringComparison.OrdinalIgnoreCase))
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDealerCodeNotMatch, dealerCode, loginDealerCode)));
                }
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.DealerCode, dealerCode)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Cek revision faktur exists by chassis number
        /// </summary>
        /// <param name="chassisNumber"></param>
        /// <returns></returns>
        public static bool IsRevisionFakturExists(string chassisNumber)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(RevisionFaktur).ToString());

            // criteria
            var criterias = new CriteriaComposite(new Criteria(typeof(RevisionFaktur), "RowStatus", MatchType.Exact, (int)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(RevisionFaktur), "ChassisMaster.ChassisNumber", MatchType.Exact, chassisNumber));


            var data = _mapper.RetrieveByCriteria(criterias);
            if (data.Count > 0)
            {
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// Validate dealer branch code
        /// </summary>
        /// <param name="dealerCode"></param>
        /// <param name="validationResults"></param>
        /// <param name="dealerBranchCode"></param>
        /// <param name="dealerBranch"></param>
        /// <returns></returns>
        public static bool ValidateDealerBranch(string dealerCode, List<DNetValidationResult> validationResults, string dealerBranchCode, ref DealerBranch dealerBranch)
        {
            // no need to validate
            if (string.IsNullOrEmpty(dealerBranchCode))
                return true;

            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(DealerBranch).ToString());

            // get by criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(DealerBranch), "DealerBranchCode", "Status", dealerBranchCode, "1"));
            if (masters.Count > 0)
            {
                // cast the object
                dealerBranch = masters[0] as DealerBranch;

                // validate dealer branch against dealer
                if (!dealerBranch.Dealer.DealerCode.Equals(dealerCode, System.StringComparison.OrdinalIgnoreCase))
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDealerBranchCodeNotMatch, dealerBranchCode, dealerCode)));
                }
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNotUsual, FieldResource.DealerBranchCode)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate passed chassis number parameter
        /// </summary>
        /// <param name="chassisNumber"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        /// <param name="chassisMaster"></param>
        public static bool ValidateChassisMaster(string chassisNumber, List<DNetValidationResult> validationResults, ref ChassisMaster chassisMaster)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMaster).ToString());

            // get by criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(ChassisMaster), "ChassisNumber", "Category.ProductCategory.Code", chassisNumber, ConfigurationManager.AppSettings["CompanyCode"]));
            if (masters.Count > 0)
            {
                // cast the object
                chassisMaster = masters[0] as ChassisMaster;
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.ChassisNumber, chassisNumber)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// to validate chassis by all rowstatus
        /// </summary>
        /// <param name="chassisNumber"></param>
        /// <param name="validationResults"></param>
        /// <param name="chassisMaster"></param>
        /// <returns></returns>
        public static bool ValidateChassisMasterAll(string chassisNumber, List<DNetValidationResult> validationResults, ref ChassisMaster chassisMaster)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMaster).ToString());

            // criteria
            var criterias = new CriteriaComposite(new Criteria(typeof(ChassisMaster), "ChassisNumber", MatchType.Exact, chassisNumber));
            criterias.opAnd(new Criteria(typeof(ChassisMaster), "Category.ProductCategory.Code", MatchType.Exact, ConfigurationManager.AppSettings["CompanyCode"]));

            // get by criteria
            var masters = _mapper.RetrieveByCriteria(criterias);
            if (masters.Count > 0)
            {
                // cast the object
                chassisMaster = masters[0] as ChassisMaster;
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.ChassisNumber, chassisNumber)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate passed chassis number parameter
        /// </summary>
        /// <param name="chassisNumber"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        /// <param name="chassisMasterBB"></param>
        public static bool ValidateChassisMasterBB(string chassisNumber, List<DNetValidationResult> validationResults, ref ChassisMasterBB chassisMasterBB)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMasterBB).ToString());

            // get by criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(ChassisMasterBB), "ChassisNumber", "Category.ProductCategory.Code", chassisNumber, ConfigurationManager.AppSettings["CompanyCode"]));
            if (masters.Count > 0)
            {
                // cast the object
                chassisMasterBB = masters[0] as ChassisMasterBB;
                if (validationResults.Count > 0)
                {
                    for (var i = 0; i < validationResults.Count; i++)
                    {
                        if (validationResults[i].ErrorMessage.Contains(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.ChassisNumber, chassisNumber)))
                        {
                            validationResults.RemoveAt(i);
                        }
                    }
                }
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.ChassisNumber, chassisNumber)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate passed chassis number parameter
        /// </summary>
        /// <param name="chassisNumber"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        /// <param name="chassisMasterPKT"></param>
        public static bool ValidateChassisMasterPKT(string chassisNumber, List<DNetValidationResult> validationResults, ref ChassisMasterPKT chassisMasterPKT)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(ChassisMasterPKT).ToString());

            // get by criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(ChassisMasterPKT), "ChassisMaster.ChassisNumber", chassisNumber));
            if (masters.Count > 0)
            {
                // cast the object
                chassisMasterPKT = masters[0] as ChassisMasterPKT;
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.ChassisNumber, chassisNumber)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate salesman header
        /// </summary>
        /// <param name="salesmanCode"></param>
        /// <param name="dealerCode"></param>
        /// <param name="validationResults"></param>
        /// <param name="salesmanHeader"></param>
        /// <param name="isValidateDealerCode"></param>
        /// <returns></returns>
        public static bool ValidateSalesmanHeader(string salesmanCode, string dealerCode, List<DNetValidationResult> validationResults, ref SalesmanHeader salesmanHeader, bool isValidateDealerCode = true)
        {
            if (string.IsNullOrEmpty(salesmanCode))
                return true;

            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(SalesmanHeader).ToString());

            // get by criteria
            CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(SalesmanHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteria.opAnd(new Criteria(typeof(SalesmanHeader), "SalesmanCode", MatchType.Exact, salesmanCode));
            if (isValidateDealerCode)
            {
                criteria.opAnd(new Criteria(typeof(SalesmanHeader), "Dealer.DealerCode", MatchType.Exact, dealerCode));
            }

            var masters = _mapper.RetrieveByCriteria(criteria);
            if (masters.Count > 0)
            {
                // cast the object
                salesmanHeader = masters[0] as SalesmanHeader;
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.SalesmanHeader, salesmanCode)));
            }

            return validationResults.Count == 0;
        }

        public static bool ValidateSalesmanHeaderEmployeeSales(string salesmanCode, string dealerCode, List<DNetValidationResult> validationResults, ref SalesmanHeader salesmanHeader, bool isValidateDealerCode = true)
        {
            if (string.IsNullOrEmpty(salesmanCode))
                return true;

            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(SalesmanHeader).ToString());
            var _dealerMapper = MapperFactory.GetInstance().GetMapper(typeof(Dealer).ToString());

            var dataDealer = _dealerMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(Dealer), "DealerCode", dealerCode));
            bool isMainDealer = false;
            string parentDealerCode = string.Empty;
            if (dataDealer.Count > 0)
            {
                Dealer d = dataDealer[0] as Dealer;
                isMainDealer = d.MainDealer.ID == d.ID;
                if (d.MainDealer.ID != d.ID)
                    parentDealerCode = d.MainDealer.DealerCode;
            }

            // get by criteria
            CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(SalesmanHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteria.opAnd(new Criteria(typeof(SalesmanHeader), "SalesmanCode", MatchType.Exact, salesmanCode));
            if (isValidateDealerCode)
            {
                criteria.opAnd(new Criteria(typeof(SalesmanHeader), "Dealer.DealerCode", MatchType.Exact, dealerCode));
            }

            var masters = _mapper.RetrieveByCriteria(criteria);
            if (masters.Count > 0)
            {
                // cast the object
                salesmanHeader = masters[0] as SalesmanHeader;
            }
            else
            {
                if (isMainDealer)
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.SalesmanHeader, salesmanCode)));
                }
                else
                {
                    if (!string.IsNullOrEmpty(parentDealerCode))
                    {
                        // get by criteria
                        CriteriaComposite ctr = new CriteriaComposite(new Criteria(typeof(SalesmanHeader), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
                        ctr.opAnd(new Criteria(typeof(SalesmanHeader), "SalesmanCode", MatchType.Exact, salesmanCode));
                        ctr.opAnd(new Criteria(typeof(SalesmanHeader), "Dealer.DealerCode", MatchType.Exact, parentDealerCode));
                        
                        var dataSalesmanH = _mapper.RetrieveByCriteria(ctr);
                        if (dataSalesmanH.Count > 0)
                        {
                            // cast the object
                            salesmanHeader = dataSalesmanH[0] as SalesmanHeader;
                        }
                        else
                        {
                            validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.SalesmanHeader, salesmanCode)));
                        }
                    }
                }
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate Assist Work Order Category
        /// </summary>
        /// <param name="workOrderCategoryCode"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        /// <param name="workOrderCategory"></param>
        /// <returns></returns>
        public static bool ValidateAssistWorkOrderCategory(string workOrderCategoryCode, List<DNetValidationResult> validationResults, ref AssistWorkOrderCategory workOrderCategory)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(AssistWorkOrderCategory).ToString());

            // get by criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(AssistWorkOrderCategory), "WorkOrderCategory", workOrderCategoryCode));
            if (masters.Count > 0)
            {
                // cast the object
                workOrderCategory = masters[0] as AssistWorkOrderCategory;
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.WorkOrderCategory, workOrderCategoryCode)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate Assist Service Place
        /// </summary>
        /// <param name="servicePlaceCode"></param>
        /// <param name="validationResults"></param>
        /// <param name="servicePlace"></param>
        /// <returns></returns>
        public static bool ValidateAssistServicePlace(string servicePlaceCode, List<DNetValidationResult> validationResults, ref AssistServicePlace servicePlace)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(AssistServicePlace).ToString());

            // get by criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(AssistServicePlace), "ServicePlaceCode", servicePlaceCode));
            if (masters.Count > 0)
            {
                // cast the object
                servicePlace = masters[0] as AssistServicePlace;
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.ServicePlaceCode, servicePlaceCode)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate Service Type
        /// </summary>
        /// <param name="serviceTypeCode"></param>
        /// <param name="validationResults"></param>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public static bool ValidateAssistServiceType(string serviceTypeCode, List<DNetValidationResult> validationResults, ref AssistServiceType serviceType)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(AssistServiceType).ToString());

            // get by criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(AssistServiceType), "ServiceTypeCode", serviceTypeCode));
            if (masters.Count > 0)
            {
                // cast the object
                serviceType = masters[0] as AssistServiceType;
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.ServiceTypeCode, serviceTypeCode)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// TrTrainee
        /// </summary>
        /// <param name="traineeID"></param>
        /// <param name="validationResults"></param>
        /// <param name="trTrainee"></param>
        /// <param name="dealerCode"></param>        
        /// <returns></returns>
        public static bool ValidateTrTrainee(string traineeID, List<DNetValidationResult> validationResults, ref TrTrainee trTrainee, string dealerCode = null)
        {
            int id = 0;
            if (int.TryParse(traineeID, out id))
            {
                return ValidateTrTrainee(id, validationResults, ref trTrainee, dealerCode);
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.EmployeeService, traineeID)));
                return false;
            }
        }

        /// <summary>
        /// TrTrainee
        /// </summary>
        /// <param name="traineeID"></param>
        /// <param name="validationResults"></param>
        /// <param name="trTrainee"></param>
        /// <param name="dealerCode"></param>        
        /// <returns></returns>
        public static bool ValidateTrTrainee(int traineeID, List<DNetValidationResult> validationResults, ref TrTrainee trTrainee, string dealerCode = null)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(TrTrainee).ToString());

            // get by criteria
            CriteriaComposite criteriaMekanik = new CriteriaComposite(new Criteria(typeof(TrTrainee), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteriaMekanik.opAnd(new Criteria(typeof(TrTrainee), "ID", MatchType.Exact, traineeID));
            if (dealerCode != null)
            {
                criteriaMekanik.opAnd(new Criteria(typeof(TrTrainee), "Dealer.DealerCode", MatchType.Exact, dealerCode));
            }

            var masters = _mapper.RetrieveByCriteria(criteriaMekanik);
            if (masters.Count > 0)
            {
                // cast the object
                trTrainee = masters[0] as TrTrainee;
            }
            else
            {
                if (dealerCode == null)
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.EmployeeService, traineeID)));
                else
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrence, FieldResource.EmployeeService, traineeID, dealerCode)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate Sales Channel
        /// </summary>
        /// <param name="salesChannelCode"></param>
        /// <param name="validationResults"></param>        
        /// <returns></returns>
        public static bool ValidateSalesChannel(string salesChannelCode, List<DNetValidationResult> validationResults, ref AssistSalesChannel salesChannel)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(AssistSalesChannel).ToString());

            // get by criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(AssistSalesChannel), "SalesChannelCode", salesChannelCode));
            if (masters.Count == 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.SalesChannelCode, salesChannelCode)));
            }
            else
            {
                salesChannel = masters[0] as AssistSalesChannel;
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate passed vehicle type code parameter
        /// </summary>
        /// <param name="vehicleTypeCode"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        /// <param name="vehicleType"></param>
        public static bool ValidateVehicleType(string vehicleTypeCode, List<DNetValidationResult> validationResults, ref VechileType vehicleType)
        {
            if (string.IsNullOrEmpty(vehicleTypeCode))
                return true;

            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(VechileType).ToString());

            // get by criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(VechileType), "VechileTypeCode", vehicleTypeCode));
            if (masters.Count > 0)
            {
                // cast the object
                var temp = masters[0] as VechileType;
                // validate the status
                if (temp.Status.Equals("A", StringComparison.OrdinalIgnoreCase))
                {
                    vehicleType = temp;
                }
                else
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgVehicleTypeStatusInvalid, vehicleTypeCode, temp.Status)));
                }
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.VehicleTypeCode, vehicleTypeCode)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate passed customer dealer id parameter
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        /// <param name="customerDealer"></param>
        public static bool ValidateCustomerDealer(int customerId, List<DNetValidationResult> validationResults, ref CustomerDealer customerDealer, int dealerId = 0)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(CustomerDealer).ToString());

            // get by criteria
            CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(CustomerDealer), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteria.opAnd(new Criteria(typeof(CustomerDealer), "Customer.ID", MatchType.Exact, customerId));
            if (dealerId > 0)
            {
                criteria.opAnd(new Criteria(typeof(CustomerDealer), "Dealer.ID", MatchType.Exact, dealerId));
            }

            var masters = _mapper.RetrieveByCriteria(criteria);
            if (masters.Count > 0)
            {
                // cast the object
                customerDealer = masters[0] as CustomerDealer;
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.CustomerDealer, customerId)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Customer Dealer  by code
        /// </summary>
        /// <param name="customerCode"></param>
        /// <param name="dealerCode"></param>
        /// <param name="validationResults"></param>
        /// <param name="customerDealer"></param>
        /// <returns></returns>
        public static bool ValidateCustomerDealer(string customerCode, string dealerCode, List<DNetValidationResult> validationResults, bool isMandatory = false)
        {
            // no need to validate if not mandatory
            if (string.IsNullOrEmpty(customerCode) && !isMandatory)
            {
                return true;
            }

            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(CustomerDealer).ToString());

            // get by criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(CustomerDealer), "Customer.Code", "Dealer.DealerCode", customerCode, dealerCode));
            if (masters.Count == 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrence, FieldResource.CustomerDealer, customerCode, dealerCode)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate passed customer code parameter
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="validationResults"></param>
        /// <param name="isValid"></param>
        /// <param name="customer"></param>
        public static bool ValidateCustomer(string customerCode, List<DNetValidationResult> validationResults, ref Customer customer)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(Customer).ToString());

            // get by criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(Customer), "Code", customerCode));
            if (masters.Count > 0)
            {
                // cast the object
                customer = masters[0] as Customer;
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.Customer, customerCode)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate SPK Detail
        /// </summary>
        /// <param name="vehicleColorCode"></param>
        /// <param name="vehicleTypeCode"></param>
        /// <param name="spkHeaderId"></param>
        /// <param name="validationResults"></param>
        /// <param name="spkDetail"></param>
        /// <returns></returns>
        public static bool ValidateSPKDetail(string vehicleColorCode, string vehicleTypeCode, SPKHeader spkHeader, List<DNetValidationResult> validationResults, ref SPKDetail spkDetail)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(SPKDetail).ToString());

            // get by criteria
            CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(SPKDetail), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteria.opAnd(new Criteria(typeof(SPKDetail), "VehicleColorCode", MatchType.Exact, vehicleColorCode));
            criteria.opAnd(new Criteria(typeof(SPKDetail), "VehicleTypeCode", MatchType.Exact, vehicleTypeCode));
            criteria.opAnd(new Criteria(typeof(SPKDetail), "SPKHeader.ID", MatchType.Exact, spkHeader.ID));
            var masters = _mapper.RetrieveByCriteria(criteria);
            if (masters.Count > 0)
            {
                // cast the object
                spkDetail = masters[0] as SPKDetail;
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSpkDetailNotFound, spkHeader.SPKNumber, vehicleColorCode, vehicleTypeCode)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate SPK Detail By TypeCode
        /// </summary>
        /// <param name="vehicleTypeCode"></param>
        /// <param name="spkHeaderId"></param>
        /// <param name="validationResults"></param>
        /// <param name="spkDetail"></param>
        /// <returns></returns>
        public static bool ValidateSPKDetailByTypeCode(string vehicleTypeCode, SPKHeader spkHeader, int spkDetailID, List<DNetValidationResult> validationResults, ref SPKDetail spkDetail)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(SPKDetail).ToString());

            // get by criteria
            CriteriaComposite criteria = new CriteriaComposite(new Criteria(typeof(SPKDetail), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criteria.opAnd(new Criteria(typeof(SPKDetail), "VehicleTypeCode", MatchType.Exact, vehicleTypeCode));
            criteria.opAnd(new Criteria(typeof(SPKDetail), "ID", MatchType.Exact, spkDetailID));
            criteria.opAnd(new Criteria(typeof(SPKDetail), "SPKHeader.ID", MatchType.Exact, spkHeader.ID));
            var masters = _mapper.RetrieveByCriteria(criteria);
            if (masters.Count > 0)
            {
                // cast the object
                spkDetail = masters[0] as SPKDetail;
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgSpkDetailByTypeNotFound, spkHeader.SPKNumber, vehicleTypeCode)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate SPK Header
        /// </summary>
        /// <param name="spkNumber"></param>
        /// <param name="validationResults"></param>
        /// <param name="spkHeader"></param>
        /// <returns></returns>
        public static bool ValidateSPKHeader(string spkNumber, List<DNetValidationResult> validationResults, ref SPKHeader spkHeader)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(SPKHeader).ToString());

            // get by criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(SPKHeader), "SPKNumber", spkNumber));
            if (masters.Count > 0)
            {
                // cast the object
                spkHeader = masters[0] as SPKHeader;
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.SPKNumber, spkNumber)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate city
        /// </summary>
        /// <param name="code"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        public static bool ValidateCity(string code, List<DNetValidationResult> validationResults, ref City city, bool isCheckStatus = true)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(City).ToString());

            // get by criteria
            var criterias = new CriteriaComposite(new Criteria(typeof(City), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(City), "CityCode", MatchType.Exact, code));
            if (isCheckStatus)
                criterias.opAnd(new Criteria(typeof(City), "Status", MatchType.Exact, "A"));
            var masters = _mapper.RetrieveByCriteria(criterias);
            if (masters.Count > 0)
            {
                city = masters[0] as City;
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrence, FieldResource.City, code, "A")));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate citypart
        /// </summary>
        /// <param name="cityID"></param>
        /// <param name="cityPart"></param>
        /// <returns></returns>
        public static bool ValidateCityPart(int cityID, ref CityPart cityPart)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(CityPart).ToString());

            // get by criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(CityPart), "City.ID", cityID));
            if (masters.Count > 0)
            {
                cityPart = masters[0] as CityPart;
            }

            return masters.Count != 0;
        }

        /// <summary>
        /// Validate Customer Request
        /// </summary>
        /// <param name="id"></param>
        /// <param name="validationResults"></param>
        /// <param name="customerRequest"></param>
        /// <returns></returns>
        public static bool ValidateCustomerRequest(int id, List<DNetValidationResult> validationResults, ref CustomerRequest customerRequest)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(CustomerRequest).ToString());

            // get by criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(CustomerRequest), "ID", id));
            if (masters.Count > 0)
            {
                customerRequest = masters[0] as CustomerRequest;
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.City, id)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate OCR Identity on OCR server
        /// </summary>
        /// <param name="identityType"></param>
        /// <param name="imageID"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        public static bool ValidateOCRIdentityServer(short identityType, string imageID, List<DNetValidationResult> validationResults)
        {
            // intialize
            IOCRIdentityBL ocrBL = new OCRIdentityBL();

            // validate the OCR
            if (identityType == 0)
            {
                ResponseBase<OCRKTPDataDto> results = ocrBL.DataKTP(imageID);
                if (!results.success)
                {
                    validationResults.Add(new DNetValidationResult(ErrorCode.DataReadNotAvailable, string.Format(MessageResource.ErrorMsgOcrIdentityNotFound, imageID)));
                }
            }
            else if (identityType == 1)
            {
                ResponseBase<OCRSIMDataDto> results = ocrBL.DataSIM(imageID);
                if (!results.success)
                {
                    validationResults.Add(new DNetValidationResult(ErrorCode.DataReadNotAvailable, string.Format(MessageResource.ErrorMsgOcrIdentityNotFound, imageID)));
                }
            }
            else
            {
                if (identityType == -1)
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataRequired, FieldResource.IdentityType)));
                }
                else
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.IdentityType, identityType)));
                }
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate ocr identity in OCRIdentity DB
        /// </summary>
        /// <param name="imageID"></param>
        /// <param name="validationResults"></param>
        /// <param name="ocrIdentity"></param>
        /// <returns></returns>
        public static bool ValidateOCRIdentity(string imageID, List<DNetValidationResult> validationResults, ref OCRIdentity ocrIdentity, bool isExistenceValidation = false)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(OCRIdentity).ToString());

            // get by criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(OCRIdentity), "ImageID", imageID));
            if (masters.Count > 0)
            {
                ocrIdentity = masters[0] as OCRIdentity;
            }
            else
            {
                if (isExistenceValidation)
                    return false;
                else
                    validationResults.Add(new DNetValidationResult(ErrorCode.DataReadNotAvailable, string.Format(MessageResource.ErrorMsgOcrIdentityNotFound, imageID)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate ocr identity in OCRIdentity DB
        /// </summary>
        /// <param name="imageID"></param>
        /// <param name="validationResults"></param>
        /// <param name="ocrIdentity"></param>
        /// <returns></returns>
        public static bool ValidateOCRIdentity(int spkCustomerID, List<DNetValidationResult> validationResults, ref OCRIdentity ocrIdentity)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(OCRIdentity).ToString());

            // get by criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(OCRIdentity), "SPKCustomer.ID", spkCustomerID));
            if (masters.Count > 0)
            {
                ocrIdentity = masters[0] as OCRIdentity;
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgOcrIdentityNotFoundBySPKCustomerID, spkCustomerID)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate ocr KK identity in OCRFamilyCard DB
        public static bool ValidateOCRFamilyCard(int spkHeaderID, List<DNetValidationResult> validationResults, ref List<OCRFamilyCard> ocrKK)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(OCRFamilyCard).ToString());

            // get by criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(OCRFamilyCard), "SPKHeader.ID", spkHeaderID));
            if (masters.Count > 0)
            {
                for (int i = 0; i < masters.Count; i++)
                {
                    ocrKK.Add(masters[i] as OCRFamilyCard);
                }
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgOcrFamilyCardNotFoundBySPKHeaderID, spkHeaderID)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate Part Shop
        /// </summary>
        /// <param name="partShopId"></param>
        /// <param name="validationResults"></param>
        /// <param name="newPartShop"></param>
        /// <returns></returns>
        public static bool ValidatePartShop(int partShopId, string dealerCode, List<DNetValidationResult> validationResults, ref PartShop oldPartShop)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(PartShop).ToString());
            var partshop = _mapper.Retrieve(partShopId);
            if (partshop != null)
            {
                oldPartShop = partshop as PartShop;
                if (oldPartShop.Dealer != null && oldPartShop.Dealer.DealerCode != dealerCode)
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgPartShopDealerNotMatch, oldPartShop.Name, oldPartShop.Dealer.DealerCode)));
                }
            }
            else
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataReferrenceSingle, FieldResource.PartShop, partShopId)));
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate Partshop kuota
        /// </summary>
        /// <param name="cityPartID"></param>
        /// <param name="cityName"></param>
        /// <param name="cityID"></param>
        /// <param name="validationResults"></param>        
        /// <returns></returns>
        public static bool ValidatePartShopKuota(int cityPartID, string cityName, int cityID, List<DNetValidationResult> validationResults)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(PartShop).ToString());

            // get by criteria
            var criterias = new CriteriaComposite(new Criteria(typeof(PartShop), "RowStatus", MatchType.Exact, (short)DBRowStatus.Active));
            criterias.opAnd(new Criteria(typeof(PartShop), "CityPart.ID", MatchType.Exact, cityPartID));
            var sortColl = new SortCollection();
            sortColl.Add(new Sort(typeof(PartShop), "PartShopCode", Sort.SortDirection.DESC));
            var masters = _mapper.RetrieveByCriteria(criterias, sortColl);
            if (masters.Count > 0)
            {
                var partShop = masters[0] as PartShop;
                string code = partShop.PartShopCode;
                if (code.Substring(4, 2) == "9999")
                {
                    validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgNumberPartShopExceed, cityName + "(" + cityID + ")")));
                }
            }

            return validationResults.Count == 0;
        }

        /// <summary>
        /// Validate passed chassis number parameter
        /// </summary>
        /// <param name="dmsPRNO"></param>
        /// <param name="validationResults"></param>        
        /// <param name="indentPartHeader"></param>
        public static bool ValidateIndentPartHeader(string dmsPRNO, List<DNetValidationResult> validationResults)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(IndentPartHeader).ToString());

            // get by criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(IndentPartHeader), "DMSPRNo", dmsPRNO));
            if (masters.Count > 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format(MessageResource.ErrorMsgDataIsExist, FieldResource.IndentPartHeader, dmsPRNO)));
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Check if the dealer is TOP dealer
        /// </summary>
        /// <param name="dealerCode"></param>
        /// <returns></returns>
        public static bool ValidateDealerTOP(string dealerCode, AutoMapper.IMapper mapper)
        {
            // get NonTOPID
            var sparepartNonTopID = Helper.GetSparePartNonTOPID(mapper);

            // initialize the mapper
            var _topCreditAccountMapper = MapperFactory.GetInstance().GetMapper(typeof(TOPCreditAccount).ToString());

            // get by criteria
            var criterias = Helper.GenerateCriteria(typeof(TOPCreditAccount), "Dealer.DealerCode", dealerCode);
            var data = _topCreditAccountMapper.RetrieveByCriteria(criterias);
            if (data.Count > 0)
            {
                // check its status
                var topCreditAccount = data[0] as TOPCreditAccount;
                if (topCreditAccount.Status == 1)
                {
                    // check top ID it is not non TOP ID
                    if (topCreditAccount.TermOfPayment != null && topCreditAccount.TermOfPayment.ID != sparepartNonTopID)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Validate Team Category and Vehicle Type Id
        /// </summary>
        /// <param name="dealerCode"></param>
        /// <param name="teamCategory"></param>
        /// <param name="vehicleTypeCategoryId"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        public static bool ValidateDealerCategory(string dealerCode, string salesmanCode, string vehicleTypeCode, List<DNetValidationResult> validationResults)
        {
            var isValid = false;

            #region initialize mapper
            var _dealerMapper = MapperFactory.GetInstance().GetMapper(typeof(Dealer).ToString());
            var _dealerCategoryMapper = MapperFactory.GetInstance().GetMapper(typeof(DealerCategory).ToString());
            var _salesmanHeaderMapper = MapperFactory.GetInstance().GetMapper(typeof(SalesmanHeader).ToString());
            var _salesmanProfileMapper = MapperFactory.GetInstance().GetMapper(typeof(SalesmanProfile).ToString());
            var _vehicleTypeMapper = MapperFactory.GetInstance().GetMapper(typeof(VechileType).ToString());
            var _categoryMapper = MapperFactory.GetInstance().GetMapper(typeof(Category).ToString());
            #endregion

            #region Check Dealer Category Config
            var isCheckDealerCategory = CheckDealerCategoryConfig();
            if (!isCheckDealerCategory)
            {
                return true;
            }
            #endregion

            #region Get Dealer
            // Get Dealer by dealerCode
            var dealers = _dealerMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(Dealer), "DealerCode", dealerCode));
            if (dealers.Count == 0)
            {
                validationResults.Add(new DNetValidationResult(MessageResource.ErrorMsgDealerCodeInvalid));
                return false;
            }

            var dealer = (Dealer)dealers[0];
            #endregion

            #region Get Dealer Category
            // Get Dealer Category
            var dealerCategories = _dealerCategoryMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(DealerCategory), "Dealer.ID", dealer.ID));
            if (dealerCategories.Count == 0)
            {
                validationResults.Add(new DNetValidationResult("Dealer Category tidak ditemukan dalam Database"));
                return false;
            }
            var dealerCategoryList = dealerCategories.Cast<DealerCategory>().ToList();
            List<int> allowedDealerCategoryId = dealerCategoryList.Select(x => x.Category.ID).ToList();
            #endregion

            #region Get Salesman Team Category
            // Get Salesman Header by Salesman Code
            var salesmanHeaders = _salesmanHeaderMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(SalesmanHeader), "SalesmanCode", salesmanCode));
            if (salesmanHeaders.Count == 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format("Salesman dengan kode {0} tidak ditemukan dalam Database", salesmanCode)));
                return false;
            }

            var salesmanHeader = (SalesmanHeader)salesmanHeaders[0];

            // Get Salesman Profile
            var salesmanProfiles = _salesmanProfileMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(SalesmanProfile), "SalesmanHeader.ID", salesmanHeader.ID));
            if (salesmanProfiles.Count == 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format("Salesman Profile untuk Salesman dengan kode {0} tidak ditemukan dalam Database", salesmanHeader.SalesmanCode)));
                return false;
            }

            // Get Salesman Team Category Value
            var salesmanProfileList = salesmanProfiles.Cast<SalesmanProfile>().ToList();
            var salesmanTeamCategoryValue = salesmanProfileList.Where(x => x.ProfileHeader.ID == 45).Select(x => x.ProfileValue).FirstOrDefault();

            if (salesmanTeamCategoryValue == null)
            {
                validationResults.Add(new DNetValidationResult(string.Format("Team Category untuk Salesman dengan kode {0} tidak ditemukan dalam Database", salesmanHeader.SalesmanCode)));
                return false;
            }

            // Get Team Category ID

            var categories = _categoryMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(Category), "CategoryCode", salesmanTeamCategoryValue));
            if (categories.Count == 0)
            {
                validationResults.Add(new DNetValidationResult(string.Format("Category untuk Profile Value {0} tidak ditemukan dalam database", salesmanTeamCategoryValue)));
                return false;
            }

            var teamCategory = ((Category)categories[0]).ID;

            #endregion

            #region Get Vehicle Type
            // Get Vehicle Type
            var vehicleTypes = _vehicleTypeMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(VechileType), "VechileTypeCode", vehicleTypeCode));
            if (vehicleTypes.Count == 0)
            {
                validationResults.Add(new DNetValidationResult("Vehicle Type tidak ditemukan dalam database"));
                return false;
            }

            var vehicleTypeCategoryId = ((VechileType)vehicleTypes[0]).Category.ID;
            #endregion

            #region Validate Team Category and VehicleType
            if (!allowedDealerCategoryId.Contains(teamCategory))
            {
                validationResults.Add(new DNetValidationResult("Kategori Tim tidak sesuai dengan kategori Dealer"));
                return false;
            }

            if (!allowedDealerCategoryId.Contains(vehicleTypeCategoryId))
            {
                validationResults.Add(new DNetValidationResult("Kategori Vehicle Type tidak sesuai dengan kategori Dealer"));
                return false;
            }
            #endregion

            return isValid;
        }

        /// <summary>
        /// Validate Dealer Category for Salesman Header
        /// </summary>
        /// <param name="dealerCode"></param>
        /// <param name="teamCategoryProfileId"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        public static bool ValidateDealerCategory(string dealerCode, int teamCategoryProfileId, List<DNetValidationResult> validationResults)
        {

            #region Initialize Mapper
            var _dealerMapper = MapperFactory.GetInstance().GetMapper(typeof(Dealer).ToString());
            var _dealerCategoryMapper = MapperFactory.GetInstance().GetMapper(typeof(DealerCategory).ToString());
            var _profileDetailMapper = MapperFactory.GetInstance().GetMapper(typeof(ProfileDetail).ToString());
            var _categoryMapper = MapperFactory.GetInstance().GetMapper(typeof(Category).ToString());
            #endregion

            #region Check Dealer Category Config
            var isCheckDealerCategory = CheckDealerCategoryConfig();
            if (!isCheckDealerCategory)
            {
                return true;
            }
            #endregion

            #region Get Dealer
            // Get Dealer by dealerCode
            var dealers = _dealerMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(Dealer), "DealerCode", dealerCode));
            if (dealers.Count == 0)
            {
                validationResults.Add(new DNetValidationResult(MessageResource.ErrorMsgDealerCodeInvalid));
                return false;
            }

            var dealer = (Dealer)dealers[0];
            #endregion

            #region Get Dealer Category
            // Get Dealer Category
            var dealerCategories = _dealerCategoryMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(DealerCategory), "Dealer.ID", dealer.ID));
            if (dealerCategories.Count == 0)
            {
                validationResults.Add(new DNetValidationResult("Dealer Category tidak ditemukan dalam Database"));
                return false;
            }
            var dealerCategoryList = dealerCategories.Cast<DealerCategory>().ToList();
            List<int> allowedDealerCategoryId = dealerCategoryList.Select(x => x.Category.ID).ToList();
            #endregion

            #region Get Profile Detail
            // Get Profile Detail by Id
            var profileDetail = _profileDetailMapper.Retrieve(teamCategoryProfileId);
            if (profileDetail == null)
            {
                validationResults.Add(new DNetValidationResult("Team Category tidak ditemukan dalam Database"));
                return false;
            }
            var teamCategoryValue = ((ProfileDetail)profileDetail).Code;
            #endregion

            #region Get Team Category Id
            // Get Team Category
            var categories = _categoryMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(Category), "CategoryCode", teamCategoryValue));
            if (categories.Count == 0)
            {
                validationResults.Add(new DNetValidationResult("Team Category tidak ditemukan dalam Database"));
                return false;
            }

            var teamCategory = ((Category)categories[0]).ID;
            #endregion

            #region Validate Team Category
            if (!allowedDealerCategoryId.Contains(teamCategory))
            {
                validationResults.Add(new DNetValidationResult("Kategori Tim tidak sesuai dengan kategori Dealer"));
                return false;
            }
            #endregion
            return true;
        }

        /// <summary>
        /// Check if SF or not
        /// </summary>
        /// <returns></returns>
        public static bool ValidateWoSf(string dealerCode)
        {

            #region initialize mapper
            var appConfigMapper = MapperFactory.GetInstance().GetMapper(typeof(AppConfig).ToString());
            var dealerSystemsMapper = MapperFactory.GetInstance().GetMapper(typeof(DealerSystems).ToString());
            #endregion

            // Check App Config
            var appConfigs = appConfigMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(AppConfig), "Name", "CheckSalesFunnel"));
            if (appConfigs.Count == 0)
            {
                return false;
            }
            if (((AppConfig)appConfigs[0]).Value == "1")
            {
                // Check Dealer System
                var dealerSystems = dealerSystemsMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(DealerSystems), "isSalesFunnelValidate", "Dealer.DealerCode", 1, dealerCode));

                if (dealerSystems.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check the spk match faktur status
        /// </summary>
        /// <returns></returns>
        public static int ValidateDealerSystems(string DealerCode)
        {
            var _dealerMapper = MapperFactory.GetInstance().GetMapper(typeof(Dealer).ToString());
            var _dealerSystemMapper = MapperFactory.GetInstance().GetMapper(typeof(DealerSystems).ToString());

            var dealers = _dealerMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(Dealer), "DealerCode", DealerCode));
            int dealerID = 0;
            if (dealers.Count > 0)
            {
                dealerID = (dealers[0] as Dealer).ID;
            }

            if (dealerID == 0)
                return 1;

            var dealerSystems = _dealerSystemMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(DealerSystems), "Dealer.ID", dealerID));
            if (dealerSystems.Count > 0)
            {
                var dealer = (dealerSystems[0] as DealerSystems);
                return dealer.SystemID;
            }
            else
                return 1;
        }

        /// <summary>
        /// Validate Data Input
        /// </summary>
        /// <param name="dataParam"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        public static bool ValidateDataInput(ValidationParameterDto dataParam, List<DNetValidationResult> validationResults, int indx = 0, bool isCreateList = false)
        {
            ValidationParameterDto dataMaster = new ValidationParameterDto();
            ValidationDapper dapper = new ValidationDapper();
            string IndexData = indx == 0 ? string.Empty : "Index " + indx.ToString() + " - ";
            if (isCreateList && indx == 0)
            {
                IndexData = "Index " + indx.ToString() + " - ";
            }

            #region Get Data Master
            dataMaster = dapper.getdataMasterBusinessUnitByDealerCode(dataParam.dealerCode, validationResults);
            if (validationResults.Count > 0)
            {
                return false;
            }
            #endregion

            #region validate Data
            //Check Data Company
            if (dataParam.xts_company != "" && dataParam.xts_company != string.Empty && dataParam.xts_company != null)
            {
                if (dataParam.xts_company != dataMaster.xts_company)
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_company harus diisi dengan '" + dataMaster.xts_company + "' !"));
                }
            }
            //Check Data BusinessUnitId
            if (dataParam.xts_businessunitid != null)
            {
                if (dataParam.xts_businessunitid != dataMaster.xts_businessunitid)
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field businessunitid harus diisi dengan '" + dataMaster.xts_businessunitid + "' !"));
                }
            }
            //Check Data OwningBusinessUnit
            if (dataParam.owningbusinessunit != null)
            {
                if (dataParam.owningbusinessunit != dataMaster.owningbusinessunit)
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field Owningbusinessunit harus diisi dengan '" + dataMaster.owningbusinessunit + "' !"));
                }
            }
            //Check Data OwnerId
            if (dataParam.ownerid != null || dataParam.xts_ownerid != null)
            {
                if (dataParam.ownerid != null && !dapper.checkdataMasterOwnerById(dataParam.ownerid.ToString(), "ownerid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field ownerid value tidak di temukan di data Master"));
                }
                if (dataParam.xts_ownerid != null && !dapper.checkdataMasterAccountById(dataParam.xts_ownerid.ToString(), "ownerid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_ownerid value tidak di temukan di data Master"));
                }
            }
            //Check Data TransactionCurrenctyId
            if (dataParam.transactioncurrencyid != null || dataParam.xts_currencyid != null)
            {
                if (dataParam.transactioncurrencyid != null && !dapper.checkdataMasterTransactioncurrencyById(dataParam.transactioncurrencyid.ToString()))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field transactioncurrencyid value tidak di temukan di data Master"));
                }
                if (dataParam.xts_currencyid != null && !dapper.checkdataMasterTransactioncurrencyById(dataParam.xts_currencyid.ToString()))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_currencyid value tidak di temukan di data Master"));
                }
            }
            //Check Data BillToId
            if (dataParam.xts_billtoid != null || dataParam.xjp_billtoid != null)
            {
                if (dataParam.xts_billtoid != null && !dapper.checkdataMasterAccountById(dataParam.xts_billtoid.ToString(), "accountid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_billtoid value tidak di temukan di data Master"));
                }
                if (dataParam.xjp_billtoid != null && !dapper.checkdataMasterAccountById(dataParam.xjp_billtoid.ToString(), "accountid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xjp_billtoid value tidak di temukan di data Master"));
                }
            }
            //Check Data CityId
            if (dataParam.xts_cityid != null)
            {
                if (!dapper.checkdataMasterCityById(dataParam.xts_cityid.ToString()))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_cityid value tidak di temukan di data Master"));
                }
            }
            //Check Data ClassId
            if (dataParam.xts_classid != null)
            {
                if (!dapper.checkdataMasterVendorClassById(dataParam.xts_classid.ToString(), "xts_vendorclassid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_classid value tidak di temukan di data Master"));
                }
            }
            //Check Data CustomerId
            if (dataParam.xts_customerid != null || dataParam.ktb_namakonsumenspkid != null || dataParam.xts_delivertocustomerid != null)
            {
                if (dataParam.xts_customerid != null && !dapper.checkdataMasterAccountById(dataParam.xts_customerid.ToString(), "accountid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_customerid value tidak di temukan di data Master"));
                }
                if (dataParam.ktb_namakonsumenspkid != null && !dapper.checkdataMasterAccountById(dataParam.ktb_namakonsumenspkid.ToString(), "accountid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field ktb_namakonsumenspkid value tidak di temukan di data Master"));
                }
                if (dataParam.xts_delivertocustomerid != null && !dapper.checkdataMasterAccountById(dataParam.xts_delivertocustomerid.ToString(), "accountid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_delivertocustomerid value tidak di temukan di data Master"));
                }
            }
            //Check Data CustomerClassId
            if (dataParam.xts_customerclassid != null)
            {
                if (!dapper.checkdataMasterCustomerClassById(dataParam.xts_customerclassid.ToString(), "xts_customerclassid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_customerclassid value tidak di temukan di data Master"));
                }
            }
            //Check Data EmployeeId
            if (dataParam.xts_employeeid != null || dataParam.ktb_superiors != null)
            {
                if (dataParam.xts_employeeid != null && !dapper.checkdataMasterEmployeeById(dataParam.xts_employeeid.ToString(), "xts_employeeid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_employeeid value tidak di temukan di data Master"));
                }
                if (dataParam.ktb_superiors != null && !dapper.checkdataMasterEmployeeById(dataParam.ktb_superiors.ToString(), "xts_employeeid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field ktb_superiors value tidak di temukan di data Master"));
                }
            }
            //Check Data IncomingOutSourceWorkOrderId
            if (dataParam.xts_incomingoutsourceworkorderid != null)
            {
                if (!dapper.checkdataMasterIncomingOutSourceWorkOrderById(dataParam.xts_incomingoutsourceworkorderid.ToString(), "xts_incomingoutsourceworkorderid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_incomingoutsourceworkorderid value tidak di temukan di data Master"));
                }
            }
            //Check Data ManufacturerId
            if (dataParam.xts_manufacturerid != null)
            {
                if (!dapper.checkdataMasterManufacturerById(dataParam.xts_manufacturerid.ToString(), "xts_manufacturerid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_manufacturerid value tidak di temukan di data Master"));
                }
            }
            //Check Data NewVehicleSalesOrderId
            if (dataParam.xts_newvehiclesalesorderid != null || dataParam.xid_newvehiclesalesorderid != null)
            {
                if (dataParam.xts_newvehiclesalesorderid != null && !dapper.checkdataMasterNVSOById(dataParam.xts_newvehiclesalesorderid.ToString(), "xts_newvehiclesalesorderid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_newvehiclesalesorderid value tidak di temukan di data Master"));
                }
                if (dataParam.xid_newvehiclesalesorderid != null && !dapper.checkdataMasterNVSOById(dataParam.xid_newvehiclesalesorderid.ToString(), "xts_newvehiclesalesorderid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xid_newvehiclesalesorderid value tidak di temukan di data Master"));
                }
            }
            //Check Data Opportuityid
            if (dataParam.xts_opportunityid != null)
            {
                if (!dapper.checkdataMasterOpportunityById(dataParam.xts_opportunityid.ToString(), "opportunityid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_opportunityid value tidak di temukan di data Master"));
                }
            }
            //Check Data OrderTypeId
            if (dataParam.xts_ordertypeid != null)
            {
                if (!dapper.checkdataMasterOrderTypeById(dataParam.xts_ordertypeid.ToString(), "xts_ordertypeid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_ordertypeid value tidak di temukan di data Master"));
                }
            }
            //Check Data PotentialCustomerId
            if (dataParam.xts_potentialcustomerid != null)
            {
                if (!dapper.checkdataMasterAccountById(dataParam.xts_potentialcustomerid.ToString(), "accountid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_potentialcustomerid value tidak di temukan di data Master"));
                }
            }
            //Check Data ProductId
            if (dataParam.xts_productid != null || dataParam.xjp_productid != null)
            {
                if (dataParam.xts_productid != null && !dapper.checkdataMasterProductById(dataParam.xts_productid.ToString(), "xts_productid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_productid value tidak di temukan di data Master"));
                }
                if (dataParam.xjp_productid != null && !dapper.checkdataMasterProductById(dataParam.xjp_productid.ToString(), "xts_productid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xjp_productid value tidak di temukan di data Master"));
                }
            }
            //Check Data ProductExteriorColorId
            if (dataParam.xts_productexteriorcolorid != null)
            {
                if (!dapper.checkdataMasterProductExteriorColorById(dataParam.xts_productexteriorcolorid.ToString(), "xts_productexteriorcolorid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_productexteriorcolorid value tidak di temukan di data Master"));
                }
            }
            //Check Data ProductSegment3Id
            if (dataParam.xts_productsegment3id != null)
            {
                if (!dapper.checkdataMasterProductSegment3ById(dataParam.xts_productsegment3id.ToString(), "xts_productsegment3id"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_productsegment3id value tidak di temukan di data Master"));
                }
            }
            //Check Data Reservationclassid
            if (dataParam.xts_reservationclassid != null)
            {
                if (!dapper.checkdataMasterServiceReservationClassById(dataParam.xts_reservationclassid.ToString(), "xts_servicereservationclassid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_reservationclassid value tidak di temukan di data Master"));
                }
            }
            //Check Data SalesPersonid
            if (dataParam.xts_salespersonid != null || dataParam.xto_salespersonid != null)
            {
                if (dataParam.xts_salespersonid != null && !dapper.checkdataMasterEmployeeById(dataParam.xts_salespersonid.ToString(), "xts_employeeid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_salespersonid value tidak di temukan di data Master"));
                }
                if (dataParam.xto_salespersonid != null && !dapper.checkdataMasterEmployeeById(dataParam.xto_salespersonid.ToString(), "xts_employeeid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xto_salespersonid value tidak di temukan di data Master"));
                }
            }
            //Check Data Serviceactivityid
            if (dataParam.xts_serviceactivityid != null)
            {
                if (!dapper.checkdataMasterServiceAppointmentById(dataParam.xts_serviceactivityid.ToString(), "activityid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_serviceactivityid value tidak di temukan di data Master"));
                }
            }
            //Check Data ServiceCategoryId
            if (dataParam.xts_servicecategoryid != null || dataParam.xjp_servicecategoryid != null)
            {
                if (dataParam.xts_servicecategoryid != null && !dapper.checkdataMasterServiceCategoryById(dataParam.xts_servicecategoryid.ToString(), "xts_servicecategoryid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_servicecategoryid value tidak di temukan di data Master"));
                }
                if (dataParam.xjp_servicecategoryid != null && !dapper.checkdataMasterServiceCategoryById(dataParam.xjp_servicecategoryid.ToString(), "xts_servicecategoryid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xjp_servicecategoryid value tidak di temukan di data Master"));
                }
            }
            //Check Data SiteId
            if (dataParam.xts_siteid != null || dataParam.xjp_destinationsiteid != null)
            {
                if (dataParam.xts_siteid != null && !dapper.checkdataMasterSiteById(dataParam.xts_siteid.ToString(), "xts_siteid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_siteid value tidak di temukan di data Master"));
                }
                if (dataParam.xjp_destinationsiteid != null && !dapper.checkdataMasterSiteById(dataParam.xjp_destinationsiteid.ToString(), "xts_siteid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xjp_destinationsiteid value tidak di temukan di data Master"));
                }
            }
            //Check Data Specialcolorpriceid
            if (dataParam.xts_specialcolorpriceid != null)
            {
                if (!dapper.checkdataMasterVehiclePriceDetailById(dataParam.xts_specialcolorpriceid.ToString(), "xts_vehiclepricedetailid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_specialcolorpriceid value tidak di temukan di data Master"));
                }
            }
            //Check Data Taxzoneid
            if (dataParam.xts_taxzoneid != null || dataParam.ktb_servicetypeid != null)
            {
                if (dataParam.xts_taxzoneid != null && !dapper.checkdataMasterCommonById(dataParam.xts_taxzoneid.ToString(), "xts_commonid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_taxzoneid value tidak di temukan di data Master"));
                }
                else if (dataParam.ktb_servicetypeid != null && !dapper.checkdataMasterCommonById(dataParam.ktb_servicetypeid.ToString(), "xts_commonid", true))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field ktb_servicetypeid value tidak di temukan di data Master"));
                }
            }
            //Check Data VehicleExteriorColorId
            if (dataParam.xts_vehicleexteriorcolorid != null)
            {
                if (!dapper.checkdataMasterProductExteriorColorById(dataParam.xts_vehicleexteriorcolorid.ToString(), "xts_productexteriorcolorid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_vehicleexteriorcolorid value tidak di temukan di data Master"));
                }
            }
            //Check Data VehicleIdentificationId
            if (dataParam.xts_vehicleidentificationid != null)
            {
                if (!dapper.checkdataMasterVehicleInformationById(dataParam.xts_vehicleidentificationid.ToString(), "xts_vehicleinformationid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_vehicleidentificationid value tidak di temukan di data Master"));
                }
            }
            //Check Data VehiclePriceListId
            if (dataParam.xts_vehiclepricelistid != null || dataParam.xts_vehiclepriceid != null)
            {
                if (dataParam.xts_vehiclepricelistid != null && !dapper.checkdataMasterVehiclePriceById(dataParam.xts_vehiclepricelistid.ToString(), "xts_vehiclepriceid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_vehiclepricelistid value tidak di temukan di data Master"));
                }
                if (dataParam.xts_vehiclepriceid != null && !dapper.checkdataMasterVehiclePriceById(dataParam.xts_vehiclepriceid.ToString(), "xts_vehiclepriceid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_vehiclepriceid value tidak di temukan di data Master"));
                }
            }
            //Check Data WareHouseId
            if (dataParam.xts_warehouseid != null || dataParam.xjp_destinationwarehouseid != null)
            {
                if (dataParam.xts_warehouseid != null && !dapper.checkdataMasterWareHouseById(dataParam.xts_warehouseid.ToString(), "xts_warehouseid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_warehouseid value tidak di temukan di data Master"));
                }
                if (dataParam.xjp_destinationwarehouseid != null && !dapper.checkdataMasterWareHouseById(dataParam.xjp_destinationwarehouseid.ToString(), "xts_warehouseid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xjp_destinationwarehouseid value tidak di temukan di data Master"));
                }
            }
            //Check Data WholeSaleOrderId
            if (dataParam.xts_wholesaleorderid != null)
            {
                if (!dapper.checkdataMasterNVWSOById(dataParam.xts_wholesaleorderid.ToString(), "xts_newvehiclewholesaleorderid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_wholesaleorderid value tidak di temukan di data Master"));
                }
            }
            //Check Data WorkOrderId
            if (dataParam.xts_workorderid != null)
            {
                if (!dapper.checkdataMasterWorkOrderById(dataParam.xts_workorderid.ToString(), "xts_workorderid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_workorderid value tidak di temukan di data Master"));
                }
            }
            // Check Data WO by WO number
            if (dataParam.xts_workorder != null)
            {
                if (!dapper.checkdataMasterWorkOrderById(dataParam.xts_workorder.ToString(), "xts_workorder"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field name value tidak di temukan di data Work Order"));
                }
            }
            //Check Data PRPOTypeId
            if (dataParam.xts_prpotypeid != null)
            {
                if (!dapper.checkdataMasterPRPOTById(dataParam.xts_prpotypeid.ToString(), "xts_purchaserequisitionpurchaseordertypeid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_prpotypeid value tidak di temukan di data Master"));
                }
            }
            //Check Data FromNoId
            if (dataParam.xts_fromnoid != null)
            {
                if (!dapper.checkdataMasterNVSONumRegistDetailById(dataParam.xts_fromnoid.ToString(), "xts_nvsonumberregistrationdetailsid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_fromnoid value tidak di temukan di data Master"));
                }
            }
            //Check Data ToNoId
            if (dataParam.xts_tonoid != null)
            {
                if (!dapper.checkdataMasterNVSONumRegistDetailById(dataParam.xts_tonoid.ToString(), "xts_nvsonumberregistrationdetailsid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_tonoid value tidak di temukan di data Master"));
                }
            }
            //Check Data ToSalesPersonId
            if (dataParam.xts_tosalespersonid != null)
            {
                if (!dapper.checkdataMasterEmployeeById(dataParam.xts_tosalespersonid.ToString(), "xts_employeeid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_tosalespersonid value tidak di temukan di data Master"));
                }
            }
            //Check Data VehicleOrderFormNumberId
            if (dataParam.xts_vehicleorderformnumberid != null)
            {
                if (!dapper.checkdataMasterNVSONumRegistDetailById(dataParam.xts_vehicleorderformnumberid.ToString(), "xts_nvsonumberregistrationdetailsid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_vehicleorderformnumberid value tidak di temukan di data Master"));
                }
            }
            //Check Data TerritoryId
            if (dataParam.xid_territoryid != null)
            {
                if (!dapper.checkdataMasterTerritoryById(dataParam.xid_territoryid.ToString(), "territoryid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xid_territoryid value tidak di temukan di data Master"));
                }
            }
            //Check Data VendorId
            if (dataParam.ktb_vendorid != null || dataParam.xjp_vendorid != null || dataParam.xts_vendorid != null)
            {
                if (dataParam.ktb_vendorid != null && !dapper.checkdataMasterVendorById(dataParam.ktb_vendorid.ToString(), "xts_vendorid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field ktb_vendorid value tidak di temukan di data Master"));
                }
                if (dataParam.xjp_vendorid != null && !dapper.checkdataMasterVendorById(dataParam.xjp_vendorid.ToString(), "xts_vendorid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xjp_vendorid value tidak di temukan di data Master"));
                }
                if (dataParam.xts_vendorid != null && !dapper.checkdataMasterVendorById(dataParam.xts_vendorid.ToString(), "xts_vendorid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_vendorid value tidak di temukan di data Master"));
                }
            }
            //Check Data PurchaseUnitId
            if (dataParam.xts_purchaseunitid != null)
            {
                if (!dapper.checkdataMasterUOMById(dataParam.xts_purchaseunitid.ToString(), "xts_uomid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_purchaseunitid value tidak di temukan di data Master"));
                }
            }
            //Check Data SalesUnitId
            if (dataParam.xts_salesunitid != null)
            {
                if (!dapper.checkdataMasterUOMById(dataParam.xts_salesunitid.ToString(), "xts_uomid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_salesunitid value tidak di temukan di data Master"));
                }
            }
            //Check Data StockId
            if (dataParam.xjp_stockid != null || dataParam.xts_stockid != null)
            {
                if (dataParam.xjp_stockid != null && !dapper.checkdataMasterInventoryNVById(dataParam.xjp_stockid.ToString(), "xts_inventorynewvehicleid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xjp_stockid value tidak di temukan di data Master"));
                }
                if (dataParam.xts_stockid != null && !dapper.checkdataMasterInventoryNVById(dataParam.xts_stockid.ToString(), "xts_inventorynewvehicleid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_stockid value tidak di temukan di data Master"));
                }
            }
            //Check Data CashAndBankId
            if (dataParam.xts_cashandbankid != null)
            {
                if (!dapper.checkdataMasterCashAndBankById(dataParam.xts_cashandbankid.ToString(), "xts_cashandbankid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_cashandbankid value tidak di temukan di data Master"));
                }
            }
            //Check Data AccessoriesId
            if (dataParam.xjp_accessoriesid != null)
            {
                if (!dapper.checkdataMasterProductById(dataParam.xjp_accessoriesid.ToString(), "xts_productid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xjp_accessoriesid value tidak di temukan di data Master"));
                }
            }
            //Check Data ARInvoiceId
            if (dataParam.xts_accountreceivableinvoiceid != null)
            {
                if (!dapper.checkdataMasterARInvoiceById(dataParam.xts_accountreceivableinvoiceid.ToString(), "xts_accountreceivableinvoiceid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_accountreceivableinvoiceid value tidak di temukan di data Master"));
                }
            }
            //Check Data ARReceiptId
            if (dataParam.xts_accountreceivablereceiptid != null)
            {
                if (!dapper.checkdataMasterARReceiptById(dataParam.xts_accountreceivablereceiptid.ToString(), "xts_accountreceivablereceiptid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_accountreceivablereceiptid value tidak di temukan di data Master"));
                }
            }
            //Check Data APVoucherId
            if (dataParam.ktb_apvoucherid != null)
            {
                if (!dapper.checkdataMasterAPVoucherById(dataParam.ktb_apvoucherid.ToString(), "xts_accountpayablevoucherid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field ktb_apvoucherid value tidak di temukan di data Master"));
                }
            }
            //Check Data APVoucherId
            if (dataParam.xts_billtocustomerid != null)
            {
                if (!dapper.checkdataMasterAccountById(dataParam.xts_billtocustomerid.ToString(), "accountid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_billtocustomerid value tidak di temukan di data Master"));
                }
            }
            //Check Data KontrabonId
            if (dataParam.ktb_kontrabonid != null)
            {
                if (!dapper.checkdataMasterKontrabonById(dataParam.ktb_kontrabonid.ToString(), "ktb_kontrabonid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field ktb_kontrabonid value tidak di temukan di data Master"));
                }
            }
            //Check Data OrderNVSOId
            if (dataParam.xts_ordernvsoid != null)
            {
                if (!dapper.checkdataMasterNVSOById(dataParam.xts_ordernvsoid.ToString(), "xts_newvehiclesalesorderid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_ordernvsoid value tidak di temukan di data Master"));
                }
            }
            //Check Data PDIId
            if (dataParam.xjp_predeliveryinspectionid != null)
            {
                if (!dapper.checkdataMasterPDIById(dataParam.xjp_predeliveryinspectionid.ToString(), "xjp_predeliveryinspectionid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xjp_predeliveryinspectionid value tidak di temukan di data Master"));
                }
            }
            //Check Data ReferenceNumberDOId
            if (dataParam.xts_referencenumberdeliveryorderid != null)
            {
                if (!dapper.checkdataMasterDeliveryOrderById(dataParam.xts_referencenumberdeliveryorderid.ToString(), "xts_deliveryorderid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_referencenumberdeliveryorderid value tidak di temukan di data Master"));
                }
            }
            //Check Data ReferenceNumberSOId
            if (dataParam.xts_referencenumbersalesorderid != null)
            {
                if (!dapper.checkdataMasterSalesOrderById(dataParam.xts_referencenumbersalesorderid.ToString(), "xts_salesorderid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_referencenumbersalesorderid value tidak di temukan di data Master"));
                }
            }
            //Check Data SalesCHannelId
            if (dataParam.ktb_saleschannelid != null)
            {
                if (!dapper.checkdataMasterSalesChannelById(dataParam.ktb_saleschannelid.ToString(), "ktb_saleschannelid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field ktb_saleschannelid value tidak di temukan di data Master"));
                }
            }
            //Check Data TermOfPaymentId
            if (dataParam.xts_termofpaymentid != null)
            {
                if (!dapper.checkdataMasterTOPById(dataParam.xts_termofpaymentid.ToString(), "xts_termofpaymentid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_termofpaymentid value tidak di temukan di data Master"));
                }
            }
            //Check Data TransactionDocumentId
            if (dataParam.xts_transactiondocumentid != null)
            {
                if (!dapper.checkdataMasterAPTranDocById(dataParam.xts_transactiondocumentid.ToString(), "xts_aptransactiondocumentid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_transactiondocumentid value tidak di temukan di data Master"));
                }
            }
            //Check Data AccountPayablePaymentId
            if (dataParam.xts_accountpayablepaymentid != null)
            {
                if (!dapper.checkdataMasterAPPById(dataParam.xts_accountpayablepaymentid.ToString(), "xts_accountpayablepaymentid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_accountpayablepaymentid value tidak di temukan di data Master"));
                }
            }
            //Check Data APVId
            if (dataParam.xts_accountpayablevoucherid != null)
            {
                if (!dapper.checkdataMasterAPVoucherById(dataParam.xts_accountpayablevoucherid.ToString(), "xts_accountpayablevoucherid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_accountpayablevoucherid value tidak di temukan di data Master"));
                }
            }
            //Check Data APReferenceNumberId
            if (dataParam.xts_apvoucherreferencenumberid != null)
            {
                if (!dapper.checkdataMasterAPVoucherById(dataParam.xts_apvoucherreferencenumberid.ToString(), "xts_accountpayablevoucherid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_apvoucherreferencenumberid value tidak di temukan di data Master"));
                }
            }
            //Check Data ConsumptionTaxId
            if (dataParam.xts_consumptiontax1id != null)
            {
                if (!dapper.checkdataMasterConsumptionTaxById(dataParam.xts_consumptiontax1id.ToString(), "xts_consumptiontaxid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_consumptiontax1id value tidak di temukan di data Master"));
                }
            }
            //Check Data DeliveryOrderId
            if (dataParam.xts_deliveryorderid != null)
            {
                if (!dapper.checkdataMasterDeliveryOrderById(dataParam.xts_deliveryorderid.ToString(), "xts_deliveryorderid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_deliveryorderid value tidak di temukan di data Master"));
                }
            }
            //Check Data DeliveryTypeId
            if (dataParam.xts_deliverytypeid != null || dataParam.xts_reasonid != null)
            {
                if (dataParam.xts_deliverytypeid != null && !dapper.checkdataMasterReasonById(dataParam.xts_deliverytypeid.ToString(), "xts_reasonid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_deliverytypeid value tidak di temukan di data Master"));
                }
                if (dataParam.xts_reasonid != null && !dapper.checkdataMasterReasonById(dataParam.xts_reasonid.ToString(), "xts_reasonid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_reasonid value tidak di temukan di data Master"));
                }
            }
            //Check Data FromBUId
            if (dataParam.xts_frombusinessunitid != null)
            {
                if (!dapper.checkdataMasterBUById(dataParam.xts_frombusinessunitid.ToString(), "businessunitid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_frombusinessunitid value tidak di temukan di data Master"));
                }
            }
            //Check Data FromSiteId
            if (dataParam.xts_fromsiteid != null)
            {
                if (!dapper.checkdataMasterSiteById(dataParam.xts_fromsiteid.ToString(), "xts_siteid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_fromsiteid value tidak di temukan di data Master"));
                }
            }
            //Check Data FromSiteId
            if (dataParam.xts_fromwarehouseid != null)
            {
                if (!dapper.checkdataMasterWareHouseById(dataParam.xts_fromwarehouseid.ToString(), "xts_warehouseid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_fromwarehouseid value tidak di temukan di data Master"));
                }
            }
            //Check Data LandedCostId
            if (dataParam.xts_landedcostid != null)
            {
                if (!dapper.checkdataMasterLandedCostById(dataParam.xts_landedcostid.ToString(), "xts_landedcostid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_landedcostid value tidak di temukan di data Master"));
                }
            }
            //Check Data InventoryTransactionId
            if (dataParam.xts_inventorytransactionid != null)
            {
                if (!dapper.checkdataMasterInventTransById(dataParam.xts_inventorytransactionid.ToString(), "xts_inventorytransactionid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_inventorytransactionid value tidak di temukan di data Master"));
                }
            }
            //Check Data InventoryTransferId
            if (dataParam.xts_inventorytransferid != null)
            {
                if (!dapper.checkdataMasterInventTransferById(dataParam.xts_inventorytransferid.ToString(), "xts_inventorytransferid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_inventorytransferid value tidak di temukan di data Master"));
                }
            }
            //Check Data OrderAPVoucherId
            if (dataParam.xts_orderapvoucherid != null)
            {
                if (!dapper.checkdataMasterAPVoucherById(dataParam.xts_orderapvoucherid.ToString(), "xts_accountpayablevoucherid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_orderapvoucherid value tidak di temukan di data Master"));
                }
            }
            //Check Data OutSourceWorkOrderId
            if (dataParam.xts_outsourceworkorderid != null)
            {
                if (!dapper.checkdataMasterOutSourceWorkOrderById(dataParam.xts_outsourceworkorderid.ToString(), "xts_outsourceworkorderid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_outsourceworkorderid value tidak di temukan di data Master"));
                }
            }
            //Check Data OutSourceWorkShopId
            if (dataParam.xts_outsourceworkshopid != null)
            {
                if (!dapper.checkdataMasterOutSourceWorkshopConfigById(dataParam.xts_outsourceworkshopid.ToString(), "xts_outsourceworkshopconfigurationid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_outsourceworkshopid value tidak di temukan di data Master"));
                }
            }
            //Check Data PBUId
            if (dataParam.xts_parentbusinessunitid != null)
            {
                if (!dapper.checkdataMasterBUById(dataParam.xts_parentbusinessunitid.ToString(), "businessunitid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_parentbusinessunitid value tidak di temukan di data Master"));
                }
            }
            //Check Data ProductSubtituteId
            if (dataParam.xts_productsubstituteid != null)
            {
                if (!dapper.checkdataMasterProductSubtituteById(dataParam.xts_productsubstituteid.ToString(), "xts_productsubstituteid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_productsubstituteid value tidak di temukan di data Master"));
                }
            }
            //Check Data PurchaseReceiptId
            if (dataParam.xts_purchasereceiptid != null)
            {
                if (!dapper.checkdataMasterPurchaseReceiptById(dataParam.xts_purchasereceiptid.ToString(), "xts_purchasereceiptid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_purchasereceiptid value tidak di temukan di data Master"));
                }
            }
            //Check Data PurchaseRequisitionId
            if (dataParam.xts_purchaserequisitionid != null)
            {
                if (!dapper.checkdataMasterPurchaseRequisitionById(dataParam.xts_purchaserequisitionid.ToString(), "xts_purchaserequisitionid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_purchaserequisitionid value tidak di temukan di data Master"));
                }
            }
            //Check Data PurchaseOrderId
            if (dataParam.xts_purchaseorderid != null || dataParam.xts_orderpurchaseorderid != null)
            {
                if (dataParam.xts_purchaseorderid != null && !dapper.checkdataMasterPurchaseOrderById(dataParam.xts_purchaseorderid.ToString(), "xts_purchaseorderid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_purchaseorderid value tidak di temukan di data Master"));
                }
                if (dataParam.xts_orderpurchaseorderid != null && !dapper.checkdataMasterPurchaseOrderById(dataParam.xts_orderpurchaseorderid.ToString(), "xts_purchaseorderid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_orderpurchaseorderid value tidak di temukan di data Master"));
                }
            }
            //Check Data SalesOrderId
            if (dataParam.xts_salesorderid != null)
            {
                if (!dapper.checkdataMasterSalesOrderById(dataParam.xts_salesorderid.ToString(), "xts_salesorderid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_salesorderid value tidak di temukan di data Master"));
                }
            }
            //Check Data StockInventoryNewVehicleId
            if (dataParam.xts_stockinventorynewvehicleid != null)
            {
                if (!dapper.checkdataMasterInventoryNVById(dataParam.xts_stockinventorynewvehicleid.ToString(), "xts_inventorynewvehicleid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_stockinventorynewvehicleid value tidak di temukan di data Master"));
                }
            }
            //Check Data ToBusinessunitId
            if (dataParam.xts_tobusinessunitid != null)
            {
                if (!dapper.checkdataMasterBUById(dataParam.xts_tobusinessunitid.ToString(), "businessunitid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_tobusinessunitid value tidak di temukan di data Master"));
                }
            }
            //Check Data ToSiteId
            if (dataParam.xts_tositeid != null)
            {
                if (!dapper.checkdataMasterSiteById(dataParam.xts_tositeid.ToString(), "xts_siteid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_tositeid value tidak di temukan di data Master"));
                }
            }
            //Check Data ToWareHouseId
            if (dataParam.xts_towarehouseid != null)
            {
                if (!dapper.checkdataMasterWareHouseById(dataParam.xts_towarehouseid.ToString(), "xts_warehouseid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_towarehouseid value tidak di temukan di data Master"));
                }
            }
            //Check Data WriteOffBalanceId
            if (dataParam.xts_writeoffbalanceid != null)
            {
                if (!dapper.checkdataMasterWriteOffBalanceById(dataParam.xts_writeoffbalanceid.ToString(), "xts_writeoffbalanceid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_writeoffbalanceid value tidak di temukan di data Master"));
                }
            }
            //Check Data OutsourceWOReceiptId
            if (dataParam.xts_outsourceworkorderreceiptid != null)
            {
                if (!dapper.checkdataMasterOutsourceWOReceiptById(dataParam.xts_outsourceworkorderreceiptid.ToString(), "xts_outsourceworkorderreceiptid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_outsourceworkorderreceiptid value tidak di temukan di data Master"));
                }
            }
            //Check Data ProductSegment1Id
            if (dataParam.xts_productsegment1id != null)
            {
                if (!dapper.checkdataMasterProductSegment1ById(dataParam.xts_productsegment1id.ToString(), "xts_productsegment1id"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_productsegment1id value tidak di temukan di data Master"));
                }
            }
            //Check Data ProductClassId
            if (dataParam.xts_productclassid != null)
            {
                if (!dapper.checkdataMasterProductClassById(dataParam.xts_productclassid.ToString(), "xts_productclassid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_productclassid value tidak di temukan di data Master"));
                }
            }
            //Check Data ProvinceId
            if (dataParam.xts_provinceid != null)
            {
                if (!dapper.checkdataMasterProvinceById(dataParam.xts_provinceid.ToString(), "xts_provinceid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_provinceid value tidak di temukan di data Master"));
                }
            }
            //Check Data JobPositionId
            if (dataParam.ktb_jobpositionid != null)
            {
                if (!dapper.checkdataMasterJobPositionById(dataParam.ktb_jobpositionid.ToString(), "ktb_jobpositionid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field ktb_jobpositionid value tidak di temukan di data Master"));
                }
            }
            //Check Data VehicleBrandId
            if (dataParam.xts_vehiclebrandid != null)
            {
                if (!dapper.checkdataMasterVehicleBrandById(dataParam.xts_vehiclebrandid.ToString(), "xts_vehiclebrandid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_vehiclebrandid value tidak di temukan di data Master"));
                }
            }
            //Check Data Dimension1
            if (dataParam.xts_dimension1id != null)
            {
                if (!dapper.checkdataMasterDimension1ById(dataParam.xts_dimension1id.ToString(), "xts_dimension1id"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_dimension1id value tidak di temukan di data Master"));
                }
            }
            //Check Data Dimension2
            if (dataParam.xts_dimension2id != null)
            {
                if (!dapper.checkdataMasterDimension2ById(dataParam.xts_dimension2id.ToString(), "xts_dimension2id"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_dimension2id value tidak di temukan di data Master"));
                }
            }
            //Check Data Dimension3
            if (dataParam.xts_dimension3id != null)
            {
                if (!dapper.checkdataMasterDimension3ById(dataParam.xts_dimension3id.ToString(), "xts_dimension3id"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_dimension3id value tidak di temukan di data Master"));
                }
            }
            //Check Data Dimension4
            if (dataParam.xts_dimension4id != null)
            {
                if (!dapper.checkdataMasterDimension4ById(dataParam.xts_dimension4id.ToString(), "xts_dimension4id"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_dimension4id value tidak di temukan di data Master"));
                }
            }
            //Check Data Dimension5
            if (dataParam.xts_dimension5id != null)
            {
                if (!dapper.checkdataMasterDimension5ById(dataParam.xts_dimension5id.ToString(), "xts_dimension5id"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_dimension5id value tidak di temukan di data Master"));
                }
            }
            //Check Data Dimension6
            if (dataParam.xts_dimension6id != null)
            {
                if (!dapper.checkdataMasterDimension6ById(dataParam.xts_dimension6id.ToString(), "xts_dimension6id"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_dimension6id value tidak di temukan di data Master"));
                }
            }
            //Check Data PerlengkapanStandardId
            if (dataParam.ktb_perlengkapanstandardid != null)
            {
                if (!dapper.checkdataMasterPerlengkapanStandardById(dataParam.ktb_perlengkapanstandardid.ToString(), "ktb_perlengkapanstandardid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field ktb_perlengkapanstandardid value tidak di temukan di data Master"));
                }
            }
            //Check Data ServiceId
            if (dataParam.serviceid != null)
            {
                if (!dapper.checkdataMasterServiceById(dataParam.serviceid.ToString(), "serviceid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field serviceid value tidak di temukan di data Master"));
                }
            }
            //Check Data Purchase Receipt Detail
            if (dataParam.xts_purchasereceiptdetailid != null)
            {
                if (!dapper.checkdataMasterPurchaseReceiptDetailById(dataParam.serviceid.ToString(), "xts_purchasereceiptdetailid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_purchasereceiptdetailid value tidak di temukan di data Master"));
                }
            }
            //Check Data Employee to Equipment
            if (dataParam.xts_employeeidWOTR != null)
            {
                if (!dapper.checkdataMasterEquipmentById(dataParam.xts_employeeidWOTR.ToString(), "equipmentid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_employeeid value tidak di temukan di data Master"));
                }
            }
            //Check Data Outsource BU
            if (dataParam.xts_outsourcebusinessunitid != null)
            {
                if (!dapper.checkdataMasterBusinessUnitById(dataParam.xts_outsourcebusinessunitid.ToString(), "businessunitid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field xts_outsourcebusinessunitid value tidak di temukan di data Master"));
                }
            }
            //Check Data Booking Status
            if (dataParam.bookingstatus != null)
            {
                if (!dapper.checkdataMasterBookingStatusById(dataParam.bookingstatus.ToString(), "bookingstatusid"))
                {
                    validationResults.Add(new DNetValidationResult(IndexData + "Field bookingstatus value tidak di temukan di data Master"));
                }
            }
            #endregion

            #region Get Pick List Data
            if (dataParam.pickList != null && dataParam.pickList.Count > 0)
            {
                for (int i = 0; i < dataParam.pickList.Count; i++)
                {
                    string rst = dapper.checkdataMasterStandardCode(dataParam.pickList[i]);
                    if (rst != "")
                    {
                        string[] field = dataParam.pickList[i].Category.Split('.');
                        validationResults.Add(new DNetValidationResult(field[1] == null ? "" : IndexData + "Field " + field[1].ToString() + " value tidak di temukan di list berikut -> " + rst));
                    }
                }
            }
            #endregion

            return validationResults.Count == 0;
        }

        #region Get Check Dealer Category Config
        public static bool CheckDealerCategoryConfig()
        {
            var isCheck = false;
            #region Initialize Mapper
            var _dealerMapper = MapperFactory.GetInstance().GetMapper(typeof(Dealer).ToString());
            var _dealerCategoryMapper = MapperFactory.GetInstance().GetMapper(typeof(DealerCategory).ToString());
            var _profileDetailMapper = MapperFactory.GetInstance().GetMapper(typeof(ProfileDetail).ToString());
            var _categoryMapper = MapperFactory.GetInstance().GetMapper(typeof(Category).ToString());
            var _appConfigMapper = MapperFactory.GetInstance().GetMapper(typeof(AppConfig).ToString());
            #endregion

            #region Check App Config
            var appConfigs = _appConfigMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(AppConfig), "Name", "CheckDealerCategory"));

            if (appConfigs.Count > 0)
            {
                var appConfig = (AppConfig)appConfigs[0];
                if (appConfig.Value == "1")
                {
                    isCheck = true;
                }
            }

            return isCheck;
            #endregion
        }
        #endregion

        #region Get Dealer Category Id
        public static bool GetDealerCategoryId(string dealerCode, out List<int> dealerCategoryIdList)
        {
            #region Initialize 
            var _dealerCategoryMapper = MapperFactory.GetInstance().GetMapper(typeof(DealerCategory).ToString());
            var _dealerMapper = MapperFactory.GetInstance().GetMapper(typeof(Dealer).ToString());

            dealerCategoryIdList = new List<int>();
            #endregion

            #region Get Dealer
            var dealers = _dealerMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(Dealer), "DealerCode", dealerCode));
            if (dealers.Count == 0)
            {
                return false;
            }

            var dealer = (Dealer)dealers[0];
            #endregion
            #region Get Dealer Category
            // Get Dealer Category
            var dealerCategories = _dealerCategoryMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(DealerCategory), "Dealer.ID", dealer.ID));
            if (dealerCategories.Count == 0)
            {
                return false;
            }
            var dealerCategoryList = dealerCategories.Cast<DealerCategory>().ToList();
            dealerCategoryIdList = dealerCategoryList.Select(x => x.Category.ID).ToList();
            return true;
            #endregion
        }
        #endregion

        public static bool ValidateDataCampaign(int EventType, string CampaignName, string DealerCode)
        {
            try
            {
                bool result = false;
                VWI_CampaignFilterDto filterDto = new VWI_CampaignFilterDto();
                
                #region use Mapper
                var _DealerMapper = MapperFactory.GetInstance().GetMapper(typeof(Dealer).ToString());
                var _BabitMapper = MapperFactory.GetInstance().GetMapper(typeof(BabitHeader).ToString());
                var _BabitMasterEventTypeMapper = MapperFactory.GetInstance().GetMapper(typeof(BabitMasterEventType).ToString());
                var _EventMapper = MapperFactory.GetInstance().GetMapper(typeof(BabitEventProposalHeader).ToString());
                var _NationalMapper = MapperFactory.GetInstance().GetMapper(typeof(NationalEvent).ToString());
                var _NationalDetailMapper = MapperFactory.GetInstance().GetMapper(typeof(NationalEventDetail).ToString());

                var dealers = _DealerMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(Dealer), "DealerCode", DealerCode));
                if (dealers != null && dealers.Count > 0)
                {
                    Dealer dealer_ = dealers[0] as Dealer;
                    if (EventType == 1)
                    {
                        List<MatchTypeFilter> filters = new List<MatchTypeFilter>();
                        string[] nm = "BabitRegNumber|RowStatus".Split('|');
                        string[] val = (CampaignName + "|0").Split('|');
                        for (int i = 0; i < nm.Length; i++)
                        {
                            MatchTypeFilter matchFilter = new MatchTypeFilter();
                            matchFilter.MatchType = 0;
                            matchFilter.PropertyName = nm[i];
                            matchFilter.PropertyValue = val[i];
                            matchFilter.SqlOperation = 0;
                            filters.Add(matchFilter);
                        }
                        filterDto.find = filters;
                        var ctrFltr = Helper.BuildCriteria(typeof(BabitHeader), filterDto);
                        var babit = _BabitMapper.RetrieveByCriteria(ctrFltr);

                        foreach (BabitHeader item in babit)
                        {
                            VWI_CampaignFilterDto filterDto2 = new VWI_CampaignFilterDto();
                            List<MatchTypeFilter> filters2 = new List<MatchTypeFilter>();
                            string[] nm2 = "ID|RowStatus".Split('|');
                            string[] val2 = (item.BabitMasterEventType.ID.ToString() + "|0").Split('|');
                            for (int i = 0; i < nm.Length; i++)
                            {
                                MatchTypeFilter matchFilter = new MatchTypeFilter();
                                matchFilter.MatchType = 0;
                                matchFilter.PropertyName = nm2[i];
                                matchFilter.PropertyValue = val2[i];
                                matchFilter.SqlOperation = 0;
                                filters2.Add(matchFilter);
                            }
                            filterDto2.find = filters2;
                            var ctrFltr2 = Helper.BuildCriteria(typeof(BabitMasterEventType), filterDto2);
                            var babitMasterEventType = _BabitMasterEventTypeMapper.RetrieveByCriteria(ctrFltr2);

                            if (babitMasterEventType.Count > 0)
                            {
                                string babitDealerGrp = string.IsNullOrEmpty(item.BabitDealerGroup.Trim()) ? item.Dealer.ID.ToString() : (item.Dealer.ID.ToString() + ";" + item.BabitDealerGroup);
                                string[] groupDealer = (babitDealerGrp).Split(';');
                                foreach (var x in groupDealer)
                                {
                                    if (Convert.ToInt32(x) == dealer_.ID)
                                    {
                                        result = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else if (EventType == 2)
                    {
                        List<MatchTypeFilter> filters = new List<MatchTypeFilter>();
                        string[] nm = "Dealer.ID|EventRegNumber|RowStatus".Split('|');
                        string[] val = (dealer_.ID.ToString() + "|" + CampaignName + "|0").Split('|');
                        for (int i = 0; i < nm.Length; i++)
                        {
                            MatchTypeFilter matchFilter = new MatchTypeFilter();
                            matchFilter.MatchType = 0;
                            matchFilter.PropertyName = nm[i];
                            matchFilter.PropertyValue = val[i];
                            matchFilter.SqlOperation = 0;
                            filters.Add(matchFilter);
                        }
                        filterDto.find = filters;
                        var ctrFltr = Helper.BuildCriteria(typeof(BabitEventProposalHeader), filterDto);
                        var event_ = _EventMapper.RetrieveByCriteria(ctrFltr);

                        result = event_.Count > 0;
                    }
                    else if (EventType == 3)
                    {
                        var nationalHeader = _NationalMapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(NationalEvent), "RegNumber", CampaignName));
                        if (nationalHeader != null && nationalHeader.Count > 0)
                        {
                            NationalEvent ne = nationalHeader[0] as NationalEvent;

                            List<MatchTypeFilter> filters = new List<MatchTypeFilter>();
                            string[] nm = "Dealer.ID|NationalEvent.ID|RowStatus".Split('|');
                            string[] val = (dealer_.ID.ToString() + "|" + ne.ID + "|0").Split('|');
                            for (int i = 0; i < nm.Length; i++)
                            {
                                MatchTypeFilter matchFilter = new MatchTypeFilter();
                                matchFilter.MatchType = 0;
                                matchFilter.PropertyName = nm[i];
                                matchFilter.PropertyValue = val[i];
                                matchFilter.SqlOperation = 0;
                                filters.Add(matchFilter);
                            }
                            filterDto.find = filters;
                            var ctrFltr = Helper.BuildCriteria(typeof(NationalEventDetail), filterDto);
                            var nationalevent_ = _NationalDetailMapper.RetrieveByCriteria(ctrFltr);

                            result = nationalevent_.Count > 0;
                        }
                    }
                }
                #endregion

                //bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Get Methods
        /// <summary>
        /// Get StandardCodeChar by value
        /// </summary>
        /// <param name="value"></param>                
        /// <param name="category"></param>
        public static StandardCodeChar GetStandardCodeCharByValue(string category, string value)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(StandardCodeChar).ToString());

            // get by criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(StandardCodeChar), "Category", "ValueId", category, value));
            if (masters.Count > 0)
                return masters[0] as StandardCodeChar;

            return null;
        }

        /// <summary>
        /// Get SparepartPOTypeTOP by poType
        /// </summary>
        /// <param name="poType"></param>                
        public static SparePartPOTypeTOP GetSparepartPOTypeTOP(string poType)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(SparePartPOTypeTOP).ToString());

            // get by criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(SparePartPOTypeTOP), "SparePartPOType", "IsTOP", poType, 1));
            if (masters.Count > 0)
                return masters[0] as SparePartPOTypeTOP;

            return null;
        }

        /// <summary>
        /// Get TermOfPaymentIDNotTOP by poType
        /// </summary>
        /// <param name="poType"></param>                
        public static int GetSparepartTOPIdNotTOP(string poType)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(SparePartPOTypeTOP).ToString());

            // get by criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(SparePartPOTypeTOP), "SparePartPOType", poType));
            if (masters.Count > 0)
            {
                var data = masters[0] as SparePartPOTypeTOP;
                return data.TermOfPaymentIDNotTOP.ID;
            }

            return 1;
        }

        /// <summary>
        /// Get Dealer Dto by Code
        /// </summary>
        /// <param name="code"></param>
        /// <param name="dealerCode"></param>
        /// <returns></returns>
        public static ResponseBase<DealerDto> GetDealerDtoByCode(string code, string dealerCode)
        {
            var result = new ResponseBase<DealerDto>();
            Dealer dealer = null;
            List<DNetValidationResult> validationResult = new List<DNetValidationResult>();

            if (ValidationHelper.ValidateDealer(code, validationResult, dealerCode, ref dealer, false))
            {
                var _mapper = AutoMapperConfig.InitializeAutoMapper().CreateMapper();
                var DealerDto = _mapper.Map<DealerDto>(dealer);
                result.lst = DealerDto;
                result.success = true;
                result._id = DealerDto.ID;
                result.total = 1;
            }
            else
            {
                foreach (ValidationResult item in validationResult)
                {
                    result.messages.Add(new MessageBase { ErrorCode = ErrorCode.DataReadNotAvailable, ErrorMessage = item.ErrorMessage });
                }
            }

            return result;
        }

        /// <summary>
        /// Get domain by code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static ResponseBase<DealerBranch> GetDealerBranchByCode(string code)
        {
            var result = new ResponseBase<DealerBranch>();

            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(DealerBranch).ToString());

            // get by criteria
            CriteriaComposite criterias = new CriteriaComposite(new Criteria(typeof(DealerBranch), "DealerBranchCode", MatchType.Exact, code));
            var branches = _mapper.RetrieveByCriteria(criterias);
            if (branches.Count > 0)
            {
                result.lst = (DealerBranch)branches[0];
                result.success = true;
                result._id = result.lst.ID;
                result.total = 1;
            }
            else
            {
                result.messages.Add(new MessageBase { ErrorCode = ErrorCode.DataReadNotAvailable, ErrorMessage = String.Format(MessageResource.ErrorMessageDataNotFoundWithColumn, typeof(BenefitMasterHeader).Name, Helper.GetCriteriasMessageFormat(typeof(BenefitMasterHeader), null, "DealerBranchCode", code)) });
            }

            return result;
        }

        /// <summary>
        /// Get VWI Fleet by code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static VWI_Fleet GetByVWIFleetCode(string code)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(VWI_Fleet).ToString());

            // retrieve by certain criteria
            var criterias = new CriteriaComposite(new Criteria(typeof(VWI_Fleet), "FleetCode", MatchType.Exact, code));
            var masters = _mapper.RetrieveByCriteria(criterias);
            if (masters.Count > 0)
            {
                return (VWI_Fleet)masters[0];
            }

            return null;
        }

        /// <summary>
        /// Get by code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static PartShop GetByPartShopCode(string code)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(PartShop).ToString());

            // retrieve by certain criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(PartShop), "PartShopCode", code));
            if (masters.Count > 0)
            {
                return (PartShop)masters[0];
            }

            return null;
        }

        /// <summary>
        /// Get by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static PartShop GetByPartShopByName(string name)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(PartShop).ToString());

            // retrieve by certain criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(PartShop), "Name", name));
            if (masters.Count > 0)
            {
                return (PartShop)masters[0];
            }

            return null;
        }

        /// <summary>
        /// Get SparePartMasterTOP
        /// </summary>
        /// <param name="DMSPRNosparepartMasterID"></param>
        /// <returns></returns>
        public static SparePartMasterTOP GetMasterTop(int sparepartMasterID, int poTypeID)
        {
            // initialize the mapper
            var _mapper = MapperFactory.GetInstance().GetMapper(typeof(SparePartMasterTOP).ToString());

            // retrieve by certain criteria
            var masters = _mapper.RetrieveByCriteria(Helper.GenerateCriteria(typeof(SparePartMasterTOP), "SparePartMaster.ID", "SparePartPOTypeTOP.ID", sparepartMasterID, poTypeID));
            if (masters.Count > 0)
                return masters[0] as SparePartMasterTOP;

            return null;
        }

        /// <summary>
        /// Determine whether the customer code is customer code retail
        /// </summary>
        /// <param name="customerCode"></param>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public static bool IsCustomerRetailByCode(string customerCode, AutoMapper.IMapper mapper)
        {
            IAppConfigBL appConfigBL = new AppConfigBL(mapper);
            AppConfig appConfig = appConfigBL.GetConfigByName("CustomerRetailCode");
            if (appConfig != null)
            {
                return appConfig.Value.Equals(customerCode, StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }
        #endregion
    }

    #region Validation Dapper
    public class ValidationDapper
    {
        #region Get Data Master By Id
        //Get Error Message List
        public List<MessageBase> messageList(List<DNetValidationResult> validationResults)
        {
            List<MessageBase> errMsg = new List<MessageBase>();
            for (int i = 0; i < validationResults.Count; i++)
            {
                MessageBase mb = new MessageBase();
                mb.ErrorCode = ErrorCode.DataRequiredField;
                mb.ErrorMessage = validationResults[i].ErrorMessage;
                errMsg.Add(mb);
            }
            return errMsg;
        }

        //Get Data CRM_businessunit
        public ValidationParameterDto getdataMasterBusinessUnitByDealerCode(string DealerCode, List<DNetValidationResult> validationResults)
        {
            ValidationParameterDto data = new ValidationParameterDto();
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_BusinessUnitRepository _CRM_BusinessUnitRepo = new VWI_CRM_BusinessUnitRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_BusinessUnit), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "name", DealerCode, false, criteriasFilter);
                IFDomain.VWI_CRM_BusinessUnit dataMaster = _CRM_BusinessUnitRepo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow).FirstOrDefault();

                if (dataMaster != null)
                {
                    data.xts_company = dataMaster.msdyn_companycode;
                    data.xts_businessunitid = dataMaster.businessunitid;
                    data.owningbusinessunit = dataMaster.businessunitid;
                }
                else
                {
                    validationResults.Add(new DNetValidationResult("Data DealerCode '" + DealerCode + "' tidak ditemukan di data Master"));
                }
            }
            catch (Exception ex)
            {
                validationResults.Add(new DNetValidationResult("Error : " + ex.Message));
            }

            return data;
        }

        //Get Data CRM_owner by Id
        public bool checkdataMasterOwnerById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_ownerRepository _Repo = new VWI_CRM_ownerRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_owner), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_transactioncurrency by Id
        public bool checkdataMasterTransactioncurrencyById(string Id)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_transactioncurrencyRepository _Repo = new VWI_CRM_transactioncurrencyRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_transactioncurrency), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "transactioncurrencyid", Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_city by Id
        public bool checkdataMasterCityById(string Id)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_cityRepository _Repo = new VWI_CRM_xts_cityRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_city), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_cityid", Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_customerclass by Id
        public bool checkdataMasterCustomerClassById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_customerclassRepository _Repo = new VWI_CRM_xts_customerclassRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_customerclass), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_account by Id
        public bool checkdataMasterAccountById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_accountRepository _Repo = new VWI_CRM_accountRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_account), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IFDomain.VWI_CRM_account getdataMasterAccountById(string Id)
        {
            try
            {
                IFDomain.VWI_CRM_account account = new IFDomain.VWI_CRM_account();
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_accountRepository _Repo = new VWI_CRM_accountRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_account), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "accountid", Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);
                if (data.Count > 0)
                {
                    account = data.FirstOrDefault();
                }

                return account;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_vendorclass by Id
        public bool checkdataMasterVendorClassById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_vendorclassRepository _Repo = new VWI_CRM_xts_vendorclassRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_vendorclass), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_employee by Id
        public bool checkdataMasterEmployeeById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_employeeRepository _Repo = new VWI_CRM_xts_employeeRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_employee), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_incomingoutsourceworkorder by Id
        public bool checkdataMasterIncomingOutSourceWorkOrderById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_incomingoutsourceworkorderRepository _Repo = new VWI_CRM_xts_incomingoutsourceworkorderRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_incomingoutsourceworkorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_manufacturer by Id
        public bool checkdataMasterManufacturerById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_manufacturerRepository _Repo = new VWI_CRM_xts_manufacturerRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_manufacturer), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_newvehiclesalesorder by Id
        public bool checkdataMasterNVSOById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_newvehiclesalesorderRepository _Repo = new VWI_CRM_xts_newvehiclesalesorderRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_newvehiclesalesorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_opportunity by Id
        public bool checkdataMasterOpportunityById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_opportunityRepository _Repo = new VWI_CRM_opportunityRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_opportunity), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_ordertype by Id
        public bool checkdataMasterOrderTypeById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_ordertypeRepository _Repo = new VWI_CRM_xts_ordertypeRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_ordertype), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_product by Id
        public bool checkdataMasterProductById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_productRepository _Repo = new VWI_CRM_xts_productRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_product), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_productexteriorcolor by Id
        public bool checkdataMasterProductExteriorColorById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_productexteriorcolorRepository _Repo = new VWI_CRM_xts_productexteriorcolorRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_productexteriorcolor), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_productsegment3 by Id
        public bool checkdataMasterProductSegment3ById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_productsegment3Repository _Repo = new VWI_CRM_xts_productsegment3Repository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_productsegment3), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_servicereservationclass by Id
        public bool checkdataMasterServiceReservationClassById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_servicereservationclassRepository _Repo = new VWI_CRM_xts_servicereservationclassRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_servicereservationclass), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_serviceappointment by Id
        public bool checkdataMasterServiceAppointmentById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_serviceappointmentRepository _Repo = new VWI_CRM_serviceappointmentRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_serviceappointment), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_servicecategory by Id
        public bool checkdataMasterServiceCategoryById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_servicecategoryRepository _Repo = new VWI_CRM_xts_servicecategoryRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_servicecategory), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_site by Id
        public bool checkdataMasterSiteById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_siteRepository _Repo = new VWI_CRM_xts_siteRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_site), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_vehiclepricedetail by Id
        public bool checkdataMasterVehiclePriceDetailById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_vehiclepricedetailRepository _Repo = new VWI_CRM_xts_vehiclepricedetailRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_vehiclepricedetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_common by Id
        public bool checkdataMasterCommonById(string Id, string field, bool isServiceTypeWO = false)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_commonRepository _Repo = new VWI_CRM_xts_commonRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_common), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                if (isServiceTypeWO)
                {
                    criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_common), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_common", "None", false, criteriasFilter);
                    criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_common), new MatchTypeFilter { SqlOperation = SQLOperation.Or }, MatchType.Exact.GetHashCode(), "xts_common", "SB", false, criteriasFilter);
                }

                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_vehicleinformation by Id
        public bool checkdataMasterVehicleInformationById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_vehicleinformationRepository _Repo = new VWI_CRM_xts_vehicleinformationRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_vehicleinformation), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_vehicleprice By Id
        public bool checkdataMasterVehiclePriceById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_vehiclepriceRepository _Repo = new VWI_CRM_xts_vehiclepriceRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_vehicleprice), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_warehouse By Id
        public bool checkdataMasterWareHouseById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_warehouseRepository _Repo = new VWI_CRM_xts_warehouseRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_warehouse), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_newvehiclewholesaleorder By Id
        public bool checkdataMasterNVWSOById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_newvehiclewholesaleorderRepository _Repo = new VWI_CRM_xts_newvehiclewholesaleorderRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_newvehiclewholesaleorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Get Data CRM_xts_workorder By Id
        public bool checkdataMasterWorkOrderById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_workorderRepository _Repo = new VWI_CRM_xts_workorderRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_workorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_purchaserequisitionpurchaseordertype By Id
        public bool checkdataMasterPRPOTById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_purchaserequisitionpurchaseordertypeRepository _Repo = new VWI_CRM_xts_purchaserequisitionpurchaseordertypeRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_purchaserequisitionpurchaseordertype), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_nvsonumberregistrationdetails By Id
        public bool checkdataMasterNVSONumRegistDetailById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_nvsonumberregistrationdetailsRepository _Repo = new VWI_CRM_xts_nvsonumberregistrationdetailsRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_nvsonumberregistrationdetails), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_territory By Id
        public bool checkdataMasterTerritoryById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_territoryRepository _Repo = new VWI_CRM_territoryRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_territory), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_vendor By Id
        public bool checkdataMasterVendorById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_vendorRepository _Repo = new VWI_CRM_xts_vendorRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_vendor), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_uom By Id
        public bool checkdataMasterUOMById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_uomRepository _Repo = new VWI_CRM_xts_uomRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_uom), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_inventorynewvehicle By Id
        public bool checkdataMasterInventoryNVById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_inventorynewvehicleRepository _Repo = new VWI_CRM_xts_inventorynewvehicleRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_inventorynewvehicle), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_cashandbank By Id
        public bool checkdataMasterCashAndBankById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_cashandbankRepository _Repo = new VWI_CRM_xts_cashandbankRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_cashandbank), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_accountreceivableinvoice By Id
        public bool checkdataMasterARInvoiceById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_accountreceivableinvoiceRepository _Repo = new VWI_CRM_xts_accountreceivableinvoiceRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_accountreceivableinvoice), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_accountreceivablereceipt By Id
        public bool checkdataMasterARReceiptById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_accountreceivablereceiptRepository _Repo = new VWI_CRM_xts_accountreceivablereceiptRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_accountreceivablereceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_accountpayablevoucher By Id
        public bool checkdataMasterAPVoucherById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_accountpayablevoucherRepository _Repo = new VWI_CRM_xts_accountpayablevoucherRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_accountpayablevoucher), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_ktb_kontrabon By Id
        public bool checkdataMasterKontrabonById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_ktb_kontrabonRepository _Repo = new VWI_CRM_ktb_kontrabonRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_ktb_kontrabon), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xjp_predeliveryinspection By Id
        public bool checkdataMasterPDIById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xjp_predeliveryinspectionRepository _Repo = new VWI_CRM_xjp_predeliveryinspectionRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xjp_predeliveryinspection), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_deliveryorder By Id
        public bool checkdataMasterDeliveryOrderById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_deliveryorderRepository _Repo = new VWI_CRM_xts_deliveryorderRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_deliveryorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_salesorder By Id
        public bool checkdataMasterSalesOrderById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_salesorderRepository _Repo = new VWI_CRM_xts_salesorderRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_salesorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_ktb_saleschannel By Id
        public bool checkdataMasterSalesChannelById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_ktb_saleschannelRepository _Repo = new VWI_CRM_ktb_saleschannelRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_ktb_saleschannel), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_termofpayment By Id
        public bool checkdataMasterTOPById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_termofpaymentRepository _Repo = new VWI_CRM_xts_termofpaymentRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_termofpayment), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_aptransactiondocument By Id
        public bool checkdataMasterAPTranDocById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_aptransactiondocumentRepository _Repo = new VWI_CRM_xts_aptransactiondocumentRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_aptransactiondocument), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_accountpayablepayment By Id
        public bool checkdataMasterAPPById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_accountpayablepaymentRepository _Repo = new VWI_CRM_xts_accountpayablepaymentRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_accountpayablepayment), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_consumptiontax By Id
        public bool checkdataMasterConsumptionTaxById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_consumptiontaxRepository _Repo = new VWI_CRM_xts_consumptiontaxRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_consumptiontax), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_reason By Id
        public bool checkdataMasterReasonById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_reasonRepository _Repo = new VWI_CRM_xts_reasonRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_reason), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_businessunit By Id
        public bool checkdataMasterBUById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_BusinessUnitRepository _Repo = new VWI_CRM_BusinessUnitRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_BusinessUnit), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_landedcost By Id
        public bool checkdataMasterLandedCostById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_landedcostRepository _Repo = new VWI_CRM_xts_landedcostRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_landedcost), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_inventorytransaction By Id
        public bool checkdataMasterInventTransById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_inventorytransactionRepository _Repo = new VWI_CRM_xts_inventorytransactionRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_inventorytransaction), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_inventorytransfer By Id
        public bool checkdataMasterInventTransferById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_inventorytransferRepository _Repo = new VWI_CRM_xts_inventorytransferRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_inventorytransfer), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_outsourceworkorder By Id
        public bool checkdataMasterOutSourceWorkOrderById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_outsourceworkorderRepository _Repo = new VWI_CRM_xts_outsourceworkorderRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_outsourceworkorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_outsourceworkshopconfiguration By Id
        public bool checkdataMasterOutSourceWorkshopConfigById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_outsourceworkshopconfigurationRepository _Repo = new VWI_CRM_xts_outsourceworkshopconfigurationRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_outsourceworkshopconfiguration), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_productsubstitute By Id
        public bool checkdataMasterProductSubtituteById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_productsubstituteRepository _Repo = new VWI_CRM_xts_productsubstituteRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_productsubstitute), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_purchasereceipt By Id
        public bool checkdataMasterPurchaseReceiptById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_purchasereceiptRepository _Repo = new VWI_CRM_xts_purchasereceiptRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_purchasereceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_purchaserequisition By Id
        public bool checkdataMasterPurchaseRequisitionById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_purchaserequisitionRepository _Repo = new VWI_CRM_xts_purchaserequisitionRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_purchaserequisition), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_purchaseorder By Id
        public bool checkdataMasterPurchaseOrderById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_purchaseorderRepository _Repo = new VWI_CRM_xts_purchaseorderRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_purchaseorder), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_writeoffbalance By Id
        public bool checkdataMasterWriteOffBalanceById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_writeoffbalanceRepository _Repo = new VWI_CRM_xts_writeoffbalanceRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_writeoffbalance), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Gett Data CRM_xts_outsourceworkorderreceipt By Id
        public bool checkdataMasterOutsourceWOReceiptById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_outsourceworkorderreceiptRepository _Repo = new VWI_CRM_xts_outsourceworkorderreceiptRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_outsourceworkorderreceipt), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_productsegment1 By Id
        public bool checkdataMasterProductSegment1ById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_productsegment1Repository _Repo = new VWI_CRM_xts_productsegment1Repository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_productsegment1), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_productclass By Id
        public bool checkdataMasterProductClassById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_productclassRepository _Repo = new VWI_CRM_xts_productclassRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_productclass), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_province By Id
        public bool checkdataMasterProvinceById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_provinceRepository _Repo = new VWI_CRM_xts_provinceRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_province), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_ktb_jobposition By Id
        public bool checkdataMasterJobPositionById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_ktb_jobpositionRepository _Repo = new VWI_CRM_ktb_jobpositionRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_ktb_jobposition), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_vehiclebrand By Id
        public bool checkdataMasterVehicleBrandById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_vehiclebrandRepository _Repo = new VWI_CRM_xts_vehiclebrandRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_vehiclebrand), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_dimension1 By Id
        public bool checkdataMasterDimension1ById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_dimension1Repository _Repo = new VWI_CRM_xts_dimension1Repository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_dimension1), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_dimension2 By Id
        public bool checkdataMasterDimension2ById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_dimension2Repository _Repo = new VWI_CRM_xts_dimension2Repository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_dimension2), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_dimension3 By Id
        public bool checkdataMasterDimension3ById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_dimension3Repository _Repo = new VWI_CRM_xts_dimension3Repository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_dimension3), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_dimension4 By Id
        public bool checkdataMasterDimension4ById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_dimension4Repository _Repo = new VWI_CRM_xts_dimension4Repository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_dimension4), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_dimension5 By Id
        public bool checkdataMasterDimension5ById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_dimension5Repository _Repo = new VWI_CRM_xts_dimension5Repository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_dimension5), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_dimension6 By Id
        public bool checkdataMasterDimension6ById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_dimension6Repository _Repo = new VWI_CRM_xts_dimension6Repository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_dimension6), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_ktb_perlengkapanstandard By Id
        public bool checkdataMasterPerlengkapanStandardById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_ktb_perlengkapanstandardRepository _Repo = new VWI_CRM_ktb_perlengkapanstandardRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_ktb_perlengkapanstandard), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_service By Id
        public bool checkdataMasterServiceById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_serviceRepository _Repo = new VWI_CRM_serviceRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_service), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_xts_purchasereceiptdetail By Id
        public bool checkdataMasterPurchaseReceiptDetailById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_xts_purchasereceiptdetailRepository _Repo = new VWI_CRM_xts_purchasereceiptdetailRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_xts_purchasereceiptdetail), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_equipment By Id
        public bool checkdataMasterEquipmentById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_equipmentRepository _Repo = new VWI_CRM_equipmentRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_equipment), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_equipment), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "xts_type", "4", false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_BusinessUnit By Id
        public bool checkdataMasterBusinessUnitById(string Id, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_BusinessUnitRepository _Repo = new VWI_CRM_BusinessUnitRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_BusinessUnit), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, Id, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data CRM_BookingStatus
        public bool checkdataMasterBookingStatusById (string value, string field)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_bookingstatusRepository _Repo = new VWI_CRM_bookingstatusRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_bookingstatus), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), field, value, false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                bool result = data.Count > 0;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region GetDataBusinessUnitByDealerCode
        public IFDomain.VWI_CRM_BusinessUnit getBUbyDealerCode(string DealerCode)
        {
            IFDomain.VWI_CRM_BusinessUnit buData = new IFDomain.VWI_CRM_BusinessUnit();
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_BusinessUnitRepository _Repo = new VWI_CRM_BusinessUnitRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_BusinessUnit), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "name", DealerCode, false, criteriasFilter);
                List<IFDomain.VWI_CRM_BusinessUnit> data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                buData = data.FirstOrDefault();
            }
            catch (Exception ex)
            {

            }
            return buData;
        }
        #endregion

        #region Check Data Standard Code
        public string checkdataMasterStandardCode(ParamPick prm)
        {
            try
            {
                var sortColl = string.Empty;
                int totalRow = 0;
                int filteredTotalRow = 0;
                string rawSql = string.Empty;
                var innerQueryCriteria = string.Empty;
                var page = 1;
                var size = 20;
                var criteriasFilter = "";
                VWI_CRM_StandardCodeRepository _Repo = new VWI_CRM_StandardCodeRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));

                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_StandardCode), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, criteriasFilter);
                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_StandardCode), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "Category", prm.Category, false, criteriasFilter);
                criteriasFilter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_StandardCode), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "ValueId", prm.ValueId.ToString(), false, criteriasFilter);
                var data = _Repo.Search(criteriasFilter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);

                string result = string.Empty;
                if (data.Count == 0)
                {
                    var filter = "";
                    filter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_StandardCode), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "RowStatus", "0", false, filter);
                    filter = Helper.UpdateStrCriteria(typeof(IFDomain.VWI_CRM_StandardCode), new MatchTypeFilter { SqlOperation = SQLOperation.And }, MatchType.Exact.GetHashCode(), "Category", prm.Category, false, filter);
                    var list = _Repo.Search(filter, innerQueryCriteria, sortColl, page, size, out filteredTotalRow, out totalRow);
                    List<string> x = new List<string>();
                    for (int i = 0; i < list.Count; i++)
                    {
                        x.Add(list[i].ValueId.ToString() + " : " + list[i].ValueCode);
                    }
                    result = string.Join(", ", x);
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region insert data
        public ResponseMessage insertDataSalesOrderByDataDO(IFDomain.CRM_xts_deliveryorder dataDO)
        {
            Guid SalesOrderId = Guid.NewGuid();
            IFDomain.CRM_xts_salesorder dataSO = new IFDomain.CRM_xts_salesorder();
            #region add data do to so
            dataSO.createdon = dataDO.createdon;
            dataSO.xts_salesordernumber = "SO-" + dataDO.xts_deliveryordernumber;
            dataSO.xts_referencenumberlookuptype = 0;
            dataSO.xts_status = "6"; //(Completed)
            dataSO.xts_customerid = dataDO.xts_customerid;
            dataSO.xts_description = "Sales Order Generated By API";
            dataSO.xts_salesorderid = SalesOrderId;
            dataSO.xts_totalbaseamount = dataDO.xts_totalbaseamount;
            dataSO.xts_overcreditlimitonhold = false;
            dataSO.xts_billtocustomerid = dataDO.xts_billtocustomerid;
            dataSO.xts_ordertypeid = dataDO.xts_ordertypeid;
            dataSO.xts_downpaymentamount = 0;
            dataSO.ktb_saleschannelid = new Guid("BE79F953-3EDE-47BC-99BA-71D8A5CDF0B4");
            dataSO.xts_totalreceipt = dataDO.xts_totalreceipt;
            dataSO.xts_exchangeratedate = null;
            dataSO.statecode = 0;
            dataSO.xts_salespersonid = dataDO.xts_salespersonid;
            dataSO.xts_termofpaymentid = dataDO.xts_termofpaymentid;
            dataSO.xts_exchangerateamount = null;
            dataSO.xts_totalconsumptiontaxamount = null;
            dataSO.xts_totalamountbeforediscount = dataDO.xts_totalamountbeforediscount;
            dataSO.xts_customernumber = dataDO.xts_customernumber;
            dataSO.xts_totaldiscountamount = dataDO.xts_totaldiscountamount;
            dataSO.xts_businessunitid = dataDO.xts_businessunitid;
            dataSO.ownerid = dataDO.ownerid;
            dataSO.modifiedon = dataDO.modifiedon;
            dataSO.xts_transactiondate = dataDO.xts_transactiondate;
            dataSO.xts_downpaymentispaid = false;
            dataSO.xts_totalwithholdingtaxamount = dataDO.xts_totalwithholdingtaxamount;
            dataSO.xts_methodofpaymentid = dataDO.xts_methodofpaymentid;
            dataSO.xts_grandtotal = dataDO.xts_grandtotal;
            dataSO.transactioncurrencyid = dataDO.transactioncurrencyid;
            dataSO.ktb_customerdescription = dataDO.ktb_customerdescription;
            dataSO.xts_shipmenttype = "1"; //(Ship Complete)
            dataSO.xts_downpaymentamountreceived = 0;
            dataSO.ktb_overdueonhold = false;
            dataSO.RowStatus = dataDO.RowStatus;
            dataSO.DealerCode = dataDO.DealerCode;
            dataSO.SourceType = dataDO.SourceType;
            #endregion

            CRM_xts_salesorderRepository _Repo = new CRM_xts_salesorderRepository(AppConfigs.ConnectionString(Constants.ConnectionStringName.AccountingConnection));
            var Result = _Repo.Create(dataSO);
            Result.Message = SalesOrderId.ToString();

            return Result;
        }
        #endregion
    }
    #endregion
}
