Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security



Public Class FrmSalesmanTrainingList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents txtSalesmanCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnClose As System.Web.UI.WebControls.Button
    Protected WithEvents dgSalesmanTraining As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "PrivateVariables"
    Private _SalesmanTrainingParticipantFacade As New SalesmanTrainingParticipantFacade(User)
    Private _create As Boolean
    Private _edit As Boolean
    Private _view As Boolean
    Private _delete As Boolean
    Private sessHelper As New SessionHelper

#End Region

#Region "PrivateCustomMethods"

    

   
#End Region

#Region "EventHandlers"



#End Region


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CheckPrivilege()

        If Not IsPostBack Then
            Initialize()
            CheckQueryStr()
            'txtSalesmanCode.Text = "BAS-001"
            BindDataGrid(0)
        End If
    End Sub


#Region "Need To Add"

    Private Sub CheckQueryStr()
        If (Request.QueryString("SalesmanCode") <> "") Then
            txtSalesmanCode.Text = Request.QueryString("SalesmanCode")
        End If
        If (Request.QueryString("SalesmanName") <> "") Then
            txtName.Text = Request.QueryString("SalesmanName")
        End If
    End Sub

    ' penambahan untuk initialize data
    Private Sub ClearData()
        txtSalesmanCode.Text = String.Empty
        txtName.Text = String.Empty
    End Sub

    Private Sub Initialize()
        ClearData()
    End Sub


    ' ini perlu set security
    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiLihat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=UMUM - Daftar SPL")
        End If

        _create = SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiBuat_Privilege)
        _edit = SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiUbah_Privilege)
        _view = SecurityProvider.Authorize(context.User, SR.ENHPKDaftarAplikasiDetail_Privilege)

        'lbtnNew.Visible = _create
        'btnSearch.Visible = _view

    End Sub

    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        If txtSalesmanCode.Text <> String.Empty Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanTrainingParticipant), "SalesmanHeader.SalesmanCode", MatchType.Exact, txtSalesmanCode.Text))

            arrList = _SalesmanTrainingParticipantFacade.RetrieveByCriteria(criterias, idxPage + 1, dgSalesmanTraining.PageSize, totalRow, _
            CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

            dgSalesmanTraining.DataSource = arrList
            dgSalesmanTraining.VirtualItemCount = totalRow
            dgSalesmanTraining.DataBind()

        End If


    End Sub
#End Region


    Private Sub dgSalesmanTraining_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSalesmanTraining.PageIndexChanged
        dgSalesmanTraining.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgSalesmanTraining.CurrentPageIndex)
    End Sub

    Private Sub dgSalesmanTraining_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSalesmanTraining.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objSalesmanTrainingParticipant As SalesmanTrainingParticipant = e.Item.DataItem

            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgSalesmanTraining.CurrentPageIndex * dgSalesmanTraining.PageSize)

            ' menentukan value yg seharusnya
            Dim lblTrainingCodeNew As Label = CType(e.Item.FindControl("lblTrainingCode"), Label)
            lblTrainingCodeNew.Text = objSalesmanTrainingParticipant.SalesmanMasterTraining.TrainingCode

            Dim lblStartingDateNew As Label = CType(e.Item.FindControl("lblStartingDate"), Label)
            lblStartingDateNew.Text = CType(objSalesmanTrainingParticipant.SalesmanMasterTraining.StartingDate, String)

            Dim lblEndDateNew As Label = CType(e.Item.FindControl("lblEndDate"), Label)
            lblEndDateNew.Text = CType(objSalesmanTrainingParticipant.SalesmanMasterTraining.EndDate, String)

        End If
    End Sub

    Private Sub dgSalesmanTraining_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSalesmanTraining.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If
        dgSalesmanTraining.SelectedIndex = -1
        dgSalesmanTraining.CurrentPageIndex = 0
        BindDataGrid(dgSalesmanTraining.CurrentPageIndex)
    End Sub

    Private Sub dgSalesmanTraining_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSalesmanTraining.ItemCommand

    End Sub



End Class
