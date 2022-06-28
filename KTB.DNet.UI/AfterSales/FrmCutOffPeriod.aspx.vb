
#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNET.BusinessFacade.AfterSales
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.General

#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
#End Region

Public Class FrmCutOffPeriod
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents rfvKode As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents rfvDeskripsi As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents btnActivateAll As System.Web.UI.WebControls.Button
    Protected WithEvents btnInactiveAll As System.Web.UI.WebControls.Button
    Protected WithEvents dtgCutOffPeriod As System.Web.UI.WebControls.DataGrid

    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label

    Protected WithEvents ddlMonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents hdnValActive As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnValInActive As System.Web.UI.HtmlControls.HtmlInputHidden

    'Protected WithEvents chkType As System.Web.UI.WebControls.CheckBox
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
    Private ListCutOffPeriod As ArrayList
#End Region

#Region "Event"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
        Else
            If Request.Form("hdnValActive") = "1" Then
                btnActivateAll_Click(Nothing, Nothing)
                hdnValActive.Value = "-1"
            ElseIf Request.Form("hdnValActive") = "0" Then
                hdnValActive.Value = "-1"
            End If

            If Request.Form("hdnValInActive") = "1" Then
                btnInactiveAll_Click(Nothing, Nothing)
                hdnValInActive.Value = "-1"
            ElseIf Request.Form("hdnValInActive") = "0" Then
                hdnValInActive.Value = "-1"
            End If

        End If

        txtDealerCode.Visible = True
        lblPopUpDealer.Visible = True
        lblDealerCode.Visible = False
        lblPopUpDealer.Attributes("onClick") = "ShowPPDealerSelection()"
        btnSimpan.Visible = False 'tidak jadi ada simpan
        'chkType.Attributes.Add("onclick", "ChangeType()")
    End Sub

    Private Sub BindMonth()
        '
        Dim al As ArrayList = enumMonthGet.RetriveMonth
        ddlMonth.DataSource = enumMonthGet.RetriveMonth()
        ddlMonth.DataValueField = "ValStatus"
        ddlMonth.DataTextField = "NameStatus"
        ddlMonth.DataBind()
        ddlMonth.SelectedValue = DateTime.Now.Month

    End Sub

    Private Sub BindYear()

        Dim a As Integer
        Dim now As DateTime = DateTime.Now
        For a = -1 To 1
            ddlYear.Items.Insert(0, New ListItem(now.AddYears(a).Year, now.AddYears(a).Year))
        Next
        ddlYear.SelectedValue = now.Year
    End Sub
    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        'If (chkType.Checked = False) Then
        If txtDealerCode.Text = "" Then
            MessageBox.Show("Kode Dealer belum diisi")
            Return
        End If
        'End If

        txtDealerCode.ReadOnly = False
        'If (chkType.Checked = False) Then
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            InsertModel()
        End If
        'Else
        'InsertAllDealer()
        'End If
        ClearData()
        dtgCutOffPeriod.CurrentPageIndex = 0
        BindDataGrid(dtgCutOffPeriod.CurrentPageIndex)
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        SearchCutOffPeriodByCriteria()
        If txtDealerCode.Text = "" Then
            btnActivateAll.Visible = True
            btnInactiveAll.Visible = True
        Else
            btnActivateAll.Visible = False
            btnInactiveAll.Visible = False
        End If
    End Sub


    Private Sub dtgCutOffPeriod_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgCutOffPeriod.ItemCommand
        If e.CommandName = "Delete" Then
            Try
                txtDealerCode.ReadOnly = False
                DeleteCutOffPeriod(e.Item.Cells(0).Text)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
        ElseIf e.CommandName = "Activate" Then
            Try
                Activate(e.Item.Cells(0).Text)
                txtDealerCode.ReadOnly = False
                ClearData()
            Catch ex As Exception
                MessageBox.Show("Activate error")
            End Try
        ElseIf e.CommandName = "Deactivate" Then
            Try
                DeActivate(e.Item.Cells(0).Text)
                txtDealerCode.ReadOnly = False
                ClearData()
            Catch ex As Exception
                MessageBox.Show("Activate error")
            End Try
        End If
    End Sub

    Private Sub dtgCutOffPeriod_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgCutOffPeriod.ItemDataBound
        Dim RowValue As AssistCutOffPeriod = CType(e.Item.DataItem, AssistCutOffPeriod)
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

        If Not e.Item.FindControl("btnHapus") Is Nothing Then
            CType(e.Item.FindControl("btnHapus"), LinkButton).Visible = m_bFormPrivilege
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgCutOffPeriod.CurrentPageIndex * dtgCutOffPeriod.PageSize)
        End If

    End Sub

    Private Sub dtgCutOffPeriod_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgCutOffPeriod.SortCommand
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

        dtgCutOffPeriod.SelectedIndex = -1
        dtgCutOffPeriod.CurrentPageIndex = 0
        BindDataGrid(dtgCutOffPeriod.CurrentPageIndex)
        ClearData()
    End Sub


    Private Sub dtgCutOffPeriod_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgCutOffPeriod.PageIndexChanged
        dtgCutOffPeriod.SelectedIndex = -1
        dtgCutOffPeriod.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgCutOffPeriod.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

