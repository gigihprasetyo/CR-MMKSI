
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

    Public Class Worker

#Region "Variable Declaration"
        Private _log As EventLog
        Private _fileName As String
        Private _destFolder As String
        Private _user As String = "System SAP"
        Private _randomFileName As String
        Private _useSignal As Boolean
        Private _isLogToSysLog As Boolean
        Private smtp As String
        Private sender As String
        Private rec As String
        Private subject As String
        Private body As String
        Private user As IPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)

        'File Name for Finish Unit
        Private _SAPDNETTESTING_FILE_NAME As String = String.Empty
        Private CONTRACT_FILE_NAME As String = String.Empty
        Private PROPOSE_FILE_NAME As String = String.Empty
        Private CHASSIS_MASTER_FILE_NAME As String = String.Empty
        Private PURCHASE_ORDER_FILE_NAME As String = String.Empty
        Private SO_FILE_NAME As String = String.Empty
        Private MO_FILE_NAME As String = String.Empty

        Private PAYMENT_FILE_NAME As String = String.Empty
        Private GYRO_FACTORING_FILE_NAME As String = String.Empty
        Private DO_FILE_NAME As String = String.Empty
        Private ACTUAL_GI_FILE_NAME As String = String.Empty
        Private WSC_STATUS_FILE_NAME As String = String.Empty
        Private WSC_STATUS_FILE_NAME_BB As String = String.Empty
        Private PO_BLOCK_FILE_NAME As String = String.Empty
        Private FREE_SERVICE_STATUS_FILE_NAME As String = String.Empty
        Private FREE_SERVICE_STATUS_FILE_NAME_BB As String = String.Empty
        Private PDI_STATUS_FILE_NAME As String = String.Empty
        Private LABOR_FILE_NAME As String = String.Empty
        Private INVOICE_FILE_NAME As String = String.Empty

        'EQUIPMENT PRICE
        Private EQUIPMENT_PRICE_FILE_NAME As String = String.Empty
        Private EQUIPMENT_MASTER_FILE_NAME As String = String.Empty
        Private BOM_MASTER_FILE_NAME As String = String.Empty


        'File Name for sparepart

        Private NORMAL_PENDINGRO_FILE_NAME As String = String.Empty
        Private BO_PENDINGRO_FILE_NAME As String = String.Empty
        Private BO_ESTIMATERO_FILE_NAME As String = String.Empty
        Private BO_ESTIMATEEO_FILE_NAME As String = String.Empty
        Private BO_OUTSTANDINGRO_FILE_NAME As String = String.Empty
        Private BO_EOUTSTANDINGEO_FILE_NAME As String = String.Empty
        Private BO_EOUTSTANDINGEORO_FILE_NAME As String = String.Empty


        Private PARTMASTER_FILE_NAME As String = String.Empty
        Private CHECKLIST_FILE_NAME As String = String.Empty
        Private EMERGENCY_ESTIMATE_FILE_NAME As String = String.Empty
        Private REGULER_ESTIMATE_FILE_NAME As String = String.Empty
        Private ANNUAL_DISCOUNT_ACHIEVEMENT_FILE_NAME As String = String.Empty
        Private ORDERSTATUS_FILE_NAME As String = String.Empty
        Private DEPOSIT_C2_FILE_NAME As String = String.Empty
        Private DEPOSIT_FILE_NAME As String = String.Empty
        Private SPAREPART_PO_BILLING_RECAP_FILE_NAME As String = String.Empty
        Private SPAREPART_CEILING_FILENAME As String = String.Empty
        Private DCSPPENALTY_FILENAME As String = String.Empty

        'file 4 Montly Document
        Private DEPOSIT_B_REPORT_FILE_NAME As String = String.Empty
        Private KWITANSI_WARANTY_FILE_NAME As String = String.Empty
        Private KWITANSI_FREE_SERVICE_CAMPAIGN_FILE_NAME As String = String.Empty
        Private PDI_LETTER_FILE_NAME As String = String.Empty

        Private FL_LETTER_FILE_NAME As String = String.Empty
        Private KFL_LETTER_FILE_NAME As String = String.Empty

        Private FREE_MAINTENANCE_LETTER_FILE_NAME As String = String.Empty
        Private KWITANSI_FREE_MAINTENANCE_FILE_NAME As String = String.Empty
        Private Kwitansi_Warranty_Spare_Part_Accessories_FILE_NAME As String = String.Empty

        Private PDI_LETTER2_FILE_NAME As String = String.Empty
        Private FREE_SERVICE_LETTER_FILE_NAME As String = String.Empty
        Private FREE_SERVICE_CAMPAIGN_LETTER_FILE_NAME As String = String.Empty
        Private WARANTY_LETTER_FILE_NAME As String = String.Empty
        Private WARANTY_STATUS_LIST_FILE_NAME As String = String.Empty
        Private KODE_POSISI_WSC_FILE_NAME As String = String.Empty
        Private KODE_KERJA_FILE_NAME As String = String.Empty
        Private KODE_POSISI_FILE_NAME As String = String.Empty
        'Penambahan monthly doc BA:Dony, Dev:ridwan
        Private Kwitansi_Free_Maintenance_and_Campaign_FILE_NAME As String = String.Empty
        Private Free_Maintenance_and_campaign_letter_FILE_NAME As String = String.Empty
        'end penambahan monthly doc
        Private _KodekerjaParser As KodeKerjaParser
        Private _KodePosisiParser As KodePosisiParser


        'type
        Private SO_TYPE As String = "SO"
        'Parser Finish Unit
        Private _ContractParser As ContractParser
        Private _ProposeQuantityParser As ProposedQuantityParser
        Private _EndCustomerParser As EndCustomerParser
        Private _PurchaseOrderParser As PurchaseOrderParser
        Private _dailyPaymentParser As DailyPaymentParser
        Private _factoringGyroParser As FactoringGyroParser
        Private _doParser As DOPrintParser
        Private _ActualGIDateParser As ActualGIDateParser
        Private _wscStatusParser As WSCSStatusParser
        Private _wscStatusBBParser As WSCStatusBBParser
        Private _poBlockParser As POBlockParser
        Private _freeServiceStatusParser As FreeServiceSynChronizationParser
        Private _freeServiceStatusBBParser As FreeServiceSynChronizationBBParser
        Private _pdiStatusParser As PDISynChronizationParser
        Private _laborMasterParser As LaborMasterParser
        Private _InvoiceParser As InvoiceParser



        'Sparepart Parser
        Private _sparePartMasterParser As SparePartMasterParser
        Private _checkListParter As SPPOChecklistParser
        Private _estimateParser As SPPOEEstimateParser
        Private _pendingBOParser As SPPendingOrderBOParser
        Private _outstandingOrderParser As SPOutstandingOrderParser
        Private _pendingParser As SPPendingOrderParser
        Private _orderStatusParser As SPPOStatusParser
        Private _annualDiscountAchievementParser As AnnualDiscountAchievementParser
        Private _depositC2Parser As DepositC2Parser
        Private _depositParser As DepositParser
        Private _sparePartPOBillingRecapParser As SparePartPOBillingRecapParser
        Private _creditMasterSPParser As CreditMasterSPParser
        Private _DCSPPenaltyParser As DCSPPenaltyParser
        Private _spNotaReturParser As SPNotaReturParser

        'EQUIPMENT PARSSER
        Private _equipmentPriceParser As EquipmentPriceParser
        Private _equipmentBOMParser As BOMParser
        Private _equipmentMasterParser As EquipmentParser

        'Service
        Private _MontlyDocumentParser As MontlyDocumentParser

        ' - Enhancement
        Private REJECTEDSPAREPARTPO As String = String.Empty
        Private _rejectedSparePartPoParser As RejectedSparePartPOParser
        Private _kodePositionWSCParser As KodePositionWSCParser

        'Phase 4
        Private BILLING_RETUR_FILE_NAME As String = String.Empty
        Private _BillingReturParser As BillingReturParser
        Private CUSTOMER_DATA_FILE_NAME As String = String.Empty
        Private _CustomerDataParser As CustomerDataParser
        Private CUSTOMER_DEALER_FILE_NAME As String = String.Empty
        Private CUSTOMER_REQUEST_FILE_NAME As String = String.Empty
        Private _CustomerDealerParser As CustomerDealerParser
        Private _customerRequestParser As CustomerRequestParser
        Private PM_STATUS_FILE_NAME As String = String.Empty
        Private _pmStatusParser As PMStatusParser
        Private DEPOSIT_A_FILE_NAME As String = String.Empty
        Private _DepositAParser As DepositAParser
        Private PERIODICAL_MAINTENANCE_LETTER_FILE_NAME As String = String.Empty
        Private PENDING_ORDER_FILE_NAME As String = String.Empty
        Private _PendingOrderParser As PendingOrderParser

        'MOTORPOOL/LOGISTIC COST
        Private _LogisticDCParser As LogisticDCParser
        Private _LogisticFakturParser As LogisticFakturParser
        Private _LogisticJVReturParser As LogisticJVReturParser

        Private KUDEPB_FILE_NAME As String = String.Empty
        Private INDEPB_FILE_NAME As String = String.Empty
        Private PAYMENT_OBLIGATION_FILE_NAME As String = String.Empty
        Private _PaymentObligationParser As PaymentObligationParser
        Private PAYMENT_OBLIGATION_STATUS_FILE_NAME As String = String.Empty
        Private _PaymentObligationStatusParser As PaymentObligationStatusParser

        Private DEPOSITA_INTEREST_FILE_NAME As String = String.Empty
        Private _DepositAInterestParser As DepositAInterestParser

        'Nambah Variable : Hari
        Private LIST_INVOICE_FILE_NAME As String = String.Empty
        'Private SO_FILE_FOLDER As String = String.Empty
        Private _ListInvoiceParser As ListInvoiceParser

        Private DEALER_BANKACCOUNT_FILE_NAME As String = String.Empty
        Private _DealerBankAccountParser As DealerBankAccountParser

        Private DEBIT_NOTE_FILE_NAME As String = String.Empty
        Private _DebitNoteParser As DebitNoteParser

        Private JV_FILE_NAME As String = String.Empty
        Private _JVParser As JVParser

        Private JV_CANCEL_FILE_NAME As String = String.Empty
        Private _JVCancelParser As JVCancelParser

        Private JV_CAIR_FILE_NAME As String = String.Empty
        Private _JVCairParser As JVCairParser

        'Credit Ceiling
        Private CREDIT_CEILING_FILE_NAME As String = String.Empty
        Private _CreditCeilingParser As CreditCeilingParser

        'SPAF Optimazation
        Private SPAF_FILE As String = String.Empty
        Private _SPAFParser As SPAFParser

        'Factoring
        Private CESSIE_DATA As String = String.Empty
        Private CESSIE_DOCUMENT As String = String.Empty
        Private CESSIE_DOCUMENT2 As String = String.Empty
        Private _CessieParser As CessieParser
        Private _CessieDocParser As CessieDocParser
        'ParkingFee
        Private PARKINGFEE_FILENAME As String = String.Empty
        Private PARKINGFEE_DEBITCHARGE_FILENAME As String = String.Empty
        Private PARKINGFEE_DEBITMEMO_FILENAME As String = String.Empty
        Private PARKINGFEE_JVRETURN_FILENAME As String = String.Empty
        Private PARKINGFEE_JVNUMBER_FILENAME As String = String.Empty
        Private PARKINGFEE_FAKTURPAJAK_FILENAME As String = String.Empty
        Private ParkingFeeDirectory As String = String.Empty
        Private MSPExtDebitMemoDirectory As String = String.Empty
        Private _ParkingFeeParser As ParkingFeeParser
        Private _ParkingFeeMemoDocParser As ParkingFeeMemoDocParser
        Private _ParkingFeeJVNumberParser As ParkingFeeJVNumberParser
        Private _ParkingFeeJVReturParser As ParkingFeeJVReturnParser
        Private _ParkingFeeFakturPajakParser As ParkingFeeFakturPajakParser

        Private _MSPEXRegistrationPDFParser As MSPEXRegistrationPDFParser

        Private _SAPTestingParser As SAPTestingParser

        'EDOC
        Private _EDocumentFakturParser As EDocumentFakturParser
        'Private _MSPEXRegistrationPDFParser As MSPEXRegistrationPDFParser
        'Private _MSPEXRegistrationPDFParser As MSPEXRegistrationPDFParser
        'Private _MSPEXRegistrationPDFParser As MSPEXRegistrationPDFParser
        'Private _MSPEXRegistrationPDFParser As MSPEXRegistrationPDFParser
        'Private _MSPEXRegistrationPDFParser As MSPEXRegistrationPDFParser
        'Private _MSPEXRegistrationPDFParser As MSPEXRegistrationPDFParser
        'Private _MSPEXRegistrationPDFParser As MSPEXRegistrationPDFParser

        'SSE
        Private _DepositCInterestPDFParser As DepositCInterestPDFParser
        Private _SPNOTARETURPDFParser As SPNotaReturParser

        'Sales Campaign
        Private JV_CAMPAIGN_FILE_NAME As String = String.Empty
        Private _benefitClaimParser As BenefitClaimParser

        'DepositB
        Private DEPOSIT_B_FILE_NAME As String = String.Empty
        Private _DepositBParser As DepositBParser

        Private DEPOSIT_B_INTEREST_FILE_NAME As String = String.Empty
        Private _DepositBInterestParser As DepositBInterestParser

        Private DEPOSIT_B_DEBITNOTE_FILE_NAME As String = String.Empty
        Private _DepositBDebitNoteParser As DepositBDebitNoteParser

        Private DEPOSIT_B_JV_FILE_NAME As String = String.Empty
        Private _DepositBJVParser As DepositBJVParser

        Private DEPOSIT_B_SO_FILE_NAME As String = String.Empty
        Private _DepositBKewajibanHeaderParser As DepositBKewajibanHeaderParser

        Private DEPOSIT_B_JVCAIR_FILE_NAME As String = String.Empty
        Private _DepositBReceiptParser As DepositBReceiptParser

        'DepositC
        Private DEPOSIT_C_INTEREST_FILE_NAME As String = String.Empty
        Private _DepositCInterestParser As DepositCInterestParser

        'MOTORPOOL LOGISTIC
        Private LOGISTIC_DEBITCHARGE_FILE_NAME As String = String.Empty
        Private LOGISTIC_DEBITMEMO_FILE_NAME As String = String.Empty
        Private LOGISTIC_FAKTURPAJAK_FILE_NAME As String = String.Empty
        Private LOGISTIC_JVRETURN_FILE_NAME As String = String.Empty

        ' MSP
        Private MSP_REGISTRATION_DC As String = String.Empty
        Private MSP_REGISTRATION_DM As String = String.Empty
        Private MSP_CLAIM_LETTER As String = String.Empty
        Private MSP_CLAIM_KUITANSI As String = String.Empty

        ' MSP EXT
        Private MSP_EXT_REGISTRATION_PDF As String = String.Empty

        'EDOC
        Private EDOC_FAKTUR_PDF As String = String.Empty
        Private EDOC_CREDITMEMORETUR_PDF As String = String.Empty
        Private EDOC_TTTDEPOSITC2_PDF As String = String.Empty
        Private EDOC_PENALTIPENGEMBALIANBARANG_PDF As String = String.Empty
        Private EDOC_EOPACKLISTCASE_PDF As String = String.Empty
        Private EDOC_ROPACKLISTCASE_PDF As String = String.Empty
        Private EDOC_CREDITMEMORETURMANUAL_PDF As String = String.Empty

        'SSE
        Private DEPOSIT_PDF As String = String.Empty
        Private KINTB_PDF As String = String.Empty
        Private NOTARETUR_PDF As String = String.Empty
        Private KWITANSI_PDF As String = String.Empty
        Private SPNOTARETUR_FILE_NAME As String = String.Empty

        Private _MSPEXRegistrationParser As MSPEXRegistrationParser

        Private _MSPRegistrationSOParser As MSPRegistrationSOParser
        Private _MSPRegistrationINVParser As MSPRegistrationINVParser
        Private _MSPClaimDocumentParser As MSPClaimDocumentParser

