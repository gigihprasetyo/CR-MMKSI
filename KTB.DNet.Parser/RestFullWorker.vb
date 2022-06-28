
#Region "System Imports"
Imports System
Imports System.IO
Imports System.Diagnostics
Imports System.Threading
Imports System.Configuration
Imports System.Security.Principal
#End Region

#Region "Custom Imports"
Imports KTB.DNet.Parser
Imports KTB.DNet.Utility
#End Region


Namespace KTB.DNet.Parser

    Public Class RestFullWorker

#Region "Variable Declaration"
        '+++++++++++++++++++++++++++++++++++++++++++++++++++++
        Private _log As EventLog
        Private _fileName As String
        Private _user As String = "System SAP"
        Private user As IPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
        Private _fullkeyname As String

        Private _ROW_LINEBREAKER As String = "\n"
        Private _COL_SEPARATOR As String = ";"
        Private _INDICATOR_KEY As String = "K"
        Private _INDICATOR_HEADER As String = "H"
        Private _INDICATOR_DETAIL As String = "D"

        Private _DEALERSTALLEQUIPMENT_NAME As String = String.Empty
        Private _DEALERFACILITY_NAME As String = String.Empty
        Private _SALESORGANIZATION_NAME As String = String.Empty
        Private _DEALERCONTACT_NAME As String = String.Empty


        Private _TRANSFERCEILING_NAME As String = String.Empty
        Private _TRANSFERCEILINGDETAIL_NAME As String = String.Empty
        Private _SOCHANGE_NAME As String = String.Empty
        Private _SOCREATE_NAME As String = String.Empty
        Private _TRANSPAYMENT_NAME As String = String.Empty
        Private _UPDATECEILING_NAME As String = String.Empty
        Private _TRANSACTUALDATE_NAME As String = String.Empty

        Private _SPDELIVERYORDER_NAME As String = String.Empty
        Private _SPDODELETE_NAME As String = String.Empty
        Private _SPPACKING_NAME As String = String.Empty
        Private _SPPRINTPACKING_NAME As String = String.Empty
        Private _SPBILLING_NAME As String = String.Empty
        Private _SPBILLINGDELETE_NAME As String = String.Empty
        Private _SPEXPEDITION_NAME As String = String.Empty
        Private _SPPAYMENT_NAME As String = String.Empty
        Private _SPSALESORDER_NAME As String = String.Empty
        Private _SPSODELETE_NAME As String = String.Empty
        Private _SPBILLINGCREATE_NAME As String = String.Empty
        Private _SPBILLINGUPDATE_NAME As String = String.Empty

        Private _PODESTINATION_NAME As String = String.Empty
        Private _LOGMODEL_NAME As String = String.Empty
        Private _LOGCOST_NAME As String = String.Empty
        Private _DCCREATE_NAME As String = String.Empty
        Private _DCCHANGE_NAME As String = String.Empty
        'Start  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20171206
        Private _MSPMASTER_NAME As String = String.Empty
        Private _MSPTYPE_NAME As String = String.Empty
        Private _MSPDURATIONPMKIND_NAME As String = String.Empty
        Private _MDPDAILYSTOCK_NAME As String = String.Empty
        Private _MDPDEALER_NAME As String = String.Empty
        Private _MSPTRANSPAYMENT_NAME As String = String.Empty
        Private _MDPVEHICLE_NAME As String = String.Empty
        'End  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20171206

        'MSPEXT'
        Private _MSPEXTACTUALDATE_NAME As String = String.Empty
        Private _MSPEXTPAYMENT_NAME As String = String.Empty
        Private _MSPEXTTYPE_NAME As String = String.Empty
        Private _MSPEXTMASTER_NAME As String = String.Empty
        Private _MSPEXTREGISTRATION_NAME As String = String.Empty
        Private _MSPEXFAKTURPAJAK_NAME As String = String.Empty

        'MSPExt Fleet
        Private _FLEETMASTER_NAME As String = String.Empty
        Private _FLEETTYPE_NAME As String = String.Empty

        'CR Centralized Master Data START
        Private _VECHILEMODEL_NAME As String = String.Empty
        Private _VECHILETYPE_NAME As String = String.Empty

        Private _PROVINCECITY_NAME As String = String.Empty
        Private _VEHICLEPRICE_NAME As String = String.Empty
        Private _SPAREPART_NAME As String = String.Empty
        Private _SPAREPARTPRICE_NAME As String = String.Empty
        Private _SPAREPARTMASTERALT_NAME As String = String.Empty

        Private _AREA_NAME As String = String.Empty
        Private _DEALERBRANCH_NAME As String = String.Empty
        'CR Centralized Master Data END

        'CR IR Start
        Private _KAROSERI_NAME As String = String.Empty
        Private _LEASING_NAME As String = String.Empty
        Private _REVISIONPRICE_NAME As String = String.Empty
        Private _REVISIONPAYMENTTRANS_NAME As String = String.Empty
        Private _REVISIONPAYMENTVIRTU_NAME As String = String.Empty
        Private _REVISIONPAYMENTGYRO_NAME As String = String.Empty
        Private _REVISIONPAYMENTJVCANCEL_NAME As String = String.Empty
        Private _REVISIONPAYMENTIRTRANSPAYMENT_NAME As String = String.Empty
        'CR IR END

        'Start Sparepart TOP
        Private _TOPSPBILLINGBLOCK_NAME As String = String.Empty
        Private _TOPSPTRANSCEILING_NAME As String = String.Empty
        Private _TOPSPTRANSCEILINGDETAIL_NAME As String = String.Empty
        Private _TOPSPUPDATECEILING_NAME As String = String.Empty
        Private _TOPSPBILLINGDEPOSIT_NAME As String = String.Empty
        Private _TOPTRANSACTUALDATE_NAME As String = String.Empty
        Private _TOPSPKLIRING_NAME As String = String.Empty
        Private _CODOUTSTANDING_NAME As String = String.Empty
        Private _CODPAYMENT_NAME As String = String.Empty
        Private _TOPSPPENALTY_NAME As String = String.Empty
        Private _TOPSPPENALTYUPDATE_NAME As String = String.Empty
        Private _TOPSPPENALTYJV_NAME As String = String.Empty
        Private _TOPSPPAYMENTOUTSTANDING_NAME As String = String.Empty
        'End Sparepart TOP

        '<!--Start Master Dealer Centrallized-->
        Private _MASTERDEALERGROUP_NAME As String = String.Empty
        Private _MASTERCREDITACCOUNT_NAME As String = String.Empty
        Private _MASTERTOP_NAME As String = String.Empty
        Private _MASTERDEALERTERRITORY_NAME As String = String.Empty
        Private _MASTERDEALER_NAME As String = String.Empty
        Private _MASTERVEHICLEKIND_NAME As String = String.Empty
        '<!--End Master Dealer Centrallized-->


        Private _MASTERINTEREST_NAME As String = String.Empty
        Private _MASTERINTERESTPO_NAME As String = String.Empty

        '<!--Start Babit -->
        Private _SBABITJV_NAME As String = String.Empty

        '<!--End Babit


        Private _ALLOCATEFSKIND_NAME As String = String.Empty
        Private _FSKIND_NAME As String = String.Empty

        Private _SPAREPARTNOTARETUR_NAME As String = String.Empty

        Private _BILLINGAPOUTSTANDING_NAME As String = String.Empty


#End Region

