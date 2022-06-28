
#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNET.BusinessFacade.AfterSales
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
#End Region

Public Class FrmServicePlace
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents rfvKode As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtDeskripsi As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvDeskripsi As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents dtgServicePlace As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtServicePlaceCode As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Declaration"
    Private m_bFormPrivilege As Boolean = False
    Private ListServicePlace As ArrayList
#End Region

#Region "Event"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        If txtServicePlaceCode.Text = "" Then
            MessageBox.Show("Channel Code belum diisi")
            Return
        End If
        txtServicePlaceCode.ReadOnly = False
        txtDeskripsi.ReadOnly = False
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            InsertModel()
        Else
            UpdateModel()
            MessageBox.Show("Ubah Sukses")
        End If
        ClearData()
        dtgServicePlace.CurrentPageIndex = 0
        BindDataGrid(dtgServicePlace.CurrentPageIndex)
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        SearchServicePlaceByCriteria()
    End Sub

    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        txtServicePlaceCode.ReadOnly = False
        txtDeskripsi.ReadOnly = False
        ClearData()
    End Sub

    Private Sub dtgServicePlace_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgServicePlace.ItemCommand
        If e.CommandName = "Edit" Then
            txtServicePlaceCode.ReadOnly = False
            txtDeskripsi.ReadOnly = False
            ViewState.Add("vsProcess", "Edit")
            ViewModel(e.Item.Cells(0).Text, True)
            ''dtgServicePlace.SelectedIndex = e.Item.ItemIndex
        ElseIf e.CommandName = "Delete" Then
            Try
                txtServicePlaceCode.ReadOnly = False
                txtDeskripsi.ReadOnly = False
                DeleteServicePlace(e.Item.Cells(0).Text)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
        ElseIf e.CommandName = "Activate" Then
            Try
                Activate(e.Item.Cells(0).Text)
                txtServicePlaceCode.ReadOnly = False
                txtDeskripsi.ReadOnly = False
                ClearData()
            Catch ex As Exception
                MessageBox.Show("Activate error")
            End Try
        ElseIf e.CommandName = "Deactivate" Then
            Try
                DeActivate(e.Item.Cells(0).Text)

                txtServicePlaceCode.ReadOnly = False
                txtDeskripsi.ReadOnly = False
                ClearData()
            Catch ex As Exception
                MessageBox.Show("Activate error")
            End Try
        End If
    End Sub

    Private Sub dtgServicePlace_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgServicePlace.ItemDataBound
        Dim RowValue As AssistServicePlace = CType(e.Item.DataItem, AssistServicePlace)
        Dim actLb As LinkButton
        Dim nactLb As LinkButton
        If Not e.Item.FindControl("btnHapus") Is Nothing Then
            CType(e.Item.FindControl("btnHapus"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
        End If
        If Not e.Item.FindControl("linkButonActive") Is Nothing Then
            CType(e.Item.FindControl("linkButonActive"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan diAktifkan?');")
            actLb = CType(e.Item.FindControl("linkButonActive"), LinkButton)
        End If

        If Not e.Item.FindControl("LinkButtonNonActive") Is Nothing Then
            CType(e.Item.FindControl("LinkButtonNonActive"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan diNonAktifkan?');")
            nactLb = CType(e.Item.FindControl("LinkButtonNonActive"), LinkButton)

            actLb.Visible = False
            nactLb.Visible = False

            If m_bFormPrivilege = True Then
                If RowValue.Status = 0 Then
                    actLb.Visible = True
                Else
                    nactLb.Visible = True
                End If
            End If
        End If

        If Not e.Item.FindControl("btnUbah") Is Nothing Then
            CType(e.Item.FindControl("btnUbah"), LinkButton).Visible = m_bFormPrivilege
        End If

        If Not e.Item.FindControl("btnHapus") Is Nothing Then
            CType(e.Item.FindControl("btnHapus"), LinkButton).Visible = m_bFormPrivilege
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgServicePlace.CurrentPageIndex * dtgServicePlace.PageSize)
        End If

    End Sub

    Private Sub dtgServicePlace_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgServicePlace.SortCommand
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

        dtgServicePlace.SelectedIndex = -1
        dtgServicePlace.CurrentPageIndex = 0
        BindDataGrid(dtgServicePlace.CurrentPageIndex)
        ClearData()
    End Sub


    Private Sub dtgServicePlace_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgServicePlace.PageIndexChanged
        dtgServicePlace.SelectedIndex = -1
        dtgServicePlace.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgServicePlace.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

#Region "Custom"

    Private Sub SetControlPrivilege()
        btnSimpan.Visible = m_bFormPrivilege
        btnBatal.Visible = m_bFormPrivilege
    End Sub

    Private Sub ActivateUserPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.AfterSales_ServicePlace_Edit_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.AfterSales_ServicePlace_View_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Master - Service Place")
        End If

    End Sub

    Private Sub InitiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "Description"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

    End Sub



    Private Sub ClearData()
        txtServicePlaceCode.Text = String.Empty
        txtDeskripsi.Text = String.Empty
        btnSimpan.Enabled = True
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totRow As Integer = 0
        If (indexPage >= 0) Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AssistServicePlace), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            If txtServicePlaceCode.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServicePlace), "ServicePlaceCode", MatchType.[Partial], txtServicePlaceCode.Text.Trim))
            End If
            If txtDeskripsi.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServicePlace), "Description", MatchType.[Partial], txtDeskripsi.Text.Trim))
            End If

            ListServicePlace = New AssistServicePlaceFacade(User).RetrieveActiveList(criterias, indexPage + 1, _
                    dtgServicePlace.PageSize, totRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgServicePlace.DataSource = ListServicePlace
            dtgServicePlace.VirtualItemCount = totRow
            dtgServicePlace.DataBind()
        End If
    End Sub

    Private Sub Activate(ByVal nID As Integer)
        Dim objServicePlace As AssistServicePlace = New AssistServicePlaceFacade(User).Retrieve(nID)
        Dim facade As AssistServicePlaceFacade = New AssistServicePlaceFacade(User)
        objServicePlace.Status = 1
        facade.Update(objServicePlace)
        dtgServicePlace.CurrentPageIndex = 0
        BindDataGrid(dtgServicePlace.CurrentPageIndex)
    End Sub

    Private Sub DeActivate(ByVal nID As Integer)
        Dim objServicePlace As AssistServicePlace = New AssistServicePlaceFacade(User).Retrieve(nID)
        Dim facade As AssistServicePlaceFacade = New AssistServicePlaceFacade(User)
        objServicePlace.Status = 0
        facade.Update(objServicePlace)
        dtgServicePlace.CurrentPageIndex = 0
        BindDataGrid(dtgServicePlace.CurrentPageIndex)
    End Sub

    Private Function InsertModel() As Integer
        Dim objServicePlaceFacade As AssistServicePlaceFacade = New AssistServicePlaceFacade(User)
        Dim objServicePlace As AssistServicePlace = New AssistServicePlace
        Dim nResult As Integer

        If objServicePlaceFacade.ValidateCode(txtServicePlaceCode.Text) = 0 Then
            objServicePlace.ServicePlaceCode = txtServicePlaceCode.Text
            objServicePlace.Description = txtDeskripsi.Text
            objServicePlace.Status = 1
            nResult = New AssistServicePlaceFacade(User).Insert(objServicePlace)
            If nResult = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
            End If
        Else
            MessageBox.Show(SR.DataIsExist("Service Place"))
        End If
        Return nResult
    End Function

    Private Sub UpdateModel()
        Dim objServicePlace As AssistServicePlace = CType(Session.Item("vsServicePlace"), AssistServicePlace)
        objServicePlace.ServicePlaceCode = txtServicePlaceCode.Text
        objServicePlace.Description = txtDeskripsi.Text

        Dim nResult = New AssistServicePlaceFacade(User).Update(objServicePlace)
    End Sub

    Private Sub DeleteServicePlace(ByVal nID As Integer)
        Dim objServicePlace As AssistServicePlace = New AssistServicePlaceFacade(User).Retrieve(nID)

        objServicePlace.RowStatus = CType(DBRowStatus.Deleted, Short)
        Dim facade As AssistServicePlaceFacade = New AssistServicePlaceFacade(User)
        Dim nResult As Integer = facade.Update(objServicePlace)

        If nResult <> -1 Then
            ClearData()
            dtgServicePlace.CurrentPageIndex = 0
            BindDataGrid(dtgServicePlace.CurrentPageIndex)
        Else
            MessageBox.Show(SR.DeleteFail)
        End If
    End Sub

    Private Sub ViewModel(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objServicePlace As AssistServicePlace = New AssistServicePlaceFacade(User).Retrieve(nID)
        'Todo session
        Session.Add("vsServicePlace", objServicePlace)
        If IsNothing(objServicePlace) Then
            txtDeskripsi.Text = ""
            txtServicePlaceCode.Text = ""
        Else
            txtServicePlaceCode.Text = objServicePlace.ServicePlaceCode
            txtDeskripsi.Text = objServicePlace.Description
        End If

        Me.btnSimpan.Enabled = EditStatus
    End Sub

    Private Sub SearchServicePlaceByCriteria()
        dtgServicePlace.CurrentPageIndex = 0
        BindDataGrid(dtgServicePlace.CurrentPageIndex)
    End Sub
#End Region

End Class
