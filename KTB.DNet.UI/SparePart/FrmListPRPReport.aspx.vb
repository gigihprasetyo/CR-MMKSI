Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Security
Imports KTB.DNet.Utility

Public Class FrmListPRPReport
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgReportPRP As System.Web.UI.WebControls.DataGrid
    Protected WithEvents cfFilter As FilterCompositeControl.CompositeFilter
    Protected WithEvents lblKodeOrganisasiValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaOrganisasiValue As System.Web.UI.WebControls.Label
    Protected WithEvents btnDelete As System.Web.UI.WebControls.Button
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents txtCreatedBy As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim sHPRP As SessionHelper = New SessionHelper

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ActivateUserPrivilege()
        lblPopUpDealer.Attributes.Add("onClick", "ShowPPDealerSelection();")
        If Not Page.IsPostBack Then
            InitiatePage()
            BindDataGrid(0)
        End If
    End Sub

    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.ViewListReportSubmitPRP_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=PARTSHOP REWARD PROGRAM - Daftar Pengiriman Laporan PRP")
        End If
    End Sub

    Private Sub InitiatePage()
        Dim objUser As UserInfo = sHPRP.GetSession("LOGINUSERINFO")
        If Not IsNothing(objUser) Then
            lblKodeOrganisasiValue.Text = objUser.Dealer.DealerCode
            lblNamaOrganisasiValue.Text = objUser.Dealer.DealerName & " / " & objUser.Dealer.SearchTerm2
        End If
        ViewState("vsSortDirect") = Sort.SortDirection.DESC
        ViewState("vsSortColumn") = "CreatedTime"
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim objFacade As PRPSenderInfoFacade = New PRPSenderInfoFacade(User)
        Dim critComp As CriteriaComposite
        If ddlStatus.SelectedValue = 0 Then
            critComp = New CriteriaComposite(New Criteria(GetType(PRPSenderInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Else
            If ddlStatus.SelectedValue = -1 Then
                critComp = New CriteriaComposite(New Criteria(GetType(PRPSenderInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Deleted, Short)))
            Else
                critComp = New CriteriaComposite(New Criteria(GetType(PRPSenderInfo), "RowStatus", MatchType.InSet, "(" & CType(DBRowStatus.Active, Short) & "," & CType(DBRowStatus.Deleted, Short) & ")"))
            End If
        End If

        If txtCreatedBy.Text.Trim <> String.Empty Then
            critComp.opAnd(New Criteria(GetType(PRPSenderInfo), "CreatedBy", MatchType.[Partial], txtCreatedBy.Text.Trim))
        End If

        If txtDealerCode.Text <> String.Empty And txtDealerCode.Text.Length = 6 Then
            Dim critDealer As Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text)
            If IsNothing(critDealer) Or critDealer.ID = 0 Then
                MessageBox.Show("Kriteria pencarian salah")
                dtgReportPRP.DataSource = New ArrayList
                dtgReportPRP.VirtualItemCount = 0
                dtgReportPRP.DataBind()
                Return
            End If
            critComp.opAnd(New Criteria(GetType(PRPSenderInfo), "CreatedBy", MatchType.StartsWith, critDealer.ID.ToString("000000")))
        ElseIf txtDealerCode.Text <> String.Empty And txtDealerCode.Text.Length <> 6 Then
            MessageBox.Show("Kriteria pencarian salah")
            dtgReportPRP.DataSource = New ArrayList
            dtgReportPRP.VirtualItemCount = 0
            dtgReportPRP.DataBind()
            Return
        End If
        If txtDescription.Text <> String.Empty Then
            critComp.opAnd(New Criteria(GetType(PRPSenderInfo), "Description", MatchType.[Partial], txtDescription.Text))
        End If

        Dim totalRow As Integer

        Dim objDomains As ArrayList
        Try
            objDomains = objFacade.RetrieveActiveList(critComp, indexPage + 1, dtgReportPRP.PageSize, totalRow, ViewState("vsSortColumn"), ViewState("vsSortDirect"))
        Catch
            objDomains = New ArrayList
        End Try

        dtgReportPRP.DataSource = objDomains
        dtgReportPRP.VirtualItemCount = totalRow
        dtgReportPRP.DataBind()
        Dim sh As SessionHelper = New SessionHelper
        sh.SetSession("CRITERIAPRP", critComp)
        'Dim sortDirection As Integer = ViewState("vsSortColumn")
        'Dim sortColumn As String = ViewState("vsSortColumn")
        'sh.SetSession("CRITERIAPRPSORTCOLOUMN", sortColumn)
        'sh.SetSession("CRITERIAPRPSORTDIRECTION", sortDirection)
    End Sub

    Private Sub dtgReportPRP_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgReportPRP.PageIndexChanged
        dtgReportPRP.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgReportPRP.CurrentPageIndex)
    End Sub

    Private Sub dtgReportPRP_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgReportPRP.SortCommand
        If CType(ViewState("vsSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("vsSortDirect"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("vsSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("vsSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("vsSortColumn") = e.SortExpression
            ViewState("vsSortDirect") = Sort.SortDirection.ASC
        End If

        dtgReportPRP.SelectedIndex = -1
        dtgReportPRP.CurrentPageIndex = 0
        BindDataGrid(dtgReportPRP.CurrentPageIndex)
    End Sub

    Private Sub dtgReportPRP_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgReportPRP.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dtgReportPRP.CurrentPageIndex * dtgReportPRP.PageSize)

            Dim data As PRPSenderInfo = CType(e.Item.DataItem, PRPSenderInfo)

            If Not e.Item.FindControl("lnkDelete") Is Nothing Then
                CType(e.Item.FindControl("lnkDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
            End If

            Dim cbItem As CheckBox = CType(e.Item.FindControl("cbItem"), CheckBox)
            cbItem.Attributes.Add("onclick", "EnableDelete('cbItem')")

            Dim lblOrganization As Label = CType(e.Item.FindControl("lblOrganization"), Label)
            If data.CreatedBy.Length >= 6 Then
                Dim objOrg As Dealer = New DealerFacade(User).Retrieve(CInt(data.CreatedBy().Substring(0, 6)))
                If Not IsNothing(objOrg) Then
                    lblOrganization.Text = objOrg.DealerCode
                End If
            End If

            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Select Case data.Status
                Case PRPSenderInfo.EnumSendStatus.Baru
                    lblStatus.Text = "Baru"
                Case PRPSenderInfo.EnumSendStatus.Sukses
                    lblStatus.Text = "Sukses"
                Case PRPSenderInfo.EnumSendStatus.Gagal
                    lblStatus.Text = "Gagal"
            End Select

            Dim lblCreatedBy As Label = CType(e.Item.FindControl("lblCreatedBy"), Label)
            Dim createdBy As String = data.CreatedBy.Remove(0, 6)
            Dim objUser As UserInfo = New UserInfoFacade(User).Retrieve(createdBy.Trim)
            lblCreatedBy.Text = createdBy & " - " & objUser.FirstName & " " & objUser.LastName & " - " & objUser.Email

            Dim lbtnActive As LinkButton = CType(e.Item.FindControl("lbtnActive"), LinkButton)
            Dim lbtnInActive As LinkButton = CType(e.Item.FindControl("lbtnInactive"), LinkButton)
            If data.RowStatus = 0 Then
                lbtnActive.Visible = False
                lbtnInActive.Visible = True
            Else
                lbtnActive.Visible = True
                lbtnInActive.Visible = False
            End If

        End If
    End Sub

    Private Sub dtgReportPRP_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgReportPRP.ItemCommand
        If e.CommandName = "Delete" Then
            Try
                DeleteReport(e.Item.Cells(0).Text)
                BindDataGrid(dtgReportPRP.CurrentPageIndex)
                MessageBox.Show(SR.DeleteSucces)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
        End If
        If e.CommandName = "Active" Then
            Try
                Activated(e.Item.Cells(0).Text, True)
                BindDataGrid(dtgReportPRP.CurrentPageIndex)
                MessageBox.Show("Proses aktivasi atau deaktivasi berhasil")
            Catch ex As Exception
                MessageBox.Show("Gagal proses aktivasi atau deaktivasi")
            End Try
        End If
        If e.CommandName = "Inactive" Then
            Try
                Activated(e.Item.Cells(0).Text, False)
                BindDataGrid(dtgReportPRP.CurrentPageIndex)
                MessageBox.Show("Proses aktivasi atau deaktivasi berhasil")
            Catch ex As Exception
                MessageBox.Show("Gagal proses aktivasi atau deaktivasi")
            End Try
        End If
    End Sub

    Private Sub Activated(ByVal nID As Integer, ByVal Activated As Boolean)
        Dim objFacade As PRPSenderInfoFacade = New PRPSenderInfoFacade(User)
        Dim objPRPSenderInfo As PRPSenderInfo = objFacade.Retrieve(nID)
        If IsNothing(objPRPSenderInfo) Or objPRPSenderInfo.ID = 0 Then
            Throw New Exception("Gagal proses aktivasi atau deaktivasi")
        Else
            If Activated = True Then
                objPRPSenderInfo.RowStatus = DBRowStatus.Active
                If objFacade.Update(objPRPSenderInfo) < 0 Then
                    Throw New Exception("Gagal proses aktivasi atau deaktivasi")
                End If
            Else
                objPRPSenderInfo.RowStatus = DBRowStatus.Deleted
                If objFacade.Update(objPRPSenderInfo) < 0 Then
                    Throw New Exception("Gagal proses aktivasi atau deaktivasi")
                End If
            End If
        End If
    End Sub

    Private Sub DeleteReport(ByVal nID As Integer)
        Dim objFacade As PRPSenderInfoFacade = New PRPSenderInfoFacade(User)
        Dim objPRPSenderInfo As PRPSenderInfo = objFacade.Retrieve(nID)
        If IsNothing(objPRPSenderInfo) Or objPRPSenderInfo.ID = 0 Then
            Throw New Exception(SR.DeleteFail)
        Else
            If objFacade.DeleteFromDB(objPRPSenderInfo) < 0 Then
                Throw New Exception(SR.DeleteFail)
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim message As String = ""
        Try
            DeleteAll()
            message = SR.DeleteSucces
        Catch ex As Exception
            message = ex.Message
        Finally
            dtgReportPRP.CurrentPageIndex = 0
            BindDataGrid(dtgReportPRP.CurrentPageIndex)
            MessageBox.Show(message)
        End Try
    End Sub

    Private Sub DeleteAll()
        Dim ttlDeleteSuccess As Integer = 0
        Dim ttlDeleteFail As Integer = 0
        For row As Integer = 0 To dtgReportPRP.Items.Count - 1
            If dtgReportPRP.Items(row).ItemType = ListItemType.Item Or dtgReportPRP.Items(row).ItemType = ListItemType.AlternatingItem Then
                Dim cbItem As CheckBox = dtgReportPRP.Items(row).FindControl("cbItem")
                If Not IsNothing(cbItem) And cbItem.Checked Then
                    Try
                        DeleteReport(CInt(dtgReportPRP.Items(row).Cells(0).Text))
                        ttlDeleteSuccess += 1
                    Catch
                        ttlDeleteFail += 1
                    End Try
                End If
            End If
        Next
        If ttlDeleteFail <> 0 Then
            Throw New Exception("Delete: Sukses=" + CStr(ttlDeleteSuccess) + " Gagal=" + CStr(ttlDeleteFail))
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dtgReportPRP.CurrentPageIndex = 0
        BindDataGrid(dtgReportPRP.CurrentPageIndex)
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Response.Redirect("FrmDownloadPRP.aspx")
    End Sub
End Class
