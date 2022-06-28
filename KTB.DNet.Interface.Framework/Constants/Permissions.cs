
using System.Collections.Generic;
namespace KTB.DNet.Interface.Framework
{
    public partial class Constants
    {
        public static class Permissions
        {

            #region Master Constants
            public const string WebAPI_Master_VWI_VehicleType_Read = "WebAPI_Master_VWI_VehicleType_Read";

            public const string WebAPI_Master_Area1_Read = "WebAPI_Master_Area1_Read";
            public const string WebAPI_Master_Area2_Read = "WebAPI_Master_Area2_Read";
            public const string WebAPI_Master_BusinessSector_Read = "WebAPI_Master_BusinessSector_Read";
            //public const string WebAPI_Master_BusinessSectorDetail_Read = "WebAPI_Master_BusinessSectorDetail_Read";
            //public const string WebAPI_Master_BusinessSectorHeader_Read = "WebAPI_Master_BusinessSectorHeader_Read";
            public const string WebAPI_Master_Campaign_Read = "WebAPI_Master_Campaign_Read";
            public const string WebAPI_Master_Category_Read = "WebAPI_Master_Category_Read";
            //public const string WebAPI_Master_ChassisMaster_Read = "WebAPI_Master_ChassisMaster_Read";
            public const string WebAPI_Master_City_Read = "WebAPI_Master_City_Read";
            public const string WebAPI_Master_CustomerVehicle_Read = "WebAPI_Master_CustomerVehicle_Read";
            public const string WebAPI_Master_Dealer_Read = "WebAPI_Master_Dealer_Read";
            public const string WebAPI_Master_DealerBranch_Read = "WebAPI_Master_DealerBranch_Read";
            public const string WebAPI_Master_DealerGroup_Read = "WebAPI_Master_DealerGroup_Read";
            public const string WebAPI_Master_EmployeeParts_Create = "WebAPI_Master_EmployeeParts_Create";
            public const string WebAPI_Master_EmployeeParts_Read = "WebAPI_Master_EmployeeParts_Read";
            public const string WebAPI_Master_EmployeeParts_Update = "WebAPI_Master_EmployeeParts_Update";
            public const string WebAPI_Master_EmployeeSales_Create = "WebAPI_Master_EmployeeSales_Create";
            public const string WebAPI_Master_EmployeeSales_Read = "WebAPI_Master_EmployeeSales_Read";
            public const string WebAPI_Master_EmployeeSales_Update = "WebAPI_Master_EmployeeSales_Update";
            public const string WebAPI_Master_EmployeeService_Create = "WebAPI_Master_EmployeeService_Create";
            public const string WebAPI_Master_EmployeeService_Read = "WebAPI_Master_EmployeeService_Read";
            public const string WebAPI_Master_EmployeeService_Update = "WebAPI_Master_EmployeeService_Update";
            public const string WebAPI_Master_CustomerVehicle_Create = "WebAPI_Master_CustomerVehicle_Create";
            public const string WebAPI_Master_CustomerVehicle_Update = "WebAPI_Master_CustomerVehicle_Update";
            public const string WebAPI_Master_EndCustomer_Read = "WebAPI_Master_EndCustomer_Read";
            public const string WebAPI_Master_Fleet_Read = "WebAPI_Master_Fleet_Read";
            public const string WebAPI_Master_FSKind_Read = "WebAPI_Master_FSKind_Read";
            public const string WebAPI_Master_LaborMaster_Read = "WebAPI_Master_LaborMaster_Read";
            public const string WebAPI_Master_WorkOrderType_Read = "WebAPI_Master_WorkOrderType_Read";
            public const string WebAPI_Master_WorkOrderCategory_Read = "WebAPI_Master_WorkOrderCategory_Read";
            public const string WebAPI_Master_PartShop_Create = "WebAPI_Master_PartShop_Create";
            public const string WebAPI_Master_PartShop_Read = "WebAPI_Master_PartShop_Read";
            public const string WebAPI_Master_PartShop_Update = "WebAPI_Master_PartShop_Update";
            public const string WebAPI_Master_PaymentMethod_Read = "WebAPI_Master_PaymentMethod_Read";
            public const string WebAPI_Master_PaymentPurpose_Read = "WebAPI_Master_PaymentPurpose_Read";
            public const string WebAPI_Master_PositionWSC_Read = "WebAPI_Master_PositionWSC_Read";
            public const string WebAPI_Master_Price_Read = "WebAPI_Master_Price_Read";
            public const string WebAPI_Master_Province_Read = "WebAPI_Master_Province_Read";
            public const string WebAPI_Master_QuickProduct_Read = "WebAPI_Master_QuickProduct_Read";
            public const string WebAPI_Master_QuickProductAll_Read = "WebAPI_Master_QuickProductAll_Read";
            public const string WebAPI_Master_SalesChannel_Read = "WebAPI_Master_SalesChannel_Read";
            public const string WebAPI_Master_SalesmanArea_Read = "WebAPI_Master_SalesmanArea_Read";
            public const string WebAPI_Master_SalesmanLevel_Read = "WebAPI_Master_SalesmanLevel_Read";
            public const string WebAPI_Master_ServiceTemplate_Read = "WebAPI_Master_ServiceTemplate_Read";
            public const string WebAPI_Master_SparePart_Read = "WebAPI_Master_SparePart_Read";
            public const string WebAPI_Master_TermOfPayment_Read = "WebAPI_Master_TermOfPayment_Read";
            public const string WebAPI_Master_VehicleClass_Read = "WebAPI_Master_VehicleClass_Read";
            //public const string WebAPI_Master_VehicleColor_Read = "WebAPI_Master_VehicleColor_Read";
            //public const string WebAPI_Master_VehicleExteriorColor_Read = "WebAPI_Master_VehicleExteriorColor_Read";
            public const string WebAPI_Master_VehicleInformation_Read = "WebAPI_Master_VehicleInformation_Read";
            public const string WebAPI_Master_VehicleKind_Read = "WebAPI_Master_VehicleKind_Read";
            public const string WebAPI_Master_VehicleKindGroup_Read = "WebAPI_Master_VehicleKindGroup_Read";
            public const string WebAPI_Master_VehicleModel_Read = "WebAPI_Master_VehicleModel_Read";
            public const string WebAPI_Master_VehicleSpecification_Read = "WebAPI_Master_VehicleSpecification_Read";
            //public const string WebAPI_Master_VehicleType_Read = "WebAPI_Master_VehicleType_Read";
            public const string WebAPI_Master_VWICustomer_Read = "WebAPI_Master_VWICustomer_Read";
            public const string WebAPI_Master_WholeSalesPrice_Read = "WebAPI_Master_WholeSalesPrice_Read";
            public const string WebAPI_Master_ServiceType_Read = "WebAPI_Master_ServiceType_Read";
            public const string WebAPI_Master_ServicePlace_Read = "WebAPI_Master_ServicePlace_Read";
            public const string WebAPI_Master_SparePartPriceList_Read = "WebAPI_Master_SparePartPriceList_Read";
            public const string WebAPI_Master_SparePartUoM_Read = "WebAPI_Master_SparePartUoM_Read";
            public const string WebAPI_Master_Conversion_Read = "WebAPI_Master_Conversion_Read";
            public const string WebAPI_Master_JobPositionParts_Read = "WebAPI_Master_JobPositionParts_Read";
            public const string WebAPI_Master_JobPositionSales_Read = "WebAPI_Master_JobPositionSales_Read";
            public const string WebAPI_Master_JobPositionServices_Read = "WebAPI_Master_JobPositionServices_Read";
            public const string WebAPI_Master_ProfileDetailFromHeader_Read = "WebAPI_Master_ProfileDetailFromHeader_Read";
            public const string WebAPI_Master_SFDMobileSalesman_Read = "WebAPI_Master_SFDMobileSalesman_Read";
            public const string WebAPI_Master_EmployeeSales_Resign_Read = "WebAPI_Master_EmployeeSales_Resign_Read";
            public const string WebAPI_Master_EmployeeParts_Resign_Read = "WebAPI_Master_EmployeeParts_Resign_Read";
            public const string WebAPI_Master_EmployeeService_Resign_Read = "WebAPI_Master_EmployeeService_Resign_Read";
            public const string WebAPI_Master_EmployeeSales_ResignData_Read = "WebAPI_Master_EmployeeSales_ResignData_Read";
            public const string WebAPI_Master_EmployeeParts_ResignData_Read = "WebAPI_Master_EmployeeParts_ResignData_Read";
            public const string WebAPI_Master_EmployeeService_ResignData_Read = "WebAPI_Master_EmployeeService_ResignData_Read";
            public const string WebAPI_Master_PMKind_Read = "WebAPI_Master_PMKind_Read";
            public const string WebAPI_Master_CustomerVehicle_UploadImage = "WebAPI_Master_CustomerVehicle_UploadImage";
            public const string WebAPI_Master_SendASBSFID_SendCustomer = "WebAPI_Master_SendASBSFID_SendCustomer";
            public const string WebAPI_Master_SendASBSFID_SendSuspect = "WebAPI_Master_SendASBSFID_SendSuspect";
            public const string WebAPI_Master_SendASBSFID_SendProspect = "WebAPI_Master_SendASBSFID_SendProspect";
            public const string WebAPI_Master_SendASBSFID_SendProspectCreate = "WebAPI_Master_SendASBSFID_SendProspectCreate";
            public const string WebAPI_Master_SendASBSFID_SendActivity = "WebAPI_Master_SendASBSFID_SendActivity";
            public const string WebAPI_Master_SendASBSFID_SendActivityContact = "WebAPI_Master_SendASBSFID_SendActivityContact";
            public const string WebAPI_Master_SendASBSFID_SendSuspectContact = "WebAPI_Master_SendASBSFID_SendSuspectContact";
            public const string WebAPI_Master_SendASBSFID_SendActivitySuspect = "WebAPI_Master_SendASBSFID_SendActivitySuspect";
            public const string WebAPI_Master_PODestination_Read = "WebAPI_Master_PODestination_Read";
            public const string WebAPI_Master_VWI_ServiceAdvisor_Read = "WebAPI_Master_VWI_ServiceAdvisor_Read";
            public const string WebAPI_Master_LKPP_Create = "WebAPI_Master_LKPP_Create";
            public const string WebAPI_Master_LKPP_Read = "WebAPI_Master_LKPP_Read";
            public const string WebAPI_Master_LKPP_Update = "WebAPI_Master_LKPP_Update";
            public const string WebAPI_Master_VWI_BabitMasterRetailTarget_Read = "WebAPI_Master_VWI_BabitMasterRetailTarget_Read";
            public const string WebAPI_Master_SFDContact_Read = "WebAPI_Master_SFDContact_Read";
            public const string WebAPI_Master_DMSAnnouncement_Read = "WebAPI_Master_DMSAnnouncement_Read";
            
            public const string WebAPI_DepositA_Read = "WebAPI_DepositA_Read";
            public const string WebAPI_DepositB_Read = "WebAPI_DepositB_Read";
            public const string WebAPI_DepositC_Read = "WebAPI_DepositC_Read";
            public const string WebAPI_DepositC2_Read = "WebAPI_DepositC2_Read";

            public const string WebAPI_IPAddressRestriction_Read = "WebAPI_IPAddressRestriction_Read";
            #endregion

            #region Service
            public const string WebAPI_Service_CustomerCase_Create = "WebAPI_Service_CustomerCase_Create";
            public const string WebAPI_Service_DMSWorkOrderWSCStatus_Read = "WebAPI_Service_DMSWorkOrderWSCStatus_Read";
            public const string WebAPI_Service_DMSWOWarranty_Create = "WebAPI_Service_DMSWOWarranty_Create";
            public const string WebAPI_Service_FieldFix_Create = "WebAPI_Service_FieldFix_Create";
            public const string WebAPI_Service_FieldFix_Read = "WebAPI_Service_FieldFix_Read";
            public const string WebAPI_Service_FieldFixServiced_Read = "WebAPI_Service_FieldFixServiced_Read";
            public const string WebAPI_Service_FreeService_Create = "WebAPI_Service_FreeService_Create";
            public const string WebAPI_Service_FreeService_Read = "WebAPI_Service_FreeService_Read";
            public const string WebAPI_Service_MSPMembership_Read = "WebAPI_Service_MSPMembership_Read";
            public const string WebAPI_Service_MSPExMembership_Read = "WebAPI_Service_MSPExMembership_Read";
            public const string WebAPI_Service_PDI_Create = "WebAPI_Service_PDI_Create";
            public const string WebAPI_Service_PDI_Read = "WebAPI_Service_PDI_Read";
            public const string WebAPI_Service_PDI_Delete = "WebAPI_Service_PDI_Delete";
            public const string WebAPI_Service_PDI_GetFile = "WebAPI_Service_PDI_GetFile";
            public const string WebAPI_Service_ServiceHistory_Read = "WebAPI_Service_ServiceHistory_Read";
            public const string WebAPI_Service_ServiceTemplateActivity_Read = "WebAPI_Service_ServiceTemplateActivity_Read";
            public const string WebAPI_Service_ServiceIncoming_Create = "WebAPI_Service_ServiceIncoming_Create";
            public const string WebAPI_Service_ServiceIncoming_Update = "WebAPI_Service_ServiceIncoming_Update";
            public const string WebAPI_Service_WorkOrderPM_Create = "WebAPI_Service_WorkOrderPM_Create";
            public const string WebAPI_Service_WorkOrderPM_Delete = "WebAPI_Service_WorkOrderPM_Delete";
            public const string WebAPI_Service_MSPClaim_Create = "WebAPI_Service_MSPClaim_Create";
            public const string WebAPI_Service_MSPClaim_Update = "WebAPI_Service_MSPClaim_Update";
            public const string WebAPI_Service_MSPClaim_Read = "WebAPI_Service_MSPClaim_Read";
            public const string WebAPI_Service_ServiceValidator_Validate = "WebAPI_Service_ServiceValidator_Validate";
            public const string WebAPI_Service_ServiceHistoryWithDetail_Read = "WebAPI_Service_ServiceHistoryWithDetail_Read";
            public const string WebAPI_Service_DMSWOWarranty_Delete = "WebAPI_Service_DMSWOWarranty_Delete";
            public const string WebAPI_Service_ServiceReminder_Read = "WebAPI_Service_ServiceReminder_Read";
            public const string WebAPI_Service_ServiceReminder_FollowUp = "WebAPI_Service_ServiceReminder_FollowUp";

            public const string WebAPI_Service_MobileServiceReminder_Read = "WebAPI_Service_MobileServiceReminder_Read";
            
            public const string WebAPI_Service_ServiceIncomingBP_Create = "WebAPI_Service_ServiceIncomingBP_Create";
            public const string WebAPI_Service_ServiceIncomingBP_Update = "WebAPI_Service_ServiceIncomingBP_Update";
            public const string WebAPI_Service_ServiceIncomingBP_Delete = "WebAPI_Service_ServiceIncomingBP_Delete";
            
            public const string WebAPI_Service_StallMaster_Read = "WebAPI_Service_StallMaster_Read";
            public const string WebAPI_Service_StallMaster_Create = "WebAPI_Service_StallMaster_Create";
            public const string WebAPI_Service_StallMaster_Update = "WebAPI_Service_StallMaster_Update";
            public const string WebAPI_Service_StallMaster_Delete = "WebAPI_Service_StallMaster_Delete";
            public const string WebAPI_Service_ServiceTemplate_Read = "WebAPI_Service_ServiceTemplate_Read";
            public const string WebAPI_Service_ServiceBooking_Create = "WebAPI_Service_ServiceBooking_Create";
            public const string WebAPI_Service_ServiceBooking_Update = "WebAPI_Service_ServiceBooking_Update";
            public const string WebAPI_Service_ServiceBooking_Realtime_EstimationCost = "WebAPI_Service_ServiceBooking_Realtime_EstimationCost";
            public const string WebAPI_Service_ServiceBooking_Realtime_DealerSuggestion = "WebAPI_Service_ServiceBooking_Realtime_DealerSuggestion";
            public const string WebAPI_Service_ServiceBooking_Realtime_GetServiceType = "WebAPI_Service_ServiceBooking_Realtime_GetServiceType";
            public const string WebAPI_Service_ServiceBooking_Realtime_ServiceRecommendation = "WebAPI_Service_ServiceBooking_Realtime_ServiceRecommendation";
            public const string WebAPI_Service_ServiceBooking_Realtime_Read = "WebAPI_Service_ServiceBooking_Realtime_Read";
            public const string WebAPI_Service_ServiceBooking_Realtime_Create = "WebAPI_Service_ServiceBooking_Realtime_Create";
            public const string WebAPI_Service_ServiceBooking_Realtime_Update = "WebAPI_Service_ServiceBooking_Realtime_Update";
            public const string WebAPI_Service_ServiceStandardTime_Read = "WebAPI_Service_ServiceStandardTime_Read";

            public const string WebAPI_Service_StallWorkingTime_Create = "WebAPI_Service_StallWorkingTime_Create";
            public const string WebAPI_Service_StallWorkingTime_Update = "WebAPI_Service_StallWorkingTime_Update";

            public const string WebAPI_Service_WarrantyActivation_Read = "WebAPI_Service_WarrantyActivation_Read";
            public const string WebAPI_Service_WarrantyActivation_Create = "WebAPI_Service_WarrantyActivation_Create";
            public const string WebAPI_Service_WarrantyActivation_GetFile = "WebAPI_Service_WarrantyActivation_GetFile";

            public const string WebAPI_Service_PDIExpired_Read = "WebAPI_Service_PDIExpired_Read";

            public const string WebAPI_Service_ServiceMMS_Create = "WebAPI_Service_ServiceMMS_Create";
            public const string WebAPI_Service_ServiceMMS_Update = "WebAPI_Service_ServiceMMS_Update";
            public const string WebAPI_Service_ServiceMMS_Delete = "WebAPI_Service_ServiceMMS_Delete";

            public const string WebAPI_Service_StallWorkingTime_CreateList = "WebAPI_Service_StallWorkingTime_CreateList";
            public const string WebAPI_Service_StallWorkingTime_UpdateList = "WebAPI_Service_StallWorkingTime_UpdateList";

