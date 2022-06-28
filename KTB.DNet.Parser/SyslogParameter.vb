#Region "Imports"
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.BusinessFacade.Helper
Imports System.Security.Principal
Imports KTB.DNet.Utility
Imports System.Configuration
#End Region

Public Class WSMSyslogParameter

#Region "Constructor"

    Public Sub New(ByVal userPrincipal As IPrincipal)
        mUser = userPrincipal
    End Sub

#End Region

#Region "Private Variable"
    Private mUser As IPrincipal
    Private IPSERVER As String = "172.17.111.26"
    'Private blockName As String = "SAP Listener"

#End Region

#Region "Enum"
    Public Enum ParserType
        ActualGIDateParser
        AnnualDiscountAchievementParser
        BasicProductParser
        BillingReturParser
        BOMParser
        ConditionMasterParser
        ContractParser
        CustomerDataParser
        CustomerDealerParser
        CustomerOrderParser
        CustomerRequestParser
        DailyPaymentParser
        DepositAParser
        DepositC2Parser
        DepositParser
        DOStatusListParser
        endCustomerParser
        EquipmentParser
        EquipmentPriceParser
        FreeServiceSynChronizationParser
        FreeServisParser
        InvoiceParser
        KodeKerjaParser
        KodePosisiParser
        KodePositionWSCParser
        LaborMasterParser
        ListInvoiceParser
        MaterialAllocationParser
        MontlyDocumentParser
        ParserHelper
        PaymentObligationParser
        PaymentStatusParser
        PDIParser
        PDISynChronizationParser
        PendingOrderParser
        PKAlocationParser
        PMParser
        PMStatusParser
        POBlockParser
        PriceParser
        ProductionPlanParser
        ProposedQuantityParser
        PurchaseOrderParser
        RejectedSparePartPOParser
        SPAFParser
        SparepartMasterParser
        SparePartPOBillingRecapParser
        SparePartPOParser
        SparePartParser
        SPPOChecklistParser
        SPPOEEstimateParser
        SPPOStatusParser
        UserParser
        VehicleColorParser
        VechicleInformationSystemParser
        WSCParser
        WSCSStatusParser
        DepositAInterestParser
        DebitNoteParser
        DealerBankAccountParser
        JVParser
        JVCancelParser
        CreditCeilingParser
        CessieParser
        PartShopParser
        CreditMasterSPParser
        SPPendingOrderBOParser
        SPPendingOrderParser
        SPOutstansdingOrderParser
        JVCairParser
        StockTargetParser
        BenefitClaimParser
        FSChassisCampaignParser
        DepositBParser
        DepositBInterestParser
        DepositBDebitNoteParser
        DepositBJVParser
        DepositBKewajibanPHeaderParser
        SparePartDOParser
        SparePartDODeleteParser
        SparePartDOExpeditionParser
        SparePartBillingParser
        SparePartBillingDeleteParser
        SparePartPackingParser
        SparePartSOParser
        SparePartSODeleteParser
        PODestinationParser
        LogModelParser
        LogCostParser
        LogisticDCParser
        BillingCreateParser
        BillingUpdateParser
        MSPMasterParser
        MSPExMasterParser
        MSPTypeParser
        MSPExTypeParser
        MSPDurationPMKindParser
        MSPDC
        MSPDM
        MSPTRFPAYMENTRESULT
        MSPCLAIMDOCUMENT
        MSPTransferPayment
        KaroseriParser
        LeasingParser
        RevisionPriceParser
        RevisionPaymentTransParser
        RevisionPaymentVirtuParser
        RevisionPaymentGyroParser
        RevisionPaymentIRTransPaymentParser
        VechileModelParser
        ProvinceCityParser
        VechileTypeParser
        VehiclePriceParser
        SparePartPriceParser
        AreaParser
        DealerBranchParser
        TOPSPBillingBlockParser
        TOPSPTransferCeilingParser
        TOPSPBillingDepositParser
        TOPTRANSACTUALDATEParser

        MasterDealerGroupParser
        MasterCreditAccountParser
        MasterTermOfPaymentParser
        MasterDealerTerritoryParser
        MasterDealerParser
        MasterVehicleKindGroupParser
        MasterVehicleKindParser
        MasterInterestParser
        DealerPOTargetParser
        MDPDailyStockParser
        MDPPeriodStockParser
        MDPMasterDealerParser
        MDPMasterVehicleParser
        BabitParser

        TOPSPKliringParser

        DepositC2InterestParser

        CODOutstandingParser
        CODPaymentParser
        MSPEXRegistrationParser
        MSPEXRegistrationPDFParser

        TOPSPPenaltyParser
        TOPSPPenaltyUpdateParser
        TOPSPPenaltyUpdateJVParser
        DCSPPenaltyParser
        MSPExFakturPajakParser

        EDocumentFakturParser

        FlatRateMasterFSParser
        FlatRateMasterPMParser
        FlatRateMasterFieldFixParser
        ServiceTemplateFSLaborParser
        ServiceTemplateFSPartParser
        ServiceTemplatePMLaborParser
        ServiceTemplatePMPartParser
        ServiceTemplateFFLaborParser
        ServiceTemplateFFPartParser

        AllocateFSKINDParser
        FSKINDParser

        TOPSPTransferOutstandingParser

        SparepartNotaReturParser

        BillingAPOutstandingParser

        SPNotaReturParser
        DepositCInterestPDFParser
        '+++++++++++++++++++++++++++++++++++++++++++++++++++++
    End Enum