#End Region

#Region "Constructor/Initalization"

        Public Sub New()
            InitFileExtension()
        End Sub

        Public Sub New(ByVal filename As String, ByVal destFolder As String)
            _fileName = filename
            _destFolder = destFolder
            InitFileExtension()
        End Sub

        Private Sub InitFileExtension()
            _randomFileName = Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond

            'email error conf
            smtp = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
            sender = KTB.DNet.Lib.WebConfig.GetValue("ErrorSender")
            rec = KTB.DNet.Lib.WebConfig.GetValue("ErrorReceiver")
            subject = KTB.DNet.Lib.WebConfig.GetValue("ErrorSubject")

            'Finish Unit Extention 
            _SAPDNETTESTING_FILE_NAME = ConfigurationSettings.AppSettings("SAPDNETTESTING_FILE_NAME")
            CONTRACT_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("CONTRACT_FILE_NAME")
            PROPOSE_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("PROPOSE_FILE_NAME")
            CHASSIS_MASTER_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("CHASSIS_MASTER_FILE_NAME")
            PURCHASE_ORDER_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("PURCHASE_ORDER_FILE_NAME")
            SO_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("SO_FILE_NAME")
            'SO_FILE_FOLDER = KTB.DNet.Lib.WebConfig.GetValue("SO_FILE_FOLDER")
            MO_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("MO_FILE_NAME")

            'Nambah Configuration : Hari


            PAYMENT_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("PAYMENT_FILE_NAME")
            GYRO_FACTORING_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("GYRO_FACTORING_FILE_NAME")
            DO_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("DO_FILE_NAME")
            ACTUAL_GI_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("ACTUAL_GI_FILE_NAME")
            WSC_STATUS_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("WSC_STATUS_FILE_NAME")
            WSC_STATUS_FILE_NAME_BB = KTB.DNet.Lib.WebConfig.GetValue("WSC_STATUS_FILE_NAME_BB")
            PO_BLOCK_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("PO_BLOCK_FILE_NAME")
            FREE_SERVICE_STATUS_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("FREE_SERVICE_STATUS_FILE_NAME")
            FREE_SERVICE_STATUS_FILE_NAME_BB = KTB.DNet.Lib.WebConfig.GetValue("FREE_SERVICE_STATUS_FILE_NAME_BB")
            PDI_STATUS_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("PDI_STATUS_FILE_NAME")
            LABOR_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("LABOR_FILE_NAME")
            INVOICE_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("INVOICE_FILE_NAME")

            'eQUIPMENT
            EQUIPMENT_PRICE_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("EQUIPMENT_PRICE_FILE_NAME")
            EQUIPMENT_MASTER_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("EQUIPMENT_MASTER_FILE_NAME")
            BOM_MASTER_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("BOM_MASTER_FILE_NAME")


            'Sparepart Extention 

            NORMAL_PENDINGRO_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("NORMAL_PENDINGRO_FILE_NAME")
            BO_PENDINGRO_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("BO_PENDINGRO_FILE_NAME")
            BO_ESTIMATERO_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("BO_ESTIMATERO_FILE_NAME")
            BO_ESTIMATEEO_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("BO_ESTIMATEEO_FILE_NAME")
            BO_OUTSTANDINGRO_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("BO_OUTSTANDINGRO_FILE_NAME")
            BO_EOUTSTANDINGEO_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("BO_EOUTSTANDINGEO_FILE_NAME")
            BO_EOUTSTANDINGEORO_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("BO_EOUTSTANDINGEORO_FILE_NAME")


            PARTMASTER_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("PARTMASTER_FILE_NAME")
            CHECKLIST_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("CHECKLIST_FILE_NAME")
            EMERGENCY_ESTIMATE_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("EMERGENCY_ESTIMATE_FILE_NAME")
            REGULER_ESTIMATE_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("REGULER_ESTIMATE_FILE_NAME")
            ORDERSTATUS_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("ORDERSTATUS_FILE_NAME")
            ANNUAL_DISCOUNT_ACHIEVEMENT_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("ANNUAL_DISCOUNT_ACHIEVEMENT_FILE_NAME")
            DEPOSIT_C2_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("DEPOSIT_C2_FILE_NAME")
            DEPOSIT_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("DEPOSIT_FILE_NAME")
            SPAREPART_PO_BILLING_RECAP_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("SPAREPART_PO_BILLING_RECAP_FILE_NAME")
            SPAREPART_CEILING_FILENAME = KTB.DNet.Lib.WebConfig.GetValue("SPAREPART_CEILING_FILENAME")
            DCSPPENALTY_FILENAME = KTB.DNet.Lib.WebConfig.GetValue("DCSPPENALTY_FILENAME")

            'Montly Document
            DEPOSIT_B_REPORT_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("DEPOSIT_B_REPORT_FILE_NAME")
            KWITANSI_WARANTY_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("KWITANSI_WARANTY_FILE_NAME")
            KWITANSI_FREE_SERVICE_CAMPAIGN_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("KWITANSI_FREE_SERVICE_CAMPAIGN_FILE_NAME")
            PDI_LETTER_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("PDI_LETTER_FILE_NAME")
            PDI_LETTER2_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("PDI_LETTER2_FILE_NAME")
            FREE_SERVICE_LETTER_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("FREE_SERVICE_LETTER_FILE_NAME")
            FREE_SERVICE_CAMPAIGN_LETTER_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("FREE_SERVICE_CAMPAIGN_LETTER_FILE_NAME")
            WARANTY_LETTER_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("WARANTY_LETTER_FILE_NAME")
            WARANTY_STATUS_LIST_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("WARANTY_STATUS_LIST_FILE_NAME")


            FL_LETTER_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("FL_LETTER_FILE_NAME")
            KFL_LETTER_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("KFL_LETTER_FILE_NAME")

            FREE_MAINTENANCE_LETTER_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("FREE_MAINTENANCE_LETTER_FILE_NAME")
            KWITANSI_FREE_MAINTENANCE_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("KWITANSI_FREE_MAINTENANCE_FILE_NAME")
            Kwitansi_Warranty_Spare_Part_Accessories_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("Kwitansi_Warranty_Spare_Part_Accessories_FILE_NAME")
            Kwitansi_Free_Maintenance_and_Campaign_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("Kwitansi_Free_Maintenance_and_Campaign_FILE_NAME")
            Free_Maintenance_and_campaign_letter_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("Free_Maintenance_and_campaign_letter_FILE_NAME")

            ' Enhancement
            REJECTEDSPAREPARTPO = KTB.DNet.Lib.WebConfig.GetValue("REJECTEDSPAREPARTPO")

            KODE_KERJA_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("KODE_KERJA_FILE_NAME")
            KODE_POSISI_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("KODE_POSISI_FILE_NAME")
            KODE_POSISI_WSC_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("KODE_POSISI_WSC_FILE_NAME")

            'Phase 4
            BILLING_RETUR_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("BILLING_RETUR_FILE_NAME")
            CUSTOMER_DATA_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("CUSTOMER_DATA_FILE_NAME")
            CUSTOMER_DEALER_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("CUSTOMER_DEALER_FILE_NAME")
            CUSTOMER_REQUEST_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("CUSTOMER_REQUEST_FILE_NAME")
            PM_STATUS_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("PM_STATUS_FILE_NAME")
            DEPOSIT_A_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("DEPOSIT_A_FILE_NAME")
            DEPOSITA_INTEREST_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("DEPOSITA_INTEREST_FILE_NAME")
            DEBIT_NOTE_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("DEBIT_NOTE_FILE_NAME")
            DEALER_BANKACCOUNT_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("DEALER_BANKACCOUNT_FILE_NAME")
            PERIODICAL_MAINTENANCE_LETTER_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("PERIODICAL_MAINTENANCE_LETTER_FILE_NAME")
            PENDING_ORDER_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("PENDING_ORDER_FILE_NAME")
            KUDEPB_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("KUDEPB_FILE_NAME")
            INDEPB_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("INDEPB_FILE_NAME")
            PAYMENT_OBLIGATION_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("PAYMENT_OBLIGATION_FILE_NAME")
            PAYMENT_OBLIGATION_STATUS_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("PAYMENT_OBLIGATION_STATUS_FILE_NAME")

            PAYMENT_OBLIGATION_STATUS_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("PAYMENT_OBLIGATION_STATUS_FILE_NAME")

            LIST_INVOICE_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("LIST_INVOICE_FILE_NAME")

            JV_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("JV_FILE_NAME")
            JV_CAIR_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("JV_CAIR_FILE_NAME")

            JV_CANCEL_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("JV_CANCEL_FILE_NAME")

            'Remaining Module
            CREDIT_CEILING_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("CREDIT_CEILING_FILE_NAME")

            'SPAF Optimazation
            SPAF_FILE = KTB.DNet.Lib.WebConfig.GetValue("SPAF_FILE")

            'Factoring
            CESSIE_DATA = KTB.DNet.Lib.WebConfig.GetValue("CESSIE_DATA")
            CESSIE_DOCUMENT = KTB.DNet.Lib.WebConfig.GetValue("CESSIE_DOCUMENT")
            CESSIE_DOCUMENT2 = KTB.DNet.Lib.WebConfig.GetValue("CESSIE_DOCUMENT2")

            'Parking Fee / Penalty Parkir
            PARKINGFEE_FILENAME = KTB.DNet.Lib.WebConfig.GetValue("PARKINGFEE_FILENAME")
            PARKINGFEE_DEBITCHARGE_FILENAME = KTB.DNet.Lib.WebConfig.GetValue("PARKINGFEE_DEBITCHARGE_FILENAME")
            PARKINGFEE_DEBITMEMO_FILENAME = KTB.DNet.Lib.WebConfig.GetValue("PARKINGFEE_DEBITMEMO_FILENAME")
            ParkingFeeDirectory = KTB.DNet.Lib.WebConfig.GetValue("ParkingFeeDirectory")
            MSPExtDebitMemoDirectory = KTB.DNet.Lib.WebConfig.GetValue("MSPExtDirectory")
            PARKINGFEE_JVRETURN_FILENAME = KTB.DNet.Lib.WebConfig.GetValue("PARKINGFEE_JVRETURN_FILENAME")
            PARKINGFEE_JVNUMBER_FILENAME = KTB.DNet.Lib.WebConfig.GetValue("PARKINGFEE_JVNUMBER_FILENAME")
            PARKINGFEE_FAKTURPAJAK_FILENAME = KTB.DNet.Lib.WebConfig.GetValue("PARKINGFEE_FAKTURPAJAK_FILENAME")

            JV_CAMPAIGN_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("JV_CAMPAIGN_FILE_NAME")

            'DEPOSIT_B 
            DEPOSIT_B_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("DEPOSIT_B_FILE_NAME")
            DEPOSIT_B_INTEREST_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("DEPOSIT_B_INTEREST_FILE_NAME")
            DEPOSIT_B_DEBITNOTE_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("DEPOSIT_B_DEBITNOTE_FILE_NAME")
            DEPOSIT_B_SO_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("DEPOSIT_B_SO_FILE_NAME")
            DEPOSIT_B_JV_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("DEPOSIT_B_JV_FILE_NAME")
            DEPOSIT_B_JVCAIR_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("DEPOSIT_B_JVCAIR_FILE_NAME")

            'DEPOSIT C
            DEPOSIT_C_INTEREST_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("DEPOSIT_C_INTEREST_FILE_NAME")

            'MOTORPOOL/LOGISTIC COST
            LOGISTIC_DEBITCHARGE_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("LOGISTIC_DEBITCHARGE_FILE_NAME")
            LOGISTIC_DEBITMEMO_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("LOGISTIC_DEBITMEMO_FILE_NAME")
            LOGISTIC_FAKTURPAJAK_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("LOGISTIC_FAKTURPAJAK_FILE_NAME")
            LOGISTIC_JVRETURN_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("LOGISTIC_JVRETURN_FILE_NAME")

            ' MSP 
            MSP_REGISTRATION_DC = KTB.DNet.Lib.WebConfig.GetValue("MSP_REGISTRATION_DC")
            MSP_REGISTRATION_DM = KTB.DNet.Lib.WebConfig.GetValue("MSP_REGISTRATION_DM")
            MSP_CLAIM_LETTER = KTB.DNet.Lib.WebConfig.GetValue("MSP_CLAIM_LETTER")
            MSP_CLAIM_KUITANSI = KTB.DNet.Lib.WebConfig.GetValue("MSP_CLAIM_KUITANSI")

            ' MSP EXT
            MSP_EXT_REGISTRATION_PDF = KTB.DNet.Lib.WebConfig.GetValue("MSP_EXT_REGISTRATION_PDF")

            'EDOC
            EDOC_FAKTUR_PDF = KTB.DNet.Lib.WebConfig.GetValue("EDOC_FAKTUR_PDF")
            EDOC_CREDITMEMORETUR_PDF = KTB.DNet.Lib.WebConfig.GetValue("EDOC_CREDITMEMORETUR_PDF")
            EDOC_TTTDEPOSITC2_PDF = KTB.DNet.Lib.WebConfig.GetValue("EDOC_TTTDEPOSITC2_PDF")
            EDOC_PENALTIPENGEMBALIANBARANG_PDF = KTB.DNet.Lib.WebConfig.GetValue("EDOC_PENALTIPENGEMBALIANBARANG_PDF")
            EDOC_EOPACKLISTCASE_PDF = KTB.DNet.Lib.WebConfig.GetValue("EDOC_EOPACKLISTCASE_PDF")
            EDOC_ROPACKLISTCASE_PDF = KTB.DNet.Lib.WebConfig.GetValue("EDOC_ROPACKLISTCASE_PDF")
            EDOC_CREDITMEMORETURMANUAL_PDF = KTB.DNet.Lib.WebConfig.GetValue("EDOC_CREDITMEMORETURMANUAL_PDF")

            'SSE
            DEPOSIT_PDF = KTB.DNet.Lib.WebConfig.GetValue("DEPOSIT_PDF")
            KINTB_PDF = KTB.DNet.Lib.WebConfig.GetValue("KINTB_PDF")

            NOTARETUR_PDF = KTB.DNet.Lib.WebConfig.GetValue("NOTARETUR_PDF")
            SPNOTARETUR_FILE_NAME = KTB.DNet.Lib.WebConfig.GetValue("SPNOTARETUR_FILE_NAME")

            Dim strLog As String = KTB.DNet.Lib.WebConfig.GetValue("IsLogToSysLog")
            If strLog.Trim.ToUpper = "Y" Then
                _isLogToSysLog = True
            Else
                _isLogToSysLog = False
            End If
        End Sub