            public const string WebAPI_Service_ServiceBooking_RealtimeAll_Read = "WebAPI_Service_ServiceBooking_RealtimeAll_Read";
            #endregion

            #region SparePart
            public const string WebAPI_SparePart_APPayment_Create = "WebAPI_SparePart_APPayment_Create";
            public const string WebAPI_SparePart_ARReceipt_Create = "WebAPI_SparePart_ARReceipt_Create";
            public const string WebAPI_SparePart_AssistPartSales_Create = "WebAPI_SparePart_AssistPartSales_Create";
            public const string WebAPI_SparePart_AssistPartSales_CreateList = "WebAPI_SparePart_AssistPartSales_CreateList";
            public const string WebAPI_SparePart_AssistPartSales_Read = "WebAPI_SparePart_AssistPartSales_Read";
            public const string WebAPI_SparePart_AssistPartStock_Create = "WebAPI_SparePart_AssistPartStock_Create";
            public const string WebAPI_SparePart_AssistPartStock_CreateList = "WebAPI_SparePart_AssistPartStock_CreateList";
            public const string WebAPI_SparePart_AssistPartStock_Update = "WebAPI_SparePart_AssistPartStock_Update";
            public const string WebAPI_SparePart_AssistPartStock_UpdateList = "WebAPI_SparePart_AssistPartStock_UpdateList";
            public const string WebAPI_SparePart_DeliveryOrder_Create = "WebAPI_SparePart_DeliveryOrder_Create";
            public const string WebAPI_SparePart_EstimationEquipHeader_Create = "WebAPI_SparePart_EstimationEquipHeader_Create";
            public const string WebAPI_SparePart_IndentPart_Create = "WebAPI_SparePart_IndentPart_Create";
            public const string WebAPI_SparePart_InventoryTransaction_Create = "WebAPI_SparePart_InventoryTransaction_Create";
            public const string WebAPI_SparePart_InventoryTransfer_Create = "WebAPI_SparePart_InventoryTransfer_Create";
            public const string WebAPI_SparePart_PaymentDeposit_Read = "WebAPI_SparePart_PaymentDeposit_Read";
            public const string WebAPI_SparePart_POOtherVendor_Create = "WebAPI_SparePart_POOtherVendor_Create";
            public const string WebAPI_SparePart_PRHistoryDO_Read = "WebAPI_SparePart_PRHistoryDO_Read";
            public const string WebAPI_SparePart_PRHistoryIndentStatusCancel_Read = "WebAPI_SparePart_PRHistoryIndentStatusCancel_Read";
            public const string WebAPI_SparePart_PRHistoryPOStatusCancel_Read = "WebAPI_SparePart_PRHistoryPOStatusCancel_Read";
            public const string WebAPI_SparePart_PRHistorySO_Read = "WebAPI_SparePart_PRHistorySO_Read";
            public const string WebAPI_SparePart_PurchaseReceipt_Read = "WebAPI_SparePart_PurchaseReceipt_Read";
            public const string WebAPI_SparePart_PurchaseReceipt_Update = "WebAPI_SparePart_PurchaseReceipt_Update";
            public const string WebAPI_SparePart_SparePartPayment_Read = "WebAPI_SparePart_SparePartPayment_Read";
            public const string WebAPI_SparePart_SparePartPO_Create = "WebAPI_SparePart_SparePartPO_Create";
            public const string WebAPI_SparePart_SparePartPO_Update = "WebAPI_SparePart_SparePartPO_Update";
            public const string WebAPI_SparePart_SparePartPOEstimate_Read = "WebAPI_SparePart_SparePartPOEstimate_Read";
            public const string WebAPI_SparePart_SparePartPOOther_Read = "WebAPI_SparePart_SparePartPOOther_Read";
            public const string WebAPI_SparePart_SparePartPRFromOtherVendor_Create = "WebAPI_SparePart_SparePartPRFromOtherVendor_Create";
            public const string WebAPI_SparePart_SparePartSalesOrder_Create = "WebAPI_SparePart_SparePartSalesOrder_Create";
            public const string WebAPI_SparePart_PurchaseReturn_Read = "WebAPI_SparePart_PurchaseReturn_Read";
            public const string WebAPI_SparePartOutstandingOrder_Read = "WebAPI_SparePartOutstandingOrder_Read";
            public const string WebAPI_SparePartOutstandingOrderDetail_Read = "WebAPI_SparePartOutstandingOrderDetail_Read";

            public const string WebAPI_SparePart_SparePartPenalty_Read = "WebAPI_SparePart_SparePartPenalty_Read";
            public const string WebAPI_PQR_Read = "WebAPI_PQR_Read";

            public const string WebAPI_SparePart_SparePartForecast_Create = "WebAPI_SparePart_SparePartForecast_Create";
            public const string WebAPI_SparePart_SparePartForecast_StockManagement_Read = "WebAPI_SparePart_SparePartForecast_StockManagement_Read";
            public const string WebAPI_SparePart_SparePartForecast_Reject_Read = "WebAPI_SparePart_SparePartForecast_Reject_Read";
            public const string WebAPI_SparePart_SparePartForecast_POEstimate_Read = "WebAPI_SparePart_SparePartForecast_POEstimate_Read";
            public const string WebAPI_SparePart_SparePartForecast_GoodReceipt_Read = "WebAPI_SparePart_SparePartForecast_GoodReceipt_Read";
            public const string WebAPI_SparePart_SparePartForecast_Validator = "WebAPI_SparePart_SparePartForecast_Validator";

            public const string WebAPI_SparePart_Claim_Read = "WebAPI_SparePart_Claim_Read";
            public const string WebAPI_SparePart_Claim_Create = "WebAPI_SparePart_Claim_Create";
            #endregion

            #region StandardCode
            //public const string WebAPI_StandardCode_Create = "WebAPI_StandardCode_Create";
            //public const string WebAPI_StandardCode_Delete = "WebAPI_StandardCode_Delete";
            public const string WebAPI_StandardCode_Read = "WebAPI_StandardCode_Read";
            //public const string WebAPI_StandardCode_Update = "WebAPI_StandardCode_Update";
            //public const string WebAPI_StandardCodeChar_Create = "WebAPI_StandardCodeChar_Create";
            //public const string WebAPI_StandardCodeChar_Delete = "WebAPI_StandardCodeChar_Delete";
            public const string WebAPI_StandardCodeChar_Read = "WebAPI_StandardCodeChar_Read";
            //public const string WebAPI_StandardCodeChar_Update = "WebAPI_StandardCodeChar_Update";
            #endregion

            #region Vehicle Purchase
            public const string WebAPI_VehiclePurchase_Carrosserie_Create = "WebAPI_VehiclePurchase_Carrosserie_Create";
            public const string WebAPI_VehiclePurchase_PODealer_Read = "WebAPI_VehiclePurchase_PODealer_Read";
            public const string WebAPI_VehiclePurchase_POReceiptDealer_Read = "WebAPI_VehiclePurchase_POReceiptDealer_Read";
            public const string WebAPI_VehiclePurchase_VehiclePurchase_Create = "WebAPI_VehiclePurchase_VehiclePurchase_Create";
            public const string WebAPI_VehiclePurchase_ChassisMasterATA_Update = "WebAPI_VehiclePurchase_ChassisMasterATA_Update";
            public const string WebAPI_VehiclePurchase_ChassisMasterClaim_Create = "WebAPI_VehiclePurchase_ChassisMasterClaim_Create";
            public const string WebAPI_VehiclePurchase_ChassisMasterClaim_Read = "WebAPI_VehiclePurchase_ChassisMasterClaim_Read";
            public const string WebAPI_VehiclePurchase_ChassisMasterClaim_Update = "WebAPI_VehiclePurchase_ChassisMasterClaim_Update";
            #endregion

            #region Vehicle Sales
            public const string WebAPI_VehicleSales_CampaignReport_Read = "WebAPI_VehicleSales_CampaignReport_Read";
            public const string WebAPI_VehicleSales_ChassisMasterPKT_Read = "WebAPI_VehicleSales_ChassisMasterPKT_Read";
            public const string WebAPI_VehicleSales_ChassisMasterPKT_Create = "WebAPI_VehicleSales_ChassisMasterPKT_Create";
            public const string WebAPI_VehicleSales_ChassisMasterPKT_Update = "WebAPI_VehicleSales_ChassisMasterPKT_Update";
            public const string WebAPI_VehicleSales_CompleteOrCanceledSPKHeader_Read = "WebAPI_VehicleSales_CompleteOrCanceledSPKHeader_Read";
            public const string WebAPI_VehicleSales_ChassisStatusFaktur_Read = "WebAPI_VehicleSales_ChassisStatusFaktur_Read";
            public const string WebAPI_VehicleSales_ChassisStatusFakturList_Read = "WebAPI_VehicleSales_ChassisStatusFakturList_Read";
            public const string WebAPI_VehicleSales_ChassisStatusFaktur_InvoiceRevision_Read = "WebAPI_VehicleSales_ChassisStatusFaktur_InvoiceRevision_Read";
            public const string WebAPI_VehicleSales_Karoseri_Read = "WebAPI_VehicleSales_Karoseri_Read";
            public const string WebAPI_VehicleSales_Lead_Create = "WebAPI_VehicleSales_Lead_Create";
            public const string WebAPI_VehicleSales_Lead_Update = "WebAPI_VehicleSales_Lead_Update";
            public const string WebAPI_VehicleSales_LeadCustomerSalesForce_Read = "WebAPI_VehicleSales_LeadCustomerSalesForce_Read";
            public const string WebAPI_VehicleSales_Leasing_Read = "WebAPI_VehicleSales_Leasing_Read";
            public const string WebAPI_VehicleSales_OpenFaktur_Read = "WebAPI_VehicleSales_OpenFaktur_Read";
            public const string WebAPI_VehicleSales_OpenFakturForPDI_Read = "WebAPI_VehicleSales_OpenFakturForPDI_Read";
            public const string WebAPI_VehicleSales_SPK_Create = "WebAPI_VehicleSales_SPK_Create";
            public const string WebAPI_VehicleSales_SPK_KTP_Read = "WebAPI_VehicleSales_SPK_KTP_Read";
            public const string WebAPI_VehicleSales_SPK_Update = "WebAPI_VehicleSales_SPK_Update";
            public const string WebAPI_VehicleSales_SPKChassis_Create = "WebAPI_VehicleSales_SPKChassis_Create";
            public const string WebAPI_VehicleSales_SPKChassis_ValidateChassis = "WebAPI_VehicleSales_SPKChassis_ValidateChassis";
            public const string WebAPI_VehicleSales_SPKDocument_Read = "WebAPI_VehicleSales_SPKDocument_Read";
            public const string WebAPI_VehicleSales_UnmatchSPKChassis_Read = "WebAPI_VehicleSales_UnmatchSPKChassis_Read";
            public const string WebAPI_VehicleSales_SPK_CustomerHaveRequest_Read = "WebAPI_VehicleSales_SPK_CustomerHaveRequest_Read";
            public const string WebAPI_VehicleSales_SPK_Read = "WebAPI_VehicleSales_SPK_Read";
            public const string WebAPI_SPKMasterCountryCodePhone_Read = "WebAPI_SPKMasterCountryCodePhone_Read";
            #endregion

            #region OCR Identity
            public const string WebAPI_OCR_DataKTP = "WebAPI_OCR_DataKTP";
            public const string WebAPI_OCR_DataSIM = "WebAPI_OCR_DataSIM";
            public const string WebAPI_OCR_ProgressKTP = "WebAPI_OCR_ProgressKTP";
            public const string WebAPI_OCR_ProgressSIM = "WebAPI_OCR_ProgressSIM";
            public const string WebAPI_OCR_UploadKTP = "WebAPI_OCR_UploadKTP";
            public const string WebAPI_OCR_UploadSIM = "WebAPI_OCR_UploadSIM";
            #endregion

            #region WebUI Permission
            public const string WebUI_App_Access = "WebUI_App_Access";

            // User
            public const string WebUI_User_Create = "WebUI_User_Create";
            public const string WebUI_User_Delete = "WebUI_User_Delete";
            public const string WebUI_User_Read = "WebUI_User_Read";
            public const string WebUI_User_Update = "WebUI_User_Update";
            public const string WebUI_User_Upload = "WebUI_User_Upload";
            public const string WebUI_User_UploadTemplate = "WebUI_User_UploadTemplate";

            // Permission
            public const string WebUI_Permission_Create = "WebUI_Permission_Create";
            public const string WebUI_Permission_Delete = "WebUI_Permission_Delete";
            public const string WebUI_Permission_Read = "WebUI_Permission_Read";
            public const string WebUI_Permission_Update = "WebUI_Permission_Update";

            // Client Role Pemission
            public const string WebUI_ClientRolePermission_Create = "WebUI_ClientRolePermission_Create";
            public const string WebUI_ClientRolePermission_Delete = "WebUI_ClientRolePermission_Delete";
            public const string WebUI_ClientRolePermission_Read = "WebUI_ClientRolePermission_Read";
            public const string WebUI_ClientRolePermission_Update = "WebUI_ClientRolePermission_Update";

            // Role
            public const string WebUI_Role_Create = "WebUI_Role_Create";
            public const string WebUI_Role_Delete = "WebUI_Role_Delete";
            public const string WebUI_Role_Read = "WebUI_Role_Read";
            public const string WebUI_Role_Update = "WebUI_Role_Update";

            // Client
            public const string WebUI_Client_Create = "WebUI_Client_Create";
            public const string WebUI_Client_Delete = "WebUI_Client_Delete";
            public const string WebUI_Client_Read = "WebUI_Client_Read";
            public const string WebUI_Client_Update = "WebUI_Client_Update";

            // TransactionLog
            public const string WebUI_TransactionDetail_Read = "WebUI_TransactionDetail_Read";
            public const string WebUI_TransactionLog_Delete = "WebUI_TransactionLog_Delete";
            public const string WebUI_TransactionLog_Read = "WebUI_TransactionLog_Read";
            public const string WebUI_FailedTransactionDetail_Read = "WebUI_FailedTransactionDetail_Read";
            public const string WebUI_FailedTransactionLog_Read = "WebUI_FailedTransactionLog_Read";
            public const string WebUI_FailedTransactionLog_Resend = "WebUI_FailedTransactionLog_Resend";

            // Chart
            public const string WebUI_Chart_Read = "WebUI_Chart_Read";

            // Application Config
            public const string WebUI_AppConfig_Create = "WebUI_AppConfig_Create";
            public const string WebUI_AppConfig_Delete = "WebUI_AppConfig_Delete";
            public const string WebUI_AppConfig_Read = "WebUI_AppConfig_Read";
            public const string WebUI_AppConfig_Update = "WebUI_AppConfig_Update";

            // Schedule
            public const string WebUI_Schedule_Create = "WebUI_Schedule_Create";
            public const string WebUI_Schedule_Delete = "WebUI_Schedule_Delete";
            public const string WebUI_Schedule_Read = "WebUI_Schedule_Read";
            public const string WebUI_Schedule_Update = "WebUI_Schedule_Update";

            // EndpointSchedule
            public const string WebUI_EndpointSchedule_Create = "WebUI_EndpointSchedule_Create";
            public const string WebUI_EndpointSchedule_Delete = "WebUI_EndpointSchedule_Delete";
            public const string WebUI_EndpointSchedule_Read = "WebUI_EndpointSchedule_Read";
            public const string WebUI_EndpointSchedule_Update = "WebUI_EndpointSchedule_Update";

            //ThreadLog
            public const string WebUI_TransactionRuntime_Read = "WebUI_TransactionRuntime_Read";

            // UserActivity
            public const string WebUI_Activity_Read = "WebUI_Activity_Read";

            // Throttle
            public const string WebUI_Throttle_Create = "WebUI_Throttle_Create";
            public const string WebUI_Throttle_Delete = "WebUI_Throttle_Delete";
            public const string WebUI_Throttle_Export = "WebUI_Throttle_Export";
            public const string WebUI_Throttle_Read = "WebUI_Throttle_Read";
            public const string WebUI_Throttle_Update = "WebUI_Throttle_Update";

            // Deployment
            public const string WebUI_JenkinsJob_Deploy = "WebUI_JenkinsJob_Deploy";
            public const string WebUI_JenkinsJob_Read = "WebUI_JenkinsJob_Read";
            public const string WebUI_JenkinsJob_ViewOutput = "WebUI_JenkinsJob_ViewOutput";

            // MsApplication
            public const string WebUI_MsApplication_Read = "WebUI_MsApplication_Read";
            public const string WebUI_MsApplication_Create = "WebUI_MsApplication_Create";
            public const string WebUI_MsApplication_Update = "WebUI_MsApplication_Update";
            public const string WebUI_MsApplication_Delete = "WebUI_MsApplication_Delete";

            // MsAppVersion
            public const string WebUI_MsAppVersion_Read = "WebUI_MsAppVersion_Read";
            public const string WebUI_MsAppVersion_Create = "WebUI_MsAppVersion_Create";
            public const string WebUI_MsAppVersion_Update = "WebUI_MsAppVersion_Update";
            public const string WebUI_MsAppVersion_Delete = "WebUI_MsAppVersion_Delete";

            // ErrorLog
            public const string WebUI_ErrorLog_Read = "WebUI_ErrorLog_Read";
            public const string WebUI_Log_Delete = "WebUI_Log_Delete";

            // Standard Code
            public const string WebUI_StandardCode_Read = "WebUI_StandardCode_Read";
            public const string WebUI_StandardCode_Create = "WebUI_StandardCode_Create";
            public const string WebUI_StandardCode_Update = "WebUI_StandardCode_Update";
            public const string WebUI_StandardCode_Delete = "WebUI_StandardCode_Delete";

            // Standard Code Char
            public const string WebUI_StandardCodeChar_Read = "WebUI_StandardCodeChar_Read";
            public const string WebUI_StandardCodeChar_Create = "WebUI_StandardCodeChar_Create";
            public const string WebUI_StandardCodeChar_Update = "WebUI_StandardCodeChar_Update";
            public const string WebUI_StandardCodeChar_Delete = "WebUI_StandardCodeChar_Delete";
            #endregion

