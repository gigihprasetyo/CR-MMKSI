#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.VehicleData
Imports KTB.DNet.Parser
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Configuration
#End Region

Public Class PopUpVHF
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label120 As System.Web.UI.WebControls.Label
    Protected WithEvents Label121 As System.Web.UI.WebControls.Label
    Protected WithEvents lblItemNo As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblChassis As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents lblMMC As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents lblSerial As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents lblEngine As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents lblInvoice As System.Web.UI.WebControls.Label
    Protected WithEvents Label13 As System.Web.UI.WebControls.Label
    Protected WithEvents Label14 As System.Web.UI.WebControls.Label
    Protected WithEvents lblOrder As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents lblProdYear As System.Web.UI.WebControls.Label
    Protected WithEvents Label15 As System.Web.UI.WebControls.Label
    Protected WithEvents Label16 As System.Web.UI.WebControls.Label
    Protected WithEvents lblPIUDNo As System.Web.UI.WebControls.Label
    Protected WithEvents Label18 As System.Web.UI.WebControls.Label
    Protected WithEvents Label19 As System.Web.UI.WebControls.Label
    Protected WithEvents lblPIUDDate As System.Web.UI.WebControls.Label
    Protected WithEvents Label17 As System.Web.UI.WebControls.Label
    Protected WithEvents Label20 As System.Web.UI.WebControls.Label
    Protected WithEvents lblReceiptCBUDate As System.Web.UI.WebControls.Label
    Protected WithEvents Label21 As System.Web.UI.WebControls.Label
    Protected WithEvents Label22 As System.Web.UI.WebControls.Label
    Protected WithEvents lblRequestDate As System.Web.UI.WebControls.Label
    Protected WithEvents Label23 As System.Web.UI.WebControls.Label
    Protected WithEvents Label24 As System.Web.UI.WebControls.Label
    Protected WithEvents lblCTD As System.Web.UI.WebControls.Label
    Protected WithEvents Label26 As System.Web.UI.WebControls.Label
    Protected WithEvents Label27 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDOdate As System.Web.UI.WebControls.Label
    Protected WithEvents Label25 As System.Web.UI.WebControls.Label
    Protected WithEvents Label28 As System.Web.UI.WebControls.Label
    Protected WithEvents lblRCD As System.Web.UI.WebControls.Label
    Protected WithEvents Label30 As System.Web.UI.WebControls.Label
    Protected WithEvents Label31 As System.Web.UI.WebControls.Label
    Protected WithEvents lblSSD As System.Web.UI.WebControls.Label
    Protected WithEvents Label29 As System.Web.UI.WebControls.Label
    Protected WithEvents Label33 As System.Web.UI.WebControls.Label
    Protected WithEvents Label35 As System.Web.UI.WebControls.Label
    Protected WithEvents Label36 As System.Web.UI.WebControls.Label
    Protected WithEvents lblStockOutDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblNIKNo As System.Web.UI.WebControls.Label
    Protected WithEvents Label34 As System.Web.UI.WebControls.Label
    Protected WithEvents Label37 As System.Web.UI.WebControls.Label
    Protected WithEvents lblCustomer As System.Web.UI.WebControls.Label
    Protected WithEvents Label38 As System.Web.UI.WebControls.Label
    Protected WithEvents Label39 As System.Web.UI.WebControls.Label
    Protected WithEvents lblEndUser As System.Web.UI.WebControls.Label
    Protected WithEvents Label40 As System.Web.UI.WebControls.Label
    Protected WithEvents Label41 As System.Web.UI.WebControls.Label
    Protected WithEvents lblType As System.Web.UI.WebControls.Label
    Protected WithEvents Label42 As System.Web.UI.WebControls.Label
    Protected WithEvents Label43 As System.Web.UI.WebControls.Label
    Protected WithEvents lblEndUserAddress As System.Web.UI.WebControls.Label
    Protected WithEvents Label45 As System.Web.UI.WebControls.Label
    Protected WithEvents Label46 As System.Web.UI.WebControls.Label
    Protected WithEvents lblR As System.Web.UI.WebControls.Label
    Protected WithEvents Label44 As System.Web.UI.WebControls.Label
    Protected WithEvents Label47 As System.Web.UI.WebControls.Label
    Protected WithEvents lblkelurahan As System.Web.UI.WebControls.Label
    Protected WithEvents Label49 As System.Web.UI.WebControls.Label
    Protected WithEvents Label50 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKecamatan As System.Web.UI.WebControls.Label
    Protected WithEvents Label52 As System.Web.UI.WebControls.Label
    Protected WithEvents Label53 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKabKodya As System.Web.UI.WebControls.Label
    Protected WithEvents Label55 As System.Web.UI.WebControls.Label
    Protected WithEvents Label56 As System.Web.UI.WebControls.Label
    Protected WithEvents lblPropinsi As System.Web.UI.WebControls.Label
    Protected WithEvents Label48 As System.Web.UI.WebControls.Label
    Protected WithEvents Label51 As System.Web.UI.WebControls.Label
    Protected WithEvents lblFactureDate As System.Web.UI.WebControls.Label
    Protected WithEvents Label57 As System.Web.UI.WebControls.Label
    Protected WithEvents Label58 As System.Web.UI.WebControls.Label
    Protected WithEvents lblFactureNo As System.Web.UI.WebControls.Label
    Protected WithEvents Label60 As System.Web.UI.WebControls.Label
    Protected WithEvents Label61 As System.Web.UI.WebControls.Label
    Protected WithEvents lblFactureCommand As System.Web.UI.WebControls.Label
    Protected WithEvents Label54 As System.Web.UI.WebControls.Label
    Protected WithEvents Label59 As System.Web.UI.WebControls.Label
    Protected WithEvents lblVATNo As System.Web.UI.WebControls.Label
    Protected WithEvents Label63 As System.Web.UI.WebControls.Label
    Protected WithEvents Label64 As System.Web.UI.WebControls.Label
    Protected WithEvents lblVatDate As System.Web.UI.WebControls.Label
    Protected WithEvents Label62 As System.Web.UI.WebControls.Label
    Protected WithEvents Label65 As System.Web.UI.WebControls.Label
    Protected WithEvents lblSVCDateI As System.Web.UI.WebControls.Label
    Protected WithEvents Label67 As System.Web.UI.WebControls.Label
    Protected WithEvents Label68 As System.Web.UI.WebControls.Label
    Protected WithEvents lblSVCDateII As System.Web.UI.WebControls.Label
    Protected WithEvents Label66 As System.Web.UI.WebControls.Label
    Protected WithEvents Label69 As System.Web.UI.WebControls.Label
    Protected WithEvents lblSvcCustI As System.Web.UI.WebControls.Label
    Protected WithEvents Label71 As System.Web.UI.WebControls.Label
    Protected WithEvents Label72 As System.Web.UI.WebControls.Label
    Protected WithEvents lblSvcCustII As System.Web.UI.WebControls.Label
    Protected WithEvents btnClose As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Private Variables"

