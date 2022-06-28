Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.General

Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports KTB.DNet.BusinessFacade.Helper

Public Class FrmEventDealerList
    Inherits System.Web.UI.Page

    Private sessEventDealerList As String = "SessionFrmEventDealerList"
    Private sesHelper As New SessionHelper
    Private displayPriv As Boolean
    Private editPriv As Boolean
    Private deletePriv As Boolean
    Private oDealer As Dealer
    Private Property SesDealer() As Dealer
        Get
            Return CType(sesHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sesHelper.SetSession("DEALER", Value)
        End Set
    End Property

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtKodeDealer.Text = ""
        txtKodeTempOut.Text = ""
        icPeriodeStart.Value = Date.Now
        icPeriodeEnd.Value = Date.Now
        txtEventName.Text = ""

        Dim crit As Hashtable = New Hashtable
        sesHelper.SetSession("CriteriaFrmEventDealerList", crit)
        btnSearch_Click(Nothing, Nothing)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Authorization()
        If Not IsPostBack Then
            ViewState("currSortColumn") = "CreatedTime"
            ViewState("currSortDirection") = Sort.SortDirection.ASC

            PageInit()
            BindDDLDealerCategory()

            If IsLoginAsDealer() Then
                txtKodeDealer.Text = SesDealer().DealerCode
                hdnDealer.Value = txtKodeDealer.Text
                lblKodeDealer.Text = SesDealer().DealerCode & " / " & SesDealer().DealerName
                txtKodeDealer.Attributes("style") = "display:none"
                lblPopUpDealer.Attributes("style") = "display:none"
            Else
                txtKodeDealer.Attributes("style") = "display:table-row"
                lblPopUpDealer.Attributes("style") = "display:table-row"
                lblKodeDealer.Text = ""
                hdnDealer.Value = ""
                txtKodeDealer.Text = ""
            End If

            '-- Restore selection criteria
            RestoreCriteria()
            ReadData()   '-- Read all data matching criteria
            BindGrid(dgEventDealerList.CurrentPageIndex)  '-- Bind page-1
        End If

        lblPopUpDealer.Attributes("onclick") = "ShowPopUpDealer()"
        lblPopUpTO.Attributes("onclick") = "ShowPopUpTO()"
    End Sub

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

    Private Sub Authorization()
        If Not SecurityProvider.Authorize(Context.User, SR.EVENT_Daftar_EventDealer_Display_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=EVENT - DAFTAR EVENT DEALER")
        Else
            displayPriv = SecurityProvider.Authorize(Context.User, SR.EVENT_Daftar_EventDealer_Detail_Privilege)
            editPriv = SecurityProvider.Authorize(Context.User, SR.EVENT_Daftar_EventDealer_Edit_Privilege)
            deletePriv = SecurityProvider.Authorize(Context.User, SR.EVENT_Daftar_EventDealer_Delete_Privilege)
        End If
    End Sub

    Private Sub PageInit()
        icPeriodeStart.Value = Date.Now.AddMonths(-1)
        icPeriodeEnd.Value = Date.Now
    End Sub

    Private Sub BindDDLDealerCategory()
        Dim arrDDL As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(Category), "ProductCategory.Code", MatchType.Exact, "MMC"))
        arrDDL = New CategoryFacade(User).Retrieve(criterias)

        With ddlCategory
            .Items.Clear()
            .DataSource = arrDDL
            .DataTextField = "CategoryCode"
            .DataValueField = "ID"
            .DataBind()
            .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        End With
    End Sub

    Private Sub BindGrid(ByVal pageIndex As Integer)
        Dim arrEventDealerHeaderList As ArrayList = CType(sesHelper.GetSession(sessEventDealerList), ArrayList)
        If arrEventDealerHeaderList.Count <> 0 Then
            CommonFunction.SortListControl(arrEventDealerHeaderList, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arrEventDealerHeaderList, pageIndex, dgEventDealerList.PageSize)
            dgEventDealerList.DataSource = PagedList
            dgEventDealerList.VirtualItemCount = arrEventDealerHeaderList.Count()
            dgEventDealerList.DataBind()
        Else
            dgEventDealerList.DataSource = New ArrayList
            dgEventDealerList.VirtualItemCount = 0
            dgEventDealerList.CurrentPageIndex = 0
            dgEventDealerList.DataBind()
        End If
    End Sub

    Private Sub ReadData()
        '-- Row status = active
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EventDealerHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If ddlCategory.SelectedValue <> "-1" Then
            crit.opAnd(New Criteria(GetType(EventDealerHeader), "Category.ID", MatchType.Exact, ddlCategory.SelectedValue))
        End If

        If cbDate.Checked Then
            Dim tglFrom As New Date(icPeriodeStart.Value.Year, icPeriodeStart.Value.Month, icPeriodeStart.Value.Day, 0, 0, 0)
            Dim tglTo As New Date(icPeriodeEnd.Value.Year, icPeriodeEnd.Value.Month, icPeriodeEnd.Value.Day, 23, 59, 59)
            crit.opAnd(New Criteria(GetType(EventDealerHeader), "PeriodStart", MatchType.GreaterOrEqual, Format(tglFrom, "yyyy-MM-dd HH:mm:ss")), "((", True)
            crit.opAnd(New Criteria(GetType(EventDealerHeader), "PeriodStart", MatchType.LesserOrEqual, Format(tglTo, "yyyy-MM-dd HH:mm:ss")), ")", False)

            crit.opOr(New Criteria(GetType(EventDealerHeader), "PeriodEnd", MatchType.GreaterOrEqual, Format(tglFrom, "yyyy-MM-dd HH:mm:ss")), "(", True)
            crit.opAnd(New Criteria(GetType(EventDealerHeader), "PeriodEnd", MatchType.LesserOrEqual, Format(tglTo, "yyyy-MM-dd HH:mm:ss")), "))", False)
        End If

        If txtKodeDealer.Text.Trim <> "" Then
            Dim sSql As String = String.Empty
            sSql = "Select distinct a.ID "
            sSql += "From EventDealerHeader a "
            sSql += "join EventDealerDetail b on a.ID = b.EventDealerHeaderID and b.RowStatus = 0 "
            sSql += "join Dealer c on b.DealerID = c.ID and c.RowStatus = 0 "
            sSql += "where a.RowStatus = 0 "
            sSql += "and c.DealerCode in ('" & txtKodeDealer.Text.Trim.Replace(";", "','") & "')"

            crit.opAnd(New Criteria(GetType(EventDealerHeader), "ID", MatchType.InSet, "(" & sSql & ")"))
        End If

        If txtKodeTempOut.Text.Trim <> "" Then
            Dim strID As String = String.Empty
            Dim strData() As String = txtKodeTempOut.Text.Split(";")
            For index As Integer = 0 To strData.Length - 1
                Dim intID As Integer = New DealerBranchFacade(User).Retrieve(strData(0)).ID
                If strID.Trim = "" Then
                    strID = intID.ToString
                Else
                    strID += "," & intID.ToString
                End If
            Next
            Dim sSql As String = String.Empty
            sSql = "Select distinct a.ID "
            sSql += "From EventDealerHeader a "
            sSql += "join EventDealerDetail b on a.ID = b.EventDealerHeaderID and b.RowStatus = 0 "
            sSql += "join Dealer c on b.DealerID = c.ID and c.RowStatus = 0 "
            sSql += "where a.RowStatus = 0 "
            sSql += "and c.DealerCode in ('" & txtKodeDealer.Text.Trim.Replace(";", "','") & "') "
            sSql += "and b.DealerBranchID in (" & strID & ") "

            crit.opAnd(New Criteria(GetType(EventDealerHeader), "ID", MatchType.InSet, "(" & sSql & ")"))
        End If
        If txtEventName.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(EventDealerHeader), "EventName", MatchType.Partial, Me.txtEventName.Text.Trim))
        End If

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(EventDealerHeader), CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection)))

        '-- Retrieve recordset
        Dim arrEventDealerHeaderList As ArrayList = New EventDealerHeaderFacade(User).RetrieveByCriteria(crit, sortColl)
        sesHelper.SetSession(sessEventDealerList, arrEventDealerHeaderList)
        If arrEventDealerHeaderList.Count <= 0 Then
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Data"))
            End If
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ReadData()
        dgEventDealerList.CurrentPageIndex = 0 '-- Read all data matching criteria
        BindGrid(dgEventDealerList.CurrentPageIndex)  '-- Bind page-1
        StoreCriteria()
    End Sub

    Private Sub dgEventDealerList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgEventDealerList.PageIndexChanged
        '-- Change datagrid page

        ReadData()
        dgEventDealerList.CurrentPageIndex = e.NewPageIndex
        BindGrid(e.NewPageIndex)
        StoreCriteria()
    End Sub

    Private Sub dgEventDealerList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgEventDealerList.SortCommand
        '-- Sort datagrid rows based on a column header clicked

        If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currSortDirection") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("currSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColumn") = e.SortExpression
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If

        '-- Bind page-1
        dgEventDealerList.CurrentPageIndex = 0
        ReadData()
        BindGrid(dgEventDealerList.CurrentPageIndex)
        StoreCriteria()
    End Sub

    Private Sub RestoreCriteria()
        '-- Restore selection criteria
        Dim crit As Hashtable
        crit = CType(sesHelper.GetSession("CriteriaFrmEventDealerList"), Hashtable)
        If Not crit Is Nothing Then
            txtKodeDealer.Text = CStr(crit.Item("DealerCode"))
            lblKodeDealer.Text = CStr(crit.Item("DealerCodeName"))
            txtKodeTempOut.Text = CStr(crit.Item("DealerBranchCode"))
            icPeriodeStart.Value = CStr(crit.Item("PeriodStart"))
            icPeriodeEnd.Value = CStr(crit.Item("PeriodEnd"))
            txtEventName.Text = CStr(crit.Item("EventName"))

            ViewState("currSortColumn") = CStr(crit.Item("currSortColumn"))
            ViewState("currSortDirection") = CStr(crit.Item("currSortDirection"))
            dgEventDealerList.CurrentPageIndex = CInt(crit.Item("PageIndex"))
        End If
    End Sub

    Private Sub StoreCriteria()
        Dim crit As Hashtable = New Hashtable
        crit.Add("DealerCode", txtKodeDealer.Text)
        crit.Add("DealerCodeName", lblKodeDealer.Text)
        crit.Add("DealerBranchCode", txtKodeTempOut.Text)
        crit.Add("PeriodStart", icPeriodeStart.Value)
        crit.Add("PeriodEnd", icPeriodeEnd.Value)
        crit.Add("EventName", txtEventName.Text)
        crit.Add("PageIndex", dgEventDealerList.CurrentPageIndex)
        crit.Add("currSortColumn", ViewState("currSortColumn"))
        crit.Add("currSortDirection", ViewState("currSortDirection"))

        sesHelper.SetSession("CriteriaFrmEventDealerList", crit)  '-- Store in session
    End Sub

    Protected Sub dgEventDealerList_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgEventDealerList.ItemCommand
        Select Case e.CommandName
            Case "Detail"
                Response.Redirect("FrmInputEvent.aspx?Mode=Detail&EventDealerHeaderID=" & e.Item.Cells(0).Text)
            Case "Edit"
                Response.Redirect("FrmInputEvent.aspx?Mode=Edit&EventDealerHeaderID=" & e.Item.Cells(0).Text)
            Case "Delete"
                DeleteEventDealerHeader(CInt(e.CommandArgument))
        End Select
    End Sub

    Private Sub DeleteEventDealerHeader(ByVal intEventDealerHeaderID As Integer)
        Dim objBabitEventProposalHeaderFacade As BabitEventProposalHeaderFacade = New BabitEventProposalHeaderFacade(User)
        Dim arrBabitEventProposalHeader As ArrayList = New ArrayList
        Dim criteriasaa As New CriteriaComposite(New Criteria(GetType(BabitEventProposalHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriasaa.opAnd(New Criteria(GetType(BabitEventProposalHeader), "EventDealerHeader.ID", MatchType.Exact, intEventDealerHeaderID))
        arrBabitEventProposalHeader = objBabitEventProposalHeaderFacade.Retrieve(criteriasaa)
        If Not IsNothing(arrBabitEventProposalHeader) AndAlso arrBabitEventProposalHeader.Count > 0 Then
            MessageBox.Show("Nama Event ini sudah di pakai di Proposal Event")
            Exit Sub
        End If

        Dim objEventDealerHeaderFacade As EventDealerHeaderFacade = New EventDealerHeaderFacade(User)
        Dim objEventDealerDetailFacade As EventDealerDetailFacade = New EventDealerDetailFacade(User)
        Dim objEventDealerDocumentFacade As EventDealerDocumentFacade = New EventDealerDocumentFacade(User)
        Dim objEventDealerRequiredDocumentFacade As EventDealerRequiredDocumentFacade = New EventDealerRequiredDocumentFacade(User)

        Dim objEventDealerHeader As EventDealerHeader = objEventDealerHeaderFacade.Retrieve(intEventDealerHeaderID)
        objEventDealerHeader.RowStatus = CType(DBRowStatus.Deleted, Short)

        Dim arrEventDealerDetail As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(EventDealerDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(EventDealerDetail), "EventDealerHeader.ID", MatchType.Exact, intEventDealerHeaderID))
        arrEventDealerDetail = objEventDealerDetailFacade.Retrieve(criterias)

        Dim arrEventDealerDoc As ArrayList = New ArrayList
        Dim criterias2 As New CriteriaComposite(New Criteria(GetType(EventDealerDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias2.opAnd(New Criteria(GetType(EventDealerDocument), "EventDealerHeader.ID", MatchType.Exact, intEventDealerHeaderID))
        arrEventDealerDoc = objEventDealerDocumentFacade.Retrieve(criterias2)

        Dim arrEventDealerReqDoc As ArrayList = New ArrayList
        Dim criterias3 As New CriteriaComposite(New Criteria(GetType(EventDealerRequiredDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias3.opAnd(New Criteria(GetType(EventDealerRequiredDocument), "EventDealerHeader.ID", MatchType.Exact, intEventDealerHeaderID))
        arrEventDealerReqDoc = objEventDealerRequiredDocumentFacade.Retrieve(criterias3)

        Dim _result As Integer = 0
        _result = New EventDealerHeaderFacade(User).DeleteTransaction(objEventDealerHeader, arrEventDealerDetail, arrEventDealerDoc, arrEventDealerReqDoc)
        If _result > 0 Then
            MessageBox.Show("Delete Data Sukses")
        End If
        ReadData()
        BindGrid(dgEventDealerList.CurrentPageIndex)
    End Sub

    Protected Sub dgEventDealerList_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgEventDealerList.ItemDataBound
        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
        Dim lblTempOut As Label = CType(e.Item.FindControl("lblTempOut"), Label)
        Dim lblEventDealerName As Label = CType(e.Item.FindControl("lblEventDealerName"), Label)

        Dim lblPeriodStart As Label = CType(e.Item.FindControl("lblPeriodStart"), Label)
        Dim lblPeriodEnd As Label = CType(e.Item.FindControl("lblPeriodEnd"), Label)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oData As EventDealerHeader = CType(e.Item.DataItem, EventDealerHeader)

            lblNo.Text = e.Item.ItemIndex + 1
            If Not IsNothing(oData.EventDealerDetails) AndAlso oData.EventDealerDetails.Count > 0 Then
                Dim strDealerCode As String = String.Empty
                Dim strDealerBranchCode As String = String.Empty
                For Each oEDD As EventDealerDetail In oData.EventDealerDetails
                    If Not IsNothing(oEDD.Dealer) Then
                        If strDealerCode.Trim = "" Then
                            strDealerCode = oEDD.Dealer.DealerCode
                        Else
                            strDealerCode += ", " & oEDD.Dealer.DealerCode
                        End If
                    End If

                    If Not IsNothing(oEDD.DealerBranch) Then
                        If strDealerBranchCode.Trim = "" Then
                            strDealerBranchCode = oEDD.DealerBranch.DealerBranchCode
                        Else
                            strDealerBranchCode += ", " & oEDD.DealerBranch.DealerBranchCode
                        End If
                    End If
                Next
                lblDealer.Text = strDealerCode
                lblTempOut.Text = strDealerBranchCode
            End If

            lblEventDealerName.Text = oData.EventName
            lblPeriodStart.Text = oData.PeriodStart
            lblPeriodEnd.Text = oData.PeriodEnd

            Dim lnkbtnDetail As LinkButton = CType(e.Item.FindControl("lnkbtnDetail"), LinkButton)
            Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
            Dim lnkbtnDelete As LinkButton = CType(e.Item.FindControl("lnkbtnDelete"), LinkButton)

            If Not IsNothing(lnkbtnDetail) Then
                lnkbtnDetail.Visible = displayPriv
            End If
            If Not IsNothing(lnkbtnEdit) Then
                lnkbtnEdit.Visible = editPriv
            End If
            If Not IsNothing(lnkbtnDelete) Then
                lnkbtnDelete.Visible = deletePriv
            End If
            If IsLoginAsDealer() Then
                lnkbtnDetail.Visible = displayPriv
                lnkbtnEdit.Visible = False
                lnkbtnDelete.Visible = False
            End If
        End If
    End Sub

    Protected Sub hdnDealer_ValueChanged(sender As Object, e As EventArgs) Handles hdnDealer.ValueChanged
        'Dim data As String() = hdnDealer.Value.Trim.Split(";")
        txtKodeDealer.Text = hdnDealer.Value.Trim
    End Sub

    Protected Sub hdnTempOut_ValueChanged(sender As Object, e As EventArgs) Handles hdnTempOut.ValueChanged
        'Dim data As String() = hdnTempOut.Value.Trim.Split(";")
        txtKodeTempOut.Text = hdnTempOut.Value.Trim
    End Sub

End Class