            #region AccountingReport
            public const string WebAPI_VWI_CRM_VehicleInvoice_Read = "WebAPI_VWI_CRM_VehicleInvoice_Read";
            public const string WebAPI_VWI_CRM_SOInvoice_Read = "WebAPI_VWI_CRM_SOInvoice_Read";
            public const string WebAPI_VWI_CRM_WOInvoice_Read = "WebAPI_VWI_CRM_WOInvoice_Read";
            public const string WebAPI_VWI_CRM_SVC_DailyReport_Read = "WebAPI_VWI_CRM_SVC_DailyReport_Read";
            public const string WebAPI_VWI_AX_SLS_BankTransaction_Read = "WebAPI_VWI_AX_SLS_BankTransaction_Read";
            public const string WebAPI_VWI_AX_PRT_StockMovement_Read = "WebAPI_VWI_AX_PRT_StockMovement_Read";
            public const string WebAPI_VWI_AX_PRT_FlowReportPart_Read = "WebAPI_VWI_AX_PRT_FlowReportPart_Read";
            public const string WebAPI_VWI_AX_SLS_LedgerTransaction_Read = "WebAPI_VWI_AX_SLS_LedgerTransaction_Read";
            public const string WebAPI_VWI_CRM_PRT_IncomingInventoryTransfer_Read = "WebAPI_VWI_CRM_PRT_IncomingInventoryTransfer_Read";
            public const string WebAPI_VWI_CRM_PRT_IncomingTransaction_Read = "WebAPI_VWI_CRM_PRT_IncomingTransaction_Read";
            public const string WebAPI_VWI_CRM_PRT_OutgoingInventoryTransfer_Read = "WebAPI_VWI_CRM_PRT_OutgoingInventoryTransfer_Read";
            public const string WebAPI_VWI_CRM_PRT_OutgoingTransaction_Read = "WebAPI_VWI_CRM_PRT_OutgoingTransaction_Read";
            public const string WebAPI_VWI_CRM_PRT_SparepartPurchase_Read = "WebAPI_VWI_CRM_PRT_SparepartPurchase_Read";
            public const string WebAPI_VWI_CRM_PRT_SparepartSales_Read = "WebAPI_VWI_CRM_PRT_SparepartSales_Read";
            public const string WebAPI_VWI_CRM_PRT_SparepartSalesReturn_Read = "WebAPI_VWI_CRM_PRT_SparepartSalesReturn_Read";
            public const string WebAPI_VWI_CRM_PRT_SparepartSalesToPartshop_Read = "WebAPI_VWI_CRM_PRT_SparepartSalesToPartshop_Read";
            public const string WebAPI_VWI_CRM_SLS_APBalance_Read = "WebAPI_VWI_CRM_SLS_APBalance_Read";
            public const string WebAPI_VWI_CRM_SLS_ARBalance_Read = "WebAPI_VWI_CRM_SLS_ARBalance_Read";
            public const string WebAPI_VWI_CRM_SLS_SalesUnit_Read = "WebAPI_VWI_CRM_SLS_SalesUnit_Read";
            public const string WebAPI_VWI_CRM_SLS_StockMutation_Read = "WebAPI_VWI_CRM_SLS_StockMutation_Read";
            public const string WebAPI_VWI_CRM_SLS_StockSummary_Read = "WebAPI_VWI_CRM_SLS_StockSummary_Read";
            public const string WebAPI_VWI_CRM_SLS_VehicleStock_Read = "WebAPI_VWI_CRM_SLS_VehicleStock_Read";
            public const string WebAPI_VWI_CRM_SVC_ARReceipt_Read = "WebAPI_VWI_CRM_SVC_ARReceipt_Read";
            public const string WebAPI_VWI_CRM_SVC_ARReceiptDetailBasedOnWO_Read = "WebAPI_VWI_CRM_SVC_ARReceiptDetailBasedOnWO_Read";
            public const string WebAPI_VWI_CRM_SVC_FreeServiceToBeInvoiced_Read = "WebAPI_VWI_CRM_SVC_FreeServiceToBeInvoiced_Read";
            public const string WebAPI_VWI_CRM_PRT_PurchaseReturn_Read = "WebAPI_VWI_CRM_PRT_PurchaseReturn_Read";
            public const string WebAPI_VWI_CRM_SLS_DailyActivityMonitoring_Lead_Read = "WebAPI_VWI_CRM_SLS_DailyActivityMonitoring_Lead_Read";
            public const string WebAPI_VWI_CRM_SLS_DailyActivityMonitoring_SPK_Read = "WebAPI_VWI_CRM_SLS_DailyActivityMonitoring_SPK_Read";
            public const string WebAPI_VWI_CRM_SLS_DailyActivityMonitoring_VDO_Read = "WebAPI_VWI_CRM_SLS_DailyActivityMonitoring_VDO_Read";
            public const string WebAPI_VWI_CRM_SLS_SalesmanActivity_Read = "WebAPI_VWI_CRM_SLS_SalesmanActivity_Read";
            public const string WebAPI_VWI_CRM_PRT_Purchase_Read = "WebAPI_VWI_CRM_PRT_Purchase_Read";
            public const string WebAPI_VWI_CRM_PRT_IncomingInventoryTransferWarehouse_Read = "WebAPI_VWI_CRM_PRT_IncomingInventoryTransferWarehouse_Read";
            public const string WebAPI_VWI_CRM_PRT_OutgoingInventoryTransferWarehouse_Read = "WebAPI_VWI_CRM_PRT_OutgoingInventoryTransferWarehouse_Read";
            public const string WebAPI_VWI_AX_SLS_StockMutation_Read = "WebAPI_VWI_AX_SLS_StockMutation_Read";
            public const string WebAPI_VWI_CRM_PRT_IncomingInventoryTransferOutlet_Read = "WebAPI_VWI_CRM_PRT_IncomingInventoryTransferOutlet_Read";
            public const string WebAPI_VWI_CRM_PRT_OutgoingInventoryTransferOutlet_Read = "WebAPI_VWI_CRM_PRT_OutgoingInventoryTransferOutlet_Read";
            public const string WebAPI_VWI_AX_SLS_FlowReportVehicle_Read = "WebAPI_VWI_AX_SLS_FlowReportVehicle_Read";

            #endregion

            #region AccountingData
            public const string WebAPI_AX_TSTransStockMutations_Read = "WebAPI_AX_TSTransStockMutations_Read";
            public const string WebAPI_VWI_CRM_xts_inventtrans_Read = "WebAPI_VWI_CRM_xts_inventtrans_Read";
            public const string WebAPI_CRM_ktb_openfacture_Read = "WebAPI_CRM_ktb_openfacture_Read";
            public const string WebAPI_VWI_CRM_xts_globalworkorderhistory_Read = "WebAPI_VWI_CRM_xts_globalworkorderhistory_Read";
            public const string WebAPI_VWI_CRM_xts_globalworkorderhistorydetail_Read = "WebAPI_VWI_CRM_xts_globalworkorderhistorydetail_Read";
            public const string WebAPI_VWI_CRM_xts_outsourceworkorderreceiptdetail_Read = "WebAPI_VWI_CRM_xts_outsourceworkorderreceiptdetail_Read";
            public const string WebAPI_VWI_CRM_BusinessUnit_Read = "WebAPI_VWI_CRM_BusinessUnit_Read";
            public const string WebAPI_VWI_CRM_account_Read = "WebAPI_VWI_CRM_account_Read";
            public const string WebAPI_VWI_CRM_xjp_vehiclecostinputdetail_Read = "WebAPI_VWI_CRM_xjp_vehiclecostinputdetail_Read";
            public const string WebAPI_VWI_CRM_campaign_Read = "WebAPI_VWI_CRM_campaign_Read";
            public const string WebAPI_VWI_CRM_equipment_Read = "WebAPI_VWI_CRM_equipment_Read";
            public const string WebAPI_VWI_CRM_pricelevel_Read = "WebAPI_VWI_CRM_pricelevel_Read";
            public const string WebAPI_VWI_CRM_appointment_Read = "WebAPI_VWI_CRM_appointment_Read";
            public const string WebAPI_VWI_CRM_xts_ratetype_Read = "WebAPI_VWI_CRM_xts_ratetype_Read";
            public const string WebAPI_VWI_CRM_systemuser_Read = "WebAPI_VWI_CRM_systemuser_Read";
            public const string WebAPI_VWI_CRM_xts_servicereceipt_Read = "WebAPI_VWI_CRM_xts_servicereceipt_Read";
            public const string WebAPI_VWI_CRM_xts_usedvehicledeliveryorder_Read = "WebAPI_VWI_CRM_xts_usedvehicledeliveryorder_Read";
            public const string WebAPI_VWI_CRM_xts_partsforecast_Read = "WebAPI_VWI_CRM_xts_partsforecast_Read";
            public const string WebAPI_VWI_CRM_serviceappointment_Read = "WebAPI_VWI_CRM_serviceappointment_Read";
            public const string WebAPI_VWI_CRM_xts_usedvehiclesalesorder_Read = "WebAPI_VWI_CRM_xts_usedvehiclesalesorder_Read";
            public const string WebAPI_VWI_CRM_xts_accessorieskitgroup_Read = "WebAPI_VWI_CRM_xts_accessorieskitgroup_Read";
            public const string WebAPI_VWI_CRM_xts_productvariant_Read = "WebAPI_VWI_CRM_xts_productvariant_Read";
            public const string WebAPI_VWI_CRM_xts_productinteriorcolor_Read = "WebAPI_VWI_CRM_xts_productinteriorcolor_Read";
            public const string WebAPI_VWI_CRM_xts_accessoriesinstallationcategory_Read = "WebAPI_VWI_CRM_xts_accessoriesinstallationcategory_Read";
            public const string WebAPI_VWI_CRM_xts_kit_Read = "WebAPI_VWI_CRM_xts_kit_Read";
            public const string WebAPI_VWI_CRM_ktb_benefit_Read = "WebAPI_VWI_CRM_ktb_benefit_Read";
            public const string WebAPI_VWI_CRM_ktb_karoseri_Read = "WebAPI_VWI_CRM_ktb_karoseri_Read";
            public const string WebAPI_VWI_CRM_xts_subsidyanddiscount_Read = "WebAPI_VWI_CRM_xts_subsidyanddiscount_Read";
            public const string WebAPI_VWI_CRM_xjp_automobiletax_Read = "WebAPI_VWI_CRM_xjp_automobiletax_Read";
            public const string WebAPI_VWI_CRM_invalidcustomer_Read = "WebAPI_VWI_CRM_invalidcustomer_Read";
            public const string WebAPI_VWI_CRM_ktb_daftardeposita_Read = "WebAPI_VWI_CRM_ktb_daftardeposita_Read";
            public const string WebAPI_VWI_CRM_ktb_daftardepositb_Read = "WebAPI_VWI_CRM_ktb_daftardepositb_Read";
            public const string WebAPI_VWI_CRM_ktb_daftardepositc_Read = "WebAPI_VWI_CRM_ktb_daftardepositc_Read";
            public const string WebAPI_VWI_CRM_ktb_depositc2_Read = "WebAPI_VWI_CRM_ktb_depositc2_Read";
            public const string WebAPI_VWI_CRM_ktb_daftardepositadetail_Read = "WebAPI_VWI_CRM_ktb_daftardepositadetail_Read";
            public const string WebAPI_VWI_CRM_ktb_daftardepositbdetail_Read = "WebAPI_VWI_CRM_ktb_daftardepositbdetail_Read";
            public const string WebAPI_VWI_CRM_ktb_daftardepositcdetail_Read = "WebAPI_VWI_CRM_ktb_daftardepositcdetail_Read";