#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            btnClose.Attributes.Add("onClick", "window.close();")
            DisplayData()
        End If
    End Sub
#End Region

#Region "Custom Method"
    Private Sub DisplayData()
        Dim vdhID As String
        Dim objVDH As VDH
        Dim objVdhItem As VDHItem
        Dim objVdhCustomer1 As VDHCustomer
        Dim objVdhCustomer2 As VDHCustomer

        If Not IsNothing(Request.QueryString("ID")) Then
            vdhID = Request.QueryString("ID").ToString
            Try
                objVDH = New VDHFacade(User).Retrieve(CInt(vdhID))
                objVdhItem = New VDHItemFacade(User).Retrieve(objVDH.ItemNo.Trim)
                objVdhCustomer1 = New VDHCustomerFacade(User).Retrieve(objVDH.SVCCust1.Trim)
                objVdhCustomer2 = New VDHCustomerFacade(User).Retrieve(objVDH.SVCCust2.Trim)
                lblItemNo.Text = objVDH.ItemNo.Trim & " - " & objVdhItem.Descrption.Trim
                lblChassis.Text = objVDH.ChassisNo & " - " & objVdhItem.Chassis.Trim
                lblMMC.Text = IIf(objVDH.MMCLotNo.ToUpper = "NULL", "", objVDH.MMCLotNo)
                lblSerial.Text = objVDH.Serial.Trim
                lblEngine.Text = objVDH.EngineNo.Trim & " - " & objVdhItem.Engine.Trim
                lblInvoice.Text = IIf(objVDH.InvoiceBuy.ToUpper = "NULL", "", objVDH.InvoiceBuy)
                lblOrder.Text = IIf(objVDH.Orders.Trim.ToUpper = "NULL", "--", objVDH.Orders)
                lblProdYear.Text = IIf(objVDH.ProductionYear.Trim.ToUpper = "NULL", "", objVDH.ProductionYear)
                lblPIUDNo.Text = IIf(objVDH.PIUDNo.Trim.ToUpper = "NULL", "", objVDH.PIUDNo)
                lblPIUDDate.Text = IIf(objVDH.PIUDDate.Trim.ToUpper = "NULL", "", objVDH.PIUDDate)

                lblReceiptCBUDate.Text = IIf(Year(objVDH.ReceiptCBUDate) < 1970, " ", objVDH.ReceiptCBUDate.ToString("dd/MM/yyyy"))
                lblRequestDate.Text = IIf(Year(objVDH.RequestDate) < 1970, " ", objVDH.RequestDate.ToString("dd/MM/yyyy"))
                lblCTD.Text = IIf(Year(objVDH.CarrosseryTransferDate) < 1970, "--", objVDH.CarrosseryTransferDate.ToString("dd/MM/yyyy"))
                lblDOdate.Text = IIf(Year(objVDH.DOPrintDate) < 1970, " ", objVDH.DOPrintDate.ToString("dd/MM/yyyy"))
                lblRCD.Text = IIf(Year(objVDH.ReceiptCarrosseryDate) < 1970, "--", objVDH.ReceiptCarrosseryDate.ToString("dd/MM/yyyy"))
                lblSSD.Text = IIf(Year(objVDH.ScheduleShipDate) < 1970, " ", objVDH.ScheduleShipDate.ToString("dd/MM/yyyy"))
                lblStockOutDate.Text = IIf(Year(objVDH.StockOutDate) < 1970, " ", objVDH.StockOutDate.ToString("dd/MM/yyyy"))
                lblNIKNo.Text = IIf(objVDH.NIKNo.Trim.ToUpper = "NULL", "", objVDH.NIKNo)

                lblCustomer.Text = objVDH.Customer.Trim & " - " & objVDH.VDHCustomer.Name.Trim

                lblEndUser.Text = IIf(objVDH.EndCustomerName.Trim.ToUpper = "NULL", "", objVDH.EndCustomerName)
                lblEndUserAddress.Text = IIf(objVDH.EndCustomerAddress.Trim.ToUpper = "NULL", "", objVDH.EndCustomerAddress)
                lblType.Text = IIf(objVDH.Type.Trim.ToUpper = "NULL", "", objVDH.Type)
                lblR.Text = IIf(objVDH.R.Trim.ToUpper = "NULL", "", objVDH.R)
                lblkelurahan.Text = IIf(objVDH.Kelurahan.Trim.ToUpper = "NULL", "", objVDH.Kelurahan)
                lblKecamatan.Text = IIf(objVDH.Kecamatan.Trim.ToUpper = "NULL", "", objVDH.Kecamatan)
                lblKabKodya.Text = IIf(objVDH.Kabupaten.Trim.ToUpper = "NULL", "", objVDH.Kabupaten)
                lblPropinsi.Text = IIf(objVDH.Propinsi.Trim.ToUpper = "NULL", "", objVDH.Propinsi)
                lblFactureDate.Text = IIf(Year(objVDH.FactureOpenDate) < 1970, " ", objVDH.FactureOpenDate.ToString("dd/MM/yyyy"))
                lblFactureNo.Text = IIf(objVDH.FactureNo.ToUpper = "NULL", "", objVDH.FactureNo)
                lblFactureCommand.Text = IIf(objVDH.FactureComment.Trim.ToUpper = "NULL", "", objVDH.FactureComment)
                lblVATNo.Text = IIf(objVDH.VATNo.Trim.ToUpper = "NULL", "", objVDH.VATNo)
                lblVatDate.Text = IIf(Year(objVDH.VATDate) < 1970, " ", objVDH.VATDate.ToString("dd/MM/yyyy"))
                lblSVCDateI.Text = IIf(Year(objVDH.SCVDate1) < 1970, "", objVDH.SCVDate1.ToString("dd/MM/yyyy"))
                lblSVCDateII.Text = IIf(Year(objVDH.SVCDate2) < 1970, "", objVDH.SVCDate2.ToString("dd/MM/yyyy"))
                'lblSvcCustI.Text = IIf(objVDH.SVCCust1.Trim.ToUpper = "NULL", "", objVDH.SVCCust1)
                lblSvcCustI.Text = IIf(objVdhCustomer1.Name.Trim.ToUpper = "NULL", "", objVDH.SVCCust1 & " - " & objVdhCustomer1.Name)
                'lblSvcCustII.Text = IIf(objVDH.SVCCust2.Trim.ToUpper = "NULL", "", objVDH.SVCCust2)
                lblSvcCustII.Text = IIf(objVdhCustomer2.Name.Trim.ToUpper = "NULL", "", objVDH.SVCCust2 & " - " & objVdhCustomer2.Name)
            Catch ex As Exception
                objVDH = Nothing
            End Try
        End If
    End Sub
#End Region
End Class



