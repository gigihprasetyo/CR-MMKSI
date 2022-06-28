
#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNET.BusinessFacade.AfterSales
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports OfficeOpenXml

#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
#End Region

Public Class FrmSalesChannel
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
    Protected WithEvents dtgSalesChannel As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtChannelType As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtChannelCode As System.Web.UI.WebControls.TextBox

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
    Private ListSalesChannel As ArrayList
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
        If txtChannelType.Text = "" Then
            MessageBox.Show("Channel Type belum diisi")
            Return
        End If
        If txtChannelCode.Text = "" Then
            MessageBox.Show("Channel Code belum diisi")
            Return
        End If
        txtChannelCode.ReadOnly = False
        txtDeskripsi.ReadOnly = False
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            InsertModel()
        Else
            UpdateModel()
            MessageBox.Show("Ubah Sukses")
        End If
        ClearData()
        dtgSalesChannel.CurrentPageIndex = 0
        BindDataGrid(dtgSalesChannel.CurrentPageIndex)
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        SearchSalesChannelByCriteria()
    End Sub

    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        txtChannelType.ReadOnly = False
        txtChannelCode.ReadOnly = False
        txtDeskripsi.ReadOnly = False
        ClearData()
    End Sub

    Private Sub dtgSalesChannel_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgSalesChannel.ItemCommand
        If e.CommandName = "Edit" Then
            txtChannelType.ReadOnly = False
            txtChannelCode.ReadOnly = False
            txtDeskripsi.ReadOnly = False
            ViewState.Add("vsProcess", "Edit")
            ViewModel(e.Item.Cells(0).Text, True)
            ''dtgSalesChannel.SelectedIndex = e.Item.ItemIndex
        ElseIf e.CommandName = "Delete" Then
            Try
                txtChannelType.ReadOnly = False
                txtChannelCode.ReadOnly = False
                txtDeskripsi.ReadOnly = False
                DeleteSalesChannel(e.Item.Cells(0).Text)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
        ElseIf e.CommandName = "Activate" Then
            Try
                Activate(e.Item.Cells(0).Text)
                txtChannelType.ReadOnly = False
                txtChannelCode.ReadOnly = False
                txtDeskripsi.ReadOnly = False
                ClearData()
            Catch ex As Exception
                MessageBox.Show("Activate error")
            End Try
        ElseIf e.CommandName = "Deactivate" Then
            Try
                DeActivate(e.Item.Cells(0).Text)

                txtChannelType.ReadOnly = False
                txtChannelCode.ReadOnly = False
                txtDeskripsi.ReadOnly = False
                ClearData()
            Catch ex As Exception
                MessageBox.Show("Activate error")
            End Try
        End If
    End Sub

    Private Sub dtgSalesChannel_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSalesChannel.ItemDataBound
        Dim RowValue As AssistSalesChannel = CType(e.Item.DataItem, AssistSalesChannel)
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
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgSalesChannel.CurrentPageIndex * dtgSalesChannel.PageSize)
        End If

    End Sub

    Private Sub dtgSalesChannel_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSalesChannel.SortCommand
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

        dtgSalesChannel.SelectedIndex = -1
        dtgSalesChannel.CurrentPageIndex = 0
        BindDataGrid(dtgSalesChannel.CurrentPageIndex)
        ClearData()
    End Sub


    Private Sub dtgSalesChannel_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSalesChannel.PageIndexChanged
        dtgSalesChannel.SelectedIndex = -1
        dtgSalesChannel.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgSalesChannel.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