#End Region

#Region "Property"

        Public Property LOG() As EventLog
            Get
                Return _log
            End Get
            Set(ByVal Value As EventLog)
                _log = Value
                Dim Src As String = ""
                Src = System.Configuration.ConfigurationManager.AppSettings("CompanyCode").ToString()
                Try
                    _log.Source = String.Format("{0}.DNet.Source", Src)
                    _log.Log = String.Format("{0}.DNet.Log", Src)
                Catch ex As Exception
                    _log.Source = "MMC.DNet.Source"
                    _log.Log = "MMC.DNet.Log"
                End Try

            End Set
        End Property

        Public Property FileName() As String
            Get
                Return _fileName
            End Get
            Set(ByVal Value As String)
                _fileName = Value
            End Set
        End Property

        Public Property DestinationFolder() As String
            Get
                Return _destFolder
            End Get
            Set(ByVal Value As String)
                _destFolder = Value
            End Set
        End Property

        Public Property UseSignal() As Boolean
            Get
                Return _useSignal
            End Get
            Set(ByVal Value As Boolean)
                _useSignal = Value
            End Set
        End Property

#End Region

#Region "Public Method"

        Public Sub Work(ByVal obj As Object)
            Dim Success As Boolean = False
            Dim loopCount As Int32 = 0
            Dim finfo As New FileInfo(_fileName)
            Dim fs As FileStream
            Dim objSysLog As WSMSyslogParameter = New WSMSyslogParameter(user)

            ''Ali 20150416
            ''add try catchs
            Try
                If finfo.Attributes.ReadOnly = FileAttributes.ReadOnly Then
                    finfo.Attributes = FileAttributes.Normal
                End If

                LOG = New EventLog

                While (Not Success And loopCount < 100)
                    Try
                        fs = finfo.OpenWrite()
                        Success = True
                    Catch
                        Success = False
                        loopCount += loopCount
                        Thread.Sleep(50)
                    Finally
                        If Not fs Is Nothing Then
                            fs.Close()
                            fs = Nothing
                        End If
                    End Try
                End While

                If (Success) Then
                    DoingJob()
                    'GC.Collect()
                Else
                    SendingEmail(_fileName, New Exception("Error while reading file 447"))
                    LOG.WriteEntry("Error while reading file ", EventLogEntryType.Error)
                    objSysLog.LogErrorToSyslog("Error while reading file" & finfo.Name, "wsm-worker", "/KTB.DNet.Parser/worker.vb", "", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, "", -1, "parser-read-file")
                End If
            Catch ex As Exception
                Try
                    SendingEmail(_fileName, ex)
                    LOG.WriteEntry("Error while reading file ", EventLogEntryType.Error)
                    objSysLog.LogErrorToSyslog("Error while reading file" & finfo.Name, "wsm-worker", "/KTB.DNet.Parser/worker.vb", "", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, "", -1, "parser-read-file")

                Catch exx As Exception
                    'If Not EventLog.SourceExists("WSM") Then
                    '    EventLog.CreateEventSource("WSM", "WSM")
                    'End If
                    'EventLog.WriteEntry("WSM", exx.Message.ToString())
                Finally


                End Try

            End Try

        End Sub

        Public Function WaitFileUnLock(ByVal fName As String) As Boolean
            Dim Success As Boolean = False
            Dim loopCount As Int32 = 0
            Dim lockFinfo As New FileInfo(fName)
            Dim lockFs As FileStream
            If lockFinfo.Attributes.ReadOnly = FileAttributes.ReadOnly Then
                lockFinfo.Attributes = FileAttributes.Normal
            End If
            LOG = New EventLog
            Thread.Sleep(1000)
            While (Not Success And loopCount < 100)
                Try
                    lockFs = lockFinfo.OpenWrite()
                    Success = True
                Catch
                    Success = False
                    loopCount += loopCount
                    Thread.Sleep(20)
                Finally
                    If Not lockFs Is Nothing Then
                        lockFs.Close()
                        lockFs = Nothing
                    End If
                End Try
            End While
            Return Success
        End Function
#End Region

