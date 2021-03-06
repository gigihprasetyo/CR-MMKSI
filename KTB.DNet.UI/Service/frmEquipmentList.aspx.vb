#Region ".NET Base Class Namespace Imports"
Imports System.IO
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
#End Region

Public Class frmEquipmentList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents txtEquipmentNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents dgEquipmentList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents txtSpesifikasi As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    'Private EquipmentlistArrayList As ArrayList
    Private sessionHelper As New SessionHelper
#End Region

#Region "Custom Method"

    Private Sub SetSessionCriteria()
        Dim objSSPO As ArrayList = New ArrayList
        objSSPO.Add(txtEquipmentNumber.Text)
        objSSPO.Add(txtDescription.Text)
        objSSPO.Add(txtSpesifikasi.Text)
        objSSPO.Add(ddlStatus.SelectedIndex)
        objSSPO.Add(dgEquipmentList.CurrentPageIndex)
        objSSPO.Add(CType(ViewState("CurrentSortColumn"), String))
        objSSPO.Add(CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        sessionHelper.SetSession("SESSIONEQLIST", objSSPO)
    End Sub

    Private Sub GetSessionCriteria()
        Dim objSSPO As ArrayList = sessionHelper.GetSession("SESSIONEQLIST")
        If Not objSSPO Is Nothing Then
            txtEquipmentNumber.Text = objSSPO.Item(0)
            txtDescription.Text = objSSPO.Item(1)
            txtSpesifikasi.Text = objSSPO.Item(2)
            ddlStatus.SelectedIndex = objSSPO.Item(3)
            dgEquipmentList.CurrentPageIndex = objSSPO.Item(4)
            ViewState("CurrentSortColumn") = objSSPO.Item(5)
            ViewState("CurrentSortDirect") = objSSPO.Item(6)
        End If
    End Sub


    Private Sub CreateCriteria(ByVal criterias As CriteriaComposite)
        If txtEquipmentNumber.Text <> "" Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentMaster), "EquipmentNumber", MatchType.StartsWith, txtEquipmentNumber.Text))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentMaster), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        If txtDescription.Text <> "" Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentMaster), "Description", MatchType.[Partial], txtDescription.Text))
        If txtSpesifikasi.Text <> "" Then
            Dim strSpec() As String = txtSpesifikasi.Text.Split(";")
            For i As Integer = 0 To strSpec.Length - 1
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentMaster), "Specification", MatchType.[Partial], strSpec(i)))
            Next
        End If
        sessionHelper.SetSession("CRITERIAS", criterias)
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        'EquipmentlistArrayList = New EquipmentMasterFacade(User).Retrieve(criterias)
        If (indexPage >= 0) And (indexPage <= dgEquipmentList.PageCount) Then
            dgEquipmentList.DataSource = New EquipmentMasterFacade(User).RetrieveActiveList(CType(sessionHelper.GetSession("CRITERIAS"), CriteriaComposite), indexPage + 1, dgEquipmentList.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dgEquipmentList.VirtualItemCount = totalRow
            If totalRow > 0 Then
                btnDownload.Enabled = True
            Else
                btnDownload.Enabled = False
            End If
            dgEquipmentList.DataBind()
        End If
    End Sub

    Private Function RetriveDownload() As ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.EquipmentMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtEquipmentNumber.Text <> "" Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentMaster), "EquipmentNumber", MatchType.StartsWith, txtEquipmentNumber.Text))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentMaster), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        If txtDescription.Text <> "" Then criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentMaster), "Description", MatchType.[Partial], txtDescription.Text))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EquipmentMaster), "Kind", MatchType.InSet, "(" & CType(EquipmentKind.EquipmentKindEnum.Perbaikan, Integer) & "," & CType(EquipmentKind.EquipmentKindEnum.Pembelian, Integer) & ")"))

        Dim EquipmentlistArrayList As ArrayList = New EquipmentMasterFacade(User).Retrieve(criterias)
        Return EquipmentlistArrayList
    End Function

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "EquipmentNumber"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        'btnDownload.Visible = False
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            GetSessionCriteria()
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.EquipmentMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            CreateCriteria(criterias)
            BindDataGrid(dgEquipmentList.CurrentPageIndex)
        End If
    End Sub

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.EquipmentMasterListView_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Daftar Equipment")
        End If
        btnDownload.Visible = SecurityProvider.Authorize(Context.User, SR.EquipmentMasterListDownload_Privilege)
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.EquipmentMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        CreateCriteria(criterias)
        dgEquipmentList.CurrentPageIndex = 0
        BindDataGrid(dgEquipmentList.CurrentPageIndex)
        'Bind()
    End Sub

    Private Sub dgEquipmentList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgEquipmentList.SortCommand
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

        dgEquipmentList.SelectedIndex = -1
        dgEquipmentList.CurrentPageIndex = 0
        BindDataGrid(dgEquipmentList.CurrentPageIndex)

    End Sub

    Private Sub dgEquipmentList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgEquipmentList.PageIndexChanged
        dgEquipmentList.SelectedIndex = -1
        dgEquipmentList.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgEquipmentList.CurrentPageIndex)
    End Sub

    'Used for Icon Priviledge
    Private ViewPrivilege As Boolean
    Private EditPrivilege As Boolean
    Private Sub dgEquipmentList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgEquipmentList.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgEquipmentList.CurrentPageIndex * dgEquipmentList.PageSize)
            If e.Item.ItemIndex = 0 Then
                ViewPrivilege = SecurityProvider.Authorize(Context.User, SR.EquipmentMasterViewListDetail_Privilege)
                EditPrivilege = SecurityProvider.Authorize(Context.User, SR.EquipmentMasterUpdateListDetail_Privilege)
            End If
            Dim lbtnView As LinkButton = e.Item.FindControl("LinkButton2")
            lbtnView.Visible = ViewPrivilege
            Dim lbtnEdit As LinkButton = e.Item.FindControl("LinkButton1")
            lbtnEdit.Visible = EditPrivilege

            Dim RowValue As EquipmentMaster = CType(e.Item.DataItem, EquipmentMaster)

            Dim lblspek As LinkButton = CType(e.Item.FindControl("lbtnSpek"), LinkButton)
            If RowValue.Specification <> Nothing Then
                lblspek.Visible = True
            Else
                lblspek.Visible = False
                e.Item.Cells(6).Text = ""
            End If

            Dim lbtnPhoto As LinkButton = CType(e.Item.FindControl("lbtnPhoto"), LinkButton)
            If RowValue.PhotoPath <> Nothing Then
                lbtnPhoto.Visible = True
            Else
                lbtnPhoto.Visible = False
                e.Item.Cells(7).Text = ""
            End If

            Dim temp As HeaderBOM = New EquipmentMasterFacade(User).CekHeaderBOM(RowValue.ID)
            Dim lbtnHeader As LinkButton = CType(e.Item.FindControl("lbtnHeader"), LinkButton)
            If (temp Is Nothing) Then
                lbtnHeader.Visible = False
                e.Item.Cells(4).Text = FormatNumber(RowValue.Price, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            Else
                lbtnHeader.Visible = True
                e.Item.Cells(4).Text = FormatNumber(temp.TotalHarga, 0, TriState.UseDefault, TriState.UseDefault, TriState.True)
            End If
        End If
    End Sub

    Private Sub dgEquipmentList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgEquipmentList.ItemCommand
        If e.Item.ItemIndex <> -1 Then
            If e.CommandName = "edit" Then
                sessionHelper.SetSession("PrevPage", Request.Url.ToString())
                SetSessionCriteria()
                Response.Redirect("../Service/frmEquipmentListDetail.aspx?Eq=" & e.Item.Cells(0).Text & "&Status=" & e.CommandName)
            Else
                sessionHelper.SetSession("PrevPage", Request.Url.ToString())
                SetSessionCriteria()
                Response.Redirect("../Service/frmEquipmentListDetail.aspx?Eq=" & e.Item.Cells(0).Text & "&Status=" & e.CommandName)
            End If
        End If

    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim EquipmentlistArrayList As ArrayList = RetriveDownload()
        Dim _fileHelper As New FileHelper
        Dim fileInfo1 As New FileInfo(Server.MapPath(""))
        Dim _fileNameInfo As FileInfo = _fileHelper.TransferEquipmenttoText(EquipmentlistArrayList, fileInfo1)
        Try
            Response.Redirect("../Downloadlocal.aspx?file=" & KTB.DNet.Lib.WebConfig.GetValue("EqDestFileDirectory").ToString & "\" & _fileNameInfo.Name)
        Catch ex As Exception
            MessageBox.Show(SR.DownloadFail(_fileNameInfo.Name))
        End Try
    End Sub

#End Region

End Class