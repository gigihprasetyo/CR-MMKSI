#Region "Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
#End Region

Public Class PopUpAplikasiSPLSelection
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents txtCustomerName As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgSPL As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents txtSPLNumber As System.Web.UI.WebControls.TextBox

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private strDealerCode As String = String.Empty
    Private strTipeKendaraan As String = String.Empty
    Private strDiscountType As String = String.Empty

    Private Sub ClearData()
        Me.txtSPLNumber.Text = String.Empty
        Me.txtCustomerName.Text = String.Empty
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "SPLNumber"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        ClearData()
        btnSearch_Click(Nothing, Nothing)
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        dtgSPL.DataSource = New SPLFacade(User).RetrieveActiveList(CriteriaSearch(), indexPage + 1, _
            dtgSPL.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgSPL.VirtualItemCount = totalRow
        dtgSPL.DataBind()
    End Sub

    Public Function CriteriaSearch() As CriteriaComposite
        Dim strSQL As String = ""
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SPL), "ApprovalStatus", MatchType.Exact, 5))
        If strDealerCode <> "" Then
            criterias.opAnd(New Criteria(GetType(SPL), "DealerName", MatchType.[Partial], strDealerCode))
        End If
        If strDiscountType <> "" Then
            strSQL = "Select a.ID from SPL a join SPLDetail b on a.ID = b.SPLID and b.RowStatus = 0 "
            strSQL += "join SPLDetailtoSPL c on b.ID = c.SPLDetailID and c.RowStatus = 0 where a.RowStatus = 0 and c.DiscountMasterID = " & strDiscountType
            strSQL += " AND b.[PeriodMonth] = MONTH(GETDATE()) AND [b].[PeriodYear] = YEAR(GETDATE())"
            criterias.opAnd(New Criteria(GetType(SPL), "ID", MatchType.InSet, "(" & strSQL & ")"))
        End If

        If strTipeKendaraan <> "" Then
            Dim intVechileTypeID As Integer = 0
            Dim strVechileTypeCode As String = ""
            Dim objVechileType As New VechileType
            If IsNumeric(strTipeKendaraan) Then
                intVechileTypeID = strTipeKendaraan
                objVechileType = New VechileTypeFacade(User).Retrieve(intVechileTypeID)
            Else
                strVechileTypeCode = strTipeKendaraan
                objVechileType = New VechileTypeFacade(User).Retrieve(strVechileTypeCode)
            End If

            strSQL = "Select a.ID From SPL a join SPLDetail b on a.ID = b.SPLID Where a.RowStatus = 0 and b.RowStatus = 0 and b.VehicleTypeID = " & objVechileType.ID
            strSQL += " AND b.[PeriodMonth] = MONTH(GETDATE()) AND [b].[PeriodYear] = YEAR(GETDATE())"
            criterias.opAnd(New Criteria(GetType(SPL), "ID", MatchType.InSet, "(" & strSQL & ")"))
        End If
        If txtSPLNumber.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(SPL), "SPLNumber", MatchType.[Partial], txtSPLNumber.Text))
        End If
        If txtCustomerName.Text.Trim <> "" Then
            criterias.opAnd(New Criteria(GetType(SPL), "CustomerName", MatchType.[Partial], txtCustomerName.Text))
        End If
        Return criterias
    End Function

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Try
            strDealerCode = Request.QueryString("DealerCode")
        Catch
        End Try
        strTipeKendaraan = Request.QueryString("TipeKendaraan")
        strDiscountType = Request.QueryString("DiscountType")
        If Not Page.IsPostBack Then
            InitiatePage()
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        BindDataGrid(0)
        If dtgSPL.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub

    Private Sub dtgPosisi_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSPL.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""rbSelectDealer"">")
            e.Item.Cells(0).Controls.Add(rdbChoice)

            Dim lblSPLNumber As Label = CType(e.Item.FindControl("lblSPLNumber"), Label)
            Dim lblDealerName As Label = CType(e.Item.FindControl("lblDealerName"), Label)
            Dim lblCustomerName As Label = CType(e.Item.FindControl("lblCustomerName"), Label)

            Dim objSPL As SPL = CType(e.Item.DataItem, SPL)
            lblSPLNumber.Text = objSPL.SPLNumber
            lblDealerName.Text = FormatDealerName(objSPL.DealerName)
            lblCustomerName.Text = objSPL.CustomerName
        End If
    End Sub

    Private Function FormatDealerName(ByVal _DealerName As String) As String
        Dim result As String
        Dim DealerCount As Integer = _DealerName.Split(";").Length - 1
        Dim PosToInsert As Integer = 0
        For i As Integer = 1 To DealerCount
            PosToInsert = _DealerName.IndexOf(";", PosToInsert) + 1
            If i Mod 10 = 0 Then
                _DealerName = _DealerName.Insert(PosToInsert, "<br>")
            End If
        Next

        Return _DealerName
    End Function

    Private Sub dtgPosisi_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSPL.PageIndexChanged
        dtgSPL.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgSPL.CurrentPageIndex)
    End Sub

    Private Sub dtgPosisi_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSPL.SortCommand
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
        dtgSPL.CurrentPageIndex = 0
        BindDataGrid(dtgSPL.CurrentPageIndex)
    End Sub

End Class