            public const string WebAPI_VWI_CRM_bookingstatus_Read = "WebAPI_VWI_CRM_bookingstatus_Read";
            public const string WebAPI_VWI_CRM_bookableresourcebooking_Read = "WebAPI_VWI_CRM_bookableresourcebooking_Read";
            public const string WebAPI_VWI_CRM_campaignactivity_Read = "WebAPI_VWI_CRM_campaignactivity_Read";
            public const string WebAPI_VWI_CRM_campaignresponse_Read = "WebAPI_VWI_CRM_campaignresponse_Read";
            public const string WebAPI_VWI_CRM_contact_Read = "WebAPI_VWI_CRM_contact_Read";
            public const string WebAPI_VWI_CRM_customworkorder_Read = "WebAPI_VWI_CRM_customworkorder_Read";
            public const string WebAPI_VWI_CRM_ktb_daerahlogistic_Read = "WebAPI_VWI_CRM_ktb_daerahlogistic_Read";
            public const string WebAPI_VWI_CRM_ktb_externaldealerinterfacelog_Read = "WebAPI_VWI_CRM_ktb_externaldealerinterfacelog_Read";
            public const string WebAPI_VWI_CRM_ktb_jobposition_Read = "WebAPI_VWI_CRM_ktb_jobposition_Read";
            public const string WebAPI_VWI_CRM_ktb_kontrabon_Read = "WebAPI_VWI_CRM_ktb_kontrabon_Read";
            public const string WebAPI_VWI_CRM_ktb_leasingcompany_Read = "WebAPI_VWI_CRM_ktb_leasingcompany_Read";
            public const string WebAPI_VWI_CRM_ktb_mastervehiclemodel_Read = "WebAPI_VWI_CRM_ktb_mastervehiclemodel_Read";
            public const string WebAPI_VWI_CRM_ktb_perlengkapanstandard_Read = "WebAPI_VWI_CRM_ktb_perlengkapanstandard_Read";
            public const string WebAPI_VWI_CRM_ktb_purchaserequisitionhistory_Read = "WebAPI_VWI_CRM_ktb_purchaserequisitionhistory_Read";
            public const string WebAPI_VWI_CRM_ktb_saleschannel_Read = "WebAPI_VWI_CRM_ktb_saleschannel_Read";
            public const string WebAPI_VWI_CRM_ktb_salesquotation_Read = "WebAPI_VWI_CRM_ktb_salesquotation_Read";
            public const string WebAPI_VWI_CRM_ktb_servicetemplateproductnongenuine_Read = "WebAPI_VWI_CRM_ktb_servicetemplateproductnongenuine_Read";
            public const string WebAPI_VWI_CRM_ktb_servicereminder_Read = "WebAPI_VWI_CRM_ktb_servicereminder_Read";
            public const string WebAPI_VWI_CRM_ktb_userareaassignment_Read = "WebAPI_VWI_CRM_ktb_userareaassignment_Read";
            public const string WebAPI_VWI_CRM_ktb_vendorlogistic_Read = "WebAPI_VWI_CRM_ktb_vendorlogistic_Read";
            public const string WebAPI_VWI_CRM_lead_Read = "WebAPI_VWI_CRM_lead_Read";
            public const string WebAPI_VWI_CRM_opportunity_Read = "WebAPI_VWI_CRM_opportunity_Read";
            public const string WebAPI_VWI_CRM_team_Read = "WebAPI_VWI_CRM_team_Read";
            public const string WebAPI_VWI_CRM_territory_Read = "WebAPI_VWI_CRM_territory_Read";
            public const string WebAPI_VWI_CRM_xid_documentregistration_Read = "WebAPI_VWI_CRM_xid_documentregistration_Read";
            public const string WebAPI_VWI_CRM_xid_registrationmonitoring_Read = "WebAPI_VWI_CRM_xid_registrationmonitoring_Read";
            public const string WebAPI_VWI_CRM_xid_registrationprogressstage_Read = "WebAPI_VWI_CRM_xid_registrationprogressstage_Read";
            public const string WebAPI_VWI_CRM_xjp_pdidetail_Read = "WebAPI_VWI_CRM_xjp_pdidetail_Read";
            public const string WebAPI_VWI_CRM_xjp_pdireceipt_Read = "WebAPI_VWI_CRM_xjp_pdireceipt_Read";
            public const string WebAPI_VWI_CRM_xjp_pdireceiptdetail_Read = "WebAPI_VWI_CRM_xjp_pdireceiptdetail_Read";
            public const string WebAPI_VWI_CRM_xjp_predeliveryinspection_Read = "WebAPI_VWI_CRM_xjp_predeliveryinspection_Read";
            public const string WebAPI_VWI_CRM_xjp_registrationcolor_Read = "WebAPI_VWI_CRM_xjp_registrationcolor_Read";
            public const string WebAPI_VWI_CRM_xjp_registrationdocument_Read = "WebAPI_VWI_CRM_xjp_registrationdocument_Read";
            public const string WebAPI_VWI_CRM_xjp_registrationrequest_Read = "WebAPI_VWI_CRM_xjp_registrationrequest_Read";
            public const string WebAPI_VWI_CRM_xjp_standardplate_Read = "WebAPI_VWI_CRM_xjp_standardplate_Read";
            public const string WebAPI_VWI_CRM_xjp_vehiclecostinput_Read = "WebAPI_VWI_CRM_xjp_vehiclecostinput_Read";
            public const string WebAPI_VWI_CRM_xjp_vehicletransfer_Read = "WebAPI_VWI_CRM_xjp_vehicletransfer_Read";
            public const string WebAPI_VWI_CRM_xjp_weighttax_Read = "WebAPI_VWI_CRM_xjp_weighttax_Read";
            public const string WebAPI_VWI_CRM_xts_accountpayablepayment_Read = "WebAPI_VWI_CRM_xts_accountpayablepayment_Read";
            public const string WebAPI_VWI_CRM_xts_accountpayablepaymentdetail_Read = "WebAPI_VWI_CRM_xts_accountpayablepaymentdetail_Read";
            public const string WebAPI_VWI_CRM_xts_accountpayablevoucher_Read = "WebAPI_VWI_CRM_xts_accountpayablevoucher_Read";
            public const string WebAPI_VWI_CRM_xts_accountreceivableinvoice_Read = "WebAPI_VWI_CRM_xts_accountreceivableinvoice_Read";
            public const string WebAPI_VWI_CRM_xts_accountreceivableinvoicedetail_Read = "WebAPI_VWI_CRM_xts_accountreceivableinvoicedetail_Read";
            public const string WebAPI_VWI_CRM_xts_accountreceivablereceipt_Read = "WebAPI_VWI_CRM_xts_accountreceivablereceipt_Read";
            public const string WebAPI_VWI_CRM_xts_accountreceivablereceiptdetail_Read = "WebAPI_VWI_CRM_xts_accountreceivablereceiptdetail_Read";
            public const string WebAPI_VWI_CRM_xts_accountreceivablereceiptotherexpense_Read = "WebAPI_VWI_CRM_xts_accountreceivablereceiptotherexpense_Read";
            public const string WebAPI_VWI_CRM_xts_aptransactiondocument_Read = "WebAPI_VWI_CRM_xts_aptransactiondocument_Read";
            public const string WebAPI_VWI_CRM_xts_aptransactiondocumentdetail_Read = "WebAPI_VWI_CRM_xts_aptransactiondocumentdetail_Read";
            public const string WebAPI_VWI_CRM_xts_apvdetail_Read = "WebAPI_VWI_CRM_xts_apvdetail_Read";
            public const string WebAPI_VWI_CRM_xts_apvlandedcost_Read = "WebAPI_VWI_CRM_xts_apvlandedcost_Read";
            public const string WebAPI_VWI_CRM_xts_apvmiscellaneouscharge_Read = "WebAPI_VWI_CRM_xts_apvmiscellaneouscharge_Read";
            public const string WebAPI_VWI_CRM_xts_artransactiondocument_Read = "WebAPI_VWI_CRM_xts_artransactiondocument_Read";
            public const string WebAPI_VWI_CRM_xts_artransactiondocumentdetail_Read = "WebAPI_VWI_CRM_xts_artransactiondocumentdetail_Read";
            public const string WebAPI_VWI_CRM_xts_assessment_Read = "WebAPI_VWI_CRM_xts_assessment_Read";
            public const string WebAPI_VWI_CRM_xts_assigntosalesperson_Read = "WebAPI_VWI_CRM_xts_assigntosalesperson_Read";
            public const string WebAPI_VWI_CRM_xts_bank_Read = "WebAPI_VWI_CRM_xts_bank_Read";
            public const string WebAPI_VWI_CRM_xts_businessunitinquiry_Read = "WebAPI_VWI_CRM_xts_businessunitinquiry_Read";
            public const string WebAPI_VWI_CRM_xts_cashandbank_Read = "WebAPI_VWI_CRM_xts_cashandbank_Read";
            public const string WebAPI_VWI_CRM_xts_cashandbankaccount_Read = "WebAPI_VWI_CRM_xts_cashandbankaccount_Read";
            public const string WebAPI_VWI_CRM_xts_cashtransaction_Read = "WebAPI_VWI_CRM_xts_cashtransaction_Read";
            public const string WebAPI_VWI_CRM_xts_cashtransactiondetail_Read = "WebAPI_VWI_CRM_xts_cashtransactiondetail_Read";
            public const string WebAPI_VWI_CRM_xts_chartofaccount_Read = "WebAPI_VWI_CRM_xts_chartofaccount_Read";
            public const string WebAPI_VWI_CRM_xts_city_Read = "WebAPI_VWI_CRM_xts_city_Read";
            public const string WebAPI_VWI_CRM_xts_common_Read = "WebAPI_VWI_CRM_xts_common_Read";
            public const string WebAPI_VWI_CRM_xts_commonbusinessunit_Read = "WebAPI_VWI_CRM_xts_commonbusinessunit_Read";
            public const string WebAPI_VWI_CRM_xts_consumptiontax_Read = "WebAPI_VWI_CRM_xts_consumptiontax_Read";
            public const string WebAPI_VWI_CRM_xts_customerclass_Read = "WebAPI_VWI_CRM_xts_customerclass_Read";
            public const string WebAPI_VWI_CRM_xts_customerpublic_Read = "WebAPI_VWI_CRM_xts_customerpublic_Read";
            public const string WebAPI_VWI_CRM_xts_damageorloss_Read = "WebAPI_VWI_CRM_xts_damageorloss_Read";
            public const string WebAPI_VWI_CRM_xts_deliveryorder_Read = "WebAPI_VWI_CRM_xts_deliveryorder_Read";
            public const string WebAPI_VWI_CRM_xts_deliveryorderdetail_Read = "WebAPI_VWI_CRM_xts_deliveryorderdetail_Read";
            public const string WebAPI_VWI_CRM_xts_deliveryordermiscellaneouscharge_Read = "WebAPI_VWI_CRM_xts_deliveryordermiscellaneouscharge_Read";
            public const string WebAPI_VWI_CRM_xts_department_Read = "WebAPI_VWI_CRM_xts_department_Read";
            public const string WebAPI_VWI_CRM_xts_dimension1_Read = "WebAPI_VWI_CRM_xts_dimension1_Read";
            public const string WebAPI_VWI_CRM_xts_dimension2_Read = "WebAPI_VWI_CRM_xts_dimension2_Read";
            public const string WebAPI_VWI_CRM_xts_dimension3_Read = "WebAPI_VWI_CRM_xts_dimension3_Read";
            public const string WebAPI_VWI_CRM_xts_dimension4_Read = "WebAPI_VWI_CRM_xts_dimension4_Read";
            public const string WebAPI_VWI_CRM_xts_dimension5_Read = "WebAPI_VWI_CRM_xts_dimension5_Read";
            public const string WebAPI_VWI_CRM_xts_dimension6_Read = "WebAPI_VWI_CRM_xts_dimension6_Read";
            public const string WebAPI_VWI_CRM_xts_dimension7_Read = "WebAPI_VWI_CRM_xts_dimension7_Read";
            public const string WebAPI_VWI_CRM_xts_dimension8_Read = "WebAPI_VWI_CRM_xts_dimension8_Read";
            public const string WebAPI_VWI_CRM_xts_dimension9_Read = "WebAPI_VWI_CRM_xts_dimension9_Read";
            public const string WebAPI_VWI_CRM_xts_dimension10_Read = "WebAPI_VWI_CRM_xts_dimension10_Read";
            public const string WebAPI_VWI_CRM_xts_discountsetup_Read = "WebAPI_VWI_CRM_xts_discountsetup_Read";
            public const string WebAPI_VWI_CRM_xts_discountsetupdetail_Read = "WebAPI_VWI_CRM_xts_discountsetupdetail_Read";
            public const string WebAPI_VWI_CRM_xts_employee_Read = "WebAPI_VWI_CRM_xts_employee_Read";
            public const string WebAPI_VWI_CRM_xts_employeeclass_Read = "WebAPI_VWI_CRM_xts_employeeclass_Read";
            public const string WebAPI_VWI_CRM_xts_generateinventorylist_Read = "WebAPI_VWI_CRM_xts_generateinventorylist_Read";
            public const string WebAPI_VWI_CRM_xts_goodsreceipt_Read = "WebAPI_VWI_CRM_xts_goodsreceipt_Read";
            public const string WebAPI_VWI_CRM_xts_gljournal_Read = "WebAPI_VWI_CRM_xts_gljournal_Read";
            public const string WebAPI_VWI_CRM_xts_gljournaldetails_Read = "WebAPI_VWI_CRM_xts_gljournaldetails_Read";
            public const string WebAPI_VWI_CRM_xts_grade_Read = "WebAPI_VWI_CRM_xts_grade_Read";
            public const string WebAPI_VWI_CRM_xts_incomingoutsourceworkorder_Read = "WebAPI_VWI_CRM_xts_incomingoutsourceworkorder_Read";
            public const string WebAPI_VWI_CRM_xts_incomingoutsourceworkorderdetail_Read = "WebAPI_VWI_CRM_xts_incomingoutsourceworkorderdetail_Read";
            public const string WebAPI_VWI_CRM_xts_incomingpdiandserviceinstruction_Read = "WebAPI_VWI_CRM_xts_incomingpdiandserviceinstruction_Read";
            public const string WebAPI_VWI_CRM_xts_inventbatch_Read = "WebAPI_VWI_CRM_xts_inventbatch_Read";
            public const string WebAPI_VWI_CRM_xts_inventorynewvehicle_Read = "WebAPI_VWI_CRM_xts_inventorynewvehicle_Read";
            public const string WebAPI_VWI_CRM_xts_inventorytransaction_Read = "WebAPI_VWI_CRM_xts_inventorytransaction_Read";
            public const string WebAPI_VWI_CRM_xts_inventorytransactiondetail_Read = "WebAPI_VWI_CRM_xts_inventorytransactiondetail_Read";
            public const string WebAPI_VWI_CRM_xts_inventorytransfer_Read = "WebAPI_VWI_CRM_xts_inventorytransfer_Read";
            public const string WebAPI_VWI_CRM_xts_inventorytransferdetail_Read = "WebAPI_VWI_CRM_xts_inventorytransferdetail_Read";
            public const string WebAPI_VWI_CRM_xts_inventserial_Read = "WebAPI_VWI_CRM_xts_inventserial_Read";
            public const string WebAPI_VWI_CRM_xts_landedcost_Read = "WebAPI_VWI_CRM_xts_landedcost_Read";
            public const string WebAPI_VWI_CRM_xts_location_Read = "WebAPI_VWI_CRM_xts_location_Read";
            public const string WebAPI_VWI_CRM_xts_manufacturer_Read = "WebAPI_VWI_CRM_xts_manufacturer_Read";
            public const string WebAPI_VWI_CRM_xts_matchunmatch_Read = "WebAPI_VWI_CRM_xts_matchunmatch_Read";
            public const string WebAPI_VWI_CRM_xts_miscellaneouscharge_Read = "WebAPI_VWI_CRM_xts_miscellaneouscharge_Read";
            public const string WebAPI_VWI_CRM_xts_miscellaneouschargetemplate_Read = "WebAPI_VWI_CRM_xts_miscellaneouschargetemplate_Read";
            public const string WebAPI_VWI_CRM_xts_moreaddress_Read = "WebAPI_VWI_CRM_xts_moreaddress_Read";
            public const string WebAPI_VWI_CRM_xts_newvehicledeliveryorder_Read = "WebAPI_VWI_CRM_xts_newvehicledeliveryorder_Read";
            public const string WebAPI_VWI_CRM_xts_newvehicleexteriorcolor_Read = "WebAPI_VWI_CRM_xts_newvehicleexteriorcolor_Read";
            public const string WebAPI_VWI_CRM_xts_newvehicleinteriorcolor_Read = "WebAPI_VWI_CRM_xts_newvehicleinteriorcolor_Read";
            public const string WebAPI_VWI_CRM_xts_newvehiclesalesorder_Read = "WebAPI_VWI_CRM_xts_newvehiclesalesorder_Read";
            public const string WebAPI_VWI_CRM_xts_newvehiclesalesquote_Read = "WebAPI_VWI_CRM_xts_newvehiclesalesquote_Read";
            public const string WebAPI_VWI_CRM_xts_newvehiclewholesaleorder_Read = "WebAPI_VWI_CRM_xts_newvehiclewholesaleorder_Read";
            public const string WebAPI_VWI_CRM_xts_nonsalesdelivery_Read = "WebAPI_VWI_CRM_xts_nonsalesdelivery_Read";
            public const string WebAPI_VWI_CRM_xts_nvsomiscellaneouscharge_Read = "WebAPI_VWI_CRM_xts_nvsomiscellaneouscharge_Read";
            public const string WebAPI_VWI_CRM_xts_nvsoaccessories_Read = "WebAPI_VWI_CRM_xts_nvsoaccessories_Read";
            public const string WebAPI_VWI_CRM_xts_nvsonumberregistrationdetails_Read = "WebAPI_VWI_CRM_xts_nvsonumberregistrationdetails_Read";
            public const string WebAPI_VWI_CRM_xts_nvsoreferral_Read = "WebAPI_VWI_CRM_xts_nvsoreferral_Read";
            public const string WebAPI_VWI_CRM_xts_nvsqaccessories_Read = "WebAPI_VWI_CRM_xts_nvsqaccessories_Read";
            public const string WebAPI_VWI_CRM_xts_nvsqmiscellaneouscharge_Read = "WebAPI_VWI_CRM_xts_nvsqmiscellaneouscharge_Read";
            public const string WebAPI_VWI_CRM_xts_ordertype_Read = "WebAPI_VWI_CRM_xts_ordertype_Read";
            public const string WebAPI_VWI_CRM_xts_outsourcereservation_Read = "WebAPI_VWI_CRM_xts_outsourcereservation_Read";
            public const string WebAPI_VWI_CRM_xts_outsourceworkorder_Read = "WebAPI_VWI_CRM_xts_outsourceworkorder_Read";
            public const string WebAPI_VWI_CRM_xts_outsourceworkorderdetail_Read = "WebAPI_VWI_CRM_xts_outsourceworkorderdetail_Read";
            public const string WebAPI_VWI_CRM_xts_outsourceworkorderreceipt_Read = "WebAPI_VWI_CRM_xts_outsourceworkorderreceipt_Read";
            public const string WebAPI_VWI_CRM_xts_outsourceworkshopconfiguration_Read = "WebAPI_VWI_CRM_xts_outsourceworkshopconfiguration_Read";
            public const string WebAPI_VWI_CRM_xts_physicalinventorylist_Read = "WebAPI_VWI_CRM_xts_physicalinventorylist_Read";
            public const string WebAPI_VWI_CRM_xts_physicalinventorylistdetail_Read = "WebAPI_VWI_CRM_xts_physicalinventorylistdetail_Read";
            public const string WebAPI_VWI_CRM_xts_pricelist_Read = "WebAPI_VWI_CRM_xts_pricelist_Read";
            public const string WebAPI_VWI_CRM_xts_pricelistdetail_Read = "WebAPI_VWI_CRM_xts_pricelistdetail_Read";
            public const string WebAPI_VWI_CRM_xts_product_Read = "WebAPI_VWI_CRM_xts_product_Read";
            public const string WebAPI_VWI_CRM_xts_productclass_Read = "WebAPI_VWI_CRM_xts_productclass_Read";
            public const string WebAPI_VWI_CRM_xts_productcrossreference_Read = "WebAPI_VWI_CRM_xts_productcrossreference_Read";
            public const string WebAPI_VWI_CRM_xts_productexteriorcolor_Read = "WebAPI_VWI_CRM_xts_productexteriorcolor_Read";
            public const string WebAPI_VWI_CRM_xts_productsegment1_Read = "WebAPI_VWI_CRM_xts_productsegment1_Read";
            public const string WebAPI_VWI_CRM_xts_productsegment2_Read = "WebAPI_VWI_CRM_xts_productsegment2_Read";
            public const string WebAPI_VWI_CRM_xts_productsegment3_Read = "WebAPI_VWI_CRM_xts_productsegment3_Read";
            public const string WebAPI_VWI_CRM_xts_productsegment4_Read = "WebAPI_VWI_CRM_xts_productsegment4_Read";
            public const string WebAPI_VWI_CRM_xts_productstyle_Read = "WebAPI_VWI_CRM_xts_productstyle_Read";
            public const string WebAPI_VWI_CRM_xts_productsubstitute_Read = "WebAPI_VWI_CRM_xts_productsubstitute_Read";
            public const string WebAPI_VWI_CRM_xts_province_Read = "WebAPI_VWI_CRM_xts_province_Read";
            public const string WebAPI_VWI_CRM_xts_purchaseorder_Read = "WebAPI_VWI_CRM_xts_purchaseorder_Read";
            public const string WebAPI_VWI_CRM_xts_purchaseorderdetail_Read = "WebAPI_VWI_CRM_xts_purchaseorderdetail_Read";
            public const string WebAPI_VWI_CRM_xts_purchasereceipt_Read = "WebAPI_VWI_CRM_xts_purchasereceipt_Read";
            public const string WebAPI_VWI_CRM_xts_purchasereceiptdetail_Read = "WebAPI_VWI_CRM_xts_purchasereceiptdetail_Read";
            public const string WebAPI_VWI_CRM_xts_purchasereceiptdetaillandedcost_Read = "WebAPI_VWI_CRM_xts_purchasereceiptdetaillandedcost_Read";
            public const string WebAPI_VWI_CRM_xts_purchaserequisition_Read = "WebAPI_VWI_CRM_xts_purchaserequisition_Read";
            public const string WebAPI_VWI_CRM_xts_purchaserequisitiondetail_Read = "WebAPI_VWI_CRM_xts_purchaserequisitiondetail_Read";
            public const string WebAPI_VWI_CRM_xts_purchaserequisitionpurchaseordertype_Read = "WebAPI_VWI_CRM_xts_purchaserequisitionpurchaseordertype_Read";
            public const string WebAPI_VWI_CRM_xts_reason_Read = "WebAPI_VWI_CRM_xts_reason_Read";
            public const string WebAPI_VWI_CRM_xts_recommendedproduct_Read = "WebAPI_VWI_CRM_xts_recommendedproduct_Read";
            public const string WebAPI_VWI_CRM_xts_reservationtransaction_Read = "WebAPI_VWI_CRM_xts_reservationtransaction_Read";
            public const string WebAPI_VWI_CRM_xts_salesorder_Read = "WebAPI_VWI_CRM_xts_salesorder_Read";
            public const string WebAPI_VWI_CRM_xts_salesorderdetail_Read = "WebAPI_VWI_CRM_xts_salesorderdetail_Read";
            public const string WebAPI_VWI_CRM_xts_servicecampaign_Read = "WebAPI_VWI_CRM_xts_servicecampaign_Read";
            public const string WebAPI_VWI_CRM_xts_servicecategory_Read = "WebAPI_VWI_CRM_xts_servicecategory_Read";
            public const string WebAPI_VWI_CRM_xts_servicecategorygroup_Read = "WebAPI_VWI_CRM_xts_servicecategorygroup_Read";
            public const string WebAPI_VWI_CRM_xts_serviceinstruction_Read = "WebAPI_VWI_CRM_xts_serviceinstruction_Read";
            public const string WebAPI_VWI_CRM_xts_servicemiscellaneouscharge_Read = "WebAPI_VWI_CRM_xts_servicemiscellaneouscharge_Read";
            public const string WebAPI_VWI_CRM_xts_servicemms_Read = "WebAPI_VWI_CRM_xts_servicemms_Read";
            public const string WebAPI_VWI_CRM_xts_serviceproportionalinvoice_Read = "WebAPI_VWI_CRM_xts_serviceproportionalinvoice_Read";
            public const string WebAPI_VWI_CRM_xts_servicereservationclass_Read = "WebAPI_VWI_CRM_xts_servicereservationclass_Read";
            public const string WebAPI_VWI_CRM_xts_servicetemplate_Read = "WebAPI_VWI_CRM_xts_servicetemplate_Read";
            public const string WebAPI_VWI_CRM_xts_servicetemplateactivity_Read = "WebAPI_VWI_CRM_xts_servicetemplateactivity_Read";
            public const string WebAPI_VWI_CRM_xts_servicetemplatedetail_Read = "WebAPI_VWI_CRM_xts_servicetemplatedetail_Read";
            public const string WebAPI_VWI_CRM_xts_servicetemplateparentgroup_Read = "WebAPI_VWI_CRM_xts_servicetemplateparentgroup_Read";
            public const string WebAPI_VWI_CRM_xts_servicetemplateparentgroupdetail_Read = "WebAPI_VWI_CRM_xts_servicetemplateparentgroupdetail_Read";
            public const string WebAPI_VWI_CRM_xts_serviceworkshopsetting_Read = "WebAPI_VWI_CRM_xts_serviceworkshopsetting_Read";
            public const string WebAPI_VWI_CRM_xts_sharedproduct_Read = "WebAPI_VWI_CRM_xts_sharedproduct_Read";
            public const string WebAPI_VWI_CRM_xts_site_Read = "WebAPI_VWI_CRM_xts_site_Read";
            public const string WebAPI_VWI_CRM_xts_siteinquiry_Read = "WebAPI_VWI_CRM_xts_siteinquiry_Read";
            public const string WebAPI_VWI_CRM_xts_style_Read = "WebAPI_VWI_CRM_xts_style_Read";
            public const string WebAPI_VWI_CRM_xts_termofpayment_Read = "WebAPI_VWI_CRM_xts_termofpayment_Read";
            public const string WebAPI_VWI_CRM_xts_transferorderrequesting_Read = "WebAPI_VWI_CRM_xts_transferorderrequesting_Read";
            public const string WebAPI_VWI_CRM_xts_uom_Read = "WebAPI_VWI_CRM_xts_uom_Read";
            public const string WebAPI_VWI_CRM_xts_uomconversion_Read = "WebAPI_VWI_CRM_xts_uomconversion_Read";
            public const string WebAPI_VWI_CRM_xts_uvsoreferralinformation_Read = "WebAPI_VWI_CRM_xts_uvsoreferralinformation_Read";
            public const string WebAPI_VWI_CRM_xts_vehiclebrand_Read = "WebAPI_VWI_CRM_xts_vehiclebrand_Read";
            public const string WebAPI_VWI_CRM_xts_vehicleinformation_Read = "WebAPI_VWI_CRM_xts_vehicleinformation_Read";
            public const string WebAPI_VWI_CRM_xts_vehiclemodel_Read = "WebAPI_VWI_CRM_xts_vehiclemodel_Read";
            public const string WebAPI_VWI_CRM_xts_vehicleprice_Read = "WebAPI_VWI_CRM_xts_vehicleprice_Read";
            public const string WebAPI_VWI_CRM_xts_vehiclepricedetail_Read = "WebAPI_VWI_CRM_xts_vehiclepricedetail_Read";
            public const string WebAPI_VWI_CRM_xts_vehiclepublic_Read = "WebAPI_VWI_CRM_xts_vehiclepublic_Read";
            public const string WebAPI_VWI_CRM_xts_vehiclerecognizedmodel_Read = "WebAPI_VWI_CRM_xts_vehiclerecognizedmodel_Read";
            public const string WebAPI_VWI_CRM_xts_vehiclespecification_Read = "WebAPI_VWI_CRM_xts_vehiclespecification_Read";
            public const string WebAPI_VWI_CRM_xts_vehicletransactionhistory_Read = "WebAPI_VWI_CRM_xts_vehicletransactionhistory_Read";
            public const string WebAPI_VWI_CRM_xts_vehicletransferhistory_Read = "WebAPI_VWI_CRM_xts_vehicletransferhistory_Read";
            public const string WebAPI_VWI_CRM_xts_vendor_Read = "WebAPI_VWI_CRM_xts_vendor_Read";
            public const string WebAPI_VWI_CRM_xts_vendorclass_Read = "WebAPI_VWI_CRM_xts_vendorclass_Read";
            public const string WebAPI_VWI_CRM_xts_villageandstreet_Read = "WebAPI_VWI_CRM_xts_villageandstreet_Read";
            public const string WebAPI_VWI_CRM_xts_warehouse_Read = "WebAPI_VWI_CRM_xts_warehouse_Read";
            public const string WebAPI_VWI_CRM_xts_warehouseinquiry_Read = "WebAPI_VWI_CRM_xts_warehouseinquiry_Read";
            public const string WebAPI_VWI_CRM_xts_workorder_Read = "WebAPI_VWI_CRM_xts_workorder_Read";
            public const string WebAPI_VWI_CRM_xts_workorderpartmaterialandservice_Read = "WebAPI_VWI_CRM_xts_workorderpartmaterialandservice_Read";
            public const string WebAPI_VWI_CRM_xts_workorderservicetemplate_Read = "WebAPI_VWI_CRM_xts_workorderservicetemplate_Read";
            public const string WebAPI_VWI_CRM_xts_workordertimeregister_Read = "WebAPI_VWI_CRM_xts_workordertimeregister_Read";
            public const string WebAPI_VWI_CRM_xts_writeoffbalance_Read = "WebAPI_VWI_CRM_xts_writeoffbalance_Read";
            public const string WebAPI_VWI_CRM_xts_writeoffbalancedetail_Read = "WebAPI_VWI_CRM_xts_writeoffbalancedetail_Read";
            public const string WebAPI_VWI_CRM_ktb_productsapconversion_Read = "WebAPI_VWI_CRM_ktb_productsapconversion_Read";
            public const string WebAPI_VWI_CRM_ktb_lkpp_Read = "WebAPI_VWI_CRM_ktb_lkpp_Read";
            public const string WebAPI_VWI_CRM_ktb_lkppdetail_Read = "WebAPI_VWI_CRM_ktb_lkppdetail_Read";