#Region "Custom"

    Private Sub SetControlPrivilege()
        btnSimpan.Visible = m_bFormPrivilege
        btnActivateAll.Visible = m_bFormPrivilege
        btnInactiveAll.Visible = m_bFormPrivilege
    End Sub

    Private Sub ActivateUserPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.AfterSales_CutOffPeriod_Edit_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.AfterSales_CutOffPeriod_View_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Master - Cut Off Period")
        End If

    End Sub

    Private Sub InitiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
        BindMonth()
        BindYear()
        btnActivateAll.Visible = False
        btnInactiveAll.Visible = False
    End Sub

    Private Sub ClearData()
        txtDealerCode.Text = String.Empty
        btnSimpan.Enabled = True
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totRow As Integer = 0
        If (indexPage >= 0) Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AssistCutOffPeriod), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            'If chkType.Checked = False Then
            If txtDealerCode.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistCutOffPeriod), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text.Trim))
            End If
            'End If

            If ddlMonth.SelectedValue > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistCutOffPeriod), "Month", MatchType.Exact, ddlMonth.SelectedValue.Trim))
            End If

            If ddlYear.SelectedValue > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistCutOffPeriod), "Year", MatchType.Exact, ddlYear.SelectedValue.Trim))
            End If

            ListCutOffPeriod = New AssistCutOffPeriodFacade(User).RetrieveActiveList(criterias, indexPage + 1, _
                    dtgCutOffPeriod.PageSize, totRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgCutOffPeriod.DataSource = ListCutOffPeriod
            dtgCutOffPeriod.VirtualItemCount = totRow
            dtgCutOffPeriod.DataBind()
        End If
    End Sub

    Private Sub Activate(ByVal nID As Integer)
        Dim objCutOffPeriod As AssistCutOffPeriod = New AssistCutOffPeriodFacade(User).Retrieve(nID)
        Dim facade As AssistCutOffPeriodFacade = New AssistCutOffPeriodFacade(User)
        objCutOffPeriod.Status = 1
        objCutOffPeriod.CutOffDate = DateTime.Now
        facade.Update(objCutOffPeriod)
        dtgCutOffPeriod.CurrentPageIndex = 0
        BindDataGrid(dtgCutOffPeriod.CurrentPageIndex)
    End Sub

    Private Sub DeActivate(ByVal nID As Integer)
        Dim objCutOffPeriod As AssistCutOffPeriod = New AssistCutOffPeriodFacade(User).Retrieve(nID)
        Dim facade As AssistCutOffPeriodFacade = New AssistCutOffPeriodFacade(User)
        objCutOffPeriod.Status = 0
        facade.Update(objCutOffPeriod)
        dtgCutOffPeriod.CurrentPageIndex = 0
        BindDataGrid(dtgCutOffPeriod.CurrentPageIndex)
    End Sub

    Private Sub InsertAllDealer()
        Dim result As Boolean = New AssistCutOffPeriodFacade(User).CuttOffAllDealer(ddlMonth.SelectedValue, ddlYear.SelectedValue)
        If result = True Then
            MessageBox.Show(SR.SaveSuccess)
        Else
            MessageBox.Show(SR.SaveFail)
        End If
    End Sub

    Private Function InsertModel() As Integer
        Dim objCutOffPeriodFacade As AssistCutOffPeriodFacade = New AssistCutOffPeriodFacade(User)
        Dim objCutOffPeriod As AssistCutOffPeriod = New AssistCutOffPeriod
        Dim nResult As Integer
        Dim dealer As Dealer = New DealerFacade(User).GetDealer(txtDealerCode.Text)
        If objCutOffPeriodFacade.ValidateCode(dealer.ID, ddlMonth.SelectedValue, ddlYear.SelectedValue) = 0 Then
            objCutOffPeriod.Dealer = New DealerFacade(User).GetDealer(txtDealerCode.Text)
            objCutOffPeriod.Month = ddlMonth.SelectedValue
            objCutOffPeriod.Year = ddlYear.SelectedValue
            objCutOffPeriod.Status = 1
            objCutOffPeriod.CutOffDate = DateTime.Now
            nResult = New AssistCutOffPeriodFacade(User).Insert(objCutOffPeriod)
            If nResult = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
            End If
        Else
            MessageBox.Show(SR.DataIsExist("Cut Off Period Dealer " & txtDealerCode.Text))
        End If
        Return nResult
    End Function

    Private Sub btnActivateAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnActivateAll.Click
        If (hdnValActive.Value = "-1") Then
            MessageBox.Confirm("Anda yakin ingin mengaktifkan semua cut off?", "hdnValActive")
            Return
        End If

        Try
            Dim result As Boolean = New AssistCutOffPeriodFacade(User).ChangeStatusClosing(ddlMonth.SelectedValue, ddlYear.SelectedValue, 1)
            If result = True Then
                MessageBox.Show(SR.SaveSuccess)
            Else
                MessageBox.Show(SR.SaveFail)
            End If
            dtgCutOffPeriod.CurrentPageIndex = 0
            BindDataGrid(dtgCutOffPeriod.CurrentPageIndex)
        Catch ex As Exception
            MessageBox.Show(SR.SaveFail & ". Error : " & ex.Message)
        End Try
    End Sub

    Private Sub btnInactiveAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInactiveAll.Click
        If (hdnValInActive.Value = "-1") Then
            MessageBox.Confirm("Anda yakin ingin Non aktifkan semua cut off?", "hdnValInActive")
            Return
        End If

        Try
            Dim result As Boolean = New AssistCutOffPeriodFacade(User).ChangeStatusClosing(ddlMonth.SelectedValue, ddlYear.SelectedValue, 0)
            If result = True Then
                MessageBox.Show(SR.SaveSuccess)
            Else
                MessageBox.Show(SR.SaveFail)
            End If
            dtgCutOffPeriod.CurrentPageIndex = 0
            BindDataGrid(dtgCutOffPeriod.CurrentPageIndex)
        Catch ex As Exception
            MessageBox.Show(SR.SaveFail & ". Error : " & ex.Message)
        End Try
    End Sub


    Private Sub DeleteCutOffPeriod(ByVal nID As Integer)
        Dim objCutOffPeriod As AssistCutOffPeriod = New AssistCutOffPeriodFacade(User).Retrieve(nID)

        objCutOffPeriod.RowStatus = CType(DBRowStatus.Deleted, Short)
        Dim facade As AssistCutOffPeriodFacade = New AssistCutOffPeriodFacade(User)
        Dim nResult As Integer = facade.Update(objCutOffPeriod)

        If nResult <> -1 Then
            ClearData()
            dtgCutOffPeriod.CurrentPageIndex = 0
            BindDataGrid(dtgCutOffPeriod.CurrentPageIndex)
        Else
            MessageBox.Show(SR.DeleteFail)
        End If
    End Sub

    Private Sub ViewModel(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objCutOffPeriod As AssistCutOffPeriod = New AssistCutOffPeriodFacade(User).Retrieve(nID)
        'Todo session
        Session.Add("vsCutOffPeriod", objCutOffPeriod)
        If IsNothing(objCutOffPeriod) Then
            txtDealerCode.Text = ""
        Else
            txtDealerCode.Text = ""
        End If

        Me.btnSimpan.Enabled = EditStatus
    End Sub

    Private Sub SearchCutOffPeriodByCriteria()
        dtgCutOffPeriod.CurrentPageIndex = 0
        BindDataGrid(dtgCutOffPeriod.CurrentPageIndex)
    End Sub
#End Region

End Class