#End Region

#Region "Public Method"

    Private Function GetXmlMessage(ByVal FullMessage As String, ByVal moduleName As String, ByVal pages As String, ByVal code As String, ByVal status As String, ByVal subBlockName As String, ByVal typeParser As Integer, Optional ByVal blockName As String = "FinishUnit", Optional ByVal statusResult As String = "failed") As KTB.DNet.Lib.SysLogXMLMessage
        Dim m As New KTB.DNet.Lib.SysLogXMLMessage
        ' Dim app As String = System.AppDomain.CurrentDomain.p

        Select Case typeParser
            Case Is = ParserType.ActualGIDateParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.AnnualDiscountAchievementParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.BasicProductParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.BillingReturParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.BOMParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.ConditionMasterParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.ContractParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.FlatRateMasterFSParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.FlatRateMasterPMParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.FlatRateMasterFieldFixParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.ServiceTemplateFSLaborParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.ServiceTemplateFSPartParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.ServiceTemplatePMLaborParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.ServiceTemplatePMPartParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.ServiceTemplateFFLaborParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.ServiceTemplateFFPartParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.CustomerDataParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.CustomerDealerParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.CustomerOrderParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.StatusResult = statusResult
                m.Status = status
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.CustomerRequestParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.StatusResult = statusResult
                m.Status = status
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.DailyPaymentParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.DepositAParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.DepositC2Parser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.DepositParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.DOStatusListParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.endCustomerParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.EquipmentParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.EquipmentPriceParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.FreeServiceSynChronizationParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.FreeServisParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.InvoiceParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.KodeKerjaParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.KodePosisiParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.KodePositionWSCParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.LaborMasterParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"


                'Nambah ListInvoice : Hari
            Case Is = ParserType.ListInvoiceParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.MaterialAllocationParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.MontlyDocumentParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.ParserHelper
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.PaymentObligationParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.PaymentStatusParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.PDIParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.PDISynChronizationParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.PendingOrderParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.PKAlocationParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.PMParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.PMStatusParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.POBlockParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.PriceParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.ProductionPlanParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.ProposedQuantityParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.PurchaseOrderParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.RejectedSparePartPOParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.SPAFParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.SparepartMasterParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.SparePartPOBillingRecapParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.SparePartPOParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.SPPOChecklistParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.SPPOEEstimateParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.SPPOStatusParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.UserParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.VechicleInformationSystemParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.VehicleColorParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.WSCParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.WSCSStatusParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"
            Case Is = ParserType.DepositAInterestParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"
            Case Is = ParserType.DebitNoteParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.DealerBankAccountParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.JVParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.JVCancelParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.MSPMasterParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.MSPTypeParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.MSPDurationPMKindParser
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.MSPDC
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.MSPDM
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

            Case Is = ParserType.MSPTRFPAYMENTRESULT
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"
            Case Is = ParserType.MSPCLAIMDOCUMENT
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"
            Case Is = ParserType.MSPTransferPayment
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = "/KTB.DNet.Parser/ParserMapper/" & pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"
            Case Else
                m.Action = "Parsing"
                m.BlockName = blockName
                m.FullMessage = FullMessage
                m.ModuleName = moduleName
                m.Pages = pages
                m.RemoteIPAddress = IPSERVER
                m.Result = code
                m.Status = status
                m.StatusResult = statusResult
                m.SubBlockName = subBlockName
                m.UserName = "sap"
                m.Dealer = "system"

        End Select


        Return m
    End Function


    Public Sub LogErrorToSyslog(ByVal FullMessage As String, ByVal moduleName As String, ByVal pages As String, ByVal code As String, ByVal status As String, ByVal subBlockName As String, ByVal typeParser As Integer, Optional ByVal blockName As String = "Finish Unit", Optional ByVal statusResult As String = "failed")
        Dim m As KTB.DNet.Lib.SysLogXMLMessage = GetXmlMessage(FullMessage, moduleName, pages, code, status, subBlockName, typeParser, blockName, statusResult)
        Dim strServerName As String = KTB.DNet.Lib.WebConfig.GetValue("SyslogServerHostName")
        Dim PortNo As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("SyslogServerPortNumber"))
        Dim IsUseUDP As Boolean = CBool(KTB.DNet.Lib.WebConfig.GetValue("IsSyslogUseUDP"))
        Dim logger As New KTB.DNet.Lib.SyslogLogger(strServerName, PortNo, IsUseUDP)
        Dim strLog As String = KTB.DNet.Lib.WebConfig.GetValue("IsLogToSysLog")
        If strLog.Trim.ToUpper = "Y" Then
            Try
                logger.Log(m)
            Catch ex As KTB.DNet.Lib.SysLogServerNotAvailableException
                Dim objSysLogParameter As WSMSyslogParameter = New WSMSyslogParameter(mUser)
                objSysLogParameter.DeferredSyslogMessage(m)
            Catch ex As Exception
                'LogError(ex)
            End Try
        End If
    End Sub

    Public Function GetSyslogParameter(ByVal e As Exception, ByVal parser As String) As KTB.DNet.Lib.SysLogXMLMessage
        Dim m As New KTB.DNet.Lib.SysLogXMLMessage
        m.UserName = mUser.Identity.Name
        m.FullMessage = e.ToString
        m.ModuleName = e.Source
        m.Pages = parser
        If Not e.TargetSite Is Nothing Then
            m.BlockName = e.TargetSite.Name
        End If

        Dim strRemoteIP As String = IPSERVER
        If strRemoteIP Is Nothing Then
            strRemoteIP = String.Empty
        End If
        m.RemoteIPAddress = strRemoteIP
        Return m
    End Function

    Public Sub DeferredSyslogMessage(ByVal message As KTB.DNet.Lib.SysLogXMLMessage)
        Dim objDomain As New KTB.DNet.Domain.SysLog
        Dim facade As New KTB.DNet.BusinessFacade.General.SysLogFacade(mUser)
        Try
            objDomain.Action = message.Action
            objDomain.BlockName = message.BlockName
            objDomain.FullMessage = message.FullMessage
            objDomain.ModuleName = message.ModuleName
            objDomain.Pages = message.Pages
            objDomain.RemoteIPAddress = message.RemoteIPAddress
            objDomain.ResultCode = message.Result
            objDomain.Status = KTB.DNet.Lib.DNetLogFormatStatus.Deferred.ToString()
            objDomain.SubBlockName = message.SubBlockName
            objDomain.UserName = message.UserName
            objDomain.LogTime = DateTime.Now
            facade.Insert(objDomain)
        Catch ex As Exception
            'LogError(ex)
        End Try
    End Sub

    Public Sub LogError(ByVal message As KTB.DNet.Lib.SysLogXMLMessage)
        Dim strServerName As String = KTB.DNet.Lib.WebConfig.GetValue("SyslogServerHostName")
        Dim PortNo As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("SyslogServerPortNumber"))
        Dim IsUseUDP As Boolean = CBool(KTB.DNet.Lib.WebConfig.GetValue("IsSyslogUseUDP"))
        Dim logger As New KTB.DNet.Lib.SyslogLogger(strServerName, PortNo, IsUseUDP)

        Try
            logger.Log(message)
        Catch ex As KTB.DNet.Lib.SysLogServerNotAvailableException
            Dim objSysLogParameter As WSMSyslogParameter = New WSMSyslogParameter(mUser)
            objSysLogParameter.DeferredSyslogMessage(message)
        Catch ex As Exception
            'LogError(ex)
        End Try

    End Sub

#End Region

End Class

