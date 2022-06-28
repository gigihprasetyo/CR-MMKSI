﻿Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade.UserManagement


Public Class PopUpDealerSelectionTOP
    Inherits System.Web.UI.Page

    Protected countChk As Integer = 0
    Private objDealer As Dealer

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            'Added by Ery for sorting
            ViewState.Add("SortColDealer", "DealerCode")
            ViewState.Add("SortDirDealer", Sort.SortDirection.ASC)
            ViewState.Add("Mode", CStr(Request.QueryString("Mode")))

            '''  - BEGIN : Added by MU, used in FrmUserOrganizationAssignment.aspx
            ViewState.Add("Group", CStr(Request.QueryString("Group")))
            ViewState.Add("Dealer", CStr(Request.QueryString("Dealer")))
            '''  - END   : Added by MU, used in FrmUserOrganizationAssignment.aspx
            objDealer = Session("DEALER")
            If Not objDealer.DealerGroup Is Nothing OrElse objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                BindDdlGroup()
            End If

            ClearData()
        End If
        SearchData()
        Dim mode As String = ViewState("Mode")
        If mode = "Dealer" Then
            btnChoose.Attributes.Add("onclick", "ShowPPDealerSelection();")
        Else
            btnChoose.Attributes.Add("onclick", "ShowPPBillingSelection();")
        End If
    End Sub

    Private Sub ClearData()
        Me.txtSearch1.Text = String.Empty
        Me.txtSearch2.Text = String.Empty
        Me.txtDealerName.Text = String.Empty
        Me.lboxGroup.SelectedIndex = -1
    End Sub

    Private Sub BindDdlGroup()
        'Dim nTotRow As Integer = 0
        'Dim nPageNumber As Integer = 1
        'Dim nPageSize As Integer = 50

        objDealer = Session("DEALER")
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        '''  - BEGIN : Added by MU, used in FrmUserOrganizationAssignment.aspx
        If Not ViewState("Group") Is Nothing Then
            criterias.opAnd(New Criteria(GetType(DealerGroup), "ID", MatchType.Exact, CInt(ViewState("Group"))))
        End If
        '''  - END   : Added by MU, used in FrmUserOrganizationAssignment.aspx

        ' 08-Nov-2007   Deddy H     Handle bug 1383, bila dari send message bs send ke semua dealer
        If IsNothing(Request.QueryString("All")) Then
            If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                criterias.opAnd(New Criteria(GetType(DealerGroup), "ID", MatchType.Exact, CInt(objDealer.DealerGroup.ID)))
            End If
        End If

        'ddlGroup.DataSource = New DealerGroupFacade(User).RetrieveActiveList(criterias, nPageNumber, nPageSize, nTotRow, "GroupName", Sort.SortDirection.ASC)
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(DealerGroup), "GroupName", Sort.SortDirection.ASC))
        lboxGroup.DataSource = New DealerGroupFacade(User).Retrieve(criterias, sortColl)
        lboxGroup.DataTextField = "GroupName"
        lboxGroup.DataValueField = "ID"
        lboxGroup.DataBind()

        ''  - BEGIN : Added by MU, used in FrmUserOrganizationAssignment.aspx
        'If ViewState("Group") Is Nothing Then
        'lboxGroup.Items.Insert(0, New ListItem("Pilih Group", ""))
        'End If
        ''  - END   : Added by MU, used in FrmUserOrganizationAssignment.aspx
    End Sub

    Private Function GetSelectedItem(ByVal listboxGroup As ListBox) As String
        Dim _strGroup As String = String.Empty
        If Not listboxGroup.SelectedIndex = -1 Then
            'For Each item As ListItem In listboxGroup.Items
            '    If _strGroup = String.Empty Then
            '        _strGroup = item.Value
            '    Else
            '        _strGroup = _strGroup & "," & item.Value
            '    End If
            'Next
            'Else

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
        objDealer = Session("DEALER")
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "Status", MatchType.Exact, CType(EnumDealerStatus.DealerStatus.Aktive, String)))
        If objDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
            If objDealer.CreditAccount <> "" Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "CreditAccount", MatchType.Exact, objDealer.CreditAccount))
            Else
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "CreditAccount", MatchType.Exact, objDealer.DealerCode))
            End If
            'If ViewState("Group") Is Nothing Then
            '    ' 08-Nov-2007   Deddy H     Handle bug 1383, bila dari send message bs send ke semua dealer
            '    If IsNothing(Request.QueryString("All")) Then
            '        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            '            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
            '        End If
            '    End If
            'End If
        End If


        If Not txtDealerName.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerName", MatchType.[Partial], txtDealerName.Text))
        End If

        If Not txtSearch1.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "SearchTerm1", MatchType.[Partial], txtSearch1.Text))
        End If
        If Not txtSearch2.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "SearchTerm2", MatchType.[Partial], txtSearch2.Text))
        End If

        If Not ViewState("Dealer") Is Nothing Then
            Try
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.No, CInt(ViewState("Dealer"))))
            Catch ex As Exception
                'None
            End Try
        End If

        Dim SelectedGroup As String = GetSelectedItem(lboxGroup)
        If SelectedGroup <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerGroup.ID", MatchType.InSet, "(" & SelectedGroup & ")"))
        End If
        dtgDealerSelection.DataSource = New DealerFacade(User).RetrieveActiveList(criterias, ViewState("SortColDealer"), ViewState("SortDirDealer"))
        dtgDealerSelection.DataBind()
    End Sub

    Private Sub BindSearchBilling()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "Status", MatchType.Exact, CType(EnumDealerStatus.DealerStatus.Aktive, String)))

        objDealer = Session("DEALER")
        If objDealer.Title <> EnumDealerTittle.DealerTittle.KTB Then
            If objDealer.CreditAccount <> "" Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "CreditAccount", MatchType.Exact, objDealer.CreditAccount))
            Else
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "CreditAccount", MatchType.Exact, objDealer.DealerCode))
            End If

        End If

        If Not txtDealerName.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerName", MatchType.[Partial], txtDealerName.Text))
        End If

        If Not txtSearch1.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "SearchTerm1", MatchType.[Partial], txtSearch1.Text))
        End If
        If Not txtSearch2.Text = "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "SearchTerm2", MatchType.[Partial], txtSearch2.Text))
        End If

        If Not ViewState("Dealer") Is Nothing Then
            Try
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.No, CInt(ViewState("Dealer"))))
            Catch ex As Exception
                'None
            End Try
        End If

        Dim SelectedGroup As String = GetSelectedItem(lboxGroup)
        If SelectedGroup <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerGroup.ID", MatchType.InSet, "(" & SelectedGroup & ")"))
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
        SearchData()
    End Sub

    Private Sub SearchData()
        If ViewState("Mode") = "Dealer" Then
            BindSearch()
        Else
            BindSearchBilling()
        End If
        'BindSearch()

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
End Class