            public const string WebAPI_VWI_Zombie_WOTimeRegister_Read = "WebAPI_VWI_Zombie_WOTimeRegister_Read";
            public const string WebAPI_VWI_Zombie_Suspect_Read = "WebAPI_VWI_Zombie_Suspect_Read";
            public const string WebAPI_VWI_Zombie_Campaign_Read = "WebAPI_VWI_Zombie_Campaign_Read";
            public const string WebAPI_VWI_Zombie_Reservation_Read = "WebAPI_VWI_Zombie_Reservation_Read";
            #endregion

            #region Non DMS
            public const string WebAPI_CRM_xts_purchaserequisition_Create = "WebAPI_CRM_xts_purchaserequisition_Create";
            public const string WebAPI_CRM_xts_purchaserequisition_Delete = "WebAPI_CRM_xts_purchaserequisition_Delete";
            public const string WebAPI_CRM_xts_purchaserequisition_Read = "WebAPI_CRM_xts_purchaserequisition_Read";
            public const string WebAPI_CRM_xts_purchaserequisition_Update = "WebAPI_CRM_xts_purchaserequisition_Update";
            public const string WebAPI_CRM_xts_purchaserequisition_CreateList = "WebAPI_CRM_xts_purchaserequisition_CreateList";

            public const string WebAPI_CRM_xts_purchasereceipt_Create = "WebAPI_CRM_xts_purchasereceipt_Create";
            public const string WebAPI_CRM_xts_purchasereceipt_Delete = "WebAPI_CRM_xts_purchasereceipt_Delete";
            public const string WebAPI_CRM_xts_purchasereceipt_Read = "WebAPI_CRM_xts_purchasereceipt_Read";
            public const string WebAPI_CRM_xts_purchasereceipt_Update = "WebAPI_CRM_xts_purchasereceipt_Update";
            public const string WebAPI_CRM_xts_purchasereceipt_CreateList = "WebAPI_CRM_xts_purchasereceipt_CreateList";

            public const string WebAPI_CRM_xts_inventorytransfer_Create = "WebAPI_CRM_xts_inventorytransfer_Create";
            public const string WebAPI_CRM_xts_inventorytransfer_Delete = "WebAPI_CRM_xts_inventorytransfer_Delete";
            public const string WebAPI_CRM_xts_inventorytransfer_Read = "WebAPI_CRM_xts_inventorytransfer_Read";
            public const string WebAPI_CRM_xts_inventorytransfer_Update = "WebAPI_CRM_xts_inventorytransfer_Update";
            public const string WebAPI_CRM_xts_inventorytransfer_CreateList = "WebAPI_CRM_xts_inventorytransfer_CreateList";

            public const string WebAPI_CRM_xts_customerpublic_Create = "WebAPI_CRM_xts_customerpublic_Create";
            public const string WebAPI_CRM_xts_customerpublic_Delete = "WebAPI_CRM_xts_customerpublic_Delete";
            public const string WebAPI_CRM_xts_customerpublic_Read = "WebAPI_CRM_xts_customerpublic_Read";
            public const string WebAPI_CRM_xts_customerpublic_Update = "WebAPI_CRM_xts_customerpublic_Update";
            public const string WebAPI_CRM_xts_customerpublic_CreateList = "WebAPI_CRM_xts_customerpublic_CreateList";

            public const string WebAPI_CRM_account_Create = "WebAPI_CRM_account_Create";
            public const string WebAPI_CRM_account_Delete = "WebAPI_CRM_account_Delete";
            public const string WebAPI_CRM_account_Read = "WebAPI_CRM_account_Read";
            public const string WebAPI_CRM_account_Update = "WebAPI_CRM_account_Update";
            public const string WebAPI_CRM_account_CreateList = "WebAPI_CRM_account_CreateList";

            public const string WebAPI_CRM_campaign_Create = "WebAPI_CRM_campaign_Create";
            public const string WebAPI_CRM_campaign_Delete = "WebAPI_CRM_campaign_Delete";
            public const string WebAPI_CRM_campaign_Read = "WebAPI_CRM_campaign_Read";
            public const string WebAPI_CRM_campaign_Update = "WebAPI_CRM_campaign_Update";
            public const string WebAPI_CRM_campaign_CreateList = "WebAPI_CRM_campaign_CreateList";

            public const string WebAPI_CRM_lead_Create = "WebAPI_CRM_lead_Create";
            public const string WebAPI_CRM_lead_Delete = "WebAPI_CRM_lead_Delete";
            public const string WebAPI_CRM_lead_Read = "WebAPI_CRM_lead_Read";
            public const string WebAPI_CRM_lead_Update = "WebAPI_CRM_lead_Update";
            public const string WebAPI_CRM_lead_CreateList = "WebAPI_CRM_lead_CreateList";

            public const string WebAPI_CRM_opportunity_Create = "WebAPI_CRM_opportunity_Create";
            public const string WebAPI_CRM_opportunity_Delete = "WebAPI_CRM_opportunity_Delete";
            public const string WebAPI_CRM_opportunity_Read = "WebAPI_CRM_opportunity_Read";
            public const string WebAPI_CRM_opportunity_Update = "WebAPI_CRM_opportunity_Update";
            public const string WebAPI_CRM_opportunity_CreateList = "WebAPI_CRM_opportunity_CreateList";

            public const string WebAPI_CRM_xid_registrationmonitoring_Create = "WebAPI_CRM_xid_registrationmonitoring_Create";
            public const string WebAPI_CRM_xid_registrationmonitoring_Delete = "WebAPI_CRM_xid_registrationmonitoring_Delete";
            public const string WebAPI_CRM_xid_registrationmonitoring_Read = "WebAPI_CRM_xid_registrationmonitoring_Read";
            public const string WebAPI_CRM_xid_registrationmonitoring_Update = "WebAPI_CRM_xid_registrationmonitoring_Update";
            public const string WebAPI_CRM_xid_registrationmonitoring_CreateList = "WebAPI_CRM_xid_registrationmonitoring_CreateList";

            public const string WebAPI_CRM_xts_deliveryorder_Create = "WebAPI_CRM_xts_deliveryorder_Create";
            public const string WebAPI_CRM_xts_deliveryorder_Delete = "WebAPI_CRM_xts_deliveryorder_Delete";
            public const string WebAPI_CRM_xts_deliveryorder_Read = "WebAPI_CRM_xts_deliveryorder_Read";
            public const string WebAPI_CRM_xts_deliveryorder_Update = "WebAPI_CRM_xts_deliveryorder_Update";
            public const string WebAPI_CRM_xts_deliveryorder_CreateList = "WebAPI_CRM_xts_deliveryorder_CreateList";

            public const string WebAPI_CRM_xts_deliveryorderdetail_Create = "WebAPI_CRM_xts_deliveryorderdetail_Create";
            public const string WebAPI_CRM_xts_deliveryorderdetail_Delete = "WebAPI_CRM_xts_deliveryorderdetail_Delete";
            public const string WebAPI_CRM_xts_deliveryorderdetail_Read = "WebAPI_CRM_xts_deliveryorderdetail_Read";
            public const string WebAPI_CRM_xts_deliveryorderdetail_Update = "WebAPI_CRM_xts_deliveryorderdetail_Update";
            public const string WebAPI_CRM_xts_deliveryorderdetail_CreateList = "WebAPI_CRM_xts_deliveryorderdetail_CreateList";

            public const string WebAPI_CRM_xts_incomingoutsourceworkorderdetail_Create = "WebAPI_CRM_xts_incomingoutsourceworkorderdetail_Create";
            public const string WebAPI_CRM_xts_incomingoutsourceworkorderdetail_Delete = "WebAPI_CRM_xts_incomingoutsourceworkorderdetail_Delete";
            public const string WebAPI_CRM_xts_incomingoutsourceworkorderdetail_Read = "WebAPI_CRM_xts_incomingoutsourceworkorderdetail_Read";
            public const string WebAPI_CRM_xts_incomingoutsourceworkorderdetail_Update = "WebAPI_CRM_xts_incomingoutsourceworkorderdetail_Update";
            public const string WebAPI_CRM_xts_incomingoutsourceworkorderdetail_CreateList = "WebAPI_CRM_xts_incomingoutsourceworkorderdetail_CreateList";

            public const string WebAPI_CRM_xts_inventtrans_Create = "WebAPI_CRM_xts_inventtrans_Create";
            public const string WebAPI_CRM_xts_inventtrans_Delete = "WebAPI_CRM_xts_inventtrans_Delete";
            public const string WebAPI_CRM_xts_inventtrans_Read = "WebAPI_CRM_xts_inventtrans_Read";
            public const string WebAPI_CRM_xts_inventtrans_Update = "WebAPI_CRM_xts_inventtrans_Update";
            public const string WebAPI_CRM_xts_inventtrans_CreateList = "WebAPI_CRM_xts_inventtrans_CreateList";

            public const string WebAPI_CRM_xts_newvehicledeliveryorder_Create = "WebAPI_CRM_xts_newvehicledeliveryorder_Create";
            public const string WebAPI_CRM_xts_newvehicledeliveryorder_Delete = "WebAPI_CRM_xts_newvehicledeliveryorder_Delete";
            public const string WebAPI_CRM_xts_newvehicledeliveryorder_Read = "WebAPI_CRM_xts_newvehicledeliveryorder_Read";
            public const string WebAPI_CRM_xts_newvehicledeliveryorder_Update = "WebAPI_CRM_xts_newvehicledeliveryorder_Update";
            public const string WebAPI_CRM_xts_newvehicledeliveryorder_CreateList = "WebAPI_CRM_xts_newvehicledeliveryorder_CreateList";

            public const string WebAPI_CRM_xts_newvehiclewholesaleorder_Create = "WebAPI_CRM_xts_newvehiclewholesaleorder_Create";
            public const string WebAPI_CRM_xts_newvehiclewholesaleorder_Delete = "WebAPI_CRM_xts_newvehiclewholesaleorder_Delete";
            public const string WebAPI_CRM_xts_newvehiclewholesaleorder_Read = "WebAPI_CRM_xts_newvehiclewholesaleorder_Read";
            public const string WebAPI_CRM_xts_newvehiclewholesaleorder_Update = "WebAPI_CRM_xts_newvehiclewholesaleorder_Update";
            public const string WebAPI_CRM_xts_newvehiclewholesaleorder_CreateList = "WebAPI_CRM_xts_newvehiclewholesaleorder_CreateList";

            public const string WebAPI_CRM_xts_purchasereceiptdetail_Create = "WebAPI_CRM_xts_purchasereceiptdetail_Create";
            public const string WebAPI_CRM_xts_purchasereceiptdetail_Delete = "WebAPI_CRM_xts_purchasereceiptdetail_Delete";
            public const string WebAPI_CRM_xts_purchasereceiptdetail_Read = "WebAPI_CRM_xts_purchasereceiptdetail_Read";
            public const string WebAPI_CRM_xts_purchasereceiptdetail_Update = "WebAPI_CRM_xts_purchasereceiptdetail_Update";
            public const string WebAPI_CRM_xts_purchasereceiptdetail_CreateList = "WebAPI_CRM_xts_purchasereceiptdetail_CreateList";

            public const string WebAPI_CRM_xts_purchasereceiptdetaillandedcost_Create = "WebAPI_CRM_xts_purchasereceiptdetaillandedcost_Create";
            public const string WebAPI_CRM_xts_purchasereceiptdetaillandedcost_Delete = "WebAPI_CRM_xts_purchasereceiptdetaillandedcost_Delete";
            public const string WebAPI_CRM_xts_purchasereceiptdetaillandedcost_Read = "WebAPI_CRM_xts_purchasereceiptdetaillandedcost_Read";
            public const string WebAPI_CRM_xts_purchasereceiptdetaillandedcost_Update = "WebAPI_CRM_xts_purchasereceiptdetaillandedcost_Update";
            public const string WebAPI_CRM_xts_purchasereceiptdetaillandedcost_CreateList = "WebAPI_CRM_xts_purchasereceiptdetaillandedcost_CreateList";

