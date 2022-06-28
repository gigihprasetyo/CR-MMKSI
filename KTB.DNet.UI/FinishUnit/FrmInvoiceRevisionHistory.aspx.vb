
#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
Imports System.Linq
#End Region

#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade

#End Region

Public Class FrmInvoiceRevisionHistory
    Inherits System.Web.UI.Page

#Region " Private fields "
    Dim _objChassisMaster As ChassisMaster
    Dim sessHelp As SessionHelper = New SessionHelper
    Dim InvoiceList As New ArrayList  '-- List of invoice
    Dim _PCAccessAllowed As Boolean = False
    Dim _CVAccessAllowed As Boolean = False
    Dim _LCVAccessAllowed As Boolean = False
    Private objDealer As Dealer
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        'InitiateAuthorization()
        If Not IsPostBack Then
            ViewState("currSortColumn") = "CreatedTime"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            Dim crit As Hashtable = New Hashtable
            crit = CType(Session("CriteriaFormInvoiceResultList"), Hashtable)
            If Not crit Is Nothing Then
                dgInvoiceList.CurrentPageIndex = CInt(crit.Item("PageIndex"))
            End If

            ReadData()   '-- Read all data matching criteria
            BindPage(dgInvoiceList.CurrentPageIndex)  '-- Bind page-1
        End If
    End Sub

    Private Sub ReadData()

        ''-- Row status = active
        'Dim criterias As New CriteriaComposite(New Criteria(GetType(EndCustomerRev), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(EndCustomerRev), "EndCustomerID", MatchType.Exact, _objChassisMaster.EndCustomer.ID))

        'objDealer = Session("DEALER")
        'If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EndCustomerRev), "ChassisMaster.Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        'End If


        ''-- Retrieve recordset
        'Dim InvoiceResultList As ArrayList = New EndCustomerRevFacade(User).RetrieveByCriteria(criterias, CType(ViewState("currSortColumn"), String), Sort.SortDirection.ASC)

        '-- Store recordset into session for later use
        'sessHelp.SetSession("InvoiceResList", InvoiceResultList)

        'If InvoiceResultList.Count > 0 Then
        '    '-- Enable all buttons if any record exists

        'Else
        '    If IsPostBack Then
        '        MessageBox.Show(SR.DataNotFound("Data"))
        '    End If

        'End If
        _objChassisMaster = CType(sessHelp.GetSession("ChassisMaster"), ChassisMaster)

        Dim chasssisID As Integer = CType(Request.QueryString("ChassisMasterID"), Integer)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(RevisionFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(RevisionFaktur), "ChassisMaster.ID", MatchType.Exact, chasssisID))
        criterias.opAnd(New Criteria(GetType(RevisionFaktur), "RevisionStatus", MatchType.Exact, CType(EnumDNET.enumFakturKendaraanRev.Selesai, Integer)))

        objDealer = Session("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(RevisionFaktur), "ChassisMaster.Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))
        End If

        Dim revFakturtList As ArrayList = New RevisionFakturFacade(User).RetrieveByCriteria(criterias, CType(ViewState("currSortColumn"), String), Sort.SortDirection.ASC)

        Dim endCustomerList As ArrayList = New ArrayList
        For Each e As RevisionFaktur In revFakturtList

            Dim oldendcust As EndCustomer = e.OldEndCustomer
            oldendcust.RevisionFaktur = New RevisionFaktur
            oldendcust.RevisionFaktur.RevisionStatus = -1
            oldendcust.ChassisMaster = e.ChassisMaster
            endCustomerList.Add(oldendcust)

            e.EndCustomer.RevisionFaktur = New RevisionFaktur
            e.EndCustomer.RevisionFaktur.RevisionStatus = e.RevisionStatus
            e.EndCustomer.RevisionFaktur.RevisionType = e.RevisionType
            e.EndCustomer.RevisionFaktur.CreatedTime = e.CreatedTime
            endCustomerList.Add(e.EndCustomer)
        Next

        Dim q As ArrayList = New System.Collections.ArrayList((From item As EndCustomer In endCustomerList.OfType(Of EndCustomer)()
                            Select item Order By item.ID).ToList())

        sessHelp.SetSession("InvoiceResList", q)

        If revFakturtList.Count < 1 Then
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Data"))
            End If
        End If

    End Sub

    Private Sub BindPage(ByVal pageIndex As Integer)
        Dim InvoiceResList As ArrayList = CType(sessHelp.GetSession("InvoiceResList"), ArrayList)
        If InvoiceResList.Count <> 0 Then
            Dim PagedList As ArrayList = ArrayListPager.DoPage(InvoiceResList, pageIndex, dgInvoiceList.PageSize)
            dgInvoiceList.DataSource = PagedList
            dgInvoiceList.VirtualItemCount = InvoiceResList.Count()
            dgInvoiceList.DataBind()
        Else
            dgInvoiceList.DataSource = New ArrayList
            dgInvoiceList.VirtualItemCount = 0
            dgInvoiceList.CurrentPageIndex = 0
            dgInvoiceList.DataBind()
        End If
        If dgInvoiceList.VirtualItemCount > 0 Then
            lblJumRecord.Text = "Jumlah record : " & dgInvoiceList.VirtualItemCount
        End If
    End Sub

    Private Sub dgInvoiceList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgInvoiceList.SortCommand
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
        dgInvoiceList.CurrentPageIndex = 0
        ReadData()
        BindPage(dgInvoiceList.CurrentPageIndex)

    End Sub

    Private Sub dgInvoiceList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgInvoiceList.ItemCommand

        If e.CommandName = "lnkDetail" Then

            '-- Retrieve Invoice and its related end customer if any
            Dim chassisMaster As ChassisMaster = New ChassisMasterFacade(User).Retrieve(CType(Request.QueryString("ChassisMasterID"), Integer))

            '-- Store Invoice and its related end customer for display on form FrmEntryInvoice.aspx
            sessHelp.SetSession("ChassisMaster", chassisMaster)

            '-- Store the calling page
            sessHelp.SetSession("FrmEntryInvoice_CalledBy", "FrmInvoiceRevisionHistory.aspx?ChassisMasterID=" & CType(Request.QueryString("ChassisMasterID"), Integer))

            '-- Display Invoice and its related end customer on Entry Invoice page
            Server.Transfer("FrmEntryInvoice.aspx?ChassisMasterID=" & CType(Request.QueryString("ChassisMasterID"), Integer))
        End If

    End Sub

    Private Sub dgInvoiceList_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgInvoiceList.ItemDataBound
        '-- Handle data binding

        Dim RowValue As EndCustomer = CType(e.Item.DataItem, EndCustomer)

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '-- Grid detail items

            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = (dgInvoiceList.CurrentPageIndex * dgInvoiceList.PageSize + e.Item.ItemIndex + 1).ToString()  '-- Column No

            Dim lblDealer As Label = CType(e.Item.FindControl("lblDealer"), Label)
            Try
                lblDealer.ToolTip = RowValue.ChassisMaster.Dealer.SearchTerm1  '-- Bind searchTerm1 as tooltip

            Catch ex As Exception
            End Try

            Dim lblValidateTime As Label = e.Item.FindControl("lblValidateTime")
            Dim lblConfirmTime As Label = e.Item.FindControl("lblConfirmTime")
            Dim lblPrintedTime As Label = e.Item.FindControl("lblPrintedTime")
            Dim lblCreatedTime As Label = e.Item.FindControl("lblCreatedTime")
            Dim lblFakturStatusDesc As Label = e.Item.FindControl("lblFakturStatusDesc")

            lblValidateTime.Text = FormatDate(RowValue.ValidateTime)
            lblConfirmTime.Text = FormatDate(RowValue.ConfirmTime)
            lblPrintedTime.Text = FormatDate(RowValue.PrintedTime)
            lblCreatedTime.Text = FormatDate(RowValue.RevisionFaktur.CreatedTime)

            If RowValue.RevisionFaktur.RevisionStatus > -1 Then
                lblFakturStatusDesc.Text = GetRevisionStatusName(RowValue.RevisionFaktur.RevisionStatus)
            End If

        End If
    End Sub


    Private Sub dgInvoiceList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgInvoiceList.PageIndexChanged
        '-- Change datagrid page

        dgInvoiceList.CurrentPageIndex = e.NewPageIndex
        BindPage(e.NewPageIndex)
    End Sub

    Private Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Dim url As String = CType(Session("FrmInvoiceRevisionHistory_CalledBy"), String)
        If Not url Is Nothing Then
            Server.Transfer(url)
        End If
    End Sub

    Private Function FormatDate(ByVal dtVal As Date) As String
        Return IIf(Format(dtVal, "dd/MM/yyyy") = "01/01/1753" Or Format(dtVal, "dd/MM/yyyy") = "01/01/1900", _
                               "", Format(dtVal, "dd/MM/yyyy"))
    End Function

    Public Function GetRevisionStatusName(ByVal intID As Integer) As String
        If intID > -1 Then
            Return ([Enum].Parse(GetType(EnumDNET.enumFakturKendaraanRev), intID, True)).ToString
        Else
            Return String.Empty
        End If

    End Function
End Class