#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Claim
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports System.Drawing.Color

#End Region


Public Class PopUpClaimSelectionOne
    Inherits System.Web.UI.Page

#Region "Variable Deklaration"
    Dim oDealer As Dealer
    Dim sesHelper As SessionHelper = New SessionHelper
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtDealerCode1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Private ArlClaimHeader As ArrayList
#End Region
    
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgListClaim As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents chkClaimDate As System.Web.UI.WebControls.CheckBox
    Protected WithEvents icClaimDateFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icClaimDateUntil As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtClaimNo As System.Web.UI.WebControls.TextBox
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        oDealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        If (oDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            txtDealerCode.Text = oDealer.DealerCode
            txtDealerCode.Enabled = False
            lblSearchDealer.Enabled = False
        ElseIf (oDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            txtDealerCode.Enabled = True
        End If

        If Not IsPostBack Then
            ViewState("currSortColumn") = "Status"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            BindStatus(ddlStatus)
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dtgListClaim.CurrentPageIndex = 0
        BindToGrid(dtgListClaim.CurrentPageIndex)
        If dtgListClaim.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub dtgListClaim_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgListClaim.ItemCommand
        
    End Sub

    Private Sub dtgListClaim_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgListClaim.ItemDataBound
        If (e.Item.ItemIndex >= 0) Then
            Dim i As Integer = 0
            Dim Da As DealerAdditional = New DealerAdditional
            Dim arrDA As ArrayList = New ArrayList
            Dim TotalChDetails As Double = 0
            Dim ch As ClaimHeader = CType(e.Item.DataItem, ClaimHeader)

            For i = 0 To ch.ClaimDetails.Count - 1
                Dim cd As ClaimDetail = New ClaimDetail
                cd = ch.ClaimDetails(i)
                TotalChDetails = TotalChDetails + (cd.Qty * cd.SparePartPOStatusDetail.ClaimPriceUnit)
            Next

            Dim Status As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim Total As Label = CType(e.Item.FindControl("lblTotal"), Label)
            
            Total.Text = TotalChDetails.ToString("#,###")

            If ch.Status = EnumClaimStatus.ClaimStatus.Baru Then
                Status.Text = "Baru"
            ElseIf ch.Status = EnumClaimStatus.ClaimStatus.Batal Then
                Status.Text = "Batal"
                'ElseIf ch.Status = EnumClaimStatus.ClaimStatus.Ditolak Then
                '    Status.Text = "Ditolak"
            ElseIf ch.Status = EnumClaimStatus.ClaimStatus.Dikirim Then
                Status.Text = "Dikirim"
            ElseIf ch.Status = EnumClaimStatus.ClaimStatus.Proses Then
                Status.Text = "Proses"
            ElseIf ch.Status = EnumClaimStatus.ClaimStatus.Selesai Then
                Status.Text = "Selesai"
            End If
            i = 0
        End If

        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As ClaimHeader = CType(e.Item.DataItem, ClaimHeader)
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
                e.Item.Cells(0).Controls.Add(rdbChoice)
            End If
        End If
    End Sub

    Private Sub dtgListClaim_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgListClaim.SortCommand
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

        dtgListClaim.SelectedIndex = -1
        BindToGrid(dtgListClaim.CurrentPageIndex)


    End Sub

#End Region

#Region "Custom Method"

    Sub BindStatus(ByVal ddl As DropDownList)

        ddl.DataSource = New EnumClaimStatus().RetrieveStatus()
        ddl.DataTextField = "NameStatus"
        ddl.DataValueField = "ValStatus"
        ddl.DataBind()
        ddl.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
    End Sub

    Sub BindToGrid(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If ddlStatus.SelectedValue <> -1 Then criterias.opAnd(New Criteria(GetType(ClaimHeader), "Status", MatchType.Exact, ddlStatus.SelectedValue))

        If txtDealerCode.Text <> String.Empty Then criterias.opAnd(New Criteria(GetType(ClaimHeader), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text))

        'If chkFakturDate.Checked Then
        '    If icFakturDateFrom.Value <= icFakturDateUntil.Value Then
        '        criterias.opAnd(New Criteria(GetType(ClaimHeader), "SparePartPOStatus.BillingDate", MatchType.GreaterOrEqual, icFakturDateFrom.Value))
        '        criterias.opAnd(New Criteria(GetType(ClaimHeader), "SparePartPOStatus.BillingDate", MatchType.LesserOrEqual, icFakturDateUntil.Value.AddDays(1)))
        '    Else
        '        MessageBox.Show("Tanggal Faktur 'Dari' harus lebih besar atau sama dengan Tanggal Faktur 'Sampai'")
        '        Exit Sub
        '    End If
        'End If

        If chkClaimDate.Checked Then
            If icClaimDateFrom.Value <= icClaimDateUntil.Value Then
                criterias.opAnd(New Criteria(GetType(ClaimHeader), "ClaimDate", MatchType.GreaterOrEqual, icClaimDateFrom.Value))
                criterias.opAnd(New Criteria(GetType(ClaimHeader), "ClaimDate", MatchType.LesserOrEqual, icClaimDateUntil.Value.AddDays(1)))
            Else
                MessageBox.Show("Tanggal claim dari harus lebih besar atau sama dengan tanggal claim sampai")
                Exit Sub
            End If
        End If

        If txtClaimNo.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(ClaimHeader), "ClaimNo", MatchType.Exact, txtClaimNo.Text))
        End If

        ArlClaimHeader = New ClaimHeaderFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dtgListClaim.PageSize, _
              total, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        If (ArlClaimHeader.Count > 0) Then
            dtgListClaim.Visible = True
            dtgListClaim.VirtualItemCount = total
            dtgListClaim.DataSource = ArlClaimHeader
            dtgListClaim.DataBind()
        Else
            dtgListClaim.Visible = False
            MessageBox.Show(SR.DataNotFound("Daftar Claim"))
        End If
    End Sub
#End Region




    Private Sub dtgListClaim_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgListClaim.PageIndexChanged
        dtgListClaim.CurrentPageIndex = e.NewPageIndex
        BindToGrid(dtgListClaim.CurrentPageIndex)
    End Sub
End Class
