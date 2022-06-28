#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Net
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Principal
Imports System.Data
Imports System.Linq
Imports System.Security
Imports System.Collections.Generic
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports Excel
Imports System.Reflection
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.BusinessFacade.DiscountProposal
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.BusinessFacade.Benefit

Imports PdfSharp
Imports PdfSharp.Pdf
Imports PdfSharp.Pdf.IO
Imports PdfSharp.Drawing
Imports SpireDoc = Spire.Doc
Imports Document = Spire.Doc.Document
Imports SpireDoc.Documents
Imports OfficeOpenXml
Imports Spire.Doc.Fields
Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Packaging
#End Region

Public Class FrmInputDiscountProposal
    Inherits System.Web.UI.Page

#Region "Custom Variables Declaration"
    Dim objDealer As New Dealer
    Dim objLoginUser As UserInfo
    Dim objDiscountProposalHeader As New DiscountProposalHeader

    Const MAX_FILE_SIZE As Integer = 5120000
    Private TempDirectory As String
    Private TargetDirectory As String
    Private sessHelper As New SessionHelper
    Private intItemIndex As Integer = 0
    Private intVechileTypeID As Integer = 0
    Private intModelID As Integer = 0
    Private Mode As String = "New"
    Private isInsert As Boolean = False
    Private isSimilarChar As Boolean = False

    Private arlHistoryPembelian As New DataTable
    Private arlDiscountProposalDtl As ArrayList = New ArrayList
    Private arlDiscountProposalDtlPrice As ArrayList = New ArrayList
    Private arlDiscountProposalDtlOwnerShip As ArrayList = New ArrayList
    Private arlDiscountProposalDtlDocument As ArrayList = New ArrayList
    Private arlDiscountProposalDtlCustomer As ArrayList = New ArrayList
    Private arlDiscountProposalEmailUser As ArrayList = New ArrayList
    Private arlDiscountProposalDtlHistory As ArrayList = New ArrayList
    Private arlDiscountProposalDtlApproval As ArrayList = New ArrayList
    Private arlDiscountProposalDtlApprovalToSPL As ArrayList = New ArrayList
    Private arlDiscountProposalDtlApprovalToSPLtoPDF As ArrayList = New ArrayList
    Private arlFleetCustomerDetailMappingNew As ArrayList = New ArrayList
    Private arlFleetCustomerDetailMappingOld As ArrayList = New ArrayList
    Private arlDiscountProposalPricetoParameter As ArrayList = New ArrayList
    Private arlDelDiscountProposalDtl As ArrayList = New ArrayList
    Private arlDelDiscountProposalDtlPrice As ArrayList = New ArrayList
    Private arlDelDiscountProposalDtlOwnerShip As ArrayList = New ArrayList
    Private arlDelDiscountProposalDtlDocument As ArrayList = New ArrayList
    Private arlDelDiscountProposalDtlCustomer As ArrayList = New ArrayList
    Private arlDelDiscountProposalEmailUser As ArrayList = New ArrayList
    Private arlDelDiscountProposalDtlHistory As ArrayList = New ArrayList
    Private arlDelDiscountProposalDtlApproval As ArrayList = New ArrayList
    Private arlDelDiscountProposalDtlApprovalToSPL As ArrayList = New ArrayList
    Private arlDelDiscountProposalPricetoParameter As ArrayList = New ArrayList

    Private Property sessNewGuidScreen As String
        Get
            If ViewState("NewGuidScreen") Is Nothing Then
                ViewState("NewGuidScreen") = Guid.NewGuid().ToString()
            End If
            Return DirectCast(ViewState("NewGuidScreen"), String)
        End Get
        Set(value As String)
            ViewState("NewGuidScreen") = value
        End Set
    End Property
    Private ReadOnly Property sessDiscountProposalDealer() As String
        Get
            Return CType("FrmInputDiscountProposal.DEALER_" & sessNewGuidScreen, String)
        End Get
    End Property
    Private ReadOnly Property sessDiscountProposalHdr() As String
        Get
            Return CType("FrmInputDiscountProposal.sessDataDiscountProposalHdr_" & sessNewGuidScreen, String)
        End Get
    End Property
    Private ReadOnly Property sessDiscountProposalDtlPrice() As String
        Get
            Return CType("FrmInputDiscountProposal.sessDataDiscountProposalDtlPrice_" & sessNewGuidScreen, String)
        End Get
    End Property
    Private ReadOnly Property sessDiscountProposalDtlPriceOld() As String
        Get
            Return CType("FrmInputDiscountProposal.sessDataDiscountProposalDtlPriceOld_" & sessNewGuidScreen, String)
        End Get
    End Property
    Private ReadOnly Property sessDiscountProposalDtlPricePivot() As String
        Get
            Return CType("FrmInputDiscountProposal.sessDataDiscountProposalDtlPricePivot_" & sessNewGuidScreen, String)
        End Get
    End Property
    Private ReadOnly Property sessDiscountProposalDtlPricePivotGrid() As String
        Get
            Return CType("FrmInputDiscountProposal.sessDataDiscountProposalDtlPricePivotGrid_" & sessNewGuidScreen, String)
        End Get
    End Property
    Private ReadOnly Property sessDiscountProposalDtl() As String
        Get
            Return CType("FrmInputDiscountProposal.sessDataDiscountProposalDtl_" & sessNewGuidScreen, String)
        End Get
    End Property
    Private ReadOnly Property sessDiscountProposalDtlOwnerShip() As String
        Get
            Return CType("FrmInputDiscountProposal.sessDiscountProposalDtlOwnerShip_" & sessNewGuidScreen, String)
        End Get
    End Property
    Private ReadOnly Property sessDiscountProposalDtlDocument() As String
        Get
            Return CType("FrmInputDiscountProposal.sessDiscountProposalDtlDocument_" & sessNewGuidScreen, String)
        End Get
    End Property
    Private ReadOnly Property sessDiscountProposalDtlCustomer() As String
        Get
            Return CType("FrmInputDiscountProposal.sessDiscountProposalDtlCustomer_" & sessNewGuidScreen, String)
        End Get
    End Property
    Private ReadOnly Property sessDiscountProposalEmailUser() As String
        Get
            Return CType("FrmInputDiscountProposal.sessDiscountProposalEmailUser_" & sessNewGuidScreen, String)
        End Get
    End Property
    Private ReadOnly Property sessDiscountProposalDtlHistory() As String
        Get
            Return CType("FrmInputDiscountProposal.sessDataDiscountProposalDtlHistory_" & sessNewGuidScreen, String)
        End Get
    End Property
    Private ReadOnly Property sessDiscountProposalDtlApproval() As String
        Get
            Return CType("FrmInputDiscountProposal.sessDataDiscountProposalDtlApproval_" & sessNewGuidScreen, String)
        End Get
    End Property
    Private ReadOnly Property sessDiscountProposalDtlApprovalToSPL() As String
        Get
            Return CType("FrmInputDiscountProposal.sessDataDiscountProposalDtlApprovalToSPL_" & sessNewGuidScreen, String)
        End Get
    End Property
    Private ReadOnly Property sessHistoryPembelianNew() As String
        Get
            Return CType("FrmInputDiscountProposal.sessHistoryPembelianNew_" & sessNewGuidScreen, String)
        End Get
    End Property
    Private ReadOnly Property sessHistoryPembelianOld() As String
        Get
            Return CType("FrmInputDiscountProposal.sessHistoryPembelianOld_" & sessNewGuidScreen, String)
        End Get
    End Property
    Private ReadOnly Property sessHistoryPembelianToPDF() As String
        Get
            Return CType("FrmInputDiscountProposal.sessHistoryPembelianToPDF_" & sessNewGuidScreen, String)
        End Get
    End Property
    Private ReadOnly Property sessDiscountProposalParameter() As String
        Get
            Return CType("FrmInputDiscountProposal.sessDiscountProposalParameter_" & sessNewGuidScreen, String)
        End Get
    End Property
    Private ReadOnly Property sessDiscountProposalPricetoParameter() As String
        Get
            Return CType("FrmInputDiscountProposal.sessDiscountProposalPricetoParameter_" & sessNewGuidScreen, String)
        End Get
    End Property
    Private ReadOnly Property sessDeleteDiscountProposalDtlPrice() As String
        Get
            Return CType("FrmInputDiscountProposal.sessDeleteDataDiscountProposalDtlPrice_" & sessNewGuidScreen, String)
        End Get
    End Property
    Private ReadOnly Property sessDeleteDiscountProposalDtl() As String
        Get
            Return CType("FrmInputDiscountProposal.sessDeleteDataDiscountProposalDtl_" & sessNewGuidScreen, String)
        End Get
    End Property
    Private ReadOnly Property sessDeleteDiscountProposalDtlOwnerShip() As String
        Get
            Return CType("FrmInputDiscountProposal.sessDeleteDiscountProposalDtlOwnerShip_" & sessNewGuidScreen, String)
        End Get
    End Property
    Private ReadOnly Property sessDeleteDiscountProposalDtlDocument() As String
        Get
            Return CType("FrmInputDiscountProposal.sessDeleteDiscountProposalDtlDocument_" & sessNewGuidScreen, String)
        End Get
    End Property
    Private ReadOnly Property sessDeleteDiscountProposalDtlCustomer() As String
        Get
            Return CType("FrmInputDiscountProposal.sessDeleteDiscountProposalDtlCustomer_" & sessNewGuidScreen, String)
        End Get
    End Property
    Private ReadOnly Property sessDeleteDiscountProposalEmailUser() As String
        Get
            Return CType("FrmInputDiscountProposal.sessDeleteDiscountProposalEmailUser_" & sessNewGuidScreen, String)
        End Get
    End Property
    Private ReadOnly Property sessDeleteDiscountProposalDtlHistory() As String
        Get
            Return CType("FrmInputDiscountProposal.sessDeleteDiscountProposalDtlHistory_" & sessNewGuidScreen, String)
        End Get
    End Property
    Private ReadOnly Property sessDeleteDiscountProposalDtlApproval() As String
        Get
            Return CType("FrmInputDiscountProposal.sessDeleteDiscountProposalDtlApproval_" & sessNewGuidScreen, String)
        End Get
    End Property
    Private ReadOnly Property sessDeleteDiscountProposalDtlApprovalToSPL() As String
        Get
            Return CType("FrmInputDiscountProposal.sessDeleteDiscountProposalDtlApprovalToSPL_" & sessNewGuidScreen, String)
        End Get
    End Property
    Private ReadOnly Property sessDeleteDiscountProposalPricetoParameter() As String
        Get
            Return CType("FrmInputDiscountProposal.sessDeleteDiscountProposalPricetoParameter_" & sessNewGuidScreen, String)
        End Get
    End Property

    Private Property SesDealer() As Dealer
        Get
            Return CType(sessHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sessHelper.SetSession("DEALER", Value)
        End Set
    End Property

#Region "Dynamic Grid Variable"
    Const strColumn1 As String = "Keterangan_Kendaraan"                                                  '1
    Const strColumn2 As String = "Model"                                                                 '2
    Const strColumn3 As String = "Tipe"                                                                  '3
    Const strColumn4 As String = "Warna"                                                                 '4
    Const strColumn5 As String = "Assy_Year"                                                             '5
    Const strColumn6 As String = "Model_Year"                                                            '6
    Const strColumn7 As String = "Jumlah"                                                                '7
    '========================================================================================================
    Const strColumn8 As String = "Cost_Dealer_:"                                                         '8
    Const strColumn9 As String = "Harga_Tebus"                                                           '9
    Const strColumn10 As String = "Logistic_Cost"                                                        '10
    Const strColumn11 As String = "BBN"                                                                  '11
    Const strColumn12 As String = "Biaya_(Biro_Jasa,_Form_A,_Keur,_dll)"                                 '12
    Const strColumn13 As String = "Sub_Total_Cost_Dealer"                                                '13
    Const strColumn14 As String = "Retail_Price_On_The_Road"                                             '14
    Const strColumn15 As String = "Margin_Dealer_Gross_diluar_Sales_Program"                             '15
    '========================================================================================================
    Const strColumn16 As String = "Program_Reguler_MMKSI_:"                                              '16
    Const strColumn17 As String = "Whole_Sales_Disc"                                                     '17
    Const strColumn18 As String = "Whole_Sales_Cashback_/_Pilihan_Leasing"                               '18
    Const strColumn19 As String = "AY_Compensation"                                                      '19
    Const strColumn20 As String = "Price_Increase_Compensation"                                          '20
    Const strColumn21 As String = "Actual_DO_Incentive"                                                  '21
    Const strColumn22 As String = "Focus_Area_Incentive"                                                 '22
    Const strColumn23 As String = "Sub_Total_Program_Reguler_MMKSI"                                      '23
    Const strColumn24 As String = "Total_Gross_Margin_Dealer_termasuk_Sales_Program"                     '24
    '========================================================================================================
    Const strColumn25 As String = "Biaya_Lain-lain_:"                                                    '25
    Const strColumn26 As String = "Biaya_Pengiriman_ke_Customer_(diluar_area_domisili_Dealer)"           '26
    Const strColumn27 As String = "Aksesoris_(kaca_film,_talang_air,_karpet_dasar,_sarung_jok)"          '27
    Const strColumn28 As String = "Sub_Total_Biaya_Lain-lain"                                            '28
    Const strColumn29 As String = "Gross_Dealer_Margin"                                                  '29
    Const strColumn30 As String = "Estimasi_Deal_Price"                                                  '30
    Const strColumn31 As String = "Diskon_Dealer"                                                        '31
    Const strColumn32 As String = "Nett_Dealer_Margin"                                                   '32
    Const strColumn33 As String = "Permohonan_Diskon_Program_Fleet_Customer"                             '33
    Const strColumn34 As String = "Margin_Dealer_Final"                                                  '34
    '========================================================================================================

    '========================================================================================================
    Const strddlModel As String = "ddlModel"                                                              '1
    Const strddlTipe As String = "ddlTipe"                                                                '2
    Const strddlWarna As String = "ddlWarna"                                                              '3
    Const strddlAssyYear As String = "ddlAssyYear"                                                        '4
    Const strddlModelYear As String = "ddlModelYear"                                                      '5
    Const strtxtJumlah As String = "txtJumlah"                                                            '6
    '========================================================================================================
    Const strlblHargaTebus As String = "lblHargaTebus"                                                    '7
    Const strlblLogisticCost As String = "lblLogisticCost"                                                '8
    Const strtxtBBN As String = "txtBBN"                                                                  '9
    Const strtxtBiayaBiroJasa As String = "txtBiayaBiroJasa"                                              '10
    Const strlblSubTotalCostDealer As String = "lblSubTotalCostDealer"                                    '11
    Const strtxtRetailPriceOnTheRoad As String = "txtRetailPriceOnTheRoad"                                '12
    Const strlblMarginDealerGross As String = "lblMarginDealerGross"                                      '13
    '========================================================================================================
    Const strlblWholeSalesDisc As String = "lblWholeSalesDisc"                                            '14
    Const strlblWholeSalesCashback As String = "lblWholeSalesCashback"                                    '15
    Const strlblAYCompensation As String = "lblAYCompensation"                                            '16
    Const strlblPriceIncreaseCompensation As String = "lblPriceIncreaseCompensation"                      '17
    Const strlblActualDOIncentive As String = "lblActualDOIncentive"                                      '18
    Const strlblFocusAreaIncentive As String = "lblFocusAreaIncentive"                                    '19
    Const strlblSubTotalProgramRegulerMMKSI As String = "lblSubTotalProgramRegulerMMKSI"                  '20
    Const strlblTotalGrossMarginDealer As String = "lblTotalGrossMarginDealer"                            '21
    '========================================================================================================
    Const strtxtBiayaPengirimanKeCustomer As String = "txtBiayaPengirimanKeCustomer"                      '22
    Const strtxtAksesoris As String = "txtAksesoris"                                                      '23
    Const strlblSubTotalBiayaLainlain As String = "lblSubTotalBiayaLainlain"                              '24
    Const strlblGrossDealerMargin As String = "lblGrossDealerMargin"                                      '25
    Const strtxtEstimasiDealPrice As String = "txtEstimasiDealPrice"                                      '26
    Const strlblDiskonDealer As String = "lblDiskonDealer"                                                '27
    Const strlblNettDealerMargin As String = "lblNettDealerMargin"                                        '28
    Const strtxtPermohonanDiskonProgramFleetCustomer As String = "txtPermohonanDiskonProgramFleetCustomer"      '29
    Const strlblMarginDealerFinal As String = "lblMarginDealerFinal"                                            '30
    '========================================================================================================
#End Region

    Private dt As New DataTable
#End Region

#Region "Custom Methods"

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

    Private Sub LoadDataDiscountProposal(intDiscountProposalHeaderID As Integer)
        Dim objDiscountProposalDetail As DiscountProposalDetail

        objDiscountProposalHeader = New DiscountProposalHeaderFacade(User).Retrieve(intDiscountProposalHeaderID)
        If Not IsNothing(objDiscountProposalHeader) Then
            If Mode = "Edit" Then
                isEnableProperty(True)
                If Not IsLoginAsDealer() Then
                    If objDiscountProposalHeader.Status = 3 OrElse objDiscountProposalHeader.Status = 9 Then  'Status = konfirmasi atau Revisi
                        isEnableProperty(False)
                        lnkReloadHistoryPembelian.Visible = True
                        dgProposedDiscount.ShowFooter = True
                        dgProposedDiscount.Columns(dgProposedDiscount.Columns.Count - 1).Visible = True
                        CBConsideration.Enabled = True
                        chkFinalApproval.Enabled = True
                        txtMMKSINotes.Enabled = True
                        btnSave.Visible = True
                    End If
                End If
            End If

            sessHelper.SetSession(sessDiscountProposalHdr, objDiscountProposalHeader)
            hdnDiscountProposalHeaderID.Value = objDiscountProposalHeader.ID
            objDealer = objDiscountProposalHeader.Dealer
            GetDealerData(objDealer)
            sessHelper.SetSession(sessDiscountProposalDealer, objDealer)

            txtDealerCode.Text = objDiscountProposalHeader.Dealer.DealerCode
            lblDealerCodeName.Text = objDiscountProposalHeader.Dealer.DealerCode & " / " & objDiscountProposalHeader.Dealer.DealerName
            lblSubmitDate.Text = objDiscountProposalHeader.SubmitDate.ToString("dd MMMM yyyy")
            hdnSubmitDate.Value = objDiscountProposalHeader.SubmitDate.ToString("dd/MM/yyyy")
            lblStatus.Text = If(CommonFunction.GetEnumDescription(objDiscountProposalHeader.Status, "EnumDiscountProposal.Status").Trim <> "", " / " & CommonFunction.GetEnumDescription(objDiscountProposalHeader.Status, "EnumDiscountProposal.Status"), "")
            lblProposalRegNo.Text = objDiscountProposalHeader.ProposalRegNo
            txtDealerProposalNo.Text = objDiscountProposalHeader.DealerProposalNo
            ddlCustomerType.SelectedValue = objDiscountProposalHeader.CustomerType
            ddlCustomerType_SelectedIndexChanged(ddlCustomerType, Nothing)

            ddlFleetCategory.SelectedValue = objDiscountProposalHeader.FleetCategory
            If Not IsNothing(objDiscountProposalHeader.FleetCustomerDetail) Then
                txtFleetCustomerName.Text = objDiscountProposalHeader.FleetCustomerDetail.FleetCustomerHeader.FleetCustomerName
                hdnFleetCustomerHeaderID.Value = objDiscountProposalHeader.FleetCustomerDetail.FleetCustomerHeader.ID
                hdnFleetCustomerDetailID.Value = objDiscountProposalHeader.FleetCustomerDetail.ID
                txtAddressFleetCustomerDtl.Text = objDiscountProposalHeader.FleetCustomerDetail.Address
                txtNoKTP.Text = Left(objDiscountProposalHeader.FleetCustomerDetail.IdentityNumber, 16)
                txtNoNIB.Text = Left(objDiscountProposalHeader.FleetCustomerDetail.IdentityNumber, 16)
                BindGridHistoryPembelian(hdnFleetCustomerHeaderID.Value)
            End If

            txtNameOnFaktur.Text = ""
            If Not IsNothing(objDiscountProposalHeader.BBNAreaProvince) Then
                ddlBBNAreaProvince.SelectedValue = objDiscountProposalHeader.BBNAreaProvince.ID
            End If
            txtProjectName.Text = objDiscountProposalHeader.ProjectName
            icLastPurchaseDate.Value = objDiscountProposalHeader.LastPurchaseDate
            txtProjectName.Text = objDiscountProposalHeader.ProjectName
            ddlDeliveryRegionCode.SelectedValue = objDiscountProposalHeader.DeliveryRegionCode

            Dim criterias4 As New CriteriaComposite(New Criteria(GetType(DiscountProposalDetailDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias4.opAnd(New Criteria(GetType(DiscountProposalDetailDocument), "DiscountProposalHeader.ID", MatchType.Exact, intDiscountProposalHeaderID))
            arlDiscountProposalDtlDocument = New DiscountProposalDetailDocumentFacade(User).Retrieve(criterias4)
            GenerateNumberRow("DiscountProposalDetailDocument", arlDiscountProposalDtlDocument)
            sessHelper.SetSession(sessDiscountProposalDtlDocument, arlDiscountProposalDtlDocument)
            BindGridUploadFileLampiranPOSPK()

            For Each objDPDoc As DiscountProposalDetailDocument In arlDiscountProposalDtlDocument
                If objDPDoc.FileType = 0 Then   '---ValueCode = Surat Komitmen
                    txtSuratKomitmen.Text = objDPDoc.Path
                    'lblSuratKomitmentKontrak.Text = objDPDoc.FileName
                    LinkDownloadSuratKomitmentKontrak.Text = objDPDoc.FileName
                End If
                If objDPDoc.FileType = 1 Then   '---ValueCode = Surat Pernyataan
                    txtSuratPernyataan.Text = objDPDoc.Path
                    'lblSuratPernyataan.Text = objDPDoc.FileName
                    LinkDownloadSuratPernyataan.Text = objDPDoc.FileName
                    'If objDPDoc.FileName.Length > 0 Then
                    '    lnkDownload.NavigateUrl = String.Format("~\Download.aspx?file={0}\({1}){2}", _
                    '        KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory"), objDPDoc.ID, _
                    '        objDPDoc.FileName)
                    'End If
                End If
                If objDPDoc.FileType = 2 Then   '---ValueCode = LampiranPOSPK
                    txtLampiranPOSPK.Text = objDPDoc.Path
                    lblLampiranPOSPK.Text = objDPDoc.FileName
                End If
            Next

            ddlBusinessSector.SelectedValue = objDiscountProposalHeader.BusinessSectorDetailID
            ddlProjectKindMethod.SelectedValue = objDiscountProposalHeader.ProjectKindMethod
            ddlProjectKindMethod_SelectedIndexChanged(ddlProjectKindMethod, Nothing)
            ddlDealerDirectSales.SelectedValue = objDiscountProposalHeader.IsDealerDirectSales
            ddlDealerDirectSales_SelectedIndexChanged(ddlDealerDirectSales, Nothing)
            txtContractorName.Text = objDiscountProposalHeader.ContractorName

            ddlPurchaseMethod.SelectedValue = objDiscountProposalHeader.PurchaseMethod
            ddlPurchaseMethod_SelectedIndexChanged(ddlPurchaseMethod, Nothing)
            If Not IsNothing(objDiscountProposalHeader.LeasingCompany) Then
                ddlLeasing.SelectedValue = objDiscountProposalHeader.LeasingCompany.ID
            End If
            ddlLeasing_SelectedIndexChanged(ddlLeasing, Nothing)
            ddlAPMSubsidy.SelectedValue = objDiscountProposalHeader.IsAPMSubsidy
            ddlPaymentMethod.SelectedValue = objDiscountProposalHeader.PaymentMethod
            ddlPurchaseKind.SelectedValue = objDiscountProposalHeader.PurchaseKind

            txtProjectKindMethodOther.Text = objDiscountProposalHeader.ProjectKindMethodOther
            txtDeliveryPlanDate.Text = Format(objDiscountProposalHeader.DeliveryPlanDate, "MMyyyy")
            txtDealerNotes.Text = objDiscountProposalHeader.DealerNotes
            txtMMKSINotes.Text = objDiscountProposalHeader.MMKSINotes
            chkFinalApproval.Checked = If(objDiscountProposalHeader.FinalApproval = 1, True, False)
            Dim strConsideration As String = objDiscountProposalHeader.Consideration
            Dim arrConsideration As New ArrayList
            If strConsideration.Trim <> "" Then
                For Each id As String In strConsideration.Split(";")
                    Dim oDiscountProposalParameter As DiscountProposalParameter = New DiscountProposalParameterFacade(User).Retrieve(CInt(id))
                    If Not IsNothing(oDiscountProposalParameter) AndAlso oDiscountProposalParameter.ID > 0 Then
                        arrConsideration.Add(oDiscountProposalParameter)
                    End If
                Next
            End If
            BindCheckBoxConsideration(arrConsideration)

            'Binding DP Prameter
            BindSessionDataDiscountProposalParameter()

            Dim criterias As New CriteriaComposite(New Criteria(GetType(DiscountProposalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DiscountProposalDetail), "DiscountProposalHeader.ID", MatchType.Exact, intDiscountProposalHeaderID))
            arlDiscountProposalDtl = New DiscountProposalDetailFacade(User).Retrieve(criterias)
            sessHelper.SetSession(sessDiscountProposalDtl, arlDiscountProposalDtl)

            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(DiscountProposalDetailOwnership), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(DiscountProposalDetailOwnership), "DiscountProposalHeader.ID", MatchType.Exact, intDiscountProposalHeaderID))
            arlDiscountProposalDtlOwnerShip = New DiscountProposalDetailOwnershipFacade(User).Retrieve(criterias2)
            sessHelper.SetSession(sessDiscountProposalDtlOwnerShip, arlDiscountProposalDtlOwnerShip)
            BindGridKepemilikanKendaraan()

            Dim criterias3 As New CriteriaComposite(New Criteria(GetType(DiscountProposalDetailPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias3.opAnd(New Criteria(GetType(DiscountProposalDetailPrice), "DiscountProposalHeader.ID", MatchType.Exact, intDiscountProposalHeaderID))
            arlDiscountProposalDtlPrice = New DiscountProposalDetailPriceFacade(User).Retrieve(criterias3)
            For Each oDiscountProposalDetailPrice As DiscountProposalDetailPrice In arlDiscountProposalDtlPrice
                oDiscountProposalDetailPrice.SubCategoryVehicleID = oDiscountProposalDetailPrice.DiscountProposalDetail.SubCategoryVehicle.ID
                If Not IsNothing(oDiscountProposalDetailPrice.DiscountProposalDetail.VechileColorIsActiveOnPK) Then
                    oDiscountProposalDetailPrice.VechileColorID = oDiscountProposalDetailPrice.DiscountProposalDetail.VechileColorIsActiveOnPK.VechileColor.ID
                    oDiscountProposalDetailPrice.VechileTypeID = oDiscountProposalDetailPrice.DiscountProposalDetail.VechileColorIsActiveOnPK.VechileTypeGeneral.ID
                    oDiscountProposalDetailPrice.AssyYear = oDiscountProposalDetailPrice.DiscountProposalDetail.AssyYear
                    oDiscountProposalDetailPrice.ModelYear = oDiscountProposalDetailPrice.DiscountProposalDetail.ModelYear
                End If
            Next
            sessHelper.SetSession(sessDiscountProposalDtlPrice, arlDiscountProposalDtlPrice)
            GenerateNumberRow("DISCOUNTPROPOSALDETAILPRICE", arlDiscountProposalDtlPrice)

            Dim arlDPPricetoParameter As New ArrayList
            For Each oDiscountProposalDetailPrice As DiscountProposalDetailPrice In arlDiscountProposalDtlPrice
                Dim crit As New CriteriaComposite(New Criteria(GetType(DiscountProposalPricetoParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(DiscountProposalPricetoParameter), "DiscountProposalDetailPrice.ID", MatchType.Exact, oDiscountProposalDetailPrice.ID))
                arlDPPricetoParameter = New DiscountProposalPricetoParameterFacade(User).Retrieve(crit)
                arlDPPricetoParameter = New System.Collections.ArrayList(
                                (From obj As DiscountProposalPricetoParameter In arlDPPricetoParameter.OfType(Of DiscountProposalPricetoParameter)()
                                    Order By obj.DiscountProposalDetailPrice.ID, obj.DiscountProposalParameter.ID
                                    Select obj).ToList())

                For Each oDiscountProposalPricetoParameter As DiscountProposalPricetoParameter In arlDPPricetoParameter
                    oDiscountProposalPricetoParameter.NumberRowParent = oDiscountProposalDetailPrice.NumberRow
                    arlDiscountProposalPricetoParameter.Add(oDiscountProposalPricetoParameter)
                Next
            Next
            sessHelper.SetSession(sessDiscountProposalPricetoParameter, arlDiscountProposalPricetoParameter)

            Dim criterias5 As New CriteriaComposite(New Criteria(GetType(DiscountProposalDetailCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias5.opAnd(New Criteria(GetType(DiscountProposalDetailCustomer), "DiscountProposalHeader.ID", MatchType.Exact, intDiscountProposalHeaderID))
            arlDiscountProposalDtlCustomer = New DiscountProposalDetailCustomerFacade(User).Retrieve(criterias5)
            sessHelper.SetSession(sessDiscountProposalDtlCustomer, arlDiscountProposalDtlCustomer)
            BindGridDPCustomer()

            Dim criterias7 As New CriteriaComposite(New Criteria(GetType(DiscountProposalEmailUser), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias7.opAnd(New Criteria(GetType(DiscountProposalEmailUser), "DiscountProposalHeader.ID", MatchType.Exact, intDiscountProposalHeaderID))
            arlDiscountProposalEmailUser = New DiscountProposalEmailUserFacade(User).Retrieve(criterias7)
            sessHelper.SetSession(sessDiscountProposalEmailUser, arlDiscountProposalEmailUser)
            BindGridEmailUser()

            Dim criterias8 As New CriteriaComposite(New Criteria(GetType(DiscountProposalDetailApprovaltoSPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias8.opAnd(New Criteria(GetType(DiscountProposalDetailApprovaltoSPL), "DiscountProposalDetailApproval.DiscountProposalHeader.ID", MatchType.Exact, intDiscountProposalHeaderID))
            arlDiscountProposalDtlApprovalToSPL = New DiscountProposalDetailApprovaltoSPLFacade(User).Retrieve(criterias8)
            For Each oDiscountProposalDtlApprovalToSPL As DiscountProposalDetailApprovaltoSPL In arlDiscountProposalDtlApprovalToSPL
                oDiscountProposalDtlApprovalToSPL.ModelID = oDiscountProposalDtlApprovalToSPL.DiscountProposalDetailApproval.ModelID
                If Not IsNothing(oDiscountProposalDtlApprovalToSPL.DiscountProposalDetailApproval.VechileType) Then
                    oDiscountProposalDtlApprovalToSPL.VechileTypeID = oDiscountProposalDtlApprovalToSPL.DiscountProposalDetailApproval.VechileType.ID
                End If
            Next
            sessHelper.SetSession(sessDiscountProposalDtlApprovalToSPL, arlDiscountProposalDtlApprovalToSPL)

            Dim criterias9 As New CriteriaComposite(New Criteria(GetType(DiscountProposalDetailApproval), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias9.opAnd(New Criteria(GetType(DiscountProposalDetailApproval), "DiscountProposalHeader.ID", MatchType.Exact, intDiscountProposalHeaderID))
            arlDiscountProposalDtlApproval = New DiscountProposalDetailApprovalFacade(User).Retrieve(criterias9)
            sessHelper.SetSession(sessDiscountProposalDtlApproval, arlDiscountProposalDtlApproval)

            BindGridProposedDiscount()

            Dim criterias6 As New CriteriaComposite(New Criteria(GetType(DiscountProposalDetailCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias6.opAnd(New Criteria(GetType(DiscountProposalDetailCustomer), "DiscountProposalHeader.ID", MatchType.Exact, intDiscountProposalHeaderID))
            arlDiscountProposalDtlCustomer = New DiscountProposalDetailCustomerFacade(User).Retrieve(criterias6)
            sessHelper.SetSession(sessDiscountProposalDtlCustomer, arlDiscountProposalDtlCustomer)
            If Not IsNothing(arlDiscountProposalDtlCustomer) AndAlso arlDiscountProposalDtlCustomer.Count > 0 Then
                For Each obj As DiscountProposalDetailCustomer In arlDiscountProposalDtlCustomer
                    If txtNameOnFaktur.Text.Trim = "" Then
                        txtNameOnFaktur.Text = obj.Name
                    Else
                        txtNameOnFaktur.Text += ";" & obj.Name
                    End If
                Next
            End If
        End If

        btnHapus.Visible = False
        If IsLoginAsDealer() Then
            If objDiscountProposalHeader.Status = 0 Then
                btnHapus.Visible = True
            End If
        End If

        If Mode = "View" Then
            isEnableProperty(False)
        End If
    End Sub

    Private Sub isEnableProperty(isStatus As Boolean)
        btnSave.Visible = isStatus
        btnBaru.Visible = isStatus
        If Not IsLoginAsDealer() Then
            btnBaru.Visible = False
        End If

        btnValidasi.Visible = isStatus
        If Not IsLoginAsDealer() Then
            btnValidasi.Visible = False
        End If
        trFinalApproval.Visible = True
        If IsLoginAsDealer() Then
            trFinalApproval.Visible = False
        End If
        btnHapus.Visible = False

        lblPopUpDealer.Visible = isStatus
        txtDealerProposalNo.Enabled = isStatus
        ddlCustomerType.Enabled = isStatus
        ddlFleetCategory.Enabled = isStatus
        txtFleetCustomerName.Enabled = False
        lblPopUpFleetCustomer.Visible = isStatus
        txtAddressFleetCustomerDtl.Enabled = isStatus
        'txtNoNIB.Enabled = False
        'txtNoKTP.Enabled = False
        txtNoNIB.Enabled = isStatus
        txtNoKTP.Enabled = isStatus
        If ddlCustomerType.SelectedIndex = 0 Then
            trKTP.Visible = False
            trNIB.Visible = False
        End If
        txtNameOnFaktur.Enabled = isStatus
        lnkPopUpNameOnFaktur.Visible = isStatus
        ddlBusinessSector.Enabled = isStatus
        ddlBBNAreaProvince.Enabled = isStatus
        txtProjectName.Enabled = isStatus
        icLastPurchaseDate.Enabled = isStatus
        ddlDealerDirectSales.Enabled = isStatus
        ddlPurchaseMethod.Enabled = isStatus
        ddlLeasing.Enabled = isStatus
        ddlAPMSubsidy.Enabled = isStatus
        txtContractorName.Enabled = isStatus
        ddlPaymentMethod.Enabled = isStatus
        ddlPurchaseKind.Enabled = isStatus
        ddlProjectKindMethod.Enabled = isStatus
        txtProjectKindMethodOther.Enabled = isStatus
        txtDeliveryPlanDate.Enabled = isStatus
        txtDealerNotes.Enabled = isStatus
        txtMMKSINotes.Enabled = isStatus
        chkFinalApproval.Enabled = isStatus
        ddlDeliveryRegionCode.Enabled = isStatus
        lbtnDeleteFileSuratKomitmen.Visible = isStatus
        lbtnDeleteFileSuratPernyataan.Visible = isStatus
        lbtnDeleteFileLampiranPOSPK.Visible = isStatus
        FUSuratKomitmentKontrak.Visible = isStatus
        FUSuratPernyataan.Visible = isStatus
        FULampiranPOSPK.Visible = isStatus
        txtSuratKomitmen.Visible = isStatus
        txtSuratPernyataan.Visible = isStatus
        txtLampiranPOSPK.Visible = isStatus
        CBConsideration.Enabled = isStatus
        lnkReloadHistoryPembelian.Visible = isStatus

        'lblSuratKomitmentKontrak.Visible = Not isStatus
        'lblSuratPernyataan.Visible = Not isStatus
        'perubahan untuk download dokumen
        LinkDownloadSuratKomitmentKontrak.Visible = Not isStatus
        LinkDownloadSuratPernyataan.Visible = Not isStatus
        lblLampiranPOSPK.Visible = Not isStatus
        If IsLoginAsDealer() Then
            tblMMKSISide.Visible = False
            divMMKSISide.Visible = False
            divMMKSISide2.Visible = False
            tblMMKSISide3.Visible = False
        Else
            tblMMKSISide.Visible = True
            divMMKSISide.Visible = True
            divMMKSISide2.Visible = True
            tblMMKSISide3.Visible = True
        End If
        btnBuatFleet.Visible = isStatus
        divAddVehicle.Visible = isStatus
        dgKepemilikanKendaraan.ShowFooter = isStatus
        dgKepemilikanKendaraan.Columns(dgKepemilikanKendaraan.Columns.Count - 1).Visible = isStatus
        'dgJadwalPengadaan.ShowFooter = isStatus
        'dgJadwalPengadaan.Columns(dgJadwalPengadaan.Columns.Count - 1).Visible = isStatus
        dgEmailUser.ShowFooter = isStatus
        dgEmailUser.Columns(dgEmailUser.Columns.Count - 1).Visible = isStatus
        dgUploadFile.ShowFooter = isStatus
        dgUploadFile.Columns(dgUploadFile.Columns.Count - 1).Visible = isStatus
        dgProposedDiscount.ShowFooter = isStatus
        dgProposedDiscount.Columns(dgProposedDiscount.Columns.Count - 1).Visible = isStatus
    End Sub

    Private Sub RemoveALLSession()
        sessHelper.RemoveSession(sessDiscountProposalHdr)
        sessHelper.RemoveSession(sessDiscountProposalDtlPrice)
        sessHelper.RemoveSession(sessDiscountProposalDtlPricePivot)
        sessHelper.RemoveSession(sessDiscountProposalDtlPricePivotGrid)
        sessHelper.RemoveSession(sessDiscountProposalDtlPriceOld)
        sessHelper.RemoveSession(sessDiscountProposalDtl)
        sessHelper.RemoveSession(sessDiscountProposalDtlOwnerShip)
        sessHelper.RemoveSession(sessDiscountProposalDtlDocument)
        sessHelper.RemoveSession(sessDiscountProposalDtlCustomer)
        sessHelper.RemoveSession(sessDiscountProposalEmailUser)
        sessHelper.RemoveSession(sessHistoryPembelianNew)
        sessHelper.RemoveSession(sessHistoryPembelianOld)
        sessHelper.RemoveSession(sessHistoryPembelianToPDF)
        sessHelper.RemoveSession(sessDiscountProposalDtlApproval)
        sessHelper.RemoveSession(sessDiscountProposalDtlApprovalToSPL)
        sessHelper.RemoveSession(sessDiscountProposalParameter)
        sessHelper.RemoveSession(sessDiscountProposalPricetoParameter)

        sessHelper.RemoveSession(sessDeleteDiscountProposalDtlPrice)
        sessHelper.RemoveSession(sessDeleteDiscountProposalDtl)
        sessHelper.RemoveSession(sessDeleteDiscountProposalDtlOwnerShip)
        sessHelper.RemoveSession(sessDeleteDiscountProposalDtlDocument)
        sessHelper.RemoveSession(sessDeleteDiscountProposalDtlCustomer)
        sessHelper.RemoveSession(sessDeleteDiscountProposalEmailUser)
        sessHelper.RemoveSession(sessDeleteDiscountProposalDtlApproval)
        sessHelper.RemoveSession(sessDeleteDiscountProposalPricetoParameter)
    End Sub

    Private Sub RemoveQueryString()
        Dim isreadonly As PropertyInfo = GetType(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance Or BindingFlags.NonPublic)
        Try
            ' make collection editable
            isreadonly.SetValue(Me.Request.QueryString, False, Nothing)
            ' remove
            Me.Request.QueryString.Remove("Mode")
            Me.Request.QueryString.Remove("DiscountProposalHeaderID")
        Catch
        End Try
    End Sub

    Private Sub ClearAll()
        hdnDiscountProposalHeaderID.Value = ""
        hdnModelYear.Value = ""
        hdnAssyYear.Value = ""
        hdnVechileTypeID.Value = ""
        hdnVechileColorID.Value = ""
        hdnSubTotalCostDealer.Value = ""
        hdnValNew.Value = "-1"
        hdnShowDataCustomer.Value = 0
        hdnIndexProposedDiscountGrid.Value = ""
        hdnIndexEmailUserGrid.Value = ""

        lblProposalRegNo.Text = "[Auto Generated]"
        txtDealerProposalNo.Text = ""
        lblSubmitDate.Text = Date.Now.ToString("dd MMMM yyyy")
        hdnSubmitDate.Value = Date.Now.ToString("dd/MM/yyyy")
        lblStatus.Text = " / Baru"
        ddlFleetCategory.SelectedIndex = 0
        txtFleetCustomerName.Text = ""
        hdnFleetCustomerHeaderID.Value = ""
        hdnFleetCustomerDetailID.Value = ""
        txtNameOnFaktur.Text = ""
        ddlBBNAreaProvince.SelectedIndex = 0
        txtAddressFleetCustomerDtl.Text = ""
        txtNoKTP.Text = ""
        txtNoNIB.Text = ""
        txtProjectName.Text = ""
        icLastPurchaseDate.Value = Date.Now
        txtContractorName.Text = ""
        txtProjectName.Text = ""

        ddlBusinessSector.SelectedIndex = 0
        ddlDealerDirectSales.SelectedIndex = 0
        ddlPurchaseMethod.SelectedIndex = 0
        ddlLeasing.SelectedIndex = 0
        ddlAPMSubsidy.SelectedIndex = 0
        txtDealerNotes.Text = ""
        ddlDeliveryRegionCode.SelectedIndex = 0
        ddlPaymentMethod.SelectedIndex = 0
        ddlPurchaseKind.SelectedIndex = 0
        ddlProjectKindMethod.SelectedIndex = 0
        txtProjectKindMethodOther.Text = ""
        txtDeliveryPlanDate.Text = Format(Date.Now, "MMyyyy")
        txtSuratKomitmen.Text = ""
        txtSuratPernyataan.Text = ""
        txtLampiranPOSPK.Text = ""
        'lblSuratKomitmentKontrak.Text = ""
        'lblSuratPernyataan.Text = ""
        LinkDownloadSuratKomitmentKontrak.Text = ""
        LinkDownloadSuratPernyataan.Text = ""
        lblLampiranPOSPK.Text = ""
        txtMMKSINotes.Text = ""
        chkFinalApproval.Checked = False

        MainPanel.Attributes("style") = "display:table-row"
        PanelDataCustomer.Attributes("style") = "display:none"

        isEnableProperty(True)
        'ViewState.Clear()
        RemoveALLSession()
        sessHelper.SetSession(sessDiscountProposalHdr, New DiscountProposalHeader)
        sessHelper.SetSession(sessDiscountProposalDtl, New ArrayList)
        sessHelper.SetSession(sessDiscountProposalDtlPrice, New ArrayList)
        sessHelper.SetSession(sessDiscountProposalDtlPricePivot, New ArrayList)
        sessHelper.SetSession(sessDiscountProposalDtlPricePivotGrid, New ArrayList)
        sessHelper.SetSession(sessDiscountProposalDtlPriceOld, New ArrayList)
        sessHelper.SetSession(sessDiscountProposalDtlOwnerShip, New ArrayList)
        sessHelper.SetSession(sessDiscountProposalDtlDocument, New ArrayList)
        sessHelper.SetSession(sessDiscountProposalDtlCustomer, New ArrayList)
        sessHelper.SetSession(sessDiscountProposalEmailUser, New ArrayList)
        sessHelper.SetSession(sessHistoryPembelianNew, New ArrayList)
        sessHelper.SetSession(sessHistoryPembelianOld, New ArrayList)
        sessHelper.SetSession(sessHistoryPembelianToPDF, New DataTable)
        sessHelper.SetSession(sessDiscountProposalDtlApproval, New ArrayList)
        sessHelper.SetSession(sessDiscountProposalDtlApprovalToSPL, New ArrayList)
        sessHelper.SetSession(sessDiscountProposalPricetoParameter, New ArrayList)

        sessHelper.SetSession(sessDeleteDiscountProposalDtl, New ArrayList)
        sessHelper.SetSession(sessDeleteDiscountProposalDtlPrice, New ArrayList)
        sessHelper.SetSession(sessDeleteDiscountProposalDtlOwnerShip, New ArrayList)
        sessHelper.SetSession(sessDeleteDiscountProposalDtlDocument, New ArrayList)
        sessHelper.SetSession(sessDeleteDiscountProposalDtlCustomer, New ArrayList)
        sessHelper.SetSession(sessDeleteDiscountProposalDtlApproval, New ArrayList)
        sessHelper.SetSession(sessDeleteDiscountProposalDtlApprovalToSPL, New ArrayList)
        sessHelper.SetSession(sessDeleteDiscountProposalPricetoParameter, New ArrayList)

        BindSessionDataDiscountProposalParameter()

        isInsert = True
        btnBatalSimpanGrid_Click(Nothing, Nothing)
        BindGridRincianHargaKendaraan()
        BindGridKepemilikanKendaraan()
        BindGridUploadFileLampiranPOSPK()
        BindGridDPCustomer()
        BindGridEmailUser()
    End Sub

    Private Function BindSessionDataDiscountProposalParameter()
        Dim objDPHeader As DiscountProposalHeader = sessHelper.GetSession(sessDiscountProposalHdr)
        If IsNothing(objDPHeader) Then objDPHeader = New DiscountProposalHeader
        Dim arlDPParameter As ArrayList = New ArrayList

        If Mode = "New" OrElse objDPHeader.Status = 0 Then
            If Mode <> "View" Then
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DiscountProposalParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(DiscountProposalParameter), "ParameterType", MatchType.Exact, "1"))
                criterias.opAnd(New Criteria(GetType(DiscountProposalParameter), "Status", MatchType.Exact, "1"))
                arlDPParameter = New DiscountProposalParameterFacade(User).Retrieve(criterias)

            Else
                Dim strSql As String = String.Empty
                Dim criterias As New CriteriaComposite(New Criteria(GetType(DiscountProposalParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(DiscountProposalParameter), "ParameterType", MatchType.Exact, "1"))

                strSql = "SELECT DISTINCT a.DiscountProposalParameterID "
                strSql += "FROM DiscountProposalPricetoParameter a "
                strSql += "INNER JOIN DiscountProposalDetailPrice b ON a.DiscountProposalDetailPriceID = b.ID AND b.RowStatus = 0 "
                strSql += "INNER JOIN DiscountProposalDetail c ON b.DiscountProposalDetailID = c.ID AND c.RowStatus = 0 "
                strSql += "WHERE a.RowStatus = 0 "
                strSql += "AND c.DiscountProposalHeaderID ='" & hdnDiscountProposalHeaderID.Value & "'"
                criterias.opAnd(New Criteria(GetType(DiscountProposalParameter), "ID", MatchType.InSet, "(" & strSql & ")"))
                Dim sortColl As SortCollection = New SortCollection
                sortColl.Add(New Sort(GetType(DiscountProposalParameter), "ID", Sort.SortDirection.ASC))
                arlDPParameter = New DiscountProposalParameterFacade(User).RetrieveByCriteria(criterias, sortColl)
            End If
        Else
            Dim strSql As String = String.Empty
            Dim criterias As New CriteriaComposite(New Criteria(GetType(DiscountProposalParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DiscountProposalParameter), "ParameterType", MatchType.Exact, "1"))
            'criterias.opAnd(New Criteria(GetType(DiscountProposalParameter), "Status", MatchType.Exact, "1"))

            strSql = "SELECT DISTINCT a.DiscountProposalParameterID "
            strSql += "FROM DiscountProposalPricetoParameter a "
            strSql += "INNER JOIN DiscountProposalDetailPrice b ON a.DiscountProposalDetailPriceID = b.ID AND b.RowStatus = 0 "
            strSql += "INNER JOIN DiscountProposalDetail c ON b.DiscountProposalDetailID = c.ID AND c.RowStatus = 0 "
            strSql += "WHERE a.RowStatus = 0 "
            strSql += "AND c.DiscountProposalHeaderID ='" & hdnDiscountProposalHeaderID.Value & "'"
            criterias.opAnd(New Criteria(GetType(DiscountProposalParameter), "ID", MatchType.InSet, "(" & strSql & ")"))
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(DiscountProposalParameter), "ID", Sort.SortDirection.ASC))
            arlDPParameter = New DiscountProposalParameterFacade(User).RetrieveByCriteria(criterias, sortColl)
        End If

        sessHelper.SetSession(sessDiscountProposalParameter, arlDPParameter)
        ViewState("RowCountProgramRegulerMMKSI") = arlDPParameter.Count
    End Function

    Private Function GenerateRegNumber() As String
        Dim YearParameter As Integer = Date.Today.Year
        If Date.Today.Month = 12 Then
            YearParameter = Date.Today.Year + 1
        End If
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalHeader), "CreatedTime", MatchType.GreaterOrEqual, Date.Today.Year & Date.Today.Month.ToString("d2") & "01"))
        crit.opAnd(New Criteria(GetType(DiscountProposalHeader), "CreatedTime", MatchType.Lesser, YearParameter & Date.Today.AddMonths(1).Month.ToString("d2") & "01"))

        Dim arrl As ArrayList = New DiscountProposalHeaderFacade(User).Retrieve(crit)
        Dim _result As String
        If arrl.Count > 0 Then
            Dim objBH As DiscountProposalHeader = CommonFunction.SortListControl(arrl, "ProposalRegNo", Sort.SortDirection.DESC)(0)
            Dim noReg As String = objBH.ProposalRegNo
            _result = (CInt(noReg.Substring(0, 3)) + 1).ToString("d3") & "/DISC/" & Date.Today.Month.ToString("d2") & "/" & Date.Today.Year.ToString("d4")
        Else
            _result = (CInt(1)).ToString("d3") & "/DISC/" & Date.Today.Month.ToString("d2") & "/" & Date.Today.Year.ToString("d4")
        End If

        Return _result
    End Function

    Private Function GenerateFleetDetailCode(ByVal strFleetCode As String) As String
        Dim _result As String = ""
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetCustomerDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(FleetCustomerDetail), "FleetCustomerHeader.FleetCode", MatchType.Exact, strFleetCode))
        Dim arrl As ArrayList = New FleetCustomerDetailFacade(User).Retrieve(criterias)
        If Not IsNothing(arrl) Then
            If arrl.Count > 0 Then
                Dim objFCD As FleetCustomerDetail = CommonFunction.SortListControl(arrl, "FleetDetailCode", Sort.SortDirection.DESC)(0)
                Dim noReg As String = objFCD.FleetDetailCode
                If Len(noReg) = 10 Then
                    _result = strFleetCode & "-" & (CInt(noReg.Substring(7, 3)) + 1).ToString("d3")
                Else
                    _result = strFleetCode & "-" & (CInt(1)).ToString("d3")
                End If
            Else
                _result = strFleetCode & "-" & (CInt(1)).ToString("d3")
            End If
        Else
            _result = strFleetCode & "-" & (CInt(1)).ToString("d3")
        End If
        Return _result
    End Function

    Private Function GenerateFleetCode() As String
        Dim arrl As ArrayList = New FleetCustomerHeaderFacade(User).RetrieveList()
        Dim _result As String = ""
        If arrl.Count > 0 Then
            Dim objFCH As FleetCustomerHeader = CommonFunction.SortListControl(arrl, "FleetCode", Sort.SortDirection.DESC)(0)
            Dim noReg As String = objFCH.FleetCode
            If Len(noReg) = 6 Then
                _result = "F" & (CInt(noReg.Substring(1, 5)) + 1).ToString("d5")
            Else
                _result = "F" & (CInt(1)).ToString("d5")
            End If
        Else
            _result = "F" & (CInt(1)).ToString("d5")
        End If
        Return _result
    End Function

    Private Function TimeStamp() As String
        Return DateTime.Now.Year & DateTime.Now.Month & DateTime.Now.Day & DateTime.Now.Hour & DateTime.Now.Minute & DateTime.Now.Second & DateTime.Now.Millisecond
    End Function

    Private Sub CommitAttachment(ByVal AttachmentCollection As ArrayList)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim TargetFInfo As FileInfo
        Dim TempFInfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                For Each obj As DiscountProposalDetailDocument In AttachmentCollection
                    If Not IsNothing(obj.AttachmentData) Then
                        TargetFInfo = New FileInfo(TargetDirectory + obj.Path)
                        TempFInfo = New FileInfo(TempDirectory + obj.Path)

                        If TempFInfo.Exists Then
                            If Not TargetFInfo.Directory.Exists Then
                                Directory.CreateDirectory(TargetFInfo.DirectoryName)
                            End If
                            TempFInfo.MoveTo(TargetFInfo.FullName)
                        End If
                        obj.AttachmentData.SaveAs(TargetDirectory + obj.Path)
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub GetDealerData(ByVal oDealer As Dealer)
        txtDealerCode.Attributes("style") = "display:none"
        lblPopUpDealer.Attributes("style") = "display:none"
        lblDealerCodeName.Visible = True
        lblDealerCodeName.Text = oDealer.DealerCode & " / " & oDealer.DealerName
        lblPopUpDealer.Attributes("onClick") = "ShowPPDealerSelection()"
        txtDealerCode.Text = oDealer.DealerCode
    End Sub

    Private Sub BindCheckBoxConsideration(Optional ByVal arlObj As ArrayList = Nothing)
        Dim isEdit As Boolean = False
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DiscountProposalParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(DiscountProposalParameter), "ParameterType", MatchType.Exact, "3"))
        criterias.opAnd(New Criteria(GetType(DiscountProposalParameter), "Status", MatchType.Exact, "1"))

        Dim arlConsideration As ArrayList = New DiscountProposalParameterFacade(User).Retrieve(criterias)
        If Not IsNothing(arlObj) Then
            isEdit = True
        End If
        Dim Litems As ListItem = New ListItem
        CBConsideration.Items.Clear()
        For Each mp As DiscountProposalParameter In arlConsideration
            Dim Litem As ListItem = New ListItem
            Litem.Text = mp.ParameterName
            Litem.Value = mp.ID
            If isEdit Then
                For Each ibpd As DiscountProposalParameter In arlObj
                    If mp.ID = ibpd.ID Then
                        Litem.Selected = True
                        Exit For
                    End If
                Next
            End If
            CBConsideration.Items.Add(Litem)
        Next
    End Sub

    Private Sub BindDDLBusinessSector()
        With ddlBusinessSector
            .DataSource = New VWI_BusinessSectorFacade(User).RetrieveList()
            .DataValueField = "ID"
            .DataTextField = "BusinessName"
            .DataBind()
            .Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
        End With
    End Sub

    Private Sub BindDDLDealerDirectSales()
        With ddlDealerDirectSales
            .Items.Clear()
            .DataSource = New StandardCodeFacade(User).RetrieveByCategory("EnumDiscountProposal.DealerDirectSales")
            .DataTextField = "ValueDesc"
            .DataValueField = "ValueId"
            .DataBind()
            .Items.Insert(0, New ListItem("Silakan Pilih ", -1))
            .SelectedIndex = 0
        End With
    End Sub

    Sub BindDDLFleetCategoryCustomer()
        With ddlFleetCategory
            .Items.Clear()
            .DataSource = New StandardCodeFacade(User).RetrieveByCategory("EnumDiscountProposal.FleetCategory")
            .DataTextField = "ValueDesc"
            .DataValueField = "ValueId"
            .DataBind()
            .Items.Insert(0, New ListItem("Silakan Pilih ", -1))
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub BindDDLProjectKindMethod()
        With ddlProjectKindMethod
            .Items.Clear()
            .DataSource = New StandardCodeFacade(User).RetrieveByCategory("EnumDiscountProposal.ProjectKindMethod")
            .DataTextField = "ValueDesc"
            .DataValueField = "ValueId"
            .DataBind()
            .Items.Insert(0, New ListItem("Silakan Pilih ", -1))
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub BindDDLPurchaseKind()
        With ddlPurchaseKind
            .Items.Clear()
            .DataSource = New StandardCodeFacade(User).RetrieveByCategory("EnumDiscountProposal.PurchaseKind")
            .DataTextField = "ValueDesc"
            .DataValueField = "ValueId"
            .DataBind()
            .Items.Insert(0, New ListItem("Silakan Pilih ", -1))
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub BindDDLBBNAreaProvince()
        Dim arrDDL As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        arrDDL = New ProvinceFacade(User).Retrieve(criterias)

        With ddlBBNAreaProvince
            .Items.Clear()
            .DataSource = arrDDL
            .DataTextField = "ProvinceName"
            .DataValueField = "ID"
            .DataBind()
            .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        End With
    End Sub

    Private Sub BindDDLDeliveryRegionCode()
        Dim arrDDL As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(V_LogisticPriceAndNationality), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(V_LogisticPriceAndNationality), "RegionDescription", Sort.SortDirection.ASC))  '-- Sort by Vechile type code
        arrDDL = New V_LogisticPriceAndNationalityFacade(User).RetrieveByCriteria(criterias, sortColl)

        With ddlDeliveryRegionCode
            .Items.Clear()
            .DataSource = arrDDL
            .DataTextField = "RegionDescription"
            .DataValueField = "RegionCode"
            .DataBind()
            .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        End With
    End Sub

    Private Sub BindDDLPaymentMethod()
        With ddlPaymentMethod
            .Items.Clear()
            .DataSource = New StandardCodeFacade(User).RetrieveByCategory("EnumDiscountProposal.PaymentMethod")
            .DataTextField = "ValueDesc"
            .DataValueField = "ValueId"
            .DataBind()
            .Items.Insert(0, New ListItem("Silakan Pilih ", -1))
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub BindDDLPurchaseMethod()
        With ddlPurchaseMethod
            .Items.Clear()
            .DataSource = New StandardCodeFacade(User).RetrieveByCategory("EnumDiscountProposal.PurchaseMethod")
            .DataTextField = "ValueDesc"
            .DataValueField = "ValueId"
            .DataBind()
            .Items.Insert(0, New ListItem("Silakan Pilih ", -1))
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub BindDDLAPMSubsidy()
        With ddlAPMSubsidy
            .Items.Clear()
            .DataSource = New StandardCodeFacade(User).RetrieveByCategory("EnumDiscountProposal.APMSubsidy")
            .DataTextField = "ValueDesc"
            .DataValueField = "ValueId"
            .DataBind()
            .Items.Insert(0, New ListItem("Silakan Pilih ", -1))
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub BindDDLCustomerType()
        With ddlCustomerType
            .Items.Clear()
            .DataSource = New StandardCodeFacade(User).RetrieveByCategory("EnumDiscountProposal.CustomerType")
            .DataTextField = "ValueDesc"
            .DataValueField = "ValueId"
            .DataBind()
            .Items.Insert(0, New ListItem("Silakan Pilih ", -1))
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub BindDDLLeasing()
        Dim arrDDL As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(LeasingCompany), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        arrDDL = New LeasingCompanyFacade(User).Retrieve(criterias)

        With ddlLeasing
            .Items.Clear()
            .DataSource = arrDDL
            .DataTextField = "LeasingName"
            .DataValueField = "ID"
            .DataBind()
            .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        End With
    End Sub

    Sub BindGridEmailUser()
        arlDiscountProposalEmailUser = CType(sessHelper.GetSession(sessDiscountProposalEmailUser), ArrayList)
        If IsNothing(arlDiscountProposalEmailUser) Then arlDiscountProposalEmailUser = New ArrayList()

        GenerateNumberRow("DiscountProposalEmailUser", arlDiscountProposalEmailUser)
        sessHelper.SetSession(sessDiscountProposalEmailUser, arlDiscountProposalEmailUser)

        dgEmailUser.DataSource = arlDiscountProposalEmailUser
        dgEmailUser.DataBind()
    End Sub

    Sub BindGridKepemilikanKendaraan()
        arlDiscountProposalDtlOwnerShip = CType(sessHelper.GetSession(sessDiscountProposalDtlOwnerShip), ArrayList)
        If IsNothing(arlDiscountProposalDtlOwnerShip) Then arlDiscountProposalDtlOwnerShip = New ArrayList()

        GenerateNumberRow("DiscountProposalDetailOwnerShip", arlDiscountProposalDtlOwnerShip)
        sessHelper.SetSession(sessDiscountProposalDtlOwnerShip, arlDiscountProposalDtlOwnerShip)

        dgKepemilikanKendaraan.DataSource = arlDiscountProposalDtlOwnerShip
        dgKepemilikanKendaraan.DataBind()
    End Sub

    Sub BindGridProposedDiscount()
        arlDiscountProposalDtlApprovalToSPL = CType(sessHelper.GetSession(sessDiscountProposalDtlApprovalToSPL), ArrayList)
        If IsNothing(arlDiscountProposalDtlApprovalToSPL) Then arlDiscountProposalDtlApprovalToSPL = New ArrayList()
        arlDiscountProposalDtlApproval = CType(sessHelper.GetSession(sessDiscountProposalDtlApproval), ArrayList)
        If IsNothing(arlDiscountProposalDtlApproval) Then arlDiscountProposalDtlApproval = New ArrayList()

        GenerateNumberRow("DISCOUNTPROPOSALDETAILAPPROVALTOSPL", arlDiscountProposalDtlApprovalToSPL)
        GenerateNumberRow("DISCOUNTPROPOSALDETAILAPPROVAL", arlDiscountProposalDtlApproval)

        Dim dataList As ArrayList = New ArrayList
        dataList = New System.Collections.ArrayList(
                        (From obj As DiscountProposalDetailApprovaltoSPL In arlDiscountProposalDtlApprovalToSPL.OfType(Of DiscountProposalDetailApprovaltoSPL)()
                            Where obj.LabelTotal <> "Total Discount :"
                            Order By obj.ModelID, obj.VechileTypeID
                            Select obj).ToList())

        Dim objDPParamDtl As New DiscountProposalParameter
        Dim oDiscountProposalDetailApprovaltoSPL As New DiscountProposalDetailApprovaltoSPL
        Dim strModelID As String = ""
        Dim strVechileTypeID As String = ""

        Dim arlDiscountProposalDtlApprovalToSPL2 As ArrayList = New ArrayList
        For i As Integer = 0 To dataList.Count - 1
            Dim oDPAtoSPL As DiscountProposalDetailApprovaltoSPL = CType(dataList(i), DiscountProposalDetailApprovaltoSPL)
            If i = 0 Then
                strModelID = oDPAtoSPL.ModelID
                strVechileTypeID = oDPAtoSPL.VechileTypeID
            End If
            If strVechileTypeID <> oDPAtoSPL.VechileTypeID OrElse strModelID <> oDPAtoSPL.ModelID Then
                oDiscountProposalDetailApprovaltoSPL = New DiscountProposalDetailApprovaltoSPL
                oDiscountProposalDetailApprovaltoSPL.DiscountProposalDetailApproval = New DiscountProposalDetailApproval
                oDiscountProposalDetailApprovaltoSPL.ModelID = strModelID
                oDiscountProposalDetailApprovaltoSPL.VechileTypeID = strVechileTypeID
                oDiscountProposalDetailApprovaltoSPL.LabelTotal = "Total Discount :"
                oDiscountProposalDetailApprovaltoSPL.TotalDiscount = GetTotalPriceByVechileTypeID(strModelID, strVechileTypeID)
                arlDiscountProposalDtlApprovalToSPL2.Add(oDiscountProposalDetailApprovaltoSPL)
                strModelID = oDPAtoSPL.ModelID
                strVechileTypeID = oDPAtoSPL.VechileTypeID
            End If

            arlDiscountProposalDtlApprovalToSPL2.Add(oDPAtoSPL)
            If i = dataList.Count - 1 Then
                oDiscountProposalDetailApprovaltoSPL = New DiscountProposalDetailApprovaltoSPL
                oDiscountProposalDetailApprovaltoSPL.DiscountProposalDetailApproval = New DiscountProposalDetailApproval
                oDiscountProposalDetailApprovaltoSPL.ModelID = oDPAtoSPL.ModelID
                oDiscountProposalDetailApprovaltoSPL.VechileTypeID = oDPAtoSPL.VechileTypeID
                oDiscountProposalDetailApprovaltoSPL.LabelTotal = "Total Discount :"
                oDiscountProposalDetailApprovaltoSPL.TotalDiscount = GetTotalPriceByVechileTypeID(strModelID, strVechileTypeID)
                arlDiscountProposalDtlApprovalToSPL2.Add(oDiscountProposalDetailApprovaltoSPL)
            End If
        Next
        Dim dataList2 As ArrayList = New System.Collections.ArrayList(
                        (From obj As DiscountProposalDetailApprovaltoSPL In arlDiscountProposalDtlApprovalToSPL2.OfType(Of DiscountProposalDetailApprovaltoSPL)()
                            Order By obj.ModelID, obj.VechileTypeID
                            Select obj).ToList())

        sessHelper.SetSession(sessDiscountProposalDtlApprovalToSPL, dataList2)
        sessHelper.SetSession(sessDiscountProposalDtlApproval, arlDiscountProposalDtlApproval)

        dgProposedDiscount.DataSource = dataList2
        dgProposedDiscount.DataBind()
    End Sub

    Private Function GetTotalPriceByVechileTypeID(ByVal strModelID As String, ByVal strVechileTypeID As String) As Double
        Dim dblSumTotalDiscount As Double = 0
        dblSumTotalDiscount = (From item As DiscountProposalDetailApprovaltoSPL In arlDiscountProposalDtlApprovalToSPL
                                 Where item.VechileTypeID = strVechileTypeID And item.ModelID = strModelID And item.LabelTotal <> "Total Discount :"
                                Select (item.DiscountProposed)).Sum()
        Return dblSumTotalDiscount
    End Function

    Sub BindGridDPCustomer()
        arlDiscountProposalDtlCustomer = CType(sessHelper.GetSession(sessDiscountProposalDtlCustomer), ArrayList)
        If IsNothing(arlDiscountProposalDtlCustomer) Then arlDiscountProposalDtlCustomer = New ArrayList()

        dgDPCustomer.DataSource = arlDiscountProposalDtlCustomer
        dgDPCustomer.DataBind()
        sessHelper.SetSession(sessDiscountProposalDtlCustomer, arlDiscountProposalDtlCustomer)
    End Sub

    Sub BindGridUploadFileLampiranPOSPK()
        Dim arlDocumentLampiranPOSPK As New ArrayList
        arlDiscountProposalDtlDocument = CType(sessHelper.GetSession(sessDiscountProposalDtlDocument), ArrayList)
        If IsNothing(arlDiscountProposalDtlDocument) Then arlDiscountProposalDtlDocument = New ArrayList()

        For Each objDPDoc As DiscountProposalDetailDocument In arlDiscountProposalDtlDocument
            If objDPDoc.FileType = 2 Then   '---ValueCode = LampiranPOSPK
                arlDocumentLampiranPOSPK.Add(objDPDoc)
            End If
        Next
        dgUploadFile.DataSource = arlDocumentLampiranPOSPK
        dgUploadFile.DataBind()
    End Sub

    Sub BindGridHistoryPembelianReload()
        arlHistoryPembelian = New DataTable
        Dim intPsnMiripName As Integer = 0
        Dim intPsnMiripNIKNIB As Integer = 0
        Dim strNoKTPNIB As String = ""
        If ddlCustomerType.SelectedValue = 0 Then
            strNoKTPNIB = txtNoKTP.Text.Trim
        ElseIf ddlCustomerType.SelectedValue = 1 OrElse ddlCustomerType.SelectedValue = 2 Then
            strNoKTPNIB = txtNoNIB.Text.Trim
        End If

        If strNoKTPNIB <> "" AndAlso txtFleetCustomerName.Text.Trim <> "" Then
            Dim oldTable As New DataTable()
            With oldTable
                .Columns.Add("Model", GetType(String))
                .Columns.Add("Tahun_Faktur", GetType(String))
                .Columns.Add("Jumlah", GetType(Integer))
            End With

            Dim dsDPH As DataSet = New DiscountProposalHeaderFacade(User).DoRetrieveDataHistoryPembelian(txtFleetCustomerName.Text.Trim, strNoKTPNIB)
            Dim tblDPH As DataTable = dsDPH.Tables(0)
            Dim tblCustomerID As DataTable = dsDPH.Tables(1)

            If IsNothing(tblDPH) Then tblDPH = New DataTable
            If IsNothing(tblCustomerID) Then tblCustomerID = New DataTable

            Dim NonDistList As New List(Of String)
            Dim strSplitCustomerID As String = ""
            arlFleetCustomerDetailMappingNew = New ArrayList
            For Each row As DataRow In tblCustomerID.Rows
                strSplitCustomerID = row.Item("CustomerID")
                Dim elements() As String = strSplitCustomerID.Split(New Char() {","c},
                        StringSplitOptions.RemoveEmptyEntries)
                For i As Integer = 0 To elements.Length - 1
                    NonDistList.Add(elements(i))
                Next
            Next row

            Dim Dict As New Dictionary(Of String, String)
            For Each CustID As String In NonDistList.ToList
                If Not Dict.ContainsKey(CustID) Then
                    Dict.Add(CustID, CustID)
                    Dim objFleetCustomerDetailMapping As New FleetCustomerDetailMapping
                    Dim objCustomer As Customer = New CustomerFacade(User).Retrieve(CInt(CustID))
                    objFleetCustomerDetailMapping.Customer = objCustomer
                    objFleetCustomerDetailMapping.FleetCustomerHeader = New FleetCustomerHeader
                    arlFleetCustomerDetailMappingNew.Add(objFleetCustomerDetailMapping)
                Else
                    NonDistList.Remove(CustID)
                End If
            Next
            sessHelper.SetSession(sessHistoryPembelianNew, arlFleetCustomerDetailMappingNew)

            sessHelper.SetSession(sessHistoryPembelianToPDF, tblDPH)
            arlHistoryPembelian = GetInversedDataTable(tblDPH, "Tahun_Faktur", "Model", "Jumlah", "", True)
        End If
        If arlHistoryPembelian.Rows.Count > 0 Then
            dgHistoryPembelian.DataSource = arlHistoryPembelian
            dgHistoryPembelian.VirtualItemCount = arlHistoryPembelian.Rows.Count
            dgHistoryPembelian.DataBind()
            MessageBox.Show("Data Histori Pembelian ditemukan")
        Else
            MessageBox.Show("Tidak ada Data Histori Pembelian")
        End If
    End Sub

    Sub BindGridHistoryPembelian(Optional ByVal _fleetCustHeaderID As Integer = 0)
        arlHistoryPembelian = New DataTable
        With arlHistoryPembelian
            .Columns.Add("Model", GetType(String))
            .Columns.Add("Tahun_Faktur", GetType(String))
            .Columns.Add("Jumlah", GetType(Integer))
        End With

        If _fleetCustHeaderID <> 0 Then
            Dim strCustIDs As String = ""
            Dim criterias As New CriteriaComposite(New Criteria(GetType(FleetCustomerDetailMapping), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(FleetCustomerDetailMapping), "FleetCustomerHeader.ID", MatchType.Exact, _fleetCustHeaderID))
            Dim arrFleetCustomerDetailMapping As ArrayList = New FleetCustomerDetailMappingFacade(User).Retrieve(criterias)
            If Not IsNothing(arrFleetCustomerDetailMapping) AndAlso arrFleetCustomerDetailMapping.Count > 0 Then
                For Each objFleetCustomerDetailMapping As FleetCustomerDetailMapping In arrFleetCustomerDetailMapping
                    If strCustIDs = "" Then
                        strCustIDs = objFleetCustomerDetailMapping.Customer.ID
                    Else
                        strCustIDs += "," & objFleetCustomerDetailMapping.Customer.ID.ToString
                    End If
                Next
            End If

            sessHelper.SetSession(sessHistoryPembelianOld, arrFleetCustomerDetailMapping)

            If strCustIDs <> "" Then
                Dim dsDPH As DataSet = New DiscountProposalHeaderFacade(User).DoRetrieveDataSet(strCustIDs)
                If dsDPH.Tables.Count > 0 Then
                    Dim tblDPH As DataTable = dsDPH.Tables(0)
                    Dim oldTable As New DataTable()
                    With oldTable
                        .Columns.Add("Model", GetType(String))
                        .Columns.Add("Tahun_Faktur", GetType(String))
                        .Columns.Add("Jumlah", GetType(Integer))
                    End With

                    For Each dr As DataRow In tblDPH.Rows
                        Dim rowA As DataRow = oldTable.Rows.Add()
                        rowA(0) = dr("Model")
                        rowA(1) = dr("Tahun_Faktur")
                        rowA(2) = dr("Jumlah")
                    Next
                    If IsNothing(oldTable) Then oldTable = New DataTable
                    sessHelper.SetSession(sessHistoryPembelianToPDF, oldTable)
                    arlHistoryPembelian = GetInversedDataTable(oldTable, "Tahun_Faktur", "Model", "Jumlah", "", True)
                End If
            End If
        End If
        dgHistoryPembelian.DataSource = arlHistoryPembelian
        dgHistoryPembelian.VirtualItemCount = arlHistoryPembelian.Rows.Count
        dgHistoryPembelian.DataBind()
    End Sub

    Function GetInversedDataTable(ByVal table As DataTable, ByVal columnX As String, ByVal columnY As String, ByVal columnZ As String, ByVal nullValue As String, ByVal sumValues As Boolean) As DataTable
        ' ColumnX = Is the Column that you want to make cross tab (Column shown in header)
        ' ColumnY = Is the column that will be put in the left column (company_code)
        ' ColumnZ = Is the total value from combining columnX and columnY (PAY_AMOUNT)

        'Create a DataTable to Return
        Dim returnTable As New DataTable()
        If columnX = "" Then
            columnX = table.Columns(0).ColumnName
        End If

        'Add a Column at the beginning of the table
        returnTable.Columns.Add(columnY)

        'Read all DISTINCT values from columnX Column in the provided DataTale
        Dim columnXValues As New List(Of String)()

        For Each dr As DataRow In table.Rows
            Dim columnXTemp As String = dr(columnX).ToString()
            If Not columnXValues.Contains(columnXTemp) Then
                'Read each row value, if it's different from others provided, add to the list of values and creates a new Column with its value.
                columnXValues.Add(columnXTemp)
                returnTable.Columns.Add(columnXTemp)
            End If
        Next

        returnTable.Columns.Add("Total")

        'Verify if Y and Z Axis columns re provided
        If columnY <> "" AndAlso columnZ <> "" Then
            'Read DISTINCT Values for Y Axis Column
            Dim columnYValues As New List(Of String)()

            For Each dr As DataRow In table.Rows
                If Not columnYValues.Contains(dr(columnY).ToString()) Then
                    columnYValues.Add(dr(columnY).ToString())
                End If
            Next

            columnYValues.Add("Total")

            'Loop all Column Y Distinct Value
            For Each columnYValue As String In columnYValues
                'Creates a new Row
                Dim drReturn As DataRow = returnTable.NewRow()
                drReturn(0) = columnYValue
                'foreach column Y value, The rows are selected distincted
                Dim rows As DataRow() = table.[Select]((columnY & "='") + columnYValue & "'")

                'Read each row to fill the DataTable
                For Each dr As DataRow In rows
                    Dim rowColumnTitle As String = dr(columnX).ToString()

                    'Read each column to fill the DataTable
                    Dim Tot As Integer = 0
                    For Each dc As DataColumn In returnTable.Columns
                        If dc.ColumnName = rowColumnTitle Then
                            'If Sum of Values is True it try to perform a Sum
                            'If sum is not possible due to value types, the value displayed is the last one read
                            If sumValues Then
                                Try
                                    drReturn(rowColumnTitle) = Convert.ToDecimal(drReturn(rowColumnTitle)) + Convert.ToDecimal(dr(columnZ))
                                Catch
                                    drReturn(rowColumnTitle) = dr(columnZ)
                                End Try
                            Else
                                drReturn(rowColumnTitle) = dr(columnZ)
                            End If
                        End If

                        'If Not dc.ColumnName.ToLower = "model" Then
                        '    Tot += dr(columnZ)
                        'End If

                        'If dc.ColumnName.ToLower = "total" Then
                        '    drReturn(rowColumnTitle) = Tot
                        'End If
                    Next
                Next

                returnTable.Rows.Add(drReturn)
            Next
        Else
            Throw New Exception("The columns to perform inversion are not provided")
        End If

        'if a nullValue is provided, fill the datable with it
        If nullValue <> "" Then
            For Each dr As DataRow In returnTable.Rows
                For Each dc As DataColumn In returnTable.Columns
                    If dr(dc.ColumnName).ToString() = "" Then
                        dr(dc.ColumnName) = nullValue
                    End If
                Next
            Next
        End If

        For Each Rw As DataRow In returnTable.Rows
            Dim TotSamping As Integer = 0
            For Each Col As DataColumn In returnTable.Columns
                If Not Col.ColumnName.ToLower = "model" Then
                    If Not Rw.Item(Col).GetType() Is GetType(DBNull) Then
                        TotSamping = TotSamping + Convert.ToInt32(Rw.Item(Col))
                    End If
                End If
            Next
            Rw("Total") = TotSamping
        Next

        For Each dtCol As DataColumn In returnTable.Columns
            Dim TotBawah As Integer = 0
            Dim i As Integer = 0

            For Each dtRw As DataRow In returnTable.Rows
                If Not dtCol.ColumnName.ToLower = "model" Then
                    If Not dtRw.Item(dtCol).GetType() Is GetType(DBNull) Then
                        TotBawah = TotBawah + Convert.ToInt32(dtRw.Item(dtCol))
                    End If
                End If

                If i = returnTable.Rows.Count - 1 AndAlso Not dtCol.ColumnName.ToLower = "model" Then
                    dtRw(dtCol) = TotBawah
                End If

                i = i + 1

            Next
        Next

        Return returnTable

    End Function

    Private Function PivotTable2(ByVal dt As DataTable, ByVal ColNum As Integer) As DataTable
        Dim dp As New DataTable("Pivoted")
        If ColNum > 0 Then
            dp.Columns.Add(dt.Columns(ColNum - 1).ColumnName)
        Else
            dp.Columns.Add(dt.Columns(ColNum).ColumnName)
        End If
        For Each row As DataRow In dt.Rows
            dp.Columns.Add(row.Item(ColNum).ToString)     '<----a duplicate column name exception here(A column named 'Eng' already belongs to this DataTable.)
        Next

        Dim IColumns As IEnumerable(Of DataColumn) = From c As DataColumn In dt.Columns
                                                     Where c.Ordinal <> ColNum

        For Each col As DataColumn In IColumns
            Dim tr(dt.Rows.Count) As Object
            'tr(0) = col.ColumnName

            Dim i As Integer = 0
            For Each row As DataRow In dt.Rows
                tr(i) = row.Item(col.Ordinal)
                i += 1
            Next

            dp.Rows.Add(tr)
        Next
        Return dp
    End Function

    Private Sub RemoveDiscountProposalDetailDocumentAttachment(ByVal ObjAttachment As DiscountProposalDetailDocument, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                finfo = New FileInfo(TargetPath + ObjAttachment.Path)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function ValidateIsMandatoryParameterDP(ByRef strParamName As String, ByVal intParameterType As Integer) As String
        strParamName = String.Empty

        Dim isMandatory As Boolean = False
        Dim criterias As New CriteriaComposite(New Criteria(GetType(DiscountProposalParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(DiscountProposalParameter), "IsMandatory", MatchType.Exact, 1))
        criterias.opAnd(New Criteria(GetType(DiscountProposalParameter), "Status", MatchType.Exact, 1))
        criterias.opAnd(New Criteria(GetType(DiscountProposalParameter), "ParameterType", MatchType.Exact, intParameterType))

        Dim arrDiscountProposalParameter As ArrayList = New DiscountProposalParameterFacade(User).Retrieve(criterias)
        If Not IsNothing(arrDiscountProposalParameter) AndAlso arrDiscountProposalParameter.Count > 0 Then
            Dim CBList As ListItemCollection = New ListItemCollection
            CBList = CBConsideration.Items
            Dim countUnChecked As Integer = 0
            For Each oDPParam As DiscountProposalParameter In arrDiscountProposalParameter
                For Each cbm As ListItem In CBList
                    If CInt(cbm.Value) = oDPParam.ID Then
                        If cbm.Selected = False Then
                            countUnChecked += 1
                        End If
                        Exit For
                    End If
                Next
            Next
            If arrDiscountProposalParameter.Count = countUnChecked Then
                strParamName = "Harap pilih salah satu Consideration"
            End If
        End If

        Return strParamName
    End Function

    Private Function ValidateSaveData() As String
        Dim sb As StringBuilder = New StringBuilder

        If Not IsLoginAsDealer() Then
            If (txtDealerCode.Text = String.Empty) Then
                sb.Append("- Kode Dealer Harus Diisi\n")
            End If
        End If

        If (ddlCustomerType.SelectedIndex = 0) Then
            sb.Append("- Tipe Customer Harus Diisi\n")
        End If

        If Mode = "Edit" Then
            hdnDiscountProposalHeaderID.Value = Request.QueryString("DiscountProposalHeaderID")
            Dim objDiscountProposalHeader As DiscountProposalHeader = New DiscountProposalHeaderFacade(User).Retrieve(CInt(hdnDiscountProposalHeaderID.Value))
            If objDiscountProposalHeader.Status = 0 Then
                If ddlCustomerType.SelectedValue = 0 Then
                    If (txtNoKTP.Text.Trim = String.Empty) Then
                        sb.Append("- Nomor KTP Harus Diisi\n")
                    End If
                Else
                    If (txtNoNIB.Text.Trim = String.Empty) Then
                        sb.Append("- Nomor NIB Harus Diisi\n")
                    End If
                End If

                If (txtAddressFleetCustomerDtl.Text.Trim = String.Empty) Then
                    sb.Append("- Alamat Harus Diisi\n")
                End If
            End If
        Else
            If ddlCustomerType.SelectedValue = 0 Then
                If (txtNoKTP.Text.Trim = String.Empty) Then
                    sb.Append("- Nomor KTP Harus Diisi\n")
                End If
            Else
                If (txtNoNIB.Text.Trim = String.Empty) Then
                    sb.Append("- Nomor NIB Harus Diisi\n")
                End If
            End If

            If (txtAddressFleetCustomerDtl.Text.Trim = String.Empty) Then
                sb.Append("- Alamat Harus Diisi\n")
            End If
        End If

        If (txtDealerProposalNo.Text.Trim = String.Empty) Then
            sb.Append("- Nomor Aplikasi Dealer Harus Diisi\n")
        End If
        If (ddlFleetCategory.SelectedIndex = 0) Then
            sb.Append("- Kategori Fleet Customer Harus Diisi\n")
        End If
        If (txtFleetCustomerName.Text.Trim = String.Empty) Then
            sb.Append("- Nama Fleet Customer Harus Diisi\n")
        End If
        If (txtNameOnFaktur.Text.Trim = String.Empty) Then
            sb.Append("- Nama di Faktur/STNK Harus Diisi\n")
        End If
        If (ddlBBNAreaProvince.SelectedValue.Trim = String.Empty) Then
            sb.Append("- BBN Area Harus Diisi\n")
        End If
        If (txtProjectName.Text.Trim = String.Empty) Then
            sb.Append("- Nama Proyek Harus Diisi\n")
        End If
        'If (CInt(Format(icLastPurchaseDate.Value, "yyyyMMdd")) < CInt(Format(CDate(hdnSubmitDate.Value), "yyyyMMdd"))) Then
        '    sb.Append("- Data Terakhir Pembelian harus lebih besar atau sama dengan tanggal hari ini\n")
        'End If
        If (ddlBusinessSector.SelectedIndex = 0) Then
            sb.Append("- Profil Bisnis Harus Dipilih\n")
        End If
        If ddlCustomerType.SelectedValue = 2 Then
            If (ddlDealerDirectSales.SelectedIndex = 0) Then
                sb.Append("- Penjualan Langsung oleh Dealer Harus Dipilih karena Tipe Customer BUMN/Pemerintah\n")
            End If
        End If
        If (ddlDealerDirectSales.SelectedValue = 1) Then
            If (txtContractorName.Text.Trim = String.Empty) Then
                sb.Append("- Nama Kontraktor Harus Diisi\n")
            End If
            If (txtSuratKomitmen.Text.Trim = String.Empty) Then
                'sb.Append("- Surat Komitmen Kontraktor belum di upload\n")
            End If
        End If

        'jika bisnis sektor = Pemerintah - Militer atau TNI dan Pemerintah - Polisi
        If ddlBusinessSector.SelectedValue = "25" OrElse ddlBusinessSector.SelectedValue = "26" Then
            If (txtSuratPernyataan.Text.Trim = String.Empty) Then
                sb.Append("- Surat Pernyataan Harus Diisi\n")
            End If
        End If
        If (ddlPurchaseMethod.SelectedIndex = 0) Then
            sb.Append("- Metode Pembelian Harus Dipilih\n")
        End If
        If (ddlPaymentMethod.SelectedIndex = 0) Then
            sb.Append("- Metode Pembayaran Harus Dipilih\n")
        End If
        If (ddlPurchaseKind.SelectedIndex = 0) Then
            sb.Append("- Jenis Pembelian Harus Dipilih\n")
        End If
        If (ddlProjectKindMethod.SelectedIndex = 0) Then
            sb.Append("- Metode Pengadaan Proyek Harus Dipilih\n")
        End If
        If (ddlProjectKindMethod.SelectedValue = 2) Then
            If (txtProjectKindMethodOther.Text.Trim = "") Then
                sb.Append("- Metode Pengadaan Proyek lainnya harus diisi\n")
            End If
        End If
        If (ddlDeliveryRegionCode.SelectedIndex = 0) Then
            sb.Append("- Area Pengiriman Harus Dipilih\n")
        End If

        If txtDeliveryPlanDate.Text.Trim = "" Then
            sb.Append("- Waktu Pengiriman Harus Diisi\n")
        End If
        Dim sValid As String = IsMonthYearValid(txtDeliveryPlanDate.Text, Format(CDate(hdnSubmitDate.Value), "MMyyyy"))
        If sValid.Trim <> "" Then
            sb.Append("- " & sValid & "\n")
        End If

        If Not IsLoginAsDealer() Then
            Dim strParamCategoryName As String = String.Empty
            If ValidateIsMandatoryParameterDP(strParamCategoryName, 3) <> String.Empty Then
                sb.Append("- Consideration :\n" & strParamCategoryName & "\n")
            End If
        End If

        If (sessHelper.GetSession(sessDiscountProposalDtl) Is Nothing) Then
            sb.Append("- Data Jadwal Pengadaan belum ada\n")
        Else
            If CType(sessHelper.GetSession(sessDiscountProposalDtl), ArrayList).Count = 0 Then
                sb.Append("- Data Jadwal Pengadaan belum ada\n")
            End If
        End If

        If (sessHelper.GetSession(sessDiscountProposalDtlOwnerShip) Is Nothing) Then
            sb.Append("- Data Kepemilikan Kendaraan belum ada\n")
        Else
            If CType(sessHelper.GetSession(sessDiscountProposalDtlOwnerShip), ArrayList).Count = 0 Then
                sb.Append("- Data Kepemilikan Kendaraan belum ada\n")
            End If
        End If

        If (sessHelper.GetSession(sessDiscountProposalDtlPrice) Is Nothing) Then
            sb.Append("- Data Rincian Harga Kendaraan belum ada\n")
        Else
            If CType(sessHelper.GetSession(sessDiscountProposalDtlPrice), ArrayList).Count = 0 Then
                sb.Append("- Data Rincian Harga Kendaraan belum ada\n")
            End If
        End If

        If (sessHelper.GetSession(sessDiscountProposalDtlCustomer) Is Nothing) Then
            sb.Append("- Data Customer belum ada\n")
        Else
            If CType(sessHelper.GetSession(sessDiscountProposalDtlCustomer), ArrayList).Count = 0 Then
                sb.Append("- Data Customer belum ada\n")
            End If
        End If

        If (sessHelper.GetSession(sessDiscountProposalEmailUser) Is Nothing) Then
            sb.Append("- Data Penerima Email Notifikasi belum ada\n")
        Else
            If CType(sessHelper.GetSession(sessDiscountProposalEmailUser), ArrayList).Count = 0 Then
                sb.Append("- Data Penerima Email Notifikasi belum ada\n")
            End If
        End If

        If (sessHelper.GetSession(sessDiscountProposalDtlDocument) Is Nothing) Then
            sb.Append("- Data Dokumen belum ada\n")
        Else
            'arlDiscountProposalDtlDocument = CType(sessHelper.GetSession(sessDiscountProposalDtlDocument), ArrayList)
            'If arlDiscountProposalDtlDocument.Count > 0 Then
            '    Dim arlDocumentLampiranPOSPK As New ArrayList
            '    For Each objDPDoc As DiscountProposalDetailDocument In arlDiscountProposalDtlDocument
            '        If objDPDoc.FileType = 2 Then   '---ValueCode = LampiranPOSPK
            '            arlDocumentLampiranPOSPK.Add(objDPDoc)
            '        End If
            '    Next
            '    If arlDocumentLampiranPOSPK.Count = 0 Then
            '        sb.Append("- Data Dokumen Lampiran PO/SPK belum ada\n")
            '    End If
            'End If
        End If

        If Not IsLoginAsDealer() Then
            If (sessHelper.GetSession(sessDiscountProposalDtlApproval) Is Nothing) Then
                sb.Append("- Data Proposed Discount belum ada\n")
            Else
                If CType(sessHelper.GetSession(sessDiscountProposalDtlApproval), ArrayList).Count = 0 Then
                    sb.Append("- Data Proposed Discount belum ada\n")
                End If
            End If
        End If

        Return sb.ToString()
    End Function

    Private Sub lnkGenerateExcel_Click(sender As Object, e As EventArgs) Handles lnkGenerateExcel.Click
        Dim strFileName As String = String.Empty
        Dim strDestFile As String = String.Empty
        Dim DataRincianHargaKendaraan As DataTable
        DataRincianHargaKendaraan = sessHelper.GetSession(sessDiscountProposalDtlPricePivotGrid)
        If IsNothing(DataRincianHargaKendaraan) Then DataRincianHargaKendaraan = New DataTable

        Dim dataRincianHargaKendaraanCopy As DataTable = New DataTable()
        dataRincianHargaKendaraanCopy = DataRincianHargaKendaraan.Clone()
        dataRincianHargaKendaraanCopy = DataRincianHargaKendaraan.Copy()

        If dataRincianHargaKendaraanCopy.Rows.Count > 0 Then
            CreateExcelRincianKendaraan(dataRincianHargaKendaraanCopy, strFileName, strDestFile)
        Else
            MessageBox.Show("Data Rincian harga kendaraan tidak ditemukan")
        End If
    End Sub

    Function CreateExcelRincianKendaraan(ByVal Data As DataTable, ByRef strFileName As String, ByRef strDestFile As String) As String
        Dim strMessage As String = ""
        Dim list As ArrayList = New ArrayList
        Dim templateFile As String = Server.MapPath("~\DataFile\Template\DP\Template_excel_rincian_kendaraan.xlsx")
        Dim fileExtention As String = System.IO.Path.GetExtension(templateFile)
        Dim NewFileName As String = "DP_" & objDealer.DealerCode & "_DetilPrice" & Now.Year.ToString & Now.Month.ToString("d2") & Now.Day.ToString("d2") & "_" & Now.Hour.ToString("d2") & Now.Minute.ToString("d2") & Now.Second.ToString("d2") & ".xlsx"
        strFileName = NewFileName
        strDestFile = "DiscountProposal\Fleet\" & objDealer.DealerCode & "\" & NewFileName
        Dim NewFileCopy As String = TargetDirectory & strDestFile

        Try
            If System.IO.File.Exists(templateFile) = True Then
                If Not System.IO.Directory.Exists(NewFileCopy) Then
                    System.IO.Directory.CreateDirectory(Path.GetDirectoryName(NewFileCopy))
                End If
                System.IO.File.Copy(templateFile, NewFileCopy)
                Dim fileInfo As FileInfo = New FileInfo(NewFileCopy)
                Dim excelFile As ExcelPackage = New ExcelPackage(fileInfo)
                Dim ws As ExcelWorksheet = excelFile.Workbook.Worksheets(1)

                Dim arlDiscountProposalParameter As ArrayList = sessHelper.GetSession(sessDiscountProposalParameter)
                If IsNothing(arlDiscountProposalParameter) Then arlDiscountProposalParameter = New ArrayList

                Dim newDataTable As New DataTable
                newDataTable = Data.Clone()
                For i As Integer = 0 To Data.Rows.Count - 1
                    If arlDiscountProposalParameter.Count = 0 Then
                        If i = 15 Then
                            Dim dr2 As DataRow = newDataTable.NewRow
                            For j As Integer = 0 To Data.Columns.Count - 1
                                dr2(j) = ""
                            Next
                            newDataTable.Rows.Add(dr2)
                        End If
                    End If
                    newDataTable.ImportRow(Data.Rows(i))
                Next

                Dim idxRow As Integer = 1
                Dim cellsfrom As String = String.Empty
                Dim cellsto As String = String.Empty
                If Not IsNothing(arlDiscountProposalParameter) AndAlso arlDiscountProposalParameter.Count > 0 Then
                    For i As Integer = 1 To arlDiscountProposalParameter.Count - 1
                        ws.InsertRow(17, 1)
                        cellsfrom = "A" & (i + 17).ToString & ":Z" & (i + 17).ToString
                        cellsto = "A17:Z17"
                        ws.Cells(cellsfrom).Copy(ws.Cells(cellsto))
                        If idxRow > 1 Then
                            ws.Cells(cellsto).Value = ""
                        End If
                        idxRow += 1
                    Next
                End If

                Dim strKeyIndex As String = ""
                Dim strValue As String = ""
                Dim col As Integer, row As Integer
                For col = 1 To newDataTable.Columns.Count - 1
                    For row = 6 To newDataTable.Rows.Count - 1
                        strValue = (newDataTable.Rows(row)(col)).ToString.Replace(".", "").Replace(",", "")
                        newDataTable.Rows(row)(col) = strValue
                    Next
                Next

                For i As Integer = 0 To 1
                    newDataTable.Columns.RemoveAt(0)
                Next

                ws.Cells("C9:Z29").Style.Numberformat.Format = "#,##0"
                ws.Cells("C2").LoadFromDataTable(newDataTable, False)

                idxRow = 0
                If Not IsNothing(arlDiscountProposalParameter) AndAlso arlDiscountProposalParameter.Count > 0 Then
                    For Each cell As ExcelRangeBase In ws.Cells("B:B")
                        If idxRow > 15 And idxRow <= ((arlDiscountProposalParameter.Count - 1) + 16) Then
                            Try
                                Dim objDPParam As DiscountProposalParameter = CType(arlDiscountProposalParameter(idxRow - 16), DiscountProposalParameter)
                                cell.Value = objDPParam.ParameterName
                            Catch
                            End Try
                        End If
                        If idxRow > ((arlDiscountProposalParameter.Count - 1) + 16) Then
                            Exit For
                        End If
                        idxRow += 1
                    Next
                End If

                Dim p As Integer = 0
                Dim strCellFormula As String = String.Empty
                For Each cell As ExcelRangeBase In ws.Cells("C:Z")
                    If p > 6 Then
                        If IsNumeric(cell.Value) Then
                            If Not IsNothing(cell.Formula) Then
                                If cell.Formula = "" Then
                                    If cell.Value.ToString.Trim <> "" Then
                                        Try
                                            cell.Value = Convert.ToDecimal(cell.Value)
                                        Catch
                                        End Try
                                    End If
                                Else
                                    Dim intDPParamCount As Integer = arlDiscountProposalParameter.Count
                                    If arlDiscountProposalParameter.Count = 0 Then intDPParamCount += 1

                                    Dim idxSubTotalProgramReguler As Integer = intDPParamCount + 16 + 1
                                    If InStr(cell.Address, idxSubTotalProgramReguler.ToString()) Then
                                        strCellFormula = Left(cell.Address, 1) & "17:" & Left(cell.Address, 1) & (idxSubTotalProgramReguler - 1)
                                        cell.Formula = "=SUM(" & strCellFormula & ")"
                                    End If
                                End If
                            End If
                        End If
                    End If
                    p += 1
                Next

                'Fit the columns according to its content
                ws.Cells("B1:Z29").AutoFitColumns()
                excelFile.Save()
            End If

        Catch ex As Exception
            strMessage = ex.ToString
        End Try

        Return strMessage
    End Function

    Private Function IsMonthYearValid(ByVal txtValidFrom As String, ByVal txtValidTo As String) As String
        Dim retValue As String = ""
        Try
            Dim dd1 As Date = GetDateFromMonthYear(txtValidFrom.Trim(), 2)
            Dim dd2 As Date = GetDateFromMonthYear(txtValidTo.Trim(), 1)
            If dd1 < dd2 Then
                retValue = "Waktu pengiriman tidak boleh lebih kecil dari Tgl Pengajuan"
            Else
                retValue = ""
            End If

        Catch ex As Exception
            retValue = "Format bulan dan tahun waktu pengiriman salah"
        End Try
        Return retValue
    End Function

    Private Function GetDateFromMonthYear(ByVal MonthYear As String, ByVal type As Integer) As Date
        If MonthYear.Length = 5 Then
            MonthYear = "0" + MonthYear
        End If
        Dim month As Integer = CInt(MonthYear.Substring(0, 2))
        Dim year As Integer = CInt(MonthYear.Substring(2, 4))
        Dim retDate As DateTime
        If type = 1 Then
            retDate = New Date(year, month, 1)
        Else
            retDate = New Date(year, month, DateTime.DaysInMonth(year, month))
        End If
        Return retDate
    End Function

    Sub UploadAttachment(ByVal ObjAttachment As DiscountProposalDetailDocument, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                If Not IsNothing(ObjAttachment.AttachmentData) Then
                    If Not IsNothing(ObjAttachment.Path) Then
                        finfo = New FileInfo(TargetPath + ObjAttachment.Path)
                        If Not finfo.Directory.Exists Then
                            Directory.CreateDirectory(finfo.DirectoryName)
                        End If
                        ObjAttachment.AttachmentData.SaveAs(TargetPath + ObjAttachment.Path)
                    End If
                End If
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function FileIsExist(ByVal FileName As String, ByVal AttachmentCollection As ArrayList) As Boolean
        Dim bResult As Boolean = False
        If AttachmentCollection.Count > 0 Then
            For Each obj As DiscountProposalDetailDocument In AttachmentCollection
                If Not IsNothing(obj.AttachmentData) Then
                    If Path.GetFileName(obj.AttachmentData.FileName) = FileName Then
                        bResult = True
                        Exit For
                    End If
                End If
            Next
        End If
        Return bResult
    End Function

    Function GenerateNumberRow(ByVal Tobj As String, ByRef objArray As ArrayList)
        If Not IsNothing(objArray) AndAlso objArray.Count > 0 Then
            Dim i% = 0
            If Tobj.ToUpper = "DISCOUNTPROPOSALDETAIL" Then
                For Each obj As DiscountProposalDetail In objArray
                    obj.NumberRow = i + 1
                    i += 1
                Next
            End If
            i = 0

            If Tobj.ToUpper = "DISCOUNTPROPOSALDETAILPRICE" Then
                For Each obj As DiscountProposalDetailPrice In objArray
                    obj.NumberRow = i + 1
                    i += 1
                Next
            End If
            i = 0

            If Tobj.ToUpper = "DISCOUNTPROPOSALDETAILDOCUMENT" Then
                For Each obj As DiscountProposalDetailDocument In objArray
                    If obj.FileType = 2 Then
                        obj.NumberRow = i + 1
                        i += 1
                    End If
                Next
            End If
            i = 0

            If Tobj.ToUpper = "DISCOUNTPROPOSALDETAILOWNERSHIP" Then
                For Each obj As DiscountProposalDetailOwnership In objArray
                    obj.NumberRow = i + 1
                    i += 1
                Next
            End If
            i = 0

            If Tobj.ToUpper = "DISCOUNTPROPOSALEMAILUSER" Then
                For Each obj As DiscountProposalEmailUser In objArray
                    obj.NumberRow = i + 1
                    i += 1
                Next
            End If
            i = 0

            If Tobj.ToUpper = "DISCOUNTPROPOSALDETAILAPPROVALTOSPL" Then
                For Each obj As DiscountProposalDetailApprovaltoSPL In objArray
                    obj.NumberRow = i + 1
                    i += 1
                Next
            End If
            If Tobj.ToUpper = "DISCOUNTPROPOSALDETAILAPPROVAL" Then
                For Each obj As DiscountProposalDetailApproval In objArray
                    obj.NumberRow = i + 1
                    i += 1
                Next
            End If
        End If
    End Function

    Private Sub RemoveDiscountProposalDetailDocumentAttachment(ByVal AttachmentCollection As ArrayList, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                For Each obj As DiscountProposalDetailDocument In AttachmentCollection
                    finfo = New FileInfo(TargetPath + obj.Path)
                    If finfo.Exists Then
                        finfo.Delete()
                    End If
                Next
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function CompareStrings(strA As String, strB As String) As Double
        Dim setA As List(Of String) = New List(Of String)
        Dim setB As List(Of String) = New List(Of String)

        For i As Integer = 0 To strA.Length - 1
            If strA.Length - i < 2 Then
                setA.Add(strA.Substring(i, 1))
            Else
                setA.Add(strA.Substring(i, 2))
            End If
        Next
        For i As Integer = 0 To strB.Length - 1
            If strB.Length - i < 2 Then
                setB.Add(strB.Substring(i, 1))
            Else
                setB.Add(strB.Substring(i, 2))
            End If
        Next

        Dim intersection = setA.Intersect(setB, StringComparer.InvariantCultureIgnoreCase)

        Return (2.0 * intersection.Count()) / (setA.Count + setB.Count)
    End Function

    Public Function Levenshtein_distance(ByVal s As String, ByVal t As String) As Integer
        Dim i As Integer ' iterates through s
        Dim j As Integer ' iterates through t
        Dim s_i As String ' ith character of s
        Dim t_j As String ' jth character of t
        Dim cost As Integer ' cost
        ' Step 1
        Dim n As Integer = s.Length
        'length of s
        Dim m As Integer = t.Length
        'length of t
        If n = 0 Then
            Return m
        End If
        If m = 0 Then
            Return n
        End If
        Dim d(0 To n, 0 To m) As Integer
        ' Step 2
        For i = 0 To n
            d(i, 0) = i
        Next i
        For j = 0 To m
            d(0, j) = j
        Next j
        ' Step 3
        For i = 1 To n
            s_i = s.Substring(i - 1, 1)
            ' Step 4
            For j = 1 To m
                t_j = t.Substring(j - 1, 1)
                ' Step 5
                If s_i = t_j Then
                    cost = 0
                Else
                    cost = 1
                End If
                ' Step 6
                d(i, j) = System.Math.Min(System.Math.Min((d((i - 1), j) + 1), (d(i, (j - 1)) + 1)), (d((i - 1), (j - 1)) + cost))
            Next j
        Next i
        ' Step 7
        Return d(n, m)
    End Function

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If SesDealer().Title = EnumDealerTittle.DealerTittle.KTB Then
            If Not SecurityProvider.Authorize(Context.User, SR.DP_PengajuanDP_Lihat_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=DISCOUNT PROPOSAL - Pengajuan Discount Proposal")
            End If
        Else
            ' case from dealer
            If Not SecurityProvider.Authorize(Context.User, SR.DP_PengajuanDP_Lihat_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=DISCOUNT PROPOSAL - Pengajuan Discount Proposal")
            End If
        End If
    End Sub

#End Region

#Region "Event Handlers"
    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        MyBase.OnInit(e)
    End Sub

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        objLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        TargetDirectory = KTB.DNet.Lib.WebConfig.GetValue("SAN")
        TempDirectory = Server.MapPath("") + "\..\DataTemp\"
        sessHelper.SetSession("CtrlIDFocus", Nothing)
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        InitiateAuthorization()
        If Not IsNothing(Request.QueryString("Mode")) Then
            Mode = Request.QueryString("Mode")
            btnKembali.Visible = True
        End If
        If Mode = "New" Then  ' Login as Dealer
            objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        Else
            objDealer = CType(sessHelper.GetSession(sessDiscountProposalDealer), Dealer)
            If IsNothing(objDealer) Then
                objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
            End If
            objDiscountProposalHeader = CType(sessHelper.GetSession(sessDiscountProposalHdr), DiscountProposalHeader)
            If IsNothing(objDiscountProposalHeader) Then
                objDiscountProposalHeader = New DiscountProposalHeader
            End If
        End If
        If Mode = "New" Then
            If trNIB.Visible = True Then
                Dim regExp As RegularExpressionValidator = New RegularExpressionValidator()
                regExp.ID = String.Format("RegularExpressionValidator{0}", 10.ToString())
                regExp.ControlToValidate = txtNoNIB.ID
                regExp.Font.Size = 10
                regExp.ErrorMessage = "Panjang karakter maksimal 13 digit"
                regExp.Display = ValidatorDisplay.None
                regExp.ValidationGroup = "vgDynamicSubmit"
                regExp.ValidationExpression = "^([\S\s]{0,13})$"
                regExp.EnableViewState = True
            End If
        End If

        If (Not IsPostBack) Then
            BindDDLFleetCategoryCustomer()
            BindDDLDealerDirectSales()
            BindDDLBusinessSector()
            BindDDLPurchaseMethod()
            BindDDLAPMSubsidy()
            BindDDLLeasing()
            BindDDLCustomerType()
            BindDDLProjectKindMethod()
            BindDDLPurchaseKind()
            BindDDLPaymentMethod()
            BindDDLBBNAreaProvince()
            BindDDLDeliveryRegionCode()

            FUSuratKomitmentKontrak.Attributes("onchange") = "UploadFile1(this)"
            FUSuratPernyataan.Attributes("onchange") = "UploadFile2(this)"
            FULampiranPOSPK.Attributes("onchange") = "UploadFile3(this)"

            lblPopUpDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            lblPopUpFleetCustomer.Attributes("onclick") = "ShowPPFleetCustSelection();"
            'lnkPopUpNameOnFaktur.Attributes("onclick") = "ShowPanelCustomer();"
            LinkDownloadSuratPernyataan.Attributes("onclick") = "getpath();"
            LinkDownloadSuratKomitmentKontrak.Attributes("onclick") = "getpath();"

            Dim clickHandler As String = String.Format("document.body.style.cursor = 'wait'; this.value='Please wait...'; this.disabled = true; {0};",
               ClientScript.GetPostBackEventReference(lnkReloadHistoryPembelian, String.Empty))
            lnkReloadHistoryPembelian.Attributes.Add("onclick", clickHandler)

            GetDealerData(objDealer)
            ClearAll()
            objDiscountProposalHeader = New DiscountProposalHeader
            sessHelper.SetSession(sessDiscountProposalHdr, objDiscountProposalHeader)

            btnValidasi.Visible = False
            btnHapus.Visible = False
            If Not IsNothing(Request.QueryString("DiscountProposalHeaderID")) Then
                hdnDiscountProposalHeaderID.Value = Request.QueryString("DiscountProposalHeaderID")
                LoadDataDiscountProposal(hdnDiscountProposalHeaderID.Value)
                isInsert = True
            Else
                BindGridKepemilikanKendaraan()
                BindGridDPCustomer()
                BindGridHistoryPembelian()
                BindGridUploadFileLampiranPOSPK()
                BindGridEmailUser()
                BindGridProposedDiscount()
                BindCheckBoxConsideration()
            End If
            BindGridRincianHargaKendaraan()
        Else
            isInsert = True
            BindGridRincianHargaKendaraan()
            If hdnShowDataCustomer.Value = 0 Then
                MainPanel.Attributes("style") = "display:table-row"
                PanelDataCustomer.Attributes("style") = "display:none"
            End If
        End If
    End Sub

    Private Sub btnBaru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBaru.Click
        Dim strJs As String = "window.location = '../DiscountProposal/FrmInputDiscountProposal.aspx'"
        System.Web.UI.ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Script", strJs, True)
    End Sub

    Private Sub btnRetrieveDetailDiscount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRetrieveDetailDiscount.Click
    End Sub

    Public Sub ddlFBrandCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim arrDDL As ArrayList = New ArrayList
        Dim ddlFBrandCategory As DropDownList = sender
        Dim gridItem As DataGridItem = ddlFBrandCategory.Parent.Parent
        Dim txtFBrandNameCategory As System.Web.UI.WebControls.TextBox
        If gridItem.DataSetIndex > -1 Then
            txtFBrandNameCategory = gridItem.FindControl("txtEBrandNameCategory")
        Else
            txtFBrandNameCategory = gridItem.FindControl("txtFBrandNameCategory")
        End If

        txtFBrandNameCategory.Text = ""
        If ddlFBrandCategory.SelectedValue > 0 Then
            txtFBrandNameCategory.Enabled = True
        Else
            txtFBrandNameCategory.Enabled = False
            txtFBrandNameCategory.Text = ""
        End If
    End Sub

    Public Sub ddlFKategoriKendaraan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim arrDDL As ArrayList = New ArrayList
        Dim ddlFKategoriKendaraan As DropDownList = sender
        Dim gridItem As DataGridItem = ddlFKategoriKendaraan.Parent.Parent
        Dim ddlFModelKendaraan As DropDownList
        If gridItem.DataSetIndex > -1 Then
            ddlFModelKendaraan = gridItem.FindControl("ddlEModelKendaraan")
        Else
            ddlFModelKendaraan = gridItem.FindControl("ddlFModelKendaraan")
        End If

        ddlFModelKendaraan.Items.Clear()
        If ddlFKategoriKendaraan.SelectedIndex > 0 Then
            CommonFunction.BindVehicleSubCategoryToDDL2(ddlFModelKendaraan, ddlFKategoriKendaraan.SelectedItem.Text)
        Else
            ddlFModelKendaraan.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        End If
    End Sub

    Function RemoveRowTotalDiscountOnGrid(ByVal arlDiscountProposalDtlApprovalToSPL As ArrayList) As ArrayList
        If IsNothing(arlDiscountProposalDtlApprovalToSPLtoPDF) Then arlDiscountProposalDtlApprovalToSPLtoPDF = New ArrayList
        For i As Integer = 0 To arlDiscountProposalDtlApprovalToSPL.Count - 1
            Dim objDPAtoSPL As DiscountProposalDetailApprovaltoSPL = CType(arlDiscountProposalDtlApprovalToSPL(i), DiscountProposalDetailApprovaltoSPL)
            arlDiscountProposalDtlApprovalToSPLtoPDF.Add(objDPAtoSPL)
            If Not IsNothing(objDPAtoSPL.LabelTotal) Then
                If objDPAtoSPL.LabelTotal.ToLower = "total discount :" Then
                    arlDiscountProposalDtlApprovalToSPL.RemoveAt(i)
                    i -= 1
                End If
            End If
            If i = arlDiscountProposalDtlApprovalToSPL.Count - 1 Then
                Exit For
            End If
        Next
        Return arlDiscountProposalDtlApprovalToSPL
    End Function

    Public Class SkenarioLavenstein
        Private _isSimilarHdrDtl As Boolean
        Private _persenHdrNama As Integer
        Private _persenDtlKTP As Integer
        Private _fleetCustHdrID As Integer
        Private _fleetCustDtlID As Integer

        Public Property IsSimilarHdrDtl() As Boolean
            Get
                Return _isSimilarHdrDtl
            End Get
            Set(ByVal Value As Boolean)
                _isSimilarHdrDtl = Value
            End Set
        End Property

        Public Property PersenHdrNama() As Integer
            Get
                Return _persenHdrNama
            End Get
            Set(ByVal Value As Integer)
                _persenHdrNama = Value
            End Set
        End Property

        Public Property PersenDtlKTP() As Integer
            Get
                Return _persenDtlKTP
            End Get
            Set(ByVal Value As Integer)
                _persenDtlKTP = Value
            End Set
        End Property

        Public Property FleetCustHdrID() As Integer
            Get
                Return _fleetCustHdrID
            End Get
            Set(ByVal Value As Integer)
                _fleetCustHdrID = Value
            End Set
        End Property

        Public Property FleetCustDtlID() As Integer
            Get
                Return _fleetCustDtlID
            End Get
            Set(ByVal Value As Integer)
                _fleetCustDtlID = Value
            End Set
        End Property
    End Class

    Private Sub btnReloadConfirm_Click(sender As Object, e As EventArgs) Handles btnReloadConfirm.Click
        If hdnValNew.Value = "1" OrElse hdnValNew.Value = "0" Then
            If hdnValNew.Value = "1" Then
                hdnFleetCustomerHeaderID.Value = ""
                hdnFleetCustomerDetailID.Value = ""
            End If
            btnSave2_Click(Nothing, Nothing)
        End If
    End Sub

    Function CekLavensteinFleetCustomer(ByRef objFleetCustomerHeaderNew As FleetCustomerHeader, ByRef objFleetCustomerDetailNew As FleetCustomerDetail) As Boolean
        Dim isPopUp As Boolean = False
        Dim strSql As String = String.Empty

        If hdnFleetCustomerHeaderID.Value.Trim = "" Then            '----> pasti dari klik tombol buat Data Fleet Baru
            Dim criterias As New CriteriaComposite(New Criteria(GetType(FleetCustomerHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            strSql = "Select distinct a.ID from FleetCustomerHeader a Left Join FleetCustomerDetail b on a.ID=b.FleetCustomerHeaderID and b.RowStatus=0 "
            strSql += " and b.DealerID = " & objDealer.ID & " Where a.RowStatus=0 "
            criterias.opAnd(New Criteria(GetType(FleetCustomerHeader), "ID", MatchType.InSet, "(" & strSql & ")"))
            Dim arlFleetCustomerHeader As ArrayList = New FleetCustomerHeaderFacade(User).Retrieve(criterias)
            If IsNothing(arlFleetCustomerHeader) Then arlFleetCustomerHeader = New ArrayList

            Dim intPsnMiripName As Integer = 0
            Dim intPsnMiripNIKNIB As Integer = 0
            Dim strNoKTPNIB As String = ""
            If trKTP.Visible = True Then
                strNoKTPNIB = txtNoKTP.Text.Trim
            ElseIf trNIB.Visible = True Then
                strNoKTPNIB = txtNoNIB.Text.Trim
            End If
            If hdnValNew.Value = "-1" Then
                Dim ArlSkenario1 As New ArrayList
                Dim ArlSkenario2 As New ArrayList
                Dim ArlSkenario3 As New ArrayList
                For Each objFCH As FleetCustomerHeader In arlFleetCustomerHeader
                    Dim Skenario1 As New SkenarioLavenstein
                    Dim Skenario2 As New SkenarioLavenstein
                    Dim Skenario3 As New SkenarioLavenstein

                    intPsnMiripName = 100 - (Levenshtein_distance(objFCH.FleetCustomerName, txtFleetCustomerName.Text.Trim) * 10)
                    If intPsnMiripName < 0 Then intPsnMiripName = 0
                    If intPsnMiripName >= 90 Then
                        hdnFleetCustomerHeaderID.Value = objFCH.ID

                        Dim criterias1 As New CriteriaComposite(New Criteria(GetType(FleetCustomerDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias1.opAnd(New Criteria(GetType(FleetCustomerDetail), "Dealer.ID", MatchType.Exact, objDealer.ID))
                        criterias1.opAnd(New Criteria(GetType(FleetCustomerDetail), "FleetCustomerHeader.ID", MatchType.Exact, hdnFleetCustomerHeaderID.Value))
                        Dim arlFleetCustomerDetail As ArrayList = New FleetCustomerDetailFacade(User).Retrieve(criterias1)
                        If IsNothing(arlFleetCustomerDetail) Then arlFleetCustomerDetail = New ArrayList
                        If Not IsNothing(arlFleetCustomerDetail) AndAlso arlFleetCustomerDetail.Count > 0 Then
                            arlFleetCustomerDetail = CommonFunction.SortListControl(arlFleetCustomerDetail, "CreatedTime", Sort.SortDirection.DESC)
                            For Each objFCD As FleetCustomerDetail In arlFleetCustomerDetail
                                intPsnMiripNIKNIB = 100 - (Levenshtein_distance(objFCD.IdentityNumber, strNoKTPNIB) * 10)
                                If intPsnMiripNIKNIB < 0 Then intPsnMiripNIKNIB = 0
                                If intPsnMiripNIKNIB >= 90 Then
                                    Skenario1 = New SkenarioLavenstein
                                    Skenario1.IsSimilarHdrDtl = True
                                    Skenario1.FleetCustHdrID = objFCH.ID
                                    Skenario1.FleetCustDtlID = objFCD.ID
                                    Skenario1.PersenHdrNama = intPsnMiripName
                                    Skenario1.PersenDtlKTP = intPsnMiripNIKNIB
                                    ArlSkenario1.Add(Skenario1)
                                Else
                                    Skenario2 = New SkenarioLavenstein
                                    Skenario2.IsSimilarHdrDtl = False
                                    Skenario2.FleetCustHdrID = objFCH.ID
                                    Skenario2.FleetCustDtlID = objFCD.ID
                                    Skenario2.PersenHdrNama = intPsnMiripName
                                    Skenario2.PersenDtlKTP = intPsnMiripNIKNIB
                                    ArlSkenario2.Add(Skenario2)
                                End If
                            Next
                        Else
                            Skenario2 = New SkenarioLavenstein
                            Skenario2.IsSimilarHdrDtl = False
                            Skenario2.FleetCustHdrID = objFCH.ID
                            Skenario2.FleetCustDtlID = 0
                            Skenario2.PersenHdrNama = intPsnMiripName
                            Skenario2.PersenDtlKTP = 0
                            ArlSkenario2.Add(Skenario2)
                        End If
                        If ArlSkenario1.Count > 0 Then
                            Exit For
                        End If
                    Else
                        Skenario3 = New SkenarioLavenstein
                        Skenario3.IsSimilarHdrDtl = False
                        Skenario3.FleetCustHdrID = objFCH.ID
                        Skenario3.FleetCustDtlID = 0
                        Skenario3.PersenHdrNama = intPsnMiripName
                        Skenario3.PersenDtlKTP = 0
                        ArlSkenario3.Add(Skenario3)
                    End If
                Next
                If ArlSkenario1.Count > 0 Then
                    Dim newArlSkenario1 = From obj As SkenarioLavenstein In ArlSkenario1
                                        Order By obj.PersenDtlKTP Descending, obj.PersenHdrNama Descending
                                        Select obj

                    For Each obj As SkenarioLavenstein In newArlSkenario1
                        If obj.FleetCustHdrID > 0 AndAlso obj.FleetCustDtlID > 0 Then
                            Dim objFCH As FleetCustomerHeader = New FleetCustomerHeaderFacade(User).Retrieve(obj.FleetCustHdrID)
                            Dim objFCD As FleetCustomerDetail = New FleetCustomerDetailFacade(User).Retrieve(obj.FleetCustDtlID)
                            'MessageBox.Confirm("Data Fleet sudah terdaftar di sistem dengan Nama: [" & objFCH.FleetCustomerName & "] dan NIB/NIK : [" & objFCD.IdentityNumber & "].\n" & _
                            '                   "Apakah tetap akan membuat data Fleet baru ?", "hdnValNew")

                            Dim strJs As String = "OpenChild(" & obj.FleetCustHdrID & ", " & objDealer.ID & ");"
                            System.Web.UI.ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Script", strJs, True)
                            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Redirect", "OpenChild(" & obj.FleetCustHdrID & ", " & objDealer.ID & ");", True)
                            'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "popup", "<script type='text/javascript'>showPopUp('../PopUp/PopUpConfirmationDiscountProposal.aspx?fchID=" & obj.FleetCustHdrID & "&dealerID=" & objDealer.ID & "', '',150, 350, ConfirmationFleetCustomer);</script>", True)

                            hdnFleetCustomerHeaderID.Value = obj.FleetCustHdrID
                            hdnFleetCustomerDetailID.Value = obj.FleetCustDtlID
                            isPopUp = True
                            Return isPopUp
                        End If
                    Next
                Else
                    If ArlSkenario2.Count > 0 Then
                        Dim newArlSkenario2 = From obj As SkenarioLavenstein In ArlSkenario2
                                            Order By obj.PersenHdrNama Descending, obj.PersenDtlKTP Descending
                                            Select obj

                        For Each obj As SkenarioLavenstein In newArlSkenario2
                            If obj.FleetCustHdrID > 0 Then
                                Dim objFCH As FleetCustomerHeader = New FleetCustomerHeaderFacade(User).Retrieve(obj.FleetCustHdrID)
                                hdnFleetCustomerHeaderID.Value = obj.FleetCustHdrID
                                objFleetCustomerHeaderNew = New FleetCustomerHeaderFacade(User).Retrieve(obj.FleetCustHdrID)
                                objFleetCustomerDetailNew = New FleetCustomerDetail
                                Exit For
                            End If
                        Next
                    Else
                        If ArlSkenario3.Count > 0 Then
                            objFleetCustomerHeaderNew = New FleetCustomerHeader
                            objFleetCustomerDetailNew = New FleetCustomerDetail
                        End If
                    End If
                End If
            End If

            If IsNothing(objFleetCustomerHeaderNew) Then
                objFleetCustomerHeaderNew = New FleetCustomerHeader
                objFleetCustomerHeaderNew.FleetCode = GenerateFleetCode()
            Else
                If objFleetCustomerHeaderNew.ID <= 0 Then
                    objFleetCustomerHeaderNew.FleetCode = GenerateFleetCode()
                End If
            End If
            With objFleetCustomerHeaderNew
                .FleetCustomerType = ddlCustomerType.SelectedValue
                .FleetCustomerName = txtFleetCustomerName.Text.Trim
                If ddlBusinessSector.SelectedIndex > 0 Then
                    .BusinessSectorDetail = New BusinessSectorDetailFacade(User).Retrieve(CInt(ddlBusinessSector.SelectedValue))
                End If
            End With

            If IsNothing(objFleetCustomerDetailNew) Then
                objFleetCustomerDetailNew = New FleetCustomerDetail
                objFleetCustomerDetailNew.FleetDetailCode = GenerateFleetDetailCode(objFleetCustomerHeaderNew.FleetCode)
            Else
                If objFleetCustomerDetailNew.ID <= 0 Then
                    objFleetCustomerDetailNew.FleetDetailCode = GenerateFleetDetailCode(objFleetCustomerHeaderNew.FleetCode)
                End If
            End If
            With objFleetCustomerDetailNew
                .Dealer = objDealer
                .Address = txtAddressFleetCustomerDtl.Text
                'If ddlCustomerType.SelectedValue = 0 Then
                '    .IdentityType = 1
                '    .IdentityNumber = txtNoKTP.Text.Trim
                'ElseIf ddlCustomerType.SelectedValue = 1 Then
                '    .IdentityType = 2
                '    .IdentityNumber = txtNoNIB.Text.Trim
                'Else
                '    .IdentityType = 3
                '    .IdentityNumber = txtNoNIB.Text.Trim
                'End If

                If ddlCustomerType.SelectedValue = 0 Then
                    .IdentityNumber = txtNoKTP.Text.Trim
                Else
                    .IdentityNumber = txtNoNIB.Text.Trim
                End If
                .IdentityType = ddlCustomerType.SelectedValue
            End With

        Else    '--> Get Data Fleet dari Popup
            objFleetCustomerDetailNew = New FleetCustomerDetailFacade(User).Retrieve(CInt(hdnFleetCustomerDetailID.Value))
            With objFleetCustomerDetailNew
                .Dealer = objDealer
                .Address = txtAddressFleetCustomerDtl.Text
                'If ddlCustomerType.SelectedValue = 0 Then
                '    .IdentityType = 1
                '    .IdentityNumber = txtNoKTP.Text.Trim
                'ElseIf ddlCustomerType.SelectedValue = 1 Then
                '    .IdentityType = 2
                '    .IdentityNumber = txtNoNIB.Text.Trim
                'Else
                '    .IdentityType = 3
                '    .IdentityNumber = txtNoNIB.Text.Trim
                'End If
                If ddlCustomerType.SelectedValue = 0 Then
                    .IdentityNumber = txtNoKTP.Text.Trim
                Else
                    .IdentityNumber = txtNoNIB.Text.Trim
                End If
                .IdentityType = ddlCustomerType.SelectedValue
            End With

            objFleetCustomerHeaderNew = New FleetCustomerHeaderFacade(User).Retrieve(CInt(hdnFleetCustomerHeaderID.Value))
            With objFleetCustomerHeaderNew
                .FleetCustomerType = ddlCustomerType.SelectedValue
                .FleetCustomerName = txtFleetCustomerName.Text.Trim
                If ddlBusinessSector.SelectedIndex > 0 Then
                    .BusinessSectorDetail = New BusinessSectorDetailFacade(User).Retrieve(CInt(ddlBusinessSector.SelectedValue))
                End If
            End With
        End If

        Return isPopUp
    End Function

    Private Function UpdateProgramRegulerRincianKendaraan()
        arlDiscountProposalDtlPrice = sessHelper.GetSession(sessDiscountProposalDtlPrice)
        If IsNothing(arlDiscountProposalDtlPrice) Then arlDiscountProposalDtlPrice = New ArrayList

        arlDiscountProposalDtlPrice = New System.Collections.ArrayList(
                                            (From obj As DiscountProposalDetailPrice In arlDiscountProposalDtlPrice.OfType(Of DiscountProposalDetailPrice)()
                                                Order By obj.NumberRow, obj.DiscountProposalDetail.ID
                                                Select obj).ToList())

        Dim arlDPPricetoParameterLoop As New ArrayList
        For Each objDPDtlPrice As DiscountProposalDetailPrice In arlDiscountProposalDtlPrice
            arlDiscountProposalPricetoParameter = CType(sessHelper.GetSession(sessDiscountProposalPricetoParameter), ArrayList)
            If IsNothing(arlDiscountProposalPricetoParameter) Then arlDiscountProposalPricetoParameter = New ArrayList()

            arlDPPricetoParameterLoop = New System.Collections.ArrayList(
                                        (From obj As DiscountProposalPricetoParameter In arlDiscountProposalPricetoParameter.OfType(Of DiscountProposalPricetoParameter)()
                                            Where obj.NumberRowParent = objDPDtlPrice.NumberRow
                                            Order By obj.NumberRowParent, obj.DiscountProposalParameter.ID
                                            Select obj).ToList())

            Dim arrDelete2 As ArrayList = CType(sessHelper.GetSession(sessDeleteDiscountProposalPricetoParameter), ArrayList)
            If IsNothing(arrDelete2) Then arrDelete2 = New ArrayList
            Dim arlDPParameter As ArrayList = sessHelper.GetSession(sessDiscountProposalParameter)
            If IsNothing(arlDPParameter) Then arlDPParameter = New ArrayList

            Dim arrExistDPPriceToParam As New ArrayList
            If arlDPParameter.Count = 0 Then
                For Each objDPPricetoParameter As DiscountProposalPricetoParameter In arlDPPricetoParameterLoop
                    arrExistDPPriceToParam.Add(objDPPricetoParameter)
                Next
                If arrExistDPPriceToParam.Count > 0 Then
                    For i As Integer = arlDiscountProposalPricetoParameter.Count - 1 To 0 Step -1
                        Dim objDPPricetoParameter As DiscountProposalPricetoParameter = CType(arlDiscountProposalPricetoParameter(i), DiscountProposalPricetoParameter)
                        If objDPPricetoParameter.NumberRowParent = objDPDtlPrice.NumberRow Then
                            For j As Integer = arrExistDPPriceToParam.Count - 1 To 0 Step -1
                                Dim objExistDPPricetoParameter As DiscountProposalPricetoParameter = CType(arrExistDPPriceToParam(j), DiscountProposalPricetoParameter)
                                If objDPPricetoParameter.NumberRowParent = objExistDPPricetoParameter.NumberRowParent Then
                                    If objDPPricetoParameter.DiscountProposalParameter.ID = objExistDPPricetoParameter.DiscountProposalParameter.ID Then
                                        If objDPPricetoParameter.ID = objExistDPPricetoParameter.ID Then
                                            If objDPPricetoParameter.ID > 0 Then
                                                arrDelete2.Add(objDPPricetoParameter)
                                            End If
                                            arlDiscountProposalPricetoParameter.Remove(objDPPricetoParameter)
                                            Exit For
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    Next
                    sessHelper.SetSession(sessDeleteDiscountProposalPricetoParameter, arrDelete2)
                End If
            Else
                For Each objDPParameter As DiscountProposalParameter In arlDPParameter
                    Dim isExistRow As Boolean = False
                    For Each objDPPricetoParameter As DiscountProposalPricetoParameter In arlDPPricetoParameterLoop
                        If objDPPricetoParameter.DiscountProposalParameter.ID = objDPParameter.ID Then
                            isExistRow = True
                            objDPPricetoParameter.Amount = GetDiscountFromProgramReguler(objDPParameter.ParameterName, objDPDtlPrice.VechileTypeID, objDPDtlPrice.AssyYear, objDPDtlPrice.ModelYear)
                            arrExistDPPriceToParam.Add(objDPPricetoParameter)
                            Exit For
                        End If
                    Next
                    If isExistRow = False Then
                        Dim objDiscountProposalPricetoParameter As DiscountProposalPricetoParameter = New DiscountProposalPricetoParameter
                        With objDiscountProposalPricetoParameter
                            .NumberRowParent = objDPDtlPrice.NumberRow
                            .DiscountProposalDetailPrice = objDPDtlPrice
                            .DiscountProposalParameter = objDPParameter
                            .Amount = GetDiscountFromProgramReguler(objDPParameter.ParameterName, objDPDtlPrice.VechileTypeID, objDPDtlPrice.AssyYear, objDPDtlPrice.ModelYear)
                        End With
                        arrExistDPPriceToParam.Add(objDiscountProposalPricetoParameter)
                        arlDiscountProposalPricetoParameter.Add(objDiscountProposalPricetoParameter)
                    End If
                Next
                If arrExistDPPriceToParam.Count > 0 Then
                    For i As Integer = arlDiscountProposalPricetoParameter.Count - 1 To 0 Step -1
                        Dim objDPPricetoParameter As DiscountProposalPricetoParameter = CType(arlDiscountProposalPricetoParameter(i), DiscountProposalPricetoParameter)
                        If objDPPricetoParameter.NumberRowParent = objDPDtlPrice.NumberRow Then
                            Dim isExistRow As Boolean = False
                            For j As Integer = arrExistDPPriceToParam.Count - 1 To 0 Step -1
                                Dim objExistDPPricetoParameter As DiscountProposalPricetoParameter = CType(arrExistDPPriceToParam(j), DiscountProposalPricetoParameter)
                                If objDPPricetoParameter.NumberRowParent = objExistDPPricetoParameter.NumberRowParent Then
                                    If objDPPricetoParameter.DiscountProposalParameter.ID = objExistDPPricetoParameter.DiscountProposalParameter.ID Then
                                        If objDPPricetoParameter.ID = objExistDPPricetoParameter.ID Then
                                            isExistRow = True
                                            Exit For
                                        End If
                                    End If
                                End If
                            Next
                            If isExistRow = False Then
                                If objDPPricetoParameter.ID > 0 Then
                                    arrDelete2.Add(objDPPricetoParameter)
                                End If
                                arlDiscountProposalPricetoParameter.Remove(objDPPricetoParameter)
                            End If
                        End If
                    Next
                    sessHelper.SetSession(sessDeleteDiscountProposalPricetoParameter, arrDelete2)
                End If
            End If
            sessHelper.SetSession(sessDiscountProposalPricetoParameter, arlDiscountProposalPricetoParameter)
        Next
    End Function

    Private Sub btnSave2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave2.Click
        Dim str As String = ""
        If hdnValNew.Value = "-1" Then
            str = ValidateSaveData()
            If (str.Length > 0) Then
                MessageBox.Show(str)
                Exit Sub
            End If
        End If

        arlDiscountProposalDtl = CType(sessHelper.GetSession(sessDiscountProposalDtl), ArrayList)
        arlDiscountProposalDtlPrice = CType(sessHelper.GetSession(sessDiscountProposalDtlPrice), ArrayList)
        arlDiscountProposalDtlOwnerShip = CType(sessHelper.GetSession(sessDiscountProposalDtlOwnerShip), ArrayList)
        arlDiscountProposalDtlDocument = CType(sessHelper.GetSession(sessDiscountProposalDtlDocument), ArrayList)
        arlDiscountProposalDtlCustomer = CType(sessHelper.GetSession(sessDiscountProposalDtlCustomer), ArrayList)
        arlDiscountProposalEmailUser = CType(sessHelper.GetSession(sessDiscountProposalEmailUser), ArrayList)
        arlDiscountProposalDtlApproval = CType(sessHelper.GetSession(sessDiscountProposalDtlApproval), ArrayList)
        arlDiscountProposalDtlApprovalToSPL = CType(sessHelper.GetSession(sessDiscountProposalDtlApprovalToSPL), ArrayList)
        arlFleetCustomerDetailMappingNew = CType(sessHelper.GetSession(sessHistoryPembelianNew), ArrayList)
        arlFleetCustomerDetailMappingOld = CType(sessHelper.GetSession(sessHistoryPembelianOld), ArrayList)
        arlDiscountProposalPricetoParameter = CType(sessHelper.GetSession(sessDiscountProposalPricetoParameter), ArrayList)

        'Remove row Total Discount grid Proposed Diskon
        arlDiscountProposalDtlApprovalToSPL = RemoveRowTotalDiscountOnGrid(arlDiscountProposalDtlApprovalToSPL)

        Dim objFleetCustomerHeaderNew As FleetCustomerHeader
        Dim objFleetCustomerDetailNew As FleetCustomerDetail
        If CekLavensteinFleetCustomer(objFleetCustomerHeaderNew, objFleetCustomerDetailNew) Then
            Exit Sub
        End If

        Dim _objDiscountProposalHeader As New DiscountProposalHeader
        If Mode = "Edit" Then
            hdnDiscountProposalHeaderID.Value = Request.QueryString("DiscountProposalHeaderID")
            _objDiscountProposalHeader = New DiscountProposalHeaderFacade(User).Retrieve(CInt(hdnDiscountProposalHeaderID.Value))
            _objDiscountProposalHeader.Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text)
        Else
            _objDiscountProposalHeader.ProposalRegNo = GenerateRegNumber()
            _objDiscountProposalHeader.Dealer = objDealer
            _objDiscountProposalHeader.Status = 0  '--- Status Baru
            lblSubmitDate.Text = Date.Now
        End If

        With _objDiscountProposalHeader
            .CustomerType = ddlCustomerType.SelectedValue
            .FleetCustomerDetail = objFleetCustomerDetailNew
            .DealerProposalNo = txtDealerProposalNo.Text
            .BBNAreaProvince = New ProvinceFacade(User).Retrieve(CInt(ddlBBNAreaProvince.SelectedValue))
            .CustomerType = ddlCustomerType.SelectedValue
            .FleetCategory = ddlFleetCategory.SelectedValue
            .ProjectName = txtProjectName.Text
            .LastPurchaseDate = icLastPurchaseDate.Value
            .IsDealerDirectSales = ddlDealerDirectSales.SelectedValue
            .ContractorName = txtContractorName.Text
            .PurchaseMethod = ddlPurchaseMethod.SelectedValue
            .LeasingCompany = New LeasingCompanyFacade(User).Retrieve(CShort(ddlLeasing.SelectedValue))
            .IsAPMSubsidy = ddlAPMSubsidy.SelectedValue
            .PaymentMethod = ddlPaymentMethod.SelectedValue
            .PurchaseKind = ddlPurchaseKind.SelectedValue
            .ProjectKindMethod = ddlProjectKindMethod.SelectedValue
            .ProjectKindMethodOther = txtProjectKindMethodOther.Text
            If txtDeliveryPlanDate.Text.Trim <> "" Then
                .DeliveryPlanDate = GetDateFromMonthYear(txtDeliveryPlanDate.Text.Trim, 1)
            End If
            .DealerNotes = txtDealerNotes.Text
            .SubmitDate = CDate(lblSubmitDate.Text)
            .BusinessSectorDetailID = ddlBusinessSector.SelectedValue
            .DeliveryRegionCode = ddlDeliveryRegionCode.SelectedValue

            If Not IsLoginAsDealer() Then
                Dim strConsideration As String = String.Empty
                For Each cb As ListItem In CBConsideration.Items
                    If cb.Selected Then
                        If strConsideration = "" Then
                            strConsideration = cb.Value.ToString
                        Else
                            strConsideration += ";" & cb.Value.ToString
                        End If
                    End If
                Next
                .Consideration = strConsideration
                .MMKSINotes = txtMMKSINotes.Text
            End If
            .FinalApproval = If(chkFinalApproval.Checked = True, 1, 0)
        End With

        Dim _result As Integer = 0
        If IsNothing(arlDiscountProposalDtl) Then arlDiscountProposalDtl = New ArrayList
        If IsNothing(arlDiscountProposalDtlPrice) Then arlDiscountProposalDtlPrice = New ArrayList
        If IsNothing(arlDiscountProposalDtlOwnerShip) Then arlDiscountProposalDtlOwnerShip = New ArrayList
        If IsNothing(arlDiscountProposalDtlDocument) Then arlDiscountProposalDtlDocument = New ArrayList
        If IsNothing(arlDiscountProposalDtlCustomer) Then arlDiscountProposalDtlCustomer = New ArrayList
        If IsNothing(arlDiscountProposalEmailUser) Then arlDiscountProposalEmailUser = New ArrayList
        If IsNothing(arlDiscountProposalDtlApproval) Then arlDiscountProposalDtlApproval = New ArrayList
        If IsNothing(arlDiscountProposalDtlApprovalToSPL) Then arlDiscountProposalDtlApprovalToSPL = New ArrayList
        If IsNothing(arlFleetCustomerDetailMappingNew) Then arlFleetCustomerDetailMappingNew = New ArrayList
        If IsNothing(arlFleetCustomerDetailMappingOld) Then arlFleetCustomerDetailMappingOld = New ArrayList

        If Mode = "Edit" Then
            If IsLoginAsDealer() Then
                arlFleetCustomerDetailMappingNew = Nothing
                arlFleetCustomerDetailMappingOld = Nothing
            End If
            arlDelDiscountProposalDtl = CType(sessHelper.GetSession(sessDeleteDiscountProposalDtl), ArrayList)
            arlDelDiscountProposalDtlPrice = CType(sessHelper.GetSession(sessDeleteDiscountProposalDtlPrice), ArrayList)
            arlDelDiscountProposalDtlOwnerShip = CType(sessHelper.GetSession(sessDeleteDiscountProposalDtlOwnerShip), ArrayList)
            arlDelDiscountProposalDtlDocument = CType(sessHelper.GetSession(sessDeleteDiscountProposalDtlDocument), ArrayList)
            arlDelDiscountProposalDtlCustomer = CType(sessHelper.GetSession(sessDeleteDiscountProposalDtlCustomer), ArrayList)
            arlDelDiscountProposalEmailUser = CType(sessHelper.GetSession(sessDeleteDiscountProposalEmailUser), ArrayList)
            arlDelDiscountProposalDtlApproval = CType(sessHelper.GetSession(sessDeleteDiscountProposalDtlApproval), ArrayList)
            arlDelDiscountProposalDtlApprovalToSPL = CType(sessHelper.GetSession(sessDeleteDiscountProposalDtlApprovalToSPL), ArrayList)
            arlDelDiscountProposalPricetoParameter = CType(sessHelper.GetSession(sessDeleteDiscountProposalPricetoParameter), ArrayList)

            If _objDiscountProposalHeader.Status = 0 Then  '--selama statusnya masih Baru
                UpdateProgramRegulerRincianKendaraan()
            End If

            _result = New DiscountProposalHeaderFacade(User).UpdateTransaction(_objDiscountProposalHeader, objFleetCustomerDetailNew, objFleetCustomerHeaderNew, _
                 arlDiscountProposalDtl, arlDelDiscountProposalDtl, arlDiscountProposalDtlOwnerShip, arlDelDiscountProposalDtlOwnerShip, arlDiscountProposalDtlDocument, _
                 arlDelDiscountProposalDtlDocument, arlDiscountProposalDtlCustomer, arlDelDiscountProposalDtlCustomer, arlDiscountProposalEmailUser, _
                 arlDelDiscountProposalEmailUser, arlDiscountProposalDtlApproval, arlDelDiscountProposalDtlApproval, arlDiscountProposalDtlPrice, arlDelDiscountProposalDtlPrice, _
                 arlDiscountProposalDtlApprovalToSPL, arlDelDiscountProposalDtlApprovalToSPL, arlDiscountProposalPricetoParameter, arlDelDiscountProposalPricetoParameter)

            If _result > 0 Then
                _result = New FleetCustomerDetailMappingFacade(User).UpdateTransaction(objFleetCustomerHeaderNew, arlFleetCustomerDetailMappingNew, arlFleetCustomerDetailMappingOld)
                If _result <> -1 Then
                    _result = _objDiscountProposalHeader.ID
                End If
            End If
        Else
            _result = New DiscountProposalHeaderFacade(User).InsertTransaction(_objDiscountProposalHeader, objFleetCustomerDetailNew, objFleetCustomerHeaderNew, _
                      arlDiscountProposalDtl, arlDiscountProposalDtlOwnerShip, arlDiscountProposalDtlDocument, arlDiscountProposalDtlCustomer, _
                      arlDiscountProposalEmailUser, arlDiscountProposalDtlApproval, arlDiscountProposalDtlPrice, arlDiscountProposalDtlApprovalToSPL, arlDiscountProposalPricetoParameter)
        End If

        Dim strJs As String = String.Empty
        If _result > 0 Then
            hdnDiscountProposalHeaderID.Value = _result
            CommitAttachment(arlDiscountProposalDtlDocument)
            If Request.QueryString("Mode") = "Edit" Then
                If Not IsNothing(arlDelDiscountProposalDtlDocument) Then
                    'RemoveDiscountProposalDetailDocumentAttachment(arlDelDiscountProposalDtlDocument, TargetDirectory)
                End If
            End If

            '---Proses Generate File Excel dan PDF ------
            Dim strMessageGeneratePDF As String = ""
            If hdnDiscountProposalHeaderID.Value.Trim <> "" Then
                Dim strFileName As String, strDestFile As String
                Dim objDiscountProposalDtlDocument As DiscountProposalDetailDocument
                arlDiscountProposalDtlDocument = New ArrayList

                '1.----------
                strFileName = ""
                strDestFile = ""
                Dim isSuccessGenerateExcel As Boolean = GenerateExcelFileRincianKendaraan(hdnDiscountProposalHeaderID.Value, strFileName, strDestFile)
                If Not isSuccessGenerateExcel Then
                    strMessageGeneratePDF = "- [Gagal Generate File Excel Rincian Kendaraan]"
                Else
                    objDiscountProposalDtlDocument = New DiscountProposalDetailDocument
                    With objDiscountProposalDtlDocument
                        .DiscountProposalHeader = _objDiscountProposalHeader
                        .FileType = 4  '---> Generate File Excel to Groupware
                        .FileName = strFileName
                        .Path = strDestFile
                    End With
                    arlDiscountProposalDtlDocument.Add(objDiscountProposalDtlDocument)
                End If

                '2..----------
                strFileName = ""
                strDestFile = ""
                Dim strResult As String = GeneratePDFtoGroupware(hdnDiscountProposalHeaderID.Value, strFileName, strDestFile)
                If strResult <> "" Then
                    strMessageGeneratePDF += "\n- [Gagal Generate File PDF to Groupware]"
                    strMessageGeneratePDF += "\n- " & strResult.Replace("'", "")
                Else
                    objDiscountProposalDtlDocument = New DiscountProposalDetailDocument
                    With objDiscountProposalDtlDocument
                        .DiscountProposalHeader = _objDiscountProposalHeader
                        .FileType = 3  '---> Generate File PDF to Groupware
                        .FileName = strFileName
                        .Path = strDestFile
                    End With
                    arlDiscountProposalDtlDocument.Add(objDiscountProposalDtlDocument)
                End If

                _result = New DiscountProposalHeaderFacade(User).InsertTransactionGenerateFiletoGW(_objDiscountProposalHeader, arlDiscountProposalDtlDocument)
            End If

            ClearAll()
            strJs = "alert('Simpan Data Berhasil " & strMessageGeneratePDF & "');"
            strJs += "window.location = '../DiscountProposal/FrmDaftarDiscountProposal.aspx'"
        Else
            strJs = "alert('Simpan Data Gagal');"
        End If
        System.Web.UI.ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Script", strJs, True)
    End Sub

    Private Sub dgUploadFile_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgUploadFile.ItemCommand
        Dim _arrDataUploadFile As ArrayList = CType(sessHelper.GetSession(sessDiscountProposalDtlDocument), ArrayList)
        If IsNothing(_arrDataUploadFile) Then _arrDataUploadFile = New ArrayList
        GenerateNumberRow("DiscountProposalDetailDocument", _arrDataUploadFile)
        Dim _arrDataUploadFileFileType2 As ArrayList = New System.Collections.ArrayList(
                        (From obj As DiscountProposalDetailDocument In _arrDataUploadFile.OfType(Of DiscountProposalDetailDocument)()
                            Where obj.FileType = 2
                            Select obj).ToList())

        Select Case e.CommandName
            Case "add" 'Insert New item to datagrid
                Dim FileUpload As HtmlInputFile = CType(e.Item.FindControl("UploadFile"), HtmlInputFile)
                Dim objPostedData As HttpPostedFile
                Dim objDiscountProposalDetailDocument As DiscountProposalDetailDocument = New DiscountProposalDetailDocument()
                Dim sFileName As String

                '========= Validasi  =======================================================================
                If IsNothing(FileUpload) OrElse FileUpload.Value = String.Empty Then
                    MessageBox.Show("Lampiran masih kosong")
                    Return
                End If
                Dim _filename As String = System.IO.Path.GetFileName(FileUpload.PostedFile.FileName)
                If _filename.Trim().Length <= 0 Then
                    MessageBox.Show("Upload file belum diisi\n")
                    Return
                End If
                If _filename.Trim().Length > 0 Then
                    If FileUpload.PostedFile.ContentLength > MAX_FILE_SIZE Then
                        MessageBox.Show("Ukuran file tidak boleh melebihi " & (MAX_FILE_SIZE / 1024) & "kb\n")
                        Return
                    End If
                End If
                Dim ext As String = System.IO.Path.GetExtension(FileUpload.PostedFile.FileName)
                If Not (ext.ToUpper() = ".PDF" OrElse
                        ext.ToUpper() = ".DOC" OrElse
                        ext.ToUpper() = ".DOCX" OrElse
                        ext.ToUpper() = ".PNG" OrElse
                        ext.ToUpper() = ".JPG" OrElse
                        ext.ToUpper() = ".JPEG" OrElse
                        ext.ToUpper() = ".XLS" OrElse
                        ext.ToUpper() = ".XLSX") Then
                    MessageBox.Show("Hanya menerima file format (PNG/JPG/JPEG/PDF/XLS/DOC)")
                    Return
                End If

                If Not IsNothing(FileUpload) OrElse FileUpload.Value <> String.Empty Then
                    objPostedData = FileUpload.PostedFile
                Else
                    objPostedData = Nothing
                End If

                If Not (IsNothing(objPostedData)) Then
                    sFileName = objPostedData.FileName.Split("\")(objPostedData.FileName.Split("\").Length - 1)

                    If KTB.DNet.UI.Helper.FileHelper.IsExecutableFile(sFileName) Then
                        MessageBox.Show("Tidak diperkenankan mengupload file dengan ekstensi '.exe'. Pastikan file anda bebas dari virus.")
                        BindGridUploadFileLampiranPOSPK()
                        Return
                    End If

                    Dim strDealerCode As String = String.Empty
                    If IsLoginAsDealer() Then
                        strDealerCode = lblDealerCodeName.Text
                    Else
                        strDealerCode = txtDealerCode.Text
                    End If

                    If Not FileIsExist(sFileName, _arrDataUploadFile) Then
                        Dim SrcFile As String = Path.GetFileName(objPostedData.FileName) '-- Source file name
                        Dim strDPPathConfig As String = "DiscountProposal\Fleet\"
                        Dim strDPPathFile As String = objDealer.DealerCode & "\" & TimeStamp() & SrcFile.Substring(SrcFile.Length - 4)
                        Dim strDestFile As String = strDPPathConfig & strDPPathFile '--Destination File                       

                        With objDiscountProposalDetailDocument
                            .DiscountProposalHeader = New DiscountProposalHeader()
                            .FileType = 2   '---ValueCode = LampiranPOSPK
                            .AttachmentData = objPostedData
                            .FileName = sFileName
                            .Path = strDestFile
                        End With

                        UploadAttachment(objDiscountProposalDetailDocument, TempDirectory)

                        _arrDataUploadFile.Add(objDiscountProposalDetailDocument)
                        GenerateNumberRow("DiscountProposalDetailDocument", _arrDataUploadFile)
                        sessHelper.SetSession(sessDiscountProposalDtlDocument, _arrDataUploadFile)
                    Else
                        MessageBox.Show(SR.DataIsExist("Attachment File"))
                    End If
                Else
                    MessageBox.Show(SR.DataNotFound("Attachment File"))
                End If

            Case "Delete" 'Delete this datagrid item 
                Dim oDiscountProposalDetailDocument As DiscountProposalDetailDocument = CType(_arrDataUploadFileFileType2(e.Item.ItemIndex), DiscountProposalDetailDocument)
                If oDiscountProposalDetailDocument.ID > 0 Then
                    Dim arrDelete As ArrayList = CType(sessHelper.GetSession(sessDeleteDiscountProposalDtlDocument), ArrayList)
                    If IsNothing(arrDelete) Then arrDelete = New ArrayList
                    arrDelete.Add(oDiscountProposalDetailDocument)
                    sessHelper.SetSession(sessDeleteDiscountProposalDtlDocument, arrDelete)
                End If

                'RemoveDiscountProposalDetailDocumentAttachment(CType(_arrDataUploadFileFileType2(e.Item.ItemIndex), DiscountProposalDetailDocument), TempDirectory)
                Dim i% = 0
                For Each obj As DiscountProposalDetailDocument In _arrDataUploadFile
                    If obj.FileType = 2 Then
                        If oDiscountProposalDetailDocument.NumberRow = obj.NumberRow Then
                            _arrDataUploadFile.RemoveAt(i)
                            Exit For
                        End If
                    End If
                    i += 1
                Next
                GenerateNumberRow("DiscountProposalDetailDocument", _arrDataUploadFile)
                sessHelper.SetSession(sessDiscountProposalDtlDocument, _arrDataUploadFile)

            Case "Download" 'Download File
                Response.Redirect("../Download.aspx?file=" & e.CommandArgument)

        End Select

        BindGridUploadFileLampiranPOSPK()
    End Sub

    Private Sub dgUploadFile_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgUploadFile.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dgUploadFile.CurrentPageIndex * dgUploadFile.PageSize)
            Dim arrUpload As ArrayList = CType(sessHelper.GetSession(sessDiscountProposalDtlDocument), ArrayList)
            arrUpload = New System.Collections.ArrayList(
                            (From obj As DiscountProposalDetailDocument In arrUpload.OfType(Of DiscountProposalDetailDocument)()
                                Where obj.FileType = 2
                                Select obj).ToList())

            If Not IsNothing(arrUpload) AndAlso arrUpload.Count > 0 Then
                Dim objDiscountProposalDtlDocument As DiscountProposalDetailDocument = arrUpload(e.Item.ItemIndex)
                Dim lblFileName As Label = CType(e.Item.FindControl("lblFileName"), Label)
                lblFileName.Text = Path.GetFileName(objDiscountProposalDtlDocument.FileName)
            End If
        End If
    End Sub

    Private Sub btnGetInfoDealer_Click(sender As Object, e As EventArgs) Handles btnGetInfoDealer.Click
        Dim oDealer As Dealer = New KTB.DNet.BusinessFacade.General.DealerFacade(User).Retrieve(txtDealerCode.Text.Trim())
        lblDealerCodeName.Text = oDealer.DealerCode & " / " & oDealer.DealerName
    End Sub


#Region "HeaderGridhandler"
    Private Sub dgKepemilikanKendaraan_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgKepemilikanKendaraan.ItemCreated
        Dim gvr As DataGridItem = e.Item
        If (gvr.ItemType = ListItemType.Header) Then
            If (dgKepemilikanKendaraan.Controls.Count < 0) Then
                Return
            End If

            e.Item.Cells.RemoveAt(2)
            e.Item.Cells(2).ColumnSpan = 2

            e.Item.Cells(1).Text = "<table style='FONT-WEIGHT: bold;" & _
              " WIDTH: 100%; COLOR: white; TEXT-ALIGN: center'><tr" & _
              " align =center><td colspan = 2 style='BORDER-BOTTOM:" & _
              " cccccc 1pt solid'>Name</td></tr>" & _
              "<tr align =center ><td style ='BORDER-RIGHT:" & _
              " cccccc 1pt solid'>F Name</td><td>L" & _
              " Name</td></tr></table>"
        End If
    End Sub

    Public ReadOnly Property info() As MergedColumnsInfo
        Get
            If (ViewState("info") Is Nothing) Then

                ViewState("info") = New MergedColumnsInfo
            End If
            Return CType(ViewState("info"), MergedColumnsInfo)
        End Get
    End Property


    <Serializable()> _
    Public Class MergedColumnsInfo
        Public MergedColumns As New ArrayList
        Public StartColumns As New Hashtable
        '            // key-value pairs: key = first column index, value = common title of merged columns 
        Public Titles As New Hashtable
        '            //parameters: merged columns's indexes, common title of merged columns 

        Public Sub AddMergedColumns(ByVal columnsIndexes As Integer(), ByVal title As String)
            If Not StartColumns.ContainsKey(columnsIndexes(0)) Then
                MergedColumns.AddRange(columnsIndexes)
                StartColumns.Add(columnsIndexes(0), columnsIndexes.Length)
                Titles.Add(columnsIndexes(0), title)
            End If
        End Sub
    End Class

    Public Sub RenderHeader(ByVal output As HtmlTextWriter, ByVal container As Control)
        For i As Integer = 0 To container.Controls.Count - 1
            Dim cell As TableCell = CType(container.Controls(i), TableCell)
            If (Not info.MergedColumns.Contains(i)) Then
                cell.Attributes("colspan") = "2"
                cell.RenderControl(output)
            Else
                If (info.StartColumns.Contains(i)) Then
                    output.Write(String.Format("<th align='center' Class='titleTableSales' colspan='{0}'>{1}</th>", info.StartColumns(i), info.Titles(i)))
                End If
            End If
        Next
        output.RenderEndTag()

        dgKepemilikanKendaraan.HeaderStyle.AddAttributesToRender(output)
        output.RenderBeginTag("tr")

        For i As Integer = 0 To info.MergedColumns.Count - 1
            Dim cell As TableCell = CType(container.Controls(info.MergedColumns(i)), TableCell)
            cell.RenderControl(output)
        Next
    End Sub
#End Region


    Protected Sub dgKepemilikanKendaraan_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgKepemilikanKendaraan.ItemDataBound
        Dim ddlFBrandCategory As DropDownList
        Dim ddlEBrandCategory As DropDownList
        Dim txtFBrandNameCategory As System.Web.UI.WebControls.TextBox
        Dim txtEBrandNameCategory As System.Web.UI.WebControls.TextBox
        Dim txtFModel As System.Web.UI.WebControls.TextBox
        Dim txtEModel As System.Web.UI.WebControls.TextBox
        Dim txtFQtyUnit As System.Web.UI.WebControls.TextBox
        Dim txtEQtyUnit As System.Web.UI.WebControls.TextBox

        Dim lblBrandCategory As Label
        Dim lblBrandNameCategory As Label
        Dim lblModel As Label
        Dim lblQtyUnit As Label
        Dim lbtnEdit As LinkButton
        Dim lbtnDelete As LinkButton

        If e.Item.ItemType = ListItemType.Footer Then
            ddlFBrandCategory = CType(e.Item.FindControl("ddlFBrandCategory"), DropDownList)
            With ddlFBrandCategory
                .Items.Clear()
                .DataSource = New StandardCodeFacade(User).RetrieveByCategory("EnumDiscountProposal.BrandCategory")
                .DataTextField = "ValueDesc"
                .DataValueField = "ValueId"
                .DataBind()
                .Items.Insert(0, New ListItem("Silakan Pilih ", -1))
                .SelectedIndex = 0
            End With
        End If

        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            Dim oDPO As DiscountProposalDetailOwnership = CType(e.Item.DataItem, DiscountProposalDetailOwnership)
            If e.Item.ItemIndex = 0 Then
                ViewState("ItemIndex") = e.Item.ItemIndex
                intItemIndex = CType(ViewState("ItemIndex"), Integer)
            Else
                ViewState("ItemIndex") = CType(ViewState("ItemIndex"), Integer) + 1
                intItemIndex = CType(ViewState("ItemIndex"), Integer)
            End If
            '---Create Number Row
            e.Item.Cells(0).Text = intItemIndex + 1 + (dgKepemilikanKendaraan.CurrentPageIndex * dgKepemilikanKendaraan.PageSize)

            ddlEBrandCategory = CType(e.Item.FindControl("ddlEBrandCategory"), DropDownList)
            With ddlEBrandCategory
                .Items.Clear()
                .DataSource = New StandardCodeFacade(User).RetrieveByCategory("EnumDiscountProposal.BrandCategory")
                .DataTextField = "ValueDesc"
                .DataValueField = "ValueId"
                .DataBind()
                .Items.Insert(0, New ListItem("Silakan Pilih ", -1))
                .SelectedValue = oDPO.VehicleBrandCategory
            End With
            ddlFBrandCategory_SelectedIndexChanged(ddlEBrandCategory, Nothing)

            txtEBrandNameCategory = CType(e.Item.FindControl("txtEBrandNameCategory"), System.Web.UI.WebControls.TextBox)
            txtEBrandNameCategory.Text = oDPO.VehicleBrandName
            txtEModel = CType(e.Item.FindControl("txtEModel"), System.Web.UI.WebControls.TextBox)
            txtEModel.Text = oDPO.VehicleModel
            txtEQtyUnit = CType(e.Item.FindControl("txtEQtyUnit"), System.Web.UI.WebControls.TextBox)
            txtEQtyUnit.Text = oDPO.Quantity
        End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oBDC As DiscountProposalDetailOwnership = CType(e.Item.DataItem, DiscountProposalDetailOwnership)
            If e.Item.ItemIndex = 0 Then
                ViewState("ItemIndex") = e.Item.ItemIndex
                intItemIndex = CType(ViewState("ItemIndex"), Integer)
            Else
                ViewState("ItemIndex") = CType(ViewState("ItemIndex"), Integer) + 1
                intItemIndex = CType(ViewState("ItemIndex"), Integer)
            End If
            e.Item.Cells(0).Text = intItemIndex + 1 + (dgKepemilikanKendaraan.CurrentPageIndex * dgKepemilikanKendaraan.PageSize)

            lblBrandCategory = CType(e.Item.FindControl("lblBrandCategory"), Label)
            lblBrandNameCategory = CType(e.Item.FindControl("lblBrandNameCategory"), Label)
            lblModel = CType(e.Item.FindControl("lblModel"), Label)
            lblQtyUnit = CType(e.Item.FindControl("lblQtyUnit"), Label)
            lbtnEdit = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            lbtnDelete = CType(e.Item.FindControl("lbtnDelete"), LinkButton)

            lblBrandCategory.Text = CommonFunction.GetEnumDescription(oBDC.VehicleBrandCategory, "EnumDiscountProposal.BrandCategory")
            lblBrandNameCategory.Text = oBDC.VehicleBrandName
            lblModel.Text = oBDC.VehicleModel
            lblQtyUnit.Text = oBDC.Quantity
            lbtnEdit.Attributes("style") = "display:table-row"
            lbtnDelete.Attributes("style") = "display:table-row"
        End If
    End Sub

    Protected Sub dgKepemilikanKendaraan_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgKepemilikanKendaraan.ItemCommand
        Dim ddlFBrandCategory As DropDownList
        Dim ddlEBrandCategory As DropDownList
        Dim txtFBrandNameCategory As System.Web.UI.WebControls.TextBox
        Dim txtEBrandNameCategory As System.Web.UI.WebControls.TextBox
        Dim txtFModel As System.Web.UI.WebControls.TextBox
        Dim txtEModel As System.Web.UI.WebControls.TextBox
        Dim txtFQtyUnit As System.Web.UI.WebControls.TextBox
        Dim txtEQtyUnit As System.Web.UI.WebControls.TextBox
        Dim oCategory As New Category

        arlDiscountProposalDtlOwnerShip = CType(sessHelper.GetSession(sessDiscountProposalDtlOwnerShip), ArrayList)

        Select Case e.CommandName
            Case "add"
                ddlFBrandCategory = CType(e.Item.FindControl("ddlFBrandCategory"), DropDownList)
                txtFBrandNameCategory = CType(e.Item.FindControl("txtFBrandNameCategory"), System.Web.UI.WebControls.TextBox)
                txtFQtyUnit = CType(e.Item.FindControl("txtFQtyUnit"), System.Web.UI.WebControls.TextBox)
                txtFModel = CType(e.Item.FindControl("txtFModel"), System.Web.UI.WebControls.TextBox)

                If ddlFBrandCategory.SelectedIndex = 0 Then
                    MessageBox.Show("Brand Kendaraan harus dipilih.")
                    Return
                End If
                If ddlFBrandCategory.SelectedValue = 1 Then   '--  Value = 'Non-Mitsubishi 
                    If txtFBrandNameCategory.Text = String.Empty Then
                        MessageBox.Show("Brand Name harus diisi")
                        Return
                    End If
                End If
                If txtFModel.Text = String.Empty Then
                    MessageBox.Show("Model Kendaraan harus diisi")
                    Return
                End If
                If txtFQtyUnit.Text = String.Empty OrElse txtFQtyUnit.Text.Trim = "0" Then
                    MessageBox.Show("Qty Unit harus diisi atau harus lebih dari 0")
                    Return
                End If

                For Each objDtlOwn As DiscountProposalDetailOwnership In arlDiscountProposalDtlOwnerShip
                    If objDtlOwn.VehicleBrandName = txtFBrandNameCategory.Text.Trim Then
                        If objDtlOwn.VehicleModel = txtFModel.Text.Trim Then
                            MessageBox.Show("Merk dan Model sudah pernah di input.")
                            Return
                        End If
                    End If
                Next

                objDiscountProposalHeader = CType(sessHelper.GetSession(sessDiscountProposalHdr), DiscountProposalHeader)

                arlDiscountProposalDtlOwnerShip = CType(sessHelper.GetSession(sessDiscountProposalDtlOwnerShip), ArrayList)
                If IsNothing(arlDiscountProposalDtlOwnerShip) Then arlDiscountProposalDtlOwnerShip = New ArrayList

                Dim oDiscountProposalDetailOwnership As New DiscountProposalDetailOwnership
                oDiscountProposalDetailOwnership.NumberRow = arlDiscountProposalDtlOwnerShip.Count + 1
                oDiscountProposalDetailOwnership.DiscountProposalHeader = objDiscountProposalHeader
                oDiscountProposalDetailOwnership.VehicleBrandCategory = ddlFBrandCategory.SelectedValue
                oDiscountProposalDetailOwnership.VehicleBrandName = txtFBrandNameCategory.Text
                oDiscountProposalDetailOwnership.VehicleModel = txtFModel.Text.Trim
                oDiscountProposalDetailOwnership.Quantity = CInt(txtFQtyUnit.Text.Trim)
                arlDiscountProposalDtlOwnerShip.Add(oDiscountProposalDetailOwnership)

            Case "save"
                ddlEBrandCategory = CType(e.Item.FindControl("ddlEBrandCategory"), DropDownList)
                txtEBrandNameCategory = CType(e.Item.FindControl("txtEBrandNameCategory"), System.Web.UI.WebControls.TextBox)
                txtEModel = CType(e.Item.FindControl("txtEModel"), System.Web.UI.WebControls.TextBox)
                txtEQtyUnit = CType(e.Item.FindControl("txtEQtyUnit"), System.Web.UI.WebControls.TextBox)

                If ddlEBrandCategory.SelectedValue = -1 Then
                    MessageBox.Show("Brand Kendaraan harus dipilih.")
                    Return
                End If
                If ddlEBrandCategory.SelectedValue = 1 Then   '--  Value = 'Non-Mitsubishi 
                    If txtEBrandNameCategory.Text = String.Empty Then
                        MessageBox.Show("Brand Name harus diisi")
                        Return
                    End If
                End If
                If txtEModel.Text = String.Empty Then
                    MessageBox.Show("Model Kendaraan harus diisi")
                    Return
                End If
                If txtEQtyUnit.Text = String.Empty OrElse txtEQtyUnit.Text.Trim = "0" Then
                    MessageBox.Show("Qty Unit harus diisi dan harus lebih dari 0")
                    Return
                End If

                For Each objDtlOwn As DiscountProposalDetailOwnership In arlDiscountProposalDtlOwnerShip
                    If objDtlOwn.VehicleBrandName = txtEBrandNameCategory.Text.Trim Then
                        If objDtlOwn.VehicleModel = txtEModel.Text.Trim Then
                            If objDtlOwn.NumberRow <> (e.Item.ItemIndex + 1) Then
                                MessageBox.Show("Merk dan Model sudah pernah di input.")
                                Return
                            End If
                        End If
                    End If
                Next

                Dim oDiscountProposalDetailOwnership As DiscountProposalDetailOwnership = CType(arlDiscountProposalDtlOwnerShip(e.Item.ItemIndex), DiscountProposalDetailOwnership)
                oDiscountProposalDetailOwnership.VehicleBrandCategory = ddlEBrandCategory.SelectedValue
                oDiscountProposalDetailOwnership.VehicleBrandName = txtEBrandNameCategory.Text.Trim
                oDiscountProposalDetailOwnership.VehicleModel = txtEModel.Text.Trim
                oDiscountProposalDetailOwnership.Quantity = CInt(txtEQtyUnit.Text.Trim)
                dgKepemilikanKendaraan.EditItemIndex = -1
                dgKepemilikanKendaraan.ShowFooter = True

            Case "edit"
                dgKepemilikanKendaraan.ShowFooter = False
                dgKepemilikanKendaraan.EditItemIndex = e.Item.ItemIndex

            Case "delete"
                Try
                    Dim oDiscountProposalDetailOwnership As DiscountProposalDetailOwnership = CType(arlDiscountProposalDtlOwnerShip(e.Item.ItemIndex), DiscountProposalDetailOwnership)
                    If oDiscountProposalDetailOwnership.ID > 0 Then
                        Dim arrDelete As ArrayList = CType(sessHelper.GetSession(sessDeleteDiscountProposalDtlOwnerShip), ArrayList)
                        If IsNothing(arrDelete) Then arrDelete = New ArrayList
                        arrDelete.Add(oDiscountProposalDetailOwnership)
                        sessHelper.SetSession(sessDeleteDiscountProposalDtlOwnerShip, arrDelete)
                    End If
                    arlDiscountProposalDtlOwnerShip.RemoveAt(e.Item.ItemIndex)
                Catch
                End Try

            Case "cancel"
                dgKepemilikanKendaraan.EditItemIndex = -1
                dgKepemilikanKendaraan.ShowFooter = True
        End Select

        sessHelper.SetSession(sessDiscountProposalDtlOwnerShip, arlDiscountProposalDtlOwnerShip)
        BindGridKepemilikanKendaraan()
    End Sub

    Protected Sub dgEmailUser_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgEmailUser.ItemDataBound
        Dim txtFRecipientName As System.Web.UI.WebControls.TextBox
        Dim txtERecipientName As System.Web.UI.WebControls.TextBox
        Dim txtFRecipientPosition As System.Web.UI.WebControls.TextBox
        Dim txtERecipientPosition As System.Web.UI.WebControls.TextBox
        Dim txtFEmail As System.Web.UI.WebControls.TextBox
        Dim txtEEmail As System.Web.UI.WebControls.TextBox

        Dim lblSearchFEmailUser As Label
        Dim lblSearchEEmailUser As Label
        Dim lblRecipientName As Label
        Dim lblRecipientPosition As Label
        Dim lblEmail As Label
        Dim lbtnEdit As LinkButton
        Dim lbtnDelete As LinkButton

        If e.Item.ItemType = ListItemType.Footer Then
            lblSearchFEmailUser = CType(e.Item.FindControl("lblSearchFEmailUser"), Label)
            lblSearchFEmailUser.Attributes("onclick") = "showPopupSearchFEmailUser(this,'" & txtDealerCode.Text & "');"
        End If

        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            Dim oDPE As DiscountProposalEmailUser = CType(e.Item.DataItem, DiscountProposalEmailUser)
            If e.Item.ItemIndex = 0 Then
                ViewState("ItemIndex") = e.Item.ItemIndex
                intItemIndex = CType(ViewState("ItemIndex"), Integer)
            Else
                ViewState("ItemIndex") = CType(ViewState("ItemIndex"), Integer) + 1
                intItemIndex = CType(ViewState("ItemIndex"), Integer)
            End If
            '---Create Number Row
            e.Item.Cells(0).Text = intItemIndex + 1 + (dgEmailUser.CurrentPageIndex * dgEmailUser.PageSize)

            txtERecipientName = CType(e.Item.FindControl("txtERecipientName"), System.Web.UI.WebControls.TextBox)
            txtERecipientPosition = CType(e.Item.FindControl("txtERecipientPosition"), System.Web.UI.WebControls.TextBox)
            txtEEmail = CType(e.Item.FindControl("txtEEmail"), System.Web.UI.WebControls.TextBox)
            lblSearchEEmailUser = CType(e.Item.FindControl("lblSearchEEmailUser"), Label)

            lblSearchEEmailUser.Attributes("onclick") = "showPopupSearchFEmailUser(this,'" & txtDealerCode.Text & "');"

            txtERecipientName.Text = oDPE.RecipientName
            txtERecipientPosition.Text = oDPE.RecipientPosition
            txtEEmail.Text = oDPE.Email
        End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oDPE As DiscountProposalEmailUser = CType(e.Item.DataItem, DiscountProposalEmailUser)
            If e.Item.ItemIndex = 0 Then
                ViewState("ItemIndex") = e.Item.ItemIndex
                intItemIndex = CType(ViewState("ItemIndex"), Integer)
            Else
                ViewState("ItemIndex") = CType(ViewState("ItemIndex"), Integer) + 1
                intItemIndex = CType(ViewState("ItemIndex"), Integer)
            End If
            e.Item.Cells(0).Text = intItemIndex + 1 + (dgEmailUser.CurrentPageIndex * dgEmailUser.PageSize)

            lblRecipientName = CType(e.Item.FindControl("lblRecipientName"), Label)
            lblRecipientPosition = CType(e.Item.FindControl("lblRecipientPosition"), Label)
            lblEmail = CType(e.Item.FindControl("lblEmail"), Label)

            lblRecipientName.Text = oDPE.RecipientName
            lblRecipientPosition.Text = oDPE.RecipientPosition
            lblEmail.Text = oDPE.Email

            lbtnEdit = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            lbtnDelete = CType(e.Item.FindControl("lbtnDelete"), LinkButton)

            lbtnEdit.Attributes("style") = "display:table-row"
            lbtnDelete.Attributes("style") = "display:table-row"
        End If
    End Sub

    Protected Sub dgEmailUser_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgEmailUser.ItemCommand
        Dim txtFRecipientName As System.Web.UI.WebControls.TextBox
        Dim txtERecipientName As System.Web.UI.WebControls.TextBox
        Dim txtFRecipientPosition As System.Web.UI.WebControls.TextBox
        Dim txtERecipientPosition As System.Web.UI.WebControls.TextBox
        Dim txtFEmail As System.Web.UI.WebControls.TextBox
        Dim txtEEmail As System.Web.UI.WebControls.TextBox
        Dim hdnFDPEmailID As System.Web.UI.WebControls.HiddenField
        Dim hdnEDPEmailID As System.Web.UI.WebControls.HiddenField

        Dim lblRecipientName As Label
        Dim lblRecipientPosition As Label
        Dim lblEmail As Label

        arlDiscountProposalEmailUser = CType(sessHelper.GetSession(sessDiscountProposalEmailUser), ArrayList)

        Select Case e.CommandName
            Case "add"
                hdnFDPEmailID = CType(e.Item.FindControl("hdnFDPEmailID"), System.Web.UI.WebControls.HiddenField)
                txtFRecipientName = CType(e.Item.FindControl("txtFRecipientName"), System.Web.UI.WebControls.TextBox)
                txtFRecipientPosition = CType(e.Item.FindControl("txtFRecipientPosition"), System.Web.UI.WebControls.TextBox)
                txtFEmail = CType(e.Item.FindControl("txtFEmail"), System.Web.UI.WebControls.TextBox)

                If hdnFDPEmailID.Value.Trim <> "" Then
                    Dim _oDiscountProposalEmailUser As DiscountProposalEmailUser = New DiscountProposalEmailUserFacade(User).Retrieve(CInt(hdnFDPEmailID.Value.Trim))
                    If Not IsNothing(_oDiscountProposalEmailUser) Then
                        txtFRecipientName.Text = _oDiscountProposalEmailUser.RecipientName
                        txtFRecipientPosition.Text = _oDiscountProposalEmailUser.RecipientPosition
                        txtFEmail.Text = _oDiscountProposalEmailUser.Email
                    End If
                End If
                If txtFRecipientName.Text.Trim = "" Then
                    MessageBox.Show("Nama harus diisi.")
                    Return
                End If
                If txtFRecipientPosition.Text.Trim = "" Then
                    MessageBox.Show("Jabatan harus diisi.")
                    Return
                End If
                If txtFEmail.Text.Trim = String.Empty Then
                    MessageBox.Show("Email harus diisi")
                    Return
                End If

                For Each objDtlEmail As DiscountProposalEmailUser In arlDiscountProposalEmailUser
                    If objDtlEmail.Email.Trim = txtFEmail.Text.Trim Then
                        MessageBox.Show("Email penerima notifikasi sudah pernah di input.")
                        Return
                    End If
                Next

                objDiscountProposalHeader = CType(sessHelper.GetSession(sessDiscountProposalHdr), DiscountProposalHeader)

                arlDiscountProposalEmailUser = CType(sessHelper.GetSession(sessDiscountProposalEmailUser), ArrayList)
                If IsNothing(arlDiscountProposalEmailUser) Then arlDiscountProposalEmailUser = New ArrayList

                Dim oDiscountProposalEmailUser As New DiscountProposalEmailUser
                With oDiscountProposalEmailUser
                    .NumberRow = arlDiscountProposalEmailUser.Count + 1
                    .DiscountProposalHeader = objDiscountProposalHeader
                    .RecipientName = txtFRecipientName.Text
                    .RecipientPosition = txtFRecipientPosition.Text
                    .Email = txtFEmail.Text
                End With
                arlDiscountProposalEmailUser.Add(oDiscountProposalEmailUser)

                hdnFDPEmailID.Value = ""
            Case "save"
                hdnEDPEmailID = CType(e.Item.FindControl("hdnEDPEmailID"), System.Web.UI.WebControls.HiddenField)
                txtERecipientName = CType(e.Item.FindControl("txtERecipientName"), System.Web.UI.WebControls.TextBox)
                txtERecipientPosition = CType(e.Item.FindControl("txtERecipientPosition"), System.Web.UI.WebControls.TextBox)
                txtEEmail = CType(e.Item.FindControl("txtEEmail"), System.Web.UI.WebControls.TextBox)

                If hdnEDPEmailID.Value.Trim <> "" Then
                    Dim _oDiscountProposalEmailUser As DiscountProposalEmailUser = New DiscountProposalEmailUserFacade(User).Retrieve(CInt(hdnEDPEmailID.Value.Trim))
                    If Not IsNothing(_oDiscountProposalEmailUser) Then
                        txtERecipientName.Text = _oDiscountProposalEmailUser.RecipientName
                        txtERecipientPosition.Text = _oDiscountProposalEmailUser.RecipientPosition
                        txtEEmail.Text = _oDiscountProposalEmailUser.Email
                    End If
                End If
                If txtERecipientName.Text.Trim = "" Then
                    MessageBox.Show("Nama harus dipilih.")
                    Return
                End If
                If txtERecipientPosition.Text.Trim = "" Then
                    MessageBox.Show("Jabatan harus dipilih.")
                    Return
                End If
                If txtEEmail.Text.Trim = String.Empty Then
                    MessageBox.Show("Email harus diisi")
                    Return
                End If

                For Each objDtlEmail As DiscountProposalEmailUser In arlDiscountProposalEmailUser
                    If objDtlEmail.Email = txtEEmail.Text.Trim Then
                        If objDtlEmail.NumberRow <> (e.Item.ItemIndex + 1) Then
                            MessageBox.Show("Email penerima notifikasi sudah pernah di input.")
                            Return
                        End If
                    End If
                Next

                objDiscountProposalHeader = CType(sessHelper.GetSession(sessDiscountProposalHdr), DiscountProposalHeader)

                Dim oDiscountProposalEmailUser As DiscountProposalEmailUser = CType(arlDiscountProposalEmailUser(e.Item.ItemIndex), DiscountProposalEmailUser)
                With oDiscountProposalEmailUser
                    .DiscountProposalHeader = objDiscountProposalHeader
                    .RecipientName = txtERecipientName.Text
                    .RecipientPosition = txtERecipientPosition.Text
                    .Email = txtEEmail.Text
                End With
                dgEmailUser.EditItemIndex = -1
                dgEmailUser.ShowFooter = True

            Case "edit"
                dgEmailUser.ShowFooter = False
                dgEmailUser.EditItemIndex = e.Item.ItemIndex

            Case "delete"
                Try
                    Dim oDiscountProposalEmailUser As DiscountProposalEmailUser = CType(arlDiscountProposalEmailUser(e.Item.ItemIndex), DiscountProposalEmailUser)
                    If oDiscountProposalEmailUser.ID > 0 Then
                        Dim arrDelete As ArrayList = CType(sessHelper.GetSession(sessDeleteDiscountProposalEmailUser), ArrayList)
                        If IsNothing(arrDelete) Then arrDelete = New ArrayList
                        arrDelete.Add(oDiscountProposalEmailUser)
                        sessHelper.SetSession(sessDeleteDiscountProposalEmailUser, arrDelete)
                    End If
                    arlDiscountProposalEmailUser.RemoveAt(e.Item.ItemIndex)
                Catch
                End Try

            Case "cancel"
                dgEmailUser.EditItemIndex = -1
                dgEmailUser.ShowFooter = True
        End Select

        sessHelper.SetSession(sessDiscountProposalEmailUser, arlDiscountProposalEmailUser)
        BindGridEmailUser()
    End Sub

    Protected Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        RemoveALLSession()
        Response.Redirect("FrmDaftarDiscountProposal.aspx")
    End Sub

    Private Sub ddlPurchaseMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPurchaseMethod.SelectedIndexChanged
        If ddlPurchaseMethod.SelectedIndex = 0 Then
            ddlLeasing.Attributes("style") = "display:none"
            ddlAPMSubsidy.Attributes("style") = "display:none"
            ddlLeasing.SelectedIndex = 0
            ddlAPMSubsidy.SelectedIndex = 0
        End If
        If ddlPurchaseMethod.SelectedValue = 1 Then
            ddlLeasing.Attributes("style") = "display:table-row"
            ddlAPMSubsidy.Attributes("style") = "display:table-row"
        Else
            ddlLeasing.Attributes("style") = "display:none"
            ddlAPMSubsidy.Attributes("style") = "display:none"
            ddlLeasing.SelectedIndex = 0
            ddlAPMSubsidy.SelectedIndex = 0
        End If
    End Sub

    Private Sub btnUploadSuratKomitmen_Click(sender As Object, e As EventArgs) Handles btnUploadSuratKomitmen.Click
        Dim objPostedData As HttpPostedFile
        Dim sFileName As String
        Dim objDiscountProposalDetailDocument As DiscountProposalDetailDocument = New DiscountProposalDetailDocument()

        If IsNothing(FUSuratKomitmentKontrak) OrElse FUSuratKomitmentKontrak.Value = String.Empty Then
            MessageBox.Show("Lampiran masih kosong")
            Return
        End If
        Dim _filename As String = System.IO.Path.GetFileName(FUSuratKomitmentKontrak.PostedFile.FileName)
        If _filename.Trim().Length <= 0 Then
            MessageBox.Show("Upload file belum diisi\n")
            Return
        End If
        If _filename.Trim().Length > 0 Then
            If FUSuratKomitmentKontrak.PostedFile.ContentLength > MAX_FILE_SIZE Then
                MessageBox.Show("Ukuran file tidak boleh melebihi " & (MAX_FILE_SIZE / 1024) & "kb\n")
                Return
            End If
        End If
        Dim ext As String = System.IO.Path.GetExtension(FUSuratKomitmentKontrak.PostedFile.FileName)
        If Not (ext.ToUpper() = ".PDF" OrElse
                ext.ToUpper() = ".DOC" OrElse
                ext.ToUpper() = ".DOCX" OrElse
                ext.ToUpper() = ".XLS" OrElse
                ext.ToUpper() = ".XLSX") Then
            MessageBox.Show("Hanya menerima file format (PDF/XLS/DOC)")
            Return
        End If

        lbtnDeleteFileSuratKomitmen_Click(Nothing, Nothing)

        If Not IsNothing(FUSuratKomitmentKontrak) OrElse FUSuratKomitmentKontrak.Value <> String.Empty Then
            objPostedData = FUSuratKomitmentKontrak.PostedFile
        Else
            objPostedData = Nothing
        End If

        If Not (IsNothing(objPostedData)) Then
            'sFileName = objPostedData.FileName.Split("\")(objPostedData.FileName.Split("\").Length - 1)
            sFileName = "Anti Bribery Clause_" & TimeStamp()

            If KTB.DNet.UI.Helper.FileHelper.IsExecutableFile(sFileName) Then
                MessageBox.Show("Tidak diperkenankan mengupload file dengan ekstensi '.exe'. Pastikan file anda bebas dari virus.")
                Return
            End If

            Dim SrcFile As String = Path.GetFileName(objPostedData.FileName) '-- Source file name
            Dim strDPPathConfig As String = "DiscountProposal\Fleet\"
            Dim extFile As String = SrcFile.Substring(SrcFile.Length - 4)
            If Left(extFile, 1) <> "." Then
                extFile = "." & extFile
            End If
            Dim strDPPathFile As String = objDealer.DealerCode & "\" & TimeStamp() & extFile
            Dim strDestFile As String = strDPPathConfig & strDPPathFile '--Destination File                       

            objDiscountProposalHeader = CType(sessHelper.GetSession(sessDiscountProposalHdr), DiscountProposalHeader)
            With objDiscountProposalDetailDocument
                .DiscountProposalHeader = objDiscountProposalHeader
                .AttachmentData = objPostedData
                .FileType = 0  '---> Surat Komitmen
                .FileName = sFileName
                .Path = strDestFile
            End With
            txtSuratKomitmen.Text = objPostedData.FileName
            If txtSuratKomitmen.Text.Trim <> "" Then
                lbtnDeleteFileSuratKomitmen.Visible = True
            Else
                lbtnDeleteFileSuratKomitmen.Visible = False
            End If

            UploadAttachment(objDiscountProposalDetailDocument, TempDirectory)

            arlDiscountProposalDtlDocument = CType(sessHelper.GetSession(sessDiscountProposalDtlDocument), ArrayList)
            arlDiscountProposalDtlDocument.Add(objDiscountProposalDetailDocument)
        Else
            MessageBox.Show(SR.DataNotFound("Attachment File"))
        End If
    End Sub

    Private Sub btnUploadSuratPernyataan_Click(sender As Object, e As EventArgs) Handles btnUploadSuratPernyataan.Click
        Dim objPostedData As HttpPostedFile
        Dim sFileName As String
        Dim objDiscountProposalDetailDocument As DiscountProposalDetailDocument = New DiscountProposalDetailDocument()

        If IsNothing(FUSuratPernyataan) OrElse FUSuratPernyataan.Value = String.Empty Then
            MessageBox.Show("Lampiran masih kosong")
            Return
        End If
        Dim _filename As String = System.IO.Path.GetFileName(FUSuratPernyataan.PostedFile.FileName)
        If _filename.Trim().Length <= 0 Then
            MessageBox.Show("Upload file belum diisi\n")
            Return
        End If
        If _filename.Trim().Length > 0 Then
            If FUSuratPernyataan.PostedFile.ContentLength > MAX_FILE_SIZE Then
                MessageBox.Show("Ukuran file tidak boleh melebihi " & (MAX_FILE_SIZE / 1024) & "kb\n")
                Return
            End If
        End If
        Dim ext As String = System.IO.Path.GetExtension(FUSuratPernyataan.PostedFile.FileName)
        If Not (ext.ToUpper() = ".PDF" OrElse
                ext.ToUpper() = ".DOC" OrElse
                ext.ToUpper() = ".DOCX" OrElse
                ext.ToUpper() = ".XLS" OrElse
                ext.ToUpper() = ".XLSX" OrElse
                ext.ToUpper() = ".JPEG" OrElse
                ext.ToUpper() = ".JPG") Then
            MessageBox.Show("Hanya menerima file format (PDF/XLS/DOC/JPG/JPEG)")
            Return
        End If
        If Not IsNothing(FUSuratPernyataan) OrElse FUSuratPernyataan.Value <> String.Empty Then
            objPostedData = FUSuratPernyataan.PostedFile
        Else
            objPostedData = Nothing
        End If

        If Not (IsNothing(objPostedData)) Then
            'sFileName = objPostedData.FileName.Split("\")(objPostedData.FileName.Split("\").Length - 1)
            sFileName = "Anti War Statement Letter_" & TimeStamp()

            If KTB.DNet.UI.Helper.FileHelper.IsExecutableFile(sFileName) Then
                MessageBox.Show("Tidak diperkenankan mengupload file dengan ekstensi '.exe'. Pastikan file anda bebas dari virus.")
                Return
            End If

            lbtnDeleteFileSuratPernyataan_Click(Nothing, Nothing)

            Dim SrcFile As String = Path.GetFileName(objPostedData.FileName) '-- Source file name
            Dim strDPPathConfig As String = "DiscountProposal\Fleet\"
            Dim extFile As String = SrcFile.Substring(SrcFile.Length - 4)
            If Left(extFile, 1) <> "." Then
                extFile = "." & extFile
            End If
            Dim strDPPathFile As String = objDealer.DealerCode & "\" & TimeStamp() & extFile
            Dim strDestFile As String = strDPPathConfig & strDPPathFile '--Destination File                       

            objDiscountProposalHeader = CType(sessHelper.GetSession(sessDiscountProposalHdr), DiscountProposalHeader)
            With objDiscountProposalDetailDocument
                .DiscountProposalHeader = objDiscountProposalHeader
                .AttachmentData = objPostedData
                .FileType = 1  '---> Surat Pernyataan
                .FileName = sFileName
                .Path = strDestFile
            End With
            txtSuratPernyataan.Text = objPostedData.FileName
            If txtSuratPernyataan.Text.Trim <> "" Then
                If IsLoginAsDealer() Then lbtnDeleteFileSuratPernyataan.Visible = True
            Else
                lbtnDeleteFileSuratPernyataan.Visible = False
            End If

            UploadAttachment(objDiscountProposalDetailDocument, TempDirectory)

            arlDiscountProposalDtlDocument = CType(sessHelper.GetSession(sessDiscountProposalDtlDocument), ArrayList)
            arlDiscountProposalDtlDocument.Add(objDiscountProposalDetailDocument)
        Else
            MessageBox.Show(SR.DataNotFound("Attachment File"))
        End If
    End Sub

    Private Sub btnUploadLampiranPOSPK_Click(sender As Object, e As EventArgs) Handles btnUploadLampiranPOSPK.Click
        Dim objPostedData As HttpPostedFile
        Dim sFileName As String
        Dim objDiscountProposalDetailDocument As DiscountProposalDetailDocument = New DiscountProposalDetailDocument()

        If IsNothing(FULampiranPOSPK) OrElse FULampiranPOSPK.Value = String.Empty Then
            MessageBox.Show("Lampiran masih kosong")
            Return
        End If
        Dim _filename As String = System.IO.Path.GetFileName(FULampiranPOSPK.PostedFile.FileName)
        If _filename.Trim().Length <= 0 Then
            MessageBox.Show("Upload file belum diisi\n")
            Return
        End If
        If _filename.Trim().Length > 0 Then
            If FULampiranPOSPK.PostedFile.ContentLength > MAX_FILE_SIZE Then
                MessageBox.Show("Ukuran file tidak boleh melebihi " & (MAX_FILE_SIZE / 1024) & "kb\n")
                Return
            End If
        End If
        Dim ext As String = System.IO.Path.GetExtension(FULampiranPOSPK.PostedFile.FileName)
        If Not (ext.ToUpper() = ".PDF" OrElse
                ext.ToUpper() = ".DOC" OrElse
                ext.ToUpper() = ".DOCX" OrElse
                ext.ToUpper() = ".XLS" OrElse
                ext.ToUpper() = ".XLSX") Then
            MessageBox.Show("Hanya menerima file format (PDF/XLS/DOC)")
            Return
        End If
        If Not IsNothing(FULampiranPOSPK) OrElse FULampiranPOSPK.Value <> String.Empty Then
            objPostedData = FULampiranPOSPK.PostedFile
        Else
            objPostedData = Nothing
        End If

        If Not (IsNothing(objPostedData)) Then
            sFileName = objPostedData.FileName.Split("\")(objPostedData.FileName.Split("\").Length - 1)
            lbtnDeleteFileLampiranPOSPK_Click(Nothing, Nothing)

            If KTB.DNet.UI.Helper.FileHelper.IsExecutableFile(sFileName) Then
                MessageBox.Show("Tidak diperkenankan mengupload file dengan ekstensi '.exe'. Pastikan file anda bebas dari virus.")
                Return
            End If

            Dim SrcFile As String = Path.GetFileName(objPostedData.FileName) '-- Source file name
            Dim strDPPathConfig As String = "DiscountProposal\Fleet\"
            Dim strDPPathFile As String = objDealer.DealerCode & "\" & TimeStamp() & SrcFile.Substring(SrcFile.Length - 4)
            Dim strDestFile As String = strDPPathConfig & strDPPathFile '--Destination File                       

            objDiscountProposalHeader = CType(sessHelper.GetSession(sessDiscountProposalHdr), DiscountProposalHeader)
            With objDiscountProposalDetailDocument
                .DiscountProposalHeader = objDiscountProposalHeader
                .AttachmentData = objPostedData
                .FileType = 2  '---> Lampiran PO/SPK
                .FileName = sFileName
                .Path = strDestFile
            End With
            txtLampiranPOSPK.Text = objPostedData.FileName

            UploadAttachment(objDiscountProposalDetailDocument, TempDirectory)

            arlDiscountProposalDtlDocument = CType(sessHelper.GetSession(sessDiscountProposalDtlDocument), ArrayList)
            arlDiscountProposalDtlDocument.Add(objDiscountProposalDetailDocument)
        Else
            MessageBox.Show(SR.DataNotFound("Attachment File"))
        End If
    End Sub

    Private Sub lbtnDeleteFileSuratKomitmen_Click(sender As Object, e As EventArgs) Handles lbtnDeleteFileSuratKomitmen.Click
        Dim i% = 0
        Dim isExistDeleteFile As Boolean = False
        Dim oDiscountProposalDtlDocument As New DiscountProposalDetailDocument
        arlDiscountProposalDtlDocument = CType(sessHelper.GetSession(sessDiscountProposalDtlDocument), ArrayList)
        If IsNothing(arlDiscountProposalDtlDocument) Then arlDiscountProposalDtlDocument = New ArrayList
        If Not IsNothing(arlDiscountProposalDtlDocument) AndAlso arlDiscountProposalDtlDocument.Count = 0 Then Exit Sub

        For Each objDPDoc As DiscountProposalDetailDocument In arlDiscountProposalDtlDocument
            If objDPDoc.FileType = 0 Then   '---ValueCode = SuratKomitmenKontraktor
                oDiscountProposalDtlDocument = objDPDoc
                isExistDeleteFile = True
                Exit For
            End If
            i += 1
        Next
        If Not IsNothing(oDiscountProposalDtlDocument) AndAlso oDiscountProposalDtlDocument.ID > 0 Then
            Dim arrDelete As ArrayList = CType(sessHelper.GetSession(sessDeleteDiscountProposalDtlDocument), ArrayList)
            If IsNothing(arrDelete) Then arrDelete = New ArrayList
            arrDelete.Add(oDiscountProposalDtlDocument)
            sessHelper.SetSession(sessDeleteDiscountProposalDtlDocument, arrDelete)
        End If
        If isExistDeleteFile Then
            arlDiscountProposalDtlDocument.RemoveAt(i)
            txtSuratKomitmen.Text = ""
        End If
        sessHelper.SetSession(sessDiscountProposalDtlDocument, arlDiscountProposalDtlDocument)
    End Sub

    Private Sub lbtnDeleteFileSuratPernyataan_Click(sender As Object, e As EventArgs) Handles lbtnDeleteFileSuratPernyataan.Click
        Dim i% = 0
        Dim isExistDeleteFile As Boolean = False
        Dim oDiscountProposalDtlDocument As New DiscountProposalDetailDocument
        arlDiscountProposalDtlDocument = CType(sessHelper.GetSession(sessDiscountProposalDtlDocument), ArrayList)
        If IsNothing(arlDiscountProposalDtlDocument) Then arlDiscountProposalDtlDocument = New ArrayList
        If Not IsNothing(arlDiscountProposalDtlDocument) AndAlso arlDiscountProposalDtlDocument.Count = 0 Then Exit Sub

        For Each objDPDoc As DiscountProposalDetailDocument In arlDiscountProposalDtlDocument
            If objDPDoc.FileType = 1 Then   '---ValueCode = SuratPernyataanTidakUntukMiliter
                oDiscountProposalDtlDocument = objDPDoc
                isExistDeleteFile = True
                Exit For
            End If
            i += 1
        Next
        If Not IsNothing(oDiscountProposalDtlDocument) AndAlso oDiscountProposalDtlDocument.ID > 0 Then
            Dim arrDelete As ArrayList = CType(sessHelper.GetSession(sessDeleteDiscountProposalDtlDocument), ArrayList)
            If IsNothing(arrDelete) Then arrDelete = New ArrayList
            arrDelete.Add(oDiscountProposalDtlDocument)
            sessHelper.SetSession(sessDeleteDiscountProposalDtlDocument, arrDelete)
        End If
        If isExistDeleteFile Then
            arlDiscountProposalDtlDocument.RemoveAt(i)
            txtSuratPernyataan.Text = ""
        End If
        sessHelper.SetSession(sessDiscountProposalDtlDocument, arlDiscountProposalDtlDocument)
    End Sub

    Private Sub lbtnDeleteFileLampiranPOSPK_Click(sender As Object, e As EventArgs) Handles lbtnDeleteFileLampiranPOSPK.Click
        Dim i% = 0
        Dim isExistDeleteFile As Boolean = False
        Dim oDiscountProposalDtlDocument As DiscountProposalDetailDocument
        arlDiscountProposalDtlDocument = CType(sessHelper.GetSession(sessDiscountProposalDtlDocument), ArrayList)
        If Not IsNothing(arlDiscountProposalDtlDocument) AndAlso arlDiscountProposalDtlDocument.Count = 0 Then Exit Sub
        For Each objDPDoc As DiscountProposalDetailDocument In arlDiscountProposalDtlDocument
            If objDPDoc.FileType = 2 Then   '---ValueCode = LampiranPOSPK
                oDiscountProposalDtlDocument = objDPDoc
                isExistDeleteFile = True
                Exit For
            End If
            i += 1
        Next
        If Not IsNothing(oDiscountProposalDtlDocument) AndAlso oDiscountProposalDtlDocument.ID > 0 Then
            Dim arrDelete As ArrayList = CType(sessHelper.GetSession(sessDeleteDiscountProposalDtlDocument), ArrayList)
            If IsNothing(arrDelete) Then arrDelete = New ArrayList
            arrDelete.Add(oDiscountProposalDtlDocument)
            sessHelper.SetSession(sessDeleteDiscountProposalDtlDocument, arrDelete)
        End If
        If isExistDeleteFile Then
            arlDiscountProposalDtlDocument.RemoveAt(i)
            txtLampiranPOSPK.Text = ""
        End If
        sessHelper.SetSession(sessDiscountProposalDtlDocument, arlDiscountProposalDtlDocument)
    End Sub

    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        If hdnDiscountProposalHeaderID.Value = "" Then
            MessageBox.Show("Data Discount Proposal belum ada")
            Exit Sub
        End If

        objDiscountProposalHeader = CType(sessHelper.GetSession(sessDiscountProposalHdr), DiscountProposalHeader)

        Dim strJs As String = String.Empty
        Dim _result As Integer = -1
        If Not IsNothing(objDiscountProposalHeader) AndAlso objDiscountProposalHeader.ID > 0 Then
            _result = New DiscountProposalHeaderFacade(User).DeleteTransaction(objDiscountProposalHeader)
            If _result >= 0 Then
                strJs = "alert('Hapus data sukses');"
                strJs += "window.location = '../DiscountProposal/FrmDaftarDiscountProposal.aspx?Back=OK';"
            Else
                strJs = "alert('Hapus data gagal');"
            End If
        Else
            strJs = "alert('Hapus data gagal');"
        End If
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Script", strJs, True)
    End Sub

    Private Sub ddlLeasing_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLeasing.SelectedIndexChanged
        If ddlLeasing.SelectedIndex = 0 Then
            ddlAPMSubsidy.SelectedIndex = 0
        End If
    End Sub

    Private Sub ddlCustomerType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomerType.SelectedIndexChanged
        If ddlCustomerType.SelectedIndex = 0 Then
            trKTP.Visible = False
            trNIB.Visible = False
        ElseIf ddlCustomerType.SelectedValue = 0 Then
            trKTP.Visible = True
            trNIB.Visible = False
        ElseIf ddlCustomerType.SelectedValue = 1 OrElse ddlCustomerType.SelectedValue = 2 Then
            trKTP.Visible = False
            trNIB.Visible = True
        Else
            trKTP.Visible = False
            trNIB.Visible = False
        End If
        If ddlCustomerType.SelectedValue = 0 OrElse ddlCustomerType.SelectedValue = 1 Then
            ddlDealerDirectSales.Enabled = False
            txtContractorName.Enabled = False
            txtSuratKomitmen.Enabled = False
            FUSuratKomitmentKontrak.Visible = False
            lbtnDeleteFileSuratKomitmen.Visible = False
            ddlDealerDirectSales.SelectedIndex = 0
            txtContractorName.Text = ""
            txtSuratKomitmen.Text = ""
        Else
            If IsLoginAsDealer() Then
                ddlDealerDirectSales.Enabled = True
                txtContractorName.Enabled = True
                txtSuratKomitmen.Enabled = True
                FUSuratKomitmentKontrak.Visible = True
                lbtnDeleteFileSuratKomitmen.Visible = True
            End If
            ddlDealerDirectSales_SelectedIndexChanged(ddlDealerDirectSales, Nothing)
        End If
        txtFleetCustomerName.Text = ""
        hdnFleetCustomerHeaderID.Value = ""
        hdnFleetCustomerDetailID.Value = ""
        txtAddressFleetCustomerDtl.Text = ""
        txtNoKTP.Text = ""
        txtNoNIB.Text = ""
        ddlBusinessSector.SelectedIndex = 0
    End Sub

    Private Sub dgDPCustomer_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgDPCustomer.ItemCommand
        Dim oDiscountProposalDetailCustomer As New DiscountProposalDetailCustomer

        Dim lblNama As Label
        Dim txtFName As System.Web.UI.WebControls.TextBox
        Dim txtEName As System.Web.UI.WebControls.TextBox
        Dim lblIdentityNumber As Label
        Dim txtFIdentityNumber As System.Web.UI.WebControls.TextBox
        Dim txtEIdentityNumber As System.Web.UI.WebControls.TextBox

        objDiscountProposalHeader = CType(sessHelper.GetSession(sessDiscountProposalHdr), DiscountProposalHeader)
        arlDiscountProposalDtlCustomer = CType(sessHelper.GetSession(sessDiscountProposalDtlCustomer), ArrayList)

        Select Case e.CommandName
            Case "add"
                txtFName = CType(e.Item.FindControl("txtFName"), System.Web.UI.WebControls.TextBox)
                txtFIdentityNumber = CType(e.Item.FindControl("txtFIdentityNumber"), System.Web.UI.WebControls.TextBox)

                If txtFName.Text.Trim = String.Empty Then
                    MessageBox.Show("Nama konsumen harus diisi.")
                    Return
                End If

                For Each objDtl As DiscountProposalDetailCustomer In arlDiscountProposalDtlCustomer
                    If objDtl.Name.Trim = txtFName.Text.Trim Then
                        MessageBox.Show("Nama konsumen sudah pernah di input.")
                        Return
                    End If
                Next

                objDiscountProposalHeader = CType(sessHelper.GetSession(sessDiscountProposalHdr), DiscountProposalHeader)
                With oDiscountProposalDetailCustomer
                    .DiscountProposalHeader = objDiscountProposalHeader
                    .Name = txtFName.Text
                    .IdentityNumber = txtFIdentityNumber.Text
                End With
                arlDiscountProposalDtlCustomer.Add(oDiscountProposalDetailCustomer)

            Case "save"
                txtEName = CType(e.Item.FindControl("txtEName"), System.Web.UI.WebControls.TextBox)
                txtEIdentityNumber = CType(e.Item.FindControl("txtEIdentityNumber"), System.Web.UI.WebControls.TextBox)

                If txtEName.Text.Trim = String.Empty Then
                    MessageBox.Show("Nama konsumen harus diisi.")
                    Return
                End If
                Dim countExistRec As Short = 0
                For Each objDtl As DiscountProposalDetailCustomer In arlDiscountProposalDtlCustomer
                    If objDtl.Name.Trim = txtEName.Text.Trim Then
                        countExistRec += 1
                    End If
                    If countExistRec > 1 Then
                        MessageBox.Show("Nama konsumen sudah pernah di input.")
                        Return
                    End If
                Next

                objDiscountProposalHeader = CType(sessHelper.GetSession(sessDiscountProposalHdr), DiscountProposalHeader)
                Dim oDiscountProposalDtlCustomer As DiscountProposalDetailCustomer = CType(arlDiscountProposalDtlCustomer(e.Item.ItemIndex), DiscountProposalDetailCustomer)
                With oDiscountProposalDtlCustomer
                    .DiscountProposalHeader = objDiscountProposalHeader
                    .Name = txtEName.Text
                    .IdentityNumber = txtEIdentityNumber.Text
                End With
                dgDPCustomer.EditItemIndex = -1
                dgDPCustomer.ShowFooter = True

            Case "edit"
                dgDPCustomer.ShowFooter = False
                dgDPCustomer.EditItemIndex = e.Item.ItemIndex

            Case "delete" 'Delete this datagrid item 
                Try
                    oDiscountProposalDetailCustomer = CType(arlDiscountProposalDtlCustomer(e.Item.ItemIndex), DiscountProposalDetailCustomer)
                    If oDiscountProposalDetailCustomer.ID > 0 Then
                        Dim arrDelete As ArrayList = CType(sessHelper.GetSession(sessDeleteDiscountProposalDtlCustomer), ArrayList)
                        If IsNothing(arrDelete) Then arrDelete = New ArrayList
                        arrDelete.Add(oDiscountProposalDetailCustomer)
                        sessHelper.SetSession(sessDeleteDiscountProposalDtlCustomer, arrDelete)
                    End If
                    arlDiscountProposalDtlCustomer.RemoveAt(e.Item.ItemIndex)
                Catch
                End Try
            Case "cancel"
                dgDPCustomer.EditItemIndex = -1
                dgDPCustomer.ShowFooter = True
        End Select

        If e.CommandName <> "" Then
            sessHelper.SetSession(sessDiscountProposalDtlCustomer, arlDiscountProposalDtlCustomer)
            BindGridDPCustomer()
        End If

    End Sub

    Private Sub dgDPCustomer_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgDPCustomer.ItemDataBound
        Dim oDiscountProposalDetailCustomer As New DiscountProposalDetailCustomer

        Dim lblName As Label
        Dim lblIdentityNumber As Label
        Dim lblFIdentityNumber As Label
        Dim txtEIdentityNumber As System.Web.UI.WebControls.TextBox
        Dim txtEName As System.Web.UI.WebControls.TextBox
        Dim lbtnEdit As LinkButton
        Dim lbtnDelete As LinkButton

        objDiscountProposalHeader = CType(sessHelper.GetSession(sessDiscountProposalHdr), DiscountProposalHeader)
        Dim arrDiscountProposalDtlCustomer As ArrayList = CType(sessHelper.GetSession(sessDiscountProposalDtlCustomer), ArrayList)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oDiscountProposalDtlCustomer As DiscountProposalDetailCustomer = CType(e.Item.DataItem, DiscountProposalDetailCustomer)
            If e.Item.ItemIndex = 0 Then
                ViewState("ItemIndex") = e.Item.ItemIndex
                intItemIndex = CType(ViewState("ItemIndex"), Integer)
            Else
                ViewState("ItemIndex") = CType(ViewState("ItemIndex"), Integer) + 1
                intItemIndex = CType(ViewState("ItemIndex"), Integer)
            End If
            e.Item.Cells(0).Text = intItemIndex + 1 + (dgDPCustomer.CurrentPageIndex * dgDPCustomer.PageSize)

            lblName = CType(e.Item.FindControl("lblName"), Label)
            lblIdentityNumber = CType(e.Item.FindControl("lblIdentityNumber"), Label)

            If Not IsNothing(oDiscountProposalDtlCustomer) Then
                lblName.Text = oDiscountProposalDtlCustomer.Name
                lblIdentityNumber.Text = oDiscountProposalDtlCustomer.IdentityNumber
            End If
        End If

        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            Dim oDiscountProposalDtlCustomer As DiscountProposalDetailCustomer = CType(e.Item.DataItem, DiscountProposalDetailCustomer)
            If e.Item.ItemIndex = 0 Then
                ViewState("ItemIndex") = e.Item.ItemIndex
                intItemIndex = CType(ViewState("ItemIndex"), Integer)
            Else
                ViewState("ItemIndex") = CType(ViewState("ItemIndex"), Integer) + 1
                intItemIndex = CType(ViewState("ItemIndex"), Integer)
            End If
            e.Item.Cells(0).Text = intItemIndex + 1 + (dgDPCustomer.CurrentPageIndex * dgDPCustomer.PageSize)

            txtEName = CType(e.Item.FindControl("txtEName"), System.Web.UI.WebControls.TextBox)
            txtEIdentityNumber = CType(e.Item.FindControl("txtEIdentityNumber"), System.Web.UI.WebControls.TextBox)

            If Not IsNothing(oDiscountProposalDtlCustomer) Then
                txtEName.Text = oDiscountProposalDtlCustomer.Name
                txtEIdentityNumber.Text = oDiscountProposalDtlCustomer.IdentityNumber
            End If
        End If
    End Sub

    Private Sub btnKembaliToMain_Click(sender As Object, e As EventArgs) Handles btnKembaliToMain.Click
        Dim arrDiscountProposalDtlCustomer As ArrayList = CType(sessHelper.GetSession(sessDiscountProposalDtlCustomer), ArrayList)
        If Not IsNothing(arrDiscountProposalDtlCustomer) AndAlso arrDiscountProposalDtlCustomer.Count > 0 Then
            txtNameOnFaktur.Text = ""
            For Each obj As DiscountProposalDetailCustomer In arrDiscountProposalDtlCustomer
                If txtNameOnFaktur.Text.Trim = "" Then
                    txtNameOnFaktur.Text = obj.Name
                Else
                    txtNameOnFaktur.Text += ";" & obj.Name
                End If
            Next
        End If

        MainPanel.Attributes("style") = "display:table-row"
        PanelDataCustomer.Attributes("style") = "display:none"
        hdnShowDataCustomer.Value = 0
    End Sub

    Private Sub btnGetDataCustomer_Click(sender As Object, e As EventArgs) Handles btnGetDataCustomer.Click
        arlDiscountProposalDtlCustomer = CType(sessHelper.GetSession(sessDiscountProposalDtlCustomer), ArrayList)
        sessHelper.SetSession(sessDiscountProposalDtlCustomer, arlDiscountProposalDtlCustomer)
        BindGridDPCustomer()

        MainPanel.Attributes("style") = "display:none"
        PanelDataCustomer.Attributes("style") = "display:table-row"
    End Sub

    Private Sub btnBuatFleet_Click(sender As Object, e As EventArgs) Handles btnBuatFleet.Click
        hdnFleetCustomerHeaderID.Value = ""
        hdnFleetCustomerDetailID.Value = ""
        txtFleetCustomerName.Text = ""
        txtAddressFleetCustomerDtl.Text = ""
        txtNoNIB.Text = ""
        txtNoKTP.Text = ""
        ddlBusinessSector.SelectedIndex = 0

        txtFleetCustomerName.Enabled = True
        txtAddressFleetCustomerDtl.Enabled = True
        txtNoNIB.Enabled = True
        txtNoKTP.Enabled = True
        ddlBusinessSector.Enabled = True
        txtFleetCustomerName.Focus()
    End Sub

    Private Sub ddlProjectKindMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProjectKindMethod.SelectedIndexChanged
        If ddlProjectKindMethod.SelectedValue = 2 Then
            txtProjectKindMethodOther.Attributes("style") = "display:table-row"
        Else
            txtProjectKindMethodOther.Attributes("style") = "display:none"
            txtProjectKindMethodOther.Text = ""
        End If
    End Sub

    Private Sub ddlDealerDirectSales_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDealerDirectSales.SelectedIndexChanged
        If ddlDealerDirectSales.SelectedValue = 1 Then  '---- value = 'Tidak'
            If IsLoginAsDealer() Then
                txtContractorName.Enabled = True
                txtSuratKomitmen.Enabled = True
                FUSuratKomitmentKontrak.Visible = True
            End If
            If txtSuratKomitmen.Text.Trim = "" Then
                lbtnDeleteFileSuratKomitmen.Visible = False
            Else
                If IsLoginAsDealer() Then lbtnDeleteFileSuratKomitmen.Visible = True
            End If
        Else
            If ddlCustomerType.SelectedValue = 2 Then
                txtContractorName.Enabled = False
                txtSuratKomitmen.Enabled = False
                txtContractorName.Text = ""
                txtSuratKomitmen.Text = ""
                FUSuratKomitmentKontrak.Visible = False
                lbtnDeleteFileSuratKomitmen.Visible = False
                lbtnDeleteFileSuratKomitmen_Click(Nothing, Nothing)
            End If
        End If
    End Sub

    Protected Sub txtEstimasiDealPrice_TextChanged(sender As Object, e As EventArgs)
        Dim txtEstimasiDealPrice As System.Web.UI.WebControls.TextBox = DirectCast(sender, System.Web.UI.WebControls.TextBox)
        If txtEstimasiDealPrice.Text.Trim = "" Then txtEstimasiDealPrice.Text = 0
        txtEstimasiDealPrice.Text = NotZerroFirstIndexChar(txtEstimasiDealPrice.Text)

        Dim dblEstimasiDealPrice As Double = BlankToZerro(txtEstimasiDealPrice.Text)

        Dim dblRetailPriceOnTheRoad As Double = 0
        Dim txtRetailPriceOnTheRoad As System.Web.UI.WebControls.TextBox
        For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
            txtRetailPriceOnTheRoad = CType(itemData.FindControl(strtxtRetailPriceOnTheRoad), System.Web.UI.WebControls.TextBox)
            If Not IsNothing(txtRetailPriceOnTheRoad) Then
                dblRetailPriceOnTheRoad = NotZerroFirstIndexChar(txtRetailPriceOnTheRoad.Text)
                Exit For
            End If
        Next

        Dim lblGrossDealerMargin As Label
        Dim lblDiskonDealer As Label
        Dim lblNettDealerMargin As Label
        Dim txtPermohonanDiskonProgramFleetCustomer As System.Web.UI.WebControls.TextBox
        For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
            If IsNothing(lblGrossDealerMargin) Then
                lblGrossDealerMargin = CType(itemData.FindControl(strlblGrossDealerMargin), Label)
            End If
            If IsNothing(lblDiskonDealer) Then
                lblDiskonDealer = CType(itemData.FindControl(strlblDiskonDealer), Label)
            End If
            If IsNothing(lblNettDealerMargin) Then
                lblNettDealerMargin = CType(itemData.FindControl(strlblNettDealerMargin), Label)
            End If
            If IsNothing(txtPermohonanDiskonProgramFleetCustomer) Then
                txtPermohonanDiskonProgramFleetCustomer = CType(itemData.FindControl(strtxtPermohonanDiskonProgramFleetCustomer), System.Web.UI.WebControls.TextBox)
            End If
            If Not IsNothing(lblGrossDealerMargin) And Not IsNothing(lblDiskonDealer) And Not IsNothing(lblNettDealerMargin) And Not IsNothing(txtPermohonanDiskonProgramFleetCustomer) Then
                Exit For
            End If
        Next
        If Not IsNothing(lblDiskonDealer) Then
            lblDiskonDealer.Text = Format(BlankToZerro(dblRetailPriceOnTheRoad - BlankToZerro(dblEstimasiDealPrice)), "#,##0")
        End If
        Dim dblGrossDealerMargin As Double = BlankToZerro(lblGrossDealerMargin.Text)
        Dim dblDiskonDealer As Double = BlankToZerro(lblDiskonDealer.Text)
        If Not IsNothing(lblNettDealerMargin) Then
            lblNettDealerMargin.Text = Format(dblGrossDealerMargin - dblDiskonDealer, "#,##0")
        End If
        If Not IsNothing(txtPermohonanDiskonProgramFleetCustomer) Then
            txtPermohonanDiskonProgramFleetCustomer_TextChanged(txtPermohonanDiskonProgramFleetCustomer, Nothing)
            If IsNothing(sessHelper.GetSession("CtrlIDFocus")) Then
                sessHelper.SetSession("CtrlIDFocus", txtPermohonanDiskonProgramFleetCustomer)
            End If
        End If
    End Sub

    Protected Sub txtPermohonanDiskonProgramFleetCustomer_TextChanged(sender As Object, e As EventArgs)
        Dim txtPermohonanDiskonProgramFleetCustomer As System.Web.UI.WebControls.TextBox = DirectCast(sender, System.Web.UI.WebControls.TextBox)
        If IsNothing(sessHelper.GetSession("CtrlIDFocus")) Then
            sessHelper.SetSession("CtrlIDFocus", txtPermohonanDiskonProgramFleetCustomer)
        End If
        If txtPermohonanDiskonProgramFleetCustomer.Text.Trim = "" Then txtPermohonanDiskonProgramFleetCustomer.Text = 0
        txtPermohonanDiskonProgramFleetCustomer.Text = NotZerroFirstIndexChar(txtPermohonanDiskonProgramFleetCustomer.Text)

        Dim dblNetDealerFinal As Double = 0
        Dim lblNettDealerMargin As Label
        For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
            lblNettDealerMargin = CType(itemData.FindControl(strlblNettDealerMargin), Label)
            If Not IsNothing(lblNettDealerMargin) Then Exit For
        Next
        dblNetDealerFinal = BlankToZerro(lblNettDealerMargin.Text) + BlankToZerro(txtPermohonanDiskonProgramFleetCustomer.Text)

        Dim lblMarginDealerFinal As Label
        For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
            lblMarginDealerFinal = CType(itemData.FindControl(strlblMarginDealerFinal), Label)
            If Not IsNothing(lblMarginDealerFinal) Then Exit For
        Next
        If Not IsNothing(lblMarginDealerFinal) Then
            lblMarginDealerFinal.Text = Format(dblNetDealerFinal, "#,##0")
        End If
        setFocusControl()
    End Sub

    Private Sub setFocusControl()
        Try
            Dim ctrl As System.Web.UI.WebControls.TextBox = CType(sessHelper.GetSession("CtrlIDFocus"), System.Web.UI.WebControls.TextBox)
            ctrl.Focus()
        Catch
            Try
                Dim ctrl As DropDownList = CType(sessHelper.GetSession("CtrlIDFocus"), DropDownList)
                ctrl.Focus()
            Catch
            End Try
        End Try
        sessHelper.SetSession("CtrlIDFocus", Nothing)
    End Sub

    Protected Sub txtBiayaKirimCustomer_TextChanged(sender As Object, e As EventArgs)
        Dim txtBiayaKirimCustomer As System.Web.UI.WebControls.TextBox = DirectCast(sender, System.Web.UI.WebControls.TextBox)
        If IsNothing(sessHelper.GetSession("CtrlIDFocus")) Then
            sessHelper.SetSession("CtrlIDFocus", txtBiayaKirimCustomer)
        End If
        If txtBiayaKirimCustomer.Text.Trim = "" Then txtBiayaKirimCustomer.Text = 0
        txtBiayaKirimCustomer.Text = NotZerroFirstIndexChar(txtBiayaKirimCustomer.Text)

        Dim dblSubTotalBiayaLain As Double = 0
        dblSubTotalBiayaLain += BlankToZerro(txtBiayaKirimCustomer.Text)
        Dim txtAksesoris As System.Web.UI.WebControls.TextBox
        For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
            txtAksesoris = CType(itemData.FindControl(strtxtAksesoris), System.Web.UI.WebControls.TextBox)
            If Not IsNothing(txtAksesoris) Then
                dblSubTotalBiayaLain += BlankToZerro(txtAksesoris.Text)
            End If
        Next

        Dim lblTotalGrossMarginDealer As Label
        Dim lblSubTotalBiayaLainLain As Label
        Dim lblGrossDealerMargin As Label
        Dim txtEstimasiDealPrice As System.Web.UI.WebControls.TextBox
        For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
            If IsNothing(lblTotalGrossMarginDealer) Then
                lblTotalGrossMarginDealer = CType(itemData.FindControl(strlblTotalGrossMarginDealer), Label)
            End If
            If IsNothing(lblSubTotalBiayaLainLain) Then
                lblSubTotalBiayaLainLain = CType(itemData.FindControl(strlblSubTotalBiayaLainlain), Label)
            End If
            If IsNothing(lblGrossDealerMargin) Then
                lblGrossDealerMargin = CType(itemData.FindControl(strlblGrossDealerMargin), Label)
            End If
            If IsNothing(txtEstimasiDealPrice) Then
                txtEstimasiDealPrice = CType(itemData.FindControl(strtxtEstimasiDealPrice), System.Web.UI.WebControls.TextBox)
            End If
            If IsNothing(txtAksesoris) Then
                txtAksesoris = CType(itemData.FindControl(strtxtAksesoris), System.Web.UI.WebControls.TextBox)
            End If
            If Not IsNothing(lblTotalGrossMarginDealer) And Not IsNothing(lblSubTotalBiayaLainLain) And Not IsNothing(lblGrossDealerMargin) _
                And Not IsNothing(txtEstimasiDealPrice) And Not IsNothing(txtAksesoris) Then
                Exit For
            End If
        Next
        If Not IsNothing(lblSubTotalBiayaLainLain) Then
            lblSubTotalBiayaLainLain.Text = Format(dblSubTotalBiayaLain, "#,##0")
        End If
        Dim dblTotalGrossMarginDealer As Double = BlankToZerro(lblTotalGrossMarginDealer.Text)
        Dim dblSubTotalBiayaLainLain As Double = BlankToZerro(lblSubTotalBiayaLainLain.Text)
        If Not IsNothing(lblGrossDealerMargin) Then
            lblGrossDealerMargin.Text = Format(dblTotalGrossMarginDealer - dblSubTotalBiayaLainLain, "#,##0")
        End If
        If Not IsNothing(txtEstimasiDealPrice) Then
            txtEstimasiDealPrice_TextChanged(txtEstimasiDealPrice, Nothing)
            txtAksesoris.Focus()
        End If
    End Sub

    Protected Sub txtAksesoris_TextChanged(sender As Object, e As EventArgs)
        Dim txtAksesoris As System.Web.UI.WebControls.TextBox = DirectCast(sender, System.Web.UI.WebControls.TextBox)
        If IsNothing(sessHelper.GetSession("CtrlIDFocus")) Then
            sessHelper.SetSession("CtrlIDFocus", txtAksesoris)
        End If

        If txtAksesoris.Text.Trim = "" Then txtAksesoris.Text = 0
        txtAksesoris.Text = NotZerroFirstIndexChar(txtAksesoris.Text)

        Dim dblSubTotalBiayaLain As Double = 0
        Dim dblAksesoris As Double = txtAksesoris.Text
        dblSubTotalBiayaLain += dblAksesoris
        Dim dblBiayaKirimCustomer As Double = 0
        Dim txtBiayaKirimCustomer As System.Web.UI.WebControls.TextBox
        For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
            txtBiayaKirimCustomer = CType(itemData.FindControl(strtxtBiayaPengirimanKeCustomer), System.Web.UI.WebControls.TextBox)
            If Not IsNothing(txtBiayaKirimCustomer) Then
                dblBiayaKirimCustomer = NotZerroFirstIndexChar(txtBiayaKirimCustomer.Text)
                dblSubTotalBiayaLain += dblBiayaKirimCustomer
            End If
        Next

        Dim lblTotalGrossMarginDealer As Label
        Dim lblSubTotalBiayaLainLain As Label
        Dim lblGrossDealerMargin As Label
        Dim txtEstimasiDealPrice As System.Web.UI.WebControls.TextBox
        For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
            If IsNothing(lblTotalGrossMarginDealer) Then
                lblTotalGrossMarginDealer = CType(itemData.FindControl(strlblTotalGrossMarginDealer), Label)
            End If
            If IsNothing(lblSubTotalBiayaLainLain) Then
                lblSubTotalBiayaLainLain = CType(itemData.FindControl(strlblSubTotalBiayaLainlain), Label)
            End If
            If IsNothing(lblGrossDealerMargin) Then
                lblGrossDealerMargin = CType(itemData.FindControl(strlblGrossDealerMargin), Label)
            End If
            If IsNothing(txtEstimasiDealPrice) Then
                txtEstimasiDealPrice = CType(itemData.FindControl(strtxtEstimasiDealPrice), System.Web.UI.WebControls.TextBox)
            End If
            If Not IsNothing(lblTotalGrossMarginDealer) And Not IsNothing(lblSubTotalBiayaLainLain) And Not IsNothing(lblGrossDealerMargin) _
                And Not IsNothing(txtEstimasiDealPrice) Then
                Exit For
            End If
        Next
        If Not IsNothing(lblSubTotalBiayaLainLain) Then
            lblSubTotalBiayaLainLain.Text = Format(dblSubTotalBiayaLain, "#,##0")
        End If
        Dim dblTotalGrossMarginDealer As Double = BlankToZerro(lblTotalGrossMarginDealer.Text)
        Dim dblSubTotalBiayaLainLain As Double = BlankToZerro(lblSubTotalBiayaLainLain.Text)
        If Not IsNothing(lblGrossDealerMargin) Then
            lblGrossDealerMargin.Text = Format(dblTotalGrossMarginDealer - dblSubTotalBiayaLainLain, "#,##0")
        End If
        If Not IsNothing(txtEstimasiDealPrice) Then
            txtEstimasiDealPrice_TextChanged(txtEstimasiDealPrice, Nothing)
            txtEstimasiDealPrice.Focus()
        End If
    End Sub

    Function NotZerroFirstIndexChar(ByVal ctrlVal As String) As String
        Dim result As String = "0"
        If Len(ctrlVal.Trim) > 0 Then
            ctrlVal = Replace(Replace(ctrlVal.Trim, ".", ""), ",", "")
            result = ctrlVal
            For i As Integer = 0 To ctrlVal.Length - 1
                If Left(ctrlVal.Trim, i + 1) = "0" Then
                    result = Mid(result.Trim, i + 2, Len(result.Trim) - 1)
                Else
                    Exit For
                End If
            Next
        End If

        If result.Trim = "" Then result = "0"
        result = Format(CDbl(result), "#,##0")
        Return result
    End Function

    Protected Sub ddlAssyYear_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim ddlAssyYear As DropDownList = DirectCast(sender, DropDownList)

        Dim ddlTipe As DropDownList
        Dim ddlModelYear As DropDownList
        Dim ddlWarna As DropDownList
        If IsNothing(sessHelper.GetSession("CtrlIDFocus")) Then
            sessHelper.SetSession("CtrlIDFocus", ddlAssyYear)
        End If

        Dim gridRow As GridViewRow = ddlAssyYear.Parent.Parent
        For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
            If IsNothing(ddlTipe) Then
                ddlTipe = CType(itemData.FindControl(strddlTipe), DropDownList)
            End If
            If IsNothing(ddlModelYear) Then
                ddlModelYear = CType(itemData.FindControl(strddlModelYear), DropDownList)
            End If
            If IsNothing(ddlWarna) Then
                ddlWarna = CType(itemData.FindControl(strddlWarna), DropDownList)
            End If
            If Not IsNothing(ddlTipe) AndAlso Not IsNothing(ddlModelYear) AndAlso Not IsNothing(ddlWarna) Then
                Exit For
            End If
        Next
        If Not IsNothing(ddlTipe) AndAlso Not IsNothing(ddlAssyYear) AndAlso Not IsNothing(ddlModelYear) AndAlso Not IsNothing(ddlWarna) Then
            BindModelYear(ddlTipe, ddlAssyYear, ddlModelYear)
            ddlModelYear_SelectedIndexChanged(ddlModelYear, Nothing)
        End If

        If Not IsNothing(ddlTipe) AndAlso Not IsNothing(ddlAssyYear) AndAlso Not IsNothing(ddlModelYear) Then
            CalculateTotalProgramRegulerMMKSI(ddlTipe, ddlAssyYear, ddlModelYear)
            If Not IsNothing(ddlModelYear) Then
                ddlAssyYear.Focus()
            End If
        End If
    End Sub

    Protected Sub ddlModelYear_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim ddlModelYear As DropDownList = DirectCast(sender, DropDownList)

        Dim ddlTipe As DropDownList
        Dim ddlAssyYear As DropDownList
        Dim ddlWarna As DropDownList

        Dim gridRow As GridViewRow = ddlModelYear.Parent.Parent
        For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
            If IsNothing(ddlTipe) Then
                ddlTipe = CType(itemData.FindControl(strddlTipe), DropDownList)
            End If
            If IsNothing(ddlAssyYear) Then
                ddlAssyYear = CType(itemData.FindControl(strddlAssyYear), DropDownList)
            End If
            If IsNothing(ddlWarna) Then
                ddlWarna = CType(itemData.FindControl(strddlWarna), DropDownList)
            End If
            If Not IsNothing(ddlTipe) AndAlso Not IsNothing(ddlWarna) AndAlso Not IsNothing(ddlAssyYear) Then
                Exit For
            End If
        Next
        If Not IsNothing(ddlTipe) AndAlso Not IsNothing(ddlAssyYear) AndAlso Not IsNothing(ddlModelYear) AndAlso Not IsNothing(ddlWarna) Then
            BindVehicleColor(ddlTipe, ddlAssyYear, ddlModelYear, ddlWarna)
            ddlWarna_SelectedIndexChanged(ddlWarna, Nothing)
        End If

        If Not IsNothing(ddlTipe) AndAlso Not IsNothing(ddlAssyYear) AndAlso Not IsNothing(ddlModelYear) Then
            CalculateTotalProgramRegulerMMKSI(ddlTipe, ddlAssyYear, ddlModelYear)
            If Not IsNothing(ddlModelYear) Then
                ddlModelYear.Focus()
            End If
        End If
    End Sub

    Protected Sub txtBBN_TextChanged(sender As Object, e As EventArgs)
        Dim txtBBN As System.Web.UI.WebControls.TextBox = DirectCast(sender, System.Web.UI.WebControls.TextBox)
        If IsNothing(sessHelper.GetSession("CtrlIDFocus")) Then
            sessHelper.SetSession("CtrlIDFocus", txtBBN)
        End If

        If txtBBN.Text.Trim = "" Then txtBBN.Text = 0
        txtBBN.Text = NotZerroFirstIndexChar(txtBBN.Text)

        Dim dblSubTotalCostDealer As Double = 0
        Dim lblHarga_Tebus As Label
        Dim lblLogistic_Cost As Label
        Dim txtBiayaBiroJasa As System.Web.UI.WebControls.TextBox

        dblSubTotalCostDealer += BlankToZerro(txtBBN.Text)
        For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
            lblHarga_Tebus = CType(itemData.FindControl(strlblHargaTebus), Label)
            If Not IsNothing(lblHarga_Tebus) Then
                dblSubTotalCostDealer += BlankToZerro(lblHarga_Tebus.Text)
            End If
            lblLogistic_Cost = CType(itemData.FindControl(strlblLogisticCost), Label)
            If Not IsNothing(lblLogistic_Cost) Then
                dblSubTotalCostDealer += BlankToZerro(lblLogistic_Cost.Text)
            End If
            txtBiayaBiroJasa = CType(itemData.FindControl(strtxtBiayaBiroJasa), System.Web.UI.WebControls.TextBox)
            If Not IsNothing(txtBiayaBiroJasa) Then
                dblSubTotalCostDealer += BlankToZerro(txtBiayaBiroJasa.Text)
            End If
            If Not IsNothing(lblHarga_Tebus) And Not IsNothing(lblLogistic_Cost) And Not IsNothing(txtBiayaBiroJasa) Then
                Exit For
            End If
        Next

        Dim lblSubTotalCostDealer As Label
        Dim txtRetailPriceOnTheRoad As System.Web.UI.WebControls.TextBox
        For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
            If Not IsNothing(CType(itemData.FindControl(strlblSubTotalCostDealer), Label)) Then
                lblSubTotalCostDealer = CType(itemData.FindControl(strlblSubTotalCostDealer), Label)
            End If
            If Not IsNothing(CType(itemData.FindControl(strtxtRetailPriceOnTheRoad), System.Web.UI.WebControls.TextBox)) Then
                txtRetailPriceOnTheRoad = CType(itemData.FindControl(strtxtRetailPriceOnTheRoad), System.Web.UI.WebControls.TextBox)
            End If
            If IsNothing(txtBiayaBiroJasa) Then
                txtBiayaBiroJasa = CType(itemData.FindControl(strtxtBiayaBiroJasa), System.Web.UI.WebControls.TextBox)
            End If
            If Not IsNothing(lblSubTotalCostDealer) And Not IsNothing(txtRetailPriceOnTheRoad) And Not IsNothing(txtBiayaBiroJasa) Then Exit For
        Next

        If Not IsNothing(lblSubTotalCostDealer) Then
            lblSubTotalCostDealer.Text = Format(dblSubTotalCostDealer, "#,##0")
        End If
        If Not IsNothing(txtRetailPriceOnTheRoad) Then
            txtRetailPriceOnTheRoad_TextChanged(txtRetailPriceOnTheRoad, Nothing)
        End If
        If Not IsNothing(txtBiayaBiroJasa) Then
            txtBiayaBiroJasa.Focus()
        End If
    End Sub

    Protected Sub txtBiayaBiroJasa_TextChanged(sender As Object, e As EventArgs)
        Dim txtBiayaBiroJasa As System.Web.UI.WebControls.TextBox = DirectCast(sender, System.Web.UI.WebControls.TextBox)
        If IsNothing(sessHelper.GetSession("CtrlIDFocus")) Then
            sessHelper.SetSession("CtrlIDFocus", txtBiayaBiroJasa)
        End If

        If txtBiayaBiroJasa.Text.Trim = "" Then txtBiayaBiroJasa.Text = 0
        txtBiayaBiroJasa.Text = NotZerroFirstIndexChar(txtBiayaBiroJasa.Text)

        Dim dblSubTotalCostDealer As Double = 0
        Dim lblHarga_Tebus As Label
        Dim lblLogistic_Cost As Label
        Dim txtBBN As System.Web.UI.WebControls.TextBox

        dblSubTotalCostDealer += BlankToZerro(txtBiayaBiroJasa.Text)
        For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
            lblHarga_Tebus = CType(itemData.FindControl(strlblHargaTebus), Label)
            If Not IsNothing(lblHarga_Tebus) Then
                dblSubTotalCostDealer += BlankToZerro(lblHarga_Tebus.Text)
            End If
            lblLogistic_Cost = CType(itemData.FindControl(strlblLogisticCost), Label)
            If Not IsNothing(lblLogistic_Cost) Then
                dblSubTotalCostDealer += BlankToZerro(lblLogistic_Cost.Text)
            End If
            txtBBN = CType(itemData.FindControl(strtxtBBN), System.Web.UI.WebControls.TextBox)
            If Not IsNothing(txtBBN) Then
                dblSubTotalCostDealer += BlankToZerro(txtBBN.Text)
            End If
            If Not IsNothing(lblHarga_Tebus) And Not IsNothing(lblLogistic_Cost) And Not IsNothing(txtBBN) Then
                Exit For
            End If
        Next

        Dim lblSubTotalCostDealer As Label
        Dim txtRetailPriceOnTheRoad As System.Web.UI.WebControls.TextBox
        For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
            If Not IsNothing(CType(itemData.FindControl(strlblSubTotalCostDealer), Label)) Then
                lblSubTotalCostDealer = CType(itemData.FindControl(strlblSubTotalCostDealer), Label)
            End If
            If Not IsNothing(CType(itemData.FindControl(strtxtRetailPriceOnTheRoad), System.Web.UI.WebControls.TextBox)) Then
                txtRetailPriceOnTheRoad = CType(itemData.FindControl(strtxtRetailPriceOnTheRoad), System.Web.UI.WebControls.TextBox)
            End If
            If Not IsNothing(lblSubTotalCostDealer) And Not IsNothing(txtRetailPriceOnTheRoad) Then
                Exit For
            End If
        Next
        If Not IsNothing(lblSubTotalCostDealer) Then
            lblSubTotalCostDealer.Text = Format(dblSubTotalCostDealer, "#,##0")
        End If
        If Not IsNothing(txtRetailPriceOnTheRoad) Then
            txtRetailPriceOnTheRoad_TextChanged(txtRetailPriceOnTheRoad, Nothing)
            txtRetailPriceOnTheRoad.Focus()
        End If
    End Sub

    Protected Sub txtRetailPriceOnTheRoad_TextChanged(sender As Object, e As EventArgs)
        Dim txtRetailPriceOnTheRoad As System.Web.UI.WebControls.TextBox = DirectCast(sender, System.Web.UI.WebControls.TextBox)
        If IsNothing(sessHelper.GetSession("CtrlIDFocus")) Then
            sessHelper.SetSession("CtrlIDFocus", txtRetailPriceOnTheRoad)
        End If
        If txtRetailPriceOnTheRoad.Text.Trim = "" Then txtRetailPriceOnTheRoad.Text = 0
        txtRetailPriceOnTheRoad.Text = NotZerroFirstIndexChar(txtRetailPriceOnTheRoad.Text)

        Dim lblSubTotalCostDealer As Label
        For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
            lblSubTotalCostDealer = CType(itemData.FindControl(strlblSubTotalCostDealer), Label)
            If Not IsNothing(lblSubTotalCostDealer) Then Exit For
        Next

        Dim dblRetailPriceOnTheRoad As Double = txtRetailPriceOnTheRoad.Text
        Dim dblMarginDealerGross As Double = dblRetailPriceOnTheRoad - NotZerroFirstIndexChar(lblSubTotalCostDealer.Text)
        Dim lblMarginDealerGross As Label
        For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
            lblMarginDealerGross = CType(itemData.FindControl(strlblMarginDealerGross), Label)
            If Not IsNothing(lblMarginDealerGross) Then Exit For
        Next
        If Not IsNothing(lblMarginDealerGross) Then
            lblMarginDealerGross.Text = Format(dblMarginDealerGross, "#,##0")
        End If

        'Sub_Total_Program_Reguler_MMKSI
        ''========================================================================================================
        Dim dblSubTotalProgramRegulerMMKSI As Double = 0
        Dim lblSubTotalProgramRegulerMMKSI As Label
        For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
            lblSubTotalProgramRegulerMMKSI = CType(itemData.FindControl(strlblSubTotalProgramRegulerMMKSI), Label)
            If Not IsNothing(lblSubTotalProgramRegulerMMKSI) Then
                dblSubTotalProgramRegulerMMKSI = BlankToZerro(lblSubTotalProgramRegulerMMKSI.Text)
                Exit For
            End If
        Next

        'Total_Gross_Margin_Dealer_(Termasuk_Sales_Program)
        ''========================================================================================================
        Dim lblTotalGrossMarginDealer As Label
        Dim dblTotalGrossMarginDealer As Double = dblMarginDealerGross + dblSubTotalProgramRegulerMMKSI
        For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
            lblTotalGrossMarginDealer = CType(itemData.FindControl(strlblTotalGrossMarginDealer), Label)
            If Not IsNothing(lblTotalGrossMarginDealer) Then Exit For
        Next
        If Not IsNothing(lblTotalGrossMarginDealer) Then
            lblTotalGrossMarginDealer.Text = Format(dblTotalGrossMarginDealer, "#,##0")
        End If

        Dim dblSubTotalBiayaLainlain As Double = 0
        Dim lblGrossDealerMargin As Label
        Dim lblSubTotalBiayaLainLain As Label
        Dim txtEstimasiDealPrice As System.Web.UI.WebControls.TextBox
        Dim txtBiayaPengirimanKeCustomer As System.Web.UI.WebControls.TextBox
        For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
            If IsNothing(lblTotalGrossMarginDealer) Then
                lblTotalGrossMarginDealer = CType(itemData.FindControl(strlblTotalGrossMarginDealer), Label)
            End If
            If IsNothing(lblSubTotalBiayaLainLain) Then
                lblSubTotalBiayaLainLain = CType(itemData.FindControl(strlblSubTotalBiayaLainlain), Label)
            End If
            If IsNothing(lblGrossDealerMargin) Then
                lblGrossDealerMargin = CType(itemData.FindControl(strlblGrossDealerMargin), Label)
            End If
            If IsNothing(txtEstimasiDealPrice) Then
                txtEstimasiDealPrice = CType(itemData.FindControl(strtxtEstimasiDealPrice), System.Web.UI.WebControls.TextBox)
            End If
            If IsNothing(txtBiayaPengirimanKeCustomer) Then
                txtBiayaPengirimanKeCustomer = CType(itemData.FindControl(strtxtBiayaPengirimanKeCustomer), System.Web.UI.WebControls.TextBox)
            End If
            If Not IsNothing(lblTotalGrossMarginDealer) And Not IsNothing(lblSubTotalBiayaLainLain) And Not IsNothing(lblGrossDealerMargin) _
                And Not IsNothing(txtEstimasiDealPrice) And Not IsNothing(txtBiayaPengirimanKeCustomer) Then
                Exit For
            End If
        Next
        If Not IsNothing(lblTotalGrossMarginDealer) Then
            dblTotalGrossMarginDealer = BlankToZerro(lblTotalGrossMarginDealer.Text)
        End If
        If Not IsNothing(lblSubTotalBiayaLainLain) Then
            dblSubTotalBiayaLainlain = BlankToZerro(lblSubTotalBiayaLainLain.Text)
        End If
        If Not IsNothing(lblGrossDealerMargin) Then
            lblGrossDealerMargin.Text = Format(dblTotalGrossMarginDealer - dblSubTotalBiayaLainlain, "#,##0")
        End If
        If Not IsNothing(txtEstimasiDealPrice) Then
            txtEstimasiDealPrice_TextChanged(txtEstimasiDealPrice, Nothing)
            txtBiayaPengirimanKeCustomer.Focus()
        End If
    End Sub

    Protected Sub ddlModel_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim ddlModel As DropDownList = DirectCast(sender, DropDownList)
        Dim gridRow As GridViewRow = ddlModel.Parent.Parent
        Dim ddlTipe As DropDownList
        Dim ddlWarna As DropDownList
        Dim ddlAssyYear As DropDownList
        Dim ddlModelYear As DropDownList
        For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
            If IsNothing(ddlTipe) Then
                ddlTipe = CType(itemData.FindControl(strddlTipe), DropDownList)
            End If
            If Not IsNothing(ddlTipe) Then Exit For
        Next
        If Not IsNothing(ddlTipe) Then
            BindVehicleTypeGeneral(ddlModel, ddlTipe)
            ddlTipe_SelectedIndexChanged(ddlTipe, Nothing)
        End If
    End Sub

    Public Sub ddlWarna_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ddlWarna As DropDownList = DirectCast(sender, DropDownList)
        Dim dblBasePrice As Double = 0
        If ddlWarna.SelectedIndex > 0 Then
            Dim dblLogisticPrice As Double = 0
            If ddlDeliveryRegionCode.SelectedIndex > 0 Then
                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LogisticPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(LogisticPrice), "RegionCode", MatchType.Exact, ddlDeliveryRegionCode.SelectedValue))
                Dim strSQL As String = "Select SAPModel from VechileType where RowStatus = 0 and ID = (Select top 1 VechileTypeID from VechileColor where RowStatus = 0 and ID = " & ddlWarna.SelectedValue & ")"
                crit.opAnd(New Criteria(GetType(LogisticPrice), "SAPModel", MatchType.InSet, "(" & strSQL & ")"))
                Dim sortColls As SortCollection = New SortCollection
                sortColls.Add(New Sort(GetType(LogisticPrice), "EffectiveDate", Sort.SortDirection.DESC))  '-- Sort by Vechile type code
                Dim arlLogisticPrice As ArrayList = New LogisticPriceFacade(User).RetrieveByCriteria(crit, sortColls)
                Dim objLogisticPrice As LogisticPrice = New LogisticPrice
                If Not IsNothing(arlLogisticPrice) AndAlso arlLogisticPrice.Count > 0 Then
                    objLogisticPrice = CType(arlLogisticPrice(0), LogisticPrice)
                End If
                dblLogisticPrice = objLogisticPrice.LogisticPrice + (objLogisticPrice.LogisticPrice * (objLogisticPrice.PPn / 100))
            End If
            Dim lblLogisticCost As Label
            For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
                lblLogisticCost = CType(itemData.FindControl(strlblLogisticCost), Label)
                If Not IsNothing(lblLogisticCost) Then Exit For
            Next
            If Not IsNothing(lblLogisticCost) Then
                lblLogisticCost.Text = Format(dblLogisticPrice, "#,##0")
            End If

            Dim ValidFrom As Date = New DateTime(Now.Year, Now.Month, Now.Day)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Price), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Price), "Dealer.ID", MatchType.Exact, objDealer.ID))
            criterias.opAnd(New Criteria(GetType(Price), "ValidFrom", MatchType.LesserOrEqual, ValidFrom))
            criterias.opAnd(New Criteria(GetType(Price), "VechileColor.ID", MatchType.Exact, ddlWarna.SelectedValue))
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(Price), "ValidFrom", Sort.SortDirection.DESC))
            Dim arlPrice As ArrayList = New PriceFacade(User).RetrieveByCriteria(criterias, sortColl)
            Dim objPrice As Price = New Price
            If Not IsNothing(arlPrice) AndAlso arlPrice.Count > 0 Then
                objPrice = CType(arlPrice(0), Price)
                'dblBasePrice = objPrice.BasePrice + (objPrice.BasePrice * (objPrice.PPN / 100))
                dblBasePrice = objPrice.VehiclePrice + objPrice.PPh22Amount
            End If
        End If
        Dim lblHargaTebus As Label
        For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
            lblHargaTebus = CType(itemData.FindControl(strlblHargaTebus), Label)
            If Not IsNothing(lblHargaTebus) Then Exit For
        Next
        If Not IsNothing(lblHargaTebus) Then
            lblHargaTebus.Text = Format(dblBasePrice, "#,##0")
        End If
    End Sub

    Private Function GetDiscountFromProgramReguler(ByVal _parameterName As String, ByVal _vechileTypeGeneralID As Integer, ByVal _assyYear As String, ByVal _modelYear As String) As Double
        Dim dblDiscountAmount As Double = 0
        If _parameterName = "" OrElse _vechileTypeGeneralID = 0 OrElse _assyYear = "" OrElse _modelYear = "" Then
            Return dblDiscountAmount
        End If
        Dim objDiscountProposalProgramReguler As New DiscountProposalProgramReguler
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalProgramReguler), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(DiscountProposalProgramReguler), "DiscountProposalParameter.ParameterName", MatchType.Exact, _parameterName.Replace("&nbsp;", "")))
        criterias.opAnd(New Criteria(GetType(DiscountProposalProgramReguler), "VechileTypeGeneral.ID", MatchType.Exact, _vechileTypeGeneralID))
        criterias.opAnd(New Criteria(GetType(DiscountProposalProgramReguler), "AssyYear", MatchType.Exact, _assyYear))
        criterias.opAnd(New Criteria(GetType(DiscountProposalProgramReguler), "ModelYear", MatchType.Exact, _modelYear))

        Dim currentDate As DateTime = DateAdd(DateInterval.Day, 1, CDate(hdnSubmitDate.Value.Trim))
        criterias.opAnd(New Criteria(GetType(DiscountProposalProgramReguler), "ValidFrom", MatchType.LesserOrEqual, currentDate))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(DiscountProposalProgramReguler), "ValidFrom", Sort.SortDirection.DESC))
        Dim arlDiscountProposalProgramReguler As ArrayList = New DiscountProposalProgramRegulerFacade(User).RetrieveByCriteria(criterias, sortColl)
        If Not IsNothing(arlDiscountProposalProgramReguler) AndAlso arlDiscountProposalProgramReguler.Count > 0 Then
            objDiscountProposalProgramReguler = CType(arlDiscountProposalProgramReguler(0), DiscountProposalProgramReguler)
        End If
        dblDiscountAmount = objDiscountProposalProgramReguler.DiscountAmount
        Return dblDiscountAmount
    End Function

    Public Sub ddlTipe_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ddlTipe As DropDownList = DirectCast(sender, DropDownList)
        Dim ddlAssyYear As DropDownList
        Dim ddlModelYear As DropDownList
        If IsNothing(sessHelper.GetSession("CtrlIDFocus")) Then
            sessHelper.SetSession("CtrlIDFocus", ddlTipe)
        End If

        For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
            If IsNothing(ddlAssyYear) Then
                ddlAssyYear = CType(itemData.FindControl(strddlAssyYear), DropDownList)
            End If
            If IsNothing(ddlModelYear) Then
                ddlModelYear = CType(itemData.FindControl(strddlModelYear), DropDownList)
            End If
            If Not IsNothing(ddlAssyYear) AndAlso Not IsNothing(ddlModelYear) Then Exit For
        Next
        If Not IsNothing(ddlTipe) AndAlso Not IsNothing(ddlAssyYear) Then
            BindAssyYear(ddlTipe, ddlAssyYear)
            ddlAssyYear_SelectedIndexChanged(ddlAssyYear, Nothing)
        End If

        If Not IsNothing(ddlTipe) AndAlso Not IsNothing(ddlAssyYear) AndAlso Not IsNothing(ddlModelYear) Then
            CalculateTotalProgramRegulerMMKSI(ddlTipe, ddlAssyYear, ddlModelYear)
            If Not IsNothing(ddlTipe) Then
                ddlTipe.Focus()
            End If
        End If
    End Sub

    Private Function CalculateTotalProgramRegulerMMKSI(ByVal ddlTipe As DropDownList, ByVal ddlAssyYear As DropDownList, ByVal ddlModelYear As DropDownList)
        Dim dblSubTotalProgramRegulerMMKSI As Double = 0
        Dim objDiscountProposalPricetoParameter As DiscountProposalPricetoParameter
        Dim arlDPParameter As ArrayList = sessHelper.GetSession(sessDiscountProposalParameter)
        For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
            If Not IsNothing(arlDPParameter) AndAlso arlDPParameter.Count > 0 Then
                For Each _objDPParameter As DiscountProposalParameter In arlDPParameter
                    Dim dblDiscountAmount As Double = 0
                    Dim strlblJudulParam As String = "hdn" & _objDPParameter.ParameterName.Replace(" ", "")
                    Dim strlblParam As String = "lbl" & _objDPParameter.ParameterName.Replace(" ", "")
                    Dim lblJudulParam As HiddenField = CType(itemData.FindControl(strlblJudulParam), HiddenField)
                    Dim lblParamProgramRegulerMMKSI As Label = CType(itemData.FindControl(strlblParam), Label)
                    If Not IsNothing(lblJudulParam) AndAlso Not IsNothing(lblParamProgramRegulerMMKSI) Then
                        If ddlTipe.SelectedIndex > 0 Then
                            dblDiscountAmount = GetDiscountFromProgramReguler(lblJudulParam.Value, ddlTipe.SelectedValue, ddlAssyYear.SelectedValue, ddlModelYear.SelectedValue)
                            dblSubTotalProgramRegulerMMKSI += dblDiscountAmount
                        End If
                        lblParamProgramRegulerMMKSI.Text = Format(dblDiscountAmount, "#,##0")
                        Exit For
                    End If
                Next
            End If
        Next

        ''========================================================================================================
        'Sub_Total_Program_Reguler_MMKSI_(D)
        ''========================================================================================================
        Dim lblSubTotalProgramRegulerMMKSI As Label
        For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
            lblSubTotalProgramRegulerMMKSI = CType(itemData.FindControl(strlblSubTotalProgramRegulerMMKSI), Label)
            If Not IsNothing(lblSubTotalProgramRegulerMMKSI) Then Exit For
        Next
        If Not IsNothing(lblSubTotalProgramRegulerMMKSI) Then
            lblSubTotalProgramRegulerMMKSI.Text = Format(dblSubTotalProgramRegulerMMKSI, "#,##0")
        End If

        ''========================================================================================================
        'Total_Gross_Margin_Dealer_(Termasuk_Sales_Program)_(E=C+D)
        ''========================================================================================================
        Dim lblMarginDealerGross As Label
        Dim dblMarginDealerGross As Double = 0
        For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
            lblMarginDealerGross = CType(itemData.FindControl(strlblMarginDealerGross), Label)
            If Not IsNothing(lblMarginDealerGross) Then
                dblMarginDealerGross = BlankToZerro(lblMarginDealerGross.Text)
                Exit For
            End If
        Next
        Dim lblTotalGrossMarginDealer As Label
        Dim dblTotalGrossMarginDealer As Double = dblMarginDealerGross + dblSubTotalProgramRegulerMMKSI
        For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
            lblTotalGrossMarginDealer = CType(itemData.FindControl(strlblTotalGrossMarginDealer), Label)
            If Not IsNothing(lblTotalGrossMarginDealer) Then Exit For
        Next
        If Not IsNothing(lblTotalGrossMarginDealer) Then
            lblTotalGrossMarginDealer.Text = Format(dblTotalGrossMarginDealer, "#,##0")
        End If

        ''========================================================================================================
        Dim dblSubTotalBiayaLainlain As Double = 0
        Dim lblGrossDealerMargin As Label
        Dim lblSubTotalBiayaLainLain As Label
        Dim txtEstimasiDealPrice As System.Web.UI.WebControls.TextBox
        For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
            If IsNothing(lblSubTotalBiayaLainLain) Then
                lblSubTotalBiayaLainLain = CType(itemData.FindControl(strlblSubTotalBiayaLainlain), Label)
            End If
            If IsNothing(lblGrossDealerMargin) Then
                lblGrossDealerMargin = CType(itemData.FindControl(strlblGrossDealerMargin), Label)
            End If
            If IsNothing(txtEstimasiDealPrice) Then
                txtEstimasiDealPrice = CType(itemData.FindControl(strtxtEstimasiDealPrice), System.Web.UI.WebControls.TextBox)
            End If
            If Not IsNothing(lblSubTotalBiayaLainLain) And Not IsNothing(lblGrossDealerMargin) And Not IsNothing(txtEstimasiDealPrice) Then
                Exit For
            End If
        Next
        If Not IsNothing(lblSubTotalBiayaLainLain) Then
            dblSubTotalBiayaLainlain = BlankToZerro(lblSubTotalBiayaLainLain.Text)
        End If
        If Not IsNothing(lblGrossDealerMargin) Then
            lblGrossDealerMargin.Text = Format(dblTotalGrossMarginDealer - dblSubTotalBiayaLainlain, "#,##0")
        End If
        If Not IsNothing(txtEstimasiDealPrice) Then
            txtEstimasiDealPrice_TextChanged(txtEstimasiDealPrice, Nothing)
        End If
    End Function

    Private Sub BindVehicleTypeGeneral(ByVal ddlModel As DropDownList, ByRef ddlTipe As DropDownList)
        ddlTipe.Items.Clear()
        ddlTipe.Items.Add(New ListItem("Silahkan Pilih", ""))
        If ddlModel.SelectedIndex > 0 Then
            '-- Vehicle criteria & sort
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileTypeGeneral), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VechileTypeGeneral), "Status", MatchType.Exact, 1))
            criterias.opAnd(New Criteria(GetType(VechileTypeGeneral), "SubCategoryVehicle.ID", MatchType.Exact, ddlModel.SelectedValue))

            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(VechileTypeGeneral), "Name", Sort.SortDirection.ASC))  '-- Sort by Vechile type code
            '-- Bind Vehicle type dropdownlist
            Dim arlVTG As ArrayList = New VechileTypeGeneralFacade(User).RetrieveByCriteria(criterias, sortColl)
            For Each oVTG As VechileTypeGeneral In arlVTG
                ddlTipe.Items.Add(New ListItem(oVTG.Name, oVTG.ID))
            Next
            If hdnVechileTypeID.Value.Trim <> "" Then
                ddlTipe.SelectedValue = hdnVechileTypeID.Value
            End If
        End If
    End Sub

    Private Sub BindAssyYear(ByVal ddlTipe As DropDownList, ByVal ddlAssyYear As DropDownList)
        ddlAssyYear.Items.Clear()
        ddlAssyYear.Items.Add(New ListItem("Silahkan Pilih", ""))
        If ddlTipe.SelectedIndex > 0 Then
            Dim intDDLTipeSelectedValue As Integer = 0
            If Not IsNothing(ddlTipe) Then
                If ddlTipe.SelectedValue <> "" Then
                    intDDLTipeSelectedValue = ddlTipe.SelectedValue
                End If
            End If
            Dim arlVC As ArrayList = New VechileColorIsActiveOnPKFacade(User).RetrieveAssyYearByVechileTypeGeneralID(CStr(intDDLTipeSelectedValue))
            For Each oVC As VechileColorIsActiveOnPK In arlVC
                ddlAssyYear.Items.Add(New ListItem(oVC.ProductionYear, oVC.ProductionYear))
            Next
            If hdnAssyYear.Value.Trim <> "" Then
                ddlAssyYear.SelectedValue = hdnAssyYear.Value.Trim
            End If
        End If
    End Sub

    Private Sub BindModelYear(ByVal ddlTipe As DropDownList, ByVal ddlAssyYear As DropDownList, ByVal ddlModelYear As DropDownList)
        ddlModelYear.Items.Clear()
        ddlModelYear.Items.Add(New ListItem("Silahkan Pilih", ""))
        If ddlTipe.SelectedIndex > 0 AndAlso ddlAssyYear.SelectedIndex > 0 Then
            Dim intDDLTipeSelectedValue As Integer = 0
            If Not IsNothing(ddlTipe) Then
                If ddlTipe.SelectedValue <> "" Then
                    intDDLTipeSelectedValue = ddlTipe.SelectedValue
                End If
            End If
            Dim intDDLAssyYearSelectedValue As Integer = 0
            If Not IsNothing(ddlAssyYear) Then
                If ddlAssyYear.SelectedValue <> "" Then
                    intDDLAssyYearSelectedValue = ddlAssyYear.SelectedValue
                End If
            End If
            Dim arlVC As ArrayList = New VechileColorIsActiveOnPKFacade(User).RetrieverModelYearByVechileTypeGeneralIDAndAssyYear(CStr(intDDLTipeSelectedValue), CStr(intDDLAssyYearSelectedValue))
            For Each oVC As VechileColorIsActiveOnPK In arlVC
                ddlModelYear.Items.Add(New ListItem(oVC.ModelYear, oVC.ModelYear))
            Next
            If hdnModelYear.Value.Trim <> "" Then
                ddlModelYear.SelectedValue = hdnModelYear.Value.Trim
            End If
        End If
    End Sub

    Private Sub BindVehicleColor(ByVal ddlType As DropDownList, ByVal ddlAssyYear As DropDownList, ByVal ddlModelYear As DropDownList, ByRef ddlColor As DropDownList)
        ddlColor.Items.Clear()
        ddlColor.Items.Add(New ListItem("Silahkan Pilih", ""))
        If ddlType.SelectedIndex > 0 Then
            '-- Vehicle criteria & sort
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim strSQL As String = ""
            Dim strDDLTypeSelectedValue As Integer = 0
            If Not IsNothing(strDDLTypeSelectedValue) Then
                If ddlType.SelectedValue <> "" Then
                    strDDLTypeSelectedValue = ddlType.SelectedValue
                End If
            End If
            strSQL = "Select VehicleColorID "
            strSQL += " from VechileColorIsActiveOnPK "
            strSQL += " Where VechileTypeGeneralID = '" & strDDLTypeSelectedValue & "'"
            strSQL += " And ProductionYear = '" & ddlAssyYear.SelectedValue & "'"
            strSQL += " And ModelYear = '" & ddlModelYear.SelectedValue & "'"
            'strSQL += " AND Status = 1"
            criterias.opAnd(New Criteria(GetType(VechileColor), "ID", MatchType.InSet, "(" & strSQL & ")"))

            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(VechileColor), "ColorCode", Sort.SortDirection.ASC))  '-- Sort by Vechile type code

            '-- Bind Vehicle type dropdownlist
            Dim arlVC As ArrayList = New VechileColorFacade(User).RetrieveByCriteria(criterias, sortColl)
            For Each oVC As VechileColor In arlVC
                ddlColor.Items.Add(New ListItem(oVC.MaterialNumber & " (" & oVC.ColorIndName & ")", oVC.ID))
            Next
            If hdnVechileColorID.Value.Trim <> "" Then
                ddlColor.SelectedValue = hdnVechileColorID.Value
            End If
        End If
    End Sub

    Private Sub dgRincianHargaKendaraan_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles dgRincianHargaKendaraan.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            'For first column set to 200 px
            Dim cell As TableCell = e.Row.Cells(0)
            cell.Width = New Unit("150px")
            For i As Integer = 1 To e.Row.Cells.Count - 1
                'Mind that i used i=1 not 0 because the width of cells(0) has already been set
                Dim cell2 As TableCell = e.Row.Cells(i)
                cell2.Width = New Unit("150px")
            Next
        End If
    End Sub

    Private Sub btnDeleteColRincianHrg_Click(sender As Object, e As EventArgs)
        Try
            arlDiscountProposalDtl = CType(sessHelper.GetSession(sessDiscountProposalDtl), ArrayList)
            If IsNothing(arlDiscountProposalDtl) Then arlDiscountProposalDtl = New ArrayList()
            If arlDiscountProposalDtl.Count = 0 Then Return

            arlDiscountProposalDtlPrice = CType(sessHelper.GetSession(sessDiscountProposalDtlPrice), ArrayList)
            If IsNothing(arlDiscountProposalDtlPrice) Then arlDiscountProposalDtlPrice = New ArrayList()
            If arlDiscountProposalDtlPrice.Count = 0 Then Return

            arlDiscountProposalPricetoParameter = CType(sessHelper.GetSession(sessDiscountProposalPricetoParameter), ArrayList)
            If IsNothing(arlDiscountProposalPricetoParameter) Then arlDiscountProposalPricetoParameter = New ArrayList()

            Dim btnDeleteColRincianHrg As Button = DirectCast(sender, Button)
            Dim colIndex As Integer = Right(btnDeleteColRincianHrg.ID, Len(btnDeleteColRincianHrg.ID) - Len("btnDeleteColRincianHrg"))
            Dim objDiscountProposalDtl As DiscountProposalDetail = CType(arlDiscountProposalDtl(colIndex), DiscountProposalDetail)
            Dim objDiscountProposalDtlPrice As DiscountProposalDetailPrice = CType(arlDiscountProposalDtlPrice(colIndex), DiscountProposalDetailPrice)

            If objDiscountProposalDtl.ID > 0 Then
                Dim arrDelete1 As ArrayList = CType(sessHelper.GetSession(sessDeleteDiscountProposalDtl), ArrayList)
                If IsNothing(arrDelete1) Then arrDelete1 = New ArrayList
                arrDelete1.Add(objDiscountProposalDtl)
                sessHelper.SetSession(sessDeleteDiscountProposalDtl, arrDelete1)
            End If
            arlDiscountProposalDtl.RemoveAt(colIndex)
            sessHelper.SetSession(sessDiscountProposalDtl, arlDiscountProposalDtl)

            If objDiscountProposalDtlPrice.ID > 0 Then
                Dim arrDelete1 As ArrayList = CType(sessHelper.GetSession(sessDeleteDiscountProposalDtlPrice), ArrayList)
                If IsNothing(arrDelete1) Then arrDelete1 = New ArrayList
                arrDelete1.Add(objDiscountProposalDtlPrice)
                sessHelper.SetSession(sessDeleteDiscountProposalDtlPrice, arrDelete1)

                Dim arrDelete2 As ArrayList = CType(sessHelper.GetSession(sessDeleteDiscountProposalPricetoParameter), ArrayList)

                Dim arlDPPricetoParameter As ArrayList = New ArrayList
                If IsNothing(arrDelete2) Then arrDelete2 = New ArrayList
                For Each objDPPriceParam As DiscountProposalPricetoParameter In arlDiscountProposalPricetoParameter
                    If Not IsNothing(objDPPriceParam.DiscountProposalDetailPrice) Then
                        If objDPPriceParam.DiscountProposalDetailPrice.ID = objDiscountProposalDtlPrice.ID Then
                            arlDPPricetoParameter.Add(objDPPriceParam)
                        End If
                    End If
                Next
                'Dim arlDPPricetoParameter As ArrayList = New System.Collections.ArrayList(
                '                                        (From obj As DiscountProposalPricetoParameter In arlDiscountProposalPricetoParameter.OfType(Of DiscountProposalPricetoParameter)()
                '                                            Where obj.DiscountProposalDetailPrice.ID = objDiscountProposalDtlPrice.ID
                '                                            Order By obj.DiscountProposalDetailPrice.ID, obj.DiscountProposalParameter.ID
                '                                            Select obj).ToList())
                For Each objDPPricetoParameter As DiscountProposalPricetoParameter In arlDPPricetoParameter
                    arrDelete2.Add(objDPPricetoParameter)
                Next
                sessHelper.SetSession(sessDeleteDiscountProposalPricetoParameter, arrDelete2)
            End If

            Dim objDPPricetoParameter2 As DiscountProposalPricetoParameter
            For i As Integer = arlDiscountProposalPricetoParameter.Count - 1 To 0 Step -1
                objDPPricetoParameter2 = CType(arlDiscountProposalPricetoParameter(i), DiscountProposalPricetoParameter)
                If objDiscountProposalDtlPrice.NumberRow = objDPPricetoParameter2.NumberRowParent Then
                    arlDiscountProposalPricetoParameter.Remove(objDPPricetoParameter2)
                End If
            Next
            sessHelper.SetSession(sessDiscountProposalPricetoParameter, arlDiscountProposalPricetoParameter)

            arlDiscountProposalDtlPrice.RemoveAt(colIndex)
            sessHelper.SetSession(sessDiscountProposalDtlPrice, arlDiscountProposalDtlPrice)

            btnBatalSimpanGrid_Click(Nothing, Nothing)
        Catch
        End Try

    End Sub

    Private Sub btnDuplikatColRincianHrg_Click(sender As Object, e As EventArgs)
        arlDiscountProposalDtlPrice = CType(sessHelper.GetSession(sessDiscountProposalDtlPrice), ArrayList)
        If IsNothing(arlDiscountProposalDtlPrice) Then arlDiscountProposalDtlPrice = New ArrayList()
        If arlDiscountProposalDtlPrice.Count = 0 Then Return

        ViewState("isDuplikatRincianHarga") = True
        ViewState("isEditRincianHarga") = False

        Dim btnDuplikatColRincianHrg As Button = DirectCast(sender, Button)
        Dim colIndex As Integer = Right(btnDuplikatColRincianHrg.ID, Len(btnDuplikatColRincianHrg.ID) - Len("btnDuplikatColRincianHrg"))
        ViewState("isInsertCol") = True
        BindGridRincianHargaKendaraan()

        Dim hdnColIndex As HiddenField
        Dim ddlModel As DropDownList, ddlTipe As DropDownList, ddlWarna As DropDownList
        Dim ddlAssyYear As DropDownList, ddlModelYear As DropDownList, txtJumlah As System.Web.UI.WebControls.TextBox
        Dim lblHargaTebus As Label, lblLogisticCost As Label
        Dim txtBBN As System.Web.UI.WebControls.TextBox, txtBiayaBiroJasa As System.Web.UI.WebControls.TextBox, txtRetailPriceOnTheRoad As System.Web.UI.WebControls.TextBox
        Dim lblSubTotalCostDealer As Label, lblMarginDealerGross As Label

        Dim lblSubTotalProgramRegulerMMKSI As Label, lblTotalGrossMarginDealer As Label
        Dim txtBiayaKirimCustomer As System.Web.UI.WebControls.TextBox, txtAksesoris As System.Web.UI.WebControls.TextBox
        Dim lblSubTotalBiayaLainLain As Label, lblGrossDealerMargin As Label
        Dim txtEstimasiDealPrice As System.Web.UI.WebControls.TextBox, lblDiskonDealer As Label
        Dim lblNettDealerMargin As Label, txtPermohonanDiskonProgramFleetCustomer As System.Web.UI.WebControls.TextBox
        Dim lblMarginDealerFinal As Label

        Dim objDiscountProposalDtlPrice As DiscountProposalDetailPrice = CType(arlDiscountProposalDtlPrice(colIndex), DiscountProposalDetailPrice)
        For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
            If IsNothing(hdnColIndex) Then
                hdnColIndex = CType(itemData.FindControl("hdnColIndex"), HiddenField)
            End If
            If IsNothing(ddlModel) Then
                ddlModel = CType(itemData.FindControl(strddlModel), DropDownList)
            End If
            If IsNothing(ddlTipe) Then
                ddlTipe = CType(itemData.FindControl(strddlTipe), DropDownList)
            End If
            If IsNothing(ddlWarna) Then
                ddlWarna = CType(itemData.FindControl(strddlWarna), DropDownList)
            End If
            If IsNothing(ddlAssyYear) Then
                ddlAssyYear = CType(itemData.FindControl(strddlAssyYear), DropDownList)
            End If
            If IsNothing(ddlModelYear) Then
                ddlModelYear = CType(itemData.FindControl(strddlModelYear), DropDownList)
            End If
            If IsNothing(txtJumlah) Then
                txtJumlah = CType(itemData.FindControl(strtxtJumlah), System.Web.UI.WebControls.TextBox)
            End If
            If IsNothing(lblHargaTebus) Then
                lblHargaTebus = CType(itemData.FindControl(strlblHargaTebus), Label)
            End If
            If IsNothing(lblLogisticCost) Then
                lblLogisticCost = CType(itemData.FindControl(strlblLogisticCost), Label)
            End If
            If IsNothing(txtBBN) Then
                txtBBN = CType(itemData.FindControl(strtxtBBN), System.Web.UI.WebControls.TextBox)
            End If
            If IsNothing(txtBiayaBiroJasa) Then
                txtBiayaBiroJasa = CType(itemData.FindControl(strtxtBiayaBiroJasa), System.Web.UI.WebControls.TextBox)
            End If
            If IsNothing(lblSubTotalCostDealer) Then
                lblSubTotalCostDealer = CType(itemData.FindControl(strlblSubTotalCostDealer), Label)
            End If
            If IsNothing(txtRetailPriceOnTheRoad) Then
                txtRetailPriceOnTheRoad = CType(itemData.FindControl(strtxtRetailPriceOnTheRoad), System.Web.UI.WebControls.TextBox)
            End If
            If IsNothing(lblMarginDealerGross) Then
                lblMarginDealerGross = CType(itemData.FindControl(strlblMarginDealerGross), Label)
            End If
            If IsNothing(lblSubTotalProgramRegulerMMKSI) Then
                lblSubTotalProgramRegulerMMKSI = CType(itemData.FindControl(strlblSubTotalProgramRegulerMMKSI), Label)
            End If
            If IsNothing(lblTotalGrossMarginDealer) Then
                lblTotalGrossMarginDealer = CType(itemData.FindControl(strlblTotalGrossMarginDealer), Label)
            End If
            If IsNothing(txtBiayaKirimCustomer) Then
                txtBiayaKirimCustomer = CType(itemData.FindControl(strtxtBiayaPengirimanKeCustomer), System.Web.UI.WebControls.TextBox)
            End If
            If IsNothing(txtAksesoris) Then
                txtAksesoris = CType(itemData.FindControl(strtxtAksesoris), System.Web.UI.WebControls.TextBox)
            End If
            If IsNothing(lblSubTotalBiayaLainLain) Then
                lblSubTotalBiayaLainLain = CType(itemData.FindControl(strlblSubTotalBiayaLainlain), Label)
            End If
            If IsNothing(lblGrossDealerMargin) Then
                lblGrossDealerMargin = CType(itemData.FindControl(strlblGrossDealerMargin), Label)
            End If
            If IsNothing(txtEstimasiDealPrice) Then
                txtEstimasiDealPrice = CType(itemData.FindControl(strtxtEstimasiDealPrice), System.Web.UI.WebControls.TextBox)
            End If
            If IsNothing(lblDiskonDealer) Then
                lblDiskonDealer = CType(itemData.FindControl(strlblDiskonDealer), Label)
            End If
            If IsNothing(lblNettDealerMargin) Then
                lblNettDealerMargin = CType(itemData.FindControl(strlblNettDealerMargin), Label)
            End If
            If IsNothing(txtPermohonanDiskonProgramFleetCustomer) Then
                txtPermohonanDiskonProgramFleetCustomer = CType(itemData.FindControl(strtxtPermohonanDiskonProgramFleetCustomer), System.Web.UI.WebControls.TextBox)
            End If
            If IsNothing(lblMarginDealerFinal) Then
                lblMarginDealerFinal = CType(itemData.FindControl(strlblMarginDealerFinal), Label)
            End If
        Next
        With objDiscountProposalDtlPrice
            If Not IsNothing(.DiscountProposalDetail) Then
                Dim objDPDtl As New DiscountProposalDetail
                If arlDiscountProposalDtl.Count > 0 AndAlso .DiscountProposalDetail.ID > 0 Then
                    objDPDtl = (From item As DiscountProposalDetail In arlDiscountProposalDtl
                                Where item.ID = .DiscountProposalDetail.ID
                                    Select (item)).FirstOrDefault()
                Else
                    objDPDtl = .DiscountProposalDetail
                End If

                ddlModel.SelectedValue = objDPDtl.SubCategoryVehicle.ID
                ddlModel_SelectedIndexChanged(ddlModel, Nothing)

                If Not IsNothing(objDPDtl.VechileColorIsActiveOnPK) Then
                    If Not IsNothing(objDPDtl.VechileColorIsActiveOnPK.VechileTypeGeneral) Then
                        ddlTipe.SelectedValue = objDPDtl.VechileColorIsActiveOnPK.VechileTypeGeneral.ID
                    Else
                        ddlTipe.SelectedValue = .VechileTypeID
                    End If
                End If
                hdnVechileTypeID.Value = ddlTipe.SelectedValue
                ddlTipe_SelectedIndexChanged(ddlTipe, Nothing)

                ddlAssyYear.SelectedValue = objDPDtl.AssyYear
                hdnAssyYear.Value = ddlAssyYear.SelectedValue
                ddlAssyYear_SelectedIndexChanged(ddlAssyYear, Nothing)

                ddlModelYear.SelectedValue = objDPDtl.ModelYear
                hdnModelYear.Value = ddlModelYear.SelectedValue
                ddlModelYear_SelectedIndexChanged(ddlModelYear, Nothing)

                If Not IsNothing(objDPDtl.VechileColorIsActiveOnPK) Then
                    If Not IsNothing(objDPDtl.VechileColorIsActiveOnPK.VechileColor) Then
                        ddlWarna.SelectedValue = objDPDtl.VechileColorIsActiveOnPK.VechileColor.ID
                    End If
                End If
                hdnVechileColorID.Value = ddlWarna.SelectedValue
                ddlWarna_SelectedIndexChanged(ddlWarna, Nothing)

                txtJumlah.Text = objDPDtl.ProposeQty
            Else
                ddlModel.SelectedValue = .SubCategoryVehicleID
                ddlModel_SelectedIndexChanged(ddlModel, Nothing)

                ddlTipe.SelectedValue = .VechileTypeID
                hdnVechileTypeID.Value = ddlTipe.SelectedValue
                ddlTipe_SelectedIndexChanged(ddlTipe, Nothing)

                ddlAssyYear.SelectedValue = .AssyYear
                hdnAssyYear.Value = ddlAssyYear.SelectedValue
                ddlAssyYear_SelectedIndexChanged(ddlAssyYear, Nothing)

                ddlModelYear.SelectedValue = .ModelYear
                hdnModelYear.Value = ddlModelYear.SelectedValue
                ddlModelYear_SelectedIndexChanged(ddlModelYear, Nothing)

                ddlWarna.SelectedValue = .VechileColorID
                hdnVechileColorID.Value = ddlWarna.SelectedValue
                ddlWarna_SelectedIndexChanged(ddlWarna, Nothing)

                txtJumlah.Text = .ProposeQty
            End If

            lblHargaTebus.Text = Format(.RedemptionPrice, "#,##0")
            lblLogisticCost.Text = Format(.LogisticCost, "#,##0")
            txtBBN.Text = Format(.BBN, "#,##0")
            txtBBN_TextChanged(txtBBN, Nothing)
            '-------
            txtBiayaBiroJasa.Text = Format(.OtherCost, "#,##0")
            txtBiayaBiroJasa_TextChanged(txtBiayaBiroJasa, Nothing)
            '-------
            txtRetailPriceOnTheRoad.Text = Format(.RetailPriceOnRoad, "#,##0")
            txtRetailPriceOnTheRoad_TextChanged(txtRetailPriceOnTheRoad, Nothing)
            '-------

            Dim dblSubTotalProgramRegulerMMKSI As Double = 0
            Dim objDiscountProposalPricetoParameter As DiscountProposalPricetoParameter
            Dim arlDPParameter As ArrayList = sessHelper.GetSession(sessDiscountProposalParameter)
            For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
                If Not IsNothing(arlDPParameter) AndAlso arlDPParameter.Count > 0 Then
                    For Each _objDPParameter As DiscountProposalParameter In arlDPParameter
                        'Dim strlblJudulParam As String = "lblJudul" & _objDPParameter.ParameterName.Replace(" ", "")
                        Dim strlblParam As String = "lbl" & _objDPParameter.ParameterName.Replace(" ", "")
                        Dim lblParamProgramRegulerMMKSI As Label = CType(itemData.FindControl(strlblParam), Label)
                        If Not IsNothing(lblParamProgramRegulerMMKSI) Then
                            If arlDiscountProposalPricetoParameter.Count > 0 Then
                                For Each objDPPricetoParameter As DiscountProposalPricetoParameter In arlDiscountProposalPricetoParameter
                                    If objDPPricetoParameter.NumberRowParent = objDiscountProposalDtlPrice.NumberRow AndAlso _
                                       objDPPricetoParameter.DiscountProposalParameter.ID = _objDPParameter.ID Then
                                        lblParamProgramRegulerMMKSI.Text = Format(objDPPricetoParameter.Amount, "#,##0")
                                        dblSubTotalProgramRegulerMMKSI += objDPPricetoParameter.Amount
                                        Exit For
                                    End If
                                Next
                            End If
                            Exit For
                        End If
                    Next
                End If
            Next
            lblSubTotalProgramRegulerMMKSI.Text = Format(dblSubTotalProgramRegulerMMKSI, "#,##0")

            '-------
            For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
                lblMarginDealerGross = CType(itemData.FindControl(strlblMarginDealerGross), Label)
                If Not IsNothing(lblMarginDealerGross) Then Exit For
            Next

            lblTotalGrossMarginDealer.Text = Format(BlankToZerro(lblMarginDealerGross.Text) + BlankToZerro(lblSubTotalProgramRegulerMMKSI.Text), "#,##0")
            txtBiayaKirimCustomer.Text = Format(.DeliveryCost, "#,##0")
            txtBiayaKirimCustomer_TextChanged(txtBiayaKirimCustomer, Nothing)

            txtAksesoris.Text = Format(.Accessories, "#,##0")
            txtAksesoris_TextChanged(txtAksesoris, Nothing)

            txtEstimasiDealPrice.Text = Format(.DealPriceEstimation, "#,##0")
            txtEstimasiDealPrice_TextChanged(txtEstimasiDealPrice, Nothing)

            txtPermohonanDiskonProgramFleetCustomer.Text = Format(.DiscountRequest, "#,##0")
            txtPermohonanDiskonProgramFleetCustomer_TextChanged(txtPermohonanDiskonProgramFleetCustomer, Nothing)
        End With
        ViewState("isDuplikatRincianHarga") = True
        hdnColIndex.Value = colIndex
    End Sub

    Private Sub btnEditColRincianHrg_Click(sender As Object, e As EventArgs)
        arlDiscountProposalDtlPrice = CType(sessHelper.GetSession(sessDiscountProposalDtlPrice), ArrayList)
        If IsNothing(arlDiscountProposalDtlPrice) Then arlDiscountProposalDtlPrice = New ArrayList()
        If arlDiscountProposalDtlPrice.Count = 0 Then Return

        ViewState("isEditRincianHarga") = True
        ViewState("isDuplikatRincianHarga") = False

        Dim btnEditColRincianHrg As Button = DirectCast(sender, Button)
        Dim colIndex As Integer = Right(btnEditColRincianHrg.ID, Len(btnEditColRincianHrg.ID) - Len("btnEditColRincianHrg"))
        ViewState("isInsertCol") = True
        BindGridRincianHargaKendaraan()

        Dim hdnColIndex As HiddenField
        Dim ddlModel As DropDownList, ddlTipe As DropDownList, ddlWarna As DropDownList
        Dim ddlAssyYear As DropDownList, ddlModelYear As DropDownList, txtJumlah As System.Web.UI.WebControls.TextBox
        Dim lblHargaTebus As Label, lblLogisticCost As Label
        Dim txtBBN As System.Web.UI.WebControls.TextBox, txtBiayaBiroJasa As System.Web.UI.WebControls.TextBox, txtRetailPriceOnTheRoad As System.Web.UI.WebControls.TextBox
        Dim lblSubTotalCostDealer As Label, lblMarginDealerGross As Label

        Dim lblSubTotalProgramRegulerMMKSI As Label, lblTotalGrossMarginDealer As Label
        Dim txtBiayaKirimCustomer As System.Web.UI.WebControls.TextBox, txtAksesoris As System.Web.UI.WebControls.TextBox
        Dim lblSubTotalBiayaLainLain As Label, lblGrossDealerMargin As Label
        Dim txtEstimasiDealPrice As System.Web.UI.WebControls.TextBox, lblDiskonDealer As Label
        Dim lblNettDealerMargin As Label, txtPermohonanDiskonProgramFleetCustomer As System.Web.UI.WebControls.TextBox
        Dim lblMarginDealerFinal As Label

        Dim objDiscountProposalDtlPrice As DiscountProposalDetailPrice = CType(arlDiscountProposalDtlPrice(colIndex), DiscountProposalDetailPrice)
        For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
            If IsNothing(hdnColIndex) Then
                hdnColIndex = CType(itemData.FindControl("hdnColIndex"), HiddenField)
            End If
            If IsNothing(ddlModel) Then
                ddlModel = CType(itemData.FindControl(strddlModel), DropDownList)
            End If
            If IsNothing(ddlTipe) Then
                ddlTipe = CType(itemData.FindControl(strddlTipe), DropDownList)
            End If
            If IsNothing(ddlWarna) Then
                ddlWarna = CType(itemData.FindControl(strddlWarna), DropDownList)
            End If
            If IsNothing(ddlAssyYear) Then
                ddlAssyYear = CType(itemData.FindControl(strddlAssyYear), DropDownList)
            End If
            If IsNothing(ddlModelYear) Then
                ddlModelYear = CType(itemData.FindControl(strddlModelYear), DropDownList)
            End If
            If IsNothing(txtJumlah) Then
                txtJumlah = CType(itemData.FindControl(strtxtJumlah), System.Web.UI.WebControls.TextBox)
            End If
            If IsNothing(lblHargaTebus) Then
                lblHargaTebus = CType(itemData.FindControl(strlblHargaTebus), Label)
            End If
            If IsNothing(lblLogisticCost) Then
                lblLogisticCost = CType(itemData.FindControl(strlblLogisticCost), Label)
            End If
            If IsNothing(txtBBN) Then
                txtBBN = CType(itemData.FindControl(strtxtBBN), System.Web.UI.WebControls.TextBox)
            End If
            If IsNothing(txtBiayaBiroJasa) Then
                txtBiayaBiroJasa = CType(itemData.FindControl(strtxtBiayaBiroJasa), System.Web.UI.WebControls.TextBox)
            End If
            If IsNothing(lblSubTotalCostDealer) Then
                lblSubTotalCostDealer = CType(itemData.FindControl(strlblSubTotalCostDealer), Label)
            End If
            If IsNothing(txtRetailPriceOnTheRoad) Then
                txtRetailPriceOnTheRoad = CType(itemData.FindControl(strtxtRetailPriceOnTheRoad), System.Web.UI.WebControls.TextBox)
            End If
            If IsNothing(lblMarginDealerGross) Then
                lblMarginDealerGross = CType(itemData.FindControl(strlblMarginDealerGross), Label)
            End If

            If IsNothing(lblSubTotalProgramRegulerMMKSI) Then
                lblSubTotalProgramRegulerMMKSI = CType(itemData.FindControl(strlblSubTotalProgramRegulerMMKSI), Label)
            End If
            If IsNothing(lblTotalGrossMarginDealer) Then
                lblTotalGrossMarginDealer = CType(itemData.FindControl(strlblTotalGrossMarginDealer), Label)
            End If
            If IsNothing(txtBiayaKirimCustomer) Then
                txtBiayaKirimCustomer = CType(itemData.FindControl(strtxtBiayaPengirimanKeCustomer), System.Web.UI.WebControls.TextBox)
            End If
            If IsNothing(txtAksesoris) Then
                txtAksesoris = CType(itemData.FindControl(strtxtAksesoris), System.Web.UI.WebControls.TextBox)
            End If
            If IsNothing(lblSubTotalBiayaLainLain) Then
                lblSubTotalBiayaLainLain = CType(itemData.FindControl(strlblSubTotalBiayaLainlain), Label)
            End If
            If IsNothing(lblGrossDealerMargin) Then
                lblGrossDealerMargin = CType(itemData.FindControl(strlblGrossDealerMargin), Label)
            End If
            If IsNothing(txtEstimasiDealPrice) Then
                txtEstimasiDealPrice = CType(itemData.FindControl(strtxtEstimasiDealPrice), System.Web.UI.WebControls.TextBox)
            End If
            If IsNothing(lblDiskonDealer) Then
                lblDiskonDealer = CType(itemData.FindControl(strlblDiskonDealer), Label)
            End If
            If IsNothing(lblNettDealerMargin) Then
                lblNettDealerMargin = CType(itemData.FindControl(strlblNettDealerMargin), Label)
            End If
            If IsNothing(txtPermohonanDiskonProgramFleetCustomer) Then
                txtPermohonanDiskonProgramFleetCustomer = CType(itemData.FindControl(strtxtPermohonanDiskonProgramFleetCustomer), System.Web.UI.WebControls.TextBox)
            End If
            If IsNothing(lblMarginDealerFinal) Then
                lblMarginDealerFinal = CType(itemData.FindControl(strlblMarginDealerFinal), Label)
            End If
        Next
        With objDiscountProposalDtlPrice
            If Not IsNothing(.DiscountProposalDetail) Then
                Dim objDPDtl As New DiscountProposalDetail
                If arlDiscountProposalDtl.Count > 0 AndAlso .DiscountProposalDetail.ID > 0 Then
                    objDPDtl = (From item As DiscountProposalDetail In arlDiscountProposalDtl
                                Where item.ID = .DiscountProposalDetail.ID
                                    Select (item)).FirstOrDefault()
                Else
                    objDPDtl = .DiscountProposalDetail
                End If

                ddlModel.SelectedValue = objDPDtl.SubCategoryVehicle.ID
                ddlModel_SelectedIndexChanged(ddlModel, Nothing)

                If Not IsNothing(objDPDtl.VechileColorIsActiveOnPK) Then
                    If Not IsNothing(objDPDtl.VechileColorIsActiveOnPK.VechileTypeGeneral) Then
                        ddlTipe.SelectedValue = objDPDtl.VechileColorIsActiveOnPK.VechileTypeGeneral.ID
                    Else
                        ddlTipe.SelectedValue = .VechileTypeID
                    End If
                End If
                hdnVechileTypeID.Value = ddlTipe.SelectedValue
                ddlTipe_SelectedIndexChanged(ddlTipe, Nothing)

                ddlAssyYear.SelectedValue = objDPDtl.AssyYear
                hdnAssyYear.Value = ddlAssyYear.SelectedValue
                ddlAssyYear_SelectedIndexChanged(ddlAssyYear, Nothing)

                ddlModelYear.SelectedValue = objDPDtl.ModelYear
                hdnModelYear.Value = ddlModelYear.SelectedValue
                ddlModelYear_SelectedIndexChanged(ddlModelYear, Nothing)

                If Not IsNothing(objDPDtl.VechileColorIsActiveOnPK) Then
                    If Not IsNothing(objDPDtl.VechileColorIsActiveOnPK.VechileColor) Then
                        ddlWarna.SelectedValue = objDPDtl.VechileColorIsActiveOnPK.VechileColor.ID
                    End If
                End If
                hdnVechileColorID.Value = ddlWarna.SelectedValue
                ddlWarna_SelectedIndexChanged(ddlWarna, Nothing)

                txtJumlah.Text = objDPDtl.ProposeQty
            Else
                ddlModel.SelectedValue = .SubCategoryVehicleID
                ddlModel_SelectedIndexChanged(ddlModel, Nothing)

                ddlTipe.SelectedValue = .VechileTypeID
                hdnVechileTypeID.Value = ddlTipe.SelectedValue
                ddlTipe_SelectedIndexChanged(ddlTipe, Nothing)

                ddlAssyYear.SelectedValue = .AssyYear
                hdnAssyYear.Value = ddlAssyYear.SelectedValue
                ddlAssyYear_SelectedIndexChanged(ddlAssyYear, Nothing)

                ddlModelYear.SelectedValue = .ModelYear
                hdnModelYear.Value = ddlModelYear.SelectedValue
                ddlModelYear_SelectedIndexChanged(ddlModelYear, Nothing)

                ddlWarna.SelectedValue = .VechileColorID
                hdnVechileColorID.Value = ddlWarna.SelectedValue
                ddlWarna_SelectedIndexChanged(ddlWarna, Nothing)

                txtJumlah.Text = .ProposeQty
            End If

            lblHargaTebus.Text = Format(.RedemptionPrice, "#,##0")
            lblLogisticCost.Text = Format(.LogisticCost, "#,##0")
            txtBBN.Text = Format(.BBN, "#,##0")
            txtBBN_TextChanged(txtBBN, Nothing)
            '-------
            txtBiayaBiroJasa.Text = Format(.OtherCost, "#,##0")
            txtBiayaBiroJasa_TextChanged(txtBiayaBiroJasa, Nothing)
            '-------
            txtRetailPriceOnTheRoad.Text = Format(.RetailPriceOnRoad, "#,##0")
            txtRetailPriceOnTheRoad_TextChanged(txtRetailPriceOnTheRoad, Nothing)
            '-------

            Dim dblSubTotalProgramRegulerMMKSI As Double = 0
            Dim objDiscountProposalPricetoParameter As DiscountProposalPricetoParameter
            Dim arlDPParameter As ArrayList = sessHelper.GetSession(sessDiscountProposalParameter)
            For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
                If Not IsNothing(arlDPParameter) AndAlso arlDPParameter.Count > 0 Then
                    For Each _objDPParameter As DiscountProposalParameter In arlDPParameter
                        'Dim strlblJudulParam As String = "lblJudul" & _objDPParameter.ParameterName.Replace(" ", "")
                        Dim strlblParam As String = "lbl" & _objDPParameter.ParameterName.Replace(" ", "")
                        Dim lblParamProgramRegulerMMKSI As Label = CType(itemData.FindControl(strlblParam), Label)
                        If Not IsNothing(lblParamProgramRegulerMMKSI) Then
                            If arlDiscountProposalPricetoParameter.Count > 0 Then
                                For Each objDPPricetoParameter As DiscountProposalPricetoParameter In arlDiscountProposalPricetoParameter
                                    If objDPPricetoParameter.NumberRowParent = objDiscountProposalDtlPrice.NumberRow AndAlso _
                                       objDPPricetoParameter.DiscountProposalParameter.ID = _objDPParameter.ID Then
                                        lblParamProgramRegulerMMKSI.Text = Format(objDPPricetoParameter.Amount, "#,##0")
                                        dblSubTotalProgramRegulerMMKSI += objDPPricetoParameter.Amount
                                        Exit For
                                    End If
                                Next
                            End If
                            Exit For
                        End If
                    Next
                End If
            Next
            lblSubTotalProgramRegulerMMKSI.Text = Format(dblSubTotalProgramRegulerMMKSI, "#,##0")

            '-------
            For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
                lblMarginDealerGross = CType(itemData.FindControl(strlblMarginDealerGross), Label)
                If Not IsNothing(lblMarginDealerGross) Then Exit For
            Next

            lblTotalGrossMarginDealer.Text = Format(BlankToZerro(lblMarginDealerGross.Text) + BlankToZerro(lblSubTotalProgramRegulerMMKSI.Text), "#,##0")
            txtBiayaKirimCustomer.Text = Format(.DeliveryCost, "#,##0")
            txtBiayaKirimCustomer_TextChanged(txtBiayaKirimCustomer, Nothing)

            txtAksesoris.Text = Format(.Accessories, "#,##0")
            txtAksesoris_TextChanged(txtAksesoris, Nothing)

            txtEstimasiDealPrice.Text = Format(.DealPriceEstimation, "#,##0")
            txtEstimasiDealPrice_TextChanged(txtEstimasiDealPrice, Nothing)

            txtPermohonanDiskonProgramFleetCustomer.Text = Format(.DiscountRequest, "#,##0")
            txtPermohonanDiskonProgramFleetCustomer_TextChanged(txtPermohonanDiskonProgramFleetCustomer, Nothing)
        End With
        ViewState("isEditRincianHarga") = True
        hdnColIndex.Value = colIndex
    End Sub

    Function updateDataTableRincianKendaraan(ByVal idxRow As Integer, ByVal columnName As String)
        Dim arlObj As DataTable = CType(sessHelper.GetSession(sessDiscountProposalDtlPricePivotGrid), DataTable)
        For i As Integer = 0 To arlObj.Columns.Count - 1
            For k As Integer = 0 To dt.Rows.Count - 1
                If Not (arlObj.Rows(k).IsNull(i)) AndAlso arlObj.Rows(k)(i).ToString().Trim = "" Then

                    Exit For
                End If
            Next
        Next
    End Function

    Private Sub dgRincianHargaKendaraan_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgRincianHargaKendaraan.RowDataBound
        Dim gvr As GridViewRow = e.Row

        arlDiscountProposalDtlPrice = CType(sessHelper.GetSession(sessDiscountProposalDtlPrice), ArrayList)
        If IsNothing(arlDiscountProposalDtlPrice) Then arlDiscountProposalDtlPrice = New ArrayList()
        Dim intCountGridCols As Integer = arlDiscountProposalDtlPrice.Count

        Dim arlDPParameter As ArrayList = sessHelper.GetSession(sessDiscountProposalParameter)
        If IsNothing(arlDPParameter) Then arlDPParameter = New ArrayList
        Dim intCountProgRegulerCols As Integer = arlDPParameter.Count

        Dim dtObj As DataTable = CType(sessHelper.GetSession(sessDiscountProposalDtlPricePivotGrid), DataTable)
        If IsNothing(dtObj) Then dtObj = New DataTable

        If (gvr.RowType = ListItemType.Header) Then
            If IsNothing(ViewState("isInsertCol")) Then ViewState("isInsertCol") = False
            e.Row.Cells(1).Visible = CType(ViewState("isInsertCol"), Boolean)

            For i As Integer = 0 To intCountGridCols + 1
                If i = 0 Then
                    e.Row.Cells(i).Text = "Keterangan Kendaraan :"
                    e.Row.Cells(i).Font.Bold = True
                    e.Row.Cells(i).HorizontalAlign = HorizontalAlign.Left
                ElseIf i = 1 Then
                    If CType(ViewState("isEditRincianHarga"), Boolean) Then
                        e.Row.Cells(i).Text = "#Edit Data :"
                        e.Row.Cells(i).BackColor = Color.Blue
                    ElseIf CType(ViewState("isDuplikatRincianHarga"), Boolean) Then
                        e.Row.Cells(i).Text = "#Copy Data :"
                        e.Row.Cells(i).BackColor = Color.Green
                    Else
                        e.Row.Cells(i).Text = "#Insert Data :"
                        e.Row.Cells(i).BackColor = Color.Brown
                    End If
                    e.Row.Cells(i).Font.Bold = True
                    e.Row.Cells(i).HorizontalAlign = HorizontalAlign.Center
                    e.Row.Cells(i).Width = 150
                Else
                    e.Row.Cells(i).Text = ""
                    Dim hd As HiddenField = New HiddenField()
                    hd.ID = "hdn" & (i - 2).ToString
                    hd.Value = (i - 2).ToString
                    e.Row.Cells(i).Controls.Add(hd)

                    If Mode <> "View" Then
                        Dim btnEdit As Button = New Button()
                        btnEdit.ID = "btnEditColRincianHrg" & (i - 2).ToString
                        btnEdit.Width = 45
                        btnEdit.Text = "Edit"
                        btnEdit.Attributes("Style") = "background-image:url('../images/edit.gif'); background-repeat: no-repeat; text-align:right;"
                        If Not IsLoginAsDealer() Then
                            If objDiscountProposalHeader.Status = 3 OrElse objDiscountProposalHeader.Status = 9 Then  'Status = konfirmasi & Revisi
                                btnEdit.Attributes("Style") = "display:none;"
                            End If
                        End If
                        e.Row.Cells(i).Controls.Add(btnEdit)
                        AddHandler btnEdit.Click, New EventHandler(AddressOf btnEditColRincianHrg_Click)

                        Dim lbl As Label = New Label()
                        lbl.ID = "lblSpace" & (i - 2).ToString
                        lbl.Width = 1
                        lbl.Text = ""
                        e.Row.Cells(i).Controls.Add(lbl)

                        Dim btnDuplikat As Button = New Button()
                        btnDuplikat.ID = "btnDuplikatColRincianHrg" & (i - 2).ToString
                        btnDuplikat.Width = 45
                        btnDuplikat.Text = "Salin"
                        btnDuplikat.Attributes("Style") = "background-image:url('../images/popup.gif'); background-repeat: no-repeat; text-align:right;"
                        If Not IsLoginAsDealer() Then
                            If objDiscountProposalHeader.Status = 3 OrElse objDiscountProposalHeader.Status = 9 Then  'Status = konfirmasi & Revisi
                                btnDuplikat.Attributes("Style") = "display:none;"
                            End If
                        End If
                        e.Row.Cells(i).Controls.Add(btnDuplikat)
                        AddHandler btnDuplikat.Click, New EventHandler(AddressOf btnDuplikatColRincianHrg_Click)

                        Dim lbl2 As Label = New Label()
                        lbl2.ID = "lblSpace2" & (i - 2).ToString
                        lbl2.Width = 1
                        lbl2.Text = ""
                        e.Row.Cells(i).Controls.Add(lbl2)

                        Dim btnDel As Button = New Button()
                        btnDel.ID = "btnDeleteColRincianHrg" & (i - 2).ToString
                        btnDel.Width = 55
                        btnDel.Text = "Hapus"
                        btnDel.Attributes("OnClick") = "return confirm('Anda yakin mau hapus ?')"
                        btnDel.Attributes("Style") = "background-image:url('../images/trash.gif'); background-repeat: no-repeat; text-align:right;"
                        e.Row.Cells(i).Controls.Add(btnDel)
                        If Not IsLoginAsDealer() Then
                            If objDiscountProposalHeader.Status = 3 OrElse objDiscountProposalHeader.Status = 9 Then  'Status = konfirmasi & revisi
                                btnDel.Attributes("Style") = "display:none;"
                            End If
                        End If
                        AddHandler btnDel.Click, New EventHandler(AddressOf btnDeleteColRincianHrg_Click)
                    End If
                End If
                If i <> 1 Then
                    e.Row.Cells(i).BackColor = Color.DarkGray
                    e.Row.Cells(i).ForeColor = Color.White
                End If
            Next
        End If

        If (gvr.RowType = ListItemType.Footer) Then
            If IsNothing(ViewState("isInsertCol")) Then ViewState("isInsertCol") = False
            e.Row.Cells(1).Visible = CType(ViewState("isInsertCol"), Boolean)

            If Mode <> "View" Then
                Dim btnInsert As Button = New Button()
                btnInsert.ID = "btnInsertData"
                btnInsert.Width = 60
                btnInsert.Text = "Simpan"
                btnInsert.Attributes("OnClick") = "return confirm('Anda yakin mau simpan ?')"
                btnInsert.Attributes("Style") = "background-image:url('../images/simpan.gif'); background-repeat: no-repeat; text-align:right;"
                e.Row.Cells(1).Controls.Add(btnInsert)
                AddHandler btnInsert.Click, New EventHandler(AddressOf btnSaveColRincianHrg_Click)

                Dim lbl As Label = New Label()
                lbl.ID = "lblSpace"
                lbl.Width = 20
                lbl.Text = ""
                e.Row.Cells(1).Controls.Add(lbl)

                Dim btnBatal As Button = New Button()
                btnBatal.ID = "btnBatalInsert"
                btnBatal.Width = 60
                btnBatal.Text = "Batal"
                btnBatal.Attributes("Style") = "background-image:url('../images/batal.gif'); background-repeat: no-repeat; text-align:right;"
                e.Row.Cells(1).Controls.Add(btnBatal)
                AddHandler btnBatal.Click, New EventHandler(AddressOf btnBatalSimpanGrid_Click)
            End If

            e.Row.Cells(0).BackColor = Color.SkyBlue
        End If

        If (gvr.RowType = ListItemType.Item) Then
            If IsNothing(ViewState("isInsertCol")) Then ViewState("isInsertCol") = False
            e.Row.Cells(1).Visible = CType(ViewState("isInsertCol"), Boolean)

            If isInsert Then
                'Model
                If e.Row.RowIndex = 0 Then
                    Dim ddlModel As DropDownList = New DropDownList()
                    ddlModel.AutoPostBack = True
                    ddlModel.ID = strddlModel

                    ddlModel.Items.Clear()
                    '-- SubCategoryVehicle criteria & sort
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Status", MatchType.Exact, ""))  '-- Type still active
                    criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Category.ID", MatchType.InSet, "(1,2)"))
                    Dim sortColl As SortCollection = New SortCollection
                    sortColl.Add(New Sort(GetType(SubCategoryVehicle), "ID", Sort.SortDirection.DESC))  '-- Sort by Vechile type code

                    '-- Bind ddlSubCategory dropdownlist
                    ddlModel.DataSource = New SubCategoryVehicleFacade(User).Retrieve(criterias, sortColl)
                    ddlModel.DataTextField = "Name"
                    ddlModel.DataValueField = "ID"
                    ddlModel.DataBind()
                    ddlModel.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))  '-- Dummy blank item
                    e.Row.Cells(1).Controls.Add(ddlModel)

                    ' ADD DROPDOWN CHANGED EVENT
                    AddHandler ddlModel.SelectedIndexChanged, New EventHandler(AddressOf ddlModel_SelectedIndexChanged)
                    e.Row.Cells(0).BackColor = Color.SeaGreen
                    e.Row.Cells(0).ForeColor = Color.White
                    e.Row.Cells(0).Text = "&nbsp;&nbsp;Model *"
                End If

                'Tipe
                If e.Row.RowIndex = 1 Then
                    Dim ddlTipe As DropDownList = New DropDownList()
                    ddlTipe.AutoPostBack = True
                    ddlTipe.ID = strddlTipe

                    ddlTipe.Items.Clear()
                    ddlTipe.Items.Add(New ListItem("Silahkan Pilih", ""))

                    e.Row.Cells(1).Controls.Add(ddlTipe)
                    ddlTipe.Attributes("onchange") = "ChangeDDLTipe(this.value)"

                    '' ADD DROPDOWN CHANGED EVENT.
                    AddHandler ddlTipe.SelectedIndexChanged, New EventHandler(AddressOf ddlTipe_SelectedIndexChanged)
                    e.Row.Cells(0).BackColor = Color.SeaGreen
                    e.Row.Cells(0).ForeColor = Color.White
                    e.Row.Cells(0).Text = "&nbsp;&nbsp;Tipe *"

                    Dim hdnColIndex As HiddenField = New HiddenField()
                    hdnColIndex.ID = "hdnColIndex"
                    hdnColIndex.Value = -1
                    e.Row.Cells(1).Controls.Add(hdnColIndex)
                End If

                'Assy Year
                If e.Row.RowIndex = 2 Then
                    Dim ddlAssyYear As DropDownList = New DropDownList()
                    ddlAssyYear.AutoPostBack = True
                    ddlAssyYear.ID = strddlAssyYear

                    ddlAssyYear.Items.Clear()
                    ddlAssyYear.Items.Add(New ListItem("Silahkan Pilih", ""))

                    e.Row.Cells(1).Controls.Add(ddlAssyYear)
                    ddlAssyYear.Attributes("onchange") = "ChangeDDLAssyYear(this.value)"

                    AddHandler ddlAssyYear.SelectedIndexChanged, New EventHandler(AddressOf ddlAssyYear_SelectedIndexChanged)
                    e.Row.Cells(0).BackColor = Color.SeaGreen
                    e.Row.Cells(0).ForeColor = Color.White
                    e.Row.Cells(0).Text = "&nbsp;&nbsp;Assy Year *"
                End If

                'Model Year
                If e.Row.RowIndex = 3 Then
                    Dim ddlModelYear As DropDownList = New DropDownList()
                    ddlModelYear.AutoPostBack = True
                    ddlModelYear.ID = strddlModelYear

                    ddlModelYear.Items.Clear()
                    ddlModelYear.Items.Add(New ListItem("Silahkan Pilih", ""))

                    e.Row.Cells(1).Controls.Add(ddlModelYear)
                    ddlModelYear.Attributes("onchange") = "ChangeDDLModelYear(this.value)"

                    AddHandler ddlModelYear.TextChanged, New EventHandler(AddressOf ddlModelYear_SelectedIndexChanged)
                    e.Row.Cells(0).BackColor = Color.SeaGreen
                    e.Row.Cells(0).ForeColor = Color.White
                    e.Row.Cells(0).Text = "&nbsp;&nbsp;Model Year *"
                End If

                'Warna
                If e.Row.RowIndex = 4 Then
                    Dim ddlWarna As DropDownList = New DropDownList()
                    ddlWarna.AutoPostBack = True
                    ddlWarna.ID = strddlWarna

                    ddlWarna.Items.Clear()
                    ddlWarna.Items.Add(New ListItem("Silahkan Pilih", ""))
                    e.Row.Cells(1).Controls.Add(ddlWarna)
                    ddlWarna.Attributes("onchange") = "ChangeDDLColor(this.value)"

                    AddHandler ddlWarna.SelectedIndexChanged, New EventHandler(AddressOf ddlWarna_SelectedIndexChanged)
                    e.Row.Cells(0).BackColor = Color.SeaGreen
                    e.Row.Cells(0).ForeColor = Color.White
                    e.Row.Cells(0).Text = "&nbsp;&nbsp;Warna *"
                End If

                'Jumlah
                If e.Row.RowIndex = 5 Then
                    Dim tb As System.Web.UI.WebControls.TextBox = New System.Web.UI.WebControls.TextBox()
                    tb.ID = strtxtJumlah
                    tb.Attributes("style") = "text-align:right"
                    tb.Width = 70
                    tb.MaxLength = 8
                    tb.Attributes("autocomplete") = "off"
                    tb.Attributes("onkeypress") = "return numericOnlyUniv(event)"
                    tb.Attributes("onblur") = "CekBlankToZerro(this);"
                    tb.Attributes("onfocus") = "this.select()"
                    tb.Text = "0"
                    e.Row.Cells(1).Controls.Add(tb)
                    e.Row.Cells(0).BackColor = Color.SeaGreen
                    e.Row.Cells(0).ForeColor = Color.White
                    e.Row.Cells(0).Text = "&nbsp;&nbsp;Jumlah *"
                End If

                If e.Row.RowIndex = 6 Then
                    e.Row.Cells(0).Text = "Cost Dealer :"
                    e.Row.Cells(1).Text = ""
                    e.Row.Cells(0).Font.Bold = True
                    For i As Integer = 0 To intCountGridCols + 1
                        e.Row.Cells(i).BackColor = Color.DarkGray
                        e.Row.Cells(i).ForeColor = Color.White
                    Next
                End If

                'Harga Tebus
                If e.Row.RowIndex = 7 Then
                    Dim lbl As Label = New Label()
                    lbl.ID = strlblHargaTebus
                    lbl.Text = 0
                    e.Row.Cells(1).Controls.Add(lbl)
                    e.Row.Cells(0).BackColor = Color.SeaGreen
                    e.Row.Cells(0).ForeColor = Color.White
                    e.Row.Cells(0).Text = "&nbsp;&nbsp;Harga Tebus"
                End If

                'Logistic Cost
                If e.Row.RowIndex = 8 Then
                    Dim lbl As Label = New Label()
                    lbl.ID = strlblLogisticCost
                    lbl.Text = 0
                    e.Row.Cells(1).Controls.Add(lbl)
                    e.Row.Cells(0).BackColor = Color.SeaGreen
                    e.Row.Cells(0).ForeColor = Color.White
                    e.Row.Cells(0).Text = "&nbsp;&nbsp;Logistic Cost"
                End If

                'BBN
                If e.Row.RowIndex = 9 Then
                    Dim tb As System.Web.UI.WebControls.TextBox = New System.Web.UI.WebControls.TextBox()
                    tb.AutoPostBack = True
                    tb.ID = strtxtBBN
                    tb.Attributes("style") = "text-align:right"
                    tb.Width = 90
                    tb.MaxLength = 15
                    tb.Attributes("autocomplete") = "off"
                    tb.Attributes("onkeypress") = "return numericOnlyUniv(event)"
                    tb.Attributes("onblur") = "CekBlankToZerro(this);return pic(this,this.value,'9999999999','N')"
                    tb.Attributes("onfocus") = "this.select()"
                    tb.Text = "0"
                    e.Row.Cells(1).Controls.Add(tb)
                    AddHandler tb.TextChanged, New EventHandler(AddressOf txtBBN_TextChanged)
                    e.Row.Cells(0).BackColor = Color.SeaGreen
                    e.Row.Cells(0).ForeColor = Color.White
                    e.Row.Cells(0).Text = "&nbsp;&nbsp;BBN"
                End If

                'Biaya (Biro Jasa, Form A, Keur, dll)
                If e.Row.RowIndex = 10 Then
                    Dim tb As System.Web.UI.WebControls.TextBox = New System.Web.UI.WebControls.TextBox()
                    tb.AutoPostBack = True
                    tb.ID = strtxtBiayaBiroJasa
                    tb.Attributes("style") = "text-align:right"
                    tb.Width = 90
                    tb.MaxLength = 15
                    tb.Attributes("autocomplete") = "off"
                    tb.Attributes("onkeypress") = "return numericOnlyUniv(event)"
                    tb.Attributes("onblur") = "CekBlankToZerro(this);return pic(this,this.value,'9999999999','N')"
                    tb.Attributes("onfocus") = "this.select()"
                    tb.Text = "0"
                    e.Row.Cells(1).Controls.Add(tb)
                    AddHandler tb.TextChanged, New EventHandler(AddressOf txtBiayaBiroJasa_TextChanged)
                    e.Row.Cells(0).BackColor = Color.SeaGreen
                    e.Row.Cells(0).ForeColor = Color.White
                    e.Row.Cells(0).Text = "&nbsp;&nbsp;Biaya (Biro Jasa, Form A, Keur, dll)"
                End If

                'Sub Total Cost Dealer
                If e.Row.RowIndex = 11 Then
                    Dim lbl As Label = New Label()
                    lbl.ID = strlblSubTotalCostDealer
                    lbl.Text = 0
                    e.Row.Cells(1).Controls.Add(lbl)
                    lbl.Attributes("onchange") = "ChangeSubTotalCostDealer(this.value)"

                    e.Row.Cells(0).BackColor = Color.SeaGreen
                    e.Row.Cells(0).ForeColor = Color.White
                    e.Row.Cells(0).Text = "Sub Total Cost Dealer"
                End If

                'Retail Price on the Road
                If e.Row.RowIndex = 12 Then
                    Dim tb As System.Web.UI.WebControls.TextBox = New System.Web.UI.WebControls.TextBox()
                    tb.AutoPostBack = True
                    tb.ID = strtxtRetailPriceOnTheRoad
                    tb.Attributes("style") = "text-align:right"
                    tb.Width = 90
                    tb.MaxLength = 15
                    tb.Attributes("autocomplete") = "off"
                    tb.Attributes("onkeypress") = "return numericOnlyUniv(event)"
                    tb.Attributes("onblur") = "CekBlankToZerro(this);return pic(this,this.value,'9999999999','N')"
                    tb.Attributes("onfocus") = "this.select()"
                    tb.Text = "0"
                    e.Row.Cells(1).Controls.Add(tb)
                    AddHandler tb.TextChanged, New EventHandler(AddressOf txtRetailPriceOnTheRoad_TextChanged)
                    e.Row.Cells(0).BackColor = Color.SeaGreen
                    e.Row.Cells(0).ForeColor = Color.White
                    e.Row.Cells(0).Text = "&nbsp;&nbsp;Retail Price on the Road"
                End If

                'Margin Dealer Gross diluar Sales Program
                If e.Row.RowIndex = 13 Then
                    Dim lbl As Label = New Label()
                    lbl.ID = strlblMarginDealerGross
                    Dim dblMarginDealerGross As Double = 0
                    lbl.Text = Format(dblMarginDealerGross, "#,##0")

                    e.Row.Cells(1).Controls.Add(lbl)
                    e.Row.Cells(0).BackColor = Color.SeaGreen
                    e.Row.Cells(0).ForeColor = Color.White
                    e.Row.Cells(0).Text = "Margin Dealer Gross diluar Sales Program"
                End If

                If e.Row.RowIndex = 14 Then
                    e.Row.Cells(0).Text = "Program Reguler MMKSI :"
                    e.Row.Cells(1).Text = ""
                    e.Row.Cells(0).Font.Bold = True
                    For i As Integer = 0 To intCountGridCols + 1
                        e.Row.Cells(i).BackColor = Color.DarkGray
                        e.Row.Cells(i).ForeColor = Color.White
                    Next
                End If

                Dim idxRowFrom As Integer = 14
                Dim idxRowTo As Integer = idxRowFrom + intCountProgRegulerCols

                If e.Row.RowIndex > idxRowFrom And e.Row.RowIndex <= idxRowTo Then
                    If Not IsNothing(arlDPParameter) AndAlso arlDPParameter.Count > 0 Then
                        Dim idx As Integer = e.Row.RowIndex - (idxRowFrom + 1)
                        Dim objDPParameter As DiscountProposalParameter = CType(arlDPParameter(idx), DiscountProposalParameter)
                        If Not IsNothing(objDPParameter) AndAlso objDPParameter.ID > 0 Then
                            Dim lbl As Label = New Label()
                            lbl.ID = "lbl" & objDPParameter.ParameterName.Replace(" ", "")
                            lbl.Text = 0
                            e.Row.Cells(1).Controls.Add(lbl)
                            Dim hdn As HiddenField = New HiddenField()
                            hdn.ID = "hdn" & objDPParameter.ParameterName.Replace(" ", "")
                            hdn.Value = objDPParameter.ParameterName
                            e.Row.Cells(1).Controls.Add(hdn)

                            Dim lblJudul As Label = New Label()
                            lblJudul.ID = "lblJudul" & objDPParameter.ParameterName.Replace(" ", "")
                            lblJudul.Text = "&nbsp;&nbsp;" & objDPParameter.ParameterName
                            lblJudul.ForeColor = Color.White
                            e.Row.Cells(0).Controls.Add(lblJudul)
                            e.Row.Cells(0).BackColor = Color.SeaGreen
                            e.Row.Cells(0).ForeColor = Color.White
                            e.Row.Cells(0).Text = lblJudul.Text
                        End If
                    End If
                End If

                'Sub_Total_Program_Reguler_MMKSI_(D)
                If e.Row.RowIndex = (idxRowTo + 1) Then
                    Dim lbl As Label = New Label()
                    lbl.ID = strlblSubTotalProgramRegulerMMKSI
                    lbl.Text = 0
                    e.Row.Cells(1).Controls.Add(lbl)
                    e.Row.Cells(0).BackColor = Color.SeaGreen
                    e.Row.Cells(0).ForeColor = Color.White
                    e.Row.Cells(0).Text = "Sub Total Program Reguler MMKSI"
                End If

                'Total_Gross_Margin_Dealer_(Termasuk_Sales_Program)_(E=C+D)
                If e.Row.RowIndex = (idxRowTo + 2) Then
                    Dim lbl As Label = New Label()
                    lbl.ID = strlblTotalGrossMarginDealer
                    lbl.Text = 0
                    e.Row.Cells(1).Controls.Add(lbl)
                    e.Row.Cells(0).BackColor = Color.SeaGreen
                    e.Row.Cells(0).ForeColor = Color.White
                    e.Row.Cells(0).Text = "Total Gross Margin Dealer termasuk Sales Program"
                End If

                If e.Row.RowIndex = (idxRowTo + 3) Then
                    e.Row.Cells(0).Text = "Biaya Lain - lain :"
                    e.Row.Cells(1).Text = ""
                    e.Row.Cells(0).Font.Bold = True
                    For i As Integer = 0 To intCountGridCols + 1
                        e.Row.Cells(i).BackColor = Color.DarkGray
                        e.Row.Cells(i).ForeColor = Color.White
                    Next
                End If

                'Biaya Pengiriman ke Customer
                If e.Row.RowIndex = (idxRowTo + 4) Then
                    Dim tb As System.Web.UI.WebControls.TextBox = New System.Web.UI.WebControls.TextBox()
                    tb.AutoPostBack = True
                    tb.ID = strtxtBiayaPengirimanKeCustomer
                    tb.Attributes("style") = "text-align:right"
                    tb.Width = 90
                    tb.MaxLength = 15
                    tb.Attributes("autocomplete") = "off"
                    tb.Attributes("onkeypress") = "return numericOnlyUniv(event)"
                    tb.Attributes("onblur") = "CekBlankToZerro(this);return pic(this,this.value,'9999999999','N')"
                    tb.Attributes("onfocus") = "this.select()"
                    tb.Text = "0"
                    e.Row.Cells(1).Controls.Add(tb)
                    AddHandler tb.TextChanged, New EventHandler(AddressOf txtBiayaKirimCustomer_TextChanged)
                    e.Row.Cells(0).BackColor = Color.SeaGreen
                    e.Row.Cells(0).ForeColor = Color.White
                    e.Row.Cells(0).Text = "&nbsp;&nbsp;Biaya Pengiriman ke Customer (diluar area domisili Dealer)"
                End If

                'Aksesoris (kaca film, talang air, karpet dasar, sarung jok)
                If e.Row.RowIndex = (idxRowTo + 5) Then
                    Dim tb As System.Web.UI.WebControls.TextBox = New System.Web.UI.WebControls.TextBox()
                    tb.AutoPostBack = True
                    tb.ID = strtxtAksesoris
                    tb.Attributes("style") = "text-align:right"
                    tb.Width = 90
                    tb.MaxLength = 15
                    tb.Attributes("autocomplete") = "off"
                    tb.Attributes("onkeypress") = "return numericOnlyUniv(event)"
                    tb.Attributes("onblur") = "CekBlankToZerro(this);return pic(this,this.value,'9999999999','N')"
                    tb.Attributes("onfocus") = "this.select()"
                    tb.Text = "0"
                    e.Row.Cells(1).Controls.Add(tb)
                    AddHandler tb.TextChanged, New EventHandler(AddressOf txtAksesoris_TextChanged)
                    e.Row.Cells(0).BackColor = Color.SeaGreen
                    e.Row.Cells(0).ForeColor = Color.White
                    e.Row.Cells(0).Text = "&nbsp;&nbsp;Aksesoris (kaca film, talang air, karpet dasar, sarung jok)"
                End If

                'Sub Total Biaya Lain-lain
                If e.Row.RowIndex = (idxRowTo + 6) Then
                    Dim lbl As Label = New Label()
                    lbl.ID = strlblSubTotalBiayaLainlain
                    If lbl.Text.Trim = "" Then lbl.Text = 0
                    e.Row.Cells(1).Controls.Add(lbl)
                    e.Row.Cells(0).BackColor = Color.SeaGreen
                    e.Row.Cells(0).ForeColor = Color.White
                    e.Row.Cells(0).Text = "Sub Total Biaya Lain-lain"
                End If

                'Gross Dealer Margin
                If e.Row.RowIndex = (idxRowTo + 7) Then
                    Dim lbl As Label = New Label()
                    lbl.ID = strlblGrossDealerMargin
                    lbl.Text = 0
                    e.Row.Cells(1).Controls.Add(lbl)
                    e.Row.Cells(0).BackColor = Color.SeaGreen
                    e.Row.Cells(0).ForeColor = Color.White
                    e.Row.Cells(0).Text = "Gross Dealer Margin"
                End If

                'Estimasi Deal Price
                If e.Row.RowIndex = (idxRowTo + 8) Then
                    Dim tb As System.Web.UI.WebControls.TextBox = New System.Web.UI.WebControls.TextBox()
                    tb.AutoPostBack = True
                    tb.ID = strtxtEstimasiDealPrice
                    tb.Attributes("style") = "text-align:right"
                    tb.Width = 90
                    tb.MaxLength = 15
                    tb.Attributes("autocomplete") = "off"
                    tb.Attributes("onkeypress") = "return numericOnlyUniv(event)"
                    tb.Attributes("onblur") = "CekBlankToZerro(this);return pic(this,this.value,'9999999999','N')"
                    tb.Attributes("onfocus") = "this.select()"
                    tb.Text = "0"
                    e.Row.Cells(1).Controls.Add(tb)
                    AddHandler tb.TextChanged, New EventHandler(AddressOf txtEstimasiDealPrice_TextChanged)
                    e.Row.Cells(0).BackColor = Color.SeaGreen
                    e.Row.Cells(0).ForeColor = Color.White
                    e.Row.Cells(0).Text = "&nbsp;&nbsp;Estimasi Deal Price"
                End If

                'Diskon Dealer
                If e.Row.RowIndex = (idxRowTo + 9) Then
                    Dim lbl As Label = New Label()
                    lbl.ID = strlblDiskonDealer
                    lbl.Text = 0
                    e.Row.Cells(1).Controls.Add(lbl)
                    e.Row.Cells(0).BackColor = Color.SeaGreen
                    e.Row.Cells(0).ForeColor = Color.White
                    e.Row.Cells(0).Text = "Diskon Dealer"
                End If

                'Nett Dealer Margin
                If e.Row.RowIndex = (idxRowTo + 10) Then
                    Dim lbl As Label = New Label()
                    lbl.ID = strlblNettDealerMargin
                    lbl.Text = 0
                    e.Row.Cells(1).Controls.Add(lbl)
                    e.Row.Cells(0).BackColor = Color.SeaGreen
                    e.Row.Cells(0).ForeColor = Color.White
                    e.Row.Cells(0).Text = "Nett Dealer Margin"
                End If

                'Permohonan Diskon Program Fleet Customer
                If e.Row.RowIndex = (idxRowTo + 11) Then
                    Dim tb As System.Web.UI.WebControls.TextBox = New System.Web.UI.WebControls.TextBox()
                    tb.AutoPostBack = True
                    tb.ID = strtxtPermohonanDiskonProgramFleetCustomer
                    tb.Attributes("style") = "text-align:right"
                    tb.Width = 90
                    tb.MaxLength = 15
                    tb.Attributes("autocomplete") = "off"
                    tb.Attributes("onkeypress") = "return numericOnlyUniv(event)"
                    tb.Attributes("onblur") = "CekBlankToZerro(this);return pic(this,this.value,'9999999999','N')"
                    tb.Attributes("onfocus") = "this.select()"
                    tb.Text = "0"
                    e.Row.Cells(1).Controls.Add(tb)
                    AddHandler tb.TextChanged, New EventHandler(AddressOf txtPermohonanDiskonProgramFleetCustomer_TextChanged)
                    e.Row.Cells(0).BackColor = Color.SeaGreen
                    e.Row.Cells(0).ForeColor = Color.White
                    e.Row.Cells(0).Text = "&nbsp;&nbsp;Permohonan Diskon Program Fleet Customer"
                End If

                'Margin Dealer Final
                If e.Row.RowIndex = (idxRowTo + 12) Then
                    Dim lbl As Label = New Label()
                    lbl.ID = strlblMarginDealerFinal
                    lbl.Text = 0
                    e.Row.Cells(1).Controls.Add(lbl)
                    e.Row.Cells(0).BackColor = Color.SeaGreen
                    e.Row.Cells(0).ForeColor = Color.White
                    e.Row.Cells(0).Text = "Margin Dealer Final"
                End If

                dtObj.Rows(e.Row.RowIndex)(0) = e.Row.Cells(0).Text.Replace("&nbsp;", " ").Replace("*", "").Trim
            End If
        End If
    End Sub

    Private Sub btnAddVehicle_Click(sender As Object, e As EventArgs) Handles btnAddVehicle.Click
        hdnAssyYear.Value = ""
        hdnModelYear.Value = ""
        hdnVechileTypeID.Value = ""
        hdnVechileColorID.Value = ""
        hdnSubTotalCostDealer.Value = ""
        ViewState("isEditRincianHarga") = False
        ViewState("isDuplikatRincianHarga") = False
        ViewState("isInsertCol") = True
        BindGridRincianHargaKendaraan()
    End Sub

    Private Sub BindGridRincianHargaKendaraan()
        Dim strIDModelKendaraan As String, strModelKendaraan As String, strTypeDescription As String, intTypeGeneralID As Integer = 0
        Dim strWarnaKendaraan As String, strAssyYear As Integer, strModelYear As Integer
        Dim intProposeQty As Integer

        Dim dblRedemptionPrice As Double, dblLogisticCost As Double, dblBBN As Double
        Dim dblOtherCost As Double, dblSubTotalCostDealer As Double, dblDealerMargin As Double
        Dim dblOtherBenefitProgramPrice As Double, dblPenawaranHrg As Double, dblCustomerPriceRequest As Double
        Dim dblSelisih As Double, dblDiscountRequest As Double, dblDealerMarginSubsidy As Double
        Dim dblDealerMarginFinal As Double, dblRetailPriceOnRoad As Double
        Dim dblMarginDealerGross As Double

        Dim dblSubTotalProgressReguler As Double = 0, dblAmountProgressReguler As Double = 0
        Dim dblTotalGrossMarginDealer As Double, dblDeliveryCost As Double, dblAccessories As Double
        Dim dblSubTotalBiayaLainlain As Double, dblGrossDealerMargin As Double, dblDealPriceEstimation As Double
        Dim dblDiscountDealer As Double, dblNettDealerMargin As Double, dblMarginDealerFinal As Double
        Dim objVechileTypeGeneral As New VechileTypeGeneral

        Dim objDPHeader As DiscountProposalHeader = sessHelper.GetSession(sessDiscountProposalHdr)
        If IsNothing(objDPHeader) Then objDPHeader = New DiscountProposalHeader
        arlDiscountProposalDtlPrice = CType(sessHelper.GetSession(sessDiscountProposalDtlPrice), ArrayList)
        If IsNothing(arlDiscountProposalDtlPrice) Then arlDiscountProposalDtlPrice = New ArrayList()
        arlDiscountProposalDtl = CType(sessHelper.GetSession(sessDiscountProposalDtl), ArrayList)
        If IsNothing(arlDiscountProposalDtl) Then arlDiscountProposalDtl = New ArrayList()
        arlDiscountProposalPricetoParameter = CType(sessHelper.GetSession(sessDiscountProposalPricetoParameter), ArrayList)
        If IsNothing(arlDiscountProposalPricetoParameter) Then arlDiscountProposalPricetoParameter = New ArrayList()

        GenerateNumberRow("DiscountProposalDetailPrice", arlDiscountProposalDtlPrice)
        GenerateNumberRow("DiscountProposalDetail", arlDiscountProposalDtl)

        dt = New DataTable
        Dim arlDPParameter As ArrayList = sessHelper.GetSession(sessDiscountProposalParameter)
        If IsNothing(arlDPParameter) Then arlDPParameter = New ArrayList

        Dim colName As String = ""
        Dim intCountCols As Integer = 28 + arlDPParameter.Count
        For j As Integer = 0 To intCountCols - 1
            colName = "col" & j.ToString
            dt.Columns.Add(colName)
        Next

        If IsNothing(isInsert) Then isInsert = False
        If isInsert Then
            Dim dr As DataRow = dt.NewRow()
            For j As Integer = 0 To intCountCols - 1
                If j > 4 Then
                    dr(j) = "0"
                Else
                    dr(j) = ""
                End If
            Next
            dt.Rows.Add(dr.ItemArray)
        End If

        Dim i% = 1
        For Each objDPDtlPrice As DiscountProposalDetailPrice In arlDiscountProposalDtlPrice
            If Not IsNothing(objDPDtlPrice.DiscountProposalDetail) AndAlso objDPDtlPrice.DiscountProposalDetail.ID > 0 Then
                Dim objDPDtl As New DiscountProposalDetail
                If arlDiscountProposalDtl.Count > 0 Then
                    objDPDtl = (From item As DiscountProposalDetail In arlDiscountProposalDtl
                                Where item.ID = objDPDtlPrice.DiscountProposalDetail.ID
                                    Select (item)).FirstOrDefault()
                Else
                    objDPDtl = objDPDtlPrice.DiscountProposalDetail
                End If
                strIDModelKendaraan = objDPDtl.SubCategoryVehicle.ID
                strModelKendaraan = objDPDtl.SubCategoryVehicle.Name
                strTypeDescription = ""
                If Not IsNothing(objDPDtl.VechileColorIsActiveOnPK) Then
                    If Not IsNothing(objDPDtl.VechileColorIsActiveOnPK.VechileTypeGeneral) Then
                        strTypeDescription = objDPDtl.VechileColorIsActiveOnPK.VechileTypeGeneral.Name
                        intTypeGeneralID = objDPDtl.VechileColorIsActiveOnPK.VechileTypeGeneral.ID
                    Else
                        objVechileTypeGeneral = New VechileTypeGeneralFacade(User).Retrieve(CShort(objDPDtlPrice.VechileTypeID))
                        If Not IsNothing(objVechileTypeGeneral) Then
                            strTypeDescription = objVechileTypeGeneral.Name
                            intTypeGeneralID = objVechileTypeGeneral.ID
                        End If
                    End If
                End If
                strWarnaKendaraan = ""
                If Not IsNothing(objDPDtl.VechileColorIsActiveOnPK) Then
                    If Not IsNothing(objDPDtl.VechileColorIsActiveOnPK.VechileColor) Then
                        strWarnaKendaraan = objDPDtl.VechileColorIsActiveOnPK.VechileColor.ColorIndName
                    End If
                End If
                strAssyYear = objDPDtl.AssyYear
                strModelYear = objDPDtl.ModelYear
                intProposeQty = objDPDtl.ProposeQty
            Else
                strIDModelKendaraan = objDPDtlPrice.SubCategoryVehicleID
                strModelKendaraan = objDPDtlPrice.SubCategoryVehicleName
                strTypeDescription = objDPDtlPrice.VechileTypeDesc
                intTypeGeneralID = objDPDtlPrice.VechileTypeID
                strWarnaKendaraan = objDPDtlPrice.ColorIndName
                strAssyYear = objDPDtlPrice.AssyYear
                strModelYear = objDPDtlPrice.ModelYear
                intProposeQty = objDPDtlPrice.ProposeQty
            End If

            dblRedemptionPrice = objDPDtlPrice.RedemptionPrice
            dblBBN = objDPDtlPrice.BBN
            dblOtherCost = objDPDtlPrice.OtherCost
            dblLogisticCost = objDPDtlPrice.LogisticCost
            dblSubTotalCostDealer = dblRedemptionPrice + dblBBN + dblOtherCost + dblLogisticCost
            dblRetailPriceOnRoad = objDPDtlPrice.RetailPriceOnRoad
            dblMarginDealerGross = dblRetailPriceOnRoad - dblSubTotalCostDealer

            Dim arrColGrid(intCountCols) As String
            arrColGrid(0) = "#" & i
            arrColGrid(1) = strModelKendaraan
            arrColGrid(2) = strTypeDescription
            arrColGrid(3) = strAssyYear
            arrColGrid(4) = strModelYear
            arrColGrid(5) = strWarnaKendaraan
            arrColGrid(6) = intProposeQty
            arrColGrid(7) = ""
            arrColGrid(8) = Format(dblRedemptionPrice, "#,##0")
            arrColGrid(9) = Format(dblLogisticCost, "#,##0")
            arrColGrid(10) = Format(dblBBN, "#,##0")
            arrColGrid(11) = Format(dblOtherCost, "#,##0")
            arrColGrid(12) = Format(dblSubTotalCostDealer, "#,##0")
            arrColGrid(13) = Format(dblRetailPriceOnRoad, "#,##0")
            arrColGrid(14) = Format(dblMarginDealerGross, "#,##0")
            arrColGrid(15) = ""

            Dim intCountCols2 As Integer = 15 + arlDPParameter.Count
            Dim j% = 16
            Dim arlDPPricetoParameter As ArrayList = New System.Collections.ArrayList(
                            (From obj As DiscountProposalPricetoParameter In arlDiscountProposalPricetoParameter.OfType(Of DiscountProposalPricetoParameter)()
                                Where obj.NumberRowParent = objDPDtlPrice.NumberRow
                                Order By obj.NumberRowParent, obj.DiscountProposalParameter.ID
                                Select obj).ToList())


            dblSubTotalProgressReguler = 0 : dblAmountProgressReguler = 0
            For Each _objDPParameter As DiscountProposalParameter In arlDPParameter
                If Mode = "New" OrElse objDPHeader.Status = 0 Then
                    dblAmountProgressReguler = GetDiscountFromProgramReguler(_objDPParameter.ParameterName, intTypeGeneralID, strAssyYear, strModelYear)
                    arrColGrid(j) = Format(dblAmountProgressReguler, "#,##0")
                    dblSubTotalProgressReguler += dblAmountProgressReguler
                Else
                    If Not IsNothing(arlDPPricetoParameter) AndAlso arlDPPricetoParameter.Count > 0 Then
                        For Each objDPPricetoParameter As DiscountProposalPricetoParameter In arlDPPricetoParameter
                            If objDPPricetoParameter.NumberRowParent = objDPDtlPrice.NumberRow AndAlso _
                               objDPPricetoParameter.DiscountProposalParameter.ID = _objDPParameter.ID Then
                                arrColGrid(j) = Format(objDPPricetoParameter.Amount, "#,##0")
                                dblSubTotalProgressReguler += objDPPricetoParameter.Amount
                            End If
                        Next
                    Else
                        dblAmountProgressReguler = GetDiscountFromProgramReguler(_objDPParameter.ParameterName, intTypeGeneralID, strAssyYear, strModelYear)
                        arrColGrid(j) = Format(dblAmountProgressReguler, "#,##0")
                    End If
                End If
                If IsNothing(arrColGrid(j)) Then arrColGrid(j) = 0
                j += 1
            Next

            dblTotalGrossMarginDealer = dblMarginDealerGross + dblSubTotalProgressReguler
            dblDeliveryCost = objDPDtlPrice.DeliveryCost
            dblAccessories = objDPDtlPrice.Accessories
            dblSubTotalBiayaLainlain = dblDeliveryCost + dblAccessories
            dblGrossDealerMargin = dblTotalGrossMarginDealer - dblSubTotalBiayaLainlain
            dblDealPriceEstimation = objDPDtlPrice.DealPriceEstimation
            dblDiscountDealer = dblRetailPriceOnRoad - dblDealPriceEstimation
            dblNettDealerMargin = dblGrossDealerMargin - dblDiscountDealer
            dblDiscountRequest = objDPDtlPrice.DiscountRequest
            dblMarginDealerFinal = dblNettDealerMargin + dblDiscountRequest

            arrColGrid(intCountCols2 + 1) = Format(dblSubTotalProgressReguler, "#,##0")
            arrColGrid(intCountCols2 + 2) = Format(dblTotalGrossMarginDealer, "#,##0")
            arrColGrid(intCountCols2 + 3) = ""
            arrColGrid(intCountCols2 + 4) = Format(dblDeliveryCost, "#,##0")
            arrColGrid(intCountCols2 + 5) = Format(dblAccessories, "#,##0")
            arrColGrid(intCountCols2 + 6) = Format(dblSubTotalBiayaLainlain, "#,##0")
            arrColGrid(intCountCols2 + 7) = Format(dblGrossDealerMargin, "#,##0")
            arrColGrid(intCountCols2 + 8) = Format(dblDealPriceEstimation, "#,##0")
            arrColGrid(intCountCols2 + 9) = Format(dblDiscountDealer, "#,##0")
            arrColGrid(intCountCols2 + 10) = Format(dblNettDealerMargin, "#,##0")
            arrColGrid(intCountCols2 + 11) = Format(dblDiscountRequest, "#,##0")
            arrColGrid(intCountCols2 + 12) = Format(dblMarginDealerFinal, "#,##0")

            Dim dr As DataRow = dt.NewRow()
            For k As Integer = 0 To intCountCols - 1
                dr(k) = arrColGrid(k)
            Next
            dt.Rows.Add(dr.ItemArray)
            i += 1
        Next

        sessHelper.SetSession(sessDiscountProposalDtlPricePivot, dt)
        BindGridRincianHargaKendaraan2()
    End Sub

    Private Sub BindGridRincianHargaKendaraan2()
        Dim oldTable As DataTable
        oldTable = sessHelper.GetSession(sessDiscountProposalDtlPricePivot)
        If IsNothing(oldTable) Then oldTable = New DataTable
        sessHelper.SetSession(sessDiscountProposalDtlPriceOld, oldTable)
        Dim newTable As DataTable = PivotTable(oldTable, 0)
        sessHelper.SetSession(sessDiscountProposalDtlPricePivotGrid, newTable)

        dgRincianHargaKendaraan.DataSource = newTable
        dgRincianHargaKendaraan.DataBind()
    End Sub

    Private Function PivotTable(oldTable As DataTable, Optional pivotColumnOrdinal As Integer = 0) As DataTable
        Dim newTable As New DataTable
        Dim dr As DataRow
        Dim row1 As DataRow

        ' add pivot column name
        If pivotColumnOrdinal > 0 Then
            newTable.Columns.Add(oldTable.Columns(pivotColumnOrdinal - 1).ColumnName)
        Else
            newTable.Columns.Add(oldTable.Columns(pivotColumnOrdinal).ColumnName)
        End If

        ' add pivot column values in each row as column headers to new Table
        For Each row1 In oldTable.Rows
            newTable.Columns.Add(row1(pivotColumnOrdinal))
        Next

        Dim col As Integer
        Dim row As Integer
        ' loop through columns
        For col = 0 To oldTable.Columns.Count - 1
            'pivot column doen't get it's own row (it is already a header)
            If col = pivotColumnOrdinal Then Continue For

            ' each column becomes a new row
            dr = newTable.NewRow()

            ' add the Column Name in the first Column
            dr(0) = oldTable.Columns(col).ColumnName

            ' add data from every row to the pivoted row
            For row = 0 To oldTable.Rows.Count - 1
                dr(row + 1) = oldTable.Rows(row)(col)
            Next

            'add the DataRow to the new table
            newTable.Rows.Add(dr)
        Next

        Return newTable
    End Function

    Private Function BlankToZerro(ByVal _valueProperty As String) As Double
        If Len(_valueProperty.Trim) > 0 Then
            _valueProperty = Replace(Replace(_valueProperty.Trim, ".", ""), ",", "")
            If _valueProperty.Trim = "" OrElse _valueProperty.Trim <= 0 Then
                Return 0
            Else
                Return Format(CDbl(_valueProperty), "#,##0")
            End If
        Else
            Return 0
        End If
    End Function

    Private Sub btnSaveColRincianHrg_Click(sender As Object, e As EventArgs)
        Dim hdnColIndex As HiddenField
        Dim ddlModel As DropDownList, ddlTipe As DropDownList, ddlWarna As DropDownList
        Dim ddlAssyYear As DropDownList, ddlModelYear As DropDownList, txtJumlah As System.Web.UI.WebControls.TextBox
        Dim lblHargaTebus As Label, lblLogisticCost As Label
        Dim txtBBN As System.Web.UI.WebControls.TextBox, txtBiayaBiroJasaDLL As System.Web.UI.WebControls.TextBox, txtRetailPriceOnTheRoad As System.Web.UI.WebControls.TextBox
        Dim lblSubTotalCostDealer As Label, lblMarginDealerGross As Label

        Dim lblSubTotalProgramRegulerMMKSI As Label, lblTotalGrossMarginDealer As Label
        Dim txtBiayaPengirimankeCustomer As System.Web.UI.WebControls.TextBox, txtAksesoris As System.Web.UI.WebControls.TextBox
        Dim lblSubTotalBiayaLainLain As Label, lblGrossDealerMargin As Label
        Dim txtEstimasiDealPrice As System.Web.UI.WebControls.TextBox, lblDiskonDealer As Label
        Dim lblNettDealerMargin As Label, txtPermohonanDiskonProgramFleetCustomer As System.Web.UI.WebControls.TextBox
        Dim lblMarginDealerFinal As Label

        For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
            If IsNothing(hdnColIndex) Then
                hdnColIndex = CType(itemData.FindControl("hdnColIndex"), HiddenField)
            End If
            If IsNothing(ddlModel) Then
                ddlModel = CType(itemData.FindControl(strddlModel), DropDownList)
            End If
            If IsNothing(ddlTipe) Then
                ddlTipe = CType(itemData.FindControl(strddlTipe), DropDownList)
            End If
            If IsNothing(ddlWarna) Then
                ddlWarna = CType(itemData.FindControl(strddlWarna), DropDownList)
            End If
            If IsNothing(ddlAssyYear) Then
                ddlAssyYear = CType(itemData.FindControl(strddlAssyYear), DropDownList)
            End If
            If IsNothing(ddlModelYear) Then
                ddlModelYear = CType(itemData.FindControl(strddlModelYear), DropDownList)
            End If
            If IsNothing(txtJumlah) Then
                txtJumlah = CType(itemData.FindControl(strtxtJumlah), System.Web.UI.WebControls.TextBox)
            End If
            If IsNothing(lblHargaTebus) Then
                lblHargaTebus = CType(itemData.FindControl(strlblHargaTebus), Label)
            End If
            If IsNothing(lblLogisticCost) Then
                lblLogisticCost = CType(itemData.FindControl(strlblLogisticCost), Label)
            End If
            If IsNothing(txtBBN) Then
                txtBBN = CType(itemData.FindControl(strtxtBBN), System.Web.UI.WebControls.TextBox)
            End If
            If IsNothing(txtBiayaBiroJasaDLL) Then
                txtBiayaBiroJasaDLL = CType(itemData.FindControl(strtxtBiayaBiroJasa), System.Web.UI.WebControls.TextBox)
            End If
            If IsNothing(lblSubTotalCostDealer) Then
                lblSubTotalCostDealer = CType(itemData.FindControl(strlblSubTotalCostDealer), Label)
            End If
            If IsNothing(txtRetailPriceOnTheRoad) Then
                txtRetailPriceOnTheRoad = CType(itemData.FindControl(strtxtRetailPriceOnTheRoad), System.Web.UI.WebControls.TextBox)
            End If
            If IsNothing(lblMarginDealerGross) Then
                lblMarginDealerGross = CType(itemData.FindControl(strlblMarginDealerGross), Label)
            End If
            If IsNothing(lblSubTotalProgramRegulerMMKSI) Then
                lblSubTotalProgramRegulerMMKSI = CType(itemData.FindControl(strlblSubTotalProgramRegulerMMKSI), Label)
            End If
            If IsNothing(lblTotalGrossMarginDealer) Then
                lblTotalGrossMarginDealer = CType(itemData.FindControl(strlblTotalGrossMarginDealer), Label)
            End If
            If IsNothing(txtBiayaPengirimankeCustomer) Then
                txtBiayaPengirimankeCustomer = CType(itemData.FindControl(strtxtBiayaPengirimanKeCustomer), System.Web.UI.WebControls.TextBox)
            End If
            If IsNothing(txtAksesoris) Then
                txtAksesoris = CType(itemData.FindControl(strtxtAksesoris), System.Web.UI.WebControls.TextBox)
            End If
            If IsNothing(lblSubTotalBiayaLainLain) Then
                lblSubTotalBiayaLainLain = CType(itemData.FindControl(strlblSubTotalBiayaLainlain), Label)
            End If
            If IsNothing(lblGrossDealerMargin) Then
                lblGrossDealerMargin = CType(itemData.FindControl(strlblGrossDealerMargin), Label)
            End If
            If IsNothing(txtEstimasiDealPrice) Then
                txtEstimasiDealPrice = CType(itemData.FindControl(strtxtEstimasiDealPrice), System.Web.UI.WebControls.TextBox)
            End If
            If IsNothing(lblDiskonDealer) Then
                lblDiskonDealer = CType(itemData.FindControl(strlblDiskonDealer), Label)
            End If
            If IsNothing(lblNettDealerMargin) Then
                lblNettDealerMargin = CType(itemData.FindControl(strlblNettDealerMargin), Label)
            End If
            If IsNothing(txtPermohonanDiskonProgramFleetCustomer) Then
                txtPermohonanDiskonProgramFleetCustomer = CType(itemData.FindControl(strtxtPermohonanDiskonProgramFleetCustomer), System.Web.UI.WebControls.TextBox)
            End If
            If IsNothing(lblMarginDealerFinal) Then
                lblMarginDealerFinal = CType(itemData.FindControl(strlblMarginDealerFinal), Label)
            End If
        Next

        If ddlModel.SelectedIndex = 0 Then
            MessageBox.Show("Model harus diisi.")
            Exit Sub
        End If
        If ddlTipe.SelectedIndex = 0 Then
            MessageBox.Show("Tipe harus diisi.")
            Exit Sub
        End If
        If ddlAssyYear.SelectedIndex = 0 Then
            MessageBox.Show("Assy Year harus diisi.")
            Exit Sub
        End If
        'If ddlModelYear.SelectedIndex = 0 Then
        '    MessageBox.Show("Model Year harus diisi.")
        '    Exit Sub
        'End If
        If ddlWarna.SelectedIndex = 0 Then
            MessageBox.Show("Warna harus diisi.")
            Exit Sub
        End If
        If txtJumlah.Text.Trim = "" OrElse txtJumlah.Text.Trim = "0" Then
            MessageBox.Show("Jumlah Unit harus diisi.")
            Exit Sub
        End If
        If txtBBN.Text.Trim = 0 Then
            MessageBox.Show("BBN harus diisi.")
            Exit Sub
        End If
        If txtBiayaBiroJasaDLL.Text.Trim = "" OrElse txtBiayaBiroJasaDLL.Text.Trim = "0" Then
            MessageBox.Show("Biaya Biro Jasa dan lain-lain harus diisi.")
            Exit Sub
        End If
        If txtRetailPriceOnTheRoad.Text.Trim = "" OrElse txtRetailPriceOnTheRoad.Text.Trim = "0" Then
            MessageBox.Show("Retail Proses on the Road harus diisi.")
            Exit Sub
        End If
        'If txtBiayaPengirimankeCustomer.Text.Trim = "" OrElse txtBiayaPengirimankeCustomer.Text.Trim = "0" Then
        '    MessageBox.Show("Biaya Pengiriman ke Customer harus diisi.")
        '    Exit Sub
        'End If

        arlDiscountProposalDtl = CType(sessHelper.GetSession(sessDiscountProposalDtl), ArrayList)
        If IsNothing(arlDiscountProposalDtl) Then arlDiscountProposalDtl = New ArrayList()
        arlDiscountProposalDtlPrice = CType(sessHelper.GetSession(sessDiscountProposalDtlPrice), ArrayList)
        If IsNothing(arlDiscountProposalDtlPrice) Then arlDiscountProposalDtlPrice = New ArrayList()
        arlDiscountProposalPricetoParameter = CType(sessHelper.GetSession(sessDiscountProposalPricetoParameter), ArrayList)
        If IsNothing(arlDiscountProposalPricetoParameter) Then arlDiscountProposalPricetoParameter = New ArrayList()

        Dim intVechileColorID As Integer = 0
        Dim intVechileTypeGeneralID As Integer = 0

        If ViewState("isEditRincianHarga") = False OrElse ViewState("isDuplikatRincianHarga") = True Then
            For Each objDtl As DiscountProposalDetail In arlDiscountProposalDtl
                If objDtl.SubCategoryVehicle.ID = ddlModel.SelectedValue Then
                    intVechileColorID = 0
                    intVechileTypeGeneralID = 0
                    If Not IsNothing(objDtl.VechileColorIsActiveOnPK) Then
                        If Not IsNothing(objDtl.VechileColorIsActiveOnPK.VechileColor) Then
                            intVechileColorID = objDtl.VechileColorIsActiveOnPK.VechileColor.ID
                        End If
                        If Not IsNothing(objDtl.VechileColorIsActiveOnPK.VechileTypeGeneral) Then
                            intVechileTypeGeneralID = objDtl.VechileColorIsActiveOnPK.VechileTypeGeneral.ID
                        End If
                    End If
                    If intVechileColorID = ddlWarna.SelectedValue Then
                        If intVechileTypeGeneralID = ddlTipe.SelectedValue Then
                            If objDtl.AssyYear = ddlAssyYear.SelectedValue Then
                                If objDtl.ModelYear = ddlModelYear.SelectedValue Then
                                    MessageBox.Show("Model, Tipe, Warna, Assy Year dan Model Year sudah pernah di input.")
                                    Return
                                End If
                            End If
                        End If
                    End If
                End If
            Next
        Else
            For Each objDtl As DiscountProposalDetail In arlDiscountProposalDtl
                If objDtl.SubCategoryVehicle.ID = ddlModel.SelectedValue Then
                    intVechileColorID = 0
                    intVechileTypeGeneralID = 0
                    If Not IsNothing(objDtl.VechileColorIsActiveOnPK) Then
                        If Not IsNothing(objDtl.VechileColorIsActiveOnPK.VechileColor) Then
                            intVechileColorID = objDtl.VechileColorIsActiveOnPK.VechileColor.ID
                        End If
                        If Not IsNothing(objDtl.VechileColorIsActiveOnPK.VechileTypeGeneral) Then
                            intVechileTypeGeneralID = objDtl.VechileColorIsActiveOnPK.VechileTypeGeneral.ID
                        End If
                    End If
                    If intVechileColorID = ddlWarna.SelectedValue Then
                        If intVechileTypeGeneralID = ddlTipe.SelectedValue Then
                            If objDtl.AssyYear = ddlAssyYear.SelectedValue Then
                                If objDtl.ModelYear = ddlModelYear.SelectedValue Then
                                    If objDtl.NumberRow <> (hdnColIndex.Value + 1) Then
                                        MessageBox.Show("Model, Tipe, Warna, Assy Year dan Model Year sudah pernah di input.")
                                        Return
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            Next
        End If

        Dim objDiscountProposalDtl As DiscountProposalDetail
        Dim objDiscountProposalDtlPrice As DiscountProposalDetailPrice
        If ViewState("isEditRincianHarga") = False OrElse ViewState("isDuplikatRincianHarga") = True Then
            objDiscountProposalDtl = New DiscountProposalDetail
            objDiscountProposalDtl.NumberRow = arlDiscountProposalDtl.Count + 1
            objDiscountProposalDtlPrice = New DiscountProposalDetailPrice
            objDiscountProposalDtlPrice.NumberRow = arlDiscountProposalDtlPrice.Count + 1
        Else
            objDiscountProposalDtl = CType(arlDiscountProposalDtl(hdnColIndex.Value), DiscountProposalDetail)
            objDiscountProposalDtl.NumberRow = hdnColIndex.Value + 1
            objDiscountProposalDtlPrice = CType(arlDiscountProposalDtlPrice(hdnColIndex.Value), DiscountProposalDetailPrice)
            objDiscountProposalDtlPrice.NumberRow = hdnColIndex.Value + 1
        End If

        arlDiscountProposalPricetoParameter = New System.Collections.ArrayList(
                                            (From obj As DiscountProposalPricetoParameter In arlDiscountProposalPricetoParameter.OfType(Of DiscountProposalPricetoParameter)()
                                                Order By obj.NumberRowParent, obj.DiscountProposalParameter.ID
                                                Select obj).ToList())
        Dim arlDPPricetoParameterLoop As ArrayList = New System.Collections.ArrayList((From obj As DiscountProposalPricetoParameter In arlDiscountProposalPricetoParameter.OfType(Of DiscountProposalPricetoParameter)()
                                                Where obj.NumberRowParent = objDiscountProposalDtlPrice.NumberRow
                                                Order By obj.NumberRowParent, obj.DiscountProposalParameter.ID
                                                Select obj).ToList())

        Dim arlDPParameter As ArrayList = sessHelper.GetSession(sessDiscountProposalParameter)
        If IsNothing(arlDPParameter) Then arlDPParameter = New ArrayList
        Dim arrDelete2 As ArrayList = CType(sessHelper.GetSession(sessDeleteDiscountProposalPricetoParameter), ArrayList)
        If IsNothing(arrDelete2) Then arrDelete2 = New ArrayList

        Dim arrExistDPPriceToParam As New ArrayList
        Dim objDiscountProposalPricetoParameter As DiscountProposalPricetoParameter
        If arlDPParameter.Count = 0 Then
            For Each objDPPricetoParameter As DiscountProposalPricetoParameter In arlDPPricetoParameterLoop
                arrExistDPPriceToParam.Add(objDPPricetoParameter)
            Next
            If arrExistDPPriceToParam.Count > 0 Then
                For i As Integer = arlDiscountProposalPricetoParameter.Count - 1 To 0 Step -1
                    Dim objDPPricetoParameter As DiscountProposalPricetoParameter = CType(arlDiscountProposalPricetoParameter(i), DiscountProposalPricetoParameter)
                    If objDPPricetoParameter.NumberRowParent = objDiscountProposalDtlPrice.NumberRow Then
                        For j As Integer = arrExistDPPriceToParam.Count - 1 To 0 Step -1
                            Dim objExistDPPricetoParameter As DiscountProposalPricetoParameter = CType(arrExistDPPriceToParam(j), DiscountProposalPricetoParameter)
                            If objDPPricetoParameter.NumberRowParent = objExistDPPricetoParameter.NumberRowParent Then
                                If objDPPricetoParameter.DiscountProposalParameter.ID = objExistDPPricetoParameter.DiscountProposalParameter.ID Then
                                    If objDPPricetoParameter.ID = objExistDPPricetoParameter.ID Then
                                        If objDPPricetoParameter.ID > 0 Then
                                            arrDelete2.Add(objDPPricetoParameter)
                                        End If
                                        arlDiscountProposalPricetoParameter.Remove(objDPPricetoParameter)
                                        Exit For
                                    End If
                                End If
                            End If
                        Next
                    End If
                Next
                sessHelper.SetSession(sessDeleteDiscountProposalPricetoParameter, arrDelete2)
            End If
        Else
            For Each itemData As GridViewRow In dgRincianHargaKendaraan.Rows
                If Not IsNothing(arlDPParameter) AndAlso arlDPParameter.Count > 0 Then
                    For Each _objDPParameter As DiscountProposalParameter In arlDPParameter
                        Dim strHdnJudulParam As String = "hdn" & _objDPParameter.ParameterName.Replace(" ", "")
                        Dim hdnJudulParam As HiddenField = CType(itemData.FindControl(strHdnJudulParam), HiddenField)
                        Dim strlblParam As String = "lbl" & _objDPParameter.ParameterName.Replace(" ", "")
                        Dim lblParamProgramRegulerMMKSI As Label = CType(itemData.FindControl(strlblParam), Label)
                        If Not IsNothing(hdnJudulParam) AndAlso Not IsNothing(lblParamProgramRegulerMMKSI) Then
                            '---Saat Insert Grid
                            If ViewState("isEditRincianHarga") = False OrElse ViewState("isDuplikatRincianHarga") = True Then
                                objDiscountProposalPricetoParameter = New DiscountProposalPricetoParameter
                                With objDiscountProposalPricetoParameter
                                    .NumberRowParent = arlDiscountProposalDtlPrice.Count + 1
                                    .DiscountProposalDetailPrice = objDiscountProposalDtlPrice
                                    .DiscountProposalParameter = _objDPParameter
                                    .Amount = lblParamProgramRegulerMMKSI.Text.Replace(".", "").Replace(",", "")
                                End With
                                arlDiscountProposalPricetoParameter.Add(objDiscountProposalPricetoParameter)

                            Else    '---Saat Edit Grid
                                Dim isExsistRow As Boolean = False
                                For i As Integer = 0 To arlDPPricetoParameterLoop.Count - 1
                                    Dim objDPPricetoParameter As DiscountProposalPricetoParameter = CType(arlDiscountProposalPricetoParameter(i), DiscountProposalPricetoParameter)
                                    If objDPPricetoParameter.NumberRowParent = objDiscountProposalDtlPrice.NumberRow Then
                                        If objDPPricetoParameter.DiscountProposalParameter.ID = _objDPParameter.ID Then
                                            objDPPricetoParameter.Amount = GetDiscountFromProgramReguler(_objDPParameter.ParameterName, objDiscountProposalDtlPrice.VechileTypeID, objDiscountProposalDtlPrice.AssyYear, objDiscountProposalDtlPrice.ModelYear)
                                            isExsistRow = True
                                            arrExistDPPriceToParam.Add(objDPPricetoParameter)
                                            Exit For
                                        End If
                                    End If
                                Next
                                If isExsistRow = False Then
                                    objDiscountProposalPricetoParameter = New DiscountProposalPricetoParameter
                                    With objDiscountProposalPricetoParameter
                                        .NumberRowParent = objDiscountProposalDtlPrice.NumberRow
                                        .DiscountProposalDetailPrice = objDiscountProposalDtlPrice
                                        .DiscountProposalParameter = _objDPParameter
                                        .Amount = GetDiscountFromProgramReguler(_objDPParameter.ParameterName, objDiscountProposalDtlPrice.VechileTypeID, objDiscountProposalDtlPrice.AssyYear, objDiscountProposalDtlPrice.ModelYear)
                                    End With
                                    arlDiscountProposalPricetoParameter.Add(objDiscountProposalPricetoParameter)
                                    arrExistDPPriceToParam.Add(objDiscountProposalPricetoParameter)
                                End If
                            End If
                            Exit For
                        End If
                    Next
                End If
            Next
            If arrExistDPPriceToParam.Count > 0 Then
                For i As Integer = arlDiscountProposalPricetoParameter.Count - 1 To 0 Step -1
                    Dim objDPPricetoParameter As DiscountProposalPricetoParameter = CType(arlDiscountProposalPricetoParameter(i), DiscountProposalPricetoParameter)
                    If objDPPricetoParameter.NumberRowParent = objDiscountProposalDtlPrice.NumberRow Then
                        Dim isExistRow As Boolean = False
                        For Each objExistDPPricetoParameter As DiscountProposalPricetoParameter In arrExistDPPriceToParam
                            If objDPPricetoParameter.NumberRowParent = objExistDPPricetoParameter.NumberRowParent Then
                                If objDPPricetoParameter.DiscountProposalParameter.ID = objExistDPPricetoParameter.DiscountProposalParameter.ID Then
                                    If objDPPricetoParameter.ID = objExistDPPricetoParameter.ID Then
                                        isExistRow = True
                                        Exit For
                                    End If
                                End If
                            End If
                        Next
                        If isExistRow = False Then
                            If objDPPricetoParameter.ID > 0 Then
                                arrDelete2.Add(objDPPricetoParameter)
                            End If
                            arlDiscountProposalPricetoParameter.Remove(objDPPricetoParameter)
                        End If
                    End If
                Next
                sessHelper.SetSession(sessDeleteDiscountProposalPricetoParameter, arrDelete2)
            End If
        End If
        sessHelper.SetSession(sessDiscountProposalPricetoParameter, arlDiscountProposalPricetoParameter)

        Dim ddlWarnaSelectedValue As Integer = 0
        Dim ddlAssyYearSelectedValue As Integer = 0
        Dim ddlModelYearSelectedValue As Integer = 0
        If ddlWarna.SelectedValue = "" Then ddlWarnaSelectedValue = 0 Else ddlWarnaSelectedValue = ddlWarna.SelectedValue
        If ddlAssyYear.SelectedValue = "" Then ddlAssyYearSelectedValue = 0 Else ddlAssyYearSelectedValue = ddlAssyYear.SelectedValue
        If ddlModelYear.SelectedValue = "" Then ddlModelYearSelectedValue = 0 Else ddlModelYearSelectedValue = ddlModelYear.SelectedValue
        Dim objVechileColorIsActiveOnPK As VechileColorIsActiveOnPK = New VechileColorIsActiveOnPKFacade(User).RetrieveByVechileColorID(CInt(ddlWarnaSelectedValue), CInt(ddlAssyYearSelectedValue), CInt(ddlModelYearSelectedValue))
        With objDiscountProposalDtl
            .DiscountProposalHeader = objDiscountProposalHeader
            .SubCategoryVehicle = New SubCategoryVehicleFacade(User).Retrieve(CShort(ddlModel.SelectedValue))
            .AssyYear = ddlAssyYear.SelectedValue
            .ModelYear = If(ddlModelYear.SelectedIndex = 0, Nothing, ddlModelYear.SelectedValue.Trim)
            .VechileColorIsActiveOnPK = objVechileColorIsActiveOnPK
            .ProposeQty = txtJumlah.Text
        End With

        With objDiscountProposalDtlPrice
            .DiscountProposalDetail = objDiscountProposalDtl
            .DiscountProposalDetail.SubCategoryVehicle = objDiscountProposalDtl.SubCategoryVehicle
            .DiscountProposalDetail.VechileColorIsActiveOnPK = objDiscountProposalDtl.VechileColorIsActiveOnPK
            .DiscountProposalHeader = objDiscountProposalHeader
            .SubCategoryVehicleID = objDiscountProposalDtl.SubCategoryVehicle.ID
            .SubCategoryVehicleName = objDiscountProposalDtl.SubCategoryVehicle.Name

            Dim objVechileTypeGeneral As VechileTypeGeneral = New VechileTypeGeneralFacade(User).Retrieve(CShort(ddlTipe.SelectedValue))
            If IsNothing(objVechileTypeGeneral) Then objVechileTypeGeneral = New VechileTypeGeneral
            .VechileTypeID = objVechileTypeGeneral.ID
            .VechileTypeDesc = objVechileTypeGeneral.Name

            intVechileColorID = 0
            Dim strColorIndName As String = ""
            If Not IsNothing(objDiscountProposalDtl.VechileColorIsActiveOnPK) Then
                If Not IsNothing(objDiscountProposalDtl.VechileColorIsActiveOnPK.VechileColor) Then
                    intVechileColorID = objDiscountProposalDtl.VechileColorIsActiveOnPK.VechileColor.ID
                    strColorIndName = objDiscountProposalDtl.VechileColorIsActiveOnPK.VechileColor.ColorIndName
                End If
            End If
            .VechileColorID = intVechileColorID
            .ColorIndName = strColorIndName
            .AssyYear = objDiscountProposalDtl.AssyYear
            .ModelYear = objDiscountProposalDtl.ModelYear
            .ProposeQty = objDiscountProposalDtl.ProposeQty

            .RedemptionPrice = lblHargaTebus.Text
            .BBN = txtBBN.Text
            .OtherCost = txtBiayaBiroJasaDLL.Text
            .DiscountRequest = txtPermohonanDiskonProgramFleetCustomer.Text
            .LogisticCost = lblLogisticCost.Text
            .RetailPriceOnRoad = txtRetailPriceOnTheRoad.Text
            .DeliveryCost = txtBiayaPengirimankeCustomer.Text
            .Accessories = txtAksesoris.Text
            .DealPriceEstimation = txtEstimasiDealPrice.Text
            .DiscountProposalDetail = objDiscountProposalDtl
        End With

        If ViewState("isEditRincianHarga") = False OrElse ViewState("isDuplikatRincianHarga") = True Then
            arlDiscountProposalDtl.Add(objDiscountProposalDtl)
            arlDiscountProposalDtlPrice.Add(objDiscountProposalDtlPrice)
        End If

        '---Update Parameter program Reguler semua baris Price  --
        UpdateAllRowsRincianKendaraan(objDiscountProposalDtlPrice, arlDiscountProposalDtlPrice, arlDiscountProposalPricetoParameter)
        '-----------------------------------------------------------

        sessHelper.SetSession(sessDiscountProposalDtl, arlDiscountProposalDtl)
        sessHelper.SetSession(sessDiscountProposalDtlPrice, arlDiscountProposalDtlPrice)
        sessHelper.SetSession(sessDiscountProposalPricetoParameter, arlDiscountProposalPricetoParameter)

        btnBatalSimpanGrid_Click(Nothing, Nothing)
    End Sub

    Private Sub UpdateAllRowsRincianKendaraan(ByVal objDiscountProposalDtlPrice As DiscountProposalDetailPrice, ByVal arlDiscountProposalDtlPrice As ArrayList, ByVal arlDiscountProposalPricetoParameter As ArrayList)
        Dim arrDelete2 As ArrayList = CType(sessHelper.GetSession(sessDeleteDiscountProposalPricetoParameter), ArrayList)
        If IsNothing(arrDelete2) Then arrDelete2 = New ArrayList
        Dim arrExistDPPriceToParam As New ArrayList
        Dim objDiscountProposalPricetoParameter As DiscountProposalPricetoParameter

        arlDiscountProposalDtlPrice = New System.Collections.ArrayList(
                                            (From obj As DiscountProposalDetailPrice In arlDiscountProposalDtlPrice.OfType(Of DiscountProposalDetailPrice)()
                                                Order By obj.NumberRow, obj.DiscountProposalDetail.ID
                                                Select obj).ToList())
        Dim arlDPPricetoParameterCurrent As ArrayList = New System.Collections.ArrayList(
                                                        (From obj As DiscountProposalPricetoParameter In arlDiscountProposalPricetoParameter.OfType(Of DiscountProposalPricetoParameter)()
                                                            Where obj.NumberRowParent = objDiscountProposalDtlPrice.NumberRow
                                                            Order By obj.NumberRowParent, obj.DiscountProposalParameter.ID
                                                            Select obj).ToList())

        For Each objDPDtlPrice As DiscountProposalDetailPrice In arlDiscountProposalDtlPrice
            If objDPDtlPrice.NumberRow <> objDiscountProposalDtlPrice.NumberRow Then
                arlDiscountProposalPricetoParameter = CType(sessHelper.GetSession(sessDiscountProposalPricetoParameter), ArrayList)
                If IsNothing(arlDiscountProposalPricetoParameter) Then arlDiscountProposalPricetoParameter = New ArrayList()
                arrDelete2 = CType(sessHelper.GetSession(sessDeleteDiscountProposalPricetoParameter), ArrayList)
                If IsNothing(arrDelete2) Then arrDelete2 = New ArrayList

                Dim arlDPPricetoParameterPerPrice As ArrayList = New System.Collections.ArrayList(
                                                                (From obj As DiscountProposalPricetoParameter In arlDiscountProposalPricetoParameter.OfType(Of DiscountProposalPricetoParameter)()
                                                                    Where obj.NumberRowParent = objDPDtlPrice.NumberRow
                                                                    Order By obj.NumberRowParent, obj.DiscountProposalParameter.ID
                                                                    Select obj).ToList())
                arrExistDPPriceToParam = New ArrayList
                If arlDPPricetoParameterCurrent.Count = 0 Then
                    For Each objDPPricetoParameter As DiscountProposalPricetoParameter In arlDPPricetoParameterPerPrice
                        arrExistDPPriceToParam.Add(objDPPricetoParameter)
                    Next
                    If arrExistDPPriceToParam.Count > 0 Then
                        For i As Integer = arlDiscountProposalPricetoParameter.Count - 1 To 0 Step -1
                            Dim objDPPricetoParameter As DiscountProposalPricetoParameter = CType(arlDiscountProposalPricetoParameter(i), DiscountProposalPricetoParameter)
                            If objDPPricetoParameter.NumberRowParent = objDPDtlPrice.NumberRow Then
                                For j As Integer = arrExistDPPriceToParam.Count - 1 To 0 Step -1
                                    Dim objExistDPPricetoParameter As DiscountProposalPricetoParameter = CType(arrExistDPPriceToParam(j), DiscountProposalPricetoParameter)
                                    If objDPPricetoParameter.NumberRowParent = objExistDPPricetoParameter.NumberRowParent Then
                                        If objDPPricetoParameter.DiscountProposalParameter.ID = objExistDPPricetoParameter.DiscountProposalParameter.ID Then
                                            If objDPPricetoParameter.ID = objExistDPPricetoParameter.ID Then
                                                If objDPPricetoParameter.ID > 0 Then
                                                    arrDelete2.Add(objDPPricetoParameter)
                                                End If
                                                arlDiscountProposalPricetoParameter.Remove(objDPPricetoParameter)
                                                Exit For
                                            End If
                                        End If
                                    End If
                                Next
                            End If
                        Next
                        sessHelper.SetSession(sessDeleteDiscountProposalPricetoParameter, arrDelete2)
                    End If
                Else
                    For Each objDPPricetoParameterCurrent As DiscountProposalPricetoParameter In arlDPPricetoParameterCurrent
                        Dim isExistRow As Boolean = False
                        For Each objDPPricetoParameter As DiscountProposalPricetoParameter In arlDPPricetoParameterPerPrice
                            If objDPPricetoParameter.DiscountProposalParameter.ID = objDPPricetoParameterCurrent.DiscountProposalParameter.ID Then
                                isExistRow = True
                                arrExistDPPriceToParam.Add(objDPPricetoParameter)
                                Exit For
                            End If
                        Next
                        If isExistRow = False Then
                            objDiscountProposalPricetoParameter = New DiscountProposalPricetoParameter
                            With objDiscountProposalPricetoParameter
                                .NumberRowParent = objDPDtlPrice.NumberRow
                                .DiscountProposalDetailPrice = objDPDtlPrice
                                .DiscountProposalParameter = objDPPricetoParameterCurrent.DiscountProposalParameter
                                .Amount = GetDiscountFromProgramReguler(objDPPricetoParameterCurrent.DiscountProposalParameter.ParameterName, objDPDtlPrice.VechileTypeID, objDiscountProposalDtlPrice.AssyYear, objDiscountProposalDtlPrice.ModelYear)
                            End With
                            arrExistDPPriceToParam.Add(objDiscountProposalPricetoParameter)
                            arlDiscountProposalPricetoParameter.Add(objDiscountProposalPricetoParameter)
                        End If
                    Next
                    If arrExistDPPriceToParam.Count > 0 Then
                        For i As Integer = arlDiscountProposalPricetoParameter.Count - 1 To 0 Step -1
                            Dim objDPPricetoParameter As DiscountProposalPricetoParameter = CType(arlDiscountProposalPricetoParameter(i), DiscountProposalPricetoParameter)
                            If objDPPricetoParameter.NumberRowParent = objDPDtlPrice.NumberRow Then
                                Dim isExistRow As Boolean = False
                                For j As Integer = arrExistDPPriceToParam.Count - 1 To 0 Step -1
                                    Dim objExistDPPricetoParameter As DiscountProposalPricetoParameter = CType(arrExistDPPriceToParam(j), DiscountProposalPricetoParameter)
                                    If objDPPricetoParameter.NumberRowParent = objExistDPPricetoParameter.NumberRowParent Then
                                        If objDPPricetoParameter.DiscountProposalParameter.ID = objExistDPPricetoParameter.DiscountProposalParameter.ID Then
                                            If objDPPricetoParameter.ID = objExistDPPricetoParameter.ID Then
                                                isExistRow = True
                                                Exit For
                                            End If
                                        End If
                                    End If
                                Next
                                If isExistRow = False Then
                                    If objDPPricetoParameter.ID > 0 Then
                                        arrDelete2.Add(objDPPricetoParameter)
                                    End If
                                    arlDiscountProposalPricetoParameter.Remove(objDPPricetoParameter)
                                End If
                            End If
                        Next
                        sessHelper.SetSession(sessDeleteDiscountProposalPricetoParameter, arrDelete2)
                    End If
                End If
                sessHelper.SetSession(sessDiscountProposalPricetoParameter, arlDiscountProposalPricetoParameter)
            End If
        Next
    End Sub

    Private Sub btnBatalSimpanGrid_Click(sender As Object, e As EventArgs)
        hdnModelYear.Value = ""
        hdnAssyYear.Value = ""
        hdnVechileTypeID.Value = ""
        hdnVechileColorID.Value = ""
        hdnSubTotalCostDealer.Value = ""
        ViewState("isEditRincianHarga") = False
        ViewState("isDuplikatRincianHarga") = False
        ViewState("isInsertCol") = False
        isInsert = True
        BindGridRincianHargaKendaraan()
    End Sub

    Private Sub lnkReloadHistoryPembelian_Click(sender As Object, e As EventArgs) Handles lnkReloadHistoryPembelian.Click
        BindGridHistoryPembelianReload()
    End Sub

    Private Sub lnkPopUpNameOnFaktur_Click(sender As Object, e As EventArgs) Handles lnkPopUpNameOnFaktur.Click
        hdnShowDataCustomer.Value = 1
        btnGetDataCustomer_Click(Nothing, Nothing)
    End Sub

    Private Sub dgHistoryPembelian_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles dgHistoryPembelian.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            Dim cell As TableCell = e.Row.Cells(0)
            cell.Width = New Unit("50px")
            Dim cell0 As TableCell = e.Row.Cells(1)
            cell0.Width = New Unit("250px")
            For i As Integer = 2 To e.Row.Cells.Count - 1
                Dim cell2 As TableCell = e.Row.Cells(i)
                cell2.Width = New Unit("100px")
            Next

            Dim cell3 As New TableCell
            For i As Integer = 0 To e.Row.Cells.Count - 1
                cell3 = e.Row.Cells(i)
                cell3.HorizontalAlign = HorizontalAlign.Center
            Next
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim cell As New TableCell
            For i As Integer = 2 To e.Row.Cells.Count - 1
                cell = e.Row.Cells(i)
                cell.HorizontalAlign = HorizontalAlign.Center
            Next
        End If
    End Sub

    Private Sub dgHistoryPembelian_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgHistoryPembelian.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = e.Row.RowIndex + 1 + (dgHistoryPembelian.PageIndex * dgHistoryPembelian.PageSize)
            If CType(e.Row.Cells(0).Text, Integer) = dgHistoryPembelian.VirtualItemCount Then
                e.Row.Cells(0).Text = String.Empty
            End If
        End If
    End Sub

    Private Sub dgProposedDiscount_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgProposedDiscount.ItemCommand
        Dim ddlFModelKendaraan As DropDownList, ddlEModelKendaraan As DropDownList, ddlFTipeKendaraan As DropDownList, ddlETipeKendaraan As DropDownList
        Dim txtFQuantity As System.Web.UI.WebControls.TextBox, txtEQuantity As System.Web.UI.WebControls.TextBox, ddlFDiscountType As DropDownList, ddlEDiscountType As DropDownList, txtFApplicationNo As System.Web.UI.WebControls.TextBox
        Dim txtEApplicationNo As System.Web.UI.WebControls.TextBox, txtFDetailDiscount As System.Web.UI.WebControls.TextBox, txtEDetailDiscount As System.Web.UI.WebControls.TextBox, txtFPriceReff As System.Web.UI.WebControls.TextBox, txtEPriceReff As System.Web.UI.WebControls.TextBox
        Dim txtFTOP As System.Web.UI.WebControls.TextBox, txtETOP As System.Web.UI.WebControls.TextBox, ddlFInterest As DropDownList, ddlEInterest As DropDownList, txtFDeliveryTime As System.Web.UI.WebControls.TextBox, txtEDeliveryTime As System.Web.UI.WebControls.TextBox

        Dim lblModelKendaraan As Label, lblTipeKendaraan As Label, lblQuantity As Label, lblDiscountType As Label, lblApplicationNo As Label, lblDetailDiscount As Label
        Dim lblPriceReff As Label, lblTOP As Label, lblInterest As Label, lblDeliveryTime As Label, lblSearchFApplicationNo As Label, lblSearchFPriceReff As Label
        Dim hdnFSPLID As HiddenField, hdnESPLID As HiddenField
        Dim lbtnEdit As LinkButton
        Dim lbtnDelete As LinkButton

        objDiscountProposalHeader = CType(sessHelper.GetSession(sessDiscountProposalHdr), DiscountProposalHeader)
        If IsNothing(objDiscountProposalHeader) Then objDiscountProposalHeader = New DiscountProposalHeader

        arlDiscountProposalDtlApprovalToSPL = CType(sessHelper.GetSession(sessDiscountProposalDtlApprovalToSPL), ArrayList)
        If IsNothing(arlDiscountProposalDtlApprovalToSPL) Then arlDiscountProposalDtlApprovalToSPL = New ArrayList
        arlDiscountProposalDtlApproval = CType(sessHelper.GetSession(sessDiscountProposalDtlApproval), ArrayList)
        If IsNothing(arlDiscountProposalDtlApproval) Then arlDiscountProposalDtlApproval = New ArrayList

        Select Case e.CommandName
            Case "add"
                ddlFModelKendaraan = CType(e.Item.FindControl("ddlFModelKendaraan"), DropDownList)
                ddlFTipeKendaraan = CType(e.Item.FindControl("ddlFTipeKendaraan"), DropDownList)
                txtFQuantity = CType(e.Item.FindControl("txtFQuantity"), System.Web.UI.WebControls.TextBox)
                ddlFDiscountType = CType(e.Item.FindControl("ddlFDiscountType"), DropDownList)
                txtFApplicationNo = CType(e.Item.FindControl("txtFApplicationNo"), System.Web.UI.WebControls.TextBox)
                txtFDetailDiscount = CType(e.Item.FindControl("txtFDetailDiscount"), System.Web.UI.WebControls.TextBox)
                txtFPriceReff = CType(e.Item.FindControl("txtFPriceReff"), System.Web.UI.WebControls.TextBox)
                txtFTOP = CType(e.Item.FindControl("txtFTOP"), System.Web.UI.WebControls.TextBox)
                ddlFInterest = CType(e.Item.FindControl("ddlFInterest"), DropDownList)
                txtFDeliveryTime = CType(e.Item.FindControl("txtFDeliveryTime"), System.Web.UI.WebControls.TextBox)
                hdnFSPLID = CType(e.Item.FindControl("hdnFSPLID"), HiddenField)

                If ddlFModelKendaraan.SelectedIndex = 0 Then
                    MessageBox.Show("Model Kendaraan harus dipilih.")
                    Exit Sub
                End If
                If ddlFTipeKendaraan.SelectedIndex = 0 Then
                    MessageBox.Show("Tipe Kendaraan harus dipilih.")
                    Exit Sub
                End If
                If txtFQuantity.Text.Trim = String.Empty OrElse txtFQuantity.Text.Trim = "0" Then
                    MessageBox.Show("Qty Unit harus diisi atau harus lebih dari 0")
                    Exit Sub
                End If
                If ddlFDiscountType.SelectedIndex = 0 Then
                    MessageBox.Show("Discount Type harus dipilih.")
                    Exit Sub
                End If
                Dim strDiscountTypeCode As String = String.Empty
                Dim objDiscountMaster As DiscountMaster = New DiscountMasterFacade(User).Retrieve(CInt(ddlFDiscountType.SelectedValue))
                If Not IsNothing(objDiscountMaster) Then strDiscountTypeCode = objDiscountMaster.Code
                If strDiscountTypeCode <> "D01" Then   '--> Fleet/Government Discount
                    If txtFApplicationNo.Text = String.Empty Then
                        'MessageBox.Show("Application No harus diisi.")
                        'Exit Sub
                    End If
                End If
                If txtFDetailDiscount.Enabled = True Then
                    If txtFDetailDiscount.Text.Trim = String.Empty OrElse txtFDetailDiscount.Text.Trim = "0" Then
                        MessageBox.Show("Detail Discount harus diisi atau harus lebih dari 0")
                        Exit Sub
                    End If
                End If
                If txtFPriceReff.Text = String.Empty Then
                    MessageBox.Show("Price Reff harus diisi.")
                    Exit Sub
                End If
                If txtFTOP.Text.Trim = String.Empty Then
                    txtFTOP.Text = 0
                End If
                'If ddlFInterest.SelectedIndex = 0 Then
                '    MessageBox.Show("Interest harus dipilih.")
                '    Exit Sub
                'End If
                If txtFDeliveryTime.Text.Trim = "" Then
                    MessageBox.Show("Delivery Time harus dipilih.")
                    Exit Sub
                End If
                Dim dtePriceReff As DateTime
                Try
                    If txtFPriceReff.Text.Trim <> "Update Price" Then
                        If Len(txtFPriceReff.Text.Trim) < 6 Then
                            MessageBox.Show("Format bulan tahun salah")
                            Exit Sub
                        End If
                        If IsNumeric(txtFPriceReff.Text.Trim) Then
                            If Len(txtFPriceReff.Text.Trim) = 6 Then
                                If Mid(txtFPriceReff.Text.Trim, 3, 2) < 20 Then
                                    MessageBox.Show("Format bulan tahun salah")
                                    Exit Sub
                                End If
                                If CInt(Left(txtFPriceReff.Text, 2)) < 1 OrElse CInt(Left(txtFPriceReff.Text, 2)) > 12 Then
                                    MessageBox.Show("Format bulan salah")
                                    Exit Sub
                                End If
                                dtePriceReff = CDate("01" & "/" & Left(txtFPriceReff.Text, 2) & "/" & Mid(txtFPriceReff.Text, 3, 4))
                            Else
                                MessageBox.Show("Format bulan tahun salah")
                                Exit Sub
                            End If
                        Else
                            MessageBox.Show("Format bulan tahun salah")
                            Exit Sub
                        End If
                    Else
                        dtePriceReff = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
                    End If

                    If Len(txtFDeliveryTime.Text.Trim) < 6 Then
                        MessageBox.Show("Format bulan tahun salah")
                        Exit Sub
                    End If
                    If IsNumeric(txtFDeliveryTime.Text.Trim) Then
                        If Len(txtFDeliveryTime.Text.Trim) = 6 Then
                            If Mid(txtFDeliveryTime.Text.Trim, 3, 2) < 20 Then
                                MessageBox.Show("Format bulan tahun salah")
                                Exit Sub
                            End If
                            If CInt(Left(txtFDeliveryTime.Text, 2)) < 1 OrElse CInt(Left(txtFDeliveryTime.Text, 2)) > 12 Then
                                MessageBox.Show("Format bulan salah")
                                Exit Sub
                            End If
                        Else
                            MessageBox.Show("Format bulan tahun salah")
                            Exit Sub
                        End If
                    Else
                        MessageBox.Show("Format bulan tahun salah")
                        Exit Sub
                    End If
                Catch
                End Try

                If txtFApplicationNo.Text.Trim <> "" Then
                    Dim criterias0 As New CriteriaComposite(New Criteria(GetType(SPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias0.opAnd(New Criteria(GetType(SPL), "ApprovalStatus", MatchType.Exact, 5))
                    criterias0.opAnd(New Criteria(GetType(SPL), "DealerName", MatchType.[Partial], txtDealerCode.Text))

                    Dim strSQL As String = String.Empty
                    If ddlFDiscountType.SelectedValue <> "" Then
                        strSQL = "Select distinct a.ID from SPL a join SPLDetail b on a.ID = b.SPLID and b.RowStatus = 0 "
                        strSQL += "join SPLDetailtoSPL c on b.ID = c.SPLDetailID and c.RowStatus = 0 "
                        strSQL += "where a.RowStatus = 0 And c.DiscountMasterID = " & ddlFDiscountType.SelectedValue
                        criterias0.opAnd(New Criteria(GetType(SPL), "ID", MatchType.InSet, "(" & strSQL & ")"))
                    End If
                    If ddlFTipeKendaraan.SelectedValue <> "" Then
                        strSQL = "Select a.ID From SPL a join SPLDetail b on a.ID = b.SPLID "
                        strSQL += "Where a.RowStatus = 0 and b.RowStatus = 0 and b.VehicleTypeID = " & ddlFTipeKendaraan.SelectedValue
                        criterias0.opAnd(New Criteria(GetType(SPL), "ID", MatchType.InSet, "(" & strSQL & ")"))
                    End If
                    criterias0.opAnd(New Criteria(GetType(SPL), "SPLNumber", MatchType.[Partial], txtFApplicationNo.Text))
                    Dim arrSPL As ArrayList = New SPLFacade(User).Retrieve(criterias0)
                    If Not IsNothing(arrSPL) AndAlso arrSPL.Count > 0 Then
                    Else
                        MessageBox.Show("Application No. tidak ada di data Pencarian Aplikasi")
                        txtFApplicationNo.Focus()
                        Exit Sub
                    End If
                End If

                Dim strLabelTotal As String = ""
                Dim oDiscountProposalDtlApproval As DiscountProposalDetailApproval
                For Each objDtlApprovalToSPL As DiscountProposalDetailApprovaltoSPL In arlDiscountProposalDtlApprovalToSPL
                    strLabelTotal = ""
                    If Not IsNothing(objDtlApprovalToSPL.LabelTotal) Then strLabelTotal = objDtlApprovalToSPL.LabelTotal
                    If strLabelTotal.ToLower <> "total discount :" Then
                        oDiscountProposalDtlApproval = New DiscountProposalDetailApproval
                        For Each objDiscountProposalDtlApproval As DiscountProposalDetailApproval In arlDiscountProposalDtlApproval
                            If objDiscountProposalDtlApproval.VechileTypeID = objDtlApprovalToSPL.VechileTypeID AndAlso objDiscountProposalDtlApproval.ModelID = objDtlApprovalToSPL.ModelID Then
                                oDiscountProposalDtlApproval = objDiscountProposalDtlApproval
                                Exit For
                            End If
                        Next
                        If Not IsNothing(objDtlApprovalToSPL.DiscountProposalDetailApproval) Then
                            oDiscountProposalDtlApproval = objDtlApprovalToSPL.DiscountProposalDetailApproval
                        End If
                        Dim objSubCategoryVehicleToModel As New SubCategoryVehicleToModel
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicleToModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(SubCategoryVehicleToModel), "VechileModel.ID", MatchType.Exact, oDiscountProposalDtlApproval.VechileType.VechileModel.ID))
                        Dim arrSCVToModel As ArrayList = New SubCategoryVehicleToModelFacade(User).Retrieve(criterias)
                        If Not IsNothing(arrSCVToModel) AndAlso arrSCVToModel.Count > 0 Then
                            objSubCategoryVehicleToModel = CType(arrSCVToModel(0), SubCategoryVehicleToModel)
                        End If

                        Dim strSPLID As String = ""
                        Dim objSPLDetail As SPLDetail
                        If objSubCategoryVehicleToModel.SubCategoryVehicle.ID = ddlFModelKendaraan.SelectedValue Then
                            If oDiscountProposalDtlApproval.VechileType.ID = ddlFTipeKendaraan.SelectedValue Then
                                If objDtlApprovalToSPL.DiscountMaster.ID = ddlFDiscountType.SelectedValue Then
                                    If Not IsNothing(objDtlApprovalToSPL.SPLDetail) Then
                                        objSPLDetail = objDtlApprovalToSPL.SPLDetail
                                        If Not IsNothing(objSPLDetail.SPL) Then
                                            strSPLID = objSPLDetail.SPL.ID.ToString
                                        End If
                                    End If
                                    If strSPLID.ToString.ToLower.Trim = hdnFSPLID.Value.ToLower.Trim Then
                                        MessageBox.Show("Model, Tipe, Discount Type dan Application No. sudah pernah di input.")
                                        Exit Sub
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next

                oDiscountProposalDtlApproval = New DiscountProposalDetailApproval
                Dim oDiscountProposalDtlApprovaltoSPL As New DiscountProposalDetailApprovaltoSPL
                With oDiscountProposalDtlApprovaltoSPL
                    .NumberRow = arlDiscountProposalDtlApprovalToSPL.Count + 1

                    Dim objSPLDetail As SPLDetail
                    If hdnFSPLID.Value <> "" Then
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPLDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(SPLDetail), "PeriodMonth", MatchType.Exact, DateTime.Now.Month))
                        criterias.opAnd(New Criteria(GetType(SPLDetail), "PeriodYear", MatchType.Exact, DateTime.Now.Year))
                        criterias.opAnd(New Criteria(GetType(SPLDetail), "SPL.ID", MatchType.Exact, hdnFSPLID.Value))
                        criterias.opAnd(New Criteria(GetType(SPLDetail), "VechileType.ID", MatchType.Exact, ddlFTipeKendaraan.SelectedValue))
                        
                        Dim arrSPLDtl As ArrayList = New SPLDetailFacade(User).Retrieve(criterias)
                        If Not IsNothing(arrSPLDtl) AndAlso arrSPLDtl.Count > 0 Then
                            If Not IsNothing(arrSPLDtl) AndAlso arrSPLDtl.Count > 0 Then
                                objSPLDetail = CType(arrSPLDtl(0), SPLDetail)
                            End If
                        Else
                            Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPLDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias2.opAnd(New Criteria(GetType(SPLDetail), "SPL.ID", MatchType.Exact, hdnFSPLID.Value))
                            criterias2.opAnd(New Criteria(GetType(SPLDetail), "VechileType.ID", MatchType.Exact, ddlFTipeKendaraan.SelectedValue))
                            arrSPLDtl = New SPLDetailFacade(User).Retrieve(criterias2)
                            If Not IsNothing(arrSPLDtl) AndAlso arrSPLDtl.Count > 0 Then
                                objSPLDetail = CType(arrSPLDtl(0), SPLDetail)
                            End If
                        End If
                    End If
                    .SPLDetail = objSPLDetail
                    .DiscountMaster = New DiscountMasterFacade(User).Retrieve(CInt(ddlFDiscountType.SelectedValue))
                    .DiscountProposed = txtFDetailDiscount.Text
                    .VechileTypeID = ddlFTipeKendaraan.SelectedValue
                    .ModelID = ddlFModelKendaraan.SelectedValue

                    oDiscountProposalDtlApproval.DiscountProposalHeader = objDiscountProposalHeader
                    oDiscountProposalDtlApproval.VechileType = New VechileTypeFacade(User).Retrieve(CInt(ddlFTipeKendaraan.SelectedValue))
                    oDiscountProposalDtlApproval.ResponseQty = txtFQuantity.Text
                    oDiscountProposalDtlApproval.PriceReff = dtePriceReff
                    oDiscountProposalDtlApproval.MaxTOPDay = txtFTOP.Text
                    oDiscountProposalDtlApproval.FreeIntIndicator = ddlFInterest.SelectedValue
                    oDiscountProposalDtlApproval.DeliveryDate = CDate("01" & "/" & Left(txtFDeliveryTime.Text, 2) & "/" & Mid(txtFDeliveryTime.Text, 3, 4))
                    oDiscountProposalDtlApproval.VechileTypeID = ddlFTipeKendaraan.SelectedValue
                    oDiscountProposalDtlApproval.ModelID = ddlFModelKendaraan.SelectedValue
                End With
                Dim IsExistTipe As Boolean = False
                For Each oDPA As DiscountProposalDetailApproval In arlDiscountProposalDtlApproval
                    If oDPA.ModelID = oDiscountProposalDtlApproval.ModelID AndAlso oDPA.VechileTypeID = oDiscountProposalDtlApproval.VechileTypeID Then
                        IsExistTipe = True
                        Exit For
                    End If
                Next
                If IsExistTipe = False Then
                    arlDiscountProposalDtlApproval.Add(oDiscountProposalDtlApproval)
                End If
                arlDiscountProposalDtlApprovalToSPL.Add(oDiscountProposalDtlApprovaltoSPL)

            Case "save"
                ddlEModelKendaraan = CType(e.Item.FindControl("ddlEModelKendaraan"), DropDownList)
                ddlETipeKendaraan = CType(e.Item.FindControl("ddlETipeKendaraan"), DropDownList)
                txtEQuantity = CType(e.Item.FindControl("txtEQuantity"), System.Web.UI.WebControls.TextBox)
                ddlEDiscountType = CType(e.Item.FindControl("ddlEDiscountType"), DropDownList)
                txtEApplicationNo = CType(e.Item.FindControl("txtEApplicationNo"), System.Web.UI.WebControls.TextBox)
                txtEDetailDiscount = CType(e.Item.FindControl("txtEDetailDiscount"), System.Web.UI.WebControls.TextBox)
                txtEPriceReff = CType(e.Item.FindControl("txtEPriceReff"), System.Web.UI.WebControls.TextBox)
                txtETOP = CType(e.Item.FindControl("txtETOP"), System.Web.UI.WebControls.TextBox)
                ddlEInterest = CType(e.Item.FindControl("ddlEInterest"), DropDownList)
                txtEDeliveryTime = CType(e.Item.FindControl("txtEDeliveryTime"), System.Web.UI.WebControls.TextBox)
                hdnESPLID = CType(e.Item.FindControl("hdnESPLID"), HiddenField)

                If ddlEModelKendaraan.SelectedIndex = 0 Then
                    MessageBox.Show("Model Kendaraan harus dipilih.")
                    Exit Sub
                End If
                If ddlETipeKendaraan.SelectedIndex = 0 Then
                    MessageBox.Show("Tipe Kendaraan harus dipilih.")
                    Exit Sub
                End If
                If txtEQuantity.Text.Trim = String.Empty OrElse txtEQuantity.Text.Trim = "0" Then
                    MessageBox.Show("Qty Unit harus diisi atau harus lebih dari 0")
                    Exit Sub
                End If
                If ddlEDiscountType.SelectedIndex = 0 Then
                    MessageBox.Show("Discount Type harus dipilih.")
                    Exit Sub
                End If
                Dim strDiscountTypeCode As String = String.Empty
                Dim objDiscountMaster As DiscountMaster = New DiscountMasterFacade(User).Retrieve(CInt(ddlEDiscountType.SelectedValue))
                If Not IsNothing(objDiscountMaster) Then strDiscountTypeCode = objDiscountMaster.Code
                If strDiscountTypeCode <> "D01" Then   '--> Fleet/Government Discount
                    If txtEApplicationNo.Text = String.Empty Then
                        'MessageBox.Show("Application No harus diisi.")
                        'Exit Sub
                    End If
                End If
                If txtEDetailDiscount.Enabled = True Then
                    If txtEDetailDiscount.Text.Trim = String.Empty OrElse txtEDetailDiscount.Text.Trim = "0" Then
                        MessageBox.Show("Detail Discount harus diisi atau harus lebih dari 0")
                        Exit Sub
                    End If
                End If
                If txtEPriceReff.Text = String.Empty Then
                    MessageBox.Show("Price Reff harus diisi.")
                    Exit Sub
                End If
                If txtETOP.Text.Trim = String.Empty Then
                    txtETOP.Text = 0
                End If
                'If ddlEInterest.SelectedIndex = 0 Then
                '    MessageBox.Show("Interest harus dipilih.")
                '    Exit Sub
                'End If
                If txtEDeliveryTime.Text.Trim = "" Then
                    MessageBox.Show("Delivery Time harus dipilih.")
                    Exit Sub
                End If

                Dim dtePriceReff As DateTime
                If txtEPriceReff.Text.Trim <> "Update Price" Then
                    If Len(txtEPriceReff.Text.Trim) < 6 Then
                        MessageBox.Show("Format bulan tahun salah")
                        Exit Sub
                    End If
                    If Mid(txtEPriceReff.Text.Trim, 3, 2) < 20 Then
                        MessageBox.Show("Format bulan tahun salah")
                        Exit Sub
                    End If
                    If CInt(Left(txtEPriceReff.Text, 2)) < 1 OrElse CInt(Left(txtEPriceReff.Text, 2)) > 12 Then
                        MessageBox.Show("Format bulan salah")
                        Exit Sub
                    End If
                    dtePriceReff = CDate("01" & "/" & Left(txtEPriceReff.Text, 2) & "/" & Mid(txtEPriceReff.Text, 3, 4))
                Else
                    dtePriceReff = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
                End If
                If Len(txtEDeliveryTime.Text.Trim) < 6 Then
                    MessageBox.Show("Format bulan tahun salah")
                    Exit Sub
                End If
                Try
                    If Mid(txtEDeliveryTime.Text.Trim, 3, 2) < 20 Then
                        MessageBox.Show("Format bulan tahun salah")
                        Exit Sub
                    End If
                    If CInt(Left(txtEDeliveryTime.Text, 2)) < 1 OrElse CInt(Left(txtEDeliveryTime.Text, 2)) > 12 Then
                        MessageBox.Show("Format bulan salah")
                        Exit Sub
                    End If
                Catch
                End Try

                If txtEApplicationNo.Text.Trim <> "" Then
                    Dim strSQL As String = String.Empty
                    Dim criterias0 As New CriteriaComposite(New Criteria(GetType(SPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias0.opAnd(New Criteria(GetType(SPL), "ApprovalStatus", MatchType.Exact, 5))
                    criterias0.opAnd(New Criteria(GetType(SPL), "DealerName", MatchType.[Partial], txtDealerCode.Text))
                    If ddlEDiscountType.SelectedValue <> "" Then
                        strSQL = "Select distinct a.ID from SPL a join SPLDetail b on a.ID = b.SPLID and b.RowStatus = 0 "
                        strSQL += "join SPLDetailtoSPL c on b.ID = c.SPLDetailID and c.RowStatus = 0 "
                        strSQL += "where a.RowStatus = 0 And c.DiscountMasterID = " & ddlEDiscountType.SelectedValue
                        criterias0.opAnd(New Criteria(GetType(SPL), "ID", MatchType.InSet, "(" & strSQL & ")"))
                    End If
                    If ddlETipeKendaraan.SelectedValue <> "" Then
                        strSQL = "Select a.ID From SPL a join SPLDetail b on a.ID = b.SPLID "
                        strSQL += "Where a.RowStatus = 0 And b.RowStatus = 0 And b.VehicleTypeID = " & ddlETipeKendaraan.SelectedValue
                        criterias0.opAnd(New Criteria(GetType(SPL), "ID", MatchType.InSet, "(" & strSQL & ")"))
                    End If
                    criterias0.opAnd(New Criteria(GetType(SPL), "SPLNumber", MatchType.[Partial], txtEApplicationNo.Text))
                    Dim arrSPL As ArrayList = New SPLFacade(User).Retrieve(criterias0)
                    If Not IsNothing(arrSPL) AndAlso arrSPL.Count > 0 Then
                    Else
                        MessageBox.Show("Application No. tidak ada di data Pencarian Aplikasi")
                        txtEApplicationNo.Focus()
                        Exit Sub
                    End If
                End If

                Dim oDiscountProposalDtlApprovaltoSPL As DiscountProposalDetailApprovaltoSPL = CType(arlDiscountProposalDtlApprovalToSPL(e.Item.ItemIndex), DiscountProposalDetailApprovaltoSPL)

                Dim oDiscountProposalDtlApproval As New DiscountProposalDetailApproval
                Dim strLabelTotal As String = ""
                For Each objDtlApprovalToSPL As DiscountProposalDetailApprovaltoSPL In arlDiscountProposalDtlApprovalToSPL
                    oDiscountProposalDtlApproval = New DiscountProposalDetailApproval
                    For Each objDiscountProposalDtlApproval As DiscountProposalDetailApproval In arlDiscountProposalDtlApproval
                        If objDiscountProposalDtlApproval.VechileTypeID = objDtlApprovalToSPL.VechileTypeID AndAlso objDiscountProposalDtlApproval.ModelID = objDtlApprovalToSPL.ModelID Then
                            oDiscountProposalDtlApproval = objDiscountProposalDtlApproval
                            Exit For
                        End If
                    Next
                    If Not IsNothing(objDtlApprovalToSPL.DiscountProposalDetailApproval) Then
                        oDiscountProposalDtlApproval = objDtlApprovalToSPL.DiscountProposalDetailApproval
                    End If
                    strLabelTotal = ""
                    If Not IsNothing(objDtlApprovalToSPL.LabelTotal) Then strLabelTotal = objDtlApprovalToSPL.LabelTotal
                    If strLabelTotal.ToLower <> "total discount :" Then
                        Dim objSubCategoryVehicleToModel As New SubCategoryVehicleToModel
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicleToModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(SubCategoryVehicleToModel), "VechileModel.ID", MatchType.Exact, oDiscountProposalDtlApproval.VechileType.VechileModel.ID))
                        Dim arrSCVToModel As ArrayList = New SubCategoryVehicleToModelFacade(User).Retrieve(criterias)
                        If Not IsNothing(arrSCVToModel) AndAlso arrSCVToModel.Count > 0 Then
                            objSubCategoryVehicleToModel = CType(arrSCVToModel(0), SubCategoryVehicleToModel)
                        End If
                        Dim strSPLID As String = ""
                        Dim objSPLDetail As SPLDetail
                        If objSubCategoryVehicleToModel.SubCategoryVehicle.ID = ddlEModelKendaraan.SelectedValue Then
                            If oDiscountProposalDtlApproval.VechileType.ID = ddlETipeKendaraan.SelectedValue Then
                                If objDtlApprovalToSPL.DiscountMaster.ID = ddlEDiscountType.SelectedValue Then
                                    If Not IsNothing(objDtlApprovalToSPL.SPLDetail) Then
                                        objSPLDetail = objDtlApprovalToSPL.SPLDetail
                                        If Not IsNothing(objSPLDetail.SPL) Then
                                            strSPLID = objSPLDetail.SPL.ID.ToString
                                        End If
                                    End If
                                    If strSPLID.ToString.ToLower.Trim = hdnESPLID.Value.ToLower.Trim Then
                                        If objDtlApprovalToSPL.NumberRow <> (e.Item.ItemIndex + 1) Then
                                            MessageBox.Show("Model, Tipe, Discount Type dan Application No. sudah pernah di input.")
                                            Exit Sub
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next

                oDiscountProposalDtlApproval = New DiscountProposalDetailApproval
                For Each objDiscountProposalDtlApproval As DiscountProposalDetailApproval In arlDiscountProposalDtlApproval
                    If objDiscountProposalDtlApproval.VechileTypeID = oDiscountProposalDtlApprovaltoSPL.VechileTypeID _
                        AndAlso objDiscountProposalDtlApproval.ModelID = oDiscountProposalDtlApprovaltoSPL.ModelID Then
                        oDiscountProposalDtlApproval = objDiscountProposalDtlApproval
                        Exit For
                    End If
                Next

                With oDiscountProposalDtlApprovaltoSPL
                    oDiscountProposalDtlApproval.DiscountProposalHeader = objDiscountProposalHeader
                    oDiscountProposalDtlApproval.VechileType = New VechileTypeFacade(User).Retrieve(CInt(ddlETipeKendaraan.SelectedValue))
                    oDiscountProposalDtlApproval.ResponseQty = txtEQuantity.Text
                    oDiscountProposalDtlApproval.PriceReff = dtePriceReff
                    oDiscountProposalDtlApproval.MaxTOPDay = txtETOP.Text
                    oDiscountProposalDtlApproval.FreeIntIndicator = ddlEInterest.SelectedValue
                    oDiscountProposalDtlApproval.DeliveryDate = CDate("01" & "/" & Left(txtEDeliveryTime.Text, 2) & "/" & Mid(txtEDeliveryTime.Text, 3, 4))
                    oDiscountProposalDtlApproval.VechileTypeID = ddlETipeKendaraan.SelectedValue
                    oDiscountProposalDtlApproval.ModelID = ddlEModelKendaraan.SelectedValue

                    .DiscountProposalDetailApproval = oDiscountProposalDtlApproval
                    Dim objSPLDetail As SPLDetail
                    If hdnESPLID.Value <> "" Then
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPLDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(SPLDetail), "PeriodMonth", MatchType.Exact, DateTime.Now.Month))
                        criterias.opAnd(New Criteria(GetType(SPLDetail), "PeriodYear", MatchType.Exact, DateTime.Now.Year))
                        criterias.opAnd(New Criteria(GetType(SPLDetail), "SPL.ID", MatchType.Exact, hdnESPLID.Value))
                        criterias.opAnd(New Criteria(GetType(SPLDetail), "VechileType.ID", MatchType.Exact, ddlETipeKendaraan.SelectedValue))

                        Dim arrSPLDtl As ArrayList = New SPLDetailFacade(User).Retrieve(criterias)
                        If Not IsNothing(arrSPLDtl) AndAlso arrSPLDtl.Count > 0 Then
                            If Not IsNothing(arrSPLDtl) AndAlso arrSPLDtl.Count > 0 Then
                                objSPLDetail = CType(arrSPLDtl(0), SPLDetail)
                            End If
                        Else
                            Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPLDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(SPLDetail), "SPL.ID", MatchType.Exact, hdnESPLID.Value))
                            criterias.opAnd(New Criteria(GetType(SPLDetail), "VechileType.ID", MatchType.Exact, ddlETipeKendaraan.SelectedValue))
                            arrSPLDtl = New SPLDetailFacade(User).Retrieve(criterias2)

                            If Not IsNothing(arrSPLDtl) AndAlso arrSPLDtl.Count > 0 Then
                                objSPLDetail = CType(arrSPLDtl(0), SPLDetail)
                            End If
                        End If
                        
                    End If
                    .SPLDetail = objSPLDetail
                    .DiscountMaster = New DiscountMasterFacade(User).Retrieve(CInt(ddlEDiscountType.SelectedValue))
                    .DiscountProposed = txtEDetailDiscount.Text
                    .VechileTypeID = ddlETipeKendaraan.SelectedValue
                    .ModelID = ddlEModelKendaraan.SelectedValue
                End With
                dgProposedDiscount.EditItemIndex = -1
                dgProposedDiscount.ShowFooter = True

            Case "edit"
                dgProposedDiscount.ShowFooter = False
                dgProposedDiscount.EditItemIndex = e.Item.ItemIndex

            Case "delete"
                Try
                    Dim oDiscountProposalDtlApprovaltoSPL As DiscountProposalDetailApprovaltoSPL = CType(arlDiscountProposalDtlApprovalToSPL(e.Item.ItemIndex), DiscountProposalDetailApprovaltoSPL)
                    If oDiscountProposalDtlApprovaltoSPL.ID > 0 Then
                        Dim arrDelete As ArrayList = CType(sessHelper.GetSession(sessDeleteDiscountProposalDtlApprovalToSPL), ArrayList)
                        If IsNothing(arrDelete) Then arrDelete = New ArrayList
                        arrDelete.Add(oDiscountProposalDtlApprovaltoSPL)
                        sessHelper.SetSession(sessDeleteDiscountProposalDtlApprovalToSPL, arrDelete)
                    End If
                    arlDiscountProposalDtlApprovalToSPL.RemoveAt(e.Item.ItemIndex)

                    Dim arlDiscountProposalDtlApprovalToSPLPerType As ArrayList = New ArrayList
                    arlDiscountProposalDtlApprovalToSPLPerType = New System.Collections.ArrayList(
                                                            (From obj As DiscountProposalDetailApprovaltoSPL In arlDiscountProposalDtlApprovalToSPL.OfType(Of DiscountProposalDetailApprovaltoSPL)()
                                                                Where obj.VechileTypeID = oDiscountProposalDtlApprovaltoSPL.VechileTypeID _
                                                                And obj.ModelID = oDiscountProposalDtlApprovaltoSPL.ModelID
                                                                Order By obj.ModelID, obj.VechileTypeID
                                                                Select obj).ToList())

                    If arlDiscountProposalDtlApprovalToSPLPerType.Count = 0 Then
                        Dim i% = 0
                        Dim oDiscountProposalDtlApproval As New DiscountProposalDetailApproval
                        For Each objDiscountProposalDtlApproval As DiscountProposalDetailApproval In arlDiscountProposalDtlApproval
                            If objDiscountProposalDtlApproval.VechileTypeID = oDiscountProposalDtlApprovaltoSPL.VechileTypeID AndAlso _
                                objDiscountProposalDtlApproval.ModelID = oDiscountProposalDtlApprovaltoSPL.ModelID Then
                                oDiscountProposalDtlApproval = objDiscountProposalDtlApproval
                                Exit For
                            End If
                            i += 1
                        Next
                        If oDiscountProposalDtlApproval.ID > 0 Then
                            Dim arrDelete As ArrayList = CType(sessHelper.GetSession(sessDeleteDiscountProposalDtlApproval), ArrayList)
                            If IsNothing(arrDelete) Then arrDelete = New ArrayList
                            arrDelete.Add(oDiscountProposalDtlApproval)
                            sessHelper.SetSession(sessDeleteDiscountProposalDtlApproval, arrDelete)
                        End If
                        arlDiscountProposalDtlApproval.RemoveAt(i)
                    End If
                    sessHelper.SetSession(sessDiscountProposalDtlApproval, arlDiscountProposalDtlApproval)
                Catch
                End Try

            Case "cancel"
                dgProposedDiscount.EditItemIndex = -1
                dgProposedDiscount.ShowFooter = True
        End Select

        sessHelper.SetSession(sessDiscountProposalDtlApproval, arlDiscountProposalDtlApproval)
        sessHelper.SetSession(sessDiscountProposalDtlApprovalToSPL, arlDiscountProposalDtlApprovalToSPL)
        BindGridProposedDiscount()
    End Sub

    Private Sub dgProposedDiscount_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgProposedDiscount.ItemDataBound
        Dim ddlFModelKendaraan As DropDownList, ddlEModelKendaraan As DropDownList, ddlFTipeKendaraan As DropDownList, ddlETipeKendaraan As DropDownList
        Dim txtFQuantity As System.Web.UI.WebControls.TextBox, txtEQuantity As System.Web.UI.WebControls.TextBox, ddlFDiscountType As DropDownList, ddlEDiscountType As DropDownList, txtFApplicationNo As System.Web.UI.WebControls.TextBox
        Dim txtEApplicationNo As System.Web.UI.WebControls.TextBox, txtFDetailDiscount As System.Web.UI.WebControls.TextBox, txtEDetailDiscount As System.Web.UI.WebControls.TextBox, txtFPriceReff As System.Web.UI.WebControls.TextBox, txtEPriceReff As System.Web.UI.WebControls.TextBox
        Dim txtFTOP As System.Web.UI.WebControls.TextBox, txtETOP As System.Web.UI.WebControls.TextBox, ddlFInterest As DropDownList, ddlEInterest As DropDownList, txtFDeliveryTime As System.Web.UI.WebControls.TextBox, txtEDeliveryTime As System.Web.UI.WebControls.TextBox

        Dim lblModelKendaraan As Label, lblTipeKendaraan As Label, lblQuantity As Label, lblDiscountType As Label, lblApplicationNo As Label, lblDetailDiscount As Label
        Dim lblPriceReff As Label, lblTOP As Label, lblInterest As Label, lblDeliveryTime As Label, lblSearchFApplicationNo As Label, lblSearchFPriceReff As Label
        Dim hdnESPLID As HiddenField, hdnLabelTotal As HiddenField
        Dim lblSearchEApplicationNo As Label, lblSearchEPriceReff As Label, lblNo As Label
        Dim lbtnEdit As LinkButton
        Dim lbtnDelete As LinkButton

        arlDiscountProposalDtlApprovalToSPL = CType(sessHelper.GetSession(sessDiscountProposalDtlApprovalToSPL), ArrayList)
        If IsNothing(arlDiscountProposalDtlApprovalToSPL) Then arlDiscountProposalDtlApprovalToSPL = New ArrayList
        arlDiscountProposalDtlApproval = CType(sessHelper.GetSession(sessDiscountProposalDtlApproval), ArrayList)
        If IsNothing(arlDiscountProposalDtlApproval) Then arlDiscountProposalDtlApproval = New ArrayList

        Dim intItemIndexx As Integer = 0
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oDPAToSPL As DiscountProposalDetailApprovaltoSPL = CType(e.Item.DataItem, DiscountProposalDetailApprovaltoSPL)
            Dim strLabelTotalDisc As String = If(IsNothing(oDPAToSPL.LabelTotal), "", oDPAToSPL.LabelTotal)
            lblNo = CType(e.Item.FindControl("lblNo"), Label)

            If e.Item.ItemIndex = 0 Then
                ViewState("ItemIndexx") = e.Item.ItemIndex
                intItemIndexx = CType(ViewState("ItemIndexx"), Integer)
                lblNo.Text = intItemIndexx + 1 + (dgProposedDiscount.CurrentPageIndex * dgProposedDiscount.PageSize)
                ViewState("ItemIndexx") = lblNo.Text
            Else
                If strLabelTotalDisc.Trim.ToLower = "total discount :" Then
                    ViewState("ItemIndexx") = 0
                Else
                    intItemIndexx = CType(ViewState("ItemIndexx"), Integer)
                    lblNo.Text = intItemIndexx + 1 + (dgProposedDiscount.CurrentPageIndex * dgProposedDiscount.PageSize)
                    ViewState("ItemIndexx") = lblNo.Text
                End If
            End If

            Dim i% = 0
            Dim oDPA As DiscountProposalDetailApproval
            If arlDiscountProposalDtlApproval.Count > 0 Then
                For Each objDiscountProposalDtlApproval As DiscountProposalDetailApproval In arlDiscountProposalDtlApproval
                    If objDiscountProposalDtlApproval.VechileTypeID = oDPAToSPL.VechileTypeID AndAlso objDiscountProposalDtlApproval.ModelID = oDPAToSPL.ModelID Then
                        oDPA = objDiscountProposalDtlApproval
                        Exit For
                    End If
                    i += 1
                Next
            End If

            lblModelKendaraan = CType(e.Item.FindControl("lblModelKendaraan"), Label)
            lblTipeKendaraan = CType(e.Item.FindControl("lblTipeKendaraan"), Label)
            lblQuantity = CType(e.Item.FindControl("lblQuantity"), Label)
            lblDiscountType = CType(e.Item.FindControl("lblDiscountType"), Label)
            lblApplicationNo = CType(e.Item.FindControl("lblApplicationNo"), Label)
            lblDetailDiscount = CType(e.Item.FindControl("lblDetailDiscount"), Label)
            lblPriceReff = CType(e.Item.FindControl("lblPriceReff"), Label)
            lblTOP = CType(e.Item.FindControl("lblTOP"), Label)
            lblInterest = CType(e.Item.FindControl("lblInterest"), Label)
            lblDeliveryTime = CType(e.Item.FindControl("lblDeliveryTime"), Label)
            hdnLabelTotal = CType(e.Item.FindControl("hdnLabelTotal"), HiddenField)

            lbtnEdit = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            lbtnDelete = CType(e.Item.FindControl("lbtnDelete"), LinkButton)

            hdnLabelTotal.Value = oDPAToSPL.LabelTotal

            Dim objSubCategoryVehicleToModel As New SubCategoryVehicleToModel
            If hdnLabelTotal.Value.Trim.ToLower <> "total discount :" Then
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicleToModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SubCategoryVehicleToModel), "VechileModel.ID", MatchType.Exact, oDPA.VechileType.VechileModel.ID))
                Dim arrSCVToModel As ArrayList = New SubCategoryVehicleToModelFacade(User).Retrieve(criterias)
                If Not IsNothing(arrSCVToModel) AndAlso arrSCVToModel.Count > 0 Then
                    objSubCategoryVehicleToModel = CType(arrSCVToModel(0), SubCategoryVehicleToModel)
                End If
                lblModelKendaraan.Text = objSubCategoryVehicleToModel.SubCategoryVehicle.Name
                lblTipeKendaraan.Text = oDPA.VechileType.VechileTypeCode & " (" & oDPA.VechileType.Description & ")"
                lblQuantity.Text = oDPA.ResponseQty
                lblDiscountType.Text = oDPAToSPL.DiscountMaster.Category
                lblApplicationNo.Text = If(Not IsNothing(oDPAToSPL.SPLDetail), oDPAToSPL.SPLDetail.SPL.SPLNumber, "")
                lblDetailDiscount.Text = Format(oDPAToSPL.DiscountProposed, "#,##0")
                lblPriceReff.Text = If(oDPA.PriceReff = System.Data.SqlTypes.SqlDateTime.MinValue.Value, "Update Price", oDPA.PriceReff.ToString("MMyyyy"))
                lblTOP.Text = oDPA.MaxTOPDay
                lblInterest.Text = If(CommonFunction.GetEnumDescription(oDPA.FreeIntIndicator, "EnumDiscountProposal.InterestIndicator").Trim <> "", CommonFunction.GetEnumDescription(oDPA.FreeIntIndicator, "EnumDiscountProposal.InterestIndicator"), "")
                lblDeliveryTime.Text = enumMonthGet.GetName(oDPA.DeliveryDate.Month) & " " & oDPA.DeliveryDate.Year.ToString("d4")

                lbtnEdit.Attributes("style") = "display:table-row"
                lbtnDelete.Attributes("style") = "display:table-row"
            Else
                lblTipeKendaraan.Text = hdnLabelTotal.Value
                e.Item.Cells(2).BackColor = Color.SkyBlue
                e.Item.Cells(3).BackColor = Color.SkyBlue
                e.Item.Cells(4).BackColor = Color.SkyBlue
                e.Item.Cells(5).BackColor = Color.SkyBlue
                e.Item.Cells(6).BackColor = Color.SkyBlue
                lblTipeKendaraan.Font.Bold = True
                lblDetailDiscount.Font.Bold = True
                lblModelKendaraan.Text = ""
                lblQuantity.Text = ""
                lblDiscountType.Text = ""
                lblApplicationNo.Text = ""
                lblDetailDiscount.Text = Format(oDPAToSPL.TotalDiscount, "#,##0")
                lblPriceReff.Text = ""
                lblTOP.Text = ""
                lblInterest.Text = ""
                lblDeliveryTime.Text = ""
                lbtnEdit.Attributes("style") = "display:none"
                lbtnDelete.Attributes("style") = "display:none"
                e.Item.Cells(0).Text = ""
            End If
        End If

        If e.Item.ItemType = ListItemType.Footer Then
            ddlFModelKendaraan = CType(e.Item.FindControl("ddlFModelKendaraan"), DropDownList)
            ddlFTipeKendaraan = CType(e.Item.FindControl("ddlFTipeKendaraan"), DropDownList)
            ddlFDiscountType = CType(e.Item.FindControl("ddlFDiscountType"), DropDownList)
            ddlFInterest = CType(e.Item.FindControl("ddlFInterest"), DropDownList)
            txtFDeliveryTime = CType(e.Item.FindControl("txtFDeliveryTime"), System.Web.UI.WebControls.TextBox)
            lblSearchFApplicationNo = CType(e.Item.FindControl("lblSearchFApplicationNo"), Label)
            lblSearchFPriceReff = CType(e.Item.FindControl("lblSearchFPriceReff"), Label)
            txtFQuantity = CType(e.Item.FindControl("txtFQuantity"), System.Web.UI.WebControls.TextBox)

            arlDiscountProposalDtl = CType(sessHelper.GetSession(sessDiscountProposalDtl), ArrayList)
            If IsNothing(arlDiscountProposalDtl) Then arlDiscountProposalDtl = New ArrayList()
            If arlDiscountProposalDtl.Count > 0 Then
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Status", MatchType.Exact, ""))  '-- Type still active
                criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Category.ID", MatchType.InSet, "(1,2)"))
                Dim strSubCategoryVehicleID As String = ""
                For Each obj As DiscountProposalDetail In arlDiscountProposalDtl
                    If strSubCategoryVehicleID = "" Then
                        strSubCategoryVehicleID = obj.SubCategoryVehicle.ID.ToString
                    Else
                        strSubCategoryVehicleID += "," & obj.SubCategoryVehicle.ID.ToString
                    End If
                Next
                If strSubCategoryVehicleID.Trim <> "" Then
                    criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "ID", MatchType.InSet, "(" & strSubCategoryVehicleID & ")"))
                End If
                Dim sortColl As SortCollection = New SortCollection
                sortColl.Add(New Sort(GetType(SubCategoryVehicle), "ID", Sort.SortDirection.DESC))  '-- Sort by Vechile type code
                '-- Bind ddlSubCategory dropdownlist
                Dim arrDDLModel As ArrayList = New ArrayList
                arrDDLModel = New SubCategoryVehicleFacade(User).Retrieve(criterias, sortColl)
                With ddlFModelKendaraan
                    .Items.Clear()
                    .DataSource = arrDDLModel
                    .DataTextField = "Name"
                    .DataValueField = "ID"
                    .DataBind()
                    .Items.Insert(0, New ListItem("Silahkan Pilih", ""))
                    .SelectedIndex = 0
                End With
            End If

            With ddlFTipeKendaraan
                .Items.Clear()
                .Items.Insert(0, New ListItem("Silakan Pilih ", ""))
                .SelectedIndex = 0
            End With

            lblSearchFApplicationNo.Attributes("onclick") = "showPopupSearchFApplicationNo(this,'" & txtDealerCode.Text & "');"
            lblSearchFPriceReff.Attributes("onclick") = "showPopupSearchFPriceReff(this);"

            txtFQuantity.Text = 0

            Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(DiscountMaster), "Status", MatchType.Exact, "1"))  '-- Type still active
            Dim sortColl2 As SortCollection = New SortCollection
            sortColl2.Add(New Sort(GetType(DiscountMaster), "Category", Sort.SortDirection.ASC))  '-- Sort by Vechile type code
            '-- Bind ddlFDiscountType dropdownlist
            Dim arrDDLDiscType As ArrayList = New ArrayList
            arrDDLDiscType = New DiscountMasterFacade(User).Retrieve(criterias2, sortColl2)
            With ddlFDiscountType
                .Items.Clear()
                .DataSource = arrDDLDiscType
                .DataTextField = "Category"
                .DataValueField = "ID"
                .DataBind()
                .Items.Insert(0, New ListItem("Silahkan Pilih", ""))
            End With

            'ddlFInterest
            With ddlFInterest
                .Items.Clear()
                .DataSource = New StandardCodeFacade(User).RetrieveByCategory("EnumDiscountProposal.InterestIndicator")
                .DataTextField = "ValueDesc"
                .DataValueField = "ValueId"
                .DataBind()
                .SelectedIndex = 1
            End With

            'txtFDeliveryTime
            txtFDeliveryTime.MaxLength = 6
        End If

        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            Dim oDPAtoSPL As DiscountProposalDetailApprovaltoSPL = CType(e.Item.DataItem, DiscountProposalDetailApprovaltoSPL)
            Dim strLabelTotalDisc As String = If(IsNothing(oDPAtoSPL.LabelTotal), "", oDPAtoSPL.LabelTotal)
            lblNo = CType(e.Item.FindControl("lblNo"), Label)

            If e.Item.ItemIndex = 0 Then
                ViewState("ItemIndexx") = e.Item.ItemIndex
                intItemIndexx = CType(ViewState("ItemIndexx"), Integer)
                lblNo.Text = intItemIndexx + 1 + (dgProposedDiscount.CurrentPageIndex * dgProposedDiscount.PageSize)
                ViewState("ItemIndexx") = lblNo.Text
            Else
                If strLabelTotalDisc.Trim.ToLower = "total discount :" Then
                    ViewState("ItemIndexx") = 0
                Else
                    intItemIndexx = CType(ViewState("ItemIndexx"), Integer)
                    lblNo.Text = intItemIndexx + 1 + (dgProposedDiscount.CurrentPageIndex * dgProposedDiscount.PageSize)
                    ViewState("ItemIndexx") = lblNo.Text
                End If
            End If

            Dim i% = 0
            Dim oDPA As DiscountProposalDetailApproval
            If arlDiscountProposalDtlApproval.Count > 0 Then
                For Each objDiscountProposalDtlApproval As DiscountProposalDetailApproval In arlDiscountProposalDtlApproval
                    If objDiscountProposalDtlApproval.VechileTypeID = oDPAtoSPL.VechileTypeID AndAlso objDiscountProposalDtlApproval.ModelID = oDPAtoSPL.ModelID Then
                        oDPA = objDiscountProposalDtlApproval
                        Exit For
                    End If
                    i += 1
                Next
            End If

            ddlEModelKendaraan = CType(e.Item.FindControl("ddlEModelKendaraan"), DropDownList)
            ddlETipeKendaraan = CType(e.Item.FindControl("ddlETipeKendaraan"), DropDownList)
            txtEQuantity = CType(e.Item.FindControl("txtEQuantity"), System.Web.UI.WebControls.TextBox)
            ddlEDiscountType = CType(e.Item.FindControl("ddlEDiscountType"), DropDownList)
            ddlEInterest = CType(e.Item.FindControl("ddlEInterest"), DropDownList)
            txtEDeliveryTime = CType(e.Item.FindControl("txtEDeliveryTime"), System.Web.UI.WebControls.TextBox)
            hdnESPLID = CType(e.Item.FindControl("hdnESPLID"), HiddenField)
            txtETOP = CType(e.Item.FindControl("txtETOP"), System.Web.UI.WebControls.TextBox)
            txtEPriceReff = CType(e.Item.FindControl("txtEPriceReff"), System.Web.UI.WebControls.TextBox)
            txtEDetailDiscount = CType(e.Item.FindControl("txtEDetailDiscount"), System.Web.UI.WebControls.TextBox)
            txtEApplicationNo = CType(e.Item.FindControl("txtEApplicationNo"), System.Web.UI.WebControls.TextBox)
            lblSearchEApplicationNo = CType(e.Item.FindControl("lblSearchEApplicationNo"), Label)
            lblSearchEPriceReff = CType(e.Item.FindControl("lblSearchEPriceReff"), Label)

            arlDiscountProposalDtl = CType(sessHelper.GetSession(sessDiscountProposalDtl), ArrayList)
            If IsNothing(arlDiscountProposalDtl) Then arlDiscountProposalDtl = New ArrayList()
            If arlDiscountProposalDtl.Count > 0 Then
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Status", MatchType.Exact, ""))  '-- Type still active
                criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Category.ID", MatchType.InSet, "(1,2)"))
                Dim strSubCategoryVehicleID As String = ""
                For Each obj As DiscountProposalDetail In arlDiscountProposalDtl
                    If strSubCategoryVehicleID = "" Then
                        strSubCategoryVehicleID = obj.SubCategoryVehicle.ID.ToString
                    Else
                        strSubCategoryVehicleID += "," & obj.SubCategoryVehicle.ID.ToString
                    End If
                Next
                If strSubCategoryVehicleID.Trim <> "" Then
                    criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "ID", MatchType.InSet, "(" & strSubCategoryVehicleID & ")"))
                End If
                Dim sortColl As SortCollection = New SortCollection
                sortColl.Add(New Sort(GetType(SubCategoryVehicle), "ID", Sort.SortDirection.DESC))  '-- Sort by Vechile type code
                '-- Bind ddlSubCategory dropdownlist
                Dim arrDDLModel As ArrayList = New ArrayList
                arrDDLModel = New SubCategoryVehicleFacade(User).Retrieve(criterias, sortColl)
                With ddlEModelKendaraan
                    .Items.Clear()
                    .DataSource = arrDDLModel
                    .DataTextField = "Name"
                    .DataValueField = "ID"
                    .DataBind()
                    .Items.Insert(0, New ListItem("Silahkan Pilih", ""))

                    Dim objSubCategoryVehicleToModel As New SubCategoryVehicleToModel
                    Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicleToModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias2.opAnd(New Criteria(GetType(SubCategoryVehicleToModel), "VechileModel.ID", MatchType.Exact, oDPA.VechileType.VechileModel.ID))
                    Dim arrSCVToModel As ArrayList = New SubCategoryVehicleToModelFacade(User).Retrieve(criterias2)
                    If Not IsNothing(arrSCVToModel) AndAlso arrSCVToModel.Count > 0 Then
                        objSubCategoryVehicleToModel = CType(arrSCVToModel(0), SubCategoryVehicleToModel)
                    End If
                    .SelectedValue = objSubCategoryVehicleToModel.SubCategoryVehicle.ID
                End With
                ddlFModelKendaraan_SelectedIndexChanged(ddlEModelKendaraan, Nothing)
                ddlETipeKendaraan.SelectedValue = oDPA.VechileType.ID
            End If
            txtEQuantity.Text = oDPA.ResponseQty

            lblSearchEApplicationNo.Attributes("onclick") = "showPopupSearchFApplicationNo(this,'" & txtDealerCode.Text & "');"
            lblSearchEPriceReff.Attributes("onclick") = "showPopupSearchFPriceReff(this);"

            Dim criterias3 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias3.opAnd(New Criteria(GetType(DiscountMaster), "Status", MatchType.Exact, "1"))  '-- Type still active
            Dim sortColl3 As SortCollection = New SortCollection
            sortColl3.Add(New Sort(GetType(DiscountMaster), "Category", Sort.SortDirection.ASC))  '-- Sort by Vechile type code
            '-- Bind ddlEDiscountType dropdownlist
            Dim arrDDLDiscType As ArrayList = New ArrayList
            arrDDLDiscType = New DiscountMasterFacade(User).Retrieve(criterias3, sortColl3)
            With ddlEDiscountType
                .Items.Clear()
                .DataSource = arrDDLDiscType
                .DataTextField = "Category"
                .DataValueField = "ID"
                .DataBind()
                .Items.Insert(0, New ListItem("Silahkan Pilih", ""))
                .SelectedValue = oDPAtoSPL.DiscountMaster.ID
            End With
            ddlFDiscountType_SelectedIndexChanged(ddlEDiscountType, Nothing)

            hdnESPLID.Value = If(Not IsNothing(oDPAtoSPL.SPLDetail), oDPAtoSPL.SPLDetail.SPL.ID, "")
            txtEApplicationNo.Text = If(Not IsNothing(oDPAtoSPL.SPLDetail), oDPAtoSPL.SPLDetail.SPL.SPLNumber, "")
            txtEDetailDiscount.Text = Format(oDPAtoSPL.DiscountProposed, "#,##0")
            txtEPriceReff.Text = If(oDPA.PriceReff = System.Data.SqlTypes.SqlDateTime.MinValue.Value, "Update Price", oDPA.PriceReff.ToString("MMyyyy"))
            txtETOP.Text = oDPA.MaxTOPDay
            txtEDeliveryTime.Text = oDPA.DeliveryDate.Month.ToString("d2") & oDPA.DeliveryDate.Year.ToString("d4")

            'ddlEInterest
            With ddlEInterest
                .Items.Clear()
                .DataSource = New StandardCodeFacade(User).RetrieveByCategory("EnumDiscountProposal.InterestIndicator")
                .DataTextField = "ValueDesc"
                .DataValueField = "ValueId"
                .DataBind()
                .Items.Insert(0, New ListItem("Silakan Pilih ", ""))
                .SelectedValue = oDPA.FreeIntIndicator
            End With

            ddlEModelKendaraan.Enabled = False
            ddlETipeKendaraan.Enabled = False
        End If

    End Sub

    Public Sub ddlFDiscountType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ddlFDiscountType As DropDownList = sender
        Dim gridItem As DataGridItem = ddlFDiscountType.Parent.Parent
        Dim txtFDetailDiscount As System.Web.UI.WebControls.TextBox
        Dim txtFApplicationNo As System.Web.UI.WebControls.TextBox
        Dim lblSearchFApplicationNo As System.Web.UI.WebControls.Label
        Dim hdnFSPLID As HiddenField
        Dim ddlFTipeKendaraan As DropDownList
        If gridItem.DataSetIndex > -1 Then
            txtFDetailDiscount = gridItem.FindControl("txtEDetailDiscount")
            hdnFSPLID = gridItem.FindControl("hdnESPLID")
            txtFApplicationNo = gridItem.FindControl("txtEApplicationNo")
            lblSearchFApplicationNo = gridItem.FindControl("lblSearchEApplicationNo")
            ddlFTipeKendaraan = gridItem.FindControl("ddlETipeKendaraan")
        Else
            txtFDetailDiscount = gridItem.FindControl("txtFDetailDiscount")
            hdnFSPLID = gridItem.FindControl("hdnFSPLID")
            txtFApplicationNo = gridItem.FindControl("txtFApplicationNo")
            lblSearchFApplicationNo = gridItem.FindControl("lblSearchFApplicationNo")
            ddlFTipeKendaraan = gridItem.FindControl("ddlFTipeKendaraan")
        End If
        Dim strDiscountTypeCode As String = String.Empty
        Dim objDiscountMaster As DiscountMaster = New DiscountMasterFacade(User).Retrieve(CInt(ddlFDiscountType.SelectedValue))
        If Not IsNothing(objDiscountMaster) Then strDiscountTypeCode = objDiscountMaster.Code
        If strDiscountTypeCode = "D01" Then   '--> Fleet/Government Discount
            txtFApplicationNo.Text = ""
            hdnFSPLID.Value = ""
            txtFApplicationNo.Enabled = False
            lblSearchFApplicationNo.Visible = False
            txtFDetailDiscount.Enabled = True
            txtFDetailDiscount.Focus()
        Else
            txtFApplicationNo.Enabled = True
            lblSearchFApplicationNo.Visible = True
            txtFDetailDiscount.Enabled = True
            If txtFDetailDiscount.Text.Trim = "" Then txtFDetailDiscount.Text = "0"
            If hdnFSPLID.Value.Trim <> "" AndAlso ddlFTipeKendaraan.SelectedIndex <> 0 Then
                Dim objSPL As SPL = New SPLFacade(User).Retrieve(CInt(hdnFSPLID.Value))
                If Not IsNothing(objSPL) Then
                    For Each objSPLDtl As SPLDetail In objSPL.SPLDetails
                        If objSPLDtl.VechileType.ID = ddlFTipeKendaraan.SelectedValue Then
                            txtFDetailDiscount.Text = Format(objSPLDtl.Discount, "#,##0")

                            Dim obSPLD As New SPLDetailtoSPL
                            Dim crt2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SPLDetailtoSPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            crt2.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPLDetailtoSPL), "SPLDetail.ID", MatchType.Exact, objSPLDtl.ID))
                            crt2.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPLDetailtoSPL), "DiscountMaster.ID", MatchType.Exact, objDiscountMaster.ID))
                            Dim arRes As New ArrayList
                            arRes = New SPLDetailtoSPLFacade(User).Retrieve(crt2)
                            If arRes.Count > 0 Then
                                obSPLD = CType(arRes(0), SPLDetailtoSPL)
                            End If
                            'txtFDetailDiscount.Text = Format(objSPLDtl.Discount, "#,##0")
                            txtFDetailDiscount.Text = Format(obSPLD.Discount, "#,##0")
                            Exit For

                            'Exit For
                        End If
                    Next
                End If
            End If
        End If
    End Sub

    Protected Sub hdnFSPLID_ValueChanged(sender As Object, e As EventArgs)
        Dim hdnFSPLID As HiddenField = sender
        Dim gridItem As DataGridItem = hdnFSPLID.Parent.Parent
        Dim txtFDetailDiscount As System.Web.UI.WebControls.TextBox
        Dim ddlFTipeKendaraan As DropDownList
        Dim ddlFDiscountType As DropDownList
        If gridItem.DataSetIndex > -1 Then
            txtFDetailDiscount = gridItem.FindControl("txtEDetailDiscount")
            ddlFTipeKendaraan = gridItem.FindControl("ddlETipeKendaraan")
            ddlFDiscountType = gridItem.FindControl("ddlEDiscountType")
        Else
            txtFDetailDiscount = gridItem.FindControl("txtFDetailDiscount")
            ddlFTipeKendaraan = gridItem.FindControl("ddlFTipeKendaraan")
            ddlFDiscountType = gridItem.FindControl("ddlFDiscountType")
        End If
        Dim strDiscountTypeCode As String = String.Empty
        Dim objDiscountMaster As DiscountMaster = New DiscountMasterFacade(User).Retrieve(CInt(ddlFDiscountType.SelectedValue))
        If Not IsNothing(objDiscountMaster) Then strDiscountTypeCode = objDiscountMaster.Code
        If strDiscountTypeCode = "D01" Then   '--> Fleet/Government Discount
            txtFDetailDiscount.Enabled = True
            txtFDetailDiscount.Focus()
        Else
            txtFDetailDiscount.Enabled = True
            If txtFDetailDiscount.Text.Trim = "" Then txtFDetailDiscount.Text = "0"
            If hdnFSPLID.Value.Trim <> "" AndAlso ddlFTipeKendaraan.SelectedIndex <> 0 Then
                Dim objSPL As SPL = New SPLFacade(User).Retrieve(CInt(hdnFSPLID.Value))
                If Not IsNothing(objSPL) Then
                    For Each objSPLDtl As SPLDetail In objSPL.SPLDetails
                        If objSPLDtl.VechileType.ID = ddlFTipeKendaraan.SelectedValue Then
                            txtFDetailDiscount.Text = Format(objSPLDtl.Discount, "#,##0")
                            'Exit For

                            Dim obSPLD As New SPLDetailtoSPL
                            Dim crt2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SPLDetailtoSPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            crt2.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPLDetailtoSPL), "SPLDetail.ID", MatchType.Exact, objSPLDtl.ID))
                            crt2.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPLDetailtoSPL), "DiscountMaster.ID", MatchType.Exact, objDiscountMaster.ID))
                            Dim arRes As New ArrayList
                            arRes = New SPLDetailtoSPLFacade(User).Retrieve(crt2)
                            If arRes.Count > 0 Then
                                obSPLD = CType(arRes(0), SPLDetailtoSPL)
                            End If
                            'txtFDetailDiscount.Text = Format(objSPLDtl.Discount, "#,##0")
                            txtFDetailDiscount.Text = Format(obSPLD.Discount, "#,##0")
                            Exit For

                        End If
                    Next
                End If
            End If
        End If
    End Sub

    Public Sub txtFApplicationNo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim txtFApplicationNo As System.Web.UI.WebControls.TextBox = sender
        Dim gridItem As DataGridItem = txtFApplicationNo.Parent.Parent
        Dim txtFDetailDiscount As System.Web.UI.WebControls.TextBox
        Dim hdnFSPLID As HiddenField
        Dim ddlFTipeKendaraan As DropDownList
        Dim ddlFDiscountType As DropDownList
        If gridItem.DataSetIndex > -1 Then
            txtFDetailDiscount = gridItem.FindControl("txtEDetailDiscount")
            hdnFSPLID = gridItem.FindControl("hdnESPLID")
            ddlFTipeKendaraan = gridItem.FindControl("ddlETipeKendaraan")
            ddlFDiscountType = gridItem.FindControl("ddlEDiscountType")
        Else
            txtFDetailDiscount = gridItem.FindControl("txtFDetailDiscount")
            hdnFSPLID = gridItem.FindControl("hdnFSPLID")
            ddlFTipeKendaraan = gridItem.FindControl("ddlFTipeKendaraan")
            ddlFDiscountType = gridItem.FindControl("ddlFDiscountType")
        End If
        Dim strDiscountTypeCode As String = String.Empty
        Dim objDiscountMaster As DiscountMaster = New DiscountMasterFacade(User).Retrieve(CInt(ddlFDiscountType.SelectedValue))
        If Not IsNothing(objDiscountMaster) Then strDiscountTypeCode = objDiscountMaster.Code
        If strDiscountTypeCode = "D01" Then   '--> Fleet/Government Discount
            txtFDetailDiscount.Enabled = True
            txtFDetailDiscount.Focus()
        Else
            txtFDetailDiscount.Enabled = True
            If txtFDetailDiscount.Text.Trim = "" Then txtFDetailDiscount.Text = "0"

            If hdnFSPLID.Value.Trim <> "" AndAlso ddlFTipeKendaraan.SelectedIndex <> 0 Then
                Dim objSPL As SPL = New SPLFacade(User).Retrieve(CInt(hdnFSPLID.Value))
                If Not IsNothing(objSPL) Then
                    For Each objSPLDtl As SPLDetail In objSPL.SPLDetails
                        If objSPLDtl.VechileType.ID = ddlFTipeKendaraan.SelectedValue Then
                            Dim obSPLD As New SPLDetailtoSPL
                            Dim crt2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SPLDetailtoSPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            crt2.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPLDetailtoSPL), "SPLDetail.ID", MatchType.Exact, objSPLDtl.ID))
                            crt2.opAnd(New Criteria(GetType(KTB.DNet.Domain.SPLDetailtoSPL), "DiscountMaster.ID", MatchType.Exact, objDiscountMaster.ID))
                            Dim arRes As New ArrayList
                            arRes = New SPLDetailtoSPLFacade(User).Retrieve(crt2)
                            If arRes.Count > 0 Then
                                obSPLD = CType(arRes(0), SPLDetailtoSPL)
                            End If
                            'txtFDetailDiscount.Text = Format(objSPLDtl.Discount, "#,##0")
                            txtFDetailDiscount.Text = Format(obSPLD.Discount, "#,##0")
                            Exit For
                            'Exit For
                        End If
                    Next
                End If
            End If
        End If
    End Sub

    Public Sub ddlFTipeKendaraan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim arrDDL As ArrayList = New ArrayList
        Dim ddlFTipeKendaraan As DropDownList = sender
        Dim gridItem As DataGridItem = ddlFTipeKendaraan.Parent.Parent
        Dim ddlFModelKendaraan As DropDownList
        Dim txtFQuantity As System.Web.UI.WebControls.TextBox
        Dim txtFPriceReff As System.Web.UI.WebControls.TextBox
        Dim txtFTOP As System.Web.UI.WebControls.TextBox
        Dim ddlFInterest As DropDownList
        Dim txtFDeliveryTime As System.Web.UI.WebControls.TextBox
        Dim lblSearchFPriceReff As Label
        If gridItem.DataSetIndex > -1 Then
            ddlFModelKendaraan = gridItem.FindControl("ddlEModelKendaraan")
            txtFQuantity = gridItem.FindControl("txtEQuantity")
            txtFPriceReff = gridItem.FindControl("txtEPriceReff")
            txtFTOP = gridItem.FindControl("txtETOP")
            ddlFInterest = gridItem.FindControl("ddlEInterest")
            txtFDeliveryTime = gridItem.FindControl("txtEDeliveryTime")
            lblSearchFPriceReff = gridItem.FindControl("lblSearchEPriceReff")
        Else
            ddlFModelKendaraan = gridItem.FindControl("ddlFModelKendaraan")
            txtFQuantity = gridItem.FindControl("txtFQuantity")
            txtFPriceReff = gridItem.FindControl("txtFPriceReff")
            txtFTOP = gridItem.FindControl("txtFTOP")
            ddlFInterest = gridItem.FindControl("ddlFInterest")
            txtFDeliveryTime = gridItem.FindControl("txtFDeliveryTime")
            lblSearchFPriceReff = gridItem.FindControl("lblSearchFPriceReff")
        End If

        lblSearchFPriceReff.Visible = True
        txtFQuantity.Enabled = True
        txtFPriceReff.Enabled = True
        txtFTOP.Enabled = True
        ddlFInterest.Enabled = True
        txtFDeliveryTime.Enabled = True

        arlDiscountProposalDtlApproval = CType(sessHelper.GetSession(sessDiscountProposalDtlApproval), ArrayList)
        If IsNothing(arlDiscountProposalDtlApproval) Then arlDiscountProposalDtlApproval = New ArrayList
        If Not IsNothing(ddlFModelKendaraan) AndAlso Not IsNothing(ddlFTipeKendaraan) Then
            For Each oDPA As DiscountProposalDetailApproval In arlDiscountProposalDtlApproval
                If oDPA.ModelID = ddlFModelKendaraan.SelectedValue AndAlso oDPA.VechileTypeID = ddlFTipeKendaraan.SelectedValue Then
                    txtFQuantity.Text = oDPA.ResponseQty
                    txtFPriceReff.Text = If(oDPA.PriceReff = System.Data.SqlTypes.SqlDateTime.MinValue.Value, "Update Price", oDPA.PriceReff.ToString("MMyyyy"))
                    txtFTOP.Text = oDPA.MaxTOPDay
                    ddlFInterest.SelectedValue = oDPA.FreeIntIndicator
                    txtFDeliveryTime.Text = oDPA.DeliveryDate.Month.ToString("d2") & oDPA.DeliveryDate.Year.ToString("d4")
                    lblSearchFPriceReff.Visible = False
                    txtFQuantity.Enabled = False
                    txtFPriceReff.Enabled = False
                    txtFTOP.Enabled = False
                    ddlFInterest.Enabled = False
                    txtFDeliveryTime.Enabled = False
                    Exit For
                End If
            Next
        End If
    End Sub

    Public Sub ddlFModelKendaraan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim arrDDL As ArrayList = New ArrayList
        Dim ddlFModelKendaraan As DropDownList = sender
        Dim gridItem As DataGridItem = ddlFModelKendaraan.Parent.Parent
        Dim ddlFTipeKendaraan As DropDownList
        Dim txtFQuantity As System.Web.UI.WebControls.TextBox
        Dim txtFPriceReff As System.Web.UI.WebControls.TextBox
        Dim txtFTOP As System.Web.UI.WebControls.TextBox
        Dim ddlFInterest As DropDownList
        Dim txtFDeliveryTime As System.Web.UI.WebControls.TextBox
        Dim lblSearchFPriceReff As Label
        If gridItem.DataSetIndex > -1 Then
            ddlFTipeKendaraan = gridItem.FindControl("ddlETipeKendaraan")
            txtFQuantity = gridItem.FindControl("txtEQuantity")
            txtFPriceReff = gridItem.FindControl("txtEPriceReff")
            txtFTOP = gridItem.FindControl("txtETOP")
            ddlFInterest = gridItem.FindControl("ddlEInterest")
            txtFDeliveryTime = gridItem.FindControl("txtEDeliveryTime")
            lblSearchFPriceReff = gridItem.FindControl("lblSearchEPriceReff")
        Else
            ddlFTipeKendaraan = gridItem.FindControl("ddlFTipeKendaraan")
            txtFQuantity = gridItem.FindControl("txtFQuantity")
            txtFPriceReff = gridItem.FindControl("txtFPriceReff")
            txtFTOP = gridItem.FindControl("txtFTOP")
            ddlFInterest = gridItem.FindControl("ddlFInterest")
            txtFDeliveryTime = gridItem.FindControl("txtFDeliveryTime")
            lblSearchFPriceReff = gridItem.FindControl("lblSearchFPriceReff")
        End If

        txtFQuantity.Text = 0
        txtFPriceReff.Text = ""
        txtFTOP.Text = 0
        ddlFInterest.SelectedIndex = 0
        txtFDeliveryTime.Text = ""
        lblSearchFPriceReff.Visible = True
        txtFQuantity.Enabled = True
        txtFPriceReff.Enabled = True
        txtFTOP.Enabled = True
        ddlFInterest.Enabled = True
        txtFDeliveryTime.Enabled = True
        If Not IsNothing(ddlFTipeKendaraan) Then
            BindVehicleTypeProposedDiscount(ddlFModelKendaraan, ddlFTipeKendaraan)
        End If
    End Sub

    Private Sub BindVehicleTypeProposedDiscount(ByVal ddlModel As DropDownList, ByRef ddlTipe As DropDownList)
        ddlTipe.Items.Clear()
        ddlTipe.Items.Add(New ListItem("Silahkan Pilih", ""))
        If ddlModel.SelectedIndex > 0 Then
            '-- Vehicle criteria & sort
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VechileType), "Category.ID", MatchType.InSet, "(1,2)"))
            criterias.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.No, "X"))  '-- Type still active
            Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where SubCategoryVehicleID = " & ddlModel.SelectedValue
            criterias.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))
            strSql = "select distinct d.VechileTypeID "
            strSql += "from DiscountProposalHeader a "
            strSql += "join DiscountProposalDetail b on b.DiscountProposalHeaderID = a.ID and b.RowStatus = 0 "
            strSql += "join VechileColorIsActiveOnPK c on c.ID = b.VechileColorIsActiveOnPKID and c.RowStatus = 0 "
            strSql += "join VechileColor d on d.ID = c.VehicleColorID and d.RowStatus = 0 "
            strSql += "join VechileType e on e.ID = d.VechileTypeID and e.RowStatus = 0 "
            strSql += "where a.RowStatus = 0 "
            strSql += "and a.ProposalRegno = '" & lblProposalRegNo.Text.Trim & "' " 'sesuai dengan discount proposal yang sedang diedit
            criterias.opAnd(New Criteria(GetType(VechileType), "ID", MatchType.InSet, "(" & strSql & ")"))

            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(VechileType), "VechileTypeCode", Sort.SortDirection.ASC))  '-- Sort by Vechile type code
            '-- Bind Vehicle type dropdownlist
            Dim arlVT As ArrayList = New VechileTypeFacade(User).RetrieveByCriteria(criterias, sortColl)
            For Each oVT As VechileType In arlVT
                ddlTipe.Items.Add(New ListItem(oVT.VechileTypeCode & " (" & oVT.Description & ")", oVT.ID))
            Next
        End If
    End Sub

    Protected Sub btnValidasi_Click(sender As Object, e As EventArgs) Handles btnValidasi.Click
        If IsLoginAsDealer() Then
            If hdnDiscountProposalHeaderID.Value <> "" Then
                Dim arrDPOldStatus As New ArrayList
                Dim criterias As New CriteriaComposite(New Criteria(GetType(DiscountProposalHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(DiscountProposalHeader), "ID", MatchType.Exact, hdnDiscountProposalHeaderID.Value))
                Dim arrDPToProcess As ArrayList = New DiscountProposalHeaderFacade(User).Retrieve(criterias)
                If Not IsNothing(arrDPToProcess) AndAlso arrDPToProcess.Count > 0 Then
                    Dim objDPNew As DiscountProposalHeader = CType(arrDPToProcess(0), DiscountProposalHeader)
                    If Not IsNothing(objDPNew) AndAlso objDPNew.ID > 0 Then
                        If objDPNew.Status = 0 Then     '--- Status Baru
                            Dim objDPOld As DiscountProposalHeader = New DiscountProposalHeader
                            objDPOld.ID = objDPNew.ID
                            objDPOld.Status = objDPNew.Status
                            arrDPOldStatus.Add(objDPOld)

                            objDPNew.Status = 1     '--- Status Validasi
                            Dim _result As Integer = New DiscountProposalHeaderFacade(User).UpdateStatus(arrDPToProcess, arrDPOldStatus)
                            If _result < 0 Then
                                MessageBox.Show("Gagal ubah status ke Validasi")
                            Else
                                Dim strJs As String = ""
                                strJs = "alert('Sukses Ubah status ke Validasi');"
                                strJs += "window.location = '../DiscountProposal/FrmDaftarDiscountProposal.aspx'"
                                System.Web.UI.ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Script", strJs, True)
                            End If
                        Else
                            MessageBox.Show("Update status ke Validasi harus dari status Baru")
                        End If
                    Else
                        MessageBox.Show("Data Discount Proposal belum ada")
                    End If
                End If
            Else
                MessageBox.Show("Data Discount Proposal belum ada")
            End If
        Else
            MessageBox.Show("Login MKS tidak boleh mengubah status ke Validasi")
        End If
    End Sub

    Private Sub lnkAppointmentLetter_Click(sender As Object, e As EventArgs) Handles lnkAppointmentLetter.Click
        Dim strFileName As String, strDestFile As String
        If hdnDiscountProposalHeaderID.Value.Trim <> "" Then
            If GeneratePDFtoGroupware(hdnDiscountProposalHeaderID.Value, strFileName, strDestFile) Then
                MessageBox.Show("Sukses Generate File PDF")
            Else
                MessageBox.Show("Gagal Generate File PDF")
            End If
        End If
    End Sub

    Private Function GeneratePdfPages(ByVal mainFilePath As String) As PdfPages
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        'Dim filePath As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") + mainFilePath
        Dim filePath As String = mainFilePath

        Dim fileInfo As New FileInfo(filePath)
        Dim destFilePath As String = fileInfo.FullName
        Dim success As Boolean = False

        Try
            success = imp.Start()
            If success Then
                If (fileInfo.Exists) Then
                    Dim pdfDoc As PdfDocument = PdfSharp.Pdf.IO.PdfReader.Open(filePath, PdfDocumentOpenMode.Import)
                    Return pdfDoc.Pages
                Else
                    MessageBox.Show(SR.FileNotFound(fileInfo.Name))
                End If
                imp.StopImpersonate()
            End If

        Catch ex As Exception
            MessageBox.Show(SR.DownloadFail(fileInfo.Name))

        End Try
    End Function

    Private Function GenerateExcelFileRincianKendaraan(ByVal id As Integer, ByRef strFileName As String, ByRef strDestFile As String) As Boolean
        Dim result As Boolean = False

        Dim DataRincianHargaKendaraan As DataTable
        DataRincianHargaKendaraan = sessHelper.GetSession(sessDiscountProposalDtlPricePivotGrid)
        If IsNothing(DataRincianHargaKendaraan) Then DataRincianHargaKendaraan = New DataTable

        Dim dataRincianHargaKendaraanCopy As DataTable = New DataTable()
        dataRincianHargaKendaraanCopy = DataRincianHargaKendaraan.Clone()
        dataRincianHargaKendaraanCopy = DataRincianHargaKendaraan.Copy()

        If dataRincianHargaKendaraanCopy.Rows.Count > 0 Then
            If CreateExcelRincianKendaraan(dataRincianHargaKendaraanCopy, strFileName, strDestFile) = "" Then
                result = True
            End If
        End If

        Return result
    End Function

    Private Function GeneratePDFtoGroupware(ByVal intID As Integer, ByRef strFileName As String, ByRef strDestFile As String) As String
        Dim result As String = String.Empty
        Try
            Dim data As DiscountProposalHeader = New DiscountProposalHeaderFacade(User).Retrieve(intID)
            Dim newLine As String = Environment.NewLine

            Dim filePath As String = Server.MapPath("~\DataFile\Template\DP\Fleet_Discount_Template.docx")
            Dim directoryTemp As String = Server.MapPath("~\DataFile\Template\DP\")
            Dim directoryInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(directoryTemp)
            If Not directoryInfo.Exists Then
                directoryInfo.Create()
            End If
            Dim finfo As FileInfo = New FileInfo(filePath)
            If Not finfo.Exists Then
                result = "File template diskon fleet tidak ditemukan"
                Return result
            End If

            Dim filebytes As Byte() = File.ReadAllBytes(filePath)

            Using Stream As MemoryStream = New MemoryStream()
                Stream.Write(filebytes, 0, filebytes.Length)
                Using doc As WordprocessingDocument = WordprocessingDocument.Open(Stream, True)
                    Dim body As DocumentFormat.OpenXml.Wordprocessing.Body = doc.MainDocumentPart.Document.Body
                    Dim tables As List(Of DocumentFormat.OpenXml.Wordprocessing.Table) = body.Elements(Of DocumentFormat.OpenXml.Wordprocessing.Table)().ToList()
                    For Each table As DocumentFormat.OpenXml.Wordprocessing.Table In tables
                        Dim rows As List(Of DocumentFormat.OpenXml.Wordprocessing.TableRow) = table.Elements(Of DocumentFormat.OpenXml.Wordprocessing.TableRow)().ToList()
                        For Each row As DocumentFormat.OpenXml.Wordprocessing.TableRow In rows
                            For Each cell As DocumentFormat.OpenXml.Wordprocessing.TableCell In row.Descendants(Of DocumentFormat.OpenXml.Wordprocessing.TableCell)().Where(Function(x) x.InnerText.Contains("_"))
                                Dim texts As List(Of DocumentFormat.OpenXml.Wordprocessing.Text) = cell.SelectMany(Function(p) p.Elements(Of DocumentFormat.OpenXml.Wordprocessing.Run)()).SelectMany(Function(r) r.Elements(Of DocumentFormat.OpenXml.Wordprocessing.Text)()).ToList()
                                Dim word As DocumentFormat.OpenXml.Wordprocessing.Text = New DocumentFormat.OpenXml.Wordprocessing.Text
                                Dim wordGabungan As String = ""
                                Dim i% = 0
                                For Each word2 As DocumentFormat.OpenXml.Wordprocessing.Text In texts
                                    wordGabungan += word2.InnerText
                                    If texts.Count > 1 Then
                                        If i = 0 Then
                                            word2.Text = ""
                                        End If
                                    End If
                                    word = word2
                                    i += 1
                                Next
                                word.Text = wordGabungan
                                If word.Text.Contains("_") Then
                                    Select Case word.Text.ToLower
                                        Case "dealer_code"
                                            word.Text = data.Dealer.DealerCode
                                        Case "dealer_name"
                                            word.Text = data.Dealer.DealerName
                                        Case "city_name"
                                            word.Text = data.Dealer.City.CityName
                                        Case "submit_date"
                                            word.Text = data.SubmitDate.ToString("dd MMMM yyyy")
                                        Case "app_regno"
                                            word.Text = data.ProposalRegNo
                                        Case "app_dealerregno"
                                            word.Text = data.DealerProposalNo
                                        Case "fleet_name"
                                            If Not IsNothing(data.FleetCustomerDetail) Then
                                                If Not IsNothing(data.FleetCustomerDetail.FleetCustomerHeader) Then
                                                    word.Text = data.FleetCustomerDetail.FleetCustomerHeader.FleetCustomerName
                                                End If
                                            End If
                                        Case "fleet_address"
                                            If Not IsNothing(data.FleetCustomerDetail) Then
                                                word.Text = data.FleetCustomerDetail.Address
                                            End If
                                        Case "fleet_identity"
                                            If Not IsNothing(data.FleetCustomerDetail) Then
                                                word.Text = data.FleetCustomerDetail.IdentityNumber
                                            End If
                                        Case "invoice_name"
                                            Dim sb As StringBuilder = New StringBuilder
                                            If Not IsNothing(data.DiscountProposalDetailCustomers) Then
                                                For Each objDPDCustomer As DiscountProposalDetailCustomer In data.DiscountProposalDetailCustomers
                                                    If sb.ToString().Trim = "" Then
                                                        sb.Append(objDPDCustomer.Name)
                                                    Else
                                                        sb.Append(", " & objDPDCustomer.Name)
                                                    End If
                                                Next
                                                If (sb.ToString().Length > 0) Then
                                                    word.Text = sb.ToString()
                                                Else
                                                    word.Text = ""
                                                End If
                                            End If
                                        Case "bbn_area"
                                            If Not IsNothing(data.BBNAreaProvince) Then
                                                word.Text = data.BBNAreaProvince.ProvinceName
                                            End If
                                        Case "customer_type"
                                            word.Text = CommonFunction.GetEnumDescription(data.CustomerType, "EnumDiscountProposal.CustomerType")
                                        Case "fleet_category"
                                            word.Text = CommonFunction.GetEnumDescription(data.FleetCategory, "EnumDiscountProposal.FleetCategory")
                                        Case "business_core"
                                            word.Text = New VWI_BusinessSectorFacade(User).Retrieve(data.BusinessSectorDetailID).BusinessName
                                        Case "project_name"
                                            word.Text = data.ProjectName
                                        Case "project_method"
                                            Dim strProjectMethod As String = ""
                                            If data.ProjectKindMethodOther.Trim <> "" AndAlso data.ProjectKindMethod = 2 Then  'Method Lainnya
                                                strProjectMethod = CommonFunction.GetEnumDescription(data.ProjectKindMethod, "EnumDiscountProposal.ProjectKindMethod") & " / " & data.ProjectKindMethodOther
                                            Else
                                                strProjectMethod = CommonFunction.GetEnumDescription(data.ProjectKindMethod, "EnumDiscountProposal.ProjectKindMethod")
                                            End If
                                            word.Text = strProjectMethod
                                        Case "directsales_indicator"
                                            word.Text = CommonFunction.GetEnumDescription(data.IsDealerDirectSales, "EnumDiscountProposal.DealerDirectSales")
                                        Case "concractor_name"
                                            word.Text = data.ContractorName

                                        Case "antibribery_clause"
                                            Dim arL As ArrayList = New ArrayList
                                            Dim criterias As New CriteriaComposite(New Criteria(GetType(DiscountProposalDetailDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                            criterias.opAnd(New Criteria(GetType(DiscountProposalDetailDocument), "DiscountProposalHeader.ID", MatchType.Exact, data.ID))
                                            criterias.opAnd(New Criteria(GetType(DiscountProposalDetailDocument), "FileType", MatchType.Exact, 0))
                                            arL = New DiscountProposalDetailDocumentFacade(User).Retrieve(criterias)
                                            If Not IsNothing(arL) AndAlso arL.Count > 0 Then
                                                word.Text = "Ada"
                                            Else
                                                word.Text = "Tidak Ada"
                                            End If

                                        Case "statement_letter"
                                            Dim arL As ArrayList = New ArrayList
                                            Dim criterias As New CriteriaComposite(New Criteria(GetType(DiscountProposalDetailDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                            criterias.opAnd(New Criteria(GetType(DiscountProposalDetailDocument), "DiscountProposalHeader.ID", MatchType.Exact, data.ID))
                                            criterias.opAnd(New Criteria(GetType(DiscountProposalDetailDocument), "FileType", MatchType.Exact, 1))
                                            arL = New DiscountProposalDetailDocumentFacade(User).Retrieve(criterias)
                                            If Not IsNothing(arL) AndAlso arL.Count > 0 Then
                                                word.Text = "Ada"
                                            Else
                                                word.Text = "Tidak Ada"
                                            End If

                                        Case "purchase_purpose"
                                            word.Text = CommonFunction.GetEnumDescription(data.PurchaseKind, "EnumDiscountProposal.PurchaseKind")
                                        Case "delivery_time"
                                            word.Text = data.DeliveryPlanDate.ToString("MMMM yyyy")
                                        Case "delivery_area"
                                            Dim arL As ArrayList = New ArrayList
                                            Dim criterias As New CriteriaComposite(New Criteria(GetType(V_LogisticPriceAndNationality), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                            criterias.opAnd(New Criteria(GetType(V_LogisticPriceAndNationality), "RegionCode", MatchType.Exact, data.DeliveryRegionCode))
                                            arL = New V_LogisticPriceAndNationalityFacade(User).Retrieve(criterias)
                                            If Not IsNothing(arL) AndAlso arL.Count > 0 Then
                                                word.Text = CType(arL(0), V_LogisticPriceAndNationality).RegionDescription
                                            End If
                                        Case "purchase_method"
                                            Dim strPurchaseMethod As String = ""
                                            If data.PurchaseMethod = 1 Then  'Leasing
                                                If Not IsNothing(data.LeasingCompany) Then
                                                    strPurchaseMethod = CommonFunction.GetEnumDescription(data.PurchaseMethod, "EnumDiscountProposal.PurchaseMethod") & _
                                                                        " / " & data.LeasingCompany.LeasingName & _
                                                                        " / " & CommonFunction.GetEnumDescription(data.IsAPMSubsidy, "EnumDiscountProposal.APMSubsidy")
                                                Else
                                                    strPurchaseMethod = CommonFunction.GetEnumDescription(data.PurchaseMethod, "EnumDiscountProposal.PurchaseMethod")
                                                End If
                                            Else
                                                strPurchaseMethod = CommonFunction.GetEnumDescription(data.PurchaseMethod, "EnumDiscountProposal.PurchaseMethod")
                                            End If
                                            word.Text = strPurchaseMethod

                                        Case "payment_method"
                                            word.Text = CommonFunction.GetEnumDescription(data.PaymentMethod, "EnumDiscountProposal.PaymentMethod")
                                        Case "distributor_consideration"
                                            'Ditulis di bawah dari data table
                                        Case "distributor_comment"
                                            word.Text = data.MMKSINotes
                                    End Select
                                End If
                            Next
                        Next
                    Next
                End Using
                Dim bytes As Byte() = Stream.ToArray()

                Dim tempPath As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") + "DataTemp\DiscountProposal\" + Guid.NewGuid().ToString().Substring(0, 5) + ".docx"
                UploadDocXFile(bytes, tempPath)
                Dim tempPath2 As String = ""
                CreateTableinWord(tempPath, tempPath2)
                DownloadPDFFile(tempPath2, strFileName, strDestFile)
            End Using

            result = String.Empty

        Catch ex As Exception
            result = ex.Message
        End Try
        Return result
    End Function

    Private Sub DownloadPDFFile(ByVal tempPath As String, ByRef strFileName As String, ByRef strDestFile As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim success As Boolean = False
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            success = imp.Start()
            If success Then
                strFileName = "DP_" & hdnDiscountProposalHeaderID.Value & "_" & Now.Year.ToString & Now.Month.ToString("d2") & Now.Day.ToString("d2") & "_" & Now.Hour.ToString("d2") & Now.Minute.ToString("d2") & Now.Second.ToString("d2") & ".pdf"
                strDestFile = "DiscountProposal\Fleet\" & objDealer.DealerCode & "\" & strFileName
                Dim document As Document = New Document()
                document.LoadFromFile(tempPath)
                document.SaveToFile(TargetDirectory & strDestFile, Spire.Doc.FileFormat.PDF)

                imp.StopImpersonate()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UploadDocXFile(ByVal bytes As Byte(), ByVal tempPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim success As Boolean = False
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            success = imp.Start()
            If imp.Start() Then
                If Not System.IO.Directory.Exists(tempPath) Then
                    System.IO.Directory.CreateDirectory(Path.GetDirectoryName(tempPath))
                End If

                If System.IO.File.Exists(tempPath) Then
                    System.IO.File.Delete(Path.GetDirectoryName(tempPath))
                End If

                Try
                    Dim wFile As System.IO.FileStream
                    wFile = New FileStream(tempPath, FileMode.Append)
                    wFile.Write(bytes, 0, bytes.Length)
                    wFile.Close()
                Catch ex As IOException
                    MsgBox(ex.ToString)
                End Try

                imp.StopImpersonate()
                imp = Nothing
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Class HistoryPembelian
        Private _model As String
        Private _tahun_faktur As String
        Private _jumlah As Integer

        Public Property Model() As String
            Get
                Return _model
            End Get
            Set(ByVal Value As String)
                _model = Value
            End Set
        End Property

        Public Property Tahun_Faktur() As String
            Get
                Return _tahun_faktur
            End Get
            Set(ByVal Value As String)
                _tahun_faktur = Value
            End Set
        End Property

        Public Property Jumlah() As Integer
            Get
                Return _jumlah
            End Get
            Set(ByVal Value As Integer)
                _jumlah = Value
            End Set
        End Property
    End Class

    Function CreateTableHistoryPembelian(ByVal doc As Document, ByVal j As Integer)
        Dim section As SpireDoc.Section = doc.Sections(j)
        Dim selection As SpireDoc.Documents.TextSelection = doc.FindString("gridData_Purchase_History", True, True)
        Dim range As TextRange = selection.GetAsOneRange()

        arlHistoryPembelian = CType(sessHelper.GetSession(sessHistoryPembelianToPDF), DataTable)
        If IsNothing(arlHistoryPembelian) Then arlHistoryPembelian = New DataTable
        If arlHistoryPembelian.Rows.Count = 0 Then
            range.Text = ""
            Exit Function
        End If

        Dim paragraph As SpireDoc.Documents.Paragraph = range.OwnerParagraph
        Dim body As SpireDoc.Body = paragraph.OwnerTextBody
        Dim index As Integer = body.ChildObjects.IndexOf(paragraph)
        Dim table As SpireDoc.Table = section.AddTable(True)
        table.TableFormat.IsBreakAcrossPages = False

        'Create Header and Data
        Dim arlObj As DataTable = GetInversedDataTable(arlHistoryPembelian, "Tahun_Faktur", "Model", "Jumlah", "", True)
        Dim Header(arlObj.Columns.Count) As String
        Header(0) = "No"
        For i As Integer = 0 To arlObj.Columns.Count - 1
            Dim dc As DataColumn = arlObj.Columns(i)
            Header(i + 1) = dc.ColumnName
        Next i

        table.ResetCells(arlObj.Rows.Count + 1, Header.Length)
        table.TableFormat.WrapTextAround = True
        table.TableFormat.Positioning.HorizPositionAbs = SpireDoc.HorizontalPosition.Outside
        table.TableFormat.Positioning.VertRelationTo = SpireDoc.VerticalRelation.Margin
        table.TableFormat.Positioning.VertPosition = 10
        Dim width As SpireDoc.PreferredWidth = New SpireDoc.PreferredWidth(SpireDoc.WidthType.Percentage, 70)
        table.PreferredWidth = width

        'Header Row
        Dim FRow As SpireDoc.TableRow = table.Rows(0)
        FRow.IsHeader = True
        'Row Height
        FRow.Height = 5
        'Header Format
        FRow.RowFormat.BackColor = Color.PaleTurquoise
        For i As Integer = 0 To Header.Length - 1
            'Cell Alignment
            Dim p As SpireDoc.Documents.Paragraph = FRow.Cells(i).AddParagraph()
            FRow.Cells(i).CellFormat.VerticalAlignment = SpireDoc.Documents.VerticalAlignment.Middle
            p.Format.HorizontalAlignment = SpireDoc.Documents.HorizontalAlignment.Center
            'Data Format
            Dim TR As TextRange = p.AppendText(Header(i))
            TR.CharacterFormat.FontName = "MMC Office"
            TR.CharacterFormat.FontSize = 10
            TR.CharacterFormat.TextColor = Color.Black
            TR.CharacterFormat.Bold = True
            If i = 0 Then
                FRow.Cells(i).Width = 15
            End If
        Next i

        'Data Row
        For r As Integer = 0 To arlObj.Rows.Count - 1
            Dim DataRow As SpireDoc.TableRow = table.Rows(r + 1)

            'Row Height
            DataRow.Height = 5

            Dim c As Integer = 0
            While c <= Header.Length - 1
                'Cell Alignment
                DataRow.Cells(c).CellFormat.VerticalAlignment = SpireDoc.Documents.VerticalAlignment.Middle
                'Fill Data in Rows

                Dim p2 As SpireDoc.Documents.Paragraph
                Dim TR2 As TextRange
                p2 = DataRow.Cells(c).AddParagraph()
                If c = 0 Then
                    TR2 = p2.AppendText(r + 1)
                Else
                    Dim trText As String = ""
                    Try
                        trText = arlObj(r)(c - 1).ToString
                    Catch
                        trText = ""
                    End Try
                    TR2 = p2.AppendText(trText)
                End If

                'Format Cells
                p2.Format.HorizontalAlignment = SpireDoc.Documents.HorizontalAlignment.Center
                TR2.CharacterFormat.FontName = "MMC Office"
                TR2.CharacterFormat.FontSize = 10
                TR2.CharacterFormat.TextColor = Color.Black

                c += 1
            End While
        Next r

        body.ChildObjects.Remove(paragraph)
        body.ChildObjects.Insert(index, table)
    End Function

    Function CreateTableRincianHargaKendaraan(ByVal doc As Document, ByVal j As Integer)
        Dim section As SpireDoc.Section = doc.Sections(j)
        Dim selection As SpireDoc.Documents.TextSelection = doc.FindString("gridData_Detail_Price", True, True)
        Dim range As TextRange = selection.GetAsOneRange()
        Dim paragraph As SpireDoc.Documents.Paragraph = range.OwnerParagraph
        Dim body As SpireDoc.Body = paragraph.OwnerTextBody
        Dim index As Integer = body.ChildObjects.IndexOf(paragraph)
        Dim table As SpireDoc.Table = section.AddTable(True)
        table.TableFormat.IsBreakAcrossPages = False

        'Create Header and Data
        arlDiscountProposalDtlPrice = CType(sessHelper.GetSession(sessDiscountProposalDtlPrice), ArrayList)
        If IsNothing(arlDiscountProposalDtlPrice) Then arlDiscountProposalDtlPrice = New ArrayList()

        Dim oldTable As DataTable = sessHelper.GetSession(sessDiscountProposalDtlPricePivotGrid)
        If IsNothing(oldTable) Then oldTable = New DataTable

        Dim arlObj As DataTable = CType(sessHelper.GetSession(sessDiscountProposalDtlPricePivotGrid), DataTable)
        Dim flag As Boolean = False
        For i As Integer = 0 To arlObj.Columns.Count - 1
            flag = False
            For k As Integer = 0 To dt.Rows.Count - 1
                If Not (arlObj.Rows(k).IsNull(i)) AndAlso arlObj.Rows(k)(i).ToString().Trim = "" Then
                    flag = True
                    Exit For
                End If
            Next
            If flag Then
                arlObj.Columns.RemoveAt(i)
                Exit For
            End If
        Next

        Dim Header(arlObj.Columns.Count - 1) As String
        For i As Integer = 0 To arlObj.Columns.Count - 1
            Dim dc As DataColumn = arlObj.Columns(i)
            If dc.ColumnName.Trim <> "" Then
                If dc.ColumnName.Trim = "col0" Then
                    Header(i) = "Keterangan Kendaraan :"
                Else
                    Header(i) = dc.ColumnName.Replace("_", " ")
                End If
            End If
        Next i

        table.ResetCells(arlObj.Rows.Count + 1, Header.Length)
        table.TableFormat.WrapTextAround = True
        table.TableFormat.Positioning.HorizPositionAbs = SpireDoc.HorizontalPosition.Outside
        table.TableFormat.Positioning.VertRelationTo = SpireDoc.VerticalRelation.Margin
        table.TableFormat.Positioning.VertPosition = 10

        Dim width As SpireDoc.PreferredWidth
        If Header.Length <= 2 Then
            width = New SpireDoc.PreferredWidth(SpireDoc.WidthType.Percentage, 70)
        ElseIf Header.Length = 3 Then
            width = New SpireDoc.PreferredWidth(SpireDoc.WidthType.Percentage, 85)
        Else
            width = New SpireDoc.PreferredWidth(SpireDoc.WidthType.Percentage, 100)
        End If
        table.PreferredWidth = width

        'Header Row
        Dim FRow As SpireDoc.TableRow = table.Rows(0)
        FRow.IsHeader = True
        FRow.Height = 20
        FRow.RowFormat.BackColor = Color.PaleTurquoise
        For i As Integer = 0 To Header.Length - 1
            'Cell Alignment
            Dim p As SpireDoc.Documents.Paragraph = FRow.Cells(i).AddParagraph()
            FRow.Cells(i).CellFormat.VerticalAlignment = SpireDoc.Documents.VerticalAlignment.Middle
            If i = 0 Then
                p.Format.HorizontalAlignment = SpireDoc.Documents.HorizontalAlignment.Left
            Else
                p.Format.HorizontalAlignment = SpireDoc.Documents.HorizontalAlignment.Center
            End If
            'Data Format
            Dim TR As TextRange = p.AppendText(Header(i))
            TR.CharacterFormat.FontName = "MMC Office"
            TR.CharacterFormat.FontSize = 10
            TR.CharacterFormat.TextColor = Color.Black
            TR.CharacterFormat.Bold = True
        Next i

        'Data Row
        For r As Integer = 0 To arlObj.Rows.Count - 1
            Dim DataRow As SpireDoc.TableRow = table.Rows(r + 1)
            'Row Height
            DataRow.Height = 20

            'C Represents Column.
            For c As Integer = 0 To Header.Length - 1
                'Cell Alignment
                DataRow.Cells(c).CellFormat.VerticalAlignment = SpireDoc.Documents.VerticalAlignment.Middle
                'Fill Data in Rows
                Dim p2 As SpireDoc.Documents.Paragraph = DataRow.Cells(c).AddParagraph()
                Dim TR2 As TextRange = p2.AppendText((arlObj(r)(c)).ToString().Replace("_", " "))
                'Format Cells
                p2.Format.HorizontalAlignment = SpireDoc.Documents.HorizontalAlignment.Left

                TR2.CharacterFormat.FontName = "MMC Office"
                TR2.CharacterFormat.FontSize = 10
                TR2.CharacterFormat.TextColor = Color.Black
                If Right((arlObj(r)(c)).ToString().Trim, 1) = ":" Then
                    TR2.CharacterFormat.Bold = True
                    TR2.CharacterFormat.FontSize = 10
                    DataRow.RowFormat.BackColor = Color.PaleTurquoise
                End If
                If r = arlObj.Rows.Count - 1 Then
                    DataRow.Cells(c).CellFormat.BackColor = Color.PaleTurquoise
                End If
            Next c
        Next r

        body.ChildObjects.Remove(paragraph)
        body.ChildObjects.Insert(index, table)
    End Function

    Function CreateTableDPDetailOwnership(ByVal doc As Document, ByVal j As Integer)
        Dim section As SpireDoc.Section = doc.Sections(j)
        Dim selection As SpireDoc.Documents.TextSelection = doc.FindString("gridData_Vehicle_Owned", True, True)
        Dim range As TextRange = selection.GetAsOneRange()
        Dim paragraph As SpireDoc.Documents.Paragraph = range.OwnerParagraph
        Dim body As SpireDoc.Body = paragraph.OwnerTextBody
        Dim index As Integer = body.ChildObjects.IndexOf(paragraph)
        Dim table As SpireDoc.Table = section.AddTable(True)
        table.TableFormat.IsBreakAcrossPages = False

        'Create Header and Data
        Dim Header() As String = {"No", "Brand", "Merk", "Model", "Unit"}

        Dim arlObj As New ArrayList
        Dim oDiscountProposalHeader As DiscountProposalHeader = New DiscountProposalHeaderFacade(User).Retrieve(CInt(hdnDiscountProposalHeaderID.Value))
        For Each obj As DiscountProposalDetailOwnership In oDiscountProposalHeader.DiscountProposalDetailOwnerships
            arlObj.Add(obj)
        Next

        table.ResetCells(arlObj.Count + 1, Header.Length)
        table.TableFormat.WrapTextAround = True
        table.TableFormat.Positioning.HorizPositionAbs = SpireDoc.HorizontalPosition.Outside
        table.TableFormat.Positioning.VertRelationTo = SpireDoc.VerticalRelation.Margin
        table.TableFormat.Positioning.VertPosition = 10

        Dim width As SpireDoc.PreferredWidth = New SpireDoc.PreferredWidth(SpireDoc.WidthType.Percentage, 75)
        table.PreferredWidth = width

        'Header Row
        Dim FRow As SpireDoc.TableRow = table.Rows(0)
        FRow.IsHeader = True
        'Row Height
        FRow.Height = 20
        'Header Format
        FRow.RowFormat.BackColor = Color.PaleTurquoise
        For i As Integer = 0 To Header.Length - 1
            'Cell Alignment
            Dim p As SpireDoc.Documents.Paragraph = FRow.Cells(i).AddParagraph()
            FRow.Cells(i).CellFormat.VerticalAlignment = SpireDoc.Documents.VerticalAlignment.Middle
            p.Format.HorizontalAlignment = SpireDoc.Documents.HorizontalAlignment.Center
            'Data Format
            Dim TR As TextRange = p.AppendText(Header(i))
            TR.CharacterFormat.FontName = "MMC Office"
            TR.CharacterFormat.FontSize = 10
            TR.CharacterFormat.TextColor = Color.Black
            TR.CharacterFormat.Bold = True
            If i = 0 Then
                FRow.Cells(i).Width = 20
            End If
            If i = Header.Length - 1 Then
                FRow.Cells(i).Width = 50
            End If
        Next i

        'Data Row
        For r As Integer = 0 To arlObj.Count - 1
            Dim objDPOwnership As DiscountProposalDetailOwnership = CType(arlObj(r), DiscountProposalDetailOwnership)
            Dim DataRow As SpireDoc.TableRow = table.Rows(r + 1)

            'Row Height
            DataRow.Height = 20
            Dim cols(Header.Length) As String
            cols(0) = r + 1
            cols(1) = CommonFunction.GetEnumDescription(objDPOwnership.VehicleBrandCategory, "EnumDiscountProposal.BrandCategory")
            cols(2) = objDPOwnership.VehicleBrandName
            cols(3) = objDPOwnership.VehicleModel
            cols(4) = objDPOwnership.Quantity

            'C Represents Column.
            For c As Integer = 0 To Header.Length - 1
                'Cell Alignment
                DataRow.Cells(c).CellFormat.VerticalAlignment = SpireDoc.Documents.VerticalAlignment.Middle
                'Fill Data in Rows
                Dim p2 As SpireDoc.Documents.Paragraph = DataRow.Cells(c).AddParagraph()
                Dim TR2 As TextRange = p2.AppendText(cols(c))
                'Format Cells
                p2.Format.HorizontalAlignment = SpireDoc.Documents.HorizontalAlignment.Center
                TR2.CharacterFormat.FontName = "MMC Office"
                TR2.CharacterFormat.FontSize = 10
                TR2.CharacterFormat.TextColor = Color.Black
            Next c
        Next r

        body.ChildObjects.Remove(paragraph)
        body.ChildObjects.Insert(index, table)
    End Function

    Function CreateTableDPDetailApproval(ByVal doc As Document, ByVal j As Integer)
        Dim section As SpireDoc.Section = doc.Sections(j)
        Dim selection As SpireDoc.Documents.TextSelection = doc.FindString("gridData_Propose_Discount", True, True)
        Dim range As TextRange = selection.GetAsOneRange()
        Dim paragraph As SpireDoc.Documents.Paragraph = range.OwnerParagraph
        Dim body As SpireDoc.Body = paragraph.OwnerTextBody
        Dim index As Integer = body.ChildObjects.IndexOf(paragraph)
        Dim table As SpireDoc.Table = section.AddTable(True)
        table.TableFormat.IsBreakAcrossPages = False

        'Create Header and Data
        Dim Header() As String = {"No", "Model", "Tipe", "Unit", "Discount Type", "Application No.", "Detail Discount", "Price Reff", "TOP", "Interest", "Delivery Time"}

        Dim strLabelTotalDiscount As String = "Total Discount :"
        Dim arlObjDPAtoSPL As New ArrayList
        arlDiscountProposalDtlApprovalToSPL = CType(sessHelper.GetSession(sessDiscountProposalDtlApprovalToSPL), ArrayList)
        If IsNothing(arlDiscountProposalDtlApprovalToSPL) Then arlDiscountProposalDtlApprovalToSPL = New ArrayList()
        arlDiscountProposalDtlApproval = CType(sessHelper.GetSession(sessDiscountProposalDtlApproval), ArrayList)
        If IsNothing(arlDiscountProposalDtlApproval) Then arlDiscountProposalDtlApproval = New ArrayList
        If arlDiscountProposalDtlApproval.Count = 0 Then
            range.Text = ""
            Exit Function
        End If

        For Each obj As DiscountProposalDetailApprovaltoSPL In arlDiscountProposalDtlApprovalToSPLtoPDF
            arlObjDPAtoSPL.Add(obj)
        Next

        table.ResetCells(arlObjDPAtoSPL.Count + 1, Header.Length)
        table.TableFormat.WrapTextAround = True
        table.TableFormat.Positioning.HorizPositionAbs = SpireDoc.HorizontalPosition.Outside
        table.TableFormat.Positioning.VertRelationTo = SpireDoc.VerticalRelation.Margin
        table.TableFormat.Positioning.VertPosition = 10
        Dim width As SpireDoc.PreferredWidth = New SpireDoc.PreferredWidth(SpireDoc.WidthType.Percentage, 100)
        table.PreferredWidth = width

        'Header Row
        Dim FRow As SpireDoc.TableRow = table.Rows(0)
        FRow.IsHeader = True
        'Row Height
        FRow.Height = 18
        'Header Format
        FRow.RowFormat.BackColor = Color.PaleTurquoise
        For i As Integer = 0 To Header.Length - 1
            'Cell Alignment
            Dim p As SpireDoc.Documents.Paragraph = FRow.Cells(i).AddParagraph()
            FRow.Cells(i).CellFormat.VerticalAlignment = SpireDoc.Documents.VerticalAlignment.Middle
            p.Format.HorizontalAlignment = SpireDoc.Documents.HorizontalAlignment.Center
            'Data Format
            Dim TR As TextRange = p.AppendText(Header(i))
            TR.CharacterFormat.FontName = "MMC Office"
            TR.CharacterFormat.FontSize = 10
            TR.CharacterFormat.TextColor = Color.Black
            TR.CharacterFormat.Bold = True
            If i = 0 Then
                FRow.Cells(i).Width = 15
            End If
        Next i

        'Data Row
        Dim strVechileType As String = ""
        Dim objDPAtoSPL As DiscountProposalDetailApprovaltoSPL
        Dim q As Integer = 0
        For r As Integer = 0 To arlObjDPAtoSPL.Count - 1
            objDPAtoSPL = CType(arlObjDPAtoSPL(r), DiscountProposalDetailApprovaltoSPL)
            Dim DataRow As SpireDoc.TableRow = table.Rows(r + 1)

            'Row Height
            DataRow.Height = 20
            Dim cols(Header.Length) As String

            Dim i% = 0
            Dim oDPA As DiscountProposalDetailApproval
            If arlDiscountProposalDtlApproval.Count > 0 Then
                For Each _oDPA As DiscountProposalDetailApproval In arlDiscountProposalDtlApproval
                    If _oDPA.VechileTypeID = objDPAtoSPL.VechileTypeID AndAlso _oDPA.ModelID = objDPAtoSPL.ModelID Then
                        oDPA = _oDPA
                        Exit For
                    End If
                    i += 1
                Next
            End If

            Dim objSubCategoryVehicleToModel As New SubCategoryVehicleToModel
            Dim strLabelTotal As String = If(Not IsNothing(objDPAtoSPL.LabelTotal), objDPAtoSPL.LabelTotal, "")
            If strLabelTotal.Trim.ToLower <> strLabelTotalDiscount.Trim.ToLower Then
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicleToModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SubCategoryVehicleToModel), "VechileModel.ID", MatchType.Exact, oDPA.VechileType.VechileModel.ID))
                Dim arrSCVToModel As ArrayList = New SubCategoryVehicleToModelFacade(User).Retrieve(criterias)
                If Not IsNothing(arrSCVToModel) AndAlso arrSCVToModel.Count > 0 Then
                    objSubCategoryVehicleToModel = CType(arrSCVToModel(0), SubCategoryVehicleToModel)
                End If

                If strVechileType.ToLower.Trim <> oDPA.VechileType.VechileTypeCode.ToLower.Trim Then
                    strVechileType = oDPA.VechileType.VechileTypeCode
                    q += 1
                End If

                cols(0) = q
                cols(1) = objSubCategoryVehicleToModel.SubCategoryVehicle.Name
                cols(2) = oDPA.VechileType.VechileTypeCode & " (" & oDPA.VechileType.Description & ")"
                cols(3) = oDPA.ResponseQty
                cols(4) = objDPAtoSPL.DiscountMaster.Category
                cols(5) = If(Not IsNothing(objDPAtoSPL.SPLDetail), objDPAtoSPL.SPLDetail.SPL.SPLNumber, "")
                cols(6) = Format(objDPAtoSPL.DiscountProposed, "#,##0")
                cols(7) = If(oDPA.PriceReff = System.Data.SqlTypes.SqlDateTime.MinValue.Value, "Update Price", oDPA.PriceReff.ToString("MMyyyy"))
                cols(8) = oDPA.MaxTOPDay
                cols(9) = If(CommonFunction.GetEnumDescription(oDPA.FreeIntIndicator, "EnumDiscountProposal.InterestIndicator").Trim <> "", CommonFunction.GetEnumDescription(oDPA.FreeIntIndicator, "EnumDiscountProposal.InterestIndicator"), "")
                cols(10) = enumMonthGet.GetName(oDPA.DeliveryDate.Month) & " " & oDPA.DeliveryDate.Year.ToString("d4")
            Else
                cols(0) = strLabelTotalDiscount & " "
                cols(1) = ""
                cols(2) = ""
                cols(3) = ""
                cols(4) = ""
                cols(5) = ""
                cols(6) = Format(objDPAtoSPL.TotalDiscount, "#,##0")
                cols(7) = ""
                cols(8) = ""
                cols(9) = ""
                cols(10) = ""
            End If

            'C Represents Column.
            For c As Integer = 0 To Header.Length - 1
                'Cell Alignment
                DataRow.Cells(c).CellFormat.VerticalAlignment = SpireDoc.Documents.VerticalAlignment.Middle
                'Fill Data in Rows
                Dim p2 As SpireDoc.Documents.Paragraph = DataRow.Cells(c).AddParagraph()
                Dim TR2 As TextRange = p2.AppendText(cols(c))
                'Format Cells
                p2.Format.HorizontalAlignment = SpireDoc.Documents.HorizontalAlignment.Center
                TR2.CharacterFormat.FontName = "MMC Office"
                TR2.CharacterFormat.FontSize = 10
                TR2.CharacterFormat.TextColor = Color.Black
                If cols(0).Trim.ToLower = strLabelTotalDiscount.Trim.ToLower Then
                    TR2.CharacterFormat.Bold = True
                    TR2.CharacterFormat.FontSize = 10
                    p2.Format.HorizontalAlignment = SpireDoc.Documents.HorizontalAlignment.Right
                    DataRow.Cells(c).CellFormat.BackColor = Color.PaleTurquoise
                    DataRow.Cells(6).CellFormat.VerticalAlignment = SpireDoc.Documents.VerticalAlignment.Middle
                End If
            Next c
        Next r

        Dim x As Integer = 0
        Dim y As Integer = 0
        Dim objDPAtoSPL2 As DiscountProposalDetailApprovaltoSPL
        For r As Integer = 0 To arlObjDPAtoSPL.Count - 1
            objDPAtoSPL = CType(arlObjDPAtoSPL(r), DiscountProposalDetailApprovaltoSPL)
            If r > 0 Then
                objDPAtoSPL2 = CType(arlObjDPAtoSPL(r - 1), DiscountProposalDetailApprovaltoSPL)
                Dim strLabelTotalLoop As String = String.Empty
                If Not IsNothing(objDPAtoSPL.LabelTotal) Then
                    strLabelTotalLoop = objDPAtoSPL.LabelTotal.ToLower
                End If
                If objDPAtoSPL.VechileTypeID = objDPAtoSPL2.VechileTypeID AndAlso _
                   objDPAtoSPL.ModelID = objDPAtoSPL2.ModelID AndAlso _
                   strLabelTotalLoop.Trim.ToLower <> strLabelTotalDiscount.Trim.ToLower Then
                    If x = 0 Then
                        y = r
                        x = 1
                    Else
                        x += 1
                    End If
                End If
                If strLabelTotalLoop.Trim.ToLower = strLabelTotalDiscount.Trim.ToLower Then
                    table.ApplyHorizontalMerge(r + 1, 0, 5)
                    table.ApplyHorizontalMerge(r + 1, 7, 10)

                    If x > 0 Then
                        table.ApplyVerticalMerge(0, y, y + x)
                        table.ApplyVerticalMerge(1, y, y + x)
                        table.ApplyVerticalMerge(2, y, y + x)
                        table.ApplyVerticalMerge(3, y, y + x)
                        table.ApplyVerticalMerge(7, y, y + x)
                        table.ApplyVerticalMerge(8, y, y + x)
                        table.ApplyVerticalMerge(9, y, y + x)
                        table.ApplyVerticalMerge(10, y, y + x)
                    End If
                    x = 0
                End If
            End If
        Next

        body.ChildObjects.Remove(paragraph)
        body.ChildObjects.Insert(index, table)
    End Function

    Public Class ConsiderationDP
        Private _valueCheck As Integer
        Private _descText As String

        Public Property ValueCheck() As Integer
            Get
                Return _valueCheck
            End Get
            Set(ByVal Value As Integer)
                _valueCheck = Value
            End Set
        End Property

        Public Property DescText() As String
            Get
                Return _descText
            End Get
            Set(ByVal Value As String)
                _descText = Value
            End Set
        End Property
    End Class

    Function CreateTableConsideration(ByVal doc As Document, ByVal j As Integer)
        Dim section As SpireDoc.Section = doc.Sections(j)
        Dim selection As SpireDoc.Documents.TextSelection = doc.FindString("Distributor_Consideration", True, True)
        Dim range As TextRange = selection.GetAsOneRange()
        Dim paragraph As SpireDoc.Documents.Paragraph = range.OwnerParagraph
        Dim body As SpireDoc.Body = paragraph.OwnerTextBody
        Dim index As Integer = body.ChildObjects.IndexOf(paragraph)
        Dim table As SpireDoc.Table = section.AddTable(True)
        table.TableFormat.IsBreakAcrossPages = False

        'Create Header and Data
        Dim sb As StringBuilder = New StringBuilder

        Dim Header() As String = {" ", " "}

        Dim arlObj As New ArrayList
        Dim data As DiscountProposalHeader = New DiscountProposalHeaderFacade(User).Retrieve(CInt(hdnDiscountProposalHeaderID.Value))

        If Not IsNothing(data) Then
            If data.Consideration.Trim <> "" Then
                For Each strID As String In data.Consideration.Split(";")
                    Dim criterias As New CriteriaComposite(New Criteria(GetType(DiscountProposalParameter), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(DiscountProposalParameter), "Status", MatchType.Exact, 1))
                    criterias.opAnd(New Criteria(GetType(DiscountProposalParameter), "ParameterType", MatchType.Exact, 3))
                    If strID.Trim = "" Then strID = "0"
                    criterias.opAnd(New Criteria(GetType(DiscountProposalParameter), "ID", MatchType.Exact, CInt(strID)))
                    Dim arrDiscountProposalParameter As ArrayList = New DiscountProposalParameterFacade(User).Retrieve(criterias)
                    If Not IsNothing(arrDiscountProposalParameter) AndAlso arrDiscountProposalParameter.Count > 0 Then
                        Dim oDPParam As DiscountProposalParameter = CType(arrDiscountProposalParameter(0), DiscountProposalParameter)
                        If Not IsNothing(oDPParam) AndAlso oDPParam.ID > 0 Then
                            Dim oConsiderationDP As New ConsiderationDP
                            oConsiderationDP.ValueCheck = oDPParam.ID
                            oConsiderationDP.DescText = oDPParam.ParameterName
                            arlObj.Add(oConsiderationDP)
                        End If
                    End If
                Next
            End If
        End If
        If arlObj.Count = 0 Then
            range.Text = ""
            Exit Function
        End If

        table.ResetCells(arlObj.Count + 1, Header.Length)
        table.TableFormat.WrapTextAround = True
        table.TableFormat.Positioning.HorizPositionAbs = SpireDoc.HorizontalPosition.Outside
        table.TableFormat.Positioning.VertRelationTo = SpireDoc.VerticalRelation.Margin
        table.TableFormat.Positioning.VertPosition = 10

        Dim width As SpireDoc.PreferredWidth = New SpireDoc.PreferredWidth(SpireDoc.WidthType.Percentage, 50)
        table.PreferredWidth = width

        'Data Row
        Dim cols(Header.Length) As String
        Dim p As Integer = 0, q As Integer = 0
        For c As Integer = 0 To Header.Length - 1
            If p > arlObj.Count - 1 Then Exit For
            If q > arlObj.Count - 1 Then Exit For
            For r As Integer = p To arlObj.Count - 1
                If c = 0 AndAlso r > 3 Then
                    p = 4
                    Exit For
                End If
                If c = 1 AndAlso r > 7 Then
                    p = 8
                    Exit For
                End If
                Dim objConsiderationDP As ConsiderationDP = CType(arlObj(r), ConsiderationDP)
                Dim DataRow As SpireDoc.TableRow = table.Rows(r - p)

                'Row Height
                DataRow.Height = 20
                cols(c) = objConsiderationDP.DescText

                'Cell Alignment
                DataRow.Cells(c).CellFormat.VerticalAlignment = SpireDoc.Documents.VerticalAlignment.Middle
                'Fill Data in Rows
                Dim p2 As SpireDoc.Documents.Paragraph = DataRow.Cells(c).AddParagraph()
                Dim TR2 As TextRange = p2.AppendText(cols(c))
                'Format Cells
                p2.Format.HorizontalAlignment = SpireDoc.Documents.HorizontalAlignment.Left
                TR2.CharacterFormat.FontName = "MMC Office"
                TR2.CharacterFormat.FontSize = 10
                TR2.CharacterFormat.TextColor = Color.Black

                q += 1
            Next r
        Next c

        If arlObj.Count <= 4 Then
            For i As Integer = 0 To table.Rows.Count - 1
                table.Rows(i).Cells.RemoveAt(1)
            Next
        End If
        If arlObj.Count > 0 Then
            If table.Rows.Count > 4 Then
                Dim rowCount As Integer = table.Rows.Count
                For i As Integer = 4 To rowCount - 1
                    table.Rows.RemoveAt(table.Rows.Count - 1)
                Next
            End If
        End If

        body.ChildObjects.Remove(paragraph)
        body.ChildObjects.Insert(index, table)
    End Function

    Private Sub CreateTableinWord(ByVal tempPath As String, ByRef tempPath2 As String)
        'Load Document
        Dim doc As New Document()
        Dim filePath As String = tempPath
        doc.LoadFromFile(filePath)

        'Set Margins
        doc.Sections(0).PageSetup.Margins.Top = 17.9F
        doc.Sections(0).PageSetup.Margins.Bottom = 17.9F

        CreateTableDPDetailOwnership(doc, 0)
        CreateTableDPDetailApproval(doc, 0)
        CreateTableHistoryPembelian(doc, 0)
        CreateTableRincianHargaKendaraan(doc, 0)
        CreateTableConsideration(doc, 0)

        ''Save and Launch
        tempPath2 = KTB.DNet.Lib.WebConfig.GetValue("SAN") + "DataTemp\DiscountProposal\" + Guid.NewGuid().ToString().Substring(0, 5) + ".docx"
        doc.SaveToFile(tempPath2, Spire.Doc.FileFormat.Docx)
    End Sub

    Private Sub btnReloadDataFleet_Click(sender As Object, e As EventArgs) Handles btnReloadDataFleet.Click
        If hdnFleetCustomerHeaderID.Value <> "" Then
            Dim objFleetCustomerHeader As FleetCustomerHeader = New FleetCustomerHeaderFacade(User).Retrieve(CInt(hdnFleetCustomerHeaderID.Value))
            If Not IsNothing(objFleetCustomerHeader) AndAlso objFleetCustomerHeader.ID > 0 Then
                txtFleetCustomerName.Text = objFleetCustomerHeader.FleetCustomerName
                hdnFleetCustomerHeaderID.Value = objFleetCustomerHeader.ID
                If Not IsNothing(objFleetCustomerHeader.BusinessSectorDetail) Then
                    ddlBusinessSector.SelectedValue = objFleetCustomerHeader.BusinessSectorDetail.ID
                End If
                Dim criterias1 As New CriteriaComposite(New Criteria(GetType(FleetCustomerDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias1.opAnd(New Criteria(GetType(FleetCustomerDetail), "Dealer.ID", MatchType.Exact, objDealer.ID))
                criterias1.opAnd(New Criteria(GetType(FleetCustomerDetail), "FleetCustomerHeader.ID", MatchType.Exact, hdnFleetCustomerHeaderID.Value))
                Dim arlFleetCustomerDetail As ArrayList = New FleetCustomerDetailFacade(User).Retrieve(criterias1)
                If IsNothing(arlFleetCustomerDetail) Then arlFleetCustomerDetail = New ArrayList
                If Not IsNothing(arlFleetCustomerDetail) AndAlso arlFleetCustomerDetail.Count > 0 Then
                    Dim objFleetCustomerDetail As FleetCustomerDetail = CommonFunction.SortListControl(arlFleetCustomerDetail, "CreatedTime", Sort.SortDirection.DESC)(0)
                    hdnFleetCustomerDetailID.Value = objFleetCustomerDetail.ID
                    txtNoKTP.Text = Left(objFleetCustomerDetail.IdentityNumber, 16)
                    txtNoNIB.Text = Left(objFleetCustomerDetail.IdentityNumber, 13)
                    txtAddressFleetCustomerDtl.Text = objFleetCustomerDetail.Address
                    txtFleetCustomerName.Enabled = False
                    'txtNoKTP.Enabled = False
                    'txtNoNIB.Enabled = False
                    txtNoKTP.Enabled = True
                    txtNoNIB.Enabled = True
                Else
                    hdnFleetCustomerHeaderID.Value = ""
                    hdnFleetCustomerDetailID.Value = ""
                    txtNoKTP.Text = ""
                    txtNoNIB.Text = ""
                    txtAddressFleetCustomerDtl.Text = ""
                    txtFleetCustomerName.Enabled = True
                    txtNoKTP.Enabled = True
                    txtNoNIB.Enabled = True
                    MessageBox.Show("Data Fleet Customer, NIB/NIK dan alamat tidak ada")
                End If

                If hdnFleetCustomerDetailID.Value = "" Then
                    txtFleetCustomerName.Enabled = True
                End If
                If txtNoNIB.Text.Trim = "" Then
                    txtNoNIB.Enabled = True
                End If
                If txtNoKTP.Text.Trim = "" Then
                    txtNoKTP.Enabled = True
                End If
                If txtAddressFleetCustomerDtl.Text.Trim = "" Then
                    txtAddressFleetCustomerDtl.Enabled = True
                End If
            End If
        End If

    End Sub

    Protected Sub LinkDownload_Click(sender As Object, e As EventArgs) Handles LinkDownload.Click
        Response.Redirect("../downloadlocal.aspx?file=DataFile\Template\DP\Draft_Perjanjian_Jual_Beli_Dealer_dengan_Kontraktor.docx")
    End Sub

    Protected Sub LinkDownloadSuratPernyataan_Click(sender As Object, e As EventArgs) Handles LinkDownloadSuratPernyataan.Click
        Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalDetailDocument), "DiscountProposalHeader.ProposalRegNo", MatchType.Exact, lblProposalRegNo.Text))
        'Dim criterias2 As New CriteriaComposite(New Criteria(GetType(DiscountProposalHeader), "ProposalRegNo", MatchType.Exact, lblProposalRegNo.Text))
        criterias2.opAnd(New Criteria(GetType(DiscountProposalDetailDocument), "FileName", MatchType.Exact, LinkDownloadSuratPernyataan.Text))
        Dim arlDiscountProposalDetailDocument As ArrayList = New DiscountProposalDetailDocumentFacade(User).Retrieve(criterias2)

        'If IsNothing(arlDiscountDetail) Then arlDiscountDetail = New ArrayList
        'If Not IsNothing(arlDiscountDetail) AndAlso arlDiscountDetail.Count > 0 Then
        '    Dim objDiscountProposalHeader As DiscountProposalHeader = CommonFunction.SortListControl(arlDiscountDetail, "CreatedTime", Sort.SortDirection.DESC)(0)
        '    Dim criterias3 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalDetailDocument), "DiscountProposalHeaderID", MatchType.Exact, objDiscountProposalHeader.ID))
        '    Dim arlDiscountProposalDetailDocument As ArrayList = New DiscountProposalDetailDocumentFacade(User).Retrieve(criterias2)
        If IsNothing(arlDiscountProposalDetailDocument) Then arlDiscountProposalDetailDocument = New ArrayList
        If Not IsNothing(arlDiscountProposalDetailDocument) AndAlso arlDiscountProposalDetailDocument.Count > 0 Then
            Dim objDiscountProposalDetailDocument As DiscountProposalDetailDocument = CommonFunction.SortListControl(arlDiscountProposalDetailDocument, "CreatedTime", Sort.SortDirection.DESC)(0)
            Dim urldownload As String = objDiscountProposalDetailDocument.Path

            Response.Redirect("~\Download.aspx?file=" & urldownload & "")
        End If
        'End If

    End Sub

    Protected Sub LinkDownloadSuratKomitmentKontrak_Click(sender As Object, e As EventArgs) Handles LinkDownloadSuratKomitmentKontrak.Click
        Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalDetailDocument), "DiscountProposalHeader.ProposalRegNo", MatchType.Exact, lblProposalRegNo.Text))
        'Dim criterias2 As New CriteriaComposite(New Criteria(GetType(DiscountProposalHeader), "ProposalRegNo", MatchType.Exact, lblProposalRegNo.Text))
        criterias2.opAnd(New Criteria(GetType(DiscountProposalDetailDocument), "FileName", MatchType.Exact, LinkDownloadSuratKomitmentKontrak.Text))
        Dim arlDiscountProposalDetailDocument As ArrayList = New DiscountProposalDetailDocumentFacade(User).Retrieve(criterias2)

        'If IsNothing(arlDiscountDetail) Then arlDiscountDetail = New ArrayList
        'If Not IsNothing(arlDiscountDetail) AndAlso arlDiscountDetail.Count > 0 Then
        '    Dim objDiscountProposalHeader As DiscountProposalHeader = CommonFunction.SortListControl(arlDiscountDetail, "CreatedTime", Sort.SortDirection.DESC)(0)
        '    Dim criterias3 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalDetailDocument), "DiscountProposalHeaderID", MatchType.Exact, objDiscountProposalHeader.ID))
        '    Dim arlDiscountProposalDetailDocument As ArrayList = New DiscountProposalDetailDocumentFacade(User).Retrieve(criterias2)
        If IsNothing(arlDiscountProposalDetailDocument) Then arlDiscountProposalDetailDocument = New ArrayList
        If Not IsNothing(arlDiscountProposalDetailDocument) AndAlso arlDiscountProposalDetailDocument.Count > 0 Then
            Dim objDiscountProposalDetailDocument As DiscountProposalDetailDocument = CommonFunction.SortListControl(arlDiscountProposalDetailDocument, "CreatedTime", Sort.SortDirection.DESC)(0)
            Dim urldownload As String = objDiscountProposalDetailDocument.Path

            Response.Redirect("~\Download.aspx?file=" & urldownload & "")
        End If
        'End If

    End Sub

    Private Sub ddlDeliveryRegionCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDeliveryRegionCode.SelectedIndexChanged
        UpdateLogisticCostRincianKendaraan()
        BindGridRincianHargaKendaraan()
    End Sub

    Private Function UpdateLogisticCostRincianKendaraan()
        arlDiscountProposalDtlPrice = sessHelper.GetSession(sessDiscountProposalDtlPrice)
        If IsNothing(arlDiscountProposalDtlPrice) Then arlDiscountProposalDtlPrice = New ArrayList

        arlDiscountProposalDtlPrice = New System.Collections.ArrayList(
                                            (From obj As DiscountProposalDetailPrice In arlDiscountProposalDtlPrice.OfType(Of DiscountProposalDetailPrice)()
                                                Order By obj.NumberRow, obj.DiscountProposalDetail.ID
                                                Select obj).ToList())

        Dim strModelID As String = ""
        Dim strVehicleTypeGeneralID As String = ""
        Dim strAssyYear As String = ""
        Dim strModelYear As String = ""
        Dim strVechileColorID As String = ""

        For Each objDPDtlPrice As DiscountProposalDetailPrice In arlDiscountProposalDtlPrice
            strModelID = ""
            strVehicleTypeGeneralID = ""
            strAssyYear = ""
            strModelYear = ""
            strVechileColorID = ""

            With objDPDtlPrice
                If Not IsNothing(.DiscountProposalDetail) Then
                    Dim objDPDtl As New DiscountProposalDetail
                    If arlDiscountProposalDtl.Count > 0 AndAlso .DiscountProposalDetail.ID > 0 Then
                        objDPDtl = (From item As DiscountProposalDetail In arlDiscountProposalDtl
                                    Where item.ID = .DiscountProposalDetail.ID
                                        Select (item)).FirstOrDefault()
                    Else
                        objDPDtl = .DiscountProposalDetail
                    End If

                    strModelID = objDPDtl.SubCategoryVehicle.ID
                    If Not IsNothing(objDPDtl.VechileColorIsActiveOnPK) Then
                        If Not IsNothing(objDPDtl.VechileColorIsActiveOnPK.VechileTypeGeneral) Then
                            strVehicleTypeGeneralID = objDPDtl.VechileColorIsActiveOnPK.VechileTypeGeneral.ID
                        Else
                            strVehicleTypeGeneralID = .VechileTypeID
                        End If
                    End If
                    strAssyYear = objDPDtl.AssyYear
                    strModelYear = objDPDtl.ModelYear
                    If Not IsNothing(objDPDtl.VechileColorIsActiveOnPK) Then
                        If Not IsNothing(objDPDtl.VechileColorIsActiveOnPK.VechileColor) Then
                            strVechileColorID = objDPDtl.VechileColorIsActiveOnPK.VechileColor.ID
                        End If
                    End If
                Else
                    strModelID = .SubCategoryVehicleID
                    strVehicleTypeGeneralID = .VechileTypeID
                    strAssyYear = .AssyYear
                    strModelYear = .ModelYear
                    strVechileColorID = .VechileColorID
                End If

                Dim dblLogisticPrice As Double = 0
                If ddlDeliveryRegionCode.SelectedIndex > 0 Then
                    Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LogisticPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crit.opAnd(New Criteria(GetType(LogisticPrice), "RegionCode", MatchType.Exact, ddlDeliveryRegionCode.SelectedValue))
                    Dim strSQL As String = "Select SAPModel from VechileType where RowStatus = 0 and ID = (Select top 1 VechileTypeID from VechileColor where RowStatus = 0 and ID = " & strVechileColorID & ")"
                    crit.opAnd(New Criteria(GetType(LogisticPrice), "SAPModel", MatchType.InSet, "(" & strSQL & ")"))
                    Dim sortColls As SortCollection = New SortCollection
                    sortColls.Add(New Sort(GetType(LogisticPrice), "EffectiveDate", Sort.SortDirection.DESC))  '-- Sort by Vechile type code
                    Dim arlLogisticPrice As ArrayList = New LogisticPriceFacade(User).RetrieveByCriteria(crit, sortColls)
                    Dim objLogisticPrice As LogisticPrice = New LogisticPrice
                    If Not IsNothing(arlLogisticPrice) AndAlso arlLogisticPrice.Count > 0 Then
                        objLogisticPrice = CType(arlLogisticPrice(0), LogisticPrice)
                    End If
                    dblLogisticPrice = objLogisticPrice.LogisticPrice
                End If
                .LogisticCost = dblLogisticPrice

            End With
        Next

        sessHelper.SetSession(sessDiscountProposalDtlPrice, arlDiscountProposalDtlPrice)
    End Function


#End Region

End Class