#Region "Declar Parser"
        Private _MSPExtTypeParser As MSPExtTypeParser
        Private _MSPExtMasterParser As MSPExtMasterParser
        Private _MSPExtActualDateParser As MSPExtActualDateParser
        Private _MSPExtPaymentParser As MSPExtPaymentParser
        Private _MSPEXRegistrationParser As MSPEXRegistrationParser
        Private _MSPEXFakturPajakParser As MSPEXFakturPajakParser

        'Start  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20171206

        Private _StallEquipmentParser As DealerStallEquipmentParser
        Private _DealerFacilityParser As DealerFacilityParser
        Private _SalesOrganizationParser As DealerSalesOrganizationParser
        Private _DealerContactParser As DealerContactParser

        Private _MSPMaster As MSPMasterParser
        Private _MSPType As MSPTypeParser
        Private _MSPDurationPMKind As MSPDurationPMKindParser
        Private _MSPTransferPaymentParser As MSPTransferPaymentParser
        'End  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20171206
        Private _MDPDailyStockParser As MDPDailyStockParser
        Private _MDPMasterDealerParser As MDPMasterDealerParser

        Private _TransferCeilingParser As TransferCeilingParser
        Private _TransferCeilingDetailParser As TransferCeilingDetailParser
        Private _SOCreateParser As SOCreateParser
        Private _SOChangeParser As SOChangeParser
        Private _TransferPaymentParser As TransferPaymentParser

        Private _SparePartDOParser As SparePartDOParser
        Private _SparePartDODeleteParser As SparePartDODeleteParser
        Private _SparePartPackingParser As SparePartPackingParser
        Private _SparePartPrintPackingParser As SparePartPrintPackingParser
        Private _SparePartBillingParser As SparePartBillingParser
        Private _SparePartBillingDeleteParser As SparePartBillingDeleteParser
        Private _SparePartBillingCreateParser As BillingCreateParser
        Private _SparePartBillingUpdateParser As BillingUpdateParser
        Private _SparePartDOExpeditionParser As SparePartDOExpeditionParser
        Private _SparePartPaymentParser As SparePartPaymentParser
        Private _SparePartSOParser As SparePartSOParser
        Private _SparePartSODeleteParser As SparePartSODeleteParser

        Private _PODestinationParser As PODestinationParser
        Private _CeilingUpdateParser As CeilingUpdateParser
        Private _PaymentTransferActualDate As TransferPaymentActualDateParser

        Private _LogModelParser As LogModelParser
        Private _LogCostParser As LogCostParser
        Private _DCCreateParser As DCCreateParser

        'CR Centralized Master Data START
        Private _vehicleModelParser As VechileModelParser
        Private _provinceCityParser As ProvinceCityParser

        Private _vehicleTypeParser As VechileTypeParser
        Private _vehiclePriceParser As VehiclePriceParser
        Private _sparePartParser As SparePartParser
        Private _sparePartPriceParser As SparePartPriceParser
        Private _sparePartMasterAltParser As SparePartMasterAltParser

        Private _areaParser As AreaParser
        Private _dealerBranchParser As DealerBranchParser
        'CR Centralized Master Data END

        'CR IR Start
        Private _Karoseri As KaroseriParser
        Private _Leasing As LeasingParser
        Private _RevisionPrice As RevisionPriceParser
        Private _RevisionPaymentTrans As RevisionPaymentTransParser
        Private _RevisionPaymentVirtu As RevisionPaymentVirtuParser
        Private _RevisionPaymentGyro As RevisionPaymentGyroParser
        Private _RevisionPaymentJVCancel As RevisionPaymentJVCancelParser
        Private _RevisionPaymentIRTransPayment As RevisionPaymentIRTransPaymentParser
        ' CR IR End


        'Start Sparepart TOP
        Private _TOPSPBillingBlockParser As TOPSPBillingBlockParser
        Private _TOPSPTransferCeilingParser As TOPSPTransferCeilingParser
        Private _TOPSPTransferCeilingDetailParser As TOPSPTransferCeilingDetailParser
        Private _TOPSPTransferCeilingUpdateParser As TOPSPTransferCeilingUpdateParser
        Private _TOPSPBillingDepositParser As TOPSPBillingDepositParser
        Private _TOPTranActualDateParser As TOPTranActualDateParser
        Private _TOPSPKliringParser As TOPSPKliringParser
        Private _CODOutstandingParser As CODOutstandingParser
        Private _CODPaymentParser As CODPaymentParser
        Private _TOPSPPenaltyParser As TOPSPPenaltyParser
        Private _TOPSPPenaltyUpdateParser As TOPSPPenaltyUpdateParser
        Private _TOPSPPenaltyUpdateJVParser As TOPSPPenaltyUpdateJVParser
        Private _TOPSPPaymentOutstandingParser As TOPSPPaymentOutstandingParser
        'End Sparepart TOP

        '<!--Start Master Dealer Centralize-->
        Private _MasterDealerGroupParser As MasterDealerGroupParser
        Private _MasterCreditAccountParser As MasterCreditAccountParser
        Private _MasterTOPParser As MasterTOPParser
        Private _MasterDealerTerritoryParser As MasterDealerTerritoryParser
        Private _MasterDealerParser As MasterDealerParser
        Private _MasterVehicleKindParser As MasterVehicleKindParser
        '<!--End Master Dealer Centralize-->


        Private _MasterInterestParser As MasterInterestParser
        Private _MasterInterestPOParser As DealerPOTargetParser

        Private _MDPMasterVehicleParser As MDPMasterVehicleParser

        '<!-- Start Babit -->
        Private _BabitParser As BabitParser

        '<!-- End Babit -->

        Private _AllocateFSKINDParser As AllocateFSKINDParser
        Private _FSKINDParser As FSKINDParser

        Private _SparepartNotaReturParser As SparepartNotaReturParser

        Private _BillingAPOutstandingParser As BillingAPOutstandingParser
