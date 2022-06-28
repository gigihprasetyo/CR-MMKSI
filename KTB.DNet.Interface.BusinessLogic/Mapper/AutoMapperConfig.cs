#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AutoMapperConfig mapper class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

using AutoMapper;

namespace KTB.DNet.Interface.BusinessLogic.MapperBL
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration InitializeAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SPKHeaderProfile());
                cfg.AddProfile(new SPKHeaderStatusProfile());
                cfg.AddProfile(new SPKCustomerProfile());
                cfg.AddProfile(new SAPCustomerProfile());
                cfg.AddProfile(new SPKFakturProfile());
                cfg.AddProfile(new DealerProfile());
                cfg.AddProfile(new SalesmanHeaderProfile());
                cfg.AddProfile(new VehicleTypeProfile());
                cfg.AddProfile(new VehicleColorProfile());
                cfg.AddProfile(new VehicleClassProfile());
                cfg.AddProfile(new VehicleModelProfile());
                cfg.AddProfile(new VehicleKindProfile());
                cfg.AddProfile(new VehicleKindGroupProfile());
                cfg.AddProfile(new SPKDetailProfile());
                cfg.AddProfile(new ProvinceProfile());
                cfg.AddProfile(new CityProfile());
                cfg.AddProfile(new Area1Profile());
                cfg.AddProfile(new Area2Profile());
                cfg.AddProfile(new DealerGroupProfile());
                cfg.AddProfile(new DealerBranchProfile());
                cfg.AddProfile(new PaymentMethodProfile());
                cfg.AddProfile(new PaymentPurposeProfile());
                cfg.AddProfile(new CategoryProfile());
                cfg.AddProfile(new ChassisMasterProfile());
                cfg.AddProfile(new EndCustomerProfile());
                cfg.AddProfile(new FSKindProfile());
                cfg.AddProfile(new LaborMasterProfile());
                cfg.AddProfile(new PositionWSCProfile());
                cfg.AddProfile(new PriceProfile());
                cfg.AddProfile(new TermOfPaymentProfile());
                cfg.AddProfile(new StandardCodeProfile());
                cfg.AddProfile(new PDIProfile());
                cfg.AddProfile(new ProfileGroupProfile());
                cfg.AddProfile(new BenefitMasterDealerProfile());
                cfg.AddProfile(new BenefitMasterDetailProfile());
                cfg.AddProfile(new BenefitMasterHeaderProfile());
                cfg.AddProfile(new BenefitTypeProfile());
                cfg.AddProfile(new ProfileHeaderToGroupProfile());
                cfg.AddProfile(new SPKChassisProfile());
                cfg.AddProfile(new ProfileHeaderProfile());
                cfg.AddProfile(new CustomerProfile());
                cfg.AddProfile(new SPKCustomerProfileProfile());
                cfg.AddProfile(new AppConfigProfile());
                cfg.AddProfile(new FleetProfile());
                cfg.AddProfile(new ChassisMasterPKTProfile());
                cfg.AddProfile(new StandardCodeCharProfile());
                cfg.AddProfile(new BusinessSectorHeaderProfile());
                cfg.AddProfile(new BusinessSectorDetailProfile());
                cfg.AddProfile(new CarrosserieDetailProfile());
                cfg.AddProfile(new CarrosserieHeaderProfile());
                cfg.AddProfile(new VehiclePurchaseDetailProfile());
                cfg.AddProfile(new VehiclePurchaseHeaderProfile());
                cfg.AddProfile(new EstimationEquipHeaderProfile());
                cfg.AddProfile(new EstimationEquipDetailProfile());
                cfg.AddProfile(new SparePartMasterProfile());
                cfg.AddProfile(new SparePartPOEstimateProfile());
                cfg.AddProfile(new SparePartPOEstimateDetailProfile());
                cfg.AddProfile(new SparePartPOProfile());
                cfg.AddProfile(new SparePartPODetailProfile());
                cfg.AddProfile(new IndentPartHeaderProfile());
                cfg.AddProfile(new IndentPartDetailProfile());
                cfg.AddProfile(new IndentPartPOProfile());
                cfg.AddProfile(new AssistPartStockProfile());
                cfg.AddProfile(new DepositLineProfile());
                cfg.AddProfile(new SparePartBillingProfile());
                cfg.AddProfile(new AssistPartSalesProfile());
                cfg.AddProfile(new FreeServiceProfile());
                cfg.AddProfile(new PMHeaderProfile());
                cfg.AddProfile(new AssistServiceIncomingProfile());
                cfg.AddProfile(new InventoryTransferProfile());
                cfg.AddProfile(new InventoryTransferDetailProfile());
                cfg.AddProfile(new InventoryTransactionProfile());
                cfg.AddProfile(new InventoryTransactionDetailProfile());
                cfg.AddProfile(new APPaymentProfile());
                cfg.AddProfile(new APPaymentDetailProfile());
                cfg.AddProfile(new RecallServiceProfile());
                cfg.AddProfile(new FieldFixListProfile());
                cfg.AddProfile(new POOtherVendorProfile());
                cfg.AddProfile(new POOtherVendorDetailProfile());
                cfg.AddProfile(new PartShopProfile());
                cfg.AddProfile(new AssistSalesChannelProfile());
                cfg.AddProfile(new SparePartPRFromVendorProfile());
                cfg.AddProfile(new SparePartPRDetailFromVendorProfile());
                cfg.AddProfile(new ARReceiptProfile());
                cfg.AddProfile(new ARReceiptDetailProfile());
                cfg.AddProfile(new SparePartDeliveryOrderProfile());
                cfg.AddProfile(new SparePartDeliveryOrderDetailProfile());
                cfg.AddProfile(new SparePartSalesOrderProfile());
                cfg.AddProfile(new SparePartSalesOrderDetailProfile());
                cfg.AddProfile(new SparePartDOProfile());
                cfg.AddProfile(new SparePartDODetailProfile());
                cfg.AddProfile(new TrTraineeProfile());
                cfg.AddProfile(new EmployeeSalesProfile());
                cfg.AddProfile(new CustomerVehicleProfile());
                cfg.AddProfile(new DMSWOWarrantyClaimProfile());
                cfg.AddProfile(new EmployeePartProfile());
                cfg.AddProfile(new VWI_VehicleExteriorColorProfile());
                cfg.AddProfile(new VWI_ChassisStatusFakturProfile());
                cfg.AddProfile(new MSPClaimProfile());
                cfg.AddProfile(new ChassisMasterATAProfile());
                cfg.AddProfile(new VWI_POReceiptDealerProfile());
                cfg.AddProfile(new ServiceReminderProfile());
                cfg.AddProfile(new ServiceReminderFollowUpProfile());
                cfg.AddProfile(new MobileServiceReminderProfile());
                cfg.AddProfile(new SPKDetailCustomerProfile());
                cfg.AddProfile(new SPKDetailCustomerProfileProfile());
                cfg.AddProfile(new ChassisMasterClaimHeaderProfile());
                cfg.AddProfile(new ChassisMasterClaimDetailProfile());
                cfg.AddProfile(new ChassisMasterLogisticCompanyProfile());
                cfg.AddProfile(new ChassisMasterClaimDocumentUploadProfile());
                cfg.AddProfile(new PODestinationProfile());
                cfg.AddProfile(new StallMasterProfile());
                cfg.AddProfile(new ServiceBookingProfile());
                cfg.AddProfile(new ServiceBookingActivityProfile());
                cfg.AddProfile(new StallWorkingTimeProfile());
                cfg.AddProfile(new VWI_SPKTrackingProfile());
                cfg.AddProfile(new VWI_ServiceAdvisorProfile());
                cfg.AddProfile(new VWI_ServiceCostEstimationProfile());
                cfg.AddProfile(new DealerSuggestionServiceProfile());
                cfg.AddProfile(new GetServiceTypeProfile());
                cfg.AddProfile(new ServiceRecommendationProfile());
                cfg.AddProfile(new LKPPHeaderProfile());
                cfg.AddProfile(new LKPPDetailProfile());
                cfg.AddProfile(new LKPPDealerProfile());
                cfg.AddProfile(new SparePartForecastProfile());
            });


            return config;
        }
    }
}
