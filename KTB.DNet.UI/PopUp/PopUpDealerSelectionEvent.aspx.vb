Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade
Imports System.Text

Public Class PopUpDealerSelectionEvent
    Inherits System.Web.UI.Page

    Protected countChk As Integer = 0
    Private objDealer As Dealer

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            'Added by Ery for sorting
            ViewState.Add("SortColDealer", "DealerCode")
            ViewState.Add("SortDirDealer", Sort.SortDirection.ASC)

            objDealer = Session("DEALER")
            If Not objDealer.DealerGroup Is Nothing OrElse objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                BindDdlGroup()
            End If

            ClearData()
            Me.hdnDealerCode.Value = Request.QueryString("DealerCode")
            Me.hdnCategory.Value = Request.QueryString("Category")
            Me.hdnMode.Value = Request.QueryString("Mode")
            Me.hdnEventDealerHeaderID.Value = Request.QueryString("EventDealerHeaderID")
            If hdnDealerCode.Value <> String.Empty Then
                btnSearch_Click(Nothing, Nothing)
            End If
        End If
    End Sub

    Private Sub ClearData()
        Me.txtSearch1.Text = String.Empty
        Me.txtSearch2.Text = String.Empty
        Me.txtDealerName.Text = String.Empty
        Me.lboxGroup.SelectedIndex = -1
    End Sub

    Private Sub BindDdlGroup()
        objDealer = Session("DEALER")
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Area2), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(Area2), "Description", Sort.SortDirection.ASC))
        lboxGroup.DataSource = New Area2Facade(User).Retrieve(criterias, sortColl)
        lboxGroup.DataTextField = "Description"
        lboxGroup.DataValueField = "ID"
        lboxGroup.DataBind()
    End Sub

    Private Function GetSelectedItem(ByVal listboxGroup As ListBox) As String
        Dim _strGroup As String = String.Empty
        If Not listboxGroup.SelectedIndex = -1 Then
            For Each item As ListItem In listboxGroup.Items
                If item.Selected Then
                    If _strGroup = String.Empty Then
                        _strGroup = item.Value
                    Else
                        _strGroup = _strGroup & "," & item.Value
                    End If
                End If
            Next
        End If

        Return _strGroup
    End Function

    Public Sub BindSearch()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "Status", MatchType.Exact, CType(EnumDealerStatus.DealerStatus.Aktive, String)))
        'If ViewState("Group") Is Nothing Then
        '    objDealer = Session("DEALER")
        '    ' 08-Nov-2007   Deddy H     Handle bug 1383, bila dari send message bs send ke semua dealer
        '    If IsNothing(Request.QueryString("All")) Then
        '        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
        '            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        '        End If
        '    End If
        'End If


        If Not txtDealerName.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerName", MatchType.[Partial], txtDealerName.Text))
        End If

        If Not txtSearch1.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "SearchTerm1", MatchType.[Partial], txtSearch1.Text))
        End If
        If Not txtSearch2.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.[Partial], txtSearch2.Text))
        End If

        If Not IsNothing(Request.QueryString("DEALERONLY")) Then

            criterias.opAnd(New Criteria(GetType(Dealer), "ID", MatchType.No, "1"))
            criterias.opAnd(New Criteria(GetType(Dealer), "Title", MatchType.Exact, "0"))

        End If

        Dim SelectedGroup As String = GetSelectedItem(lboxGroup)
        If SelectedGroup <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "Area2.ID", MatchType.InSet, "(" & SelectedGroup & ")"))
        End If

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "OrganizationBranchType", MatchType.Exact, 1))

        If Me.hdnCategory.Value.Trim <> "-1" Then
            Dim strSQL As String = "select DealerID from DealerCategory "
            strSQL += "where CategoryID in (Select ID from Category where ID ='" & hdnCategory.Value & "')"
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "ID", MatchType.InSet, "(" & strSQL & ")"))
        End If
        dtgDealerSelection.DataSource = New DealerFacade(User).RetrieveActiveList(criterias, ViewState("SortColDealer"), ViewState("SortDirDealer"))
        dtgDealerSelection.DataBind()
    End Sub


    Private Sub dtgDealerSelection_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDealerSelection.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As Dealer = CType(e.Item.DataItem, Dealer)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim lblGroup As Label = CType(e.Item.FindControl("lblGroup"), Label)

                Dim chkItemChecked As CheckBox = CType(e.Item.FindControl("chkItemChecked"), CheckBox)
                If hdnDealerCode.Value.Contains(RowValue.DealerCode) Then
                    chkItemChecked.Checked = True
                Else
                    chkItemChecked.Checked = False
                End If

                If Not IsNothing(RowValue.DealerGroup) Then
                    lblGroup.Text = RowValue.DealerGroup.GroupName
                End If
                Dim lblCity As Label = CType(e.Item.FindControl("lblCity"), Label)
                If Not IsNothing(RowValue.City) Then
                    lblCity.Text = RowValue.City.CityName
                End If
            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindSearch()

        If dtgDealerSelection.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub dtgDealerSelection_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgDealerSelection.SortCommand
        If e.SortExpression = ViewState("SortColDealer") Then
            If ViewState("SortDirDealer") = Sort.SortDirection.ASC Then
                ViewState.Add("SortDirDealer", Sort.SortDirection.DESC)
            Else
                ViewState.Add("SortDirDealer", Sort.SortDirection.ASC)
            End If
        End If
        ViewState.Add("SortColDealer", e.SortExpression)
        BindSearch()
    End Sub

    Private Sub dtgDealerSelection_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgDealerSelection.PageIndexChanged
        dtgDealerSelection.CurrentPageIndex = e.NewPageIndex
        BindSearch()
    End Sub

    Private Sub btnChoose_ServerClick(sender As Object, e As EventArgs) Handles btnChoose.ServerClick
        Dim objBabitEventProposalHeaderFacade As BabitEventProposalHeaderFacade = New BabitEventProposalHeaderFacade(User)
        Dim arrBabitEventProposalHeader As ArrayList = New ArrayList

        If hdnMode.Value = "Edit" Then
            Dim arrDealerCode As New ArrayList
            For Each item As DataGridItem In dtgDealerSelection.Items
                Dim chkCek As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
                Dim lblDealerCode As Label = CType(item.FindControl("lblDealerCode"), Label)
                If chkCek.Checked = True Then
                    arrDealerCode.Add(lblDealerCode.Text)
                End If
            Next
            If hdnDealerCode.Value.Trim <> String.Empty Then
                Dim arrDealerCodeChecked As New ArrayList
                Dim isExistDealer As Boolean = False
                For Each sDealerCodeOld As String In hdnDealerCode.Value.Trim.Split(";")
                    isExistDealer = False
                    For Each sDealerCodeNew As String In arrDealerCode
                        If sDealerCodeOld = sDealerCodeNew Then
                            isExistDealer = True
                            Exit For
                        End If
                    Next
                    If isExistDealer = False Then
                        arrDealerCodeChecked.Add(sDealerCodeOld)
                    End If
                Next

                Dim sb As StringBuilder = New StringBuilder
                Dim i% = 0
                If arrDealerCodeChecked.Count > 0 Then
                    sb.Append("Event telah dibuat proposal oleh dealer:\n")
                    For Each sDealerCode As String In arrDealerCodeChecked
                        Dim criteriasaa As New CriteriaComposite(New Criteria(GetType(BabitEventProposalHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criteriasaa.opAnd(New Criteria(GetType(BabitEventProposalHeader), "Dealer.DealerCode", MatchType.Exact, sDealerCode))
                        criteriasaa.opAnd(New Criteria(GetType(BabitEventProposalHeader), "EventDealerHeader.ID", MatchType.Exact, hdnEventDealerHeaderID.Value))
                        arrBabitEventProposalHeader = objBabitEventProposalHeaderFacade.Retrieve(criteriasaa)
                        If Not IsNothing(arrBabitEventProposalHeader) AndAlso arrBabitEventProposalHeader.Count > 0 Then
                            i += 1
                            Dim objBabitEventProposalHeader As BabitEventProposalHeader = CType(arrBabitEventProposalHeader(0), BabitEventProposalHeader)
                            sb.Append(i.ToString() & ". " & sDealerCode & " - Proposal " & objBabitEventProposalHeader.EventRegNumber & "\n")
                        End If
                    Next
                    If sb.Length > 0 AndAlso i > 0 Then
                        MessageBox.Show(sb.ToString())
                        Exit Sub
                    End If
                End If
            End If
        End If

        ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "GetSelectedDealer();", True)
    End Sub
End Class