            public const string WebAPI_CRM_xts_workordertimeregister_Create = "WebAPI_CRM_xts_workordertimeregister_Create";
            public const string WebAPI_CRM_xts_workordertimeregister_Delete = "WebAPI_CRM_xts_workordertimeregister_Delete";
            public const string WebAPI_CRM_xts_workordertimeregister_Read = "WebAPI_CRM_xts_workordertimeregister_Read";
            public const string WebAPI_CRM_xts_workordertimeregister_Update = "WebAPI_CRM_xts_workordertimeregister_Update";
            public const string WebAPI_CRM_xts_workordertimeregister_CreateList = "WebAPI_CRM_xts_workordertimeregister_CreateList";

            public const string WebAPI_CRM_xts_workorder_Create = "WebAPI_CRM_xts_workorder_Create";
            public const string WebAPI_CRM_xts_workorder_Delete = "WebAPI_CRM_xts_workorder_Delete";
            public const string WebAPI_CRM_xts_workorder_Read = "WebAPI_CRM_xts_workorder_Read";
            public const string WebAPI_CRM_xts_workorder_Update = "WebAPI_CRM_xts_workorder_Update";
            public const string WebAPI_CRM_xts_workorder_CreateList = "WebAPI_CRM_xts_workorder_CreateList";

            public const string WebAPI_CRM_xts_vendor_Create = "WebAPI_CRM_xts_vendor_Create";
            public const string WebAPI_CRM_xts_vendor_Delete = "WebAPI_CRM_xts_vendor_Delete";
            public const string WebAPI_CRM_xts_vendor_Read = "WebAPI_CRM_xts_vendor_Read";
            public const string WebAPI_CRM_xts_vendor_Update = "WebAPI_CRM_xts_vendor_Update";
            public const string WebAPI_CRM_xts_vendor_CreateList = "WebAPI_CRM_xts_vendor_CreateList";

            public const string WebAPI_CRM_xts_salesorderdetail_Create = "WebAPI_CRM_xts_salesorderdetail_Create";
            public const string WebAPI_CRM_xts_salesorderdetail_Delete = "WebAPI_CRM_xts_salesorderdetail_Delete";
            public const string WebAPI_CRM_xts_salesorderdetail_Read = "WebAPI_CRM_xts_salesorderdetail_Read";
            public const string WebAPI_CRM_xts_salesorderdetail_Update = "WebAPI_CRM_xts_salesorderdetail_Update";
            public const string WebAPI_CRM_xts_salesorderdetail_CreateList = "WebAPI_CRM_xts_salesorderdetail_CreateList";

            public const string WebAPI_CRM_xts_salesorder_Create = "WebAPI_CRM_xts_salesorder_Create";
            public const string WebAPI_CRM_xts_salesorder_Delete = "WebAPI_CRM_xts_salesorder_Delete";
            public const string WebAPI_CRM_xts_salesorder_Read = "WebAPI_CRM_xts_salesorder_Read";
            public const string WebAPI_CRM_xts_salesorder_Update = "WebAPI_CRM_xts_salesorder_Update";
            public const string WebAPI_CRM_xts_salesorder_CreateList = "WebAPI_CRM_xts_salesorder_CreateList";

            public const string WebAPI_CRM_xts_purchaserequisitiondetail_Create = "WebAPI_CRM_xts_purchaserequisitiondetail_Create";
            public const string WebAPI_CRM_xts_purchaserequisitiondetail_Delete = "WebAPI_CRM_xts_purchaserequisitiondetail_Delete";
            public const string WebAPI_CRM_xts_purchaserequisitiondetail_Read = "WebAPI_CRM_xts_purchaserequisitiondetail_Read";
            public const string WebAPI_CRM_xts_purchaserequisitiondetail_Update = "WebAPI_CRM_xts_purchaserequisitiondetail_Update";
            public const string WebAPI_CRM_xts_purchaserequisitiondetail_CreateList = "WebAPI_CRM_xts_purchaserequisitiondetail_CreateList";

            public const string WebAPI_CRM_xts_purchaseorderdetail_Create = "WebAPI_CRM_xts_purchaseorderdetail_Create";
            public const string WebAPI_CRM_xts_purchaseorderdetail_Delete = "WebAPI_CRM_xts_purchaseorderdetail_Delete";
            public const string WebAPI_CRM_xts_purchaseorderdetail_Read = "WebAPI_CRM_xts_purchaseorderdetail_Read";
            public const string WebAPI_CRM_xts_purchaseorderdetail_Update = "WebAPI_CRM_xts_purchaseorderdetail_Update";
            public const string WebAPI_CRM_xts_purchaseorderdetail_CreateList = "WebAPI_CRM_xts_purchaseorderdetail_CreateList";

            public const string WebAPI_CRM_xts_purchaseorder_Create = "WebAPI_CRM_xts_purchaseorder_Create";
            public const string WebAPI_CRM_xts_purchaseorder_Delete = "WebAPI_CRM_xts_purchaseorder_Delete";
            public const string WebAPI_CRM_xts_purchaseorder_Read = "WebAPI_CRM_xts_purchaseorder_Read";
            public const string WebAPI_CRM_xts_purchaseorder_Update = "WebAPI_CRM_xts_purchaseorder_Update";
            public const string WebAPI_CRM_xts_purchaseorder_CreateList = "WebAPI_CRM_xts_purchaseorder_CreateList";

            public const string WebAPI_CRM_xts_nonsalesdelivery_Create = "WebAPI_CRM_xts_nonsalesdelivery_Create";
            public const string WebAPI_CRM_xts_nonsalesdelivery_Delete = "WebAPI_CRM_xts_nonsalesdelivery_Delete";
            public const string WebAPI_CRM_xts_nonsalesdelivery_Read = "WebAPI_CRM_xts_nonsalesdelivery_Read";
            public const string WebAPI_CRM_xts_nonsalesdelivery_Update = "WebAPI_CRM_xts_nonsalesdelivery_Update";
            public const string WebAPI_CRM_xts_nonsalesdelivery_CreateList = "WebAPI_CRM_xts_nonsalesdelivery_CreateList";

            public const string WebAPI_CRM_xts_newvehiclesalesorder_Create = "WebAPI_CRM_xts_newvehiclesalesorder_Create";
            public const string WebAPI_CRM_xts_newvehiclesalesorder_Delete = "WebAPI_CRM_xts_newvehiclesalesorder_Delete";
            public const string WebAPI_CRM_xts_newvehiclesalesorder_Read = "WebAPI_CRM_xts_newvehiclesalesorder_Read";
            public const string WebAPI_CRM_xts_newvehiclesalesorder_Update = "WebAPI_CRM_xts_newvehiclesalesorder_Update";
            public const string WebAPI_CRM_xts_newvehiclesalesorder_CreateList = "WebAPI_CRM_xts_newvehiclesalesorder_CreateList";

            public const string WebAPI_CRM_xts_matchunmatch_Create = "WebAPI_CRM_xts_matchunmatch_Create";
            public const string WebAPI_CRM_xts_matchunmatch_Delete = "WebAPI_CRM_xts_matchunmatch_Delete";
            public const string WebAPI_CRM_xts_matchunmatch_Read = "WebAPI_CRM_xts_matchunmatch_Read";
            public const string WebAPI_CRM_xts_matchunmatch_Update = "WebAPI_CRM_xts_matchunmatch_Update";
            public const string WebAPI_CRM_xts_matchunmatch_CreateList = "WebAPI_CRM_xts_matchunmatch_CreateList";

            public const string WebAPI_CRM_xts_inventorytransactiondetail_Create = "WebAPI_CRM_xts_inventorytransactiondetail_Create";
            public const string WebAPI_CRM_xts_inventorytransactiondetail_Delete = "WebAPI_CRM_xts_inventorytransactiondetail_Delete";
            public const string WebAPI_CRM_xts_inventorytransactiondetail_Read = "WebAPI_CRM_xts_inventorytransactiondetail_Read";
            public const string WebAPI_CRM_xts_inventorytransactiondetail_Update = "WebAPI_CRM_xts_inventorytransactiondetail_Update";
            public const string WebAPI_CRM_xts_inventorytransactiondetail_CreateList = "WebAPI_CRM_xts_inventorytransactiondetail_CreateList";

            public const string WebAPI_CRM_xts_inventorytransaction_Create = "WebAPI_CRM_xts_inventorytransaction_Create";
            public const string WebAPI_CRM_xts_inventorytransaction_Delete = "WebAPI_CRM_xts_inventorytransaction_Delete";
            public const string WebAPI_CRM_xts_inventorytransaction_Read = "WebAPI_CRM_xts_inventorytransaction_Read";
            public const string WebAPI_CRM_xts_inventorytransaction_Update = "WebAPI_CRM_xts_inventorytransaction_Update";
            public const string WebAPI_CRM_xts_inventorytransaction_CreateList = "WebAPI_CRM_xts_inventorytransaction_CreateList";

            public const string WebAPI_CRM_xts_assigntosalesperson_Create = "WebAPI_CRM_xts_assigntosalesperson_Create";
            public const string WebAPI_CRM_xts_assigntosalesperson_Delete = "WebAPI_CRM_xts_assigntosalesperson_Delete";
            public const string WebAPI_CRM_xts_assigntosalesperson_Read = "WebAPI_CRM_xts_assigntosalesperson_Read";
            public const string WebAPI_CRM_xts_assigntosalesperson_Update = "WebAPI_CRM_xts_assigntosalesperson_Update";
            public const string WebAPI_CRM_xts_assigntosalesperson_CreateList = "WebAPI_CRM_xts_assigntosalesperson_CreateList";

            public const string WebAPI_CRM_xjp_predeliveryinspection_Create = "WebAPI_CRM_xjp_predeliveryinspection_Create";
            public const string WebAPI_CRM_xjp_predeliveryinspection_Delete = "WebAPI_CRM_xjp_predeliveryinspection_Delete";
            public const string WebAPI_CRM_xjp_predeliveryinspection_Read = "WebAPI_CRM_xjp_predeliveryinspection_Read";
            public const string WebAPI_CRM_xjp_predeliveryinspection_Update = "WebAPI_CRM_xjp_predeliveryinspection_Update";
            public const string WebAPI_CRM_xjp_predeliveryinspection_CreateList = "WebAPI_CRM_xjp_predeliveryinspection_CreateList";

            public const string WebAPI_CRM_xjp_pdireceipt_Create = "WebAPI_CRM_xjp_pdireceipt_Create";
            public const string WebAPI_CRM_xjp_pdireceipt_Delete = "WebAPI_CRM_xjp_pdireceipt_Delete";
            public const string WebAPI_CRM_xjp_pdireceipt_Read = "WebAPI_CRM_xjp_pdireceipt_Read";
            public const string WebAPI_CRM_xjp_pdireceipt_Update = "WebAPI_CRM_xjp_pdireceipt_Update";
            public const string WebAPI_CRM_xjp_pdireceipt_CreateList = "WebAPI_CRM_xjp_pdireceipt_CreateList";

            public const string WebAPI_CRM_xjp_pdidetail_Create = "WebAPI_CRM_xjp_pdidetail_Create";
            public const string WebAPI_CRM_xjp_pdidetail_Delete = "WebAPI_CRM_xjp_pdidetail_Delete";
            public const string WebAPI_CRM_xjp_pdidetail_Read = "WebAPI_CRM_xjp_pdidetail_Read";
            public const string WebAPI_CRM_xjp_pdidetail_Update = "WebAPI_CRM_xjp_pdidetail_Update";
            public const string WebAPI_CRM_xjp_pdidetail_CreateList = "WebAPI_CRM_xjp_pdidetail_CreateList";

            public const string WebAPI_CRM_xts_newvehicleaccessories_Create = "WebAPI_CRM_xts_newvehicleaccessories_Create";
            public const string WebAPI_CRM_xts_newvehicleaccessories_Delete = "WebAPI_CRM_xts_newvehicleaccessories_Delete";
            public const string WebAPI_CRM_xts_newvehicleaccessories_Read = "WebAPI_CRM_xts_newvehicleaccessories_Read";
            public const string WebAPI_CRM_xts_newvehicleaccessories_Update = "WebAPI_CRM_xts_newvehicleaccessories_Update";
            public const string WebAPI_CRM_xts_newvehicleaccessories_CreateList = "WebAPI_CRM_xts_newvehicleaccessories_CreateList";

            public const string WebAPI_CRM_xts_productlist_Create = "WebAPI_CRM_xts_productlist_Create";
            public const string WebAPI_CRM_xts_productlist_Delete = "WebAPI_CRM_xts_productlist_Delete";
            public const string WebAPI_CRM_xts_productlist_Read = "WebAPI_CRM_xts_productlist_Read";
            public const string WebAPI_CRM_xts_productlist_Update = "WebAPI_CRM_xts_productlist_Update";
            public const string WebAPI_CRM_xts_productlist_CreateList = "WebAPI_CRM_xts_productlist_CreateList";

            public const string WebAPI_CRM_xts_outsourceworkorderreceipt_Create = "WebAPI_CRM_xts_outsourceworkorderreceipt_Create";
            public const string WebAPI_CRM_xts_outsourceworkorderreceipt_Delete = "WebAPI_CRM_xts_outsourceworkorderreceipt_Delete";
            public const string WebAPI_CRM_xts_outsourceworkorderreceipt_Read = "WebAPI_CRM_xts_outsourceworkorderreceipt_Read";
            public const string WebAPI_CRM_xts_outsourceworkorderreceipt_Update = "WebAPI_CRM_xts_outsourceworkorderreceipt_Update";
            public const string WebAPI_CRM_xts_outsourceworkorderreceipt_CreateList = "WebAPI_CRM_xts_outsourceworkorderreceipt_CreateList";

            public const string WebAPI_CRM_xts_outsourceworkorder_Create = "WebAPI_CRM_xts_outsourceworkorder_Create";
            public const string WebAPI_CRM_xts_outsourceworkorder_Delete = "WebAPI_CRM_xts_outsourceworkorder_Delete";
            public const string WebAPI_CRM_xts_outsourceworkorder_Read = "WebAPI_CRM_xts_outsourceworkorder_Read";
            public const string WebAPI_CRM_xts_outsourceworkorder_Update = "WebAPI_CRM_xts_outsourceworkorder_Update";
            public const string WebAPI_CRM_xts_outsourceworkorder_CreateList = "WebAPI_CRM_xts_outsourceworkorder_CreateList";

            public const string WebAPI_CRM_xts_newvehiclesalesquote_Create = "WebAPI_CRM_xts_newvehiclesalesquote_Create";
            public const string WebAPI_CRM_xts_newvehiclesalesquote_Delete = "WebAPI_CRM_xts_newvehiclesalesquote_Delete";
            public const string WebAPI_CRM_xts_newvehiclesalesquote_Read = "WebAPI_CRM_xts_newvehiclesalesquote_Read";
            public const string WebAPI_CRM_xts_newvehiclesalesquote_Update = "WebAPI_CRM_xts_newvehiclesalesquote_Update";
            public const string WebAPI_CRM_xts_newvehiclesalesquote_CreateList = "WebAPI_CRM_xts_newvehiclesalesquote_CreateList";

            public const string WebAPI_CRM_xts_inventorytransferdetail_Create = "WebAPI_CRM_xts_inventorytransferdetail_Create";
            public const string WebAPI_CRM_xts_inventorytransferdetail_Delete = "WebAPI_CRM_xts_inventorytransferdetail_Delete";
            public const string WebAPI_CRM_xts_inventorytransferdetail_Read = "WebAPI_CRM_xts_inventorytransferdetail_Read";
            public const string WebAPI_CRM_xts_inventorytransferdetail_Update = "WebAPI_CRM_xts_inventorytransferdetail_Update";
            public const string WebAPI_CRM_xts_inventorytransferdetail_CreateList = "WebAPI_CRM_xts_inventorytransferdetail_CreateList";

            public const string WebAPI_CRM_xts_inventorynewvehicle_Create = "WebAPI_CRM_xts_inventorynewvehicle_Create";
            public const string WebAPI_CRM_xts_inventorynewvehicle_Delete = "WebAPI_CRM_xts_inventorynewvehicle_Delete";
            public const string WebAPI_CRM_xts_inventorynewvehicle_Read = "WebAPI_CRM_xts_inventorynewvehicle_Read";
            public const string WebAPI_CRM_xts_inventorynewvehicle_Update = "WebAPI_CRM_xts_inventorynewvehicle_Update";
            public const string WebAPI_CRM_xts_inventorynewvehicle_CreateList = "WebAPI_CRM_xts_inventorynewvehicle_CreateList";

            public const string WebAPI_CRM_xts_apvdetail_Create = "WebAPI_CRM_xts_apvdetail_Create";
            public const string WebAPI_CRM_xts_apvdetail_Delete = "WebAPI_CRM_xts_apvdetail_Delete";
            public const string WebAPI_CRM_xts_apvdetail_Read = "WebAPI_CRM_xts_apvdetail_Read";
            public const string WebAPI_CRM_xts_apvdetail_Update = "WebAPI_CRM_xts_apvdetail_Update";
            public const string WebAPI_CRM_xts_apvdetail_CreateList = "WebAPI_CRM_xts_apvdetail_CreateList";

            public const string WebAPI_CRM_xts_accountpayablevoucher_Create = "WebAPI_CRM_xts_accountpayablevoucher_Create";
            public const string WebAPI_CRM_xts_accountpayablevoucher_Delete = "WebAPI_CRM_xts_accountpayablevoucher_Delete";
            public const string WebAPI_CRM_xts_accountpayablevoucher_Read = "WebAPI_CRM_xts_accountpayablevoucher_Read";
            public const string WebAPI_CRM_xts_accountpayablevoucher_Update = "WebAPI_CRM_xts_accountpayablevoucher_Update";
            public const string WebAPI_CRM_xts_accountpayablevoucher_CreateList = "WebAPI_CRM_xts_accountpayablevoucher_CreateList";