#End Region

        Private Sub InitKeyName()
            _fullkeyname = ""
            _ROW_LINEBREAKER = System.Configuration.ConfigurationManager.AppSettings("ROW_LINEBREAKER").ToString()
            _COL_SEPARATOR = System.Configuration.ConfigurationManager.AppSettings("COL_SEPARATOR").ToString()

            _INDICATOR_KEY = System.Configuration.ConfigurationManager.AppSettings("INDICATOR_KEY").ToString()
            _INDICATOR_HEADER = System.Configuration.ConfigurationManager.AppSettings("INDICATOR_HEADER").ToString()
            _INDICATOR_DETAIL = System.Configuration.ConfigurationManager.AppSettings("INDICATOR_DETAIL").ToString()

            _TRANSFERCEILING_NAME = System.Configuration.ConfigurationManager.AppSettings("TRANSFERCEILING_NAME").ToString().ToUpper()
            _TRANSFERCEILINGDETAIL_NAME = System.Configuration.ConfigurationManager.AppSettings("TRANSFERCEILINGDETAIL_NAME").ToString().ToUpper()
            _SOCHANGE_NAME = System.Configuration.ConfigurationManager.AppSettings("SOCHANGE_NAME").ToString().ToUpper()
            _SOCREATE_NAME = System.Configuration.ConfigurationManager.AppSettings("SOCREATE_NAME").ToString().ToUpper()
            _TRANSACTUALDATE_NAME = System.Configuration.ConfigurationManager.AppSettings("TRANSACTUALDATE_NAME").ToString().ToUpper()

            _TRANSPAYMENT_NAME = System.Configuration.ConfigurationManager.AppSettings("TRANSPAYMENT_NAME").ToString().ToUpper()

            _SPDELIVERYORDER_NAME = System.Configuration.ConfigurationManager.AppSettings("SPDELIVERYORDER_NAME").ToString().ToUpper()
            _SPDODELETE_NAME = System.Configuration.ConfigurationManager.AppSettings("SPDODELETE_NAME").ToString().ToUpper()
            _SPPACKING_NAME = System.Configuration.ConfigurationManager.AppSettings("SPPACKING_NAME").ToString().ToUpper()
            _SPPRINTPACKING_NAME = System.Configuration.ConfigurationManager.AppSettings("SPPRINTPACKING_NAME").ToString().ToUpper()
            _SPBILLING_NAME = System.Configuration.ConfigurationManager.AppSettings("SPBILLING_NAME").ToString().ToUpper()
            _SPBILLINGDELETE_NAME = System.Configuration.ConfigurationManager.AppSettings("SPBILLINGDELETE_NAME").ToString().ToUpper()
            _SPEXPEDITION_NAME = System.Configuration.ConfigurationManager.AppSettings("SPEXPEDITION_NAME").ToString().ToUpper()
            _SPPAYMENT_NAME = System.Configuration.ConfigurationManager.AppSettings("SPPAYMENT_NAME").ToString().ToUpper()
            _SPSALESORDER_NAME = System.Configuration.ConfigurationManager.AppSettings("SPSALESORDER_NAME").ToString().ToUpper()
            _SPSODELETE_NAME = System.Configuration.ConfigurationManager.AppSettings("SPSODELETE_NAME").ToString().ToUpper()

            _SPBILLINGCREATE_NAME = System.Configuration.ConfigurationManager.AppSettings("BILLINGCREATE_NAME").ToString().ToUpper()
            _SPBILLINGUPDATE_NAME = System.Configuration.ConfigurationManager.AppSettings("BILLINGUPDATE_NAME").ToString().ToUpper()


            _PODESTINATION_NAME = System.Configuration.ConfigurationManager.AppSettings("PODESTINATION_NAME").ToString().ToUpper()

            _MDPDAILYSTOCK_NAME = System.Configuration.ConfigurationManager.AppSettings("MDPDAILYSTOCK_NAME").ToString().ToUpper()
            _MDPDEALER_NAME = System.Configuration.ConfigurationManager.AppSettings("MDPDEALER_NAME").ToString().ToUpper()

            _UPDATECEILING_NAME = System.Configuration.ConfigurationManager.AppSettings("UPDATECEILING_NAME").ToString().ToUpper()

            _LOGMODEL_NAME = System.Configuration.ConfigurationManager.AppSettings("LOGMODEL_NAME").ToString().ToUpper()
            _LOGCOST_NAME = System.Configuration.ConfigurationManager.AppSettings("LOGCOST_NAME").ToString().ToUpper()
            _DCCREATE_NAME = System.Configuration.ConfigurationManager.AppSettings("DCCREATE_NAME").ToString().ToUpper()
            _DCCHANGE_NAME = System.Configuration.ConfigurationManager.AppSettings("DCCHANGE_NAME").ToString().ToUpper()

            'Start  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20171206
            _MSPMASTER_NAME = System.Configuration.ConfigurationManager.AppSettings("MSPMASTER_NAME").ToString().ToUpper()
            _MSPTYPE_NAME = System.Configuration.ConfigurationManager.AppSettings("MSPTYPE_NAME").ToString().ToUpper()
            _MSPDURATIONPMKIND_NAME = System.Configuration.ConfigurationManager.AppSettings("MSPDURATIONPMKIND_NAME").ToString().ToUpper()
            _MSPTRANSPAYMENT_NAME = System.Configuration.ConfigurationManager.AppSettings("MSPTRANSPAYMENT_NAME").ToString().ToUpper()
            'End  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20171206

            _MSPEXTTYPE_NAME = System.Configuration.ConfigurationManager.AppSettings("MSPEXTTYPE_NAME").ToString().ToUpper()
            _MSPEXTMASTER_NAME = System.Configuration.ConfigurationManager.AppSettings("MSPEXTMASTER_NAME").ToString().ToUpper()
            _MSPEXTACTUALDATE_NAME = System.Configuration.ConfigurationManager.AppSettings("MSPEXTACTUALDATE_NAME").ToString().ToUpper()
            _MSPEXTPAYMENT_NAME = System.Configuration.ConfigurationManager.AppSettings("MSPEXTPAYMENT_NAME").ToString().ToUpper()
            _MSPEXTREGISTRATION_NAME = System.Configuration.ConfigurationManager.AppSettings("MSPEXTREGISTRATION_NAME").ToString().ToUpper()
            _MSPEXFAKTURPAJAK_NAME = System.Configuration.ConfigurationManager.AppSettings("MSPEXFAKTURPAJAK_NAME").ToString().ToUpper()

            'MSP Ext Fleet
            _FLEETMASTER_NAME = System.Configuration.ConfigurationManager.AppSettings("FLEETMASTER_NAME").ToString().ToUpper()
            _FLEETTYPE_NAME = System.Configuration.ConfigurationManager.AppSettings("FLEETTYPE_NAME").ToString().ToUpper()

            'Untuk DNET CR Centralized Master Data; Date: 20180821
            _VECHILEMODEL_NAME = System.Configuration.ConfigurationManager.AppSettings("VEHICLEMODEL_NAME").ToString().ToUpper()
            _PROVINCECITY_NAME = System.Configuration.ConfigurationManager.AppSettings("PROVINCECITY_NAME").ToString().ToUpper()
            _VECHILETYPE_NAME = System.Configuration.ConfigurationManager.AppSettings("VEHICLETYPE_NAME").ToString().ToUpper()
            _VEHICLEPRICE_NAME = System.Configuration.ConfigurationManager.AppSettings("VEHICLEPRICE_NAME").ToString().ToUpper()
            _SPAREPART_NAME = System.Configuration.ConfigurationManager.AppSettings("SPAREPART_NAME").ToString().ToUpper()
            _SPAREPARTPRICE_NAME = System.Configuration.ConfigurationManager.AppSettings("SPAREPARTPRICE_NAME").ToString().ToUpper()
            _SPAREPARTMASTERALT_NAME = System.Configuration.ConfigurationManager.AppSettings("SPAREPARTMASTERALT_NAME").ToString().ToUpper()
            _AREA_NAME = System.Configuration.ConfigurationManager.AppSettings("AREA_NAME").ToString().ToUpper()
            _DEALERBRANCH_NAME = System.Configuration.ConfigurationManager.AppSettings("DEALERBRANCH_NAME").ToString().ToUpper()
            'CR Centralize END
            'CR IR Start
            _KAROSERI_NAME = System.Configuration.ConfigurationManager.AppSettings("KAROSERI_NAME").ToString().ToUpper()
            _LEASING_NAME = System.Configuration.ConfigurationManager.AppSettings("LEASING_NAME").ToString().ToUpper()
            _REVISIONPRICE_NAME = System.Configuration.ConfigurationManager.AppSettings("REVISIONPRICE_NAME").ToString().ToUpper()
            _REVISIONPAYMENTTRANS_NAME = System.Configuration.ConfigurationManager.AppSettings("REVISIONPAYMENTTRANS_NAME").ToString().ToUpper()
            _REVISIONPAYMENTVIRTU_NAME = System.Configuration.ConfigurationManager.AppSettings("REVISIONPAYMENTVIRTU_NAME").ToString().ToUpper()
            _REVISIONPAYMENTGYRO_NAME = System.Configuration.ConfigurationManager.AppSettings("REVISIONPAYMENTGYRO_NAME").ToString().ToUpper()
            _REVISIONPAYMENTJVCANCEL_NAME = System.Configuration.ConfigurationManager.AppSettings("REVISIONPAYMENTJVCANCEL_NAME").ToString().ToUpper()
            _REVISIONPAYMENTIRTRANSPAYMENT_NAME = System.Configuration.ConfigurationManager.AppSettings("REVISIONPAYMENTIRTRANSPAYMENT_NAME").ToString().ToUpper()
            'CR IR END


            'Start SparePart TOP
            _TOPSPBILLINGBLOCK_NAME = System.Configuration.ConfigurationManager.AppSettings("TOPSPBILLINGBLOCK_NAME").ToString().ToUpper()
            _TOPSPTRANSCEILING_NAME = System.Configuration.ConfigurationManager.AppSettings("TOPSPTRANSCEILING_NAME").ToString().ToUpper()
            _TOPSPTRANSCEILINGDETAIL_NAME = System.Configuration.ConfigurationManager.AppSettings("TOPSPTRANSCEILINGDETAIL_NAME").ToString().ToUpper()
            _TOPSPUPDATECEILING_NAME = System.Configuration.ConfigurationManager.AppSettings("TOPSPUPDATECEILING_NAME").ToString().ToUpper()
            _TOPSPBILLINGDEPOSIT_NAME = System.Configuration.ConfigurationManager.AppSettings("TOPSPBILLINGDEPOSIT_NAME").ToString().ToUpper()
            _TOPTRANSACTUALDATE_NAME = System.Configuration.ConfigurationManager.AppSettings("TOPTRANSACTUALDATE_NAME").ToString().ToUpper()
            _TOPSPKLIRING_NAME = System.Configuration.ConfigurationManager.AppSettings("TOPSPKLIRING_NAME").ToString().ToUpper()
            _CODOUTSTANDING_NAME = System.Configuration.ConfigurationManager.AppSettings("CODOUTSTANDING_NAME").ToString().ToUpper()
            _CODPAYMENT_NAME = System.Configuration.ConfigurationManager.AppSettings("CODPAYMENT_NAME").ToString().ToUpper()
            _TOPSPPENALTY_NAME = System.Configuration.ConfigurationManager.AppSettings("TOPSPPENALTY_NAME").ToString().ToUpper()
            _TOPSPPENALTYUPDATE_NAME = System.Configuration.ConfigurationManager.AppSettings("TOPSPPENALTYUPDATE_NAME").ToString().ToUpper()
            _TOPSPPENALTYJV_NAME = System.Configuration.ConfigurationManager.AppSettings("TOPSPPENALTYJV_NAME").ToString().ToUpper()

            _TOPSPPAYMENTOUTSTANDING_NAME = System.Configuration.ConfigurationManager.AppSettings("TOPSPPAYMENTOUTSTANDING_NAME").ToString().ToUpper()
            'End

            _MASTERINTEREST_NAME = System.Configuration.ConfigurationManager.AppSettings("MASTERINTEREST_NAME").ToString().ToUpper()
            _MASTERINTERESTPO_NAME = System.Configuration.ConfigurationManager.AppSettings("MASTERINTERESTPO_NAME").ToString().ToUpper()

            _MDPVEHICLE_NAME = System.Configuration.ConfigurationManager.AppSettings("MDPVEHICLE_NAME").ToString().ToUpper()

            '<!--Start Master Dealer Centralilzed-->
            _MASTERDEALERGROUP_NAME = System.Configuration.ConfigurationManager.AppSettings("MASTERDEALERGROUP_NAME").ToString().ToUpper()
            _MASTERCREDITACCOUNT_NAME = System.Configuration.ConfigurationManager.AppSettings("MASTERCREDITACCOUNT_NAME").ToString().ToUpper()
            _MASTERTOP_NAME = System.Configuration.ConfigurationManager.AppSettings("MASTERTOP_NAME").ToString().ToUpper()
            _MASTERDEALERTERRITORY_NAME = System.Configuration.ConfigurationManager.AppSettings("MASTERDEALERTERRITORY_NAME").ToString().ToUpper()
            _MASTERDEALER_NAME = System.Configuration.ConfigurationManager.AppSettings("MASTERDEALER_NAME").ToString().ToUpper()
            _MASTERVEHICLEKIND_NAME = System.Configuration.ConfigurationManager.AppSettings("MASTERVEHICLEKIND_NAME").ToString().ToUpper()
            '<!--End Master Dealer Centralilzed-->

            '<!-- Start Babit -->
            _SBABITJV_NAME = System.Configuration.ConfigurationManager.AppSettings("SBABITJV_NAME").ToString().ToUpper()
            '<!-- End Babit -->


            _DEALERSTALLEQUIPMENT_NAME = System.Configuration.ConfigurationManager.AppSettings("DEALERSTALLEQUIPMENT_NAME").ToString().ToUpper()
            _DEALERFACILITY_NAME = System.Configuration.ConfigurationManager.AppSettings("DEALERFACILITY_NAME").ToString().ToUpper()
            _SALESORGANIZATION_NAME = System.Configuration.ConfigurationManager.AppSettings("SALESORGANIZATION_NAME").ToString().ToUpper()
            _DEALERCONTACT_NAME = System.Configuration.ConfigurationManager.AppSettings("DEALERCONTACT_NAME").ToString().ToUpper()

            _ALLOCATEFSKIND_NAME = System.Configuration.ConfigurationManager.AppSettings("ALLOCATEFSKIND_NAME").ToString().ToUpper()
            _FSKIND_NAME = System.Configuration.ConfigurationManager.AppSettings("FSKIND_NAME").ToString().ToUpper()

            _SPAREPARTNOTARETUR_NAME = System.Configuration.ConfigurationManager.AppSettings("SPAREPARTNOTARETUR_NAME").ToString().ToUpper()

            _BILLINGAPOUTSTANDING_NAME = System.Configuration.ConfigurationManager.AppSettings("BILLINGAPOUTSTANDING_NAME").ToString().ToUpper()


        End Sub
        Public Sub New()
            InitKeyName()
        End Sub

        Private Function GetKeyName(ByRef body As String) As String
            Dim idx As String = body.IndexOf(Me._ROW_LINEBREAKER)
            Dim line1 As String = body.Substring(0, idx)
            Dim cols As String() = line1.Split(Me._COL_SEPARATOR)
            Dim sKey As String

            If (cols.Length = 2) Then
                sKey = cols(0)
                If (sKey = Me._INDICATOR_KEY) Then
                    sKey = cols(1)
                    If (sKey.IndexOf("_") > 0) Then
                        sKey = sKey.Split("_")(0)
                    End If
                    Return sKey
                End If
            End If

            Return String.Empty
        End Function

        Private Function GetFullKeyName(ByRef body As String) As String
            Dim idx As String = body.IndexOf(Me._ROW_LINEBREAKER)
            Dim line1 As String = body.Substring(0, idx)
            Dim cols As String() = line1.Split(Me._COL_SEPARATOR)
            Dim sKey As String

            If (cols.Length = 2) Then
                sKey = cols(1)
                Return sKey
                If (sKey = Me._INDICATOR_KEY) Then
                    sKey = cols(1)
                    If (sKey.IndexOf("_") > 0) Then
                        sKey = sKey.Split("_")(0)
                    End If
                    Return sKey
                End If
            End If

            Return String.Empty
        End Function

        Private Function GetContent(ByRef body As String) As String
            Dim idx As String = body.IndexOf(Me._ROW_LINEBREAKER)

            Return body.Substring(idx + 2)
        End Function

        Private Function Distribute(ByVal KeyName As String, ByVal content As String, Optional ByVal fullKeyName As String = "") As Boolean
            Select Case KeyName.ToUpper()
                Case _SPBILLINGCREATE_NAME
                    _SparePartBillingCreateParser = New BillingCreateParser()
                    _SparePartBillingCreateParser.SourceName = KeyName
                    _SparePartBillingCreateParser.BlockName = "SparePart"
                    _SparePartBillingCreateParser.ParseWithTransactionWS(KeyName, content)
                    If Not _SparePartBillingCreateParser Is Nothing Then
                        _SparePartBillingCreateParser = Nothing
                    End If
                Case _SPBILLINGUPDATE_NAME
                    _SparePartBillingUpdateParser = New BillingUpdateParser()
                    _SparePartBillingUpdateParser.SourceName = KeyName
                    _SparePartBillingUpdateParser.BlockName = "SparePart"
                    _SparePartBillingUpdateParser.ParseWithTransactionWS(KeyName, content)
                    If Not _SparePartBillingUpdateParser Is Nothing Then
                        _SparePartBillingUpdateParser = Nothing
                    End If
                Case _TRANSFERCEILING_NAME
                    _TransferCeilingParser = New TransferCeilingParser()
                    _TransferCeilingParser.SourceName = KeyName
                    _TransferCeilingParser.BlockName = "FinishUnit"
                    _TransferCeilingParser.ParseWithTransactionWS(KeyName, content)
                    If Not _TransferCeilingParser Is Nothing Then
                        _TransferCeilingParser = Nothing
                    End If
                Case _TRANSFERCEILINGDETAIL_NAME
                    _TransferCeilingDetailParser = New TransferCeilingDetailParser()
                    _TransferCeilingDetailParser.SourceName = KeyName
                    _TransferCeilingDetailParser.BlockName = "FinishUnit"
                    _TransferCeilingDetailParser.ParseWithTransactionWS(KeyName, content)
                    If Not _TransferCeilingDetailParser Is Nothing Then
                        _TransferCeilingDetailParser = Nothing
                    End If
                Case _SOCREATE_NAME
                    _SOCreateParser = New SOCreateParser()
                    _SOCreateParser.SourceName = KeyName
                    _SOCreateParser.BlockName = "FinishUnit"
                    _SOCreateParser.ParseWithTransactionWS(KeyName, content)
                    If Not _SOCreateParser Is Nothing Then
                        _SOCreateParser = Nothing
                    End If
                Case _SOCHANGE_NAME
                    _SOChangeParser = New SOChangeParser()
                    _SOChangeParser.SourceName = KeyName
                    _SOChangeParser.BlockName = "FinishUnit"
                    _SOChangeParser.ParseWithTransactionWS(KeyName, content)
                    If Not _SOChangeParser Is Nothing Then
                        _SOChangeParser = Nothing
                    End If

                Case _TRANSPAYMENT_NAME
                    _TransferPaymentParser = New TransferPaymentParser()
                    _TransferPaymentParser.SourceName = KeyName
                    _TransferPaymentParser.BlockName = "FinishUnit"
                    _TransferPaymentParser.ParseWithTransactionWS(KeyName, content)
                    If Not _TransferPaymentParser Is Nothing Then
                        _TransferPaymentParser = Nothing
                    End If

                Case _SPDELIVERYORDER_NAME
                    _SparePartDOParser = New SparePartDOParser()
                    _SparePartDOParser.SourceName = KeyName
                    _SparePartDOParser.BlockName = "SparePart"
                    _SparePartDOParser.ParseWithTransactionWS(KeyName, content)
                    If Not _SparePartDOParser Is Nothing Then
                        _SparePartDOParser = Nothing
                    End If
                Case _SPDODELETE_NAME
                    _SparePartDODeleteParser = New SparePartDODeleteParser()
                    _SparePartDODeleteParser.SourceName = KeyName
                    _SparePartDODeleteParser.BlockName = "SparePart"
                    _SparePartDODeleteParser.ParseWithTransactionWS(KeyName, content)
                    If Not _SparePartDODeleteParser Is Nothing Then
                        _SparePartDODeleteParser = Nothing
                    End If
                Case _SPPACKING_NAME
                    _SparePartPackingParser = New SparePartPackingParser()
                    _SparePartPackingParser.SourceName = KeyName
                    _SparePartPackingParser.BlockName = "SparePart"
                    _SparePartPackingParser.ParseWithTransactionWS(KeyName, content)
                    If Not _SparePartPackingParser Is Nothing Then
                        _SparePartPackingParser = Nothing
                    End If
                Case _SPPRINTPACKING_NAME
                    _SparePartPrintPackingParser = New SparePartPrintPackingParser()
                    _SparePartPrintPackingParser.SourceName = KeyName
                    _SparePartPrintPackingParser.BlockName = "SparePart"
                    _SparePartPrintPackingParser.ParseWithTransactionWS(KeyName, content)
                    If Not _SparePartPrintPackingParser Is Nothing Then
                        _SparePartPrintPackingParser = Nothing
                    End If
                Case _SPBILLING_NAME
                    _SparePartBillingParser = New SparePartBillingParser()
                    _SparePartBillingParser.SourceName = KeyName
                    _SparePartBillingParser.BlockName = "SparePart"
                    _SparePartBillingParser.ParseWithTransactionWS(KeyName, content)
                    If Not _SparePartBillingParser Is Nothing Then
                        _SparePartBillingParser = Nothing
                    End If
                Case _SPBILLINGDELETE_NAME
                    _SparePartBillingDeleteParser = New SparePartBillingDeleteParser()
                    _SparePartBillingDeleteParser.SourceName = KeyName
                    _SparePartBillingDeleteParser.BlockName = "SparePart"
                    _SparePartBillingDeleteParser.ParseWithTransactionWS(KeyName, content)
                    If Not _SparePartBillingDeleteParser Is Nothing Then
                        _SparePartBillingDeleteParser = Nothing
                    End If
                Case _SPEXPEDITION_NAME
                    _SparePartDOExpeditionParser = New SparePartDOExpeditionParser()
                    _SparePartDOExpeditionParser.SourceName = KeyName
                    _SparePartDOExpeditionParser.BlockName = "SparePart"
                    _SparePartDOExpeditionParser.ParseWithTransactionWS(KeyName, content)
                    If Not _SparePartDOExpeditionParser Is Nothing Then
                        _SparePartDOExpeditionParser = Nothing
                    End If
                Case _SPPAYMENT_NAME
                    _SparePartPaymentParser = New SparePartPaymentParser()
                    _SparePartPaymentParser.SourceName = KeyName
                    _SparePartPaymentParser.BlockName = "SparePart"
                    _SparePartPaymentParser.ParseWithTransactionWS(KeyName, content)
                    If Not _SparePartPaymentParser Is Nothing Then
                        _SparePartPaymentParser = Nothing
                    End If
                Case _SPSALESORDER_NAME
                    _SparePartSOParser = New SparePartSOParser()
                    _SparePartSOParser.SourceName = KeyName
                    _SparePartSOParser.BlockName = "SparePart"
                    _SparePartSOParser.ParseWithTransactionWS(KeyName, content)
                    If Not _SparePartSOParser Is Nothing Then
                        _SparePartSOParser = Nothing
                    End If
                Case _SPSODELETE_NAME
                    _SparePartSODeleteParser = New SparePartSODeleteParser()
                    _SparePartSODeleteParser.SourceName = KeyName
                    _SparePartSODeleteParser.BlockName = "SparePart"
                    _SparePartSODeleteParser.ParseWithTransactionWS(KeyName, content)
                    If Not _SparePartSODeleteParser Is Nothing Then
                        _SparePartSODeleteParser = Nothing
                    End If

                Case _PODESTINATION_NAME
                    _PODestinationParser = New PODestinationParser()
                    _PODestinationParser.SourceName = KeyName
                    _PODestinationParser.BlockName = "FinishUnit"
                    _PODestinationParser.ParseWithTransactionWS(KeyName, content)
                    If Not _PODestinationParser Is Nothing Then
                        _PODestinationParser = Nothing
                    End If


                Case _UPDATECEILING_NAME
                    _CeilingUpdateParser = New CeilingUpdateParser(_fullkeyname)
                    _CeilingUpdateParser.SourceName = KeyName
                    _CeilingUpdateParser.BlockName = "FinishUnit"
                    _CeilingUpdateParser.ParseWithTransactionWS(KeyName, content)
                    If Not _CeilingUpdateParser Is Nothing Then
                        _CeilingUpdateParser = Nothing
                    End If


                Case _LOGMODEL_NAME
                    _LogModelParser = New LogModelParser()
                    _LogModelParser.SourceName = KeyName
                    _LogModelParser.BlockName = "FinishUnit"
                    _LogModelParser.ParseWithTransactionWS(KeyName, content)
                    If Not _LogModelParser Is Nothing Then
                        _LogModelParser = Nothing
                    End If



                Case _LOGCOST_NAME

                    _LogCostParser = New LogCostParser()
                    _LogCostParser.SourceName = KeyName
                    _LogCostParser.BlockName = "FinishUnit"
                    _LogCostParser.ParseWithTransactionWS(KeyName, content)
                    If Not _LogCostParser Is Nothing Then
                        _LogCostParser = Nothing
                    End If

                Case _DCCREATE_NAME
                    _DCCreateParser = New DCCreateParser()
                    _DCCreateParser.SourceName = KeyName
                    _DCCreateParser.BlockName = "FinishUnit"
                    _DCCreateParser.ParseWithTransactionWS(KeyName, content)
                    If Not _DCCreateParser Is Nothing Then
                        _DCCreateParser = Nothing
                    End If


                Case _DCCHANGE_NAME
                    _DCCreateParser = New DCCreateParser()
                    _DCCreateParser.SourceName = KeyName
                    _DCCreateParser.BlockName = "FinishUnit"
                    _DCCreateParser.ParseWithTransactionWS(KeyName, content)
                    If Not _DCCreateParser Is Nothing Then
                        _DCCreateParser = Nothing
                    End If


                Case _TRANSACTUALDATE_NAME
                    _PaymentTransferActualDate = New TransferPaymentActualDateParser()
                    _PaymentTransferActualDate.SourceName = KeyName
                    _PaymentTransferActualDate.BlockName = "FinishUnit"
                    _PaymentTransferActualDate.ParseWithTransactionWS(KeyName, content)
                    If Not _PaymentTransferActualDate Is Nothing Then
                        _PaymentTransferActualDate = Nothing
                    End If

                    'Start  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20171206
                Case _MSPMASTER_NAME
                    _MSPMaster = New MSPMasterParser()
                    _MSPMaster.SourceName = KeyName
                    _MSPMaster.BlockName = "MSPMaster"
                    _MSPMaster.ParseWithTransactionWS(KeyName, content)
                    If Not _MSPMaster Is Nothing Then
                        _MSPMaster = Nothing
                    End If

                Case _MSPTYPE_NAME
                    _MSPType = New MSPTypeParser()
                    _MSPType.SourceName = KeyName
                    _MSPType.BlockName = "MSPType"
                    _MSPType.ParseWithTransactionWS(KeyName, content)
                    If Not _MSPType Is Nothing Then
                        _MSPType = Nothing
                    End If

                Case _MSPDURATIONPMKIND_NAME
                    _MSPDurationPMKind = New MSPDurationPMKindParser()
                    _MSPDurationPMKind.SourceName = KeyName
                    _MSPDurationPMKind.BlockName = "MSPDurationPMKind"
                    _MSPDurationPMKind.ParseWithTransactionWS(KeyName, content)
                    If Not _MSPDurationPMKind Is Nothing Then
                        _MSPDurationPMKind = Nothing
                    End If

                Case _MSPTRANSPAYMENT_NAME
                    _MSPTransferPaymentParser = New MSPTransferPaymentParser()
                    _MSPTransferPaymentParser.SourceName = KeyName
                    _MSPTransferPaymentParser.BlockName = "MSPTransferPayment"
                    _MSPTransferPaymentParser.ParseWithTransactionWS(KeyName, content)
                    If Not _MSPTransferPaymentParser Is Nothing Then
                        _MSPTransferPaymentParser = Nothing
                    End If

                Case _MSPTRANSPAYMENT_NAME
                    _MSPTransferPaymentParser = New MSPTransferPaymentParser()
                    _MSPTransferPaymentParser.SourceName = KeyName
                    _MSPTransferPaymentParser.BlockName = "MSPTransferPayment"
                    _MSPTransferPaymentParser.ParseWithTransactionWS(KeyName, content)
                    If Not _MSPTransferPaymentParser Is Nothing Then
                        _MSPTransferPaymentParser = Nothing
                    End If

                    'End  :CR:MitsubishiSmartPackage;By:Ako;For:Isye/Halimi;Date:20171206

                    'CR MSPExtended'
                Case _MSPEXTTYPE_NAME
                    _MSPExtTypeParser = New MSPExtTypeParser()
                    _MSPExtTypeParser.SourceName = KeyName
                    _MSPExtTypeParser.BlockName = "FinishUnit"
                    _MSPExtTypeParser.ParseWithTransactionWS(KeyName, content)
                    If Not _MSPExtTypeParser Is Nothing Then
                        _MSPExtTypeParser = Nothing
                    End If

                Case _MSPEXTMASTER_NAME
                    _MSPExtMasterParser = New MSPExtMasterParser()
                    _MSPExtMasterParser.SourceName = KeyName
                    _MSPExtMasterParser.BlockName = "FinishUnit"
                    _MSPExtMasterParser.ParseWithTransactionWS(KeyName, content)
                    If Not _MSPExtMasterParser Is Nothing Then
                        _MSPExtMasterParser = Nothing
                    End If

                Case _MSPEXTACTUALDATE_NAME
                    _MSPExtActualDateParser = New MSPExtActualDateParser()
                    _MSPExtActualDateParser.SourceName = KeyName
                    _MSPExtActualDateParser.BlockName = "FinishUnit"
                    _MSPExtActualDateParser.ParseWithTransactionWS(KeyName, content)
                    If Not _MSPExtActualDateParser Is Nothing Then
                        _MSPExtActualDateParser = Nothing
                    End If

                Case _MSPEXTPAYMENT_NAME
                    _MSPExtPaymentParser = New MSPExtPaymentParser()
                    _MSPExtPaymentParser.SourceName = KeyName
                    _MSPExtPaymentParser.BlockName = "FinishUnit"
                    _MSPExtPaymentParser.ParseWithTransactionWS(KeyName, content)
                    If Not _MSPExtPaymentParser Is Nothing Then
                        _MSPExtPaymentParser = Nothing
                    End If

                Case _MSPEXTREGISTRATION_NAME
                    _MSPEXRegistrationParser = New MSPEXRegistrationParser()
                    _MSPEXRegistrationParser.SourceName = KeyName
                    _MSPEXRegistrationParser.BlockName = "Service"
                    _MSPEXRegistrationParser.ParseWithTransactionWS(KeyName, content)
                    If Not _MSPEXRegistrationParser Is Nothing Then
                        _MSPEXRegistrationParser = Nothing
                    End If

                Case _MSPEXFAKTURPAJAK_NAME
                    _MSPEXFakturPajakParser = New MSPEXFakturPajakParser()
                    _MSPEXFakturPajakParser.SourceName = KeyName
                    _MSPEXFakturPajakParser.BlockName = "FinishUnit"
                    _MSPEXFakturPajakParser.ParseWithTransactionWS(KeyName, content)
                    If Not _MSPEXFakturPajakParser Is Nothing Then
                        _MSPEXFakturPajakParser = Nothing
                    End If

                    'MSPExtended Fleet'
                Case _FLEETMASTER_NAME
                    _MSPExtMasterParser = New MSPExtMasterParser()
                    _MSPExtMasterParser.SourceName = KeyName
                    _MSPExtMasterParser.BlockName = "FinishUnit"
                    _MSPExtMasterParser.ParseWithTransactionWS(KeyName, content)
                    If Not _MSPExtMasterParser Is Nothing Then
                        _MSPExtMasterParser = Nothing
                    End If

                Case _FLEETTYPE_NAME
                    _MSPExtTypeParser = New MSPExtTypeParser()
                    _MSPExtTypeParser.SourceName = KeyName
                    _MSPExtTypeParser.BlockName = "FinishUnit"
                    _MSPExtTypeParser.ParseWithTransactionWS(KeyName, content)
                    If Not _MSPExtTypeParser Is Nothing Then
                        _MSPExtTypeParser = Nothing
                    End If

                    'CR Centralized Master Data START 20180822
                Case _VECHILEMODEL_NAME
                    _vehicleModelParser = New VechileModelParser()
                    _vehicleModelParser.SourceName = KeyName
                    _vehicleModelParser.BlockName = "Master Vechile Model"
                    _vehicleModelParser.ParseWithTransactionWS(KeyName, content)
                    If Not _vehicleModelParser Is Nothing Then
                        _vehicleModelParser = Nothing
                    End If

                Case _PROVINCECITY_NAME
                    _provinceCityParser = New ProvinceCityParser()
                    _provinceCityParser.SourceName = KeyName
                    _provinceCityParser.BlockName = "Master Province City"
                    _provinceCityParser.ParseWithTransactionWS(KeyName, content)
                    If Not _provinceCityParser Is Nothing Then
                        _provinceCityParser = Nothing
                    End If



                Case _VECHILETYPE_NAME
                    _vehicleTypeParser = New VechileTypeParser()
                    _vehicleTypeParser.SourceName = KeyName
                    _vehicleTypeParser.BlockName = "Master Vechile Type"
                    _vehicleTypeParser.ParseWithTransactionWS(KeyName, content)
                    If Not _vehicleTypeParser Is Nothing Then
                        _vehicleTypeParser = Nothing
                    End If

                Case _VEHICLEPRICE_NAME
                    _vehiclePriceParser = New VehiclePriceParser()
                    _vehiclePriceParser.SourceName = KeyName
                    _vehiclePriceParser.BlockName = "Vehicle Price"
                    _vehiclePriceParser.ParseWithTransactionWS(KeyName, content)
                    If Not _vehiclePriceParser Is Nothing Then
                        _vehiclePriceParser = Nothing
                    End If

                Case _SPAREPART_NAME
                    _sparePartParser = New SparePartParser()
                    _sparePartParser.SourceName = KeyName
                    _sparePartParser.BlockName = "Spare Part Master"
                    _sparePartParser.ParseWithTransactionWS(KeyName, content)
                    If Not _sparePartParser Is Nothing Then
                        _sparePartParser = Nothing
                    End If

                Case _SPAREPARTPRICE_NAME
                    _sparePartPriceParser = New SparePartPriceParser()
                    _sparePartPriceParser.SourceName = KeyName
                    _sparePartPriceParser.BlockName = "Spare Part Price"
                    _sparePartPriceParser.ParseWithTransactionWS(KeyName, content)
                    If Not _sparePartPriceParser Is Nothing Then
                        _sparePartPriceParser = Nothing
                    End If

                Case _SPAREPARTMASTERALT_NAME
                    _sparePartMasterAltParser = New SparePartMasterAltParser()
                    _sparePartMasterAltParser.SourceName = KeyName
                    _sparePartMasterAltParser.BlockName = "Spare Part Master Alt"
                    _sparePartMasterAltParser.ParseWithTransactionWS(KeyName, content)
                    If Not _sparePartMasterAltParser Is Nothing Then
                        _sparePartMasterAltParser = Nothing
                    End If


                Case _AREA_NAME
                    _areaParser = New AreaParser()
                    _areaParser.SourceName = KeyName
                    _areaParser.BlockName = "Area Master"
                    _areaParser.ParseWithTransactionWS(KeyName, content)
                    If Not _areaParser Is Nothing Then
                        _areaParser = Nothing
                    End If

                Case _DEALERSTALLEQUIPMENT_NAME
                    _StallEquipmentParser = New DealerStallEquipmentParser()
                    _StallEquipmentParser.SourceName = KeyName
                    _StallEquipmentParser.BlockName = "DealerStallEquipment Master"
                    _StallEquipmentParser.ParseWithTransactionWS(KeyName, content)
                    If Not _StallEquipmentParser Is Nothing Then
                        _StallEquipmentParser = Nothing
                    End If

                Case _DEALERFACILITY_NAME
                    _DealerFacilityParser = New DealerFacilityParser()
                    _DealerFacilityParser.SourceName = KeyName
                    _DealerFacilityParser.BlockName = "DealerFacility Master"
                    _DealerFacilityParser.ParseWithTransactionWS(KeyName, content)
                    If Not _DealerFacilityParser Is Nothing Then
                        _DealerFacilityParser = Nothing
                    End If

                Case _SALESORGANIZATION_NAME
                    _SalesOrganizationParser = New DealerSalesOrganizationParser()
                    _SalesOrganizationParser.SourceName = KeyName
                    _SalesOrganizationParser.BlockName = "Dealer Sales Organization Master"
                    _SalesOrganizationParser.ParseWithTransactionWS(KeyName, content)
                    If Not _SalesOrganizationParser Is Nothing Then
                        _SalesOrganizationParser = Nothing
                    End If

                Case _DEALERCONTACT_NAME
                    _DealerContactParser = New DealerContactParser()
                    _DealerContactParser.SourceName = KeyName
                    _DealerContactParser.BlockName = "Dealer Contact Master"
                    _DealerContactParser.ParseWithTransactionWS(KeyName, content)
                    If Not _DealerContactParser Is Nothing Then
                        _DealerContactParser = Nothing
                    End If


                Case _DEALERBRANCH_NAME
                    _dealerBranchParser = New DealerBranchParser()
                    _dealerBranchParser.SourceName = KeyName
                    _dealerBranchParser.BlockName = "Dealer Branch Master"
                    _dealerBranchParser.ParseWithTransactionWS(KeyName, content)
                    If Not _dealerBranchParser Is Nothing Then
                        _dealerBranchParser = Nothing
                    End If
                    'CR Centralized Master Data END
                Case _KAROSERI_NAME
                    _Karoseri = New KaroseriParser()
                    _Karoseri.SourceName = KeyName
                    _Karoseri.BlockName = "General"
                    _Karoseri.ParseWithTransactionWS(KeyName, content)
                    If Not _Karoseri Is Nothing Then
                        _Karoseri = Nothing
                    End If

                Case _LEASING_NAME
                    _Leasing = New LeasingParser()
                    _Leasing.SourceName = KeyName
                    _Leasing.BlockName = "General"
                    _Leasing.ParseWithTransactionWS(KeyName, content)
                    If Not _Leasing Is Nothing Then
                        _Leasing = Nothing
                    End If

                Case _REVISIONPRICE_NAME
                    _RevisionPrice = New RevisionPriceParser()
                    _RevisionPrice.SourceName = KeyName
                    _RevisionPrice.BlockName = "General"
                    _RevisionPrice.ParseWithTransactionWS(KeyName, content)
                    If Not _RevisionPrice Is Nothing Then
                        _RevisionPrice = Nothing
                    End If

                Case _REVISIONPAYMENTTRANS_NAME
                    _RevisionPaymentTrans = New RevisionPaymentTransParser()
                    _RevisionPaymentTrans.SourceName = KeyName
                    _RevisionPaymentTrans.BlockName = "FinishUnit"
                    _RevisionPaymentTrans.ParseWithTransactionWS(KeyName, content)
                    If Not _RevisionPaymentTrans Is Nothing Then
                        _RevisionPaymentTrans = Nothing
                    End If

                Case _REVISIONPAYMENTVIRTU_NAME
                    _RevisionPaymentVirtu = New RevisionPaymentVirtuParser()
                    _RevisionPaymentVirtu.SourceName = KeyName
                    _RevisionPaymentVirtu.BlockName = "FinishUnit"
                    _RevisionPaymentVirtu.ParseWithTransactionWS(KeyName, content)
                    If Not _RevisionPaymentVirtu Is Nothing Then
                        _RevisionPaymentVirtu = Nothing
                    End If

                Case _REVISIONPAYMENTGYRO_NAME
                    _RevisionPaymentGyro = New RevisionPaymentGyroParser()
                    _RevisionPaymentGyro.SourceName = KeyName
                    _RevisionPaymentGyro.BlockName = "FinishUnit"
                    _RevisionPaymentGyro.ParseWithTransactionWS(KeyName, content)
                    If Not _RevisionPaymentGyro Is Nothing Then
                        _RevisionPaymentGyro = Nothing
                    End If

                Case _REVISIONPAYMENTJVCANCEL_NAME
                    _RevisionPaymentJVCancel = New RevisionPaymentJVCancelParser()
                    _RevisionPaymentJVCancel.SourceName = KeyName
                    _RevisionPaymentJVCancel.BlockName = "FinishUnit"
                    _RevisionPaymentJVCancel.ParseWithTransactionWS(KeyName, content)
                    If Not _RevisionPaymentJVCancel Is Nothing Then
                        _RevisionPaymentJVCancel = Nothing
                    End If

                Case _REVISIONPAYMENTIRTRANSPAYMENT_NAME
                    _RevisionPaymentIRTransPayment = New RevisionPaymentIRTransPaymentParser()
                    _RevisionPaymentIRTransPayment.SourceName = KeyName
                    _RevisionPaymentIRTransPayment.BlockName = "FinishUnit"
                    _RevisionPaymentIRTransPayment.ParseWithTransactionWS(KeyName, content)
                    If Not _RevisionPaymentIRTransPayment Is Nothing Then
                        _RevisionPaymentIRTransPayment = Nothing
                    End If


                    'Start TOPSParepart
                Case _TOPSPBILLINGBLOCK_NAME
                    _TOPSPBillingBlockParser = New TOPSPBillingBlockParser()
                    _TOPSPBillingBlockParser.SourceName = KeyName
                    _TOPSPBillingBlockParser.BlockName = "TOPSPBillingBlock"
                    _TOPSPBillingBlockParser.ParseWithTransactionWS(KeyName, content)
                    If Not _TOPSPBillingBlockParser Is Nothing Then
                        _TOPSPBillingBlockParser = Nothing
                    End If

                Case _TOPSPTRANSCEILING_NAME
                    _TOPSPTransferCeilingParser = New TOPSPTransferCeilingParser()
                    _TOPSPTransferCeilingParser.SourceName = KeyName
                    _TOPSPTransferCeilingParser.BlockName = "TOPSPTransferCeilingParser"
                    _TOPSPTransferCeilingParser.ParseWithTransactionWS(KeyName, content)
                    If Not _TOPSPTransferCeilingParser Is Nothing Then
                        _TOPSPTransferCeilingParser = Nothing
                    End If



                Case _TOPSPTRANSCEILINGDETAIL_NAME
                    _TOPSPTransferCeilingDetailParser = New TOPSPTransferCeilingDetailParser()
                    _TOPSPTransferCeilingDetailParser.SourceName = KeyName
                    _TOPSPTransferCeilingDetailParser.BlockName = "TOPSPTransferCeilingDetailParser"
                    _TOPSPTransferCeilingDetailParser.ParseWithTransactionWS(KeyName, content)
                    If Not _TOPSPTransferCeilingDetailParser Is Nothing Then
                        _TOPSPTransferCeilingDetailParser = Nothing
                    End If


                Case _TOPSPUPDATECEILING_NAME
                    _TOPSPTransferCeilingUpdateParser = New TOPSPTransferCeilingUpdateParser(_fullkeyname)
                    _TOPSPTransferCeilingUpdateParser.SourceName = KeyName
                    _TOPSPTransferCeilingUpdateParser.BlockName = "SparePart"
                    _TOPSPTransferCeilingUpdateParser.ParseWithTransactionWS(KeyName, content)
                    If Not _TOPSPTransferCeilingUpdateParser Is Nothing Then
                        _TOPSPTransferCeilingUpdateParser = Nothing
                    End If

                Case _TOPSPBILLINGDEPOSIT_NAME
                    _TOPSPBillingDepositParser = New TOPSPBillingDepositParser()
                    _TOPSPBillingDepositParser.SourceName = KeyName
                    _TOPSPBillingDepositParser.BlockName = "SparePart"
                    _TOPSPBillingDepositParser.ParseWithTransactionWS(KeyName, content)
                    If Not _TOPSPBillingDepositParser Is Nothing Then
                        _TOPSPBillingDepositParser = Nothing
                    End If

                Case _TOPTRANSACTUALDATE_NAME
                    _TOPTranActualDateParser = New TOPTranActualDateParser()
                    _TOPTranActualDateParser.SourceName = KeyName
                    _TOPTranActualDateParser.BlockName = "SparePart"
                    _TOPTranActualDateParser.ParseWithTransactionWS(KeyName, content)
                    If Not _TOPTranActualDateParser Is Nothing Then
                        _TOPTranActualDateParser = Nothing
                    End If

                Case _TOPSPKLIRING_NAME
                    _TOPSPKliringParser = New TOPSPKliringParser()
                    _TOPSPKliringParser.SourceName = KeyName
                    _TOPSPKliringParser.BlockName = "SparePart"
                    _TOPSPKliringParser.ParseWithTransactionWS(KeyName, content)
                    If Not _TOPSPKliringParser Is Nothing Then
                        _TOPSPKliringParser = Nothing
                    End If

                Case _CODOUTSTANDING_NAME
                    _CODOutstandingParser = New CODOutstandingParser()
                    _CODOutstandingParser.SourceName = KeyName
                    _CODOutstandingParser.BlockName = "SparePart"
                    _CODOutstandingParser.ParseWithTransactionWS(KeyName, content)
                    If Not _CODOutstandingParser Is Nothing Then
                        _CODOutstandingParser = Nothing
                    End If

                Case _CODPAYMENT_NAME
                    _CODPaymentParser = New CODPaymentParser()
                    _CODPaymentParser.SourceName = KeyName
                    _CODPaymentParser.BlockName = "SparePart"
                    _CODPaymentParser.ParseWithTransactionWS(KeyName, content)
                    If Not _CODPaymentParser Is Nothing Then
                        _CODPaymentParser = Nothing
                    End If

                Case _TOPSPPENALTY_NAME
                    _TOPSPPenaltyParser = New TOPSPPenaltyParser()
                    _TOPSPPenaltyParser.SourceName = KeyName
                    _TOPSPPenaltyParser.BlockName = "SparePart"
                    _TOPSPPenaltyParser.ParseWithTransactionWS(KeyName, content)
                    If Not _TOPSPPenaltyParser Is Nothing Then
                        _TOPSPPenaltyParser = Nothing
                    End If

                Case _TOPSPPENALTYUPDATE_NAME
                    _TOPSPPenaltyUpdateParser = New TOPSPPenaltyUpdateParser()
                    _TOPSPPenaltyUpdateParser.SourceName = KeyName
                    _TOPSPPenaltyUpdateParser.BlockName = "SparePart"
                    _TOPSPPenaltyUpdateParser.ParseWithTransactionWS(KeyName, content)
                    If Not _TOPSPPenaltyUpdateParser Is Nothing Then
                        _TOPSPPenaltyUpdateParser = Nothing
                    End If
                Case _TOPSPPENALTYJV_NAME
                    _TOPSPPenaltyUpdateJVParser = New TOPSPPenaltyUpdateJVParser()
                    _TOPSPPenaltyUpdateJVParser.SourceName = KeyName
                    _TOPSPPenaltyUpdateJVParser.BlockName = "SparePart"
                    _TOPSPPenaltyUpdateJVParser.ParseWithTransactionWS(KeyName, content)
                    If Not _TOPSPPenaltyUpdateJVParser Is Nothing Then
                        _TOPSPPenaltyUpdateJVParser = Nothing
                    End If

                Case _TOPSPPAYMENTOUTSTANDING_NAME
                    _TOPSPPaymentOutstandingParser = New TOPSPPaymentOutstandingParser()
                    _TOPSPPaymentOutstandingParser.SourceName = KeyName
                    _TOPSPPaymentOutstandingParser.BlockName = "SparePart"
                    _TOPSPPaymentOutstandingParser.ParseWithTransactionWS(KeyName, content)
                    If Not _TOPSPPaymentOutstandingParser Is Nothing Then
                        _TOPSPPaymentOutstandingParser = Nothing
                    End If

                    'End TOPSParepart

                Case _MASTERDEALERGROUP_NAME
                    _MasterDealerGroupParser = New MasterDealerGroupParser()
                    _MasterDealerGroupParser.SourceName = KeyName
                    _MasterDealerGroupParser.BlockName = "MasterDealerGroup"
                    _MasterDealerGroupParser.ParseWithTransactionWS(KeyName, content)
                    If Not _MasterDealerGroupParser Is Nothing Then
                        _MasterDealerGroupParser = Nothing
                    End If

                Case _MASTERCREDITACCOUNT_NAME
                    _MasterCreditAccountParser = New MasterCreditAccountParser()
                    _MasterCreditAccountParser.SourceName = KeyName
                    _MasterCreditAccountParser.BlockName = "MasterCreditAccount"
                    _MasterCreditAccountParser.ParseWithTransactionWS(KeyName, content)
                    If Not _MasterCreditAccountParser Is Nothing Then
                        _MasterCreditAccountParser = Nothing
                    End If

                Case _MASTERTOP_NAME
                    _MasterTOPParser = New MasterTOPParser()
                    _MasterTOPParser.SourceName = KeyName
                    _MasterTOPParser.BlockName = "MasterTOP"
                    _MasterTOPParser.ParseWithTransactionWS(KeyName, content)
                    If Not _MasterTOPParser Is Nothing Then
                        _MasterTOPParser = Nothing
                    End If

                Case _MASTERDEALERTERRITORY_NAME
                    _MasterDealerTerritoryParser = New MasterDealerTerritoryParser()
                    _MasterDealerTerritoryParser.SourceName = KeyName
                    _MasterDealerTerritoryParser.BlockName = "MasterDealerTerritory"
                    _MasterDealerTerritoryParser.ParseWithTransactionWS(KeyName, content)
                    If Not _MasterDealerTerritoryParser Is Nothing Then
                        _MasterDealerTerritoryParser = Nothing
                    End If

                Case _MASTERDEALER_NAME
                    _MasterDealerParser = New MasterDealerParser()
                    _MasterDealerParser.SourceName = KeyName
                    _MasterDealerParser.BlockName = "MasterDealer"
                    _MasterDealerParser.ParseWithTransactionWS(KeyName, content)
                    If Not _MasterDealerParser Is Nothing Then
                        _MasterDealerParser = Nothing
                    End If

                Case _MASTERVEHICLEKIND_NAME
                    _MasterVehicleKindParser = New MasterVehicleKindParser()
                    _MasterVehicleKindParser.SourceName = KeyName
                    _MasterVehicleKindParser.BlockName = "MasterVehicleKind"
                    _MasterVehicleKindParser.ParseWithTransactionWS(KeyName, content)
                    If Not _MasterVehicleKindParser Is Nothing Then
                        _MasterVehicleKindParser = Nothing
                    End If


                Case _MASTERINTEREST_NAME
                    _MasterInterestParser = New MasterInterestParser()
                    _MasterInterestParser.SourceName = KeyName
                    _MasterInterestParser.BlockName = "Master Interest"
                    _MasterInterestParser.ParseWithTransactionWS(KeyName, content)
                    If Not _MasterInterestParser Is Nothing Then
                        _MasterInterestParser = Nothing
                    End If


                Case _MASTERINTERESTPO_NAME
                    _MasterInterestPOParser = New DealerPOTargetParser()
                    _MasterInterestPOParser.SourceName = KeyName
                    _MasterInterestPOParser.BlockName = "Dealer PO Target"
                    _MasterInterestPOParser.ParseWithTransactionWS(KeyName, content)
                    If Not _MasterInterestParser Is Nothing Then
                        _MasterInterestParser = Nothing
                    End If


                Case _MDPDEALER_NAME
                    _MDPMasterDealerParser = New MDPMasterDealerParser()
                    _MDPMasterDealerParser.SourceName = KeyName
                    _MDPMasterDealerParser.BlockName = "MDPDealer"
                    _MDPMasterDealerParser.ParseWithTransactionWS(KeyName, content)
                    If Not _MDPMasterDealerParser Is Nothing Then
                        _MDPMasterDealerParser = Nothing
                    End If
                Case _MDPDAILYSTOCK_NAME
                    _MDPDailyStockParser = New MDPDailyStockParser()
                    _MDPDailyStockParser.SourceName = KeyName
                    _MDPDailyStockParser.BlockName = "MDPDailyStock"
                    _MDPDailyStockParser.ParseWithTransactionWS(KeyName, content)
                    If Not _MDPDailyStockParser Is Nothing Then
                        _MDPDailyStockParser = Nothing
                    End If
                Case _MDPVEHICLE_NAME
                    _MDPMasterVehicleParser = New MDPMasterVehicleParser()
                    _MDPMasterVehicleParser.SourceName = KeyName
                    _MDPMasterVehicleParser.BlockName = "MDPVehicle"
                    _MDPMasterVehicleParser.ParseWithTransactionWS(KeyName, content)
                    If Not _MDPMasterVehicleParser Is Nothing Then
                        _MDPMasterVehicleParser = Nothing
                    End If

                Case _SBABITJV_NAME
                    _BabitParser = New BabitParser()
                    _BabitParser.SourceName = KeyName
                    _BabitParser.BlockName = "Babit"
                    _BabitParser.ParseWithTransactionWS(KeyName, content)
                    If Not _BabitParser Is Nothing Then
                        _BabitParser = Nothing
                    End If

                Case _ALLOCATEFSKIND_NAME
                    _AllocateFSKINDParser = New AllocateFSKINDParser()
                    _AllocateFSKINDParser.SourceName = KeyName
                    _AllocateFSKINDParser.BlockName = "Service"
                    _AllocateFSKINDParser.ParseWithTransactionWS(KeyName, content)
                    If Not _AllocateFSKINDParser Is Nothing Then
                        _AllocateFSKINDParser = Nothing
                    End If

                Case _FSKIND_NAME
                    _FSKINDParser = New FSKINDParser()
                    _FSKINDParser.SourceName = KeyName
                    _FSKINDParser.BlockName = "Service"
                    _FSKINDParser.ParseWithTransactionWS(KeyName, content)
                    If Not _FSKINDParser Is Nothing Then
                        _FSKINDParser = Nothing
                    End If

                Case _SPAREPARTNOTARETUR_NAME
                    _SparepartNotaReturParser = New SparepartNotaReturParser()
                    _SparepartNotaReturParser.SourceName = KeyName
                    _SparepartNotaReturParser.BlockName = "SparePart"
                    _SparepartNotaReturParser.ParseWithTransactionWS(KeyName, content)
                    If Not _SparepartNotaReturParser Is Nothing Then
                        _SparepartNotaReturParser = Nothing
                    End If

                Case _BILLINGAPOUTSTANDING_NAME
                    _BillingAPOutstandingParser = New BillingAPOutstandingParser()
                    _BillingAPOutstandingParser.SourceName = KeyName
                    _BillingAPOutstandingParser.BlockName = "Service"
                    _BillingAPOutstandingParser.ParseWithTransactionWS(KeyName, content)
                    If Not _BillingAPOutstandingParser Is Nothing Then
                        _BillingAPOutstandingParser = Nothing
                    End If


                Case Else
                    Return False
            End Select

            Return True
        End Function


        Public Function WSProses(ByVal body As String, Optional ByRef Msg As String = "") As Boolean
            Dim IsOk As Boolean = False

            If Not String.IsNullorEmpty(body) Then
                Dim sKey As String = Me.GetKeyName(body)
                Dim sContent As String = Me.GetContent(body)
                Dim strFullKey As String = Me.GetFullKeyName(body)
                _fullkeyname = strFullKey
                If sKey <> String.Empty Then
                    Try
                        IsOk = Distribute(sKey, sContent)
                    Catch ex As Exception
                        Msg = ex.Message
                    End Try

                    Return IsOk
                End If

                Msg = "Invalid Data"
                Return False
            Else
                Msg = "Invalid Data"
                Return False
            End If


            Return True
        End Function

#Region "Private Methods"
#End Region

    End Class

End Namespace