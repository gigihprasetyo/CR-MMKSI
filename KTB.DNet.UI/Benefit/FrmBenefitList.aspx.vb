Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Benefit

Public Class FrmBenefitList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgTable As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtCodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRegNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtNoSurat As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnTambah As System.Web.UI.WebControls.Button
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Private sessHelper As New SessionHelper

#Region "Private Property"
    Private Viewdaftarbenefit_privillage As Boolean
    Private inputdaftarbenefit_privillage As Boolean
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        'If Not SecurityProvider.Authorize(context.User, SR.AlertManagementListView_Privilege) Then
        '    Server.Transfer("../FrmAccessDenied.aspx?modulName=ALERT MANAGEMENT - Alert Managemen List")
        'End If

        inputdaftarbenefit_privillage = False
        Viewdaftarbenefit_privillage = False
        inputdaftarbenefit_privillage = SecurityProvider.Authorize(Context.User, SR.inputdaftarbenefit_privillage)

        If Not inputdaftarbenefit_privillage Then
            Viewdaftarbenefit_privillage = SecurityProvider.Authorize(Context.User, SR.Viewdaftarbenefit_privillage)
            If Not Viewdaftarbenefit_privillage Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=SALES CAMPAIGN - Daftar Benefit")
            End If

        End If

        Viewdaftarbenefit_privillage = inputdaftarbenefit_privillage
 

    End Sub

    'Dim bCekDetailPriv As Boolean = SecurityProvider.Authorize(context.User, SR.AlertManagementListView_Privilege)
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        InitiateAuthorization()
        If Not IsPostBack Then

            'InitDisplayAlertategory()
            lblPopUpDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            ViewState("currentSortColumn") = "Name"
            ViewState("currentSortDirection") = Sort.SortDirection.ASC
            'ViewState("currentSortDirection") = Sort.SortDirection.DESC

            dgTable.CurrentPageIndex = 0

            If Not Request.QueryString("SessionForDisplayDetail") Is Nothing Then
                If Not sessHelper.GetSession("SessionForDisplay") Is Nothing Then
                    Dim list As New ArrayList
                    list = CType(sessHelper.GetSession("SessionForDisplay"), ArrayList)
                    txtCodeDealer.Text = list.Item(0).ToString
                    txtRegNo.Text = list.Item(1).ToString
                    txtNoSurat.Text = list.Item(2).ToString
                    ddlStatus.SelectedValue = list.Item(3).ToString
                End If
            End If

            If Not sessHelper.GetSession("SessionForDisplay") Is Nothing Then
                Dim list As New ArrayList
                list = CType(sessHelper.GetSession("SessionForDisplay"), ArrayList)
                txtCodeDealer.Text = list.Item(0).ToString
                txtRegNo.Text = list.Item(1).ToString
                txtNoSurat.Text = list.Item(2).ToString
                ddlStatus.SelectedValue = list.Item(3).ToString
            End If

            BindDataGrid(dgTable.CurrentPageIndex, "ID", Sort.SortDirection.DESC)
        End If
    End Sub

    'Private Sub InitDisplayAlertCategory()
    '    Dim facade As New AlertCategoryFacade(User)
    '    Dim arlAlertCategory As ArrayList = facade.RetrieveActiveList()

    '    ddlAlertCategory.Items.Clear()
    '    ddlAlertCategory.Items.Add(New ListItem("Semua", 0))

    '    For Each cat As AlertCategory In arlAlertCategory
    '        ddlAlertCategory.Items.Add(New ListItem(cat.Description, cat.ID))
    '    Next
    '    ddlAlertCategory.SelectedIndex = 0
    '    RebindModulDropdownList()
    'End Sub

    'Private Sub RebindModulDropdownList()
    '    Dim categoryId As Integer = CInt(ddlAlertCategory.SelectedValue)
    '    Dim arlModul As ArrayList = New AlertModulFacade(User).RetrieveActiveListByCategoryID(categoryId)

    '    ddlAlertModul.Items.Clear()
    '    ddlAlertModul.Items.Add(New ListItem("Semua", 0))
    '    For Each modul As AlertModul In arlModul
    '        ddlAlertModul.Items.Add(New ListItem(modul.Description, modul.ID))
    '    Next
    'End Sub

    Private Sub BindDataGrid(ByVal index As Integer, Optional ByVal sortColoum As String = Nothing, Optional ByVal sortType As Sort.SortDirection = Sort.SortDirection.ASC)
        Dim _arrList As New ArrayList
        Dim totalRow As Integer = 0


        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))


        If txtCodeDealer.Text <> "" Then
            Dim strSql As String = ""
            strSql += " select ID from BenefitMasterHeader as bmh where bmh.ID in "
            strSql += " (select a.BenefitMasterHeaderID from BenefitMasterDealer a "
            strSql += " inner join Dealer b on a.DealerID = b.ID where "
            strSql += " b.DealerCode in ('" & txtCodeDealer.Text.Replace(";", "','") & "'))"
            criterias.opAnd(New Criteria(GetType(BenefitMasterHeader), "ID", MatchType.InSet, "(" & strSql & ")"))
        End If
        If txtRegNo.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(BenefitMasterHeader), "BenefitRegNo", MatchType.[Partial], txtRegNo.Text.Trim))
        End If
        If Not ddlStatus.SelectedValue = "" Then
            criterias.opAnd(New Criteria(GetType(BenefitMasterHeader), "Status", MatchType.Exact, CType(ddlStatus.SelectedValue, Short)))
        End If
        If txtNoSurat.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(BenefitMasterHeader), "NomorSurat", MatchType.[Partial], txtNoSurat.Text.Trim))
        End If


        '_arrList = New BenefitMasterHeaderFacade(User).RetrieveActiveList(criterias, index + 1, dgTable.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        _arrList = New BenefitMasterHeaderFacade(User).RetrieveActiveList(criterias, index + 1, dgTable.PageSize, totalRow, sortColoum, sortType)


        Dim arForDisplay As New ArrayList
        arForDisplay.Add(txtCodeDealer.Text)
        arForDisplay.Add(txtRegNo.Text)
        arForDisplay.Add(txtNoSurat.Text)
        arForDisplay.Add(ddlStatus.SelectedValue)
        sessHelper.SetSession("SessionForDisplay", arForDisplay)

        dgTable.VirtualItemCount = totalRow

        dgTable.DataSource = _arrList
        'dgTable.VirtualItemCount = totalRow
        dgTable.DataBind()
    End Sub
    Private Sub dgTable_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgTable.ItemCommand
        Select Case e.CommandName
            Case "View"
                Response.Redirect("~/Benefit/FrmNewInputMasterList.aspx?Mode=View&id=" & CInt(e.CommandArgument))
            Case "Edit"

                'check claim related
                'Dim _arrList3 As New ArrayList
                'Dim criterias3 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitClaimDetails), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'Dim strSql As String = ""
                'strSql += "   select a.id from benefitmasterdetail a"
                'strSql += "   inner join benefitmasterheader b on a.benefitmasterheaderid = b.id "
                'strSql += "  where b.ID = " & CInt(e.CommandArgument)
                'criterias3.opAnd(New Criteria(GetType(BenefitClaimDetails), "BenefitMasterDetail", MatchType.InSet, "(" & strSql & ")"))

                '_arrList3 = New BenefitClaimDetailsFacade(User).Retrieve(criterias3)
                'If _arrList3.Count > 0 Then
                '    '  MessageBox.Show("Benefit masih digunakan di modul lain (Claim).")
                '    ' Return
                '    Response.Redirect("~/Benefit/FrmNewInputMasterList.aspx?Mode=View&status=1&id=" & CInt(e.CommandArgument))
                'Else
                '    Response.Redirect("~/Benefit/FrmNewInputMasterList.aspx?Mode=Edit&id=" & CInt(e.CommandArgument))
                'End If
                'end check claim related

                Response.Redirect("~/Benefit/FrmNewInputMasterList.aspx?Mode=Edit&id=" & CInt(e.CommandArgument))


                'Dim _arrList As New ArrayList
                'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                ''  criterias.opAnd(New Criteria(GetType(BenefitEventHeader), "BenefitMasterHeader", MatchType.Exact, CInt(IDBenefitListHeader)))


                'Dim strSql As String = ""
                'strSql += "  select b.id from BenefitMasterHeader a "
                'strSql += "  inner join BenefitMasterDetail b on a.ID = b.BenefitMasterHeaderID"
                'strSql += "  and b.RowStatus = 0"
                'strSql += "  where a.RowStatus = 0 and b.BenefitTypeID is not null and b.BenefitTypeID <> ''"
                'strSql += "  and a.ID = " & CInt(e.CommandArgument)
                'criterias.opAnd(New Criteria(GetType(BenefitMasterDetail), "ID", MatchType.InSet, "(" & strSql & ")"))
                '_arrList = New BenefitMasterDetailFacade(User).Retrieve(criterias)
                'If _arrList.Count > 0 Then
                '    MessageBox.Show("Benefit masih digunakan di modul lain (Benefit Detail).")
                '    Return
                'End If

                'Dim _arrList2 As New ArrayList
                'Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitEventDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'criterias2.opAnd(New Criteria(GetType(BenefitEventDetail), "BenefitMasterHeader", MatchType.Exact, CInt(e.CommandArgument)))
                '_arrList2 = New BenefitEventDetailFacade(User).Retrieve(criterias2)
                'If _arrList2.Count > 0 Then
                '    MessageBox.Show("Benefit masih digunakan di modul lain (Event).")
                '    Return
                'End If




                

                ' Response.Redirect("~/Benefit/FrmNewInputMasterList.aspx?Mode=Edit&id=" & CInt(e.CommandArgument))
            Case "Delete"


                'Dim _arrList As New ArrayList
                'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitMasterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                ''  criterias.opAnd(New Criteria(GetType(BenefitEventHeader), "BenefitMasterHeader", MatchType.Exact, CInt(IDBenefitListHeader)))


                'Dim strSql As String = ""
                'strSql += "  select b.id from BenefitMasterHeader a "
                'strSql += "  inner join BenefitMasterDetail b on a.ID = b.BenefitMasterHeaderID"
                'strSql += "  and b.RowStatus = 0"
                'strSql += "  where a.RowStatus = 0 and b.BenefitTypeID is not null and b.BenefitTypeID <> ''"
                'strSql += "  and a.ID = " & CInt(e.CommandArgument)
                'criterias.opAnd(New Criteria(GetType(BenefitMasterDetail), "ID", MatchType.InSet, "(" & strSql & ")"))
                '_arrList = New BenefitMasterDetailFacade(User).Retrieve(criterias)
                'If _arrList.Count > 0 Then
                '    MessageBox.Show("Benefit masih digunakan di modul lain (Benefit Detail).")
                '    Return
                'End If

                'Dim _arrList2 As New ArrayList
                'Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BenefitEventDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'criterias2.opAnd(New Criteria(GetType(BenefitEventDetail), "BenefitMasterHeader", MatchType.Exact, CInt(e.CommandArgument)))
                '_arrList2 = New BenefitEventDetailFacade(User).Retrieve(criterias2)
                'If _arrList2.Count > 0 Then
                '    MessageBox.Show("Benefit masih digunakan di modul lain (Event).")
                '    Return
                'End If



                Response.Redirect("~/Benefit/FrmNewInputMasterList.aspx?Mode=Delete&id=" & CInt(e.CommandArgument))
        End Select
    End Sub

    Private Sub dgTable_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgTable.ItemDataBound
        If Not IsNothing(e.Item.DataItem) Then
            Dim objDomain As BenefitMasterHeader = CType(e.Item.DataItem, BenefitMasterHeader)


            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = (e.Item.ItemIndex + 1 + (dgTable.CurrentPageIndex * dgTable.PageSize)).ToString()

            Dim lblNoSurat As Label = CType(e.Item.FindControl("lblNoSurat"), Label)
            lblNoSurat.Text = objDomain.NomorSurat

            Dim lblRegNo As Label = CType(e.Item.FindControl("lblRegNo"), Label)
            lblRegNo.Text = objDomain.BenefitRegNo

            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            If objDomain.Status = 1 Then
                lblStatus.Text = "Tidak Aktif"
            Else
                lblStatus.Text = "Aktif"
            End If


            Dim alBenefitMasterDealers As ArrayList = objDomain.BenefitMasterDealers
            Dim idDealer As String = ""
            Dim index As Integer = 0
            For Each el As BenefitMasterDealer In alBenefitMasterDealers
                'idDealer += el.ID.ToString() + ";"
                'idDealer += el.Dealer.DealerCode + "<br />"
                'idDealer += el.Dealer.DealerCode + ","
                ' idDealer += el.Dealer.DealerCode + " - " + el.Dealer.DealerName + "; "
                idDealer += el.Dealer.DealerCode + "; "
                If index >= 3 Then
                    idDealer += "..... "
                    Exit For
                End If
                index += 1
                'idDealer += el.Dealer.DealerCode + " - " + el.Dealer.DealerName + "<br />"
            Next
            Dim lblKodeDealer As Label = CType(e.Item.FindControl("lblKodeDealer"), Label)
            lblKodeDealer.Text = idDealer

            'Dim alBenefitMasterDetails As ArrayList = objDomain.BenefitMasterDetails
            'Dim idPeriod As String = ""
            'For Each el As BenefitMasterDetail In alBenefitMasterDetails
            '    'idDealer += el.ID.ToString() + ";"
            '    idPeriod += el.FakturValidationStart.ToString("dd/MM/yyyy") + " - " + el.FakturValidationEnd.ToString("dd/MM/yyyy") + "<br />"
            'Next
            'Dim lblPeriod As Label = CType(e.Item.FindControl("lblPeriod"), Label)
            'lblPeriod.Text = idPeriod

            Dim lblRemark As Label = CType(e.Item.FindControl("lblRemark"), Label)
            lblRemark.Text = objDomain.Remarks

            Dim lnkbtnView As LinkButton = CType(e.Item.FindControl("lnkbtnView"), LinkButton)
            Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)

            'add privilige
            ' lnkbtnPopUp.Visible = bCekDetailPriv
            lnkbtnView.Visible = inputdaftarbenefit_privillage
            lnkbtnEdit.Visible = inputdaftarbenefit_privillage




        End If
    End Sub

    Private Sub dgTable_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgTable.PageIndexChanged
        dgTable.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgTable.CurrentPageIndex, "ID", Sort.SortDirection.DESC)
    End Sub

    Private Sub dgTable_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgTable.SortCommand
        'If CType(viewstate("currentSortColumn"), String) = e.SortExpression Then
        '    Select Case CType(viewstate("currentSortDirection"), Sort.SortDirection)

        '        Case Sort.SortDirection.ASC
        '            viewstate("currentSortDirection") = Sort.SortDirection.DESC

        '        Case Sort.SortDirection.DESC
        '            viewstate("currentSortDirection") = Sort.SortDirection.ASC
        '    End Select
        'Else
        '    viewstate("currentSortColumn") = e.SortExpression
        '    viewstate("currentSortDirection") = Sort.SortDirection.DESC
        'End If

        Select Case CType(ViewState("currentSortDirection"), Sort.SortDirection)

            Case Sort.SortDirection.ASC
                ViewState("currentSortDirection") = Sort.SortDirection.DESC

            Case Sort.SortDirection.DESC
                ViewState("currentSortDirection") = Sort.SortDirection.ASC
        End Select

        ViewState("currentSortColumn") = e.SortExpression

        dgTable.SelectedIndex = -1
        dgTable.CurrentPageIndex = 0
        'BindDataGrid(dgTable.CurrentPageIndex)
        BindDataGrid(dgTable.CurrentPageIndex, e.SortExpression, ViewState("currentSortDirection"))
    End Sub


    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dgTable.CurrentPageIndex = 0
        BindDataGrid(dgTable.CurrentPageIndex, "ID", Sort.SortDirection.DESC)
    End Sub

    Private Sub btnTambah_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTambah.Click
        Response.Redirect("~/Benefit/FrmNewInputMasterList.aspx?mode=Insert")
    End Sub
End Class