#Region "Private Method"

        Private Sub SendingEmail(ByVal fName As String, ByVal ErrorEx As Exception)

            Dim Src As String = IIf(Not IsNothing(ErrorEx.InnerException), " ; SRC: " & ErrorEx.InnerException.Source, String.Empty)

            Dim body As String = "Error Parsing " & fName & " Error : " & ErrorEx.Message.ToString & Src
            Dim _DetMail As DNetMail = New DNetMail(smtp)
            Dim objSysLog As WSMSyslogParameter = New WSMSyslogParameter(user)
            Try
                _DetMail.sendMail(rec, sender, sender, subject, Web.Mail.MailFormat.Text, body)
            Catch ex As Exception
                'LOG.WriteEntry("Send Email Error " & ex.Message & " ---> " & fName, EventLogEntryType.Error)
                objSysLog.LogErrorToSyslog("Sending Email", "wsm-worker", "/KTB.DNet.Parser/worker.vb", "", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, "", -1, "parser-send-email")
                'objSysLog.LogErrorToSyslog("Sending Email", "WSM Worker", subject, "", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, "", -1)
            End Try
        End Sub

        Private Sub FinishUnitJob(ByVal finfo As FileInfo)
            Dim newFileInfo As FileInfo
            Dim fileFolder As String
            Dim fileType As String
            If finfo.Name.Length > 5 Then
                fileType = finfo.Name.Substring(0, 6).ToUpper
            Else
                fileType = finfo.Name
            End If
            If finfo.Extension.ToUpper.Equals(".TXT") Or finfo.Extension.ToUpper.Equals(".CSV") Then
                Select Case fileType
                    Case Is = CONTRACT_FILE_NAME
                        _ContractParser = New ContractParser
                        _ContractParser.SourceName = fileType
                        _ContractParser.BlockName = "FinishUnit"
                        _ContractParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _ContractParser Is Nothing Then
                            _ContractParser = Nothing
                        End If
                    Case Is = PROPOSE_FILE_NAME
                        _ProposeQuantityParser = New ProposedQuantityParser
                        _ProposeQuantityParser.SourceName = fileType
                        _ProposeQuantityParser.BlockName = "FinishUnit"
                        _ProposeQuantityParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _ProposeQuantityParser Is Nothing Then
                            _ProposeQuantityParser = Nothing
                        End If
                    Case Is = CHASSIS_MASTER_FILE_NAME

                    Case Is = PURCHASE_ORDER_FILE_NAME
                        _PurchaseOrderParser = New PurchaseOrderParser
                        _PurchaseOrderParser.SourceName = fileType
                        _PurchaseOrderParser.BlockName = "FinishUnit"
                        _PurchaseOrderParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _PurchaseOrderParser Is Nothing Then
                            _PurchaseOrderParser = Nothing
                        End If
                    Case Is = PAYMENT_FILE_NAME
                        _dailyPaymentParser = New DailyPaymentParser
                        _dailyPaymentParser.SourceName = fileType
                        _dailyPaymentParser.BlockName = "FinishUnit"
                        _dailyPaymentParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _dailyPaymentParser Is Nothing Then
                            _dailyPaymentParser = Nothing
                        End If
                    Case Is = GYRO_FACTORING_FILE_NAME
                        _factoringGyroParser = New FactoringGyroParser
                        _factoringGyroParser.SourceName = fileType
                        _factoringGyroParser.BlockName = "FinishUnit"
                        _factoringGyroParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _factoringGyroParser Is Nothing Then
                            _factoringGyroParser = Nothing
                        End If
                    Case Is = DO_FILE_NAME
                        _doParser = New DOPrintParser
                        _doParser.SourceName = fileType
                        _doParser.BlockName = "FinishUnit"
                        _doParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _doParser Is Nothing Then
                            _doParser = Nothing
                        End If
                    Case Is = ACTUAL_GI_FILE_NAME
                        _ActualGIDateParser = New ActualGIDateParser
                        _ActualGIDateParser.SourceName = fileType
                        _ActualGIDateParser.BlockName = "FinishUnit"
                        _ActualGIDateParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _ActualGIDateParser Is Nothing Then
                            _ActualGIDateParser = Nothing
                        End If
                    Case Is = PO_BLOCK_FILE_NAME
                        _poBlockParser = New POBlockParser
                        _poBlockParser.SourceName = fileType
                        _poBlockParser.BlockName = "FinishUnit"
                        _poBlockParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _poBlockParser Is Nothing Then
                            _poBlockParser = Nothing
                        End If
                    Case Is = INVOICE_FILE_NAME
                        _InvoiceParser = New InvoiceParser
                        _InvoiceParser.SourceName = fileType
                        _InvoiceParser.BlockName = "FinishUnit"
                        _InvoiceParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _InvoiceParser Is Nothing Then
                            _InvoiceParser = Nothing
                        End If
                    Case Is = DEPOSIT_A_FILE_NAME
                        _DepositAParser = New DepositAParser
                        _DepositAParser.SourceName = fileType
                        _DepositAParser.BlockName = "FinishUnit"
                        _DepositAParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _DepositAParser Is Nothing Then
                            _DepositAParser = Nothing
                        End If
                    Case Is = DEALER_BANKACCOUNT_FILE_NAME
                        _DealerBankAccountParser = New DealerBankAccountParser
                        _DealerBankAccountParser.SourceName = fileType
                        _DealerBankAccountParser.BlockName = "FinishUnit"
                        _DealerBankAccountParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _DealerBankAccountParser Is Nothing Then
                            _DealerBankAccountParser = Nothing
                        End If
                    Case Is = DEBIT_NOTE_FILE_NAME
                        _DebitNoteParser = New DebitNoteParser
                        _DebitNoteParser.SourceName = fileType
                        _DebitNoteParser.BlockName = "FinishUnit"
                        _DebitNoteParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _DebitNoteParser Is Nothing Then
                            _DebitNoteParser = Nothing
                        End If
                    Case Is = CUSTOMER_DATA_FILE_NAME
                        _CustomerDataParser = New CustomerDataParser
                        _CustomerDataParser.SourceName = fileType
                        _CustomerDataParser.BlockName = "FinishUnit"
                        _CustomerDataParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _CustomerDataParser Is Nothing Then
                            _CustomerDataParser = Nothing
                        End If
                    Case Is = CUSTOMER_DEALER_FILE_NAME
                        _CustomerDealerParser = New CustomerDealerParser
                        _CustomerDealerParser.SourceName = fileType
                        _CustomerDealerParser.BlockName = "FinishUnit"
                        _CustomerDealerParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _CustomerDealerParser Is Nothing Then
                            _CustomerDealerParser = Nothing
                        End If
                    Case Is = CUSTOMER_REQUEST_FILE_NAME
                        _customerRequestParser = New CustomerRequestParser
                        _customerRequestParser.SourceName = fileType
                        _customerRequestParser.BlockName = "FinishUnit"
                        _customerRequestParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _customerRequestParser Is Nothing Then
                            _customerRequestParser = Nothing
                        End If
                    Case Is = PAYMENT_OBLIGATION_FILE_NAME
                        _PaymentObligationParser = New PaymentObligationParser
                        _PaymentObligationParser.SourceName = fileType
                        _PaymentObligationParser.BlockName = "FinishUnit"
                        _PaymentObligationParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _PaymentObligationParser Is Nothing Then
                            _PaymentObligationParser = Nothing
                        End If
                    Case Is = PAYMENT_OBLIGATION_STATUS_FILE_NAME
                        _PaymentObligationStatusParser = New PaymentObligationStatusParser
                        _PaymentObligationStatusParser.SourceName = fileType
                        _PaymentObligationStatusParser.BlockName = "FinishUnit"
                        _PaymentObligationStatusParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _PaymentObligationStatusParser Is Nothing Then
                            _PaymentObligationStatusParser = Nothing
                        End If
                    Case Is = DEPOSITA_INTEREST_FILE_NAME
                        'Indra
                        _DepositAInterestParser = New DepositAInterestParser
                        _DepositAInterestParser.SourceName = fileType
                        _DepositAInterestParser.BlockName = "FinishUnit"
                        _DepositAInterestParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _DepositAInterestParser Is Nothing Then
                            _DepositAInterestParser = Nothing
                        End If
                    Case Is = LIST_INVOICE_FILE_NAME
                        _ListInvoiceParser = New ListInvoiceParser
                        _ListInvoiceParser.SourceName = fileType
                        _ListInvoiceParser.BlockName = "FinishUnit"
                        _ListInvoiceParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _ListInvoiceParser Is Nothing Then
                            _ListInvoiceParser = Nothing
                        End If
                    Case Is = JV_FILE_NAME
                        _JVParser = New JVParser
                        _JVParser.SourceName = fileType
                        _JVParser.BlockName = "FinishUnit"
                        _JVParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _JVParser Is Nothing Then
                            _JVParser = Nothing
                        End If

                    Case Is = JV_CANCEL_FILE_NAME
                        _JVCancelParser = New JVCancelParser
                        _JVCancelParser.SourceName = fileType
                        _JVCancelParser.BlockName = "FinishUnit"
                        _JVCancelParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _JVCancelParser Is Nothing Then
                            _JVCancelParser = Nothing
                        End If


                    Case Is = JV_CAIR_FILE_NAME
                        _JVCairParser = New JVCairParser
                        _JVCairParser.SourceName = fileType
                        _JVCairParser.BlockName = "FinishUnit"
                        _JVCairParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _JVCairParser Is Nothing Then
                            _JVCairParser = Nothing
                        End If


                    Case Is = CREDIT_CEILING_FILE_NAME
                        _CreditCeilingParser = New CreditCeilingParser
                        _CreditCeilingParser.SourceName = fileType
                        _CreditCeilingParser.BlockName = "FinishUnit"
                        _CreditCeilingParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _CreditCeilingParser Is Nothing Then
                            _CreditCeilingParser = Nothing
                        End If

                    Case Is = SPAF_FILE
                        _SPAFParser = New SPAFParser
                        _SPAFParser.SourceName = fileType
                        _SPAFParser.BlockName = "FinishUnit"
                        _SPAFParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _SPAFParser Is Nothing Then
                            _SPAFParser = Nothing
                        End If
                    Case Is = CESSIE_DATA
                        _CessieParser = New CessieParser
                        _CessieParser.SourceName = fileType
                        _CessieParser.BlockName = "FinishUnit"
                        _CessieParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _CessieParser Is Nothing Then
                            _CessieParser = Nothing
                        End If
                    Case Is = CESSIE_DOCUMENT
                        CopyFileToWebServer(finfo.FullName, KTB.DNet.Lib.WebConfig.GetValue("FACTORING_FOLDER"))
                        _CessieDocParser = New CessieDocParser
                        _CessieDocParser.SourceName = fileType
                        _CessieDocParser.BlockName = "FinishUnit"
                        _CessieDocParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _CessieDocParser Is Nothing Then
                            _CessieDocParser = Nothing
                        End If
                    Case Is = Me.PARKINGFEE_FILENAME
                        _ParkingFeeParser = New ParkingFeeParser
                        _ParkingFeeParser.SourceName = fileType
                        _ParkingFeeParser.BlockName = "FinishUnit"
                        _ParkingFeeParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _ParkingFeeParser Is Nothing Then
                            _ParkingFeeParser = Nothing
                        End If
                    Case Is = Me.PARKINGFEE_JVRETURN_FILENAME
                        Me._ParkingFeeJVReturParser = New ParkingFeeJVReturnParser
                        Me._ParkingFeeJVReturParser.SourceName = fileType
                        Me._ParkingFeeJVReturParser.BlockName = "FinishUnit"
                        Me._ParkingFeeJVReturParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not Me._ParkingFeeJVReturParser Is Nothing Then
                            Me._ParkingFeeJVReturParser = Nothing
                        End If
                    Case Is = Me.PARKINGFEE_JVNUMBER_FILENAME
                        Me._ParkingFeeJVNumberParser = New ParkingFeeJVNumberParser
                        Me._ParkingFeeJVNumberParser.SourceName = fileType
                        Me._ParkingFeeJVNumberParser.BlockName = "FinishUnit"
                        Me._ParkingFeeJVNumberParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not Me._ParkingFeeJVNumberParser Is Nothing Then
                            Me._ParkingFeeJVNumberParser = Nothing
                        End If
                    Case Is = Me.PARKINGFEE_FAKTURPAJAK_FILENAME
                        Me._ParkingFeeFakturPajakParser = New ParkingFeeFakturPajakParser
                        Me._ParkingFeeFakturPajakParser.SourceName = fileType
                        Me._ParkingFeeFakturPajakParser.BlockName = "FinishUnit"
                        Me._ParkingFeeFakturPajakParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not Me._ParkingFeeFakturPajakParser Is Nothing Then
                            Me._ParkingFeeFakturPajakParser = Nothing
                        End If

                    Case Is = JV_CAMPAIGN_FILE_NAME
                        _benefitClaimParser = New BenefitClaimParser
                        _benefitClaimParser.SourceName = fileType
                        _benefitClaimParser.BlockName = "FinishUnit"
                        _benefitClaimParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _benefitClaimParser Is Nothing Then
                            _benefitClaimParser = Nothing
                        End If

                    Case Is = LOGISTIC_DEBITCHARGE_FILE_NAME
                        _LogisticDCParser = New LogisticDCParser
                        _LogisticDCParser.SourceName = fileType
                        _LogisticDCParser.BlockName = "FinishUnit"
                        _LogisticDCParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _LogisticDCParser Is Nothing Then
                            _LogisticDCParser = Nothing
                        End If

                    Case Is = LOGISTIC_DEBITMEMO_FILE_NAME
                        _LogisticDCParser = New LogisticDCParser
                        _LogisticDCParser.SourceName = fileType
                        _LogisticDCParser.BlockName = "FinishUnit"
                        _LogisticDCParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _LogisticDCParser Is Nothing Then
                            _LogisticDCParser = Nothing
                        End If

                    Case Is = LOGISTIC_FAKTURPAJAK_FILE_NAME
                        _LogisticFakturParser = New LogisticFakturParser
                        _LogisticFakturParser.SourceName = fileType
                        _LogisticFakturParser.BlockName = "FinishUnit"
                        _LogisticFakturParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _LogisticFakturParser Is Nothing Then
                            _LogisticFakturParser = Nothing
                        End If

                    Case Is = LOGISTIC_JVRETURN_FILE_NAME
                        _LogisticJVReturParser = New LogisticJVReturParser
                        _LogisticJVReturParser.SourceName = fileType
                        _LogisticJVReturParser.BlockName = "FinishUnit"
                        _LogisticJVReturParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _LogisticJVReturParser Is Nothing Then
                            _LogisticJVReturParser = Nothing
                        End If


                End Select

            End If

            If finfo.Extension.ToUpper.Equals(".PDF") Then
                Select Case fileType
                    Case Is = SO_FILE_NAME
                        CopyFileToWebServer(finfo.FullName, KTB.DNet.Lib.WebConfig.GetValue("WEBSOFILE_FOLDER"))
                    Case Is = MO_FILE_NAME
                        CopyFileToWebServer(finfo.FullName, KTB.DNet.Lib.WebConfig.GetValue("WEBMOFILE_FOLDER"))
                    Case Is = LIST_INVOICE_FILE_NAME
                        CopyFileToWebServer(finfo.FullName, KTB.DNet.Lib.WebConfig.GetValue("INVOICE_FOLDER"))
                    Case CESSIE_DOCUMENT, CESSIE_DOCUMENT2
                        CopyFileToWebServer(finfo.FullName, KTB.DNet.Lib.WebConfig.GetValue("FACTORING_FOLDER"))
                        _CessieDocParser = New CessieDocParser
                        _CessieDocParser.SourceName = fileType
                        _CessieDocParser.BlockName = "FinishUnit"
                        _CessieDocParser.ParseWithTransaction(finfo.Name, _user)
                        If Not _CessieDocParser Is Nothing Then
                            _CessieDocParser = Nothing
                        End If
                    Case Me.PARKINGFEE_DEBITMEMO_FILENAME
                        CopyFileToWebServer(finfo.FullName, Me.ParkingFeeDirectory)
                        Me._ParkingFeeMemoDocParser = New ParkingFeeMemoDocParser
                        Me._ParkingFeeMemoDocParser.SourceName = fileType
                        Me._ParkingFeeMemoDocParser.BlockName = "FinishUnit"
                        Me._ParkingFeeMemoDocParser.ParseWithTransaction(finfo.Name, _user)
                        If Not Me._ParkingFeeMemoDocParser Is Nothing Then
                            Me._ParkingFeeMemoDocParser = Nothing
                        End If
                    Case Me.PARKINGFEE_DEBITCHARGE_FILENAME
                        CopyFileToWebServer(finfo.FullName, Me.ParkingFeeDirectory)
                        Me._ParkingFeeMemoDocParser = New ParkingFeeMemoDocParser
                        Me._ParkingFeeMemoDocParser.SourceName = fileType
                        Me._ParkingFeeMemoDocParser.BlockName = "FinishUnit"
                        Me._ParkingFeeMemoDocParser.ParseWithTransaction(finfo.Name, _user)
                        If Not Me._ParkingFeeMemoDocParser Is Nothing Then
                            Me._ParkingFeeMemoDocParser = Nothing
                        End If

                    Case LOGISTIC_DEBITCHARGE_FILE_NAME
                        CopyFileToWebServer(finfo.FullName, KTB.DNet.Lib.WebConfig.GetValue("LOGISTIC_FOLDER"))

                    Case LOGISTIC_DEBITMEMO_FILE_NAME
                        CopyFileToWebServer(finfo.FullName, KTB.DNet.Lib.WebConfig.GetValue("LOGISTIC_FOLDER"))
                End Select
            End If
            fileFolder = _destFolder & "\" & GetTypeDirectory(finfo, "FINISHUNIT") & GetHistoryDetailDirectory() & _randomFileName & finfo.Name
            newFileInfo = New FileInfo(fileFolder)
            SyncLock (newFileInfo)
                If Not newFileInfo.Directory.Exists Then
                    newFileInfo.Directory.Create()
                End If
            End SyncLock
            Try
                finfo.MoveTo(fileFolder)
            Catch ex As Exception
                Throw New Exception("Fail move file to history folder, but parsing is done : " & ex.Message.ToString)
            End Try
        End Sub

        Private Sub SparePartJob(ByVal finfo As FileInfo)
            Dim newFileInfo As FileInfo
            Dim fileFolder As String
            Dim fileType As String
            If finfo.Name.Length >= 7 Then
                If Right(Left(finfo.Name.Trim, finfo.Name.Trim.Length - 4), 3).ToUpper = CHECKLIST_FILE_NAME Then
                    _checkListParter = New SPPOChecklistParser
                    _checkListParter.ParseWithTransaction(_fileName, _user)
                    If Not _checkListParter Is Nothing Then
                        _checkListParter = Nothing
                    End If
                Else
                    If finfo.Name.Length > 8 Then
                        fileType = finfo.Name.Substring(0, 9).ToUpper
                    Else
                        fileType = finfo.Name
                    End If
                    Select Case fileType
                        Case Is = DEPOSIT_C_INTEREST_FILE_NAME
                            _DepositCInterestParser = New DepositCInterestParser
                            _DepositCInterestParser.SourceName = fileType
                            _DepositCInterestParser.BlockName = "Service"
                            _DepositCInterestParser.ParseWithTransaction(finfo.FullName, _user)
                            If Not _DepositCInterestParser Is Nothing Then
                                _DepositCInterestParser = Nothing
                            End If
                        Case Is = NORMAL_PENDINGRO_FILE_NAME
                            _pendingParser = New SPPendingOrderParser
                            _pendingParser.SourceName = fileType
                            _pendingParser.BlockName = "SparePart"

                            _pendingParser.ParseWithTransaction(_fileName, _user)
                            If Not _pendingParser Is Nothing Then
                                _pendingParser = Nothing
                            End If
                        Case Is = BO_PENDINGRO_FILE_NAME
                            _pendingBOParser = New SPPendingOrderBOParser
                            _pendingBOParser.SourceName = fileType
                            _pendingBOParser.BlockName = "SparePart"

                            _pendingBOParser.ParseWithTransaction(_fileName, _user)
                            If Not _pendingBOParser Is Nothing Then
                                _pendingBOParser = Nothing
                            End If
                        Case Is = BO_OUTSTANDINGRO_FILE_NAME, BO_EOUTSTANDINGEO_FILE_NAME, BO_EOUTSTANDINGEORO_FILE_NAME
                            _outstandingOrderParser = New SPOutstandingOrderParser
                            _outstandingOrderParser.SourceName = fileType
                            _outstandingOrderParser.BlockName = "SparePart"

                            _outstandingOrderParser.ParseWithTransaction(_fileName, _user)
                            If Not _outstandingOrderParser Is Nothing Then
                                _outstandingOrderParser = Nothing
                            End If
                        Case Is = BO_ESTIMATERO_FILE_NAME, BO_ESTIMATEEO_FILE_NAME
                            _estimateParser = New SPPOEEstimateParser
                            _estimateParser.DocumentType = "B"
                            _estimateParser.SourceName = fileType
                            _estimateParser.BlockName = "SparePart"
                            _estimateParser.ParseWithTransaction(_fileName, _user)
                            If Not _estimateParser Is Nothing Then
                                _estimateParser = Nothing
                            End If

                        Case Is = PARTMASTER_FILE_NAME
                            _sparePartMasterParser = New SparePartMasterParser
                            _sparePartMasterParser.SourceName = fileType
                            _sparePartMasterParser.BlockName = "SparePart"
                            _sparePartMasterParser.ParseWithTransaction(_fileName, _user)
                            If Not _sparePartMasterParser Is Nothing Then
                                _sparePartMasterParser = Nothing
                            End If
                        Case Is = EMERGENCY_ESTIMATE_FILE_NAME
                            _estimateParser = New SPPOEEstimateParser
                            _estimateParser.DocumentType = "N"
                            _estimateParser.SourceName = fileType
                            _estimateParser.BlockName = "SparePart"
                            _estimateParser.ParseWithTransaction(_fileName, _user)
                            If Not _estimateParser Is Nothing Then
                                _estimateParser = Nothing
                            End If
                        Case Is = REGULER_ESTIMATE_FILE_NAME
                            _estimateParser = New SPPOEEstimateParser
                            _estimateParser.DocumentType = "N"
                            _estimateParser.SourceName = fileType
                            _estimateParser.BlockName = "SparePart"
                            _estimateParser.ParseWithTransaction(_fileName, _user)
                            If Not _estimateParser Is Nothing Then
                                _estimateParser = Nothing
                            End If
                        Case Is = ORDERSTATUS_FILE_NAME
                            _orderStatusParser = New SPPOStatusParser
                            _orderStatusParser.SourceName = fileType
                            _orderStatusParser.BlockName = "SparePart"
                            _orderStatusParser.ParseWithTransaction(_fileName, _user)
                            If Not _orderStatusParser Is Nothing Then
                                _orderStatusParser = Nothing
                            End If
                        Case Is = ANNUAL_DISCOUNT_ACHIEVEMENT_FILE_NAME
                            _annualDiscountAchievementParser = New AnnualDiscountAchievementParser
                            _annualDiscountAchievementParser.SourceName = fileType
                            _annualDiscountAchievementParser.BlockName = "SparePart"
                            _annualDiscountAchievementParser.ParseWithTransaction(_fileName, _user)
                            If Not _annualDiscountAchievementParser Is Nothing Then
                                _annualDiscountAchievementParser = Nothing
                            End If
                        Case Is = DEPOSIT_C2_FILE_NAME
                            _depositC2Parser = New DepositC2Parser
                            _depositC2Parser.SourceName = fileType
                            _depositC2Parser.BlockName = "SparePart"
                            _depositC2Parser.ParseWithTransaction(_fileName, _user)
                            If Not _depositC2Parser Is Nothing Then
                                _depositC2Parser = Nothing
                            End If
                        Case Is = DEPOSIT_FILE_NAME
                            _depositParser = New DepositParser
                            _depositParser.SourceName = fileType
                            _depositParser.BlockName = "SparePart"
                            _depositParser.ParseWithTransaction(_fileName, _user)
                            If Not _depositParser Is Nothing Then
                                _depositParser = Nothing
                            End If
                        Case Is = SPAREPART_PO_BILLING_RECAP_FILE_NAME
                            _sparePartPOBillingRecapParser = New SparePartPOBillingRecapParser
                            _sparePartPOBillingRecapParser.SourceName = fileType
                            _sparePartPOBillingRecapParser.BlockName = "SparePart"
                            _sparePartPOBillingRecapParser.ParseWithTransaction(_fileName, _user)
                            If Not _sparePartPOBillingRecapParser Is Nothing Then
                                _sparePartPOBillingRecapParser = Nothing
                            End If
                        Case Is = REJECTEDSPAREPARTPO
                            _rejectedSparePartPoParser = New RejectedSparePartPOParser
                            _rejectedSparePartPoParser.SourceName = fileType
                            _rejectedSparePartPoParser.BlockName = "SparePart"
                            _rejectedSparePartPoParser.ParseWithTransaction(_fileName, _user)
                            If Not _rejectedSparePartPoParser Is Nothing Then
                                _rejectedSparePartPoParser = Nothing
                            End If
                        Case Is = BILLING_RETUR_FILE_NAME
                            _BillingReturParser = New BillingReturParser
                            _BillingReturParser.SourceName = fileType
                            _BillingReturParser.BlockName = "SparePart"
                            _BillingReturParser.ParseWithTransaction(_fileName, _user)
                            If Not _BillingReturParser Is Nothing Then
                                _BillingReturParser = Nothing
                            End If
                        Case Is = PENDING_ORDER_FILE_NAME
                            _PendingOrderParser = New PendingOrderParser
                            _PendingOrderParser.SourceName = fileType & " FILE NAME"
                            _PendingOrderParser.BlockName = "SparePart"
                            _PendingOrderParser.ParseWithTransaction(_fileName, _user)
                            If Not _PendingOrderParser Is Nothing Then
                                _PendingOrderParser = Nothing
                            End If
                        Case Is = SPAREPART_CEILING_FILENAME
                            _creditMasterSPParser = New CreditMasterSPParser
                            _creditMasterSPParser.SourceName = fileType
                            _creditMasterSPParser.BlockName = "SparePart"
                            _creditMasterSPParser.ParseWithTransaction(_fileName, _user)
                            If Not _creditMasterSPParser Is Nothing Then
                                _creditMasterSPParser = Nothing
                            End If
                        Case Is = DCSPPENALTY_FILENAME
                            _DCSPPenaltyParser = New DCSPPenaltyParser
                            _DCSPPenaltyParser.SourceName = fileType
                            _DCSPPenaltyParser.BlockName = "SparePart"
                            _DCSPPenaltyParser.ParseWithTransaction(_fileName, _user)
                            If Not _DCSPPenaltyParser Is Nothing Then
                                _DCSPPenaltyParser = Nothing
                            End If

                    End Select
                End If

                'EDocument
                Select Case finfo.Name.ToUpper.Split("_")(0)
                    Case EDOC_FAKTUR_PDF, EDOC_CREDITMEMORETUR_PDF, EDOC_TTTDEPOSITC2_PDF, EDOC_PENALTIPENGEMBALIANBARANG_PDF, _
                        EDOC_EOPACKLISTCASE_PDF, EDOC_ROPACKLISTCASE_PDF, EDOC_CREDITMEMORETURMANUAL_PDF
                        _EDocumentFakturParser = New EDocumentFakturParser
                        _EDocumentFakturParser.SourceName = fileType
                        _EDocumentFakturParser.BlockName = "SparePart"
                        _EDocumentFakturParser.ParseWithTransaction(_fileName, _user)
                        If Not _EDocumentFakturParser Is Nothing Then
                            CopyFileToWebServer(finfo.FullName, KTB.DNet.Lib.WebConfig.GetValue("SPEDocumentPath") & "\" & Date.Now.Year)
                            _EDocumentFakturParser = Nothing
                        End If
                    Case DEPOSIT_PDF, KINTB_PDF
                        _DepositCInterestPDFParser = New DepositCInterestPDFParser
                        _DepositCInterestPDFParser.SourceName = fileType
                        _DepositCInterestPDFParser.BlockName = "SparePart"
                        Dim newFileName As String = _DepositCInterestPDFParser.ParseFixFormatFile(finfo.FullName, _user)
                        If Not _DepositCInterestPDFParser Is Nothing Then
                            'If _DepositCInterestPDFParser.errorMessage.ToString.Trim.Length = 0 Then
                            Dim dir As String = KTB.DNet.Lib.WebConfig.GetValue("WebServerFolder") & KTB.DNet.Lib.WebConfig.GetValue("DepositCDocumentPath") & "\" & Date.Now.Year
                            If Not Directory.Exists(dir) Then
                                Directory.CreateDirectory(dir)
                            End If
                            finfo.CopyTo(dir & "\" & newFileName, True)
                            _DepositCInterestPDFParser = Nothing
                        Else
                            Throw New Exception(_DepositCInterestPDFParser.errorMessage.ToString)
                        End If
                    Case NOTARETUR_PDF
                        _SPNOTARETURPDFParser = New SPNotaReturParser
                        _SPNOTARETURPDFParser.SourceName = fileType
                        _SPNOTARETURPDFParser.BlockName = "SparePart"
                        Dim newFileName As String = _SPNOTARETURPDFParser.ParseFixFormatFile(finfo.FullName, _user)
                        'If Not _SPNOTARETURPDFParser Is Nothing Then
                        If _SPNOTARETURPDFParser.ErrorMessage.ToString.Trim.Length = 0 Then
                            Dim destFilePath As String = KTB.DNet.Lib.WebConfig.GetValue("WebServerFolder") & KTB.DNet.Lib.WebConfig.GetValue("SpNotaReturPath") & "\" & Date.Now.Year & "\" & newFileName
                            Dim destinationFileInfo As New FileInfo(destFilePath)
                            If Not destinationFileInfo.Directory.Exists Then
                                destinationFileInfo.Directory.Create()
                            End If
                            finfo.CopyTo(destFilePath, True)
                            _SPNOTARETURPDFParser = Nothing

                            '''' menggunakan methode SAPImpersonate untuk akses ke server
                            'Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                            'Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                            'Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
                            'Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                            'Dim success As Boolean = False
                            'success = imp.Start()
                            'If success Then
                            '    Dim destFilePath As String = KTB.DNet.Lib.WebConfig.GetValue("WebServerFolder") & KTB.DNet.Lib.WebConfig.GetValue("SpNotaReturPath") & "\" & Date.Now.Year & "\" & newFileName
                            '    Dim destinationFileInfo As New FileInfo(destFilePath)
                            '    If Not destinationFileInfo.Directory.Exists Then
                            '        destinationFileInfo.Directory.Create()
                            '    End If
                            '    finfo.CopyTo(destFilePath, True)
                            '    _SPNOTARETURPDFParser = Nothing

                            '    imp.StopImpersonate()
                            '    imp = Nothing
                            'End If
                        Else
                            Throw New Exception(_SPNOTARETURPDFParser.ErrorMessage.ToString)
                        End If
                End Select

                fileFolder = _destFolder & "\" & finfo.Directory.Parent.Name.ToUpper & "\" & finfo.Directory.Name & "\" & GetHistoryDetailDirectory() & _randomFileName & finfo.Name
                newFileInfo = New FileInfo(fileFolder)
                If Not newFileInfo.Directory.Exists Then
                    newFileInfo.Directory.Create()
                End If
                Dim counter As Integer = 0
                If Left(finfo.Name.ToUpper, 7) = "SDGROUP" Then
                    Try
                        counter = 1
                        finfo.CopyTo(fileFolder, True)
                        counter = 2
                        CopyFileToWebServer(finfo.FullName, KTB.DNet.Lib.WebConfig.GetValue("WEBSPFILE_FOLDER") & "\" & finfo.Directory.Name)
                        counter = 3
                        finfo.Delete()
                        counter = 4
                    Catch ex As Exception
                        Throw New Exception("Fail Copy to local or Web Server : (" & counter & ") " & ex.Message.ToString)
                    End Try
                Else
                    Try
                        finfo.MoveTo(fileFolder)
                    Catch ex As Exception
                        Dim fullName As String = finfo.FullName
                        Dim eFileInfo As FileInfo = New FileInfo(fullName)
                        Try
                            eFileInfo.MoveTo(fileFolder)
                        Catch e As Exception
                            Throw New Exception("Fail move file to history folder *, but parsing is done : " & ex.Message.ToString)
                        End Try
                    End Try
                End If
            Else
                Try
                    finfo.MoveTo(_destFolder & "\" & _randomFileName & finfo.Name)
                Catch ex As Exception
                    Throw New Exception("Fail move file to history folder**, but parsing is done : " & ex.Message.ToString)
                End Try
            End If
        End Sub

        Private Sub ServiceJob(ByVal finfo As FileInfo)
            Dim newFileInfo As FileInfo
            Dim fileFolder As String
            Dim fileType As String
            Dim fileName As String
            fileName = finfo.Name
            If finfo.Name.Length > 5 Then
                fileType = finfo.Name.Substring(0, 6).ToUpper
            Else
                fileType = finfo.Name
            End If
            Select Case fileType
                Case Is = WSC_STATUS_FILE_NAME
                    _wscStatusParser = New WSCSStatusParser
                    _wscStatusParser.SourceName = fileType
                    _wscStatusParser.BlockName = "Service"
                    _wscStatusParser.ParseWithTransaction(finfo.FullName, _user)
                    If Not _wscStatusParser Is Nothing Then
                        _wscStatusParser = Nothing
                    End If

                Case Is = WSC_STATUS_FILE_NAME_BB
                    _wscStatusBBParser = New WSCStatusBBParser
                    _wscStatusBBParser.SourceName = fileType
                    _wscStatusBBParser.BlockName = "Service"
                    _wscStatusBBParser.ParseWithTransaction(finfo.FullName, _user)
                    If Not _wscStatusBBParser Is Nothing Then
                        _wscStatusBBParser = Nothing
                    End If
                Case Is = EQUIPMENT_PRICE_FILE_NAME
                    _equipmentPriceParser = New EquipmentPriceParser
                    _equipmentPriceParser.SourceName = fileType
                    _equipmentPriceParser.BlockName = "Service"
                    _equipmentPriceParser.ParseWithTransaction(finfo.FullName, _user)
                    If Not _equipmentPriceParser Is Nothing Then
                        _equipmentPriceParser = Nothing
                    End If
                Case Is = FREE_SERVICE_STATUS_FILE_NAME
                    _freeServiceStatusParser = New FreeServiceSynChronizationParser
                    _freeServiceStatusParser.SourceName = fileType
                    _freeServiceStatusParser.BlockName = "Service"
                    _freeServiceStatusParser.ParseWithTransaction(finfo.FullName, _user)
                    If Not _freeServiceStatusParser Is Nothing Then
                        _freeServiceStatusParser = Nothing
                    End If
                Case Is = FREE_SERVICE_STATUS_FILE_NAME_BB
                    _freeServiceStatusBBParser = New FreeServiceSynChronizationBBParser
                    _freeServiceStatusBBParser.SourceName = fileType
                    _freeServiceStatusBBParser.BlockName = "Service"
                    _freeServiceStatusBBParser.ParseWithTransaction(finfo.FullName, _user)
                    If Not _freeServiceStatusBBParser Is Nothing Then
                        _freeServiceStatusBBParser = Nothing
                    End If
                Case Is = PDI_STATUS_FILE_NAME
                    _pdiStatusParser = New PDISynChronizationParser
                    _pdiStatusParser.SourceName = fileType
                    _pdiStatusParser.BlockName = "Service"
                    _pdiStatusParser.ParseWithTransaction(finfo.FullName, _user)
                    If Not _pdiStatusParser Is Nothing Then
                        _pdiStatusParser = Nothing
                    End If
                Case Is = LABOR_FILE_NAME
                    _laborMasterParser = New LaborMasterParser
                    _laborMasterParser.SourceName = fileType
                    _laborMasterParser.BlockName = "Service"
                    _laborMasterParser.ParseWithTransaction(finfo.FullName, _user)
                    If Not _laborMasterParser Is Nothing Then
                        _laborMasterParser = Nothing
                    End If
                Case Is = DEPOSIT_B_REPORT_FILE_NAME, KWITANSI_WARANTY_FILE_NAME, KWITANSI_FREE_SERVICE_CAMPAIGN_FILE_NAME, PDI_LETTER_FILE_NAME, PDI_LETTER2_FILE_NAME, FREE_SERVICE_LETTER_FILE_NAME, FREE_SERVICE_CAMPAIGN_LETTER_FILE_NAME, WARANTY_LETTER_FILE_NAME, WARANTY_STATUS_LIST_FILE_NAME, PERIODICAL_MAINTENANCE_LETTER_FILE_NAME, KUDEPB_FILE_NAME, INDEPB_FILE_NAME, FL_LETTER_FILE_NAME, KFL_LETTER_FILE_NAME, FREE_MAINTENANCE_LETTER_FILE_NAME, KWITANSI_FREE_MAINTENANCE_FILE_NAME, Kwitansi_Warranty_Spare_Part_Accessories_FILE_NAME, _
                   Kwitansi_Free_Maintenance_and_Campaign_FILE_NAME, Free_Maintenance_and_campaign_letter_FILE_NAME
                    _MontlyDocumentParser = New MontlyDocumentParser
                    _MontlyDocumentParser.SourceName = fileType
                    _MontlyDocumentParser.BlockName = "Service"
                    _MontlyDocumentParser.SourceName = fileType
                    Dim iResult As Integer = _MontlyDocumentParser.ParseWithTransaction(finfo.FullName, _user)
                    If iResult = 1 Then
                        CopyFileToWebServerMontlyDoc(finfo.FullName, KTB.DNet.Lib.WebConfig.GetValue("WEBMONTLYDOCUMENT_FOLDER") & "\" & fileType)
                    End If
                    If Not _MontlyDocumentParser Is Nothing Then
                        _MontlyDocumentParser = Nothing
                    End If
                Case Is = SO_FILE_NAME
                    CopyFileToWebServer(finfo.FullName, KTB.DNet.Lib.WebConfig.GetValue("WEBSOFILE_FOLDER"))
                Case Is = EQUIPMENT_MASTER_FILE_NAME
                    _equipmentMasterParser = New EquipmentParser
                    _equipmentMasterParser.SourceName = fileType
                    _equipmentMasterParser.BlockName = "Service"
                    _equipmentMasterParser.ParseWithTransaction(finfo.FullName, _user)
                    If Not _equipmentMasterParser Is Nothing Then
                        _equipmentMasterParser = Nothing
                    End If
                Case Is = BOM_MASTER_FILE_NAME
                    _equipmentBOMParser = New BOMParser
                    _equipmentBOMParser.SourceName = fileType
                    _equipmentBOMParser.BlockName = "Service"
                    _equipmentBOMParser.ParseWithTransaction(finfo.FullName, _user)
                    If Not _equipmentBOMParser Is Nothing Then
                        _equipmentBOMParser = Nothing
                    End If
                Case Is = KODE_POSISI_FILE_NAME
                    _KodePosisiParser = New KodePosisiParser
                    _KodePosisiParser.SourceName = fileType
                    _KodePosisiParser.BlockName = "Service"
                    _KodePosisiParser.ParseWithTransaction(finfo.FullName, _user)
                    If Not _KodePosisiParser Is Nothing Then
                        _KodePosisiParser = Nothing
                    End If
                Case Is = KODE_KERJA_FILE_NAME
                    _KodekerjaParser = New KodeKerjaParser
                    _KodekerjaParser.SourceName = fileType
                    _KodekerjaParser.BlockName = "Service"
                    _KodekerjaParser.ParseWithTransaction(finfo.FullName, _user)
                    If Not _KodekerjaParser Is Nothing Then
                        _KodekerjaParser = Nothing
                    End If
                Case Is = KODE_POSISI_WSC_FILE_NAME
                    _kodePositionWSCParser = New KodePositionWSCParser
                    _kodePositionWSCParser.SourceName = fileType
                    _kodePositionWSCParser.BlockName = "Service"
                    _kodePositionWSCParser.ParseWithTransaction(finfo.FullName, _user)
                    If Not _kodePositionWSCParser Is Nothing Then
                        _kodePositionWSCParser = Nothing
                    End If
                Case Is = PM_STATUS_FILE_NAME
                    _pmStatusParser = New PMStatusParser
                    _pmStatusParser.SourceName = fileType
                    _pmStatusParser.BlockName = "Service"
                    _pmStatusParser.ParseWithTransaction(finfo.FullName, _user)
                    If Not _pmStatusParser Is Nothing Then
                        _pmStatusParser = Nothing
                    End If
                Case Is = DEPOSIT_B_FILE_NAME
                    _DepositBParser = New DepositBParser
                    _DepositBParser.SourceName = fileType
                    _DepositBParser.BlockName = "Service"
                    _DepositBParser.ParseWithTransaction(finfo.FullName, _user)
                    If Not _DepositBParser Is Nothing Then
                        _DepositBParser = Nothing
                    End If
                Case Is = DEPOSIT_B_INTEREST_FILE_NAME
                    _DepositBInterestParser = New DepositBInterestParser
                    _DepositBInterestParser.SourceName = fileType
                    _DepositBInterestParser.BlockName = "Service"
                    _DepositBInterestParser.ParseWithTransaction(finfo.FullName, _user)
                    If Not _DepositBInterestParser Is Nothing Then
                        _DepositBInterestParser = Nothing
                    End If
                Case Is = DEPOSIT_B_DEBITNOTE_FILE_NAME
                    _DepositBDebitNoteParser = New DepositBDebitNoteParser
                    _DepositBDebitNoteParser.SourceName = fileType
                    _DepositBDebitNoteParser.BlockName = "Service"
                    _DepositBDebitNoteParser.ParseWithTransaction(finfo.FullName, _user)
                    If Not _DepositBDebitNoteParser Is Nothing Then
                        _DepositBDebitNoteParser = Nothing
                    End If
                Case Is = DEPOSIT_B_JV_FILE_NAME
                    _DepositBJVParser = New DepositBJVParser
                    _DepositBJVParser.SourceName = fileType
                    _DepositBJVParser.BlockName = "Service"
                    _DepositBJVParser.ParseWithTransaction(finfo.FullName, _user)
                    If Not _DepositBJVParser Is Nothing Then
                        _DepositBJVParser = Nothing
                    End If
                Case Is = DEPOSIT_B_SO_FILE_NAME
                    _DepositBKewajibanHeaderParser = New DepositBKewajibanHeaderParser
                    _DepositBKewajibanHeaderParser.SourceName = fileType
                    _DepositBKewajibanHeaderParser.BlockName = "Service"
                    _DepositBKewajibanHeaderParser.ParseWithTransaction(finfo.FullName, _user)
                    If Not _DepositBKewajibanHeaderParser Is Nothing Then
                        _DepositBKewajibanHeaderParser = Nothing
                    End If
                Case Is = DEPOSIT_B_JVCAIR_FILE_NAME
                    _DepositBReceiptParser = New DepositBReceiptParser
                    _DepositBReceiptParser.SourceName = fileType
                    _DepositBReceiptParser.BlockName = "Service"
                    _DepositBReceiptParser.ParseWithTransaction(finfo.FullName, _user)
                    If Not _DepositBReceiptParser Is Nothing Then
                        _DepositBReceiptParser = Nothing
                    End If

                Case Is = MSP_REGISTRATION_DC
                    _MSPRegistrationSOParser = New MSPRegistrationSOParser
                    _MSPRegistrationSOParser.SourceName = fileType
                    _MSPRegistrationSOParser.BlockName = "Service"
                    _MSPRegistrationSOParser.ParseWithTransaction(finfo.FullName, _user)
                    If Not _MSPRegistrationSOParser Is Nothing Then
                        _MSPRegistrationSOParser = Nothing
                    End If

                Case Is = MSP_REGISTRATION_DM
                    _MSPRegistrationINVParser = New MSPRegistrationINVParser
                    _MSPRegistrationINVParser.SourceName = fileType
                    _MSPRegistrationINVParser.BlockName = "Service"
                    _MSPRegistrationINVParser.ParseWithTransaction(finfo.FullName, _user)
                    If Not _MSPRegistrationINVParser Is Nothing Then
                        _MSPRegistrationINVParser = Nothing
                    End If

                Case Is = MSP_CLAIM_LETTER
                    If finfo.Extension.ToLower = ".txt" Then
                        _MSPClaimDocumentParser = New MSPClaimDocumentParser
                        _MSPClaimDocumentParser.SourceName = fileType
                        _MSPClaimDocumentParser.BlockName = "Service"
                        _MSPClaimDocumentParser.ParseWithTransaction(finfo.FullName, _user)
                        If Not _MSPClaimDocumentParser Is Nothing Then
                            _MSPClaimDocumentParser = Nothing
                        End If
                    End If
            End Select

            fileFolder = _destFolder & "\" & GetTypeDirectory(finfo, "SERVICE") & GetHistoryDetailDirectory() & _randomFileName & finfo.Name
            newFileInfo = New FileInfo(fileFolder)
            If Not newFileInfo.Directory.Exists Then
                newFileInfo.Directory.Create()
            End If

            Try
                finfo.MoveTo(fileFolder)

                If finfo.Extension.ToUpper.Equals(".PDF") Then
                    If fileType.Substring(0, 2) = MSP_EXT_REGISTRATION_PDF Then
                        'Me._MSPEXRegistrationPDFParser = New MSPEXRegistrationPDFParser
                        'Me._MSPEXRegistrationPDFParser.SourceName = fileType
                        'Me._MSPEXRegistrationPDFParser.BlockName = "Service"
                        'Me._MSPEXRegistrationPDFParser.ParseWithTransaction(finfo.Name, _user)
                        'If Not Me._MSPEXRegistrationPDFParser Is Nothing Then
                        '    Me._MSPEXRegistrationPDFParser = Nothing
                        'End If

                        For Each item As String In KTB.DNet.Lib.WebConfig.GetValue("MSPExtDirectory").Split(";")
                            Dim finfoHistory As FileInfo = New FileInfo(fileFolder)
                            Try
                                Dim DirFinfo As DirectoryInfo = New DirectoryInfo(item)
                                If Not DirFinfo.Exists Then
                                    DirFinfo.Create()
                                End If
                                finfoHistory.CopyTo(item & "\" & fileName, True)
                            Catch ex As Exception
                                Throw ex
                            Finally
                            End Try
                        Next
                    End If
                End If

                'To handle delay on PDF spooling
                If fileType = PDI_LETTER_FILE_NAME Or fileType = DEPOSIT_B_REPORT_FILE_NAME _
                    Or fileType = KWITANSI_WARANTY_FILE_NAME Or fileType = KWITANSI_FREE_SERVICE_CAMPAIGN_FILE_NAME _
                    Or fileType = FREE_SERVICE_LETTER_FILE_NAME Or fileType = FREE_SERVICE_CAMPAIGN_LETTER_FILE_NAME _
                    Or fileType = WARANTY_LETTER_FILE_NAME Or fileType = WARANTY_STATUS_LIST_FILE_NAME _
                    Or fileType = PERIODICAL_MAINTENANCE_LETTER_FILE_NAME Or fileType = KUDEPB_FILE_NAME _
                    Or fileType = PDI_LETTER2_FILE_NAME _
                    Or fileType = INDEPB_FILE_NAME _
                    Or fileType = FL_LETTER_FILE_NAME _
                    Or fileType = KFL_LETTER_FILE_NAME _
                    Or fileType = FREE_MAINTENANCE_LETTER_FILE_NAME _
                    Or fileType = KWITANSI_FREE_MAINTENANCE_FILE_NAME _
                    Or fileType = Kwitansi_Warranty_Spare_Part_Accessories_FILE_NAME _
                    Or fileType = Kwitansi_Free_Maintenance_and_Campaign_FILE_NAME _
                    Or fileType = Free_Maintenance_and_campaign_letter_FILE_NAME Then

                    Dim webMechine As String
                    Dim _destinationFolder As String = String.Empty
                    For Each item As String In KTB.DNet.Lib.WebConfig.GetValue("WebServerFolder").Split(";")
                        _destinationFolder = item & KTB.DNet.Lib.WebConfig.GetValue("WEBMONTLYDOCUMENT_FOLDER") & "\" & fileType
                        'If Not File.Exists(_destinationFolder & "\" & fileName) Then
                        Dim finfoHistory As FileInfo = New FileInfo(fileFolder)
                        Try
                            Dim DirFinfo As DirectoryInfo = New DirectoryInfo(_destinationFolder)
                            If Not DirFinfo.Exists Then
                                DirFinfo.Create()
                            End If
                            finfoHistory.CopyTo(_destinationFolder & "\" & fileName, True)
                        Catch ex As Exception
                            Throw ex
                        Finally
                        End Try
                        'End If
                    Next
                End If

                ' MSP
                If fileType = MSP_REGISTRATION_DC Or fileType = MSP_REGISTRATION_DM Or fileType = MSP_CLAIM_LETTER Or fileType = MSP_CLAIM_KUITANSI Then
                    Dim webMechine As String
                    Dim _destinationFolder As String = String.Empty
                    For Each item As String In KTB.DNet.Lib.WebConfig.GetValue("MSPDirectory").Split(";")
                        _destinationFolder = item & "\" & fileType

                        Dim finfoHistory As FileInfo = New FileInfo(fileFolder)
                        Try
                            If finfoHistory.Extension.ToLower = ".pdf" Then
                                Dim DirFinfo As DirectoryInfo = New DirectoryInfo(_destinationFolder)
                                If Not DirFinfo.Exists Then
                                    DirFinfo.Create()
                                End If
                                finfoHistory.CopyTo(_destinationFolder & "\" & fileName, True)
                            End If
                        Catch ex As Exception
                            Throw ex
                        Finally
                        End Try

                    Next
                End If
            Catch ex As Exception
                Throw New Exception("Fail move file to history folder, but parsing is done : " & ex.Message.ToString)
            End Try
        End Sub
        Private Sub ProcessMontlyDocument(ByVal finfo As FileInfo)

        End Sub

        Private Sub DoingJob()
            'File yang pake extensiaon wts
            Dim originalFileName As String = _fileName.Trim
            Dim originalFileInfo As FileInfo = New FileInfo(originalFileName)
            Dim user As IPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            Dim objSysLog As WSMSyslogParameter = New WSMSyslogParameter(user)
            Dim _blockName As String = "parser-read-file"
            'jika pake wts maka dihilangkan wtsnya
            If UseSignal Then
                _fileName = _fileName.Trim.Substring(0, _fileName.Trim.Length - 4)
            End If

            Dim finfo As New FileInfo(_fileName)
            ''ali
            Dim FF As String = finfo.Name.ToLower().Replace(".txt", "")
            Dim aa As String = finfo.FullName
            ''ali
            'Jika original file ada maka proses, jika tidak file tidak ditemukan
            'artinya file wtsnya duluan nyampe
            If finfo.Exists Then
                'Buka dulu locking filenya jika ada
                If WaitFileUnLock(finfo.FullName) Then
                    Dim fileFolder As String
                    Dim newFileInfo As FileInfo
                    Dim isParse As Boolean = False
                    LOG = New EventLog
                    Try
                        If Not ((finfo.Extension.ToUpper = ".EOD") Or (finfo.Extension.ToUpper = ".DAT")) Then
                            If IsTypeDirectory(finfo, "FINISHUNIT") Then
                                isParse = True
                                _blockName = "FinishUnit"
                                FinishUnitJob(finfo)
                            End If
                            If IsTypeDirectory(finfo, "SPAREPART") Then
                                isParse = True
                                _blockName = "sparepart"
                                SparePartJob(finfo)
                            End If
                            If IsTypeDirectory(finfo, "SERVICE") Then
                                isParse = True
                                _blockName = "service"
                                ServiceJob(finfo)
                            End If
                            'Append COde : Ali akbar 20150317
                            If FF.ToUpper() = _SAPDNETTESTING_FILE_NAME.ToUpper() Then
                                Try
                                    Dim OCE = LogErrorToFile({"BLock Name = " & _blockName & Environment.NewLine, aa})


                                Catch ex As Exception
                                    objSysLog.LogErrorToSyslog("SAP Dnet Testing Succes But Failed to Log " & aa, "wsm-worker", "/KTB.DNet.Parser/worker.vb", "", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, "", -1, _blockName)


                                End Try

                            End If
                            'End of Append COde : Ali akbar 20150317

                        End If
                        If isParse Then
                            LOG.WriteEntry("File Created: " & _fileName & vbCrLf & isParse.ToString)
                        Else
                            LOG.WriteEntry("File Created: " & _fileName & vbCrLf & isParse.ToString, EventLogEntryType.Warning)
                        End If
                    Catch ex As Exception
                        'dna SendingEmail(_fileName, ex)

                        'objSysLog.LogErrorToSyslog("Error while reading file" & finfo.Name, "wsm-worker", "/KTB.DNet.Parser/worker.vb", "", "failed", "", -1, "parser-read-file")
                        objSysLog.LogErrorToSyslog("Error while reading file" & finfo.FullName, "wsm-worker", "/KTB.DNet.Parser/worker.vb", "", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, "", -1, _blockName)
                        'LOG.WriteEntry("Transaction Error " & ex.ToString & " ---> " & finfo.FullName, EventLogEntryType.Error)
                    End Try
                Else
                    Dim newExc As Exception = New Exception("Error while reading original file")
                    SendingEmail(_fileName, newExc)
                    objSysLog.LogErrorToSyslog(newExc.Message.ToString & " " & finfo.FullName, "wsm-worker", "/KTB.DNet.Parser/worker.vb", "", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, "", -1, _blockName)
                    'LOG.WriteEntry("Transaction Error " & newExc.ToString & " ---> " & finfo.FullName, EventLogEntryType.Error)
                End If
            Else
                Dim newExc As Exception = New Exception("File Not Found")
                SendingEmail(_fileName, newExc)
                _blockName = "parser-notfound-file"
                objSysLog.LogErrorToSyslog(newExc.Message.ToString & " " & finfo.FullName, "wsm-worker", "/KTB.DNet.Parser/worker.vb", "", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, "", -1, _blockName)
                'LOG.WriteEntry("Transaction Error " & newExc.ToString & " ---> " & finfo.FullName, EventLogEntryType.Error)
            End If
            'Delete signal file
            If originalFileInfo.Exists And UseSignal Then
                Try
                    originalFileInfo.Delete()
                Catch ex As Exception
                    Dim newExc As Exception = New Exception("File Delete Signal File , " & ex.Message)
                    SendingEmail(originalFileInfo.Name, newExc)
                    _blockName = "parser-delete-signal-file"
                    objSysLog.LogErrorToSyslog("File Delete Signal File " & finfo.FullName, "wsm-worker", "/KTB.DNet.Parser/worker.vb", "", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, "", -1, _blockName)
                    'LOG.WriteEntry("File Delete Signal File , " & ex.Message, EventLogEntryType.Error)
                End Try
            End If
        End Sub

        Private Function IsTypeDirectory(ByVal finfo As FileInfo, ByVal strdir As String) As Boolean
            Dim rootDirectory As String = KTB.DNet.Lib.WebConfig.GetValue("SourceFolder") & "\" & strdir
            Dim newRootDirectory As String = finfo.FullName.Substring(0, rootDirectory.Length)
            If rootDirectory.ToUpper.Equals(newRootDirectory.ToUpper) Then
                Return True
            Else
                Return False
            End If
        End Function

        Private Function GetHistoryDetailDirectory() As String
            Dim FolderType As Integer = 0
            Dim strFolderDetail As String = ""

            If Not IsNothing(KTB.DNet.Lib.WebConfig.GetValue("HISTORY_FOLDER_DETAIL")) Then
                Try
                    FolderType = CType(KTB.DNet.Lib.WebConfig.GetValue("HISTORY_FOLDER_DETAIL"), Integer)
                Catch ex As Exception
                    FolderType = 0
                End Try
                If FolderType = 1 Then
                    strFolderDetail = "ZH" & Format(Now, "yyyy") & "\"
                ElseIf FolderType = 2 Then
                    strFolderDetail = "ZH" & Format(Now, "yyyy") & "\" & Format(Now, "MM") & "\"
                ElseIf FolderType = 3 Then
                    strFolderDetail = "ZH" & Format(Now, "yyyy") & "\" & Format(Now, "MM") & "\" & Format(Now, "dd") & "\"
                End If
            End If
            Return strFolderDetail
        End Function

        Private Function GetTypeDirectory(ByVal finfo As FileInfo, ByVal strdir As String) As String
            Dim rootDirectory As String = KTB.DNet.Lib.WebConfig.GetValue("SourceFolder") & "\" & strdir
            Dim fileName As String = finfo.Name
            Dim newRootDirectory As String = strdir & "\" & finfo.FullName.Remove(0, rootDirectory.Length + 1)
            Return newRootDirectory.Substring(0, newRootDirectory.Trim.Length - fileName.Trim.Length)
        End Function

        Private Sub CopyFileToWebServer(ByVal _fileName As String, ByVal strFolder As String)

            Dim _MachineName = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
            Dim _destinationFolder As String = String.Empty
            Dim _user = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _file As TransferFile
            Dim webMechine As String
            For Each item As String In KTB.DNet.Lib.WebConfig.GetValue("WebServerFolder").Split(";")
                _destinationFolder = item & strFolder
                webMechine = item.Replace("\", "")
                _file = New TransferFile(_user, _password, webMechine.Trim)
                '_file.Transfer(_fileName, _destinationFolder)
                _file.copyFile(_fileName, _destinationFolder)

            Next

        End Sub

        Private Sub CopyFileToWebServerMontlyDoc(ByVal _fileName As String, ByVal strFolder As String)
            Dim _MachineName = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
            Dim _destinationFolder As String = String.Empty
            Dim _user = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _file As TransferFile
            Dim webMechine As String
            For Each item As String In KTB.DNet.Lib.WebConfig.GetValue("WebServerFolder").Split(";")
                _destinationFolder = item & strFolder
                webMechine = item.Replace("\", "")
                _file = New TransferFile(_user, _password, webMechine.Trim)
                If File.Exists(_destinationFolder & "\" & _fileName) Then
                    _file.copyFile(_fileName, _destinationFolder)
                Else
                    'Throw New Exception("File " & _fileName & " Still Exsist")
                End If
            Next
        End Sub


        Private Shared Function LogErrorToFile(ByVal strDataArray As String(), Optional ByVal ErrInfo As String = "") As Boolean

            Dim Contents As String
            Dim bAns As Boolean = False
            Dim objReader As StreamWriter
            Try
                Dim FullPath As String = KTB.DNet.Lib.WebConfig.GetValue("DNetLogFile")
                objReader = New StreamWriter(FullPath, True)
                Dim strData As String
                objReader.WriteLine(Environment.NewLine & Environment.NewLine & "===============SAP TESTING======================")
                objReader.WriteLine(DateTime.Now.ToString() & Environment.NewLine)
                For Each strData In strDataArray
                    objReader.WriteLine(strData)
                Next
                objReader.WriteLine(Environment.NewLine & "===============End Of SAP TESTING====================== " & Environment.NewLine)
                objReader.Close()
                bAns = True
            Catch Ex As Exception
                ErrInfo = Ex.Message

            End Try
            Return bAns
        End Function

#End Region

    End Class

End Namespace
