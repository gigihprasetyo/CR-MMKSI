Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Public Class PopUpSparePartPO
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents icPODateFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icPODateUntil As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents dtgSparePartPO As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Deklarasi"
    Dim criterias As CriteriaComposite
    Dim sHelper As New SessionHelper
#End Region

#Region "event"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Dim oDealer As Dealer = sHelper.GetSession("DEALER")
        If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            lblSearchDealer.Visible = False
            txtDealerCode.Enabled = False
            txtDealerCode.BorderStyle = BorderStyle.None
            txtDealerCode.Text = oDealer.DealerCode
        End If
        If Not IsPostBack Then
            viewstate.Add("SortColumn", "CreatedTime")
            viewstate.Add("SortDirection", Sort.SortDirection.ASC)
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection()"
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dtgSparePartPO.CurrentPageIndex = 0
        CreateCriteria()
        BindToGrid(dtgSparePartPO.CurrentPageIndex)
        If dtgSparePartPO.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub dtgSparePartPO_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSparePartPO.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = (dtgSparePartPO.PageSize * dtgSparePartPO.CurrentPageIndex) + e.Item.ItemIndex + 1

            If Not IsNothing(Request.QueryString("IsMultiple")) Then
                If (Not CType(Request.QueryString("IsMultiple"), Boolean)) Then
                    'CType(e.Item.FindControl("chkItemChecked"), CheckBox).Visible = False
                    Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
                    e.Item.Cells(0).Controls.Add(rdbChoice)
                End If
            Else
                Dim chkItem As CheckBox = New CheckBox
                chkItem.ID = "chkItemChecked"
                e.Item.Cells(0).Controls.Add(chkItem)
            End If
        End If
    End Sub

    Private Sub dtgSparePartPO_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSparePartPO.SortCommand
        If e.SortExpression = viewstate("SortColumn") Then
            If viewstate("SortDirection") = Sort.SortDirection.ASC Then
                viewstate.Add("SortDirection", Sort.SortDirection.DESC)
            Else
                viewstate.Add("SortDirection", Sort.SortDirection.ASC)
            End If
        End If
        viewstate.Add("SortColumn", e.SortExpression)
        BindToGrid(0)
    End Sub

    Private Sub dtgSparePartPO_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSparePartPO.PageIndexChanged
        dtgSparePartPO.CurrentPageIndex = e.NewPageIndex
        BindToGrid(dtgSparePartPO.CurrentPageIndex)
    End Sub

#End Region

#Region "Custom Method"

    Private Sub BindToGrid(ByVal indexpage As Integer)
        Dim totalRow As Integer = 0
        If indexpage >= 0 Then
            Dim arlHeader As ArrayList = New SparePartPOFacade(User).RetrieveActiveList(indexpage + 1, dtgSparePartPO.PageSize, totalRow, viewstate("SortColumn"), viewstate("SortDirection"), sHelper.GetSession("crits"))
            dtgSparePartPO.DataSource = arlHeader
            dtgSparePartPO.VirtualItemCount = totalRow
            dtgSparePartPO.DataBind()
        End If
    End Sub

    Private Sub CreateCriteria()
        Dim kdDealer As String = txtDealerCode.Text.Replace(";", "','")
        Dim tglPOStart As Date = icPODateFrom.Value
        Dim tglPOUntil As Date = icPODateUntil.Value

        criterias = New CriteriaComposite(New Criteria(GetType(SparePartPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtDealerCode.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(SparePartPO), "Dealer.DealerCode", MatchType.InSet, "('" & kdDealer & "')"))
        End If

        If Not IsNothing(Request.QueryString("ProcessCodeInSet")) Then
            Dim szInsets As String() = Request.QueryString("ProcessCodeInSet").Split(",")
            Dim szInset As String = "("
            For Each sz As String In szInsets
                szInset &= "'" & sz & "'" & ","
            Next
            szInset = szInset.Substring(0, szInset.Length - 1)
            szInset &= ")"
            criterias.opAnd(New Criteria(GetType(SparePartPO), "ProcessCode", MatchType.InSet, szInset))
        End If

        criterias.opAnd(New Criteria(GetType(SparePartPO), "CreatedTime", MatchType.GreaterOrEqual, tglPOStart))
        criterias.opAnd(New Criteria(GetType(SparePartPO), "CreatedTime", MatchType.LesserOrEqual, tglPOUntil.AddDays(1)))

        sHelper.SetSession("crits", criterias)
    End Sub

#End Region

End Class