            public const string WebAPI_CRM_xts_accountpayablepaymentdetail_Create = "WebAPI_CRM_xts_accountpayablepaymentdetail_Create";
            public const string WebAPI_CRM_xts_accountpayablepaymentdetail_Delete = "WebAPI_CRM_xts_accountpayablepaymentdetail_Delete";
            public const string WebAPI_CRM_xts_accountpayablepaymentdetail_Read = "WebAPI_CRM_xts_accountpayablepaymentdetail_Read";
            public const string WebAPI_CRM_xts_accountpayablepaymentdetail_Update = "WebAPI_CRM_xts_accountpayablepaymentdetail_Update";
            public const string WebAPI_CRM_xts_accountpayablepaymentdetail_CreateList = "WebAPI_CRM_xts_accountpayablepaymentdetail_CreateList";

            public const string WebAPI_CRM_xjp_vehicletransfer_Create = "WebAPI_CRM_xjp_vehicletransfer_Create";
            public const string WebAPI_CRM_xjp_vehicletransfer_Delete = "WebAPI_CRM_xjp_vehicletransfer_Delete";
            public const string WebAPI_CRM_xjp_vehicletransfer_Read = "WebAPI_CRM_xjp_vehicletransfer_Read";
            public const string WebAPI_CRM_xjp_vehicletransfer_Update = "WebAPI_CRM_xjp_vehicletransfer_Update";
            public const string WebAPI_CRM_xjp_vehicletransfer_CreateList = "WebAPI_CRM_xjp_vehicletransfer_CreateList";

            public const string WebAPI_CRM_xts_damageorloss_Create = "WebAPI_CRM_xts_damageorloss_Create";
            public const string WebAPI_CRM_xts_damageorloss_Delete = "WebAPI_CRM_xts_damageorloss_Delete";
            public const string WebAPI_CRM_xts_damageorloss_Read = "WebAPI_CRM_xts_damageorloss_Read";
            public const string WebAPI_CRM_xts_damageorloss_Update = "WebAPI_CRM_xts_damageorloss_Update";
            public const string WebAPI_CRM_xts_damageorloss_CreateList = "WebAPI_CRM_xts_damageorloss_CreateList";

            public const string WebAPI_CRM_ktb_kontrabondetail_Create = "WebAPI_CRM_ktb_kontrabondetail_Create";
            public const string WebAPI_CRM_ktb_kontrabondetail_Delete = "WebAPI_CRM_ktb_kontrabondetail_Delete";
            public const string WebAPI_CRM_ktb_kontrabondetail_Read = "WebAPI_CRM_ktb_kontrabondetail_Read";
            public const string WebAPI_CRM_ktb_kontrabondetail_Update = "WebAPI_CRM_ktb_kontrabondetail_Update";
            public const string WebAPI_CRM_ktb_kontrabondetail_CreateList = "WebAPI_CRM_ktb_kontrabondetail_CreateList";

            public const string WebAPI_CRM_ktb_kontrabon_Create = "WebAPI_CRM_ktb_kontrabon_Create";
            public const string WebAPI_CRM_ktb_kontrabon_Delete = "WebAPI_CRM_ktb_kontrabon_Delete";
            public const string WebAPI_CRM_ktb_kontrabon_Read = "WebAPI_CRM_ktb_kontrabon_Read";
            public const string WebAPI_CRM_ktb_kontrabon_Update = "WebAPI_CRM_ktb_kontrabon_Update";
            public const string WebAPI_CRM_ktb_kontrabon_CreateList = "WebAPI_CRM_ktb_kontrabon_CreateList";

            public const string WebAPI_CRM_xts_writeoffbalancedetail_Create = "WebAPI_CRM_xts_writeoffbalancedetail_Create";
            public const string WebAPI_CRM_xts_writeoffbalancedetail_Delete = "WebAPI_CRM_xts_writeoffbalancedetail_Delete";
            public const string WebAPI_CRM_xts_writeoffbalancedetail_Read = "WebAPI_CRM_xts_writeoffbalancedetail_Read";
            public const string WebAPI_CRM_xts_writeoffbalancedetail_Update = "WebAPI_CRM_xts_writeoffbalancedetail_Update";
            public const string WebAPI_CRM_xts_writeoffbalancedetail_CreateList = "WebAPI_CRM_xts_writeoffbalancedetail_CreateList";

            public const string WebAPI_CRM_xts_writeoffbalance_Create = "WebAPI_CRM_xts_writeoffbalance_Create";
            public const string WebAPI_CRM_xts_writeoffbalance_Delete = "WebAPI_CRM_xts_writeoffbalance_Delete";
            public const string WebAPI_CRM_xts_writeoffbalance_Read = "WebAPI_CRM_xts_writeoffbalance_Read";
            public const string WebAPI_CRM_xts_writeoffbalance_Update = "WebAPI_CRM_xts_writeoffbalance_Update";
            public const string WebAPI_CRM_xts_writeoffbalance_CreateList = "WebAPI_CRM_xts_writeoffbalance_CreateList";

            public const string WebAPI_CRM_xts_vehicleinformation_Create = "WebAPI_CRM_xts_vehicleinformation_Create";
            public const string WebAPI_CRM_xts_vehicleinformation_Delete = "WebAPI_CRM_xts_vehicleinformation_Delete";
            public const string WebAPI_CRM_xts_vehicleinformation_Read = "WebAPI_CRM_xts_vehicleinformation_Read";
            public const string WebAPI_CRM_xts_vehicleinformation_Update = "WebAPI_CRM_xts_vehicleinformation_Update";
            public const string WebAPI_CRM_xts_vehicleinformation_CreateList = "WebAPI_CRM_xts_vehicleinformation_CreateList";

            public const string WebAPI_CRM_xts_servicequeue_Create = "WebAPI_CRM_xts_servicequeue_Create";
            public const string WebAPI_CRM_xts_servicequeue_Delete = "WebAPI_CRM_xts_servicequeue_Delete";
            public const string WebAPI_CRM_xts_servicequeue_Read = "WebAPI_CRM_xts_servicequeue_Read";
            public const string WebAPI_CRM_xts_servicequeue_Update = "WebAPI_CRM_xts_servicequeue_Update";
            public const string WebAPI_CRM_xts_servicequeue_CreateList = "WebAPI_CRM_xts_servicequeue_CreateList";

            public const string WebAPI_CRM_xts_reservationtransaction_Create = "WebAPI_CRM_xts_reservationtransaction_Create";
            public const string WebAPI_CRM_xts_reservationtransaction_Delete = "WebAPI_CRM_xts_reservationtransaction_Delete";
            public const string WebAPI_CRM_xts_reservationtransaction_Read = "WebAPI_CRM_xts_reservationtransaction_Read";
            public const string WebAPI_CRM_xts_reservationtransaction_Update = "WebAPI_CRM_xts_reservationtransaction_Update";
            public const string WebAPI_CRM_xts_reservationtransaction_CreateList = "WebAPI_CRM_xts_reservationtransaction_CreateList";

            public const string WebAPI_CRM_xts_employee_Create = "WebAPI_CRM_xts_employee_Create";
            public const string WebAPI_CRM_xts_employee_Delete = "WebAPI_CRM_xts_employee_Delete";
            public const string WebAPI_CRM_xts_employee_Read = "WebAPI_CRM_xts_employee_Read";
            public const string WebAPI_CRM_xts_employee_Update = "WebAPI_CRM_xts_employee_Update";
            public const string WebAPI_CRM_xts_employee_CreateList = "WebAPI_CRM_xts_employee_CreateList";

            public const string WebAPI_CRM_xts_accountreceivableinvoice_Create = "WebAPI_CRM_xts_accountreceivableinvoice_Create";
            public const string WebAPI_CRM_xts_accountreceivableinvoice_Delete = "WebAPI_CRM_xts_accountreceivableinvoice_Delete";
            public const string WebAPI_CRM_xts_accountreceivableinvoice_Read = "WebAPI_CRM_xts_accountreceivableinvoice_Read";
            public const string WebAPI_CRM_xts_accountreceivableinvoice_Update = "WebAPI_CRM_xts_accountreceivableinvoice_Update";
            public const string WebAPI_CRM_xts_accountreceivableinvoice_CreateList = "WebAPI_CRM_xts_accountreceivableinvoice_CreateList";

            public const string WebAPI_CRM_xts_accountpayablepayment_Create = "WebAPI_CRM_xts_accountpayablepayment_Create";
            public const string WebAPI_CRM_xts_accountpayablepayment_Delete = "WebAPI_CRM_xts_accountpayablepayment_Delete";
            public const string WebAPI_CRM_xts_accountpayablepayment_Read = "WebAPI_CRM_xts_accountpayablepayment_Read";
            public const string WebAPI_CRM_xts_accountpayablepayment_Update = "WebAPI_CRM_xts_accountpayablepayment_Update";
            public const string WebAPI_CRM_xts_accountpayablepayment_CreateList = "WebAPI_CRM_xts_accountpayablepayment_CreateList";

            public const string WebAPI_CRM_xts_newvehicleexteriorcolor_Create = "WebAPI_CRM_xts_newvehicleexteriorcolor_Create";
            public const string WebAPI_CRM_xts_newvehicleexteriorcolor_Delete = "WebAPI_CRM_xts_newvehicleexteriorcolor_Delete";
            public const string WebAPI_CRM_xts_newvehicleexteriorcolor_Read = "WebAPI_CRM_xts_newvehicleexteriorcolor_Read";
            public const string WebAPI_CRM_xts_newvehicleexteriorcolor_Update = "WebAPI_CRM_xts_newvehicleexteriorcolor_Update";
            public const string WebAPI_CRM_xts_newvehicleexteriorcolor_CreateList = "WebAPI_CRM_xts_newvehicleexteriorcolor_CreateList";

            public const string WebAPI_CRM_xjp_vehiclecostinput_Create = "WebAPI_CRM_xjp_vehiclecostinput_Create";
            public const string WebAPI_CRM_xjp_vehiclecostinput_Delete = "WebAPI_CRM_xjp_vehiclecostinput_Delete";
            public const string WebAPI_CRM_xjp_vehiclecostinput_Read = "WebAPI_CRM_xjp_vehiclecostinput_Read";
            public const string WebAPI_CRM_xjp_vehiclecostinput_Update = "WebAPI_CRM_xjp_vehiclecostinput_Update";
            public const string WebAPI_CRM_xjp_vehiclecostinput_CreateList = "WebAPI_CRM_xjp_vehiclecostinput_CreateList";

            public const string WebAPI_CRM_xid_documentregistration_Create = "WebAPI_CRM_xid_documentregistration_Create";
            public const string WebAPI_CRM_xid_documentregistration_Delete = "WebAPI_CRM_xid_documentregistration_Delete";
            public const string WebAPI_CRM_xid_documentregistration_Read = "WebAPI_CRM_xid_documentregistration_Read";
            public const string WebAPI_CRM_xid_documentregistration_Update = "WebAPI_CRM_xid_documentregistration_Update";
            public const string WebAPI_CRM_xid_documentregistration_CreateList = "WebAPI_CRM_xid_documentregistration_CreateList";

            public const string WebAPI_CRM_xts_accountreceivableinvoicedetail_Create = "WebAPI_CRM_xts_accountreceivableinvoicedetail_Create";
            public const string WebAPI_CRM_xts_accountreceivableinvoicedetail_Delete = "WebAPI_CRM_xts_accountreceivableinvoicedetail_Delete";
            public const string WebAPI_CRM_xts_accountreceivableinvoicedetail_Read = "WebAPI_CRM_xts_accountreceivableinvoicedetail_Read";
            public const string WebAPI_CRM_xts_accountreceivableinvoicedetail_Update = "WebAPI_CRM_xts_accountreceivableinvoicedetail_Update";
            public const string WebAPI_CRM_xts_accountreceivableinvoicedetail_CreateList = "WebAPI_CRM_xts_accountreceivableinvoicedetail_CreateList";

            public const string WebAPI_CRM_xts_accountreceivablereceipt_Create = "WebAPI_CRM_xts_accountreceivablereceipt_Create";
            public const string WebAPI_CRM_xts_accountreceivablereceipt_Delete = "WebAPI_CRM_xts_accountreceivablereceipt_Delete";
            public const string WebAPI_CRM_xts_accountreceivablereceipt_Read = "WebAPI_CRM_xts_accountreceivablereceipt_Read";
            public const string WebAPI_CRM_xts_accountreceivablereceipt_Update = "WebAPI_CRM_xts_accountreceivablereceipt_Update";
            public const string WebAPI_CRM_xts_accountreceivablereceipt_CreateList = "WebAPI_CRM_xts_accountreceivablereceipt_CreateList";

            public const string WebAPI_CRM_xts_accountreceivablereceiptdetail_Create = "WebAPI_CRM_xts_accountreceivablereceiptdetail_Create";
            public const string WebAPI_CRM_xts_accountreceivablereceiptdetail_Delete = "WebAPI_CRM_xts_accountreceivablereceiptdetail_Delete";
            public const string WebAPI_CRM_xts_accountreceivablereceiptdetail_Read = "WebAPI_CRM_xts_accountreceivablereceiptdetail_Read";
            public const string WebAPI_CRM_xts_accountreceivablereceiptdetail_Update = "WebAPI_CRM_xts_accountreceivablereceiptdetail_Update";
            public const string WebAPI_CRM_xts_accountreceivablereceiptdetail_CreateList = "WebAPI_CRM_xts_accountreceivablereceiptdetail_CreateList";

            public const string WebAPI_CRM_team_Read = "WebAPI_CRM_team_Read";
            public const string WebAPI_CRM_team_Create = "WebAPI_CRM_team_Create";
            public const string WebAPI_CRM_team_Update = "WebAPI_CRM_team_Update";
            public const string WebAPI_CRM_team_Delete = "WebAPI_CRM_team_Delete";
            public const string WebAPI_CRM_team_CreateList = "WebAPI_CRM_team_CreateList";

            public const string WebAPI_CRM_appointment_Create = "WebAPI_CRM_appointment_Create";
            public const string WebAPI_CRM_appointment_Delete = "WebAPI_CRM_appointment_Delete";
            public const string WebAPI_CRM_appointment_Read = "WebAPI_CRM_appointment_Read";
            public const string WebAPI_CRM_appointment_Update = "WebAPI_CRM_appointment_Update";
            public const string WebAPI_CRM_appointment_CreateList = "WebAPI_CRM_appointment_CreateList";

            public const string WebAPI_CRM_campaignresponse_Create = "WebAPI_CRM_campaignresponse_Create";
            public const string WebAPI_CRM_campaignresponse_Delete = "WebAPI_CRM_campaignresponse_Delete";
            public const string WebAPI_CRM_campaignresponse_Read = "WebAPI_CRM_campaignresponse_Read";
            public const string WebAPI_CRM_campaignresponse_Update = "WebAPI_CRM_campaignresponse_Update";
            public const string WebAPI_CRM_campaignresponse_CreateList = "WebAPI_CRM_campaignresponse_CreateList";

            public const string WebAPI_CRM_equipment_Create = "WebAPI_CRM_equipment_Create";
            public const string WebAPI_CRM_equipment_Delete = "WebAPI_CRM_equipment_Delete";
            public const string WebAPI_CRM_equipment_Read = "WebAPI_CRM_equipment_Read";
            public const string WebAPI_CRM_equipment_Update = "WebAPI_CRM_equipment_Update";
            public const string WebAPI_CRM_equipment_CreateList = "WebAPI_CRM_equipment_CreateList";

            public const string WebAPI_CRM_serviceappointment_Create = "WebAPI_CRM_serviceappointment_Create";
            public const string WebAPI_CRM_serviceappointment_Delete = "WebAPI_CRM_serviceappointment_Delete";
            public const string WebAPI_CRM_serviceappointment_Read = "WebAPI_CRM_serviceappointment_Read";
            public const string WebAPI_CRM_serviceappointment_Update = "WebAPI_CRM_serviceappointment_Update";
            public const string WebAPI_CRM_serviceappointment_CreateList = "WebAPI_CRM_serviceappointment_CreateList";

            public const string WebAPI_CRM_xts_accessoriesinstallationcategory_Create = "WebAPI_CRM_xts_accessoriesinstallationcategory_Create";
            public const string WebAPI_CRM_xts_accessoriesinstallationcategory_Delete = "WebAPI_CRM_xts_accessoriesinstallationcategory_Delete";
            public const string WebAPI_CRM_xts_accessoriesinstallationcategory_Read = "WebAPI_CRM_xts_accessoriesinstallationcategory_Read";
            public const string WebAPI_CRM_xts_accessoriesinstallationcategory_Update = "WebAPI_CRM_xts_accessoriesinstallationcategory_Update";
            public const string WebAPI_CRM_xts_accessoriesinstallationcategory_CreateList = "WebAPI_CRM_xts_accessoriesinstallationcategory_CreateList";

            public const string WebAPI_CRM_xts_assessment_Create = "WebAPI_CRM_xts_assessment_Create";
            public const string WebAPI_CRM_xts_assessment_Delete = "WebAPI_CRM_xts_assessment_Delete";
            public const string WebAPI_CRM_xts_assessment_Read = "WebAPI_CRM_xts_assessment_Read";
            public const string WebAPI_CRM_xts_assessment_Update = "WebAPI_CRM_xts_assessment_Update";
            public const string WebAPI_CRM_xts_assessment_CreateList = "WebAPI_CRM_xts_assessment_CreateList";

            public const string WebAPI_CRM_xts_businessunitinquiry_Create = "WebAPI_CRM_xts_businessunitinquiry_Create";
            public const string WebAPI_CRM_xts_businessunitinquiry_Delete = "WebAPI_CRM_xts_businessunitinquiry_Delete";
            public const string WebAPI_CRM_xts_businessunitinquiry_Read = "WebAPI_CRM_xts_businessunitinquiry_Read";
            public const string WebAPI_CRM_xts_businessunitinquiry_Update = "WebAPI_CRM_xts_businessunitinquiry_Update";
            public const string WebAPI_CRM_xts_businessunitinquiry_CreateList = "WebAPI_CRM_xts_businessunitinquiry_CreateList";