#Region "Custom"

    Private Sub SetControlPrivilege()
        btnSimpan.Visible = m_bFormPrivilege
        btnBatal.Visible = m_bFormPrivilege
    End Sub

    Private Sub ActivateUserPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.AfterSales_SalesChannel_Edit_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.AfterSales_SalesChannel_View_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Master - Sales Channel")
        End If

    End Sub

    Private Sub InitiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "Description"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

    End Sub



    Private Sub ClearData()
        txtChannelCode.Text = String.Empty
        txtChannelType.Text = String.Empty
        txtDeskripsi.Text = String.Empty
        btnSimpan.Enabled = True
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totRow As Integer = 0
        If (indexPage >= 0) Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AssistSalesChannel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            If txtChannelType.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistSalesChannel), "SalesChannelType", MatchType.[Partial], txtChannelType.Text.Trim))
            End If
            If txtChannelCode.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistSalesChannel), "SalesChannelCode", MatchType.[Partial], txtChannelCode.Text.Trim))
            End If
            If txtDeskripsi.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistSalesChannel), "Description", MatchType.[Partial], txtDeskripsi.Text.Trim))
            End If

            ListSalesChannel = New AssistSalesChannelFacade(User).RetrieveActiveList(criterias, indexPage + 1, _
                    dtgSalesChannel.PageSize, totRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgSalesChannel.DataSource = ListSalesChannel
            dtgSalesChannel.VirtualItemCount = totRow
            dtgSalesChannel.DataBind()
        End If
    End Sub

    Private Sub Activate(ByVal nID As Integer)
        Dim objSalesChannel As AssistSalesChannel = New AssistSalesChannelFacade(User).Retrieve(nID)
        Dim facade As AssistSalesChannelFacade = New AssistSalesChannelFacade(User)
        objSalesChannel.Status = 1
        facade.Update(objSalesChannel)
        dtgSalesChannel.CurrentPageIndex = 0
        BindDataGrid(dtgSalesChannel.CurrentPageIndex)
    End Sub

    Private Sub DeActivate(ByVal nID As Integer)
        Dim objSalesChannel As AssistSalesChannel = New AssistSalesChannelFacade(User).Retrieve(nID)
        Dim facade As AssistSalesChannelFacade = New AssistSalesChannelFacade(User)
        objSalesChannel.Status = 0
        facade.Update(objSalesChannel)
        dtgSalesChannel.CurrentPageIndex = 0
        BindDataGrid(dtgSalesChannel.CurrentPageIndex)
    End Sub

    Private Function InsertModel() As Integer
        Dim objSalesChannelFacade As AssistSalesChannelFacade = New AssistSalesChannelFacade(User)
        Dim objSalesChannel As AssistSalesChannel = New AssistSalesChannel
        Dim nResult As Integer

        If objSalesChannelFacade.ValidateCode(txtChannelCode.Text) = 0 Then
            objSalesChannel.SalesChannelType = txtChannelType.Text
            objSalesChannel.SalesChannelCode = txtChannelCode.Text
            objSalesChannel.Description = txtDeskripsi.Text
            objSalesChannel.Status = 1
            nResult = New AssistSalesChannelFacade(User).Insert(objSalesChannel)
            If nResult = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
            End If
        Else
            MessageBox.Show(SR.DataIsExist("Sales Channel"))
        End If
        Return nResult
    End Function

    Private Sub UpdateModel()
        Dim objSalesChannel As AssistSalesChannel = CType(Session.Item("vsSalesChannel"), AssistSalesChannel)
        objSalesChannel.SalesChannelType = txtChannelType.Text
        objSalesChannel.SalesChannelCode = txtChannelCode.Text
        objSalesChannel.Description = txtDeskripsi.Text

        Dim nResult = New AssistSalesChannelFacade(User).Update(objSalesChannel)
    End Sub

    Private Sub DeleteSalesChannel(ByVal nID As Integer)
        Dim objSalesChannel As AssistSalesChannel = New AssistSalesChannelFacade(User).Retrieve(nID)

        objSalesChannel.RowStatus = CType(DBRowStatus.Deleted, Short)
        Dim facade As AssistSalesChannelFacade = New AssistSalesChannelFacade(User)
        Dim nResult As Integer = facade.Update(objSalesChannel)

        If nResult <> -1 Then
            ClearData()
            dtgSalesChannel.CurrentPageIndex = 0
            BindDataGrid(dtgSalesChannel.CurrentPageIndex)
        Else
            MessageBox.Show(SR.DeleteFail)
        End If
    End Sub

    Private Sub ViewModel(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objSalesChannel As AssistSalesChannel = New AssistSalesChannelFacade(User).Retrieve(nID)
        'Todo session
        Session.Add("vsSalesChannel", objSalesChannel)
        If IsNothing(objSalesChannel) Then
            txtDeskripsi.Text = ""
            txtChannelType.Text = ""
            txtChannelCode.Text = ""
        Else
            txtChannelType.Text = objSalesChannel.SalesChannelType
            txtChannelCode.Text = objSalesChannel.SalesChannelCode
            txtDeskripsi.Text = objSalesChannel.Description
        End If

        Me.btnSimpan.Enabled = EditStatus
    End Sub

    Private Sub SearchSalesChannelByCriteria()
        dtgSalesChannel.CurrentPageIndex = 0
        BindDataGrid(dtgSalesChannel.CurrentPageIndex)
    End Sub
#End Region

End Class
