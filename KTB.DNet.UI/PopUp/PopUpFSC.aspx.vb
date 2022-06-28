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

Public Class PopUpFSC
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label120 As System.Web.UI.WebControls.Label
    Protected WithEvents Label121 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblChassisNo As System.Web.UI.WebControls.Label
    Protected WithEvents lblEngineNo As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents lblItemNo As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNIKNo As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents lblEndCust As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents Label13 As System.Web.UI.WebControls.Label
    Protected WithEvents lblAddress As System.Web.UI.WebControls.Label
    Protected WithEvents Label15 As System.Web.UI.WebControls.Label
    Protected WithEvents Label16 As System.Web.UI.WebControls.Label
    Protected WithEvents lblCBUReceiptDate As System.Web.UI.WebControls.Label
    Protected WithEvents Label18 As System.Web.UI.WebControls.Label
    Protected WithEvents Label19 As System.Web.UI.WebControls.Label
    Protected WithEvents LblFactureDate As System.Web.UI.WebControls.Label
    Protected WithEvents Label21 As System.Web.UI.WebControls.Label
    Protected WithEvents Label22 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDeliveryDate As System.Web.UI.WebControls.Label
    Protected WithEvents btnClose As System.Web.UI.WebControls.Button
    Protected WithEvents dtgData As System.Web.UI.WebControls.DataGrid

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
    Private arrDetail As ArrayList
#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            btnClose.Attributes.Add("onClick", "window.close();")
            DisplayData()
        End If
    End Sub

    Private Sub dtgData_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgData.ItemDataBound
        If (e.Item.ItemIndex >= 0) Then
            Dim objVDHItem As VDHService = e.Item.DataItem
            Dim lblEntryDate As Label = CType(e.Item.FindControl("lblEntryDate"), Label)
            Dim lblServiceDate As Label = CType(e.Item.FindControl("lblServiceDate"), Label)
            If Year(objVDHItem.ServiceDate) < 1970 Then
                lblServiceDate.Text = " "
            Else
                lblServiceDate.Text = objVDHItem.ServiceDate.ToString("dd/MM/yyyy")
            End If

            If Year(objVDHItem.EntryDate) < 1970 Then
                lblEntryDate.Text = " "
            Else
                lblEntryDate.Text = objVDHItem.EntryDate.ToString("dd/MM/yyyy")
            End If
        End If

    End Sub
#End Region

#Region "Custom Method"
    Private Sub DisplayData()
        Dim vdhID As String
        Dim objVDH As VDH
        Dim objVdhItem As VDHItem

        If Not IsNothing(Request.QueryString("ID")) Then
            vdhID = Request.QueryString("ID").ToString
            Try
                objVDH = New VDHFacade(User).Retrieve(CInt(vdhID))
                objVdhItem = New VDHItemFacade(User).Retrieve(objVDH.ItemNo.Trim)
                objVDH = New VDHFacade(User).Retrieve(CInt(vdhID))
                lblItemNo.Text = objVDH.ItemNo.Trim & " - " & objVdhItem.Descrption.Trim
                lblChassisNo.Text = objVdhItem.Chassis.Trim & " - " & objVDH.ChassisNo
                lblEngineNo.Text = objVdhItem.Engine.Trim & " - " & objVDH.EngineNo.Trim
                lblCBUReceiptDate.Text = IIf(Year(objVDH.ReceiptCBUDate) < 1970, " ", objVDH.ReceiptCBUDate.ToString("dd/MM/yyyy"))
                lblDeliveryDate.Text = IIf(Year(objVDH.DOPrintDate) < 1970, " ", objVDH.DOPrintDate.ToString("dd/MM/yyyy"))
                lblNIKNo.Text = IIf(objVDH.NIKNo.Trim.ToUpper = "NULL", "", objVDH.NIKNo.Trim)
                lblEndCust.Text = IIf(objVDH.EndCustomerName.Trim.ToUpper = "NULL", "", objVDH.EndCustomerName.ToString)
                lblAddress.Text = IIf(objVDH.EndCustomerAddress.Trim.ToUpper = "NULL", "", objVDH.EndCustomerAddress.ToString)
                LblFactureDate.Text = IIf(Year(objVDH.FactureOpenDate) < 1970, " ", objVDH.FactureOpenDate.ToString("dd/MM/yyyy"))
                arrDetail = objVDH.VDHServices
                dtgData.DataSource = arrDetail
                dtgData.DataBind()

            Catch ex As Exception
                objVDH = Nothing
            End Try
        End If
    End Sub
#End Region

   
End Class