            public const string WebAPI_CRM_xts_landedcost_Create = "WebAPI_CRM_xts_landedcost_Create";
            public const string WebAPI_CRM_xts_landedcost_Delete = "WebAPI_CRM_xts_landedcost_Delete";
            public const string WebAPI_CRM_xts_landedcost_Read = "WebAPI_CRM_xts_landedcost_Read";
            public const string WebAPI_CRM_xts_landedcost_Update = "WebAPI_CRM_xts_landedcost_Update";
            public const string WebAPI_CRM_xts_landedcost_CreateList = "WebAPI_CRM_xts_landedcost_CreateList";

            public const string WebAPI_CRM_xts_moreaddress_Create = "WebAPI_CRM_xts_moreaddress_Create";
            public const string WebAPI_CRM_xts_moreaddress_Delete = "WebAPI_CRM_xts_moreaddress_Delete";
            public const string WebAPI_CRM_xts_moreaddress_Read = "WebAPI_CRM_xts_moreaddress_Read";
            public const string WebAPI_CRM_xts_moreaddress_Update = "WebAPI_CRM_xts_moreaddress_Update";
            public const string WebAPI_CRM_xts_moreaddress_CreateList = "WebAPI_CRM_xts_moreaddress_CreateList";

            public const string WebAPI_CRM_xts_outsourcereservation_Create = "WebAPI_CRM_xts_outsourcereservation_Create";
            public const string WebAPI_CRM_xts_outsourcereservation_Delete = "WebAPI_CRM_xts_outsourcereservation_Delete";
            public const string WebAPI_CRM_xts_outsourcereservation_Read = "WebAPI_CRM_xts_outsourcereservation_Read";
            public const string WebAPI_CRM_xts_outsourcereservation_Update = "WebAPI_CRM_xts_outsourcereservation_Update";
            public const string WebAPI_CRM_xts_outsourcereservation_CreateList = "WebAPI_CRM_xts_outsourcereservation_CreateList";

            public const string WebAPI_CRM_xts_outsourceworkorderreceiptdetail_Create = "WebAPI_CRM_xts_outsourceworkorderreceiptdetail_Create";
            public const string WebAPI_CRM_xts_outsourceworkorderreceiptdetail_Delete = "WebAPI_CRM_xts_outsourceworkorderreceiptdetail_Delete";
            public const string WebAPI_CRM_xts_outsourceworkorderreceiptdetail_Read = "WebAPI_CRM_xts_outsourceworkorderreceiptdetail_Read";
            public const string WebAPI_CRM_xts_outsourceworkorderreceiptdetail_Update = "WebAPI_CRM_xts_outsourceworkorderreceiptdetail_Update";
            public const string WebAPI_CRM_xts_outsourceworkorderreceiptdetail_CreateList = "WebAPI_CRM_xts_outsourceworkorderreceiptdetail_CreateList";

            public const string WebAPI_CRM_xts_outsourceworkshopconfiguration_Create = "WebAPI_CRM_xts_outsourceworkshopconfiguration_Create";
            public const string WebAPI_CRM_xts_outsourceworkshopconfiguration_Delete = "WebAPI_CRM_xts_outsourceworkshopconfiguration_Delete";
            public const string WebAPI_CRM_xts_outsourceworkshopconfiguration_Read = "WebAPI_CRM_xts_outsourceworkshopconfiguration_Read";
            public const string WebAPI_CRM_xts_outsourceworkshopconfiguration_Update = "WebAPI_CRM_xts_outsourceworkshopconfiguration_Update";
            public const string WebAPI_CRM_xts_outsourceworkshopconfiguration_CreateList = "WebAPI_CRM_xts_outsourceworkshopconfiguration_CreateList";

            public const string WebAPI_CRM_xts_product_Create = "WebAPI_CRM_xts_product_Create";
            public const string WebAPI_CRM_xts_product_Delete = "WebAPI_CRM_xts_product_Delete";
            public const string WebAPI_CRM_xts_product_Read = "WebAPI_CRM_xts_product_Read";
            public const string WebAPI_CRM_xts_product_Update = "WebAPI_CRM_xts_product_Update";
            public const string WebAPI_CRM_xts_product_CreateList = "WebAPI_CRM_xts_product_CreateList";

            public const string WebAPI_CRM_xts_servicemms_Create = "WebAPI_CRM_xts_servicemms_Create";
            public const string WebAPI_CRM_xts_servicemms_Delete = "WebAPI_CRM_xts_servicemms_Delete";
            public const string WebAPI_CRM_xts_servicemms_Read = "WebAPI_CRM_xts_servicemms_Read";
            public const string WebAPI_CRM_xts_servicemms_Update = "WebAPI_CRM_xts_servicemms_Update";
            public const string WebAPI_CRM_xts_servicemms_CreateList = "WebAPI_CRM_xts_servicemms_CreateList";

            public const string WebAPI_CRM_xts_serviceproportionalinvoice_Create = "WebAPI_CRM_xts_serviceproportionalinvoice_Create";
            public const string WebAPI_CRM_xts_serviceproportionalinvoice_Delete = "WebAPI_CRM_xts_serviceproportionalinvoice_Delete";
            public const string WebAPI_CRM_xts_serviceproportionalinvoice_Read = "WebAPI_CRM_xts_serviceproportionalinvoice_Read";
            public const string WebAPI_CRM_xts_serviceproportionalinvoice_Update = "WebAPI_CRM_xts_serviceproportionalinvoice_Update";
            public const string WebAPI_CRM_xts_serviceproportionalinvoice_CreateList = "WebAPI_CRM_xts_serviceproportionalinvoice_CreateList";

            public const string WebAPI_CRM_xts_siteinquiry_Create = "WebAPI_CRM_xts_siteinquiry_Create";
            public const string WebAPI_CRM_xts_siteinquiry_Delete = "WebAPI_CRM_xts_siteinquiry_Delete";
            public const string WebAPI_CRM_xts_siteinquiry_Read = "WebAPI_CRM_xts_siteinquiry_Read";
            public const string WebAPI_CRM_xts_siteinquiry_Update = "WebAPI_CRM_xts_siteinquiry_Update";
            public const string WebAPI_CRM_xts_siteinquiry_CreateList = "WebAPI_CRM_xts_siteinquiry_CreateList";

            public const string WebAPI_CRM_xts_warehouseinquiry_Create = "WebAPI_CRM_xts_warehouseinquiry_Create";
            public const string WebAPI_CRM_xts_warehouseinquiry_Delete = "WebAPI_CRM_xts_warehouseinquiry_Delete";
            public const string WebAPI_CRM_xts_warehouseinquiry_Read = "WebAPI_CRM_xts_warehouseinquiry_Read";
            public const string WebAPI_CRM_xts_warehouseinquiry_Update = "WebAPI_CRM_xts_warehouseinquiry_Update";
            public const string WebAPI_CRM_xts_warehouseinquiry_CreateList = "WebAPI_CRM_xts_warehouseinquiry_CreateList";

            public const string WebAPI_CRM_xts_workorderpartmaterialandservice_Create = "WebAPI_CRM_xts_workorderpartmaterialandservice_Create";
            public const string WebAPI_CRM_xts_workorderpartmaterialandservice_Delete = "WebAPI_CRM_xts_workorderpartmaterialandservice_Delete";
            public const string WebAPI_CRM_xts_workorderpartmaterialandservice_Read = "WebAPI_CRM_xts_workorderpartmaterialandservice_Read";
            public const string WebAPI_CRM_xts_workorderpartmaterialandservice_Update = "WebAPI_CRM_xts_workorderpartmaterialandservice_Update";
            public const string WebAPI_CRM_xts_workorderpartmaterialandservice_CreateList = "WebAPI_CRM_xts_workorderpartmaterialandservice_CreateList";

            public const string WebAPI_CRM_xts_inventserial_Create = "WebAPI_CRM_xts_inventserial_Create";
            public const string WebAPI_CRM_xts_inventserial_Delete = "WebAPI_CRM_xts_inventserial_Delete";
            public const string WebAPI_CRM_xts_inventserial_Read = "WebAPI_CRM_xts_inventserial_Read";
            public const string WebAPI_CRM_xts_inventserial_Update = "WebAPI_CRM_xts_inventserial_Update";
            public const string WebAPI_CRM_xts_inventserial_CreateList = "WebAPI_CRM_xts_inventserial_CreateList";

            public const string WebAPI_CRM_xts_site_Create = "WebAPI_CRM_xts_site_Create";
            public const string WebAPI_CRM_xts_site_Delete = "WebAPI_CRM_xts_site_Delete";
            public const string WebAPI_CRM_xts_site_Read = "WebAPI_CRM_xts_site_Read";
            public const string WebAPI_CRM_xts_site_Update = "WebAPI_CRM_xts_site_Update";
            public const string WebAPI_CRM_xts_site_CreateList = "WebAPI_CRM_xts_site_CreateList";

            public const string WebAPI_CRM_xts_vehicleprice_Create = "WebAPI_CRM_xts_vehicleprice_Create";
            public const string WebAPI_CRM_xts_vehicleprice_Delete = "WebAPI_CRM_xts_vehicleprice_Delete";
            public const string WebAPI_CRM_xts_vehicleprice_Read = "WebAPI_CRM_xts_vehicleprice_Read";
            public const string WebAPI_CRM_xts_vehicleprice_Update = "WebAPI_CRM_xts_vehicleprice_Update";
            public const string WebAPI_CRM_xts_vehicleprice_CreateList = "WebAPI_CRM_xts_vehicleprice_CreateList";

            public const string WebAPI_CRM_xts_vehiclepricedetail_Create = "WebAPI_CRM_xts_vehiclepricedetail_Create";
            public const string WebAPI_CRM_xts_vehiclepricedetail_Delete = "WebAPI_CRM_xts_vehiclepricedetail_Delete";
            public const string WebAPI_CRM_xts_vehiclepricedetail_Read = "WebAPI_CRM_xts_vehiclepricedetail_Read";
            public const string WebAPI_CRM_xts_vehiclepricedetail_Update = "WebAPI_CRM_xts_vehiclepricedetail_Update";
            public const string WebAPI_CRM_xts_vehiclepricedetail_CreateList = "WebAPI_CRM_xts_vehiclepricedetail_CreateList";

            public const string WebAPI_CRM_xts_warehouse_Create = "WebAPI_CRM_xts_warehouse_Create";
            public const string WebAPI_CRM_xts_warehouse_Delete = "WebAPI_CRM_xts_warehouse_Delete";
            public const string WebAPI_CRM_xts_warehouse_Read = "WebAPI_CRM_xts_warehouse_Read";
            public const string WebAPI_CRM_xts_warehouse_Update = "WebAPI_CRM_xts_warehouse_Update";
            public const string WebAPI_CRM_xts_warehouse_CreateList = "WebAPI_CRM_xts_warehouse_CreateList";

            public const string WebAPI_CRM_xts_nvsonumberregistrationdetails_Create = "WebAPI_CRM_xts_nvsonumberregistrationdetails_Create";
            public const string WebAPI_CRM_xts_nvsonumberregistrationdetails_Delete = "WebAPI_CRM_xts_nvsonumberregistrationdetails_Delete";
            public const string WebAPI_CRM_xts_nvsonumberregistrationdetails_Read = "WebAPI_CRM_xts_nvsonumberregistrationdetails_Read";
            public const string WebAPI_CRM_xts_nvsonumberregistrationdetails_Update = "WebAPI_CRM_xts_nvsonumberregistrationdetails_Update";
            public const string WebAPI_CRM_xts_nvsonumberregistrationdetails_CreateList = "WebAPI_CRM_xts_nvsonumberregistrationdetails_CreateList";

            public const string WebAPI_CRM_xts_outsourceworkorderdetail_Create = "WebAPI_CRM_xts_outsourceworkorderdetail_Create";
            public const string WebAPI_CRM_xts_outsourceworkorderdetail_Delete = "WebAPI_CRM_xts_outsourceworkorderdetail_Delete";
            public const string WebAPI_CRM_xts_outsourceworkorderdetail_Read = "WebAPI_CRM_xts_outsourceworkorderdetail_Read";
            public const string WebAPI_CRM_xts_outsourceworkorderdetail_Update = "WebAPI_CRM_xts_outsourceworkorderdetail_Update";
            public const string WebAPI_CRM_xts_outsourceworkorderdetail_CreateList = "WebAPI_CRM_xts_outsourceworkorderdetail_CreateList";

            public const string WebAPI_CRM_xts_servicecampaign_Create = "WebAPI_CRM_xts_servicecampaign_Create";
            public const string WebAPI_CRM_xts_servicecampaign_Delete = "WebAPI_CRM_xts_servicecampaign_Delete";
            public const string WebAPI_CRM_xts_servicecampaign_Read = "WebAPI_CRM_xts_servicecampaign_Read";
            public const string WebAPI_CRM_xts_servicecampaign_Update = "WebAPI_CRM_xts_servicecampaign_Update";
            public const string WebAPI_CRM_xts_servicecampaign_CreateList = "WebAPI_CRM_xts_servicecampaign_CreateList";

            public const string WebAPI_CRM_xts_vehiclepublic_Create = "WebAPI_CRM_xts_vehiclepublic_Create";
            public const string WebAPI_CRM_xts_vehiclepublic_Delete = "WebAPI_CRM_xts_vehiclepublic_Delete";
            public const string WebAPI_CRM_xts_vehiclepublic_Read = "WebAPI_CRM_xts_vehiclepublic_Read";
            public const string WebAPI_CRM_xts_vehiclepublic_Update = "WebAPI_CRM_xts_vehiclepublic_Update";
            public const string WebAPI_CRM_xts_vehiclepublic_CreateList = "WebAPI_CRM_xts_vehiclepublic_CreateList";

            public const string WebAPI_CRM_contact_Create = "WebAPI_CRM_contact_Create";
            public const string WebAPI_CRM_contact_Delete = "WebAPI_CRM_contact_Delete";
            public const string WebAPI_CRM_contact_Read = "WebAPI_CRM_contact_Read";
            public const string WebAPI_CRM_contact_Update = "WebAPI_CRM_contact_Update";
            public const string WebAPI_CRM_contact_CreateList = "WebAPI_CRM_contact_CreateList";

            public const string WebAPI_CRM_bookableresourcebooking_Create = "WebAPI_CRM_bookableresourcebooking_Create";
            public const string WebAPI_CRM_bookableresourcebooking_Delete = "WebAPI_CRM_bookableresourcebooking_Delete";
            public const string WebAPI_CRM_bookableresourcebooking_Read = "WebAPI_CRM_bookableresourcebooking_Read";
            public const string WebAPI_CRM_bookableresourcebooking_Update = "WebAPI_CRM_bookableresourcebooking_Update";
            public const string WebAPI_CRM_bookableresourcebooking_CreateList = "WebAPI_CRM_bookableresourcebooking_CreateList";

            #endregion
            #region FinishUnit
            public const string WebAPI_FinishUnit_DSFLeasingClaim_Update = "WebAPI_FinishUnit_DSFLeasingClaim_Update";
            public const string WebAPI_FinishUnit_LeasingDAPP_Read = "WebAPI_FinishUnit_LeasingDAPP_Read";
            public const string WebAPI_FinishUnit_DSFLeasingClaim_Create = "WebAPI_FinishUnit_DSFLeasingClaim_Create";
            public const string WebAPI_FinishUnit_DSFLeasingClaim_GetFile = "WebAPI_FinishUnit_DSFLeasingClaim_GetFile";
            //CR DSF
            public const string WebAPI_FinishUnit_Payment_Read = "WebAPI_FinishUnit_Payment_Read";
            public const string WebAPI_FinishUnit_Payment_Update = "WebAPI_FinishUnit_Payment_Update";
            public const string WebAPI_FinishUnit_Ceilling_Read = "WebAPI_FinishUnit_Ceilling_Read";
            public const string WebAPI_FinishUnit_Ceilling_Update = "WebAPI_FinishUnit_Ceilling_Update";
            
            //
            #endregion
        }

        public static readonly List<string> RestrictedPermissions = new List<string>()  
        {
            // MsApplication
            Constants.Permissions.WebUI_MsApplication_Read,
            Constants.Permissions.WebUI_MsApplication_Create,
            Constants.Permissions.WebUI_MsApplication_Update,
            Constants.Permissions.WebUI_MsApplication_Delete,

            // MsAppVersion
            Constants.Permissions.WebUI_MsAppVersion_Create,
            Constants.Permissions.WebUI_MsAppVersion_Update,
            Constants.Permissions.WebUI_MsAppVersion_Delete,

            //Permission
            Constants.Permissions.WebUI_Permission_Create,
            Constants.Permissions.WebUI_Permission_Update,
            Constants.Permissions.WebUI_Permission_Delete,

            // Role
            Constants.Permissions.WebUI_Role_Create,
            Constants.Permissions.WebUI_Role_Update,
            Constants.Permissions.WebUI_Role_Delete,


            // Client
            Constants.Permissions.WebUI_Client_Create,
            Constants.Permissions.WebUI_Client_Update,
            Constants.Permissions.WebUI_Client_Delete,

            // Deployment & Rollback
            Constants.Permissions.WebUI_JenkinsJob_Read,
            Constants.Permissions.WebUI_JenkinsJob_ViewOutput,
            Constants.Permissions.WebUI_JenkinsJob_Deploy,

            Constants.Permissions.WebUI_AppConfig_Create,
            Constants.Permissions.WebUI_AppConfig_Delete,
            Constants.Permissions.WebUI_AppConfig_Update,

            // Schedule
            Constants.Permissions.WebUI_Schedule_Create,
            Constants.Permissions.WebUI_Schedule_Delete,
            Constants.Permissions.WebUI_Schedule_Update,

            // EndpointSchedule
            Constants.Permissions.WebUI_EndpointSchedule_Create,
            Constants.Permissions.WebUI_EndpointSchedule_Delete,
            Constants.Permissions.WebUI_EndpointSchedule_Update,

            // Throttle
            Constants.Permissions.WebUI_Throttle_Create,
            Constants.Permissions.WebUI_Throttle_Delete,
            Constants.Permissions.WebUI_Throttle_Export,
            Constants.Permissions.WebUI_Throttle_Update,

        };
    }
}
