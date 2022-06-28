Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports System
Imports System.Drawing.Color
Imports System.Text
Imports KTB.DNet.Security

Public Class FrmListDealerBranch
    Inherits System.Web.UI.Page

    Private objDealer As Dealer
    Private m_bAdminAssigHakAccessOrganization_Privilege As Boolean = False
    Private m_bFormPrivilege As Boolean = False
    Private _sessHelper As SessionHelper = New SessionHelper

    Private Sub BindDatagrid(ByVal indexPage)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) And Not IsNothing(CType(_sessHelper.GetSession("Criteria"), CriteriaComposite)) Then
            dtgDealerList.DataSource = New DealerFacade(User).RetrieveActiveList(CType(_sessHelper.GetSession("Criteria"), CriteriaComposite), _
              indexPage + 1, dtgDealerList.PageSize, totalRow, _
              CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgDealerList.VirtualItemCount = totalRow
            dtgDealerList.DataBind()
        End If
    End Sub
    Private Sub BindDdlStatus()
        Dim listStatus As New EnumDealerStatus
        Dim al As ArrayList = listStatus.RetrieveStatus
        For Each item As EnumDealer In al
            ddlstatus.Items.Insert(0, New ListItem(item.NameStatus, item.ValStatus))
        Next
        ddlstatus.Items.Insert(0, New ListItem("Pilih Status", ""))
    End Sub
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            BindDdlType()
            BindDdlStatus()
            InitiatePage()
            'kalau dipanggil dari form dealer maintenance setelah proses update
            If Not (Request.QueryString("DealerID")) Is Nothing Then
                Dim nID As Integer = CType(Request.QueryString("DealerID"), Integer)
                If Not IsNothing(nID) Then
                    MessageBox.Show("Update Sukses !")
                    Dim ObjDealer As Dealer = New DealerFacade(User).Retrieve(nID)
                    If Not ObjDealer Is Nothing Then
                        Dim arrObjDealer As ArrayList = New ArrayList
                        arrObjDealer.Add(ObjDealer)
                        dtgDealerList.DataSource = arrObjDealer
                        dtgDealerList.DataBind()
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub ActivateUserPrivilege()

        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.AdminUpdateOrganization_Privilege)
        m_bAdminAssigHakAccessOrganization_Privilege = SecurityProvider.Authorize(Context.User, SR.AdminAssigHakAccessOrganization_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.AdminViewOrganization_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=GENERAL - Daftar Organisasi")
        End If

    End Sub

    Private Sub InitiatePage()
        ClearData()
        AssignAttributeControl()
        ViewState("CurrentSortColumn") = "DealerCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub
    Private Sub ClearData()
        cbSalesUnit.Checked = False
        cbService.Checked = False
        cbSparePart.Checked = False
    End Sub
    Private Sub AssignAttributeControl()
        Dim lblPopUp As Label = CType(Page.FindControl("lblPopUpDealer"), Label)
        lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpDealerSelection.aspx", "", 500, 760, "DealerSelection")
    End Sub
    Private Function ConvertKodeDealer(ByVal sKodeDealerColl As String)
        Dim func As New DealerFacade(User)
        Dim sKodeDealerTemp() As String = sKodeDealerColl.Split(New Char() {";"})
        Dim sKodeDealer As String = ""
        For i As Integer = 0 To sKodeDealerTemp.Length - 1
            Dim objDealer As Dealer = func.Retrieve(sKodeDealerTemp(i).Trim())
            If objDealer.ID > 0 Then
                sKodeDealer = sKodeDealer & "'" & objDealer.ID.ToString() & "'"

                If Not (i = sKodeDealerTemp.Length - 1) Then
                    sKodeDealer = sKodeDealer & ","
                End If
            End If
        Next
        If Not String.IsNullorEmpty(sKodeDealer) Then
            sKodeDealer = "(" & sKodeDealer & ")"
            Return sKodeDealer
        Else
            Return "(0)"
        End If
    End Function

    Private Sub BindDdlType()
        Dim listStatus As New EnumDealerBranchType
        Dim al As ArrayList = listStatus.Retrieve

        For Each item As EnumDealerBranch In al

            ddlBranchType.Items.Insert(0, New ListItem(item.NameStatus, item.ValStatus))
        Next
        ddlBranchType.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
    End Sub

    Private Sub CreateCriteriaSearch()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If
        If Not (txtKodeDealer.Text.Trim() = "") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "ParentDealerID", MatchType.InSet, ConvertKodeDealer(txtKodeDealer.Text.Trim())))
        End If
        If Not String.IsNullorEmpty(txtBranchCode.Text.Trim) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, txtBranchCode.Text))
        End If

        If Not (txtDealerName.Text.Trim() = "") Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerName", MatchType.[Partial], txtDealerName.Text.Trim()))
        End If
        If Not ddlstatus.SelectedValue = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "Status", MatchType.Exact, ddlstatus.SelectedValue))
        End If
        If cbSalesUnit.Checked Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "SalesUnitFlag", MatchType.Exact, CType(EnumDealerStatus.DealerStatus.Aktive, String)))
        End If
        If cbService.Checked Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "ServiceFlag", MatchType.Exact, CType(EnumDealerStatus.DealerStatus.Aktive, String)))
        End If
        If cbSparePart.Checked Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "SparepartFlag", MatchType.Exact, CType(EnumDealerStatus.DealerStatus.Aktive, String)))
        End If

        If ddlBranchType.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "OrganizationBranchType", MatchType.Exact, CShort(ddlBranchType.SelectedValue)))
        Else
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "OrganizationBranchType", MatchType.No, CShort(EnumOrganizationBranchType.EnumOrgBranchType.Dealer)))
        End If

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "Title", MatchType.No, CType(EnumDealerTittle.DealerTittle.KTB, String)))

        _sessHelper.SetSession("Criteria", criterias)
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        CreateCriteriaSearch()
        dtgDealerList.CurrentPageIndex = 0
        BindDatagrid(dtgDealerList.CurrentPageIndex)
    End Sub

    Private Sub dtgDealerList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDealerList.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            BoundRowItems(e)
        End If
    End Sub

    Private Function CreateAggreateForCountRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Function GetUserInfoCriteria(ByVal nDealerID As Integer, ByVal cUserStatus As Byte) As ICriteria
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserInfo), "Dealer.ID", MatchType.Exact, nDealerID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.UserInfo), "UserStatus", MatchType.Exact, cUserStatus))
        Return criterias
    End Function

    Private Sub BoundRowItems(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)

        Dim objDealer As Dealer = CType(CType(dtgDealerList.DataSource, ArrayList)(e.Item.ItemIndex), Dealer)
        CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgDealerList.CurrentPageIndex * dtgDealerList.PageSize)
        Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
        If lblStatus.Text = "Tidak Aktif" Then
            lblStatus.ForeColor = Red
            e.Item.BackColor = Color.LightSalmon
        End If

        'privilege
        'ActivateUserPrivilege()
        If Not CType(e.Item.FindControl("lbtnEdit"), LinkButton) Is Nothing Then
            CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = m_bFormPrivilege
        End If

        If Not CType(e.Item.FindControl("lbtnHakAkses"), LinkButton) Is Nothing Then
            CType(e.Item.FindControl("lbtnHakAkses"), LinkButton).Visible = m_bAdminAssigHakAccessOrganization_Privilege
        End If

        If Not CType(e.Item.FindControl("lbtnHakAkses"), LinkButton) Is Nothing Then
            'CType(e.Item.FindControl("lbtnHakAkses"), LinkButton).Visible = m_bFormPrivilege
        End If

        If Not CType(e.Item.FindControl("lblUserActive"), Label) Is Nothing Then
            CType(e.Item.FindControl("lblUserActive"), Label).Text = CType(New DealerFacade(User).RecordCount(CreateAggreateForCountRecord(GetType(UserInfo)), GetUserInfoCriteria(objDealer.ID, EnumUserStatus.UserStatus.Aktif)), String)
        End If

    End Sub


    Private Sub dtgDealerList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgDealerList.ItemCommand
        If e.CommandName = "Edit" Then
            'Dim objDealer As Dealer = CType(CType(Session("sessDealer"), ArrayList)(e.Item.ItemIndex), Dealer)
            Response.Redirect("../General/FrmDealerMaintenance.aspx?dealerid=" + e.Item.Cells(0).Text.Trim() + "&user=mks")
        End If
        If e.CommandName = "HakAkses" Then
            Dim objDealer As Dealer = New DealerFacade(User).Retrieve(CInt(e.Item.Cells(0).Text.Trim()))
            _sessHelper.SetSession("SessObjDealer", objDealer)
            Response.Redirect("../UserManagement/FrmAssignAccessibility.aspx")
        End If
    End Sub
    Private Sub dtgDealerList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgDealerList.PageIndexChanged
        dtgDealerList.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgDealerList.CurrentPageIndex)
    End Sub

    Private Sub dtgDealerList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgDealerList.SortCommand
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

        dtgDealerList.CurrentPageIndex = 0
        BindDatagrid(dtgDealerList.CurrentPageIndex)
    End Sub

End Class