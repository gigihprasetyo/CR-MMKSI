Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports System.Collections.Generic
Imports System.Linq
Imports GlobalExtensions

Public Class FrmPrintTrReminder
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Image1 As System.Web.UI.WebControls.Image
    Protected WithEvents lblToday As System.Web.UI.WebControls.Label
    Protected WithEvents lblMonth As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblCityName As System.Web.UI.WebControls.Label
    Protected WithEvents dtgReminder As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents lblInfo1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblInfo2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblInfoManager As System.Web.UI.WebControls.Label
    Protected WithEvents lblInfo3 As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private REF_TYPE As String = "TR"
    Private Const REF_RINF1 As String = "RINF1"
    Private Const REF_RINF2 As String = "RINF2"
    Private Const REF_RINF3 As String = "RINF3"

    Private ReadOnly Property AreaID As String
        Get
            Return Request.QueryString("areaid")
        End Get
    End Property

    Private ReadOnly Property DealerCode As String
        Get
            Return Request.QueryString("dealerCode")
        End Get
    End Property

    Private ReadOnly Property Tahun As String
        Get
            Return Request.QueryString("tahun")
        End Get
    End Property

    Private ReadOnly Property ClassID As String
        Get
            Return Request.QueryString("kelas")
        End Get
    End Property

    Private ReadOnly Property Bulan As String
        Get
            Dim result As String = String.Empty
            Select Case Request.QueryString("bulan")
                Case "1"
                    result = "Januari"
                Case "2"
                    result = "Februari"
                Case "3"
                    result = "Maret"
                Case "4"
                    result = "April"
                Case "5"
                    result = "Mei"
                Case "6"
                    result = "Juni"
                Case "7"
                    result = "Juli"
                Case "8"
                    result = "Agustus"
                Case "9"
                    result = "September"
                Case "10"
                    result = "Oktober"
                Case "11"
                    result = "November"
                Case "12"
                    result = "Desember"
            End Select
            Return result
        End Get
    End Property

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitPrivilage()
        If Not IsPostBack Then
            InitiatePage()
            BindDtg()
        End If
        'Put user code to initialize the page here
    End Sub

    Private Sub InitiatePage()
        Dim sessHelper As SessionHelper = New SessionHelper
        Dim objDealer As Dealer = New DealerFacade(User).Retrieve(DealerCode)
        lblMonth.Text = Me.Bulan & " " & Me.Tahun

        lblDealerName.Text = objDealer.DealerName
        lblCityName.Text = objDealer.City.CityName

        Select Case AreaID
            Case "1"
                REF_TYPE = "TRSLS"
            Case "2"
                lblInfoManager.Text = "u.p: Service Manager"
                REF_TYPE = "TRASS"
            Case "3"
                REF_TYPE = "TRCS"
        End Select
        Dim nClassID As Integer = CInt(ClassID)
        Dim objRefFacade As ReferenceFacade = New ReferenceFacade(User)
        Dim objRef As Reference = objRefFacade.RetrieveActiveList(REF_TYPE, REF_RINF1)
        If Not nClassID.Equals(0) Then
            Dim dataKelas As TrClass = New TrClassFacade(User).Retrieve(nClassID)
            lblInfo1.Text = dataKelas.Description
        ElseIf Not IsNothing(objRef) Then
            lblInfo1.Text = objRef.Description
        End If

        objRef = objRefFacade.RetrieveActiveList(REF_TYPE, REF_RINF2)
        If Not IsNothing(objRef) Then
            lblInfo2.Text = objRef.Description
        End If
        objRef = objRefFacade.RetrieveActiveList(REF_TYPE, REF_RINF3)
        If Not IsNothing(objRef) Then
            lblInfo3.Text = objRef.Description
        End If

    End Sub

    Private Sub BindDtg()
        dtgReminder.DataSource = CType(Session.Item("PrintTr"), ArrayList)
        dtgReminder.DataBind()
        'Session.Remove("PrintTr")
    End Sub

    Private Sub InitPrivilage()

    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If AreaID.IsNullorEmpty Then
            Server.Transfer("../Training/FrmTrReminder.aspx")
        Else
            Server.Transfer("../Training/FrmTrReminderNew.aspx?area=" + AreaID)
        End If
    End Sub

    Private Sub dtgReminder_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgReminder.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            e.Item.Cells(0).Text = e.CreateNumberPage()
            Dim rowValue As TrClassRegistration = e.DataItem(Of TrClassRegistration)()
            Dim lblLocation As Label = e.FindLabel("lblLocation")
            Dim lblSalesmanCode As Label = e.FindLabel("lblSalesmanCode")
            Dim lblTraineeID As Label = e.FindLabel("lblTraineeID")

            If AreaID.Equals("2") Then
                lblSalesmanCode.Visible = False
                lblLocation.Text = rowValue.TrClass.TrMRTC.Name
            Else
                lblTraineeID.Visible = False
                lblSalesmanCode.Text = rowValue.TrTrainee.ListTrTraineeSalesmanHeader.FirstOrDefault(Function(x) _
                                            x.JobPositionAreaID = CInt(AreaID)).SalesmanHeader.SalesmanCode
                lblLocation.Text = rowValue.TrClass.Location
            End If

        End If
    End Sub
End Class
